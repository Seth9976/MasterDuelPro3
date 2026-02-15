using System;
using System.Collections.Generic;
using KonamiCommonIAB;

namespace YgomSystem.Billing
{
	// Token: 0x02000792 RID: 1938
	public class Billing_None : IBilling
	{
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06003C53 RID: 15443 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool initialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003C54 RID: 15444 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool canMakePayment()
		{
			return false;
		}

		// Token: 0x06003C55 RID: 15445 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadItemList(IList<string> productIds, Action<List<ProductInfo>> callback)
		{
		}

		// Token: 0x06003C57 RID: 15447 RVA: 0x0000216A File Offset: 0x0000036A
		public ProductInfo GetItem(string productId)
		{
			return null;
		}

		// Token: 0x06003C58 RID: 15448 RVA: 0x0000216D File Offset: 0x0000036D
		public void DoRestore(Action<ResultCode> callback = null)
		{
		}

		// Token: 0x06003C59 RID: 15449 RVA: 0x0000216D File Offset: 0x0000036D
		public void BuyItem(int shopId, string productId, Action<ResultCode> callback = null)
		{
		}

		// Token: 0x06003C5A RID: 15450 RVA: 0x0000216D File Offset: 0x0000036D
		public void DoRestore(Action<ResultCode, List<Purchase>> callback)
		{
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetDoubleNotationDisplayPrice(string productId)
		{
			return null;
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual string GetDoubleNotationDisplayPrice(ProductInfo productInfo)
		{
			return null;
		}

		// Token: 0x040034E6 RID: 13542
		protected Dictionary<string, ProductInfo> m_productDic;
	}
}
