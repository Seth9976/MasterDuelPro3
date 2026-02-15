using System;

namespace System.Windows.Forms
{
	// Token: 0x02000257 RID: 599
	internal struct XPropertyEvent
	{
		// Token: 0x04000F44 RID: 3908
		internal XEventName type;

		// Token: 0x04000F45 RID: 3909
		internal IntPtr serial;

		// Token: 0x04000F46 RID: 3910
		internal bool send_event;

		// Token: 0x04000F47 RID: 3911
		internal IntPtr display;

		// Token: 0x04000F48 RID: 3912
		internal IntPtr window;

		// Token: 0x04000F49 RID: 3913
		internal IntPtr atom;

		// Token: 0x04000F4A RID: 3914
		internal IntPtr time;

		// Token: 0x04000F4B RID: 3915
		internal int state;
	}
}
