using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x0200010A RID: 266
	internal sealed class VariableBinder : ExpressionVisitor
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x00024070 File Offset: 0x00022270
		internal static AnalyzedTree Bind(LambdaExpression lambda)
		{
			VariableBinder variableBinder = new VariableBinder();
			variableBinder.Visit(lambda);
			return variableBinder._tree;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00024084 File Offset: 0x00022284
		private VariableBinder()
		{
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x000240B8 File Offset: 0x000222B8
		public override Expression Visit(Expression node)
		{
			if (!this._guard.TryEnterOnCurrentStack())
			{
				return this._guard.RunOnEmptyStack<VariableBinder, Expression, Expression>((VariableBinder @this, Expression e) => @this.Visit(e), this, node);
			}
			return base.Visit(node);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00024106 File Offset: 0x00022306
		protected internal override Expression VisitConstant(ConstantExpression node)
		{
			if (this._inQuote)
			{
				return node;
			}
			if (ILGen.CanEmitConstant(node.Value, node.Type))
			{
				return node;
			}
			this._constants.Peek().AddReference(node.Value, node.Type);
			return node;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00024144 File Offset: 0x00022344
		protected internal override Expression VisitUnary(UnaryExpression node)
		{
			if (node.NodeType == ExpressionType.Quote)
			{
				bool inQuote = this._inQuote;
				this._inQuote = true;
				this.Visit(node.Operand);
				this._inQuote = inQuote;
			}
			else
			{
				this.Visit(node.Operand);
			}
			return node;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00024190 File Offset: 0x00022390
		protected internal override Expression VisitLambda<T>(Expression<T> node)
		{
			this._scopes.Push(this._tree.Scopes[node] = new CompilerScope(node, true));
			this._constants.Push(this._tree.Constants[node] = new BoundConstants());
			base.Visit(this.MergeScopes(node));
			this._constants.Pop();
			this._scopes.Pop();
			return node;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00024210 File Offset: 0x00022410
		protected internal override Expression VisitInvocation(InvocationExpression node)
		{
			LambdaExpression lambdaOperand = node.LambdaOperand;
			if (lambdaOperand != null)
			{
				this._scopes.Push(this._tree.Scopes[node] = new CompilerScope(lambdaOperand, false));
				base.Visit(this.MergeScopes(lambdaOperand));
				this._scopes.Pop();
				int i = 0;
				int argumentCount = node.ArgumentCount;
				while (i < argumentCount)
				{
					this.Visit(node.GetArgument(i));
					i++;
				}
				return node;
			}
			return base.VisitInvocation(node);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00024294 File Offset: 0x00022494
		protected internal override Expression VisitBlock(BlockExpression node)
		{
			if (node.Variables.Count == 0)
			{
				base.Visit(node.Expressions);
				return node;
			}
			this._scopes.Push(this._tree.Scopes[node] = new CompilerScope(node, false));
			base.Visit(this.MergeScopes(node));
			this._scopes.Pop();
			return node;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00024300 File Offset: 0x00022500
		protected override CatchBlock VisitCatchBlock(CatchBlock node)
		{
			if (node.Variable == null)
			{
				this.Visit(node.Filter);
				this.Visit(node.Body);
				return node;
			}
			this._scopes.Push(this._tree.Scopes[node] = new CompilerScope(node, false));
			this.Visit(node.Filter);
			this.Visit(node.Body);
			this._scopes.Pop();
			return node;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00024380 File Offset: 0x00022580
		private ReadOnlyCollection<Expression> MergeScopes(Expression node)
		{
			LambdaExpression lambdaExpression = node as LambdaExpression;
			ReadOnlyCollection<Expression> readOnlyCollection;
			if (lambdaExpression != null)
			{
				readOnlyCollection = new ReadOnlyCollection<Expression>(new Expression[] { lambdaExpression.Body });
			}
			else
			{
				readOnlyCollection = ((BlockExpression)node).Expressions;
			}
			CompilerScope compilerScope = this._scopes.Peek();
			while (readOnlyCollection.Count == 1 && readOnlyCollection[0].NodeType == ExpressionType.Block)
			{
				BlockExpression blockExpression = (BlockExpression)readOnlyCollection[0];
				if (blockExpression.Variables.Count > 0)
				{
					foreach (ParameterExpression parameterExpression in blockExpression.Variables)
					{
						if (compilerScope.Definitions.ContainsKey(parameterExpression))
						{
							return readOnlyCollection;
						}
					}
					if (compilerScope.MergedScopes == null)
					{
						compilerScope.MergedScopes = new HashSet<BlockExpression>(global::System.Collections.Generic.ReferenceEqualityComparer<object>.Instance);
					}
					compilerScope.MergedScopes.Add(blockExpression);
					foreach (ParameterExpression parameterExpression2 in blockExpression.Variables)
					{
						compilerScope.Definitions.Add(parameterExpression2, VariableStorageKind.Local);
					}
				}
				readOnlyCollection = blockExpression.Expressions;
			}
			return readOnlyCollection;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x000244D4 File Offset: 0x000226D4
		protected internal override Expression VisitParameter(ParameterExpression node)
		{
			this.Reference(node, VariableStorageKind.Local);
			CompilerScope compilerScope = null;
			foreach (CompilerScope compilerScope2 in this._scopes)
			{
				if (compilerScope2.IsMethod || compilerScope2.Definitions.ContainsKey(node))
				{
					compilerScope = compilerScope2;
					break;
				}
			}
			if (compilerScope.ReferenceCount == null)
			{
				compilerScope.ReferenceCount = new Dictionary<ParameterExpression, int>();
			}
			Helpers.IncrementCount<ParameterExpression>(node, compilerScope.ReferenceCount);
			return node;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00024564 File Offset: 0x00022764
		private void Reference(ParameterExpression node, VariableStorageKind storage)
		{
			CompilerScope compilerScope = null;
			foreach (CompilerScope compilerScope2 in this._scopes)
			{
				if (compilerScope2.Definitions.ContainsKey(node))
				{
					compilerScope = compilerScope2;
					break;
				}
				compilerScope2.NeedsClosure = true;
				if (compilerScope2.IsMethod)
				{
					storage = VariableStorageKind.Hoisted;
				}
			}
			if (compilerScope == null)
			{
				throw Error.UndefinedVariable(node.Name, node.Type, this.CurrentLambdaName);
			}
			if (storage == VariableStorageKind.Hoisted)
			{
				if (node.IsByRef)
				{
					throw Error.CannotCloseOverByRef(node.Name, this.CurrentLambdaName);
				}
				compilerScope.Definitions[node] = VariableStorageKind.Hoisted;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0002461C File Offset: 0x0002281C
		private string CurrentLambdaName
		{
			get
			{
				foreach (CompilerScope compilerScope in this._scopes)
				{
					LambdaExpression lambdaExpression = compilerScope.Node as LambdaExpression;
					if (lambdaExpression != null)
					{
						return lambdaExpression.Name;
					}
				}
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x040002D3 RID: 723
		private readonly AnalyzedTree _tree = new AnalyzedTree();

		// Token: 0x040002D4 RID: 724
		private readonly Stack<CompilerScope> _scopes = new Stack<CompilerScope>();

		// Token: 0x040002D5 RID: 725
		private readonly Stack<BoundConstants> _constants = new Stack<BoundConstants>();

		// Token: 0x040002D6 RID: 726
		private readonly StackGuard _guard = new StackGuard();

		// Token: 0x040002D7 RID: 727
		private bool _inQuote;
	}
}
