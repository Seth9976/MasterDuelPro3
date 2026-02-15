using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms.CarbonInternal;

namespace System.Windows.Forms
{
	// Token: 0x02000299 RID: 665
	internal class XplatUICarbon : XplatUIDriver
	{
		// Token: 0x060017D9 RID: 6105 RVA: 0x000757F8 File Offset: 0x000739F8
		private XplatUICarbon()
		{
			XplatUICarbon.RefCount = 0;
			this.TimerList = new ArrayList();
			XplatUICarbon.in_doevents = false;
			XplatUICarbon.MessageQueue = new Queue();
			this.Initialize();
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00075828 File Offset: 0x00073A28
		~XplatUICarbon()
		{
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00075850 File Offset: 0x00073A50
		public static XplatUICarbon GetInstance()
		{
			object obj = XplatUICarbon.instancelock;
			lock (obj)
			{
				if (XplatUICarbon.Instance == null)
				{
					XplatUICarbon.Instance = new XplatUICarbon();
				}
				XplatUICarbon.RefCount++;
			}
			return XplatUICarbon.Instance;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x000758AC File Offset: 0x00073AAC
		internal void AddExpose(Hwnd hwnd, bool client, HIRect rect)
		{
			this.AddExpose(hwnd, client, (int)rect.origin.x, (int)rect.origin.y, (int)rect.size.width, (int)rect.size.height);
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x000758E8 File Offset: 0x00073AE8
		internal void FlushQueue()
		{
			this.CheckTimers(DateTime.UtcNow);
			object obj = XplatUICarbon.queuelock;
			lock (obj)
			{
				while (XplatUICarbon.MessageQueue.Count > 0)
				{
					object obj2 = XplatUICarbon.MessageQueue.Dequeue();
					if (obj2 is GCHandle)
					{
						XplatUIDriverSupport.ExecuteClientMessage((GCHandle)obj2);
					}
					else
					{
						MSG msg = (MSG)obj2;
						NativeWindow.WndProc(msg.hwnd, msg.message, msg.wParam, msg.lParam);
					}
				}
			}
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00075980 File Offset: 0x00073B80
		internal static Rectangle[] GetClippingRectangles(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				return null;
			}
			if (hwnd.Handle != handle)
			{
				return new Rectangle[] { hwnd.ClientRect };
			}
			return (Rectangle[])hwnd.GetClippingRectangles().ToArray(typeof(Rectangle));
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x000759D8 File Offset: 0x00073BD8
		internal IntPtr GetMousewParam(int Delta)
		{
			int num = 0;
			if ((XplatUICarbon.MouseState & MouseButtons.Left) != MouseButtons.None)
			{
				num |= 1;
			}
			if ((XplatUICarbon.MouseState & MouseButtons.Middle) != MouseButtons.None)
			{
				num |= 16;
			}
			if ((XplatUICarbon.MouseState & MouseButtons.Right) != MouseButtons.None)
			{
				num |= 2;
			}
			Keys modifierKeys = this.ModifierKeys;
			if ((modifierKeys & Keys.Control) != Keys.None)
			{
				num |= 8;
			}
			if ((modifierKeys & Keys.Shift) != Keys.None)
			{
				num |= 4;
			}
			num |= Delta << 16;
			return (IntPtr)num;
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00075A47 File Offset: 0x00073C47
		internal IntPtr HandleToWindow(IntPtr handle)
		{
			if (XplatUICarbon.HandleMapping[handle] != null)
			{
				return (IntPtr)XplatUICarbon.HandleMapping[handle];
			}
			return IntPtr.Zero;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00075A78 File Offset: 0x00073C78
		internal void Initialize()
		{
			if (Marshal.SizeOf<IntPtr>() == 8)
			{
				Console.Error.WriteLine("WARNING: The Carbon driver has not been ported to 64bits, and very few parts of Windows.Forms will work properly, or at all");
			}
			global::System.Windows.Forms.CarbonInternal.EventHandler.Driver = this;
			this.ApplicationHandler = new ApplicationHandler(this);
			this.ControlHandler = new ControlHandler(this);
			this.HIObjectHandler = new HIObjectHandler(this);
			this.KeyboardHandler = new KeyboardHandler(this);
			this.MouseHandler = new MouseHandler(this);
			this.WindowHandler = new WindowHandler(this);
			XplatUICarbon.Hover.Interval = 500;
			XplatUICarbon.Hover.Timer = new Timer();
			XplatUICarbon.Hover.Timer.Enabled = false;
			XplatUICarbon.Hover.Timer.Interval = XplatUICarbon.Hover.Interval;
			XplatUICarbon.Hover.Timer.Tick += this.HoverCallback;
			XplatUICarbon.Hover.X = -1;
			XplatUICarbon.Hover.Y = -1;
			XplatUICarbon.MouseState = MouseButtons.None;
			this.mouse_position = Point.Empty;
			XplatUICarbon.Caret.Timer = new Timer();
			XplatUICarbon.Caret.Timer.Interval = 500;
			XplatUICarbon.Caret.Timer.Tick += this.CaretCallback;
			XplatUICarbon.Dnd = new Dnd();
			XplatUICarbon.WindowMapping = new Hashtable();
			XplatUICarbon.HandleMapping = new Hashtable();
			XplatUICarbon.UtilityWindows = new ArrayList();
			Rect rect = default(Rect);
			XplatUICarbon.SetRect(ref rect, 0, 0, 0, 0);
			ProcessSerialNumber processSerialNumber = default(ProcessSerialNumber);
			XplatUICarbon.GetCurrentProcess(ref processSerialNumber);
			XplatUICarbon.TransformProcessType(ref processSerialNumber, 1U);
			XplatUICarbon.SetFrontProcess(ref processSerialNumber);
			XplatUICarbon.HIObjectRegisterSubclass(XplatUICarbon.__CFStringMakeConstantString("com.novell.mwfview"), XplatUICarbon.__CFStringMakeConstantString("com.apple.hiview"), 0U, global::System.Windows.Forms.CarbonInternal.EventHandler.EventHandlerDelegate, (uint)global::System.Windows.Forms.CarbonInternal.EventHandler.HIObjectEvents.Length, global::System.Windows.Forms.CarbonInternal.EventHandler.HIObjectEvents, IntPtr.Zero, ref XplatUICarbon.Subclass);
			global::System.Windows.Forms.CarbonInternal.EventHandler.InstallApplicationHandler();
			XplatUICarbon.CreateNewWindow(WindowClass.kDocumentWindowClass, (WindowAttributes)34078751U, ref rect, ref XplatUICarbon.FosterParent);
			XplatUICarbon.CreateNewWindow(WindowClass.kOverlayWindowClass, (WindowAttributes)196608U, ref rect, ref XplatUICarbon.ReverseWindow);
			XplatUICarbon.CreateNewWindow(WindowClass.kOverlayWindowClass, (WindowAttributes)196608U, ref rect, ref XplatUICarbon.CaretWindow);
			Rect rect2 = default(Rect);
			Rect rect3 = default(Rect);
			XplatUICarbon.GetWindowBounds(XplatUICarbon.FosterParent, 32U, ref rect2);
			XplatUICarbon.GetWindowBounds(XplatUICarbon.FosterParent, 33U, ref rect3);
			XplatUICarbon.MenuBarHeight = (int)XplatUICarbon.GetMBarHeight();
			XplatUICarbon.FocusWindow = IntPtr.Zero;
			XplatUICarbon.GetMessageResult = true;
			XplatUICarbon.ReverseWindowMapped = false;
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00075CD4 File Offset: 0x00073ED4
		internal void PerformNCCalc(Hwnd hwnd)
		{
			Rectangle rectangle = new Rectangle(0, 0, hwnd.Width, hwnd.Height);
			XplatUIWin32.NCCALCSIZE_PARAMS nccalcsize_PARAMS = default(XplatUIWin32.NCCALCSIZE_PARAMS);
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf<XplatUIWin32.NCCALCSIZE_PARAMS>(nccalcsize_PARAMS));
			nccalcsize_PARAMS.rgrc1.left = rectangle.Left;
			nccalcsize_PARAMS.rgrc1.top = rectangle.Top;
			nccalcsize_PARAMS.rgrc1.right = rectangle.Right;
			nccalcsize_PARAMS.rgrc1.bottom = rectangle.Bottom;
			Marshal.StructureToPtr<XplatUIWin32.NCCALCSIZE_PARAMS>(nccalcsize_PARAMS, intPtr, true);
			NativeWindow.WndProc(hwnd.client_window, Msg.WM_NCCALCSIZE, (IntPtr)1, intPtr);
			nccalcsize_PARAMS = (XplatUIWin32.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(intPtr, typeof(XplatUIWin32.NCCALCSIZE_PARAMS));
			Marshal.FreeHGlobal(intPtr);
			rectangle = new Rectangle(nccalcsize_PARAMS.rgrc1.left, nccalcsize_PARAMS.rgrc1.top, nccalcsize_PARAMS.rgrc1.right - nccalcsize_PARAMS.rgrc1.left, nccalcsize_PARAMS.rgrc1.bottom - nccalcsize_PARAMS.rgrc1.top);
			hwnd.ClientRect = rectangle;
			rectangle = XplatUICarbon.TranslateClientRectangleToQuartzClientRectangle(hwnd);
			if (hwnd.visible)
			{
				HIRect hirect = new HIRect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
				XplatUICarbon.HIViewSetFrame(hwnd.client_window, ref hirect);
			}
			this.AddExpose(hwnd, false, 0, 0, hwnd.Width, hwnd.Height);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00075E3C File Offset: 0x0007403C
		internal void ScreenToClient(IntPtr handle, ref QDPoint point)
		{
			int x = (int)point.x;
			int y = (int)point.y;
			this.ScreenToClient(handle, ref x, ref y);
			point.x = (short)x;
			point.y = (short)y;
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00075E72 File Offset: 0x00074072
		internal static Rectangle TranslateClientRectangleToQuartzClientRectangle(Hwnd hwnd)
		{
			return XplatUICarbon.TranslateClientRectangleToQuartzClientRectangle(hwnd, Control.FromHandle(hwnd.Handle));
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00075E88 File Offset: 0x00074088
		internal static Rectangle TranslateClientRectangleToQuartzClientRectangle(Hwnd hwnd, Control ctrl)
		{
			Rectangle rectangle = hwnd.ClientRect;
			Form form = ctrl as Form;
			CreateParams createParams = null;
			if (form != null)
			{
				createParams = form.GetCreateParams();
			}
			if (form != null && (form.window_manager == null || createParams.IsSet(WindowExStyles.WS_EX_TOOLWINDOW)))
			{
				Hwnd.Borders borders = Hwnd.GetBorders(createParams, null);
				Rectangle rectangle2 = rectangle;
				rectangle2.Y -= borders.top;
				rectangle2.X -= borders.left;
				rectangle2.Width += borders.left + borders.right;
				rectangle2.Height += borders.top + borders.bottom;
				rectangle = rectangle2;
			}
			if (rectangle.Width < 1 || rectangle.Height < 1)
			{
				rectangle.Width = 1;
				rectangle.Height = 1;
				rectangle.X = -5;
				rectangle.Y = -5;
			}
			return rectangle;
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00075F6D File Offset: 0x0007416D
		internal static Size TranslateWindowSizeToQuartzWindowSize(CreateParams cp)
		{
			return XplatUICarbon.TranslateWindowSizeToQuartzWindowSize(cp, new Size(cp.Width, cp.Height));
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00075F88 File Offset: 0x00074188
		internal static Size TranslateWindowSizeToQuartzWindowSize(CreateParams cp, Size size)
		{
			Form form = cp.control as Form;
			if (form != null && (form.window_manager == null || cp.IsSet(WindowExStyles.WS_EX_TOOLWINDOW)))
			{
				Hwnd.Borders borders = Hwnd.GetBorders(cp, null);
				Size size2 = size;
				size2.Width -= borders.left + borders.right;
				size2.Height -= borders.top + borders.bottom;
				size = size2;
			}
			if (size.Height == 0)
			{
				size.Height = 1;
			}
			if (size.Width == 0)
			{
				size.Width = 1;
			}
			return size;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00076020 File Offset: 0x00074220
		internal static Size TranslateQuartzWindowSizeToWindowSize(CreateParams cp, int width, int height)
		{
			Size size = new Size(width, height);
			Form form = cp.control as Form;
			if (form != null && (form.window_manager == null || cp.IsSet(WindowExStyles.WS_EX_TOOLWINDOW)))
			{
				Hwnd.Borders borders = Hwnd.GetBorders(cp, null);
				Size size2 = size;
				size2.Width += borders.left + borders.right;
				size2.Height += borders.top + borders.bottom;
				size = size2;
			}
			return size;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0007609D File Offset: 0x0007429D
		private void CaretCallback(object sender, EventArgs e)
		{
			if (XplatUICarbon.Caret.Paused)
			{
				return;
			}
			if (!XplatUICarbon.Caret.On)
			{
				this.ShowCaret();
				return;
			}
			this.HideCaret();
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x000760C8 File Offset: 0x000742C8
		private void HoverCallback(object sender, EventArgs e)
		{
			if (XplatUICarbon.Hover.X == this.mouse_position.X && XplatUICarbon.Hover.Y == this.mouse_position.Y)
			{
				this.EnqueueMessage(new MSG
				{
					hwnd = XplatUICarbon.Hover.Hwnd,
					message = Msg.WM_MOUSEHOVER,
					wParam = this.GetMousewParam(0),
					lParam = (IntPtr)(((int)((ushort)XplatUICarbon.Hover.X) << 16) | (int)((ushort)XplatUICarbon.Hover.X))
				});
			}
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00076164 File Offset: 0x00074364
		private Point ConvertScreenPointToClient(IntPtr handle, Point point)
		{
			Point point2 = default(Point);
			Rect rect = default(Rect);
			CGPoint cgpoint = default(CGPoint);
			XplatUICarbon.GetWindowBounds(XplatUICarbon.HIViewGetWindow(handle), 32U, ref rect);
			cgpoint.x = (float)(point.X - (int)rect.left);
			cgpoint.y = (float)(point.Y - (int)rect.top);
			XplatUICarbon.HIViewConvertPoint(ref cgpoint, IntPtr.Zero, handle);
			point2.X = (int)cgpoint.x;
			point2.Y = (int)cgpoint.y;
			return point2;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x000761F0 File Offset: 0x000743F0
		private Point ConvertClientPointToScreen(IntPtr handle, Point point)
		{
			Point point2 = default(Point);
			Rect rect = default(Rect);
			CGPoint cgpoint = default(CGPoint);
			XplatUICarbon.GetWindowBounds(XplatUICarbon.HIViewGetWindow(handle), 32U, ref rect);
			cgpoint.x = (float)point.X;
			cgpoint.y = (float)point.Y;
			XplatUICarbon.HIViewConvertPoint(ref cgpoint, handle, IntPtr.Zero);
			point2.X = (int)(cgpoint.x + (float)rect.left);
			point2.Y = (int)(cgpoint.y + (float)rect.top);
			return point2;
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00076280 File Offset: 0x00074480
		private double NextTimeout()
		{
			DateTime utcNow = DateTime.UtcNow;
			int num = 134217727;
			ArrayList timerList = this.TimerList;
			lock (timerList)
			{
				foreach (object obj in this.TimerList)
				{
					int num2 = (int)(((Timer)obj).Expires - utcNow).TotalMilliseconds;
					if (num2 < 0)
					{
						return 0.0;
					}
					if (num2 < num)
					{
						num = num2;
					}
				}
			}
			if (num < Timer.Minimum)
			{
				num = Timer.Minimum;
			}
			return (double)num / 1000.0;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0007635C File Offset: 0x0007455C
		private void CheckTimers(DateTime now)
		{
			ArrayList timerList = this.TimerList;
			lock (timerList)
			{
				if (this.TimerList.Count != 0)
				{
					for (int i = 0; i < this.TimerList.Count; i++)
					{
						Timer timer = (Timer)this.TimerList[i];
						if (timer.Enabled && timer.Expires <= now && (XplatUICarbon.in_doevents || (Application.MWFThread.Current.Context != null && Application.MWFThread.Current.Context.MainForm != null && Application.MWFThread.Current.Context.MainForm.IsLoaded)))
						{
							timer.FireTick();
							timer.Update(now);
						}
					}
				}
			}
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00076430 File Offset: 0x00074630
		private void WaitForHwndMessage(Hwnd hwnd, Msg message)
		{
			MSG msg = default(MSG);
			bool flag = false;
			do
			{
				if (this.GetMessage(null, ref msg, IntPtr.Zero, 0, 0))
				{
					if (msg.message == Msg.WM_QUIT)
					{
						this.PostQuitMessage(0);
						flag = true;
					}
					else
					{
						if (msg.hwnd == hwnd.Handle)
						{
							if (msg.message == message)
							{
								break;
							}
							if (msg.message == Msg.WM_DESTROY)
							{
								flag = true;
							}
						}
						this.TranslateMessage(ref msg);
						this.DispatchMessage(ref msg);
					}
				}
			}
			while (!flag);
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x000764AC File Offset: 0x000746AC
		private void SendParentNotify(IntPtr child, Msg cause, int x, int y)
		{
			if (child == IntPtr.Zero)
			{
				return;
			}
			Hwnd objectFromWindow = Hwnd.GetObjectFromWindow(child);
			if (objectFromWindow == null)
			{
				return;
			}
			if (objectFromWindow.Handle == IntPtr.Zero)
			{
				return;
			}
			if (this.ExStyleSet((int)objectFromWindow.initial_ex_style, WindowExStyles.WS_EX_NOPARENTNOTIFY))
			{
				return;
			}
			if (objectFromWindow.Parent == null)
			{
				return;
			}
			if (objectFromWindow.Parent.Handle == IntPtr.Zero)
			{
				return;
			}
			if (cause == Msg.WM_CREATE || cause == Msg.WM_DESTROY)
			{
				this.SendMessage(objectFromWindow.Parent.Handle, Msg.WM_PARENTNOTIFY, Control.MakeParam((int)cause, 0), child);
			}
			else
			{
				this.SendMessage(objectFromWindow.Parent.Handle, Msg.WM_PARENTNOTIFY, Control.MakeParam((int)cause, 0), Control.MakeParam(x, y));
			}
			this.SendParentNotify(objectFromWindow.Parent.Handle, cause, x, y);
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x0007657A File Offset: 0x0007477A
		private bool StyleSet(int s, WindowStyles ws)
		{
			return (s & (int)ws) == (int)ws;
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x0007657A File Offset: 0x0007477A
		private bool ExStyleSet(int ex, WindowExStyles exws)
		{
			return (ex & (int)exws) == (int)exws;
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00076584 File Offset: 0x00074784
		private void DeriveStyles(int Style, int ExStyle, out FormBorderStyle border_style, out bool border_static, out TitleStyle title_style, out int caption_height, out int tool_caption_height)
		{
			caption_height = 0;
			tool_caption_height = 0;
			border_static = false;
			if (this.StyleSet(Style, WindowStyles.WS_CHILD))
			{
				if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_CLIENTEDGE))
				{
					border_style = FormBorderStyle.Fixed3D;
				}
				else if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_STATICEDGE))
				{
					border_style = FormBorderStyle.Fixed3D;
					border_static = true;
				}
				else if (!this.StyleSet(Style, WindowStyles.WS_BORDER))
				{
					border_style = FormBorderStyle.None;
				}
				else
				{
					border_style = FormBorderStyle.FixedSingle;
				}
				title_style = TitleStyle.None;
				if (this.StyleSet(Style, WindowStyles.WS_CAPTION))
				{
					caption_height = 0;
					if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_TOOLWINDOW))
					{
						title_style = TitleStyle.Tool;
					}
					else
					{
						title_style = TitleStyle.Normal;
					}
				}
				if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_MDICHILD))
				{
					caption_height = 0;
					if (this.StyleSet(Style, WindowStyles.WS_OVERLAPPEDWINDOW) || this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_TOOLWINDOW))
					{
						border_style = (FormBorderStyle)65535;
						return;
					}
					border_style = FormBorderStyle.None;
					return;
				}
			}
			else
			{
				title_style = TitleStyle.None;
				if (this.StyleSet(Style, WindowStyles.WS_CAPTION))
				{
					if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_TOOLWINDOW))
					{
						title_style = TitleStyle.Tool;
					}
					else
					{
						title_style = TitleStyle.Normal;
					}
				}
				border_style = FormBorderStyle.None;
				if (this.StyleSet(Style, WindowStyles.WS_THICKFRAME))
				{
					if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_TOOLWINDOW))
					{
						border_style = FormBorderStyle.SizableToolWindow;
						return;
					}
					border_style = FormBorderStyle.Sizable;
					return;
				}
				else if (this.StyleSet(Style, WindowStyles.WS_CAPTION))
				{
					if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_CLIENTEDGE))
					{
						border_style = FormBorderStyle.Fixed3D;
						return;
					}
					if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_STATICEDGE))
					{
						border_style = FormBorderStyle.Fixed3D;
						border_static = true;
						return;
					}
					if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_DLGMODALFRAME))
					{
						border_style = FormBorderStyle.FixedDialog;
						return;
					}
					if (this.ExStyleSet(ExStyle, WindowExStyles.WS_EX_TOOLWINDOW))
					{
						border_style = FormBorderStyle.FixedToolWindow;
						return;
					}
					if (this.StyleSet(Style, WindowStyles.WS_BORDER))
					{
						border_style = FormBorderStyle.FixedSingle;
						return;
					}
				}
				else if (this.StyleSet(Style, WindowStyles.WS_BORDER))
				{
					border_style = FormBorderStyle.FixedSingle;
				}
			}
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00076720 File Offset: 0x00074920
		private void SetHwndStyles(Hwnd hwnd, CreateParams cp)
		{
			this.DeriveStyles(cp.Style, cp.ExStyle, out hwnd.border_style, out hwnd.border_static, out hwnd.title_style, out hwnd.caption_height, out hwnd.tool_caption_height);
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00076754 File Offset: 0x00074954
		private void ShowCaret()
		{
			if (XplatUICarbon.Caret.On)
			{
				return;
			}
			XplatUICarbon.Caret.On = true;
			XplatUICarbon.ShowWindow(XplatUICarbon.CaretWindow);
			Graphics graphics = Graphics.FromHwnd(XplatUICarbon.HIViewGetRoot(XplatUICarbon.CaretWindow));
			graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, XplatUICarbon.Caret.Width, XplatUICarbon.Caret.Height));
			graphics.Dispose();
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x000767C3 File Offset: 0x000749C3
		private void HideCaret()
		{
			if (!XplatUICarbon.Caret.On)
			{
				return;
			}
			XplatUICarbon.Caret.On = false;
			XplatUICarbon.HideWindow(XplatUICarbon.CaretWindow);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x000767E8 File Offset: 0x000749E8
		private void AccumulateDestroyedHandles(Control c, ArrayList list)
		{
			if (c != null)
			{
				Control[] allControls = c.Controls.GetAllControls();
				if (c.IsHandleCreated && !c.IsDisposed)
				{
					Hwnd hwnd = Hwnd.ObjectFromHandle(c.Handle);
					list.Add(hwnd);
					this.CleanupCachedWindows(hwnd);
				}
				for (int i = 0; i < allControls.Length; i++)
				{
					this.AccumulateDestroyedHandles(allControls[i], list);
				}
			}
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00076848 File Offset: 0x00074A48
		private void CleanupCachedWindows(Hwnd hwnd)
		{
			if (XplatUICarbon.ActiveWindow == hwnd.Handle)
			{
				this.SendMessage(hwnd.client_window, Msg.WM_ACTIVATE, (IntPtr)0, IntPtr.Zero);
				XplatUICarbon.ActiveWindow = IntPtr.Zero;
			}
			if (XplatUICarbon.FocusWindow == hwnd.Handle)
			{
				this.SendMessage(hwnd.client_window, Msg.WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
				XplatUICarbon.FocusWindow = IntPtr.Zero;
			}
			if (XplatUICarbon.Grab.Hwnd == hwnd.Handle)
			{
				XplatUICarbon.Grab.Hwnd = IntPtr.Zero;
				XplatUICarbon.Grab.Confined = false;
			}
			this.DestroyCaret(hwnd.Handle);
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x000768FC File Offset: 0x00074AFC
		private void AddExpose(Hwnd hwnd, bool client, int x, int y, int width, int height)
		{
			if (hwnd == null || x > hwnd.Width || y > hwnd.Height || x + width < 0 || y + height < 0)
			{
				return;
			}
			if (x + width > hwnd.width)
			{
				width = hwnd.width - x;
			}
			if (y + height > hwnd.height)
			{
				height = hwnd.height - y;
			}
			if (client)
			{
				hwnd.AddInvalidArea(x, y, width, height);
				if (!hwnd.expose_pending && hwnd.visible)
				{
					this.EnqueueMessage(new MSG
					{
						message = Msg.WM_PAINT,
						hwnd = hwnd.Handle
					});
					hwnd.expose_pending = true;
					return;
				}
			}
			else
			{
				hwnd.AddNcInvalidArea(x, y, width, height);
				if (!hwnd.nc_expose_pending && hwnd.visible)
				{
					MSG msg = default(MSG);
					Region region = new Region(hwnd.Invalid);
					IntPtr hrgn = region.GetHrgn(null);
					msg.message = Msg.WM_NCPAINT;
					msg.wParam = ((hrgn == IntPtr.Zero) ? ((IntPtr)1) : hrgn);
					msg.refobject = region;
					msg.hwnd = hwnd.Handle;
					this.EnqueueMessage(msg);
					hwnd.nc_expose_pending = true;
				}
			}
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00076A38 File Offset: 0x00074C38
		internal void EnqueueMessage(MSG msg)
		{
			object obj = XplatUICarbon.queuelock;
			lock (obj)
			{
				XplatUICarbon.MessageQueue.Enqueue(msg);
			}
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0000D682 File Offset: 0x0000B882
		internal override IntPtr InitializeDriver()
		{
			return IntPtr.Zero;
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x00076A84 File Offset: 0x00074C84
		internal override void Activate(IntPtr handle)
		{
			if (XplatUICarbon.ActiveWindow != IntPtr.Zero)
			{
				XplatUICarbon.UnactiveWindow = XplatUICarbon.ActiveWindow;
				XplatUICarbon.ActivateWindow(XplatUICarbon.HIViewGetWindow(XplatUICarbon.ActiveWindow), false);
			}
			XplatUICarbon.ActivateWindow(XplatUICarbon.HIViewGetWindow(handle), true);
			XplatUICarbon.ActiveWindow = handle;
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x00076AD0 File Offset: 0x00074CD0
		internal override void AudibleAlert(AlertType alert)
		{
			XplatUICarbon.AlertSoundPlay();
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x00076AD8 File Offset: 0x00074CD8
		internal override void CaretVisible(IntPtr hwnd, bool visible)
		{
			if (XplatUICarbon.Caret.Hwnd == hwnd)
			{
				if (visible)
				{
					if (XplatUICarbon.Caret.Visible < 1)
					{
						XplatUICarbon.Caret.Visible = XplatUICarbon.Caret.Visible + 1;
						XplatUICarbon.Caret.On = false;
						if (XplatUICarbon.Caret.Visible == 1)
						{
							this.ShowCaret();
							XplatUICarbon.Caret.Timer.Start();
							return;
						}
					}
				}
				else
				{
					XplatUICarbon.Caret.Visible = XplatUICarbon.Caret.Visible - 1;
					if (XplatUICarbon.Caret.Visible == 0)
					{
						XplatUICarbon.Caret.Timer.Stop();
						this.HideCaret();
					}
				}
			}
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x00076B74 File Offset: 0x00074D74
		internal override bool CalculateWindowRect(ref Rectangle ClientRect, CreateParams cp, Menu menu, out Rectangle WindowRect)
		{
			WindowRect = Hwnd.GetWindowRectangle(cp, menu, ClientRect);
			return true;
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00076B8C File Offset: 0x00074D8C
		internal override void ClientToScreen(IntPtr handle, ref int x, ref int y)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			Point point = this.ConvertClientPointToScreen(hwnd.ClientWindow, new Point(x, y));
			x = point.X;
			y = point.Y;
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x00076BC8 File Offset: 0x00074DC8
		internal override void MenuToScreen(IntPtr handle, ref int x, ref int y)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			Point point = this.ConvertClientPointToScreen(hwnd.ClientWindow, new Point(x, y));
			x = point.X;
			y = point.Y;
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00076C04 File Offset: 0x00074E04
		internal override int[] ClipboardAvailableFormats(IntPtr handle)
		{
			ArrayList arrayList = new ArrayList();
			for (DataFormats.Format format = DataFormats.Format.List; format != null; format = format.Next)
			{
				arrayList.Add(format.Id);
			}
			return (int[])arrayList.ToArray(typeof(int));
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override void ClipboardClose(IntPtr handle)
		{
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x00076C50 File Offset: 0x00074E50
		internal override int ClipboardGetID(IntPtr handle, string format)
		{
			return (int)XplatUICarbon.__CFStringMakeConstantString(format);
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x00076C5D File Offset: 0x00074E5D
		internal override IntPtr ClipboardOpen(bool primary_selection)
		{
			if (primary_selection)
			{
				return Pasteboard.Primary;
			}
			return Pasteboard.Application;
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x00076C6D File Offset: 0x00074E6D
		internal override object ClipboardRetrieve(IntPtr handle, int type, XplatUI.ClipboardToObject converter)
		{
			return Pasteboard.Retrieve(handle, type);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x00076C76 File Offset: 0x00074E76
		internal override void ClipboardStore(IntPtr handle, object obj, int type, XplatUI.ObjectToClipboard converter, bool copy)
		{
			Pasteboard.Store(handle, obj, type);
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x00076C80 File Offset: 0x00074E80
		internal override void CreateCaret(IntPtr hwnd, int width, int height)
		{
			if (XplatUICarbon.Caret.Hwnd != IntPtr.Zero)
			{
				this.DestroyCaret(XplatUICarbon.Caret.Hwnd);
			}
			XplatUICarbon.Caret.Hwnd = hwnd;
			XplatUICarbon.Caret.Width = width;
			XplatUICarbon.Caret.Height = height;
			XplatUICarbon.Caret.Visible = 0;
			XplatUICarbon.Caret.On = false;
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x00076CEC File Offset: 0x00074EEC
		internal override IntPtr CreateWindow(CreateParams cp)
		{
			Hwnd hwnd = null;
			Hwnd hwnd2 = new Hwnd();
			int num = cp.X;
			int num2 = cp.Y;
			int num3 = cp.Width;
			int num4 = cp.Height;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr zero3 = IntPtr.Zero;
			IntPtr zero4 = IntPtr.Zero;
			if (num3 < 1)
			{
				num3 = 1;
			}
			if (num4 < 1)
			{
				num4 = 1;
			}
			if (cp.Parent != IntPtr.Zero)
			{
				hwnd = Hwnd.ObjectFromHandle(cp.Parent);
				intPtr = hwnd.client_window;
			}
			else if (this.StyleSet(cp.Style, WindowStyles.WS_CHILD))
			{
				XplatUICarbon.HIViewFindByID(XplatUICarbon.HIViewGetRoot(XplatUICarbon.FosterParent), new HIViewID(2003398244U, 1U), ref intPtr);
			}
			if (cp.control is Form)
			{
				Point nextStackedFormLocation = Hwnd.GetNextStackedFormLocation(cp, hwnd);
				num = nextStackedFormLocation.X;
				num2 = nextStackedFormLocation.Y;
			}
			hwnd2.x = num;
			hwnd2.y = num2;
			hwnd2.width = num3;
			hwnd2.height = num4;
			hwnd2.Parent = Hwnd.ObjectFromHandle(cp.Parent);
			hwnd2.initial_style = cp.WindowStyle;
			hwnd2.initial_ex_style = cp.WindowExStyle;
			hwnd2.visible = false;
			if (this.StyleSet(cp.Style, WindowStyles.WS_DISABLED))
			{
				hwnd2.enabled = false;
			}
			intPtr2 = IntPtr.Zero;
			Size size = XplatUICarbon.TranslateWindowSizeToQuartzWindowSize(cp);
			Rectangle rectangle = XplatUICarbon.TranslateClientRectangleToQuartzClientRectangle(hwnd2, cp.control);
			this.SetHwndStyles(hwnd2, cp);
			if (intPtr == IntPtr.Zero)
			{
				IntPtr zero5 = IntPtr.Zero;
				IntPtr zero6 = IntPtr.Zero;
				WindowClass windowClass = WindowClass.kOverlayWindowClass;
				WindowAttributes windowAttributes = (WindowAttributes)34078720U;
				if (this.StyleSet(cp.Style, WindowStyles.WS_GROUP))
				{
					windowAttributes |= WindowAttributes.kWindowCollapseBoxAttribute;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_TABSTOP))
				{
					windowAttributes |= (WindowAttributes)22U;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_SYSMENU))
				{
					windowAttributes |= WindowAttributes.kWindowCloseBoxAttribute;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_CAPTION))
				{
					windowClass = WindowClass.kDocumentWindowClass;
				}
				if (hwnd2.border_style == FormBorderStyle.FixedToolWindow)
				{
					windowClass = WindowClass.kUtilityWindowClass;
				}
				else if (hwnd2.border_style == FormBorderStyle.SizableToolWindow)
				{
					windowAttributes |= WindowAttributes.kWindowResizableAttribute;
					windowClass = WindowClass.kUtilityWindowClass;
				}
				if (windowClass == WindowClass.kOverlayWindowClass)
				{
					windowAttributes = (WindowAttributes)34078720U;
				}
				windowAttributes |= WindowAttributes.kWindowLiveResizeAttribute;
				Rect rect = default(Rect);
				if (this.StyleSet(cp.Style, WindowStyles.WS_POPUP))
				{
					XplatUICarbon.SetRect(ref rect, (short)num, (short)num2, (short)(num + size.Width), (short)(num2 + size.Height));
				}
				else
				{
					XplatUICarbon.SetRect(ref rect, (short)num, (short)(num2 + XplatUICarbon.MenuBarHeight), (short)(num + size.Width), (short)(num2 + XplatUICarbon.MenuBarHeight + size.Height));
				}
				XplatUICarbon.CreateNewWindow(windowClass, windowAttributes, ref rect, ref zero);
				global::System.Windows.Forms.CarbonInternal.EventHandler.InstallWindowHandler(zero);
				XplatUICarbon.HIViewFindByID(XplatUICarbon.HIViewGetRoot(zero), new HIViewID(2003398244U, 1U), ref zero5);
				XplatUICarbon.HIViewFindByID(XplatUICarbon.HIViewGetRoot(zero), new HIViewID(2003398244U, 7U), ref zero6);
				XplatUICarbon.HIGrowBoxViewSetTransparent(zero6, true);
				XplatUICarbon.SetAutomaticControlDragTrackingEnabledForWindow(zero, true);
				intPtr = zero5;
			}
			XplatUICarbon.HIObjectCreate(XplatUICarbon.__CFStringMakeConstantString("com.novell.mwfview"), 0U, ref zero2);
			XplatUICarbon.HIObjectCreate(XplatUICarbon.__CFStringMakeConstantString("com.novell.mwfview"), 0U, ref intPtr2);
			global::System.Windows.Forms.CarbonInternal.EventHandler.InstallControlHandler(zero2);
			global::System.Windows.Forms.CarbonInternal.EventHandler.InstallControlHandler(intPtr2);
			XplatUICarbon.HIViewChangeFeatures(zero2, 2UL, 0UL);
			XplatUICarbon.HIViewChangeFeatures(intPtr2, 2UL, 0UL);
			XplatUICarbon.HIViewNewTrackingArea(zero2, IntPtr.Zero, (ulong)(long)zero2, ref zero3);
			XplatUICarbon.HIViewNewTrackingArea(intPtr2, IntPtr.Zero, (ulong)(long)intPtr2, ref zero4);
			HIRect hirect;
			if (zero != IntPtr.Zero)
			{
				hirect = new HIRect(0, 0, size.Width, size.Height);
			}
			else
			{
				hirect = new HIRect(num, num2, size.Width, size.Height);
			}
			HIRect hirect2 = new HIRect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
			XplatUICarbon.HIViewSetFrame(zero2, ref hirect);
			XplatUICarbon.HIViewSetFrame(intPtr2, ref hirect2);
			XplatUICarbon.HIViewAddSubview(intPtr, zero2);
			XplatUICarbon.HIViewAddSubview(zero2, intPtr2);
			hwnd2.WholeWindow = zero2;
			hwnd2.ClientWindow = intPtr2;
			if (zero != IntPtr.Zero)
			{
				XplatUICarbon.WindowMapping[hwnd2.Handle] = zero;
				XplatUICarbon.HandleMapping[zero] = hwnd2.Handle;
				if (hwnd2.border_style == FormBorderStyle.FixedToolWindow || hwnd2.border_style == FormBorderStyle.SizableToolWindow)
				{
					XplatUICarbon.UtilityWindows.Add(zero);
				}
			}
			XplatUICarbon.Dnd.SetAllowDrop(hwnd2, true);
			this.Text(hwnd2.Handle, cp.Caption);
			this.SendMessage(hwnd2.Handle, Msg.WM_CREATE, (IntPtr)1, IntPtr.Zero);
			this.SendParentNotify(hwnd2.Handle, Msg.WM_CREATE, int.MaxValue, int.MaxValue);
			if (this.StyleSet(cp.Style, WindowStyles.WS_VISIBLE))
			{
				if (zero != IntPtr.Zero)
				{
					if (Control.FromHandle(hwnd2.Handle) is Form && (Control.FromHandle(hwnd2.Handle) as Form).WindowState == FormWindowState.Normal)
					{
						this.SendMessage(hwnd2.Handle, Msg.WM_SHOWWINDOW, (IntPtr)1, IntPtr.Zero);
					}
					XplatUICarbon.ShowWindow(zero);
					this.WaitForHwndMessage(hwnd2, Msg.WM_SHOWWINDOW);
				}
				XplatUICarbon.HIViewSetVisible(zero2, true);
				XplatUICarbon.HIViewSetVisible(intPtr2, true);
				hwnd2.visible = true;
				if (!(Control.FromHandle(hwnd2.Handle) is Form))
				{
					this.SendMessage(hwnd2.Handle, Msg.WM_SHOWWINDOW, (IntPtr)1, IntPtr.Zero);
				}
			}
			if (this.StyleSet(cp.Style, WindowStyles.WS_MINIMIZE))
			{
				this.SetWindowState(hwnd2.Handle, FormWindowState.Minimized);
			}
			else if (this.StyleSet(cp.Style, WindowStyles.WS_MAXIMIZE))
			{
				this.SetWindowState(hwnd2.Handle, FormWindowState.Maximized);
			}
			return hwnd2.Handle;
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x000772BD File Offset: 0x000754BD
		internal override IntPtr DefineCursor(Bitmap bitmap, Bitmap mask, Color cursor_pixel, Color mask_pixel, int xHotSpot, int yHotSpot)
		{
			return Cursor.DefineCursor(bitmap, mask, cursor_pixel, mask_pixel, xHotSpot, yHotSpot);
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x000772CD File Offset: 0x000754CD
		internal override IntPtr DefineStdCursor(StdCursor id)
		{
			return Cursor.DefineStdCursor(id);
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x000772D8 File Offset: 0x000754D8
		internal override IntPtr DefWndProc(ref Message msg)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(msg.HWnd);
			Msg msg2 = (Msg)msg.Msg;
			if (msg2 <= Msg.WM_SETCURSOR)
			{
				if (msg2 != Msg.WM_PAINT)
				{
					if (msg2 != Msg.WM_QUIT)
					{
						if (msg2 == Msg.WM_SETCURSOR)
						{
							while (hwnd.parent != null && msg.Result == IntPtr.Zero)
							{
								hwnd = hwnd.parent;
								msg.Result = NativeWindow.WndProc(hwnd.Handle, Msg.WM_SETCURSOR, msg.HWnd, msg.LParam);
							}
							if (msg.Result == IntPtr.Zero)
							{
								HitTest hitTest = (HitTest)(msg.LParam.ToInt32() & 65535);
								IntPtr intPtr;
								if (hitTest != HitTest.HTERROR)
								{
									switch (hitTest)
									{
									case HitTest.HTLEFT:
										intPtr = Cursors.SizeWE.handle;
										goto IL_035B;
									case HitTest.HTRIGHT:
										intPtr = Cursors.SizeWE.handle;
										goto IL_035B;
									case HitTest.HTTOP:
										intPtr = Cursors.SizeNS.handle;
										goto IL_035B;
									case HitTest.HTTOPLEFT:
										intPtr = Cursors.SizeNWSE.handle;
										goto IL_035B;
									case HitTest.HTTOPRIGHT:
										intPtr = Cursors.SizeNESW.handle;
										goto IL_035B;
									case HitTest.HTBOTTOM:
										intPtr = Cursors.SizeNS.handle;
										goto IL_035B;
									case HitTest.HTBOTTOMLEFT:
										intPtr = Cursors.SizeNESW.handle;
										goto IL_035B;
									case HitTest.HTBOTTOMRIGHT:
										intPtr = Cursors.SizeNWSE.handle;
										goto IL_035B;
									case HitTest.HTBORDER:
										intPtr = Cursors.SizeNS.handle;
										goto IL_035B;
									case HitTest.HTHELP:
										intPtr = Cursors.Help.handle;
										goto IL_035B;
									}
									intPtr = Cursors.Default.handle;
								}
								else
								{
									int num = msg.LParam.ToInt32() >> 16;
									intPtr = Cursors.Default.handle;
								}
								IL_035B:
								this.SetCursor(msg.HWnd, intPtr);
							}
							return (IntPtr)1;
						}
					}
					else if (XplatUICarbon.WindowMapping[hwnd.Handle] != null)
					{
						this.Exit();
					}
				}
				else
				{
					hwnd.expose_pending = false;
				}
			}
			else if (msg2 <= Msg.WM_NCPAINT)
			{
				if (msg2 != Msg.WM_NCCALCSIZE)
				{
					if (msg2 == Msg.WM_NCPAINT)
					{
						hwnd.nc_expose_pending = false;
					}
				}
				else if (msg.WParam == (IntPtr)1)
				{
					XplatUIWin32.NCCALCSIZE_PARAMS nccalcsize_PARAMS = (XplatUIWin32.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(msg.LParam, typeof(XplatUIWin32.NCCALCSIZE_PARAMS));
					Control control = Control.FromHandle(hwnd.Handle);
					if (control != null)
					{
						Hwnd.Borders borders = Hwnd.GetBorders(control.GetCreateParams(), null);
						nccalcsize_PARAMS.rgrc1.top = nccalcsize_PARAMS.rgrc1.top + borders.top;
						nccalcsize_PARAMS.rgrc1.bottom = nccalcsize_PARAMS.rgrc1.bottom - borders.bottom;
						nccalcsize_PARAMS.rgrc1.left = nccalcsize_PARAMS.rgrc1.left + borders.left;
						nccalcsize_PARAMS.rgrc1.right = nccalcsize_PARAMS.rgrc1.right - borders.right;
						Marshal.StructureToPtr<XplatUIWin32.NCCALCSIZE_PARAMS>(nccalcsize_PARAMS, msg.LParam, true);
					}
				}
			}
			else if (msg2 != Msg.WM_IME_COMPOSITION)
			{
				if (msg2 == Msg.WM_IME_CHAR)
				{
					this.SendMessage(msg.HWnd, Msg.WM_CHAR, msg.WParam, msg.LParam);
					return IntPtr.Zero;
				}
			}
			else
			{
				foreach (char c in this.KeyboardHandler.ComposedString)
				{
					this.SendMessage(msg.HWnd, Msg.WM_IME_CHAR, (IntPtr)((int)c), msg.LParam);
				}
			}
			return IntPtr.Zero;
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0007765C File Offset: 0x0007585C
		internal override void DestroyCaret(IntPtr hwnd)
		{
			if (XplatUICarbon.Caret.Hwnd == hwnd)
			{
				if (XplatUICarbon.Caret.Visible == 1)
				{
					XplatUICarbon.Caret.Timer.Stop();
					this.HideCaret();
				}
				XplatUICarbon.Caret.Hwnd = IntPtr.Zero;
				XplatUICarbon.Caret.Visible = 0;
				XplatUICarbon.Caret.On = false;
			}
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x000776C4 File Offset: 0x000758C4
		internal override void DestroyWindow(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				return;
			}
			this.SendParentNotify(hwnd.Handle, Msg.WM_DESTROY, int.MaxValue, int.MaxValue);
			this.CleanupCachedWindows(hwnd);
			ArrayList arrayList = new ArrayList();
			this.AccumulateDestroyedHandles(Control.ControlNativeWindow.ControlFromHandle(hwnd.Handle), arrayList);
			foreach (object obj in arrayList)
			{
				Hwnd hwnd2 = (Hwnd)obj;
				this.SendMessage(hwnd2.Handle, Msg.WM_DESTROY, IntPtr.Zero, IntPtr.Zero);
				hwnd2.zombie = true;
			}
			if (XplatUICarbon.WindowMapping[hwnd.Handle] != null)
			{
				XplatUICarbon.DisposeWindow((IntPtr)XplatUICarbon.WindowMapping[hwnd.Handle]);
				XplatUICarbon.WindowMapping.Remove(hwnd.Handle);
			}
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x000777C0 File Offset: 0x000759C0
		internal override IntPtr DispatchMessage(ref MSG msg)
		{
			return NativeWindow.WndProc(msg.hwnd, msg.message, msg.wParam, msg.lParam);
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x000777E0 File Offset: 0x000759E0
		internal override void DoEvents()
		{
			MSG msg = default(MSG);
			XplatUICarbon.in_doevents = true;
			while (this.PeekMessage(null, ref msg, IntPtr.Zero, 0, 0, 1U))
			{
				this.TranslateMessage(ref msg);
				this.DispatchMessage(ref msg);
			}
			XplatUICarbon.in_doevents = false;
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override void EnableWindow(IntPtr handle, bool Enable)
		{
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override void EndLoop(Thread thread)
		{
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00077828 File Offset: 0x00075A28
		internal void Exit()
		{
			XplatUICarbon.GetMessageResult = false;
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00077830 File Offset: 0x00075A30
		internal override IntPtr GetActive()
		{
			return XplatUICarbon.ActiveWindow;
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x00077837 File Offset: 0x00075A37
		[MonoTODO]
		internal override void GetCursorInfo(IntPtr cursor, out int width, out int height, out int hotspot_x, out int hotspot_y)
		{
			width = 12;
			height = 12;
			hotspot_x = 0;
			hotspot_y = 0;
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0007784C File Offset: 0x00075A4C
		internal override void GetDisplaySize(out Size size)
		{
			HIRect hirect = XplatUICarbon.CGDisplayBounds(XplatUICarbon.CGMainDisplayID());
			size = new Size((int)hirect.size.width, (int)hirect.size.height);
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00077888 File Offset: 0x00075A88
		internal override IntPtr GetParent(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd != null && hwnd.Parent != null)
			{
				return hwnd.Parent.Handle;
			}
			return IntPtr.Zero;
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x000778B8 File Offset: 0x00075AB8
		internal override IntPtr GetPreviousWindow(IntPtr handle)
		{
			return XplatUICarbon.HIViewGetPreviousView(handle);
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x000778C0 File Offset: 0x00075AC0
		internal override void GetCursorPos(IntPtr handle, out int x, out int y)
		{
			QDPoint qdpoint = default(QDPoint);
			XplatUICarbon.GetGlobalMouse(ref qdpoint);
			x = (int)qdpoint.x;
			y = (int)qdpoint.y;
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x000778ED File Offset: 0x00075AED
		internal override IntPtr GetFocus()
		{
			return XplatUICarbon.FocusWindow;
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x000778F4 File Offset: 0x00075AF4
		internal override bool GetFontMetrics(Graphics g, Font font, out int ascent, out int descent)
		{
			FontFamily fontFamily = font.FontFamily;
			ascent = fontFamily.GetCellAscent(font.Style);
			descent = fontFamily.GetCellDescent(font.Style);
			return true;
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00077928 File Offset: 0x00075B28
		internal override Point GetMenuOrigin(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd != null)
			{
				return hwnd.MenuOrigin;
			}
			return Point.Empty;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0007794C File Offset: 0x00075B4C
		internal override bool GetMessage(object queue_id, ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr eventDispatcherTarget = XplatUICarbon.GetEventDispatcherTarget();
			this.CheckTimers(DateTime.UtcNow);
			XplatUICarbon.ReceiveNextEvent(0U, IntPtr.Zero, 0.0, true, ref zero);
			if (zero != IntPtr.Zero && eventDispatcherTarget != IntPtr.Zero)
			{
				XplatUICarbon.SendEventToEventTarget(zero, eventDispatcherTarget);
				XplatUICarbon.ReleaseEvent(zero);
			}
			object obj2;
			for (;;)
			{
				object obj = XplatUICarbon.queuelock;
				lock (obj)
				{
					if (XplatUICarbon.MessageQueue.Count <= 0)
					{
						if (this.Idle != null)
						{
							this.Idle(this, EventArgs.Empty);
						}
						else if (this.TimerList.Count == 0)
						{
							XplatUICarbon.ReceiveNextEvent(0U, IntPtr.Zero, 0.15, true, ref zero);
							if (zero != IntPtr.Zero && eventDispatcherTarget != IntPtr.Zero)
							{
								XplatUICarbon.SendEventToEventTarget(zero, eventDispatcherTarget);
								XplatUICarbon.ReleaseEvent(zero);
							}
						}
						else
						{
							XplatUICarbon.ReceiveNextEvent(0U, IntPtr.Zero, this.NextTimeout(), true, ref zero);
							if (zero != IntPtr.Zero && eventDispatcherTarget != IntPtr.Zero)
							{
								XplatUICarbon.SendEventToEventTarget(zero, eventDispatcherTarget);
								XplatUICarbon.ReleaseEvent(zero);
							}
						}
						msg.hwnd = IntPtr.Zero;
						msg.message = Msg.WM_ENTERIDLE;
						return XplatUICarbon.GetMessageResult;
					}
					obj2 = XplatUICarbon.MessageQueue.Dequeue();
				}
				if (!(obj2 is GCHandle))
				{
					break;
				}
				XplatUIDriverSupport.ExecuteClientMessage((GCHandle)obj2);
			}
			msg = (MSG)obj2;
			return XplatUICarbon.GetMessageResult;
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00077AF4 File Offset: 0x00075CF4
		internal override void GetWindowPos(IntPtr handle, bool is_toplevel, out int x, out int y, out int width, out int height, out int client_width, out int client_height)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd != null)
			{
				x = hwnd.x;
				y = hwnd.y;
				width = hwnd.width;
				height = hwnd.height;
				this.PerformNCCalc(hwnd);
				client_width = hwnd.ClientRect.Width;
				client_height = hwnd.ClientRect.Height;
				return;
			}
			x = 0;
			y = 0;
			width = 0;
			height = 0;
			client_width = 0;
			client_height = 0;
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00077B70 File Offset: 0x00075D70
		internal override FormWindowState GetWindowState(IntPtr hwnd)
		{
			IntPtr intPtr = XplatUICarbon.HIViewGetWindow(hwnd);
			if (XplatUICarbon.IsWindowCollapsed(intPtr))
			{
				return FormWindowState.Minimized;
			}
			if (XplatUICarbon.IsWindowInStandardState(intPtr, IntPtr.Zero, IntPtr.Zero))
			{
				return FormWindowState.Maximized;
			}
			return FormWindowState.Normal;
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00077BA3 File Offset: 0x00075DA3
		internal override void GrabInfo(out IntPtr handle, out bool GrabConfined, out Rectangle GrabArea)
		{
			handle = XplatUICarbon.Grab.Hwnd;
			GrabConfined = XplatUICarbon.Grab.Confined;
			GrabArea = XplatUICarbon.Grab.Area;
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00077BCD File Offset: 0x00075DCD
		internal override void GrabWindow(IntPtr handle, IntPtr confine_to_handle)
		{
			XplatUICarbon.Grab.Hwnd = handle;
			XplatUICarbon.Grab.Confined = confine_to_handle != IntPtr.Zero;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00077BF0 File Offset: 0x00075DF0
		internal override void UngrabWindow(IntPtr hwnd)
		{
			bool flag = XplatUICarbon.Grab.Hwnd != IntPtr.Zero;
			XplatUICarbon.Grab.Hwnd = IntPtr.Zero;
			XplatUICarbon.Grab.Confined = false;
			if (flag)
			{
				this.SendMessage(hwnd, Msg.WM_CAPTURECHANGED, IntPtr.Zero, IntPtr.Zero);
			}
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00077C44 File Offset: 0x00075E44
		internal override void Invalidate(IntPtr handle, Rectangle rc, bool clear)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (clear)
			{
				this.AddExpose(hwnd, true, hwnd.X, hwnd.Y, hwnd.Width, hwnd.Height);
				return;
			}
			this.AddExpose(hwnd, true, rc.X, rc.Y, rc.Width, rc.Height);
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00077CA0 File Offset: 0x00075EA0
		internal override void InvalidateNC(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			this.AddExpose(hwnd, false, 0, 0, hwnd.Width, hwnd.Height);
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00077CCA File Offset: 0x00075ECA
		internal override bool IsEnabled(IntPtr handle)
		{
			return Hwnd.ObjectFromHandle(handle).Enabled;
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00077CD8 File Offset: 0x00075ED8
		internal override void KillTimer(Timer timer)
		{
			ArrayList timerList = this.TimerList;
			lock (timerList)
			{
				this.TimerList.Remove(timer);
			}
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00077D20 File Offset: 0x00075F20
		internal override PaintEventArgs PaintEventStart(ref Message msg, IntPtr handle, bool client)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(msg.HWnd);
			Hwnd hwnd2;
			if (msg.HWnd == handle)
			{
				hwnd2 = hwnd;
			}
			else
			{
				hwnd2 = Hwnd.ObjectFromHandle(handle);
			}
			if (XplatUICarbon.Caret.Visible == 1)
			{
				XplatUICarbon.Caret.Paused = true;
				this.HideCaret();
			}
			PaintEventArgs paintEventArgs;
			if (client)
			{
				Graphics graphics = Graphics.FromHwnd(hwnd2.client_window);
				Region region = new Region();
				region.MakeEmpty();
				foreach (Rectangle rectangle in hwnd.ClipRectangles)
				{
					Rectangle rectangle2 = Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom + 1);
					region.Union(rectangle2);
				}
				if (hwnd.UserClip != null)
				{
					region.Intersect(hwnd.UserClip);
				}
				graphics.Clip = region;
				paintEventArgs = new PaintEventArgs(graphics, hwnd.Invalid);
				hwnd.expose_pending = false;
				hwnd.ClearInvalidArea();
				hwnd.drawing_stack.Push(paintEventArgs);
				hwnd.drawing_stack.Push(graphics);
			}
			else
			{
				Graphics graphics = Graphics.FromHwnd(hwnd2.whole_window);
				if (!hwnd.nc_invalid.IsEmpty)
				{
					graphics.SetClip(hwnd.nc_invalid);
					paintEventArgs = new PaintEventArgs(graphics, hwnd.nc_invalid);
				}
				else
				{
					paintEventArgs = new PaintEventArgs(graphics, new Rectangle(0, 0, hwnd.width, hwnd.height));
				}
				hwnd.nc_expose_pending = false;
				hwnd.ClearNcInvalidArea();
				hwnd.drawing_stack.Push(paintEventArgs);
				hwnd.drawing_stack.Push(graphics);
			}
			return paintEventArgs;
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00077EAC File Offset: 0x000760AC
		internal override void PaintEventEnd(ref Message msg, IntPtr handle, bool client)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			try
			{
				Graphics graphics = (Graphics)hwnd.drawing_stack.Pop();
				graphics.Flush();
				graphics.Dispose();
				PaintEventArgs paintEventArgs = (PaintEventArgs)hwnd.drawing_stack.Pop();
				paintEventArgs.SetGraphics(null);
				paintEventArgs.Dispose();
			}
			catch
			{
			}
			if (XplatUICarbon.Caret.Visible == 1)
			{
				this.ShowCaret();
				XplatUICarbon.Caret.Paused = false;
			}
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00077F2C File Offset: 0x0007612C
		internal override bool PeekMessage(object queue_id, ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax, uint flags)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr eventDispatcherTarget = XplatUICarbon.GetEventDispatcherTarget();
			this.CheckTimers(DateTime.UtcNow);
			XplatUICarbon.ReceiveNextEvent(0U, IntPtr.Zero, 0.0, true, ref zero);
			if (zero != IntPtr.Zero && eventDispatcherTarget != IntPtr.Zero)
			{
				XplatUICarbon.SendEventToEventTarget(zero, eventDispatcherTarget);
				XplatUICarbon.ReleaseEvent(zero);
			}
			object obj = XplatUICarbon.queuelock;
			bool flag2;
			lock (obj)
			{
				if (XplatUICarbon.MessageQueue.Count <= 0)
				{
					flag2 = false;
				}
				else
				{
					object obj2;
					if (flags == 1U)
					{
						obj2 = XplatUICarbon.MessageQueue.Dequeue();
					}
					else
					{
						obj2 = XplatUICarbon.MessageQueue.Peek();
					}
					if (obj2 is GCHandle)
					{
						XplatUIDriverSupport.ExecuteClientMessage((GCHandle)obj2);
						flag2 = false;
					}
					else
					{
						msg = (MSG)obj2;
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0007801C File Offset: 0x0007621C
		internal override bool PostMessage(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam)
		{
			this.EnqueueMessage(new MSG
			{
				hwnd = hwnd,
				message = message,
				wParam = wParam,
				lParam = lParam
			});
			return true;
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0007805A File Offset: 0x0007625A
		internal override void PostQuitMessage(int exitCode)
		{
			this.PostMessage(XplatUICarbon.FosterParent, Msg.WM_QUIT, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override void RequestAdditionalWM_NCMessages(IntPtr hwnd, bool hover, bool leave)
		{
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00078074 File Offset: 0x00076274
		internal override void RequestNCRecalc(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				return;
			}
			this.PerformNCCalc(hwnd);
			this.SendMessage(handle, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
			this.InvalidateNC(handle);
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x00003D19 File Offset: 0x00001F19
		[MonoTODO]
		internal override void ResetMouseHover(IntPtr handle)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x000780B0 File Offset: 0x000762B0
		internal override void ScreenToClient(IntPtr handle, ref int x, ref int y)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			Point point = this.ConvertScreenPointToClient(hwnd.ClientWindow, new Point(x, y));
			x = point.X;
			y = point.Y;
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x000780EC File Offset: 0x000762EC
		internal override void ScreenToMenu(IntPtr handle, ref int x, ref int y)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			Point point = this.ConvertScreenPointToClient(hwnd.WholeWindow, new Point(x, y));
			x = point.X;
			y = point.Y;
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00078128 File Offset: 0x00076328
		internal override void ScrollWindow(IntPtr handle, Rectangle area, int XAmount, int YAmount, bool clear)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			this.Invalidate(handle, new Rectangle(0, 0, hwnd.Width, hwnd.Height), false);
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00078158 File Offset: 0x00076358
		internal override void ScrollWindow(IntPtr handle, int XAmount, int YAmount, bool clear)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			this.Invalidate(handle, new Rectangle(0, 0, hwnd.Width, hwnd.Height), false);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00078188 File Offset: 0x00076388
		[MonoTODO]
		internal override void SendAsyncMethod(AsyncMethodData method)
		{
			object obj = XplatUICarbon.queuelock;
			lock (obj)
			{
				XplatUICarbon.MessageQueue.Enqueue(GCHandle.Alloc(method));
			}
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x000781D8 File Offset: 0x000763D8
		[MonoTODO]
		internal override IntPtr SendMessage(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam)
		{
			return NativeWindow.WndProc(hwnd, message, wParam, lParam);
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x000781E4 File Offset: 0x000763E4
		internal override void SetCaretPos(IntPtr hwnd, int x, int y)
		{
			if (hwnd != IntPtr.Zero && hwnd == XplatUICarbon.Caret.Hwnd)
			{
				XplatUICarbon.Caret.X = x;
				XplatUICarbon.Caret.Y = y;
				this.ClientToScreen(hwnd, ref x, ref y);
				this.SizeWindow(new Rectangle(x, y, XplatUICarbon.Caret.Width, XplatUICarbon.Caret.Height), XplatUICarbon.CaretWindow);
				XplatUICarbon.Caret.Timer.Stop();
				this.HideCaret();
				if (XplatUICarbon.Caret.Visible == 1)
				{
					this.ShowCaret();
					XplatUICarbon.Caret.Timer.Start();
				}
			}
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00003D19 File Offset: 0x00001F19
		internal override void SetClipRegion(IntPtr hwnd, Region region)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00078291 File Offset: 0x00076491
		internal override void SetCursor(IntPtr window, IntPtr cursor)
		{
			Hwnd.ObjectFromHandle(window).Cursor = cursor;
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x0007829F File Offset: 0x0007649F
		internal override void SetCursorPos(IntPtr handle, int x, int y)
		{
			XplatUICarbon.CGDisplayMoveCursorToPoint(XplatUICarbon.CGMainDisplayID(), new CGPoint(x, y));
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x000782B2 File Offset: 0x000764B2
		internal override void SetFocus(IntPtr handle)
		{
			if (XplatUICarbon.FocusWindow != IntPtr.Zero)
			{
				this.PostMessage(XplatUICarbon.FocusWindow, Msg.WM_KILLFOCUS, handle, IntPtr.Zero);
			}
			this.PostMessage(handle, Msg.WM_SETFOCUS, XplatUICarbon.FocusWindow, IntPtr.Zero);
			XplatUICarbon.FocusWindow = handle;
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x000782F4 File Offset: 0x000764F4
		internal override void SetIcon(IntPtr handle, Icon icon)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (XplatUICarbon.WindowMapping[hwnd.Handle] != null)
			{
				if (icon == null)
				{
					XplatUICarbon.RestoreApplicationDockTileImage();
					return;
				}
				Bitmap bitmap = new Bitmap(128, 128);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.DrawImage(icon.ToBitmap(), 0, 0, 128, 128);
				}
				int num = 0;
				int num2 = bitmap.Width * bitmap.Height;
				IntPtr[] array = new IntPtr[num2];
				for (int i = 0; i < bitmap.Height; i++)
				{
					for (int j = 0; j < bitmap.Width; j++)
					{
						int num3 = bitmap.GetPixel(j, i).ToArgb();
						if (BitConverter.IsLittleEndian)
						{
							byte b = (byte)((num3 >> 24) & 255);
							byte b2 = (byte)((num3 >> 16) & 255);
							byte b3 = (byte)((num3 >> 8) & 255);
							byte b4 = (byte)(num3 & 255);
							array[num++] = (IntPtr)((int)b + ((int)b2 << 8) + ((int)b3 << 16) + ((int)b4 << 24));
						}
						else
						{
							array[num++] = (IntPtr)num3;
						}
					}
				}
				IntPtr intPtr = XplatUICarbon.CGDataProviderCreateWithData(IntPtr.Zero, array, num2 * 4, IntPtr.Zero);
				XplatUICarbon.SetApplicationDockTileImage(XplatUICarbon.CGImageCreate(128, 128, 8, 32, 512, XplatUICarbon.CGColorSpaceCreateDeviceRGB(), 4U, intPtr, IntPtr.Zero, 0, 0));
			}
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0007848C File Offset: 0x0007668C
		internal override void SetModal(IntPtr handle, bool Modal)
		{
			IntPtr intPtr = XplatUICarbon.HIViewGetWindow(Hwnd.ObjectFromHandle(handle).WholeWindow);
			if (Modal)
			{
				XplatUICarbon.BeginAppModalStateForWindow(intPtr);
				return;
			}
			XplatUICarbon.EndAppModalStateForWindow(intPtr);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x000784BC File Offset: 0x000766BC
		internal override IntPtr SetParent(IntPtr handle, IntPtr parent)
		{
			IntPtr zero = IntPtr.Zero;
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			hwnd.Parent = Hwnd.ObjectFromHandle(parent);
			if (XplatUICarbon.HIViewGetSuperview(hwnd.whole_window) != IntPtr.Zero)
			{
				XplatUICarbon.HIViewRemoveFromSuperview(hwnd.whole_window);
			}
			if (hwnd.parent == null)
			{
				XplatUICarbon.HIViewFindByID(XplatUICarbon.HIViewGetRoot(XplatUICarbon.FosterParent), new HIViewID(2003398244U, 1U), ref zero);
			}
			XplatUICarbon.HIViewAddSubview((hwnd.parent == null) ? zero : hwnd.Parent.client_window, hwnd.whole_window);
			XplatUICarbon.HIViewPlaceInSuperviewAt(hwnd.whole_window, (float)hwnd.X, (float)hwnd.Y);
			XplatUICarbon.HIViewAddSubview(hwnd.whole_window, hwnd.client_window);
			XplatUICarbon.HIViewPlaceInSuperviewAt(hwnd.client_window, (float)hwnd.ClientRect.X, (float)hwnd.ClientRect.Y);
			return IntPtr.Zero;
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x000785A8 File Offset: 0x000767A8
		internal override void SetTimer(Timer timer)
		{
			ArrayList timerList = this.TimerList;
			lock (timerList)
			{
				this.TimerList.Add(timer);
			}
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x000785F0 File Offset: 0x000767F0
		internal override bool SetTopmost(IntPtr hWnd, bool Enabled)
		{
			XplatUICarbon.HIViewSetZOrder(hWnd, 1, IntPtr.Zero);
			return true;
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00006F54 File Offset: 0x00005154
		internal override bool SetOwner(IntPtr hWnd, IntPtr hWndOwner)
		{
			return true;
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00078600 File Offset: 0x00076800
		internal override bool SetVisible(IntPtr handle, bool visible, bool activate)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			object obj = XplatUICarbon.WindowMapping[hwnd.Handle];
			if (obj != null)
			{
				if (visible)
				{
					XplatUICarbon.ShowWindow((IntPtr)obj);
				}
				else
				{
					XplatUICarbon.HideWindow((IntPtr)obj);
				}
			}
			if (visible)
			{
				this.SendMessage(handle, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
			}
			XplatUICarbon.HIViewSetVisible(hwnd.whole_window, visible);
			XplatUICarbon.HIViewSetVisible(hwnd.client_window, visible);
			hwnd.visible = visible;
			hwnd.Mapped = true;
			return true;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override void SetAllowDrop(IntPtr handle, bool value)
		{
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0007868C File Offset: 0x0007688C
		internal override void SetBorderStyle(IntPtr handle, FormBorderStyle border_style)
		{
			Form form = Control.FromHandle(handle) as Form;
			if (form != null && form.window_manager == null && (border_style == FormBorderStyle.FixedToolWindow || border_style == FormBorderStyle.SizableToolWindow))
			{
				form.window_manager = new ToolWindowManager(form);
			}
			this.RequestNCRecalc(handle);
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x000786CB File Offset: 0x000768CB
		internal override void SetMenu(IntPtr handle, Menu menu)
		{
			Hwnd.ObjectFromHandle(handle).menu = menu;
			this.RequestNCRecalc(handle);
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override void SetWindowMinMax(IntPtr handle, Rectangle maximized, Size min, Size max)
		{
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x000786E0 File Offset: 0x000768E0
		internal override void SetWindowPos(IntPtr handle, int x, int y, int width, int height)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				return;
			}
			if (width < 0)
			{
				width = 0;
			}
			if (height < 0)
			{
				height = 0;
			}
			if (hwnd.zero_sized && width > 0 && height > 0)
			{
				if (hwnd.visible)
				{
					XplatUICarbon.HIViewSetVisible(hwnd.WholeWindow, true);
				}
				hwnd.zero_sized = false;
			}
			if (width < 1 || height < 1)
			{
				hwnd.zero_sized = true;
				XplatUICarbon.HIViewSetVisible(hwnd.WholeWindow, false);
			}
			if (hwnd.x == x && hwnd.y == y && hwnd.width == width && hwnd.height == height)
			{
				return;
			}
			if (!hwnd.zero_sized)
			{
				hwnd.x = x;
				hwnd.y = y;
				hwnd.width = width;
				hwnd.height = height;
				this.SendMessage(hwnd.client_window, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
				CreateParams createParams = Control.FromHandle(handle).GetCreateParams();
				Size size = XplatUICarbon.TranslateWindowSizeToQuartzWindowSize(createParams, new Size(width, height));
				Rect rect = default(Rect);
				if (XplatUICarbon.WindowMapping[hwnd.Handle] != null)
				{
					if (this.StyleSet(createParams.Style, WindowStyles.WS_POPUP))
					{
						XplatUICarbon.SetRect(ref rect, (short)x, (short)y, (short)(x + size.Width), (short)(y + size.Height));
					}
					else
					{
						XplatUICarbon.SetRect(ref rect, (short)x, (short)(y + XplatUICarbon.MenuBarHeight), (short)(x + size.Width), (short)(y + XplatUICarbon.MenuBarHeight + size.Height));
					}
					XplatUICarbon.SetWindowBounds((IntPtr)XplatUICarbon.WindowMapping[hwnd.Handle], 33U, ref rect);
					HIRect hirect = new HIRect(0, 0, size.Width, size.Height);
					XplatUICarbon.HIViewSetFrame(hwnd.whole_window, ref hirect);
					this.SetCaretPos(XplatUICarbon.Caret.Hwnd, XplatUICarbon.Caret.X, XplatUICarbon.Caret.Y);
				}
				else
				{
					HIRect hirect2 = new HIRect(x, y, size.Width, size.Height);
					XplatUICarbon.HIViewSetFrame(hwnd.whole_window, ref hirect2);
				}
				this.PerformNCCalc(hwnd);
			}
			hwnd.x = x;
			hwnd.y = y;
			hwnd.width = width;
			hwnd.height = height;
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00078914 File Offset: 0x00076B14
		internal override void SetWindowState(IntPtr handle, FormWindowState state)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			IntPtr intPtr = XplatUICarbon.HIViewGetWindow(handle);
			switch (state)
			{
			case FormWindowState.Normal:
				XplatUICarbon.ZoomWindow(intPtr, 7, false);
				return;
			case FormWindowState.Minimized:
				XplatUICarbon.CollapseWindow(intPtr, true);
				return;
			case FormWindowState.Maximized:
			{
				Form form = Control.FromHandle(hwnd.Handle) as Form;
				if (form != null && form.FormBorderStyle == FormBorderStyle.None)
				{
					Rect rect = default(Rect);
					HIRect hirect = XplatUICarbon.CGDisplayBounds(XplatUICarbon.CGMainDisplayID());
					XplatUICarbon.SetRect(ref rect, 0, 0, (short)hirect.size.width, (short)hirect.size.height);
					XplatUICarbon.SetWindowBounds((IntPtr)XplatUICarbon.WindowMapping[hwnd.Handle], 33U, ref rect);
					XplatUICarbon.HIViewSetFrame(hwnd.whole_window, ref hirect);
					return;
				}
				XplatUICarbon.ZoomWindow(intPtr, 8, false);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x000789E4 File Offset: 0x00076BE4
		internal override void SetWindowStyle(IntPtr handle, CreateParams cp)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			this.SetHwndStyles(hwnd, cp);
			if (XplatUICarbon.WindowMapping[hwnd.Handle] != null)
			{
				WindowAttributes windowAttributes = (WindowAttributes)34078720U;
				if ((cp.Style & 131072) != 0)
				{
					windowAttributes |= WindowAttributes.kWindowCollapseBoxAttribute;
				}
				if ((cp.Style & 65536) != 0)
				{
					windowAttributes |= (WindowAttributes)22U;
				}
				if ((cp.Style & 524288) != 0)
				{
					windowAttributes |= WindowAttributes.kWindowCloseBoxAttribute;
				}
				if ((cp.ExStyle & 128) != 0)
				{
					windowAttributes = (WindowAttributes)34078720U;
				}
				windowAttributes |= WindowAttributes.kWindowLiveResizeAttribute;
				WindowAttributes windowAttributes2 = WindowAttributes.kWindowNoAttributes;
				XplatUICarbon.GetWindowAttributes((IntPtr)XplatUICarbon.WindowMapping[hwnd.Handle], ref windowAttributes2);
				XplatUICarbon.ChangeWindowAttributes((IntPtr)XplatUICarbon.WindowMapping[hwnd.Handle], windowAttributes, windowAttributes2);
			}
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override void SetWindowTransparency(IntPtr handle, double transparency, Color key)
		{
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00002D70 File Offset: 0x00000F70
		internal override TransparencySupport SupportsTransparency()
		{
			return TransparencySupport.None;
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00078ABC File Offset: 0x00076CBC
		internal override bool SetZOrder(IntPtr handle, IntPtr after_handle, bool Top, bool Bottom)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (Top)
			{
				XplatUICarbon.HIViewSetZOrder(hwnd.whole_window, 2, IntPtr.Zero);
				return true;
			}
			if (!Bottom)
			{
				Hwnd hwnd2 = Hwnd.ObjectFromHandle(after_handle);
				XplatUICarbon.HIViewSetZOrder(hwnd.whole_window, 2, (after_handle == IntPtr.Zero) ? IntPtr.Zero : hwnd2.whole_window);
				return false;
			}
			XplatUICarbon.HIViewSetZOrder(hwnd.whole_window, 1, IntPtr.Zero);
			return true;
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00078B30 File Offset: 0x00076D30
		internal override object StartLoop(Thread thread)
		{
			return new object();
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00078B38 File Offset: 0x00076D38
		internal override bool Text(IntPtr handle, string text)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (XplatUICarbon.WindowMapping[hwnd.Handle] != null)
			{
				XplatUICarbon.SetWindowTitleWithCFString((IntPtr)XplatUICarbon.WindowMapping[hwnd.Handle], XplatUICarbon.__CFStringMakeConstantString(text));
			}
			XplatUICarbon.SetControlTitleWithCFString(hwnd.whole_window, XplatUICarbon.__CFStringMakeConstantString(text));
			XplatUICarbon.SetControlTitleWithCFString(hwnd.client_window, XplatUICarbon.__CFStringMakeConstantString(text));
			return true;
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00078BAE File Offset: 0x00076DAE
		internal override void UpdateWindow(IntPtr handle)
		{
			if (!Hwnd.ObjectFromHandle(handle).visible || !XplatUICarbon.HIViewIsVisible(handle))
			{
				return;
			}
			this.SendMessage(handle, Msg.WM_PAINT, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00078BDA File Offset: 0x00076DDA
		internal override bool TranslateMessage(ref MSG msg)
		{
			return global::System.Windows.Forms.CarbonInternal.EventHandler.TranslateMessage(ref msg);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00078BE4 File Offset: 0x00076DE4
		internal void SizeWindow(Rectangle rect, IntPtr window)
		{
			Rect rect2 = default(Rect);
			XplatUICarbon.SetRect(ref rect2, (short)rect.X, (short)rect.Y, (short)(rect.X + rect.Width), (short)(rect.Y + rect.Height));
			XplatUICarbon.SetWindowBounds(window, 33U, ref rect2);
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00078C3C File Offset: 0x00076E3C
		internal override void DrawReversibleRectangle(IntPtr handle, Rectangle rect, int line_width)
		{
			Rectangle rectangle = rect;
			int num = 0;
			int num2 = 0;
			if (XplatUICarbon.ReverseWindowMapped)
			{
				XplatUICarbon.HideWindow(XplatUICarbon.ReverseWindow);
				XplatUICarbon.ReverseWindowMapped = false;
				return;
			}
			this.ClientToScreen(handle, ref num, ref num2);
			rectangle.X += num;
			rectangle.Y += num2;
			this.SizeWindow(rectangle, XplatUICarbon.ReverseWindow);
			XplatUICarbon.ShowWindow(XplatUICarbon.ReverseWindow);
			rect.X = 0;
			rect.Y = 0;
			rect.Width--;
			rect.Height--;
			Graphics graphics = Graphics.FromHwnd(XplatUICarbon.HIViewGetRoot(XplatUICarbon.ReverseWindow));
			for (int i = 0; i < line_width; i++)
			{
				graphics.DrawRectangle(ThemeEngine.Current.ResPool.GetPen(Color.Black), rect);
				rect.X++;
				rect.Y++;
				rect.Width--;
				rect.Height--;
			}
			graphics.Flush();
			graphics.Dispose();
			XplatUICarbon.ReverseWindowMapped = true;
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00078D60 File Offset: 0x00076F60
		internal override SizeF GetAutoScaleSize(Font font)
		{
			string text = "The quick brown fox jumped over the lazy dog.";
			double num = 44.54999694824219;
			return new SizeF((float)((double)Graphics.FromImage(new Bitmap(1, 1)).MeasureString(text, font).Width / num), (float)font.Height);
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x00078DA8 File Offset: 0x00076FA8
		internal override Point MousePosition
		{
			get
			{
				return this.mouse_position;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x00078DB0 File Offset: 0x00076FB0
		internal override int CaptionHeight
		{
			get
			{
				return 19;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x00050923 File Offset: 0x0004EB23
		internal override Size DragSize
		{
			get
			{
				return new Size(4, 4);
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x00078DB4 File Offset: 0x00076FB4
		internal override Size FrameBorderSize
		{
			get
			{
				return new Size(2, 2);
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x00002D70 File Offset: 0x00000F70
		internal override bool MenuAccessKeysUnderlined
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x00078DBD File Offset: 0x00076FBD
		internal override Size MinimumWindowSize
		{
			get
			{
				return new Size(110, 22);
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x00078DC8 File Offset: 0x00076FC8
		internal override Keys ModifierKeys
		{
			get
			{
				return this.KeyboardHandler.ModifierKeys;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x00078DD5 File Offset: 0x00076FD5
		internal override Rectangle VirtualScreen
		{
			get
			{
				return this.WorkingArea;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x00078DE0 File Offset: 0x00076FE0
		internal override Rectangle WorkingArea
		{
			get
			{
				HIRect hirect = XplatUICarbon.CGDisplayBounds(XplatUICarbon.CGMainDisplayID());
				return new Rectangle((int)hirect.origin.x, (int)hirect.origin.y, (int)hirect.size.width, (int)hirect.size.height);
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x00003C7A File Offset: 0x00001E7A
		[MonoTODO]
		internal override Screen[] AllScreens
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x00078E2D File Offset: 0x0007702D
		internal override bool ThemesEnabled
		{
			get
			{
				return XplatUICarbon.themes_enabled;
			}
		}

		// Token: 0x0600185D RID: 6237
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewConvertPoint(ref CGPoint point, IntPtr pView, IntPtr cView);

		// Token: 0x0600185E RID: 6238
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewChangeFeatures(IntPtr aView, ulong bitsin, ulong bitsout);

		// Token: 0x0600185F RID: 6239
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewFindByID(IntPtr rootWnd, HIViewID id, ref IntPtr outPtr);

		// Token: 0x06001860 RID: 6240
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIGrowBoxViewSetTransparent(IntPtr GrowBox, bool transparency);

		// Token: 0x06001861 RID: 6241
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr HIViewGetRoot(IntPtr hWnd);

		// Token: 0x06001862 RID: 6242
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIObjectCreate(IntPtr cfStr, uint what, ref IntPtr hwnd);

		// Token: 0x06001863 RID: 6243
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIObjectRegisterSubclass(IntPtr classid, IntPtr superclassid, uint options, EventDelegate upp, uint count, EventTypeSpec[] list, IntPtr state, ref IntPtr cls);

		// Token: 0x06001864 RID: 6244
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewPlaceInSuperviewAt(IntPtr view, float x, float y);

		// Token: 0x06001865 RID: 6245
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewAddSubview(IntPtr parentHnd, IntPtr childHnd);

		// Token: 0x06001866 RID: 6246
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr HIViewGetPreviousView(IntPtr aView);

		// Token: 0x06001867 RID: 6247
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr HIViewGetSuperview(IntPtr aView);

		// Token: 0x06001868 RID: 6248
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewRemoveFromSuperview(IntPtr aView);

		// Token: 0x06001869 RID: 6249
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewSetVisible(IntPtr vHnd, bool visible);

		// Token: 0x0600186A RID: 6250
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern bool HIViewIsVisible(IntPtr vHnd);

		// Token: 0x0600186B RID: 6251
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewSetZOrder(IntPtr hWnd, int cmd, IntPtr oHnd);

		// Token: 0x0600186C RID: 6252
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewNewTrackingArea(IntPtr inView, IntPtr inShape, ulong inID, ref IntPtr outRef);

		// Token: 0x0600186D RID: 6253
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr HIViewGetWindow(IntPtr aView);

		// Token: 0x0600186E RID: 6254
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewSetFrame(IntPtr view_handle, ref HIRect bounds);

		// Token: 0x0600186F RID: 6255
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern void SetRect(ref Rect r, short left, short top, short right, short bottom);

		// Token: 0x06001870 RID: 6256
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int ActivateWindow(IntPtr windowHnd, bool inActivate);

		// Token: 0x06001871 RID: 6257
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetAutomaticControlDragTrackingEnabledForWindow(IntPtr window, bool enabled);

		// Token: 0x06001872 RID: 6258
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr GetEventDispatcherTarget();

		// Token: 0x06001873 RID: 6259
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SendEventToEventTarget(IntPtr evt, IntPtr target);

		// Token: 0x06001874 RID: 6260
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int ReleaseEvent(IntPtr evt);

		// Token: 0x06001875 RID: 6261
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int ReceiveNextEvent(uint evtCount, IntPtr evtTypes, double timeout, bool processEvt, ref IntPtr evt);

		// Token: 0x06001876 RID: 6262
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern bool IsWindowCollapsed(IntPtr hWnd);

		// Token: 0x06001877 RID: 6263
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern bool IsWindowInStandardState(IntPtr hWnd, IntPtr a, IntPtr b);

		// Token: 0x06001878 RID: 6264
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern void CollapseWindow(IntPtr hWnd, bool collapse);

		// Token: 0x06001879 RID: 6265
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern void ZoomWindow(IntPtr hWnd, short partCode, bool front);

		// Token: 0x0600187A RID: 6266
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetWindowAttributes(IntPtr hWnd, ref WindowAttributes outAttributes);

		// Token: 0x0600187B RID: 6267
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int ChangeWindowAttributes(IntPtr hWnd, WindowAttributes inAttributes, WindowAttributes outAttributes);

		// Token: 0x0600187C RID: 6268
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int GetGlobalMouse(ref QDPoint outData);

		// Token: 0x0600187D RID: 6269
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int BeginAppModalStateForWindow(IntPtr window);

		// Token: 0x0600187E RID: 6270
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int EndAppModalStateForWindow(IntPtr window);

		// Token: 0x0600187F RID: 6271
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int CreateNewWindow(WindowClass klass, WindowAttributes attributes, ref Rect r, ref IntPtr window);

		// Token: 0x06001880 RID: 6272
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int DisposeWindow(IntPtr wHnd);

		// Token: 0x06001881 RID: 6273
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int ShowWindow(IntPtr wHnd);

		// Token: 0x06001882 RID: 6274
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int HideWindow(IntPtr wHnd);

		// Token: 0x06001883 RID: 6275
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern bool IsWindowVisible(IntPtr wHnd);

		// Token: 0x06001884 RID: 6276
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetWindowBounds(IntPtr wHnd, uint reg, ref Rect rect);

		// Token: 0x06001885 RID: 6277
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetWindowBounds(IntPtr wHnd, uint reg, ref Rect rect);

		// Token: 0x06001886 RID: 6278
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetControlTitleWithCFString(IntPtr hWnd, IntPtr titleCFStr);

		// Token: 0x06001887 RID: 6279
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetWindowTitleWithCFString(IntPtr hWnd, IntPtr titleCFStr);

		// Token: 0x06001888 RID: 6280
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr __CFStringMakeConstantString(string cString);

		// Token: 0x06001889 RID: 6281
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern short GetMBarHeight();

		// Token: 0x0600188A RID: 6282
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern void AlertSoundPlay();

		// Token: 0x0600188B RID: 6283
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern HIRect CGDisplayBounds(IntPtr displayID);

		// Token: 0x0600188C RID: 6284
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr CGMainDisplayID();

		// Token: 0x0600188D RID: 6285
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern void CGDisplayMoveCursorToPoint(IntPtr display, CGPoint point);

		// Token: 0x0600188E RID: 6286
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetCurrentProcess(ref ProcessSerialNumber psn);

		// Token: 0x0600188F RID: 6287
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int TransformProcessType(ref ProcessSerialNumber psn, uint type);

		// Token: 0x06001890 RID: 6288
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetFrontProcess(ref ProcessSerialNumber psn);

		// Token: 0x06001891 RID: 6289
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr CGColorSpaceCreateDeviceRGB();

		// Token: 0x06001892 RID: 6290
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr CGDataProviderCreateWithData(IntPtr info, IntPtr[] data, int size, IntPtr releasefunc);

		// Token: 0x06001893 RID: 6291
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr CGImageCreate(int width, int height, int bitsPerComponent, int bitsPerPixel, int bytesPerRow, IntPtr colorspace, uint bitmapInfo, IntPtr provider, IntPtr decode, int shouldInterpolate, int intent);

		// Token: 0x06001894 RID: 6292
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern void SetApplicationDockTileImage(IntPtr imageRef);

		// Token: 0x06001895 RID: 6293
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern void RestoreApplicationDockTileImage();

		// Token: 0x04001225 RID: 4645
		private static XplatUICarbon Instance;

		// Token: 0x04001226 RID: 4646
		private static int RefCount;

		// Token: 0x04001227 RID: 4647
		private static bool themes_enabled;

		// Token: 0x04001228 RID: 4648
		internal static IntPtr FocusWindow;

		// Token: 0x04001229 RID: 4649
		internal static IntPtr ActiveWindow;

		// Token: 0x0400122A RID: 4650
		internal static IntPtr UnactiveWindow;

		// Token: 0x0400122B RID: 4651
		internal static IntPtr ReverseWindow;

		// Token: 0x0400122C RID: 4652
		internal static IntPtr CaretWindow;

		// Token: 0x0400122D RID: 4653
		internal static Hwnd MouseHwnd;

		// Token: 0x0400122E RID: 4654
		internal static MouseButtons MouseState;

		// Token: 0x0400122F RID: 4655
		internal static Hover Hover;

		// Token: 0x04001230 RID: 4656
		internal static HwndDelegate HwndDelegate = new HwndDelegate(XplatUICarbon.GetClippingRectangles);

		// Token: 0x04001231 RID: 4657
		internal Point mouse_position;

		// Token: 0x04001232 RID: 4658
		internal ApplicationHandler ApplicationHandler;

		// Token: 0x04001233 RID: 4659
		internal ControlHandler ControlHandler;

		// Token: 0x04001234 RID: 4660
		internal HIObjectHandler HIObjectHandler;

		// Token: 0x04001235 RID: 4661
		internal KeyboardHandler KeyboardHandler;

		// Token: 0x04001236 RID: 4662
		internal MouseHandler MouseHandler;

		// Token: 0x04001237 RID: 4663
		internal WindowHandler WindowHandler;

		// Token: 0x04001238 RID: 4664
		internal static GrabStruct Grab;

		// Token: 0x04001239 RID: 4665
		internal static Caret Caret;

		// Token: 0x0400123A RID: 4666
		private static Dnd Dnd;

		// Token: 0x0400123B RID: 4667
		private static Hashtable WindowMapping;

		// Token: 0x0400123C RID: 4668
		private static Hashtable HandleMapping;

		// Token: 0x0400123D RID: 4669
		private static IntPtr FosterParent;

		// Token: 0x0400123E RID: 4670
		private static IntPtr Subclass;

		// Token: 0x0400123F RID: 4671
		private static int MenuBarHeight;

		// Token: 0x04001240 RID: 4672
		internal static ArrayList UtilityWindows;

		// Token: 0x04001241 RID: 4673
		private static Queue MessageQueue;

		// Token: 0x04001242 RID: 4674
		private static bool GetMessageResult;

		// Token: 0x04001243 RID: 4675
		private static bool ReverseWindowMapped;

		// Token: 0x04001244 RID: 4676
		private ArrayList TimerList;

		// Token: 0x04001245 RID: 4677
		private static bool in_doevents;

		// Token: 0x04001246 RID: 4678
		private static readonly object instancelock = new object();

		// Token: 0x04001247 RID: 4679
		private static readonly object queuelock = new object();

		// Token: 0x04001248 RID: 4680
		[CompilerGenerated]
		private global::System.EventHandler Idle;
	}
}
