using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms.Theming.Default;

namespace System.Windows.Forms.Theming
{
	// Token: 0x02000371 RID: 881
	internal class ThemeElements
	{
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001CE3 RID: 7395 RVA: 0x000882B1 File Offset: 0x000864B1
		public static ThemeElementsDefault CurrentTheme
		{
			get
			{
				return ThemeElements.theme;
			}
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x000882B8 File Offset: 0x000864B8
		static ThemeElements()
		{
			string text = Environment.GetEnvironmentVariable("MONO_THEME");
			if (text == null)
			{
				text = "win32";
			}
			else
			{
				text = text.ToLower();
			}
			ThemeElements.theme = ThemeElements.LoadTheme(text);
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x000882F0 File Offset: 0x000864F0
		private static ThemeElementsDefault LoadTheme(string themeName)
		{
			if (!(themeName == "visualstyles"))
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				string text = typeof(ThemeElements).FullName + themeName;
				Type type = executingAssembly.GetType(text, false, true);
				if (type != null)
				{
					object obj = executingAssembly.CreateInstance(type.FullName);
					if (obj != null)
					{
						return (ThemeElementsDefault)obj;
					}
				}
				return new ThemeElementsDefault();
			}
			if (Application.VisualStylesEnabled)
			{
				return new ThemeElementsVisualStyles();
			}
			return new ThemeElementsDefault();
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x00088368 File Offset: 0x00086568
		public static void DrawButton(Graphics g, Rectangle bounds, ButtonThemeState state, Color backColor, Color foreColor)
		{
			ThemeElements.theme.ButtonPainter.Draw(g, bounds, state, backColor, foreColor);
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0008837F File Offset: 0x0008657F
		public static void DrawFlatButton(Graphics g, Rectangle bounds, ButtonThemeState state, Color backColor, Color foreColor, FlatButtonAppearance appearance)
		{
			ThemeElements.theme.ButtonPainter.DrawFlat(g, bounds, state, backColor, foreColor, appearance);
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x00088398 File Offset: 0x00086598
		public static void DrawPopupButton(Graphics g, Rectangle bounds, ButtonThemeState state, Color backColor, Color foreColor)
		{
			ThemeElements.theme.ButtonPainter.DrawPopup(g, bounds, state, backColor, foreColor);
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x000883AF File Offset: 0x000865AF
		public static LabelPainter LabelPainter
		{
			get
			{
				return ThemeElements.theme.LabelPainter;
			}
		}

		// Token: 0x04001836 RID: 6198
		private static ThemeElementsDefault theme;
	}
}
