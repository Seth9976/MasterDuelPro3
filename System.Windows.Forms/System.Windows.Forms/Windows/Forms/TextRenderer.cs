using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to measure and render text. This class cannot be inherited. </summary>
	// Token: 0x020001A3 RID: 419
	public sealed class TextRenderer
	{
		/// <summary>Draws the specified text at the specified location using the specified device context, font, color, and formatting instructions. </summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the drawn text. </param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x0600103C RID: 4156 RVA: 0x0004FB2A File Offset: 0x0004DD2A
		public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, TextFormatFlags flags)
		{
			TextRenderer.DrawTextInternal(dc, text, font, pt, foreColor, Color.Transparent, flags, false);
		}

		/// <summary>Draws the specified text within the specified bounds using the specified device context, font, color, and formatting instructions.</summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the text.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x0600103D RID: 4157 RVA: 0x0004FB3F File Offset: 0x0004DD3F
		public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, TextFormatFlags flags)
		{
			TextRenderer.DrawTextInternal(dc, text, font, bounds, foreColor, Color.Transparent, flags, false);
		}

		/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified font.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn on a single line with the specified <paramref name="font" />. You can manipulate how the text is drawn by using one of the <see cref="M:System.Windows.Forms.TextRenderer.DrawText(System.Drawing.IDeviceContext,System.String,System.Drawing.Font,System.Drawing.Rectangle,System.Drawing.Color,System.Windows.Forms.TextFormatFlags)" /> overloads that takes a <see cref="T:System.Windows.Forms.TextFormatFlags" /> parameter. For example, the default behavior of the <see cref="T:System.Windows.Forms.TextRenderer" /> is to add padding to the bounding rectangle of the drawn text to accommodate overhanging glyphs. If you need to draw a line of text without these extra spaces you should use the versions of <see cref="M:System.Windows.Forms.TextRenderer.DrawText(System.Drawing.IDeviceContext,System.String,System.Drawing.Font,System.Drawing.Point,System.Drawing.Color)" /> and <see cref="M:System.Windows.Forms.TextRenderer.MeasureText(System.Drawing.IDeviceContext,System.String,System.Drawing.Font)" /> that take a <see cref="T:System.Drawing.Size" /> and <see cref="T:System.Windows.Forms.TextFormatFlags" /> parameter. For an example, see <see cref="M:System.Windows.Forms.TextRenderer.MeasureText(System.Drawing.IDeviceContext,System.String,System.Drawing.Font,System.Drawing.Size,System.Windows.Forms.TextFormatFlags)" />.</returns>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
		// Token: 0x0600103E RID: 4158 RVA: 0x0004FB54 File Offset: 0x0004DD54
		public static Size MeasureText(string text, Font font)
		{
			return TextRenderer.MeasureTextInternal(Hwnd.GraphicsContext, text, font, Size.Empty, TextFormatFlags.Left, false);
		}

		/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified font and formatting instructions, using the specified size to create the initial bounding rectangle for the text.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn with the specified <paramref name="font" /> and format.</returns>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
		/// <param name="proposedSize">The <see cref="T:System.Drawing.Size" /> of the initial bounding rectangle.</param>
		/// <param name="flags">The formatting instructions to apply to the measured text.</param>
		// Token: 0x0600103F RID: 4159 RVA: 0x0004FB69 File Offset: 0x0004DD69
		public static Size MeasureText(string text, Font font, Size proposedSize, TextFormatFlags flags)
		{
			return TextRenderer.MeasureTextInternal(Hwnd.GraphicsContext, text, font, proposedSize, flags, false);
		}

		/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified device context, font, and formatting instructions, using the specified size to create the initial bounding rectangle for the text.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn with the specified <paramref name="font" /> and format.</returns>
		/// <param name="dc">The device context in which to measure the text.</param>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
		/// <param name="proposedSize">The <see cref="T:System.Drawing.Size" /> of the initial bounding rectangle.</param>
		/// <param name="flags">The formatting instructions to apply to the measured text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x06001040 RID: 4160 RVA: 0x0004FB7A File Offset: 0x0004DD7A
		public static Size MeasureText(IDeviceContext dc, string text, Font font, Size proposedSize, TextFormatFlags flags)
		{
			return TextRenderer.MeasureTextInternal(dc, text, font, proposedSize, flags, false);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0004FB88 File Offset: 0x0004DD88
		internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, Color backColor, TextFormatFlags flags, bool useDrawString)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (text == null || text.Length == 0)
			{
				return;
			}
			if (!useDrawString && !XplatUI.RunningOnUnix)
			{
				if ((flags & TextFormatFlags.VerticalCenter) == TextFormatFlags.VerticalCenter || (flags & TextFormatFlags.Bottom) == TextFormatFlags.Bottom)
				{
					flags |= TextFormatFlags.SingleLine;
				}
				Rectangle rectangle = TextRenderer.PadRectangle(bounds, flags);
				rectangle.Offset((int)(dc as Graphics).Transform.OffsetX, (int)(dc as Graphics).Transform.OffsetY);
				IntPtr intPtr = IntPtr.Zero;
				bool flag = false;
				if ((flags & TextFormatFlags.PreserveGraphicsClipping) == TextFormatFlags.PreserveGraphicsClipping)
				{
					Graphics graphics = (Graphics)dc;
					Region clip = graphics.Clip;
					if (!clip.IsInfinite(graphics))
					{
						IntPtr hrgn = clip.GetHrgn(graphics);
						intPtr = dc.GetHdc();
						TextRenderer.SelectClipRgn(intPtr, hrgn);
						TextRenderer.DeleteObject(hrgn);
						flag = true;
					}
				}
				if (intPtr == IntPtr.Zero)
				{
					intPtr = dc.GetHdc();
				}
				if (foreColor != Color.Empty)
				{
					TextRenderer.SetTextColor(intPtr, ColorTranslator.ToWin32(foreColor));
				}
				if (backColor != Color.Transparent && backColor != Color.Empty)
				{
					TextRenderer.SetBkMode(intPtr, 2);
					TextRenderer.SetBkColor(intPtr, ColorTranslator.ToWin32(backColor));
				}
				else
				{
					TextRenderer.SetBkMode(intPtr, 1);
				}
				XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(rectangle);
				if (font != null)
				{
					IntPtr intPtr2 = TextRenderer.SelectObject(intPtr, font.ToHfont());
					TextRenderer.Win32DrawText(intPtr, text, text.Length, ref rect, (int)flags);
					intPtr2 = TextRenderer.SelectObject(intPtr, intPtr2);
					TextRenderer.DeleteObject(intPtr2);
				}
				else
				{
					TextRenderer.Win32DrawText(intPtr, text, text.Length, ref rect, (int)flags);
				}
				if (flag)
				{
					TextRenderer.SelectClipRgn(intPtr, IntPtr.Zero);
				}
				dc.ReleaseHdc();
				return;
			}
			IntPtr zero = IntPtr.Zero;
			Graphics graphics2;
			if (dc is Graphics)
			{
				graphics2 = (Graphics)dc;
			}
			else
			{
				graphics2 = Graphics.FromHdc(dc.GetHdc());
			}
			StringFormat stringFormat = TextRenderer.FlagsToStringFormat(flags);
			Rectangle rectangle2 = TextRenderer.PadDrawStringRectangle(bounds, flags);
			graphics2.DrawString(text, font, ThemeEngine.Current.ResPool.GetSolidBrush(foreColor), rectangle2, stringFormat);
			if (!(dc is Graphics))
			{
				graphics2.Dispose();
				dc.ReleaseHdc();
			}
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0004FDA4 File Offset: 0x0004DFA4
		internal static Size MeasureTextInternal(IDeviceContext dc, string text, Font font, Size proposedSize, TextFormatFlags flags, bool useMeasureString)
		{
			if (!useMeasureString && !XplatUI.RunningOnUnix)
			{
				flags |= (TextFormatFlags)1024;
				IntPtr hdc = dc.GetHdc();
				XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(new Rectangle(Point.Empty, proposedSize));
				if (font != null)
				{
					IntPtr intPtr = TextRenderer.SelectObject(hdc, font.ToHfont());
					TextRenderer.Win32DrawText(hdc, text, text.Length, ref rect, (int)flags);
					intPtr = TextRenderer.SelectObject(hdc, intPtr);
					TextRenderer.DeleteObject(intPtr);
				}
				else
				{
					TextRenderer.Win32DrawText(hdc, text, text.Length, ref rect, (int)flags);
				}
				dc.ReleaseHdc();
				Size size = rect.ToRectangle().Size;
				if (size.Width > 0 && (flags & TextFormatFlags.NoPadding) == TextFormatFlags.Left)
				{
					size.Width += 6;
					size.Width += size.Height / 8;
				}
				return size;
			}
			StringFormat stringFormat = TextRenderer.FlagsToStringFormat(flags);
			int num;
			if (proposedSize.Width == 0)
			{
				num = int.MaxValue;
			}
			else
			{
				num = proposedSize.Width;
				if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.Left)
				{
					num -= 9;
				}
			}
			Size size2;
			if (dc is Graphics)
			{
				size2 = (dc as Graphics).MeasureString(text, font, num, stringFormat).ToSize();
			}
			else
			{
				size2 = TextRenderer.MeasureString(text, font, num, stringFormat).ToSize();
			}
			if (size2.Width > 0 && (flags & TextFormatFlags.NoPadding) == TextFormatFlags.Left)
			{
				size2.Width += 9;
			}
			return size2;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0004FF13 File Offset: 0x0004E113
		internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, TextFormatFlags flags, bool useDrawString)
		{
			TextRenderer.DrawTextInternal(dc, text, font, bounds, foreColor, Color.Transparent, flags, useDrawString);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0004FF29 File Offset: 0x0004E129
		internal static Size MeasureTextInternal(string text, Font font, bool useMeasureString)
		{
			return TextRenderer.MeasureTextInternal(Hwnd.GraphicsContext, text, font, Size.Empty, TextFormatFlags.Left, useMeasureString);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0004FF40 File Offset: 0x0004E140
		internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, Color backColor, TextFormatFlags flags, bool useDrawString)
		{
			Size size = TextRenderer.MeasureTextInternal(dc, text, font, useDrawString);
			TextRenderer.DrawTextInternal(dc, text, font, new Rectangle(pt, size), foreColor, backColor, flags, useDrawString);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0004FF6F File Offset: 0x0004E16F
		internal static Size MeasureTextInternal(IDeviceContext dc, string text, Font font, bool useMeasureString)
		{
			return TextRenderer.MeasureTextInternal(dc, text, font, Size.Empty, TextFormatFlags.Left, useMeasureString);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0004FF80 File Offset: 0x0004E180
		internal static Size MeasureTextInternal(string text, Font font, Size proposedSize, TextFormatFlags flags, bool useMeasureString)
		{
			return TextRenderer.MeasureTextInternal(Hwnd.GraphicsContext, text, font, proposedSize, flags, useMeasureString);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0004FF92 File Offset: 0x0004E192
		internal static SizeF MeasureString(string text, Font font)
		{
			return Hwnd.GraphicsContext.MeasureString(text, font);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0004FFA0 File Offset: 0x0004E1A0
		internal static SizeF MeasureString(string text, Font font, int width, StringFormat format)
		{
			return Hwnd.GraphicsContext.MeasureString(text, font, width, format);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0004FFB0 File Offset: 0x0004E1B0
		internal static SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat)
		{
			return Hwnd.GraphicsContext.MeasureString(text, font, layoutArea, stringFormat);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0004FFC0 File Offset: 0x0004E1C0
		internal static SizeF GetDpi()
		{
			return new SizeF(Hwnd.GraphicsContext.DpiX, Hwnd.GraphicsContext.DpiY);
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0004FFDC File Offset: 0x0004E1DC
		private static StringFormat FlagsToStringFormat(TextFormatFlags flags)
		{
			StringFormat stringFormat = new StringFormat();
			if ((flags & TextFormatFlags.HorizontalCenter) == TextFormatFlags.HorizontalCenter)
			{
				stringFormat.Alignment = StringAlignment.Center;
			}
			else if ((flags & TextFormatFlags.Right) == TextFormatFlags.Right)
			{
				stringFormat.Alignment = StringAlignment.Far;
			}
			else
			{
				stringFormat.Alignment = StringAlignment.Near;
			}
			if ((flags & TextFormatFlags.Bottom) == TextFormatFlags.Bottom)
			{
				stringFormat.LineAlignment = StringAlignment.Far;
			}
			else if ((flags & TextFormatFlags.VerticalCenter) == TextFormatFlags.VerticalCenter)
			{
				stringFormat.LineAlignment = StringAlignment.Center;
			}
			else
			{
				stringFormat.LineAlignment = StringAlignment.Near;
			}
			if ((flags & TextFormatFlags.EndEllipsis) == TextFormatFlags.EndEllipsis)
			{
				stringFormat.Trimming = StringTrimming.EllipsisCharacter;
			}
			else if ((flags & TextFormatFlags.PathEllipsis) == TextFormatFlags.PathEllipsis)
			{
				stringFormat.Trimming = StringTrimming.EllipsisPath;
			}
			else if ((flags & TextFormatFlags.WordEllipsis) == TextFormatFlags.WordEllipsis)
			{
				stringFormat.Trimming = StringTrimming.EllipsisWord;
			}
			else
			{
				stringFormat.Trimming = StringTrimming.Character;
			}
			if ((flags & TextFormatFlags.NoPrefix) == TextFormatFlags.NoPrefix)
			{
				stringFormat.HotkeyPrefix = HotkeyPrefix.None;
			}
			else if ((flags & TextFormatFlags.HidePrefix) == TextFormatFlags.HidePrefix)
			{
				stringFormat.HotkeyPrefix = HotkeyPrefix.Hide;
			}
			else
			{
				stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
			}
			if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.NoPadding)
			{
				stringFormat.FormatFlags |= StringFormatFlags.FitBlackBox;
			}
			if ((flags & TextFormatFlags.SingleLine) == TextFormatFlags.SingleLine)
			{
				stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
			}
			else if ((flags & TextFormatFlags.TextBoxControl) == TextFormatFlags.TextBoxControl)
			{
				stringFormat.FormatFlags |= StringFormatFlags.LineLimit;
			}
			if ((flags & TextFormatFlags.NoClipping) == TextFormatFlags.NoClipping)
			{
				stringFormat.FormatFlags |= StringFormatFlags.NoClip;
			}
			return stringFormat;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00050134 File Offset: 0x0004E334
		private static Rectangle PadRectangle(Rectangle r, TextFormatFlags flags)
		{
			if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.Left && (flags & TextFormatFlags.Right) == TextFormatFlags.Left && (flags & TextFormatFlags.HorizontalCenter) == TextFormatFlags.Left)
			{
				r.X += 3;
				r.Width -= 3;
			}
			if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.Left && (flags & TextFormatFlags.Right) == TextFormatFlags.Right)
			{
				r.Width -= 4;
			}
			if ((flags & TextFormatFlags.LeftAndRightPadding) == TextFormatFlags.LeftAndRightPadding)
			{
				r.X += 2;
				r.Width -= 2;
			}
			if ((flags & TextFormatFlags.WordEllipsis) == TextFormatFlags.WordEllipsis || (flags & TextFormatFlags.EndEllipsis) == TextFormatFlags.EndEllipsis || (flags & TextFormatFlags.WordBreak) == TextFormatFlags.WordBreak)
			{
				r.Width -= 4;
			}
			if ((flags & TextFormatFlags.VerticalCenter) == TextFormatFlags.VerticalCenter)
			{
				r.Y++;
			}
			return r;
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00050208 File Offset: 0x0004E408
		private static Rectangle PadDrawStringRectangle(Rectangle r, TextFormatFlags flags)
		{
			if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.Left && (flags & TextFormatFlags.Right) == TextFormatFlags.Left && (flags & TextFormatFlags.HorizontalCenter) == TextFormatFlags.Left)
			{
				r.X++;
				r.Width--;
			}
			if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.Left && (flags & TextFormatFlags.Right) == TextFormatFlags.Right)
			{
				r.Width -= 4;
			}
			if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.NoPadding)
			{
				r.X -= 2;
			}
			if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.Left && (flags & TextFormatFlags.Bottom) == TextFormatFlags.Bottom)
			{
				r.Y++;
			}
			if ((flags & TextFormatFlags.LeftAndRightPadding) == TextFormatFlags.LeftAndRightPadding)
			{
				r.X += 2;
				r.Width -= 2;
			}
			if ((flags & TextFormatFlags.VerticalCenter) == TextFormatFlags.VerticalCenter && XplatUI.RunningOnUnix)
			{
				r.Y--;
			}
			return r;
		}

		// Token: 0x0600104F RID: 4175
		[DllImport("user32", CharSet = CharSet.Unicode, EntryPoint = "DrawText")]
		private static extern int Win32DrawText(IntPtr hdc, string lpStr, int nCount, ref XplatUIWin32.RECT lpRect, int wFormat);

		// Token: 0x06001050 RID: 4176
		[DllImport("gdi32")]
		private static extern int SetTextColor(IntPtr hdc, int crColor);

		// Token: 0x06001051 RID: 4177
		[DllImport("gdi32")]
		private static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

		// Token: 0x06001052 RID: 4178
		[DllImport("gdi32")]
		private static extern int SetBkColor(IntPtr hdc, int crColor);

		// Token: 0x06001053 RID: 4179
		[DllImport("gdi32")]
		private static extern int SetBkMode(IntPtr hdc, int iBkMode);

		// Token: 0x06001054 RID: 4180
		[DllImport("gdi32")]
		private static extern bool DeleteObject(IntPtr objectHandle);

		// Token: 0x06001055 RID: 4181
		[DllImport("gdi32")]
		private static extern bool SelectClipRgn(IntPtr hdc, IntPtr hrgn);
	}
}
