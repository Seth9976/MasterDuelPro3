using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200013B RID: 315
	internal class ProbeVolumeScratchBufferPool
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x000205B2 File Offset: 0x0001E7B2
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x000205BA File Offset: 0x0001E7BA
		public int chunkSize { get; private set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x000205C3 File Offset: 0x0001E7C3
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x000205CB File Offset: 0x0001E7CB
		public int maxChunkCount { get; private set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x000205D4 File Offset: 0x0001E7D4
		public int allocatedMemory
		{
			get
			{
				return this.chunkSize * this.m_CurrentlyAllocatedChunkCount;
			}
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x000205E4 File Offset: 0x0001E7E4
		public ProbeVolumeScratchBufferPool(ProbeVolumeBakingSet bakingSet, ProbeVolumeSHBands shBands)
		{
			this.chunkSize = bakingSet.GetChunkGPUMemory(shBands);
			this.maxChunkCount = bakingSet.maxSHChunkCount;
			this.m_L0Size = bakingSet.L0ChunkSize;
			this.m_L1Size = bakingSet.L1ChunkSize;
			this.m_ValiditySize = bakingSet.sharedValidityMaskChunkSize;
			this.m_ValidityLayerCount = bakingSet.bakedMaskCount;
			this.m_SkyOcclusionSize = bakingSet.sharedSkyOcclusionL0L1ChunkSize;
			this.m_SkyShadingDirectionSize = bakingSet.sharedSkyShadingDirectionIndicesChunkSize;
			this.m_L2Size = bakingSet.L2TextureChunkSize;
			this.m_ProbeOcclusionSize = bakingSet.ProbeOcclusionChunkSize;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00020688 File Offset: 0x0001E888
		private ProbeReferenceVolume.CellStreamingScratchBufferLayout GetOrCreateScratchBufferLayout(int chunkCount)
		{
			ProbeReferenceVolume.CellStreamingScratchBufferLayout layout;
			if (this.m_Layouts.TryGetValue(chunkCount, out layout))
			{
				return layout;
			}
			ProbeReferenceVolume.CellStreamingScratchBufferLayout bufferLayout = default(ProbeReferenceVolume.CellStreamingScratchBufferLayout);
			bufferLayout._L0Size = this.m_L0Size;
			bufferLayout._L1Size = this.m_L1Size;
			bufferLayout._ValiditySize = this.m_ValiditySize;
			bufferLayout._ValidityProbeSize = this.m_ValidityLayerCount;
			if (this.m_SkyOcclusionSize != 0)
			{
				bufferLayout._SkyOcclusionSize = this.m_SkyOcclusionSize;
				bufferLayout._SkyOcclusionProbeSize = 8;
				if (this.m_SkyShadingDirectionSize != 0)
				{
					bufferLayout._SkyShadingDirectionSize = this.m_SkyShadingDirectionSize;
					bufferLayout._SkyShadingDirectionProbeSize = 1;
				}
				else
				{
					bufferLayout._SkyShadingDirectionSize = 0;
					bufferLayout._SkyShadingDirectionProbeSize = 0;
				}
			}
			else
			{
				bufferLayout._SkyOcclusionSize = 0;
				bufferLayout._SkyOcclusionProbeSize = 0;
				bufferLayout._SkyShadingDirectionSize = 0;
				bufferLayout._SkyShadingDirectionProbeSize = 0;
			}
			bufferLayout._L2Size = this.m_L2Size;
			if (this.m_ProbeOcclusionSize != 0)
			{
				bufferLayout._ProbeOcclusionSize = this.m_ProbeOcclusionSize;
				bufferLayout._ProbeOcclusionProbeSize = 4;
			}
			else
			{
				bufferLayout._ProbeOcclusionSize = 0;
				bufferLayout._ProbeOcclusionProbeSize = 0;
			}
			bufferLayout._L0ProbeSize = 8;
			bufferLayout._L1ProbeSize = 4;
			bufferLayout._L2ProbeSize = 4;
			int destChunksSize = chunkCount * 4 * 4;
			bufferLayout._SharedDestChunksOffset = destChunksSize;
			bufferLayout._L0L1rxOffset = bufferLayout._SharedDestChunksOffset + destChunksSize;
			bufferLayout._L1GryOffset = bufferLayout._L0L1rxOffset + this.m_L0Size * chunkCount;
			bufferLayout._L1BrzOffset = bufferLayout._L1GryOffset + this.m_L1Size * chunkCount;
			bufferLayout._ValidityOffset = bufferLayout._L1BrzOffset + this.m_L1Size * chunkCount;
			bufferLayout._ProbeOcclusionOffset = bufferLayout._ValidityOffset + this.m_ValiditySize * chunkCount;
			bufferLayout._SkyOcclusionOffset = bufferLayout._ProbeOcclusionOffset + this.m_ProbeOcclusionSize * chunkCount;
			bufferLayout._SkyShadingDirectionOffset = bufferLayout._SkyOcclusionOffset + this.m_SkyOcclusionSize * chunkCount;
			bufferLayout._L2_0Offset = bufferLayout._SkyShadingDirectionOffset + this.m_SkyShadingDirectionSize * chunkCount;
			bufferLayout._L2_1Offset = bufferLayout._L2_0Offset + this.m_L2Size * chunkCount;
			bufferLayout._L2_2Offset = bufferLayout._L2_1Offset + this.m_L2Size * chunkCount;
			bufferLayout._L2_3Offset = bufferLayout._L2_2Offset + this.m_L2Size * chunkCount;
			bufferLayout._ProbeCountInChunkLine = 512;
			bufferLayout._ProbeCountInChunkSlice = 2048;
			this.m_Layouts.Add(chunkCount, bufferLayout);
			return bufferLayout;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x000208C4 File Offset: 0x0001EAC4
		private ProbeReferenceVolume.CellStreamingScratchBuffer CreateScratchBuffer(int chunkCount, bool allocateGraphicsBuffers)
		{
			ProbeReferenceVolume.CellStreamingScratchBuffer cellStreamingScratchBuffer = new ProbeReferenceVolume.CellStreamingScratchBuffer(chunkCount, this.chunkSize, allocateGraphicsBuffers);
			this.m_CurrentlyAllocatedChunkCount += chunkCount;
			return cellStreamingScratchBuffer;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x000208E4 File Offset: 0x0001EAE4
		public bool AllocateScratchBuffer(int chunkCount, out ProbeReferenceVolume.CellStreamingScratchBuffer scratchBuffer, out ProbeReferenceVolume.CellStreamingScratchBufferLayout layout, bool allocateGraphicsBuffers)
		{
			ProbeVolumeScratchBufferPool.s_ChunkCount = chunkCount;
			int index = this.m_Pools.FindIndex(0, (ProbeVolumeScratchBufferPool.ScratchBufferPool o) => o.chunkCount == ProbeVolumeScratchBufferPool.s_ChunkCount);
			layout = this.GetOrCreateScratchBufferLayout(chunkCount);
			if (index == -1)
			{
				ProbeVolumeScratchBufferPool.ScratchBufferPool newPool = new ProbeVolumeScratchBufferPool.ScratchBufferPool(chunkCount);
				this.m_Pools.Add(newPool);
				this.m_Pools.Sort();
				scratchBuffer = this.CreateScratchBuffer(chunkCount, allocateGraphicsBuffers);
				return true;
			}
			Stack<ProbeReferenceVolume.CellStreamingScratchBuffer> pool = this.m_Pools[index].pool;
			if (pool.Count > 0)
			{
				scratchBuffer = pool.Pop();
				scratchBuffer.Swap();
				return true;
			}
			for (int i = index; i < this.m_Pools.Count; i++)
			{
				ProbeVolumeScratchBufferPool.ScratchBufferPool biggerPool = this.m_Pools[i];
				if (biggerPool.chunkCount >= chunkCount * 2)
				{
					break;
				}
				if (biggerPool.pool.Count > 0)
				{
					scratchBuffer = biggerPool.pool.Pop();
					scratchBuffer.Swap();
					return true;
				}
			}
			if (this.m_CurrentlyAllocatedChunkCount + chunkCount < this.maxChunkCount)
			{
				scratchBuffer = this.CreateScratchBuffer(chunkCount, allocateGraphicsBuffers);
				return true;
			}
			scratchBuffer = null;
			return false;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00020A04 File Offset: 0x0001EC04
		public void ReleaseScratchBuffer(ProbeReferenceVolume.CellStreamingScratchBuffer scratchBuffer)
		{
			ProbeVolumeScratchBufferPool.s_ChunkCount = scratchBuffer.chunkCount;
			this.m_Pools.Find((ProbeVolumeScratchBufferPool.ScratchBufferPool o) => o.chunkCount == ProbeVolumeScratchBufferPool.s_ChunkCount).pool.Push(scratchBuffer);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00020A54 File Offset: 0x0001EC54
		public void Cleanup()
		{
			foreach (ProbeVolumeScratchBufferPool.ScratchBufferPool pool in this.m_Pools)
			{
				while (pool.pool.Count > 0)
				{
					pool.pool.Pop().Dispose();
				}
			}
			this.m_Pools.Clear();
			this.m_CurrentlyAllocatedChunkCount = 0;
			this.chunkSize = 0;
			this.maxChunkCount = 0;
		}

		// Token: 0x040005D8 RID: 1496
		private int m_L0Size;

		// Token: 0x040005D9 RID: 1497
		private int m_L1Size;

		// Token: 0x040005DA RID: 1498
		private int m_ValiditySize;

		// Token: 0x040005DB RID: 1499
		private int m_ValidityLayerCount;

		// Token: 0x040005DC RID: 1500
		private int m_L2Size;

		// Token: 0x040005DD RID: 1501
		private int m_ProbeOcclusionSize;

		// Token: 0x040005DE RID: 1502
		private int m_SkyOcclusionSize;

		// Token: 0x040005DF RID: 1503
		private int m_SkyShadingDirectionSize;

		// Token: 0x040005E0 RID: 1504
		private int m_CurrentlyAllocatedChunkCount;

		// Token: 0x040005E1 RID: 1505
		private List<ProbeVolumeScratchBufferPool.ScratchBufferPool> m_Pools = new List<ProbeVolumeScratchBufferPool.ScratchBufferPool>();

		// Token: 0x040005E2 RID: 1506
		private Dictionary<int, ProbeReferenceVolume.CellStreamingScratchBufferLayout> m_Layouts = new Dictionary<int, ProbeReferenceVolume.CellStreamingScratchBufferLayout>();

		// Token: 0x040005E3 RID: 1507
		private static int s_ChunkCount;

		// Token: 0x0200013C RID: 316
		[DebuggerDisplay("ChunkCount = {chunkCount} ElementCount = {pool.Count}")]
		private class ScratchBufferPool : IComparable<ProbeVolumeScratchBufferPool.ScratchBufferPool>
		{
			// Token: 0x060009FA RID: 2554 RVA: 0x00020AE0 File Offset: 0x0001ECE0
			public ScratchBufferPool(int chunkCount)
			{
				this.chunkCount = chunkCount;
			}

			// Token: 0x060009FB RID: 2555 RVA: 0x00020B01 File Offset: 0x0001ED01
			private ScratchBufferPool()
			{
			}

			// Token: 0x060009FC RID: 2556 RVA: 0x00020B1B File Offset: 0x0001ED1B
			public int CompareTo(ProbeVolumeScratchBufferPool.ScratchBufferPool other)
			{
				if (this.chunkCount < other.chunkCount)
				{
					return -1;
				}
				if (this.chunkCount > other.chunkCount)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x040005E4 RID: 1508
			public int chunkCount = -1;

			// Token: 0x040005E5 RID: 1509
			public Stack<ProbeReferenceVolume.CellStreamingScratchBuffer> pool = new Stack<ProbeReferenceVolume.CellStreamingScratchBuffer>();
		}
	}
}
