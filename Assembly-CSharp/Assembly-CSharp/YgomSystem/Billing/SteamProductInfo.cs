using System;
using KonamiCommonIAB;

namespace YgomSystem.Billing
{
	// Token: 0x02000796 RID: 1942
	public class SteamProductInfo : ProductInfo
	{
		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06003C72 RID: 15474 RVA: 0x0000216A File Offset: 0x0000036A
		public override string productId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06003C73 RID: 15475 RVA: 0x0000216A File Offset: 0x0000036A
		public override string title
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06003C74 RID: 15476 RVA: 0x0000216A File Offset: 0x0000036A
		public override string description
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06003C75 RID: 15477 RVA: 0x0000216A File Offset: 0x0000036A
		public override string displayedPrice
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06003C76 RID: 15478 RVA: 0x0000216A File Offset: 0x0000036A
		public override string currency
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06003C77 RID: 15479 RVA: 0x000029C5 File Offset: 0x00000BC5
		public override float price
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x000F462E File Offset: 0x000F282E
		public SteamProductInfo(string _productId, float _price = 0f, string _displayedPrice = "", string _currency = "")
		{
		}

		// Token: 0x040034FC RID: 13564
		private string m_productId;

		// Token: 0x040034FD RID: 13565
		private float m_price;

		// Token: 0x040034FE RID: 13566
		private string m_currency;

		// Token: 0x040034FF RID: 13567
		private string m_displayedPrice;
	}
}
