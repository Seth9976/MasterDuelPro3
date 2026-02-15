using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x02000059 RID: 89
	[NativeContainer]
	internal struct NativeArrayDispose
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00003FA8 File Offset: 0x000021A8
		public void Dispose()
		{
			UnsafeUtility.FreeTracked(this.m_Buffer, this.m_AllocatorLabel);
		}

		// Token: 0x04000107 RID: 263
		[NativeDisableUnsafePtrRestriction]
		internal unsafe void* m_Buffer;

		// Token: 0x04000108 RID: 264
		internal Allocator m_AllocatorLabel;
	}
}
