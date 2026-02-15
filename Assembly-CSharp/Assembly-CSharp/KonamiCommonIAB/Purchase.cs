using System;

namespace KonamiCommonIAB
{
	// Token: 0x020011A3 RID: 4515
	public abstract class Purchase
	{
		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x0600872C RID: 34604
		public abstract string productId { get; }

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x0600872D RID: 34605 RVA: 0x0000216A File Offset: 0x0000036A
		public string receipt
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600872E RID: 34606
		public abstract string getReceipt();

		// Token: 0x0600872F RID: 34607
		public abstract ProductInfo getProductInfo();
	}
}
