using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.Utility;
using Newtonsoft.Json;
using UnityEngine;

namespace MDPro3.Duel
{
	// Token: 0x02001514 RID: 5396
	public static class VoicePlayer
	{
		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x06009D05 RID: 40197 RVA: 0x00194676 File Offset: 0x00192876
		public static int heroCode
		{
			get
			{
				return int.Parse(VoicePlayer.heroString);
			}
		}

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x06009D06 RID: 40198 RVA: 0x00194682 File Offset: 0x00192882
		public static int rivalCode
		{
			get
			{
				return int.Parse(VoicePlayer.rivalString);
			}
		}

		// Token: 0x06009D07 RID: 40199 RVA: 0x00194690 File Offset: 0x00192890
		public static void LoadData()
		{
			OcgCore.Condition condition = OcgCore.condition;
			string chara = Config.Get(condition.ToString() + "Character0", "0001");
			string language = Language.GetConfig();
			if (language != VoicePlayer.loadedLanguage || VoicePlayer.heroString != chara)
			{
				string originalChara = CharacterSelector.characters.GetCharacterOriginalId(chara);
				VoicePlayer.heroString = chara;
				VoicePlayer.heroVoices = JsonConvert.DeserializeObject<VoicesData>(File.ReadAllText("Data/locales/ja-JP/voice/V" + originalChara + ".json"));
				VoicePlayer.heroLines = JsonConvert.DeserializeObject<LinesData>(File.ReadAllText(string.Concat(new string[] { "Data/locales/", language, "/voice/SN", originalChara, ".json" })));
			}
			chara = Config.Get(condition.ToString() + "Character1", "0001");
			if (language != VoicePlayer.loadedLanguage || VoicePlayer.rivalString != chara)
			{
				string originalChara2 = CharacterSelector.characters.GetCharacterOriginalId(chara);
				VoicePlayer.rivalString = chara;
				VoicePlayer.rivalVoices = JsonConvert.DeserializeObject<VoicesData>(File.ReadAllText("Data/locales/ja-JP/voice/V" + originalChara2 + ".json"));
				VoicePlayer.rivalLines = JsonConvert.DeserializeObject<LinesData>(File.ReadAllText(string.Concat(new string[] { "Data/locales/", language, "/voice/SN", originalChara2, ".json" })));
			}
			VoicePlayer.loadedLanguage = language;
		}

		// Token: 0x06009D08 RID: 40200 RVA: 0x00194800 File Offset: 0x00192A00
		public static List<string>[] GetVoicePaths(List<VoicePlayer.VoiceData> data)
		{
			List<string>[] returnValue = new List<string>[data.Count];
			for (int i = 0; i < data.Count; i++)
			{
				returnValue[i] = new List<string>();
			}
			for (int j = 0; j < data.Count; j++)
			{
				for (int k = 0; k < data[j].num; k++)
				{
					string path = string.Concat(new string[]
					{
						"Sound/Voice/",
						data[j].name.Substring(0, 5),
						"/",
						data[j].name,
						"_",
						k.ToString(),
						".ogg"
					});
					returnValue[j].Add(path);
				}
			}
			return returnValue;
		}

		// Token: 0x06009D09 RID: 40201 RVA: 0x001948C8 File Offset: 0x00192AC8
		public static LineInfo GetLine(string voiceName, bool isHero)
		{
			if (VoicePlayer.heroLines == null || VoicePlayer.rivalLines == null)
			{
				return null;
			}
			LineInfo line;
			if ((isHero ? VoicePlayer.heroLines : VoicePlayer.rivalLines).info.TryGetValue(voiceName, out line))
			{
				return line;
			}
			LineInfo line2;
			if (((!isHero) ? VoicePlayer.heroLines : VoicePlayer.rivalLines).info.TryGetValue(voiceName, out line2))
			{
				return line2;
			}
			return null;
		}

		// Token: 0x06009D0A RID: 40202 RVA: 0x00194928 File Offset: 0x00192B28
		public static string GetVoiceBySubCategory(VoiceInfoEntry entry, int sub, int subInCase, int patternIndex, bool patternRestrict = false)
		{
			List<string> tempStrings = new List<string>();
			foreach (KeyValuePair<string, VoiceInfo> e in entry.rawKvp)
			{
				if (e.Value.subCategoryIndex == sub)
				{
					if (e.Value.patternIndex == patternIndex)
					{
						return e.Value.shortName;
					}
					tempStrings.Add(e.Value.shortName);
				}
			}
			if (tempStrings.Count == 0)
			{
				foreach (KeyValuePair<string, VoiceInfo> e2 in entry.rawKvp)
				{
					if (e2.Value.subCategoryIndex == subInCase)
					{
						if (e2.Value.patternIndex == patternIndex)
						{
							return e2.Value.shortName;
						}
						tempStrings.Add(e2.Value.shortName);
					}
				}
			}
			if (patternRestrict)
			{
				return null;
			}
			if (tempStrings.Count > 0)
			{
				return tempStrings[global::UnityEngine.Random.Range(0, tempStrings.Count)];
			}
			Debug.LogErrorFormat("Did not find Subcategory {0}-{1} in {2}", new object[]
			{
				sub,
				subInCase,
				entry.rawKvp.First<KeyValuePair<string, VoiceInfo>>().Key.Substring(0, 8)
			});
			return null;
		}

		// Token: 0x06009D0B RID: 40203 RVA: 0x00194AA8 File Offset: 0x00192CA8
		public static string GetVoiceBySituation(VoiceInfoEntry entry, int situation)
		{
			string returnValue = Tools.GetRandomDictionaryElement<string, VoiceInfo>(entry.rawKvp).Value.shortName;
			List<string> tempStrings = new List<string>();
			foreach (KeyValuePair<string, VoiceInfo> e in entry.rawKvp)
			{
				if (e.Value.situations != null && e.Value.situations.Length != 0 && Array.IndexOf<int>(e.Value.situations, situation) > 0)
				{
					tempStrings.Add(e.Value.shortName);
				}
			}
			if (tempStrings.Count > 0)
			{
				returnValue = tempStrings[global::UnityEngine.Random.Range(0, tempStrings.Count)];
			}
			return returnValue;
		}

		// Token: 0x06009D0C RID: 40204 RVA: 0x00194B78 File Offset: 0x00192D78
		public static string GetVoiceByDuelist(VoiceInfoEntry entry, VoiceInfoEntry entrySp, int duelist)
		{
			string returnValue = Tools.GetRandomDictionaryElement<string, VoiceInfo>(entry.rawKvp).Value.shortName;
			if (entrySp == null)
			{
				return returnValue;
			}
			List<string> tempStrings = new List<string>();
			foreach (KeyValuePair<string, VoiceInfo> e in entrySp.rawKvp)
			{
				for (int i = 0; i < e.Value.duelists.Length; i++)
				{
					if (Convert.ToInt32(e.Value.duelists[i]) == duelist)
					{
						tempStrings.Add(e.Value.shortName);
					}
				}
			}
			if (tempStrings.Count > 0)
			{
				returnValue = tempStrings[global::UnityEngine.Random.Range(0, tempStrings.Count)];
			}
			return returnValue;
		}

		// Token: 0x06009D0D RID: 40205 RVA: 0x00194C4C File Offset: 0x00192E4C
		private static bool DataHaveVoice(VoiceInfoEntry entry, int sub, int patternIndex)
		{
			if (entry == null)
			{
				return false;
			}
			foreach (KeyValuePair<string, VoiceInfo> e in entry.rawKvp)
			{
				if (e.Value.subCategoryIndex == sub && e.Value.patternIndex == patternIndex)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009D0E RID: 40206 RVA: 0x00194CC4 File Offset: 0x00192EC4
		public static int GetVoiceNum(VoicesData target, string key)
		{
			if (key == null)
			{
				Debug.LogWarning("String key for GetVoiceNum is null. ");
				return 1;
			}
			int numVoices;
			if (target.NumVoices.TryGetValue(key, out numVoices))
			{
				return numVoices;
			}
			Debug.LogFormat("Did not Find NumVoices '{0}' in {1} ", new object[]
			{
				key,
				target.BeforeDuel.rawKvp.First<KeyValuePair<string, VoiceInfo>>().Key.Substring(0, 5)
			});
			return 1;
		}

		// Token: 0x06009D0F RID: 40207 RVA: 0x001927A3 File Offset: 0x001909A3
		public static bool DamageIsBig(int damage)
		{
			return damage >= 2000;
		}

		// Token: 0x06009D10 RID: 40208 RVA: 0x00194D2C File Offset: 0x00192F2C
		public static VoicePlayer.VoiceData GetFinishDamageVoiceData(VoicesData target, bool isHero)
		{
			VoicePlayer.VoiceData data = default(VoicePlayer.VoiceData);
			data.name = Tools.GetRandomDictionaryElement<string, VoiceInfo>(target.FinishDamage.rawKvp).Value.shortName;
			data.num = VoicePlayer.GetVoiceNum(target, data.name);
			data.isHero = isHero;
			data.wait = false;
			data.delay = 0f;
			return data;
		}

		// Token: 0x06009D11 RID: 40209 RVA: 0x00194D98 File Offset: 0x00192F98
		public static VoicePlayer.VoiceData GetBeforeCardEffectData(VoicesData target, bool isHero)
		{
			VoicePlayer.VoiceData data = default(VoicePlayer.VoiceData);
			data.name = Tools.GetRandomDictionaryElement<string, VoiceInfo>(target.BeforeCardEffect.rawKvp).Value.shortName;
			data.num = VoicePlayer.GetVoiceNum(target, data.name);
			data.isHero = isHero;
			data.wait = true;
			data.delay = 0f;
			return data;
		}

		// Token: 0x06009D12 RID: 40210 RVA: 0x00194E04 File Offset: 0x00193004
		public static VoicePlayer.VoiceData GetBeforeSummonData(VoicesData target, bool isHero)
		{
			VoicePlayer.VoiceData data = default(VoicePlayer.VoiceData);
			data.name = Tools.GetRandomDictionaryElement<string, VoiceInfo>(target.BeforeSummon.rawKvp).Value.shortName;
			data.num = VoicePlayer.GetVoiceNum(target, data.name);
			data.isHero = isHero;
			data.wait = true;
			data.delay = 0f;
			return data;
		}

		// Token: 0x06009D13 RID: 40211 RVA: 0x00194E70 File Offset: 0x00193070
		public static VoicePlayer.VoiceData GetVoiceByCard(VoicesData target, VoiceInfoEntry entry, int card, int engineparam, bool isMe)
		{
			VoicePlayer.VoiceData returnValue = default(VoicePlayer.VoiceData);
			returnValue.name = string.Empty;
			if (entry == null)
			{
				return returnValue;
			}
			card = VoicePlayer.GetCidDefaultAltCard(card);
			int cid = Cid2Ydk.GetCID(card);
			if (cid == card)
			{
				return returnValue;
			}
			List<string> tempStrings = new List<string>();
			foreach (VoiceInfo value in entry.rawKvp.Values)
			{
				if (value.cards != null && value.cards.Contains(cid))
				{
					tempStrings.Add(value.shortName);
					if (value.engineparams != null && value.engineparams.Contains(engineparam))
					{
						tempStrings.Clear();
						tempStrings.Add(value.shortName);
						break;
					}
				}
			}
			if (tempStrings.Count > 0)
			{
				returnValue.name = tempStrings[global::UnityEngine.Random.Range(0, tempStrings.Count)];
			}
			if (returnValue.name != string.Empty)
			{
				returnValue.num = VoicePlayer.GetVoiceNum(target, returnValue.name);
				returnValue.isHero = isMe;
				returnValue.wait = true;
				returnValue.delay = 0f;
			}
			return returnValue;
		}

		// Token: 0x06009D14 RID: 40212 RVA: 0x00194FB0 File Offset: 0x001931B0
		public static int GetCidDefaultAltCard(int card)
		{
			Card data = CardsManager.Get(card, false);
			if (card == 89631139 || data.Alias == 89631139)
			{
				return 89631141;
			}
			if (card == 46986414 || data.Alias == 46986414)
			{
				return 46986417;
			}
			return card;
		}

		// Token: 0x06009D15 RID: 40213 RVA: 0x00194FFC File Offset: 0x001931FC
		public static void ExportAllCardsNotFound()
		{
			List<int> ids = new List<int>();
			string[] files = Directory.GetFiles("Data/locales/zh-CN/voice/");
			List<string> vjsons = new List<string>();
			foreach (string json in files)
			{
				if (Path.GetFileName(json).StartsWith("V"))
				{
					vjsons.Add(json);
				}
			}
			foreach (string text2 in vjsons)
			{
				foreach (VoiceInfoEntry entry in JsonConvert.DeserializeObject<VoicesData>(File.ReadAllText(text2)).GetEntryWithCard())
				{
					if (entry != null)
					{
						foreach (VoiceInfo value in entry.rawKvp.Values)
						{
							if (value.cards != null)
							{
								foreach (int card in value.cards)
								{
									if (!ids.Contains(card))
									{
										ids.Add(card);
									}
								}
							}
						}
					}
				}
			}
			string text = string.Empty;
			foreach (int id in ids)
			{
				if (!Cid2Ydk.HaveCid(id))
				{
					text = text + id.ToString() + "\r\n";
				}
			}
			File.WriteAllText("Data/Duel Links Ids.txt", text);
		}

		// Token: 0x06009D16 RID: 40214 RVA: 0x001951CC File Offset: 0x001933CC
		public static int LeadingStateOfHero()
		{
			if (OcgCore.life0 >= OcgCore.lpLimit / 2)
			{
				if (OcgCore.life1 < OcgCore.lpLimit / 2)
				{
					return 1;
				}
			}
			else
			{
				if (OcgCore.life1 >= OcgCore.lpLimit / 2)
				{
					return 2;
				}
				if (OcgCore.life0 >= OcgCore.lpLimit / 4 && OcgCore.life1 < OcgCore.lpLimit / 4)
				{
					return 1;
				}
				if (OcgCore.life0 < OcgCore.lpLimit / 4)
				{
					return 2;
				}
			}
			return 0;
		}

		// Token: 0x06009D17 RID: 40215 RVA: 0x00195238 File Offset: 0x00193438
		public static int LeadingStateOfRival()
		{
			if (OcgCore.life1 >= OcgCore.lpLimit / 2)
			{
				if (OcgCore.life0 < OcgCore.lpLimit / 2)
				{
					return 1;
				}
			}
			else
			{
				if (OcgCore.life0 >= OcgCore.lpLimit / 2)
				{
					return 2;
				}
				if (OcgCore.life1 >= OcgCore.lpLimit / 4 && OcgCore.life0 < OcgCore.lpLimit / 4)
				{
					return 1;
				}
				if (OcgCore.life1 < OcgCore.lpLimit / 4)
				{
					return 2;
				}
			}
			return 0;
		}

		// Token: 0x06009D18 RID: 40216 RVA: 0x001952A2 File Offset: 0x001934A2
		public static bool NeedBeforeCardEffect(bool isHero)
		{
			if (OcgCore.cardsInChain.Count == 0)
			{
				return false;
			}
			List<GameCard> cardsInChain = OcgCore.cardsInChain;
			return cardsInChain[cardsInChain.Count - 1].p.controller == 0U != isHero;
		}

		// Token: 0x0400DADA RID: 56026
		private const string voicePath = "Sound/Voice/";

		// Token: 0x0400DADB RID: 56027
		private const string customVoicePath = "Sound/CustomVoice/";

		// Token: 0x0400DADC RID: 56028
		private const string jsonPath = "Data/locales/";

		// Token: 0x0400DADD RID: 56029
		public const string defaultCharacter = "0001";

		// Token: 0x0400DADE RID: 56030
		public static string loadedLanguage;

		// Token: 0x0400DADF RID: 56031
		public static string heroString;

		// Token: 0x0400DAE0 RID: 56032
		public static string rivalString;

		// Token: 0x0400DAE1 RID: 56033
		public static VoicesData heroVoices;

		// Token: 0x0400DAE2 RID: 56034
		public static VoicesData rivalVoices;

		// Token: 0x0400DAE3 RID: 56035
		public static LinesData heroLines;

		// Token: 0x0400DAE4 RID: 56036
		public static LinesData rivalLines;

		// Token: 0x02001515 RID: 5397
		public struct VoiceData
		{
			// Token: 0x0400DAE5 RID: 56037
			public string name;

			// Token: 0x0400DAE6 RID: 56038
			public int num;

			// Token: 0x0400DAE7 RID: 56039
			public bool isHero;

			// Token: 0x0400DAE8 RID: 56040
			public bool wait;

			// Token: 0x0400DAE9 RID: 56041
			public float delay;
		}
	}
}
