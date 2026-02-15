using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200010B RID: 267
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	internal struct HashMapHelper<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey> where TKey : struct, ValueType, IEquatable<TKey>
	{
		// Token: 0x06000B3C RID: 2876 RVA: 0x000227C8 File Offset: 0x000209C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal int CalcCapacityCeilPow2(int capacity)
		{
			capacity = math.max(math.max(1, this.Count), capacity);
			return math.ceilpow2(math.max(capacity, 1 << this.Log2MinGrowth));
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000227F4 File Offset: 0x000209F4
		internal static int GetBucketSize(int capacity)
		{
			return capacity * 2;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x000227F9 File Offset: 0x000209F9
		internal readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.Ptr != null;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00022808 File Offset: 0x00020A08
		internal readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return !this.IsCreated || this.Count == 0;
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00022820 File Offset: 0x00020A20
		internal unsafe void Clear()
		{
			UnsafeUtility.MemSet((void*)this.Buckets, byte.MaxValue, (long)(this.BucketCapacity * 4));
			UnsafeUtility.MemSet((void*)this.Next, byte.MaxValue, (long)(this.Capacity * 4));
			this.Count = 0;
			this.FirstFreeIdx = -1;
			this.AllocatedIndex = 0;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00022874 File Offset: 0x00020A74
		internal unsafe void Init(int capacity, int sizeOfValueT, int minGrowth, AllocatorManager.AllocatorHandle allocator)
		{
			this.Count = 0;
			this.Log2MinGrowth = (int)((byte)(32 - math.lzcnt(math.max(1, minGrowth) - 1)));
			capacity = this.CalcCapacityCeilPow2(capacity);
			this.Capacity = capacity;
			this.BucketCapacity = HashMapHelper<TKey>.GetBucketSize(capacity);
			this.Allocator = allocator;
			this.SizeOfTValue = sizeOfValueT;
			int keyOffset;
			int nextOffset;
			int bucketOffset;
			int totalSize = HashMapHelper<TKey>.CalculateDataSize(capacity, this.BucketCapacity, sizeOfValueT, out keyOffset, out nextOffset, out bucketOffset);
			this.Ptr = (byte*)Memory.Unmanaged.Allocate((long)totalSize, 64, allocator);
			this.Keys = (TKey*)(this.Ptr + keyOffset);
			this.Next = (int*)(this.Ptr + nextOffset);
			this.Buckets = (int*)(this.Ptr + bucketOffset);
			this.Clear();
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00022920 File Offset: 0x00020B20
		internal void Dispose()
		{
			Memory.Unmanaged.Free<byte>(this.Ptr, this.Allocator);
			this.Ptr = null;
			this.Keys = null;
			this.Next = null;
			this.Buckets = null;
			this.Count = 0;
			this.BucketCapacity = 0;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0002296C File Offset: 0x00020B6C
		internal unsafe static HashMapHelper<TKey>* Alloc(int capacity, int sizeOfValueT, int minGrowth, AllocatorManager.AllocatorHandle allocator)
		{
			HashMapHelper<TKey>* data = (HashMapHelper<TKey>*)Memory.Unmanaged.Allocate((long)sizeof(HashMapHelper<TKey>), UnsafeUtility.AlignOf<HashMapHelper<TKey>>(), allocator);
			data->Init(capacity, sizeOfValueT, minGrowth, allocator);
			return data;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00022997 File Offset: 0x00020B97
		internal unsafe static void Free(HashMapHelper<TKey>* data)
		{
			if (data == null)
			{
				throw new InvalidOperationException("Hash based container has yet to be created or has been destroyed!");
			}
			data->Dispose();
			Memory.Unmanaged.Free<HashMapHelper<TKey>>(data, data->Allocator);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x000229BC File Offset: 0x00020BBC
		internal void Resize(int newCapacity)
		{
			newCapacity = math.max(newCapacity, this.Count);
			int newBucketCapacity = math.ceilpow2(HashMapHelper<TKey>.GetBucketSize(newCapacity));
			if (this.Capacity == newCapacity && this.BucketCapacity == newBucketCapacity)
			{
				return;
			}
			this.ResizeExact(newCapacity, newBucketCapacity);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00022A00 File Offset: 0x00020C00
		internal unsafe void ResizeExact(int newCapacity, int newBucketCapacity)
		{
			int keyOffset;
			int nextOffset;
			int bucketOffset;
			int totalSize = HashMapHelper<TKey>.CalculateDataSize(newCapacity, newBucketCapacity, this.SizeOfTValue, out keyOffset, out nextOffset, out bucketOffset);
			byte* oldPtr = this.Ptr;
			TKey* oldKeys = this.Keys;
			int* oldNext = this.Next;
			int* oldBuckets = this.Buckets;
			int bucketCapacity = this.BucketCapacity;
			this.Ptr = (byte*)Memory.Unmanaged.Allocate((long)totalSize, 64, this.Allocator);
			this.Keys = (TKey*)(this.Ptr + keyOffset);
			this.Next = (int*)(this.Ptr + nextOffset);
			this.Buckets = (int*)(this.Ptr + bucketOffset);
			this.Capacity = newCapacity;
			this.BucketCapacity = newBucketCapacity;
			this.Clear();
			int i = 0;
			int num = bucketCapacity;
			while (i < num)
			{
				for (int idx = oldBuckets[i]; idx != -1; idx = oldNext[idx])
				{
					int newIdx = this.TryAdd(in oldKeys[(IntPtr)idx * (IntPtr)sizeof(TKey) / (IntPtr)sizeof(TKey)]);
					UnsafeUtility.MemCpy((void*)(this.Ptr + this.SizeOfTValue * newIdx), (void*)(oldPtr + this.SizeOfTValue * idx), (long)this.SizeOfTValue);
				}
				i++;
			}
			Memory.Unmanaged.Free<byte>(oldPtr, this.Allocator);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00022B14 File Offset: 0x00020D14
		internal void TrimExcess()
		{
			int capacity = this.CalcCapacityCeilPow2(this.Count);
			this.ResizeExact(capacity, HashMapHelper<TKey>.GetBucketSize(capacity));
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00022B3C File Offset: 0x00020D3C
		internal static int CalculateDataSize(int capacity, int bucketCapacity, int sizeOfTValue, out int outKeyOffset, out int outNextOffset, out int outBucketOffset)
		{
			int num = sizeof(TKey);
			int sizeOfInt = 4;
			int valuesSize = sizeOfTValue * capacity;
			int keysSize = num * capacity;
			int nextSize = sizeOfInt * capacity;
			int bucketSize = sizeOfInt * bucketCapacity;
			int num2 = valuesSize + keysSize + nextSize + bucketSize;
			outKeyOffset = valuesSize;
			outNextOffset = outKeyOffset + keysSize;
			outBucketOffset = outNextOffset + nextSize;
			return num2;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00022B7C File Offset: 0x00020D7C
		internal unsafe readonly int GetCount()
		{
			if (this.AllocatedIndex <= 0)
			{
				return 0;
			}
			int numFree = 0;
			for (int freeIdx = this.FirstFreeIdx; freeIdx >= 0; freeIdx = this.Next[freeIdx])
			{
				numFree++;
			}
			return math.min(this.Capacity, this.AllocatedIndex) - numFree;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00022BC8 File Offset: 0x00020DC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private int GetBucket(in TKey key)
		{
			TKey tkey = key;
			return (int)((ulong)tkey.GetHashCode() & (ulong)((long)(this.BucketCapacity - 1)));
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00022BF8 File Offset: 0x00020DF8
		internal unsafe int TryAdd(in TKey key)
		{
			if (-1 == this.Find(key))
			{
				if (this.AllocatedIndex >= this.Capacity && this.FirstFreeIdx < 0)
				{
					int newCap = this.CalcCapacityCeilPow2(this.Capacity + (1 << this.Log2MinGrowth));
					this.Resize(newCap);
				}
				int idx = this.FirstFreeIdx;
				if (idx >= 0)
				{
					this.FirstFreeIdx = this.Next[idx];
				}
				else
				{
					int allocatedIndex = this.AllocatedIndex;
					this.AllocatedIndex = allocatedIndex + 1;
					idx = allocatedIndex;
				}
				UnsafeUtility.WriteArrayElement<TKey>((void*)this.Keys, idx, key);
				int bucket = this.GetBucket(in key);
				this.Next[idx] = this.Buckets[bucket];
				this.Buckets[bucket] = idx;
				this.Count++;
				return idx;
			}
			return -1;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00022CD0 File Offset: 0x00020ED0
		internal unsafe int Find(TKey key)
		{
			if (this.AllocatedIndex > 0)
			{
				int bucket = this.GetBucket(in key);
				int entryIdx = this.Buckets[bucket];
				if (entryIdx < this.Capacity)
				{
					int* nextPtrs = this.Next;
					do
					{
						TKey tkey = UnsafeUtility.ReadArrayElement<TKey>((void*)this.Keys, entryIdx);
						if (tkey.Equals(key))
						{
							return entryIdx;
						}
						entryIdx = nextPtrs[entryIdx];
					}
					while (entryIdx < this.Capacity);
					return -1;
				}
			}
			return -1;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00022D44 File Offset: 0x00020F44
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		internal unsafe bool TryGetValue<[global::System.Runtime.CompilerServices.IsUnmanaged] TValue>(TKey key, out TValue item) where TValue : struct, ValueType
		{
			int idx = this.Find(key);
			if (-1 != idx)
			{
				item = UnsafeUtility.ReadArrayElement<TValue>((void*)this.Ptr, idx);
				return true;
			}
			item = default(TValue);
			return false;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00022D7C File Offset: 0x00020F7C
		internal unsafe int TryRemove(TKey key)
		{
			if (this.Capacity == 0)
			{
				return -1;
			}
			int removed = 0;
			int bucket = this.GetBucket(in key);
			int prevEntry = -1;
			int entryIdx = this.Buckets[bucket];
			while (entryIdx >= 0 && entryIdx < this.Capacity)
			{
				TKey tkey = UnsafeUtility.ReadArrayElement<TKey>((void*)this.Keys, entryIdx);
				if (tkey.Equals(key))
				{
					removed++;
					if (prevEntry < 0)
					{
						this.Buckets[bucket] = this.Next[entryIdx];
					}
					else
					{
						this.Next[prevEntry] = this.Next[entryIdx];
					}
					int num = this.Next[entryIdx];
					this.Next[entryIdx] = this.FirstFreeIdx;
					this.FirstFreeIdx = entryIdx;
					break;
				}
				prevEntry = entryIdx;
				entryIdx = this.Next[entryIdx];
			}
			this.Count -= removed;
			if (removed == 0)
			{
				return -1;
			}
			return removed;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00022E68 File Offset: 0x00021068
		internal unsafe bool MoveNextSearch(ref int bucketIndex, ref int nextIndex, out int index)
		{
			int i = bucketIndex;
			int num = this.BucketCapacity;
			while (i < num)
			{
				int idx = this.Buckets[i];
				if (idx != -1)
				{
					index = idx;
					bucketIndex = i + 1;
					nextIndex = this.Next[idx];
					return true;
				}
				i++;
			}
			index = -1;
			bucketIndex = this.BucketCapacity;
			nextIndex = -1;
			return false;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00022EC1 File Offset: 0x000210C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe bool MoveNext(ref int bucketIndex, ref int nextIndex, out int index)
		{
			if (nextIndex != -1)
			{
				index = nextIndex;
				nextIndex = this.Next[nextIndex];
				return true;
			}
			return this.MoveNextSearch(ref bucketIndex, ref nextIndex, out index);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00022EE8 File Offset: 0x000210E8
		internal unsafe NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TKey> result = CollectionHelper.CreateNativeArray<TKey>(this.Count, allocator, NativeArrayOptions.UninitializedMemory);
			int i = 0;
			int count = 0;
			int max = result.Length;
			int capacity = this.BucketCapacity;
			while (i < capacity && count < max)
			{
				for (int bucket = this.Buckets[i]; bucket != -1; bucket = this.Next[bucket])
				{
					result[count++] = UnsafeUtility.ReadArrayElement<TKey>((void*)this.Keys, bucket);
				}
				i++;
			}
			return result;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00022F64 File Offset: 0x00021164
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		internal unsafe NativeArray<TValue> GetValueArray<[global::System.Runtime.CompilerServices.IsUnmanaged] TValue>(AllocatorManager.AllocatorHandle allocator) where TValue : struct, ValueType
		{
			NativeArray<TValue> result = CollectionHelper.CreateNativeArray<TValue>(this.Count, allocator, NativeArrayOptions.UninitializedMemory);
			int i = 0;
			int count = 0;
			int max = result.Length;
			int capacity = this.BucketCapacity;
			while (i < capacity && count < max)
			{
				for (int bucket = this.Buckets[i]; bucket != -1; bucket = this.Next[bucket])
				{
					result[count++] = UnsafeUtility.ReadArrayElement<TValue>((void*)this.Ptr, bucket);
				}
				i++;
			}
			return result;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00022FE0 File Offset: 0x000211E0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		internal unsafe NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays<[global::System.Runtime.CompilerServices.IsUnmanaged] TValue>(AllocatorManager.AllocatorHandle allocator) where TValue : struct, ValueType
		{
			NativeKeyValueArrays<TKey, TValue> result = new NativeKeyValueArrays<TKey, TValue>(this.Count, allocator, NativeArrayOptions.UninitializedMemory);
			int i = 0;
			int count = 0;
			int max = result.Length;
			int capacity = this.BucketCapacity;
			while (i < capacity && count < max)
			{
				for (int bucket = this.Buckets[i]; bucket != -1; bucket = this.Next[bucket])
				{
					result.Keys[count] = UnsafeUtility.ReadArrayElement<TKey>((void*)this.Keys, bucket);
					result.Values[count] = UnsafeUtility.ReadArrayElement<TValue>((void*)this.Ptr, bucket);
					count++;
				}
				i++;
			}
			return result;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002307C File Offset: 0x0002127C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckIndexOutOfBounds(int idx)
		{
			if (idx >= this.Capacity)
			{
				throw new InvalidOperationException(string.Format("Internal HashMap error. idx {0}", idx));
			}
		}

		// Token: 0x040004AE RID: 1198
		[NativeDisableUnsafePtrRestriction]
		internal unsafe byte* Ptr;

		// Token: 0x040004AF RID: 1199
		[NativeDisableUnsafePtrRestriction]
		internal unsafe TKey* Keys;

		// Token: 0x040004B0 RID: 1200
		[NativeDisableUnsafePtrRestriction]
		internal unsafe int* Next;

		// Token: 0x040004B1 RID: 1201
		[NativeDisableUnsafePtrRestriction]
		internal unsafe int* Buckets;

		// Token: 0x040004B2 RID: 1202
		internal int Count;

		// Token: 0x040004B3 RID: 1203
		internal int Capacity;

		// Token: 0x040004B4 RID: 1204
		internal int Log2MinGrowth;

		// Token: 0x040004B5 RID: 1205
		internal int BucketCapacity;

		// Token: 0x040004B6 RID: 1206
		internal int AllocatedIndex;

		// Token: 0x040004B7 RID: 1207
		internal int FirstFreeIdx;

		// Token: 0x040004B8 RID: 1208
		internal int SizeOfTValue;

		// Token: 0x040004B9 RID: 1209
		internal AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x040004BA RID: 1210
		internal const int kMinimumCapacity = 256;

		// Token: 0x0200010C RID: 268
		internal struct Enumerator
		{
			// Token: 0x06000B55 RID: 2901 RVA: 0x0002309D File Offset: 0x0002129D
			internal unsafe Enumerator(HashMapHelper<TKey>* data)
			{
				this.m_Data = data;
				this.m_Index = -1;
				this.m_BucketIndex = 0;
				this.m_NextIndex = -1;
			}

			// Token: 0x06000B56 RID: 2902 RVA: 0x000230BB File Offset: 0x000212BB
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal unsafe bool MoveNext()
			{
				return this.m_Data->MoveNext(ref this.m_BucketIndex, ref this.m_NextIndex, out this.m_Index);
			}

			// Token: 0x06000B57 RID: 2903 RVA: 0x000230DA File Offset: 0x000212DA
			internal void Reset()
			{
				this.m_Index = -1;
				this.m_BucketIndex = 0;
				this.m_NextIndex = -1;
			}

			// Token: 0x06000B58 RID: 2904 RVA: 0x000230F4 File Offset: 0x000212F4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal KVPair<TKey, TValue> GetCurrent<[global::System.Runtime.CompilerServices.IsUnmanaged] TValue>() where TValue : struct, ValueType
			{
				return new KVPair<TKey, TValue>
				{
					m_Data = this.m_Data,
					m_Index = this.m_Index
				};
			}

			// Token: 0x06000B59 RID: 2905 RVA: 0x00023124 File Offset: 0x00021324
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal unsafe TKey GetCurrentKey()
			{
				if (this.m_Index != -1)
				{
					return this.m_Data->Keys[(IntPtr)this.m_Index * (IntPtr)sizeof(TKey) / (IntPtr)sizeof(TKey)];
				}
				return default(TKey);
			}

			// Token: 0x040004BB RID: 1211
			[NativeDisableUnsafePtrRestriction]
			internal unsafe HashMapHelper<TKey>* m_Data;

			// Token: 0x040004BC RID: 1212
			internal int m_Index;

			// Token: 0x040004BD RID: 1213
			internal int m_BucketIndex;

			// Token: 0x040004BE RID: 1214
			internal int m_NextIndex;
		}
	}
}
