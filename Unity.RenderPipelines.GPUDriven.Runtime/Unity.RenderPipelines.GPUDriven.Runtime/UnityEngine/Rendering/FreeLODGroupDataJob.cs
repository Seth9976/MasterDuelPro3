using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x020000AA RID: 170
	[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct FreeLODGroupDataJob : IJob
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x00011924 File Offset: 0x0000FB24
		public unsafe void Execute()
		{
			foreach (int lodGroupID in this.destroyedLODGroupsID)
			{
				GPUInstanceIndex lodGroupInstance;
				if (this.lodGroupDataHash.TryGetValue(lodGroupID, out lodGroupInstance))
				{
					this.lodGroupDataHash.Remove(lodGroupID);
					this.freeLODGroupDataHandles.Add(in lodGroupInstance);
					ref LODGroupData lodGroupData = ref this.lodGroupsData.ElementAt(lodGroupInstance.index);
					*this.removedRendererCount += lodGroupData.rendererCount;
					lodGroupData.valid = false;
				}
			}
		}

		// Token: 0x04000376 RID: 886
		[ReadOnly]
		public NativeArray<int> destroyedLODGroupsID;

		// Token: 0x04000377 RID: 887
		public NativeList<LODGroupData> lodGroupsData;

		// Token: 0x04000378 RID: 888
		public NativeParallelHashMap<int, GPUInstanceIndex> lodGroupDataHash;

		// Token: 0x04000379 RID: 889
		public NativeList<GPUInstanceIndex> freeLODGroupDataHandles;

		// Token: 0x0400037A RID: 890
		[NativeDisableUnsafePtrRestriction]
		public unsafe int* removedRendererCount;
	}
}
