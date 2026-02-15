using System;

namespace MDPro3.Duel.YGOSharp
{
	// Token: 0x02001539 RID: 5433
	public enum DuelMode
	{
		// Token: 0x0400DD20 RID: 56608
		TestMode = 1,
		// Token: 0x0400DD21 RID: 56609
		AttackFirstTurn,
		// Token: 0x0400DD22 RID: 56610
		OldReplay = 4,
		// Token: 0x0400DD23 RID: 56611
		ObsoleteRuling = 8,
		// Token: 0x0400DD24 RID: 56612
		PseudoShuffle = 16,
		// Token: 0x0400DD25 RID: 56613
		TagMode = 32,
		// Token: 0x0400DD26 RID: 56614
		SimpleAI = 64,
		// Token: 0x0400DD27 RID: 56615
		ReturnDeckTop = 128
	}
}
