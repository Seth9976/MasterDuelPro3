using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderItemText" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DD RID: 477
	public class ToolStripItemTextRenderEventArgs : ToolStripItemRenderEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemTextRenderEventArgs" /> class with the specified text and text properties. </summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text.</param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to draw the text.</param>
		/// <param name="text">The text to be drawn.</param>
		/// <param name="textRectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds to draw the text in.</param>
		/// <param name="textColor">The <see cref="T:System.Drawing.Color" /> used to draw the text.</param>
		/// <param name="textFont">The <see cref="T:System.Drawing.Font" /> used to draw the text.</param>
		/// <param name="textAlign">The <see cref="T:System.Drawing.ContentAlignment" /> that specifies the vertical and horizontal alignment of the text in the bounding area.</param>
		// Token: 0x0600145C RID: 5212 RVA: 0x00065934 File Offset: 0x00063B34
		public ToolStripItemTextRenderEventArgs(Graphics g, ToolStripItem item, string text, Rectangle textRectangle, Color textColor, Font textFont, ContentAlignment textAlign)
			: base(g, item)
		{
			this.text = text;
			this.text_rectangle = textRectangle;
			this.text_color = textColor;
			this.text_font = textFont;
			this.text_direction = item.TextDirection;
			if (textAlign <= ContentAlignment.MiddleCenter)
			{
				switch (textAlign)
				{
				case ContentAlignment.TopLeft:
					this.text_format = TextFormatFlags.Left;
					goto IL_00DF;
				case ContentAlignment.TopCenter:
					this.text_format = TextFormatFlags.HorizontalCenter;
					goto IL_00DF;
				case (ContentAlignment)3:
					break;
				case ContentAlignment.TopRight:
					this.text_format = TextFormatFlags.Right;
					goto IL_00DF;
				default:
					if (textAlign != ContentAlignment.MiddleLeft)
					{
						if (textAlign == ContentAlignment.MiddleCenter)
						{
							this.text_format = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
							goto IL_00DF;
						}
					}
					break;
				}
			}
			else if (textAlign <= ContentAlignment.BottomLeft)
			{
				if (textAlign == ContentAlignment.MiddleRight)
				{
					this.text_format = TextFormatFlags.Right | TextFormatFlags.VerticalCenter;
					goto IL_00DF;
				}
				if (textAlign == ContentAlignment.BottomLeft)
				{
					this.text_format = TextFormatFlags.Bottom;
					goto IL_00DF;
				}
			}
			else
			{
				if (textAlign == ContentAlignment.BottomCenter)
				{
					this.text_format = TextFormatFlags.HorizontalCenter | TextFormatFlags.Bottom;
					goto IL_00DF;
				}
				if (textAlign == ContentAlignment.BottomRight)
				{
					this.text_format = TextFormatFlags.Right | TextFormatFlags.Bottom;
					goto IL_00DF;
				}
			}
			this.text_format = TextFormatFlags.VerticalCenter;
			IL_00DF:
			if ((Application.KeyboardCapture == null || !ToolStripManager.ActivatedByKeyboard) && !SystemInformation.MenuAccessKeysUnderlined)
			{
				this.text_format |= TextFormatFlags.HidePrefix;
			}
		}

		/// <summary>Gets or sets the text to be drawn on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A string that represents the text to be painted on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x00065A47 File Offset: 0x00063C47
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		/// <summary>Gets or sets the color of the <see cref="T:System.Windows.Forms.ToolStripItem" /> text. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of the <see cref="T:System.Windows.Forms.ToolStripItem" /> text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x00065A4F File Offset: 0x00063C4F
		public Color TextColor
		{
			get
			{
				return this.text_color;
			}
		}

		/// <summary>Gets or sets whether the text is drawn vertically or horizontally.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x00065A57 File Offset: 0x00063C57
		public ToolStripTextDirection TextDirection
		{
			get
			{
				return this.text_direction;
			}
		}

		/// <summary>Gets or sets the font of the text drawn on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> of the text drawn on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x00065A5F File Offset: 0x00063C5F
		public Font TextFont
		{
			get
			{
				return this.text_font;
			}
		}

		/// <summary>Gets or sets the display and layout information of the text drawn on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values that specify the display and layout information of the drawn text. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x00065A67 File Offset: 0x00063C67
		public TextFormatFlags TextFormat
		{
			get
			{
				return this.text_format;
			}
		}

		/// <summary>Gets or sets the rectangle that represents the bounds to draw the text in.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds to draw the text in.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x00065A6F File Offset: 0x00063C6F
		public Rectangle TextRectangle
		{
			get
			{
				return this.text_rectangle;
			}
		}

		// Token: 0x04000C63 RID: 3171
		private string text;

		// Token: 0x04000C64 RID: 3172
		private Color text_color;

		// Token: 0x04000C65 RID: 3173
		private ToolStripTextDirection text_direction;

		// Token: 0x04000C66 RID: 3174
		private Font text_font;

		// Token: 0x04000C67 RID: 3175
		private TextFormatFlags text_format;

		// Token: 0x04000C68 RID: 3176
		private Rectangle text_rectangle;
	}
}
