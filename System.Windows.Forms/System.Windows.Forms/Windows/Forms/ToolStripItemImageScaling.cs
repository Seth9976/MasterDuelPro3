using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies whether the size of the image on a <see cref="T:System.Windows.Forms.ToolStripItem" /> is automatically adjusted to fit on a <see cref="T:System.Windows.Forms.ToolStrip" /> while retaining the original image proportions.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D8 RID: 472
	public enum ToolStripItemImageScaling
	{
		/// <summary>Specifies that the size of the image on a <see cref="T:System.Windows.Forms.ToolStripItem" /> is not automatically adjusted to fit on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		// Token: 0x04000C57 RID: 3159
		None,
		/// <summary>Specifies that the size of the image on a <see cref="T:System.Windows.Forms.ToolStripItem" /> is automatically adjusted to fit on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		// Token: 0x04000C58 RID: 3160
		SizeToFit
	}
}
