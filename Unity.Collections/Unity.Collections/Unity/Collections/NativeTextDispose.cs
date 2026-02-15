using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000D5 RID: 213
	[NativeContainer]
	[GenerateTestsForBurstCompatibility]
	internal struct NativeTextDispose
	{
		// Token: 0x060009E3 RID: 2531 RVA: 0x0001D1F1 File Offset: 0x0001B3F1
		public void Dispose()
		{
			UnsafeText.Free(this.m_TextData);
		}

		// Token: 0x04000411 RID: 1041
		[NativeDisableUnsafePtrRestriction]
		public unsafe UnsafeText* m_TextData;
	}
}
