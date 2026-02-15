using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000126 RID: 294
	[NativeContainer]
	[GenerateTestsForBurstCompatibility]
	internal struct UnsafeParallelHashMapDataDispose
	{
		// Token: 0x06000C44 RID: 3140 RVA: 0x00025081 File Offset: 0x00023281
		public void Dispose()
		{
			UnsafeParallelHashMapData.DeallocateHashMap(this.m_Buffer, this.m_AllocatorLabel);
		}

		// Token: 0x040004F6 RID: 1270
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x040004F7 RID: 1271
		internal AllocatorManager.AllocatorHandle m_AllocatorLabel;
	}
}
