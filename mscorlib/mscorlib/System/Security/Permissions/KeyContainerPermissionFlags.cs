using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the type of key container access allowed.</summary>
	// Token: 0x02000365 RID: 869
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum KeyContainerPermissionFlags
	{
		/// <summary>No access to a key container.</summary>
		// Token: 0x04000E0D RID: 3597
		NoFlags = 0,
		/// <summary>Create a key container.</summary>
		// Token: 0x04000E0E RID: 3598
		Create = 1,
		/// <summary>Open a key container and use the public key.</summary>
		// Token: 0x04000E0F RID: 3599
		Open = 2,
		/// <summary>Delete a key container.</summary>
		// Token: 0x04000E10 RID: 3600
		Delete = 4,
		/// <summary>Import a key into a key container.</summary>
		// Token: 0x04000E11 RID: 3601
		Import = 16,
		/// <summary>Export a key from a key container.</summary>
		// Token: 0x04000E12 RID: 3602
		Export = 32,
		/// <summary>Sign a file using a key.</summary>
		// Token: 0x04000E13 RID: 3603
		Sign = 256,
		/// <summary>Decrypt a key container.</summary>
		// Token: 0x04000E14 RID: 3604
		Decrypt = 512,
		/// <summary>View the access control list (ACL) for a key container.</summary>
		// Token: 0x04000E15 RID: 3605
		ViewAcl = 4096,
		/// <summary>Change the access control list (ACL) for a key container. </summary>
		// Token: 0x04000E16 RID: 3606
		ChangeAcl = 8192,
		/// <summary>Create, decrypt, delete, and open a key container; export and import a key; sign files using a key; and view and change the access control list for a key container.</summary>
		// Token: 0x04000E17 RID: 3607
		AllFlags = 13111
	}
}
