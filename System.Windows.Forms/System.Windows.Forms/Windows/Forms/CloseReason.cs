using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the reason that a form was closed.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000034 RID: 52
	public enum CloseReason
	{
		/// <summary>The cause of the closure was not defined or could not be determined.</summary>
		// Token: 0x04000123 RID: 291
		None,
		/// <summary>The operating system is closing all applications before shutting down.</summary>
		// Token: 0x04000124 RID: 292
		WindowsShutDown,
		/// <summary>The parent form of this multiple document interface (MDI) form is closing.</summary>
		// Token: 0x04000125 RID: 293
		MdiFormClosing,
		/// <summary>The user is closing the form through the user interface (UI), for example by clicking the Close button on the form window, selecting Close from the window's control menu, or pressing ALT+F4.</summary>
		// Token: 0x04000126 RID: 294
		UserClosing,
		/// <summary>The Microsoft Windows Task Manager is closing the application.</summary>
		// Token: 0x04000127 RID: 295
		TaskManagerClosing,
		/// <summary>The owner form is closing.</summary>
		// Token: 0x04000128 RID: 296
		FormOwnerClosing,
		/// <summary>The <see cref="M:System.Windows.Forms.Application.Exit" /> method of the <see cref="T:System.Windows.Forms.Application" /> class was invoked. </summary>
		// Token: 0x04000129 RID: 297
		ApplicationExitCall
	}
}
