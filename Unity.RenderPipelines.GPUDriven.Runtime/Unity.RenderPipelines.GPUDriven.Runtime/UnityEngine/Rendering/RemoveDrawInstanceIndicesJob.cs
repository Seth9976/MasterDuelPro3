using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x02000053 RID: 83
	[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct RemoveDrawInstanceIndicesJob : IJob
	{
		// Token: 0x0600013A RID: 314 RVA: 0x00008B64 File Offset: 0x00006D64
		public void RemoveDrawRange(in RangeKey key)
		{
			int drawRangeIndex = this.rangeHash[key];
			ref DrawRange lastDrawRange = ref this.drawRanges.ElementAt(this.drawRanges.Length - 1);
			this.rangeHash[lastDrawRange.key] = drawRangeIndex;
			this.rangeHash.Remove(key);
			this.drawRanges.RemoveAtSwapBack(drawRangeIndex);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00008BCC File Offset: 0x00006DCC
		public void RemoveDrawBatch(in DrawKey key)
		{
			int drawBatchIndex = this.batchHash[key];
			this.drawBatches.ElementAt(drawBatchIndex);
			int drawRangeIndex = this.rangeHash[key.range];
			ref DrawRange drawRange = ref this.drawRanges.ElementAt(drawRangeIndex);
			ref DrawRange ptr = ref drawRange;
			int num = ptr.drawCount - 1;
			ptr.drawCount = num;
			if (num == 0)
			{
				this.RemoveDrawRange(in drawRange.key);
			}
			ref DrawBatch lastDrawBatch = ref this.drawBatches.ElementAt(this.drawBatches.Length - 1);
			this.batchHash[lastDrawBatch.key] = drawBatchIndex;
			this.batchHash.Remove(key);
			this.drawBatches.RemoveAtSwapBack(drawBatchIndex);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00008C80 File Offset: 0x00006E80
		public unsafe void Execute()
		{
			DrawInstance* drawInstancesPtr = this.drawInstances.GetUnsafePtr<DrawInstance>();
			int drawInstancesNewBack = this.drawInstances.Length - 1;
			for (int indexRev = this.drawInstanceIndices.Length - 1; indexRev >= 0; indexRev--)
			{
				int indexToRemove = this.drawInstanceIndices[indexRev];
				DrawInstance* drawInstance = drawInstancesPtr + indexToRemove;
				int drawBatchIndex = this.batchHash[drawInstance->key];
				ref DrawBatch drawBatch = ref this.drawBatches.ElementAt(drawBatchIndex);
				ref DrawBatch ptr = ref drawBatch;
				int num = ptr.instanceCount - 1;
				ptr.instanceCount = num;
				if (num == 0)
				{
					this.RemoveDrawBatch(in drawBatch.key);
				}
				void* ptr2 = (void*)drawInstance;
				DrawInstance* ptr3 = drawInstancesPtr;
				IntPtr intPtr = (IntPtr)(drawInstancesNewBack--);
				UnsafeUtility.MemCpy(ptr2, (void*)((byte*)ptr3 + intPtr * (IntPtr)sizeof(DrawInstance)), (long)sizeof(DrawInstance));
			}
			this.drawInstances.ResizeUninitialized(drawInstancesNewBack + 1);
		}

		// Token: 0x0400016F RID: 367
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		[ReadOnly]
		public NativeArray<int> drawInstanceIndices;

		// Token: 0x04000170 RID: 368
		public NativeList<DrawInstance> drawInstances;

		// Token: 0x04000171 RID: 369
		public NativeParallelHashMap<RangeKey, int> rangeHash;

		// Token: 0x04000172 RID: 370
		public NativeParallelHashMap<DrawKey, int> batchHash;

		// Token: 0x04000173 RID: 371
		public NativeList<DrawRange> drawRanges;

		// Token: 0x04000174 RID: 372
		public NativeList<DrawBatch> drawBatches;
	}
}
