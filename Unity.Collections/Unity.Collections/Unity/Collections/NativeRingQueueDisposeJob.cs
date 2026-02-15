using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000C2 RID: 194
	[BurstCompile]
	internal struct NativeRingQueueDisposeJob : IJob
	{
		// Token: 0x060008FF RID: 2303 RVA: 0x0001B1B3 File Offset: 0x000193B3
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x040003ED RID: 1005
		public NativeRingQueueDispose Data;
	}
}
