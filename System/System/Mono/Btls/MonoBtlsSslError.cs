using System;

namespace Mono.Btls
{
	// Token: 0x020000B5 RID: 181
	internal enum MonoBtlsSslError
	{
		// Token: 0x04000296 RID: 662
		None,
		// Token: 0x04000297 RID: 663
		Ssl,
		// Token: 0x04000298 RID: 664
		WantRead,
		// Token: 0x04000299 RID: 665
		WantWrite,
		// Token: 0x0400029A RID: 666
		WantX509Lookup,
		// Token: 0x0400029B RID: 667
		Syscall,
		// Token: 0x0400029C RID: 668
		ZeroReturn,
		// Token: 0x0400029D RID: 669
		WantConnect,
		// Token: 0x0400029E RID: 670
		WantAccept,
		// Token: 0x0400029F RID: 671
		WantChannelIdLookup,
		// Token: 0x040002A0 RID: 672
		PendingSession = 11,
		// Token: 0x040002A1 RID: 673
		PendingCertificate,
		// Token: 0x040002A2 RID: 674
		WantPrivateKeyOperation
	}
}
