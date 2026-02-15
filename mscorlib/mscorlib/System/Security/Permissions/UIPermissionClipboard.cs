using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies the type of clipboard access that is allowed to the calling code.</summary>
	// Token: 0x02000357 RID: 855
	public enum UIPermissionClipboard
	{
		/// <summary>Clipboard can be used without restriction.</summary>
		// Token: 0x04000DE2 RID: 3554
		AllClipboard = 2,
		/// <summary>Clipboard cannot be used.</summary>
		// Token: 0x04000DE3 RID: 3555
		NoClipboard = 0,
		/// <summary>The ability to put data on the clipboard (Copy, Cut) is unrestricted. Intrinsic controls that accept Paste, such as text box, can accept the clipboard data, but user controls that must programmatically read the clipboard cannot.</summary>
		// Token: 0x04000DE4 RID: 3556
		OwnClipboard
	}
}
