using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200013E RID: 318
	[DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(UnsafeRingQueueDebugView<>))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct UnsafeRingQueue<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable where T : struct, ValueType
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x00029834 File Offset: 0x00027A34
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Filled == 0;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0002983F File Offset: 0x00027A3F
		public readonly int Length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Filled;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x00029847 File Offset: 0x00027A47
		public readonly int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Capacity;
			}
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0002984F File Offset: 0x00027A4F
		public unsafe UnsafeRingQueue(T* ptr, int capacity)
		{
			this.Ptr = ptr;
			this.Allocator = AllocatorManager.None;
			this.m_Capacity = capacity;
			this.m_Filled = 0;
			this.m_Write = 0;
			this.m_Read = 0;
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00029880 File Offset: 0x00027A80
		public unsafe UnsafeRingQueue(int capacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			this.Allocator = allocator;
			this.m_Capacity = capacity;
			this.m_Filled = 0;
			this.m_Write = 0;
			this.m_Read = 0;
			int sizeInBytes = capacity * UnsafeUtility.SizeOf<T>();
			this.Ptr = (T*)Memory.Unmanaged.Allocate((long)sizeInBytes, 16, allocator);
			if (options == NativeArrayOptions.ClearMemory)
			{
				UnsafeUtility.MemClear((void*)this.Ptr, (long)sizeInBytes);
			}
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x000298DC File Offset: 0x00027ADC
		internal unsafe static UnsafeRingQueue<T>* Alloc(AllocatorManager.AllocatorHandle allocator)
		{
			return (UnsafeRingQueue<T>*)Memory.Unmanaged.Allocate((long)sizeof(UnsafeRingQueue<T>), UnsafeUtility.AlignOf<UnsafeRingQueue<T>>(), allocator);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00029900 File Offset: 0x00027B00
		internal unsafe static void Free(UnsafeRingQueue<T>* data)
		{
			if (data == null)
			{
				throw new InvalidOperationException("UnsafeRingQueue has yet to be created or has been destroyed!");
			}
			AllocatorManager.AllocatorHandle allocator = data->Allocator;
			data->Dispose();
			Memory.Unmanaged.Free<UnsafeRingQueue<T>>(data, allocator);
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00029931 File Offset: 0x00027B31
		public readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.Ptr != null;
			}
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00029940 File Offset: 0x00027B40
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				Memory.Unmanaged.Free<T>(this.Ptr, this.Allocator);
				this.Allocator = AllocatorManager.Invalid;
			}
			this.Ptr = null;
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0002997C File Offset: 0x00027B7C
		public unsafe JobHandle Dispose(JobHandle inputDeps)
		{
			if (!this.IsCreated)
			{
				return inputDeps;
			}
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				JobHandle jobHandle = new UnsafeDisposeJob
				{
					Ptr = (void*)this.Ptr,
					Allocator = this.Allocator
				}.Schedule(inputDeps);
				this.Ptr = null;
				this.Allocator = AllocatorManager.Invalid;
				return jobHandle;
			}
			this.Ptr = null;
			return inputDeps;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x000299E8 File Offset: 0x00027BE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe bool TryEnqueueInternal(T value)
		{
			if (this.m_Filled == this.m_Capacity)
			{
				return false;
			}
			this.Ptr[(IntPtr)this.m_Write * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = value;
			this.m_Write++;
			if (this.m_Write == this.m_Capacity)
			{
				this.m_Write = 0;
			}
			this.m_Filled++;
			return true;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00029A52 File Offset: 0x00027C52
		public bool TryEnqueue(T value)
		{
			return this.TryEnqueueInternal(value);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00029A5B File Offset: 0x00027C5B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void ThrowQueueFull()
		{
			throw new InvalidOperationException("Trying to enqueue into full queue.");
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00029A67 File Offset: 0x00027C67
		public void Enqueue(T value)
		{
			this.TryEnqueueInternal(value);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00029A74 File Offset: 0x00027C74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe bool TryDequeueInternal(out T item)
		{
			item = this.Ptr[(IntPtr)this.m_Read * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
			if (this.m_Filled == 0)
			{
				return false;
			}
			this.m_Read++;
			if (this.m_Read == this.m_Capacity)
			{
				this.m_Read = 0;
			}
			this.m_Filled--;
			return true;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00029ADD File Offset: 0x00027CDD
		public bool TryDequeue(out T item)
		{
			return this.TryDequeueInternal(out item);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00029AE6 File Offset: 0x00027CE6
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void ThrowQueueEmpty()
		{
			throw new InvalidOperationException("Trying to dequeue from an empty queue");
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00029AF4 File Offset: 0x00027CF4
		public T Dequeue()
		{
			T item;
			this.TryDequeueInternal(out item);
			return item;
		}

		// Token: 0x0400051E RID: 1310
		[NativeDisableUnsafePtrRestriction]
		public unsafe T* Ptr;

		// Token: 0x0400051F RID: 1311
		public AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x04000520 RID: 1312
		internal readonly int m_Capacity;

		// Token: 0x04000521 RID: 1313
		internal int m_Filled;

		// Token: 0x04000522 RID: 1314
		internal int m_Write;

		// Token: 0x04000523 RID: 1315
		internal int m_Read;
	}
}
