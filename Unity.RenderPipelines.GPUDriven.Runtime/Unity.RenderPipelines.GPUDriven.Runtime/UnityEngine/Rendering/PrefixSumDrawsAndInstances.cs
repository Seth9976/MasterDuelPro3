using System;
using System.Threading;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x0200003C RID: 60
	[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct PrefixSumDrawsAndInstances : IJob
	{
		// Token: 0x06000104 RID: 260 RVA: 0x000064F0 File Offset: 0x000046F0
		public unsafe void Execute()
		{
			BatchCullingOutputDrawCommands output = this.cullingOutput[0];
			bool allowIndirect = this.indirectBufferLimits.maxInstanceCount > 0;
			int outDirectCommandIndex;
			int outDirectVisibleInstanceIndex;
			int outIndirectCommandIndex;
			for (;;)
			{
				int outRangeIndex = 0;
				outDirectCommandIndex = 0;
				outDirectVisibleInstanceIndex = 0;
				outIndirectCommandIndex = 0;
				int outIndirectVisibleInstanceIndex = 0;
				for (int rangeIndex = 0; rangeIndex < this.drawRanges.Length; rangeIndex++)
				{
					DrawRange drawRangeInfo = this.drawRanges[rangeIndex];
					bool isIndirect = allowIndirect && drawRangeInfo.key.supportsIndirect;
					int rangeDrawCommandCount = 0;
					int rangeDrawCommandOffset = (isIndirect ? outIndirectCommandIndex : outDirectCommandIndex);
					for (int drawIndexInRange = 0; drawIndexInRange < drawRangeInfo.drawCount; drawIndexInRange++)
					{
						int batchIndex = this.drawBatchIndices[drawRangeInfo.drawOffset + drawIndexInRange];
						int binAllocOffset = this.batchBinAllocOffsets[batchIndex];
						int binCount = this.batchBinCounts[batchIndex];
						if (isIndirect)
						{
							this.batchDrawCommandOffsets[batchIndex] = outIndirectCommandIndex;
							outIndirectCommandIndex += binCount;
						}
						else
						{
							this.batchDrawCommandOffsets[batchIndex] = outDirectCommandIndex;
							outDirectCommandIndex += binCount;
						}
						rangeDrawCommandCount += binCount;
						for (int binIndexInBatch = 0; binIndexInBatch < binCount; binIndexInBatch++)
						{
							int binIndex = binAllocOffset + binIndexInBatch;
							if (isIndirect)
							{
								this.binVisibleInstanceOffsets[binIndex] = outIndirectVisibleInstanceIndex;
								outIndirectVisibleInstanceIndex += this.binVisibleInstanceCounts[binIndex];
							}
							else
							{
								this.binVisibleInstanceOffsets[binIndex] = outDirectVisibleInstanceIndex;
								outDirectVisibleInstanceIndex += this.binVisibleInstanceCounts[binIndex];
							}
						}
					}
					if (rangeDrawCommandCount != 0)
					{
						RangeKey rangeKey = drawRangeInfo.key;
						output.drawRanges[outRangeIndex] = new BatchDrawRange
						{
							drawCommandsBegin = (uint)rangeDrawCommandOffset,
							drawCommandsCount = (uint)rangeDrawCommandCount,
							drawCommandsType = (isIndirect ? BatchDrawCommandType.Indirect : BatchDrawCommandType.Direct),
							filterSettings = new BatchFilterSettings
							{
								renderingLayerMask = rangeKey.renderingLayerMask,
								rendererPriority = rangeKey.rendererPriority,
								layer = rangeKey.layer,
								batchLayer = (isIndirect ? 28 : 29),
								motionMode = rangeKey.motionMode,
								shadowCastingMode = rangeKey.shadowCastingMode,
								receiveShadows = true,
								staticShadowCaster = rangeKey.staticShadowCaster,
								allDepthSorted = false
							}
						};
						outRangeIndex++;
					}
				}
				output.drawRangeCount = outRangeIndex;
				bool isValid = true;
				if (allowIndirect)
				{
					int* allocCounters = (int*)this.indirectAllocationCounters.GetUnsafePtr<int>();
					IndirectBufferAllocInfo allocInfo = new IndirectBufferAllocInfo
					{
						drawCount = outIndirectCommandIndex,
						instanceCount = outIndirectVisibleInstanceIndex
					};
					int drawAllocCount = allocInfo.drawCount + 1;
					int drawAllocEnd = Interlocked.Add(UnsafeUtility.AsRef<int>((void*)(allocCounters + 1)), drawAllocCount);
					allocInfo.drawAllocIndex = drawAllocEnd - drawAllocCount;
					int instanceAllocEnd = Interlocked.Add(UnsafeUtility.AsRef<int>((void*)allocCounters), allocInfo.instanceCount);
					allocInfo.instanceAllocIndex = instanceAllocEnd - allocInfo.instanceCount;
					if (!allocInfo.IsWithinLimits(in this.indirectBufferLimits))
					{
						allocInfo = default(IndirectBufferAllocInfo);
						isValid = false;
					}
					this.indirectBufferAllocInfo[0] = allocInfo;
				}
				if (isValid)
				{
					break;
				}
				allowIndirect = false;
			}
			if (outDirectCommandIndex != 0)
			{
				output.drawCommandCount = outDirectCommandIndex;
				output.drawCommands = MemoryUtilities.Malloc<BatchDrawCommand>(outDirectCommandIndex, Allocator.TempJob);
				output.visibleInstanceCount = outDirectVisibleInstanceIndex;
				output.visibleInstances = MemoryUtilities.Malloc<int>(outDirectVisibleInstanceIndex, Allocator.TempJob);
			}
			if (outIndirectCommandIndex != 0)
			{
				output.indirectDrawCommandCount = outIndirectCommandIndex;
				output.indirectDrawCommands = MemoryUtilities.Malloc<BatchDrawCommandIndirect>(outIndirectCommandIndex, Allocator.TempJob);
			}
			int totalCommandCount = outDirectCommandIndex + outIndirectCommandIndex;
			output.instanceSortingPositions = MemoryUtilities.Malloc<float>(3 * totalCommandCount, Allocator.TempJob);
			this.cullingOutput[0] = output;
		}

		// Token: 0x040000F0 RID: 240
		[ReadOnly]
		public NativeList<DrawRange> drawRanges;

		// Token: 0x040000F1 RID: 241
		[ReadOnly]
		public NativeArray<int> drawBatchIndices;

		// Token: 0x040000F2 RID: 242
		[ReadOnly]
		public NativeArray<int> batchBinAllocOffsets;

		// Token: 0x040000F3 RID: 243
		[ReadOnly]
		public NativeArray<int> batchBinCounts;

		// Token: 0x040000F4 RID: 244
		[ReadOnly]
		public NativeArray<int> binVisibleInstanceCounts;

		// Token: 0x040000F5 RID: 245
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		[WriteOnly]
		public NativeArray<int> batchDrawCommandOffsets;

		// Token: 0x040000F6 RID: 246
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		[WriteOnly]
		public NativeArray<int> binVisibleInstanceOffsets;

		// Token: 0x040000F7 RID: 247
		[NativeDisableUnsafePtrRestriction]
		public NativeArray<BatchCullingOutputDrawCommands> cullingOutput;

		// Token: 0x040000F8 RID: 248
		[ReadOnly]
		public IndirectBufferLimits indirectBufferLimits;

		// Token: 0x040000F9 RID: 249
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		public NativeArray<IndirectBufferAllocInfo> indirectBufferAllocInfo;

		// Token: 0x040000FA RID: 250
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		public NativeArray<int> indirectAllocationCounters;
	}
}
