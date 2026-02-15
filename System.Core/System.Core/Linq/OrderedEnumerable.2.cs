using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000048 RID: 72
	internal sealed class OrderedEnumerable<TElement, TKey> : OrderedEnumerable<TElement>
	{
		// Token: 0x06000245 RID: 581 RVA: 0x0000AD30 File Offset: 0x00008F30
		internal OrderedEnumerable(IEnumerable<TElement> source, Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending, OrderedEnumerable<TElement> parent)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			this._source = source;
			this._parent = parent;
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			this._keySelector = keySelector;
			this._comparer = comparer ?? Comparer<TKey>.Default;
			this._descending = descending;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000AD90 File Offset: 0x00008F90
		internal override EnumerableSorter<TElement> GetEnumerableSorter(EnumerableSorter<TElement> next)
		{
			EnumerableSorter<TElement> enumerableSorter = new EnumerableSorter<TElement, TKey>(this._keySelector, this._comparer, this._descending, next);
			if (this._parent != null)
			{
				enumerableSorter = this._parent.GetEnumerableSorter(enumerableSorter);
			}
			return enumerableSorter;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000ADCC File Offset: 0x00008FCC
		internal override CachingComparer<TElement> GetComparer(CachingComparer<TElement> childComparer)
		{
			CachingComparer<TElement> cachingComparer = ((childComparer == null) ? new CachingComparer<TElement, TKey>(this._keySelector, this._comparer, this._descending) : new CachingComparerWithChild<TElement, TKey>(this._keySelector, this._comparer, this._descending, childComparer));
			if (this._parent == null)
			{
				return cachingComparer;
			}
			return this._parent.GetComparer(cachingComparer);
		}

		// Token: 0x040000C1 RID: 193
		private readonly OrderedEnumerable<TElement> _parent;

		// Token: 0x040000C2 RID: 194
		private readonly Func<TElement, TKey> _keySelector;

		// Token: 0x040000C3 RID: 195
		private readonly IComparer<TKey> _comparer;

		// Token: 0x040000C4 RID: 196
		private readonly bool _descending;
	}
}
