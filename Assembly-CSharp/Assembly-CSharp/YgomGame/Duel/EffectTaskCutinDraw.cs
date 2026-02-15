using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DE1 RID: 3553
	public class EffectTaskCutinDraw : EffectTask
	{
		// Token: 0x06006792 RID: 26514 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006793 RID: 26515 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCutinDraw(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x06006794 RID: 26516 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A270 RID: 41584
		private bool finished;
	}
}
