using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x020002AF RID: 687
	internal class XplatUIWin32 : XplatUIDriver
	{
		// Token: 0x06001914 RID: 6420 RVA: 0x000790E8 File Offset: 0x000772E8
		private XplatUIWin32()
		{
			XplatUIWin32.ref_count = 0;
			XplatUIWin32.mouse_state = MouseButtons.None;
			XplatUIWin32.mouse_position = Point.Empty;
			XplatUIWin32.grab_confined = false;
			XplatUIWin32.grab_area = Rectangle.Empty;
			XplatUIWin32.message_queue = new Queue();
			XplatUIWin32.themes_enabled = false;
			XplatUIWin32.wnd_proc = new XplatUIDriver.WndProc(this.InternalWndProc);
			XplatUIWin32.FosterParentLast = IntPtr.Zero;
			XplatUIWin32.scroll_height = XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYHSCROLL);
			XplatUIWin32.scroll_width = XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXVSCROLL);
			this.timer_list = new Hashtable();
			this.registered_classes = new Hashtable();
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00079178 File Offset: 0x00077378
		private IntPtr GetFosterParent()
		{
			if (!XplatUIWin32.IsWindow(XplatUIWin32.FosterParentLast))
			{
				XplatUIWin32.FosterParentLast = XplatUIWin32.Win32CreateWindow(WindowExStyles.WS_EX_TOOLWINDOW, "static", "Foster Parent Window", WindowStyles.WS_OVERLAPPEDWINDOW, 0, 0, 0, 0, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
				if (XplatUIWin32.FosterParentLast == IntPtr.Zero)
				{
					XplatUIWin32.Win32MessageBox(IntPtr.Zero, "Could not create foster window, win32 error " + XplatUIWin32.Win32GetLastError().ToString(), "Oops", 0U);
				}
			}
			return XplatUIWin32.FosterParentLast;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00079208 File Offset: 0x00077408
		private string RegisterWindowClass(int classStyle)
		{
			Hashtable hashtable = this.registered_classes;
			string text;
			lock (hashtable)
			{
				text = (string)this.registered_classes[classStyle];
				if (text != null)
				{
					return text;
				}
				text = string.Format("Mono.WinForms.{0}.{1}", Thread.GetDomainID().ToString(), classStyle);
				XplatUIWin32.WNDCLASS wndclass;
				wndclass.style = classStyle;
				wndclass.lpfnWndProc = XplatUIWin32.wnd_proc;
				wndclass.cbClsExtra = 0;
				wndclass.cbWndExtra = 0;
				wndclass.hbrBackground = (IntPtr)6;
				wndclass.hCursor = XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
				wndclass.hIcon = IntPtr.Zero;
				wndclass.hInstance = IntPtr.Zero;
				wndclass.lpszClassName = text;
				wndclass.lpszMenuName = "";
				if (!XplatUIWin32.Win32RegisterClass(ref wndclass))
				{
					XplatUIWin32.Win32MessageBox(IntPtr.Zero, "Could not register the window class, win32 error " + XplatUIWin32.Win32GetLastError().ToString(), "Oops", 0U);
				}
				this.registered_classes[classStyle] = text;
			}
			return text;
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0007934C File Offset: 0x0007754C
		private static bool RetrieveMessage(ref MSG msg)
		{
			if (XplatUIWin32.message_queue.Count == 0)
			{
				return false;
			}
			MSG msg2 = (MSG)XplatUIWin32.message_queue.Dequeue();
			msg = msg2;
			return true;
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00079380 File Offset: 0x00077580
		private static bool StoreMessage(ref MSG msg)
		{
			MSG msg2 = default(MSG);
			msg2 = msg;
			XplatUIWin32.message_queue.Enqueue(msg2);
			return true;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x000793AD File Offset: 0x000775AD
		internal static string AnsiToString(IntPtr ansi_data)
		{
			return Marshal.PtrToStringAnsi(ansi_data);
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x000793B5 File Offset: 0x000775B5
		internal static string UnicodeToString(IntPtr unicode_data)
		{
			return Marshal.PtrToStringUni(unicode_data);
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x000793C0 File Offset: 0x000775C0
		internal static Image DIBtoImage(IntPtr dib_data)
		{
			BITMAPINFOHEADER bitmapinfoheader = (BITMAPINFOHEADER)Marshal.PtrToStructure(dib_data, typeof(BITMAPINFOHEADER));
			int num = (int)bitmapinfoheader.biClrUsed;
			if (num == 0 && bitmapinfoheader.biBitCount < 24)
			{
				num = 1 << (int)bitmapinfoheader.biBitCount;
			}
			uint biSizeImage = bitmapinfoheader.biSizeImage;
			ushort biBitCount = bitmapinfoheader.biBitCount;
			Bitmap bitmap;
			int[] array;
			if (biBitCount <= 4)
			{
				if (biBitCount == 1)
				{
					bitmap = new Bitmap(bitmapinfoheader.biWidth, bitmapinfoheader.biHeight, PixelFormat.Format1bppIndexed);
					array = new int[2];
					goto IL_0117;
				}
				if (biBitCount == 4)
				{
					bitmap = new Bitmap(bitmapinfoheader.biWidth, bitmapinfoheader.biHeight, PixelFormat.Format4bppIndexed);
					array = new int[16];
					goto IL_0117;
				}
			}
			else
			{
				if (biBitCount == 8)
				{
					bitmap = new Bitmap(bitmapinfoheader.biWidth, bitmapinfoheader.biHeight, PixelFormat.Format8bppIndexed);
					array = new int[256];
					goto IL_0117;
				}
				if (biBitCount == 24 || biBitCount == 32)
				{
					bitmap = new Bitmap(bitmapinfoheader.biWidth, bitmapinfoheader.biHeight, PixelFormat.Format32bppArgb);
					array = new int[0];
					goto IL_0117;
				}
			}
			throw new Exception("Unexpected number of bits:" + bitmapinfoheader.biBitCount.ToString());
			IL_0117:
			if (bitmapinfoheader.biBitCount < 24)
			{
				ColorPalette palette = bitmap.Palette;
				Marshal.Copy((IntPtr)((int)dib_data + Marshal.SizeOf(typeof(BITMAPINFOHEADER))), array, 0, array.Length);
				for (int i = 0; i < num; i++)
				{
					palette.Entries[i] = Color.FromArgb(array[i] | -16777216);
				}
				bitmap.Palette = palette;
			}
			int num2 = ((bitmapinfoheader.biWidth * (int)bitmapinfoheader.biBitCount + 31) & -32) >> 3;
			BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
			byte[] array2 = new byte[num2];
			for (int j = 0; j < bitmapinfoheader.biHeight; j++)
			{
				Marshal.Copy((IntPtr)((int)dib_data + Marshal.SizeOf(typeof(BITMAPINFOHEADER)) + array.Length * 4 + num2 * j), array2, 0, num2);
				Marshal.Copy(array2, 0, (IntPtr)((int)bitmapData.Scan0 + bitmapData.Stride * (bitmapinfoheader.biHeight - 1 - j)), array2.Length);
			}
			bitmap.UnlockBits(bitmapData);
			return bitmap;
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00079614 File Offset: 0x00077814
		internal static byte[] ImageToDIB(Image image)
		{
			MemoryStream memoryStream = new MemoryStream();
			image.Save(memoryStream, ImageFormat.Bmp);
			byte[] buffer = memoryStream.GetBuffer();
			byte[] array = new byte[buffer.Length];
			Array.Copy(buffer, 14, array, 0, buffer.Length - 14);
			return array;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x00079654 File Offset: 0x00077854
		internal static IntPtr DupGlobalMem(IntPtr mem)
		{
			uint num = XplatUIWin32.Win32GlobalSize(mem);
			IntPtr intPtr = XplatUIWin32.Win32GlobalLock(mem);
			IntPtr intPtr2 = XplatUIWin32.Win32GlobalAlloc(XplatUIWin32.GAllocFlags.GMEM_MOVEABLE, (int)num);
			XplatUIWin32.Win32CopyMemory(XplatUIWin32.Win32GlobalLock(intPtr2), intPtr, (int)num);
			XplatUIWin32.Win32GlobalUnlock(mem);
			XplatUIWin32.Win32GlobalUnlock(intPtr2);
			return intPtr2;
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x00079691 File Offset: 0x00077891
		public override Size MenuButtonSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXMENUSIZE), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYMENUSIZE));
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x000796A8 File Offset: 0x000778A8
		internal override Keys ModifierKeys
		{
			get
			{
				Keys keys = Keys.None;
				if (((int)XplatUIWin32.Win32GetKeyState(VirtualKeys.VK_SHIFT) & 32768) != 0)
				{
					keys |= Keys.Shift;
				}
				if (((int)XplatUIWin32.Win32GetKeyState(VirtualKeys.VK_CONTROL) & 32768) != 0)
				{
					keys |= Keys.Control;
				}
				if (((int)XplatUIWin32.Win32GetKeyState(VirtualKeys.VK_MENU) & 32768) != 0)
				{
					keys |= Keys.Alt;
				}
				return keys;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x000796FD File Offset: 0x000778FD
		internal override Point MousePosition
		{
			get
			{
				return XplatUIWin32.mouse_position;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x00079704 File Offset: 0x00077904
		internal override int HorizontalScrollBarHeight
		{
			get
			{
				return XplatUIWin32.scroll_height;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x00002D70 File Offset: 0x00000F70
		internal override bool UserClipWontExposeParent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x0007970B File Offset: 0x0007790B
		internal override int VerticalScrollBarWidth
		{
			get
			{
				return XplatUIWin32.scroll_width;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x00079712 File Offset: 0x00077912
		internal override int MenuHeight
		{
			get
			{
				return XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYMENU);
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x0007971B File Offset: 0x0007791B
		internal override Size Border3DSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXEDGE), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYEDGE));
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x00079730 File Offset: 0x00077930
		internal override Size BorderSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXBORDER), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYBORDER));
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x00079743 File Offset: 0x00077943
		internal override Size CaptionButtonSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXSIZE), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYSIZE));
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x00079758 File Offset: 0x00077958
		internal override int CaptionHeight
		{
			get
			{
				return XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYCAPTION);
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x00079760 File Offset: 0x00077960
		internal override Size DragSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXDRAG), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYDRAG));
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x00079775 File Offset: 0x00077975
		internal override int DoubleClickTime
		{
			get
			{
				return XplatUIWin32.Win32GetDoubleClickTime();
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x0007977C File Offset: 0x0007797C
		internal override Size FrameBorderSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXFRAME), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYFRAME));
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x00079794 File Offset: 0x00077994
		internal override bool MenuAccessKeysUnderlined
		{
			get
			{
				int num = 0;
				XplatUIWin32.Win32SystemParametersInfo(XplatUIWin32.SPIAction.SPI_GETKEYBOARDCUES, 0U, ref num, 0U);
				return num != 0;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x000797B6 File Offset: 0x000779B6
		internal override Size MinimizedWindowSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXMINIMIZED), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYMINIMIZED));
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x000797CB File Offset: 0x000779CB
		internal override Size MinimumWindowSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXMIN), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYMIN));
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x000797E0 File Offset: 0x000779E0
		internal override Size MinWindowTrackSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXMINTRACK), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYMINTRACK));
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x000797F5 File Offset: 0x000779F5
		internal override Rectangle VirtualScreen
		{
			get
			{
				return new Rectangle(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_XVIRTUALSCREEN), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_YVIRTUALSCREEN), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXVIRTUALSCREEN), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYVIRTUALSCREEN));
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001931 RID: 6449 RVA: 0x00079818 File Offset: 0x00077A18
		internal override Rectangle WorkingArea
		{
			get
			{
				XplatUIWin32.RECT rect = default(XplatUIWin32.RECT);
				XplatUIWin32.Win32SystemParametersInfo(XplatUIWin32.SPIAction.SPI_GETWORKAREA, 0U, ref rect, 0U);
				return new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x00003C7A File Offset: 0x00001E7A
		[MonoTODO]
		internal override Screen[] AllScreens
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x00079864 File Offset: 0x00077A64
		internal override bool ThemesEnabled
		{
			get
			{
				return XplatUIWin32.themes_enabled;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x00002D70 File Offset: 0x00000F70
		internal override bool RequiresPositiveClientAreaSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x0007986B File Offset: 0x00077A6B
		public override int ToolWindowCaptionHeight
		{
			get
			{
				return XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYSMCAPTION);
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x00079874 File Offset: 0x00077A74
		public override Size ToolWindowCaptionButtonSize
		{
			get
			{
				return new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXSMSIZE), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CYSMSIZE));
			}
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00079889 File Offset: 0x00077A89
		public static XplatUIWin32 GetInstance()
		{
			if (XplatUIWin32.instance == null)
			{
				XplatUIWin32.instance = new XplatUIWin32();
			}
			XplatUIWin32.ref_count++;
			return XplatUIWin32.instance;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x0000D682 File Offset: 0x0000B882
		internal override IntPtr InitializeDriver()
		{
			return IntPtr.Zero;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x000798AD File Offset: 0x00077AAD
		private string GetSoundAlias(AlertType alert)
		{
			switch (alert)
			{
			case AlertType.Error:
				return "SystemHand";
			case AlertType.Question:
				return "SystemQuestion";
			case AlertType.Warning:
				return "SystemExclamation";
			case AlertType.Information:
				return "SystemAsterisk";
			default:
				return "SystemDefault";
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x000798E6 File Offset: 0x00077AE6
		internal override void AudibleAlert(AlertType alert)
		{
			XplatUIWin32.Win32PlaySound(this.GetSoundAlias(alert), IntPtr.Zero, (XplatUIWin32.SndFlags)1122321);
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00079900 File Offset: 0x00077B00
		internal override void GetDisplaySize(out Size size)
		{
			XplatUIWin32.RECT rect;
			XplatUIWin32.Win32GetWindowRect(XplatUIWin32.Win32GetDesktopWindow(), out rect);
			size = new Size(rect.right - rect.left, rect.bottom - rect.top);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00079940 File Offset: 0x00077B40
		internal override IntPtr CreateWindow(CreateParams cp)
		{
			Hwnd hwnd = new Hwnd();
			IntPtr intPtr = cp.Parent;
			if (intPtr == IntPtr.Zero && (cp.Style & 1073741824) != 0)
			{
				intPtr = this.GetFosterParent();
			}
			if ((cp.Style & -1073741824) == 0 && (cp.ExStyle & 262144) == 0)
			{
				intPtr = this.GetFosterParent();
			}
			Point nextStackedFormLocation;
			if (cp.HasWindowManager)
			{
				nextStackedFormLocation = Hwnd.GetNextStackedFormLocation(cp, Hwnd.ObjectFromHandle(cp.Parent));
			}
			else
			{
				nextStackedFormLocation = new Point(cp.X, cp.Y);
			}
			string text = this.RegisterWindowClass(cp.ClassStyle);
			this.HwndCreating = hwnd;
			if ((cp.WindowExStyle & WindowExStyles.WS_EX_MDICHILD) == WindowExStyles.WS_EX_MDICHILD)
			{
				cp.WindowExStyle ^= WindowExStyles.WS_EX_MDICHILD;
			}
			IntPtr intPtr2 = XplatUIWin32.Win32CreateWindow(cp.WindowExStyle, text, cp.Caption, cp.WindowStyle, nextStackedFormLocation.X, nextStackedFormLocation.Y, cp.Width, cp.Height, intPtr, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			this.HwndCreating = null;
			if (intPtr2 == IntPtr.Zero)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				XplatUIWin32.Win32MessageBox(IntPtr.Zero, "Error : " + lastWin32Error.ToString(), "Failed to create window, class '" + cp.ClassName + "'", 0U);
			}
			hwnd.ClientWindow = intPtr2;
			hwnd.Mapped = true;
			XplatUIWin32.Win32SetWindowLong(intPtr2, XplatUIWin32.WindowLong.GWL_USERDATA, (uint)ThemeEngine.Current.DefaultControlBackColor.ToArgb());
			return intPtr2;
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00079ABB File Offset: 0x00077CBB
		internal override void DestroyWindow(IntPtr handle)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			XplatUIWin32.Win32DestroyWindow(handle);
			hwnd.Dispose();
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override void SetWindowMinMax(IntPtr handle, Rectangle maximized, Size min, Size max)
		{
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00079AD0 File Offset: 0x00077CD0
		internal override FormWindowState GetWindowState(IntPtr handle)
		{
			uint num = XplatUIWin32.Win32GetWindowLong(handle, XplatUIWin32.WindowLong.GWL_STYLE);
			if ((num & 16777216U) != 0U)
			{
				return FormWindowState.Maximized;
			}
			if ((num & 536870912U) != 0U)
			{
				return FormWindowState.Minimized;
			}
			return FormWindowState.Normal;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00079AFD File Offset: 0x00077CFD
		internal override void SetWindowState(IntPtr hwnd, FormWindowState state)
		{
			switch (state)
			{
			case FormWindowState.Normal:
				XplatUIWin32.Win32ShowWindow(hwnd, XplatUIWin32.WindowPlacementFlags.SW_RESTORE);
				return;
			case FormWindowState.Minimized:
				XplatUIWin32.Win32ShowWindow(hwnd, XplatUIWin32.WindowPlacementFlags.SW_MINIMIZE);
				return;
			case FormWindowState.Maximized:
				XplatUIWin32.Win32ShowWindow(hwnd, XplatUIWin32.WindowPlacementFlags.SW_SHOWMAXIMIZED);
				return;
			default:
				return;
			}
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00079B2D File Offset: 0x00077D2D
		internal override void SetWindowStyle(IntPtr handle, CreateParams cp)
		{
			XplatUIWin32.Win32SetWindowLong(handle, XplatUIWin32.WindowLong.GWL_STYLE, (uint)cp.Style);
			XplatUIWin32.Win32SetWindowLong(handle, XplatUIWin32.WindowLong.GWL_EXSTYLE, (uint)cp.ExStyle);
			if (cp.control is Form)
			{
				XplatUI.RequestNCRecalc(handle);
			}
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00079B60 File Offset: 0x00077D60
		internal override void SetWindowTransparency(IntPtr handle, double transparency, Color key)
		{
			XplatUIWin32.LayeredWindowAttributes layeredWindowAttributes = XplatUIWin32.LayeredWindowAttributes.LWA_ALPHA;
			byte b = (byte)(transparency * 255.0);
			XplatUIWin32.COLORREF colorref = default(XplatUIWin32.COLORREF);
			if (key != Color.Empty)
			{
				colorref.R = key.R;
				colorref.G = key.G;
				colorref.B = key.B;
				layeredWindowAttributes |= XplatUIWin32.LayeredWindowAttributes.LWA_COLORKEY;
			}
			XplatUIWin32.RECT rect;
			rect.right = 1000;
			rect.bottom = 1000;
			XplatUIWin32.Win32SetLayeredWindowAttributes(handle, colorref, b, layeredWindowAttributes);
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00079BE4 File Offset: 0x00077DE4
		internal override TransparencySupport SupportsTransparency()
		{
			if (this.queried_transparency_support)
			{
				return this.support;
			}
			this.support = TransparencySupport.None;
			bool flag = true;
			try
			{
				XplatUIWin32.Win32SetLayeredWindowAttributes(IntPtr.Zero, default(XplatUIWin32.COLORREF), byte.MaxValue, XplatUIWin32.LayeredWindowAttributes.LWA_ALPHA);
			}
			catch (EntryPointNotFoundException)
			{
				flag = false;
			}
			catch
			{
			}
			if (flag)
			{
				this.support |= TransparencySupport.Set;
			}
			flag = true;
			try
			{
				XplatUIWin32.COLORREF colorref;
				byte b;
				XplatUIWin32.LayeredWindowAttributes layeredWindowAttributes;
				XplatUIWin32.Win32GetLayeredWindowAttributes(IntPtr.Zero, out colorref, out b, out layeredWindowAttributes);
			}
			catch (EntryPointNotFoundException)
			{
				flag = false;
			}
			catch
			{
			}
			if (flag)
			{
				this.support |= TransparencySupport.Get;
			}
			this.queried_transparency_support = true;
			return this.support;
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00079CB0 File Offset: 0x00077EB0
		internal override void UpdateWindow(IntPtr handle)
		{
			XplatUIWin32.Win32UpdateWindow(handle);
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00079CBC File Offset: 0x00077EBC
		internal override PaintEventArgs PaintEventStart(ref Message msg, IntPtr handle, bool client)
		{
			Rectangle rectangle = default(Rectangle);
			XplatUIWin32.RECT rect = default(XplatUIWin32.RECT);
			XplatUIWin32.PAINTSTRUCT paintstruct = default(XplatUIWin32.PAINTSTRUCT);
			Hwnd hwnd = Hwnd.ObjectFromHandle(msg.HWnd);
			IntPtr intPtr;
			if (client)
			{
				if (XplatUIWin32.Win32GetUpdateRect(msg.HWnd, ref rect, false))
				{
					if (handle != msg.HWnd)
					{
						XplatUIWin32.Win32GetClientRect(msg.HWnd, out rect);
						XplatUIWin32.Win32ValidateRect(msg.HWnd, ref rect);
						intPtr = XplatUIWin32.Win32GetDC(handle);
					}
					else
					{
						intPtr = XplatUIWin32.Win32BeginPaint(handle, ref paintstruct);
						rect = paintstruct.rcPaint;
					}
				}
				else
				{
					intPtr = XplatUIWin32.Win32GetDC(handle);
				}
				rectangle = rect.ToRectangle();
			}
			else
			{
				intPtr = XplatUIWin32.Win32GetWindowDC(handle);
				XplatUIWin32.Win32GetWindowRect(handle, out rect);
				rectangle = new Rectangle(0, 0, rect.Width, rect.Height);
			}
			if (paintstruct.hdc != IntPtr.Zero)
			{
				hwnd.drawing_stack.Push(paintstruct);
			}
			else
			{
				hwnd.drawing_stack.Push(intPtr);
			}
			Graphics graphics = Graphics.FromHdc(intPtr);
			hwnd.drawing_stack.Push(graphics);
			return new PaintEventArgs(graphics, rectangle);
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00079DD4 File Offset: 0x00077FD4
		internal override void PaintEventEnd(ref Message m, IntPtr handle, bool client)
		{
			Hwnd hwnd = Hwnd.ObjectFromHandle(m.HWnd);
			((Graphics)hwnd.drawing_stack.Pop()).Dispose();
			object obj = hwnd.drawing_stack.Pop();
			if (obj is IntPtr)
			{
				IntPtr intPtr = (IntPtr)obj;
				XplatUIWin32.Win32ReleaseDC(handle, intPtr);
				return;
			}
			if (obj is XplatUIWin32.PAINTSTRUCT)
			{
				XplatUIWin32.PAINTSTRUCT paintstruct = (XplatUIWin32.PAINTSTRUCT)obj;
				XplatUIWin32.Win32EndPaint(handle, ref paintstruct);
			}
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00079E3C File Offset: 0x0007803C
		internal override void SetWindowPos(IntPtr handle, int x, int y, int width, int height)
		{
			XplatUIWin32.Win32MoveWindow(handle, x, y, width, height, true);
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00079E4C File Offset: 0x0007804C
		internal override void GetWindowPos(IntPtr handle, bool is_toplevel, out int x, out int y, out int width, out int height, out int client_width, out int client_height)
		{
			XplatUIWin32.RECT rect;
			XplatUIWin32.Win32GetWindowRect(handle, out rect);
			width = rect.right - rect.left;
			height = rect.bottom - rect.top;
			POINT point;
			point.x = rect.left;
			point.y = rect.top;
			IntPtr intPtr = XplatUIWin32.Win32GetAncestor(handle, XplatUIWin32.AncestorType.GA_PARENT);
			if (intPtr != IntPtr.Zero && intPtr != XplatUIWin32.Win32GetDesktopWindow())
			{
				XplatUIWin32.Win32ScreenToClient(intPtr, ref point);
			}
			x = point.x;
			y = point.y;
			XplatUIWin32.Win32GetClientRect(handle, out rect);
			client_width = rect.right - rect.left;
			client_height = rect.bottom - rect.top;
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00079F04 File Offset: 0x00078104
		internal override void Activate(IntPtr handle)
		{
			XplatUIWin32.Win32SetActiveWindow(handle);
			Hashtable hashtable = this.timer_list;
			lock (hashtable)
			{
				foreach (object obj in this.timer_list.Values)
				{
					Timer timer = (Timer)obj;
					if (timer.Enabled && timer.window == IntPtr.Zero)
					{
						timer.window = handle;
						int hashCode = timer.GetHashCode();
						XplatUIWin32.Win32SetTimer(handle, hashCode, (uint)timer.Interval, IntPtr.Zero);
					}
				}
			}
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00079FCC File Offset: 0x000781CC
		internal override void Invalidate(IntPtr handle, Rectangle rc, bool clear)
		{
			XplatUIWin32.RECT rect;
			rect.left = rc.Left;
			rect.top = rc.Top;
			rect.right = rc.Right;
			rect.bottom = rc.Bottom;
			XplatUIWin32.Win32InvalidateRect(handle, ref rect, clear);
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0007A01B File Offset: 0x0007821B
		internal override void InvalidateNC(IntPtr handle)
		{
			XplatUIWin32.Win32SetWindowPos(handle, IntPtr.Zero, 0, 0, 0, 0, XplatUIWin32.SetWindowPosFlags.SWP_DRAWFRAME | XplatUIWin32.SetWindowPosFlags.SWP_NOACTIVATE | XplatUIWin32.SetWindowPosFlags.SWP_NOMOVE | XplatUIWin32.SetWindowPosFlags.SWP_NOSIZE | XplatUIWin32.SetWindowPosFlags.SWP_NOZORDER);
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0007A02F File Offset: 0x0007822F
		private IntPtr InternalWndProc(IntPtr hWnd, Msg msg, IntPtr wParam, IntPtr lParam)
		{
			if (this.HwndCreating != null && this.HwndCreating.ClientWindow == IntPtr.Zero)
			{
				this.HwndCreating.ClientWindow = hWnd;
			}
			return NativeWindow.WndProc(hWnd, msg, wParam, lParam);
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0007A066 File Offset: 0x00078266
		internal override IntPtr DefWndProc(ref Message msg)
		{
			msg.Result = XplatUIWin32.Win32DefWindowProc(msg.HWnd, (Msg)msg.Msg, msg.WParam, msg.LParam);
			return msg.Result;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0007A094 File Offset: 0x00078294
		internal override void DoEvents()
		{
			MSG msg = default(MSG);
			while (this.GetMessage(ref msg, IntPtr.Zero, 0, 0, false))
			{
				Message message = Message.Create(msg.hwnd, (int)msg.message, msg.wParam, msg.lParam);
				if (!Application.FilterMessage(ref message))
				{
					XplatUI.TranslateMessage(ref msg);
					XplatUI.DispatchMessage(ref msg);
				}
			}
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0007A0F4 File Offset: 0x000782F4
		internal override bool PeekMessage(object queue_id, ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax, uint flags)
		{
			return XplatUIWin32.Win32PeekMessage(ref msg, hWnd, wFilterMin, wFilterMax, flags);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0007A103 File Offset: 0x00078303
		internal override void PostQuitMessage(int exitCode)
		{
			XplatUIWin32.Win32PostQuitMessage(exitCode);
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0007A10C File Offset: 0x0007830C
		internal override void RequestAdditionalWM_NCMessages(IntPtr hwnd, bool hover, bool leave)
		{
			if (XplatUIWin32.wm_nc_registered == null)
			{
				XplatUIWin32.wm_nc_registered = new Hashtable();
			}
			XplatUIWin32.TMEFlags tmeflags = XplatUIWin32.TMEFlags.TME_NONCLIENT;
			if (hover)
			{
				tmeflags |= XplatUIWin32.TMEFlags.TME_HOVER;
			}
			if (leave)
			{
				tmeflags |= XplatUIWin32.TMEFlags.TME_LEAVE;
			}
			if (tmeflags == XplatUIWin32.TMEFlags.TME_NONCLIENT)
			{
				if (XplatUIWin32.wm_nc_registered.Contains(hwnd))
				{
					XplatUIWin32.wm_nc_registered.Remove(hwnd);
					return;
				}
			}
			else
			{
				if (!XplatUIWin32.wm_nc_registered.Contains(hwnd))
				{
					XplatUIWin32.wm_nc_registered.Add(hwnd, tmeflags);
					return;
				}
				XplatUIWin32.wm_nc_registered[hwnd] = tmeflags;
			}
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0007A1A2 File Offset: 0x000783A2
		internal override void RequestNCRecalc(IntPtr handle)
		{
			XplatUIWin32.Win32SetWindowPos(handle, IntPtr.Zero, 0, 0, 0, 0, XplatUIWin32.SetWindowPosFlags.SWP_DRAWFRAME | XplatUIWin32.SetWindowPosFlags.SWP_NOACTIVATE | XplatUIWin32.SetWindowPosFlags.SWP_NOMOVE | XplatUIWin32.SetWindowPosFlags.SWP_NOOWNERZORDER | XplatUIWin32.SetWindowPosFlags.SWP_NOSIZE | XplatUIWin32.SetWindowPosFlags.SWP_NOZORDER);
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0007A1BC File Offset: 0x000783BC
		internal override void ResetMouseHover(IntPtr handle)
		{
			XplatUIWin32.TRACKMOUSEEVENT trackmouseevent = default(XplatUIWin32.TRACKMOUSEEVENT);
			trackmouseevent.size = Marshal.SizeOf<XplatUIWin32.TRACKMOUSEEVENT>(trackmouseevent);
			trackmouseevent.hWnd = handle;
			trackmouseevent.dwFlags = XplatUIWin32.TMEFlags.TME_HOVER | XplatUIWin32.TMEFlags.TME_LEAVE;
			XplatUIWin32.Win32TrackMouseEvent(ref trackmouseevent);
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0007A1F6 File Offset: 0x000783F6
		internal override bool GetMessage(object queue_id, ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax)
		{
			return this.GetMessage(ref msg, hWnd, wFilterMin, wFilterMax, true);
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0007A208 File Offset: 0x00078408
		private bool GetMessage(ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax, bool blocking)
		{
			msg.refobject = 0;
			if (XplatUIWin32.RetrieveMessage(ref msg))
			{
				return true;
			}
			bool flag;
			if (blocking)
			{
				flag = XplatUIWin32.Win32GetMessage(ref msg, hWnd, wFilterMin, wFilterMax);
			}
			else
			{
				flag = XplatUIWin32.Win32PeekMessage(ref msg, hWnd, wFilterMin, wFilterMax, 1U);
				if (!flag)
				{
					return false;
				}
			}
			Msg message = msg.message;
			if (message <= Msg.WM_MBUTTONUP)
			{
				if (message != Msg.WM_NCMOUSEMOVE)
				{
					if (message != Msg.WM_TIMER)
					{
						switch (message)
						{
						case Msg.WM_MOUSEMOVE:
							if (msg.hwnd != XplatUIWin32.prev_mouse_hwnd)
							{
								XplatUIWin32.mouse_state = Control.FromParamToMouseButtons((long)msg.lParam.ToInt32());
								XplatUIWin32.StoreMessage(ref msg);
								msg.message = Msg.WM_MOUSE_ENTER;
								XplatUIWin32.prev_mouse_hwnd = msg.hwnd;
								XplatUIWin32.TRACKMOUSEEVENT trackmouseevent = default(XplatUIWin32.TRACKMOUSEEVENT);
								trackmouseevent.size = Marshal.SizeOf<XplatUIWin32.TRACKMOUSEEVENT>(trackmouseevent);
								trackmouseevent.hWnd = msg.hwnd;
								trackmouseevent.dwFlags = XplatUIWin32.TMEFlags.TME_HOVER | XplatUIWin32.TMEFlags.TME_LEAVE;
								XplatUIWin32.Win32TrackMouseEvent(ref trackmouseevent);
								return flag;
							}
							break;
						case Msg.WM_LBUTTONDOWN:
							XplatUIWin32.mouse_state |= MouseButtons.Left;
							break;
						case Msg.WM_LBUTTONUP:
							XplatUIWin32.mouse_state &= ~MouseButtons.Left;
							break;
						case Msg.WM_RBUTTONDOWN:
							XplatUIWin32.mouse_state |= MouseButtons.Right;
							break;
						case Msg.WM_RBUTTONUP:
							XplatUIWin32.mouse_state &= ~MouseButtons.Right;
							break;
						case Msg.WM_MBUTTONDOWN:
							XplatUIWin32.mouse_state |= MouseButtons.Middle;
							break;
						case Msg.WM_MBUTTONUP:
							XplatUIWin32.mouse_state &= ~MouseButtons.Middle;
							break;
						}
					}
					else
					{
						Timer timer = (Timer)this.timer_list[(int)msg.wParam];
						if (timer != null)
						{
							timer.FireTick();
						}
					}
				}
				else if (XplatUIWin32.wm_nc_registered != null && XplatUIWin32.wm_nc_registered.Contains(msg.hwnd))
				{
					XplatUIWin32.mouse_state = Control.FromParamToMouseButtons((long)msg.lParam.ToInt32());
					XplatUIWin32.TRACKMOUSEEVENT trackmouseevent2 = default(XplatUIWin32.TRACKMOUSEEVENT);
					trackmouseevent2.size = Marshal.SizeOf<XplatUIWin32.TRACKMOUSEEVENT>(trackmouseevent2);
					trackmouseevent2.hWnd = msg.hwnd;
					trackmouseevent2.dwFlags = (XplatUIWin32.TMEFlags)XplatUIWin32.wm_nc_registered[msg.hwnd];
					XplatUIWin32.Win32TrackMouseEvent(ref trackmouseevent2);
					return flag;
				}
			}
			else
			{
				if (message == Msg.WM_DROPFILES)
				{
					return Win32DnD.HandleWMDropFiles(ref msg);
				}
				if (message != Msg.WM_MOUSELEAVE)
				{
					if (message == Msg.WM_ASYNC_MESSAGE)
					{
						XplatUIDriverSupport.ExecuteClientMessage((GCHandle)msg.lParam);
					}
				}
				else
				{
					XplatUIWin32.prev_mouse_hwnd = IntPtr.Zero;
				}
			}
			return flag;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0007A4A4 File Offset: 0x000786A4
		internal override bool TranslateMessage(ref MSG msg)
		{
			return XplatUIWin32.Win32TranslateMessage(ref msg);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0007A4AC File Offset: 0x000786AC
		internal override IntPtr DispatchMessage(ref MSG msg)
		{
			return XplatUIWin32.Win32DispatchMessage(ref msg);
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0007A4B4 File Offset: 0x000786B4
		internal override bool SetZOrder(IntPtr hWnd, IntPtr AfterhWnd, bool Top, bool Bottom)
		{
			if (Top)
			{
				XplatUIWin32.Win32SetWindowPos(hWnd, XplatUIWin32.SetWindowPosZOrder.HWND_TOP, 0, 0, 0, 0, XplatUIWin32.SetWindowPosFlags.SWP_NOMOVE | XplatUIWin32.SetWindowPosFlags.SWP_NOSIZE);
				return true;
			}
			if (!Bottom)
			{
				XplatUIWin32.Win32SetWindowPos(hWnd, AfterhWnd, 0, 0, 0, 0, XplatUIWin32.SetWindowPosFlags.SWP_NOMOVE | XplatUIWin32.SetWindowPosFlags.SWP_NOSIZE);
				return false;
			}
			XplatUIWin32.Win32SetWindowPos(hWnd, (IntPtr)1, 0, 0, 0, 0, XplatUIWin32.SetWindowPosFlags.SWP_NOMOVE | XplatUIWin32.SetWindowPosFlags.SWP_NOSIZE);
			return true;
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0007A4F0 File Offset: 0x000786F0
		internal override bool SetTopmost(IntPtr hWnd, bool Enabled)
		{
			if (Enabled)
			{
				XplatUIWin32.Win32SetWindowPos(hWnd, XplatUIWin32.SetWindowPosZOrder.HWND_TOPMOST, 0, 0, 0, 0, XplatUIWin32.SetWindowPosFlags.SWP_NOACTIVATE | XplatUIWin32.SetWindowPosFlags.SWP_NOMOVE | XplatUIWin32.SetWindowPosFlags.SWP_NOSIZE);
				return true;
			}
			XplatUIWin32.Win32SetWindowPos(hWnd, XplatUIWin32.SetWindowPosZOrder.HWND_NOTOPMOST, 0, 0, 0, 0, XplatUIWin32.SetWindowPosFlags.SWP_NOACTIVATE | XplatUIWin32.SetWindowPosFlags.SWP_NOMOVE | XplatUIWin32.SetWindowPosFlags.SWP_NOSIZE);
			return true;
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0007A515 File Offset: 0x00078715
		internal override bool SetOwner(IntPtr hWnd, IntPtr hWndOwner)
		{
			XplatUIWin32.Win32SetWindowLong(hWnd, XplatUIWin32.WindowLong.GWL_HWNDPARENT, (uint)(int)hWndOwner);
			return true;
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0007A527 File Offset: 0x00078727
		internal override bool Text(IntPtr handle, string text)
		{
			XplatUIWin32.Win32SetWindowText(handle, text);
			return true;
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0007A534 File Offset: 0x00078734
		internal override bool SetVisible(IntPtr handle, bool visible, bool activate)
		{
			if (visible)
			{
				Control control = Control.FromHandle(handle);
				if (control is Form)
				{
					Form form = (Form)Control.FromHandle(handle);
					XplatUIWin32.WindowPlacementFlags windowPlacementFlags = XplatUIWin32.WindowPlacementFlags.SW_SHOWNORMAL;
					switch (form.WindowState)
					{
					case FormWindowState.Normal:
						windowPlacementFlags = XplatUIWin32.WindowPlacementFlags.SW_SHOWNORMAL;
						break;
					case FormWindowState.Minimized:
						windowPlacementFlags = XplatUIWin32.WindowPlacementFlags.SW_MINIMIZE;
						break;
					case FormWindowState.Maximized:
						windowPlacementFlags = XplatUIWin32.WindowPlacementFlags.SW_SHOWMAXIMIZED;
						break;
					}
					if (!form.ActivateOnShow)
					{
						windowPlacementFlags = XplatUIWin32.WindowPlacementFlags.SW_SHOWNOACTIVATE;
					}
					XplatUIWin32.Win32ShowWindow(handle, windowPlacementFlags);
				}
				else if (control.ActivateOnShow)
				{
					XplatUIWin32.Win32ShowWindow(handle, XplatUIWin32.WindowPlacementFlags.SW_SHOWNORMAL);
				}
				else
				{
					XplatUIWin32.Win32ShowWindow(handle, XplatUIWin32.WindowPlacementFlags.SW_SHOWNOACTIVATE);
				}
			}
			else
			{
				XplatUIWin32.Win32ShowWindow(handle, XplatUIWin32.WindowPlacementFlags.SW_HIDE);
			}
			return true;
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0007A5BF File Offset: 0x000787BF
		internal override bool IsEnabled(IntPtr handle)
		{
			return XplatUIWin32.IsWindowEnabled(handle);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0007A5C8 File Offset: 0x000787C8
		internal override IntPtr SetParent(IntPtr handle, IntPtr parent)
		{
			Control control = Control.FromHandle(handle);
			if (parent == IntPtr.Zero)
			{
				if (!(control is Form))
				{
					XplatUIWin32.Win32ShowWindow(handle, XplatUIWin32.WindowPlacementFlags.SW_HIDE);
				}
			}
			else if (!(control is Form))
			{
				this.SetVisible(handle, control.is_visible, true);
			}
			XplatUIWin32.RECT rect;
			XplatUIWin32.Win32GetWindowRect(handle, out rect);
			WindowStyles windowStyles = (WindowStyles)XplatUIWin32.Win32GetWindowLong(handle, XplatUIWin32.WindowLong.GWL_STYLE);
			WindowStyles windowStyles2;
			IntPtr intPtr;
			if (parent == IntPtr.Zero)
			{
				windowStyles2 = windowStyles & ~WindowStyles.WS_CHILD;
				intPtr = XplatUIWin32.Win32SetParent(handle, this.GetFosterParent());
			}
			else
			{
				windowStyles2 = windowStyles | WindowStyles.WS_CHILD;
				intPtr = XplatUIWin32.Win32SetParent(handle, parent);
			}
			if (windowStyles != windowStyles2 && control is Form)
			{
				XplatUIWin32.Win32SetWindowLong(handle, XplatUIWin32.WindowLong.GWL_STYLE, (uint)windowStyles2);
			}
			XplatUIWin32.RECT rect2;
			XplatUIWin32.Win32GetWindowRect(handle, out rect2);
			if (rect.top != rect2.top && rect.left != rect2.left && control is Form)
			{
				XplatUIWin32.Win32SetWindowPos(handle, IntPtr.Zero, rect.top, rect.left, rect.Width, rect.Height, XplatUIWin32.SetWindowPosFlags.SWP_NOACTIVATE | XplatUIWin32.SetWindowPosFlags.SWP_NOOWNERZORDER | XplatUIWin32.SetWindowPosFlags.SWP_NOREDRAW | XplatUIWin32.SetWindowPosFlags.SWP_NOENDSCHANGING | XplatUIWin32.SetWindowPosFlags.SWP_NOZORDER);
			}
			return intPtr;
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0007A6CF File Offset: 0x000788CF
		internal override IntPtr GetParent(IntPtr handle)
		{
			return XplatUIWin32.Win32GetParent(handle);
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x00009E44 File Offset: 0x00008044
		internal override IntPtr GetPreviousWindow(IntPtr handle)
		{
			return handle;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0007A6D8 File Offset: 0x000788D8
		internal override void GrabWindow(IntPtr hWnd, IntPtr ConfineToHwnd)
		{
			XplatUIWin32.grab_hwnd = hWnd;
			XplatUIWin32.Win32SetCapture(hWnd);
			if (ConfineToHwnd != IntPtr.Zero)
			{
				XplatUIWin32.RECT rect;
				XplatUIWin32.Win32GetWindowRect(ConfineToHwnd, out rect);
				XplatUIWin32.Win32GetClipCursor(out XplatUIWin32.clipped_cursor_rect);
				XplatUIWin32.Win32ClipCursor(ref rect);
			}
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0007A71B File Offset: 0x0007891B
		internal override void GrabInfo(out IntPtr hWnd, out bool GrabConfined, out Rectangle GrabArea)
		{
			hWnd = XplatUIWin32.grab_hwnd;
			GrabConfined = XplatUIWin32.grab_confined;
			GrabArea = XplatUIWin32.grab_area;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0007A738 File Offset: 0x00078938
		internal override void UngrabWindow(IntPtr hWnd)
		{
			if (XplatUIWin32.clipped_cursor_rect.top != 0 || XplatUIWin32.clipped_cursor_rect.bottom != 0 || XplatUIWin32.clipped_cursor_rect.left != 0 || XplatUIWin32.clipped_cursor_rect.right != 0)
			{
				XplatUIWin32.Win32ClipCursor(ref XplatUIWin32.clipped_cursor_rect);
				XplatUIWin32.clipped_cursor_rect = default(XplatUIWin32.RECT);
			}
			XplatUIWin32.Win32ReleaseCapture();
			XplatUIWin32.grab_hwnd = IntPtr.Zero;
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0007A79C File Offset: 0x0007899C
		internal override bool CalculateWindowRect(ref Rectangle ClientRect, CreateParams cp, Menu menu, out Rectangle WindowRect)
		{
			XplatUIWin32.RECT rect;
			rect.left = ClientRect.Left;
			rect.top = ClientRect.Top;
			rect.right = ClientRect.Right;
			rect.bottom = ClientRect.Bottom;
			if (!XplatUIWin32.Win32AdjustWindowRectEx(ref rect, cp.Style, menu != null, cp.ExStyle))
			{
				WindowRect = new Rectangle(ClientRect.Left, ClientRect.Top, ClientRect.Width, ClientRect.Height);
				return false;
			}
			WindowRect = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
			return true;
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0007A84F File Offset: 0x00078A4F
		internal override void SetCursor(IntPtr window, IntPtr cursor)
		{
			XplatUIWin32.Win32SetCursor(cursor);
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0007A858 File Offset: 0x00078A58
		internal override IntPtr DefineCursor(Bitmap bitmap, Bitmap mask, Color cursor_pixel, Color mask_pixel, int xHotSpot, int yHotSpot)
		{
			Bitmap bitmap2;
			Bitmap bitmap3;
			if (bitmap.Width != XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXCURSOR) || bitmap.Width != XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXCURSOR))
			{
				bitmap2 = new Bitmap(bitmap, new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXCURSOR), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXCURSOR)));
				bitmap3 = new Bitmap(mask, new Size(XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXCURSOR), XplatUIWin32.Win32GetSystemMetrics(XplatUIWin32.SystemMetrics.SM_CXCURSOR)));
			}
			else
			{
				bitmap2 = bitmap;
				bitmap3 = mask;
			}
			int width = bitmap2.Width;
			int height = bitmap2.Height;
			byte[] array = new byte[width / 8 * height];
			byte[] array2 = new byte[width / 8 * height];
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					if (bitmap2.GetPixel(j, i) == cursor_pixel)
					{
						byte[] array3 = array;
						int num = i * width / 8 + j / 8;
						array3[num] |= (byte)(128 >> j % 8);
					}
					if (bitmap3.GetPixel(j, i) == mask_pixel)
					{
						byte[] array4 = array2;
						int num2 = i * width / 8 + j / 8;
						array4[num2] |= (byte)(128 >> j % 8);
					}
				}
			}
			return XplatUIWin32.Win32CreateCursor(IntPtr.Zero, xHotSpot, yHotSpot, width, height, array2, array);
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0007A994 File Offset: 0x00078B94
		[MonoTODO("Define the missing cursors")]
		internal override IntPtr DefineStdCursor(StdCursor id)
		{
			switch (id)
			{
			case StdCursor.Default:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.AppStarting:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_APPSTARTING);
			case StdCursor.Arrow:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.Cross:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_CROSS);
			case StdCursor.Hand:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_HAND);
			case StdCursor.Help:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_HELP);
			case StdCursor.HSplit:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.IBeam:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_IBEAM);
			case StdCursor.No:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_NO);
			case StdCursor.NoMove2D:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.NoMoveHoriz:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.NoMoveVert:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.PanEast:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.PanNE:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.PanNorth:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.PanNW:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.PanSE:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.PanSouth:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.PanSW:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.PanWest:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.SizeAll:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_SIZEALL);
			case StdCursor.SizeNESW:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_SIZENESW);
			case StdCursor.SizeNS:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_SIZENS);
			case StdCursor.SizeNWSE:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_SIZENWSE);
			case StdCursor.SizeWE:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_SIZEWE);
			case StdCursor.UpArrow:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_UPARROW);
			case StdCursor.VSplit:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.First);
			case StdCursor.WaitCursor:
				return XplatUIWin32.Win32LoadCursor(IntPtr.Zero, XplatUIWin32.LoadCursorType.IDC_WAIT);
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0007ABE4 File Offset: 0x00078DE4
		[MonoTODO]
		internal override void GetCursorInfo(IntPtr cursor, out int width, out int height, out int hotspot_x, out int hotspot_y)
		{
			XplatUIWin32.ICONINFO iconinfo = default(XplatUIWin32.ICONINFO);
			if (!XplatUIWin32.Win32GetIconInfo(cursor, out iconinfo))
			{
				throw new Win32Exception();
			}
			width = 20;
			height = 20;
			hotspot_x = iconinfo.xHotspot;
			hotspot_y = iconinfo.yHotspot;
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0007AC23 File Offset: 0x00078E23
		internal override void SetCursorPos(IntPtr handle, int x, int y)
		{
			XplatUIWin32.Win32SetCursorPos(x, y);
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0007AC2D File Offset: 0x00078E2D
		internal override void SetClipRegion(IntPtr hwnd, Region region)
		{
			if (region == null)
			{
				XplatUIWin32.Win32SetWindowRgn(hwnd, IntPtr.Zero, true);
				return;
			}
			XplatUIWin32.Win32SetWindowRgn(hwnd, region.GetHrgn(Graphics.FromHwnd(hwnd)), true);
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0007AC54 File Offset: 0x00078E54
		internal override void EnableWindow(IntPtr handle, bool Enable)
		{
			XplatUIWin32.Win32EnableWindow(handle, Enable);
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override void EndLoop(Thread thread)
		{
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00003C7A File Offset: 0x00001E7A
		internal override object StartLoop(Thread thread)
		{
			return null;
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override void SetModal(IntPtr handle, bool Modal)
		{
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x0007AC60 File Offset: 0x00078E60
		internal override void GetCursorPos(IntPtr handle, out int x, out int y)
		{
			POINT point;
			XplatUIWin32.Win32GetCursorPos(out point);
			if (handle != IntPtr.Zero)
			{
				XplatUIWin32.Win32ScreenToClient(handle, ref point);
			}
			x = point.x;
			y = point.y;
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0007AC9C File Offset: 0x00078E9C
		internal override void ScreenToClient(IntPtr handle, ref int x, ref int y)
		{
			POINT point = default(POINT);
			point.x = x;
			point.y = y;
			XplatUIWin32.Win32ScreenToClient(handle, ref point);
			x = point.x;
			y = point.y;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0007ACDC File Offset: 0x00078EDC
		internal override void ClientToScreen(IntPtr handle, ref int x, ref int y)
		{
			POINT point = default(POINT);
			point.x = x;
			point.y = y;
			XplatUIWin32.Win32ClientToScreen(handle, ref point);
			x = point.x;
			y = point.y;
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0007AD1C File Offset: 0x00078F1C
		internal override void ScreenToMenu(IntPtr handle, ref int x, ref int y)
		{
			XplatUIWin32.RECT rect;
			XplatUIWin32.Win32GetWindowRect(handle, out rect);
			x -= rect.left + SystemInformation.FrameBorderSize.Width;
			y -= rect.top + SystemInformation.FrameBorderSize.Height;
			if (CreateParams.IsSet((WindowStyles)XplatUIWin32.Win32GetWindowLong(handle, XplatUIWin32.WindowLong.GWL_STYLE), WindowStyles.WS_CAPTION))
			{
				y -= ThemeEngine.Current.CaptionHeight;
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0007AD88 File Offset: 0x00078F88
		internal override void MenuToScreen(IntPtr handle, ref int x, ref int y)
		{
			XplatUIWin32.RECT rect;
			XplatUIWin32.Win32GetWindowRect(handle, out rect);
			x += rect.left + SystemInformation.FrameBorderSize.Width;
			y += rect.top + SystemInformation.FrameBorderSize.Height + ThemeEngine.Current.CaptionHeight;
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0007ADDB File Offset: 0x00078FDB
		internal override void SendAsyncMethod(AsyncMethodData method)
		{
			XplatUIWin32.Win32PostMessage(this.GetFosterParent(), Msg.WM_ASYNC_MESSAGE, IntPtr.Zero, (IntPtr)GCHandle.Alloc(method));
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0007AE00 File Offset: 0x00079000
		internal override void SetTimer(Timer timer)
		{
			IntPtr fosterParent = this.GetFosterParent();
			int hashCode = timer.GetHashCode();
			Hashtable hashtable = this.timer_list;
			lock (hashtable)
			{
				this.timer_list[hashCode] = timer;
			}
			if (XplatUIWin32.Win32SetTimer(fosterParent, hashCode, (uint)timer.Interval, IntPtr.Zero) != IntPtr.Zero)
			{
				timer.window = fosterParent;
				return;
			}
			timer.window = IntPtr.Zero;
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0007AE8C File Offset: 0x0007908C
		internal override void KillTimer(Timer timer)
		{
			int hashCode = timer.GetHashCode();
			XplatUIWin32.Win32KillTimer(timer.window, hashCode);
			Hashtable hashtable = this.timer_list;
			lock (hashtable)
			{
				this.timer_list.Remove(hashCode);
			}
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0007AEEC File Offset: 0x000790EC
		internal override void CreateCaret(IntPtr hwnd, int width, int height)
		{
			XplatUIWin32.Win32CreateCaret(hwnd, IntPtr.Zero, width, height);
			XplatUIWin32.caret_visible = false;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0007AF02 File Offset: 0x00079102
		internal override void DestroyCaret(IntPtr hwnd)
		{
			XplatUIWin32.Win32DestroyCaret();
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0007AF0A File Offset: 0x0007910A
		internal override void SetCaretPos(IntPtr hwnd, int x, int y)
		{
			XplatUIWin32.Win32SetCaretPos(x, y);
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0007AF14 File Offset: 0x00079114
		internal override void CaretVisible(IntPtr hwnd, bool visible)
		{
			if (visible)
			{
				if (!XplatUIWin32.caret_visible)
				{
					XplatUIWin32.Win32ShowCaret(hwnd);
					XplatUIWin32.caret_visible = true;
					return;
				}
			}
			else if (XplatUIWin32.caret_visible)
			{
				XplatUIWin32.Win32HideCaret(hwnd);
				XplatUIWin32.caret_visible = false;
			}
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0007AF42 File Offset: 0x00079142
		internal override IntPtr GetFocus()
		{
			return XplatUIWin32.Win32GetFocus();
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0007AF49 File Offset: 0x00079149
		internal override void SetFocus(IntPtr hwnd)
		{
			XplatUIWin32.Win32SetFocus(hwnd);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0007AF52 File Offset: 0x00079152
		internal override IntPtr GetActive()
		{
			return XplatUIWin32.Win32GetActiveWindow();
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0007AF5C File Offset: 0x0007915C
		internal override bool GetFontMetrics(Graphics g, Font font, out int ascent, out int descent)
		{
			XplatUIWin32.TEXTMETRIC textmetric = default(XplatUIWin32.TEXTMETRIC);
			IntPtr intPtr = XplatUIWin32.Win32GetDC(IntPtr.Zero);
			IntPtr intPtr2 = XplatUIWin32.Win32SelectObject(intPtr, font.ToHfont());
			if (!XplatUIWin32.Win32GetTextMetrics(intPtr, ref textmetric))
			{
				intPtr2 = XplatUIWin32.Win32SelectObject(intPtr, intPtr2);
				XplatUIWin32.Win32DeleteObject(intPtr2);
				XplatUIWin32.Win32ReleaseDC(IntPtr.Zero, intPtr);
				ascent = 0;
				descent = 0;
				return false;
			}
			intPtr2 = XplatUIWin32.Win32SelectObject(intPtr, intPtr2);
			XplatUIWin32.Win32DeleteObject(intPtr2);
			XplatUIWin32.Win32ReleaseDC(IntPtr.Zero, intPtr);
			ascent = textmetric.tmAscent;
			descent = textmetric.tmDescent;
			return true;
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0007AFE4 File Offset: 0x000791E4
		internal override void ScrollWindow(IntPtr hwnd, Rectangle rectangle, int XAmount, int YAmount, bool with_children)
		{
			XplatUIWin32.RECT rect = default(XplatUIWin32.RECT);
			rect.left = rectangle.X;
			rect.top = rectangle.Y;
			rect.right = rectangle.Right;
			rect.bottom = rectangle.Bottom;
			XplatUIWin32.Win32ScrollWindowEx(hwnd, XAmount, YAmount, IntPtr.Zero, ref rect, IntPtr.Zero, IntPtr.Zero, XplatUIWin32.ScrollWindowExFlags.SW_INVALIDATE | XplatUIWin32.ScrollWindowExFlags.SW_ERASE | (with_children ? XplatUIWin32.ScrollWindowExFlags.SW_SCROLLCHILDREN : XplatUIWin32.ScrollWindowExFlags.SW_NONE));
			XplatUIWin32.Win32UpdateWindow(hwnd);
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0007B060 File Offset: 0x00079260
		internal override void ScrollWindow(IntPtr hwnd, int XAmount, int YAmount, bool with_children)
		{
			XplatUIWin32.Win32ScrollWindowEx(hwnd, XAmount, YAmount, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, XplatUIWin32.ScrollWindowExFlags.SW_INVALIDATE | XplatUIWin32.ScrollWindowExFlags.SW_ERASE | (with_children ? XplatUIWin32.ScrollWindowExFlags.SW_SCROLLCHILDREN : XplatUIWin32.ScrollWindowExFlags.SW_NONE));
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0000493C File Offset: 0x00002B3C
		internal override void SetBorderStyle(IntPtr handle, FormBorderStyle border_style)
		{
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0007B094 File Offset: 0x00079294
		internal override void SetMenu(IntPtr handle, Menu menu)
		{
			XplatUIWin32.Win32SetWindowPos(handle, IntPtr.Zero, 0, 0, 0, 0, XplatUIWin32.SetWindowPosFlags.SWP_DRAWFRAME | XplatUIWin32.SetWindowPosFlags.SWP_NOMOVE | XplatUIWin32.SetWindowPosFlags.SWP_NOSIZE);
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0007B0A8 File Offset: 0x000792A8
		internal override Point GetMenuOrigin(IntPtr handle)
		{
			Form form = Control.FromHandle(handle) as Form;
			if (form == null)
			{
				return new Point(SystemInformation.FrameBorderSize.Width, SystemInformation.FrameBorderSize.Height + ThemeEngine.Current.CaptionHeight);
			}
			if (form.FormBorderStyle == FormBorderStyle.None)
			{
				return Point.Empty;
			}
			int num = (form.Width - form.ClientSize.Width) / 2;
			if (form.FormBorderStyle == FormBorderStyle.FixedToolWindow || form.FormBorderStyle == FormBorderStyle.SizableToolWindow)
			{
				return new Point(num, num + SystemInformation.ToolWindowCaptionHeight);
			}
			return new Point(num, num + SystemInformation.CaptionHeight);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0007B142 File Offset: 0x00079342
		internal override void SetIcon(IntPtr hwnd, Icon icon)
		{
			XplatUIWin32.Win32SendMessage(hwnd, Msg.WM_SETICON, (IntPtr)1, (icon == null) ? IntPtr.Zero : icon.Handle);
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0007B166 File Offset: 0x00079366
		internal override void ClipboardClose(IntPtr handle)
		{
			if (handle != XplatUIWin32.clip_magic)
			{
				throw new ArgumentException("handle is not a valid clipboard handle");
			}
			XplatUIWin32.Win32CloseClipboard();
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0007B188 File Offset: 0x00079388
		internal override int ClipboardGetID(IntPtr handle, string format)
		{
			if (handle != XplatUIWin32.clip_magic)
			{
				throw new ArgumentException("handle is not a valid clipboard handle");
			}
			if (format == "Text")
			{
				return 1;
			}
			if (format == "Bitmap")
			{
				return 2;
			}
			if (format == "MetaFilePict")
			{
				return 3;
			}
			if (format == "SymbolicLink")
			{
				return 4;
			}
			if (format == "DataInterchangeFormat")
			{
				return 5;
			}
			if (format == "Tiff")
			{
				return 6;
			}
			if (format == "OEMText")
			{
				return 7;
			}
			if (format == "DeviceIndependentBitmap")
			{
				return 8;
			}
			if (format == "Palette")
			{
				return 9;
			}
			if (format == "PenData")
			{
				return 10;
			}
			if (format == "RiffAudio")
			{
				return 11;
			}
			if (format == "WaveAudio")
			{
				return 12;
			}
			if (format == "UnicodeText")
			{
				return 13;
			}
			if (format == "EnhancedMetafile")
			{
				return 14;
			}
			if (format == "FileDrop")
			{
				return 15;
			}
			if (format == "Locale")
			{
				return 16;
			}
			return (int)XplatUIWin32.Win32RegisterClipboardFormat(format);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0007B2AB File Offset: 0x000794AB
		internal override IntPtr ClipboardOpen(bool primary_selection)
		{
			XplatUIWin32.Win32OpenClipboard(this.GetFosterParent());
			return XplatUIWin32.clip_magic;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0007B2C0 File Offset: 0x000794C0
		internal override int[] ClipboardAvailableFormats(IntPtr handle)
		{
			if (handle != XplatUIWin32.clip_magic)
			{
				return null;
			}
			int num = 0;
			uint num2 = 0U;
			do
			{
				num2 = XplatUIWin32.Win32EnumClipboardFormats(num2);
				if (num2 != 0U)
				{
					num++;
				}
			}
			while (num2 != 0U);
			int[] array = new int[num];
			num = 0;
			num2 = 0U;
			do
			{
				num2 = XplatUIWin32.Win32EnumClipboardFormats(num2);
				if (num2 != 0U)
				{
					array[num++] = (int)num2;
				}
			}
			while (num2 != 0U);
			return array;
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0007B314 File Offset: 0x00079514
		internal override object ClipboardRetrieve(IntPtr handle, int type, XplatUI.ClipboardToObject converter)
		{
			if (handle != XplatUIWin32.clip_magic)
			{
				throw new ArgumentException("handle is not a valid clipboard handle");
			}
			IntPtr intPtr = XplatUIWin32.Win32GetClipboardData((uint)type);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			IntPtr intPtr2 = XplatUIWin32.Win32GlobalLock(intPtr);
			if (intPtr2 == IntPtr.Zero)
			{
				uint num = XplatUIWin32.Win32GetLastError();
				Console.WriteLine("Error: {0}", num);
				return null;
			}
			object obj = null;
			if (type == DataFormats.GetFormat(DataFormats.Rtf).Id)
			{
				obj = XplatUIWin32.AnsiToString(intPtr2);
			}
			else
			{
				ClipboardFormats clipboardFormats = (ClipboardFormats)type;
				if (clipboardFormats != ClipboardFormats.CF_TEXT)
				{
					if (clipboardFormats != ClipboardFormats.CF_DIB)
					{
						if (clipboardFormats != ClipboardFormats.CF_UNICODETEXT)
						{
							if (converter != null && !converter(type, intPtr2, out obj))
							{
								obj = null;
							}
						}
						else
						{
							obj = XplatUIWin32.UnicodeToString(intPtr2);
						}
					}
					else
					{
						obj = XplatUIWin32.DIBtoImage(intPtr2);
					}
				}
				else
				{
					obj = XplatUIWin32.AnsiToString(intPtr2);
				}
			}
			XplatUIWin32.Win32GlobalUnlock(intPtr);
			return obj;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0007B3E4 File Offset: 0x000795E4
		internal override void ClipboardStore(IntPtr handle, object obj, int type, XplatUI.ObjectToClipboard converter, bool copy)
		{
			byte[] array = null;
			if (handle != XplatUIWin32.clip_magic)
			{
				throw new ArgumentException("handle is not a valid clipboard handle");
			}
			if (obj != null)
			{
				if (type == -1)
				{
					if (obj is string)
					{
						type = 13;
					}
					else if (obj is Image)
					{
						type = 8;
					}
				}
				if (type == DataFormats.GetFormat(DataFormats.Rtf).Id)
				{
					array = XplatUIWin32.StringToAnsi((string)obj);
				}
				else
				{
					ClipboardFormats clipboardFormats = (ClipboardFormats)type;
					if (clipboardFormats <= ClipboardFormats.CF_BITMAP)
					{
						if (clipboardFormats == ClipboardFormats.CF_TEXT)
						{
							array = XplatUIWin32.StringToAnsi((string)obj);
							goto IL_00C8;
						}
						if (clipboardFormats != ClipboardFormats.CF_BITMAP)
						{
							goto IL_00B4;
						}
					}
					else if (clipboardFormats != ClipboardFormats.CF_DIB)
					{
						if (clipboardFormats == ClipboardFormats.CF_UNICODETEXT)
						{
							array = XplatUIWin32.StringToUnicode((string)obj);
							goto IL_00C8;
						}
						goto IL_00B4;
					}
					array = XplatUIWin32.ImageToDIB((Image)obj);
					type = 8;
					goto IL_00C8;
					IL_00B4:
					if (converter != null && !converter(ref type, obj, out array))
					{
						array = null;
					}
				}
				IL_00C8:
				if (array != null)
				{
					this.SetClipboardData((uint)type, array);
				}
				return;
			}
			if (!XplatUIWin32.Win32EmptyClipboard())
			{
				throw new ExternalException("Win32EmptyClipboard");
			}
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0007B4C4 File Offset: 0x000796C4
		internal static byte[] StringToUnicode(string text)
		{
			return Encoding.Unicode.GetBytes(text + "\0");
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0007B4DB File Offset: 0x000796DB
		internal static byte[] StringToAnsi(string text)
		{
			return Encoding.UTF8.GetBytes(text + "\0");
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0007B4F4 File Offset: 0x000796F4
		private void SetClipboardData(uint type, byte[] data)
		{
			if (data.Length == 0)
			{
				return;
			}
			IntPtr intPtr = XplatUIWin32.CopyToMoveableMemory(data);
			if (intPtr == IntPtr.Zero)
			{
				throw new ExternalException("CopyToMoveableMemory failed.");
			}
			if (XplatUIWin32.Win32SetClipboardData(type, intPtr) == IntPtr.Zero)
			{
				throw new ExternalException("Win32SetClipboardData");
			}
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0007B544 File Offset: 0x00079744
		internal static IntPtr CopyToMoveableMemory(byte[] data)
		{
			if (data == null || data.Length == 0)
			{
				throw new ArgumentException("Can't create a zero length memory block.");
			}
			IntPtr intPtr = XplatUIWin32.Win32GlobalAlloc(XplatUIWin32.GAllocFlags.GMEM_MOVEABLE | XplatUIWin32.GAllocFlags.GMEM_SHARE, data.Length);
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception();
			}
			IntPtr intPtr2 = XplatUIWin32.Win32GlobalLock(intPtr);
			if (intPtr2 == IntPtr.Zero)
			{
				throw new Win32Exception();
			}
			Marshal.Copy(data, 0, intPtr2, data.Length);
			XplatUIWin32.Win32GlobalUnlock(intPtr);
			return intPtr;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0007B5AF File Offset: 0x000797AF
		internal override void SetAllowDrop(IntPtr hwnd, bool allowed)
		{
			if (allowed)
			{
				Win32DnD.RegisterDropTarget(hwnd);
				return;
			}
			Win32DnD.UnregisterDropTarget(hwnd);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0007B5C4 File Offset: 0x000797C4
		internal override void DrawReversibleRectangle(IntPtr handle, Rectangle rect, int line_width)
		{
			POINT point = default(POINT);
			point.x = 0;
			point.y = 0;
			XplatUIWin32.Win32ClientToScreen(handle, ref point);
			IntPtr intPtr = XplatUIWin32.Win32CreatePen(XplatUIWin32.PenStyle.PS_SOLID, line_width, IntPtr.Zero);
			IntPtr intPtr2 = XplatUIWin32.Win32GetDC(IntPtr.Zero);
			XplatUIWin32.Win32SetROP2(intPtr2, XplatUIWin32.ROP2DrawMode.R2_NOT);
			IntPtr intPtr3 = XplatUIWin32.Win32SelectObject(intPtr2, intPtr);
			Control control = Control.FromHandle(handle);
			if (control != null)
			{
				XplatUIWin32.RECT rect2;
				XplatUIWin32.Win32GetWindowRect(control.Handle, out rect2);
				Region region = new Region(new Rectangle(rect2.left, rect2.top, rect2.right - rect2.left, rect2.bottom - rect2.top));
				XplatUIWin32.Win32ExtSelectClipRgn(intPtr2, region.GetHrgn(Graphics.FromHdc(intPtr2)), 1);
			}
			XplatUIWin32.Win32MoveToEx(intPtr2, point.x + rect.Left, point.y + rect.Top, IntPtr.Zero);
			if (rect.Width > 0 && rect.Height > 0)
			{
				XplatUIWin32.Win32LineTo(intPtr2, point.x + rect.Right, point.y + rect.Top);
				XplatUIWin32.Win32LineTo(intPtr2, point.x + rect.Right, point.y + rect.Bottom);
				XplatUIWin32.Win32LineTo(intPtr2, point.x + rect.Left, point.y + rect.Bottom);
				XplatUIWin32.Win32LineTo(intPtr2, point.x + rect.Left, point.y + rect.Top);
			}
			else if (rect.Width > 0)
			{
				XplatUIWin32.Win32LineTo(intPtr2, point.x + rect.Right, point.y + rect.Top);
			}
			else
			{
				XplatUIWin32.Win32LineTo(intPtr2, point.x + rect.Left, point.y + rect.Bottom);
			}
			XplatUIWin32.Win32SelectObject(intPtr2, intPtr3);
			XplatUIWin32.Win32DeleteObject(intPtr);
			if (control != null)
			{
				XplatUIWin32.Win32ExtSelectClipRgn(intPtr2, IntPtr.Zero, 5);
			}
			XplatUIWin32.Win32ReleaseDC(IntPtr.Zero, intPtr2);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0007B7D0 File Offset: 0x000799D0
		internal override SizeF GetAutoScaleSize(Font font)
		{
			string text = "The quick brown fox jumped over the lazy dog.";
			double num = 44.54999694824219;
			return new SizeF((float)((double)Graphics.FromHwnd(this.GetFosterParent()).MeasureString(text, font).Width / num), (float)font.Height);
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0007B817 File Offset: 0x00079A17
		internal override IntPtr SendMessage(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam)
		{
			return XplatUIWin32.Win32SendMessage(hwnd, message, wParam, lParam);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0007B823 File Offset: 0x00079A23
		internal override bool PostMessage(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam)
		{
			return XplatUIWin32.Win32PostMessage(hwnd, message, wParam, lParam);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0007B830 File Offset: 0x00079A30
		internal override void CreateOffscreenDrawable(IntPtr handle, int width, int height, out object offscreen_drawable)
		{
			Graphics graphics = Graphics.FromHwnd(handle);
			IntPtr hdc = graphics.GetHdc();
			IntPtr intPtr = XplatUIWin32.Win32CreateCompatibleDC(hdc);
			IntPtr intPtr2 = XplatUIWin32.Win32CreateCompatibleBitmap(hdc, width, height);
			XplatUIWin32.Win32SelectObject(intPtr, intPtr2);
			offscreen_drawable = new XplatUIWin32.WinBuffer(intPtr, intPtr2);
			graphics.ReleaseHdc(hdc);
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0007B872 File Offset: 0x00079A72
		internal override Graphics GetOffscreenGraphics(object offscreen_drawable)
		{
			return Graphics.FromHdc(((XplatUIWin32.WinBuffer)offscreen_drawable).hdc);
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0007B884 File Offset: 0x00079A84
		internal override void BlitFromOffscreen(IntPtr dest_handle, Graphics dest_dc, object offscreen_drawable, Graphics offscreen_dc, Rectangle r)
		{
			XplatUIWin32.WinBuffer winBuffer = (XplatUIWin32.WinBuffer)offscreen_drawable;
			IntPtr hdc = dest_dc.GetHdc();
			XplatUIWin32.Win32BitBlt(hdc, r.Left, r.Top, r.Width, r.Height, winBuffer.hdc, r.Left, r.Top, XplatUIWin32.TernaryRasterOperations.SRCCOPY);
			dest_dc.ReleaseHdc(hdc);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0007B8E2 File Offset: 0x00079AE2
		internal override void DestroyOffscreenDrawable(object offscreen_drawable)
		{
			XplatUIWin32.WinBuffer winBuffer = (XplatUIWin32.WinBuffer)offscreen_drawable;
			XplatUIWin32.Win32DeleteObject(winBuffer.bitmap);
			XplatUIWin32.Win32DeleteDC(winBuffer.hdc);
		}

		// Token: 0x06001998 RID: 6552
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetLastError")]
		private static extern uint Win32GetLastError();

		// Token: 0x06001999 RID: 6553
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "CreateWindowExW")]
		internal static extern IntPtr Win32CreateWindow(WindowExStyles dwExStyle, string lpClassName, string lpWindowName, WindowStyles dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lParam);

		// Token: 0x0600199A RID: 6554
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "DestroyWindow")]
		internal static extern bool Win32DestroyWindow(IntPtr hWnd);

		// Token: 0x0600199B RID: 6555
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "PeekMessageW")]
		internal static extern bool Win32PeekMessage(ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax, uint flags);

		// Token: 0x0600199C RID: 6556
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "GetMessageW")]
		internal static extern bool Win32GetMessage(ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax);

		// Token: 0x0600199D RID: 6557
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "TranslateMessage")]
		internal static extern bool Win32TranslateMessage(ref MSG msg);

		// Token: 0x0600199E RID: 6558
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "DispatchMessageW")]
		internal static extern IntPtr Win32DispatchMessage(ref MSG msg);

		// Token: 0x0600199F RID: 6559
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "MoveWindow")]
		internal static extern bool Win32MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);

		// Token: 0x060019A0 RID: 6560
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetWindowPos")]
		internal static extern bool Win32SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, XplatUIWin32.SetWindowPosFlags Flags);

		// Token: 0x060019A1 RID: 6561
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetWindowPos")]
		internal static extern bool Win32SetWindowPos(IntPtr hWnd, XplatUIWin32.SetWindowPosZOrder pos, int x, int y, int cx, int cy, XplatUIWin32.SetWindowPosFlags Flags);

		// Token: 0x060019A2 RID: 6562
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "SetWindowTextW")]
		internal static extern bool Win32SetWindowText(IntPtr hWnd, string lpString);

		// Token: 0x060019A3 RID: 6563
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetParent")]
		internal static extern IntPtr Win32SetParent(IntPtr hWnd, IntPtr hParent);

		// Token: 0x060019A4 RID: 6564
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "RegisterClassW")]
		private static extern bool Win32RegisterClass(ref XplatUIWin32.WNDCLASS wndClass);

		// Token: 0x060019A5 RID: 6565
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "LoadCursorW")]
		private static extern IntPtr Win32LoadCursor(IntPtr hInstance, XplatUIWin32.LoadCursorType type);

		// Token: 0x060019A6 RID: 6566
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetCursor")]
		private static extern IntPtr Win32SetCursor(IntPtr hCursor);

		// Token: 0x060019A7 RID: 6567
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "CreateCursor")]
		private static extern IntPtr Win32CreateCursor(IntPtr hInstance, int xHotSpot, int yHotSpot, int nWidth, int nHeight, byte[] pvANDPlane, byte[] pvORPlane);

		// Token: 0x060019A8 RID: 6568
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "DefWindowProcW")]
		private static extern IntPtr Win32DefWindowProc(IntPtr hWnd, Msg Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x060019A9 RID: 6569
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "PostQuitMessage")]
		private static extern IntPtr Win32PostQuitMessage(int nExitCode);

		// Token: 0x060019AA RID: 6570
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "UpdateWindow")]
		private static extern IntPtr Win32UpdateWindow(IntPtr hWnd);

		// Token: 0x060019AB RID: 6571
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetUpdateRect")]
		private static extern bool Win32GetUpdateRect(IntPtr hWnd, ref XplatUIWin32.RECT rect, bool erase);

		// Token: 0x060019AC RID: 6572
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "BeginPaint")]
		private static extern IntPtr Win32BeginPaint(IntPtr hWnd, ref XplatUIWin32.PAINTSTRUCT ps);

		// Token: 0x060019AD RID: 6573
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ValidateRect")]
		private static extern IntPtr Win32ValidateRect(IntPtr hWnd, ref XplatUIWin32.RECT rect);

		// Token: 0x060019AE RID: 6574
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "EndPaint")]
		private static extern bool Win32EndPaint(IntPtr hWnd, ref XplatUIWin32.PAINTSTRUCT ps);

		// Token: 0x060019AF RID: 6575
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetDC")]
		private static extern IntPtr Win32GetDC(IntPtr hWnd);

		// Token: 0x060019B0 RID: 6576
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetWindowDC")]
		private static extern IntPtr Win32GetWindowDC(IntPtr hWnd);

		// Token: 0x060019B1 RID: 6577
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ReleaseDC")]
		private static extern IntPtr Win32ReleaseDC(IntPtr hWnd, IntPtr hDC);

		// Token: 0x060019B2 RID: 6578
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "MessageBoxW")]
		private static extern IntPtr Win32MessageBox(IntPtr hParent, string pText, string pCaption, uint uType);

		// Token: 0x060019B3 RID: 6579
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "InvalidateRect")]
		private static extern IntPtr Win32InvalidateRect(IntPtr hWnd, ref XplatUIWin32.RECT lpRect, bool bErase);

		// Token: 0x060019B4 RID: 6580
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetCapture")]
		private static extern IntPtr Win32SetCapture(IntPtr hWnd);

		// Token: 0x060019B5 RID: 6581
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ReleaseCapture")]
		private static extern IntPtr Win32ReleaseCapture();

		// Token: 0x060019B6 RID: 6582
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetWindowRect")]
		private static extern IntPtr Win32GetWindowRect(IntPtr hWnd, out XplatUIWin32.RECT rect);

		// Token: 0x060019B7 RID: 6583
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetClientRect")]
		private static extern IntPtr Win32GetClientRect(IntPtr hWnd, out XplatUIWin32.RECT rect);

		// Token: 0x060019B8 RID: 6584
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ScreenToClient")]
		private static extern bool Win32ScreenToClient(IntPtr hWnd, ref POINT pt);

		// Token: 0x060019B9 RID: 6585
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ClientToScreen")]
		private static extern bool Win32ClientToScreen(IntPtr hWnd, ref POINT pt);

		// Token: 0x060019BA RID: 6586
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetParent")]
		private static extern IntPtr Win32GetParent(IntPtr hWnd);

		// Token: 0x060019BB RID: 6587
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetAncestor")]
		private static extern IntPtr Win32GetAncestor(IntPtr hWnd, XplatUIWin32.AncestorType flags);

		// Token: 0x060019BC RID: 6588
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetActiveWindow")]
		private static extern IntPtr Win32SetActiveWindow(IntPtr hWnd);

		// Token: 0x060019BD RID: 6589
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "AdjustWindowRectEx")]
		private static extern bool Win32AdjustWindowRectEx(ref XplatUIWin32.RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);

		// Token: 0x060019BE RID: 6590
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetCursorPos")]
		private static extern bool Win32GetCursorPos(out POINT lpPoint);

		// Token: 0x060019BF RID: 6591
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetCursorPos")]
		private static extern bool Win32SetCursorPos(int x, int y);

		// Token: 0x060019C0 RID: 6592
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "TrackMouseEvent")]
		private static extern bool Win32TrackMouseEvent(ref XplatUIWin32.TRACKMOUSEEVENT tme);

		// Token: 0x060019C1 RID: 6593
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetWindowLong")]
		private static extern uint Win32SetWindowLong(IntPtr hwnd, XplatUIWin32.WindowLong index, uint value);

		// Token: 0x060019C2 RID: 6594
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetWindowLong")]
		private static extern uint Win32GetWindowLong(IntPtr hwnd, XplatUIWin32.WindowLong index);

		// Token: 0x060019C3 RID: 6595
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetLayeredWindowAttributes")]
		private static extern uint Win32SetLayeredWindowAttributes(IntPtr hwnd, XplatUIWin32.COLORREF crKey, byte bAlpha, XplatUIWin32.LayeredWindowAttributes dwFlags);

		// Token: 0x060019C4 RID: 6596
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetLayeredWindowAttributes")]
		private static extern uint Win32GetLayeredWindowAttributes(IntPtr hwnd, out XplatUIWin32.COLORREF pcrKey, out byte pbAlpha, out XplatUIWin32.LayeredWindowAttributes pwdFlags);

		// Token: 0x060019C5 RID: 6597
		[DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "DeleteObject")]
		public static extern bool Win32DeleteObject(IntPtr o);

		// Token: 0x060019C6 RID: 6598
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetKeyState")]
		private static extern short Win32GetKeyState(VirtualKeys nVirtKey);

		// Token: 0x060019C7 RID: 6599
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetDesktopWindow")]
		private static extern IntPtr Win32GetDesktopWindow();

		// Token: 0x060019C8 RID: 6600
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetTimer")]
		private static extern IntPtr Win32SetTimer(IntPtr hwnd, int nIDEvent, uint uElapse, IntPtr timerProc);

		// Token: 0x060019C9 RID: 6601
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "KillTimer")]
		private static extern IntPtr Win32KillTimer(IntPtr hwnd, int nIDEvent);

		// Token: 0x060019CA RID: 6602
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ShowWindow")]
		private static extern IntPtr Win32ShowWindow(IntPtr hwnd, XplatUIWin32.WindowPlacementFlags nCmdShow);

		// Token: 0x060019CB RID: 6603
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "EnableWindow")]
		private static extern IntPtr Win32EnableWindow(IntPtr hwnd, bool Enabled);

		// Token: 0x060019CC RID: 6604
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetFocus")]
		internal static extern IntPtr Win32SetFocus(IntPtr hwnd);

		// Token: 0x060019CD RID: 6605
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetFocus")]
		internal static extern IntPtr Win32GetFocus();

		// Token: 0x060019CE RID: 6606
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "CreateCaret")]
		internal static extern bool Win32CreateCaret(IntPtr hwnd, IntPtr hBitmap, int nWidth, int nHeight);

		// Token: 0x060019CF RID: 6607
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "DestroyCaret")]
		private static extern bool Win32DestroyCaret();

		// Token: 0x060019D0 RID: 6608
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ShowCaret")]
		private static extern bool Win32ShowCaret(IntPtr hwnd);

		// Token: 0x060019D1 RID: 6609
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "HideCaret")]
		private static extern bool Win32HideCaret(IntPtr hwnd);

		// Token: 0x060019D2 RID: 6610
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetCaretPos")]
		private static extern bool Win32SetCaretPos(int X, int Y);

		// Token: 0x060019D3 RID: 6611
		[DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "GetTextMetricsW")]
		internal static extern bool Win32GetTextMetrics(IntPtr hdc, ref XplatUIWin32.TEXTMETRIC tm);

		// Token: 0x060019D4 RID: 6612
		[DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SelectObject")]
		internal static extern IntPtr Win32SelectObject(IntPtr hdc, IntPtr hgdiobject);

		// Token: 0x060019D5 RID: 6613
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ScrollWindowEx")]
		private static extern bool Win32ScrollWindowEx(IntPtr hwnd, int dx, int dy, IntPtr prcScroll, ref XplatUIWin32.RECT prcClip, IntPtr hrgnUpdate, IntPtr prcUpdate, XplatUIWin32.ScrollWindowExFlags flags);

		// Token: 0x060019D6 RID: 6614
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ScrollWindowEx")]
		private static extern bool Win32ScrollWindowEx(IntPtr hwnd, int dx, int dy, IntPtr prcScroll, IntPtr prcClip, IntPtr hrgnUpdate, IntPtr prcUpdate, XplatUIWin32.ScrollWindowExFlags flags);

		// Token: 0x060019D7 RID: 6615
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetActiveWindow")]
		private static extern IntPtr Win32GetActiveWindow();

		// Token: 0x060019D8 RID: 6616
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetSystemMetrics")]
		private static extern int Win32GetSystemMetrics(XplatUIWin32.SystemMetrics nIndex);

		// Token: 0x060019D9 RID: 6617
		[DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "CreateRectRgn")]
		internal static extern IntPtr Win32CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

		// Token: 0x060019DA RID: 6618
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
		private static extern bool IsWindowEnabled(IntPtr hwnd);

		// Token: 0x060019DB RID: 6619
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
		private static extern bool IsWindow(IntPtr hwnd);

		// Token: 0x060019DC RID: 6620
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "SendMessageW")]
		private static extern IntPtr Win32SendMessage(IntPtr hwnd, Msg msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x060019DD RID: 6621
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "PostMessageW")]
		private static extern bool Win32PostMessage(IntPtr hwnd, Msg msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x060019DE RID: 6622
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "SystemParametersInfoW")]
		private static extern bool Win32SystemParametersInfo(XplatUIWin32.SPIAction uiAction, uint uiParam, ref XplatUIWin32.RECT rect, uint fWinIni);

		// Token: 0x060019DF RID: 6623
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "SystemParametersInfoW")]
		private static extern bool Win32SystemParametersInfo(XplatUIWin32.SPIAction uiAction, uint uiParam, ref int value, uint fWinIni);

		// Token: 0x060019E0 RID: 6624
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "OpenClipboard")]
		private static extern bool Win32OpenClipboard(IntPtr hwnd);

		// Token: 0x060019E1 RID: 6625
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "EmptyClipboard")]
		private static extern bool Win32EmptyClipboard();

		// Token: 0x060019E2 RID: 6626
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "RegisterClipboardFormatW")]
		private static extern uint Win32RegisterClipboardFormat(string format);

		// Token: 0x060019E3 RID: 6627
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "CloseClipboard")]
		private static extern bool Win32CloseClipboard();

		// Token: 0x060019E4 RID: 6628
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "EnumClipboardFormats")]
		private static extern uint Win32EnumClipboardFormats(uint format);

		// Token: 0x060019E5 RID: 6629
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetClipboardData")]
		private static extern IntPtr Win32GetClipboardData(uint format);

		// Token: 0x060019E6 RID: 6630
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetClipboardData")]
		private static extern IntPtr Win32SetClipboardData(uint format, IntPtr handle);

		// Token: 0x060019E7 RID: 6631
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GlobalAlloc")]
		internal static extern IntPtr Win32GlobalAlloc(XplatUIWin32.GAllocFlags Flags, int dwBytes);

		// Token: 0x060019E8 RID: 6632
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "CopyMemory")]
		internal static extern void Win32CopyMemory(IntPtr Destination, IntPtr Source, int length);

		// Token: 0x060019E9 RID: 6633
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GlobalSize")]
		internal static extern uint Win32GlobalSize(IntPtr hMem);

		// Token: 0x060019EA RID: 6634
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GlobalLock")]
		internal static extern IntPtr Win32GlobalLock(IntPtr hMem);

		// Token: 0x060019EB RID: 6635
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GlobalUnlock")]
		internal static extern IntPtr Win32GlobalUnlock(IntPtr hMem);

		// Token: 0x060019EC RID: 6636
		[DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetROP2")]
		internal static extern int Win32SetROP2(IntPtr hdc, XplatUIWin32.ROP2DrawMode fnDrawMode);

		// Token: 0x060019ED RID: 6637
		[DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "MoveToEx")]
		internal static extern bool Win32MoveToEx(IntPtr hdc, int x, int y, IntPtr lpPoint);

		// Token: 0x060019EE RID: 6638
		[DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "LineTo")]
		internal static extern bool Win32LineTo(IntPtr hdc, int x, int y);

		// Token: 0x060019EF RID: 6639
		[DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "CreatePen")]
		internal static extern IntPtr Win32CreatePen(XplatUIWin32.PenStyle fnPenStyle, int nWidth, IntPtr color);

		// Token: 0x060019F0 RID: 6640
		[DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ExcludeClipRect")]
		internal static extern int Win32ExcludeClipRect(IntPtr hdc, int left, int top, int right, int bottom);

		// Token: 0x060019F1 RID: 6641
		[DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ExtSelectClipRgn")]
		internal static extern int Win32ExtSelectClipRgn(IntPtr hdc, IntPtr hrgn, int mode);

		// Token: 0x060019F2 RID: 6642
		[DllImport("winmm.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "PlaySoundW")]
		internal static extern IntPtr Win32PlaySound(string pszSound, IntPtr hmod, XplatUIWin32.SndFlags fdwSound);

		// Token: 0x060019F3 RID: 6643
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "GetDoubleClickTime")]
		private static extern int Win32GetDoubleClickTime();

		// Token: 0x060019F4 RID: 6644
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "SetWindowRgn")]
		internal static extern int Win32SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);

		// Token: 0x060019F5 RID: 6645
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "ClipCursor")]
		internal static extern bool Win32ClipCursor(ref XplatUIWin32.RECT lpRect);

		// Token: 0x060019F6 RID: 6646
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetClipCursor")]
		internal static extern bool Win32GetClipCursor(out XplatUIWin32.RECT lpRect);

		// Token: 0x060019F7 RID: 6647
		[DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "BitBlt")]
		internal static extern bool Win32BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, XplatUIWin32.TernaryRasterOperations dwRop);

		// Token: 0x060019F8 RID: 6648
		[DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "CreateCompatibleDC", ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr Win32CreateCompatibleDC(IntPtr hdc);

		// Token: 0x060019F9 RID: 6649
		[DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "DeleteDC", ExactSpelling = true, SetLastError = true)]
		internal static extern bool Win32DeleteDC(IntPtr hdc);

		// Token: 0x060019FA RID: 6650
		[DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "CreateCompatibleBitmap")]
		internal static extern IntPtr Win32CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

		// Token: 0x060019FB RID: 6651
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetIconInfo")]
		internal static extern bool Win32GetIconInfo(IntPtr hIcon, out XplatUIWin32.ICONINFO piconinfo);

		// Token: 0x0400148A RID: 5258
		private static XplatUIWin32 instance;

		// Token: 0x0400148B RID: 5259
		private static int ref_count;

		// Token: 0x0400148C RID: 5260
		private static IntPtr FosterParentLast;

		// Token: 0x0400148D RID: 5261
		internal static MouseButtons mouse_state;

		// Token: 0x0400148E RID: 5262
		internal static Point mouse_position;

		// Token: 0x0400148F RID: 5263
		internal static bool grab_confined;

		// Token: 0x04001490 RID: 5264
		internal static IntPtr grab_hwnd;

		// Token: 0x04001491 RID: 5265
		internal static Rectangle grab_area;

		// Token: 0x04001492 RID: 5266
		internal static XplatUIDriver.WndProc wnd_proc;

		// Token: 0x04001493 RID: 5267
		internal static IntPtr prev_mouse_hwnd;

		// Token: 0x04001494 RID: 5268
		internal static bool caret_visible;

		// Token: 0x04001495 RID: 5269
		internal static bool themes_enabled;

		// Token: 0x04001496 RID: 5270
		private Hashtable timer_list;

		// Token: 0x04001497 RID: 5271
		private static Queue message_queue;

		// Token: 0x04001498 RID: 5272
		private static IntPtr clip_magic = new IntPtr(27051977);

		// Token: 0x04001499 RID: 5273
		private static int scroll_width;

		// Token: 0x0400149A RID: 5274
		private static int scroll_height;

		// Token: 0x0400149B RID: 5275
		private static Hashtable wm_nc_registered;

		// Token: 0x0400149C RID: 5276
		private static XplatUIWin32.RECT clipped_cursor_rect;

		// Token: 0x0400149D RID: 5277
		private Hashtable registered_classes;

		// Token: 0x0400149E RID: 5278
		private Hwnd HwndCreating;

		// Token: 0x0400149F RID: 5279
		private TransparencySupport support;

		// Token: 0x040014A0 RID: 5280
		private bool queried_transparency_support;

		// Token: 0x020002B0 RID: 688
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct WNDCLASS
		{
			// Token: 0x040014A1 RID: 5281
			internal int style;

			// Token: 0x040014A2 RID: 5282
			internal XplatUIDriver.WndProc lpfnWndProc;

			// Token: 0x040014A3 RID: 5283
			internal int cbClsExtra;

			// Token: 0x040014A4 RID: 5284
			internal int cbWndExtra;

			// Token: 0x040014A5 RID: 5285
			internal IntPtr hInstance;

			// Token: 0x040014A6 RID: 5286
			internal IntPtr hIcon;

			// Token: 0x040014A7 RID: 5287
			internal IntPtr hCursor;

			// Token: 0x040014A8 RID: 5288
			internal IntPtr hbrBackground;

			// Token: 0x040014A9 RID: 5289
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string lpszMenuName;

			// Token: 0x040014AA RID: 5290
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string lpszClassName;
		}

		// Token: 0x020002B1 RID: 689
		internal struct RECT
		{
			// Token: 0x060019FD RID: 6653 RVA: 0x0007B912 File Offset: 0x00079B12
			public RECT(int left, int top, int right, int bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}

			// Token: 0x1700064F RID: 1615
			// (get) Token: 0x060019FE RID: 6654 RVA: 0x0007B931 File Offset: 0x00079B31
			public int Height
			{
				get
				{
					return this.bottom - this.top;
				}
			}

			// Token: 0x17000650 RID: 1616
			// (get) Token: 0x060019FF RID: 6655 RVA: 0x0007B940 File Offset: 0x00079B40
			public int Width
			{
				get
				{
					return this.right - this.left;
				}
			}

			// Token: 0x06001A00 RID: 6656 RVA: 0x0007B94F File Offset: 0x00079B4F
			public Rectangle ToRectangle()
			{
				return Rectangle.FromLTRB(this.left, this.top, this.right, this.bottom);
			}

			// Token: 0x06001A01 RID: 6657 RVA: 0x0007B96E File Offset: 0x00079B6E
			public static XplatUIWin32.RECT FromRectangle(Rectangle rectangle)
			{
				return new XplatUIWin32.RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
			}

			// Token: 0x06001A02 RID: 6658 RVA: 0x0007B994 File Offset: 0x00079B94
			public override int GetHashCode()
			{
				return this.left ^ ((this.top << 13) | (this.top >> 19)) ^ ((this.Width << 26) | (this.Width >> 6)) ^ ((this.Height << 7) | (this.Height >> 25));
			}

			// Token: 0x06001A03 RID: 6659 RVA: 0x0007B9E4 File Offset: 0x00079BE4
			public override string ToString()
			{
				return string.Format("RECT left={0}, top={1}, right={2}, bottom={3}, width={4}, height={5}", new object[]
				{
					this.left,
					this.top,
					this.right,
					this.bottom,
					this.right - this.left,
					this.bottom - this.top
				});
			}

			// Token: 0x040014AB RID: 5291
			internal int left;

			// Token: 0x040014AC RID: 5292
			internal int top;

			// Token: 0x040014AD RID: 5293
			internal int right;

			// Token: 0x040014AE RID: 5294
			internal int bottom;
		}

		// Token: 0x020002B2 RID: 690
		internal enum SPIAction
		{
			// Token: 0x040014B0 RID: 5296
			SPI_GETACTIVEWINDOWTRACKING = 4096,
			// Token: 0x040014B1 RID: 5297
			SPI_GETACTIVEWNDTRKTIMEOUT = 8194,
			// Token: 0x040014B2 RID: 5298
			SPI_GETANIMATION = 72,
			// Token: 0x040014B3 RID: 5299
			SPI_GETCARETWIDTH = 8198,
			// Token: 0x040014B4 RID: 5300
			SPI_GETCOMBOBOXANIMATION = 4100,
			// Token: 0x040014B5 RID: 5301
			SPI_GETDRAGFULLWINDOWS = 38,
			// Token: 0x040014B6 RID: 5302
			SPI_GETDROPSHADOW = 4132,
			// Token: 0x040014B7 RID: 5303
			SPI_GETFONTSMOOTHING = 74,
			// Token: 0x040014B8 RID: 5304
			SPI_GETFONTSMOOTHINGCONTRAST = 8204,
			// Token: 0x040014B9 RID: 5305
			SPI_GETFONTSMOOTHINGTYPE = 8202,
			// Token: 0x040014BA RID: 5306
			SPI_GETGRADIENTCAPTIONS = 4104,
			// Token: 0x040014BB RID: 5307
			SPI_GETHOTTRACKING = 4110,
			// Token: 0x040014BC RID: 5308
			SPI_GETICONTITLEWRAP = 25,
			// Token: 0x040014BD RID: 5309
			SPI_GETKEYBOARDSPEED = 10,
			// Token: 0x040014BE RID: 5310
			SPI_GETKEYBOARDDELAY = 22,
			// Token: 0x040014BF RID: 5311
			SPI_GETKEYBOARDCUES = 4106,
			// Token: 0x040014C0 RID: 5312
			SPI_GETKEYBOARDPREF = 68,
			// Token: 0x040014C1 RID: 5313
			SPI_GETLISTBOXSMOOTHSCROLLING = 4102,
			// Token: 0x040014C2 RID: 5314
			SPI_GETMENUANIMATION = 4098,
			// Token: 0x040014C3 RID: 5315
			SPI_GETMENUDROPALIGNMENT = 27,
			// Token: 0x040014C4 RID: 5316
			SPI_GETMENUFADE = 4114,
			// Token: 0x040014C5 RID: 5317
			SPI_GETMENUSHOWDELAY = 106,
			// Token: 0x040014C6 RID: 5318
			SPI_GETMOUSESPEED = 112,
			// Token: 0x040014C7 RID: 5319
			SPI_GETSELECTIONFADE = 4116,
			// Token: 0x040014C8 RID: 5320
			SPI_GETSNAPTODEFBUTTON = 95,
			// Token: 0x040014C9 RID: 5321
			SPI_GETTOOLTIPANIMATION = 4118,
			// Token: 0x040014CA RID: 5322
			SPI_GETWORKAREA = 48,
			// Token: 0x040014CB RID: 5323
			SPI_GETMOUSEHOVERWIDTH = 98,
			// Token: 0x040014CC RID: 5324
			SPI_GETMOUSEHOVERHEIGHT = 100,
			// Token: 0x040014CD RID: 5325
			SPI_GETMOUSEHOVERTIME = 102,
			// Token: 0x040014CE RID: 5326
			SPI_GETUIEFFECTS = 4158,
			// Token: 0x040014CF RID: 5327
			SPI_GETWHEELSCROLLLINES = 104
		}

		// Token: 0x020002B3 RID: 691
		internal enum WindowPlacementFlags
		{
			// Token: 0x040014D1 RID: 5329
			SW_HIDE,
			// Token: 0x040014D2 RID: 5330
			SW_SHOWNORMAL,
			// Token: 0x040014D3 RID: 5331
			SW_NORMAL = 1,
			// Token: 0x040014D4 RID: 5332
			SW_SHOWMINIMIZED,
			// Token: 0x040014D5 RID: 5333
			SW_SHOWMAXIMIZED,
			// Token: 0x040014D6 RID: 5334
			SW_MAXIMIZE = 3,
			// Token: 0x040014D7 RID: 5335
			SW_SHOWNOACTIVATE,
			// Token: 0x040014D8 RID: 5336
			SW_SHOW,
			// Token: 0x040014D9 RID: 5337
			SW_MINIMIZE,
			// Token: 0x040014DA RID: 5338
			SW_SHOWMINNOACTIVE,
			// Token: 0x040014DB RID: 5339
			SW_SHOWNA,
			// Token: 0x040014DC RID: 5340
			SW_RESTORE,
			// Token: 0x040014DD RID: 5341
			SW_SHOWDEFAULT,
			// Token: 0x040014DE RID: 5342
			SW_FORCEMINIMIZE,
			// Token: 0x040014DF RID: 5343
			SW_MAX = 11
		}

		// Token: 0x020002B4 RID: 692
		internal struct NCCALCSIZE_PARAMS
		{
			// Token: 0x040014E0 RID: 5344
			internal XplatUIWin32.RECT rgrc1;

			// Token: 0x040014E1 RID: 5345
			internal XplatUIWin32.RECT rgrc2;

			// Token: 0x040014E2 RID: 5346
			internal XplatUIWin32.RECT rgrc3;

			// Token: 0x040014E3 RID: 5347
			internal IntPtr lppos;
		}

		// Token: 0x020002B5 RID: 693
		[Flags]
		private enum TMEFlags
		{
			// Token: 0x040014E5 RID: 5349
			TME_HOVER = 1,
			// Token: 0x040014E6 RID: 5350
			TME_LEAVE = 2,
			// Token: 0x040014E7 RID: 5351
			TME_NONCLIENT = 16,
			// Token: 0x040014E8 RID: 5352
			TME_QUERY = 1073741824,
			// Token: 0x040014E9 RID: 5353
			TME_CANCEL = -2147483648
		}

		// Token: 0x020002B6 RID: 694
		private struct TRACKMOUSEEVENT
		{
			// Token: 0x040014EA RID: 5354
			internal int size;

			// Token: 0x040014EB RID: 5355
			internal XplatUIWin32.TMEFlags dwFlags;

			// Token: 0x040014EC RID: 5356
			internal IntPtr hWnd;

			// Token: 0x040014ED RID: 5357
			internal int dwHoverTime;
		}

		// Token: 0x020002B7 RID: 695
		private struct PAINTSTRUCT
		{
			// Token: 0x040014EE RID: 5358
			internal IntPtr hdc;

			// Token: 0x040014EF RID: 5359
			internal int fErase;

			// Token: 0x040014F0 RID: 5360
			internal XplatUIWin32.RECT rcPaint;

			// Token: 0x040014F1 RID: 5361
			internal int fRestore;

			// Token: 0x040014F2 RID: 5362
			internal int fIncUpdate;

			// Token: 0x040014F3 RID: 5363
			internal int Reserved1;

			// Token: 0x040014F4 RID: 5364
			internal int Reserved2;

			// Token: 0x040014F5 RID: 5365
			internal int Reserved3;

			// Token: 0x040014F6 RID: 5366
			internal int Reserved4;

			// Token: 0x040014F7 RID: 5367
			internal int Reserved5;

			// Token: 0x040014F8 RID: 5368
			internal int Reserved6;

			// Token: 0x040014F9 RID: 5369
			internal int Reserved7;

			// Token: 0x040014FA RID: 5370
			internal int Reserved8;
		}

		// Token: 0x020002B8 RID: 696
		internal struct ICONINFO
		{
			// Token: 0x040014FB RID: 5371
			internal bool fIcon;

			// Token: 0x040014FC RID: 5372
			internal int xHotspot;

			// Token: 0x040014FD RID: 5373
			internal int yHotspot;

			// Token: 0x040014FE RID: 5374
			internal IntPtr hbmMask;

			// Token: 0x040014FF RID: 5375
			internal IntPtr hbmColor;
		}

		// Token: 0x020002B9 RID: 697
		internal enum SetWindowPosZOrder
		{
			// Token: 0x04001501 RID: 5377
			HWND_TOP,
			// Token: 0x04001502 RID: 5378
			HWND_BOTTOM,
			// Token: 0x04001503 RID: 5379
			HWND_TOPMOST = -1,
			// Token: 0x04001504 RID: 5380
			HWND_NOTOPMOST = -2
		}

		// Token: 0x020002BA RID: 698
		[Flags]
		internal enum SetWindowPosFlags
		{
			// Token: 0x04001506 RID: 5382
			SWP_ASYNCWINDOWPOS = 16384,
			// Token: 0x04001507 RID: 5383
			SWP_DEFERERASE = 8192,
			// Token: 0x04001508 RID: 5384
			SWP_DRAWFRAME = 32,
			// Token: 0x04001509 RID: 5385
			SWP_FRAMECHANGED = 32,
			// Token: 0x0400150A RID: 5386
			SWP_HIDEWINDOW = 128,
			// Token: 0x0400150B RID: 5387
			SWP_NOACTIVATE = 16,
			// Token: 0x0400150C RID: 5388
			SWP_NOCOPYBITS = 256,
			// Token: 0x0400150D RID: 5389
			SWP_NOMOVE = 2,
			// Token: 0x0400150E RID: 5390
			SWP_NOOWNERZORDER = 512,
			// Token: 0x0400150F RID: 5391
			SWP_NOREDRAW = 8,
			// Token: 0x04001510 RID: 5392
			SWP_NOREPOSITION = 512,
			// Token: 0x04001511 RID: 5393
			SWP_NOENDSCHANGING = 1024,
			// Token: 0x04001512 RID: 5394
			SWP_NOSIZE = 1,
			// Token: 0x04001513 RID: 5395
			SWP_NOZORDER = 4,
			// Token: 0x04001514 RID: 5396
			SWP_SHOWWINDOW = 64
		}

		// Token: 0x020002BB RID: 699
		private enum LoadCursorType
		{
			// Token: 0x04001516 RID: 5398
			First = 32512,
			// Token: 0x04001517 RID: 5399
			IDC_ARROW = 32512,
			// Token: 0x04001518 RID: 5400
			IDC_IBEAM,
			// Token: 0x04001519 RID: 5401
			IDC_WAIT,
			// Token: 0x0400151A RID: 5402
			IDC_CROSS,
			// Token: 0x0400151B RID: 5403
			IDC_UPARROW,
			// Token: 0x0400151C RID: 5404
			IDC_SIZE = 32640,
			// Token: 0x0400151D RID: 5405
			IDC_ICON,
			// Token: 0x0400151E RID: 5406
			IDC_SIZENWSE,
			// Token: 0x0400151F RID: 5407
			IDC_SIZENESW,
			// Token: 0x04001520 RID: 5408
			IDC_SIZEWE,
			// Token: 0x04001521 RID: 5409
			IDC_SIZENS,
			// Token: 0x04001522 RID: 5410
			IDC_SIZEALL,
			// Token: 0x04001523 RID: 5411
			IDC_NO = 32648,
			// Token: 0x04001524 RID: 5412
			IDC_HAND,
			// Token: 0x04001525 RID: 5413
			IDC_APPSTARTING,
			// Token: 0x04001526 RID: 5414
			IDC_HELP,
			// Token: 0x04001527 RID: 5415
			Last = 32651
		}

		// Token: 0x020002BC RID: 700
		private enum AncestorType
		{
			// Token: 0x04001529 RID: 5417
			GA_PARENT = 1,
			// Token: 0x0400152A RID: 5418
			GA_ROOT,
			// Token: 0x0400152B RID: 5419
			GA_ROOTOWNER
		}

		// Token: 0x020002BD RID: 701
		[Flags]
		private enum WindowLong
		{
			// Token: 0x0400152D RID: 5421
			GWL_WNDPROC = -4,
			// Token: 0x0400152E RID: 5422
			GWL_HINSTANCE = -6,
			// Token: 0x0400152F RID: 5423
			GWL_HWNDPARENT = -8,
			// Token: 0x04001530 RID: 5424
			GWL_STYLE = -16,
			// Token: 0x04001531 RID: 5425
			GWL_EXSTYLE = -20,
			// Token: 0x04001532 RID: 5426
			GWL_USERDATA = -21,
			// Token: 0x04001533 RID: 5427
			GWL_ID = -12
		}

		// Token: 0x020002BE RID: 702
		internal struct COLORREF
		{
			// Token: 0x04001534 RID: 5428
			internal byte R;

			// Token: 0x04001535 RID: 5429
			internal byte G;

			// Token: 0x04001536 RID: 5430
			internal byte B;

			// Token: 0x04001537 RID: 5431
			internal byte A;
		}

		// Token: 0x020002BF RID: 703
		internal struct TEXTMETRIC
		{
			// Token: 0x04001538 RID: 5432
			internal int tmHeight;

			// Token: 0x04001539 RID: 5433
			internal int tmAscent;

			// Token: 0x0400153A RID: 5434
			internal int tmDescent;

			// Token: 0x0400153B RID: 5435
			internal int tmInternalLeading;

			// Token: 0x0400153C RID: 5436
			internal int tmExternalLeading;

			// Token: 0x0400153D RID: 5437
			internal int tmAveCharWidth;

			// Token: 0x0400153E RID: 5438
			internal int tmMaxCharWidth;

			// Token: 0x0400153F RID: 5439
			internal int tmWeight;

			// Token: 0x04001540 RID: 5440
			internal int tmOverhang;

			// Token: 0x04001541 RID: 5441
			internal int tmDigitizedAspectX;

			// Token: 0x04001542 RID: 5442
			internal int tmDigitizedAspectY;

			// Token: 0x04001543 RID: 5443
			internal short tmFirstChar;

			// Token: 0x04001544 RID: 5444
			internal short tmLastChar;

			// Token: 0x04001545 RID: 5445
			internal short tmDefaultChar;

			// Token: 0x04001546 RID: 5446
			internal short tmBreakChar;

			// Token: 0x04001547 RID: 5447
			internal byte tmItalic;

			// Token: 0x04001548 RID: 5448
			internal byte tmUnderlined;

			// Token: 0x04001549 RID: 5449
			internal byte tmStruckOut;

			// Token: 0x0400154A RID: 5450
			internal byte tmPitchAndFamily;

			// Token: 0x0400154B RID: 5451
			internal byte tmCharSet;
		}

		// Token: 0x020002C0 RID: 704
		public enum TernaryRasterOperations : uint
		{
			// Token: 0x0400154D RID: 5453
			SRCCOPY = 13369376U,
			// Token: 0x0400154E RID: 5454
			SRCPAINT = 15597702U,
			// Token: 0x0400154F RID: 5455
			SRCAND = 8913094U,
			// Token: 0x04001550 RID: 5456
			SRCINVERT = 6684742U,
			// Token: 0x04001551 RID: 5457
			SRCERASE = 4457256U,
			// Token: 0x04001552 RID: 5458
			NOTSRCCOPY = 3342344U,
			// Token: 0x04001553 RID: 5459
			NOTSRCERASE = 1114278U,
			// Token: 0x04001554 RID: 5460
			MERGECOPY = 12583114U,
			// Token: 0x04001555 RID: 5461
			MERGEPAINT = 12255782U,
			// Token: 0x04001556 RID: 5462
			PATCOPY = 15728673U,
			// Token: 0x04001557 RID: 5463
			PATPAINT = 16452105U,
			// Token: 0x04001558 RID: 5464
			PATINVERT = 5898313U,
			// Token: 0x04001559 RID: 5465
			DSTINVERT = 5570569U,
			// Token: 0x0400155A RID: 5466
			BLACKNESS = 66U,
			// Token: 0x0400155B RID: 5467
			WHITENESS = 16711778U
		}

		// Token: 0x020002C1 RID: 705
		[Flags]
		private enum ScrollWindowExFlags
		{
			// Token: 0x0400155D RID: 5469
			SW_NONE = 0,
			// Token: 0x0400155E RID: 5470
			SW_SCROLLCHILDREN = 1,
			// Token: 0x0400155F RID: 5471
			SW_INVALIDATE = 2,
			// Token: 0x04001560 RID: 5472
			SW_ERASE = 4,
			// Token: 0x04001561 RID: 5473
			SW_SMOOTHSCROLL = 16
		}

		// Token: 0x020002C2 RID: 706
		internal enum SystemMetrics
		{
			// Token: 0x04001563 RID: 5475
			SM_CXSCREEN,
			// Token: 0x04001564 RID: 5476
			SM_CYSCREEN,
			// Token: 0x04001565 RID: 5477
			SM_CXVSCROLL,
			// Token: 0x04001566 RID: 5478
			SM_CYHSCROLL,
			// Token: 0x04001567 RID: 5479
			SM_CYCAPTION,
			// Token: 0x04001568 RID: 5480
			SM_CXBORDER,
			// Token: 0x04001569 RID: 5481
			SM_CYBORDER,
			// Token: 0x0400156A RID: 5482
			SM_CXDLGFRAME,
			// Token: 0x0400156B RID: 5483
			SM_CYDLGFRAME,
			// Token: 0x0400156C RID: 5484
			SM_CYVTHUMB,
			// Token: 0x0400156D RID: 5485
			SM_CXHTHUMB,
			// Token: 0x0400156E RID: 5486
			SM_CXICON,
			// Token: 0x0400156F RID: 5487
			SM_CYICON,
			// Token: 0x04001570 RID: 5488
			SM_CXCURSOR,
			// Token: 0x04001571 RID: 5489
			SM_CYCURSOR,
			// Token: 0x04001572 RID: 5490
			SM_CYMENU,
			// Token: 0x04001573 RID: 5491
			SM_CXFULLSCREEN,
			// Token: 0x04001574 RID: 5492
			SM_CYFULLSCREEN,
			// Token: 0x04001575 RID: 5493
			SM_CYKANJIWINDOW,
			// Token: 0x04001576 RID: 5494
			SM_MOUSEPRESENT,
			// Token: 0x04001577 RID: 5495
			SM_CYVSCROLL,
			// Token: 0x04001578 RID: 5496
			SM_CXHSCROLL,
			// Token: 0x04001579 RID: 5497
			SM_DEBUG,
			// Token: 0x0400157A RID: 5498
			SM_SWAPBUTTON,
			// Token: 0x0400157B RID: 5499
			SM_RESERVED1,
			// Token: 0x0400157C RID: 5500
			SM_RESERVED2,
			// Token: 0x0400157D RID: 5501
			SM_RESERVED3,
			// Token: 0x0400157E RID: 5502
			SM_RESERVED4,
			// Token: 0x0400157F RID: 5503
			SM_CXMIN,
			// Token: 0x04001580 RID: 5504
			SM_CYMIN,
			// Token: 0x04001581 RID: 5505
			SM_CXSIZE,
			// Token: 0x04001582 RID: 5506
			SM_CYSIZE,
			// Token: 0x04001583 RID: 5507
			SM_CXFRAME,
			// Token: 0x04001584 RID: 5508
			SM_CYFRAME,
			// Token: 0x04001585 RID: 5509
			SM_CXMINTRACK,
			// Token: 0x04001586 RID: 5510
			SM_CYMINTRACK,
			// Token: 0x04001587 RID: 5511
			SM_CXDOUBLECLK,
			// Token: 0x04001588 RID: 5512
			SM_CYDOUBLECLK,
			// Token: 0x04001589 RID: 5513
			SM_CXICONSPACING,
			// Token: 0x0400158A RID: 5514
			SM_CYICONSPACING,
			// Token: 0x0400158B RID: 5515
			SM_MENUDROPALIGNMENT,
			// Token: 0x0400158C RID: 5516
			SM_PENWINDOWS,
			// Token: 0x0400158D RID: 5517
			SM_DBCSENABLED,
			// Token: 0x0400158E RID: 5518
			SM_CMOUSEBUTTONS,
			// Token: 0x0400158F RID: 5519
			SM_CXFIXEDFRAME = 7,
			// Token: 0x04001590 RID: 5520
			SM_CYFIXEDFRAME,
			// Token: 0x04001591 RID: 5521
			SM_CXSIZEFRAME = 32,
			// Token: 0x04001592 RID: 5522
			SM_CYSIZEFRAME,
			// Token: 0x04001593 RID: 5523
			SM_SECURE = 44,
			// Token: 0x04001594 RID: 5524
			SM_CXEDGE,
			// Token: 0x04001595 RID: 5525
			SM_CYEDGE,
			// Token: 0x04001596 RID: 5526
			SM_CXMINSPACING,
			// Token: 0x04001597 RID: 5527
			SM_CYMINSPACING,
			// Token: 0x04001598 RID: 5528
			SM_CXSMICON,
			// Token: 0x04001599 RID: 5529
			SM_CYSMICON,
			// Token: 0x0400159A RID: 5530
			SM_CYSMCAPTION,
			// Token: 0x0400159B RID: 5531
			SM_CXSMSIZE,
			// Token: 0x0400159C RID: 5532
			SM_CYSMSIZE,
			// Token: 0x0400159D RID: 5533
			SM_CXMENUSIZE,
			// Token: 0x0400159E RID: 5534
			SM_CYMENUSIZE,
			// Token: 0x0400159F RID: 5535
			SM_ARRANGE,
			// Token: 0x040015A0 RID: 5536
			SM_CXMINIMIZED,
			// Token: 0x040015A1 RID: 5537
			SM_CYMINIMIZED,
			// Token: 0x040015A2 RID: 5538
			SM_CXMAXTRACK,
			// Token: 0x040015A3 RID: 5539
			SM_CYMAXTRACK,
			// Token: 0x040015A4 RID: 5540
			SM_CXMAXIMIZED,
			// Token: 0x040015A5 RID: 5541
			SM_CYMAXIMIZED,
			// Token: 0x040015A6 RID: 5542
			SM_NETWORK,
			// Token: 0x040015A7 RID: 5543
			SM_CLEANBOOT = 67,
			// Token: 0x040015A8 RID: 5544
			SM_CXDRAG,
			// Token: 0x040015A9 RID: 5545
			SM_CYDRAG,
			// Token: 0x040015AA RID: 5546
			SM_SHOWSOUNDS,
			// Token: 0x040015AB RID: 5547
			SM_CXMENUCHECK,
			// Token: 0x040015AC RID: 5548
			SM_CYMENUCHECK,
			// Token: 0x040015AD RID: 5549
			SM_SLOWMACHINE,
			// Token: 0x040015AE RID: 5550
			SM_MIDEASTENABLED,
			// Token: 0x040015AF RID: 5551
			SM_MOUSEWHEELPRESENT,
			// Token: 0x040015B0 RID: 5552
			SM_XVIRTUALSCREEN,
			// Token: 0x040015B1 RID: 5553
			SM_YVIRTUALSCREEN,
			// Token: 0x040015B2 RID: 5554
			SM_CXVIRTUALSCREEN,
			// Token: 0x040015B3 RID: 5555
			SM_CYVIRTUALSCREEN,
			// Token: 0x040015B4 RID: 5556
			SM_CMONITORS,
			// Token: 0x040015B5 RID: 5557
			SM_SAMEDISPLAYFORMAT,
			// Token: 0x040015B6 RID: 5558
			SM_IMMENABLED,
			// Token: 0x040015B7 RID: 5559
			SM_CXFOCUSBORDER,
			// Token: 0x040015B8 RID: 5560
			SM_CYFOCUSBORDER,
			// Token: 0x040015B9 RID: 5561
			SM_TABLETPC = 86,
			// Token: 0x040015BA RID: 5562
			SM_MEDIACENTER,
			// Token: 0x040015BB RID: 5563
			SM_CMETRICS
		}

		// Token: 0x020002C3 RID: 707
		[Flags]
		internal enum GAllocFlags : uint
		{
			// Token: 0x040015BD RID: 5565
			GMEM_FIXED = 0U,
			// Token: 0x040015BE RID: 5566
			GMEM_MOVEABLE = 2U,
			// Token: 0x040015BF RID: 5567
			GMEM_NOCOMPACT = 16U,
			// Token: 0x040015C0 RID: 5568
			GMEM_NODISCARD = 32U,
			// Token: 0x040015C1 RID: 5569
			GMEM_ZEROINIT = 64U,
			// Token: 0x040015C2 RID: 5570
			GMEM_MODIFY = 128U,
			// Token: 0x040015C3 RID: 5571
			GMEM_DISCARDABLE = 256U,
			// Token: 0x040015C4 RID: 5572
			GMEM_NOT_BANKED = 4096U,
			// Token: 0x040015C5 RID: 5573
			GMEM_SHARE = 8192U,
			// Token: 0x040015C6 RID: 5574
			GMEM_DDESHARE = 8192U,
			// Token: 0x040015C7 RID: 5575
			GMEM_NOTIFY = 16384U,
			// Token: 0x040015C8 RID: 5576
			GMEM_LOWER = 4096U,
			// Token: 0x040015C9 RID: 5577
			GMEM_VALID_FLAGS = 32626U,
			// Token: 0x040015CA RID: 5578
			GMEM_INVALID_HANDLE = 32768U,
			// Token: 0x040015CB RID: 5579
			GHND = 66U,
			// Token: 0x040015CC RID: 5580
			GPTR = 64U
		}

		// Token: 0x020002C4 RID: 708
		internal enum ROP2DrawMode
		{
			// Token: 0x040015CE RID: 5582
			R2_BLACK = 1,
			// Token: 0x040015CF RID: 5583
			R2_NOTMERGEPEN,
			// Token: 0x040015D0 RID: 5584
			R2_MASKNOTPEN,
			// Token: 0x040015D1 RID: 5585
			R2_NOTCOPYPEN,
			// Token: 0x040015D2 RID: 5586
			R2_MASKPENNOT,
			// Token: 0x040015D3 RID: 5587
			R2_NOT,
			// Token: 0x040015D4 RID: 5588
			R2_XORPEN,
			// Token: 0x040015D5 RID: 5589
			R2_NOTMASKPEN,
			// Token: 0x040015D6 RID: 5590
			R2_MASKPEN,
			// Token: 0x040015D7 RID: 5591
			R2_NOTXORPEN,
			// Token: 0x040015D8 RID: 5592
			R2_NOP,
			// Token: 0x040015D9 RID: 5593
			R2_MERGENOTPEN,
			// Token: 0x040015DA RID: 5594
			R2_COPYPEN,
			// Token: 0x040015DB RID: 5595
			R2_MERGEPENNOT,
			// Token: 0x040015DC RID: 5596
			R2_MERGEPEN,
			// Token: 0x040015DD RID: 5597
			R2_WHITE,
			// Token: 0x040015DE RID: 5598
			R2_LAST = 16
		}

		// Token: 0x020002C5 RID: 709
		internal enum PenStyle
		{
			// Token: 0x040015E0 RID: 5600
			PS_SOLID,
			// Token: 0x040015E1 RID: 5601
			PS_DASH,
			// Token: 0x040015E2 RID: 5602
			PS_DOT,
			// Token: 0x040015E3 RID: 5603
			PS_DASHDOT,
			// Token: 0x040015E4 RID: 5604
			PS_DASHDOTDOT,
			// Token: 0x040015E5 RID: 5605
			PS_NULL,
			// Token: 0x040015E6 RID: 5606
			PS_INSIDEFRAME,
			// Token: 0x040015E7 RID: 5607
			PS_USERSTYLE,
			// Token: 0x040015E8 RID: 5608
			PS_ALTERNATE
		}

		// Token: 0x020002C6 RID: 710
		[Flags]
		internal enum SndFlags
		{
			// Token: 0x040015EA RID: 5610
			SND_SYNC = 0,
			// Token: 0x040015EB RID: 5611
			SND_ASYNC = 1,
			// Token: 0x040015EC RID: 5612
			SND_NODEFAULT = 2,
			// Token: 0x040015ED RID: 5613
			SND_MEMORY = 4,
			// Token: 0x040015EE RID: 5614
			SND_LOOP = 8,
			// Token: 0x040015EF RID: 5615
			SND_NOSTOP = 16,
			// Token: 0x040015F0 RID: 5616
			SND_NOWAIT = 8192,
			// Token: 0x040015F1 RID: 5617
			SND_ALIAS = 65536,
			// Token: 0x040015F2 RID: 5618
			SND_ALIAS_ID = 1114112,
			// Token: 0x040015F3 RID: 5619
			SND_FILENAME = 131072,
			// Token: 0x040015F4 RID: 5620
			SND_RESOURCE = 262148,
			// Token: 0x040015F5 RID: 5621
			SND_PURGE = 64,
			// Token: 0x040015F6 RID: 5622
			SND_APPLICATION = 128
		}

		// Token: 0x020002C7 RID: 711
		[Flags]
		internal enum LayeredWindowAttributes
		{
			// Token: 0x040015F8 RID: 5624
			LWA_COLORKEY = 1,
			// Token: 0x040015F9 RID: 5625
			LWA_ALPHA = 2
		}

		// Token: 0x020002C8 RID: 712
		private class WinBuffer
		{
			// Token: 0x06001A04 RID: 6660 RVA: 0x0007BA63 File Offset: 0x00079C63
			public WinBuffer(IntPtr hdc, IntPtr bitmap)
			{
				this.hdc = hdc;
				this.bitmap = bitmap;
			}

			// Token: 0x040015FA RID: 5626
			public IntPtr hdc;

			// Token: 0x040015FB RID: 5627
			public IntPtr bitmap;
		}
	}
}
