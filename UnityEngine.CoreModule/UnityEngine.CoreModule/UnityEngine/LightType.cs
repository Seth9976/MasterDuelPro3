using System;

namespace UnityEngine
{
	// Token: 0x02000102 RID: 258
	public enum LightType
	{
		// Token: 0x040002F5 RID: 757
		Spot,
		// Token: 0x040002F6 RID: 758
		Directional,
		// Token: 0x040002F7 RID: 759
		Point,
		// Token: 0x040002F8 RID: 760
		[Obsolete("Enum member LightType.Area has been deprecated. Use LightType.Rectangle instead (UnityUpgradable) -> Rectangle", true)]
		Area,
		// Token: 0x040002F9 RID: 761
		Rectangle = 3,
		// Token: 0x040002FA RID: 762
		Disc,
		// Token: 0x040002FB RID: 763
		Pyramid,
		// Token: 0x040002FC RID: 764
		Box,
		// Token: 0x040002FD RID: 765
		Tube
	}
}
