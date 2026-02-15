using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000127 RID: 295
	[BurstCompile]
	internal struct UnsafeParallelHashMapDataDisposeJob : IJob
	{
		// Token: 0x06000C45 RID: 3141 RVA: 0x00025094 File Offset: 0x00023294
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x040004F8 RID: 1272
		internal UnsafeParallelHashMapDataDispose Data;
	}
}
