using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the action that raised a <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000202 RID: 514
	public enum TreeViewAction
	{
		/// <summary>The action that caused the event is unknown.</summary>
		// Token: 0x04000D51 RID: 3409
		Unknown,
		/// <summary>The event was caused by a keystroke.</summary>
		// Token: 0x04000D52 RID: 3410
		ByKeyboard,
		/// <summary>The event was caused by a mouse operation.</summary>
		// Token: 0x04000D53 RID: 3411
		ByMouse,
		/// <summary>The event was caused by the <see cref="T:System.Windows.Forms.TreeNode" /> collapsing.</summary>
		// Token: 0x04000D54 RID: 3412
		Collapse,
		/// <summary>The event was caused by the <see cref="T:System.Windows.Forms.TreeNode" /> expanding.</summary>
		// Token: 0x04000D55 RID: 3413
		Expand
	}
}
