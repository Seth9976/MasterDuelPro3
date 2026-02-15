using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000195 RID: 405
	internal class TextBoxTextRenderer
	{
		// Token: 0x06000FAC RID: 4012 RVA: 0x00048F30 File Offset: 0x00047130
		static TextBoxTextRenderer()
		{
			int platform = (int)Environment.OSVersion.Platform;
			if (platform == 4 || platform == 128 || platform == 6)
			{
				TextBoxTextRenderer.use_textrenderer = false;
			}
			else
			{
				TextBoxTextRenderer.use_textrenderer = true;
			}
			TextBoxTextRenderer.max_size = new Size(32767, 32767);
			TextBoxTextRenderer.sf_nonprinting = new StringFormat(StringFormat.GenericTypographic);
			TextBoxTextRenderer.sf_nonprinting.Trimming = StringTrimming.None;
			TextBoxTextRenderer.sf_nonprinting.FormatFlags = StringFormatFlags.DisplayFormatControl;
			TextBoxTextRenderer.sf_nonprinting.HotkeyPrefix = HotkeyPrefix.None;
			TextBoxTextRenderer.sf_printing = StringFormat.GenericTypographic;
			TextBoxTextRenderer.sf_printing.HotkeyPrefix = HotkeyPrefix.None;
			TextBoxTextRenderer.measure_cache = new Hashtable();
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00048FCC File Offset: 0x000471CC
		public static void DrawText(Graphics g, string text, Font font, Color color, float x, float y, bool showNonPrint)
		{
			if (!TextBoxTextRenderer.use_textrenderer)
			{
				if (showNonPrint)
				{
					g.DrawString(text, font, ThemeEngine.Current.ResPool.GetSolidBrush(color), x, y, TextBoxTextRenderer.sf_nonprinting);
					return;
				}
				g.DrawString(text, font, ThemeEngine.Current.ResPool.GetSolidBrush(color), x, y, TextBoxTextRenderer.sf_printing);
				return;
			}
			else
			{
				if (showNonPrint)
				{
					TextRenderer.DrawTextInternal(g, text, font, new Rectangle(new Point((int)x, (int)y), TextBoxTextRenderer.max_size), color, TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding, false);
					return;
				}
				TextRenderer.DrawTextInternal(g, text, font, new Rectangle(new Point((int)x, (int)y), TextBoxTextRenderer.max_size), color, TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding, false);
				return;
			}
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x00049078 File Offset: 0x00047278
		public static SizeF MeasureText(Graphics g, string text, Font font)
		{
			if (text.Length == 1)
			{
				RectangleF visibleClipBounds = g.VisibleClipBounds;
				string text2;
				if (visibleClipBounds.Width == 1f && visibleClipBounds.Height == 1f && visibleClipBounds.X == 0f && visibleClipBounds.Y == 0f)
				{
					text2 = "-1x1|";
				}
				else
				{
					text2 = "|";
				}
				string text3 = font.GetHashCode().ToString() + text2 + text;
				if (TextBoxTextRenderer.measure_cache.ContainsKey(text3))
				{
					return (SizeF)TextBoxTextRenderer.measure_cache[text3];
				}
				SizeF sizeF;
				if (!TextBoxTextRenderer.use_textrenderer)
				{
					sizeF = g.MeasureString(text, font, 10000, TextBoxTextRenderer.sf_nonprinting);
				}
				else
				{
					sizeF = TextRenderer.MeasureTextInternal(g, text, font, Size.Empty, TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding, false);
				}
				TextBoxTextRenderer.measure_cache[text3] = sizeF;
				return sizeF;
			}
			else
			{
				if (!TextBoxTextRenderer.use_textrenderer)
				{
					return g.MeasureString(text, font, 10000, TextBoxTextRenderer.sf_nonprinting);
				}
				return TextRenderer.MeasureTextInternal(g, text, font, Size.Empty, TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding, false);
			}
		}

		// Token: 0x04000A7C RID: 2684
		private static Size max_size;

		// Token: 0x04000A7D RID: 2685
		private static bool use_textrenderer;

		// Token: 0x04000A7E RID: 2686
		private static StringFormat sf_nonprinting;

		// Token: 0x04000A7F RID: 2687
		private static StringFormat sf_printing;

		// Token: 0x04000A80 RID: 2688
		private static Hashtable measure_cache;
	}
}
