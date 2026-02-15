using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs
{
	// Token: 0x0200000E RID: 14
	public static class Glass
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public static bool OSSupportsDwmComposition
		{
			get
			{
				return NativeMethods.IsWindowsVistaOrLater;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003CC8 File Offset: 0x00001EC8
		public static bool IsDwmCompositionEnabled
		{
			get
			{
				return Glass.OSSupportsDwmComposition && NativeMethods.DwmIsCompositionEnabled();
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003CEC File Offset: 0x00001EEC
		public static void ExtendFrameIntoClientArea(this IWin32Window window, Padding glassMargin)
		{
			bool flag = !Glass.IsDwmCompositionEnabled;
			if (flag)
			{
				throw new NotSupportedException(Resources.GlassNotSupportedError);
			}
			bool flag2 = window == null;
			if (flag2)
			{
				throw new ArgumentNullException("window");
			}
			NativeMethods.MARGINS margins = new NativeMethods.MARGINS(glassMargin);
			NativeMethods.DwmExtendFrameIntoClientArea(window.Handle, ref margins);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003D3C File Offset: 0x00001F3C
		public static void DrawCompositedText(IDeviceContext dc, string text, Font font, Rectangle bounds, Padding padding, Color foreColor, int glowSize, TextFormatFlags textFormat)
		{
			bool flag = !Glass.IsDwmCompositionEnabled;
			if (flag)
			{
				throw new NotSupportedException(Resources.GlassNotSupportedError);
			}
			bool flag2 = dc == null;
			if (flag2)
			{
				throw new ArgumentNullException("dc");
			}
			bool flag3 = text == null;
			if (flag3)
			{
				throw new ArgumentNullException("text");
			}
			bool flag4 = font == null;
			if (flag4)
			{
				throw new ArgumentNullException("font");
			}
			IntPtr hdc = dc.GetHdc();
			try
			{
				using (SafeDeviceHandle safeDeviceHandle = NativeMethods.CreateCompatibleDC(hdc))
				{
					using (SafeGDIHandle safeGDIHandle = new SafeGDIHandle(font.ToHfont(), true))
					{
						using (NativeMethods.CreateDib(bounds, hdc, safeDeviceHandle))
						{
							NativeMethods.SelectObject(safeDeviceHandle, safeGDIHandle);
							VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
							NativeMethods.DTTOPTS dttopts = default(NativeMethods.DTTOPTS);
							dttopts.dwSize = Marshal.SizeOf(typeof(NativeMethods.DTTOPTS));
							dttopts.dwFlags = NativeMethods.DrawThemeTextFlags.TextColor | NativeMethods.DrawThemeTextFlags.GlowSize | NativeMethods.DrawThemeTextFlags.Composited;
							dttopts.crText = ColorTranslator.ToWin32(foreColor);
							dttopts.iGlowSize = glowSize;
							NativeMethods.RECT rect = new NativeMethods.RECT(padding.Left, padding.Top, bounds.Width - padding.Right, bounds.Height - padding.Bottom);
							NativeMethods.DrawThemeTextEx(visualStyleRenderer.Handle, safeDeviceHandle, 0, 0, text, text.Length, textFormat, ref rect, ref dttopts);
							NativeMethods.BitBlt(hdc, bounds.Left, bounds.Top, bounds.Width, bounds.Height, safeDeviceHandle, 0, 0, 13369376U);
						}
					}
				}
			}
			finally
			{
				dc.ReleaseHdc();
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003F3C File Offset: 0x0000213C
		public static Size MeasureCompositedText(IDeviceContext dc, string text, Font font, TextFormatFlags textFormat)
		{
			bool flag = !Glass.IsDwmCompositionEnabled;
			if (flag)
			{
				throw new NotSupportedException(Resources.GlassNotSupportedError);
			}
			bool flag2 = dc == null;
			if (flag2)
			{
				throw new ArgumentNullException("dc");
			}
			bool flag3 = text == null;
			if (flag3)
			{
				throw new ArgumentNullException("text");
			}
			bool flag4 = font == null;
			if (flag4)
			{
				throw new ArgumentNullException("font");
			}
			IntPtr hdc = dc.GetHdc();
			Size size;
			try
			{
				Rectangle rectangle;
				rectangle..ctor(0, 0, int.MaxValue, int.MaxValue);
				using (SafeDeviceHandle safeDeviceHandle = NativeMethods.CreateCompatibleDC(hdc))
				{
					using (SafeGDIHandle safeGDIHandle = new SafeGDIHandle(font.ToHfont(), true))
					{
						using (NativeMethods.CreateDib(rectangle, hdc, safeDeviceHandle))
						{
							NativeMethods.SelectObject(safeDeviceHandle, safeGDIHandle);
							VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
							NativeMethods.RECT rect = new NativeMethods.RECT(rectangle);
							NativeMethods.RECT rect2;
							NativeMethods.GetThemeTextExtent(visualStyleRenderer.Handle, safeDeviceHandle, 0, 0, text, text.Length, textFormat, ref rect, out rect2);
							size = new Size(rect2.Right - rect2.Left, rect2.Bottom - rect2.Top);
						}
					}
				}
			}
			finally
			{
				dc.ReleaseHdc();
			}
			return size;
		}
	}
}
