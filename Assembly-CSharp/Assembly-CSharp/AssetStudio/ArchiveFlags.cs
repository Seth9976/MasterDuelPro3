using System;

namespace AssetStudio
{
	// Token: 0x02000090 RID: 144
	[Flags]
	public enum ArchiveFlags
	{
		// Token: 0x040003A8 RID: 936
		CompressionTypeMask = 63,
		// Token: 0x040003A9 RID: 937
		BlocksAndDirectoryInfoCombined = 64,
		// Token: 0x040003AA RID: 938
		BlocksInfoAtTheEnd = 128,
		// Token: 0x040003AB RID: 939
		OldWebPluginCompatibility = 256,
		// Token: 0x040003AC RID: 940
		BlockInfoNeedPaddingAtStart = 512
	}
}
