using System;

namespace System.Windows.Forms
{
	// Token: 0x02000241 RID: 577
	internal struct XKeyEvent
	{
		// Token: 0x04000E4F RID: 3663
		internal XEventName type;

		// Token: 0x04000E50 RID: 3664
		internal IntPtr serial;

		// Token: 0x04000E51 RID: 3665
		internal bool send_event;

		// Token: 0x04000E52 RID: 3666
		internal IntPtr display;

		// Token: 0x04000E53 RID: 3667
		internal IntPtr window;

		// Token: 0x04000E54 RID: 3668
		internal IntPtr root;

		// Token: 0x04000E55 RID: 3669
		internal IntPtr subwindow;

		// Token: 0x04000E56 RID: 3670
		internal IntPtr time;

		// Token: 0x04000E57 RID: 3671
		internal int x;

		// Token: 0x04000E58 RID: 3672
		internal int y;

		// Token: 0x04000E59 RID: 3673
		internal int x_root;

		// Token: 0x04000E5A RID: 3674
		internal int y_root;

		// Token: 0x04000E5B RID: 3675
		internal int state;

		// Token: 0x04000E5C RID: 3676
		internal int keycode;

		// Token: 0x04000E5D RID: 3677
		internal bool same_screen;
	}
}
