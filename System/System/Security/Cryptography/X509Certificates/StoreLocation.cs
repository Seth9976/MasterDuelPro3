using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies the location of the X.509 certificate store.</summary>
	// Token: 0x020001AE RID: 430
	public enum StoreLocation
	{
		/// <summary>The X.509 certificate store used by the current user.</summary>
		// Token: 0x040007A4 RID: 1956
		CurrentUser = 1,
		/// <summary>The X.509 certificate store assigned to the local machine.</summary>
		// Token: 0x040007A5 RID: 1957
		LocalMachine
	}
}
