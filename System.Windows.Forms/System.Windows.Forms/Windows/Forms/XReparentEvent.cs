using System;

namespace System.Windows.Forms
{
	// Token: 0x02000250 RID: 592
	internal struct XReparentEvent
	{
		// Token: 0x04000F02 RID: 3842
		internal XEventName type;

		// Token: 0x04000F03 RID: 3843
		internal IntPtr serial;

		// Token: 0x04000F04 RID: 3844
		internal bool send_event;

		// Token: 0x04000F05 RID: 3845
		internal IntPtr display;

		// Token: 0x04000F06 RID: 3846
		internal IntPtr xevent;

		// Token: 0x04000F07 RID: 3847
		internal IntPtr window;

		// Token: 0x04000F08 RID: 3848
		internal IntPtr parent;

		// Token: 0x04000F09 RID: 3849
		internal int x;

		// Token: 0x04000F0A RID: 3850
		internal int y;

		// Token: 0x04000F0B RID: 3851
		internal bool override_redirect;
	}
}
