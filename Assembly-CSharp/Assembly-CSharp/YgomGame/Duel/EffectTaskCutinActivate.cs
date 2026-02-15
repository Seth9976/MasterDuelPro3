using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DDE RID: 3550
	public class EffectTaskCutinActivate : EffectTask
	{
		// Token: 0x06006789 RID: 26505 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MinimumEffect(RunEffectWorker worker, int param1, int param2, int param3)
		{
		}

		// Token: 0x0600678A RID: 26506 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600678B RID: 26507 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x0600678C RID: 26508 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCutinActivate(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x0600678D RID: 26509 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0600678E RID: 26510 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0400A265 RID: 41573
		private EffectTaskCutinActivate.Step step;

		// Token: 0x0400A266 RID: 41574
		private bool finished;

		// Token: 0x0400A267 RID: 41575
		private int player;

		// Token: 0x0400A268 RID: 41576
		private int mixedId;

		// Token: 0x0400A269 RID: 41577
		private int owner;

		// Token: 0x0400A26A RID: 41578
		private int state;

		// Token: 0x0400A26B RID: 41579
		private int cardId;

		// Token: 0x0400A26C RID: 41580
		private int type;

		// Token: 0x02000DDF RID: 3551
		private enum Step
		{
			// Token: 0x0400A26E RID: 41582
			Finish
		}
	}
}
