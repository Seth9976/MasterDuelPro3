using System;
using TMPro;
using UnityEngine;

namespace YgomSystem.LocalizedFont
{
	// Token: 0x0200073C RID: 1852
	public class LocalizedTMPFontSettings : LocalizedFontSettings<TMP_FontAsset>
	{
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600397D RID: 14717 RVA: 0x0000216A File Offset: 0x0000036A
		public static LocalizedTMPFontSettings Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string GetFontName(TMP_FontAsset font)
		{
			return null;
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Material GetFontMaterial(TMP_FontAsset font)
		{
			return null;
		}

		// Token: 0x04003404 RID: 13316
		private const string SETTING_PATH = "ScriptableObjects/LocalizedFont/LocalizedTMPFontSettings";

		// Token: 0x04003405 RID: 13317
		private static LocalizedTMPFontSettings s_instance;
	}
}
