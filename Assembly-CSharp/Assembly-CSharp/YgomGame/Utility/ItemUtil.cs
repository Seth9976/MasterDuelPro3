using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Dialog.CommonDialog;
using YgomGame.Menu.Common;

namespace YgomGame.Utility
{
	// Token: 0x02000827 RID: 2087
	public class ItemUtil
	{
		// Token: 0x06004058 RID: 16472 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsValidItem(bool isPerild, int itemCategory, int itemId)
		{
			return false;
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsGemItem(bool isPerild, int itemCategory, int itemId)
		{
			return false;
		}

		// Token: 0x0600405A RID: 16474 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsPaidGemItem(bool isPerild, int itemCategory, int itemId)
		{
			return false;
		}

		// Token: 0x0600405B RID: 16475 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsTicketItem(bool isPerild, int itemCategory, int itemId)
		{
			return false;
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsCardItem(bool isPerild, int itemCategory, int itemId)
		{
			return false;
		}

		// Token: 0x0600405D RID: 16477 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsConsumeItem(bool isPerild, int itemCategory, int itemId)
		{
			return false;
		}

		// Token: 0x0600405E RID: 16478 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsPeriodConsumeItem(ItemUtil.PeriodCategory periodCategory)
		{
			return false;
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsConsumeItem(ItemUtil.Category itemCategory)
		{
			return false;
		}

		// Token: 0x06004060 RID: 16480 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsSkippableCategoryNameItem(bool isPeriod, int itemCategory, int itemId)
		{
			return false;
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsSkippableDescItem(bool isPeriod, int itemCategory, int itemId)
		{
			return false;
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsSkippableNum(bool isPeriod, int itemCategory, int itemId)
		{
			return false;
		}

		// Token: 0x06004063 RID: 16483 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingItemThumb BindItemThumb(GameObject target, int itemID, float scale = 0f, BindingItemThumb.DxBadgeMode dxBadgeMode = BindingItemThumb.DxBadgeMode.None)
		{
			return null;
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingItemThumb BindItemThumb(GameObject target, bool isPerild, int itemCategory, int itemID, float scale = 0f, BindingItemThumb.DxBadgeMode dxBadgeMode = BindingItemThumb.DxBadgeMode.None)
		{
			return null;
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingItemThumb BindItemThumbLarge(GameObject target, int itemID, float scale = 0f)
		{
			return null;
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingItemThumb BindItemThumbLarge(GameObject target, bool isPerild, int itemCategory, int itemID, float scale = 0f)
		{
			return null;
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetItemName(int itemID, TextGroupLoadHolder textGroupLoadHolder = null)
		{
			return null;
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetItemName(bool isPerild, int itemCategory, int itemID, TextGroupLoadHolder textGroupLoadHolder = null)
		{
			return null;
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetItemNameWithFreePaidCheck(int itemID, TextGroupLoadHolder textGroupLoadHolder = null)
		{
			return null;
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetItemDesc(int itemID, bool useMobileSfx = false)
		{
			return null;
		}

		// Token: 0x0600406B RID: 16491 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetItemDesc(bool isPeriod, int itemCategory, int itemID, bool useMobileSfx = false, TextGroupLoadHolder textGroupLoadHolder = null)
		{
			return null;
		}

		// Token: 0x0600406C RID: 16492 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetItemCategoryName(bool isPeriod, int category, int itemId, TextGroupLoadHolder textGroupLoadHolder = null)
		{
			return null;
		}

		// Token: 0x0600406D RID: 16493 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetCategoryName(ItemUtil.Category category, TextGroupLoadHolder textGroupLoadHolder = null, bool isDx = false)
		{
			return null;
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetPeriodCategoryName(ItemUtil.PeriodCategory periodCategory, TextGroupLoadHolder textGroupLoadHolder = null)
		{
			return null;
		}

		// Token: 0x0600406F RID: 16495 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetTagLabel(int tagId, TextGroupLoadHolder textGroupLoadHolder = null)
		{
			return null;
		}

		// Token: 0x06004070 RID: 16496 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetItemDic(ItemUtil.Category category)
		{
			return null;
		}

		// Token: 0x06004071 RID: 16497 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetHasTotalGem()
		{
			return 0;
		}

		// Token: 0x06004072 RID: 16498 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetHasFreeGem()
		{
			return 0;
		}

		// Token: 0x06004073 RID: 16499 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetHasPaidGem()
		{
			return 0;
		}

		// Token: 0x06004074 RID: 16500 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetHasItemQuantity(int itemID)
		{
			return 0;
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetHasAllItemDic()
		{
			return null;
		}

		// Token: 0x06004076 RID: 16502 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> GetHasItemList(ItemUtil.Category category)
		{
			return null;
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x000F470A File Offset: 0x000F290A
		public static bool GetMrkStyleIdFromID(int itemId, out int mrk, out int styleId)
		{
			mrk = 0;
			styleId = 0;
			return false;
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x000029CC File Offset: 0x00000BCC
		public static ItemUtil.Category GetCategoryFromID(int itemID)
		{
			return ItemUtil.Category.NONE;
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCategoryOffset(ItemUtil.Category category)
		{
			return 0;
		}

		// Token: 0x0600407A RID: 16506 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int SelectValidCategory(bool isPeriod, int categoryID, int itemID)
		{
			return 0;
		}

		// Token: 0x0600407B RID: 16507 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDeluxe(bool isPeriod, int categoryID, int itemID)
		{
			return false;
		}

		// Token: 0x0600407C RID: 16508 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDeluxe(int itemID)
		{
			return false;
		}

		// Token: 0x0600407D RID: 16509 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool IsMatchingCategoryRange(ItemUtil.Category category, object value)
		{
			return false;
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x000029CC File Offset: 0x00000BCC
		[Obsolete]
		public static bool IsTargetItemConfirmDialog(bool isPeriod, int itemCategory)
		{
			return false;
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenItemDetail(bool isPeriod, int itemCategory, int itemId, int itemNum = -1, Action callback = null, Dictionary<string, object> itemArgs = null)
		{
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x000029CC File Offset: 0x00000BCC
		public static ItemUtil.ItemDetailType GetItemDetailType(bool isPeriod, int itemCategory, int itemId)
		{
			return ItemUtil.ItemDetailType.CARD_BROWSER;
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenItemConfirmDialog(string title, bool isPeriod, int itemCategory, int itemId, int itemNum, Action callback = null, bool hideNum = false, Dictionary<string, object> itemArgs = null)
		{
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenItemRecieveDialog(bool isPeriod, int itemCategory, int itemId, int itemNum, bool isSendPresent, Action callback = null)
		{
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenItemRecieveDialog(string title, bool isPeriod, int itemCategory, int itemId, int itemNum, bool isSendPresent, Action callback = null)
		{
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenItemRecieveDialog(EntryItemListData receiveItemListData, bool isSendPresent, Action action = null, string title = null)
		{
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenItemRecieveDialog(string title, EntryItemListData receiveItemListData, bool isSendPresent, Action action = null)
		{
		}

		// Token: 0x06004086 RID: 16518 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenItemRecieveHighlightDialogs(string title, IReadOnlyList<ValueTuple<EntryItemListData.Context, bool>> receivedItems, Action action = null, int playIdx = 0)
		{
		}

		// Token: 0x06004087 RID: 16519 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenItemRecieveListOrHighlightDialog(EntryItemListData recievedItems, bool isSendPresent, string title = null, Action action = null)
		{
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenItemRecieveHighlightDialog(string title, bool isPeriod, int itemCategory, int itemId, int itemNum, bool isSendPresent, Action callback = null, bool isEffect = true, bool hideNum = false, bool isRecieve = true, Dictionary<string, object> itemArgs = null)
		{
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenItemReceiveHighlightDialogBase(string title, bool isPeriod, int itemCategory, int itemId, int itemNum, bool isSendPresent, EntryButtonData[] entryButtons, bool isEffect, bool hideNum, bool isRecieve, Dictionary<string, object> itemArgs = null)
		{
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x000F4714 File Offset: 0x000F2914
		public static ValueTuple<int, int> GetCardIDandDecoID(int itemId)
		{
			return default(ValueTuple<int, int>);
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingGameObjectEx BindDeluxBadge(GameObject target, bool isSimple = false, bool isLarge = false)
		{
			return null;
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x0000216A File Offset: 0x0000036A
		private static Dictionary<string, int> GetItemOrder(List<object> list)
		{
			return null;
		}

		// Token: 0x0600408D RID: 16525 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int ItemSorter(object a, object b, Dictionary<string, int> itemOrder)
		{
			return 0;
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> SortItem(List<object> list)
		{
			return null;
		}

		// Token: 0x04003966 RID: 14694
		public const int k_ItemIdFreeGem = 1;

		// Token: 0x04003967 RID: 14695
		public const int k_ItemIdPaidGem = 2;

		// Token: 0x04003968 RID: 14696
		public const int k_ItemIdCardRareShine = 100000;

		// Token: 0x04003969 RID: 14697
		public const int k_ItemIdCardRareRoyal = 200000;

		// Token: 0x02000828 RID: 2088
		public enum Category
		{
			// Token: 0x0400396B RID: 14699
			NONE,
			// Token: 0x0400396C RID: 14700
			CONSUME,
			// Token: 0x0400396D RID: 14701
			CARD,
			// Token: 0x0400396E RID: 14702
			AVATAR,
			// Token: 0x0400396F RID: 14703
			ICON,
			// Token: 0x04003970 RID: 14704
			PROFILE_TAG,
			// Token: 0x04003971 RID: 14705
			ICON_FRAME,
			// Token: 0x04003972 RID: 14706
			PROTECTOR,
			// Token: 0x04003973 RID: 14707
			DECK_CASE,
			// Token: 0x04003974 RID: 14708
			FIELD,
			// Token: 0x04003975 RID: 14709
			FIELD_OBJ,
			// Token: 0x04003976 RID: 14710
			AVATAR_HOME,
			// Token: 0x04003977 RID: 14711
			STRUCTURE,
			// Token: 0x04003978 RID: 14712
			WALLPAPER,
			// Token: 0x04003979 RID: 14713
			PACK_TICKET,
			// Token: 0x0400397A RID: 14714
			DECK_LIMIT
		}

		// Token: 0x02000829 RID: 2089
		public enum ItemType
		{
			// Token: 0x0400397C RID: 14716
			NONE,
			// Token: 0x0400397D RID: 14717
			DELUXE
		}

		// Token: 0x0200082A RID: 2090
		public enum PeriodCategory
		{
			// Token: 0x0400397F RID: 14719
			NONE,
			// Token: 0x04003980 RID: 14720
			EVENT,
			// Token: 0x04003981 RID: 14721
			PACK_TICKET,
			// Token: 0x04003982 RID: 14722
			DUEL_PASS,
			// Token: 0x04003983 RID: 14723
			CAMPAIGN_PACK_TICKET,
			// Token: 0x04003984 RID: 14724
			UNPACK_RIGHT
		}

		// Token: 0x0200082B RID: 2091
		public enum ItemDetailType
		{
			// Token: 0x04003986 RID: 14726
			CARD_BROWSER,
			// Token: 0x04003987 RID: 14727
			DECK_BROWSER,
			// Token: 0x04003988 RID: 14728
			ITEM_PREVIEW
		}
	}
}
