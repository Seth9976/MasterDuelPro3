using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.CardPack;
using YgomGame.Colosseum;
using YgomGame.Duel;
using YgomGame.Menu;
using YgomGame.Shop;

namespace YgomSystem.Utility
{
	// Token: 0x02000512 RID: 1298
	public static class ClientWorkUtil
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool HasServerTimeLag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsExistAccount()
		{
			return false;
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetToken()
		{
			return null;
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPushNotificationToken(string token)
		{
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetPushNotificationToken()
		{
			return null;
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetAdId()
		{
			return null;
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetSocialId(string id)
		{
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetSocialId()
		{
			return null;
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetLanguage(string lang, bool noSave = false)
		{
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetMyPlayerCode()
		{
			return 0L;
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetMyPlayerCodeStringForAppIdentifier()
		{
			return null;
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsCrossPlayOK()
		{
			return false;
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsCrossPlayOKOnGame()
		{
			return false;
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMultiPlayOKByPlatform()
		{
			return false;
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsCrossPlayOKByPlatform()
		{
			return false;
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsOptOut()
		{
			return false;
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool DownloadCompleted()
		{
			return false;
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DownloadFinish()
		{
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<string> GetDownloadList()
		{
			return null;
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<Dictionary<string, string>> GetDownloadUrls()
		{
			return null;
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetDownloadRevision()
		{
			return null;
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetDownloadPass()
		{
			return null;
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetDownloadSalt()
		{
			return null;
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetLocalDownloadRevision()
		{
			return null;
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetLocalDownladListIndex()
		{
			return 0;
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetFirstDownloadNum()
		{
			return 0;
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetShouldDownload()
		{
			return false;
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetTakeOverUrl()
		{
			return null;
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetRegistUrl()
		{
			return null;
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetInheritEnable()
		{
			return false;
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDoubleNotationCandidate()
		{
			return false;
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsRealtimeDuelWatchAvailable()
		{
			return false;
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetPvpRank()
		{
			return 0;
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetChatEnable()
		{
			return false;
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ObjectToInt(object i)
		{
			return 0;
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float ObjectToFloat(object i)
		{
			return 0f;
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x000F1A6C File Offset: 0x000EFC6C
		public static Color ObjectToColor(object c)
		{
			return default(Color);
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x000F1A84 File Offset: 0x000EFC84
		public static Vector2 ObjectToVector2(object c)
		{
			return default(Vector2);
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x0000216A File Offset: 0x0000036A
		public static T[] ListToTypedArray<T>(List<object> dic)
		{
			return null;
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x0000216A File Offset: 0x0000036A
		public static int[] ListToIntArray(List<object> dic)
		{
			return null;
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetUnixTime()
		{
			return 0L;
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x000F1669 File Offset: 0x000EF869
		private static long GetRawUnixTime()
		{
			return 0L;
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ServerTimeNotificator(object v)
		{
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int getIntWithCache(string jsonPath, int defaultValue = 0)
		{
			return 0;
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float getFloatWithCache(string jsonPath, float defaultValue = 0f)
		{
			return 0f;
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x0000216A File Offset: 0x0000036A
		public static string getStringWithCache(string jsonPath, string defaultValue = "")
		{
			return null;
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool getBoolByJsonPathWithCache(string jsonPath, bool defaultValue = false)
		{
			return false;
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> getDictionaryWithCache(string jsonPath, Dictionary<string, object> defaultValue = null)
		{
			return null;
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> getListWithCache(string jsonPath, List<object> defaultValue = null)
		{
			return null;
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x0000216A File Offset: 0x0000036A
		public static object getObjectWithCache(string jsonPath, object defaultValue = null)
		{
			return null;
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RegisterNotificator()
		{
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetData()
		{
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetPeriodItemName(int pCategory, int pItemId)
		{
			return null;
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetPeriodItemThumb(int pCategory, int pItemId)
		{
			return null;
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetPeriodLimitDateTs(int pCategory, int pItemId)
		{
			return 0L;
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetRankLabelId(int rank)
		{
			return null;
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetRankLabel(int rank, int tier)
		{
			return null;
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetCardTermData(int mrk, int termType)
		{
			return null;
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetCardTermData(int termId)
		{
			return null;
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x0000216A File Offset: 0x0000036A
		public static IReadOnlyDictionary<string, object> GetStructureFirstData()
		{
			return null;
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x0000216A File Offset: 0x0000036A
		public static object GetStructureMaster(int structureId)
		{
			return null;
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetUserProfile()
		{
			return null;
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetUserName()
		{
			return null;
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetUserLevel(int defaultValue = 1)
		{
			return 0;
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetUserRank(int defaultValue = 1)
		{
			return 0;
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetUserTier(int defaultValue = 5)
		{
			return 0;
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetUserWallpaper(int defaultValue = 1)
		{
			return 0;
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetUserPcode()
		{
			return 0L;
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetFriendProfile()
		{
			return null;
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetUserReplays()
		{
			return null;
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetFriendReplays()
		{
			return null;
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsFriendInBattle(object friendData)
		{
			return false;
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsFriendOnline(object friendData)
		{
			return false;
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetUserHistory(int mode)
		{
			return null;
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetUserHistoryReplayLimit()
		{
			return 0L;
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetUserAchievements()
		{
			return null;
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsExistsUserReview()
		{
			return false;
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x0000216A File Offset: 0x0000036A
		public static IReadOnlyDictionary<string, object> GetUserReview()
		{
			return null;
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x0000216A File Offset: 0x0000036A
		public static IReadOnlyDictionary<string, object> GetGemShopProducts()
		{
			return null;
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0000216A File Offset: 0x0000036A
		public static IReadOnlyDictionary<string, object> GetGemShopProduct(long shopPaidId)
		{
			return null;
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetGemShopConfirmRegDatas(int confirmRegId)
		{
			return null;
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x0000216A File Offset: 0x0000036A
		public static IReadOnlyDictionary<string, object> GetGemShopBuyResult()
		{
			return null;
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x0000216A File Offset: 0x0000036A
		public static IReadOnlyDictionary<string, object> GetGemShopInform()
		{
			return null;
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetShopProductsByCategory(ShopDef.ShowcaseCategory productCategory)
		{
			return null;
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x000F1A9C File Offset: 0x000EFC9C
		public static ValueTuple<ShopDef.ShowcaseCategory, Dictionary<string, object>> GetShopProductData(int shopId)
		{
			return default(ValueTuple<ShopDef.ShowcaseCategory, Dictionary<string, object>>);
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetProductByCategory(ShopDef.ShowcaseCategory productCategory, int shopId)
		{
			return null;
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetShopConfirmRegDatas(int confirmRegId)
		{
			return null;
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetShopTicketInfo()
		{
			return null;
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsShopPayItemSpecial(bool isPeriod, int itemCategory, int itemId)
		{
			return false;
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetShopPeriodItemHave(int pItemCategory, int pItemId)
		{
			return 0;
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetShopPeriodItemLimitDateStr(int pItemCategory, int pItemId)
		{
			return null;
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetAllProductPickup()
		{
			return null;
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetProductPickup(int shopId)
		{
			return null;
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetAllPackDatas()
		{
			return null;
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000F1AB4 File Offset: 0x000EFCB4
		public static ValueTuple<ShopDef.ShowcaseCategory, Dictionary<string, object>> GetProduct(int shopId)
		{
			return default(ValueTuple<ShopDef.ShowcaseCategory, Dictionary<string, object>>);
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetPackDatasInTypes(params CardPackDef.PackType[] packTypes)
		{
			return null;
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetProductPack(int shopId)
		{
			return null;
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsExistsSecretPack()
		{
			return false;
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetAllProductStructure()
		{
			return null;
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetProductStructure(int shopId)
		{
			return null;
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetAllProductSpecial()
		{
			return null;
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetProductSpecial(int shopId)
		{
			return null;
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetAllProductAccessory()
		{
			return null;
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetProductAccessory(int shopId)
		{
			return null;
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ContainsGachaDrawInfo()
		{
			return false;
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetGachaDrawInfo()
		{
			return null;
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetGachaDrawCardInfo()
		{
			return null;
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetGachaResult()
		{
			return null;
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsExistsGachaCardList(int cardListId)
		{
			return false;
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x0000216A File Offset: 0x0000036A
		public static IReadOnlyList<object> GetGachaCardList(int cardListId)
		{
			return null;
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x0000216A File Offset: 0x0000036A
		public static object GetGachaRateData()
		{
			return null;
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetPrizeData(int prizeId)
		{
			return null;
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetPrizeResultInfo()
		{
			return null;
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuStandard()
		{
			return null;
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetDuelMenuStandardSeason()
		{
			return 0;
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuStandardHolding()
		{
			return null;
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuTournament()
		{
			return null;
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuExhibition()
		{
			return null;
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuTournament(int tid)
		{
			return null;
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuExhibition(int exhid)
		{
			return null;
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuTournamentHolding(int tid)
		{
			return null;
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuExhibitionHolding(int tid)
		{
			return null;
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsHoldingTournament(int tid)
		{
			return false;
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsHoldingExhibition(int exid)
		{
			return false;
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsHoldingStandard()
		{
			return false;
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMatchingStandard()
		{
			return false;
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuFree()
		{
			return null;
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuFreeHolding()
		{
			return null;
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsHoldingFree()
		{
			return false;
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuTeamMatchHolding()
		{
			return null;
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsHoldingTeam()
		{
			return false;
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuTeamMatchPreTeamInfo()
		{
			return null;
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuRankEvent()
		{
			return null;
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuRankEvent(int eid)
		{
			return null;
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuRankEventHolding(int rank_event_id)
		{
			return null;
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsHoldingRankEvent(int rank_event_id)
		{
			return false;
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuFromPlayMode(ColosseumUtil.PlayMode mode, int eventId = 0)
		{
			return null;
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuHoldingFromPlayMode(ColosseumUtil.PlayMode mode, int eventId = 0)
		{
			return null;
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsHoldingFromPlayMode(ColosseumUtil.PlayMode mode, int eventId = 0)
		{
			return false;
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool IsHoldingCup(ColosseumUtil.PlayMode mode)
		{
			return false;
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetFirstDlvFromPlayMode(ColosseumUtil.PlayMode mode)
		{
			return null;
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetFinalFromPlayMode(ColosseumUtil.PlayMode mode)
		{
			return null;
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetRankingExistFromPlayMode(ColosseumUtil.PlayMode mode)
		{
			return false;
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuDuelTrial()
		{
			return null;
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuDuelTrial(int tid)
		{
			return null;
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuDuelTrialHolding(int tid)
		{
			return null;
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsHoldingDuelTrial(int tid)
		{
			return false;
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMasterTournament()
		{
			return null;
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMasterExhibition()
		{
			return null;
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMasterRankEvent()
		{
			return null;
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMasterFromPlayMode(ColosseumUtil.PlayMode mode, int eventId = 0)
		{
			return null;
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMasterDuelTrial()
		{
			return null;
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMasterDuelTrial(int id)
		{
			return null;
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMasterVersusl()
		{
			return null;
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMasterVersusl(int id)
		{
			return null;
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetTournamentNameTextID(int id)
		{
			return null;
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetRegulationId(int mode, int identifier = 0)
		{
			return 0;
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetRegulationId(Util.GameMode mode, int identifier = 0)
		{
			return 0;
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetTournamentRegulationID(int id)
		{
			return 0;
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCupRegulationID()
		{
			return 0;
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetWcsRegulationID()
		{
			return 0;
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetRankEventRegulationID(int id)
		{
			return 0;
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetDuelTrialRegulationID(int id)
		{
			return 0;
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetVersusGroupID(int id)
		{
			return 0;
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetVersusRegulationID(int id, int groupId)
		{
			return 0;
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetMasterTournamentGroupNum(int id)
		{
			return 0;
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetExhibtionNameTextID(int id)
		{
			return null;
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetExhibitionRegulationID(int id)
		{
			return 0;
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelMenuExhibitionRentalDeckInfo(int exhid)
		{
			return null;
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetDuelMenuExhibitionRentalState(int exhid, int defaultValue = 0)
		{
			return 0;
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetTournamentInfo()
		{
			return null;
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetTournamentInfo(int id)
		{
			return null;
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetTournamentResult()
		{
			return null;
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetMasterTournamentStatus(int id)
		{
			return 0;
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetTournamentRankingMembers(int id)
		{
			return null;
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetTournamentRankingMyself(int id)
		{
			return null;
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetTournamentRecvResult()
		{
			return null;
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetMasterTournamentRewardInfo(int id)
		{
			return null;
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMasterDuelTrialRewards(int duel_trial_id)
		{
			return null;
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetExhibitionRewardInfo(int id)
		{
			return null;
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetChallengeInfo(int mode = 1)
		{
			return null;
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetChallengeInfoRankInfoHistory(int mode = 1)
		{
			return null;
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetChallengeRankingMembers(int mode = 1)
		{
			return null;
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetChallengeRankingMyself(int mode = 1)
		{
			return null;
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetChallengeSelectDeck(int mode = 1)
		{
			return 0;
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetChallengeRewardList(int mode = 1)
		{
			return null;
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetTDeckList(int tid)
		{
			return null;
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetEXHDeckList(int exhid)
		{
			return null;
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRankEvent(int rank_event_id = 1)
		{
			return null;
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRankEventInfo(int rank_event_id = 1)
		{
			return null;
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetRankEventInfoRankInfoNowRank(int rank_event_id, int defaultValue = 1)
		{
			return 0;
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetRankEventInfoRankInfoNowTier(int rank_event_id, int defaultValue = 1)
		{
			return 0;
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetCupNameTextID()
		{
			return null;
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetWcsNameTextID()
		{
			return null;
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetRankEventNameTextID(int id)
		{
			return null;
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetDuelTrialNameTextID(int id)
		{
			return null;
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetVersusNameTextID(int id, int index)
		{
			return null;
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelTrial(int duel_trial_id = 1)
		{
			return null;
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetVersus(int versus_id = 1)
		{
			return null;
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetVersusGroupTotalPoint(int versus_id, int group_id, long defaultValue = 0L)
		{
			return 0L;
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetVersusGroupTotalPercent(int versus_id, int group_id, long defaultValue = 0L)
		{
			return 0L;
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRoom()
		{
			return null;
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRoomInfo()
		{
			return null;
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetRoomName(long pcode)
		{
			return null;
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetRoomInfoIsJoinPlayer(bool defaultValue = true)
		{
			return false;
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetRoomInfoTable()
		{
			return null;
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRoomInfoBattleSetting()
		{
			return null;
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRoomInfoRoomMembers()
		{
			return null;
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRoomInfoRoomMember(long pcode)
		{
			return null;
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRoomInfoLeaveRoomMember(long pcode)
		{
			return null;
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetRoomInfoNewComment(long pcode)
		{
			return 0;
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRoomInfoInvitedList()
		{
			return null;
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetRoomList()
		{
			return null;
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRoomRuleList()
		{
			return null;
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetRoomHoldingRuleList()
		{
			return null;
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetRoomInfoBattleResultList()
		{
			return null;
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetPlatformRoomInviteId()
		{
			return 0;
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetPlatformTeamInviteId()
		{
			return 0;
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMasterSoloGate()
		{
			return null;
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMasterSoloChapter(int gateID)
		{
			return null;
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMasterSoloUnlock(int unlockID)
		{
			return null;
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMasterSoloUnlockItem(int setID)
		{
			return null;
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMasterSoloReward(int setID)
		{
			return null;
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetSoloGateClearChapter(int gateID)
		{
			return 0;
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetSoloChapter(int chapterID)
		{
			return null;
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsExistsSoloChapterDetail(int chapterID)
		{
			return false;
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetSoloClearedGate(int gateID)
		{
			return false;
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetSoloLastPlayDate(int gateId)
		{
			return 0L;
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetSoloClearedChapter(int chapterID)
		{
			return false;
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x000F16D4 File Offset: 0x000EF8D4
		public static bool GetSoloClearedChapter(int chapterID, out int status)
		{
			status = 0;
			return false;
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetSoloClearedChapter(int chapterID, bool isRental)
		{
			return false;
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsClearedTutorialChapter()
		{
			return false;
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetSoloClearedChapterDic(int gateID)
		{
			return null;
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetSoloResultRewards()
		{
			return null;
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetSoloResultGateClear()
		{
			return null;
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetSoloResultIsPresentSend()
		{
			return false;
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetSoloDeckInfo()
		{
			return null;
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetSoloNotify()
		{
			return null;
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMissionAllMasters()
		{
			return null;
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ContainsCampaignPoolMaster(int poolId)
		{
			return false;
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMissionAllDatas()
		{
			return null;
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsNewMission(int poolId, int missionId)
		{
			return false;
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMissionData(int poolId, int missionId)
		{
			return null;
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMissionRecievedRewardData()
		{
			return null;
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMissionRecieveCompletes()
		{
			return null;
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetMissionRecieveHides()
		{
			return null;
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetPresentBoxHave()
		{
			return null;
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetPresentBoxRecieveResult()
		{
			return null;
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuel()
		{
			return null;
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDuelResult()
		{
			return null;
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x0000216A File Offset: 0x0000036A
		public static object GetDuelTeam(int myid)
		{
			return null;
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetTeamRegulationSetList()
		{
			return null;
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetTeamInfo()
		{
			return null;
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetTeamDeckId()
		{
			return 0;
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetTeamDeckInfo()
		{
			return null;
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetTeamInvitedList()
		{
			return null;
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetTeamNameCardID()
		{
			return 0;
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetNotificationNotification()
		{
			return null;
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetNotificationMaintenance()
		{
			return null;
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetNotificationBug()
		{
			return null;
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000F1ACC File Offset: 0x000EFCCC
		internal static ValueTuple<NotificationViewController.Type, Dictionary<string, object>> GetNotificationByID(int id)
		{
			return default(ValueTuple<NotificationViewController.Type, Dictionary<string, object>>);
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetTopics()
		{
			return null;
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetEventNotifyNotify()
		{
			return null;
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetEventNotifyBadge()
		{
			return null;
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetLoginBonus()
		{
			return null;
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetPromoCodesAllData()
		{
			return null;
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetPromoCodesData(int promoCodeId)
		{
			return null;
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetPromoCodesResult()
		{
			return null;
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x0000216A File Offset: 0x0000036A
		public static IReadOnlyDictionary<string, object> GetOperationDialog()
		{
			return null;
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetFriendPollingSpan()
		{
			return 0;
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x0000216A File Offset: 0x0000036A
		public static IReadOnlyDictionary<string, object> GetFriendRefreshDic()
		{
			return null;
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x0000216A File Offset: 0x0000036A
		public static IReadOnlyList<object> GetFriendSearchProfileTags()
		{
			return null;
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetFollowPlayers()
		{
			return null;
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x0000216A File Offset: 0x0000036A
		public static object GetFollowPlayer(long pcode)
		{
			return null;
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetFollowPin(long pcode)
		{
			return false;
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x000F1AE4 File Offset: 0x000EFCE4
		public static ValueTuple<List<object>, bool> GetFollowerPlayers()
		{
			return default(ValueTuple<List<object>, bool>);
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetBlockPlayers()
		{
			return null;
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x000F1AFC File Offset: 0x000EFCFC
		public static ValueTuple<List<object>, bool> GetFriendSearchResult()
		{
			return default(ValueTuple<List<object>, bool>);
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x0000216A File Offset: 0x0000036A
		public static object GetUpdatedFriend()
		{
			return null;
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetCardIllustType()
		{
			return null;
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetOtherCardIllustType()
		{
			return null;
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsWhileTutorial()
		{
			return false;
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x000F1B12 File Offset: 0x000EFD12
		public static bool IsWhileTutorial(out int step)
		{
			step = 0;
			return false;
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x0000216A File Offset: 0x0000036A
		public static IReadOnlyList<object> GetFirstStructureDeckIds()
		{
			return null;
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool InPeriod(long startTs, long endTs)
		{
			return false;
		}

		// Token: 0x04002952 RID: 10578
		private static long ServerUnixTimeDiff;

		// Token: 0x04002953 RID: 10579
		private static DateTime UnixEpoch;
	}
}
