using System;

namespace System.Windows.Forms
{
	// Token: 0x02000252 RID: 594
	internal struct XGravityEvent
	{
		// Token: 0x04000F19 RID: 3865
		internal XEventName type;

		// Token: 0x04000F1A RID: 3866
		internal IntPtr serial;

		// Token: 0x04000F1B RID: 3867
		internal bool send_event;

		// Token: 0x04000F1C RID: 3868
		internal IntPtr display;

		// Token: 0x04000F1D RID: 3869
		internal IntPtr xevent;

		// Token: 0x04000F1E RID: 3870
		internal IntPtr window;

		// Token: 0x04000F1F RID: 3871
		internal int x;

		// Token: 0x04000F20 RID: 3872
		internal int y;
	}
}
