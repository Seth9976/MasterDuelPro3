using System;
using UnityEngine.Categorization;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000186 RID: 390
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[CategoryInfo(Name = "Render Graph", Order = 50)]
	[ElementInfo(Order = -10)]
	[Serializable]
	public class RenderGraphSettings : IRenderPipelineGraphicsSettings
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00027F89 File Offset: 0x00026189
		public int version
		{
			get
			{
				return (int)this.m_Version;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x000039B4 File Offset: 0x00001BB4
		bool IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00027F91 File Offset: 0x00026191
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x00027FA5 File Offset: 0x000261A5
		public bool enableRenderCompatibilityMode
		{
			get
			{
				return this.m_EnableRenderCompatibilityMode && !RenderGraphGraphicsAutomatedTests.enabled;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_EnableRenderCompatibilityMode, value, "m_EnableRenderCompatibilityMode");
			}
		}

		// Token: 0x040008C2 RID: 2242
		[SerializeField]
		[HideInInspector]
		private RenderGraphSettings.Version m_Version;

		// Token: 0x040008C3 RID: 2243
		[SerializeField]
		[Tooltip("When enabled, URP does not use the Render Graph API to construct and execute the frame. Use this option only for compatibility purposes.")]
		[RecreatePipelineOnChange]
		private bool m_EnableRenderCompatibilityMode;

		// Token: 0x02000187 RID: 391
		internal enum Version
		{
			// Token: 0x040008C5 RID: 2245
			Initial
		}
	}
}
