using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x02000045 RID: 69
	internal struct InstanceCuller : IDisposable
	{
		// Token: 0x06000115 RID: 277 RVA: 0x0000751C File Offset: 0x0000571C
		internal void Init(GPUResidentDrawerResources resources, DebugRendererBatcherStats debugStats = null)
		{
			this.m_IndirectStorage.Init();
			this.m_OcclusionTestShader.Init(resources.instanceOcclusionCullingKernels);
			this.m_ResetDrawArgsKernel = this.m_OcclusionTestShader.cs.FindKernel("ResetDrawArgs");
			this.m_CopyInstancesKernel = this.m_OcclusionTestShader.cs.FindKernel("CopyInstances");
			this.m_CullInstancesKernel = this.m_OcclusionTestShader.cs.FindKernel("CullInstances");
			this.m_DebugStats = debugStats;
			this.m_SplitDebugArray = default(InstanceCullerSplitDebugArray);
			this.m_SplitDebugArray.Init();
			this.m_OcclusionEventDebugArray = default(InstanceOcclusionEventDebugArray);
			this.m_OcclusionEventDebugArray.Init();
			this.m_ProfilingSampleInstanceOcclusionTest = new ProfilingSampler("InstanceOcclusionTest");
			this.m_ShaderVariables = new NativeArray<InstanceOcclusionCullerShaderVariables>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.m_ConstantBuffer = new ComputeBuffer(1, UnsafeUtility.SizeOf<InstanceOcclusionCullerShaderVariables>(), ComputeBufferType.Constant);
			this.m_CommandBuffer = new CommandBuffer();
			this.m_CommandBuffer.name = "EnsureValidOcclusionTestResults";
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00007618 File Offset: 0x00005818
		private unsafe JobHandle CreateFrustumCullingJob(in BatchCullingContext cc, in CPUInstanceData.ReadOnly instanceData, in CPUSharedInstanceData.ReadOnly sharedInstanceData, NativeList<LODGroupCullingData> lodGroupCullingData, in BinningConfig binningConfig, float smallMeshScreenPercentage, OcclusionCullingCommon occlusionCullingCommon, NativeArray<byte> rendererVisibilityMasks, NativeArray<byte> rendererCrossFadeValues)
		{
			FrustumPlaneCuller frustumPlaneCuller;
			ReceiverPlanes receiverPlanes;
			ReceiverSphereCuller receiverSphereCuller;
			float screenRelativeMetric;
			fixed (BatchCullingContext* ptr = &cc)
			{
				BatchCullingContext* contextPtr = ptr;
				new InstanceCuller.SetupCullingJobInput
				{
					lodBias = QualitySettings.lodBias,
					context = contextPtr,
					frustumPlaneCuller = &frustumPlaneCuller,
					receiverPlanes = &receiverPlanes,
					receiverSphereCuller = &receiverSphereCuller,
					screenRelativeMetric = &screenRelativeMetric
				}.Run<InstanceCuller.SetupCullingJobInput>();
			}
			if (occlusionCullingCommon != null)
			{
				occlusionCullingCommon.UpdateSilhouettePlanes(cc.viewID.GetInstanceID(), receiverPlanes.SilhouettePlaneSubArray());
			}
			JobHandle cullingJob = new CullingJob
			{
				binningConfig = binningConfig,
				viewType = cc.viewType,
				frustumPlanePackets = frustumPlaneCuller.planePackets.AsArray(),
				frustumSplitInfos = frustumPlaneCuller.splitInfos.AsArray(),
				lightFacingFrustumPlanes = receiverPlanes.LightFacingFrustumPlaneSubArray(),
				receiverSplitInfos = receiverSphereCuller.splitInfos.AsArray(),
				worldToLightSpaceRotation = receiverSphereCuller.worldToLightSpaceRotation,
				cullLightmappedShadowCasters = ((cc.cullingFlags & BatchCullingFlags.CullLightmappedShadowCasters) > BatchCullingFlags.None),
				cameraPosition = cc.lodParameters.cameraPosition,
				sqrScreenRelativeMetric = screenRelativeMetric * screenRelativeMetric,
				minScreenRelativeHeight = smallMeshScreenPercentage * 0.01f,
				isOrtho = cc.lodParameters.isOrthographic,
				instanceData = instanceData,
				sharedInstanceData = sharedInstanceData,
				lodGroupCullingData = lodGroupCullingData,
				occlusionBuffer = cc.occlusionBuffer,
				rendererVisibilityMasks = rendererVisibilityMasks,
				rendererCrossFadeValues = rendererCrossFadeValues,
				maxLOD = QualitySettings.maximumLODLevel,
				cullingLayerMask = cc.cullingLayerMask,
				sceneCullingMask = cc.sceneCullingMask
			}.Schedule(instanceData.instancesLength, 32, default(JobHandle));
			receiverPlanes.Dispose(cullingJob);
			frustumPlaneCuller.Dispose(cullingJob);
			receiverSphereCuller.Dispose(cullingJob);
			return cullingJob;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00007814 File Offset: 0x00005A14
		private int ComputeWorstCaseDrawCommandCount(in BatchCullingContext cc, BinningConfig binningConfig, CPUDrawInstanceData drawInstanceData, int crossFadedRendererCount)
		{
			int visibleInstancesCount = drawInstanceData.drawInstances.Length;
			int drawCommandCount = drawInstanceData.drawBatches.Length;
			drawCommandCount += math.min(crossFadedRendererCount, drawCommandCount);
			drawCommandCount *= 2;
			if (binningConfig.supportsMotionCheck)
			{
				drawCommandCount *= 2;
			}
			if (cc.cullingSplits.Length > 1)
			{
				drawCommandCount <<= cc.cullingSplits.Length - 1;
			}
			return math.min(drawCommandCount, visibleInstancesCount);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000788C File Offset: 0x00005A8C
		public unsafe JobHandle CreateCullJobTree(in BatchCullingContext cc, BatchCullingOutput cullingOutput, in CPUInstanceData.ReadOnly instanceData, in CPUSharedInstanceData.ReadOnly sharedInstanceData, in GPUInstanceDataBuffer.ReadOnly instanceDataBuffer, NativeList<LODGroupCullingData> lodGroupCullingData, CPUDrawInstanceData drawInstanceData, NativeParallelHashMap<uint, BatchID> batchIDs, int crossFadedRendererCount, float smallMeshScreenPercentage, OcclusionCullingCommon occlusionCullingCommon)
		{
			BatchCullingOutputDrawCommands drawCommands = default(BatchCullingOutputDrawCommands);
			drawCommands.drawRangeCount = drawInstanceData.drawRanges.Length;
			drawCommands.drawRanges = MemoryUtilities.Malloc<BatchDrawRange>(drawCommands.drawRangeCount, Allocator.TempJob);
			for (int i = 0; i < drawCommands.drawRangeCount; i++)
			{
				drawCommands.drawRanges[i].drawCommandsCount = 0U;
			}
			cullingOutput.drawCommands[0] = drawCommands;
			cullingOutput.customCullingResult[0] = IntPtr.Zero;
			BinningConfig binningConfig = new BinningConfig
			{
				viewCount = cc.cullingSplits.Length,
				supportsCrossFade = (crossFadedRendererCount > 0),
				supportsMotionCheck = (cc.viewType == BatchCullingViewType.Camera)
			};
			int visibilityLength = instanceData.handlesLength;
			NativeArray<byte> rendererVisibilityMasks = new NativeArray<byte>(visibilityLength, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<byte> rendererCrossFadeValues = new NativeArray<byte>(visibilityLength, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			JobHandle cullingJobHandle = this.CreateFrustumCullingJob(in cc, in instanceData, in sharedInstanceData, lodGroupCullingData, in binningConfig, smallMeshScreenPercentage, occlusionCullingCommon, rendererVisibilityMasks, rendererCrossFadeValues);
			if (cc.viewType == BatchCullingViewType.Camera || cc.viewType == BatchCullingViewType.Light || cc.viewType == BatchCullingViewType.SelectionOutline)
			{
				cullingJobHandle = this.CreateCompactedVisibilityMaskJob(in instanceData, rendererVisibilityMasks, cullingJobHandle);
				int debugCounterBaseIndex = -1;
				DebugRendererBatcherStats debugStats = this.m_DebugStats;
				if (debugStats != null && debugStats.enabled)
				{
					debugCounterBaseIndex = this.m_SplitDebugArray.TryAddSplits(cc.viewType, cc.viewID.GetInstanceID(), cc.cullingSplits.Length);
				}
				int batchCount = drawInstanceData.drawBatches.Length;
				int maxBinCount = this.ComputeWorstCaseDrawCommandCount(in cc, binningConfig, drawInstanceData, crossFadedRendererCount);
				NativeArray<int> batchBinAllocOffsets = new NativeArray<int>(batchCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				NativeArray<int> batchBinCounts = new NativeArray<int>(batchCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				NativeArray<int> batchDrawCommandOffsets = new NativeArray<int>(batchCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				NativeArray<int> binAllocCounter = new NativeArray<int>(16, Allocator.TempJob, NativeArrayOptions.ClearMemory);
				NativeArray<short> binConfigIndices = new NativeArray<short>(maxBinCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				NativeArray<int> binVisibleInstanceCounts = new NativeArray<int>(maxBinCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				NativeArray<int> binVisibleInstanceOffsets = new NativeArray<int>(maxBinCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				int indirectContextIndex = -1;
				bool flag = occlusionCullingCommon != null && occlusionCullingCommon.HasOccluderContext(cc.viewID.GetInstanceID());
				if (flag)
				{
					int viewInstanceID = cc.viewID.GetInstanceID();
					indirectContextIndex = this.m_IndirectStorage.TryAllocateContext(viewInstanceID);
					cullingOutput.customCullingResult[0] = (IntPtr)viewInstanceID;
				}
				IndirectBufferLimits indirectBufferLimits = this.m_IndirectStorage.GetLimits(indirectContextIndex);
				NativeArray<IndirectBufferAllocInfo> indirectBufferAllocInfo = this.m_IndirectStorage.GetAllocInfoSubArray(indirectContextIndex);
				JobHandle allocateBinsHandle = new AllocateBinsPerBatch
				{
					binningConfig = binningConfig,
					drawBatches = drawInstanceData.drawBatches,
					drawInstanceIndices = drawInstanceData.drawInstanceIndices,
					instanceData = instanceData,
					rendererVisibilityMasks = rendererVisibilityMasks,
					batchBinAllocOffsets = batchBinAllocOffsets,
					batchBinCounts = batchBinCounts,
					binAllocCounter = binAllocCounter,
					binConfigIndices = binConfigIndices,
					binVisibleInstanceCounts = binVisibleInstanceCounts,
					splitDebugCounters = this.m_SplitDebugArray.Counters,
					debugCounterIndexBase = debugCounterBaseIndex
				}.Schedule(batchCount, 1, cullingJobHandle);
				this.m_SplitDebugArray.AddSync(debugCounterBaseIndex, allocateBinsHandle);
				JobHandle prefixSumHandle = new PrefixSumDrawsAndInstances
				{
					drawRanges = drawInstanceData.drawRanges,
					drawBatchIndices = drawInstanceData.drawBatchIndices,
					batchBinAllocOffsets = batchBinAllocOffsets,
					batchBinCounts = batchBinCounts,
					binVisibleInstanceCounts = binVisibleInstanceCounts,
					batchDrawCommandOffsets = batchDrawCommandOffsets,
					binVisibleInstanceOffsets = binVisibleInstanceOffsets,
					cullingOutput = cullingOutput.drawCommands,
					indirectBufferLimits = indirectBufferLimits,
					indirectBufferAllocInfo = indirectBufferAllocInfo,
					indirectAllocationCounters = this.m_IndirectStorage.allocationCounters
				}.Schedule(allocateBinsHandle);
				JobHandle drawCommandOutputHandle = new DrawCommandOutputPerBatch
				{
					binningConfig = binningConfig,
					batchIDs = batchIDs,
					instanceDataBuffer = instanceDataBuffer,
					drawBatches = drawInstanceData.drawBatches,
					drawInstanceIndices = drawInstanceData.drawInstanceIndices,
					instanceData = instanceData,
					rendererVisibilityMasks = rendererVisibilityMasks,
					rendererCrossFadeValues = rendererCrossFadeValues,
					batchBinAllocOffsets = batchBinAllocOffsets,
					batchBinCounts = batchBinCounts,
					batchDrawCommandOffsets = batchDrawCommandOffsets,
					binConfigIndices = binConfigIndices,
					binVisibleInstanceOffsets = binVisibleInstanceOffsets,
					binVisibleInstanceCounts = binVisibleInstanceCounts,
					cullingOutput = cullingOutput.drawCommands,
					indirectBufferLimits = indirectBufferLimits,
					visibleInstancesBufferHandle = this.m_IndirectStorage.visibleInstanceBufferHandle,
					indirectArgsBufferHandle = this.m_IndirectStorage.indirectArgsBufferHandle,
					indirectBufferAllocInfo = indirectBufferAllocInfo,
					indirectInstanceInfoGlobalArray = this.m_IndirectStorage.instanceInfoGlobalArray,
					indirectDrawInfoGlobalArray = this.m_IndirectStorage.drawInfoGlobalArray
				}.Schedule(batchCount, 1, prefixSumHandle);
				if (flag)
				{
					this.m_IndirectStorage.SetBufferContext(indirectContextIndex, new IndirectBufferContext(drawCommandOutputHandle));
				}
				cullingJobHandle = drawCommandOutputHandle;
			}
			cullingJobHandle = rendererVisibilityMasks.Dispose(cullingJobHandle);
			return rendererCrossFadeValues.Dispose(cullingJobHandle);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00007D4C File Offset: 0x00005F4C
		private JobHandle CreateCompactedVisibilityMaskJob(in CPUInstanceData.ReadOnly instanceData, NativeArray<byte> rendererVisibilityMasks, JobHandle cullingJobHandle)
		{
			if (!this.m_CompactedVisibilityMasks.IsCreated)
			{
				this.m_CompactedVisibilityMasks = new ParallelBitArray(instanceData.handlesLength, Allocator.TempJob, NativeArrayOptions.ClearMemory);
			}
			JobHandle compactVisibilityMasksJobHandle = new CompactVisibilityMasksJob
			{
				rendererVisibilityMasks = rendererVisibilityMasks,
				compactedVisibilityMasks = this.m_CompactedVisibilityMasks
			}.ScheduleBatch(rendererVisibilityMasks.Length, 64, cullingJobHandle);
			this.m_CompactedVisibilityMasksJobsHandle = JobHandle.CombineDependencies(this.m_CompactedVisibilityMasksJobsHandle, compactVisibilityMasksJobHandle);
			return compactVisibilityMasksJobHandle;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007DBC File Offset: 0x00005FBC
		public void InstanceOccludersUpdated(int viewInstanceID, int subviewMask, RenderersBatchersContext batchersContext)
		{
			DebugRendererBatcherStats debugStats = this.m_DebugStats;
			OccluderContext occluderCtx;
			if (debugStats != null && debugStats.enabled && batchersContext.occlusionCullingCommon.GetOccluderContext(viewInstanceID, out occluderCtx))
			{
				this.m_OcclusionEventDebugArray.TryAdd(viewInstanceID, InstanceOcclusionEventType.OccluderUpdate, occluderCtx.version, subviewMask, OcclusionTest.None);
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007E03 File Offset: 0x00006003
		private void DisposeCompactVisibilityMasks()
		{
			if (this.m_CompactedVisibilityMasks.IsCreated)
			{
				this.m_CompactedVisibilityMasks.Dispose();
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004C45 File Offset: 0x00002E45
		private void DisposeSceneViewHiddenBits()
		{
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00007E1D File Offset: 0x0000601D
		public ParallelBitArray GetCompactedVisibilityMasks(bool syncCullingJobs)
		{
			if (syncCullingJobs)
			{
				this.m_CompactedVisibilityMasksJobsHandle.Complete();
			}
			return this.m_CompactedVisibilityMasks;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00007E34 File Offset: 0x00006034
		public void InstanceOcclusionTest(RenderGraph renderGraph, in OcclusionCullingSettings settings, ReadOnlySpan<SubviewOcclusionTest> subviewOcclusionTests, RenderersBatchersContext batchersContext)
		{
			OccluderContext occluderCtx;
			if (!batchersContext.occlusionCullingCommon.GetOccluderContext(settings.viewInstanceID, out occluderCtx))
			{
				return;
			}
			OccluderHandles occluderHandles = occluderCtx.Import(renderGraph);
			if (!occluderHandles.IsValid())
			{
				return;
			}
			InstanceCuller.InstanceOcclusionTestPassData passData;
			using (IComputeRenderGraphBuilder builder = renderGraph.AddComputePass<InstanceCuller.InstanceOcclusionTestPassData>("Instance Occlusion Test", out passData, this.m_ProfilingSampleInstanceOcclusionTest, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/GPUDriven/InstanceCuller.cs", 2029))
			{
				builder.AllowGlobalStateModification(true);
				passData.settings = settings;
				passData.subviewSettings = InstanceOcclusionTestSubviewSettings.FromSpan(subviewOcclusionTests);
				passData.bufferHandles = this.m_IndirectStorage.ImportBuffers(renderGraph);
				passData.occluderHandles = occluderHandles;
				passData.bufferHandles.UseForOcclusionTest(builder);
				passData.occluderHandles.UseForOcclusionTest(builder);
				builder.SetRenderFunc<InstanceCuller.InstanceOcclusionTestPassData>(delegate(InstanceCuller.InstanceOcclusionTestPassData data, ComputeGraphContext context)
				{
					GPUResidentBatcher batcher = GPUResidentDrawer.instance.batcher;
					batcher.instanceCullingBatcher.culler.AddOcclusionCullingDispatch(context.cmd, in data.settings, in data.subviewSettings, in data.bufferHandles, in data.occluderHandles, batcher.batchersContext);
				});
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00007F1C File Offset: 0x0000611C
		internal void EnsureValidOcclusionTestResults(int viewInstanceID)
		{
			int indirectContextIndex = this.m_IndirectStorage.TryGetContextIndex(viewInstanceID);
			if (indirectContextIndex >= 0)
			{
				IndirectBufferContext bufferCtx = this.m_IndirectStorage.GetBufferContext(indirectContextIndex);
				if (bufferCtx.bufferState == IndirectBufferContext.BufferState.Pending)
				{
					bufferCtx.cullingJobHandle.Complete();
				}
				IndirectBufferAllocInfo allocInfo = this.m_IndirectStorage.GetAllocInfo(indirectContextIndex);
				if (!allocInfo.IsEmpty())
				{
					CommandBuffer cmd = this.m_CommandBuffer;
					cmd.Clear();
					this.m_IndirectStorage.CopyFromStaging(cmd, in allocInfo);
					ComputeShader cs = this.m_OcclusionTestShader.cs;
					this.m_ShaderVariables[0] = new InstanceOcclusionCullerShaderVariables
					{
						_DrawInfoAllocIndex = (uint)allocInfo.drawAllocIndex,
						_DrawInfoCount = (uint)allocInfo.drawCount,
						_InstanceInfoAllocIndex = (uint)(2 * allocInfo.instanceAllocIndex),
						_InstanceInfoCount = (uint)allocInfo.instanceCount,
						_BoundingSphereInstanceDataAddress = 0,
						_DebugCounterIndex = -1,
						_InstanceMultiplierShift = 0
					};
					cmd.SetBufferData<InstanceOcclusionCullerShaderVariables>(this.m_ConstantBuffer, this.m_ShaderVariables);
					cmd.SetComputeConstantBufferParam(cs, InstanceCuller.ShaderIDs.InstanceOcclusionCullerShaderVariables, this.m_ConstantBuffer, 0, this.m_ConstantBuffer.stride);
					int kernel = this.m_CopyInstancesKernel;
					cmd.SetComputeBufferParam(cs, kernel, InstanceCuller.ShaderIDs._DrawInfo, this.m_IndirectStorage.drawInfoBuffer);
					cmd.SetComputeBufferParam(cs, kernel, InstanceCuller.ShaderIDs._InstanceInfo, this.m_IndirectStorage.instanceInfoBuffer);
					cmd.SetComputeBufferParam(cs, kernel, InstanceCuller.ShaderIDs._DrawArgs, this.m_IndirectStorage.argsBuffer);
					cmd.SetComputeBufferParam(cs, kernel, InstanceCuller.ShaderIDs._InstanceIndices, this.m_IndirectStorage.instanceBuffer);
					cmd.DispatchCompute(cs, kernel, (allocInfo.instanceCount + 63) / 64, 1, 1);
					Graphics.ExecuteCommandBuffer(cmd);
					cmd.Clear();
				}
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000080C8 File Offset: 0x000062C8
		private void AddOcclusionCullingDispatch(ComputeCommandBuffer cmd, in OcclusionCullingSettings settings, in InstanceOcclusionTestSubviewSettings subviewSettings, in IndirectBufferContextHandles bufferHandles, in OccluderHandles occluderHandles, RenderersBatchersContext batchersContext)
		{
			OcclusionCullingCommon occlusionCullingCommon = batchersContext.occlusionCullingCommon;
			int indirectContextIndex = this.m_IndirectStorage.TryGetContextIndex(settings.viewInstanceID);
			if (indirectContextIndex >= 0)
			{
				IndirectBufferContext bufferCtx = this.m_IndirectStorage.GetBufferContext(indirectContextIndex);
				OccluderContext occluderCtx;
				bool hasOccluders = occlusionCullingCommon.GetOccluderContext(settings.viewInstanceID, out occluderCtx);
				hasOccluders = hasOccluders && (subviewSettings.occluderSubviewMask & occluderCtx.subviewValidMask) == subviewSettings.occluderSubviewMask;
				IndirectBufferContext.BufferState newBufferState = IndirectBufferContext.BufferState.Zeroed;
				int newOccluderVersion = 0;
				int newSubviewMask = 0;
				switch (settings.occlusionTest)
				{
				case OcclusionTest.None:
					newBufferState = IndirectBufferContext.BufferState.NoOcclusionTest;
					break;
				case OcclusionTest.TestAll:
					if (hasOccluders)
					{
						newBufferState = IndirectBufferContext.BufferState.AllInstancesOcclusionTested;
						newOccluderVersion = occluderCtx.version;
						newSubviewMask = subviewSettings.occluderSubviewMask;
					}
					else
					{
						newBufferState = IndirectBufferContext.BufferState.NoOcclusionTest;
					}
					break;
				case OcclusionTest.TestCulled:
					if (hasOccluders)
					{
						bool hasMatchingCullingOutput = true;
						IndirectBufferContext.BufferState bufferState = bufferCtx.bufferState;
						if (bufferState - IndirectBufferContext.BufferState.Zeroed > 1)
						{
							if (bufferState - IndirectBufferContext.BufferState.AllInstancesOcclusionTested <= 1)
							{
								if (bufferCtx.subviewMask != subviewSettings.occluderSubviewMask)
								{
									Debug.Log("Expected an occlusion test of TestCulled to use the same subview mask as the previous occlusion test");
									hasMatchingCullingOutput = false;
								}
							}
							else
							{
								hasMatchingCullingOutput = false;
								Debug.Log("Expected the previous occlusion test to be TestAll before using TestCulled");
							}
						}
						else
						{
							hasMatchingCullingOutput = false;
						}
						if (hasMatchingCullingOutput)
						{
							newBufferState = IndirectBufferContext.BufferState.OccludedInstancesReTested;
							newOccluderVersion = occluderCtx.version;
							newSubviewMask = subviewSettings.occluderSubviewMask;
						}
					}
					break;
				}
				if (!bufferCtx.Matches(newBufferState, newOccluderVersion, newSubviewMask))
				{
					bool isFirstPass = newBufferState == IndirectBufferContext.BufferState.AllInstancesOcclusionTested;
					bool isSecondPass = newBufferState == IndirectBufferContext.BufferState.OccludedInstancesReTested;
					bool flag = bufferCtx.bufferState == IndirectBufferContext.BufferState.Pending;
					bool doCopyInstances = newBufferState == IndirectBufferContext.BufferState.NoOcclusionTest;
					bool doResetDraws = bufferCtx.bufferState != IndirectBufferContext.BufferState.Zeroed && !doCopyInstances;
					bool doCullInstances = newBufferState != IndirectBufferContext.BufferState.Zeroed && !doCopyInstances;
					if (flag)
					{
						bufferCtx.cullingJobHandle.Complete();
					}
					IndirectBufferAllocInfo allocInfo = this.m_IndirectStorage.GetAllocInfo(indirectContextIndex);
					bufferCtx.bufferState = newBufferState;
					bufferCtx.occluderVersion = newOccluderVersion;
					bufferCtx.subviewMask = newSubviewMask;
					if (!allocInfo.IsEmpty())
					{
						int debugCounterIndex = -1;
						DebugRendererBatcherStats debugStats = this.m_DebugStats;
						if (debugStats != null && debugStats.enabled)
						{
							debugCounterIndex = this.m_OcclusionEventDebugArray.TryAdd(settings.viewInstanceID, InstanceOcclusionEventType.OcclusionTest, newOccluderVersion, newSubviewMask, isFirstPass ? OcclusionTest.TestAll : (isSecondPass ? OcclusionTest.TestCulled : OcclusionTest.None));
						}
						bool occlusionDebug = false;
						if (isFirstPass || isSecondPass)
						{
							bool flag2;
							if (OcclusionCullingCommon.UseOcclusionDebug(in occluderCtx))
							{
								BufferHandle occlusionDebugOverlay = occluderHandles.occlusionDebugOverlay;
								flag2 = occlusionDebugOverlay.IsValid();
							}
							else
							{
								flag2 = false;
							}
							occlusionDebug = flag2;
						}
						ComputeShader cs = this.m_OcclusionTestShader.cs;
						LocalKeyword firstPassKeyword = new LocalKeyword(cs, "OCCLUSION_FIRST_PASS");
						LocalKeyword secondPassKeyword = new LocalKeyword(cs, "OCCLUSION_SECOND_PASS");
						OccluderContext.SetKeyword(cmd, cs, in firstPassKeyword, isFirstPass);
						OccluderContext.SetKeyword(cmd, cs, in secondPassKeyword, isSecondPass);
						this.m_ShaderVariables[0] = new InstanceOcclusionCullerShaderVariables
						{
							_DrawInfoAllocIndex = (uint)allocInfo.drawAllocIndex,
							_DrawInfoCount = (uint)allocInfo.drawCount,
							_InstanceInfoAllocIndex = (uint)(2 * allocInfo.instanceAllocIndex),
							_InstanceInfoCount = (uint)allocInfo.instanceCount,
							_BoundingSphereInstanceDataAddress = batchersContext.renderersParameters.boundingSphere.gpuAddress,
							_DebugCounterIndex = debugCounterIndex,
							_InstanceMultiplierShift = ((settings.instanceMultiplier == 2) ? 1 : 0)
						};
						cmd.SetBufferData<InstanceOcclusionCullerShaderVariables>(this.m_ConstantBuffer, this.m_ShaderVariables);
						cmd.SetComputeConstantBufferParam(cs, InstanceCuller.ShaderIDs.InstanceOcclusionCullerShaderVariables, this.m_ConstantBuffer, 0, this.m_ConstantBuffer.stride);
						occlusionCullingCommon.PrepareCulling(cmd, in occluderCtx, in settings, in subviewSettings, in this.m_OcclusionTestShader, occlusionDebug);
						if (doCopyInstances)
						{
							int kernel = this.m_CopyInstancesKernel;
							cmd.SetComputeBufferParam(cs, kernel, InstanceCuller.ShaderIDs._DrawInfo, this.m_IndirectStorage.drawInfoBuffer);
							cmd.SetComputeBufferParam(cs, kernel, InstanceCuller.ShaderIDs._InstanceInfo, this.m_IndirectStorage.instanceInfoBuffer);
							cmd.SetComputeBufferParam(cs, kernel, InstanceCuller.ShaderIDs._DrawArgs, this.m_IndirectStorage.argsBuffer);
							cmd.SetComputeBufferParam(cs, kernel, InstanceCuller.ShaderIDs._InstanceIndices, this.m_IndirectStorage.instanceBuffer);
							cmd.DispatchCompute(cs, kernel, (allocInfo.instanceCount + 63) / 64, 1, 1);
						}
						if (doResetDraws)
						{
							int kernel2 = this.m_ResetDrawArgsKernel;
							cmd.SetComputeBufferParam(cs, kernel2, InstanceCuller.ShaderIDs._DrawInfo, bufferHandles.drawInfoBuffer);
							cmd.SetComputeBufferParam(cs, kernel2, InstanceCuller.ShaderIDs._DrawArgs, bufferHandles.argsBuffer);
							cmd.DispatchCompute(cs, kernel2, (allocInfo.drawCount + 63) / 64, 1, 1);
						}
						if (doCullInstances)
						{
							int kernel3 = this.m_CullInstancesKernel;
							cmd.SetComputeBufferParam(cs, kernel3, InstanceCuller.ShaderIDs._DrawInfo, bufferHandles.drawInfoBuffer);
							cmd.SetComputeBufferParam(cs, kernel3, InstanceCuller.ShaderIDs._InstanceInfo, bufferHandles.instanceInfoBuffer);
							cmd.SetComputeBufferParam(cs, kernel3, InstanceCuller.ShaderIDs._DrawArgs, bufferHandles.argsBuffer);
							cmd.SetComputeBufferParam(cs, kernel3, InstanceCuller.ShaderIDs._InstanceIndices, bufferHandles.instanceBuffer);
							cmd.SetComputeBufferParam(cs, kernel3, InstanceCuller.ShaderIDs._InstanceDataBuffer, batchersContext.gpuInstanceDataBuffer);
							cmd.SetComputeBufferParam(cs, kernel3, InstanceCuller.ShaderIDs._OcclusionDebugCounters, this.m_OcclusionEventDebugArray.CounterBuffer);
							if (isFirstPass || isSecondPass)
							{
								OcclusionCullingCommon.SetDepthPyramid(cmd, in this.m_OcclusionTestShader, kernel3, in occluderHandles);
							}
							if (occlusionDebug)
							{
								OcclusionCullingCommon.SetDebugPyramid(cmd, in this.m_OcclusionTestShader, kernel3, in occluderHandles);
							}
							if (isSecondPass)
							{
								cmd.DispatchCompute(cs, kernel3, bufferHandles.argsBuffer, (uint)(20 * allocInfo.GetExtraDrawInfoSlotIndex()));
							}
							else
							{
								cmd.DispatchCompute(cs, kernel3, (allocInfo.instanceCount + 63) / 64, 1, 1);
							}
						}
					}
				}
				this.m_IndirectStorage.SetBufferContext(indirectContextIndex, bufferCtx);
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000085F3 File Offset: 0x000067F3
		private void FlushDebugCounters()
		{
			DebugRendererBatcherStats debugStats = this.m_DebugStats;
			if (debugStats != null && debugStats.enabled)
			{
				this.m_SplitDebugArray.MoveToDebugStatsAndClear(this.m_DebugStats);
				this.m_OcclusionEventDebugArray.MoveToDebugStatsAndClear(this.m_DebugStats);
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004C45 File Offset: 0x00002E45
		private void OnBeginSceneViewCameraRendering()
		{
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004C45 File Offset: 0x00002E45
		private void OnEndSceneViewCameraRendering()
		{
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000862B File Offset: 0x0000682B
		public void UpdateFrame()
		{
			this.DisposeSceneViewHiddenBits();
			this.DisposeCompactVisibilityMasks();
			this.FlushDebugCounters();
			this.m_IndirectStorage.ClearContextsAndGrowBuffers();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000864A File Offset: 0x0000684A
		public void OnBeginCameraRendering(Camera camera)
		{
			if (camera.cameraType == CameraType.SceneView)
			{
				this.OnBeginSceneViewCameraRendering();
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000865B File Offset: 0x0000685B
		public void OnEndCameraRendering(Camera camera)
		{
			if (camera.cameraType == CameraType.SceneView)
			{
				this.OnEndSceneViewCameraRendering();
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000866C File Offset: 0x0000686C
		public void Dispose()
		{
			this.DisposeSceneViewHiddenBits();
			this.DisposeCompactVisibilityMasks();
			this.m_IndirectStorage.Dispose();
			this.m_DebugStats = null;
			this.m_OcclusionEventDebugArray.Dispose();
			this.m_SplitDebugArray.Dispose();
			this.m_ShaderVariables.Dispose();
			this.m_ConstantBuffer.Release();
			this.m_CommandBuffer.Dispose();
		}

		// Token: 0x0400012D RID: 301
		private ParallelBitArray m_CompactedVisibilityMasks;

		// Token: 0x0400012E RID: 302
		private JobHandle m_CompactedVisibilityMasksJobsHandle;

		// Token: 0x0400012F RID: 303
		private IndirectBufferContextStorage m_IndirectStorage;

		// Token: 0x04000130 RID: 304
		private OcclusionTestComputeShader m_OcclusionTestShader;

		// Token: 0x04000131 RID: 305
		private int m_ResetDrawArgsKernel;

		// Token: 0x04000132 RID: 306
		private int m_CopyInstancesKernel;

		// Token: 0x04000133 RID: 307
		private int m_CullInstancesKernel;

		// Token: 0x04000134 RID: 308
		private DebugRendererBatcherStats m_DebugStats;

		// Token: 0x04000135 RID: 309
		private InstanceCullerSplitDebugArray m_SplitDebugArray;

		// Token: 0x04000136 RID: 310
		private InstanceOcclusionEventDebugArray m_OcclusionEventDebugArray;

		// Token: 0x04000137 RID: 311
		private ProfilingSampler m_ProfilingSampleInstanceOcclusionTest;

		// Token: 0x04000138 RID: 312
		private NativeArray<InstanceOcclusionCullerShaderVariables> m_ShaderVariables;

		// Token: 0x04000139 RID: 313
		private ComputeBuffer m_ConstantBuffer;

		// Token: 0x0400013A RID: 314
		private CommandBuffer m_CommandBuffer;

		// Token: 0x02000046 RID: 70
		private static class ShaderIDs
		{
			// Token: 0x0400013B RID: 315
			public static readonly int InstanceOcclusionCullerShaderVariables = Shader.PropertyToID("InstanceOcclusionCullerShaderVariables");

			// Token: 0x0400013C RID: 316
			public static readonly int _DrawInfo = Shader.PropertyToID("_DrawInfo");

			// Token: 0x0400013D RID: 317
			public static readonly int _InstanceInfo = Shader.PropertyToID("_InstanceInfo");

			// Token: 0x0400013E RID: 318
			public static readonly int _DrawArgs = Shader.PropertyToID("_DrawArgs");

			// Token: 0x0400013F RID: 319
			public static readonly int _InstanceIndices = Shader.PropertyToID("_InstanceIndices");

			// Token: 0x04000140 RID: 320
			public static readonly int _InstanceDataBuffer = Shader.PropertyToID("_InstanceDataBuffer");

			// Token: 0x04000141 RID: 321
			public static readonly int _OccluderDepthPyramid = Shader.PropertyToID("_OccluderDepthPyramid");

			// Token: 0x04000142 RID: 322
			public static readonly int _OcclusionDebugCounters = Shader.PropertyToID("_OcclusionDebugCounters");
		}

		// Token: 0x02000047 RID: 71
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct SetupCullingJobInput : IJob
		{
			// Token: 0x06000129 RID: 297 RVA: 0x00008758 File Offset: 0x00006958
			public unsafe void Execute()
			{
				*this.receiverPlanes = ReceiverPlanes.Create(in *this.context, Allocator.TempJob);
				*this.receiverSphereCuller = ReceiverSphereCuller.Create(in *this.context, Allocator.TempJob);
				*this.frustumPlaneCuller = FrustumPlaneCuller.Create(in *this.context, this.receiverPlanes->planes.AsArray(), in *this.receiverSphereCuller, Allocator.TempJob);
				*this.screenRelativeMetric = LODGroupRenderingUtils.CalculateScreenRelativeMetric(this.context->lodParameters, this.lodBias);
			}

			// Token: 0x04000143 RID: 323
			public float lodBias;

			// Token: 0x04000144 RID: 324
			[NativeDisableUnsafePtrRestriction]
			public unsafe BatchCullingContext* context;

			// Token: 0x04000145 RID: 325
			[NativeDisableUnsafePtrRestriction]
			public unsafe ReceiverPlanes* receiverPlanes;

			// Token: 0x04000146 RID: 326
			[NativeDisableUnsafePtrRestriction]
			public unsafe ReceiverSphereCuller* receiverSphereCuller;

			// Token: 0x04000147 RID: 327
			[NativeDisableUnsafePtrRestriction]
			public unsafe FrustumPlaneCuller* frustumPlaneCuller;

			// Token: 0x04000148 RID: 328
			[NativeDisableUnsafePtrRestriction]
			public unsafe float* screenRelativeMetric;
		}

		// Token: 0x02000048 RID: 72
		private class InstanceOcclusionTestPassData
		{
			// Token: 0x04000149 RID: 329
			public OcclusionCullingSettings settings;

			// Token: 0x0400014A RID: 330
			public InstanceOcclusionTestSubviewSettings subviewSettings;

			// Token: 0x0400014B RID: 331
			public OccluderHandles occluderHandles;

			// Token: 0x0400014C RID: 332
			public IndirectBufferContextHandles bufferHandles;
		}
	}
}
