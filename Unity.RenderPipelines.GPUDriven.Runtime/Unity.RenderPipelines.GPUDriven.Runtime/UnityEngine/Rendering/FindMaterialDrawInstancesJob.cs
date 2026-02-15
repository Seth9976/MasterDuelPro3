using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x02000050 RID: 80
	[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct FindMaterialDrawInstancesJob : IJobParallelForBatch
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00008A78 File Offset: 0x00006C78
		public unsafe void Execute(int startIndex, int count)
		{
			int* instancesToRemove = stackalloc int[(UIntPtr)512];
			int length = 0;
			for (int i = startIndex; i < startIndex + count; i++)
			{
				ref DrawInstance drawInstance = ref this.drawInstances.ElementAt(i);
				if (this.materialsSorted.BinarySearch(drawInstance.key.materialID.value) >= 0)
				{
					instancesToRemove[(IntPtr)(length++) * 4] = i;
				}
			}
			this.outDrawInstanceIndicesWriter.AddRangeNoResize((void*)instancesToRemove, length);
		}

		// Token: 0x04000163 RID: 355
		public const int k_BatchSize = 128;

		// Token: 0x04000164 RID: 356
		[ReadOnly]
		public NativeArray<uint> materialsSorted;

		// Token: 0x04000165 RID: 357
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		[ReadOnly]
		public NativeList<DrawInstance> drawInstances;

		// Token: 0x04000166 RID: 358
		[WriteOnly]
		public NativeList<int>.ParallelWriter outDrawInstanceIndicesWriter;
	}
}
