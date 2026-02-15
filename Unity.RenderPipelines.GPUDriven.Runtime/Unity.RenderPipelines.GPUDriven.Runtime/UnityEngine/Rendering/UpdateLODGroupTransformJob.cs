using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A7 RID: 167
	[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct UpdateLODGroupTransformJob : IJobParallelFor
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x000113BC File Offset: 0x0000F5BC
		public unsafe void Execute(int index)
		{
			int lodGroupID = this.lodGroupIDs[index];
			GPUInstanceIndex lodGroupInstance;
			if (this.lodGroupDataHash.TryGetValue(lodGroupID, out lodGroupInstance))
			{
				float worldSpaceSize = this.worldSpaceSizes[index];
				LODGroupData* lodGroup = this.lodGroupData.GetUnsafePtr<LODGroupData>() + lodGroupInstance.index;
				LODGroupCullingData* lodGroupTransformResult = this.lodGroupCullingData.GetUnsafePtr<LODGroupCullingData>() + lodGroupInstance.index;
				lodGroupTransformResult->worldSpaceSize = worldSpaceSize;
				lodGroupTransformResult->worldSpaceReferencePoint = this.worldSpaceReferencePoints[index];
				for (int i = 0; i < lodGroup->lodCount; i++)
				{
					float lodHeight = *((ref lodGroup->screenRelativeTransitionHeights.FixedElementField) + (IntPtr)i * 4);
					float lodDist = LODGroupRenderingUtils.CalculateLODDistance(lodHeight, worldSpaceSize);
					*((ref lodGroupTransformResult->sqrDistances.FixedElementField) + (IntPtr)i * 4) = lodDist * lodDist;
					if (this.supportDitheringCrossFade && !(*((ref lodGroupTransformResult->percentageFlags.FixedElementField) + i)))
					{
						float prevLODHeight = ((i != 0) ? (*((ref lodGroup->screenRelativeTransitionHeights.FixedElementField) + (IntPtr)(i - 1) * 4)) : 1f);
						float transitionHeight = lodHeight + *((ref lodGroup->fadeTransitionWidth.FixedElementField) + (IntPtr)i * 4) * (prevLODHeight - lodHeight);
						float transitionDistance = lodDist - LODGroupRenderingUtils.CalculateLODDistance(transitionHeight, worldSpaceSize);
						transitionDistance = Mathf.Max(0f, transitionDistance);
						*((ref lodGroupTransformResult->transitionDistances.FixedElementField) + (IntPtr)i * 4) = transitionDistance;
					}
					else
					{
						*((ref lodGroupTransformResult->transitionDistances.FixedElementField) + (IntPtr)i * 4) = 0f;
					}
				}
			}
		}

		// Token: 0x0400035E RID: 862
		public const int k_BatchSize = 256;

		// Token: 0x0400035F RID: 863
		[ReadOnly]
		public NativeParallelHashMap<int, GPUInstanceIndex> lodGroupDataHash;

		// Token: 0x04000360 RID: 864
		[ReadOnly]
		public NativeArray<int> lodGroupIDs;

		// Token: 0x04000361 RID: 865
		[ReadOnly]
		public NativeArray<Vector3> worldSpaceReferencePoints;

		// Token: 0x04000362 RID: 866
		[ReadOnly]
		public NativeArray<float> worldSpaceSizes;

		// Token: 0x04000363 RID: 867
		[ReadOnly]
		public bool requiresGPUUpload;

		// Token: 0x04000364 RID: 868
		[ReadOnly]
		public bool supportDitheringCrossFade;

		// Token: 0x04000365 RID: 869
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		[ReadOnly]
		public NativeList<LODGroupData> lodGroupData;

		// Token: 0x04000366 RID: 870
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		[WriteOnly]
		public NativeList<LODGroupCullingData> lodGroupCullingData;

		// Token: 0x04000367 RID: 871
		[NativeDisableUnsafePtrRestriction]
		public UnsafeAtomicCounter32 atomicUpdateCount;
	}
}
