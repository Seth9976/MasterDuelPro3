using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000045 RID: 69
	internal abstract class OrderedEnumerable<TElement> : IOrderedEnumerable<TElement>, IEnumerable<TElement>, IEnumerable, IPartition<TElement>, IIListProvider<TElement>
	{
		// Token: 0x0600021F RID: 543 RVA: 0x0000A48F File Offset: 0x0000868F
		private int[] SortedMap(Buffer<TElement> buffer)
		{
			return this.GetEnumerableSorter().Sort(buffer._items, buffer._count);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000A4A8 File Offset: 0x000086A8
		private int[] SortedMap(Buffer<TElement> buffer, int minIdx, int maxIdx)
		{
			return this.GetEnumerableSorter().Sort(buffer._items, buffer._count, minIdx, maxIdx);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A4C3 File Offset: 0x000086C3
		public IEnumerator<TElement> GetEnumerator()
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			if (buffer._count > 0)
			{
				int[] map = this.SortedMap(buffer);
				int num;
				for (int i = 0; i < buffer._count; i = num + 1)
				{
					yield return buffer._items[map[i]];
					num = i;
				}
				map = null;
			}
			yield break;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A4D4 File Offset: 0x000086D4
		public TElement[] ToArray()
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			if (count == 0)
			{
				return buffer._items;
			}
			TElement[] array = new TElement[count];
			int[] array2 = this.SortedMap(buffer);
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = buffer._items[array2[num]];
			}
			return array;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A538 File Offset: 0x00008738
		public List<TElement> ToList()
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			List<TElement> list = new List<TElement>(count);
			if (count > 0)
			{
				int[] array = this.SortedMap(buffer);
				for (int num = 0; num != count; num++)
				{
					list.Add(buffer._items[array[num]]);
				}
			}
			return list;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A594 File Offset: 0x00008794
		public int GetCount(bool onlyIfCheap)
		{
			IIListProvider<TElement> iilistProvider = this._source as IIListProvider<TElement>;
			if (iilistProvider != null)
			{
				return iilistProvider.GetCount(onlyIfCheap);
			}
			if (onlyIfCheap && !(this._source is ICollection<TElement>) && !(this._source is ICollection))
			{
				return -1;
			}
			return this._source.Count<TElement>();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A5E2 File Offset: 0x000087E2
		internal IEnumerator<TElement> GetEnumerator(int minIdx, int maxIdx)
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			if (count > minIdx)
			{
				if (count <= maxIdx)
				{
					maxIdx = count - 1;
				}
				if (minIdx == maxIdx)
				{
					yield return this.GetEnumerableSorter().ElementAt(buffer._items, count, minIdx);
				}
				else
				{
					int[] map = this.SortedMap(buffer, minIdx, maxIdx);
					while (minIdx <= maxIdx)
					{
						yield return buffer._items[map[minIdx]];
						int num = minIdx + 1;
						minIdx = num;
					}
					map = null;
				}
			}
			yield break;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000A600 File Offset: 0x00008800
		internal TElement[] ToArray(int minIdx, int maxIdx)
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			if (count <= minIdx)
			{
				return Array.Empty<TElement>();
			}
			if (count <= maxIdx)
			{
				maxIdx = count - 1;
			}
			if (minIdx == maxIdx)
			{
				return new TElement[] { this.GetEnumerableSorter().ElementAt(buffer._items, count, minIdx) };
			}
			int[] array = this.SortedMap(buffer, minIdx, maxIdx);
			TElement[] array2 = new TElement[maxIdx - minIdx + 1];
			int num = 0;
			while (minIdx <= maxIdx)
			{
				array2[num] = buffer._items[array[minIdx]];
				num++;
				minIdx++;
			}
			return array2;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000A69C File Offset: 0x0000889C
		internal List<TElement> ToList(int minIdx, int maxIdx)
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			if (count <= minIdx)
			{
				return new List<TElement>();
			}
			if (count <= maxIdx)
			{
				maxIdx = count - 1;
			}
			if (minIdx == maxIdx)
			{
				return new List<TElement>(1) { this.GetEnumerableSorter().ElementAt(buffer._items, count, minIdx) };
			}
			int[] array = this.SortedMap(buffer, minIdx, maxIdx);
			List<TElement> list = new List<TElement>(maxIdx - minIdx + 1);
			while (minIdx <= maxIdx)
			{
				list.Add(buffer._items[array[minIdx]]);
				minIdx++;
			}
			return list;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000A72C File Offset: 0x0000892C
		internal int GetCount(int minIdx, int maxIdx, bool onlyIfCheap)
		{
			int count = this.GetCount(onlyIfCheap);
			if (count <= 0)
			{
				return count;
			}
			if (count <= minIdx)
			{
				return 0;
			}
			return ((count <= maxIdx) ? count : (maxIdx + 1)) - minIdx;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000A759 File Offset: 0x00008959
		private EnumerableSorter<TElement> GetEnumerableSorter()
		{
			return this.GetEnumerableSorter(null);
		}

		// Token: 0x0600022A RID: 554
		internal abstract EnumerableSorter<TElement> GetEnumerableSorter(EnumerableSorter<TElement> next);

		// Token: 0x0600022B RID: 555 RVA: 0x0000A762 File Offset: 0x00008962
		private CachingComparer<TElement> GetComparer()
		{
			return this.GetComparer(null);
		}

		// Token: 0x0600022C RID: 556
		internal abstract CachingComparer<TElement> GetComparer(CachingComparer<TElement> childComparer);

		// Token: 0x0600022D RID: 557 RVA: 0x0000A76B File Offset: 0x0000896B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000A773 File Offset: 0x00008973
		IOrderedEnumerable<TElement> IOrderedEnumerable<TElement>.CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
		{
			return new OrderedEnumerable<TElement, TKey>(this._source, keySelector, comparer, descending, this);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000A784 File Offset: 0x00008984
		public IPartition<TElement> Skip(int count)
		{
			return new OrderedPartition<TElement>(this, count, int.MaxValue);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000A792 File Offset: 0x00008992
		public IPartition<TElement> Take(int count)
		{
			return new OrderedPartition<TElement>(this, 0, count - 1);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000A7A0 File Offset: 0x000089A0
		public TElement TryGetElementAt(int index, out bool found)
		{
			if (index == 0)
			{
				return this.TryGetFirst(out found);
			}
			if (index > 0)
			{
				Buffer<TElement> buffer = new Buffer<TElement>(this._source);
				int count = buffer._count;
				if (index < count)
				{
					found = true;
					return this.GetEnumerableSorter().ElementAt(buffer._items, count, index);
				}
			}
			found = false;
			return default(TElement);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000A7F8 File Offset: 0x000089F8
		public TElement TryGetFirst(out bool found)
		{
			CachingComparer<TElement> comparer = this.GetComparer();
			TElement telement;
			using (IEnumerator<TElement> enumerator = this._source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					found = false;
					telement = default(TElement);
					telement = telement;
				}
				else
				{
					TElement telement2 = enumerator.Current;
					comparer.SetElement(telement2);
					while (enumerator.MoveNext())
					{
						TElement telement3 = enumerator.Current;
						if (comparer.Compare(telement3, true) < 0)
						{
							telement2 = telement3;
						}
					}
					found = true;
					telement = telement2;
				}
			}
			return telement;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000A880 File Offset: 0x00008A80
		public TElement TryGetFirst(Func<TElement, bool> predicate, out bool found)
		{
			CachingComparer<TElement> comparer = this.GetComparer();
			TElement telement3;
			using (IEnumerator<TElement> enumerator = this._source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TElement telement = enumerator.Current;
					if (predicate(telement))
					{
						comparer.SetElement(telement);
						while (enumerator.MoveNext())
						{
							TElement telement2 = enumerator.Current;
							if (predicate(telement2) && comparer.Compare(telement2, true) < 0)
							{
								telement = telement2;
							}
						}
						found = true;
						return telement;
					}
				}
				found = false;
				telement3 = default(TElement);
				telement3 = telement3;
			}
			return telement3;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000A91C File Offset: 0x00008B1C
		public TElement TryGetLast(out bool found)
		{
			TElement telement;
			using (IEnumerator<TElement> enumerator = this._source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					found = false;
					telement = default(TElement);
					telement = telement;
				}
				else
				{
					CachingComparer<TElement> comparer = this.GetComparer();
					TElement telement2 = enumerator.Current;
					comparer.SetElement(telement2);
					while (enumerator.MoveNext())
					{
						TElement telement3 = enumerator.Current;
						if (comparer.Compare(telement3, false) >= 0)
						{
							telement2 = telement3;
						}
					}
					found = true;
					telement = telement2;
				}
			}
			return telement;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000A9A4 File Offset: 0x00008BA4
		public TElement TryGetLast(int minIdx, int maxIdx, out bool found)
		{
			Buffer<TElement> buffer = new Buffer<TElement>(this._source);
			int count = buffer._count;
			if (minIdx >= count)
			{
				found = false;
				return default(TElement);
			}
			found = true;
			if (maxIdx >= count - 1)
			{
				return this.Last(buffer);
			}
			return this.GetEnumerableSorter().ElementAt(buffer._items, count, maxIdx);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000A9FC File Offset: 0x00008BFC
		private TElement Last(Buffer<TElement> buffer)
		{
			CachingComparer<TElement> comparer = this.GetComparer();
			TElement[] items = buffer._items;
			int count = buffer._count;
			TElement telement = items[0];
			comparer.SetElement(telement);
			for (int num = 1; num != count; num++)
			{
				TElement telement2 = items[num];
				if (comparer.Compare(telement2, false) >= 0)
				{
					telement = telement2;
				}
			}
			return telement;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000AA58 File Offset: 0x00008C58
		public TElement TryGetLast(Func<TElement, bool> predicate, out bool found)
		{
			CachingComparer<TElement> comparer = this.GetComparer();
			TElement telement3;
			using (IEnumerator<TElement> enumerator = this._source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TElement telement = enumerator.Current;
					if (predicate(telement))
					{
						comparer.SetElement(telement);
						while (enumerator.MoveNext())
						{
							TElement telement2 = enumerator.Current;
							if (predicate(telement2) && comparer.Compare(telement2, false) >= 0)
							{
								telement = telement2;
							}
						}
						found = true;
						return telement;
					}
				}
				found = false;
				telement3 = default(TElement);
				telement3 = telement3;
			}
			return telement3;
		}

		// Token: 0x040000B3 RID: 179
		internal IEnumerable<TElement> _source;
	}
}
