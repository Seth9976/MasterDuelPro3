using System;

namespace System.Windows.Forms
{
	// Token: 0x02000262 RID: 610
	internal struct XWindowAttributes
	{
		// Token: 0x0600173F RID: 5951 RVA: 0x0007497D File Offset: 0x00072B7D
		public override string ToString()
		{
			return XEvent.ToString(this);
		}

		// Token: 0x04000FD1 RID: 4049
		internal int x;

		// Token: 0x04000FD2 RID: 4050
		internal int y;

		// Token: 0x04000FD3 RID: 4051
		internal int width;

		// Token: 0x04000FD4 RID: 4052
		internal int height;

		// Token: 0x04000FD5 RID: 4053
		internal int border_width;

		// Token: 0x04000FD6 RID: 4054
		internal int depth;

		// Token: 0x04000FD7 RID: 4055
		internal IntPtr visual;

		// Token: 0x04000FD8 RID: 4056
		internal IntPtr root;

		// Token: 0x04000FD9 RID: 4057
		internal int c_class;

		// Token: 0x04000FDA RID: 4058
		internal Gravity bit_gravity;

		// Token: 0x04000FDB RID: 4059
		internal Gravity win_gravity;

		// Token: 0x04000FDC RID: 4060
		internal int backing_store;

		// Token: 0x04000FDD RID: 4061
		internal IntPtr backing_planes;

		// Token: 0x04000FDE RID: 4062
		internal IntPtr backing_pixel;

		// Token: 0x04000FDF RID: 4063
		internal bool save_under;

		// Token: 0x04000FE0 RID: 4064
		internal IntPtr colormap;

		// Token: 0x04000FE1 RID: 4065
		internal bool map_installed;

		// Token: 0x04000FE2 RID: 4066
		internal MapState map_state;

		// Token: 0x04000FE3 RID: 4067
		internal IntPtr all_event_masks;

		// Token: 0x04000FE4 RID: 4068
		internal IntPtr your_event_mask;

		// Token: 0x04000FE5 RID: 4069
		internal IntPtr do_not_propagate_mask;

		// Token: 0x04000FE6 RID: 4070
		internal bool override_direct;

		// Token: 0x04000FE7 RID: 4071
		internal IntPtr screen;
	}
}
