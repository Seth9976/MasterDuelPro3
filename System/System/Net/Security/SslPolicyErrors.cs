using System;

namespace System.Net.Security
{
	/// <summary>Enumerates Secure Socket Layer (SSL) policy errors.</summary>
	// Token: 0x020004F2 RID: 1266
	[Flags]
	public enum SslPolicyErrors
	{
		/// <summary>No SSL policy errors.</summary>
		// Token: 0x04001679 RID: 5753
		None = 0,
		/// <summary>Certificate not available.</summary>
		// Token: 0x0400167A RID: 5754
		RemoteCertificateNotAvailable = 1,
		/// <summary>Certificate name mismatch.</summary>
		// Token: 0x0400167B RID: 5755
		RemoteCertificateNameMismatch = 2,
		/// <summary>
		///   <see cref="P:System.Security.Cryptography.X509Certificates.X509Chain.ChainStatus" /> has returned a non empty array.</summary>
		// Token: 0x0400167C RID: 5756
		RemoteCertificateChainErrors = 4
	}
}
