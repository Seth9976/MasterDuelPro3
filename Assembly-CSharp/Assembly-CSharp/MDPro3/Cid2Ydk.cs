using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using MDPro3.Duel.YGOSharp;
using Newtonsoft.Json;

namespace MDPro3
{
	// Token: 0x02001237 RID: 4663
	public static class Cid2Ydk
	{
		// Token: 0x060089D1 RID: 35281 RVA: 0x0010D8D8 File Offset: 0x0010BAD8
		private static void Initialize()
		{
			if (Cid2Ydk.initialized)
			{
				return;
			}
			if (File.Exists("Data/cards_Lite.json"))
			{
				Cid2Ydk.dic = JsonConvert.DeserializeObject<Dictionary<string, Cid2Ydk.Id2Ydk>>(File.ReadAllText("Data/cards_Lite.json"));
			}
			else
			{
				Cid2Ydk.dic = JsonConvert.DeserializeObject<Dictionary<string, Cid2Ydk.Id2Ydk>>(File.ReadAllText("Data/cards.json"));
				Dictionary<string, Cid2Ydk.Id2Ydk> altDic = JsonConvert.DeserializeObject<Dictionary<string, Cid2Ydk.Id2Ydk>>(File.ReadAllText("Data/cards_Alt.json"));
				foreach (string key in altDic.Keys)
				{
					Cid2Ydk.dic[key] = altDic[key];
				}
				File.WriteAllText("Data/cards_Lite.json", JsonConvert.SerializeObject(Cid2Ydk.dic));
			}
			Cid2Ydk.initialized = true;
		}

		// Token: 0x060089D2 RID: 35282 RVA: 0x0010D9A0 File Offset: 0x0010BBA0
		public static bool HaveCid(int cid)
		{
			Cid2Ydk.Initialize();
			return Cid2Ydk.dic.ContainsKey(cid.ToString());
		}

		// Token: 0x060089D3 RID: 35283 RVA: 0x0010D9B8 File Offset: 0x0010BBB8
		public static int GetYDK(int cid)
		{
			Cid2Ydk.Initialize();
			if (Cid2Ydk.dic.ContainsKey(cid.ToString()))
			{
				return Cid2Ydk.dic[cid.ToString()].id;
			}
			return cid;
		}

		// Token: 0x060089D4 RID: 35284 RVA: 0x0010D9EC File Offset: 0x0010BBEC
		public static int GetCID(int ydk)
		{
			Cid2Ydk.Initialize();
			Card card = CardsManager.Get(ydk, false);
			if (card.Alias != 0)
			{
				ydk = card.Alias;
			}
			foreach (Cid2Ydk.Id2Ydk value in Cid2Ydk.dic.Values)
			{
				if (value.id == ydk || value.id == card.Id)
				{
					return value.cid;
				}
			}
			return ydk;
		}

		// Token: 0x060089D5 RID: 35285 RVA: 0x0010DA7C File Offset: 0x0010BC7C
		public static string ReplaceWithCardName(string origin)
		{
			Cid2Ydk.Initialize();
			origin = origin.Replace(" get='name'", string.Empty);
			return Regex.Replace(origin, "<card mrk='(\\d+)'/>", new MatchEvaluator(Cid2Ydk.EvaluatorGetNameFromNumber));
		}

		// Token: 0x060089D6 RID: 35286 RVA: 0x0010DAAC File Offset: 0x0010BCAC
		private static string EvaluatorGetNameFromNumber(Match match)
		{
			int cid = int.Parse(match.Groups[1].Value);
			Cid2Ydk.Id2Ydk code;
			Cid2Ydk.dic.TryGetValue(cid.ToString(), out code);
			if (code != null)
			{
				return CardsManager.Get(code.id, false).Name;
			}
			return CardsManager.Get(cid, false).Name;
		}

		// Token: 0x0400C4FC RID: 50428
		private const string cardsPath = "Data/cards.json";

		// Token: 0x0400C4FD RID: 50429
		private const string cardsAltPath = "Data/cards_Alt.json";

		// Token: 0x0400C4FE RID: 50430
		private const string cardsLitePath = "Data/cards_Lite.json";

		// Token: 0x0400C4FF RID: 50431
		private static bool initialized;

		// Token: 0x0400C500 RID: 50432
		private static Dictionary<string, Cid2Ydk.Id2Ydk> dic;

		// Token: 0x02001238 RID: 4664
		public class Id2Ydk
		{
			// Token: 0x0400C501 RID: 50433
			public int cid;

			// Token: 0x0400C502 RID: 50434
			public int id;
		}
	}
}
