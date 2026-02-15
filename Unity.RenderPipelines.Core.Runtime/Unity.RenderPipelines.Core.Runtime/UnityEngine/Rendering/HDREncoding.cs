using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200014A RID: 330
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/PostProcessing/HDROutputDefines.cs")]
	public enum HDREncoding
	{
		// Token: 0x04000624 RID: 1572
		Linear = 3,
		// Token: 0x04000625 RID: 1573
		PQ = 2,
		// Token: 0x04000626 RID: 1574
		Gamma22 = 4,
		// Token: 0x04000627 RID: 1575
		sRGB = 0
	}
}
