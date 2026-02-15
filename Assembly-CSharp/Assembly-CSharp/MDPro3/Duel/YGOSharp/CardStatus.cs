using System;

namespace MDPro3.Duel.YGOSharp
{
	// Token: 0x02001532 RID: 5426
	public enum CardStatus
	{
		// Token: 0x0400DC53 RID: 56403
		Disabled = 1,
		// Token: 0x0400DC54 RID: 56404
		ToEnable,
		// Token: 0x0400DC55 RID: 56405
		ToDisable = 4,
		// Token: 0x0400DC56 RID: 56406
		Proc_Complete = 8,
		// Token: 0x0400DC57 RID: 56407
		Set_Turn = 16,
		// Token: 0x0400DC58 RID: 56408
		No_Level = 32,
		// Token: 0x0400DC59 RID: 56409
		BattleResult = 64,
		// Token: 0x0400DC5A RID: 56410
		SpSummonStep = 128,
		// Token: 0x0400DC5B RID: 56411
		FormChanged = 256,
		// Token: 0x0400DC5C RID: 56412
		Summing = 512,
		// Token: 0x0400DC5D RID: 56413
		EffectEnabled = 1024,
		// Token: 0x0400DC5E RID: 56414
		SummonTurn = 2048,
		// Token: 0x0400DC5F RID: 56415
		DestroyConfirmed = 4096,
		// Token: 0x0400DC60 RID: 56416
		LeaveConfirmed = 8192,
		// Token: 0x0400DC61 RID: 56417
		BattleDestroyed = 16384,
		// Token: 0x0400DC62 RID: 56418
		CopyingEffect = 32768,
		// Token: 0x0400DC63 RID: 56419
		Chaining = 65536,
		// Token: 0x0400DC64 RID: 56420
		SummonDisabled = 131072,
		// Token: 0x0400DC65 RID: 56421
		ActivateDisabled = 262144,
		// Token: 0x0400DC66 RID: 56422
		EffectReplaced = 524288,
		// Token: 0x0400DC67 RID: 56423
		FutureFusion = 1048576,
		// Token: 0x0400DC68 RID: 56424
		AttackCanceled = 2097152,
		// Token: 0x0400DC69 RID: 56425
		Initializing = 4194304,
		// Token: 0x0400DC6A RID: 56426
		ToHandWithoutConfirm = 8388608,
		// Token: 0x0400DC6B RID: 56427
		JustPos = 16777216,
		// Token: 0x0400DC6C RID: 56428
		ContinuousPos = 33554432,
		// Token: 0x0400DC6D RID: 56429
		Forbidden = 67108864,
		// Token: 0x0400DC6E RID: 56430
		ActFromHand = 134217728,
		// Token: 0x0400DC6F RID: 56431
		OppoBattle = 268435456,
		// Token: 0x0400DC70 RID: 56432
		FlipSummonTurn = 536870912,
		// Token: 0x0400DC71 RID: 56433
		SpsummonTurn = 1073741824
	}
}
