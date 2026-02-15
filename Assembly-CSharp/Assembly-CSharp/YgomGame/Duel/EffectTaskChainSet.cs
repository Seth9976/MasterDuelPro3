using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DDA RID: 3546
	public class EffectTaskChainSet : EffectTask
	{
		// Token: 0x0600677D RID: 26493 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MinimumEffect(RunEffectWorker worker, int param1, int param2, int param3)
		{
		}

		// Token: 0x0600677E RID: 26494 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600677F RID: 26495 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskChainSet(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x06006780 RID: 26496 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06006781 RID: 26497 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardEffectStep()
		{
		}

		// Token: 0x06006782 RID: 26498 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitChainEffectStep()
		{
		}

		// Token: 0x06006783 RID: 26499 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0400A252 RID: 41554
		private EffectTaskChainSet.Step step;

		// Token: 0x0400A253 RID: 41555
		private bool finished;

		// Token: 0x0400A254 RID: 41556
		private int player;

		// Token: 0x0400A255 RID: 41557
		private int position;

		// Token: 0x0400A256 RID: 41558
		private int num;

		// Token: 0x0400A257 RID: 41559
		private int uniqueID;

		// Token: 0x02000DDB RID: 3547
		private enum Step
		{
			// Token: 0x0400A259 RID: 41561
			WaitCardEffect,
			// Token: 0x0400A25A RID: 41562
			WaitChainEffect,
			// Token: 0x0400A25B RID: 41563
			Finish
		}
	}
}
