using System;
using System.Threading;

namespace Unity.Collections
{
	// Token: 0x020000ED RID: 237
	[GenerateTestsForBurstCompatibility]
	internal struct UnsafeQueueBlockPoolData
	{
		// Token: 0x06000A4B RID: 2635 RVA: 0x0001ECD8 File Offset: 0x0001CED8
		public unsafe UnsafeQueueBlockHeader* AllocateBlock()
		{
			while (Interlocked.CompareExchange(ref this.m_AllocLock, 1, 0) != 0)
			{
			}
			UnsafeQueueBlockHeader* checkBlock = (UnsafeQueueBlockHeader*)(void*)this.m_FirstBlock;
			UnsafeQueueBlockHeader* block;
			for (;;)
			{
				block = checkBlock;
				if (block == null)
				{
					break;
				}
				checkBlock = (UnsafeQueueBlockHeader*)(void*)Interlocked.CompareExchange(ref this.m_FirstBlock, (IntPtr)((void*)block->m_NextBlock), (IntPtr)((void*)block));
				if (checkBlock == block)
				{
					goto Block_2;
				}
			}
			Interlocked.Exchange(ref this.m_AllocLock, 0);
			Interlocked.Increment(ref this.m_NumBlocks);
			return (UnsafeQueueBlockHeader*)Memory.Unmanaged.Allocate(16384L, 16, Allocator.Persistent);
			Block_2:
			Interlocked.Exchange(ref this.m_AllocLock, 0);
			return block;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001ED6C File Offset: 0x0001CF6C
		public unsafe void FreeBlock(UnsafeQueueBlockHeader* block)
		{
			if (this.m_NumBlocks > this.m_MaxBlocks)
			{
				if (Interlocked.Decrement(ref this.m_NumBlocks) + 1 > this.m_MaxBlocks)
				{
					Memory.Unmanaged.Free<UnsafeQueueBlockHeader>(block, Allocator.Persistent);
					return;
				}
				Interlocked.Increment(ref this.m_NumBlocks);
			}
			UnsafeQueueBlockHeader* checkBlock = (UnsafeQueueBlockHeader*)(void*)this.m_FirstBlock;
			UnsafeQueueBlockHeader* nextPtr;
			do
			{
				nextPtr = checkBlock;
				block->m_NextBlock = checkBlock;
				checkBlock = (UnsafeQueueBlockHeader*)(void*)Interlocked.CompareExchange(ref this.m_FirstBlock, (IntPtr)((void*)block), (IntPtr)((void*)checkBlock));
			}
			while (checkBlock != nextPtr);
		}

		// Token: 0x0400045B RID: 1115
		internal IntPtr m_FirstBlock;

		// Token: 0x0400045C RID: 1116
		internal int m_NumBlocks;

		// Token: 0x0400045D RID: 1117
		internal int m_MaxBlocks;

		// Token: 0x0400045E RID: 1118
		internal const int m_BlockSize = 16384;

		// Token: 0x0400045F RID: 1119
		internal int m_AllocLock;
	}
}
