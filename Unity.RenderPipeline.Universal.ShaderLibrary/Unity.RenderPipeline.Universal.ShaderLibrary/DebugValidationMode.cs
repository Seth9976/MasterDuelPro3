using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200000E RID: 14
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.universal/ShaderLibrary/Debug/DebugViewEnums.cs")]
	public enum DebugValidationMode
	{
		// Token: 0x0400004B RID: 75
		None,
		// Token: 0x0400004C RID: 76
		[InspectorName("Highlight NaN, Inf and Negative Values")]
		HighlightNanInfNegative,
		// Token: 0x0400004D RID: 77
		[InspectorName("Highlight Values Outside Range")]
		HighlightOutsideOfRange
	}
}
