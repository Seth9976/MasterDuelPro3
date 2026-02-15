using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000D6 RID: 214
	[BurstCompile]
	internal struct NativeTextDisposeJob : IJob
	{
		// Token: 0x060009E4 RID: 2532 RVA: 0x0001D1FE File Offset: 0x0001B3FE
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x04000412 RID: 1042
		public NativeTextDispose Data;
	}
}
