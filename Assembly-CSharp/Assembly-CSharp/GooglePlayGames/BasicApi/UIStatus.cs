using System;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x020011BB RID: 4539
	public enum UIStatus
	{
		// Token: 0x0400C1E5 RID: 49637
		Valid = 1,
		// Token: 0x0400C1E6 RID: 49638
		InternalError = -2,
		// Token: 0x0400C1E7 RID: 49639
		NotAuthorized = -3,
		// Token: 0x0400C1E8 RID: 49640
		VersionUpdateRequired = -4,
		// Token: 0x0400C1E9 RID: 49641
		Timeout = -5,
		// Token: 0x0400C1EA RID: 49642
		UserClosedUI = -6,
		// Token: 0x0400C1EB RID: 49643
		UiBusy = -12,
		// Token: 0x0400C1EC RID: 49644
		NetworkError = -20
	}
}
