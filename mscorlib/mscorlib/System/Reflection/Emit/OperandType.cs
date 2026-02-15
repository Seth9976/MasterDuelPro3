using System;

namespace System.Reflection.Emit
{
	/// <summary>Describes the operand type of Microsoft intermediate language (MSIL) instruction.</summary>
	// Token: 0x0200064D RID: 1613
	public enum OperandType
	{
		/// <summary>The operand is a 32-bit integer branch target.</summary>
		// Token: 0x04001899 RID: 6297
		InlineBrTarget,
		/// <summary>The operand is a 32-bit metadata token.</summary>
		// Token: 0x0400189A RID: 6298
		InlineField,
		/// <summary>The operand is a 32-bit integer.</summary>
		// Token: 0x0400189B RID: 6299
		InlineI,
		/// <summary>The operand is a 64-bit integer.</summary>
		// Token: 0x0400189C RID: 6300
		InlineI8,
		/// <summary>The operand is a 32-bit metadata token.</summary>
		// Token: 0x0400189D RID: 6301
		InlineMethod,
		/// <summary>No operand.</summary>
		// Token: 0x0400189E RID: 6302
		InlineNone,
		/// <summary>The operand is reserved and should not be used.</summary>
		// Token: 0x0400189F RID: 6303
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		InlinePhi,
		/// <summary>The operand is a 64-bit IEEE floating point number.</summary>
		// Token: 0x040018A0 RID: 6304
		InlineR,
		/// <summary>The operand is a 32-bit metadata signature token.</summary>
		// Token: 0x040018A1 RID: 6305
		InlineSig = 9,
		/// <summary>The operand is a 32-bit metadata string token.</summary>
		// Token: 0x040018A2 RID: 6306
		InlineString,
		/// <summary>The operand is the 32-bit integer argument to a switch instruction.</summary>
		// Token: 0x040018A3 RID: 6307
		InlineSwitch,
		/// <summary>The operand is a FieldRef, MethodRef, or TypeRef token.</summary>
		// Token: 0x040018A4 RID: 6308
		InlineTok,
		/// <summary>The operand is a 32-bit metadata token.</summary>
		// Token: 0x040018A5 RID: 6309
		InlineType,
		/// <summary>The operand is 16-bit integer containing the ordinal of a local variable or an argument.</summary>
		// Token: 0x040018A6 RID: 6310
		InlineVar,
		/// <summary>The operand is an 8-bit integer branch target.</summary>
		// Token: 0x040018A7 RID: 6311
		ShortInlineBrTarget,
		/// <summary>The operand is an 8-bit integer.</summary>
		// Token: 0x040018A8 RID: 6312
		ShortInlineI,
		/// <summary>The operand is a 32-bit IEEE floating point number.</summary>
		// Token: 0x040018A9 RID: 6313
		ShortInlineR,
		/// <summary>The operand is an 8-bit integer containing the ordinal of a local variable or an argumenta.</summary>
		// Token: 0x040018AA RID: 6314
		ShortInlineVar
	}
}
