using System;

namespace System.Security.Permissions
{
	/// <summary>Defines the smallest unit of a code access security permission set.</summary>
	// Token: 0x0200019D RID: 413
	[Serializable]
	public class ResourcePermissionBaseEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> class.</summary>
		// Token: 0x060009E7 RID: 2535 RVA: 0x00033AB3 File Offset: 0x00031CB3
		public ResourcePermissionBaseEntry()
		{
			this.permissionAccessPath = new string[0];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> class with the specified permission access and permission access path.</summary>
		/// <param name="permissionAccess">The integer representation of the permission access level enumeration value. The <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccess" /> property is set to this value. </param>
		/// <param name="permissionAccessPath">The array of strings that identify the resource you are protecting. The <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccessPath" /> property is set to this value. </param>
		/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="permissionAccessPath" /> is null. </exception>
		// Token: 0x060009E8 RID: 2536 RVA: 0x00033AC7 File Offset: 0x00031CC7
		public ResourcePermissionBaseEntry(int permissionAccess, string[] permissionAccessPath)
		{
			if (permissionAccessPath == null)
			{
				throw new ArgumentNullException("permissionAccessPath");
			}
			this.permissionAccess = permissionAccess;
			this.permissionAccessPath = permissionAccessPath;
		}

		/// <summary>Gets an integer representation of the access level enumeration value.</summary>
		/// <returns>The access level enumeration value.</returns>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00033AEB File Offset: 0x00031CEB
		public int PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		/// <summary>Gets an array of strings that identify the resource you are protecting.</summary>
		/// <returns>An array of strings that identify the resource you are protecting.</returns>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x00033AF3 File Offset: 0x00031CF3
		public string[] PermissionAccessPath
		{
			get
			{
				return this.permissionAccessPath;
			}
		}

		// Token: 0x04000758 RID: 1880
		private int permissionAccess;

		// Token: 0x04000759 RID: 1881
		private string[] permissionAccessPath;
	}
}
