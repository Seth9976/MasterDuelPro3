using System;

namespace System.Windows.Forms
{
	// Token: 0x0200025E RID: 606
	internal struct XErrorEvent
	{
		// Token: 0x04000F82 RID: 3970
		internal XEventName type;

		// Token: 0x04000F83 RID: 3971
		internal IntPtr display;

		// Token: 0x04000F84 RID: 3972
		internal IntPtr resourceid;

		// Token: 0x04000F85 RID: 3973
		internal IntPtr serial;

		// Token: 0x04000F86 RID: 3974
		internal byte error_code;

		// Token: 0x04000F87 RID: 3975
		internal XRequest request_code;

		// Token: 0x04000F88 RID: 3976
		internal byte minor_code;
	}
}
