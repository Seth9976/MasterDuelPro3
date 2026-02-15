using System;

namespace System.Windows.Forms
{
	// Token: 0x0200025A RID: 602
	internal struct XSelectionEvent
	{
		// Token: 0x04000F5D RID: 3933
		internal XEventName type;

		// Token: 0x04000F5E RID: 3934
		internal IntPtr serial;

		// Token: 0x04000F5F RID: 3935
		internal bool send_event;

		// Token: 0x04000F60 RID: 3936
		internal IntPtr display;

		// Token: 0x04000F61 RID: 3937
		internal IntPtr requestor;

		// Token: 0x04000F62 RID: 3938
		internal IntPtr selection;

		// Token: 0x04000F63 RID: 3939
		internal IntPtr target;

		// Token: 0x04000F64 RID: 3940
		internal IntPtr property;

		// Token: 0x04000F65 RID: 3941
		internal IntPtr time;
	}
}
