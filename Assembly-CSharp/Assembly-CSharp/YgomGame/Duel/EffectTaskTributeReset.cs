using System;

namespace YgomGame.Duel
{
	// Token: 0x02000E20 RID: 3616
	public class EffectTaskTributeReset : EffectTask
	{
		// Token: 0x0600685E RID: 26718 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600685F RID: 26719 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskTributeReset(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x06006860 RID: 26720 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A36B RID: 41835
		private bool finished;
	}
}
