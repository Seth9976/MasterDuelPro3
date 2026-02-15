using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000105 RID: 261
	[VolumeComponentMenu("Post-processing/White Balance")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[Serializable]
	public sealed class WhiteBalance : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x00016DDD File Offset: 0x00014FDD
		public bool IsActive()
		{
			return this.temperature.value != 0f || this.tint.value != 0f;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000039B4 File Offset: 0x00001BB4
		[Obsolete("Unused #from(2023.1)", false)]
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x04000582 RID: 1410
		[Tooltip("Sets the white balance to a custom color temperature.")]
		public ClampedFloatParameter temperature = new ClampedFloatParameter(0f, -100f, 100f, false);

		// Token: 0x04000583 RID: 1411
		[Tooltip("Sets the white balance to compensate for a green or magenta tint.")]
		public ClampedFloatParameter tint = new ClampedFloatParameter(0f, -100f, 100f, false);
	}
}
