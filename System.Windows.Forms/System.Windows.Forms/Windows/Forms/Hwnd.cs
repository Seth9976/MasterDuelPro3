using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020000C5 RID: 197
	internal class Hwnd : IDisposable
	{
		// Token: 0x06000779 RID: 1913 RVA: 0x00020E08 File Offset: 0x0001F008
		public Hwnd()
		{
			this.x = 0;
			this.y = 0;
			this.width = 0;
			this.height = 0;
			this.visible = false;
			this.menu = null;
			this.border_style = FormBorderStyle.None;
			this.client_window = IntPtr.Zero;
			this.whole_window = IntPtr.Zero;
			this.cursor = IntPtr.Zero;
			this.handle = IntPtr.Zero;
			this.parent = null;
			this.invalid_list = new ArrayList();
			this.expose_pending = false;
			this.nc_expose_pending = false;
			this.enabled = true;
			this.reparented = false;
			this.client_rectangle = Rectangle.Empty;
			this.marshal_free_list = new ArrayList(2);
			this.opacity = uint.MaxValue;
			this.fixed_size = false;
			this.drawing_stack = new Stack();
			this.children = new ArrayList();
			this.resizing_or_moving = false;
			this.whacky_wm = false;
			this.topmost = false;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00020F28 File Offset: 0x0001F128
		public void Dispose()
		{
			this.expose_pending = false;
			this.nc_expose_pending = false;
			this.Parent = null;
			Hashtable hashtable = Hwnd.windows;
			lock (hashtable)
			{
				Hwnd.windows.Remove(this.client_window);
				Hwnd.windows.Remove(this.whole_window);
			}
			this.client_window = IntPtr.Zero;
			this.whole_window = IntPtr.Zero;
			this.zombie = false;
			for (int i = 0; i < this.marshal_free_list.Count; i++)
			{
				Marshal.FreeHGlobal((IntPtr)this.marshal_free_list[i]);
			}
			this.marshal_free_list.Clear();
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00020FF4 File Offset: 0x0001F1F4
		public static Hwnd ObjectFromWindow(IntPtr window)
		{
			Hashtable hashtable = Hwnd.windows;
			Hwnd hwnd;
			lock (hashtable)
			{
				hwnd = (Hwnd)Hwnd.windows[window];
			}
			return hwnd;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00021044 File Offset: 0x0001F244
		public static Hwnd ObjectFromHandle(IntPtr handle)
		{
			Hashtable hashtable = Hwnd.windows;
			Hwnd hwnd;
			lock (hashtable)
			{
				hwnd = (Hwnd)Hwnd.windows[handle];
			}
			return hwnd;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00021094 File Offset: 0x0001F294
		public static IntPtr HandleFromObject(Hwnd obj)
		{
			return obj.handle;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0002109C File Offset: 0x0001F29C
		public static Hwnd GetObjectFromWindow(IntPtr window)
		{
			Hashtable hashtable = Hwnd.windows;
			Hwnd hwnd;
			lock (hashtable)
			{
				hwnd = (Hwnd)Hwnd.windows[window];
			}
			return hwnd;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x000210EC File Offset: 0x0001F2EC
		public static IntPtr GetHandleFromWindow(IntPtr window)
		{
			Hashtable hashtable = Hwnd.windows;
			Hwnd hwnd;
			lock (hashtable)
			{
				hwnd = (Hwnd)Hwnd.windows[window];
			}
			if (hwnd != null)
			{
				return hwnd.handle;
			}
			return IntPtr.Zero;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0002114C File Offset: 0x0001F34C
		public static Hwnd.Borders GetBorderWidth(CreateParams cp)
		{
			Hwnd.Borders borders = default(Hwnd.Borders);
			Size borderSize = ThemeEngine.Current.BorderSize;
			Size borderStaticSize = ThemeEngine.Current.BorderStaticSize;
			Size border3DSize = ThemeEngine.Current.Border3DSize;
			Size size = new Size(2 + borderSize.Width, 2 + borderSize.Height);
			Size borderSizableSize = ThemeEngine.Current.BorderSizableSize;
			if (cp.IsSet(WindowStyles.WS_CAPTION))
			{
				borders.Inflate(borderSizableSize);
			}
			else if (cp.IsSet(WindowStyles.WS_BORDER))
			{
				if (cp.IsSet(WindowExStyles.WS_EX_DLGMODALFRAME))
				{
					if (cp.IsSet(WindowStyles.WS_THICKFRAME) && (cp.IsSet(WindowExStyles.WS_EX_STATICEDGE) || cp.IsSet(WindowExStyles.WS_EX_CLIENTEDGE)))
					{
						borders.Inflate(borderStaticSize);
					}
				}
				else
				{
					borders.Inflate(borderStaticSize);
				}
			}
			else if (cp.IsSet(WindowStyles.WS_DLGFRAME))
			{
				borders.Inflate(borderSizableSize);
			}
			if (cp.IsSet(WindowStyles.WS_THICKFRAME))
			{
				if (cp.IsSet(WindowStyles.WS_DLGFRAME))
				{
					borders.Inflate(borderStaticSize);
				}
				else
				{
					borders.Inflate(size);
				}
			}
			Size size2 = Size.Empty;
			bool flag = cp.IsSet(WindowStyles.WS_THICKFRAME) || cp.IsSet(WindowStyles.WS_DLGFRAME);
			if (flag && cp.IsSet(WindowStyles.WS_THICKFRAME) && !cp.IsSet(WindowStyles.WS_BORDER) && !cp.IsSet(WindowStyles.WS_DLGFRAME))
			{
				size2 = borderStaticSize;
			}
			if (cp.IsSet(WindowExStyles.WS_EX_DLGMODALFRAME | WindowExStyles.WS_EX_CLIENTEDGE))
			{
				borders.Inflate(border3DSize + (flag ? size2 : borderSizableSize));
			}
			else if (cp.IsSet(WindowExStyles.WS_EX_DLGMODALFRAME | WindowExStyles.WS_EX_STATICEDGE))
			{
				borders.Inflate(flag ? size2 : borderSizableSize);
			}
			else if (cp.IsSet(WindowExStyles.WS_EX_CLIENTEDGE | WindowExStyles.WS_EX_STATICEDGE))
			{
				borders.Inflate(borderStaticSize + (flag ? Size.Empty : border3DSize));
			}
			else
			{
				if (cp.IsSet(WindowExStyles.WS_EX_CLIENTEDGE))
				{
					borders.Inflate(border3DSize);
				}
				if (cp.IsSet(WindowExStyles.WS_EX_DLGMODALFRAME) && !cp.IsSet(WindowStyles.WS_DLGFRAME))
				{
					borders.Inflate(cp.IsSet(WindowStyles.WS_THICKFRAME) ? borderStaticSize : borderSizableSize);
				}
				if (cp.IsSet(WindowExStyles.WS_EX_STATICEDGE))
				{
					if (cp.IsSet(WindowStyles.WS_THICKFRAME) || cp.IsSet(WindowStyles.WS_DLGFRAME))
					{
						borders.Inflate(new Size(-borderStaticSize.Width, -borderStaticSize.Height));
					}
					else
					{
						borders.Inflate(borderStaticSize);
					}
				}
			}
			return borders;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000213B0 File Offset: 0x0001F5B0
		public static Rectangle GetWindowRectangle(CreateParams cp, Menu menu, Rectangle client_rect)
		{
			Hwnd.Borders borders = Hwnd.GetBorders(cp, menu);
			Rectangle rectangle = new Rectangle(Point.Empty, client_rect.Size);
			rectangle.Y -= borders.top;
			rectangle.Height += borders.top + borders.bottom;
			rectangle.X -= borders.left;
			rectangle.Width += borders.left + borders.right;
			return rectangle;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00021438 File Offset: 0x0001F638
		public ArrayList GetClippingRectangles()
		{
			ArrayList arrayList = new ArrayList();
			if (this.x < 0)
			{
				arrayList.Add(new Rectangle(0, 0, this.x * -1, this.Height));
				if (this.y < 0)
				{
					arrayList.Add(new Rectangle(this.x * -1, 0, this.Width, this.y * -1));
				}
			}
			else if (this.y < 0)
			{
				arrayList.Add(new Rectangle(0, 0, this.Width, this.y * -1));
			}
			foreach (object obj in this.children)
			{
				Hwnd hwnd = (Hwnd)obj;
				if (hwnd.visible)
				{
					arrayList.Add(new Rectangle(hwnd.X, hwnd.Y, hwnd.Width, hwnd.Height));
				}
			}
			if (this.parent == null)
			{
				return arrayList;
			}
			foreach (object obj2 in this.parent.children)
			{
				Hwnd hwnd2 = (Hwnd)obj2;
				IntPtr previousWindow = this.whole_window;
				if (hwnd2 != this)
				{
					do
					{
						previousWindow = XplatUI.GetPreviousWindow(previousWindow);
						if (previousWindow == hwnd2.WholeWindow && hwnd2.visible)
						{
							Rectangle rectangle = Rectangle.Intersect(new Rectangle(this.X, this.Y, this.Width, this.Height), new Rectangle(hwnd2.X, hwnd2.Y, hwnd2.Width, hwnd2.Height));
							if (!(rectangle == Rectangle.Empty))
							{
								rectangle.X -= this.X;
								rectangle.Y -= this.Y;
								arrayList.Add(rectangle);
							}
						}
					}
					while (previousWindow != IntPtr.Zero);
				}
			}
			return arrayList;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00021674 File Offset: 0x0001F874
		public static Hwnd.Borders GetBorders(CreateParams cp, Menu menu)
		{
			Hwnd.Borders borders = default(Hwnd.Borders);
			if (menu != null)
			{
				int num = menu.Rect.Height;
				if (num == 0)
				{
					num = ThemeEngine.Current.CalcMenuBarSize(Hwnd.GraphicsContext, menu, cp.Width);
				}
				borders.top += num;
			}
			if (cp.IsSet(WindowStyles.WS_CAPTION))
			{
				int num2;
				if (cp.IsSet(WindowExStyles.WS_EX_TOOLWINDOW))
				{
					num2 = ThemeEngine.Current.ToolWindowCaptionHeight;
				}
				else
				{
					num2 = ThemeEngine.Current.CaptionHeight;
				}
				borders.top += num2;
			}
			Hwnd.Borders borderWidth = Hwnd.GetBorderWidth(cp);
			borders.left += borderWidth.left;
			borders.right += borderWidth.right;
			borders.top += borderWidth.top;
			borders.bottom += borderWidth.bottom;
			return borders;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00021750 File Offset: 0x0001F950
		public static Rectangle GetClientRectangle(CreateParams cp, Menu menu, int width, int height)
		{
			Hwnd.Borders borders = Hwnd.GetBorders(cp, menu);
			Rectangle rectangle = new Rectangle(0, 0, width, height);
			rectangle.Y += borders.top;
			rectangle.Height -= borders.top + borders.bottom;
			rectangle.X += borders.left;
			rectangle.Width -= borders.left + borders.right;
			return rectangle;
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x000217CF File Offset: 0x0001F9CF
		public static Graphics GraphicsContext
		{
			get
			{
				if (Hwnd.bmp_g == null)
				{
					Hwnd.bmp = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
					Hwnd.bmp_g = Graphics.FromImage(Hwnd.bmp);
				}
				return Hwnd.bmp_g;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x000217FD File Offset: 0x0001F9FD
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x0002181E File Offset: 0x0001FA1E
		public Rectangle ClientRect
		{
			get
			{
				if (this.client_rectangle == Rectangle.Empty)
				{
					return this.DefaultClientRect;
				}
				return this.client_rectangle;
			}
			set
			{
				this.client_rectangle = value;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x00021827 File Offset: 0x0001FA27
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x0002182F File Offset: 0x0001FA2F
		public IntPtr Cursor
		{
			get
			{
				return this.cursor;
			}
			set
			{
				this.cursor = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00021838 File Offset: 0x0001FA38
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x00021840 File Offset: 0x0001FA40
		public IntPtr ClientWindow
		{
			get
			{
				return this.client_window;
			}
			set
			{
				this.client_window = value;
				this.handle = value;
				this.zombie = false;
				if (this.client_window != IntPtr.Zero)
				{
					Hashtable hashtable = Hwnd.windows;
					lock (hashtable)
					{
						if (Hwnd.windows[this.client_window] == null)
						{
							Hwnd.windows[this.client_window] = this;
						}
					}
				}
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x000218D0 File Offset: 0x0001FAD0
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x000218D8 File Offset: 0x0001FAD8
		public Region UserClip
		{
			get
			{
				return this.user_clip;
			}
			set
			{
				this.user_clip = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x000218E1 File Offset: 0x0001FAE1
		public Rectangle DefaultClientRect
		{
			get
			{
				return Hwnd.GetClientRectangle(new CreateParams
				{
					WindowStyle = this.initial_style,
					WindowExStyle = this.initial_ex_style
				}, null, this.width, this.height);
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x00021912 File Offset: 0x0001FB12
		public IntPtr Handle
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					throw new ArgumentNullException("Handle", "Handle is not yet assigned, need a ClientWindow");
				}
				return this.handle;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0002193C File Offset: 0x0001FB3C
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x00021944 File Offset: 0x0001FB44
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		// Token: 0x170001DB RID: 475
		// (set) Token: 0x06000792 RID: 1938 RVA: 0x0002194D File Offset: 0x0001FB4D
		public bool Reparented
		{
			set
			{
				this.reparented = value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x00021956 File Offset: 0x0001FB56
		// (set) Token: 0x06000794 RID: 1940 RVA: 0x0002195E File Offset: 0x0001FB5E
		public XEventQueue Queue
		{
			get
			{
				return this.queue;
			}
			set
			{
				this.queue = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00021967 File Offset: 0x0001FB67
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x00021988 File Offset: 0x0001FB88
		public bool Enabled
		{
			get
			{
				return this.enabled && (this.parent == null || this.parent.Enabled);
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x00021991 File Offset: 0x0001FB91
		public IntPtr EnabledHwnd
		{
			get
			{
				if (this.Enabled || this.parent == null)
				{
					return this.Handle;
				}
				return this.parent.EnabledHwnd;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x000219B8 File Offset: 0x0001FBB8
		public Point MenuOrigin
		{
			get
			{
				Form form = Control.FromHandle(this.handle) as Form;
				if (form != null && form.window_manager != null)
				{
					return form.window_manager.GetMenuOrigin();
				}
				Size border3DSize = ThemeEngine.Current.Border3DSize;
				Point point = new Point(0, 0);
				if (this.border_style == FormBorderStyle.Fixed3D)
				{
					point.X += border3DSize.Width;
					point.Y += border3DSize.Height;
				}
				else if (this.border_style == FormBorderStyle.FixedSingle)
				{
					point.X++;
					point.Y++;
				}
				if (this.title_style == TitleStyle.Normal)
				{
					point.Y += this.caption_height;
				}
				else if (this.title_style == TitleStyle.Tool)
				{
					point.Y += this.tool_caption_height;
				}
				return point;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x00021A9C File Offset: 0x0001FC9C
		public Rectangle Invalid
		{
			get
			{
				if (this.invalid_list.Count == 0)
				{
					return Rectangle.Empty;
				}
				Rectangle rectangle = (Rectangle)this.invalid_list[0];
				for (int i = 1; i < this.invalid_list.Count; i++)
				{
					rectangle = Rectangle.Union(rectangle, (Rectangle)this.invalid_list[i]);
				}
				return rectangle;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00021AFD File Offset: 0x0001FCFD
		public Rectangle[] ClipRectangles
		{
			get
			{
				return (Rectangle[])this.invalid_list.ToArray(typeof(Rectangle));
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00021B19 File Offset: 0x0001FD19
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x00021B21 File Offset: 0x0001FD21
		public Hwnd Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				if (this.parent != null)
				{
					this.parent.children.Remove(this);
				}
				this.parent = value;
				if (this.parent != null)
				{
					this.parent.children.Add(this);
				}
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x00021B5D File Offset: 0x0001FD5D
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x00021B7E File Offset: 0x0001FD7E
		public bool Mapped
		{
			get
			{
				return this.mapped && (this.parent == null || this.parent.Mapped);
			}
			set
			{
				this.mapped = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00021B87 File Offset: 0x0001FD87
		// (set) Token: 0x060007A0 RID: 1952 RVA: 0x00021B90 File Offset: 0x0001FD90
		public IntPtr WholeWindow
		{
			get
			{
				return this.whole_window;
			}
			set
			{
				this.whole_window = value;
				this.zombie = false;
				if (this.whole_window != IntPtr.Zero)
				{
					Hashtable hashtable = Hwnd.windows;
					lock (hashtable)
					{
						if (Hwnd.windows[this.whole_window] == null)
						{
							Hwnd.windows[this.whole_window] = this;
						}
					}
				}
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00021C18 File Offset: 0x0001FE18
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x00021C20 File Offset: 0x0001FE20
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00021C29 File Offset: 0x0001FE29
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x00021C31 File Offset: 0x0001FE31
		public int X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00021C3A File Offset: 0x0001FE3A
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x00021C42 File Offset: 0x0001FE42
		public int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00021C4B File Offset: 0x0001FE4B
		public void AddInvalidArea(int x, int y, int width, int height)
		{
			this.AddInvalidArea(new Rectangle(x, y, width, height));
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00021C60 File Offset: 0x0001FE60
		public void AddInvalidArea(Rectangle rect)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.invalid_list)
			{
				Rectangle rectangle = (Rectangle)obj;
				if (!rect.Contains(rectangle))
				{
					arrayList.Add(rectangle);
				}
			}
			arrayList.Add(rect);
			this.invalid_list = arrayList;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00021CE4 File Offset: 0x0001FEE4
		public void ClearInvalidArea()
		{
			this.invalid_list.Clear();
			this.expose_pending = false;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00021CF8 File Offset: 0x0001FEF8
		public void AddNcInvalidArea(int x, int y, int width, int height)
		{
			if (this.nc_invalid == Rectangle.Empty)
			{
				this.nc_invalid = new Rectangle(x, y, width, height);
				return;
			}
			int num = Math.Max(this.nc_invalid.Right, x + width);
			int num2 = Math.Max(this.nc_invalid.Bottom, y + height);
			this.nc_invalid.X = Math.Min(this.nc_invalid.X, x);
			this.nc_invalid.Y = Math.Min(this.nc_invalid.Y, y);
			this.nc_invalid.Width = num - this.nc_invalid.X;
			this.nc_invalid.Height = num2 - this.nc_invalid.Y;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00021DB9 File Offset: 0x0001FFB9
		public void ClearNcInvalidArea()
		{
			this.nc_invalid = Rectangle.Empty;
			this.nc_expose_pending = false;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00021DD0 File Offset: 0x0001FFD0
		public override string ToString()
		{
			return string.Format("Hwnd, Mapped:{3} ClientWindow:0x{0:X}, WholeWindow:0x{1:X}, Zombie={4}, Parent:[{2:X}]", new object[]
			{
				this.client_window.ToInt32(),
				this.whole_window.ToInt32(),
				(this.parent != null) ? this.parent.ToString() : "<null>",
				this.Mapped,
				this.zombie
			});
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00021E4C File Offset: 0x0002004C
		public static Point GetNextStackedFormLocation(CreateParams cp, Hwnd parent_hwnd)
		{
			if (cp.control == null)
			{
				return Point.Empty;
			}
			int num = cp.X;
			int num2 = cp.Y;
			Point point;
			Rectangle rectangle;
			if (parent_hwnd != null)
			{
				Control control = cp.control.Parent;
				point = parent_hwnd.previous_child_startup_location;
				if (parent_hwnd.client_rectangle == Rectangle.Empty && control != null)
				{
					rectangle = control.ClientRectangle;
				}
				else
				{
					rectangle = parent_hwnd.client_rectangle;
				}
			}
			else
			{
				point = Hwnd.previous_main_startup_location;
				rectangle = Screen.PrimaryScreen.WorkingArea;
			}
			Point point2;
			if (point.X == -2147483648 || point.Y == -2147483648)
			{
				point2 = Point.Empty;
			}
			else
			{
				point2 = new Point(point.X + 22, point.Y + 22);
			}
			if (!rectangle.Contains(point2.X * 3, point2.Y * 3))
			{
				point2 = Point.Empty;
			}
			if (point2 == Point.Empty && cp.Parent == IntPtr.Zero)
			{
				point2 = new Point(22, 22);
			}
			if (parent_hwnd != null)
			{
				parent_hwnd.previous_child_startup_location = point2;
			}
			else
			{
				Hwnd.previous_main_startup_location = point2;
			}
			if (num == -2147483648 && num2 == -2147483648)
			{
				num = point2.X;
				num2 = point2.Y;
			}
			return new Point(num, num2);
		}

		// Token: 0x040004C9 RID: 1225
		private static Hashtable windows = new Hashtable(100, 0.5f);

		// Token: 0x040004CA RID: 1226
		private IntPtr handle;

		// Token: 0x040004CB RID: 1227
		internal IntPtr client_window;

		// Token: 0x040004CC RID: 1228
		internal IntPtr whole_window;

		// Token: 0x040004CD RID: 1229
		internal IntPtr cursor;

		// Token: 0x040004CE RID: 1230
		internal Menu menu;

		// Token: 0x040004CF RID: 1231
		internal TitleStyle title_style;

		// Token: 0x040004D0 RID: 1232
		internal FormBorderStyle border_style;

		// Token: 0x040004D1 RID: 1233
		internal bool border_static;

		// Token: 0x040004D2 RID: 1234
		internal int x;

		// Token: 0x040004D3 RID: 1235
		internal int y;

		// Token: 0x040004D4 RID: 1236
		internal int width;

		// Token: 0x040004D5 RID: 1237
		internal int height;

		// Token: 0x040004D6 RID: 1238
		internal bool allow_drop;

		// Token: 0x040004D7 RID: 1239
		internal Hwnd parent;

		// Token: 0x040004D8 RID: 1240
		internal bool visible;

		// Token: 0x040004D9 RID: 1241
		internal bool mapped;

		// Token: 0x040004DA RID: 1242
		internal uint opacity;

		// Token: 0x040004DB RID: 1243
		internal bool enabled;

		// Token: 0x040004DC RID: 1244
		internal bool zero_sized;

		// Token: 0x040004DD RID: 1245
		internal ArrayList invalid_list;

		// Token: 0x040004DE RID: 1246
		internal Rectangle nc_invalid;

		// Token: 0x040004DF RID: 1247
		internal bool expose_pending;

		// Token: 0x040004E0 RID: 1248
		internal bool nc_expose_pending;

		// Token: 0x040004E1 RID: 1249
		internal bool configure_pending;

		// Token: 0x040004E2 RID: 1250
		internal bool resizing_or_moving;

		// Token: 0x040004E3 RID: 1251
		internal bool reparented;

		// Token: 0x040004E4 RID: 1252
		internal Stack drawing_stack;

		// Token: 0x040004E5 RID: 1253
		internal Rectangle client_rectangle;

		// Token: 0x040004E6 RID: 1254
		internal ArrayList marshal_free_list;

		// Token: 0x040004E7 RID: 1255
		internal int caption_height;

		// Token: 0x040004E8 RID: 1256
		internal int tool_caption_height;

		// Token: 0x040004E9 RID: 1257
		internal bool whacky_wm;

		// Token: 0x040004EA RID: 1258
		internal bool fixed_size;

		// Token: 0x040004EB RID: 1259
		internal bool zombie;

		// Token: 0x040004EC RID: 1260
		internal bool topmost;

		// Token: 0x040004ED RID: 1261
		internal Region user_clip;

		// Token: 0x040004EE RID: 1262
		internal XEventQueue queue;

		// Token: 0x040004EF RID: 1263
		internal WindowExStyles initial_ex_style;

		// Token: 0x040004F0 RID: 1264
		internal WindowStyles initial_style;

		// Token: 0x040004F1 RID: 1265
		internal FormWindowState cached_window_state = (FormWindowState)(-1);

		// Token: 0x040004F2 RID: 1266
		internal Point previous_child_startup_location = new Point(int.MinValue, int.MinValue);

		// Token: 0x040004F3 RID: 1267
		internal static Point previous_main_startup_location = new Point(int.MinValue, int.MinValue);

		// Token: 0x040004F4 RID: 1268
		internal ArrayList children;

		// Token: 0x040004F5 RID: 1269
		[ThreadStatic]
		private static Bitmap bmp;

		// Token: 0x040004F6 RID: 1270
		[ThreadStatic]
		private static Graphics bmp_g;

		// Token: 0x040004F7 RID: 1271
		internal object configure_lock = new object();

		// Token: 0x040004F8 RID: 1272
		internal object expose_lock = new object();

		// Token: 0x020000C6 RID: 198
		internal struct Borders
		{
			// Token: 0x060007AF RID: 1967 RVA: 0x00021FB4 File Offset: 0x000201B4
			public void Inflate(Size size)
			{
				this.left += size.Width;
				this.right += size.Width;
				this.top += size.Height;
				this.bottom += size.Height;
			}

			// Token: 0x060007B0 RID: 1968 RVA: 0x00022014 File Offset: 0x00020214
			public override string ToString()
			{
				return string.Format("{{top={0}, bottom={1}, left={2}, right={3}}}", new object[] { this.top, this.bottom, this.left, this.right });
			}

			// Token: 0x060007B1 RID: 1969 RVA: 0x00022069 File Offset: 0x00020269
			public override bool Equals(object obj)
			{
				return base.Equals(obj);
			}

			// Token: 0x060007B2 RID: 1970 RVA: 0x0002207C File Offset: 0x0002027C
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x040004F9 RID: 1273
			public int top;

			// Token: 0x040004FA RID: 1274
			public int bottom;

			// Token: 0x040004FB RID: 1275
			public int left;

			// Token: 0x040004FC RID: 1276
			public int right;
		}
	}
}
