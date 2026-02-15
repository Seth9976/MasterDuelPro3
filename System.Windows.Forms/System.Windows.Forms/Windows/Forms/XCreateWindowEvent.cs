using System;

namespace System.Windows.Forms
{
	// Token: 0x0200024B RID: 587
	internal struct XCreateWindowEvent
	{
		// Token: 0x04000EDC RID: 3804
		internal XEventName type;

		// Token: 0x04000EDD RID: 3805
		internal IntPtr serial;

		// Token: 0x04000EDE RID: 3806
		internal bool send_event;

		// Token: 0x04000EDF RID: 3807
		internal IntPtr display;

		// Token: 0x04000EE0 RID: 3808
		internal IntPtr parent;

		// Token: 0x04000EE1 RID: 3809
		internal IntPtr window;

		// Token: 0x04000EE2 RID: 3810
		internal int x;

		// Token: 0x04000EE3 RID: 3811
		internal int y;

		// Token: 0x04000EE4 RID: 3812
		internal int width;

		// Token: 0x04000EE5 RID: 3813
		internal int height;

		// Token: 0x04000EE6 RID: 3814
		internal int border_width;

		// Token: 0x04000EE7 RID: 3815
		internal bool override_redirect;
	}
}
