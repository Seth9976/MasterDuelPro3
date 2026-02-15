using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000A1 RID: 161
	[NativeContainer]
	[DebuggerDisplay("Count = {m_HashMapData.Count()}, Capacity = {m_HashMapData.Capacity}, IsCreated = {m_HashMapData.IsCreated}, IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(NativeParallelHashMapDebuggerTypeProxy<, >))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct NativeParallelHashMap<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue> : INativeDisposable, IDisposable, IEnumerable<KeyValue<TKey, TValue>>, IEnumerable where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType
	{
		// Token: 0x060007D0 RID: 2000 RVA: 0x000184FE File Offset: 0x000166FE
		public NativeParallelHashMap(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_HashMapData = new UnsafeParallelHashMap<TKey, TValue>(capacity, allocator);
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0001850D File Offset: 0x0001670D
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return !this.IsCreated || this.m_HashMapData.IsEmpty;
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00018524 File Offset: 0x00016724
		public int Count()
		{
			return this.m_HashMapData.Count();
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00018531 File Offset: 0x00016731
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x0001853E File Offset: 0x0001673E
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.m_HashMapData.Capacity;
			}
			set
			{
				this.m_HashMapData.Capacity = value;
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001854C File Offset: 0x0001674C
		public void Clear()
		{
			this.m_HashMapData.Clear();
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00018559 File Offset: 0x00016759
		public bool TryAdd(TKey key, TValue item)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(this.m_HashMapData.m_Buffer, key, item, false, this.m_HashMapData.m_AllocatorLabel);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00018579 File Offset: 0x00016779
		public void Add(TKey key, TValue item)
		{
			UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(this.m_HashMapData.m_Buffer, key, item, false, this.m_HashMapData.m_AllocatorLabel);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001859A File Offset: 0x0001679A
		public bool Remove(TKey key)
		{
			return this.m_HashMapData.Remove(key);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x000185A8 File Offset: 0x000167A8
		public bool TryGetValue(TKey key, out TValue item)
		{
			return this.m_HashMapData.TryGetValue(key, out item);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x000185B7 File Offset: 0x000167B7
		public bool ContainsKey(TKey key)
		{
			return this.m_HashMapData.ContainsKey(key);
		}

		// Token: 0x170000DA RID: 218
		public TValue this[TKey key]
		{
			get
			{
				TValue res;
				if (this.m_HashMapData.TryGetValue(key, out res))
				{
					return res;
				}
				return default(TValue);
			}
			set
			{
				this.m_HashMapData[key] = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x000185FF File Offset: 0x000167FF
		public readonly bool IsCreated
		{
			get
			{
				return this.m_HashMapData.IsCreated;
			}
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001860C File Offset: 0x0001680C
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			this.m_HashMapData.Dispose();
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00018624 File Offset: 0x00016824
		public JobHandle Dispose(JobHandle inputDeps)
		{
			if (!this.IsCreated)
			{
				return inputDeps;
			}
			JobHandle jobHandle = new UnsafeParallelHashMapDataDisposeJob
			{
				Data = new UnsafeParallelHashMapDataDispose
				{
					m_Buffer = this.m_HashMapData.m_Buffer,
					m_AllocatorLabel = this.m_HashMapData.m_AllocatorLabel
				}
			}.Schedule(inputDeps);
			this.m_HashMapData.m_Buffer = null;
			return jobHandle;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001868B File Offset: 0x0001688B
		public NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_HashMapData.GetKeyArray(allocator);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00018699 File Offset: 0x00016899
		public NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_HashMapData.GetValueArray(allocator);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x000186A7 File Offset: 0x000168A7
		public NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_HashMapData.GetKeyValueArrays(allocator);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x000186B8 File Offset: 0x000168B8
		public NativeParallelHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			NativeParallelHashMap<TKey, TValue>.ParallelWriter writer;
			writer.m_Writer = this.m_HashMapData.AsParallelWriter();
			return writer;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000186D8 File Offset: 0x000168D8
		public NativeParallelHashMap<TKey, TValue>.ReadOnly AsReadOnly()
		{
			return new NativeParallelHashMap<TKey, TValue>.ReadOnly(this.m_HashMapData);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x000186E8 File Offset: 0x000168E8
		public NativeParallelHashMap<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new NativeParallelHashMap<TKey, TValue>.Enumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_HashMapData.m_Buffer)
			};
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<TKey, TValue>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private readonly void CheckRead()
		{
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckWrite()
		{
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00018715 File Offset: 0x00016915
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void ThrowKeyNotPresent(TKey key)
		{
			throw new ArgumentException(string.Format("Key: {0} is not present in the NativeParallelHashMap.", key));
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001872C File Offset: 0x0001692C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void ThrowKeyAlreadyAdded(TKey key)
		{
			throw new ArgumentException("An item with the same key has already been added", "key");
		}

		// Token: 0x040003C7 RID: 967
		internal UnsafeParallelHashMap<TKey, TValue> m_HashMapData;

		// Token: 0x020000A2 RID: 162
		[NativeContainer]
		[NativeContainerIsReadOnly]
		[DebuggerTypeProxy(typeof(NativeParallelHashMapDebuggerTypeProxy<, >))]
		[DebuggerDisplay("Count = {m_HashMapData.Count()}, Capacity = {m_HashMapData.Capacity}, IsCreated = {m_HashMapData.IsCreated}, IsEmpty = {IsEmpty}")]
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ReadOnly : IEnumerable<KeyValue<TKey, TValue>>, IEnumerable
		{
			// Token: 0x060007EC RID: 2028 RVA: 0x0001873D File Offset: 0x0001693D
			internal ReadOnly(UnsafeParallelHashMap<TKey, TValue> hashMapData)
			{
				this.m_HashMapData = hashMapData;
			}

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x060007ED RID: 2029 RVA: 0x00018746 File Offset: 0x00016946
			public readonly bool IsCreated
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_HashMapData.IsCreated;
				}
			}

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x060007EE RID: 2030 RVA: 0x00018753 File Offset: 0x00016953
			public readonly bool IsEmpty
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return !this.IsCreated || this.m_HashMapData.IsEmpty;
				}
			}

			// Token: 0x060007EF RID: 2031 RVA: 0x0001876A File Offset: 0x0001696A
			public readonly int Count()
			{
				return this.m_HashMapData.Count();
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00018777 File Offset: 0x00016977
			public readonly int Capacity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_HashMapData.Capacity;
				}
			}

			// Token: 0x060007F1 RID: 2033 RVA: 0x00018784 File Offset: 0x00016984
			public readonly bool TryGetValue(TKey key, out TValue item)
			{
				UnsafeParallelHashMap<TKey, TValue> hashMapData = this.m_HashMapData;
				return hashMapData.TryGetValue(key, out item);
			}

			// Token: 0x060007F2 RID: 2034 RVA: 0x000187A4 File Offset: 0x000169A4
			public readonly bool ContainsKey(TKey key)
			{
				UnsafeParallelHashMap<TKey, TValue> hashMapData = this.m_HashMapData;
				return hashMapData.ContainsKey(key);
			}

			// Token: 0x170000DF RID: 223
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

			// Token: 0x060007F4 RID: 2036 RVA: 0x000187EC File Offset: 0x000169EC
			public readonly NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
			{
				UnsafeParallelHashMap<TKey, TValue> hashMapData = this.m_HashMapData;
				return hashMapData.GetKeyArray(allocator);
			}

			// Token: 0x060007F5 RID: 2037 RVA: 0x00018808 File Offset: 0x00016A08
			public readonly NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
			{
				UnsafeParallelHashMap<TKey, TValue> hashMapData = this.m_HashMapData;
				return hashMapData.GetValueArray(allocator);
			}

			// Token: 0x060007F6 RID: 2038 RVA: 0x00018824 File Offset: 0x00016A24
			public readonly NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
			{
				UnsafeParallelHashMap<TKey, TValue> hashMapData = this.m_HashMapData;
				return hashMapData.GetKeyValueArrays(allocator);
			}

			// Token: 0x060007F7 RID: 2039 RVA: 0x00002C47 File Offset: 0x00000E47
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private readonly void CheckRead()
			{
			}

			// Token: 0x060007F8 RID: 2040 RVA: 0x00018715 File Offset: 0x00016915
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			private readonly void ThrowKeyNotPresent(TKey key)
			{
				throw new ArgumentException(string.Format("Key: {0} is not present in the NativeParallelHashMap.", key));
			}

			// Token: 0x060007F9 RID: 2041 RVA: 0x00018840 File Offset: 0x00016A40
			public readonly NativeParallelHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new NativeParallelHashMap<TKey, TValue>.Enumerator
				{
					m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_HashMapData.m_Buffer)
				};
			}

			// Token: 0x060007FA RID: 2042 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<TKey, TValue>>.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007FB RID: 2043 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x040003C8 RID: 968
			internal UnsafeParallelHashMap<TKey, TValue> m_HashMapData;
		}

		// Token: 0x020000A3 RID: 163
		[NativeContainer]
		[NativeContainerIsAtomicWriteOnly]
		[DebuggerDisplay("Capacity = {m_Writer.Capacity}")]
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ParallelWriter
		{
			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x060007FC RID: 2044 RVA: 0x0001886D File Offset: 0x00016A6D
			public int ThreadIndex
			{
				get
				{
					return this.m_Writer.m_ThreadIndex;
				}
			}

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x060007FD RID: 2045 RVA: 0x0001886D File Offset: 0x00016A6D
			[Obsolete("'m_ThreadIndex' has been deprecated; use 'ThreadIndex' instead. (UnityUpgradable) -> ThreadIndex")]
			public int m_ThreadIndex
			{
				get
				{
					return this.m_Writer.m_ThreadIndex;
				}
			}

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x060007FE RID: 2046 RVA: 0x0001887A File Offset: 0x00016A7A
			public readonly int Capacity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Writer.Capacity;
				}
			}

			// Token: 0x060007FF RID: 2047 RVA: 0x00018887 File Offset: 0x00016A87
			public bool TryAdd(TKey key, TValue item)
			{
				return this.m_Writer.TryAdd(key, item);
			}

			// Token: 0x06000800 RID: 2048 RVA: 0x00018896 File Offset: 0x00016A96
			internal bool TryAdd(TKey key, TValue item, int threadIndexOverride)
			{
				return this.m_Writer.TryAdd(key, item, threadIndexOverride);
			}

			// Token: 0x040003C9 RID: 969
			internal UnsafeParallelHashMap<TKey, TValue>.ParallelWriter m_Writer;
		}

		// Token: 0x020000A4 RID: 164
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct Enumerator : IEnumerator<KeyValue<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x06000801 RID: 2049 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x06000802 RID: 2050 RVA: 0x000188A6 File Offset: 0x00016AA6
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000803 RID: 2051 RVA: 0x000188B3 File Offset: 0x00016AB3
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x06000804 RID: 2052 RVA: 0x000188C0 File Offset: 0x00016AC0
			public KeyValue<TKey, TValue> Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Enumerator.GetCurrent<TKey, TValue>();
				}
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x06000805 RID: 2053 RVA: 0x000188CD File Offset: 0x00016ACD
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040003CA RID: 970
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
