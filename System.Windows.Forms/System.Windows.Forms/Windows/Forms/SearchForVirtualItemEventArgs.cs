using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.SearchForVirtualItem" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000187 RID: 391
	public class SearchForVirtualItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SearchForVirtualItemEventArgs" /> class. </summary>
		/// <param name="isTextSearch">A value indicating whether the search is a text search.</param>
		/// <param name="isPrefixSearch">A value indicating whether the search is a prefix search.</param>
		/// <param name="includeSubItemsInSearch">A value indicating whether to include subitems of list items in the search.</param>
		/// <param name="text">The text of the item to search for.</param>
		/// <param name="startingPoint">The <see cref="T:System.Drawing.Point" /> at which to start the search.</param>
		/// <param name="direction">One of the <see cref="T:System.Windows.Forms.SearchDirectionHint" /> values.</param>
		/// <param name="startIndex">The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> at which to start the search.</param>
		// Token: 0x06000EC7 RID: 3783 RVA: 0x00043FC4 File Offset: 0x000421C4
		public SearchForVirtualItemEventArgs(bool isTextSearch, bool isPrefixSearch, bool includeSubItemsInSearch, string text, Point startingPoint, SearchDirectionHint direction, int startIndex)
		{
			this.is_text_search = isTextSearch;
			this.is_prefix_search = isPrefixSearch;
			this.include_sub_items_in_search = includeSubItemsInSearch;
			this.text = text;
			this.starting_point = startingPoint;
			this.direction = direction;
			this.start_index = startIndex;
			this.index = -1;
		}

		/// <summary>Gets or sets the index of the <see cref="T:System.Windows.Forms.ListViewItem" /> found in the <see cref="T:System.Windows.Forms.ListView" /> .</summary>
		/// <returns>The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> found in the <see cref="T:System.Windows.Forms.ListView" />.</returns>
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00044013 File Offset: 0x00042213
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x0400097C RID: 2428
		private SearchDirectionHint direction;

		// Token: 0x0400097D RID: 2429
		private bool include_sub_items_in_search;

		// Token: 0x0400097E RID: 2430
		private int index;

		// Token: 0x0400097F RID: 2431
		private bool is_prefix_search;

		// Token: 0x04000980 RID: 2432
		private bool is_text_search;

		// Token: 0x04000981 RID: 2433
		private int start_index;

		// Token: 0x04000982 RID: 2434
		private Point starting_point;

		// Token: 0x04000983 RID: 2435
		private string text;
	}
}
