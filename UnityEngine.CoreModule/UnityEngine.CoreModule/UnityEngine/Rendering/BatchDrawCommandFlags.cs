using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000374 RID: 884
	[Flags]
	public enum BatchDrawCommandFlags
	{
		// Token: 0x04000A63 RID: 2659
		None = 0,
		// Token: 0x04000A64 RID: 2660
		FlipWinding = 1,
		// Token: 0x04000A65 RID: 2661
		HasMotion = 2,
		// Token: 0x04000A66 RID: 2662
		IsLightMapped = 4,
		// Token: 0x04000A67 RID: 2663
		HasSortingPosition = 8,
		// Token: 0x04000A68 RID: 2664
		LODCrossFadeKeyword = 16,
		// Token: 0x04000A69 RID: 2665
		LODCrossFadeValuePacked = 32,
		// Token: 0x04000A6A RID: 2666
		LODCrossFade = 48,
		// Token: 0x04000A6B RID: 2667
		UseLegacyLightmapsKeyword = 64
	}
}
