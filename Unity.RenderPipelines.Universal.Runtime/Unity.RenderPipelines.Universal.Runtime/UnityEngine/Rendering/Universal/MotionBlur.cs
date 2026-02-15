using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000F4 RID: 244
	[VolumeComponentMenu("Post-processing/Motion Blur")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[Serializable]
	public sealed class MotionBlur : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x000167E2 File Offset: 0x000149E2
		public bool IsActive()
		{
			return this.intensity.value > 0f;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00002886 File Offset: 0x00000A86
		[Obsolete("Unused #from(2023.1)", false)]
		public bool IsTileCompatible()
		{
			return false;
		}

		// Token: 0x04000543 RID: 1347
		[Tooltip("The motion blur technique to use. If you don't need object motion blur, CameraOnly will result in better performance.")]
		public MotionBlurModeParameter mode = new MotionBlurModeParameter(MotionBlurMode.CameraOnly, false);

		// Token: 0x04000544 RID: 1348
		[Tooltip("The quality of the effect. Lower presets will result in better performance at the expense of visual quality.")]
		public MotionBlurQualityParameter quality = new MotionBlurQualityParameter(MotionBlurQuality.Low, false);

		// Token: 0x04000545 RID: 1349
		[Tooltip("The strength of the motion blur filter. Acts as a multiplier for velocities.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x04000546 RID: 1350
		[Tooltip("Sets the maximum length, as a fraction of the screen's full resolution, that the velocity resulting from Camera rotation can have. Lower values will improve performance.")]
		public ClampedFloatParameter clamp = new ClampedFloatParameter(0.05f, 0f, 0.2f, false);
	}
}
