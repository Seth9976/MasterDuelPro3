using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies where a <see cref="T:System.Windows.Forms.ToolStripItem" /> is to be layed out.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DA RID: 474
	public enum ToolStripItemPlacement
	{
		/// <summary>Specifies that a <see cref="T:System.Windows.Forms.ToolStripItem" /> is to be layed out on the main <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		// Token: 0x04000C5E RID: 3166
		Main,
		/// <summary>Specifies that a <see cref="T:System.Windows.Forms.ToolStripItem" /> is to be layed out on the overflow <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		// Token: 0x04000C5F RID: 3167
		Overflow,
		/// <summary>Specifies that a <see cref="T:System.Windows.Forms.ToolStripItem" /> is not to be layed out on the screen.</summary>
		// Token: 0x04000C60 RID: 3168
		None
	}
}
