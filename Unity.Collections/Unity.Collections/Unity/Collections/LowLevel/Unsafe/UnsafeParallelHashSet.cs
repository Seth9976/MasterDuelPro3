using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000132 RID: 306
	[DebuggerTypeProxy(typeof(UnsafeParallelHashSetDebuggerTypeProxy<>))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct UnsafeParallelHashSet<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable, IEnumerable<T>, IEnumerable where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x06000C92 RID: 3218 RVA: 0x00026054 File Offset: 0x00024254
		public UnsafeParallelHashSet(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_Data = new UnsafeParallelHashMap<T, bool>(capacity, allocator);
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x00026063 File Offset: 0x00024263
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Data.IsEmpty;
			}
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00026070 File Offset: 0x00024270
		public int Count()
		{
			return this.m_Data.Count();
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0002607D File Offset: 0x0002427D
		// (set) Token: 0x06000C96 RID: 3222 RVA: 0x0002608A File Offset: 0x0002428A
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

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x00026098 File Offset: 0x00024298
		public readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Data.IsCreated;
			}
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x000260A5 File Offset: 0x000242A5
		public void Dispose()
		{
			this.m_Data.Dispose();
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x000260B2 File Offset: 0x000242B2
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return this.m_Data.Dispose(inputDeps);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x000260C0 File Offset: 0x000242C0
		public void Clear()
		{
			this.m_Data.Clear();
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x000260CD File Offset: 0x000242CD
		public bool Add(T item)
		{
			return this.m_Data.TryAdd(item, false);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x000260DC File Offset: 0x000242DC
		public bool Remove(T item)
		{
			return this.m_Data.Remove(item);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x000260EA File Offset: 0x000242EA
		public bool Contains(T item)
		{
			return this.m_Data.ContainsKey(item);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x000260F8 File Offset: 0x000242F8
		public NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_Data.GetKeyArray(allocator);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00026108 File Offset: 0x00024308
		public UnsafeParallelHashSet<T>.ParallelWriter AsParallelWriter()
		{
			return new UnsafeParallelHashSet<T>.ParallelWriter
			{
				m_Data = this.m_Data.AsParallelWriter()
			};
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00026130 File Offset: 0x00024330
		public UnsafeParallelHashSet<T>.Enumerator GetEnumerator()
		{
			return new UnsafeParallelHashSet<T>.Enumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_Data.m_Buffer)
			};
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0002615D File Offset: 0x0002435D
		public UnsafeParallelHashSet<T>.ReadOnly AsReadOnly()
		{
			return new UnsafeParallelHashSet<T>.ReadOnly(ref this);
		}

		// Token: 0x0400050D RID: 1293
		internal UnsafeParallelHashMap<T, bool> m_Data;

		// Token: 0x02000133 RID: 307
		[NativeContainerIsAtomicWriteOnly]
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ParallelWriter
		{
			// Token: 0x17000184 RID: 388
			// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x00026165 File Offset: 0x00024365
			public readonly int Capacity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data.Capacity;
				}
			}

			// Token: 0x06000CA5 RID: 3237 RVA: 0x00026172 File Offset: 0x00024372
			public bool Add(T item)
			{
				return this.m_Data.TryAdd(item, false);
			}

			// Token: 0x06000CA6 RID: 3238 RVA: 0x00026181 File Offset: 0x00024381
			internal bool Add(T item, int threadIndexOverride)
			{
				return this.m_Data.TryAdd(item, false, threadIndexOverride);
			}

			// Token: 0x0400050E RID: 1294
			internal UnsafeParallelHashMap<T, bool>.ParallelWriter m_Data;
		}

		// Token: 0x02000134 RID: 308
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000CA7 RID: 3239 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x06000CA8 RID: 3240 RVA: 0x00026191 File Offset: 0x00024391
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000CA9 RID: 3241 RVA: 0x0002619E File Offset: 0x0002439E
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x17000185 RID: 389
			// (get) Token: 0x06000CAA RID: 3242 RVA: 0x000261AB File Offset: 0x000243AB
			public T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Enumerator.GetCurrentKey<T>();
				}
			}

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x06000CAB RID: 3243 RVA: 0x000261B8 File Offset: 0x000243B8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0400050F RID: 1295
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}

		// Token: 0x02000135 RID: 309
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ReadOnly : IEnumerable<T>, IEnumerable
		{
			// Token: 0x06000CAC RID: 3244 RVA: 0x000261C5 File Offset: 0x000243C5
			internal ReadOnly(ref UnsafeParallelHashSet<T> data)
			{
				this.m_Data = data.m_Data;
			}

			// Token: 0x17000187 RID: 391
			// (get) Token: 0x06000CAD RID: 3245 RVA: 0x000261D3 File Offset: 0x000243D3
			public readonly bool IsCreated
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data.IsCreated;
				}
			}

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x06000CAE RID: 3246 RVA: 0x000261E0 File Offset: 0x000243E0
			public readonly bool IsEmpty
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return !this.m_Data.IsCreated || this.m_Data.IsEmpty;
				}
			}

			// Token: 0x06000CAF RID: 3247 RVA: 0x000261FC File Offset: 0x000243FC
			public readonly int Count()
			{
				return this.m_Data.Count();
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00026209 File Offset: 0x00024409
			public readonly int Capacity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data.Capacity;
				}
			}

			// Token: 0x06000CB1 RID: 3249 RVA: 0x00026218 File Offset: 0x00024418
			public readonly bool Contains(T item)
			{
				UnsafeParallelHashMap<T, bool> data = this.m_Data;
				return data.ContainsKey(item);
			}

			// Token: 0x06000CB2 RID: 3250 RVA: 0x00026234 File Offset: 0x00024434
			public readonly NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
			{
				UnsafeParallelHashMap<T, bool> data = this.m_Data;
				return data.GetKeyArray(allocator);
			}

			// Token: 0x06000CB3 RID: 3251 RVA: 0x00026250 File Offset: 0x00024450
			public readonly UnsafeParallelHashSet<T>.Enumerator GetEnumerator()
			{
				return new UnsafeParallelHashSet<T>.Enumerator
				{
					m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_Data.m_Buffer)
				};
			}

			// Token: 0x06000CB4 RID: 3252 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000CB5 RID: 3253 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x04000510 RID: 1296
			internal UnsafeParallelHashMap<T, bool> m_Data;
		}
	}
}
