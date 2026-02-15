using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000012 RID: 18
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.universal/ShaderLibrary/Debug/DebugViewEnums.cs")]
	[Flags]
	public enum DebugLightingFeatureFlags
	{
		// Token: 0x04000062 RID: 98
		None = 0,
		// Token: 0x04000063 RID: 99
		GlobalIllumination = 1,
		// Token: 0x04000064 RID: 100
		MainLight = 2,
		// Token: 0x04000065 RID: 101
		AdditionalLights = 4,
		// Token: 0x04000066 RID: 102
		VertexLighting = 8,
		// Token: 0x04000067 RID: 103
		Emission = 16,
		// Token: 0x04000068 RID: 104
		AmbientOcclusion = 32
	}
}
