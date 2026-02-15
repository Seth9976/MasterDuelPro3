using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the border styles for a form.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000B3 RID: 179
	[ComVisible(true)]
	public enum FormBorderStyle
	{
		/// <summary>No border.</summary>
		// Token: 0x040004AA RID: 1194
		None,
		/// <summary>A fixed, single-line border.</summary>
		// Token: 0x040004AB RID: 1195
		FixedSingle,
		/// <summary>A fixed, three-dimensional border.</summary>
		// Token: 0x040004AC RID: 1196
		Fixed3D,
		/// <summary>A thick, fixed dialog-style border.</summary>
		// Token: 0x040004AD RID: 1197
		FixedDialog,
		/// <summary>A resizable border.</summary>
		// Token: 0x040004AE RID: 1198
		Sizable,
		/// <summary>A tool window border that is not resizable. A tool window does not appear in the taskbar or in the window that appears when the user presses ALT+TAB. Although forms that specify <see cref="F:System.Windows.Forms.FormBorderStyle.FixedToolWindow" /> typically are not shown in the taskbar, you must also ensure that the <see cref="P:System.Windows.Forms.Form.ShowInTaskbar" /> property is set to false, since its default value is true.</summary>
		// Token: 0x040004AF RID: 1199
		FixedToolWindow,
		/// <summary>A resizable tool window border. A tool window does not appear in the taskbar or in the window that appears when the user presses ALT+TAB.</summary>
		// Token: 0x040004B0 RID: 1200
		SizableToolWindow
	}
}
