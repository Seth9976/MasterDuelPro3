using System;
using UnityEngine.Categorization;

namespace UnityEngine.Rendering
{
	// Token: 0x0200015C RID: 348
	[SupportedOnRenderPipeline(new Type[] { })]
	[CategoryInfo(Name = "Render Graph", Order = 50)]
	[ElementInfo(Order = 0)]
	[Serializable]
	public class RenderGraphGlobalSettings : IRenderPipelineGraphicsSettings
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x000104EC File Offset: 0x0000E6EC
		bool IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00024E60 File Offset: 0x00023060
		int IRenderPipelineGraphicsSettings.version
		{
			get
			{
				return (int)this.m_version;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00024E68 File Offset: 0x00023068
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x00024E70 File Offset: 0x00023070
		public bool enableCompilationCaching
		{
			get
			{
				return this.m_EnableCompilationCaching;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_EnableCompilationCaching, value, "enableCompilationCaching");
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00024E84 File Offset: 0x00023084
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x00024E8C File Offset: 0x0002308C
		public bool enableValidityChecks
		{
			get
			{
				return this.m_EnableValidityChecks;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_EnableValidityChecks, value, "enableValidityChecks");
			}
		}

		// Token: 0x040006BB RID: 1723
		[SerializeField]
		[HideInInspector]
		private RenderGraphGlobalSettings.Version m_version;

		// Token: 0x040006BC RID: 1724
		[RecreatePipelineOnChange]
		[SerializeField]
		[Tooltip("Enable caching of render graph compilation from one frame to another.")]
		private bool m_EnableCompilationCaching = true;

		// Token: 0x040006BD RID: 1725
		[RecreatePipelineOnChange]
		[SerializeField]
		[Tooltip("Enable validity checks of render graph in Editor and Development mode. Always disabled in Release build.")]
		private bool m_EnableValidityChecks = true;

		// Token: 0x0200015D RID: 349
		private enum Version
		{
			// Token: 0x040006BF RID: 1727
			Initial,
			// Token: 0x040006C0 RID: 1728
			Count,
			// Token: 0x040006C1 RID: 1729
			Last = 0
		}
	}
}
