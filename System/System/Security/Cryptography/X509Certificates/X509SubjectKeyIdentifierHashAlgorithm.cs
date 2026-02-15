using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines the type of hash algorithm to use with the <see cref="T:System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension" /> class.</summary>
	// Token: 0x020001B7 RID: 439
	public enum X509SubjectKeyIdentifierHashAlgorithm
	{
		/// <summary>The SKI is composed of the 160-bit SHA-1 hash of the value of the public key (excluding the tag, length, and number of unused bits).</summary>
		// Token: 0x04000800 RID: 2048
		Sha1,
		/// <summary>The SKI is composed of a four-bit type field with the value 0100, followed by the least significant 60 bits of the SHA-1 hash of the value of the public key (excluding the tag, length, and number of unused bit string bits)</summary>
		// Token: 0x04000801 RID: 2049
		ShortSha1,
		/// <summary>The subject key identifier (SKI) is composed of a 160-bit SHA-1 hash of the encoded public key (including the tag, length, and number of unused bits).</summary>
		// Token: 0x04000802 RID: 2050
		CapiSha1
	}
}
