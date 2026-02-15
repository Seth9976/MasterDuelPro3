using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.Deck
{
	// Token: 0x02000FCE RID: 4046
	public class DeckInfo
	{
		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06007908 RID: 30984 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isReachDeckNumLimit
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007909 RID: 30985 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetDeckNum()
		{
			return 0;
		}

		// Token: 0x0600790A RID: 30986 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetDeckLimit()
		{
			return 0;
		}

		// Token: 0x0600790B RID: 30987 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> DeckSaveFormatParser(List<CardBaseData> main, List<CardBaseData> extra, List<CardBaseData> side, bool exceptRental = true)
		{
			return null;
		}

		// Token: 0x0600790C RID: 30988 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<CardBaseData> GetFavoriteCardBaseData()
		{
			return null;
		}

		// Token: 0x0600790D RID: 30989 RVA: 0x0000216A File Offset: 0x0000036A
		private static List<int> GetFavoriteCardIDList()
		{
			return null;
		}

		// Token: 0x0600790E RID: 30990 RVA: 0x0000216A File Offset: 0x0000036A
		private static List<int> GetFavoriteCardPremiumList()
		{
			return null;
		}

		// Token: 0x0600790F RID: 30991 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> FavoriteSaveFormatParser(List<CardBaseData> bookmark)
		{
			return null;
		}

		// Token: 0x06007910 RID: 30992 RVA: 0x0000216A File Offset: 0x0000036A
		public static GameObject CreateEmbedObj(Dictionary<string, object> args, ElementObjectManager eom, Transform transform)
		{
			return null;
		}

		// Token: 0x06007911 RID: 30993 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<int, string> GetRankEventDeckIDAndName()
		{
			return null;
		}

		// Token: 0x06007912 RID: 30994 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetRankEventDeckList(int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007913 RID: 30995 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetRankEventCardPremium(int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007914 RID: 30996 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<CardBaseData> GetRankEventCardBaseData(int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007915 RID: 30997 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRankEventDeckAccessory(int id)
		{
			return null;
		}

		// Token: 0x06007916 RID: 30998 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetCupDeckName()
		{
			return null;
		}

		// Token: 0x06007917 RID: 30999 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetWcsDeckName()
		{
			return null;
		}

		// Token: 0x06007918 RID: 31000 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetRankEventDeckNameByID(int id)
		{
			return null;
		}

		// Token: 0x06007919 RID: 31001 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRankEventPickUpCardIDs(int id)
		{
			return null;
		}

		// Token: 0x0600791A RID: 31002 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRankEventPickUpCardDecorations(int id)
		{
			return null;
		}

		// Token: 0x0600791B RID: 31003 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRankEventPickUps(int tid)
		{
			return null;
		}

		// Token: 0x0600791C RID: 31004 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<int, string> GetDeckIDAndName()
		{
			return null;
		}

		// Token: 0x0600791D RID: 31005 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetDeckNameByID(int id)
		{
			return null;
		}

		// Token: 0x0600791E RID: 31006 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetDeckListByID(int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x0600791F RID: 31007 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetCardPremiumByID(int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007920 RID: 31008 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<CardBaseData> GetCardBaseDataByID(int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007921 RID: 31009 RVA: 0x0000216A File Offset: 0x0000036A
		public static int[] GetDeckPickUpCardIDs(int id)
		{
			return null;
		}

		// Token: 0x06007922 RID: 31010 RVA: 0x0000216A File Offset: 0x0000036A
		public static int[] GetDeckPickUpCardDecorations(int id)
		{
			return null;
		}

		// Token: 0x06007923 RID: 31011 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDeckPickUpCardIDs2(int id)
		{
			return null;
		}

		// Token: 0x06007924 RID: 31012 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDeckPickUpCardDecorations2(int id)
		{
			return null;
		}

		// Token: 0x06007925 RID: 31013 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDeckPickUps(int id)
		{
			return null;
		}

		// Token: 0x06007926 RID: 31014 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetDeckEditTime(int id)
		{
			return 0L;
		}

		// Token: 0x06007927 RID: 31015 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetDeckCreateTime(int id)
		{
			return 0L;
		}

		// Token: 0x06007928 RID: 31016 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDeckAccessory(int id)
		{
			return null;
		}

		// Token: 0x06007929 RID: 31017 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetExhibitionNameByID(int id)
		{
			return null;
		}

		// Token: 0x0600792A RID: 31018 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetExhibitionDeckList(int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x0600792B RID: 31019 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetExhibitionCardPremium(int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x0600792C RID: 31020 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<CardBaseData> GetExhibitionCardBaseData(int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x0600792D RID: 31021 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetExhibitionDeckAccessory(int id)
		{
			return null;
		}

		// Token: 0x0600792E RID: 31022 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetExhibitionPickUpCardIDs(int id)
		{
			return null;
		}

		// Token: 0x0600792F RID: 31023 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetExhibitionPickUpCardDecorations(int id)
		{
			return null;
		}

		// Token: 0x06007930 RID: 31024 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetExhibitionPickUps(int exhid)
		{
			return null;
		}

		// Token: 0x06007931 RID: 31025 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<int, string> GetExhibitionDeckIDAndName()
		{
			return null;
		}

		// Token: 0x06007932 RID: 31026 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetRentalDeckIDs(int exhid)
		{
			return null;
		}

		// Token: 0x06007933 RID: 31027 RVA: 0x0000216A File Offset: 0x0000036A
		private static List<int> GetRentalDeckList(int exhid, int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007934 RID: 31028 RVA: 0x0000216A File Offset: 0x0000036A
		private static List<int> GetRentalCardPremium(int exhid, int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007935 RID: 31029 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<CardBaseData> GetRentalCardBaseData(int exhid, int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007936 RID: 31030 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRentalDeckAccessory(int exhid, int id)
		{
			return null;
		}

		// Token: 0x06007937 RID: 31031 RVA: 0x0000216A File Offset: 0x0000036A
		private static Dictionary<string, object> GetRentalPickUpCardIDs(int exhid, int id)
		{
			return null;
		}

		// Token: 0x06007938 RID: 31032 RVA: 0x0000216A File Offset: 0x0000036A
		public static int[] GetRentalPickUpCardIDArray(int exhid, int id)
		{
			return null;
		}

		// Token: 0x06007939 RID: 31033 RVA: 0x0000216A File Offset: 0x0000036A
		private static Dictionary<string, object> GetRentalPickUpCardDecorations(int exhid, int id)
		{
			return null;
		}

		// Token: 0x0600793A RID: 31034 RVA: 0x0000216A File Offset: 0x0000036A
		public static int[] GetRentalPickUpCardDecorationArray(int exhid, int id)
		{
			return null;
		}

		// Token: 0x0600793B RID: 31035 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRentalPickUps(int exhid, int id)
		{
			return null;
		}

		// Token: 0x0600793C RID: 31036 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetRentalDeckName(int exhid, int id)
		{
			return null;
		}

		// Token: 0x0600793D RID: 31037 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetRentalDeckDesc(int exhid, int id)
		{
			return null;
		}

		// Token: 0x0600793E RID: 31038 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetDuelTrialDeckList(int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x0600793F RID: 31039 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetDuelTrialCardPremium(int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007940 RID: 31040 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<CardBaseData> GetDuelTrialCardBaseData(int id, DeckInfo.DeckType type, List<int> rentalList = null)
		{
			return null;
		}

		// Token: 0x06007941 RID: 31041 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetDuelTrialDeckIDs(int dtid)
		{
			return null;
		}

		// Token: 0x06007942 RID: 31042 RVA: 0x0000216A File Offset: 0x0000036A
		private static List<int> GetDuelTrialDeckList(int dtid, int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007943 RID: 31043 RVA: 0x0000216A File Offset: 0x0000036A
		private static List<int> GetDuelTrialCardPremium(int dtid, int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007944 RID: 31044 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<CardBaseData> GetDuelTrialCardBaseData(int dtid, int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007945 RID: 31045 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelTrialDeckAccessory(int dtid, int id)
		{
			return null;
		}

		// Token: 0x06007946 RID: 31046 RVA: 0x0000216A File Offset: 0x0000036A
		private static Dictionary<string, object> GetDuelTrialPickUpCardIDs(int dtid, int id)
		{
			return null;
		}

		// Token: 0x06007947 RID: 31047 RVA: 0x0000216A File Offset: 0x0000036A
		public static int[] GetDuelTrialPickUpCardIDArray(int dtid, int id)
		{
			return null;
		}

		// Token: 0x06007948 RID: 31048 RVA: 0x0000216A File Offset: 0x0000036A
		private static Dictionary<string, object> GetDuelTrialPickUpCardDecorations(int dtid, int id)
		{
			return null;
		}

		// Token: 0x06007949 RID: 31049 RVA: 0x0000216A File Offset: 0x0000036A
		public static int[] GetDuelTrialPickUpCardDecorationArray(int dtid, int id)
		{
			return null;
		}

		// Token: 0x0600794A RID: 31050 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelTrialPickUps(int dtid, int id)
		{
			return null;
		}

		// Token: 0x0600794B RID: 31051 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetDuelTrialDeckName(int dtid, int id)
		{
			return null;
		}

		// Token: 0x0600794C RID: 31052 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetDuelTrialDeckDesc(int dtid, int id)
		{
			return null;
		}

		// Token: 0x0600794D RID: 31053 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetVersusNameByID(int id)
		{
			return null;
		}

		// Token: 0x0600794E RID: 31054 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetVersusDeckList(int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x0600794F RID: 31055 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetVersusCardPremium(int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007950 RID: 31056 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<CardBaseData> GetVersusCardBaseData(int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007951 RID: 31057 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetVersusDeckAccessory(int id)
		{
			return null;
		}

		// Token: 0x06007952 RID: 31058 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetVersusPickUpCardIDs(int id)
		{
			return null;
		}

		// Token: 0x06007953 RID: 31059 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetVersusPickUpCardDecorations(int id)
		{
			return null;
		}

		// Token: 0x06007954 RID: 31060 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetVersusPickUps(int exhid)
		{
			return null;
		}

		// Token: 0x06007955 RID: 31061 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetVersusRentalDeckIDs(int vid, int groupId)
		{
			return null;
		}

		// Token: 0x06007956 RID: 31062 RVA: 0x0000216A File Offset: 0x0000036A
		private static List<int> GetVersusRentalDeckList(int vid, int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007957 RID: 31063 RVA: 0x0000216A File Offset: 0x0000036A
		private static List<int> GetVersusRentalCardPremium(int exhid, int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007958 RID: 31064 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<CardBaseData> GetVersusRentalCardBaseData(int exhid, int id, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007959 RID: 31065 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetVersusRentalDeckAccessory(int vid, int id)
		{
			return null;
		}

		// Token: 0x0600795A RID: 31066 RVA: 0x0000216A File Offset: 0x0000036A
		private static Dictionary<string, object> GetVersusRentalPickUpCardIDs(int vid, int id)
		{
			return null;
		}

		// Token: 0x0600795B RID: 31067 RVA: 0x0000216A File Offset: 0x0000036A
		public static int[] GetVersusRentalPickUpCardIDArray(int vid, int id)
		{
			return null;
		}

		// Token: 0x0600795C RID: 31068 RVA: 0x0000216A File Offset: 0x0000036A
		private static Dictionary<string, object> GetVersusRentalPickUpCardDecorations(int vid, int id)
		{
			return null;
		}

		// Token: 0x0600795D RID: 31069 RVA: 0x0000216A File Offset: 0x0000036A
		public static int[] GetVersusRentalPickUpCardDecorationArray(int vid, int id)
		{
			return null;
		}

		// Token: 0x0600795E RID: 31070 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetVersusRentalPickUps(int exhid, int id)
		{
			return null;
		}

		// Token: 0x0600795F RID: 31071 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetVersusRentalDeckName(int vid, int id)
		{
			return null;
		}

		// Token: 0x06007960 RID: 31072 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetVersusRentalDeckDesc(int exhid, int id)
		{
			return null;
		}

		// Token: 0x06007961 RID: 31073 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetReplayDeckList(long did, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007962 RID: 31074 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetReplayCardPremium(long did, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007963 RID: 31075 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<CardBaseData> GetReplayCardBaseData(int did, DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x0400B0D6 RID: 45270
		private const string CLIENTWORK_PATH_DECK_LISTS = "$.Deck.list";

		// Token: 0x0400B0D7 RID: 45271
		private const string CLIENTWORK_PATH_DECKLIST = "$.DeckList";

		// Token: 0x0400B0D8 RID: 45272
		private const string CLIENTWORK_PATH_TOURNAMENT = "$.TDeckList";

		// Token: 0x0400B0D9 RID: 45273
		private const string CLIENTWORK_PATH_TOURNAMENT_DECK_LIST = "$.TDeck.list";

		// Token: 0x0400B0DA RID: 45274
		private const string CLIENTWORK_PATH_EXHIBITION = "$.EXHDeckList";

		// Token: 0x0400B0DB RID: 45275
		private const string CLIENTWORK_PATH_EXHIBITION_DECK_LIST = "$.EXHDeck.list";

		// Token: 0x0400B0DC RID: 45276
		private const string CLIENTWORK_PATH_CUP = "$.CUPDeckList";

		// Token: 0x0400B0DD RID: 45277
		private const string CLIENTWORK_PATH_CUP_DECK_LIST = "$.CUPDeck";

		// Token: 0x0400B0DE RID: 45278
		private const string CLIENTWORK_PATH_RANKEVENT = "$.REDeckList";

		// Token: 0x0400B0DF RID: 45279
		private const string CLIENTWORK_PATH_RANKEVENT_DECK_LIST = "$.REDeck.list";

		// Token: 0x0400B0E0 RID: 45280
		private const string CLIENTWORK_PATH_DUELTRIAL = "$.DTDeckList";

		// Token: 0x0400B0E1 RID: 45281
		private const string CLIENTWORK_PATH_DUELTRIAL_DECK_LIST = "$.DTDeck.list";

		// Token: 0x0400B0E2 RID: 45282
		private const string CLIENTWORK_PATH_VERSUS = "$.VDeckList";

		// Token: 0x0400B0E3 RID: 45283
		private const string CLIENTWORK_PATH_VERSUS_DECK_LIST = "$.VDeck.list";

		// Token: 0x0400B0E4 RID: 45284
		private const string CLIENTWORK_PATH_CARD_FAVORITE = "$.Cards.favorite.";

		// Token: 0x0400B0E5 RID: 45285
		private const string KEY_DECK_COUNT = "num";

		// Token: 0x0400B0E6 RID: 45286
		private const string KEY_DECK_MAX = "empty";

		// Token: 0x0400B0E7 RID: 45287
		private const string KEY_DECK_ID = "deck_id";

		// Token: 0x0400B0E8 RID: 45288
		private const string KEY_DECK_NAME = "name";

		// Token: 0x0400B0E9 RID: 45289
		private const string KEY_DECK_REG = "regulation_id";

		// Token: 0x0400B0EA RID: 45290
		private const string KEY_DECKLIST_MAIN = "m";

		// Token: 0x0400B0EB RID: 45291
		private const string KEY_DECKLIST_EXTRA = "e";

		// Token: 0x0400B0EC RID: 45292
		private const string KEY_DECKLIST_SIDE = "s";

		// Token: 0x0400B0ED RID: 45293
		private const string KEY_DECKLIST_TRAY = "t";

		// Token: 0x0400B0EE RID: 45294
		private const string KEY_DECKLIST_CARDIDS = "ids";

		// Token: 0x0400B0EF RID: 45295
		private const string KEY_DECKLIST_PREMIUMIDS = "r";

		// Token: 0x0400B0F0 RID: 45296
		private const string KEY_TOURNAMENT_DECKLIST = "DeckList.1";

		// Token: 0x0400B0F1 RID: 45297
		private const string KEY_CARD_FAVOTITE_CARDIDS = "ids";

		// Token: 0x0400B0F2 RID: 45298
		private const string KEY_CARD_FAVOTITE_PREMIUMIDS = "r";

		// Token: 0x0400B0F3 RID: 45299
		private const string KEY_CARD_FAVOTITE_CARDLIST = "card_list";

		// Token: 0x0400B0F4 RID: 45300
		public static readonly int PICKUPCARDS_NUM;

		// Token: 0x0400B0F5 RID: 45301
		public static DeckInfo.MyDeckInfo myDeckInfo;

		// Token: 0x0400B0F6 RID: 45302
		public static DeckInfo.ExhibitionDeckInfo exhibitionDeckInfo;

		// Token: 0x0400B0F7 RID: 45303
		public static DeckInfo.DuelistCupDeckInfo duelistCupDeckInfo;

		// Token: 0x0400B0F8 RID: 45304
		public static DeckInfo.WCSDeckInfo wcsCupDeckInfo;

		// Token: 0x0400B0F9 RID: 45305
		public static DeckInfo.WCSFinalDeckInfo wcsFinalDeckInfo;

		// Token: 0x0400B0FA RID: 45306
		public static DeckInfo.RankEventDeckInfo rankEventDeckInfo;

		// Token: 0x0400B0FB RID: 45307
		public static DeckInfo.DuelTrialDeckInfo duelTrialDeckInfo;

		// Token: 0x0400B0FC RID: 45308
		public static DeckInfo.VersusDeckInfo versusDeckInfo;

		// Token: 0x0400B0FD RID: 45309
		public static DeckInfo.RentalDeckInfo rentalDeckInfo;

		// Token: 0x0400B0FE RID: 45310
		public static DeckInfo.DuelTrialRentalDeckInfo duelTrialRentalDeckInfo;

		// Token: 0x0400B0FF RID: 45311
		public static DeckInfo.VersusRentalDeckInfo versusRentalDeckInfo;

		// Token: 0x0400B100 RID: 45312
		public static DeckInfo.ReplayDeckInfo replayDeckInfo;

		// Token: 0x0400B101 RID: 45313
		public const string k_ArgsKeyDeckCase = "box";

		// Token: 0x0400B102 RID: 45314
		public const string k_ArgsKeyProtector = "sleeve";

		// Token: 0x0400B103 RID: 45315
		public const string k_ArgsKeyField = "field";

		// Token: 0x0400B104 RID: 45316
		public const string k_ArgsKeyObject = "object";

		// Token: 0x0400B105 RID: 45317
		public const string k_ArgsKeyMateBase = "av_base";

		// Token: 0x0400B106 RID: 45318
		public const string k_ArgsKeyPickIds = "pickIds";

		// Token: 0x0400B107 RID: 45319
		public const string k_ArgsKeyPickDecos = "pickDecos";

		// Token: 0x0400B108 RID: 45320
		public const string k_ArgsKeyRegulation = "regulation";

		// Token: 0x0400B109 RID: 45321
		public const string k_ArgsKeyEvent = "event";

		// Token: 0x0400B10A RID: 45322
		public const string k_ArgsKeyLogo = "logo";

		// Token: 0x0400B10B RID: 45323
		public const string k_ArgsKeyStage = "stage";

		// Token: 0x0400B10C RID: 45324
		public const string k_ArgsKeyName = "name";

		// Token: 0x02000FCF RID: 4047
		public enum DeckType
		{
			// Token: 0x0400B10E RID: 45326
			M,
			// Token: 0x0400B10F RID: 45327
			E,
			// Token: 0x0400B110 RID: 45328
			S,
			// Token: 0x0400B111 RID: 45329
			T
		}

		// Token: 0x02000FD0 RID: 4048
		public abstract class BaseDeckInfo
		{
			// Token: 0x06007965 RID: 31077 RVA: 0x0000216A File Offset: 0x0000036A
			protected string GetStrDeckType(DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x06007966 RID: 31078 RVA: 0x0000216A File Offset: 0x0000036A
			public virtual Dictionary<int, string> GetDeckIDAndName()
			{
				return null;
			}

			// Token: 0x06007967 RID: 31079 RVA: 0x0000216A File Offset: 0x0000036A
			public virtual string GetDeckNameByID(int id)
			{
				return null;
			}

			// Token: 0x06007968 RID: 31080 RVA: 0x0000216A File Offset: 0x0000036A
			public virtual List<int> GetDeckListByID(int id, DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x06007969 RID: 31081 RVA: 0x0000216A File Offset: 0x0000036A
			public virtual List<int> GetCardPremiumByID(int id, DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x0600796A RID: 31082 RVA: 0x0000216A File Offset: 0x0000036A
			public virtual Dictionary<string, object> GetDeckDictByID(int deckID, DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x0600796B RID: 31083 RVA: 0x0000216A File Offset: 0x0000036A
			public virtual Dictionary<string, object> GetDeckDictByID(int deckID, int eventID, DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x0600796C RID: 31084 RVA: 0x0000216A File Offset: 0x0000036A
			public virtual List<CardBaseData> GetCardBaseListByID(int id, DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x0600796D RID: 31085 RVA: 0x0000216A File Offset: 0x0000036A
			public virtual Dictionary<string, object> GetAccessory(int id)
			{
				return null;
			}

			// Token: 0x0600796E RID: 31086 RVA: 0x0000216A File Offset: 0x0000036A
			public virtual int[] GetPickUpCardIDs(int id)
			{
				return null;
			}

			// Token: 0x0600796F RID: 31087 RVA: 0x0000216A File Offset: 0x0000036A
			public virtual int[] GetPickUpCardPremiums(int id)
			{
				return null;
			}

			// Token: 0x06007970 RID: 31088 RVA: 0x0000216A File Offset: 0x0000036A
			public virtual Dictionary<string, object> GetPickUpCardIDs2(int id)
			{
				return null;
			}

			// Token: 0x06007971 RID: 31089 RVA: 0x0000216A File Offset: 0x0000036A
			public virtual Dictionary<string, object> GetPickUpCardPremiums2(int id)
			{
				return null;
			}

			// Token: 0x06007972 RID: 31090 RVA: 0x0000216A File Offset: 0x0000036A
			public virtual Dictionary<string, object> GetPickUps(int id)
			{
				return null;
			}

			// Token: 0x06007973 RID: 31091 RVA: 0x000F1669 File Offset: 0x000EF869
			public virtual long GetEditTime(int id)
			{
				return 0L;
			}

			// Token: 0x06007974 RID: 31092 RVA: 0x000F1669 File Offset: 0x000EF869
			public virtual long GetDeckCreateTime(int id)
			{
				return 0L;
			}

			// Token: 0x0400B112 RID: 45330
			protected const string KEY_M = "m";

			// Token: 0x0400B113 RID: 45331
			protected const string KEY_E = "e";

			// Token: 0x0400B114 RID: 45332
			protected const string KEY_S = "s";

			// Token: 0x0400B115 RID: 45333
			protected const string KEY_T = "t";

			// Token: 0x0400B116 RID: 45334
			protected const string KEY_IDS = "ids";

			// Token: 0x0400B117 RID: 45335
			protected const string KEY_R = "r";

			// Token: 0x0400B118 RID: 45336
			protected const string KEY_DECKID = "deck_id";

			// Token: 0x0400B119 RID: 45337
			protected const string KEY_NAME = "name";

			// Token: 0x0400B11A RID: 45338
			protected const string KEY_ACCESSORY = "accessory";

			// Token: 0x0400B11B RID: 45339
			protected const string KEY_PICKCARDS = "pick_cards";

			// Token: 0x0400B11C RID: 45340
			protected const string KEY_EDITTIME = "et";

			// Token: 0x0400B11D RID: 45341
			protected const string KEY_CREATETIME = "ct";

			// Token: 0x0400B11E RID: 45342
			protected const string KEY_REG = "regulation_id";

			// Token: 0x0400B11F RID: 45343
			protected const string KEY_NAME_REG_ID = "name_reg_id";

			// Token: 0x0400B120 RID: 45344
			protected string deckPath;

			// Token: 0x0400B121 RID: 45345
			protected string listPath;
		}

		// Token: 0x02000FD1 RID: 4049
		public class MyDeckInfo : DeckInfo.BaseDeckInfo
		{
			// Token: 0x06007976 RID: 31094 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetDeckRegByID(int id)
			{
				return 0;
			}
		}

		// Token: 0x02000FD2 RID: 4050
		public class ExhibitionDeckInfo : DeckInfo.BaseDeckInfo
		{
		}

		// Token: 0x02000FD3 RID: 4051
		public class DuelistCupDeckInfo : DeckInfo.BaseDeckInfo
		{
			// Token: 0x06007979 RID: 31097 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetQualifierCupPath()
			{
			}

			// Token: 0x0600797A RID: 31098 RVA: 0x0000216A File Offset: 0x0000036A
			public new Dictionary<int, string> GetDeckIDAndName()
			{
				return null;
			}

			// Token: 0x0600797B RID: 31099 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetDeckName()
			{
				return null;
			}

			// Token: 0x0600797C RID: 31100 RVA: 0x0000216A File Offset: 0x0000036A
			public virtual Dictionary<string, object> GetDeckDict(DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x0600797D RID: 31101 RVA: 0x0000216A File Offset: 0x0000036A
			public List<int> GetDeckListByID(DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x0600797E RID: 31102 RVA: 0x0000216A File Offset: 0x0000036A
			public List<int> GetCardPremiumByID(DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x0600797F RID: 31103 RVA: 0x0000216A File Offset: 0x0000036A
			public List<CardBaseData> GetCardBaseListByID(DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x06007980 RID: 31104 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetAccessory()
			{
				return null;
			}

			// Token: 0x06007981 RID: 31105 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUpCardIDs()
			{
				return null;
			}

			// Token: 0x06007982 RID: 31106 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUpCardPremiums()
			{
				return null;
			}

			// Token: 0x06007983 RID: 31107 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUps()
			{
				return null;
			}

			// Token: 0x06007984 RID: 31108 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetStage()
			{
				return 0;
			}
		}

		// Token: 0x02000FD4 RID: 4052
		public class WCSDeckInfo : DeckInfo.DuelistCupDeckInfo
		{
			// Token: 0x06007986 RID: 31110 RVA: 0x0000216A File Offset: 0x0000036A
			public new Dictionary<int, string> GetDeckIDAndName()
			{
				return null;
			}
		}

		// Token: 0x02000FD5 RID: 4053
		public class WCSFinalDeckInfo : DeckInfo.WCSDeckInfo
		{
			// Token: 0x06007988 RID: 31112 RVA: 0x0000216A File Offset: 0x0000036A
			public new Dictionary<int, string> GetDeckIDAndName()
			{
				return null;
			}

			// Token: 0x06007989 RID: 31113 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetDeckDict(DeckInfo.DeckType type, int index, int slot)
			{
				return null;
			}
		}

		// Token: 0x02000FD6 RID: 4054
		public class RankEventDeckInfo : DeckInfo.BaseDeckInfo
		{
		}

		// Token: 0x02000FD7 RID: 4055
		public class DuelTrialDeckInfo : DeckInfo.BaseDeckInfo
		{
			// Token: 0x0600798C RID: 31116 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetDeckNameByID(int trialID, int deckID = 1)
			{
				return null;
			}

			// Token: 0x0600798D RID: 31117 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetAccessory(int trialID, int deckID = 1)
			{
				return null;
			}

			// Token: 0x0600798E RID: 31118 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUpCardIDs(int trialID, int deckID = 1)
			{
				return null;
			}

			// Token: 0x0600798F RID: 31119 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUpCardDecorations(int trialID, int deckID = 1)
			{
				return null;
			}

			// Token: 0x06007990 RID: 31120 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUps(int trialID, int deckID = 1)
			{
				return null;
			}

			// Token: 0x06007991 RID: 31121 RVA: 0x0000216A File Offset: 0x0000036A
			public new Dictionary<KeyValuePair<int, int>, string> GetDeckIDAndName()
			{
				return null;
			}

			// Token: 0x06007992 RID: 31122 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<int, string> GetDeckIDAndNameByTrialID(int trialID)
			{
				return null;
			}

			// Token: 0x06007993 RID: 31123 RVA: 0x0000216A File Offset: 0x0000036A
			public List<int> GetDeckListByID(int trialID, int deckID, DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x06007994 RID: 31124 RVA: 0x0000216A File Offset: 0x0000036A
			public List<int> GetCardPremiumByID(int trialID, int deckID, DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x06007995 RID: 31125 RVA: 0x0000216A File Offset: 0x0000036A
			public List<CardBaseData> GetCardBaseListByID(int trialID, int deckID, DeckInfo.DeckType type)
			{
				return null;
			}
		}

		// Token: 0x02000FD8 RID: 4056
		public class VersusDeckInfo : DeckInfo.BaseDeckInfo
		{
			// Token: 0x06007997 RID: 31127 RVA: 0x0000216A File Offset: 0x0000036A
			public new Dictionary<KeyValuePair<int, int>, string> GetDeckIDAndName()
			{
				return null;
			}

			// Token: 0x06007998 RID: 31128 RVA: 0x0000216A File Offset: 0x0000036A
			public List<CardBaseData> GetCardBaseListByID(int versusId, int groupId, DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x06007999 RID: 31129 RVA: 0x0000216A File Offset: 0x0000036A
			public List<int> GetDeckListByID(int versusId, int groupId, DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x0600799A RID: 31130 RVA: 0x0000216A File Offset: 0x0000036A
			public List<int> GetCardPremiumByID(int versusId, int groupId, DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x0600799B RID: 31131 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetAccessory(int versusId, int versusGroupId)
			{
				return null;
			}

			// Token: 0x0600799C RID: 31132 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUps(int versusId, int versusGroupId)
			{
				return null;
			}

			// Token: 0x0600799D RID: 31133 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUpCardIDs(int versusId, int versusGroupId)
			{
				return null;
			}

			// Token: 0x0600799E RID: 31134 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUpCardPremiums(int versusId, int versusGroupId)
			{
				return null;
			}

			// Token: 0x0600799F RID: 31135 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetDeckNameByID(int versusID, int groupID = 1)
			{
				return null;
			}
		}

		// Token: 0x02000FD9 RID: 4057
		public class RentalDeckInfo : DeckInfo.BaseDeckInfo
		{
			// Token: 0x060079A1 RID: 31137 RVA: 0x0000216A File Offset: 0x0000036A
			public static List<int> GetRentalDeckIDs(int exhid)
			{
				return null;
			}

			// Token: 0x060079A2 RID: 31138 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetDeckNameByID(int eventID, int deckID)
			{
				return null;
			}

			// Token: 0x060079A3 RID: 31139 RVA: 0x0000216A File Offset: 0x0000036A
			public static string GetDeckDesc(int eventID, int deckID)
			{
				return null;
			}

			// Token: 0x060079A4 RID: 31140 RVA: 0x0000216A File Offset: 0x0000036A
			public new Dictionary<string, object> GetDeckDictByID(int deckID, int eventID, DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x060079A5 RID: 31141 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetAccessory(int eventID, int deckID)
			{
				return null;
			}

			// Token: 0x060079A6 RID: 31142 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUpCardIDs(int eventID, int deckID)
			{
				return null;
			}

			// Token: 0x060079A7 RID: 31143 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUpCardDecorations(int eventID, int deckID)
			{
				return null;
			}

			// Token: 0x060079A8 RID: 31144 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUps(int eventID, int deckID)
			{
				return null;
			}
		}

		// Token: 0x02000FDA RID: 4058
		public class DuelTrialRentalDeckInfo : DeckInfo.BaseDeckInfo
		{
			// Token: 0x060079AA RID: 31146 RVA: 0x0000216A File Offset: 0x0000036A
			public static List<int> GetRentalDeckIDs(int exhid)
			{
				return null;
			}

			// Token: 0x060079AB RID: 31147 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetDeckNameByID(int eventID, int deckID)
			{
				return null;
			}

			// Token: 0x060079AC RID: 31148 RVA: 0x0000216A File Offset: 0x0000036A
			public static string GetDeckDesc(int eventID, int deckID)
			{
				return null;
			}

			// Token: 0x060079AD RID: 31149 RVA: 0x0000216A File Offset: 0x0000036A
			public new Dictionary<string, object> GetDeckDictByID(int deckID, int eventID, DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x060079AE RID: 31150 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetAccessory(int eventID, int deckID)
			{
				return null;
			}

			// Token: 0x060079AF RID: 31151 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUpCardIDs(int eventID, int deckID)
			{
				return null;
			}

			// Token: 0x060079B0 RID: 31152 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUpCardDecorations(int eventID, int deckID)
			{
				return null;
			}

			// Token: 0x060079B1 RID: 31153 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUps(int eventID, int deckID)
			{
				return null;
			}
		}

		// Token: 0x02000FDB RID: 4059
		public class VersusRentalDeckInfo : DeckInfo.BaseDeckInfo
		{
			// Token: 0x060079B3 RID: 31155 RVA: 0x0000216A File Offset: 0x0000036A
			public static List<int> GetRentalDeckIDs(int vid, int groupId)
			{
				return null;
			}

			// Token: 0x060079B4 RID: 31156 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetDeckNameByID(int eventID, int deckID)
			{
				return null;
			}

			// Token: 0x060079B5 RID: 31157 RVA: 0x0000216A File Offset: 0x0000036A
			public new Dictionary<string, object> GetDeckDictByID(int deckID, int eventID, DeckInfo.DeckType type)
			{
				return null;
			}

			// Token: 0x060079B6 RID: 31158 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetAccessory(int eventID, int deckID)
			{
				return null;
			}

			// Token: 0x060079B7 RID: 31159 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUpCardIDs(int eventID, int deckID)
			{
				return null;
			}

			// Token: 0x060079B8 RID: 31160 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUpCardDecorations(int eventID, int deckID)
			{
				return null;
			}

			// Token: 0x060079B9 RID: 31161 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> GetPickUps(int eventID, int deckID)
			{
				return null;
			}
		}

		// Token: 0x02000FDC RID: 4060
		public class ReplayDeckInfo : DeckInfo.BaseDeckInfo
		{
		}
	}
}
