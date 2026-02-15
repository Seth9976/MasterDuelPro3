using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000010 RID: 16
	[Flags]
	public enum GeneralBitFlags
	{
		// Token: 0x04000045 RID: 69
		Encrypted = 1,
		// Token: 0x04000046 RID: 70
		Method = 6,
		// Token: 0x04000047 RID: 71
		Descriptor = 8,
		// Token: 0x04000048 RID: 72
		ReservedPKware4 = 16,
		// Token: 0x04000049 RID: 73
		Patched = 32,
		// Token: 0x0400004A RID: 74
		StrongEncryption = 64,
		// Token: 0x0400004B RID: 75
		Unused7 = 128,
		// Token: 0x0400004C RID: 76
		Unused8 = 256,
		// Token: 0x0400004D RID: 77
		Unused9 = 512,
		// Token: 0x0400004E RID: 78
		Unused10 = 1024,
		// Token: 0x0400004F RID: 79
		UnicodeText = 2048,
		// Token: 0x04000050 RID: 80
		EnhancedCompress = 4096,
		// Token: 0x04000051 RID: 81
		HeaderMasked = 8192,
		// Token: 0x04000052 RID: 82
		ReservedPkware14 = 16384,
		// Token: 0x04000053 RID: 83
		ReservedPkware15 = 32768
	}
}
