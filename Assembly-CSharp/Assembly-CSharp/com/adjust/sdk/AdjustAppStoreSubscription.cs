using System;
using System.Collections.Generic;

namespace com.adjust.sdk
{
	// Token: 0x0200045E RID: 1118
	public class AdjustAppStoreSubscription
	{
		// Token: 0x060024FC RID: 9468 RVA: 0x00002739 File Offset: 0x00000939
		public AdjustAppStoreSubscription(string price, string currency, string transactionId, string receipt)
		{
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x0000216D File Offset: 0x0000036D
		public void setTransactionDate(string transactionDate)
		{
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x0000216D File Offset: 0x0000036D
		public void setSalesRegion(string salesRegion)
		{
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x0000216D File Offset: 0x0000036D
		public void addCallbackParameter(string key, string value)
		{
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x0000216D File Offset: 0x0000036D
		public void addPartnerParameter(string key, string value)
		{
		}

		// Token: 0x040026AC RID: 9900
		internal string price;

		// Token: 0x040026AD RID: 9901
		internal string currency;

		// Token: 0x040026AE RID: 9902
		internal string transactionId;

		// Token: 0x040026AF RID: 9903
		internal string receipt;

		// Token: 0x040026B0 RID: 9904
		internal string billingStore;

		// Token: 0x040026B1 RID: 9905
		internal string transactionDate;

		// Token: 0x040026B2 RID: 9906
		internal string salesRegion;

		// Token: 0x040026B3 RID: 9907
		internal List<string> partnerList;

		// Token: 0x040026B4 RID: 9908
		internal List<string> callbackList;
	}
}
