using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DFA RID: 3578
	public class EffectTaskLinkReset : EffectTask
	{
		// Token: 0x060067DA RID: 26586 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060067DB RID: 26587 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskLinkReset(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x060067DC RID: 26588 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A2DA RID: 41690
		private bool finished;
	}
}
