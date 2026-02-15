using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies the permitted access to registry keys and values.</summary>
	// Token: 0x02000356 RID: 854
	[Flags]
	public enum RegistryPermissionAccess
	{
		/// <summary>
		///   <see cref="F:System.Security.Permissions.RegistryPermissionAccess.Create" />, <see cref="F:System.Security.Permissions.RegistryPermissionAccess.Read" />, and <see cref="F:System.Security.Permissions.RegistryPermissionAccess.Write" /> access to registry variables. <see cref="F:System.Security.Permissions.RegistryPermissionAccess.AllAccess" /> represents multiple <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values and causes an <see cref="T:System.ArgumentException" /> when used as the <paramref name="access" /> parameter for the <see cref="M:System.Security.Permissions.RegistryPermission.GetPathList(System.Security.Permissions.RegistryPermissionAccess)" /> method, which expects a single value.</summary>
		// Token: 0x04000DDC RID: 3548
		AllAccess = 7,
		/// <summary>Create access to registry variables.</summary>
		// Token: 0x04000DDD RID: 3549
		Create = 4,
		/// <summary>No access to registry variables. <see cref="F:System.Security.Permissions.RegistryPermissionAccess.NoAccess" /> represents no valid <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values and causes an <see cref="T:System.ArgumentException" /> when used as the parameter for <see cref="M:System.Security.Permissions.RegistryPermission.GetPathList(System.Security.Permissions.RegistryPermissionAccess)" />, which expects a single value.</summary>
		// Token: 0x04000DDE RID: 3550
		NoAccess = 0,
		/// <summary>Read access to registry variables.</summary>
		// Token: 0x04000DDF RID: 3551
		Read = 1,
		/// <summary>Write access to registry variables.</summary>
		// Token: 0x04000DE0 RID: 3552
		Write = 2
	}
}
