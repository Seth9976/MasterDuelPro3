using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x02000094 RID: 148
	[NativeContainer]
	[DebuggerTypeProxy(typeof(NativeHashSetDebuggerTypeProxy<>))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct NativeHashSet<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable, IEnumerable<T>, IEnumerable where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x06000757 RID: 1879 RVA: 0x00017B38 File Offset: 0x00015D38
		public NativeHashSet(int initialCapacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_Data = HashMapHelper<T>.Alloc(initialCapacity, 0, 256, allocator);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00017B4D File Offset: 0x00015D4D
		public unsafe readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return !this.IsCreated || this.m_Data->IsEmpty;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00017B64 File Offset: 0x00015D64
		public unsafe readonly int Count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Data->Count;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00017B71 File Offset: 0x00015D71
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x00017B7E File Offset: 0x00015D7E
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

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00017B8C File Offset: 0x00015D8C
		public unsafe readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Data != null && this.m_Data->IsCreated;
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00017BA5 File Offset: 0x00015DA5
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			HashMapHelper<T>.Free(this.m_Data);
			this.m_Data = null;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00017BC4 File Offset: 0x00015DC4
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

		// Token: 0x0600075F RID: 1887 RVA: 0x00017C0F File Offset: 0x00015E0F
		public unsafe void Clear()
		{
			this.m_Data->Clear();
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00017C1C File Offset: 0x00015E1C
		public unsafe bool Add(T item)
		{
			return -1 != this.m_Data->TryAdd(in item);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00017C31 File Offset: 0x00015E31
		public unsafe bool Remove(T item)
		{
			return -1 != this.m_Data->TryRemove(item);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00017C45 File Offset: 0x00015E45
		public unsafe bool Contains(T item)
		{
			return -1 != this.m_Data->Find(item);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00017C59 File Offset: 0x00015E59
		public unsafe void TrimExcess()
		{
			this.m_Data->TrimExcess();
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00017C66 File Offset: 0x00015E66
		public unsafe NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_Data->GetKeyArray(allocator);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00017C74 File Offset: 0x00015E74
		public NativeHashSet<T>.Enumerator GetEnumerator()
		{
			return new NativeHashSet<T>.Enumerator
			{
				m_Enumerator = new HashMapHelper<T>.Enumerator(this.m_Data)
			};
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00017C9C File Offset: 0x00015E9C
		public NativeHashSet<T>.ReadOnly AsReadOnly()
		{
			return new NativeHashSet<T>.ReadOnly(ref this);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private readonly void CheckRead()
		{
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckWrite()
		{
		}

		// Token: 0x040003BC RID: 956
		[NativeDisableUnsafePtrRestriction]
		internal unsafe HashMapHelper<T>* m_Data;

		// Token: 0x02000095 RID: 149
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x0600076B RID: 1899 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x0600076C RID: 1900 RVA: 0x00017CA4 File Offset: 0x00015EA4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x0600076D RID: 1901 RVA: 0x00017CB1 File Offset: 0x00015EB1
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x0600076E RID: 1902 RVA: 0x00017CBE File Offset: 0x00015EBE
			public T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Enumerator.GetCurrentKey();
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x0600076F RID: 1903 RVA: 0x00017CCB File Offset: 0x00015ECB
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040003BD RID: 957
			[NativeDisableUnsafePtrRestriction]
			internal HashMapHelper<T>.Enumerator m_Enumerator;
		}

		// Token: 0x02000096 RID: 150
		[NativeContainer]
		[NativeContainerIsReadOnly]
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ReadOnly : IEnumerable<T>, IEnumerable
		{
			// Token: 0x06000770 RID: 1904 RVA: 0x00017CD8 File Offset: 0x00015ED8
			internal ReadOnly(ref NativeHashSet<T> data)
			{
				this.m_Data = data.m_Data;
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000771 RID: 1905 RVA: 0x00017CE6 File Offset: 0x00015EE6
			public unsafe readonly bool IsCreated
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data != null && this.m_Data->IsCreated;
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06000772 RID: 1906 RVA: 0x00017CFF File Offset: 0x00015EFF
			public unsafe readonly bool IsEmpty
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return !this.IsCreated || this.m_Data->IsEmpty;
				}
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x06000773 RID: 1907 RVA: 0x00017D16 File Offset: 0x00015F16
			public unsafe readonly int Count
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data->Count;
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x06000774 RID: 1908 RVA: 0x00017D23 File Offset: 0x00015F23
			public unsafe readonly int Capacity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data->Capacity;
				}
			}

			// Token: 0x06000775 RID: 1909 RVA: 0x00017D30 File Offset: 0x00015F30
			public unsafe readonly bool Contains(T item)
			{
				return -1 != this.m_Data->Find(item);
			}

			// Token: 0x06000776 RID: 1910 RVA: 0x00017D44 File Offset: 0x00015F44
			public unsafe readonly NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
			{
				return this.m_Data->GetKeyArray(allocator);
			}

			// Token: 0x06000777 RID: 1911 RVA: 0x00017D54 File Offset: 0x00015F54
			public readonly NativeHashSet<T>.Enumerator GetEnumerator()
			{
				return new NativeHashSet<T>.Enumerator
				{
					m_Enumerator = new HashMapHelper<T>.Enumerator(this.m_Data)
				};
			}

			// Token: 0x06000778 RID: 1912 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000779 RID: 1913 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600077A RID: 1914 RVA: 0x00002C47 File Offset: 0x00000E47
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private readonly void CheckRead()
			{
			}

			// Token: 0x040003BE RID: 958
			[NativeDisableUnsafePtrRestriction]
			internal unsafe HashMapHelper<T>* m_Data;
		}
	}
}
