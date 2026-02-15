using System;

namespace System.Windows.Forms
{
	// Token: 0x02000248 RID: 584
	internal struct XGraphicsExposeEvent
	{
		// Token: 0x04000EC3 RID: 3779
		internal XEventName type;

		// Token: 0x04000EC4 RID: 3780
		internal IntPtr serial;

		// Token: 0x04000EC5 RID: 3781
		internal bool send_event;

		// Token: 0x04000EC6 RID: 3782
		internal IntPtr display;

		// Token: 0x04000EC7 RID: 3783
		internal IntPtr drawable;

		// Token: 0x04000EC8 RID: 3784
		internal int x;

		// Token: 0x04000EC9 RID: 3785
		internal int y;

		// Token: 0x04000ECA RID: 3786
		internal int width;

		// Token: 0x04000ECB RID: 3787
		internal int height;

		// Token: 0x04000ECC RID: 3788
		internal int count;

		// Token: 0x04000ECD RID: 3789
		internal int major_code;

		// Token: 0x04000ECE RID: 3790
		internal int minor_code;
	}
}
