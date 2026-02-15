using System;

namespace YgomGame.Duel
{
	// Token: 0x02000DE0 RID: 3552
	public class EffectTaskCutinCoinDice : EffectTask
	{
		// Token: 0x0600678F RID: 26511 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006790 RID: 26512 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskCutinCoinDice(RunEffectWorker worker, int player, int cardId, int result)
			: base(null)
		{
		}

		// Token: 0x06006791 RID: 26513 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x0400A26F RID: 41583
		private bool finished;
	}
}
