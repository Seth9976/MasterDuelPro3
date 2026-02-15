using System;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms.VisualStyles
{
	// Token: 0x02000370 RID: 880
	internal class VisualStylesNative : IVisualStyles
	{
		// Token: 0x06001CD0 RID: 7376 RVA: 0x00087F75 File Offset: 0x00086175
		public int UxThemeCloseThemeData(IntPtr hTheme)
		{
			return UXTheme.CloseThemeData(hTheme);
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x00087F80 File Offset: 0x00086180
		public int UxThemeDrawThemeBackground(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(bounds);
			int num = UXTheme.DrawThemeBackground(hTheme, dc.GetHdc(), iPartId, iStateId, ref rect, IntPtr.Zero);
			dc.ReleaseHdc();
			return num;
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x00087FB4 File Offset: 0x000861B4
		public int UxThemeDrawThemeBackground(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, Rectangle clipRectangle)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(bounds);
			XplatUIWin32.RECT rect2 = XplatUIWin32.RECT.FromRectangle(clipRectangle);
			int num = UXTheme.DrawThemeBackground(hTheme, dc.GetHdc(), iPartId, iStateId, ref rect, ref rect2);
			dc.ReleaseHdc();
			return num;
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x00087FEC File Offset: 0x000861EC
		public int UxThemeDrawThemeText(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, string text, TextFormatFlags textFlags, Rectangle bounds)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(bounds);
			int num = UXTheme.DrawThemeText(hTheme, dc.GetHdc(), iPartId, iStateId, text, text.Length, (uint)textFlags, 0U, ref rect);
			dc.ReleaseHdc();
			return num;
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x00088024 File Offset: 0x00086224
		public int UxThemeGetThemeBackgroundRegion(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, out Region result)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(bounds);
			IntPtr intPtr;
			int themeBackgroundRegion = UXTheme.GetThemeBackgroundRegion(hTheme, dc.GetHdc(), iPartId, iStateId, ref rect, out intPtr);
			dc.ReleaseHdc();
			result = Region.FromHrgn(intPtr);
			return themeBackgroundRegion;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0008805C File Offset: 0x0008625C
		public int UxThemeGetThemeColor(IntPtr hTheme, int iPartId, int iStateId, ColorProperty prop, out Color result)
		{
			int num;
			int themeColor = UXTheme.GetThemeColor(hTheme, iPartId, iStateId, (int)prop, out num);
			result = Color.FromArgb((int)(255L & (long)num), (int)(65280L & (long)num) >> 8, (int)(16711680L & (long)num) >> 16);
			return themeColor;
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x000880A4 File Offset: 0x000862A4
		public int UxThemeGetThemePartSize(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, ThemeSizeType type, out Size result)
		{
			UXTheme.SIZE size;
			int themePartSize = UXTheme.GetThemePartSize(hTheme, dc.GetHdc(), iPartId, iStateId, IntPtr.Zero, (int)type, out size);
			dc.ReleaseHdc();
			result = size.ToSize();
			return themePartSize;
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x000880E0 File Offset: 0x000862E0
		public int UxThemeGetThemeTextExtent(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, string textToDraw, TextFormatFlags flags, Rectangle bounds, out Rectangle result)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(bounds);
			XplatUIWin32.RECT rect2;
			int themeTextExtent = UXTheme.GetThemeTextExtent(hTheme, dc.GetHdc(), iPartId, iStateId, textToDraw, textToDraw.Length, (int)flags, ref rect, out rect2);
			dc.ReleaseHdc();
			result = rect2.ToRectangle();
			return themeTextExtent;
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x00088127 File Offset: 0x00086327
		public bool UxThemeIsAppThemed()
		{
			return UXTheme.IsAppThemed();
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0008812E File Offset: 0x0008632E
		public bool UxThemeIsThemeActive()
		{
			return UXTheme.IsThemeActive();
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x00088135 File Offset: 0x00086335
		public bool UxThemeIsThemePartDefined(IntPtr hTheme, int iPartId)
		{
			return UXTheme.IsThemePartDefined(hTheme, iPartId, 0);
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0008813F File Offset: 0x0008633F
		public bool UxThemeIsThemeBackgroundPartiallyTransparent(IntPtr hTheme, int iPartId, int iStateId)
		{
			return UXTheme.IsThemeBackgroundPartiallyTransparent(hTheme, iPartId, iStateId) != 0;
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0008814E File Offset: 0x0008634E
		public IntPtr UxThemeOpenThemeData(IntPtr hWnd, string classList)
		{
			return UXTheme.OpenThemeData(hWnd, classList);
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x00088158 File Offset: 0x00086358
		public string VisualStyleInformationColorScheme
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(260);
				StringBuilder stringBuilder2 = new StringBuilder(260);
				StringBuilder stringBuilder3 = new StringBuilder(260);
				UXTheme.GetCurrentThemeName(stringBuilder, stringBuilder.Capacity, stringBuilder2, stringBuilder2.Capacity, stringBuilder3, stringBuilder3.Capacity);
				return stringBuilder2.ToString();
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001CDE RID: 7390 RVA: 0x000881A8 File Offset: 0x000863A8
		public string VisualStyleInformationFileName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(260);
				StringBuilder stringBuilder2 = new StringBuilder(260);
				StringBuilder stringBuilder3 = new StringBuilder(260);
				UXTheme.GetCurrentThemeName(stringBuilder, stringBuilder.Capacity, stringBuilder2, stringBuilder2.Capacity, stringBuilder3, stringBuilder3.Capacity);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x000881F5 File Offset: 0x000863F5
		public bool VisualStyleInformationIsSupportedByOS
		{
			get
			{
				return VisualStylesNative.IsSupported();
			}
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x000881FC File Offset: 0x000863FC
		public static bool IsSupported()
		{
			return Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version >= new Version(5, 1);
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x00088228 File Offset: 0x00086428
		public void VisualStyleRendererDrawBackgroundExcludingArea(IntPtr theme, IDeviceContext dc, int part, int state, Rectangle bounds, Rectangle excludedArea)
		{
			XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(bounds);
			IntPtr hdc = dc.GetHdc();
			XplatUIWin32.Win32ExcludeClipRect(hdc, excludedArea.Left, excludedArea.Top, excludedArea.Right, excludedArea.Bottom);
			UXTheme.DrawThemeBackground(theme, hdc, part, state, ref rect, IntPtr.Zero);
			IntPtr intPtr = XplatUIWin32.Win32CreateRectRgn(excludedArea.Left, excludedArea.Top, excludedArea.Right, excludedArea.Bottom);
			XplatUIWin32.Win32ExtSelectClipRgn(hdc, intPtr, 2);
			XplatUIWin32.Win32DeleteObject(intPtr);
			dc.ReleaseHdc();
		}
	}
}
