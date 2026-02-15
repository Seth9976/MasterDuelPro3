using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x020000AB RID: 171
	internal class LODGroupDataPool : IDisposable
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060002AA RID: 682 RVA: 0x000119C4 File Offset: 0x0000FBC4
		public NativeParallelHashMap<int, GPUInstanceIndex> lodGroupDataHash
		{
			get
			{
				return this.m_LODGroupDataHash;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000119CC File Offset: 0x0000FBCC
		public NativeList<LODGroupCullingData> lodGroupCullingData
		{
			get
			{
				return this.m_LODGroupCullingData;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000119D4 File Offset: 0x0000FBD4
		public int crossfadedRendererCount
		{
			get
			{
				return this.m_CrossfadedRendererCount;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060002AD RID: 685 RVA: 0x000119DC File Offset: 0x0000FBDC
		public int activeLodGroupCount
		{
			get
			{
				return this.m_LODGroupData.Length;
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000119EC File Offset: 0x0000FBEC
		public LODGroupDataPool(GPUResidentDrawerResources resources, int initialInstanceCount, bool supportDitheringCrossFade)
		{
			this.m_LODGroupData = new NativeList<LODGroupData>(Allocator.Persistent);
			this.m_LODGroupDataHash = new NativeParallelHashMap<int, GPUInstanceIndex>(64, Allocator.Persistent);
			this.m_LODGroupCullingData = new NativeList<LODGroupCullingData>(Allocator.Persistent);
			this.m_FreeLODGroupDataHandles = new NativeList<GPUInstanceIndex>(Allocator.Persistent);
			this.m_SupportDitheringCrossFade = supportDitheringCrossFade;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00011A4C File Offset: 0x0000FC4C
		public void Dispose()
		{
			this.m_LODGroupData.Dispose();
			this.m_LODGroupDataHash.Dispose();
			this.m_LODGroupCullingData.Dispose();
			this.m_FreeLODGroupDataHandles.Dispose();
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00011A7C File Offset: 0x0000FC7C
		public unsafe void UpdateLODGroupTransformData(in GPUDrivenLODGroupData inputData)
		{
			NativeArray<int> lodGroupID = inputData.lodGroupID;
			int lodGroupCount = lodGroupID.Length;
			int updateCount = 0;
			UpdateLODGroupTransformJob jobData = new UpdateLODGroupTransformJob
			{
				lodGroupDataHash = this.m_LODGroupDataHash,
				lodGroupIDs = inputData.lodGroupID,
				worldSpaceReferencePoints = inputData.worldSpaceReferencePoint,
				worldSpaceSizes = inputData.worldSpaceSize,
				lodGroupData = this.m_LODGroupData,
				lodGroupCullingData = this.m_LODGroupCullingData,
				supportDitheringCrossFade = this.m_SupportDitheringCrossFade,
				atomicUpdateCount = new UnsafeAtomicCounter32((void*)(&updateCount))
			};
			if (lodGroupCount >= 256)
			{
				jobData.Schedule(lodGroupCount, 256, default(JobHandle)).Complete();
				return;
			}
			jobData.Run(lodGroupCount);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00011B40 File Offset: 0x0000FD40
		public unsafe void UpdateLODGroupData(in GPUDrivenLODGroupData inputData)
		{
			this.FreeLODGroupData(inputData.invalidLODGroupID);
			NativeArray<int> lodGroupID = inputData.lodGroupID;
			NativeArray<GPUInstanceIndex> lodGroupInstances = new NativeArray<GPUInstanceIndex>(lodGroupID.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			int previousRendererCount = 0;
			new AllocateOrGetLODGroupDataInstancesJob
			{
				lodGroupsID = inputData.lodGroupID,
				lodGroupsData = this.m_LODGroupData,
				lodGroupCullingData = this.m_LODGroupCullingData,
				lodGroupDataHash = this.m_LODGroupDataHash,
				freeLODGroupDataHandles = this.m_FreeLODGroupDataHandles,
				lodGroupInstances = lodGroupInstances,
				previousRendererCount = &previousRendererCount
			}.Run<AllocateOrGetLODGroupDataInstancesJob>();
			this.m_CrossfadedRendererCount -= previousRendererCount;
			int rendererCount = 0;
			UpdateLODGroupDataJob updateLODGroupDataJobData = new UpdateLODGroupDataJob
			{
				lodGroupInstances = lodGroupInstances,
				inputData = inputData,
				supportDitheringCrossFade = this.m_SupportDitheringCrossFade,
				lodGroupsData = this.m_LODGroupData.AsArray(),
				lodGroupsCullingData = this.m_LODGroupCullingData.AsArray(),
				rendererCount = new UnsafeAtomicCounter32((void*)(&rendererCount))
			};
			if (lodGroupInstances.Length >= 256)
			{
				updateLODGroupDataJobData.Schedule(lodGroupInstances.Length, 256, default(JobHandle)).Complete();
			}
			else
			{
				updateLODGroupDataJobData.Run(lodGroupInstances.Length);
			}
			this.m_CrossfadedRendererCount += rendererCount;
			lodGroupInstances.Dispose();
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00011C9C File Offset: 0x0000FE9C
		public unsafe void FreeLODGroupData(NativeArray<int> destroyedLODGroupsID)
		{
			if (destroyedLODGroupsID.Length == 0)
			{
				return;
			}
			int removedRendererCount = 0;
			new FreeLODGroupDataJob
			{
				destroyedLODGroupsID = destroyedLODGroupsID,
				lodGroupsData = this.m_LODGroupData,
				lodGroupDataHash = this.m_LODGroupDataHash,
				freeLODGroupDataHandles = this.m_FreeLODGroupDataHandles,
				removedRendererCount = &removedRendererCount
			}.Run<FreeLODGroupDataJob>();
			this.m_CrossfadedRendererCount -= removedRendererCount;
		}

		// Token: 0x0400037B RID: 891
		private NativeList<LODGroupData> m_LODGroupData;

		// Token: 0x0400037C RID: 892
		private NativeParallelHashMap<int, GPUInstanceIndex> m_LODGroupDataHash;

		// Token: 0x0400037D RID: 893
		private NativeList<LODGroupCullingData> m_LODGroupCullingData;

		// Token: 0x0400037E RID: 894
		private NativeList<GPUInstanceIndex> m_FreeLODGroupDataHandles;

		// Token: 0x0400037F RID: 895
		private int m_CrossfadedRendererCount;

		// Token: 0x04000380 RID: 896
		private bool m_SupportDitheringCrossFade;

		// Token: 0x020000AC RID: 172
		private static class LodGroupShaderIDs
		{
			// Token: 0x04000381 RID: 897
			public static readonly int _SupportDitheringCrossFade = Shader.PropertyToID("_SupportDitheringCrossFade");

			// Token: 0x04000382 RID: 898
			public static readonly int _LodGroupCullingDataGPUByteSize = Shader.PropertyToID("_LodGroupCullingDataGPUByteSize");

			// Token: 0x04000383 RID: 899
			public static readonly int _LodGroupCullingDataStartOffset = Shader.PropertyToID("_LodGroupCullingDataStartOffset");

			// Token: 0x04000384 RID: 900
			public static readonly int _LodCullingDataQueueCount = Shader.PropertyToID("_LodCullingDataQueueCount");

			// Token: 0x04000385 RID: 901
			public static readonly int _InputLodCullingDataIndices = Shader.PropertyToID("_InputLodCullingDataIndices");

			// Token: 0x04000386 RID: 902
			public static readonly int _InputLodCullingDataBuffer = Shader.PropertyToID("_InputLodCullingDataBuffer");

			// Token: 0x04000387 RID: 903
			public static readonly int _LodGroupCullingData = Shader.PropertyToID("_LodGroupCullingData");
		}
	}
}
