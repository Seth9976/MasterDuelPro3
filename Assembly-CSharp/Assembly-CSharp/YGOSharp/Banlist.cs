using System;
using System.Collections.Generic;

namespace YGOSharp
{
	// Token: 0x020001AF RID: 431
	public class Banlist
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001FA45 File Offset: 0x0001DC45
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x0001FA4D File Offset: 0x0001DC4D
		public IList<int> BannedIds { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0001FA56 File Offset: 0x0001DC56
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x0001FA5E File Offset: 0x0001DC5E
		public IList<int> LimitedIds { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0001FA67 File Offset: 0x0001DC67
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x0001FA6F File Offset: 0x0001DC6F
		public IList<int> SemiLimitedIds { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0001FA78 File Offset: 0x0001DC78
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x0001FA80 File Offset: 0x0001DC80
		public uint Hash { get; private set; }

		// Token: 0x0600066D RID: 1645 RVA: 0x0001FA89 File Offset: 0x0001DC89
		public Banlist()
		{
			this.BannedIds = new List<int>();
			this.LimitedIds = new List<int>();
			this.SemiLimitedIds = new List<int>();
			this.Hash = 2113728106U;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001FABD File Offset: 0x0001DCBD
		public int GetQuantity(int cardId)
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

		// Token: 0x0600066F RID: 1647 RVA: 0x0001FAF0 File Offset: 0x0001DCF0
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
	}
}
