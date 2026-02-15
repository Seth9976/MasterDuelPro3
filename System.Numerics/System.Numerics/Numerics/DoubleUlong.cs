using System;
using System.Runtime.InteropServices;

namespace System.Numerics
{
	// Token: 0x02000008 RID: 8
	[StructLayout(LayoutKind.Explicit)]
	internal struct DoubleUlong
	{
		// Token: 0x04000014 RID: 20
		[FieldOffset(0)]
		public double dbl;

		// Token: 0x04000015 RID: 21
		[FieldOffset(0)]
		public ulong uu;
	}
}
