using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020003A4 RID: 932
	internal class HIObjectHandler : EventHandlerBase, IEventHandler
	{
		// Token: 0x06001DFC RID: 7676 RVA: 0x00093AC8 File Offset: 0x00091CC8
		internal HIObjectHandler(XplatUICarbon driver)
			: base(driver)
		{
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x00094CAC File Offset: 0x00092EAC
		public bool ProcessEvent(IntPtr callref, IntPtr eventref, IntPtr handle, uint kind, ref MSG msg)
		{
			switch (kind)
			{
			case 1U:
			{
				IntPtr zero = IntPtr.Zero;
				HIObjectHandler.GetEventParameter(eventref, 1751740265U, 1751740258U, IntPtr.Zero, 4U, IntPtr.Zero, ref zero);
				return false;
			}
			case 2U:
				HIObjectHandler.CallNextEventHandler(callref, eventref);
				return false;
			case 3U:
				return false;
			default:
				return false;
			}
		}

		// Token: 0x06001DFE RID: 7678
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int CallNextEventHandler(IntPtr callref, IntPtr eventref);

		// Token: 0x06001DFF RID: 7679
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int GetEventParameter(IntPtr eventref, uint name, uint type, IntPtr outtype, uint size, IntPtr outsize, ref IntPtr data);
	}
}
