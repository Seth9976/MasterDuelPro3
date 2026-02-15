using System;
using System.Collections.Generic;
using System.Globalization;

namespace YgomSystem.Utility
{
	// Token: 0x0200052D RID: 1325
	public class Locale
	{
		// Token: 0x06002A7E RID: 10878 RVA: 0x0000216A File Offset: 0x0000036A
		private static string normalizeLanguage(string lang, bool useDefault)
		{
			return null;
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x0000216D File Offset: 0x0000036D
		private static void setupLang(List<object> langs, List<string> languages, List<string> readables)
		{
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x0000216D File Offset: 0x0000036D
		private static void setup()
		{
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<string> GetSupportedLanguages()
		{
			return null;
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsSupportedLanguage(string lang)
		{
			return false;
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<string> GetSupportedVoices()
		{
			return null;
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetLanguage(string lang)
		{
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetInitLanguage(string lang)
		{
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetVoice()
		{
			return null;
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetLanguage()
		{
			return null;
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x0000216A File Offset: 0x0000036A
		public static CultureInfo GetCultureInfo()
		{
			return null;
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetReadableLanguage(string lang)
		{
			return null;
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<string> GetReadableLanguages()
		{
			return null;
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetCurrentReadableLanguage()
		{
			return null;
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<string> GetReadableVoices()
		{
			return null;
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetReadableVoice(string lang)
		{
			return null;
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool EnableVoices()
		{
			return false;
		}

		// Token: 0x040029A5 RID: 10661
		public const string Japanese = "ja-JP";

		// Token: 0x040029A6 RID: 10662
		public const string English = "en-US";

		// Token: 0x040029A7 RID: 10663
		public const string French = "fr-FR";

		// Token: 0x040029A8 RID: 10664
		public const string Italian = "it-IT";

		// Token: 0x040029A9 RID: 10665
		public const string German = "de-DE";

		// Token: 0x040029AA RID: 10666
		public const string Spanish = "es-ES";

		// Token: 0x040029AB RID: 10667
		public const string Portuguese = "pt-BR";

		// Token: 0x040029AC RID: 10668
		public const string Korean = "ko-KR";

		// Token: 0x040029AD RID: 10669
		public const string TCH = "zh-TW";

		// Token: 0x040029AE RID: 10670
		public const string SCH = "zh-CN";

		// Token: 0x040029AF RID: 10671
		public const string DefaultLanguage = "en-US";

		// Token: 0x040029B0 RID: 10672
		private const string DefaultLanguageName = "English";

		// Token: 0x040029B1 RID: 10673
		private static string language;

		// Token: 0x040029B2 RID: 10674
		private static CultureInfo cultureInfo;

		// Token: 0x040029B3 RID: 10675
		private static List<string> supportedLanguages;

		// Token: 0x040029B4 RID: 10676
		private static List<string> supportedReadableLanguages;

		// Token: 0x040029B5 RID: 10677
		private static List<string> supportedVoices;

		// Token: 0x040029B6 RID: 10678
		private static List<string> supportedReadableVoices;
	}
}
