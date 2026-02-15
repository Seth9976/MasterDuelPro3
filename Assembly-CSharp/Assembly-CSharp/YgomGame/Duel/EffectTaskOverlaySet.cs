using System;

namespace YgomGame.Duel
{
	// Token: 0x02000E06 RID: 3590
	public class EffectTaskOverlaySet : EffectTask
	{
		// Token: 0x060067FE RID: 26622 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060067FF RID: 26623 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskOverlaySet(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x06006800 RID: 26624 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A2F2 RID: 41714
		private bool finished;
	}
}
