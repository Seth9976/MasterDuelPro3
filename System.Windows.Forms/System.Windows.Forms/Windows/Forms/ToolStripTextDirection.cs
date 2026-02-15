using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the text orientation to use with a particular <see cref="P:System.Windows.Forms.ToolStrip.LayoutStyle" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F2 RID: 498
	public enum ToolStripTextDirection
	{
		/// <summary>Specifies that the text direction is inherited from the parent control.</summary>
		// Token: 0x04000CB3 RID: 3251
		Inherit,
		/// <summary>Specifies horizontal text orientation.</summary>
		// Token: 0x04000CB4 RID: 3252
		Horizontal,
		/// <summary>Specifies that text is to be rotated 90 degrees.</summary>
		// Token: 0x04000CB5 RID: 3253
		Vertical90,
		/// <summary>Specifies that text is to be rotated 270 degrees.</summary>
		// Token: 0x04000CB6 RID: 3254
		Vertical270
	}
}
