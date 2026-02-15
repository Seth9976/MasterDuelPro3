using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000104 RID: 260
	[VolumeComponentMenu("Post-processing/Vignette")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[Serializable]
	public sealed class Vignette : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x0600060C RID: 1548 RVA: 0x00016D42 File Offset: 0x00014F42
		public bool IsActive()
		{
			return this.intensity.value > 0f;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000039B4 File Offset: 0x00001BB4
		[Obsolete("Unused #from(2023.1)", false)]
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x0400057D RID: 1405
		[Tooltip("Vignette color.")]
		public ColorParameter color = new ColorParameter(Color.black, false, false, true, false);

		// Token: 0x0400057E RID: 1406
		[Tooltip("Sets the vignette center point (screen center is [0.5,0.5]).")]
		public Vector2Parameter center = new Vector2Parameter(new Vector2(0.5f, 0.5f), false);

		// Token: 0x0400057F RID: 1407
		[Tooltip("Use the slider to set the strength of the Vignette effect.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x04000580 RID: 1408
		[Tooltip("Smoothness of the vignette borders.")]
		public ClampedFloatParameter smoothness = new ClampedFloatParameter(0.2f, 0.01f, 1f, false);

		// Token: 0x04000581 RID: 1409
		[Tooltip("Should the vignette be perfectly round or be dependent on the current aspect ratio?")]
		public BoolParameter rounded = new BoolParameter(false, false);
	}
}
