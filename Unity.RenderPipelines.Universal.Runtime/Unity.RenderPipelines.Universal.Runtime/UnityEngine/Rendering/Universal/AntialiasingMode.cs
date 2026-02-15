using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001AC RID: 428
	public enum AntialiasingMode
	{
		// Token: 0x04000966 RID: 2406
		[InspectorName("No Anti-aliasing")]
		None,
		// Token: 0x04000967 RID: 2407
		[InspectorName("Fast Approximate Anti-aliasing (FXAA)")]
		FastApproximateAntialiasing,
		// Token: 0x04000968 RID: 2408
		[InspectorName("Subpixel Morphological Anti-aliasing (SMAA)")]
		SubpixelMorphologicalAntiAliasing,
		// Token: 0x04000969 RID: 2409
		[InspectorName("Temporal Anti-aliasing (TAA)")]
		TemporalAntiAliasing
	}
}
