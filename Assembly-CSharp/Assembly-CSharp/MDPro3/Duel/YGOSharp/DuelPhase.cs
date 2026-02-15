using System;

namespace MDPro3.Duel.YGOSharp
{
	// Token: 0x02001537 RID: 5431
	public enum DuelPhase
	{
		// Token: 0x0400DD07 RID: 56583
		Draw = 1,
		// Token: 0x0400DD08 RID: 56584
		Standby,
		// Token: 0x0400DD09 RID: 56585
		Main1 = 4,
		// Token: 0x0400DD0A RID: 56586
		BattleStart = 8,
		// Token: 0x0400DD0B RID: 56587
		BattleStep = 16,
		// Token: 0x0400DD0C RID: 56588
		Damage = 32,
		// Token: 0x0400DD0D RID: 56589
		DamageCal = 64,
		// Token: 0x0400DD0E RID: 56590
		Battle = 128,
		// Token: 0x0400DD0F RID: 56591
		Main2 = 256,
		// Token: 0x0400DD10 RID: 56592
		End = 512
	}
}
