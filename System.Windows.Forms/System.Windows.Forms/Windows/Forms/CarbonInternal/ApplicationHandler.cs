using System;
using System.Collections;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x02000397 RID: 919
	internal class ApplicationHandler : EventHandlerBase, IEventHandler
	{
		// Token: 0x06001DB8 RID: 7608 RVA: 0x00093AC8 File Offset: 0x00091CC8
		internal ApplicationHandler(XplatUICarbon driver)
			: base(driver)
		{
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x00093AD4 File Offset: 0x00091CD4
		public bool ProcessEvent(IntPtr callref, IntPtr eventref, IntPtr handle, uint kind, ref MSG msg)
		{
			if (kind != 1U)
			{
				if (kind != 2U)
				{
					return true;
				}
			}
			else
			{
				using (IEnumerator enumerator = XplatUICarbon.UtilityWindows.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						IntPtr intPtr = (IntPtr)obj;
						if (!XplatUICarbon.IsWindowVisible(intPtr))
						{
							XplatUICarbon.ShowWindow(intPtr);
						}
					}
					return true;
				}
			}
			if (XplatUICarbon.FocusWindow != IntPtr.Zero)
			{
				this.Driver.SendMessage(XplatUICarbon.FocusWindow, Msg.WM_KILLFOCUS, IntPtr.Zero, IntPtr.Zero);
			}
			if (XplatUICarbon.Grab.Hwnd != IntPtr.Zero)
			{
				this.Driver.SendMessage(Hwnd.ObjectFromHandle(XplatUICarbon.Grab.Hwnd).Handle, Msg.WM_LBUTTONDOWN, (IntPtr)1, (IntPtr)((this.Driver.MousePosition.X << 16) | this.Driver.MousePosition.Y));
			}
			foreach (object obj2 in XplatUICarbon.UtilityWindows)
			{
				IntPtr intPtr2 = (IntPtr)obj2;
				if (XplatUICarbon.IsWindowVisible(intPtr2))
				{
					XplatUICarbon.HideWindow(intPtr2);
				}
			}
			return true;
		}
	}
}
