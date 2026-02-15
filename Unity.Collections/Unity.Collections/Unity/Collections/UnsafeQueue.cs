using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000F0 RID: 240
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct UnsafeQueue<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable where T : struct, ValueType
	{
		// Token: 0x06000A57 RID: 2647 RVA: 0x0001F0AD File Offset: 0x0001D2AD
		public UnsafeQueue(AllocatorManager.AllocatorHandle allocator)
		{
			this.m_QueuePool = UnsafeQueueBlockPool.GetQueueBlockPool();
			this.m_AllocatorLabel = allocator;
			UnsafeQueueData.AllocateQueue<T>(allocator, out this.m_Buffer);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001F0D0 File Offset: 0x0001D2D0
		internal unsafe static UnsafeQueue<T>* Alloc(AllocatorManager.AllocatorHandle allocator)
		{
			return (UnsafeQueue<T>*)Memory.Unmanaged.Allocate((long)sizeof(UnsafeQueue<T>), UnsafeUtility.AlignOf<UnsafeQueue<T>>(), allocator);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001F0F4 File Offset: 0x0001D2F4
		internal unsafe static void Free(UnsafeQueue<T>* data)
		{
			if (data == null)
			{
				throw new InvalidOperationException("UnsafeQueue has yet to be created or has been destroyed!");
			}
			AllocatorManager.AllocatorHandle allocator = data->m_AllocatorLabel;
			data->Dispose();
			Memory.Unmanaged.Free<UnsafeQueue<T>>(data, allocator);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001F128 File Offset: 0x0001D328
		public unsafe readonly bool IsEmpty()
		{
			if (this.IsCreated)
			{
				int count = 0;
				int currentRead = this.m_Buffer->m_CurrentRead;
				for (UnsafeQueueBlockHeader* block = (UnsafeQueueBlockHeader*)(void*)this.m_Buffer->m_FirstBlock; block != null; block = block->m_NextBlock)
				{
					count += block->m_NumItems;
					if (count > currentRead)
					{
						return false;
					}
				}
				return count == currentRead;
			}
			return true;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0001F180 File Offset: 0x0001D380
		public unsafe readonly int Count
		{
			get
			{
				int count = 0;
				for (UnsafeQueueBlockHeader* block = (UnsafeQueueBlockHeader*)(void*)this.m_Buffer->m_FirstBlock; block != null; block = block->m_NextBlock)
				{
					count += block->m_NumItems;
				}
				return count - this.m_Buffer->m_CurrentRead;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0001F1C4 File Offset: 0x0001D3C4
		// (set) Token: 0x06000A5D RID: 2653 RVA: 0x0001F1D0 File Offset: 0x0001D3D0
		internal unsafe static int PersistentMemoryBlockCount
		{
			get
			{
				return UnsafeQueueBlockPool.GetQueueBlockPool()->m_MaxBlocks;
			}
			set
			{
				Interlocked.Exchange(ref UnsafeQueueBlockPool.GetQueueBlockPool()->m_MaxBlocks, value);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0001F1E3 File Offset: 0x0001D3E3
		internal static int MemoryBlockSize
		{
			get
			{
				return 16384;
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001F1EC File Offset: 0x0001D3EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe T Peek()
		{
			UnsafeQueueBlockHeader* firstBlock = (UnsafeQueueBlockHeader*)(void*)this.m_Buffer->m_FirstBlock;
			return UnsafeUtility.ReadArrayElement<T>((void*)(firstBlock + 1), this.m_Buffer->m_CurrentRead);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001F224 File Offset: 0x0001D424
		public unsafe void Enqueue(T value)
		{
			UnsafeQueueBlockHeader* writeBlock = UnsafeQueueData.AllocateWriteBlockMT<T>(this.m_Buffer, this.m_QueuePool, 0);
			UnsafeUtility.WriteArrayElement<T>((void*)(writeBlock + 1), writeBlock->m_NumItems, value);
			writeBlock->m_NumItems++;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001F264 File Offset: 0x0001D464
		public T Dequeue()
		{
			T item;
			this.TryDequeue(out item);
			return item;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001F27C File Offset: 0x0001D47C
		public unsafe bool TryDequeue(out T item)
		{
			UnsafeQueueBlockHeader* firstBlock = (UnsafeQueueBlockHeader*)(void*)this.m_Buffer->m_FirstBlock;
			if (firstBlock != null)
			{
				UnsafeQueueData* buffer = this.m_Buffer;
				int currentRead2 = buffer->m_CurrentRead;
				buffer->m_CurrentRead = currentRead2 + 1;
				int currentRead = currentRead2;
				int numItems = firstBlock->m_NumItems;
				item = UnsafeUtility.ReadArrayElement<T>((void*)(firstBlock + 1), currentRead);
				if (currentRead + 1 >= numItems)
				{
					this.m_Buffer->m_CurrentRead = 0;
					this.m_Buffer->m_FirstBlock = (IntPtr)((void*)firstBlock->m_NextBlock);
					if (this.m_Buffer->m_FirstBlock == IntPtr.Zero)
					{
						this.m_Buffer->m_LastBlock = IntPtr.Zero;
					}
					int maxThreadCount = JobsUtility.ThreadIndexCount;
					for (int threadIndex = 0; threadIndex < maxThreadCount; threadIndex++)
					{
						if (this.m_Buffer->GetCurrentWriteBlockTLS(threadIndex) == firstBlock)
						{
							this.m_Buffer->SetCurrentWriteBlockTLS(threadIndex, null);
						}
					}
					this.m_QueuePool->FreeBlock(firstBlock);
				}
				return true;
			}
			item = default(T);
			return false;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001F370 File Offset: 0x0001D570
		public unsafe NativeArray<T> ToArray(AllocatorManager.AllocatorHandle allocator)
		{
			UnsafeQueueBlockHeader* firstBlock = (UnsafeQueueBlockHeader*)(void*)this.m_Buffer->m_FirstBlock;
			NativeArray<T> outputArray = CollectionHelper.CreateNativeArray<T>(this.Count, allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeQueueBlockHeader* currentBlock = firstBlock;
			byte* arrayPtr = (byte*)outputArray.GetUnsafePtr<T>();
			int size = UnsafeUtility.SizeOf<T>();
			int dstOffset = 0;
			int srcOffset = this.m_Buffer->m_CurrentRead * size;
			int srcOffsetElements = this.m_Buffer->m_CurrentRead;
			while (currentBlock != null)
			{
				int bytesToCopy = (currentBlock->m_NumItems - srcOffsetElements) * size;
				UnsafeUtility.MemCpy((void*)(arrayPtr + dstOffset), (void*)(currentBlock + 1 + srcOffset / sizeof(UnsafeQueueBlockHeader)), (long)bytesToCopy);
				srcOffsetElements = (srcOffset = 0);
				dstOffset += bytesToCopy;
				currentBlock = currentBlock->m_NextBlock;
			}
			return outputArray;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0001F410 File Offset: 0x0001D610
		public unsafe void Clear()
		{
			UnsafeQueueBlockHeader* nextBlock;
			for (UnsafeQueueBlockHeader* firstBlock = (UnsafeQueueBlockHeader*)(void*)this.m_Buffer->m_FirstBlock; firstBlock != null; firstBlock = nextBlock)
			{
				nextBlock = firstBlock->m_NextBlock;
				this.m_QueuePool->FreeBlock(firstBlock);
			}
			this.m_Buffer->m_FirstBlock = IntPtr.Zero;
			this.m_Buffer->m_LastBlock = IntPtr.Zero;
			this.m_Buffer->m_CurrentRead = 0;
			int maxThreadCount = JobsUtility.ThreadIndexCount;
			for (int threadIndex = 0; threadIndex < maxThreadCount; threadIndex++)
			{
				this.m_Buffer->SetCurrentWriteBlockTLS(threadIndex, null);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0001F494 File Offset: 0x0001D694
		public readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Buffer != null;
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001F4A3 File Offset: 0x0001D6A3
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			UnsafeQueueData.DeallocateQueue(this.m_Buffer, this.m_QueuePool, this.m_AllocatorLabel);
			this.m_Buffer = null;
			this.m_QueuePool = null;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001F4D8 File Offset: 0x0001D6D8
		public JobHandle Dispose(JobHandle inputDeps)
		{
			if (!this.IsCreated)
			{
				return inputDeps;
			}
			JobHandle jobHandle = new UnsafeQueueDisposeJob
			{
				Data = new UnsafeQueueDispose
				{
					m_Buffer = this.m_Buffer,
					m_QueuePool = this.m_QueuePool,
					m_AllocatorLabel = this.m_AllocatorLabel
				}
			}.Schedule(inputDeps);
			this.m_Buffer = null;
			this.m_QueuePool = null;
			return jobHandle;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0001F545 File Offset: 0x0001D745
		public UnsafeQueue<T>.ReadOnly AsReadOnly()
		{
			return new UnsafeQueue<T>.ReadOnly(ref this);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001F550 File Offset: 0x0001D750
		public UnsafeQueue<T>.ParallelWriter AsParallelWriter()
		{
			UnsafeQueue<T>.ParallelWriter writer;
			writer.m_Buffer = this.m_Buffer;
			writer.m_QueuePool = this.m_QueuePool;
			writer.m_ThreadIndex = 0;
			return writer;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001F580 File Offset: 0x0001D780
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void CheckNotEmpty()
		{
			this.m_Buffer->m_FirstBlock == (IntPtr)0;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001F599 File Offset: 0x0001D799
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void ThrowEmpty()
		{
			throw new InvalidOperationException("Trying to read from an empty queue.");
		}

		// Token: 0x04000466 RID: 1126
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeQueueData* m_Buffer;

		// Token: 0x04000467 RID: 1127
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeQueueBlockPoolData* m_QueuePool;

		// Token: 0x04000468 RID: 1128
		internal AllocatorManager.AllocatorHandle m_AllocatorLabel;

		// Token: 0x020000F1 RID: 241
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000A6C RID: 2668 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x06000A6D RID: 2669 RVA: 0x0001F5A8 File Offset: 0x0001D7A8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public unsafe bool MoveNext()
			{
				this.m_Index++;
				while (this.m_Block != null)
				{
					int numItems = this.m_Block->m_NumItems;
					if (this.m_Index < numItems)
					{
						this.value = UnsafeUtility.ReadArrayElement<T>((void*)(this.m_Block + 1), this.m_Index);
						return true;
					}
					this.m_Index -= numItems;
					this.m_Block = this.m_Block->m_NextBlock;
				}
				this.value = default(T);
				return false;
			}

			// Token: 0x06000A6E RID: 2670 RVA: 0x0001F630 File Offset: 0x0001D830
			public void Reset()
			{
				this.m_Block = this.m_FirstBlock;
				this.m_Index = -1;
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0001F645 File Offset: 0x0001D845
			public T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.value;
				}
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x06000A70 RID: 2672 RVA: 0x0001F64D File Offset: 0x0001D84D
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000469 RID: 1129
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeQueueBlockHeader* m_FirstBlock;

			// Token: 0x0400046A RID: 1130
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeQueueBlockHeader* m_Block;

			// Token: 0x0400046B RID: 1131
			internal int m_Index;

			// Token: 0x0400046C RID: 1132
			private T value;
		}

		// Token: 0x020000F2 RID: 242
		public struct ReadOnly : IEnumerable<T>, IEnumerable
		{
			// Token: 0x06000A71 RID: 2673 RVA: 0x0001F65A File Offset: 0x0001D85A
			internal ReadOnly(ref UnsafeQueue<T> data)
			{
				this.m_Buffer = data.m_Buffer;
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0001F668 File Offset: 0x0001D868
			public readonly bool IsCreated
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Buffer != null;
				}
			}

			// Token: 0x06000A73 RID: 2675 RVA: 0x0001F678 File Offset: 0x0001D878
			public unsafe readonly bool IsEmpty()
			{
				int count = 0;
				int currentRead = this.m_Buffer->m_CurrentRead;
				for (UnsafeQueueBlockHeader* block = (UnsafeQueueBlockHeader*)(void*)this.m_Buffer->m_FirstBlock; block != null; block = block->m_NextBlock)
				{
					count += block->m_NumItems;
					if (count > currentRead)
					{
						return false;
					}
				}
				return count == currentRead;
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0001F6C8 File Offset: 0x0001D8C8
			public unsafe readonly int Count
			{
				get
				{
					int count = 0;
					for (UnsafeQueueBlockHeader* block = (UnsafeQueueBlockHeader*)(void*)this.m_Buffer->m_FirstBlock; block != null; block = block->m_NextBlock)
					{
						count += block->m_NumItems;
					}
					return count - this.m_Buffer->m_CurrentRead;
				}
			}

			// Token: 0x17000137 RID: 311
			public readonly T this[int index]
			{
				get
				{
					T result;
					this.TryGetValue(index, out result);
					return result;
				}
			}

			// Token: 0x06000A76 RID: 2678 RVA: 0x0001F724 File Offset: 0x0001D924
			private unsafe readonly bool TryGetValue(int index, out T item)
			{
				if (index >= 0)
				{
					int idx = index;
					for (UnsafeQueueBlockHeader* block = (UnsafeQueueBlockHeader*)(void*)this.m_Buffer->m_FirstBlock; block != null; block = block->m_NextBlock)
					{
						int numItems = block->m_NumItems;
						if (idx < numItems)
						{
							item = UnsafeUtility.ReadArrayElement<T>((void*)(block + 1), idx);
							return true;
						}
						idx -= numItems;
					}
				}
				item = default(T);
				return false;
			}

			// Token: 0x06000A77 RID: 2679 RVA: 0x0001F784 File Offset: 0x0001D984
			public unsafe readonly UnsafeQueue<T>.Enumerator GetEnumerator()
			{
				return new UnsafeQueue<T>.Enumerator
				{
					m_FirstBlock = (UnsafeQueueBlockHeader*)(void*)this.m_Buffer->m_FirstBlock,
					m_Block = (UnsafeQueueBlockHeader*)(void*)this.m_Buffer->m_FirstBlock,
					m_Index = -1
				};
			}

			// Token: 0x06000A78 RID: 2680 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A79 RID: 2681 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A7A RID: 2682 RVA: 0x0001F7D0 File Offset: 0x0001D9D0
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private readonly void ThrowIndexOutOfRangeException(int index)
			{
				throw new IndexOutOfRangeException(string.Format("Index {0} is out of bounds [0-{1}].", index, this.Count));
			}

			// Token: 0x0400046D RID: 1133
			[NativeDisableUnsafePtrRestriction]
			private unsafe UnsafeQueueData* m_Buffer;
		}

		// Token: 0x020000F3 RID: 243
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ParallelWriter
		{
			// Token: 0x06000A7B RID: 2683 RVA: 0x0001F7F4 File Offset: 0x0001D9F4
			public unsafe void Enqueue(T value)
			{
				UnsafeQueueBlockHeader* writeBlock = UnsafeQueueData.AllocateWriteBlockMT<T>(this.m_Buffer, this.m_QueuePool, this.m_ThreadIndex);
				UnsafeUtility.WriteArrayElement<T>((void*)(writeBlock + 1), writeBlock->m_NumItems, value);
				writeBlock->m_NumItems++;
			}

			// Token: 0x06000A7C RID: 2684 RVA: 0x0001F838 File Offset: 0x0001DA38
			internal unsafe void Enqueue(T value, int threadIndexOverride)
			{
				UnsafeQueueBlockHeader* writeBlock = UnsafeQueueData.AllocateWriteBlockMT<T>(this.m_Buffer, this.m_QueuePool, threadIndexOverride);
				UnsafeUtility.WriteArrayElement<T>((void*)(writeBlock + 1), writeBlock->m_NumItems, value);
				writeBlock->m_NumItems++;
			}

			// Token: 0x0400046E RID: 1134
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeQueueData* m_Buffer;

			// Token: 0x0400046F RID: 1135
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeQueueBlockPoolData* m_QueuePool;

			// Token: 0x04000470 RID: 1136
			[NativeSetThreadIndex]
			internal int m_ThreadIndex;
		}
	}
}
