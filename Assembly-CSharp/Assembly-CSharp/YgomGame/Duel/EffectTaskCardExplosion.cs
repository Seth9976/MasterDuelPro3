using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DBF RID: 3519
	public class EffectTaskCardExplosion : EffectTask
	{
		// Token: 0x0600671D RID: 26397 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600671E RID: 26398 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCardExplosion(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x0600671F RID: 26399 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06006720 RID: 26400 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x06006721 RID: 26401 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0400A191 RID: 41361
		private bool finished;

		// Token: 0x0400A192 RID: 41362
		private EffectTaskCardExplosion.Step step;

		// Token: 0x02000DC0 RID: 3520
		private enum Step
		{
			// Token: 0x0400A194 RID: 41364
			WaitCardMove,
			// Token: 0x0400A195 RID: 41365
			Finish
		}
	}
}
