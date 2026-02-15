using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.VirtualItemsSelectionRangeChanged" /> event. </summary>
	// Token: 0x02000125 RID: 293
	public class ListViewVirtualItemsSelectionRangeChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewVirtualItemsSelectionRangeChangedEventArgs" /> class. </summary>
		/// <param name="startIndex">The index of the first item in the range that has changed.</param>
		/// <param name="endIndex">The index of the last item in the range that has changed.</param>
		/// <param name="isSelected">true to indicate the items are selected; false to indicate the items are deselected.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="startIndex" /> is larger than <paramref name="endIndex." /></exception>
		// Token: 0x06000B7C RID: 2940 RVA: 0x0003101C File Offset: 0x0002F21C
		public ListViewVirtualItemsSelectionRangeChangedEventArgs(int startIndex, int endIndex, bool isSelected)
		{
			this.start_index = startIndex;
			this.end_index = endIndex;
			this.is_selected = isSelected;
		}

		// Token: 0x04000776 RID: 1910
		private bool is_selected;

		// Token: 0x04000777 RID: 1911
		private int end_index;

		// Token: 0x04000778 RID: 1912
		private int start_index;
	}
}
