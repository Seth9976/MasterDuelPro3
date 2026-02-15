using System;

namespace System.Reflection.Emit
{
	/// <summary>Specifies one of two factors that determine the memory alignment of fields when a type is marshaled.</summary>
	// Token: 0x0200064E RID: 1614
	public enum PackingSize
	{
		/// <summary>The packing size is not specified.</summary>
		// Token: 0x040018AC RID: 6316
		Unspecified,
		/// <summary>The packing size is 1 byte.</summary>
		// Token: 0x040018AD RID: 6317
		Size1,
		/// <summary>The packing size is 2 bytes.</summary>
		// Token: 0x040018AE RID: 6318
		Size2,
		/// <summary>The packing size is 4 bytes.</summary>
		// Token: 0x040018AF RID: 6319
		Size4 = 4,
		/// <summary>The packing size is 8 bytes.</summary>
		// Token: 0x040018B0 RID: 6320
		Size8 = 8,
		/// <summary>The packing size is 16 bytes.</summary>
		// Token: 0x040018B1 RID: 6321
		Size16 = 16,
		/// <summary>The packing size is 32 bytes.</summary>
		// Token: 0x040018B2 RID: 6322
		Size32 = 32,
		/// <summary>The packing size is 64 bytes.</summary>
		// Token: 0x040018B3 RID: 6323
		Size64 = 64,
		/// <summary>The packing size is 128 bytes.</summary>
		// Token: 0x040018B4 RID: 6324
		Size128 = 128
	}
}
