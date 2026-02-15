using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.CacheVirtualItems" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200002C RID: 44
	public class CacheVirtualItemsEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CacheVirtualItemsEventArgs" /> class with the specified starting and ending indices.</summary>
		/// <param name="startIndex">The starting index of a range of items needed by the <see cref="T:System.Windows.Forms.ListView" /> for the next <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event that occurs.</param>
		/// <param name="endIndex">The ending index of a range of items needed by the <see cref="T:System.Windows.Forms.ListView" /> for the next <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event that occurs.</param>
		// Token: 0x060000F9 RID: 249 RVA: 0x00004A59 File Offset: 0x00002C59
		public CacheVirtualItemsEventArgs(int startIndex, int endIndex)
		{
			this.start_index = startIndex;
			this.end_index = endIndex;
		}

		// Token: 0x04000108 RID: 264
		private int start_index;

		// Token: 0x04000109 RID: 265
		private int end_index;
	}
}
