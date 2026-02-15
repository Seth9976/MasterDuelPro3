using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000476 RID: 1142
	[StructLayout(LayoutKind.Explicit)]
	internal struct AlignmentUnion
	{
		// Token: 0x04001335 RID: 4917
		[FieldOffset(0)]
		public ulong Alignment;

		// Token: 0x04001336 RID: 4918
		[FieldOffset(0)]
		public int Length;

		// Token: 0x04001337 RID: 4919
		[FieldOffset(4)]
		public int IfIndex;
	}
}
