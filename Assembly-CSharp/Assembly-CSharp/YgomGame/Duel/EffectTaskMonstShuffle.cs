using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000E02 RID: 3586
	public class EffectTaskMonstShuffle : EffectTask
	{
		// Token: 0x060067F2 RID: 26610 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060067F3 RID: 26611 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskMonstShuffle(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x060067F4 RID: 26612 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x060067F5 RID: 26613 RVA: 0x000F5DAA File Offset: 0x000F3FAA
		private void FlagToEachPlaces(int team, int flag, out List<BasicCardPlace> basicPlaces)
		{
			basicPlaces = null;
		}

		// Token: 0x060067F6 RID: 26614 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFinishedSwap()
		{
		}

		// Token: 0x060067F7 RID: 26615 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x0400A2E9 RID: 41705
		private bool finished;

		// Token: 0x0400A2EA RID: 41706
		private EffectTaskMonstShuffle.Step step;

		// Token: 0x0400A2EB RID: 41707
		private int team;

		// Token: 0x0400A2EC RID: 41708
		private int flag;

		// Token: 0x0400A2ED RID: 41709
		private int callCounter;

		// Token: 0x02000E03 RID: 3587
		private enum Step
		{
			// Token: 0x0400A2EF RID: 41711
			WaitCardMove,
			// Token: 0x0400A2F0 RID: 41712
			Wait
		}
	}
}
