using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DD0 RID: 3536
	public class EffectTaskCardSet : EffectTask
	{
		// Token: 0x06006759 RID: 26457 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker workerHUD, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600675A RID: 26458 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCardSet(RunEffectWorker workerHUD, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x0600675B RID: 26459 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A22B RID: 41515
		private bool finished;
	}
}
