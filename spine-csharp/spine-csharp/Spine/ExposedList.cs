using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Spine
{
	// Token: 0x0200005F RID: 95
	[DebuggerDisplay("Count={Count}")]
	public class ExposedList<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x0000CC7C File Offset: 0x0000AE7C
		public ExposedList()
		{
			this.Items = ExposedList<T>.EmptyArray;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000CC90 File Offset: 0x0000AE90
		public ExposedList(IEnumerable<T> collection)
		{
			this.CheckCollection(collection);
			ICollection<T> c = collection as ICollection<T>;
			if (c == null)
			{
				this.Items = ExposedList<T>.EmptyArray;
				this.AddEnumerable(collection);
				return;
			}
			this.Items = new T[c.Count];
			this.AddCollection(c);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000CCDF File Offset: 0x0000AEDF
		public ExposedList(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			this.Items = new T[capacity];
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000CD02 File Offset: 0x0000AF02
		internal ExposedList(T[] data, int size)
		{
			this.Items = data;
			this.Count = size;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000CD18 File Offset: 0x0000AF18
		public void Add(T item)
		{
			if (this.Count == this.Items.Length)
			{
				this.GrowIfNeeded(1);
			}
			T[] items = this.Items;
			int count = this.Count;
			this.Count = count + 1;
			items[count] = item;
			this.version++;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public void GrowIfNeeded(int addedCount)
		{
			int minimumSize = this.Count + addedCount;
			if (minimumSize > this.Items.Length)
			{
				this.Capacity = Math.Max(Math.Max(this.Capacity * 2, 4), minimumSize);
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
		public ExposedList<T> Resize(int newSize)
		{
			int itemsLength = this.Items.Length;
			T[] oldItems = this.Items;
			if (newSize > itemsLength)
			{
				Array.Resize<T>(ref this.Items, newSize);
			}
			else if (newSize < itemsLength)
			{
				for (int i = newSize; i < itemsLength; i++)
				{
					oldItems[i] = default(T);
				}
			}
			this.Count = newSize;
			return this;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000CDFC File Offset: 0x0000AFFC
		public void EnsureCapacity(int min)
		{
			if (this.Items.Length < min)
			{
				int newCapacity = ((this.Items.Length == 0) ? 4 : (this.Items.Length * 2));
				if (newCapacity < min)
				{
					newCapacity = min;
				}
				this.Capacity = newCapacity;
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000CE38 File Offset: 0x0000B038
		private void CheckRange(int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index + count > this.Count)
			{
				throw new ArgumentException("index and count exceed length of list");
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000CE70 File Offset: 0x0000B070
		private void AddCollection(ICollection<T> collection)
		{
			int collectionCount = collection.Count;
			if (collectionCount == 0)
			{
				return;
			}
			this.GrowIfNeeded(collectionCount);
			collection.CopyTo(this.Items, this.Count);
			this.Count += collectionCount;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000CEB0 File Offset: 0x0000B0B0
		private void AddEnumerable(IEnumerable<T> enumerable)
		{
			foreach (T t in enumerable)
			{
				this.Add(t);
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000CEF8 File Offset: 0x0000B0F8
		public void AddRange(ExposedList<T> list)
		{
			this.CheckCollection(list);
			int collectionCount = list.Count;
			if (collectionCount == 0)
			{
				return;
			}
			this.GrowIfNeeded(collectionCount);
			list.CopyTo(this.Items, this.Count);
			this.Count += collectionCount;
			this.version++;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000CF4C File Offset: 0x0000B14C
		public void AddRange(IEnumerable<T> collection)
		{
			this.CheckCollection(collection);
			ICollection<T> c = collection as ICollection<T>;
			if (c != null)
			{
				this.AddCollection(c);
			}
			else
			{
				this.AddEnumerable(collection);
			}
			this.version++;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000CF88 File Offset: 0x0000B188
		public int BinarySearch(T item)
		{
			return Array.BinarySearch<T>(this.Items, 0, this.Count, item);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000CF9D File Offset: 0x0000B19D
		public int BinarySearch(T item, IComparer<T> comparer)
		{
			return Array.BinarySearch<T>(this.Items, 0, this.Count, item, comparer);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000CFB3 File Offset: 0x0000B1B3
		public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
		{
			this.CheckRange(index, count);
			return Array.BinarySearch<T>(this.Items, index, count, item, comparer);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000CFCD File Offset: 0x0000B1CD
		public void Clear(bool clearArray = true)
		{
			if (clearArray)
			{
				Array.Clear(this.Items, 0, this.Items.Length);
			}
			this.Count = 0;
			this.version++;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000CFFB File Offset: 0x0000B1FB
		public bool Contains(T item)
		{
			return Array.IndexOf<T>(this.Items, item, 0, this.Count) != -1;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000D018 File Offset: 0x0000B218
		public ExposedList<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
		{
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			ExposedList<TOutput> u = new ExposedList<TOutput>(this.Count);
			u.Count = this.Count;
			T[] items = this.Items;
			TOutput[] uItems = u.Items;
			for (int i = 0; i < this.Count; i++)
			{
				uItems[i] = converter(items[i]);
			}
			return u;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000D07F File Offset: 0x0000B27F
		public void CopyTo(T[] array)
		{
			Array.Copy(this.Items, 0, array, 0, this.Count);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000D095 File Offset: 0x0000B295
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this.Items, 0, array, arrayIndex, this.Count);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000D0AB File Offset: 0x0000B2AB
		public void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			this.CheckRange(index, count);
			Array.Copy(this.Items, index, array, arrayIndex, count);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000D0C6 File Offset: 0x0000B2C6
		public bool Exists(Predicate<T> match)
		{
			ExposedList<T>.CheckMatch(match);
			return this.GetIndex(0, this.Count, match) != -1;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000D0E4 File Offset: 0x0000B2E4
		public T Find(Predicate<T> match)
		{
			ExposedList<T>.CheckMatch(match);
			int i = this.GetIndex(0, this.Count, match);
			if (i == -1)
			{
				return default(T);
			}
			return this.Items[i];
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000D120 File Offset: 0x0000B320
		private static void CheckMatch(Predicate<T> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000D130 File Offset: 0x0000B330
		public ExposedList<T> FindAll(Predicate<T> match)
		{
			ExposedList<T>.CheckMatch(match);
			return this.FindAllList(match);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000D140 File Offset: 0x0000B340
		private ExposedList<T> FindAllList(Predicate<T> match)
		{
			ExposedList<T> results = new ExposedList<T>();
			for (int i = 0; i < this.Count; i++)
			{
				if (match(this.Items[i]))
				{
					results.Add(this.Items[i]);
				}
			}
			return results;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000D18B File Offset: 0x0000B38B
		public int FindIndex(Predicate<T> match)
		{
			ExposedList<T>.CheckMatch(match);
			return this.GetIndex(0, this.Count, match);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000D1A1 File Offset: 0x0000B3A1
		public int FindIndex(int startIndex, Predicate<T> match)
		{
			ExposedList<T>.CheckMatch(match);
			this.CheckIndex(startIndex);
			return this.GetIndex(startIndex, this.Count - startIndex, match);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000D1C0 File Offset: 0x0000B3C0
		public int FindIndex(int startIndex, int count, Predicate<T> match)
		{
			ExposedList<T>.CheckMatch(match);
			this.CheckRange(startIndex, count);
			return this.GetIndex(startIndex, count, match);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000D1DC File Offset: 0x0000B3DC
		private int GetIndex(int startIndex, int count, Predicate<T> match)
		{
			int end = startIndex + count;
			for (int i = startIndex; i < end; i++)
			{
				if (match(this.Items[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000D210 File Offset: 0x0000B410
		public T FindLast(Predicate<T> match)
		{
			ExposedList<T>.CheckMatch(match);
			int i = this.GetLastIndex(0, this.Count, match);
			if (i != -1)
			{
				return this.Items[i];
			}
			return default(T);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000D24C File Offset: 0x0000B44C
		public int FindLastIndex(Predicate<T> match)
		{
			ExposedList<T>.CheckMatch(match);
			return this.GetLastIndex(0, this.Count, match);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000D262 File Offset: 0x0000B462
		public int FindLastIndex(int startIndex, Predicate<T> match)
		{
			ExposedList<T>.CheckMatch(match);
			this.CheckIndex(startIndex);
			return this.GetLastIndex(0, startIndex + 1, match);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000D27C File Offset: 0x0000B47C
		public int FindLastIndex(int startIndex, int count, Predicate<T> match)
		{
			ExposedList<T>.CheckMatch(match);
			int start = startIndex - count + 1;
			this.CheckRange(start, count);
			return this.GetLastIndex(start, count, match);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000D2A8 File Offset: 0x0000B4A8
		private int GetLastIndex(int startIndex, int count, Predicate<T> match)
		{
			int i = startIndex + count;
			while (i != startIndex)
			{
				if (match(this.Items[--i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000D2DC File Offset: 0x0000B4DC
		public void ForEach(Action<T> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			for (int i = 0; i < this.Count; i++)
			{
				action(this.Items[i]);
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000D31A File Offset: 0x0000B51A
		public ExposedList<T>.Enumerator GetEnumerator()
		{
			return new ExposedList<T>.Enumerator(this);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000D324 File Offset: 0x0000B524
		public ExposedList<T> GetRange(int index, int count)
		{
			this.CheckRange(index, count);
			T[] tmpArray = new T[count];
			Array.Copy(this.Items, index, tmpArray, 0, count);
			return new ExposedList<T>(tmpArray, count);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000D356 File Offset: 0x0000B556
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this.Items, item, 0, this.Count);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000D36B File Offset: 0x0000B56B
		public int IndexOf(T item, int index)
		{
			this.CheckIndex(index);
			return Array.IndexOf<T>(this.Items, item, index, this.Count - index);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000D38C File Offset: 0x0000B58C
		public int IndexOf(T item, int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index + count > this.Count)
			{
				throw new ArgumentOutOfRangeException("index and count exceed length of list");
			}
			return Array.IndexOf<T>(this.Items, item, index, count);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000D3DC File Offset: 0x0000B5DC
		private void Shift(int start, int delta)
		{
			if (delta < 0)
			{
				start -= delta;
			}
			if (start < this.Count)
			{
				Array.Copy(this.Items, start, this.Items, start + delta, this.Count - start);
			}
			this.Count += delta;
			if (delta < 0)
			{
				Array.Clear(this.Items, this.Count, -delta);
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000D43D File Offset: 0x0000B63D
		private void CheckIndex(int index)
		{
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000D458 File Offset: 0x0000B658
		public void Insert(int index, T item)
		{
			this.CheckIndex(index);
			if (this.Count == this.Items.Length)
			{
				this.GrowIfNeeded(1);
			}
			this.Shift(index, 1);
			this.Items[index] = item;
			this.version++;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000D4A6 File Offset: 0x0000B6A6
		private void CheckCollection(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
		public void InsertRange(int index, IEnumerable<T> collection)
		{
			this.CheckCollection(collection);
			this.CheckIndex(index);
			if (collection == this)
			{
				T[] buffer = new T[this.Count];
				this.CopyTo(buffer, 0);
				this.GrowIfNeeded(this.Count);
				this.Shift(index, buffer.Length);
				Array.Copy(buffer, 0, this.Items, index, buffer.Length);
			}
			else
			{
				ICollection<T> c = collection as ICollection<T>;
				if (c != null)
				{
					this.InsertCollection(index, c);
				}
				else
				{
					this.InsertEnumeration(index, collection);
				}
			}
			this.version++;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000D540 File Offset: 0x0000B740
		private void InsertCollection(int index, ICollection<T> collection)
		{
			int collectionCount = collection.Count;
			this.GrowIfNeeded(collectionCount);
			this.Shift(index, collectionCount);
			collection.CopyTo(this.Items, index);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000D570 File Offset: 0x0000B770
		private void InsertEnumeration(int index, IEnumerable<T> enumerable)
		{
			foreach (T t in enumerable)
			{
				this.Insert(index++, t);
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
		public int LastIndexOf(T item)
		{
			return Array.LastIndexOf<T>(this.Items, item, this.Count - 1, this.Count);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		public int LastIndexOf(T item, int index)
		{
			this.CheckIndex(index);
			return Array.LastIndexOf<T>(this.Items, item, index, index + 1);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000D5F8 File Offset: 0x0000B7F8
		public int LastIndexOf(T item, int index, int count)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "index is negative");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "count is negative");
			}
			if (index - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "count is too large");
			}
			return Array.LastIndexOf<T>(this.Items, item, index, count);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000D668 File Offset: 0x0000B868
		public bool Remove(T item)
		{
			int loc = this.IndexOf(item);
			if (loc != -1)
			{
				this.RemoveAt(loc);
			}
			return loc != -1;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000D690 File Offset: 0x0000B890
		public int RemoveAll(Predicate<T> match)
		{
			ExposedList<T>.CheckMatch(match);
			int i = 0;
			while (i < this.Count && !match(this.Items[i]))
			{
				i++;
			}
			if (i == this.Count)
			{
				return 0;
			}
			this.version++;
			int j;
			for (j = i + 1; j < this.Count; j++)
			{
				if (!match(this.Items[j]))
				{
					this.Items[i++] = this.Items[j];
				}
			}
			if (j - i > 0)
			{
				Array.Clear(this.Items, i, j - i);
			}
			this.Count = i;
			return j - i;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000D748 File Offset: 0x0000B948
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.Shift(index, -1);
			Array.Clear(this.Items, this.Count, 1);
			this.version++;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000D798 File Offset: 0x0000B998
		public T Pop()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException("List is empty. Nothing to pop.");
			}
			int i = this.Count - 1;
			T t = this.Items[i];
			this.Items[i] = default(T);
			this.Count--;
			this.version++;
			return t;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000D7FE File Offset: 0x0000B9FE
		public void RemoveRange(int index, int count)
		{
			this.CheckRange(index, count);
			if (count > 0)
			{
				this.Shift(index, -count);
				Array.Clear(this.Items, this.Count, count);
				this.version++;
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000D835 File Offset: 0x0000BA35
		public void Reverse()
		{
			Array.Reverse<T>(this.Items, 0, this.Count);
			this.version++;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000D857 File Offset: 0x0000BA57
		public void Reverse(int index, int count)
		{
			this.CheckRange(index, count);
			Array.Reverse<T>(this.Items, index, count);
			this.version++;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000D87C File Offset: 0x0000BA7C
		public void Sort()
		{
			Array.Sort<T>(this.Items, 0, this.Count, Comparer<T>.Default);
			this.version++;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000D8A3 File Offset: 0x0000BAA3
		public void Sort(IComparer<T> comparer)
		{
			Array.Sort<T>(this.Items, 0, this.Count, comparer);
			this.version++;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000D8C6 File Offset: 0x0000BAC6
		public void Sort(Comparison<T> comparison)
		{
			Array.Sort<T>(this.Items, comparison);
			this.version++;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000D8E2 File Offset: 0x0000BAE2
		public void Sort(int index, int count, IComparer<T> comparer)
		{
			this.CheckRange(index, count);
			Array.Sort<T>(this.Items, index, count, comparer);
			this.version++;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000D908 File Offset: 0x0000BB08
		public T[] ToArray()
		{
			T[] t = new T[this.Count];
			Array.Copy(this.Items, t, this.Count);
			return t;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000D934 File Offset: 0x0000BB34
		public void TrimExcess()
		{
			this.Capacity = this.Count;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000D944 File Offset: 0x0000BB44
		public bool TrueForAll(Predicate<T> match)
		{
			ExposedList<T>.CheckMatch(match);
			for (int i = 0; i < this.Count; i++)
			{
				if (!match(this.Items[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000D97F File Offset: 0x0000BB7F
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000D989 File Offset: 0x0000BB89
		public int Capacity
		{
			get
			{
				return this.Items.Length;
			}
			set
			{
				if (value < this.Count)
				{
					throw new ArgumentOutOfRangeException();
				}
				Array.Resize<T>(ref this.Items, value);
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000D9A6 File Offset: 0x0000BBA6
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000D9A6 File Offset: 0x0000BBA6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040001BF RID: 447
		public T[] Items;

		// Token: 0x040001C0 RID: 448
		public int Count;

		// Token: 0x040001C1 RID: 449
		private const int DefaultCapacity = 4;

		// Token: 0x040001C2 RID: 450
		private static readonly T[] EmptyArray = new T[0];

		// Token: 0x040001C3 RID: 451
		private int version;

		// Token: 0x02000060 RID: 96
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000320 RID: 800 RVA: 0x0000D9C0 File Offset: 0x0000BBC0
			internal Enumerator(ExposedList<T> l)
			{
				this = default(ExposedList<T>.Enumerator);
				this.l = l;
				this.ver = l.version;
			}

			// Token: 0x06000321 RID: 801 RVA: 0x0000D9DC File Offset: 0x0000BBDC
			public void Dispose()
			{
				this.l = null;
			}

			// Token: 0x06000322 RID: 802 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
			private void VerifyState()
			{
				if (this.l == null)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				if (this.ver != this.l.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
			}

			// Token: 0x06000323 RID: 803 RVA: 0x0000DA38 File Offset: 0x0000BC38
			public bool MoveNext()
			{
				this.VerifyState();
				if (this.next < 0)
				{
					return false;
				}
				if (this.next < this.l.Count)
				{
					T[] items = this.l.Items;
					int num = this.next;
					this.next = num + 1;
					this.current = items[num];
					return true;
				}
				this.next = -1;
				return false;
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x06000324 RID: 804 RVA: 0x0000DA9A File Offset: 0x0000BC9A
			public T Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x06000325 RID: 805 RVA: 0x0000DAA2 File Offset: 0x0000BCA2
			void IEnumerator.Reset()
			{
				this.VerifyState();
				this.next = 0;
			}

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x06000326 RID: 806 RVA: 0x0000DAB1 File Offset: 0x0000BCB1
			object IEnumerator.Current
			{
				get
				{
					this.VerifyState();
					if (this.next <= 0)
					{
						throw new InvalidOperationException();
					}
					return this.current;
				}
			}

			// Token: 0x040001C4 RID: 452
			private ExposedList<T> l;

			// Token: 0x040001C5 RID: 453
			private int next;

			// Token: 0x040001C6 RID: 454
			private int ver;

			// Token: 0x040001C7 RID: 455
			private T current;
		}
	}
}
