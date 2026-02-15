using System;

namespace WindBot.Game
{
	// Token: 0x02000202 RID: 514
	public class MainPhaseAction
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00030AA4 File Offset: 0x0002ECA4
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x00030AAC File Offset: 0x0002ECAC
		public MainPhaseAction.MainAction Action { get; private set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x00030AB5 File Offset: 0x0002ECB5
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x00030ABD File Offset: 0x0002ECBD
		public int Index { get; private set; }

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00030AC6 File Offset: 0x0002ECC6
		public MainPhaseAction(MainPhaseAction.MainAction action)
		{
			this.Action = action;
			this.Index = 0;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00030ADC File Offset: 0x0002ECDC
		public MainPhaseAction(MainPhaseAction.MainAction action, int index)
		{
			this.Action = action;
			this.Index = index;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00030AF2 File Offset: 0x0002ECF2
		public MainPhaseAction(MainPhaseAction.MainAction action, int[] indexes)
		{
			this.Action = action;
			this.Index = indexes[(int)action];
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00030B0A File Offset: 0x0002ED0A
		public int ToValue()
		{
			return (int)((this.Index << 16) + this.Action);
		}

		// Token: 0x02000203 RID: 515
		public enum MainAction
		{
			// Token: 0x04000DEB RID: 3563
			Summon,
			// Token: 0x04000DEC RID: 3564
			SpSummon,
			// Token: 0x04000DED RID: 3565
			Repos,
			// Token: 0x04000DEE RID: 3566
			SetMonster,
			// Token: 0x04000DEF RID: 3567
			SetSpell,
			// Token: 0x04000DF0 RID: 3568
			Activate,
			// Token: 0x04000DF1 RID: 3569
			ToBattlePhase,
			// Token: 0x04000DF2 RID: 3570
			ToEndPhase
		}
	}
}
