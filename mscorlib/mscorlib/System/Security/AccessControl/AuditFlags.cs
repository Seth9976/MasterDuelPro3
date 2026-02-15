using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the conditions for auditing attempts to access a securable object.</summary>
	// Token: 0x020003EA RID: 1002
	[Flags]
	public enum AuditFlags
	{
		/// <summary>No access attempts are to be audited.</summary>
		// Token: 0x0400106E RID: 4206
		None = 0,
		/// <summary>Successful access attempts are to be audited.</summary>
		// Token: 0x0400106F RID: 4207
		Success = 1,
		/// <summary>Failed access attempts are to be audited.</summary>
		// Token: 0x04001070 RID: 4208
		Failure = 2
	}
}
