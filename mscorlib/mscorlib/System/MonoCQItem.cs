using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020001D4 RID: 468
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class MonoCQItem
	{
		// Token: 0x04000766 RID: 1894
		private object[] array;

		// Token: 0x04000767 RID: 1895
		private byte[] array_state;

		// Token: 0x04000768 RID: 1896
		private int head;

		// Token: 0x04000769 RID: 1897
		private int tail;
	}
}
