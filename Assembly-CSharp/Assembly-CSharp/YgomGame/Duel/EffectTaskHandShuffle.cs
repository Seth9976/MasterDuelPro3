using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DF4 RID: 3572
	public class EffectTaskHandShuffle : EffectTask
	{
		// Token: 0x060067CB RID: 26571 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060067CC RID: 26572 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskHandShuffle(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x060067CD RID: 26573 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x060067CE RID: 26574 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x0400A2C3 RID: 41667
		private bool finished;

		// Token: 0x0400A2C4 RID: 41668
		private EffectTaskHandShuffle.Step step;

		// Token: 0x0400A2C5 RID: 41669
		private int team;

		// Token: 0x02000DF5 RID: 3573
		private enum Step
		{
			// Token: 0x0400A2C7 RID: 41671
			WaitCardMove,
			// Token: 0x0400A2C8 RID: 41672
			WaitShuffle
		}
	}
}
