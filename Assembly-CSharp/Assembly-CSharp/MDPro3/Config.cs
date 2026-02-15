using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using MDPro3.Utility;
using UnityEngine;

namespace MDPro3
{
	// Token: 0x02001239 RID: 4665
	public static class Config
	{
		// Token: 0x060089D8 RID: 35288 RVA: 0x0010DB08 File Offset: 0x0010BD08
		public static void Initialize(string path)
		{
			Config.path = path;
			if (!File.Exists(path))
			{
				File.Create(path).Close();
				if (Application.systemLanguage == SystemLanguage.ChineseSimplified)
				{
					Language.SetConfig("zh-CN");
				}
				else if (Application.systemLanguage == SystemLanguage.ChineseTraditional)
				{
					Language.SetConfig("zh-TW");
				}
				else if (Application.systemLanguage == SystemLanguage.Spanish)
				{
					Language.SetConfig("es-ES");
				}
				else if (Application.systemLanguage == SystemLanguage.Japanese)
				{
					Language.SetConfig("ja-JP");
				}
				else if (Application.systemLanguage == SystemLanguage.Korean)
				{
					Language.SetConfig("ko-KR");
				}
				else if (Application.systemLanguage == SystemLanguage.French)
				{
					Language.SetConfig("fr-FR");
				}
				else if (Application.systemLanguage == SystemLanguage.German)
				{
					Language.SetConfig("de-DE");
				}
				else if (Application.systemLanguage == SystemLanguage.Italian)
				{
					Language.SetConfig("it-IT");
				}
				else if (Application.systemLanguage == SystemLanguage.Portuguese)
				{
					Language.SetConfig("pt-PT");
				}
				else
				{
					Config.Set("Language", "en-US");
				}
				Config.Save();
			}
			string[] lines = File.ReadAllText(path).Replace("\r", "").Split('\n', StringSplitOptions.None);
			Config.translations.Clear();
			for (int i = 0; i < lines.Length; i++)
			{
				string[] mats = Regex.Split(lines[i], "->");
				if (mats.Length == 2)
				{
					Config.OneString s = new Config.OneString
					{
						original = mats[0],
						translated = mats[1]
					};
					Config.translations.Add(s);
				}
			}
		}

		// Token: 0x060089D9 RID: 35289 RVA: 0x0010DC78 File Offset: 0x0010BE78
		public static bool Have(string original)
		{
			bool found = false;
			for (int i = 0; i < Config.translations.Count; i++)
			{
				if (Config.translations[i].original == original)
				{
					found = true;
					break;
				}
			}
			return found;
		}

		// Token: 0x060089DA RID: 35290 RVA: 0x0010DCBC File Offset: 0x0010BEBC
		public static string Get(string original, string defau)
		{
			string return_value = defau;
			bool found = false;
			for (int i = 0; i < Config.translations.Count; i++)
			{
				if (Config.translations[i].original == original)
				{
					return_value = Config.translations[i].translated;
					found = true;
					break;
				}
			}
			if (!found && Config.path != null)
			{
				File.AppendAllText(Config.path, original + "->" + defau + "\r\n");
				Config.OneString s = new Config.OneString
				{
					original = original,
					translated = defau
				};
				return_value = defau;
				Config.translations.Add(s);
			}
			return return_value.Replace("@ui", string.Empty);
		}

		// Token: 0x060089DB RID: 35291 RVA: 0x0010DD68 File Offset: 0x0010BF68
		public static void Set(string original, string setted)
		{
			bool found = false;
			for (int i = 0; i < Config.translations.Count; i++)
			{
				if (Config.translations[i].original == original)
				{
					found = true;
					Config.translations[i].translated = setted;
				}
			}
			if (!found)
			{
				Config.OneString s = new Config.OneString();
				s.original = original;
				s.translated = setted;
				Config.translations.Add(s);
			}
		}

		// Token: 0x060089DC RID: 35292 RVA: 0x0010DDDC File Offset: 0x0010BFDC
		public static float GetFloat(string v, float defau)
		{
			int getted = 0;
			try
			{
				getted = int.Parse(Config.Get(v, (defau * 1000f).ToString()));
			}
			catch
			{
			}
			return (float)getted / 1000f;
		}

		// Token: 0x060089DD RID: 35293 RVA: 0x0010DE24 File Offset: 0x0010C024
		public static void SetFloat(string v, float f)
		{
			Config.Set(v, ((int)(f * 1000f)).ToString());
		}

		// Token: 0x060089DE RID: 35294 RVA: 0x0010DE48 File Offset: 0x0010C048
		public static bool GetBool(string original, bool value)
		{
			try
			{
				value = Config.Get(original, value ? "1" : "0") == "1";
			}
			catch
			{
			}
			return value;
		}

		// Token: 0x060089DF RID: 35295 RVA: 0x0010DE8C File Offset: 0x0010C08C
		public static void SetBool(string original, bool value)
		{
			Config.Set(original, value ? "1" : "0");
		}

		// Token: 0x060089E0 RID: 35296 RVA: 0x0010DEA4 File Offset: 0x0010C0A4
		public static void Save()
		{
			string all = "";
			for (int i = 0; i < Config.translations.Count; i++)
			{
				all = string.Concat(new string[]
				{
					all,
					Config.translations[i].original,
					"->",
					Config.translations[i].translated,
					"\r\n"
				});
			}
			try
			{
				File.WriteAllText(Config.path, all);
			}
			catch (Exception ex)
			{
				Program.noAccess = true;
				Debug.Log(ex);
			}
		}

		// Token: 0x060089E1 RID: 35297 RVA: 0x0010DF40 File Offset: 0x0010C140
		public static float GetUIScale(float maxUIScale = 1.5f)
		{
			float scale = float.Parse(Config.Get("UIScale", 1000f.ToString())) / 1000f;
			if (scale <= maxUIScale)
			{
				return scale;
			}
			return maxUIScale;
		}

		// Token: 0x060089E2 RID: 35298 RVA: 0x0010DF78 File Offset: 0x0010C178
		public static string GetConfigDeckName(bool containType = true)
		{
			string config = Config.Get("DeckInUse", "@ui");
			if (config.StartsWith("/") && config.Length > 1)
			{
				string text = config;
				config = text.Substring(1, text.Length - 1);
			}
			if (!containType && config.Contains("/"))
			{
				config = Path.GetFileName(config);
			}
			return config;
		}

		// Token: 0x060089E3 RID: 35299 RVA: 0x0010DFD8 File Offset: 0x0010C1D8
		public static void SetConfigDeck(string deckName, bool needCheck = false)
		{
			if (needCheck)
			{
				string type = Program.instance.deckSelector.DeckType;
				string config = ((type == string.Empty) ? deckName : (type + "/" + deckName));
				Config.Set("DeckInUse", config);
				return;
			}
			Config.Set("DeckInUse", deckName);
		}

		// Token: 0x0400C503 RID: 50435
		public const string LABEL_TIMING = "Timing";

		// Token: 0x0400C504 RID: 50436
		private const string SEPARATOR = "->";

		// Token: 0x0400C505 RID: 50437
		public const string EMPTY_STRING = "@ui";

		// Token: 0x0400C506 RID: 50438
		public const string STRING_YES = "1";

		// Token: 0x0400C507 RID: 50439
		public const string STRING_NO = "0";

		// Token: 0x0400C508 RID: 50440
		public static uint ClientVersion = 4962U;

		// Token: 0x0400C509 RID: 50441
		private static readonly List<Config.OneString> translations = new List<Config.OneString>();

		// Token: 0x0400C50A RID: 50442
		private static string path;

		// Token: 0x0200123A RID: 4666
		private class OneString
		{
			// Token: 0x0400C50B RID: 50443
			public string original = "";

			// Token: 0x0400C50C RID: 50444
			public string translated = "";
		}
	}
}
