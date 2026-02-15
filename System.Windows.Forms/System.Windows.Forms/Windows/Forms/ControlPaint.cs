using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to paint common Windows controls and their elements. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000057 RID: 87
	public sealed class ControlPaint
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x0001089C File Offset: 0x0000EA9C
		internal static void Color2HBS(Color color, out int h, out int l, out int s)
		{
			int r = (int)color.R;
			int g = (int)color.G;
			int b = (int)color.B;
			int num = Math.Max(Math.Max(r, g), b);
			int num2 = Math.Min(Math.Min(r, g), b);
			l = ((num + num2) * ControlPaint.HLSMax + ControlPaint.RGBMax) / (2 * ControlPaint.RGBMax);
			if (num == num2)
			{
				h = 0;
				s = 0;
				return;
			}
			if (l <= ControlPaint.HLSMax / 2)
			{
				s = ((num - num2) * ControlPaint.HLSMax + (num + num2) / 2) / (num + num2);
			}
			else
			{
				s = ((num - num2) * ControlPaint.HLSMax + (2 * ControlPaint.RGBMax - num - num2) / 2) / (2 * ControlPaint.RGBMax - num - num2);
			}
			int num3 = ((num - r) * (ControlPaint.HLSMax / 6) + (num - num2) / 2) / (num - num2);
			int num4 = ((num - g) * (ControlPaint.HLSMax / 6) + (num - num2) / 2) / (num - num2);
			int num5 = ((num - b) * (ControlPaint.HLSMax / 6) + (num - num2) / 2) / (num - num2);
			if (r == num)
			{
				h = num5 - num4;
			}
			else if (g == num)
			{
				h = ControlPaint.HLSMax / 3 + num3 - num5;
			}
			else
			{
				h = 2 * ControlPaint.HLSMax / 3 + num4 - num3;
			}
			if (h < 0)
			{
				h += ControlPaint.HLSMax;
			}
			if (h > ControlPaint.HLSMax)
			{
				h -= ControlPaint.HLSMax;
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x000109F4 File Offset: 0x0000EBF4
		private static int HueToRGB(int n1, int n2, int hue)
		{
			if (hue < 0)
			{
				hue += ControlPaint.HLSMax;
			}
			if (hue > ControlPaint.HLSMax)
			{
				hue -= ControlPaint.HLSMax;
			}
			if (hue < ControlPaint.HLSMax / 6)
			{
				return n1 + ((n2 - n1) * hue + ControlPaint.HLSMax / 12) / (ControlPaint.HLSMax / 6);
			}
			if (hue < ControlPaint.HLSMax / 2)
			{
				return n2;
			}
			if (hue < ControlPaint.HLSMax * 2 / 3)
			{
				return n1 + ((n2 - n1) * (ControlPaint.HLSMax * 2 / 3 - hue) + ControlPaint.HLSMax / 12) / (ControlPaint.HLSMax / 6);
			}
			return n1;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00010A80 File Offset: 0x0000EC80
		internal static Color HBS2Color(int hue, int lum, int sat)
		{
			int num3;
			int num2;
			int num;
			if (sat == 0)
			{
				num = (num2 = (num3 = lum * ControlPaint.RGBMax / ControlPaint.HLSMax));
			}
			else
			{
				int num4;
				if (lum <= ControlPaint.HLSMax / 2)
				{
					num4 = (lum * (ControlPaint.HLSMax + sat) + ControlPaint.HLSMax / 2) / ControlPaint.HLSMax;
				}
				else
				{
					num4 = sat + lum - (sat * lum + ControlPaint.HLSMax / 2) / ControlPaint.HLSMax;
				}
				int num5 = 2 * lum - num4;
				num2 = Math.Min(255, (ControlPaint.HueToRGB(num5, num4, hue + ControlPaint.HLSMax / 3) * ControlPaint.RGBMax + ControlPaint.HLSMax / 2) / ControlPaint.HLSMax);
				num = Math.Min(255, (ControlPaint.HueToRGB(num5, num4, hue) * ControlPaint.RGBMax + ControlPaint.HLSMax / 2) / ControlPaint.HLSMax);
				num3 = Math.Min(255, (ControlPaint.HueToRGB(num5, num4, hue - ControlPaint.HLSMax / 3) * ControlPaint.RGBMax + ControlPaint.HLSMax / 2) / ControlPaint.HLSMax);
			}
			return Color.FromArgb(num2, num, num3);
		}

		/// <summary>Creates a new light color object for the control from the specified color.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the light color on the control.</returns>
		/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be lightened. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600043E RID: 1086 RVA: 0x00010B7A File Offset: 0x0000ED7A
		public static Color Light(Color baseColor)
		{
			return ControlPaint.Light(baseColor, 0.5f);
		}

		/// <summary>Creates a new light color object for the control from the specified color and lightens it by the specified percentage.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the light color on the control.</returns>
		/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be lightened. </param>
		/// <param name="percOfLightLight">The percentage to lighten the specified <see cref="T:System.Drawing.Color" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600043F RID: 1087 RVA: 0x00010B88 File Offset: 0x0000ED88
		public static Color Light(Color baseColor, float percOfLightLight)
		{
			if (baseColor.ToArgb() != ThemeEngine.Current.ColorControl.ToArgb())
			{
				int num;
				int num2;
				int num3;
				ControlPaint.Color2HBS(baseColor, out num, out num2, out num3);
				int num4 = Math.Min(255, num2 + (int)((float)(255 - num2) * 0.5f * percOfLightLight));
				return ControlPaint.HBS2Color(num, num4, num3);
			}
			if (percOfLightLight <= 0f)
			{
				return ThemeEngine.Current.ColorControlLight;
			}
			if (percOfLightLight == 1f)
			{
				return ThemeEngine.Current.ColorControlLightLight;
			}
			int num5 = (int)(ThemeEngine.Current.ColorControlLightLight.R - ThemeEngine.Current.ColorControlLight.R);
			int num6 = (int)(ThemeEngine.Current.ColorControlLightLight.G - ThemeEngine.Current.ColorControlLight.G);
			int num7 = (int)(ThemeEngine.Current.ColorControlLightLight.B - ThemeEngine.Current.ColorControlLight.B);
			return Color.FromArgb((int)ThemeEngine.Current.ColorControlLight.A, (int)((float)ThemeEngine.Current.ColorControlLight.R + (float)num5 * percOfLightLight), (int)((float)ThemeEngine.Current.ColorControlLight.G + (float)num6 * percOfLightLight), (int)((float)ThemeEngine.Current.ColorControlLight.B + (float)num7 * percOfLightLight));
		}

		/// <summary>Creates a new light color object for the control from the specified color.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the light color on the control.</returns>
		/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be lightened. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000440 RID: 1088 RVA: 0x00010CF1 File Offset: 0x0000EEF1
		public static Color LightLight(Color baseColor)
		{
			return ControlPaint.Light(baseColor, 1f);
		}

		/// <summary>Creates a new dark color object for the control from the specified color.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the dark color on the control.</returns>
		/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be darkened. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000441 RID: 1089 RVA: 0x00010CFE File Offset: 0x0000EEFE
		public static Color Dark(Color baseColor)
		{
			return ControlPaint.Dark(baseColor, 0.5f);
		}

		/// <summary>Creates a new dark color object for the control from the specified color and darkens it by the specified percentage.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represent the dark color on the control.</returns>
		/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be darkened. </param>
		/// <param name="percOfDarkDark">The percentage to darken the specified <see cref="T:System.Drawing.Color" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000442 RID: 1090 RVA: 0x00010D0C File Offset: 0x0000EF0C
		public static Color Dark(Color baseColor, float percOfDarkDark)
		{
			if (baseColor.ToArgb() != ThemeEngine.Current.ColorControl.ToArgb())
			{
				int num;
				int num2;
				int num3;
				ControlPaint.Color2HBS(baseColor, out num, out num2, out num3);
				int num4 = Math.Max(0, num2 - (int)((float)num2 * 0.333f));
				int num5 = Math.Max(0, num4 - (int)((float)num4 * percOfDarkDark));
				return ControlPaint.HBS2Color(num, num5, num3);
			}
			if (percOfDarkDark <= 0f)
			{
				return ThemeEngine.Current.ColorControlDark;
			}
			if (percOfDarkDark == 1f)
			{
				return ThemeEngine.Current.ColorControlDarkDark;
			}
			int num6 = (int)(ThemeEngine.Current.ColorControlDarkDark.R - ThemeEngine.Current.ColorControlDark.R);
			int num7 = (int)(ThemeEngine.Current.ColorControlDarkDark.G - ThemeEngine.Current.ColorControlDark.G);
			int num8 = (int)(ThemeEngine.Current.ColorControlDarkDark.B - ThemeEngine.Current.ColorControlDark.B);
			return Color.FromArgb((int)ThemeEngine.Current.ColorControlDark.A, (int)((float)ThemeEngine.Current.ColorControlDark.R + (float)num6 * percOfDarkDark), (int)((float)ThemeEngine.Current.ColorControlDark.G + (float)num7 * percOfDarkDark), (int)((float)ThemeEngine.Current.ColorControlDark.B + (float)num8 * percOfDarkDark));
		}

		/// <summary>Creates a new dark color object for the control from the specified color.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the dark color on the control.</returns>
		/// <param name="baseColor">The <see cref="T:System.Drawing.Color" /> to be darkened. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000443 RID: 1091 RVA: 0x00010E79 File Offset: 0x0000F079
		public static Color DarkDark(Color baseColor)
		{
			return ControlPaint.Dark(baseColor, 1f);
		}

		/// <summary>Draws a border with the specified style and color, on the specified graphics surface, and within the specified bounds on a button-style control.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the border. </param>
		/// <param name="color">The <see cref="T:System.Drawing.Color" /> of the border. </param>
		/// <param name="style">One of the <see cref="T:System.Windows.Forms.ButtonBorderStyle" /> values that specifies the style of the border. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000444 RID: 1092 RVA: 0x00010E88 File Offset: 0x0000F088
		public static void DrawBorder(Graphics graphics, Rectangle bounds, Color color, ButtonBorderStyle style)
		{
			int num = 1;
			int num2 = 1;
			if (style == ButtonBorderStyle.Inset)
			{
				num = 2;
			}
			if (style == ButtonBorderStyle.Outset)
			{
				num2 = 2;
				num = 2;
			}
			ControlPaint.DrawBorder(graphics, bounds, color, num, style, color, num, style, color, num2, style, color, num2, style);
		}

		/// <summary>Draws a border on a button-style control with the specified styles, colors, and border widths; on the specified graphics surface; and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the border. </param>
		/// <param name="leftColor">The <see cref="T:System.Drawing.Color" /> of the left of the border. </param>
		/// <param name="leftWidth">The width of the left border. </param>
		/// <param name="leftStyle">One of the <see cref="T:System.Windows.Forms.ButtonBorderStyle" /> values that specifies the style of the left border. </param>
		/// <param name="topColor">The <see cref="T:System.Drawing.Color" /> of the top of the border. </param>
		/// <param name="topWidth">The width of the top border. </param>
		/// <param name="topStyle">One of the <see cref="T:System.Windows.Forms.ButtonBorderStyle" /> values that specifies the style of the top border. </param>
		/// <param name="rightColor">The <see cref="T:System.Drawing.Color" /> of the right of the border. </param>
		/// <param name="rightWidth">The width of the right border. </param>
		/// <param name="rightStyle">One of the <see cref="T:System.Windows.Forms.ButtonBorderStyle" /> values that specifies the style of the right border. </param>
		/// <param name="bottomColor">The <see cref="T:System.Drawing.Color" /> of the bottom of the border. </param>
		/// <param name="bottomWidth">The width of the bottom border. </param>
		/// <param name="bottomStyle">One of the <see cref="T:System.Windows.Forms.ButtonBorderStyle" /> values that specifies the style of the bottom border. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000445 RID: 1093 RVA: 0x00010EBC File Offset: 0x0000F0BC
		public static void DrawBorder(Graphics graphics, Rectangle bounds, Color leftColor, int leftWidth, ButtonBorderStyle leftStyle, Color topColor, int topWidth, ButtonBorderStyle topStyle, Color rightColor, int rightWidth, ButtonBorderStyle rightStyle, Color bottomColor, int bottomWidth, ButtonBorderStyle bottomStyle)
		{
			ThemeEngine.Current.CPDrawBorder(graphics, bounds, leftColor, leftWidth, leftStyle, topColor, topWidth, topStyle, rightColor, rightWidth, rightStyle, bottomColor, bottomWidth, bottomStyle);
		}

		/// <summary>Draws a three-dimensional style border with the specified style, on the specified graphics surface, and within the specified bounds on a control.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the border. </param>
		/// <param name="style">One of the <see cref="T:System.Windows.Forms.Border3DStyle" /> values that specifies the style of the border. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000446 RID: 1094 RVA: 0x00010EEB File Offset: 0x0000F0EB
		public static void DrawBorder3D(Graphics graphics, Rectangle rectangle, Border3DStyle style)
		{
			ControlPaint.DrawBorder3D(graphics, rectangle, style, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
		}

		/// <summary>Draws a three-dimensional style border with the specified style, on the specified graphics surface and sides, and within the specified bounds on a control.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the border. </param>
		/// <param name="style">One of the <see cref="T:System.Windows.Forms.Border3DStyle" /> values that specifies the style of the border. </param>
		/// <param name="sides">One of the <see cref="T:System.Windows.Forms.Border3DSide" /> values that specifies the side of the rectangle to draw the border on. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000447 RID: 1095 RVA: 0x00010EF7 File Offset: 0x0000F0F7
		public static void DrawBorder3D(Graphics graphics, Rectangle rectangle, Border3DStyle style, Border3DSide sides)
		{
			ThemeEngine.Current.CPDrawBorder3D(graphics, rectangle, style, sides);
		}

		/// <summary>Draws a button control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the button. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the button in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000448 RID: 1096 RVA: 0x00010F07 File Offset: 0x0000F107
		public static void DrawButton(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			ThemeEngine.Current.CPDrawButton(graphics, rectangle, state);
		}

		/// <summary>Draws the specified caption button control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the caption button. </param>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.CaptionButton" /> values that specifies the type of caption button to draw. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the button in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000449 RID: 1097 RVA: 0x00010F16 File Offset: 0x0000F116
		public static void DrawCaptionButton(Graphics graphics, Rectangle rectangle, CaptionButton button, ButtonState state)
		{
			ThemeEngine.Current.CPDrawCaptionButton(graphics, rectangle, button, state);
		}

		/// <summary>Draws a check box control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the check box. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the check box in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600044A RID: 1098 RVA: 0x00010F26 File Offset: 0x0000F126
		public static void DrawCheckBox(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			ThemeEngine.Current.CPDrawCheckBox(graphics, rectangle, state);
		}

		/// <summary>Draws a focus rectangle on the specified graphics surface and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the grab handle glyph. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600044B RID: 1099 RVA: 0x00010F35 File Offset: 0x0000F135
		public static void DrawFocusRectangle(Graphics graphics, Rectangle rectangle)
		{
			ControlPaint.DrawFocusRectangle(graphics, rectangle, SystemColors.Control, SystemColors.ControlText);
		}

		/// <summary>Draws a focus rectangle on the specified graphics surface and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the grab handle glyph. </param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> that is the foreground color of the object to draw the focus rectangle on. </param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> that is the background color of the object to draw the focus rectangle on. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600044C RID: 1100 RVA: 0x00010F48 File Offset: 0x0000F148
		public static void DrawFocusRectangle(Graphics graphics, Rectangle rectangle, Color foreColor, Color backColor)
		{
			ThemeEngine.Current.CPDrawFocusRectangle(graphics, rectangle, foreColor, backColor);
		}

		/// <summary>Draws the specified image in a disabled state.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="x">The x-coordinate of the top left of the border image. </param>
		/// <param name="y">The y-coordinate of the top left of the border image. </param>
		/// <param name="background">The <see cref="T:System.Drawing.Color" /> of the background behind the image. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600044D RID: 1101 RVA: 0x00010F58 File Offset: 0x0000F158
		public static void DrawImageDisabled(Graphics graphics, Image image, int x, int y, Color background)
		{
			ThemeEngine.Current.CPDrawImageDisabled(graphics, image, x, y, background);
		}

		/// <summary>Draws the specified menu glyph on a menu item control within the specified bounds and on the specified surface.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the glyph. </param>
		/// <param name="glyph">One of the <see cref="T:System.Windows.Forms.MenuGlyph" /> values that specifies the image to draw. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600044E RID: 1102 RVA: 0x00010F6A File Offset: 0x0000F16A
		public static void DrawMenuGlyph(Graphics graphics, Rectangle rectangle, MenuGlyph glyph)
		{
			ThemeEngine.Current.CPDrawMenuGlyph(graphics, rectangle, glyph, ThemeEngine.Current.ColorMenuText, Color.Empty);
		}

		/// <summary>Draws a three-state check box control in the specified state, on the specified graphics surface, and within the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="rectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the check box. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.ButtonState" /> values that specifies the state to draw the check box in. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600044F RID: 1103 RVA: 0x00010F88 File Offset: 0x0000F188
		public static void DrawMixedCheckBox(Graphics graphics, Rectangle rectangle, ButtonState state)
		{
			ThemeEngine.Current.CPDrawMixedCheckBox(graphics, rectangle, state);
		}

		/// <summary>Draws a size grip on a form with the specified bounds and background color and on the specified graphics surface.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> of the background used to determine the colors of the size grip.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the dimensions of the size grip.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000450 RID: 1104 RVA: 0x00010F97 File Offset: 0x0000F197
		public static void DrawSizeGrip(Graphics graphics, Color backColor, Rectangle bounds)
		{
			ThemeEngine.Current.CPDrawSizeGrip(graphics, backColor, bounds);
		}

		/// <summary>Draws the specified string in a disabled state on the specified graphics surface; within the specified bounds; and in the specified font, color, and format.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> to draw on. </param>
		/// <param name="s">The string to draw. </param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to draw the string with. </param>
		/// <param name="color">The <see cref="T:System.Drawing.Color" /> of the background behind the string. </param>
		/// <param name="layoutRectangle">The <see cref="T:System.Drawing.RectangleF" /> that represents the dimensions of the string. </param>
		/// <param name="format">The <see cref="T:System.Drawing.StringFormat" /> to apply to the string. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000451 RID: 1105 RVA: 0x00010FA6 File Offset: 0x0000F1A6
		public static void DrawStringDisabled(Graphics graphics, string s, Font font, Color color, RectangleF layoutRectangle, StringFormat format)
		{
			ThemeEngine.Current.CPDrawStringDisabled(graphics, s, font, color, layoutRectangle, format);
		}

		// Token: 0x0400022C RID: 556
		private static int RGBMax = 255;

		// Token: 0x0400022D RID: 557
		private static int HLSMax = 255;

		// Token: 0x0400022E RID: 558
		[MonoTODO("Stub, does nothing")]
		private static bool DSFNotImpl = false;
	}
}
