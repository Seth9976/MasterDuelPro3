using System;

namespace System.Net
{
	/// <summary>Specifies the security protocols that are supported by the Schannel security package.</summary>
	// Token: 0x0200039E RID: 926
	[Flags]
	public enum SecurityProtocolType
	{
		// Token: 0x04000E53 RID: 3667
		SystemDefault = 0,
		/// <summary>Specifies the Secure Socket Layer (SSL) 3.0 security protocol.</summary>
		// Token: 0x04000E54 RID: 3668
		Ssl3 = 48,
		/// <summary>Specifies the Transport Layer Security (TLS) 1.0 security protocol.</summary>
		// Token: 0x04000E55 RID: 3669
		Tls = 192,
		/// <summary>Specifies the Transport Layer Security (TLS) 1.1 security protocol.</summary>
		// Token: 0x04000E56 RID: 3670
		Tls11 = 768,
		/// <summary>Specifies the Transport Layer Security (TLS) 1.2 security protocol.</summary>
		// Token: 0x04000E57 RID: 3671
		Tls12 = 3072,
		// Token: 0x04000E58 RID: 3672
		Tls13 = 12288
	}
}
