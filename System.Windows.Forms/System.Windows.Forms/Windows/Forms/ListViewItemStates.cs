using System;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that represent the possible states of a <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000124 RID: 292
	[Flags]
	public enum ListViewItemStates
	{
		/// <summary>The item is selected.</summary>
		// Token: 0x0400076D RID: 1901
		Selected = 1,
		/// <summary>The item is disabled.</summary>
		// Token: 0x0400076E RID: 1902
		Grayed = 2,
		/// <summary>The item is checked.</summary>
		// Token: 0x0400076F RID: 1903
		Checked = 8,
		/// <summary>The item has focus.</summary>
		// Token: 0x04000770 RID: 1904
		Focused = 16,
		/// <summary>The item is in its default state.</summary>
		// Token: 0x04000771 RID: 1905
		Default = 32,
		/// <summary>The item is currently under the mouse pointer.</summary>
		// Token: 0x04000772 RID: 1906
		Hot = 64,
		/// <summary>The item is marked.</summary>
		// Token: 0x04000773 RID: 1907
		Marked = 128,
		/// <summary>The item is in an indeterminate state.</summary>
		// Token: 0x04000774 RID: 1908
		Indeterminate = 256,
		/// <summary>The item should indicate a keyboard shortcut.</summary>
		// Token: 0x04000775 RID: 1909
		ShowKeyboardCues = 512
	}
}
