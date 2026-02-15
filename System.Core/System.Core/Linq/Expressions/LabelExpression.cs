using System;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	/// <summary>Represents a label, which can be put in any <see cref="T:System.Linq.Expressions.Expression" /> context. If it is jumped to, it will get the value provided by the corresponding <see cref="T:System.Linq.Expressions.GotoExpression" />. Otherwise, it receives the value in <see cref="P:System.Linq.Expressions.LabelExpression.DefaultValue" />. If the <see cref="T:System.Type" /> equals System.Void, no value should be provided.</summary>
	// Token: 0x020000A7 RID: 167
	[DebuggerTypeProxy(typeof(Expression.LabelExpressionProxy))]
	public sealed class LabelExpression : Expression
	{
		// Token: 0x060005A0 RID: 1440 RVA: 0x00015B8B File Offset: 0x00013D8B
		internal LabelExpression(LabelTarget label, Expression defaultValue)
		{
			this.Target = label;
			this.DefaultValue = defaultValue;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.LabelExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00015BA1 File Offset: 0x00013DA1
		public sealed override Type Type
		{
			get
			{
				return this.Target.Type;
			}
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00015BAE File Offset: 0x00013DAE
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.Label;
			}
		}

		/// <summary>The <see cref="T:System.Linq.Expressions.LabelTarget" /> which this label is associated with.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.LabelTarget" /> which this label is associated with.</returns>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00015BB2 File Offset: 0x00013DB2
		public LabelTarget Target { get; }

		/// <summary>The value of the <see cref="T:System.Linq.Expressions.LabelExpression" /> when the label is reached through regular control flow (for example, is not jumped to).</summary>
		/// <returns>The Expression object representing the value of the <see cref="T:System.Linq.Expressions.LabelExpression" />.</returns>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00015BBA File Offset: 0x00013DBA
		public Expression DefaultValue { get; }

		// Token: 0x060005A5 RID: 1445 RVA: 0x00015BC2 File Offset: 0x00013DC2
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitLabel(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="target">The <see cref="P:System.Linq.Expressions.LabelExpression.Target" /> property of the result.</param>
		/// <param name="defaultValue">The <see cref="P:System.Linq.Expressions.LabelExpression.DefaultValue" /> property of the result</param>
		// Token: 0x060005A6 RID: 1446 RVA: 0x00015BCB File Offset: 0x00013DCB
		public LabelExpression Update(LabelTarget target, Expression defaultValue)
		{
			if (target == this.Target && defaultValue == this.DefaultValue)
			{
				return this;
			}
			return Expression.Label(target, defaultValue);
		}
	}
}
