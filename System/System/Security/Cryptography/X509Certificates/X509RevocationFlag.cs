using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies which X509 certificates in the chain should be checked for revocation.</summary>
	// Token: 0x020001B5 RID: 437
	public enum X509RevocationFlag
	{
		/// <summary>Only the end certificate is checked for revocation.</summary>
		// Token: 0x040007F8 RID: 2040
		EndCertificateOnly,
		/// <summary>The entire chain of certificates is checked for revocation.</summary>
		// Token: 0x040007F9 RID: 2041
		EntireChain,
		/// <summary>The entire chain, except the root certificate, is checked for revocation.</summary>
		// Token: 0x040007FA RID: 2042
		ExcludeRoot
	}
}
