using System;
using System.Drawing;

namespace System.Windows.Forms.Theming.Default
{
	// Token: 0x02000377 RID: 887
	internal class ButtonPainter
	{
		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x000884F8 File Offset: 0x000866F8
		protected SystemResPool ResPool
		{
			get
			{
				return ThemeEngine.Current.ResPool;
			}
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x00088504 File Offset: 0x00086704
		public virtual void Draw(Graphics g, Rectangle bounds, ButtonThemeState state, Color backColor, Color foreColor)
		{
			bool flag = backColor.ToArgb() == ThemeEngine.Current.ColorControl.ToArgb() || backColor == Color.Empty;
			CPColor cpcolor = (flag ? CPColor.Empty : this.ResPool.GetCPColor(backColor));
			Pen pen;
			if (state <= ButtonThemeState.Pressed)
			{
				if (state - ButtonThemeState.Normal > 1)
				{
					if (state != ButtonThemeState.Pressed)
					{
						return;
					}
					g.DrawRectangle(this.ResPool.GetPen(foreColor), bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
					bounds.Inflate(-1, -1);
					pen = (flag ? SystemPens.ControlDark : this.ResPool.GetPen(cpcolor.Dark));
					g.DrawRectangle(pen, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
					return;
				}
			}
			else if (state != ButtonThemeState.Disabled)
			{
				if (state != ButtonThemeState.Default)
				{
					return;
				}
				g.DrawRectangle(this.ResPool.GetPen(foreColor), bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
				bounds.Inflate(-1, -1);
				pen = (flag ? SystemPens.ControlLightLight : this.ResPool.GetPen(cpcolor.LightLight));
				g.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 2);
				g.DrawLine(pen, bounds.X + 1, bounds.Y, bounds.Right - 2, bounds.Y);
				pen = (flag ? SystemPens.Control : this.ResPool.GetPen(backColor));
				g.DrawLine(pen, bounds.X + 1, bounds.Y + 1, bounds.X + 1, bounds.Bottom - 3);
				g.DrawLine(pen, bounds.X + 2, bounds.Y + 1, bounds.Right - 3, bounds.Y + 1);
				pen = (flag ? SystemPens.ControlDark : this.ResPool.GetPen(cpcolor.Dark));
				g.DrawLine(pen, bounds.X + 1, bounds.Bottom - 2, bounds.Right - 2, bounds.Bottom - 2);
				g.DrawLine(pen, bounds.Right - 2, bounds.Y + 1, bounds.Right - 2, bounds.Bottom - 3);
				pen = (flag ? SystemPens.ControlDarkDark : this.ResPool.GetPen(cpcolor.DarkDark));
				g.DrawLine(pen, bounds.X, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
				g.DrawLine(pen, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 2);
				return;
			}
			pen = (flag ? SystemPens.ControlLightLight : this.ResPool.GetPen(cpcolor.LightLight));
			g.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 2);
			g.DrawLine(pen, bounds.X + 1, bounds.Y, bounds.Right - 2, bounds.Y);
			pen = (flag ? SystemPens.Control : this.ResPool.GetPen(backColor));
			g.DrawLine(pen, bounds.X + 1, bounds.Y + 1, bounds.X + 1, bounds.Bottom - 3);
			g.DrawLine(pen, bounds.X + 2, bounds.Y + 1, bounds.Right - 3, bounds.Y + 1);
			pen = (flag ? SystemPens.ControlDark : this.ResPool.GetPen(cpcolor.Dark));
			g.DrawLine(pen, bounds.X + 1, bounds.Bottom - 2, bounds.Right - 2, bounds.Bottom - 2);
			g.DrawLine(pen, bounds.Right - 2, bounds.Y + 1, bounds.Right - 2, bounds.Bottom - 3);
			pen = (flag ? SystemPens.ControlDarkDark : this.ResPool.GetPen(cpcolor.DarkDark));
			g.DrawLine(pen, bounds.X, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
			g.DrawLine(pen, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 2);
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x000889A0 File Offset: 0x00086BA0
		public virtual void DrawFlat(Graphics g, Rectangle bounds, ButtonThemeState state, Color backColor, Color foreColor, FlatButtonAppearance appearance)
		{
			bool flag = backColor.ToArgb() == ThemeEngine.Current.ColorControl.ToArgb() || backColor == Color.Empty;
			CPColor cpcolor = (flag ? CPColor.Empty : this.ResPool.GetCPColor(backColor));
			if (state <= ButtonThemeState.Disabled)
			{
				switch (state)
				{
				case ButtonThemeState.Normal:
				case ButtonThemeState.Normal | ButtonThemeState.Entered:
					goto IL_0149;
				case ButtonThemeState.Entered:
					break;
				case ButtonThemeState.Pressed:
					if (appearance.MouseDownBackColor != Color.Empty)
					{
						g.FillRectangle(this.ResPool.GetSolidBrush(appearance.MouseDownBackColor), bounds);
						goto IL_0149;
					}
					g.FillRectangle(this.ResPool.GetSolidBrush(ButtonPainter.ChangeIntensity(backColor, 0.95f)), bounds);
					goto IL_0149;
				default:
					if (state != ButtonThemeState.Disabled)
					{
						goto IL_0149;
					}
					goto IL_0149;
				}
			}
			else if (state != ButtonThemeState.Default)
			{
				if (state != (ButtonThemeState.Entered | ButtonThemeState.Default))
				{
					goto IL_0149;
				}
			}
			else
			{
				if (appearance.CheckedBackColor != Color.Empty)
				{
					g.FillRectangle(this.ResPool.GetSolidBrush(appearance.CheckedBackColor), bounds);
					goto IL_0149;
				}
				goto IL_0149;
			}
			if (appearance.MouseOverBackColor != Color.Empty)
			{
				g.FillRectangle(this.ResPool.GetSolidBrush(appearance.MouseOverBackColor), bounds);
			}
			else
			{
				g.FillRectangle(this.ResPool.GetSolidBrush(ButtonPainter.ChangeIntensity(backColor, 0.9f)), bounds);
			}
			IL_0149:
			Pen pen;
			if (appearance.BorderColor == Color.Empty)
			{
				pen = (flag ? SystemPens.ControlDarkDark : this.ResPool.GetSizedPen(cpcolor.DarkDark, appearance.BorderSize));
			}
			else
			{
				pen = this.ResPool.GetSizedPen(appearance.BorderColor, appearance.BorderSize);
			}
			bounds.Width--;
			bounds.Height--;
			if (appearance.BorderSize > 0)
			{
				g.DrawRectangle(pen, bounds);
			}
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x00088B78 File Offset: 0x00086D78
		public virtual void DrawPopup(Graphics g, Rectangle bounds, ButtonThemeState state, Color backColor, Color foreColor)
		{
			bool flag = backColor.ToArgb() == ThemeEngine.Current.ColorControl.ToArgb() || backColor == Color.Empty;
			CPColor cpcolor = (flag ? CPColor.Empty : this.ResPool.GetCPColor(backColor));
			Pen pen;
			switch (state)
			{
			case ButtonThemeState.Normal:
			case ButtonThemeState.Pressed:
				break;
			case ButtonThemeState.Entered:
				pen = (flag ? SystemPens.ControlLightLight : this.ResPool.GetPen(cpcolor.LightLight));
				g.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 2);
				g.DrawLine(pen, bounds.X + 1, bounds.Y, bounds.Right - 2, bounds.Y);
				pen = (flag ? SystemPens.ControlDark : this.ResPool.GetPen(cpcolor.Dark));
				g.DrawLine(pen, bounds.X, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
				g.DrawLine(pen, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 2);
				return;
			case ButtonThemeState.Normal | ButtonThemeState.Entered:
				return;
			default:
				if (state != ButtonThemeState.Disabled && state != ButtonThemeState.Default)
				{
					return;
				}
				break;
			}
			pen = (flag ? SystemPens.ControlDarkDark : this.ResPool.GetPen(cpcolor.DarkDark));
			bounds.Width--;
			bounds.Height--;
			g.DrawRectangle(pen, bounds);
			if (state == ButtonThemeState.Default || state == ButtonThemeState.Pressed)
			{
				bounds.Inflate(-1, -1);
				g.DrawRectangle(pen, bounds);
				return;
			}
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x00088D28 File Offset: 0x00086F28
		private static Color ChangeIntensity(Color baseColor, float percent)
		{
			int num;
			int num2;
			int num3;
			ControlPaint.Color2HBS(baseColor, out num, out num2, out num3);
			int num4 = Math.Min(255, (int)((float)num2 * percent));
			return ControlPaint.HBS2Color(num, num4, num3);
		}
	}
}
