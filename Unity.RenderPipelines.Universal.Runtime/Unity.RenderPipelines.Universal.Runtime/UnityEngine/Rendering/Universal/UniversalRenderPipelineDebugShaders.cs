using System;
using UnityEngine.Categorization;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200015B RID: 347
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[CategoryInfo(Name = "R: Debug Shaders", Order = 1000)]
	[HideInInspector]
	[Serializable]
	public class UniversalRenderPipelineDebugShaders : IRenderPipelineResources, IRenderPipelineGraphicsSettings
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x00002886 File Offset: 0x00000A86
		public int version
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00002886 File Offset: 0x00000A86
		bool IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x00024C6B File Offset: 0x00022E6B
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x00024C73 File Offset: 0x00022E73
		public Shader debugReplacementPS
		{
			get
			{
				return this.m_DebugReplacementPS;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_DebugReplacementPS, value, "m_DebugReplacementPS");
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x00024C87 File Offset: 0x00022E87
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x00024C8F File Offset: 0x00022E8F
		public Shader hdrDebugViewPS
		{
			get
			{
				return this.m_HdrDebugViewPS;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_HdrDebugViewPS, value, "m_HdrDebugViewPS");
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x00024CA3 File Offset: 0x00022EA3
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x00024CAB File Offset: 0x00022EAB
		public ComputeShader probeVolumeSamplingDebugComputeShader
		{
			get
			{
				return this.m_ProbeVolumeSamplingDebugComputeShader;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_ProbeVolumeSamplingDebugComputeShader, value, "m_ProbeVolumeSamplingDebugComputeShader");
			}
		}

		// Token: 0x04000803 RID: 2051
		[SerializeField]
		[ResourcePath("Shaders/Debug/DebugReplacement.shader", SearchType.ProjectPath)]
		private Shader m_DebugReplacementPS;

		// Token: 0x04000804 RID: 2052
		[SerializeField]
		[ResourcePath("Shaders/Debug/HDRDebugView.shader", SearchType.ProjectPath)]
		private Shader m_HdrDebugViewPS;

		// Token: 0x04000805 RID: 2053
		[SerializeField]
		[ResourcePath("Shaders/Debug/ProbeVolumeSamplingDebugPositionNormal.compute", SearchType.ProjectPath)]
		private ComputeShader m_ProbeVolumeSamplingDebugComputeShader;
	}
}
