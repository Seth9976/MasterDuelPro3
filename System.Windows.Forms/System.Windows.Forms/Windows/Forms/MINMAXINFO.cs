using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020002AB RID: 683
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct MINMAXINFO
	{
		// Token: 0x0400146A RID: 5226
		internal POINT ptReserved;

		// Token: 0x0400146B RID: 5227
		internal POINT ptMaxSize;

		// Token: 0x0400146C RID: 5228
		internal POINT ptMaxPosition;

		// Token: 0x0400146D RID: 5229
		internal POINT ptMinTrackSize;

		// Token: 0x0400146E RID: 5230
		internal POINT ptMaxTrackSize;
	}
}
