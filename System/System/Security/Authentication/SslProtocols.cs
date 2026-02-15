using System;

namespace System.Security.Authentication
{
	/// <summary>Defines the possible versions of <see cref="T:System.Security.Authentication.SslProtocols" />.</summary>
	// Token: 0x0200019F RID: 415
	[Flags]
	public enum SslProtocols
	{
		/// <summary>No SSL protocol is specified.</summary>
		// Token: 0x0400075B RID: 1883
		None = 0,
		/// <summary>Specifies the SSL 2.0 protocol. SSL 2.0 has been superseded by the TLS protocol and is provided for backward compatibility only.</summary>
		// Token: 0x0400075C RID: 1884
		Ssl2 = 12,
		/// <summary>Specifies the SSL 3.0 protocol. SSL 3.0 has been superseded by the TLS protocol and is provided for backward compatibility only.</summary>
		// Token: 0x0400075D RID: 1885
		Ssl3 = 48,
		/// <summary>Specifies the TLS 1.0 security protocol. The TLS protocol is defined in IETF RFC 2246.</summary>
		// Token: 0x0400075E RID: 1886
		Tls = 192,
		/// <summary>Specifies the TLS 1.1 security protocol. The TLS protocol is defined in IETF RFC 4346.</summary>
		// Token: 0x0400075F RID: 1887
		[MonoTODO("unsupported")]
		Tls11 = 768,
		/// <summary>Specifies the TLS 1.2 security protocol. The TLS protocol is defined in IETF RFC 5246.</summary>
		// Token: 0x04000760 RID: 1888
		[MonoTODO("unsupported")]
		Tls12 = 3072,
		// Token: 0x04000761 RID: 1889
		Tls13 = 12288,
		/// <summary>Specifies that either Secure Sockets Layer (SSL) 3.0 or Transport Layer Security (TLS) 1.0 are acceptable for secure communications</summary>
		// Token: 0x04000762 RID: 1890
		Default = 240
	}
}
