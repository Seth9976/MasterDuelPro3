using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000131 RID: 305
	public struct UntypedUnsafeParallelHashMap
	{
		// Token: 0x0400050B RID: 1291
		[NativeDisableUnsafePtrRestriction]
		private unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x0400050C RID: 1292
		private AllocatorManager.AllocatorHandle m_AllocatorLabel;
	}
}
