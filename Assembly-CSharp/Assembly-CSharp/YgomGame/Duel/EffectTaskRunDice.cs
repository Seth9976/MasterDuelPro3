using System;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000E0D RID: 3597
	public class EffectTaskRunDice : EffectTask
	{
		// Token: 0x06006813 RID: 26643 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006814 RID: 26644 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskRunDice(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x06006815 RID: 26645 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06006816 RID: 26646 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardEffectStep()
		{
		}

		// Token: 0x06006817 RID: 26647 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitDiceStep()
		{
		}

		// Token: 0x06006818 RID: 26648 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnDestroy()
		{
		}

		// Token: 0x0400A310 RID: 41744
		private int team;

		// Token: 0x0400A311 RID: 41745
		private int numThrows;

		// Token: 0x0400A312 RID: 41746
		private int number;

		// Token: 0x0400A313 RID: 41747
		private ScreenSelector selector;

		// Token: 0x0400A314 RID: 41748
		private bool isSkip;

		// Token: 0x0400A315 RID: 41749
		private bool isTimelineLoaded;

		// Token: 0x0400A316 RID: 41750
		private EffectTaskRunDice.Step step;

		// Token: 0x02000E0E RID: 3598
		private enum Step
		{
			// Token: 0x0400A318 RID: 41752
			WaitCardEffect,
			// Token: 0x0400A319 RID: 41753
			WaitDice,
			// Token: 0x0400A31A RID: 41754
			Finish
		}
	}
}
