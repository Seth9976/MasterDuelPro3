using System;

namespace System.Windows.Forms
{
	// Token: 0x0200024F RID: 591
	internal struct XMapRequestEvent
	{
		// Token: 0x04000EFC RID: 3836
		internal XEventName type;

		// Token: 0x04000EFD RID: 3837
		internal IntPtr serial;

		// Token: 0x04000EFE RID: 3838
		internal bool send_event;

		// Token: 0x04000EFF RID: 3839
		internal IntPtr display;

		// Token: 0x04000F00 RID: 3840
		internal IntPtr parent;

		// Token: 0x04000F01 RID: 3841
		internal IntPtr window;
	}
}
