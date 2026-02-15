using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x0200003E RID: 62
	[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct CompactVisibilityMasksJob : IJobParallelForBatch
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00006F74 File Offset: 0x00005174
		public void Execute(int startIndex, int count)
		{
			ulong chunkBits = 0UL;
			for (int i = 0; i < count; i++)
			{
				if (this.rendererVisibilityMasks[startIndex + i] != 0)
				{
					chunkBits |= 1UL << i;
				}
			}
			int chunkIndex = startIndex / 64;
			this.compactedVisibilityMasks.InterlockedOrChunk(chunkIndex, chunkBits);
		}

		// Token: 0x04000110 RID: 272
		public const int k_BatchSize = 64;

		// Token: 0x04000111 RID: 273
		[ReadOnly]
		public NativeArray<byte> rendererVisibilityMasks;

		// Token: 0x04000112 RID: 274
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		public ParallelBitArray compactedVisibilityMasks;
	}
}
