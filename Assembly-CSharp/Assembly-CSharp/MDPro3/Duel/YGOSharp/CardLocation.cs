using System;

namespace MDPro3.Duel.YGOSharp
{
	// Token: 0x0200152C RID: 5420
	public enum CardLocation
	{
		// Token: 0x0400DBDD RID: 56285
		Unknown,
		// Token: 0x0400DBDE RID: 56286
		Deck,
		// Token: 0x0400DBDF RID: 56287
		Hand,
		// Token: 0x0400DBE0 RID: 56288
		MonsterZone = 4,
		// Token: 0x0400DBE1 RID: 56289
		SpellZone = 8,
		// Token: 0x0400DBE2 RID: 56290
		Grave = 16,
		// Token: 0x0400DBE3 RID: 56291
		Removed = 32,
		// Token: 0x0400DBE4 RID: 56292
		Extra = 64,
		// Token: 0x0400DBE5 RID: 56293
		Overlay = 128,
		// Token: 0x0400DBE6 RID: 56294
		Onfield = 12,
		// Token: 0x0400DBE7 RID: 56295
		FieldZone = 256,
		// Token: 0x0400DBE8 RID: 56296
		PendulumZone = 512,
		// Token: 0x0400DBE9 RID: 56297
		Search = 2048
	}
}
