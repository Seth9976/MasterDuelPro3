using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200000E RID: 14
	public enum CompressionMethod
	{
		// Token: 0x0400002E RID: 46
		Stored,
		// Token: 0x0400002F RID: 47
		Deflated = 8,
		// Token: 0x04000030 RID: 48
		Deflate64,
		// Token: 0x04000031 RID: 49
		BZip2 = 12,
		// Token: 0x04000032 RID: 50
		LZMA = 14,
		// Token: 0x04000033 RID: 51
		PPMd = 98,
		// Token: 0x04000034 RID: 52
		WinZipAES
	}
}
