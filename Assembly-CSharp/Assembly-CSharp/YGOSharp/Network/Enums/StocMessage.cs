using System;

namespace YGOSharp.Network.Enums
{
	// Token: 0x020001E4 RID: 484
	public enum StocMessage
	{
		// Token: 0x04000D0C RID: 3340
		GameMsg = 1,
		// Token: 0x04000D0D RID: 3341
		ErrorMsg,
		// Token: 0x04000D0E RID: 3342
		SelectHand,
		// Token: 0x04000D0F RID: 3343
		SelectTp,
		// Token: 0x04000D10 RID: 3344
		HandResult,
		// Token: 0x04000D11 RID: 3345
		TpResult,
		// Token: 0x04000D12 RID: 3346
		ChangeSide,
		// Token: 0x04000D13 RID: 3347
		WaitingSide,
		// Token: 0x04000D14 RID: 3348
		CreateGame = 17,
		// Token: 0x04000D15 RID: 3349
		JoinGame,
		// Token: 0x04000D16 RID: 3350
		TypeChange,
		// Token: 0x04000D17 RID: 3351
		LeaveGame,
		// Token: 0x04000D18 RID: 3352
		DuelStart,
		// Token: 0x04000D19 RID: 3353
		DuelEnd,
		// Token: 0x04000D1A RID: 3354
		Replay,
		// Token: 0x04000D1B RID: 3355
		TimeLimit,
		// Token: 0x04000D1C RID: 3356
		Chat,
		// Token: 0x04000D1D RID: 3357
		HsPlayerEnter = 32,
		// Token: 0x04000D1E RID: 3358
		HsPlayerChange,
		// Token: 0x04000D1F RID: 3359
		HsWatchChange,
		// Token: 0x04000D20 RID: 3360
		TeammateSurrender
	}
}
