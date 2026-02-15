using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200000C RID: 12
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.universal/ShaderLibrary/Debug/DebugViewEnums.cs")]
	public enum DebugMipMapModeTerrainTexture
	{
		// Token: 0x04000041 RID: 65
		Control,
		// Token: 0x04000042 RID: 66
		[InspectorName("Layer 0 - Diffuse")]
		Layer0,
		// Token: 0x04000043 RID: 67
		[InspectorName("Layer 1 - Diffuse")]
		Layer1,
		// Token: 0x04000044 RID: 68
		[InspectorName("Layer 2 - Diffuse")]
		Layer2,
		// Token: 0x04000045 RID: 69
		[InspectorName("Layer 3 - Diffuse")]
		Layer3
	}
}
