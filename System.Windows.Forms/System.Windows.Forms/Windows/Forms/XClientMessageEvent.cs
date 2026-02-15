using System;

namespace System.Windows.Forms
{
	// Token: 0x0200025C RID: 604
	internal struct XClientMessageEvent
	{
		// Token: 0x04000F6E RID: 3950
		internal XEventName type;

		// Token: 0x04000F6F RID: 3951
		internal IntPtr serial;

		// Token: 0x04000F70 RID: 3952
		internal bool send_event;

		// Token: 0x04000F71 RID: 3953
		internal IntPtr display;

		// Token: 0x04000F72 RID: 3954
		internal IntPtr window;

		// Token: 0x04000F73 RID: 3955
		internal IntPtr message_type;

		// Token: 0x04000F74 RID: 3956
		internal int format;

		// Token: 0x04000F75 RID: 3957
		internal IntPtr ptr1;

		// Token: 0x04000F76 RID: 3958
		internal IntPtr ptr2;

		// Token: 0x04000F77 RID: 3959
		internal IntPtr ptr3;

		// Token: 0x04000F78 RID: 3960
		internal IntPtr ptr4;

		// Token: 0x04000F79 RID: 3961
		internal IntPtr ptr5;
	}
}
