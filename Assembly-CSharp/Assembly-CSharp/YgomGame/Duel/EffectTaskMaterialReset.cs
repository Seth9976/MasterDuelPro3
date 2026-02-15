using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DFF RID: 3583
	public class EffectTaskMaterialReset : EffectTask
	{
		// Token: 0x060067E9 RID: 26601 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060067EA RID: 26602 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskMaterialReset(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x060067EB RID: 26603 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A2E7 RID: 41703
		private bool finished;
	}
}
