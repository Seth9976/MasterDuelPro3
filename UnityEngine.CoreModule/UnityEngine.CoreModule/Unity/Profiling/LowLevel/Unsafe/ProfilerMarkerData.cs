using System;
using System.Runtime.InteropServices;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x02000033 RID: 51
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct ProfilerMarkerData
	{
		// Token: 0x0400008E RID: 142
		[FieldOffset(0)]
		public byte Type;

		// Token: 0x0400008F RID: 143
		[FieldOffset(1)]
		private readonly byte reserved0;

		// Token: 0x04000090 RID: 144
		[FieldOffset(2)]
		private readonly ushort reserved1;

		// Token: 0x04000091 RID: 145
		[FieldOffset(4)]
		public uint Size;

		// Token: 0x04000092 RID: 146
		[FieldOffset(8)]
		public unsafe void* Ptr;
	}
}
