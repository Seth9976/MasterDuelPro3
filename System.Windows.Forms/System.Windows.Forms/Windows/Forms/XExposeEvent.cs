using System;

namespace System.Windows.Forms
{
	// Token: 0x02000247 RID: 583
	internal struct XExposeEvent
	{
		// Token: 0x04000EB9 RID: 3769
		internal XEventName type;

		// Token: 0x04000EBA RID: 3770
		internal IntPtr serial;

		// Token: 0x04000EBB RID: 3771
		internal bool send_event;

		// Token: 0x04000EBC RID: 3772
		internal IntPtr display;

		// Token: 0x04000EBD RID: 3773
		internal IntPtr window;

		// Token: 0x04000EBE RID: 3774
		internal int x;

		// Token: 0x04000EBF RID: 3775
		internal int y;

		// Token: 0x04000EC0 RID: 3776
		internal int width;

		// Token: 0x04000EC1 RID: 3777
		internal int height;

		// Token: 0x04000EC2 RID: 3778
		internal int count;
	}
}
