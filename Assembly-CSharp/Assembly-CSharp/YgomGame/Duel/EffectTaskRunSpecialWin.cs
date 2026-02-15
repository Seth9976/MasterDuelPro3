using System;

namespace YgomGame.Duel
{
	// Token: 0x02000E18 RID: 3608
	public class EffectTaskRunSpecialWin : EffectTask
	{
		// Token: 0x06006849 RID: 26697 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600684A RID: 26698 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskRunSpecialWin(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x0600684B RID: 26699 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A34E RID: 41806
		private EffectTaskRunSpecialWin.Step step;

		// Token: 0x02000E19 RID: 3609
		private enum Step
		{
			// Token: 0x0400A350 RID: 41808
			Wait,
			// Token: 0x0400A351 RID: 41809
			Finish
		}
	}
}
