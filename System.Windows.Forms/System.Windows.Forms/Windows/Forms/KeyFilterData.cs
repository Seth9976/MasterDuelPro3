using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020002AC RID: 684
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct KeyFilterData
	{
		// Token: 0x0400146F RID: 5231
		internal bool Down;

		// Token: 0x04001470 RID: 5232
		internal int keycode;

		// Token: 0x04001471 RID: 5233
		internal int keysym;

		// Token: 0x04001472 RID: 5234
		internal Keys ModifierKeys;

		// Token: 0x04001473 RID: 5235
		internal string str;
	}
}
