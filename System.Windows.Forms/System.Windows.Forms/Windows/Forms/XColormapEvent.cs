using System;

namespace System.Windows.Forms
{
	// Token: 0x0200025B RID: 603
	internal struct XColormapEvent
	{
		// Token: 0x04000F66 RID: 3942
		internal XEventName type;

		// Token: 0x04000F67 RID: 3943
		internal IntPtr serial;

		// Token: 0x04000F68 RID: 3944
		internal bool send_event;

		// Token: 0x04000F69 RID: 3945
		internal IntPtr display;

		// Token: 0x04000F6A RID: 3946
		internal IntPtr window;

		// Token: 0x04000F6B RID: 3947
		internal IntPtr colormap;

		// Token: 0x04000F6C RID: 3948
		internal bool c_new;

		// Token: 0x04000F6D RID: 3949
		internal int state;
	}
}
