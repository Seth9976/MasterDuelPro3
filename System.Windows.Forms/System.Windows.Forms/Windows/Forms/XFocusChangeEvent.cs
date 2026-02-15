using System;

namespace System.Windows.Forms
{
	// Token: 0x02000245 RID: 581
	internal struct XFocusChangeEvent
	{
		// Token: 0x04000E8D RID: 3725
		internal XEventName type;

		// Token: 0x04000E8E RID: 3726
		internal IntPtr serial;

		// Token: 0x04000E8F RID: 3727
		internal bool send_event;

		// Token: 0x04000E90 RID: 3728
		internal IntPtr display;

		// Token: 0x04000E91 RID: 3729
		internal IntPtr window;

		// Token: 0x04000E92 RID: 3730
		internal int mode;

		// Token: 0x04000E93 RID: 3731
		internal NotifyDetail detail;
	}
}
