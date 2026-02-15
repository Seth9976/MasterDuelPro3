using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the MeasureItem event of the <see cref="T:System.Windows.Forms.ListBox" />, <see cref="T:System.Windows.Forms.ComboBox" />, <see cref="T:System.Windows.Forms.CheckedListBox" />, and <see cref="T:System.Windows.Forms.MenuItem" /> controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200012E RID: 302
	public class MeasureItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> object being written to. </param>
		/// <param name="index">The index of the item for which you need the height or width. </param>
		// Token: 0x06000BEB RID: 3051 RVA: 0x00033C83 File Offset: 0x00031E83
		public MeasureItemEventArgs(Graphics graphics, int index)
		{
			this.graphics = graphics;
			this.index = index;
			this.itemHeight = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> class providing a parameter for the item height.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> object being written to. </param>
		/// <param name="index">The index of the item for which you need the height or width. </param>
		/// <param name="itemHeight">The height of the item to measure relative to the <paramref name="graphics" /> object. </param>
		// Token: 0x06000BEC RID: 3052 RVA: 0x00033CA0 File Offset: 0x00031EA0
		public MeasureItemEventArgs(Graphics graphics, int index, int itemHeight)
		{
			this.graphics = graphics;
			this.index = index;
			this.itemHeight = itemHeight;
		}

		/// <summary>Gets or sets the height of the item specified by the <see cref="P:System.Windows.Forms.MeasureItemEventArgs.Index" />.</summary>
		/// <returns>The height of the item measured.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x00033CBD File Offset: 0x00031EBD
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x00033CC5 File Offset: 0x00031EC5
		public int ItemHeight
		{
			get
			{
				return this.itemHeight;
			}
			set
			{
				this.itemHeight = value;
			}
		}

		/// <summary>Gets or sets the width of the item specified by the <see cref="P:System.Windows.Forms.MeasureItemEventArgs.Index" />.</summary>
		/// <returns>The width of the item measured.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x00033CCE File Offset: 0x00031ECE
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x00033CD6 File Offset: 0x00031ED6
		public int ItemWidth
		{
			get
			{
				return this.itemWidth;
			}
			set
			{
				this.itemWidth = value;
			}
		}

		// Token: 0x0400079F RID: 1951
		private Graphics graphics;

		// Token: 0x040007A0 RID: 1952
		private int index;

		// Token: 0x040007A1 RID: 1953
		private int itemHeight;

		// Token: 0x040007A2 RID: 1954
		private int itemWidth;
	}
}
