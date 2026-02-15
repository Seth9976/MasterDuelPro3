using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000E7 RID: 231
	[VolumeComponentMenu("Post-processing/Color Adjustments")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[Serializable]
	public sealed class ColorAdjustments : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060005DB RID: 1499 RVA: 0x00015FB0 File Offset: 0x000141B0
		public bool IsActive()
		{
			return this.postExposure.value != 0f || this.contrast.value != 0f || this.colorFilter != Color.white || this.hueShift != 0f || this.saturation != 0f;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000039B4 File Offset: 0x00001BB4
		[Obsolete("Unused #from(2023.1)", false)]
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x04000506 RID: 1286
		[Tooltip("Adjusts the overall exposure of the scene in EV100. This is applied after HDR effect and right before tonemapping so it won't affect previous effects in the chain.")]
		public FloatParameter postExposure = new FloatParameter(0f, false);

		// Token: 0x04000507 RID: 1287
		[Tooltip("Expands or shrinks the overall range of tonal values.")]
		public ClampedFloatParameter contrast = new ClampedFloatParameter(0f, -100f, 100f, false);

		// Token: 0x04000508 RID: 1288
		[Tooltip("Tint the render by multiplying a color.")]
		public ColorParameter colorFilter = new ColorParameter(Color.white, true, false, true, false);

		// Token: 0x04000509 RID: 1289
		[Tooltip("Shift the hue of all colors.")]
		public ClampedFloatParameter hueShift = new ClampedFloatParameter(0f, -180f, 180f, false);

		// Token: 0x0400050A RID: 1290
		[Tooltip("Pushes the intensity of all colors.")]
		public ClampedFloatParameter saturation = new ClampedFloatParameter(0f, -100f, 100f, false);
	}
}
