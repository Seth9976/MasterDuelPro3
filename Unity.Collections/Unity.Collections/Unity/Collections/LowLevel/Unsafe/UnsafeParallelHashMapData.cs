using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000125 RID: 293
	[GenerateTestsForBurstCompatibility]
	[StructLayout(LayoutKind.Explicit)]
	internal struct UnsafeParallelHashMapData
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00024AB1 File Offset: 0x00022CB1
		internal unsafe int* firstFreeTLS
		{
			get
			{
				return (int*)((byte*)UnsafeUtility.AddressOf<UnsafeParallelHashMapData>(ref this) + 64);
			}
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x000227F4 File Offset: 0x000209F4
		internal static int GetBucketSize(int capacity)
		{
			return capacity * 2;
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00024ABC File Offset: 0x00022CBC
		internal static int GrowCapacity(int capacity)
		{
			if (capacity == 0)
			{
				return 1;
			}
			return capacity * 2;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00024AC8 File Offset: 0x00022CC8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal unsafe static void AllocateHashMap<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue>(int length, int bucketLength, AllocatorManager.AllocatorHandle label, out UnsafeParallelHashMapData* outBuf) where TKey : struct, ValueType where TValue : struct, ValueType
		{
			int maxThreadCount = JobsUtility.ThreadIndexCount;
			UnsafeParallelHashMapData* data = (UnsafeParallelHashMapData*)Memory.Unmanaged.Allocate((long)(64 + 64 * maxThreadCount), 64, label);
			bucketLength = math.ceilpow2(bucketLength);
			data->keyCapacity = length;
			data->bucketCapacityMask = bucketLength - 1;
			int keyOffset;
			int nextOffset;
			int bucketOffset;
			int totalSize = UnsafeParallelHashMapData.CalculateDataSize<TKey, TValue>(length, bucketLength, out keyOffset, out nextOffset, out bucketOffset);
			data->values = (byte*)Memory.Unmanaged.Allocate((long)totalSize, 64, label);
			data->keys = data->values + keyOffset;
			data->next = data->values + nextOffset;
			data->buckets = data->values + bucketOffset;
			outBuf = data;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00024B54 File Offset: 0x00022D54
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal unsafe static void ReallocateHashMap<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue>(UnsafeParallelHashMapData* data, int newCapacity, int newBucketCapacity, AllocatorManager.AllocatorHandle label) where TKey : struct, ValueType where TValue : struct, ValueType
		{
			newBucketCapacity = math.ceilpow2(newBucketCapacity);
			if (data->keyCapacity == newCapacity && data->bucketCapacityMask + 1 == newBucketCapacity)
			{
				return;
			}
			int keyOffset;
			int nextOffset;
			int bucketOffset;
			byte* newData = (byte*)Memory.Unmanaged.Allocate((long)UnsafeParallelHashMapData.CalculateDataSize<TKey, TValue>(newCapacity, newBucketCapacity, out keyOffset, out nextOffset, out bucketOffset), 64, label);
			byte* newKeys = newData + keyOffset;
			byte* newNext = newData + nextOffset;
			byte* newBuckets = newData + bucketOffset;
			UnsafeUtility.MemCpy((void*)newData, (void*)data->values, (long)(data->keyCapacity * UnsafeUtility.SizeOf<TValue>()));
			UnsafeUtility.MemCpy((void*)newKeys, (void*)data->keys, (long)(data->keyCapacity * UnsafeUtility.SizeOf<TKey>()));
			UnsafeUtility.MemCpy((void*)newNext, (void*)data->next, (long)(data->keyCapacity * UnsafeUtility.SizeOf<int>()));
			for (int emptyNext = data->keyCapacity; emptyNext < newCapacity; emptyNext++)
			{
				*(int*)(newNext + (IntPtr)emptyNext * 4) = -1;
			}
			for (int bucket = 0; bucket < newBucketCapacity; bucket++)
			{
				*(int*)(newBuckets + (IntPtr)bucket * 4) = -1;
			}
			for (int bucket2 = 0; bucket2 <= data->bucketCapacityMask; bucket2++)
			{
				int* buckets = (int*)data->buckets;
				int* nextPtrs = (int*)newNext;
				while (buckets[bucket2] >= 0)
				{
					int curEntry = buckets[bucket2];
					buckets[bucket2] = nextPtrs[curEntry];
					TKey tkey = UnsafeUtility.ReadArrayElement<TKey>((void*)data->keys, curEntry);
					int newBucket = tkey.GetHashCode() & (newBucketCapacity - 1);
					nextPtrs[curEntry] = *(int*)(newBuckets + (IntPtr)newBucket * 4);
					*(int*)(newBuckets + (IntPtr)newBucket * 4) = curEntry;
				}
			}
			Memory.Unmanaged.Free<byte>(data->values, label);
			if (data->allocatedIndexLength > data->keyCapacity)
			{
				data->allocatedIndexLength = data->keyCapacity;
			}
			data->values = newData;
			data->keys = newKeys;
			data->next = newNext;
			data->buckets = newBuckets;
			data->keyCapacity = newCapacity;
			data->bucketCapacityMask = newBucketCapacity - 1;
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00024D0D File Offset: 0x00022F0D
		internal unsafe static void DeallocateHashMap(UnsafeParallelHashMapData* data, AllocatorManager.AllocatorHandle allocator)
		{
			Memory.Unmanaged.Free<byte>(data->values, allocator);
			Memory.Unmanaged.Free<UnsafeParallelHashMapData>(data, allocator);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00024D24 File Offset: 0x00022F24
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal static int CalculateDataSize<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue>(int length, int bucketLength, out int keyOffset, out int nextOffset, out int bucketOffset) where TKey : struct, ValueType where TValue : struct, ValueType
		{
			int num = UnsafeUtility.SizeOf<TValue>();
			int sizeOfTKey = UnsafeUtility.SizeOf<TKey>();
			int sizeOfInt = UnsafeUtility.SizeOf<int>();
			int valuesSize = CollectionHelper.Align(num * length, 64);
			int keysSize = CollectionHelper.Align(sizeOfTKey * length, 64);
			int nextSize = CollectionHelper.Align(sizeOfInt * length, 64);
			int bucketSize = CollectionHelper.Align(sizeOfInt * bucketLength, 64);
			int num2 = valuesSize + keysSize + nextSize + bucketSize;
			keyOffset = valuesSize;
			nextOffset = keyOffset + keysSize;
			bucketOffset = nextOffset + nextSize;
			return num2;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00024D8C File Offset: 0x00022F8C
		internal unsafe static bool IsEmpty(UnsafeParallelHashMapData* data)
		{
			if (data->allocatedIndexLength <= 0)
			{
				return true;
			}
			int* bucketArray = (int*)data->buckets;
			int* bucketNext = (int*)data->next;
			int capacityMask = data->bucketCapacityMask;
			for (int i = 0; i <= capacityMask; i++)
			{
				if (bucketArray[i] != -1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00024DD4 File Offset: 0x00022FD4
		internal unsafe static int GetCount(UnsafeParallelHashMapData* data)
		{
			if (data->allocatedIndexLength <= 0)
			{
				return 0;
			}
			int* bucketNext = (int*)data->next;
			int freeListSize = 0;
			int maxThreadCount = JobsUtility.ThreadIndexCount;
			for (int tls = 0; tls < maxThreadCount; tls++)
			{
				for (int freeIdx = data->firstFreeTLS[tls * 16]; freeIdx >= 0; freeIdx = bucketNext[freeIdx])
				{
					freeListSize++;
				}
			}
			return math.min(data->keyCapacity, data->allocatedIndexLength) - freeListSize;
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00024E40 File Offset: 0x00023040
		internal unsafe static bool MoveNextSearch(UnsafeParallelHashMapData* data, ref int bucketIndex, ref int nextIndex, out int index)
		{
			int* bucketArray = (int*)data->buckets;
			int capacityMask = data->bucketCapacityMask;
			for (int i = bucketIndex; i <= capacityMask; i++)
			{
				int idx = bucketArray[i];
				if (idx != -1)
				{
					int* bucketNext = (int*)data->next;
					index = idx;
					bucketIndex = i + 1;
					nextIndex = bucketNext[idx];
					return true;
				}
			}
			index = -1;
			bucketIndex = capacityMask + 1;
			nextIndex = -1;
			return false;
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00024E9C File Offset: 0x0002309C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static bool MoveNext(UnsafeParallelHashMapData* data, ref int bucketIndex, ref int nextIndex, out int index)
		{
			if (nextIndex != -1)
			{
				int* bucketNext = (int*)data->next;
				index = nextIndex;
				nextIndex = bucketNext[nextIndex];
				return true;
			}
			return UnsafeParallelHashMapData.MoveNextSearch(data, ref bucketIndex, ref nextIndex, out index);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00024ED0 File Offset: 0x000230D0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		internal unsafe static void GetKeyArray<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey>(UnsafeParallelHashMapData* data, NativeArray<TKey> result) where TKey : struct, ValueType
		{
			int* bucketArray = (int*)data->buckets;
			int* bucketNext = (int*)data->next;
			int i = 0;
			int count = 0;
			int max = result.Length;
			while (i <= data->bucketCapacityMask && count < max)
			{
				for (int bucket = bucketArray[i]; bucket != -1; bucket = bucketNext[bucket])
				{
					result[count++] = UnsafeUtility.ReadArrayElement<TKey>((void*)data->keys, bucket);
				}
				i++;
			}
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00024F40 File Offset: 0x00023140
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		internal unsafe static void GetValueArray<[global::System.Runtime.CompilerServices.IsUnmanaged] TValue>(UnsafeParallelHashMapData* data, NativeArray<TValue> result) where TValue : struct, ValueType
		{
			int* bucketArray = (int*)data->buckets;
			int* bucketNext = (int*)data->next;
			int i = 0;
			int count = 0;
			int max = result.Length;
			int capacityMask = data->bucketCapacityMask;
			while (i <= capacityMask && count < max)
			{
				for (int bucket = bucketArray[i]; bucket != -1; bucket = bucketNext[bucket])
				{
					result[count++] = UnsafeUtility.ReadArrayElement<TValue>((void*)data->values, bucket);
				}
				i++;
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00024FB4 File Offset: 0x000231B4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal unsafe static void GetKeyValueArrays<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue>(UnsafeParallelHashMapData* data, NativeKeyValueArrays<TKey, TValue> result) where TKey : struct, ValueType where TValue : struct, ValueType
		{
			int* bucketArray = (int*)data->buckets;
			int* bucketNext = (int*)data->next;
			int i = 0;
			int count = 0;
			int max = result.Length;
			int capacityMask = data->bucketCapacityMask;
			while (i <= capacityMask && count < max)
			{
				for (int bucket = bucketArray[i]; bucket != -1; bucket = bucketNext[bucket])
				{
					result.Keys[count] = UnsafeUtility.ReadArrayElement<TKey>((void*)data->keys, bucket);
					result.Values[count] = UnsafeUtility.ReadArrayElement<TValue>((void*)data->values, bucket);
					count++;
				}
				i++;
			}
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00025046 File Offset: 0x00023246
		internal UnsafeParallelHashMapBucketData GetBucketData()
		{
			return new UnsafeParallelHashMapBucketData(this.values, this.keys, this.next, this.buckets, this.bucketCapacityMask);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0002506B File Offset: 0x0002326B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private unsafe static void CheckHashMapReallocateDoesNotShrink(UnsafeParallelHashMapData* data, int newCapacity)
		{
			if (data->keyCapacity > newCapacity)
			{
				throw new InvalidOperationException("Shrinking a hash map is not supported");
			}
		}

		// Token: 0x040004ED RID: 1261
		[FieldOffset(0)]
		internal unsafe byte* values;

		// Token: 0x040004EE RID: 1262
		[FieldOffset(8)]
		internal unsafe byte* keys;

		// Token: 0x040004EF RID: 1263
		[FieldOffset(16)]
		internal unsafe byte* next;

		// Token: 0x040004F0 RID: 1264
		[FieldOffset(24)]
		internal unsafe byte* buckets;

		// Token: 0x040004F1 RID: 1265
		[FieldOffset(32)]
		internal int keyCapacity;

		// Token: 0x040004F2 RID: 1266
		[FieldOffset(36)]
		internal int bucketCapacityMask;

		// Token: 0x040004F3 RID: 1267
		[FieldOffset(40)]
		internal int allocatedIndexLength;

		// Token: 0x040004F4 RID: 1268
		private const int kFirstFreeTLSOffset = 64;

		// Token: 0x040004F5 RID: 1269
		internal const int IntsPerCacheLine = 16;
	}
}
