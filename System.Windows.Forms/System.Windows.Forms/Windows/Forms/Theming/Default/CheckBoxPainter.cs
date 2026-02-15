using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms.Theming.Default
{
	// Token: 0x02000378 RID: 888
	internal class CheckBoxPainter
	{
		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x000884F8 File Offset: 0x000866F8
		protected SystemResPool ResPool
		{
			get
			{
				return ThemeEngine.Current.ResPool;
			}
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x00088D5C File Offset: 0x00086F5C
		public void PaintCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, ElementState state, FlatStyle style, CheckState checkState)
		{
			switch (style)
			{
			case FlatStyle.Flat:
				switch (state)
				{
				case ElementState.Normal:
					this.DrawFlatNormalCheckBox(g, bounds, backColor, foreColor, checkState);
					return;
				case ElementState.Hot:
					this.DrawFlatHotCheckBox(g, bounds, backColor, foreColor, checkState);
					return;
				case ElementState.Pressed:
					this.DrawFlatPressedCheckBox(g, bounds, backColor, foreColor, checkState);
					return;
				case ElementState.Disabled:
					this.DrawFlatDisabledCheckBox(g, bounds, backColor, foreColor, checkState);
					return;
				default:
					return;
				}
				break;
			case FlatStyle.Popup:
				switch (state)
				{
				case ElementState.Normal:
					this.DrawPopupNormalCheckBox(g, bounds, backColor, foreColor, checkState);
					return;
				case ElementState.Hot:
					this.DrawPopupHotCheckBox(g, bounds, backColor, foreColor, checkState);
					return;
				case ElementState.Pressed:
					this.DrawPopupPressedCheckBox(g, bounds, backColor, foreColor, checkState);
					return;
				case ElementState.Disabled:
					this.DrawPopupDisabledCheckBox(g, bounds, backColor, foreColor, checkState);
					return;
				default:
					return;
				}
				break;
			case FlatStyle.Standard:
			case FlatStyle.System:
				switch (state)
				{
				case ElementState.Normal:
					this.DrawNormalCheckBox(g, bounds, backColor, foreColor, checkState);
					return;
				case ElementState.Hot:
					this.DrawHotCheckBox(g, bounds, backColor, foreColor, checkState);
					return;
				case ElementState.Pressed:
					this.DrawPressedCheckBox(g, bounds, backColor, foreColor, checkState);
					return;
				case ElementState.Disabled:
					this.DrawDisabledCheckBox(g, bounds, backColor, foreColor, checkState);
					return;
				default:
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00088E78 File Offset: 0x00087078
		public virtual void DrawNormalCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			int num = ((bounds.Height > bounds.Width) ? bounds.Width : bounds.Height);
			int num2 = Math.Max(0, bounds.X + bounds.Width / 2 - num / 2);
			int num3 = Math.Max(0, bounds.Y + bounds.Height / 2 - num / 2);
			Rectangle rectangle = new Rectangle(num2, num3, num, num);
			g.FillRectangle(SystemBrushes.ControlLightLight, rectangle.X + 2, rectangle.Y + 2, rectangle.Width - 3, rectangle.Height - 3);
			Pen pen = SystemPens.ControlDark;
			g.DrawLine(pen, rectangle.X, rectangle.Y, rectangle.X, rectangle.Bottom - 2);
			g.DrawLine(pen, rectangle.X + 1, rectangle.Y, rectangle.Right - 2, rectangle.Y);
			pen = SystemPens.ControlDarkDark;
			g.DrawLine(pen, rectangle.X + 1, rectangle.Y + 1, rectangle.X + 1, rectangle.Bottom - 3);
			g.DrawLine(pen, rectangle.X + 2, rectangle.Y + 1, rectangle.Right - 3, rectangle.Y + 1);
			pen = SystemPens.ControlLightLight;
			g.DrawLine(pen, rectangle.Right - 1, rectangle.Y, rectangle.Right - 1, rectangle.Bottom - 1);
			g.DrawLine(pen, rectangle.X, rectangle.Bottom - 1, rectangle.Right - 1, rectangle.Bottom - 1);
			using (Pen pen2 = new Pen(this.ResPool.GetHatchBrush(HatchStyle.Percent50, Color.FromArgb(this.Clamp((int)(this.ColorControl.R + 3), 0, 255), (int)this.ColorControl.G, (int)this.ColorControl.B), this.ColorControl)))
			{
				g.DrawLine(pen2, rectangle.X + 1, rectangle.Bottom - 2, rectangle.Right - 2, rectangle.Bottom - 2);
				g.DrawLine(pen2, rectangle.Right - 2, rectangle.Y + 1, rectangle.Right - 2, rectangle.Bottom - 2);
			}
			if (state == CheckState.Checked)
			{
				this.DrawCheck(g, bounds, Color.Black);
				return;
			}
			if (state == CheckState.Indeterminate)
			{
				this.DrawCheck(g, bounds, SystemColors.ControlDark);
			}
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x0008911C File Offset: 0x0008731C
		public virtual void DrawHotCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			this.DrawNormalCheckBox(g, bounds, backColor, foreColor, state);
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x0008912C File Offset: 0x0008732C
		public virtual void DrawPressedCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			int num = ((bounds.Height > bounds.Width) ? bounds.Width : bounds.Height);
			int num2 = Math.Max(0, bounds.X + bounds.Width / 2 - num / 2);
			int num3 = Math.Max(0, bounds.Y + bounds.Height / 2 - num / 2);
			Rectangle rectangle = new Rectangle(num2, num3, num, num);
			g.FillRectangle(this.ResPool.GetHatchBrush(HatchStyle.Percent50, Color.FromArgb(this.Clamp((int)(this.ColorControl.R + 3), 0, 255), (int)this.ColorControl.G, (int)this.ColorControl.B), this.ColorControl), rectangle.X + 2, rectangle.Y + 2, rectangle.Width - 3, rectangle.Height - 3);
			Pen pen = SystemPens.ControlDark;
			g.DrawLine(pen, rectangle.X, rectangle.Y, rectangle.X, rectangle.Bottom - 2);
			g.DrawLine(pen, rectangle.X + 1, rectangle.Y, rectangle.Right - 2, rectangle.Y);
			pen = SystemPens.ControlDarkDark;
			g.DrawLine(pen, rectangle.X + 1, rectangle.Y + 1, rectangle.X + 1, rectangle.Bottom - 3);
			g.DrawLine(pen, rectangle.X + 2, rectangle.Y + 1, rectangle.Right - 3, rectangle.Y + 1);
			pen = SystemPens.ControlLightLight;
			g.DrawLine(pen, rectangle.Right - 1, rectangle.Y, rectangle.Right - 1, rectangle.Bottom - 1);
			g.DrawLine(pen, rectangle.X, rectangle.Bottom - 1, rectangle.Right - 1, rectangle.Bottom - 1);
			using (Pen pen2 = new Pen(this.ResPool.GetHatchBrush(HatchStyle.Percent50, Color.FromArgb(this.Clamp((int)(this.ColorControl.R + 3), 0, 255), (int)this.ColorControl.G, (int)this.ColorControl.B), this.ColorControl)))
			{
				g.DrawLine(pen2, rectangle.X + 1, rectangle.Bottom - 2, rectangle.Right - 2, rectangle.Bottom - 2);
				g.DrawLine(pen2, rectangle.Right - 2, rectangle.Y + 1, rectangle.Right - 2, rectangle.Bottom - 2);
			}
			if (state == CheckState.Checked)
			{
				this.DrawCheck(g, bounds, Color.Black);
				return;
			}
			if (state == CheckState.Indeterminate)
			{
				this.DrawCheck(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x00089420 File Offset: 0x00087620
		public virtual void DrawDisabledCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			this.DrawPressedCheckBox(g, bounds, backColor, foreColor, CheckState.Unchecked);
			if (state == CheckState.Checked || state == CheckState.Indeterminate)
			{
				this.DrawCheck(g, bounds, SystemColors.ControlDark);
			}
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x00089448 File Offset: 0x00087648
		public virtual void DrawFlatNormalCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			Rectangle rectangle = new Rectangle(bounds.X, bounds.Y, Math.Max(bounds.Width - 2, 0), Math.Max(bounds.Height - 2, 0));
			Rectangle rectangle2 = new Rectangle(rectangle.X + 1, rectangle.Y + 1, Math.Max(rectangle.Width - 2, 0), Math.Max(rectangle.Height - 2, 0));
			g.FillRectangle(this.ResPool.GetSolidBrush(ControlPaint.LightLight(backColor)), rectangle2);
			ControlPaint.DrawBorder(g, rectangle, foreColor, ButtonBorderStyle.Solid);
			bounds.Offset(-1, 0);
			if (state == CheckState.Checked)
			{
				this.DrawCheck(g, bounds, Color.Black);
				return;
			}
			if (state == CheckState.Indeterminate)
			{
				this.DrawCheck(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x00089510 File Offset: 0x00087710
		public virtual void DrawFlatHotCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			Rectangle rectangle = new Rectangle(bounds.X, bounds.Y, Math.Max(bounds.Width - 2, 0), Math.Max(bounds.Height - 2, 0));
			Rectangle rectangle2 = new Rectangle(rectangle.X + 1, rectangle.Y + 1, Math.Max(rectangle.Width - 2, 0), Math.Max(rectangle.Height - 2, 0));
			g.FillRectangle(this.ResPool.GetSolidBrush(backColor), rectangle2);
			ControlPaint.DrawBorder(g, rectangle, foreColor, ButtonBorderStyle.Solid);
			bounds.Offset(-1, 0);
			if (state == CheckState.Checked)
			{
				this.DrawCheck(g, bounds, Color.Black);
				return;
			}
			if (state == CheckState.Indeterminate)
			{
				this.DrawCheck(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x000895D2 File Offset: 0x000877D2
		public virtual void DrawFlatPressedCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			this.DrawFlatNormalCheckBox(g, bounds, backColor, foreColor, state);
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x000895E4 File Offset: 0x000877E4
		public virtual void DrawFlatDisabledCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			Rectangle rectangle = new Rectangle(bounds.X, bounds.Y, Math.Max(bounds.Width - 2, 0), Math.Max(bounds.Height - 2, 0));
			ControlPaint.DrawBorder(g, rectangle, foreColor, ButtonBorderStyle.Solid);
			bounds.Offset(-1, 0);
			if (state == CheckState.Checked || state == CheckState.Indeterminate)
			{
				this.DrawCheck(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x000895D2 File Offset: 0x000877D2
		public virtual void DrawPopupNormalCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			this.DrawFlatNormalCheckBox(g, bounds, backColor, foreColor, state);
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x00089650 File Offset: 0x00087850
		public virtual void DrawPopupHotCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			Rectangle rectangle = new Rectangle(bounds.X, bounds.Y, Math.Max(bounds.Width - 1, 0), Math.Max(bounds.Height - 1, 0));
			Rectangle rectangle2 = new Rectangle(rectangle.X + 1, rectangle.Y + 1, Math.Max(rectangle.Width - 3, 0), Math.Max(rectangle.Height - 3, 0));
			g.FillRectangle(this.ResPool.GetSolidBrush(ControlPaint.LightLight(backColor)), rectangle2);
			ThemeEngine.Current.CPDrawBorder3D(g, rectangle, Border3DStyle.SunkenInner, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom, backColor);
			bounds.Offset(-1, 0);
			if (state == CheckState.Checked)
			{
				this.DrawCheck(g, bounds, Color.Black);
				return;
			}
			if (state == CheckState.Indeterminate)
			{
				this.DrawCheck(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x00089720 File Offset: 0x00087920
		public virtual void DrawPopupPressedCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			Rectangle rectangle = new Rectangle(bounds.X, bounds.Y, Math.Max(bounds.Width - 1, 0), Math.Max(bounds.Height - 1, 0));
			Rectangle rectangle2 = new Rectangle(rectangle.X + 1, rectangle.Y + 1, Math.Max(rectangle.Width - 3, 0), Math.Max(rectangle.Height - 3, 0));
			g.FillRectangle(this.ResPool.GetSolidBrush(backColor), rectangle2);
			ThemeEngine.Current.CPDrawBorder3D(g, rectangle, Border3DStyle.SunkenInner, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom, backColor);
			bounds.Offset(-1, 0);
			if (state == CheckState.Checked)
			{
				this.DrawCheck(g, bounds, Color.Black);
				return;
			}
			if (state == CheckState.Indeterminate)
			{
				this.DrawCheck(g, bounds, SystemColors.ControlDarkDark);
			}
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x000897E8 File Offset: 0x000879E8
		public virtual void DrawPopupDisabledCheckBox(Graphics g, Rectangle bounds, Color backColor, Color foreColor, CheckState state)
		{
			this.DrawFlatDisabledCheckBox(g, bounds, backColor, foreColor, state);
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x000897F8 File Offset: 0x000879F8
		public virtual void DrawCheck(Graphics g, Rectangle bounds, Color checkColor)
		{
			int num = ((bounds.Height > bounds.Width) ? (bounds.Width / 2) : (bounds.Height / 2));
			Pen pen = this.ResPool.GetPen(checkColor);
			if (num < 7)
			{
				int num2 = Math.Max(3, num / 3);
				int num3 = Math.Max(1, num / 9);
				Rectangle rectangle = new Rectangle(bounds.X + bounds.Width / 2 - num / 2 - 1, bounds.Y + bounds.Height / 2 - num / 2 - 1, num, num);
				for (int i = 0; i < num2; i++)
				{
					g.DrawLine(pen, rectangle.Left + num2 / 2, rectangle.Top + num2 + i, rectangle.Left + num2 / 2 + 2 * num3, rectangle.Top + num2 + 2 * num3 + i);
					g.DrawLine(pen, rectangle.Left + num2 / 2 + 2 * num3, rectangle.Top + num2 + 2 * num3 + i, rectangle.Left + num2 / 2 + 6 * num3, rectangle.Top + num2 - 2 * num3 + i);
				}
				return;
			}
			int num4 = Math.Max(3, num / 3) + 1;
			int num5 = bounds.Width / 2;
			int num6 = bounds.Height / 2;
			Rectangle rectangle2 = new Rectangle(bounds.X + num5 - num / 2 - 1, bounds.Y + num6 - num / 2, num, num);
			int num7 = num / 3;
			int num8 = num - num7 - 1;
			for (int j = 0; j < num4; j++)
			{
				g.DrawLine(pen, rectangle2.X, rectangle2.Bottom - 1 - num7 - j, rectangle2.X + num7, rectangle2.Bottom - 1 - j);
				g.DrawLine(pen, rectangle2.X + num7, rectangle2.Bottom - 1 - j, rectangle2.Right - 1, rectangle2.Bottom - j - 1 - num8);
			}
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x00050995 File Offset: 0x0004EB95
		private int Clamp(int value, int lower, int upper)
		{
			if (value < lower)
			{
				return lower;
			}
			if (value > upper)
			{
				return upper;
			}
			return value;
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x0005083C File Offset: 0x0004EA3C
		private Color ColorControl
		{
			get
			{
				return SystemColors.Control;
			}
		}
	}
}
