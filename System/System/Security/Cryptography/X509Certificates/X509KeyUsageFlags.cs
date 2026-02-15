using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines how the certificate key can be used. If this value is not defined, the key can be used for any purpose.</summary>
	// Token: 0x020001B3 RID: 435
	[Flags]
	public enum X509KeyUsageFlags
	{
		/// <summary>No key usage parameters.</summary>
		// Token: 0x040007E6 RID: 2022
		None = 0,
		/// <summary>The key can be used for encryption only.</summary>
		// Token: 0x040007E7 RID: 2023
		EncipherOnly = 1,
		/// <summary>The key can be used to sign a certificate revocation list (CRL).</summary>
		// Token: 0x040007E8 RID: 2024
		CrlSign = 2,
		/// <summary>The key can be used to sign certificates.</summary>
		// Token: 0x040007E9 RID: 2025
		KeyCertSign = 4,
		/// <summary>The key can be used to determine key agreement, such as a key created using the Diffie-Hellman key agreement algorithm.</summary>
		// Token: 0x040007EA RID: 2026
		KeyAgreement = 8,
		/// <summary>The key can be used for data encryption.</summary>
		// Token: 0x040007EB RID: 2027
		DataEncipherment = 16,
		/// <summary>The key can be used for key encryption.</summary>
		// Token: 0x040007EC RID: 2028
		KeyEncipherment = 32,
		/// <summary>The key can be used for authentication.</summary>
		// Token: 0x040007ED RID: 2029
		NonRepudiation = 64,
		/// <summary>The key can be used as a digital signature.</summary>
		// Token: 0x040007EE RID: 2030
		DigitalSignature = 128,
		/// <summary>The key can be used for decryption only.</summary>
		// Token: 0x040007EF RID: 2031
		DecipherOnly = 32768
	}
}
