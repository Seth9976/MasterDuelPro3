using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B3 RID: 179
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/GPUDriven/OcclusionCullingCommon.cs")]
	internal enum OcclusionCullingCommonConfig
	{
		// Token: 0x04000399 RID: 921
		MaxOccluderMips = 8,
		// Token: 0x0400039A RID: 922
		MaxOccluderSilhouettePlanes = 6,
		// Token: 0x0400039B RID: 923
		MaxSubviewsPerView = 6,
		// Token: 0x0400039C RID: 924
		DebugPyramidOffset = 4
	}
}
