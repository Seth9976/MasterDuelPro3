using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ColumnReordered" /> event. </summary>
	// Token: 0x0200003B RID: 59
	public class ColumnReorderedEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnReorderedEventArgs" /> class. </summary>
		/// <param name="oldDisplayIndex">The previous display position of the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
		/// <param name="newDisplayIndex">The new display position for the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
		/// <param name="header">The <see cref="T:System.Windows.Forms.ColumnHeader" /> that is being reordered.</param>
		// Token: 0x0600014B RID: 331 RVA: 0x00005942 File Offset: 0x00003B42
		public ColumnReorderedEventArgs(int oldDisplayIndex, int newDisplayIndex, ColumnHeader header)
		{
			this.old_display_index = oldDisplayIndex;
			this.new_display_index = newDisplayIndex;
			this.header = header;
		}

		// Token: 0x04000141 RID: 321
		private ColumnHeader header;

		// Token: 0x04000142 RID: 322
		private int new_display_index;

		// Token: 0x04000143 RID: 323
		private int old_display_index;
	}
}
