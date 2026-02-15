using System;

namespace System.Net
{
	// Token: 0x0200038E RID: 910
	internal enum FtpLoginState : byte
	{
		// Token: 0x04000DD3 RID: 3539
		NotLoggedIn,
		// Token: 0x04000DD4 RID: 3540
		LoggedIn,
		// Token: 0x04000DD5 RID: 3541
		LoggedInButNeedsRelogin,
		// Token: 0x04000DD6 RID: 3542
		ReloginFailed
	}
}
