using System;

namespace System.Windows.Forms
{
	// Token: 0x02000240 RID: 576
	internal struct XAnyEvent
	{
		// Token: 0x04000E4A RID: 3658
		internal XEventName type;

		// Token: 0x04000E4B RID: 3659
		internal IntPtr serial;

		// Token: 0x04000E4C RID: 3660
		internal bool send_event;

		// Token: 0x04000E4D RID: 3661
		internal IntPtr display;

		// Token: 0x04000E4E RID: 3662
		internal IntPtr window;
	}
}
