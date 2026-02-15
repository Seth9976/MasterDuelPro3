using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using Mono.Unix.Native;

namespace System.Windows.Forms
{
	// Token: 0x020002C9 RID: 713
	internal class XplatUIX11 : XplatUIDriver
	{
		// Token: 0x06001A05 RID: 6661 RVA: 0x0007BA7C File Offset: 0x00079C7C
		private XplatUIX11()
		{
			XplatUIX11.RefCount = 0;
			XplatUIX11.in_doevents = false;
			XplatUIX11.XlibLock = new object();
			X11Keyboard.XlibLock = XplatUIX11.XlibLock;
			XplatUIX11.MessageQueues = Hashtable.Synchronized(new Hashtable(7));
			XplatUIX11.unattached_timer_list = ArrayList.Synchronized(new ArrayList(3));
			XplatUIX11.messageHold = Hashtable.Synchronized(new Hashtable(3));
			XplatUIX11.Clipboard = new ClipboardData();
			XplatUIX11.XInitThreads();
			XplatUIX11.ErrorExceptions = false;
			this.SetDisplay(XplatUIX11.XOpenDisplay(IntPtr.Zero));
			X11DesktopColors.Initialize();
			try
			{
				XplatUIX11.XkbSetDetectableAutoRepeat(XplatUIX11.DisplayHandle, true, IntPtr.Zero);
				XplatUIX11.detectable_key_auto_repeat = true;
			}
			catch
			{
				Console.Error.WriteLine("Could not disable keyboard auto repeat, will attempt to disable manually.");
				XplatUIX11.detectable_key_auto_repeat = false;
			}
			XplatUIX11.ErrorHandler = new XErrorHandler(this.HandleError);
			XplatUIX11.XSetErrorHandler(XplatUIX11.ErrorHandler);
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x0007BB68 File Offset: 0x00079D68
		~XplatUIX11()
		{
			Graphics.FromHdcInternal(IntPtr.Zero);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x0007BB9C File Offset: 0x00079D9C
		public static XplatUIX11 GetInstance()
		{
			object obj = XplatUIX11.lockobj;
			lock (obj)
			{
				if (XplatUIX11.Instance == null)
				{
					XplatUIX11.Instance = new XplatUIX11();
				}
				XplatUIX11.RefCount++;
			}
			return XplatUIX11.Instance;
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x0007BC00 File Offset: 0x00079E00
		internal static IntPtr Display
		{
			get
			{
				return XplatUIX11.DisplayHandle;
			}
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0007BC08 File Offset: 0x00079E08
		internal void SetDisplay(IntPtr display_handle)
		{
			if (display_handle != IntPtr.Zero)
			{
				if (XplatUIX11.DisplayHandle != IntPtr.Zero && XplatUIX11.FosterParent != IntPtr.Zero)
				{
					Hwnd hwnd = Hwnd.ObjectFromHandle(XplatUIX11.FosterParent);
					XplatUIX11.XDestroyWindow(XplatUIX11.DisplayHandle, XplatUIX11.FosterParent);
					hwnd.Dispose();
				}
				if (XplatUIX11.DisplayHandle != IntPtr.Zero)
				{
					XplatUIX11.XCloseDisplay(XplatUIX11.DisplayHandle);
				}
				XplatUIX11.DisplayHandle = display_handle;
				Graphics.FromHdcInternal(XplatUIX11.DisplayHandle);
				XplatUIX11.XQueryExtension(XplatUIX11.DisplayHandle, "RENDER", ref this.render_major_opcode, ref this.render_first_event, ref this.render_first_error);
				if (Environment.GetEnvironmentVariable("MONO_XSYNC") != null)
				{
					XplatUIX11.XSynchronize(XplatUIX11.DisplayHandle, true);
				}
				if (Environment.GetEnvironmentVariable("MONO_XEXCEPTIONS") != null)
				{
					XplatUIX11.ErrorExceptions = true;
				}
				XplatUIX11.ScreenNo = XplatUIX11.XDefaultScreen(XplatUIX11.DisplayHandle);
				XplatUIX11.RootWindow = XplatUIX11.XRootWindow(XplatUIX11.DisplayHandle, XplatUIX11.ScreenNo);
				XplatUIX11.DefaultColormap = XplatUIX11.XDefaultColormap(XplatUIX11.DisplayHandle, XplatUIX11.ScreenNo);
				XplatUIX11.FosterParent = XplatUIX11.XCreateSimpleWindow(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, 0, 0, 1, 1, 0, UIntPtr.Zero, UIntPtr.Zero);
				if (XplatUIX11.FosterParent == IntPtr.Zero)
				{
					Console.WriteLine("XplatUIX11 Constructor failed to create FosterParent");
				}
				Hwnd hwnd2 = new Hwnd();
				hwnd2.Queue = this.ThreadQueue(Thread.CurrentThread);
				hwnd2.WholeWindow = XplatUIX11.FosterParent;
				hwnd2.ClientWindow = XplatUIX11.FosterParent;
				Hwnd hwnd3 = new Hwnd();
				hwnd3.Queue = this.ThreadQueue(Thread.CurrentThread);
				hwnd3.whole_window = XplatUIX11.RootWindow;
				hwnd3.ClientWindow = XplatUIX11.RootWindow;
				XplatUIX11.listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
				IPEndPoint ipendPoint = new IPEndPoint(IPAddress.Loopback, 0);
				XplatUIX11.listen.Bind(ipendPoint);
				XplatUIX11.listen.Listen(1);
				XplatUIX11.network_buffer = new byte[10];
				XplatUIX11.wake = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
				XplatUIX11.wake.Connect(XplatUIX11.listen.LocalEndPoint);
				XplatUIX11.wake.Blocking = false;
				XplatUIX11.wake_receive = XplatUIX11.listen.Accept();
				XplatUIX11.pollfds = new Pollfd[2];
				XplatUIX11.pollfds[0] = default(Pollfd);
				XplatUIX11.pollfds[0].fd = XplatUIX11.XConnectionNumber(XplatUIX11.DisplayHandle);
				XplatUIX11.pollfds[0].events = PollEvents.POLLIN;
				XplatUIX11.pollfds[1] = default(Pollfd);
				XplatUIX11.pollfds[1].fd = XplatUIX11.wake_receive.Handle.ToInt32();
				XplatUIX11.pollfds[1].events = PollEvents.POLLIN;
				XplatUIX11.Keyboard = new X11Keyboard(XplatUIX11.DisplayHandle, XplatUIX11.FosterParent);
				XplatUIX11.Dnd = new X11Dnd(XplatUIX11.DisplayHandle, XplatUIX11.Keyboard);
				XplatUIX11.DoubleClickInterval = 500;
				XplatUIX11.HoverState.Interval = 500;
				XplatUIX11.HoverState.Timer = new Timer();
				XplatUIX11.HoverState.Timer.Enabled = false;
				XplatUIX11.HoverState.Timer.Interval = XplatUIX11.HoverState.Interval;
				XplatUIX11.HoverState.Timer.Tick += this.MouseHover;
				XplatUIX11.HoverState.Size = new Size(4, 4);
				XplatUIX11.HoverState.X = -1;
				XplatUIX11.HoverState.Y = -1;
				XplatUIX11.ActiveWindow = IntPtr.Zero;
				XplatUIX11.FocusWindow = IntPtr.Zero;
				XplatUIX11.ModalWindows = new Stack(3);
				XplatUIX11.MouseState = MouseButtons.None;
				this.mouse_position = new Point(0, 0);
				XplatUIX11.Caret.Timer = new Timer();
				XplatUIX11.Caret.Timer.Interval = 500;
				XplatUIX11.Caret.Timer.Tick += this.CaretCallback;
				XplatUIX11.SetupAtoms();
				XplatUIX11.XSelectInput(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, new IntPtr((int)(EventMask.PropertyChangeMask | XplatUIX11.Keyboard.KeyEventMask)));
				XplatUIX11.ErrorHandler = new XErrorHandler(this.HandleError);
				XplatUIX11.XSetErrorHandler(XplatUIX11.ErrorHandler);
				return;
			}
			throw new ArgumentNullException("Display", "Could not open display (X-Server required. Check your DISPLAY environment variable)");
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0007C02C File Offset: 0x0007A22C
		private int unixtime()
		{
			return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x0007C058 File Offset: 0x0007A258
		private static void SetupAtoms()
		{
			string[] array = new string[]
			{
				"WM_PROTOCOLS", "WM_DELETE_WINDOW", "WM_TAKE_FOCUS", "_NET_DESKTOP_GEOMETRY", "_NET_CURRENT_DESKTOP", "_NET_ACTIVE_WINDOW", "_NET_WORKAREA", "_NET_WM_MOVERESIZE", "_NET_WM_NAME", "_NET_WM_WINDOW_TYPE",
				"_NET_WM_STATE", "_NET_WM_ICON", "_NET_WM_USER_TIME", "_NET_FRAME_EXTENTS", "_NET_SYSTEM_TRAY_OPCODE", "_NET_WM_STATE_MAXIMIZED_HORZ", "_NET_WM_STATE_MAXIMIZED_VERT", "_NET_WM_STATE_HIDDEN", "_XEMBED", "_XEMBED_INFO",
				"_MOTIF_WM_HINTS", "_NET_WM_STATE_SKIP_TASKBAR", "_NET_WM_STATE_ABOVE", "_NET_WM_STATE_MODAL", "_NET_WM_CONTEXT_HELP", "_NET_WM_WINDOW_OPACITY", "_NET_WM_WINDOW_TYPE_UTILITY", "_NET_WM_WINDOW_TYPE_NORMAL", "CLIPBOARD", "PRIMARY",
				"COMPOUND_TEXT", "UTF8_STRING", "UTF16_STRING", "RICHTEXTFORMAT", "TARGETS", "_SWF_AsyncAtom", "_SWF_PostMessageAtom", "_SWF_HoverAtom"
			};
			IntPtr[] array2 = new IntPtr[array.Length];
			XplatUIX11.XInternAtoms(XplatUIX11.DisplayHandle, array, array.Length, false, array2);
			int num = 0;
			XplatUIX11.WM_PROTOCOLS = array2[num++];
			XplatUIX11.WM_DELETE_WINDOW = array2[num++];
			XplatUIX11.WM_TAKE_FOCUS = array2[num++];
			XplatUIX11._NET_DESKTOP_GEOMETRY = array2[num++];
			XplatUIX11._NET_CURRENT_DESKTOP = array2[num++];
			XplatUIX11._NET_ACTIVE_WINDOW = array2[num++];
			XplatUIX11._NET_WORKAREA = array2[num++];
			XplatUIX11._NET_WM_MOVERESIZE = array2[num++];
			XplatUIX11._NET_WM_NAME = array2[num++];
			XplatUIX11._NET_WM_WINDOW_TYPE = array2[num++];
			XplatUIX11._NET_WM_STATE = array2[num++];
			XplatUIX11._NET_WM_ICON = array2[num++];
			XplatUIX11._NET_WM_USER_TIME = array2[num++];
			XplatUIX11._NET_FRAME_EXTENTS = array2[num++];
			XplatUIX11._NET_SYSTEM_TRAY_OPCODE = array2[num++];
			XplatUIX11._NET_WM_STATE_MAXIMIZED_HORZ = array2[num++];
			XplatUIX11._NET_WM_STATE_MAXIMIZED_VERT = array2[num++];
			XplatUIX11._NET_WM_STATE_HIDDEN = array2[num++];
			XplatUIX11._XEMBED = array2[num++];
			XplatUIX11._XEMBED_INFO = array2[num++];
			XplatUIX11._MOTIF_WM_HINTS = array2[num++];
			XplatUIX11._NET_WM_STATE_SKIP_TASKBAR = array2[num++];
			XplatUIX11._NET_WM_STATE_ABOVE = array2[num++];
			XplatUIX11._NET_WM_STATE_MODAL = array2[num++];
			XplatUIX11._NET_WM_CONTEXT_HELP = array2[num++];
			XplatUIX11._NET_WM_WINDOW_OPACITY = array2[num++];
			XplatUIX11._NET_WM_WINDOW_TYPE_UTILITY = array2[num++];
			XplatUIX11._NET_WM_WINDOW_TYPE_NORMAL = array2[num++];
			XplatUIX11.CLIPBOARD = array2[num++];
			XplatUIX11.PRIMARY = array2[num++];
			XplatUIX11.OEMTEXT = array2[num++];
			XplatUIX11.UTF8_STRING = array2[num++];
			XplatUIX11.UTF16_STRING = array2[num++];
			XplatUIX11.RICHTEXTFORMAT = array2[num++];
			XplatUIX11.TARGETS = array2[num++];
			XplatUIX11.AsyncAtom = array2[num++];
			XplatUIX11.PostAtom = array2[num++];
			XplatUIX11.HoverState.Atom = array2[num++];
			XplatUIX11._NET_SYSTEM_TRAY_S = XplatUIX11.XInternAtom(XplatUIX11.DisplayHandle, "_NET_SYSTEM_TRAY_S" + XplatUIX11.ScreenNo.ToString(), false);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x0007C3C7 File Offset: 0x0007A5C7
		private void SendNetWMMessage(IntPtr window, IntPtr message_type, IntPtr l0, IntPtr l1, IntPtr l2)
		{
			this.SendNetWMMessage(window, message_type, l0, l1, l2, IntPtr.Zero);
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0007C3DC File Offset: 0x0007A5DC
		private void SendNetWMMessage(IntPtr window, IntPtr message_type, IntPtr l0, IntPtr l1, IntPtr l2, IntPtr l3)
		{
			XEvent xevent = default(XEvent);
			xevent.ClientMessageEvent.type = XEventName.ClientMessage;
			xevent.ClientMessageEvent.send_event = true;
			xevent.ClientMessageEvent.window = window;
			xevent.ClientMessageEvent.message_type = message_type;
			xevent.ClientMessageEvent.format = 32;
			xevent.ClientMessageEvent.ptr1 = l0;
			xevent.ClientMessageEvent.ptr2 = l1;
			xevent.ClientMessageEvent.ptr3 = l2;
			xevent.ClientMessageEvent.ptr4 = l3;
			XplatUIX11.XSendEvent(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, false, new IntPtr(1572864), ref xevent);
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0007C488 File Offset: 0x0007A688
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

		// Token: 0x06001A0F RID: 6671 RVA: 0x0007657A File Offset: 0x0007477A
		private bool StyleSet(int s, WindowStyles ws)
		{
			return (s & (int)ws) == (int)ws;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0007657A File Offset: 0x0007477A
		private bool ExStyleSet(int ex, WindowExStyles exws)
		{
			return (ex & (int)exws) == (int)exws;
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x0007C556 File Offset: 0x0007A756
		internal static Rectangle TranslateClientRectangleToXClientRectangle(Hwnd hwnd)
		{
			return XplatUIX11.TranslateClientRectangleToXClientRectangle(hwnd, Control.FromHandle(hwnd.Handle));
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0007C56C File Offset: 0x0007A76C
		internal static Rectangle TranslateClientRectangleToXClientRectangle(Hwnd hwnd, Control ctrl)
		{
			Rectangle rectangle = hwnd.ClientRect;
			Form form = ctrl as Form;
			CreateParams createParams = null;
			if (form != null)
			{
				createParams = form.GetCreateParams();
			}
			if (form != null && form.window_manager == null && !createParams.IsSet(WindowExStyles.WS_EX_TOOLWINDOW))
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

		// Token: 0x06001A13 RID: 6675 RVA: 0x0007C651 File Offset: 0x0007A851
		internal static Size TranslateWindowSizeToXWindowSize(CreateParams cp)
		{
			return XplatUIX11.TranslateWindowSizeToXWindowSize(cp, new Size(cp.Width, cp.Height));
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x0007C66C File Offset: 0x0007A86C
		internal static Size TranslateWindowSizeToXWindowSize(CreateParams cp, Size size)
		{
			Form form = cp.control as Form;
			if (form != null && form.window_manager == null && !cp.IsSet(WindowExStyles.WS_EX_TOOLWINDOW))
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

		// Token: 0x06001A15 RID: 6677 RVA: 0x0007C704 File Offset: 0x0007A904
		internal static Size TranslateXWindowSizeToWindowSize(CreateParams cp, int xWidth, int xHeight)
		{
			Size size = new Size(xWidth, xHeight);
			Form form = cp.control as Form;
			if (form != null && form.window_manager == null && !cp.IsSet(WindowExStyles.WS_EX_TOOLWINDOW))
			{
				Hwnd.Borders borders = Hwnd.GetBorders(cp, null);
				Size size2 = size;
				size2.Width += borders.left + borders.right;
				size2.Height += borders.top + borders.bottom;
				size = size2;
			}
			return size;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x0007C784 File Offset: 0x0007A984
		internal static Point GetTopLevelWindowLocation(Hwnd hwnd)
		{
			int num;
			int num2;
			IntPtr intPtr;
			XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11.RootWindow, 0, 0, out num, out num2, out intPtr);
			Hwnd.Borders borders = XplatUIX11.FrameExtents(hwnd.whole_window);
			num -= borders.left;
			num2 -= borders.top;
			return new Point(num, num2);
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x0007C7D4 File Offset: 0x0007A9D4
		private void DeriveStyles(int Style, int ExStyle, out FormBorderStyle border_style, out bool border_static, out TitleStyle title_style, out int caption_height, out int tool_caption_height)
		{
			caption_height = 0;
			tool_caption_height = 19;
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
					caption_height = 19;
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
					caption_height = 19;
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

		// Token: 0x06001A18 RID: 6680 RVA: 0x0007C973 File Offset: 0x0007AB73
		private void SetHwndStyles(Hwnd hwnd, CreateParams cp)
		{
			this.DeriveStyles(cp.Style, cp.ExStyle, out hwnd.border_style, out hwnd.border_static, out hwnd.title_style, out hwnd.caption_height, out hwnd.tool_caption_height);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x0007C9A8 File Offset: 0x0007ABA8
		private void SetWMStyles(Hwnd hwnd, CreateParams cp)
		{
			if (cp.HasWindowManager && !cp.IsSet(WindowExStyles.WS_EX_TOOLWINDOW))
			{
				return;
			}
			int[] array = new int[8];
			MotifWmHints motifWmHints = default(MotifWmHints);
			MotifFunctions motifFunctions = (MotifFunctions)0;
			MotifDecorations motifDecorations = (MotifDecorations)0;
			IntPtr intPtr = XplatUIX11._NET_WM_WINDOW_TYPE_NORMAL;
			IntPtr intPtr2 = IntPtr.Zero;
			motifWmHints.flags = (IntPtr)3;
			motifWmHints.functions = (IntPtr)0;
			motifWmHints.decorations = (IntPtr)0;
			Form form = cp.control as Form;
			if (this.ExStyleSet(cp.ExStyle, WindowExStyles.WS_EX_TOOLWINDOW))
			{
				motifFunctions |= MotifFunctions.Resize | MotifFunctions.Move | MotifFunctions.Minimize | MotifFunctions.Maximize;
			}
			else if (form != null && form.FormBorderStyle == FormBorderStyle.None)
			{
				motifFunctions |= MotifFunctions.All | MotifFunctions.Resize;
			}
			else
			{
				if (this.StyleSet(cp.Style, WindowStyles.WS_CAPTION))
				{
					motifFunctions |= MotifFunctions.Move;
					motifDecorations |= MotifDecorations.Title | MotifDecorations.Menu;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_THICKFRAME))
				{
					motifFunctions |= MotifFunctions.Resize | MotifFunctions.Move;
					motifDecorations |= MotifDecorations.Border | MotifDecorations.ResizeH;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_GROUP))
				{
					motifFunctions |= MotifFunctions.Minimize;
					motifDecorations |= MotifDecorations.Minimize;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_TABSTOP))
				{
					motifFunctions |= MotifFunctions.Maximize;
					motifDecorations |= MotifDecorations.Maximize;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_THICKFRAME))
				{
					motifFunctions |= MotifFunctions.Resize;
					motifDecorations |= MotifDecorations.ResizeH;
				}
				if (this.ExStyleSet(cp.ExStyle, WindowExStyles.WS_EX_DLGMODALFRAME))
				{
					motifDecorations |= MotifDecorations.Border;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_BORDER))
				{
					motifDecorations |= MotifDecorations.Border;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_DLGFRAME))
				{
					motifDecorations |= MotifDecorations.Border;
				}
				if (this.StyleSet(cp.Style, WindowStyles.WS_SYSMENU))
				{
					motifFunctions |= MotifFunctions.Close;
				}
				else
				{
					motifFunctions &= ~(MotifFunctions.Minimize | MotifFunctions.Maximize | MotifFunctions.Close);
					motifDecorations &= ~(MotifDecorations.Menu | MotifDecorations.Minimize | MotifDecorations.Maximize);
					if (cp.Caption == "")
					{
						motifFunctions &= ~MotifFunctions.Move;
						motifDecorations &= ~(MotifDecorations.ResizeH | MotifDecorations.Title);
					}
				}
			}
			if ((motifFunctions & MotifFunctions.Resize) == (MotifFunctions)0)
			{
				hwnd.fixed_size = true;
				Rectangle rectangle = new Rectangle(cp.X, cp.Y, cp.Width, cp.Height);
				this.SetWindowMinMax(hwnd.Handle, rectangle, rectangle.Size, rectangle.Size, cp);
			}
			else
			{
				hwnd.fixed_size = false;
			}
			motifWmHints.functions = (IntPtr)((int)motifFunctions);
			motifWmHints.decorations = (IntPtr)((int)motifDecorations);
			if (cp.IsSet(WindowExStyles.WS_EX_TOOLWINDOW))
			{
				intPtr = XplatUIX11._NET_WM_WINDOW_TYPE_UTILITY;
			}
			else
			{
				intPtr = XplatUIX11._NET_WM_WINDOW_TYPE_NORMAL;
			}
			bool flag = !cp.IsSet(WindowExStyles.WS_EX_APPWINDOW) || (cp.IsSet(WindowExStyles.WS_EX_TOOLWINDOW) && form != null && form.Parent != null && !form.ShowInTaskbar);
			if (this.ExStyleSet(cp.ExStyle, WindowExStyles.WS_EX_TOOLWINDOW) && form != null && !hwnd.reparented && form.Owner != null && form.Owner.Handle != IntPtr.Zero)
			{
				Hwnd hwnd2 = Hwnd.ObjectFromHandle(form.Owner.Handle);
				if (hwnd2 != null)
				{
					intPtr2 = hwnd2.whole_window;
				}
			}
			if (this.StyleSet(cp.Style, WindowStyles.WS_POPUP) && hwnd.parent != null && hwnd.parent.whole_window != IntPtr.Zero)
			{
				intPtr2 = hwnd.parent.whole_window;
			}
			FormWindowState formWindowState = this.GetWindowState(hwnd.Handle);
			if (formWindowState == (FormWindowState)(-1))
			{
				formWindowState = FormWindowState.Normal;
			}
			Rectangle rectangle2 = XplatUIX11.TranslateClientRectangleToXClientRectangle(hwnd);
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				int num = 0;
				array[0] = intPtr.ToInt32();
				XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_WINDOW_TYPE, (IntPtr)4, 32, PropertyMode.Replace, array, 1);
				XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._MOTIF_WM_HINTS, XplatUIX11._MOTIF_WM_HINTS, 32, PropertyMode.Replace, ref motifWmHints, 5);
				if (intPtr2 != IntPtr.Zero)
				{
					XplatUIX11.XSetTransientForHint(XplatUIX11.DisplayHandle, hwnd.whole_window, intPtr2);
				}
				XplatUIX11.MoveResizeWindow(XplatUIX11.DisplayHandle, hwnd.client_window, rectangle2.X, rectangle2.Y, rectangle2.Width, rectangle2.Height);
				if (flag)
				{
					array[num++] = XplatUIX11._NET_WM_STATE_SKIP_TASKBAR.ToInt32();
				}
				if (formWindowState == FormWindowState.Maximized)
				{
					array[num++] = XplatUIX11._NET_WM_STATE_MAXIMIZED_HORZ.ToInt32();
					array[num++] = XplatUIX11._NET_WM_STATE_MAXIMIZED_VERT.ToInt32();
				}
				if (form != null && form.Modal)
				{
					array[num++] = XplatUIX11._NET_WM_STATE_MODAL.ToInt32();
				}
				XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_STATE, (IntPtr)4, 32, PropertyMode.Replace, array, num);
				num = 0;
				IntPtr[] array2 = new IntPtr[2];
				array2[num++] = XplatUIX11.WM_DELETE_WINDOW;
				if (this.ExStyleSet(cp.ExStyle, WindowExStyles.WS_EX_CONTEXTHELP))
				{
					array2[num++] = XplatUIX11._NET_WM_CONTEXT_HELP;
				}
				XplatUIX11.XSetWMProtocols(XplatUIX11.DisplayHandle, hwnd.whole_window, array2, num);
			}
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x0007CE90 File Offset: 0x0007B090
		private void SetIcon(Hwnd hwnd, Icon icon)
		{
			if (icon == null)
			{
				XplatUIX11.XDeleteProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_ICON);
				return;
			}
			Bitmap bitmap = icon.ToBitmap();
			int num = 0;
			int num2 = bitmap.Width * bitmap.Height + 2;
			IntPtr[] array = new IntPtr[num2];
			array[num++] = (IntPtr)bitmap.Width;
			array[num++] = (IntPtr)bitmap.Height;
			for (int i = 0; i < bitmap.Height; i++)
			{
				for (int j = 0; j < bitmap.Width; j++)
				{
					array[num++] = (IntPtr)bitmap.GetPixel(j, i).ToArgb();
				}
			}
			XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_ICON, (IntPtr)6, 32, PropertyMode.Replace, array, num2);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0007CF68 File Offset: 0x0007B168
		private void WakeupMain()
		{
			try
			{
				XplatUIX11.wake.Send(new byte[] { byte.MaxValue });
			}
			catch (SocketException ex)
			{
				if (ex.SocketErrorCode != SocketError.WouldBlock)
				{
					throw;
				}
			}
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x0007CFB0 File Offset: 0x0007B1B0
		private XEventQueue ThreadQueue(Thread thread)
		{
			XEventQueue xeventQueue = (XEventQueue)XplatUIX11.MessageQueues[thread];
			if (xeventQueue == null)
			{
				xeventQueue = new XEventQueue(thread);
				XplatUIX11.MessageQueues[thread] = xeventQueue;
			}
			return xeventQueue;
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0007CFE8 File Offset: 0x0007B1E8
		private void TranslatePropertyToClipboard(IntPtr property)
		{
			IntPtr zero = IntPtr.Zero;
			XplatUIX11.Clipboard.Item = null;
			IntPtr intPtr;
			int num;
			IntPtr intPtr2;
			IntPtr intPtr3;
			XplatUIX11.XGetWindowProperty(XplatUIX11.DisplayHandle, XplatUIX11.FosterParent, property, IntPtr.Zero, new IntPtr(int.MaxValue), true, (IntPtr)0, out intPtr, out num, out intPtr2, out intPtr3, ref zero);
			if ((long)intPtr2 > 0L)
			{
				if (property == (IntPtr)31)
				{
					string text = Marshal.PtrToStringAnsi(zero);
					if (string.IsNullOrEmpty(text))
					{
						StringBuilder stringBuilder = new StringBuilder();
						for (int i = 0; i < (int)intPtr2; i++)
						{
							byte b = Marshal.ReadByte(zero, i);
							stringBuilder.Append((char)b);
						}
						text = stringBuilder.ToString();
					}
					XplatUIX11.Clipboard.Item = this.UnescapeUnicodeFromAnsi(text);
				}
				else if (!(property == (IntPtr)5) && !(property == (IntPtr)20))
				{
					if (property == XplatUIX11.OEMTEXT)
					{
						XplatUIX11.Clipboard.Item = this.UnescapeUnicodeFromAnsi(Marshal.PtrToStringAnsi(zero));
					}
					else if (property == XplatUIX11.UTF8_STRING)
					{
						byte[] array = new byte[(int)intPtr2];
						for (int j = 0; j < (int)intPtr2; j++)
						{
							array[j] = Marshal.ReadByte(zero, j);
						}
						XplatUIX11.Clipboard.Item = Encoding.UTF8.GetString(array);
					}
					else if (property == XplatUIX11.UTF16_STRING)
					{
						byte[] array2 = new byte[(int)intPtr2];
						for (int k = 0; k < (int)intPtr2; k++)
						{
							array2[k] = Marshal.ReadByte(zero, k);
						}
						XplatUIX11.Clipboard.Item = Encoding.Unicode.GetString(array2);
					}
					else if (property == XplatUIX11.RICHTEXTFORMAT)
					{
						XplatUIX11.Clipboard.Item = Marshal.PtrToStringAnsi(zero);
					}
					else if (DataFormats.ContainsFormat(property.ToInt32()) && DataFormats.GetFormat(property.ToInt32()).is_serializable)
					{
						MemoryStream memoryStream = new MemoryStream((int)intPtr2);
						for (int l = 0; l < (int)intPtr2; l++)
						{
							memoryStream.WriteByte(Marshal.ReadByte(zero, l));
						}
						memoryStream.Position = 0L;
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						XplatUIX11.Clipboard.Item = binaryFormatter.Deserialize(memoryStream);
						memoryStream.Close();
					}
				}
				XplatUIX11.XFree(zero);
			}
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x0007D258 File Offset: 0x0007B458
		private string UnescapeUnicodeFromAnsi(string value)
		{
			if (value == null || value.IndexOf("\\u") == -1)
			{
				return value;
			}
			StringBuilder stringBuilder = new StringBuilder(value.Length);
			int i;
			int num;
			for (i = 0; i < value.Length; i = num)
			{
				num = value.IndexOf("\\u", i);
				if (num == -1)
				{
					break;
				}
				stringBuilder.Append(value, i, num - i);
				num += 2;
				i = num;
				int num2 = 0;
				while (num < value.Length && num2 < 4 && XplatUIX11.ValidHexDigit(value[num]))
				{
					num2++;
					num++;
				}
				int num3;
				if (!int.TryParse(value.Substring(i, num2), NumberStyles.HexNumber, null, out num3))
				{
					return value;
				}
				stringBuilder.Append((char)num3);
			}
			if (i < value.Length)
			{
				stringBuilder.Append(value, i, value.Length - i);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0007D322 File Offset: 0x0007B522
		private static bool ValidHexDigit(char e)
		{
			return char.IsDigit(e) || (e >= 'A' && e <= 'F') || (e >= 'a' && e <= 'f');
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0007D348 File Offset: 0x0007B548
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
				if (!hwnd.expose_pending)
				{
					if (!hwnd.nc_expose_pending)
					{
						hwnd.Queue.Paint.Enqueue(hwnd);
					}
					hwnd.expose_pending = true;
					return;
				}
			}
			else
			{
				hwnd.AddNcInvalidArea(x, y, width, height);
				if (!hwnd.nc_expose_pending)
				{
					if (!hwnd.expose_pending)
					{
						hwnd.Queue.Paint.Enqueue(hwnd);
					}
					hwnd.nc_expose_pending = true;
				}
			}
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x0007D418 File Offset: 0x0007B618
		private static Hwnd.Borders FrameExtents(IntPtr window)
		{
			IntPtr zero = IntPtr.Zero;
			Hwnd.Borders borders = default(Hwnd.Borders);
			IntPtr intPtr;
			int num;
			IntPtr intPtr2;
			IntPtr intPtr3;
			XplatUIX11.XGetWindowProperty(XplatUIX11.DisplayHandle, window, XplatUIX11._NET_FRAME_EXTENTS, IntPtr.Zero, new IntPtr(16), false, (IntPtr)6, out intPtr, out num, out intPtr2, out intPtr3, ref zero);
			if (zero != IntPtr.Zero)
			{
				if (intPtr2.ToInt32() == 4)
				{
					borders.left = Marshal.ReadInt32(zero, 0);
					borders.right = Marshal.ReadInt32(zero, IntPtr.Size);
					borders.top = Marshal.ReadInt32(zero, 2 * IntPtr.Size);
					borders.bottom = Marshal.ReadInt32(zero, 3 * IntPtr.Size);
				}
				XplatUIX11.XFree(zero);
			}
			return borders;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x0007D4D0 File Offset: 0x0007B6D0
		private void AddConfigureNotify(XEvent xevent)
		{
			Hwnd objectFromWindow = Hwnd.GetObjectFromWindow(xevent.ConfigureEvent.window);
			if (objectFromWindow == null || objectFromWindow.zombie)
			{
				return;
			}
			if (xevent.ConfigureEvent.window == objectFromWindow.whole_window)
			{
				if (objectFromWindow.parent == null)
				{
					Point topLevelWindowLocation = XplatUIX11.GetTopLevelWindowLocation(objectFromWindow);
					objectFromWindow.x = topLevelWindowLocation.X;
					objectFromWindow.y = topLevelWindowLocation.Y;
				}
				Control control = Control.FromHandle(objectFromWindow.Handle);
				Size size;
				if (control != null)
				{
					size = XplatUIX11.TranslateXWindowSizeToWindowSize(control.GetCreateParams(), xevent.ConfigureEvent.width, xevent.ConfigureEvent.height);
				}
				else
				{
					size = new Size(xevent.ConfigureEvent.width, xevent.ConfigureEvent.height);
				}
				objectFromWindow.width = size.Width;
				objectFromWindow.height = size.Height;
				objectFromWindow.ClientRect = Rectangle.Empty;
				object configure_lock = objectFromWindow.configure_lock;
				lock (configure_lock)
				{
					if (!objectFromWindow.configure_pending)
					{
						objectFromWindow.Queue.EnqueueLocked(xevent);
						objectFromWindow.configure_pending = true;
					}
				}
			}
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x0007D600 File Offset: 0x0007B800
		private void ShowCaret()
		{
			if (XplatUIX11.Caret.gc == IntPtr.Zero || XplatUIX11.Caret.On)
			{
				return;
			}
			XplatUIX11.Caret.On = true;
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				XplatUIX11.XDrawLine(XplatUIX11.DisplayHandle, XplatUIX11.Caret.Window, XplatUIX11.Caret.gc, XplatUIX11.Caret.X, XplatUIX11.Caret.Y, XplatUIX11.Caret.X, XplatUIX11.Caret.Y + XplatUIX11.Caret.Height);
			}
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x0007D6BC File Offset: 0x0007B8BC
		private void HideCaret()
		{
			if (XplatUIX11.Caret.gc == IntPtr.Zero || !XplatUIX11.Caret.On)
			{
				return;
			}
			XplatUIX11.Caret.On = false;
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				XplatUIX11.XDrawLine(XplatUIX11.DisplayHandle, XplatUIX11.Caret.Window, XplatUIX11.Caret.gc, XplatUIX11.Caret.X, XplatUIX11.Caret.Y, XplatUIX11.Caret.X, XplatUIX11.Caret.Y + XplatUIX11.Caret.Height);
			}
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0007D778 File Offset: 0x0007B978
		private int NextTimeout(ArrayList timers, DateTime now)
		{
			int num = 0;
			foreach (object obj in timers)
			{
				int num2 = (int)(((Timer)obj).Expires - now).TotalMilliseconds;
				if (num2 < 0)
				{
					return 0;
				}
				if (num2 < num)
				{
					num = num2;
				}
			}
			if (num < Timer.Minimum)
			{
				num = Timer.Minimum;
			}
			if (num > 1000)
			{
				num = 1000;
			}
			return num;
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x0007D810 File Offset: 0x0007BA10
		private void CheckTimers(ArrayList timers, DateTime now)
		{
			if (timers.Count == 0)
			{
				return;
			}
			for (int i = 0; i < timers.Count; i++)
			{
				Timer timer = (Timer)timers[i];
				if (timer.Enabled && timer.Expires <= now && !timer.Busy && (XplatUIX11.in_doevents || (Application.MWFThread.Current.Context != null && (Application.MWFThread.Current.Context.MainForm == null || Application.MWFThread.Current.Context.MainForm.IsLoaded))))
				{
					timer.Busy = true;
					timer.Update(now);
					timer.FireTick();
					timer.Busy = false;
				}
			}
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x0007D8BD File Offset: 0x0007BABD
		private void WaitForHwndMessage(Hwnd hwnd, Msg message)
		{
			this.WaitForHwndMessage(hwnd, message, false);
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x0007D8C8 File Offset: 0x0007BAC8
		private void WaitForHwndMessage(Hwnd hwnd, Msg message, bool process)
		{
			MSG msg = default(MSG);
			XEventQueue xeventQueue = this.ThreadQueue(Thread.CurrentThread);
			xeventQueue.DispatchIdle = false;
			bool flag = false;
			string text = hwnd.Handle.ToString() + ":" + message;
			if (!XplatUIX11.messageHold.ContainsKey(text))
			{
				XplatUIX11.messageHold.Add(text, 1);
			}
			else
			{
				XplatUIX11.messageHold[text] = (int)XplatUIX11.messageHold[text] + 1;
			}
			for (;;)
			{
				if (this.PeekMessage(xeventQueue, ref msg, IntPtr.Zero, 0, 0, 1U))
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
				flag = !XplatUIX11.messageHold.ContainsKey(text) || (int)XplatUIX11.messageHold[text] < 1 || flag;
				if (flag)
				{
					goto IL_0123;
				}
			}
			if (process)
			{
				this.TranslateMessage(ref msg);
				this.DispatchMessage(ref msg);
			}
			IL_0123:
			XplatUIX11.messageHold.Remove(text);
			xeventQueue.DispatchIdle = true;
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0007DA0C File Offset: 0x0007BC0C
		private void MapWindow(Hwnd hwnd, WindowType windows)
		{
			if (!hwnd.mapped)
			{
				Form form = Control.FromHandle(hwnd.Handle) as Form;
				if (form != null && form.WindowState == FormWindowState.Normal)
				{
					form.waiting_showwindow = true;
					this.SendMessage(hwnd.Handle, Msg.WM_SHOWWINDOW, (IntPtr)1, IntPtr.Zero);
				}
				if (hwnd.zombie)
				{
					return;
				}
				if (hwnd.topmost)
				{
					if ((windows & WindowType.Whole) != (WindowType)0)
					{
						XplatUIX11.XMapRaised(XplatUIX11.DisplayHandle, hwnd.whole_window);
					}
					if ((windows & WindowType.Client) != (WindowType)0)
					{
						XplatUIX11.XMapRaised(XplatUIX11.DisplayHandle, hwnd.client_window);
					}
				}
				else
				{
					if ((windows & WindowType.Whole) != (WindowType)0)
					{
						XplatUIX11.XMapWindow(XplatUIX11.DisplayHandle, hwnd.whole_window);
					}
					if ((windows & WindowType.Client) != (WindowType)0)
					{
						XplatUIX11.XMapWindow(XplatUIX11.DisplayHandle, hwnd.client_window);
					}
				}
				hwnd.mapped = true;
				if (form != null && form.waiting_showwindow)
				{
					this.WaitForHwndMessage(hwnd, Msg.WM_SHOWWINDOW);
					CreateParams createParams = form.GetCreateParams();
					if (!this.ExStyleSet(createParams.ExStyle, WindowExStyles.WS_EX_MDICHILD) && !this.StyleSet(createParams.Style, WindowStyles.WS_CHILD))
					{
						this.WaitForHwndMessage(hwnd, Msg.WM_ACTIVATE, true);
					}
				}
			}
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x0007DB1C File Offset: 0x0007BD1C
		private void UnmapWindow(Hwnd hwnd, WindowType windows)
		{
			if (hwnd.mapped)
			{
				Form form = null;
				if (Control.FromHandle(hwnd.Handle) is Form)
				{
					form = Control.FromHandle(hwnd.Handle) as Form;
					if (form.WindowState == FormWindowState.Normal)
					{
						form.waiting_showwindow = true;
						this.SendMessage(hwnd.Handle, Msg.WM_SHOWWINDOW, IntPtr.Zero, IntPtr.Zero);
					}
				}
				if (hwnd.zombie)
				{
					return;
				}
				if ((windows & WindowType.Client) != (WindowType)0)
				{
					XplatUIX11.XUnmapWindow(XplatUIX11.DisplayHandle, hwnd.client_window);
				}
				if ((windows & WindowType.Whole) != (WindowType)0)
				{
					XplatUIX11.XUnmapWindow(XplatUIX11.DisplayHandle, hwnd.whole_window);
				}
				hwnd.mapped = false;
				if (form != null && form.waiting_showwindow)
				{
					this.WaitForHwndMessage(hwnd, Msg.WM_SHOWWINDOW);
					CreateParams createParams = form.GetCreateParams();
					if (!this.ExStyleSet(createParams.ExStyle, WindowExStyles.WS_EX_MDICHILD) && !this.StyleSet(createParams.Style, WindowStyles.WS_CHILD))
					{
						this.WaitForHwndMessage(hwnd, Msg.WM_ACTIVATE, true);
					}
				}
			}
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x0007DC04 File Offset: 0x0007BE04
		private void UpdateMessageQueue(XEventQueue queue)
		{
			this.UpdateMessageQueue(queue, true);
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x0007DC10 File Offset: 0x0007BE10
		private void UpdateMessageQueue(XEventQueue queue, bool allowIdle)
		{
			DateTime utcNow = DateTime.UtcNow;
			object obj = XplatUIX11.XlibLock;
			int num;
			lock (obj)
			{
				num = XplatUIX11.XPending(XplatUIX11.DisplayHandle);
			}
			if (num == 0 && allowIdle)
			{
				if ((queue == null || queue.DispatchIdle) && this.Idle != null)
				{
					this.Idle(this, EventArgs.Empty);
				}
				obj = XplatUIX11.XlibLock;
				lock (obj)
				{
					num = XplatUIX11.XPending(XplatUIX11.DisplayHandle);
				}
			}
			if (num == 0)
			{
				int num2 = 0;
				if (queue != null)
				{
					if (queue.Paint.Count > 0)
					{
						return;
					}
					num2 = this.NextTimeout(queue.timer_list, utcNow);
				}
				if (num2 > 0)
				{
					int num3 = XplatUIX11.pollfds.Length - 1;
					obj = XplatUIX11.wake_waiting_lock;
					lock (obj)
					{
						if (!XplatUIX11.wake_waiting)
						{
							num3++;
							XplatUIX11.wake_waiting = true;
						}
					}
					Syscall.poll(XplatUIX11.pollfds, (uint)num3, num2);
					if (num3 == XplatUIX11.pollfds.Length)
					{
						if (XplatUIX11.pollfds[1].revents != (PollEvents)0)
						{
							XplatUIX11.wake_receive.Receive(XplatUIX11.network_buffer, 0, 1, SocketFlags.None);
						}
						obj = XplatUIX11.wake_waiting_lock;
						lock (obj)
						{
							XplatUIX11.wake_waiting = false;
						}
					}
					obj = XplatUIX11.XlibLock;
					lock (obj)
					{
						num = XplatUIX11.XPending(XplatUIX11.DisplayHandle);
					}
				}
			}
			if (queue != null)
			{
				this.CheckTimers(queue.timer_list, utcNow);
			}
			XEvent xevent;
			Hwnd objectFromWindow;
			for (;;)
			{
				xevent = default(XEvent);
				obj = XplatUIX11.XlibLock;
				lock (obj)
				{
					if (XplatUIX11.XPending(XplatUIX11.DisplayHandle) == 0)
					{
						return;
					}
					XplatUIX11.XNextEvent(XplatUIX11.DisplayHandle, ref xevent);
					if (xevent.AnyEvent.type == XEventName.KeyPress || xevent.AnyEvent.type == XEventName.KeyRelease)
					{
						XplatUIX11.Keyboard.PreFilter(xevent);
						if (XplatUIX11.XFilterEvent(ref xevent, XplatUIX11.Keyboard.ClientWindow))
						{
							continue;
						}
					}
					else if (XplatUIX11.XFilterEvent(ref xevent, IntPtr.Zero))
					{
						continue;
					}
				}
				objectFromWindow = Hwnd.GetObjectFromWindow(xevent.AnyEvent.window);
				if (objectFromWindow != null)
				{
					switch (xevent.type)
					{
					case XEventName.KeyPress:
						goto IL_0A17;
					case XEventName.KeyRelease:
					{
						if (XplatUIX11.detectable_key_auto_repeat || XplatUIX11.XPending(XplatUIX11.DisplayHandle) == 0)
						{
							goto IL_0A17;
						}
						XEvent xevent2 = default(XEvent);
						XplatUIX11.XPeekEvent(XplatUIX11.DisplayHandle, ref xevent2);
						if (xevent2.type != XEventName.KeyPress || xevent2.KeyEvent.keycode != xevent.KeyEvent.keycode || !(xevent2.KeyEvent.time == xevent.KeyEvent.time))
						{
							goto IL_0A17;
						}
						break;
					}
					case XEventName.ButtonPress:
					case XEventName.ButtonRelease:
					case XEventName.EnterNotify:
					case XEventName.LeaveNotify:
					case XEventName.FocusIn:
					case XEventName.FocusOut:
					case XEventName.CreateNotify:
					case XEventName.DestroyNotify:
					case XEventName.UnmapNotify:
					case XEventName.MapNotify:
					case XEventName.ReparentNotify:
					case XEventName.ClientMessage:
						objectFromWindow.Queue.EnqueueLocked(xevent);
						break;
					case XEventName.MotionNotify:
						if (Thread.CurrentThread != objectFromWindow.Queue.Thread || objectFromWindow.Queue.Count <= 0 || objectFromWindow.Queue.Peek().AnyEvent.type != XEventName.MotionNotify)
						{
							goto IL_0A17;
						}
						break;
					case XEventName.Expose:
						this.AddExpose(objectFromWindow, xevent.ExposeEvent.window == objectFromWindow.ClientWindow, xevent.ExposeEvent.x, xevent.ExposeEvent.y, xevent.ExposeEvent.width, xevent.ExposeEvent.height);
						break;
					case XEventName.ConfigureNotify:
						this.AddConfigureNotify(xevent);
						break;
					case XEventName.PropertyNotify:
						if (xevent.PropertyEvent.atom == XplatUIX11._NET_ACTIVE_WINDOW)
						{
							IntPtr zero = IntPtr.Zero;
							IntPtr activeWindow = XplatUIX11.ActiveWindow;
							IntPtr intPtr;
							int num4;
							IntPtr intPtr2;
							IntPtr intPtr3;
							XplatUIX11.XGetWindowProperty(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, XplatUIX11._NET_ACTIVE_WINDOW, IntPtr.Zero, new IntPtr(1), false, (IntPtr)33, out intPtr, out num4, out intPtr2, out intPtr3, ref zero);
							if ((long)intPtr2 > 0L && zero != IntPtr.Zero)
							{
								XplatUIX11.ActiveWindow = Hwnd.GetHandleFromWindow((IntPtr)Marshal.ReadInt32(zero));
								XplatUIX11.XFree(zero);
								if (activeWindow != XplatUIX11.ActiveWindow)
								{
									if (activeWindow != IntPtr.Zero)
									{
										this.PostMessage(activeWindow, Msg.WM_ACTIVATE, (IntPtr)0, IntPtr.Zero);
									}
									if (XplatUIX11.ActiveWindow != IntPtr.Zero)
									{
										this.PostMessage(XplatUIX11.ActiveWindow, Msg.WM_ACTIVATE, (IntPtr)1, IntPtr.Zero);
									}
								}
								if (XplatUIX11.ModalWindows.Count != 0)
								{
									Form form = Control.FromHandle(XplatUIX11.ActiveWindow) as Form;
									if (form != null)
									{
										Form form2 = Control.FromHandle((IntPtr)XplatUIX11.ModalWindows.Peek()) as Form;
										if (XplatUIX11.ActiveWindow != (IntPtr)XplatUIX11.ModalWindows.Peek() && (form2 == null || form.context == form2.context))
										{
											this.Activate((IntPtr)XplatUIX11.ModalWindows.Peek());
										}
									}
								}
							}
						}
						else if (xevent.PropertyEvent.atom == XplatUIX11._NET_WM_STATE)
						{
							objectFromWindow.cached_window_state = (FormWindowState)(-1);
							this.PostMessage(objectFromWindow.Handle, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
						}
						break;
					case XEventName.SelectionRequest:
						if (!XplatUIX11.Dnd.HandleSelectionRequestEvent(ref xevent))
						{
							XEvent xevent3 = default(XEvent);
							xevent3.SelectionEvent.type = XEventName.SelectionNotify;
							xevent3.SelectionEvent.send_event = true;
							xevent3.SelectionEvent.display = XplatUIX11.DisplayHandle;
							xevent3.SelectionEvent.selection = xevent.SelectionRequestEvent.selection;
							xevent3.SelectionEvent.target = xevent.SelectionRequestEvent.target;
							xevent3.SelectionEvent.requestor = xevent.SelectionRequestEvent.requestor;
							xevent3.SelectionEvent.time = xevent.SelectionRequestEvent.time;
							xevent3.SelectionEvent.property = IntPtr.Zero;
							IntPtr target = xevent.SelectionRequestEvent.target;
							if (target == XplatUIX11.TARGETS)
							{
								int[] array = new int[5];
								int num5 = 0;
								if (XplatUIX11.Clipboard.IsSourceText)
								{
									array[num5++] = 31;
									array[num5++] = (int)XplatUIX11.OEMTEXT;
									array[num5++] = (int)XplatUIX11.UTF8_STRING;
									array[num5++] = (int)XplatUIX11.UTF16_STRING;
									array[num5++] = (int)XplatUIX11.RICHTEXTFORMAT;
								}
								else if (XplatUIX11.Clipboard.IsSourceImage)
								{
									array[num5++] = 20;
									array[num5++] = 5;
								}
								XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, xevent.SelectionRequestEvent.requestor, xevent.SelectionRequestEvent.property, xevent.SelectionRequestEvent.target, 32, PropertyMode.Replace, array, num5);
								xevent3.SelectionEvent.property = xevent.SelectionRequestEvent.property;
							}
							else if (target == XplatUIX11.RICHTEXTFORMAT)
							{
								string rtfText = XplatUIX11.Clipboard.GetRtfText();
								if (rtfText != null)
								{
									byte[] bytes = Encoding.ASCII.GetBytes(rtfText);
									int num6 = bytes.Length;
									IntPtr intPtr4 = Marshal.AllocHGlobal(num6);
									for (int i = 0; i < num6; i++)
									{
										Marshal.WriteByte(intPtr4, i, bytes[i]);
									}
									XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, xevent.SelectionRequestEvent.requestor, xevent.SelectionRequestEvent.property, xevent.SelectionRequestEvent.target, 8, PropertyMode.Replace, intPtr4, num6);
									xevent3.SelectionEvent.property = xevent.SelectionRequestEvent.property;
									Marshal.FreeHGlobal(intPtr4);
								}
							}
							else if (XplatUIX11.Clipboard.IsSourceText && (target == (IntPtr)31 || target == XplatUIX11.OEMTEXT || target == XplatUIX11.UTF16_STRING || target == XplatUIX11.UTF8_STRING))
							{
								IntPtr intPtr5 = IntPtr.Zero;
								Encoding encoding = null;
								IntPtr target2 = xevent.SelectionRequestEvent.target;
								if (target2 == (IntPtr)31 || target2 == XplatUIX11.OEMTEXT)
								{
									encoding = Encoding.ASCII;
								}
								else if (target2 == XplatUIX11.UTF16_STRING)
								{
									encoding = Encoding.Unicode;
								}
								else if (target2 == XplatUIX11.UTF8_STRING)
								{
									encoding = Encoding.UTF8;
								}
								byte[] bytes2 = encoding.GetBytes(XplatUIX11.Clipboard.GetPlainText());
								intPtr5 = Marshal.AllocHGlobal(bytes2.Length);
								int num7 = bytes2.Length;
								for (int j = 0; j < num7; j++)
								{
									Marshal.WriteByte(intPtr5, j, bytes2[j]);
								}
								if (intPtr5 != IntPtr.Zero)
								{
									XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, xevent.SelectionRequestEvent.requestor, xevent.SelectionRequestEvent.property, xevent.SelectionRequestEvent.target, 8, PropertyMode.Replace, intPtr5, num7);
									xevent3.SelectionEvent.property = xevent.SelectionRequestEvent.property;
									Marshal.FreeHGlobal(intPtr5);
								}
							}
							else if (XplatUIX11.Clipboard.GetSource(target.ToInt32()) != null)
							{
								if (DataFormats.GetFormat(target.ToInt32()).is_serializable)
								{
									object source = XplatUIX11.Clipboard.GetSource(target.ToInt32());
									BinaryFormatter binaryFormatter = new BinaryFormatter();
									MemoryStream memoryStream = new MemoryStream();
									binaryFormatter.Serialize(memoryStream, source);
									int num8 = (int)memoryStream.Length;
									IntPtr intPtr6 = Marshal.AllocHGlobal(num8);
									memoryStream.Position = 0L;
									for (int k = 0; k < num8; k++)
									{
										Marshal.WriteByte(intPtr6, k, (byte)memoryStream.ReadByte());
									}
									memoryStream.Close();
									XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, xevent.SelectionRequestEvent.requestor, xevent.SelectionRequestEvent.property, xevent.SelectionRequestEvent.target, 8, PropertyMode.Replace, intPtr6, num8);
									xevent3.SelectionEvent.property = xevent.SelectionRequestEvent.property;
									Marshal.FreeHGlobal(intPtr6);
								}
							}
							else if (XplatUIX11.Clipboard.IsSourceImage && !(xevent.SelectionEvent.target == (IntPtr)20))
							{
								xevent.SelectionEvent.target == (IntPtr)20;
							}
							XplatUIX11.XSendEvent(XplatUIX11.DisplayHandle, xevent.SelectionRequestEvent.requestor, false, new IntPtr(0), ref xevent3);
						}
						break;
					case XEventName.SelectionNotify:
						if (XplatUIX11.Clipboard.Enumerating)
						{
							XplatUIX11.Clipboard.Enumerating = false;
							if (xevent.SelectionEvent.property != IntPtr.Zero)
							{
								XplatUIX11.XDeleteProperty(XplatUIX11.DisplayHandle, XplatUIX11.FosterParent, xevent.SelectionEvent.property);
								if (!XplatUIX11.Clipboard.Formats.Contains(xevent.SelectionEvent.property))
								{
									XplatUIX11.Clipboard.Formats.Add(xevent.SelectionEvent.property);
								}
							}
						}
						else if (XplatUIX11.Clipboard.Retrieving)
						{
							XplatUIX11.Clipboard.Retrieving = false;
							if (xevent.SelectionEvent.property != IntPtr.Zero)
							{
								this.TranslatePropertyToClipboard(xevent.SelectionEvent.property);
							}
							else
							{
								XplatUIX11.Clipboard.ClearSources();
								XplatUIX11.Clipboard.Item = null;
							}
						}
						else
						{
							XplatUIX11.Dnd.HandleSelectionNotifyEvent(ref xevent);
						}
						break;
					}
				}
			}
			IL_0A17:
			objectFromWindow.Queue.EnqueueLocked(xevent);
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0007E870 File Offset: 0x0007CA70
		private IntPtr GetMousewParam(int Delta)
		{
			int num = 0;
			if ((XplatUIX11.MouseState & MouseButtons.Left) != MouseButtons.None)
			{
				num |= 1;
			}
			if ((XplatUIX11.MouseState & MouseButtons.Middle) != MouseButtons.None)
			{
				num |= 16;
			}
			if ((XplatUIX11.MouseState & MouseButtons.Right) != MouseButtons.None)
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

		// Token: 0x06001A2E RID: 6702 RVA: 0x0007E8E0 File Offset: 0x0007CAE0
		private IntPtr XGetParent(IntPtr handle)
		{
			object obj = XplatUIX11.XlibLock;
			IntPtr intPtr2;
			IntPtr intPtr3;
			lock (obj)
			{
				IntPtr intPtr;
				int num;
				XplatUIX11.XQueryTree(XplatUIX11.DisplayHandle, handle, out intPtr, out intPtr2, out intPtr3, out num);
			}
			if (intPtr3 != IntPtr.Zero)
			{
				obj = XplatUIX11.XlibLock;
				lock (obj)
				{
					XplatUIX11.XFree(intPtr3);
				}
			}
			return intPtr2;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0007E974 File Offset: 0x0007CB74
		private int HandleError(IntPtr display, ref XErrorEvent error_event)
		{
			if (error_event.request_code == (XRequest)this.render_major_opcode && error_event.minor_code == 7 && (int)error_event.error_code == this.render_first_error + 1)
			{
				return 0;
			}
			if (XplatUIX11.ErrorExceptions)
			{
				XplatUIX11.XUngrabPointer(display, IntPtr.Zero);
				throw new XplatUIX11.XException(error_event.display, error_event.resourceid, error_event.serial, error_event.error_code, error_event.request_code, error_event.minor_code);
			}
			Console.WriteLine("X11 Error encountered: {0}{1}\n", XplatUIX11.XException.GetMessage(error_event.display, error_event.resourceid, error_event.serial, error_event.error_code, error_event.request_code, error_event.minor_code), Environment.StackTrace);
			return 0;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x0007EA24 File Offset: 0x0007CC24
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

		// Token: 0x06001A31 RID: 6705 RVA: 0x0007EA84 File Offset: 0x0007CC84
		private void CleanupCachedWindows(Hwnd hwnd)
		{
			if (XplatUIX11.ActiveWindow == hwnd.Handle)
			{
				this.SendMessage(hwnd.client_window, Msg.WM_ACTIVATE, (IntPtr)0, IntPtr.Zero);
				XplatUIX11.ActiveWindow = IntPtr.Zero;
			}
			if (XplatUIX11.FocusWindow == hwnd.Handle)
			{
				this.SendMessage(hwnd.client_window, Msg.WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
				XplatUIX11.FocusWindow = IntPtr.Zero;
			}
			if (XplatUIX11.Grab.Hwnd == hwnd.Handle)
			{
				XplatUIX11.Grab.Hwnd = IntPtr.Zero;
				XplatUIX11.Grab.Confined = false;
			}
			this.DestroyCaret(hwnd.Handle);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0007EB38 File Offset: 0x0007CD38
		private void PerformNCCalc(Hwnd hwnd)
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
			rectangle = XplatUIX11.TranslateClientRectangleToXClientRectangle(hwnd);
			if (hwnd.visible)
			{
				XplatUIX11.MoveResizeWindow(XplatUIX11.DisplayHandle, hwnd.client_window, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
			}
			this.AddExpose(hwnd, hwnd.WholeWindow == hwnd.ClientWindow, 0, 0, hwnd.Width, hwnd.Height);
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0007ECAC File Offset: 0x0007CEAC
		private void MouseHover(object sender, EventArgs e)
		{
			XplatUIX11.HoverState.Timer.Enabled = false;
			if (XplatUIX11.HoverState.Window != IntPtr.Zero)
			{
				Hwnd objectFromWindow = Hwnd.GetObjectFromWindow(XplatUIX11.HoverState.Window);
				if (objectFromWindow != null)
				{
					XEvent xevent = default(XEvent);
					xevent.type = XEventName.ClientMessage;
					xevent.ClientMessageEvent.display = XplatUIX11.DisplayHandle;
					xevent.ClientMessageEvent.window = XplatUIX11.HoverState.Window;
					xevent.ClientMessageEvent.message_type = XplatUIX11.HoverState.Atom;
					xevent.ClientMessageEvent.format = 32;
					xevent.ClientMessageEvent.ptr1 = (IntPtr)((XplatUIX11.HoverState.Y << 16) | XplatUIX11.HoverState.X);
					objectFromWindow.Queue.EnqueueLocked(xevent);
					this.WakeupMain();
				}
			}
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0007ED90 File Offset: 0x0007CF90
		private void CaretCallback(object sender, EventArgs e)
		{
			if (XplatUIX11.Caret.Paused)
			{
				return;
			}
			XplatUIX11.Caret.On = !XplatUIX11.Caret.On;
			XplatUIX11.XDrawLine(XplatUIX11.DisplayHandle, XplatUIX11.Caret.Hwnd, XplatUIX11.Caret.gc, XplatUIX11.Caret.X, XplatUIX11.Caret.Y, XplatUIX11.Caret.X, XplatUIX11.Caret.Y + XplatUIX11.Caret.Height);
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x00078DB0 File Offset: 0x00076FB0
		internal override int CaptionHeight
		{
			get
			{
				return 19;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x00050923 File Offset: 0x0004EB23
		internal override Size DragSize
		{
			get
			{
				return new Size(4, 4);
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x00050923 File Offset: 0x0004EB23
		internal override Size FrameBorderSize
		{
			get
			{
				return new Size(4, 4);
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x00002D70 File Offset: 0x00000F70
		internal override bool MenuAccessKeysUnderlined
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x00078DBD File Offset: 0x00076FBD
		internal override Size MinimumWindowSize
		{
			get
			{
				return new Size(110, 22);
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x0007EE13 File Offset: 0x0007D013
		internal override Size MinimumFixedToolWindowSize
		{
			get
			{
				return new Size(27, 22);
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x0007EE1E File Offset: 0x0007D01E
		internal override Size MinimumSizeableToolWindowSize
		{
			get
			{
				return new Size(37, 22);
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x00078DB4 File Offset: 0x00076FB4
		internal override Size MinimumNoBorderWindowSize
		{
			get
			{
				return new Size(2, 2);
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x0007EE29 File Offset: 0x0007D029
		internal override Keys ModifierKeys
		{
			get
			{
				return XplatUIX11.Keyboard.ModifierKeys;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x0007EE35 File Offset: 0x0007D035
		internal override Point MousePosition
		{
			get
			{
				return this.mouse_position;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x0007EE40 File Offset: 0x0007D040
		internal override Rectangle VirtualScreen
		{
			get
			{
				IntPtr zero = IntPtr.Zero;
				IntPtr intPtr;
				int num;
				IntPtr intPtr2;
				IntPtr intPtr3;
				XplatUIX11.XGetWindowProperty(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, XplatUIX11._NET_DESKTOP_GEOMETRY, IntPtr.Zero, new IntPtr(256), false, (IntPtr)6, out intPtr, out num, out intPtr2, out intPtr3, ref zero);
				if ((long)intPtr2 >= 2L)
				{
					int num2 = Marshal.ReadIntPtr(zero, 0).ToInt32();
					int num3 = Marshal.ReadIntPtr(zero, IntPtr.Size).ToInt32();
					XplatUIX11.XFree(zero);
					return new Rectangle(0, 0, num2, num3);
				}
				XWindowAttributes xwindowAttributes = default(XWindowAttributes);
				object xlibLock = XplatUIX11.XlibLock;
				lock (xlibLock)
				{
					XplatUIX11.XGetWindowAttributes(XplatUIX11.DisplayHandle, XplatUIX11.XRootWindow(XplatUIX11.DisplayHandle, 0), ref xwindowAttributes);
				}
				return new Rectangle(0, 0, xwindowAttributes.width, xwindowAttributes.height);
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x0007EF38 File Offset: 0x0007D138
		internal override Rectangle WorkingArea
		{
			get
			{
				IntPtr zero = IntPtr.Zero;
				IntPtr intPtr;
				int num;
				IntPtr intPtr2;
				IntPtr intPtr3;
				XplatUIX11.XGetWindowProperty(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, XplatUIX11._NET_CURRENT_DESKTOP, IntPtr.Zero, new IntPtr(1), false, (IntPtr)6, out intPtr, out num, out intPtr2, out intPtr3, ref zero);
				if ((long)intPtr2 >= 1L)
				{
					int num2 = Marshal.ReadIntPtr(zero, 0).ToInt32();
					XplatUIX11.XFree(zero);
					XplatUIX11.XGetWindowProperty(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, XplatUIX11._NET_WORKAREA, IntPtr.Zero, new IntPtr(256), false, (IntPtr)6, out intPtr, out num, out intPtr2, out intPtr3, ref zero);
					if ((long)intPtr2 >= (long)(4 * (num2 + 1)))
					{
						int num3 = Marshal.ReadIntPtr(zero, IntPtr.Size * 4 * num2).ToInt32();
						int num4 = Marshal.ReadIntPtr(zero, IntPtr.Size * 4 * num2 + IntPtr.Size).ToInt32();
						int num5 = Marshal.ReadIntPtr(zero, IntPtr.Size * 4 * num2 + IntPtr.Size * 2).ToInt32();
						int num6 = Marshal.ReadIntPtr(zero, IntPtr.Size * 4 * num2 + IntPtr.Size * 3).ToInt32();
						XplatUIX11.XFree(zero);
						return new Rectangle(num3, num4, num5, num6);
					}
				}
				XWindowAttributes xwindowAttributes = default(XWindowAttributes);
				object xlibLock = XplatUIX11.XlibLock;
				lock (xlibLock)
				{
					XplatUIX11.XGetWindowAttributes(XplatUIX11.DisplayHandle, XplatUIX11.XRootWindow(XplatUIX11.DisplayHandle, 0), ref xwindowAttributes);
				}
				return new Rectangle(0, 0, xwindowAttributes.width, xwindowAttributes.height);
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x0007F0EC File Offset: 0x0007D2EC
		internal override Screen[] AllScreens
		{
			get
			{
				if (!XplatUIX11.XineramaIsActive(XplatUIX11.DisplayHandle))
				{
					return null;
				}
				int num;
				IntPtr intPtr = XplatUIX11.XineramaQueryScreens(XplatUIX11.DisplayHandle, out num);
				Screen[] array = new Screen[num];
				IntPtr intPtr2 = intPtr;
				for (int i = 0; i < num; i++)
				{
					XineramaScreenInfo xineramaScreenInfo = (XineramaScreenInfo)Marshal.PtrToStructure(intPtr2, typeof(XineramaScreenInfo));
					Rectangle rectangle = new Rectangle((int)xineramaScreenInfo.x_org, (int)xineramaScreenInfo.y_org, (int)xineramaScreenInfo.width, (int)xineramaScreenInfo.height);
					string text = string.Format("Display {0}", xineramaScreenInfo.screen_number);
					array[i] = new Screen(i == 0, text, rectangle, rectangle);
					intPtr2 = (IntPtr)((long)intPtr2 + (long)Marshal.SizeOf(typeof(XineramaScreenInfo)));
				}
				XplatUIX11.XFree(intPtr);
				return array;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x0007F1BE File Offset: 0x0007D3BE
		internal override bool ThemesEnabled
		{
			get
			{
				return XplatUIX11.themes_enabled;
			}
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0007F1C8 File Offset: 0x0007D3C8
		internal override IntPtr InitializeDriver()
		{
			lock (this)
			{
				if (XplatUIX11.DisplayHandle == IntPtr.Zero)
				{
					this.SetDisplay(XplatUIX11.XOpenDisplay(IntPtr.Zero));
				}
			}
			return IntPtr.Zero;
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x0007F224 File Offset: 0x0007D424
		internal override void Activate(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd != null)
			{
				object xlibLock = XplatUIX11.XlibLock;
				lock (xlibLock)
				{
					this.SendNetWMMessage(hwnd.whole_window, XplatUIX11._NET_ACTIVE_WINDOW, (IntPtr)1, IntPtr.Zero, IntPtr.Zero);
					XEventQueue xeventQueue = null;
					ArrayList arrayList = XplatUIX11.unattached_timer_list;
					lock (arrayList)
					{
						foreach (object obj in XplatUIX11.unattached_timer_list)
						{
							Timer timer = (Timer)obj;
							if (xeventQueue == null)
							{
								xeventQueue = (XEventQueue)XplatUIX11.MessageQueues[Thread.CurrentThread];
							}
							timer.thread = xeventQueue.Thread;
							xeventQueue.timer_list.Add(timer);
						}
						XplatUIX11.unattached_timer_list.Clear();
					}
				}
			}
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x0007F340 File Offset: 0x0007D540
		internal override void AudibleAlert(AlertType alert)
		{
			XplatUIX11.XBell(XplatUIX11.DisplayHandle, 0);
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x0007F350 File Offset: 0x0007D550
		internal override void CaretVisible(IntPtr handle, bool visible)
		{
			if (XplatUIX11.Caret.Hwnd == handle)
			{
				if (visible)
				{
					if (!XplatUIX11.Caret.Visible)
					{
						XplatUIX11.Caret.Visible = true;
						this.ShowCaret();
						XplatUIX11.Caret.Timer.Start();
						return;
					}
				}
				else
				{
					XplatUIX11.Caret.Visible = false;
					XplatUIX11.Caret.Timer.Stop();
					this.HideCaret();
				}
			}
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00076B74 File Offset: 0x00074D74
		internal override bool CalculateWindowRect(ref Rectangle ClientRect, CreateParams cp, Menu menu, out Rectangle WindowRect)
		{
			WindowRect = Hwnd.GetWindowRectangle(cp, menu, ClientRect);
			return true;
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x0007F3C0 File Offset: 0x0007D5C0
		internal override void ClientToScreen(IntPtr handle, ref int x, ref int y)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			object xlibLock = XplatUIX11.XlibLock;
			int num;
			int num2;
			lock (xlibLock)
			{
				IntPtr intPtr;
				XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, hwnd.client_window, XplatUIX11.RootWindow, x, y, out num, out num2, out intPtr);
			}
			x = num;
			y = num2;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0007F42C File Offset: 0x0007D62C
		internal override int[] ClipboardAvailableFormats(IntPtr handle)
		{
			DataFormats.Format format = DataFormats.Format.List;
			if (XplatUIX11.XGetSelectionOwner(XplatUIX11.DisplayHandle, XplatUIX11.CLIPBOARD) == IntPtr.Zero)
			{
				return null;
			}
			XplatUIX11.Clipboard.Formats = new ArrayList();
			while (format != null)
			{
				XplatUIX11.XConvertSelection(XplatUIX11.DisplayHandle, XplatUIX11.CLIPBOARD, (IntPtr)format.Id, (IntPtr)format.Id, XplatUIX11.FosterParent, IntPtr.Zero);
				TimeSpan timeSpan = TimeSpan.FromSeconds(4.0);
				DateTime now = DateTime.Now;
				XplatUIX11.Clipboard.Enumerating = true;
				while (XplatUIX11.Clipboard.Enumerating)
				{
					this.UpdateMessageQueue(null, false);
					if (DateTime.Now - now > timeSpan)
					{
						break;
					}
				}
				format = format.Next;
			}
			int[] array = new int[XplatUIX11.Clipboard.Formats.Count];
			for (int i = 0; i < XplatUIX11.Clipboard.Formats.Count; i++)
			{
				array[i] = ((IntPtr)XplatUIX11.Clipboard.Formats[i]).ToInt32();
			}
			XplatUIX11.Clipboard.Formats = null;
			return array;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0007F558 File Offset: 0x0007D758
		internal override void ClipboardClose(IntPtr handle)
		{
			if (handle != XplatUIX11.ClipMagic)
			{
				throw new ArgumentException("handle is not a valid clipboard handle");
			}
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0007F574 File Offset: 0x0007D774
		internal override int ClipboardGetID(IntPtr handle, string format)
		{
			if (handle != XplatUIX11.ClipMagic)
			{
				throw new ArgumentException("handle is not a valid clipboard handle");
			}
			if (format == "Text")
			{
				return 31;
			}
			if (format == "Bitmap")
			{
				return 5;
			}
			if (format == "OEMText")
			{
				return XplatUIX11.OEMTEXT.ToInt32();
			}
			if (format == "DeviceIndependentBitmap")
			{
				return 20;
			}
			if (format == "Palette")
			{
				return 7;
			}
			if (format == "UnicodeText")
			{
				return XplatUIX11.UTF8_STRING.ToInt32();
			}
			if (format == "Rich Text Format")
			{
				return XplatUIX11.RICHTEXTFORMAT.ToInt32();
			}
			return XplatUIX11.XInternAtom(XplatUIX11.DisplayHandle, format, false).ToInt32();
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0007F633 File Offset: 0x0007D833
		internal override IntPtr ClipboardOpen(bool primary_selection)
		{
			if (!primary_selection)
			{
				XplatUIX11.ClipMagic = XplatUIX11.CLIPBOARD;
			}
			else
			{
				XplatUIX11.ClipMagic = XplatUIX11.PRIMARY;
			}
			return XplatUIX11.ClipMagic;
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0007F654 File Offset: 0x0007D854
		internal override object ClipboardRetrieve(IntPtr handle, int type, XplatUI.ClipboardToObject converter)
		{
			XplatUIX11.XConvertSelection(XplatUIX11.DisplayHandle, handle, (IntPtr)type, (IntPtr)type, XplatUIX11.FosterParent, IntPtr.Zero);
			XplatUIX11.Clipboard.Retrieving = true;
			while (XplatUIX11.Clipboard.Retrieving)
			{
				this.UpdateMessageQueue(null, false);
			}
			return XplatUIX11.Clipboard.Item;
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0007F6B0 File Offset: 0x0007D8B0
		internal override void ClipboardStore(IntPtr handle, object obj, int type, XplatUI.ObjectToClipboard converter, bool copy)
		{
			XplatUIX11.Clipboard.Converter = converter;
			if (obj != null)
			{
				XplatUIX11.Clipboard.AddSource(type, obj);
				XplatUIX11.XSetSelectionOwner(XplatUIX11.DisplayHandle, XplatUIX11.CLIPBOARD, XplatUIX11.FosterParent, IntPtr.Zero);
				if (!copy)
				{
					return;
				}
				try
				{
					IntPtr intPtr = XplatUIX11.gtk_clipboard_get(XplatUIX11.gdk_atom_intern("CLIPBOARD", true));
					if (intPtr != IntPtr.Zero)
					{
						string text = XplatUIX11.Clipboard.GetRtfText();
						if (string.IsNullOrEmpty(text))
						{
							text = XplatUIX11.Clipboard.GetPlainText();
						}
						if (!string.IsNullOrEmpty(text))
						{
							XplatUIX11.gtk_clipboard_set_text(intPtr, text, text.Length);
							XplatUIX11.gtk_clipboard_store(intPtr);
						}
					}
					return;
				}
				catch
				{
					return;
				}
			}
			XplatUIX11.Clipboard.ClearSources();
			XplatUIX11.XSetSelectionOwner(XplatUIX11.DisplayHandle, XplatUIX11.CLIPBOARD, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x0007F78C File Offset: 0x0007D98C
		internal override void CreateCaret(IntPtr handle, int width, int height)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (XplatUIX11.Caret.Hwnd != IntPtr.Zero)
			{
				this.DestroyCaret(XplatUIX11.Caret.Hwnd);
			}
			XplatUIX11.Caret.Hwnd = handle;
			XplatUIX11.Caret.Window = hwnd.client_window;
			XplatUIX11.Caret.Width = width;
			XplatUIX11.Caret.Height = height;
			XplatUIX11.Caret.Visible = false;
			XplatUIX11.Caret.On = false;
			XGCValues xgcvalues = default(XGCValues);
			xgcvalues.line_width = width;
			XplatUIX11.Caret.gc = XplatUIX11.XCreateGC(XplatUIX11.DisplayHandle, XplatUIX11.Caret.Window, new IntPtr(16), ref xgcvalues);
			if (XplatUIX11.Caret.gc == IntPtr.Zero)
			{
				XplatUIX11.Caret.Hwnd = IntPtr.Zero;
				return;
			}
			XplatUIX11.XSetFunction(XplatUIX11.DisplayHandle, XplatUIX11.Caret.gc, GXFunction.GXinvert);
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0007F884 File Offset: 0x0007DA84
		internal override IntPtr CreateWindow(CreateParams cp)
		{
			Hwnd hwnd = null;
			Hwnd hwnd2 = new Hwnd();
			XSetWindowAttributes xsetWindowAttributes = default(XSetWindowAttributes);
			int num = cp.X;
			int num2 = cp.Y;
			int num3 = cp.Width;
			int num4 = cp.Height;
			if (num3 < 1)
			{
				num3 = 1;
			}
			if (num4 < 1)
			{
				num4 = 1;
			}
			IntPtr intPtr;
			if (cp.Parent != IntPtr.Zero)
			{
				hwnd = Hwnd.ObjectFromHandle(cp.Parent);
				intPtr = hwnd.client_window;
			}
			else if (this.StyleSet(cp.Style, WindowStyles.WS_CHILD))
			{
				intPtr = XplatUIX11.FosterParent;
			}
			else
			{
				intPtr = XplatUIX11.RootWindow;
			}
			if (cp.control is Form)
			{
				Point nextStackedFormLocation = Hwnd.GetNextStackedFormLocation(cp, hwnd);
				num = nextStackedFormLocation.X;
				num2 = nextStackedFormLocation.Y;
			}
			SetWindowValuemask setWindowValuemask = SetWindowValuemask.BitGravity | SetWindowValuemask.WinGravity;
			xsetWindowAttributes.bit_gravity = Gravity.NorthWestGravity;
			xsetWindowAttributes.win_gravity = Gravity.NorthWestGravity;
			if (this.ExStyleSet(cp.ExStyle, WindowExStyles.WS_EX_TOOLWINDOW))
			{
				xsetWindowAttributes.save_under = true;
				setWindowValuemask |= SetWindowValuemask.SaveUnder;
			}
			if (this.StyleSet(cp.Style, WindowStyles.WS_POPUP) && !this.StyleSet(cp.Style, WindowStyles.WS_CAPTION))
			{
				xsetWindowAttributes.override_redirect = true;
				setWindowValuemask |= SetWindowValuemask.OverrideRedirect;
			}
			hwnd2.x = num;
			hwnd2.y = num2;
			hwnd2.width = num3;
			hwnd2.height = num4;
			hwnd2.parent = Hwnd.ObjectFromHandle(cp.Parent);
			hwnd2.initial_style = cp.WindowStyle;
			hwnd2.initial_ex_style = cp.WindowExStyle;
			if (this.StyleSet(cp.Style, WindowStyles.WS_DISABLED))
			{
				hwnd2.enabled = false;
			}
			IntPtr intPtr2 = IntPtr.Zero;
			Size size = XplatUIX11.TranslateWindowSizeToXWindowSize(cp);
			Rectangle rectangle = XplatUIX11.TranslateClientRectangleToXClientRectangle(hwnd2, cp.control);
			object obj = XplatUIX11.XlibLock;
			IntPtr intPtr3;
			lock (obj)
			{
				intPtr3 = XplatUIX11.XCreateWindow(XplatUIX11.DisplayHandle, intPtr, num, num2, size.Width, size.Height, 0, 0, 1, IntPtr.Zero, new UIntPtr((uint)setWindowValuemask), ref xsetWindowAttributes);
				if (intPtr3 != IntPtr.Zero)
				{
					setWindowValuemask &= ~(SetWindowValuemask.OverrideRedirect | SetWindowValuemask.SaveUnder);
					if (XplatUIX11.CustomVisual != IntPtr.Zero && XplatUIX11.CustomColormap != IntPtr.Zero)
					{
						setWindowValuemask = SetWindowValuemask.ColorMap;
						xsetWindowAttributes.colormap = XplatUIX11.CustomColormap;
					}
					intPtr2 = XplatUIX11.XCreateWindow(XplatUIX11.DisplayHandle, intPtr3, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, 0, 0, 1, XplatUIX11.CustomVisual, new UIntPtr((uint)setWindowValuemask), ref xsetWindowAttributes);
				}
			}
			if (intPtr3 == IntPtr.Zero || intPtr2 == IntPtr.Zero)
			{
				throw new Exception("Could not create X11 windows");
			}
			hwnd2.Queue = this.ThreadQueue(Thread.CurrentThread);
			hwnd2.WholeWindow = intPtr3;
			hwnd2.ClientWindow = intPtr2;
			if (!this.StyleSet(cp.Style, WindowStyles.WS_CHILD) && num != -2147483648 && num2 != -2147483648)
			{
				XSizeHints xsizeHints = default(XSizeHints);
				xsizeHints.x = num;
				xsizeHints.y = num2;
				xsizeHints.flags = (IntPtr)5;
				XplatUIX11.XSetWMNormalHints(XplatUIX11.DisplayHandle, intPtr3, ref xsizeHints);
			}
			obj = XplatUIX11.XlibLock;
			lock (obj)
			{
				XplatUIX11.XSelectInput(XplatUIX11.DisplayHandle, hwnd2.whole_window, new IntPtr((int)(EventMask.KeyPressMask | EventMask.KeyReleaseMask | EventMask.ButtonPressMask | EventMask.ButtonReleaseMask | EventMask.EnterWindowMask | EventMask.LeaveWindowMask | EventMask.PointerMotionMask | EventMask.PointerMotionHintMask | EventMask.ExposureMask | EventMask.StructureNotifyMask | EventMask.SubstructureNotifyMask | EventMask.FocusChangeMask | EventMask.PropertyChangeMask | XplatUIX11.Keyboard.KeyEventMask)));
				if (hwnd2.whole_window != hwnd2.client_window)
				{
					XplatUIX11.XSelectInput(XplatUIX11.DisplayHandle, hwnd2.client_window, new IntPtr((int)(EventMask.KeyPressMask | EventMask.KeyReleaseMask | EventMask.ButtonPressMask | EventMask.ButtonReleaseMask | EventMask.EnterWindowMask | EventMask.LeaveWindowMask | EventMask.PointerMotionMask | EventMask.PointerMotionHintMask | EventMask.ExposureMask | EventMask.StructureNotifyMask | EventMask.SubstructureNotifyMask | EventMask.FocusChangeMask | XplatUIX11.Keyboard.KeyEventMask)));
				}
			}
			if (this.ExStyleSet(cp.ExStyle, WindowExStyles.WS_EX_TOPMOST))
			{
				this.SetTopmost(hwnd2.whole_window, true);
			}
			this.SetWMStyles(hwnd2, cp);
			XWMHints xwmhints = default(XWMHints);
			xwmhints.flags = (IntPtr)67;
			xwmhints.input = !this.StyleSet(cp.Style, WindowStyles.WS_DISABLED);
			xwmhints.initial_state = (this.StyleSet(cp.Style, WindowStyles.WS_MINIMIZE) ? XInitialState.IconicState : XInitialState.NormalState);
			if (intPtr != XplatUIX11.RootWindow)
			{
				xwmhints.window_group = hwnd2.whole_window;
			}
			else
			{
				xwmhints.window_group = intPtr;
			}
			obj = XplatUIX11.XlibLock;
			lock (obj)
			{
				XplatUIX11.XSetWMHints(XplatUIX11.DisplayHandle, hwnd2.whole_window, ref xwmhints);
			}
			if (this.StyleSet(cp.Style, WindowStyles.WS_MINIMIZE))
			{
				this.SetWindowState(hwnd2.Handle, FormWindowState.Minimized);
			}
			else if (this.StyleSet(cp.Style, WindowStyles.WS_MAXIMIZE))
			{
				this.SetWindowState(hwnd2.Handle, FormWindowState.Maximized);
			}
			XplatUIX11.Dnd.SetAllowDrop(hwnd2, true);
			this.Text(hwnd2.Handle, cp.Caption);
			this.SendMessage(hwnd2.Handle, Msg.WM_CREATE, (IntPtr)1, IntPtr.Zero);
			this.SendParentNotify(hwnd2.Handle, Msg.WM_CREATE, int.MaxValue, int.MaxValue);
			if (this.StyleSet(cp.Style, WindowStyles.WS_VISIBLE))
			{
				hwnd2.visible = true;
				this.MapWindow(hwnd2, WindowType.Both);
				if (!(Control.FromHandle(hwnd2.Handle) is Form))
				{
					this.SendMessage(hwnd2.Handle, Msg.WM_SHOWWINDOW, (IntPtr)1, IntPtr.Zero);
				}
			}
			return hwnd2.Handle;
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0007FE08 File Offset: 0x0007E008
		internal override IntPtr DefineCursor(Bitmap bitmap, Bitmap mask, Color cursor_pixel, Color mask_pixel, int xHotSpot, int yHotSpot)
		{
			int width;
			int height;
			if (XplatUIX11.XQueryBestCursor(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, bitmap.Width, bitmap.Height, out width, out height) == 0)
			{
				return IntPtr.Zero;
			}
			Bitmap bitmap2;
			Bitmap bitmap3;
			if (bitmap.Width != width || bitmap.Width != height)
			{
				bitmap2 = new Bitmap(bitmap, new Size(width, height));
				bitmap3 = new Bitmap(mask, new Size(width, height));
			}
			else
			{
				bitmap2 = bitmap;
				bitmap3 = mask;
			}
			width = bitmap2.Width;
			height = bitmap2.Height;
			byte[] array = new byte[width / 8 * height];
			byte[] array2 = new byte[width / 8 * height];
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					Color pixel = bitmap2.GetPixel(j, i);
					Color pixel2 = bitmap3.GetPixel(j, i);
					bool flag = pixel == cursor_pixel;
					bool flag2 = pixel2 == mask_pixel;
					if (!flag && !flag2)
					{
						byte[] array3 = array2;
						int num = i * width / 8 + j / 8;
						array3[num] |= (byte)(1 << j % 8);
					}
					else if (flag && !flag2)
					{
						byte[] array4 = array;
						int num2 = i * width / 8 + j / 8;
						array4[num2] |= (byte)(1 << j % 8);
						byte[] array5 = array2;
						int num3 = i * width / 8 + j / 8;
						array5[num3] |= (byte)(1 << j % 8);
					}
				}
			}
			IntPtr intPtr = XplatUIX11.XCreatePixmapFromBitmapData(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, array, width, height, (IntPtr)1, (IntPtr)0, 1);
			IntPtr intPtr2 = XplatUIX11.XCreatePixmapFromBitmapData(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, array2, width, height, (IntPtr)1, (IntPtr)0, 1);
			XColor xcolor = default(XColor);
			XColor xcolor2 = default(XColor);
			xcolor.pixel = XplatUIX11.XWhitePixel(XplatUIX11.DisplayHandle, XplatUIX11.ScreenNo);
			xcolor.red = ushort.MaxValue;
			xcolor.green = ushort.MaxValue;
			xcolor.blue = ushort.MaxValue;
			xcolor2.pixel = XplatUIX11.XBlackPixel(XplatUIX11.DisplayHandle, XplatUIX11.ScreenNo);
			IntPtr intPtr3 = XplatUIX11.XCreatePixmapCursor(XplatUIX11.DisplayHandle, intPtr, intPtr2, ref xcolor, ref xcolor2, xHotSpot, yHotSpot);
			XplatUIX11.XFreePixmap(XplatUIX11.DisplayHandle, intPtr);
			XplatUIX11.XFreePixmap(XplatUIX11.DisplayHandle, intPtr2);
			return intPtr3;
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00080044 File Offset: 0x0007E244
		internal override IntPtr DefineStdCursor(StdCursor id)
		{
			CursorFontShape cursorFontShape = XplatUIX11.StdCursorToFontShape(id);
			object xlibLock = XplatUIX11.XlibLock;
			IntPtr intPtr;
			lock (xlibLock)
			{
				intPtr = XplatUIX11.XCreateFontCursor(XplatUIX11.DisplayHandle, cursorFontShape);
			}
			return intPtr;
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00080094 File Offset: 0x0007E294
		internal static CursorFontShape StdCursorToFontShape(StdCursor id)
		{
			CursorFontShape cursorFontShape;
			switch (id)
			{
			case StdCursor.Default:
				cursorFontShape = CursorFontShape.XC_top_left_arrow;
				break;
			case StdCursor.AppStarting:
				cursorFontShape = CursorFontShape.XC_watch;
				break;
			case StdCursor.Arrow:
				cursorFontShape = CursorFontShape.XC_top_left_arrow;
				break;
			case StdCursor.Cross:
				cursorFontShape = CursorFontShape.XC_crosshair;
				break;
			case StdCursor.Hand:
				cursorFontShape = CursorFontShape.XC_hand1;
				break;
			case StdCursor.Help:
				cursorFontShape = CursorFontShape.XC_question_arrow;
				break;
			case StdCursor.HSplit:
				cursorFontShape = CursorFontShape.XC_sb_v_double_arrow;
				break;
			case StdCursor.IBeam:
				cursorFontShape = CursorFontShape.XC_xterm;
				break;
			case StdCursor.No:
				cursorFontShape = CursorFontShape.XC_circle;
				break;
			case StdCursor.NoMove2D:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.NoMoveHoriz:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.NoMoveVert:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.PanEast:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.PanNE:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.PanNorth:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.PanNW:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.PanSE:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.PanSouth:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.PanSW:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.PanWest:
				cursorFontShape = CursorFontShape.XC_sizing;
				break;
			case StdCursor.SizeAll:
				cursorFontShape = CursorFontShape.XC_fleur;
				break;
			case StdCursor.SizeNESW:
				cursorFontShape = CursorFontShape.XC_top_right_corner;
				break;
			case StdCursor.SizeNS:
				cursorFontShape = CursorFontShape.XC_sb_v_double_arrow;
				break;
			case StdCursor.SizeNWSE:
				cursorFontShape = CursorFontShape.XC_top_left_corner;
				break;
			case StdCursor.SizeWE:
				cursorFontShape = CursorFontShape.XC_sb_h_double_arrow;
				break;
			case StdCursor.UpArrow:
				cursorFontShape = CursorFontShape.XC_center_ptr;
				break;
			case StdCursor.VSplit:
				cursorFontShape = CursorFontShape.XC_sb_h_double_arrow;
				break;
			case StdCursor.WaitCursor:
				cursorFontShape = CursorFontShape.XC_watch;
				break;
			default:
				cursorFontShape = CursorFontShape.XC_X_cursor;
				break;
			}
			return cursorFontShape;
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x000801D0 File Offset: 0x0007E3D0
		internal override IntPtr DefWndProc(ref Message msg)
		{
			Msg msg2 = (Msg)msg.Msg;
			if (msg2 <= Msg.WM_NCCALCSIZE)
			{
				if (msg2 <= Msg.WM_SETCURSOR)
				{
					if (msg2 == Msg.WM_PAINT)
					{
						Hwnd objectFromWindow = Hwnd.GetObjectFromWindow(msg.HWnd);
						if (objectFromWindow != null)
						{
							objectFromWindow.expose_pending = false;
						}
						return IntPtr.Zero;
					}
					if (msg2 == Msg.WM_SETCURSOR)
					{
						Hwnd hwnd = Hwnd.GetObjectFromWindow(msg.HWnd);
						if (hwnd != null)
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
										goto IL_042B;
									case HitTest.HTRIGHT:
										intPtr = Cursors.SizeWE.handle;
										goto IL_042B;
									case HitTest.HTTOP:
										intPtr = Cursors.SizeNS.handle;
										goto IL_042B;
									case HitTest.HTTOPLEFT:
										intPtr = Cursors.SizeNWSE.handle;
										goto IL_042B;
									case HitTest.HTTOPRIGHT:
										intPtr = Cursors.SizeNESW.handle;
										goto IL_042B;
									case HitTest.HTBOTTOM:
										intPtr = Cursors.SizeNS.handle;
										goto IL_042B;
									case HitTest.HTBOTTOMLEFT:
										intPtr = Cursors.SizeNESW.handle;
										goto IL_042B;
									case HitTest.HTBOTTOMRIGHT:
										intPtr = Cursors.SizeNWSE.handle;
										goto IL_042B;
									case HitTest.HTBORDER:
										intPtr = Cursors.SizeNS.handle;
										goto IL_042B;
									case HitTest.HTHELP:
										intPtr = Cursors.Help.handle;
										goto IL_042B;
									}
									intPtr = Cursors.Default.handle;
								}
								else
								{
									if (msg.LParam.ToInt32() >> 16 == 513)
									{
										this.AudibleAlert(AlertType.Default);
									}
									intPtr = Cursors.Default.handle;
								}
								IL_042B:
								this.SetCursor(msg.HWnd, intPtr);
							}
							return (IntPtr)1;
						}
					}
				}
				else
				{
					if (msg2 == Msg.WM_CONTEXTMENU)
					{
						Hwnd objectFromWindow2 = Hwnd.GetObjectFromWindow(msg.HWnd);
						if (objectFromWindow2 != null && objectFromWindow2.parent != null)
						{
							this.SendMessage(objectFromWindow2.parent.client_window, Msg.WM_CONTEXTMENU, msg.WParam, msg.LParam);
						}
						return IntPtr.Zero;
					}
					if (msg2 == Msg.WM_NCCALCSIZE)
					{
						if (msg.WParam == (IntPtr)1)
						{
							Hwnd objectFromWindow3 = Hwnd.GetObjectFromWindow(msg.HWnd);
							XplatUIWin32.NCCALCSIZE_PARAMS nccalcsize_PARAMS = (XplatUIWin32.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(msg.LParam, typeof(XplatUIWin32.NCCALCSIZE_PARAMS));
							Control control = Control.FromHandle(objectFromWindow3.Handle);
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
						return IntPtr.Zero;
					}
				}
			}
			else if (msg2 <= Msg.WM_IME_COMPOSITION)
			{
				if (msg2 == Msg.WM_NCPAINT)
				{
					Hwnd objectFromWindow4 = Hwnd.GetObjectFromWindow(msg.HWnd);
					if (objectFromWindow4 != null)
					{
						objectFromWindow4.nc_expose_pending = false;
					}
					return IntPtr.Zero;
				}
				if (msg2 == Msg.WM_IME_COMPOSITION)
				{
					foreach (char c in XplatUIX11.Keyboard.GetCompositionString())
					{
						this.SendMessage(msg.HWnd, Msg.WM_IME_CHAR, (IntPtr)((int)c), msg.LParam);
					}
					return IntPtr.Zero;
				}
			}
			else if (msg2 != Msg.WM_MOUSEWHEEL)
			{
				if (msg2 == Msg.WM_IME_CHAR)
				{
					this.SendMessage(msg.HWnd, Msg.WM_CHAR, msg.WParam, msg.LParam);
					return IntPtr.Zero;
				}
			}
			else
			{
				Hwnd objectFromWindow5 = Hwnd.GetObjectFromWindow(msg.HWnd);
				if (objectFromWindow5 != null && objectFromWindow5.parent != null)
				{
					this.SendMessage(objectFromWindow5.parent.client_window, Msg.WM_MOUSEWHEEL, msg.WParam, msg.LParam);
					msg.Result == IntPtr.Zero;
					return IntPtr.Zero;
				}
				return IntPtr.Zero;
			}
			return IntPtr.Zero;
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00080624 File Offset: 0x0007E824
		internal override void DestroyCaret(IntPtr handle)
		{
			if (XplatUIX11.Caret.Hwnd == handle)
			{
				if (XplatUIX11.Caret.Visible)
				{
					this.HideCaret();
					XplatUIX11.Caret.Timer.Stop();
				}
				if (XplatUIX11.Caret.gc != IntPtr.Zero)
				{
					XplatUIX11.XFreeGC(XplatUIX11.DisplayHandle, XplatUIX11.Caret.gc);
					XplatUIX11.Caret.gc = IntPtr.Zero;
				}
				XplatUIX11.Caret.Hwnd = IntPtr.Zero;
				XplatUIX11.Caret.Visible = false;
				XplatUIX11.Caret.On = false;
			}
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000806C8 File Offset: 0x0007E8C8
		internal override void DestroyWindow(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null || hwnd.zombie)
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
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				if (hwnd.whole_window != IntPtr.Zero)
				{
					XplatUIX11.Keyboard.DestroyICForWindow(hwnd.whole_window);
					XplatUIX11.XDestroyWindow(XplatUIX11.DisplayHandle, hwnd.whole_window);
				}
				else if (hwnd.client_window != IntPtr.Zero)
				{
					XplatUIX11.Keyboard.DestroyICForWindow(hwnd.client_window);
					XplatUIX11.XDestroyWindow(XplatUIX11.DisplayHandle, hwnd.client_window);
				}
			}
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x000777C0 File Offset: 0x000759C0
		internal override IntPtr DispatchMessage(ref MSG msg)
		{
			return NativeWindow.WndProc(msg.hwnd, msg.message, msg.wParam, msg.lParam);
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00080818 File Offset: 0x0007EA18
		private IntPtr GetReversibleControlGC(Control control, int line_width)
		{
			XGCValues xgcvalues = default(XGCValues);
			xgcvalues.subwindow_mode = GCSubwindowMode.IncludeInferiors;
			xgcvalues.line_width = line_width;
			xgcvalues.foreground = XplatUIX11.XBlackPixel(XplatUIX11.DisplayHandle, XplatUIX11.ScreenNo);
			IntPtr intPtr = XplatUIX11.XCreateGC(XplatUIX11.DisplayHandle, control.Handle, new IntPtr(32788), ref xgcvalues);
			XColor xcolor = default(XColor);
			xcolor.red = (ushort)((int)control.ForeColor.R * 257);
			xcolor.green = (ushort)((int)control.ForeColor.G * 257);
			xcolor.blue = (ushort)((int)control.ForeColor.B * 257);
			XplatUIX11.XAllocColor(XplatUIX11.DisplayHandle, XplatUIX11.DefaultColormap, ref xcolor);
			uint num = (uint)xcolor.pixel.ToInt32();
			xcolor.red = (ushort)((int)control.BackColor.R * 257);
			xcolor.green = (ushort)((int)control.BackColor.G * 257);
			xcolor.blue = (ushort)((int)control.BackColor.B * 257);
			XplatUIX11.XAllocColor(XplatUIX11.DisplayHandle, XplatUIX11.DefaultColormap, ref xcolor);
			uint num2 = (uint)xcolor.pixel.ToInt32();
			uint num3 = num ^ num2;
			XplatUIX11.XSetForeground(XplatUIX11.DisplayHandle, intPtr, (UIntPtr)uint.MaxValue);
			XplatUIX11.XSetBackground(XplatUIX11.DisplayHandle, intPtr, (UIntPtr)num2);
			XplatUIX11.XSetFunction(XplatUIX11.DisplayHandle, intPtr, GXFunction.GXxor);
			XplatUIX11.XSetPlaneMask(XplatUIX11.DisplayHandle, intPtr, (IntPtr)((long)((ulong)num3)));
			return intPtr;
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x000809B0 File Offset: 0x0007EBB0
		internal override void DrawReversibleRectangle(IntPtr handle, Rectangle rect, int line_width)
		{
			Control control = Control.FromHandle(handle);
			IntPtr reversibleControlGC = this.GetReversibleControlGC(control, line_width);
			if (rect.Width > 0 && rect.Height > 0)
			{
				XplatUIX11.XDrawRectangle(XplatUIX11.DisplayHandle, control.Handle, reversibleControlGC, rect.Left, rect.Top, rect.Width, rect.Height);
			}
			else if (rect.Width > 0)
			{
				XplatUIX11.XDrawLine(XplatUIX11.DisplayHandle, control.Handle, reversibleControlGC, rect.X, rect.Y, rect.Right, rect.Y);
			}
			else
			{
				XplatUIX11.XDrawLine(XplatUIX11.DisplayHandle, control.Handle, reversibleControlGC, rect.X, rect.Y, rect.X, rect.Bottom);
			}
			XplatUIX11.XFreeGC(XplatUIX11.DisplayHandle, reversibleControlGC);
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00080A88 File Offset: 0x0007EC88
		internal override void DoEvents()
		{
			MSG msg = default(MSG);
			if (XplatUIX11.OverrideCursorHandle != IntPtr.Zero)
			{
				XplatUIX11.OverrideCursorHandle = IntPtr.Zero;
			}
			XEventQueue xeventQueue = this.ThreadQueue(Thread.CurrentThread);
			xeventQueue.DispatchIdle = false;
			XplatUIX11.in_doevents = true;
			while (this.PeekMessage(xeventQueue, ref msg, IntPtr.Zero, 0, 0, 1U))
			{
				Message message = Message.Create(msg.hwnd, (int)msg.message, msg.wParam, msg.lParam);
				if (!Application.FilterMessage(ref message))
				{
					this.TranslateMessage(ref msg);
					this.DispatchMessage(ref msg);
					string text = msg.hwnd.ToString() + ":" + msg.message;
					if (XplatUIX11.messageHold[text] != null)
					{
						XplatUIX11.messageHold[text] = (int)XplatUIX11.messageHold[text] - 1;
					}
				}
			}
			XplatUIX11.in_doevents = false;
			xeventQueue.DispatchIdle = true;
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00080B84 File Offset: 0x0007ED84
		internal override void EnableWindow(IntPtr handle, bool Enable)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd != null)
			{
				hwnd.Enabled = Enable;
			}
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override void EndLoop(Thread thread)
		{
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00080BA4 File Offset: 0x0007EDA4
		internal override IntPtr GetActive()
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2;
			int num;
			IntPtr intPtr3;
			IntPtr intPtr4;
			XplatUIX11.XGetWindowProperty(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, XplatUIX11._NET_ACTIVE_WINDOW, IntPtr.Zero, new IntPtr(1), false, (IntPtr)33, out intPtr2, out num, out intPtr3, out intPtr4, ref zero);
			if ((long)intPtr3 > 0L && zero != IntPtr.Zero)
			{
				intPtr = (IntPtr)Marshal.ReadInt32(zero);
				XplatUIX11.XFree(zero);
			}
			else
			{
				IntPtr zero2 = IntPtr.Zero;
				XplatUIX11.XGetInputFocus(XplatUIX11.DisplayHandle, out intPtr, out zero2);
			}
			if (intPtr != IntPtr.Zero)
			{
				Hwnd objectFromWindow = Hwnd.GetObjectFromWindow(intPtr);
				if (objectFromWindow != null)
				{
					intPtr = objectFromWindow.Handle;
				}
				else
				{
					intPtr = IntPtr.Zero;
				}
			}
			return intPtr;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00080C65 File Offset: 0x0007EE65
		internal override void GetCursorInfo(IntPtr cursor, out int width, out int height, out int hotspot_x, out int hotspot_y)
		{
			width = 20;
			height = 20;
			hotspot_x = 0;
			hotspot_y = 0;
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00080C78 File Offset: 0x0007EE78
		internal override void GetDisplaySize(out Size size)
		{
			XWindowAttributes xwindowAttributes = default(XWindowAttributes);
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				XplatUIX11.XGetWindowAttributes(XplatUIX11.DisplayHandle, XplatUIX11.XRootWindow(XplatUIX11.DisplayHandle, 0), ref xwindowAttributes);
			}
			size = new Size(xwindowAttributes.width, xwindowAttributes.height);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00080CE8 File Offset: 0x0007EEE8
		internal override SizeF GetAutoScaleSize(Font font)
		{
			string text = "The quick brown fox jumped over the lazy dog.";
			double num = 44.54999694824219;
			return new SizeF((float)((double)Graphics.FromHwnd(XplatUIX11.FosterParent).MeasureString(text, font).Width / num), (float)font.Height);
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00080D30 File Offset: 0x0007EF30
		internal override IntPtr GetParent(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd != null && hwnd.parent != null)
			{
				return hwnd.parent.Handle;
			}
			return IntPtr.Zero;
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00009E44 File Offset: 0x00008044
		internal override IntPtr GetPreviousWindow(IntPtr handle)
		{
			return handle;
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00080D60 File Offset: 0x0007EF60
		internal override void GetCursorPos(IntPtr handle, out int x, out int y)
		{
			IntPtr intPtr;
			if (handle != IntPtr.Zero)
			{
				intPtr = Hwnd.ObjectFromHandle(handle).client_window;
			}
			else
			{
				intPtr = XplatUIX11.RootWindow;
			}
			object xlibLock = XplatUIX11.XlibLock;
			int num;
			int num2;
			int num3;
			int num4;
			lock (xlibLock)
			{
				IntPtr intPtr2;
				IntPtr intPtr3;
				int num5;
				this.QueryPointer(XplatUIX11.DisplayHandle, intPtr, out intPtr2, out intPtr3, out num, out num2, out num3, out num4, out num5);
			}
			if (handle != IntPtr.Zero)
			{
				x = num3;
				y = num4;
				return;
			}
			x = num;
			y = num2;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00080DF8 File Offset: 0x0007EFF8
		internal override IntPtr GetFocus()
		{
			return XplatUIX11.FocusWindow;
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00080E00 File Offset: 0x0007F000
		internal override bool GetFontMetrics(Graphics g, Font font, out int ascent, out int descent)
		{
			FontFamily fontFamily = font.FontFamily;
			ascent = fontFamily.GetCellAscent(font.Style);
			descent = fontFamily.GetCellDescent(font.Style);
			return true;
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00080E34 File Offset: 0x0007F034
		internal override Point GetMenuOrigin(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd != null)
			{
				return hwnd.MenuOrigin;
			}
			return Point.Empty;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00080E58 File Offset: 0x0007F058
		[MonoTODO("Implement filtering")]
		internal override bool GetMessage(object queue_id, ref MSG msg, IntPtr handle, int wFilterMin, int wFilterMax)
		{
			XEvent xevent;
			Hwnd hwnd;
			bool flag2;
			for (;;)
			{
				IL_0000:
				if (((XEventQueue)queue_id).Count > 0)
				{
					xevent = ((XEventQueue)queue_id).Dequeue();
				}
				else
				{
					this.UpdateMessageQueue((XEventQueue)queue_id);
					if (((XEventQueue)queue_id).Count > 0)
					{
						xevent = ((XEventQueue)queue_id).Dequeue();
					}
					else
					{
						if (((XEventQueue)queue_id).Paint.Count <= 0)
						{
							break;
						}
						xevent = ((XEventQueue)queue_id).Paint.Dequeue();
					}
				}
				hwnd = Hwnd.GetObjectFromWindow(xevent.AnyEvent.window);
				if (hwnd != null && hwnd.zombie && xevent.type == XEventName.Expose)
				{
					Hwnd hwnd2 = hwnd;
					bool flag = (hwnd.nc_expose_pending = false);
					hwnd2.expose_pending = flag;
					hwnd.Queue.Paint.Remove(hwnd);
				}
				else if (hwnd != null && (!hwnd.zombie || xevent.AnyEvent.type == XEventName.ClientMessage))
				{
					if (hwnd.zombie)
					{
						hwnd.resizing_or_moving = false;
					}
					flag2 = hwnd.client_window == xevent.AnyEvent.window;
					msg.hwnd = hwnd.Handle;
					if (hwnd.resizing_or_moving)
					{
						IntPtr intPtr;
						IntPtr intPtr2;
						int num;
						int num2;
						int num3;
						int num4;
						int num5;
						XplatUIX11.XQueryPointer(XplatUIX11.DisplayHandle, hwnd.Handle, out intPtr, out intPtr2, out num, out num2, out num3, out num4, out num5);
						if ((num5 & 256) == 0 && (num5 & 512) == 0 && (num5 & 1024) == 0)
						{
							hwnd.resizing_or_moving = false;
							this.SendMessage(hwnd.Handle, Msg.WM_EXITSIZEMOVE, IntPtr.Zero, IntPtr.Zero);
						}
					}
					XEventName type = xevent.type;
					switch (type)
					{
					case XEventName.KeyPress:
						goto IL_0202;
					case XEventName.KeyRelease:
						goto IL_02A1;
					case XEventName.ButtonPress:
						goto IL_02B7;
					case XEventName.ButtonRelease:
						switch (xevent.ButtonEvent.button)
						{
						case 1:
							goto IL_07C3;
						case 2:
							goto IL_0851;
						case 3:
							goto IL_08DF;
						case 4:
						case 5:
							continue;
						}
						goto Block_34;
					case XEventName.MotionNotify:
						goto IL_0B13;
					case XEventName.EnterNotify:
						if (!hwnd.Enabled || xevent.CrossingEvent.mode == NotifyMode.NotifyGrab || xevent.AnyEvent.window != hwnd.client_window)
						{
							continue;
						}
						if (xevent.CrossingEvent.mode != NotifyMode.NotifyUngrab)
						{
							goto IL_1036;
						}
						if (XplatUIX11.LastPointerWindow == xevent.AnyEvent.window)
						{
							continue;
						}
						if (XplatUIX11.LastPointerWindow != IntPtr.Zero)
						{
							Point point = new Point(xevent.ButtonEvent.x, xevent.ButtonEvent.y);
							Control[] allControls = Control.FromHandle(hwnd.client_window).Controls.GetAllControls();
							for (int i = 0; i < allControls.Length; i++)
							{
								if (allControls[i].Bounds.Contains(point))
								{
									goto IL_0000;
								}
							}
							goto Block_59;
						}
						goto IL_1036;
					case XEventName.LeaveNotify:
						if (xevent.CrossingEvent.mode == NotifyMode.NotifyUngrab)
						{
							this.WindowUngrabbed(hwnd.Handle);
							continue;
						}
						if (hwnd.Enabled && xevent.CrossingEvent.mode == NotifyMode.NotifyNormal && !(xevent.CrossingEvent.window != hwnd.client_window) && !(XplatUIX11.Grab.Hwnd != IntPtr.Zero))
						{
							goto Block_64;
						}
						continue;
					case XEventName.FocusIn:
						break;
					case XEventName.FocusOut:
						if (xevent.FocusChangeEvent.detail != NotifyDetail.NotifyNonlinear)
						{
							continue;
						}
						while (XplatUIX11.Keyboard.ResetKeyState(XplatUIX11.FocusWindow, ref msg))
						{
							this.SendMessage(XplatUIX11.FocusWindow, msg.message, msg.wParam, msg.lParam);
						}
						XplatUIX11.Keyboard.FocusOut(hwnd.client_window);
						this.SendMessage(XplatUIX11.FocusWindow, Msg.WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
						continue;
					case XEventName.KeymapNotify:
					case XEventName.GraphicsExpose:
					case XEventName.NoExpose:
					case XEventName.VisibilityNotify:
					case XEventName.CreateNotify:
					case XEventName.UnmapNotify:
					case XEventName.MapNotify:
					case XEventName.MapRequest:
						continue;
					case XEventName.Expose:
						if (!hwnd.Mapped)
						{
							if (flag2)
							{
								hwnd.expose_pending = false;
								continue;
							}
							hwnd.nc_expose_pending = false;
							continue;
						}
						else if (flag2)
						{
							if (!hwnd.expose_pending)
							{
								continue;
							}
							goto IL_168C;
						}
						else
						{
							if (hwnd.nc_expose_pending)
							{
								goto Block_83;
							}
							continue;
						}
						break;
					case XEventName.DestroyNotify:
						hwnd = Hwnd.ObjectFromHandle(xevent.DestroyWindowEvent.window);
						if (hwnd != null && hwnd.client_window == xevent.DestroyWindowEvent.window)
						{
							goto Block_91;
						}
						continue;
					case XEventName.ReparentNotify:
						if (hwnd.parent != null)
						{
							continue;
						}
						if (xevent.ReparentEvent.parent != IntPtr.Zero && xevent.ReparentEvent.window == hwnd.whole_window)
						{
							hwnd.Reparented = true;
							Point topLevelWindowLocation = XplatUIX11.GetTopLevelWindowLocation(hwnd);
							hwnd.X = topLevelWindowLocation.X;
							hwnd.Y = topLevelWindowLocation.Y;
							if (hwnd.opacity != 4294967295U)
							{
								IntPtr intPtr3 = (IntPtr)((int)hwnd.opacity);
								XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, this.XGetParent(hwnd.whole_window), XplatUIX11._NET_WM_WINDOW_OPACITY, (IntPtr)6, 32, PropertyMode.Replace, ref intPtr3, 1);
							}
							this.SendMessage(msg.hwnd, Msg.WM_WINDOWPOSCHANGED, msg.wParam, msg.lParam);
							continue;
						}
						hwnd.Reparented = false;
						continue;
					case XEventName.ConfigureNotify:
					{
						if (flag2 || !(xevent.ConfigureEvent.xevent == xevent.ConfigureEvent.window))
						{
							continue;
						}
						object configure_lock = hwnd.configure_lock;
						lock (configure_lock)
						{
							Form form = Control.FromHandle(hwnd.client_window) as Form;
							if (form != null && !hwnd.resizing_or_moving)
							{
								if (hwnd.x != form.Bounds.X || hwnd.y != form.Bounds.Y)
								{
									this.SendMessage(form.Handle, Msg.WM_SYSCOMMAND, (IntPtr)61456, IntPtr.Zero);
									hwnd.resizing_or_moving = true;
								}
								else if (hwnd.width != form.Bounds.Width || hwnd.height != form.Bounds.Height)
								{
									this.SendMessage(form.Handle, Msg.WM_SYSCOMMAND, (IntPtr)61440, IntPtr.Zero);
									hwnd.resizing_or_moving = true;
								}
								if (hwnd.resizing_or_moving)
								{
									this.SendMessage(form.Handle, Msg.WM_ENTERSIZEMOVE, IntPtr.Zero, IntPtr.Zero);
								}
							}
							this.SendMessage(msg.hwnd, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
							hwnd.configure_pending = false;
							if (hwnd.whole_window != hwnd.client_window)
							{
								this.PerformNCCalc(hwnd);
							}
							continue;
						}
						break;
					}
					default:
						if (type != XEventName.ClientMessage)
						{
							continue;
						}
						if (XplatUIX11.Dnd.HandleClientMessage(ref xevent))
						{
							continue;
						}
						if (xevent.ClientMessageEvent.message_type == XplatUIX11.AsyncAtom)
						{
							XplatUIDriverSupport.ExecuteClientMessage((GCHandle)xevent.ClientMessageEvent.ptr1);
							continue;
						}
						if (xevent.ClientMessageEvent.message_type == XplatUIX11.HoverState.Atom)
						{
							goto Block_94;
						}
						if (xevent.ClientMessageEvent.message_type == XplatUIX11.PostAtom)
						{
							goto Block_95;
						}
						if (xevent.ClientMessageEvent.message_type == XplatUIX11._XEMBED && xevent.ClientMessageEvent.ptr2.ToInt32() == 0)
						{
							XSizeHints xsizeHints = default(XSizeHints);
							IntPtr intPtr4;
							XplatUIX11.XGetWMNormalHints(XplatUIX11.DisplayHandle, hwnd.whole_window, ref xsizeHints, out intPtr4);
							hwnd.width = xsizeHints.max_width;
							hwnd.height = xsizeHints.max_height;
							hwnd.ClientRect = Rectangle.Empty;
							this.SendMessage(msg.hwnd, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
						}
						if (!(xevent.ClientMessageEvent.message_type == XplatUIX11.WM_PROTOCOLS))
						{
							continue;
						}
						if (xevent.ClientMessageEvent.ptr1 == XplatUIX11.WM_DELETE_WINDOW)
						{
							goto Block_100;
						}
						if (xevent.ClientMessageEvent.ptr1 == XplatUIX11.WM_TAKE_FOCUS)
						{
							continue;
						}
						continue;
					}
					if (xevent.FocusChangeEvent.detail == NotifyDetail.NotifyNonlinear)
					{
						if (XplatUIX11.FocusWindow == IntPtr.Zero)
						{
							Control control = Control.FromHandle(hwnd.client_window);
							if (control != null)
							{
								Form form2 = control.FindForm();
								if (form2 != null && XplatUIX11.ActiveWindow != form2.Handle)
								{
									XplatUIX11.ActiveWindow = form2.Handle;
									this.SendMessage(XplatUIX11.ActiveWindow, Msg.WM_ACTIVATE, (IntPtr)1, IntPtr.Zero);
								}
							}
						}
						else
						{
							XplatUIX11.Keyboard.FocusIn(XplatUIX11.FocusWindow);
							this.SendMessage(XplatUIX11.FocusWindow, Msg.WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
						}
					}
				}
			}
			msg.hwnd = IntPtr.Zero;
			msg.message = Msg.WM_ENTERIDLE;
			return true;
			IL_0202:
			XplatUIX11.Keyboard.KeyEvent(XplatUIX11.FocusWindow, xevent, ref msg);
			if (msg.wParam == (IntPtr)112 || msg.wParam == (IntPtr)47)
			{
				HELPINFO helpinfo = default(HELPINFO);
				this.GetCursorPos(IntPtr.Zero, out helpinfo.MousePos.x, out helpinfo.MousePos.y);
				IntPtr intPtr5 = Marshal.AllocHGlobal(Marshal.SizeOf<HELPINFO>(helpinfo));
				Marshal.StructureToPtr<HELPINFO>(helpinfo, intPtr5, true);
				NativeWindow.WndProc(XplatUIX11.FocusWindow, Msg.WM_HELP, IntPtr.Zero, intPtr5);
				Marshal.FreeHGlobal(intPtr5);
				return true;
			}
			return true;
			IL_02A1:
			XplatUIX11.Keyboard.KeyEvent(XplatUIX11.FocusWindow, xevent, ref msg);
			return true;
			IL_02B7:
			switch (xevent.ButtonEvent.button)
			{
			case 1:
				XplatUIX11.MouseState |= MouseButtons.Left;
				if (flag2)
				{
					msg.message = Msg.WM_LBUTTONDOWN;
					msg.wParam = this.GetMousewParam(0);
				}
				else
				{
					msg.message = Msg.WM_NCLBUTTONDOWN;
					msg.wParam = (IntPtr)((int)this.NCHitTest(hwnd, xevent.MotionEvent.x, xevent.MotionEvent.y));
					this.MenuToScreen(xevent.AnyEvent.window, ref xevent.ButtonEvent.x, ref xevent.ButtonEvent.y);
				}
				break;
			case 2:
				XplatUIX11.MouseState |= MouseButtons.Middle;
				if (flag2)
				{
					msg.message = Msg.WM_MBUTTONDOWN;
					msg.wParam = this.GetMousewParam(0);
				}
				else
				{
					msg.message = Msg.WM_NCMBUTTONDOWN;
					msg.wParam = (IntPtr)((int)this.NCHitTest(hwnd, xevent.MotionEvent.x, xevent.MotionEvent.y));
					this.MenuToScreen(xevent.AnyEvent.window, ref xevent.ButtonEvent.x, ref xevent.ButtonEvent.y);
				}
				break;
			case 3:
				XplatUIX11.MouseState |= MouseButtons.Right;
				if (flag2)
				{
					msg.message = Msg.WM_RBUTTONDOWN;
					msg.wParam = this.GetMousewParam(0);
				}
				else
				{
					msg.message = Msg.WM_NCRBUTTONDOWN;
					msg.wParam = (IntPtr)((int)this.NCHitTest(hwnd, xevent.MotionEvent.x, xevent.MotionEvent.y));
					this.MenuToScreen(xevent.AnyEvent.window, ref xevent.ButtonEvent.x, ref xevent.ButtonEvent.y);
				}
				break;
			case 4:
				msg.hwnd = XplatUIX11.FocusWindow;
				msg.message = Msg.WM_MOUSEWHEEL;
				msg.wParam = this.GetMousewParam(120);
				break;
			case 5:
				msg.hwnd = XplatUIX11.FocusWindow;
				msg.message = Msg.WM_MOUSEWHEEL;
				msg.wParam = this.GetMousewParam(-120);
				break;
			}
			msg.lParam = (IntPtr)((xevent.ButtonEvent.y << 16) | xevent.ButtonEvent.x);
			this.mouse_position.X = xevent.ButtonEvent.x;
			this.mouse_position.Y = xevent.ButtonEvent.y;
			if (!hwnd.Enabled)
			{
				msg.hwnd = hwnd.EnabledHwnd;
				IntPtr intPtr6;
				XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, xevent.AnyEvent.window, Hwnd.ObjectFromHandle(msg.hwnd).ClientWindow, xevent.ButtonEvent.x, xevent.ButtonEvent.y, out xevent.ButtonEvent.x, out xevent.ButtonEvent.y, out intPtr6);
				msg.lParam = (IntPtr)((this.mouse_position.Y << 16) | this.mouse_position.X);
			}
			if (XplatUIX11.Grab.Hwnd != IntPtr.Zero)
			{
				msg.hwnd = XplatUIX11.Grab.Hwnd;
			}
			if (XplatUIX11.ClickPending.Pending && (long)xevent.ButtonEvent.time - XplatUIX11.ClickPending.Time < (long)XplatUIX11.DoubleClickInterval && msg.wParam == XplatUIX11.ClickPending.wParam && msg.lParam == XplatUIX11.ClickPending.lParam && msg.message == XplatUIX11.ClickPending.Message)
			{
				switch (xevent.ButtonEvent.button)
				{
				case 1:
					msg.message = (flag2 ? Msg.WM_LBUTTONDBLCLK : Msg.WM_NCLBUTTONDBLCLK);
					break;
				case 2:
					msg.message = (flag2 ? Msg.WM_MBUTTONDBLCLK : Msg.WM_NCMBUTTONDBLCLK);
					break;
				case 3:
					msg.message = (flag2 ? Msg.WM_RBUTTONDBLCLK : Msg.WM_NCRBUTTONDBLCLK);
					break;
				}
				XplatUIX11.ClickPending.Pending = false;
			}
			else
			{
				XplatUIX11.ClickPending.Pending = true;
				XplatUIX11.ClickPending.Hwnd = msg.hwnd;
				XplatUIX11.ClickPending.Message = msg.message;
				XplatUIX11.ClickPending.wParam = msg.wParam;
				XplatUIX11.ClickPending.lParam = msg.lParam;
				XplatUIX11.ClickPending.Time = (long)xevent.ButtonEvent.time;
			}
			if (msg.message == Msg.WM_LBUTTONDOWN || msg.message == Msg.WM_MBUTTONDOWN || msg.message == Msg.WM_RBUTTONDOWN)
			{
				this.SendParentNotify(msg.hwnd, msg.message, this.mouse_position.X, this.mouse_position.Y);
				return true;
			}
			return true;
			Block_34:
			goto IL_0968;
			IL_07C3:
			if (flag2)
			{
				msg.message = Msg.WM_LBUTTONUP;
			}
			else
			{
				msg.message = Msg.WM_NCLBUTTONUP;
				msg.wParam = (IntPtr)((int)this.NCHitTest(hwnd, xevent.MotionEvent.x, xevent.MotionEvent.y));
				this.MenuToScreen(xevent.AnyEvent.window, ref xevent.ButtonEvent.x, ref xevent.ButtonEvent.y);
			}
			XplatUIX11.MouseState &= ~MouseButtons.Left;
			msg.wParam = this.GetMousewParam(0);
			goto IL_0968;
			IL_0851:
			if (flag2)
			{
				msg.message = Msg.WM_MBUTTONUP;
			}
			else
			{
				msg.message = Msg.WM_NCMBUTTONUP;
				msg.wParam = (IntPtr)((int)this.NCHitTest(hwnd, xevent.MotionEvent.x, xevent.MotionEvent.y));
				this.MenuToScreen(xevent.AnyEvent.window, ref xevent.ButtonEvent.x, ref xevent.ButtonEvent.y);
			}
			XplatUIX11.MouseState &= ~MouseButtons.Middle;
			msg.wParam = this.GetMousewParam(0);
			goto IL_0968;
			IL_08DF:
			if (flag2)
			{
				msg.message = Msg.WM_RBUTTONUP;
			}
			else
			{
				msg.message = Msg.WM_NCRBUTTONUP;
				msg.wParam = (IntPtr)((int)this.NCHitTest(hwnd, xevent.MotionEvent.x, xevent.MotionEvent.y));
				this.MenuToScreen(xevent.AnyEvent.window, ref xevent.ButtonEvent.x, ref xevent.ButtonEvent.y);
			}
			XplatUIX11.MouseState &= ~MouseButtons.Right;
			msg.wParam = this.GetMousewParam(0);
			IL_0968:
			if (!hwnd.Enabled)
			{
				msg.hwnd = hwnd.EnabledHwnd;
				IntPtr intPtr7;
				XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, xevent.AnyEvent.window, Hwnd.ObjectFromHandle(msg.hwnd).ClientWindow, xevent.ButtonEvent.x, xevent.ButtonEvent.y, out xevent.ButtonEvent.x, out xevent.ButtonEvent.y, out intPtr7);
				msg.lParam = (IntPtr)((this.mouse_position.Y << 16) | this.mouse_position.X);
			}
			if (XplatUIX11.Grab.Hwnd != IntPtr.Zero)
			{
				msg.hwnd = XplatUIX11.Grab.Hwnd;
			}
			msg.lParam = (IntPtr)((xevent.ButtonEvent.y << 16) | xevent.ButtonEvent.x);
			this.mouse_position.X = xevent.ButtonEvent.x;
			this.mouse_position.Y = xevent.ButtonEvent.y;
			if (msg.message == Msg.WM_LBUTTONUP || msg.message == Msg.WM_MBUTTONUP || msg.message == Msg.WM_RBUTTONUP)
			{
				XEvent xevent2 = default(XEvent);
				xevent2.type = XEventName.MotionNotify;
				xevent2.MotionEvent.display = XplatUIX11.DisplayHandle;
				xevent2.MotionEvent.window = xevent.ButtonEvent.window;
				xevent2.MotionEvent.x = xevent.ButtonEvent.x;
				xevent2.MotionEvent.y = xevent.ButtonEvent.y;
				hwnd.Queue.EnqueueLocked(xevent2);
				return true;
			}
			return true;
			IL_0B13:
			if (!flag2)
			{
				msg.message = Msg.WM_NCMOUSEMOVE;
				if (!hwnd.Enabled)
				{
					msg.hwnd = hwnd.EnabledHwnd;
					IntPtr intPtr8;
					XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, xevent.AnyEvent.window, Hwnd.ObjectFromHandle(msg.hwnd).ClientWindow, xevent.MotionEvent.x, xevent.MotionEvent.y, out xevent.MotionEvent.x, out xevent.MotionEvent.y, out intPtr8);
					msg.lParam = (IntPtr)((this.mouse_position.Y << 16) | this.mouse_position.X);
				}
				HitTest hitTest = this.NCHitTest(hwnd, xevent.MotionEvent.x, xevent.MotionEvent.y);
				NativeWindow.WndProc(hwnd.client_window, Msg.WM_SETCURSOR, msg.hwnd, (IntPtr)((int)hitTest));
				this.mouse_position.X = xevent.MotionEvent.x;
				this.mouse_position.Y = xevent.MotionEvent.y;
				return true;
			}
			if (XplatUIX11.Grab.Hwnd != IntPtr.Zero)
			{
				msg.hwnd = XplatUIX11.Grab.Hwnd;
			}
			else if (hwnd.Enabled)
			{
				NativeWindow.WndProc(msg.hwnd, Msg.WM_SETCURSOR, msg.hwnd, (IntPtr)1);
			}
			if (xevent.MotionEvent.is_hint != 0)
			{
				IntPtr intPtr9;
				IntPtr intPtr10;
				int num6;
				XplatUIX11.XQueryPointer(XplatUIX11.DisplayHandle, xevent.AnyEvent.window, out intPtr9, out intPtr10, out xevent.MotionEvent.x_root, out xevent.MotionEvent.y_root, out xevent.MotionEvent.x, out xevent.MotionEvent.y, out num6);
			}
			msg.message = Msg.WM_MOUSEMOVE;
			msg.wParam = this.GetMousewParam(0);
			msg.lParam = (IntPtr)((xevent.MotionEvent.y << 16) | (xevent.MotionEvent.x & 65535));
			if (!hwnd.Enabled)
			{
				msg.hwnd = hwnd.EnabledHwnd;
				IntPtr intPtr11;
				XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, xevent.AnyEvent.window, Hwnd.ObjectFromHandle(msg.hwnd).ClientWindow, xevent.MotionEvent.x, xevent.MotionEvent.y, out xevent.MotionEvent.x, out xevent.MotionEvent.y, out intPtr11);
				msg.lParam = (IntPtr)((this.mouse_position.Y << 16) | this.mouse_position.X);
			}
			this.mouse_position.X = xevent.MotionEvent.x;
			this.mouse_position.Y = xevent.MotionEvent.y;
			if (XplatUIX11.HoverState.Timer.Enabled && (this.mouse_position.X + XplatUIX11.HoverState.Size.Width < XplatUIX11.HoverState.X || this.mouse_position.X - XplatUIX11.HoverState.Size.Width > XplatUIX11.HoverState.X || this.mouse_position.Y + XplatUIX11.HoverState.Size.Height < XplatUIX11.HoverState.Y || this.mouse_position.Y - XplatUIX11.HoverState.Size.Height > XplatUIX11.HoverState.Y))
			{
				XplatUIX11.HoverState.Timer.Stop();
				XplatUIX11.HoverState.Timer.Start();
				XplatUIX11.HoverState.X = this.mouse_position.X;
				XplatUIX11.HoverState.Y = this.mouse_position.Y;
				return true;
			}
			return true;
			Block_59:
			int x_root = xevent.CrossingEvent.x_root;
			int y_root = xevent.CrossingEvent.y_root;
			this.ScreenToClient(XplatUIX11.LastPointerWindow, ref x_root, ref y_root);
			XEvent xevent3 = default(XEvent);
			xevent3.type = XEventName.LeaveNotify;
			xevent3.CrossingEvent.display = XplatUIX11.DisplayHandle;
			xevent3.CrossingEvent.window = XplatUIX11.LastPointerWindow;
			xevent3.CrossingEvent.x = x_root;
			xevent3.CrossingEvent.y = y_root;
			xevent3.CrossingEvent.mode = NotifyMode.NotifyNormal;
			Hwnd.ObjectFromHandle(XplatUIX11.LastPointerWindow).Queue.EnqueueLocked(xevent3);
			IL_1036:
			XplatUIX11.LastPointerWindow = xevent.AnyEvent.window;
			msg.message = Msg.WM_MOUSE_ENTER;
			XplatUIX11.HoverState.X = xevent.CrossingEvent.x;
			XplatUIX11.HoverState.Y = xevent.CrossingEvent.y;
			XplatUIX11.HoverState.Timer.Enabled = true;
			XplatUIX11.HoverState.Window = xevent.CrossingEvent.window;
			XEvent xevent4 = default(XEvent);
			xevent4.type = XEventName.MotionNotify;
			xevent4.MotionEvent.display = XplatUIX11.DisplayHandle;
			xevent4.MotionEvent.window = xevent.ButtonEvent.window;
			xevent4.MotionEvent.x = xevent.ButtonEvent.x;
			xevent4.MotionEvent.y = xevent.ButtonEvent.y;
			hwnd.Queue.EnqueueLocked(xevent4);
			return true;
			Block_64:
			this.SetCursor(hwnd.client_window, IntPtr.Zero);
			msg.message = Msg.WM_MOUSELEAVE;
			XplatUIX11.HoverState.Timer.Enabled = false;
			XplatUIX11.HoverState.Window = IntPtr.Zero;
			return true;
			Block_83:
			FormBorderStyle border_style = hwnd.border_style;
			if (border_style != FormBorderStyle.FixedSingle)
			{
				if (border_style == FormBorderStyle.Fixed3D)
				{
					Graphics graphics = Graphics.FromHwnd(hwnd.whole_window);
					if (hwnd.border_static)
					{
						ControlPaint.DrawBorder3D(graphics, new Rectangle(0, 0, hwnd.Width, hwnd.Height), Border3DStyle.SunkenOuter);
					}
					else
					{
						ControlPaint.DrawBorder3D(graphics, new Rectangle(0, 0, hwnd.Width, hwnd.Height), Border3DStyle.Sunken);
					}
					graphics.Dispose();
				}
			}
			else
			{
				Graphics graphics2 = Graphics.FromHwnd(hwnd.whole_window);
				ControlPaint.DrawBorder(graphics2, new Rectangle(0, 0, hwnd.Width, hwnd.Height), Color.Black, ButtonBorderStyle.Solid);
				graphics2.Dispose();
			}
			Region region = new Region(new Rectangle(xevent.ExposeEvent.x, xevent.ExposeEvent.y, xevent.ExposeEvent.width, xevent.ExposeEvent.height));
			IntPtr hrgn = region.GetHrgn(null);
			msg.message = Msg.WM_NCPAINT;
			msg.wParam = ((hrgn == IntPtr.Zero) ? ((IntPtr)1) : hrgn);
			msg.refobject = region;
			return true;
			IL_168C:
			if (XplatUIX11.Caret.Visible)
			{
				XplatUIX11.Caret.Paused = true;
				this.HideCaret();
			}
			if (XplatUIX11.Caret.Visible)
			{
				this.ShowCaret();
				XplatUIX11.Caret.Paused = false;
			}
			msg.message = Msg.WM_PAINT;
			return true;
			Block_91:
			this.CleanupCachedWindows(hwnd);
			msg.hwnd = hwnd.client_window;
			msg.message = Msg.WM_DESTROY;
			hwnd.Dispose();
			return true;
			Block_94:
			msg.message = Msg.WM_MOUSEHOVER;
			msg.wParam = this.GetMousewParam(0);
			msg.lParam = xevent.ClientMessageEvent.ptr1;
			return true;
			Block_95:
			msg.hwnd = xevent.ClientMessageEvent.ptr1;
			msg.message = (Msg)xevent.ClientMessageEvent.ptr2.ToInt32();
			msg.wParam = xevent.ClientMessageEvent.ptr3;
			msg.lParam = xevent.ClientMessageEvent.ptr4;
			return msg.message != Msg.WM_QUIT;
			Block_100:
			this.SendMessage(msg.hwnd, Msg.WM_SYSCOMMAND, (IntPtr)61536, IntPtr.Zero);
			msg.message = Msg.WM_CLOSE;
			return true;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x000827A4 File Offset: 0x000809A4
		private HitTest NCHitTest(Hwnd hwnd, int x, int y)
		{
			int num;
			int num2;
			IntPtr intPtr;
			XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, hwnd.WholeWindow, XplatUIX11.RootWindow, x, y, out num, out num2, out intPtr);
			return (HitTest)(int)NativeWindow.WndProc(hwnd.client_window, Msg.WM_NCHITTEST, IntPtr.Zero, (IntPtr)((num2 << 16) | (num & 65535)));
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x000827FC File Offset: 0x000809FC
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

		// Token: 0x06001A6A RID: 6762 RVA: 0x00082878 File Offset: 0x00080A78
		internal override FormWindowState GetWindowState(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd.cached_window_state == (FormWindowState)(-1))
			{
				hwnd.cached_window_state = this.UpdateWindowState(handle);
			}
			return hwnd.cached_window_state;
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x000828A8 File Offset: 0x00080AA8
		private FormWindowState UpdateWindowState(IntPtr handle)
		{
			IntPtr zero = IntPtr.Zero;
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			int num = 0;
			bool flag = false;
			IntPtr intPtr;
			int num2;
			IntPtr intPtr2;
			IntPtr intPtr3;
			XplatUIX11.XGetWindowProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_STATE, IntPtr.Zero, new IntPtr(256), false, (IntPtr)4, out intPtr, out num2, out intPtr2, out intPtr3, ref zero);
			if ((long)intPtr2 > 0L && zero != IntPtr.Zero)
			{
				int num3 = 0;
				while ((long)num3 < (long)intPtr2)
				{
					IntPtr intPtr4 = (IntPtr)Marshal.ReadInt32(zero, num3 * 4);
					if (intPtr4 == XplatUIX11._NET_WM_STATE_MAXIMIZED_HORZ || intPtr4 == XplatUIX11._NET_WM_STATE_MAXIMIZED_VERT)
					{
						num++;
					}
					else if (intPtr4 == XplatUIX11._NET_WM_STATE_HIDDEN)
					{
						flag = true;
					}
					num3++;
				}
				XplatUIX11.XFree(zero);
			}
			if (flag)
			{
				return FormWindowState.Minimized;
			}
			if (num == 2)
			{
				return FormWindowState.Maximized;
			}
			XWindowAttributes xwindowAttributes = default(XWindowAttributes);
			XplatUIX11.XGetWindowAttributes(XplatUIX11.DisplayHandle, hwnd.client_window, ref xwindowAttributes);
			if (xwindowAttributes.map_state == MapState.IsUnmapped)
			{
				return (FormWindowState)(-1);
			}
			return FormWindowState.Normal;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x000829B3 File Offset: 0x00080BB3
		internal override void GrabInfo(out IntPtr handle, out bool GrabConfined, out Rectangle GrabArea)
		{
			handle = XplatUIX11.Grab.Hwnd;
			GrabConfined = XplatUIX11.Grab.Confined;
			GrabArea = XplatUIX11.Grab.Area;
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x000829E0 File Offset: 0x00080BE0
		internal override void GrabWindow(IntPtr handle, IntPtr confine_to_handle)
		{
			IntPtr intPtr = IntPtr.Zero;
			Hwnd hwnd;
			object obj;
			if (confine_to_handle != IntPtr.Zero)
			{
				XWindowAttributes xwindowAttributes = default(XWindowAttributes);
				hwnd = Hwnd.ObjectFromHandle(confine_to_handle);
				obj = XplatUIX11.XlibLock;
				lock (obj)
				{
					XplatUIX11.XGetWindowAttributes(XplatUIX11.DisplayHandle, hwnd.client_window, ref xwindowAttributes);
				}
				XplatUIX11.Grab.Area.X = xwindowAttributes.x;
				XplatUIX11.Grab.Area.Y = xwindowAttributes.y;
				XplatUIX11.Grab.Area.Width = xwindowAttributes.width;
				XplatUIX11.Grab.Area.Height = xwindowAttributes.height;
				XplatUIX11.Grab.Confined = true;
				intPtr = hwnd.client_window;
			}
			XplatUIX11.Grab.Hwnd = handle;
			hwnd = Hwnd.ObjectFromHandle(handle);
			obj = XplatUIX11.XlibLock;
			lock (obj)
			{
				XplatUIX11.XGrabPointer(XplatUIX11.DisplayHandle, hwnd.client_window, false, EventMask.ButtonPressMask | EventMask.ButtonReleaseMask | EventMask.LeaveWindowMask | EventMask.PointerMotionMask | EventMask.PointerMotionHintMask | EventMask.ButtonMotionMask, GrabMode.GrabModeAsync, GrabMode.GrabModeAsync, intPtr, IntPtr.Zero, IntPtr.Zero);
			}
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00082B1C File Offset: 0x00080D1C
		internal override void UngrabWindow(IntPtr hwnd)
		{
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				XplatUIX11.XUngrabPointer(XplatUIX11.DisplayHandle, IntPtr.Zero);
				XplatUIX11.XFlush(XplatUIX11.DisplayHandle);
			}
			this.WindowUngrabbed(hwnd);
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00082B78 File Offset: 0x00080D78
		private void WindowUngrabbed(IntPtr hwnd)
		{
			bool flag = XplatUIX11.Grab.Hwnd != IntPtr.Zero;
			XplatUIX11.Grab.Hwnd = IntPtr.Zero;
			XplatUIX11.Grab.Confined = false;
			if (flag)
			{
				this.SendMessage(hwnd, Msg.WM_CAPTURECHANGED, IntPtr.Zero, IntPtr.Zero);
			}
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x00082BCC File Offset: 0x00080DCC
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

		// Token: 0x06001A71 RID: 6769 RVA: 0x00082C28 File Offset: 0x00080E28
		internal override void InvalidateNC(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			this.AddExpose(hwnd, hwnd.WholeWindow == hwnd.ClientWindow, 0, 0, hwnd.Width, hwnd.Height);
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00082C64 File Offset: 0x00080E64
		internal override bool IsEnabled(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			return hwnd != null && hwnd.Enabled;
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00082C84 File Offset: 0x00080E84
		internal override void KillTimer(Timer timer)
		{
			XEventQueue xeventQueue = (XEventQueue)XplatUIX11.MessageQueues[timer.thread];
			if (xeventQueue == null)
			{
				ArrayList arrayList = XplatUIX11.unattached_timer_list;
				lock (arrayList)
				{
					if (XplatUIX11.unattached_timer_list.Contains(timer))
					{
						XplatUIX11.unattached_timer_list.Remove(timer);
					}
				}
				return;
			}
			xeventQueue.timer_list.Remove(timer);
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00082CFC File Offset: 0x00080EFC
		internal override void MenuToScreen(IntPtr handle, ref int x, ref int y)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			object xlibLock = XplatUIX11.XlibLock;
			int num;
			int num2;
			lock (xlibLock)
			{
				IntPtr intPtr;
				XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11.RootWindow, x, y, out num, out num2, out intPtr);
			}
			x = num;
			y = num2;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00082D68 File Offset: 0x00080F68
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
			if (XplatUIX11.Caret.Visible)
			{
				XplatUIX11.Caret.Paused = true;
				this.HideCaret();
			}
			Graphics graphics;
			PaintEventArgs paintEventArgs;
			if (client)
			{
				graphics = Graphics.FromHwnd(hwnd2.client_window);
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
				return paintEventArgs;
			}
			graphics = Graphics.FromHwnd(hwnd2.whole_window);
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
			return paintEventArgs;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00082EF0 File Offset: 0x000810F0
		internal override void PaintEventEnd(ref Message msg, IntPtr handle, bool client)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(msg.HWnd);
			Graphics graphics = (Graphics)hwnd.drawing_stack.Pop();
			graphics.Flush();
			graphics.Dispose();
			PaintEventArgs paintEventArgs = (PaintEventArgs)hwnd.drawing_stack.Pop();
			paintEventArgs.SetGraphics(null);
			paintEventArgs.Dispose();
			if (XplatUIX11.Caret.Visible)
			{
				this.ShowCaret();
				XplatUIX11.Caret.Paused = false;
			}
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x00082F5C File Offset: 0x0008115C
		[MonoTODO("Implement filtering and PM_NOREMOVE")]
		internal override bool PeekMessage(object queue_id, ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax, uint flags)
		{
			XEventQueue xeventQueue = (XEventQueue)queue_id;
			if ((flags & 1U) == 0U)
			{
				throw new NotImplementedException("PeekMessage PM_NOREMOVE is not implemented yet");
			}
			bool flag = false;
			if (xeventQueue.Count > 0)
			{
				flag = true;
			}
			else if (XplatUIX11.XPending(XplatUIX11.DisplayHandle) != 0)
			{
				this.UpdateMessageQueue((XEventQueue)queue_id);
				flag = true;
			}
			else if (((XEventQueue)queue_id).Paint.Count > 0)
			{
				flag = true;
			}
			this.CheckTimers(xeventQueue.timer_list, DateTime.UtcNow);
			return flag && this.GetMessage(queue_id, ref msg, hWnd, wFilterMin, wFilterMax);
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00082FE4 File Offset: 0x000811E4
		internal override bool PostMessage(IntPtr handle, Msg message, IntPtr wparam, IntPtr lparam)
		{
			XEvent xevent = default(XEvent);
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			xevent.type = XEventName.ClientMessage;
			xevent.ClientMessageEvent.display = XplatUIX11.DisplayHandle;
			if (hwnd != null)
			{
				xevent.ClientMessageEvent.window = hwnd.whole_window;
			}
			else
			{
				xevent.ClientMessageEvent.window = IntPtr.Zero;
			}
			xevent.ClientMessageEvent.message_type = XplatUIX11.PostAtom;
			xevent.ClientMessageEvent.format = 32;
			xevent.ClientMessageEvent.ptr1 = handle;
			xevent.ClientMessageEvent.ptr2 = (IntPtr)((int)message);
			xevent.ClientMessageEvent.ptr3 = wparam;
			xevent.ClientMessageEvent.ptr4 = lparam;
			if (hwnd != null)
			{
				hwnd.Queue.EnqueueLocked(xevent);
			}
			else
			{
				this.ThreadQueue(Thread.CurrentThread).EnqueueLocked(xevent);
			}
			return true;
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x000830C0 File Offset: 0x000812C0
		internal override void PostQuitMessage(int exitCode)
		{
			ApplicationContext context = Application.MWFThread.Current.Context;
			if (((context != null) ? context.MainForm : null) != null)
			{
				this.PostMessage(Application.MWFThread.Current.Context.MainForm.window.Handle, Msg.WM_QUIT, IntPtr.Zero, IntPtr.Zero);
			}
			else
			{
				this.PostMessage(XplatUIX11.FosterParent, Msg.WM_QUIT, IntPtr.Zero, IntPtr.Zero);
			}
			XplatUIX11.XFlush(XplatUIX11.DisplayHandle);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override void RequestAdditionalWM_NCMessages(IntPtr hwnd, bool hover, bool leave)
		{
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00083138 File Offset: 0x00081338
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

		// Token: 0x06001A7C RID: 6780 RVA: 0x00083174 File Offset: 0x00081374
		internal override void ResetMouseHover(IntPtr handle)
		{
			if (Hwnd.ObjectFromHandle(handle) == null)
			{
				return;
			}
			XplatUIX11.HoverState.Timer.Enabled = true;
			XplatUIX11.HoverState.X = this.mouse_position.X;
			XplatUIX11.HoverState.Y = this.mouse_position.Y;
			XplatUIX11.HoverState.Window = handle;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x000831D0 File Offset: 0x000813D0
		internal override void ScreenToClient(IntPtr handle, ref int x, ref int y)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			object xlibLock = XplatUIX11.XlibLock;
			int num;
			int num2;
			lock (xlibLock)
			{
				IntPtr intPtr;
				XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, hwnd.client_window, x, y, out num, out num2, out intPtr);
			}
			x = num;
			y = num2;
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0008323C File Offset: 0x0008143C
		internal override void ScreenToMenu(IntPtr handle, ref int x, ref int y)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			object xlibLock = XplatUIX11.XlibLock;
			int num;
			int num2;
			lock (xlibLock)
			{
				IntPtr intPtr;
				XplatUIX11.XTranslateCoordinates(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, hwnd.whole_window, x, y, out num, out num2, out intPtr);
			}
			Form form = Control.FromHandle(handle) as Form;
			if (form != null && form.window_manager != null)
			{
				num2 -= form.window_manager.TitleBarHeight;
			}
			x = num;
			y = num2;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x000832D0 File Offset: 0x000814D0
		private bool GraphicsExposePredicate(IntPtr display, ref XEvent xevent, IntPtr arg)
		{
			return (xevent.type == XEventName.GraphicsExpose || xevent.type == XEventName.NoExpose) && arg == xevent.GraphicsExposeEvent.drawable;
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x000832FC File Offset: 0x000814FC
		private void ProcessGraphicsExpose(Hwnd hwnd)
		{
			XEvent xevent = default(XEvent);
			IntPtr intPtr = Hwnd.HandleFromObject(hwnd);
			XplatUIX11.EventPredicate eventPredicate = new XplatUIX11.EventPredicate(this.GraphicsExposePredicate);
			do
			{
				XplatUIX11.XIfEvent(XplatUIX11.Display, ref xevent, eventPredicate, intPtr);
				if (xevent.type != XEventName.GraphicsExpose)
				{
					break;
				}
				this.AddExpose(hwnd, xevent.ExposeEvent.window == hwnd.ClientWindow, xevent.GraphicsExposeEvent.x, xevent.GraphicsExposeEvent.y, xevent.GraphicsExposeEvent.width, xevent.GraphicsExposeEvent.height);
			}
			while (xevent.GraphicsExposeEvent.count != 0);
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x00083394 File Offset: 0x00081594
		internal override void ScrollWindow(IntPtr handle, Rectangle area, int XAmount, int YAmount, bool with_children)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			Rectangle rectangle = Rectangle.Intersect(hwnd.Invalid, area);
			if (!rectangle.IsEmpty)
			{
				rectangle.X += XAmount;
				rectangle.Y += YAmount;
				if (rectangle.X < 0)
				{
					rectangle.Width += rectangle.X;
					rectangle.X = 0;
				}
				if (rectangle.Y < 0)
				{
					rectangle.Height += rectangle.Y;
					rectangle.Y = 0;
				}
				if (area.Contains(hwnd.Invalid))
				{
					hwnd.ClearInvalidArea();
				}
				hwnd.AddInvalidArea(rectangle);
			}
			XGCValues xgcvalues = default(XGCValues);
			if (with_children)
			{
				xgcvalues.subwindow_mode = GCSubwindowMode.IncludeInferiors;
			}
			IntPtr intPtr = XplatUIX11.XCreateGC(XplatUIX11.DisplayHandle, hwnd.client_window, IntPtr.Zero, ref xgcvalues);
			Rectangle totalVisibleArea = this.GetTotalVisibleArea(hwnd.client_window);
			totalVisibleArea.Intersect(area);
			Rectangle rectangle2 = totalVisibleArea;
			rectangle2.Y += YAmount;
			rectangle2.X += XAmount;
			rectangle2.Intersect(area);
			Point point = new Point(rectangle2.X - XAmount, rectangle2.Y - YAmount);
			XplatUIX11.XCopyArea(XplatUIX11.DisplayHandle, hwnd.client_window, hwnd.client_window, intPtr, point.X, point.Y, rectangle2.Width, rectangle2.Height, rectangle2.X, rectangle2.Y);
			Rectangle dirtyArea = this.GetDirtyArea(area, rectangle2, XAmount, YAmount);
			this.AddExpose(hwnd, true, dirtyArea.X, dirtyArea.Y, dirtyArea.Width, dirtyArea.Height);
			this.ProcessGraphicsExpose(hwnd);
			XplatUIX11.XFreeGC(XplatUIX11.DisplayHandle, intPtr);
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x00083558 File Offset: 0x00081758
		internal override void ScrollWindow(IntPtr handle, int XAmount, int YAmount, bool with_children)
		{
			Rectangle clientRect = Hwnd.GetObjectFromWindow(handle).ClientRect;
			clientRect.X = 0;
			clientRect.Y = 0;
			this.ScrollWindow(handle, clientRect, XAmount, YAmount, with_children);
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00083590 File Offset: 0x00081790
		private Rectangle GetDirtyArea(Rectangle total_area, Rectangle valid_area, int XAmount, int YAmount)
		{
			Rectangle rectangle = total_area;
			if (YAmount > 0)
			{
				rectangle.Height -= valid_area.Height;
			}
			else if (YAmount < 0)
			{
				rectangle.Height -= valid_area.Height;
				rectangle.Y += valid_area.Height;
			}
			if (XAmount > 0)
			{
				rectangle.Width -= valid_area.Width;
			}
			else if (XAmount < 0)
			{
				rectangle.Width -= valid_area.Width;
				rectangle.X += valid_area.Width;
			}
			return rectangle;
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00083634 File Offset: 0x00081834
		private Rectangle GetTotalVisibleArea(IntPtr handle)
		{
			Control control = Control.FromHandle(handle);
			Rectangle clientRectangle = control.ClientRectangle;
			clientRectangle.Location = control.PointToScreen(Point.Empty);
			for (Control control2 = control.Parent; control2 != null; control2 = control2.Parent)
			{
				if (!control2.IsHandleCreated || !control2.Visible)
				{
					return clientRectangle;
				}
				Rectangle clientRectangle2 = control2.ClientRectangle;
				clientRectangle2.Location = control2.PointToScreen(Point.Empty);
				clientRectangle.Intersect(clientRectangle2);
			}
			clientRectangle.Location = control.PointToClient(clientRectangle.Location);
			return clientRectangle;
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x000836BC File Offset: 0x000818BC
		internal override void SendAsyncMethod(AsyncMethodData method)
		{
			XEvent xevent = default(XEvent);
			Hwnd hwnd = Hwnd.ObjectFromHandle(method.Handle);
			xevent.type = XEventName.ClientMessage;
			xevent.ClientMessageEvent.display = XplatUIX11.DisplayHandle;
			xevent.ClientMessageEvent.window = method.Handle;
			xevent.ClientMessageEvent.message_type = XplatUIX11.AsyncAtom;
			xevent.ClientMessageEvent.format = 32;
			xevent.ClientMessageEvent.ptr1 = (IntPtr)GCHandle.Alloc(method);
			hwnd.Queue.EnqueueLocked(xevent);
			this.WakeupMain();
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x00083750 File Offset: 0x00081950
		internal override IntPtr SendMessage(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam)
		{
			Hwnd hwnd2 = Hwnd.ObjectFromHandle(hwnd);
			if (hwnd2 != null && hwnd2.queue != this.ThreadQueue(Thread.CurrentThread))
			{
				AsyncMethodResult asyncMethodResult = new AsyncMethodResult();
				this.SendAsyncMethod(new AsyncMethodData
				{
					Handle = hwnd,
					Method = new XplatUIX11.WndProcDelegate(NativeWindow.WndProc),
					Args = new object[] { hwnd, message, wParam, lParam },
					Result = asyncMethodResult
				});
				return IntPtr.Zero;
			}
			string text = hwnd.ToString() + ":" + message;
			if (XplatUIX11.messageHold[text] != null)
			{
				XplatUIX11.messageHold[text] = (int)XplatUIX11.messageHold[text] - 1;
			}
			return NativeWindow.WndProc(hwnd, message, wParam, lParam);
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override void SetAllowDrop(IntPtr handle, bool value)
		{
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00083838 File Offset: 0x00081A38
		internal override void SetBorderStyle(IntPtr handle, FormBorderStyle border_style)
		{
			Form form = Control.FromHandle(handle) as Form;
			if (form != null && form.window_manager == null)
			{
				CreateParams createParams = form.GetCreateParams();
				if (border_style == FormBorderStyle.FixedToolWindow || border_style == FormBorderStyle.SizableToolWindow || createParams.IsSet(WindowExStyles.WS_EX_TOOLWINDOW))
				{
					form.window_manager = new ToolWindowManager(form);
				}
			}
			this.RequestNCRecalc(handle);
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0008388C File Offset: 0x00081A8C
		internal override void SetCaretPos(IntPtr handle, int x, int y)
		{
			if (XplatUIX11.Caret.Hwnd == handle)
			{
				XplatUIX11.Caret.Timer.Stop();
				this.HideCaret();
				XplatUIX11.Caret.X = x;
				XplatUIX11.Caret.Y = y;
				XplatUIX11.Keyboard.SetCaretPos(XplatUIX11.Caret, handle, x, y);
				if (XplatUIX11.Caret.Visible)
				{
					this.ShowCaret();
					XplatUIX11.Caret.Timer.Start();
				}
			}
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x0008390C File Offset: 0x00081B0C
		internal override void SetClipRegion(IntPtr handle, Region region)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				return;
			}
			hwnd.UserClip = region;
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x0008392C File Offset: 0x00081B2C
		internal override void SetCursor(IntPtr handle, IntPtr cursor)
		{
			Hwnd hwnd;
			object obj;
			if (!(XplatUIX11.OverrideCursorHandle == IntPtr.Zero))
			{
				hwnd = Hwnd.ObjectFromHandle(handle);
				obj = XplatUIX11.XlibLock;
				lock (obj)
				{
					XplatUIX11.XDefineCursor(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11.OverrideCursorHandle);
				}
				return;
			}
			if (XplatUIX11.LastCursorWindow == handle && XplatUIX11.LastCursorHandle == cursor)
			{
				return;
			}
			XplatUIX11.LastCursorHandle = cursor;
			XplatUIX11.LastCursorWindow = handle;
			hwnd = Hwnd.ObjectFromHandle(handle);
			obj = XplatUIX11.XlibLock;
			lock (obj)
			{
				if (cursor != IntPtr.Zero)
				{
					XplatUIX11.XDefineCursor(XplatUIX11.DisplayHandle, hwnd.whole_window, cursor);
				}
				else
				{
					XplatUIX11.XUndefineCursor(XplatUIX11.DisplayHandle, hwnd.whole_window);
				}
				XplatUIX11.XFlush(XplatUIX11.DisplayHandle);
			}
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x00083A2C File Offset: 0x00081C2C
		private void QueryPointer(IntPtr display, IntPtr w, out IntPtr root, out IntPtr child, out int root_x, out int root_y, out int child_x, out int child_y, out int mask)
		{
			XplatUIX11.XGrabServer(display);
			IntPtr intPtr;
			XplatUIX11.XQueryPointer(display, w, out root, out intPtr, out root_x, out root_y, out child_x, out child_y, out mask);
			if (root != w)
			{
				intPtr = root;
			}
			IntPtr intPtr2 = IntPtr.Zero;
			while (intPtr != IntPtr.Zero)
			{
				intPtr2 = intPtr;
				XplatUIX11.XQueryPointer(display, intPtr, out root, out intPtr, out root_x, out root_y, out child_x, out child_y, out mask);
			}
			XplatUIX11.XUngrabServer(display);
			XplatUIX11.XFlush(display);
			child = intPtr2;
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x00083AA0 File Offset: 0x00081CA0
		internal override void SetCursorPos(IntPtr handle, int x, int y)
		{
			object obj;
			if (handle == IntPtr.Zero)
			{
				obj = XplatUIX11.XlibLock;
				lock (obj)
				{
					IntPtr intPtr;
					IntPtr intPtr2;
					int num;
					int num2;
					int num3;
					int num4;
					int num5;
					this.QueryPointer(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, out intPtr, out intPtr2, out num, out num2, out num3, out num4, out num5);
					XplatUIX11.XWarpPointer(XplatUIX11.DisplayHandle, IntPtr.Zero, IntPtr.Zero, 0, 0, 0U, 0U, x - num, y - num2);
					XplatUIX11.XFlush(XplatUIX11.DisplayHandle);
					this.QueryPointer(XplatUIX11.DisplayHandle, XplatUIX11.RootWindow, out intPtr, out intPtr2, out num, out num2, out num3, out num4, out num5);
					Hwnd hwnd = Hwnd.ObjectFromHandle(intPtr2);
					if (hwnd == null)
					{
						return;
					}
					XEvent xevent = default(XEvent);
					xevent.type = XEventName.MotionNotify;
					xevent.MotionEvent.display = XplatUIX11.DisplayHandle;
					xevent.MotionEvent.window = hwnd.client_window;
					xevent.MotionEvent.root = XplatUIX11.RootWindow;
					xevent.MotionEvent.x = num3;
					xevent.MotionEvent.y = num4;
					xevent.MotionEvent.x_root = num;
					xevent.MotionEvent.y_root = num2;
					xevent.MotionEvent.state = num5;
					hwnd.Queue.EnqueueLocked(xevent);
					return;
				}
			}
			Hwnd hwnd2 = Hwnd.ObjectFromHandle(handle);
			obj = XplatUIX11.XlibLock;
			lock (obj)
			{
				XplatUIX11.XWarpPointer(XplatUIX11.DisplayHandle, IntPtr.Zero, hwnd2.client_window, 0, 0, 0U, 0U, x, y);
			}
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x00083C60 File Offset: 0x00081E60
		internal override void SetFocus(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd.client_window == XplatUIX11.FocusWindow)
			{
				return;
			}
			if (!hwnd.enabled)
			{
				return;
			}
			IntPtr focusWindow = XplatUIX11.FocusWindow;
			XplatUIX11.FocusWindow = hwnd.client_window;
			if (focusWindow != IntPtr.Zero)
			{
				this.SendMessage(focusWindow, Msg.WM_KILLFOCUS, XplatUIX11.FocusWindow, IntPtr.Zero);
			}
			XplatUIX11.Keyboard.FocusIn(XplatUIX11.FocusWindow);
			this.SendMessage(XplatUIX11.FocusWindow, Msg.WM_SETFOCUS, focusWindow, IntPtr.Zero);
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x00083CE4 File Offset: 0x00081EE4
		internal override void SetIcon(IntPtr handle, Icon icon)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd != null)
			{
				this.SetIcon(hwnd, icon);
			}
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x000786CB File Offset: 0x000768CB
		internal override void SetMenu(IntPtr handle, Menu menu)
		{
			Hwnd.ObjectFromHandle(handle).menu = menu;
			this.RequestNCRecalc(handle);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x00083D04 File Offset: 0x00081F04
		internal override void SetModal(IntPtr handle, bool Modal)
		{
			if (Modal)
			{
				XplatUIX11.ModalWindows.Push(handle);
			}
			else
			{
				if (XplatUIX11.ModalWindows.Contains(handle))
				{
					XplatUIX11.ModalWindows.Pop();
				}
				if (XplatUIX11.ModalWindows.Count > 0)
				{
					this.Activate((IntPtr)XplatUIX11.ModalWindows.Peek());
				}
			}
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			Control control = Control.FromHandle(handle);
			this.SetWMStyles(hwnd, control.GetCreateParams());
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00083D80 File Offset: 0x00081F80
		internal override IntPtr SetParent(IntPtr handle, IntPtr parent)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			hwnd.parent = Hwnd.ObjectFromHandle(parent);
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				XplatUIX11.XReparentWindow(XplatUIX11.DisplayHandle, hwnd.whole_window, (hwnd.parent == null) ? XplatUIX11.FosterParent : hwnd.parent.client_window, hwnd.x, hwnd.y);
			}
			return IntPtr.Zero;
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00083E08 File Offset: 0x00082008
		internal override void SetTimer(Timer timer)
		{
			XEventQueue xeventQueue = (XEventQueue)XplatUIX11.MessageQueues[timer.thread];
			if (xeventQueue == null)
			{
				XplatUIX11.unattached_timer_list.Add(timer);
				return;
			}
			xeventQueue.timer_list.Add(timer);
			this.WakeupMain();
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x00083E50 File Offset: 0x00082050
		internal override bool SetTopmost(IntPtr handle, bool enabled)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			hwnd.topmost = enabled;
			object obj;
			if (enabled)
			{
				obj = XplatUIX11.XlibLock;
				lock (obj)
				{
					if (hwnd.Mapped)
					{
						this.SendNetWMMessage(hwnd.WholeWindow, XplatUIX11._NET_WM_STATE, (IntPtr)1, XplatUIX11._NET_WM_STATE_ABOVE, IntPtr.Zero);
						return true;
					}
					int[] array = new int[8];
					array[0] = XplatUIX11._NET_WM_STATE_ABOVE.ToInt32();
					XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_STATE, (IntPtr)4, 32, PropertyMode.Replace, array, 1);
					return true;
				}
			}
			obj = XplatUIX11.XlibLock;
			lock (obj)
			{
				if (hwnd.Mapped)
				{
					this.SendNetWMMessage(hwnd.WholeWindow, XplatUIX11._NET_WM_STATE, (IntPtr)0, XplatUIX11._NET_WM_STATE_ABOVE, IntPtr.Zero);
				}
				else
				{
					XplatUIX11.XDeleteProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_STATE);
				}
			}
			return true;
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00083F68 File Offset: 0x00082168
		internal override bool SetOwner(IntPtr handle, IntPtr handle_owner)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			object obj;
			if (handle_owner != IntPtr.Zero)
			{
				Hwnd hwnd2 = Hwnd.ObjectFromHandle(handle_owner);
				obj = XplatUIX11.XlibLock;
				lock (obj)
				{
					int[] array = new int[8];
					array[0] = XplatUIX11._NET_WM_WINDOW_TYPE_NORMAL.ToInt32();
					XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_WINDOW_TYPE, (IntPtr)4, 32, PropertyMode.Replace, array, 1);
					if (hwnd2 != null)
					{
						XplatUIX11.XSetTransientForHint(XplatUIX11.DisplayHandle, hwnd.whole_window, hwnd2.whole_window);
						return true;
					}
					XplatUIX11.XSetTransientForHint(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11.RootWindow);
					return true;
				}
			}
			obj = XplatUIX11.XlibLock;
			lock (obj)
			{
				XplatUIX11.XDeleteProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, (IntPtr)68);
			}
			return true;
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x0008406C File Offset: 0x0008226C
		internal override bool SetVisible(IntPtr handle, bool visible, bool activate)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			hwnd.visible = visible;
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				if (visible)
				{
					this.MapWindow(hwnd, WindowType.Both);
					if (Control.FromHandle(handle) is Form)
					{
						FormWindowState windowState = ((Form)Control.FromHandle(handle)).WindowState;
						if (windowState != FormWindowState.Minimized)
						{
							if (windowState == FormWindowState.Maximized)
							{
								this.SetWindowState(handle, FormWindowState.Maximized);
							}
						}
						else
						{
							this.SetWindowState(handle, FormWindowState.Minimized);
						}
					}
					this.SendMessage(handle, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
				}
				else
				{
					this.UnmapWindow(hwnd, WindowType.Both);
				}
			}
			return true;
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00084118 File Offset: 0x00082318
		internal override void SetWindowMinMax(IntPtr handle, Rectangle maximized, Size min, Size max)
		{
			Control control = Control.FromHandle(handle);
			this.SetWindowMinMax(handle, maximized, min, max, (control != null) ? control.GetCreateParams() : null);
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x00084144 File Offset: 0x00082344
		internal void SetWindowMinMax(IntPtr handle, Rectangle maximized, Size min, Size max, CreateParams cp)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				return;
			}
			min.Width = Math.Max(min.Width, SystemInformation.MinimumWindowSize.Width);
			min.Height = Math.Max(min.Height, SystemInformation.MinimumWindowSize.Height);
			XSizeHints xsizeHints = default(XSizeHints);
			IntPtr intPtr;
			XplatUIX11.XGetWMNormalHints(XplatUIX11.DisplayHandle, hwnd.whole_window, ref xsizeHints, out intPtr);
			if (min != Size.Empty && min.Width > 0 && min.Height > 0)
			{
				if (cp != null)
				{
					min = XplatUIX11.TranslateWindowSizeToXWindowSize(cp, min);
				}
				xsizeHints.flags = (IntPtr)((int)xsizeHints.flags | 16);
				xsizeHints.min_width = min.Width;
				xsizeHints.min_height = min.Height;
			}
			if (max != Size.Empty && max.Width > 0 && max.Height > 0)
			{
				if (cp != null)
				{
					max = XplatUIX11.TranslateWindowSizeToXWindowSize(cp, max);
				}
				xsizeHints.flags = (IntPtr)((int)xsizeHints.flags | 32);
				xsizeHints.max_width = max.Width;
				xsizeHints.max_height = max.Height;
			}
			if (xsizeHints.flags != IntPtr.Zero)
			{
				XplatUIX11.XSetWMNormalHints(XplatUIX11.DisplayHandle, hwnd.whole_window, ref xsizeHints);
			}
			if (maximized != Rectangle.Empty && maximized.Width > 0 && maximized.Height > 0)
			{
				if (cp != null)
				{
					maximized.Size = XplatUIX11.TranslateWindowSizeToXWindowSize(cp);
				}
				xsizeHints.flags = (IntPtr)4;
				xsizeHints.x = maximized.X;
				xsizeHints.y = maximized.Y;
				xsizeHints.width = maximized.Width;
				xsizeHints.height = maximized.Height;
				XplatUIX11.XSetZoomHints(XplatUIX11.DisplayHandle, hwnd.whole_window, ref xsizeHints);
			}
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x00084334 File Offset: 0x00082534
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
					this.MapWindow(hwnd, WindowType.Whole);
				}
				hwnd.zero_sized = false;
			}
			if (width < 1 || height < 1)
			{
				hwnd.zero_sized = true;
				this.UnmapWindow(hwnd, WindowType.Whole);
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
				if (hwnd.fixed_size)
				{
					this.SetWindowMinMax(handle, Rectangle.Empty, new Size(width, height), new Size(width, height));
				}
				object xlibLock = XplatUIX11.XlibLock;
				lock (xlibLock)
				{
					Size size = XplatUIX11.TranslateWindowSizeToXWindowSize(Control.FromHandle(handle).GetCreateParams(), new Size(width, height));
					XplatUIX11.MoveResizeWindow(XplatUIX11.DisplayHandle, hwnd.whole_window, x, y, size.Width, size.Height);
					this.PerformNCCalc(hwnd);
				}
			}
			hwnd.x = x;
			hwnd.y = y;
			hwnd.width = width;
			hwnd.height = height;
			hwnd.ClientRect = Rectangle.Empty;
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x000844C4 File Offset: 0x000826C4
		internal override void SetWindowState(IntPtr handle, FormWindowState state)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			FormWindowState windowState = this.GetWindowState(handle);
			if (windowState == state)
			{
				return;
			}
			switch (state)
			{
			case FormWindowState.Normal:
			{
				object obj = XplatUIX11.XlibLock;
				lock (obj)
				{
					if (windowState == FormWindowState.Minimized)
					{
						this.MapWindow(hwnd, WindowType.Both);
					}
					else if (windowState == FormWindowState.Maximized)
					{
						this.SendNetWMMessage(hwnd.whole_window, XplatUIX11._NET_WM_STATE, (IntPtr)2, XplatUIX11._NET_WM_STATE_MAXIMIZED_HORZ, XplatUIX11._NET_WM_STATE_MAXIMIZED_VERT);
					}
				}
				this.Activate(handle);
				return;
			}
			case FormWindowState.Minimized:
			{
				object obj = XplatUIX11.XlibLock;
				lock (obj)
				{
					if (windowState == FormWindowState.Maximized)
					{
						this.SendNetWMMessage(hwnd.whole_window, XplatUIX11._NET_WM_STATE, (IntPtr)2, XplatUIX11._NET_WM_STATE_MAXIMIZED_HORZ, XplatUIX11._NET_WM_STATE_MAXIMIZED_VERT);
					}
					XplatUIX11.XIconifyWindow(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11.ScreenNo);
				}
				return;
			}
			case FormWindowState.Maximized:
			{
				object obj = XplatUIX11.XlibLock;
				lock (obj)
				{
					if (windowState == FormWindowState.Minimized)
					{
						this.MapWindow(hwnd, WindowType.Both);
					}
					this.SendNetWMMessage(hwnd.whole_window, XplatUIX11._NET_WM_STATE, (IntPtr)1, XplatUIX11._NET_WM_STATE_MAXIMIZED_HORZ, XplatUIX11._NET_WM_STATE_MAXIMIZED_VERT);
				}
				this.Activate(handle);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00084620 File Offset: 0x00082820
		internal override void SetWindowStyle(IntPtr handle, CreateParams cp)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			this.SetHwndStyles(hwnd, cp);
			this.SetWMStyles(hwnd, cp);
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00084644 File Offset: 0x00082844
		internal override void SetWindowTransparency(IntPtr handle, double transparency, Color key)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				return;
			}
			hwnd.opacity = (uint)(4294967295.0 * transparency);
			IntPtr intPtr = (IntPtr)((long)((ulong)hwnd.opacity));
			if (transparency >= 1.0)
			{
				XplatUIX11.XDeleteProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_WINDOW_OPACITY);
				return;
			}
			XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_WINDOW_OPACITY, (IntPtr)6, 32, PropertyMode.Replace, ref intPtr, 1);
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000846C0 File Offset: 0x000828C0
		internal override bool SetZOrder(IntPtr handle, IntPtr after_handle, bool top, bool bottom)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (!hwnd.mapped)
			{
				return false;
			}
			object obj;
			if (top)
			{
				obj = XplatUIX11.XlibLock;
				lock (obj)
				{
					XplatUIX11.XRaiseWindow(XplatUIX11.DisplayHandle, hwnd.whole_window);
				}
				return true;
			}
			if (!bottom)
			{
				Hwnd hwnd2 = null;
				if (after_handle != IntPtr.Zero)
				{
					hwnd2 = Hwnd.ObjectFromHandle(after_handle);
				}
				XWindowChanges xwindowChanges = default(XWindowChanges);
				if (hwnd2 == null)
				{
					int[] array = new int[2];
					array[0] = this.unixtime();
					XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_USER_TIME, (IntPtr)6, 32, PropertyMode.Replace, array, 1);
					XplatUIX11.XRaiseWindow(XplatUIX11.DisplayHandle, hwnd.whole_window);
					this.SendNetWMMessage(hwnd.whole_window, XplatUIX11._NET_ACTIVE_WINDOW, (IntPtr)1, IntPtr.Zero, IntPtr.Zero);
					return true;
				}
				xwindowChanges.sibling = hwnd2.whole_window;
				xwindowChanges.stack_mode = StackMode.Below;
				obj = XplatUIX11.XlibLock;
				lock (obj)
				{
					XplatUIX11.XConfigureWindow(XplatUIX11.DisplayHandle, hwnd.whole_window, ChangeWindowFlags.CWSibling | ChangeWindowFlags.CWStackMode, ref xwindowChanges);
					return false;
				}
			}
			obj = XplatUIX11.XlibLock;
			lock (obj)
			{
				XplatUIX11.XLowerWindow(XplatUIX11.DisplayHandle, hwnd.whole_window);
			}
			return true;
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x00084840 File Offset: 0x00082A40
		internal override object StartLoop(Thread thread)
		{
			return this.ThreadQueue(thread);
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x00058DE5 File Offset: 0x00056FE5
		internal override TransparencySupport SupportsTransparency()
		{
			return TransparencySupport.Set;
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x0008484C File Offset: 0x00082A4C
		internal override bool Text(IntPtr handle, string text)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			object xlibLock = XplatUIX11.XlibLock;
			lock (xlibLock)
			{
				XplatUIX11.XChangeProperty(XplatUIX11.DisplayHandle, hwnd.whole_window, XplatUIX11._NET_WM_NAME, XplatUIX11.UTF8_STRING, 8, PropertyMode.Replace, text, Encoding.UTF8.GetByteCount(text));
				XplatUIX11.XStoreName(XplatUIX11.DisplayHandle, Hwnd.ObjectFromHandle(handle).whole_window, text);
			}
			return true;
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x000848D0 File Offset: 0x00082AD0
		internal override bool TranslateMessage(ref MSG msg)
		{
			return XplatUIX11.Keyboard.TranslateMessage(ref msg);
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x000848E0 File Offset: 0x00082AE0
		internal override void UpdateWindow(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (!hwnd.visible || !hwnd.expose_pending || !hwnd.Mapped)
			{
				return;
			}
			this.SendMessage(handle, Msg.WM_PAINT, IntPtr.Zero, IntPtr.Zero);
			hwnd.Queue.Paint.Remove(hwnd);
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00084934 File Offset: 0x00082B34
		internal override void CreateOffscreenDrawable(IntPtr handle, int width, int height, out object offscreen_drawable)
		{
			IntPtr intPtr;
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			int num6;
			XplatUIX11.XGetGeometry(XplatUIX11.DisplayHandle, handle, out intPtr, out num, out num2, out num3, out num4, out num5, out num6);
			IntPtr intPtr2 = XplatUIX11.XCreatePixmap(XplatUIX11.DisplayHandle, handle, width, height, num6);
			offscreen_drawable = intPtr2;
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00084976 File Offset: 0x00082B76
		internal override void DestroyOffscreenDrawable(object offscreen_drawable)
		{
			XplatUIX11.XFreePixmap(XplatUIX11.DisplayHandle, (IntPtr)offscreen_drawable);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x00084989 File Offset: 0x00082B89
		internal override Graphics GetOffscreenGraphics(object offscreen_drawable)
		{
			return Graphics.FromHwnd((IntPtr)offscreen_drawable);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00084998 File Offset: 0x00082B98
		internal override void BlitFromOffscreen(IntPtr dest_handle, Graphics dest_dc, object offscreen_drawable, Graphics offscreen_dc, Rectangle r)
		{
			XGCValues xgcvalues = default(XGCValues);
			IntPtr intPtr = XplatUIX11.XCreateGC(XplatUIX11.DisplayHandle, dest_handle, IntPtr.Zero, ref xgcvalues);
			XplatUIX11.XCopyArea(XplatUIX11.DisplayHandle, (IntPtr)offscreen_drawable, dest_handle, intPtr, r.X, r.Y, r.Width, r.Height, r.X, r.Y);
			XplatUIX11.XFreeGC(XplatUIX11.DisplayHandle, intPtr);
		}

		// Token: 0x06001AA7 RID: 6823
		[DllImport("libX11")]
		internal static extern IntPtr XOpenDisplay(IntPtr display);

		// Token: 0x06001AA8 RID: 6824
		[DllImport("libX11")]
		internal static extern int XCloseDisplay(IntPtr display);

		// Token: 0x06001AA9 RID: 6825
		[DllImport("libX11")]
		internal static extern IntPtr XSynchronize(IntPtr display, bool onoff);

		// Token: 0x06001AAA RID: 6826
		[DllImport("libX11")]
		internal static extern IntPtr XCreateWindow(IntPtr display, IntPtr parent, int x, int y, int width, int height, int border_width, int depth, int xclass, IntPtr visual, UIntPtr valuemask, ref XSetWindowAttributes attributes);

		// Token: 0x06001AAB RID: 6827
		[DllImport("libX11")]
		internal static extern IntPtr XCreateSimpleWindow(IntPtr display, IntPtr parent, int x, int y, int width, int height, int border_width, UIntPtr border, UIntPtr background);

		// Token: 0x06001AAC RID: 6828
		[DllImport("libX11")]
		internal static extern int XMapWindow(IntPtr display, IntPtr window);

		// Token: 0x06001AAD RID: 6829
		[DllImport("libX11")]
		internal static extern int XMapRaised(IntPtr display, IntPtr window);

		// Token: 0x06001AAE RID: 6830
		[DllImport("libX11")]
		internal static extern int XUnmapWindow(IntPtr display, IntPtr window);

		// Token: 0x06001AAF RID: 6831
		[DllImport("libX11")]
		internal static extern IntPtr XRootWindow(IntPtr display, int screen_number);

		// Token: 0x06001AB0 RID: 6832
		[DllImport("libX11")]
		internal static extern IntPtr XNextEvent(IntPtr display, ref XEvent xevent);

		// Token: 0x06001AB1 RID: 6833
		[DllImport("libX11")]
		internal static extern int XConnectionNumber(IntPtr display);

		// Token: 0x06001AB2 RID: 6834
		[DllImport("libX11")]
		internal static extern int XPending(IntPtr display);

		// Token: 0x06001AB3 RID: 6835
		[DllImport("libX11")]
		internal static extern IntPtr XSelectInput(IntPtr display, IntPtr window, IntPtr mask);

		// Token: 0x06001AB4 RID: 6836
		[DllImport("libX11")]
		internal static extern int XDestroyWindow(IntPtr display, IntPtr window);

		// Token: 0x06001AB5 RID: 6837
		[DllImport("libX11")]
		internal static extern int XReparentWindow(IntPtr display, IntPtr window, IntPtr parent, int x, int y);

		// Token: 0x06001AB6 RID: 6838
		[DllImport("libX11")]
		private static extern int XMoveResizeWindow(IntPtr display, IntPtr window, int x, int y, int width, int height);

		// Token: 0x06001AB7 RID: 6839 RVA: 0x00084A09 File Offset: 0x00082C09
		internal static int MoveResizeWindow(IntPtr display, IntPtr window, int x, int y, int width, int height)
		{
			int num = XplatUIX11.XMoveResizeWindow(display, window, x, y, width, height);
			XplatUIX11.Keyboard.MoveCurrentCaretPos();
			return num;
		}

		// Token: 0x06001AB8 RID: 6840
		[DllImport("libX11")]
		internal static extern int XGetWindowAttributes(IntPtr display, IntPtr window, ref XWindowAttributes attributes);

		// Token: 0x06001AB9 RID: 6841
		[DllImport("libX11")]
		internal static extern int XFlush(IntPtr display);

		// Token: 0x06001ABA RID: 6842
		[DllImport("libX11")]
		internal static extern int XStoreName(IntPtr display, IntPtr window, string window_name);

		// Token: 0x06001ABB RID: 6843
		[DllImport("libX11")]
		internal static extern int XSendEvent(IntPtr display, IntPtr window, bool propagate, IntPtr event_mask, ref XEvent send_event);

		// Token: 0x06001ABC RID: 6844
		[DllImport("libX11")]
		internal static extern int XQueryTree(IntPtr display, IntPtr window, out IntPtr root_return, out IntPtr parent_return, out IntPtr children_return, out int nchildren_return);

		// Token: 0x06001ABD RID: 6845
		[DllImport("libX11")]
		internal static extern int XFree(IntPtr data);

		// Token: 0x06001ABE RID: 6846
		[DllImport("libX11")]
		internal static extern int XRaiseWindow(IntPtr display, IntPtr window);

		// Token: 0x06001ABF RID: 6847
		[DllImport("libX11")]
		internal static extern uint XLowerWindow(IntPtr display, IntPtr window);

		// Token: 0x06001AC0 RID: 6848
		[DllImport("libX11")]
		internal static extern uint XConfigureWindow(IntPtr display, IntPtr window, ChangeWindowFlags value_mask, ref XWindowChanges values);

		// Token: 0x06001AC1 RID: 6849
		[DllImport("libX11")]
		internal static extern IntPtr XInternAtom(IntPtr display, string atom_name, bool only_if_exists);

		// Token: 0x06001AC2 RID: 6850
		[DllImport("libX11")]
		internal static extern int XInternAtoms(IntPtr display, string[] atom_names, int atom_count, bool only_if_exists, IntPtr[] atoms);

		// Token: 0x06001AC3 RID: 6851
		[DllImport("libX11")]
		internal static extern int XSetWMProtocols(IntPtr display, IntPtr window, IntPtr[] protocols, int count);

		// Token: 0x06001AC4 RID: 6852
		[DllImport("libX11")]
		internal static extern int XGrabPointer(IntPtr display, IntPtr window, bool owner_events, EventMask event_mask, GrabMode pointer_mode, GrabMode keyboard_mode, IntPtr confine_to, IntPtr cursor, IntPtr timestamp);

		// Token: 0x06001AC5 RID: 6853
		[DllImport("libX11")]
		internal static extern int XUngrabPointer(IntPtr display, IntPtr timestamp);

		// Token: 0x06001AC6 RID: 6854
		[DllImport("libX11")]
		internal static extern bool XQueryPointer(IntPtr display, IntPtr window, out IntPtr root, out IntPtr child, out int root_x, out int root_y, out int win_x, out int win_y, out int keys_buttons);

		// Token: 0x06001AC7 RID: 6855
		[DllImport("libX11")]
		internal static extern bool XTranslateCoordinates(IntPtr display, IntPtr src_w, IntPtr dest_w, int src_x, int src_y, out int intdest_x_return, out int dest_y_return, out IntPtr child_return);

		// Token: 0x06001AC8 RID: 6856
		[DllImport("libX11")]
		internal static extern bool XGetGeometry(IntPtr display, IntPtr window, out IntPtr root, out int x, out int y, out int width, out int height, out int border_width, out int depth);

		// Token: 0x06001AC9 RID: 6857
		[DllImport("libX11")]
		internal static extern uint XWarpPointer(IntPtr display, IntPtr src_w, IntPtr dest_w, int src_x, int src_y, uint src_width, uint src_height, int dest_x, int dest_y);

		// Token: 0x06001ACA RID: 6858
		[DllImport("libX11")]
		internal static extern int XDefaultScreen(IntPtr display);

		// Token: 0x06001ACB RID: 6859
		[DllImport("libX11")]
		internal static extern IntPtr XDefaultColormap(IntPtr display, int screen_number);

		// Token: 0x06001ACC RID: 6860
		[DllImport("libX11")]
		internal static extern int XAllocColor(IntPtr display, IntPtr Colormap, ref XColor colorcell_def);

		// Token: 0x06001ACD RID: 6861
		[DllImport("libX11")]
		internal static extern int XSetTransientForHint(IntPtr display, IntPtr window, IntPtr prop_window);

		// Token: 0x06001ACE RID: 6862
		[DllImport("libX11")]
		internal static extern int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, PropertyMode mode, ref MotifWmHints data, int nelements);

		// Token: 0x06001ACF RID: 6863
		[DllImport("libX11")]
		internal static extern int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, PropertyMode mode, ref IntPtr value, int nelements);

		// Token: 0x06001AD0 RID: 6864
		[DllImport("libX11")]
		internal static extern int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, PropertyMode mode, int[] data, int nelements);

		// Token: 0x06001AD1 RID: 6865
		[DllImport("libX11")]
		internal static extern int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, PropertyMode mode, IntPtr[] data, int nelements);

		// Token: 0x06001AD2 RID: 6866
		[DllImport("libX11")]
		internal static extern int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, PropertyMode mode, IntPtr atoms, int nelements);

		// Token: 0x06001AD3 RID: 6867
		[DllImport("libX11", CharSet = CharSet.Ansi)]
		internal static extern int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, PropertyMode mode, string text, int text_length);

		// Token: 0x06001AD4 RID: 6868
		[DllImport("libX11")]
		internal static extern int XDeleteProperty(IntPtr display, IntPtr window, IntPtr property);

		// Token: 0x06001AD5 RID: 6869
		[DllImport("libX11")]
		internal static extern IntPtr XCreateGC(IntPtr display, IntPtr window, IntPtr valuemask, ref XGCValues values);

		// Token: 0x06001AD6 RID: 6870
		[DllImport("libX11")]
		internal static extern int XFreeGC(IntPtr display, IntPtr gc);

		// Token: 0x06001AD7 RID: 6871
		[DllImport("libX11")]
		internal static extern int XSetFunction(IntPtr display, IntPtr gc, GXFunction function);

		// Token: 0x06001AD8 RID: 6872
		[DllImport("libX11")]
		internal static extern int XDrawLine(IntPtr display, IntPtr drawable, IntPtr gc, int x1, int y1, int x2, int y2);

		// Token: 0x06001AD9 RID: 6873
		[DllImport("libX11")]
		internal static extern int XDrawRectangle(IntPtr display, IntPtr drawable, IntPtr gc, int x1, int y1, int width, int height);

		// Token: 0x06001ADA RID: 6874
		[DllImport("libX11")]
		internal static extern int XCopyArea(IntPtr display, IntPtr src, IntPtr dest, IntPtr gc, int src_x, int src_y, int width, int height, int dest_x, int dest_y);

		// Token: 0x06001ADB RID: 6875
		[DllImport("libX11")]
		internal static extern int XGetWindowProperty(IntPtr display, IntPtr window, IntPtr atom, IntPtr long_offset, IntPtr long_length, bool delete, IntPtr req_type, out IntPtr actual_type, out int actual_format, out IntPtr nitems, out IntPtr bytes_after, ref IntPtr prop);

		// Token: 0x06001ADC RID: 6876
		[DllImport("libX11")]
		internal static extern int XIconifyWindow(IntPtr display, IntPtr window, int screen_number);

		// Token: 0x06001ADD RID: 6877
		[DllImport("libX11")]
		internal static extern int XDefineCursor(IntPtr display, IntPtr window, IntPtr cursor);

		// Token: 0x06001ADE RID: 6878
		[DllImport("libX11")]
		internal static extern int XUndefineCursor(IntPtr display, IntPtr window);

		// Token: 0x06001ADF RID: 6879
		[DllImport("libX11")]
		internal static extern IntPtr XCreateFontCursor(IntPtr display, CursorFontShape shape);

		// Token: 0x06001AE0 RID: 6880
		[DllImport("libX11")]
		internal static extern IntPtr XCreatePixmapCursor(IntPtr display, IntPtr source, IntPtr mask, ref XColor foreground_color, ref XColor background_color, int x_hot, int y_hot);

		// Token: 0x06001AE1 RID: 6881
		[DllImport("libX11")]
		internal static extern IntPtr XCreatePixmapFromBitmapData(IntPtr display, IntPtr drawable, byte[] data, int width, int height, IntPtr fg, IntPtr bg, int depth);

		// Token: 0x06001AE2 RID: 6882
		[DllImport("libX11")]
		internal static extern IntPtr XCreatePixmap(IntPtr display, IntPtr d, int width, int height, int depth);

		// Token: 0x06001AE3 RID: 6883
		[DllImport("libX11")]
		internal static extern IntPtr XFreePixmap(IntPtr display, IntPtr pixmap);

		// Token: 0x06001AE4 RID: 6884
		[DllImport("libX11")]
		internal static extern int XQueryBestCursor(IntPtr display, IntPtr drawable, int width, int height, out int best_width, out int best_height);

		// Token: 0x06001AE5 RID: 6885
		[DllImport("libX11")]
		internal static extern int XQueryExtension(IntPtr display, string extension_name, ref int major, ref int first_event, ref int first_error);

		// Token: 0x06001AE6 RID: 6886
		[DllImport("libX11")]
		internal static extern IntPtr XWhitePixel(IntPtr display, int screen_no);

		// Token: 0x06001AE7 RID: 6887
		[DllImport("libX11")]
		internal static extern IntPtr XBlackPixel(IntPtr display, int screen_no);

		// Token: 0x06001AE8 RID: 6888
		[DllImport("libX11")]
		internal static extern void XGrabServer(IntPtr display);

		// Token: 0x06001AE9 RID: 6889
		[DllImport("libX11")]
		internal static extern void XUngrabServer(IntPtr display);

		// Token: 0x06001AEA RID: 6890
		[DllImport("libX11")]
		internal static extern void XGetWMNormalHints(IntPtr display, IntPtr window, ref XSizeHints hints, out IntPtr supplied_return);

		// Token: 0x06001AEB RID: 6891
		[DllImport("libX11")]
		internal static extern void XSetWMNormalHints(IntPtr display, IntPtr window, ref XSizeHints hints);

		// Token: 0x06001AEC RID: 6892
		[DllImport("libX11")]
		internal static extern void XSetZoomHints(IntPtr display, IntPtr window, ref XSizeHints hints);

		// Token: 0x06001AED RID: 6893
		[DllImport("libX11")]
		internal static extern void XSetWMHints(IntPtr display, IntPtr window, ref XWMHints wmhints);

		// Token: 0x06001AEE RID: 6894
		[DllImport("libX11")]
		internal static extern IntPtr XSetErrorHandler(XErrorHandler error_handler);

		// Token: 0x06001AEF RID: 6895
		[DllImport("libX11")]
		internal static extern IntPtr XGetErrorText(IntPtr display, byte code, StringBuilder buffer, int length);

		// Token: 0x06001AF0 RID: 6896
		[DllImport("libX11")]
		internal static extern int XInitThreads();

		// Token: 0x06001AF1 RID: 6897
		[DllImport("libX11")]
		internal static extern int XConvertSelection(IntPtr display, IntPtr selection, IntPtr target, IntPtr property, IntPtr requestor, IntPtr time);

		// Token: 0x06001AF2 RID: 6898
		[DllImport("libX11")]
		internal static extern IntPtr XGetSelectionOwner(IntPtr display, IntPtr selection);

		// Token: 0x06001AF3 RID: 6899
		[DllImport("libX11")]
		internal static extern int XSetSelectionOwner(IntPtr display, IntPtr selection, IntPtr owner, IntPtr time);

		// Token: 0x06001AF4 RID: 6900
		[DllImport("libX11")]
		internal static extern int XSetPlaneMask(IntPtr display, IntPtr gc, IntPtr mask);

		// Token: 0x06001AF5 RID: 6901
		[DllImport("libX11")]
		internal static extern int XSetForeground(IntPtr display, IntPtr gc, UIntPtr foreground);

		// Token: 0x06001AF6 RID: 6902
		[DllImport("libX11")]
		internal static extern int XSetBackground(IntPtr display, IntPtr gc, UIntPtr background);

		// Token: 0x06001AF7 RID: 6903
		[DllImport("libX11")]
		internal static extern int XBell(IntPtr display, int percent);

		// Token: 0x06001AF8 RID: 6904
		[DllImport("libX11")]
		internal static extern int XChangeActivePointerGrab(IntPtr display, EventMask event_mask, IntPtr cursor, IntPtr time);

		// Token: 0x06001AF9 RID: 6905
		[DllImport("libX11")]
		internal static extern bool XFilterEvent(ref XEvent xevent, IntPtr window);

		// Token: 0x06001AFA RID: 6906
		[DllImport("libX11")]
		internal static extern void XkbSetDetectableAutoRepeat(IntPtr display, bool detectable, IntPtr supported);

		// Token: 0x06001AFB RID: 6907
		[DllImport("libX11")]
		internal static extern void XPeekEvent(IntPtr display, ref XEvent xevent);

		// Token: 0x06001AFC RID: 6908
		[DllImport("libX11")]
		internal static extern void XIfEvent(IntPtr display, ref XEvent xevent, Delegate event_predicate, IntPtr arg);

		// Token: 0x06001AFD RID: 6909
		[DllImport("libX11")]
		internal static extern void XGetInputFocus(IntPtr display, out IntPtr focus, out IntPtr revert_to);

		// Token: 0x06001AFE RID: 6910
		[DllImport("libgdk-x11-2.0")]
		internal static extern IntPtr gdk_atom_intern(string atomName, bool onlyIfExists);

		// Token: 0x06001AFF RID: 6911
		[DllImport("libgtk-x11-2.0")]
		internal static extern IntPtr gtk_clipboard_get(IntPtr atom);

		// Token: 0x06001B00 RID: 6912
		[DllImport("libgtk-x11-2.0")]
		internal static extern void gtk_clipboard_store(IntPtr clipboard);

		// Token: 0x06001B01 RID: 6913
		[DllImport("libgtk-x11-2.0")]
		internal static extern void gtk_clipboard_set_text(IntPtr clipboard, string text, int len);

		// Token: 0x06001B02 RID: 6914
		[DllImport("libXinerama")]
		internal static extern IntPtr XineramaQueryScreens(IntPtr display, out int number);

		// Token: 0x06001B03 RID: 6915
		[DllImport("libXinerama", EntryPoint = "XineramaIsActive")]
		private static extern bool _XineramaIsActive(IntPtr display);

		// Token: 0x06001B04 RID: 6916 RVA: 0x00084A24 File Offset: 0x00082C24
		internal static bool XineramaIsActive(IntPtr display)
		{
			if (XplatUIX11.XineramaNotInstalled)
			{
				return false;
			}
			bool flag;
			try
			{
				flag = XplatUIX11._XineramaIsActive(display);
			}
			catch (DllNotFoundException)
			{
				XplatUIX11.XineramaNotInstalled = true;
				flag = false;
			}
			return flag;
		}

		// Token: 0x040015FC RID: 5628
		private static volatile XplatUIX11 Instance;

		// Token: 0x040015FD RID: 5629
		private static int RefCount;

		// Token: 0x040015FE RID: 5630
		private static object XlibLock;

		// Token: 0x040015FF RID: 5631
		private static bool themes_enabled;

		// Token: 0x04001600 RID: 5632
		private static IntPtr DisplayHandle;

		// Token: 0x04001601 RID: 5633
		private static int ScreenNo;

		// Token: 0x04001602 RID: 5634
		private static IntPtr DefaultColormap;

		// Token: 0x04001603 RID: 5635
		private static IntPtr CustomVisual;

		// Token: 0x04001604 RID: 5636
		private static IntPtr CustomColormap;

		// Token: 0x04001605 RID: 5637
		private static IntPtr RootWindow;

		// Token: 0x04001606 RID: 5638
		private static IntPtr FosterParent;

		// Token: 0x04001607 RID: 5639
		private static XErrorHandler ErrorHandler;

		// Token: 0x04001608 RID: 5640
		private static bool ErrorExceptions;

		// Token: 0x04001609 RID: 5641
		private int render_major_opcode;

		// Token: 0x0400160A RID: 5642
		private int render_first_event;

		// Token: 0x0400160B RID: 5643
		private int render_first_error;

		// Token: 0x0400160C RID: 5644
		private static IntPtr ClipMagic;

		// Token: 0x0400160D RID: 5645
		private static ClipboardData Clipboard;

		// Token: 0x0400160E RID: 5646
		private static IntPtr PostAtom;

		// Token: 0x0400160F RID: 5647
		private static IntPtr AsyncAtom;

		// Token: 0x04001610 RID: 5648
		private static Hashtable MessageQueues;

		// Token: 0x04001611 RID: 5649
		private static ArrayList unattached_timer_list;

		// Token: 0x04001612 RID: 5650
		private static Pollfd[] pollfds;

		// Token: 0x04001613 RID: 5651
		private static bool wake_waiting;

		// Token: 0x04001614 RID: 5652
		private static object wake_waiting_lock = new object();

		// Token: 0x04001615 RID: 5653
		private static X11Keyboard Keyboard;

		// Token: 0x04001616 RID: 5654
		private static X11Dnd Dnd;

		// Token: 0x04001617 RID: 5655
		private static Socket listen;

		// Token: 0x04001618 RID: 5656
		private static Socket wake;

		// Token: 0x04001619 RID: 5657
		private static Socket wake_receive;

		// Token: 0x0400161A RID: 5658
		private static byte[] network_buffer;

		// Token: 0x0400161B RID: 5659
		private static bool detectable_key_auto_repeat;

		// Token: 0x0400161C RID: 5660
		private static IntPtr ActiveWindow;

		// Token: 0x0400161D RID: 5661
		private static IntPtr FocusWindow;

		// Token: 0x0400161E RID: 5662
		private static Stack ModalWindows;

		// Token: 0x0400161F RID: 5663
		private static IntPtr LastCursorWindow;

		// Token: 0x04001620 RID: 5664
		private static IntPtr LastCursorHandle;

		// Token: 0x04001621 RID: 5665
		private static IntPtr OverrideCursorHandle;

		// Token: 0x04001622 RID: 5666
		private static CaretStruct Caret;

		// Token: 0x04001623 RID: 5667
		private static IntPtr LastPointerWindow;

		// Token: 0x04001624 RID: 5668
		private static IntPtr WM_PROTOCOLS;

		// Token: 0x04001625 RID: 5669
		private static IntPtr WM_DELETE_WINDOW;

		// Token: 0x04001626 RID: 5670
		private static IntPtr WM_TAKE_FOCUS;

		// Token: 0x04001627 RID: 5671
		private static IntPtr _NET_DESKTOP_GEOMETRY;

		// Token: 0x04001628 RID: 5672
		private static IntPtr _NET_CURRENT_DESKTOP;

		// Token: 0x04001629 RID: 5673
		private static IntPtr _NET_ACTIVE_WINDOW;

		// Token: 0x0400162A RID: 5674
		private static IntPtr _NET_WORKAREA;

		// Token: 0x0400162B RID: 5675
		private static IntPtr _NET_WM_MOVERESIZE;

		// Token: 0x0400162C RID: 5676
		private static IntPtr _NET_WM_NAME;

		// Token: 0x0400162D RID: 5677
		private static IntPtr _NET_WM_WINDOW_TYPE;

		// Token: 0x0400162E RID: 5678
		private static IntPtr _NET_WM_STATE;

		// Token: 0x0400162F RID: 5679
		private static IntPtr _NET_WM_ICON;

		// Token: 0x04001630 RID: 5680
		private static IntPtr _NET_WM_USER_TIME;

		// Token: 0x04001631 RID: 5681
		private static IntPtr _NET_FRAME_EXTENTS;

		// Token: 0x04001632 RID: 5682
		private static IntPtr _NET_SYSTEM_TRAY_S;

		// Token: 0x04001633 RID: 5683
		private static IntPtr _NET_SYSTEM_TRAY_OPCODE;

		// Token: 0x04001634 RID: 5684
		private static IntPtr _NET_WM_STATE_MAXIMIZED_HORZ;

		// Token: 0x04001635 RID: 5685
		private static IntPtr _NET_WM_STATE_MAXIMIZED_VERT;

		// Token: 0x04001636 RID: 5686
		private static IntPtr _XEMBED;

		// Token: 0x04001637 RID: 5687
		private static IntPtr _XEMBED_INFO;

		// Token: 0x04001638 RID: 5688
		private static IntPtr _MOTIF_WM_HINTS;

		// Token: 0x04001639 RID: 5689
		private static IntPtr _NET_WM_STATE_SKIP_TASKBAR;

		// Token: 0x0400163A RID: 5690
		private static IntPtr _NET_WM_STATE_ABOVE;

		// Token: 0x0400163B RID: 5691
		private static IntPtr _NET_WM_STATE_MODAL;

		// Token: 0x0400163C RID: 5692
		private static IntPtr _NET_WM_STATE_HIDDEN;

		// Token: 0x0400163D RID: 5693
		private static IntPtr _NET_WM_CONTEXT_HELP;

		// Token: 0x0400163E RID: 5694
		private static IntPtr _NET_WM_WINDOW_OPACITY;

		// Token: 0x0400163F RID: 5695
		private static IntPtr _NET_WM_WINDOW_TYPE_UTILITY;

		// Token: 0x04001640 RID: 5696
		private static IntPtr _NET_WM_WINDOW_TYPE_NORMAL;

		// Token: 0x04001641 RID: 5697
		private static IntPtr CLIPBOARD;

		// Token: 0x04001642 RID: 5698
		private static IntPtr PRIMARY;

		// Token: 0x04001643 RID: 5699
		private static IntPtr OEMTEXT;

		// Token: 0x04001644 RID: 5700
		private static IntPtr UTF8_STRING;

		// Token: 0x04001645 RID: 5701
		private static IntPtr UTF16_STRING;

		// Token: 0x04001646 RID: 5702
		private static IntPtr RICHTEXTFORMAT;

		// Token: 0x04001647 RID: 5703
		private static IntPtr TARGETS;

		// Token: 0x04001648 RID: 5704
		private static HoverStruct HoverState;

		// Token: 0x04001649 RID: 5705
		private static ClickStruct ClickPending;

		// Token: 0x0400164A RID: 5706
		private static GrabStruct Grab;

		// Token: 0x0400164B RID: 5707
		private Point mouse_position;

		// Token: 0x0400164C RID: 5708
		internal static MouseButtons MouseState;

		// Token: 0x0400164D RID: 5709
		internal static bool in_doevents;

		// Token: 0x0400164E RID: 5710
		private static int DoubleClickInterval;

		// Token: 0x0400164F RID: 5711
		private static readonly object lockobj = new object();

		// Token: 0x04001650 RID: 5712
		private static Hashtable messageHold;

		// Token: 0x04001651 RID: 5713
		[CompilerGenerated]
		private EventHandler Idle;

		// Token: 0x04001652 RID: 5714
		private static bool XineramaNotInstalled;

		// Token: 0x020002CA RID: 714
		internal class XException : ApplicationException
		{
			// Token: 0x06001B06 RID: 6918 RVA: 0x00084A76 File Offset: 0x00082C76
			public XException(IntPtr Display, IntPtr ResourceID, IntPtr Serial, byte ErrorCode, XRequest RequestCode, byte MinorCode)
			{
				this.Display = Display;
				this.ResourceID = ResourceID;
				this.Serial = Serial;
				this.RequestCode = RequestCode;
				this.ErrorCode = ErrorCode;
				this.MinorCode = MinorCode;
			}

			// Token: 0x17000660 RID: 1632
			// (get) Token: 0x06001B07 RID: 6919 RVA: 0x00084AAB File Offset: 0x00082CAB
			public override string Message
			{
				get
				{
					return XplatUIX11.XException.GetMessage(this.Display, this.ResourceID, this.Serial, this.ErrorCode, this.RequestCode, this.MinorCode);
				}
			}

			// Token: 0x06001B08 RID: 6920 RVA: 0x00084AD8 File Offset: 0x00082CD8
			public static string GetMessage(IntPtr Display, IntPtr ResourceID, IntPtr Serial, byte ErrorCode, XRequest RequestCode, byte MinorCode)
			{
				StringBuilder stringBuilder = new StringBuilder(160);
				XplatUIX11.XGetErrorText(Display, ErrorCode, stringBuilder, stringBuilder.Capacity);
				string text = stringBuilder.ToString();
				Hwnd hwnd = Hwnd.ObjectFromHandle(ResourceID);
				string text2;
				string text3;
				if (hwnd != null)
				{
					text2 = hwnd.ToString();
					Control control = Control.FromHandle(hwnd.Handle);
					if (control != null)
					{
						text3 = control.ToString();
					}
					else
					{
						text3 = string.Format("<handle {0:X} non-existant>", hwnd.Handle.ToInt32());
					}
				}
				else
				{
					text2 = "<null>";
					text3 = "<null>";
				}
				return string.Format("\n  Error: {0}\n  Request:     {1:D} ({2})\n  Resource ID: 0x{3:X}\n  Serial:      {4}\n  Hwnd:        {5}\n  Control:     {6}", new object[]
				{
					text,
					RequestCode,
					MinorCode,
					ResourceID.ToInt32(),
					Serial,
					text2,
					text3
				});
			}

			// Token: 0x04001653 RID: 5715
			private IntPtr Display;

			// Token: 0x04001654 RID: 5716
			private IntPtr ResourceID;

			// Token: 0x04001655 RID: 5717
			private IntPtr Serial;

			// Token: 0x04001656 RID: 5718
			private XRequest RequestCode;

			// Token: 0x04001657 RID: 5719
			private byte ErrorCode;

			// Token: 0x04001658 RID: 5720
			private byte MinorCode;
		}

		// Token: 0x020002CB RID: 715
		// (Invoke) Token: 0x06001B0A RID: 6922
		private delegate bool EventPredicate(IntPtr display, ref XEvent xevent, IntPtr arg);

		// Token: 0x020002CC RID: 716
		// (Invoke) Token: 0x06001B0C RID: 6924
		private delegate IntPtr WndProcDelegate(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam);
	}
}
