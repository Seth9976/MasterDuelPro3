using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000128 RID: 296
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	internal struct UnsafeParallelHashMapBase<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue> where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType
	{
		// Token: 0x06000C46 RID: 3142 RVA: 0x000250A4 File Offset: 0x000232A4
		internal unsafe static void Clear(UnsafeParallelHashMapData* data)
		{
			UnsafeUtility.MemSet((void*)data->buckets, byte.MaxValue, (long)((data->bucketCapacityMask + 1) * 4));
			UnsafeUtility.MemSet((void*)data->next, byte.MaxValue, (long)(data->keyCapacity * 4));
			int maxThreadCount = JobsUtility.ThreadIndexCount;
			for (int tls = 0; tls < maxThreadCount; tls++)
			{
				data->firstFreeTLS[tls * 16] = -1;
			}
			data->allocatedIndexLength = 0;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00025110 File Offset: 0x00023310
		internal unsafe static int AllocEntry(UnsafeParallelHashMapData* data, int threadIndex)
		{
			int* nextPtrs = (int*)data->next;
			int idx;
			int other;
			for (;;)
			{
				idx = Volatile.Read(ref data->firstFreeTLS[threadIndex * 16]);
				if (idx != -3)
				{
					if (idx < 0)
					{
						Interlocked.Exchange(ref data->firstFreeTLS[threadIndex * 16], -2);
						if (data->allocatedIndexLength < data->keyCapacity)
						{
							idx = Interlocked.Add(ref data->allocatedIndexLength, 16) - 16;
							if (idx < data->keyCapacity - 1)
							{
								break;
							}
							if (idx == data->keyCapacity - 1)
							{
								goto Block_6;
							}
						}
						Interlocked.Exchange(ref data->firstFreeTLS[threadIndex * 16], -1);
						int maxThreadCount = JobsUtility.ThreadIndexCount;
						bool again = true;
						while (again)
						{
							again = false;
							for (other = (threadIndex + 1) % maxThreadCount; other != threadIndex; other = (other + 1) % maxThreadCount)
							{
								do
								{
									idx = Volatile.Read(ref data->firstFreeTLS[other * 16]);
								}
								while (idx == -3 || (idx >= 0 && Interlocked.CompareExchange(ref data->firstFreeTLS[other * 16], -3, idx) != idx));
								if (idx == -2)
								{
									again = true;
								}
								else if (idx >= 0)
								{
									goto Block_10;
								}
							}
						}
					}
					if (Interlocked.CompareExchange(ref data->firstFreeTLS[threadIndex * 16], -3, idx) == idx)
					{
						goto Block_11;
					}
				}
			}
			int count = math.min(16, data->keyCapacity - idx);
			for (int i = 1; i < count; i++)
			{
				nextPtrs[idx + i] = idx + i + 1;
			}
			nextPtrs[idx + count - 1] = -1;
			nextPtrs[idx] = -1;
			Interlocked.Exchange(ref data->firstFreeTLS[threadIndex * 16], idx + 1);
			return idx;
			Block_6:
			Interlocked.Exchange(ref data->firstFreeTLS[threadIndex * 16], -1);
			return idx;
			Block_10:
			Interlocked.Exchange(ref data->firstFreeTLS[other * 16], nextPtrs[idx]);
			nextPtrs[idx] = -1;
			return idx;
			Block_11:
			Interlocked.Exchange(ref data->firstFreeTLS[threadIndex * 16], nextPtrs[idx]);
			nextPtrs[idx] = -1;
			return idx;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x000252F4 File Offset: 0x000234F4
		internal unsafe static void FreeEntry(UnsafeParallelHashMapData* data, int idx, int threadIndex)
		{
			int* nextPtrs = (int*)data->next;
			for (;;)
			{
				int next = Volatile.Read(ref data->firstFreeTLS[threadIndex * 16]);
				if (next != -3)
				{
					nextPtrs[idx] = next;
					if (Interlocked.CompareExchange(ref data->firstFreeTLS[threadIndex * 16], idx, next) == next)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00025344 File Offset: 0x00023544
		internal unsafe static bool TryAddAtomic(UnsafeParallelHashMapData* data, TKey key, TValue item, int threadIndex)
		{
			TValue tempItem;
			NativeParallelMultiHashMapIterator<TKey> tempIt;
			if (UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(data, key, out tempItem, out tempIt))
			{
				return false;
			}
			int idx = UnsafeParallelHashMapBase<TKey, TValue>.AllocEntry(data, threadIndex);
			UnsafeUtility.WriteArrayElement<TKey>((void*)data->keys, idx, key);
			UnsafeUtility.WriteArrayElement<TValue>((void*)data->values, idx, item);
			int bucket = key.GetHashCode() & data->bucketCapacityMask;
			int* buckets = (int*)data->buckets;
			if (Interlocked.CompareExchange(ref buckets[bucket], idx, -1) != -1)
			{
				int* nextPtrs = (int*)data->next;
				for (;;)
				{
					int next = buckets[bucket];
					nextPtrs[idx] = next;
					if (UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(data, key, out tempItem, out tempIt))
					{
						break;
					}
					if (Interlocked.CompareExchange(ref buckets[bucket], idx, next) == next)
					{
						return true;
					}
				}
				UnsafeParallelHashMapBase<TKey, TValue>.FreeEntry(data, idx, threadIndex);
				return false;
			}
			return true;
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000253FC File Offset: 0x000235FC
		internal unsafe static void AddAtomicMulti(UnsafeParallelHashMapData* data, TKey key, TValue item, int threadIndex)
		{
			int idx = UnsafeParallelHashMapBase<TKey, TValue>.AllocEntry(data, threadIndex);
			UnsafeUtility.WriteArrayElement<TKey>((void*)data->keys, idx, key);
			UnsafeUtility.WriteArrayElement<TValue>((void*)data->values, idx, item);
			int bucket = key.GetHashCode() & data->bucketCapacityMask;
			int* buckets = (int*)data->buckets;
			int* nextPtrs = (int*)data->next;
			int nextPtr;
			do
			{
				nextPtr = buckets[bucket];
				nextPtrs[idx] = nextPtr;
			}
			while (Interlocked.CompareExchange(ref buckets[bucket], idx, nextPtr) != nextPtr);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00025470 File Offset: 0x00023670
		internal unsafe static bool TryAdd(UnsafeParallelHashMapData* data, TKey key, TValue item, bool isMultiHashMap, AllocatorManager.AllocatorHandle allocation)
		{
			TValue tempItem;
			NativeParallelMultiHashMapIterator<TKey> tempIt;
			if (isMultiHashMap || !UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(data, key, out tempItem, out tempIt))
			{
				int idx;
				int* nextPtrs;
				if (data->allocatedIndexLength >= data->keyCapacity && *data->firstFreeTLS < 0)
				{
					int maxThreadCount = JobsUtility.ThreadIndexCount;
					for (int tls = 1; tls < maxThreadCount; tls++)
					{
						if (data->firstFreeTLS[tls * 16] >= 0)
						{
							idx = data->firstFreeTLS[tls * 16];
							nextPtrs = (int*)data->next;
							data->firstFreeTLS[tls * 16] = nextPtrs[idx];
							nextPtrs[idx] = -1;
							*data->firstFreeTLS = idx;
							break;
						}
					}
					if (*data->firstFreeTLS < 0)
					{
						int newCap = UnsafeParallelHashMapData.GrowCapacity(data->keyCapacity);
						UnsafeParallelHashMapData.ReallocateHashMap<TKey, TValue>(data, newCap, UnsafeParallelHashMapData.GetBucketSize(newCap), allocation);
					}
				}
				idx = *data->firstFreeTLS;
				if (idx >= 0)
				{
					*data->firstFreeTLS = *(int*)(data->next + (IntPtr)idx * 4);
				}
				else
				{
					int allocatedIndexLength = data->allocatedIndexLength;
					data->allocatedIndexLength = allocatedIndexLength + 1;
					idx = allocatedIndexLength;
				}
				UnsafeUtility.WriteArrayElement<TKey>((void*)data->keys, idx, key);
				UnsafeUtility.WriteArrayElement<TValue>((void*)data->values, idx, item);
				int bucket = key.GetHashCode() & data->bucketCapacityMask;
				int* buckets = (int*)data->buckets;
				nextPtrs = (int*)data->next;
				nextPtrs[idx] = buckets[bucket];
				buckets[bucket] = idx;
				return true;
			}
			return false;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x000255D4 File Offset: 0x000237D4
		internal unsafe static int Remove(UnsafeParallelHashMapData* data, TKey key, bool isMultiHashMap)
		{
			if (data->keyCapacity == 0)
			{
				return 0;
			}
			int removed = 0;
			int* buckets = (int*)data->buckets;
			int* nextPtrs = (int*)data->next;
			int bucket = key.GetHashCode() & data->bucketCapacityMask;
			int prevEntry = -1;
			int entryIdx = buckets[bucket];
			while (entryIdx >= 0 && entryIdx < data->keyCapacity)
			{
				TKey tkey = UnsafeUtility.ReadArrayElement<TKey>((void*)data->keys, entryIdx);
				if (tkey.Equals(key))
				{
					removed++;
					if (prevEntry < 0)
					{
						buckets[bucket] = nextPtrs[entryIdx];
					}
					else
					{
						nextPtrs[prevEntry] = nextPtrs[entryIdx];
					}
					int num = nextPtrs[entryIdx];
					nextPtrs[entryIdx] = *data->firstFreeTLS;
					*data->firstFreeTLS = entryIdx;
					entryIdx = num;
					if (!isMultiHashMap)
					{
						break;
					}
				}
				else
				{
					prevEntry = entryIdx;
					entryIdx = nextPtrs[entryIdx];
				}
			}
			return removed;
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x000256B0 File Offset: 0x000238B0
		internal unsafe static void Remove(UnsafeParallelHashMapData* data, NativeParallelMultiHashMapIterator<TKey> it)
		{
			int* buckets = (int*)data->buckets;
			int* nextPtrs = (int*)data->next;
			int bucket = it.key.GetHashCode() & data->bucketCapacityMask;
			int entryIdx = buckets[bucket];
			if (entryIdx == it.EntryIndex)
			{
				buckets[bucket] = nextPtrs[entryIdx];
			}
			else
			{
				while (entryIdx >= 0 && nextPtrs[entryIdx] != it.EntryIndex)
				{
					entryIdx = nextPtrs[entryIdx];
				}
				nextPtrs[entryIdx] = nextPtrs[it.EntryIndex];
			}
			nextPtrs[it.EntryIndex] = *data->firstFreeTLS;
			*data->firstFreeTLS = it.EntryIndex;
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00025758 File Offset: 0x00023958
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		internal unsafe static void RemoveKeyValue<[global::System.Runtime.CompilerServices.IsUnmanaged] TValueEQ>(UnsafeParallelHashMapData* data, TKey key, TValueEQ value) where TValueEQ : struct, ValueType, IEquatable<TValueEQ>
		{
			if (data->keyCapacity == 0)
			{
				return;
			}
			int* buckets = (int*)data->buckets;
			uint keyCapacity = (uint)data->keyCapacity;
			int* prevNextPtr = buckets + (key.GetHashCode() & data->bucketCapacityMask);
			int entryIdx = *prevNextPtr;
			if (entryIdx >= (int)keyCapacity)
			{
				return;
			}
			int* nextPtrs = (int*)data->next;
			byte* keys = data->keys;
			byte* values = data->values;
			int* firstFreeTLS = data->firstFreeTLS;
			for (;;)
			{
				TKey tkey = UnsafeUtility.ReadArrayElement<TKey>((void*)keys, entryIdx);
				if (!tkey.Equals(key))
				{
					goto IL_00AE;
				}
				TValueEQ tvalueEQ = UnsafeUtility.ReadArrayElement<TValueEQ>((void*)values, entryIdx);
				if (!tvalueEQ.Equals(value))
				{
					goto IL_00AE;
				}
				int nextIdx = nextPtrs[entryIdx];
				nextPtrs[entryIdx] = *firstFreeTLS;
				*firstFreeTLS = entryIdx;
				entryIdx = (*prevNextPtr = nextIdx);
				IL_00B9:
				if (entryIdx >= (int)keyCapacity)
				{
					break;
				}
				continue;
				IL_00AE:
				prevNextPtr = nextPtrs + entryIdx;
				entryIdx = *prevNextPtr;
				goto IL_00B9;
			}
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00025824 File Offset: 0x00023A24
		internal unsafe static bool TryGetFirstValueAtomic(UnsafeParallelHashMapData* data, TKey key, out TValue item, out NativeParallelMultiHashMapIterator<TKey> it)
		{
			it.key = key;
			if (data->allocatedIndexLength <= 0)
			{
				it.EntryIndex = (it.NextEntryIndex = -1);
				item = default(TValue);
				return false;
			}
			int* buckets = (int*)data->buckets;
			int bucket = key.GetHashCode() & data->bucketCapacityMask;
			it.EntryIndex = (it.NextEntryIndex = buckets[bucket]);
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetNextValueAtomic(data, out item, ref it);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00025894 File Offset: 0x00023A94
		internal unsafe static bool TryGetNextValueAtomic(UnsafeParallelHashMapData* data, out TValue item, ref NativeParallelMultiHashMapIterator<TKey> it)
		{
			int entryIdx = it.NextEntryIndex;
			it.NextEntryIndex = -1;
			it.EntryIndex = -1;
			item = default(TValue);
			if (entryIdx < 0 || entryIdx >= data->keyCapacity)
			{
				return false;
			}
			int* nextPtrs = (int*)data->next;
			do
			{
				TKey tkey = UnsafeUtility.ReadArrayElement<TKey>((void*)data->keys, entryIdx);
				if (tkey.Equals(it.key))
				{
					goto Block_3;
				}
				entryIdx = nextPtrs[entryIdx];
			}
			while (entryIdx >= 0 && entryIdx < data->keyCapacity);
			return false;
			Block_3:
			it.NextEntryIndex = nextPtrs[entryIdx];
			it.EntryIndex = entryIdx;
			item = UnsafeUtility.ReadArrayElement<TValue>((void*)data->values, entryIdx);
			return true;
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00025938 File Offset: 0x00023B38
		internal unsafe static bool SetValue(UnsafeParallelHashMapData* data, ref NativeParallelMultiHashMapIterator<TKey> it, ref TValue item)
		{
			int entryIdx = it.EntryIndex;
			if (entryIdx < 0 || entryIdx >= data->keyCapacity)
			{
				return false;
			}
			UnsafeUtility.WriteArrayElement<TValue>((void*)data->values, entryIdx, item);
			return true;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002596E File Offset: 0x00023B6E
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckOutOfCapacity(int idx, int keyCapacity)
		{
			if (idx >= keyCapacity)
			{
				throw new InvalidOperationException(string.Format("nextPtr idx {0} beyond capacity {1}", idx, keyCapacity));
			}
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00025990 File Offset: 0x00023B90
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private unsafe static void CheckIndexOutOfBounds(UnsafeParallelHashMapData* data, int idx)
		{
			if (idx < 0 || idx >= data->keyCapacity)
			{
				throw new InvalidOperationException("Internal HashMap error");
			}
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x000259AA File Offset: 0x00023BAA
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void ThrowFull()
		{
			throw new InvalidOperationException("HashMap is full");
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x000259B6 File Offset: 0x00023BB6
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void ThrowInvalidIterator()
		{
			throw new InvalidOperationException("Invalid iterator passed to HashMap remove");
		}

		// Token: 0x040004F9 RID: 1273
		private const int SentinelRefilling = -2;

		// Token: 0x040004FA RID: 1274
		private const int SentinelSwapInProgress = -3;
	}
}
