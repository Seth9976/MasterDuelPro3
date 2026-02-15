using System;
using UnityEngine.Categorization;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200015D RID: 349
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[CategoryInfo(Name = "R: Runtime Textures", Order = 1000)]
	[HideInInspector]
	[Serializable]
	public class UniversalRenderPipelineRuntimeTextures : IRenderPipelineResources, IRenderPipelineGraphicsSettings
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00024D53 File Offset: 0x00022F53
		public int version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x000039B4 File Offset: 0x00001BB4
		bool IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00024D5B File Offset: 0x00022F5B
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x00024D63 File Offset: 0x00022F63
		public Texture2D blueNoise64LTex
		{
			get
			{
				return this.m_BlueNoise64LTex;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_BlueNoise64LTex, value, "m_BlueNoise64LTex");
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x00024D77 File Offset: 0x00022F77
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x00024D7F File Offset: 0x00022F7F
		public Texture2D bayerMatrixTex
		{
			get
			{
				return this.m_BayerMatrixTex;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_BayerMatrixTex, value, "m_BayerMatrixTex");
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x00024D93 File Offset: 0x00022F93
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x00024D9B File Offset: 0x00022F9B
		public Texture2D debugFontTexture
		{
			get
			{
				return this.m_DebugFontTex;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_DebugFontTex, value, "m_DebugFontTex");
			}
		}

		// Token: 0x0400080C RID: 2060
		[SerializeField]
		[HideInInspector]
		private int m_Version = 1;

		// Token: 0x0400080D RID: 2061
		[SerializeField]
		[ResourcePath("Textures/BlueNoise64/L/LDR_LLL1_0.png", SearchType.ProjectPath)]
		private Texture2D m_BlueNoise64LTex;

		// Token: 0x0400080E RID: 2062
		[SerializeField]
		[ResourcePath("Textures/BayerMatrix.png", SearchType.ProjectPath)]
		private Texture2D m_BayerMatrixTex;

		// Token: 0x0400080F RID: 2063
		[SerializeField]
		[ResourcePath("Textures/DebugFont.tga", SearchType.ProjectPath)]
		private Texture2D m_DebugFontTex;
	}
}
