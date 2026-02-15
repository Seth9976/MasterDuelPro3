using System;
using System.Collections.Generic;

namespace YgomGame.Shop
{
	// Token: 0x02000924 RID: 2340
	public class HighlightContext
	{
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06004437 RID: 17463 RVA: 0x000029CC File Offset: 0x00000BCC
		public ShopDef.HighlightType highlightType
		{
			get
			{
				return (ShopDef.HighlightType)0;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06004438 RID: 17464 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool supportedCard
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06004439 RID: 17465 RVA: 0x000029CC File Offset: 0x00000BCC
		public int mrk
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600443A RID: 17466 RVA: 0x000029CC File Offset: 0x00000BCC
		public int rare
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600443B RID: 17467 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600443C RID: 17468 RVA: 0x0000216D File Offset: 0x0000036D
		public int itemId
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600443D RID: 17469 RVA: 0x0000216A File Offset: 0x0000036A
		public string path
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x0600443E RID: 17470 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isPref
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600443F RID: 17471 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isMate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06004440 RID: 17472 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isSupportedPlay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06004441 RID: 17473 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isSupportedMonsterCutin
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004442 RID: 17474 RVA: 0x0000216D File Offset: 0x0000036D
		public void Import(Dictionary<string, object> priceData)
		{
		}

		// Token: 0x040082FA RID: 33530
		private int m_ItemId;

		// Token: 0x040082FB RID: 33531
		private Dictionary<string, object> m_HighlightData;
	}
}
