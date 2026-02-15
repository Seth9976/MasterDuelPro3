using System;
using System.Runtime.InteropServices;

namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	// Token: 0x02000163 RID: 355
	[StructLayout(LayoutKind.Explicit, Size = 47)]
	internal struct DualSenseHIDOutputReportPayload
	{
		// Token: 0x040008DC RID: 2268
		[FieldOffset(0)]
		public byte enableFlags1;

		// Token: 0x040008DD RID: 2269
		[FieldOffset(1)]
		public byte enableFlags2;

		// Token: 0x040008DE RID: 2270
		[FieldOffset(2)]
		public byte highFrequencyMotorSpeed;

		// Token: 0x040008DF RID: 2271
		[FieldOffset(3)]
		public byte lowFrequencyMotorSpeed;

		// Token: 0x040008E0 RID: 2272
		[FieldOffset(44)]
		public byte redColor;

		// Token: 0x040008E1 RID: 2273
		[FieldOffset(45)]
		public byte greenColor;

		// Token: 0x040008E2 RID: 2274
		[FieldOffset(46)]
		public byte blueColor;
	}
}
