using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the way to open the X.509 certificate store.</summary>
	// Token: 0x020001AD RID: 429
	[Flags]
	public enum OpenFlags
	{
		/// <summary>Open the X.509 certificate store for reading only.</summary>
		// Token: 0x0400079E RID: 1950
		ReadOnly = 0,
		/// <summary>Open the X.509 certificate store for both reading and writing.</summary>
		// Token: 0x0400079F RID: 1951
		ReadWrite = 1,
		/// <summary>Open the X.509 certificate store for the highest access allowed.</summary>
		// Token: 0x040007A0 RID: 1952
		MaxAllowed = 2,
		/// <summary>Opens only existing stores; if no store exists, the <see cref="M:System.Security.Cryptography.X509Certificates.X509Store.Open(System.Security.Cryptography.X509Certificates.OpenFlags)" /> method will not create a new store.</summary>
		// Token: 0x040007A1 RID: 1953
		OpenExistingOnly = 4,
		/// <summary>Open the X.509 certificate store and include archived certificates.</summary>
		// Token: 0x040007A2 RID: 1954
		IncludeArchived = 8
	}
}
