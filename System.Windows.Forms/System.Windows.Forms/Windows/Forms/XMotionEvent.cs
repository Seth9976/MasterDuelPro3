using System;

namespace System.Windows.Forms
{
	// Token: 0x02000243 RID: 579
	internal struct XMotionEvent
	{
		// Token: 0x04000E6D RID: 3693
		internal XEventName type;

		// Token: 0x04000E6E RID: 3694
		internal IntPtr serial;

		// Token: 0x04000E6F RID: 3695
		internal bool send_event;

		// Token: 0x04000E70 RID: 3696
		internal IntPtr display;

		// Token: 0x04000E71 RID: 3697
		internal IntPtr window;

		// Token: 0x04000E72 RID: 3698
		internal IntPtr root;

		// Token: 0x04000E73 RID: 3699
		internal IntPtr subwindow;

		// Token: 0x04000E74 RID: 3700
		internal IntPtr time;

		// Token: 0x04000E75 RID: 3701
		internal int x;

		// Token: 0x04000E76 RID: 3702
		internal int y;

		// Token: 0x04000E77 RID: 3703
		internal int x_root;

		// Token: 0x04000E78 RID: 3704
		internal int y_root;

		// Token: 0x04000E79 RID: 3705
		internal int state;

		// Token: 0x04000E7A RID: 3706
		internal byte is_hint;

		// Token: 0x04000E7B RID: 3707
		internal bool same_screen;
	}
}
