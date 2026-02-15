using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DED RID: 3565
	public class EffectTaskDuelStart : EffectTask
	{
		// Token: 0x060067B6 RID: 26550 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060067B7 RID: 26551 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x060067B8 RID: 26552 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskDuelStart(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x060067B9 RID: 26553 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x060067BA RID: 26554 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayEachPlayerStep()
		{
		}

		// Token: 0x060067BB RID: 26555 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x060067BC RID: 26556 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCharaStep()
		{
		}

		// Token: 0x060067BD RID: 26557 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishCharaStep()
		{
		}

		// Token: 0x0400A299 RID: 41625
		private bool finished;

		// Token: 0x0400A29A RID: 41626
		private EffectTaskDuelStart.Step step;

		// Token: 0x0400A29B RID: 41627
		private bool charaFinished;

		// Token: 0x0400A29C RID: 41628
		private EffectTaskDuelStart.CharaStep charaStep;

		// Token: 0x02000DEE RID: 3566
		private enum Step
		{
			// Token: 0x0400A29E RID: 41630
			PlayEachPlayer,
			// Token: 0x0400A29F RID: 41631
			WaitEachPlayer,
			// Token: 0x0400A2A0 RID: 41632
			Finish,
			// Token: 0x0400A2A1 RID: 41633
			Tutorial
		}

		// Token: 0x02000DEF RID: 3567
		private enum CharaStep
		{
			// Token: 0x0400A2A3 RID: 41635
			Entry,
			// Token: 0x0400A2A4 RID: 41636
			Wait,
			// Token: 0x0400A2A5 RID: 41637
			Finish
		}
	}
}
