using System;
using UnityEngine.Categorization;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000188 RID: 392
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[CategoryInfo(Name = "Volume", Order = 0)]
	[Serializable]
	public class URPDefaultVolumeProfileSettings : IDefaultVolumeProfileSettings, IRenderPipelineGraphicsSettings
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00027FB9 File Offset: 0x000261B9
		public int version
		{
			get
			{
				return (int)this.m_Version;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00027FC1 File Offset: 0x000261C1
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x00027FC9 File Offset: 0x000261C9
		public VolumeProfile volumeProfile
		{
			get
			{
				return this.m_VolumeProfile;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_VolumeProfile, value, "volumeProfile");
			}
		}

		// Token: 0x040008C6 RID: 2246
		[SerializeField]
		[HideInInspector]
		private URPDefaultVolumeProfileSettings.Version m_Version;

		// Token: 0x040008C7 RID: 2247
		[SerializeField]
		private VolumeProfile m_VolumeProfile;

		// Token: 0x02000189 RID: 393
		internal enum Version
		{
			// Token: 0x040008C9 RID: 2249
			Initial
		}
	}
}
