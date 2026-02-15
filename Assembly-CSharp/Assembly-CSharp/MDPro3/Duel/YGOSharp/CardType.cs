using System;

namespace MDPro3.Duel.YGOSharp
{
	// Token: 0x0200152E RID: 5422
	public enum CardType
	{
		// Token: 0x0400DBF4 RID: 56308
		Monster = 1,
		// Token: 0x0400DBF5 RID: 56309
		Spell,
		// Token: 0x0400DBF6 RID: 56310
		Trap = 4,
		// Token: 0x0400DBF7 RID: 56311
		Normal = 16,
		// Token: 0x0400DBF8 RID: 56312
		Effect = 32,
		// Token: 0x0400DBF9 RID: 56313
		Fusion = 64,
		// Token: 0x0400DBFA RID: 56314
		Ritual = 128,
		// Token: 0x0400DBFB RID: 56315
		TrapMonster = 256,
		// Token: 0x0400DBFC RID: 56316
		Spirit = 512,
		// Token: 0x0400DBFD RID: 56317
		Union = 1024,
		// Token: 0x0400DBFE RID: 56318
		Dual = 2048,
		// Token: 0x0400DBFF RID: 56319
		Tuner = 4096,
		// Token: 0x0400DC00 RID: 56320
		Synchro = 8192,
		// Token: 0x0400DC01 RID: 56321
		Token = 16384,
		// Token: 0x0400DC02 RID: 56322
		QuickPlay = 65536,
		// Token: 0x0400DC03 RID: 56323
		Continuous = 131072,
		// Token: 0x0400DC04 RID: 56324
		Equip = 262144,
		// Token: 0x0400DC05 RID: 56325
		Field = 524288,
		// Token: 0x0400DC06 RID: 56326
		Counter = 1048576,
		// Token: 0x0400DC07 RID: 56327
		Flip = 2097152,
		// Token: 0x0400DC08 RID: 56328
		Toon = 4194304,
		// Token: 0x0400DC09 RID: 56329
		Xyz = 8388608,
		// Token: 0x0400DC0A RID: 56330
		Pendulum = 16777216,
		// Token: 0x0400DC0B RID: 56331
		SpSummon = 33554432,
		// Token: 0x0400DC0C RID: 56332
		Link = 67108864
	}
}
