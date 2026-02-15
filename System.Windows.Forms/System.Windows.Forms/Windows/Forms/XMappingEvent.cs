using System;

namespace System.Windows.Forms
{
	// Token: 0x0200025D RID: 605
	internal struct XMappingEvent
	{
		// Token: 0x04000F7A RID: 3962
		internal XEventName type;

		// Token: 0x04000F7B RID: 3963
		internal IntPtr serial;

		// Token: 0x04000F7C RID: 3964
		internal bool send_event;

		// Token: 0x04000F7D RID: 3965
		internal IntPtr display;

		// Token: 0x04000F7E RID: 3966
		internal IntPtr window;

		// Token: 0x04000F7F RID: 3967
		internal int request;

		// Token: 0x04000F80 RID: 3968
		internal int first_keycode;

		// Token: 0x04000F81 RID: 3969
		internal int count;
	}
}
