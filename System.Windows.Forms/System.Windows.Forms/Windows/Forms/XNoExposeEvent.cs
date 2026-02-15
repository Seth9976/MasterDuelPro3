using System;

namespace System.Windows.Forms
{
	// Token: 0x02000249 RID: 585
	internal struct XNoExposeEvent
	{
		// Token: 0x04000ECF RID: 3791
		internal XEventName type;

		// Token: 0x04000ED0 RID: 3792
		internal IntPtr serial;

		// Token: 0x04000ED1 RID: 3793
		internal bool send_event;

		// Token: 0x04000ED2 RID: 3794
		internal IntPtr display;

		// Token: 0x04000ED3 RID: 3795
		internal IntPtr drawable;

		// Token: 0x04000ED4 RID: 3796
		internal int major_code;

		// Token: 0x04000ED5 RID: 3797
		internal int minor_code;
	}
}
