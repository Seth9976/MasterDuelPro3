using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200012B RID: 299
	[DebuggerDisplay("Count = {Count()}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(UnsafeParallelHashMapDebuggerTypeProxy<, >))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct UnsafeParallelHashMap<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue> : INativeDisposable, IDisposable, IEnumerable<KeyValue<TKey, TValue>>, IEnumerable where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType
	{
		// Token: 0x06000C5F RID: 3167 RVA: 0x00025B56 File Offset: 0x00023D56
		public UnsafeParallelHashMap(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_AllocatorLabel = allocator;
			UnsafeParallelHashMapData.AllocateHashMap<TKey, TValue>(capacity, capacity * 2, allocator, out this.m_Buffer);
			this.Clear();
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00025B75 File Offset: 0x00023D75
		public readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Buffer != null;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00025B84 File Offset: 0x00023D84
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return !this.IsCreated || UnsafeParallelHashMapData.IsEmpty(this.m_Buffer);
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00025B9B File Offset: 0x00023D9B
		public readonly int Count()
		{
			return UnsafeParallelHashMapData.GetCount(this.m_Buffer);
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x00025BA8 File Offset: 0x00023DA8
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x00025BB5 File Offset: 0x00023DB5
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

		// Token: 0x06000C65 RID: 3173 RVA: 0x00025BCF File Offset: 0x00023DCF
		public void Clear()
		{
			UnsafeParallelHashMapBase<TKey, TValue>.Clear(this.m_Buffer);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00025BDC File Offset: 0x00023DDC
		public bool TryAdd(TKey key, TValue item)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(this.m_Buffer, key, item, false, this.m_AllocatorLabel);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00025BF2 File Offset: 0x00023DF2
		public void Add(TKey key, TValue item)
		{
			UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(this.m_Buffer, key, item, false, this.m_AllocatorLabel);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00025C09 File Offset: 0x00023E09
		public bool Remove(TKey key)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.Remove(this.m_Buffer, key, false) != 0;
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00025C1C File Offset: 0x00023E1C
		public bool TryGetValue(TKey key, out TValue item)
		{
			NativeParallelMultiHashMapIterator<TKey> tempIt;
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(this.m_Buffer, key, out item, out tempIt);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00025C38 File Offset: 0x00023E38
		public bool ContainsKey(TKey key)
		{
			TValue tempValue;
			NativeParallelMultiHashMapIterator<TKey> tempIt;
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(this.m_Buffer, key, out tempValue, out tempIt);
		}

		// Token: 0x17000177 RID: 375
		public TValue this[TKey key]
		{
			get
			{
				TValue res;
				this.TryGetValue(key, out res);
				return res;
			}
			set
			{
				TValue item;
				NativeParallelMultiHashMapIterator<TKey> iterator;
				if (UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(this.m_Buffer, key, out item, out iterator))
				{
					UnsafeParallelHashMapBase<TKey, TValue>.SetValue(this.m_Buffer, ref iterator, ref value);
					return;
				}
				UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(this.m_Buffer, key, value, false, this.m_AllocatorLabel);
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00025CB5 File Offset: 0x00023EB5
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			UnsafeParallelHashMapData.DeallocateHashMap(this.m_Buffer, this.m_AllocatorLabel);
			this.m_Buffer = null;
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00025CDC File Offset: 0x00023EDC
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

		// Token: 0x06000C6F RID: 3183 RVA: 0x00025D24 File Offset: 0x00023F24
		public NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TKey> result = CollectionHelper.CreateNativeArray<TKey>(UnsafeParallelHashMapData.GetCount(this.m_Buffer), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetKeyArray<TKey>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00025D54 File Offset: 0x00023F54
		public NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TValue> result = CollectionHelper.CreateNativeArray<TValue>(UnsafeParallelHashMapData.GetCount(this.m_Buffer), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetValueArray<TValue>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00025D84 File Offset: 0x00023F84
		public NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			NativeKeyValueArrays<TKey, TValue> result = new NativeKeyValueArrays<TKey, TValue>(UnsafeParallelHashMapData.GetCount(this.m_Buffer), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetKeyValueArrays<TKey, TValue>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00025DB4 File Offset: 0x00023FB4
		public UnsafeParallelHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			UnsafeParallelHashMap<TKey, TValue>.ParallelWriter writer;
			writer.m_ThreadIndex = 0;
			writer.m_Buffer = this.m_Buffer;
			return writer;
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00025DD7 File Offset: 0x00023FD7
		public UnsafeParallelHashMap<TKey, TValue>.ReadOnly AsReadOnly()
		{
			return new UnsafeParallelHashMap<TKey, TValue>.ReadOnly(this);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00025DE4 File Offset: 0x00023FE4
		public UnsafeParallelHashMap<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new UnsafeParallelHashMap<TKey, TValue>.Enumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_Buffer)
			};
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<TKey, TValue>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000502 RID: 1282
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x04000503 RID: 1283
		internal AllocatorManager.AllocatorHandle m_AllocatorLabel;

		// Token: 0x0200012C RID: 300
		[DebuggerDisplay("Count = {m_HashMapData.Count()}, Capacity = {m_HashMapData.Capacity}, IsCreated = {m_HashMapData.IsCreated}, IsEmpty = {IsEmpty}")]
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ReadOnly : IEnumerable<KeyValue<TKey, TValue>>, IEnumerable
		{
			// Token: 0x06000C77 RID: 3191 RVA: 0x00025E0C File Offset: 0x0002400C
			internal ReadOnly(UnsafeParallelHashMap<TKey, TValue> hashMapData)
			{
				this.m_HashMapData = hashMapData;
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00025E15 File Offset: 0x00024015
			public readonly bool IsCreated
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_HashMapData.IsCreated;
				}
			}

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00025E22 File Offset: 0x00024022
			public readonly bool IsEmpty
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return !this.IsCreated || this.m_HashMapData.IsEmpty;
				}
			}

			// Token: 0x06000C7A RID: 3194 RVA: 0x00025E39 File Offset: 0x00024039
			public readonly int Count()
			{
				return this.m_HashMapData.Count();
			}

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00025E46 File Offset: 0x00024046
			public readonly int Capacity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_HashMapData.Capacity;
				}
			}

			// Token: 0x06000C7C RID: 3196 RVA: 0x00025E54 File Offset: 0x00024054
			public readonly bool TryGetValue(TKey key, out TValue item)
			{
				UnsafeParallelHashMap<TKey, TValue> hashMapData = this.m_HashMapData;
				return hashMapData.TryGetValue(key, out item);
			}

			// Token: 0x06000C7D RID: 3197 RVA: 0x00025E74 File Offset: 0x00024074
			public readonly bool ContainsKey(TKey key)
			{
				UnsafeParallelHashMap<TKey, TValue> hashMapData = this.m_HashMapData;
				return hashMapData.ContainsKey(key);
			}

			// Token: 0x1700017B RID: 379
			public readonly TValue this[TKey key]
			{
				get
				{
					UnsafeParallelHashMap<TKey, TValue> hashMapData = this.m_HashMapData;
					TValue res;
					if (hashMapData.TryGetValue(key, out res))
					{
						return res;
					}
					return default(TValue);
				}
			}

			// Token: 0x06000C7F RID: 3199 RVA: 0x00025EBC File Offset: 0x000240BC
			public readonly NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
			{
				UnsafeParallelHashMap<TKey, TValue> hashMapData = this.m_HashMapData;
				return hashMapData.GetKeyArray(allocator);
			}

			// Token: 0x06000C80 RID: 3200 RVA: 0x00025ED8 File Offset: 0x000240D8
			public readonly NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
			{
				UnsafeParallelHashMap<TKey, TValue> hashMapData = this.m_HashMapData;
				return hashMapData.GetValueArray(allocator);
			}

			// Token: 0x06000C81 RID: 3201 RVA: 0x00025EF4 File Offset: 0x000240F4
			public readonly NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
			{
				UnsafeParallelHashMap<TKey, TValue> hashMapData = this.m_HashMapData;
				return hashMapData.GetKeyValueArrays(allocator);
			}

			// Token: 0x06000C82 RID: 3202 RVA: 0x00018715 File Offset: 0x00016915
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			private readonly void ThrowKeyNotPresent(TKey key)
			{
				throw new ArgumentException(string.Format("Key: {0} is not present in the NativeParallelHashMap.", key));
			}

			// Token: 0x06000C83 RID: 3203 RVA: 0x00025F10 File Offset: 0x00024110
			public readonly UnsafeParallelHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new UnsafeParallelHashMap<TKey, TValue>.Enumerator
				{
					m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_HashMapData.m_Buffer)
				};
			}

			// Token: 0x06000C84 RID: 3204 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<TKey, TValue>>.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000C85 RID: 3205 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x04000504 RID: 1284
			internal UnsafeParallelHashMap<TKey, TValue> m_HashMapData;
		}

		// Token: 0x0200012D RID: 301
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ParallelWriter
		{
			// Token: 0x1700017C RID: 380
			// (get) Token: 0x06000C86 RID: 3206 RVA: 0x00025F3D File Offset: 0x0002413D
			public int ThreadIndex
			{
				get
				{
					return this.m_ThreadIndex;
				}
			}

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x06000C87 RID: 3207 RVA: 0x00025F45 File Offset: 0x00024145
			public unsafe readonly int Capacity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Buffer->keyCapacity;
				}
			}

			// Token: 0x06000C88 RID: 3208 RVA: 0x00025F52 File Offset: 0x00024152
			public bool TryAdd(TKey key, TValue item)
			{
				return UnsafeParallelHashMapBase<TKey, TValue>.TryAddAtomic(this.m_Buffer, key, item, this.m_ThreadIndex);
			}

			// Token: 0x06000C89 RID: 3209 RVA: 0x00025F67 File Offset: 0x00024167
			internal bool TryAdd(TKey key, TValue item, int threadIndexOverride)
			{
				return UnsafeParallelHashMapBase<TKey, TValue>.TryAddAtomic(this.m_Buffer, key, item, threadIndexOverride);
			}

			// Token: 0x04000505 RID: 1285
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeParallelHashMapData* m_Buffer;

			// Token: 0x04000506 RID: 1286
			[NativeSetThreadIndex]
			internal int m_ThreadIndex;
		}

		// Token: 0x0200012E RID: 302
		public struct Enumerator : IEnumerator<KeyValue<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x06000C8A RID: 3210 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x06000C8B RID: 3211 RVA: 0x00025F77 File Offset: 0x00024177
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000C8C RID: 3212 RVA: 0x00025F84 File Offset: 0x00024184
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x06000C8D RID: 3213 RVA: 0x00025F91 File Offset: 0x00024191
			public KeyValue<TKey, TValue> Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Enumerator.GetCurrent<TKey, TValue>();
				}
			}

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00025F9E File Offset: 0x0002419E
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000507 RID: 1287
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
