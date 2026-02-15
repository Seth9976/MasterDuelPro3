using System;

namespace YgomGame.Duel
{
	// Token: 0x02000E23 RID: 3619
	public class EffectTaskTuningReset : EffectTask
	{
		// Token: 0x06006867 RID: 26727 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006868 RID: 26728 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskTuningReset(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x06006869 RID: 26729 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A36D RID: 41837
		private bool finished;
	}
}
