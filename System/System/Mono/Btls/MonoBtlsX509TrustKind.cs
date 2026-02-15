using System;

namespace Mono.Btls
{
	// Token: 0x020000D0 RID: 208
	[Flags]
	internal enum MonoBtlsX509TrustKind
	{
		// Token: 0x04000327 RID: 807
		DEFAULT = 0,
		// Token: 0x04000328 RID: 808
		TRUST_CLIENT = 1,
		// Token: 0x04000329 RID: 809
		TRUST_SERVER = 2,
		// Token: 0x0400032A RID: 810
		TRUST_ALL = 4,
		// Token: 0x0400032B RID: 811
		REJECT_CLIENT = 32,
		// Token: 0x0400032C RID: 812
		REJECT_SERVER = 64,
		// Token: 0x0400032D RID: 813
		REJECT_ALL = 128
	}
}
