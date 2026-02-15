using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the direction in which a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control is displayed relative to its parent control.</summary>
	// Token: 0x020001C5 RID: 453
	public enum ToolStripDropDownDirection
	{
		/// <summary>Uses the mouse position to specify that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed above and to the left of its parent control.</summary>
		// Token: 0x04000BDE RID: 3038
		AboveLeft,
		/// <summary>Uses the mouse position to specify that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed above and to the right of its parent control.</summary>
		// Token: 0x04000BDF RID: 3039
		AboveRight,
		/// <summary>Uses the mouse position to specify that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed below and to the left of its parent control.</summary>
		// Token: 0x04000BE0 RID: 3040
		BelowLeft,
		/// <summary>Uses the mouse position to specify that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed below and to the right of its parent control.</summary>
		// Token: 0x04000BE1 RID: 3041
		BelowRight,
		/// <summary>Compensates for nested drop-down controls and specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed to the left of its parent control.</summary>
		// Token: 0x04000BE2 RID: 3042
		Left,
		/// <summary>Compensates for nested drop-down controls and specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed to the right of its parent control.</summary>
		// Token: 0x04000BE3 RID: 3043
		Right,
		/// <summary>Compensates for nested drop-down controls and responds to the <see cref="T:System.Windows.Forms.RightToLeft" /> setting, specifying either <see cref="F:System.Windows.Forms.ToolStripDropDownDirection.Left" /> or <see cref="F:System.Windows.Forms.ToolStripDropDownDirection.Right" /> accordingly.</summary>
		// Token: 0x04000BE4 RID: 3044
		Default = 7
	}
}
