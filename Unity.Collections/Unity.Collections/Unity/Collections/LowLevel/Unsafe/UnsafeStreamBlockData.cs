using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000144 RID: 324
	[GenerateTestsForBurstCompatibility]
	internal struct UnsafeStreamBlockData
	{
		// Token: 0x06000D87 RID: 3463 RVA: 0x00029C50 File Offset: 0x00027E50
		internal unsafe UnsafeStreamBlock* Allocate(UnsafeStreamBlock* oldBlock, int threadIndex)
		{
			UnsafeStreamBlock* block = (UnsafeStreamBlock*)Memory.Unmanaged.Array.Resize(null, 0L, 4096L, this.Allocator, 1L, 16);
			block->Next = null;
			if (oldBlock == null)
			{
				block->Next = *(IntPtr*)(this.Blocks + (IntPtr)threadIndex * (IntPtr)sizeof(UnsafeStreamBlock*) / (IntPtr)sizeof(UnsafeStreamBlock*));
				*(IntPtr*)(this.Blocks + (IntPtr)threadIndex * (IntPtr)sizeof(UnsafeStreamBlock*) / (IntPtr)sizeof(UnsafeStreamBlock*)) = block;
			}
			else
			{
				block->Next = oldBlock->Next;
				oldBlock->Next = block;
			}
			return block;
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00029CC3 File Offset: 0x00027EC3
		internal unsafe void Free(UnsafeStreamBlock* oldBlock)
		{
			Memory.Unmanaged.Array.Resize((void*)oldBlock, 4096L, 0L, this.Allocator, 1L, 16);
		}

		// Token: 0x04000530 RID: 1328
		internal const int AllocationSize = 4096;

		// Token: 0x04000531 RID: 1329
		internal AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x04000532 RID: 1330
		internal unsafe UnsafeStreamBlock** Blocks;

		// Token: 0x04000533 RID: 1331
		internal int BlockCount;

		// Token: 0x04000534 RID: 1332
		internal AllocatorManager.Block Ranges;

		// Token: 0x04000535 RID: 1333
		internal int RangeCount;
	}
}
