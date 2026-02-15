using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x0200008E RID: 142
	[BurstCompile]
	internal struct NativeHashMapDisposeJob : IJob
	{
		// Token: 0x0600071E RID: 1822 RVA: 0x0001761A File Offset: 0x0001581A
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x040003B4 RID: 948
		internal NativeHashMapDispose Data;
	}
}
