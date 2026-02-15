using System;

namespace Mono
{
	// Token: 0x0200002E RID: 46
	[Flags]
	internal enum CertificateImportFlags
	{
		// Token: 0x04000106 RID: 262
		None = 0,
		// Token: 0x04000107 RID: 263
		DisableNativeBackend = 1,
		// Token: 0x04000108 RID: 264
		DisableAutomaticFallback = 2
	}
}
