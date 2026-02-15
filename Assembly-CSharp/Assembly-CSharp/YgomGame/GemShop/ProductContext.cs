using System;
using System.Collections.Generic;

namespace YgomGame.GemShop
{
	// Token: 0x02000C02 RID: 3074
	public class ProductContext : IComparable<ProductContext>
	{
		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06005745 RID: 22341 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isLimitedDate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06005746 RID: 22342 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isInTermDate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005747 RID: 22343 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetItemNum(int itemId, int num)
		{
		}

		// Token: 0x06005748 RID: 22344 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetItemNum(int itemId)
		{
			return 0;
		}

		// Token: 0x06005749 RID: 22345 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Compare(ProductContext a, ProductContext b)
		{
			return 0;
		}

		// Token: 0x0600574A RID: 22346 RVA: 0x000029CC File Offset: 0x00000BCC
		public int CompareTo(ProductContext other)
		{
			return 0;
		}

		// Token: 0x04009400 RID: 37888
		private Dictionary<int, int> m_ItemNumMap;

		// Token: 0x04009401 RID: 37889
		public ProductStyle style;

		// Token: 0x04009402 RID: 37890
		public string productName;

		// Token: 0x04009403 RID: 37891
		public long endDateTs;

		// Token: 0x04009404 RID: 37892
		public string limitdate;

		// Token: 0x04009405 RID: 37893
		public long shopPaidId;

		// Token: 0x04009406 RID: 37894
		public int thumbId;

		// Token: 0x04009407 RID: 37895
		public string priceLabel;

		// Token: 0x04009408 RID: 37896
		public string doubleNotationPriceLabel;

		// Token: 0x04009409 RID: 37897
		public string limitBuyLabel;

		// Token: 0x0400940A RID: 37898
		public int order;

		// Token: 0x0400940B RID: 37899
		public bool soldout;
	}
}
