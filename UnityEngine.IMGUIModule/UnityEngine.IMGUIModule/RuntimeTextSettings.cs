using System;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;

namespace UnityEngine
{
	// Token: 0x02000027 RID: 39
	internal class RuntimeTextSettings : TextSettings
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00008BA4 File Offset: 0x00006DA4
		internal static RuntimeTextSettings defaultTextSettings
		{
			get
			{
				bool flag = RuntimeTextSettings.s_DefaultTextSettings == null;
				if (flag)
				{
					RuntimeTextSettings.s_DefaultTextSettings = ScriptableObject.CreateInstance<RuntimeTextSettings>();
				}
				return RuntimeTextSettings.s_DefaultTextSettings;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008BD8 File Offset: 0x00006DD8
		internal override Shader GetFontShader()
		{
			return TextShaderUtilities.ShaderRef_MobileSDF_IMGUI;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008BF0 File Offset: 0x00006DF0
		internal override List<FontAsset> GetStaticFallbackOSFontAsset()
		{
			return RuntimeTextSettings.s_FallbackOSFontAssetIMGUIInternal;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00008C07 File Offset: 0x00006E07
		internal override void SetStaticFallbackOSFontAsset(List<FontAsset> fontAssets)
		{
			RuntimeTextSettings.s_FallbackOSFontAssetIMGUIInternal = fontAssets;
		}

		// Token: 0x040000F7 RID: 247
		private static RuntimeTextSettings s_DefaultTextSettings;

		// Token: 0x040000F8 RID: 248
		private static List<FontAsset> s_FallbackOSFontAssetIMGUIInternal;
	}
}
