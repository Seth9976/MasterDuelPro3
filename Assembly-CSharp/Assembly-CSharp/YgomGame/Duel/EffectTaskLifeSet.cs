using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DF8 RID: 3576
	public class EffectTaskLifeSet : EffectTask
	{
		// Token: 0x060067D6 RID: 26582 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MinimumEffect(RunEffectWorker worker, int param1, int param2, int param3)
		{
		}

		// Token: 0x060067D7 RID: 26583 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060067D8 RID: 26584 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskLifeSet(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x060067D9 RID: 26585 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A2D3 RID: 41683
		private EffectTaskLifeSet.Step step;

		// Token: 0x0400A2D4 RID: 41684
		private int team;

		// Token: 0x0400A2D5 RID: 41685
		private bool isLethal;

		// Token: 0x02000DF9 RID: 3577
		private enum Step
		{
			// Token: 0x0400A2D7 RID: 41687
			WaitCardEffectToBreakAllCards,
			// Token: 0x0400A2D8 RID: 41688
			WaitBreakAllCards,
			// Token: 0x0400A2D9 RID: 41689
			Finish
		}
	}
}
