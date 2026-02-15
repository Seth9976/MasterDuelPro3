using System;

namespace YgomGame.Duel
{
	// Token: 0x02000E16 RID: 3606
	public class EffectTaskRunSpSummon : EffectTask
	{
		// Token: 0x06006842 RID: 26690 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006843 RID: 26691 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskRunSpSummon(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x06006844 RID: 26692 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnDestroy()
		{
		}

		// Token: 0x06006845 RID: 26693 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06006846 RID: 26694 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x06006847 RID: 26695 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCamMoveStep()
		{
		}

		// Token: 0x06006848 RID: 26696 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitSummonStep()
		{
		}

		// Token: 0x0400A345 RID: 41797
		private bool finished;

		// Token: 0x0400A346 RID: 41798
		private EffectTaskRunSpSummon.Step step;

		// Token: 0x0400A347 RID: 41799
		private Engine.CardStatus st;

		// Token: 0x0400A348 RID: 41800
		private CardRoot cardRoot;

		// Token: 0x0400A349 RID: 41801
		private bool camMoved;

		// Token: 0x02000E17 RID: 3607
		private enum Step
		{
			// Token: 0x0400A34B RID: 41803
			WaitCardMove,
			// Token: 0x0400A34C RID: 41804
			WaitCamMove,
			// Token: 0x0400A34D RID: 41805
			WaitSummon
		}
	}
}
