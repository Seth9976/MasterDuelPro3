using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x02000037 RID: 55
	public static class CoreUnsafeUtils
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x00006D4C File Offset: 0x00004F4C
		public unsafe static void CopyTo<T>(this List<T> list, void* dest, int count) where T : struct
		{
			int c = Mathf.Min(count, list.Count);
			for (int i = 0; i < c; i++)
			{
				UnsafeUtility.WriteArrayElement<T>(dest, i, list[i]);
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00006D80 File Offset: 0x00004F80
		public unsafe static void CopyTo<T>(this T[] list, void* dest, int count) where T : struct
		{
			int c = Mathf.Min(count, list.Length);
			for (int i = 0; i < c; i++)
			{
				UnsafeUtility.WriteArrayElement<T>(dest, i, list[i]);
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00006DB1 File Offset: 0x00004FB1
		private static void CalculateRadixParams(int radixBits, out int bitStates)
		{
			bitStates = 1 << radixBits;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00006DBB File Offset: 0x00004FBB
		private static int CalculateRadixSupportSize(int bitStates, int arrayLength)
		{
			return bitStates * 3 + arrayLength;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00006DC2 File Offset: 0x00004FC2
		private unsafe static void CalculateRadixSortSupportArrays(int bitStates, int arrayLength, uint* supportArray, out uint* bucketIndices, out uint* bucketSizes, out uint* bucketPrefix, out uint* arrayOutput)
		{
			bucketIndices = supportArray;
			bucketSizes = bucketIndices + (IntPtr)bitStates * 4;
			bucketPrefix = bucketSizes + (IntPtr)bitStates * 4;
			arrayOutput = bucketPrefix + (IntPtr)bitStates * 4;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00006DE8 File Offset: 0x00004FE8
		private unsafe static void MergeSort(uint* array, uint* support, int length)
		{
			for (int i = 1; i < length; i *= 2)
			{
				int left = 0;
				while (left + i < length)
				{
					int right = left + i;
					int rightend = right + i;
					if (rightend > length)
					{
						rightend = length;
					}
					int j = left;
					int k = left;
					int l = right;
					while (k < right)
					{
						if (l >= rightend)
						{
							break;
						}
						if (array[k] <= array[l])
						{
							support[j] = array[k++];
						}
						else
						{
							support[j] = array[l++];
						}
						j++;
					}
					while (k < right)
					{
						support[j] = array[k++];
						j++;
					}
					while (l < rightend)
					{
						support[j] = array[l++];
						j++;
					}
					for (j = left; j < rightend; j++)
					{
						array[j] = support[j];
					}
					left += i * 2;
				}
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00006EE8 File Offset: 0x000050E8
		public unsafe static void MergeSort(uint[] arr, int sortSize, ref uint[] supportArray)
		{
			sortSize = Math.Min(sortSize, arr.Length);
			if (arr == null || sortSize == 0)
			{
				return;
			}
			if (supportArray == null || supportArray.Length < sortSize)
			{
				supportArray = new uint[sortSize];
			}
			fixed (uint[] array = arr)
			{
				uint* arrPtr;
				if (arr == null || array.Length == 0)
				{
					arrPtr = null;
				}
				else
				{
					arrPtr = &array[0];
				}
				uint[] array2;
				uint* supportPtr;
				if ((array2 = supportArray) == null || array2.Length == 0)
				{
					supportPtr = null;
				}
				else
				{
					supportPtr = &array2[0];
				}
				CoreUnsafeUtils.MergeSort(arrPtr, supportPtr, sortSize);
				array2 = null;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00006F58 File Offset: 0x00005158
		public unsafe static void MergeSort(NativeArray<uint> arr, int sortSize, ref NativeArray<uint> supportArray)
		{
			sortSize = Math.Min(sortSize, arr.Length);
			if (!arr.IsCreated || sortSize == 0)
			{
				return;
			}
			if (!supportArray.IsCreated || supportArray.Length < sortSize)
			{
				(ref supportArray).ResizeArray(arr.Length);
			}
			CoreUnsafeUtils.MergeSort((uint*)arr.GetUnsafePtr<uint>(), (uint*)supportArray.GetUnsafePtr<uint>(), sortSize);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00006FB8 File Offset: 0x000051B8
		private unsafe static void InsertionSort(uint* arr, int length)
		{
			for (int i = 0; i < length; i++)
			{
				int j = i;
				while (j >= 1 && arr[j] < arr[j - 1])
				{
					uint tmp = arr[j];
					arr[j] = arr[j - 1];
					arr[j - 1] = tmp;
					j--;
				}
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00007014 File Offset: 0x00005214
		public unsafe static void InsertionSort(uint[] arr, int sortSize)
		{
			sortSize = Math.Min(arr.Length, sortSize);
			if (arr == null || sortSize == 0)
			{
				return;
			}
			fixed (uint[] array = arr)
			{
				uint* ptr;
				if (arr == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				CoreUnsafeUtils.InsertionSort(ptr, sortSize);
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00007054 File Offset: 0x00005254
		public unsafe static void InsertionSort(NativeArray<uint> arr, int sortSize)
		{
			sortSize = Math.Min(arr.Length, sortSize);
			if (!arr.IsCreated || sortSize == 0)
			{
				return;
			}
			CoreUnsafeUtils.InsertionSort((uint*)arr.GetUnsafePtr<uint>(), sortSize);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00007080 File Offset: 0x00005280
		private unsafe static void RadixSort(uint* array, uint* support, int radixBits, int bitStates, int length)
		{
			uint mask = (uint)(bitStates - 1);
			uint* bucketIndices;
			uint* bucketSizes;
			uint* bucketPrefix;
			uint* arrayOutput;
			CoreUnsafeUtils.CalculateRadixSortSupportArrays(bitStates, length, support, out bucketIndices, out bucketSizes, out bucketPrefix, out arrayOutput);
			int buckets = 32 / radixBits;
			uint* targetBuffer = arrayOutput;
			uint* inputBuffer = array;
			for (int b = 0; b < buckets; b++)
			{
				int shift = b * radixBits;
				for (int s = 0; s < 3 * bitStates; s++)
				{
					bucketIndices[s] = 0U;
				}
				for (int i = 0; i < length; i++)
				{
					bucketSizes[(ulong)((inputBuffer[i] >> shift) & mask) * 4UL / 4UL] += 1U;
				}
				for (int s2 = 1; s2 < bitStates; s2++)
				{
					bucketPrefix[s2] = bucketPrefix[s2 - 1] + bucketSizes[s2 - 1];
				}
				for (int j = 0; j < length; j++)
				{
					uint val = inputBuffer[j];
					uint bucket = (val >> shift) & mask;
					ref int ptr = ref *(int*)targetBuffer;
					uint num = bucketPrefix[(ulong)bucket * 4UL / 4UL];
					uint* ptr2 = bucketIndices + (ulong)bucket * 4UL / 4UL;
					uint num2 = *ptr2;
					*ptr2 = num2 + 1U;
					*((ref ptr) + (IntPtr)((ulong)(num + num2) * 4UL)) = (int)val;
				}
				uint* ptr3 = inputBuffer;
				inputBuffer = targetBuffer;
				targetBuffer = ptr3;
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000719C File Offset: 0x0000539C
		public unsafe static void RadixSort(uint[] arr, int sortSize, ref uint[] supportArray, int radixBits = 8)
		{
			sortSize = Math.Min(sortSize, arr.Length);
			int bitStates;
			CoreUnsafeUtils.CalculateRadixParams(radixBits, out bitStates);
			if (arr == null || sortSize == 0)
			{
				return;
			}
			int supportSize = CoreUnsafeUtils.CalculateRadixSupportSize(bitStates, sortSize);
			if (supportArray == null || supportArray.Length < supportSize)
			{
				supportArray = new uint[supportSize];
			}
			fixed (uint[] array = arr)
			{
				uint* ptr;
				if (arr == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				uint[] array2;
				uint* supportArrayPtr;
				if ((array2 = supportArray) == null || array2.Length == 0)
				{
					supportArrayPtr = null;
				}
				else
				{
					supportArrayPtr = &array2[0];
				}
				CoreUnsafeUtils.RadixSort(ptr, supportArrayPtr, radixBits, bitStates, sortSize);
				array2 = null;
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00007224 File Offset: 0x00005424
		public unsafe static void RadixSort(NativeArray<uint> array, int sortSize, ref NativeArray<uint> supportArray, int radixBits = 8)
		{
			sortSize = Math.Min(sortSize, array.Length);
			int bitStates;
			CoreUnsafeUtils.CalculateRadixParams(radixBits, out bitStates);
			if (!array.IsCreated || sortSize == 0)
			{
				return;
			}
			int supportSize = CoreUnsafeUtils.CalculateRadixSupportSize(bitStates, sortSize);
			if (!supportArray.IsCreated || supportArray.Length < supportSize)
			{
				(ref supportArray).ResizeArray(supportSize);
			}
			CoreUnsafeUtils.RadixSort((uint*)array.GetUnsafePtr<uint>(), (uint*)supportArray.GetUnsafePtr<uint>(), radixBits, bitStates, sortSize);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00007290 File Offset: 0x00005490
		public unsafe static void QuickSort(uint[] arr, int left, int right)
		{
			fixed (uint[] array = arr)
			{
				uint* ptr;
				if (arr == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				CoreUnsafeUtils.QuickSort<uint, uint, CoreUnsafeUtils.UintKeyGetter>((void*)ptr, left, right);
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000072C0 File Offset: 0x000054C0
		public unsafe static void QuickSort(ulong[] arr, int left, int right)
		{
			fixed (ulong[] array = arr)
			{
				ulong* ptr;
				if (arr == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				CoreUnsafeUtils.QuickSort<ulong, ulong, CoreUnsafeUtils.UlongKeyGetter>((void*)ptr, left, right);
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000072EF File Offset: 0x000054EF
		public unsafe static void QuickSort<T>(int count, void* data) where T : struct, IComparable<T>
		{
			CoreUnsafeUtils.QuickSort<T, T, CoreUnsafeUtils.DefaultKeyGetter<T>>(data, 0, count - 1);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000072FB File Offset: 0x000054FB
		public unsafe static void QuickSort<TValue, TKey, TGetter>(int count, void* data) where TValue : struct where TKey : struct, IComparable<TKey> where TGetter : struct, CoreUnsafeUtils.IKeyGetter<TValue, TKey>
		{
			CoreUnsafeUtils.QuickSort<TValue, TKey, TGetter>(data, 0, count - 1);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00007308 File Offset: 0x00005508
		public unsafe static void QuickSort<TValue, TKey, TGetter>(void* data, int left, int right) where TValue : struct where TKey : struct, IComparable<TKey> where TGetter : struct, CoreUnsafeUtils.IKeyGetter<TValue, TKey>
		{
			if (left < right)
			{
				int pivot = CoreUnsafeUtils.Partition<TValue, TKey, TGetter>(data, left, right);
				if (pivot >= 1)
				{
					CoreUnsafeUtils.QuickSort<TValue, TKey, TGetter>(data, left, pivot);
				}
				if (pivot + 1 < right)
				{
					CoreUnsafeUtils.QuickSort<TValue, TKey, TGetter>(data, pivot + 1, right);
				}
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00007340 File Offset: 0x00005540
		public unsafe static int IndexOf<T>(void* data, int count, T v) where T : struct, IEquatable<T>
		{
			for (int i = 0; i < count; i++)
			{
				T t = UnsafeUtility.ReadArrayElement<T>(data, i);
				if (t.Equals(v))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00007374 File Offset: 0x00005574
		public unsafe static int CompareHashes<TOldValue, TOldGetter, TNewValue, TNewGetter>(int oldHashCount, void* oldHashes, int newHashCount, void* newHashes, int* addIndices, int* removeIndices, out int addCount, out int remCount) where TOldValue : struct where TOldGetter : struct, CoreUnsafeUtils.IKeyGetter<TOldValue, Hash128> where TNewValue : struct where TNewGetter : struct, CoreUnsafeUtils.IKeyGetter<TNewValue, Hash128>
		{
			TOldGetter oldGetter = new TOldGetter();
			TNewGetter newGetter = new TNewGetter();
			addCount = 0;
			remCount = 0;
			if (oldHashCount == newHashCount)
			{
				Hash128 oldHash = default(Hash128);
				Hash128 newHash = default(Hash128);
				CoreUnsafeUtils.CombineHashes<TOldValue, TOldGetter>(oldHashCount, oldHashes, &oldHash);
				CoreUnsafeUtils.CombineHashes<TNewValue, TNewGetter>(newHashCount, newHashes, &newHash);
				if (oldHash == newHash)
				{
					return 0;
				}
			}
			int numOperations = 0;
			int oldI = 0;
			int newI = 0;
			while (oldI < oldHashCount || newI < newHashCount)
			{
				if (oldI == oldHashCount)
				{
					while (newI < newHashCount)
					{
						int num = addCount;
						addCount = num + 1;
						addIndices[num] = newI;
						numOperations++;
						newI++;
					}
				}
				else if (newI == newHashCount)
				{
					while (oldI < oldHashCount)
					{
						int num = remCount;
						remCount = num + 1;
						removeIndices[num] = oldI;
						numOperations++;
						oldI++;
					}
				}
				else
				{
					TNewValue newVal = UnsafeUtility.ReadArrayElement<TNewValue>(newHashes, newI);
					TOldValue oldVal = UnsafeUtility.ReadArrayElement<TOldValue>(oldHashes, oldI);
					Hash128 newKey = newGetter.Get(ref newVal);
					Hash128 oldKey = oldGetter.Get(ref oldVal);
					if (newKey == oldKey)
					{
						newI++;
						oldI++;
					}
					else if (newKey < oldKey)
					{
						while (newI < newHashCount)
						{
							if (!(newKey < oldKey))
							{
								break;
							}
							int num = addCount;
							addCount = num + 1;
							addIndices[num] = newI;
							newI++;
							numOperations++;
							newVal = UnsafeUtility.ReadArrayElement<TNewValue>(newHashes, newI);
							newKey = newGetter.Get(ref newVal);
						}
					}
					else
					{
						while (oldI < oldHashCount && oldKey < newKey)
						{
							int num = remCount;
							remCount = num + 1;
							removeIndices[num] = oldI;
							numOperations++;
							oldI++;
						}
					}
				}
			}
			return numOperations;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000751C File Offset: 0x0000571C
		public unsafe static int CompareHashes(int oldHashCount, Hash128* oldHashes, int newHashCount, Hash128* newHashes, int* addIndices, int* removeIndices, out int addCount, out int remCount)
		{
			return CoreUnsafeUtils.CompareHashes<Hash128, CoreUnsafeUtils.DefaultKeyGetter<Hash128>, Hash128, CoreUnsafeUtils.DefaultKeyGetter<Hash128>>(oldHashCount, (void*)oldHashes, newHashCount, (void*)newHashes, addIndices, removeIndices, out addCount, out remCount);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00007530 File Offset: 0x00005730
		public unsafe static void CombineHashes<TValue, TGetter>(int count, void* hashes, Hash128* outHash) where TValue : struct where TGetter : struct, CoreUnsafeUtils.IKeyGetter<TValue, Hash128>
		{
			TGetter getter = new TGetter();
			for (int i = 0; i < count; i++)
			{
				TValue v = UnsafeUtility.ReadArrayElement<TValue>(hashes, i);
				Hash128 h = getter.Get(ref v);
				HashUtilities.AppendHash(ref h, ref *outHash);
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000756F File Offset: 0x0000576F
		public unsafe static void CombineHashes(int count, Hash128* hashes, Hash128* outHash)
		{
			CoreUnsafeUtils.CombineHashes<Hash128, CoreUnsafeUtils.DefaultKeyGetter<Hash128>>(count, (void*)hashes, outHash);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000757C File Offset: 0x0000577C
		private unsafe static int Partition<TValue, TKey, TGetter>(void* data, int left, int right) where TValue : struct where TKey : struct, IComparable<TKey> where TGetter : struct, CoreUnsafeUtils.IKeyGetter<TValue, TKey>
		{
			TGetter getter = default(TGetter);
			TValue pivotvalue = UnsafeUtility.ReadArrayElement<TValue>(data, left);
			TKey pivot = getter.Get(ref pivotvalue);
			left--;
			right++;
			for (;;)
			{
				TValue lvalue = default(TValue);
				TKey lkey = default(TKey);
				int c;
				do
				{
					left++;
					lvalue = UnsafeUtility.ReadArrayElement<TValue>(data, left);
					lkey = getter.Get(ref lvalue);
					c = lkey.CompareTo(pivot);
				}
				while (c < 0);
				TValue rvalue = default(TValue);
				TKey rkey = default(TKey);
				do
				{
					right--;
					rvalue = UnsafeUtility.ReadArrayElement<TValue>(data, right);
					rkey = getter.Get(ref rvalue);
					c = rkey.CompareTo(pivot);
				}
				while (c > 0);
				if (left >= right)
				{
					break;
				}
				UnsafeUtility.WriteArrayElement<TValue>(data, right, lvalue);
				UnsafeUtility.WriteArrayElement<TValue>(data, left, rvalue);
			}
			return right;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00007658 File Offset: 0x00005858
		public unsafe static bool HaveDuplicates(int[] arr)
		{
			int* copy;
			checked
			{
				copy = stackalloc int[unchecked((UIntPtr)arr.Length) * 4];
				arr.CopyTo((void*)copy, arr.Length);
				CoreUnsafeUtils.QuickSort<int>(arr.Length, (void*)copy);
			}
			for (int i = arr.Length - 1; i > 0; i--)
			{
				if (UnsafeUtility.ReadArrayElement<int>((void*)copy, i).CompareTo(UnsafeUtility.ReadArrayElement<int>((void*)copy, i - 1)) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x02000038 RID: 56
		public struct FixedBufferStringQueue
		{
			// Token: 0x1700002A RID: 42
			// (get) Token: 0x0600041B RID: 1051 RVA: 0x000076AE File Offset: 0x000058AE
			// (set) Token: 0x0600041C RID: 1052 RVA: 0x000076B6 File Offset: 0x000058B6
			public int Count { readonly get; private set; }

			// Token: 0x0600041D RID: 1053 RVA: 0x000076C0 File Offset: 0x000058C0
			public unsafe FixedBufferStringQueue(byte* ptr, int length)
			{
				this.m_BufferStart = ptr;
				this.m_BufferLength = length;
				this.m_BufferEnd = this.m_BufferStart + this.m_BufferLength;
				this.m_ReadCursor = this.m_BufferStart;
				this.m_WriteCursor = this.m_BufferStart;
				this.Count = 0;
				this.Clear();
			}

			// Token: 0x0600041E RID: 1054 RVA: 0x00007714 File Offset: 0x00005914
			public unsafe bool TryPush(string v)
			{
				int size = v.Length * 2 + 4;
				if (this.m_WriteCursor + size >= this.m_BufferEnd)
				{
					return false;
				}
				*(int*)this.m_WriteCursor = v.Length;
				this.m_WriteCursor += 4;
				char* charPtr = (char*)this.m_WriteCursor;
				int i = 0;
				while (i < v.Length)
				{
					*charPtr = v[i];
					i++;
					charPtr++;
				}
				this.m_WriteCursor += 2 * v.Length;
				int num = this.Count + 1;
				this.Count = num;
				return true;
			}

			// Token: 0x0600041F RID: 1055 RVA: 0x000077A4 File Offset: 0x000059A4
			public unsafe bool TryPop(out string v)
			{
				int size = *(int*)this.m_ReadCursor;
				if (size != 0)
				{
					this.m_ReadCursor += 4;
					v = new string((char*)this.m_ReadCursor, 0, size);
					this.m_ReadCursor += size * 2;
					return true;
				}
				v = null;
				return false;
			}

			// Token: 0x06000420 RID: 1056 RVA: 0x000077EF File Offset: 0x000059EF
			public unsafe void Clear()
			{
				this.m_WriteCursor = this.m_BufferStart;
				this.m_ReadCursor = this.m_BufferStart;
				this.Count = 0;
				UnsafeUtility.MemClear((void*)this.m_BufferStart, (long)this.m_BufferLength);
			}

			// Token: 0x040000BE RID: 190
			private unsafe byte* m_ReadCursor;

			// Token: 0x040000BF RID: 191
			private unsafe byte* m_WriteCursor;

			// Token: 0x040000C0 RID: 192
			private unsafe readonly byte* m_BufferEnd;

			// Token: 0x040000C1 RID: 193
			private unsafe readonly byte* m_BufferStart;

			// Token: 0x040000C2 RID: 194
			private readonly int m_BufferLength;
		}

		// Token: 0x02000039 RID: 57
		public interface IKeyGetter<TValue, TKey>
		{
			// Token: 0x06000421 RID: 1057
			TKey Get(ref TValue v);
		}

		// Token: 0x0200003A RID: 58
		internal struct DefaultKeyGetter<T> : CoreUnsafeUtils.IKeyGetter<T, T>
		{
			// Token: 0x06000422 RID: 1058 RVA: 0x00007822 File Offset: 0x00005A22
			public T Get(ref T v)
			{
				return v;
			}
		}

		// Token: 0x0200003B RID: 59
		internal struct UintKeyGetter : CoreUnsafeUtils.IKeyGetter<uint, uint>
		{
			// Token: 0x06000423 RID: 1059 RVA: 0x0000782A File Offset: 0x00005A2A
			public uint Get(ref uint v)
			{
				return v;
			}
		}

		// Token: 0x0200003C RID: 60
		internal struct UlongKeyGetter : CoreUnsafeUtils.IKeyGetter<ulong, ulong>
		{
			// Token: 0x06000424 RID: 1060 RVA: 0x0000782E File Offset: 0x00005A2E
			public ulong Get(ref ulong v)
			{
				return v;
			}
		}
	}
}
