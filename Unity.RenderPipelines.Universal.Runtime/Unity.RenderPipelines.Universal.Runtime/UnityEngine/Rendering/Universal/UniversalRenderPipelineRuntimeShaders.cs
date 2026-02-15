using System;
using UnityEngine.Categorization;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200015C RID: 348
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[CategoryInfo(Name = "R: Runtime Shaders", Order = 1000)]
	[HideInInspector]
	[Serializable]
	public class UniversalRenderPipelineRuntimeShaders : IRenderPipelineResources, IRenderPipelineGraphicsSettings
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00024CBF File Offset: 0x00022EBF
		public int version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x000039B4 File Offset: 0x00001BB4
		bool IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x00024CC7 File Offset: 0x00022EC7
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x00024CCF File Offset: 0x00022ECF
		public Shader fallbackErrorShader
		{
			get
			{
				return this.m_FallbackErrorShader;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_FallbackErrorShader, value, "m_FallbackErrorShader");
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00024CE3 File Offset: 0x00022EE3
		// (set) Token: 0x06000782 RID: 1922 RVA: 0x00024CEB File Offset: 0x00022EEB
		public Shader blitHDROverlay
		{
			get
			{
				return this.m_BlitHDROverlay;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_BlitHDROverlay, value, "m_BlitHDROverlay");
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x00024CFF File Offset: 0x00022EFF
		// (set) Token: 0x06000784 RID: 1924 RVA: 0x00024D07 File Offset: 0x00022F07
		public Shader coreBlitPS
		{
			get
			{
				return this.m_CoreBlitPS;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_CoreBlitPS, value, "m_CoreBlitPS");
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x00024D1B File Offset: 0x00022F1B
		// (set) Token: 0x06000786 RID: 1926 RVA: 0x00024D23 File Offset: 0x00022F23
		public Shader coreBlitColorAndDepthPS
		{
			get
			{
				return this.m_CoreBlitColorAndDepthPS;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_CoreBlitColorAndDepthPS, value, "m_CoreBlitColorAndDepthPS");
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x00024D37 File Offset: 0x00022F37
		// (set) Token: 0x06000788 RID: 1928 RVA: 0x00024D3F File Offset: 0x00022F3F
		public Shader samplingPS
		{
			get
			{
				return this.m_SamplingPS;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_SamplingPS, value, "m_SamplingPS");
			}
		}

		// Token: 0x04000806 RID: 2054
		[SerializeField]
		[HideInInspector]
		private int m_Version;

		// Token: 0x04000807 RID: 2055
		[SerializeField]
		[ResourcePath("Shaders/Utils/FallbackError.shader", SearchType.ProjectPath)]
		private Shader m_FallbackErrorShader;

		// Token: 0x04000808 RID: 2056
		[SerializeField]
		[ResourcePath("Shaders/Utils/BlitHDROverlay.shader", SearchType.ProjectPath)]
		internal Shader m_BlitHDROverlay;

		// Token: 0x04000809 RID: 2057
		[SerializeField]
		[ResourcePath("Shaders/Utils/CoreBlit.shader", SearchType.ProjectPath)]
		internal Shader m_CoreBlitPS;

		// Token: 0x0400080A RID: 2058
		[SerializeField]
		[ResourcePath("Shaders/Utils/CoreBlitColorAndDepth.shader", SearchType.ProjectPath)]
		internal Shader m_CoreBlitColorAndDepthPS;

		// Token: 0x0400080B RID: 2059
		[SerializeField]
		[ResourcePath("Shaders/Utils/Sampling.shader", SearchType.ProjectPath)]
		private Shader m_SamplingPS;
	}
}
