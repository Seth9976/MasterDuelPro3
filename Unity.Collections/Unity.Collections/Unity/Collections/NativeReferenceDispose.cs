using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000BD RID: 189
	[NativeContainer]
	internal struct NativeReferenceDispose
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x0001AFE9 File Offset: 0x000191E9
		public void Dispose()
		{
			Memory.Unmanaged.Free(this.m_Data, this.m_AllocatorLabel);
		}

		// Token: 0x040003E7 RID: 999
		[NativeDisableUnsafePtrRestriction]
		internal unsafe void* m_Data;

		// Token: 0x040003E8 RID: 1000
		internal AllocatorManager.AllocatorHandle m_AllocatorLabel;
	}
}
