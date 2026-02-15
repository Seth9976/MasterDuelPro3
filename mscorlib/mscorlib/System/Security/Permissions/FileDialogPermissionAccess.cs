using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies the type of access to files allowed through the File dialog boxes.</summary>
	// Token: 0x02000351 RID: 849
	[Flags]
	public enum FileDialogPermissionAccess
	{
		/// <summary>No access to files through the File dialog boxes.</summary>
		// Token: 0x04000DC0 RID: 3520
		None = 0,
		/// <summary>Ability to open files through the File dialog boxes.</summary>
		// Token: 0x04000DC1 RID: 3521
		Open = 1,
		/// <summary>Ability to open and save files through the File dialog boxes.</summary>
		// Token: 0x04000DC2 RID: 3522
		OpenSave = 3,
		/// <summary>Ability to save files through the File dialog boxes.</summary>
		// Token: 0x04000DC3 RID: 3523
		Save = 2
	}
}
