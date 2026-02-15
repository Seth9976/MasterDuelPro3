using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies options on a <see cref="T:System.Windows.Forms.MessageBox" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000141 RID: 321
	[Flags]
	public enum MessageBoxOptions
	{
		/// <summary>The message box is displayed on the active desktop.</summary>
		// Token: 0x04000823 RID: 2083
		DefaultDesktopOnly = 131072,
		/// <summary>The message box text is right-aligned.</summary>
		// Token: 0x04000824 RID: 2084
		RightAlign = 524288,
		/// <summary>Specifies that the message box text is displayed with right to left reading order.</summary>
		// Token: 0x04000825 RID: 2085
		RtlReading = 1048576,
		/// <summary>The message box is displayed on the active desktop.</summary>
		// Token: 0x04000826 RID: 2086
		ServiceNotification = 2097152
	}
}
