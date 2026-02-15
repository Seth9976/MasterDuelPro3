using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.RenderPipelines.Core.Runtime.Shared;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001B9 RID: 441
	public sealed class UniversalRenderPipeline : RenderPipeline
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0002EAEF File Offset: 0x0002CCEF
		public static float maxShadowBias
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x0002EAF6 File Offset: 0x0002CCF6
		public static float minRenderScale
		{
			get
			{
				return 0.1f;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0002EAFD File Offset: 0x0002CCFD
		public static float maxRenderScale
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x0002EB04 File Offset: 0x0002CD04
		public static int maxNumIterationsEnclosingSphere
		{
			get
			{
				return 1000;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0002EB0B File Offset: 0x0002CD0B
		public static int maxPerObjectLights
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x0002EB10 File Offset: 0x0002CD10
		public static int maxVisibleAdditionalLights
		{
			get
			{
				bool isMobileOrMobileBuildTarget = PlatformAutoDetect.isShaderAPIMobileDefined;
				if (isMobileOrMobileBuildTarget && SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3 && Graphics.minOpenGLESVersion <= OpenGLESVersion.OpenGLES30)
				{
					return 16;
				}
				if (!isMobileOrMobileBuildTarget && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLCore && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES3 && SystemInfo.graphicsDeviceType != GraphicsDeviceType.WebGPU)
				{
					return 256;
				}
				return 32;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0002EB60 File Offset: 0x0002CD60
		internal static int lightsPerTile
		{
			get
			{
				return (UniversalRenderPipeline.maxVisibleAdditionalLights + 31) / 32 * 32;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x0002EB70 File Offset: 0x0002CD70
		internal static int maxZBinWords
		{
			get
			{
				return 4096;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0002EB77 File Offset: 0x0002CD77
		internal static int maxTileWords
		{
			get
			{
				return ((UniversalRenderPipeline.maxVisibleAdditionalLights <= 32) ? 1024 : 4096) * 4;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0002EB90 File Offset: 0x0002CD90
		internal static int maxVisibleReflectionProbes
		{
			get
			{
				return Math.Min(UniversalRenderPipeline.maxVisibleAdditionalLights, 64);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0002EB9E File Offset: 0x0002CD9E
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x0002EBA6 File Offset: 0x0002CDA6
		internal UniversalRenderPipelineRuntimeTextures runtimeTextures { get; private set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0002EBAF File Offset: 0x0002CDAF
		public override RenderPipelineGlobalSettings defaultSettings
		{
			get
			{
				return this.m_GlobalSettings;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x0002EBB7 File Offset: 0x0002CDB7
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x0002EBBE File Offset: 0x0002CDBE
		internal static bool canOptimizeScreenMSAASamples { get; private set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0002EBC6 File Offset: 0x0002CDC6
		// (set) Token: 0x06000985 RID: 2437 RVA: 0x0002EBCD File Offset: 0x0002CDCD
		internal static int startFrameScreenMSAASamples { get; private set; }

		// Token: 0x06000986 RID: 2438 RVA: 0x0002EBD5 File Offset: 0x0002CDD5
		public override string ToString()
		{
			UniversalRenderPipelineAsset universalRenderPipelineAsset = this.pipelineAsset;
			if (universalRenderPipelineAsset == null)
			{
				return null;
			}
			return universalRenderPipelineAsset.ToString();
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0002EBE8 File Offset: 0x0002CDE8
		public UniversalRenderPipeline(UniversalRenderPipelineAsset asset)
		{
			this.pipelineAsset = asset;
			this.m_GlobalSettings = RenderPipelineGlobalSettings<UniversalRenderPipelineGlobalSettings, UniversalRenderPipeline>.instance;
			this.runtimeTextures = GraphicsSettings.GetRenderPipelineSettings<UniversalRenderPipelineRuntimeTextures>();
			UniversalRenderPipelineRuntimeShaders shaders = GraphicsSettings.GetRenderPipelineSettings<UniversalRenderPipelineRuntimeShaders>();
			Blitter.Initialize(shaders.coreBlitPS, shaders.coreBlitColorAndDepthPS);
			UniversalRenderPipeline.SetSupportedRenderingFeatures(this.pipelineAsset);
			RTHandles.Initialize(Screen.width, Screen.height);
			ShaderGlobalKeywords.InitializeShaderGlobalKeywords();
			GraphicsSettings.useScriptableRenderPipelineBatching = asset.useSRPBatcher;
			if (((QualitySettings.antiAliasing > 0) ? QualitySettings.antiAliasing : 1) != asset.msaaSampleCount)
			{
				QualitySettings.antiAliasing = asset.msaaSampleCount;
			}
			URPDefaultVolumeProfileSettings defaultVolumeProfileSettings = GraphicsSettings.GetRenderPipelineSettings<URPDefaultVolumeProfileSettings>();
			VolumeManager.instance.Initialize(defaultVolumeProfileSettings.volumeProfile, asset.volumeProfile);
			XRSystem.SetDisplayMSAASamples((MSAASamples)Mathf.Clamp(Mathf.NextPowerOfTwo(QualitySettings.antiAliasing), 1, 8));
			XRSystem.SetRenderScale(asset.renderScale);
			Lightmapping.SetDelegate(UniversalRenderPipeline.lightsDelegate);
			CameraCaptureBridge.enabled = true;
			RenderingUtils.ClearSystemInfoCache();
			DecalProjector.defaultMaterial = asset.decalMaterial;
			UniversalRenderPipeline.s_RenderGraph = new RenderGraph("URPRenderGraph");
			UniversalRenderPipeline.useRenderGraph = !GraphicsSettings.GetRenderPipelineSettings<RenderGraphSettings>().enableRenderCompatibilityMode;
			Debug.Log("RenderGraph is now " + (UniversalRenderPipeline.useRenderGraph ? "enabled" : "disabled") + ".");
			UniversalRenderPipeline.s_RTHandlePool = new RTHandleResourcePool();
			DebugManager.instance.RefreshEditor();
			QualitySettings.enableLODCrossFade = asset.enableLODCrossFade;
			this.apvIsEnabled = asset != null && asset.lightProbeSystem == LightProbeSystem.ProbeVolumes;
			SupportedRenderingFeatures.active.overridesLightProbeSystem = this.apvIsEnabled;
			SupportedRenderingFeatures.active.skyOcclusion = this.apvIsEnabled;
			if (this.apvIsEnabled)
			{
				ProbeReferenceVolume instance = ProbeReferenceVolume.instance;
				ProbeVolumeSystemParameters probeVolumeSystemParameters = default(ProbeVolumeSystemParameters);
				probeVolumeSystemParameters.memoryBudget = asset.probeVolumeMemoryBudget;
				probeVolumeSystemParameters.blendingMemoryBudget = asset.probeVolumeBlendingMemoryBudget;
				probeVolumeSystemParameters.shBands = asset.probeVolumeSHBands;
				probeVolumeSystemParameters.supportGPUStreaming = asset.supportProbeVolumeGPUStreaming;
				probeVolumeSystemParameters.supportDiskStreaming = asset.supportProbeVolumeDiskStreaming;
				probeVolumeSystemParameters.supportScenarios = asset.supportProbeVolumeScenarios;
				probeVolumeSystemParameters.supportScenarioBlending = asset.supportProbeVolumeScenarioBlending;
				probeVolumeSystemParameters.sceneData = this.m_GlobalSettings.GetOrCreateAPVSceneData();
				instance.Initialize(in probeVolumeSystemParameters);
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0002EE44 File Offset: 0x0002D044
		protected override void Dispose(bool disposing)
		{
			if (this.apvIsEnabled)
			{
				ProbeReferenceVolume.instance.Cleanup();
			}
			Blitter.Cleanup();
			base.Dispose(disposing);
			this.pipelineAsset.DestroyRenderers();
			SupportedRenderingFeatures.active = new SupportedRenderingFeatures();
			ShaderData.instance.Dispose();
			XRSystem.Dispose();
			UniversalRenderPipeline.s_RenderGraph.Cleanup();
			UniversalRenderPipeline.s_RenderGraph = null;
			UniversalRenderPipeline.s_RTHandlePool.Cleanup();
			UniversalRenderPipeline.s_RTHandlePool = null;
			Lightmapping.ResetDelegate();
			CameraCaptureBridge.enabled = false;
			ConstantBuffer.ReleaseAll();
			VolumeManager.instance.Deinitialize();
			this.DisposeAdditionalCameraData();
			AdditionalLightsShadowAtlasLayout.ClearStaticCaches();
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0002EED8 File Offset: 0x0002D0D8
		private void DisposeAdditionalCameraData()
		{
			Camera[] allCameras = Camera.allCameras;
			for (int i = 0; i < allCameras.Length; i++)
			{
				UniversalAdditionalCameraData additionalCameraData;
				if (allCameras[i].TryGetComponent<UniversalAdditionalCameraData>(out additionalCameraData))
				{
					additionalCameraData.historyManager.Dispose();
				}
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0002EF10 File Offset: 0x0002D110
		protected override void Render(ScriptableRenderContext renderContext, Camera[] cameras)
		{
			this.Render(renderContext, new List<Camera>(cameras));
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0002EF20 File Offset: 0x0002D120
		protected override void Render(ScriptableRenderContext renderContext, List<Camera> cameras)
		{
			this.SetHDRState(cameras);
			int count = cameras.Count;
			UniversalRenderPipeline.AdjustUIOverlayOwnership(count);
			UniversalRenderPipeline.SetupScreenMSAASamplesState(count);
			GPUResidentDrawer.ReinitializeIfNeeded();
			using (new ProfilingScope(ProfilingSampler.Get<URPProfileId>(URPProfileId.UniversalRenderTotal)))
			{
				using (new UniversalRenderPipeline.ContextRenderingScope(renderContext, cameras))
				{
					GraphicsSettings.lightsUseLinearIntensity = QualitySettings.activeColorSpace == ColorSpace.Linear;
					GraphicsSettings.lightsUseColorTemperature = true;
					this.SetupPerFrameShaderConstants();
					XRSystem.SetDisplayMSAASamples((MSAASamples)UniversalRenderPipeline.asset.msaaSampleCount);
					RTHandles.SetHardwareDynamicResolutionState(true);
					this.SortCameras(cameras);
					for (int i = 0; i < cameras.Count; i++)
					{
						Camera camera = cameras[i];
						if (UniversalRenderPipeline.IsGameCamera(camera))
						{
							UniversalRenderPipeline.RenderCameraStack(renderContext, camera);
						}
						else
						{
							using (new UniversalRenderPipeline.CameraRenderingScope(renderContext, camera))
							{
								UniversalRenderPipeline.UpdateVolumeFramework(camera, null);
								UniversalRenderPipeline.RenderSingleCameraInternal(renderContext, camera);
							}
						}
					}
					UniversalRenderPipeline.s_RenderGraph.EndFrame();
					UniversalRenderPipeline.s_RTHandlePool.PurgeUnusedResources(Time.frameCount);
				}
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0002F044 File Offset: 0x0002D244
		protected override bool IsRenderRequestSupported<RequestData>(Camera camera, RequestData data)
		{
			return data is RenderPipeline.StandardRequest || data is UniversalRenderPipeline.SingleCameraRequest;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0002F068 File Offset: 0x0002D268
		protected override void ProcessRenderRequests<RequestData>(ScriptableRenderContext context, Camera camera, RequestData renderRequest)
		{
			RenderPipeline.StandardRequest standardRequest = renderRequest as RenderPipeline.StandardRequest;
			UniversalRenderPipeline.SingleCameraRequest singleRequest = renderRequest as UniversalRenderPipeline.SingleCameraRequest;
			if (standardRequest == null && singleRequest == null)
			{
				Debug.LogWarning("RenderRequest type: " + typeof(RequestData).FullName + " is either invalid or unsupported by the current pipeline");
				return;
			}
			RenderTexture destination = ((standardRequest != null) ? standardRequest.destination : singleRequest.destination);
			if (destination == null)
			{
				Debug.LogError("RenderRequest has no destination texture, set one before sending request");
				return;
			}
			int mipLevel = ((standardRequest != null) ? standardRequest.mipLevel : singleRequest.mipLevel);
			int slice = ((standardRequest != null) ? standardRequest.slice : singleRequest.slice);
			int face = (int)((standardRequest != null) ? standardRequest.face : singleRequest.face);
			RenderTexture originalTarget = camera.targetTexture;
			RenderTexture temporaryRT = null;
			RenderTextureDescriptor RTDesc = destination.descriptor;
			if (destination.dimension == TextureDimension.Cube)
			{
				RTDesc = default(RenderTextureDescriptor);
			}
			RTDesc.colorFormat = destination.format;
			RTDesc.volumeDepth = 1;
			RTDesc.msaaSamples = destination.descriptor.msaaSamples;
			RTDesc.dimension = TextureDimension.Tex2D;
			RTDesc.width = destination.width / (int)Math.Pow(2.0, (double)mipLevel);
			RTDesc.height = destination.height / (int)Math.Pow(2.0, (double)mipLevel);
			RTDesc.width = Mathf.Max(1, RTDesc.width);
			RTDesc.height = Mathf.Max(1, RTDesc.height);
			if (destination.dimension != TextureDimension.Tex2D || mipLevel != 0)
			{
				temporaryRT = RenderTexture.GetTemporary(RTDesc);
			}
			camera.targetTexture = (temporaryRT ? temporaryRT : destination);
			if (standardRequest != null)
			{
				this.Render(context, new Camera[] { camera });
			}
			else
			{
				List<Camera> tmp;
				using (ListPool<Camera>.Get(out tmp))
				{
					tmp.Add(camera);
					using (new UniversalRenderPipeline.ContextRenderingScope(context, tmp))
					{
						using (new UniversalRenderPipeline.CameraRenderingScope(context, camera))
						{
							UniversalAdditionalCameraData additionalCameraData;
							camera.gameObject.TryGetComponent<UniversalAdditionalCameraData>(out additionalCameraData);
							UniversalRenderPipeline.RenderSingleCameraInternal(context, camera, ref additionalCameraData);
						}
					}
				}
			}
			if (temporaryRT)
			{
				bool isCopySupported = false;
				switch (destination.dimension)
				{
				case TextureDimension.Tex2D:
					if ((SystemInfo.copyTextureSupport & CopyTextureSupport.Basic) != CopyTextureSupport.None)
					{
						isCopySupported = true;
						Graphics.CopyTexture(temporaryRT, 0, 0, destination, 0, mipLevel);
					}
					break;
				case TextureDimension.Tex3D:
					if ((SystemInfo.copyTextureSupport & CopyTextureSupport.DifferentTypes) != CopyTextureSupport.None)
					{
						isCopySupported = true;
						Graphics.CopyTexture(temporaryRT, 0, 0, destination, slice, mipLevel);
					}
					break;
				case TextureDimension.Cube:
					if ((SystemInfo.copyTextureSupport & CopyTextureSupport.DifferentTypes) != CopyTextureSupport.None)
					{
						isCopySupported = true;
						Graphics.CopyTexture(temporaryRT, 0, 0, destination, face, mipLevel);
					}
					break;
				case TextureDimension.Tex2DArray:
					if ((SystemInfo.copyTextureSupport & CopyTextureSupport.DifferentTypes) != CopyTextureSupport.None)
					{
						isCopySupported = true;
						Graphics.CopyTexture(temporaryRT, 0, 0, destination, slice, mipLevel);
					}
					break;
				case TextureDimension.CubeArray:
					if ((SystemInfo.copyTextureSupport & CopyTextureSupport.DifferentTypes) != CopyTextureSupport.None)
					{
						isCopySupported = true;
						Graphics.CopyTexture(temporaryRT, 0, 0, destination, face + slice * 6, mipLevel);
					}
					break;
				}
				if (!isCopySupported)
				{
					Debug.LogError("RenderRequest cannot have destination texture of this format: " + Enum.GetName(typeof(TextureDimension), destination.dimension));
				}
			}
			camera.targetTexture = originalTarget;
			Graphics.SetRenderTarget(originalTarget);
			RenderTexture.ReleaseTemporary(temporaryRT);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0002F3AC File Offset: 0x0002D5AC
		[Obsolete("RenderSingleCamera is obsolete, please use RenderPipeline.SubmitRenderRequest with UniversalRenderer.SingleCameraRequest as RequestData type")]
		public static void RenderSingleCamera(ScriptableRenderContext context, Camera camera)
		{
			UniversalRenderPipeline.RenderSingleCameraInternal(context, camera);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0002F3B8 File Offset: 0x0002D5B8
		internal static void RenderSingleCameraInternal(ScriptableRenderContext context, Camera camera)
		{
			UniversalAdditionalCameraData additionalCameraData = null;
			if (UniversalRenderPipeline.IsGameCamera(camera))
			{
				camera.gameObject.TryGetComponent<UniversalAdditionalCameraData>(out additionalCameraData);
			}
			UniversalRenderPipeline.RenderSingleCameraInternal(context, camera, ref additionalCameraData);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0002F3E8 File Offset: 0x0002D5E8
		internal static void RenderSingleCameraInternal(ScriptableRenderContext context, Camera camera, ref UniversalAdditionalCameraData additionalCameraData)
		{
			if (additionalCameraData != null && additionalCameraData.renderType != CameraRenderType.Base)
			{
				Debug.LogWarning("Only Base cameras can be rendered with standalone RenderSingleCamera. Camera will be skipped.");
				return;
			}
			if (camera.targetTexture.width == 0 || camera.targetTexture.height == 0 || camera.pixelWidth == 0 || camera.pixelHeight == 0)
			{
				Debug.LogWarning(string.Format("Camera '{0}' has an invalid render target size (width: {1}, height: {2}) or pixel dimensions (width: {3}, height: {4}). Camera will be skipped.", new object[]
				{
					camera.name,
					camera.targetTexture.width,
					camera.targetTexture.height,
					camera.pixelWidth,
					camera.pixelHeight
				}));
				return;
			}
			UniversalCameraData cameraData = UniversalRenderPipeline.CreateCameraData(UniversalRenderPipeline.GetRenderer(camera, additionalCameraData).frameData, camera, additionalCameraData, true);
			UniversalRenderPipeline.InitializeAdditionalCameraData(camera, additionalCameraData, true, cameraData);
			UniversalRenderPipeline.RenderSingleCamera(context, cameraData);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0002F4C8 File Offset: 0x0002D6C8
		private static bool TryGetCullingParameters(UniversalCameraData cameraData, out ScriptableCullingParameters cullingParams)
		{
			if (cameraData.xr.enabled)
			{
				cullingParams = cameraData.xr.cullingParams;
				if (!cameraData.camera.usePhysicalProperties && !XRGraphicsAutomatedTests.enabled)
				{
					cameraData.camera.fieldOfView = 57.29578f * Mathf.Atan(1f / cullingParams.stereoProjectionMatrix.m11) * 2f;
				}
				return true;
			}
			return cameraData.camera.TryGetCullingParameters(false, out cullingParams);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002F544 File Offset: 0x0002D744
		private static void RenderSingleCamera(ScriptableRenderContext context, UniversalCameraData cameraData)
		{
			Camera camera = cameraData.camera;
			ScriptableRenderer renderer = cameraData.renderer;
			if (renderer == null)
			{
				Debug.LogWarning(string.Format("Trying to render {0} with an invalid renderer. Camera rendering will be skipped.", camera.name));
				return;
			}
			using (ContextContainer frameData = renderer.frameData)
			{
				ScriptableCullingParameters cullingParameters;
				if (UniversalRenderPipeline.TryGetCullingParameters(cameraData, out cullingParameters))
				{
					ScriptableRenderer.current = renderer;
					UniversalRenderPipeline.s_RenderGraph.nativeRenderPassesEnabled = renderer.supportsNativeRenderPassRendergraphCompiler;
					bool isSceneViewCamera = cameraData.isSceneViewCamera;
					CommandBuffer cmd = CommandBufferPool.Get();
					CommandBuffer cmdScope = (cameraData.xr.enabled ? null : cmd);
					UniversalRenderPipeline.CameraMetadataCache.CameraMetadataCacheEntry cameraMetadata = UniversalRenderPipeline.CameraMetadataCache.GetCached(camera);
					using (new ProfilingScope(cmdScope, cameraMetadata.sampler))
					{
						renderer.Clear(cameraData.renderType);
						using (new ProfilingScope(UniversalRenderPipeline.Profiling.Pipeline.Renderer.setupCullingParameters))
						{
							CameraData legacyCameraData = new CameraData(frameData);
							renderer.OnPreCullRenderPasses(in legacyCameraData);
							renderer.SetupCullingParameters(ref cullingParameters, ref legacyCameraData);
						}
						context.ExecuteCommandBuffer(cmd);
						cmd.Clear();
						UniversalRenderPipeline.SetupPerCameraShaderConstants(cmd);
						ProbeVolumesOptions apvOptions = null;
						UniversalAdditionalCameraData additionalCameraData;
						if (camera.TryGetComponent<UniversalAdditionalCameraData>(out additionalCameraData))
						{
							VolumeStack volumeStack = additionalCameraData.volumeStack;
							apvOptions = ((volumeStack != null) ? volumeStack.GetComponent<ProbeVolumesOptions>() : null);
						}
						bool supportProbeVolume = UniversalRenderPipeline.asset != null && UniversalRenderPipeline.asset.lightProbeSystem == LightProbeSystem.ProbeVolumes;
						ProbeReferenceVolume.instance.SetEnableStateFromSRP(supportProbeVolume);
						ProbeReferenceVolume.instance.SetVertexSamplingEnabled(UniversalRenderPipeline.asset.shEvalMode == ShEvalMode.PerVertex || UniversalRenderPipeline.asset.shEvalMode == ShEvalMode.Mixed);
						if (supportProbeVolume && ProbeReferenceVolume.instance.isInitialized)
						{
							ProbeReferenceVolume.instance.PerformPendingOperations();
							if (camera.cameraType != CameraType.Reflection && camera.cameraType != CameraType.Preview)
							{
								ProbeReferenceVolume.instance.UpdateCellStreaming(cmd, camera, apvOptions);
							}
						}
						if (camera.cameraType == CameraType.Reflection || camera.cameraType == CameraType.Preview)
						{
							ScriptableRenderContext.EmitGeometryForCamera(camera);
						}
						if (supportProbeVolume)
						{
							ProbeReferenceVolume.instance.BindAPVRuntimeResources(cmd, true);
						}
						ProbeReferenceVolume.instance.RenderDebug(camera, apvOptions, Texture2D.whiteTexture);
						if (additionalCameraData != null)
						{
							additionalCameraData.motionVectorsPersistentData.Update(cameraData);
						}
						if (cameraData.taaHistory != null)
						{
							UniversalRenderPipeline.UpdateTemporalAATargets(cameraData);
						}
						RTHandles.SetReferenceSize(cameraData.cameraTargetDescriptor.width, cameraData.cameraTargetDescriptor.height);
						UniversalRenderingData data = frameData.Create<UniversalRenderingData>();
						data.cullResults = context.Cull(ref cullingParameters);
						GPUResidentDrawer.PostCullBeginCameraRendering(new RenderRequestBatcherContext
						{
							commandBuffer = cmd
						});
						UniversalRenderer universalRenderer = cameraData.renderer as UniversalRenderer;
						bool isForwardPlus = universalRenderer != null && universalRenderer.renderingModeActual == RenderingMode.ForwardPlus;
						UniversalLightData lightData;
						UniversalShadowData shadowData;
						using (new ProfilingScope(UniversalRenderPipeline.Profiling.Pipeline.initializeRenderingData))
						{
							UniversalRenderPipeline.CreateUniversalResourceData(frameData);
							lightData = UniversalRenderPipeline.CreateLightData(frameData, UniversalRenderPipeline.asset, data.cullResults.visibleLights);
							shadowData = UniversalRenderPipeline.CreateShadowData(frameData, UniversalRenderPipeline.asset, isForwardPlus);
							UniversalRenderPipeline.CreatePostProcessingData(frameData, UniversalRenderPipeline.asset);
							UniversalRenderPipeline.CreateRenderingData(frameData, UniversalRenderPipeline.asset, cmd, isForwardPlus, cameraData.renderer);
						}
						RenderingData legacyRenderingData = new RenderingData(frameData);
						UniversalRenderPipeline.CheckAndApplyDebugSettings(ref legacyRenderingData);
						UniversalRenderPipeline.CreateShadowAtlasAndCullShadowCasters(lightData, shadowData, cameraData, ref data.cullResults, ref context);
						renderer.AddRenderPasses(ref legacyRenderingData);
						if (UniversalRenderPipeline.useRenderGraph)
						{
							UniversalRenderPipeline.RecordAndExecuteRenderGraph(UniversalRenderPipeline.s_RenderGraph, context, renderer, cmd, cameraData.camera, cameraMetadata.name);
							renderer.FinishRenderGraphRendering(cmd);
						}
						else
						{
							using (new ProfilingScope(UniversalRenderPipeline.Profiling.Pipeline.Renderer.setup))
							{
								renderer.Setup(context, ref legacyRenderingData);
							}
							renderer.Execute(context, ref legacyRenderingData);
						}
					}
					context.ExecuteCommandBuffer(cmd);
					CommandBufferPool.Release(cmd);
					using (new ProfilingScope(UniversalRenderPipeline.Profiling.Pipeline.Context.submit))
					{
						if (!UniversalRenderPipeline.useRenderGraph && renderer.useRenderPassEnabled && !context.SubmitForRenderPassValidation())
						{
							renderer.useRenderPassEnabled = false;
							cmd.SetKeyword(in ShaderGlobalKeywords.RenderPassEnabled, false);
							Debug.LogWarning("Rendering command not supported inside a native RenderPass found. Falling back to non-RenderPass rendering path");
						}
						context.Submit();
					}
					ScriptableRenderer.current = null;
				}
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0002F9C4 File Offset: 0x0002DBC4
		private static void CreateShadowAtlasAndCullShadowCasters(UniversalLightData lightData, UniversalShadowData shadowData, UniversalCameraData cameraData, ref CullingResults cullResults, ref ScriptableRenderContext context)
		{
			if (!shadowData.supportsMainLightShadows && !shadowData.supportsAdditionalLightShadows)
			{
				return;
			}
			if (shadowData.supportsMainLightShadows)
			{
				UniversalRenderPipeline.InitializeMainLightShadowResolution(shadowData);
			}
			if (shadowData.supportsAdditionalLightShadows)
			{
				shadowData.shadowAtlasLayout = UniversalRenderPipeline.BuildAdditionalLightsShadowAtlasLayout(lightData, shadowData, cameraData);
			}
			shadowData.visibleLightsShadowCullingInfos = ShadowCulling.CullShadowCasters(ref context, shadowData, ref shadowData.shadowAtlasLayout, ref cullResults);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0002FA1C File Offset: 0x0002DC1C
		private static void RenderCameraStack(ScriptableRenderContext context, Camera baseCamera)
		{
			using (new ProfilingScope(ProfilingSampler.Get<URPProfileId>(URPProfileId.RenderCameraStack)))
			{
				UniversalAdditionalCameraData baseCameraAdditionalData;
				baseCamera.TryGetComponent<UniversalAdditionalCameraData>(out baseCameraAdditionalData);
				if (!(baseCameraAdditionalData != null) || baseCameraAdditionalData.renderType != CameraRenderType.Overlay)
				{
					ScriptableRenderer renderer = UniversalRenderPipeline.GetRenderer(baseCamera, baseCameraAdditionalData);
					List<Camera> cameraStack = ((renderer != null && renderer.SupportsCameraStackingType(CameraRenderType.Base)) ? ((baseCameraAdditionalData != null) ? baseCameraAdditionalData.cameraStack : null) : null);
					bool anyPostProcessingEnabled = baseCameraAdditionalData != null && baseCameraAdditionalData.renderPostProcessing;
					bool mainHdrDisplayOutputActive = UniversalRenderPipeline.HDROutputForMainDisplayIsActive();
					int num = UniversalRenderPipeline.asset.m_RendererDataList.Length;
					int lastActiveOverlayCameraIndex = -1;
					if (cameraStack != null)
					{
						Type baseCameraRendererType = renderer.GetType();
						bool shouldUpdateCameraStack = false;
						UniversalRenderPipeline.cameraStackRequiresDepthForPostprocessing = false;
						for (int i = 0; i < cameraStack.Count; i++)
						{
							Camera overlayCamera = cameraStack[i];
							if (overlayCamera == null)
							{
								shouldUpdateCameraStack = true;
							}
							else if (overlayCamera.isActiveAndEnabled)
							{
								UniversalAdditionalCameraData data;
								overlayCamera.TryGetComponent<UniversalAdditionalCameraData>(out data);
								ScriptableRenderer overlayRenderer = UniversalRenderPipeline.GetRenderer(overlayCamera, data);
								Type overlayRendererType = overlayRenderer.GetType();
								if (overlayRendererType != baseCameraRendererType)
								{
									Debug.LogWarning(string.Concat(new string[] { "Only cameras with compatible renderer types can be stacked. The camera: ", overlayCamera.name, " are using the renderer ", overlayRendererType.Name, ", but the base camera: ", baseCamera.name, " are using ", baseCameraRendererType.Name, ". Will skip rendering" }));
								}
								else if ((overlayRenderer.SupportedCameraStackingTypes() & 2) == 0)
								{
									Debug.LogWarning(string.Concat(new string[]
									{
										"The camera: ",
										overlayCamera.name,
										" is using a renderer of type ",
										renderer.GetType().Name,
										" which does not support Overlay cameras in it's current state."
									}));
								}
								else if (data == null || data.renderType != CameraRenderType.Overlay)
								{
									Debug.LogWarning("Stack can only contain Overlay cameras. The camera: " + overlayCamera.name + " " + string.Format("has a type {0} that is not supported. Will skip rendering.", data.renderType));
								}
								else
								{
									UniversalRenderPipeline.cameraStackRequiresDepthForPostprocessing |= UniversalRenderPipeline.CheckPostProcessForDepth();
									anyPostProcessingEnabled |= data.renderPostProcessing;
									lastActiveOverlayCameraIndex = i;
								}
							}
						}
						if (shouldUpdateCameraStack)
						{
							baseCameraAdditionalData.UpdateCameraStack();
						}
					}
					bool isStackedRendering = lastActiveOverlayCameraIndex != -1;
					bool xrActive = false;
					bool xrRendering = baseCameraAdditionalData == null || baseCameraAdditionalData.allowXRRendering;
					XRLayout xrLayout = XRSystem.NewLayout();
					xrLayout.AddCamera(baseCamera, xrRendering);
					foreach (ValueTuple<Camera, XRPass> valueTuple in xrLayout.GetActivePasses())
					{
						XRPass xrPass = valueTuple.Item2;
						XRPassUniversal xrPassUniversal = xrPass as XRPassUniversal;
						if (xrPass.enabled)
						{
							xrActive = true;
							UniversalRenderPipeline.UpdateCameraStereoMatrices(baseCamera, xrPass);
							float renderViewportScale = XRSystem.GetRenderViewportScale();
							ScalableBufferManager.ResizeBuffers(renderViewportScale, renderViewportScale);
						}
						bool finalOutputHDR = false;
						using (new UniversalRenderPipeline.CameraRenderingScope(context, baseCamera))
						{
							UniversalRenderPipeline.UpdateVolumeFramework(baseCamera, baseCameraAdditionalData);
							UniversalCameraData baseCameraData = UniversalRenderPipeline.CreateCameraData(renderer.frameData, baseCamera, baseCameraAdditionalData, !isStackedRendering);
							if (xrPass.enabled)
							{
								baseCameraData.xr = xrPass;
								UniversalRenderPipeline.UpdateCameraData(baseCameraData, in xrPass);
								xrLayout.ReconfigurePass(xrPass, baseCamera);
								XRSystemUniversal.BeginLateLatching(baseCamera, xrPassUniversal);
							}
							UniversalRenderPipeline.InitializeAdditionalCameraData(baseCamera, baseCameraAdditionalData, !isStackedRendering, baseCameraData);
							baseCameraData.postProcessingRequiresDepthTexture |= UniversalRenderPipeline.cameraStackRequiresDepthForPostprocessing;
							bool hdrDisplayOutputActive = mainHdrDisplayOutputActive;
							if (xrPass.enabled)
							{
								hdrDisplayOutputActive = xrPass.isHDRDisplayOutputActive;
							}
							finalOutputHDR = UniversalRenderPipeline.asset.supportsHDR && hdrDisplayOutputActive && baseCamera.targetTexture == null && (baseCamera.cameraType == CameraType.Game || baseCamera.cameraType == CameraType.VR) && baseCameraData.allowHDROutput;
							baseCameraData.stackAnyPostProcessingEnabled = anyPostProcessingEnabled;
							baseCameraData.stackLastCameraOutputToHDR = finalOutputHDR;
							UniversalRenderPipeline.RenderSingleCamera(context, baseCameraData);
						}
						if (xrPass.enabled)
						{
							XRSystemUniversal.EndLateLatching(baseCamera, xrPassUniversal);
						}
						if (isStackedRendering)
						{
							for (int j = 0; j < cameraStack.Count; j++)
							{
								Camera overlayCamera2 = cameraStack[j];
								if (overlayCamera2.isActiveAndEnabled)
								{
									UniversalAdditionalCameraData overlayAdditionalCameraData;
									overlayCamera2.TryGetComponent<UniversalAdditionalCameraData>(out overlayAdditionalCameraData);
									if (overlayAdditionalCameraData != null)
									{
										UniversalCameraData overlayCameraData = UniversalRenderPipeline.CreateCameraData(UniversalRenderPipeline.GetRenderer(overlayCamera2, overlayAdditionalCameraData).frameData, baseCamera, baseCameraAdditionalData, false);
										if (xrPass.enabled)
										{
											overlayCameraData.xr = xrPass;
											UniversalRenderPipeline.UpdateCameraData(overlayCameraData, in xrPass);
										}
										UniversalRenderPipeline.InitializeAdditionalCameraData(overlayCamera2, overlayAdditionalCameraData, false, overlayCameraData);
										overlayCameraData.camera = overlayCamera2;
										overlayCameraData.baseCamera = baseCamera;
										UniversalRenderPipeline.UpdateCameraStereoMatrices(overlayAdditionalCameraData.camera, xrPass);
										using (new UniversalRenderPipeline.CameraRenderingScope(context, overlayCamera2))
										{
											UniversalRenderPipeline.UpdateVolumeFramework(overlayCamera2, overlayAdditionalCameraData);
											bool lastCamera = j == lastActiveOverlayCameraIndex;
											UniversalRenderPipeline.InitializeAdditionalCameraData(overlayCamera2, overlayAdditionalCameraData, lastCamera, overlayCameraData);
											overlayCameraData.stackAnyPostProcessingEnabled = anyPostProcessingEnabled;
											overlayCameraData.stackLastCameraOutputToHDR = finalOutputHDR;
											xrLayout.ReconfigurePass(overlayCameraData.xr, overlayCamera2);
											UniversalRenderPipeline.RenderSingleCamera(context, overlayCameraData);
										}
									}
								}
							}
						}
					}
					if (xrActive)
					{
						CommandBuffer cmd = CommandBufferPool.Get();
						XRSystem.RenderMirrorView(cmd, baseCamera);
						context.ExecuteCommandBuffer(cmd);
						context.Submit();
						CommandBufferPool.Release(cmd);
					}
					XRSystem.EndLayout();
				}
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0002FF94 File Offset: 0x0002E194
		private static void UpdateCameraData(UniversalCameraData baseCameraData, in XRPass xr)
		{
			Rect cameraRect = baseCameraData.camera.rect;
			Rect xrViewport = xr.GetViewport(0);
			baseCameraData.pixelRect = new Rect(cameraRect.x * xrViewport.width + xrViewport.x, cameraRect.y * xrViewport.height + xrViewport.y, cameraRect.width * xrViewport.width, cameraRect.height * xrViewport.height);
			Rect camPixelRect = baseCameraData.pixelRect;
			baseCameraData.pixelWidth = (int)Math.Round((double)(camPixelRect.width + camPixelRect.x)) - (int)Math.Round((double)camPixelRect.x);
			baseCameraData.pixelHeight = (int)Math.Round((double)(camPixelRect.height + camPixelRect.y)) - (int)Math.Round((double)camPixelRect.y);
			baseCameraData.aspectRatio = (float)baseCameraData.pixelWidth / (float)baseCameraData.pixelHeight;
			RenderTextureDescriptor originalTargetDesc = baseCameraData.cameraTargetDescriptor;
			baseCameraData.cameraTargetDescriptor = xr.renderTargetDesc;
			if (baseCameraData.isHdrEnabled)
			{
				baseCameraData.cameraTargetDescriptor.graphicsFormat = originalTargetDesc.graphicsFormat;
			}
			baseCameraData.cameraTargetDescriptor.msaaSamples = originalTargetDesc.msaaSamples;
			if (baseCameraData.isDefaultViewport)
			{
				baseCameraData.cameraTargetDescriptor.useDynamicScale = true;
				return;
			}
			baseCameraData.cameraTargetDescriptor.width = baseCameraData.pixelWidth;
			baseCameraData.cameraTargetDescriptor.height = baseCameraData.pixelHeight;
			baseCameraData.cameraTargetDescriptor.useDynamicScale = false;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00030104 File Offset: 0x0002E304
		private static void UpdateVolumeFramework(Camera camera, UniversalAdditionalCameraData additionalCameraData)
		{
			using (new ProfilingScope(ProfilingSampler.Get<URPProfileId>(URPProfileId.UpdateVolumeFramework)))
			{
				if (!((camera.cameraType == CameraType.SceneView) | (additionalCameraData != null && additionalCameraData.requiresVolumeFrameworkUpdate)) && additionalCameraData)
				{
					if (additionalCameraData.volumeStack != null && !additionalCameraData.volumeStack.isValid)
					{
						camera.DestroyVolumeStack(additionalCameraData);
					}
					if (additionalCameraData.volumeStack == null)
					{
						camera.UpdateVolumeStack(additionalCameraData);
					}
					VolumeManager.instance.stack = additionalCameraData.volumeStack;
				}
				else
				{
					if (additionalCameraData && additionalCameraData.volumeStack != null)
					{
						camera.DestroyVolumeStack(additionalCameraData);
					}
					LayerMask layerMask;
					Transform trigger;
					camera.GetVolumeLayerMaskAndTrigger(additionalCameraData, out layerMask, out trigger);
					VolumeManager.instance.ResetMainStack();
					VolumeManager.instance.Update(trigger, layerMask);
				}
			}
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x000301DC File Offset: 0x0002E3DC
		private static bool CheckPostProcessForDepth(UniversalCameraData cameraData)
		{
			return cameraData.postProcessEnabled && ((cameraData.IsTemporalAAEnabled() && cameraData.renderType == CameraRenderType.Base) || UniversalRenderPipeline.CheckPostProcessForDepth());
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00030200 File Offset: 0x0002E400
		private static bool CheckPostProcessForDepth()
		{
			VolumeStack stack = VolumeManager.instance.stack;
			return stack.GetComponent<DepthOfField>().IsActive() || stack.GetComponent<MotionBlur>().IsActive();
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00030237 File Offset: 0x0002E437
		private static void SetSupportedRenderingFeatures(UniversalRenderPipelineAsset pipelineAsset)
		{
			SupportedRenderingFeatures.active.supportsHDR = pipelineAsset.supportsHDR;
			SupportedRenderingFeatures.active.rendersUIOverlay = true;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00030254 File Offset: 0x0002E454
		private static ScriptableRenderer GetRenderer(Camera camera, UniversalAdditionalCameraData additionalCameraData)
		{
			ScriptableRenderer renderer = ((additionalCameraData != null) ? additionalCameraData.scriptableRenderer : null);
			if (renderer == null || camera.cameraType == CameraType.SceneView)
			{
				renderer = UniversalRenderPipeline.asset.scriptableRenderer;
			}
			return renderer;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0003028C File Offset: 0x0002E48C
		private static UniversalCameraData CreateCameraData(ContextContainer frameData, Camera camera, UniversalAdditionalCameraData additionalCameraData, bool resolveFinalTarget)
		{
			UniversalCameraData universalCameraData;
			using (new ProfilingScope(UniversalRenderPipeline.Profiling.Pipeline.initializeCameraData))
			{
				ScriptableRenderer renderer = UniversalRenderPipeline.GetRenderer(camera, additionalCameraData);
				UniversalCameraData cameraData = frameData.Create<UniversalCameraData>();
				UniversalRenderPipeline.InitializeStackedCameraData(camera, additionalCameraData, cameraData);
				cameraData.camera = camera;
				cameraData.historyManager = ((additionalCameraData != null) ? additionalCameraData.historyManager : null);
				bool rendererSupportsMSAA = renderer != null && renderer.supportedRenderingFeatures.msaa;
				int msaaSamples = 1;
				if (camera.allowMSAA && UniversalRenderPipeline.asset.msaaSampleCount > 1 && rendererSupportsMSAA)
				{
					msaaSamples = ((camera.targetTexture != null) ? camera.targetTexture.antiAliasing : UniversalRenderPipeline.asset.msaaSampleCount);
				}
				if (cameraData.xrRendering && rendererSupportsMSAA && camera.targetTexture == null)
				{
					msaaSamples = (int)XRSystem.GetDisplayMSAASamples();
				}
				bool needsAlphaChannel = Graphics.preserveFramebufferAlpha;
				cameraData.hdrColorBufferPrecision = (UniversalRenderPipeline.asset ? UniversalRenderPipeline.asset.hdrColorBufferPrecision : HDRColorBufferPrecision._32Bits);
				cameraData.cameraTargetDescriptor = UniversalRenderPipeline.CreateRenderTextureDescriptor(camera, cameraData, cameraData.isHdrEnabled, cameraData.hdrColorBufferPrecision, msaaSamples, needsAlphaChannel, cameraData.requiresOpaqueTexture);
				GraphicsFormatUtility.GetAlphaComponentCount(cameraData.cameraTargetDescriptor.graphicsFormat);
				cameraData.isAlphaOutputEnabled = GraphicsFormatUtility.HasAlphaChannel(cameraData.cameraTargetDescriptor.graphicsFormat);
				if (cameraData.camera.cameraType == CameraType.SceneView && CoreUtils.IsSceneFilteringEnabled())
				{
					cameraData.isAlphaOutputEnabled = true;
				}
				universalCameraData = cameraData;
			}
			return universalCameraData;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0003040C File Offset: 0x0002E60C
		private static void InitializeStackedCameraData(Camera baseCamera, UniversalAdditionalCameraData baseAdditionalCameraData, UniversalCameraData cameraData)
		{
			using (new ProfilingScope(UniversalRenderPipeline.Profiling.Pipeline.initializeStackedCameraData))
			{
				UniversalRenderPipelineAsset settings = UniversalRenderPipeline.asset;
				cameraData.targetTexture = baseCamera.targetTexture;
				cameraData.cameraType = baseCamera.cameraType;
				if (cameraData.isSceneViewCamera)
				{
					cameraData.volumeLayerMask = 1;
					cameraData.volumeTrigger = null;
					cameraData.isStopNaNEnabled = false;
					cameraData.isDitheringEnabled = false;
					cameraData.antialiasing = AntialiasingMode.None;
					cameraData.antialiasingQuality = AntialiasingQuality.High;
					cameraData.xrRendering = false;
					cameraData.allowHDROutput = false;
				}
				else if (baseAdditionalCameraData != null)
				{
					cameraData.volumeLayerMask = baseAdditionalCameraData.volumeLayerMask;
					cameraData.volumeTrigger = ((baseAdditionalCameraData.volumeTrigger == null) ? baseCamera.transform : baseAdditionalCameraData.volumeTrigger);
					cameraData.isStopNaNEnabled = baseAdditionalCameraData.stopNaN && SystemInfo.graphicsShaderLevel >= 35;
					cameraData.isDitheringEnabled = baseAdditionalCameraData.dithering;
					cameraData.antialiasing = baseAdditionalCameraData.antialiasing;
					cameraData.antialiasingQuality = baseAdditionalCameraData.antialiasingQuality;
					cameraData.xrRendering = baseAdditionalCameraData.allowXRRendering && XRSystem.displayActive;
					cameraData.allowHDROutput = baseAdditionalCameraData.allowHDROutput;
				}
				else
				{
					cameraData.volumeLayerMask = 1;
					cameraData.volumeTrigger = null;
					cameraData.isStopNaNEnabled = false;
					cameraData.isDitheringEnabled = false;
					cameraData.antialiasing = AntialiasingMode.None;
					cameraData.antialiasingQuality = AntialiasingQuality.High;
					cameraData.xrRendering = XRSystem.displayActive;
					cameraData.allowHDROutput = true;
				}
				cameraData.isHdrEnabled = baseCamera.allowHDR && settings.supportsHDR;
				cameraData.allowHDROutput &= settings.supportsHDR;
				Rect cameraRect = baseCamera.rect;
				cameraData.pixelRect = baseCamera.pixelRect;
				cameraData.pixelWidth = baseCamera.pixelWidth;
				cameraData.pixelHeight = baseCamera.pixelHeight;
				cameraData.aspectRatio = (float)cameraData.pixelWidth / (float)cameraData.pixelHeight;
				cameraData.isDefaultViewport = Math.Abs(cameraRect.x) <= 0f && Math.Abs(cameraRect.y) <= 0f && Math.Abs(cameraRect.width) >= 1f && Math.Abs(cameraRect.height) >= 1f;
				bool isScenePreviewOrReflectionCamera = cameraData.cameraType == CameraType.SceneView || cameraData.cameraType == CameraType.Preview || cameraData.cameraType == CameraType.Reflection;
				cameraData.renderScale = ((Mathf.Abs(1f - settings.renderScale) < 0.05f || isScenePreviewOrReflectionCamera) ? 1f : settings.renderScale);
				RenderGraphSettings renderGraphSettings;
				bool enableRenderGraph = GraphicsSettings.TryGetRenderPipelineSettings<RenderGraphSettings>(out renderGraphSettings) && !renderGraphSettings.enableRenderCompatibilityMode;
				cameraData.upscalingFilter = UniversalRenderPipeline.ResolveUpscalingFilterSelection(new Vector2((float)cameraData.pixelWidth, (float)cameraData.pixelHeight), cameraData.renderScale, settings.upscalingFilter, enableRenderGraph);
				if (cameraData.renderScale > 1f)
				{
					cameraData.imageScalingMode = ImageScalingMode.Downscaling;
				}
				else if (cameraData.renderScale < 1f || (!isScenePreviewOrReflectionCamera && (cameraData.upscalingFilter == ImageUpscalingFilter.FSR || cameraData.upscalingFilter == ImageUpscalingFilter.STP)))
				{
					cameraData.imageScalingMode = ImageScalingMode.Upscaling;
					if (cameraData.upscalingFilter == ImageUpscalingFilter.STP)
					{
						cameraData.antialiasing = AntialiasingMode.TemporalAntiAliasing;
					}
				}
				else
				{
					cameraData.imageScalingMode = ImageScalingMode.None;
				}
				cameraData.fsrOverrideSharpness = settings.fsrOverrideSharpness;
				cameraData.fsrSharpness = settings.fsrSharpness;
				cameraData.xr = XRSystem.emptyPass;
				XRSystem.SetRenderScale(cameraData.renderScale);
				SortingCriteria commonOpaqueFlags = SortingCriteria.CommonOpaque;
				SortingCriteria noFrontToBackOpaqueFlags = SortingCriteria.SortingLayer | SortingCriteria.RenderQueue | SortingCriteria.OptimizeStateChanges | SortingCriteria.CanvasOrder;
				bool hasHSRGPU = SystemInfo.hasHiddenSurfaceRemovalOnGPU;
				cameraData.defaultOpaqueSortFlags = (((baseCamera.opaqueSortMode == OpaqueSortMode.Default && hasHSRGPU) || baseCamera.opaqueSortMode == OpaqueSortMode.NoDistanceSort) ? noFrontToBackOpaqueFlags : commonOpaqueFlags);
				cameraData.captureActions = CameraCaptureBridge.GetCachedCaptureActionsEnumerator(baseCamera);
			}
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000307C0 File Offset: 0x0002E9C0
		private static void InitializeAdditionalCameraData(Camera camera, UniversalAdditionalCameraData additionalCameraData, bool resolveFinalTarget, UniversalCameraData cameraData)
		{
			using (new ProfilingScope(UniversalRenderPipeline.Profiling.Pipeline.initializeAdditionalCameraData))
			{
				ScriptableRenderer renderer = UniversalRenderPipeline.GetRenderer(camera, additionalCameraData);
				UniversalRenderPipelineAsset settings = UniversalRenderPipeline.asset;
				bool anyShadowsEnabled = settings.supportsMainLightShadows || settings.supportsAdditionalLightShadows;
				cameraData.maxShadowDistance = Mathf.Min(settings.shadowDistance, camera.farClipPlane);
				cameraData.maxShadowDistance = ((anyShadowsEnabled && cameraData.maxShadowDistance >= camera.nearClipPlane) ? cameraData.maxShadowDistance : 0f);
				bool isSceneViewCamera = cameraData.isSceneViewCamera;
				if (isSceneViewCamera)
				{
					cameraData.renderType = CameraRenderType.Base;
					cameraData.clearDepth = true;
					cameraData.postProcessEnabled = CoreUtils.ArePostProcessesEnabled(camera);
					cameraData.requiresDepthTexture = settings.supportsCameraDepthTexture;
					cameraData.requiresOpaqueTexture = settings.supportsCameraOpaqueTexture;
					cameraData.useScreenCoordOverride = false;
					cameraData.screenSizeOverride = cameraData.pixelRect.size;
					cameraData.screenCoordScaleBias = Vector2.one;
				}
				else if (additionalCameraData != null)
				{
					cameraData.renderType = additionalCameraData.renderType;
					cameraData.clearDepth = additionalCameraData.renderType == CameraRenderType.Base || additionalCameraData.clearDepth;
					cameraData.postProcessEnabled = additionalCameraData.renderPostProcessing;
					cameraData.maxShadowDistance = (additionalCameraData.renderShadows ? cameraData.maxShadowDistance : 0f);
					cameraData.requiresDepthTexture = additionalCameraData.requiresDepthTexture;
					cameraData.requiresOpaqueTexture = additionalCameraData.requiresColorTexture;
					cameraData.useScreenCoordOverride = additionalCameraData.useScreenCoordOverride;
					cameraData.screenSizeOverride = additionalCameraData.screenSizeOverride;
					cameraData.screenCoordScaleBias = additionalCameraData.screenCoordScaleBias;
				}
				else
				{
					cameraData.renderType = CameraRenderType.Base;
					cameraData.clearDepth = true;
					cameraData.postProcessEnabled = false;
					cameraData.requiresDepthTexture = settings.supportsCameraDepthTexture;
					cameraData.requiresOpaqueTexture = settings.supportsCameraOpaqueTexture;
					cameraData.useScreenCoordOverride = false;
					cameraData.screenSizeOverride = cameraData.pixelRect.size;
					cameraData.screenCoordScaleBias = Vector2.one;
				}
				cameraData.renderer = renderer;
				cameraData.requiresDepthTexture = cameraData.requiresDepthTexture || isSceneViewCamera;
				cameraData.postProcessingRequiresDepthTexture = UniversalRenderPipeline.CheckPostProcessForDepth(cameraData);
				cameraData.resolveFinalTarget = resolveFinalTarget;
				bool flag;
				if (GPUResidentDrawer.IsInstanceOcclusionCullingEnabled() && renderer.supportsGPUOcclusion)
				{
					CameraType cameraType = camera.cameraType;
					flag = cameraType == CameraType.SceneView || cameraType == CameraType.Game || cameraType == CameraType.Preview;
				}
				else
				{
					flag = false;
				}
				cameraData.useGPUOcclusionCulling = flag;
				cameraData.requiresDepthTexture |= cameraData.useGPUOcclusionCulling;
				bool flag2 = cameraData.renderType == CameraRenderType.Overlay;
				if (flag2)
				{
					cameraData.requiresOpaqueTexture = false;
				}
				if (additionalCameraData != null)
				{
					UniversalRenderPipeline.UpdateTemporalAAData(cameraData, additionalCameraData);
				}
				Matrix4x4 projectionMatrix = camera.projectionMatrix;
				if (flag2 && !camera.orthographic && cameraData.pixelRect != camera.pixelRect)
				{
					float newCotangent = camera.projectionMatrix.m00 * camera.aspect / cameraData.aspectRatio;
					projectionMatrix.m00 = newCotangent;
				}
				UniversalRenderPipeline.ApplyTaaRenderingDebugOverrides(ref cameraData.taaSettings);
				TemporalAA.JitterFunc jitterFunc = (cameraData.IsSTPEnabled() ? StpUtils.s_JitterFunc : TemporalAA.s_JitterFunc);
				Matrix4x4 jitterMat = TemporalAA.CalculateJitterMatrix(cameraData, jitterFunc);
				cameraData.SetViewProjectionAndJitterMatrix(camera.worldToCameraMatrix, projectionMatrix, jitterMat);
				cameraData.worldSpaceCameraPos = camera.transform.position;
				Color backgroundColorSRGB = camera.backgroundColor;
				cameraData.backgroundColor = CoreUtils.ConvertSRGBToActiveColorSpace(backgroundColorSRGB);
				cameraData.stackAnyPostProcessingEnabled = cameraData.postProcessEnabled;
				cameraData.stackLastCameraOutputToHDR = cameraData.isHDROutputActive;
				bool allowAlphaOutput = !cameraData.postProcessEnabled || (cameraData.postProcessEnabled && settings.allowPostProcessAlphaOutput);
				cameraData.isAlphaOutputEnabled = cameraData.isAlphaOutputEnabled && allowAlphaOutput;
			}
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00030B44 File Offset: 0x0002ED44
		private static UniversalRenderingData CreateRenderingData(ContextContainer frameData, UniversalRenderPipelineAsset settings, CommandBuffer cmd, bool isForwardPlus, ScriptableRenderer renderer)
		{
			UniversalLightData universalLightData = frameData.Get<UniversalLightData>();
			UniversalRenderingData data = frameData.Get<UniversalRenderingData>();
			data.supportsDynamicBatching = settings.supportsDynamicBatching;
			data.perObjectData = UniversalRenderPipeline.GetPerObjectLightFlags(universalLightData.additionalLightsCount, isForwardPlus, settings.reflectionProbeBlending);
			if (UniversalRenderPipeline.useRenderGraph)
			{
				data.m_CommandBuffer = null;
			}
			else
			{
				data.m_CommandBuffer = cmd;
			}
			UniversalRenderer universalRenderer = renderer as UniversalRenderer;
			if (universalRenderer != null)
			{
				data.renderingMode = universalRenderer.renderingModeActual;
				data.opaqueLayerMask = universalRenderer.opaqueLayerMask;
				data.transparentLayerMask = universalRenderer.transparentLayerMask;
			}
			return data;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00030BCC File Offset: 0x0002EDCC
		private static UniversalShadowData CreateShadowData(ContextContainer frameData, UniversalRenderPipelineAsset urpAsset, bool isForwardPlus)
		{
			UniversalShadowData universalShadowData;
			using (new ProfilingScope(UniversalRenderPipeline.Profiling.Pipeline.initializeShadowData))
			{
				UniversalShadowData shadowData = frameData.Create<UniversalShadowData>();
				UniversalCameraData universalCameraData = frameData.Get<UniversalCameraData>();
				UniversalLightData lightData = frameData.Get<UniversalLightData>();
				UniversalRenderPipeline.m_ShadowBiasData.Clear();
				UniversalRenderPipeline.m_ShadowResolutionData.Clear();
				shadowData.shadowmapDepthBufferBits = 16;
				shadowData.mainLightShadowCascadeBorder = urpAsset.cascadeBorder;
				shadowData.mainLightShadowCascadesCount = urpAsset.shadowCascadeCount;
				shadowData.mainLightShadowCascadesSplit = UniversalRenderPipeline.GetMainLightCascadeSplit(shadowData.mainLightShadowCascadesCount, urpAsset);
				shadowData.mainLightShadowmapWidth = urpAsset.mainLightShadowmapResolution;
				shadowData.mainLightShadowmapHeight = urpAsset.mainLightShadowmapResolution;
				shadowData.additionalLightsShadowmapWidth = (shadowData.additionalLightsShadowmapHeight = urpAsset.additionalLightsShadowmapResolution);
				shadowData.isKeywordAdditionalLightShadowsEnabled = false;
				shadowData.isKeywordSoftShadowsEnabled = false;
				shadowData.mainLightShadowResolution = 0;
				shadowData.mainLightRenderTargetWidth = 0;
				shadowData.mainLightRenderTargetHeight = 0;
				shadowData.shadowAtlasLayout = default(AdditionalLightsShadowAtlasLayout);
				shadowData.visibleLightsShadowCullingInfos = default(NativeArray<URPLightShadowCullingInfos>);
				int mainLightIndex = lightData.mainLightIndex;
				NativeArray<VisibleLight> visibleLights = lightData.visibleLights;
				bool cameraRenderShadows = universalCameraData.maxShadowDistance > 0f;
				shadowData.mainLightShadowsEnabled = urpAsset.supportsMainLightShadows && urpAsset.mainLightRenderingMode == LightRenderingMode.PerPixel;
				shadowData.supportsMainLightShadows = SystemInfo.supportsShadows && shadowData.mainLightShadowsEnabled && cameraRenderShadows;
				shadowData.additionalLightShadowsEnabled = urpAsset.supportsAdditionalLightShadows && (urpAsset.additionalLightsRenderingMode == LightRenderingMode.PerPixel || isForwardPlus);
				shadowData.supportsAdditionalLightShadows = SystemInfo.supportsShadows && shadowData.additionalLightShadowsEnabled && !lightData.shadeAdditionalLightsPerVertex && cameraRenderShadows;
				if (!shadowData.supportsMainLightShadows && !shadowData.supportsAdditionalLightShadows)
				{
					universalShadowData = shadowData;
				}
				else
				{
					shadowData.supportsMainLightShadows &= mainLightIndex != -1 && visibleLights[mainLightIndex].light != null && visibleLights[mainLightIndex].light.shadows > LightShadows.None;
					if (shadowData.supportsAdditionalLightShadows)
					{
						bool additionalLightsCastShadows = false;
						for (int i = 0; i < visibleLights.Length; i++)
						{
							if (i != mainLightIndex)
							{
								ref VisibleLight vl = ref visibleLights.UnsafeElementAtMutable(i);
								if (vl.lightType == LightType.Spot || vl.lightType == LightType.Point)
								{
									Light light = vl.light;
									if (!(light == null) && light.shadows != LightShadows.None)
									{
										additionalLightsCastShadows = true;
										break;
									}
								}
							}
						}
						shadowData.supportsAdditionalLightShadows = additionalLightsCastShadows;
					}
					if (!shadowData.supportsMainLightShadows && !shadowData.supportsAdditionalLightShadows)
					{
						universalShadowData = shadowData;
					}
					else
					{
						for (int j = 0; j < visibleLights.Length; j++)
						{
							if (!shadowData.supportsMainLightShadows && j == mainLightIndex)
							{
								UniversalRenderPipeline.m_ShadowBiasData.Add(Vector4.zero);
								UniversalRenderPipeline.m_ShadowResolutionData.Add(0);
							}
							else if (!shadowData.supportsAdditionalLightShadows && j != mainLightIndex)
							{
								UniversalRenderPipeline.m_ShadowBiasData.Add(Vector4.zero);
								UniversalRenderPipeline.m_ShadowResolutionData.Add(0);
							}
							else
							{
								Light light2 = visibleLights.UnsafeElementAtMutable(j).light;
								UniversalAdditionalLightData data = null;
								if (light2 != null)
								{
									light2.gameObject.TryGetComponent<UniversalAdditionalLightData>(out data);
								}
								if (data && !data.usePipelineSettings)
								{
									UniversalRenderPipeline.m_ShadowBiasData.Add(new Vector4(light2.shadowBias, light2.shadowNormalBias, 0f, 0f));
								}
								else
								{
									UniversalRenderPipeline.m_ShadowBiasData.Add(new Vector4(urpAsset.shadowDepthBias, urpAsset.shadowNormalBias, 0f, 0f));
								}
								if (data && data.additionalLightsShadowResolutionTier == UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierCustom)
								{
									UniversalRenderPipeline.m_ShadowResolutionData.Add((int)light2.shadowResolution);
								}
								else if (data && data.additionalLightsShadowResolutionTier != UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierCustom)
								{
									int resolutionTier = Mathf.Clamp(data.additionalLightsShadowResolutionTier, UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierLow, UniversalAdditionalLightData.AdditionalLightsShadowResolutionTierHigh);
									UniversalRenderPipeline.m_ShadowResolutionData.Add(urpAsset.GetAdditionalLightsShadowResolution(resolutionTier));
								}
								else
								{
									UniversalRenderPipeline.m_ShadowResolutionData.Add(urpAsset.GetAdditionalLightsShadowResolution(UniversalAdditionalLightData.AdditionalLightsShadowDefaultResolutionTier));
								}
							}
						}
						shadowData.bias = UniversalRenderPipeline.m_ShadowBiasData;
						shadowData.resolution = UniversalRenderPipeline.m_ShadowResolutionData;
						shadowData.supportsSoftShadows = urpAsset.supportsSoftShadows && (shadowData.supportsMainLightShadows || shadowData.supportsAdditionalLightShadows);
						universalShadowData = shadowData;
					}
				}
			}
			return universalShadowData;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00031010 File Offset: 0x0002F210
		private static Vector3 GetMainLightCascadeSplit(int mainLightShadowCascadesCount, UniversalRenderPipelineAsset urpAsset)
		{
			switch (mainLightShadowCascadesCount)
			{
			case 1:
				return new Vector3(1f, 0f, 0f);
			case 2:
				return new Vector3(urpAsset.cascade2Split, 1f, 0f);
			case 3:
				return urpAsset.cascade3Split;
			default:
				return urpAsset.cascade4Split;
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00031070 File Offset: 0x0002F270
		private static void InitializeMainLightShadowResolution(UniversalShadowData shadowData)
		{
			shadowData.mainLightShadowResolution = ShadowUtils.GetMaxTileResolutionInAtlas(shadowData.mainLightShadowmapWidth, shadowData.mainLightShadowmapHeight, shadowData.mainLightShadowCascadesCount);
			shadowData.mainLightRenderTargetWidth = shadowData.mainLightShadowmapWidth;
			shadowData.mainLightRenderTargetHeight = ((shadowData.mainLightShadowCascadesCount == 2) ? (shadowData.mainLightShadowmapHeight >> 1) : shadowData.mainLightShadowmapHeight);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000310C8 File Offset: 0x0002F2C8
		private static UniversalPostProcessingData CreatePostProcessingData(ContextContainer frameData, UniversalRenderPipelineAsset settings)
		{
			UniversalPostProcessingData postProcessingData = frameData.Create<UniversalPostProcessingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			postProcessingData.isEnabled = cameraData.stackAnyPostProcessingEnabled;
			postProcessingData.gradingMode = (settings.supportsHDR ? settings.colorGradingMode : ColorGradingMode.LowDynamicRange);
			if (cameraData.stackLastCameraOutputToHDR)
			{
				postProcessingData.gradingMode = ColorGradingMode.HighDynamicRange;
			}
			postProcessingData.lutSize = settings.colorGradingLutSize;
			postProcessingData.useFastSRGBLinearConversion = settings.useFastSRGBLinearConversion;
			postProcessingData.supportScreenSpaceLensFlare = settings.supportScreenSpaceLensFlare;
			postProcessingData.supportDataDrivenLensFlare = settings.supportDataDrivenLensFlare;
			return postProcessingData;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00031146 File Offset: 0x0002F346
		private static UniversalResourceData CreateUniversalResourceData(ContextContainer frameData)
		{
			return frameData.Create<UniversalResourceData>();
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00031150 File Offset: 0x0002F350
		private static UniversalLightData CreateLightData(ContextContainer frameData, UniversalRenderPipelineAsset settings, NativeArray<VisibleLight> visibleLights)
		{
			UniversalLightData universalLightData;
			using (new ProfilingScope(UniversalRenderPipeline.Profiling.Pipeline.initializeLightData))
			{
				UniversalLightData lightData = frameData.Create<UniversalLightData>();
				lightData.mainLightIndex = UniversalRenderPipeline.GetMainLightIndex(settings, visibleLights);
				if (settings.additionalLightsRenderingMode != LightRenderingMode.Disabled)
				{
					lightData.additionalLightsCount = Math.Min((lightData.mainLightIndex != -1) ? (visibleLights.Length - 1) : visibleLights.Length, UniversalRenderPipeline.maxVisibleAdditionalLights);
					lightData.maxPerObjectAdditionalLightsCount = Math.Min(settings.maxAdditionalLightsCount, UniversalRenderPipeline.maxPerObjectLights);
				}
				else
				{
					lightData.additionalLightsCount = 0;
					lightData.maxPerObjectAdditionalLightsCount = 0;
				}
				lightData.supportsAdditionalLights = settings.additionalLightsRenderingMode > LightRenderingMode.Disabled;
				lightData.shadeAdditionalLightsPerVertex = settings.additionalLightsRenderingMode == LightRenderingMode.PerVertex;
				lightData.visibleLights = visibleLights;
				lightData.supportsMixedLighting = settings.supportsMixedLighting;
				lightData.reflectionProbeBlending = settings.reflectionProbeBlending;
				lightData.reflectionProbeBoxProjection = settings.reflectionProbeBoxProjection;
				lightData.supportsLightLayers = RenderingUtils.SupportsLightLayers(SystemInfo.graphicsDeviceType) && settings.useRenderingLayers;
				universalLightData = lightData;
			}
			return universalLightData;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00031260 File Offset: 0x0002F460
		private static void ApplyTaaRenderingDebugOverrides(ref TemporalAA.Settings taaSettings)
		{
			switch (DebugDisplaySettings<UniversalRenderPipelineDebugDisplaySettings>.Instance.renderingSettings.taaDebugMode)
			{
			case DebugDisplaySettingsRendering.TaaDebugMode.ShowRawFrame:
				taaSettings.m_FrameInfluence = 1f;
				return;
			case DebugDisplaySettingsRendering.TaaDebugMode.ShowRawFrameNoJitter:
				taaSettings.m_FrameInfluence = 1f;
				taaSettings.jitterScale = 0f;
				return;
			case DebugDisplaySettingsRendering.TaaDebugMode.ShowClampedHistory:
				taaSettings.m_FrameInfluence = 0f;
				return;
			default:
				return;
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000312C0 File Offset: 0x0002F4C0
		private static void UpdateTemporalAAData(UniversalCameraData cameraData, UniversalAdditionalCameraData additionalCameraData)
		{
			additionalCameraData.historyManager.RequestAccess<TaaHistory>();
			cameraData.taaHistory = additionalCameraData.historyManager.GetHistoryForWrite<TaaHistory>();
			if (cameraData.IsSTPEnabled())
			{
				additionalCameraData.historyManager.RequestAccess<StpHistory>();
				cameraData.stpHistory = additionalCameraData.historyManager.GetHistoryForWrite<StpHistory>();
			}
			ref TemporalAA.Settings taaSettings = ref additionalCameraData.taaSettings;
			cameraData.taaSettings = taaSettings;
			taaSettings.resetHistoryFrames -= ((taaSettings.resetHistoryFrames > 0) ? 1 : 0);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00031338 File Offset: 0x0002F538
		private static void UpdateTemporalAATargets(UniversalCameraData cameraData)
		{
			if (cameraData.IsTemporalAAEnabled())
			{
				bool xrMultipassEnabled = cameraData.xr.enabled && !cameraData.xr.singlePassEnabled;
				bool allocation;
				if (cameraData.IsSTPEnabled())
				{
					cameraData.taaHistory.Reset();
					allocation = cameraData.stpHistory.Update(cameraData);
				}
				else
				{
					allocation = cameraData.taaHistory.Update(ref cameraData.cameraTargetDescriptor, xrMultipassEnabled);
				}
				if (allocation)
				{
					cameraData.taaSettings.resetHistoryFrames = cameraData.taaSettings.resetHistoryFrames + (xrMultipassEnabled ? 2 : 1);
					return;
				}
			}
			else
			{
				cameraData.taaHistory.Reset();
				if (cameraData.IsSTPEnabled())
				{
					cameraData.stpHistory.Reset();
				}
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000313DC File Offset: 0x0002F5DC
		private static void UpdateCameraStereoMatrices(Camera camera, XRPass xr)
		{
			if (xr.enabled)
			{
				if (xr.singlePassEnabled)
				{
					for (int i = 0; i < Mathf.Min(2, xr.viewCount); i++)
					{
						camera.SetStereoProjectionMatrix((Camera.StereoscopicEye)i, xr.GetProjMatrix(i));
						camera.SetStereoViewMatrix((Camera.StereoscopicEye)i, xr.GetViewMatrix(i));
					}
					return;
				}
				camera.SetStereoProjectionMatrix((Camera.StereoscopicEye)xr.multipassId, xr.GetProjMatrix(0));
				camera.SetStereoViewMatrix((Camera.StereoscopicEye)xr.multipassId, xr.GetViewMatrix(0));
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00031454 File Offset: 0x0002F654
		private static PerObjectData GetPerObjectLightFlags(int additionalLightsCount, bool isForwardPlus, bool reflectionProbeBlending)
		{
			PerObjectData perObjectData;
			using (new ProfilingScope(UniversalRenderPipeline.Profiling.Pipeline.getPerObjectLightFlags))
			{
				PerObjectData configuration = PerObjectData.LightProbe | PerObjectData.Lightmaps | PerObjectData.OcclusionProbe | PerObjectData.ShadowMask;
				if (!isForwardPlus)
				{
					configuration |= PerObjectData.ReflectionProbes | PerObjectData.LightData;
				}
				else if (!reflectionProbeBlending)
				{
					configuration |= PerObjectData.ReflectionProbes;
				}
				if (additionalLightsCount > 0 && !isForwardPlus && !RenderingUtils.useStructuredBuffer)
				{
					configuration |= PerObjectData.LightIndices;
				}
				perObjectData = configuration;
			}
			return perObjectData;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x000314BC File Offset: 0x0002F6BC
		private static int GetMainLightIndex(UniversalRenderPipelineAsset settings, NativeArray<VisibleLight> visibleLights)
		{
			int num;
			using (new ProfilingScope(UniversalRenderPipeline.Profiling.Pipeline.getMainLightIndex))
			{
				int totalVisibleLights = visibleLights.Length;
				if (totalVisibleLights == 0 || settings.mainLightRenderingMode != LightRenderingMode.PerPixel)
				{
					num = -1;
				}
				else
				{
					Light sunLight = RenderSettings.sun;
					int brightestDirectionalLightIndex = -1;
					float brightestLightIntensity = 0f;
					for (int i = 0; i < totalVisibleLights; i++)
					{
						ref VisibleLight currVisibleLight = ref visibleLights.UnsafeElementAtMutable(i);
						Light currLight = currVisibleLight.light;
						if (currLight == null)
						{
							break;
						}
						if (currVisibleLight.lightType == LightType.Directional)
						{
							if (currLight == sunLight)
							{
								return i;
							}
							if (currLight.intensity > brightestLightIntensity)
							{
								brightestLightIntensity = currLight.intensity;
								brightestDirectionalLightIndex = i;
							}
						}
					}
					num = brightestDirectionalLightIndex;
				}
			}
			return num;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00031584 File Offset: 0x0002F784
		private void SetupPerFrameShaderConstants()
		{
			using (new ProfilingScope(UniversalRenderPipeline.Profiling.Pipeline.setupPerFrameShaderConstants))
			{
				Shader.SetGlobalColor(ShaderPropertyId.rendererColor, Color.white);
				Texture2D ditheringTexture = null;
				LODCrossFadeDitheringType lodCrossFadeDitheringType = UniversalRenderPipeline.asset.lodCrossFadeDitheringType;
				if (lodCrossFadeDitheringType != LODCrossFadeDitheringType.BayerMatrix)
				{
					if (lodCrossFadeDitheringType != LODCrossFadeDitheringType.BlueNoise)
					{
						Debug.LogWarning(string.Format("This Lod Cross Fade Dithering Type is not supported: {0}", UniversalRenderPipeline.asset.lodCrossFadeDitheringType));
					}
					else
					{
						ditheringTexture = this.runtimeTextures.blueNoise64LTex;
					}
				}
				else
				{
					ditheringTexture = this.runtimeTextures.bayerMatrixTex;
				}
				if (ditheringTexture != null)
				{
					Shader.SetGlobalFloat(ShaderPropertyId.ditheringTextureInvSize, 1f / (float)ditheringTexture.width);
					Shader.SetGlobalTexture(ShaderPropertyId.ditheringTexture, ditheringTexture);
				}
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00031648 File Offset: 0x0002F848
		private static void SetupPerCameraShaderConstants(CommandBuffer cmd)
		{
			using (new ProfilingScope(UniversalRenderPipeline.Profiling.Pipeline.setupPerCameraShaderConstants))
			{
				SphericalHarmonicsL2 ambientSH = RenderSettings.ambientProbe;
				Color glossyEnvColor = CoreUtils.ConvertLinearToActiveColorSpace(new Color(ambientSH[0, 0], ambientSH[1, 0], ambientSH[2, 0]) * RenderSettings.reflectionIntensity);
				cmd.SetGlobalVector(ShaderPropertyId.glossyEnvironmentColor, glossyEnvColor);
				cmd.SetGlobalTexture(ShaderPropertyId.glossyEnvironmentCubeMap, ReflectionProbe.defaultTexture);
				cmd.SetGlobalVector(ShaderPropertyId.glossyEnvironmentCubeMapHDR, ReflectionProbe.defaultTextureHDRDecodeValues);
				cmd.SetGlobalVector(ShaderPropertyId.ambientSkyColor, CoreUtils.ConvertSRGBToActiveColorSpace(RenderSettings.ambientSkyColor));
				cmd.SetGlobalVector(ShaderPropertyId.ambientEquatorColor, CoreUtils.ConvertSRGBToActiveColorSpace(RenderSettings.ambientEquatorColor));
				cmd.SetGlobalVector(ShaderPropertyId.ambientGroundColor, CoreUtils.ConvertSRGBToActiveColorSpace(RenderSettings.ambientGroundColor));
				cmd.SetGlobalVector(ShaderPropertyId.subtractiveShadowColor, CoreUtils.ConvertSRGBToActiveColorSpace(RenderSettings.subtractiveShadowColor));
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00031758 File Offset: 0x0002F958
		private unsafe static void CheckAndApplyDebugSettings(ref RenderingData renderingData)
		{
			UniversalRenderPipelineDebugDisplaySettings debugDisplaySettings = DebugDisplaySettings<UniversalRenderPipelineDebugDisplaySettings>.Instance;
			ref CameraData cameraData = ref renderingData.cameraData;
			if (debugDisplaySettings.AreAnySettingsActive && !cameraData.isPreviewCamera)
			{
				DebugDisplaySettingsRendering renderingSettings = debugDisplaySettings.renderingSettings;
				int msaaSamples = cameraData.cameraTargetDescriptor.msaaSamples;
				if (!renderingSettings.enableMsaa)
				{
					msaaSamples = 1;
				}
				if (!renderingSettings.enableHDR)
				{
					*cameraData.isHdrEnabled = false;
				}
				if (!debugDisplaySettings.IsPostProcessingAllowed)
				{
					*cameraData.postProcessEnabled = false;
				}
				*cameraData.hdrColorBufferPrecision = (UniversalRenderPipeline.asset ? UniversalRenderPipeline.asset.hdrColorBufferPrecision : HDRColorBufferPrecision._32Bits);
				cameraData.cameraTargetDescriptor.graphicsFormat = UniversalRenderPipeline.MakeRenderTextureGraphicsFormat(*cameraData.isHdrEnabled, *cameraData.hdrColorBufferPrecision, true);
				cameraData.cameraTargetDescriptor.msaaSamples = msaaSamples;
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00031810 File Offset: 0x0002FA10
		private static ImageUpscalingFilter ResolveUpscalingFilterSelection(Vector2 imageSize, float renderScale, UpscalingFilterSelection selection, bool enableRenderGraph)
		{
			ImageUpscalingFilter filter = ImageUpscalingFilter.Linear;
			if ((selection == UpscalingFilterSelection.FSR && !FSRUtils.IsSupported()) || (selection == UpscalingFilterSelection.STP && (!STP.IsSupported() || !enableRenderGraph)))
			{
				selection = UpscalingFilterSelection.Auto;
			}
			switch (selection)
			{
			case UpscalingFilterSelection.Auto:
			{
				float pixelScale = 1f / renderScale;
				if (Mathf.Approximately(pixelScale - Mathf.Floor(pixelScale), 0f))
				{
					float num = imageSize.x / pixelScale;
					float heightScale = imageSize.y / pixelScale;
					if (Mathf.Approximately(num - Mathf.Floor(num), 0f) && Mathf.Approximately(heightScale - Mathf.Floor(heightScale), 0f))
					{
						filter = ImageUpscalingFilter.Point;
					}
				}
				break;
			}
			case UpscalingFilterSelection.Point:
				filter = ImageUpscalingFilter.Point;
				break;
			case UpscalingFilterSelection.FSR:
				filter = ImageUpscalingFilter.FSR;
				break;
			case UpscalingFilterSelection.STP:
				filter = ImageUpscalingFilter.STP;
				break;
			}
			return filter;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x000318C0 File Offset: 0x0002FAC0
		internal static bool HDROutputForMainDisplayIsActive()
		{
			bool flag = SystemInfo.hdrDisplaySupportFlags.HasFlag(HDRDisplaySupportFlags.Supported) && UniversalRenderPipeline.asset.supportsHDR;
			bool hdrOutputActive = HDROutputSettings.main.available && HDROutputSettings.main.active;
			return flag && hdrOutputActive;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00031910 File Offset: 0x0002FB10
		internal static bool HDROutputForAnyDisplayIsActive()
		{
			bool hdrDisplayOutputActive = UniversalRenderPipeline.HDROutputForMainDisplayIsActive();
			if (XRSystem.displayActive)
			{
				hdrDisplayOutputActive |= XRSystem.isHDRDisplayOutputActive;
			}
			return hdrDisplayOutputActive;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00031934 File Offset: 0x0002FB34
		private void SetHDRState(List<Camera> cameras)
		{
			bool hdrOutputActive = HDROutputSettings.main.available && HDROutputSettings.main.active;
			if (SystemInfo.hdrDisplaySupportFlags.HasFlag(HDRDisplaySupportFlags.RuntimeSwitchable) && !UniversalRenderPipeline.asset.supportsHDR && hdrOutputActive)
			{
				HDROutputSettings.main.RequestHDRModeChange(false);
			}
			if (hdrOutputActive)
			{
				HDROutputSettings.main.automaticHDRTonemapping = false;
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x000319A0 File Offset: 0x0002FBA0
		internal static void GetHDROutputLuminanceParameters(HDROutputUtils.HDRDisplayInformation hdrDisplayInformation, ColorGamut hdrDisplayColorGamut, Tonemapping tonemapping, out Vector4 hdrOutputParameters)
		{
			float minNits = (float)hdrDisplayInformation.minToneMapLuminance;
			float maxNits = (float)hdrDisplayInformation.maxToneMapLuminance;
			float paperWhite = hdrDisplayInformation.paperWhiteNits;
			if (!tonemapping.detectPaperWhite.value)
			{
				paperWhite = tonemapping.paperWhite.value;
			}
			if (!tonemapping.detectBrightnessLimits.value)
			{
				minNits = tonemapping.minNits.value;
				maxNits = tonemapping.maxNits.value;
			}
			hdrOutputParameters = new Vector4(minNits, maxNits, paperWhite, 1f / paperWhite);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00031A18 File Offset: 0x0002FC18
		internal static void GetHDROutputGradingParameters(Tonemapping tonemapping, out Vector4 hdrOutputParameters)
		{
			int eetfMode = 0;
			float hueShift = 0f;
			TonemappingMode value = tonemapping.mode.value;
			if (value != TonemappingMode.Neutral)
			{
				if (value == TonemappingMode.ACES)
				{
					eetfMode = (int)tonemapping.acesPreset.value;
				}
			}
			else
			{
				eetfMode = (int)tonemapping.neutralHDRRangeReductionMode.value;
				hueShift = tonemapping.hueShiftAmount.value;
			}
			hdrOutputParameters = new Vector4((float)eetfMode, hueShift, 0f, 0f);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00031A84 File Offset: 0x0002FC84
		private static AdditionalLightsShadowAtlasLayout BuildAdditionalLightsShadowAtlasLayout(UniversalLightData lightData, UniversalShadowData shadowData, UniversalCameraData cameraData)
		{
			AdditionalLightsShadowAtlasLayout additionalLightsShadowAtlasLayout;
			using (new ProfilingScope(UniversalRenderPipeline.Profiling.Pipeline.buildAdditionalLightsShadowAtlasLayout))
			{
				additionalLightsShadowAtlasLayout = new AdditionalLightsShadowAtlasLayout(lightData, shadowData, cameraData);
			}
			return additionalLightsShadowAtlasLayout;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00031AC8 File Offset: 0x0002FCC8
		private static void AdjustUIOverlayOwnership(int cameraCount)
		{
			if (XRSystem.displayActive || cameraCount == 0)
			{
				SupportedRenderingFeatures.active.rendersUIOverlay = false;
				return;
			}
			SupportedRenderingFeatures.active.rendersUIOverlay = true;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00031AEB File Offset: 0x0002FCEB
		private static void SetupScreenMSAASamplesState(int cameraCount)
		{
			UniversalRenderPipeline.canOptimizeScreenMSAASamples = cameraCount == 1;
			UniversalRenderPipeline.startFrameScreenMSAASamples = Screen.msaaSamples;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00031B00 File Offset: 0x0002FD00
		public static bool IsGameCamera(Camera camera)
		{
			if (camera == null)
			{
				throw new ArgumentNullException("camera");
			}
			return camera.cameraType == CameraType.Game || camera.cameraType == CameraType.VR;
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x00031B2A File Offset: 0x0002FD2A
		public static UniversalRenderPipelineAsset asset
		{
			get
			{
				return GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
			}
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00031B36 File Offset: 0x0002FD36
		private void SortCameras(List<Camera> cameras)
		{
			if (cameras.Count > 1)
			{
				cameras.Sort(this.cameraComparison);
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00031B4D File Offset: 0x0002FD4D
		internal static GraphicsFormat MakeRenderTextureGraphicsFormat(bool isHdrEnabled, HDRColorBufferPrecision requestHDRColorBufferPrecision, bool needsAlpha)
		{
			if (!isHdrEnabled)
			{
				return SystemInfo.GetGraphicsFormat(DefaultFormat.LDR);
			}
			if (!needsAlpha && requestHDRColorBufferPrecision != HDRColorBufferPrecision._64Bits && SystemInfo.IsFormatSupported(GraphicsFormat.B10G11R11_UFloatPack32, GraphicsFormatUsage.Blend))
			{
				return GraphicsFormat.B10G11R11_UFloatPack32;
			}
			if (SystemInfo.IsFormatSupported(GraphicsFormat.R16G16B16A16_SFloat, GraphicsFormatUsage.Blend))
			{
				return GraphicsFormat.R16G16B16A16_SFloat;
			}
			return SystemInfo.GetGraphicsFormat(DefaultFormat.HDR);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00031B82 File Offset: 0x0002FD82
		internal static GraphicsFormat MakeUnormRenderTextureGraphicsFormat()
		{
			if (SystemInfo.IsFormatSupported(GraphicsFormat.A2B10G10R10_UNormPack32, GraphicsFormatUsage.Blend))
			{
				return GraphicsFormat.A2B10G10R10_UNormPack32;
			}
			return GraphicsFormat.R8G8B8A8_UNorm;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00031B94 File Offset: 0x0002FD94
		internal static RenderTextureDescriptor CreateRenderTextureDescriptor(Camera camera, UniversalCameraData cameraData, bool isHdrEnabled, HDRColorBufferPrecision requestHDRColorBufferPrecision, int msaaSamples, bool needsAlpha, bool requiresOpaqueTexture)
		{
			RenderTextureDescriptor desc;
			if (camera.targetTexture == null)
			{
				desc = new RenderTextureDescriptor(cameraData.scaledWidth, cameraData.scaledHeight);
				desc.graphicsFormat = UniversalRenderPipeline.MakeRenderTextureGraphicsFormat(isHdrEnabled, requestHDRColorBufferPrecision, needsAlpha);
				desc.depthStencilFormat = SystemInfo.GetGraphicsFormat(DefaultFormat.DepthStencil);
				desc.msaaSamples = msaaSamples;
				desc.sRGB = QualitySettings.activeColorSpace == ColorSpace.Linear;
			}
			else
			{
				desc = camera.targetTexture.descriptor;
				desc.msaaSamples = msaaSamples;
				desc.width = cameraData.scaledWidth;
				desc.height = cameraData.scaledHeight;
				if (camera.cameraType == CameraType.SceneView && !isHdrEnabled)
				{
					desc.graphicsFormat = SystemInfo.GetGraphicsFormat(DefaultFormat.LDR);
				}
			}
			desc.enableRandomWrite = false;
			desc.bindMS = false;
			desc.useDynamicScale = camera.allowDynamicResolution;
			desc.msaaSamples = SystemInfo.GetRenderTextureSupportedMSAASampleCount(desc);
			if (!SystemInfo.supportsStoreAndResolveAction)
			{
				desc.msaaSamples = 1;
			}
			return desc;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00031C7B File Offset: 0x0002FE7B
		public static void GetLightAttenuationAndSpotDirection(LightType lightType, float lightRange, Matrix4x4 lightLocalToWorldMatrix, float spotAngle, float? innerSpotAngle, out Vector4 lightAttenuation, out Vector4 lightSpotDir)
		{
			lightAttenuation = UniversalRenderPipeline.k_DefaultLightAttenuation;
			lightSpotDir = UniversalRenderPipeline.k_DefaultLightSpotDirection;
			if (lightType != LightType.Directional)
			{
				UniversalRenderPipeline.GetPunctualLightDistanceAttenuation(lightRange, ref lightAttenuation);
				if (lightType == LightType.Spot)
				{
					UniversalRenderPipeline.GetSpotDirection(ref lightLocalToWorldMatrix, out lightSpotDir);
					UniversalRenderPipeline.GetSpotAngleAttenuation(spotAngle, innerSpotAngle, ref lightAttenuation);
				}
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00031CB8 File Offset: 0x0002FEB8
		internal static void GetPunctualLightDistanceAttenuation(float lightRange, ref Vector4 lightAttenuation)
		{
			float lightRangeSqr = lightRange * lightRange;
			float fadeRangeSqr = 0.64000005f * lightRangeSqr - lightRangeSqr;
			float lightRangeSqrOverFadeRangeSqr = -lightRangeSqr / fadeRangeSqr;
			float oneOverLightRangeSqr = 1f / Mathf.Max(0.0001f, lightRangeSqr);
			lightAttenuation.x = oneOverLightRangeSqr;
			lightAttenuation.y = lightRangeSqrOverFadeRangeSqr;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00031CF8 File Offset: 0x0002FEF8
		internal static void GetSpotAngleAttenuation(float spotAngle, float? innerSpotAngle, ref Vector4 lightAttenuation)
		{
			float cosOuterAngle = Mathf.Cos(0.017453292f * spotAngle * 0.5f);
			float cosInnerAngle;
			if (innerSpotAngle != null)
			{
				cosInnerAngle = Mathf.Cos(innerSpotAngle.Value * 0.017453292f * 0.5f);
			}
			else
			{
				cosInnerAngle = Mathf.Cos(2f * Mathf.Atan(Mathf.Tan(spotAngle * 0.5f * 0.017453292f) * 46f / 64f) * 0.5f);
			}
			float smoothAngleRange = Mathf.Max(0.001f, cosInnerAngle - cosOuterAngle);
			float invAngleRange = 1f / smoothAngleRange;
			float add = -cosOuterAngle * invAngleRange;
			lightAttenuation.z = invAngleRange;
			lightAttenuation.w = add;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00031D9C File Offset: 0x0002FF9C
		internal static void GetSpotDirection(ref Matrix4x4 lightLocalToWorldMatrix, out Vector4 lightSpotDir)
		{
			Vector4 dir = lightLocalToWorldMatrix.GetColumn(2);
			lightSpotDir = new Vector4(-dir.x, -dir.y, -dir.z, 0f);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00031DD8 File Offset: 0x0002FFD8
		public static void InitializeLightConstants_Common(NativeArray<VisibleLight> lights, int lightIndex, out Vector4 lightPos, out Vector4 lightColor, out Vector4 lightAttenuation, out Vector4 lightSpotDir, out Vector4 lightOcclusionProbeChannel)
		{
			lightPos = UniversalRenderPipeline.k_DefaultLightPosition;
			lightColor = UniversalRenderPipeline.k_DefaultLightColor;
			lightOcclusionProbeChannel = UniversalRenderPipeline.k_DefaultLightsProbeChannel;
			lightAttenuation = UniversalRenderPipeline.k_DefaultLightAttenuation;
			lightSpotDir = UniversalRenderPipeline.k_DefaultLightSpotDirection;
			if (lightIndex < 0)
			{
				return;
			}
			ref VisibleLight lightData = ref lights.UnsafeElementAtMutable(lightIndex);
			Light light = lightData.light;
			Matrix4x4 lightLocalToWorld = lightData.localToWorldMatrix;
			LightType lightType = lightData.lightType;
			if (lightType == LightType.Directional)
			{
				Vector4 dir = -lightLocalToWorld.GetColumn(2);
				lightPos = new Vector4(dir.x, dir.y, dir.z, 0f);
			}
			else
			{
				Vector4 pos = lightLocalToWorld.GetColumn(3);
				lightPos = new Vector4(pos.x, pos.y, pos.z, 1f);
				UniversalRenderPipeline.GetPunctualLightDistanceAttenuation(lightData.range, ref lightAttenuation);
				if (lightType == LightType.Spot)
				{
					UniversalRenderPipeline.GetSpotAngleAttenuation(lightData.spotAngle, (light != null) ? new float?(light.innerSpotAngle) : null, ref lightAttenuation);
					UniversalRenderPipeline.GetSpotDirection(ref lightLocalToWorld, out lightSpotDir);
				}
			}
			lightColor = lightData.finalColor;
			if (light != null && light.bakingOutput.lightmapBakeType == LightmapBakeType.Mixed && 0 <= light.bakingOutput.occlusionMaskChannel && light.bakingOutput.occlusionMaskChannel < 4)
			{
				lightOcclusionProbeChannel[light.bakingOutput.occlusionMaskChannel] = 1f;
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00031F45 File Offset: 0x00030145
		private static void RecordRenderGraph(RenderGraph renderGraph, ScriptableRenderContext context, ScriptableRenderer renderer)
		{
			renderer.RecordRenderGraph(renderGraph, context);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00031F50 File Offset: 0x00030150
		private static void RecordAndExecuteRenderGraph(RenderGraph renderGraph, ScriptableRenderContext context, ScriptableRenderer renderer, CommandBuffer cmd, Camera camera, string cameraName)
		{
			RenderGraphParameters rgParams = new RenderGraphParameters
			{
				executionName = cameraName,
				commandBuffer = cmd,
				scriptableRenderContext = context,
				currentFrameIndex = Time.frameCount
			};
			renderGraph.BeginRecording(in rgParams);
			UniversalRenderPipeline.RecordRenderGraph(renderGraph, context, renderer);
			renderGraph.EndRecordingAndExecute();
		}

		// Token: 0x040009BE RID: 2494
		public const string k_ShaderTagName = "UniversalPipeline";

		// Token: 0x040009BF RID: 2495
		internal const int k_DefaultRenderingLayerMask = 1;

		// Token: 0x040009C0 RID: 2496
		private readonly DebugDisplaySettingsUI m_DebugDisplaySettingsUI = new DebugDisplaySettingsUI();

		// Token: 0x040009C1 RID: 2497
		private UniversalRenderPipelineGlobalSettings m_GlobalSettings;

		// Token: 0x040009C3 RID: 2499
		internal static bool cameraStackRequiresDepthForPostprocessing = false;

		// Token: 0x040009C4 RID: 2500
		internal static RenderGraph s_RenderGraph;

		// Token: 0x040009C5 RID: 2501
		internal static RTHandleResourcePool s_RTHandlePool;

		// Token: 0x040009C6 RID: 2502
		internal static bool useRenderGraph;

		// Token: 0x040009C7 RID: 2503
		internal bool apvIsEnabled;

		// Token: 0x040009CA RID: 2506
		private readonly UniversalRenderPipelineAsset pipelineAsset;

		// Token: 0x040009CB RID: 2507
		internal bool enableHDROnce = true;

		// Token: 0x040009CC RID: 2508
		private static Vector4 k_DefaultLightPosition = new Vector4(0f, 0f, 1f, 0f);

		// Token: 0x040009CD RID: 2509
		private static Vector4 k_DefaultLightColor = Color.black;

		// Token: 0x040009CE RID: 2510
		private static Vector4 k_DefaultLightAttenuation = new Vector4(0f, 1f, 0f, 1f);

		// Token: 0x040009CF RID: 2511
		private static Vector4 k_DefaultLightSpotDirection = new Vector4(0f, 0f, 1f, 0f);

		// Token: 0x040009D0 RID: 2512
		private static Vector4 k_DefaultLightsProbeChannel = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x040009D1 RID: 2513
		private static List<Vector4> m_ShadowBiasData = new List<Vector4>();

		// Token: 0x040009D2 RID: 2514
		private static List<int> m_ShadowResolutionData = new List<int>();

		// Token: 0x040009D3 RID: 2515
		private Comparison<Camera> cameraComparison = (Camera camera1, Camera camera2) => (int)camera1.depth - (int)camera2.depth;

		// Token: 0x040009D4 RID: 2516
		private static Lightmapping.RequestLightsDelegate lightsDelegate = delegate(Light[] requests, NativeArray<LightDataGI> lightsOutput)
		{
			LightDataGI lightData = default(LightDataGI);
			if (!SupportedRenderingFeatures.active.enlighten || (SupportedRenderingFeatures.active.lightmapBakeTypes | LightmapBakeType.Realtime) == (LightmapBakeType)0)
			{
				for (int i = 0; i < requests.Length; i++)
				{
					Light light = requests[i];
					lightData.InitNoBake(light.GetInstanceID());
					lightsOutput[i] = lightData;
				}
				return;
			}
			for (int j = 0; j < requests.Length; j++)
			{
				Light light2 = requests[j];
				switch (light2.type)
				{
				case LightType.Spot:
				{
					SpotLight spotLight = default(SpotLight);
					LightmapperUtils.Extract(light2, ref spotLight);
					spotLight.innerConeAngle = light2.innerSpotAngle * 0.017453292f;
					spotLight.angularFalloff = AngularFalloffType.AnalyticAndInnerAngle;
					lightData.Init(ref spotLight);
					break;
				}
				case LightType.Directional:
				{
					DirectionalLight directionalLight = default(DirectionalLight);
					LightmapperUtils.Extract(light2, ref directionalLight);
					lightData.Init(ref directionalLight);
					break;
				}
				case LightType.Point:
				{
					PointLight pointLight = default(PointLight);
					LightmapperUtils.Extract(light2, ref pointLight);
					lightData.Init(ref pointLight);
					break;
				}
				case LightType.Area:
					lightData.InitNoBake(light2.GetInstanceID());
					break;
				case LightType.Disc:
					lightData.InitNoBake(light2.GetInstanceID());
					break;
				default:
					lightData.InitNoBake(light2.GetInstanceID());
					break;
				}
				lightData.falloff = FalloffType.InverseSquared;
				lightsOutput[j] = lightData;
			}
		};

		// Token: 0x020001BA RID: 442
		internal static class CameraMetadataCache
		{
			// Token: 0x060009C5 RID: 2501 RVA: 0x00032068 File Offset: 0x00030268
			public static UniversalRenderPipeline.CameraMetadataCache.CameraMetadataCacheEntry GetCached(Camera camera)
			{
				int cameraId = camera.GetHashCode();
				UniversalRenderPipeline.CameraMetadataCache.CameraMetadataCacheEntry result;
				if (!UniversalRenderPipeline.CameraMetadataCache.s_MetadataCache.TryGetValue(cameraId, out result))
				{
					string cameraName = camera.name;
					result = new UniversalRenderPipeline.CameraMetadataCache.CameraMetadataCacheEntry
					{
						name = cameraName,
						sampler = new ProfilingSampler("UniversalRenderPipeline.RenderSingleCameraInternal: " + cameraName)
					};
					UniversalRenderPipeline.CameraMetadataCache.s_MetadataCache.Add(cameraId, result);
				}
				return result;
			}

			// Token: 0x040009D5 RID: 2517
			private static Dictionary<int, UniversalRenderPipeline.CameraMetadataCache.CameraMetadataCacheEntry> s_MetadataCache = new Dictionary<int, UniversalRenderPipeline.CameraMetadataCache.CameraMetadataCacheEntry>();

			// Token: 0x040009D6 RID: 2518
			private static readonly UniversalRenderPipeline.CameraMetadataCache.CameraMetadataCacheEntry k_NoAllocEntry = new UniversalRenderPipeline.CameraMetadataCache.CameraMetadataCacheEntry
			{
				name = "Unknown",
				sampler = new ProfilingSampler("Unknown")
			};

			// Token: 0x020001BB RID: 443
			public class CameraMetadataCacheEntry
			{
				// Token: 0x040009D7 RID: 2519
				public string name;

				// Token: 0x040009D8 RID: 2520
				public ProfilingSampler sampler;
			}
		}

		// Token: 0x020001BC RID: 444
		internal static class Profiling
		{
			// Token: 0x020001BD RID: 445
			public static class Pipeline
			{
				// Token: 0x040009D9 RID: 2521
				private const string k_Name = "UniversalRenderPipeline";

				// Token: 0x040009DA RID: 2522
				public static readonly ProfilingSampler initializeCameraData = new ProfilingSampler("UniversalRenderPipeline.CreateCameraData");

				// Token: 0x040009DB RID: 2523
				public static readonly ProfilingSampler initializeStackedCameraData = new ProfilingSampler("UniversalRenderPipeline.InitializeStackedCameraData");

				// Token: 0x040009DC RID: 2524
				public static readonly ProfilingSampler initializeAdditionalCameraData = new ProfilingSampler("UniversalRenderPipeline.InitializeAdditionalCameraData");

				// Token: 0x040009DD RID: 2525
				public static readonly ProfilingSampler initializeRenderingData = new ProfilingSampler("UniversalRenderPipeline.CreateRenderingData");

				// Token: 0x040009DE RID: 2526
				public static readonly ProfilingSampler initializeShadowData = new ProfilingSampler("UniversalRenderPipeline.CreateShadowData");

				// Token: 0x040009DF RID: 2527
				public static readonly ProfilingSampler initializeLightData = new ProfilingSampler("UniversalRenderPipeline.CreateLightData");

				// Token: 0x040009E0 RID: 2528
				public static readonly ProfilingSampler buildAdditionalLightsShadowAtlasLayout = new ProfilingSampler("UniversalRenderPipeline.BuildAdditionalLightsShadowAtlasLayout");

				// Token: 0x040009E1 RID: 2529
				public static readonly ProfilingSampler getPerObjectLightFlags = new ProfilingSampler("UniversalRenderPipeline.GetPerObjectLightFlags");

				// Token: 0x040009E2 RID: 2530
				public static readonly ProfilingSampler getMainLightIndex = new ProfilingSampler("UniversalRenderPipeline.GetMainLightIndex");

				// Token: 0x040009E3 RID: 2531
				public static readonly ProfilingSampler setupPerFrameShaderConstants = new ProfilingSampler("UniversalRenderPipeline.SetupPerFrameShaderConstants");

				// Token: 0x040009E4 RID: 2532
				public static readonly ProfilingSampler setupPerCameraShaderConstants = new ProfilingSampler("UniversalRenderPipeline.SetupPerCameraShaderConstants");

				// Token: 0x020001BE RID: 446
				public static class Renderer
				{
					// Token: 0x040009E5 RID: 2533
					private const string k_Name = "ScriptableRenderer";

					// Token: 0x040009E6 RID: 2534
					public static readonly ProfilingSampler setupCullingParameters = new ProfilingSampler("ScriptableRenderer.SetupCullingParameters");

					// Token: 0x040009E7 RID: 2535
					public static readonly ProfilingSampler setup = new ProfilingSampler("ScriptableRenderer.Setup");
				}

				// Token: 0x020001BF RID: 447
				public static class Context
				{
					// Token: 0x040009E8 RID: 2536
					private const string k_Name = "ScriptableRenderContext";

					// Token: 0x040009E9 RID: 2537
					public static readonly ProfilingSampler submit = new ProfilingSampler("ScriptableRenderContext.Submit");
				}
			}
		}

		// Token: 0x020001C0 RID: 448
		private readonly struct CameraRenderingScope : IDisposable
		{
			// Token: 0x060009CB RID: 2507 RVA: 0x000321D8 File Offset: 0x000303D8
			public CameraRenderingScope(ScriptableRenderContext context, Camera camera)
			{
				using (new ProfilingScope(UniversalRenderPipeline.CameraRenderingScope.beginCameraRenderingSampler))
				{
					this.m_Context = context;
					this.m_Camera = camera;
					RenderPipeline.BeginCameraRendering(context, camera);
				}
			}

			// Token: 0x060009CC RID: 2508 RVA: 0x00032228 File Offset: 0x00030428
			public void Dispose()
			{
				using (new ProfilingScope(UniversalRenderPipeline.CameraRenderingScope.endCameraRenderingSampler))
				{
					RenderPipeline.EndCameraRendering(this.m_Context, this.m_Camera);
				}
			}

			// Token: 0x040009EA RID: 2538
			private static readonly ProfilingSampler beginCameraRenderingSampler = new ProfilingSampler("RenderPipeline.BeginCameraRendering");

			// Token: 0x040009EB RID: 2539
			private static readonly ProfilingSampler endCameraRenderingSampler = new ProfilingSampler("RenderPipeline.EndCameraRendering");

			// Token: 0x040009EC RID: 2540
			private readonly ScriptableRenderContext m_Context;

			// Token: 0x040009ED RID: 2541
			private readonly Camera m_Camera;
		}

		// Token: 0x020001C1 RID: 449
		private readonly struct ContextRenderingScope : IDisposable
		{
			// Token: 0x060009CE RID: 2510 RVA: 0x00032294 File Offset: 0x00030494
			public ContextRenderingScope(ScriptableRenderContext context, List<Camera> cameras)
			{
				this.m_Context = context;
				this.m_Cameras = cameras;
				using (new ProfilingScope(UniversalRenderPipeline.ContextRenderingScope.beginContextRenderingSampler))
				{
					RenderPipeline.BeginContextRendering(this.m_Context, this.m_Cameras);
				}
			}

			// Token: 0x060009CF RID: 2511 RVA: 0x000322EC File Offset: 0x000304EC
			public void Dispose()
			{
				using (new ProfilingScope(UniversalRenderPipeline.ContextRenderingScope.endContextRenderingSampler))
				{
					RenderPipeline.EndContextRendering(this.m_Context, this.m_Cameras);
				}
			}

			// Token: 0x040009EE RID: 2542
			private static readonly ProfilingSampler beginContextRenderingSampler = new ProfilingSampler("RenderPipeline.BeginContextRendering");

			// Token: 0x040009EF RID: 2543
			private static readonly ProfilingSampler endContextRenderingSampler = new ProfilingSampler("RenderPipeline.EndContextRendering");

			// Token: 0x040009F0 RID: 2544
			private readonly ScriptableRenderContext m_Context;

			// Token: 0x040009F1 RID: 2545
			private readonly List<Camera> m_Cameras;
		}

		// Token: 0x020001C2 RID: 450
		public class SingleCameraRequest
		{
			// Token: 0x040009F2 RID: 2546
			public RenderTexture destination;

			// Token: 0x040009F3 RID: 2547
			public int mipLevel;

			// Token: 0x040009F4 RID: 2548
			public CubemapFace face = CubemapFace.Unknown;

			// Token: 0x040009F5 RID: 2549
			public int slice;
		}
	}
}
