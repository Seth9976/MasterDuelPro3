using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200010D RID: 269
	[DebuggerTypeProxy(typeof(UnsafeHashMapDebuggerTypeProxy<, >))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct UnsafeHashMap<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue> : INativeDisposable, IDisposable, IEnumerable<KVPair<TKey, TValue>>, IEnumerable where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType
	{
		// Token: 0x06000B5A RID: 2906 RVA: 0x00023163 File Offset: 0x00021363
		public UnsafeHashMap(int initialCapacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_Data = default(HashMapHelper<TKey>);
			this.m_Data.Init(initialCapacity, sizeof(TValue), 256, allocator);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00023189 File Offset: 0x00021389
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			this.m_Data.Dispose();
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x000231A0 File Offset: 0x000213A0
		public unsafe JobHandle Dispose(JobHandle inputDeps)
		{
			if (!this.IsCreated)
			{
				return inputDeps;
			}
			JobHandle jobHandle = new UnsafeDisposeJob
			{
				Ptr = (void*)this.m_Data.Ptr,
				Allocator = this.m_Data.Allocator
			}.Schedule(inputDeps);
			this.m_Data = default(HashMapHelper<TKey>);
			return jobHandle;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x000231F6 File Offset: 0x000213F6
		public readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Data.IsCreated;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00023203 File Offset: 0x00021403
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Data.IsEmpty;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x00023210 File Offset: 0x00021410
		public readonly int Count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Data.Count;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0002321D File Offset: 0x0002141D
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x0002322A File Offset: 0x0002142A
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.m_Data.Capacity;
			}
			set
			{
				this.m_Data.Resize(value);
			}
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00023238 File Offset: 0x00021438
		public void Clear()
		{
			this.m_Data.Clear();
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00023248 File Offset: 0x00021448
		public unsafe bool TryAdd(TKey key, TValue item)
		{
			int idx = this.m_Data.TryAdd(in key);
			if (-1 != idx)
			{
				UnsafeUtility.WriteArrayElement<TValue>((void*)this.m_Data.Ptr, idx, item);
				return true;
			}
			return false;
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002327C File Offset: 0x0002147C
		public void Add(TKey key, TValue item)
		{
			this.TryAdd(key, item);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00023287 File Offset: 0x00021487
		public bool Remove(TKey key)
		{
			return -1 != this.m_Data.TryRemove(key);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002329B File Offset: 0x0002149B
		public bool TryGetValue(TKey key, out TValue item)
		{
			return this.m_Data.TryGetValue<TValue>(key, out item);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x000232AA File Offset: 0x000214AA
		public bool ContainsKey(TKey key)
		{
			return -1 != this.m_Data.Find(key);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x000232BE File Offset: 0x000214BE
		public void TrimExcess()
		{
			this.m_Data.TrimExcess();
		}

		// Token: 0x17000149 RID: 329
		public unsafe TValue this[TKey key]
		{
			get
			{
				TValue result;
				this.m_Data.TryGetValue<TValue>(key, out result);
				return result;
			}
			set
			{
				int idx = this.m_Data.Find(key);
				if (-1 != idx)
				{
					UnsafeUtility.WriteArrayElement<TValue>((void*)this.m_Data.Ptr, idx, value);
					return;
				}
				this.TryAdd(key, value);
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00023326 File Offset: 0x00021526
		public NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_Data.GetKeyArray(allocator);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00023334 File Offset: 0x00021534
		public NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_Data.GetValueArray<TValue>(allocator);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00023342 File Offset: 0x00021542
		public NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_Data.GetKeyValueArrays<TValue>(allocator);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00023350 File Offset: 0x00021550
		public unsafe UnsafeHashMap<TKey, TValue>.Enumerator GetEnumerator()
		{
			fixed (HashMapHelper<TKey>* ptr = &this.m_Data)
			{
				HashMapHelper<TKey>* data = ptr;
				return new UnsafeHashMap<TKey, TValue>.Enumerator
				{
					m_Enumerator = new HashMapHelper<TKey>.Enumerator(data)
				};
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<KVPair<TKey, TValue>> IEnumerable<KVPair<TKey, TValue>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002337D File Offset: 0x0002157D
		public UnsafeHashMap<TKey, TValue>.ReadOnly AsReadOnly()
		{
			return new UnsafeHashMap<TKey, TValue>.ReadOnly(ref this.m_Data);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00017930 File Offset: 0x00015B30
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void ThrowKeyNotPresent(TKey key)
		{
			throw new ArgumentException(string.Format("Key: {0} is not present.", key));
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00017947 File Offset: 0x00015B47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void ThrowKeyAlreadyAdded(TKey key)
		{
			throw new ArgumentException(string.Format("An item with the same key has already been added: {0}", key));
		}

		// Token: 0x040004BF RID: 1215
		[NativeDisableUnsafePtrRestriction]
		internal HashMapHelper<TKey> m_Data;

		// Token: 0x0200010E RID: 270
		public struct Enumerator : IEnumerator<KVPair<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x06000B74 RID: 2932 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x06000B75 RID: 2933 RVA: 0x0002338A File Offset: 0x0002158A
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000B76 RID: 2934 RVA: 0x00023397 File Offset: 0x00021597
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x06000B77 RID: 2935 RVA: 0x000233A4 File Offset: 0x000215A4
			public KVPair<TKey, TValue> Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Enumerator.GetCurrent<TValue>();
				}
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x06000B78 RID: 2936 RVA: 0x000233B1 File Offset: 0x000215B1
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040004C0 RID: 1216
			internal HashMapHelper<TKey>.Enumerator m_Enumerator;
		}

		// Token: 0x0200010F RID: 271
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ReadOnly : IEnumerable<KVPair<TKey, TValue>>, IEnumerable
		{
			// Token: 0x06000B79 RID: 2937 RVA: 0x000233BE File Offset: 0x000215BE
			internal ReadOnly(ref HashMapHelper<TKey> data)
			{
				this.m_Data = data;
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x06000B7A RID: 2938 RVA: 0x000233CC File Offset: 0x000215CC
			public readonly bool IsCreated
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data.IsCreated;
				}
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06000B7B RID: 2939 RVA: 0x000233D9 File Offset: 0x000215D9
			public readonly bool IsEmpty
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data.IsEmpty;
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x06000B7C RID: 2940 RVA: 0x000233E6 File Offset: 0x000215E6
			public readonly int Count
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data.Count;
				}
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x06000B7D RID: 2941 RVA: 0x000233F3 File Offset: 0x000215F3
			public readonly int Capacity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data.Capacity;
				}
			}

			// Token: 0x06000B7E RID: 2942 RVA: 0x00023400 File Offset: 0x00021600
			public readonly bool TryGetValue(TKey key, out TValue item)
			{
				HashMapHelper<TKey> data = this.m_Data;
				return data.TryGetValue<TValue>(key, out item);
			}

			// Token: 0x06000B7F RID: 2943 RVA: 0x00023420 File Offset: 0x00021620
			public readonly bool ContainsKey(TKey key)
			{
				int num = -1;
				HashMapHelper<TKey> data = this.m_Data;
				return num != data.Find(key);
			}

			// Token: 0x17000150 RID: 336
			public readonly TValue this[TKey key]
			{
				get
				{
					HashMapHelper<TKey> data = this.m_Data;
					TValue result;
					data.TryGetValue<TValue>(key, out result);
					return result;
				}
			}

			// Token: 0x06000B81 RID: 2945 RVA: 0x00023464 File Offset: 0x00021664
			public readonly NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
			{
				HashMapHelper<TKey> data = this.m_Data;
				return data.GetKeyArray(allocator);
			}

			// Token: 0x06000B82 RID: 2946 RVA: 0x00023480 File Offset: 0x00021680
			public readonly NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
			{
				HashMapHelper<TKey> data = this.m_Data;
				return data.GetValueArray<TValue>(allocator);
			}

			// Token: 0x06000B83 RID: 2947 RVA: 0x0002349C File Offset: 0x0002169C
			public readonly NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
			{
				HashMapHelper<TKey> data = this.m_Data;
				return data.GetKeyValueArrays<TValue>(allocator);
			}

			// Token: 0x06000B84 RID: 2948 RVA: 0x000234B8 File Offset: 0x000216B8
			public unsafe readonly UnsafeHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				fixed (HashMapHelper<TKey>* ptr = &this.m_Data)
				{
					HashMapHelper<TKey>* data = ptr;
					return new UnsafeHashMap<TKey, TValue>.Enumerator
					{
						m_Enumerator = new HashMapHelper<TKey>.Enumerator(data)
					};
				}
			}

			// Token: 0x06000B85 RID: 2949 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator<KVPair<TKey, TValue>> IEnumerable<KVPair<TKey, TValue>>.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000B86 RID: 2950 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x040004C1 RID: 1217
			[NativeDisableUnsafePtrRestriction]
			internal HashMapHelper<TKey> m_Data;
		}
	}
}
