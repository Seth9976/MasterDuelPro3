using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the DrawItem event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000072 RID: 114
	public class DrawItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> class for the specified control with the specified font, state, surface to draw on, and the bounds to draw within.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to use, usually the parent control's <see cref="T:System.Drawing.Font" /> property. </param>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> bounds to draw within. </param>
		/// <param name="index">The <see cref="T:System.Windows.Forms.Control.ControlCollection" /> index value of the item that is being drawn. </param>
		/// <param name="state">The control's <see cref="T:System.Windows.Forms.DrawItemState" /> information. </param>
		// Token: 0x060004FF RID: 1279 RVA: 0x000135D3 File Offset: 0x000117D3
		public DrawItemEventArgs(Graphics graphics, Font font, Rectangle rect, int index, DrawItemState state)
			: this(graphics, font, rect, index, state, Control.DefaultForeColor, Control.DefaultBackColor)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> class for the specified control with the specified font, state, foreground color, background color, surface to draw on, and the bounds to draw within.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to use, usually the parent control's <see cref="T:System.Drawing.Font" /> property. </param>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> bounds to draw within. </param>
		/// <param name="index">The <see cref="T:System.Windows.Forms.Control.ControlCollection" /> index value of the item that is being drawn. </param>
		/// <param name="state">The control's <see cref="T:System.Windows.Forms.DrawItemState" /> information. </param>
		/// <param name="foreColor">The foreground <see cref="T:System.Drawing.Color" /> to draw the control with. </param>
		/// <param name="backColor">The background <see cref="T:System.Drawing.Color" /> to draw the control with. </param>
		// Token: 0x06000500 RID: 1280 RVA: 0x000135EC File Offset: 0x000117EC
		public DrawItemEventArgs(Graphics graphics, Font font, Rectangle rect, int index, DrawItemState state, Color foreColor, Color backColor)
		{
			this.graphics = graphics;
			this.font = font;
			this.rect = rect;
			this.index = index;
			this.state = state;
			this.fore_color = foreColor;
			this.back_color = backColor;
		}

		/// <summary>Gets the graphics surface to draw the item on.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> surface to draw the item on.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00013629 File Offset: 0x00011829
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the font that is assigned to the item being drawn.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> that is assigned to the item being drawn.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00013631 File Offset: 0x00011831
		public Font Font
		{
			get
			{
				return this.font;
			}
		}

		/// <summary>Gets the rectangle that represents the bounds of the item that is being drawn.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the item that is being drawn.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00013639 File Offset: 0x00011839
		public Rectangle Bounds
		{
			get
			{
				return this.rect;
			}
		}

		/// <summary>Gets the index value of the item that is being drawn.</summary>
		/// <returns>The numeric value that represents the <see cref="P:System.Windows.Forms.Control.ControlCollection.Item(System.Int32)" /> value of the item being drawn.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00013641 File Offset: 0x00011841
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets the state of the item being drawn.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DrawItemState" /> that represents the state of the item being drawn.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00013649 File Offset: 0x00011849
		public DrawItemState State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Gets the background color of the item that is being drawn.</summary>
		/// <returns>The background <see cref="T:System.Drawing.Color" /> of the item that is being drawn.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00013651 File Offset: 0x00011851
		public Color BackColor
		{
			get
			{
				return this.back_color;
			}
		}

		/// <summary>Gets the foreground color of the of the item being drawn.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the item being drawn.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00013659 File Offset: 0x00011859
		public Color ForeColor
		{
			get
			{
				return this.fore_color;
			}
		}

		// Token: 0x040002EB RID: 747
		private Graphics graphics;

		// Token: 0x040002EC RID: 748
		private Font font;

		// Token: 0x040002ED RID: 749
		private Rectangle rect;

		// Token: 0x040002EE RID: 750
		private int index;

		// Token: 0x040002EF RID: 751
		private DrawItemState state;

		// Token: 0x040002F0 RID: 752
		private Color fore_color;

		// Token: 0x040002F1 RID: 753
		private Color back_color;
	}
}
