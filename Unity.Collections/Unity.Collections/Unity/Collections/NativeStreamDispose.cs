using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000D0 RID: 208
	[NativeContainer]
	[GenerateTestsForBurstCompatibility]
	internal struct NativeStreamDispose
	{
		// Token: 0x06000966 RID: 2406 RVA: 0x0001C570 File Offset: 0x0001A770
		public void Dispose()
		{
			this.m_StreamData.Dispose();
		}

		// Token: 0x0400040A RID: 1034
		public UnsafeStream m_StreamData;
	}
}
