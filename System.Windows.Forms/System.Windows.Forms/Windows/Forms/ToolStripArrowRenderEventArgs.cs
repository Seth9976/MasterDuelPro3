using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderArrow" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B8 RID: 440
	public class ToolStripArrowRenderEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripArrowRenderEventArgs" /> class. </summary>
		/// <param name="g">The graphics used to paint the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</param>
		/// <param name="toolStripItem">The <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to paint the arrow.</param>
		/// <param name="arrowRectangle">The bounding area of the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</param>
		/// <param name="arrowColor">The color of the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</param>
		/// <param name="arrowDirection">The direction in which the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow points.</param>
		// Token: 0x0600131A RID: 4890 RVA: 0x00061D13 File Offset: 0x0005FF13
		public ToolStripArrowRenderEventArgs(Graphics g, ToolStripItem toolStripItem, Rectangle arrowRectangle, Color arrowColor, ArrowDirection arrowDirection)
		{
			this.graphics = g;
			this.tool_strip_item = toolStripItem;
			this.arrow_rectangle = arrowRectangle;
			this.arrow_color = arrowColor;
			this.arrow_direction = arrowDirection;
		}

		/// <summary>Gets or sets the color of the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of the arrow.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x00061D40 File Offset: 0x0005FF40
		public Color ArrowColor
		{
			get
			{
				return this.arrow_color;
			}
		}

		/// <summary>Gets or sets the bounding area of the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding area.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x00061D48 File Offset: 0x0005FF48
		public Rectangle ArrowRectangle
		{
			get
			{
				return this.arrow_rectangle;
			}
		}

		/// <summary>Gets or sets the direction in which the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow points.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ArrowDirection" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x00061D50 File Offset: 0x0005FF50
		public ArrowDirection Direction
		{
			get
			{
				return this.arrow_direction;
			}
		}

		/// <summary>Gets the graphics used to paint the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x00061D58 File Offset: 0x0005FF58
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x04000BB4 RID: 2996
		private Color arrow_color;

		// Token: 0x04000BB5 RID: 2997
		private Rectangle arrow_rectangle;

		// Token: 0x04000BB6 RID: 2998
		private ArrowDirection arrow_direction;

		// Token: 0x04000BB7 RID: 2999
		private Graphics graphics;

		// Token: 0x04000BB8 RID: 3000
		private ToolStripItem tool_strip_item;
	}
}
