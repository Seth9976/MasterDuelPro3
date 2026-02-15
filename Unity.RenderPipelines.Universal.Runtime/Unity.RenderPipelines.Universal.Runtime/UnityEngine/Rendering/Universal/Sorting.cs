using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000E1 RID: 225
	internal struct Sorting
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x00015AE0 File Offset: 0x00013CE0
		public static void QuickSort<T>(T[] data, Func<T, T, int> compare)
		{
			using (new ProfilingScope(Sorting.s_QuickSortSampler))
			{
				Sorting.QuickSort<T>(data, 0, data.Length - 1, compare);
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00015B28 File Offset: 0x00013D28
		public static void QuickSort<T>(T[] data, int start, int end, Func<T, T, int> compare)
		{
			int diff = end - start;
			if (diff < 1)
			{
				return;
			}
			if (diff < 8)
			{
				Sorting.InsertionSort<T>(data, start, end, compare);
				return;
			}
			if (start < end)
			{
				int pivot = Sorting.Partition<T>(data, start, end, compare);
				if (pivot >= 1)
				{
					Sorting.QuickSort<T>(data, start, pivot, compare);
				}
				if (pivot + 1 < end)
				{
					Sorting.QuickSort<T>(data, pivot + 1, end, compare);
				}
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00015B78 File Offset: 0x00013D78
		private static T Median3Pivot<T>(T[] data, int start, int pivot, int end, Func<T, T, int> compare)
		{
			Sorting.<>c__DisplayClass4_0<T> CS$<>8__locals1;
			CS$<>8__locals1.data = data;
			if (compare(CS$<>8__locals1.data[end], CS$<>8__locals1.data[start]) < 0)
			{
				Sorting.<Median3Pivot>g__Swap|4_0<T>(start, end, ref CS$<>8__locals1);
			}
			if (compare(CS$<>8__locals1.data[pivot], CS$<>8__locals1.data[start]) < 0)
			{
				Sorting.<Median3Pivot>g__Swap|4_0<T>(start, pivot, ref CS$<>8__locals1);
			}
			if (compare(CS$<>8__locals1.data[end], CS$<>8__locals1.data[pivot]) < 0)
			{
				Sorting.<Median3Pivot>g__Swap|4_0<T>(pivot, end, ref CS$<>8__locals1);
			}
			return CS$<>8__locals1.data[pivot];
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00015C1C File Offset: 0x00013E1C
		private static int Partition<T>(T[] data, int start, int end, Func<T, T, int> compare)
		{
			int diff = end - start;
			int pivot = start + diff / 2;
			T pivotValue = Sorting.Median3Pivot<T>(data, start, pivot, end, compare);
			for (;;)
			{
				if (compare(data[start], pivotValue) >= 0)
				{
					while (compare(data[end], pivotValue) > 0)
					{
						end--;
					}
					if (start >= end)
					{
						break;
					}
					T tmp = data[start];
					data[start++] = data[end];
					data[end--] = tmp;
				}
				else
				{
					start++;
				}
			}
			return end;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00015CA0 File Offset: 0x00013EA0
		public static void InsertionSort<T>(T[] data, Func<T, T, int> compare)
		{
			using (new ProfilingScope(Sorting.s_InsertionSortSampler))
			{
				Sorting.InsertionSort<T>(data, 0, data.Length - 1, compare);
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00015CE8 File Offset: 0x00013EE8
		public static void InsertionSort<T>(T[] data, int start, int end, Func<T, T, int> compare)
		{
			for (int i = start + 1; i < end + 1; i++)
			{
				T iData = data[i];
				int j = i - 1;
				while (j >= 0 && compare(iData, data[j]) < 0)
				{
					data[j + 1] = data[j];
					j--;
				}
				data[j + 1] = iData;
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00015D68 File Offset: 0x00013F68
		[CompilerGenerated]
		internal static void <Median3Pivot>g__Swap|4_0<T>(int a, int b, ref Sorting.<>c__DisplayClass4_0<T> A_2)
		{
			T tmp = A_2.data[a];
			A_2.data[a] = A_2.data[b];
			A_2.data[b] = tmp;
		}

		// Token: 0x040004F6 RID: 1270
		public static ProfilingSampler s_QuickSortSampler = new ProfilingSampler("QuickSort");

		// Token: 0x040004F7 RID: 1271
		public static ProfilingSampler s_InsertionSortSampler = new ProfilingSampler("InsertionSort");
	}
}
