using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x02000398 RID: 920
	internal class ControlHandler : EventHandlerBase, IEventHandler
	{
		// Token: 0x06001DBA RID: 7610 RVA: 0x00093AC8 File Offset: 0x00091CC8
		internal ControlHandler(XplatUICarbon driver)
			: base(driver)
		{
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x00093C38 File Offset: 0x00091E38
		public bool ProcessEvent(IntPtr callref, IntPtr eventref, IntPtr handle, uint kind, ref MSG msg)
		{
			ControlHandler.GetEventParameter(eventref, 757935405U, 1668575852U, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(IntPtr)), IntPtr.Zero, ref handle);
			Hwnd hwnd = Hwnd.ObjectFromHandle(handle);
			if (hwnd == null)
			{
				return false;
			}
			msg.hwnd = hwnd.Handle;
			bool flag = hwnd.ClientWindow == handle;
			if (kind <= 8U)
			{
				if (kind != 4U)
				{
					if (kind == 8U)
					{
						short num = 0;
						ControlHandler.SetEventParameter(eventref, 1668313716U, 1668313716U, (uint)Marshal.SizeOf(typeof(short)), ref num);
						return false;
					}
				}
				else
				{
					IntPtr zero = IntPtr.Zero;
					HIRect hirect = default(HIRect);
					ControlHandler.GetEventParameter(eventref, 1919381096U, 1919381096U, IntPtr.Zero, (uint)Marshal.SizeOf(typeof(IntPtr)), IntPtr.Zero, ref zero);
					if (zero != IntPtr.Zero)
					{
						Rect rect = default(Rect);
						ControlHandler.GetRegionBounds(zero, ref rect);
						hirect.origin.x = (float)rect.left;
						hirect.origin.y = (float)rect.top;
						hirect.size.width = (float)(rect.right - rect.left);
						hirect.size.height = (float)(rect.bottom - rect.top);
					}
					else
					{
						ControlHandler.HIViewGetBounds(handle, ref hirect);
					}
					if (!hwnd.visible)
					{
						if (flag)
						{
							hwnd.expose_pending = false;
						}
						else
						{
							hwnd.nc_expose_pending = false;
						}
						return false;
					}
					if (!flag)
					{
						this.DrawBorders(hwnd);
					}
					this.Driver.AddExpose(hwnd, flag, hirect);
					return true;
				}
			}
			else
			{
				if (kind - 18U <= 3U)
				{
					return Dnd.HandleEvent(callref, eventref, handle, kind, ref msg);
				}
				if (kind == 154U)
				{
					HIRect hirect2 = default(HIRect);
					ControlHandler.HIViewGetFrame(handle, ref hirect2);
					if (!flag)
					{
						hwnd.X = (int)hirect2.origin.x;
						hwnd.Y = (int)hirect2.origin.y;
						hwnd.Width = (int)hirect2.size.width;
						hwnd.Height = (int)hirect2.size.height;
						this.Driver.PerformNCCalc(hwnd);
					}
					msg.message = Msg.WM_WINDOWPOSCHANGED;
					msg.hwnd = hwnd.Handle;
					return true;
				}
				if (kind == 157U)
				{
					if (flag)
					{
						msg.message = Msg.WM_SHOWWINDOW;
						msg.lParam = (IntPtr)0;
						msg.wParam = (ControlHandler.HIViewIsVisible(handle) ? ((IntPtr)1) : ((IntPtr)0));
						return true;
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x00093ECC File Offset: 0x000920CC
		private void DrawBorders(Hwnd hwnd)
		{
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
					return;
				}
			}
			else
			{
				Graphics graphics2 = Graphics.FromHwnd(hwnd.whole_window);
				ControlPaint.DrawBorder(graphics2, new Rectangle(0, 0, hwnd.Width, hwnd.Height), Color.Black, ButtonBorderStyle.Solid);
				graphics2.Dispose();
			}
		}

		// Token: 0x06001DBD RID: 7613
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetRegionBounds(IntPtr rgnhandle, ref Rect region);

		// Token: 0x06001DBE RID: 7614
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetEventParameter(IntPtr eventref, uint name, uint type, IntPtr outtype, uint size, IntPtr outsize, ref IntPtr data);

		// Token: 0x06001DBF RID: 7615
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int SetEventParameter(IntPtr eventref, uint name, uint type, uint size, ref short data);

		// Token: 0x06001DC0 RID: 7616
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewGetBounds(IntPtr handle, ref HIRect rect);

		// Token: 0x06001DC1 RID: 7617
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewGetFrame(IntPtr handle, ref HIRect rect);

		// Token: 0x06001DC2 RID: 7618
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern bool HIViewIsVisible(IntPtr vHnd);
	}
}
