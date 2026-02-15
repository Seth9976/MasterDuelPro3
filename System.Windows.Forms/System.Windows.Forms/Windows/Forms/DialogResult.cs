using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies identifiers to indicate the return value of a dialog box.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200006A RID: 106
	[ComVisible(true)]
	public enum DialogResult
	{
		/// <summary>Nothing is returned from the dialog box. This means that the modal dialog continues running.</summary>
		// Token: 0x040002C5 RID: 709
		None,
		/// <summary>The dialog box return value is OK (usually sent from a button labeled OK).</summary>
		// Token: 0x040002C6 RID: 710
		OK,
		/// <summary>The dialog box return value is Cancel (usually sent from a button labeled Cancel).</summary>
		// Token: 0x040002C7 RID: 711
		Cancel,
		/// <summary>The dialog box return value is Abort (usually sent from a button labeled Abort).</summary>
		// Token: 0x040002C8 RID: 712
		Abort,
		/// <summary>The dialog box return value is Retry (usually sent from a button labeled Retry).</summary>
		// Token: 0x040002C9 RID: 713
		Retry,
		/// <summary>The dialog box return value is Ignore (usually sent from a button labeled Ignore).</summary>
		// Token: 0x040002CA RID: 714
		Ignore,
		/// <summary>The dialog box return value is Yes (usually sent from a button labeled Yes).</summary>
		// Token: 0x040002CB RID: 715
		Yes,
		/// <summary>The dialog box return value is No (usually sent from a button labeled No).</summary>
		// Token: 0x040002CC RID: 716
		No
	}
}
