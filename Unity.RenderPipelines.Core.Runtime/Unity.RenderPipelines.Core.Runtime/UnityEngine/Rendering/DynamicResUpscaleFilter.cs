using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200004B RID: 75
	public enum DynamicResUpscaleFilter : byte
	{
		// Token: 0x040000FD RID: 253
		[Obsolete("Bilinear upscale filter is considered obsolete and is not supported anymore, please use CatmullRom for a very cheap, but blurry filter.", false)]
		Bilinear,
		// Token: 0x040000FE RID: 254
		CatmullRom,
		// Token: 0x040000FF RID: 255
		[Obsolete("Lanczos upscale filter is considered obsolete and is not supported anymore, please use Contrast Adaptive Sharpening for very sharp filter or FidelityFX Super Resolution 1.0.", false)]
		Lanczos,
		// Token: 0x04000100 RID: 256
		ContrastAdaptiveSharpen,
		// Token: 0x04000101 RID: 257
		[InspectorName("FidelityFX Super Resolution 1.0")]
		EdgeAdaptiveScalingUpres,
		// Token: 0x04000102 RID: 258
		[InspectorName("TAA Upscale")]
		TAAU
	}
}
