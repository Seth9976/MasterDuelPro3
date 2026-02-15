using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000426 RID: 1062
	internal struct Hashes
	{
		// Token: 0x04000D6D RID: 3437
		public const int kSize = 4;

		// Token: 0x04000D6E RID: 3438
		[FixedBuffer(typeof(int), 4)]
		public Hashes.<hashes>e__FixedBuffer hashes;

		// Token: 0x02000427 RID: 1063
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 16)]
		public struct <hashes>e__FixedBuffer
		{
			// Token: 0x04000D6F RID: 3439
			public int FixedElementField;
		}
	}
}
