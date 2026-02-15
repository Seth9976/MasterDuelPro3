using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200004D RID: 77
	internal sealed class Renderer2D : ScriptableRenderer
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00010D86 File Offset: 0x0000EF86
		internal bool createColorTexture
		{
			get
			{
				return this.m_CreateColorTexture;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00010D8E File Offset: 0x0000EF8E
		internal bool createDepthTexture
		{
			get
			{
				return this.m_CreateDepthTexture;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00010D96 File Offset: 0x0000EF96
		internal ColorGradingLutPass colorGradingLutPass
		{
			get
			{
				return this.m_PostProcessPasses.colorGradingLutPass;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00010DA3 File Offset: 0x0000EFA3
		internal PostProcessPass postProcessPass
		{
			get
			{
				return this.m_PostProcessPasses.postProcessPass;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00010DB0 File Offset: 0x0000EFB0
		internal PostProcessPass finalPostProcessPass
		{
			get
			{
				return this.m_PostProcessPasses.finalPostProcessPass;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00010DBD File Offset: 0x0000EFBD
		internal RTHandle afterPostProcessColorHandle
		{
			get
			{
				return this.m_PostProcessPasses.afterPostProcessColor;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00010DCA File Offset: 0x0000EFCA
		internal RTHandle colorGradingLutHandle
		{
			get
			{
				return this.m_PostProcessPasses.colorGradingLut;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00010DD7 File Offset: 0x0000EFD7
		public override int SupportedCameraStackingTypes()
		{
			return 3;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00010DDC File Offset: 0x0000EFDC
		public Renderer2D(Renderer2DData data)
			: base(data)
		{
			UniversalRenderPipelineRuntimeShaders shadersResources;
			if (GraphicsSettings.TryGetRenderPipelineSettings<UniversalRenderPipelineRuntimeShaders>(out shadersResources))
			{
				this.m_BlitMaterial = CoreUtils.CreateEngineMaterial(shadersResources.coreBlitPS);
				this.m_BlitHDRMaterial = CoreUtils.CreateEngineMaterial(shadersResources.blitHDROverlay);
				this.m_SamplingMaterial = CoreUtils.CreateEngineMaterial(shadersResources.samplingPS);
			}
			Renderer2DResources renderer2DResources;
			if (GraphicsSettings.TryGetRenderPipelineSettings<Renderer2DResources>(out renderer2DResources))
			{
				this.m_Render2DLightingPass = new Render2DLightingPass(data, this.m_BlitMaterial, this.m_SamplingMaterial, renderer2DResources.fallOffLookup);
				this.m_CopyDepthPass = new CopyDepthPass(RenderPassEvent.AfterRenderingTransparents, renderer2DResources.copyDepthPS, true, false, RenderingUtils.MultisampleDepthResolveSupported(), null);
			}
			this.m_PixelPerfectBackgroundPass = new PixelPerfectBackgroundPass(RenderPassEvent.AfterRenderingTransparents);
			this.m_UpscalePass = new UpscalePass(RenderPassEvent.AfterRenderingPostProcessing, this.m_BlitMaterial);
			this.m_CopyCameraSortingLayerPass = new CopyCameraSortingLayerPass(this.m_BlitMaterial);
			this.m_FinalBlitPass = new FinalBlitPass((RenderPassEvent)1001, this.m_BlitMaterial, this.m_BlitHDRMaterial);
			this.m_DrawOffscreenUIPass = new DrawScreenSpaceUIPass(RenderPassEvent.BeforeRenderingPostProcessing, true);
			this.m_DrawOverlayUIPass = new DrawScreenSpaceUIPass((RenderPassEvent)1002, false);
			this.m_ColorBufferSystem = new RenderTargetBufferSystem("_CameraColorAttachment");
			PostProcessParams ppParams = PostProcessParams.Create();
			ppParams.blitMaterial = this.m_BlitMaterial;
			ppParams.requestColorFormat = GraphicsFormat.B10G11R11_UFloatPack32;
			this.m_PostProcessPasses = new PostProcessPasses(data.postProcessData, ref ppParams);
			this.m_UseDepthStencilBuffer = data.useDepthStencilBuffer;
			this.m_Renderer2DData = data;
			base.supportedRenderingFeatures = new ScriptableRenderer.RenderingFeatures();
			this.m_Renderer2DData.lightCullResult = new Light2DCullResult();
			LensFlareCommonSRP.mergeNeeded = 0;
			LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample = 1;
			LensFlareCommonSRP.Initialize();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00010FA4 File Offset: 0x0000F1A4
		protected override void Dispose(bool disposing)
		{
			this.m_Renderer2DData.Dispose();
			Render2DLightingPass render2DLightingPass = this.m_Render2DLightingPass;
			if (render2DLightingPass != null)
			{
				render2DLightingPass.Dispose();
			}
			this.m_PostProcessPasses.Dispose();
			RTHandle colorTextureHandle = this.m_ColorTextureHandle;
			if (colorTextureHandle != null)
			{
				colorTextureHandle.Release();
			}
			RTHandle depthTextureHandle = this.m_DepthTextureHandle;
			if (depthTextureHandle != null)
			{
				depthTextureHandle.Release();
			}
			this.ReleaseRenderTargets();
			this.m_UpscalePass.Dispose();
			CopyDepthPass copyDepthPass = this.m_CopyDepthPass;
			if (copyDepthPass != null)
			{
				copyDepthPass.Dispose();
			}
			FinalBlitPass finalBlitPass = this.m_FinalBlitPass;
			if (finalBlitPass != null)
			{
				finalBlitPass.Dispose();
			}
			DrawScreenSpaceUIPass drawOffscreenUIPass = this.m_DrawOffscreenUIPass;
			if (drawOffscreenUIPass != null)
			{
				drawOffscreenUIPass.Dispose();
			}
			DrawScreenSpaceUIPass drawOverlayUIPass = this.m_DrawOverlayUIPass;
			if (drawOverlayUIPass != null)
			{
				drawOverlayUIPass.Dispose();
			}
			CoreUtils.Destroy(this.m_BlitMaterial);
			CoreUtils.Destroy(this.m_BlitHDRMaterial);
			CoreUtils.Destroy(this.m_SamplingMaterial);
			this.CleanupRenderGraphResources();
			base.Dispose(disposing);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0001107D File Offset: 0x0000F27D
		internal override void ReleaseRenderTargets()
		{
			this.m_ColorBufferSystem.Dispose();
			this.m_PostProcessPasses.ReleaseRenderTargets();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00011095 File Offset: 0x0000F295
		public Renderer2DData GetRenderer2DData()
		{
			return this.m_Renderer2DData;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000110A0 File Offset: 0x0000F2A0
		private Renderer2D.RenderPassInputSummary GetRenderPassInputs(UniversalCameraData cameraData)
		{
			Renderer2D.RenderPassInputSummary inputSummary = default(Renderer2D.RenderPassInputSummary);
			for (int i = 0; i < base.activeRenderPassQueue.Count; i++)
			{
				ScriptableRenderPass scriptableRenderPass = base.activeRenderPassQueue[i];
				bool needsDepth = (scriptableRenderPass.input & ScriptableRenderPassInput.Depth) > ScriptableRenderPassInput.None;
				bool needsColor = (scriptableRenderPass.input & ScriptableRenderPassInput.Color) > ScriptableRenderPassInput.None;
				inputSummary.requiresDepthTexture = inputSummary.requiresDepthTexture || needsDepth;
				inputSummary.requiresColorTexture = inputSummary.requiresColorTexture || needsColor;
			}
			inputSummary.requiresColorTexture |= cameraData.postProcessEnabled || cameraData.isHdrEnabled || cameraData.isSceneViewCamera || !cameraData.isDefaultViewport || cameraData.requireSrgbConversion || !cameraData.resolveFinalTarget || this.m_Renderer2DData.useCameraSortingLayerTexture || !Mathf.Approximately(cameraData.renderScale, 1f) || (base.DebugHandler != null && base.DebugHandler.WriteToDebugScreenTexture(cameraData.resolveFinalTarget));
			inputSummary.requiresDepthTexture |= !cameraData.resolveFinalTarget && this.m_UseDepthStencilBuffer;
			return inputSummary;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0001119C File Offset: 0x0000F39C
		private void CreateRenderTextures(ref Renderer2D.RenderPassInputSummary renderPassInputs, CommandBuffer cmd, UniversalCameraData cameraData, bool forceCreateColorTexture, FilterMode colorTextureFilterMode, out RTHandle colorTargetHandle, out RTHandle depthTargetHandle)
		{
			ref RenderTextureDescriptor cameraTargetDescriptor = ref cameraData.cameraTargetDescriptor;
			RenderTextureDescriptor colorDescriptor = cameraTargetDescriptor;
			colorDescriptor.depthStencilFormat = GraphicsFormat.None;
			this.m_ColorBufferSystem.SetCameraSettings(colorDescriptor, colorTextureFilterMode);
			if (cameraData.renderType == CameraRenderType.Base)
			{
				this.m_CreateColorTexture = renderPassInputs.requiresColorTexture;
				this.m_CreateDepthTexture = renderPassInputs.requiresDepthTexture;
				this.m_CreateColorTexture = this.m_CreateColorTexture || forceCreateColorTexture;
				this.m_CreateDepthTexture |= this.createColorTexture;
				if (this.createColorTexture)
				{
					if (this.m_ColorBufferSystem.PeekBackBuffer() == null || this.m_ColorBufferSystem.PeekBackBuffer().nameID != BuiltinRenderTextureType.CameraTarget)
					{
						this.m_ColorTextureHandle = this.m_ColorBufferSystem.GetBackBuffer(cmd);
						cmd.SetGlobalTexture("_CameraColorTexture", this.m_ColorTextureHandle.nameID);
						cmd.SetGlobalTexture("_AfterPostProcessTexture", this.m_ColorTextureHandle.nameID);
					}
					this.m_ColorTextureHandle = this.m_ColorBufferSystem.PeekBackBuffer();
				}
				if (this.createDepthTexture)
				{
					RenderTextureDescriptor depthDescriptor = cameraTargetDescriptor;
					depthDescriptor.colorFormat = RenderTextureFormat.Depth;
					depthDescriptor.depthStencilFormat = GraphicsFormat.D32_SFloat_S8_UInt;
					if (!cameraData.resolveFinalTarget && this.m_UseDepthStencilBuffer)
					{
						depthDescriptor.bindMS = depthDescriptor.msaaSamples > 1 && !SystemInfo.supportsMultisampleAutoResolve && SystemInfo.supportsMultisampledTextures != 0;
					}
					RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_DepthTextureHandle, in depthDescriptor, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, "_CameraDepthAttachment");
				}
				colorTargetHandle = (this.createColorTexture ? this.m_ColorTextureHandle : ScriptableRenderer.k_CameraTarget);
				depthTargetHandle = (this.createDepthTexture ? this.m_DepthTextureHandle : ScriptableRenderer.k_CameraTarget);
				return;
			}
			UniversalAdditionalCameraData baseCameraData;
			cameraData.baseCamera.TryGetComponent<UniversalAdditionalCameraData>(out baseCameraData);
			Renderer2D baseRenderer = (Renderer2D)baseCameraData.scriptableRenderer;
			if (this.m_ColorBufferSystem != baseRenderer.m_ColorBufferSystem)
			{
				this.m_ColorBufferSystem.Dispose();
				this.m_ColorBufferSystem = baseRenderer.m_ColorBufferSystem;
			}
			this.m_CreateColorTexture = true;
			this.m_CreateDepthTexture = true;
			this.m_ColorTextureHandle = baseRenderer.m_ColorTextureHandle;
			this.m_DepthTextureHandle = baseRenderer.m_DepthTextureHandle;
			colorTargetHandle = this.m_ColorTextureHandle;
			depthTargetHandle = this.m_DepthTextureHandle;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000113B0 File Offset: 0x0000F5B0
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void Setup(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			UniversalRenderingData universalRenderingData = base.frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = base.frameData.Get<UniversalCameraData>();
			UniversalPostProcessingData postProcessingData = base.frameData.Get<UniversalPostProcessingData>();
			ref RenderTextureDescriptor cameraTargetDescriptor = ref cameraData.cameraTargetDescriptor;
			bool stackHasPostProcess = postProcessingData.isEnabled && this.m_PostProcessPasses.isCreated;
			bool hasPostProcess = cameraData.postProcessEnabled && this.m_PostProcessPasses.isCreated;
			bool lastCameraInStack = cameraData.resolveFinalTarget;
			FilterMode colorTextureFilterMode = FilterMode.Bilinear;
			PixelPerfectCamera ppc = null;
			bool ppcUsesOffscreenRT = false;
			bool ppcUpscaleRT = false;
			if (base.DebugHandler != null)
			{
				if (base.DebugHandler.AreAnySettingsActive)
				{
					stackHasPostProcess = stackHasPostProcess && base.DebugHandler.IsPostProcessingAllowed;
					hasPostProcess = hasPostProcess && base.DebugHandler.IsPostProcessingAllowed;
				}
				if (base.DebugHandler.IsActiveForCamera(cameraData.isPreviewCamera))
				{
					if (base.DebugHandler.WriteToDebugScreenTexture(cameraData.resolveFinalTarget))
					{
						RenderTextureDescriptor descriptor = cameraData.cameraTargetDescriptor;
						DebugHandler.ConfigureColorDescriptorForDebugScreen(ref descriptor, cameraData.pixelWidth, cameraData.pixelHeight);
						RenderingUtils.ReAllocateHandleIfNeeded(base.DebugHandler.DebugScreenColorHandle, in descriptor, FilterMode.Point, TextureWrapMode.Repeat, 1, 0f, "_DebugScreenColor");
						RenderTextureDescriptor depthDesc = cameraData.cameraTargetDescriptor;
						DebugHandler.ConfigureDepthDescriptorForDebugScreen(ref depthDesc, GraphicsFormat.D32_SFloat_S8_UInt, cameraData.pixelWidth, cameraData.pixelHeight);
						RenderingUtils.ReAllocateHandleIfNeeded(base.DebugHandler.DebugScreenDepthHandle, in depthDesc, FilterMode.Point, TextureWrapMode.Repeat, 1, 0f, "_DebugScreenDepth");
					}
					if (base.DebugHandler.HDRDebugViewIsActive(cameraData.resolveFinalTarget))
					{
						base.DebugHandler.hdrDebugViewPass.Setup(cameraData, base.DebugHandler.DebugDisplaySettings.lightingSettings.hdrDebugMode);
						base.EnqueuePass(base.DebugHandler.hdrDebugViewPass);
					}
				}
			}
			if (cameraData.renderType == CameraRenderType.Base && lastCameraInStack)
			{
				cameraData.camera.TryGetComponent<PixelPerfectCamera>(out ppc);
				if (ppc != null && ppc.enabled)
				{
					if (ppc.offscreenRTSize != Vector2Int.zero)
					{
						ppcUsesOffscreenRT = true;
						cameraTargetDescriptor.width = ppc.offscreenRTSize.x;
						cameraTargetDescriptor.height = ppc.offscreenRTSize.y;
						FullScreenPassRendererFeature.FullScreenRenderPass fullScreenRenderPass = base.activeRenderPassQueue.Find((ScriptableRenderPass x) => x is FullScreenPassRendererFeature.FullScreenRenderPass) as FullScreenPassRendererFeature.FullScreenRenderPass;
						if (fullScreenRenderPass != null)
						{
							fullScreenRenderPass.ReAllocate(cameraTargetDescriptor);
						}
					}
					colorTextureFilterMode = FilterMode.Point;
					ppcUpscaleRT = ppc.gridSnapping == PixelPerfectCamera.GridSnapping.UpscaleRenderTexture || ppc.requiresUpscalePass;
				}
			}
			Renderer2D.RenderPassInputSummary renderPassInputs = this.GetRenderPassInputs(cameraData);
			CommandBuffer cmd = universalRenderingData.commandBuffer;
			RTHandle colorTargetHandle;
			RTHandle depthTargetHandle;
			using (new ProfilingScope(cmd, Renderer2D.m_ProfilingSampler))
			{
				this.CreateRenderTextures(ref renderPassInputs, cmd, cameraData, ppcUsesOffscreenRT, colorTextureFilterMode, out colorTargetHandle, out depthTargetHandle);
			}
			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();
			base.ConfigureCameraTarget(colorTargetHandle, depthTargetHandle);
			if (hasPostProcess)
			{
				RenderTextureDescriptor desc;
				FilterMode filterMode;
				this.colorGradingLutPass.ConfigureDescriptor(in postProcessingData, out desc, out filterMode);
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_PostProcessPasses.m_ColorGradingLut, in desc, filterMode, TextureWrapMode.Clamp, 1, 0f, "_InternalGradingLut");
				ColorGradingLutPass colorGradingLutPass = this.colorGradingLutPass;
				RTHandle rthandle = this.colorGradingLutHandle;
				colorGradingLutPass.Setup(in rthandle);
				base.EnqueuePass(this.colorGradingLutPass);
			}
			this.m_Render2DLightingPass.Setup(renderPassInputs.requiresDepthTexture || this.m_UseDepthStencilBuffer);
			this.m_Render2DLightingPass.ConfigureTarget(colorTargetHandle, depthTargetHandle);
			base.EnqueuePass(this.m_Render2DLightingPass);
			bool rendersOverlayUI = cameraData.rendersOverlayUI;
			bool outputToHDR = cameraData.isHDROutputActive;
			if (rendersOverlayUI && outputToHDR)
			{
				this.m_DrawOffscreenUIPass.Setup(cameraData, GraphicsFormat.D32_SFloat_S8_UInt);
				base.EnqueuePass(this.m_DrawOffscreenUIPass);
			}
			bool isFXAAEnabled = cameraData.antialiasing == AntialiasingMode.FastApproximateAntialiasing && !outputToHDR;
			bool requireFinalPostProcessPass = lastCameraInStack && !ppcUpscaleRT && stackHasPostProcess && isFXAAEnabled;
			bool hasPassesAfterPostProcessing = base.activeRenderPassQueue.Find((ScriptableRenderPass x) => x.renderPassEvent == RenderPassEvent.AfterRenderingPostProcessing) != null;
			bool needsColorEncoding = base.DebugHandler == null || !base.DebugHandler.HDRDebugViewIsActive(cameraData.resolveFinalTarget);
			if (hasPostProcess)
			{
				RenderTextureDescriptor desc2 = PostProcessPass.GetCompatibleDescriptor(cameraTargetDescriptor, cameraTargetDescriptor.width, cameraTargetDescriptor.height, cameraTargetDescriptor.graphicsFormat, GraphicsFormat.None);
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_PostProcessPasses.m_AfterPostProcessColor, in desc2, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, "_AfterPostProcessTexture");
				PostProcessPass postProcessPass = this.postProcessPass;
				ref RenderTextureDescriptor ptr = ref cameraTargetDescriptor;
				RTHandle afterPostProcessColorHandle = this.afterPostProcessColorHandle;
				RTHandle rthandle = this.colorGradingLutHandle;
				postProcessPass.Setup(in ptr, in colorTargetHandle, afterPostProcessColorHandle, in depthTargetHandle, in rthandle, requireFinalPostProcessPass, this.afterPostProcessColorHandle.nameID == ScriptableRenderer.k_CameraTarget.nameID && needsColorEncoding);
				base.EnqueuePass(this.postProcessPass);
			}
			RTHandle finalTargetHandle = colorTargetHandle;
			if (ppc != null && ppc.enabled && ppc.cropFrame != PixelPerfectCamera.CropFrame.None)
			{
				base.EnqueuePass(this.m_PixelPerfectBackgroundPass);
				if (ppc.requiresUpscalePass)
				{
					int upscaleWidth = ppc.refResolutionX * ppc.pixelRatio;
					int upscaleHeight = ppc.refResolutionY * ppc.pixelRatio;
					this.m_UpscalePass.Setup(colorTargetHandle, upscaleWidth, upscaleHeight, ppc.finalBlitFilterMode, cameraData.cameraTargetDescriptor, out finalTargetHandle);
					base.EnqueuePass(this.m_UpscalePass);
				}
			}
			if (requireFinalPostProcessPass)
			{
				this.finalPostProcessPass.SetupFinalPass(in finalTargetHandle, hasPassesAfterPostProcessing, needsColorEncoding);
				base.EnqueuePass(this.finalPostProcessPass);
			}
			else if (lastCameraInStack && finalTargetHandle != ScriptableRenderer.k_CameraTarget)
			{
				this.m_FinalBlitPass.Setup(cameraTargetDescriptor, finalTargetHandle);
				base.EnqueuePass(this.m_FinalBlitPass);
			}
			if (rendersOverlayUI && !outputToHDR)
			{
				base.EnqueuePass(this.m_DrawOverlayUIPass);
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0001193C File Offset: 0x0000FB3C
		public unsafe override void SetupCullingParameters(ref ScriptableCullingParameters cullingParameters, ref CameraData cameraData)
		{
			cullingParameters.cullingOptions = CullingOptions.None;
			cullingParameters.isOrthographic = cameraData.camera->orthographic;
			cullingParameters.shadowDistance = 0f;
			(this.m_Renderer2DData.lightCullResult as Light2DCullResult).SetupCulling(ref cullingParameters, *cameraData.camera);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0001198C File Offset: 0x0000FB8C
		internal override void SwapColorBuffer(CommandBuffer cmd)
		{
			this.m_ColorBufferSystem.Swap();
			if (this.m_DepthTextureHandle.nameID != BuiltinRenderTextureType.CameraTarget)
			{
				base.ConfigureCameraTarget(this.m_ColorBufferSystem.GetBackBuffer(cmd), this.m_DepthTextureHandle);
			}
			else
			{
				base.ConfigureCameraColorTarget(this.m_ColorBufferSystem.GetBackBuffer(cmd));
			}
			this.m_ColorTextureHandle = this.m_ColorBufferSystem.GetBackBuffer(cmd);
			cmd.SetGlobalTexture("_CameraColorTexture", this.m_ColorTextureHandle.nameID);
			cmd.SetGlobalTexture("_AfterPostProcessTexture", this.m_ColorTextureHandle.nameID);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00011A26 File Offset: 0x0000FC26
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		internal override RTHandle GetCameraColorFrontBuffer(CommandBuffer cmd)
		{
			return this.m_ColorBufferSystem.GetFrontBuffer(cmd);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00011A34 File Offset: 0x0000FC34
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		internal override RTHandle GetCameraColorBackBuffer(CommandBuffer cmd)
		{
			return this.m_ColorBufferSystem.GetBackBuffer(cmd);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00011A42 File Offset: 0x0000FC42
		internal override void EnableSwapBufferMSAA(bool enable)
		{
			this.m_ColorBufferSystem.EnableMSAA(enable);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00011A50 File Offset: 0x0000FC50
		internal static bool IsGLDevice()
		{
			return SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3 || SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLCore;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00011A66 File Offset: 0x0000FC66
		internal static bool supportsMRT
		{
			get
			{
				return !Renderer2D.IsGLDevice();
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00011A70 File Offset: 0x0000FC70
		internal override bool supportsNativeRenderPassRendergraphCompiler
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00011A73 File Offset: 0x0000FC73
		private RTHandle currentRenderGraphCameraColorHandle
		{
			get
			{
				return this.m_RenderGraphCameraColorHandles[Renderer2D.m_CurrentColorHandle];
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00011A81 File Offset: 0x0000FC81
		private RTHandle nextRenderGraphCameraColorHandle
		{
			get
			{
				Renderer2D.m_CurrentColorHandle = (Renderer2D.m_CurrentColorHandle + 1) % 2;
				return this.currentRenderGraphCameraColorHandle;
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00011A98 File Offset: 0x0000FC98
		private Renderer2D.ImportResourceSummary GetImportResourceSummary(RenderGraph renderGraph, UniversalCameraData cameraData)
		{
			Renderer2D.ImportResourceSummary output = default(Renderer2D.ImportResourceSummary);
			bool clearColor = cameraData.renderType == CameraRenderType.Base;
			bool clearDepth = cameraData.renderType == CameraRenderType.Base || cameraData.clearDepth;
			bool clearBackbufferOnFirstUse = cameraData.renderType == CameraRenderType.Base && !this.m_CreateColorTexture;
			Color cameraBackgroundColor = ((cameraData.camera.clearFlags == CameraClearFlags.Nothing) ? Color.yellow : cameraData.backgroundColor);
			if (base.IsSceneFilteringEnabled(cameraData.camera))
			{
				cameraBackgroundColor.a = 0f;
				clearDepth = false;
			}
			DebugHandler debugHandler = cameraData.renderer.DebugHandler;
			if (debugHandler != null && debugHandler.IsActiveForCamera(cameraData.isPreviewCamera) && debugHandler.IsScreenClearNeeded)
			{
				clearColor = true;
				clearDepth = true;
			}
			output.cameraColorParams.clearOnFirstUse = clearColor;
			output.cameraColorParams.clearColor = cameraBackgroundColor;
			output.cameraColorParams.discardOnLastUse = false;
			output.cameraDepthParams.clearOnFirstUse = clearDepth;
			output.cameraDepthParams.clearColor = cameraBackgroundColor;
			output.cameraDepthParams.discardOnLastUse = false;
			output.backBufferColorParams.clearOnFirstUse = clearBackbufferOnFirstUse;
			output.backBufferColorParams.clearColor = cameraBackgroundColor;
			output.backBufferColorParams.discardOnLastUse = false;
			output.backBufferDepthParams.clearOnFirstUse = clearBackbufferOnFirstUse;
			output.backBufferDepthParams.clearColor = cameraBackgroundColor;
			output.backBufferDepthParams.discardOnLastUse = true;
			if (cameraData.targetTexture != null)
			{
				output.importInfo.width = cameraData.targetTexture.width;
				output.importInfo.height = cameraData.targetTexture.height;
				output.importInfo.volumeDepth = cameraData.targetTexture.volumeDepth;
				output.importInfo.msaaSamples = cameraData.targetTexture.antiAliasing;
				output.importInfo.format = cameraData.targetTexture.graphicsFormat;
				output.importInfoDepth = output.importInfo;
				output.importInfoDepth.format = cameraData.targetTexture.depthStencilFormat;
				if (output.importInfoDepth.format == GraphicsFormat.None)
				{
					output.importInfoDepth.format = SystemInfo.GetGraphicsFormat(DefaultFormat.DepthStencil);
					Debug.LogWarning("In the render graph API, the output Render Texture must have a depth buffer. When you select a Render Texture in any camera's Output Texture property, the Depth Stencil Format property of the texture must be set to a value other than None.");
				}
			}
			else
			{
				int numSamples = base.AdjustAndGetScreenMSAASamples(renderGraph, this.m_CreateColorTexture);
				output.importInfo.width = Screen.width;
				output.importInfo.height = Screen.height;
				output.importInfo.volumeDepth = 1;
				output.importInfo.msaaSamples = numSamples;
				output.importInfo.format = UniversalRenderPipeline.MakeRenderTextureGraphicsFormat(cameraData.isHdrEnabled, cameraData.hdrColorBufferPrecision, Graphics.preserveFramebufferAlpha);
				output.importInfoDepth = output.importInfo;
				output.importInfoDepth.format = SystemInfo.GetGraphicsFormat(DefaultFormat.DepthStencil);
			}
			return output;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00011D4C File Offset: 0x0000FF4C
		private void InitializeLayerBatches()
		{
			Universal2DResourceData resourceData = base.frameData.Get<Universal2DResourceData>();
			this.m_LayerBatches = LayerUtility.CalculateBatches(this.m_Renderer2DData.lightCullResult, out this.m_BatchCount);
			if (resourceData.normalsTexture.Length != this.m_BatchCount)
			{
				resourceData.normalsTexture = new TextureHandle[this.m_BatchCount];
			}
			if (resourceData.lightTextures.Length != this.m_BatchCount)
			{
				resourceData.lightTextures = new TextureHandle[this.m_BatchCount][];
			}
			for (int i = 0; i < resourceData.lightTextures.Length; i++)
			{
				if (resourceData.lightTextures[i] == null || resourceData.lightTextures[i].Length != this.m_LayerBatches[i].activeBlendStylesIndices.Length)
				{
					resourceData.lightTextures[i] = new TextureHandle[this.m_LayerBatches[i].activeBlendStylesIndices.Length];
				}
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00011E20 File Offset: 0x00010020
		private void CreateResources(RenderGraph renderGraph)
		{
			Universal2DResourceData universal2DResourceData = base.frameData.Get<Universal2DResourceData>();
			UniversalResourceData commonResourceData = base.frameData.Get<UniversalResourceData>();
			UniversalCameraData cameraData = base.frameData.Get<UniversalCameraData>();
			ref RenderTextureDescriptor cameraTargetDescriptor = ref cameraData.cameraTargetDescriptor;
			FilterMode cameraTargetFilterMode = FilterMode.Bilinear;
			bool lastCameraInTheStack = cameraData.resolveFinalTarget;
			bool forceCreateColorTexture = false;
			if (cameraData.renderType == CameraRenderType.Base && lastCameraInTheStack)
			{
				PixelPerfectCamera ppc;
				cameraData.camera.TryGetComponent<PixelPerfectCamera>(out ppc);
				if (ppc != null && ppc.enabled)
				{
					if (ppc.offscreenRTSize != Vector2Int.zero)
					{
						forceCreateColorTexture = true;
						cameraTargetDescriptor.width = ppc.offscreenRTSize.x;
						cameraTargetDescriptor.height = ppc.offscreenRTSize.y;
					}
					cameraTargetFilterMode = FilterMode.Point;
					this.ppcUpscaleRT = ppc.gridSnapping == PixelPerfectCamera.GridSnapping.UpscaleRenderTexture || ppc.requiresUpscalePass;
					if (ppc.requiresUpscalePass)
					{
						RenderTextureDescriptor upscaleDescriptor = cameraTargetDescriptor;
						upscaleDescriptor.width = ppc.refResolutionX * ppc.pixelRatio;
						upscaleDescriptor.height = ppc.refResolutionY * ppc.pixelRatio;
						upscaleDescriptor.depthStencilFormat = GraphicsFormat.None;
						universal2DResourceData.upscaleTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, upscaleDescriptor, "_UpscaleTexture", true, ppc.finalBlitFilterMode, TextureWrapMode.Clamp);
					}
				}
			}
			float renderTextureScale = this.m_Renderer2DData.lightRenderTextureScale;
			int width = (int)Mathf.Max(1f, (float)cameraData.cameraTargetDescriptor.width * renderTextureScale);
			int height = (int)Mathf.Max(1f, (float)cameraData.cameraTargetDescriptor.height * renderTextureScale);
			universal2DResourceData.intermediateDepth = UniversalRenderer.CreateRenderGraphTexture(renderGraph, new RenderTextureDescriptor(width, height)
			{
				colorFormat = RenderTextureFormat.Depth,
				depthStencilFormat = GraphicsFormat.D32_SFloat_S8_UInt,
				width = width,
				height = height
			}, "DepthTexture", true, FilterMode.Point, TextureWrapMode.Clamp);
			RenderTextureDescriptor desc = new RenderTextureDescriptor(width, height);
			desc.graphicsFormat = RendererLighting.GetRenderTextureFormat();
			desc.autoGenerateMips = false;
			desc.depthStencilFormat = GraphicsFormat.None;
			for (int i = 0; i < universal2DResourceData.normalsTexture.Length; i++)
			{
				universal2DResourceData.normalsTexture[i] = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, "_NormalMap", true, RendererLighting.k_NormalClearColor, FilterMode.Point, TextureWrapMode.Clamp);
			}
			for (int j = 0; j < universal2DResourceData.lightTextures.Length; j++)
			{
				for (int k = 0; k < this.m_LayerBatches[j].activeBlendStylesIndices.Length; k++)
				{
					int index = this.m_LayerBatches[j].activeBlendStylesIndices[k];
					Color clearColor;
					if (!Light2DManager.GetGlobalColor(this.m_LayerBatches[j].startLayerID, index, out clearColor))
					{
						clearColor = Color.black;
					}
					universal2DResourceData.lightTextures[j][k] = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, RendererLighting.k_ShapeLightTextureIDs[index], true, clearColor, FilterMode.Bilinear, TextureWrapMode.Clamp);
				}
			}
			universal2DResourceData.shadowsTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, new RenderTextureDescriptor(width, height)
			{
				graphicsFormat = GraphicsFormat.B10G11R11_UFloatPack32,
				autoGenerateMips = false,
				depthStencilFormat = GraphicsFormat.None
			}, "_ShadowTex", false, FilterMode.Bilinear, TextureWrapMode.Clamp);
			universal2DResourceData.shadowsDepth = UniversalRenderer.CreateRenderGraphTexture(renderGraph, new RenderTextureDescriptor(width, height)
			{
				graphicsFormat = GraphicsFormat.None,
				autoGenerateMips = false,
				depthStencilFormat = GraphicsFormat.D32_SFloat_S8_UInt
			}, "_ShadowDepth", false, FilterMode.Bilinear, TextureWrapMode.Clamp);
			if (this.m_Renderer2DData.useCameraSortingLayerTexture)
			{
				RenderTextureDescriptor descriptor = cameraTargetDescriptor;
				descriptor.msaaSamples = 1;
				FilterMode filterMode;
				CopyCameraSortingLayerPass.ConfigureDescriptor(this.m_Renderer2DData.cameraSortingLayerDownsamplingMethod, ref descriptor, out filterMode);
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_CameraSortingLayerHandle, in descriptor, filterMode, TextureWrapMode.Clamp, 1, 0f, CopyCameraSortingLayerPass.k_CameraSortingLayerTexture);
				universal2DResourceData.cameraSortingLayerTexture = renderGraph.ImportTexture(this.m_CameraSortingLayerHandle);
			}
			if (cameraData.renderType == CameraRenderType.Base)
			{
				Renderer2D.RenderPassInputSummary renderPassInputs = this.GetRenderPassInputs(cameraData);
				this.m_CreateColorTexture = renderPassInputs.requiresColorTexture;
				this.m_CreateDepthTexture = renderPassInputs.requiresDepthTexture;
				this.m_CreateColorTexture = this.m_CreateColorTexture || forceCreateColorTexture;
				this.m_CreateDepthTexture |= this.createColorTexture;
				if (this.createColorTexture)
				{
					cameraTargetDescriptor.useMipMap = false;
					cameraTargetDescriptor.autoGenerateMips = false;
					cameraTargetDescriptor.depthStencilFormat = GraphicsFormat.None;
					RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_RenderGraphCameraColorHandles[0], in cameraTargetDescriptor, cameraTargetFilterMode, TextureWrapMode.Clamp, 1, 0f, "_CameraTargetAttachmentA");
					RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_RenderGraphCameraColorHandles[1], in cameraTargetDescriptor, cameraTargetFilterMode, TextureWrapMode.Clamp, 1, 0f, "_CameraTargetAttachmentB");
					commonResourceData.activeColorID = UniversalResourceDataBase.ActiveID.Camera;
				}
				else
				{
					commonResourceData.activeColorID = UniversalResourceDataBase.ActiveID.BackBuffer;
				}
				if (this.createDepthTexture)
				{
					RenderTextureDescriptor depthDescriptor = cameraData.cameraTargetDescriptor;
					depthDescriptor.useMipMap = false;
					depthDescriptor.autoGenerateMips = false;
					depthDescriptor.bindMS = false;
					bool flag = depthDescriptor.msaaSamples > 1 && SystemInfo.supportsMultisampledTextures != 0;
					bool resolveDepth = RenderingUtils.MultisampleDepthResolveSupported() && renderGraph.nativeRenderPassesEnabled;
					if (this.m_CopyDepthPass != null)
					{
						this.m_CopyDepthPass.m_CopyResolvedDepth = resolveDepth;
					}
					if (flag)
					{
						depthDescriptor.bindMS = !resolveDepth;
					}
					depthDescriptor.graphicsFormat = GraphicsFormat.None;
					depthDescriptor.depthStencilFormat = GraphicsFormat.D32_SFloat_S8_UInt;
					RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_RenderGraphCameraDepthHandle, in depthDescriptor, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, "_CameraDepthAttachment");
					commonResourceData.activeDepthID = UniversalResourceDataBase.ActiveID.Camera;
				}
				else
				{
					commonResourceData.activeDepthID = UniversalResourceDataBase.ActiveID.BackBuffer;
				}
			}
			else
			{
				UniversalAdditionalCameraData baseCameraData;
				cameraData.baseCamera.TryGetComponent<UniversalAdditionalCameraData>(out baseCameraData);
				Renderer2D baseRenderer = (Renderer2D)baseCameraData.scriptableRenderer;
				this.m_RenderGraphCameraColorHandles = baseRenderer.m_RenderGraphCameraColorHandles;
				this.m_RenderGraphCameraDepthHandle = baseRenderer.m_RenderGraphCameraDepthHandle;
				this.m_RenderGraphBackbufferColorHandle = baseRenderer.m_RenderGraphBackbufferColorHandle;
				this.m_RenderGraphBackbufferDepthHandle = baseRenderer.m_RenderGraphBackbufferDepthHandle;
				this.m_CreateColorTexture = baseRenderer.m_CreateColorTexture;
				this.m_CreateDepthTexture = baseRenderer.m_CreateDepthTexture;
			}
			Renderer2D.ImportResourceSummary importSummary = this.GetImportResourceSummary(renderGraph, cameraData);
			if (this.m_CreateColorTexture)
			{
				importSummary.cameraColorParams.discardOnLastUse = lastCameraInTheStack;
				importSummary.cameraDepthParams.discardOnLastUse = lastCameraInTheStack;
				commonResourceData.cameraColor = renderGraph.ImportTexture(this.currentRenderGraphCameraColorHandle, importSummary.cameraColorParams);
				commonResourceData.cameraDepth = renderGraph.ImportTexture(this.m_RenderGraphCameraDepthHandle, importSummary.cameraDepthParams);
			}
			RenderTargetIdentifier targetColorId = ((cameraData.targetTexture != null) ? new RenderTargetIdentifier(cameraData.targetTexture) : BuiltinRenderTextureType.CameraTarget);
			RenderTargetIdentifier targetDepthId = ((cameraData.targetTexture != null) ? new RenderTargetIdentifier(cameraData.targetTexture) : BuiltinRenderTextureType.Depth);
			if (this.m_RenderGraphBackbufferColorHandle == null)
			{
				this.m_RenderGraphBackbufferColorHandle = RTHandles.Alloc(targetColorId, "Backbuffer color");
			}
			else if (this.m_RenderGraphBackbufferColorHandle.nameID != targetColorId)
			{
				RTHandleStaticHelpers.SetRTHandleUserManagedWrapper(ref this.m_RenderGraphBackbufferColorHandle, targetColorId);
			}
			if (this.m_RenderGraphBackbufferDepthHandle == null)
			{
				this.m_RenderGraphBackbufferDepthHandle = RTHandles.Alloc(targetDepthId, "Backbuffer depth");
			}
			else if (this.m_RenderGraphBackbufferDepthHandle.nameID != targetDepthId)
			{
				RTHandleStaticHelpers.SetRTHandleUserManagedWrapper(ref this.m_RenderGraphBackbufferDepthHandle, targetDepthId);
			}
			commonResourceData.backBufferColor = renderGraph.ImportTexture(this.m_RenderGraphBackbufferColorHandle, importSummary.importInfo, importSummary.backBufferColorParams);
			commonResourceData.backBufferDepth = renderGraph.ImportTexture(this.m_RenderGraphBackbufferDepthHandle, importSummary.importInfoDepth, importSummary.backBufferDepthParams);
			RenderTextureDescriptor postProcessDesc = PostProcessPass.GetCompatibleDescriptor(cameraTargetDescriptor, cameraTargetDescriptor.width, cameraTargetDescriptor.height, cameraTargetDescriptor.graphicsFormat, GraphicsFormat.None);
			commonResourceData.afterPostProcessColor = UniversalRenderer.CreateRenderGraphTexture(renderGraph, postProcessDesc, "_AfterPostProcessTexture", true, FilterMode.Point, TextureWrapMode.Clamp);
			if (this.RequiresDepthCopyPass(cameraData))
			{
				this.CreateCameraDepthCopyTexture(renderGraph, cameraTargetDescriptor);
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00012544 File Offset: 0x00010744
		private bool RequiresDepthCopyPass(UniversalCameraData cameraData)
		{
			Renderer2D.RenderPassInputSummary renderPassInputs = this.GetRenderPassInputs(cameraData);
			return ((cameraData.postProcessEnabled && this.m_PostProcessPasses.isCreated && cameraData.postProcessingRequiresDepthTexture) || renderPassInputs.requiresDepthTexture) && this.m_CreateDepthTexture;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0001258C File Offset: 0x0001078C
		private void CreateCameraDepthCopyTexture(RenderGraph renderGraph, RenderTextureDescriptor descriptor)
		{
			UniversalResourceData universalResourceData = base.frameData.Get<UniversalResourceData>();
			RenderTextureDescriptor depthDescriptor = descriptor;
			depthDescriptor.msaaSamples = 1;
			depthDescriptor.graphicsFormat = GraphicsFormat.R32_SFloat;
			depthDescriptor.depthStencilFormat = GraphicsFormat.None;
			universalResourceData.cameraDepthTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, depthDescriptor, "_CameraDepthTexture", true, FilterMode.Point, TextureWrapMode.Clamp);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000125D4 File Offset: 0x000107D4
		public override void OnBeginRenderGraphFrame()
		{
			Universal2DResourceData universal2DResourceData = base.frameData.Create<Universal2DResourceData>();
			UniversalResourceDataBase orCreate = base.frameData.GetOrCreate<UniversalResourceData>();
			universal2DResourceData.InitFrame();
			orCreate.InitFrame();
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00012604 File Offset: 0x00010804
		internal void RecordCustomRenderGraphPasses(RenderGraph renderGraph, RenderPassEvent2D activeRPEvent)
		{
			foreach (ScriptableRenderPass pass in base.activeRenderPassQueue)
			{
				RenderPassEvent2D rpEvent;
				int rpLayer;
				pass.GetInjectionPoint2D(out rpEvent, out rpLayer);
				if (rpEvent == activeRPEvent)
				{
					pass.RecordRenderGraph(renderGraph, base.frameData);
				}
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0001266C File Offset: 0x0001086C
		internal override void OnRecordRenderGraph(RenderGraph renderGraph, ScriptableRenderContext context)
		{
			UniversalResourceData commonResourceData = base.frameData.GetOrCreate<UniversalResourceData>();
			this.InitializeLayerBatches();
			this.CreateResources(renderGraph);
			base.SetupRenderGraphCameraProperties(renderGraph, commonResourceData.isActiveTargetBackBuffer);
			this.OnBeforeRendering(renderGraph);
			this.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent2D.BeforeRendering);
			this.OnMainRendering(renderGraph);
			this.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent2D.BeforeRenderingPostProcessing);
			this.OnAfterRendering(renderGraph);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000126C8 File Offset: 0x000108C8
		public override void OnEndRenderGraphFrame()
		{
			Universal2DResourceData universal2DResourceData = base.frameData.Get<Universal2DResourceData>();
			UniversalResourceDataBase universalResourceDataBase = base.frameData.Get<UniversalResourceData>();
			universal2DResourceData.EndFrame();
			universalResourceDataBase.EndFrame();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000126F7 File Offset: 0x000108F7
		internal override void OnFinishRenderGraphRendering(CommandBuffer cmd)
		{
			CopyDepthPass copyDepthPass = this.m_CopyDepthPass;
			if (copyDepthPass == null)
			{
				return;
			}
			copyDepthPass.OnCameraCleanup(cmd);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0001270C File Offset: 0x0001090C
		private void OnBeforeRendering(RenderGraph renderGraph)
		{
			UniversalCameraData cameraData = base.frameData.Get<UniversalCameraData>();
			this.m_LightPass.Setup(renderGraph, ref this.m_Renderer2DData);
			List<Light2D> culledLights = this.m_Renderer2DData.lightCullResult.visibleLights;
			for (int i = 0; i < culledLights.Count; i++)
			{
				culledLights[i].CacheValues();
			}
			ShadowCasterGroup2DManager.CacheValues();
			ShadowRendering.CallOnBeforeRender(cameraData.camera, this.m_Renderer2DData.lightCullResult);
			RendererLighting.lightBatch.Reset();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0001278C File Offset: 0x0001098C
		private void OnMainRendering(RenderGraph renderGraph)
		{
			Universal2DResourceData universal2DResourceData = base.frameData.Get<Universal2DResourceData>();
			UniversalResourceData commonResourceData = base.frameData.Get<UniversalResourceData>();
			UniversalCameraData cameraData = base.frameData.Get<UniversalCameraData>();
			if (cameraData.postProcessEnabled && this.m_PostProcessPasses.isCreated)
			{
				TextureHandle internalColorLut;
				this.m_PostProcessPasses.colorGradingLutPass.Render(renderGraph, base.frameData, out internalColorLut);
				commonResourceData.internalColorLut = internalColorLut;
			}
			short cameraSortingLayerBoundsIndex = Render2DLightingPass.GetCameraSortingLayerBoundsIndex(this.m_Renderer2DData);
			GlobalPropertiesPass.Setup(renderGraph, cameraData);
			for (int i = 0; i < this.m_BatchCount; i++)
			{
				this.m_NormalPass.Render(renderGraph, base.frameData, this.m_Renderer2DData, ref this.m_LayerBatches[i], i);
			}
			for (int j = 0; j < this.m_BatchCount; j++)
			{
				this.m_ShadowPass.Render(renderGraph, base.frameData, this.m_Renderer2DData, ref this.m_LayerBatches[j], j, false);
			}
			for (int k = 0; k < this.m_BatchCount; k++)
			{
				this.m_LightPass.Render(renderGraph, base.frameData, this.m_Renderer2DData, ref this.m_LayerBatches[k], k, false);
			}
			for (int l = 0; l < this.m_BatchCount; l++)
			{
				if (!renderGraph.nativeRenderPassesEnabled && l == 0)
				{
					RTClearFlags clearFlags = (RTClearFlags)ScriptableRenderer.GetCameraClearFlag(cameraData);
					if (clearFlags != RTClearFlags.None)
					{
						ClearTargetsPass.Render(renderGraph, commonResourceData.activeColorTexture, commonResourceData.activeDepthTexture, clearFlags, cameraData.backgroundColor);
					}
				}
				ref LayerBatch layerBatch = ref this.m_LayerBatches[l];
				FilteringSettings filterSettings;
				LayerUtility.GetFilterSettings(this.m_Renderer2DData, ref this.m_LayerBatches[l], cameraSortingLayerBoundsIndex, out filterSettings);
				this.m_RendererPass.Render(renderGraph, base.frameData, this.m_Renderer2DData, ref this.m_LayerBatches, l, ref filterSettings);
				this.m_ShadowPass.Render(renderGraph, base.frameData, this.m_Renderer2DData, ref this.m_LayerBatches[l], l, true);
				this.m_LightPass.Render(renderGraph, base.frameData, this.m_Renderer2DData, ref this.m_LayerBatches[l], l, true);
				if (this.m_Renderer2DData.useCameraSortingLayerTexture)
				{
					if (cameraSortingLayerBoundsIndex >= layerBatch.layerRange.lowerBound && cameraSortingLayerBoundsIndex < layerBatch.layerRange.upperBound)
					{
						CopyCameraSortingLayerPass copyCameraSortingLayerPass = this.m_CopyCameraSortingLayerPass;
						TextureHandle textureHandle = commonResourceData.activeColorTexture;
						TextureHandle textureHandle2 = universal2DResourceData.cameraSortingLayerTexture;
						copyCameraSortingLayerPass.Render(renderGraph, in textureHandle, in textureHandle2);
						filterSettings.sortingLayerRange = new SortingLayerRange(cameraSortingLayerBoundsIndex + 1, layerBatch.layerRange.upperBound);
						this.m_RendererPass.Render(renderGraph, base.frameData, this.m_Renderer2DData, ref this.m_LayerBatches, l, ref filterSettings);
					}
					else if (cameraSortingLayerBoundsIndex == layerBatch.layerRange.upperBound)
					{
						CopyCameraSortingLayerPass copyCameraSortingLayerPass2 = this.m_CopyCameraSortingLayerPass;
						TextureHandle textureHandle = commonResourceData.activeColorTexture;
						TextureHandle textureHandle2 = universal2DResourceData.cameraSortingLayerTexture;
						copyCameraSortingLayerPass2.Render(renderGraph, in textureHandle, in textureHandle2);
					}
				}
			}
			if (this.RequiresDepthCopyPass(cameraData))
			{
				CopyDepthPass copyDepthPass = this.m_CopyDepthPass;
				if (copyDepthPass != null)
				{
					copyDepthPass.Render(renderGraph, base.frameData, commonResourceData.cameraDepthTexture, commonResourceData.activeDepthTexture, true, "Copy Depth");
				}
			}
			bool rendersOverlayUI = cameraData.rendersOverlayUI;
			bool outputToHDR = cameraData.isHDROutputActive;
			if (rendersOverlayUI && outputToHDR)
			{
				TextureHandle overlayUI;
				this.m_DrawOffscreenUIPass.RenderOffscreen(renderGraph, base.frameData, GraphicsFormat.D32_SFloat_S8_UInt, out overlayUI);
				commonResourceData.overlayUITexture = overlayUI;
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00012ACC File Offset: 0x00010CCC
		private void OnAfterRendering(RenderGraph renderGraph)
		{
			Universal2DResourceData universal2DResourceData = base.frameData.Get<Universal2DResourceData>();
			UniversalResourceData commonResourceData = base.frameData.Get<UniversalResourceData>();
			base.frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = base.frameData.Get<UniversalCameraData>();
			UniversalPostProcessingData postProcessingData = base.frameData.Get<UniversalPostProcessingData>();
			bool flag = DebugDisplaySettings<UniversalRenderPipelineDebugDisplaySettings>.Instance.renderingSettings.sceneOverrideMode == DebugSceneOverrideMode.None;
			if (flag)
			{
				base.DrawRenderGraphGizmos(renderGraph, base.frameData, commonResourceData.activeColorTexture, commonResourceData.activeDepthTexture, GizmoSubset.PreImageEffects);
			}
			DebugHandler debugHandler = ScriptableRenderPass.GetActiveDebugHandler(cameraData);
			bool resolveToDebugScreen = debugHandler != null && debugHandler.WriteToDebugScreenTexture(cameraData.resolveFinalTarget);
			if (resolveToDebugScreen)
			{
				RenderTextureDescriptor colorDesc = cameraData.cameraTargetDescriptor;
				DebugHandler.ConfigureColorDescriptorForDebugScreen(ref colorDesc, cameraData.pixelWidth, cameraData.pixelHeight);
				commonResourceData.debugScreenColor = UniversalRenderer.CreateRenderGraphTexture(renderGraph, colorDesc, "_DebugScreenColor", false, FilterMode.Point, TextureWrapMode.Clamp);
				RenderTextureDescriptor depthDesc = cameraData.cameraTargetDescriptor;
				DebugHandler.ConfigureDepthDescriptorForDebugScreen(ref depthDesc, GraphicsFormat.D32_SFloat_S8_UInt, cameraData.pixelWidth, cameraData.pixelHeight);
				commonResourceData.debugScreenDepth = UniversalRenderer.CreateRenderGraphTexture(renderGraph, depthDesc, "_DebugScreenDepth", false, FilterMode.Point, TextureWrapMode.Clamp);
			}
			bool flag2 = cameraData.postProcessEnabled && this.m_PostProcessPasses.isCreated;
			bool anyPostProcessing = postProcessingData.isEnabled && this.m_PostProcessPasses.isCreated;
			PixelPerfectCamera ppc;
			cameraData.camera.TryGetComponent<PixelPerfectCamera>(out ppc);
			bool requirePixelPerfectUpscale = ppc != null && ppc.enabled && ppc.cropFrame > PixelPerfectCamera.CropFrame.None && ppc.requiresUpscalePass;
			bool applyFinalPostProcessing = cameraData.resolveFinalTarget && !this.ppcUpscaleRT && anyPostProcessing && cameraData.antialiasing == AntialiasingMode.FastApproximateAntialiasing;
			base.activeRenderPassQueue.Find((ScriptableRenderPass x) => x.renderPassEvent == RenderPassEvent.AfterRenderingPostProcessing);
			bool needsColorEncoding = base.DebugHandler == null || !base.DebugHandler.HDRDebugViewIsActive(cameraData.resolveFinalTarget);
			if (flag2)
			{
				TextureHandle activeColor = commonResourceData.activeColorTexture;
				ImportResourceParams importColorParams = default(ImportResourceParams);
				importColorParams.clearOnFirstUse = true;
				importColorParams.clearColor = Color.black;
				importColorParams.discardOnLastUse = cameraData.resolveFinalTarget;
				commonResourceData.cameraColor = renderGraph.ImportTexture(this.nextRenderGraphCameraColorHandle, importColorParams);
				PostProcessPass postProcessPass = this.postProcessPass;
				ContextContainer frameData = base.frameData;
				TextureHandle textureHandle = commonResourceData.internalColorLut;
				TextureHandle overlayUITexture = commonResourceData.overlayUITexture;
				TextureHandle activeColorTexture = commonResourceData.activeColorTexture;
				postProcessPass.RenderPostProcessingRenderGraph(renderGraph, frameData, in activeColor, in textureHandle, in overlayUITexture, in activeColorTexture, applyFinalPostProcessing, resolveToDebugScreen, needsColorEncoding);
			}
			TextureHandle finalColorHandle = commonResourceData.activeColorTexture;
			this.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent2D.AfterRenderingPostProcessing);
			if (requirePixelPerfectUpscale)
			{
				UpscalePass upscalePass = this.m_UpscalePass;
				Camera camera = cameraData.camera;
				TextureHandle textureHandle = universal2DResourceData.upscaleTexture;
				upscalePass.Render(renderGraph, camera, in finalColorHandle, in textureHandle);
				finalColorHandle = universal2DResourceData.upscaleTexture;
			}
			TextureHandle finalBlitTarget = (resolveToDebugScreen ? commonResourceData.debugScreenColor : commonResourceData.backBufferColor);
			TextureHandle finalDepthHandle = (resolveToDebugScreen ? commonResourceData.debugScreenDepth : commonResourceData.backBufferDepth);
			if (this.createColorTexture)
			{
				if (applyFinalPostProcessing)
				{
					PostProcessPass postProcessPass2 = this.postProcessPass;
					ContextContainer frameData2 = base.frameData;
					TextureHandle textureHandle = commonResourceData.overlayUITexture;
					postProcessPass2.RenderFinalPassRenderGraph(renderGraph, frameData2, in finalColorHandle, in textureHandle, in finalBlitTarget, needsColorEncoding);
				}
				else if (cameraData.resolveFinalTarget)
				{
					this.m_FinalBlitPass.Render(renderGraph, base.frameData, cameraData, in finalColorHandle, in finalBlitTarget, commonResourceData.overlayUITexture);
				}
				finalColorHandle = finalBlitTarget;
				if (cameraData.resolveFinalTarget)
				{
					commonResourceData.activeColorID = UniversalResourceDataBase.ActiveID.BackBuffer;
					commonResourceData.activeDepthID = UniversalResourceDataBase.ActiveID.BackBuffer;
				}
			}
			bool rendersOverlayUI = cameraData.rendersOverlayUI;
			bool outputToHDR = cameraData.isHDROutputActive;
			if (rendersOverlayUI && !outputToHDR)
			{
				this.m_DrawOverlayUIPass.RenderOverlay(renderGraph, base.frameData, in finalColorHandle, in finalDepthHandle);
			}
			if (cameraData.isSceneViewCamera)
			{
				base.DrawRenderGraphWireOverlay(renderGraph, base.frameData, commonResourceData.backBufferColor);
			}
			if (flag)
			{
				base.DrawRenderGraphGizmos(renderGraph, base.frameData, commonResourceData.activeColorTexture, commonResourceData.activeDepthTexture, GizmoSubset.PostImageEffects);
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00012E5C File Offset: 0x0001105C
		private void CleanupRenderGraphResources()
		{
			RTHandle rthandle = this.m_RenderGraphCameraColorHandles[0];
			if (rthandle != null)
			{
				rthandle.Release();
			}
			RTHandle rthandle2 = this.m_RenderGraphCameraColorHandles[1];
			if (rthandle2 != null)
			{
				rthandle2.Release();
			}
			RTHandle renderGraphCameraDepthHandle = this.m_RenderGraphCameraDepthHandle;
			if (renderGraphCameraDepthHandle != null)
			{
				renderGraphCameraDepthHandle.Release();
			}
			RTHandle renderGraphBackbufferColorHandle = this.m_RenderGraphBackbufferColorHandle;
			if (renderGraphBackbufferColorHandle != null)
			{
				renderGraphBackbufferColorHandle.Release();
			}
			RTHandle renderGraphBackbufferDepthHandle = this.m_RenderGraphBackbufferDepthHandle;
			if (renderGraphBackbufferDepthHandle != null)
			{
				renderGraphBackbufferDepthHandle.Release();
			}
			RTHandle cameraSortingLayerHandle = this.m_CameraSortingLayerHandle;
			if (cameraSortingLayerHandle != null)
			{
				cameraSortingLayerHandle.Release();
			}
			Light2DLookupTexture.Release();
		}

		// Token: 0x040001B1 RID: 433
		private const GraphicsFormat k_DepthStencilFormat = GraphicsFormat.D32_SFloat_S8_UInt;

		// Token: 0x040001B2 RID: 434
		private const int k_FinalBlitPassQueueOffset = 1;

		// Token: 0x040001B3 RID: 435
		private const int k_AfterFinalBlitPassQueueOffset = 2;

		// Token: 0x040001B4 RID: 436
		private Render2DLightingPass m_Render2DLightingPass;

		// Token: 0x040001B5 RID: 437
		private PixelPerfectBackgroundPass m_PixelPerfectBackgroundPass;

		// Token: 0x040001B6 RID: 438
		private UpscalePass m_UpscalePass;

		// Token: 0x040001B7 RID: 439
		private CopyDepthPass m_CopyDepthPass;

		// Token: 0x040001B8 RID: 440
		private CopyCameraSortingLayerPass m_CopyCameraSortingLayerPass;

		// Token: 0x040001B9 RID: 441
		private FinalBlitPass m_FinalBlitPass;

		// Token: 0x040001BA RID: 442
		private DrawScreenSpaceUIPass m_DrawOffscreenUIPass;

		// Token: 0x040001BB RID: 443
		private DrawScreenSpaceUIPass m_DrawOverlayUIPass;

		// Token: 0x040001BC RID: 444
		internal RenderTargetBufferSystem m_ColorBufferSystem;

		// Token: 0x040001BD RID: 445
		private static readonly ProfilingSampler m_ProfilingSampler = new ProfilingSampler("Create Camera Textures");

		// Token: 0x040001BE RID: 446
		private bool m_UseDepthStencilBuffer = true;

		// Token: 0x040001BF RID: 447
		private bool m_CreateColorTexture;

		// Token: 0x040001C0 RID: 448
		private bool m_CreateDepthTexture;

		// Token: 0x040001C1 RID: 449
		internal RTHandle m_ColorTextureHandle;

		// Token: 0x040001C2 RID: 450
		internal RTHandle m_DepthTextureHandle;

		// Token: 0x040001C3 RID: 451
		private Material m_BlitMaterial;

		// Token: 0x040001C4 RID: 452
		private Material m_BlitHDRMaterial;

		// Token: 0x040001C5 RID: 453
		private Material m_SamplingMaterial;

		// Token: 0x040001C6 RID: 454
		private Renderer2DData m_Renderer2DData;

		// Token: 0x040001C7 RID: 455
		private PostProcessPasses m_PostProcessPasses;

		// Token: 0x040001C8 RID: 456
		private static int m_CurrentColorHandle = 0;

		// Token: 0x040001C9 RID: 457
		private RTHandle[] m_RenderGraphCameraColorHandles = new RTHandle[2];

		// Token: 0x040001CA RID: 458
		private RTHandle m_RenderGraphCameraDepthHandle;

		// Token: 0x040001CB RID: 459
		private RTHandle m_RenderGraphBackbufferColorHandle;

		// Token: 0x040001CC RID: 460
		private RTHandle m_RenderGraphBackbufferDepthHandle;

		// Token: 0x040001CD RID: 461
		private RTHandle m_CameraSortingLayerHandle;

		// Token: 0x040001CE RID: 462
		private DrawNormal2DPass m_NormalPass = new DrawNormal2DPass();

		// Token: 0x040001CF RID: 463
		private DrawLight2DPass m_LightPass = new DrawLight2DPass();

		// Token: 0x040001D0 RID: 464
		private DrawShadow2DPass m_ShadowPass = new DrawShadow2DPass();

		// Token: 0x040001D1 RID: 465
		private DrawRenderer2DPass m_RendererPass = new DrawRenderer2DPass();

		// Token: 0x040001D2 RID: 466
		private LayerBatch[] m_LayerBatches;

		// Token: 0x040001D3 RID: 467
		private int m_BatchCount;

		// Token: 0x040001D4 RID: 468
		private bool ppcUpscaleRT;

		// Token: 0x0200004E RID: 78
		private struct RenderPassInputSummary
		{
			// Token: 0x040001D5 RID: 469
			internal bool requiresDepthTexture;

			// Token: 0x040001D6 RID: 470
			internal bool requiresColorTexture;
		}

		// Token: 0x0200004F RID: 79
		private struct ImportResourceSummary
		{
			// Token: 0x040001D7 RID: 471
			internal RenderTargetInfo importInfo;

			// Token: 0x040001D8 RID: 472
			internal RenderTargetInfo importInfoDepth;

			// Token: 0x040001D9 RID: 473
			internal ImportResourceParams cameraColorParams;

			// Token: 0x040001DA RID: 474
			internal ImportResourceParams cameraDepthParams;

			// Token: 0x040001DB RID: 475
			internal ImportResourceParams backBufferColorParams;

			// Token: 0x040001DC RID: 476
			internal ImportResourceParams backBufferDepthParams;
		}
	}
}
