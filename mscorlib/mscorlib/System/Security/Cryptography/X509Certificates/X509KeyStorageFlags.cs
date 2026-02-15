using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines where and how to import the private key of an X.509 certificate.</summary>
	// Token: 0x020003CB RID: 971
	[Flags]
	public enum X509KeyStorageFlags
	{
		/// <summary>The default key set is used.  The user key set is usually the default. </summary>
		// Token: 0x04000F82 RID: 3970
		DefaultKeySet = 0,
		/// <summary>Private keys are stored in the current user store rather than the local computer store. This occurs even if the certificate specifies that the keys should go in the local computer store. </summary>
		// Token: 0x04000F83 RID: 3971
		UserKeySet = 1,
		/// <summary>Private keys are stored in the local computer store rather than the current user store. </summary>
		// Token: 0x04000F84 RID: 3972
		MachineKeySet = 2,
		/// <summary>Imported keys are marked as exportable.  </summary>
		// Token: 0x04000F85 RID: 3973
		Exportable = 4,
		/// <summary>Notify the user through a dialog box or other method that the key is accessed.  The Cryptographic Service Provider (CSP) in use defines the precise behavior.</summary>
		// Token: 0x04000F86 RID: 3974
		UserProtected = 8,
		/// <summary>The key associated with a PFX file is persisted when importing a certificate.</summary>
		// Token: 0x04000F87 RID: 3975
		PersistKeySet = 16,
		// Token: 0x04000F88 RID: 3976
		EphemeralKeySet = 32
	}
}
