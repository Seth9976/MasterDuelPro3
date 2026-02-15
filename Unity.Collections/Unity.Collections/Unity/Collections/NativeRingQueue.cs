using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000BF RID: 191
	[NativeContainer]
	[DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(NativeRingQueueDebugView<>))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct NativeRingQueue<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable where T : struct, ValueType
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0001B009 File Offset: 0x00019209
		public unsafe readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_RingQueue != null && this.m_RingQueue->IsCreated;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0001B022 File Offset: 0x00019222
		public unsafe readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_RingQueue == null || this.m_RingQueue->Length == 0;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0001B03E File Offset: 0x0001923E
		public unsafe readonly int Length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return CollectionHelper.AssumePositive(this.m_RingQueue->Length);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0001B050 File Offset: 0x00019250
		public unsafe readonly int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return CollectionHelper.AssumePositive(this.m_RingQueue->Capacity);
			}
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001B062 File Offset: 0x00019262
		public unsafe NativeRingQueue(int capacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			this.m_RingQueue = UnsafeRingQueue<T>.Alloc(allocator);
			*this.m_RingQueue = new UnsafeRingQueue<T>(capacity, allocator, options);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001B083 File Offset: 0x00019283
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			UnsafeRingQueue<T>.Free(this.m_RingQueue);
			this.m_RingQueue = null;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001B0A4 File Offset: 0x000192A4
		public unsafe JobHandle Dispose(JobHandle inputDeps)
		{
			if (!this.IsCreated)
			{
				return inputDeps;
			}
			JobHandle jobHandle = new NativeRingQueueDisposeJob
			{
				Data = new NativeRingQueueDispose
				{
					m_QueueData = (UnsafeRingQueue<int>*)this.m_RingQueue
				}
			}.Schedule(inputDeps);
			this.m_RingQueue = null;
			return jobHandle;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001B0EF File Offset: 0x000192EF
		public unsafe bool TryEnqueue(T value)
		{
			return this.m_RingQueue->TryEnqueue(value);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001B0FD File Offset: 0x000192FD
		public unsafe void Enqueue(T value)
		{
			this.m_RingQueue->Enqueue(value);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001B10B File Offset: 0x0001930B
		public unsafe bool TryDequeue(out T item)
		{
			return this.m_RingQueue->TryDequeue(out item);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001B119 File Offset: 0x00019319
		public unsafe T Dequeue()
		{
			return this.m_RingQueue->Dequeue();
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private readonly void CheckRead()
		{
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private readonly void CheckWrite()
		{
		}

		// Token: 0x040003EA RID: 1002
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeRingQueue<T>* m_RingQueue;
	}
}
