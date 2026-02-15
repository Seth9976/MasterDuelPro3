using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001A3 RID: 419
	[BurstCompile(FloatMode = FloatMode.Fast, DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct TileRangeExpansionJob : IJobFor
	{
		// Token: 0x060008DF RID: 2271 RVA: 0x0002A90C File Offset: 0x00028B0C
		public void Execute(int jobIndex)
		{
			int rowIndex = jobIndex % this.tileResolution.y;
			int viewIndex = jobIndex / this.tileResolution.y;
			int compactCount = 0;
			NativeArray<short> itemIndices = new NativeArray<short>(this.itemsPerTile, Allocator.Temp, NativeArrayOptions.ClearMemory);
			NativeArray<InclusiveRange> itemRanges = new NativeArray<InclusiveRange>(this.itemsPerTile, Allocator.Temp, NativeArrayOptions.ClearMemory);
			for (int itemIndex = 0; itemIndex < this.itemsPerTile; itemIndex++)
			{
				InclusiveRange range = this.tileRanges[viewIndex * this.rangesPerItem * this.itemsPerTile + itemIndex * this.rangesPerItem + 1 + rowIndex];
				if (!range.isEmpty)
				{
					itemIndices[compactCount] = (short)itemIndex;
					itemRanges[compactCount] = range;
					compactCount++;
				}
			}
			int rowBaseMaskIndex = viewIndex * this.wordsPerTile * this.tileResolution.x * this.tileResolution.y + rowIndex * this.wordsPerTile * this.tileResolution.x;
			for (int tileIndex = 0; tileIndex < this.tileResolution.x; tileIndex++)
			{
				int tileBaseIndex = rowBaseMaskIndex + tileIndex * this.wordsPerTile;
				for (int i = 0; i < compactCount; i++)
				{
					int itemIndex2 = (int)itemIndices[i];
					int wordIndex = itemIndex2 / 32;
					uint itemMask = 1U << itemIndex2 % 32;
					if (itemRanges[i].Contains((short)tileIndex))
					{
						ref NativeArray<uint> ptr = ref this.tileMasks;
						int num = tileBaseIndex + wordIndex;
						ptr[num] |= itemMask;
					}
				}
			}
			itemIndices.Dispose();
			itemRanges.Dispose();
		}

		// Token: 0x04000929 RID: 2345
		[ReadOnly]
		public NativeArray<InclusiveRange> tileRanges;

		// Token: 0x0400092A RID: 2346
		[NativeDisableParallelForRestriction]
		public NativeArray<uint> tileMasks;

		// Token: 0x0400092B RID: 2347
		public int rangesPerItem;

		// Token: 0x0400092C RID: 2348
		public int itemsPerTile;

		// Token: 0x0400092D RID: 2349
		public int wordsPerTile;

		// Token: 0x0400092E RID: 2350
		public int2 tileResolution;
	}
}
