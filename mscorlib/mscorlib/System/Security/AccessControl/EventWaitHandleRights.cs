using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to named system event objects.</summary>
	// Token: 0x020003F2 RID: 1010
	[Flags]
	public enum EventWaitHandleRights
	{
		/// <summary>The right to set or reset the signaled state of a named event.</summary>
		// Token: 0x04001090 RID: 4240
		Modify = 2,
		/// <summary>The right to delete a named event.</summary>
		// Token: 0x04001091 RID: 4241
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a named event.</summary>
		// Token: 0x04001092 RID: 4242
		ReadPermissions = 131072,
		/// <summary>The right to change the security and audit rules associated with a named event.</summary>
		// Token: 0x04001093 RID: 4243
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a named event.</summary>
		// Token: 0x04001094 RID: 4244
		TakeOwnership = 524288,
		/// <summary>The right to wait on a named event.</summary>
		// Token: 0x04001095 RID: 4245
		Synchronize = 1048576,
		/// <summary>The right to exert full control over a named event, and to modify its access rules and audit rules.</summary>
		// Token: 0x04001096 RID: 4246
		FullControl = 2031619
	}
}
