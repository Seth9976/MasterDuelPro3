using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000511 RID: 1297
	internal class BestFitAllocator
	{
		// Token: 0x06002420 RID: 9248 RVA: 0x00087D54 File Offset: 0x00085F54
		public BestFitAllocator(uint size)
		{
			this.totalSize = size;
			this.m_FirstBlock = (this.m_FirstAvailableBlock = this.m_BlockPool.Get());
			this.m_FirstAvailableBlock.end = size;
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x00087DA1 File Offset: 0x00085FA1
		public uint totalSize { get; }

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x00087DAC File Offset: 0x00085FAC
		public uint highWatermark
		{
			get
			{
				return this.m_HighWatermark;
			}
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x00087DC4 File Offset: 0x00085FC4
		public Alloc Allocate(uint size)
		{
			BestFitAllocator.Block block = this.BestFitFindAvailableBlock(size);
			bool flag = block == null;
			Alloc alloc;
			if (flag)
			{
				alloc = default(Alloc);
			}
			else
			{
				Debug.Assert(block.size >= size);
				Debug.Assert(!block.allocated);
				bool flag2 = size != block.size;
				if (flag2)
				{
					this.SplitBlock(block, size);
				}
				Debug.Assert(block.size == size);
				bool flag3 = block.end > this.m_HighWatermark;
				if (flag3)
				{
					this.m_HighWatermark = block.end;
				}
				bool flag4 = block == this.m_FirstAvailableBlock;
				if (flag4)
				{
					this.m_FirstAvailableBlock = this.m_FirstAvailableBlock.nextAvailable;
				}
				bool flag5 = block.prevAvailable != null;
				if (flag5)
				{
					block.prevAvailable.nextAvailable = block.nextAvailable;
				}
				bool flag6 = block.nextAvailable != null;
				if (flag6)
				{
					block.nextAvailable.prevAvailable = block.prevAvailable;
				}
				block.allocated = true;
				block.prevAvailable = (block.nextAvailable = null);
				alloc = new Alloc
				{
					start = block.start,
					size = block.size,
					handle = block
				};
			}
			return alloc;
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x00087F04 File Offset: 0x00086104
		public void Free(Alloc alloc)
		{
			BestFitAllocator.Block block = (BestFitAllocator.Block)alloc.handle;
			bool flag = !block.allocated;
			if (flag)
			{
				Debug.Assert(false, "Severe error: UIR allocation double-free");
			}
			else
			{
				Debug.Assert(block.allocated);
				Debug.Assert(block.start == alloc.start);
				Debug.Assert(block.size == alloc.size);
				bool flag2 = block.end == this.m_HighWatermark;
				if (flag2)
				{
					bool flag3 = block.prev != null;
					if (flag3)
					{
						this.m_HighWatermark = (block.prev.allocated ? block.prev.end : block.prev.start);
					}
					else
					{
						this.m_HighWatermark = 0U;
					}
				}
				block.allocated = false;
				BestFitAllocator.Block availableIt = this.m_FirstAvailableBlock;
				BestFitAllocator.Block availableBefore = null;
				while (availableIt != null && availableIt.start < block.start)
				{
					availableBefore = availableIt;
					availableIt = availableIt.nextAvailable;
				}
				bool flag4 = availableBefore == null;
				if (flag4)
				{
					Debug.Assert(block.prevAvailable == null);
					block.nextAvailable = this.m_FirstAvailableBlock;
					this.m_FirstAvailableBlock = block;
				}
				else
				{
					block.prevAvailable = availableBefore;
					block.nextAvailable = availableBefore.nextAvailable;
					availableBefore.nextAvailable = block;
				}
				bool flag5 = block.nextAvailable != null;
				if (flag5)
				{
					block.nextAvailable.prevAvailable = block;
				}
				bool flag6 = block.prevAvailable == block.prev && block.prev != null;
				if (flag6)
				{
					block = this.CoalesceBlockWithPrevious(block);
				}
				bool flag7 = block.nextAvailable == block.next && block.next != null;
				if (flag7)
				{
					block = this.CoalesceBlockWithPrevious(block.next);
				}
			}
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x000880C0 File Offset: 0x000862C0
		private BestFitAllocator.Block CoalesceBlockWithPrevious(BestFitAllocator.Block block)
		{
			Debug.Assert(block.prevAvailable.end == block.start);
			Debug.Assert(block.prev.nextAvailable == block);
			BestFitAllocator.Block prev = block.prev;
			prev.next = block.next;
			bool flag = block.next != null;
			if (flag)
			{
				block.next.prev = prev;
			}
			prev.nextAvailable = block.nextAvailable;
			bool flag2 = block.nextAvailable != null;
			if (flag2)
			{
				block.nextAvailable.prevAvailable = block.prevAvailable;
			}
			prev.end = block.end;
			this.m_BlockPool.Return(block);
			return prev;
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x00088170 File Offset: 0x00086370
		private BestFitAllocator.Block BestFitFindAvailableBlock(uint size)
		{
			BestFitAllocator.Block availableBlock = this.m_FirstAvailableBlock;
			BestFitAllocator.Block bestFit = null;
			uint bestFitBlockSize = uint.MaxValue;
			while (availableBlock != null)
			{
				bool flag = availableBlock.size >= size && bestFitBlockSize > availableBlock.size;
				if (flag)
				{
					bestFit = availableBlock;
					bestFitBlockSize = availableBlock.size;
				}
				availableBlock = availableBlock.nextAvailable;
			}
			return bestFit;
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x000881CC File Offset: 0x000863CC
		private void SplitBlock(BestFitAllocator.Block block, uint size)
		{
			Debug.Assert(block.size > size);
			BestFitAllocator.Block after = this.m_BlockPool.Get();
			after.next = block.next;
			after.nextAvailable = block.nextAvailable;
			after.prev = block;
			after.prevAvailable = block;
			after.start = block.start + size;
			after.end = block.end;
			bool flag = after.next != null;
			if (flag)
			{
				after.next.prev = after;
			}
			bool flag2 = after.nextAvailable != null;
			if (flag2)
			{
				after.nextAvailable.prevAvailable = after;
			}
			block.next = after;
			block.nextAvailable = after;
			block.end = after.start;
		}

		// Token: 0x040010C4 RID: 4292
		private BestFitAllocator.Block m_FirstBlock;

		// Token: 0x040010C5 RID: 4293
		private BestFitAllocator.Block m_FirstAvailableBlock;

		// Token: 0x040010C6 RID: 4294
		private BestFitAllocator.BlockPool m_BlockPool = new BestFitAllocator.BlockPool();

		// Token: 0x040010C7 RID: 4295
		private uint m_HighWatermark;

		// Token: 0x02000512 RID: 1298
		private class BlockPool : LinkedPool<BestFitAllocator.Block>
		{
			// Token: 0x06002428 RID: 9256 RVA: 0x00088284 File Offset: 0x00086484
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static BestFitAllocator.Block CreateBlock()
			{
				return new BestFitAllocator.Block();
			}

			// Token: 0x06002429 RID: 9257 RVA: 0x000020EA File Offset: 0x000002EA
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static void ResetBlock(BestFitAllocator.Block block)
			{
			}

			// Token: 0x0600242A RID: 9258 RVA: 0x0008829B File Offset: 0x0008649B
			public BlockPool()
				: base(new Func<BestFitAllocator.Block>(BestFitAllocator.BlockPool.CreateBlock), new Action<BestFitAllocator.Block>(BestFitAllocator.BlockPool.ResetBlock), 10000)
			{
			}
		}

		// Token: 0x02000513 RID: 1299
		private class Block : LinkedPoolItem<BestFitAllocator.Block>
		{
			// Token: 0x17000964 RID: 2404
			// (get) Token: 0x0600242B RID: 9259 RVA: 0x000882C4 File Offset: 0x000864C4
			public uint size
			{
				get
				{
					return this.end - this.start;
				}
			}

			// Token: 0x040010C8 RID: 4296
			public uint start;

			// Token: 0x040010C9 RID: 4297
			public uint end;

			// Token: 0x040010CA RID: 4298
			public BestFitAllocator.Block prev;

			// Token: 0x040010CB RID: 4299
			public BestFitAllocator.Block next;

			// Token: 0x040010CC RID: 4300
			public BestFitAllocator.Block prevAvailable;

			// Token: 0x040010CD RID: 4301
			public BestFitAllocator.Block nextAvailable;

			// Token: 0x040010CE RID: 4302
			public bool allocated;
		}
	}
}
