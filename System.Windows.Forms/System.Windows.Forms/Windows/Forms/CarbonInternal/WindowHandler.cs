using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020003B3 RID: 947
	internal class WindowHandler : EventHandlerBase, IEventHandler
	{
		// Token: 0x06001E2D RID: 7725 RVA: 0x00093AC8 File Offset: 0x00091CC8
		internal WindowHandler(XplatUICarbon driver)
			: base(driver)
		{
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x00095B18 File Offset: 0x00093D18
		public bool ProcessEvent(IntPtr callref, IntPtr eventref, IntPtr handle, uint kind, ref MSG msg)
		{
			IntPtr intPtr = this.Driver.HandleToWindow(handle);
			Hwnd hwnd = Hwnd.ObjectFromHandle(intPtr);
			if (intPtr != IntPtr.Zero)
			{
				if (kind <= 67U)
				{
					if (kind <= 6U)
					{
						if (kind != 5U)
						{
							if (kind != 6U)
							{
								return false;
							}
							Control control = Control.FromHandle(hwnd.client_window);
							if (control != null)
							{
								Form form = control.FindForm();
								if (form != null && XplatUICarbon.UnactiveWindow != form.Handle)
								{
									this.Driver.SendMessage(form.Handle, Msg.WM_ACTIVATE, (IntPtr)0, IntPtr.Zero);
									XplatUICarbon.ActiveWindow = IntPtr.Zero;
								}
							}
							using (IEnumerator enumerator = XplatUICarbon.UtilityWindows.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									IntPtr intPtr2 = (IntPtr)obj;
									if (intPtr2 != handle && XplatUICarbon.IsWindowVisible(intPtr2))
									{
										XplatUICarbon.HideWindow(intPtr2);
									}
								}
								return false;
							}
							goto IL_0294;
						}
						else
						{
							Control control2 = Control.FromHandle(hwnd.client_window);
							if (control2 != null)
							{
								Form form2 = control2.FindForm();
								if (form2 != null && !form2.IsDisposed)
								{
									this.Driver.SendMessage(form2.Handle, Msg.WM_ACTIVATE, (IntPtr)1, IntPtr.Zero);
									XplatUICarbon.ActiveWindow = hwnd.client_window;
								}
							}
							using (IEnumerator enumerator = XplatUICarbon.UtilityWindows.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj2 = enumerator.Current;
									IntPtr intPtr3 = (IntPtr)obj2;
									if (intPtr3 != handle && !XplatUICarbon.IsWindowVisible(intPtr3))
									{
										XplatUICarbon.ShowWindow(intPtr3);
									}
								}
								return false;
							}
						}
					}
					else
					{
						switch (kind)
						{
						case 24U:
							msg.message = Msg.WM_SHOWWINDOW;
							msg.lParam = (IntPtr)1;
							msg.wParam = (IntPtr)0;
							msg.hwnd = hwnd.Handle;
							return true;
						case 25U:
						case 26U:
							return false;
						case 27U:
						{
							Rect rect = default(Rect);
							HIRect hirect = default(HIRect);
							WindowHandler.GetWindowBounds(handle, 33U, ref rect);
							hirect.size.width = (float)(rect.right - rect.left);
							hirect.size.height = (float)(rect.bottom - rect.top);
							WindowHandler.HIViewSetFrame(hwnd.WholeWindow, ref hirect);
							Size size = XplatUICarbon.TranslateQuartzWindowSizeToWindowSize(Control.FromHandle(hwnd.Handle).GetCreateParams(), (int)hirect.size.width, (int)hirect.size.height);
							hwnd.X = (int)rect.left;
							hwnd.Y = (int)rect.top;
							hwnd.Width = size.Width;
							hwnd.Height = size.Height;
							this.Driver.PerformNCCalc(hwnd);
							msg.hwnd = hwnd.Handle;
							msg.message = Msg.WM_WINDOWPOSCHANGED;
							this.Driver.SetCaretPos(XplatUICarbon.Caret.Hwnd, XplatUICarbon.Caret.X, XplatUICarbon.Caret.Y);
							return true;
						}
						case 28U:
							msg.message = Msg.WM_ENTERSIZEMOVE;
							msg.hwnd = hwnd.Handle;
							return true;
						case 29U:
							msg.message = Msg.WM_EXITSIZEMOVE;
							msg.hwnd = hwnd.Handle;
							return true;
						default:
							if (kind != 67U)
							{
								return false;
							}
							NativeWindow.WndProc(hwnd.Handle, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
							msg.hwnd = hwnd.Handle;
							msg.message = Msg.WM_EXITSIZEMOVE;
							return true;
						}
					}
				}
				else if (kind <= 72U)
				{
					if (kind == 70U)
					{
						NativeWindow.WndProc(hwnd.Handle, Msg.WM_WINDOWPOSCHANGED, IntPtr.Zero, IntPtr.Zero);
						msg.hwnd = hwnd.Handle;
						msg.message = Msg.WM_EXITSIZEMOVE;
						return true;
					}
					if (kind != 72U)
					{
						return false;
					}
					NativeWindow.WndProc(hwnd.Handle, Msg.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
					return false;
				}
				else
				{
					if (kind == 86U)
					{
						goto IL_0294;
					}
					if (kind != 87U)
					{
						return false;
					}
				}
				foreach (object obj3 in XplatUICarbon.UtilityWindows)
				{
					IntPtr intPtr4 = (IntPtr)obj3;
					if (intPtr4 != handle && !XplatUICarbon.IsWindowVisible(intPtr4))
					{
						XplatUICarbon.ShowWindow(intPtr4);
					}
				}
				msg.hwnd = hwnd.Handle;
				msg.message = Msg.WM_ENTERSIZEMOVE;
				return true;
				IL_0294:
				foreach (object obj4 in XplatUICarbon.UtilityWindows)
				{
					IntPtr intPtr5 = (IntPtr)obj4;
					if (intPtr5 != handle && XplatUICarbon.IsWindowVisible(intPtr5))
					{
						XplatUICarbon.HideWindow(intPtr5);
					}
				}
				msg.hwnd = hwnd.Handle;
				msg.message = Msg.WM_ENTERSIZEMOVE;
				return true;
			}
			return false;
		}

		// Token: 0x06001E2F RID: 7727
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetWindowBounds(IntPtr handle, uint region, ref Rect bounds);

		// Token: 0x06001E30 RID: 7728
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int HIViewSetFrame(IntPtr handle, ref HIRect bounds);
	}
}
