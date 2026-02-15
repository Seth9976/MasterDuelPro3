using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x02000056 RID: 86
	internal class InstanceCullingBatcher : IDisposable
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000097A9 File Offset: 0x000079A9
		public NativeParallelHashMap<int, BatchMaterialID> batchMaterialHash
		{
			get
			{
				return this.m_BatchMaterialHash;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000097B4 File Offset: 0x000079B4
		public InstanceCullingBatcher(RenderersBatchersContext batcherContext, InstanceCullingBatcherDesc desc, BatchRendererGroup.OnFinishedCulling onFinishedCulling)
		{
			this.m_BatchersContext = batcherContext;
			this.m_DrawInstanceData = new CPUDrawInstanceData();
			this.m_DrawInstanceData.Initialize();
			this.m_BRG = new BatchRendererGroup(new BatchRendererGroupCreateInfo
			{
				cullingCallback = new BatchRendererGroup.OnPerformCulling(this.OnPerformCulling),
				finishedCullingCallback = onFinishedCulling,
				userContext = IntPtr.Zero
			});
			this.m_Culler = default(InstanceCuller);
			this.m_Culler.Init(batcherContext.resources, batcherContext.debugStats);
			this.m_CachedInstanceDataBufferLayoutVersion = -1;
			this.m_OnCompleteCallback = desc.onCompleteCallback;
			this.m_BatchMaterialHash = new NativeParallelHashMap<int, BatchMaterialID>(64, Allocator.Persistent);
			this.m_BatchMeshHash = new NativeParallelHashMap<int, BatchMeshID>(64, Allocator.Persistent);
			this.m_GlobalBatchIDs = new NativeParallelHashMap<uint, BatchID>(6, Allocator.Persistent);
			this.m_GlobalBatchIDs.Add(1U, this.GetBatchID(InstanceComponentGroup.Default));
			this.m_GlobalBatchIDs.Add(3U, this.GetBatchID(InstanceComponentGroup.DefaultWind));
			this.m_GlobalBatchIDs.Add(5U, this.GetBatchID(InstanceComponentGroup.DefaultLightProbe));
			this.m_GlobalBatchIDs.Add(9U, this.GetBatchID(InstanceComponentGroup.DefaultLightmap));
			this.m_GlobalBatchIDs.Add(7U, this.GetBatchID(InstanceComponentGroup.DefaultWindLightProbe));
			this.m_GlobalBatchIDs.Add(11U, this.GetBatchID(InstanceComponentGroup.DefaultWindLightmap));
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00009903 File Offset: 0x00007B03
		internal ref InstanceCuller culler
		{
			get
			{
				return ref this.m_Culler;
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000990C File Offset: 0x00007B0C
		public unsafe void Dispose()
		{
			this.m_OnCompleteCallback = null;
			this.m_Culler.Dispose();
			foreach (KeyValue<uint, BatchID> batchID in this.m_GlobalBatchIDs)
			{
				if (!batchID.Value.Equals(BatchID.Null))
				{
					this.m_BRG.RemoveBatch(*batchID.Value);
				}
			}
			this.m_GlobalBatchIDs.Dispose();
			if (this.m_BRG != null)
			{
				this.m_BRG.Dispose();
			}
			this.m_DrawInstanceData.Dispose();
			this.m_DrawInstanceData = null;
			this.m_BatchMaterialHash.Dispose();
			this.m_BatchMeshHash.Dispose();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000099DC File Offset: 0x00007BDC
		private BatchID GetBatchID(InstanceComponentGroup componentsOverriden)
		{
			if (this.m_CachedInstanceDataBufferLayoutVersion != this.m_BatchersContext.instanceDataBufferLayoutVersion)
			{
				return BatchID.Null;
			}
			NativeList<MetadataValue> tempMetadata = new NativeList<MetadataValue>(this.m_BatchersContext.defaultMetadata.Length, Allocator.Temp);
			for (int i = 0; i < this.m_BatchersContext.defaultDescriptions.Length; i++)
			{
				InstanceComponentGroup componentGroup = this.m_BatchersContext.defaultDescriptions[i].componentGroup;
				MetadataValue metadata = this.m_BatchersContext.defaultMetadata[i];
				uint value = metadata.Value;
				if ((componentsOverriden & componentGroup) == (InstanceComponentGroup)0U)
				{
					value &= 1342177279U;
				}
				MetadataValue metadataValue = default(MetadataValue);
				metadataValue.NameID = metadata.NameID;
				metadataValue.Value = value;
				tempMetadata.Add(in metadataValue);
			}
			return this.m_BRG.AddBatch(tempMetadata.AsArray(), this.m_BatchersContext.gpuInstanceDataBuffer.bufferHandle);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00009AD8 File Offset: 0x00007CD8
		private unsafe void UpdateInstanceDataBufferLayoutVersion()
		{
			if (this.m_CachedInstanceDataBufferLayoutVersion != this.m_BatchersContext.instanceDataBufferLayoutVersion)
			{
				this.m_CachedInstanceDataBufferLayoutVersion = this.m_BatchersContext.instanceDataBufferLayoutVersion;
				foreach (KeyValue<uint, BatchID> componentsToBatchID in this.m_GlobalBatchIDs)
				{
					BatchID batchID = *componentsToBatchID.Value;
					if (!batchID.Equals(BatchID.Null))
					{
						this.m_BRG.RemoveBatch(batchID);
					}
					InstanceComponentGroup componentsOverriden = (InstanceComponentGroup)componentsToBatchID.Key;
					*componentsToBatchID.Value = this.GetBatchID(componentsOverriden);
				}
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00009B90 File Offset: 0x00007D90
		public CPUDrawInstanceData GetDrawInstanceData()
		{
			return this.m_DrawInstanceData;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00009B98 File Offset: 0x00007D98
		public JobHandle OnPerformCulling(BatchRendererGroup rendererGroup, BatchCullingContext cc, BatchCullingOutput cullingOutput, IntPtr userContext)
		{
			foreach (KeyValue<uint, BatchID> batchID in this.m_GlobalBatchIDs)
			{
				if (batchID.Value.Equals(BatchID.Null))
				{
					return default(JobHandle);
				}
			}
			this.m_DrawInstanceData.RebuildDrawListsIfNeeded();
			bool allowOcclusionCulling = this.m_BatchersContext.hasBoundingSpheres;
			BatchCullingOutput batchCullingOutput = cullingOutput;
			CPUInstanceData.ReadOnly instanceData = this.m_BatchersContext.instanceData;
			CPUSharedInstanceData.ReadOnly sharedInstanceData = this.m_BatchersContext.sharedInstanceData;
			GPUInstanceDataBuffer.ReadOnly instanceDataBuffer = this.m_BatchersContext.instanceDataBuffer;
			JobHandle jobHandle = this.m_Culler.CreateCullJobTree(in cc, batchCullingOutput, in instanceData, in sharedInstanceData, in instanceDataBuffer, this.m_BatchersContext.lodGroupCullingData, this.m_DrawInstanceData, this.m_GlobalBatchIDs, this.m_BatchersContext.crossfadedRendererCount, this.m_BatchersContext.smallMeshScreenPercentage, allowOcclusionCulling ? this.m_BatchersContext.occlusionCullingCommon : null);
			if (this.m_OnCompleteCallback != null)
			{
				this.m_OnCompleteCallback(jobHandle, in cc, in cullingOutput);
			}
			return jobHandle;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00009CB8 File Offset: 0x00007EB8
		public void OnFinishedCulling(IntPtr customCullingResult)
		{
			int viewInstanceID = (int)customCullingResult;
			this.m_Culler.EnsureValidOcclusionTestResults(viewInstanceID);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00009CD8 File Offset: 0x00007ED8
		public void DestroyInstances(NativeArray<InstanceHandle> instances)
		{
			if (instances.Length == 0)
			{
				return;
			}
			this.m_DrawInstanceData.DestroyDrawInstances(instances);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00009CF0 File Offset: 0x00007EF0
		public void DestroyMaterials(NativeArray<int> destroyedMaterials)
		{
			if (destroyedMaterials.Length == 0)
			{
				return;
			}
			NativeList<uint> destroyedBatchMaterials = new NativeList<uint>(destroyedMaterials.Length, Allocator.TempJob);
			foreach (int destroyedMaterial in destroyedMaterials)
			{
				BatchMaterialID destroyedBatchMaterial;
				if (this.m_BatchMaterialHash.TryGetValue(destroyedMaterial, out destroyedBatchMaterial))
				{
					destroyedBatchMaterials.Add(in destroyedBatchMaterial.value);
					this.m_BatchMaterialHash.Remove(destroyedMaterial);
					this.m_BRG.UnregisterMaterial(destroyedBatchMaterial);
				}
			}
			this.m_DrawInstanceData.DestroyMaterialDrawInstances(destroyedBatchMaterials.AsArray());
			destroyedBatchMaterials.Dispose();
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00009DA8 File Offset: 0x00007FA8
		public void DestroyMeshes(NativeArray<int> destroyedMeshes)
		{
			if (destroyedMeshes.Length == 0)
			{
				return;
			}
			foreach (int destroyedMesh in destroyedMeshes)
			{
				BatchMeshID destroyedBatchMesh;
				if (this.m_BatchMeshHash.TryGetValue(destroyedMesh, out destroyedBatchMesh))
				{
					this.m_BatchMeshHash.Remove(destroyedMesh);
					this.m_BRG.UnregisterMesh(destroyedBatchMesh);
				}
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00004C45 File Offset: 0x00002E45
		public void PostCullBeginCameraRendering(RenderRequestBatcherContext context)
		{
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00009E24 File Offset: 0x00008024
		private void RegisterBatchMeshes(NativeArray<int> meshIDs)
		{
			NativeList<int> newMeshIDs = new NativeList<int>(meshIDs.Length, Allocator.TempJob);
			new FindNonRegisteredInstancesJob<BatchMeshID>
			{
				instanceIDs = meshIDs,
				hashMap = this.m_BatchMeshHash,
				outInstancesWriter = newMeshIDs.AsParallelWriter()
			}.ScheduleBatch(meshIDs.Length, 128, default(JobHandle)).Complete();
			NativeArray<BatchMeshID> newBatchMeshIDs = new NativeArray<BatchMeshID>(newMeshIDs.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			BatchRendererGroup brg = this.m_BRG;
			NativeArray<int> nativeArray = newMeshIDs.AsArray();
			brg.RegisterMeshes(in nativeArray, in newBatchMeshIDs);
			int totalMeshesNum = this.m_BatchMeshHash.Count() + newBatchMeshIDs.Length;
			this.m_BatchMeshHash.Capacity = Math.Max(this.m_BatchMeshHash.Capacity, Mathf.CeilToInt((float)totalMeshesNum / 1023f) * 1024);
			new RegisterNewInstancesJob<BatchMeshID>
			{
				instanceIDs = newMeshIDs.AsArray(),
				batchIDs = newBatchMeshIDs,
				hashMap = this.m_BatchMeshHash.AsParallelWriter()
			}.Schedule(newMeshIDs.Length, 128, default(JobHandle)).Complete();
			newMeshIDs.Dispose();
			newBatchMeshIDs.Dispose();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00009F70 File Offset: 0x00008170
		private void RegisterBatchMaterials(in NativeArray<int> usedMaterialIDs)
		{
			NativeArray<int> nativeArray = usedMaterialIDs;
			NativeList<int> newMaterialIDs = new NativeList<int>(nativeArray.Length, Allocator.TempJob);
			FindNonRegisteredInstancesJob<BatchMaterialID> findNonRegisteredInstancesJob = new FindNonRegisteredInstancesJob<BatchMaterialID>
			{
				instanceIDs = usedMaterialIDs,
				hashMap = this.m_BatchMaterialHash,
				outInstancesWriter = newMaterialIDs.AsParallelWriter()
			};
			nativeArray = usedMaterialIDs;
			findNonRegisteredInstancesJob.ScheduleBatch(nativeArray.Length, 128, default(JobHandle)).Complete();
			NativeArray<BatchMaterialID> newBatchMaterialIDs = new NativeArray<BatchMaterialID>(newMaterialIDs.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			BatchRendererGroup brg = this.m_BRG;
			nativeArray = newMaterialIDs.AsArray();
			brg.RegisterMaterials(in nativeArray, in newBatchMaterialIDs);
			int totalMaterialsNum = this.m_BatchMaterialHash.Count() + newMaterialIDs.Length;
			this.m_BatchMaterialHash.Capacity = Math.Max(this.m_BatchMaterialHash.Capacity, Mathf.CeilToInt((float)totalMaterialsNum / 1023f) * 1024);
			new RegisterNewInstancesJob<BatchMaterialID>
			{
				instanceIDs = newMaterialIDs.AsArray(),
				batchIDs = newBatchMaterialIDs,
				hashMap = this.m_BatchMaterialHash.AsParallelWriter()
			}.Schedule(newMaterialIDs.Length, 128, default(JobHandle)).Complete();
			newMaterialIDs.Dispose();
			newBatchMaterialIDs.Dispose();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000A0CC File Offset: 0x000082CC
		public void BuildBatch(NativeArray<InstanceHandle> instances, NativeArray<int> usedMaterialIDs, NativeArray<int> usedMeshIDs, in GPUDrivenRendererGroupData rendererData)
		{
			this.RegisterBatchMaterials(in usedMaterialIDs);
			this.RegisterBatchMeshes(usedMeshIDs);
			CreateDrawBatchesJob createDrawBatchesJob = default(CreateDrawBatchesJob);
			NativeArray<int> instancesCount = rendererData.instancesCount;
			createDrawBatchesJob.implicitInstanceIndices = instancesCount.Length == 0;
			createDrawBatchesJob.instances = instances;
			createDrawBatchesJob.rendererData = rendererData;
			createDrawBatchesJob.batchMeshHash = this.m_BatchMeshHash;
			createDrawBatchesJob.batchMaterialHash = this.m_BatchMaterialHash;
			createDrawBatchesJob.rangeHash = this.m_DrawInstanceData.rangeHash;
			createDrawBatchesJob.drawRanges = this.m_DrawInstanceData.drawRanges;
			createDrawBatchesJob.batchHash = this.m_DrawInstanceData.batchHash;
			createDrawBatchesJob.drawBatches = this.m_DrawInstanceData.drawBatches;
			createDrawBatchesJob.drawInstances = this.m_DrawInstanceData.drawInstances;
			createDrawBatchesJob.Run<CreateDrawBatchesJob>();
			this.m_DrawInstanceData.NeedsRebuild();
			this.UpdateInstanceDataBufferLayoutVersion();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000A1AA File Offset: 0x000083AA
		public void InstanceOccludersUpdated(int viewInstanceID, int subviewMask)
		{
			this.m_Culler.InstanceOccludersUpdated(viewInstanceID, subviewMask, this.m_BatchersContext);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000A1BF File Offset: 0x000083BF
		public void UpdateFrame()
		{
			this.m_Culler.UpdateFrame();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000A1CC File Offset: 0x000083CC
		public ParallelBitArray GetCompactedVisibilityMasks(bool syncCullingJobs)
		{
			return this.m_Culler.GetCompactedVisibilityMasks(syncCullingJobs);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000A1DC File Offset: 0x000083DC
		public void OnEndContextRendering()
		{
			ParallelBitArray compactedVisibilityMasks = this.GetCompactedVisibilityMasks(true);
			if (compactedVisibilityMasks.IsCreated)
			{
				this.m_BatchersContext.UpdatePerFrameInstanceVisibility(in compactedVisibilityMasks);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000A207 File Offset: 0x00008407
		public void OnBeginCameraRendering(Camera camera)
		{
			this.m_Culler.OnBeginCameraRendering(camera);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000A215 File Offset: 0x00008415
		public void OnEndCameraRendering(Camera camera)
		{
			this.m_Culler.OnEndCameraRendering(camera);
		}

		// Token: 0x04000187 RID: 391
		private RenderersBatchersContext m_BatchersContext;

		// Token: 0x04000188 RID: 392
		private CPUDrawInstanceData m_DrawInstanceData;

		// Token: 0x04000189 RID: 393
		private BatchRendererGroup m_BRG;

		// Token: 0x0400018A RID: 394
		private NativeParallelHashMap<uint, BatchID> m_GlobalBatchIDs;

		// Token: 0x0400018B RID: 395
		private InstanceCuller m_Culler;

		// Token: 0x0400018C RID: 396
		private NativeParallelHashMap<int, BatchMaterialID> m_BatchMaterialHash;

		// Token: 0x0400018D RID: 397
		private NativeParallelHashMap<int, BatchMeshID> m_BatchMeshHash;

		// Token: 0x0400018E RID: 398
		private int m_CachedInstanceDataBufferLayoutVersion;

		// Token: 0x0400018F RID: 399
		private OnCullingCompleteCallback m_OnCompleteCallback;
	}
}
