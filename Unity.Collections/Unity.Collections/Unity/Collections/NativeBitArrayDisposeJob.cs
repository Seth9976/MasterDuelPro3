using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x0200008C RID: 140
	[BurstCompile]
	internal struct NativeBitArrayDisposeJob : IJob
	{
		// Token: 0x0600071C RID: 1820 RVA: 0x000175F2 File Offset: 0x000157F2
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x040003B1 RID: 945
		public NativeBitArrayDispose Data;
	}
}
