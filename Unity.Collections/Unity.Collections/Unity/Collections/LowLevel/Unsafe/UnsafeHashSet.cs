using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000111 RID: 273
	[DebuggerTypeProxy(typeof(UnsafeHashSetDebuggerTypeProxy<>))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct UnsafeHashSet<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable, IEnumerable<T>, IEnumerable where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x06000B8A RID: 2954 RVA: 0x00023594 File Offset: 0x00021794
		public UnsafeHashSet(int initialCapacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_Data = default(HashMapHelper<T>);
			this.m_Data.Init(initialCapacity, 0, 256, allocator);
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x000235B5 File Offset: 0x000217B5
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return !this.IsCreated || this.m_Data.IsEmpty;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x000235CC File Offset: 0x000217CC
		public readonly int Count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Data.Count;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x000235D9 File Offset: 0x000217D9
		// (set) Token: 0x06000B8E RID: 2958 RVA: 0x000235E6 File Offset: 0x000217E6
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

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x000235F4 File Offset: 0x000217F4
		public readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Data.IsCreated;
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00023601 File Offset: 0x00021801
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			this.m_Data.Dispose();
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00023618 File Offset: 0x00021818
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
			this.m_Data.Ptr = null;
			return jobHandle;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002366F File Offset: 0x0002186F
		public void Clear()
		{
			this.m_Data.Clear();
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002367C File Offset: 0x0002187C
		public bool Add(T item)
		{
			return -1 != this.m_Data.TryAdd(in item);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00023691 File Offset: 0x00021891
		public bool Remove(T item)
		{
			return -1 != this.m_Data.TryRemove(item);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x000236A5 File Offset: 0x000218A5
		public bool Contains(T item)
		{
			return -1 != this.m_Data.Find(item);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x000236B9 File Offset: 0x000218B9
		public void TrimExcess()
		{
			this.m_Data.TrimExcess();
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x000236C6 File Offset: 0x000218C6
		public NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_Data.GetKeyArray(allocator);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x000236D4 File Offset: 0x000218D4
		public unsafe UnsafeHashSet<T>.Enumerator GetEnumerator()
		{
			fixed (HashMapHelper<T>* ptr = &this.m_Data)
			{
				HashMapHelper<T>* data = ptr;
				return new UnsafeHashSet<T>.Enumerator
				{
					m_Enumerator = new HashMapHelper<T>.Enumerator(data)
				};
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00023701 File Offset: 0x00021901
		public UnsafeHashSet<T>.ReadOnly AsReadOnly()
		{
			return new UnsafeHashSet<T>.ReadOnly(ref this.m_Data);
		}

		// Token: 0x040004C3 RID: 1219
		internal HashMapHelper<T> m_Data;

		// Token: 0x02000112 RID: 274
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000B9C RID: 2972 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x06000B9D RID: 2973 RVA: 0x0002370E File Offset: 0x0002190E
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000B9E RID: 2974 RVA: 0x0002371B File Offset: 0x0002191B
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00023728 File Offset: 0x00021928
			public unsafe T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Enumerator.m_Data->Keys[(IntPtr)this.m_Enumerator.m_Index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x00023753 File Offset: 0x00021953
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040004C4 RID: 1220
			internal HashMapHelper<T>.Enumerator m_Enumerator;
		}

		// Token: 0x02000113 RID: 275
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ReadOnly : IEnumerable<T>, IEnumerable
		{
			// Token: 0x06000BA1 RID: 2977 RVA: 0x00023760 File Offset: 0x00021960
			internal ReadOnly(ref HashMapHelper<T> data)
			{
				this.m_Data = data;
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0002376E File Offset: 0x0002196E
			public readonly bool IsCreated
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data.IsCreated;
				}
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0002377B File Offset: 0x0002197B
			public readonly bool IsEmpty
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data.IsEmpty;
				}
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x00023788 File Offset: 0x00021988
			public readonly int Count
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data.Count;
				}
			}

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x00023795 File Offset: 0x00021995
			public readonly int Capacity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Data.Capacity;
				}
			}

			// Token: 0x06000BA6 RID: 2982 RVA: 0x000237A4 File Offset: 0x000219A4
			public readonly bool Contains(T item)
			{
				int num = -1;
				HashMapHelper<T> data = this.m_Data;
				return num != data.Find(item);
			}

			// Token: 0x06000BA7 RID: 2983 RVA: 0x000237C8 File Offset: 0x000219C8
			public readonly NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
			{
				HashMapHelper<T> data = this.m_Data;
				return data.GetKeyArray(allocator);
			}

			// Token: 0x06000BA8 RID: 2984 RVA: 0x000237E4 File Offset: 0x000219E4
			public unsafe readonly UnsafeHashSet<T>.Enumerator GetEnumerator()
			{
				fixed (HashMapHelper<T>* ptr = &this.m_Data)
				{
					HashMapHelper<T>* data = ptr;
					return new UnsafeHashSet<T>.Enumerator
					{
						m_Enumerator = new HashMapHelper<T>.Enumerator(data)
					};
				}
			}

			// Token: 0x06000BA9 RID: 2985 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000BAA RID: 2986 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x040004C5 RID: 1221
			internal HashMapHelper<T> m_Data;
		}
	}
}
