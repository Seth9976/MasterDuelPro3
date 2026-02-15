using System;
using System.Collections.Generic;

namespace WindBot.Game
{
	// Token: 0x020001ED RID: 493
	public class BattlePhase
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00028814 File Offset: 0x00026A14
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x0002881C File Offset: 0x00026A1C
		public IList<ClientCard> AttackableCards { get; private set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x00028825 File Offset: 0x00026A25
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x0002882D File Offset: 0x00026A2D
		public IList<ClientCard> ActivableCards { get; private set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00028836 File Offset: 0x00026A36
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x0002883E File Offset: 0x00026A3E
		public IList<int> ActivableDescs { get; private set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00028847 File Offset: 0x00026A47
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x0002884F File Offset: 0x00026A4F
		public bool CanMainPhaseTwo { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00028858 File Offset: 0x00026A58
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x00028860 File Offset: 0x00026A60
		public bool CanEndPhase { get; set; }

		// Token: 0x060008C7 RID: 2247 RVA: 0x00028869 File Offset: 0x00026A69
		public BattlePhase()
		{
			this.AttackableCards = new List<ClientCard>();
			this.ActivableCards = new List<ClientCard>();
			this.ActivableDescs = new List<int>();
		}
	}
}
