using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020001D3 RID: 467
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoAsyncCall
	{
		// Token: 0x04000760 RID: 1888
		private object msg;

		// Token: 0x04000761 RID: 1889
		private IntPtr cb_method;

		// Token: 0x04000762 RID: 1890
		private object cb_target;

		// Token: 0x04000763 RID: 1891
		private object state;

		// Token: 0x04000764 RID: 1892
		private object res;

		// Token: 0x04000765 RID: 1893
		private object out_args;
	}
}
