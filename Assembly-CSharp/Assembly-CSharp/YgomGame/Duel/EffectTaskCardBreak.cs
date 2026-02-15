using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000DB6 RID: 3510
	public class EffectTaskCardBreak : EffectTask
	{
		// Token: 0x060066F9 RID: 26361 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MinimumEffect(RunEffectWorker worker, int param1, int param2, int param3)
		{
		}

		// Token: 0x060066FA RID: 26362 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060066FB RID: 26363 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x060066FC RID: 26364 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCardBreak(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x060066FD RID: 26365 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x060066FE RID: 26366 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayBreakEffect()
		{
		}

		// Token: 0x060066FF RID: 26367 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0400A15C RID: 41308
		private bool finished;

		// Token: 0x0400A15D RID: 41309
		private EffectTaskCardBreak.Step step;

		// Token: 0x0400A15E RID: 41310
		private int team;

		// Token: 0x0400A15F RID: 41311
		private int position;

		// Token: 0x0400A160 RID: 41312
		private int index;

		// Token: 0x02000DB7 RID: 3511
		private enum Step
		{
			// Token: 0x0400A162 RID: 41314
			WaitCardRunEffect,
			// Token: 0x0400A163 RID: 41315
			Finish
		}
	}
}
