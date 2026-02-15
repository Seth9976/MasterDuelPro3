using System;
using System.Collections.Generic;

namespace com.adjust.sdk
{
	// Token: 0x02000463 RID: 1123
	public class AdjustEvent
	{
		// Token: 0x0600253D RID: 9533 RVA: 0x00002739 File Offset: 0x00000939
		public AdjustEvent(string eventToken)
		{
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x0000216D File Offset: 0x0000036D
		public void setRevenue(double amount, string currency)
		{
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x0000216D File Offset: 0x0000036D
		public void addCallbackParameter(string key, string value)
		{
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x0000216D File Offset: 0x0000036D
		public void addPartnerParameter(string key, string value)
		{
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x0000216D File Offset: 0x0000036D
		public void setTransactionId(string transactionId)
		{
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x0000216D File Offset: 0x0000036D
		public void setCallbackId(string callbackId)
		{
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x0000216D File Offset: 0x0000036D
		[Obsolete]
		public void setReceipt(string receipt, string transactionId)
		{
		}

		// Token: 0x040026F0 RID: 9968
		internal string currency;

		// Token: 0x040026F1 RID: 9969
		internal string eventToken;

		// Token: 0x040026F2 RID: 9970
		internal string callbackId;

		// Token: 0x040026F3 RID: 9971
		internal string transactionId;

		// Token: 0x040026F4 RID: 9972
		internal double? revenue;

		// Token: 0x040026F5 RID: 9973
		internal List<string> partnerList;

		// Token: 0x040026F6 RID: 9974
		internal List<string> callbackList;

		// Token: 0x040026F7 RID: 9975
		internal string receipt;

		// Token: 0x040026F8 RID: 9976
		internal bool isReceiptSet;
	}
}
