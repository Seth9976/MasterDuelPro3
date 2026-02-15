using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DD8 RID: 3544
	public class EffectTaskChainRun : EffectTask
	{
		// Token: 0x06006775 RID: 26485 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MinimumEffect(RunEffectWorker worker, int param1, int param2, int param3)
		{
		}

		// Token: 0x06006776 RID: 26486 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006777 RID: 26487 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskChainRun(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x06006778 RID: 26488 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06006779 RID: 26489 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardEffectStep()
		{
		}

		// Token: 0x0600677A RID: 26490 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitChainEffectStep()
		{
		}

		// Token: 0x0600677B RID: 26491 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFinishChainEffect()
		{
		}

		// Token: 0x0600677C RID: 26492 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0400A24B RID: 41547
		private EffectTaskChainRun.Step step;

		// Token: 0x0400A24C RID: 41548
		private bool finished;

		// Token: 0x0400A24D RID: 41549
		private int num;

		// Token: 0x02000DD9 RID: 3545
		private enum Step
		{
			// Token: 0x0400A24F RID: 41551
			WaitCardEffect,
			// Token: 0x0400A250 RID: 41552
			WaitChainEffect,
			// Token: 0x0400A251 RID: 41553
			Finish
		}
	}
}
