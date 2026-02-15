using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x020000C3 RID: 195
	[GenerateTestsForBurstCompatibility]
	public static class NativeSortExtension
	{
		// Token: 0x06000900 RID: 2304 RVA: 0x0001B1C0 File Offset: 0x000193C0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T* array, int length) where T : struct, ValueType, IComparable<T>
		{
			NativeSortExtension.IntroSort<T, NativeSortExtension.DefaultComparer<T>>((void*)array, length, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001B1DD File Offset: 0x000193DD
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(T* array, int length, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			NativeSortExtension.IntroSort<T, U>((void*)array, length, comp);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001B1E8 File Offset: 0x000193E8
		[GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static SortJob<T, NativeSortExtension.DefaultComparer<T>> SortJob<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T* array, int length) where T : struct, ValueType, IComparable<T>
		{
			return new SortJob<T, NativeSortExtension.DefaultComparer<T>>
			{
				Data = array,
				Length = length,
				Comp = default(NativeSortExtension.DefaultComparer<T>)
			};
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001B21C File Offset: 0x0001941C
		[GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static SortJob<T, U> SortJob<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(T* array, int length, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return new SortJob<T, U>
			{
				Data = array,
				Length = length,
				Comp = comp
			};
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0001B24C File Offset: 0x0001944C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static int BinarySearch<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T* ptr, int length, T value) where T : struct, ValueType, IComparable<T>
		{
			return NativeSortExtension.BinarySearch<T, NativeSortExtension.DefaultComparer<T>>(ptr, length, value, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0001B26C File Offset: 0x0001946C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static int BinarySearch<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(T* ptr, int length, T value, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			int offset = 0;
			for (int i = length; i != 0; i >>= 1)
			{
				int idx = offset + (i >> 1);
				T curr = ptr[(IntPtr)idx * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				int r = comp.Compare(value, curr);
				if (r == 0)
				{
					return idx;
				}
				if (r > 0)
				{
					offset = idx + 1;
					i--;
				}
			}
			return ~offset;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001B2C4 File Offset: 0x000194C4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeArray<T> array) where T : struct, ValueType, IComparable<T>
		{
			NativeSortExtension.IntroSortStruct<T, NativeSortExtension.DefaultComparer<T>>(array.GetUnsafePtr<T>(), array.Length, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0001B2EC File Offset: 0x000194EC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this NativeArray<T> array, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			T* ptr = (T*)array.GetUnsafePtr<T>();
			int len = array.Length;
			NativeSortExtension.IntroSortStruct<T, U>((void*)ptr, len, comp);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0001B310 File Offset: 0x00019510
		[GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static SortJob<T, NativeSortExtension.DefaultComparer<T>> SortJob<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeArray<T> array) where T : struct, ValueType, IComparable<T>
		{
			return NativeSortExtension.SortJob<T, NativeSortExtension.DefaultComparer<T>>((T*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(array), array.Length, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0001B338 File Offset: 0x00019538
		[GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static SortJob<T, U> SortJob<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this NativeArray<T> array, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			T* ptr = (T*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(array);
			int len = array.Length;
			return new SortJob<T, U>
			{
				Data = ptr,
				Length = len,
				Comp = comp
			};
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0001B378 File Offset: 0x00019578
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static int BinarySearch<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeArray<T> array, T value) where T : struct, ValueType, IComparable<T>
		{
			return array.BinarySearch(value, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001B395 File Offset: 0x00019595
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static int BinarySearch<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this NativeArray<T> array, T value, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.BinarySearch<T, U>((T*)array.GetUnsafeReadOnlyPtr<T>(), array.Length, value, comp);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0001B3AC File Offset: 0x000195AC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static int BinarySearch<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeArray<T>.ReadOnly array, T value) where T : struct, ValueType, IComparable<T>
		{
			return array.BinarySearch(value, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001B3C9 File Offset: 0x000195C9
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static int BinarySearch<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this NativeArray<T>.ReadOnly array, T value, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.BinarySearch<T, U>((T*)array.GetUnsafeReadOnlyPtr<T>(), array.Length, value, comp);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0001B3E0 File Offset: 0x000195E0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeList<T> list) where T : struct, ValueType, IComparable<T>
		{
			list.Sort(default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0001B3FC File Offset: 0x000195FC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this NativeList<T> list, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			NativeSortExtension.IntroSort<T, U>((void*)list.GetUnsafePtr<T>(), list.Length, comp);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0001B414 File Offset: 0x00019614
		[GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new Type[] { typeof(int) })]
		public static SortJob<T, NativeSortExtension.DefaultComparer<T>> SortJob<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeList<T> list) where T : struct, ValueType, IComparable<T>
		{
			return list.SortJob(default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0001B430 File Offset: 0x00019630
		[GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public static SortJob<T, U> SortJob<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this NativeList<T> list, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.SortJob<T, U>(list.GetUnsafePtr<T>(), list.Length, comp);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0001B448 File Offset: 0x00019648
		[GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new Type[] { typeof(int) })]
		public static SortJobDefer<T, NativeSortExtension.DefaultComparer<T>> SortJobDefer<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeList<T> list) where T : struct, ValueType, IComparable<T>
		{
			return list.SortJobDefer(default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001B464 File Offset: 0x00019664
		[GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public static SortJobDefer<T, U> SortJobDefer<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this NativeList<T> list, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return new SortJobDefer<T, U>
			{
				Data = list,
				Comp = comp
			};
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001B48C File Offset: 0x0001968C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static int BinarySearch<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeList<T> list, T value) where T : struct, ValueType, IComparable<T>
		{
			return list.AsReadOnly().BinarySearch(value, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001B4AF File Offset: 0x000196AF
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public static int BinarySearch<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this NativeList<T> list, T value, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return list.AsReadOnly().BinarySearch(value, comp);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0001B4C0 File Offset: 0x000196C0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeList<T> list) where T : struct, ValueType, IComparable<T>
		{
			list.Sort(default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0001B4DC File Offset: 0x000196DC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this UnsafeList<T> list, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			NativeSortExtension.IntroSort<T, U>((void*)list.Ptr, list.Length, comp);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0001B4F4 File Offset: 0x000196F4
		[GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new Type[] { typeof(int) })]
		public static SortJob<T, NativeSortExtension.DefaultComparer<T>> SortJob<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeList<T> list) where T : struct, ValueType, IComparable<T>
		{
			return NativeSortExtension.SortJob<T, NativeSortExtension.DefaultComparer<T>>(list.Ptr, list.Length, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0001B51C File Offset: 0x0001971C
		[GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public static SortJob<T, U> SortJob<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this UnsafeList<T> list, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.SortJob<T, U>(list.Ptr, list.Length, comp);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0001B534 File Offset: 0x00019734
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static int BinarySearch<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeList<T> list, T value) where T : struct, ValueType, IComparable<T>
		{
			return list.BinarySearch(value, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001B551 File Offset: 0x00019751
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public static int BinarySearch<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this UnsafeList<T> list, T value, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.BinarySearch<T, U>(list.Ptr, list.Length, value, comp);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001B568 File Offset: 0x00019768
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeSlice<T> slice) where T : struct, ValueType, IComparable<T>
		{
			slice.Sort(default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0001B584 File Offset: 0x00019784
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this NativeSlice<T> slice, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			T* ptr = (T*)slice.GetUnsafePtr<T>();
			int len = slice.Length;
			NativeSortExtension.IntroSortStruct<T, U>((void*)ptr, len, comp);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001B5A8 File Offset: 0x000197A8
		[GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static SortJob<T, NativeSortExtension.DefaultComparer<T>> SortJob<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeSlice<T> slice) where T : struct, ValueType, IComparable<T>
		{
			return NativeSortExtension.SortJob<T, NativeSortExtension.DefaultComparer<T>>((T*)slice.GetUnsafePtr<T>(), slice.Length, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001B5D0 File Offset: 0x000197D0
		[GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static SortJob<T, U> SortJob<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this NativeSlice<T> slice, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.SortJob<T, U>((T*)slice.GetUnsafePtr<T>(), slice.Length, comp);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001B5E8 File Offset: 0x000197E8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static int BinarySearch<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeSlice<T> slice, T value) where T : struct, ValueType, IComparable<T>
		{
			return slice.BinarySearch(value, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001B605 File Offset: 0x00019805
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static int BinarySearch<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this NativeSlice<T> slice, T value, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.BinarySearch<T, U>((T*)slice.GetUnsafeReadOnlyPtr<T>(), slice.Length, value, comp);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001B61B File Offset: 0x0001981B
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		internal unsafe static void IntroSort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(void* array, int length, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			NativeSortExtension.IntroSort_R<T, U>(array, 0, length - 1, 2 * CollectionHelper.Log2Floor(length), comp);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001B630 File Offset: 0x00019830
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		internal unsafe static void IntroSort_R<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(void* array, int lo, int hi, int depth, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			while (hi > lo)
			{
				int partitionSize = hi - lo + 1;
				if (partitionSize <= 16)
				{
					if (partitionSize == 1)
					{
						return;
					}
					if (partitionSize == 2)
					{
						NativeSortExtension.SwapIfGreaterWithItems<T, U>(array, lo, hi, comp);
						return;
					}
					if (partitionSize == 3)
					{
						NativeSortExtension.SwapIfGreaterWithItems<T, U>(array, lo, hi - 1, comp);
						NativeSortExtension.SwapIfGreaterWithItems<T, U>(array, lo, hi, comp);
						NativeSortExtension.SwapIfGreaterWithItems<T, U>(array, hi - 1, hi, comp);
						return;
					}
					NativeSortExtension.InsertionSort<T, U>(array, lo, hi, comp);
					return;
				}
				else
				{
					if (depth == 0)
					{
						NativeSortExtension.HeapSort<T, U>(array, lo, hi, comp);
						return;
					}
					depth--;
					int p = NativeSortExtension.Partition<T, U>(array, lo, hi, comp);
					NativeSortExtension.IntroSort_R<T, U>(array, p + 1, hi, depth, comp);
					hi = p - 1;
				}
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0001B6CC File Offset: 0x000198CC
		private unsafe static void InsertionSort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(void* array, int lo, int hi, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			for (int i = lo; i < hi; i++)
			{
				int j = i;
				T t = UnsafeUtility.ReadArrayElement<T>(array, i + 1);
				while (j >= lo && comp.Compare(t, UnsafeUtility.ReadArrayElement<T>(array, j)) < 0)
				{
					UnsafeUtility.WriteArrayElement<T>(array, j + 1, UnsafeUtility.ReadArrayElement<T>(array, j));
					j--;
				}
				UnsafeUtility.WriteArrayElement<T>(array, j + 1, t);
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001B730 File Offset: 0x00019930
		private unsafe static int Partition<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(void* array, int lo, int hi, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			int mid = lo + (hi - lo) / 2;
			NativeSortExtension.SwapIfGreaterWithItems<T, U>(array, lo, mid, comp);
			NativeSortExtension.SwapIfGreaterWithItems<T, U>(array, lo, hi, comp);
			NativeSortExtension.SwapIfGreaterWithItems<T, U>(array, mid, hi, comp);
			T pivot = UnsafeUtility.ReadArrayElement<T>(array, mid);
			NativeSortExtension.Swap<T>(array, mid, hi - 1);
			int left = lo;
			int right = hi - 1;
			while (left < right)
			{
				while (left < hi && comp.Compare(pivot, UnsafeUtility.ReadArrayElement<T>(array, ++left)) > 0)
				{
				}
				while (right > left && comp.Compare(pivot, UnsafeUtility.ReadArrayElement<T>(array, --right)) < 0)
				{
				}
				if (left >= right)
				{
					break;
				}
				NativeSortExtension.Swap<T>(array, left, right);
			}
			NativeSortExtension.Swap<T>(array, left, hi - 1);
			return left;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001B7D8 File Offset: 0x000199D8
		private unsafe static void HeapSort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(void* array, int lo, int hi, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			int i = hi - lo + 1;
			for (int j = i / 2; j >= 1; j--)
			{
				NativeSortExtension.Heapify<T, U>(array, j, i, lo, comp);
			}
			for (int k = i; k > 1; k--)
			{
				NativeSortExtension.Swap<T>(array, lo, lo + k - 1);
				NativeSortExtension.Heapify<T, U>(array, 1, k - 1, lo, comp);
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0001B828 File Offset: 0x00019A28
		private unsafe static void Heapify<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(void* array, int i, int n, int lo, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			T val = UnsafeUtility.ReadArrayElement<T>(array, lo + i - 1);
			while (i <= n / 2)
			{
				int child = 2 * i;
				if (child < n && comp.Compare(UnsafeUtility.ReadArrayElement<T>(array, lo + child - 1), UnsafeUtility.ReadArrayElement<T>(array, lo + child)) < 0)
				{
					child++;
				}
				if (comp.Compare(UnsafeUtility.ReadArrayElement<T>(array, lo + child - 1), val) < 0)
				{
					break;
				}
				UnsafeUtility.WriteArrayElement<T>(array, lo + i - 1, UnsafeUtility.ReadArrayElement<T>(array, lo + child - 1));
				i = child;
			}
			UnsafeUtility.WriteArrayElement<T>(array, lo + i - 1, val);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0001B8BC File Offset: 0x00019ABC
		private unsafe static void Swap<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(void* array, int lhs, int rhs) where T : struct, ValueType
		{
			T val = UnsafeUtility.ReadArrayElement<T>(array, lhs);
			UnsafeUtility.WriteArrayElement<T>(array, lhs, UnsafeUtility.ReadArrayElement<T>(array, rhs));
			UnsafeUtility.WriteArrayElement<T>(array, rhs, val);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0001B8E7 File Offset: 0x00019AE7
		private unsafe static void SwapIfGreaterWithItems<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(void* array, int lhs, int rhs, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			if (lhs != rhs && comp.Compare(UnsafeUtility.ReadArrayElement<T>(array, lhs), UnsafeUtility.ReadArrayElement<T>(array, rhs)) > 0)
			{
				NativeSortExtension.Swap<T>(array, lhs, rhs);
			}
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001B914 File Offset: 0x00019B14
		private unsafe static void IntroSortStruct<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(void* array, int length, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			int num = 0;
			int num2 = length - 1;
			NativeSortExtension.IntroSortStruct_R<T, U>(array, in num, in num2, 2 * CollectionHelper.Log2Floor(length), comp);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0001B93C File Offset: 0x00019B3C
		private unsafe static void IntroSortStruct_R<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(void* array, in int lo, in int _hi, int depth, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			int hi = _hi;
			while (hi > lo)
			{
				int partitionSize = hi - lo + 1;
				if (partitionSize <= 16)
				{
					if (partitionSize == 1)
					{
						return;
					}
					if (partitionSize == 2)
					{
						NativeSortExtension.SwapIfGreaterWithItemsStruct<T, U>(array, lo, hi, comp);
						return;
					}
					if (partitionSize == 3)
					{
						NativeSortExtension.SwapIfGreaterWithItemsStruct<T, U>(array, lo, hi - 1, comp);
						NativeSortExtension.SwapIfGreaterWithItemsStruct<T, U>(array, lo, hi, comp);
						NativeSortExtension.SwapIfGreaterWithItemsStruct<T, U>(array, hi - 1, hi, comp);
						return;
					}
					NativeSortExtension.InsertionSortStruct<T, U>(array, in lo, in hi, comp);
					return;
				}
				else
				{
					if (depth == 0)
					{
						NativeSortExtension.HeapSortStruct<T, U>(array, in lo, in hi, comp);
						return;
					}
					depth--;
					int p = NativeSortExtension.PartitionStruct<T, U>(array, in lo, in hi, comp);
					int num = p + 1;
					NativeSortExtension.IntroSortStruct_R<T, U>(array, in num, in hi, depth, comp);
					hi = p - 1;
				}
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001B9E4 File Offset: 0x00019BE4
		private unsafe static void InsertionSortStruct<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(void* array, in int lo, in int hi, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			for (int i = lo; i < hi; i++)
			{
				int j = i;
				T t = UnsafeUtility.ReadArrayElement<T>(array, i + 1);
				while (j >= lo && comp.Compare(t, UnsafeUtility.ReadArrayElement<T>(array, j)) < 0)
				{
					UnsafeUtility.WriteArrayElement<T>(array, j + 1, UnsafeUtility.ReadArrayElement<T>(array, j));
					j--;
				}
				UnsafeUtility.WriteArrayElement<T>(array, j + 1, t);
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0001BA48 File Offset: 0x00019C48
		private unsafe static int PartitionStruct<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(void* array, in int lo, in int hi, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			int mid = lo + (hi - lo) / 2;
			NativeSortExtension.SwapIfGreaterWithItemsStruct<T, U>(array, lo, mid, comp);
			NativeSortExtension.SwapIfGreaterWithItemsStruct<T, U>(array, lo, hi, comp);
			NativeSortExtension.SwapIfGreaterWithItemsStruct<T, U>(array, mid, hi, comp);
			T pivot = UnsafeUtility.ReadArrayElement<T>(array, mid);
			NativeSortExtension.SwapStruct<T>(array, mid, hi - 1);
			int left = lo;
			int right = hi - 1;
			while (left < right)
			{
				while (left < hi && comp.Compare(pivot, UnsafeUtility.ReadArrayElement<T>(array, ++left)) > 0)
				{
				}
				while (right > left && comp.Compare(pivot, UnsafeUtility.ReadArrayElement<T>(array, --right)) < 0)
				{
				}
				if (left >= right)
				{
					break;
				}
				NativeSortExtension.SwapStruct<T>(array, left, right);
			}
			NativeSortExtension.SwapStruct<T>(array, left, hi - 1);
			return left;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001BAFC File Offset: 0x00019CFC
		private unsafe static void HeapSortStruct<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(void* array, in int lo, in int hi, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			int i = hi - lo + 1;
			for (int j = i / 2; j >= 1; j--)
			{
				NativeSortExtension.HeapifyStruct<T, U>(array, j, i, in lo, comp);
			}
			for (int k = i; k > 1; k--)
			{
				NativeSortExtension.SwapStruct<T>(array, lo, lo + k - 1);
				NativeSortExtension.HeapifyStruct<T, U>(array, 1, k - 1, in lo, comp);
			}
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001BB50 File Offset: 0x00019D50
		private unsafe static void HeapifyStruct<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(void* array, int i, int n, in int lo, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			T val = UnsafeUtility.ReadArrayElement<T>(array, lo + i - 1);
			while (i <= n / 2)
			{
				int child = 2 * i;
				if (child < n && comp.Compare(UnsafeUtility.ReadArrayElement<T>(array, lo + child - 1), UnsafeUtility.ReadArrayElement<T>(array, lo + child)) < 0)
				{
					child++;
				}
				if (comp.Compare(UnsafeUtility.ReadArrayElement<T>(array, lo + child - 1), val) < 0)
				{
					break;
				}
				UnsafeUtility.WriteArrayElement<T>(array, lo + i - 1, UnsafeUtility.ReadArrayElement<T>(array, lo + child - 1));
				i = child;
			}
			UnsafeUtility.WriteArrayElement<T>(array, lo + i - 1, val);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001BBEC File Offset: 0x00019DEC
		private unsafe static void SwapStruct<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(void* array, int lhs, int rhs) where T : struct, ValueType
		{
			T val = UnsafeUtility.ReadArrayElement<T>(array, lhs);
			UnsafeUtility.WriteArrayElement<T>(array, lhs, UnsafeUtility.ReadArrayElement<T>(array, rhs));
			UnsafeUtility.WriteArrayElement<T>(array, rhs, val);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001BC17 File Offset: 0x00019E17
		private unsafe static void SwapIfGreaterWithItemsStruct<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(void* array, int lhs, int rhs, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			if (lhs != rhs && comp.Compare(UnsafeUtility.ReadArrayElement<T>(array, lhs), UnsafeUtility.ReadArrayElement<T>(array, rhs)) > 0)
			{
				NativeSortExtension.SwapStruct<T>(array, lhs, rhs);
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001BC43 File Offset: 0x00019E43
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckStrideMatchesSize<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(int stride) where T : struct, ValueType
		{
			if (stride != UnsafeUtility.SizeOf<T>())
			{
				throw new InvalidOperationException("Sort requires that stride matches the size of the source type");
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001BC58 File Offset: 0x00019E58
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private unsafe static void CheckComparer<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(T* array, int length, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			if (length > 0)
			{
				T a = *array;
				if (comp.Compare(a, a) != 0)
				{
					throw new InvalidOperationException("Comparison function is incorrect. Compare(a, a) must return 0/equal.");
				}
				int i = 1;
				int len = math.min(length, 8);
				while (i < len)
				{
					T b = array[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
					if (comp.Compare(a, b) != 0 || comp.Compare(b, a) != 0)
					{
						if (comp.Compare(a, b) == 0)
						{
							throw new InvalidOperationException("Comparison function is incorrect. Compare(a, b) of two different values should not return 0/equal.");
						}
						if (comp.Compare(b, a) == 0)
						{
							throw new InvalidOperationException("Comparison function is incorrect. Compare(b, a) of two different values should not return 0/equal.");
						}
						if (comp.Compare(a, b) == comp.Compare(b, a))
						{
							throw new InvalidOperationException("Comparison function is incorrect. Compare(a, b) when a and b are different values should not return the same value as Compare(b, a).");
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}
		}

		// Token: 0x040003EE RID: 1006
		private const int k_IntrosortSizeThreshold = 16;

		// Token: 0x020000C4 RID: 196
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct DefaultComparer<T> : IComparer<T> where T : IComparable<T>
		{
			// Token: 0x06000934 RID: 2356 RVA: 0x0001BD3F File Offset: 0x00019F3F
			public int Compare(T x, T y)
			{
				return x.CompareTo(y);
			}
		}
	}
}
