using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ItemSelectionChanged" /> event. </summary>
	// Token: 0x02000122 RID: 290
	public class ListViewItemSelectionChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItemSelectionChangedEventArgs" /> class. </summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> whose selection state has changed.</param>
		/// <param name="itemIndex">The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> whose selection state has changed.</param>
		/// <param name="isSelected">true to indicate the item's state has changed to selected; false to indicate the item's state has changed to deselected.</param>
		// Token: 0x06000B79 RID: 2937 RVA: 0x00030FFF File Offset: 0x0002F1FF
		public ListViewItemSelectionChangedEventArgs(ListViewItem item, int itemIndex, bool isSelected)
		{
			this.item = item;
			this.item_index = itemIndex;
			this.is_selected = isSelected;
		}

		// Token: 0x04000769 RID: 1897
		private bool is_selected;

		// Token: 0x0400076A RID: 1898
		private ListViewItem item;

		// Token: 0x0400076B RID: 1899
		private int item_index;
	}
}
