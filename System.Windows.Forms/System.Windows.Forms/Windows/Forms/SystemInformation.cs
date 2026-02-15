using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides information about the current system environment.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200018E RID: 398
	public class SystemInformation
	{
		/// <summary>Gets the standard size, in pixels, of a button in a window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the standard dimensions, in pixels, of a button in a window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x00044609 File Offset: 0x00042809
		public static Size CaptionButtonSize
		{
			get
			{
				return ThemeEngine.Current.CaptionButtonSize;
			}
		}

		/// <summary>Gets the height, in pixels, of the standard title bar area of a window.</summary>
		/// <returns>The height, in pixels, of the standard title bar area of a window.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x00044615 File Offset: 0x00042815
		public static int CaptionHeight
		{
			get
			{
				return ThemeEngine.Current.CaptionHeight;
			}
		}

		/// <summary>Gets the dimensions, in pixels, of the area within which the user must click twice for the operating system to consider the two clicks a double-click.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of the area within which the user must click twice for the operating system to consider the two clicks a double-click.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x00044621 File Offset: 0x00042821
		public static Size DoubleClickSize
		{
			get
			{
				return ThemeEngine.Current.DoubleClickSize;
			}
		}

		/// <summary>Gets the maximum number of milliseconds that can elapse between a first click and a second click for the OS to consider the mouse action a double-click.</summary>
		/// <returns>The maximum amount of time, in milliseconds, that can elapse between a first click and a second click for the OS to consider the mouse action a double-click.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x0004462D File Offset: 0x0004282D
		public static int DoubleClickTime
		{
			get
			{
				return ThemeEngine.Current.DoubleClickTime;
			}
		}

		/// <summary>Gets the width and height of a rectangle centered on the point the mouse button was pressed, within which a drag operation will not begin.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the area of a rectangle, in pixels, centered on the point the mouse button was pressed, within which a drag operation will not begin.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x00044639 File Offset: 0x00042839
		public static Size DragSize
		{
			get
			{
				return XplatUI.DragSize;
			}
		}

		/// <summary>Gets the thickness, in pixels, of the resizing border that is drawn around the perimeter of a window that is being drag resized.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the thickness, in pixels, of the width of a vertical resizing border and the height of a horizontal resizing border.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00044640 File Offset: 0x00042840
		public static Size FrameBorderSize
		{
			get
			{
				return ThemeEngine.Current.FrameBorderSize;
			}
		}

		/// <summary>Gets the default height, in pixels, of the horizontal scroll bar.</summary>
		/// <returns>The default height, in pixels, of the horizontal scroll bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x0004464C File Offset: 0x0004284C
		public static int HorizontalScrollBarHeight
		{
			get
			{
				return ThemeEngine.Current.HorizontalScrollBarHeight;
			}
		}

		/// <summary>Gets a value indicating whether menu access keys are always underlined.</summary>
		/// <returns>true if menu access keys are always underlined; false if they are underlined only when the menu is activated or receives focus.</returns>
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x00044658 File Offset: 0x00042858
		public static bool MenuAccessKeysUnderlined
		{
			get
			{
				return ThemeEngine.Current.MenuAccessKeysUnderlined;
			}
		}

		/// <summary>Gets the default dimensions, in pixels, of menu-bar buttons.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the default dimensions, in pixels, of menu-bar buttons.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00044664 File Offset: 0x00042864
		public static Size MenuButtonSize
		{
			get
			{
				return ThemeEngine.Current.MenuButtonSize;
			}
		}

		/// <summary>Gets the height, in pixels, of one line of a menu.</summary>
		/// <returns>The height, in pixels, of one line of a menu.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x00044670 File Offset: 0x00042870
		public static int MenuHeight
		{
			get
			{
				return ThemeEngine.Current.MenuHeight;
			}
		}

		/// <summary>Gets the dimensions, in pixels, of a normal minimized window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of a normal minimized window.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x0004467C File Offset: 0x0004287C
		public static Size MinimizedWindowSize
		{
			get
			{
				return XplatUI.MinimizedWindowSize;
			}
		}

		/// <summary>Gets the minimum width and height for a window, in pixels.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the minimum allowable dimensions of a window, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x00044683 File Offset: 0x00042883
		public static Size MinimumWindowSize
		{
			get
			{
				return XplatUI.MinimumWindowSize;
			}
		}

		/// <summary>Gets the default minimum dimensions, in pixels, that a window may occupy during a drag resize.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the default minimum width and height of a window during resize, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x0004468A File Offset: 0x0004288A
		public static Size MinWindowTrackSize
		{
			get
			{
				return XplatUI.MinWindowTrackSize;
			}
		}

		/// <summary>Gets the number of lines to scroll when the mouse wheel is rotated.</summary>
		/// <returns>The number of lines to scroll on a mouse wheel rotation, or -1 if the "One screen at a time" mouse option is selected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x00044691 File Offset: 0x00042891
		public static int MouseWheelScrollLines
		{
			get
			{
				return ThemeEngine.Current.MouseWheelScrollLines;
			}
		}

		/// <summary>Gets the dimensions, in pixels, of small caption buttons.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of small caption buttons.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0004469D File Offset: 0x0004289D
		public static Size ToolWindowCaptionButtonSize
		{
			get
			{
				return ThemeEngine.Current.ToolWindowCaptionButtonSize;
			}
		}

		/// <summary>Gets the height, in pixels, of a tool window caption.</summary>
		/// <returns>The height, in pixels, of a tool window caption in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x000446A9 File Offset: 0x000428A9
		public static int ToolWindowCaptionHeight
		{
			get
			{
				return ThemeEngine.Current.ToolWindowCaptionHeight;
			}
		}

		/// <summary>Gets a value indicating whether the current process is running in user-interactive mode.</summary>
		/// <returns>true if the current process is running in user-interactive mode; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x000446B5 File Offset: 0x000428B5
		public static bool UserInteractive
		{
			get
			{
				return Environment.UserInteractive;
			}
		}

		/// <summary>Gets the default width, in pixels, of the vertical scroll bar.</summary>
		/// <returns>The default width, in pixels, of the vertical scroll bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x000446BC File Offset: 0x000428BC
		public static int VerticalScrollBarWidth
		{
			get
			{
				return ThemeEngine.Current.VerticalScrollBarWidth;
			}
		}

		/// <summary>Gets the bounds of the virtual screen.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the bounding rectangle of the entire virtual screen.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x000446C8 File Offset: 0x000428C8
		public static Rectangle VirtualScreen
		{
			get
			{
				Rectangle rectangle = default(Rectangle);
				foreach (Screen screen in Screen.AllScreens)
				{
					rectangle = Rectangle.Union(rectangle, screen.Bounds);
				}
				return rectangle;
			}
		}

		/// <summary>Gets the size, in pixels, of the working area of the screen.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size, in pixels, of the working area of the screen.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00044703 File Offset: 0x00042903
		public static Rectangle WorkingArea
		{
			get
			{
				return Screen.PrimaryScreen.WorkingArea;
			}
		}
	}
}
