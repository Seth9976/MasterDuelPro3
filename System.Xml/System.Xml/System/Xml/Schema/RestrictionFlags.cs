using System;

namespace System.Xml.Schema
{
	// Token: 0x02000232 RID: 562
	[Flags]
	internal enum RestrictionFlags
	{
		// Token: 0x04000B9D RID: 2973
		Length = 1,
		// Token: 0x04000B9E RID: 2974
		MinLength = 2,
		// Token: 0x04000B9F RID: 2975
		MaxLength = 4,
		// Token: 0x04000BA0 RID: 2976
		Pattern = 8,
		// Token: 0x04000BA1 RID: 2977
		Enumeration = 16,
		// Token: 0x04000BA2 RID: 2978
		WhiteSpace = 32,
		// Token: 0x04000BA3 RID: 2979
		MaxInclusive = 64,
		// Token: 0x04000BA4 RID: 2980
		MaxExclusive = 128,
		// Token: 0x04000BA5 RID: 2981
		MinInclusive = 256,
		// Token: 0x04000BA6 RID: 2982
		MinExclusive = 512,
		// Token: 0x04000BA7 RID: 2983
		TotalDigits = 1024,
		// Token: 0x04000BA8 RID: 2984
		FractionDigits = 2048
	}
}
