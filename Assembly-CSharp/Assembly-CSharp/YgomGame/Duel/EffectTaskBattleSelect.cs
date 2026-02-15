using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DB5 RID: 3509
	public class EffectTaskBattleSelect : EffectTask
	{
		// Token: 0x060066F6 RID: 26358 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060066F7 RID: 26359 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskBattleSelect(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x060066F8 RID: 26360 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A15B RID: 41307
		private bool finished;
	}
}
