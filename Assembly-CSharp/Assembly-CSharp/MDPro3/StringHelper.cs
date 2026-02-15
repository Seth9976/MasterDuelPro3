using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ionic.Zip;
using MDPro3.Duel.YGOSharp;
using MDPro3.Utility;
using UnityEngine;

namespace MDPro3
{
	// Token: 0x02001243 RID: 4675
	public static class StringHelper
	{
		// Token: 0x06008A12 RID: 35346 RVA: 0x0010F55C File Offset: 0x0010D75C
		public static void Initialize()
		{
			StringHelper.hashedStrings.Clear();
			StringHelper.hashedStringsForRender.Clear();
			StringHelper.hashedStringsForPrerelease.Clear();
			string text = File.ReadAllText("Data/locales/" + Language.GetConfig() + "/strings.conf");
			StringHelper.AddExpansionStrings(ref text);
			StringHelper.InitializeContent(text, 0);
			text = File.ReadAllText("Data/locales/" + Language.GetCardConfig() + "/strings.conf");
			StringHelper.AddExpansionStrings(ref text);
			StringHelper.InitializeContent(text, 1);
			text = File.ReadAllText("Data/locales/" + Language.GetPrereleaseConfig() + "/strings.conf");
			StringHelper.AddExpansionStrings(ref text);
			StringHelper.InitializeContent(text, 2);
		}

		// Token: 0x06008A13 RID: 35347 RVA: 0x0010F600 File Offset: 0x0010D800
		private static void AddExpansionStrings(ref string text)
		{
			if (Config.GetBool("Expansions", true))
			{
				foreach (string conf in Directory.GetFiles("Expansions/", "*.conf"))
				{
					if (!conf.ToLower().EndsWith("lflist.conf"))
					{
						text = text + "\r\n" + File.ReadAllText(conf);
					}
				}
				foreach (ZipFile zip in ZipHelper.zips)
				{
					if (!zip.Name.ToLower().EndsWith("script.zip"))
					{
						foreach (string file in zip.EntryFileNames)
						{
							if (file.ToLower().EndsWith(".conf") && !file.ToLower().EndsWith("lflist.conf"))
							{
								MemoryStream ms = new MemoryStream();
								zip[file].Extract(ms);
								text = text + "\r\n" + Encoding.UTF8.GetString(ms.ToArray());
							}
						}
					}
				}
			}
		}

		// Token: 0x06008A14 RID: 35348 RVA: 0x0010F760 File Offset: 0x0010D960
		private static void InitializeContent(string text, int type)
		{
			string[] array = text.Replace("\r", string.Empty).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
			List<StringHelper.HashedString> targetStrings = StringHelper.GetHashedStrings(type);
			targetStrings.Clear();
			if (type == 0)
			{
				StringHelper.setNames.Clear();
			}
			foreach (string line in array)
			{
				if (line.Length > 1 && line.Substring(0, 1) == "!")
				{
					string text2 = line;
					string[] mats = text2.Substring(1, text2.Length - 1).Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
					if (mats.Length > 2)
					{
						StringHelper.HashedString a = new StringHelper.HashedString
						{
							region = mats[0]
						};
						try
						{
							a.hashCode = StringHelper.StringToInt(mats[1]);
						}
						catch (Exception ex)
						{
							MessageManager.Cast(ex.ToString());
						}
						a.content = string.Empty;
						for (int i = 2; i < mats.Length; i++)
						{
							StringHelper.HashedString hashedString = a;
							hashedString.content = hashedString.content + mats[i] + " ";
						}
						StringHelper.HashedString hashedString2 = a;
						text2 = a.content;
						hashedString2.content = text2.Substring(0, text2.Length - 1);
						if (StringHelper.Get(a.region, a.hashCode, type) == string.Empty)
						{
							targetStrings.Add(a);
							if (a.region == "setname" && type == 0)
							{
								StringHelper.setNames.Add(a);
							}
						}
					}
				}
			}
		}

		// Token: 0x06008A15 RID: 35349 RVA: 0x0010F900 File Offset: 0x0010DB00
		private static int StringToInt(string str)
		{
			int return_value = 0;
			try
			{
				if (str.Length > 2 && str.Substring(0, 2) == "0x")
				{
					return_value = Convert.ToInt32(str, 16);
				}
				else
				{
					return_value = int.Parse(str);
				}
			}
			catch
			{
			}
			return return_value;
		}

		// Token: 0x06008A16 RID: 35350 RVA: 0x0010F954 File Offset: 0x0010DB54
		private static List<StringHelper.HashedString> GetHashedStrings(int type)
		{
			List<StringHelper.HashedString> list;
			if (type != 1)
			{
				if (type != 2)
				{
					list = StringHelper.hashedStrings;
				}
				else
				{
					list = StringHelper.hashedStringsForPrerelease;
				}
			}
			else
			{
				list = StringHelper.hashedStringsForRender;
			}
			return list;
		}

		// Token: 0x06008A17 RID: 35351 RVA: 0x0010F984 File Offset: 0x0010DB84
		internal static string Get(string region, int hashCode, int type = 0)
		{
			string re = string.Empty;
			foreach (StringHelper.HashedString s in StringHelper.GetHashedStrings(type))
			{
				if (s.region == region && s.hashCode == hashCode)
				{
					re = s.content;
					break;
				}
			}
			return re;
		}

		// Token: 0x06008A18 RID: 35352 RVA: 0x0010F9F8 File Offset: 0x0010DBF8
		internal static string GetUnsafe(int hashCode, int type = 0)
		{
			string re = string.Empty;
			foreach (StringHelper.HashedString s in StringHelper.GetHashedStrings(type))
			{
				if (s.region == "system" && s.hashCode == hashCode)
				{
					re = s.content;
					break;
				}
			}
			return re;
		}

		// Token: 0x06008A19 RID: 35353 RVA: 0x0010FA70 File Offset: 0x0010DC70
		internal static string Get(int description)
		{
			string a = string.Empty;
			if (description < 10000)
			{
				a = StringHelper.Get("system", description, 0);
			}
			else
			{
				int code = description >> 4;
				int index = description & 15;
				try
				{
					a = CardsManager.Get(code, false).Str[index];
				}
				catch (Exception ex)
				{
					MessageManager.Cast(ex.ToString());
				}
			}
			return a;
		}

		// Token: 0x06008A1A RID: 35354 RVA: 0x0010FAD4 File Offset: 0x0010DCD4
		internal static string FormatLocation(uint location, uint sequence)
		{
			if (location == 8U)
			{
				if (sequence < 5U)
				{
					return StringHelper.Get(1003);
				}
				if (sequence == 5U)
				{
					return StringHelper.Get(1008);
				}
				return StringHelper.Get(1009);
			}
			else
			{
				uint filter = 1U;
				int i = 1000;
				while (filter != 256U && filter != location)
				{
					i++;
					filter <<= 1;
				}
				if (filter == location)
				{
					return StringHelper.Get(i);
				}
				return "???";
			}
		}

		// Token: 0x06008A1B RID: 35355 RVA: 0x0010FB3C File Offset: 0x0010DD3C
		internal static string FormatLocation(GPS gps)
		{
			return StringHelper.FormatLocation(gps.location, gps.sequence);
		}

		// Token: 0x06008A1C RID: 35356 RVA: 0x0010FB50 File Offset: 0x0010DD50
		internal static string Attribute(long attribute, int type = 0)
		{
			string r = string.Empty;
			bool passFirst = false;
			for (int i = 0; i < 7; i++)
			{
				if ((attribute & (long)(1UL << (i & 31))) > 0L)
				{
					if (passFirst)
					{
						r += "/";
					}
					r += StringHelper.GetUnsafe(1010 + i, type);
					passFirst = true;
				}
			}
			return r;
		}

		// Token: 0x06008A1D RID: 35357 RVA: 0x0010FBA4 File Offset: 0x0010DDA4
		internal static string Race(long race, int type = 0)
		{
			string r = string.Empty;
			bool passFirst = false;
			for (int i = 0; i < 26; i++)
			{
				if ((race & (1L << (i & 31))) > 0L)
				{
					if (passFirst)
					{
						r += "/";
					}
					r += StringHelper.GetUnsafe(1020 + i, type);
					passFirst = true;
				}
			}
			return r;
		}

		// Token: 0x06008A1E RID: 35358 RVA: 0x0010FBFC File Offset: 0x0010DDFC
		internal static string MainType(long cardType, int type = 0)
		{
			string r = string.Empty;
			bool passFirst = false;
			for (int i = 0; i < 3; i++)
			{
				if ((cardType & (1L << (i & 31))) > 0L)
				{
					if (passFirst)
					{
						r += "/";
					}
					r += StringHelper.GetUnsafe(1050 + i, type);
					passFirst = true;
				}
			}
			return r;
		}

		// Token: 0x06008A1F RID: 35359 RVA: 0x0010FC50 File Offset: 0x0010DE50
		internal static string SecondType(long a, int type = 0)
		{
			string start = string.Empty;
			string end = string.Empty;
			if ((a & 109060288L) > 0L)
			{
				for (int i = 4; i < 27; i++)
				{
					if ((a & 109060288L & (1L << (i & 31))) > 0L && (type != 0 || i != 25 || Language.NeedSpSummonString(Language.GetConfig())) && (type != 1 || i != 25 || Language.NeedSpSummonString(Language.GetCardConfig())) && (type != 2 || i != 25 || Language.NeedSpSummonString(Language.GetPrereleaseConfig())))
					{
						start = start + "/" + StringHelper.GetUnsafe(1050 + i, type);
						break;
					}
				}
				a -= a & 109060288L;
			}
			if ((a & 16777216L) > 0L)
			{
				start = start + "/" + StringHelper.GetUnsafe(1074, type);
				a -= 16777216L;
			}
			if ((a & 48L) > 0L)
			{
				for (int j = 4; j < 6; j++)
				{
					if ((a & (1L << (j & 31))) > 0L)
					{
						end = end + "/" + StringHelper.GetUnsafe(1050 + j, type);
						break;
					}
				}
				a -= a & 48L;
			}
			for (int k = 4; k < 27; k++)
			{
				if ((a & (1L << (k & 31))) > 0L)
				{
					start = start + "/" + StringHelper.GetUnsafe(1050 + k, type);
				}
			}
			string returnValue = start + end;
			if (returnValue == string.Empty)
			{
				returnValue = StringHelper.GetUnsafe(1054, type);
			}
			else
			{
				string text = returnValue;
				returnValue = text.Substring(1, text.Length - 1);
			}
			return returnValue;
		}

		// Token: 0x06008A20 RID: 35360 RVA: 0x0010FDEC File Offset: 0x0010DFEC
		internal static string Zone(long data)
		{
			List<string> strs = new List<string>();
			for (long filter = 1L; filter <= 4294967296L; filter <<= 1)
			{
				string str = string.Empty;
				long s = filter & data;
				if (s != 0L)
				{
					if ((s & 96L) != 0L)
					{
						str += StringHelper.GetUnsafe(1081, 0);
						data &= -6291457L;
					}
					else if ((s & 65535L) != 0L)
					{
						str += StringHelper.GetUnsafe(102, 0);
					}
					else if ((s & (long)((ulong)(-65536))) != 0L)
					{
						str += StringHelper.GetUnsafe(103, 0);
						s >>= 16;
					}
					if ((s & 31L) != 0L)
					{
						str += StringHelper.GetUnsafe(1002, 0);
					}
					else if ((s & 65280L) != 0L)
					{
						s >>= 8;
						if ((s & 31L) != 0L)
						{
							str += StringHelper.GetUnsafe(1003, 0);
						}
						else if ((s & 32L) != 0L)
						{
							str += StringHelper.GetUnsafe(1008, 0);
						}
						else if ((s & 192L) != 0L)
						{
							str += StringHelper.GetUnsafe(1009, 0);
						}
					}
					int seq = 1;
					int i = 1;
					while (i < 256 && (s & (long)i) == 0L)
					{
						seq++;
						i <<= 1;
					}
					str = str + "(" + seq.ToString() + ")";
					strs.Add(str);
				}
			}
			return string.Join(", ", strs.ToArray());
		}

		// Token: 0x06008A21 RID: 35361 RVA: 0x0010FF58 File Offset: 0x0010E158
		internal static string GetSetName(long Setcode)
		{
			int[] setcodes = new int[4];
			for (int i = 0; i < 4; i++)
			{
				setcodes[i] = (int)((Setcode >> i * 16) & 65535L);
			}
			List<string> returnValue = new List<string>();
			for (int j = 0; j < StringHelper.setNames.Count; j++)
			{
				int currentHash = StringHelper.setNames[j].hashCode;
				for (int k = 0; k < 4; k++)
				{
					if (currentHash == setcodes[k])
					{
						string setString = StringHelper.setNames[j].content.Split('\t', StringSplitOptions.None)[0];
						returnValue.Add(setString);
					}
				}
			}
			if (returnValue.Count > 0)
			{
				return string.Join("|", returnValue.ToArray());
			}
			return string.Empty;
		}

		// Token: 0x06008A22 RID: 35362 RVA: 0x00110014 File Offset: 0x0010E214
		internal static int GetSetNameCode(string setName)
		{
			int returnValue = 0;
			for (int i = 0; i < StringHelper.setNames.Count; i++)
			{
				string setString = StringHelper.setNames[i].content.Split('\t', StringSplitOptions.None)[0];
				if (setName == setString)
				{
					returnValue = StringHelper.setNames[i].hashCode;
				}
			}
			return returnValue;
		}

		// Token: 0x06008A23 RID: 35363 RVA: 0x00110070 File Offset: 0x0010E270
		public static string GetType(Card data, bool render = false, bool rushDuel = false)
		{
			string re = string.Empty;
			if (data.Id == 0)
			{
				return re;
			}
			string bracketLeft = "【";
			string bracketRight = "】";
			if (render && Language.NeedSmallBracket(data.isPre ? Language.GetPrereleaseConfig() : Language.GetCardConfig()))
			{
				bracketLeft = "[";
				bracketRight = "]";
			}
			Card origin = (render ? CardsManager.GetRenderCard(data.Id) : CardsManager.Get(data.Id, false));
			try
			{
				if (CardDescription.CardIsMonster(data))
				{
					if (data.Race != origin.Race)
					{
						re = string.Concat(new string[]
						{
							bracketLeft,
							"<color=#FD3E08>",
							InterString.Get("[?]族", StringHelper.Race((long)data.Race, 0), 0),
							"</color>/",
							StringHelper.SecondType((long)data.Type, 0),
							bracketRight
						});
					}
					else
					{
						re = string.Concat(new string[]
						{
							bracketLeft,
							InterString.Get("[?]族", StringHelper.Race((long)data.Race, 2), 2),
							"/",
							StringHelper.SecondType((long)data.Type, 2),
							bracketRight
						});
					}
				}
				else if (rushDuel)
				{
					int translationType = 0;
					if (render)
					{
						translationType = 1;
						if (data.isPre)
						{
							translationType = 2;
						}
					}
					re = bracketLeft;
					if (data.HasType(CardType.Spell))
					{
						re += InterString.Get("魔法卡", translationType);
					}
					else
					{
						re += InterString.Get("陷阱卡", translationType);
					}
					re = re.Replace("SPELL CARD", "Spell Card").Replace("TRAP CARD", "Trap Card").Replace("CARTA MÁGICA", "Carta Mágica")
						.Replace("CARTA TRAMPA", "Carta Trampa");
					string secondType = StringHelper.SecondType((long)data.Type, 2);
					if (secondType != StringHelper.GetUnsafe(1054, 2))
					{
						re = re + "/" + secondType;
						if (data.HasType(CardType.Equip))
						{
							re += "<Sprite=0>";
						}
						if (data.HasType(CardType.QuickPlay))
						{
							re += "<Sprite=1>";
						}
						if (data.HasType(CardType.Field))
						{
							re += "<Sprite=2>";
						}
						if (data.HasType(CardType.Ritual))
						{
							re += "<Sprite=3>";
						}
						if (data.HasType(CardType.Continuous))
						{
							re += "<Sprite=4>";
						}
						if (data.HasType(CardType.Counter))
						{
							re += "<Sprite=5>";
						}
					}
					re += bracketRight;
				}
				else if (render)
				{
					int translationType2 = 1;
					if (data.isPre)
					{
						translationType2 = 2;
					}
					re = bracketLeft;
					if (data.HasType(CardType.Spell))
					{
						re += InterString.Get("魔法卡", translationType2);
					}
					else
					{
						re += InterString.Get("陷阱卡", translationType2);
					}
					if (StringHelper.SecondType((long)data.Type, 2) != StringHelper.GetUnsafe(1054, 2))
					{
						if (data.HasType(CardType.Equip))
						{
							re += "<Sprite=0>";
						}
						if (data.HasType(CardType.QuickPlay))
						{
							re += "<Sprite=1>";
						}
						if (data.HasType(CardType.Field))
						{
							re += "<Sprite=2>";
						}
						if (data.HasType(CardType.Ritual))
						{
							re += "<Sprite=3>";
						}
						if (data.HasType(CardType.Continuous))
						{
							re += "<Sprite=4>";
						}
						if (data.HasType(CardType.Counter))
						{
							re += "<Sprite=5>";
						}
					}
					re += bracketRight;
				}
				else
				{
					re = bracketLeft + StringHelper.MainType((long)data.Type, 2) + bracketRight;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
			return re;
		}

		// Token: 0x0400C54D RID: 50509
		private const string PATH_CONF_FILE = "/strings.conf";

		// Token: 0x0400C54E RID: 50510
		private static readonly List<StringHelper.HashedString> hashedStrings = new List<StringHelper.HashedString>();

		// Token: 0x0400C54F RID: 50511
		private static readonly List<StringHelper.HashedString> hashedStringsForRender = new List<StringHelper.HashedString>();

		// Token: 0x0400C550 RID: 50512
		private static readonly List<StringHelper.HashedString> hashedStringsForPrerelease = new List<StringHelper.HashedString>();

		// Token: 0x0400C551 RID: 50513
		private static readonly List<StringHelper.HashedString> setNames = new List<StringHelper.HashedString>();

		// Token: 0x02001244 RID: 4676
		private class HashedString
		{
			// Token: 0x0400C552 RID: 50514
			public string content = string.Empty;

			// Token: 0x0400C553 RID: 50515
			public int hashCode;

			// Token: 0x0400C554 RID: 50516
			public string region = string.Empty;
		}
	}
}
