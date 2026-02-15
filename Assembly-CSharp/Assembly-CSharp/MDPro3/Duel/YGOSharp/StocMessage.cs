using System;

namespace MDPro3.Duel.YGOSharp
{
	// Token: 0x0200152B RID: 5419
	public enum StocMessage
	{
		// Token: 0x0400DBC6 RID: 56262
		GameMsg = 1,
		// Token: 0x0400DBC7 RID: 56263
		ErrorMsg,
		// Token: 0x0400DBC8 RID: 56264
		SelectHand,
		// Token: 0x0400DBC9 RID: 56265
		SelectTp,
		// Token: 0x0400DBCA RID: 56266
		HandResult,
		// Token: 0x0400DBCB RID: 56267
		TpResult,
		// Token: 0x0400DBCC RID: 56268
		ChangeSide,
		// Token: 0x0400DBCD RID: 56269
		WaitingSide,
		// Token: 0x0400DBCE RID: 56270
		DeckCount,
		// Token: 0x0400DBCF RID: 56271
		CreateGame = 17,
		// Token: 0x0400DBD0 RID: 56272
		JoinGame,
		// Token: 0x0400DBD1 RID: 56273
		TypeChange,
		// Token: 0x0400DBD2 RID: 56274
		LeaveGame,
		// Token: 0x0400DBD3 RID: 56275
		DuelStart,
		// Token: 0x0400DBD4 RID: 56276
		DuelEnd,
		// Token: 0x0400DBD5 RID: 56277
		Replay,
		// Token: 0x0400DBD6 RID: 56278
		TimeLimit,
		// Token: 0x0400DBD7 RID: 56279
		Chat,
		// Token: 0x0400DBD8 RID: 56280
		HsPlayerEnter = 32,
		// Token: 0x0400DBD9 RID: 56281
		HsPlayerChange,
		// Token: 0x0400DBDA RID: 56282
		HsWatchChange,
		// Token: 0x0400DBDB RID: 56283
		TeammateSurrender
	}
}
