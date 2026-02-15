using System;

namespace System.Windows.Forms
{
	// Token: 0x020002A5 RID: 677
	internal struct HELPINFO
	{
		// Token: 0x04001408 RID: 5128
		internal uint cbSize;

		// Token: 0x04001409 RID: 5129
		internal int iContextType;

		// Token: 0x0400140A RID: 5130
		internal int iCtrlId;

		// Token: 0x0400140B RID: 5131
		internal IntPtr hItemHandle;

		// Token: 0x0400140C RID: 5132
		internal uint dwContextId;

		// Token: 0x0400140D RID: 5133
		internal POINT MousePos;
	}
}
