using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000041 RID: 65
	internal sealed class GroupedEnumerable<TSource, TKey> : IIListProvider<IGrouping<TKey, TSource>>, IEnumerable<IGrouping<TKey, TSource>>, IEnumerable
	{
		// Token: 0x06000207 RID: 519 RVA: 0x0000A0AF File Offset: 0x000082AF
		public GroupedEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			this._source = source;
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			this._keySelector = keySelector;
			this._comparer = comparer;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000A0EA File Offset: 0x000082EA
		public IEnumerator<IGrouping<TKey, TSource>> GetEnumerator()
		{
			return Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer).GetEnumerator();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000A108 File Offset: 0x00008308
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000A110 File Offset: 0x00008310
		public IGrouping<TKey, TSource>[] ToArray()
		{
			return ((IIListProvider<IGrouping<TKey, TSource>>)Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer)).ToArray();
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000A12E File Offset: 0x0000832E
		public List<IGrouping<TKey, TSource>> ToList()
		{
			return ((IIListProvider<IGrouping<TKey, TSource>>)Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer)).ToList();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000A14C File Offset: 0x0000834C
		public int GetCount(bool onlyIfCheap)
		{
			if (!onlyIfCheap)
			{
				return Lookup<TKey, TSource>.Create(this._source, this._keySelector, this._comparer).Count;
			}
			return -1;
		}

		// Token: 0x040000A8 RID: 168
		private readonly IEnumerable<TSource> _source;

		// Token: 0x040000A9 RID: 169
		private readonly Func<TSource, TKey> _keySelector;

		// Token: 0x040000AA RID: 170
		private readonly IEqualityComparer<TKey> _comparer;
	}
}
