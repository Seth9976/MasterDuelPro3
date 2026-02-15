using System;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000EE RID: 238
	internal class UnsafeQueueBlockPool
	{
		// Token: 0x06000A4D RID: 2637 RVA: 0x0001EDEC File Offset: 0x0001CFEC
		internal unsafe static UnsafeQueueBlockPoolData* GetQueueBlockPool()
		{
			UnsafeQueueBlockPoolData** pData = (UnsafeQueueBlockPoolData**)UnsafeQueueBlockPool.Data.UnsafeDataPointer;
			UnsafeQueueBlockPoolData* data = *(IntPtr*)pData;
			if (data == null)
			{
				data = (UnsafeQueueBlockPoolData*)Memory.Unmanaged.Allocate((long)UnsafeUtility.SizeOf<UnsafeQueueBlockPoolData>(), 8, Allocator.Persistent);
				*(IntPtr*)pData = data;
				data->m_NumBlocks = (data->m_MaxBlocks = 256);
				data->m_AllocLock = 0;
				UnsafeQueueBlockHeader* prev = null;
				for (int i = 0; i < data->m_MaxBlocks; i++)
				{
					UnsafeQueueBlockHeader* block = (UnsafeQueueBlockHeader*)Memory.Unmanaged.Allocate(16384L, 16, Allocator.Persistent);
					block->m_NextBlock = prev;
					prev = block;
				}
				data->m_FirstBlock = (IntPtr)((void*)prev);
				UnsafeQueueBlockPool.AppDomainOnDomainUnload();
			}
			return data;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0001EE87 File Offset: 0x0001D087
		[BurstDiscard]
		private static void AppDomainOnDomainUnload()
		{
			AppDomain.CurrentDomain.DomainUnload += UnsafeQueueBlockPool.OnDomainUnload;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001EEA0 File Offset: 0x0001D0A0
		private unsafe static void OnDomainUnload(object sender, EventArgs e)
		{
			UnsafeQueueBlockPoolData** pData = (UnsafeQueueBlockPoolData**)UnsafeQueueBlockPool.Data.UnsafeDataPointer;
			UnsafeQueueBlockPoolData* data = *(IntPtr*)pData;
			while (data->m_FirstBlock != IntPtr.Zero)
			{
				UnsafeQueueBlockHeader* block = (UnsafeQueueBlockHeader*)(void*)data->m_FirstBlock;
				data->m_FirstBlock = (IntPtr)((void*)block->m_NextBlock);
				Memory.Unmanaged.Free<UnsafeQueueBlockHeader>(block, Allocator.Persistent);
				data->m_NumBlocks--;
			}
			Memory.Unmanaged.Free<UnsafeQueueBlockPoolData>(data, Allocator.Persistent);
			*(IntPtr*)pData = (IntPtr)((UIntPtr)0);
		}

		// Token: 0x04000460 RID: 1120
		private static readonly SharedStatic<IntPtr> Data = SharedStatic<IntPtr>.GetOrCreateUnsafe(0U, 8615650021869908731L, 0L);
	}
}
