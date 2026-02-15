using System;

namespace UnityEngine.Bindings
{
	// Token: 0x02000244 RID: 580
	[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
	[VisibleToOtherModules]
	internal readonly ref struct ManagedSpanWrapper
	{
		// Token: 0x060014A5 RID: 5285 RVA: 0x0002BA4D File Offset: 0x00029C4D
		public unsafe ManagedSpanWrapper(void* begin, int length)
		{
			this.begin = begin;
			this.length = length;
		}

		// Token: 0x040007A9 RID: 1961
		public unsafe readonly void* begin;

		// Token: 0x040007AA RID: 1962
		public readonly int length;
	}
}
