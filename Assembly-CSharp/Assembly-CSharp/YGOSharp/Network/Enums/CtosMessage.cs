using System;

namespace YGOSharp.Network.Enums
{
	// Token: 0x020001DF RID: 479
	public enum CtosMessage
	{
		// Token: 0x04000CD8 RID: 3288
		Response = 1,
		// Token: 0x04000CD9 RID: 3289
		UpdateDeck,
		// Token: 0x04000CDA RID: 3290
		HandResult,
		// Token: 0x04000CDB RID: 3291
		TpResult,
		// Token: 0x04000CDC RID: 3292
		PlayerInfo = 16,
		// Token: 0x04000CDD RID: 3293
		CreateGame,
		// Token: 0x04000CDE RID: 3294
		JoinGame,
		// Token: 0x04000CDF RID: 3295
		LeaveGame,
		// Token: 0x04000CE0 RID: 3296
		Surrender,
		// Token: 0x04000CE1 RID: 3297
		TimeConfirm,
		// Token: 0x04000CE2 RID: 3298
		Chat,
		// Token: 0x04000CE3 RID: 3299
		ExternalAddress,
		// Token: 0x04000CE4 RID: 3300
		HsToDuelist = 32,
		// Token: 0x04000CE5 RID: 3301
		HsToObserver,
		// Token: 0x04000CE6 RID: 3302
		HsReady,
		// Token: 0x04000CE7 RID: 3303
		HsNotReady,
		// Token: 0x04000CE8 RID: 3304
		HsKick,
		// Token: 0x04000CE9 RID: 3305
		HsStart
	}
}
