using System;

namespace YgomGame.Duel
{
	// Token: 0x02000E04 RID: 3588
	public class EffectTaskOverlayReset : EffectTask
	{
		// Token: 0x060067F8 RID: 26616 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x060067F9 RID: 26617 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskOverlayReset(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x060067FA RID: 26618 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A2F1 RID: 41713
		private bool finished;
	}
}
