using System;
using System.Drawing;

namespace System.Windows.Forms.VisualStyles
{
	// Token: 0x02000307 RID: 775
	internal interface IVisualStyles
	{
		// Token: 0x06001BCA RID: 7114
		int UxThemeCloseThemeData(IntPtr hTheme);

		// Token: 0x06001BCB RID: 7115
		int UxThemeDrawThemeBackground(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds);

		// Token: 0x06001BCC RID: 7116
		int UxThemeDrawThemeBackground(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, Rectangle clipRectangle);

		// Token: 0x06001BCD RID: 7117
		int UxThemeDrawThemeText(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, string text, TextFormatFlags textFlags, Rectangle bounds);

		// Token: 0x06001BCE RID: 7118
		int UxThemeGetThemeBackgroundRegion(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, out Region result);

		// Token: 0x06001BCF RID: 7119
		int UxThemeGetThemeColor(IntPtr hTheme, int iPartId, int iStateId, ColorProperty prop, out Color result);

		// Token: 0x06001BD0 RID: 7120
		int UxThemeGetThemePartSize(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, ThemeSizeType type, out Size result);

		// Token: 0x06001BD1 RID: 7121
		int UxThemeGetThemeTextExtent(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, string textToDraw, TextFormatFlags flags, Rectangle bounds, out Rectangle result);

		// Token: 0x06001BD2 RID: 7122
		bool UxThemeIsAppThemed();

		// Token: 0x06001BD3 RID: 7123
		bool UxThemeIsThemeActive();

		// Token: 0x06001BD4 RID: 7124
		bool UxThemeIsThemeBackgroundPartiallyTransparent(IntPtr hTheme, int iPartId, int iStateId);

		// Token: 0x06001BD5 RID: 7125
		bool UxThemeIsThemePartDefined(IntPtr hTheme, int iPartId);

		// Token: 0x06001BD6 RID: 7126
		IntPtr UxThemeOpenThemeData(IntPtr hWnd, string classList);

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001BD7 RID: 7127
		string VisualStyleInformationColorScheme { get; }

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001BD8 RID: 7128
		string VisualStyleInformationFileName { get; }

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001BD9 RID: 7129
		bool VisualStyleInformationIsSupportedByOS { get; }

		// Token: 0x06001BDA RID: 7130
		void VisualStyleRendererDrawBackgroundExcludingArea(IntPtr theme, IDeviceContext dc, int part, int state, Rectangle bounds, Rectangle excludedArea);
	}
}
