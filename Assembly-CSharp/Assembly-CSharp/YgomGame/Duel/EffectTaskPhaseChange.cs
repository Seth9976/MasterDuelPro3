using System;

namespace YgomGame.Duel
{
	// Token: 0x02000E07 RID: 3591
	public class EffectTaskPhaseChange : EffectTask
	{
		// Token: 0x06006801 RID: 26625 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006802 RID: 26626 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskPhaseChange(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x06006803 RID: 26627 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayPhaseChangeEffect()
		{
		}

		// Token: 0x06006804 RID: 26628 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A2F3 RID: 41715
		private int player;

		// Token: 0x0400A2F4 RID: 41716
		private Engine.Phase phase;

		// Token: 0x0400A2F5 RID: 41717
		private EffectTaskPhaseChange.Step step;

		// Token: 0x02000E08 RID: 3592
		private enum Step
		{
			// Token: 0x0400A2F7 RID: 41719
			Tutorial,
			// Token: 0x0400A2F8 RID: 41720
			WaitEffect,
			// Token: 0x0400A2F9 RID: 41721
			Finish
		}
	}
}
