using System;

namespace System.Windows.Forms
{
	// Token: 0x02000242 RID: 578
	internal struct XButtonEvent
	{
		// Token: 0x04000E5E RID: 3678
		internal XEventName type;

		// Token: 0x04000E5F RID: 3679
		internal IntPtr serial;

		// Token: 0x04000E60 RID: 3680
		internal bool send_event;

		// Token: 0x04000E61 RID: 3681
		internal IntPtr display;

		// Token: 0x04000E62 RID: 3682
		internal IntPtr window;

		// Token: 0x04000E63 RID: 3683
		internal IntPtr root;

		// Token: 0x04000E64 RID: 3684
		internal IntPtr subwindow;

		// Token: 0x04000E65 RID: 3685
		internal IntPtr time;

		// Token: 0x04000E66 RID: 3686
		internal int x;

		// Token: 0x04000E67 RID: 3687
		internal int y;

		// Token: 0x04000E68 RID: 3688
		internal int x_root;

		// Token: 0x04000E69 RID: 3689
		internal int y_root;

		// Token: 0x04000E6A RID: 3690
		internal int state;

		// Token: 0x04000E6B RID: 3691
		internal int button;

		// Token: 0x04000E6C RID: 3692
		internal bool same_screen;
	}
}
