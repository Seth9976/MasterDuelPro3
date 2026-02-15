using System;
using System.Collections.Generic;
using KonamiCommonIAB;

namespace YgomSystem.Billing
{
	// Token: 0x0200078F RID: 1935
	public class BillingSystem
	{
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06003C20 RID: 15392 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool restoreOnLogin
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06003C21 RID: 15393 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isVoid
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06003C22 RID: 15394 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool initialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003C23 RID: 15395 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize()
		{
		}

		// Token: 0x06003C24 RID: 15396 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool canMakePayment()
		{
			return false;
		}

		// Token: 0x06003C25 RID: 15397 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadItemList(IList<string> productIds, Action<List<ProductInfo>> callback)
		{
		}

		// Token: 0x06003C26 RID: 15398 RVA: 0x0000216A File Offset: 0x0000036A
		public static ProductInfo GetItem(string productId)
		{
			return null;
		}

		// Token: 0x06003C27 RID: 15399 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetDisplayedPrice(string productId)
		{
			return null;
		}

		// Token: 0x06003C28 RID: 15400 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetDoubleNotationDisplayPrice(string productId)
		{
			return null;
		}

		// Token: 0x06003C29 RID: 15401 RVA: 0x0000216D File Offset: 0x0000036D
		public static void BuyItem(int shopId, string productId, Action<ResultCode> callback = null)
		{
		}

		// Token: 0x06003C2A RID: 15402 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DoRestore(Action<ResultCode> callback)
		{
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DoRestore(Action<ResultCode, List<Purchase>> callback)
		{
		}

		// Token: 0x040034E2 RID: 13538
		private static IBilling m_billing;
	}
}
