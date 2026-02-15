using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000100 RID: 256
	[VolumeComponentMenu("Post-processing/Tonemapping")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[Serializable]
	public sealed class Tonemapping : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x00016C52 File Offset: 0x00014E52
		public bool IsActive()
		{
			return this.mode.value > TonemappingMode.None;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000039B4 File Offset: 0x00001BB4
		[Obsolete("Unused #from(2023.1)", false)]
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x04000574 RID: 1396
		[Tooltip("Select a tonemapping algorithm to use for the color grading process.")]
		public TonemappingModeParameter mode = new TonemappingModeParameter(TonemappingMode.None, false);

		// Token: 0x04000575 RID: 1397
		[AdditionalProperty]
		[Tooltip("Specifies the range reduction mode used when HDR output is enabled and Neutral tonemapping is enabled.")]
		public NeutralRangeReductionModeParameter neutralHDRRangeReductionMode = new NeutralRangeReductionModeParameter(NeutralRangeReductionMode.BT2390, false);

		// Token: 0x04000576 RID: 1398
		[Tooltip("Use the ACES preset for HDR displays.")]
		public HDRACESPresetParameter acesPreset = new HDRACESPresetParameter(HDRACESPreset.ACES1000Nits, false);

		// Token: 0x04000577 RID: 1399
		[Tooltip("Specify how much hue to preserve. Values closer to 0 are likely to preserve hue. As values get closer to 1, Unity doesn't correct hue shifts.")]
		public ClampedFloatParameter hueShiftAmount = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x04000578 RID: 1400
		[Tooltip("Enable to use values detected from the output device as paper white. When enabled, output images might differ between SDR and HDR. For best accuracy, set this value manually.")]
		public BoolParameter detectPaperWhite = new BoolParameter(false, false);

		// Token: 0x04000579 RID: 1401
		[Tooltip("The reference brightness of a paper white surface. This property determines the maximum brightness of UI. The brightness of the scene is scaled relative to this value. The value is in nits.")]
		public ClampedFloatParameter paperWhite = new ClampedFloatParameter(300f, 0f, 400f, false);

		// Token: 0x0400057A RID: 1402
		[Tooltip("Enable to use the minimum and maximum brightness values detected from the output device. For best accuracy, considering calibrating these values manually.")]
		public BoolParameter detectBrightnessLimits = new BoolParameter(true, false);

		// Token: 0x0400057B RID: 1403
		[Tooltip("The minimum brightness of the screen (in nits). This value is assumed to be 0.005f with ACES Tonemap.")]
		public ClampedFloatParameter minNits = new ClampedFloatParameter(0.005f, 0f, 50f, false);

		// Token: 0x0400057C RID: 1404
		[Tooltip("The maximum brightness of the screen (in nits). This value is defined by the preset when using ACES Tonemap.")]
		public ClampedFloatParameter maxNits = new ClampedFloatParameter(1000f, 0f, 5000f, false);
	}
}
