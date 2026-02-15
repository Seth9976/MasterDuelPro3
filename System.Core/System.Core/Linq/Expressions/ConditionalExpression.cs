using System;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	/// <summary>Represents an expression that has a conditional operator.</summary>
	// Token: 0x0200008D RID: 141
	[DebuggerTypeProxy(typeof(Expression.ConditionalExpressionProxy))]
	public class ConditionalExpression : Expression
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x0001313A File Offset: 0x0001133A
		internal ConditionalExpression(Expression test, Expression ifTrue)
		{
			this.Test = test;
			this.IfTrue = ifTrue;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00013150 File Offset: 0x00011350
		internal static ConditionalExpression Make(Expression test, Expression ifTrue, Expression ifFalse, Type type)
		{
			if (ifTrue.Type != type || ifFalse.Type != type)
			{
				return new FullConditionalExpressionWithType(test, ifTrue, ifFalse, type);
			}
			if (ifFalse is DefaultExpression && ifFalse.Type == typeof(void))
			{
				return new ConditionalExpression(test, ifTrue);
			}
			return new FullConditionalExpression(test, ifTrue, ifFalse);
		}

		/// <summary>Returns the node type of this expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x000131B2 File Offset: 0x000113B2
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Conditional;
			}
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.ConditionalExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x000131B5 File Offset: 0x000113B5
		public override Type Type
		{
			get
			{
				return this.IfTrue.Type;
			}
		}

		/// <summary>Gets the test of the conditional operation.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the test of the conditional operation.</returns>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x000131C2 File Offset: 0x000113C2
		public Expression Test { get; }

		/// <summary>Gets the expression to execute if the test evaluates to true.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the expression to execute if the test is true.</returns>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x000131CA File Offset: 0x000113CA
		public Expression IfTrue { get; }

		/// <summary>Gets the expression to execute if the test evaluates to false.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the expression to execute if the test is false.</returns>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x000131D2 File Offset: 0x000113D2
		public Expression IfFalse
		{
			get
			{
				return this.GetFalse();
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000131DA File Offset: 0x000113DA
		internal virtual Expression GetFalse()
		{
			return Utils.Empty;
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <returns>The result of visiting this node.</returns>
		/// <param name="visitor">The visitor to visit this node with.</param>
		// Token: 0x06000440 RID: 1088 RVA: 0x000131E1 File Offset: 0x000113E1
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitConditional(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression</summary>
		/// <returns>This expression if no children changed, or an expression with the updated children.</returns>
		/// <param name="test">The <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" /> property of the result.</param>
		/// <param name="ifTrue">The <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" /> property of the result.</param>
		/// <param name="ifFalse">The <see cref="P:System.Linq.Expressions.ConditionalExpression.IfFalse" /> property of the result.</param>
		// Token: 0x06000441 RID: 1089 RVA: 0x000131EA File Offset: 0x000113EA
		public ConditionalExpression Update(Expression test, Expression ifTrue, Expression ifFalse)
		{
			if (test == this.Test && ifTrue == this.IfTrue && ifFalse == this.IfFalse)
			{
				return this;
			}
			return Expression.Condition(test, ifTrue, ifFalse, this.Type);
		}
	}
}
