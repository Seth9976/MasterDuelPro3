using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000F9 RID: 249
	[VolumeComponentMenu("Post-processing/Screen Space Lens Flare")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[Serializable]
	public class ScreenSpaceLensFlare : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x000168C4 File Offset: 0x00014AC4
		public ScreenSpaceLensFlare()
		{
			base.displayName = "Screen Space Lens Flare";
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00016A75 File Offset: 0x00014C75
		public bool IsActive()
		{
			return this.intensity.value > 0f;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00016A89 File Offset: 0x00014C89
		public bool IsStreaksActive()
		{
			return this.streaksIntensity.value > 0f;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00002886 File Offset: 0x00000A86
		[Obsolete("Unused #from(2023.1)", false)]
		public bool IsTileCompatible()
		{
			return false;
		}

		// Token: 0x0400054D RID: 1357
		public MinFloatParameter intensity = new MinFloatParameter(0f, 0f, false);

		// Token: 0x0400054E RID: 1358
		public ColorParameter tintColor = new ColorParameter(Color.white, false);

		// Token: 0x0400054F RID: 1359
		[AdditionalProperty]
		public ClampedIntParameter bloomMip = new ClampedIntParameter(1, 0, 5, false);

		// Token: 0x04000550 RID: 1360
		[Header("Flares")]
		public MinFloatParameter firstFlareIntensity = new MinFloatParameter(1f, 0f, false);

		// Token: 0x04000551 RID: 1361
		public MinFloatParameter secondaryFlareIntensity = new MinFloatParameter(1f, 0f, false);

		// Token: 0x04000552 RID: 1362
		public MinFloatParameter warpedFlareIntensity = new MinFloatParameter(1f, 0f, false);

		// Token: 0x04000553 RID: 1363
		[AdditionalProperty]
		public Vector2Parameter warpedFlareScale = new Vector2Parameter(new Vector2(1f, 1f), false);

		// Token: 0x04000554 RID: 1364
		public ClampedIntParameter samples = new ClampedIntParameter(1, 1, 3, false);

		// Token: 0x04000555 RID: 1365
		[AdditionalProperty]
		public ClampedFloatParameter sampleDimmer = new ClampedFloatParameter(0.5f, 0.1f, 1f, false);

		// Token: 0x04000556 RID: 1366
		public ClampedFloatParameter vignetteEffect = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x04000557 RID: 1367
		public ClampedFloatParameter startingPosition = new ClampedFloatParameter(1.25f, 1f, 3f, false);

		// Token: 0x04000558 RID: 1368
		public ClampedFloatParameter scale = new ClampedFloatParameter(1.5f, 1f, 4f, false);

		// Token: 0x04000559 RID: 1369
		[Header("Streaks")]
		public MinFloatParameter streaksIntensity = new MinFloatParameter(0f, 0f, false);

		// Token: 0x0400055A RID: 1370
		public ClampedFloatParameter streaksLength = new ClampedFloatParameter(0.5f, 0f, 1f, false);

		// Token: 0x0400055B RID: 1371
		public FloatParameter streaksOrientation = new FloatParameter(0f, false);

		// Token: 0x0400055C RID: 1372
		public ClampedFloatParameter streaksThreshold = new ClampedFloatParameter(0.25f, 0f, 1f, false);

		// Token: 0x0400055D RID: 1373
		[SerializeField]
		[AdditionalProperty]
		public ScreenSpaceLensFlareResolutionParameter resolution = new ScreenSpaceLensFlareResolutionParameter(ScreenSpaceLensFlareResolution.Quarter, false);

		// Token: 0x0400055E RID: 1374
		[Header("Chromatic Abberation")]
		public ClampedFloatParameter chromaticAbberationIntensity = new ClampedFloatParameter(0.5f, 0f, 1f, false);
	}
}
