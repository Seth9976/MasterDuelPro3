using System;

namespace KonamiCommonIAB
{
	// Token: 0x020011A7 RID: 4519
	internal class WindowsShiftProductInfo : ProductInfo
	{
		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x0600873C RID: 34620 RVA: 0x0000216A File Offset: 0x0000036A
		public override string productId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x0600873D RID: 34621 RVA: 0x0000216A File Offset: 0x0000036A
		public override string title
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x0600873E RID: 34622 RVA: 0x0000216A File Offset: 0x0000036A
		public override string description
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x0600873F RID: 34623 RVA: 0x0000216A File Offset: 0x0000036A
		public override string displayedPrice
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x06008740 RID: 34624 RVA: 0x0000216A File Offset: 0x0000036A
		public override string currency
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06008741 RID: 34625 RVA: 0x000029C5 File Offset: 0x00000BC5
		public override float price
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x06008742 RID: 34626 RVA: 0x000F462E File Offset: 0x000F282E
		public WindowsShiftProductInfo(string prodId, string title, long price)
		{
		}

		// Token: 0x0400C196 RID: 49558
		private string _productId;

		// Token: 0x0400C197 RID: 49559
		private string _title;

		// Token: 0x0400C198 RID: 49560
		private long _price;
	}
}
