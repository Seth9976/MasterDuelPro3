using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000F5 RID: 245
	[BurstCompile]
	internal struct UnsafeQueueDisposeJob : IJob
	{
		// Token: 0x06000A7E RID: 2686 RVA: 0x0001F890 File Offset: 0x0001DA90
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x04000474 RID: 1140
		public UnsafeQueueDispose Data;
	}
}
