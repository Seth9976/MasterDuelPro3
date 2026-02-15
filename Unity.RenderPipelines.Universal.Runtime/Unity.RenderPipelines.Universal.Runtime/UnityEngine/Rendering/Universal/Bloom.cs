using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000B5 RID: 181
	[VolumeComponentMenu("Post-processing/Bloom")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[Serializable]
	public sealed class Bloom : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x00012086 File Offset: 0x00010286
		public bool IsActive()
		{
			return this.intensity.value > 0f;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00002886 File Offset: 0x00000A86
		[Obsolete("Unused #from(2023.1)", false)]
		public bool IsTileCompatible()
		{
			return false;
		}

		// Token: 0x04000397 RID: 919
		[Obsolete("This is obsolete, please use maxIterations instead.", true)]
		[Tooltip("The number of final iterations to skip in the effect processing sequence.")]
		public ClampedIntParameter skipIterations = new ClampedIntParameter(1, 0, 16, false);

		// Token: 0x04000398 RID: 920
		[Header("Bloom")]
		[Tooltip("Filters out pixels under this level of brightness. Value is in gamma-space.")]
		public MinFloatParameter threshold = new MinFloatParameter(0.9f, 0f, false);

		// Token: 0x04000399 RID: 921
		[Tooltip("Strength of the bloom filter.")]
		public MinFloatParameter intensity = new MinFloatParameter(0f, 0f, false);

		// Token: 0x0400039A RID: 922
		[Tooltip("Set the radius of the bloom effect.")]
		public ClampedFloatParameter scatter = new ClampedFloatParameter(0.7f, 0f, 1f, false);

		// Token: 0x0400039B RID: 923
		[Tooltip("Set the maximum intensity that Unity uses to calculate Bloom. If pixels in your Scene are more intense than this, URP renders them at their current intensity, but uses this intensity value for the purposes of Bloom calculations.")]
		public MinFloatParameter clamp = new MinFloatParameter(65472f, 0f, false);

		// Token: 0x0400039C RID: 924
		[Tooltip("Use the color picker to select a color for the Bloom effect to tint to.")]
		public ColorParameter tint = new ColorParameter(Color.white, false, false, true, false);

		// Token: 0x0400039D RID: 925
		[Tooltip("Use bicubic sampling instead of bilinear sampling for the upsampling passes. This is slightly more expensive but helps getting smoother visuals.")]
		public BoolParameter highQualityFiltering = new BoolParameter(false, false);

		// Token: 0x0400039E RID: 926
		[Tooltip("The starting resolution that this effect begins processing.")]
		[AdditionalProperty]
		public DownscaleParameter downscale = new DownscaleParameter(BloomDownscaleMode.Half, false);

		// Token: 0x0400039F RID: 927
		[Tooltip("The maximum number of iterations in the effect processing sequence.")]
		[AdditionalProperty]
		public ClampedIntParameter maxIterations = new ClampedIntParameter(6, 2, 8, false);

		// Token: 0x040003A0 RID: 928
		[Header("Lens Dirt")]
		[Tooltip("Dirtiness texture to add smudges or dust to the bloom effect.")]
		public TextureParameter dirtTexture = new TextureParameter(null, false);

		// Token: 0x040003A1 RID: 929
		[Tooltip("Amount of dirtiness.")]
		public MinFloatParameter dirtIntensity = new MinFloatParameter(0f, 0f, false);
	}
}
