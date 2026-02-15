using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Specifies flags that modify the behavior of the cryptographic service providers (CSP).</summary>
	// Token: 0x02000387 RID: 903
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum CspProviderFlags
	{
		/// <summary>Do not specify any settings.</summary>
		// Token: 0x04000EAC RID: 3756
		NoFlags = 0,
		/// <summary>Use key information from the computer's key store.</summary>
		// Token: 0x04000EAD RID: 3757
		UseMachineKeyStore = 1,
		/// <summary>Use key information from the default key container.</summary>
		// Token: 0x04000EAE RID: 3758
		UseDefaultKeyContainer = 2,
		/// <summary>Use key information that cannot be exported.</summary>
		// Token: 0x04000EAF RID: 3759
		UseNonExportableKey = 4,
		/// <summary>Use key information from the current key.</summary>
		// Token: 0x04000EB0 RID: 3760
		UseExistingKey = 8,
		/// <summary>Allow a key to be exported for archival or recovery.</summary>
		// Token: 0x04000EB1 RID: 3761
		UseArchivableKey = 16,
		/// <summary>Notify the user through a dialog box or another method when certain actions are attempting to use a key.  This flag is not compatible with the <see cref="F:System.Security.Cryptography.CspProviderFlags.NoPrompt" /> flag.</summary>
		// Token: 0x04000EB2 RID: 3762
		UseUserProtectedKey = 32,
		/// <summary>Prevent the CSP from displaying any user interface (UI) for this context.</summary>
		// Token: 0x04000EB3 RID: 3763
		NoPrompt = 64,
		/// <summary>Create a temporary key that is released when the associated Rivest-Shamir-Adleman (RSA) object is closed. Do not use this flag if you want your key to be independent of the RSA object.</summary>
		// Token: 0x04000EB4 RID: 3764
		CreateEphemeralKey = 128
	}
}
