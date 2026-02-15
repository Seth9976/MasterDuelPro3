using System;

namespace YGOSharp.OCGWrapper.Enums
{
	// Token: 0x020001D4 RID: 468
	public enum DuelPhase
	{
		// Token: 0x04000C29 RID: 3113
		Draw = 1,
		// Token: 0x04000C2A RID: 3114
		Standby,
		// Token: 0x04000C2B RID: 3115
		Main1 = 4,
		// Token: 0x04000C2C RID: 3116
		BattleStart = 8,
		// Token: 0x04000C2D RID: 3117
		BattleStep = 16,
		// Token: 0x04000C2E RID: 3118
		Damage = 32,
		// Token: 0x04000C2F RID: 3119
		DamageCal = 64,
		// Token: 0x04000C30 RID: 3120
		Battle = 128,
		// Token: 0x04000C31 RID: 3121
		Main2 = 256,
		// Token: 0x04000C32 RID: 3122
		End = 512
	}
}
