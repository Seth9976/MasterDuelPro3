using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000D1 RID: 209
	[BurstCompile]
	internal struct NativeStreamDisposeJob : IJob
	{
		// Token: 0x06000967 RID: 2407 RVA: 0x0001C57D File Offset: 0x0001A77D
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x0400040B RID: 1035
		public NativeStreamDispose Data;
	}
}
