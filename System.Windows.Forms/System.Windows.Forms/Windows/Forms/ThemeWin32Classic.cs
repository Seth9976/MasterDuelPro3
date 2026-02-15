using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms.Theming;

namespace System.Windows.Forms
{
	// Token: 0x020001AA RID: 426
	internal class ThemeWin32Classic : Theme
	{
		// Token: 0x06001145 RID: 4421 RVA: 0x00052C31 File Offset: 0x00050E31
		public ThemeWin32Classic()
		{
			this.ResetDefaults();
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00052C40 File Offset: 0x00050E40
		public override void ResetDefaults()
		{
			this.defaultWindowBackColor = this.ColorWindow;
			this.defaultWindowForeColor = this.ColorControlText;
			this.window_border_font = null;
			ThemeWin32Classic.string_format_menu_text = new StringFormat();
			ThemeWin32Classic.string_format_menu_text.LineAlignment = StringAlignment.Center;
			ThemeWin32Classic.string_format_menu_text.Alignment = StringAlignment.Near;
			ThemeWin32Classic.string_format_menu_text.HotkeyPrefix = HotkeyPrefix.Show;
			ThemeWin32Classic.string_format_menu_text.SetTabStops(0f, new float[] { 50f });
			ThemeWin32Classic.string_format_menu_text.FormatFlags |= StringFormatFlags.NoWrap;
			ThemeWin32Classic.string_format_menu_shortcut = new StringFormat();
			ThemeWin32Classic.string_format_menu_shortcut.LineAlignment = StringAlignment.Center;
			ThemeWin32Classic.string_format_menu_shortcut.Alignment = StringAlignment.Far;
			ThemeWin32Classic.string_format_menu_menubar_text = new StringFormat();
			ThemeWin32Classic.string_format_menu_menubar_text.LineAlignment = StringAlignment.Center;
			ThemeWin32Classic.string_format_menu_menubar_text.Alignment = StringAlignment.Center;
			ThemeWin32Classic.string_format_menu_menubar_text.HotkeyPrefix = HotkeyPrefix.Show;
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x00006F54 File Offset: 0x00005154
		public override bool DoubleBufferingSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x00052D15 File Offset: 0x00050F15
		public override int HorizontalScrollBarHeight
		{
			get
			{
				return XplatUI.HorizontalScrollBarHeight;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x00052D1C File Offset: 0x00050F1C
		public override int VerticalScrollBarWidth
		{
			get
			{
				return XplatUI.VerticalScrollBarWidth;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00052D24 File Offset: 0x00050F24
		public override Font WindowBorderFont
		{
			get
			{
				Font font;
				if ((font = this.window_border_font) == null)
				{
					font = (this.window_border_font = new Font(FontFamily.GenericSansSerif, 8.25f, FontStyle.Bold));
				}
				return font;
			}
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00052D54 File Offset: 0x00050F54
		protected Brush GetControlBackBrush(Color c)
		{
			if (c.ToArgb() == this.DefaultControlBackColor.ToArgb())
			{
				return SystemBrushes.Control;
			}
			return this.ResPool.GetSolidBrush(c);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00052D8C File Offset: 0x00050F8C
		protected Brush GetControlForeBrush(Color c)
		{
			if (c.ToArgb() == this.DefaultControlForeColor.ToArgb())
			{
				return SystemBrushes.ControlText;
			}
			return this.ResPool.GetSolidBrush(c);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00052DC4 File Offset: 0x00050FC4
		public override void DrawButton(Graphics g, Button b, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle)
		{
			this.DrawButtonBackground(g, b, clipRectangle);
			if (imageBounds.Size != Size.Empty)
			{
				this.DrawButtonImage(g, b, imageBounds);
			}
			if (b.Focused && b.Enabled && b.ShowFocusCues)
			{
				this.DrawButtonFocus(g, b);
			}
			if (textBounds != Rectangle.Empty)
			{
				this.DrawButtonText(g, b, textBounds);
			}
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00052E30 File Offset: 0x00051030
		public virtual void DrawButtonBackground(Graphics g, Button button, Rectangle clipArea)
		{
			if (button.Pressed)
			{
				ThemeElements.DrawButton(g, button.ClientRectangle, ButtonThemeState.Pressed, button.BackColor, button.ForeColor);
				return;
			}
			if (button.InternalSelected)
			{
				ThemeElements.DrawButton(g, button.ClientRectangle, ButtonThemeState.Default, button.BackColor, button.ForeColor);
				return;
			}
			if (button.Entered)
			{
				ThemeElements.DrawButton(g, button.ClientRectangle, ButtonThemeState.Entered, button.BackColor, button.ForeColor);
				return;
			}
			if (!button.Enabled)
			{
				ThemeElements.DrawButton(g, button.ClientRectangle, ButtonThemeState.Disabled, button.BackColor, button.ForeColor);
				return;
			}
			ThemeElements.DrawButton(g, button.ClientRectangle, ButtonThemeState.Normal, button.BackColor, button.ForeColor);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00052EDF File Offset: 0x000510DF
		public virtual void DrawButtonFocus(Graphics g, Button button)
		{
			ControlPaint.DrawFocusRectangle(g, Rectangle.Inflate(button.ClientRectangle, -4, -4));
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00052EF6 File Offset: 0x000510F6
		public virtual void DrawButtonImage(Graphics g, ButtonBase button, Rectangle imageBounds)
		{
			if (button.Enabled)
			{
				g.DrawImage(button.Image, imageBounds);
				return;
			}
			this.CPDrawImageDisabled(g, button.Image, imageBounds.Left, imageBounds.Top, this.ColorControl);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00052F30 File Offset: 0x00051130
		public virtual void DrawButtonText(Graphics g, ButtonBase button, Rectangle textBounds)
		{
			if (button.Font != null && button.Font.Height > 0)
			{
				textBounds.Height = Math.Max(textBounds.Height, button.Font.Height);
			}
			if (button.Enabled)
			{
				TextRenderer.DrawTextInternal(g, button.Text, button.Font, textBounds, button.ForeColor, button.TextFormatFlags, button.UseCompatibleTextRendering);
				return;
			}
			this.DrawStringDisabled20(g, button.Text, button.Font, textBounds, button.BackColor, button.TextFormatFlags, button.UseCompatibleTextRendering);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00052FC8 File Offset: 0x000511C8
		public override void DrawFlatButton(Graphics g, ButtonBase b, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle)
		{
			if (b.BackgroundImage == null)
			{
				this.DrawFlatButtonBackground(g, b, clipRectangle);
			}
			if (imageBounds.Size != Size.Empty)
			{
				this.DrawFlatButtonImage(g, b, imageBounds);
			}
			if (b.Focused && b.Enabled && b.ShowFocusCues)
			{
				this.DrawFlatButtonFocus(g, b);
			}
			if (textBounds != Rectangle.Empty)
			{
				this.DrawFlatButtonText(g, b, textBounds);
			}
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0005303C File Offset: 0x0005123C
		public virtual void DrawFlatButtonBackground(Graphics g, ButtonBase button, Rectangle clipArea)
		{
			if (button.Pressed)
			{
				ThemeElements.DrawFlatButton(g, button.ClientRectangle, ButtonThemeState.Pressed, button.BackColor, button.ForeColor, button.FlatAppearance);
				return;
			}
			if (button.InternalSelected)
			{
				if (button.Entered)
				{
					ThemeElements.DrawFlatButton(g, button.ClientRectangle, ButtonThemeState.Entered | ButtonThemeState.Default, button.BackColor, button.ForeColor, button.FlatAppearance);
					return;
				}
				ThemeElements.DrawFlatButton(g, button.ClientRectangle, ButtonThemeState.Default, button.BackColor, button.ForeColor, button.FlatAppearance);
				return;
			}
			else
			{
				if (button.Entered)
				{
					ThemeElements.DrawFlatButton(g, button.ClientRectangle, ButtonThemeState.Entered, button.BackColor, button.ForeColor, button.FlatAppearance);
					return;
				}
				if (!button.Enabled)
				{
					ThemeElements.DrawFlatButton(g, button.ClientRectangle, ButtonThemeState.Disabled, button.BackColor, button.ForeColor, button.FlatAppearance);
					return;
				}
				ThemeElements.DrawFlatButton(g, button.ClientRectangle, ButtonThemeState.Normal, button.BackColor, button.ForeColor, button.FlatAppearance);
				return;
			}
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00053134 File Offset: 0x00051334
		public virtual void DrawFlatButtonFocus(Graphics g, ButtonBase button)
		{
			if (!button.Pressed)
			{
				Color color = ControlPaint.Dark(button.BackColor);
				g.DrawRectangle(this.ResPool.GetPen(color), new Rectangle(button.ClientRectangle.Left + 4, button.ClientRectangle.Top + 4, button.ClientRectangle.Width - 9, button.ClientRectangle.Height - 9));
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x000531AE File Offset: 0x000513AE
		public virtual void DrawFlatButtonImage(Graphics g, ButtonBase button, Rectangle imageBounds)
		{
			this.DrawButtonImage(g, button, imageBounds);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000531B9 File Offset: 0x000513B9
		public virtual void DrawFlatButtonText(Graphics g, ButtonBase button, Rectangle textBounds)
		{
			this.DrawButtonText(g, button, textBounds);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000531C4 File Offset: 0x000513C4
		public override void DrawPopupButton(Graphics g, Button b, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle)
		{
			this.DrawPopupButtonBackground(g, b, clipRectangle);
			if (imageBounds.Size != Size.Empty)
			{
				this.DrawPopupButtonImage(g, b, imageBounds);
			}
			if (b.Focused && b.Enabled && b.ShowFocusCues)
			{
				this.DrawPopupButtonFocus(g, b);
			}
			if (textBounds != Rectangle.Empty)
			{
				this.DrawPopupButtonText(g, b, textBounds);
			}
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00053230 File Offset: 0x00051430
		public virtual void DrawPopupButtonBackground(Graphics g, Button button, Rectangle clipArea)
		{
			if (button.Pressed)
			{
				ThemeElements.DrawPopupButton(g, button.ClientRectangle, ButtonThemeState.Pressed, button.BackColor, button.ForeColor);
				return;
			}
			if (button.Entered)
			{
				ThemeElements.DrawPopupButton(g, button.ClientRectangle, ButtonThemeState.Entered, button.BackColor, button.ForeColor);
				return;
			}
			if (button.InternalSelected)
			{
				ThemeElements.DrawPopupButton(g, button.ClientRectangle, ButtonThemeState.Default, button.BackColor, button.ForeColor);
				return;
			}
			if (!button.Enabled)
			{
				ThemeElements.DrawPopupButton(g, button.ClientRectangle, ButtonThemeState.Disabled, button.BackColor, button.ForeColor);
				return;
			}
			ThemeElements.DrawPopupButton(g, button.ClientRectangle, ButtonThemeState.Normal, button.BackColor, button.ForeColor);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x000532DF File Offset: 0x000514DF
		public virtual void DrawPopupButtonFocus(Graphics g, Button button)
		{
			this.DrawButtonFocus(g, button);
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x000531AE File Offset: 0x000513AE
		public virtual void DrawPopupButtonImage(Graphics g, Button button, Rectangle imageBounds)
		{
			this.DrawButtonImage(g, button, imageBounds);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x000531B9 File Offset: 0x000513B9
		public virtual void DrawPopupButtonText(Graphics g, Button button, Rectangle textBounds)
		{
			this.DrawButtonText(g, button, textBounds);
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x000532EC File Offset: 0x000514EC
		public override Size CalculateButtonAutoSize(Button button)
		{
			Size empty = Size.Empty;
			Size size = TextRenderer.MeasureTextInternal(button.Text, button.Font, button.UseCompatibleTextRendering);
			Size size2 = ((button.Image == null) ? Size.Empty : button.Image.Size);
			if (button.Text.Length != 0)
			{
				size.Height += 4;
				size.Width += 4;
			}
			switch (button.TextImageRelation)
			{
			case TextImageRelation.Overlay:
				empty.Height = Math.Max((button.Text.Length == 0) ? 0 : size.Height, size2.Height);
				empty.Width = Math.Max(size.Width, size2.Width);
				break;
			case TextImageRelation.ImageAboveText:
			case TextImageRelation.TextAboveImage:
				empty.Height = size.Height + size2.Height;
				empty.Width = Math.Max(size.Width, size2.Width);
				break;
			case TextImageRelation.ImageBeforeText:
			case TextImageRelation.TextBeforeImage:
				empty.Height = Math.Max(size.Height, size2.Height);
				empty.Width = size.Width + size2.Width;
				break;
			}
			empty.Height += button.Padding.Vertical + 6;
			empty.Width += button.Padding.Horizontal + 6;
			return empty;
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0005347C File Offset: 0x0005167C
		public override void CalculateButtonTextAndImageLayout(Graphics g, ButtonBase button, out Rectangle textRectangle, out Rectangle imageRectangle)
		{
			Image image = button.Image;
			string text = button.Text;
			Rectangle paddingClientRectangle = button.PaddingClientRectangle;
			Size size = TextRenderer.MeasureTextInternal(g, text, button.Font, paddingClientRectangle.Size, button.TextFormatFlags, button.UseCompatibleTextRendering);
			Size size2 = ((image == null) ? Size.Empty : image.Size);
			textRectangle = Rectangle.Inflate(paddingClientRectangle, -4, -4);
			imageRectangle = Rectangle.Empty;
			bool flag = (button.TextFormatFlags & (TextFormatFlags.PathEllipsis | TextFormatFlags.EndEllipsis | TextFormatFlags.WordEllipsis)) > TextFormatFlags.Left;
			switch (button.TextImageRelation)
			{
			case TextImageRelation.Overlay:
			{
				if (image == null)
				{
					if (button.Pressed)
					{
						textRectangle.Offset(1, 1);
					}
					return;
				}
				int height = image.Height;
				int width = image.Width;
				ContentAlignment imageAlign = button.ImageAlign;
				int num;
				int num2;
				if (imageAlign <= ContentAlignment.MiddleCenter)
				{
					switch (imageAlign)
					{
					case ContentAlignment.TopLeft:
						num = 5;
						num2 = 5;
						goto IL_0233;
					case ContentAlignment.TopCenter:
						num = (paddingClientRectangle.Width - width) / 2;
						num2 = 5;
						goto IL_0233;
					case (ContentAlignment)3:
						break;
					case ContentAlignment.TopRight:
						num = paddingClientRectangle.Width - width - 5;
						num2 = 5;
						goto IL_0233;
					default:
						if (imageAlign == ContentAlignment.MiddleLeft)
						{
							num = 5;
							num2 = (paddingClientRectangle.Height - height) / 2;
							goto IL_0233;
						}
						if (imageAlign == ContentAlignment.MiddleCenter)
						{
							num = (paddingClientRectangle.Width - width) / 2;
							num2 = (paddingClientRectangle.Height - height) / 2;
							goto IL_0233;
						}
						break;
					}
				}
				else if (imageAlign <= ContentAlignment.BottomLeft)
				{
					if (imageAlign == ContentAlignment.MiddleRight)
					{
						num = paddingClientRectangle.Width - width - 4;
						num2 = (paddingClientRectangle.Height - height) / 2;
						goto IL_0233;
					}
					if (imageAlign == ContentAlignment.BottomLeft)
					{
						num = 5;
						num2 = paddingClientRectangle.Height - height - 4;
						goto IL_0233;
					}
				}
				else
				{
					if (imageAlign == ContentAlignment.BottomCenter)
					{
						num = (paddingClientRectangle.Width - width) / 2;
						num2 = paddingClientRectangle.Height - height - 4;
						goto IL_0233;
					}
					if (imageAlign == ContentAlignment.BottomRight)
					{
						num = paddingClientRectangle.Width - width - 4;
						num2 = paddingClientRectangle.Height - height - 4;
						goto IL_0233;
					}
				}
				num = 5;
				num2 = 5;
				IL_0233:
				imageRectangle = new Rectangle(num, num2, width, height);
				break;
			}
			case TextImageRelation.ImageAboveText:
				this.LayoutTextAboveOrBelowImage(textRectangle, false, size, size2, button.TextAlign, button.ImageAlign, flag, out textRectangle, out imageRectangle);
				break;
			case TextImageRelation.TextAboveImage:
				this.LayoutTextAboveOrBelowImage(textRectangle, true, size, size2, button.TextAlign, button.ImageAlign, flag, out textRectangle, out imageRectangle);
				break;
			case TextImageRelation.ImageBeforeText:
				this.LayoutTextBeforeOrAfterImage(textRectangle, false, size, size2, button.TextAlign, button.ImageAlign, out textRectangle, out imageRectangle);
				break;
			case TextImageRelation.TextBeforeImage:
				this.LayoutTextBeforeOrAfterImage(textRectangle, true, size, size2, button.TextAlign, button.ImageAlign, out textRectangle, out imageRectangle);
				break;
			}
			if (button.Pressed)
			{
				textRectangle.Offset(1, 1);
			}
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0005376C File Offset: 0x0005196C
		private void LayoutTextBeforeOrAfterImage(Rectangle totalArea, bool textFirst, Size textSize, Size imageSize, ContentAlignment textAlign, ContentAlignment imageAlign, out Rectangle textRect, out Rectangle imageRect)
		{
			int num = 0;
			int num2 = textSize.Width + num + imageSize.Width;
			if (!textFirst)
			{
				num += 2;
			}
			if (num2 > totalArea.Width)
			{
				textSize.Width = totalArea.Width - num - imageSize.Width;
				num2 = totalArea.Width;
			}
			int num3 = totalArea.Width - num2;
			int num4 = 0;
			HorizontalAlignment horizontalAlignment = this.GetHorizontalAlignment(textAlign);
			HorizontalAlignment horizontalAlignment2 = this.GetHorizontalAlignment(imageAlign);
			if (horizontalAlignment2 == HorizontalAlignment.Left)
			{
				num4 = 0;
			}
			else if (horizontalAlignment2 == HorizontalAlignment.Right && horizontalAlignment == HorizontalAlignment.Right)
			{
				num4 = num3;
			}
			else if (horizontalAlignment2 == HorizontalAlignment.Center && (horizontalAlignment == HorizontalAlignment.Left || horizontalAlignment == HorizontalAlignment.Center))
			{
				num4 += num3 / 3;
			}
			else
			{
				num4 += 2 * (num3 / 3);
			}
			Rectangle rectangle;
			Rectangle rectangle2;
			if (textFirst)
			{
				rectangle = new Rectangle(totalArea.Left + num4, this.AlignInRectangle(totalArea, textSize, textAlign).Top, textSize.Width, textSize.Height);
				rectangle2 = new Rectangle(rectangle.Right + num, this.AlignInRectangle(totalArea, imageSize, imageAlign).Top, imageSize.Width, imageSize.Height);
			}
			else
			{
				rectangle2 = new Rectangle(totalArea.Left + num4, this.AlignInRectangle(totalArea, imageSize, imageAlign).Top, imageSize.Width, imageSize.Height);
				rectangle = new Rectangle(rectangle2.Right + num, this.AlignInRectangle(totalArea, textSize, textAlign).Top, textSize.Width, textSize.Height);
			}
			textRect = rectangle;
			imageRect = rectangle2;
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x000538F0 File Offset: 0x00051AF0
		private void LayoutTextAboveOrBelowImage(Rectangle totalArea, bool textFirst, Size textSize, Size imageSize, ContentAlignment textAlign, ContentAlignment imageAlign, bool displayEllipsis, out Rectangle textRect, out Rectangle imageRect)
		{
			int num = 0;
			int num2 = textSize.Height + num + imageSize.Height;
			if (textFirst)
			{
				num += 2;
			}
			if (textSize.Width > totalArea.Width)
			{
				textSize.Width = totalArea.Width;
			}
			if (num2 > totalArea.Height && textFirst)
			{
				imageSize = Size.Empty;
				num2 = totalArea.Height;
			}
			int num3 = totalArea.Height - num2;
			int num4 = 0;
			ThemeWin32Classic.VerticalAlignment verticalAlignment = this.GetVerticalAlignment(textAlign);
			ThemeWin32Classic.VerticalAlignment verticalAlignment2 = this.GetVerticalAlignment(imageAlign);
			if (verticalAlignment2 == ThemeWin32Classic.VerticalAlignment.Top)
			{
				num4 = 0;
			}
			else if (verticalAlignment2 == ThemeWin32Classic.VerticalAlignment.Bottom && verticalAlignment == ThemeWin32Classic.VerticalAlignment.Bottom)
			{
				num4 = num3;
			}
			else if (verticalAlignment2 == ThemeWin32Classic.VerticalAlignment.Center && (verticalAlignment == ThemeWin32Classic.VerticalAlignment.Top || verticalAlignment == ThemeWin32Classic.VerticalAlignment.Center))
			{
				num4 += num3 / 3;
			}
			else
			{
				num4 += 2 * (num3 / 3);
			}
			Rectangle rectangle;
			Rectangle rectangle2;
			if (textFirst)
			{
				int num5 = ((num3 >= 0) ? (totalArea.Height - imageSize.Height - num) : textSize.Height);
				rectangle = new Rectangle(this.AlignInRectangle(totalArea, textSize, textAlign).Left, totalArea.Top + num4, textSize.Width, num5);
				rectangle2 = new Rectangle(this.AlignInRectangle(totalArea, imageSize, imageAlign).Left, rectangle.Bottom + num, imageSize.Width, imageSize.Height);
			}
			else
			{
				rectangle2 = new Rectangle(this.AlignInRectangle(totalArea, imageSize, imageAlign).Left, totalArea.Top + num4, imageSize.Width, imageSize.Height);
				int num6 = ((num3 >= 0) ? (totalArea.Height - rectangle2.Height) : textSize.Height);
				rectangle = new Rectangle(this.AlignInRectangle(totalArea, textSize, textAlign).Left, rectangle2.Bottom + num, textSize.Width, num6);
				if (rectangle.Bottom > totalArea.Bottom)
				{
					rectangle.Y -= rectangle.Bottom - totalArea.Bottom;
					if (rectangle.Y < totalArea.Top)
					{
						rectangle.Y = totalArea.Top;
					}
				}
			}
			if (displayEllipsis && rectangle.Height > totalArea.Bottom)
			{
				rectangle.Height = totalArea.Bottom - rectangle.Top;
			}
			textRect = rectangle;
			imageRect = rectangle2;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00053B34 File Offset: 0x00051D34
		private HorizontalAlignment GetHorizontalAlignment(ContentAlignment align)
		{
			if (align <= ContentAlignment.MiddleCenter)
			{
				switch (align)
				{
				case ContentAlignment.TopLeft:
					break;
				case ContentAlignment.TopCenter:
					return HorizontalAlignment.Center;
				case (ContentAlignment)3:
					return HorizontalAlignment.Left;
				case ContentAlignment.TopRight:
					return HorizontalAlignment.Right;
				default:
					if (align != ContentAlignment.MiddleLeft)
					{
						if (align != ContentAlignment.MiddleCenter)
						{
							return HorizontalAlignment.Left;
						}
						return HorizontalAlignment.Center;
					}
					break;
				}
			}
			else if (align <= ContentAlignment.BottomLeft)
			{
				if (align == ContentAlignment.MiddleRight)
				{
					return HorizontalAlignment.Right;
				}
				if (align != ContentAlignment.BottomLeft)
				{
					return HorizontalAlignment.Left;
				}
			}
			else
			{
				if (align == ContentAlignment.BottomCenter)
				{
					return HorizontalAlignment.Center;
				}
				if (align != ContentAlignment.BottomRight)
				{
					return HorizontalAlignment.Left;
				}
				return HorizontalAlignment.Right;
			}
			return HorizontalAlignment.Left;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00053B9C File Offset: 0x00051D9C
		private ThemeWin32Classic.VerticalAlignment GetVerticalAlignment(ContentAlignment align)
		{
			if (align > ContentAlignment.MiddleCenter)
			{
				if (align <= ContentAlignment.BottomLeft)
				{
					if (align == ContentAlignment.MiddleRight)
					{
						return ThemeWin32Classic.VerticalAlignment.Center;
					}
					if (align != ContentAlignment.BottomLeft)
					{
						return ThemeWin32Classic.VerticalAlignment.Top;
					}
				}
				else if (align != ContentAlignment.BottomCenter && align != ContentAlignment.BottomRight)
				{
					return ThemeWin32Classic.VerticalAlignment.Top;
				}
				return ThemeWin32Classic.VerticalAlignment.Bottom;
			}
			if (align <= ContentAlignment.TopRight)
			{
				if (align - ContentAlignment.TopLeft > 1 && align != ContentAlignment.TopRight)
				{
					return ThemeWin32Classic.VerticalAlignment.Top;
				}
				return ThemeWin32Classic.VerticalAlignment.Top;
			}
			else if (align != ContentAlignment.MiddleLeft && align != ContentAlignment.MiddleCenter)
			{
				return ThemeWin32Classic.VerticalAlignment.Top;
			}
			return ThemeWin32Classic.VerticalAlignment.Center;
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00053BFC File Offset: 0x00051DFC
		internal Rectangle AlignInRectangle(Rectangle outer, Size inner, ContentAlignment align)
		{
			int num = 0;
			int num2 = 0;
			if (align == ContentAlignment.BottomLeft || align == ContentAlignment.MiddleLeft || align == ContentAlignment.TopLeft)
			{
				num = outer.X;
			}
			else if (align == ContentAlignment.BottomCenter || align == ContentAlignment.MiddleCenter || align == ContentAlignment.TopCenter)
			{
				num = Math.Max(outer.X + (outer.Width - inner.Width) / 2, outer.Left);
			}
			else if (align == ContentAlignment.BottomRight || align == ContentAlignment.MiddleRight || align == ContentAlignment.TopRight)
			{
				num = outer.Right - inner.Width;
			}
			if (align == ContentAlignment.TopCenter || align == ContentAlignment.TopLeft || align == ContentAlignment.TopRight)
			{
				num2 = outer.Y;
			}
			else if (align == ContentAlignment.MiddleCenter || align == ContentAlignment.MiddleLeft || align == ContentAlignment.MiddleRight)
			{
				num2 = outer.Y + (outer.Height - inner.Height) / 2;
			}
			else if (align == ContentAlignment.BottomCenter || align == ContentAlignment.BottomRight || align == ContentAlignment.BottomLeft)
			{
				num2 = outer.Bottom - inner.Height;
			}
			return new Rectangle(num, num2, Math.Min(inner.Width, outer.Width), Math.Min(inner.Height, outer.Height));
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00053D18 File Offset: 0x00051F18
		public override void DrawButtonBase(Graphics dc, Rectangle clip_area, ButtonBase button)
		{
			this.ButtonBase_DrawButton(button, dc);
			if (button.FlatStyle != FlatStyle.System && (button.image != null || button.image_list != null))
			{
				this.ButtonBase_DrawImage(button, dc);
			}
			if (ThemeWin32Classic.ShouldPaintFocusRectagle(button))
			{
				this.ButtonBase_DrawFocus(button, dc);
			}
			if (button.Text != null && button.Text != string.Empty)
			{
				this.ButtonBase_DrawText(button, dc);
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00053D80 File Offset: 0x00051F80
		protected static bool ShouldPaintFocusRectagle(ButtonBase button)
		{
			return (button.Focused || button.paint_as_acceptbutton) && button.Enabled && button.ShowFocusCues;
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00053DA4 File Offset: 0x00051FA4
		protected virtual void ButtonBase_DrawButton(ButtonBase button, Graphics dc)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = button.BackColor.ToArgb() == this.ColorControl.ToArgb();
			CPColor cpcolor = (flag3 ? CPColor.Empty : this.ResPool.GetCPColor(button.BackColor));
			if (button is CheckBox)
			{
				flag = true;
				flag2 = ((CheckBox)button).Checked;
			}
			else if (button is RadioButton)
			{
				flag = true;
				flag2 = ((RadioButton)button).Checked;
			}
			Rectangle rectangle;
			if (button.Focused && button.Enabled && !flag)
			{
				rectangle = Rectangle.Inflate(button.ClientRectangle, -1, -1);
			}
			else
			{
				rectangle = button.ClientRectangle;
			}
			if (button.FlatStyle == FlatStyle.Popup)
			{
				if (!button.is_pressed && !button.is_entered && !flag2)
				{
					this.Internal_DrawButton(dc, rectangle, 1, cpcolor, flag3, button.BackColor);
					return;
				}
				if (!button.is_pressed && button.is_entered && !flag2)
				{
					this.Internal_DrawButton(dc, rectangle, 2, cpcolor, flag3, button.BackColor);
					return;
				}
				if (button.is_pressed || flag2)
				{
					this.Internal_DrawButton(dc, rectangle, 1, cpcolor, flag3, button.BackColor);
					return;
				}
			}
			else
			{
				if (button.FlatStyle == FlatStyle.Flat)
				{
					if (button.is_entered && !button.is_pressed && !flag2)
					{
						if (button.image == null && button.image_list == null)
						{
							Brush brush = (flag3 ? SystemBrushes.ControlDark : this.ResPool.GetSolidBrush(cpcolor.Dark));
							dc.FillRectangle(brush, rectangle);
						}
					}
					else if (button.is_pressed || flag2)
					{
						if (button.image == null && button.image_list == null)
						{
							Brush brush2 = (flag3 ? SystemBrushes.ControlLightLight : this.ResPool.GetSolidBrush(cpcolor.LightLight));
							dc.FillRectangle(brush2, rectangle);
						}
						Pen pen = (flag3 ? SystemPens.ControlDark : this.ResPool.GetPen(cpcolor.Dark));
						dc.DrawRectangle(pen, rectangle.X + 4, rectangle.Y + 4, rectangle.Width - 9, rectangle.Height - 9);
					}
					this.Internal_DrawButton(dc, rectangle, 3, cpcolor, flag3, button.BackColor);
					return;
				}
				if ((!button.is_pressed || !button.Enabled) && !flag2)
				{
					this.Internal_DrawButton(dc, rectangle, 0, cpcolor, flag3, button.BackColor);
					return;
				}
				this.Internal_DrawButton(dc, rectangle, 1, cpcolor, flag3, button.BackColor);
			}
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00054000 File Offset: 0x00052200
		private void Internal_DrawButton(Graphics dc, Rectangle rect, int state, CPColor cpcolor, bool is_ColorControl, Color backcolor)
		{
			switch (state)
			{
			case 0:
			{
				Pen pen = (is_ColorControl ? SystemPens.ControlLightLight : this.ResPool.GetPen(cpcolor.LightLight));
				dc.DrawLine(pen, rect.X, rect.Y, rect.X, rect.Bottom - 2);
				dc.DrawLine(pen, rect.X + 1, rect.Y, rect.Right - 2, rect.Y);
				pen = (is_ColorControl ? SystemPens.Control : this.ResPool.GetPen(backcolor));
				dc.DrawLine(pen, rect.X + 1, rect.Y + 1, rect.X + 1, rect.Bottom - 3);
				dc.DrawLine(pen, rect.X + 2, rect.Y + 1, rect.Right - 3, rect.Y + 1);
				pen = (is_ColorControl ? SystemPens.ControlDark : this.ResPool.GetPen(cpcolor.Dark));
				dc.DrawLine(pen, rect.X + 1, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 2);
				dc.DrawLine(pen, rect.Right - 2, rect.Y + 1, rect.Right - 2, rect.Bottom - 3);
				pen = (is_ColorControl ? SystemPens.ControlDarkDark : this.ResPool.GetPen(cpcolor.DarkDark));
				dc.DrawLine(pen, rect.X, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 1);
				dc.DrawLine(pen, rect.Right - 1, rect.Y, rect.Right - 1, rect.Bottom - 2);
				return;
			}
			case 1:
			{
				Pen pen = (is_ColorControl ? SystemPens.ControlDark : this.ResPool.GetPen(cpcolor.Dark));
				dc.DrawRectangle(pen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
				return;
			}
			case 2:
			{
				Pen pen = (is_ColorControl ? SystemPens.ControlLightLight : this.ResPool.GetPen(cpcolor.LightLight));
				dc.DrawLine(pen, rect.X, rect.Y, rect.X, rect.Bottom - 2);
				dc.DrawLine(pen, rect.X + 1, rect.Y, rect.Right - 2, rect.Y);
				pen = (is_ColorControl ? SystemPens.ControlDark : this.ResPool.GetPen(cpcolor.Dark));
				dc.DrawLine(pen, rect.X, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 1);
				dc.DrawLine(pen, rect.Right - 1, rect.Y, rect.Right - 1, rect.Bottom - 2);
				return;
			}
			case 3:
			{
				Pen pen = (is_ColorControl ? SystemPens.ControlDarkDark : this.ResPool.GetPen(cpcolor.DarkDark));
				dc.DrawRectangle(pen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00054348 File Offset: 0x00052548
		protected virtual void ButtonBase_DrawImage(ButtonBase button, Graphics dc)
		{
			int width = button.ClientSize.Width;
			int height = button.ClientSize.Height;
			Image image;
			if (button.ImageIndex != -1)
			{
				image = button.image_list.Images[button.ImageIndex];
			}
			else
			{
				image = button.image;
			}
			int width2 = image.Width;
			int height2 = image.Height;
			ContentAlignment imageAlign = button.ImageAlign;
			int num;
			int num2;
			if (imageAlign <= ContentAlignment.MiddleCenter)
			{
				switch (imageAlign)
				{
				case ContentAlignment.TopLeft:
					num = 5;
					num2 = 5;
					goto IL_013F;
				case ContentAlignment.TopCenter:
					num = (width - width2) / 2;
					num2 = 5;
					goto IL_013F;
				case (ContentAlignment)3:
					break;
				case ContentAlignment.TopRight:
					num = width - width2 - 5;
					num2 = 5;
					goto IL_013F;
				default:
					if (imageAlign == ContentAlignment.MiddleLeft)
					{
						num = 5;
						num2 = (height - height2) / 2;
						goto IL_013F;
					}
					if (imageAlign == ContentAlignment.MiddleCenter)
					{
						num = (width - width2) / 2;
						num2 = (height - height2) / 2;
						goto IL_013F;
					}
					break;
				}
			}
			else if (imageAlign <= ContentAlignment.BottomLeft)
			{
				if (imageAlign == ContentAlignment.MiddleRight)
				{
					num = width - width2 - 4;
					num2 = (height - height2) / 2;
					goto IL_013F;
				}
				if (imageAlign == ContentAlignment.BottomLeft)
				{
					num = 5;
					num2 = height - height2 - 4;
					goto IL_013F;
				}
			}
			else
			{
				if (imageAlign == ContentAlignment.BottomCenter)
				{
					num = (width - width2) / 2;
					num2 = height - height2 - 4;
					goto IL_013F;
				}
				if (imageAlign == ContentAlignment.BottomRight)
				{
					num = width - width2 - 4;
					num2 = height - height2 - 4;
					goto IL_013F;
				}
			}
			num = 5;
			num2 = 5;
			IL_013F:
			dc.SetClip(new Rectangle(3, 3, width - 5, height - 5));
			if (button.Enabled)
			{
				dc.DrawImage(image, num, num2, width2, height2);
			}
			else
			{
				this.CPDrawImageDisabled(dc, image, num, num2, this.ColorControl);
			}
			dc.ResetClip();
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x000544D8 File Offset: 0x000526D8
		protected virtual void ButtonBase_DrawFocus(ButtonBase button, Graphics dc)
		{
			Color color = button.ForeColor;
			int num = -3;
			if (!(button is CheckBox) && !(button is RadioButton))
			{
				num = -4;
				if (button.FlatStyle == FlatStyle.Popup && !button.is_pressed)
				{
					color = ControlPaint.Dark(button.BackColor);
				}
				dc.DrawRectangle(this.ResPool.GetPen(color), button.ClientRectangle.X, button.ClientRectangle.Y, button.ClientRectangle.Width - 1, button.ClientRectangle.Height - 1);
			}
			if (button.Focused)
			{
				Rectangle rectangle = Rectangle.Inflate(button.ClientRectangle, num, num);
				ControlPaint.DrawFocusRectangle(dc, rectangle);
			}
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0005458C File Offset: 0x0005278C
		protected virtual void ButtonBase_DrawText(ButtonBase button, Graphics dc)
		{
			Rectangle rectangle = Rectangle.Inflate(button.ClientRectangle, -4, -4);
			if (button.is_pressed)
			{
				int num = rectangle.X;
				rectangle.X = num + 1;
				num = rectangle.Y;
				rectangle.Y = num + 1;
			}
			rectangle.Height = Math.Max(button.Font.Height, rectangle.Height);
			if (button.Enabled)
			{
				dc.DrawString(button.Text, button.Font, this.ResPool.GetSolidBrush(button.ForeColor), rectangle, button.text_format);
				return;
			}
			if (button.FlatStyle == FlatStyle.Flat || button.FlatStyle == FlatStyle.Popup)
			{
				dc.DrawString(button.Text, button.Font, this.ResPool.GetSolidBrush(this.ColorGrayText), rectangle, button.text_format);
				return;
			}
			this.CPDrawStringDisabled(dc, button.Text, button.Font, button.BackColor, rectangle, button.text_format);
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x0005468F File Offset: 0x0005288F
		public override Size ButtonBaseDefaultSize
		{
			get
			{
				return new Size(75, 23);
			}
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0005469C File Offset: 0x0005289C
		public override void DrawCheckBox(Graphics g, CheckBox cb, Rectangle glyphArea, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle)
		{
			if (cb.Appearance == Appearance.Button && cb.FlatStyle != FlatStyle.Flat)
			{
				this.ButtonBase_DrawButton(cb, g);
			}
			else if (cb.Appearance != Appearance.Button)
			{
				this.DrawCheckBoxGlyph(g, cb, glyphArea);
			}
			if (cb.Appearance == Appearance.Button && cb.FlatStyle == FlatStyle.Flat)
			{
				this.DrawFlatButton(g, cb, textBounds, imageBounds, clipRectangle);
			}
			if (imageBounds.Size != Size.Empty)
			{
				this.DrawCheckBoxImage(g, cb, imageBounds);
			}
			if (cb.Focused && cb.Enabled && cb.ShowFocusCues && textBounds != Rectangle.Empty)
			{
				this.DrawCheckBoxFocus(g, cb, textBounds);
			}
			if (textBounds != Rectangle.Empty)
			{
				this.DrawCheckBoxText(g, cb, textBounds);
			}
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0005475C File Offset: 0x0005295C
		public virtual void DrawCheckBoxGlyph(Graphics g, CheckBox cb, Rectangle glyphArea)
		{
			if (cb.Pressed)
			{
				ThemeElements.CurrentTheme.CheckBoxPainter.PaintCheckBox(g, glyphArea, cb.BackColor, cb.ForeColor, ElementState.Pressed, cb.FlatStyle, cb.CheckState);
				return;
			}
			if (cb.InternalSelected)
			{
				ThemeElements.CurrentTheme.CheckBoxPainter.PaintCheckBox(g, glyphArea, cb.BackColor, cb.ForeColor, ElementState.Normal, cb.FlatStyle, cb.CheckState);
				return;
			}
			if (cb.Entered)
			{
				ThemeElements.CurrentTheme.CheckBoxPainter.PaintCheckBox(g, glyphArea, cb.BackColor, cb.ForeColor, ElementState.Hot, cb.FlatStyle, cb.CheckState);
				return;
			}
			if (!cb.Enabled)
			{
				ThemeElements.CurrentTheme.CheckBoxPainter.PaintCheckBox(g, glyphArea, cb.BackColor, cb.ForeColor, ElementState.Disabled, cb.FlatStyle, cb.CheckState);
				return;
			}
			ThemeElements.CurrentTheme.CheckBoxPainter.PaintCheckBox(g, glyphArea, cb.BackColor, cb.ForeColor, ElementState.Normal, cb.FlatStyle, cb.CheckState);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0005485F File Offset: 0x00052A5F
		public virtual void DrawCheckBoxFocus(Graphics g, CheckBox cb, Rectangle focusArea)
		{
			ControlPaint.DrawFocusRectangle(g, focusArea);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00052EF6 File Offset: 0x000510F6
		public virtual void DrawCheckBoxImage(Graphics g, CheckBox cb, Rectangle imageBounds)
		{
			if (cb.Enabled)
			{
				g.DrawImage(cb.Image, imageBounds);
				return;
			}
			this.CPDrawImageDisabled(g, cb.Image, imageBounds.Left, imageBounds.Top, this.ColorControl);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00054868 File Offset: 0x00052A68
		public virtual void DrawCheckBoxText(Graphics g, CheckBox cb, Rectangle textBounds)
		{
			if (cb.Enabled)
			{
				TextRenderer.DrawTextInternal(g, cb.Text, cb.Font, textBounds, cb.ForeColor, cb.TextFormatFlags, cb.UseCompatibleTextRendering);
				return;
			}
			this.DrawStringDisabled20(g, cb.Text, cb.Font, textBounds, cb.BackColor, cb.TextFormatFlags, cb.UseCompatibleTextRendering);
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x000548CC File Offset: 0x00052ACC
		public override void CalculateCheckBoxTextAndImageLayout(ButtonBase button, Point p, out Rectangle glyphArea, out Rectangle textRectangle, out Rectangle imageRectangle)
		{
			int num = 13;
			if (button is CheckBox)
			{
				num = (((button as CheckBox).Appearance == Appearance.Normal) ? num : 0);
			}
			glyphArea = new Rectangle(button.Padding.Left, button.Padding.Top, num, num);
			Rectangle paddingClientRectangle = button.PaddingClientRectangle;
			ContentAlignment contentAlignment = ContentAlignment.TopLeft;
			if (button is CheckBox)
			{
				contentAlignment = (button as CheckBox).CheckAlign;
			}
			else if (button is RadioButton)
			{
				contentAlignment = (button as RadioButton).CheckAlign;
			}
			if (contentAlignment <= ContentAlignment.MiddleCenter)
			{
				switch (contentAlignment)
				{
				case ContentAlignment.TopLeft:
					paddingClientRectangle.Width -= num;
					paddingClientRectangle.Offset(num, 0);
					break;
				case ContentAlignment.TopCenter:
					glyphArea.X += (paddingClientRectangle.Width - num) / 2;
					break;
				case (ContentAlignment)3:
					break;
				case ContentAlignment.TopRight:
					glyphArea.X += paddingClientRectangle.Width - num;
					paddingClientRectangle.Width -= num;
					break;
				default:
					if (contentAlignment != ContentAlignment.MiddleLeft)
					{
						if (contentAlignment == ContentAlignment.MiddleCenter)
						{
							glyphArea.Y += (paddingClientRectangle.Height - num) / 2;
							glyphArea.X += (paddingClientRectangle.Width - num) / 2;
						}
					}
					else
					{
						glyphArea.Y += (paddingClientRectangle.Height - num) / 2;
						paddingClientRectangle.Width -= num;
						paddingClientRectangle.Offset(num, 0);
					}
					break;
				}
			}
			else if (contentAlignment <= ContentAlignment.BottomLeft)
			{
				if (contentAlignment != ContentAlignment.MiddleRight)
				{
					if (contentAlignment == ContentAlignment.BottomLeft)
					{
						glyphArea.Y += paddingClientRectangle.Height - num - 2;
						paddingClientRectangle.Width -= num;
						paddingClientRectangle.Offset(num, 0);
					}
				}
				else
				{
					glyphArea.Y += (paddingClientRectangle.Height - num) / 2;
					glyphArea.X += paddingClientRectangle.Width - num;
					paddingClientRectangle.Width -= num;
				}
			}
			else if (contentAlignment != ContentAlignment.BottomCenter)
			{
				if (contentAlignment == ContentAlignment.BottomRight)
				{
					glyphArea.Y += paddingClientRectangle.Height - num - 2;
					glyphArea.X += paddingClientRectangle.Width - num;
					paddingClientRectangle.Width -= num;
				}
			}
			else
			{
				glyphArea.Y += paddingClientRectangle.Height - num - 2;
				glyphArea.X += (paddingClientRectangle.Width - num) / 2;
			}
			Image image = button.Image;
			string text = button.Text;
			Size empty = Size.Empty;
			if (!button.AutoSize)
			{
				empty.Width = button.PaddingClientRectangle.Width - glyphArea.Width - 2;
			}
			Size size = TextRenderer.MeasureTextInternal(text, button.Font, empty, button.TextFormatFlags, button.UseCompatibleTextRendering);
			size.Height = Math.Min(size.Height, paddingClientRectangle.Height);
			size.Width = Math.Min(size.Width, paddingClientRectangle.Width);
			Size size2 = ((image == null) ? Size.Empty : image.Size);
			textRectangle = Rectangle.Empty;
			imageRectangle = Rectangle.Empty;
			switch (button.TextImageRelation)
			{
			case TextImageRelation.Overlay:
			{
				textRectangle.X = paddingClientRectangle.Left + 2;
				textRectangle.Y = button.PaddingClientRectangle.Top + (paddingClientRectangle.Height - size.Height) / 2 - 1;
				textRectangle.Size = size;
				if (image == null)
				{
					return;
				}
				int num2 = button.PaddingClientRectangle.Left;
				int num3 = button.PaddingClientRectangle.Top;
				int height = image.Height;
				int width = image.Width;
				ContentAlignment imageAlign = button.ImageAlign;
				if (imageAlign <= ContentAlignment.MiddleCenter)
				{
					switch (imageAlign)
					{
					case ContentAlignment.TopLeft:
						num2 += 5;
						num3 += 5;
						goto IL_0589;
					case ContentAlignment.TopCenter:
						num2 += (paddingClientRectangle.Width - width) / 2;
						num3 += 5;
						goto IL_0589;
					case (ContentAlignment)3:
						break;
					case ContentAlignment.TopRight:
						num2 += paddingClientRectangle.Width - width - 5;
						num3 += 5;
						goto IL_0589;
					default:
						if (imageAlign == ContentAlignment.MiddleLeft)
						{
							num2 += 5;
							num3 += (paddingClientRectangle.Height - height) / 2;
							goto IL_0589;
						}
						if (imageAlign == ContentAlignment.MiddleCenter)
						{
							num2 += (paddingClientRectangle.Width - width) / 2;
							num3 += (paddingClientRectangle.Height - height) / 2;
							goto IL_0589;
						}
						break;
					}
				}
				else if (imageAlign <= ContentAlignment.BottomLeft)
				{
					if (imageAlign == ContentAlignment.MiddleRight)
					{
						num2 += paddingClientRectangle.Width - width - 4;
						num3 += (paddingClientRectangle.Height - height) / 2;
						goto IL_0589;
					}
					if (imageAlign == ContentAlignment.BottomLeft)
					{
						num2 += 5;
						num3 += paddingClientRectangle.Height - height - 4;
						goto IL_0589;
					}
				}
				else
				{
					if (imageAlign == ContentAlignment.BottomCenter)
					{
						num2 += (paddingClientRectangle.Width - width) / 2;
						num3 += paddingClientRectangle.Height - height - 4;
						goto IL_0589;
					}
					if (imageAlign == ContentAlignment.BottomRight)
					{
						num2 += paddingClientRectangle.Width - width - 4;
						num3 += paddingClientRectangle.Height - height - 4;
						goto IL_0589;
					}
				}
				num2 += 5;
				num3 += 5;
				IL_0589:
				imageRectangle = new Rectangle(num2 + num, num3, width, height);
				return;
			}
			case TextImageRelation.ImageAboveText:
				paddingClientRectangle.Inflate(-4, -4);
				this.LayoutTextAboveOrBelowImage(paddingClientRectangle, false, size, size2, button.TextAlign, button.ImageAlign, false, out textRectangle, out imageRectangle);
				return;
			case TextImageRelation.TextAboveImage:
				paddingClientRectangle.Inflate(-4, -4);
				this.LayoutTextAboveOrBelowImage(paddingClientRectangle, true, size, size2, button.TextAlign, button.ImageAlign, false, out textRectangle, out imageRectangle);
				return;
			case (TextImageRelation)3:
			case (TextImageRelation)5:
			case (TextImageRelation)6:
			case (TextImageRelation)7:
				break;
			case TextImageRelation.ImageBeforeText:
				paddingClientRectangle.Inflate(-4, -4);
				this.LayoutTextBeforeOrAfterImage(paddingClientRectangle, false, size, size2, button.TextAlign, button.ImageAlign, out textRectangle, out imageRectangle);
				return;
			case TextImageRelation.TextBeforeImage:
				paddingClientRectangle.Inflate(-4, -4);
				this.LayoutTextBeforeOrAfterImage(paddingClientRectangle, true, size, size2, button.TextAlign, button.ImageAlign, out textRectangle, out imageRectangle);
				break;
			default:
				return;
			}
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00054F1C File Offset: 0x0005311C
		public override Size CalculateCheckBoxAutoSize(CheckBox checkBox)
		{
			Size empty = Size.Empty;
			Size size = TextRenderer.MeasureTextInternal(checkBox.Text, checkBox.Font, checkBox.UseCompatibleTextRendering);
			Size size2 = ((checkBox.Image == null) ? Size.Empty : checkBox.Image.Size);
			if (checkBox.Text.Length != 0)
			{
				size.Height += 4;
				size.Width += 4;
			}
			switch (checkBox.TextImageRelation)
			{
			case TextImageRelation.Overlay:
				empty.Height = Math.Max((checkBox.Text.Length == 0) ? 0 : size.Height, size2.Height);
				empty.Width = Math.Max(size.Width, size2.Width);
				break;
			case TextImageRelation.ImageAboveText:
			case TextImageRelation.TextAboveImage:
				empty.Height = size.Height + size2.Height;
				empty.Width = Math.Max(size.Width, size2.Width);
				break;
			case TextImageRelation.ImageBeforeText:
			case TextImageRelation.TextBeforeImage:
				empty.Height = Math.Max(size.Height, size2.Height);
				empty.Width = size.Width + size2.Width;
				break;
			}
			empty.Height += checkBox.Padding.Vertical;
			empty.Width += checkBox.Padding.Horizontal + 15;
			if (empty.Height == checkBox.Padding.Vertical)
			{
				empty.Height += 14;
			}
			return empty;
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x000550D4 File Offset: 0x000532D4
		public override void DrawCheckBox(Graphics dc, Rectangle clip_area, CheckBox checkbox)
		{
			int num = 13;
			int num2 = 4;
			Rectangle clientRectangle = checkbox.ClientRectangle;
			Rectangle rectangle = clientRectangle;
			Rectangle rectangle2 = new Rectangle(rectangle.X, rectangle.Y, num, num);
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = StringAlignment.Near;
			stringFormat.LineAlignment = StringAlignment.Center;
			if (checkbox.ShowKeyboardCuesInternal)
			{
				stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
			}
			else
			{
				stringFormat.HotkeyPrefix = HotkeyPrefix.Hide;
			}
			ContentAlignment contentAlignment;
			if (checkbox.appearance != Appearance.Button)
			{
				contentAlignment = checkbox.check_alignment;
				if (contentAlignment <= ContentAlignment.MiddleCenter)
				{
					switch (contentAlignment)
					{
					case ContentAlignment.TopLeft:
						rectangle2.X = clientRectangle.Left;
						rectangle.X = clientRectangle.X + num + num2;
						rectangle.Width = clientRectangle.Width - num - num2;
						goto IL_03D6;
					case ContentAlignment.TopCenter:
						rectangle2.X = (clientRectangle.Right - clientRectangle.Left) / 2 - num / 2;
						rectangle2.Y = clientRectangle.Top;
						rectangle.X = clientRectangle.X;
						rectangle.Width = clientRectangle.Width;
						rectangle.Y = num + num2;
						rectangle.Height = clientRectangle.Height - num - num2;
						goto IL_03D6;
					case (ContentAlignment)3:
						break;
					case ContentAlignment.TopRight:
						rectangle2.X = clientRectangle.Right - num;
						rectangle.X = clientRectangle.X;
						rectangle.Width = clientRectangle.Width - num - num2;
						goto IL_03D6;
					default:
						if (contentAlignment != ContentAlignment.MiddleLeft)
						{
							if (contentAlignment == ContentAlignment.MiddleCenter)
							{
								rectangle2.X = (clientRectangle.Right - clientRectangle.Left) / 2 - num / 2;
								rectangle2.Y = (clientRectangle.Bottom - clientRectangle.Top) / 2 - num / 2;
								rectangle.X = clientRectangle.X;
								rectangle.Width = clientRectangle.Width;
								goto IL_03D6;
							}
						}
						break;
					}
				}
				else if (contentAlignment <= ContentAlignment.BottomLeft)
				{
					if (contentAlignment == ContentAlignment.MiddleRight)
					{
						rectangle2.X = clientRectangle.Right - num;
						rectangle2.Y = (clientRectangle.Bottom - clientRectangle.Top) / 2 - num / 2;
						rectangle.X = clientRectangle.X;
						rectangle.Width = clientRectangle.Width - num - num2;
						goto IL_03D6;
					}
					if (contentAlignment == ContentAlignment.BottomLeft)
					{
						rectangle2.X = clientRectangle.Left;
						rectangle2.Y = clientRectangle.Bottom - num;
						rectangle.X = clientRectangle.X + num + num2;
						rectangle.Width = clientRectangle.Width - num - num2;
						goto IL_03D6;
					}
				}
				else
				{
					if (contentAlignment == ContentAlignment.BottomCenter)
					{
						rectangle2.X = (clientRectangle.Right - clientRectangle.Left) / 2 - num / 2;
						rectangle2.Y = clientRectangle.Bottom - num;
						rectangle.X = clientRectangle.X;
						rectangle.Width = clientRectangle.Width;
						rectangle.Height = clientRectangle.Height - rectangle2.Y - num2;
						goto IL_03D6;
					}
					if (contentAlignment == ContentAlignment.BottomRight)
					{
						rectangle2.X = clientRectangle.Right - num;
						rectangle2.Y = clientRectangle.Bottom - num;
						rectangle.X = clientRectangle.X;
						rectangle.Width = clientRectangle.Width - num - num2;
						goto IL_03D6;
					}
				}
				rectangle2.X = clientRectangle.Left;
				rectangle2.Y = (clientRectangle.Bottom - clientRectangle.Top) / 2 - num / 2;
				rectangle.X = clientRectangle.X + num + num2;
				rectangle.Width = clientRectangle.Width - num - num2;
			}
			else
			{
				rectangle.X = clientRectangle.X;
				rectangle.Width = clientRectangle.Width;
			}
			IL_03D6:
			contentAlignment = checkbox.text_alignment;
			if (contentAlignment <= ContentAlignment.MiddleCenter)
			{
				switch (contentAlignment)
				{
				case ContentAlignment.TopLeft:
					break;
				case ContentAlignment.TopCenter:
					goto IL_0442;
				case (ContentAlignment)3:
					goto IL_0452;
				case ContentAlignment.TopRight:
					goto IL_044B;
				default:
					if (contentAlignment != ContentAlignment.MiddleLeft)
					{
						if (contentAlignment != ContentAlignment.MiddleCenter)
						{
							goto IL_0452;
						}
						goto IL_0442;
					}
					break;
				}
			}
			else if (contentAlignment <= ContentAlignment.BottomLeft)
			{
				if (contentAlignment == ContentAlignment.MiddleRight)
				{
					goto IL_044B;
				}
				if (contentAlignment != ContentAlignment.BottomLeft)
				{
					goto IL_0452;
				}
			}
			else
			{
				if (contentAlignment == ContentAlignment.BottomCenter)
				{
					goto IL_0442;
				}
				if (contentAlignment != ContentAlignment.BottomRight)
				{
					goto IL_0452;
				}
				goto IL_044B;
			}
			stringFormat.Alignment = StringAlignment.Near;
			goto IL_0452;
			IL_0442:
			stringFormat.Alignment = StringAlignment.Center;
			goto IL_0452;
			IL_044B:
			stringFormat.Alignment = StringAlignment.Far;
			IL_0452:
			contentAlignment = checkbox.text_alignment;
			if (contentAlignment > ContentAlignment.MiddleCenter)
			{
				if (contentAlignment <= ContentAlignment.BottomLeft)
				{
					if (contentAlignment == ContentAlignment.MiddleRight)
					{
						goto IL_04C1;
					}
					if (contentAlignment != ContentAlignment.BottomLeft)
					{
						goto IL_04C8;
					}
				}
				else if (contentAlignment != ContentAlignment.BottomCenter && contentAlignment != ContentAlignment.BottomRight)
				{
					goto IL_04C8;
				}
				stringFormat.LineAlignment = StringAlignment.Far;
				goto IL_04C8;
			}
			if (contentAlignment <= ContentAlignment.TopRight)
			{
				if (contentAlignment - ContentAlignment.TopLeft > 1 && contentAlignment != ContentAlignment.TopRight)
				{
					goto IL_04C8;
				}
				stringFormat.LineAlignment = StringAlignment.Near;
				goto IL_04C8;
			}
			else if (contentAlignment != ContentAlignment.MiddleLeft && contentAlignment != ContentAlignment.MiddleCenter)
			{
				goto IL_04C8;
			}
			IL_04C1:
			stringFormat.LineAlignment = StringAlignment.Center;
			IL_04C8:
			ButtonState buttonState = ButtonState.Normal;
			if (checkbox.FlatStyle == FlatStyle.Flat)
			{
				buttonState |= ButtonState.Flat;
			}
			if (checkbox.Checked)
			{
				buttonState |= ButtonState.Checked;
			}
			if (checkbox.ThreeState && checkbox.CheckState == CheckState.Indeterminate)
			{
				buttonState |= ButtonState.Checked;
				buttonState |= ButtonState.Pushed;
			}
			if (!checkbox.Enabled)
			{
				buttonState |= ButtonState.Inactive;
			}
			else if (checkbox.is_pressed)
			{
				buttonState |= ButtonState.Pushed;
			}
			this.CheckBox_DrawCheckBox(dc, checkbox, buttonState, rectangle2);
			if (checkbox.image != null || checkbox.image_list != null)
			{
				this.ButtonBase_DrawImage(checkbox, dc);
			}
			this.CheckBox_DrawText(checkbox, rectangle, dc, stringFormat);
			if (checkbox.Focused && checkbox.Enabled && checkbox.appearance != Appearance.Button && checkbox.Text != string.Empty && checkbox.ShowFocusCues)
			{
				SizeF sizeF = dc.MeasureString(checkbox.Text, checkbox.Font);
				Rectangle empty = Rectangle.Empty;
				empty.X = rectangle.X;
				empty.Y = (int)(((float)rectangle.Height - sizeF.Height) / 2f);
				empty.Size = sizeF.ToSize();
				this.CheckBox_DrawFocus(checkbox, dc, empty);
			}
			stringFormat.Dispose();
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x000556E8 File Offset: 0x000538E8
		protected virtual void CheckBox_DrawCheckBox(Graphics dc, CheckBox checkbox, ButtonState state, Rectangle checkbox_rectangle)
		{
			Brush brush = ((checkbox.BackColor.ToArgb() == this.ColorControl.ToArgb()) ? SystemBrushes.Control : this.ResPool.GetSolidBrush(checkbox.BackColor));
			dc.FillRectangle(brush, checkbox.ClientRectangle);
			if (checkbox.appearance == Appearance.Button)
			{
				this.ButtonBase_DrawButton(checkbox, dc);
				if (checkbox.Focused && checkbox.Enabled)
				{
					this.ButtonBase_DrawFocus(checkbox, dc);
					return;
				}
			}
			else
			{
				if (checkbox.FlatStyle == FlatStyle.Flat || checkbox.FlatStyle == FlatStyle.Popup)
				{
					this.DrawFlatStyleCheckBox(dc, checkbox_rectangle, checkbox);
					return;
				}
				this.CPDrawCheckBox(dc, checkbox_rectangle, state);
			}
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00055789 File Offset: 0x00053989
		protected virtual void CheckBox_DrawText(CheckBox checkbox, Rectangle text_rectangle, Graphics dc, StringFormat text_format)
		{
			this.DrawCheckBox_and_RadioButtonText(checkbox, text_rectangle, dc, text_format, checkbox.Appearance, checkbox.Checked);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000557A2 File Offset: 0x000539A2
		protected virtual void CheckBox_DrawFocus(CheckBox checkbox, Graphics dc, Rectangle text_rectangle)
		{
			this.DrawInnerFocusRectangle(dc, text_rectangle, checkbox.BackColor);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000557B4 File Offset: 0x000539B4
		protected virtual void DrawFlatStyleCheckBox(Graphics graphics, Rectangle rectangle, CheckBox checkbox)
		{
			Rectangle rectangle2;
			Rectangle rectangle3;
			if (checkbox.FlatStyle == FlatStyle.Popup && checkbox.is_entered)
			{
				rectangle2 = new Rectangle(rectangle.X, rectangle.Y, Math.Max(rectangle.Width - 1, 0), Math.Max(rectangle.Height - 1, 0));
				rectangle3 = new Rectangle(rectangle2.X + 1, rectangle2.Y + 1, Math.Max(rectangle2.Width - 3, 0), Math.Max(rectangle2.Height - 3, 0));
			}
			else
			{
				rectangle2 = new Rectangle(rectangle.X, rectangle.Y, Math.Max(rectangle.Width - 2, 0), Math.Max(rectangle.Height - 2, 0));
				rectangle3 = new Rectangle(rectangle2.X + 1, rectangle2.Y + 1, Math.Max(rectangle2.Width - 2, 0), Math.Max(rectangle2.Height - 2, 0));
			}
			if (checkbox.Enabled)
			{
				if (checkbox.is_entered || checkbox.Capture)
				{
					if (checkbox.FlatStyle == FlatStyle.Popup && checkbox.is_entered && checkbox.Capture)
					{
						graphics.FillRectangle(this.ResPool.GetSolidBrush(checkbox.BackColor), rectangle3);
					}
					else if (checkbox.FlatStyle == FlatStyle.Flat)
					{
						if (!checkbox.is_pressed)
						{
							graphics.FillRectangle(this.ResPool.GetSolidBrush(checkbox.BackColor), rectangle3);
						}
						else
						{
							graphics.FillRectangle(this.ResPool.GetSolidBrush(ControlPaint.LightLight(checkbox.BackColor)), rectangle3);
						}
					}
					else
					{
						graphics.FillRectangle(this.ResPool.GetSolidBrush(ControlPaint.LightLight(checkbox.BackColor)), rectangle3);
					}
					if (checkbox.FlatStyle == FlatStyle.Flat)
					{
						ControlPaint.DrawBorder(graphics, rectangle2, checkbox.ForeColor, ButtonBorderStyle.Solid);
					}
					else
					{
						this.CPDrawBorder3D(graphics, rectangle2, Border3DStyle.SunkenInner, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom, checkbox.BackColor);
					}
				}
				else
				{
					graphics.FillRectangle(this.ResPool.GetSolidBrush(ControlPaint.LightLight(checkbox.BackColor)), rectangle3);
					if (checkbox.FlatStyle == FlatStyle.Flat)
					{
						ControlPaint.DrawBorder(graphics, rectangle2, checkbox.ForeColor, ButtonBorderStyle.Solid);
					}
					else
					{
						ControlPaint.DrawBorder(graphics, rectangle2, ControlPaint.DarkDark(checkbox.BackColor), ButtonBorderStyle.Solid);
					}
				}
			}
			else
			{
				if (checkbox.FlatStyle == FlatStyle.Popup)
				{
					graphics.FillRectangle(SystemBrushes.Control, rectangle3);
				}
				ControlPaint.DrawBorder(graphics, rectangle2, this.ColorControlDark, ButtonBorderStyle.Solid);
			}
			if (checkbox.Checked)
			{
				int num = Math.Max(3, rectangle3.Width / 3);
				int num2 = Math.Max(1, rectangle3.Width / 9);
				Rectangle rectangle4 = new Rectangle(rectangle3.X, rectangle3.Y + 1, rectangle3.Width, rectangle3.Height);
				Pen pen;
				if (checkbox.Enabled)
				{
					pen = this.ResPool.GetPen(checkbox.ForeColor);
				}
				else
				{
					pen = SystemPens.ControlDark;
				}
				for (int i = 0; i < num; i++)
				{
					graphics.DrawLine(pen, rectangle4.Left + num / 2, rectangle4.Top + num + i, rectangle4.Left + num / 2 + 2 * num2, rectangle4.Top + num + 2 * num2 + i);
					graphics.DrawLine(pen, rectangle4.Left + num / 2 + 2 * num2, rectangle4.Top + num + 2 * num2 + i, rectangle4.Left + num / 2 + 6 * num2, rectangle4.Top + num - 2 * num2 + i);
				}
			}
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00055B18 File Offset: 0x00053D18
		private void DrawCheckBox_and_RadioButtonText(ButtonBase button_base, Rectangle text_rectangle, Graphics dc, StringFormat text_format, Appearance appearance, bool ischecked)
		{
			if (appearance == Appearance.Button)
			{
				if (ischecked || (button_base.Capture && button_base.FlatStyle != FlatStyle.Flat))
				{
					int num = text_rectangle.X;
					text_rectangle.X = num + 1;
					num = text_rectangle.Y;
					text_rectangle.Y = num + 1;
				}
				text_rectangle.Inflate(-4, -4);
			}
			if ((float)button_base.Font.Height * 1.5f > (float)text_rectangle.Height)
			{
				text_format.FormatFlags |= StringFormatFlags.NoWrap;
			}
			if (button_base.Enabled)
			{
				dc.DrawString(button_base.Text, button_base.Font, this.ResPool.GetSolidBrush(button_base.ForeColor), text_rectangle, text_format);
				return;
			}
			if (button_base.FlatStyle == FlatStyle.Flat || button_base.FlatStyle == FlatStyle.Popup)
			{
				dc.DrawString(button_base.Text, button_base.Font, SystemBrushes.ControlDarkDark, text_rectangle, text_format);
				return;
			}
			this.CPDrawStringDisabled(dc, button_base.Text, button_base.Font, button_base.BackColor, text_rectangle, text_format);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00055C24 File Offset: 0x00053E24
		public override void DrawComboBoxItem(ComboBox ctrl, DrawItemEventArgs e)
		{
			Rectangle bounds = e.Bounds;
			StringFormat stringFormat = new StringFormat();
			stringFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit;
			Color color;
			Color color2;
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				color = this.ColorHighlight;
				color2 = this.ColorHighlightText;
			}
			else
			{
				color = e.BackColor;
				color2 = e.ForeColor;
			}
			if (!ctrl.Enabled)
			{
				color2 = this.ColorInactiveCaptionText;
			}
			e.Graphics.FillRectangle(this.ResPool.GetSolidBrush(color), e.Bounds);
			if (e.Index != -1)
			{
				e.Graphics.DrawString(ctrl.GetItemText(ctrl.Items[e.Index]), e.Font, this.ResPool.GetSolidBrush(color2), bounds, stringFormat);
			}
			if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
			{
				this.CPDrawFocusRectangle(e.Graphics, e.Bounds, color2, color);
			}
			stringFormat.Dispose();
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00055D0C File Offset: 0x00053F0C
		public override void DrawFlatStyleComboButton(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			Point[] array = new Point[3];
			Rectangle rectangle2 = new Rectangle(rectangle.X + rectangle.Width / 4, rectangle.Y + rectangle.Height / 4, rectangle.Width / 2, rectangle.Height / 2);
			int num = rectangle2.Left + rectangle2.Width / 2;
			int num2 = rectangle2.Top + rectangle2.Height / 2;
			int num3 = Math.Max(1, rectangle2.Width / 8);
			int num4 = Math.Max(1, rectangle2.Height / 8);
			if ((state & ButtonState.Pushed) != ButtonState.Normal)
			{
				num3++;
				num4++;
			}
			rectangle2.Y -= num4;
			num2 -= num4;
			Point point = new Point(rectangle2.Left + 1, num2);
			Point point2 = new Point(rectangle2.Right - 1, num2);
			Point point3 = new Point(num, rectangle2.Bottom - 1);
			array[0] = point;
			array[1] = point2;
			array[2] = point3;
			if ((state & ButtonState.Inactive) != ButtonState.Normal)
			{
				Point[] array2 = array;
				int num5 = 0;
				array2[num5].X = array2[num5].X + 1;
				Point[] array3 = array;
				int num6 = 0;
				array3[num6].Y = array3[num6].Y + 1;
				Point[] array4 = array;
				int num7 = 1;
				array4[num7].X = array4[num7].X + 1;
				Point[] array5 = array;
				int num8 = 1;
				array5[num8].Y = array5[num8].Y + 1;
				Point[] array6 = array;
				int num9 = 2;
				array6[num9].X = array6[num9].X + 1;
				Point[] array7 = array;
				int num10 = 2;
				array7[num10].Y = array7[num10].Y + 1;
				graphics.FillPolygon(SystemBrushes.ControlLightLight, array, FillMode.Winding);
				array[0] = point;
				array[1] = point2;
				array[2] = point3;
				graphics.FillPolygon(SystemBrushes.ControlDark, array, FillMode.Winding);
				return;
			}
			graphics.FillPolygon(SystemBrushes.ControlText, array, FillMode.Winding);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00055EE1 File Offset: 0x000540E1
		public override void ComboBoxDrawNormalDropDownButton(ComboBox comboBox, Graphics g, Rectangle clippingArea, Rectangle area, ButtonState state)
		{
			this.CPDrawComboButton(g, area, state);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00006F54 File Offset: 0x00005154
		public override bool ComboBoxNormalDropDownButtonHasTransparentBackground(ComboBox comboBox, ButtonState state)
		{
			return true;
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00002D70 File Offset: 0x00000F70
		public override bool ComboBoxDropDownButtonHasHotElementStyle(ComboBox comboBox)
		{
			return false;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00055EF0 File Offset: 0x000540F0
		public override void ComboBoxDrawBackground(ComboBox comboBox, Graphics g, Rectangle clippingArea, FlatStyle style)
		{
			if (!comboBox.Enabled)
			{
				g.FillRectangle(this.ResPool.GetSolidBrush(this.ColorControl), comboBox.ClientRectangle);
			}
			if (comboBox.DropDownStyle == ComboBoxStyle.Simple)
			{
				g.FillRectangle(this.ResPool.GetSolidBrush(comboBox.Parent.BackColor), comboBox.ClientRectangle);
			}
			if (style == FlatStyle.Popup && (comboBox.Entered || comboBox.Focused))
			{
				Rectangle textArea = comboBox.TextArea;
				textArea.Height--;
				textArea.Width--;
				g.DrawRectangle(this.ResPool.GetPen(SystemColors.ControlDark), textArea);
				g.DrawLine(this.ResPool.GetPen(SystemColors.ControlDark), comboBox.ButtonArea.X - 1, comboBox.ButtonArea.Top, comboBox.ButtonArea.X - 1, comboBox.ButtonArea.Bottom);
			}
			if (style != FlatStyle.Flat && style != FlatStyle.Popup && clippingArea.IntersectsWith(comboBox.TextArea))
			{
				ControlPaint.DrawBorder3D(g, comboBox.TextArea, Border3DStyle.Sunken);
			}
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00002D70 File Offset: 0x00000F70
		public override bool CombBoxBackgroundHasHotElementStyle(ComboBox comboBox)
		{
			return false;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00056020 File Offset: 0x00054220
		public override void DrawGroupBox(Graphics dc, Rectangle area, GroupBox box)
		{
			dc.FillRectangle(this.GetControlBackBrush(box.BackColor), box.ClientRectangle);
			StringFormat stringFormat = new StringFormat();
			stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
			SizeF sizeF = dc.MeasureString(box.Text, box.Font);
			int num = 0;
			if (sizeF.Width > 0f)
			{
				num = (int)sizeF.Width + 7;
				if (num > box.Width - 16)
				{
					num = box.Width - 16;
				}
			}
			int num2 = box.Font.Height / 2;
			Region clip = dc.Clip;
			dc.SetClip(new Rectangle(10, 0, num, box.Font.Height), CombineMode.Exclude);
			this.CPDrawBorder3D(dc, new Rectangle(0, num2, box.Width, box.Height - num2), Border3DStyle.Etched, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom, box.BackColor);
			dc.Clip = clip;
			if (box.Text.Length != 0)
			{
				if (box.Enabled)
				{
					dc.DrawString(box.Text, box.Font, this.ResPool.GetSolidBrush(box.ForeColor), 10f, 0f, stringFormat);
				}
				else
				{
					this.CPDrawStringDisabled(dc, box.Text, box.Font, box.BackColor, new RectangleF(10f, 0f, (float)num, (float)box.Font.Height), stringFormat);
				}
			}
			stringFormat.Dispose();
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x00056176 File Offset: 0x00054376
		public override Size GroupBoxDefaultSize
		{
			get
			{
				return new Size(200, 100);
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x00056184 File Offset: 0x00054384
		public override Size HScrollBarDefaultSize
		{
			get
			{
				return new Size(80, this.ScrollBarButtonSize);
			}
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00056194 File Offset: 0x00054394
		public override void DrawListViewItems(Graphics dc, Rectangle clip, ListView control)
		{
			bool flag = control.View == View.Details;
			int firstVisibleIndex = control.FirstVisibleIndex;
			int lastVisibleIndex = control.LastVisibleIndex;
			if (control.VirtualMode)
			{
				control.OnCacheVirtualItems(new CacheVirtualItemsEventArgs(firstVisibleIndex, lastVisibleIndex));
			}
			for (int i = firstVisibleIndex; i <= lastVisibleIndex; i++)
			{
				ListViewItem itemAtDisplayIndex = control.GetItemAtDisplayIndex(i);
				if (clip.IntersectsWith(itemAtDisplayIndex.Bounds))
				{
					bool flag2 = false;
					if (control.OwnerDraw)
					{
						flag2 = this.DrawListViewItemOwnerDraw(dc, itemAtDisplayIndex, i);
					}
					if (!flag2)
					{
						this.DrawListViewItem(dc, control, itemAtDisplayIndex);
						if (control.View == View.Details)
						{
							this.DrawListViewSubItems(dc, control, itemAtDisplayIndex);
						}
					}
				}
			}
			if (control.UsingGroups)
			{
				for (int j = 0; j < control.Groups.InternalCount; j++)
				{
					ListViewGroup internalGroup = control.Groups.GetInternalGroup(j);
					if (internalGroup.ItemCount > 0 && clip.IntersectsWith(internalGroup.HeaderBounds))
					{
						this.DrawListViewGroupHeader(dc, control, internalGroup);
					}
				}
			}
			ListViewInsertionMark insertionMark = control.InsertionMark;
			int index = insertionMark.Index;
			if (Application.VisualStylesEnabled && insertionMark.Bounds != Rectangle.Empty && control.View != View.Details && control.View != View.List && index > -1 && index < control.Items.Count)
			{
				Brush solidBrush = this.ResPool.GetSolidBrush(insertionMark.Color);
				dc.FillRectangle(solidBrush, insertionMark.Line);
				dc.FillPolygon(solidBrush, insertionMark.TopTriangle);
				dc.FillPolygon(solidBrush, insertionMark.BottomTriangle);
			}
			if (flag && control.GridLines && !control.UsingGroups)
			{
				Size clientSize = control.ClientSize;
				int num = ((control.HeaderStyle == ColumnHeaderStyle.None) ? 0 : control.header_control.Height);
				foreach (object obj in control.Columns)
				{
					int num2 = ((ColumnHeader)obj).Rect.Right - control.h_marker;
					dc.DrawLine(SystemPens.Control, num2, num, num2, clientSize.Height);
				}
				int num3 = control.ItemSize.Height;
				if (num3 == 0)
				{
					num3 = control.Font.Height + 2;
				}
				for (int k = num + num3 - control.v_marker % num3; k < clientSize.Height; k += num3)
				{
					dc.DrawLine(SystemPens.Control, 0, k, clientSize.Width, k);
				}
			}
			if (control.h_scroll.Visible && control.v_scroll.Visible)
			{
				Rectangle rectangle = default(Rectangle);
				rectangle.X = control.h_scroll.Location.X + control.h_scroll.Width;
				rectangle.Width = control.v_scroll.Width;
				rectangle.Y = control.v_scroll.Location.Y + control.v_scroll.Height;
				rectangle.Height = control.h_scroll.Height;
				dc.FillRectangle(SystemBrushes.Control, rectangle);
			}
			Rectangle boxSelectRectangle = control.item_control.BoxSelectRectangle;
			if (!boxSelectRectangle.Size.IsEmpty)
			{
				dc.DrawRectangle(this.ResPool.GetDashPen(this.ColorControlText, DashStyle.Dot), boxSelectRectangle);
			}
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00056508 File Offset: 0x00054708
		public override void DrawListViewHeader(Graphics dc, Rectangle clip, ListView control)
		{
			if (control.View == View.Details && control.HeaderStyle != ColumnHeaderStyle.None)
			{
				dc.FillRectangle(SystemBrushes.Control, 0, 0, control.TotalWidth, control.Font.Height + 5);
				if (control.Columns.Count > 0)
				{
					foreach (object obj in control.Columns)
					{
						ColumnHeader columnHeader = (ColumnHeader)obj;
						Rectangle rect = columnHeader.Rect;
						rect.X -= control.h_marker;
						bool flag = false;
						if (control.OwnerDraw)
						{
							flag = this.DrawListViewColumnHeaderOwnerDraw(dc, control, columnHeader, rect);
						}
						if (!flag)
						{
							this.ListViewDrawColumnHeaderBackground(control, columnHeader, dc, rect, clip);
							rect.X += 5;
							rect.Width -= 10;
							if (rect.Width > 0)
							{
								int num;
								if (control.SmallImageList == null)
								{
									num = -1;
								}
								else
								{
									num = ((columnHeader.ImageKey == string.Empty) ? columnHeader.ImageIndex : control.SmallImageList.Images.IndexOfKey(columnHeader.ImageKey));
								}
								if (num > -1 && num < control.SmallImageList.Images.Count)
								{
									int num2 = control.SmallImageList.ImageSize.Width + 5;
									int num3 = (int)dc.MeasureString(columnHeader.Text, control.Font).Width;
									int num4 = rect.X;
									int num5 = rect.Y + (rect.Height - control.SmallImageList.ImageSize.Height) / 2;
									switch (columnHeader.TextAlign)
									{
									case HorizontalAlignment.Right:
										num4 = rect.Right - (num3 + num2);
										break;
									case HorizontalAlignment.Center:
										num4 = (rect.Width - (num3 + num2)) / 2 + rect.X;
										break;
									}
									if (num4 < rect.X)
									{
										num4 = rect.X;
									}
									control.SmallImageList.Draw(dc, new Point(num4, num5), num);
									rect.X += num2;
									rect.Width -= num2;
								}
								dc.DrawString(columnHeader.Text, control.Font, SystemBrushes.ControlText, rect, columnHeader.Format);
							}
						}
					}
					int num6 = control.GetReorderedColumn(control.Columns.Count - 1).Rect.Right - control.h_marker;
					if (num6 < control.Right)
					{
						Rectangle rect2 = control.Columns[0].Rect;
						rect2.X = num6;
						rect2.Width = control.Right - num6;
						this.ListViewDrawUnusedHeaderBackground(control, dc, rect2, clip);
					}
				}
			}
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0005680C File Offset: 0x00054A0C
		protected virtual void ListViewDrawColumnHeaderBackground(ListView listView, ColumnHeader columnHeader, Graphics g, Rectangle area, Rectangle clippingArea)
		{
			ButtonState buttonState;
			if (listView.HeaderStyle == ColumnHeaderStyle.Clickable)
			{
				buttonState = (columnHeader.Pressed ? ButtonState.Pushed : ButtonState.Normal);
			}
			else
			{
				buttonState = ButtonState.Flat;
			}
			this.CPDrawButton(g, area, buttonState);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00056848 File Offset: 0x00054A48
		protected virtual void ListViewDrawUnusedHeaderBackground(ListView listView, Graphics g, Rectangle area, Rectangle clippingArea)
		{
			ButtonState buttonState;
			if (listView.HeaderStyle == ColumnHeaderStyle.Clickable)
			{
				buttonState = ButtonState.Normal;
			}
			else
			{
				buttonState = ButtonState.Flat;
			}
			this.CPDrawButton(g, area, buttonState);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00056874 File Offset: 0x00054A74
		public override void DrawListViewHeaderDragDetails(Graphics dc, ListView view, ColumnHeader col, int target_x)
		{
			Rectangle rect = col.Rect;
			rect.X -= view.h_marker;
			Color color = Color.FromArgb(127, (int)this.ColorControlDark.R, (int)this.ColorControlDark.G, (int)this.ColorControlDark.B);
			dc.FillRectangle(this.ResPool.GetSolidBrush(color), rect);
			rect.X += 3;
			rect.Width -= 8;
			if (rect.Width <= 0)
			{
				return;
			}
			color = Color.FromArgb(127, (int)this.ColorControlText.R, (int)this.ColorControlText.G, (int)this.ColorControlText.B);
			dc.DrawString(col.Text, view.Font, this.ResPool.GetSolidBrush(color), rect, col.Format);
			dc.DrawLine(this.ResPool.GetSizedPen(this.ColorHighlight, 2), target_x, 0, target_x, col.Rect.Height);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00056994 File Offset: 0x00054B94
		protected virtual bool DrawListViewColumnHeaderOwnerDraw(Graphics dc, ListView control, ColumnHeader column, Rectangle bounds)
		{
			ListViewItemStates listViewItemStates = ListViewItemStates.ShowKeyboardCues;
			if (column.Pressed)
			{
				listViewItemStates |= ListViewItemStates.Selected;
			}
			DrawListViewColumnHeaderEventArgs drawListViewColumnHeaderEventArgs = new DrawListViewColumnHeaderEventArgs(dc, bounds, column.Index, column, listViewItemStates, SystemColors.ControlText, ThemeEngine.Current.ColorControl, this.DefaultFont);
			control.OnDrawColumnHeader(drawListViewColumnHeaderEventArgs);
			return !drawListViewColumnHeaderEventArgs.DrawDefault;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x000569EC File Offset: 0x00054BEC
		protected virtual bool DrawListViewItemOwnerDraw(Graphics dc, ListViewItem item, int index)
		{
			ListViewItemStates listViewItemStates = ListViewItemStates.ShowKeyboardCues;
			if (item.Selected)
			{
				listViewItemStates |= ListViewItemStates.Selected;
			}
			if (item.Focused)
			{
				listViewItemStates |= ListViewItemStates.Focused;
			}
			DrawListViewItemEventArgs drawListViewItemEventArgs = new DrawListViewItemEventArgs(dc, item, item.Bounds, index, listViewItemStates);
			item.ListView.OnDrawItem(drawListViewItemEventArgs);
			if (drawListViewItemEventArgs.DrawDefault)
			{
				return false;
			}
			if (item.ListView.View == View.Details)
			{
				int num = Math.Min(item.ListView.Columns.Count, item.SubItems.Count);
				for (int i = 0; i < num; i++)
				{
					if (!this.DrawListViewSubItemOwnerDraw(dc, item, listViewItemStates, i))
					{
						if (i == 0)
						{
							this.DrawListViewItem(dc, item.ListView, item);
						}
						else
						{
							this.DrawListViewSubItem(dc, item.ListView, item, i);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00056AA8 File Offset: 0x00054CA8
		protected virtual void DrawListViewItem(Graphics dc, ListView control, ListViewItem item)
		{
			Rectangle checkRectReal = item.CheckRectReal;
			Rectangle bounds = item.GetBounds(ItemBoundsPortion.Icon);
			Rectangle bounds2 = item.GetBounds(ItemBoundsPortion.Entire);
			Rectangle bounds3 = item.GetBounds(ItemBoundsPortion.Label);
			if (control.CheckBoxes && control.View != View.Tile)
			{
				if (control.StateImageList == null)
				{
					int num = Math.Max(3, checkRectReal.Width / 6);
					int num2 = Math.Max(1, checkRectReal.Width / 12);
					dc.FillRectangle(SystemBrushes.Window, checkRectReal);
					Rectangle rectangle = new Rectangle(checkRectReal.X + 2, checkRectReal.Y + 2, checkRectReal.Width - 4, checkRectReal.Height - 4);
					Pen sizedPen = this.ResPool.GetSizedPen(this.ColorWindowText, 2);
					dc.DrawRectangle(sizedPen, rectangle);
					if (item.Checked)
					{
						Pen sizedPen2 = this.ResPool.GetSizedPen(this.ColorWindowText, 1);
						int num3 = rectangle.X;
						rectangle.X = num3 + 1;
						num3 = rectangle.Y;
						rectangle.Y = num3 + 1;
						int num4 = rectangle.Width / 5;
						int num5 = rectangle.Height / 3;
						for (int i = 0; i < num; i++)
						{
							dc.DrawLine(sizedPen2, rectangle.Left + num4, rectangle.Top + num5 + i, rectangle.Left + num4 + 2 * num2, rectangle.Top + num5 + 2 * num2 + i);
							dc.DrawLine(sizedPen2, rectangle.Left + num4 + 2 * num2, rectangle.Top + num5 + 2 * num2 + i, rectangle.Left + num4 + 6 * num2, rectangle.Top + num5 - 2 * num2 + i);
						}
					}
				}
				else
				{
					int num6;
					if (item.Checked)
					{
						num6 = ((control.StateImageList.Images.Count > 1) ? 1 : (-1));
					}
					else
					{
						num6 = ((control.StateImageList.Images.Count > 0) ? 0 : (-1));
					}
					if (num6 > -1)
					{
						control.StateImageList.Draw(dc, checkRectReal.Location, num6);
					}
				}
			}
			ImageList imageList = ((control.View == View.LargeIcon || control.View == View.Tile) ? control.LargeImageList : control.SmallImageList);
			if (imageList != null)
			{
				int num7;
				if (item.ImageKey != string.Empty)
				{
					num7 = imageList.Images.IndexOfKey(item.ImageKey);
				}
				else
				{
					num7 = item.ImageIndex;
				}
				if (num7 > -1 && num7 < imageList.Images.Count)
				{
					FileViewListViewItem fileViewListViewItem = item as FileViewListViewItem;
					if (fileViewListViewItem != null && fileViewListViewItem.FSEntry != null && fileViewListViewItem.FSEntry.Image != null)
					{
						dc.DrawImage(fileViewListViewItem.FSEntry.Image, bounds);
					}
					else
					{
						imageList.Draw(dc, bounds.Location, num7);
					}
				}
			}
			StringFormat stringFormat = new StringFormat();
			if (control.View == View.SmallIcon || control.View == View.LargeIcon)
			{
				stringFormat.LineAlignment = StringAlignment.Near;
			}
			else
			{
				stringFormat.LineAlignment = StringAlignment.Center;
			}
			if (control.View == View.LargeIcon)
			{
				stringFormat.Alignment = StringAlignment.Center;
			}
			else
			{
				stringFormat.Alignment = StringAlignment.Near;
			}
			if (control.LabelWrap && control.View != View.Details && control.View != View.Tile)
			{
				stringFormat.FormatFlags = StringFormatFlags.LineLimit;
			}
			else
			{
				stringFormat.FormatFlags = StringFormatFlags.NoWrap;
			}
			if ((control.View == View.LargeIcon && !item.Focused) || control.View == View.Details || control.View == View.Tile)
			{
				stringFormat.Trimming = StringTrimming.EllipsisCharacter;
			}
			Rectangle rectangle2 = bounds3;
			if (control.View == View.Details)
			{
				Size size = Size.Ceiling(dc.MeasureString(item.Text, item.Font));
				if (!control.FullRowSelect)
				{
					rectangle2.Width = Math.Min(size.Width + 4, bounds3.Width);
				}
			}
			if (item.Selected && control.Focused)
			{
				dc.FillRectangle(SystemBrushes.Highlight, rectangle2);
			}
			else if (item.Selected && !control.HideSelection)
			{
				dc.FillRectangle(SystemBrushes.Control, rectangle2);
			}
			else
			{
				dc.FillRectangle(this.ResPool.GetSolidBrush(item.BackColor), bounds3);
			}
			Brush brush = ((!control.Enabled) ? SystemBrushes.ControlLight : ((item.Selected && control.Focused) ? SystemBrushes.HighlightText : this.ResPool.GetSolidBrush(item.ForeColor)));
			if (control.View == View.Tile && Application.VisualStylesEnabled)
			{
				dc.DrawString(item.Text, item.Font, brush, item.SubItems[0].Bounds, stringFormat);
				int num8 = Math.Min(control.Columns.Count, item.SubItems.Count);
				for (int j = 1; j < num8; j++)
				{
					ListViewItem.ListViewSubItem listViewSubItem = item.SubItems[j];
					if (listViewSubItem.Text != null && listViewSubItem.Text.Length != 0)
					{
						Brush brush2 = ((item.Selected && control.Focused) ? SystemBrushes.HighlightText : this.GetControlForeBrush(listViewSubItem.ForeColor));
						dc.DrawString(listViewSubItem.Text, listViewSubItem.Font, brush2, listViewSubItem.Bounds, stringFormat);
					}
				}
			}
			else if (item.Text != null && item.Text.Length > 0)
			{
				Font font = item.Font;
				if (control.HotTracking && item.Hot)
				{
					font = item.HotFont;
				}
				if (item.Selected && control.Focused)
				{
					dc.DrawString(item.Text, font, brush, rectangle2, stringFormat);
				}
				else
				{
					dc.DrawString(item.Text, font, brush, bounds3, stringFormat);
				}
			}
			if (item.Focused && control.Focused)
			{
				Rectangle rectangle3 = rectangle2;
				if (control.FullRowSelect && control.View == View.Details)
				{
					int num9 = 0;
					foreach (object obj in control.Columns)
					{
						ColumnHeader columnHeader = (ColumnHeader)obj;
						num9 += columnHeader.Width;
					}
					rectangle3 = new Rectangle(0, bounds2.Y, num9, bounds2.Height);
				}
				if (control.ShowFocusCues)
				{
					if (item.Selected)
					{
						this.CPDrawFocusRectangle(dc, rectangle3, this.ColorHighlightText, this.ColorHighlight);
					}
					else
					{
						this.CPDrawFocusRectangle(dc, rectangle3, control.ForeColor, control.BackColor);
					}
				}
			}
			stringFormat.Dispose();
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00057130 File Offset: 0x00055330
		protected virtual void DrawListViewSubItems(Graphics dc, ListView control, ListViewItem item)
		{
			int count = control.Columns.Count;
			int num = Math.Min(item.SubItems.Count, count);
			for (int i = 1; i < num; i++)
			{
				this.DrawListViewSubItem(dc, control, item, i);
			}
			Rectangle bounds = item.GetBounds(ItemBoundsPortion.Label);
			if (item.Selected && (control.Focused || !control.HideSelection) && control.FullRowSelect)
			{
				for (int j = num; j < count; j++)
				{
					ColumnHeader columnHeader = control.Columns[j];
					bounds.X = columnHeader.Rect.X - control.h_marker;
					bounds.Width = columnHeader.Wd;
					dc.FillRectangle(control.Focused ? SystemBrushes.Highlight : SystemBrushes.Control, bounds);
				}
			}
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00057200 File Offset: 0x00055400
		protected virtual void DrawListViewSubItem(Graphics dc, ListView control, ListViewItem item, int index)
		{
			ListViewItem.ListViewSubItem listViewSubItem = item.SubItems[index];
			ColumnHeader columnHeader = control.Columns[index];
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = columnHeader.Format.Alignment;
			stringFormat.LineAlignment = StringAlignment.Center;
			stringFormat.FormatFlags = StringFormatFlags.NoWrap;
			stringFormat.Trimming = StringTrimming.EllipsisCharacter;
			Rectangle bounds = listViewSubItem.Bounds;
			Rectangle rectangle = bounds;
			rectangle.X += 3;
			rectangle.Width -= this.ListViewItemPaddingWidth;
			SolidBrush solidBrush;
			SolidBrush solidBrush2;
			Font font;
			if (item.UseItemStyleForSubItems)
			{
				solidBrush = this.ResPool.GetSolidBrush(item.BackColor);
				solidBrush2 = this.ResPool.GetSolidBrush(item.ForeColor);
				if (control.HotTracking && item.Hot)
				{
					font = item.HotFont;
				}
				else
				{
					font = item.Font;
				}
			}
			else
			{
				solidBrush = this.ResPool.GetSolidBrush(listViewSubItem.BackColor);
				solidBrush2 = this.ResPool.GetSolidBrush(listViewSubItem.ForeColor);
				font = listViewSubItem.Font;
			}
			if (item.Selected && (control.Focused || !control.HideSelection) && control.FullRowSelect)
			{
				Brush brush;
				Brush brush2;
				if (control.Focused)
				{
					brush = SystemBrushes.Highlight;
					brush2 = SystemBrushes.HighlightText;
				}
				else
				{
					brush = SystemBrushes.Control;
					brush2 = solidBrush2;
				}
				dc.FillRectangle(brush, bounds);
				if (listViewSubItem.Text != null && listViewSubItem.Text.Length > 0)
				{
					dc.DrawString(listViewSubItem.Text, font, brush2, rectangle, stringFormat);
				}
			}
			else
			{
				dc.FillRectangle(solidBrush, bounds);
				if (listViewSubItem.Text != null && listViewSubItem.Text.Length > 0)
				{
					dc.DrawString(listViewSubItem.Text, font, solidBrush2, rectangle, stringFormat);
				}
			}
			stringFormat.Dispose();
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x000573C8 File Offset: 0x000555C8
		protected virtual bool DrawListViewSubItemOwnerDraw(Graphics dc, ListViewItem item, ListViewItemStates state, int index)
		{
			ListView listView = item.ListView;
			ListViewItem.ListViewSubItem listViewSubItem = item.SubItems[index];
			DrawListViewSubItemEventArgs drawListViewSubItemEventArgs = new DrawListViewSubItemEventArgs(dc, listViewSubItem.Bounds, item, listViewSubItem, item.Index, index, listView.Columns[index], state);
			listView.OnDrawSubItem(drawListViewSubItemEventArgs);
			return !drawListViewSubItemEventArgs.DrawDefault;
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00057420 File Offset: 0x00055620
		protected virtual void DrawListViewGroupHeader(Graphics dc, ListView control, ListViewGroup group)
		{
			Rectangle headerBounds = group.HeaderBounds;
			Rectangle headerBounds2 = group.HeaderBounds;
			headerBounds.Offset(8, 0);
			headerBounds.Inflate(-8, 0);
			int num = control.Font.Height + 2;
			Font font = new Font(control.Font, control.Font.Style | FontStyle.Bold);
			Brush brush = new LinearGradientBrush(new Point(headerBounds2.Left, 0), new Point(headerBounds2.Left + this.ListViewGroupLineWidth, 0), SystemColors.Desktop, Color.White);
			Pen pen = new Pen(brush);
			StringFormat stringFormat = new StringFormat();
			switch (group.HeaderAlignment)
			{
			case HorizontalAlignment.Left:
				stringFormat.Alignment = StringAlignment.Near;
				break;
			case HorizontalAlignment.Right:
				stringFormat.Alignment = StringAlignment.Far;
				break;
			case HorizontalAlignment.Center:
				stringFormat.Alignment = StringAlignment.Center;
				break;
			}
			stringFormat.LineAlignment = StringAlignment.Near;
			dc.DrawString(group.Header, font, SystemBrushes.ControlText, headerBounds, stringFormat);
			dc.DrawLine(pen, headerBounds2.Left, headerBounds2.Top + num, headerBounds2.Left + this.ListViewGroupLineWidth, headerBounds2.Top + num);
			stringFormat.Dispose();
			font.Dispose();
			pen.Dispose();
			brush.Dispose();
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x00002D70 File Offset: 0x00000F70
		public override bool ListViewHasHotHeaderStyle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0005755C File Offset: 0x0005575C
		public override int ListViewGetHeaderHeight(ListView listView, Font font)
		{
			return ThemeWin32Classic.ListViewGetHeaderHeight(font);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00057564 File Offset: 0x00055764
		private static int ListViewGetHeaderHeight(Font font)
		{
			return font.Height + 5;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0005756E File Offset: 0x0005576E
		public static int ListViewGetHeaderHeight()
		{
			return ThemeWin32Classic.ListViewGetHeaderHeight(ThemeEngine.Current.DefaultFont);
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x0005757F File Offset: 0x0005577F
		public override Size ListViewCheckBoxSize
		{
			get
			{
				return new Size(16, 16);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x0005758A File Offset: 0x0005578A
		public override int ListViewDefaultColumnWidth
		{
			get
			{
				return 60;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x0005758E File Offset: 0x0005578E
		public override int ListViewVerticalSpacing
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x00057592 File Offset: 0x00055792
		public override int ListViewEmptyColumnWidth
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x00057596 File Offset: 0x00055796
		public override int ListViewHorizontalSpacing
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x00057599 File Offset: 0x00055799
		public override int ListViewItemPaddingWidth
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x0005759C File Offset: 0x0005579C
		public override Size ListViewDefaultSize
		{
			get
			{
				return new Size(121, 97);
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x000575A7 File Offset: 0x000557A7
		public int ListViewGroupLineWidth
		{
			get
			{
				return 200;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x0005758E File Offset: 0x0005578E
		public override int ListViewTileWidthFactor
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x00050984 File Offset: 0x0004EB84
		public override int ListViewTileHeightFactor
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x000575B0 File Offset: 0x000557B0
		public override void CalcItemSize(Graphics dc, MenuItem item, int y, int x, bool menuBar)
		{
			item.X = x;
			item.Y = y;
			if (!item.Visible)
			{
				item.Width = 0;
				item.Height = 0;
				return;
			}
			if (item.Separator)
			{
				item.Height = 6;
				item.Width = 20;
				return;
			}
			if (item.MeasureEventDefined)
			{
				MeasureItemEventArgs measureItemEventArgs = new MeasureItemEventArgs(dc, item.Index);
				item.PerformMeasureItem(measureItemEventArgs);
				item.Height = measureItemEventArgs.ItemHeight;
				item.Width = measureItemEventArgs.ItemWidth;
				return;
			}
			SizeF sizeF = dc.MeasureString(item.Text, this.MenuFont, int.MaxValue, ThemeWin32Classic.string_format_menu_text);
			item.Width = (int)sizeF.Width;
			item.Height = (int)sizeF.Height;
			if (!menuBar)
			{
				if (item.Shortcut != Shortcut.None && item.ShowShortcut)
				{
					item.XTab = this.MenuCheckSize.Width + 8 + (int)sizeF.Width;
					sizeF = dc.MeasureString(" " + item.GetShortCutText(), this.MenuFont);
					item.Width += 8 + (int)sizeF.Width;
				}
				item.Width += 4 + this.MenuCheckSize.Width * 2;
			}
			else
			{
				item.Width += 8;
				x += item.Width;
			}
			if (item.Height < this.MenuHeight)
			{
				item.Height = this.MenuHeight;
			}
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00057728 File Offset: 0x00055928
		public override int CalcMenuBarSize(Graphics dc, Menu menu, int width)
		{
			int num = 0;
			int num2 = 0;
			menu.Height = 0;
			foreach (object obj in menu.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				this.CalcItemSize(dc, menuItem, num2, num, true);
				if (num + menuItem.Width > width)
				{
					menuItem.X = 0;
					num2 += menuItem.Height;
					menuItem.Y = num2;
					num = 0;
				}
				num += menuItem.Width;
				menuItem.MenuBar = true;
				if (num2 + menuItem.Height > menu.Height)
				{
					menu.Height = menuItem.Height + num2;
				}
			}
			menu.Width = width;
			return menu.Height;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x000577F4 File Offset: 0x000559F4
		public override void CalcPopupMenuSize(Graphics dc, Menu menu)
		{
			int num = 3;
			int i = 0;
			menu.Height = 0;
			while (i < menu.MenuItems.Count)
			{
				int num2 = 3;
				int num3 = 0;
				int j;
				for (j = i; j < menu.MenuItems.Count; j++)
				{
					MenuItem menuItem = menu.MenuItems[j];
					if (j != i && (menuItem.Break || menuItem.BarBreak))
					{
						break;
					}
					this.CalcItemSize(dc, menuItem, num2, num, false);
					num2 += menuItem.Height;
					if (menuItem.Width > num3)
					{
						num3 = menuItem.Width;
					}
				}
				int k = i;
				while (k < j)
				{
					menu.MenuItems[k].Width = num3;
					k++;
					i++;
				}
				if (num2 > menu.Height)
				{
					menu.Height = num2;
				}
				num += num3;
			}
			menu.Width = num;
			menu.Width += 2;
			menu.Height += 2;
			menu.Width++;
			menu.Height++;
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00057908 File Offset: 0x00055B08
		public override void DrawMenuBar(Graphics dc, Menu menu, Rectangle rect)
		{
			if (menu.Height == 0)
			{
				this.CalcMenuBarSize(dc, menu, rect.Width);
			}
			bool hotkey_active = (menu as MainMenu).tracker.hotkey_active;
			HotkeyPrefix hotkeyPrefix = ((this.MenuAccessKeysUnderlined || hotkey_active) ? HotkeyPrefix.Show : HotkeyPrefix.Hide);
			ThemeWin32Classic.string_format_menu_menubar_text.HotkeyPrefix = hotkeyPrefix;
			ThemeWin32Classic.string_format_menu_text.HotkeyPrefix = hotkeyPrefix;
			rect.Height = menu.Height;
			dc.FillRectangle(SystemBrushes.Menu, rect);
			for (int i = 0; i < menu.MenuItems.Count; i++)
			{
				MenuItem menuItem = menu.MenuItems[i];
				Rectangle bounds = menuItem.bounds;
				bounds.X += rect.X;
				bounds.Y += rect.Y;
				menuItem.MenuHeight = menu.Height;
				menuItem.PerformDrawItem(new DrawItemEventArgs(dc, this.MenuFont, bounds, i, menuItem.Status));
			}
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x000579F8 File Offset: 0x00055BF8
		protected Bitmap CreateGlyphBitmap(Size size, MenuGlyph glyph, Color color)
		{
			Color color2;
			if (color.R == 0 && color.G == 0 && color.B == 0)
			{
				color2 = Color.White;
			}
			else
			{
				color2 = Color.Black;
			}
			Bitmap bitmap = new Bitmap(size.Width, size.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			Rectangle rectangle = new Rectangle(Point.Empty, size);
			graphics.FillRectangle(this.ResPool.GetSolidBrush(color2), rectangle);
			this.CPDrawMenuGlyph(graphics, rectangle, glyph, color, Color.Empty);
			bitmap.MakeTransparent(color2);
			graphics.Dispose();
			return bitmap;
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00057A84 File Offset: 0x00055C84
		public override void DrawMenuItem(MenuItem item, DrawItemEventArgs e)
		{
			Rectangle bounds = e.Bounds;
			if (!item.Visible)
			{
				return;
			}
			StringFormat stringFormat;
			if (item.MenuBar)
			{
				stringFormat = ThemeWin32Classic.string_format_menu_menubar_text;
			}
			else
			{
				stringFormat = ThemeWin32Classic.string_format_menu_text;
			}
			if (item.Separator)
			{
				int num = e.Bounds.Y + e.Bounds.Height / 2;
				e.Graphics.DrawLine(SystemPens.ControlDark, e.Bounds.X, num, e.Bounds.X + e.Bounds.Width, num);
				e.Graphics.DrawLine(SystemPens.ControlLight, e.Bounds.X, num + 1, e.Bounds.X + e.Bounds.Width, num + 1);
				return;
			}
			if (!item.MenuBar)
			{
				bounds.X += this.MenuCheckSize.Width;
			}
			if (item.BarBreak)
			{
				Rectangle bounds2 = e.Bounds;
				int y = bounds2.Y;
				bounds2.Y = y + 1;
				bounds2.Width = 3;
				bounds2.Height = item.MenuHeight - 6;
				e.Graphics.DrawLine(SystemPens.ControlDark, bounds2.X, bounds2.Y, bounds2.X, bounds2.Y + bounds2.Height);
				e.Graphics.DrawLine(SystemPens.ControlLight, bounds2.X + 1, bounds2.Y, bounds2.X + 1, bounds2.Y + bounds2.Height);
			}
			Color color;
			Color color2;
			Brush brush;
			Brush brush2;
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && !item.MenuBar)
			{
				color = this.ColorHighlightText;
				color2 = this.ColorHighlight;
				brush = SystemBrushes.HighlightText;
				brush2 = SystemBrushes.Highlight;
			}
			else
			{
				color = this.ColorMenuText;
				color2 = this.ColorMenu;
				brush = this.ResPool.GetSolidBrush(this.ColorMenuText);
				brush2 = SystemBrushes.Menu;
			}
			if (!item.MenuBar)
			{
				e.Graphics.FillRectangle(brush2, e.Bounds);
			}
			if (item.Enabled)
			{
				e.Graphics.DrawString(item.Text, e.Font, brush, bounds, stringFormat);
				if (item.MenuBar)
				{
					Border3DStyle border3DStyle = Border3DStyle.Adjust;
					if ((item.Status & DrawItemState.HotLight) != DrawItemState.None)
					{
						border3DStyle = Border3DStyle.RaisedInner;
					}
					else if ((item.Status & DrawItemState.Selected) != DrawItemState.None)
					{
						border3DStyle = Border3DStyle.SunkenOuter;
					}
					if (border3DStyle != Border3DStyle.Adjust)
					{
						this.CPDrawBorder3D(e.Graphics, e.Bounds, border3DStyle, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom, this.ColorMenu);
					}
				}
			}
			else
			{
				if ((item.Status & DrawItemState.Selected) != DrawItemState.Selected)
				{
					e.Graphics.DrawString(item.Text, e.Font, Brushes.White, new RectangleF((float)(bounds.X + 1), (float)(bounds.Y + 1), (float)bounds.Width, (float)bounds.Height), stringFormat);
				}
				e.Graphics.DrawString(item.Text, e.Font, this.ResPool.GetSolidBrush(this.ColorGrayText), bounds, stringFormat);
			}
			if (!item.MenuBar && item.Shortcut != Shortcut.None && item.ShowShortcut)
			{
				string shortCutText = item.GetShortCutText();
				Rectangle rectangle = bounds;
				rectangle.X = item.XTab;
				rectangle.Width -= item.XTab;
				if (item.Enabled)
				{
					e.Graphics.DrawString(shortCutText, e.Font, brush, rectangle, ThemeWin32Classic.string_format_menu_shortcut);
				}
				else
				{
					if ((item.Status & DrawItemState.Selected) != DrawItemState.Selected)
					{
						e.Graphics.DrawString(shortCutText, e.Font, Brushes.White, new RectangleF((float)(rectangle.X + 1), (float)(rectangle.Y + 1), (float)rectangle.Width, (float)bounds.Height), ThemeWin32Classic.string_format_menu_shortcut);
					}
					e.Graphics.DrawString(shortCutText, e.Font, this.ResPool.GetSolidBrush(this.ColorGrayText), rectangle, ThemeWin32Classic.string_format_menu_shortcut);
				}
			}
			if (!item.MenuBar && (item.IsPopup || item.MdiList))
			{
				int width = this.MenuCheckSize.Width;
				int height = this.MenuCheckSize.Height;
				Bitmap bitmap = this.CreateGlyphBitmap(new Size(width, height), MenuGlyph.Arrow, color);
				if (item.Enabled)
				{
					e.Graphics.DrawImage(bitmap, e.Bounds.X + e.Bounds.Width - width, e.Bounds.Y + (e.Bounds.Height - height) / 2);
				}
				else
				{
					ControlPaint.DrawImageDisabled(e.Graphics, bitmap, e.Bounds.X + e.Bounds.Width - width, e.Bounds.Y + (e.Bounds.Height - height) / 2, color2);
				}
				bitmap.Dispose();
			}
			if (!item.MenuBar && item.Checked)
			{
				Rectangle bounds3 = e.Bounds;
				int width2 = this.MenuCheckSize.Width;
				int height2 = this.MenuCheckSize.Height;
				Bitmap bitmap2 = this.CreateGlyphBitmap(new Size(width2, height2), item.RadioCheck ? MenuGlyph.Bullet : MenuGlyph.Checkmark, color);
				e.Graphics.DrawImage(bitmap2, bounds3.X, e.Bounds.Y + (e.Bounds.Height - height2) / 2);
				bitmap2.Dispose();
			}
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00058060 File Offset: 0x00056260
		public override void DrawPopupMenu(Graphics dc, Menu menu, Rectangle cliparea, Rectangle rect)
		{
			dc.FillRectangle(SystemBrushes.Menu, cliparea);
			this.CPDrawBorder3D(dc, rect, Border3DStyle.Raised, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
			for (int i = 0; i < menu.MenuItems.Count; i++)
			{
				if (cliparea.IntersectsWith(menu.MenuItems[i].bounds))
				{
					MenuItem menuItem = menu.MenuItems[i];
					menuItem.MenuHeight = menu.Height;
					menuItem.PerformDrawItem(new DrawItemEventArgs(dc, this.MenuFont, menuItem.bounds, i, menuItem.Status));
				}
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x00056176 File Offset: 0x00054376
		public override Size PanelDefaultSize
		{
			get
			{
				return new Size(200, 100);
			}
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x000580F0 File Offset: 0x000562F0
		public override void DrawPictureBox(Graphics dc, Rectangle clip, PictureBox pb)
		{
			Rectangle clientRectangle = pb.ClientRectangle;
			clientRectangle = new Rectangle(clientRectangle.Left + pb.Padding.Left, clientRectangle.Top + pb.Padding.Top, clientRectangle.Width - pb.Padding.Horizontal, clientRectangle.Height - pb.Padding.Vertical);
			if (pb.Image != null)
			{
				switch (pb.SizeMode)
				{
				case PictureBoxSizeMode.StretchImage:
					dc.DrawImage(pb.Image, clientRectangle.Left, clientRectangle.Top, clientRectangle.Width, clientRectangle.Height);
					return;
				case PictureBoxSizeMode.CenterImage:
					dc.DrawImage(pb.Image, clientRectangle.Width / 2 - pb.Image.Width / 2, clientRectangle.Height / 2 - pb.Image.Height / 2);
					return;
				case PictureBoxSizeMode.Zoom:
				{
					Size size;
					if ((float)pb.Image.Width / (float)pb.Image.Height >= (float)clientRectangle.Width / (float)clientRectangle.Height)
					{
						size = new Size(clientRectangle.Width, pb.Image.Height * clientRectangle.Width / pb.Image.Width);
					}
					else
					{
						size = new Size(pb.Image.Width * clientRectangle.Height / pb.Image.Height, clientRectangle.Height);
					}
					dc.DrawImage(pb.Image, clientRectangle.Width / 2 - size.Width / 2, clientRectangle.Height / 2 - size.Height / 2, size.Width, size.Height);
					return;
				}
				}
				dc.DrawImage(pb.Image, clientRectangle.Left, clientRectangle.Top, pb.Image.Width, pb.Image.Height);
				return;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x000582F0 File Offset: 0x000564F0
		public override Size PictureBoxDefaultSize
		{
			get
			{
				return new Size(100, 50);
			}
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000582FC File Offset: 0x000564FC
		public override void DrawScrollBar(Graphics dc, Rectangle clip, ScrollBar bar)
		{
			int scrollbutton_width = bar.scrollbutton_width;
			int scrollbutton_height = bar.scrollbutton_height;
			Rectangle thumbPos = bar.ThumbPos;
			if (bar.vert)
			{
				Rectangle rectangle = new Rectangle(0, 0, bar.Width, scrollbutton_height);
				bar.FirstArrowArea = rectangle;
				Rectangle rectangle2 = new Rectangle(0, bar.ClientRectangle.Height - scrollbutton_height, bar.Width, scrollbutton_height);
				bar.SecondArrowArea = rectangle2;
				thumbPos.Width = bar.Width;
				bar.ThumbPos = thumbPos;
				Brush brush;
				if (bar.thumb_moving == ScrollBar.ThumbMoving.Backwards)
				{
					brush = this.ResPool.GetHatchBrush(HatchStyle.Percent50, Color.FromArgb(255, 63, 63, 63), Color.Black);
				}
				else
				{
					brush = this.ResPool.GetHatchBrush(HatchStyle.Percent50, this.ColorScrollBar, Color.White);
				}
				Rectangle rectangle3 = new Rectangle(0, 0, bar.ClientRectangle.Width, bar.ThumbPos.Bottom);
				if (clip.IntersectsWith(rectangle3))
				{
					dc.FillRectangle(brush, rectangle3);
				}
				if (bar.thumb_moving == ScrollBar.ThumbMoving.Forward)
				{
					brush = this.ResPool.GetHatchBrush(HatchStyle.Percent50, Color.FromArgb(255, 63, 63, 63), Color.Black);
				}
				else
				{
					brush = this.ResPool.GetHatchBrush(HatchStyle.Percent50, this.ColorScrollBar, Color.White);
				}
				Rectangle rectangle4 = new Rectangle(0, bar.ThumbPos.Bottom, bar.ClientRectangle.Width, bar.ClientRectangle.Height - bar.ThumbPos.Bottom);
				if (clip.IntersectsWith(rectangle4))
				{
					dc.FillRectangle(brush, rectangle4);
				}
				if (clip.IntersectsWith(rectangle))
				{
					this.CPDrawScrollButton(dc, rectangle, ScrollButton.Min, bar.firstbutton_state);
				}
				if (clip.IntersectsWith(rectangle2))
				{
					this.CPDrawScrollButton(dc, rectangle2, ScrollButton.Down, bar.secondbutton_state);
				}
			}
			else
			{
				Rectangle rectangle = new Rectangle(0, 0, scrollbutton_width, bar.Height);
				bar.FirstArrowArea = rectangle;
				Rectangle rectangle2 = new Rectangle(bar.ClientRectangle.Width - scrollbutton_width, 0, scrollbutton_width, bar.Height);
				bar.SecondArrowArea = rectangle2;
				thumbPos.Height = bar.Height;
				bar.ThumbPos = thumbPos;
				Brush brush2;
				if (bar.thumb_moving == ScrollBar.ThumbMoving.Backwards)
				{
					brush2 = this.ResPool.GetHatchBrush(HatchStyle.Percent50, Color.FromArgb(255, 63, 63, 63), Color.Black);
				}
				else
				{
					brush2 = this.ResPool.GetHatchBrush(HatchStyle.Percent50, this.ColorScrollBar, Color.White);
				}
				Rectangle rectangle5 = new Rectangle(0, 0, bar.ThumbPos.Right, bar.ClientRectangle.Height);
				if (clip.IntersectsWith(rectangle5))
				{
					dc.FillRectangle(brush2, rectangle5);
				}
				if (bar.thumb_moving == ScrollBar.ThumbMoving.Forward)
				{
					brush2 = this.ResPool.GetHatchBrush(HatchStyle.Percent50, Color.FromArgb(255, 63, 63, 63), Color.Black);
				}
				else
				{
					brush2 = this.ResPool.GetHatchBrush(HatchStyle.Percent50, this.ColorScrollBar, Color.White);
				}
				Rectangle rectangle6 = new Rectangle(bar.ThumbPos.Right, 0, bar.ClientRectangle.Width - bar.ThumbPos.Right, bar.ClientRectangle.Height);
				if (clip.IntersectsWith(rectangle6))
				{
					dc.FillRectangle(brush2, rectangle6);
				}
				if (clip.IntersectsWith(rectangle))
				{
					this.CPDrawScrollButton(dc, rectangle, ScrollButton.Left, bar.firstbutton_state);
				}
				if (clip.IntersectsWith(rectangle2))
				{
					this.CPDrawScrollButton(dc, rectangle2, ScrollButton.Right, bar.secondbutton_state);
				}
			}
			this.ScrollBar_DrawThumb(bar, thumbPos, clip, dc);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00058697 File Offset: 0x00056897
		protected virtual void ScrollBar_DrawThumb(ScrollBar bar, Rectangle thumb_pos, Rectangle clip, Graphics dc)
		{
			if (bar.Enabled && thumb_pos.Width > 0 && thumb_pos.Height > 0 && clip.IntersectsWith(thumb_pos))
			{
				this.DrawScrollButtonPrimitive(dc, thumb_pos, ButtonState.Normal);
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0005093A File Offset: 0x0004EB3A
		public override int ScrollBarButtonSize
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x00002D70 File Offset: 0x00000F70
		public override bool ScrollBarHasHotElementStyles
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x00002D70 File Offset: 0x00000F70
		public override bool ScrollBarHasPressedThumbStyle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00002D70 File Offset: 0x00000F70
		public override bool ScrollBarHasHoverArrowButtonStyle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x000586CC File Offset: 0x000568CC
		public override void TextBoxBaseFillBackground(TextBoxBase textBoxBase, Graphics g, Rectangle clippingArea)
		{
			if (textBoxBase.backcolor_set || (textBoxBase.Enabled && !textBoxBase.read_only))
			{
				g.FillRectangle(this.ResPool.GetSolidBrush(textBoxBase.BackColor), clippingArea);
				return;
			}
			g.FillRectangle(this.ResPool.GetSolidBrush(this.ColorControl), clippingArea);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00002D70 File Offset: 0x00000F70
		public override bool TextBoxBaseHandleWmNcPaint(TextBoxBase textBoxBase, ref Message m)
		{
			return false;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00006F54 File Offset: 0x00005154
		public override bool TextBoxBaseShouldPaintBackground(TextBoxBase textBoxBase)
		{
			return true;
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00058724 File Offset: 0x00056924
		public override void DrawToolBar(Graphics dc, Rectangle clip_rectangle, ToolBar control)
		{
			StringFormat stringFormat = new StringFormat();
			stringFormat.Trimming = StringTrimming.EllipsisCharacter;
			stringFormat.LineAlignment = StringAlignment.Center;
			if (control.ShowKeyboardCuesInternal)
			{
				stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
			}
			else
			{
				stringFormat.HotkeyPrefix = HotkeyPrefix.Hide;
			}
			if (control.TextAlign == ToolBarTextAlign.Underneath)
			{
				stringFormat.Alignment = StringAlignment.Center;
			}
			else
			{
				stringFormat.Alignment = StringAlignment.Near;
			}
			if (control.Appearance != ToolBarAppearance.Flat || control.Parent == null)
			{
				dc.FillRectangle(SystemBrushes.Control, clip_rectangle);
			}
			if (control.Divider && clip_rectangle.Y < 2)
			{
				if (clip_rectangle.Y < 1)
				{
					dc.DrawLine(SystemPens.ControlDark, clip_rectangle.X, 0, clip_rectangle.Right, 0);
				}
				dc.DrawLine(SystemPens.ControlLightLight, clip_rectangle.X, 1, clip_rectangle.Right, 1);
			}
			foreach (ToolBarItem toolBarItem in control.items)
			{
				if (toolBarItem.Button.Visible && clip_rectangle.IntersectsWith(toolBarItem.Rectangle))
				{
					this.DrawToolBarButton(dc, control, toolBarItem, stringFormat);
				}
			}
			stringFormat.Dispose();
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0005882C File Offset: 0x00056A2C
		protected virtual void DrawToolBarButton(Graphics dc, ToolBar control, ToolBarItem item, StringFormat format)
		{
			bool flag = control.Appearance == ToolBarAppearance.Flat;
			this.DrawToolBarButtonBorder(dc, item, flag);
			switch (item.Button.Style)
			{
			case ToolBarButtonStyle.ToggleButton:
				this.DrawToolBarToggleButtonBackground(dc, item);
				this.DrawToolBarButtonContents(dc, control, item, format);
				return;
			case ToolBarButtonStyle.Separator:
				if (flag)
				{
					this.DrawToolBarSeparator(dc, item);
					return;
				}
				break;
			case ToolBarButtonStyle.DropDownButton:
				if (control.DropDownArrows)
				{
					this.DrawToolBarDropDownArrow(dc, item, flag);
				}
				this.DrawToolBarButtonContents(dc, control, item, format);
				return;
			default:
				this.DrawToolBarButtonContents(dc, control, item, format);
				break;
			}
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x000588B8 File Offset: 0x00056AB8
		protected virtual void DrawToolBarButtonBorder(Graphics dc, ToolBarItem item, bool is_flat)
		{
			if (item.Button.Style == ToolBarButtonStyle.Separator)
			{
				return;
			}
			Border3DStyle border3DStyle;
			if (is_flat)
			{
				if (item.Button.Pushed || item.Pressed)
				{
					border3DStyle = Border3DStyle.SunkenOuter;
				}
				else
				{
					if (!item.Hilight)
					{
						return;
					}
					border3DStyle = Border3DStyle.RaisedInner;
				}
			}
			else if (item.Button.Pushed || item.Pressed)
			{
				border3DStyle = Border3DStyle.Sunken;
			}
			else
			{
				border3DStyle = Border3DStyle.Raised;
			}
			Rectangle rectangle = item.Rectangle;
			if (item.Button.Style == ToolBarButtonStyle.DropDownButton && item.Button.Parent.DropDownArrows && is_flat)
			{
				rectangle.Width -= this.ToolBarDropDownWidth;
			}
			this.CPDrawBorder3D(dc, rectangle, border3DStyle, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00058964 File Offset: 0x00056B64
		protected virtual void DrawToolBarSeparator(Graphics dc, ToolBarItem item)
		{
			Rectangle rectangle = item.Rectangle;
			int num = (int)SystemPens.Control.Width + 1;
			dc.DrawLine(SystemPens.ControlDark, rectangle.X + 1, rectangle.Y, rectangle.X + 1, rectangle.Bottom);
			dc.DrawLine(SystemPens.ControlLight, rectangle.X + num, rectangle.Y, rectangle.X + num, rectangle.Bottom);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x000589DC File Offset: 0x00056BDC
		protected virtual void DrawToolBarToggleButtonBackground(Graphics dc, ToolBarItem item)
		{
			Rectangle rectangle = item.Rectangle;
			rectangle.X += this.ToolBarImageGripWidth;
			rectangle.Y += this.ToolBarImageGripWidth;
			rectangle.Width -= 2 * this.ToolBarImageGripWidth;
			rectangle.Height -= 2 * this.ToolBarImageGripWidth;
			Brush brush;
			if (item.Button.Pushed)
			{
				brush = this.ResPool.GetHatchBrush(HatchStyle.Percent50, this.ColorScrollBar, this.ColorControlLightLight);
			}
			else if (item.Button.PartialPush)
			{
				brush = SystemBrushes.ControlLight;
			}
			else
			{
				brush = SystemBrushes.Control;
			}
			dc.FillRectangle(brush, rectangle);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00058A90 File Offset: 0x00056C90
		protected virtual void DrawToolBarDropDownArrow(Graphics dc, ToolBarItem item, bool is_flat)
		{
			Rectangle rectangle = item.Rectangle;
			rectangle.X = item.Rectangle.Right - this.ToolBarDropDownWidth;
			rectangle.Width = this.ToolBarDropDownWidth;
			if (is_flat)
			{
				if (item.DDPressed)
				{
					this.CPDrawBorder3D(dc, rectangle, Border3DStyle.SunkenOuter, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
				}
				else if (item.Button.Pushed || item.Pressed)
				{
					this.CPDrawBorder3D(dc, rectangle, Border3DStyle.SunkenOuter, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
				}
				else if (item.Hilight)
				{
					this.CPDrawBorder3D(dc, rectangle, Border3DStyle.RaisedInner, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
				}
			}
			else if (item.DDPressed)
			{
				this.CPDrawBorder3D(dc, rectangle, Border3DStyle.Flat, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
			}
			else if (item.Button.Pushed || item.Pressed)
			{
				this.CPDrawBorder3D(dc, Rectangle.Inflate(rectangle, -1, -1), Border3DStyle.SunkenOuter, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
			}
			else
			{
				this.CPDrawBorder3D(dc, rectangle, Border3DStyle.Raised, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
			}
			PointF[] array = new PointF[3];
			PointF pointF = new PointF((float)rectangle.X + (float)rectangle.Width / 2f, (float)(rectangle.Y + rectangle.Height / 2));
			if (item.Pressed || item.Button.Pushed || item.DDPressed)
			{
				pointF.X += 1f;
				pointF.Y += 1f;
			}
			array[0].X = pointF.X - (float)this.ToolBarDropDownArrowWidth / 2f + 0.5f;
			array[0].Y = pointF.Y;
			array[1].X = pointF.X + (float)this.ToolBarDropDownArrowWidth / 2f + 0.5f;
			array[1].Y = pointF.Y;
			array[2].X = pointF.X + 0.5f;
			array[2].Y = pointF.Y + (float)this.ToolBarDropDownArrowHeight;
			dc.FillPolygon(SystemBrushes.ControlText, array);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00058C9C File Offset: 0x00056E9C
		protected virtual void DrawToolBarButtonContents(Graphics dc, ToolBar control, ToolBarItem item, StringFormat format)
		{
			if (item.Button.Image != null)
			{
				int num = item.ImageRectangle.X + this.ToolBarImageGripWidth;
				int num2 = item.ImageRectangle.Y + this.ToolBarImageGripWidth;
				if (item.Pressed || item.Button.Pushed)
				{
					num++;
					num2++;
				}
				if (item.Button.Enabled)
				{
					dc.DrawImage(item.Button.Image, num, num2);
				}
				else
				{
					this.CPDrawImageDisabled(dc, item.Button.Image, num, num2, this.ColorControl);
				}
			}
			Rectangle textRectangle = item.TextRectangle;
			if (textRectangle.Width <= 0 || textRectangle.Height <= 0)
			{
				return;
			}
			if (item.Pressed || item.Button.Pushed)
			{
				textRectangle.X++;
				textRectangle.Y++;
			}
			if (item.Button.Enabled)
			{
				dc.DrawString(item.Button.Text, control.Font, SystemBrushes.ControlText, textRectangle, format);
				return;
			}
			this.CPDrawStringDisabled(dc, item.Button.Text, control.Font, control.BackColor, textRectangle, format);
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x00058DE5 File Offset: 0x00056FE5
		public override int ToolBarGripWidth
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00058DE5 File Offset: 0x00056FE5
		public override int ToolBarImageGripWidth
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x00057596 File Offset: 0x00055796
		public override int ToolBarSeparatorWidth
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x00058DE8 File Offset: 0x00056FE8
		public override int ToolBarDropDownWidth
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x00058DEC File Offset: 0x00056FEC
		public override int ToolBarDropDownArrowWidth
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x00050984 File Offset: 0x0004EB84
		public override int ToolBarDropDownArrowHeight
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x00058DEF File Offset: 0x00056FEF
		public override Size ToolBarDefaultSize
		{
			get
			{
				return new Size(100, 42);
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00058DFA File Offset: 0x00056FFA
		public override bool ToolBarHasHotElementStyles(ToolBar toolBar)
		{
			return toolBar.Appearance == ToolBarAppearance.Flat;
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x00002D70 File Offset: 0x00000F70
		public override bool ToolBarHasHotCheckedElementStyles
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00058E08 File Offset: 0x00057008
		public override void DrawToolTip(Graphics dc, Rectangle clip_rectangle, ToolTip.ToolTipWindow control)
		{
			this.ToolTipDrawBackground(dc, clip_rectangle, control);
			TextFormatFlags textFormatFlags = TextFormatFlags.HidePrefix;
			Color foreColor = control.ForeColor;
			if (control.title.Length > 0)
			{
				Font font = new Font(control.Font, control.Font.Style | FontStyle.Bold);
				TextRenderer.DrawTextInternal(dc, control.title, font, control.title_rect, foreColor, textFormatFlags, false);
				font.Dispose();
			}
			if (control.icon != null)
			{
				dc.DrawIcon(control.icon, control.icon_rect);
			}
			TextRenderer.DrawTextInternal(dc, control.Text, control.Font, control.text_rect, foreColor, textFormatFlags, false);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00058EA4 File Offset: 0x000570A4
		protected virtual void ToolTipDrawBackground(Graphics dc, Rectangle clip_rectangle, ToolTip.ToolTipWindow control)
		{
			Brush solidBrush = this.ResPool.GetSolidBrush(control.BackColor);
			dc.FillRectangle(solidBrush, control.ClientRectangle);
			dc.DrawRectangle(SystemPens.WindowFrame, 0, 0, control.Width - 1, control.Height - 1);
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00058EF0 File Offset: 0x000570F0
		public override Size ToolTipSize(ToolTip.ToolTipWindow tt, string text)
		{
			Size size = TextRenderer.MeasureTextInternal(text, tt.Font, false);
			size.Width += 4;
			size.Height += 3;
			Rectangle rectangle = new Rectangle(Point.Empty, size);
			rectangle.Inflate(-2, -1);
			tt.text_rect = rectangle;
			tt.icon_rect = (tt.title_rect = Rectangle.Empty);
			Size size2 = Size.Empty;
			if (tt.title.Length > 0)
			{
				Font font = new Font(tt.Font, tt.Font.Style | FontStyle.Bold);
				size2 = TextRenderer.MeasureTextInternal(tt.title, font, false);
				font.Dispose();
			}
			Size empty = Size.Empty;
			if (tt.icon != null)
			{
				empty = new Size(size.Height, size.Height);
			}
			if (empty != Size.Empty || size2 != Size.Empty)
			{
				int num = 8;
				int num2 = 0;
				int num3 = ((empty.Height > size2.Height) ? empty.Height : size2.Height);
				Size size3 = size;
				Point point = new Point(num, num);
				if (empty != Size.Empty)
				{
					tt.icon_rect = new Rectangle(point, empty);
					num2 = empty.Width + num;
				}
				if (size2 != Size.Empty)
				{
					Rectangle rectangle2 = new Rectangle(point, new Size(size2.Width, num3));
					if (empty != Size.Empty)
					{
						rectangle2.X += empty.Width + num;
					}
					tt.title_rect = rectangle2;
					num2 += size2.Width;
				}
				tt.text_rect = new Rectangle(new Point(point.X, point.Y + num3 + num), size3);
				size.Height += num + num3;
				if (num2 > size.Width)
				{
					size.Width = num2;
				}
				size.Width += num * 2;
				size.Height += num * 2;
			}
			return size;
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x00002D70 File Offset: 0x00000F70
		public override bool ToolTipTransparentBackground
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0005910D File Offset: 0x0005730D
		public static Size TrackBarGetThumbSize()
		{
			return new Size(10, 22);
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00059118 File Offset: 0x00057318
		public override Size VScrollBarDefaultSize
		{
			get
			{
				return new Size(this.ScrollBarButtonSize, 80);
			}
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00059128 File Offset: 0x00057328
		public override void TreeViewDrawNodePlusMinus(TreeView treeView, TreeNode node, Graphics dc, int x, int middle)
		{
			int num = treeView.ActualItemHeight - 2;
			dc.FillRectangle(this.ResPool.GetSolidBrush(treeView.BackColor), x + 4 - num / 2, node.GetY() + 1, num, num);
			dc.DrawRectangle(SystemPens.ControlDarkDark, x, middle - 4, 8, 8);
			if (node.IsExpanded)
			{
				dc.DrawLine(SystemPens.ControlDarkDark, x + 2, middle, x + 6, middle);
				return;
			}
			dc.DrawLine(SystemPens.ControlDarkDark, x + 2, middle, x + 6, middle);
			dc.DrawLine(SystemPens.ControlDarkDark, x + 4, middle - 2, x + 4, middle + 2);
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x000591CC File Offset: 0x000573CC
		public override int ManagedWindowTitleBarHeight(InternalWindowManager wm)
		{
			if (wm.IsToolWindow && !wm.IsMinimized)
			{
				return SystemInformation.ToolWindowCaptionHeight;
			}
			if (wm.Form.FormBorderStyle == FormBorderStyle.None)
			{
				return 0;
			}
			return SystemInformation.CaptionHeight;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x000591F8 File Offset: 0x000573F8
		public override int ManagedWindowBorderWidth(InternalWindowManager wm)
		{
			if ((wm.IsToolWindow && wm.form.FormBorderStyle == FormBorderStyle.FixedToolWindow) || wm.IsMinimized)
			{
				return 3;
			}
			return 4;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0005921C File Offset: 0x0005741C
		public override void ManagedWindowSetButtonLocations(InternalWindowManager wm)
		{
			TitleButtons titleButtons = wm.TitleButtons;
			Form form = wm.form;
			titleButtons.HelpButton.Visible = form.HelpButton;
			foreach (object obj in titleButtons)
			{
				((TitleButton)obj).Visible = false;
			}
			switch (form.FormBorderStyle)
			{
			case FormBorderStyle.None:
				if (form.WindowState == FormWindowState.Normal)
				{
					goto IL_0148;
				}
				break;
			case FormBorderStyle.FixedSingle:
			case FormBorderStyle.Fixed3D:
			case FormBorderStyle.FixedDialog:
			case FormBorderStyle.Sizable:
				break;
			case FormBorderStyle.FixedToolWindow:
			case FormBorderStyle.SizableToolWindow:
				titleButtons.CloseButton.Visible = true;
				if (form.WindowState == FormWindowState.Normal)
				{
					goto IL_0148;
				}
				break;
			default:
				goto IL_0148;
			}
			switch (form.WindowState)
			{
			case FormWindowState.Normal:
				titleButtons.MinimizeButton.Visible = true;
				titleButtons.MaximizeButton.Visible = true;
				titleButtons.RestoreButton.Visible = false;
				break;
			case FormWindowState.Minimized:
				titleButtons.MinimizeButton.Visible = false;
				titleButtons.MaximizeButton.Visible = true;
				titleButtons.RestoreButton.Visible = true;
				break;
			case FormWindowState.Maximized:
				titleButtons.MinimizeButton.Visible = true;
				titleButtons.MaximizeButton.Visible = false;
				titleButtons.RestoreButton.Visible = true;
				break;
			}
			titleButtons.CloseButton.Visible = true;
			IL_0148:
			if (!form.MinimizeBox && !form.MaximizeBox)
			{
				titleButtons.MinimizeButton.Visible = false;
				titleButtons.MaximizeButton.Visible = false;
			}
			else if (!form.MinimizeBox)
			{
				titleButtons.MinimizeButton.State = ButtonState.Inactive;
			}
			else if (!form.MaximizeBox)
			{
				titleButtons.MaximizeButton.State = ButtonState.Inactive;
			}
			int num = this.ManagedWindowBorderWidth(wm);
			Size size = this.ManagedWindowButtonSize(wm);
			int width = size.Width;
			int height = size.Height;
			int num2 = num + 2;
			int num3 = form.Width - num - width - 2;
			if ((!wm.IsToolWindow || wm.IsMinimized) && wm.HasBorders)
			{
				titleButtons.CloseButton.Rectangle = new Rectangle(num3, num2, width, height);
				num3 -= 2 + width;
				if (titleButtons.MaximizeButton.Visible)
				{
					titleButtons.MaximizeButton.Rectangle = new Rectangle(num3, num2, width, height);
					num3 -= 2 + width;
				}
				if (titleButtons.RestoreButton.Visible)
				{
					titleButtons.RestoreButton.Rectangle = new Rectangle(num3, num2, width, height);
					num3 -= 2 + width;
				}
				titleButtons.MinimizeButton.Rectangle = new Rectangle(num3, num2, width, height);
				num3 -= 2 + width;
				return;
			}
			if (wm.IsToolWindow)
			{
				titleButtons.CloseButton.Rectangle = new Rectangle(num3, num2, width, height);
				num3 -= 2 + width;
			}
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x000594FC File Offset: 0x000576FC
		protected virtual Rectangle ManagedWindowDrawTitleBarAndBorders(Graphics dc, Rectangle clip, InternalWindowManager wm)
		{
			Form form = wm.Form;
			int num = this.ManagedWindowTitleBarHeight(wm);
			int num2 = this.ManagedWindowBorderWidth(wm);
			Color color = Color.FromArgb(255, 10, 36, 106);
			Color color2 = Color.FromArgb(255, 166, 202, 240);
			Color color3 = ThemeEngine.Current.ColorControlDark;
			Color color4 = Color.FromArgb(255, 192, 192, 192);
			Pen pen = this.ResPool.GetPen(this.ColorControl);
			Rectangle rectangle = new Rectangle(0, 0, form.Width, form.Height);
			ControlPaint.DrawBorder3D(dc, rectangle, Border3DStyle.Raised);
			rectangle = new Rectangle(2, 2, form.Width - 5, form.Height - 5);
			for (int i = 2; i < num2; i++)
			{
				dc.DrawRectangle(pen, rectangle);
				rectangle.Inflate(-1, -1);
			}
			bool flag = false;
			if (wm.Form.Parent != null && wm.Form.Parent is Form)
			{
				flag = false;
			}
			else if (wm.IsActive && !wm.IsMaximized)
			{
				flag = true;
			}
			if (flag)
			{
				color3 = color;
				color4 = color2;
			}
			Rectangle rectangle2 = new Rectangle(num2, num2, form.Width - num2 * 2, num - 1);
			if (rectangle2.Width > 0 && rectangle2.Height > 0)
			{
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle2, color3, color4, LinearGradientMode.Horizontal))
				{
					dc.FillRectangle(linearGradientBrush, rectangle2);
				}
			}
			if (!wm.IsMinimized)
			{
				dc.DrawLine(this.ResPool.GetPen(SystemColors.Control), num2, num + num2 - 1, form.Width - num2 - 1, num + num2 - 1);
			}
			return rectangle2;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x000596BC File Offset: 0x000578BC
		public override void DrawManagedWindowDecorations(Graphics dc, Rectangle clip, InternalWindowManager wm)
		{
			Rectangle rectangle = this.ManagedWindowDrawTitleBarAndBorders(dc, clip, wm);
			Form form = wm.Form;
			if (wm.ShowIcon)
			{
				Rectangle rectangle2 = this.ManagedWindowGetTitleBarIconArea(wm);
				if (rectangle2.IntersectsWith(clip))
				{
					dc.DrawIcon(form.Icon, rectangle2);
				}
				rectangle.Width -= rectangle2.Right + 2 - rectangle.X;
				rectangle.X = rectangle2.Right + 2;
			}
			foreach (TitleButton titleButton in wm.TitleButtons.AllButtons)
			{
				rectangle.Width -= Math.Max(0, rectangle.Right - this.DrawTitleButton(dc, titleButton, clip, form));
			}
			rectangle.Width -= 3;
			string text = form.Text;
			text = text.Replace(Environment.NewLine, string.Empty);
			if (text != null && text != string.Empty)
			{
				StringFormat stringFormat = new StringFormat();
				stringFormat.FormatFlags = StringFormatFlags.NoWrap;
				stringFormat.Trimming = StringTrimming.EllipsisCharacter;
				stringFormat.LineAlignment = StringAlignment.Center;
				if (rectangle.IntersectsWith(clip))
				{
					dc.DrawString(text, this.WindowBorderFont, ThemeEngine.Current.ResPool.GetSolidBrush(Color.White), rectangle, stringFormat);
				}
			}
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0005980C File Offset: 0x00057A0C
		public override Size ManagedWindowButtonSize(InternalWindowManager wm)
		{
			int num = this.ManagedWindowTitleBarHeight(wm);
			if (!wm.IsMaximized && !wm.IsMinimized)
			{
				if (wm.IsToolWindow)
				{
					return new Size(SystemInformation.ToolWindowCaptionButtonSize.Width - 2, num - 5);
				}
				if (wm.Form.FormBorderStyle == FormBorderStyle.None)
				{
					return Size.Empty;
				}
			}
			else
			{
				num = SystemInformation.CaptionHeight;
			}
			return new Size(SystemInformation.CaptionButtonSize.Width - 2, num - 5);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00059881 File Offset: 0x00057A81
		private int DrawTitleButton(Graphics dc, TitleButton button, Rectangle clip, Form form)
		{
			if (!button.Visible)
			{
				return int.MaxValue;
			}
			if (button.Rectangle.IntersectsWith(clip))
			{
				this.ManagedWindowDrawTitleButton(dc, button, clip, form);
			}
			return button.Rectangle.Left;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x000598B5 File Offset: 0x00057AB5
		protected virtual void ManagedWindowDrawTitleButton(Graphics dc, TitleButton button, Rectangle clip, Form form)
		{
			dc.FillRectangle(SystemBrushes.Control, button.Rectangle);
			ControlPaint.DrawCaptionButton(dc, button.Rectangle, button.Caption, button.State);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000598E0 File Offset: 0x00057AE0
		public override Rectangle ManagedWindowGetTitleBarIconArea(InternalWindowManager wm)
		{
			int num = this.ManagedWindowBorderWidth(wm);
			return new Rectangle(num + 3, num + 2, wm.IconWidth, wm.IconWidth);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0005990C File Offset: 0x00057B0C
		public override Size ManagedWindowGetMenuButtonSize(InternalWindowManager wm)
		{
			Size menuButtonSize = SystemInformation.MenuButtonSize;
			menuButtonSize.Width -= 2;
			menuButtonSize.Height -= 4;
			return menuButtonSize;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00002D70 File Offset: 0x00000F70
		public override bool ManagedWindowTitleButtonHasHotElementStyle(TitleButton button, Form form)
		{
			return false;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x000598B5 File Offset: 0x00057AB5
		public override void ManagedWindowDrawMenuButton(Graphics dc, TitleButton button, Rectangle clip, InternalWindowManager wm)
		{
			dc.FillRectangle(SystemBrushes.Control, button.Rectangle);
			ControlPaint.DrawCaptionButton(dc, button.Rectangle, button.Caption, button.State);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0000493C File Offset: 0x00002B3C
		public override void ManagedWindowOnSizeInitializedOrChanged(Form form)
		{
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00059940 File Offset: 0x00057B40
		public override void CPDrawBorder(Graphics graphics, Rectangle bounds, Color leftColor, int leftWidth, ButtonBorderStyle leftStyle, Color topColor, int topWidth, ButtonBorderStyle topStyle, Color rightColor, int rightWidth, ButtonBorderStyle rightStyle, Color bottomColor, int bottomWidth, ButtonBorderStyle bottomStyle)
		{
			ThemeWin32Classic.DrawBorderInternal(graphics, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom - 1, leftWidth, leftColor, leftStyle, Border3DSide.Left);
			ThemeWin32Classic.DrawBorderInternal(graphics, bounds.Left, bounds.Top, bounds.Right - 1, bounds.Top, topWidth, topColor, topStyle, Border3DSide.Top);
			ThemeWin32Classic.DrawBorderInternal(graphics, bounds.Right - 1, bounds.Top, bounds.Right - 1, bounds.Bottom - 1, rightWidth, rightColor, rightStyle, Border3DSide.Right);
			ThemeWin32Classic.DrawBorderInternal(graphics, bounds.Left, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1, bottomWidth, bottomColor, bottomStyle, Border3DSide.Bottom);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00059A00 File Offset: 0x00057C00
		public override void CPDrawBorder3D(Graphics graphics, Rectangle rectangle, Border3DStyle style, Border3DSide sides)
		{
			this.CPDrawBorder3D(graphics, rectangle, style, sides, this.ColorControl);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00059A14 File Offset: 0x00057C14
		public override void CPDrawBorder3D(Graphics graphics, Rectangle rectangle, Border3DStyle style, Border3DSide sides, Color control_color)
		{
			Rectangle rectangle2 = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
			bool flag = control_color.ToArgb() == this.ColorControl.ToArgb();
			if ((style & Border3DStyle.Adjust) != (Border3DStyle)0)
			{
				rectangle2.Y -= 2;
				rectangle2.X -= 2;
				rectangle2.Width += 4;
				rectangle2.Height += 4;
			}
			Pen pen4;
			Pen pen3;
			Pen pen2;
			Pen pen = (pen2 = (pen3 = (pen4 = (flag ? SystemPens.Control : this.ResPool.GetPen(control_color)))));
			CPColor cpcolor = CPColor.Empty;
			if (!flag)
			{
				cpcolor = this.ResPool.GetCPColor(control_color);
			}
			switch (style)
			{
			case Border3DStyle.RaisedOuter:
				pen3 = (flag ? SystemPens.ControlDarkDark : this.ResPool.GetPen(cpcolor.DarkDark));
				break;
			case Border3DStyle.SunkenOuter:
				pen2 = (flag ? SystemPens.ControlDark : this.ResPool.GetPen(cpcolor.Dark));
				pen3 = (flag ? SystemPens.ControlLightLight : this.ResPool.GetPen(cpcolor.LightLight));
				break;
			case (Border3DStyle)3:
			case (Border3DStyle)7:
				break;
			case Border3DStyle.RaisedInner:
				pen2 = (flag ? SystemPens.ControlLightLight : this.ResPool.GetPen(cpcolor.LightLight));
				pen3 = (flag ? SystemPens.ControlDark : this.ResPool.GetPen(cpcolor.Dark));
				break;
			case Border3DStyle.Raised:
				pen = (flag ? SystemPens.ControlLightLight : this.ResPool.GetPen(cpcolor.LightLight));
				pen3 = (flag ? SystemPens.ControlDarkDark : this.ResPool.GetPen(cpcolor.DarkDark));
				pen4 = (flag ? SystemPens.ControlDark : this.ResPool.GetPen(cpcolor.Dark));
				break;
			case Border3DStyle.Etched:
				pen4 = (pen2 = (flag ? SystemPens.ControlDark : this.ResPool.GetPen(cpcolor.Dark)));
				pen3 = (pen = (flag ? SystemPens.ControlLightLight : this.ResPool.GetPen(cpcolor.LightLight)));
				break;
			case Border3DStyle.SunkenInner:
				pen2 = (flag ? SystemPens.ControlDarkDark : this.ResPool.GetPen(cpcolor.DarkDark));
				break;
			case Border3DStyle.Bump:
				pen3 = (pen = (flag ? SystemPens.ControlDarkDark : this.ResPool.GetPen(cpcolor.DarkDark)));
				break;
			case Border3DStyle.Sunken:
				pen2 = (flag ? SystemPens.ControlDark : this.ResPool.GetPen(cpcolor.Dark));
				pen = (flag ? SystemPens.ControlDarkDark : this.ResPool.GetPen(cpcolor.DarkDark));
				pen3 = (flag ? SystemPens.ControlLightLight : this.ResPool.GetPen(cpcolor.LightLight));
				break;
			default:
				if (style == Border3DStyle.Flat)
				{
					pen3 = (pen2 = (flag ? SystemPens.ControlDark : this.ResPool.GetPen(cpcolor.Dark)));
				}
				break;
			}
			bool flag2 = style != Border3DStyle.RaisedOuter && style != Border3DStyle.SunkenOuter;
			if ((sides & Border3DSide.Middle) != (Border3DSide)0)
			{
				Brush brush = (flag ? SystemBrushes.Control : this.ResPool.GetSolidBrush(control_color));
				graphics.FillRectangle(brush, rectangle2);
			}
			if ((sides & Border3DSide.Left) != (Border3DSide)0)
			{
				graphics.DrawLine(pen2, rectangle2.Left, rectangle2.Bottom - 2, rectangle2.Left, rectangle2.Top);
				if (rectangle2.Width > 2 && flag2)
				{
					graphics.DrawLine(pen, rectangle2.Left + 1, rectangle2.Bottom - 2, rectangle2.Left + 1, rectangle2.Top);
				}
			}
			if ((sides & Border3DSide.Top) != (Border3DSide)0)
			{
				graphics.DrawLine(pen2, rectangle2.Left, rectangle2.Top, rectangle2.Right - 2, rectangle2.Top);
				if (rectangle2.Height > 2 && flag2)
				{
					graphics.DrawLine(pen, rectangle2.Left + 1, rectangle2.Top + 1, rectangle2.Right - 3, rectangle2.Top + 1);
				}
			}
			if ((sides & Border3DSide.Right) != (Border3DSide)0)
			{
				graphics.DrawLine(pen3, rectangle2.Right - 1, rectangle2.Top, rectangle2.Right - 1, rectangle2.Bottom - 1);
				if (rectangle2.Width > 3 && flag2)
				{
					graphics.DrawLine(pen4, rectangle2.Right - 2, rectangle2.Top + 1, rectangle2.Right - 2, rectangle2.Bottom - 2);
				}
			}
			if ((sides & Border3DSide.Bottom) != (Border3DSide)0)
			{
				graphics.DrawLine(pen3, rectangle2.Left, rectangle2.Bottom - 1, rectangle2.Right - 1, rectangle2.Bottom - 1);
				if (rectangle2.Height > 3 && flag2)
				{
					graphics.DrawLine(pen4, rectangle2.Left + 1, rectangle2.Bottom - 2, rectangle2.Right - 2, rectangle2.Bottom - 2);
				}
			}
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00059EFF File Offset: 0x000580FF
		public override void CPDrawButton(Graphics dc, Rectangle rectangle, ButtonState state)
		{
			this.CPDrawButtonInternal(dc, rectangle, state, SystemPens.ControlDarkDark, SystemPens.ControlDark, SystemPens.ControlLight);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00059F1C File Offset: 0x0005811C
		private void CPDrawButtonInternal(Graphics dc, Rectangle rectangle, ButtonState state, Pen DarkPen, Pen NormalPen, Pen LightPen)
		{
			dc.FillRectangle(this.ResPool.GetHatchBrush(HatchStyle.Percent50, Color.FromArgb(base.Clamp((int)(this.ColorControl.R + 3), 0, 255), (int)this.ColorControl.G, (int)this.ColorControl.B), this.ColorControl), rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2);
			if ((state & ButtonState.All) == ButtonState.All || ((state & ButtonState.Checked) == ButtonState.Checked && (state & ButtonState.Flat) == ButtonState.Flat))
			{
				dc.FillRectangle(this.ResPool.GetHatchBrush(HatchStyle.Percent50, this.ColorControlLight, this.ColorControl), rectangle.X + 2, rectangle.Y + 2, rectangle.Width - 4, rectangle.Height - 4);
				dc.DrawRectangle(SystemPens.ControlDark, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				return;
			}
			if ((state & ButtonState.Flat) == ButtonState.Flat)
			{
				dc.DrawRectangle(SystemPens.ControlDark, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				return;
			}
			if ((state & ButtonState.Checked) == ButtonState.Checked)
			{
				dc.FillRectangle(this.ResPool.GetHatchBrush(HatchStyle.Percent50, this.ColorControlLight, this.ColorControl), rectangle.X + 2, rectangle.Y + 2, rectangle.Width - 4, rectangle.Height - 4);
				dc.DrawLine(DarkPen, rectangle.X, rectangle.Y, rectangle.X, rectangle.Bottom - 2);
				dc.DrawLine(DarkPen, rectangle.X + 1, rectangle.Y, rectangle.Right - 2, rectangle.Y);
				dc.DrawLine(NormalPen, rectangle.X + 1, rectangle.Y + 1, rectangle.X + 1, rectangle.Bottom - 3);
				dc.DrawLine(NormalPen, rectangle.X + 2, rectangle.Y + 1, rectangle.Right - 3, rectangle.Y + 1);
				dc.DrawLine(LightPen, rectangle.X, rectangle.Bottom - 1, rectangle.Right - 2, rectangle.Bottom - 1);
				dc.DrawLine(LightPen, rectangle.Right - 1, rectangle.Y, rectangle.Right - 1, rectangle.Bottom - 1);
				return;
			}
			if ((state & ButtonState.Pushed) == ButtonState.Pushed && (state & ButtonState.Normal) == ButtonState.Normal)
			{
				dc.DrawLine(DarkPen, rectangle.X, rectangle.Y, rectangle.X, rectangle.Bottom - 2);
				dc.DrawLine(DarkPen, rectangle.X + 1, rectangle.Y, rectangle.Right - 2, rectangle.Y);
				dc.DrawLine(NormalPen, rectangle.X + 1, rectangle.Y + 1, rectangle.X + 1, rectangle.Bottom - 3);
				dc.DrawLine(NormalPen, rectangle.X + 2, rectangle.Y + 1, rectangle.Right - 3, rectangle.Y + 1);
				dc.DrawLine(LightPen, rectangle.X, rectangle.Bottom - 1, rectangle.Right - 2, rectangle.Bottom - 1);
				dc.DrawLine(LightPen, rectangle.Right - 1, rectangle.Y, rectangle.Right - 1, rectangle.Bottom - 1);
				return;
			}
			if ((state & ButtonState.Inactive) == ButtonState.Inactive || (state & ButtonState.Normal) == ButtonState.Normal)
			{
				dc.DrawLine(LightPen, rectangle.X, rectangle.Y, rectangle.Right - 2, rectangle.Y);
				dc.DrawLine(LightPen, rectangle.X, rectangle.Y, rectangle.X, rectangle.Bottom - 2);
				dc.DrawLine(NormalPen, rectangle.X + 1, rectangle.Bottom - 2, rectangle.Right - 2, rectangle.Bottom - 2);
				dc.DrawLine(NormalPen, rectangle.Right - 2, rectangle.Y + 1, rectangle.Right - 2, rectangle.Bottom - 3);
				dc.DrawLine(DarkPen, rectangle.X, rectangle.Bottom - 1, rectangle.Right - 1, rectangle.Bottom - 1);
				dc.DrawLine(DarkPen, rectangle.Right - 1, rectangle.Y, rectangle.Right - 1, rectangle.Bottom - 2);
			}
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0005A3F0 File Offset: 0x000585F0
		public override void CPDrawCaptionButton(Graphics graphics, Rectangle rectangle, CaptionButton button, ButtonState state)
		{
			this.CPDrawButtonInternal(graphics, rectangle, state, SystemPens.ControlDarkDark, SystemPens.ControlDark, SystemPens.ControlLightLight);
			Rectangle rectangle2;
			if (rectangle.Width < rectangle.Height)
			{
				rectangle2 = new Rectangle(rectangle.X + 1, rectangle.Y + rectangle.Height / 2 - rectangle.Width / 2 + 1, rectangle.Width - 4, rectangle.Width - 4);
			}
			else
			{
				rectangle2 = new Rectangle(rectangle.X + rectangle.Width / 2 - rectangle.Height / 2 + 1, rectangle.Y + 1, rectangle.Height - 4, rectangle.Height - 4);
			}
			if ((state & ButtonState.Pushed) != ButtonState.Normal)
			{
				rectangle2 = new Rectangle(rectangle.X + 2, rectangle.Y + 2, rectangle.Width - 3, rectangle.Height - 3);
			}
			int num = Math.Max(1, rectangle2.Width / 7);
			if (button != CaptionButton.Close)
			{
				if (button - CaptionButton.Minimize > 3)
				{
					return;
				}
				if ((state & ButtonState.Inactive) != ButtonState.Normal)
				{
					this.DrawCaptionHelper(graphics, this.ColorControlLight, SystemPens.ControlLightLight, num, 1, rectangle2, button);
					this.DrawCaptionHelper(graphics, this.ColorControlDark, SystemPens.ControlDark, num, 0, rectangle2, button);
					return;
				}
				this.DrawCaptionHelper(graphics, this.ColorControlText, SystemPens.ControlText, num, 0, rectangle2, button);
				return;
			}
			else
			{
				Pen pen;
				if ((state & ButtonState.Inactive) != ButtonState.Normal)
				{
					pen = this.ResPool.GetSizedPen(this.ColorControlLight, num);
					this.DrawCaptionHelper(graphics, this.ColorControlLight, pen, num, 1, rectangle2, button);
					pen = this.ResPool.GetSizedPen(this.ColorControlDark, num);
					this.DrawCaptionHelper(graphics, this.ColorControlDark, pen, num, 0, rectangle2, button);
					return;
				}
				pen = this.ResPool.GetSizedPen(this.ColorControlText, num);
				this.DrawCaptionHelper(graphics, this.ColorControlText, pen, num, 0, rectangle2, button);
				return;
			}
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0005A5BD File Offset: 0x000587BD
		public override void CPDrawCheckBox(Graphics dc, Rectangle rectangle, ButtonState state)
		{
			this.CPDrawCheckBoxInternal(dc, rectangle, state, false);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0005A5CC File Offset: 0x000587CC
		private void CPDrawCheckBoxInternal(Graphics dc, Rectangle rectangle, ButtonState state, bool mixed)
		{
			Pen pen = (mixed ? Pens.Gray : Pens.Black);
			Rectangle rectangle2 = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
			if ((state & ButtonState.All) == ButtonState.All)
			{
				rectangle2.Width -= 2;
				rectangle2.Height -= 2;
				dc.FillRectangle(SystemBrushes.Control, rectangle2.X, rectangle2.Y, rectangle2.Width - 1, rectangle2.Height - 1);
				dc.DrawRectangle(SystemPens.ControlDark, rectangle2.X, rectangle2.Y, rectangle2.Width - 1, rectangle2.Height - 1);
				pen = SystemPens.ControlDark;
			}
			else if ((state & ButtonState.Flat) == ButtonState.Flat)
			{
				rectangle2.Width -= 2;
				rectangle2.Height -= 2;
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					dc.FillRectangle(SystemBrushes.ControlLight, rectangle2.X, rectangle2.Y, rectangle2.Width - 1, rectangle2.Height - 1);
				}
				else
				{
					dc.FillRectangle(Brushes.White, rectangle2.X, rectangle2.Y, rectangle2.Width - 1, rectangle2.Height - 1);
				}
				dc.DrawRectangle(SystemPens.ControlDark, rectangle2.X, rectangle2.Y, rectangle2.Width - 1, rectangle2.Height - 1);
			}
			else
			{
				rectangle2.Width--;
				rectangle2.Height--;
				int num = ((rectangle2.Height > rectangle2.Width) ? rectangle2.Width : rectangle2.Height);
				int num2 = Math.Max(0, rectangle2.X + rectangle2.Width / 2 - num / 2);
				int num3 = Math.Max(0, rectangle2.Y + rectangle2.Height / 2 - num / 2);
				Rectangle rectangle3 = new Rectangle(num2, num3, num, num);
				if ((state & ButtonState.Pushed) == ButtonState.Pushed || (state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					dc.FillRectangle(this.ResPool.GetHatchBrush(HatchStyle.Percent50, Color.FromArgb(base.Clamp((int)(this.ColorControl.R + 3), 0, 255), (int)this.ColorControl.G, (int)this.ColorControl.B), this.ColorControl), rectangle3.X + 2, rectangle3.Y + 2, rectangle3.Width - 3, rectangle3.Height - 3);
				}
				else
				{
					dc.FillRectangle(SystemBrushes.ControlLightLight, rectangle3.X + 2, rectangle3.Y + 2, rectangle3.Width - 3, rectangle3.Height - 3);
				}
				Pen pen2 = SystemPens.ControlDark;
				dc.DrawLine(pen2, rectangle3.X, rectangle3.Y, rectangle3.X, rectangle3.Bottom - 1);
				dc.DrawLine(pen2, rectangle3.X + 1, rectangle3.Y, rectangle3.Right - 1, rectangle3.Y);
				pen2 = SystemPens.ControlDarkDark;
				dc.DrawLine(pen2, rectangle3.X + 1, rectangle3.Y + 1, rectangle3.X + 1, rectangle3.Bottom - 2);
				dc.DrawLine(pen2, rectangle3.X + 2, rectangle3.Y + 1, rectangle3.Right - 2, rectangle3.Y + 1);
				pen2 = SystemPens.ControlLightLight;
				dc.DrawLine(pen2, rectangle3.Right, rectangle3.Y, rectangle3.Right, rectangle3.Bottom);
				dc.DrawLine(pen2, rectangle3.X, rectangle3.Bottom, rectangle3.Right, rectangle3.Bottom);
				using (Pen pen3 = new Pen(this.ResPool.GetHatchBrush(HatchStyle.Percent50, Color.FromArgb(base.Clamp((int)(this.ColorControl.R + 3), 0, 255), (int)this.ColorControl.G, (int)this.ColorControl.B), this.ColorControl)))
				{
					dc.DrawLine(pen3, rectangle3.X + 1, rectangle3.Bottom - 1, rectangle3.Right - 1, rectangle3.Bottom - 1);
					dc.DrawLine(pen3, rectangle3.Right - 1, rectangle3.Y + 1, rectangle3.Right - 1, rectangle3.Bottom - 1);
				}
				if ((state & ButtonState.Inactive) == ButtonState.Inactive)
				{
					pen = SystemPens.ControlDark;
				}
			}
			if ((state & ButtonState.Checked) == ButtonState.Checked)
			{
				int num4 = ((rectangle2.Height > rectangle2.Width) ? (rectangle2.Width / 2) : (rectangle2.Height / 2));
				if (num4 < 7)
				{
					int num5 = Math.Max(3, num4 / 3);
					int num6 = Math.Max(1, num4 / 9);
					Rectangle rectangle4 = new Rectangle(rectangle2.X + rectangle2.Width / 2 - (int)Math.Ceiling((double)((float)num4 / 2f)) - 1, rectangle2.Y + rectangle2.Height / 2 - num4 / 2 - 1, num4, num4);
					for (int i = 0; i < num5; i++)
					{
						dc.DrawLine(pen, rectangle4.Left + num5 / 2, rectangle4.Top + num5 + i, rectangle4.Left + num5 / 2 + 2 * num6, rectangle4.Top + num5 + 2 * num6 + i);
						dc.DrawLine(pen, rectangle4.Left + num5 / 2 + 2 * num6, rectangle4.Top + num5 + 2 * num6 + i, rectangle4.Left + num5 / 2 + 6 * num6, rectangle4.Top + num5 - 2 * num6 + i);
					}
					return;
				}
				int num7 = Math.Max(3, num4 / 3) + 1;
				int num8 = rectangle2.Width / 2;
				int num9 = rectangle2.Height / 2;
				Rectangle rectangle5 = new Rectangle(rectangle2.X + num8 - num4 / 2 - 1, rectangle2.Y + num9 - num4 / 2, num4, num4);
				int num10 = num4 / 3;
				int num11 = num4 - num10 - 1;
				for (int j = 0; j < num7; j++)
				{
					dc.DrawLine(pen, rectangle5.X, rectangle5.Bottom - 1 - num10 - j, rectangle5.X + num10, rectangle5.Bottom - 1 - j);
					dc.DrawLine(pen, rectangle5.X + num10, rectangle5.Bottom - 1 - j, rectangle5.Right - 1, rectangle5.Bottom - j - 1 - num11);
				}
			}
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0005ACB4 File Offset: 0x00058EB4
		public override void CPDrawComboButton(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			Point[] array = new Point[3];
			if ((state & ButtonState.Checked) != ButtonState.Normal)
			{
				graphics.FillRectangle(this.ResPool.GetHatchBrush(HatchStyle.Percent50, this.ColorControlLightLight, this.ColorControlLight), rectangle);
			}
			if ((state & ButtonState.Flat) != ButtonState.Normal)
			{
				ControlPaint.DrawBorder(graphics, rectangle, this.ColorControlDark, ButtonBorderStyle.Solid);
			}
			else if ((state & (ButtonState.Pushed | ButtonState.Checked)) != ButtonState.Normal)
			{
				Rectangle rectangle2 = new Rectangle(rectangle.X, rectangle.Y, Math.Max(rectangle.Width - 1, 0), Math.Max(rectangle.Height - 1, 0));
				graphics.DrawRectangle(SystemPens.ControlDark, rectangle2);
			}
			else
			{
				this.CPDrawBorder3D(graphics, rectangle, Border3DStyle.Raised, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom, this.ColorControl);
			}
			Rectangle rectangle3 = new Rectangle(rectangle.X + rectangle.Width / 4, rectangle.Y + rectangle.Height / 4, rectangle.Width / 2, rectangle.Height / 2);
			int num = rectangle3.Left + rectangle3.Width / 2;
			int num2 = rectangle3.Top + rectangle3.Height / 2;
			int num3 = Math.Max(1, rectangle3.Width / 8);
			int num4 = Math.Max(1, rectangle3.Height / 8);
			if ((state & ButtonState.Pushed) != ButtonState.Normal)
			{
				num3++;
				num4++;
			}
			rectangle3.Y -= num4;
			num2 -= num4;
			Point point = new Point(rectangle3.Left, num2);
			Point point2 = new Point(rectangle3.Right, num2);
			Point point3 = new Point(num, rectangle3.Bottom);
			array[0] = point;
			array[1] = point2;
			array[2] = point3;
			if ((state & ButtonState.Inactive) != ButtonState.Normal)
			{
				Point[] array2 = array;
				int num5 = 0;
				array2[num5].X = array2[num5].X + 1;
				Point[] array3 = array;
				int num6 = 0;
				array3[num6].Y = array3[num6].Y + 1;
				Point[] array4 = array;
				int num7 = 1;
				array4[num7].X = array4[num7].X + 1;
				Point[] array5 = array;
				int num8 = 1;
				array5[num8].Y = array5[num8].Y + 1;
				Point[] array6 = array;
				int num9 = 2;
				array6[num9].X = array6[num9].X + 1;
				Point[] array7 = array;
				int num10 = 2;
				array7[num10].Y = array7[num10].Y + 1;
				graphics.FillPolygon(SystemBrushes.ControlLightLight, array, FillMode.Winding);
				array[0] = point;
				array[1] = point2;
				array[2] = point3;
				graphics.FillPolygon(SystemBrushes.ControlDark, array, FillMode.Winding);
				return;
			}
			graphics.FillPolygon(SystemBrushes.ControlText, array, FillMode.Winding);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0005AF24 File Offset: 0x00059124
		public virtual void DrawInnerFocusRectangle(Graphics graphics, Rectangle rectangle, Color backColor)
		{
			Rectangle rectangle2 = new Rectangle(rectangle.X, rectangle.Y, Math.Max(rectangle.Width - 1, 0), Math.Max(rectangle.Height - 1, 0));
			this.CPDrawFocusRectangle(graphics, rectangle2, Color.Wheat, backColor);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0005AF74 File Offset: 0x00059174
		public override void CPDrawFocusRectangle(Graphics graphics, Rectangle rectangle, Color foreColor, Color backColor)
		{
			Rectangle rectangle2 = rectangle;
			if ((double)backColor.GetBrightness() >= 0.5)
			{
				foreColor = Color.Transparent;
				backColor = Color.Black;
			}
			else
			{
				backColor = Color.FromArgb(Math.Abs((int)(backColor.R - byte.MaxValue)), Math.Abs((int)(backColor.G - byte.MaxValue)), Math.Abs((int)(backColor.B - byte.MaxValue)));
				foreColor = Color.Black;
			}
			Pen pen = new Pen(this.ResPool.GetHatchBrush(HatchStyle.Percent50, backColor, foreColor), 1f);
			int num = rectangle2.Width;
			rectangle2.Width = num - 1;
			num = rectangle2.Height;
			rectangle2.Height = num - 1;
			graphics.DrawRectangle(pen, rectangle2);
			pen.Dispose();
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0005B038 File Offset: 0x00059238
		public override void CPDrawImageDisabled(Graphics graphics, Image image, int x, int y, Color background)
		{
			if (ThemeWin32Classic.imagedisabled_attributes == null)
			{
				ThemeWin32Classic.imagedisabled_attributes = new ImageAttributes();
				ColorMatrix colorMatrix = new ColorMatrix(new float[][]
				{
					new float[] { 0.2f, 0.2f, 0.2f, 0f, 0f },
					new float[] { 0.41f, 0.41f, 0.41f, 0f, 0f },
					new float[] { 0.11f, 0.11f, 0.11f, 0f, 0f },
					new float[] { 0.15f, 0.15f, 0.15f, 1f, 0f, 0f },
					new float[] { 0.15f, 0.15f, 0.15f, 0f, 1f, 0f },
					new float[] { 0.15f, 0.15f, 0.15f, 0f, 0f, 1f }
				});
				ThemeWin32Classic.imagedisabled_attributes.SetColorMatrix(colorMatrix);
			}
			graphics.DrawImage(image, new Rectangle(x, y, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, ThemeWin32Classic.imagedisabled_attributes);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0005B118 File Offset: 0x00059318
		public override void CPDrawMenuGlyph(Graphics graphics, Rectangle rectangle, MenuGlyph glyph, Color color, Color backColor)
		{
			if (backColor != Color.Empty)
			{
				graphics.FillRectangle(this.ResPool.GetSolidBrush(backColor), rectangle);
			}
			Brush solidBrush = this.ResPool.GetSolidBrush(color);
			switch (glyph)
			{
			case MenuGlyph.Arrow:
			{
				float num = (float)rectangle.Height * 0.7f;
				float num2 = num / 2f;
				PointF pointF = new PointF((float)rectangle.X + ((float)rectangle.Width - num2) / 2f, (float)rectangle.Y + (float)rectangle.Height / 2f);
				PointF[] array = new PointF[3];
				array[0].X = pointF.X;
				array[0].Y = pointF.Y - num / 2f;
				array[1].X = pointF.X;
				array[1].Y = pointF.Y + num / 2f;
				array[2].X = pointF.X + num2 + 0.1f;
				array[2].Y = pointF.Y;
				graphics.FillPolygon(solidBrush, array);
				return;
			}
			case MenuGlyph.Checkmark:
			{
				Pen pen = this.ResPool.GetPen(color);
				int num3 = Math.Max(2, rectangle.Width / 6);
				Rectangle rectangle2 = new Rectangle(rectangle.X + num3, rectangle.Y + num3, rectangle.Width - num3 * 2, rectangle.Height - num3 * 2);
				int num4 = Math.Max(1, rectangle.Width / 12);
				int num5 = rectangle2.Y + num3 + (rectangle2.Height - (2 * num4 + num3)) / 2;
				for (int i = 0; i < num3; i++)
				{
					graphics.DrawLine(pen, rectangle2.Left + num3 / 2, num5 + i, rectangle2.Left + num3 / 2 + 2 * num4, num5 + 2 * num4 + i);
					graphics.DrawLine(pen, rectangle2.Left + num3 / 2 + 2 * num4, num5 + 2 * num4 + i, rectangle2.Left + num3 / 2 + 6 * num4, num5 - 2 * num4 + i);
				}
				return;
			}
			case MenuGlyph.Bullet:
			{
				int num3 = Math.Max(2, rectangle.Width / 3);
				Rectangle rectangle2 = new Rectangle(rectangle.X + num3, rectangle.Y + num3, rectangle.Width - num3 * 2, rectangle.Height - num3 * 2);
				graphics.FillEllipse(solidBrush, rectangle2);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0005B39D File Offset: 0x0005959D
		public override void CPDrawMixedCheckBox(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			this.CPDrawCheckBoxInternal(graphics, rectangle, state, true);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0005B3AC File Offset: 0x000595AC
		public override void CPDrawScrollButton(Graphics dc, Rectangle area, ScrollButton type, ButtonState state)
		{
			this.DrawScrollButtonPrimitive(dc, area, state);
			bool flag = true;
			int num = 0;
			if ((state & ButtonState.Pushed) != ButtonState.Normal)
			{
				num = 1;
			}
			Rectangle rectangle = new Rectangle(area.X + 2 + num, area.Y + 2 + num, area.Width - 4, area.Height - 4);
			Point[] array = new Point[3];
			for (int i = 0; i < 3; i++)
			{
				array[i] = default(Point);
			}
			Pen pen = SystemPens.ControlText;
			if ((state & ButtonState.Inactive) != ButtonState.Normal)
			{
				pen = SystemPens.ControlDark;
			}
			switch (type)
			{
			case ScrollButton.Min:
			{
				int num2 = (int)Math.Round((double)((float)rectangle.Width / 2f)) - 1;
				int num3 = (int)Math.Round((double)((float)rectangle.Height / 2f));
				if (num2 == 1)
				{
					num2 = 2;
				}
				if (num3 == 1)
				{
					num3 = 2;
				}
				int num4;
				if (rectangle.Height < 8)
				{
					num4 = 2;
					flag = false;
				}
				else if (rectangle.Height == 11)
				{
					num4 = 3;
				}
				else
				{
					num4 = (int)Math.Round((double)((float)rectangle.Height / 3f));
				}
				array[0].X = rectangle.X + num2;
				array[0].Y = rectangle.Y + num3 - num4 / 2;
				array[1].X = array[0].X + num4 - 1;
				array[1].Y = array[0].Y + num4 - 1;
				array[2].X = array[0].X - num4 + 1;
				array[2].Y = array[1].Y;
				dc.DrawPolygon(pen, array);
				if ((state & ButtonState.Inactive) != ButtonState.Normal)
				{
					dc.DrawLine(SystemPens.ControlLightLight, array[1].X + 1, array[1].Y + 1, array[2].X + 1, array[1].Y + 1);
				}
				if (flag)
				{
					for (int j = 0; j < array[1].Y - array[0].Y; j++)
					{
						dc.DrawLine(pen, array[2].X, array[1].Y - j, array[1].X, array[1].Y - j);
						Point[] array2 = array;
						int num5 = 1;
						array2[num5].X = array2[num5].X - 1;
						Point[] array3 = array;
						int num6 = 2;
						array3[num6].X = array3[num6].X + 1;
					}
					return;
				}
				break;
			}
			default:
			{
				int num2 = (int)Math.Round((double)((float)rectangle.Width / 2f)) - 1;
				int num3 = (int)Math.Round((double)((float)rectangle.Height / 2f)) - 1;
				if (num2 == 1)
				{
					num2 = 2;
				}
				int num4;
				if (rectangle.Height < 8)
				{
					num4 = 2;
					flag = false;
				}
				else if (rectangle.Height == 11)
				{
					num4 = 3;
				}
				else
				{
					num4 = (int)Math.Round((double)((float)rectangle.Height / 3f));
				}
				array[0].X = rectangle.X + num2;
				array[0].Y = rectangle.Y + num3 + num4 / 2;
				array[1].X = array[0].X + num4 - 1;
				array[1].Y = array[0].Y - num4 + 1;
				array[2].X = array[0].X - num4 + 1;
				array[2].Y = array[1].Y;
				dc.DrawPolygon(pen, array);
				if ((state & ButtonState.Inactive) != ButtonState.Normal)
				{
					dc.DrawLine(SystemPens.ControlLightLight, array[1].X + 1, array[1].Y + 1, array[0].X + 1, array[0].Y + 1);
					dc.DrawLine(SystemPens.ControlLightLight, array[1].X, array[1].Y + 1, array[0].X + 1, array[0].Y);
				}
				if (flag)
				{
					for (int k = 0; k < array[0].Y - array[1].Y; k++)
					{
						dc.DrawLine(pen, array[1].X, array[1].Y + k, array[2].X, array[1].Y + k);
						Point[] array4 = array;
						int num7 = 1;
						array4[num7].X = array4[num7].X - 1;
						Point[] array5 = array;
						int num8 = 2;
						array5[num8].X = array5[num8].X + 1;
					}
					return;
				}
				break;
			}
			case ScrollButton.Left:
			{
				int num3 = (int)Math.Round((double)((float)rectangle.Height / 2f)) - 1;
				if (num3 == 1)
				{
					num3 = 2;
				}
				int num9;
				if (rectangle.Width < 8)
				{
					num9 = 2;
					flag = false;
				}
				else if (rectangle.Width == 11)
				{
					num9 = 3;
				}
				else
				{
					num9 = (int)Math.Round((double)((float)rectangle.Width / 3f));
				}
				array[0].X = rectangle.Left + num9 - 1;
				array[0].Y = rectangle.Y + num3;
				if (array[0].X - 1 == rectangle.X)
				{
					Point[] array6 = array;
					int num10 = 0;
					array6[num10].X = array6[num10].X + 1;
				}
				array[1].X = array[0].X + num9 - 1;
				array[1].Y = array[0].Y - num9 + 1;
				array[2].X = array[1].X;
				array[2].Y = array[0].Y + num9 - 1;
				dc.DrawPolygon(pen, array);
				if ((state & ButtonState.Inactive) != ButtonState.Normal)
				{
					dc.DrawLine(SystemPens.ControlLightLight, array[1].X + 1, array[1].Y + 1, array[2].X + 1, array[2].Y + 1);
				}
				if (flag)
				{
					for (int l = 0; l < array[2].X - array[0].X; l++)
					{
						dc.DrawLine(pen, array[2].X - l, array[1].Y, array[2].X - l, array[2].Y);
						Point[] array7 = array;
						int num11 = 1;
						array7[num11].Y = array7[num11].Y + 1;
						Point[] array8 = array;
						int num12 = 2;
						array8[num12].Y = array8[num12].Y - 1;
					}
					return;
				}
				break;
			}
			case ScrollButton.Right:
			{
				int num3 = (int)Math.Round((double)((float)rectangle.Height / 2f)) - 1;
				if (num3 == 1)
				{
					num3 = 2;
				}
				int num9;
				if (rectangle.Width < 8)
				{
					num9 = 2;
					flag = false;
				}
				else if (rectangle.Width == 11)
				{
					num9 = 3;
				}
				else
				{
					num9 = (int)Math.Round((double)((float)rectangle.Width / 3f));
				}
				array[0].X = rectangle.Right - num9 - 1;
				array[0].Y = rectangle.Y + num3;
				if (array[0].X - 1 == rectangle.X)
				{
					Point[] array9 = array;
					int num13 = 0;
					array9[num13].X = array9[num13].X + 1;
				}
				array[1].X = array[0].X - num9 + 1;
				array[1].Y = array[0].Y - num9 + 1;
				array[2].X = array[1].X;
				array[2].Y = array[0].Y + num9 - 1;
				dc.DrawPolygon(pen, array);
				if ((state & ButtonState.Inactive) != ButtonState.Normal)
				{
					dc.DrawLine(SystemPens.ControlLightLight, array[0].X + 1, array[0].Y + 1, array[2].X + 1, array[2].Y + 1);
					dc.DrawLine(SystemPens.ControlLightLight, array[0].X, array[0].Y + 1, array[2].X + 1, array[2].Y);
				}
				if (flag)
				{
					for (int m = 0; m < array[0].X - array[1].X; m++)
					{
						dc.DrawLine(pen, array[2].X + m, array[1].Y, array[2].X + m, array[2].Y);
						Point[] array10 = array;
						int num14 = 1;
						array10[num14].Y = array10[num14].Y + 1;
						Point[] array11 = array;
						int num15 = 2;
						array11[num15].Y = array11[num15].Y - 1;
					}
				}
				break;
			}
			}
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0005BCFC File Offset: 0x00059EFC
		public override void CPDrawSizeGrip(Graphics dc, Color backColor, Rectangle bounds)
		{
			Pen pen = this.ResPool.GetPen(ControlPaint.Dark(backColor));
			Pen pen2 = this.ResPool.GetPen(ControlPaint.LightLight(backColor));
			for (int i = 2; i < bounds.Width - 2; i += 4)
			{
				dc.DrawLine(pen2, bounds.X + i, bounds.Bottom - 2, bounds.Right - 1, bounds.Y + i - 1);
				dc.DrawLine(pen, bounds.X + i + 1, bounds.Bottom - 2, bounds.Right - 1, bounds.Y + i);
				dc.DrawLine(pen, bounds.X + i + 2, bounds.Bottom - 2, bounds.Right - 1, bounds.Y + i + 1);
			}
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0005BDD0 File Offset: 0x00059FD0
		private void DrawStringDisabled20(Graphics g, string s, Font font, Rectangle layoutRectangle, Color color, TextFormatFlags flags, bool useDrawString)
		{
			CPColor cpcolor = this.ResPool.GetCPColor(color);
			layoutRectangle.Offset(1, 1);
			TextRenderer.DrawTextInternal(g, s, font, layoutRectangle, cpcolor.LightLight, flags, useDrawString);
			layoutRectangle.Offset(-1, -1);
			TextRenderer.DrawTextInternal(g, s, font, layoutRectangle, cpcolor.Dark, flags, useDrawString);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0005BE28 File Offset: 0x0005A028
		public override void CPDrawStringDisabled(Graphics dc, string s, Font font, Color color, RectangleF layoutRectangle, StringFormat format)
		{
			CPColor cpcolor = this.ResPool.GetCPColor(color);
			dc.DrawString(s, font, this.ResPool.GetSolidBrush(cpcolor.LightLight), new RectangleF(layoutRectangle.X + 1f, layoutRectangle.Y + 1f, layoutRectangle.Width, layoutRectangle.Height), format);
			dc.DrawString(s, font, this.ResPool.GetSolidBrush(cpcolor.Dark), layoutRectangle, format);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0005BEA8 File Offset: 0x0005A0A8
		private static void DrawBorderInternal(Graphics graphics, int startX, int startY, int endX, int endY, int width, Color color, ButtonBorderStyle style, Border3DSide side)
		{
			ThemeWin32Classic.DrawBorderInternal(graphics, (float)startX, (float)startY, (float)endX, (float)endY, width, color, style, side);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0005BECC File Offset: 0x0005A0CC
		private static void DrawBorderInternal(Graphics graphics, float startX, float startY, float endX, float endY, int width, Color color, ButtonBorderStyle style, Border3DSide side)
		{
			Pen pen;
			switch (style)
			{
			case ButtonBorderStyle.None:
				return;
			case ButtonBorderStyle.Dotted:
				pen = ThemeEngine.Current.ResPool.GetDashPen(color, DashStyle.Dot);
				break;
			case ButtonBorderStyle.Dashed:
				pen = ThemeEngine.Current.ResPool.GetDashPen(color, DashStyle.Dash);
				break;
			case ButtonBorderStyle.Solid:
			case ButtonBorderStyle.Inset:
			case ButtonBorderStyle.Outset:
				pen = ThemeEngine.Current.ResPool.GetDashPen(color, DashStyle.Solid);
				break;
			default:
				return;
			}
			if (style == ButtonBorderStyle.Inset)
			{
				int num;
				int num2;
				int num3;
				ControlPaint.Color2HBS(color, out num, out num2, out num3);
				int num4 = num2 / width;
				int num5;
				if (num2 > 127)
				{
					num5 = Math.Max(6, (160 - num2) / width);
				}
				else
				{
					num5 = (127 - num2) / width;
				}
				for (int i = 0; i < width; i++)
				{
					switch (side)
					{
					case Border3DSide.Left:
					{
						Color color2 = ControlPaint.HBS2Color(num, Math.Max(0, num2 - num4 * (width - i)), num3);
						pen = ThemeEngine.Current.ResPool.GetPen(color2);
						graphics.DrawLine(pen, startX + (float)i, startY + (float)i, endX + (float)i, endY - (float)i);
						break;
					}
					case Border3DSide.Top:
					{
						Color color2 = ControlPaint.HBS2Color(num, Math.Max(0, num2 - num4 * (width - i)), num3);
						pen = ThemeEngine.Current.ResPool.GetPen(color2);
						graphics.DrawLine(pen, startX + (float)i, startY + (float)i, endX - (float)i, endY + (float)i);
						break;
					}
					case Border3DSide.Left | Border3DSide.Top:
						break;
					case Border3DSide.Right:
					{
						Color color2 = ControlPaint.HBS2Color(num, Math.Min(255, num2 + num5 * (width - i)), num3);
						pen = ThemeEngine.Current.ResPool.GetPen(color2);
						graphics.DrawLine(pen, startX - (float)i, startY + (float)i, endX - (float)i, endY - (float)i);
						break;
					}
					default:
						if (side == Border3DSide.Bottom)
						{
							Color color2 = ControlPaint.HBS2Color(num, Math.Min(255, num2 + num5 * (width - i)), num3);
							pen = ThemeEngine.Current.ResPool.GetPen(color2);
							graphics.DrawLine(pen, startX + (float)i, startY - (float)i, endX - (float)i, endY - (float)i);
						}
						break;
					}
				}
				return;
			}
			if (style == ButtonBorderStyle.Outset)
			{
				int num6;
				int num7;
				int num8;
				ControlPaint.Color2HBS(color, out num6, out num7, out num8);
				int num9 = num7 / width;
				int num10;
				if (num7 > 127)
				{
					num10 = Math.Max(6, (160 - num7) / width);
				}
				else
				{
					num10 = (127 - num7) / width;
				}
				for (int j = 0; j < width; j++)
				{
					switch (side)
					{
					case Border3DSide.Left:
					{
						Color color3 = ControlPaint.HBS2Color(num6, Math.Min(255, num7 + num10 * (width - j)), num8);
						pen = ThemeEngine.Current.ResPool.GetPen(color3);
						graphics.DrawLine(pen, startX + (float)j, startY + (float)j, endX + (float)j, endY - (float)j);
						break;
					}
					case Border3DSide.Top:
					{
						Color color3 = ControlPaint.HBS2Color(num6, Math.Min(255, num7 + num10 * (width - j)), num8);
						pen = ThemeEngine.Current.ResPool.GetPen(color3);
						graphics.DrawLine(pen, startX + (float)j, startY + (float)j, endX - (float)j, endY + (float)j);
						break;
					}
					case Border3DSide.Left | Border3DSide.Top:
						break;
					case Border3DSide.Right:
					{
						Color color3 = ControlPaint.HBS2Color(num6, Math.Max(0, num7 - num9 * (width - j)), num8);
						pen = ThemeEngine.Current.ResPool.GetPen(color3);
						graphics.DrawLine(pen, startX - (float)j, startY + (float)j, endX - (float)j, endY - (float)j);
						break;
					}
					default:
						if (side == Border3DSide.Bottom)
						{
							Color color3 = ControlPaint.HBS2Color(num6, Math.Max(0, num7 - num9 * (width - j)), num8);
							pen = ThemeEngine.Current.ResPool.GetPen(color3);
							graphics.DrawLine(pen, startX + (float)j, startY - (float)j, endX - (float)j, endY - (float)j);
						}
						break;
					}
				}
				return;
			}
			switch (side)
			{
			case Border3DSide.Left:
			{
				for (int k = 0; k < width; k++)
				{
					graphics.DrawLine(pen, startX + (float)k, startY + (float)k, endX + (float)k, endY - (float)k);
				}
				return;
			}
			case Border3DSide.Top:
			{
				for (int l = 0; l < width; l++)
				{
					graphics.DrawLine(pen, startX + (float)l, startY + (float)l, endX - (float)l, endY + (float)l);
				}
				return;
			}
			case Border3DSide.Left | Border3DSide.Top:
				break;
			case Border3DSide.Right:
			{
				for (int m = 0; m < width; m++)
				{
					graphics.DrawLine(pen, startX - (float)m, startY + (float)m, endX - (float)m, endY - (float)m);
				}
				return;
			}
			default:
			{
				if (side != Border3DSide.Bottom)
				{
					return;
				}
				for (int n = 0; n < width; n++)
				{
					graphics.DrawLine(pen, startX + (float)n, startY - (float)n, endX - (float)n, endY - (float)n);
				}
				break;
			}
			}
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0005C378 File Offset: 0x0005A578
		private void DrawCaptionHelper(Graphics graphics, Color color, Pen pen, int lineWidth, int shift, Rectangle captionRect, CaptionButton button)
		{
			switch (button)
			{
			case CaptionButton.Close:
				if (lineWidth < 2)
				{
					graphics.DrawLine(pen, captionRect.Left + 2 * lineWidth + 1 + shift, captionRect.Top + 2 * lineWidth + shift, captionRect.Right - 2 * lineWidth + 1 + shift, captionRect.Bottom - 2 * lineWidth + shift);
					graphics.DrawLine(pen, captionRect.Right - 2 * lineWidth + 1 + shift, captionRect.Top + 2 * lineWidth + shift, captionRect.Left + 2 * lineWidth + 1 + shift, captionRect.Bottom - 2 * lineWidth + shift);
				}
				graphics.DrawLine(pen, captionRect.Left + 2 * lineWidth + shift, captionRect.Top + 2 * lineWidth + shift, captionRect.Right - 2 * lineWidth + shift, captionRect.Bottom - 2 * lineWidth + shift);
				graphics.DrawLine(pen, captionRect.Right - 2 * lineWidth + shift, captionRect.Top + 2 * lineWidth + shift, captionRect.Left + 2 * lineWidth + shift, captionRect.Bottom - 2 * lineWidth + shift);
				return;
			case CaptionButton.Minimize:
			{
				for (int i = 0; i < Math.Max(2, lineWidth); i++)
				{
					graphics.DrawLine(pen, captionRect.Left + lineWidth + shift, captionRect.Bottom - lineWidth + shift - i, captionRect.Right - 3 * lineWidth + shift, captionRect.Bottom - lineWidth + shift - i);
				}
				return;
			}
			case CaptionButton.Maximize:
			{
				for (int j = 0; j < Math.Max(2, lineWidth); j++)
				{
					graphics.DrawLine(pen, captionRect.Left + lineWidth + shift, captionRect.Top + 2 * lineWidth + shift + j, captionRect.Right - lineWidth - lineWidth / 2 + shift, captionRect.Top + 2 * lineWidth + shift + j);
				}
				for (int k = 0; k < Math.Max(1, lineWidth / 2); k++)
				{
					graphics.DrawLine(pen, captionRect.Left + lineWidth + shift + k, captionRect.Top + 2 * lineWidth + shift, captionRect.Left + lineWidth + shift + k, captionRect.Bottom - lineWidth + shift);
				}
				for (int l = 0; l < Math.Max(1, lineWidth / 2); l++)
				{
					graphics.DrawLine(pen, captionRect.Right - lineWidth - lineWidth / 2 + shift + l, captionRect.Top + 2 * lineWidth + shift, captionRect.Right - lineWidth - lineWidth / 2 + shift + l, captionRect.Bottom - lineWidth + shift);
				}
				for (int m = 0; m < Math.Max(1, lineWidth / 2); m++)
				{
					graphics.DrawLine(pen, captionRect.Left + lineWidth + shift, captionRect.Bottom - lineWidth + shift - m, captionRect.Right - lineWidth - lineWidth / 2 + shift, captionRect.Bottom - lineWidth + shift - m);
				}
				return;
			}
			case CaptionButton.Restore:
			{
				for (int n = 0; n < Math.Max(2, lineWidth); n++)
				{
					graphics.DrawLine(pen, captionRect.Left + 3 * lineWidth + shift, captionRect.Top + 2 * lineWidth + shift - n, captionRect.Right - lineWidth - lineWidth / 2 + shift, captionRect.Top + 2 * lineWidth + shift - n);
				}
				for (int num = 0; num < Math.Max(1, lineWidth / 2); num++)
				{
					graphics.DrawLine(pen, captionRect.Left + 3 * lineWidth + shift + num, captionRect.Top + 2 * lineWidth + shift, captionRect.Left + 3 * lineWidth + shift + num, captionRect.Top + 4 * lineWidth + shift);
				}
				for (int num2 = 0; num2 < Math.Max(1, lineWidth / 2); num2++)
				{
					graphics.DrawLine(pen, captionRect.Right - lineWidth - lineWidth / 2 + shift - num2, captionRect.Top + 2 * lineWidth + shift, captionRect.Right - lineWidth - lineWidth / 2 + shift - num2, captionRect.Top + 5 * lineWidth - lineWidth / 2 + shift);
				}
				for (int num3 = 0; num3 < Math.Max(1, lineWidth / 2); num3++)
				{
					graphics.DrawLine(pen, captionRect.Right - 3 * lineWidth - lineWidth / 2 + shift, captionRect.Top + 5 * lineWidth - lineWidth / 2 + shift + 1 + num3, captionRect.Right - lineWidth - lineWidth / 2 + shift, captionRect.Top + 5 * lineWidth - lineWidth / 2 + shift + 1 + num3);
				}
				for (int num4 = 0; num4 < Math.Max(2, lineWidth); num4++)
				{
					graphics.DrawLine(pen, captionRect.Left + lineWidth + shift, captionRect.Top + 4 * lineWidth + shift + 1 - num4, captionRect.Right - 3 * lineWidth - lineWidth / 2 + shift, captionRect.Top + 4 * lineWidth + shift + 1 - num4);
				}
				for (int num5 = 0; num5 < Math.Max(1, lineWidth / 2); num5++)
				{
					graphics.DrawLine(pen, captionRect.Left + lineWidth + shift + num5, captionRect.Top + 4 * lineWidth + shift + 1, captionRect.Left + lineWidth + shift + num5, captionRect.Bottom - lineWidth + shift);
				}
				for (int num6 = 0; num6 < Math.Max(1, lineWidth / 2); num6++)
				{
					graphics.DrawLine(pen, captionRect.Right - 3 * lineWidth - lineWidth / 2 + shift - num6, captionRect.Top + 4 * lineWidth + shift + 1, captionRect.Right - 3 * lineWidth - lineWidth / 2 + shift - num6, captionRect.Bottom - lineWidth + shift);
				}
				for (int num7 = 0; num7 < Math.Max(1, lineWidth / 2); num7++)
				{
					graphics.DrawLine(pen, captionRect.Left + lineWidth + shift, captionRect.Bottom - lineWidth + shift - num7, captionRect.Right - 3 * lineWidth - lineWidth / 2 + shift, captionRect.Bottom - lineWidth + shift - num7);
				}
				return;
			}
			case CaptionButton.Help:
			{
				StringFormat stringFormat = new StringFormat();
				Font font = new Font("Microsoft Sans Serif", (float)captionRect.Height, FontStyle.Bold, GraphicsUnit.Pixel);
				stringFormat.Alignment = StringAlignment.Center;
				stringFormat.LineAlignment = StringAlignment.Center;
				graphics.DrawString("?", font, this.ResPool.GetSolidBrush(color), (float)(captionRect.X + captionRect.Width / 2 + shift), (float)(captionRect.Y + captionRect.Height / 2 + shift + lineWidth / 2), stringFormat);
				stringFormat.Dispose();
				font.Dispose();
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0005CA4C File Offset: 0x0005AC4C
		public void DrawScrollButtonPrimitive(Graphics dc, Rectangle area, ButtonState state)
		{
			if ((state & ButtonState.Pushed) == ButtonState.Pushed)
			{
				dc.FillRectangle(SystemBrushes.Control, area.X + 1, area.Y + 1, area.Width - 2, area.Height - 2);
				dc.DrawRectangle(SystemPens.ControlDark, area.X, area.Y, area.Width, area.Height);
				return;
			}
			Brush control = SystemBrushes.Control;
			Brush controlLightLight = SystemBrushes.ControlLightLight;
			Brush controlDark = SystemBrushes.ControlDark;
			Brush controlDarkDark = SystemBrushes.ControlDarkDark;
			dc.FillRectangle(control, area.X, area.Y, area.Width, 1);
			dc.FillRectangle(control, area.X, area.Y, 1, area.Height);
			dc.FillRectangle(controlLightLight, area.X + 1, area.Y + 1, area.Width - 1, 1);
			dc.FillRectangle(controlLightLight, area.X + 1, area.Y + 2, 1, area.Height - 4);
			dc.FillRectangle(controlDark, area.X + 1, area.Y + area.Height - 2, area.Width - 2, 1);
			dc.FillRectangle(controlDarkDark, area.X, area.Y + area.Height - 1, area.Width, 1);
			dc.FillRectangle(controlDark, area.X + area.Width - 2, area.Y + 1, 1, area.Height - 3);
			dc.FillRectangle(controlDarkDark, area.X + area.Width - 1, area.Y, 1, area.Height - 1);
			dc.FillRectangle(control, area.X + 2, area.Y + 2, area.Width - 4, area.Height - 4);
		}

		// Token: 0x04000B30 RID: 2864
		protected static readonly Color arrow_color = Color.Black;

		// Token: 0x04000B31 RID: 2865
		protected static readonly Color pen_ticks_color = Color.Black;

		// Token: 0x04000B32 RID: 2866
		protected static StringFormat string_format_menu_text;

		// Token: 0x04000B33 RID: 2867
		protected static StringFormat string_format_menu_shortcut;

		// Token: 0x04000B34 RID: 2868
		protected static StringFormat string_format_menu_menubar_text;

		// Token: 0x04000B35 RID: 2869
		private static ImageAttributes imagedisabled_attributes;

		// Token: 0x04000B36 RID: 2870
		private Font window_border_font;

		// Token: 0x020001AB RID: 427
		private enum VerticalAlignment
		{
			// Token: 0x04000B38 RID: 2872
			Top,
			// Token: 0x04000B39 RID: 2873
			Center,
			// Token: 0x04000B3A RID: 2874
			Bottom
		}
	}
}
