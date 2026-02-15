using System;

namespace System.Windows.Forms
{
	// Token: 0x02000253 RID: 595
	internal struct XResizeRequestEvent
	{
		// Token: 0x04000F21 RID: 3873
		internal XEventName type;

		// Token: 0x04000F22 RID: 3874
		internal IntPtr serial;

		// Token: 0x04000F23 RID: 3875
		internal bool send_event;

		// Token: 0x04000F24 RID: 3876
		internal IntPtr display;

		// Token: 0x04000F25 RID: 3877
		internal IntPtr window;

		// Token: 0x04000F26 RID: 3878
		internal int width;

		// Token: 0x04000F27 RID: 3879
		internal int height;
	}
}
