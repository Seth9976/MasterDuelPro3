using System;
using YgomSystem.Network;

namespace YgomGame.Shop
{
	// Token: 0x0200096B RID: 2411
	public static class ShopUtil
	{
		// Token: 0x0600465B RID: 18011 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsPayItemSpecial(bool isPeriod, int itemCategory, int itemId)
		{
			return false;
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetPayItemHave(bool isPeriod, int itemCategory, int itemId)
		{
			return 0;
		}

		// Token: 0x0600465D RID: 18013 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetPayItemLimitDateTs(bool isPeriod, int itemCategory, int itemId)
		{
			return 0L;
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetPayItemLimitDateStr(bool isPeriod, int itemCategory, int itemId)
		{
			return null;
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CheckHandlingShopSectionMainte(Handle h, bool skipHandling = false)
		{
			return false;
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckFocusBGM()
		{
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AbortShop()
		{
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenPoolCardListBrowser(int normalCardPoolId)
		{
		}

		// Token: 0x06004663 RID: 18019 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenPickupCardListBrowser(int pickupCardPoolId)
		{
		}

		// Token: 0x06004664 RID: 18020 RVA: 0x0000216D File Offset: 0x0000036D
		private static void OpenCardListBrowser(int cardPoolId, string configPath)
		{
		}

		// Token: 0x040084C7 RID: 33991
		private const string k_CardListPoolConfig = "Definition/CardListBrowser/ShopCardPoolConfig";

		// Token: 0x040084C8 RID: 33992
		private const string k_CardListPickupConfig = "Definition/CardListBrowser/ShopCardPickupConfig";
	}
}
