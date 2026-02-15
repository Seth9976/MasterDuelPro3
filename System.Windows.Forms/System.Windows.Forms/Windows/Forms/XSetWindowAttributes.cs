using System;

namespace System.Windows.Forms
{
	// Token: 0x02000261 RID: 609
	internal struct XSetWindowAttributes
	{
		// Token: 0x04000FC2 RID: 4034
		internal IntPtr background_pixmap;

		// Token: 0x04000FC3 RID: 4035
		internal IntPtr background_pixel;

		// Token: 0x04000FC4 RID: 4036
		internal IntPtr border_pixmap;

		// Token: 0x04000FC5 RID: 4037
		internal IntPtr border_pixel;

		// Token: 0x04000FC6 RID: 4038
		internal Gravity bit_gravity;

		// Token: 0x04000FC7 RID: 4039
		internal Gravity win_gravity;

		// Token: 0x04000FC8 RID: 4040
		internal int backing_store;

		// Token: 0x04000FC9 RID: 4041
		internal IntPtr backing_planes;

		// Token: 0x04000FCA RID: 4042
		internal IntPtr backing_pixel;

		// Token: 0x04000FCB RID: 4043
		internal bool save_under;

		// Token: 0x04000FCC RID: 4044
		internal IntPtr event_mask;

		// Token: 0x04000FCD RID: 4045
		internal IntPtr do_not_propagate_mask;

		// Token: 0x04000FCE RID: 4046
		internal bool override_redirect;

		// Token: 0x04000FCF RID: 4047
		internal IntPtr colormap;

		// Token: 0x04000FD0 RID: 4048
		internal IntPtr cursor;
	}
}
