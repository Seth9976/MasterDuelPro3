using System;
using System.Collections.Generic;

namespace KonamiCommonIAB
{
	// Token: 0x0200119C RID: 4508
	public class IabDelegate
	{
		// Token: 0x0200119D RID: 4509
		// (Invoke) Token: 0x06008711 RID: 34577
		public delegate void OnInitializationFinishedDelegate(bool result);

		// Token: 0x0200119E RID: 4510
		// (Invoke) Token: 0x06008715 RID: 34581
		public delegate void OnGetItemDetailsFinished(Result result, List<ProductInfo> details);

		// Token: 0x0200119F RID: 4511
		// (Invoke) Token: 0x06008719 RID: 34585
		public delegate void OnGetItemProductDetailsFinished(Result result, List<ProductInfo> details);

		// Token: 0x020011A0 RID: 4512
		// (Invoke) Token: 0x0600871D RID: 34589
		public delegate void OnBuyFinishedDelegate(Result result, Purchase purchase);

		// Token: 0x020011A1 RID: 4513
		// (Invoke) Token: 0x06008721 RID: 34593
		public delegate void OnAcknowledgeFinishedDelegate(Result result);
	}
}
