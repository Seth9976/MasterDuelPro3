using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.SceneManagement;

namespace UnityEngine.Rendering
{
	// Token: 0x0200001D RID: 29
	public class GPUResidentDrawer
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00004AEC File Offset: 0x00002CEC
		internal static bool IsProjectSupported()
		{
			string text;
			LogType __;
			return GPUResidentDrawer.IsProjectSupported(out text, out __);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004B02 File Offset: 0x00002D02
		internal static bool IsProjectSupported(out string message, out LogType severity)
		{
			message = string.Empty;
			severity = LogType.Log;
			if (BatchRendererGroup.BufferTarget != BatchBufferTarget.RawBuffer)
			{
				severity = LogType.Warning;
				message = GPUResidentDrawer.Strings.rawBufferNotSupportedByPlatform;
				return false;
			}
			return true;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004B24 File Offset: 0x00002D24
		internal static bool IsGPUResidentDrawerSupportedBySRP(GPUResidentDrawerSettings settings, out string message, out LogType severity)
		{
			message = string.Empty;
			severity = LogType.Log;
			if (settings.mode == GPUResidentDrawerMode.Disabled)
			{
				message = GPUResidentDrawer.Strings.drawerModeDisabled;
				return false;
			}
			if (GPUResidentDrawer.IsForcedOnViaCommandLine())
			{
				return true;
			}
			IGPUResidentRenderPipeline asset = GraphicsSettings.currentRenderPipeline as IGPUResidentRenderPipeline;
			if (asset == null)
			{
				message = GPUResidentDrawer.Strings.notGPUResidentRenderPipeline;
				severity = LogType.Warning;
				return false;
			}
			return asset.IsGPUResidentDrawerSupportedBySRP(out message, out severity) && GPUResidentDrawer.IsProjectSupported(out message, out severity);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004B82 File Offset: 0x00002D82
		internal static void LogMessage(string message, LogType severity)
		{
			switch (severity)
			{
			case LogType.Error:
			case LogType.Exception:
				Debug.LogError(message);
				return;
			case LogType.Assert:
			case LogType.Log:
				break;
			case LogType.Warning:
				Debug.LogWarning(message);
				break;
			default:
				return;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004BAC File Offset: 0x00002DAC
		internal static GPUResidentDrawer instance
		{
			get
			{
				return GPUResidentDrawer.s_Instance;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004BB3 File Offset: 0x00002DB3
		public static bool IsInstanceOcclusionCullingEnabled()
		{
			return GPUResidentDrawer.s_Instance != null && GPUResidentDrawer.s_Instance.settings.mode == GPUResidentDrawerMode.InstancedDrawing && GPUResidentDrawer.s_Instance.settings.enableOcclusionCulling;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004BE6 File Offset: 0x00002DE6
		public static void PostCullBeginCameraRendering(RenderRequestBatcherContext context)
		{
			GPUResidentDrawer gpuresidentDrawer = GPUResidentDrawer.s_Instance;
			if (gpuresidentDrawer == null)
			{
				return;
			}
			gpuresidentDrawer.batcher.PostCullBeginCameraRendering(context);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004BFD File Offset: 0x00002DFD
		public static void OnSetupAmbientProbe()
		{
			GPUResidentDrawer gpuresidentDrawer = GPUResidentDrawer.s_Instance;
			if (gpuresidentDrawer == null)
			{
				return;
			}
			gpuresidentDrawer.batcher.OnSetupAmbientProbe();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004C13 File Offset: 0x00002E13
		public static void InstanceOcclusionTest(RenderGraph renderGraph, in OcclusionCullingSettings settings, ReadOnlySpan<SubviewOcclusionTest> subviewOcclusionTests)
		{
			GPUResidentDrawer gpuresidentDrawer = GPUResidentDrawer.s_Instance;
			if (gpuresidentDrawer == null)
			{
				return;
			}
			gpuresidentDrawer.batcher.InstanceOcclusionTest(renderGraph, in settings, subviewOcclusionTests);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004C2C File Offset: 0x00002E2C
		public static void UpdateInstanceOccluders(RenderGraph renderGraph, in OccluderParameters occluderParameters, ReadOnlySpan<OccluderSubviewUpdate> occluderSubviewUpdates)
		{
			GPUResidentDrawer gpuresidentDrawer = GPUResidentDrawer.s_Instance;
			if (gpuresidentDrawer == null)
			{
				return;
			}
			gpuresidentDrawer.batcher.UpdateInstanceOccluders(renderGraph, in occluderParameters, occluderSubviewUpdates);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004C45 File Offset: 0x00002E45
		public static void ReinitializeIfNeeded()
		{
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004C47 File Offset: 0x00002E47
		public static void RenderDebugOcclusionTestOverlay(RenderGraph renderGraph, DebugDisplayGPUResidentDrawer debugSettings, int viewInstanceID, TextureHandle colorBuffer)
		{
			GPUResidentDrawer gpuresidentDrawer = GPUResidentDrawer.s_Instance;
			if (gpuresidentDrawer == null)
			{
				return;
			}
			gpuresidentDrawer.batcher.occlusionCullingCommon.RenderDebugOcclusionTestOverlay(renderGraph, debugSettings, viewInstanceID, colorBuffer);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004C66 File Offset: 0x00002E66
		public static void RenderDebugOccluderOverlay(RenderGraph renderGraph, DebugDisplayGPUResidentDrawer debugSettings, Vector2 screenPos, float maxHeight, TextureHandle colorBuffer)
		{
			GPUResidentDrawer gpuresidentDrawer = GPUResidentDrawer.s_Instance;
			if (gpuresidentDrawer == null)
			{
				return;
			}
			gpuresidentDrawer.batcher.occlusionCullingCommon.RenderDebugOccluderOverlay(renderGraph, debugSettings, screenPos, maxHeight, colorBuffer);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004C87 File Offset: 0x00002E87
		internal static DebugRendererBatcherStats GetDebugStats()
		{
			GPUResidentDrawer gpuresidentDrawer = GPUResidentDrawer.s_Instance;
			if (gpuresidentDrawer == null)
			{
				return null;
			}
			return gpuresidentDrawer.m_BatchersContext.debugStats;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004CA0 File Offset: 0x00002EA0
		private void InsertIntoPlayerLoop()
		{
			PlayerLoopSystem rootLoop = PlayerLoop.GetCurrentPlayerLoop();
			bool isAdded = false;
			for (int i = 0; i < rootLoop.subSystemList.Length; i++)
			{
				PlayerLoopSystem subSystem = rootLoop.subSystemList[i];
				if (!isAdded && subSystem.type == typeof(PostLateUpdate))
				{
					List<PlayerLoopSystem> subSubSystems = new List<PlayerLoopSystem>();
					foreach (PlayerLoopSystem subSubSystem in subSystem.subSystemList)
					{
						if (subSubSystem.type == typeof(PostLateUpdate.FinishFrameRendering))
						{
							PlayerLoopSystem s = default(PlayerLoopSystem);
							s.updateDelegate = (PlayerLoopSystem.UpdateFunction)Delegate.Combine(s.updateDelegate, new PlayerLoopSystem.UpdateFunction(GPUResidentDrawer.PostPostLateUpdateStatic));
							s.type = base.GetType();
							subSubSystems.Add(s);
							isAdded = true;
						}
						subSubSystems.Add(subSubSystem);
					}
					subSystem.subSystemList = subSubSystems.ToArray();
					rootLoop.subSystemList[i] = subSystem;
				}
			}
			PlayerLoop.SetPlayerLoop(rootLoop);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004DAC File Offset: 0x00002FAC
		private void RemoveFromPlayerLoop()
		{
			PlayerLoopSystem rootLoop = PlayerLoop.GetCurrentPlayerLoop();
			for (int i = 0; i < rootLoop.subSystemList.Length; i++)
			{
				PlayerLoopSystem subsystem = rootLoop.subSystemList[i];
				if (!(subsystem.type != typeof(PostLateUpdate)))
				{
					List<PlayerLoopSystem> newList = new List<PlayerLoopSystem>();
					foreach (PlayerLoopSystem subSubSystem in subsystem.subSystemList)
					{
						if (subSubSystem.type != base.GetType())
						{
							newList.Add(subSubSystem);
						}
					}
					subsystem.subSystemList = newList.ToArray();
					rootLoop.subSystemList[i] = subsystem;
				}
			}
			PlayerLoop.SetPlayerLoop(rootLoop);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004E64 File Offset: 0x00003064
		internal static bool IsEnabled()
		{
			return GPUResidentDrawer.s_Instance != null;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004E74 File Offset: 0x00003074
		internal static GPUResidentDrawerSettings GetGlobalSettingsFromRPAsset()
		{
			IGPUResidentRenderPipeline mbAsset = GraphicsSettings.currentRenderPipeline as IGPUResidentRenderPipeline;
			if (mbAsset == null)
			{
				return default(GPUResidentDrawerSettings);
			}
			GPUResidentDrawerSettings settings = mbAsset.gpuResidentDrawerSettings;
			if (GPUResidentDrawer.IsForcedOnViaCommandLine())
			{
				settings.mode = GPUResidentDrawerMode.InstancedDrawing;
			}
			if (GPUResidentDrawer.IsOcclusionForcedOnViaCommandLine())
			{
				settings.enableOcclusionCulling = true;
			}
			return settings;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00002B87 File Offset: 0x00000D87
		private static bool IsForcedOnViaCommandLine()
		{
			return false;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00002B87 File Offset: 0x00000D87
		private static bool IsOcclusionForcedOnViaCommandLine()
		{
			return false;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004EBF File Offset: 0x000030BF
		internal static void Reinitialize()
		{
			GPUResidentDrawer.Recreate(GPUResidentDrawer.GetGlobalSettingsFromRPAsset());
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004ECB File Offset: 0x000030CB
		private static void CleanUp()
		{
			if (GPUResidentDrawer.s_Instance == null)
			{
				return;
			}
			GPUResidentDrawer.s_Instance.Dispose();
			GPUResidentDrawer.s_Instance = null;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004EE8 File Offset: 0x000030E8
		private static void Recreate(GPUResidentDrawerSettings settings)
		{
			GPUResidentDrawer.CleanUp();
			string message;
			LogType severity;
			if (GPUResidentDrawer.IsGPUResidentDrawerSupportedBySRP(settings, out message, out severity))
			{
				GPUResidentDrawer.s_Instance = new GPUResidentDrawer(settings, 4096, 0);
				return;
			}
			GPUResidentDrawer.LogMessage(message, severity);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004F1F File Offset: 0x0000311F
		internal GPUResidentBatcher batcher
		{
			get
			{
				return this.m_Batcher;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004F27 File Offset: 0x00003127
		internal GPUResidentDrawerSettings settings
		{
			get
			{
				return this.m_Settings;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004F30 File Offset: 0x00003130
		private GPUResidentDrawer(GPUResidentDrawerSettings settings, int maxInstanceCount, int maxTreeInstanceCount)
		{
			GPUResidentDrawerResources resources = GraphicsSettings.GetRenderPipelineSettings<GPUResidentDrawerResources>();
			RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
			this.m_Settings = settings;
			RenderersBatchersContextDesc rbcDesc = RenderersBatchersContextDesc.NewDefault();
			rbcDesc.instanceNumInfo = new InstanceNumInfo(maxInstanceCount, maxTreeInstanceCount);
			rbcDesc.supportDitheringCrossFade = settings.supportDitheringCrossFade;
			rbcDesc.smallMeshScreenPercentage = settings.smallMeshScreenPercentage;
			rbcDesc.enableBoundingSpheresInstanceData = settings.enableOcclusionCulling;
			rbcDesc.enableCullerDebugStats = true;
			InstanceCullingBatcherDesc instanceCullingBatcherDesc = InstanceCullingBatcherDesc.NewDefault();
			this.m_GPUDrivenProcessor = new GPUDrivenProcessor();
			this.m_BatchersContext = new RenderersBatchersContext(in rbcDesc, this.m_GPUDrivenProcessor, resources);
			this.m_Batcher = new GPUResidentBatcher(this.m_BatchersContext, instanceCullingBatcherDesc, this.m_GPUDrivenProcessor);
			this.m_Dispatcher = new ObjectDispatcher();
			this.m_Dispatcher.EnableTypeTracking<LODGroup>(ObjectDispatcher.TypeTrackingFlags.SceneObjects);
			this.m_Dispatcher.EnableTypeTracking<Mesh>(ObjectDispatcher.TypeTrackingFlags.Default);
			this.m_Dispatcher.EnableTypeTracking<Material>(ObjectDispatcher.TypeTrackingFlags.Default);
			this.m_Dispatcher.EnableTransformTracking<LODGroup>(ObjectDispatcher.TransformTrackingType.GlobalTRS);
			this.m_Dispatcher.EnableTypeTracking<MeshRenderer>(ObjectDispatcher.TypeTrackingFlags.SceneObjects);
			this.m_Dispatcher.EnableTransformTracking<MeshRenderer>(ObjectDispatcher.TransformTrackingType.GlobalTRS);
			SceneManager.sceneLoaded += this.OnSceneLoaded;
			RenderPipelineManager.beginContextRendering += this.OnBeginContextRendering;
			RenderPipelineManager.endContextRendering += this.OnEndContextRendering;
			RenderPipelineManager.beginCameraRendering += this.OnBeginCameraRendering;
			RenderPipelineManager.endCameraRendering += this.OnEndCameraRendering;
			Shader.EnableKeyword("USE_LEGACY_LIGHTMAPS");
			this.InsertIntoPlayerLoop();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000509C File Offset: 0x0000329C
		private void Dispose()
		{
			SceneManager.sceneLoaded -= this.OnSceneLoaded;
			RenderPipelineManager.beginContextRendering -= this.OnBeginContextRendering;
			RenderPipelineManager.endContextRendering -= this.OnEndContextRendering;
			RenderPipelineManager.beginCameraRendering -= this.OnBeginCameraRendering;
			RenderPipelineManager.endCameraRendering -= this.OnEndCameraRendering;
			this.RemoveFromPlayerLoop();
			Shader.DisableKeyword("USE_LEGACY_LIGHTMAPS");
			this.m_Dispatcher.Dispose();
			this.m_Dispatcher = null;
			GPUResidentDrawer.s_Instance = null;
			GPUResidentBatcher batcher = this.m_Batcher;
			if (batcher != null)
			{
				batcher.Dispose();
			}
			this.m_BatchersContext.Dispose();
			this.m_GPUDrivenProcessor.Dispose();
			this.m_ContextIntPtr = IntPtr.Zero;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005158 File Offset: 0x00003358
		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (mode == LoadSceneMode.Additive)
			{
				this.m_BatchersContext.UpdateAmbientProbeAndGpuBuffer(true);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000516A File Offset: 0x0000336A
		private static void PostPostLateUpdateStatic()
		{
			GPUResidentDrawer gpuresidentDrawer = GPUResidentDrawer.s_Instance;
			if (gpuresidentDrawer == null)
			{
				return;
			}
			gpuresidentDrawer.PostPostLateUpdate();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000517B File Offset: 0x0000337B
		private void OnBeginContextRendering(ScriptableRenderContext context, List<Camera> cameras)
		{
			if (GPUResidentDrawer.s_Instance == null)
			{
				return;
			}
			if (this.m_ContextIntPtr == IntPtr.Zero)
			{
				this.m_ContextIntPtr = context.Internal_GetPtr();
				this.m_Batcher.OnBeginContextRendering();
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000051AF File Offset: 0x000033AF
		private void OnEndContextRendering(ScriptableRenderContext context, List<Camera> cameras)
		{
			if (GPUResidentDrawer.s_Instance == null)
			{
				return;
			}
			if (this.m_ContextIntPtr == context.Internal_GetPtr())
			{
				this.m_ContextIntPtr = IntPtr.Zero;
				this.m_Batcher.OnEndContextRendering();
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000051E3 File Offset: 0x000033E3
		private void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			this.m_Batcher.OnBeginCameraRendering(camera);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000051F1 File Offset: 0x000033F1
		private void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			this.m_Batcher.OnEndCameraRendering(camera);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005200 File Offset: 0x00003400
		private void PostPostLateUpdate()
		{
			this.m_BatchersContext.UpdateAmbientProbeAndGpuBuffer(false);
			TransformDispatchData lodGroupTransformData = this.m_Dispatcher.GetTransformChangesAndClear<LODGroup>(ObjectDispatcher.TransformTrackingType.GlobalTRS, Allocator.TempJob);
			TypeDispatchData lodGroupData = this.m_Dispatcher.GetTypeChangesAndClear<LODGroup>(Allocator.TempJob, false, true);
			TypeDispatchData meshDataSorted = this.m_Dispatcher.GetTypeChangesAndClear<Mesh>(Allocator.TempJob, true, true);
			TypeDispatchData materialData = this.m_Dispatcher.GetTypeChangesAndClear<Material>(Allocator.TempJob, false, false);
			TypeDispatchData rendererData = this.m_Dispatcher.GetTypeChangesAndClear<MeshRenderer>(Allocator.TempJob, false, true);
			NativeList<int> unsupportedMaterials = this.FindUnsupportedMaterials(materialData.changedID);
			NativeList<int> unsupportedRenderers = this.FindUnsupportedRenderers(unsupportedMaterials.AsArray());
			this.ProcessMaterials(materialData.destroyedID, unsupportedMaterials.AsArray());
			this.ProcessMeshes(meshDataSorted.destroyedID);
			this.ProcessLODGroups(lodGroupData.changedID, lodGroupData.destroyedID, lodGroupTransformData.transformedID);
			this.ProcessRenderers(rendererData, unsupportedRenderers.AsArray());
			lodGroupTransformData.Dispose();
			lodGroupData.Dispose();
			meshDataSorted.Dispose();
			materialData.Dispose();
			rendererData.Dispose();
			unsupportedMaterials.Dispose();
			unsupportedRenderers.Dispose();
			this.m_BatchersContext.UpdateInstanceMotions();
			this.m_Batcher.UpdateFrame();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000530E File Offset: 0x0000350E
		private void ProcessMaterials(NativeArray<int> destroyedID, NativeArray<int> unsupportedMaterials)
		{
			if (destroyedID.Length > 0)
			{
				this.m_Batcher.DestroyMaterials(destroyedID);
			}
			if (unsupportedMaterials.Length > 0)
			{
				this.m_Batcher.DestroyMaterials(unsupportedMaterials);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000533C File Offset: 0x0000353C
		private void ProcessMeshes(NativeArray<int> destroyedID)
		{
			if (destroyedID.Length == 0)
			{
				return;
			}
			NativeList<InstanceHandle> destroyedMeshInstances = new NativeList<InstanceHandle>(Allocator.TempJob);
			this.ScheduleQueryMeshInstancesJob(destroyedID, destroyedMeshInstances).Complete();
			this.m_Batcher.DestroyInstances(destroyedMeshInstances.AsArray());
			destroyedMeshInstances.Dispose();
			this.m_Batcher.DestroyMeshes(destroyedID);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005395 File Offset: 0x00003595
		private void ProcessLODGroups(NativeArray<int> changedID, NativeArray<int> destroyed, NativeArray<int> transformedID)
		{
			this.m_BatchersContext.DestroyLODGroups(destroyed);
			this.m_BatchersContext.UpdateLODGroups(changedID);
			this.m_BatchersContext.TransformLODGroups(transformedID);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000053BC File Offset: 0x000035BC
		private void ProcessRenderers(TypeDispatchData rendererChanges, NativeArray<int> unsupportedRenderers)
		{
			NativeArray<InstanceHandle> changedInstances = new NativeArray<InstanceHandle>(rendererChanges.changedID.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			this.ScheduleQueryRendererGroupInstancesJob(rendererChanges.changedID, changedInstances).Complete();
			this.m_Batcher.DestroyInstances(changedInstances);
			changedInstances.Dispose();
			this.m_Batcher.UpdateRenderers(rendererChanges.changedID);
			this.FreeRendererGroupInstances(rendererChanges.destroyedID, unsupportedRenderers);
			TransformDispatchData transformChanges = this.m_Dispatcher.GetTransformChangesAndClear<MeshRenderer>(ObjectDispatcher.TransformTrackingType.GlobalTRS, Allocator.TempJob);
			NativeArray<InstanceHandle> transformedInstances = new NativeArray<InstanceHandle>(transformChanges.transformedID.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			this.ScheduleQueryRendererGroupInstancesJob(transformChanges.transformedID, transformedInstances).Complete();
			this.TransformInstances(transformedInstances, transformChanges.localToWorldMatrices);
			transformedInstances.Dispose();
			transformChanges.Dispose();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005477 File Offset: 0x00003677
		private void TransformInstances(NativeArray<InstanceHandle> instances, NativeArray<Matrix4x4> localToWorldMatrices)
		{
			this.m_BatchersContext.UpdateInstanceTransforms(instances, localToWorldMatrices);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005486 File Offset: 0x00003686
		private void FreeInstances(NativeArray<InstanceHandle> instances)
		{
			this.m_Batcher.DestroyInstances(instances);
			this.m_BatchersContext.FreeInstances(instances);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000054A0 File Offset: 0x000036A0
		private void FreeRendererGroupInstances(NativeArray<int> rendererGroupIDs, NativeArray<int> unsupportedRendererGroupIDs)
		{
			this.m_Batcher.FreeRendererGroupInstances(rendererGroupIDs);
			if (unsupportedRendererGroupIDs.Length > 0)
			{
				this.m_Batcher.FreeRendererGroupInstances(unsupportedRendererGroupIDs);
				this.m_GPUDrivenProcessor.DisableGPUDrivenRendering(in unsupportedRendererGroupIDs);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000054D6 File Offset: 0x000036D6
		private InstanceHandle AppendNewInstance(int rendererGroupID, in Matrix4x4 instanceTransform)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000054DD File Offset: 0x000036DD
		private JobHandle ScheduleQueryRendererGroupInstancesJob(NativeArray<int> rendererGroupIDs, NativeArray<InstanceHandle> instances)
		{
			return this.m_BatchersContext.ScheduleQueryRendererGroupInstancesJob(rendererGroupIDs, instances);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000054EC File Offset: 0x000036EC
		private JobHandle ScheduleQueryRendererGroupInstancesJob(NativeArray<int> rendererGroupIDs, NativeList<InstanceHandle> instances)
		{
			return this.m_BatchersContext.ScheduleQueryRendererGroupInstancesJob(rendererGroupIDs, instances);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000054FB File Offset: 0x000036FB
		private JobHandle ScheduleQueryRendererGroupInstancesJob(NativeArray<int> rendererGroupIDs, NativeArray<int> instancesOffset, NativeArray<int> instancesCount, NativeList<InstanceHandle> instances)
		{
			return this.m_BatchersContext.ScheduleQueryRendererGroupInstancesJob(rendererGroupIDs, instancesOffset, instancesCount, instances);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000550D File Offset: 0x0000370D
		private JobHandle ScheduleQueryMeshInstancesJob(NativeArray<int> sortedMeshIDs, NativeList<InstanceHandle> instances)
		{
			return this.m_BatchersContext.ScheduleQueryMeshInstancesJob(sortedMeshIDs, instances);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000551C File Offset: 0x0000371C
		private NativeList<int> FindUnsupportedMaterials(NativeArray<int> changedMaterialIDs)
		{
			NativeList<int> unsupportedMaterials = new NativeList<int>(Allocator.TempJob);
			if (changedMaterialIDs.Length > 0)
			{
				new GPUResidentDrawer.FindUnsupportedMaterialsJob
				{
					changedMaterialIDs = changedMaterialIDs,
					batchMaterialHash = this.m_Batcher.instanceCullingBatcher.batchMaterialHash,
					unsupportedMaterialIDs = unsupportedMaterials
				}.Run<GPUResidentDrawer.FindUnsupportedMaterialsJob>();
			}
			return unsupportedMaterials;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005578 File Offset: 0x00003778
		private NativeList<int> FindUnsupportedRenderers(NativeArray<int> unsupportedMaterials)
		{
			NativeList<int> unsupportedRenderers = new NativeList<int>(Allocator.TempJob);
			if (unsupportedMaterials.Length > 0)
			{
				new GPUResidentDrawer.FindUnsupportedRenderersJob
				{
					unsupportedMaterials = unsupportedMaterials.AsReadOnly(),
					materialIDArrays = this.m_BatchersContext.sharedInstanceData.materialIDArrays,
					rendererGroups = this.m_BatchersContext.sharedInstanceData.rendererGroupIDs,
					unsupportedRenderers = unsupportedRenderers
				}.Run<GPUResidentDrawer.FindUnsupportedRenderersJob>();
			}
			return unsupportedRenderers;
		}

		// Token: 0x0400004B RID: 75
		private static GPUResidentDrawer s_Instance;

		// Token: 0x0400004C RID: 76
		private IntPtr m_ContextIntPtr = IntPtr.Zero;

		// Token: 0x0400004D RID: 77
		private GPUResidentDrawerSettings m_Settings;

		// Token: 0x0400004E RID: 78
		private GPUDrivenProcessor m_GPUDrivenProcessor;

		// Token: 0x0400004F RID: 79
		private RenderersBatchersContext m_BatchersContext;

		// Token: 0x04000050 RID: 80
		private GPUResidentBatcher m_Batcher;

		// Token: 0x04000051 RID: 81
		private ObjectDispatcher m_Dispatcher;

		// Token: 0x0200001E RID: 30
		private static class Strings
		{
			// Token: 0x04000052 RID: 82
			public static readonly string drawerModeDisabled = "GPUResidentDrawer Drawer mode is disabled. Enable it on your current RenderPipelineAsset";

			// Token: 0x04000053 RID: 83
			public static readonly string allowInEditModeDisabled = "GPUResidentDrawer The current mode does not allow the resident drawer. Check setting Allow In Edit Mode";

			// Token: 0x04000054 RID: 84
			public static readonly string notGPUResidentRenderPipeline = "GPUResidentDrawer Disabled due to current render pipeline not being of type IGPUResidentRenderPipeline";

			// Token: 0x04000055 RID: 85
			public static readonly string rawBufferNotSupportedByPlatform = string.Format("{0} The current platform does not support {1}", "GPUResidentDrawer", BatchBufferTarget.RawBuffer.GetType());

			// Token: 0x04000056 RID: 86
			public static readonly string kernelNotPresent = "GPUResidentDrawer Kernel not present, please ensure the player settings includes a supported graphics API.";

			// Token: 0x04000057 RID: 87
			public static readonly string batchRendererGroupShaderStrippingModeInvalid = "GPUResidentDrawer \"BatchRendererGroup Variants\" setting must be \"Keep All\".  The current setting will cause errors when building a player because all DOTS instancing shaders will be stripped To fix, modify Graphics settings and set \"BatchRendererGroup Variants\" to \"Keep All\".";
		}

		// Token: 0x0200001F RID: 31
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct FindUnsupportedMaterialsJob : IJob
		{
			// Token: 0x060000D5 RID: 213 RVA: 0x00005650 File Offset: 0x00003850
			public void Execute()
			{
				NativeList<int> changedUsedMaterialIDs = new NativeList<int>(4, Allocator.Temp);
				foreach (int materialID in this.changedMaterialIDs)
				{
					if (this.batchMaterialHash.ContainsKey(materialID))
					{
						changedUsedMaterialIDs.Add(in materialID);
					}
				}
				if (changedUsedMaterialIDs.IsEmpty)
				{
					return;
				}
				this.unsupportedMaterialIDs.Resize(changedUsedMaterialIDs.Length, NativeArrayOptions.UninitializedMemory);
				int unsupportedMaterialCount = GPUDrivenProcessor.FindUnsupportedMaterialIDs(changedUsedMaterialIDs.AsArray(), this.unsupportedMaterialIDs.AsArray());
				this.unsupportedMaterialIDs.Resize(unsupportedMaterialCount, NativeArrayOptions.ClearMemory);
			}

			// Token: 0x04000058 RID: 88
			[ReadOnly]
			public NativeParallelHashMap<int, BatchMaterialID> batchMaterialHash;

			// Token: 0x04000059 RID: 89
			[ReadOnly]
			public NativeArray<int> changedMaterialIDs;

			// Token: 0x0400005A RID: 90
			public NativeList<int> unsupportedMaterialIDs;
		}

		// Token: 0x02000020 RID: 32
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		private struct FindUnsupportedRenderersJob : IJob
		{
			// Token: 0x060000D6 RID: 214 RVA: 0x00005704 File Offset: 0x00003904
			public void Execute()
			{
				if (this.unsupportedMaterials.Length == 0)
				{
					return;
				}
				for (int arrayIndex = 0; arrayIndex < this.materialIDArrays.Length; arrayIndex++)
				{
					SmallIntegerArray materialIDs = this.materialIDArrays[arrayIndex];
					int rendererID = this.rendererGroups[arrayIndex];
					for (int i = 0; i < materialIDs.Length; i++)
					{
						int materialID = materialIDs[i];
						if (this.unsupportedMaterials.Contains(materialID))
						{
							this.unsupportedRenderers.Add(in rendererID);
							break;
						}
					}
				}
			}

			// Token: 0x0400005B RID: 91
			[ReadOnly]
			public NativeArray<int>.ReadOnly unsupportedMaterials;

			// Token: 0x0400005C RID: 92
			[ReadOnly]
			public NativeArray<SmallIntegerArray>.ReadOnly materialIDArrays;

			// Token: 0x0400005D RID: 93
			[ReadOnly]
			public NativeArray<int>.ReadOnly rendererGroups;

			// Token: 0x0400005E RID: 94
			public NativeList<int> unsupportedRenderers;
		}
	}
}
