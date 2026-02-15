using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the initial position of a form.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000B9 RID: 185
	[ComVisible(true)]
	public enum FormStartPosition
	{
		/// <summary>The position of the form is determined by the <see cref="P:System.Windows.Forms.Control.Location" /> property.</summary>
		// Token: 0x040004B4 RID: 1204
		Manual,
		/// <summary>The form is centered on the current display, and has the dimensions specified in the form's size.</summary>
		// Token: 0x040004B5 RID: 1205
		CenterScreen,
		/// <summary>The form is positioned at the Windows default location and has the dimensions specified in the form's size.</summary>
		// Token: 0x040004B6 RID: 1206
		WindowsDefaultLocation,
		/// <summary>The form is positioned at the Windows default location and has the bounds determined by Windows default.</summary>
		// Token: 0x040004B7 RID: 1207
		WindowsDefaultBounds,
		/// <summary>The form is centered within the bounds of its parent form.</summary>
		// Token: 0x040004B8 RID: 1208
		CenterParent
	}
}
