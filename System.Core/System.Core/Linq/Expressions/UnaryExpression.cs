using System;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	/// <summary>Represents an expression that has a unary operator.</summary>
	// Token: 0x020000DC RID: 220
	[DebuggerTypeProxy(typeof(Expression.UnaryExpressionProxy))]
	public sealed class UnaryExpression : Expression
	{
		// Token: 0x0600074D RID: 1869 RVA: 0x000179E5 File Offset: 0x00015BE5
		internal UnaryExpression(ExpressionType nodeType, Expression expression, Type type, MethodInfo method)
		{
			this.Operand = expression;
			this.Method = method;
			this.NodeType = nodeType;
			this.Type = type;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.UnaryExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x00017A0A File Offset: 0x00015C0A
		public sealed override Type Type { get; }

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x00017A12 File Offset: 0x00015C12
		public sealed override ExpressionType NodeType { get; }

		/// <summary>Gets the operand of the unary operation.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the operand of the unary operation.</returns>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x00017A1A File Offset: 0x00015C1A
		public Expression Operand { get; }

		/// <summary>Gets the implementing method for the unary operation.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</returns>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x00017A22 File Offset: 0x00015C22
		public MethodInfo Method { get; }

		/// <summary>Gets a value that indicates whether the expression tree node represents a lifted call to an operator.</summary>
		/// <returns>true if the node represents a lifted call; otherwise, false.</returns>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x00017A2C File Offset: 0x00015C2C
		public bool IsLifted
		{
			get
			{
				if (this.NodeType == ExpressionType.TypeAs || this.NodeType == ExpressionType.Quote || this.NodeType == ExpressionType.Throw)
				{
					return false;
				}
				bool flag = this.Operand.Type.IsNullableType();
				bool flag2 = this.Type.IsNullableType();
				if (this.Method != null)
				{
					return (flag && !TypeUtils.AreEquivalent(this.Method.GetParametersCached()[0].ParameterType, this.Operand.Type)) || (flag2 && !TypeUtils.AreEquivalent(this.Method.ReturnType, this.Type));
				}
				return flag || flag2;
			}
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00017ACF File Offset: 0x00015CCF
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitUnary(this);
		}

		/// <summary>Gets a value that indicates whether the expression tree node can be reduced.</summary>
		/// <returns>True if a node can be reduced, otherwise false.</returns>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00017AD8 File Offset: 0x00015CD8
		public override bool CanReduce
		{
			get
			{
				ExpressionType nodeType = this.NodeType;
				return nodeType - ExpressionType.PreIncrementAssign <= 3;
			}
		}

		/// <summary>Reduces the expression node to a simpler expression. </summary>
		/// <returns>The reduced expression.</returns>
		// Token: 0x06000755 RID: 1877 RVA: 0x00017AF8 File Offset: 0x00015CF8
		public override Expression Reduce()
		{
			if (!this.CanReduce)
			{
				return this;
			}
			ExpressionType nodeType = this.Operand.NodeType;
			if (nodeType == ExpressionType.MemberAccess)
			{
				return this.ReduceMember();
			}
			if (nodeType == ExpressionType.Index)
			{
				return this.ReduceIndex();
			}
			return this.ReduceVariable();
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00017B39 File Offset: 0x00015D39
		private bool IsPrefix
		{
			get
			{
				return this.NodeType == ExpressionType.PreIncrementAssign || this.NodeType == ExpressionType.PreDecrementAssign;
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00017B54 File Offset: 0x00015D54
		private UnaryExpression FunctionalOp(Expression operand)
		{
			ExpressionType expressionType;
			if (this.NodeType == ExpressionType.PreIncrementAssign || this.NodeType == ExpressionType.PostIncrementAssign)
			{
				expressionType = ExpressionType.Increment;
			}
			else
			{
				expressionType = ExpressionType.Decrement;
			}
			return new UnaryExpression(expressionType, operand, operand.Type, this.Method);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00017B90 File Offset: 0x00015D90
		private Expression ReduceVariable()
		{
			if (this.IsPrefix)
			{
				return Expression.Assign(this.Operand, this.FunctionalOp(this.Operand));
			}
			ParameterExpression parameterExpression = Expression.Parameter(this.Operand.Type, null);
			return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression }), new TrueReadOnlyCollection<Expression>(new Expression[]
			{
				Expression.Assign(parameterExpression, this.Operand),
				Expression.Assign(this.Operand, this.FunctionalOp(parameterExpression)),
				parameterExpression
			}));
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00017C18 File Offset: 0x00015E18
		private Expression ReduceMember()
		{
			MemberExpression memberExpression = (MemberExpression)this.Operand;
			if (memberExpression.Expression == null)
			{
				return this.ReduceVariable();
			}
			ParameterExpression parameterExpression = Expression.Parameter(memberExpression.Expression.Type, null);
			BinaryExpression binaryExpression = Expression.Assign(parameterExpression, memberExpression.Expression);
			memberExpression = Expression.MakeMemberAccess(parameterExpression, memberExpression.Member);
			if (this.IsPrefix)
			{
				return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression }), new TrueReadOnlyCollection<Expression>(new Expression[]
				{
					binaryExpression,
					Expression.Assign(memberExpression, this.FunctionalOp(memberExpression))
				}));
			}
			ParameterExpression parameterExpression2 = Expression.Parameter(memberExpression.Type, null);
			return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression, parameterExpression2 }), new TrueReadOnlyCollection<Expression>(new Expression[]
			{
				binaryExpression,
				Expression.Assign(parameterExpression2, memberExpression),
				Expression.Assign(memberExpression, this.FunctionalOp(parameterExpression2)),
				parameterExpression2
			}));
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00017CFC File Offset: 0x00015EFC
		private Expression ReduceIndex()
		{
			bool isPrefix = this.IsPrefix;
			IndexExpression indexExpression = (IndexExpression)this.Operand;
			int argumentCount = indexExpression.ArgumentCount;
			Expression[] array = new Expression[argumentCount + (isPrefix ? 2 : 4)];
			ParameterExpression[] array2 = new ParameterExpression[argumentCount + (isPrefix ? 1 : 2)];
			ParameterExpression[] array3 = new ParameterExpression[argumentCount];
			int i = 0;
			array2[i] = Expression.Parameter(indexExpression.Object.Type, null);
			array[i] = Expression.Assign(array2[i], indexExpression.Object);
			for (i++; i <= argumentCount; i++)
			{
				Expression argument = indexExpression.GetArgument(i - 1);
				array3[i - 1] = (array2[i] = Expression.Parameter(argument.Type, null));
				array[i] = Expression.Assign(array2[i], argument);
			}
			Expression expression = array2[0];
			PropertyInfo indexer = indexExpression.Indexer;
			Expression[] array4 = array3;
			indexExpression = Expression.MakeIndex(expression, indexer, new TrueReadOnlyCollection<Expression>(array4));
			if (!isPrefix)
			{
				ParameterExpression parameterExpression = (array2[i] = Expression.Parameter(indexExpression.Type, null));
				array[i] = Expression.Assign(array2[i], indexExpression);
				i++;
				array[i++] = Expression.Assign(indexExpression, this.FunctionalOp(parameterExpression));
				array[i++] = parameterExpression;
			}
			else
			{
				array[i++] = Expression.Assign(indexExpression, this.FunctionalOp(indexExpression));
			}
			return Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(array2), new TrueReadOnlyCollection<Expression>(array));
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="operand">The <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property of the result.</param>
		// Token: 0x0600075B RID: 1883 RVA: 0x00017E5B File Offset: 0x0001605B
		public UnaryExpression Update(Expression operand)
		{
			if (operand == this.Operand)
			{
				return this;
			}
			return Expression.MakeUnary(this.NodeType, operand, this.Type, this.Method);
		}
	}
}
