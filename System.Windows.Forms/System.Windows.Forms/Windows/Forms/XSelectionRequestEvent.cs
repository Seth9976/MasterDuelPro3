using System;

namespace System.Windows.Forms
{
	// Token: 0x02000259 RID: 601
	internal struct XSelectionRequestEvent
	{
		// Token: 0x04000F53 RID: 3923
		internal XEventName type;

		// Token: 0x04000F54 RID: 3924
		internal IntPtr serial;

		// Token: 0x04000F55 RID: 3925
		internal bool send_event;

		// Token: 0x04000F56 RID: 3926
		internal IntPtr display;

		// Token: 0x04000F57 RID: 3927
		internal IntPtr owner;

		// Token: 0x04000F58 RID: 3928
		internal IntPtr requestor;

		// Token: 0x04000F59 RID: 3929
		internal IntPtr selection;

		// Token: 0x04000F5A RID: 3930
		internal IntPtr target;

		// Token: 0x04000F5B RID: 3931
		internal IntPtr property;

		// Token: 0x04000F5C RID: 3932
		internal IntPtr time;
	}
}
