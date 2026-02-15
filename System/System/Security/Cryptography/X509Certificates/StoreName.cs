using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the name of the X.509 certificate store to open.</summary>
	// Token: 0x020001AF RID: 431
	public enum StoreName
	{
		/// <summary>The X.509 certificate store for other users.</summary>
		// Token: 0x040007A7 RID: 1959
		AddressBook = 1,
		/// <summary>The X.509 certificate store for third-party certificate authorities (CAs).</summary>
		// Token: 0x040007A8 RID: 1960
		AuthRoot,
		/// <summary>The X.509 certificate store for intermediate certificate authorities (CAs). </summary>
		// Token: 0x040007A9 RID: 1961
		CertificateAuthority,
		/// <summary>The X.509 certificate store for revoked certificates.</summary>
		// Token: 0x040007AA RID: 1962
		Disallowed,
		/// <summary>The X.509 certificate store for personal certificates.</summary>
		// Token: 0x040007AB RID: 1963
		My,
		/// <summary>The X.509 certificate store for trusted root certificate authorities (CAs).</summary>
		// Token: 0x040007AC RID: 1964
		Root,
		/// <summary>The X.509 certificate store for directly trusted people and resources.</summary>
		// Token: 0x040007AD RID: 1965
		TrustedPeople,
		/// <summary>The X.509 certificate store for directly trusted publishers.</summary>
		// Token: 0x040007AE RID: 1966
		TrustedPublisher
	}
}
