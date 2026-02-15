using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DE3 RID: 3555
	public class EffectTaskCutinSummon : EffectTask
	{
		// Token: 0x06006798 RID: 26520 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006799 RID: 26521 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCutinSummon(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x0600679A RID: 26522 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0600679B RID: 26523 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0400A271 RID: 41585
		private Engine.CutinSummonType summonType;

		// Token: 0x0400A272 RID: 41586
		private bool waitEffect;

		// Token: 0x02000DE4 RID: 3556
		private enum Step
		{
			// Token: 0x0400A274 RID: 41588
			Start,
			// Token: 0x0400A275 RID: 41589
			Wait,
			// Token: 0x0400A276 RID: 41590
			Finish
		}
	}
}
