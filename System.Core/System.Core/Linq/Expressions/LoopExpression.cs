using System;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	/// <summary>Represents an infinite loop. It can be exited with "break".</summary>
	// Token: 0x020000B2 RID: 178
	[DebuggerTypeProxy(typeof(Expression.LoopExpressionProxy))]
	public sealed class LoopExpression : Expression
	{
		// Token: 0x060005EB RID: 1515 RVA: 0x00015FFF File Offset: 0x000141FF
		internal LoopExpression(Expression body, LabelTarget @break, LabelTarget @continue)
		{
			this.Body = body;
			this.BreakLabel = @break;
			this.ContinueLabel = @continue;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.LoopExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0001601C File Offset: 0x0001421C
		public sealed override Type Type
		{
			get
			{
				if (this.BreakLabel != null)
				{
					return this.BreakLabel.Type;
				}
				return typeof(void);
			}
		}

		/// <summary>Returns the node type of this expression. Extension nodes should return <see cref="F:System.Linq.Expressions.ExpressionType.Extension" /> when overriding this method.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> of the expression.</returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0001603C File Offset: 0x0001423C
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Loop;
			}
		}

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.Expression" /> that is the body of the loop.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> that is the body of the loop.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00016040 File Offset: 0x00014240
		public Expression Body { get; }

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.LabelTarget" /> that is used by the loop body as a break statement target.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.LabelTarget" /> that is used by the loop body as a break statement target.</returns>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00016048 File Offset: 0x00014248
		public LabelTarget BreakLabel { get; }

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.LabelTarget" /> that is used by the loop body as a continue statement target.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.LabelTarget" /> that is used by the loop body as a continue statement target.</returns>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00016050 File Offset: 0x00014250
		public LabelTarget ContinueLabel { get; }

		// Token: 0x060005F1 RID: 1521 RVA: 0x00016058 File Offset: 0x00014258
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitLoop(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="breakLabel">The <see cref="P:System.Linq.Expressions.LoopExpression.BreakLabel" /> property of the result.</param>
		/// <param name="continueLabel">The <see cref="P:System.Linq.Expressions.LoopExpression.ContinueLabel" /> property of the result.</param>
		/// <param name="body">The <see cref="P:System.Linq.Expressions.LoopExpression.Body" /> property of the result.</param>
		// Token: 0x060005F2 RID: 1522 RVA: 0x00016061 File Offset: 0x00014261
		public LoopExpression Update(LabelTarget breakLabel, LabelTarget continueLabel, Expression body)
		{
			if (breakLabel == this.BreakLabel && continueLabel == this.ContinueLabel && body == this.Body)
			{
				return this;
			}
			return Expression.Loop(body, breakLabel, continueLabel);
		}
	}
}
