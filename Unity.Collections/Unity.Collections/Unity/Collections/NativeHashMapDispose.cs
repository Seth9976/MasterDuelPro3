using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x0200008D RID: 141
	[NativeContainer]
	[GenerateTestsForBurstCompatibility]
	internal struct NativeHashMapDispose
	{
		// Token: 0x0600071D RID: 1821 RVA: 0x00017600 File Offset: 0x00015800
		internal unsafe void Dispose()
		{
			HashMapHelper<int>* hashMapData = (HashMapHelper<int>*)this.m_HashMapData;
			HashMapHelper<int>.Free(hashMapData);
		}

		// Token: 0x040003B2 RID: 946
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeHashMap<int, int>* m_HashMapData;

		// Token: 0x040003B3 RID: 947
		internal AllocatorManager.AllocatorHandle m_Allocator;
	}
}
