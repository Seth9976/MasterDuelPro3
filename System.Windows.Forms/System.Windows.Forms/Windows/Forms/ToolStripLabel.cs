using System;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a nonselectable <see cref="T:System.Windows.Forms.ToolStripItem" /> that renders text and images and can display hyperlinks.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DF RID: 479
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
	public class ToolStripLabel : ToolStripItem
	{
		// Token: 0x04000C69 RID: 3177
		private static object UIAIsLinkChangedEvent = new object();
	}
}
