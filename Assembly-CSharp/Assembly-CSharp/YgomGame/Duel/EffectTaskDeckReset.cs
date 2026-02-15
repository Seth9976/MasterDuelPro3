using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DE8 RID: 3560
	public class EffectTaskDeckReset : EffectTask
	{
		// Token: 0x060067A7 RID: 26535 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060067A8 RID: 26536 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x060067A9 RID: 26537 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskDeckReset(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x060067AA RID: 26538 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060067AB RID: 26539 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskDeckReset(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x060067AC RID: 26540 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x060067AD RID: 26541 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x0400A286 RID: 41606
		private bool isFinished;

		// Token: 0x0400A287 RID: 41607
		private EffectTaskDeckReset.Step step;

		// Token: 0x0400A288 RID: 41608
		private int team;

		// Token: 0x0400A289 RID: 41609
		private int position;

		// Token: 0x0400A28A RID: 41610
		private int deckNum;

		// Token: 0x0400A28B RID: 41611
		private Dictionary<string, object> immediateWork;

		// Token: 0x02000DE9 RID: 3561
		private enum Step
		{
			// Token: 0x0400A28D RID: 41613
			WaitCardMove,
			// Token: 0x0400A28E RID: 41614
			Wait
		}
	}
}
