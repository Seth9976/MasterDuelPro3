using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x0200009D RID: 157
	[BurstCompile]
	[GenerateTestsForBurstCompatibility]
	internal struct NativeListDisposeJob : IJob
	{
		// Token: 0x060007C3 RID: 1987 RVA: 0x00018396 File Offset: 0x00016596
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x040003C3 RID: 963
		internal NativeListDispose Data;
	}
}
