using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.DrawItem" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000077 RID: 119
	public class DrawListViewItemEventArgs : EventArgs
	{
		/// <summary>Gets or sets a property indicating whether the <see cref="T:System.Windows.Forms.ListView" /> control will use the default drawing for the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		/// <returns>true if the system draws the item; false if the event handler draws the item. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x000136BC File Offset: 0x000118BC
		public bool DrawDefault
		{
			get
			{
				return this.drawDefault;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawListViewItemEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> to draw. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> within which to draw. </param>
		/// <param name="itemIndex">The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> within the <see cref="P:System.Windows.Forms.ListView.Items" /> collection. </param>
		/// <param name="state">A bitwise combination of <see cref="T:System.Windows.Forms.ListViewItemStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.ListViewItem" /> to draw. </param>
		// Token: 0x0600050F RID: 1295 RVA: 0x000136C4 File Offset: 0x000118C4
		public DrawListViewItemEventArgs(Graphics graphics, ListViewItem item, Rectangle bounds, int itemIndex, ListViewItemStates state)
		{
			this.graphics = graphics;
			this.item = item;
			this.bounds = bounds;
			this.itemIndex = itemIndex;
			this.state = state;
		}

		// Token: 0x04000308 RID: 776
		private Rectangle bounds;

		// Token: 0x04000309 RID: 777
		private bool drawDefault;

		// Token: 0x0400030A RID: 778
		private Graphics graphics;

		// Token: 0x0400030B RID: 779
		private ListViewItem item;

		// Token: 0x0400030C RID: 780
		private int itemIndex;

		// Token: 0x0400030D RID: 781
		private ListViewItemStates state;
	}
}
