using System;

namespace AssetsTools.NET.Extra
{
	// Token: 0x02000077 RID: 119
	[Flags]
	public enum AssetReadFlags
	{
		// Token: 0x040003F3 RID: 1011
		None = 0,
		// Token: 0x040003F4 RID: 1012
		PreferEditor = 1,
		// Token: 0x040003F5 RID: 1013
		SkipMonoBehaviourFields = 2,
		// Token: 0x040003F6 RID: 1014
		ForceFromCldb = 4
	}
}
