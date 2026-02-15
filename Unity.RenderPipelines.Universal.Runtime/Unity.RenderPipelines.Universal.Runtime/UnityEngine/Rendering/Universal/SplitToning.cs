using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000FC RID: 252
	[VolumeComponentMenu("Post-processing/Split Toning")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[Serializable]
	public sealed class SplitToning : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x00016BD6 File Offset: 0x00014DD6
		public bool IsActive()
		{
			return this.shadows != Color.grey || this.highlights != Color.grey;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000039B4 File Offset: 0x00001BB4
		[Obsolete("Unused #from(2023.1)", false)]
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x04000566 RID: 1382
		[Tooltip("The color to use for shadows.")]
		public ColorParameter shadows = new ColorParameter(Color.grey, false, false, true, false);

		// Token: 0x04000567 RID: 1383
		[Tooltip("The color to use for highlights.")]
		public ColorParameter highlights = new ColorParameter(Color.grey, false, false, true, false);

		// Token: 0x04000568 RID: 1384
		[Tooltip("Balance between the colors in the highlights and shadows.")]
		public ClampedFloatParameter balance = new ClampedFloatParameter(0f, -100f, 100f, false);
	}
}
