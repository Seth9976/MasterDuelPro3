using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200004D RID: 77
	internal sealed class EnumerableSorter<TElement, TKey> : EnumerableSorter<TElement>
	{
		// Token: 0x0600025B RID: 603 RVA: 0x0000AFE3 File Offset: 0x000091E3
		internal EnumerableSorter(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending, EnumerableSorter<TElement> next)
		{
			this._keySelector = keySelector;
			this._comparer = comparer;
			this._descending = descending;
			this._next = next;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000B008 File Offset: 0x00009208
		internal override void ComputeKeys(TElement[] elements, int count)
		{
			this._keys = new TKey[count];
			for (int i = 0; i < count; i++)
			{
				this._keys[i] = this._keySelector(elements[i]);
			}
			EnumerableSorter<TElement> next = this._next;
			if (next == null)
			{
				return;
			}
			next.ComputeKeys(elements, count);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000B060 File Offset: 0x00009260
		internal override int CompareAnyKeys(int index1, int index2)
		{
			int num = this._comparer.Compare(this._keys[index1], this._keys[index2]);
			if (num == 0)
			{
				if (this._next == null)
				{
					return index1 - index2;
				}
				return this._next.CompareAnyKeys(index1, index2);
			}
			else
			{
				if (this._descending == num > 0)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000B0BD File Offset: 0x000092BD
		private int CompareKeys(int index1, int index2)
		{
			if (index1 != index2)
			{
				return this.CompareAnyKeys(index1, index2);
			}
			return 0;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000B0CD File Offset: 0x000092CD
		protected override void QuickSort(int[] keys, int lo, int hi)
		{
			Array.Sort<int>(keys, lo, hi - lo + 1, Comparer<int>.Create(new Comparison<int>(this.CompareAnyKeys)));
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000B0F0 File Offset: 0x000092F0
		protected override void PartialQuickSort(int[] map, int left, int right, int minIdx, int maxIdx)
		{
			do
			{
				int num = left;
				int num2 = right;
				int num3 = map[num + (num2 - num >> 1)];
				do
				{
					if (num < map.Length)
					{
						if (this.CompareKeys(num3, map[num]) > 0)
						{
							num++;
							continue;
						}
					}
					while (num2 >= 0 && this.CompareKeys(num3, map[num2]) < 0)
					{
						num2--;
					}
					if (num > num2)
					{
						break;
					}
					if (num < num2)
					{
						int num4 = map[num];
						map[num] = map[num2];
						map[num2] = num4;
					}
					num++;
					num2--;
				}
				while (num <= num2);
				if (minIdx >= num)
				{
					left = num + 1;
				}
				else if (maxIdx <= num2)
				{
					right = num2 - 1;
				}
				if (num2 - left <= right - num)
				{
					if (left < num2)
					{
						this.PartialQuickSort(map, left, num2, minIdx, maxIdx);
					}
					left = num;
				}
				else
				{
					if (num < right)
					{
						this.PartialQuickSort(map, num, right, minIdx, maxIdx);
					}
					right = num2;
				}
			}
			while (left < right);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000B1AC File Offset: 0x000093AC
		protected override int QuickSelect(int[] map, int right, int idx)
		{
			int num = 0;
			do
			{
				int num2 = num;
				int num3 = right;
				int num4 = map[num2 + (num3 - num2 >> 1)];
				do
				{
					if (num2 < map.Length)
					{
						if (this.CompareKeys(num4, map[num2]) > 0)
						{
							num2++;
							continue;
						}
					}
					while (num3 >= 0 && this.CompareKeys(num4, map[num3]) < 0)
					{
						num3--;
					}
					if (num2 > num3)
					{
						break;
					}
					if (num2 < num3)
					{
						int num5 = map[num2];
						map[num2] = map[num3];
						map[num3] = num5;
					}
					num2++;
					num3--;
				}
				while (num2 <= num3);
				if (num2 <= idx)
				{
					num = num2 + 1;
				}
				else
				{
					right = num3 - 1;
				}
				if (num3 - num <= right - num2)
				{
					if (num < num3)
					{
						right = num3;
					}
					num = num2;
				}
				else
				{
					if (num2 < right)
					{
						num = num2;
					}
					right = num3;
				}
			}
			while (num < right);
			return map[idx];
		}

		// Token: 0x040000CA RID: 202
		private readonly Func<TElement, TKey> _keySelector;

		// Token: 0x040000CB RID: 203
		private readonly IComparer<TKey> _comparer;

		// Token: 0x040000CC RID: 204
		private readonly bool _descending;

		// Token: 0x040000CD RID: 205
		private readonly EnumerableSorter<TElement> _next;

		// Token: 0x040000CE RID: 206
		private TKey[] _keys;
	}
}
