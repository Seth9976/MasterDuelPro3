using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the format of an X.509 certificate. </summary>
	// Token: 0x020003CA RID: 970
	public enum X509ContentType
	{
		/// <summary>An unknown X.509 certificate.  </summary>
		// Token: 0x04000F79 RID: 3961
		Unknown,
		/// <summary>A single X.509 certificate.</summary>
		// Token: 0x04000F7A RID: 3962
		Cert,
		/// <summary>A single serialized X.509 certificate. </summary>
		// Token: 0x04000F7B RID: 3963
		SerializedCert,
		/// <summary>A PFX-formatted certificate. The Pfx value is identical to the Pkcs12 value.</summary>
		// Token: 0x04000F7C RID: 3964
		Pfx,
		/// <summary>A PKCS #12–formatted certificate. The Pkcs12 value is identical to the Pfx value.</summary>
		// Token: 0x04000F7D RID: 3965
		Pkcs12 = 3,
		/// <summary>A serialized store.</summary>
		// Token: 0x04000F7E RID: 3966
		SerializedStore,
		/// <summary>A PKCS #7–formatted certificate.</summary>
		// Token: 0x04000F7F RID: 3967
		Pkcs7,
		/// <summary>An Authenticode X.509 certificate. </summary>
		// Token: 0x04000F80 RID: 3968
		Authenticode
	}
}
