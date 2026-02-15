using System;
using System.Collections.Generic;

namespace com.adjust.sdk
{
	// Token: 0x02000468 RID: 1128
	public class AdjustPlayStoreSubscription
	{
		// Token: 0x0600256A RID: 9578 RVA: 0x00002739 File Offset: 0x00000939
		public AdjustPlayStoreSubscription(string price, string currency, string sku, string orderId, string signature, string purchaseToken)
		{
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x0000216D File Offset: 0x0000036D
		public void setPurchaseTime(string purchaseTime)
		{
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x0000216D File Offset: 0x0000036D
		public void addCallbackParameter(string key, string value)
		{
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x0000216D File Offset: 0x0000036D
		public void addPartnerParameter(string key, string value)
		{
		}

		// Token: 0x04002701 RID: 9985
		internal string price;

		// Token: 0x04002702 RID: 9986
		internal string currency;

		// Token: 0x04002703 RID: 9987
		internal string sku;

		// Token: 0x04002704 RID: 9988
		internal string orderId;

		// Token: 0x04002705 RID: 9989
		internal string signature;

		// Token: 0x04002706 RID: 9990
		internal string purchaseToken;

		// Token: 0x04002707 RID: 9991
		internal string billingStore;

		// Token: 0x04002708 RID: 9992
		internal string purchaseTime;

		// Token: 0x04002709 RID: 9993
		internal List<string> partnerList;

		// Token: 0x0400270A RID: 9994
		internal List<string> callbackList;
	}
}
