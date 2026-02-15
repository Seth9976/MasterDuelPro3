using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000B9 RID: 185
	[NativeContainer]
	[GenerateTestsForBurstCompatibility]
	internal struct NativeQueueDispose
	{
		// Token: 0x060008D7 RID: 2263 RVA: 0x0001ADAD File Offset: 0x00018FAD
		public void Dispose()
		{
			UnsafeQueue<int>.Free(this.m_QueueData);
		}

		// Token: 0x040003E2 RID: 994
		[NativeDisableUnsafePtrRestriction]
		public unsafe UnsafeQueue<int>* m_QueueData;
	}
}
