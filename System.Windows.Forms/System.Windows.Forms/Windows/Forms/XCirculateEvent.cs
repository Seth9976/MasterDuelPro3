using System;

namespace System.Windows.Forms
{
	// Token: 0x02000255 RID: 597
	internal struct XCirculateEvent
	{
		// Token: 0x04000F36 RID: 3894
		internal XEventName type;

		// Token: 0x04000F37 RID: 3895
		internal IntPtr serial;

		// Token: 0x04000F38 RID: 3896
		internal bool send_event;

		// Token: 0x04000F39 RID: 3897
		internal IntPtr display;

		// Token: 0x04000F3A RID: 3898
		internal IntPtr xevent;

		// Token: 0x04000F3B RID: 3899
		internal IntPtr window;

		// Token: 0x04000F3C RID: 3900
		internal int place;
	}
}
