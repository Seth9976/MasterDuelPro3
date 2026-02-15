using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	/// <summary>Represents one case of a <see cref="T:System.Linq.Expressions.SwitchExpression" />.</summary>
	// Token: 0x020000D8 RID: 216
	[DebuggerTypeProxy(typeof(Expression.SwitchCaseProxy))]
	public sealed class SwitchCase
	{
		// Token: 0x0600072B RID: 1835 RVA: 0x00017569 File Offset: 0x00015769
		internal SwitchCase(Expression body, ReadOnlyCollection<Expression> testValues)
		{
			this.Body = body;
			this.TestValues = testValues;
		}

		/// <summary>Gets the values of this case. This case is selected for execution when the <see cref="P:System.Linq.Expressions.SwitchExpression.SwitchValue" /> matches any of these values.</summary>
		/// <returns>The read-only collection of the values for this case block.</returns>
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001757F File Offset: 0x0001577F
		public ReadOnlyCollection<Expression> TestValues { get; }

		/// <summary>Gets the body of this case.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> object that represents the body of the case block.</returns>
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00017587 File Offset: 0x00015787
		public Expression Body { get; }

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
		// Token: 0x0600072E RID: 1838 RVA: 0x0001758F File Offset: 0x0001578F
		public override string ToString()
		{
			return ExpressionStringBuilder.SwitchCaseToString(this);
		}

		/// <summary>Creates a new expression that is like this one, but using the supplied children. If all of the children are the same, it will return this expression.</summary>
		/// <returns>This expression if no children are changed or an expression with the updated children.</returns>
		/// <param name="testValues">The <see cref="P:System.Linq.Expressions.SwitchCase.TestValues" /> property of the result.</param>
		/// <param name="body">The <see cref="P:System.Linq.Expressions.SwitchCase.Body" /> property of the result.</param>
		// Token: 0x0600072F RID: 1839 RVA: 0x00017597 File Offset: 0x00015797
		public SwitchCase Update(IEnumerable<Expression> testValues, Expression body)
		{
			if (((body == this.Body) & (testValues != null)) && ExpressionUtils.SameElements<Expression>(ref testValues, this.TestValues))
			{
				return this;
			}
			return Expression.SwitchCase(body, testValues);
		}
	}
}
