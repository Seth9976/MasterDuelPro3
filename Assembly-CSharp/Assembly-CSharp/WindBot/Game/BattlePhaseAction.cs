using System;

namespace WindBot.Game
{
	// Token: 0x020001EE RID: 494
	public class BattlePhaseAction
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00028892 File Offset: 0x00026A92
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x0002889A File Offset: 0x00026A9A
		public BattlePhaseAction.BattleAction Action { get; private set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x000288A3 File Offset: 0x00026AA3
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x000288AB File Offset: 0x00026AAB
		public int Index { get; private set; }

		// Token: 0x060008CC RID: 2252 RVA: 0x000288B4 File Offset: 0x00026AB4
		public BattlePhaseAction(BattlePhaseAction.BattleAction action)
		{
			this.Action = action;
			this.Index = 0;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x000288CA File Offset: 0x00026ACA
		public BattlePhaseAction(BattlePhaseAction.BattleAction action, int[] indexes)
		{
			this.Action = action;
			this.Index = indexes[(int)action];
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x000288E2 File Offset: 0x00026AE2
		public int ToValue()
		{
			return (int)((this.Index << 16) + this.Action);
		}

		// Token: 0x020001EF RID: 495
		public enum BattleAction
		{
			// Token: 0x04000D42 RID: 3394
			Activate,
			// Token: 0x04000D43 RID: 3395
			Attack,
			// Token: 0x04000D44 RID: 3396
			ToMainPhaseTwo,
			// Token: 0x04000D45 RID: 3397
			ToEndPhase
		}
	}
}
