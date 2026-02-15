using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x0200009C RID: 156
	[NativeContainer]
	[GenerateTestsForBurstCompatibility]
	internal struct NativeListDispose
	{
		// Token: 0x060007C2 RID: 1986 RVA: 0x0001837C File Offset: 0x0001657C
		public unsafe void Dispose()
		{
			UnsafeList<int>* listData = (UnsafeList<int>*)this.m_ListData;
			UnsafeList<int>.Destroy(listData);
		}

		// Token: 0x040003C2 RID: 962
		[NativeDisableUnsafePtrRestriction]
		public unsafe UntypedUnsafeList* m_ListData;
	}
}
