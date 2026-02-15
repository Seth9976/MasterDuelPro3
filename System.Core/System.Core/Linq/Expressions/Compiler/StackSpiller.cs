using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020000FE RID: 254
	internal sealed class StackSpiller
	{
		// Token: 0x060008D2 RID: 2258 RVA: 0x00021FBC File Offset: 0x000201BC
		private StackSpiller.Result RewriteExpression(Expression node, StackSpiller.Stack stack)
		{
			if (node == null)
			{
				return new StackSpiller.Result(StackSpiller.RewriteAction.None, null);
			}
			if (!this._guard.TryEnterOnCurrentStack())
			{
				return this._guard.RunOnEmptyStack<StackSpiller, Expression, StackSpiller.Stack, StackSpiller.Result>((StackSpiller @this, Expression n, StackSpiller.Stack s) => @this.RewriteExpression(n, s), this, node, stack);
			}
			StackSpiller.Result result;
			switch (node.NodeType)
			{
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
			case ExpressionType.And:
			case ExpressionType.ArrayIndex:
			case ExpressionType.Divide:
			case ExpressionType.Equal:
			case ExpressionType.ExclusiveOr:
			case ExpressionType.GreaterThan:
			case ExpressionType.GreaterThanOrEqual:
			case ExpressionType.LeftShift:
			case ExpressionType.LessThan:
			case ExpressionType.LessThanOrEqual:
			case ExpressionType.Modulo:
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
			case ExpressionType.NotEqual:
			case ExpressionType.Or:
			case ExpressionType.Power:
			case ExpressionType.RightShift:
			case ExpressionType.Subtract:
			case ExpressionType.SubtractChecked:
				result = this.RewriteBinaryExpression(node, stack);
				break;
			case ExpressionType.AndAlso:
			case ExpressionType.Coalesce:
			case ExpressionType.OrElse:
				result = this.RewriteLogicalBinaryExpression(node, stack);
				break;
			case ExpressionType.ArrayLength:
			case ExpressionType.Convert:
			case ExpressionType.ConvertChecked:
			case ExpressionType.Negate:
			case ExpressionType.UnaryPlus:
			case ExpressionType.NegateChecked:
			case ExpressionType.Not:
			case ExpressionType.TypeAs:
			case ExpressionType.Decrement:
			case ExpressionType.Increment:
			case ExpressionType.Unbox:
			case ExpressionType.OnesComplement:
			case ExpressionType.IsTrue:
			case ExpressionType.IsFalse:
				result = this.RewriteUnaryExpression(node, stack);
				break;
			case ExpressionType.Call:
				result = this.RewriteMethodCallExpression(node, stack);
				break;
			case ExpressionType.Conditional:
				result = this.RewriteConditionalExpression(node, stack);
				break;
			case ExpressionType.Constant:
			case ExpressionType.Parameter:
			case ExpressionType.Quote:
			case ExpressionType.DebugInfo:
			case ExpressionType.Default:
			case ExpressionType.RuntimeVariables:
				result = new StackSpiller.Result(StackSpiller.RewriteAction.None, node);
				break;
			case ExpressionType.Invoke:
				result = this.RewriteInvocationExpression(node, stack);
				break;
			case ExpressionType.Lambda:
				result = StackSpiller.RewriteLambdaExpression(node);
				break;
			case ExpressionType.ListInit:
				result = this.RewriteListInitExpression(node, stack);
				break;
			case ExpressionType.MemberAccess:
				result = this.RewriteMemberExpression(node, stack);
				break;
			case ExpressionType.MemberInit:
				result = this.RewriteMemberInitExpression(node, stack);
				break;
			case ExpressionType.New:
				result = this.RewriteNewExpression(node, stack);
				break;
			case ExpressionType.NewArrayInit:
			case ExpressionType.NewArrayBounds:
				result = this.RewriteNewArrayExpression(node, stack);
				break;
			case ExpressionType.TypeIs:
			case ExpressionType.TypeEqual:
				result = this.RewriteTypeBinaryExpression(node, stack);
				break;
			case ExpressionType.Assign:
				result = this.RewriteAssignBinaryExpression(node, stack);
				break;
			case ExpressionType.Block:
				result = this.RewriteBlockExpression(node, stack);
				break;
			case ExpressionType.Dynamic:
				result = this.RewriteDynamicExpression(node);
				break;
			case ExpressionType.Extension:
				result = this.RewriteExtensionExpression(node, stack);
				break;
			case ExpressionType.Goto:
				result = this.RewriteGotoExpression(node, stack);
				break;
			case ExpressionType.Index:
				result = this.RewriteIndexExpression(node, stack);
				break;
			case ExpressionType.Label:
				result = this.RewriteLabelExpression(node, stack);
				break;
			case ExpressionType.Loop:
				result = this.RewriteLoopExpression(node, stack);
				break;
			case ExpressionType.Switch:
				result = this.RewriteSwitchExpression(node, stack);
				break;
			case ExpressionType.Throw:
				result = this.RewriteThrowUnaryExpression(node, stack);
				break;
			case ExpressionType.Try:
				result = this.RewriteTryExpression(node, stack);
				break;
			case ExpressionType.AddAssign:
			case ExpressionType.AndAssign:
			case ExpressionType.DivideAssign:
			case ExpressionType.ExclusiveOrAssign:
			case ExpressionType.LeftShiftAssign:
			case ExpressionType.ModuloAssign:
			case ExpressionType.MultiplyAssign:
			case ExpressionType.OrAssign:
			case ExpressionType.PowerAssign:
			case ExpressionType.RightShiftAssign:
			case ExpressionType.SubtractAssign:
			case ExpressionType.AddAssignChecked:
			case ExpressionType.MultiplyAssignChecked:
			case ExpressionType.SubtractAssignChecked:
			case ExpressionType.PreIncrementAssign:
			case ExpressionType.PreDecrementAssign:
			case ExpressionType.PostIncrementAssign:
			case ExpressionType.PostDecrementAssign:
				result = this.RewriteReducibleExpression(node, stack);
				break;
			default:
				result = this.RewriteExpression(node.ReduceAndCheck(), stack);
				if (result.Action == StackSpiller.RewriteAction.None)
				{
					result = new StackSpiller.Result(result.Action | StackSpiller.RewriteAction.Copy, result.Node);
				}
				break;
			}
			return result;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x000222F2 File Offset: 0x000204F2
		private static Expression MakeBlock(global::System.Collections.Generic.ArrayBuilder<Expression> expressions)
		{
			return new SpilledExpressionBlock(expressions.ToArray());
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00022300 File Offset: 0x00020500
		private static Expression MakeBlock(params Expression[] expressions)
		{
			return new SpilledExpressionBlock(expressions);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00022300 File Offset: 0x00020500
		private static Expression MakeBlock(IReadOnlyList<Expression> expressions)
		{
			return new SpilledExpressionBlock(expressions);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00022308 File Offset: 0x00020508
		private ParameterExpression MakeTemp(Type type)
		{
			return this._tm.Temp(type);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00022316 File Offset: 0x00020516
		private int Mark()
		{
			return this._tm.Mark();
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00022323 File Offset: 0x00020523
		private void Free(int mark)
		{
			this._tm.Free(mark);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00022334 File Offset: 0x00020534
		private ParameterExpression ToTemp(Expression expression, out Expression save, bool byRef)
		{
			Type type = (byRef ? expression.Type.MakeByRefType() : expression.Type);
			ParameterExpression parameterExpression = this.MakeTemp(type);
			save = AssignBinaryExpression.Make(parameterExpression, expression, byRef);
			return parameterExpression;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002236B File Offset: 0x0002056B
		internal static LambdaExpression AnalyzeLambda(LambdaExpression lambda)
		{
			return lambda.Accept(new StackSpiller(StackSpiller.Stack.Empty));
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00022379 File Offset: 0x00020579
		private StackSpiller(StackSpiller.Stack stack)
		{
			this._startingStack = stack;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x000223A0 File Offset: 0x000205A0
		internal Expression<T> Rewrite<T>(Expression<T> lambda)
		{
			StackSpiller.Result result = this.RewriteExpressionFreeTemps(lambda.Body, this._startingStack);
			this._lambdaRewrite = result.Action;
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				Expression expression = result.Node;
				if (this._tm.Temps.Count > 0)
				{
					expression = Expression.Block(this._tm.Temps, new TrueReadOnlyCollection<Expression>(new Expression[] { expression }));
				}
				return Expression<T>.Create(expression, lambda.Name, lambda.TailCall, new ParameterList(lambda));
			}
			return lambda;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00022428 File Offset: 0x00020628
		private StackSpiller.Result RewriteExpressionFreeTemps(Expression expression, StackSpiller.Stack stack)
		{
			int num = this.Mark();
			StackSpiller.Result result = this.RewriteExpression(expression, stack);
			this.Free(num);
			return result;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002244C File Offset: 0x0002064C
		private StackSpiller.Result RewriteDynamicExpression(Expression expr)
		{
			IDynamicExpression dynamicExpression = (IDynamicExpression)expr;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, StackSpiller.Stack.NonEmpty, dynamicExpression.ArgumentCount);
			childRewriter.AddArguments(dynamicExpression);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				StackSpiller.RequireNoRefArgs(dynamicExpression.DelegateType.GetInvokeMethod());
			}
			return childRewriter.Finish(childRewriter.Rewrite ? dynamicExpression.Rewrite(childRewriter[0, -1]) : expr);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000224B0 File Offset: 0x000206B0
		private StackSpiller.Result RewriteIndexAssignment(BinaryExpression node, StackSpiller.Stack stack)
		{
			IndexExpression indexExpression = (IndexExpression)node.Left;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, 2 + indexExpression.ArgumentCount);
			childRewriter.Add(indexExpression.Object);
			childRewriter.AddArguments(indexExpression);
			childRewriter.Add(node.Right);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				childRewriter.MarkRefInstance(indexExpression.Object);
			}
			if (childRewriter.Rewrite)
			{
				node = new AssignBinaryExpression(new IndexExpression(childRewriter[0], indexExpression.Indexer, childRewriter[1, -2]), childRewriter[-1]);
			}
			return childRewriter.Finish(node);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00022548 File Offset: 0x00020748
		private StackSpiller.Result RewriteLogicalBinaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			BinaryExpression binaryExpression = (BinaryExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(binaryExpression.Left, stack);
			StackSpiller.Result result2 = this.RewriteExpression(binaryExpression.Right, stack);
			StackSpiller.Result result3 = this.RewriteExpression(binaryExpression.Conversion, stack);
			StackSpiller.RewriteAction rewriteAction = result.Action | result2.Action | result3.Action;
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = BinaryExpression.Create(binaryExpression.NodeType, result.Node, result2.Node, binaryExpression.Type, binaryExpression.Method, (LambdaExpression)result3.Node);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x000225D4 File Offset: 0x000207D4
		private StackSpiller.Result RewriteReducibleExpression(Expression expr, StackSpiller.Stack stack)
		{
			StackSpiller.Result result = this.RewriteExpression(expr.Reduce(), stack);
			return new StackSpiller.Result(result.Action | StackSpiller.RewriteAction.Copy, result.Node);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00022604 File Offset: 0x00020804
		private StackSpiller.Result RewriteBinaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			BinaryExpression binaryExpression = (BinaryExpression)expr;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, 3);
			childRewriter.Add(binaryExpression.Left);
			childRewriter.Add(binaryExpression.Right);
			childRewriter.Add(binaryExpression.Conversion);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				StackSpiller.RequireNoRefArgs(binaryExpression.Method);
			}
			return childRewriter.Finish(childRewriter.Rewrite ? BinaryExpression.Create(binaryExpression.NodeType, childRewriter[0], childRewriter[1], binaryExpression.Type, binaryExpression.Method, (LambdaExpression)childRewriter[2]) : expr);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0002269C File Offset: 0x0002089C
		private StackSpiller.Result RewriteVariableAssignment(BinaryExpression node, StackSpiller.Stack stack)
		{
			StackSpiller.Result result = this.RewriteExpression(node.Right, stack);
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				node = new AssignBinaryExpression(node.Left, result.Node);
			}
			return new StackSpiller.Result(result.Action, node);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x000226E0 File Offset: 0x000208E0
		private StackSpiller.Result RewriteAssignBinaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			BinaryExpression binaryExpression = (BinaryExpression)expr;
			ExpressionType nodeType = binaryExpression.Left.NodeType;
			if (nodeType <= ExpressionType.Parameter)
			{
				if (nodeType == ExpressionType.MemberAccess)
				{
					return this.RewriteMemberAssignment(binaryExpression, stack);
				}
				if (nodeType == ExpressionType.Parameter)
				{
					return this.RewriteVariableAssignment(binaryExpression, stack);
				}
			}
			else
			{
				if (nodeType == ExpressionType.Extension)
				{
					return this.RewriteExtensionAssignment(binaryExpression, stack);
				}
				if (nodeType == ExpressionType.Index)
				{
					return this.RewriteIndexAssignment(binaryExpression, stack);
				}
			}
			throw Error.InvalidLvalue(binaryExpression.Left.NodeType);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00022750 File Offset: 0x00020950
		private StackSpiller.Result RewriteExtensionAssignment(BinaryExpression node, StackSpiller.Stack stack)
		{
			node = new AssignBinaryExpression(node.Left.ReduceExtensions(), node.Right);
			StackSpiller.Result result = this.RewriteAssignBinaryExpression(node, stack);
			return new StackSpiller.Result(result.Action | StackSpiller.RewriteAction.Copy, result.Node);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00022794 File Offset: 0x00020994
		private static StackSpiller.Result RewriteLambdaExpression(Expression expr)
		{
			LambdaExpression lambdaExpression = (LambdaExpression)expr;
			expr = StackSpiller.AnalyzeLambda(lambdaExpression);
			return new StackSpiller.Result((expr == lambdaExpression) ? StackSpiller.RewriteAction.None : StackSpiller.RewriteAction.Copy, expr);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x000227C0 File Offset: 0x000209C0
		private StackSpiller.Result RewriteConditionalExpression(Expression expr, StackSpiller.Stack stack)
		{
			ConditionalExpression conditionalExpression = (ConditionalExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(conditionalExpression.Test, stack);
			StackSpiller.Result result2 = this.RewriteExpression(conditionalExpression.IfTrue, stack);
			StackSpiller.Result result3 = this.RewriteExpression(conditionalExpression.IfFalse, stack);
			StackSpiller.RewriteAction rewriteAction = result.Action | result2.Action | result3.Action;
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = ConditionalExpression.Make(result.Node, result2.Node, result3.Node, conditionalExpression.Type);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0002283C File Offset: 0x00020A3C
		private StackSpiller.Result RewriteMemberAssignment(BinaryExpression node, StackSpiller.Stack stack)
		{
			MemberExpression memberExpression = (MemberExpression)node.Left;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, 2);
			childRewriter.Add(memberExpression.Expression);
			childRewriter.Add(node.Right);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				childRewriter.MarkRefInstance(memberExpression.Expression);
			}
			if (childRewriter.Rewrite)
			{
				return childRewriter.Finish(new AssignBinaryExpression(MemberExpression.Make(childRewriter[0], memberExpression.Member), childRewriter[1]));
			}
			return new StackSpiller.Result(StackSpiller.RewriteAction.None, node);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x000228C0 File Offset: 0x00020AC0
		private StackSpiller.Result RewriteMemberExpression(Expression expr, StackSpiller.Stack stack)
		{
			MemberExpression memberExpression = (MemberExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(memberExpression.Expression, stack);
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				if (result.Action == StackSpiller.RewriteAction.SpillStack && memberExpression.Member is PropertyInfo)
				{
					StackSpiller.RequireNotRefInstance(memberExpression.Expression);
				}
				expr = MemberExpression.Make(result.Node, memberExpression.Member);
			}
			return new StackSpiller.Result(result.Action, expr);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0002292C File Offset: 0x00020B2C
		private StackSpiller.Result RewriteIndexExpression(Expression expr, StackSpiller.Stack stack)
		{
			IndexExpression indexExpression = (IndexExpression)expr;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, indexExpression.ArgumentCount + 1);
			childRewriter.Add(indexExpression.Object);
			childRewriter.AddArguments(indexExpression);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				childRewriter.MarkRefInstance(indexExpression.Object);
			}
			if (childRewriter.Rewrite)
			{
				expr = new IndexExpression(childRewriter[0], indexExpression.Indexer, childRewriter[1, -1]);
			}
			return childRewriter.Finish(expr);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x000229A4 File Offset: 0x00020BA4
		private StackSpiller.Result RewriteMethodCallExpression(Expression expr, StackSpiller.Stack stack)
		{
			MethodCallExpression methodCallExpression = (MethodCallExpression)expr;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, methodCallExpression.ArgumentCount + 1);
			childRewriter.Add(methodCallExpression.Object);
			childRewriter.AddArguments(methodCallExpression);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				childRewriter.MarkRefInstance(methodCallExpression.Object);
				childRewriter.MarkRefArgs(methodCallExpression.Method, 1);
			}
			if (childRewriter.Rewrite)
			{
				if (methodCallExpression.Object != null)
				{
					expr = new InstanceMethodCallExpressionN(methodCallExpression.Method, childRewriter[0], childRewriter[1, -1]);
				}
				else
				{
					expr = new MethodCallExpressionN(methodCallExpression.Method, childRewriter[1, -1]);
				}
			}
			return childRewriter.Finish(expr);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00022A48 File Offset: 0x00020C48
		private StackSpiller.Result RewriteNewArrayExpression(Expression expr, StackSpiller.Stack stack)
		{
			NewArrayExpression newArrayExpression = (NewArrayExpression)expr;
			if (newArrayExpression.NodeType == ExpressionType.NewArrayInit)
			{
				stack = StackSpiller.Stack.NonEmpty;
			}
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, newArrayExpression.Expressions.Count);
			childRewriter.Add(newArrayExpression.Expressions);
			if (childRewriter.Rewrite)
			{
				expr = NewArrayExpression.Make(newArrayExpression.NodeType, newArrayExpression.Type, new TrueReadOnlyCollection<Expression>(childRewriter[0, -1]));
			}
			return childRewriter.Finish(expr);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00022AB8 File Offset: 0x00020CB8
		private StackSpiller.Result RewriteInvocationExpression(Expression expr, StackSpiller.Stack stack)
		{
			InvocationExpression invocationExpression = (InvocationExpression)expr;
			LambdaExpression lambdaExpression = invocationExpression.LambdaOperand;
			StackSpiller.ChildRewriter childRewriter;
			if (lambdaExpression != null)
			{
				childRewriter = new StackSpiller.ChildRewriter(this, stack, invocationExpression.ArgumentCount);
				childRewriter.AddArguments(invocationExpression);
				if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
				{
					childRewriter.MarkRefArgs(Expression.GetInvokeMethod(invocationExpression.Expression), 0);
				}
				StackSpiller stackSpiller = new StackSpiller(stack);
				lambdaExpression = lambdaExpression.Accept(stackSpiller);
				if (childRewriter.Rewrite || stackSpiller._lambdaRewrite != StackSpiller.RewriteAction.None)
				{
					invocationExpression = new InvocationExpressionN(lambdaExpression, childRewriter[0, -1], invocationExpression.Type);
				}
				StackSpiller.Result result = childRewriter.Finish(invocationExpression);
				return new StackSpiller.Result(result.Action | stackSpiller._lambdaRewrite, result.Node);
			}
			childRewriter = new StackSpiller.ChildRewriter(this, stack, invocationExpression.ArgumentCount + 1);
			childRewriter.Add(invocationExpression.Expression);
			childRewriter.AddArguments(invocationExpression);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				childRewriter.MarkRefArgs(Expression.GetInvokeMethod(invocationExpression.Expression), 1);
			}
			return childRewriter.Finish(childRewriter.Rewrite ? new InvocationExpressionN(childRewriter[0], childRewriter[1, -1], invocationExpression.Type) : expr);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00022BCC File Offset: 0x00020DCC
		private StackSpiller.Result RewriteNewExpression(Expression expr, StackSpiller.Stack stack)
		{
			NewExpression newExpression = (NewExpression)expr;
			StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, stack, newExpression.ArgumentCount);
			childRewriter.AddArguments(newExpression);
			if (childRewriter.Action == StackSpiller.RewriteAction.SpillStack)
			{
				childRewriter.MarkRefArgs(newExpression.Constructor, 0);
			}
			return childRewriter.Finish(childRewriter.Rewrite ? new NewExpression(newExpression.Constructor, childRewriter[0, -1], newExpression.Members) : expr);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00022C38 File Offset: 0x00020E38
		private StackSpiller.Result RewriteTypeBinaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			TypeBinaryExpression typeBinaryExpression = (TypeBinaryExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(typeBinaryExpression.Expression, stack);
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				expr = new TypeBinaryExpression(result.Node, typeBinaryExpression.TypeOperand, typeBinaryExpression.NodeType);
			}
			return new StackSpiller.Result(result.Action, expr);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00022C88 File Offset: 0x00020E88
		private StackSpiller.Result RewriteThrowUnaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			UnaryExpression unaryExpression = (UnaryExpression)expr;
			StackSpiller.Result result = this.RewriteExpressionFreeTemps(unaryExpression.Operand, StackSpiller.Stack.Empty);
			StackSpiller.RewriteAction rewriteAction = result.Action;
			if (stack != StackSpiller.Stack.Empty)
			{
				rewriteAction = StackSpiller.RewriteAction.SpillStack;
			}
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = new UnaryExpression(ExpressionType.Throw, result.Node, unaryExpression.Type, null);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00022CD8 File Offset: 0x00020ED8
		private StackSpiller.Result RewriteUnaryExpression(Expression expr, StackSpiller.Stack stack)
		{
			UnaryExpression unaryExpression = (UnaryExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(unaryExpression.Operand, stack);
			if (result.Action == StackSpiller.RewriteAction.SpillStack)
			{
				StackSpiller.RequireNoRefArgs(unaryExpression.Method);
			}
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				expr = new UnaryExpression(unaryExpression.NodeType, result.Node, unaryExpression.Type, unaryExpression.Method);
			}
			return new StackSpiller.Result(result.Action, expr);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00022D44 File Offset: 0x00020F44
		private StackSpiller.Result RewriteListInitExpression(Expression expr, StackSpiller.Stack stack)
		{
			ListInitExpression listInitExpression = (ListInitExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(listInitExpression.NewExpression, stack);
			Expression node = result.Node;
			StackSpiller.RewriteAction rewriteAction = result.Action;
			ReadOnlyCollection<ElementInit> initializers = listInitExpression.Initializers;
			int count = initializers.Count;
			StackSpiller.ChildRewriter[] array = new StackSpiller.ChildRewriter[count];
			for (int i = 0; i < count; i++)
			{
				ElementInit elementInit = initializers[i];
				StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(this, StackSpiller.Stack.NonEmpty, elementInit.Arguments.Count);
				childRewriter.Add(elementInit.Arguments);
				rewriteAction |= childRewriter.Action;
				array[i] = childRewriter;
			}
			switch (rewriteAction)
			{
			case StackSpiller.RewriteAction.None:
				goto IL_01EA;
			case StackSpiller.RewriteAction.Copy:
			{
				ElementInit[] array2 = new ElementInit[count];
				for (int j = 0; j < count; j++)
				{
					StackSpiller.ChildRewriter childRewriter2 = array[j];
					if (childRewriter2.Action == StackSpiller.RewriteAction.None)
					{
						array2[j] = initializers[j];
					}
					else
					{
						array2[j] = new ElementInit(initializers[j].AddMethod, new TrueReadOnlyCollection<Expression>(childRewriter2[0, -1]));
					}
				}
				expr = new ListInitExpression((NewExpression)node, new TrueReadOnlyCollection<ElementInit>(array2));
				goto IL_01EA;
			}
			case StackSpiller.RewriteAction.SpillStack:
			{
				bool flag = StackSpiller.IsRefInstance(listInitExpression.NewExpression);
				global::System.Collections.Generic.ArrayBuilder<Expression> arrayBuilder = new global::System.Collections.Generic.ArrayBuilder<Expression>(count + 2 + (flag ? 1 : 0));
				ParameterExpression parameterExpression = this.MakeTemp(node.Type);
				arrayBuilder.UncheckedAdd(new AssignBinaryExpression(parameterExpression, node));
				ParameterExpression parameterExpression2 = parameterExpression;
				if (flag)
				{
					parameterExpression2 = this.MakeTemp(parameterExpression.Type.MakeByRefType());
					arrayBuilder.UncheckedAdd(new ByRefAssignBinaryExpression(parameterExpression2, parameterExpression));
				}
				for (int k = 0; k < count; k++)
				{
					StackSpiller.ChildRewriter childRewriter3 = array[k];
					StackSpiller.Result result2 = childRewriter3.Finish(new InstanceMethodCallExpressionN(initializers[k].AddMethod, parameterExpression2, childRewriter3[0, -1]));
					arrayBuilder.UncheckedAdd(result2.Node);
				}
				arrayBuilder.UncheckedAdd(parameterExpression);
				expr = StackSpiller.MakeBlock(arrayBuilder);
				goto IL_01EA;
			}
			}
			throw ContractUtils.Unreachable;
			IL_01EA:
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00022F44 File Offset: 0x00021144
		private StackSpiller.Result RewriteMemberInitExpression(Expression expr, StackSpiller.Stack stack)
		{
			MemberInitExpression memberInitExpression = (MemberInitExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(memberInitExpression.NewExpression, stack);
			Expression node = result.Node;
			StackSpiller.RewriteAction rewriteAction = result.Action;
			ReadOnlyCollection<MemberBinding> bindings = memberInitExpression.Bindings;
			int count = bindings.Count;
			StackSpiller.BindingRewriter[] array = new StackSpiller.BindingRewriter[count];
			for (int i = 0; i < count; i++)
			{
				StackSpiller.BindingRewriter bindingRewriter = StackSpiller.BindingRewriter.Create(bindings[i], this, StackSpiller.Stack.NonEmpty);
				array[i] = bindingRewriter;
				rewriteAction |= bindingRewriter.Action;
			}
			switch (rewriteAction)
			{
			case StackSpiller.RewriteAction.None:
				goto IL_0175;
			case StackSpiller.RewriteAction.Copy:
			{
				MemberBinding[] array2 = new MemberBinding[count];
				for (int j = 0; j < count; j++)
				{
					array2[j] = array[j].AsBinding();
				}
				expr = new MemberInitExpression((NewExpression)node, new TrueReadOnlyCollection<MemberBinding>(array2));
				goto IL_0175;
			}
			case StackSpiller.RewriteAction.SpillStack:
			{
				bool flag = StackSpiller.IsRefInstance(memberInitExpression.NewExpression);
				global::System.Collections.Generic.ArrayBuilder<Expression> arrayBuilder = new global::System.Collections.Generic.ArrayBuilder<Expression>(count + 2 + (flag ? 1 : 0));
				ParameterExpression parameterExpression = this.MakeTemp(node.Type);
				arrayBuilder.UncheckedAdd(new AssignBinaryExpression(parameterExpression, node));
				ParameterExpression parameterExpression2 = parameterExpression;
				if (flag)
				{
					parameterExpression2 = this.MakeTemp(parameterExpression.Type.MakeByRefType());
					arrayBuilder.UncheckedAdd(new ByRefAssignBinaryExpression(parameterExpression2, parameterExpression));
				}
				for (int k = 0; k < count; k++)
				{
					Expression expression = array[k].AsExpression(parameterExpression2);
					arrayBuilder.UncheckedAdd(expression);
				}
				arrayBuilder.UncheckedAdd(parameterExpression);
				expr = StackSpiller.MakeBlock(arrayBuilder);
				goto IL_0175;
			}
			}
			throw ContractUtils.Unreachable;
			IL_0175:
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x000230D0 File Offset: 0x000212D0
		private StackSpiller.Result RewriteBlockExpression(Expression expr, StackSpiller.Stack stack)
		{
			BlockExpression blockExpression = (BlockExpression)expr;
			int expressionCount = blockExpression.ExpressionCount;
			StackSpiller.RewriteAction rewriteAction = StackSpiller.RewriteAction.None;
			Expression[] array = null;
			for (int i = 0; i < expressionCount; i++)
			{
				Expression expression = blockExpression.GetExpression(i);
				StackSpiller.Result result = this.RewriteExpression(expression, stack);
				rewriteAction |= result.Action;
				if (array == null && result.Action != StackSpiller.RewriteAction.None)
				{
					array = StackSpiller.Clone<Expression>(blockExpression.Expressions, i);
				}
				if (array != null)
				{
					array[i] = result.Node;
				}
			}
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = blockExpression.Rewrite(null, array);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0002315C File Offset: 0x0002135C
		private StackSpiller.Result RewriteLabelExpression(Expression expr, StackSpiller.Stack stack)
		{
			LabelExpression labelExpression = (LabelExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(labelExpression.DefaultValue, stack);
			if (result.Action != StackSpiller.RewriteAction.None)
			{
				expr = new LabelExpression(labelExpression.Target, result.Node);
			}
			return new StackSpiller.Result(result.Action, expr);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000231A8 File Offset: 0x000213A8
		private StackSpiller.Result RewriteLoopExpression(Expression expr, StackSpiller.Stack stack)
		{
			LoopExpression loopExpression = (LoopExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(loopExpression.Body, StackSpiller.Stack.Empty);
			StackSpiller.RewriteAction rewriteAction = result.Action;
			if (stack != StackSpiller.Stack.Empty)
			{
				rewriteAction = StackSpiller.RewriteAction.SpillStack;
			}
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = new LoopExpression(result.Node, loopExpression.BreakLabel, loopExpression.ContinueLabel);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x000231FC File Offset: 0x000213FC
		private StackSpiller.Result RewriteGotoExpression(Expression expr, StackSpiller.Stack stack)
		{
			GotoExpression gotoExpression = (GotoExpression)expr;
			StackSpiller.Result result = this.RewriteExpressionFreeTemps(gotoExpression.Value, StackSpiller.Stack.Empty);
			StackSpiller.RewriteAction rewriteAction = result.Action;
			if (stack != StackSpiller.Stack.Empty)
			{
				rewriteAction = StackSpiller.RewriteAction.SpillStack;
			}
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				expr = Expression.MakeGoto(gotoExpression.Kind, gotoExpression.Target, result.Node, gotoExpression.Type);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00023254 File Offset: 0x00021454
		private StackSpiller.Result RewriteSwitchExpression(Expression expr, StackSpiller.Stack stack)
		{
			SwitchExpression switchExpression = (SwitchExpression)expr;
			StackSpiller.Result result = this.RewriteExpressionFreeTemps(switchExpression.SwitchValue, stack);
			StackSpiller.RewriteAction rewriteAction = result.Action;
			ReadOnlyCollection<SwitchCase> readOnlyCollection = switchExpression.Cases;
			SwitchCase[] array = null;
			for (int i = 0; i < readOnlyCollection.Count; i++)
			{
				SwitchCase switchCase = readOnlyCollection[i];
				Expression[] array2 = null;
				ReadOnlyCollection<Expression> readOnlyCollection2 = switchCase.TestValues;
				for (int j = 0; j < readOnlyCollection2.Count; j++)
				{
					StackSpiller.Result result2 = this.RewriteExpression(readOnlyCollection2[j], stack);
					rewriteAction |= result2.Action;
					if (array2 == null && result2.Action != StackSpiller.RewriteAction.None)
					{
						array2 = StackSpiller.Clone<Expression>(readOnlyCollection2, j);
					}
					if (array2 != null)
					{
						array2[j] = result2.Node;
					}
				}
				StackSpiller.Result result3 = this.RewriteExpression(switchCase.Body, stack);
				rewriteAction |= result3.Action;
				if (result3.Action != StackSpiller.RewriteAction.None || array2 != null)
				{
					if (array2 != null)
					{
						readOnlyCollection2 = new ReadOnlyCollection<Expression>(array2);
					}
					switchCase = new SwitchCase(result3.Node, readOnlyCollection2);
					if (array == null)
					{
						array = StackSpiller.Clone<SwitchCase>(readOnlyCollection, i);
					}
				}
				if (array != null)
				{
					array[i] = switchCase;
				}
			}
			StackSpiller.Result result4 = this.RewriteExpression(switchExpression.DefaultBody, stack);
			rewriteAction |= result4.Action;
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				if (array != null)
				{
					readOnlyCollection = new ReadOnlyCollection<SwitchCase>(array);
				}
				expr = new SwitchExpression(switchExpression.Type, result.Node, result4.Node, switchExpression.Comparison, readOnlyCollection);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x000233C0 File Offset: 0x000215C0
		private StackSpiller.Result RewriteTryExpression(Expression expr, StackSpiller.Stack stack)
		{
			TryExpression tryExpression = (TryExpression)expr;
			StackSpiller.Result result = this.RewriteExpression(tryExpression.Body, StackSpiller.Stack.Empty);
			ReadOnlyCollection<CatchBlock> readOnlyCollection = tryExpression.Handlers;
			CatchBlock[] array = null;
			StackSpiller.RewriteAction rewriteAction = result.Action;
			if (readOnlyCollection != null)
			{
				for (int i = 0; i < readOnlyCollection.Count; i++)
				{
					StackSpiller.RewriteAction rewriteAction2 = result.Action;
					CatchBlock catchBlock = readOnlyCollection[i];
					Expression expression = catchBlock.Filter;
					if (catchBlock.Filter != null)
					{
						StackSpiller.Result result2 = this.RewriteExpression(catchBlock.Filter, StackSpiller.Stack.Empty);
						rewriteAction |= result2.Action;
						rewriteAction2 |= result2.Action;
						expression = result2.Node;
					}
					StackSpiller.Result result3 = this.RewriteExpression(catchBlock.Body, StackSpiller.Stack.Empty);
					rewriteAction |= result3.Action;
					rewriteAction2 |= result3.Action;
					if (rewriteAction2 != StackSpiller.RewriteAction.None)
					{
						catchBlock = Expression.MakeCatchBlock(catchBlock.Test, catchBlock.Variable, result3.Node, expression);
						if (array == null)
						{
							array = StackSpiller.Clone<CatchBlock>(readOnlyCollection, i);
						}
					}
					if (array != null)
					{
						array[i] = catchBlock;
					}
				}
			}
			StackSpiller.Result result4 = this.RewriteExpression(tryExpression.Fault, StackSpiller.Stack.Empty);
			rewriteAction |= result4.Action;
			StackSpiller.Result result5 = this.RewriteExpression(tryExpression.Finally, StackSpiller.Stack.Empty);
			rewriteAction |= result5.Action;
			if (stack != StackSpiller.Stack.Empty)
			{
				rewriteAction = StackSpiller.RewriteAction.SpillStack;
			}
			if (rewriteAction != StackSpiller.RewriteAction.None)
			{
				if (array != null)
				{
					readOnlyCollection = new ReadOnlyCollection<CatchBlock>(array);
				}
				expr = new TryExpression(tryExpression.Type, result.Node, result5.Node, result4.Node, readOnlyCollection);
			}
			return new StackSpiller.Result(rewriteAction, expr);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0002353C File Offset: 0x0002173C
		private StackSpiller.Result RewriteExtensionExpression(Expression expr, StackSpiller.Stack stack)
		{
			StackSpiller.Result result = this.RewriteExpression(expr.ReduceExtensions(), stack);
			return new StackSpiller.Result(result.Action | StackSpiller.RewriteAction.Copy, result.Node);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002356C File Offset: 0x0002176C
		private static T[] Clone<T>(ReadOnlyCollection<T> original, int max)
		{
			T[] array = new T[original.Count];
			for (int i = 0; i < max; i++)
			{
				array[i] = original[i];
			}
			return array;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x000235A0 File Offset: 0x000217A0
		private static void RequireNoRefArgs(MethodBase method)
		{
			if (method != null)
			{
				if (method.GetParametersCached().Any((ParameterInfo p) => p.ParameterType.IsByRef))
				{
					throw Error.TryNotSupportedForMethodsWithRefArgs(method);
				}
			}
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x000235DE File Offset: 0x000217DE
		private static void RequireNotRefInstance(Expression instance)
		{
			if (StackSpiller.IsRefInstance(instance))
			{
				throw Error.TryNotSupportedForValueTypeInstances(instance.Type);
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x000235F4 File Offset: 0x000217F4
		private static bool IsRefInstance(Expression instance)
		{
			return instance != null && instance.Type.IsValueType && instance.Type.GetTypeCode() == TypeCode.Object;
		}

		// Token: 0x040002AE RID: 686
		private readonly StackGuard _guard = new StackGuard();

		// Token: 0x040002AF RID: 687
		private readonly StackSpiller.TempMaker _tm = new StackSpiller.TempMaker();

		// Token: 0x040002B0 RID: 688
		private readonly StackSpiller.Stack _startingStack;

		// Token: 0x040002B1 RID: 689
		private StackSpiller.RewriteAction _lambdaRewrite;

		// Token: 0x020000FF RID: 255
		private abstract class BindingRewriter
		{
			// Token: 0x060008FF RID: 2303 RVA: 0x00023616 File Offset: 0x00021816
			internal BindingRewriter(MemberBinding binding, StackSpiller spiller)
			{
				this._binding = binding;
				this._spiller = spiller;
			}

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x06000900 RID: 2304 RVA: 0x0002362C File Offset: 0x0002182C
			internal StackSpiller.RewriteAction Action
			{
				get
				{
					return this._action;
				}
			}

			// Token: 0x06000901 RID: 2305
			internal abstract MemberBinding AsBinding();

			// Token: 0x06000902 RID: 2306
			internal abstract Expression AsExpression(Expression target);

			// Token: 0x06000903 RID: 2307 RVA: 0x00023634 File Offset: 0x00021834
			internal static StackSpiller.BindingRewriter Create(MemberBinding binding, StackSpiller spiller, StackSpiller.Stack stack)
			{
				switch (binding.BindingType)
				{
				case MemberBindingType.Assignment:
					return new StackSpiller.MemberAssignmentRewriter((MemberAssignment)binding, spiller, stack);
				case MemberBindingType.MemberBinding:
					return new StackSpiller.MemberMemberBindingRewriter((MemberMemberBinding)binding, spiller, stack);
				case MemberBindingType.ListBinding:
					return new StackSpiller.ListBindingRewriter((MemberListBinding)binding, spiller, stack);
				default:
					throw Error.UnhandledBinding();
				}
			}

			// Token: 0x06000904 RID: 2308 RVA: 0x0002368C File Offset: 0x0002188C
			protected void RequireNoValueProperty()
			{
				PropertyInfo propertyInfo = this._binding.Member as PropertyInfo;
				if (propertyInfo != null && propertyInfo.PropertyType.IsValueType)
				{
					throw Error.CannotAutoInitializeValueTypeMemberThroughProperty(propertyInfo);
				}
			}

			// Token: 0x040002B2 RID: 690
			protected readonly MemberBinding _binding;

			// Token: 0x040002B3 RID: 691
			protected readonly StackSpiller _spiller;

			// Token: 0x040002B4 RID: 692
			protected StackSpiller.RewriteAction _action;
		}

		// Token: 0x02000100 RID: 256
		private sealed class MemberMemberBindingRewriter : StackSpiller.BindingRewriter
		{
			// Token: 0x06000905 RID: 2309 RVA: 0x000236C8 File Offset: 0x000218C8
			internal MemberMemberBindingRewriter(MemberMemberBinding binding, StackSpiller spiller, StackSpiller.Stack stack)
				: base(binding, spiller)
			{
				this._bindings = binding.Bindings;
				int count = this._bindings.Count;
				this._bindingRewriters = new StackSpiller.BindingRewriter[count];
				for (int i = 0; i < count; i++)
				{
					StackSpiller.BindingRewriter bindingRewriter = StackSpiller.BindingRewriter.Create(this._bindings[i], spiller, stack);
					this._action |= bindingRewriter.Action;
					this._bindingRewriters[i] = bindingRewriter;
				}
			}

			// Token: 0x06000906 RID: 2310 RVA: 0x00023740 File Offset: 0x00021940
			internal override MemberBinding AsBinding()
			{
				StackSpiller.RewriteAction action = this._action;
				if (action == StackSpiller.RewriteAction.None)
				{
					return this._binding;
				}
				if (action != StackSpiller.RewriteAction.Copy)
				{
					throw ContractUtils.Unreachable;
				}
				int count = this._bindings.Count;
				MemberBinding[] array = new MemberBinding[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = this._bindingRewriters[i].AsBinding();
				}
				return new MemberMemberBinding(this._binding.Member, new TrueReadOnlyCollection<MemberBinding>(array));
			}

			// Token: 0x06000907 RID: 2311 RVA: 0x000237B0 File Offset: 0x000219B0
			internal override Expression AsExpression(Expression target)
			{
				base.RequireNoValueProperty();
				Expression expression = MemberExpression.Make(target, this._binding.Member);
				Expression expression2 = this._spiller.MakeTemp(expression.Type);
				int count = this._bindings.Count;
				Expression[] array = new Expression[count + 2];
				array[0] = new AssignBinaryExpression(expression2, expression);
				for (int i = 0; i < count; i++)
				{
					StackSpiller.BindingRewriter bindingRewriter = this._bindingRewriters[i];
					array[i + 1] = bindingRewriter.AsExpression(expression2);
				}
				if (expression2.Type.IsValueType)
				{
					array[count + 1] = Expression.Block(typeof(void), new Expression[]
					{
						new AssignBinaryExpression(MemberExpression.Make(target, this._binding.Member), expression2)
					});
				}
				else
				{
					array[count + 1] = Utils.Empty;
				}
				return StackSpiller.MakeBlock(array);
			}

			// Token: 0x040002B5 RID: 693
			private readonly ReadOnlyCollection<MemberBinding> _bindings;

			// Token: 0x040002B6 RID: 694
			private readonly StackSpiller.BindingRewriter[] _bindingRewriters;
		}

		// Token: 0x02000101 RID: 257
		private sealed class ListBindingRewriter : StackSpiller.BindingRewriter
		{
			// Token: 0x06000908 RID: 2312 RVA: 0x00023884 File Offset: 0x00021A84
			internal ListBindingRewriter(MemberListBinding binding, StackSpiller spiller, StackSpiller.Stack stack)
				: base(binding, spiller)
			{
				this._inits = binding.Initializers;
				int count = this._inits.Count;
				this._childRewriters = new StackSpiller.ChildRewriter[count];
				for (int i = 0; i < count; i++)
				{
					ElementInit elementInit = this._inits[i];
					StackSpiller.ChildRewriter childRewriter = new StackSpiller.ChildRewriter(spiller, stack, elementInit.Arguments.Count);
					childRewriter.Add(elementInit.Arguments);
					this._action |= childRewriter.Action;
					this._childRewriters[i] = childRewriter;
				}
			}

			// Token: 0x06000909 RID: 2313 RVA: 0x00023914 File Offset: 0x00021B14
			internal override MemberBinding AsBinding()
			{
				StackSpiller.RewriteAction action = this._action;
				if (action == StackSpiller.RewriteAction.None)
				{
					return this._binding;
				}
				if (action != StackSpiller.RewriteAction.Copy)
				{
					throw ContractUtils.Unreachable;
				}
				int count = this._inits.Count;
				ElementInit[] array = new ElementInit[count];
				for (int i = 0; i < count; i++)
				{
					StackSpiller.ChildRewriter childRewriter = this._childRewriters[i];
					if (childRewriter.Action == StackSpiller.RewriteAction.None)
					{
						array[i] = this._inits[i];
					}
					else
					{
						array[i] = new ElementInit(this._inits[i].AddMethod, new TrueReadOnlyCollection<Expression>(childRewriter[0, -1]));
					}
				}
				return new MemberListBinding(this._binding.Member, new TrueReadOnlyCollection<ElementInit>(array));
			}

			// Token: 0x0600090A RID: 2314 RVA: 0x000239C4 File Offset: 0x00021BC4
			internal override Expression AsExpression(Expression target)
			{
				base.RequireNoValueProperty();
				Expression expression = MemberExpression.Make(target, this._binding.Member);
				Expression expression2 = this._spiller.MakeTemp(expression.Type);
				int count = this._inits.Count;
				Expression[] array = new Expression[count + 2];
				array[0] = new AssignBinaryExpression(expression2, expression);
				for (int i = 0; i < count; i++)
				{
					StackSpiller.ChildRewriter childRewriter = this._childRewriters[i];
					StackSpiller.Result result = childRewriter.Finish(new InstanceMethodCallExpressionN(this._inits[i].AddMethod, expression2, childRewriter[0, -1]));
					array[i + 1] = result.Node;
				}
				if (expression2.Type.IsValueType)
				{
					array[count + 1] = Expression.Block(typeof(void), new Expression[]
					{
						new AssignBinaryExpression(MemberExpression.Make(target, this._binding.Member), expression2)
					});
				}
				else
				{
					array[count + 1] = Utils.Empty;
				}
				return StackSpiller.MakeBlock(array);
			}

			// Token: 0x040002B7 RID: 695
			private readonly ReadOnlyCollection<ElementInit> _inits;

			// Token: 0x040002B8 RID: 696
			private readonly StackSpiller.ChildRewriter[] _childRewriters;
		}

		// Token: 0x02000102 RID: 258
		private sealed class MemberAssignmentRewriter : StackSpiller.BindingRewriter
		{
			// Token: 0x0600090B RID: 2315 RVA: 0x00023AC0 File Offset: 0x00021CC0
			internal MemberAssignmentRewriter(MemberAssignment binding, StackSpiller spiller, StackSpiller.Stack stack)
				: base(binding, spiller)
			{
				StackSpiller.Result result = spiller.RewriteExpression(binding.Expression, stack);
				this._action = result.Action;
				this._rhs = result.Node;
			}

			// Token: 0x0600090C RID: 2316 RVA: 0x00023AFC File Offset: 0x00021CFC
			internal override MemberBinding AsBinding()
			{
				StackSpiller.RewriteAction action = this._action;
				if (action == StackSpiller.RewriteAction.None)
				{
					return this._binding;
				}
				if (action != StackSpiller.RewriteAction.Copy)
				{
					throw ContractUtils.Unreachable;
				}
				return new MemberAssignment(this._binding.Member, this._rhs);
			}

			// Token: 0x0600090D RID: 2317 RVA: 0x00023B3C File Offset: 0x00021D3C
			internal override Expression AsExpression(Expression target)
			{
				Expression expression = MemberExpression.Make(target, this._binding.Member);
				Expression expression2 = this._spiller.MakeTemp(expression.Type);
				return StackSpiller.MakeBlock(new Expression[]
				{
					new AssignBinaryExpression(expression2, this._rhs),
					new AssignBinaryExpression(expression, expression2),
					Utils.Empty
				});
			}

			// Token: 0x040002B9 RID: 697
			private readonly Expression _rhs;
		}

		// Token: 0x02000103 RID: 259
		private sealed class ChildRewriter
		{
			// Token: 0x0600090E RID: 2318 RVA: 0x00023B99 File Offset: 0x00021D99
			internal ChildRewriter(StackSpiller self, StackSpiller.Stack stack, int count)
			{
				this._self = self;
				this._stack = stack;
				this._expressions = new Expression[count];
			}

			// Token: 0x0600090F RID: 2319 RVA: 0x00023BBC File Offset: 0x00021DBC
			internal void Add(Expression expression)
			{
				int num;
				if (expression == null)
				{
					Expression[] expressions = this._expressions;
					num = this._expressionsCount;
					this._expressionsCount = num + 1;
					expressions[num] = null;
					return;
				}
				StackSpiller.Result result = this._self.RewriteExpression(expression, this._stack);
				this._action |= result.Action;
				this._stack = StackSpiller.Stack.NonEmpty;
				if (result.Action == StackSpiller.RewriteAction.SpillStack)
				{
					this._lastSpillIndex = this._expressionsCount;
				}
				Expression[] expressions2 = this._expressions;
				num = this._expressionsCount;
				this._expressionsCount = num + 1;
				expressions2[num] = result.Node;
			}

			// Token: 0x06000910 RID: 2320 RVA: 0x00023C48 File Offset: 0x00021E48
			internal void Add(ReadOnlyCollection<Expression> expressions)
			{
				int i = 0;
				int count = expressions.Count;
				while (i < count)
				{
					this.Add(expressions[i]);
					i++;
				}
			}

			// Token: 0x06000911 RID: 2321 RVA: 0x00023C78 File Offset: 0x00021E78
			internal void AddArguments(IArgumentProvider expressions)
			{
				int i = 0;
				int argumentCount = expressions.ArgumentCount;
				while (i < argumentCount)
				{
					this.Add(expressions.GetArgument(i));
					i++;
				}
			}

			// Token: 0x06000912 RID: 2322 RVA: 0x00023CA8 File Offset: 0x00021EA8
			private void EnsureDone()
			{
				if (!this._done)
				{
					this._done = true;
					if (this._action == StackSpiller.RewriteAction.SpillStack)
					{
						Expression[] expressions = this._expressions;
						int num = this._lastSpillIndex + 1;
						List<Expression> list = new List<Expression>(num + 1);
						for (int i = 0; i < num; i++)
						{
							Expression expression = expressions[i];
							if (StackSpiller.ChildRewriter.ShouldSaveToTemp(expression))
							{
								Expression[] array = expressions;
								int num2 = i;
								StackSpiller self = this._self;
								Expression expression2 = expression;
								bool[] byRefs = this._byRefs;
								Expression expression3;
								array[num2] = self.ToTemp(expression2, out expression3, byRefs != null && byRefs[i]);
								list.Add(expression3);
							}
						}
						list.Capacity = list.Count + 1;
						this._comma = list;
					}
				}
			}

			// Token: 0x06000913 RID: 2323 RVA: 0x00023D44 File Offset: 0x00021F44
			private static bool ShouldSaveToTemp(Expression expression)
			{
				if (expression == null)
				{
					return false;
				}
				ExpressionType nodeType = expression.NodeType;
				if (nodeType <= ExpressionType.MemberAccess)
				{
					if (nodeType != ExpressionType.Constant)
					{
						if (nodeType != ExpressionType.MemberAccess)
						{
							return true;
						}
						FieldInfo fieldInfo = ((MemberExpression)expression).Member as FieldInfo;
						if (!(fieldInfo != null))
						{
							return true;
						}
						if (fieldInfo.IsLiteral)
						{
							return false;
						}
						if (fieldInfo.IsInitOnly && fieldInfo.IsStatic)
						{
							return false;
						}
						return true;
					}
				}
				else if (nodeType != ExpressionType.Default)
				{
					if (nodeType != ExpressionType.RuntimeVariables)
					{
						return true;
					}
					return false;
				}
				return false;
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x06000914 RID: 2324 RVA: 0x00023DB5 File Offset: 0x00021FB5
			internal bool Rewrite
			{
				get
				{
					return this._action > StackSpiller.RewriteAction.None;
				}
			}

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x06000915 RID: 2325 RVA: 0x00023DC0 File Offset: 0x00021FC0
			internal StackSpiller.RewriteAction Action
			{
				get
				{
					return this._action;
				}
			}

			// Token: 0x06000916 RID: 2326 RVA: 0x00023DC8 File Offset: 0x00021FC8
			internal void MarkRefInstance(Expression expr)
			{
				if (StackSpiller.IsRefInstance(expr))
				{
					this.MarkRef(0);
				}
			}

			// Token: 0x06000917 RID: 2327 RVA: 0x00023DDC File Offset: 0x00021FDC
			internal void MarkRefArgs(MethodBase method, int startIndex)
			{
				ParameterInfo[] parametersCached = method.GetParametersCached();
				int i = 0;
				int num = parametersCached.Length;
				while (i < num)
				{
					if (parametersCached[i].ParameterType.IsByRef)
					{
						this.MarkRef(startIndex + i);
					}
					i++;
				}
			}

			// Token: 0x06000918 RID: 2328 RVA: 0x00023E18 File Offset: 0x00022018
			private void MarkRef(int index)
			{
				if (this._byRefs == null)
				{
					this._byRefs = new bool[this._expressions.Length];
				}
				this._byRefs[index] = true;
			}

			// Token: 0x06000919 RID: 2329 RVA: 0x00023E3E File Offset: 0x0002203E
			internal StackSpiller.Result Finish(Expression expression)
			{
				this.EnsureDone();
				if (this._action == StackSpiller.RewriteAction.SpillStack)
				{
					this._comma.Add(expression);
					expression = StackSpiller.MakeBlock(this._comma);
				}
				return new StackSpiller.Result(this._action, expression);
			}

			// Token: 0x1700018B RID: 395
			internal Expression this[int index]
			{
				get
				{
					this.EnsureDone();
					if (index < 0)
					{
						index += this._expressions.Length;
					}
					return this._expressions[index];
				}
			}

			// Token: 0x1700018C RID: 396
			internal Expression[] this[int first, int last]
			{
				get
				{
					this.EnsureDone();
					if (last < 0)
					{
						last += this._expressions.Length;
					}
					int num = last - first + 1;
					ContractUtils.RequiresArrayRange<Expression>(this._expressions, first, num, "first", "last");
					if (num == this._expressions.Length)
					{
						return this._expressions;
					}
					Expression[] array = new Expression[num];
					Array.Copy(this._expressions, first, array, 0, num);
					return array;
				}
			}

			// Token: 0x040002BA RID: 698
			private readonly StackSpiller _self;

			// Token: 0x040002BB RID: 699
			private readonly Expression[] _expressions;

			// Token: 0x040002BC RID: 700
			private int _expressionsCount;

			// Token: 0x040002BD RID: 701
			private int _lastSpillIndex;

			// Token: 0x040002BE RID: 702
			private List<Expression> _comma;

			// Token: 0x040002BF RID: 703
			private StackSpiller.RewriteAction _action;

			// Token: 0x040002C0 RID: 704
			private StackSpiller.Stack _stack;

			// Token: 0x040002C1 RID: 705
			private bool _done;

			// Token: 0x040002C2 RID: 706
			private bool[] _byRefs;
		}

		// Token: 0x02000104 RID: 260
		private sealed class TempMaker
		{
			// Token: 0x1700018D RID: 397
			// (get) Token: 0x0600091C RID: 2332 RVA: 0x00023EFD File Offset: 0x000220FD
			internal List<ParameterExpression> Temps { get; } = new List<ParameterExpression>();

			// Token: 0x0600091D RID: 2333 RVA: 0x00023F08 File Offset: 0x00022108
			internal ParameterExpression Temp(Type type)
			{
				ParameterExpression parameterExpression;
				if (this._freeTemps != null)
				{
					for (int i = this._freeTemps.Count - 1; i >= 0; i--)
					{
						parameterExpression = this._freeTemps[i];
						if (parameterExpression.Type == type)
						{
							this._freeTemps.RemoveAt(i);
							return this.UseTemp(parameterExpression);
						}
					}
				}
				string text = "$temp$";
				int temp = this._temp;
				this._temp = temp + 1;
				parameterExpression = ParameterExpression.Make(type, text + temp.ToString(), false);
				this.Temps.Add(parameterExpression);
				return this.UseTemp(parameterExpression);
			}

			// Token: 0x0600091E RID: 2334 RVA: 0x00023FA0 File Offset: 0x000221A0
			private ParameterExpression UseTemp(ParameterExpression temp)
			{
				if (this._usedTemps == null)
				{
					this._usedTemps = new Stack<ParameterExpression>();
				}
				this._usedTemps.Push(temp);
				return temp;
			}

			// Token: 0x0600091F RID: 2335 RVA: 0x00023FC2 File Offset: 0x000221C2
			private void FreeTemp(ParameterExpression temp)
			{
				if (this._freeTemps == null)
				{
					this._freeTemps = new List<ParameterExpression>();
				}
				this._freeTemps.Add(temp);
			}

			// Token: 0x06000920 RID: 2336 RVA: 0x00023FE3 File Offset: 0x000221E3
			internal int Mark()
			{
				Stack<ParameterExpression> usedTemps = this._usedTemps;
				if (usedTemps == null)
				{
					return 0;
				}
				return usedTemps.Count;
			}

			// Token: 0x06000921 RID: 2337 RVA: 0x00023FF6 File Offset: 0x000221F6
			internal void Free(int mark)
			{
				if (this._usedTemps != null)
				{
					while (mark < this._usedTemps.Count)
					{
						this.FreeTemp(this._usedTemps.Pop());
					}
				}
			}

			// Token: 0x040002C3 RID: 707
			private int _temp;

			// Token: 0x040002C4 RID: 708
			private List<ParameterExpression> _freeTemps;

			// Token: 0x040002C5 RID: 709
			private Stack<ParameterExpression> _usedTemps;
		}

		// Token: 0x02000105 RID: 261
		private enum Stack
		{
			// Token: 0x040002C8 RID: 712
			Empty,
			// Token: 0x040002C9 RID: 713
			NonEmpty
		}

		// Token: 0x02000106 RID: 262
		[Flags]
		private enum RewriteAction
		{
			// Token: 0x040002CB RID: 715
			None = 0,
			// Token: 0x040002CC RID: 716
			Copy = 1,
			// Token: 0x040002CD RID: 717
			SpillStack = 3
		}

		// Token: 0x02000107 RID: 263
		private readonly struct Result
		{
			// Token: 0x06000923 RID: 2339 RVA: 0x00024034 File Offset: 0x00022234
			internal Result(StackSpiller.RewriteAction action, Expression node)
			{
				this.Action = action;
				this.Node = node;
			}

			// Token: 0x040002CE RID: 718
			internal readonly StackSpiller.RewriteAction Action;

			// Token: 0x040002CF RID: 719
			internal readonly Expression Node;
		}
	}
}
