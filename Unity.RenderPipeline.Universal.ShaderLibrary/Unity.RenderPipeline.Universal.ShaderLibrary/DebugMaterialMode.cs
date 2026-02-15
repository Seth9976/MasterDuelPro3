using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000004 RID: 4
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.universal/ShaderLibrary/Debug/DebugViewEnums.cs")]
	public enum DebugMaterialMode
	{
		// Token: 0x04000007 RID: 7
		None,
		// Token: 0x04000008 RID: 8
		Albedo,
		// Token: 0x04000009 RID: 9
		Specular,
		// Token: 0x0400000A RID: 10
		Alpha,
		// Token: 0x0400000B RID: 11
		Smoothness,
		// Token: 0x0400000C RID: 12
		AmbientOcclusion,
		// Token: 0x0400000D RID: 13
		Emission,
		// Token: 0x0400000E RID: 14
		NormalWorldSpace,
		// Token: 0x0400000F RID: 15
		NormalTangentSpace,
		// Token: 0x04000010 RID: 16
		LightingComplexity,
		// Token: 0x04000011 RID: 17
		Metallic,
		// Token: 0x04000012 RID: 18
		SpriteMask,
		// Token: 0x04000013 RID: 19
		RenderingLayerMasks
	}
}
