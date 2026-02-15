using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000BE RID: 190
	[BurstCompile]
	internal struct NativeReferenceDisposeJob : IJob
	{
		// Token: 0x060008EE RID: 2286 RVA: 0x0001AFFC File Offset: 0x000191FC
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x040003E9 RID: 1001
		internal NativeReferenceDispose Data;
	}
}
