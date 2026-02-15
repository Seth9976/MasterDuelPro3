using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;

namespace UnityEngine.Rendering
{
	// Token: 0x020000F4 RID: 244
	internal class ProbeBrickIndex
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00012A69 File Offset: 0x00010C69
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x00012A71 File Offset: 0x00010C71
		internal int estimatedVMemCost { get; private set; }

		// Token: 0x060007BB RID: 1979 RVA: 0x00012A7A File Offset: 0x00010C7A
		internal ComputeBuffer GetDebugFragmentationBuffer()
		{
			return this.m_DebugFragmentationBuffer;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00012A82 File Offset: 0x00010C82
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x00012A8A File Offset: 0x00010C8A
		internal float fragmentationRate { get; private set; }

		// Token: 0x060007BE RID: 1982 RVA: 0x00012A93 File Offset: 0x00010C93
		private int SizeOfPhysicalIndexFromBudget(ProbeVolumeTextureMemoryBudget memoryBudget)
		{
			if (memoryBudget == ProbeVolumeTextureMemoryBudget.MemoryBudgetLow)
			{
				return 4000000;
			}
			if (memoryBudget == ProbeVolumeTextureMemoryBudget.MemoryBudgetMedium)
			{
				return 8000000;
			}
			if (memoryBudget != ProbeVolumeTextureMemoryBudget.MemoryBudgetHigh)
			{
				return 32000000;
			}
			return 16000000;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00012AC8 File Offset: 0x00010CC8
		internal ProbeBrickIndex(ProbeVolumeTextureMemoryBudget memoryBudget)
		{
			this.m_CenterRS = new Vector3Int(0, 0, 0);
			this.m_NeedUpdateIndexComputeBuffer = false;
			this.m_ChunksCount = Mathf.Max(1, Mathf.CeilToInt((float)this.SizeOfPhysicalIndexFromBudget(memoryBudget) / 243f));
			this.m_AvailableChunkCount = this.m_ChunksCount;
			this.m_IndexChunks = new BitArray(this.m_ChunksCount);
			this.m_IndexChunksCopyForChecks = new BitArray(this.m_ChunksCount);
			int physicalBufferSize = this.m_ChunksCount * 243;
			this.m_PhysicalIndexBufferData = new NativeArray<int>(physicalBufferSize, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			this.m_PhysicalIndexBuffer = new ComputeBuffer(physicalBufferSize, 4, ComputeBufferType.Structured);
			this.estimatedVMemCost = physicalBufferSize * 4;
			this.Clear();
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00012B8C File Offset: 0x00010D8C
		public int GetRemainingChunkCount()
		{
			return this.m_AvailableChunkCount;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00012B94 File Offset: 0x00010D94
		internal void UploadIndexData()
		{
			int count = this.m_UpdateMaxIndex - this.m_UpdateMinIndex + 1;
			this.m_PhysicalIndexBuffer.SetData<int>(this.m_PhysicalIndexBufferData, this.m_UpdateMinIndex, this.m_UpdateMinIndex, count);
			this.m_NeedUpdateIndexComputeBuffer = false;
			this.m_UpdateMaxIndex = int.MinValue;
			this.m_UpdateMinIndex = int.MaxValue;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00012BEC File Offset: 0x00010DEC
		private void UpdateDebugData()
		{
			if (this.m_DebugFragmentationData == null || this.m_DebugFragmentationData.Length != this.m_IndexChunks.Length)
			{
				this.m_DebugFragmentationData = new int[this.m_IndexChunks.Length];
				CoreUtils.SafeRelease(this.m_DebugFragmentationBuffer);
				this.m_DebugFragmentationBuffer = new ComputeBuffer(this.m_IndexChunks.Length, 4);
			}
			for (int i = 0; i < this.m_IndexChunks.Length; i++)
			{
				this.m_DebugFragmentationData[i] = (this.m_IndexChunks[i] ? 1 : (-1));
			}
			this.m_DebugFragmentationBuffer.SetData(this.m_DebugFragmentationData);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00012C90 File Offset: 0x00010E90
		internal unsafe void Clear()
		{
			this.m_IndexChunks.SetAll(false);
			this.m_AvailableChunkCount = this.m_ChunksCount;
			uint* pBuffer = (uint*)this.m_PhysicalIndexBufferData.GetUnsafePtr<int>();
			UnsafeUtility.MemSet((void*)pBuffer, byte.MaxValue, (long)(this.m_PhysicalIndexBufferData.Length * 4));
			this.m_NeedUpdateIndexComputeBuffer = true;
			this.m_UpdateMinIndex = 0;
			this.m_UpdateMaxIndex = this.m_PhysicalIndexBufferData.Length - 1;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00012CFC File Offset: 0x00010EFC
		internal void GetRuntimeResources(ref ProbeReferenceVolume.RuntimeResources rr)
		{
			bool displayFrag = ProbeReferenceVolume.instance.probeVolumeDebug.displayIndexFragmentation;
			if (this.m_NeedUpdateIndexComputeBuffer)
			{
				this.UploadIndexData();
				if (displayFrag)
				{
					this.UpdateDebugData();
				}
			}
			if (displayFrag && this.m_DebugFragmentationBuffer == null)
			{
				this.UpdateDebugData();
			}
			rr.index = this.m_PhysicalIndexBuffer;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00012D4D File Offset: 0x00010F4D
		internal void Cleanup()
		{
			this.m_PhysicalIndexBufferData.Dispose();
			CoreUtils.SafeRelease(this.m_PhysicalIndexBuffer);
			this.m_PhysicalIndexBuffer = null;
			CoreUtils.SafeRelease(this.m_DebugFragmentationBuffer);
			this.m_DebugFragmentationBuffer = null;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00012D80 File Offset: 0x00010F80
		internal void ComputeFragmentationRate()
		{
			int highestAllocatedChunk = 0;
			for (int i = this.m_ChunksCount - 1; i >= 0; i--)
			{
				if (this.m_IndexChunks[i])
				{
					highestAllocatedChunk = i + 1;
					break;
				}
			}
			int tailFreeChunks = this.m_ChunksCount - highestAllocatedChunk;
			int holes = this.m_AvailableChunkCount - tailFreeChunks;
			this.fragmentationRate = (float)holes / (float)highestAllocatedChunk;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00012DD3 File Offset: 0x00010FD3
		private int MergeIndex(int index, int size)
		{
			return (index & -1879048193) | ((size & 7) << 28);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00012DE3 File Offset: 0x00010FE3
		internal int GetNumberOfChunks(int brickCount)
		{
			return Mathf.CeilToInt((float)brickCount / 243f);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00012DF4 File Offset: 0x00010FF4
		internal bool FindSlotsForEntries(ref ProbeBrickIndex.IndirectionEntryUpdateInfo[] entriesInfo)
		{
			bool flag;
			using (new ProfilerMarker("FindSlotsForEntries").Auto())
			{
				this.m_IndexChunksCopyForChecks.SetAll(false);
				this.m_IndexChunksCopyForChecks.Or(this.m_IndexChunks);
				int numberOfEntries = entriesInfo.Length;
				for (int entry = 0; entry < numberOfEntries; entry++)
				{
					entriesInfo[entry].firstChunkIndex = -2;
					int numberOfChunksForEntry = entriesInfo[entry].numberOfChunks;
					if (numberOfChunksForEntry != 0)
					{
						for (int i = 0; i < this.m_ChunksCount - numberOfChunksForEntry; i++)
						{
							if (!this.m_IndexChunksCopyForChecks[i])
							{
								int firstSlot = i;
								int lastSlot = i + numberOfChunksForEntry;
								while (i + 1 < lastSlot && !this.m_IndexChunksCopyForChecks[++i])
								{
								}
								if (!this.m_IndexChunksCopyForChecks[i])
								{
									entriesInfo[entry].firstChunkIndex = firstSlot;
									break;
								}
							}
						}
						if (entriesInfo[entry].firstChunkIndex < 0)
						{
							for (int e = 0; e < numberOfEntries; e++)
							{
								entriesInfo[e].firstChunkIndex = -1;
							}
							return false;
						}
						for (int j = entriesInfo[entry].firstChunkIndex; j < entriesInfo[entry].firstChunkIndex + numberOfChunksForEntry; j++)
						{
							this.m_IndexChunksCopyForChecks[j] = true;
						}
					}
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00012F7C File Offset: 0x0001117C
		internal bool ReserveChunks(ProbeBrickIndex.IndirectionEntryUpdateInfo[] entriesInfo, bool ignoreErrorLog)
		{
			int entryCount = entriesInfo.Length;
			for (int entry = 0; entry < entryCount; entry++)
			{
				int firstChunkForEntry = entriesInfo[entry].firstChunkIndex;
				int numberOfChunkForEntry = entriesInfo[entry].numberOfChunks;
				if (numberOfChunkForEntry != 0)
				{
					if (firstChunkForEntry < 0)
					{
						if (!ignoreErrorLog)
						{
							Debug.LogError("APV Index Allocation failed.");
						}
						return false;
					}
					for (int i = firstChunkForEntry; i < firstChunkForEntry + numberOfChunkForEntry; i++)
					{
						this.m_IndexChunks[i] = true;
					}
					this.m_AvailableChunkCount -= numberOfChunkForEntry;
				}
			}
			return true;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00012FF8 File Offset: 0x000111F8
		internal static bool BrickOverlapEntry(Vector3Int brickMin, Vector3Int brickMax, Vector3Int entryMin, Vector3Int entryMax)
		{
			return brickMax.x > entryMin.x && entryMax.x > brickMin.x && brickMax.y > entryMin.y && entryMax.y > brickMin.y && brickMax.z > entryMin.z && entryMax.z > brickMin.z;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00013067 File Offset: 0x00011267
		private static int LocationToIndex(int x, int y, int z, Vector3Int sizeOfValid)
		{
			return z * (sizeOfValid.x * sizeOfValid.y) + x * sizeOfValid.y + y;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00013088 File Offset: 0x00011288
		private void MarkBrickInPhysicalBuffer(in ProbeBrickIndex.IndirectionEntryUpdateInfo entry, Vector3Int brickMin, Vector3Int brickMax, int brickSubdivLevel, int entrySubdivLevel, int idx)
		{
			this.m_NeedUpdateIndexComputeBuffer = true;
			if (entry.hasOnlyBiggerBricks)
			{
				int singleEntry = entry.firstChunkIndex * 243;
				this.m_UpdateMinIndex = Math.Min(this.m_UpdateMinIndex, singleEntry);
				this.m_UpdateMaxIndex = Math.Max(this.m_UpdateMaxIndex, singleEntry);
				this.m_PhysicalIndexBufferData[singleEntry] = idx;
				return;
			}
			int minBrickSize = ProbeReferenceVolume.CellSize(entry.minSubdivInCell);
			Vector3Int entryMinIndex = entry.minValidBrickIndexForCellAtMaxRes / minBrickSize;
			Vector3Int sizeOfValid = entry.maxValidBrickIndexForCellAtMaxResPlusOne / minBrickSize - entryMinIndex;
			if (brickSubdivLevel >= entrySubdivLevel)
			{
				brickMin = Vector3Int.zero;
				brickMax = sizeOfValid;
			}
			else
			{
				brickMin -= entry.entryPositionInBricksAtMaxRes;
				brickMax -= entry.entryPositionInBricksAtMaxRes;
				brickMin /= minBrickSize;
				brickMax /= minBrickSize;
				ProbeReferenceVolume.CellSize(entrySubdivLevel - entry.minSubdivInCell);
				brickMin -= entryMinIndex;
				brickMax -= entryMinIndex;
			}
			int chunkStart = entry.firstChunkIndex * 243;
			int newMin = chunkStart + ProbeBrickIndex.LocationToIndex(brickMin.x, brickMin.y, brickMin.z, sizeOfValid);
			int newMax = chunkStart + ProbeBrickIndex.LocationToIndex(brickMax.x - 1, brickMax.y - 1, brickMax.z - 1, sizeOfValid);
			this.m_UpdateMinIndex = Math.Min(this.m_UpdateMinIndex, newMin);
			this.m_UpdateMaxIndex = Math.Max(this.m_UpdateMaxIndex, newMax);
			for (int x = brickMin.x; x < brickMax.x; x++)
			{
				for (int z = brickMin.z; z < brickMax.z; z++)
				{
					for (int y = brickMin.y; y < brickMax.y; y++)
					{
						int localFlatIdx = ProbeBrickIndex.LocationToIndex(x, y, z, sizeOfValid);
						this.m_PhysicalIndexBufferData[chunkStart + localFlatIdx] = idx;
					}
				}
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00013260 File Offset: 0x00011460
		public void AddBricks(ProbeReferenceVolume.CellIndexInfo cellInfo, NativeArray<ProbeBrickIndex.Brick> bricks, List<ProbeBrickPool.BrickChunkAlloc> allocations, int allocationSize, int poolWidth, int poolHeight)
		{
			int entrySubdivLevel = ProbeReferenceVolume.instance.GetEntrySubdivLevel();
			int brick_idx = 0;
			for (int i = 0; i < allocations.Count; i++)
			{
				ProbeBrickPool.BrickChunkAlloc alloc = allocations[i];
				int last_brick = brick_idx + Mathf.Min(allocationSize, bricks.Length - brick_idx);
				while (brick_idx != last_brick)
				{
					ProbeBrickIndex.Brick brick = bricks[brick_idx++];
					int idx = this.MergeIndex(alloc.flattenIndex(poolWidth, poolHeight), brick.subdivisionLevel);
					alloc.x += 4;
					int brickSize = ProbeReferenceVolume.CellSize(brick.subdivisionLevel);
					Vector3Int brickMin = brick.position;
					Vector3Int brickMax = brick.position + new Vector3Int(brickSize, brickSize, brickSize);
					foreach (ProbeBrickIndex.IndirectionEntryUpdateInfo entry in cellInfo.updateInfo.entriesInfo)
					{
						Vector3Int minEntryPosition = entry.entryPositionInBricksAtMaxRes + entry.minValidBrickIndexForCellAtMaxRes;
						Vector3Int maxEntryPosition = entry.entryPositionInBricksAtMaxRes + entry.maxValidBrickIndexForCellAtMaxResPlusOne - Vector3Int.one;
						if (ProbeBrickIndex.BrickOverlapEntry(brickMin, brickMax, minEntryPosition, maxEntryPosition))
						{
							this.MarkBrickInPhysicalBuffer(in entry, brickMin, brickMax, brick.subdivisionLevel, entrySubdivLevel, idx);
						}
					}
				}
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x000133A4 File Offset: 0x000115A4
		public void RemoveBricks(ProbeReferenceVolume.CellIndexInfo cellInfo)
		{
			for (int e = 0; e < cellInfo.updateInfo.entriesInfo.Length; e++)
			{
				ref ProbeBrickIndex.IndirectionEntryUpdateInfo entryInfo = ref cellInfo.updateInfo.entriesInfo[e];
				if (entryInfo.firstChunkIndex >= 0)
				{
					for (int i = entryInfo.firstChunkIndex; i < entryInfo.firstChunkIndex + entryInfo.numberOfChunks; i++)
					{
						this.m_IndexChunks[i] = false;
					}
					this.m_AvailableChunkCount += entryInfo.numberOfChunks;
					entryInfo.numberOfChunks = 0;
				}
			}
		}

		// Token: 0x04000315 RID: 789
		internal const int kMaxSubdivisionLevels = 7;

		// Token: 0x04000316 RID: 790
		internal const int kIndexChunkSize = 243;

		// Token: 0x04000317 RID: 791
		internal const int kFailChunkIndex = -1;

		// Token: 0x04000318 RID: 792
		internal const int kEmptyIndex = -2;

		// Token: 0x04000319 RID: 793
		private BitArray m_IndexChunks;

		// Token: 0x0400031A RID: 794
		private BitArray m_IndexChunksCopyForChecks;

		// Token: 0x0400031B RID: 795
		private int m_ChunksCount;

		// Token: 0x0400031C RID: 796
		private int m_AvailableChunkCount;

		// Token: 0x0400031D RID: 797
		private ComputeBuffer m_PhysicalIndexBuffer;

		// Token: 0x0400031E RID: 798
		private NativeArray<int> m_PhysicalIndexBufferData;

		// Token: 0x0400031F RID: 799
		private ComputeBuffer m_DebugFragmentationBuffer;

		// Token: 0x04000320 RID: 800
		private int[] m_DebugFragmentationData;

		// Token: 0x04000321 RID: 801
		private bool m_NeedUpdateIndexComputeBuffer;

		// Token: 0x04000322 RID: 802
		private int m_UpdateMinIndex = int.MaxValue;

		// Token: 0x04000323 RID: 803
		private int m_UpdateMaxIndex = int.MinValue;

		// Token: 0x04000326 RID: 806
		private Vector3Int m_CenterRS;

		// Token: 0x020000F5 RID: 245
		[DebuggerDisplay("Brick [{position}, {subdivisionLevel}]")]
		[Serializable]
		public struct Brick : IEquatable<ProbeBrickIndex.Brick>
		{
			// Token: 0x060007D0 RID: 2000 RVA: 0x00013428 File Offset: 0x00011628
			internal Brick(Vector3Int position, int subdivisionLevel)
			{
				this.position = position;
				this.subdivisionLevel = subdivisionLevel;
			}

			// Token: 0x060007D1 RID: 2001 RVA: 0x00013438 File Offset: 0x00011638
			public bool Equals(ProbeBrickIndex.Brick other)
			{
				return this.position == other.position && this.subdivisionLevel == other.subdivisionLevel;
			}

			// Token: 0x060007D2 RID: 2002 RVA: 0x00013460 File Offset: 0x00011660
			public bool IntersectArea(Bounds boundInBricksToCheck)
			{
				int sizeInMinBricks = ProbeReferenceVolume.CellSize(this.subdivisionLevel);
				Bounds brickBounds = default(Bounds);
				brickBounds.min = this.position;
				brickBounds.max = this.position + new Vector3Int(sizeInMinBricks, sizeInMinBricks, sizeInMinBricks);
				brickBounds.extents *= 0.99f;
				return boundInBricksToCheck.Intersects(brickBounds);
			}

			// Token: 0x04000327 RID: 807
			public Vector3Int position;

			// Token: 0x04000328 RID: 808
			public int subdivisionLevel;
		}

		// Token: 0x020000F6 RID: 246
		public struct IndirectionEntryUpdateInfo
		{
			// Token: 0x04000329 RID: 809
			public int firstChunkIndex;

			// Token: 0x0400032A RID: 810
			public int numberOfChunks;

			// Token: 0x0400032B RID: 811
			public int minSubdivInCell;

			// Token: 0x0400032C RID: 812
			public Vector3Int minValidBrickIndexForCellAtMaxRes;

			// Token: 0x0400032D RID: 813
			public Vector3Int maxValidBrickIndexForCellAtMaxResPlusOne;

			// Token: 0x0400032E RID: 814
			public Vector3Int entryPositionInBricksAtMaxRes;

			// Token: 0x0400032F RID: 815
			public bool hasOnlyBiggerBricks;
		}

		// Token: 0x020000F7 RID: 247
		public struct CellIndexUpdateInfo
		{
			// Token: 0x060007D3 RID: 2003 RVA: 0x000134D4 File Offset: 0x000116D4
			public int GetNumberOfChunks()
			{
				int chunkCount = 0;
				foreach (ProbeBrickIndex.IndirectionEntryUpdateInfo entry in this.entriesInfo)
				{
					chunkCount += entry.numberOfChunks;
				}
				return chunkCount;
			}

			// Token: 0x04000330 RID: 816
			public ProbeBrickIndex.IndirectionEntryUpdateInfo[] entriesInfo;
		}
	}
}
