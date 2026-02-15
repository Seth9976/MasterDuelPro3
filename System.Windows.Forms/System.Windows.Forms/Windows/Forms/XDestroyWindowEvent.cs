using System;

namespace System.Windows.Forms
{
	// Token: 0x0200024C RID: 588
	internal struct XDestroyWindowEvent
	{
		// Token: 0x04000EE8 RID: 3816
		internal XEventName type;

		// Token: 0x04000EE9 RID: 3817
		internal IntPtr serial;

		// Token: 0x04000EEA RID: 3818
		internal bool send_event;

		// Token: 0x04000EEB RID: 3819
		internal IntPtr display;

		// Token: 0x04000EEC RID: 3820
		internal IntPtr xevent;

		// Token: 0x04000EED RID: 3821
		internal IntPtr window;
	}
}
