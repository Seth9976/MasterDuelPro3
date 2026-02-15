using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020003A7 RID: 935
	internal class MouseHandler : EventHandlerBase, IEventHandler
	{
		// Token: 0x06001E0E RID: 7694 RVA: 0x00093AC8 File Offset: 0x00091CC8
		internal MouseHandler(XplatUICarbon driver)
			: base(driver)
		{
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x000952C4 File Offset: 0x000934C4
		public bool ProcessEvent(IntPtr callref, IntPtr eventref, IntPtr handle, uint kind, ref MSG msg)
		{
			QDPoint qdpoint = default(QDPoint);
			CGPoint cgpoint = default(CGPoint);
			Rect rect = default(Rect);
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			bool flag = true;
			ushort num = 0;
			MouseHandler.GetEventParameter(eventref, 1835822947U, 1363439732U, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(QDPoint)), IntPtr.Zero, ref qdpoint);
			MouseHandler.GetEventParameter(eventref, 1835168878U, 1835168878U, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(ushort)), IntPtr.Zero, ref num);
			if (num == 1 && (this.Driver.ModifierKeys & Keys.Control) != Keys.None)
			{
				num = 2;
			}
			cgpoint.x = (float)qdpoint.x;
			cgpoint.y = (float)qdpoint.y;
			if (MouseHandler.FindWindow(qdpoint, ref zero2) == 5)
			{
				return true;
			}
			MouseHandler.GetWindowBounds(handle, 33U, ref rect);
			MouseHandler.HIViewFindByID(MouseHandler.HIViewGetRoot(handle), new HIViewID(2003398244U, 1U), ref zero2);
			cgpoint.x -= (float)rect.left;
			cgpoint.y -= (float)rect.top;
			MouseHandler.HIViewGetSubviewHit(zero2, ref cgpoint, true, ref zero);
			MouseHandler.HIViewConvertPoint(ref cgpoint, zero2, zero);
			Hwnd hwnd = Hwnd.ObjectFromHandle(zero);
			if (hwnd != null)
			{
				flag = hwnd.ClientWindow == zero;
			}
			if (XplatUICarbon.Grab.Hwnd != IntPtr.Zero)
			{
				hwnd = Hwnd.ObjectFromHandle(XplatUICarbon.Grab.Hwnd);
				flag = true;
			}
			if (hwnd == null)
			{
				return true;
			}
			if (flag)
			{
				qdpoint.x = (short)cgpoint.x;
				qdpoint.y = (short)cgpoint.y;
				this.Driver.ScreenToClient(hwnd.Handle, ref qdpoint);
			}
			else
			{
				cgpoint.x = (float)qdpoint.x;
				cgpoint.y = (float)qdpoint.y;
			}
			msg.hwnd = hwnd.Handle;
			msg.lParam = (IntPtr)(((int)((ushort)cgpoint.y) << 16) | (int)((ushort)cgpoint.x));
			switch (kind)
			{
			case 1U:
				this.UpdateMouseState((int)num, true);
				msg.message = (flag ? Msg.WM_MOUSEMOVE : Msg.WM_NCMOUSEMOVE) + (int)((num - 1) * 3) + 1;
				msg.wParam = this.Driver.GetMousewParam(0);
				if (MouseHandler.ClickPending.Pending && DateTime.Now.Ticks - MouseHandler.ClickPending.Time < 7500000L && msg.hwnd == MouseHandler.ClickPending.Hwnd && msg.wParam == MouseHandler.ClickPending.wParam && msg.lParam == MouseHandler.ClickPending.lParam && msg.message == MouseHandler.ClickPending.Message)
				{
					msg.message = (flag ? Msg.WM_MOUSEMOVE : Msg.WM_NCMOUSEMOVE) + (int)((num - 1) * 3) + 3;
					MouseHandler.ClickPending.Pending = false;
					goto IL_0528;
				}
				MouseHandler.ClickPending.Pending = true;
				MouseHandler.ClickPending.Hwnd = msg.hwnd;
				MouseHandler.ClickPending.Message = msg.message;
				MouseHandler.ClickPending.wParam = msg.wParam;
				MouseHandler.ClickPending.lParam = msg.lParam;
				MouseHandler.ClickPending.Time = DateTime.Now.Ticks;
				goto IL_0528;
			case 2U:
				this.UpdateMouseState((int)num, false);
				msg.message = (flag ? Msg.WM_MOUSEMOVE : Msg.WM_NCMOUSEMOVE) + (int)((num - 1) * 3) + 2;
				msg.wParam = this.Driver.GetMousewParam(0);
				goto IL_0528;
			case 5U:
			case 6U:
				if (XplatUICarbon.Grab.Hwnd == IntPtr.Zero)
				{
					IntPtr intPtr = IntPtr.Zero;
					if (flag)
					{
						intPtr = (IntPtr)1;
						NativeWindow.WndProc(msg.hwnd, Msg.WM_SETCURSOR, msg.hwnd, (IntPtr)1);
					}
					else
					{
						intPtr = (IntPtr)NativeWindow.WndProc(hwnd.client_window, Msg.WM_NCHITTEST, IntPtr.Zero, msg.lParam).ToInt32();
						NativeWindow.WndProc(hwnd.client_window, Msg.WM_SETCURSOR, msg.hwnd, intPtr);
					}
				}
				msg.message = (flag ? Msg.WM_MOUSEMOVE : Msg.WM_NCMOUSEMOVE);
				msg.wParam = this.Driver.GetMousewParam(0);
				goto IL_0528;
			case 10U:
			case 11U:
			{
				ushort num2 = 0;
				int num3 = 0;
				MouseHandler.GetEventParameter(eventref, 1836540280U, 1836540280U, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(ushort)), IntPtr.Zero, ref num2);
				MouseHandler.GetEventParameter(eventref, 1836541036U, 1819242087U, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(int)), IntPtr.Zero, ref num3);
				if (num2 == 1)
				{
					msg.hwnd = XplatUICarbon.FocusWindow;
					msg.message = Msg.WM_MOUSEWHEEL;
					msg.wParam = this.Driver.GetMousewParam(num3 * 40);
					return true;
				}
				goto IL_0528;
			}
			}
			return false;
			IL_0528:
			this.Driver.mouse_position.X = (int)cgpoint.x;
			this.Driver.mouse_position.Y = (int)cgpoint.y;
			return true;
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x00095828 File Offset: 0x00093A28
		internal bool TranslateMessage(ref MSG msg)
		{
			if (msg.message == Msg.WM_MOUSEMOVE || msg.message == Msg.WM_NCMOUSEMOVE)
			{
				Hwnd hwnd = Hwnd.ObjectFromHandle(msg.hwnd);
				if (XplatUICarbon.MouseHwnd == null)
				{
					this.Driver.PostMessage(hwnd.Handle, Msg.WM_MOUSE_ENTER, IntPtr.Zero, IntPtr.Zero);
					Cursor.SetCursor(hwnd.Cursor);
				}
				else if (XplatUICarbon.MouseHwnd.Handle != hwnd.Handle)
				{
					this.Driver.PostMessage(XplatUICarbon.MouseHwnd.Handle, Msg.WM_MOUSELEAVE, IntPtr.Zero, IntPtr.Zero);
					this.Driver.PostMessage(hwnd.Handle, Msg.WM_MOUSE_ENTER, IntPtr.Zero, IntPtr.Zero);
					Cursor.SetCursor(hwnd.Cursor);
				}
				XplatUICarbon.MouseHwnd = hwnd;
			}
			return false;
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x00095904 File Offset: 0x00093B04
		private void UpdateMouseState(int button, bool down)
		{
			switch (button)
			{
			case 1:
				if (down)
				{
					XplatUICarbon.MouseState |= MouseButtons.Left;
					return;
				}
				XplatUICarbon.MouseState &= ~MouseButtons.Left;
				return;
			case 2:
				if (down)
				{
					XplatUICarbon.MouseState |= MouseButtons.Right;
					return;
				}
				XplatUICarbon.MouseState &= ~MouseButtons.Right;
				return;
			case 3:
				if (down)
				{
					XplatUICarbon.MouseState |= MouseButtons.Middle;
					return;
				}
				XplatUICarbon.MouseState &= ~MouseButtons.Middle;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001E12 RID: 7698
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetEventParameter(IntPtr eventref, uint name, uint type, IntPtr outtype, uint size, IntPtr outsize, ref QDPoint data);

		// Token: 0x06001E13 RID: 7699
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetEventParameter(IntPtr eventref, uint name, uint type, IntPtr outtype, uint size, IntPtr outsize, ref int data);

		// Token: 0x06001E14 RID: 7700
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetEventParameter(IntPtr eventref, uint name, uint type, IntPtr outtype, uint size, IntPtr outsize, ref ushort data);

		// Token: 0x06001E15 RID: 7701
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern short FindWindow(QDPoint point, ref IntPtr handle);

		// Token: 0x06001E16 RID: 7702
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int GetWindowBounds(IntPtr handle, uint region, ref Rect bounds);

		// Token: 0x06001E17 RID: 7703
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int HIViewConvertPoint(ref CGPoint point, IntPtr source_view, IntPtr target_view);

		// Token: 0x06001E18 RID: 7704
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr HIViewGetRoot(IntPtr handle);

		// Token: 0x06001E19 RID: 7705
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int HIViewGetSubviewHit(IntPtr content_view, ref CGPoint point, bool tval, ref IntPtr hit_view);

		// Token: 0x06001E1A RID: 7706
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern int HIViewFindByID(IntPtr root_window, HIViewID id, ref IntPtr view_handle);

		// Token: 0x04001D3F RID: 7487
		internal static ClickStruct ClickPending;
	}
}
