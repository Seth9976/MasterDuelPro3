using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;

namespace System.Windows.Forms
{
	// Token: 0x020001B0 RID: 432
	internal class ToolBarItem : Component
	{
		// Token: 0x0600126A RID: 4714 RVA: 0x0005EE59 File Offset: 0x0005D059
		public ToolBarItem(ToolBarButton button)
		{
			this.toolbar = button.Parent;
			this.button = button;
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x0005EE74 File Offset: 0x0005D074
		public ToolBarButton Button
		{
			get
			{
				return this.button;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x0005EE7C File Offset: 0x0005D07C
		public Rectangle Rectangle
		{
			get
			{
				if (!this.button.Visible || this.toolbar == null)
				{
					return Rectangle.Empty;
				}
				if (this.button.Style == ToolBarButtonStyle.DropDownButton && this.toolbar.DropDownArrows)
				{
					Rectangle rectangle = this.bounds;
					rectangle.Width += ThemeEngine.Current.ToolBarDropDownWidth;
					return rectangle;
				}
				return this.bounds;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x0005EEE6 File Offset: 0x0005D0E6
		// (set) Token: 0x0600126E RID: 4718 RVA: 0x0005EEF3 File Offset: 0x0005D0F3
		public Point Location
		{
			get
			{
				return this.bounds.Location;
			}
			set
			{
				this.bounds.Location = value;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600126F RID: 4719 RVA: 0x0005EF04 File Offset: 0x0005D104
		public Rectangle ImageRectangle
		{
			get
			{
				Rectangle rectangle = this.image_rect;
				rectangle.X += this.bounds.X;
				rectangle.Y += this.bounds.Y;
				return rectangle;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x0005EF4C File Offset: 0x0005D14C
		public Rectangle TextRectangle
		{
			get
			{
				Rectangle rectangle = this.text_rect;
				rectangle.X += this.bounds.X;
				rectangle.Y += this.bounds.Y;
				return rectangle;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x0005EF94 File Offset: 0x0005D194
		private Size TextSize
		{
			get
			{
				StringFormat stringFormat = new StringFormat();
				stringFormat.HotkeyPrefix = HotkeyPrefix.Hide;
				SizeF sizeF = TextRenderer.MeasureString(this.button.Text, this.toolbar.Font, SizeF.Empty, stringFormat);
				if (sizeF == SizeF.Empty)
				{
					return Size.Empty;
				}
				return new Size((int)Math.Ceiling((double)sizeF.Width) + 6, (int)Math.Ceiling((double)sizeF.Height));
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x0005F006 File Offset: 0x0005D206
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x0005F018 File Offset: 0x0005D218
		public bool Pressed
		{
			get
			{
				return this.pressed && this.inside;
			}
			set
			{
				this.pressed = value;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x0005F021 File Offset: 0x0005D221
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x0005F029 File Offset: 0x0005D229
		public bool DDPressed
		{
			get
			{
				return this.dd_pressed;
			}
			set
			{
				this.dd_pressed = value;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x0005F032 File Offset: 0x0005D232
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x0005F03A File Offset: 0x0005D23A
		public bool Inside
		{
			get
			{
				return this.inside;
			}
			set
			{
				this.inside = value;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x0005F043 File Offset: 0x0005D243
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x0005F04B File Offset: 0x0005D24B
		public bool Hilight
		{
			get
			{
				return this.hilight;
			}
			set
			{
				if (this.hilight == value)
				{
					return;
				}
				this.hilight = value;
				this.Invalidate();
			}
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0005F064 File Offset: 0x0005D264
		public Size CalculateSize()
		{
			Theme theme = ThemeEngine.Current;
			int num = this.toolbar.ButtonSize.Height + 2 * theme.ToolBarGripWidth;
			if (this.button.Style == ToolBarButtonStyle.Separator)
			{
				return new Size(theme.ToolBarSeparatorWidth, num);
			}
			Size size;
			if (this.TextSize.IsEmpty && this.button.Image == null)
			{
				size = this.toolbar.default_size;
			}
			else
			{
				size = this.TextSize;
			}
			Size size2 = ((this.toolbar.ImageSize == Size.Empty) ? new Size(16, 16) : this.toolbar.ImageSize);
			int num2 = size2.Width + 2 * theme.ToolBarImageGripWidth;
			int num3 = size2.Height + 2 * theme.ToolBarImageGripWidth;
			if (this.toolbar.TextAlign == ToolBarTextAlign.Right)
			{
				size.Width = num2 + size.Width;
				size.Height = ((size.Height > num3) ? size.Height : num3);
			}
			else
			{
				size.Height = num3 + size.Height;
				size.Width = ((size.Width > num2) ? size.Width : num2);
			}
			size.Width += theme.ToolBarGripWidth;
			size.Height += theme.ToolBarGripWidth;
			return size;
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0005F1CC File Offset: 0x0005D3CC
		public bool Layout(bool vertical, int calculated_size)
		{
			if (this.toolbar == null || !this.button.Visible)
			{
				return false;
			}
			Size buttonSize = this.toolbar.ButtonSize;
			Size size = buttonSize;
			if (!this.toolbar.SizeSpecified || this.button.Style == ToolBarButtonStyle.Separator)
			{
				size = this.CalculateSize();
				if (size.Width == 0 || size.Height == 0)
				{
					size = buttonSize;
				}
				if (vertical)
				{
					size.Width = calculated_size;
				}
				else
				{
					size.Height = calculated_size;
				}
			}
			return this.Layout(size);
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0005F250 File Offset: 0x0005D450
		public bool Layout(Size size)
		{
			if (this.toolbar == null || !this.button.Visible)
			{
				return false;
			}
			this.bounds.Size = size;
			Size size2 = ((this.toolbar.ImageSize == Size.Empty) ? new Size(16, 16) : this.toolbar.ImageSize);
			int toolBarImageGripWidth = ThemeEngine.Current.ToolBarImageGripWidth;
			Rectangle rectangle;
			Rectangle rectangle2;
			if (this.toolbar.TextAlign == ToolBarTextAlign.Underneath)
			{
				rectangle = new Rectangle((this.bounds.Size.Width - size2.Width) / 2 - toolBarImageGripWidth, 0, size2.Width + 2 + toolBarImageGripWidth, size2.Height + 2 * toolBarImageGripWidth);
				rectangle2 = new Rectangle(0, rectangle.Height, this.bounds.Size.Width, this.bounds.Size.Height - rectangle.Height - 2 * toolBarImageGripWidth);
			}
			else
			{
				rectangle = new Rectangle(0, 0, size2.Width + 2 * toolBarImageGripWidth, size2.Height + 2 * toolBarImageGripWidth);
				rectangle2 = new Rectangle(rectangle.Width, 0, this.bounds.Size.Width - rectangle.Width, this.bounds.Size.Height - 2 * toolBarImageGripWidth);
			}
			bool flag = false;
			if (rectangle != this.image_rect || rectangle2 != this.text_rect)
			{
				flag = true;
			}
			this.image_rect = rectangle;
			this.text_rect = rectangle2;
			return flag;
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0005F3E0 File Offset: 0x0005D5E0
		public void Invalidate()
		{
			if (this.toolbar != null)
			{
				this.toolbar.Invalidate(this.Rectangle);
			}
		}

		// Token: 0x04000B65 RID: 2917
		private ToolBar toolbar;

		// Token: 0x04000B66 RID: 2918
		private ToolBarButton button;

		// Token: 0x04000B67 RID: 2919
		private Rectangle bounds;

		// Token: 0x04000B68 RID: 2920
		private Rectangle image_rect;

		// Token: 0x04000B69 RID: 2921
		private Rectangle text_rect;

		// Token: 0x04000B6A RID: 2922
		private bool dd_pressed;

		// Token: 0x04000B6B RID: 2923
		private bool inside;

		// Token: 0x04000B6C RID: 2924
		private bool hilight;

		// Token: 0x04000B6D RID: 2925
		private bool pressed;
	}
}
