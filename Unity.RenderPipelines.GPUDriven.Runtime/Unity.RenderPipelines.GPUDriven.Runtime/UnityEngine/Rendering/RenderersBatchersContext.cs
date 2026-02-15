using System;
using Unity.Collections;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CA RID: 202
	internal class RenderersBatchersContext : IDisposable
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x000131F1 File Offset: 0x000113F1
		public RenderersParameters renderersParameters
		{
			get
			{
				return this.m_RenderersParameters;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x000131F9 File Offset: 0x000113F9
		public GraphicsBuffer gpuInstanceDataBuffer
		{
			get
			{
				return this.m_InstanceDataBuffer.gpuBuffer;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00013206 File Offset: 0x00011406
		public int activeLodGroupCount
		{
			get
			{
				return this.m_LODGroupDataPool.activeLodGroupCount;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00013213 File Offset: 0x00011413
		public NativeArray<GPUInstanceComponentDesc>.ReadOnly defaultDescriptions
		{
			get
			{
				return this.m_InstanceDataBuffer.descriptions.AsReadOnly();
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00013225 File Offset: 0x00011425
		public NativeArray<MetadataValue> defaultMetadata
		{
			get
			{
				return this.m_InstanceDataBuffer.defaultMetadata;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00013232 File Offset: 0x00011432
		public NativeList<LODGroupCullingData> lodGroupCullingData
		{
			get
			{
				return this.m_LODGroupDataPool.lodGroupCullingData;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0001323F File Offset: 0x0001143F
		public int instanceDataBufferVersion
		{
			get
			{
				return this.m_InstanceDataBuffer.version;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0001324C File Offset: 0x0001144C
		public int instanceDataBufferLayoutVersion
		{
			get
			{
				return this.m_InstanceDataBuffer.layoutVersion;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00013259 File Offset: 0x00011459
		public int crossfadedRendererCount
		{
			get
			{
				return this.m_LODGroupDataPool.crossfadedRendererCount;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00013266 File Offset: 0x00011466
		public SphericalHarmonicsL2 cachedAmbientProbe
		{
			get
			{
				return this.m_CachedAmbientProbe;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0001326E File Offset: 0x0001146E
		public bool hasBoundingSpheres
		{
			get
			{
				return this.m_InstanceDataSystem.hasBoundingSpheres;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0001327B File Offset: 0x0001147B
		public CPUInstanceData.ReadOnly instanceData
		{
			get
			{
				return this.m_InstanceDataSystem.instanceData;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00013288 File Offset: 0x00011488
		public CPUSharedInstanceData.ReadOnly sharedInstanceData
		{
			get
			{
				return this.m_InstanceDataSystem.sharedInstanceData;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00013295 File Offset: 0x00011495
		public GPUInstanceDataBuffer.ReadOnly instanceDataBuffer
		{
			get
			{
				return this.m_InstanceDataBuffer.AsReadOnly();
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x000132A2 File Offset: 0x000114A2
		public NativeArray<InstanceHandle> aliveInstances
		{
			get
			{
				return this.m_InstanceDataSystem.aliveInstances;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x000132AF File Offset: 0x000114AF
		public float smallMeshScreenPercentage
		{
			get
			{
				return this.m_SmallMeshScreenPercentage;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x000132B7 File Offset: 0x000114B7
		public GPUResidentDrawerResources resources
		{
			get
			{
				return this.m_Resources;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x000132BF File Offset: 0x000114BF
		internal OcclusionCullingCommon occlusionCullingCommon
		{
			get
			{
				return this.m_OcclusionCullingCommon;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x000132C7 File Offset: 0x000114C7
		internal DebugRendererBatcherStats debugStats
		{
			get
			{
				return this.m_DebugStats;
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000132D0 File Offset: 0x000114D0
		public RenderersBatchersContext(in RenderersBatchersContextDesc desc, GPUDrivenProcessor gpuDrivenProcessor, GPUResidentDrawerResources resources)
		{
			this.m_Resources = resources;
			this.m_GPUDrivenProcessor = gpuDrivenProcessor;
			RenderersParameters.Flags rendererParametersFlags = RenderersParameters.Flags.None;
			if (desc.enableBoundingSpheresInstanceData)
			{
				rendererParametersFlags |= RenderersParameters.Flags.UseBoundingSphereParameter;
			}
			this.m_InstanceDataBuffer = RenderersParameters.CreateInstanceDataBuffer(rendererParametersFlags, in desc.instanceNumInfo);
			this.m_RenderersParameters = new RenderersParameters(in this.m_InstanceDataBuffer);
			InstanceNumInfo instanceNumInfo = desc.instanceNumInfo;
			this.m_LODGroupDataPool = new LODGroupDataPool(resources, instanceNumInfo.GetInstanceNum(InstanceType.MeshRenderer), desc.supportDitheringCrossFade);
			this.m_UploadResources = default(GPUInstanceDataBufferUploader.GPUResources);
			this.m_UploadResources.LoadShaders(resources);
			this.m_GrowerResources = default(GPUInstanceDataBufferGrower.GPUResources);
			this.m_GrowerResources.LoadShaders(resources);
			this.m_CmdBuffer = new CommandBuffer();
			this.m_CmdBuffer.name = "GPUCullingCommands";
			this.m_CachedAmbientProbe = RenderSettings.ambientProbe;
			instanceNumInfo = desc.instanceNumInfo;
			this.m_InstanceDataSystem = new InstanceDataSystem(instanceNumInfo.GetTotalInstanceNum(), desc.enableBoundingSpheresInstanceData, resources);
			this.m_SmallMeshScreenPercentage = desc.smallMeshScreenPercentage;
			this.m_UpdateLODGroupCallback = new GPUDrivenLODGroupDataCallback(this.UpdateLODGroupData);
			this.m_TransformLODGroupCallback = new GPUDrivenLODGroupDataCallback(this.TransformLODGroupData);
			this.m_OcclusionCullingCommon = new OcclusionCullingCommon();
			this.m_OcclusionCullingCommon.Init(resources);
			this.m_DebugStats = (desc.enableCullerDebugStats ? new DebugRendererBatcherStats() : null);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00013418 File Offset: 0x00011618
		public void Dispose()
		{
			NativeArray<int>.ReadOnly rendererGroupIDs = this.m_InstanceDataSystem.sharedInstanceData.rendererGroupIDs;
			if (rendererGroupIDs.Length > 0)
			{
				this.m_GPUDrivenProcessor.DisableGPUDrivenRendering(in rendererGroupIDs);
			}
			this.m_InstanceDataSystem.Dispose();
			this.m_CmdBuffer.Release();
			this.m_GrowerResources.Dispose();
			this.m_UploadResources.Dispose();
			this.m_LODGroupDataPool.Dispose();
			this.m_InstanceDataBuffer.Dispose();
			this.m_UpdateLODGroupCallback = null;
			this.m_TransformLODGroupCallback = null;
			DebugRendererBatcherStats debugStats = this.m_DebugStats;
			if (debugStats != null)
			{
				debugStats.Dispose();
			}
			this.m_DebugStats = null;
			OcclusionCullingCommon occlusionCullingCommon = this.m_OcclusionCullingCommon;
			if (occlusionCullingCommon != null)
			{
				occlusionCullingCommon.Dispose();
			}
			this.m_OcclusionCullingCommon = null;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000134D2 File Offset: 0x000116D2
		public int GetMaxInstancesOfType(InstanceType instanceType)
		{
			return this.m_InstanceDataSystem.GetMaxInstancesOfType(instanceType);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000134E0 File Offset: 0x000116E0
		public int GetAliveInstancesOfType(InstanceType instanceType)
		{
			return this.m_InstanceDataSystem.GetAliveInstancesOfType(instanceType);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000134F0 File Offset: 0x000116F0
		public void GrowInstanceBuffer(in InstanceNumInfo instanceNumInfo)
		{
			using (GPUInstanceDataBufferGrower grower = new GPUInstanceDataBufferGrower(this.m_InstanceDataBuffer, in instanceNumInfo))
			{
				GPUInstanceDataBuffer newInstanceDataBuffer = grower.SubmitToGpu(ref this.m_GrowerResources);
				if (newInstanceDataBuffer != this.m_InstanceDataBuffer)
				{
					if (this.m_InstanceDataBuffer != null)
					{
						this.m_InstanceDataBuffer.Dispose();
					}
					this.m_InstanceDataBuffer = newInstanceDataBuffer;
				}
			}
			this.m_RenderersParameters = new RenderersParameters(in this.m_InstanceDataBuffer);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0001356C File Offset: 0x0001176C
		private void EnsureInstanceBufferCapacity()
		{
			int maxCPUMeshRendererNum = this.m_InstanceDataSystem.GetMaxInstancesOfType(InstanceType.MeshRenderer);
			int maxCPUSpeedTreeNum = this.m_InstanceDataSystem.GetMaxInstancesOfType(InstanceType.SpeedTree);
			int maxGPUMeshRendererInstances = this.m_InstanceDataBuffer.instanceNumInfo.GetInstanceNum(InstanceType.MeshRenderer);
			int maxGPUSpeedTreeInstances = this.m_InstanceDataBuffer.instanceNumInfo.GetInstanceNum(InstanceType.SpeedTree);
			bool needToGrow = false;
			if (maxCPUMeshRendererNum > maxGPUMeshRendererInstances)
			{
				needToGrow = true;
				maxGPUMeshRendererInstances = maxCPUMeshRendererNum + 1024;
			}
			if (maxCPUSpeedTreeNum > maxGPUSpeedTreeInstances)
			{
				needToGrow = true;
				maxGPUSpeedTreeInstances = maxCPUSpeedTreeNum + 256;
			}
			if (needToGrow)
			{
				InstanceNumInfo instanceNumInfo = new InstanceNumInfo(maxGPUMeshRendererInstances, maxGPUSpeedTreeInstances);
				this.GrowInstanceBuffer(in instanceNumInfo);
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000135ED File Offset: 0x000117ED
		private void UpdateLODGroupData(in GPUDrivenLODGroupData lodGroupData)
		{
			this.m_LODGroupDataPool.UpdateLODGroupData(in lodGroupData);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000135FB File Offset: 0x000117FB
		private void TransformLODGroupData(in GPUDrivenLODGroupData lodGroupData)
		{
			this.m_LODGroupDataPool.UpdateLODGroupTransformData(in lodGroupData);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00013609 File Offset: 0x00011809
		public void DestroyLODGroups(NativeArray<int> destroyed)
		{
			if (destroyed.Length == 0)
			{
				return;
			}
			this.m_LODGroupDataPool.FreeLODGroupData(destroyed);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00013621 File Offset: 0x00011821
		public void UpdateLODGroups(NativeArray<int> changedID)
		{
			if (changedID.Length == 0)
			{
				return;
			}
			this.m_GPUDrivenProcessor.DispatchLODGroupData(in changedID, this.m_UpdateLODGroupCallback);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00013645 File Offset: 0x00011845
		public void ReallocateAndGetInstances(in GPUDrivenRendererGroupData rendererData, NativeArray<InstanceHandle> instances)
		{
			this.m_InstanceDataSystem.ReallocateAndGetInstances(in rendererData, instances);
			this.EnsureInstanceBufferCapacity();
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0001365A File Offset: 0x0001185A
		public JobHandle ScheduleUpdateInstanceDataJob(NativeArray<InstanceHandle> instances, in GPUDrivenRendererGroupData rendererData)
		{
			return this.m_InstanceDataSystem.ScheduleUpdateInstanceDataJob(instances, in rendererData, this.m_LODGroupDataPool.lodGroupDataHash);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00013674 File Offset: 0x00011874
		public void FreeRendererGroupInstances(NativeArray<int> rendererGroupsID)
		{
			this.m_InstanceDataSystem.FreeRendererGroupInstances(rendererGroupsID);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00013682 File Offset: 0x00011882
		public void FreeInstances(NativeArray<InstanceHandle> instances)
		{
			this.m_InstanceDataSystem.FreeInstances(instances);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00013690 File Offset: 0x00011890
		public JobHandle ScheduleQueryRendererGroupInstancesJob(NativeArray<int> rendererGroupIDs, NativeArray<InstanceHandle> instances)
		{
			return this.m_InstanceDataSystem.ScheduleQueryRendererGroupInstancesJob(rendererGroupIDs, instances);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0001369F File Offset: 0x0001189F
		public JobHandle ScheduleQueryRendererGroupInstancesJob(NativeArray<int> rendererGroupIDs, NativeList<InstanceHandle> instances)
		{
			return this.m_InstanceDataSystem.ScheduleQueryRendererGroupInstancesJob(rendererGroupIDs, instances);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000136AE File Offset: 0x000118AE
		public JobHandle ScheduleQueryRendererGroupInstancesJob(NativeArray<int> rendererGroupIDs, NativeArray<int> instancesOffset, NativeArray<int> instancesCount, NativeList<InstanceHandle> instances)
		{
			return this.m_InstanceDataSystem.ScheduleQueryRendererGroupInstancesJob(rendererGroupIDs, instancesOffset, instancesCount, instances);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000136C0 File Offset: 0x000118C0
		public JobHandle ScheduleQueryMeshInstancesJob(NativeArray<int> sortedMeshIDs, NativeList<InstanceHandle> instances)
		{
			return this.m_InstanceDataSystem.ScheduleQuerySortedMeshInstancesJob(sortedMeshIDs, instances);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000136CF File Offset: 0x000118CF
		public void ChangeInstanceBufferVersion()
		{
			this.m_InstanceDataBuffer.version++;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000136E4 File Offset: 0x000118E4
		public GPUInstanceDataBufferUploader CreateDataBufferUploader(int capacity, InstanceType instanceType)
		{
			return new GPUInstanceDataBufferUploader(in this.m_InstanceDataBuffer.descriptions, capacity, instanceType);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000136F8 File Offset: 0x000118F8
		public void SubmitToGpu(NativeArray<InstanceHandle> instances, ref GPUInstanceDataBufferUploader uploader, bool submitOnlyWrittenParams)
		{
			uploader.SubmitToGpu(this.m_InstanceDataBuffer, instances, ref this.m_UploadResources, submitOnlyWrittenParams);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0001370E File Offset: 0x0001190E
		public void SubmitToGpu(NativeArray<GPUInstanceIndex> gpuInstanceIndices, ref GPUInstanceDataBufferUploader uploader, bool submitOnlyWrittenParams)
		{
			uploader.SubmitToGpu(this.m_InstanceDataBuffer, gpuInstanceIndices, ref this.m_UploadResources, submitOnlyWrittenParams);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00013724 File Offset: 0x00011924
		public void InitializeInstanceTransforms(NativeArray<InstanceHandle> instances, NativeArray<Matrix4x4> localToWorldMatrices, NativeArray<Matrix4x4> prevLocalToWorldMatrices)
		{
			if (instances.Length == 0)
			{
				return;
			}
			this.m_InstanceDataSystem.InitializeInstanceTransforms(instances, localToWorldMatrices, prevLocalToWorldMatrices, in this.m_RenderersParameters, this.m_InstanceDataBuffer);
			this.ChangeInstanceBufferVersion();
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00013750 File Offset: 0x00011950
		public void UpdateInstanceTransforms(NativeArray<InstanceHandle> instances, NativeArray<Matrix4x4> localToWorldMatrices)
		{
			if (instances.Length == 0)
			{
				return;
			}
			this.m_InstanceDataSystem.UpdateInstanceTransforms(instances, localToWorldMatrices, in this.m_RenderersParameters, this.m_InstanceDataBuffer);
			this.ChangeInstanceBufferVersion();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001377B File Offset: 0x0001197B
		public void UpdateAmbientProbeAndGpuBuffer(bool forceUpdate)
		{
			if (forceUpdate || this.m_CachedAmbientProbe != RenderSettings.ambientProbe)
			{
				this.m_CachedAmbientProbe = RenderSettings.ambientProbe;
				this.m_InstanceDataSystem.UpdateAllInstanceProbes(in this.m_RenderersParameters, this.m_InstanceDataBuffer);
				this.ChangeInstanceBufferVersion();
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000137BA File Offset: 0x000119BA
		public void UpdateInstanceWindDataHistory(NativeArray<GPUInstanceIndex> gpuInstanceIndices)
		{
			if (gpuInstanceIndices.Length == 0)
			{
				return;
			}
			this.m_InstanceDataSystem.UpdateInstanceWindDataHistory(gpuInstanceIndices, this.m_RenderersParameters, this.m_InstanceDataBuffer);
			this.ChangeInstanceBufferVersion();
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000137E4 File Offset: 0x000119E4
		public void UpdateInstanceMotions()
		{
			this.m_InstanceDataSystem.UpdateInstanceMotions(in this.m_RenderersParameters, this.m_InstanceDataBuffer);
			this.ChangeInstanceBufferVersion();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00013803 File Offset: 0x00011A03
		public void TransformLODGroups(NativeArray<int> lodGroupsID)
		{
			if (lodGroupsID.Length == 0)
			{
				return;
			}
			this.m_GPUDrivenProcessor.DispatchLODGroupData(in lodGroupsID, this.m_TransformLODGroupCallback);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00013827 File Offset: 0x00011A27
		public void UpdatePerFrameInstanceVisibility(in ParallelBitArray compactedVisibilityMasks)
		{
			this.m_InstanceDataSystem.UpdatePerFrameInstanceVisibility(in compactedVisibilityMasks);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00013835 File Offset: 0x00011A35
		public JobHandle ScheduleCollectInstancesLODGroupAndMasksJob(NativeArray<InstanceHandle> instances, NativeArray<uint> lodGroupAndMasks)
		{
			return this.m_InstanceDataSystem.ScheduleCollectInstancesLODGroupAndMasksJob(instances, lodGroupAndMasks);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00013844 File Offset: 0x00011A44
		public InstanceHandle GetRendererInstanceHandle(int rendererID)
		{
			NativeArray<int> rendererIDs = new NativeArray<int>(1, Allocator.TempJob, NativeArrayOptions.ClearMemory);
			NativeArray<InstanceHandle> instances = new NativeArray<InstanceHandle>(1, Allocator.TempJob, NativeArrayOptions.ClearMemory);
			rendererIDs[0] = rendererID;
			this.m_InstanceDataSystem.ScheduleQueryRendererGroupInstancesJob(rendererIDs, instances).Complete();
			InstanceHandle instanceHandle = instances[0];
			rendererIDs.Dispose();
			instances.Dispose();
			return instanceHandle;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00013899 File Offset: 0x00011A99
		public void GetVisibleTreeInstances(in ParallelBitArray compactedVisibilityMasks, in ParallelBitArray processedBits, NativeList<int> visibeTreeRendererIDs, NativeList<InstanceHandle> visibeTreeInstances, bool becomeVisibleOnly, out int becomeVisibeTreeInstancesCount)
		{
			this.m_InstanceDataSystem.GetVisibleTreeInstances(in compactedVisibilityMasks, in processedBits, visibeTreeRendererIDs, visibeTreeInstances, becomeVisibleOnly, out becomeVisibeTreeInstancesCount);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000138AF File Offset: 0x00011AAF
		public GPUInstanceDataBuffer GetInstanceDataBuffer()
		{
			return this.m_InstanceDataBuffer;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000138B7 File Offset: 0x00011AB7
		public void UpdateFrame()
		{
			this.m_OcclusionCullingCommon.UpdateFrame();
			if (this.m_DebugStats != null)
			{
				this.m_OcclusionCullingCommon.UpdateOccluderStats(this.m_DebugStats);
			}
		}

		// Token: 0x040003F7 RID: 1015
		private InstanceDataSystem m_InstanceDataSystem;

		// Token: 0x040003F8 RID: 1016
		private GPUResidentDrawerResources m_Resources;

		// Token: 0x040003F9 RID: 1017
		private GPUDrivenProcessor m_GPUDrivenProcessor;

		// Token: 0x040003FA RID: 1018
		private LODGroupDataPool m_LODGroupDataPool;

		// Token: 0x040003FB RID: 1019
		internal GPUInstanceDataBuffer m_InstanceDataBuffer;

		// Token: 0x040003FC RID: 1020
		private RenderersParameters m_RenderersParameters;

		// Token: 0x040003FD RID: 1021
		private GPUInstanceDataBufferUploader.GPUResources m_UploadResources;

		// Token: 0x040003FE RID: 1022
		private GPUInstanceDataBufferGrower.GPUResources m_GrowerResources;

		// Token: 0x040003FF RID: 1023
		internal CommandBuffer m_CmdBuffer;

		// Token: 0x04000400 RID: 1024
		private SphericalHarmonicsL2 m_CachedAmbientProbe;

		// Token: 0x04000401 RID: 1025
		private float m_SmallMeshScreenPercentage;

		// Token: 0x04000402 RID: 1026
		private GPUDrivenLODGroupDataCallback m_UpdateLODGroupCallback;

		// Token: 0x04000403 RID: 1027
		private GPUDrivenLODGroupDataCallback m_TransformLODGroupCallback;

		// Token: 0x04000404 RID: 1028
		private OcclusionCullingCommon m_OcclusionCullingCommon;

		// Token: 0x04000405 RID: 1029
		private DebugRendererBatcherStats m_DebugStats;
	}
}
