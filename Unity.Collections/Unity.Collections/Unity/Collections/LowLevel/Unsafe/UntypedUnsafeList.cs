using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000116 RID: 278
	internal struct UntypedUnsafeList
	{
		// Token: 0x040004C9 RID: 1225
		[NativeDisableUnsafePtrRestriction]
		internal unsafe readonly void* Ptr;

		// Token: 0x040004CA RID: 1226
		internal readonly int m_length;

		// Token: 0x040004CB RID: 1227
		internal readonly int m_capacity;

		// Token: 0x040004CC RID: 1228
		internal readonly AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x040004CD RID: 1229
		internal readonly int padding;
	}
}
