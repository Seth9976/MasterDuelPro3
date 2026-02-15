using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to named system mutex objects.</summary>
	// Token: 0x020003F9 RID: 1017
	[Flags]
	public enum MutexRights
	{
		/// <summary>The right to release a named mutex.</summary>
		// Token: 0x040010A3 RID: 4259
		Modify = 1,
		/// <summary>The right to delete a named mutex.</summary>
		// Token: 0x040010A4 RID: 4260
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a named mutex.</summary>
		// Token: 0x040010A5 RID: 4261
		ReadPermissions = 131072,
		/// <summary>The right to change the security and audit rules associated with a named mutex.</summary>
		// Token: 0x040010A6 RID: 4262
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a named mutex.</summary>
		// Token: 0x040010A7 RID: 4263
		TakeOwnership = 524288,
		/// <summary>The right to wait on a named mutex.</summary>
		// Token: 0x040010A8 RID: 4264
		Synchronize = 1048576,
		/// <summary>The right to exert full control over a named mutex, and to modify its access rules and audit rules.</summary>
		// Token: 0x040010A9 RID: 4265
		FullControl = 2031617
	}
}
