using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000116 RID: 278
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/Lighting/ProbeVolume/ProbeReferenceVolume.Debug.cs")]
	public enum DebugProbeShadingMode
	{
		// Token: 0x040004C1 RID: 1217
		SH,
		// Token: 0x040004C2 RID: 1218
		SHL0,
		// Token: 0x040004C3 RID: 1219
		SHL0L1,
		// Token: 0x040004C4 RID: 1220
		Validity,
		// Token: 0x040004C5 RID: 1221
		ValidityOverDilationThreshold,
		// Token: 0x040004C6 RID: 1222
		RenderingLayerMasks,
		// Token: 0x040004C7 RID: 1223
		InvalidatedByAdjustmentVolumes,
		// Token: 0x040004C8 RID: 1224
		Size,
		// Token: 0x040004C9 RID: 1225
		SkyOcclusionSH,
		// Token: 0x040004CA RID: 1226
		SkyDirection,
		// Token: 0x040004CB RID: 1227
		ProbeOcclusion
	}
}
