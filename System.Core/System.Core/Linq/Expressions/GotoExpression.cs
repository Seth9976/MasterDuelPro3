using System;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	/// <summary>Represents an unconditional jump. This includes return statements, break and continue statements, and other jumps.</summary>
	// Token: 0x0200009A RID: 154
	[DebuggerTypeProxy(typeof(Expression.GotoExpressionProxy))]
	public sealed class GotoExpression : Expression
	{
		// Token: 0x06000563 RID: 1379 RVA: 0x000156BC File Offset: 0x000138BC
		internal GotoExpression(GotoExpressionKind kind, LabelTarget target, Expression value, Type type)
		{
			this.Kind = kind;
			this.Value = value;
			this.Target = target;
			this.Type = type;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.GotoExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x000156E1 File Offset: 0x000138E1
		public sealed override Type Type { get; }

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x000156E9 File Offset: 0x000138E9
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Goto;
			}
		}

		/// <summary>The value passed to the target, or null if the target is of type System.Void.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object representing the value passed to the target or null.</returns>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x000156ED File Offset: 0x000138ED
		public Expression Value { get; }

		/// <summary>The target label where this node jumps to.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.LabelTarget" /> object representing the target label for this node.</returns>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x000156F5 File Offset: 0x000138F5
		public LabelTarget Target { get; }

		/// <summary>The kind of the "go to" expression. Serves information purposes only.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.GotoExpressionKind" /> object representing the kind of the "go to" expression.</returns>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x000156FD File Offset: 0x000138FD
		public GotoExpressionKind Kind { get; }

		// Token: 0x06000569 RID: 1385 RVA: 0x00015705 File Offset: 0x00013905
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitGoto(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="target">The <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property of the result. </param>
		/// <param name="value">The <see cref="P:System.Linq.Expressions.GotoExpression.Value" /> property of the result. </param>
		// Token: 0x0600056A RID: 1386 RVA: 0x0001570E File Offset: 0x0001390E
		public GotoExpression Update(LabelTarget target, Expression value)
		{
			if (target == this.Target && value == this.Value)
			{
				return this;
			}
			return Expression.MakeGoto(this.Kind, target, value, this.Type);
		}
	}
}
