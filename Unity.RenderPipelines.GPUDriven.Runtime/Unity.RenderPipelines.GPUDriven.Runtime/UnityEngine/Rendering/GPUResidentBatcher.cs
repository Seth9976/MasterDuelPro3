using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x0200001C RID: 28
	internal class GPUResidentBatcher : IDisposable
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00004548 File Offset: 0x00002748
		private void ProcessTrees()
		{
			if (this.m_BatchersContext.GetAliveInstancesOfType(InstanceType.SpeedTree) == 0)
			{
				return;
			}
			ParallelBitArray compactedVisibilityMasks = this.m_InstanceCullingBatcher.GetCompactedVisibilityMasks(false);
			if (!compactedVisibilityMasks.IsCreated)
			{
				return;
			}
			int maxInstancesCount = this.m_BatchersContext.aliveInstances.Length;
			if (!this.m_ProcessedThisFrameTreeBits.IsCreated)
			{
				this.m_ProcessedThisFrameTreeBits = new ParallelBitArray(maxInstancesCount, Allocator.TempJob, NativeArrayOptions.ClearMemory);
			}
			else if (this.m_ProcessedThisFrameTreeBits.Length < maxInstancesCount)
			{
				this.m_ProcessedThisFrameTreeBits.Resize(maxInstancesCount);
			}
			bool becomeVisibleOnly = !Application.isPlaying;
			NativeList<int> visibleTreeRendererIDs = new NativeList<int>(Allocator.TempJob);
			NativeList<InstanceHandle> visibleTreeInstances = new NativeList<InstanceHandle>(Allocator.TempJob);
			int becomeVisibleTreeInstancesCount;
			this.m_BatchersContext.GetVisibleTreeInstances(in compactedVisibilityMasks, in this.m_ProcessedThisFrameTreeBits, visibleTreeRendererIDs, visibleTreeInstances, becomeVisibleOnly, out becomeVisibleTreeInstancesCount);
			if (visibleTreeRendererIDs.Length > 0)
			{
				NativeArray<int> becomeVisibleTreeRendererIDs = visibleTreeRendererIDs.AsArray().GetSubArray(0, becomeVisibleTreeInstancesCount);
				NativeArray<InstanceHandle> becomeVisibleTreeInstances = visibleTreeInstances.AsArray().GetSubArray(0, becomeVisibleTreeInstancesCount);
				if (becomeVisibleTreeRendererIDs.Length > 0)
				{
					this.UpdateSpeedTreeWindAndUploadWindParamsToGPU(becomeVisibleTreeRendererIDs, becomeVisibleTreeInstances, true);
				}
				this.UpdateSpeedTreeWindAndUploadWindParamsToGPU(visibleTreeRendererIDs.AsArray(), visibleTreeInstances.AsArray(), false);
			}
			visibleTreeRendererIDs.Dispose();
			visibleTreeInstances.Dispose();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004670 File Offset: 0x00002870
		private unsafe void UpdateSpeedTreeWindAndUploadWindParamsToGPU(NativeArray<int> treeRendererIDs, NativeArray<InstanceHandle> treeInstances, bool history)
		{
			if (treeRendererIDs.Length == 0)
			{
				return;
			}
			NativeArray<GPUInstanceIndex> gpuInstanceIndices = new NativeArray<GPUInstanceIndex>(treeInstances.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			this.m_BatchersContext.instanceDataBuffer.CPUInstanceArrayToGPUInstanceArray(treeInstances, gpuInstanceIndices);
			if (!history)
			{
				this.m_BatchersContext.UpdateInstanceWindDataHistory(gpuInstanceIndices);
			}
			GPUInstanceDataBufferUploader uploader = this.m_BatchersContext.CreateDataBufferUploader(treeInstances.Length, InstanceType.SpeedTree);
			uploader.AllocateUploadHandles(treeInstances.Length);
			SpeedTreeWindParamsBufferIterator windParams = default(SpeedTreeWindParamsBufferIterator);
			windParams.bufferPtr = uploader.GetUploadBufferPtr();
			for (int i = 0; i < 16; i++)
			{
				*((ref windParams.uintParamOffsets.FixedElementField) + (IntPtr)i * 4) = uploader.PrepareParamWrite<Vector4>(this.m_BatchersContext.renderersParameters.windParams[i].index);
			}
			windParams.uintStride = uploader.GetUIntPerInstance();
			windParams.elementOffset = 0;
			windParams.elementsCount = treeInstances.Length;
			SpeedTreeWindManager.UpdateWindAndWriteBufferWindParams(in treeRendererIDs, windParams, history);
			this.m_BatchersContext.SubmitToGpu(gpuInstanceIndices, ref uploader, true);
			gpuInstanceIndices.Dispose();
			uploader.Dispose();
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004789 File Offset: 0x00002989
		internal RenderersBatchersContext batchersContext
		{
			get
			{
				return this.m_BatchersContext;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004791 File Offset: 0x00002991
		internal OcclusionCullingCommon occlusionCullingCommon
		{
			get
			{
				return this.m_BatchersContext.occlusionCullingCommon;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000479E File Offset: 0x0000299E
		internal InstanceCullingBatcher instanceCullingBatcher
		{
			get
			{
				return this.m_InstanceCullingBatcher;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000047A8 File Offset: 0x000029A8
		public GPUResidentBatcher(RenderersBatchersContext batcherContext, InstanceCullingBatcherDesc instanceCullerBatcherDesc, GPUDrivenProcessor gpuDrivenProcessor)
		{
			this.m_BatchersContext = batcherContext;
			this.m_GPUDrivenProcessor = gpuDrivenProcessor;
			this.m_UpdateRendererDataCallback = new GPUDrivenRendererDataCallback(this.UpdateRendererData);
			this.m_InstanceCullingBatcher = new InstanceCullingBatcher(batcherContext, instanceCullerBatcherDesc, new BatchRendererGroup.OnFinishedCulling(this.OnFinishedCulling));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000047F4 File Offset: 0x000029F4
		public void Dispose()
		{
			this.m_GPUDrivenProcessor.ClearMaterialFilters();
			this.m_InstanceCullingBatcher.Dispose();
			if (this.m_ProcessedThisFrameTreeBits.IsCreated)
			{
				this.m_ProcessedThisFrameTreeBits.Dispose();
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004824 File Offset: 0x00002A24
		public void OnBeginContextRendering()
		{
			if (this.m_ProcessedThisFrameTreeBits.IsCreated)
			{
				this.m_ProcessedThisFrameTreeBits.Dispose();
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000483E File Offset: 0x00002A3E
		public void OnEndContextRendering()
		{
			InstanceCullingBatcher instanceCullingBatcher = this.m_InstanceCullingBatcher;
			if (instanceCullingBatcher == null)
			{
				return;
			}
			instanceCullingBatcher.OnEndContextRendering();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004850 File Offset: 0x00002A50
		public void OnBeginCameraRendering(Camera camera)
		{
			InstanceCullingBatcher instanceCullingBatcher = this.m_InstanceCullingBatcher;
			if (instanceCullingBatcher == null)
			{
				return;
			}
			instanceCullingBatcher.OnBeginCameraRendering(camera);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004863 File Offset: 0x00002A63
		public void OnEndCameraRendering(Camera camera)
		{
			InstanceCullingBatcher instanceCullingBatcher = this.m_InstanceCullingBatcher;
			if (instanceCullingBatcher == null)
			{
				return;
			}
			instanceCullingBatcher.OnEndCameraRendering(camera);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004876 File Offset: 0x00002A76
		public void UpdateFrame()
		{
			this.m_InstanceCullingBatcher.UpdateFrame();
			this.m_BatchersContext.UpdateFrame();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000488E File Offset: 0x00002A8E
		public void DestroyMaterials(NativeArray<int> destroyedMaterials)
		{
			this.m_InstanceCullingBatcher.DestroyMaterials(destroyedMaterials);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000489C File Offset: 0x00002A9C
		public void DestroyInstances(NativeArray<InstanceHandle> instances)
		{
			this.m_InstanceCullingBatcher.DestroyInstances(instances);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000048AA File Offset: 0x00002AAA
		public void DestroyMeshes(NativeArray<int> destroyedMeshes)
		{
			this.m_InstanceCullingBatcher.DestroyMeshes(destroyedMeshes);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000048B8 File Offset: 0x00002AB8
		internal void FreeRendererGroupInstances(NativeArray<int> rendererGroupIDs)
		{
			if (rendererGroupIDs.Length == 0)
			{
				return;
			}
			NativeList<InstanceHandle> instances = new NativeList<InstanceHandle>(rendererGroupIDs.Length, Allocator.TempJob);
			this.m_BatchersContext.ScheduleQueryRendererGroupInstancesJob(rendererGroupIDs, instances).Complete();
			this.DestroyInstances(instances.AsArray());
			instances.Dispose();
			this.m_BatchersContext.FreeRendererGroupInstances(rendererGroupIDs);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004918 File Offset: 0x00002B18
		public void InstanceOcclusionTest(RenderGraph renderGraph, in OcclusionCullingSettings settings, ReadOnlySpan<SubviewOcclusionTest> subviewOcclusionTests)
		{
			if (!this.m_BatchersContext.hasBoundingSpheres)
			{
				return;
			}
			this.m_InstanceCullingBatcher.culler.InstanceOcclusionTest(renderGraph, in settings, subviewOcclusionTests, this.m_BatchersContext);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004941 File Offset: 0x00002B41
		public void UpdateInstanceOccluders(RenderGraph renderGraph, in OccluderParameters occluderParams, ReadOnlySpan<OccluderSubviewUpdate> occluderSubviewUpdates)
		{
			if (!this.m_BatchersContext.hasBoundingSpheres)
			{
				return;
			}
			this.m_BatchersContext.occlusionCullingCommon.UpdateInstanceOccluders(renderGraph, in occluderParams, occluderSubviewUpdates);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004965 File Offset: 0x00002B65
		public void UpdateRenderers(NativeArray<int> renderersID)
		{
			if (renderersID.Length == 0)
			{
				return;
			}
			this.m_GPUDrivenProcessor.enablePartialRendering = false;
			this.m_GPUDrivenProcessor.EnableGPUDrivenRenderingAndDispatchRendererData(in renderersID, this.m_UpdateRendererDataCallback);
			this.m_GPUDrivenProcessor.enablePartialRendering = false;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000049A1 File Offset: 0x00002BA1
		public void PostCullBeginCameraRendering(RenderRequestBatcherContext context)
		{
			this.m_InstanceCullingBatcher.PostCullBeginCameraRendering(context);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000049AF File Offset: 0x00002BAF
		public void OnSetupAmbientProbe()
		{
			this.m_BatchersContext.UpdateAmbientProbeAndGpuBuffer(false);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000049C0 File Offset: 0x00002BC0
		private void UpdateRendererData(in GPUDrivenRendererGroupData rendererData, IList<Mesh> meshes, IList<Material> materials)
		{
			this.FreeRendererGroupInstances(rendererData.invalidRendererGroupID);
			NativeArray<int> rendererGroupID = rendererData.rendererGroupID;
			if (rendererGroupID.Length == 0)
			{
				return;
			}
			NativeArray<Matrix4x4> localToWorldMatrix = rendererData.localToWorldMatrix;
			NativeArray<InstanceHandle> instances = new NativeArray<InstanceHandle>(localToWorldMatrix.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			this.m_BatchersContext.ReallocateAndGetInstances(in rendererData, instances);
			JobHandle updateInstanceDataJob = this.m_BatchersContext.ScheduleUpdateInstanceDataJob(instances, in rendererData);
			GPUInstanceDataBufferUploader uploader = this.m_BatchersContext.CreateDataBufferUploader(instances.Length, InstanceType.MeshRenderer);
			uploader.AllocateUploadHandles(instances.Length);
			JobHandle writeJobHandle = default(JobHandle);
			uploader.WriteInstanceDataJob<Vector4>(this.m_BatchersContext.renderersParameters.lightmapScale.index, rendererData.lightmapScaleOffset, rendererData.rendererGroupIndex).Complete();
			this.m_BatchersContext.SubmitToGpu(instances, ref uploader, true);
			this.m_BatchersContext.ChangeInstanceBufferVersion();
			uploader.Dispose();
			updateInstanceDataJob.Complete();
			this.m_BatchersContext.InitializeInstanceTransforms(instances, rendererData.localToWorldMatrix, rendererData.prevLocalToWorldMatrix);
			this.m_InstanceCullingBatcher.BuildBatch(instances, rendererData.materialID, rendererData.meshID, in rendererData);
			instances.Dispose();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004AD8 File Offset: 0x00002CD8
		private void OnFinishedCulling(IntPtr customCullingResult)
		{
			this.ProcessTrees();
			this.m_InstanceCullingBatcher.OnFinishedCulling(customCullingResult);
		}

		// Token: 0x04000046 RID: 70
		private ParallelBitArray m_ProcessedThisFrameTreeBits;

		// Token: 0x04000047 RID: 71
		private RenderersBatchersContext m_BatchersContext;

		// Token: 0x04000048 RID: 72
		private GPUDrivenProcessor m_GPUDrivenProcessor;

		// Token: 0x04000049 RID: 73
		private GPUDrivenRendererDataCallback m_UpdateRendererDataCallback;

		// Token: 0x0400004A RID: 74
		private InstanceCullingBatcher m_InstanceCullingBatcher;
	}
}
