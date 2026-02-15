using System;

namespace System.Windows.Forms
{
	// Token: 0x02000251 RID: 593
	internal struct XConfigureEvent
	{
		// Token: 0x04000F0C RID: 3852
		internal XEventName type;

		// Token: 0x04000F0D RID: 3853
		internal IntPtr serial;

		// Token: 0x04000F0E RID: 3854
		internal bool send_event;

		// Token: 0x04000F0F RID: 3855
		internal IntPtr display;

		// Token: 0x04000F10 RID: 3856
		internal IntPtr xevent;

		// Token: 0x04000F11 RID: 3857
		internal IntPtr window;

		// Token: 0x04000F12 RID: 3858
		internal int x;

		// Token: 0x04000F13 RID: 3859
		internal int y;

		// Token: 0x04000F14 RID: 3860
		internal int width;

		// Token: 0x04000F15 RID: 3861
		internal int height;

		// Token: 0x04000F16 RID: 3862
		internal int border_width;

		// Token: 0x04000F17 RID: 3863
		internal IntPtr above;

		// Token: 0x04000F18 RID: 3864
		internal bool override_redirect;
	}
}
