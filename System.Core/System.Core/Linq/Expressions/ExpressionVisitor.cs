using System;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	/// <summary>Represents a visitor or rewriter for expression trees.</summary>
	// Token: 0x02000098 RID: 152
	public abstract class ExpressionVisitor
	{
		/// <summary>Dispatches the expression to one of the more specialized visit methods in this class.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000539 RID: 1337 RVA: 0x00014E96 File Offset: 0x00013096
		public virtual Expression Visit(Expression node)
		{
			if (node == null)
			{
				return null;
			}
			return node.Accept(this);
		}

		/// <summary>Dispatches the list of expressions to one of the more specialized visit methods in this class.</summary>
		/// <returns>The modified expression list, if any one of the elements were modified; otherwise, returns the original expression list.</returns>
		/// <param name="nodes">The expressions to visit.</param>
		// Token: 0x0600053A RID: 1338 RVA: 0x00014EA4 File Offset: 0x000130A4
		public ReadOnlyCollection<Expression> Visit(ReadOnlyCollection<Expression> nodes)
		{
			ContractUtils.RequiresNotNull(nodes, "nodes");
			Expression[] array = null;
			int i = 0;
			int count = nodes.Count;
			while (i < count)
			{
				Expression expression = this.Visit(nodes[i]);
				if (array != null)
				{
					array[i] = expression;
				}
				else if (expression != nodes[i])
				{
					array = new Expression[count];
					for (int j = 0; j < i; j++)
					{
						array[j] = nodes[j];
					}
					array[i] = expression;
				}
				i++;
			}
			if (array == null)
			{
				return nodes;
			}
			return new TrueReadOnlyCollection<Expression>(array);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00014F24 File Offset: 0x00013124
		private Expression[] VisitArguments(IArgumentProvider nodes)
		{
			return ExpressionVisitorUtils.VisitArguments(this, nodes);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00014F2D File Offset: 0x0001312D
		private ParameterExpression[] VisitParameters(IParameterProvider nodes, string callerName)
		{
			return ExpressionVisitorUtils.VisitParameters(this, nodes, callerName);
		}

		/// <summary>Visits all nodes in the collection using a specified element visitor.</summary>
		/// <returns>The modified node list, if any of the elements were modified; otherwise, returns the original node list.</returns>
		/// <param name="nodes">The nodes to visit.</param>
		/// <param name="elementVisitor">A delegate that visits a single element, optionally replacing it with a new element.</param>
		/// <typeparam name="T">The type of the nodes.</typeparam>
		// Token: 0x0600053D RID: 1341 RVA: 0x00014F38 File Offset: 0x00013138
		public static ReadOnlyCollection<T> Visit<T>(ReadOnlyCollection<T> nodes, Func<T, T> elementVisitor)
		{
			ContractUtils.RequiresNotNull(nodes, "nodes");
			ContractUtils.RequiresNotNull(elementVisitor, "elementVisitor");
			T[] array = null;
			int i = 0;
			int count = nodes.Count;
			while (i < count)
			{
				T t = elementVisitor(nodes[i]);
				if (array != null)
				{
					array[i] = t;
				}
				else if (t != nodes[i])
				{
					array = new T[count];
					for (int j = 0; j < i; j++)
					{
						array[j] = nodes[j];
					}
					array[i] = t;
				}
				i++;
			}
			if (array == null)
			{
				return nodes;
			}
			return new TrueReadOnlyCollection<T>(array);
		}

		/// <summary>Visits an expression, casting the result back to the original expression type.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		/// <param name="callerName">The name of the calling method; used to report to report a better error message.</param>
		/// <typeparam name="T">The type of the expression.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The visit method for this node returned a different type.</exception>
		// Token: 0x0600053E RID: 1342 RVA: 0x00014FDC File Offset: 0x000131DC
		public T VisitAndConvert<T>(T node, string callerName) where T : Expression
		{
			if (node == null)
			{
				return default(T);
			}
			node = this.Visit(node) as T;
			if (node == null)
			{
				throw Error.MustRewriteToSameNode(callerName, typeof(T), callerName);
			}
			return node;
		}

		/// <summary>Visits an expression, casting the result back to the original expression type.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="nodes">The expression to visit.</param>
		/// <param name="callerName">The name of the calling method; used to report to report a better error message.</param>
		/// <typeparam name="T">The type of the expression.</typeparam>
		/// <exception cref="T:System.InvalidOperationException">The visit method for this node returned a different type.</exception>
		// Token: 0x0600053F RID: 1343 RVA: 0x00015030 File Offset: 0x00013230
		public ReadOnlyCollection<T> VisitAndConvert<T>(ReadOnlyCollection<T> nodes, string callerName) where T : Expression
		{
			ContractUtils.RequiresNotNull(nodes, "nodes");
			T[] array = null;
			int i = 0;
			int count = nodes.Count;
			while (i < count)
			{
				T t = this.Visit(nodes[i]) as T;
				if (t == null)
				{
					throw Error.MustRewriteToSameNode(callerName, typeof(T), callerName);
				}
				if (array != null)
				{
					array[i] = t;
				}
				else if (t != nodes[i])
				{
					array = new T[count];
					for (int j = 0; j < i; j++)
					{
						array[j] = nodes[j];
					}
					array[i] = t;
				}
				i++;
			}
			if (array == null)
			{
				return nodes;
			}
			return new TrueReadOnlyCollection<T>(array);
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.BinaryExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000540 RID: 1344 RVA: 0x000150F5 File Offset: 0x000132F5
		protected internal virtual Expression VisitBinary(BinaryExpression node)
		{
			return ExpressionVisitor.ValidateBinary(node, node.Update(this.Visit(node.Left), this.VisitAndConvert<LambdaExpression>(node.Conversion, "VisitBinary"), this.Visit(node.Right)));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.BlockExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000541 RID: 1345 RVA: 0x0001512C File Offset: 0x0001332C
		protected internal virtual Expression VisitBlock(BlockExpression node)
		{
			Expression[] array = ExpressionVisitorUtils.VisitBlockExpressions(this, node);
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = this.VisitAndConvert<ParameterExpression>(node.Variables, "VisitBlock");
			if (readOnlyCollection == node.Variables && array == null)
			{
				return node;
			}
			return node.Rewrite(readOnlyCollection, array);
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.ConditionalExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000542 RID: 1346 RVA: 0x00015169 File Offset: 0x00013369
		protected internal virtual Expression VisitConditional(ConditionalExpression node)
		{
			return node.Update(this.Visit(node.Test), this.Visit(node.IfTrue), this.Visit(node.IfFalse));
		}

		/// <summary>Visits the <see cref="T:System.Linq.Expressions.ConstantExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000543 RID: 1347 RVA: 0x00015195 File Offset: 0x00013395
		protected internal virtual Expression VisitConstant(ConstantExpression node)
		{
			return node;
		}

		/// <summary>Visits the <see cref="T:System.Linq.Expressions.DefaultExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000544 RID: 1348 RVA: 0x00015195 File Offset: 0x00013395
		protected internal virtual Expression VisitDefault(DefaultExpression node)
		{
			return node;
		}

		/// <summary>Visits the children of the extension expression.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000545 RID: 1349 RVA: 0x00015198 File Offset: 0x00013398
		protected internal virtual Expression VisitExtension(Expression node)
		{
			return node.VisitChildren(this);
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.GotoExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000546 RID: 1350 RVA: 0x000151A1 File Offset: 0x000133A1
		protected internal virtual Expression VisitGoto(GotoExpression node)
		{
			return node.Update(this.VisitLabelTarget(node.Target), this.Visit(node.Value));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.InvocationExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000547 RID: 1351 RVA: 0x000151C4 File Offset: 0x000133C4
		protected internal virtual Expression VisitInvocation(InvocationExpression node)
		{
			Expression expression = this.Visit(node.Expression);
			Expression[] array = this.VisitArguments(node);
			if (expression == node.Expression && array == null)
			{
				return node;
			}
			return node.Rewrite(expression, array);
		}

		/// <summary>Visits the <see cref="T:System.Linq.Expressions.LabelTarget" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000548 RID: 1352 RVA: 0x00015195 File Offset: 0x00013395
		protected virtual LabelTarget VisitLabelTarget(LabelTarget node)
		{
			return node;
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.LabelExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000549 RID: 1353 RVA: 0x000151FC File Offset: 0x000133FC
		protected internal virtual Expression VisitLabel(LabelExpression node)
		{
			return node.Update(this.VisitLabelTarget(node.Target), this.Visit(node.DefaultValue));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.Expression`1" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		/// <typeparam name="T">The type of the delegate.</typeparam>
		// Token: 0x0600054A RID: 1354 RVA: 0x0001521C File Offset: 0x0001341C
		protected internal virtual Expression VisitLambda<T>(Expression<T> node)
		{
			Expression expression = this.Visit(node.Body);
			ParameterExpression[] array = this.VisitParameters(node, "VisitLambda");
			if (expression == node.Body && array == null)
			{
				return node;
			}
			return node.Rewrite(expression, array);
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.LoopExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600054B RID: 1355 RVA: 0x00015259 File Offset: 0x00013459
		protected internal virtual Expression VisitLoop(LoopExpression node)
		{
			return node.Update(this.VisitLabelTarget(node.BreakLabel), this.VisitLabelTarget(node.ContinueLabel), this.Visit(node.Body));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600054C RID: 1356 RVA: 0x00015285 File Offset: 0x00013485
		protected internal virtual Expression VisitMember(MemberExpression node)
		{
			return node.Update(this.Visit(node.Expression));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.IndexExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600054D RID: 1357 RVA: 0x0001529C File Offset: 0x0001349C
		protected internal virtual Expression VisitIndex(IndexExpression node)
		{
			Expression expression = this.Visit(node.Object);
			Expression[] array = this.VisitArguments(node);
			if (expression == node.Object && array == null)
			{
				return node;
			}
			return node.Rewrite(expression, array);
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MethodCallExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600054E RID: 1358 RVA: 0x000152D4 File Offset: 0x000134D4
		protected internal virtual Expression VisitMethodCall(MethodCallExpression node)
		{
			Expression expression = this.Visit(node.Object);
			Expression[] array = this.VisitArguments(node);
			if (expression == node.Object && array == null)
			{
				return node;
			}
			return node.Rewrite(expression, array);
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.NewArrayExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600054F RID: 1359 RVA: 0x0001530C File Offset: 0x0001350C
		protected internal virtual Expression VisitNewArray(NewArrayExpression node)
		{
			return node.Update(this.Visit(node.Expressions));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.NewExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000550 RID: 1360 RVA: 0x00015320 File Offset: 0x00013520
		protected internal virtual Expression VisitNew(NewExpression node)
		{
			Expression[] array = this.VisitArguments(node);
			if (array == null)
			{
				return node;
			}
			return node.Update(array);
		}

		/// <summary>Visits the <see cref="T:System.Linq.Expressions.ParameterExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000551 RID: 1361 RVA: 0x00015195 File Offset: 0x00013395
		protected internal virtual Expression VisitParameter(ParameterExpression node)
		{
			return node;
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.SwitchCase" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000552 RID: 1362 RVA: 0x00015341 File Offset: 0x00013541
		protected virtual SwitchCase VisitSwitchCase(SwitchCase node)
		{
			return node.Update(this.Visit(node.TestValues), this.Visit(node.Body));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.SwitchExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000553 RID: 1363 RVA: 0x00015361 File Offset: 0x00013561
		protected internal virtual Expression VisitSwitch(SwitchExpression node)
		{
			return ExpressionVisitor.ValidateSwitch(node, node.Update(this.Visit(node.SwitchValue), ExpressionVisitor.Visit<SwitchCase>(node.Cases, new Func<SwitchCase, SwitchCase>(this.VisitSwitchCase)), this.Visit(node.DefaultBody)));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.CatchBlock" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000554 RID: 1364 RVA: 0x0001539F File Offset: 0x0001359F
		protected virtual CatchBlock VisitCatchBlock(CatchBlock node)
		{
			return node.Update(this.VisitAndConvert<ParameterExpression>(node.Variable, "VisitCatchBlock"), this.Visit(node.Filter), this.Visit(node.Body));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.TryExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000555 RID: 1365 RVA: 0x000153D0 File Offset: 0x000135D0
		protected internal virtual Expression VisitTry(TryExpression node)
		{
			return node.Update(this.Visit(node.Body), ExpressionVisitor.Visit<CatchBlock>(node.Handlers, new Func<CatchBlock, CatchBlock>(this.VisitCatchBlock)), this.Visit(node.Finally), this.Visit(node.Fault));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.TypeBinaryExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000556 RID: 1366 RVA: 0x0001541F File Offset: 0x0001361F
		protected internal virtual Expression VisitTypeBinary(TypeBinaryExpression node)
		{
			return node.Update(this.Visit(node.Expression));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.UnaryExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000557 RID: 1367 RVA: 0x00015433 File Offset: 0x00013633
		protected internal virtual Expression VisitUnary(UnaryExpression node)
		{
			return ExpressionVisitor.ValidateUnary(node, node.Update(this.Visit(node.Operand)));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberInitExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000558 RID: 1368 RVA: 0x0001544D File Offset: 0x0001364D
		protected internal virtual Expression VisitMemberInit(MemberInitExpression node)
		{
			return node.Update(this.VisitAndConvert<NewExpression>(node.NewExpression, "VisitMemberInit"), ExpressionVisitor.Visit<MemberBinding>(node.Bindings, new Func<MemberBinding, MemberBinding>(this.VisitMemberBinding)));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.ListInitExpression" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x06000559 RID: 1369 RVA: 0x0001547E File Offset: 0x0001367E
		protected internal virtual Expression VisitListInit(ListInitExpression node)
		{
			return node.Update(this.VisitAndConvert<NewExpression>(node.NewExpression, "VisitListInit"), ExpressionVisitor.Visit<ElementInit>(node.Initializers, new Func<ElementInit, ElementInit>(this.VisitElementInit)));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.ElementInit" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600055A RID: 1370 RVA: 0x000154AF File Offset: 0x000136AF
		protected virtual ElementInit VisitElementInit(ElementInit node)
		{
			return node.Update(this.Visit(node.Arguments));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberBinding" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600055B RID: 1371 RVA: 0x000154C4 File Offset: 0x000136C4
		protected virtual MemberBinding VisitMemberBinding(MemberBinding node)
		{
			switch (node.BindingType)
			{
			case MemberBindingType.Assignment:
				return this.VisitMemberAssignment((MemberAssignment)node);
			case MemberBindingType.MemberBinding:
				return this.VisitMemberMemberBinding((MemberMemberBinding)node);
			case MemberBindingType.ListBinding:
				return this.VisitMemberListBinding((MemberListBinding)node);
			default:
				throw Error.UnhandledBindingType(node.BindingType);
			}
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberAssignment" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600055C RID: 1372 RVA: 0x00015523 File Offset: 0x00013723
		protected virtual MemberAssignment VisitMemberAssignment(MemberAssignment node)
		{
			return node.Update(this.Visit(node.Expression));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberMemberBinding" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600055D RID: 1373 RVA: 0x00015537 File Offset: 0x00013737
		protected virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
		{
			return node.Update(ExpressionVisitor.Visit<MemberBinding>(node.Bindings, new Func<MemberBinding, MemberBinding>(this.VisitMemberBinding)));
		}

		/// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberListBinding" />.</summary>
		/// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
		/// <param name="node">The expression to visit.</param>
		// Token: 0x0600055E RID: 1374 RVA: 0x00015557 File Offset: 0x00013757
		protected virtual MemberListBinding VisitMemberListBinding(MemberListBinding node)
		{
			return node.Update(ExpressionVisitor.Visit<ElementInit>(node.Initializers, new Func<ElementInit, ElementInit>(this.VisitElementInit)));
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00015578 File Offset: 0x00013778
		private static UnaryExpression ValidateUnary(UnaryExpression before, UnaryExpression after)
		{
			if (before != after && before.Method == null)
			{
				if (after.Method != null)
				{
					throw Error.MustRewriteWithoutMethod(after.Method, "VisitUnary");
				}
				if (before.Operand != null && after.Operand != null)
				{
					ExpressionVisitor.ValidateChildType(before.Operand.Type, after.Operand.Type, "VisitUnary");
				}
			}
			return after;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000155E8 File Offset: 0x000137E8
		private static BinaryExpression ValidateBinary(BinaryExpression before, BinaryExpression after)
		{
			if (before != after && before.Method == null)
			{
				if (after.Method != null)
				{
					throw Error.MustRewriteWithoutMethod(after.Method, "VisitBinary");
				}
				ExpressionVisitor.ValidateChildType(before.Left.Type, after.Left.Type, "VisitBinary");
				ExpressionVisitor.ValidateChildType(before.Right.Type, after.Right.Type, "VisitBinary");
			}
			return after;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00015667 File Offset: 0x00013867
		private static SwitchExpression ValidateSwitch(SwitchExpression before, SwitchExpression after)
		{
			if (before.Comparison == null && after.Comparison != null)
			{
				throw Error.MustRewriteWithoutMethod(after.Comparison, "VisitSwitch");
			}
			return after;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00015697 File Offset: 0x00013897
		private static void ValidateChildType(Type before, Type after, string methodName)
		{
			if (before.IsValueType)
			{
				if (TypeUtils.AreEquivalent(before, after))
				{
					return;
				}
			}
			else if (!after.IsValueType)
			{
				return;
			}
			throw Error.MustRewriteChildToSameType(before, after, methodName);
		}
	}
}
