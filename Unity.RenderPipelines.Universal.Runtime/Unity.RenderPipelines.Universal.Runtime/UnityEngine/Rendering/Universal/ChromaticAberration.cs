using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000E6 RID: 230
	[VolumeComponentMenu("Post-processing/Chromatic Aberration")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[Serializable]
	public sealed class ChromaticAberration : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060005D8 RID: 1496 RVA: 0x00015F76 File Offset: 0x00014176
		public bool IsActive()
		{
			return this.intensity.value > 0f;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00002886 File Offset: 0x00000A86
		[Obsolete("Unused #from(2023.1)", false)]
		public bool IsTileCompatible()
		{
			return false;
		}

		// Token: 0x04000505 RID: 1285
		[Tooltip("Use the slider to set the strength of the Chromatic Aberration effect.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f, false);
	}
}
