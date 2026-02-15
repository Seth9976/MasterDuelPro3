using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000FB RID: 251
	[VolumeComponentMenu("Post-processing/Shadows, Midtones, Highlights")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[Serializable]
	public sealed class ShadowsMidtonesHighlights : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x06000600 RID: 1536 RVA: 0x00016AA8 File Offset: 0x00014CA8
		public bool IsActive()
		{
			Vector4 defaultState = new Vector4(1f, 1f, 1f, 0f);
			return this.shadows != defaultState || this.midtones != defaultState || this.highlights != defaultState;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000039B4 File Offset: 0x00001BB4
		[Obsolete("Unused #from(2023.1)", false)]
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x0400055F RID: 1375
		public Vector4Parameter shadows = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);

		// Token: 0x04000560 RID: 1376
		public Vector4Parameter midtones = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);

		// Token: 0x04000561 RID: 1377
		public Vector4Parameter highlights = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);

		// Token: 0x04000562 RID: 1378
		[Header("Shadow Limits")]
		[Tooltip("Start point of the transition between shadows and midtones.")]
		public MinFloatParameter shadowsStart = new MinFloatParameter(0f, 0f, false);

		// Token: 0x04000563 RID: 1379
		[Tooltip("End point of the transition between shadows and midtones.")]
		public MinFloatParameter shadowsEnd = new MinFloatParameter(0.3f, 0f, false);

		// Token: 0x04000564 RID: 1380
		[Header("Highlight Limits")]
		[Tooltip("Start point of the transition between midtones and highlights.")]
		public MinFloatParameter highlightsStart = new MinFloatParameter(0.55f, 0f, false);

		// Token: 0x04000565 RID: 1381
		[Tooltip("End point of the transition between midtones and highlights.")]
		public MinFloatParameter highlightsEnd = new MinFloatParameter(1f, 0f, false);
	}
}
