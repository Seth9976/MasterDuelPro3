using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x0200004D RID: 77
	[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct PrefixSumDrawInstancesJob : IJob
	{
		// Token: 0x06000133 RID: 307 RVA: 0x00008858 File Offset: 0x00006A58
		public void Execute()
		{
			int drawPrefixSum = 0;
			for (int i = 0; i < this.drawRanges.Length; i++)
			{
				ref DrawRange drawRange = ref this.drawRanges.ElementAt(i);
				drawRange.drawOffset = drawPrefixSum;
				drawPrefixSum += drawRange.drawCount;
			}
			NativeArray<int> internalRangeIndex = new NativeArray<int>(this.drawRanges.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
			for (int j = 0; j < this.drawBatches.Length; j++)
			{
				ref DrawBatch drawBatch = ref this.drawBatches.ElementAt(j);
				int drawRangeIndex;
				if (this.rangeHash.TryGetValue(drawBatch.key.range, out drawRangeIndex))
				{
					ref DrawRange drawRange2 = ref this.drawRanges.ElementAt(drawRangeIndex);
					this.drawBatchIndices[drawRange2.drawOffset + internalRangeIndex[drawRangeIndex]] = j;
					int num = drawRangeIndex;
					int num2 = internalRangeIndex[num];
					internalRangeIndex[num] = num2 + 1;
				}
			}
			int drawInstancesPrefixSum = 0;
			for (int k = 0; k < this.drawBatchIndices.Length; k++)
			{
				int drawBatchIndex = this.drawBatchIndices[k];
				ref DrawBatch drawBatch2 = ref this.drawBatches.ElementAt(drawBatchIndex);
				drawBatch2.instanceOffset = drawInstancesPrefixSum;
				drawInstancesPrefixSum += drawBatch2.instanceCount;
			}
			internalRangeIndex.Dispose();
		}

		// Token: 0x04000154 RID: 340
		[ReadOnly]
		public NativeParallelHashMap<RangeKey, int> rangeHash;

		// Token: 0x04000155 RID: 341
		public NativeList<DrawRange> drawRanges;

		// Token: 0x04000156 RID: 342
		public NativeList<DrawBatch> drawBatches;

		// Token: 0x04000157 RID: 343
		public NativeArray<int> drawBatchIndices;
	}
}
