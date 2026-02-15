using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.DrawColumnHeader" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000075 RID: 117
	public class DrawListViewColumnHeaderEventArgs : EventArgs
	{
		/// <summary>Gets or sets a value indicating whether the column header should be drawn by the operating system instead of owner-drawn.</summary>
		/// <returns>true if the header should be drawn by the operating system; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00013661 File Offset: 0x00011861
		public bool DrawDefault
		{
			get
			{
				return this.drawDefault;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawListViewColumnHeaderEventArgs" /> class. </summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> within which to draw.</param>
		/// <param name="columnIndex">The index of the header's column within the <see cref="P:System.Windows.Forms.ListView.Columns" /> collection.</param>
		/// <param name="header">The <see cref="T:System.Windows.Forms.ColumnHeader" /> representing the header to draw.</param>
		/// <param name="state">A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the column header.</param>
		/// <param name="foreColor">The foreground <see cref="T:System.Drawing.Color" /> of the header.</param>
		/// <param name="backColor">The background <see cref="T:System.Drawing.Color" /> of the header.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> used for the header text.</param>
		// Token: 0x0600050B RID: 1291 RVA: 0x0001366C File Offset: 0x0001186C
		public DrawListViewColumnHeaderEventArgs(Graphics graphics, Rectangle bounds, int columnIndex, ColumnHeader header, ListViewItemStates state, Color foreColor, Color backColor, Font font)
		{
			this.backColor = backColor;
			this.bounds = bounds;
			this.columnIndex = columnIndex;
			this.font = font;
			this.foreColor = foreColor;
			this.graphics = graphics;
			this.header = header;
			this.state = state;
		}

		// Token: 0x040002FF RID: 767
		private Color backColor;

		// Token: 0x04000300 RID: 768
		private Rectangle bounds;

		// Token: 0x04000301 RID: 769
		private int columnIndex;

		// Token: 0x04000302 RID: 770
		private bool drawDefault;

		// Token: 0x04000303 RID: 771
		private Font font;

		// Token: 0x04000304 RID: 772
		private Color foreColor;

		// Token: 0x04000305 RID: 773
		private Graphics graphics;

		// Token: 0x04000306 RID: 774
		private ColumnHeader header;

		// Token: 0x04000307 RID: 775
		private ListViewItemStates state;
	}
}
