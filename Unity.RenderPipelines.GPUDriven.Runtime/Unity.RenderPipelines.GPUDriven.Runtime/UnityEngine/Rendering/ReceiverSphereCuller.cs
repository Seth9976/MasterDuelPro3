using System;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200001A RID: 26
	internal struct ReceiverSphereCuller
	{
		// Token: 0x06000087 RID: 135 RVA: 0x000041FC File Offset: 0x000023FC
		internal static ReceiverSphereCuller CreateEmptyForTesting(Allocator allocator)
		{
			return new ReceiverSphereCuller
			{
				splitInfos = new NativeList<ReceiverSphereCuller.SplitInfo>(0, allocator),
				worldToLightSpaceRotation = float3x3.identity
			};
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004231 File Offset: 0x00002431
		internal void Dispose(JobHandle job)
		{
			this.splitInfos.Dispose(job);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004240 File Offset: 0x00002440
		internal bool UseReceiverPlanes()
		{
			return this.splitInfos.Length == 0;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004250 File Offset: 0x00002450
		internal static ReceiverSphereCuller Create(in BatchCullingContext cc, Allocator allocator)
		{
			int splitCount = cc.cullingSplits.Length;
			bool allSpheresValid = splitCount > 1;
			for (int splitIndex = 0; splitIndex < splitCount; splitIndex++)
			{
				if (cc.cullingSplits[splitIndex].sphereRadius <= 0f)
				{
					allSpheresValid = false;
				}
			}
			if (!allSpheresValid)
			{
				splitCount = 0;
			}
			float3x3 lightToWorldSpaceRotation = (float3x3)cc.localToWorldMatrix;
			ReceiverSphereCuller result = new ReceiverSphereCuller
			{
				splitInfos = new NativeList<ReceiverSphereCuller.SplitInfo>(splitCount, allocator),
				worldToLightSpaceRotation = math.transpose(lightToWorldSpaceRotation)
			};
			result.splitInfos.ResizeUninitialized(splitCount);
			for (int splitIndex2 = 0; splitIndex2 < splitCount; splitIndex2++)
			{
				CullingSplit cullingSplit = cc.cullingSplits[splitIndex2];
				float4 receiverSphereLightSpace = new float4(math.mul(result.worldToLightSpaceRotation, cullingSplit.sphereCenter), cullingSplit.sphereRadius);
				result.splitInfos[splitIndex2] = new ReceiverSphereCuller.SplitInfo
				{
					receiverSphereLightSpace = receiverSphereLightSpace,
					cascadeBlendCullingFactor = cullingSplit.cascadeBlendCullingFactor
				};
			}
			return result;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004368 File Offset: 0x00002568
		internal static float DistanceUntilCylinderFullyCrossesPlane(float3 cylinderCenter, float3 cylinderDirection, float cylinderRadius, Plane plane)
		{
			float cosEpsilon = 0.001f;
			float cosTheta = math.max(math.abs(math.dot(plane.normal, cylinderDirection)), cosEpsilon);
			float num = (math.dot(plane.normal, cylinderCenter) + plane.distance) / cosTheta;
			float sinTheta = math.sqrt(math.max(1f - cosTheta * cosTheta, 0f));
			float edgeDistanceToPlane = cylinderRadius * sinTheta / cosTheta;
			return num + edgeDistanceToPlane;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000043D8 File Offset: 0x000025D8
		internal static uint ComputeSplitVisibilityMask(NativeArray<Plane> lightFacingFrustumPlanes, NativeArray<ReceiverSphereCuller.SplitInfo> splitInfos, float3x3 worldToLightSpaceRotation, in AABB bounds)
		{
			float3 casterCenterWorldSpace = bounds.center;
			float3 casterCenterLightSpace = math.mul(worldToLightSpaceRotation, bounds.center);
			float casterRadius = math.length(bounds.extents);
			float3 shadowDirection = math.transpose(worldToLightSpaceRotation).c2;
			float shadowLength = float.PositiveInfinity;
			for (int i = 0; i < lightFacingFrustumPlanes.Length; i++)
			{
				shadowLength = math.min(shadowLength, ReceiverSphereCuller.DistanceUntilCylinderFullyCrossesPlane(casterCenterWorldSpace, shadowDirection, casterRadius, lightFacingFrustumPlanes[i]));
			}
			shadowLength = math.max(shadowLength, 0f);
			uint splitVisibilityMask = 0U;
			int splitCount = splitInfos.Length;
			for (int splitIndex = 0; splitIndex < splitCount; splitIndex++)
			{
				ReceiverSphereCuller.SplitInfo splitInfo = splitInfos[splitIndex];
				float3 receiverCenterLightSpace = splitInfo.receiverSphereLightSpace.xyz;
				float receiverRadius = splitInfo.receiverSphereLightSpace.w;
				float3 receiverToCasterLightSpace = casterCenterLightSpace - receiverCenterLightSpace;
				float zSqAtSphereIntersection = math.lengthsq(casterRadius + receiverRadius) - math.lengthsq(receiverToCasterLightSpace.xy);
				if (zSqAtSphereIntersection >= 0f && (receiverToCasterLightSpace.z <= 0f || math.lengthsq(receiverToCasterLightSpace.z) <= zSqAtSphereIntersection))
				{
					splitVisibilityMask |= 1U << splitIndex;
					float num = receiverRadius * splitInfo.cascadeBlendCullingFactor;
					float3 receiverToShadowEndLightSpace = receiverToCasterLightSpace + new float3(0f, 0f, shadowLength);
					float capsuleMaxDistance = num - casterRadius;
					float capsuleDistanceSq = math.max(math.lengthsq(receiverToCasterLightSpace), math.lengthsq(receiverToShadowEndLightSpace));
					if (capsuleMaxDistance > 0f && capsuleDistanceSq < math.lengthsq(capsuleMaxDistance))
					{
						break;
					}
				}
			}
			return splitVisibilityMask;
		}

		// Token: 0x04000042 RID: 66
		public NativeList<ReceiverSphereCuller.SplitInfo> splitInfos;

		// Token: 0x04000043 RID: 67
		public float3x3 worldToLightSpaceRotation;

		// Token: 0x0200001B RID: 27
		internal struct SplitInfo
		{
			// Token: 0x04000044 RID: 68
			public float4 receiverSphereLightSpace;

			// Token: 0x04000045 RID: 69
			public float cascadeBlendCullingFactor;
		}
	}
}
