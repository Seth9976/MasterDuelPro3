using System;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x02000446 RID: 1094
	public class PanelTextSettings : TextSettings
	{
		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x00073D4C File Offset: 0x00071F4C
		internal static PanelTextSettings defaultPanelTextSettings
		{
			get
			{
				PanelTextSettings.InitializeDefaultPanelTextSettingsIfNull();
				return PanelTextSettings.s_DefaultPanelTextSettings;
			}
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x00073D6C File Offset: 0x00071F6C
		internal static void InitializeDefaultPanelTextSettingsIfNull()
		{
			bool flag = PanelTextSettings.s_DefaultPanelTextSettings == null;
			if (flag)
			{
				PanelTextSettings.s_DefaultPanelTextSettings = ScriptableObject.CreateInstance<PanelTextSettings>();
			}
		}

		// Token: 0x04000E04 RID: 3588
		private static PanelTextSettings s_DefaultPanelTextSettings;
	}
}
