using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x02000034 RID: 52
	[StructLayout(LayoutKind.Explicit, Size = 24)]
	public readonly struct ProfilerCategoryDescription
	{
		// Token: 0x04000093 RID: 147
		[FieldOffset(0)]
		public readonly ushort Id;

		// Token: 0x04000094 RID: 148
		[FieldOffset(2)]
		public readonly ushort Flags;

		// Token: 0x04000095 RID: 149
		[FieldOffset(4)]
		public readonly Color32 Color;

		// Token: 0x04000096 RID: 150
		[FieldOffset(8)]
		private readonly int reserved0;

		// Token: 0x04000097 RID: 151
		[FieldOffset(12)]
		public readonly int NameUtf8Len;

		// Token: 0x04000098 RID: 152
		[FieldOffset(16)]
		public unsafe readonly byte* NameUtf8;
	}
}
