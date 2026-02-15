using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ColumnClick" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000036 RID: 54
	public class ColumnClickEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnClickEventArgs" /> class.</summary>
		/// <param name="column">The zero-based index of the column that is clicked. </param>
		// Token: 0x06000123 RID: 291 RVA: 0x0000524D File Offset: 0x0000344D
		public ColumnClickEventArgs(int column)
		{
			this.column = column;
		}

		/// <summary>Gets the zero-based index of the column that is clicked.</summary>
		/// <returns>The zero-based index within the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> of the column that is clicked.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000525C File Offset: 0x0000345C
		public int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x04000130 RID: 304
		private int column;
	}
}
