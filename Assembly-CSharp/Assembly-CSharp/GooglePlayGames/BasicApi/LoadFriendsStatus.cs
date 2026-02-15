using System;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x020011B6 RID: 4534
	public enum LoadFriendsStatus
	{
		// Token: 0x0400C1C3 RID: 49603
		Unknown,
		// Token: 0x0400C1C4 RID: 49604
		Completed,
		// Token: 0x0400C1C5 RID: 49605
		LoadMore,
		// Token: 0x0400C1C6 RID: 49606
		ResolutionRequired = -3,
		// Token: 0x0400C1C7 RID: 49607
		InternalError = -4,
		// Token: 0x0400C1C8 RID: 49608
		NotAuthorized = -5,
		// Token: 0x0400C1C9 RID: 49609
		NetworkError = -6
	}
}
