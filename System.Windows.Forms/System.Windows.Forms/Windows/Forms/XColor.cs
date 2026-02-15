using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000269 RID: 617
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	internal struct XColor
	{
		// Token: 0x04001063 RID: 4195
		internal IntPtr pixel;

		// Token: 0x04001064 RID: 4196
		internal ushort red;

		// Token: 0x04001065 RID: 4197
		internal ushort green;

		// Token: 0x04001066 RID: 4198
		internal ushort blue;

		// Token: 0x04001067 RID: 4199
		internal byte flags;

		// Token: 0x04001068 RID: 4200
		internal byte pad;
	}
}
