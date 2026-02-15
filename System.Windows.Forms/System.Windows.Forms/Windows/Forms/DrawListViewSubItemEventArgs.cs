using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.DrawSubItem" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000079 RID: 121
	public class DrawListViewSubItemEventArgs : EventArgs
	{
		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> should be drawn by the operating system instead of owner-drawn.</summary>
		/// <returns>true if the subitem should be drawn by the operating system; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x000136F1 File Offset: 0x000118F1
		public bool DrawDefault
		{
			get
			{
				return this.drawDefault;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawListViewSubItemEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> within which to draw. </param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> parent of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw. </param>
		/// <param name="subItem">The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw.</param>
		/// <param name="itemIndex">The index of the parent <see cref="T:System.Windows.Forms.ListViewItem" /> within the <see cref="P:System.Windows.Forms.ListView.Items" /> collection. </param>
		/// <param name="columnIndex">The index of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> column within the <see cref="P:System.Windows.Forms.ListView.Columns" /> collection. </param>
		/// <param name="header">The <see cref="T:System.Windows.Forms.ColumnHeader" /> for the column in which the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> is displayed. </param>
		/// <param name="itemState">A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.ListViewItem" /> parent of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to draw. </param>
		// Token: 0x06000513 RID: 1299 RVA: 0x000136FC File Offset: 0x000118FC
		public DrawListViewSubItemEventArgs(Graphics graphics, Rectangle bounds, ListViewItem item, ListViewItem.ListViewSubItem subItem, int itemIndex, int columnIndex, ColumnHeader header, ListViewItemStates itemState)
		{
			this.bounds = bounds;
			this.columnIndex = columnIndex;
			this.graphics = graphics;
			this.header = header;
			this.item = item;
			this.itemIndex = itemIndex;
			this.itemState = itemState;
			this.subItem = subItem;
		}

		// Token: 0x0400030E RID: 782
		private Rectangle bounds;

		// Token: 0x0400030F RID: 783
		private int columnIndex;

		// Token: 0x04000310 RID: 784
		private bool drawDefault;

		// Token: 0x04000311 RID: 785
		private Graphics graphics;

		// Token: 0x04000312 RID: 786
		private ColumnHeader header;

		// Token: 0x04000313 RID: 787
		private ListViewItem item;

		// Token: 0x04000314 RID: 788
		private int itemIndex;

		// Token: 0x04000315 RID: 789
		private ListViewItemStates itemState;

		// Token: 0x04000316 RID: 790
		private ListViewItem.ListViewSubItem subItem;
	}
}
