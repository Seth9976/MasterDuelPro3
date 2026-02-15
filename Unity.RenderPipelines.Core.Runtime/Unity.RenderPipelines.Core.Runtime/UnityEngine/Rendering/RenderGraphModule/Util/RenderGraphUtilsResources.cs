using System;
using System.ComponentModel;

namespace UnityEngine.Rendering.RenderGraphModule.Util
{
	// Token: 0x02000281 RID: 641
	[HideInInspector]
	[Category("Resources/Render Graph Helper Function Resources")]
	[SupportedOnRenderPipeline(new Type[] { })]
	[Serializable]
	internal class RenderGraphUtilsResources : IRenderPipelineResources, IRenderPipelineGraphicsSettings
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x0003F0B9 File Offset: 0x0003D2B9
		int IRenderPipelineGraphicsSettings.version
		{
			get
			{
				return (int)this.m_Version;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x0003F0C1 File Offset: 0x0003D2C1
		// (set) Token: 0x06001163 RID: 4451 RVA: 0x0003F0C9 File Offset: 0x0003D2C9
		public Shader coreCopyPS
		{
			get
			{
				return this.m_CoreCopyPS;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_CoreCopyPS, value, "m_CoreCopyPS");
			}
		}

		// Token: 0x04000B37 RID: 2871
		[SerializeField]
		[HideInInspector]
		private RenderGraphUtilsResources.Version m_Version;

		// Token: 0x04000B38 RID: 2872
		[SerializeField]
		[ResourcePath("Shaders/CoreCopy.shader", SearchType.ProjectPath)]
		internal Shader m_CoreCopyPS;

		// Token: 0x02000282 RID: 642
		public enum Version
		{
			// Token: 0x04000B3A RID: 2874
			Initial,
			// Token: 0x04000B3B RID: 2875
			Count,
			// Token: 0x04000B3C RID: 2876
			Latest = 0
		}
	}
}
