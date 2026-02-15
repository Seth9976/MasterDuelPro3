using System;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.PolicyEnforcement" /> enumeration specifies when the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> should be enforced.</summary>
	// Token: 0x020001A3 RID: 419
	public enum PolicyEnforcement
	{
		/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> is never enforced and extended protection is disabled.</summary>
		// Token: 0x04000764 RID: 1892
		Never,
		/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> is enforced only if the client and server supports extended protection.</summary>
		// Token: 0x04000765 RID: 1893
		WhenSupported,
		/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> is always enforced. Clients that don’t support extended protection will fail to authenticate.</summary>
		// Token: 0x04000766 RID: 1894
		Always
	}
}
