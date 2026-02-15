using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000042 RID: 66
	public static class DynamicArrayExtensions
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x00007D68 File Offset: 0x00005F68
		private unsafe static int Partition<T>(Span<T> data, int left, int right) where T : IComparable<T>, new()
		{
			T pivot = *data[left];
			left--;
			right++;
			for (;;)
			{
				T lvalue = default(T);
				int c;
				do
				{
					left++;
					lvalue = *data[left];
					c = lvalue.CompareTo(pivot);
				}
				while (c < 0);
				T rvalue = default(T);
				do
				{
					right--;
					rvalue = *data[right];
					c = rvalue.CompareTo(pivot);
				}
				while (c > 0);
				if (left >= right)
				{
					break;
				}
				*data[right] = lvalue;
				*data[left] = rvalue;
			}
			return right;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00007E10 File Offset: 0x00006010
		private static void QuickSort<T>(Span<T> data, int left, int right) where T : IComparable<T>, new()
		{
			if (left < right)
			{
				int pivot = DynamicArrayExtensions.Partition<T>(data, left, right);
				if (pivot >= 1)
				{
					DynamicArrayExtensions.QuickSort<T>(data, left, pivot);
				}
				if (pivot + 1 < right)
				{
					DynamicArrayExtensions.QuickSort<T>(data, pivot + 1, right);
				}
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00007E48 File Offset: 0x00006048
		private unsafe static int Partition<T>(Span<T> data, int left, int right, DynamicArray<T>.SortComparer comparer) where T : new()
		{
			T pivot = *data[left];
			left--;
			right++;
			for (;;)
			{
				T lvalue = default(T);
				int c;
				do
				{
					left++;
					lvalue = *data[left];
					c = comparer(lvalue, pivot);
				}
				while (c < 0);
				T rvalue = default(T);
				do
				{
					right--;
					rvalue = *data[right];
					c = comparer(rvalue, pivot);
				}
				while (c > 0);
				if (left >= right)
				{
					break;
				}
				*data[right] = lvalue;
				*data[left] = rvalue;
			}
			return right;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00007EE4 File Offset: 0x000060E4
		private static void QuickSort<T>(Span<T> data, int left, int right, DynamicArray<T>.SortComparer comparer) where T : new()
		{
			if (left < right)
			{
				int pivot = DynamicArrayExtensions.Partition<T>(data, left, right, comparer);
				if (pivot >= 1)
				{
					DynamicArrayExtensions.QuickSort<T>(data, left, pivot, comparer);
				}
				if (pivot + 1 < right)
				{
					DynamicArrayExtensions.QuickSort<T>(data, pivot + 1, right, comparer);
				}
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00007F1D File Offset: 0x0000611D
		public static void QuickSort<T>(this DynamicArray<T> array) where T : IComparable<T>, new()
		{
			DynamicArrayExtensions.QuickSort<T>(array, 0, array.size - 1);
			array.BumpVersion();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00007F39 File Offset: 0x00006139
		public static void QuickSort<T>(this DynamicArray<T> array, DynamicArray<T>.SortComparer comparer) where T : new()
		{
			DynamicArrayExtensions.QuickSort<T>(array, 0, array.size - 1, comparer);
			array.BumpVersion();
		}
	}
}
