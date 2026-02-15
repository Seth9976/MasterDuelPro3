using System;

namespace System.Linq
{
	// Token: 0x0200004C RID: 76
	internal abstract class EnumerableSorter<TElement>
	{
		// Token: 0x06000251 RID: 593
		internal abstract void ComputeKeys(TElement[] elements, int count);

		// Token: 0x06000252 RID: 594
		internal abstract int CompareAnyKeys(int index1, int index2);

		// Token: 0x06000253 RID: 595 RVA: 0x0000AF50 File Offset: 0x00009150
		private int[] ComputeMap(TElement[] elements, int count)
		{
			this.ComputeKeys(elements, count);
			int[] array = new int[count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = i;
			}
			return array;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000AF80 File Offset: 0x00009180
		internal int[] Sort(TElement[] elements, int count)
		{
			int[] array = this.ComputeMap(elements, count);
			this.QuickSort(array, 0, count - 1);
			return array;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000AFA4 File Offset: 0x000091A4
		internal int[] Sort(TElement[] elements, int count, int minIdx, int maxIdx)
		{
			int[] array = this.ComputeMap(elements, count);
			this.PartialQuickSort(array, 0, count - 1, minIdx, maxIdx);
			return array;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000AFC9 File Offset: 0x000091C9
		internal TElement ElementAt(TElement[] elements, int count, int idx)
		{
			return elements[this.QuickSelect(this.ComputeMap(elements, count), count - 1, idx)];
		}

		// Token: 0x06000257 RID: 599
		protected abstract void QuickSort(int[] map, int left, int right);

		// Token: 0x06000258 RID: 600
		protected abstract void PartialQuickSort(int[] map, int left, int right, int minIdx, int maxIdx);

		// Token: 0x06000259 RID: 601
		protected abstract int QuickSelect(int[] map, int right, int idx);
	}
}
