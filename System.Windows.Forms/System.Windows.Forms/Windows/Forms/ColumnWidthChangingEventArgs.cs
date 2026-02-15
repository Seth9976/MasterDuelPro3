using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ColumnWidthChanging" /> event. </summary>
	// Token: 0x0200003F RID: 63
	public class ColumnWidthChangingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnWidthChangingEventArgs" /> class with the specified column index and width.</summary>
		/// <param name="columnIndex">The index of the column whose width is changing.</param>
		/// <param name="newWidth">The new width for the column.</param>
		// Token: 0x06000151 RID: 337 RVA: 0x0000596E File Offset: 0x00003B6E
		public ColumnWidthChangingEventArgs(int columnIndex, int newWidth)
			: this(columnIndex, newWidth, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnWidthChangingEventArgs" /> class, specifying the column index and width and whether to cancel the event.</summary>
		/// <param name="columnIndex">The index of the column whose width is changing.</param>
		/// <param name="newWidth">The new width of the column.</param>
		/// <param name="cancel">true to cancel the width change; otherwise, false.</param>
		// Token: 0x06000152 RID: 338 RVA: 0x00005979 File Offset: 0x00003B79
		public ColumnWidthChangingEventArgs(int columnIndex, int newWidth, bool cancel)
			: base(cancel)
		{
			this.column_index = columnIndex;
			this.new_width = newWidth;
		}

		// Token: 0x04000145 RID: 325
		private int column_index;

		// Token: 0x04000146 RID: 326
		private int new_width;
	}
}
