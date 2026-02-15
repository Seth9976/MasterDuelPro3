using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the possible alignments with which the items of a <see cref="T:System.Windows.Forms.ToolStrip" /> can be displayed.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E0 RID: 480
	public enum ToolStripLayoutStyle
	{
		/// <summary>Specifies that items are laid out automatically.</summary>
		// Token: 0x04000C6B RID: 3179
		StackWithOverflow,
		/// <summary>Specifies that items are laid out horizontally and overflow as necessary.</summary>
		// Token: 0x04000C6C RID: 3180
		HorizontalStackWithOverflow,
		/// <summary>Specifies that items are laid out vertically, are centered within the control, and overflow as necessary.</summary>
		// Token: 0x04000C6D RID: 3181
		VerticalStackWithOverflow,
		/// <summary>Specifies that items flow horizontally or vertically as necessary.</summary>
		// Token: 0x04000C6E RID: 3182
		Flow,
		/// <summary>Specifies that items are laid out flush left.</summary>
		// Token: 0x04000C6F RID: 3183
		Table
	}
}
