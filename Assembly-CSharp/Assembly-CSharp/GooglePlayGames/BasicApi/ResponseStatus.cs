using System;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x020011B7 RID: 4535
	public enum ResponseStatus
	{
		// Token: 0x0400C1CB RID: 49611
		Success = 1,
		// Token: 0x0400C1CC RID: 49612
		SuccessWithStale,
		// Token: 0x0400C1CD RID: 49613
		LicenseCheckFailed = -1,
		// Token: 0x0400C1CE RID: 49614
		InternalError = -2,
		// Token: 0x0400C1CF RID: 49615
		NotAuthorized = -3,
		// Token: 0x0400C1D0 RID: 49616
		VersionUpdateRequired = -4,
		// Token: 0x0400C1D1 RID: 49617
		Timeout = -5,
		// Token: 0x0400C1D2 RID: 49618
		ResolutionRequired = -6
	}
}
