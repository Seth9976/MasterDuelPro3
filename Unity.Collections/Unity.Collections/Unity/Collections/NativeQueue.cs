using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000B5 RID: 181
	[NativeContainer]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct NativeQueue<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable where T : struct, ValueType
	{
		// Token: 0x060008B7 RID: 2231 RVA: 0x0001ABAA File Offset: 0x00018DAA
		public unsafe NativeQueue(AllocatorManager.AllocatorHandle allocator)
		{
			this.m_Queue = UnsafeQueue<T>.Alloc(allocator);
			*this.m_Queue = new UnsafeQueue<T>(allocator);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001ABC9 File Offset: 0x00018DC9
		public unsafe readonly bool IsEmpty()
		{
			return !this.IsCreated || this.m_Queue->IsEmpty();
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0001ABE0 File Offset: 0x00018DE0
		public unsafe readonly int Count
		{
			get
			{
				return this.m_Queue->Count;
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001ABED File Offset: 0x00018DED
		public unsafe T Peek()
		{
			return this.m_Queue->Peek();
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001ABFA File Offset: 0x00018DFA
		public unsafe void Enqueue(T value)
		{
			this.m_Queue->Enqueue(value);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001AC08 File Offset: 0x00018E08
		public unsafe T Dequeue()
		{
			return this.m_Queue->Dequeue();
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001AC15 File Offset: 0x00018E15
		public unsafe bool TryDequeue(out T item)
		{
			return this.m_Queue->TryDequeue(out item);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0001AC23 File Offset: 0x00018E23
		public unsafe NativeArray<T> ToArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_Queue->ToArray(allocator);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001AC31 File Offset: 0x00018E31
		public unsafe void Clear()
		{
			this.m_Queue->Clear();
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x0001AC3E File Offset: 0x00018E3E
		public unsafe readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Queue != null && this.m_Queue->IsCreated;
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001AC57 File Offset: 0x00018E57
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			UnsafeQueue<T>.Free(this.m_Queue);
			this.m_Queue = null;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001AC78 File Offset: 0x00018E78
		public unsafe JobHandle Dispose(JobHandle inputDeps)
		{
			if (!this.IsCreated)
			{
				return inputDeps;
			}
			JobHandle jobHandle = new NativeQueueDisposeJob
			{
				Data = new NativeQueueDispose
				{
					m_QueueData = (UnsafeQueue<int>*)this.m_Queue
				}
			}.Schedule(inputDeps);
			this.m_Queue = null;
			return jobHandle;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001ACC3 File Offset: 0x00018EC3
		public NativeQueue<T>.ReadOnly AsReadOnly()
		{
			return new NativeQueue<T>.ReadOnly(ref this);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001ACCC File Offset: 0x00018ECC
		public unsafe NativeQueue<T>.ParallelWriter AsParallelWriter()
		{
			NativeQueue<T>.ParallelWriter writer;
			writer.unsafeWriter = this.m_Queue->AsParallelWriter();
			return writer;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private readonly void CheckRead()
		{
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckWrite()
		{
		}

		// Token: 0x040003DE RID: 990
		[NativeDisableUnsafePtrRestriction]
		private unsafe UnsafeQueue<T>* m_Queue;

		// Token: 0x020000B6 RID: 182
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x060008C7 RID: 2247 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x060008C8 RID: 2248 RVA: 0x0001ACEC File Offset: 0x00018EEC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x060008C9 RID: 2249 RVA: 0x0001ACF9 File Offset: 0x00018EF9
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x060008CA RID: 2250 RVA: 0x0001AD06 File Offset: 0x00018F06
			public T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Enumerator.Current;
				}
			}

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x060008CB RID: 2251 RVA: 0x0001AD13 File Offset: 0x00018F13
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040003DF RID: 991
			internal UnsafeQueue<T>.Enumerator m_Enumerator;
		}

		// Token: 0x020000B7 RID: 183
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct ReadOnly : IEnumerable<T>, IEnumerable
		{
			// Token: 0x060008CC RID: 2252 RVA: 0x0001AD20 File Offset: 0x00018F20
			internal unsafe ReadOnly(ref NativeQueue<T> data)
			{
				this.m_ReadOnly = new UnsafeQueue<T>.ReadOnly(ref *data.m_Queue);
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x060008CD RID: 2253 RVA: 0x0001AD33 File Offset: 0x00018F33
			public readonly bool IsCreated
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_ReadOnly.IsCreated;
				}
			}

			// Token: 0x060008CE RID: 2254 RVA: 0x0001AD40 File Offset: 0x00018F40
			public readonly bool IsEmpty()
			{
				return this.m_ReadOnly.IsEmpty();
			}

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x060008CF RID: 2255 RVA: 0x0001AD4D File Offset: 0x00018F4D
			public readonly int Count
			{
				get
				{
					return this.m_ReadOnly.Count;
				}
			}

			// Token: 0x17000103 RID: 259
			public readonly T this[int index]
			{
				get
				{
					return this.m_ReadOnly[index];
				}
			}

			// Token: 0x060008D1 RID: 2257 RVA: 0x0001AD68 File Offset: 0x00018F68
			public readonly NativeQueue<T>.Enumerator GetEnumerator()
			{
				return new NativeQueue<T>.Enumerator
				{
					m_Enumerator = this.m_ReadOnly.GetEnumerator()
				};
			}

			// Token: 0x060008D2 RID: 2258 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008D3 RID: 2259 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008D4 RID: 2260 RVA: 0x00002C47 File Offset: 0x00000E47
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private readonly void CheckRead()
			{
			}

			// Token: 0x040003E0 RID: 992
			private UnsafeQueue<T>.ReadOnly m_ReadOnly;
		}

		// Token: 0x020000B8 RID: 184
		[NativeContainer]
		[NativeContainerIsAtomicWriteOnly]
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ParallelWriter
		{
			// Token: 0x060008D5 RID: 2261 RVA: 0x0001AD90 File Offset: 0x00018F90
			public void Enqueue(T value)
			{
				this.unsafeWriter.Enqueue(value);
			}

			// Token: 0x060008D6 RID: 2262 RVA: 0x0001AD9E File Offset: 0x00018F9E
			internal void Enqueue(T value, int threadIndexOverride)
			{
				this.unsafeWriter.Enqueue(value, threadIndexOverride);
			}

			// Token: 0x040003E1 RID: 993
			internal UnsafeQueue<T>.ParallelWriter unsafeWriter;
		}
	}
}
