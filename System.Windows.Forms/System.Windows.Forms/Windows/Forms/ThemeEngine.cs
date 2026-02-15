using System;

namespace System.Windows.Forms
{
	// Token: 0x020001A8 RID: 424
	internal class ThemeEngine
	{
		// Token: 0x060010F4 RID: 4340 RVA: 0x00050BC8 File Offset: 0x0004EDC8
		static ThemeEngine()
		{
			string text = Environment.GetEnvironmentVariable("MONO_THEME");
			if (text != null)
			{
				text = text.ToLower();
			}
			if (Application.VisualStylesEnabled)
			{
				ThemeEngine.theme = new ThemeVisualStyles();
				return;
			}
			ThemeEngine.theme = new ThemeWin32Classic();
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00050C0E File Offset: 0x0004EE0E
		public static Theme Current
		{
			get
			{
				return ThemeEngine.theme;
			}
		}

		// Token: 0x04000B2B RID: 2859
		private static Theme theme;
	}
}
