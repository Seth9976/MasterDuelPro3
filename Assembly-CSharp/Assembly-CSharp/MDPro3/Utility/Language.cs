using System;

namespace MDPro3.Utility
{
	// Token: 0x020012C0 RID: 4800
	public static class Language
	{
		// Token: 0x06008C6E RID: 35950 RVA: 0x001241F0 File Offset: 0x001223F0
		public static string GetConfig()
		{
			return Config.Get("Language", "zh-CN");
		}

		// Token: 0x06008C6F RID: 35951 RVA: 0x00124201 File Offset: 0x00122401
		public static void SetConfig(string language)
		{
			Config.Set("Language", language);
		}

		// Token: 0x06008C70 RID: 35952 RVA: 0x0012420E File Offset: 0x0012240E
		public static string GetCardConfig()
		{
			return Config.Get("CardLanguage", "zh-CN");
		}

		// Token: 0x06008C71 RID: 35953 RVA: 0x0012421F File Offset: 0x0012241F
		public static void SetCardConfig(string language)
		{
			Config.Set("CardLanguage", language);
		}

		// Token: 0x06008C72 RID: 35954 RVA: 0x0012422C File Offset: 0x0012242C
		public static string GetPrereleaseConfig()
		{
			return Config.Get("PrereleaseLanguage", "zh-CN");
		}

		// Token: 0x06008C73 RID: 35955 RVA: 0x0012423D File Offset: 0x0012243D
		public static void SetPrereleaseConfig(string language)
		{
			Config.Set("PrereleaseLanguage", language);
		}

		// Token: 0x06008C74 RID: 35956 RVA: 0x0012424C File Offset: 0x0012244C
		public static bool NeedBlankToAddWord()
		{
			string config = Language.GetConfig();
			return Language.UseLatin() || config == "ko-KR";
		}

		// Token: 0x06008C75 RID: 35957 RVA: 0x00124276 File Offset: 0x00122476
		public static string GetBlankIfNeed()
		{
			if (Language.NeedBlankToAddWord())
			{
				return " ";
			}
			return string.Empty;
		}

		// Token: 0x06008C76 RID: 35958 RVA: 0x0012428C File Offset: 0x0012248C
		public static bool UseChinese()
		{
			string language = Language.GetConfig();
			return language == "zh-CN" || language == "zh-TW";
		}

		// Token: 0x06008C77 RID: 35959 RVA: 0x001242BC File Offset: 0x001224BC
		public static bool UseLatin(string language)
		{
			return language == "en-US" || language == "es-ES" || language == "pt-PT" || language == "fr-FR" || language == "de-DE" || language == "it-IT";
		}

		// Token: 0x06008C78 RID: 35960 RVA: 0x0012431A File Offset: 0x0012251A
		public static bool UseLatin()
		{
			return Language.UseLatin(Language.GetConfig());
		}

		// Token: 0x06008C79 RID: 35961 RVA: 0x00124326 File Offset: 0x00122526
		public static bool CardUseLatin()
		{
			return Language.UseLatin(Language.GetCardConfig());
		}

		// Token: 0x06008C7A RID: 35962 RVA: 0x00124332 File Offset: 0x00122532
		public static bool NeedSmallBracket(string language)
		{
			return Language.UseLatin(language) || language == "ko-KR";
		}

		// Token: 0x06008C7B RID: 35963 RVA: 0x0012434C File Offset: 0x0012254C
		public static bool NeedSmallBracket()
		{
			return Language.NeedSmallBracket(Language.GetConfig());
		}

		// Token: 0x06008C7C RID: 35964 RVA: 0x00124358 File Offset: 0x00122558
		public static bool CardNeedSmallBracket()
		{
			return Language.NeedSmallBracket(Language.GetCardConfig());
		}

		// Token: 0x06008C7D RID: 35965 RVA: 0x00124364 File Offset: 0x00122564
		public static string GetLeftBracket()
		{
			if (Language.NeedSmallBracket())
			{
				return "[";
			}
			return "【";
		}

		// Token: 0x06008C7E RID: 35966 RVA: 0x00124378 File Offset: 0x00122578
		public static string GetRightBracket()
		{
			if (Language.NeedSmallBracket())
			{
				return "]";
			}
			return "】";
		}

		// Token: 0x06008C7F RID: 35967 RVA: 0x0012438C File Offset: 0x0012258C
		public static bool NeedSpSummonString(string language)
		{
			return !Language.UseLatin(language);
		}

		// Token: 0x06008C80 RID: 35968 RVA: 0x00124397 File Offset: 0x00122597
		public static string GetMasterDuelLanguage(string language)
		{
			if (language == "pt-PT")
			{
				return "pt-BR";
			}
			return language;
		}

		// Token: 0x06008C81 RID: 35969 RVA: 0x001243B0 File Offset: 0x001225B0
		public static bool AttributeNeedRuby()
		{
			string language = Language.GetCardConfig();
			return !(language == "zh-CN") && !(language == "zh-TW");
		}

		// Token: 0x0400CA61 RID: 51809
		public const string ConfigName = "Language";

		// Token: 0x0400CA62 RID: 51810
		public const string CardConfigName = "CardLanguage";

		// Token: 0x0400CA63 RID: 51811
		public const string PrereleaseName = "PrereleaseLanguage";

		// Token: 0x0400CA64 RID: 51812
		public const string English = "en-US";

		// Token: 0x0400CA65 RID: 51813
		public const string Spanish = "es-ES";

		// Token: 0x0400CA66 RID: 51814
		public const string French = "fr-FR";

		// Token: 0x0400CA67 RID: 51815
		public const string German = "de-DE";

		// Token: 0x0400CA68 RID: 51816
		public const string Italian = "it-IT";

		// Token: 0x0400CA69 RID: 51817
		public const string Japanese = "ja-JP";

		// Token: 0x0400CA6A RID: 51818
		public const string Korean = "ko-KR";

		// Token: 0x0400CA6B RID: 51819
		public const string Portuguese = "pt-PT";

		// Token: 0x0400CA6C RID: 51820
		public const string SimplifiedChinese = "zh-CN";

		// Token: 0x0400CA6D RID: 51821
		public const string TraditionalChinese = "zh-TW";

		// Token: 0x0400CA6E RID: 51822
		private const string MasterDuelPortuguese = "pt-BR";
	}
}
