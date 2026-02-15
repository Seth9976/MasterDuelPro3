using System;

namespace KonamiCommonIAB
{
	// Token: 0x020011A4 RID: 4516
	public abstract class Result
	{
		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06008731 RID: 34609
		public abstract int code { get; }

		// Token: 0x06008732 RID: 34610
		public abstract int getResponse();

		// Token: 0x06008733 RID: 34611
		public abstract string getMessage();

		// Token: 0x06008734 RID: 34612
		public abstract bool isSuccess();

		// Token: 0x06008735 RID: 34613
		public abstract bool isFailure();

		// Token: 0x0400C18B RID: 49547
		public const int IAP_SUCCESS = 0;

		// Token: 0x0400C18C RID: 49548
		public const int IAP_FAILED = -1;

		// Token: 0x0400C18D RID: 49549
		public const int IAP_CANCEL = -2;

		// Token: 0x0400C18E RID: 49550
		public const int IAP_DEFERRED = -3;
	}
}
