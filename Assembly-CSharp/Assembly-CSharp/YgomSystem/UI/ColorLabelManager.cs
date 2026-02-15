using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000586 RID: 1414
	public static class ColorLabelManager
	{
		// Token: 0x06002CC3 RID: 11459 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Setup()
		{
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000F1EF0 File Offset: 0x000F00F0
		public static Color GetColor(string label)
		{
			return default(Color);
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x000F1F06 File Offset: 0x000F0106
		public static bool TryGetColor(string label, out Color result)
		{
			result = default(Color);
			return false;
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x000F1F10 File Offset: 0x000F0110
		public static Color GetColor(ColorLabel label)
		{
			return default(Color);
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x000029CC File Offset: 0x00000BCC
		public static ColorLabel LabelStringToEnum(string label)
		{
			return ColorLabel.None;
		}

		// Token: 0x04002B0A RID: 11018
		private static readonly string assetPath;

		// Token: 0x04002B0B RID: 11019
		private static ColorLabelSetting s_setting;

		// Token: 0x04002B0C RID: 11020
		private static Dictionary<string, ColorLabel> s_labelStrToEnumMap;

		// Token: 0x04002B0D RID: 11021
		private static bool s_initalized;
	}
}
