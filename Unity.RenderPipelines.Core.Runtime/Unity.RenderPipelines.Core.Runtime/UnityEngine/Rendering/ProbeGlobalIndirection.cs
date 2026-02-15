using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000FC RID: 252
	internal class ProbeGlobalIndirection
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00015139 File Offset: 0x00013339
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x00015141 File Offset: 0x00013341
		internal int estimatedVMemCost { get; private set; }

		// Token: 0x0600080C RID: 2060 RVA: 0x0001514A File Offset: 0x0001334A
		internal void GetMinMaxEntry(out Vector3Int minEntry, out Vector3Int maxEntry)
		{
			minEntry = this.m_EntryMin;
			maxEntry = this.m_EntryMax;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00015164 File Offset: 0x00013364
		internal Vector3Int GetGlobalIndirectionDimension()
		{
			return this.m_EntriesCount;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001516C File Offset: 0x0001336C
		internal Vector3Int GetGlobalIndirectionMinEntry()
		{
			return this.m_EntryMin;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00015174 File Offset: 0x00013374
		private int entrySizeInBricks
		{
			get
			{
				return Mathf.Min((int)Mathf.Pow(3f, 3f), this.m_CellSizeInMinBricks);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00015191 File Offset: 0x00013391
		internal int entriesPerCellDimension
		{
			get
			{
				return this.m_CellSizeInMinBricks / Mathf.Max(1, this.entrySizeInBricks);
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x000151A6 File Offset: 0x000133A6
		private int GetFlatIndex(Vector3Int normalizedPos)
		{
			return normalizedPos.z * (this.m_EntriesCount.x * this.m_EntriesCount.y) + normalizedPos.y * this.m_EntriesCount.x + normalizedPos.x;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x000151E4 File Offset: 0x000133E4
		internal ProbeGlobalIndirection(Vector3Int cellMin, Vector3Int cellMax, int cellSizeInMinBricks)
		{
			this.m_CellSizeInMinBricks = cellSizeInMinBricks;
			Vector3Int cellCount = cellMax + Vector3Int.one - cellMin;
			this.m_EntriesCount = cellCount * this.entriesPerCellDimension;
			this.m_EntryMin = cellMin * this.entriesPerCellDimension;
			this.m_EntryMax = (cellMax + Vector3Int.one) * this.entriesPerCellDimension - Vector3Int.one;
			int flatEntryCount = this.m_EntriesCount.x * this.m_EntriesCount.y * this.m_EntriesCount.z;
			int bufferSize = 3 * flatEntryCount;
			this.m_IndexOfIndicesBuffer = new ComputeBuffer(flatEntryCount, 12);
			this.m_IndexOfIndicesData = new uint[bufferSize];
			this.m_NeedUpdateComputeBuffer = false;
			this.estimatedVMemCost = flatEntryCount * 3 * 4;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x000152B0 File Offset: 0x000134B0
		internal int GetFlatIdxForEntry(Vector3Int entryPosition)
		{
			Vector3Int normalizedPos = entryPosition - this.m_EntryMin;
			return this.GetFlatIndex(normalizedPos);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x000152D4 File Offset: 0x000134D4
		internal int[] GetFlatIndicesForCell(Vector3Int cellPosition)
		{
			Vector3Int firstEntryPosition = cellPosition * this.entriesPerCellDimension;
			int entriesPerCellDim = this.m_CellSizeInMinBricks / this.entrySizeInBricks;
			int[] outListOfIndices = new int[this.entriesPerCellDimension * this.entriesPerCellDimension * this.entriesPerCellDimension];
			int i = 0;
			for (int x = 0; x < entriesPerCellDim; x++)
			{
				for (int y = 0; y < entriesPerCellDim; y++)
				{
					for (int z = 0; z < entriesPerCellDim; z++)
					{
						outListOfIndices[i++] = this.GetFlatIdxForEntry(firstEntryPosition + new Vector3Int(x, y, z));
					}
				}
			}
			return outListOfIndices;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00015368 File Offset: 0x00013568
		internal void UpdateCell(ProbeReferenceVolume.CellIndexInfo cellInfo)
		{
			for (int entry = 0; entry < cellInfo.flatIndicesInGlobalIndirection.Length; entry++)
			{
				int entryIndex = cellInfo.flatIndicesInGlobalIndirection[entry];
				ProbeBrickIndex.IndirectionEntryUpdateInfo entryUpdateInfo = cellInfo.updateInfo.entriesInfo[entry];
				int minSubdivCellSize = ProbeReferenceVolume.CellSize(entryUpdateInfo.minSubdivInCell);
				ProbeGlobalIndirection.IndexMetaData metaData = default(ProbeGlobalIndirection.IndexMetaData);
				metaData.minSubdiv = entryUpdateInfo.minSubdivInCell;
				metaData.minLocalIdx = (entryUpdateInfo.hasOnlyBiggerBricks ? Vector3Int.zero : (entryUpdateInfo.minValidBrickIndexForCellAtMaxRes / minSubdivCellSize));
				metaData.maxLocalIdxPlusOne = (entryUpdateInfo.hasOnlyBiggerBricks ? Vector3Int.one : (entryUpdateInfo.maxValidBrickIndexForCellAtMaxResPlusOne / minSubdivCellSize));
				metaData.firstChunkIndex = entryUpdateInfo.firstChunkIndex;
				uint[] packedVals;
				metaData.Pack(out packedVals);
				for (int i = 0; i < 3; i++)
				{
					this.m_IndexOfIndicesData[entryIndex * 3 + i] = packedVals[i];
				}
			}
			this.m_NeedUpdateComputeBuffer = true;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00015450 File Offset: 0x00013650
		internal void MarkEntriesAsUnloaded(int[] entriesFlatIndices)
		{
			for (int entry = 0; entry < entriesFlatIndices.Length; entry++)
			{
				for (int i = 0; i < 3; i++)
				{
					this.m_IndexOfIndicesData[entriesFlatIndices[entry] * 3 + i] = uint.MaxValue;
				}
			}
			this.m_NeedUpdateComputeBuffer = true;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001548D File Offset: 0x0001368D
		internal void PushComputeData()
		{
			this.m_IndexOfIndicesBuffer.SetData(this.m_IndexOfIndicesData);
			this.m_NeedUpdateComputeBuffer = false;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x000154A7 File Offset: 0x000136A7
		internal void GetRuntimeResources(ref ProbeReferenceVolume.RuntimeResources rr)
		{
			if (this.m_NeedUpdateComputeBuffer)
			{
				this.PushComputeData();
			}
			rr.cellIndices = this.m_IndexOfIndicesBuffer;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x000154C3 File Offset: 0x000136C3
		internal void Cleanup()
		{
			CoreUtils.SafeRelease(this.m_IndexOfIndicesBuffer);
			this.m_IndexOfIndicesBuffer = null;
		}

		// Token: 0x04000383 RID: 899
		private const int kUintPerEntry = 3;

		// Token: 0x04000385 RID: 901
		internal const int kEntryMaxSubdivLevel = 3;

		// Token: 0x04000386 RID: 902
		private ComputeBuffer m_IndexOfIndicesBuffer;

		// Token: 0x04000387 RID: 903
		private uint[] m_IndexOfIndicesData;

		// Token: 0x04000388 RID: 904
		private int m_CellSizeInMinBricks;

		// Token: 0x04000389 RID: 905
		private Vector3Int m_EntriesCount;

		// Token: 0x0400038A RID: 906
		private Vector3Int m_EntryMin;

		// Token: 0x0400038B RID: 907
		private Vector3Int m_EntryMax;

		// Token: 0x0400038C RID: 908
		private bool m_NeedUpdateComputeBuffer;

		// Token: 0x020000FD RID: 253
		internal struct IndexMetaData
		{
			// Token: 0x0600081A RID: 2074 RVA: 0x000154D8 File Offset: 0x000136D8
			internal void Pack(out uint[] vals)
			{
				vals = ProbeGlobalIndirection.IndexMetaData.s_PackedValues;
				for (int i = 0; i < 3; i++)
				{
					vals[i] = 0U;
				}
				Vector3Int sizeOfValid = this.maxLocalIdxPlusOne - this.minLocalIdx;
				vals[0] = (uint)(this.firstChunkIndex & 536870911);
				vals[0] |= (uint)((uint)(this.minSubdiv & 7) << 29);
				vals[1] = (uint)(this.minLocalIdx.x & 1023);
				vals[1] |= (uint)((uint)(this.minLocalIdx.y & 1023) << 10);
				vals[1] |= (uint)((uint)(this.minLocalIdx.z & 1023) << 20);
				vals[2] = (uint)(sizeOfValid.x & 1023);
				vals[2] |= (uint)((uint)(sizeOfValid.y & 1023) << 10);
				vals[2] |= (uint)((uint)(sizeOfValid.z & 1023) << 20);
			}

			// Token: 0x0400038D RID: 909
			private static uint[] s_PackedValues = new uint[3];

			// Token: 0x0400038E RID: 910
			internal Vector3Int minLocalIdx;

			// Token: 0x0400038F RID: 911
			internal Vector3Int maxLocalIdxPlusOne;

			// Token: 0x04000390 RID: 912
			internal int firstChunkIndex;

			// Token: 0x04000391 RID: 913
			internal int minSubdiv;
		}
	}
}
