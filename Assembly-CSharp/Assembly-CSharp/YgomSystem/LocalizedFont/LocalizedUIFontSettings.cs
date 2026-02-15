using System;
using UnityEngine;

namespace YgomSystem.LocalizedFont
{
	// Token: 0x0200073D RID: 1853
	public class LocalizedUIFontSettings : LocalizedFontSettings<Font>
	{
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06003981 RID: 14721 RVA: 0x0000216A File Offset: 0x0000036A
		public static LocalizedUIFontSettings Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string GetFontName(Font font)
		{
			return null;
		}

		// Token: 0x04003406 RID: 13318
		private const string SETTING_PATH = "ScriptableObjects/LocalizedFont/LocalizedUIFontSettings";

		// Token: 0x04003407 RID: 13319
		private static LocalizedUIFontSettings s_instance;
	}
}
