using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStrip.ItemClicked" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CF RID: 463
	public class ToolStripItemClickedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs" /> class, specifying the <see cref="T:System.Windows.Forms.ToolStripItem" /> that was clicked. </summary>
		/// <param name="clickedItem">The <see cref="T:System.Windows.Forms.ToolStripItem" /> that was clicked.</param>
		// Token: 0x0600142E RID: 5166 RVA: 0x000654BB File Offset: 0x000636BB
		public ToolStripItemClickedEventArgs(ToolStripItem clickedItem)
		{
			this.clicked_item = clickedItem;
		}

		// Token: 0x04000C42 RID: 3138
		private ToolStripItem clicked_item;
	}
}
