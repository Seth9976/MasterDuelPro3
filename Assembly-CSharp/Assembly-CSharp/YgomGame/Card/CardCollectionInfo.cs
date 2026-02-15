using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Deck;
using YgomGame.TextIDs;

namespace YgomGame.Card
{
	// Token: 0x020010F4 RID: 4340
	public class CardCollectionInfo
	{
		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x0600811D RID: 33053 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int REGULATIONID_STANDARD
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600811E RID: 33054 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize()
		{
		}

		// Token: 0x0600811F RID: 33055 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CardsCollectionNotificator(object value)
		{
		}

		// Token: 0x06008120 RID: 33056 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CardRarityNotificator(object value)
		{
		}

		// Token: 0x06008121 RID: 33057 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CardCraftNotificator(object value)
		{
		}

		// Token: 0x06008122 RID: 33058 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RegulationNotificator(object value)
		{
		}

		// Token: 0x06008123 RID: 33059 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RentalCardNotificator(object value)
		{
		}

		// Token: 0x06008124 RID: 33060 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int CardsCollectionGetValue(int id, string val)
		{
			return 0;
		}

		// Token: 0x06008125 RID: 33061 RVA: 0x000F1669 File Offset: 0x000EF869
		private static long CardsCollectionGetValueLong(int id, string val)
		{
			return 0L;
		}

		// Token: 0x06008126 RID: 33062 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int CardRarityGetValue(int id)
		{
			return 0;
		}

		// Token: 0x06008127 RID: 33063 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetSumOfCardInCollection(int id)
		{
			return 0;
		}

		// Token: 0x06008128 RID: 33064 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetCardsInCollection(bool noDupes)
		{
			return null;
		}

		// Token: 0x06008129 RID: 33065 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<CardBaseData> GetPremiumCardsInCollection(bool nonPrizeOnly = false)
		{
			return null;
		}

		// Token: 0x0600812A RID: 33066 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetPremiumCardTotal(int id, CardCollectionInfo.Premium prem, bool nonPrizeOnly = false)
		{
			return 0;
		}

		// Token: 0x0600812B RID: 33067 RVA: 0x000F6918 File Offset: 0x000F4B18
		public static ValueTuple<CardCollectionInfo.Premium, bool> GetOwnedHighPremium(int cardID, CardCollectionInfo.Premium maxPremium, int numCardInDeckNormal, int numCardInDeckPremium1, int numCardInDeckPremium2)
		{
			return default(ValueTuple<CardCollectionInfo.Premium, bool>);
		}

		// Token: 0x0600812C RID: 33068 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCardLimitByID(int id, int regulation)
		{
			return 0;
		}

		// Token: 0x0600812D RID: 33069 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCardRarityID(int id)
		{
			return 0;
		}

		// Token: 0x0600812E RID: 33070 RVA: 0x000029CC File Offset: 0x00000BCC
		public static CardCollectionInfo.Rarity GetCardRarity(int id)
		{
			return (CardCollectionInfo.Rarity)0;
		}

		// Token: 0x0600812F RID: 33071 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetCardRarityText(int rarityId)
		{
			return null;
		}

		// Token: 0x06008130 RID: 33072 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetCardRarityText(CardCollectionInfo.Rarity rarity)
		{
			return null;
		}

		// Token: 0x06008131 RID: 33073 RVA: 0x000029CC File Offset: 0x00000BCC
		public static IDS_CARD GetCardRarityTextId(CardCollectionInfo.Rarity rarity)
		{
			return IDS_CARD.RARITY_LABEL;
		}

		// Token: 0x06008132 RID: 33074 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetCardStyleText(int styleId)
		{
			return null;
		}

		// Token: 0x06008133 RID: 33075 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetCardStyleText(CardCollectionInfo.Premium style)
		{
			return null;
		}

		// Token: 0x06008134 RID: 33076 RVA: 0x000029CC File Offset: 0x00000BCC
		public static IDS_CARD GetCardStyleTextId(CardCollectionInfo.Premium style)
		{
			return IDS_CARD.RARITY_LABEL;
		}

		// Token: 0x06008135 RID: 33077 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCraftPoints(int id)
		{
			return 0;
		}

		// Token: 0x06008136 RID: 33078 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCardCreateCost(CardCollectionInfo.Premium prem, CardCollectionInfo.Rarity rarity)
		{
			return 0;
		}

		// Token: 0x06008137 RID: 33079 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCardDismantleCost(CardCollectionInfo.Premium prem, CardCollectionInfo.Rarity rarity)
		{
			return 0;
		}

		// Token: 0x06008138 RID: 33080 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsCreatable(CardBaseData data, int num = 1)
		{
			return false;
		}

		// Token: 0x06008139 RID: 33081 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDismantleable(CardBaseData data, int num = 1)
		{
			return false;
		}

		// Token: 0x0600813A RID: 33082 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsOverCraftPoint()
		{
			return false;
		}

		// Token: 0x0600813B RID: 33083 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<CardBaseData> GetAllPremiumCards(bool fullStyle)
		{
			return null;
		}

		// Token: 0x0600813C RID: 33084 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<CardBaseData> GetAllCards()
		{
			return null;
		}

		// Token: 0x0600813D RID: 33085 RVA: 0x000029CC File Offset: 0x00000BCC
		public static CardCollectionInfo.Regulation GetRegulationStatus(int cardID, int regulationID)
		{
			return CardCollectionInfo.Regulation.Forbidden;
		}

		// Token: 0x0600813E RID: 33086 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetRegulationName(int regId)
		{
			return null;
		}

		// Token: 0x0600813F RID: 33087 RVA: 0x000F37E7 File Offset: 0x000F19E7
		private static bool GetTextGroup(string fullTextId, out string groupId)
		{
			groupId = null;
			return false;
		}

		// Token: 0x06008140 RID: 33088 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRules()
		{
			return null;
		}

		// Token: 0x06008141 RID: 33089 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetRuleIds()
		{
			return null;
		}

		// Token: 0x06008142 RID: 33090 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetLatestRagulationID(int regulationID)
		{
			return 0;
		}

		// Token: 0x06008143 RID: 33091 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetLimitedCardMrks(int regulationID, CardCollectionInfo.Regulation limit)
		{
			return null;
		}

		// Token: 0x06008144 RID: 33092 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCardTimeObtained(int cardID)
		{
			return 0;
		}

		// Token: 0x06008145 RID: 33093 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsCraftableCard(int cardID)
		{
			return false;
		}

		// Token: 0x06008146 RID: 33094 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> FormatForMultiDismantle(int cardID, int n_num, int p1_num, int p2_num)
		{
			return null;
		}

		// Token: 0x06008147 RID: 33095 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> DismantleSingleCard(int cardID, int n_num, int p1_num, int p2_num)
		{
			return null;
		}

		// Token: 0x06008148 RID: 33096 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<CardBaseData, int> CardBaseDataListToCardBaseDataNumPairs(List<CardBaseData> cards)
		{
			return null;
		}

		// Token: 0x06008149 RID: 33097 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> DismantleMultiCards(List<CardBaseData> cards)
		{
			return null;
		}

		// Token: 0x0600814A RID: 33098 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> FormatForMassDismantle()
		{
			return null;
		}

		// Token: 0x0600814B RID: 33099 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> FormatForMultiCreate(int cardID, int n_num, int p1_num, int p2_num)
		{
			return null;
		}

		// Token: 0x0600814C RID: 33100 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> FormatForMultiCreate(Dictionary<int, int> lackCards)
		{
			return null;
		}

		// Token: 0x0600814D RID: 33101 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> CreateSingleCard(int cardID, CardCollectionInfo.Premium prem)
		{
			return null;
		}

		// Token: 0x0600814E RID: 33102 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetRedundantCards(int cardID, CardCollectionInfo.Premium prem)
		{
			return 0;
		}

		// Token: 0x0600814F RID: 33103 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<KeyValuePair<CardCollectionInfo.Rarity, CardCollectionInfo.Premium>, int> GetAllRedundantCards()
		{
			return null;
		}

		// Token: 0x06008150 RID: 33104 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<CardCollectionInfo.Rarity, int> GetEarnedPoints()
		{
			return null;
		}

		// Token: 0x06008151 RID: 33105 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<CardCollectionInfo.Rarity, int> GetEarnedPoints(List<CardBaseData> cards)
		{
			return null;
		}

		// Token: 0x06008152 RID: 33106 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<CardCollectionInfo.Rarity, int> GetNeedPoints(List<CardBaseData> cards)
		{
			return null;
		}

		// Token: 0x06008153 RID: 33107 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<KeyValuePair<CardCollectionInfo.Rarity, CardCollectionInfo.Premium>, int> GetRarityNumPairs(List<CardBaseData> cards)
		{
			return null;
		}

		// Token: 0x06008154 RID: 33108 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<CardCollectionInfo.SecretPackInfo> GetOpenedSecretPackInfo()
		{
			return null;
		}

		// Token: 0x06008155 RID: 33109 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<int, CardCollectionInfo.SecretPackInfo> GetOpenedSecretPacks()
		{
			return null;
		}

		// Token: 0x06008156 RID: 33110 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetRentalCardIDs(int rentalID)
		{
			return null;
		}

		// Token: 0x06008157 RID: 33111 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetRentalCardTotal(int rentalID)
		{
			return 0;
		}

		// Token: 0x06008158 RID: 33112 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetRentalCardNum(int rentalID, int cardID)
		{
			return 0;
		}

		// Token: 0x06008159 RID: 33113 RVA: 0x000F6930 File Offset: 0x000F4B30
		public static Color GetRentalColor()
		{
			return default(Color);
		}

		// Token: 0x0400B998 RID: 47512
		public const int REGULATIONID_UNLIMITED = 8;

		// Token: 0x0400B999 RID: 47513
		public const int REGULATIONID_UNDEFINED = -1;

		// Token: 0x0400B99A RID: 47514
		public const int CARDINVENTORYTHRESHOLD = 3;

		// Token: 0x0400B99B RID: 47515
		public const int MASSDISMANTLECAP = 1000;

		// Token: 0x0400B99C RID: 47516
		private static Content m_cci;

		// Token: 0x0400B99D RID: 47517
		public const string CLIENTWORK_PATH_CARDS_COLLECTION = "$.Cards.have";

		// Token: 0x0400B99E RID: 47518
		private const string CLIENTWORK_PATH_CARD_RARITY = "$.Master.CardRare";

		// Token: 0x0400B99F RID: 47519
		private const string CLIENTWORK_PATH_CARD_CRAFT = "$.Master.CardCr";

		// Token: 0x0400B9A0 RID: 47520
		private const string CLIENTWORK_PATH_CREATECOST = "$.Craft.generate_rate_list";

		// Token: 0x0400B9A1 RID: 47521
		private const string CLIENTWORK_PATH_DISMANTLECOST = "$.Craft.exchange_rate_list";

		// Token: 0x0400B9A2 RID: 47522
		private const string CLIENTWORK_PATH_CARDS_RENTAL = "$.Master.RentalCard";

		// Token: 0x0400B9A3 RID: 47523
		private const string KEY_RARITY = "r";

		// Token: 0x0400B9A4 RID: 47524
		private const string KEY_SUM = "tn";

		// Token: 0x0400B9A5 RID: 47525
		private const string KEY_PREM0 = "n";

		// Token: 0x0400B9A6 RID: 47526
		private const string KEY_PREM1 = "p1n";

		// Token: 0x0400B9A7 RID: 47527
		private const string KEY_PREM2 = "p2n";

		// Token: 0x0400B9A8 RID: 47528
		private const string KEY_PRIZE = "p_";

		// Token: 0x0400B9A9 RID: 47529
		private static Dictionary<string, CardCollectionInfo.Premium> premTbl;

		// Token: 0x0400B9AA RID: 47530
		private const string CLIENTWORK_PATH_TOURNAMENT_REGULATION = "$.Master.Regulation";

		// Token: 0x0400B9AB RID: 47531
		private const string CLIENTWORK_PATH_COMMON_FORBIDDEN = "$.Master.Regulation.common.available.a0";

		// Token: 0x0400B9AC RID: 47532
		private const string CLIENTWORK_PATH_COMMON_LIMITED = "$.Master.Regulation.common.available.a1";

		// Token: 0x0400B9AD RID: 47533
		private const string CLIENTWORK_PATH_COMMON_SEMILIMITED = "$.Master.Regulation.common.available.a2";

		// Token: 0x0400B9AE RID: 47534
		private const string KEY_COMMON = "common";

		// Token: 0x0400B9AF RID: 47535
		private const string KEY_AVAILABLE = "available";

		// Token: 0x0400B9B0 RID: 47536
		private const string KEY_A0 = "a0";

		// Token: 0x0400B9B1 RID: 47537
		private const string KEY_A1 = "a1";

		// Token: 0x0400B9B2 RID: 47538
		private const string KEY_A2 = "a2";

		// Token: 0x0400B9B3 RID: 47539
		private const string KEY_CARDPOOL = "card_pool";

		// Token: 0x0400B9B4 RID: 47540
		private const string KEY_MAIN = "main";

		// Token: 0x0400B9B5 RID: 47541
		private const string KEY_EXTRA = "extra";

		// Token: 0x0400B9B6 RID: 47542
		private const string KEY_REQUIRE = "require";

		// Token: 0x0400B9B7 RID: 47543
		private const string KEY_R1 = "r1";

		// Token: 0x0400B9B8 RID: 47544
		private const string KEY_R2 = "r2";

		// Token: 0x0400B9B9 RID: 47545
		private const string KEY_R3 = "r3";

		// Token: 0x0400B9BA RID: 47546
		private const string KEY_TIMESTAMP = "st";

		// Token: 0x0400B9BB RID: 47547
		private static Dictionary<string, object> m_cardsCollection;

		// Token: 0x0400B9BC RID: 47548
		private static Dictionary<string, object> m_cardRarity;

		// Token: 0x0400B9BD RID: 47549
		private static Dictionary<string, object> m_regulation;

		// Token: 0x0400B9BE RID: 47550
		private static List<int> m_cardCraft;

		// Token: 0x0400B9BF RID: 47551
		private static Dictionary<string, int> m_available;

		// Token: 0x0400B9C0 RID: 47552
		private static Dictionary<string, int> m_cardpool;

		// Token: 0x0400B9C1 RID: 47553
		private static Dictionary<string, int> m_useavailable;

		// Token: 0x0400B9C2 RID: 47554
		private static Dictionary<string, int> m_rental;

		// Token: 0x0400B9C3 RID: 47555
		private static Color m_rentalColor;

		// Token: 0x0400B9C4 RID: 47556
		private const string CLIENTWORK_PATH_CRAFTPOINT = "$.Item.have.";

		// Token: 0x0400B9C5 RID: 47557
		private const string CLIENTWORK_PATH_CRAFTSECRETPACK = "$.Craft.secret_pack_list";

		// Token: 0x020010F5 RID: 4341
		public enum Rarity
		{
			// Token: 0x0400B9C7 RID: 47559
			Normal = 1,
			// Token: 0x0400B9C8 RID: 47560
			Rare,
			// Token: 0x0400B9C9 RID: 47561
			SuperRare,
			// Token: 0x0400B9CA RID: 47562
			UltraRare
		}

		// Token: 0x020010F6 RID: 4342
		public enum Regulation
		{
			// Token: 0x0400B9CC RID: 47564
			Forbidden,
			// Token: 0x0400B9CD RID: 47565
			Limited,
			// Token: 0x0400B9CE RID: 47566
			SemiLimited,
			// Token: 0x0400B9CF RID: 47567
			None
		}

		// Token: 0x020010F7 RID: 4343
		public enum Premium
		{
			// Token: 0x0400B9D1 RID: 47569
			Normal = 1,
			// Token: 0x0400B9D2 RID: 47570
			Premium1,
			// Token: 0x0400B9D3 RID: 47571
			Premium2
		}

		// Token: 0x020010F8 RID: 4344
		public struct SecretPackInfo
		{
			// Token: 0x0600815B RID: 33115 RVA: 0x000029CC File Offset: 0x00000BCC
			public override int GetHashCode()
			{
				return 0;
			}

			// Token: 0x0400B9D4 RID: 47572
			public string nameTextId;

			// Token: 0x0400B9D5 RID: 47573
			public bool isExtend;

			// Token: 0x0400B9D6 RID: 47574
			public int shopID;

			// Token: 0x0400B9D7 RID: 47575
			public int freeNum;

			// Token: 0x0400B9D8 RID: 47576
			public int iconType;

			// Token: 0x0400B9D9 RID: 47577
			public string iconData;
		}
	}
}
