using System;

namespace System.Drawing
{
	// Token: 0x0200006B RID: 107
	internal struct GdiplusStartupOutput
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x0000D0D4 File Offset: 0x0000B2D4
		internal static GdiplusStartupOutput MakeGdiplusStartupOutput()
		{
			GdiplusStartupOutput gdiplusStartupOutput = default(GdiplusStartupOutput);
			gdiplusStartupOutput.NotificationHook = (gdiplusStartupOutput.NotificationUnhook = IntPtr.Zero);
			return gdiplusStartupOutput;
		}

		// Token: 0x040001F1 RID: 497
		internal IntPtr NotificationHook;

		// Token: 0x040001F2 RID: 498
		internal IntPtr NotificationUnhook;
	}
}
