using System;

namespace System.Windows.Forms
{
	// Token: 0x02000254 RID: 596
	internal struct XConfigureRequestEvent
	{
		// Token: 0x04000F28 RID: 3880
		internal XEventName type;

		// Token: 0x04000F29 RID: 3881
		internal IntPtr serial;

		// Token: 0x04000F2A RID: 3882
		internal bool send_event;

		// Token: 0x04000F2B RID: 3883
		internal IntPtr display;

		// Token: 0x04000F2C RID: 3884
		internal IntPtr parent;

		// Token: 0x04000F2D RID: 3885
		internal IntPtr window;

		// Token: 0x04000F2E RID: 3886
		internal int x;

		// Token: 0x04000F2F RID: 3887
		internal int y;

		// Token: 0x04000F30 RID: 3888
		internal int width;

		// Token: 0x04000F31 RID: 3889
		internal int height;

		// Token: 0x04000F32 RID: 3890
		internal int border_width;

		// Token: 0x04000F33 RID: 3891
		internal IntPtr above;

		// Token: 0x04000F34 RID: 3892
		internal int detail;

		// Token: 0x04000F35 RID: 3893
		internal IntPtr value_mask;
	}
}
