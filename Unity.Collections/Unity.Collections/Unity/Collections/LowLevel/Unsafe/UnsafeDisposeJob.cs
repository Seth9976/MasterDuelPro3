using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000115 RID: 277
	[BurstCompile]
	internal struct UnsafeDisposeJob : IJob
	{
		// Token: 0x06000BAD RID: 2989 RVA: 0x00023890 File Offset: 0x00021A90
		public void Execute()
		{
			AllocatorManager.Free(this.Allocator, this.Ptr);
		}

		// Token: 0x040004C7 RID: 1223
		[NativeDisableUnsafePtrRestriction]
		public unsafe void* Ptr;

		// Token: 0x040004C8 RID: 1224
		public AllocatorManager.AllocatorHandle Allocator;
	}
}
