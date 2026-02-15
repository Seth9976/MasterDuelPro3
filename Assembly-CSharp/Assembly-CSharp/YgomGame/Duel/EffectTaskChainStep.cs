using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DDC RID: 3548
	public class EffectTaskChainStep : EffectTask
	{
		// Token: 0x06006784 RID: 26500 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006785 RID: 26501 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskChainStep(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x06006786 RID: 26502 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06006787 RID: 26503 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCardUniqueEffect()
		{
		}

		// Token: 0x06006788 RID: 26504 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0400A25C RID: 41564
		private EffectTaskChainStep.Step step;

		// Token: 0x0400A25D RID: 41565
		private int mrk;

		// Token: 0x0400A25E RID: 41566
		private bool finished;

		// Token: 0x0400A25F RID: 41567
		private int player;

		// Token: 0x0400A260 RID: 41568
		private int position;

		// Token: 0x02000DDD RID: 3549
		private enum Step
		{
			// Token: 0x0400A262 RID: 41570
			Init,
			// Token: 0x0400A263 RID: 41571
			WaitEffect,
			// Token: 0x0400A264 RID: 41572
			Finish
		}
	}
}
