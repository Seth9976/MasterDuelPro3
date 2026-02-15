using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A8 RID: 168
	[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct AllocateOrGetLODGroupDataInstancesJob : IJob
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x00011548 File Offset: 0x0000F748
		public unsafe void Execute()
		{
			int freeHandlesCount = this.freeLODGroupDataHandles.Length;
			int lodDataLength = this.lodGroupsData.Length;
			for (int i = 0; i < this.lodGroupsID.Length; i++)
			{
				int lodGroupID = this.lodGroupsID[i];
				GPUInstanceIndex lodGroupInstance;
				if (!this.lodGroupDataHash.TryGetValue(lodGroupID, out lodGroupInstance))
				{
					if (freeHandlesCount == 0)
					{
						lodGroupInstance = new GPUInstanceIndex
						{
							index = lodDataLength++
						};
					}
					else
					{
						lodGroupInstance = this.freeLODGroupDataHandles[--freeHandlesCount];
					}
					this.lodGroupDataHash.TryAdd(lodGroupID, lodGroupInstance);
				}
				else
				{
					*this.previousRendererCount += this.lodGroupsData.ElementAt(lodGroupInstance.index).rendererCount;
				}
				this.lodGroupInstances[i] = lodGroupInstance;
			}
			this.freeLODGroupDataHandles.ResizeUninitialized(freeHandlesCount);
			this.lodGroupsData.ResizeUninitialized(lodDataLength);
			this.lodGroupCullingData.ResizeUninitialized(lodDataLength);
		}

		// Token: 0x04000368 RID: 872
		[ReadOnly]
		public NativeArray<int> lodGroupsID;

		// Token: 0x04000369 RID: 873
		public NativeList<LODGroupData> lodGroupsData;

		// Token: 0x0400036A RID: 874
		public NativeList<LODGroupCullingData> lodGroupCullingData;

		// Token: 0x0400036B RID: 875
		public NativeParallelHashMap<int, GPUInstanceIndex> lodGroupDataHash;

		// Token: 0x0400036C RID: 876
		public NativeList<GPUInstanceIndex> freeLODGroupDataHandles;

		// Token: 0x0400036D RID: 877
		[WriteOnly]
		public NativeArray<GPUInstanceIndex> lodGroupInstances;

		// Token: 0x0400036E RID: 878
		[NativeDisableUnsafePtrRestriction]
		public unsafe int* previousRendererCount;
	}
}
