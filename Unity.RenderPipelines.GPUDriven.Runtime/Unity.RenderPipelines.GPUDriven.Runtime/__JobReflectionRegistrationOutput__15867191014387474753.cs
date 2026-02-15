using System;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020000DA RID: 218
[DOTSCompilerGenerated]
internal class __JobReflectionRegistrationOutput__15867191014387474753
{
	// Token: 0x0600033B RID: 827 RVA: 0x000144B0 File Offset: 0x000126B0
	public static void CreateJobReflectionData()
	{
		try
		{
			IJobExtensions.EarlyJobInit<GPUResidentDrawer.FindUnsupportedMaterialsJob>();
			IJobExtensions.EarlyJobInit<GPUResidentDrawer.FindUnsupportedRenderersJob>();
			IJobParallelForExtensions.EarlyJobInit<CullingJob>();
			IJobParallelForExtensions.EarlyJobInit<AllocateBinsPerBatch>();
			IJobExtensions.EarlyJobInit<PrefixSumDrawsAndInstances>();
			IJobParallelForExtensions.EarlyJobInit<DrawCommandOutputPerBatch>();
			IJobParallelForBatchExtensions.EarlyJobInit<CompactVisibilityMasksJob>();
			IJobExtensions.EarlyJobInit<InstanceCuller.SetupCullingJobInput>();
			IJobExtensions.EarlyJobInit<PrefixSumDrawInstancesJob>();
			IJobParallelForExtensions.EarlyJobInit<BuildDrawListsJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<FindDrawInstancesJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<FindMaterialDrawInstancesJob>();
			IJobExtensions.EarlyJobInit<RemoveDrawInstanceIndicesJob>();
			IJobExtensions.EarlyJobInit<CreateDrawBatchesJob>();
			IJobParallelForExtensions.EarlyJobInit<GPUInstanceDataBuffer.ConvertCPUInstancesToGPUInstancesJob>();
			IJobParallelForExtensions.EarlyJobInit<GPUInstanceDataBufferUploader.WriteInstanceDataParameterJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<InstanceDataSystem.QueryRendererGroupInstancesCountJob>();
			IJobExtensions.EarlyJobInit<InstanceDataSystem.ComputeInstancesOffsetAndResizeInstancesArrayJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<InstanceDataSystem.QueryRendererGroupInstancesJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<InstanceDataSystem.QueryRendererGroupInstancesMultiJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<InstanceDataSystem.QuerySortedMeshInstancesJob>();
			IJobParallelForExtensions.EarlyJobInit<InstanceDataSystem.CalculateInterpolatedLightAndOcclusionProbesBatchJob>();
			IJobParallelForExtensions.EarlyJobInit<InstanceDataSystem.ScatterTetrahedronCacheIndicesJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<InstanceDataSystem.TransformUpdateJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<InstanceDataSystem.ProbesUpdateJob>();
			IJobParallelForExtensions.EarlyJobInit<InstanceDataSystem.MotionUpdateJob>();
			IJobExtensions.EarlyJobInit<InstanceDataSystem.ReallocateInstancesJob>();
			IJobExtensions.EarlyJobInit<InstanceDataSystem.FreeInstancesJob>();
			IJobExtensions.EarlyJobInit<InstanceDataSystem.FreeRendererGroupInstancesJob>();
			IJobParallelForExtensions.EarlyJobInit<InstanceDataSystem.UpdateRendererInstancesJob>();
			IJobParallelForExtensions.EarlyJobInit<InstanceDataSystem.CollectInstancesLODGroupsAndMasksJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<InstanceDataSystem.GetVisibleNonProcessedTreeInstancesJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<InstanceDataSystem.UpdateCompactedInstanceVisibilityJob>();
			IJobParallelForExtensions.EarlyJobInit<UpdateLODGroupTransformJob>();
			IJobExtensions.EarlyJobInit<AllocateOrGetLODGroupDataInstancesJob>();
			IJobParallelForExtensions.EarlyJobInit<UpdateLODGroupDataJob>();
			IJobExtensions.EarlyJobInit<FreeLODGroupDataJob>();
			IJobForExtensions.EarlyJobInit<ParallelSortExtensions.RadixSortBucketCountJob>();
			IJobForExtensions.EarlyJobInit<ParallelSortExtensions.RadixSortBatchPrefixSumJob>();
			IJobForExtensions.EarlyJobInit<ParallelSortExtensions.RadixSortPrefixSumJob>();
			IJobForExtensions.EarlyJobInit<ParallelSortExtensions.RadixSortBucketSortJob>();
			IJobParallelForExtensions.EarlyJobInit<RegisterNewInstancesJob<BatchMeshID>>();
			IJobParallelForExtensions.EarlyJobInit<RegisterNewInstancesJob<BatchMaterialID>>();
			IJobParallelForBatchExtensions.EarlyJobInit<FindNonRegisteredInstancesJob<BatchMeshID>>();
			IJobParallelForBatchExtensions.EarlyJobInit<FindNonRegisteredInstancesJob<BatchMaterialID>>();
		}
		catch (Exception ex)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex);
		}
	}

	// Token: 0x0600033C RID: 828 RVA: 0x000145C0 File Offset: 0x000127C0
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	public static void EarlyInit()
	{
		__JobReflectionRegistrationOutput__15867191014387474753.CreateJobReflectionData();
	}
}
