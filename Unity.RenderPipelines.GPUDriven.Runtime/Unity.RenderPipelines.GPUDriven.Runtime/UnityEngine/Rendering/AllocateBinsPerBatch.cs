using System;
using System.Threading;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200003B RID: 59
	[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct AllocateBinsPerBatch : IJobParallelFor
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00006194 File Offset: 0x00004394
		private bool IsInstanceFlipped(int rendererIndex)
		{
			InstanceHandle instance = InstanceHandle.FromInt(rendererIndex);
			int instanceIndex = this.instanceData.InstanceToIndex(instance);
			return this.instanceData.localToWorldIsFlippedBits.Get(instanceIndex);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000061CC File Offset: 0x000043CC
		public unsafe void Execute(int batchIndex)
		{
			int configCount = this.binningConfig.visibilityConfigCount;
			int* visibleCountPerConfig;
			checked
			{
				visibleCountPerConfig = stackalloc int[unchecked((UIntPtr)configCount) * 4];
			}
			for (int i = 0; i < configCount; i++)
			{
				visibleCountPerConfig[i] = 0;
			}
			int configMaskCount = (configCount + 63) / 64;
			ulong* configUsedMasks;
			checked
			{
				configUsedMasks = stackalloc ulong[unchecked((UIntPtr)configMaskCount) * 8];
			}
			for (int j = 0; j < configMaskCount; j++)
			{
				configUsedMasks[j] = 0UL;
			}
			DrawBatch drawBatch = this.drawBatches[batchIndex];
			int instanceCount = drawBatch.instanceCount;
			int instanceOffset = drawBatch.instanceOffset;
			for (int k = 0; k < instanceCount; k++)
			{
				int rendererIndex = this.drawInstanceIndices[instanceOffset + k];
				bool isFlipped = this.IsInstanceFlipped(rendererIndex);
				int visibilityMask = (int)this.rendererVisibilityMasks[rendererIndex];
				if (visibilityMask != 0)
				{
					int configIndex = (visibilityMask << 1) | (isFlipped ? 1 : 0);
					visibleCountPerConfig[configIndex]++;
					configUsedMasks[configIndex >> 6] |= 1UL << configIndex;
				}
			}
			int binCount = 0;
			for (int l = 0; l < configMaskCount; l++)
			{
				binCount += math.countbits(configUsedMasks[l]);
			}
			int allocOffsetStart = 0;
			if (binCount > 0)
			{
				int* drawCommandCountPerView;
				int* visibleCountPerView;
				checked
				{
					drawCommandCountPerView = stackalloc int[unchecked((UIntPtr)this.binningConfig.viewCount) * 4];
					visibleCountPerView = stackalloc int[unchecked((UIntPtr)this.binningConfig.viewCount) * 4];
				}
				for (int m = 0; m < this.binningConfig.viewCount; m++)
				{
					drawCommandCountPerView[m] = 0;
					visibleCountPerView[m] = 0;
				}
				bool countVisibilityStats = this.debugCounterIndexBase >= 0;
				int shiftForVisibilityMask = 1 + (this.binningConfig.supportsMotionCheck ? 1 : 0) + (this.binningConfig.supportsCrossFade ? 1 : 0);
				int* allocCounter = (int*)this.binAllocCounter.GetUnsafePtr<int>();
				allocOffsetStart = Interlocked.Add(UnsafeUtility.AsRef<int>((void*)allocCounter), binCount) - binCount;
				int allocOffset = allocOffsetStart;
				for (int n = 0; n < configMaskCount; n++)
				{
					ulong configRemainMask = configUsedMasks[n];
					while (configRemainMask != 0UL)
					{
						int bitPos = math.tzcnt(configRemainMask);
						configRemainMask ^= 1UL << bitPos;
						int configIndex2 = 64 * n + bitPos;
						int visibleCount = visibleCountPerConfig[configIndex2];
						this.binConfigIndices[allocOffset] = (short)configIndex2;
						this.binVisibleInstanceCounts[allocOffset] = visibleCount;
						allocOffset++;
						int visibilityMask2 = (countVisibilityStats ? (configIndex2 >> shiftForVisibilityMask) : 0);
						while (visibilityMask2 != 0)
						{
							int viewIndex = math.tzcnt(visibilityMask2);
							visibilityMask2 ^= 1 << viewIndex;
							drawCommandCountPerView[viewIndex]++;
							visibleCountPerView[viewIndex] += visibleCount;
						}
					}
				}
				if (countVisibilityStats)
				{
					for (int viewIndex2 = 0; viewIndex2 < this.binningConfig.viewCount; viewIndex2++)
					{
						int* counterPtr = (int*)((byte*)this.splitDebugCounters.GetUnsafePtr<int>() + (IntPtr)((this.debugCounterIndexBase + viewIndex2) * 2) * 4);
						int drawCommandCount = drawCommandCountPerView[viewIndex2];
						if (drawCommandCount > 0)
						{
							Interlocked.Add(UnsafeUtility.AsRef<int>((void*)(counterPtr + 1)), drawCommandCount);
						}
						int visibleCount2 = visibleCountPerView[viewIndex2];
						if (visibleCount2 > 0)
						{
							Interlocked.Add(UnsafeUtility.AsRef<int>((void*)counterPtr), visibleCount2);
						}
					}
				}
			}
			this.batchBinAllocOffsets[batchIndex] = allocOffsetStart;
			this.batchBinCounts[batchIndex] = binCount;
		}

		// Token: 0x040000E4 RID: 228
		[ReadOnly]
		public BinningConfig binningConfig;

		// Token: 0x040000E5 RID: 229
		[ReadOnly]
		public NativeList<DrawBatch> drawBatches;

		// Token: 0x040000E6 RID: 230
		[ReadOnly]
		public NativeArray<int> drawInstanceIndices;

		// Token: 0x040000E7 RID: 231
		[ReadOnly]
		public CPUInstanceData.ReadOnly instanceData;

		// Token: 0x040000E8 RID: 232
		[ReadOnly]
		public NativeArray<byte> rendererVisibilityMasks;

		// Token: 0x040000E9 RID: 233
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		[WriteOnly]
		public NativeArray<int> batchBinAllocOffsets;

		// Token: 0x040000EA RID: 234
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		[WriteOnly]
		public NativeArray<int> batchBinCounts;

		// Token: 0x040000EB RID: 235
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		[DeallocateOnJobCompletion]
		public NativeArray<int> binAllocCounter;

		// Token: 0x040000EC RID: 236
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		[WriteOnly]
		public NativeArray<short> binConfigIndices;

		// Token: 0x040000ED RID: 237
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		[WriteOnly]
		public NativeArray<int> binVisibleInstanceCounts;

		// Token: 0x040000EE RID: 238
		[ReadOnly]
		public int debugCounterIndexBase;

		// Token: 0x040000EF RID: 239
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		public NativeArray<int> splitDebugCounters;
	}
}
