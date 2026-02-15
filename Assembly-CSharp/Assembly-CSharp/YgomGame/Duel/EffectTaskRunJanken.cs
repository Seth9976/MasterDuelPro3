using System;

namespace YgomGame.Duel
{
	// Token: 0x02000E11 RID: 3601
	public class EffectTaskRunJanken : EffectTask
	{
		// Token: 0x06006837 RID: 26679 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006838 RID: 26680 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskRunJanken(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x06006839 RID: 26681 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0600683A RID: 26682 RVA: 0x000029CC File Offset: 0x00000BCC
		private EffectTaskRunJanken.Result Janken(EffectTaskRunJanken.HandType handMyself, EffectTaskRunJanken.HandType handRival)
		{
			return EffectTaskRunJanken.Result.Win;
		}

		// Token: 0x0400A332 RID: 41778
		private bool finished;

		// Token: 0x02000E12 RID: 3602
		private enum HandType
		{
			// Token: 0x0400A334 RID: 41780
			Rock,
			// Token: 0x0400A335 RID: 41781
			Scissors,
			// Token: 0x0400A336 RID: 41782
			Paper,
			// Token: 0x0400A337 RID: 41783
			Num
		}

		// Token: 0x02000E13 RID: 3603
		private enum Result
		{
			// Token: 0x0400A339 RID: 41785
			Win,
			// Token: 0x0400A33A RID: 41786
			Lose,
			// Token: 0x0400A33B RID: 41787
			Draw
		}
	}
}
