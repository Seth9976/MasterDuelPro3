using System;

namespace System.Windows.Forms
{
	// Token: 0x0200024E RID: 590
	internal struct XMapEvent
	{
		// Token: 0x04000EF5 RID: 3829
		internal XEventName type;

		// Token: 0x04000EF6 RID: 3830
		internal IntPtr serial;

		// Token: 0x04000EF7 RID: 3831
		internal bool send_event;

		// Token: 0x04000EF8 RID: 3832
		internal IntPtr display;

		// Token: 0x04000EF9 RID: 3833
		internal IntPtr xevent;

		// Token: 0x04000EFA RID: 3834
		internal IntPtr window;

		// Token: 0x04000EFB RID: 3835
		internal bool override_redirect;
	}
}
