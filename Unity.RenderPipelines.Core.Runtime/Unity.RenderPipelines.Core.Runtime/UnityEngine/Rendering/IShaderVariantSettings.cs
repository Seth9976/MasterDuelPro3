using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E4 RID: 228
	[Obsolete("Use GraphicsSettings.GetRenderPipelineSettings<ShaderStrippingSetting>(). #from(23.3)")]
	public interface IShaderVariantSettings
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000764 RID: 1892
		// (set) Token: 0x06000765 RID: 1893
		ShaderVariantLogLevel shaderVariantLogLevel { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000766 RID: 1894
		// (set) Token: 0x06000767 RID: 1895
		bool exportShaderVariants { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x000090C6 File Offset: 0x000072C6
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x00005704 File Offset: 0x00003904
		bool stripDebugVariants
		{
			get
			{
				return false;
			}
			set
			{
			}
		}
	}
}
