using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000EE RID: 238
	[VolumeComponentMenu("Post-processing/Film Grain")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[Serializable]
	public sealed class FilmGrain : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x0001657C File Offset: 0x0001477C
		public bool IsActive()
		{
			return this.intensity.value > 0f && (this.type.value != FilmGrainLookup.Custom || this.texture.value != null);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000039B4 File Offset: 0x00001BB4
		[Obsolete("Unused #from(2023.1)", false)]
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x04000530 RID: 1328
		[Tooltip("The type of grain to use. You can select a preset or provide your own texture by selecting Custom.")]
		public FilmGrainLookupParameter type = new FilmGrainLookupParameter(FilmGrainLookup.Thin1, false);

		// Token: 0x04000531 RID: 1329
		[Tooltip("Use the slider to set the strength of the Film Grain effect.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x04000532 RID: 1330
		[Tooltip("Controls the noisiness response curve based on scene luminance. Higher values mean less noise in light areas.")]
		public ClampedFloatParameter response = new ClampedFloatParameter(0.8f, 0f, 1f, false);

		// Token: 0x04000533 RID: 1331
		[Tooltip("A tileable texture to use for the grain. The neutral value is 0.5 where no grain is applied.")]
		public NoInterpTextureParameter texture = new NoInterpTextureParameter(null, false);
	}
}
