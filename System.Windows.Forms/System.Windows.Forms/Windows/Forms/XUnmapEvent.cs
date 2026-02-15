using System;

namespace System.Windows.Forms
{
	// Token: 0x0200024D RID: 589
	internal struct XUnmapEvent
	{
		// Token: 0x04000EEE RID: 3822
		internal XEventName type;

		// Token: 0x04000EEF RID: 3823
		internal IntPtr serial;

		// Token: 0x04000EF0 RID: 3824
		internal bool send_event;

		// Token: 0x04000EF1 RID: 3825
		internal IntPtr display;

		// Token: 0x04000EF2 RID: 3826
		internal IntPtr xevent;

		// Token: 0x04000EF3 RID: 3827
		internal IntPtr window;

		// Token: 0x04000EF4 RID: 3828
		internal bool from_configure;
	}
}
