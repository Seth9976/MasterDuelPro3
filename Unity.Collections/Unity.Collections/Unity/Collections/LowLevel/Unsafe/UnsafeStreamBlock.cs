using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000141 RID: 321
	[GenerateTestsForBurstCompatibility]
	internal struct UnsafeStreamBlock
	{
		// Token: 0x04000528 RID: 1320
		internal unsafe UnsafeStreamBlock* Next;

		// Token: 0x04000529 RID: 1321
		[FixedBuffer(typeof(byte), 1)]
		internal UnsafeStreamBlock.<Data>e__FixedBuffer Data;

		// Token: 0x02000142 RID: 322
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct <Data>e__FixedBuffer
		{
			// Token: 0x0400052A RID: 1322
			public byte FixedElementField;
		}
	}
}
