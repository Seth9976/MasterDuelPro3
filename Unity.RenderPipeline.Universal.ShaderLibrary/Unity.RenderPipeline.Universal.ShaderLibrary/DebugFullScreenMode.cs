using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000007 RID: 7
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.universal/ShaderLibrary/Debug/DebugViewEnums.cs")]
	public enum DebugFullScreenMode
	{
		// Token: 0x04000022 RID: 34
		None,
		// Token: 0x04000023 RID: 35
		Depth,
		// Token: 0x04000024 RID: 36
		[InspectorName("Motion Vector (100x, normalized)")]
		MotionVector,
		// Token: 0x04000025 RID: 37
		AdditionalLightsShadowMap,
		// Token: 0x04000026 RID: 38
		MainLightShadowMap,
		// Token: 0x04000027 RID: 39
		AdditionalLightsCookieAtlas,
		// Token: 0x04000028 RID: 40
		ReflectionProbeAtlas,
		// Token: 0x04000029 RID: 41
		STP
	}
}
