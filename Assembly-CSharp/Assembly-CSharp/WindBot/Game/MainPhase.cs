using System;
using System.Collections.Generic;

namespace WindBot.Game
{
	// Token: 0x02000201 RID: 513
	public class MainPhase
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x000309A9 File Offset: 0x0002EBA9
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x000309B1 File Offset: 0x0002EBB1
		public IList<ClientCard> SummonableCards { get; private set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x000309BA File Offset: 0x0002EBBA
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x000309C2 File Offset: 0x0002EBC2
		public IList<ClientCard> SpecialSummonableCards { get; private set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x000309CB File Offset: 0x0002EBCB
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x000309D3 File Offset: 0x0002EBD3
		public IList<ClientCard> ReposableCards { get; private set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x000309DC File Offset: 0x0002EBDC
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x000309E4 File Offset: 0x0002EBE4
		public IList<ClientCard> MonsterSetableCards { get; private set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x000309ED File Offset: 0x0002EBED
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x000309F5 File Offset: 0x0002EBF5
		public IList<ClientCard> SpellSetableCards { get; private set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x000309FE File Offset: 0x0002EBFE
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x00030A06 File Offset: 0x0002EC06
		public IList<ClientCard> ActivableCards { get; private set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00030A0F File Offset: 0x0002EC0F
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x00030A17 File Offset: 0x0002EC17
		public IList<int> ActivableDescs { get; private set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00030A20 File Offset: 0x0002EC20
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x00030A28 File Offset: 0x0002EC28
		public bool CanBattlePhase { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00030A31 File Offset: 0x0002EC31
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x00030A39 File Offset: 0x0002EC39
		public bool CanEndPhase { get; set; }

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00030A44 File Offset: 0x0002EC44
		public MainPhase()
		{
			this.SummonableCards = new List<ClientCard>();
			this.SpecialSummonableCards = new List<ClientCard>();
			this.ReposableCards = new List<ClientCard>();
			this.MonsterSetableCards = new List<ClientCard>();
			this.SpellSetableCards = new List<ClientCard>();
			this.ActivableCards = new List<ClientCard>();
			this.ActivableDescs = new List<int>();
		}
	}
}
