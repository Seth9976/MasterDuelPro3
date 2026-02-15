using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000126 RID: 294
	[Serializable]
	internal struct VirtualOffsetSettings
	{
		// Token: 0x0600098C RID: 2444 RVA: 0x0001E42B File Offset: 0x0001C62B
		internal void SetDefaults()
		{
			this.useVirtualOffset = true;
			this.validityThreshold = 0.25f;
			this.outOfGeoOffset = 0.01f;
			this.searchMultiplier = 0.2f;
			this.UpgradeFromTo(ProbeVolumeBakingProcessSettings.SettingsVersion.Initial, ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001E45D File Offset: 0x0001C65D
		internal void UpgradeFromTo(ProbeVolumeBakingProcessSettings.SettingsVersion from, ProbeVolumeBakingProcessSettings.SettingsVersion to)
		{
			if (from < ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset && to >= ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset)
			{
				this.rayOriginBias = -0.001f;
				this.collisionMask = -5;
			}
		}

		// Token: 0x04000547 RID: 1351
		public bool useVirtualOffset;

		// Token: 0x04000548 RID: 1352
		[Range(0f, 0.95f)]
		public float validityThreshold;

		// Token: 0x04000549 RID: 1353
		[Range(0f, 1f)]
		public float outOfGeoOffset;

		// Token: 0x0400054A RID: 1354
		[Range(0f, 2f)]
		public float searchMultiplier;

		// Token: 0x0400054B RID: 1355
		[Range(-0.05f, 0f)]
		public float rayOriginBias;

		// Token: 0x0400054C RID: 1356
		public LayerMask collisionMask;
	}
}
