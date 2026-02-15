using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using MDPro3.Utility;
using UnityEngine;

namespace MDPro3
{
	// Token: 0x0200123B RID: 4667
	public static class InterString
	{
		// Token: 0x060089E6 RID: 35302 RVA: 0x0010E060 File Offset: 0x0010C260
		public static void Initialize()
		{
			InterString.translations.Clear();
			InterString.translationsForRender.Clear();
			InterString.translationsForPrerelease.Clear();
			InterString.path = "Data/locales/" + Language.GetConfig() + "/translation.conf";
			if (!File.Exists(InterString.path))
			{
				File.Create(InterString.path).Close();
			}
			InterString.InitializeContent(File.ReadAllText(InterString.path), 0);
			if (Language.GetCardConfig() == Language.GetConfig())
			{
				InterString.pathForRender = InterString.path;
				InterString.translationsForRender = InterString.translations;
			}
			else
			{
				InterString.pathForRender = "Data/locales/" + Language.GetCardConfig() + "/translation.conf";
				if (!File.Exists(InterString.pathForRender))
				{
					File.Create(InterString.pathForRender).Close();
				}
				InterString.InitializeContent(File.ReadAllText(InterString.pathForRender), 1);
			}
			if (Language.GetPrereleaseConfig() == Language.GetConfig())
			{
				InterString.pathForPrerelease = InterString.path;
				InterString.translationsForPrerelease = InterString.translations;
				return;
			}
			if (Language.GetPrereleaseConfig() == Language.GetCardConfig())
			{
				InterString.pathForPrerelease = InterString.pathForRender;
				InterString.translationsForPrerelease = InterString.translationsForRender;
				return;
			}
			InterString.pathForPrerelease = "Data/locales/" + Language.GetPrereleaseConfig() + "/translation.conf";
			if (!File.Exists(InterString.pathForPrerelease))
			{
				File.Create(InterString.pathForPrerelease).Close();
			}
			InterString.InitializeContent(File.ReadAllText(InterString.pathForPrerelease), 2);
		}

		// Token: 0x060089E7 RID: 35303 RVA: 0x0010E1CC File Offset: 0x0010C3CC
		private static void InitializeContent(string text, int type)
		{
			Dictionary<string, string> dic = InterString.GetTranslations(type);
			string[] lines = text.Replace("\r", string.Empty).Split('\n', StringSplitOptions.None);
			for (int i = 0; i < lines.Length; i++)
			{
				string[] mats = Regex.Split(lines[i], "->");
				if (mats.Length == 2 && !dic.ContainsKey(mats[0]))
				{
					dic.Add(mats[0], mats[1]);
				}
			}
		}

		// Token: 0x060089E8 RID: 35304 RVA: 0x0010E234 File Offset: 0x0010C434
		public static string Get(string original, int type = 0)
		{
			string returnValue = original;
			Dictionary<string, string> targetTranslations = InterString.GetTranslations(type);
			if (targetTranslations.TryGetValue(original, out returnValue))
			{
				return returnValue.Replace("@n", "\r\n").Replace("@ui", string.Empty);
			}
			if (original != string.Empty)
			{
				Debug.Log(string.Format("Undefined translation {0}: {1}", targetTranslations.Count, original));
				try
				{
					File.AppendAllText(InterString.GetSavePath(type), original + "->" + original + "\r\n");
				}
				catch
				{
					Program.noAccess = true;
				}
				targetTranslations.Add(original, original);
				return original.Replace("@n", "\r\n").Replace("@ui", string.Empty);
			}
			return original;
		}

		// Token: 0x060089E9 RID: 35305 RVA: 0x0010E300 File Offset: 0x0010C500
		public static string Get(string original, string replace, int type = 0)
		{
			return InterString.Get(original, type).Replace("[?]", replace);
		}

		// Token: 0x060089EA RID: 35306 RVA: 0x0010E314 File Offset: 0x0010C514
		public static string GetOriginal(string value)
		{
			string returnValue = value;
			foreach (KeyValuePair<string, string> translation in InterString.translations)
			{
				if (translation.Value == value)
				{
					returnValue = translation.Key;
					break;
				}
			}
			return returnValue;
		}

		// Token: 0x060089EB RID: 35307 RVA: 0x0010E37C File Offset: 0x0010C57C
		private static Dictionary<string, string> GetTranslations(int type)
		{
			Dictionary<string, string> dictionary;
			if (type != 1)
			{
				if (type != 2)
				{
					dictionary = InterString.translations;
				}
				else
				{
					dictionary = InterString.translationsForPrerelease;
				}
			}
			else
			{
				dictionary = InterString.translationsForRender;
			}
			return dictionary;
		}

		// Token: 0x060089EC RID: 35308 RVA: 0x0010E3AC File Offset: 0x0010C5AC
		private static string GetSavePath(int type)
		{
			string text;
			if (type != 1)
			{
				if (type != 2)
				{
					text = InterString.path;
				}
				else
				{
					text = InterString.pathForPrerelease;
				}
			}
			else
			{
				text = InterString.pathForRender;
			}
			return text;
		}

		// Token: 0x0400C50D RID: 50445
		public const string CONFIG_LINE_BREAK = "@n";

		// Token: 0x0400C50E RID: 50446
		public const string SYSTEM_LINE_BREAK = "\r\n";

		// Token: 0x0400C50F RID: 50447
		private const string SEPARATOR = "->";

		// Token: 0x0400C510 RID: 50448
		private const string STRING_EMPTY = "@ui";

		// Token: 0x0400C511 RID: 50449
		private const string PATH_CONF_FILE = "/translation.conf";

		// Token: 0x0400C512 RID: 50450
		private static readonly Dictionary<string, string> translations = new Dictionary<string, string>();

		// Token: 0x0400C513 RID: 50451
		private static Dictionary<string, string> translationsForRender = new Dictionary<string, string>();

		// Token: 0x0400C514 RID: 50452
		private static Dictionary<string, string> translationsForPrerelease = new Dictionary<string, string>();

		// Token: 0x0400C515 RID: 50453
		private static string path;

		// Token: 0x0400C516 RID: 50454
		private static string pathForRender;

		// Token: 0x0400C517 RID: 50455
		private static string pathForPrerelease;
	}
}
