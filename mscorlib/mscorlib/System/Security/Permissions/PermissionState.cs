using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies whether a permission should have all or no access to resources at creation.</summary>
	// Token: 0x02000354 RID: 852
	public enum PermissionState
	{
		/// <summary>No access to the resource protected by the permission.</summary>
		// Token: 0x04000DD2 RID: 3538
		None,
		/// <summary>Full access to the resource protected by the permission.</summary>
		// Token: 0x04000DD3 RID: 3539
		Unrestricted
	}
}
