using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000EF RID: 239
	[GenerateTestsForBurstCompatibility]
	internal struct UnsafeQueueData
	{
		// Token: 0x06000A52 RID: 2642 RVA: 0x0001EF34 File Offset: 0x0001D134
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe UnsafeQueueBlockHeader* GetCurrentWriteBlockTLS(int threadIndex)
		{
			UnsafeQueueBlockHeader** data = (UnsafeQueueBlockHeader**)(this.m_CurrentWriteBlockTLS + threadIndex * 64);
			return *(IntPtr*)data;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001EF50 File Offset: 0x0001D150
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe void SetCurrentWriteBlockTLS(int threadIndex, UnsafeQueueBlockHeader* currentWriteBlock)
		{
			UnsafeQueueBlockHeader** data = (UnsafeQueueBlockHeader**)(this.m_CurrentWriteBlockTLS + threadIndex * 64);
			*(IntPtr*)data = currentWriteBlock;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0001EF70 File Offset: 0x0001D170
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static UnsafeQueueBlockHeader* AllocateWriteBlockMT<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(UnsafeQueueData* data, UnsafeQueueBlockPoolData* pool, int threadIndex) where T : struct, ValueType
		{
			UnsafeQueueBlockHeader* currentWriteBlock = data->GetCurrentWriteBlockTLS(threadIndex);
			if (currentWriteBlock != null)
			{
				if (currentWriteBlock->m_NumItems != data->m_MaxItems)
				{
					return currentWriteBlock;
				}
				currentWriteBlock = null;
			}
			currentWriteBlock = pool->AllocateBlock();
			currentWriteBlock->m_NextBlock = null;
			currentWriteBlock->m_NumItems = 0;
			UnsafeQueueBlockHeader* prevLast = (UnsafeQueueBlockHeader*)(void*)Interlocked.Exchange(ref data->m_LastBlock, (IntPtr)((void*)currentWriteBlock));
			if (prevLast == null)
			{
				data->m_FirstBlock = (IntPtr)((void*)currentWriteBlock);
			}
			else
			{
				prevLast->m_NextBlock = currentWriteBlock;
			}
			data->SetCurrentWriteBlockTLS(threadIndex, currentWriteBlock);
			return currentWriteBlock;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0001EFF0 File Offset: 0x0001D1F0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void AllocateQueue<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(AllocatorManager.AllocatorHandle label, out UnsafeQueueData* outBuf) where T : struct, ValueType
		{
			int maxThreadCount = JobsUtility.ThreadIndexCount;
			int queueDataSize = CollectionHelper.Align(UnsafeUtility.SizeOf<UnsafeQueueData>(), 64);
			UnsafeQueueData* data = (UnsafeQueueData*)Memory.Unmanaged.Allocate((long)(queueDataSize + 64 * maxThreadCount), 64, label);
			data->m_CurrentWriteBlockTLS = (byte*)(data + queueDataSize / sizeof(UnsafeQueueData));
			data->m_FirstBlock = IntPtr.Zero;
			data->m_LastBlock = IntPtr.Zero;
			data->m_MaxItems = (16384 - UnsafeUtility.SizeOf<UnsafeQueueBlockHeader>()) / UnsafeUtility.SizeOf<T>();
			data->m_CurrentRead = 0;
			for (int threadIndex = 0; threadIndex < maxThreadCount; threadIndex++)
			{
				data->SetCurrentWriteBlockTLS(threadIndex, null);
			}
			outBuf = data;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001F078 File Offset: 0x0001D278
		public unsafe static void DeallocateQueue(UnsafeQueueData* data, UnsafeQueueBlockPoolData* pool, AllocatorManager.AllocatorHandle allocation)
		{
			UnsafeQueueBlockHeader* nextBlock;
			for (UnsafeQueueBlockHeader* firstBlock = (UnsafeQueueBlockHeader*)(void*)data->m_FirstBlock; firstBlock != null; firstBlock = nextBlock)
			{
				nextBlock = firstBlock->m_NextBlock;
				pool->FreeBlock(firstBlock);
			}
			Memory.Unmanaged.Free<UnsafeQueueData>(data, allocation);
		}

		// Token: 0x04000461 RID: 1121
		public IntPtr m_FirstBlock;

		// Token: 0x04000462 RID: 1122
		public IntPtr m_LastBlock;

		// Token: 0x04000463 RID: 1123
		public int m_MaxItems;

		// Token: 0x04000464 RID: 1124
		public int m_CurrentRead;

		// Token: 0x04000465 RID: 1125
		public unsafe byte* m_CurrentWriteBlockTLS;
	}
}
