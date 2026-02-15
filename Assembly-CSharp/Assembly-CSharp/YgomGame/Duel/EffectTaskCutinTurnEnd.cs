using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DE5 RID: 3557
	public class EffectTaskCutinTurnEnd : EffectTask
	{
		// Token: 0x0600679C RID: 26524 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MinimumEffect(RunEffectWorker worker, int param1, int param2, int param3)
		{
		}

		// Token: 0x0600679D RID: 26525 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600679E RID: 26526 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCutinTurnEnd(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x0600679F RID: 26527 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A277 RID: 41591
		private bool finished;
	}
}
