using System;
using System.Collections.Generic;
using KonamiCommonIAB;

namespace YgomSystem.Billing
{
	// Token: 0x02000794 RID: 1940
	public interface IBilling
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06003C6A RID: 15466
		bool initialized { get; }

		// Token: 0x06003C6B RID: 15467
		bool canMakePayment();

		// Token: 0x06003C6C RID: 15468
		void Initialize();

		// Token: 0x06003C6D RID: 15469
		void LoadItemList(IList<string> productIds, Action<List<ProductInfo>> callback);

		// Token: 0x06003C6E RID: 15470
		ProductInfo GetItem(string productId);

		// Token: 0x06003C6F RID: 15471
		void DoRestore(Action<ResultCode> callback = null);

		// Token: 0x06003C70 RID: 15472
		void DoRestore(Action<ResultCode, List<Purchase>> callback);

		// Token: 0x06003C71 RID: 15473
		void BuyItem(int shopId, string productId, Action<ResultCode> callback = null);
	}
}
