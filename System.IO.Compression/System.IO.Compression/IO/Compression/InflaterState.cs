using System;

namespace System.IO.Compression
{
	// Token: 0x02000013 RID: 19
	internal enum InflaterState
	{
		// Token: 0x04000069 RID: 105
		ReadingHeader,
		// Token: 0x0400006A RID: 106
		ReadingBFinal = 2,
		// Token: 0x0400006B RID: 107
		ReadingBType,
		// Token: 0x0400006C RID: 108
		ReadingNumLitCodes,
		// Token: 0x0400006D RID: 109
		ReadingNumDistCodes,
		// Token: 0x0400006E RID: 110
		ReadingNumCodeLengthCodes,
		// Token: 0x0400006F RID: 111
		ReadingCodeLengthCodes,
		// Token: 0x04000070 RID: 112
		ReadingTreeCodesBefore,
		// Token: 0x04000071 RID: 113
		ReadingTreeCodesAfter,
		// Token: 0x04000072 RID: 114
		DecodeTop,
		// Token: 0x04000073 RID: 115
		HaveInitialLength,
		// Token: 0x04000074 RID: 116
		HaveFullLength,
		// Token: 0x04000075 RID: 117
		HaveDistCode,
		// Token: 0x04000076 RID: 118
		UncompressedAligning = 15,
		// Token: 0x04000077 RID: 119
		UncompressedByte1,
		// Token: 0x04000078 RID: 120
		UncompressedByte2,
		// Token: 0x04000079 RID: 121
		UncompressedByte3,
		// Token: 0x0400007A RID: 122
		UncompressedByte4,
		// Token: 0x0400007B RID: 123
		DecodingUncompressed,
		// Token: 0x0400007C RID: 124
		StartReadingFooter,
		// Token: 0x0400007D RID: 125
		ReadingFooter,
		// Token: 0x0400007E RID: 126
		VerifyingFooter,
		// Token: 0x0400007F RID: 127
		Done
	}
}
