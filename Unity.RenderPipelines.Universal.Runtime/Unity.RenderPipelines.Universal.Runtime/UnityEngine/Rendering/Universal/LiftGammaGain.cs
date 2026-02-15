using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000F1 RID: 241
	[VolumeComponentMenu("Post-processing/Lift, Gamma, Gain")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[Serializable]
	public sealed class LiftGammaGain : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x0001670C File Offset: 0x0001490C
		public bool IsActive()
		{
			Vector4 defaultState = new Vector4(1f, 1f, 1f, 0f);
			return this.lift != defaultState || this.gamma != defaultState || this.gain != defaultState;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000039B4 File Offset: 0x00001BB4
		[Obsolete("Unused #from(2023.1)", false)]
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x04000539 RID: 1337
		public Vector4Parameter lift = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);

		// Token: 0x0400053A RID: 1338
		public Vector4Parameter gamma = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);

		// Token: 0x0400053B RID: 1339
		public Vector4Parameter gain = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);
	}
}
