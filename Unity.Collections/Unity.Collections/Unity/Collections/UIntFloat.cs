using System;
using System.Runtime.InteropServices;

namespace Unity.Collections
{
	// Token: 0x020000E3 RID: 227
	[StructLayout(LayoutKind.Explicit)]
	internal struct UIntFloat
	{
		// Token: 0x04000440 RID: 1088
		[FieldOffset(0)]
		public float floatValue;

		// Token: 0x04000441 RID: 1089
		[FieldOffset(0)]
		public uint intValue;

		// Token: 0x04000442 RID: 1090
		[FieldOffset(0)]
		public double doubleValue;

		// Token: 0x04000443 RID: 1091
		[FieldOffset(0)]
		public ulong longValue;
	}
}
