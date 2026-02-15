using System;
using System.Collections.Generic;
using KonamiCommonIAB;
using YgomSystem.Network;

namespace YgomSystem.Billing
{
	// Token: 0x02000793 RID: 1939
	public class Billing_Steam : Billing_Base
	{
		// Token: 0x06003C5E RID: 15454 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool canMakePayment()
		{
			return false;
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Initialize()
		{
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Handle API_Billing_re_store(Purchase purchase)
		{
			return null;
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool BuyItemFromPlatform(ProductInfo product, IabDelegate.OnBuyFinishedDelegate cb)
		{
			return false;
		}

		// Token: 0x06003C62 RID: 15458 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void checkUnfinishedPurchase(ProductInfo product, Action<ResultCode, Purchase> callback)
		{
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void checkUnfinishedPurchase(Action<ResultCode, List<Purchase>> callback)
		{
		}

		// Token: 0x06003C64 RID: 15460 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void GetItemList(string[] productIds, Action<List<ProductInfo>> callback)
		{
		}

		// Token: 0x06003C65 RID: 15461 RVA: 0x0000216A File Offset: 0x0000036A
		protected List<Purchase> GetUnfinishedPurchaseItemList()
		{
			return null;
		}

		// Token: 0x06003C66 RID: 15462 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnPurchaseFinished(Purchase purchase)
		{
		}

		// Token: 0x06003C67 RID: 15463 RVA: 0x0000216D File Offset: 0x0000036D
		private void RegistCallback()
		{
		}

		// Token: 0x06003C68 RID: 15464 RVA: 0x0000216D File Offset: 0x0000036D
		private void UnregistCallback()
		{
		}

		// Token: 0x040034E7 RID: 13543
		protected ProductInfo m_ProductInfo;
	}
}
