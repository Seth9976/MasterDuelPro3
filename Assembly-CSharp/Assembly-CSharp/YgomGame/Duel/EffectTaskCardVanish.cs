using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DD4 RID: 3540
	public class EffectTaskCardVanish : EffectTask
	{
		// Token: 0x06006769 RID: 26473 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600676A RID: 26474 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCardVanish(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x0600676B RID: 26475 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0600676C RID: 26476 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x0600676D RID: 26477 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0400A23D RID: 41533
		private bool finished;

		// Token: 0x0400A23E RID: 41534
		private EffectTaskCardVanish.Step step;

		// Token: 0x0400A23F RID: 41535
		private int team;

		// Token: 0x0400A240 RID: 41536
		private int position;

		// Token: 0x0400A241 RID: 41537
		private int index;

		// Token: 0x02000DD5 RID: 3541
		private enum Step
		{
			// Token: 0x0400A243 RID: 41539
			WaitCardMove,
			// Token: 0x0400A244 RID: 41540
			Finish
		}
	}
}
