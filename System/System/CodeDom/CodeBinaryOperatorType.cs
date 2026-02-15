using System;

namespace System.CodeDom
{
	/// <summary>Defines identifiers for supported binary operators.</summary>
	// Token: 0x020001E4 RID: 484
	public enum CodeBinaryOperatorType
	{
		/// <summary>Addition operator.</summary>
		// Token: 0x04000885 RID: 2181
		Add,
		/// <summary>Subtraction operator.</summary>
		// Token: 0x04000886 RID: 2182
		Subtract,
		/// <summary>Multiplication operator.</summary>
		// Token: 0x04000887 RID: 2183
		Multiply,
		/// <summary>Division operator.</summary>
		// Token: 0x04000888 RID: 2184
		Divide,
		/// <summary>Modulus operator.</summary>
		// Token: 0x04000889 RID: 2185
		Modulus,
		/// <summary>Assignment operator.</summary>
		// Token: 0x0400088A RID: 2186
		Assign,
		/// <summary>Identity not equal operator.</summary>
		// Token: 0x0400088B RID: 2187
		IdentityInequality,
		/// <summary>Identity equal operator.</summary>
		// Token: 0x0400088C RID: 2188
		IdentityEquality,
		/// <summary>Value equal operator.</summary>
		// Token: 0x0400088D RID: 2189
		ValueEquality,
		/// <summary>Bitwise or operator.</summary>
		// Token: 0x0400088E RID: 2190
		BitwiseOr,
		/// <summary>Bitwise and operator.</summary>
		// Token: 0x0400088F RID: 2191
		BitwiseAnd,
		/// <summary>Boolean or operator. This represents a short circuiting operator. A short circuiting operator will evaluate only as many expressions as necessary before returning a correct value.</summary>
		// Token: 0x04000890 RID: 2192
		BooleanOr,
		/// <summary>Boolean and operator. This represents a short circuiting operator. A short circuiting operator will evaluate only as many expressions as necessary before returning a correct value.</summary>
		// Token: 0x04000891 RID: 2193
		BooleanAnd,
		/// <summary>Less than operator.</summary>
		// Token: 0x04000892 RID: 2194
		LessThan,
		/// <summary>Less than or equal operator.</summary>
		// Token: 0x04000893 RID: 2195
		LessThanOrEqual,
		/// <summary>Greater than operator.</summary>
		// Token: 0x04000894 RID: 2196
		GreaterThan,
		/// <summary>Greater than or equal operator.</summary>
		// Token: 0x04000895 RID: 2197
		GreaterThanOrEqual
	}
}
