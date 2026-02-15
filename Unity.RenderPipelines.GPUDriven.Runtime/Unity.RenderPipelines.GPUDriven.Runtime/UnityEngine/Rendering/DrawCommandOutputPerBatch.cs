using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200003D RID: 61
	[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct DrawCommandOutputPerBatch : IJobParallelFor
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00006864 File Offset: 0x00004A64
		private int EncodeGPUInstanceIndexAndCrossFade(int rendererIndex, bool negateCrossFade)
		{
			GPUInstanceIndex gpuInstanceIndex = this.instanceDataBuffer.CPUInstanceToGPUInstance(InstanceHandle.FromInt(rendererIndex));
			int crossFadeValue = (int)this.rendererCrossFadeValues[rendererIndex];
			crossFadeValue -= 127;
			if (negateCrossFade)
			{
				crossFadeValue = -crossFadeValue;
			}
			gpuInstanceIndex.index |= crossFadeValue << 24;
			return gpuInstanceIndex.index;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000068B4 File Offset: 0x00004AB4
		private bool IsInstanceFlipped(int rendererIndex)
		{
			InstanceHandle instance = InstanceHandle.FromInt(rendererIndex);
			int instanceIndex = this.instanceData.InstanceToIndex(instance);
			return this.instanceData.localToWorldIsFlippedBits.Get(instanceIndex);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000068EC File Offset: 0x00004AEC
		public unsafe void Execute(int batchIndex)
		{
			DrawBatch drawBatch = this.drawBatches[batchIndex];
			int binCount = this.batchBinCounts[batchIndex];
			if (binCount == 0)
			{
				return;
			}
			BatchCullingOutputDrawCommands output = this.cullingOutput[0];
			IndirectBufferAllocInfo indirectAllocInfo = default(IndirectBufferAllocInfo);
			if (this.indirectBufferLimits.maxDrawCount > 0)
			{
				indirectAllocInfo = this.indirectBufferAllocInfo[0];
			}
			bool isIndirect = !indirectAllocInfo.IsEmpty() && drawBatch.key.range.supportsIndirect;
			int configCount = this.binningConfig.visibilityConfigCount;
			int* instanceOffsetPerConfig;
			checked
			{
				instanceOffsetPerConfig = stackalloc int[unchecked((UIntPtr)configCount) * 4];
			}
			for (int i = 0; i < configCount; i++)
			{
				instanceOffsetPerConfig[i] = 0;
			}
			int* drawCommandOffsetPerConfig;
			int batchBinAllocOffset;
			int batchDrawCommandOffset;
			int lastBinInstanceOffset;
			bool rangeSupportsMotion;
			checked
			{
				drawCommandOffsetPerConfig = stackalloc int[unchecked((UIntPtr)configCount) * 4];
				batchBinAllocOffset = this.batchBinAllocOffsets[batchIndex];
				batchDrawCommandOffset = this.batchDrawCommandOffsets[batchIndex];
				lastBinInstanceOffset = 0;
				rangeSupportsMotion = drawBatch.key.range.motionMode == MotionVectorGenerationMode.Object || drawBatch.key.range.motionMode == MotionVectorGenerationMode.ForceNoMotion;
			}
			for (int binIndexInBatch = 0; binIndexInBatch < binCount; binIndexInBatch++)
			{
				int binIndex = batchBinAllocOffset + binIndexInBatch;
				int visibleInstanceOffset = this.binVisibleInstanceOffsets[binIndex];
				int visibleInstanceCount = this.binVisibleInstanceCounts[binIndex];
				lastBinInstanceOffset = visibleInstanceOffset;
				short configIndex = this.binConfigIndices[binIndex];
				instanceOffsetPerConfig[configIndex] = visibleInstanceOffset;
				int drawCommandOffset = batchDrawCommandOffset + binIndexInBatch;
				drawCommandOffsetPerConfig[configIndex] = drawCommandOffset;
				BatchDrawCommandFlags drawFlags = drawBatch.key.flags;
				if ((configIndex & 1) != 0)
				{
					drawFlags |= BatchDrawCommandFlags.FlipWinding;
				}
				int visibilityMask = configIndex >> 1;
				if (this.binningConfig.supportsCrossFade)
				{
					if ((visibilityMask & 1) != 0)
					{
						drawFlags |= BatchDrawCommandFlags.LODCrossFadeKeyword;
					}
					visibilityMask >>= 1;
				}
				if (this.binningConfig.supportsMotionCheck)
				{
					if ((visibilityMask & 1) != 0 && rangeSupportsMotion)
					{
						drawFlags |= BatchDrawCommandFlags.HasMotion;
					}
					visibilityMask >>= 1;
				}
				int sortingPosition = 0;
				if ((drawFlags & BatchDrawCommandFlags.HasSortingPosition) != BatchDrawCommandFlags.None)
				{
					int globalCommandOffset = drawCommandOffset;
					if (isIndirect)
					{
						globalCommandOffset += output.drawCommandCount;
					}
					sortingPosition = 3 * globalCommandOffset;
				}
				if (isIndirect)
				{
					int instanceInfoGlobalIndex = indirectAllocInfo.instanceAllocIndex + visibleInstanceOffset;
					int drawInfoGlobalIndex = indirectAllocInfo.drawAllocIndex + drawCommandOffset;
					this.indirectDrawInfoGlobalArray[drawInfoGlobalIndex] = new IndirectDrawInfo
					{
						indexCount = drawBatch.procInfo.indexCount,
						firstIndex = drawBatch.procInfo.firstIndex,
						baseVertex = drawBatch.procInfo.baseVertex,
						firstInstanceGlobalIndex = (uint)instanceInfoGlobalIndex,
						maxInstanceCount = (uint)visibleInstanceCount
					};
					output.indirectDrawCommands[drawCommandOffset] = new BatchDrawCommandIndirect
					{
						flags = drawFlags,
						visibleOffset = (uint)instanceInfoGlobalIndex,
						batchID = this.batchIDs[drawBatch.key.overridenComponents],
						materialID = drawBatch.key.materialID,
						splitVisibilityMask = (ushort)visibilityMask,
						lightmapIndex = (ushort)drawBatch.key.lightmapIndex,
						sortingPosition = sortingPosition,
						meshID = drawBatch.key.meshID,
						topology = drawBatch.procInfo.topology,
						visibleInstancesBufferHandle = this.visibleInstancesBufferHandle,
						indirectArgsBufferHandle = this.indirectArgsBufferHandle,
						indirectArgsBufferOffset = (uint)(drawInfoGlobalIndex * 20)
					};
				}
				else
				{
					output.drawCommands[drawCommandOffset] = new BatchDrawCommand
					{
						flags = drawFlags,
						visibleOffset = (uint)visibleInstanceOffset,
						visibleCount = (uint)visibleInstanceCount,
						batchID = this.batchIDs[drawBatch.key.overridenComponents],
						materialID = drawBatch.key.materialID,
						splitVisibilityMask = (ushort)visibilityMask,
						lightmapIndex = (ushort)drawBatch.key.lightmapIndex,
						sortingPosition = sortingPosition,
						meshID = drawBatch.key.meshID,
						submeshIndex = (ushort)drawBatch.key.submeshIndex
					};
				}
			}
			int instanceOffset = drawBatch.instanceOffset;
			int instanceCount = drawBatch.instanceCount;
			int lastRendererIndex = 0;
			if (binCount > 1)
			{
				for (int j = 0; j < instanceCount; j++)
				{
					int rendererIndex = this.drawInstanceIndices[instanceOffset + j];
					bool isFlipped = this.IsInstanceFlipped(rendererIndex);
					int visibilityMask2 = (int)this.rendererVisibilityMasks[rendererIndex];
					if (visibilityMask2 != 0)
					{
						lastRendererIndex = rendererIndex;
						int configIndex2 = (visibilityMask2 << 1) | (isFlipped ? 1 : 0);
						int visibleInstanceOffset2 = instanceOffsetPerConfig[configIndex2];
						instanceOffsetPerConfig[configIndex2]++;
						if (isIndirect)
						{
							if (this.binningConfig.supportsCrossFade)
							{
								visibilityMask2 >>= 1;
							}
							if (this.binningConfig.supportsMotionCheck)
							{
								visibilityMask2 >>= 1;
							}
							this.indirectInstanceInfoGlobalArray[indirectAllocInfo.instanceAllocIndex + visibleInstanceOffset2] = new IndirectInstanceInfo
							{
								drawOffsetAndSplitMask = ((drawCommandOffsetPerConfig[configIndex2] << 8) | visibilityMask2),
								instanceIndexAndCrossFade = this.EncodeGPUInstanceIndexAndCrossFade(rendererIndex, false)
							};
						}
						else
						{
							output.visibleInstances[visibleInstanceOffset2] = this.EncodeGPUInstanceIndexAndCrossFade(rendererIndex, false);
						}
					}
				}
			}
			else
			{
				int visibleInstanceOffset3 = lastBinInstanceOffset;
				for (int k = 0; k < instanceCount; k++)
				{
					int rendererIndex2 = this.drawInstanceIndices[instanceOffset + k];
					int visibilityMask3 = (int)this.rendererVisibilityMasks[rendererIndex2];
					if (visibilityMask3 != 0)
					{
						lastRendererIndex = rendererIndex2;
						if (isIndirect)
						{
							if (this.binningConfig.supportsCrossFade)
							{
								visibilityMask3 >>= 1;
							}
							if (this.binningConfig.supportsMotionCheck)
							{
								visibilityMask3 >>= 1;
							}
							this.indirectInstanceInfoGlobalArray[indirectAllocInfo.instanceAllocIndex + visibleInstanceOffset3] = new IndirectInstanceInfo
							{
								drawOffsetAndSplitMask = ((batchDrawCommandOffset << 8) | visibilityMask3),
								instanceIndexAndCrossFade = this.EncodeGPUInstanceIndexAndCrossFade(rendererIndex2, false)
							};
						}
						else
						{
							output.visibleInstances[visibleInstanceOffset3] = this.EncodeGPUInstanceIndexAndCrossFade(rendererIndex2, false);
						}
						visibleInstanceOffset3++;
					}
				}
			}
			if ((drawBatch.key.flags & BatchDrawCommandFlags.HasSortingPosition) != BatchDrawCommandFlags.None)
			{
				InstanceHandle instance = InstanceHandle.FromInt(lastRendererIndex & 16777215);
				int instanceIndex = this.instanceData.InstanceToIndex(instance);
				float3 position = this.instanceData.worldAABBs.UnsafeElementAt(instanceIndex).center;
				int globalCommandOffset2 = batchDrawCommandOffset;
				if (isIndirect)
				{
					globalCommandOffset2 += output.drawCommandCount;
				}
				int sortingPosition2 = 3 * globalCommandOffset2;
				output.instanceSortingPositions[sortingPosition2] = position.x;
				output.instanceSortingPositions[sortingPosition2 + 1] = position.y;
				output.instanceSortingPositions[sortingPosition2 + 2] = position.z;
			}
		}

		// Token: 0x040000FB RID: 251
		[ReadOnly]
		public BinningConfig binningConfig;

		// Token: 0x040000FC RID: 252
		[ReadOnly]
		public NativeParallelHashMap<uint, BatchID> batchIDs;

		// Token: 0x040000FD RID: 253
		[ReadOnly]
		public GPUInstanceDataBuffer.ReadOnly instanceDataBuffer;

		// Token: 0x040000FE RID: 254
		[ReadOnly]
		public NativeList<DrawBatch> drawBatches;

		// Token: 0x040000FF RID: 255
		[ReadOnly]
		public NativeArray<int> drawInstanceIndices;

		// Token: 0x04000100 RID: 256
		[ReadOnly]
		public CPUInstanceData.ReadOnly instanceData;

		// Token: 0x04000101 RID: 257
		[ReadOnly]
		public NativeArray<byte> rendererVisibilityMasks;

		// Token: 0x04000102 RID: 258
		[ReadOnly]
		public NativeArray<byte> rendererCrossFadeValues;

		// Token: 0x04000103 RID: 259
		[ReadOnly]
		[DeallocateOnJobCompletion]
		public NativeArray<int> batchBinAllocOffsets;

		// Token: 0x04000104 RID: 260
		[ReadOnly]
		[DeallocateOnJobCompletion]
		public NativeArray<int> batchBinCounts;

		// Token: 0x04000105 RID: 261
		[ReadOnly]
		[DeallocateOnJobCompletion]
		public NativeArray<int> batchDrawCommandOffsets;

		// Token: 0x04000106 RID: 262
		[ReadOnly]
		[DeallocateOnJobCompletion]
		public NativeArray<short> binConfigIndices;

		// Token: 0x04000107 RID: 263
		[ReadOnly]
		[DeallocateOnJobCompletion]
		public NativeArray<int> binVisibleInstanceOffsets;

		// Token: 0x04000108 RID: 264
		[ReadOnly]
		[DeallocateOnJobCompletion]
		public NativeArray<int> binVisibleInstanceCounts;

		// Token: 0x04000109 RID: 265
		[ReadOnly]
		public NativeArray<BatchCullingOutputDrawCommands> cullingOutput;

		// Token: 0x0400010A RID: 266
		[ReadOnly]
		public IndirectBufferLimits indirectBufferLimits;

		// Token: 0x0400010B RID: 267
		[ReadOnly]
		public GraphicsBufferHandle visibleInstancesBufferHandle;

		// Token: 0x0400010C RID: 268
		[ReadOnly]
		public GraphicsBufferHandle indirectArgsBufferHandle;

		// Token: 0x0400010D RID: 269
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		public NativeArray<IndirectBufferAllocInfo> indirectBufferAllocInfo;

		// Token: 0x0400010E RID: 270
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		public NativeArray<IndirectDrawInfo> indirectDrawInfoGlobalArray;

		// Token: 0x0400010F RID: 271
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		public NativeArray<IndirectInstanceInfo> indirectInstanceInfoGlobalArray;
	}
}
