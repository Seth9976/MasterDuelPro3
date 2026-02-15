using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000EB RID: 235
	[VolumeComponentMenu("Post-processing/Depth Of Field")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[Serializable]
	public sealed class DepthOfField : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060005E5 RID: 1509 RVA: 0x0001643A File Offset: 0x0001463A
		public bool IsActive()
		{
			return this.mode.value != DepthOfFieldMode.Off && SystemInfo.graphicsShaderLevel >= 35 && (this.mode.value != DepthOfFieldMode.Gaussian || SystemInfo.supportedRenderTargetCount > 1);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00002886 File Offset: 0x00000A86
		[Obsolete("Unused #from(2023.1)", false)]
		public bool IsTileCompatible()
		{
			return false;
		}

		// Token: 0x04000519 RID: 1305
		[Tooltip("Use \"Gaussian\" for a faster but non physical depth of field; \"Bokeh\" for a more realistic but slower depth of field.")]
		public DepthOfFieldModeParameter mode = new DepthOfFieldModeParameter(DepthOfFieldMode.Off, false);

		// Token: 0x0400051A RID: 1306
		[Tooltip("The distance at which the blurring will start.")]
		public MinFloatParameter gaussianStart = new MinFloatParameter(10f, 0f, false);

		// Token: 0x0400051B RID: 1307
		[Tooltip("The distance at which the blurring will reach its maximum radius.")]
		public MinFloatParameter gaussianEnd = new MinFloatParameter(30f, 0f, false);

		// Token: 0x0400051C RID: 1308
		[Tooltip("The maximum radius of the gaussian blur. Values above 1 may show under-sampling artifacts.")]
		public ClampedFloatParameter gaussianMaxRadius = new ClampedFloatParameter(1f, 0.5f, 1.5f, false);

		// Token: 0x0400051D RID: 1309
		[Tooltip("Use higher quality sampling to reduce flickering and improve the overall blur smoothness.")]
		public BoolParameter highQualitySampling = new BoolParameter(false, false);

		// Token: 0x0400051E RID: 1310
		[Tooltip("The distance to the point of focus.")]
		public MinFloatParameter focusDistance = new MinFloatParameter(10f, 0.1f, false);

		// Token: 0x0400051F RID: 1311
		[Tooltip("The ratio of aperture (known as f-stop or f-number). The smaller the value is, the shallower the depth of field is.")]
		public ClampedFloatParameter aperture = new ClampedFloatParameter(5.6f, 1f, 32f, false);

		// Token: 0x04000520 RID: 1312
		[Tooltip("The distance between the lens and the film. The larger the value is, the shallower the depth of field is.")]
		public ClampedFloatParameter focalLength = new ClampedFloatParameter(50f, 1f, 300f, false);

		// Token: 0x04000521 RID: 1313
		[Tooltip("The number of aperture blades.")]
		public ClampedIntParameter bladeCount = new ClampedIntParameter(5, 3, 9, false);

		// Token: 0x04000522 RID: 1314
		[Tooltip("The curvature of aperture blades. The smaller the value is, the more visible aperture blades are. A value of 1 will make the bokeh perfectly circular.")]
		public ClampedFloatParameter bladeCurvature = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x04000523 RID: 1315
		[Tooltip("The rotation of aperture blades in degrees.")]
		public ClampedFloatParameter bladeRotation = new ClampedFloatParameter(0f, -180f, 180f, false);
	}
}
