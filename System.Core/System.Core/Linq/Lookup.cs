using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace System.Linq
{
	/// <summary>Represents a collection of keys each mapped to one or more values.</summary>
	/// <typeparam name="TKey">The type of the keys in the <see cref="T:System.Linq.Lookup`2" />.</typeparam>
	/// <typeparam name="TElement">The type of the elements of each <see cref="T:System.Collections.Generic.IEnumerable`1" /> value in the <see cref="T:System.Linq.Lookup`2" />.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000042 RID: 66
	[DefaultMember("Item")]
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SystemLinq_LookupDebugView<, >))]
	public class Lookup<TKey, TElement> : IEnumerable<IGrouping<TKey, TElement>>, IEnumerable, IIListProvider<IGrouping<TKey, TElement>>
	{
		// Token: 0x0600020D RID: 525 RVA: 0x0000A170 File Offset: 0x00008370
		internal static Lookup<TKey, TElement> Create(IEnumerable<TElement> source, Func<TElement, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Lookup<TKey, TElement> lookup = new Lookup<TKey, TElement>(comparer);
			foreach (TElement telement in source)
			{
				lookup.GetGrouping(keySelector(telement), true).Add(telement);
			}
			return lookup;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000A1D0 File Offset: 0x000083D0
		private Lookup(IEqualityComparer<TKey> comparer)
		{
			this._comparer = comparer ?? EqualityComparer<TKey>.Default;
			this._groupings = new Grouping<TKey, TElement>[7];
		}

		/// <summary>Gets the number of key/value collection pairs in the <see cref="T:System.Linq.Lookup`2" />.</summary>
		/// <returns>The number of key/value collection pairs in the <see cref="T:System.Linq.Lookup`2" />.</returns>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000A1F4 File Offset: 0x000083F4
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		/// <summary>Returns a generic enumerator that iterates through the <see cref="T:System.Linq.Lookup`2" />.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Linq.Lookup`2" />.</returns>
		// Token: 0x06000210 RID: 528 RVA: 0x0000A1FC File Offset: 0x000083FC
		public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
		{
			Grouping<TKey, TElement> g = this._lastGrouping;
			if (g != null)
			{
				do
				{
					g = g._next;
					yield return g;
				}
				while (g != this._lastGrouping);
			}
			yield break;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000A20C File Offset: 0x0000840C
		IGrouping<TKey, TElement>[] IIListProvider<IGrouping<TKey, TElement>>.ToArray()
		{
			IGrouping<TKey, TElement>[] array = new IGrouping<TKey, TElement>[this._count];
			int num = 0;
			Grouping<TKey, TElement> grouping = this._lastGrouping;
			if (grouping != null)
			{
				do
				{
					grouping = grouping._next;
					array[num] = grouping;
					num++;
				}
				while (grouping != this._lastGrouping);
			}
			return array;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A24C File Offset: 0x0000844C
		List<IGrouping<TKey, TElement>> IIListProvider<IGrouping<TKey, TElement>>.ToList()
		{
			List<IGrouping<TKey, TElement>> list = new List<IGrouping<TKey, TElement>>(this._count);
			Grouping<TKey, TElement> grouping = this._lastGrouping;
			if (grouping != null)
			{
				do
				{
					grouping = grouping._next;
					list.Add(grouping);
				}
				while (grouping != this._lastGrouping);
			}
			return list;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A1F4 File Offset: 0x000083F4
		int IIListProvider<IGrouping<TKey, TElement>>.GetCount(bool onlyIfCheap)
		{
			return this._count;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Linq.Lookup`2" />. This class cannot be inherited.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Linq.Lookup`2" />.</returns>
		// Token: 0x06000214 RID: 532 RVA: 0x0000A287 File Offset: 0x00008487
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000A28F File Offset: 0x0000848F
		private int InternalGetHashCode(TKey key)
		{
			if (key != null)
			{
				return this._comparer.GetHashCode(key) & int.MaxValue;
			}
			return 0;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000A2B0 File Offset: 0x000084B0
		internal Grouping<TKey, TElement> GetGrouping(TKey key, bool create)
		{
			int num = this.InternalGetHashCode(key);
			for (Grouping<TKey, TElement> grouping = this._groupings[num % this._groupings.Length]; grouping != null; grouping = grouping._hashNext)
			{
				if (grouping._hashCode == num && this._comparer.Equals(grouping._key, key))
				{
					return grouping;
				}
			}
			if (create)
			{
				if (this._count == this._groupings.Length)
				{
					this.Resize();
				}
				int num2 = num % this._groupings.Length;
				Grouping<TKey, TElement> grouping2 = new Grouping<TKey, TElement>();
				grouping2._key = key;
				grouping2._hashCode = num;
				grouping2._elements = new TElement[1];
				grouping2._hashNext = this._groupings[num2];
				this._groupings[num2] = grouping2;
				if (this._lastGrouping == null)
				{
					grouping2._next = grouping2;
				}
				else
				{
					grouping2._next = this._lastGrouping._next;
					this._lastGrouping._next = grouping2;
				}
				this._lastGrouping = grouping2;
				this._count++;
				return grouping2;
			}
			return null;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A3A8 File Offset: 0x000085A8
		private void Resize()
		{
			int num = checked(this._count * 2 + 1);
			Grouping<TKey, TElement>[] array = new Grouping<TKey, TElement>[num];
			Grouping<TKey, TElement> grouping = this._lastGrouping;
			do
			{
				grouping = grouping._next;
				int num2 = grouping._hashCode % num;
				grouping._hashNext = array[num2];
				array[num2] = grouping;
			}
			while (grouping != this._lastGrouping);
			this._groupings = array;
		}

		// Token: 0x040000AB RID: 171
		private readonly IEqualityComparer<TKey> _comparer;

		// Token: 0x040000AC RID: 172
		private Grouping<TKey, TElement>[] _groupings;

		// Token: 0x040000AD RID: 173
		private Grouping<TKey, TElement> _lastGrouping;

		// Token: 0x040000AE RID: 174
		private int _count;
	}
}
