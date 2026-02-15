using System;
using System.Runtime.InteropServices;

namespace UnityEngineInternal.Input
{
	// Token: 0x02000005 RID: 5
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 20)]
	internal struct NativeInputEvent
	{
		// Token: 0x0400000B RID: 11
		public const int structSize = 20;

		// Token: 0x0400000C RID: 12
		[FieldOffset(0)]
		public NativeInputEventType type;

		// Token: 0x0400000D RID: 13
		[FieldOffset(4)]
		public ushort sizeInBytes;

		// Token: 0x0400000E RID: 14
		[FieldOffset(6)]
		public ushort deviceId;

		// Token: 0x0400000F RID: 15
		[FieldOffset(8)]
		public double time;

		// Token: 0x04000010 RID: 16
		[FieldOffset(16)]
		public int eventId;
	}
}
