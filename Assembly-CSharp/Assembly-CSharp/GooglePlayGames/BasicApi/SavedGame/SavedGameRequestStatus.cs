using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020011CA RID: 4554
	public enum SavedGameRequestStatus
	{
		// Token: 0x0400C219 RID: 49689
		Success = 1,
		// Token: 0x0400C21A RID: 49690
		TimeoutError = -1,
		// Token: 0x0400C21B RID: 49691
		InternalError = -2,
		// Token: 0x0400C21C RID: 49692
		AuthenticationError = -3,
		// Token: 0x0400C21D RID: 49693
		BadInputError = -4
	}
}
