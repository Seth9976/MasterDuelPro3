using System;
using System.Runtime.CompilerServices;
using KonamiCommonIAB;

namespace YgomSystem.SocialSystem
{
	// Token: 0x020006D1 RID: 1745
	public class SteamPurchaseForRestore : Purchase
	{
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600366D RID: 13933 RVA: 0x0000216A File Offset: 0x0000036A
		public override string productId
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600366E RID: 13934 RVA: 0x000F1669 File Offset: 0x000EF869
		public long OrderID
		{
			[CompilerGenerated]
			get
			{
				return 0L;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x0600366F RID: 13935 RVA: 0x000F1669 File Offset: 0x000EF869
		public long TransationID
		{
			[CompilerGenerated]
			get
			{
				return 0L;
			}
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000F35B2 File Offset: 0x000F17B2
		public SteamPurchaseForRestore(long order_id, long transaction_id, string product_id)
		{
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x0000216A File Offset: 0x0000036A
		public override ProductInfo getProductInfo()
		{
			return null;
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x0000216A File Offset: 0x0000036A
		public override string getReceipt()
		{
			return null;
		}
	}
}
