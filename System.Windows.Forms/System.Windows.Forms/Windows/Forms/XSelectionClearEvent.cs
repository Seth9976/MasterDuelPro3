using System;

namespace System.Windows.Forms
{
	// Token: 0x02000258 RID: 600
	internal struct XSelectionClearEvent
	{
		// Token: 0x04000F4C RID: 3916
		internal XEventName type;

		// Token: 0x04000F4D RID: 3917
		internal IntPtr serial;

		// Token: 0x04000F4E RID: 3918
		internal bool send_event;

		// Token: 0x04000F4F RID: 3919
		internal IntPtr display;

		// Token: 0x04000F50 RID: 3920
		internal IntPtr window;

		// Token: 0x04000F51 RID: 3921
		internal IntPtr selection;

		// Token: 0x04000F52 RID: 3922
		internal IntPtr time;
	}
}
