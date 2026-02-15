using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000039 RID: 57
	[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
	internal struct CullingJob : IJobParallelFor
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00005C3C File Offset: 0x00003E3C
		private static uint PackFloatToUint8(float percent)
		{
			uint packed = (uint)((1f + percent) * 127f + 0.5f);
			if (percent < 0f)
			{
				packed = math.clamp(packed, 0U, 126U);
			}
			else
			{
				packed = math.clamp(packed, 128U, 254U);
			}
			return packed;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005C84 File Offset: 0x00003E84
		private unsafe float CalculateLODVisibility(int instanceIndex, int sharedInstanceIndex, InstanceFlags instanceFlags)
		{
			float lodPercent = 1f;
			uint lodDataIndexAndMask = this.sharedInstanceData.lodGroupAndMasks[sharedInstanceIndex];
			if (lodDataIndexAndMask != 4294967295U)
			{
				lodPercent = 0f;
				uint lodIndex = lodDataIndexAndMask >> 8;
				uint lodMask = lodDataIndexAndMask & 255U;
				ref LODGroupCullingData lodGroup = ref this.lodGroupCullingData.ElementAt((int)lodIndex);
				float cameraSqrDistToLODCenter = (this.isOrtho ? this.sqrScreenRelativeMetric : LODGroupRenderingUtils.CalculateSqrPerspectiveDistance(lodGroup.worldSpaceReferencePoint, this.cameraPosition, this.sqrScreenRelativeMetric));
				uint maxLodMask = uint.MaxValue << this.maxLOD;
				lodMask &= maxLodMask;
				int i = math.max(math.tzcnt(lodMask) - 1, this.maxLOD);
				lodMask >>= i;
				while (lodMask > 0U)
				{
					float lodRangeSqrMin = ((i == this.maxLOD) ? 0f : (*((ref lodGroup.sqrDistances.FixedElementField) + (IntPtr)(i - 1) * 4)));
					float lodRangeSqrMax = *((ref lodGroup.sqrDistances.FixedElementField) + (IntPtr)i * 4);
					if (cameraSqrDistToLODCenter < lodRangeSqrMin)
					{
						break;
					}
					if (cameraSqrDistToLODCenter < lodRangeSqrMax)
					{
						CullingJob.CrossFadeType type = (CullingJob.CrossFadeType)(lodMask & 3U);
						if (type == CullingJob.CrossFadeType.kDisabled)
						{
							break;
						}
						if (type == CullingJob.CrossFadeType.kVisible)
						{
							lodPercent = 1f;
							break;
						}
						float distanceToLodCenter = math.sqrt(cameraSqrDistToLODCenter);
						float maxDist = math.sqrt(lodRangeSqrMax);
						if (*((ref lodGroup.percentageFlags.FixedElementField) + i))
						{
							if (type == CullingJob.CrossFadeType.kCrossFadeIn)
							{
								lodPercent = 0f;
								break;
							}
							if (type == CullingJob.CrossFadeType.kCrossFadeOut)
							{
								float minDist = ((i > 0) ? math.sqrt(*((ref lodGroup.sqrDistances.FixedElementField) + (IntPtr)(i - 1) * 4)) : lodGroup.worldSpaceSize);
								lodPercent = 2f + math.max(distanceToLodCenter - minDist, 0f) / (maxDist - minDist);
								break;
							}
							break;
						}
						else
						{
							float transitionDist = *((ref lodGroup.transitionDistances.FixedElementField) + (IntPtr)i * 4);
							float dif = maxDist - distanceToLodCenter;
							if (dif < transitionDist)
							{
								lodPercent = dif / transitionDist;
								if (type == CullingJob.CrossFadeType.kCrossFadeIn)
								{
									lodPercent = -lodPercent;
									break;
								}
								break;
							}
							else
							{
								if (type == CullingJob.CrossFadeType.kCrossFadeOut)
								{
									lodPercent = 1f;
									break;
								}
								break;
							}
						}
					}
					else
					{
						i++;
						lodMask >>= 1;
					}
				}
			}
			else if (this.viewType < BatchCullingViewType.SelectionOutline && (instanceFlags & InstanceFlags.SmallMeshCulling) != InstanceFlags.None)
			{
				readonly ref AABB worldAABB = ref this.instanceData.worldAABBs.UnsafeElementAt(instanceIndex);
				float cameraDist = math.sqrt(this.isOrtho ? this.sqrScreenRelativeMetric : LODGroupRenderingUtils.CalculateSqrPerspectiveDistance(worldAABB.center, this.cameraPosition, this.sqrScreenRelativeMetric));
				float3 aabbSize = worldAABB.extents * 2f;
				float worldSpaceSize = math.max(math.max(aabbSize.x, aabbSize.y), aabbSize.z);
				float maxDist2 = LODGroupRenderingUtils.CalculateLODDistance(this.minScreenRelativeHeight, worldSpaceSize);
				float transitionHeight = this.minScreenRelativeHeight + 0.1f * this.minScreenRelativeHeight;
				float fadeOutRange = Mathf.Max(0f, maxDist2 - LODGroupRenderingUtils.CalculateLODDistance(transitionHeight, worldSpaceSize));
				lodPercent = math.saturate((maxDist2 - cameraDist) / fadeOutRange);
			}
			return lodPercent;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005F7C File Offset: 0x0000417C
		private uint CalculateVisibilityMask(int instanceIndex, int sharedInstanceIndex, InstanceFlags instanceFlags)
		{
			if (this.cullingLayerMask == 0U)
			{
				return 0U;
			}
			if (((ulong)this.cullingLayerMask & (ulong)(1L << (this.sharedInstanceData.gameObjectLayers[sharedInstanceIndex] & 31))) == 0UL)
			{
				return 0U;
			}
			if (this.cullLightmappedShadowCasters && (instanceFlags & InstanceFlags.AffectsLightmaps) != InstanceFlags.None)
			{
				return 0U;
			}
			if (this.viewType == BatchCullingViewType.Camera && (instanceFlags & InstanceFlags.IsShadowsOnly) != InstanceFlags.None)
			{
				return 0U;
			}
			if (this.viewType == BatchCullingViewType.Light && (instanceFlags & InstanceFlags.IsShadowsOff) != InstanceFlags.None)
			{
				return 0U;
			}
			readonly ref AABB worldAABB = ref this.instanceData.worldAABBs.UnsafeElementAt(instanceIndex);
			uint visibilityMask = FrustumPlaneCuller.ComputeSplitVisibilityMask(this.frustumPlanePackets, this.frustumSplitInfos, in worldAABB);
			if (visibilityMask != 0U && this.receiverSplitInfos.Length > 0)
			{
				visibilityMask &= ReceiverSphereCuller.ComputeSplitVisibilityMask(this.lightFacingFrustumPlanes, this.receiverSplitInfos, this.worldToLightSpaceRotation, in worldAABB);
			}
			if (visibilityMask != 0U && this.occlusionBuffer != IntPtr.Zero)
			{
				visibilityMask = (BatchRendererGroup.OcclusionTestAABB(this.occlusionBuffer, worldAABB.ToBounds()) ? visibilityMask : 0U);
			}
			return visibilityMask;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006070 File Offset: 0x00004270
		public void Execute(int instanceIndex)
		{
			InstanceHandle instance = this.instanceData.instances[instanceIndex];
			int sharedInstanceIndex = this.sharedInstanceData.InstanceToIndex(in this.instanceData, instance);
			InstanceFlags instanceFlags = this.sharedInstanceData.flags[sharedInstanceIndex].instanceFlags;
			uint visibilityMask = this.CalculateVisibilityMask(instanceIndex, sharedInstanceIndex, instanceFlags);
			uint crossFadeValue = 127U;
			if (visibilityMask != 0U)
			{
				float lodPercent = this.CalculateLODVisibility(instanceIndex, sharedInstanceIndex, instanceFlags);
				if (lodPercent != 0f)
				{
					if (this.binningConfig.supportsMotionCheck)
					{
						bool hasMotion = this.instanceData.movedInPreviousFrameBits.Get(instanceIndex);
						visibilityMask = (visibilityMask << 1) | (hasMotion ? 1U : 0U);
					}
					if (this.binningConfig.supportsCrossFade)
					{
						bool hasDitheringCrossFade = false;
						if (lodPercent != 1f)
						{
							if (lodPercent >= 2f)
							{
								lodPercent -= 2f;
							}
							else
							{
								hasDitheringCrossFade = true;
							}
							crossFadeValue = CullingJob.PackFloatToUint8(lodPercent);
						}
						visibilityMask = (visibilityMask << 1) | (hasDitheringCrossFade ? 1U : 0U);
					}
				}
				else
				{
					visibilityMask = 0U;
				}
			}
			this.rendererVisibilityMasks[instance.index] = (byte)visibilityMask;
			this.rendererCrossFadeValues[instance.index] = (byte)crossFadeValue;
		}

		// Token: 0x040000C4 RID: 196
		public const int k_BatchSize = 32;

		// Token: 0x040000C5 RID: 197
		private const uint k_LODFadeZeroPacked = 127U;

		// Token: 0x040000C6 RID: 198
		private const float k_LODPercentInvisible = 0f;

		// Token: 0x040000C7 RID: 199
		private const float k_LODPercentFullyVisible = 1f;

		// Token: 0x040000C8 RID: 200
		private const float k_LODPercentSpeedTree = 2f;

		// Token: 0x040000C9 RID: 201
		private const float k_SmallMeshTransitionWidth = 0.1f;

		// Token: 0x040000CA RID: 202
		[ReadOnly]
		public BinningConfig binningConfig;

		// Token: 0x040000CB RID: 203
		[ReadOnly]
		public BatchCullingViewType viewType;

		// Token: 0x040000CC RID: 204
		[ReadOnly]
		public float3 cameraPosition;

		// Token: 0x040000CD RID: 205
		[ReadOnly]
		public float sqrScreenRelativeMetric;

		// Token: 0x040000CE RID: 206
		[ReadOnly]
		public float minScreenRelativeHeight;

		// Token: 0x040000CF RID: 207
		[ReadOnly]
		public bool isOrtho;

		// Token: 0x040000D0 RID: 208
		[ReadOnly]
		public bool cullLightmappedShadowCasters;

		// Token: 0x040000D1 RID: 209
		[ReadOnly]
		public int maxLOD;

		// Token: 0x040000D2 RID: 210
		[ReadOnly]
		public uint cullingLayerMask;

		// Token: 0x040000D3 RID: 211
		[ReadOnly]
		public ulong sceneCullingMask;

		// Token: 0x040000D4 RID: 212
		[ReadOnly]
		public NativeArray<FrustumPlaneCuller.PlanePacket4> frustumPlanePackets;

		// Token: 0x040000D5 RID: 213
		[ReadOnly]
		public NativeArray<FrustumPlaneCuller.SplitInfo> frustumSplitInfos;

		// Token: 0x040000D6 RID: 214
		[ReadOnly]
		public NativeArray<Plane> lightFacingFrustumPlanes;

		// Token: 0x040000D7 RID: 215
		[ReadOnly]
		public NativeArray<ReceiverSphereCuller.SplitInfo> receiverSplitInfos;

		// Token: 0x040000D8 RID: 216
		public float3x3 worldToLightSpaceRotation;

		// Token: 0x040000D9 RID: 217
		[ReadOnly]
		public CPUInstanceData.ReadOnly instanceData;

		// Token: 0x040000DA RID: 218
		[ReadOnly]
		public CPUSharedInstanceData.ReadOnly sharedInstanceData;

		// Token: 0x040000DB RID: 219
		[NativeDisableContainerSafetyRestriction]
		[NoAlias]
		[ReadOnly]
		public NativeList<LODGroupCullingData> lodGroupCullingData;

		// Token: 0x040000DC RID: 220
		[NativeDisableUnsafePtrRestriction]
		[ReadOnly]
		public IntPtr occlusionBuffer;

		// Token: 0x040000DD RID: 221
		[NativeDisableParallelForRestriction]
		[WriteOnly]
		public NativeArray<byte> rendererVisibilityMasks;

		// Token: 0x040000DE RID: 222
		[NativeDisableParallelForRestriction]
		[WriteOnly]
		public NativeArray<byte> rendererCrossFadeValues;

		// Token: 0x0200003A RID: 58
		private enum CrossFadeType
		{
			// Token: 0x040000E0 RID: 224
			kDisabled,
			// Token: 0x040000E1 RID: 225
			kCrossFadeOut,
			// Token: 0x040000E2 RID: 226
			kCrossFadeIn,
			// Token: 0x040000E3 RID: 227
			kVisible
		}
	}
}
