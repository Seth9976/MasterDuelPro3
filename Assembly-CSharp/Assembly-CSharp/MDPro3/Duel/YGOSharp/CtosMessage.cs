using System;

namespace MDPro3.Duel.YGOSharp
{
	// Token: 0x02001526 RID: 5414
	public enum CtosMessage
	{
		// Token: 0x0400DB92 RID: 56210
		Response = 1,
		// Token: 0x0400DB93 RID: 56211
		UpdateDeck,
		// Token: 0x0400DB94 RID: 56212
		HandResult,
		// Token: 0x0400DB95 RID: 56213
		TpResult,
		// Token: 0x0400DB96 RID: 56214
		PlayerInfo = 16,
		// Token: 0x0400DB97 RID: 56215
		CreateGame,
		// Token: 0x0400DB98 RID: 56216
		JoinGame,
		// Token: 0x0400DB99 RID: 56217
		LeaveGame,
		// Token: 0x0400DB9A RID: 56218
		Surrender,
		// Token: 0x0400DB9B RID: 56219
		TimeConfirm,
		// Token: 0x0400DB9C RID: 56220
		Chat,
		// Token: 0x0400DB9D RID: 56221
		ExternalAddress,
		// Token: 0x0400DB9E RID: 56222
		HsToDuelist = 32,
		// Token: 0x0400DB9F RID: 56223
		HsToObserver,
		// Token: 0x0400DBA0 RID: 56224
		HsReady,
		// Token: 0x0400DBA1 RID: 56225
		HsNotReady,
		// Token: 0x0400DBA2 RID: 56226
		HsKick,
		// Token: 0x0400DBA3 RID: 56227
		HsStart
	}
}
