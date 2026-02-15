using System;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that represent the possible states of a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000200 RID: 512
	[Flags]
	public enum TreeNodeStates
	{
		/// <summary>The node is selected.</summary>
		// Token: 0x04000CFE RID: 3326
		Selected = 1,
		/// <summary>The node is disabled.</summary>
		// Token: 0x04000CFF RID: 3327
		Grayed = 2,
		/// <summary>The node is checked.</summary>
		// Token: 0x04000D00 RID: 3328
		Checked = 8,
		/// <summary>The node has focus.</summary>
		// Token: 0x04000D01 RID: 3329
		Focused = 16,
		/// <summary>The node is in its default state.</summary>
		// Token: 0x04000D02 RID: 3330
		Default = 32,
		/// <summary>The node is hot. This state occurs when the <see cref="P:System.Windows.Forms.TreeView.HotTracking" /> property is set to true and the mouse pointer is over the node.</summary>
		// Token: 0x04000D03 RID: 3331
		Hot = 64,
		/// <summary>The node is marked.</summary>
		// Token: 0x04000D04 RID: 3332
		Marked = 128,
		/// <summary>The node in an indeterminate state.</summary>
		// Token: 0x04000D05 RID: 3333
		Indeterminate = 256,
		/// <summary>The node should indicate a keyboard shortcut.</summary>
		// Token: 0x04000D06 RID: 3334
		ShowKeyboardCues = 512
	}
}
