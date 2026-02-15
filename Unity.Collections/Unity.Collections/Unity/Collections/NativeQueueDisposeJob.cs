using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000BA RID: 186
	[BurstCompile]
	internal struct NativeQueueDisposeJob : IJob
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x0001ADBA File Offset: 0x00018FBA
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x040003E3 RID: 995
		public NativeQueueDispose Data;
	}
}
