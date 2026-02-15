using System;
using UnityEngine.Categorization;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200015A RID: 346
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[CategoryInfo(Name = "R: 2D Renderer", Order = 1000)]
	[HideInInspector]
	[Serializable]
	internal class Renderer2DResources : IRenderPipelineResources, IRenderPipelineGraphicsSettings
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00024B83 File Offset: 0x00022D83
		public int version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x000039B4 File Offset: 0x00001BB4
		bool IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00024B8B File Offset: 0x00022D8B
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x00024B93 File Offset: 0x00022D93
		internal Shader lightShader
		{
			get
			{
				return this.m_LightShader;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_LightShader, value, "m_LightShader");
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x00024BA7 File Offset: 0x00022DA7
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x00024BAF File Offset: 0x00022DAF
		internal Shader projectedShadowShader
		{
			get
			{
				return this.m_ProjectedShadowShader;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_ProjectedShadowShader, value, "m_ProjectedShadowShader");
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x00024BC3 File Offset: 0x00022DC3
		// (set) Token: 0x06000768 RID: 1896 RVA: 0x00024BCB File Offset: 0x00022DCB
		internal Shader spriteShadowShader
		{
			get
			{
				return this.m_SpriteShadowShader;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_SpriteShadowShader, value, "m_SpriteShadowShader");
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x00024BDF File Offset: 0x00022DDF
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x00024BE7 File Offset: 0x00022DE7
		internal Shader spriteUnshadowShader
		{
			get
			{
				return this.m_SpriteUnshadowShader;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_SpriteUnshadowShader, value, "m_SpriteUnshadowShader");
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x00024BFB File Offset: 0x00022DFB
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x00024C03 File Offset: 0x00022E03
		internal Shader geometryShadowShader
		{
			get
			{
				return this.m_GeometryShadowShader;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_GeometryShadowShader, value, "m_GeometryShadowShader");
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x00024C17 File Offset: 0x00022E17
		// (set) Token: 0x0600076E RID: 1902 RVA: 0x00024C1F File Offset: 0x00022E1F
		internal Shader geometryUnshadowShader
		{
			get
			{
				return this.m_GeometryUnshadowShader;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_GeometryUnshadowShader, value, "m_GeometryUnshadowShader");
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x00024C33 File Offset: 0x00022E33
		// (set) Token: 0x06000770 RID: 1904 RVA: 0x00024C3B File Offset: 0x00022E3B
		internal Texture2D fallOffLookup
		{
			get
			{
				return this.m_FallOffLookup;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_FallOffLookup, value, "m_FallOffLookup");
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00024C4F File Offset: 0x00022E4F
		// (set) Token: 0x06000772 RID: 1906 RVA: 0x00024C57 File Offset: 0x00022E57
		internal Shader copyDepthPS
		{
			get
			{
				return this.m_CopyDepthPS;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_CopyDepthPS, value, "m_CopyDepthPS");
			}
		}

		// Token: 0x040007FA RID: 2042
		[SerializeField]
		[HideInInspector]
		private int m_Version;

		// Token: 0x040007FB RID: 2043
		[SerializeField]
		[ResourcePath("Shaders/2D/Light2D.shader", SearchType.ProjectPath)]
		private Shader m_LightShader;

		// Token: 0x040007FC RID: 2044
		[SerializeField]
		[ResourcePath("Shaders/2D/Shadow2D-Projected.shader", SearchType.ProjectPath)]
		private Shader m_ProjectedShadowShader;

		// Token: 0x040007FD RID: 2045
		[SerializeField]
		[ResourcePath("Shaders/2D/Shadow2D-Shadow-Sprite.shader", SearchType.ProjectPath)]
		private Shader m_SpriteShadowShader;

		// Token: 0x040007FE RID: 2046
		[SerializeField]
		[ResourcePath("Shaders/2D/Shadow2D-Unshadow-Sprite.shader", SearchType.ProjectPath)]
		private Shader m_SpriteUnshadowShader;

		// Token: 0x040007FF RID: 2047
		[SerializeField]
		[ResourcePath("Shaders/2D/Shadow2D-Shadow-Geometry.shader", SearchType.ProjectPath)]
		private Shader m_GeometryShadowShader;

		// Token: 0x04000800 RID: 2048
		[SerializeField]
		[ResourcePath("Shaders/2D/Shadow2D-Unshadow-Geometry.shader", SearchType.ProjectPath)]
		private Shader m_GeometryUnshadowShader;

		// Token: 0x04000801 RID: 2049
		[SerializeField]
		[ResourcePath("Runtime/2D/Data/Textures/FalloffLookupTexture.png", SearchType.ProjectPath)]
		[HideInInspector]
		private Texture2D m_FallOffLookup;

		// Token: 0x04000802 RID: 2050
		[SerializeField]
		[ResourcePath("Shaders/Utils/CopyDepth.shader", SearchType.ProjectPath)]
		private Shader m_CopyDepthPS;
	}
}
