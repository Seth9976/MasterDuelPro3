using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ColumnWidthChanged" /> event. </summary>
	// Token: 0x0200003D RID: 61
	public class ColumnWidthChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnWidthChangedEventArgs" /> class. </summary>
		/// <param name="columnIndex">The index of the column whose width is being changed.</param>
		// Token: 0x0600014E RID: 334 RVA: 0x0000595F File Offset: 0x00003B5F
		public ColumnWidthChangedEventArgs(int columnIndex)
		{
			this.column_index = columnIndex;
		}

		// Token: 0x04000144 RID: 324
		private int column_index;
	}
}
