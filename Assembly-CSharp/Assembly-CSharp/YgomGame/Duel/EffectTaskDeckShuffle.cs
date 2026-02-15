using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DEA RID: 3562
	public class EffectTaskDeckShuffle : EffectTask
	{
		// Token: 0x060067AE RID: 26542 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060067AF RID: 26543 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskDeckShuffle(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x060067B0 RID: 26544 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x060067B1 RID: 26545 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x060067B2 RID: 26546 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishedStep()
		{
		}

		// Token: 0x0400A28F RID: 41615
		private bool finished;

		// Token: 0x0400A290 RID: 41616
		private EffectTaskDeckShuffle.Step step;

		// Token: 0x0400A291 RID: 41617
		private DeckCardPlace deckPlace;

		// Token: 0x02000DEB RID: 3563
		private enum Step
		{
			// Token: 0x0400A293 RID: 41619
			WaitCardMove,
			// Token: 0x0400A294 RID: 41620
			WaitFinish,
			// Token: 0x0400A295 RID: 41621
			Finished
		}
	}
}
