using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200004E RID: 78
	internal interface IIListProvider<TElement> : IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x06000262 RID: 610
		TElement[] ToArray();

		// Token: 0x06000263 RID: 611
		List<TElement> ToList();

		// Token: 0x06000264 RID: 612
		int GetCount(bool onlyIfCheap);
	}
}
