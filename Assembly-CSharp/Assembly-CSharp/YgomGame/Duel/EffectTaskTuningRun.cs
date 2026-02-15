using System;

namespace YgomGame.Duel
{
	// Token: 0x02000E24 RID: 3620
	public class EffectTaskTuningRun : EffectTask
	{
		// Token: 0x0600686A RID: 26730 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600686B RID: 26731 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskTuningRun(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x0600686C RID: 26732 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A36E RID: 41838
		private bool finished;
	}
}
