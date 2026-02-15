using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the events that render the background of objects derived from <see cref="T:System.Windows.Forms.ToolStripItem" /> in the <see cref="T:System.Windows.Forms.ToolStripRenderer" /> class. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DB RID: 475
	public class ToolStripItemRenderEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> and using the specified <see cref="T:System.Drawing.Graphics" />. </summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object used to draw the item.</param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to be drawn.</param>
		// Token: 0x06001456 RID: 5206 RVA: 0x00065901 File Offset: 0x00063B01
		public ToolStripItemRenderEventArgs(Graphics g, ToolStripItem item)
		{
			this.graphics = g;
			this.item = item;
		}

		/// <summary>Gets the graphics used to paint the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x00065917 File Offset: 0x00063B17
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripItem" /> to paint.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> to paint.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0006591F File Offset: 0x00063B1F
		public ToolStripItem Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>Gets the value of the <see cref="P:System.Windows.Forms.ToolStripItem.Owner" /> property for the <see cref="T:System.Windows.Forms.ToolStripItem" /> to paint.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStrip" /> that is the owner of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x00065927 File Offset: 0x00063B27
		public ToolStrip ToolStrip
		{
			get
			{
				return this.item.Owner;
			}
		}

		// Token: 0x04000C61 RID: 3169
		private Graphics graphics;

		// Token: 0x04000C62 RID: 3170
		private ToolStripItem item;
	}
}
