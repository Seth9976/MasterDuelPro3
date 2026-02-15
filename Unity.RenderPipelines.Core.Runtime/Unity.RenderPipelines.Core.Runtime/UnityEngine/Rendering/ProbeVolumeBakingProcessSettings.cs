using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000127 RID: 295
	[Serializable]
	internal struct ProbeVolumeBakingProcessSettings
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x0001E480 File Offset: 0x0001C680
		internal static ProbeVolumeBakingProcessSettings Default
		{
			get
			{
				ProbeVolumeBakingProcessSettings s = default(ProbeVolumeBakingProcessSettings);
				s.SetDefaults();
				return s;
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001E49D File Offset: 0x0001C69D
		internal ProbeVolumeBakingProcessSettings(ProbeDilationSettings dilationSettings, VirtualOffsetSettings virtualOffsetSettings)
		{
			this.m_Version = ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset;
			this.dilationSettings = dilationSettings;
			this.virtualOffsetSettings = virtualOffsetSettings;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001E4B4 File Offset: 0x0001C6B4
		internal void SetDefaults()
		{
			this.m_Version = ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset;
			this.dilationSettings.SetDefaults();
			this.virtualOffsetSettings.SetDefaults();
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001E4D3 File Offset: 0x0001C6D3
		internal void Upgrade()
		{
			if (this.m_Version != ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset)
			{
				this.dilationSettings.UpgradeFromTo(this.m_Version, ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset);
				this.virtualOffsetSettings.UpgradeFromTo(this.m_Version, ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset);
				this.m_Version = ProbeVolumeBakingProcessSettings.SettingsVersion.ThreadedVirtualOffset;
			}
		}

		// Token: 0x0400054D RID: 1357
		[SerializeField]
		private ProbeVolumeBakingProcessSettings.SettingsVersion m_Version;

		// Token: 0x0400054E RID: 1358
		public ProbeDilationSettings dilationSettings;

		// Token: 0x0400054F RID: 1359
		public VirtualOffsetSettings virtualOffsetSettings;

		// Token: 0x02000128 RID: 296
		internal enum SettingsVersion
		{
			// Token: 0x04000551 RID: 1361
			Initial,
			// Token: 0x04000552 RID: 1362
			ThreadedVirtualOffset,
			// Token: 0x04000553 RID: 1363
			Max,
			// Token: 0x04000554 RID: 1364
			Current = 1
		}
	}
}
