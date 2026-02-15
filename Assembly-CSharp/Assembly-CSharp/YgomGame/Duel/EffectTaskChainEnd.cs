using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DD6 RID: 3542
	public class EffectTaskChainEnd : EffectTask
	{
		// Token: 0x0600676E RID: 26478 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MinimumEffect(RunEffectWorker worker, int param1, int param2, int param3)
		{
		}

		// Token: 0x0600676F RID: 26479 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006770 RID: 26480 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskChainEnd(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x06006771 RID: 26481 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06006772 RID: 26482 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardEffectStep()
		{
		}

		// Token: 0x06006773 RID: 26483 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFinishChainEffect()
		{
		}

		// Token: 0x06006774 RID: 26484 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0400A245 RID: 41541
		private bool finished;

		// Token: 0x0400A246 RID: 41542
		private EffectTaskChainEnd.Step step;

		// Token: 0x02000DD7 RID: 3543
		private enum Step
		{
			// Token: 0x0400A248 RID: 41544
			WaitCardEffect,
			// Token: 0x0400A249 RID: 41545
			WaitTutorial,
			// Token: 0x0400A24A RID: 41546
			Finish
		}
	}
}
