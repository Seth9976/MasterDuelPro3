using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms.VisualStyles
{
	// Token: 0x0200030A RID: 778
	internal class UXTheme
	{
		// Token: 0x06001BDB RID: 7131
		[DllImport("uxtheme", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int CloseThemeData(IntPtr hTheme);

		// Token: 0x06001BDC RID: 7132
		[DllImport("uxtheme", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int DrawThemeBackground(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref XplatUIWin32.RECT pRect, ref XplatUIWin32.RECT pClipRect);

		// Token: 0x06001BDD RID: 7133
		[DllImport("uxtheme", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int DrawThemeBackground(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref XplatUIWin32.RECT pRect, IntPtr pClipRect);

		// Token: 0x06001BDE RID: 7134
		[DllImport("uxtheme", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int DrawThemeText(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int textLength, uint textFlags, uint textFlags2, ref XplatUIWin32.RECT pRect);

		// Token: 0x06001BDF RID: 7135
		[DllImport("uxtheme", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern IntPtr OpenThemeData(IntPtr hWnd, string classList);

		// Token: 0x06001BE0 RID: 7136
		[DllImport("uxtheme", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int GetThemeBackgroundRegion(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref XplatUIWin32.RECT pRect, out IntPtr pRegion);

		// Token: 0x06001BE1 RID: 7137
		[DllImport("uxtheme", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int GetThemeColor(IntPtr hTheme, int iPartId, int iStateId, int iPropId, out int pColor);

		// Token: 0x06001BE2 RID: 7138
		[DllImport("uxtheme", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int GetThemePartSize(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, IntPtr pRect, int eSize, out UXTheme.SIZE size);

		// Token: 0x06001BE3 RID: 7139
		[DllImport("uxtheme", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int GetThemeTextExtent(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int textLength, int textFlags, ref XplatUIWin32.RECT boundingRect, out XplatUIWin32.RECT extentRect);

		// Token: 0x06001BE4 RID: 7140
		[DllImport("uxtheme", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int IsThemeBackgroundPartiallyTransparent(IntPtr hTheme, int iPartId, int iStateId);

		// Token: 0x06001BE5 RID: 7141
		[DllImport("uxtheme", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern bool IsThemePartDefined(IntPtr hTheme, int iPartId, int iStateId);

		// Token: 0x06001BE6 RID: 7142
		[DllImport("uxtheme", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern bool IsThemeActive();

		// Token: 0x06001BE7 RID: 7143
		[DllImport("uxtheme", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern bool IsAppThemed();

		// Token: 0x06001BE8 RID: 7144
		[DllImport("uxtheme", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int GetCurrentThemeName(StringBuilder stringThemeName, int lengthThemeName, StringBuilder stringColorName, int lengthColorName, StringBuilder stringSizeName, int lengthSizeName);

		// Token: 0x0200030B RID: 779
		public struct SIZE
		{
			// Token: 0x06001BE9 RID: 7145 RVA: 0x0008645C File Offset: 0x0008465C
			public Size ToSize()
			{
				return new Size(this.cx, this.cy);
			}

			// Token: 0x0400174C RID: 5964
			public int cx;

			// Token: 0x0400174D RID: 5965
			public int cy;
		}
	}
}
