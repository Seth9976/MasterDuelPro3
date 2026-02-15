using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies whether an <see cref="T:System.Security.AccessControl.AccessRule" /> object is used to allow or deny access. These values are not flags, and they cannot be combined.</summary>
	// Token: 0x020003E5 RID: 997
	public enum AccessControlType
	{
		/// <summary>The <see cref="T:System.Security.AccessControl.AccessRule" /> object is used to allow access to a secured object.</summary>
		// Token: 0x04001046 RID: 4166
		Allow,
		/// <summary>The <see cref="T:System.Security.AccessControl.AccessRule" /> object is used to deny access to a secured object.</summary>
		// Token: 0x04001047 RID: 4167
		Deny
	}
}
