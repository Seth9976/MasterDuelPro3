using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000171 RID: 369
	public class RetrieveVirtualItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.RetrieveVirtualItemEventArgs" /> class. </summary>
		/// <param name="itemIndex">The index of the item to retrieve.</param>
		// Token: 0x06000E0A RID: 3594 RVA: 0x0003F31B File Offset: 0x0003D51B
		public RetrieveVirtualItemEventArgs(int itemIndex)
		{
			this.item_index = itemIndex;
		}

		/// <summary>Gets or sets the item retrieved from the cache.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> retrieved from the cache.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0003F32A File Offset: 0x0003D52A
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x040008ED RID: 2285
		private ListViewItem item;

		// Token: 0x040008EE RID: 2286
		private int item_index;
	}
}
