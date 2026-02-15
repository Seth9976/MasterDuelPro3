using System;
using UnityEngine.Categorization;

namespace UnityEngine.Rendering
{
	// Token: 0x02000178 RID: 376
	[SupportedOnRenderPipeline(new Type[] { })]
	[CategoryInfo(Name = "H: RP Assets Inclusion", Order = 990)]
	[HideInInspector]
	[Serializable]
	public class IncludeAdditionalRPAssets : IRenderPipelineGraphicsSettings
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x00027283 File Offset: 0x00025483
		int IRenderPipelineGraphicsSettings.version
		{
			get
			{
				return (int)this.m_version;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0002728B File Offset: 0x0002548B
		// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x00027293 File Offset: 0x00025493
		public bool includeReferencedInScenes
		{
			get
			{
				return this.m_IncludeReferencedInScenes;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_IncludeReferencedInScenes, value, "m_IncludeReferencedInScenes");
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x000272A7 File Offset: 0x000254A7
		// (set) Token: 0x06000AC9 RID: 2761 RVA: 0x000272AF File Offset: 0x000254AF
		public bool includeAssetsByLabel
		{
			get
			{
				return this.m_IncludeAssetsByLabel;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_IncludeAssetsByLabel, value, "m_IncludeAssetsByLabel");
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x000272C3 File Offset: 0x000254C3
		// (set) Token: 0x06000ACB RID: 2763 RVA: 0x000272CB File Offset: 0x000254CB
		public string labelToInclude
		{
			get
			{
				return this.m_LabelToInclude;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_LabelToInclude, value, "m_LabelToInclude");
			}
		}

		// Token: 0x04000753 RID: 1875
		[SerializeField]
		[HideInInspector]
		private IncludeAdditionalRPAssets.Version m_version;

		// Token: 0x04000754 RID: 1876
		[SerializeField]
		private bool m_IncludeReferencedInScenes;

		// Token: 0x04000755 RID: 1877
		[SerializeField]
		private bool m_IncludeAssetsByLabel;

		// Token: 0x04000756 RID: 1878
		[SerializeField]
		private string m_LabelToInclude;

		// Token: 0x02000179 RID: 377
		private enum Version
		{
			// Token: 0x04000758 RID: 1880
			Initial,
			// Token: 0x04000759 RID: 1881
			Count,
			// Token: 0x0400075A RID: 1882
			Last = 0
		}
	}
}
