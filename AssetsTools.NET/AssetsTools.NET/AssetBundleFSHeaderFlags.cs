using System;

namespace AssetsTools.NET
{
	// Token: 0x02000036 RID: 54
	public enum AssetBundleFSHeaderFlags
	{
		// Token: 0x04000150 RID: 336
		None,
		// Token: 0x04000151 RID: 337
		LZMACompressed,
		// Token: 0x04000152 RID: 338
		LZ4Compressed,
		// Token: 0x04000153 RID: 339
		LZ4HCCompressed,
		// Token: 0x04000154 RID: 340
		CompressionMask = 63,
		// Token: 0x04000155 RID: 341
		HasDirectoryInfo,
		// Token: 0x04000156 RID: 342
		BlockAndDirAtEnd = 128,
		// Token: 0x04000157 RID: 343
		OldWebPluginCompatibility = 256,
		// Token: 0x04000158 RID: 344
		BlockInfoNeedPaddingAtStart = 512
	}
}
