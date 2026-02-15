using System;

namespace YgomGame.Duel
{
	// Token: 0x02000E28 RID: 3624
	public class EffectTaskWaitFrame : EffectTask
	{
		// Token: 0x06006878 RID: 26744 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006879 RID: 26745 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskWaitFrame(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x0600687A RID: 26746 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A37A RID: 41850
		private bool finished;

		// Token: 0x0400A37B RID: 41851
		private float waitTime;

		// Token: 0x0400A37C RID: 41852
		private float totalTime;
	}
}
