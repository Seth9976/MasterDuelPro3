using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ItemMouseHover" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000120 RID: 288
	[ComVisible(true)]
	public class ListViewItemMouseHoverEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItemMouseHoverEventArgs" /> class. </summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> the mouse pointer is currently hovering over.</param>
		// Token: 0x06000B76 RID: 2934 RVA: 0x00030FF0 File Offset: 0x0002F1F0
		public ListViewItemMouseHoverEventArgs(ListViewItem item)
		{
			this.item = item;
		}

		// Token: 0x04000768 RID: 1896
		private ListViewItem item;
	}
}
