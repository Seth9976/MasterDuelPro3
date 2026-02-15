using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the function of an access control entry (ACE).</summary>
	// Token: 0x020003E8 RID: 1000
	public enum AceQualifier
	{
		/// <summary>Allow access.</summary>
		// Token: 0x04001056 RID: 4182
		AccessAllowed,
		/// <summary>Deny access.</summary>
		// Token: 0x04001057 RID: 4183
		AccessDenied,
		/// <summary>Cause a system audit.</summary>
		// Token: 0x04001058 RID: 4184
		SystemAudit,
		/// <summary>Cause a system alarm.</summary>
		// Token: 0x04001059 RID: 4185
		SystemAlarm
	}
}
