using System;

namespace System.Windows.Forms
{
	// Token: 0x02000244 RID: 580
	internal struct XCrossingEvent
	{
		// Token: 0x04000E7C RID: 3708
		internal XEventName type;

		// Token: 0x04000E7D RID: 3709
		internal IntPtr serial;

		// Token: 0x04000E7E RID: 3710
		internal bool send_event;

		// Token: 0x04000E7F RID: 3711
		internal IntPtr display;

		// Token: 0x04000E80 RID: 3712
		internal IntPtr window;

		// Token: 0x04000E81 RID: 3713
		internal IntPtr root;

		// Token: 0x04000E82 RID: 3714
		internal IntPtr subwindow;

		// Token: 0x04000E83 RID: 3715
		internal IntPtr time;

		// Token: 0x04000E84 RID: 3716
		internal int x;

		// Token: 0x04000E85 RID: 3717
		internal int y;

		// Token: 0x04000E86 RID: 3718
		internal int x_root;

		// Token: 0x04000E87 RID: 3719
		internal int y_root;

		// Token: 0x04000E88 RID: 3720
		internal NotifyMode mode;

		// Token: 0x04000E89 RID: 3721
		internal NotifyDetail detail;

		// Token: 0x04000E8A RID: 3722
		internal bool same_screen;

		// Token: 0x04000E8B RID: 3723
		internal bool focus;

		// Token: 0x04000E8C RID: 3724
		internal int state;
	}
}
