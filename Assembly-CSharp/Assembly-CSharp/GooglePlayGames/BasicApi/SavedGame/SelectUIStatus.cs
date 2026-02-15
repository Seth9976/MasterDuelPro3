using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020011CB RID: 4555
	public enum SelectUIStatus
	{
		// Token: 0x0400C21F RID: 49695
		SavedGameSelected = 1,
		// Token: 0x0400C220 RID: 49696
		UserClosedUI,
		// Token: 0x0400C221 RID: 49697
		InternalError = -1,
		// Token: 0x0400C222 RID: 49698
		TimeoutError = -2,
		// Token: 0x0400C223 RID: 49699
		AuthenticationError = -3,
		// Token: 0x0400C224 RID: 49700
		BadInputError = -4,
		// Token: 0x0400C225 RID: 49701
		UiBusy = -5
	}
}
