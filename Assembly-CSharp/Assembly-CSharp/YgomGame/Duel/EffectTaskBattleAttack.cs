using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DAD RID: 3501
	public class EffectTaskBattleAttack : EffectTask
	{
		// Token: 0x060066CE RID: 26318 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MinimumEffect(RunEffectWorker worker, int param1, int param2, int param3)
		{
		}

		// Token: 0x060066CF RID: 26319 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060066D0 RID: 26320 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x060066D1 RID: 26321 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x060066D2 RID: 26322 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskBattleAttack(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x060066D3 RID: 26323 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x060066D4 RID: 26324 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0400A10F RID: 41231
		private bool finished;

		// Token: 0x0400A110 RID: 41232
		private EffectTaskBattleAttack.Step step;

		// Token: 0x0400A111 RID: 41233
		private int srcTeam;

		// Token: 0x0400A112 RID: 41234
		private int srcPosition;

		// Token: 0x0400A113 RID: 41235
		private int dstTeam;

		// Token: 0x0400A114 RID: 41236
		private int dstPosition;

		// Token: 0x0400A115 RID: 41237
		private bool tutorialFinished;

		// Token: 0x02000DAE RID: 3502
		private enum Step
		{
			// Token: 0x0400A117 RID: 41239
			WaitCardMove,
			// Token: 0x0400A118 RID: 41240
			WaitFinalBlow,
			// Token: 0x0400A119 RID: 41241
			Finish
		}
	}
}
