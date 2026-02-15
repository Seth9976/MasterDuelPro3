using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000051 RID: 81
	internal sealed class OrderedPartition<TElement> : IPartition<TElement>, IIListProvider<TElement>, IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x0600027B RID: 635 RVA: 0x0000B2F7 File Offset: 0x000094F7
		public OrderedPartition(OrderedEnumerable<TElement> source, int minIdxInclusive, int maxIdxInclusive)
		{
			this._source = source;
			this._minIndexInclusive = minIdxInclusive;
			this._maxIndexInclusive = maxIdxInclusive;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000B314 File Offset: 0x00009514
		public IEnumerator<TElement> GetEnumerator()
		{
			return this._source.GetEnumerator(this._minIndexInclusive, this._maxIndexInclusive);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000B32D File Offset: 0x0000952D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000B338 File Offset: 0x00009538
		public IPartition<TElement> Skip(int count)
		{
			int num = this._minIndexInclusive + count;
			if (num <= this._maxIndexInclusive)
			{
				return new OrderedPartition<TElement>(this._source, num, this._maxIndexInclusive);
			}
			return EmptyPartition<TElement>.Instance;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000B374 File Offset: 0x00009574
		public IPartition<TElement> Take(int count)
		{
			int num = this._minIndexInclusive + count - 1;
			if (num >= this._maxIndexInclusive)
			{
				return this;
			}
			return new OrderedPartition<TElement>(this._source, this._minIndexInclusive, num);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000B3AC File Offset: 0x000095AC
		public TElement TryGetElementAt(int index, out bool found)
		{
			if (index <= this._maxIndexInclusive - this._minIndexInclusive)
			{
				return this._source.TryGetElementAt(index + this._minIndexInclusive, out found);
			}
			found = false;
			return default(TElement);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000B3EA File Offset: 0x000095EA
		public TElement TryGetFirst(out bool found)
		{
			return this._source.TryGetElementAt(this._minIndexInclusive, out found);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000B3FE File Offset: 0x000095FE
		public TElement TryGetLast(out bool found)
		{
			return this._source.TryGetLast(this._minIndexInclusive, this._maxIndexInclusive, out found);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000B418 File Offset: 0x00009618
		public TElement[] ToArray()
		{
			return this._source.ToArray(this._minIndexInclusive, this._maxIndexInclusive);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000B431 File Offset: 0x00009631
		public List<TElement> ToList()
		{
			return this._source.ToList(this._minIndexInclusive, this._maxIndexInclusive);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000B44A File Offset: 0x0000964A
		public int GetCount(bool onlyIfCheap)
		{
			return this._source.GetCount(this._minIndexInclusive, this._maxIndexInclusive, onlyIfCheap);
		}

		// Token: 0x040000D0 RID: 208
		private readonly OrderedEnumerable<TElement> _source;

		// Token: 0x040000D1 RID: 209
		private readonly int _minIndexInclusive;

		// Token: 0x040000D2 RID: 210
		private readonly int _maxIndexInclusive;
	}
}
