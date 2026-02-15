using System;

namespace System.Net.Security
{
	/// <summary>The EncryptionPolicy to use. </summary>
	// Token: 0x020004EF RID: 1263
	public enum EncryptionPolicy
	{
		/// <summary>Require encryption and never allow a NULL cipher.</summary>
		// Token: 0x04001675 RID: 5749
		RequireEncryption,
		/// <summary>Prefer that full encryption be used, but allow a NULL cipher (no encryption) if the server agrees. </summary>
		// Token: 0x04001676 RID: 5750
		AllowNoEncryption,
		/// <summary>Allow no encryption and request that a NULL cipher be used if the other endpoint can handle a NULL cipher.</summary>
		// Token: 0x04001677 RID: 5751
		NoEncryption
	}
}
