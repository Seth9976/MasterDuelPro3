using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000148 RID: 328
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/PostProcessing/HDROutputDefines.cs")]
	public enum HDRRangeReduction
	{
		// Token: 0x04000619 RID: 1561
		None,
		// Token: 0x0400061A RID: 1562
		Reinhard,
		// Token: 0x0400061B RID: 1563
		BT2390,
		// Token: 0x0400061C RID: 1564
		ACES1000Nits,
		// Token: 0x0400061D RID: 1565
		ACES2000Nits,
		// Token: 0x0400061E RID: 1566
		ACES4000Nits
	}
}
