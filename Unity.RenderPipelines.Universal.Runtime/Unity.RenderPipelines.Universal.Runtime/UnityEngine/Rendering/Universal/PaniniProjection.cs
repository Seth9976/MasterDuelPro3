using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000F7 RID: 247
	[VolumeComponentMenu("Post-processing/Panini Projection")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[Serializable]
	public sealed class PaniniProjection : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060005F8 RID: 1528 RVA: 0x0001686F File Offset: 0x00014A6F
		public bool IsActive()
		{
			return this.distance.value > 0f;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00002886 File Offset: 0x00000A86
		[Obsolete("Unused #from(2023.1)", false)]
		public bool IsTileCompatible()
		{
			return false;
		}

		// Token: 0x04000547 RID: 1351
		[Tooltip("Panini projection distance.")]
		public ClampedFloatParameter distance = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x04000548 RID: 1352
		[Tooltip("Panini projection crop to fit.")]
		public ClampedFloatParameter cropToFit = new ClampedFloatParameter(1f, 0f, 1f, false);
	}
}
