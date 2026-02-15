using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x0200004F RID: 79
	[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct FindDrawInstancesJob : IJobParallelForBatch
	{
		// Token: 0x06000136 RID: 310 RVA: 0x00008A10 File Offset: 0x00006C10
		public unsafe void Execute(int startIndex, int count)
		{
			int* instancesToRemove = stackalloc int[(UIntPtr)512];
			int length = 0;
			for (int i = startIndex; i < startIndex + count; i++)
			{
				ref DrawInstance drawInstance = ref this.drawInstances.ElementAt(i);
				if (this.instancesSorted.BinarySearch(InstanceHandle.FromInt(drawInstance.instanceIndex)) >= 0)
				{
					instancesToRemove[(IntPtr)(length++) * 4] = i;
				}
			}
			this.outDrawInstanceIndicesWriter.AddRangeNoResize((void*)instancesToRemove, length);
		}

		// Token: 0x0400015F RID: 351
		public const int k_BatchSize = 128;

		// Token: 0x04000160 RID: 352
		[ReadOnly]
		public NativeArray<InstanceHandle> instancesSorted;

		// Token: 0x04000161 RID: 353
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		[ReadOnly]
		public NativeList<DrawInstance> drawInstances;

		// Token: 0x04000162 RID: 354
		[WriteOnly]
		public NativeList<int>.ParallelWriter outDrawInstanceIndicesWriter;
	}
}
