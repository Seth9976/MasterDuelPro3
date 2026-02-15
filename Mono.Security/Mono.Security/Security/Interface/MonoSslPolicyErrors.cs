using System;

namespace Mono.Security.Interface
{
	// Token: 0x0200003F RID: 63
	[Flags]
	public enum MonoSslPolicyErrors
	{
		// Token: 0x040001CD RID: 461
		None = 0,
		// Token: 0x040001CE RID: 462
		RemoteCertificateNotAvailable = 1,
		// Token: 0x040001CF RID: 463
		RemoteCertificateNameMismatch = 2,
		// Token: 0x040001D0 RID: 464
		RemoteCertificateChainErrors = 4
	}
}
