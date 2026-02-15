using System;

namespace System.Windows.Forms
{
	// Token: 0x02000256 RID: 598
	internal struct XCirculateRequestEvent
	{
		// Token: 0x04000F3D RID: 3901
		internal XEventName type;

		// Token: 0x04000F3E RID: 3902
		internal IntPtr serial;

		// Token: 0x04000F3F RID: 3903
		internal bool send_event;

		// Token: 0x04000F40 RID: 3904
		internal IntPtr display;

		// Token: 0x04000F41 RID: 3905
		internal IntPtr parent;

		// Token: 0x04000F42 RID: 3906
		internal IntPtr window;

		// Token: 0x04000F43 RID: 3907
		internal int place;
	}
}
