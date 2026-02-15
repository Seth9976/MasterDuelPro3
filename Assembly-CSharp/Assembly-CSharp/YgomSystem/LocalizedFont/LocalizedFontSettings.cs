using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.LocalizedFont
{
	// Token: 0x02000734 RID: 1844
	public abstract class LocalizedFontSettings<T_FONT> : LocalizedFontSettingsBase where T_FONT : global::UnityEngine.Object
	{
		// Token: 0x06003965 RID: 14693 RVA: 0x0000216A File Offset: 0x0000036A
		public LocalizedFontSettings<T_FONT>.LocalizedFontLocaleEntry GetLocalizedFontLocaleEntry(LocalizedFontSettingsBase.FontType fontType, LocalizedFontSettingsBase.Locale locale)
		{
			return null;
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x000F3634 File Offset: 0x000F1834
		public ValueTuple<LocalizedFontSettingsBase.FontType, LocalizedFontSettings<T_FONT>.LocalizedFontLocaleEntry> GetEntryFromOtherFontName(string otherFontName)
		{
			return default(ValueTuple<LocalizedFontSettingsBase.FontType, LocalizedFontSettings<T_FONT>.LocalizedFontLocaleEntry>);
		}

		// Token: 0x06003967 RID: 14695 RVA: 0x000029CC File Offset: 0x00000BCC
		public LocalizedFontSettingsBase.FontType GetFontTypeFromOtherFontName(string otherFontName)
		{
			return LocalizedFontSettingsBase.FontType.Other;
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateLocaleCache()
		{
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x000029CC File Offset: 0x00000BCC
		public LocalizedFontSettingsBase.Locale GetCurrentLocale(bool updateCache = true)
		{
			return LocalizedFontSettingsBase.Locale.Other;
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x0000216A File Offset: 0x0000036A
		private Material _getFontMaterial(LocalizedFontSettingsBase.FontType fontType, LocalizedFontSettings<T_FONT>.LoadedFontEntry lfe, Material mat)
		{
			return null;
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x000F364C File Offset: 0x000F184C
		public ValueTuple<T_FONT, Material> GetLocalized(LocalizedFontSettingsBase.FontType fontType, int materialIndex, LocalizedFontSettingsBase.Locale locale)
		{
			return default(ValueTuple<T_FONT, Material>);
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x000F3664 File Offset: 0x000F1864
		public ValueTuple<T_FONT, Material> GetLocalized(LocalizedFontSettingsBase.FontType fontType, int materialIndex)
		{
			return default(ValueTuple<T_FONT, Material>);
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000F367C File Offset: 0x000F187C
		public T_FONT GetLocalizedFont(LocalizedFontSettingsBase.FontType fontType, LocalizedFontSettingsBase.Locale locale)
		{
			return default(T_FONT);
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x000F3694 File Offset: 0x000F1894
		public T_FONT GetLocalizedFont(LocalizedFontSettingsBase.FontType fontType)
		{
			return default(T_FONT);
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadFonts()
		{
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsFontsLoaded()
		{
			return false;
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x0000216A File Offset: 0x0000036A
		private LocalizedFontSettings<T_FONT>.LoadedFontEntry _getLocalizedFontEntry(LocalizedFontSettingsBase.FontType fontType, LocalizedFontSettings<T_FONT>.LocalizedFontLocaleEntry lfle)
		{
			return null;
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000F36AC File Offset: 0x000F18AC
		public ValueTuple<T_FONT, Material> ToLocalized(T_FONT font, Material mat)
		{
			return default(ValueTuple<T_FONT, Material>);
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x000F36C4 File Offset: 0x000F18C4
		public T_FONT ToLocalizedFont(T_FONT font)
		{
			return default(T_FONT);
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearFontCache()
		{
		}

		// Token: 0x06003975 RID: 14709
		protected abstract string GetFontName(T_FONT font);

		// Token: 0x06003976 RID: 14710 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual Material GetFontMaterial(T_FONT font)
		{
			return null;
		}

		// Token: 0x040033E3 RID: 13283
		[SerializeField]
		private LocalizedFontSettings<T_FONT>.LocalizedFontEntry[] localizedFonts;

		// Token: 0x040033E4 RID: 13284
		private Dictionary<LocalizedFontSettingsBase.FontType, LocalizedFontSettings<T_FONT>.LoadedFontEntry> m_loadedFonts;

		// Token: 0x040033E5 RID: 13285
		private bool m_localeInitialized;

		// Token: 0x040033E6 RID: 13286
		private LocalizedFontSettingsBase.Locale m_localeCache;

		// Token: 0x040033E7 RID: 13287
		public bool raiseErrorOnLoadImmediateFont;

		// Token: 0x040033E8 RID: 13288
		private LocalizedFontSettingsBase.Locale m_oldLocale;

		// Token: 0x040033E9 RID: 13289
		private Dictionary<LocalizedFontSettingsBase.FontType, LocalizedFontSettings<T_FONT>.LocalizedFontLocaleEntry> m_otherFonts;

		// Token: 0x040033EA RID: 13290
		private Dictionary<string, LocalizedFontSettings<T_FONT>.LoadedFontEntry> m_fontCache;

		// Token: 0x02000735 RID: 1845
		[Serializable]
		public class LocalizedFontMaterialEntry
		{
			// Token: 0x040033EB RID: 13291
			public string path;

			// Token: 0x040033EC RID: 13292
			public string name;
		}

		// Token: 0x02000736 RID: 1846
		[Serializable]
		public class LocalizedFontLocaleEntry
		{
			// Token: 0x040033ED RID: 13293
			public LocalizedFontSettingsBase.Locale locale;

			// Token: 0x040033EE RID: 13294
			public string fontPath;

			// Token: 0x040033EF RID: 13295
			public string fontName;

			// Token: 0x040033F0 RID: 13296
			public LocalizedFontSettings<T_FONT>.LocalizedFontMaterialEntry[] materials;
		}

		// Token: 0x02000737 RID: 1847
		[Serializable]
		public class LocalizedFontEntry
		{
			// Token: 0x040033F1 RID: 13297
			public LocalizedFontSettingsBase.FontType fontType;

			// Token: 0x040033F2 RID: 13298
			public LocalizedFontSettings<T_FONT>.LocalizedFontLocaleEntry[] locales;
		}

		// Token: 0x02000738 RID: 1848
		private class LoadedFontEntry
		{
			// Token: 0x040033F3 RID: 13299
			public string fontPath;

			// Token: 0x040033F4 RID: 13300
			public T_FONT font;

			// Token: 0x040033F5 RID: 13301
			public Material defaultMaterial;

			// Token: 0x040033F6 RID: 13302
			public Material[] materials;
		}
	}
}
