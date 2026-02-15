using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020003A5 RID: 933
	internal interface IEventHandler
	{
		// Token: 0x06001E00 RID: 7680
		bool ProcessEvent(IntPtr callref, IntPtr eventref, IntPtr handle, uint kind, ref MSG msg);
	}
}
