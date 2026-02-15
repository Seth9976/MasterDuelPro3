using System;

namespace Unity.Collections
{
	// Token: 0x020000EC RID: 236
	internal struct UnsafeQueueBlockHeader
	{
		// Token: 0x04000459 RID: 1113
		public unsafe UnsafeQueueBlockHeader* m_NextBlock;

		// Token: 0x0400045A RID: 1114
		public int m_NumItems;
	}
}
