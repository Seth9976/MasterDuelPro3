using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200012F RID: 303
	[BurstCompile]
	internal struct UnsafeParallelHashMapDisposeJob : IJob
	{
		// Token: 0x06000C8F RID: 3215 RVA: 0x00025FAB File Offset: 0x000241AB
		public void Execute()
		{
			UnsafeParallelHashMapData.DeallocateHashMap(this.Data, this.Allocator);
		}

		// Token: 0x04000508 RID: 1288
		[NativeDisableUnsafePtrRestriction]
		public unsafe UnsafeParallelHashMapData* Data;

		// Token: 0x04000509 RID: 1289
		public AllocatorManager.AllocatorHandle Allocator;
	}
}
