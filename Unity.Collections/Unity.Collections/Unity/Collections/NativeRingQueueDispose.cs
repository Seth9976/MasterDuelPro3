using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000C1 RID: 193
	[NativeContainer]
	[GenerateTestsForBurstCompatibility]
	internal struct NativeRingQueueDispose
	{
		// Token: 0x060008FE RID: 2302 RVA: 0x0001B1A6 File Offset: 0x000193A6
		public void Dispose()
		{
			UnsafeRingQueue<int>.Free(this.m_QueueData);
		}

		// Token: 0x040003EC RID: 1004
		[NativeDisableUnsafePtrRestriction]
		public unsafe UnsafeRingQueue<int>* m_QueueData;
	}
}
