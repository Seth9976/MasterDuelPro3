using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001A9 RID: 425
	[BurstCompile(FloatMode = FloatMode.Fast, DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct ZBinningJob : IJobFor
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x0002D941 File Offset: 0x0002BB41
		private static uint EncodeHeader(uint min, uint max)
		{
			return (min & 65535U) | ((max & 65535U) << 16);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0002D955 File Offset: 0x0002BB55
		private static ValueTuple<uint, uint> DecodeHeader(uint zBin)
		{
			return new ValueTuple<uint, uint>(zBin & 65535U, (zBin >> 16) & 65535U);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002D970 File Offset: 0x0002BB70
		public void Execute(int jobIndex)
		{
			int batchIndex = jobIndex % this.batchCount;
			int viewIndex = jobIndex / this.batchCount;
			int binStart = 128 * batchIndex;
			int binEnd = math.min(binStart + 128, this.binCount) - 1;
			int binOffset = viewIndex * this.binCount;
			uint emptyHeader = ZBinningJob.EncodeHeader(65535U, 0U);
			for (int binIndex = binStart; binIndex <= binEnd; binIndex++)
			{
				this.bins[(binOffset + binIndex) * (2 + this.wordsPerTile)] = emptyHeader;
				this.bins[(binOffset + binIndex) * (2 + this.wordsPerTile) + 1] = emptyHeader;
			}
			this.FillZBins(binStart, binEnd, 0, this.lightCount, 0, viewIndex * this.lightCount, binOffset);
			this.FillZBins(binStart, binEnd, this.lightCount, this.lightCount + this.reflectionProbeCount, 1, this.lightCount * (this.viewCount - 1) + viewIndex * this.reflectionProbeCount, binOffset);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0002DA5C File Offset: 0x0002BC5C
		private void FillZBins(int binStart, int binEnd, int itemStart, int itemEnd, int headerIndex, int itemOffset, int binOffset)
		{
			for (int index = itemStart; index < itemEnd; index++)
			{
				float2 minMax = this.minMaxZs[itemOffset + index];
				int num = math.max((int)((this.isOrthographic ? minMax.x : math.log2(minMax.x)) * this.zBinScale + this.zBinOffset), binStart);
				int maxBin = math.min((int)((this.isOrthographic ? minMax.y : math.log2(minMax.y)) * this.zBinScale + this.zBinOffset), binEnd);
				int wordIndex = index / 32;
				uint bitMask = 1U << index % 32;
				for (int binIndex = num; binIndex <= maxBin; binIndex++)
				{
					int baseIndex = (binOffset + binIndex) * (2 + this.wordsPerTile);
					ValueTuple<uint, uint> valueTuple = ZBinningJob.DecodeHeader(this.bins[baseIndex + headerIndex]);
					uint minIndex = valueTuple.Item1;
					uint maxIndex = valueTuple.Item2;
					minIndex = math.min(minIndex, (uint)index);
					maxIndex = math.max(maxIndex, (uint)index);
					this.bins[baseIndex + headerIndex] = ZBinningJob.EncodeHeader(minIndex, maxIndex);
					ref NativeArray<uint> ptr = ref this.bins;
					int num2 = baseIndex + 2 + wordIndex;
					ptr[num2] |= bitMask;
				}
			}
		}

		// Token: 0x04000951 RID: 2385
		public const int batchSize = 128;

		// Token: 0x04000952 RID: 2386
		public const int headerLength = 2;

		// Token: 0x04000953 RID: 2387
		[NativeDisableParallelForRestriction]
		public NativeArray<uint> bins;

		// Token: 0x04000954 RID: 2388
		[ReadOnly]
		public NativeArray<float2> minMaxZs;

		// Token: 0x04000955 RID: 2389
		public float zBinScale;

		// Token: 0x04000956 RID: 2390
		public float zBinOffset;

		// Token: 0x04000957 RID: 2391
		public int binCount;

		// Token: 0x04000958 RID: 2392
		public int wordsPerTile;

		// Token: 0x04000959 RID: 2393
		public int lightCount;

		// Token: 0x0400095A RID: 2394
		public int reflectionProbeCount;

		// Token: 0x0400095B RID: 2395
		public int batchCount;

		// Token: 0x0400095C RID: 2396
		public int viewCount;

		// Token: 0x0400095D RID: 2397
		public bool isOrthographic;
	}
}
