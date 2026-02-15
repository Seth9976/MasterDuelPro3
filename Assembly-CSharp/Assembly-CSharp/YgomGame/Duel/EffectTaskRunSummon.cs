using System;

namespace YgomGame.Duel
{
	// Token: 0x02000E1D RID: 3613
	public class EffectTaskRunSummon : EffectTask
	{
		// Token: 0x06006855 RID: 26709 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006856 RID: 26710 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskRunSummon(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x06006857 RID: 26711 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnDestroy()
		{
		}

		// Token: 0x06006858 RID: 26712 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06006859 RID: 26713 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x0600685A RID: 26714 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitSummonStep()
		{
		}

		// Token: 0x0400A363 RID: 41827
		private bool finished;

		// Token: 0x0400A364 RID: 41828
		private EffectTaskRunSummon.Step step;

		// Token: 0x0400A365 RID: 41829
		private Engine.CardStatus st;

		// Token: 0x0400A366 RID: 41830
		private CardRoot cardRoot;

		// Token: 0x0400A367 RID: 41831
		private bool camMoved;

		// Token: 0x02000E1E RID: 3614
		private enum Step
		{
			// Token: 0x0400A369 RID: 41833
			WaitCardMove,
			// Token: 0x0400A36A RID: 41834
			WaitSummon
		}
	}
}
