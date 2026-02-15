using System;
using System.Threading;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x0200004E RID: 78
	[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct BuildDrawListsJob : IJobParallelFor
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00008993 File Offset: 0x00006B93
		private unsafe static int IncrementCounter(int* counter)
		{
			return Interlocked.Increment(UnsafeUtility.AsRef<int>((void*)counter)) - 1;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000089A4 File Offset: 0x00006BA4
		public unsafe void Execute(int index)
		{
			ref DrawInstance drawInstance = ref this.drawInstances.ElementAt(index);
			int drawBatchIndex = this.batchHash[drawInstance.key];
			ref DrawBatch ptr = ref this.drawBatches.ElementAt(drawBatchIndex);
			int offset = BuildDrawListsJob.IncrementCounter((int*)((byte*)this.internalDrawIndex.GetUnsafePtr<int>() + (IntPtr)(drawBatchIndex * 16) * 4));
			int writeIndex = ptr.instanceOffset + offset;
			this.drawInstanceIndices[writeIndex] = drawInstance.instanceIndex;
		}

		// Token: 0x04000158 RID: 344
		public const int k_BatchSize = 128;

		// Token: 0x04000159 RID: 345
		public const int k_IntsPerCacheLine = 16;

		// Token: 0x0400015A RID: 346
		[ReadOnly]
		public NativeParallelHashMap<DrawKey, int> batchHash;

		// Token: 0x0400015B RID: 347
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		[ReadOnly]
		public NativeList<DrawInstance> drawInstances;

		// Token: 0x0400015C RID: 348
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		[ReadOnly]
		public NativeList<DrawBatch> drawBatches;

		// Token: 0x0400015D RID: 349
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		[WriteOnly]
		public NativeArray<int> internalDrawIndex;

		// Token: 0x0400015E RID: 350
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		[WriteOnly]
		public NativeArray<int> drawInstanceIndices;
	}
}
