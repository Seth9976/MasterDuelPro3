using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000A7 RID: 167
	[DebuggerTypeProxy(typeof(NativeParallelHashSetDebuggerTypeProxy<>))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct NativeParallelHashSet<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable, IEnumerable<T>, IEnumerable where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x0600080F RID: 2063 RVA: 0x00018A74 File Offset: 0x00016C74
		public NativeParallelHashSet(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_Data = new NativeParallelHashMap<T, bool>(capacity, allocator);
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00018A83 File Offset: 0x00016C83
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Data.IsEmpty;
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00018A90 File Offset: 0x00016C90
		public int Count()
		{
			return this.m_Data.Count();
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00018A9D File Offset: 0x00016C9D
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x00018AAA File Offset: 0x00016CAA
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.m_Data.Capacity;
			}
			set
			{
				this.m_Data.Capacity = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00018AB8 File Offset: 0x00016CB8
		public readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Data.IsCreated;
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00018AC5 File Offset: 0x00016CC5
		public void Dispose()
		{
			this.m_Data.Dispose();
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00018AD2 File Offset: 0x00016CD2
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return this.m_Data.Dispose(inputDeps);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00018AE0 File Offset: 0x00016CE0
		public void Clear()
		{
			this.m_Data.Clear();
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00018AED File Offset: 0x00016CED
		public bool Add(T item)
		{
			return this.m_Data.TryAdd(item, false);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00018AFC File Offset: 0x00016CFC
		public bool Remove(T item)
		{
			return this.m_Data.Remove(item);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00018B0A File Offset: 0x00016D0A
		public bool Contains(T item)
		{
			return this.m_Data.ContainsKey(item);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00018B18 File Offset: 0x00016D18
		public NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_Data.GetKeyArray(allocator);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00018B28 File Offset: 0x00016D28
		public NativeParallelHashSet<T>.ParallelWriter AsParallelWriter()
		{
			NativeParallelHashSet<T>.ParallelWriter writer;
			writer.m_Data = this.m_Data.AsParallelWriter();
			return writer;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00018B48 File Offset: 0x00016D48
		public NativeParallelHashSet<T>.Enumerator GetEnumerator()
		{
			return new NativeParallelHashSet<T>.Enumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_Data.m_HashMapData.m_Buffer)
			};
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00018B7A File Offset: 0x00016D7A
		public NativeParallelHashSet<T>.ReadOnly AsReadOnly()
		{
			return new NativeParallelHashSet<T>.ReadOnly(ref this);
		}

		// Token: 0x040003CC RID: 972
		internal NativeParallelHashMap<T, bool> m_Data;

		// Token: 0x020000A8 RID: 168
		[NativeContainerIsAtomicWriteOnly]
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ParallelWriter
		{
			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x06000821 RID: 2081 RVA: 0x00018B82 File Offset: 0x00016D82
			public readonly int Capacity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data.Capacity;
				}
			}

			// Token: 0x06000822 RID: 2082 RVA: 0x00018B8F File Offset: 0x00016D8F
			public bool Add(T item)
			{
				return this.m_Data.TryAdd(item, false);
			}

			// Token: 0x06000823 RID: 2083 RVA: 0x00018B9E File Offset: 0x00016D9E
			internal bool Add(T item, int threadIndexOverride)
			{
				return this.m_Data.TryAdd(item, false, threadIndexOverride);
			}

			// Token: 0x040003CD RID: 973
			internal NativeParallelHashMap<T, bool>.ParallelWriter m_Data;
		}

		// Token: 0x020000A9 RID: 169
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000824 RID: 2084 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x06000825 RID: 2085 RVA: 0x00018BAE File Offset: 0x00016DAE
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000826 RID: 2086 RVA: 0x00018BBB File Offset: 0x00016DBB
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x06000827 RID: 2087 RVA: 0x00018BC8 File Offset: 0x00016DC8
			public T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Enumerator.GetCurrentKey<T>();
				}
			}

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x06000828 RID: 2088 RVA: 0x00018BD5 File Offset: 0x00016DD5
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040003CE RID: 974
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}

		// Token: 0x020000AA RID: 170
		[NativeContainer]
		[NativeContainerIsReadOnly]
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ReadOnly : IEnumerable<T>, IEnumerable
		{
			// Token: 0x06000829 RID: 2089 RVA: 0x00018BE2 File Offset: 0x00016DE2
			internal ReadOnly(ref NativeParallelHashSet<T> data)
			{
				this.m_Data = data.m_Data.m_HashMapData;
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x0600082A RID: 2090 RVA: 0x00018BF5 File Offset: 0x00016DF5
			public readonly bool IsCreated
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data.IsCreated;
				}
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x0600082B RID: 2091 RVA: 0x00018C02 File Offset: 0x00016E02
			public readonly bool IsEmpty
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return !this.IsCreated || this.m_Data.IsEmpty;
				}
			}

			// Token: 0x0600082C RID: 2092 RVA: 0x00018C19 File Offset: 0x00016E19
			public readonly int Count()
			{
				return this.m_Data.Count();
			}

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x0600082D RID: 2093 RVA: 0x00018C26 File Offset: 0x00016E26
			public readonly int Capacity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data.Capacity;
				}
			}

			// Token: 0x0600082E RID: 2094 RVA: 0x00018C34 File Offset: 0x00016E34
			public readonly bool Contains(T item)
			{
				UnsafeParallelHashMap<T, bool> data = this.m_Data;
				return data.ContainsKey(item);
			}

			// Token: 0x0600082F RID: 2095 RVA: 0x00018C50 File Offset: 0x00016E50
			public readonly NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
			{
				UnsafeParallelHashMap<T, bool> data = this.m_Data;
				return data.GetKeyArray(allocator);
			}

			// Token: 0x06000830 RID: 2096 RVA: 0x00018C6C File Offset: 0x00016E6C
			public readonly NativeParallelHashSet<T>.Enumerator GetEnumerator()
			{
				return new NativeParallelHashSet<T>.Enumerator
				{
					m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_Data.m_Buffer)
				};
			}

			// Token: 0x06000831 RID: 2097 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000832 RID: 2098 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000833 RID: 2099 RVA: 0x00002C47 File Offset: 0x00000E47
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private readonly void CheckRead()
			{
			}

			// Token: 0x040003CF RID: 975
			internal UnsafeParallelHashMap<T, bool> m_Data;
		}
	}
}
