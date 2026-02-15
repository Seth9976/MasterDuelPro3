using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A9 RID: 169
	[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct UpdateLODGroupDataJob : IJobParallelFor
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x0001163C File Offset: 0x0000F83C
		public unsafe void Execute(int index)
		{
			GPUInstanceIndex lodGroupInstance = this.lodGroupInstances[index];
			LODFadeMode lodfadeMode = this.inputData.fadeMode[index];
			int lodOffset = this.inputData.lodOffset[index];
			int lodCount = this.inputData.lodCount[index];
			short renderersCount = this.inputData.renderersCount[index];
			Vector3 worldReferencePoint = this.inputData.worldSpaceReferencePoint[index];
			float worldSpaceSize = this.inputData.worldSpaceSize[index];
			bool lastLODIsBillboard = this.inputData.lastLODIsBillboard[index];
			bool useDitheringCrossFade = lodfadeMode != LODFadeMode.None && this.supportDitheringCrossFade;
			bool useSpeedTreeCrossFade = lodfadeMode == LODFadeMode.SpeedTree;
			LODGroupData* lodGroupData = (LODGroupData*)((byte*)this.lodGroupsData.GetUnsafePtr<LODGroupData>() + (IntPtr)lodGroupInstance.index * (IntPtr)sizeof(LODGroupData));
			LODGroupCullingData* lodGroupCullingData = (LODGroupCullingData*)((byte*)this.lodGroupsCullingData.GetUnsafePtr<LODGroupCullingData>() + (IntPtr)lodGroupInstance.index * (IntPtr)sizeof(LODGroupCullingData));
			lodGroupData->valid = true;
			lodGroupData->lodCount = lodCount;
			lodGroupData->rendererCount = (int)(useDitheringCrossFade ? renderersCount : 0);
			lodGroupCullingData->worldSpaceSize = worldSpaceSize;
			lodGroupCullingData->worldSpaceReferencePoint = worldReferencePoint;
			lodGroupCullingData->lodCount = lodCount;
			this.rendererCount.Add(lodGroupData->rendererCount);
			int crossFadeLODBegin = 0;
			if (useSpeedTreeCrossFade)
			{
				int lastLODIndex = lodOffset + (lodCount - 1);
				bool hasBillboardLOD = lodCount > 0 && this.inputData.lodRenderersCount[lastLODIndex] == 1 && lastLODIsBillboard;
				if (lodCount == 0)
				{
					crossFadeLODBegin = 0;
				}
				else if (hasBillboardLOD)
				{
					crossFadeLODBegin = Math.Max(lodCount, 2) - 2;
				}
				else
				{
					crossFadeLODBegin = lodCount - 1;
				}
			}
			for (int i = 0; i < lodCount; i++)
			{
				int lodIndex = lodOffset + i;
				float lodHeight = this.inputData.lodScreenRelativeTransitionHeight[lodIndex];
				float lodDist = LODGroupRenderingUtils.CalculateLODDistance(lodHeight, worldSpaceSize);
				*((ref lodGroupData->screenRelativeTransitionHeights.FixedElementField) + (IntPtr)i * 4) = lodHeight;
				*((ref lodGroupData->fadeTransitionWidth.FixedElementField) + (IntPtr)i * 4) = 0f;
				*((ref lodGroupCullingData->sqrDistances.FixedElementField) + (IntPtr)i * 4) = lodDist * lodDist;
				*((ref lodGroupCullingData->percentageFlags.FixedElementField) + i) = false;
				*((ref lodGroupCullingData->transitionDistances.FixedElementField) + (IntPtr)i * 4) = 0f;
				if (useSpeedTreeCrossFade && i < crossFadeLODBegin)
				{
					*((ref lodGroupCullingData->percentageFlags.FixedElementField) + i) = true;
				}
				else if (useDitheringCrossFade && i >= crossFadeLODBegin)
				{
					float fadeTransitionWidth = this.inputData.lodFadeTransitionWidth[lodIndex];
					float prevLODHeight = ((i != 0) ? this.inputData.lodScreenRelativeTransitionHeight[lodIndex - 1] : 1f);
					float transitionHeight = lodHeight + fadeTransitionWidth * (prevLODHeight - lodHeight);
					float transitionDistance = lodDist - LODGroupRenderingUtils.CalculateLODDistance(transitionHeight, worldSpaceSize);
					transitionDistance = Mathf.Max(0f, transitionDistance);
					*((ref lodGroupData->fadeTransitionWidth.FixedElementField) + (IntPtr)i * 4) = fadeTransitionWidth;
					*((ref lodGroupCullingData->transitionDistances.FixedElementField) + (IntPtr)i * 4) = transitionDistance;
				}
			}
		}

		// Token: 0x0400036F RID: 879
		public const int k_BatchSize = 256;

		// Token: 0x04000370 RID: 880
		[ReadOnly]
		public NativeArray<GPUInstanceIndex> lodGroupInstances;

		// Token: 0x04000371 RID: 881
		[ReadOnly]
		public GPUDrivenLODGroupData inputData;

		// Token: 0x04000372 RID: 882
		[ReadOnly]
		public bool supportDitheringCrossFade;

		// Token: 0x04000373 RID: 883
		public NativeArray<LODGroupData> lodGroupsData;

		// Token: 0x04000374 RID: 884
		public NativeArray<LODGroupCullingData> lodGroupsCullingData;

		// Token: 0x04000375 RID: 885
		[NativeDisableUnsafePtrRestriction]
		public UnsafeAtomicCounter32 rendererCount;
	}
}
