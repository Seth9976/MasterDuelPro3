using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000F0 RID: 240
	[VolumeComponentMenu("Post-processing/Lens Distortion")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[Serializable]
	public sealed class LensDistortion : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x00016624 File Offset: 0x00014824
		public bool IsActive()
		{
			return Mathf.Abs(this.intensity.value) > 0f && (this.xMultiplier.value > 0f || this.yMultiplier.value > 0f);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00002886 File Offset: 0x00000A86
		[Obsolete("Unused #from(2023.1)", false)]
		public bool IsTileCompatible()
		{
			return false;
		}

		// Token: 0x04000534 RID: 1332
		[Tooltip("Total distortion amount.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, -1f, 1f, false);

		// Token: 0x04000535 RID: 1333
		[Tooltip("Intensity multiplier on X axis. Set it to 0 to disable distortion on this axis.")]
		public ClampedFloatParameter xMultiplier = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x04000536 RID: 1334
		[Tooltip("Intensity multiplier on Y axis. Set it to 0 to disable distortion on this axis.")]
		public ClampedFloatParameter yMultiplier = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x04000537 RID: 1335
		[Tooltip("Distortion center point. 0.5,0.5 is center of the screen.")]
		public Vector2Parameter center = new Vector2Parameter(new Vector2(0.5f, 0.5f), false);

		// Token: 0x04000538 RID: 1336
		[Tooltip("Controls global screen scaling for the distortion effect. Use this to hide the screen borders when using high \"Intensity.\"")]
		public ClampedFloatParameter scale = new ClampedFloatParameter(1f, 0.01f, 5f, false);
	}
}
