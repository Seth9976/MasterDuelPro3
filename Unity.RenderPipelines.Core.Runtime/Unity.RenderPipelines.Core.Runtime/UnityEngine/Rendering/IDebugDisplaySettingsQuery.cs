using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D4 RID: 212
	public interface IDebugDisplaySettingsQuery
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060006EA RID: 1770
		bool AreAnySettingsActive { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x000104EC File Offset: 0x0000E6EC
		bool IsPostProcessingAllowed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x000104EC File Offset: 0x0000E6EC
		bool IsLightingActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000090C6 File Offset: 0x000072C6
		bool TryGetScreenClearColor(ref Color color)
		{
			return false;
		}
	}
}
