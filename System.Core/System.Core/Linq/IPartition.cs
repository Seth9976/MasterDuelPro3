using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200004F RID: 79
	internal interface IPartition<TElement> : IIListProvider<TElement>, IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x06000265 RID: 613
		IPartition<TElement> Skip(int count);

		// Token: 0x06000266 RID: 614
		IPartition<TElement> Take(int count);

		// Token: 0x06000267 RID: 615
		TElement TryGetElementAt(int index, out bool found);

		// Token: 0x06000268 RID: 616
		TElement TryGetFirst(out bool found);

		// Token: 0x06000269 RID: 617
		TElement TryGetLast(out bool found);
	}
}
