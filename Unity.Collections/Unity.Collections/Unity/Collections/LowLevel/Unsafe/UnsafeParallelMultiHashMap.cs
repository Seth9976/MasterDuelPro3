using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000138 RID: 312
	[DebuggerTypeProxy(typeof(UnsafeParallelMultiHashMapDebuggerTypeProxy<, >))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct UnsafeParallelMultiHashMap<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue> : INativeDisposable, IDisposable, IEnumerable<KeyValue<TKey, TValue>>, IEnumerable where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType
	{
		// Token: 0x06000D36 RID: 3382 RVA: 0x000292DC File Offset: 0x000274DC
		public UnsafeParallelMultiHashMap(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_AllocatorLabel = allocator;
			UnsafeParallelHashMapData.AllocateHashMap<TKey, TValue>(capacity, capacity * 2, allocator, out this.m_Buffer);
			this.Clear();
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x000292FB File Offset: 0x000274FB
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return !this.IsCreated || UnsafeParallelHashMapData.IsEmpty(this.m_Buffer);
			}
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00029312 File Offset: 0x00027512
		public unsafe readonly int Count()
		{
			if (this.m_Buffer->allocatedIndexLength <= 0)
			{
				return 0;
			}
			return UnsafeParallelHashMapData.GetCount(this.m_Buffer);
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0002932F File Offset: 0x0002752F
		// (set) Token: 0x06000D3A RID: 3386 RVA: 0x0002933C File Offset: 0x0002753C
		public unsafe int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.m_Buffer->keyCapacity;
			}
			set
			{
				UnsafeParallelHashMapData.ReallocateHashMap<TKey, TValue>(this.m_Buffer, value, UnsafeParallelHashMapData.GetBucketSize(value), this.m_AllocatorLabel);
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00029356 File Offset: 0x00027556
		public void Clear()
		{
			UnsafeParallelHashMapBase<TKey, TValue>.Clear(this.m_Buffer);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00029363 File Offset: 0x00027563
		public void Add(TKey key, TValue item)
		{
			UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(this.m_Buffer, key, item, true, this.m_AllocatorLabel);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0002937A File Offset: 0x0002757A
		public int Remove(TKey key)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.Remove(this.m_Buffer, key, true);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00029389 File Offset: 0x00027589
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public void Remove<[global::System.Runtime.CompilerServices.IsUnmanaged] TValueEQ>(TKey key, TValueEQ value) where TValueEQ : struct, ValueType, IEquatable<TValueEQ>
		{
			UnsafeParallelHashMapBase<TKey, TValueEQ>.RemoveKeyValue<TValueEQ>(this.m_Buffer, key, value);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00029398 File Offset: 0x00027598
		public void Remove(NativeParallelMultiHashMapIterator<TKey> it)
		{
			UnsafeParallelHashMapBase<TKey, TValue>.Remove(this.m_Buffer, it);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x000293A6 File Offset: 0x000275A6
		public readonly bool TryGetFirstValue(TKey key, out TValue item, out NativeParallelMultiHashMapIterator<TKey> it)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(this.m_Buffer, key, out item, out it);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x000293B6 File Offset: 0x000275B6
		public readonly bool TryGetNextValue(out TValue item, ref NativeParallelMultiHashMapIterator<TKey> it)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetNextValueAtomic(this.m_Buffer, out item, ref it);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x000293C8 File Offset: 0x000275C8
		public readonly bool ContainsKey(TKey key)
		{
			TValue temp0;
			NativeParallelMultiHashMapIterator<TKey> temp;
			return this.TryGetFirstValue(key, out temp0, out temp);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x000293E0 File Offset: 0x000275E0
		public readonly int CountValuesForKey(TKey key)
		{
			TValue value;
			NativeParallelMultiHashMapIterator<TKey> iterator;
			if (!this.TryGetFirstValue(key, out value, out iterator))
			{
				return 0;
			}
			int count = 1;
			while (this.TryGetNextValue(out value, ref iterator))
			{
				count++;
			}
			return count;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00029411 File Offset: 0x00027611
		public bool SetValue(TValue item, NativeParallelMultiHashMapIterator<TKey> it)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.SetValue(this.m_Buffer, ref it, ref item);
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x00029422 File Offset: 0x00027622
		public readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Buffer != null;
			}
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00029431 File Offset: 0x00027631
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			UnsafeParallelHashMapData.DeallocateHashMap(this.m_Buffer, this.m_AllocatorLabel);
			this.m_Buffer = null;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00029458 File Offset: 0x00027658
		public JobHandle Dispose(JobHandle inputDeps)
		{
			if (!this.IsCreated)
			{
				return inputDeps;
			}
			JobHandle jobHandle = new UnsafeParallelHashMapDisposeJob
			{
				Data = this.m_Buffer,
				Allocator = this.m_AllocatorLabel
			}.Schedule(inputDeps);
			this.m_Buffer = null;
			return jobHandle;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x000294A0 File Offset: 0x000276A0
		public readonly NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TKey> result = CollectionHelper.CreateNativeArray<TKey>(this.Count(), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetKeyArray<TKey>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x000294C8 File Offset: 0x000276C8
		public readonly NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TValue> result = CollectionHelper.CreateNativeArray<TValue>(this.Count(), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetValueArray<TValue>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x000294F0 File Offset: 0x000276F0
		public readonly NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			NativeKeyValueArrays<TKey, TValue> result = new NativeKeyValueArrays<TKey, TValue>(this.Count(), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetKeyValueArrays<TKey, TValue>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0002951C File Offset: 0x0002771C
		public UnsafeParallelMultiHashMap<TKey, TValue>.Enumerator GetValuesForKey(TKey key)
		{
			return new UnsafeParallelMultiHashMap<TKey, TValue>.Enumerator
			{
				hashmap = this,
				key = key,
				isFirst = true
			};
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00029550 File Offset: 0x00027750
		public UnsafeParallelMultiHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			UnsafeParallelMultiHashMap<TKey, TValue>.ParallelWriter writer;
			writer.m_ThreadIndex = 0;
			writer.m_Buffer = this.m_Buffer;
			return writer;
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00029574 File Offset: 0x00027774
		public UnsafeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator GetEnumerator()
		{
			return new UnsafeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_Buffer)
			};
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<TKey, TValue>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0002959C File Offset: 0x0002779C
		public UnsafeParallelMultiHashMap<TKey, TValue>.ReadOnly AsReadOnly()
		{
			return new UnsafeParallelMultiHashMap<TKey, TValue>.ReadOnly(this);
		}

		// Token: 0x04000512 RID: 1298
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x04000513 RID: 1299
		internal AllocatorManager.AllocatorHandle m_AllocatorLabel;

		// Token: 0x02000139 RID: 313
		public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x06000D51 RID: 3409 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x06000D52 RID: 3410 RVA: 0x000295AC File Offset: 0x000277AC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				if (this.isFirst)
				{
					this.isFirst = false;
					return this.hashmap.TryGetFirstValue(this.key, out this.value, out this.iterator);
				}
				return this.hashmap.TryGetNextValue(out this.value, ref this.iterator);
			}

			// Token: 0x06000D53 RID: 3411 RVA: 0x000295FD File Offset: 0x000277FD
			public void Reset()
			{
				this.isFirst = true;
			}

			// Token: 0x1700018E RID: 398
			// (get) Token: 0x06000D54 RID: 3412 RVA: 0x00029606 File Offset: 0x00027806
			public TValue Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.value;
				}
			}

			// Token: 0x1700018F RID: 399
			// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0002960E File Offset: 0x0002780E
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000D56 RID: 3414 RVA: 0x0002961B File Offset: 0x0002781B
			public UnsafeParallelMultiHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x04000514 RID: 1300
			internal UnsafeParallelMultiHashMap<TKey, TValue> hashmap;

			// Token: 0x04000515 RID: 1301
			internal TKey key;

			// Token: 0x04000516 RID: 1302
			internal bool isFirst;

			// Token: 0x04000517 RID: 1303
			private TValue value;

			// Token: 0x04000518 RID: 1304
			private NativeParallelMultiHashMapIterator<TKey> iterator;
		}

		// Token: 0x0200013A RID: 314
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ParallelWriter
		{
			// Token: 0x17000190 RID: 400
			// (get) Token: 0x06000D57 RID: 3415 RVA: 0x00029623 File Offset: 0x00027823
			public unsafe readonly int Capacity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Buffer->keyCapacity;
				}
			}

			// Token: 0x06000D58 RID: 3416 RVA: 0x00029630 File Offset: 0x00027830
			public void Add(TKey key, TValue item)
			{
				UnsafeParallelHashMapBase<TKey, TValue>.AddAtomicMulti(this.m_Buffer, key, item, this.m_ThreadIndex);
			}

			// Token: 0x04000519 RID: 1305
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeParallelHashMapData* m_Buffer;

			// Token: 0x0400051A RID: 1306
			[NativeSetThreadIndex]
			internal int m_ThreadIndex;
		}

		// Token: 0x0200013B RID: 315
		public struct KeyValueEnumerator : IEnumerator<KeyValue<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x06000D59 RID: 3417 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x06000D5A RID: 3418 RVA: 0x00029645 File Offset: 0x00027845
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000D5B RID: 3419 RVA: 0x00029652 File Offset: 0x00027852
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x17000191 RID: 401
			// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0002965F File Offset: 0x0002785F
			public KeyValue<TKey, TValue> Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Enumerator.GetCurrent<TKey, TValue>();
				}
			}

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0002966C File Offset: 0x0002786C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0400051B RID: 1307
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}

		// Token: 0x0200013C RID: 316
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ReadOnly : IEnumerable<KeyValue<TKey, TValue>>, IEnumerable
		{
			// Token: 0x06000D5E RID: 3422 RVA: 0x00029679 File Offset: 0x00027879
			internal ReadOnly(UnsafeParallelMultiHashMap<TKey, TValue> container)
			{
				this.m_MultiHashMapData = container;
			}

			// Token: 0x17000193 RID: 403
			// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00029682 File Offset: 0x00027882
			public readonly bool IsCreated
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_MultiHashMapData.IsCreated;
				}
			}

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0002968F File Offset: 0x0002788F
			public readonly bool IsEmpty
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return !this.IsCreated || this.m_MultiHashMapData.IsEmpty;
				}
			}

			// Token: 0x06000D61 RID: 3425 RVA: 0x000296A6 File Offset: 0x000278A6
			public readonly int Count()
			{
				return this.m_MultiHashMapData.Count();
			}

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x06000D62 RID: 3426 RVA: 0x000296B3 File Offset: 0x000278B3
			public readonly int Capacity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_MultiHashMapData.Capacity;
				}
			}

			// Token: 0x06000D63 RID: 3427 RVA: 0x000296C0 File Offset: 0x000278C0
			public readonly bool TryGetFirstValue(TKey key, out TValue item, out NativeParallelMultiHashMapIterator<TKey> it)
			{
				return this.m_MultiHashMapData.TryGetFirstValue(key, out item, out it);
			}

			// Token: 0x06000D64 RID: 3428 RVA: 0x000296D0 File Offset: 0x000278D0
			public readonly bool TryGetNextValue(out TValue item, ref NativeParallelMultiHashMapIterator<TKey> it)
			{
				return this.m_MultiHashMapData.TryGetNextValue(out item, ref it);
			}

			// Token: 0x06000D65 RID: 3429 RVA: 0x000296DF File Offset: 0x000278DF
			public readonly bool ContainsKey(TKey key)
			{
				return this.m_MultiHashMapData.ContainsKey(key);
			}

			// Token: 0x06000D66 RID: 3430 RVA: 0x000296ED File Offset: 0x000278ED
			public readonly NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
			{
				return this.m_MultiHashMapData.GetKeyArray(allocator);
			}

			// Token: 0x06000D67 RID: 3431 RVA: 0x000296FB File Offset: 0x000278FB
			public readonly NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
			{
				return this.m_MultiHashMapData.GetValueArray(allocator);
			}

			// Token: 0x06000D68 RID: 3432 RVA: 0x00029709 File Offset: 0x00027909
			public readonly NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
			{
				return this.m_MultiHashMapData.GetKeyValueArrays(allocator);
			}

			// Token: 0x06000D69 RID: 3433 RVA: 0x00029718 File Offset: 0x00027918
			public UnsafeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator GetEnumerator()
			{
				return new UnsafeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator
				{
					m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_MultiHashMapData.m_Buffer)
				};
			}

			// Token: 0x06000D6A RID: 3434 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<TKey, TValue>>.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000D6B RID: 3435 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400051C RID: 1308
			internal UnsafeParallelMultiHashMap<TKey, TValue> m_MultiHashMapData;
		}
	}
}
