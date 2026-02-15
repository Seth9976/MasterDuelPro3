using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the mode used to check for X509 certificate revocation.</summary>
	// Token: 0x020001B6 RID: 438
	public enum X509RevocationMode
	{
		/// <summary>No revocation check is performed on the certificate.</summary>
		// Token: 0x040007FC RID: 2044
		NoCheck,
		/// <summary>A revocation check is made using an online certificate revocation list (CRL).</summary>
		// Token: 0x040007FD RID: 2045
		Online,
		/// <summary>A revocation check is made using a cached certificate revocation list (CRL).</summary>
		// Token: 0x040007FE RID: 2046
		Offline
	}
}
