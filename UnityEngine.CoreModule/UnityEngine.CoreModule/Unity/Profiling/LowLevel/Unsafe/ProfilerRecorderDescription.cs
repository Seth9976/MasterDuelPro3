using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x02000031 RID: 49
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Explicit, Size = 24)]
	public readonly struct ProfilerRecorderDescription
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002EA3 File Offset: 0x000010A3
		public MarkerFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x04000086 RID: 134
		[FieldOffset(0)]
		private readonly ProfilerCategory category;

		// Token: 0x04000087 RID: 135
		[FieldOffset(2)]
		private readonly MarkerFlags flags;

		// Token: 0x04000088 RID: 136
		[FieldOffset(4)]
		private readonly ProfilerMarkerDataType dataType;

		// Token: 0x04000089 RID: 137
		[FieldOffset(5)]
		private readonly ProfilerMarkerDataUnit unitType;

		// Token: 0x0400008A RID: 138
		[FieldOffset(8)]
		private readonly int reserved0;

		// Token: 0x0400008B RID: 139
		[FieldOffset(12)]
		private readonly int nameUtf8Len;

		// Token: 0x0400008C RID: 140
		[FieldOffset(16)]
		private unsafe readonly byte* nameUtf8;
	}
}
