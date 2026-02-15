using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200022D RID: 557
	internal class X11DesktopColors
	{
		// Token: 0x060016A9 RID: 5801 RVA: 0x00070724 File Offset: 0x0006E924
		static X11DesktopColors()
		{
			X11DesktopColors.FindDesktopEnvironment();
			X11DesktopColors.Desktop desktop = X11DesktopColors.desktop;
			if (desktop != X11DesktopColors.Desktop.Gtk)
			{
				if (desktop != X11DesktopColors.Desktop.KDE)
				{
					return;
				}
			}
			else
			{
				try
				{
					X11DesktopColors.GtkInit();
					IntPtr intPtr = X11DesktopColors.gtk_invisible_new();
					X11DesktopColors.gtk_widget_ensure_style(intPtr);
					X11DesktopColors.GtkStyleStruct gtkStyleStruct = (X11DesktopColors.GtkStyleStruct)Marshal.PtrToStructure(X11DesktopColors.gtk_widget_get_style(intPtr), typeof(X11DesktopColors.GtkStyleStruct));
					ThemeEngine.Current.ColorControl = X11DesktopColors.ColorFromGdkColor(gtkStyleStruct.bg[0]);
					ThemeEngine.Current.ColorControlText = X11DesktopColors.ColorFromGdkColor(gtkStyleStruct.fg[0]);
					ThemeEngine.Current.ColorControlDark = X11DesktopColors.ColorFromGdkColor(gtkStyleStruct.dark[0]);
					ThemeEngine.Current.ColorControlLight = X11DesktopColors.ColorFromGdkColor(gtkStyleStruct.light[0]);
					ThemeEngine.Current.ColorControlLightLight = ControlPaint.Light(ThemeEngine.Current.ColorControlLight);
					ThemeEngine.Current.ColorControlDarkDark = ControlPaint.Dark(ThemeEngine.Current.ColorControlDark);
					if (ThemeEngine.Current.ColorControlLight.ToArgb() == Color.White.ToArgb())
					{
						ThemeEngine.Current.ColorControlLight = Color.FromArgb(255, 227, 227, 227);
					}
					IntPtr intPtr2 = X11DesktopColors.gtk_menu_new();
					X11DesktopColors.gtk_widget_ensure_style(intPtr2);
					gtkStyleStruct = (X11DesktopColors.GtkStyleStruct)Marshal.PtrToStructure(X11DesktopColors.gtk_widget_get_style(intPtr2), typeof(X11DesktopColors.GtkStyleStruct));
					ThemeEngine.Current.ColorMenu = X11DesktopColors.ColorFromGdkColor(gtkStyleStruct.bg[0]);
					ThemeEngine.Current.ColorMenuText = X11DesktopColors.ColorFromGdkColor(gtkStyleStruct.text[0]);
					return;
				}
				catch (DllNotFoundException)
				{
					Console.Error.WriteLine("Gtk not found (missing LD_LIBRARY_PATH to libgtk-x11-2.0.so.0?), using built-in colorscheme");
					return;
				}
				catch
				{
					Console.Error.WriteLine("Gtk colorscheme read failure, using built-in colorscheme");
					return;
				}
			}
			if (!X11DesktopColors.ReadKDEColorsheme())
			{
				Console.Error.WriteLine("KDE colorscheme read failure, using built-in colorscheme");
			}
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00070920 File Offset: 0x0006EB20
		private static void GtkInit()
		{
			X11DesktopColors.gtk_init_check(IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00070934 File Offset: 0x0006EB34
		private static void FindDesktopEnvironment()
		{
			X11DesktopColors.desktop = X11DesktopColors.Desktop.Gtk;
			string text = Environment.GetEnvironmentVariable("DESKTOP_SESSION");
			if (text != null)
			{
				text = text.ToUpper();
				if (text == "DEFAULT")
				{
					if (Environment.GetEnvironmentVariable("KDE_FULL_SESSION") != null)
					{
						X11DesktopColors.desktop = X11DesktopColors.Desktop.KDE;
						return;
					}
				}
				else if (text.StartsWith("KDE"))
				{
					X11DesktopColors.desktop = X11DesktopColors.Desktop.KDE;
				}
			}
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0000493C File Offset: 0x00002B3C
		internal static void Initialize()
		{
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x0007098F File Offset: 0x0006EB8F
		private static Color ColorFromGdkColor(X11DesktopColors.GdkColorStruct gtkcolor)
		{
			return Color.FromArgb(255, (gtkcolor.red >> 8) & 255, (gtkcolor.green >> 8) & 255, (gtkcolor.blue >> 8) & 255);
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x000709C8 File Offset: 0x0006EBC8
		private static bool ReadKDEColorsheme()
		{
			string text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/.kde/share/config/kdeglobals";
			if (!File.Exists(text))
			{
				return false;
			}
			StreamReader streamReader = new StreamReader(text);
			for (string text2 = streamReader.ReadLine(); text2 != null; text2 = streamReader.ReadLine())
			{
				text2 = text2.Trim();
				if (text2.StartsWith("background="))
				{
					Color color = X11DesktopColors.GetColorFromKDEString(text2);
					if (color != Color.Empty)
					{
						ThemeEngine.Current.ColorControl = color;
						ThemeEngine.Current.ColorMenu = color;
					}
				}
				else if (text2.StartsWith("foreground="))
				{
					Color color = X11DesktopColors.GetColorFromKDEString(text2);
					if (color != Color.Empty)
					{
						ThemeEngine.Current.ColorControlText = color;
						ThemeEngine.Current.ColorMenuText = color;
					}
				}
				else if (text2.StartsWith("selectBackground"))
				{
					Color color = X11DesktopColors.GetColorFromKDEString(text2);
					if (color != Color.Empty)
					{
						ThemeEngine.Current.ColorHighlight = color;
					}
				}
				else if (text2.StartsWith("selectForeground"))
				{
					Color color = X11DesktopColors.GetColorFromKDEString(text2);
					if (color != Color.Empty)
					{
						ThemeEngine.Current.ColorHighlightText = color;
					}
				}
			}
			streamReader.Close();
			return true;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00070AF0 File Offset: 0x0006ECF0
		private static Color GetColorFromKDEString(string line)
		{
			string[] array = line.Split(new char[] { '=' });
			if (array.Length != 0)
			{
				line = array[1];
				array = line.Split(new char[] { ',' });
				if (array.Length == 3)
				{
					int num = Convert.ToInt32(array[0]);
					int num2 = Convert.ToInt32(array[1]);
					int num3 = Convert.ToInt32(array[2]);
					return Color.FromArgb(num, num2, num3);
				}
			}
			return Color.Empty;
		}

		// Token: 0x060016B0 RID: 5808
		[DllImport("libgtk-x11-2.0")]
		private static extern bool gtk_init_check(IntPtr argc, IntPtr argv);

		// Token: 0x060016B1 RID: 5809
		[DllImport("libgtk-x11-2.0")]
		private static extern IntPtr gtk_invisible_new();

		// Token: 0x060016B2 RID: 5810
		[DllImport("libgtk-x11-2.0")]
		private static extern IntPtr gtk_menu_new();

		// Token: 0x060016B3 RID: 5811
		[DllImport("libgtk-x11-2.0")]
		private static extern void gtk_widget_ensure_style(IntPtr raw);

		// Token: 0x060016B4 RID: 5812
		[DllImport("libgtk-x11-2.0")]
		private static extern IntPtr gtk_widget_get_style(IntPtr raw);

		// Token: 0x04000DC1 RID: 3521
		private static X11DesktopColors.Desktop desktop;

		// Token: 0x0200022E RID: 558
		internal struct GdkColorStruct
		{
			// Token: 0x04000DC2 RID: 3522
			internal int pixel;

			// Token: 0x04000DC3 RID: 3523
			internal short red;

			// Token: 0x04000DC4 RID: 3524
			internal short green;

			// Token: 0x04000DC5 RID: 3525
			internal short blue;
		}

		// Token: 0x0200022F RID: 559
		internal struct GObjectStruct
		{
			// Token: 0x04000DC6 RID: 3526
			public IntPtr Instance;

			// Token: 0x04000DC7 RID: 3527
			public IntPtr ref_count;

			// Token: 0x04000DC8 RID: 3528
			public IntPtr data;
		}

		// Token: 0x02000230 RID: 560
		internal struct GtkStyleStruct
		{
			// Token: 0x04000DC9 RID: 3529
			internal X11DesktopColors.GObjectStruct obj;

			// Token: 0x04000DCA RID: 3530
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			internal X11DesktopColors.GdkColorStruct[] fg;

			// Token: 0x04000DCB RID: 3531
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			internal X11DesktopColors.GdkColorStruct[] bg;

			// Token: 0x04000DCC RID: 3532
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			internal X11DesktopColors.GdkColorStruct[] light;

			// Token: 0x04000DCD RID: 3533
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			internal X11DesktopColors.GdkColorStruct[] dark;

			// Token: 0x04000DCE RID: 3534
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			internal X11DesktopColors.GdkColorStruct[] mid;

			// Token: 0x04000DCF RID: 3535
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			internal X11DesktopColors.GdkColorStruct[] text;

			// Token: 0x04000DD0 RID: 3536
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			internal X11DesktopColors.GdkColorStruct[] baseclr;

			// Token: 0x04000DD1 RID: 3537
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
			internal X11DesktopColors.GdkColorStruct[] text_aa;

			// Token: 0x04000DD2 RID: 3538
			internal X11DesktopColors.GdkColorStruct black;

			// Token: 0x04000DD3 RID: 3539
			internal X11DesktopColors.GdkColorStruct white;
		}

		// Token: 0x02000231 RID: 561
		private enum Desktop
		{
			// Token: 0x04000DD5 RID: 3541
			Gtk,
			// Token: 0x04000DD6 RID: 3542
			KDE,
			// Token: 0x04000DD7 RID: 3543
			Unknown
		}
	}
}
