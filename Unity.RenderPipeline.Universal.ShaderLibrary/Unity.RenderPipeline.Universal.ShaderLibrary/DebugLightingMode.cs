using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000010 RID: 16
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.universal/ShaderLibrary/Debug/DebugViewEnums.cs")]
	public enum DebugLightingMode
	{
		// Token: 0x04000055 RID: 85
		None,
		// Token: 0x04000056 RID: 86
		ShadowCascades,
		// Token: 0x04000057 RID: 87
		LightingWithoutNormalMaps,
		// Token: 0x04000058 RID: 88
		LightingWithNormalMaps,
		// Token: 0x04000059 RID: 89
		Reflections,
		// Token: 0x0400005A RID: 90
		ReflectionsWithSmoothness,
		// Token: 0x0400005B RID: 91
		GlobalIllumination
	}
}
