using System;

namespace System.Windows.Forms
{
	// Token: 0x0200024A RID: 586
	internal struct XVisibilityEvent
	{
		// Token: 0x04000ED6 RID: 3798
		internal XEventName type;

		// Token: 0x04000ED7 RID: 3799
		internal IntPtr serial;

		// Token: 0x04000ED8 RID: 3800
		internal bool send_event;

		// Token: 0x04000ED9 RID: 3801
		internal IntPtr display;

		// Token: 0x04000EDA RID: 3802
		internal IntPtr window;

		// Token: 0x04000EDB RID: 3803
		internal int state;
	}
}
