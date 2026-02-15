using System;
using UnityEngine.Categorization;

namespace UnityEngine.Rendering
{
	// Token: 0x0200017B RID: 379
	[SupportedOnRenderPipeline(new Type[] { })]
	[CategoryInfo(Name = "Additional Shader Stripping Settings", Order = 40)]
	[ElementInfo(Order = 0)]
	[Serializable]
	public class ShaderStrippingSetting : IRenderPipelineGraphicsSettings
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x000272DF File Offset: 0x000254DF
		public int version
		{
			get
			{
				return (int)this.m_Version;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x000104EC File Offset: 0x0000E6EC
		bool IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x000272E7 File Offset: 0x000254E7
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x000272EF File Offset: 0x000254EF
		public bool exportShaderVariants
		{
			get
			{
				return this.m_ExportShaderVariants;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_ExportShaderVariants, value, "exportShaderVariants");
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00027303 File Offset: 0x00025503
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x0002730B File Offset: 0x0002550B
		public ShaderVariantLogLevel shaderVariantLogLevel
		{
			get
			{
				return this.m_ShaderVariantLogLevel;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_ShaderVariantLogLevel, value, "shaderVariantLogLevel");
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0002731F File Offset: 0x0002551F
		// (set) Token: 0x06000AD4 RID: 2772 RVA: 0x00027327 File Offset: 0x00025527
		public bool stripRuntimeDebugShaders
		{
			get
			{
				return this.m_StripRuntimeDebugShaders;
			}
			set
			{
				this.SetValueAndNotify(ref this.m_StripRuntimeDebugShaders, value, "stripRuntimeDebugShaders");
			}
		}

		// Token: 0x0400075F RID: 1887
		[SerializeField]
		[HideInInspector]
		private ShaderStrippingSetting.Version m_Version;

		// Token: 0x04000760 RID: 1888
		[SerializeField]
		[Tooltip("Controls whether to output shader variant information to a file.")]
		private bool m_ExportShaderVariants = true;

		// Token: 0x04000761 RID: 1889
		[SerializeField]
		[Tooltip("Controls the level of logging of shader variant information outputted during the build process. Information appears in the Unity Console when the build finishes.")]
		private ShaderVariantLogLevel m_ShaderVariantLogLevel;

		// Token: 0x04000762 RID: 1890
		[SerializeField]
		[Tooltip("When enabled, all debug display shader variants are removed when you build for the Unity Player. This decreases build time, but prevents the use of most Rendering Debugger features in Player builds.")]
		private bool m_StripRuntimeDebugShaders = true;

		// Token: 0x0200017C RID: 380
		internal enum Version
		{
			// Token: 0x04000764 RID: 1892
			Initial
		}
	}
}
