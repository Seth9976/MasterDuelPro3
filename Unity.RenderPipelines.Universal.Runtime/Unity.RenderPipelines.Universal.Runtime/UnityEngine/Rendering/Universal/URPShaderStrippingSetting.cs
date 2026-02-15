using System;
using UnityEngine.Categorization;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200018A RID: 394
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[CategoryInfo(Name = "Additional Shader Stripping Settings", Order = 40)]
	[ElementInfo(Order = 10)]
	[Serializable]
	public class URPShaderStrippingSetting : IRenderPipelineGraphicsSettings
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00027FDD File Offset: 0x000261DD
		public int version
		{
			get
			{
				return (int)this.m_Version;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x00027FE5 File Offset: 0x000261E5
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x00027FED File Offset: 0x000261ED
		public bool stripUnusedPostProcessingVariants
		{
			get
			{
				return this.m_StripUnusedPostProcessingVariants;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_StripUnusedPostProcessingVariants, value, "stripUnusedPostProcessingVariants");
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x00028001 File Offset: 0x00026201
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x00028009 File Offset: 0x00026209
		public bool stripUnusedVariants
		{
			get
			{
				return this.m_StripUnusedVariants;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_StripUnusedVariants, value, "stripUnusedVariants");
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0002801D File Offset: 0x0002621D
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x00028025 File Offset: 0x00026225
		public bool stripScreenCoordOverrideVariants
		{
			get
			{
				return this.m_StripScreenCoordOverrideVariants;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_StripScreenCoordOverrideVariants, value, "stripScreenCoordOverrideVariants");
			}
		}

		// Token: 0x040008CA RID: 2250
		[SerializeField]
		[HideInInspector]
		private URPShaderStrippingSetting.Version m_Version;

		// Token: 0x040008CB RID: 2251
		[SerializeField]
		[Tooltip("Controls whether to automatically strip post processing shader variants based on VolumeProfile components. Stripping is done based on VolumeProfiles in project, their usage in scenes is not considered.")]
		private bool m_StripUnusedPostProcessingVariants;

		// Token: 0x040008CC RID: 2252
		[SerializeField]
		[Tooltip("Controls whether to strip variants if the feature is disabled.")]
		private bool m_StripUnusedVariants = true;

		// Token: 0x040008CD RID: 2253
		[SerializeField]
		[Tooltip("Controls whether Screen Coordinates Override shader variants are automatically stripped.")]
		private bool m_StripScreenCoordOverrideVariants = true;

		// Token: 0x0200018B RID: 395
		internal enum Version
		{
			// Token: 0x040008CF RID: 2255
			Initial
		}
	}
}
