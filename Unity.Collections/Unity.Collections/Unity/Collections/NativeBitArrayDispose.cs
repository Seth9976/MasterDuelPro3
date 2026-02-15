using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x0200008B RID: 139
	[NativeContainer]
	[GenerateTestsForBurstCompatibility]
	internal struct NativeBitArrayDispose
	{
		// Token: 0x0600071B RID: 1819 RVA: 0x000175DF File Offset: 0x000157DF
		public void Dispose()
		{
			UnsafeBitArray.Free(this.m_BitArrayData, this.m_Allocator);
		}

		// Token: 0x040003AF RID: 943
		[NativeDisableUnsafePtrRestriction]
		public unsafe UnsafeBitArray* m_BitArrayData;

		// Token: 0x040003B0 RID: 944
		public AllocatorManager.AllocatorHandle m_Allocator;
	}
}
