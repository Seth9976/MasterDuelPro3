using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ItemChecked" /> event of the <see cref="T:System.Windows.Forms.ListView" /> control. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000EB RID: 235
	public class ItemCheckedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ItemCheckedEventArgs" /> class. </summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> that is being checked or unchecked.</param>
		// Token: 0x06000878 RID: 2168 RVA: 0x00024A83 File Offset: 0x00022C83
		public ItemCheckedEventArgs(ListViewItem item)
		{
			this.item = item;
		}

		// Token: 0x04000569 RID: 1385
		private ListViewItem item;
	}
}
