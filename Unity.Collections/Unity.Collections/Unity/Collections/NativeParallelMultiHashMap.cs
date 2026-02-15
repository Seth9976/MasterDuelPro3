using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000AE RID: 174
	[NativeContainer]
	[DebuggerTypeProxy(typeof(NativeParallelMultiHashMapDebuggerTypeProxy<, >))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct NativeParallelMultiHashMap<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue> : INativeDisposable, IDisposable, IEnumerable<KeyValue<TKey, TValue>>, IEnumerable where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x0001A630 File Offset: 0x00018830
		public NativeParallelMultiHashMap(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this = default(NativeParallelMultiHashMap<TKey, TValue>);
			this.Initialize<AllocatorManager.AllocatorHandle>(capacity, ref allocator);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001A642 File Offset: 0x00018842
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
		internal void Initialize<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(int capacity, ref U allocator) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			this.m_MultiHashMapData = new UnsafeParallelMultiHashMap<TKey, TValue>(capacity, allocator.Handle);
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x0001A65C File Offset: 0x0001885C
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_MultiHashMapData.IsEmpty;
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001A669 File Offset: 0x00018869
		public readonly int Count()
		{
			return this.m_MultiHashMapData.Count();
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0001A676 File Offset: 0x00018876
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x0001A683 File Offset: 0x00018883
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.m_MultiHashMapData.Capacity;
			}
			set
			{
				this.m_MultiHashMapData.Capacity = value;
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001A691 File Offset: 0x00018891
		public void Clear()
		{
			this.m_MultiHashMapData.Clear();
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001A69E File Offset: 0x0001889E
		public void Add(TKey key, TValue item)
		{
			this.m_MultiHashMapData.Add(key, item);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001A6AD File Offset: 0x000188AD
		public int Remove(TKey key)
		{
			return this.m_MultiHashMapData.Remove(key);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001A6BB File Offset: 0x000188BB
		public void Remove(NativeParallelMultiHashMapIterator<TKey> it)
		{
			this.m_MultiHashMapData.Remove(it);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001A6C9 File Offset: 0x000188C9
		public bool TryGetFirstValue(TKey key, out TValue item, out NativeParallelMultiHashMapIterator<TKey> it)
		{
			return this.m_MultiHashMapData.TryGetFirstValue(key, out item, out it);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001A6D9 File Offset: 0x000188D9
		public bool TryGetNextValue(out TValue item, ref NativeParallelMultiHashMapIterator<TKey> it)
		{
			return this.m_MultiHashMapData.TryGetNextValue(out item, ref it);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001A6E8 File Offset: 0x000188E8
		public bool ContainsKey(TKey key)
		{
			TValue temp0;
			NativeParallelMultiHashMapIterator<TKey> temp;
			return this.TryGetFirstValue(key, out temp0, out temp);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001A700 File Offset: 0x00018900
		public int CountValuesForKey(TKey key)
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

		// Token: 0x06000887 RID: 2183 RVA: 0x0001A731 File Offset: 0x00018931
		public bool SetValue(TValue item, NativeParallelMultiHashMapIterator<TKey> it)
		{
			return this.m_MultiHashMapData.SetValue(item, it);
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x0001A740 File Offset: 0x00018940
		public readonly bool IsCreated
		{
			get
			{
				return this.m_MultiHashMapData.IsCreated;
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001A74D File Offset: 0x0001894D
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			this.m_MultiHashMapData.Dispose();
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001A764 File Offset: 0x00018964
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
					m_Buffer = this.m_MultiHashMapData.m_Buffer,
					m_AllocatorLabel = this.m_MultiHashMapData.m_AllocatorLabel
				}
			}.Schedule(inputDeps);
			this.m_MultiHashMapData.m_Buffer = null;
			return jobHandle;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001A7CB File Offset: 0x000189CB
		public NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_MultiHashMapData.GetKeyArray(allocator);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001A7D9 File Offset: 0x000189D9
		public NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_MultiHashMapData.GetValueArray(allocator);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001A7E7 File Offset: 0x000189E7
		public NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_MultiHashMapData.GetKeyValueArrays(allocator);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001A7F8 File Offset: 0x000189F8
		public NativeParallelMultiHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			NativeParallelMultiHashMap<TKey, TValue>.ParallelWriter writer;
			writer.m_Writer = this.m_MultiHashMapData.AsParallelWriter();
			return writer;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001A818 File Offset: 0x00018A18
		public NativeParallelMultiHashMap<TKey, TValue>.Enumerator GetValuesForKey(TKey key)
		{
			return new NativeParallelMultiHashMap<TKey, TValue>.Enumerator
			{
				hashmap = this,
				key = key,
				isFirst = 1
			};
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001A84C File Offset: 0x00018A4C
		public NativeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator GetEnumerator()
		{
			return new NativeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_MultiHashMapData.m_Buffer)
			};
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<TKey, TValue>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001A879 File Offset: 0x00018A79
		public NativeParallelMultiHashMap<TKey, TValue>.ReadOnly AsReadOnly()
		{
			return new NativeParallelMultiHashMap<TKey, TValue>.ReadOnly(this.m_MultiHashMapData);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private readonly void CheckRead()
		{
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckWrite()
		{
		}

		// Token: 0x040003D4 RID: 980
		internal UnsafeParallelMultiHashMap<TKey, TValue> m_MultiHashMapData;

		// Token: 0x020000AF RID: 175
		[NativeContainer]
		[NativeContainerIsAtomicWriteOnly]
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ParallelWriter
		{
			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x06000896 RID: 2198 RVA: 0x0001A886 File Offset: 0x00018A86
			public int m_ThreadIndex
			{
				get
				{
					return this.m_Writer.m_ThreadIndex;
				}
			}

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x06000897 RID: 2199 RVA: 0x0001A893 File Offset: 0x00018A93
			public readonly int Capacity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Writer.Capacity;
				}
			}

			// Token: 0x06000898 RID: 2200 RVA: 0x0001A8A0 File Offset: 0x00018AA0
			public void Add(TKey key, TValue item)
			{
				this.m_Writer.Add(key, item);
			}

			// Token: 0x040003D5 RID: 981
			internal UnsafeParallelMultiHashMap<TKey, TValue>.ParallelWriter m_Writer;
		}

		// Token: 0x020000B0 RID: 176
		public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x06000899 RID: 2201 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x0600089A RID: 2202 RVA: 0x0001A8B0 File Offset: 0x00018AB0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				if (this.isFirst == 1)
				{
					this.isFirst = 0;
					return this.hashmap.TryGetFirstValue(this.key, out this.value, out this.iterator);
				}
				return this.hashmap.TryGetNextValue(out this.value, ref this.iterator);
			}

			// Token: 0x0600089B RID: 2203 RVA: 0x0001A902 File Offset: 0x00018B02
			public void Reset()
			{
				this.isFirst = 1;
			}

			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x0600089C RID: 2204 RVA: 0x0001A90B File Offset: 0x00018B0B
			public TValue Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.value;
				}
			}

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x0600089D RID: 2205 RVA: 0x0001A913 File Offset: 0x00018B13
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600089E RID: 2206 RVA: 0x0001A920 File Offset: 0x00018B20
			public NativeParallelMultiHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x040003D6 RID: 982
			internal NativeParallelMultiHashMap<TKey, TValue> hashmap;

			// Token: 0x040003D7 RID: 983
			internal TKey key;

			// Token: 0x040003D8 RID: 984
			internal byte isFirst;

			// Token: 0x040003D9 RID: 985
			private TValue value;

			// Token: 0x040003DA RID: 986
			private NativeParallelMultiHashMapIterator<TKey> iterator;
		}

		// Token: 0x020000B1 RID: 177
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct KeyValueEnumerator : IEnumerator<KeyValue<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x0600089F RID: 2207 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x060008A0 RID: 2208 RVA: 0x0001A928 File Offset: 0x00018B28
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x060008A1 RID: 2209 RVA: 0x0001A935 File Offset: 0x00018B35
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0001A944 File Offset: 0x00018B44
			public readonly KeyValue<TKey, TValue> Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					UnsafeParallelHashMapDataEnumerator enumerator = this.m_Enumerator;
					return enumerator.GetCurrent<TKey, TValue>();
				}
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x060008A3 RID: 2211 RVA: 0x0001A95F File Offset: 0x00018B5F
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040003DB RID: 987
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}

		// Token: 0x020000B2 RID: 178
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
			// Token: 0x060008A4 RID: 2212 RVA: 0x0001A96C File Offset: 0x00018B6C
			internal ReadOnly(UnsafeParallelMultiHashMap<TKey, TValue> container)
			{
				this.m_MultiHashMapData = container;
			}

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0001A975 File Offset: 0x00018B75
			public readonly bool IsCreated
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_MultiHashMapData.IsCreated;
				}
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0001A982 File Offset: 0x00018B82
			public readonly bool IsEmpty
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return !this.IsCreated || this.m_MultiHashMapData.IsEmpty;
				}
			}

			// Token: 0x060008A7 RID: 2215 RVA: 0x0001A999 File Offset: 0x00018B99
			public readonly int Count()
			{
				return this.m_MultiHashMapData.Count();
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0001A9A6 File Offset: 0x00018BA6
			public readonly int Capacity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_MultiHashMapData.Capacity;
				}
			}

			// Token: 0x060008A9 RID: 2217 RVA: 0x0001A9B3 File Offset: 0x00018BB3
			public readonly bool TryGetFirstValue(TKey key, out TValue item, out NativeParallelMultiHashMapIterator<TKey> it)
			{
				return this.m_MultiHashMapData.TryGetFirstValue(key, out item, out it);
			}

			// Token: 0x060008AA RID: 2218 RVA: 0x0001A9C3 File Offset: 0x00018BC3
			public readonly bool TryGetNextValue(out TValue item, ref NativeParallelMultiHashMapIterator<TKey> it)
			{
				return this.m_MultiHashMapData.TryGetNextValue(out item, ref it);
			}

			// Token: 0x060008AB RID: 2219 RVA: 0x0001A9D2 File Offset: 0x00018BD2
			public readonly bool ContainsKey(TKey key)
			{
				return this.m_MultiHashMapData.ContainsKey(key);
			}

			// Token: 0x060008AC RID: 2220 RVA: 0x0001A9E0 File Offset: 0x00018BE0
			public readonly NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
			{
				return this.m_MultiHashMapData.GetKeyArray(allocator);
			}

			// Token: 0x060008AD RID: 2221 RVA: 0x0001A9EE File Offset: 0x00018BEE
			public readonly NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
			{
				return this.m_MultiHashMapData.GetValueArray(allocator);
			}

			// Token: 0x060008AE RID: 2222 RVA: 0x0001A9FC File Offset: 0x00018BFC
			public readonly NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
			{
				return this.m_MultiHashMapData.GetKeyValueArrays(allocator);
			}

			// Token: 0x060008AF RID: 2223 RVA: 0x00002C47 File Offset: 0x00000E47
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private readonly void CheckRead()
			{
			}

			// Token: 0x060008B0 RID: 2224 RVA: 0x00018715 File Offset: 0x00016915
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			private readonly void ThrowKeyNotPresent(TKey key)
			{
				throw new ArgumentException(string.Format("Key: {0} is not present in the NativeParallelHashMap.", key));
			}

			// Token: 0x060008B1 RID: 2225 RVA: 0x0001AA0C File Offset: 0x00018C0C
			public NativeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator GetEnumerator()
			{
				return new NativeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator
				{
					m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_MultiHashMapData.m_Buffer)
				};
			}

			// Token: 0x060008B2 RID: 2226 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<TKey, TValue>>.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008B3 RID: 2227 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x040003DC RID: 988
			internal UnsafeParallelMultiHashMap<TKey, TValue> m_MultiHashMapData;
		}
	}
}
