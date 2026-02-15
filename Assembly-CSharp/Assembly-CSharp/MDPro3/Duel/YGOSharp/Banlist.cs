using System;
using System.Collections.Generic;

namespace MDPro3.Duel.YGOSharp
{
	// Token: 0x0200151C RID: 5404
	public class Banlist
	{
		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x06009D22 RID: 40226 RVA: 0x001956D3 File Offset: 0x001938D3
		// (set) Token: 0x06009D23 RID: 40227 RVA: 0x001956DB File Offset: 0x001938DB
		public IList<int> BannedIds { get; private set; }

		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x06009D24 RID: 40228 RVA: 0x001956E4 File Offset: 0x001938E4
		// (set) Token: 0x06009D25 RID: 40229 RVA: 0x001956EC File Offset: 0x001938EC
		public IList<int> LimitedIds { get; private set; }

		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x06009D26 RID: 40230 RVA: 0x001956F5 File Offset: 0x001938F5
		// (set) Token: 0x06009D27 RID: 40231 RVA: 0x001956FD File Offset: 0x001938FD
		public IList<int> SemiLimitedIds { get; private set; }

		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x06009D28 RID: 40232 RVA: 0x00195706 File Offset: 0x00193906
		// (set) Token: 0x06009D29 RID: 40233 RVA: 0x0019570E File Offset: 0x0019390E
		public uint Hash { get; private set; }

		// Token: 0x06009D2A RID: 40234 RVA: 0x00195717 File Offset: 0x00193917
		public Banlist()
		{
			this.BannedIds = new List<int>();
			this.LimitedIds = new List<int>();
			this.SemiLimitedIds = new List<int>();
			this.Hash = 2113728106U;
		}

		// Token: 0x06009D2B RID: 40235 RVA: 0x00195758 File Offset: 0x00193958
		public int GetQuantity(int cardId)
		{
			int al = 0;
			try
			{
				al = CardsManager.Get(cardId, false).Alias;
			}
			catch (Exception)
			{
			}
			if (al == 0)
			{
				if (this.BannedIds.Contains(cardId))
				{
					return 0;
				}
				if (this.LimitedIds.Contains(cardId))
				{
					return 1;
				}
				if (this.SemiLimitedIds.Contains(cardId))
				{
					return 2;
				}
				return 3;
			}
			else
			{
				if (this.BannedIds.Contains(al))
				{
					return 0;
				}
				if (this.LimitedIds.Contains(al))
				{
					return 1;
				}
				if (this.SemiLimitedIds.Contains(al))
				{
					return 2;
				}
				return 3;
			}
		}

		// Token: 0x06009D2C RID: 40236 RVA: 0x001957F0 File Offset: 0x001939F0
		public void Add(int cardId, int quantity)
		{
			if (quantity < 0 || quantity > 2)
			{
				return;
			}
			switch (quantity)
			{
			case 0:
				this.BannedIds.Add(cardId);
				break;
			case 1:
				this.LimitedIds.Add(cardId);
				break;
			case 2:
				this.SemiLimitedIds.Add(cardId);
				break;
			}
			this.Hash = this.Hash ^ (uint)((cardId << 18) | (int)((uint)cardId >> 14)) ^ (uint)((cardId << 27 + quantity) | (int)((uint)cardId >> 5 - quantity));
		}

		// Token: 0x0400DB40 RID: 56128
		public string Name = "";
	}
}
