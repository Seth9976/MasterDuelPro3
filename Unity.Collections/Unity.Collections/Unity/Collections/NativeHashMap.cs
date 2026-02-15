using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x02000090 RID: 144
	[NativeContainer]
	[DebuggerTypeProxy(typeof(NativeHashMapDebuggerTypeProxy<, >))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct NativeHashMap<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue> : INativeDisposable, IDisposable, IEnumerable<KVPair<TKey, TValue>>, IEnumerable where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType
	{
		// Token: 0x06000723 RID: 1827 RVA: 0x00017710 File Offset: 0x00015910
		public NativeHashMap(int initialCapacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_Data = HashMapHelper<TKey>.Alloc(initialCapacity, sizeof(TValue), 256, allocator);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001772A File Offset: 0x0001592A
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			HashMapHelper<TKey>.Free(this.m_Data);
			this.m_Data = null;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00017748 File Offset: 0x00015948
		public unsafe JobHandle Dispose(JobHandle inputDeps)
		{
			if (!this.IsCreated)
			{
				return inputDeps;
			}
			JobHandle jobHandle = new NativeHashMapDisposeJob
			{
				Data = new NativeHashMapDispose
				{
					m_HashMapData = (UnsafeHashMap<int, int>*)this.m_Data
				}
			}.Schedule(inputDeps);
			this.m_Data = null;
			return jobHandle;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x00017793 File Offset: 0x00015993
		public unsafe readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Data != null && this.m_Data->IsCreated;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x000177AC File Offset: 0x000159AC
		public unsafe readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return !this.IsCreated || this.m_Data->IsEmpty;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x000177C3 File Offset: 0x000159C3
		public unsafe readonly int Count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Data->Count;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x000177D0 File Offset: 0x000159D0
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x000177DD File Offset: 0x000159DD
		public unsafe int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.m_Data->Capacity;
			}
			set
			{
				this.m_Data->Resize(value);
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x000177EB File Offset: 0x000159EB
		public unsafe void Clear()
		{
			this.m_Data->Clear();
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x000177F8 File Offset: 0x000159F8
		public unsafe bool TryAdd(TKey key, TValue item)
		{
			int idx = this.m_Data->TryAdd(in key);
			if (-1 != idx)
			{
				UnsafeUtility.WriteArrayElement<TValue>((void*)this.m_Data->Ptr, idx, item);
				return true;
			}
			return false;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001782C File Offset: 0x00015A2C
		public void Add(TKey key, TValue item)
		{
			this.TryAdd(key, item);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00017837 File Offset: 0x00015A37
		public unsafe bool Remove(TKey key)
		{
			return -1 != this.m_Data->TryRemove(key);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001784B File Offset: 0x00015A4B
		public unsafe bool TryGetValue(TKey key, out TValue item)
		{
			return this.m_Data->TryGetValue<TValue>(key, out item);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001785A File Offset: 0x00015A5A
		public unsafe bool ContainsKey(TKey key)
		{
			return -1 != this.m_Data->Find(key);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001786E File Offset: 0x00015A6E
		public unsafe void TrimExcess()
		{
			this.m_Data->TrimExcess();
		}

		// Token: 0x170000B8 RID: 184
		public unsafe TValue this[TKey key]
		{
			get
			{
				TValue result;
				this.m_Data->TryGetValue<TValue>(key, out result);
				return result;
			}
			set
			{
				int idx = this.m_Data->Find(key);
				if (-1 == idx)
				{
					this.TryAdd(key, value);
					return;
				}
				UnsafeUtility.WriteArrayElement<TValue>((void*)this.m_Data->Ptr, idx, value);
			}
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x000178D6 File Offset: 0x00015AD6
		public unsafe NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_Data->GetKeyArray(allocator);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000178E4 File Offset: 0x00015AE4
		public unsafe NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_Data->GetValueArray<TValue>(allocator);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x000178F2 File Offset: 0x00015AF2
		public unsafe NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_Data->GetKeyValueArrays<TValue>(allocator);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00017900 File Offset: 0x00015B00
		public NativeHashMap<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new NativeHashMap<TKey, TValue>.Enumerator
			{
				m_Enumerator = new HashMapHelper<TKey>.Enumerator(this.m_Data)
			};
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<KVPair<TKey, TValue>> IEnumerable<KVPair<TKey, TValue>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00017928 File Offset: 0x00015B28
		public NativeHashMap<TKey, TValue>.ReadOnly AsReadOnly()
		{
			return new NativeHashMap<TKey, TValue>.ReadOnly(ref this);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private readonly void CheckRead()
		{
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckWrite()
		{
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00017930 File Offset: 0x00015B30
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void ThrowKeyNotPresent(TKey key)
		{
			throw new ArgumentException(string.Format("Key: {0} is not present.", key));
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00017947 File Offset: 0x00015B47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void ThrowKeyAlreadyAdded(TKey key)
		{
			throw new ArgumentException(string.Format("An item with the same key has already been added: {0}", key));
		}

		// Token: 0x040003B8 RID: 952
		[NativeDisableUnsafePtrRestriction]
		internal unsafe HashMapHelper<TKey>* m_Data;

		// Token: 0x02000091 RID: 145
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct Enumerator : IEnumerator<KVPair<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x0600073F RID: 1855 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x06000740 RID: 1856 RVA: 0x0001795E File Offset: 0x00015B5E
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000741 RID: 1857 RVA: 0x0001796B File Offset: 0x00015B6B
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x06000742 RID: 1858 RVA: 0x00017978 File Offset: 0x00015B78
			public KVPair<TKey, TValue> Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Enumerator.GetCurrent<TValue>();
				}
			}

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x06000743 RID: 1859 RVA: 0x00017985 File Offset: 0x00015B85
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040003B9 RID: 953
			[NativeDisableUnsafePtrRestriction]
			internal HashMapHelper<TKey>.Enumerator m_Enumerator;
		}

		// Token: 0x02000092 RID: 146
		[NativeContainer]
		[NativeContainerIsReadOnly]
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ReadOnly : IEnumerable<KVPair<TKey, TValue>>, IEnumerable
		{
			// Token: 0x06000744 RID: 1860 RVA: 0x00017992 File Offset: 0x00015B92
			internal ReadOnly(ref NativeHashMap<TKey, TValue> data)
			{
				this.m_Data = data.m_Data;
			}

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x06000745 RID: 1861 RVA: 0x000179A0 File Offset: 0x00015BA0
			public unsafe readonly bool IsCreated
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data != null && this.m_Data->IsCreated;
				}
			}

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x06000746 RID: 1862 RVA: 0x000179B9 File Offset: 0x00015BB9
			public unsafe readonly bool IsEmpty
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return !this.IsCreated || this.m_Data->IsEmpty;
				}
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x06000747 RID: 1863 RVA: 0x000179D0 File Offset: 0x00015BD0
			public unsafe readonly int Count
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data->Count;
				}
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000748 RID: 1864 RVA: 0x000179DD File Offset: 0x00015BDD
			public unsafe readonly int Capacity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data->Capacity;
				}
			}

			// Token: 0x06000749 RID: 1865 RVA: 0x000179EA File Offset: 0x00015BEA
			public unsafe readonly bool TryGetValue(TKey key, out TValue item)
			{
				return this.m_Data->TryGetValue<TValue>(key, out item);
			}

			// Token: 0x0600074A RID: 1866 RVA: 0x000179F9 File Offset: 0x00015BF9
			public unsafe readonly bool ContainsKey(TKey key)
			{
				return -1 != this.m_Data->Find(key);
			}

			// Token: 0x170000BF RID: 191
			public unsafe readonly TValue this[TKey key]
			{
				get
				{
					TValue result;
					this.m_Data->TryGetValue<TValue>(key, out result);
					return result;
				}
			}

			// Token: 0x0600074C RID: 1868 RVA: 0x00017A2D File Offset: 0x00015C2D
			public unsafe readonly NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
			{
				return this.m_Data->GetKeyArray(allocator);
			}

			// Token: 0x0600074D RID: 1869 RVA: 0x00017A3B File Offset: 0x00015C3B
			public unsafe readonly NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
			{
				return this.m_Data->GetValueArray<TValue>(allocator);
			}

			// Token: 0x0600074E RID: 1870 RVA: 0x00017A49 File Offset: 0x00015C49
			public unsafe readonly NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
			{
				return this.m_Data->GetKeyValueArrays<TValue>(allocator);
			}

			// Token: 0x0600074F RID: 1871 RVA: 0x00017A58 File Offset: 0x00015C58
			public readonly NativeHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				return new NativeHashMap<TKey, TValue>.Enumerator
				{
					m_Enumerator = new HashMapHelper<TKey>.Enumerator(this.m_Data)
				};
			}

			// Token: 0x06000750 RID: 1872 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator<KVPair<TKey, TValue>> IEnumerable<KVPair<TKey, TValue>>.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000751 RID: 1873 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000752 RID: 1874 RVA: 0x00002C47 File Offset: 0x00000E47
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private readonly void CheckRead()
			{
			}

			// Token: 0x06000753 RID: 1875 RVA: 0x00017930 File Offset: 0x00015B30
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			private readonly void ThrowKeyNotPresent(TKey key)
			{
				throw new ArgumentException(string.Format("Key: {0} is not present.", key));
			}

			// Token: 0x040003BA RID: 954
			[NativeDisableUnsafePtrRestriction]
			internal unsafe HashMapHelper<TKey>* m_Data;
		}
	}
}
