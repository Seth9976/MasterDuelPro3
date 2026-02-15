using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000125 RID: 293
	[Serializable]
	internal struct ProbeDilationSettings
	{
		// Token: 0x0600098A RID: 2442 RVA: 0x0001E3FE File Offset: 0x0001C5FE
		internal void SetDefaults()
		{
			this.enableDilation = false;
			this.dilationDistance = 1f;
			this.dilationValidityThreshold = 0.25f;
			this.dilationIterations = 1;
			this.squaredDistWeighting = true;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00005704 File Offset: 0x00003904
		internal void UpgradeFromTo(ProbeVolumeBakingProcessSettings.SettingsVersion from, ProbeVolumeBakingProcessSettings.SettingsVersion to)
		{
		}

		// Token: 0x04000542 RID: 1346
		public bool enableDilation;

		// Token: 0x04000543 RID: 1347
		public float dilationDistance;

		// Token: 0x04000544 RID: 1348
		public float dilationValidityThreshold;

		// Token: 0x04000545 RID: 1349
		public int dilationIterations;

		// Token: 0x04000546 RID: 1350
		public bool squaredDistWeighting;
	}
}
