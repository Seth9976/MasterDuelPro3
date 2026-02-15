using System;
using UnityEngine.Categorization;

namespace UnityEngine.Rendering
{
	// Token: 0x02000135 RID: 309
	[SupportedOnRenderPipeline(new Type[] { })]
	[CategoryInfo(Name = "Adaptive Probe Volumes", Order = 20)]
	[Serializable]
	internal class ProbeVolumeGlobalSettings : IRenderPipelineGraphicsSettings
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0001FF59 File Offset: 0x0001E159
		public int version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x0001FF61 File Offset: 0x0001E161
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x0001FF69 File Offset: 0x0001E169
		public bool probeVolumeDisableStreamingAssets
		{
			get
			{
				return this.m_ProbeVolumeDisableStreamingAssets;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_ProbeVolumeDisableStreamingAssets, value, "m_ProbeVolumeDisableStreamingAssets");
			}
		}

		// Token: 0x040005C4 RID: 1476
		[SerializeField]
		[HideInInspector]
		private int m_Version = 1;

		// Token: 0x040005C5 RID: 1477
		[SerializeField]
		[Tooltip("Enabling this will make APV baked data assets compatible with Addressables and Asset Bundles. This will also make Disk Streaming unavailable. After changing this setting, a clean rebuild may be required for data assets to be included in Adressables and Asset Bundles.")]
		private bool m_ProbeVolumeDisableStreamingAssets;
	}
}
