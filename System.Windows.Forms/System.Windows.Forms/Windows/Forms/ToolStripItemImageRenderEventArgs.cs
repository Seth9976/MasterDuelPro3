using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderItemImage" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D6 RID: 470
	public class ToolStripItemImageRenderEventArgs : ToolStripItemRenderEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> within the specified space and that has the specified properties.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to paint the image.</param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="imageRectangle">The bounding area of the image.</param>
		// Token: 0x06001450 RID: 5200 RVA: 0x000658CC File Offset: 0x00063ACC
		public ToolStripItemImageRenderEventArgs(Graphics g, ToolStripItem item, Rectangle imageRectangle)
			: this(g, item, null, imageRectangle)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays an image within the specified space and that has the specified properties. </summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to paint the image.</param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to draw the image.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to paint.</param>
		/// <param name="imageRectangle">The bounding area of the image.</param>
		// Token: 0x06001451 RID: 5201 RVA: 0x000658D8 File Offset: 0x00063AD8
		public ToolStripItemImageRenderEventArgs(Graphics g, ToolStripItem item, Image image, Rectangle imageRectangle)
			: base(g, item)
		{
			this.image = image;
			this.image_rectangle = imageRectangle;
		}

		/// <summary>Gets the image painted on the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> painted on the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x000658F1 File Offset: 0x00063AF1
		public Image Image
		{
			get
			{
				return this.image;
			}
		}

		/// <summary>Gets the rectangle that represents the bounding area of the image.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding area of the image.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x000658F9 File Offset: 0x00063AF9
		public Rectangle ImageRectangle
		{
			get
			{
				return this.image_rectangle;
			}
		}

		// Token: 0x04000C54 RID: 3156
		private Image image;

		// Token: 0x04000C55 RID: 3157
		private Rectangle image_rectangle;
	}
}
