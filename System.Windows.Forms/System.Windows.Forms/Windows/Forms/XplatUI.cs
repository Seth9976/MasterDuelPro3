using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000294 RID: 660
	internal class XplatUI
	{
		// Token: 0x0600175D RID: 5981 RVA: 0x00075060 File Offset: 0x00073260
		static XplatUI()
		{
			if (XplatUI.RunningOnUnix)
			{
				if (Environment.GetEnvironmentVariable("MONO_MWF_MAC_FORCE_X11") != null)
				{
					XplatUI.driver = XplatUIX11.GetInstance();
				}
				else
				{
					IntPtr intPtr = Marshal.AllocHGlobal(8192);
					if (XplatUI.uname(intPtr) != 0)
					{
						XplatUI.driver = XplatUIX11.GetInstance();
					}
					else if (Marshal.PtrToStringAnsi(intPtr) == "Darwin")
					{
						XplatUI.driver = XplatUICarbon.GetInstance();
					}
					else
					{
						XplatUI.driver = XplatUIX11.GetInstance();
					}
					Marshal.FreeHGlobal(intPtr);
				}
			}
			else
			{
				XplatUI.driver = XplatUIWin32.GetInstance();
			}
			XplatUI.driver.InitializeDriver();
			DataFormats.GetFormat(0);
			Application.FirePreRun();
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x00075108 File Offset: 0x00073308
		public static bool RunningOnUnix
		{
			get
			{
				int platform = (int)Environment.OSVersion.Platform;
				return platform == 4 || platform == 6 || platform == 128;
			}
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00075134 File Offset: 0x00073334
		internal static string GetDefaultClassName(Type type)
		{
			return "SWFClass" + Thread.GetDomainID().ToString() + "." + type.ToString();
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x00075163 File Offset: 0x00073363
		public static Size Border3DSize
		{
			get
			{
				return XplatUI.driver.Border3DSize;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x0007516F File Offset: 0x0007336F
		public static Size BorderSize
		{
			get
			{
				return XplatUI.driver.BorderSize;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x0007517B File Offset: 0x0007337B
		public static Size CaptionButtonSize
		{
			get
			{
				return XplatUI.driver.CaptionButtonSize;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x00075187 File Offset: 0x00073387
		public static int CaptionHeight
		{
			get
			{
				return XplatUI.driver.CaptionHeight;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x00075193 File Offset: 0x00073393
		public static int DoubleClickTime
		{
			get
			{
				return XplatUI.driver.DoubleClickTime;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x0007519F File Offset: 0x0007339F
		public static Size DragSize
		{
			get
			{
				return XplatUI.driver.DragSize;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x000751AB File Offset: 0x000733AB
		public static Size FrameBorderSize
		{
			get
			{
				return XplatUI.driver.FrameBorderSize;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x000751B7 File Offset: 0x000733B7
		public static int HorizontalScrollBarHeight
		{
			get
			{
				return XplatUI.driver.HorizontalScrollBarHeight;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x000751C3 File Offset: 0x000733C3
		public static bool MenuAccessKeysUnderlined
		{
			get
			{
				return XplatUI.driver.MenuAccessKeysUnderlined;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x000751CF File Offset: 0x000733CF
		public static Size MenuButtonSize
		{
			get
			{
				return XplatUI.driver.MenuButtonSize;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x000751DB File Offset: 0x000733DB
		public static Size MinimizedWindowSize
		{
			get
			{
				return XplatUI.driver.MinimizedWindowSize;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x000751E7 File Offset: 0x000733E7
		public static Size MinimumWindowSize
		{
			get
			{
				return XplatUI.driver.MinimumWindowSize;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x000751F3 File Offset: 0x000733F3
		public static Size MinimumFixedToolWindowSize
		{
			get
			{
				return XplatUI.driver.MinimumFixedToolWindowSize;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x000751FF File Offset: 0x000733FF
		public static Size MinimumSizeableToolWindowSize
		{
			get
			{
				return XplatUI.driver.MinimumSizeableToolWindowSize;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x0007520B File Offset: 0x0007340B
		public static Size MinimumNoBorderWindowSize
		{
			get
			{
				return XplatUI.driver.MinimumNoBorderWindowSize;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x00075217 File Offset: 0x00073417
		public static Size MinWindowTrackSize
		{
			get
			{
				return XplatUI.driver.MinWindowTrackSize;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x00075223 File Offset: 0x00073423
		public static int MenuHeight
		{
			get
			{
				return XplatUI.driver.MenuHeight;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x0007522F File Offset: 0x0007342F
		public static bool RequiresPositiveClientAreaSize
		{
			get
			{
				return XplatUI.driver.RequiresPositiveClientAreaSize;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x0007523B File Offset: 0x0007343B
		public static bool UserClipWontExposeParent
		{
			get
			{
				return XplatUI.driver.UserClipWontExposeParent;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x00075247 File Offset: 0x00073447
		public static int VerticalScrollBarWidth
		{
			get
			{
				return XplatUI.driver.VerticalScrollBarWidth;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x00075253 File Offset: 0x00073453
		public static Rectangle VirtualScreen
		{
			get
			{
				return XplatUI.driver.VirtualScreen;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x0007525F File Offset: 0x0007345F
		public static Rectangle WorkingArea
		{
			get
			{
				return XplatUI.driver.WorkingArea;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x0007526B File Offset: 0x0007346B
		public static Screen[] AllScreens
		{
			get
			{
				return XplatUI.driver.AllScreens;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x00075277 File Offset: 0x00073477
		public static bool ThemesEnabled
		{
			get
			{
				return XplatUI.driver.ThemesEnabled;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x00075283 File Offset: 0x00073483
		public static int ToolWindowCaptionHeight
		{
			get
			{
				return XplatUI.driver.ToolWindowCaptionHeight;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x0007528F File Offset: 0x0007348F
		public static Size ToolWindowCaptionButtonSize
		{
			get
			{
				return XplatUI.driver.ToolWindowCaptionButtonSize;
			}
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0007529B File Offset: 0x0007349B
		internal static void Activate(IntPtr handle)
		{
			XplatUI.driver.Activate(handle);
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x000752A8 File Offset: 0x000734A8
		internal static void AudibleAlert(AlertType alert)
		{
			XplatUI.driver.AudibleAlert(alert);
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x000752B5 File Offset: 0x000734B5
		internal static bool CalculateWindowRect(ref Rectangle ClientRect, CreateParams cp, Menu menu, out Rectangle WindowRect)
		{
			return XplatUI.driver.CalculateWindowRect(ref ClientRect, cp, menu, out WindowRect);
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x000752C5 File Offset: 0x000734C5
		internal static void CaretVisible(IntPtr handle, bool visible)
		{
			XplatUI.driver.CaretVisible(handle, visible);
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x000752D3 File Offset: 0x000734D3
		internal static void CreateCaret(IntPtr handle, int width, int height)
		{
			XplatUI.driver.CreateCaret(handle, width, height);
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x000752E2 File Offset: 0x000734E2
		internal static IntPtr CreateWindow(CreateParams cp)
		{
			return XplatUI.driver.CreateWindow(cp);
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x000752EF File Offset: 0x000734EF
		internal static void ClientToScreen(IntPtr handle, ref int x, ref int y)
		{
			XplatUI.driver.ClientToScreen(handle, ref x, ref y);
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x000752FE File Offset: 0x000734FE
		internal static int[] ClipboardAvailableFormats(IntPtr handle)
		{
			return XplatUI.driver.ClipboardAvailableFormats(handle);
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0007530B File Offset: 0x0007350B
		internal static void ClipboardClose(IntPtr handle)
		{
			XplatUI.driver.ClipboardClose(handle);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x00075318 File Offset: 0x00073518
		internal static int ClipboardGetID(IntPtr handle, string format)
		{
			return XplatUI.driver.ClipboardGetID(handle, format);
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x00075326 File Offset: 0x00073526
		internal static IntPtr ClipboardOpen(bool primary_selection)
		{
			return XplatUI.driver.ClipboardOpen(primary_selection);
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00075333 File Offset: 0x00073533
		internal static void ClipboardStore(IntPtr handle, object obj, int type, XplatUI.ObjectToClipboard converter, bool copy)
		{
			XplatUI.driver.ClipboardStore(handle, obj, type, converter, copy);
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00075345 File Offset: 0x00073545
		internal static object ClipboardRetrieve(IntPtr handle, int type, XplatUI.ClipboardToObject converter)
		{
			return XplatUI.driver.ClipboardRetrieve(handle, type, converter);
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00075354 File Offset: 0x00073554
		internal static IntPtr DefineCursor(Bitmap bitmap, Bitmap mask, Color cursor_pixel, Color mask_pixel, int xHotSpot, int yHotSpot)
		{
			return XplatUI.driver.DefineCursor(bitmap, mask, cursor_pixel, mask_pixel, xHotSpot, yHotSpot);
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00075368 File Offset: 0x00073568
		internal static IntPtr DefineStdCursor(StdCursor id)
		{
			return XplatUI.driver.DefineStdCursor(id);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00075375 File Offset: 0x00073575
		internal static IntPtr DefWndProc(ref Message msg)
		{
			return XplatUI.driver.DefWndProc(ref msg);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00075382 File Offset: 0x00073582
		internal static void DestroyCaret(IntPtr handle)
		{
			XplatUI.driver.DestroyCaret(handle);
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0007538F File Offset: 0x0007358F
		internal static void DestroyWindow(IntPtr handle)
		{
			XplatUI.driver.DestroyWindow(handle);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0007539C File Offset: 0x0007359C
		internal static IntPtr DispatchMessage(ref MSG msg)
		{
			return XplatUI.driver.DispatchMessage(ref msg);
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x000753A9 File Offset: 0x000735A9
		internal static void DoEvents()
		{
			XplatUI.driver.DoEvents();
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x000753B5 File Offset: 0x000735B5
		internal static void DrawReversibleRectangle(IntPtr handle, Rectangle rect, int line_width)
		{
			XplatUI.driver.DrawReversibleRectangle(handle, rect, line_width);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x000753C4 File Offset: 0x000735C4
		internal static void EnableWindow(IntPtr handle, bool Enable)
		{
			XplatUI.driver.EnableWindow(handle, Enable);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x000753D2 File Offset: 0x000735D2
		internal static void EndLoop(Thread thread)
		{
			XplatUI.driver.EndLoop(thread);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x000753DF File Offset: 0x000735DF
		internal static IntPtr GetActive()
		{
			return XplatUI.driver.GetActive();
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x000753EB File Offset: 0x000735EB
		internal static SizeF GetAutoScaleSize(Font font)
		{
			return XplatUI.driver.GetAutoScaleSize(font);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x000753F8 File Offset: 0x000735F8
		internal static void GetCursorInfo(IntPtr cursor, out int width, out int height, out int hotspot_x, out int hotspot_y)
		{
			XplatUI.driver.GetCursorInfo(cursor, out width, out height, out hotspot_x, out hotspot_y);
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0007540A File Offset: 0x0007360A
		internal static void GetCursorPos(IntPtr handle, out int x, out int y)
		{
			XplatUI.driver.GetCursorPos(handle, out x, out y);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00075419 File Offset: 0x00073619
		internal static void GetDisplaySize(out Size size)
		{
			XplatUI.driver.GetDisplaySize(out size);
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00075426 File Offset: 0x00073626
		internal static IntPtr GetFocus()
		{
			return XplatUI.driver.GetFocus();
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00075432 File Offset: 0x00073632
		internal static bool GetFontMetrics(Graphics g, Font font, out int ascent, out int descent)
		{
			return XplatUI.driver.GetFontMetrics(g, font, out ascent, out descent);
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x00075442 File Offset: 0x00073642
		internal static Point GetMenuOrigin(IntPtr handle)
		{
			return XplatUI.driver.GetMenuOrigin(handle);
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0007544F File Offset: 0x0007364F
		internal static bool GetMessage(object queue_id, ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax)
		{
			return XplatUI.driver.GetMessage(queue_id, ref msg, hWnd, wFilterMin, wFilterMax);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00075461 File Offset: 0x00073661
		internal static IntPtr GetParent(IntPtr handle)
		{
			return XplatUI.driver.GetParent(handle);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0007546E File Offset: 0x0007366E
		internal static IntPtr GetPreviousWindow(IntPtr handle)
		{
			return XplatUI.driver.GetPreviousWindow(handle);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0007547C File Offset: 0x0007367C
		internal static void GetWindowPos(IntPtr handle, bool is_toplevel, out int x, out int y, out int width, out int height, out int client_width, out int client_height)
		{
			XplatUI.driver.GetWindowPos(handle, is_toplevel, out x, out y, out width, out height, out client_width, out client_height);
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0007549F File Offset: 0x0007369F
		internal static FormWindowState GetWindowState(IntPtr handle)
		{
			return XplatUI.driver.GetWindowState(handle);
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x000754AC File Offset: 0x000736AC
		internal static void GrabInfo(out IntPtr handle, out bool GrabConfined, out Rectangle GrabArea)
		{
			XplatUI.driver.GrabInfo(out handle, out GrabConfined, out GrabArea);
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x000754BB File Offset: 0x000736BB
		internal static void GrabWindow(IntPtr handle, IntPtr ConfineToHwnd)
		{
			XplatUI.driver.GrabWindow(handle, ConfineToHwnd);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x000754C9 File Offset: 0x000736C9
		internal static void Invalidate(IntPtr handle, Rectangle rc, bool clear)
		{
			XplatUI.driver.Invalidate(handle, rc, clear);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x000754D8 File Offset: 0x000736D8
		internal static void InvalidateNC(IntPtr handle)
		{
			XplatUI.driver.InvalidateNC(handle);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x000754E5 File Offset: 0x000736E5
		internal static bool IsEnabled(IntPtr handle)
		{
			return XplatUI.driver.IsEnabled(handle);
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x000754F2 File Offset: 0x000736F2
		internal static void KillTimer(Timer timer)
		{
			XplatUI.driver.KillTimer(timer);
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x000754FF File Offset: 0x000736FF
		internal static void PaintEventEnd(ref Message msg, IntPtr handle, bool client)
		{
			XplatUI.driver.PaintEventEnd(ref msg, handle, client);
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0007550E File Offset: 0x0007370E
		internal static PaintEventArgs PaintEventStart(ref Message msg, IntPtr handle, bool client)
		{
			return XplatUI.driver.PaintEventStart(ref msg, handle, client);
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0007551D File Offset: 0x0007371D
		internal static bool PostMessage(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam)
		{
			return XplatUI.driver.PostMessage(hwnd, message, wParam, lParam);
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0007552D File Offset: 0x0007372D
		internal static void PostQuitMessage(int exitCode)
		{
			XplatUI.driver.PostQuitMessage(exitCode);
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0007553A File Offset: 0x0007373A
		internal static void RequestAdditionalWM_NCMessages(IntPtr handle, bool hover, bool leave)
		{
			XplatUI.driver.RequestAdditionalWM_NCMessages(handle, hover, leave);
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00075549 File Offset: 0x00073749
		internal static void RequestNCRecalc(IntPtr handle)
		{
			XplatUI.driver.RequestNCRecalc(handle);
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00075556 File Offset: 0x00073756
		internal static void ResetMouseHover(IntPtr handle)
		{
			XplatUI.driver.ResetMouseHover(handle);
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00075563 File Offset: 0x00073763
		internal static void ScreenToClient(IntPtr handle, ref int x, ref int y)
		{
			XplatUI.driver.ScreenToClient(handle, ref x, ref y);
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x00075572 File Offset: 0x00073772
		internal static void ScreenToMenu(IntPtr handle, ref int x, ref int y)
		{
			XplatUI.driver.ScreenToMenu(handle, ref x, ref y);
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00075581 File Offset: 0x00073781
		internal static void ScrollWindow(IntPtr handle, Rectangle rectangle, int XAmount, int YAmount, bool with_children)
		{
			XplatUI.driver.ScrollWindow(handle, rectangle, XAmount, YAmount, with_children);
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00075593 File Offset: 0x00073793
		internal static void ScrollWindow(IntPtr handle, int XAmount, int YAmount, bool with_children)
		{
			XplatUI.driver.ScrollWindow(handle, XAmount, YAmount, with_children);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x000755A3 File Offset: 0x000737A3
		internal static void SendAsyncMethod(AsyncMethodData data)
		{
			XplatUI.driver.SendAsyncMethod(data);
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x000755B0 File Offset: 0x000737B0
		internal static IntPtr SendMessage(IntPtr handle, Msg message, IntPtr wParam, IntPtr lParam)
		{
			return XplatUI.driver.SendMessage(handle, message, wParam, lParam);
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x000755C0 File Offset: 0x000737C0
		internal static void SetAllowDrop(IntPtr handle, bool value)
		{
			XplatUI.driver.SetAllowDrop(handle, value);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x000755CE File Offset: 0x000737CE
		internal static void SetBorderStyle(IntPtr handle, FormBorderStyle border_style)
		{
			XplatUI.driver.SetBorderStyle(handle, border_style);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x000755DC File Offset: 0x000737DC
		internal static void SetCaretPos(IntPtr handle, int x, int y)
		{
			XplatUI.driver.SetCaretPos(handle, x, y);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x000755EB File Offset: 0x000737EB
		internal static void SetClipRegion(IntPtr handle, Region region)
		{
			XplatUI.driver.SetClipRegion(handle, region);
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x000755F9 File Offset: 0x000737F9
		internal static void SetCursor(IntPtr handle, IntPtr cursor)
		{
			XplatUI.driver.SetCursor(handle, cursor);
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x00075607 File Offset: 0x00073807
		internal static void SetCursorPos(IntPtr handle, int x, int y)
		{
			XplatUI.driver.SetCursorPos(handle, x, y);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x00075616 File Offset: 0x00073816
		internal static void SetFocus(IntPtr handle)
		{
			XplatUI.driver.SetFocus(handle);
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x00075623 File Offset: 0x00073823
		internal static void SetIcon(IntPtr handle, Icon icon)
		{
			XplatUI.driver.SetIcon(handle, icon);
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x00075631 File Offset: 0x00073831
		internal static void SetMenu(IntPtr handle, Menu menu)
		{
			XplatUI.driver.SetMenu(handle, menu);
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0007563F File Offset: 0x0007383F
		internal static void SetModal(IntPtr handle, bool Modal)
		{
			XplatUI.driver.SetModal(handle, Modal);
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0007564D File Offset: 0x0007384D
		internal static IntPtr SetParent(IntPtr handle, IntPtr hParent)
		{
			return XplatUI.driver.SetParent(handle, hParent);
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0007565B File Offset: 0x0007385B
		internal static void SetTimer(Timer timer)
		{
			XplatUI.driver.SetTimer(timer);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x00075668 File Offset: 0x00073868
		internal static bool SetTopmost(IntPtr handle, bool Enabled)
		{
			return XplatUI.driver.SetTopmost(handle, Enabled);
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00075676 File Offset: 0x00073876
		internal static bool SetOwner(IntPtr handle, IntPtr hWndOwner)
		{
			return XplatUI.driver.SetOwner(handle, hWndOwner);
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x00075684 File Offset: 0x00073884
		internal static bool SetVisible(IntPtr handle, bool visible, bool activate)
		{
			return XplatUI.driver.SetVisible(handle, visible, activate);
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00075693 File Offset: 0x00073893
		internal static void SetWindowMinMax(IntPtr handle, Rectangle maximized, Size min, Size max)
		{
			XplatUI.driver.SetWindowMinMax(handle, maximized, min, max);
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x000756A3 File Offset: 0x000738A3
		internal static void SetWindowPos(IntPtr handle, int x, int y, int width, int height)
		{
			XplatUI.driver.SetWindowPos(handle, x, y, width, height);
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x000756B5 File Offset: 0x000738B5
		internal static void SetWindowState(IntPtr handle, FormWindowState state)
		{
			XplatUI.driver.SetWindowState(handle, state);
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x000756C3 File Offset: 0x000738C3
		internal static void SetWindowStyle(IntPtr handle, CreateParams cp)
		{
			XplatUI.driver.SetWindowStyle(handle, cp);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x000756D1 File Offset: 0x000738D1
		internal static void SetWindowTransparency(IntPtr handle, double transparency, Color key)
		{
			XplatUI.driver.SetWindowTransparency(handle, transparency, key);
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x000756E0 File Offset: 0x000738E0
		internal static bool SetZOrder(IntPtr handle, IntPtr AfterhWnd, bool Top, bool Bottom)
		{
			return XplatUI.driver.SetZOrder(handle, AfterhWnd, Top, Bottom);
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x000756F0 File Offset: 0x000738F0
		internal static object StartLoop(Thread thread)
		{
			return XplatUI.driver.StartLoop(thread);
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x000756FD File Offset: 0x000738FD
		internal static TransparencySupport SupportsTransparency()
		{
			return XplatUI.driver.SupportsTransparency();
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00075709 File Offset: 0x00073909
		internal static bool Text(IntPtr handle, string text)
		{
			return XplatUI.driver.Text(handle, text);
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00075717 File Offset: 0x00073917
		internal static bool TranslateMessage(ref MSG msg)
		{
			return XplatUI.driver.TranslateMessage(ref msg);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00075724 File Offset: 0x00073924
		internal static void UngrabWindow(IntPtr handle)
		{
			XplatUI.driver.UngrabWindow(handle);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00075731 File Offset: 0x00073931
		internal static void UpdateWindow(IntPtr handle)
		{
			XplatUI.driver.UpdateWindow(handle);
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0007573E File Offset: 0x0007393E
		internal static void CreateOffscreenDrawable(IntPtr handle, int width, int height, out object offscreen_drawable)
		{
			XplatUI.driver.CreateOffscreenDrawable(handle, width, height, out offscreen_drawable);
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x0007574E File Offset: 0x0007394E
		internal static void DestroyOffscreenDrawable(object offscreen_drawable)
		{
			XplatUI.driver.DestroyOffscreenDrawable(offscreen_drawable);
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0007575B File Offset: 0x0007395B
		internal static Graphics GetOffscreenGraphics(object offscreen_drawable)
		{
			return XplatUI.driver.GetOffscreenGraphics(offscreen_drawable);
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00075768 File Offset: 0x00073968
		internal static void BlitFromOffscreen(IntPtr dest_handle, Graphics dest_dc, object offscreen_drawable, Graphics offscreen_dc, Rectangle r)
		{
			XplatUI.driver.BlitFromOffscreen(dest_handle, dest_dc, offscreen_drawable, offscreen_dc, r);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0007577C File Offset: 0x0007397C
		internal static bool FilterKey(KeyFilterData key)
		{
			ArrayList arrayList = XplatUI.key_filters;
			lock (arrayList)
			{
				for (int i = 0; i < XplatUI.key_filters.Count; i++)
				{
					if (((IKeyFilter)XplatUI.key_filters[i]).PreFilterKey(key))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060017D1 RID: 6097
		[DllImport("libc")]
		private static extern int uname(IntPtr buf);

		// Token: 0x04001223 RID: 4643
		private static XplatUIDriver driver;

		// Token: 0x04001224 RID: 4644
		internal static ArrayList key_filters = new ArrayList();

		// Token: 0x02000295 RID: 661
		public class State
		{
			// Token: 0x1700060E RID: 1550
			// (get) Token: 0x060017D2 RID: 6098 RVA: 0x000757EC File Offset: 0x000739EC
			public static Keys ModifierKeys
			{
				get
				{
					return XplatUI.driver.ModifierKeys;
				}
			}
		}

		// Token: 0x02000296 RID: 662
		// (Invoke) Token: 0x060017D4 RID: 6100
		public delegate bool ClipboardToObject(int type, IntPtr data, out object obj);

		// Token: 0x02000297 RID: 663
		// (Invoke) Token: 0x060017D6 RID: 6102
		public delegate bool ObjectToClipboard(ref int type, object obj, out byte[] data);
	}
}
