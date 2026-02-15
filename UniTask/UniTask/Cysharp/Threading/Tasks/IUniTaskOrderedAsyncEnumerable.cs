using System;
using System.Collections.Generic;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000033 RID: 51
	public interface IUniTaskOrderedAsyncEnumerable<TElement> : IUniTaskAsyncEnumerable<TElement>
	{
		// Token: 0x0600010E RID: 270
		IUniTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending);

		// Token: 0x0600010F RID: 271
		IUniTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, UniTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending);

		// Token: 0x06000110 RID: 272
		IUniTaskOrderedAsyncEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, CancellationToken, UniTask<TKey>> keySelector, IComparer<TKey> comparer, bool descending);
	}
}
