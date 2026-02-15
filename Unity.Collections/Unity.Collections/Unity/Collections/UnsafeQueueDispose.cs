using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000F4 RID: 244
	[GenerateTestsForBurstCompatibility]
	internal struct UnsafeQueueDispose
	{
		// Token: 0x06000A7D RID: 2685 RVA: 0x0001F877 File Offset: 0x0001DA77
		public void Dispose()
		{
			UnsafeQueueData.DeallocateQueue(this.m_Buffer, this.m_QueuePool, this.m_AllocatorLabel);
		}

		// Token: 0x04000471 RID: 1137
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeQueueData* m_Buffer;

		// Token: 0x04000472 RID: 1138
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeQueueBlockPoolData* m_QueuePool;

		// Token: 0x04000473 RID: 1139
		internal AllocatorManager.AllocatorHandle m_AllocatorLabel;
	}
}
