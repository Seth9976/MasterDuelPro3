using System;
using System.Collections.Generic;
using YgomGame.Utility;

namespace YgomGame.Shop
{
	// Token: 0x02000937 RID: 2359
	public class PriceContext : IComparable<PriceContext>
	{
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060044B9 RID: 17593 RVA: 0x000029CC File Offset: 0x00000BCC
		public int sort
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060044BA RID: 17594 RVA: 0x000029CC File Offset: 0x00000BCC
		public int priceId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060044BB RID: 17595 RVA: 0x000029CC File Offset: 0x00000BCC
		public int confirmRegId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060044BC RID: 17596 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool payItemIsPeriod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060044BD RID: 17597 RVA: 0x000029CC File Offset: 0x00000BCC
		public int payItemCategory
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060044BE RID: 17598 RVA: 0x000029CC File Offset: 0x00000BCC
		public int payItemId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060044BF RID: 17599 RVA: 0x000029CC File Offset: 0x00000BCC
		public int payItemHave
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060044C0 RID: 17600 RVA: 0x000F1669 File Offset: 0x000EF869
		public long limitdateTs
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060044C1 RID: 17601 RVA: 0x0000216A File Offset: 0x0000036A
		public string limitdate
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060044C2 RID: 17602 RVA: 0x000029CC File Offset: 0x00000BCC
		public int priceAmount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060044C3 RID: 17603 RVA: 0x000029CC File Offset: 0x00000BCC
		public int freeNum
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060044C4 RID: 17604 RVA: 0x0000216A File Offset: 0x0000036A
		public List<PriceContext> allPrices
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060044C5 RID: 17605 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool hasSubPrices
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060044C6 RID: 17606 RVA: 0x000029CC File Offset: 0x00000BCC
		public int buyCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060044C7 RID: 17607 RVA: 0x000029CC File Offset: 0x00000BCC
		public int buttonType
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060044C8 RID: 17608 RVA: 0x0000216A File Offset: 0x0000036A
		public string priceLabelText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060044C9 RID: 17609 RVA: 0x0000216A File Offset: 0x0000036A
		public string priceButtonIconPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060044CA RID: 17610 RVA: 0x0000216A File Offset: 0x0000036A
		public string confirmDialogTitleId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060044CB RID: 17611 RVA: 0x0000216A File Offset: 0x0000036A
		public string confirmDialogMessageId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060044CC RID: 17612 RVA: 0x0000216A File Offset: 0x0000036A
		public string routeScheme
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPriceFreeText()
		{
			return null;
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x0000216A File Offset: 0x0000036A
		public string MakePopText(TextGroupLoadHolder textGroupLoadHolder)
		{
			return null;
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x0000216D File Offset: 0x0000036D
		public void Import(Dictionary<string, object> priceData)
		{
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Compare(PriceContext a, PriceContext b)
		{
			return 0;
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x000029CC File Offset: 0x00000BCC
		public int CompareTo(PriceContext other)
		{
			return 0;
		}

		// Token: 0x0400832B RID: 33579
		private Dictionary<string, object> m_PriceData;

		// Token: 0x0400832C RID: 33580
		private List<PriceContext> m_AllPrices;
	}
}
