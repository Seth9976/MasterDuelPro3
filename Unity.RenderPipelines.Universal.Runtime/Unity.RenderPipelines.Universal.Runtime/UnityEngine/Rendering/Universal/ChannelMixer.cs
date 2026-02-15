using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000E5 RID: 229
	[VolumeComponentMenu("Post-processing/Channel Mixer")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[Serializable]
	public sealed class ChannelMixer : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060005D5 RID: 1493 RVA: 0x00015DB4 File Offset: 0x00013FB4
		public bool IsActive()
		{
			return this.redOutRedIn.value != 100f || this.redOutGreenIn.value != 0f || this.redOutBlueIn.value != 0f || this.greenOutRedIn.value != 0f || this.greenOutGreenIn.value != 100f || this.greenOutBlueIn.value != 0f || this.blueOutRedIn.value != 0f || this.blueOutGreenIn.value != 0f || this.blueOutBlueIn.value != 100f;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000039B4 File Offset: 0x00001BB4
		[Obsolete("Unused #from(2023.1)", false)]
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x040004FC RID: 1276
		[Tooltip("Modify influence of the red channel in the overall mix.")]
		public ClampedFloatParameter redOutRedIn = new ClampedFloatParameter(100f, -200f, 200f, false);

		// Token: 0x040004FD RID: 1277
		[Tooltip("Modify influence of the green channel in the overall mix.")]
		public ClampedFloatParameter redOutGreenIn = new ClampedFloatParameter(0f, -200f, 200f, false);

		// Token: 0x040004FE RID: 1278
		[Tooltip("Modify influence of the blue channel in the overall mix.")]
		public ClampedFloatParameter redOutBlueIn = new ClampedFloatParameter(0f, -200f, 200f, false);

		// Token: 0x040004FF RID: 1279
		[Tooltip("Modify influence of the red channel in the overall mix.")]
		public ClampedFloatParameter greenOutRedIn = new ClampedFloatParameter(0f, -200f, 200f, false);

		// Token: 0x04000500 RID: 1280
		[Tooltip("Modify influence of the green channel in the overall mix.")]
		public ClampedFloatParameter greenOutGreenIn = new ClampedFloatParameter(100f, -200f, 200f, false);

		// Token: 0x04000501 RID: 1281
		[Tooltip("Modify influence of the blue channel in the overall mix.")]
		public ClampedFloatParameter greenOutBlueIn = new ClampedFloatParameter(0f, -200f, 200f, false);

		// Token: 0x04000502 RID: 1282
		[Tooltip("Modify influence of the red channel in the overall mix.")]
		public ClampedFloatParameter blueOutRedIn = new ClampedFloatParameter(0f, -200f, 200f, false);

		// Token: 0x04000503 RID: 1283
		[Tooltip("Modify influence of the green channel in the overall mix.")]
		public ClampedFloatParameter blueOutGreenIn = new ClampedFloatParameter(0f, -200f, 200f, false);

		// Token: 0x04000504 RID: 1284
		[Tooltip("Modify influence of the blue channel in the overall mix.")]
		public ClampedFloatParameter blueOutBlueIn = new ClampedFloatParameter(100f, -200f, 200f, false);
	}
}
