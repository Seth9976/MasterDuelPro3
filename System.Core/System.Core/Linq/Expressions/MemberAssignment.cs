using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	/// <summary>Represents assignment operation for a field or property of an object.</summary>
	// Token: 0x020000B3 RID: 179
	public sealed class MemberAssignment : MemberBinding
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x00016088 File Offset: 0x00014288
		internal MemberAssignment(MemberInfo member, Expression expression)
			: base(MemberBindingType.Assignment, member)
		{
			this._expression = expression;
		}

		/// <summary>Gets the expression to assign to the field or property.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> that represents the value to assign to the field or property.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00016099 File Offset: 0x00014299
		public Expression Expression
		{
			get
			{
				return this._expression;
			}
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="expression">The <see cref="P:System.Linq.Expressions.MemberAssignment.Expression" /> property of the result.</param>
		// Token: 0x060005F5 RID: 1525 RVA: 0x000160A1 File Offset: 0x000142A1
		public MemberAssignment Update(Expression expression)
		{
			if (expression == this.Expression)
			{
				return this;
			}
			return Expression.Bind(base.Member, expression);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000A01D File Offset: 0x0000821D
		internal override void ValidateAsDefinedHere(int index)
		{
		}

		// Token: 0x040001D1 RID: 465
		private readonly Expression _expression;
	}
}
