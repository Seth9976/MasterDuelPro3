using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.VFX;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001D7 RID: 471
	public sealed class UniversalRenderer : ScriptableRenderer
	{
		// Token: 0x06000A5E RID: 2654 RVA: 0x00033638 File Offset: 0x00031838
		public override int SupportedCameraStackingTypes()
		{
			switch (this.m_RenderingMode)
			{
			case RenderingMode.Forward:
			case RenderingMode.ForwardPlus:
				return 3;
			case RenderingMode.Deferred:
				return 1;
			default:
				return 0;
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000039B4 File Offset: 0x00001BB4
		protected internal override bool SupportsMotionVectors()
		{
			return true;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00033665 File Offset: 0x00031865
		internal RenderingMode renderingModeRequested
		{
			get
			{
				return this.m_RenderingMode;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00033670 File Offset: 0x00031870
		internal RenderingMode renderingModeActual
		{
			get
			{
				if (this.renderingModeRequested != RenderingMode.Deferred || (!GL.wireframe && (base.DebugHandler == null || !base.DebugHandler.IsActiveModeUnsupportedForDeferred) && this.m_DeferredLights != null && this.m_DeferredLights.IsRuntimeSupportedThisFrame() && !this.m_DeferredLights.IsOverlay))
				{
					return this.renderingModeRequested;
				}
				return RenderingMode.Forward;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x000336CC File Offset: 0x000318CC
		internal bool accurateGbufferNormals
		{
			get
			{
				return this.m_DeferredLights != null && this.m_DeferredLights.AccurateGbufferNormals;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x000336E3 File Offset: 0x000318E3
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x000336EB File Offset: 0x000318EB
		public DepthPrimingMode depthPrimingMode
		{
			get
			{
				return this.m_DepthPrimingMode;
			}
			set
			{
				this.m_DepthPrimingMode = value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x000336F4 File Offset: 0x000318F4
		internal ColorGradingLutPass colorGradingLutPass
		{
			get
			{
				return this.m_PostProcessPasses.colorGradingLutPass;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00033701 File Offset: 0x00031901
		internal PostProcessPass postProcessPass
		{
			get
			{
				return this.m_PostProcessPasses.postProcessPass;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0003370E File Offset: 0x0003190E
		internal PostProcessPass finalPostProcessPass
		{
			get
			{
				return this.m_PostProcessPasses.finalPostProcessPass;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x0003371B File Offset: 0x0003191B
		internal RTHandle colorGradingLut
		{
			get
			{
				return this.m_PostProcessPasses.colorGradingLut;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00033728 File Offset: 0x00031928
		internal DeferredLights deferredLights
		{
			get
			{
				return this.m_DeferredLights;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00033730 File Offset: 0x00031930
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x00033738 File Offset: 0x00031938
		internal LayerMask opaqueLayerMask { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00033741 File Offset: 0x00031941
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x00033749 File Offset: 0x00031949
		internal LayerMask transparentLayerMask { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00033752 File Offset: 0x00031952
		internal GraphicsFormat cameraDepthTextureFormat
		{
			get
			{
				if (this.m_CameraDepthTextureFormat == DepthFormat.Default)
				{
					return GraphicsFormat.D32_SFloat_S8_UInt;
				}
				return (GraphicsFormat)this.m_CameraDepthTextureFormat;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00033765 File Offset: 0x00031965
		internal GraphicsFormat cameraDepthAttachmentFormat
		{
			get
			{
				if (this.m_CameraDepthAttachmentFormat == DepthFormat.Default)
				{
					return GraphicsFormat.D32_SFloat_S8_UInt;
				}
				return (GraphicsFormat)this.m_CameraDepthAttachmentFormat;
			}
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00033778 File Offset: 0x00031978
		public UniversalRenderer(UniversalRendererData data)
			: base(data)
		{
			PlatformAutoDetect.Initialize();
			UniversalRenderPipelineRuntimeXRResources xrResources;
			if (GraphicsSettings.TryGetRenderPipelineSettings<UniversalRenderPipelineRuntimeXRResources>(out xrResources))
			{
				XRSystem.Initialize(new Func<XRPassCreateInfo, XRPass>(XRPassUniversal.Create), xrResources.xrOcclusionMeshPS, xrResources.xrMirrorViewPS);
				this.m_XRDepthMotionPass = new XRDepthMotionPass(RenderPassEvent.BeforeRenderingPrePasses, xrResources.xrMotionVector);
			}
			UniversalRenderPipelineRuntimeShaders shadersResources;
			if (GraphicsSettings.TryGetRenderPipelineSettings<UniversalRenderPipelineRuntimeShaders>(out shadersResources))
			{
				this.m_BlitMaterial = CoreUtils.CreateEngineMaterial(shadersResources.coreBlitPS);
				this.m_BlitHDRMaterial = CoreUtils.CreateEngineMaterial(shadersResources.blitHDROverlay);
				this.m_SamplingMaterial = CoreUtils.CreateEngineMaterial(shadersResources.samplingPS);
			}
			Shader copyDephPS = null;
			UniversalRendererResources universalRendererShaders;
			if (GraphicsSettings.TryGetRenderPipelineSettings<UniversalRendererResources>(out universalRendererShaders))
			{
				copyDephPS = universalRendererShaders.copyDepthPS;
				this.m_StencilDeferredMaterial = CoreUtils.CreateEngineMaterial(universalRendererShaders.stencilDeferredPS);
				this.m_CameraMotionVecMaterial = CoreUtils.CreateEngineMaterial(universalRendererShaders.cameraMotionVector);
			}
			StencilStateData stencilData = data.defaultStencilState;
			this.m_DefaultStencilState = StencilState.defaultValue;
			this.m_DefaultStencilState.enabled = stencilData.overrideStencilState;
			this.m_DefaultStencilState.SetCompareFunction(stencilData.stencilCompareFunction);
			this.m_DefaultStencilState.SetPassOperation(stencilData.passOperation);
			this.m_DefaultStencilState.SetFailOperation(stencilData.failOperation);
			this.m_DefaultStencilState.SetZFailOperation(stencilData.zFailOperation);
			this.m_IntermediateTextureMode = data.intermediateTextureMode;
			this.opaqueLayerMask = data.opaqueLayerMask;
			this.transparentLayerMask = data.transparentLayerMask;
			UniversalRenderPipelineAsset asset3 = UniversalRenderPipeline.asset;
			if (asset3 != null && asset3.supportsLightCookies)
			{
				LightCookieManager.Settings settings = LightCookieManager.Settings.Create();
				UniversalRenderPipelineAsset asset = UniversalRenderPipeline.asset;
				if (asset)
				{
					settings.atlas.format = asset.additionalLightsCookieFormat;
					settings.atlas.resolution = asset.additionalLightsCookieResolution;
				}
				this.m_LightCookieManager = new LightCookieManager(ref settings);
			}
			base.stripShadowsOffVariants = true;
			base.stripAdditionalLightOffVariants = true;
			ForwardLights.InitParams forwardInitParams;
			forwardInitParams.lightCookieManager = this.m_LightCookieManager;
			forwardInitParams.forwardPlus = data.renderingMode == RenderingMode.ForwardPlus;
			this.m_Clustering = data.renderingMode == RenderingMode.ForwardPlus;
			this.m_ForwardLights = new ForwardLights(forwardInitParams);
			this.m_RenderingMode = data.renderingMode;
			this.m_DepthPrimingMode = data.depthPrimingMode;
			this.m_CopyDepthMode = data.copyDepthMode;
			this.m_CameraDepthAttachmentFormat = data.depthAttachmentFormat;
			this.m_CameraDepthTextureFormat = data.depthTextureFormat;
			this.useRenderPassEnabled = data.useNativeRenderPass;
			this.m_DepthPrimingRecommended = true;
			this.m_MainLightShadowCasterPass = new MainLightShadowCasterPass(RenderPassEvent.BeforeRenderingShadows);
			this.m_AdditionalLightsShadowCasterPass = new AdditionalLightsShadowCasterPass(RenderPassEvent.BeforeRenderingShadows);
			this.m_XROcclusionMeshPass = new XROcclusionMeshPass(RenderPassEvent.BeforeRenderingOpaques);
			this.m_XRCopyDepthPass = new CopyDepthPass((RenderPassEvent)1002, copyDephPS, false, false, false, null);
			this.m_DepthPrepass = new DepthOnlyPass(RenderPassEvent.BeforeRenderingPrePasses, RenderQueueRange.opaque, data.opaqueLayerMask);
			this.m_DepthNormalPrepass = new DepthNormalOnlyPass(RenderPassEvent.BeforeRenderingPrePasses, RenderQueueRange.opaque, data.opaqueLayerMask);
			if (this.renderingModeRequested == RenderingMode.Forward || this.renderingModeRequested == RenderingMode.ForwardPlus)
			{
				this.m_PrimedDepthCopyPass = new CopyDepthPass(RenderPassEvent.AfterRenderingPrePasses, copyDephPS, true, true, false, null);
			}
			if (this.renderingModeRequested == RenderingMode.Deferred)
			{
				this.m_DeferredLights = new DeferredLights(new DeferredLights.InitParams
				{
					stencilDeferredMaterial = this.m_StencilDeferredMaterial,
					lightCookieManager = this.m_LightCookieManager
				}, this.useRenderPassEnabled);
				this.m_DeferredLights.AccurateGbufferNormals = data.accurateGbufferNormals;
				this.m_GBufferPass = new GBufferPass(RenderPassEvent.BeforeRenderingGbuffer, RenderQueueRange.opaque, data.opaqueLayerMask, this.m_DefaultStencilState, stencilData.stencilReference, this.m_DeferredLights);
				StencilState forwardOnlyStencilState = DeferredLights.OverwriteStencil(this.m_DefaultStencilState, 96);
				ShaderTagId[] forwardOnlyShaderTagIds = new ShaderTagId[]
				{
					new ShaderTagId("UniversalForwardOnly"),
					new ShaderTagId("SRPDefaultUnlit"),
					new ShaderTagId("LightweightForward")
				};
				int forwardOnlyStencilRef = stencilData.stencilReference | 0;
				this.m_GBufferCopyDepthPass = new CopyDepthPass((RenderPassEvent)211, copyDephPS, true, false, false, "Copy GBuffer Depth");
				this.m_DeferredPass = new DeferredPass(RenderPassEvent.BeforeRenderingDeferredLights, this.m_DeferredLights);
				this.m_RenderOpaqueForwardOnlyPass = new DrawObjectsPass("Draw Opaques Forward Only", forwardOnlyShaderTagIds, true, RenderPassEvent.BeforeRenderingOpaques, RenderQueueRange.opaque, data.opaqueLayerMask, forwardOnlyStencilState, forwardOnlyStencilRef);
			}
			this.m_RenderOpaqueForwardPass = new DrawObjectsPass(URPProfileId.DrawOpaqueObjects, true, RenderPassEvent.BeforeRenderingOpaques, RenderQueueRange.opaque, data.opaqueLayerMask, this.m_DefaultStencilState, stencilData.stencilReference);
			this.m_RenderOpaqueForwardWithRenderingLayersPass = new DrawObjectsWithRenderingLayersPass(URPProfileId.DrawOpaqueObjects, true, RenderPassEvent.BeforeRenderingOpaques, RenderQueueRange.opaque, data.opaqueLayerMask, this.m_DefaultStencilState, stencilData.stencilReference);
			bool copyDepthAfterTransparents = this.m_CopyDepthMode == CopyDepthMode.AfterTransparents;
			RenderPassEvent copyDepthEvent = (copyDepthAfterTransparents ? RenderPassEvent.AfterRenderingTransparents : RenderPassEvent.AfterRenderingSkybox);
			this.m_CopyDepthPass = new CopyDepthPass(copyDepthEvent, copyDephPS, true, false, RenderingUtils.MultisampleDepthResolveSupported() && copyDepthAfterTransparents, null);
			this.m_MotionVectorPass = new MotionVectorRenderPass(copyDepthEvent + 1, this.m_CameraMotionVecMaterial, data.opaqueLayerMask);
			this.m_DrawSkyboxPass = new DrawSkyboxPass(RenderPassEvent.BeforeRenderingSkybox);
			this.m_CopyColorPass = new CopyColorPass(RenderPassEvent.AfterRenderingSkybox, this.m_SamplingMaterial, this.m_BlitMaterial, null);
			this.m_TransparentSettingsPass = new TransparentSettingsPass(RenderPassEvent.BeforeRenderingTransparents, data.shadowTransparentReceive);
			this.m_RenderTransparentForwardPass = new DrawObjectsPass(URPProfileId.DrawTransparentObjects, false, RenderPassEvent.BeforeRenderingTransparents, RenderQueueRange.transparent, data.transparentLayerMask, this.m_DefaultStencilState, stencilData.stencilReference);
			this.m_OnRenderObjectCallbackPass = new InvokeOnRenderObjectCallbackPass(RenderPassEvent.BeforeRenderingPostProcessing);
			this.m_HistoryRawColorCopyPass = new CopyColorPass(RenderPassEvent.BeforeRenderingPostProcessing, this.m_SamplingMaterial, this.m_BlitMaterial, "Copy Color Raw History");
			this.m_HistoryRawDepthCopyPass = new CopyDepthPass(RenderPassEvent.BeforeRenderingPostProcessing, copyDephPS, false, RenderingUtils.MultisampleDepthResolveSupported(), false, "Copy Depth Raw History");
			this.m_DrawOffscreenUIPass = new DrawScreenSpaceUIPass(RenderPassEvent.BeforeRenderingPostProcessing, true);
			this.m_DrawOverlayUIPass = new DrawScreenSpaceUIPass((RenderPassEvent)1002, false);
			PostProcessParams postProcessParams = PostProcessParams.Create();
			postProcessParams.blitMaterial = this.m_BlitMaterial;
			postProcessParams.requestColorFormat = GraphicsFormat.B10G11R11_UFloatPack32;
			UniversalRenderPipelineAsset asset2 = UniversalRenderPipeline.asset;
			if (asset2)
			{
				postProcessParams.requestColorFormat = UniversalRenderPipeline.MakeRenderTextureGraphicsFormat(asset2.supportsHDR, asset2.hdrColorBufferPrecision, false);
			}
			this.m_PostProcessPasses = new PostProcessPasses(data.postProcessData, ref postProcessParams);
			this.m_CapturePass = new CapturePass(RenderPassEvent.AfterRendering);
			this.m_FinalBlitPass = new FinalBlitPass((RenderPassEvent)1001, this.m_BlitMaterial, this.m_BlitHDRMaterial);
			this.m_ColorBufferSystem = new RenderTargetBufferSystem("_CameraColorAttachment");
			base.supportedRenderingFeatures = new ScriptableRenderer.RenderingFeatures();
			if (this.renderingModeRequested == RenderingMode.Deferred)
			{
				base.supportedRenderingFeatures.msaa = false;
			}
			LensFlareCommonSRP.mergeNeeded = 0;
			LensFlareCommonSRP.maxLensFlareWithOcclusionTemporalSample = 1;
			LensFlareCommonSRP.Initialize();
			this.m_VulkanEnablePreTransform = GraphicsSettings.HasShaderDefine(BuiltinShaderDefine.UNITY_PRETRANSFORM_TO_DISPLAY_ORIENTATION);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00033DF4 File Offset: 0x00031FF4
		protected override void Dispose(bool disposing)
		{
			this.m_ForwardLights.Cleanup();
			GBufferPass gbufferPass = this.m_GBufferPass;
			if (gbufferPass != null)
			{
				gbufferPass.Dispose();
			}
			this.m_PostProcessPasses.Dispose();
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
			CopyDepthPass copyDepthPass = this.m_CopyDepthPass;
			if (copyDepthPass != null)
			{
				copyDepthPass.Dispose();
			}
			CopyDepthPass primedDepthCopyPass = this.m_PrimedDepthCopyPass;
			if (primedDepthCopyPass != null)
			{
				primedDepthCopyPass.Dispose();
			}
			CopyDepthPass gbufferCopyDepthPass = this.m_GBufferCopyDepthPass;
			if (gbufferCopyDepthPass != null)
			{
				gbufferCopyDepthPass.Dispose();
			}
			CopyDepthPass historyRawDepthCopyPass = this.m_HistoryRawDepthCopyPass;
			if (historyRawDepthCopyPass != null)
			{
				historyRawDepthCopyPass.Dispose();
			}
			CopyDepthPass xrcopyDepthPass = this.m_XRCopyDepthPass;
			if (xrcopyDepthPass != null)
			{
				xrcopyDepthPass.Dispose();
			}
			XRDepthMotionPass xrdepthMotionPass = this.m_XRDepthMotionPass;
			if (xrdepthMotionPass != null)
			{
				xrdepthMotionPass.Dispose();
			}
			RTHandle targetColorHandle = this.m_TargetColorHandle;
			if (targetColorHandle != null)
			{
				targetColorHandle.Release();
			}
			RTHandle targetDepthHandle = this.m_TargetDepthHandle;
			if (targetDepthHandle != null)
			{
				targetDepthHandle.Release();
			}
			this.ReleaseRenderTargets();
			base.Dispose(disposing);
			CoreUtils.Destroy(this.m_BlitMaterial);
			CoreUtils.Destroy(this.m_BlitHDRMaterial);
			CoreUtils.Destroy(this.m_SamplingMaterial);
			CoreUtils.Destroy(this.m_StencilDeferredMaterial);
			CoreUtils.Destroy(this.m_CameraMotionVecMaterial);
			this.CleanupRenderGraphResources();
			LensFlareCommonSRP.Dispose();
			XRSystem.Dispose();
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00033F38 File Offset: 0x00032138
		internal override void ReleaseRenderTargets()
		{
			this.m_ColorBufferSystem.Dispose();
			if (this.m_DeferredLights != null && !this.m_DeferredLights.UseFramebufferFetch)
			{
				GBufferPass gbufferPass = this.m_GBufferPass;
				if (gbufferPass != null)
				{
					gbufferPass.Dispose();
				}
			}
			this.m_PostProcessPasses.ReleaseRenderTargets();
			MainLightShadowCasterPass mainLightShadowCasterPass = this.m_MainLightShadowCasterPass;
			if (mainLightShadowCasterPass != null)
			{
				mainLightShadowCasterPass.Dispose();
			}
			AdditionalLightsShadowCasterPass additionalLightsShadowCasterPass = this.m_AdditionalLightsShadowCasterPass;
			if (additionalLightsShadowCasterPass != null)
			{
				additionalLightsShadowCasterPass.Dispose();
			}
			RTHandle cameraDepthAttachment = this.m_CameraDepthAttachment;
			if (cameraDepthAttachment != null)
			{
				cameraDepthAttachment.Release();
			}
			RTHandle depthTexture = this.m_DepthTexture;
			if (depthTexture != null)
			{
				depthTexture.Release();
			}
			RTHandle normalsTexture = this.m_NormalsTexture;
			if (normalsTexture != null)
			{
				normalsTexture.Release();
			}
			RTHandle decalLayersTexture = this.m_DecalLayersTexture;
			if (decalLayersTexture != null)
			{
				decalLayersTexture.Release();
			}
			RTHandle opaqueColor = this.m_OpaqueColor;
			if (opaqueColor != null)
			{
				opaqueColor.Release();
			}
			RTHandle motionVectorColor = this.m_MotionVectorColor;
			if (motionVectorColor != null)
			{
				motionVectorColor.Release();
			}
			RTHandle motionVectorDepth = this.m_MotionVectorDepth;
			if (motionVectorDepth != null)
			{
				motionVectorDepth.Release();
			}
			this.hasReleasedRTs = true;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00034024 File Offset: 0x00032224
		private void SetupFinalPassDebug(UniversalCameraData cameraData)
		{
			if (base.DebugHandler != null && base.DebugHandler.IsActiveForCamera(cameraData.isPreviewCamera))
			{
				DebugFullScreenMode fullScreenDebugMode;
				int textureHeightPercent;
				if (base.DebugHandler.TryGetFullscreenDebugMode(out fullScreenDebugMode, out textureHeightPercent) && (fullScreenDebugMode != DebugFullScreenMode.ReflectionProbeAtlas || this.m_Clustering))
				{
					Camera camera = cameraData.camera;
					float screenWidth = (float)camera.pixelWidth;
					float screenHeight = (float)camera.pixelHeight;
					float num = Mathf.Clamp01((float)textureHeightPercent / 100f);
					float height = num * screenHeight;
					float width = num * screenWidth;
					RenderTexture tex = null;
					if (fullScreenDebugMode == DebugFullScreenMode.ReflectionProbeAtlas)
					{
						tex = this.m_ForwardLights.reflectionProbeManager.atlasRT;
					}
					else if (fullScreenDebugMode == DebugFullScreenMode.MainLightShadowMap)
					{
						tex = this.m_MainLightShadowCasterPass.m_MainLightShadowmapTexture.rt;
					}
					else if (fullScreenDebugMode == DebugFullScreenMode.AdditionalLightsShadowMap)
					{
						tex = this.m_AdditionalLightsShadowCasterPass.m_AdditionalLightsShadowmapHandle.rt;
					}
					else if (fullScreenDebugMode == DebugFullScreenMode.AdditionalLightsCookieAtlas && this.m_LightCookieManager != null)
					{
						LightCookieManager lightCookieManager = this.m_LightCookieManager;
						RenderTexture renderTexture;
						if (lightCookieManager == null)
						{
							renderTexture = null;
						}
						else
						{
							RTHandle additionalLightsCookieAtlasTexture = lightCookieManager.AdditionalLightsCookieAtlasTexture;
							renderTexture = ((additionalLightsCookieAtlasTexture != null) ? additionalLightsCookieAtlasTexture.rt : null);
						}
						tex = renderTexture;
					}
					if (tex != null)
					{
						this.CorrectForTextureAspectRatio(ref width, ref height, (float)tex.width, (float)tex.height);
					}
					float normalizedSizeX = width / screenWidth;
					float normalizedSizeY = height / screenHeight;
					Rect normalizedRect = new Rect(1f - normalizedSizeX, 1f - normalizedSizeY, normalizedSizeX, normalizedSizeY);
					Vector4 dataRangeRemap = Vector4.zero;
					switch (fullScreenDebugMode)
					{
					case DebugFullScreenMode.Depth:
						base.DebugHandler.SetDebugRenderTarget(this.m_DepthTexture, normalizedRect, true, dataRangeRemap);
						return;
					case DebugFullScreenMode.MotionVector:
						dataRangeRemap.x = -0.01f;
						dataRangeRemap.y = 0.01f;
						dataRangeRemap.z = 0f;
						dataRangeRemap.w = 1f;
						base.DebugHandler.SetDebugRenderTarget(this.m_MotionVectorColor, normalizedRect, true, dataRangeRemap);
						return;
					case DebugFullScreenMode.AdditionalLightsShadowMap:
						base.DebugHandler.SetDebugRenderTarget(this.m_AdditionalLightsShadowCasterPass.m_AdditionalLightsShadowmapHandle, normalizedRect, false, dataRangeRemap);
						return;
					case DebugFullScreenMode.MainLightShadowMap:
						base.DebugHandler.SetDebugRenderTarget(this.m_MainLightShadowCasterPass.m_MainLightShadowmapTexture, normalizedRect, false, dataRangeRemap);
						return;
					case DebugFullScreenMode.AdditionalLightsCookieAtlas:
					{
						DebugHandler debugHandler = base.DebugHandler;
						LightCookieManager lightCookieManager2 = this.m_LightCookieManager;
						debugHandler.SetDebugRenderTarget((lightCookieManager2 != null) ? lightCookieManager2.AdditionalLightsCookieAtlasTexture : null, normalizedRect, false, dataRangeRemap);
						return;
					}
					case DebugFullScreenMode.ReflectionProbeAtlas:
						base.DebugHandler.SetDebugRenderTarget(this.m_ForwardLights.reflectionProbeManager.atlasRTHandle, normalizedRect, false, dataRangeRemap);
						return;
					default:
						return;
					}
				}
				else
				{
					base.DebugHandler.ResetDebugRenderTarget();
				}
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00034277 File Offset: 0x00032477
		public static bool IsOffscreenDepthTexture(ref CameraData cameraData)
		{
			return UniversalRenderer.IsOffscreenDepthTexture(cameraData.universalCameraData);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00034284 File Offset: 0x00032484
		public static bool IsOffscreenDepthTexture(UniversalCameraData cameraData)
		{
			return cameraData.targetTexture != null && cameraData.targetTexture.format == RenderTextureFormat.Depth;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x000342A4 File Offset: 0x000324A4
		private bool IsDepthPrimingEnabled(UniversalCameraData cameraData)
		{
			if (!this.CanCopyDepth(cameraData))
			{
				return false;
			}
			bool isNotWebGL = !this.IsWebGL();
			bool flag = (this.m_DepthPrimingRecommended && this.m_DepthPrimingMode == DepthPrimingMode.Auto) || this.m_DepthPrimingMode == DepthPrimingMode.Forced;
			bool isForwardRenderingMode = this.m_RenderingMode == RenderingMode.Forward || this.m_RenderingMode == RenderingMode.ForwardPlus;
			bool isFirstCameraToWriteDepth = cameraData.renderType == CameraRenderType.Base || cameraData.clearDepth;
			bool isNotReflectionCamera = cameraData.cameraType != CameraType.Reflection;
			bool isNotOffscreenDepthTexture = !UniversalRenderer.IsOffscreenDepthTexture(cameraData);
			bool isNotMSAA = cameraData.cameraTargetDescriptor.msaaSamples == 1;
			return flag && isForwardRenderingMode && isFirstCameraToWriteDepth && isNotReflectionCamera && isNotOffscreenDepthTexture && isNotWebGL && isNotMSAA;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00002886 File Offset: 0x00000A86
		private bool IsWebGL()
		{
			return false;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00034341 File Offset: 0x00032541
		private bool IsGLESDevice()
		{
			return SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0003434C File Offset: 0x0003254C
		private bool IsGLDevice()
		{
			return this.IsGLESDevice() || SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLCore;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00034364 File Offset: 0x00032564
		internal bool HasActiveRenderFeatures()
		{
			if (base.rendererFeatures.Count == 0)
			{
				return false;
			}
			using (List<ScriptableRendererFeature>.Enumerator enumerator = base.rendererFeatures.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.isActive)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x000343CC File Offset: 0x000325CC
		internal bool HasPassesRequiringIntermediateTexture()
		{
			if (base.activeRenderPassQueue.Count == 0)
			{
				return false;
			}
			using (List<ScriptableRenderPass>.Enumerator enumerator = base.activeRenderPassQueue.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.requiresIntermediateTexture)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00034434 File Offset: 0x00032634
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void Setup(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			UniversalRenderingData universalRenderingData = base.frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = base.frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = base.frameData.Get<UniversalLightData>();
			UniversalShadowData shadowData = base.frameData.Get<UniversalShadowData>();
			UniversalPostProcessingData postProcessingData = base.frameData.Get<UniversalPostProcessingData>();
			this.m_ForwardLights.PreSetup(universalRenderingData, cameraData, lightData);
			Camera camera = cameraData.camera;
			RenderTextureDescriptor cameraTargetDescriptor = cameraData.cameraTargetDescriptor;
			CommandBuffer cmd = universalRenderingData.commandBuffer;
			if (base.DebugHandler != null && base.DebugHandler.IsActiveForCamera(cameraData.isPreviewCamera))
			{
				if (base.DebugHandler.WriteToDebugScreenTexture(cameraData.resolveFinalTarget))
				{
					RenderTextureDescriptor colorDesc = cameraData.cameraTargetDescriptor;
					DebugHandler.ConfigureColorDescriptorForDebugScreen(ref colorDesc, cameraData.pixelWidth, cameraData.pixelHeight);
					RenderingUtils.ReAllocateHandleIfNeeded(base.DebugHandler.DebugScreenColorHandle, in colorDesc, FilterMode.Point, TextureWrapMode.Repeat, 1, 0f, "_DebugScreenColor");
					RenderTextureDescriptor depthDesc = cameraData.cameraTargetDescriptor;
					DebugHandler.ConfigureDepthDescriptorForDebugScreen(ref depthDesc, this.cameraDepthTextureFormat, cameraData.pixelWidth, cameraData.pixelHeight);
					RenderingUtils.ReAllocateHandleIfNeeded(base.DebugHandler.DebugScreenDepthHandle, in depthDesc, FilterMode.Point, TextureWrapMode.Repeat, 1, 0f, "_DebugScreenDepth");
				}
				if (base.DebugHandler.HDRDebugViewIsActive(cameraData.resolveFinalTarget))
				{
					base.DebugHandler.hdrDebugViewPass.Setup(cameraData, base.DebugHandler.DebugDisplaySettings.lightingSettings.hdrDebugMode);
					base.EnqueuePass(base.DebugHandler.hdrDebugViewPass);
				}
			}
			if (cameraData.cameraType != CameraType.Game)
			{
				this.useRenderPassEnabled = false;
			}
			base.useDepthPriming = this.IsDepthPrimingEnabled(cameraData);
			if (UniversalRenderer.IsOffscreenDepthTexture(cameraData))
			{
				base.ConfigureCameraTarget(ScriptableRenderer.k_CameraTarget, ScriptableRenderer.k_CameraTarget);
				base.EnqueuePass(this.m_RenderOpaqueForwardPass);
				base.EnqueuePass(this.m_RenderTransparentForwardPass);
				return;
			}
			bool isPreviewCamera = cameraData.isPreviewCamera;
			bool createColorTexture = (this.HasActiveRenderFeatures() && this.m_IntermediateTextureMode == IntermediateTextureMode.Always && !isPreviewCamera) || (Application.isEditor && this.m_Clustering);
			createColorTexture |= this.HasPassesRequiringIntermediateTexture();
			this.UpdateCameraHistory(cameraData);
			UniversalRenderer.RenderPassInputSummary renderPassInputs = this.GetRenderPassInputs(cameraData.IsTemporalAAEnabled(), postProcessingData.isEnabled);
			RenderingLayerUtils.Event renderingLayersEvent;
			RenderingLayerUtils.MaskSize renderingLayerMaskSize;
			bool requiresRenderingLayer = RenderingLayerUtils.RequireRenderingLayers(this, base.rendererFeatures, cameraTargetDescriptor.msaaSamples, out renderingLayersEvent, out renderingLayerMaskSize);
			if (this.IsGLDevice())
			{
				requiresRenderingLayer = false;
			}
			bool renderingLayerProvidesByDepthNormalPass = false;
			bool renderingLayerProvidesRenderObjectPass = false;
			if (requiresRenderingLayer && this.renderingModeActual != RenderingMode.Deferred)
			{
				if (renderingLayersEvent != RenderingLayerUtils.Event.DepthNormalPrePass)
				{
					if (renderingLayersEvent != RenderingLayerUtils.Event.Opaque)
					{
						throw new ArgumentOutOfRangeException();
					}
					renderingLayerProvidesRenderObjectPass = true;
				}
				else
				{
					renderingLayerProvidesByDepthNormalPass = true;
				}
			}
			if (renderingLayerProvidesByDepthNormalPass)
			{
				renderPassInputs.requiresNormalsTexture = true;
			}
			if (this.m_DeferredLights != null)
			{
				this.m_DeferredLights.RenderingLayerMaskSize = renderingLayerMaskSize;
				this.m_DeferredLights.UseDecalLayers = requiresRenderingLayer;
				this.m_DeferredLights.HasNormalPrepass = renderPassInputs.requiresNormalsTexture;
				this.m_DeferredLights.ResolveMixedLightingMode(lightData);
				this.m_DeferredLights.IsOverlay = cameraData.renderType == CameraRenderType.Overlay;
				if (this.m_DeferredLights.UseFramebufferFetch)
				{
					foreach (ScriptableRenderPass pass in base.activeRenderPassQueue)
					{
						if (pass.renderPassEvent >= RenderPassEvent.AfterRenderingGbuffer && pass.renderPassEvent <= RenderPassEvent.BeforeRenderingDeferredLights)
						{
							this.m_DeferredLights.DisableFramebufferFetchInput();
							break;
						}
					}
				}
			}
			bool applyPostProcessing = cameraData.postProcessEnabled && this.m_PostProcessPasses.isCreated;
			bool anyPostProcessing = postProcessingData.isEnabled && this.m_PostProcessPasses.isCreated;
			bool cameraHasPostProcessingWithDepth = applyPostProcessing && cameraData.postProcessingRequiresDepthTexture;
			bool generateColorGradingLUT = cameraData.postProcessEnabled && this.m_PostProcessPasses.isCreated;
			bool isSceneViewOrPreviewCamera = cameraData.isSceneViewCamera || cameraData.isPreviewCamera;
			object obj = cameraData.requiresDepthTexture || renderPassInputs.requiresDepthTexture || base.useDepthPriming;
			bool isGizmosEnabled = false;
			bool mainLightShadows = this.m_MainLightShadowCasterPass.Setup(universalRenderingData, cameraData, lightData, shadowData);
			bool additionalLightShadows = this.m_AdditionalLightsShadowCasterPass.Setup(universalRenderingData, cameraData, lightData, shadowData);
			bool transparentsNeedSettingsPass = this.m_TransparentSettingsPass.Setup();
			bool forcePrepass = this.m_CopyDepthMode == CopyDepthMode.ForcePrepass;
			object obj2 = obj;
			bool requiresDepthPrepass = (obj2 | cameraHasPostProcessingWithDepth) != null && (!this.CanCopyDepth(cameraData) || forcePrepass);
			requiresDepthPrepass = requiresDepthPrepass || isSceneViewOrPreviewCamera;
			requiresDepthPrepass = requiresDepthPrepass || isGizmosEnabled;
			requiresDepthPrepass = requiresDepthPrepass || isPreviewCamera;
			requiresDepthPrepass |= renderPassInputs.requiresDepthPrepass;
			requiresDepthPrepass |= renderPassInputs.requiresNormalsTexture;
			if (requiresDepthPrepass && this.renderingModeActual == RenderingMode.Deferred && !renderPassInputs.requiresNormalsTexture)
			{
				requiresDepthPrepass = false;
			}
			requiresDepthPrepass |= base.useDepthPriming;
			if (obj2 != null)
			{
				RenderPassEvent copyDepthPassEvent = ((this.m_CopyDepthMode == CopyDepthMode.AfterTransparents) ? RenderPassEvent.AfterRenderingTransparents : RenderPassEvent.AfterRenderingOpaques);
				if (renderPassInputs.requiresDepthTexture)
				{
					copyDepthPassEvent = (RenderPassEvent)Mathf.Min(500, renderPassInputs.requiresDepthTextureEarliestEvent - (RenderPassEvent)1);
				}
				this.m_CopyDepthPass.renderPassEvent = copyDepthPassEvent;
				if (copyDepthPassEvent < RenderPassEvent.AfterRenderingTransparents)
				{
					this.m_CopyDepthPass.m_CopyResolvedDepth = false;
					this.m_CopyDepthMode = CopyDepthMode.AfterOpaques;
				}
			}
			else if (cameraHasPostProcessingWithDepth || isSceneViewOrPreviewCamera || isGizmosEnabled)
			{
				this.m_CopyDepthPass.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
			}
			createColorTexture |= this.RequiresIntermediateColorTexture(cameraData, ref renderPassInputs);
			createColorTexture &= !isPreviewCamera;
			bool createDepthTexture = (obj2 | cameraHasPostProcessingWithDepth) != null && !requiresDepthPrepass;
			createDepthTexture |= !cameraData.resolveFinalTarget;
			createDepthTexture |= this.renderingModeActual == RenderingMode.Deferred && !this.useRenderPassEnabled;
			createDepthTexture |= base.useDepthPriming;
			createDepthTexture = createDepthTexture || renderingLayerProvidesRenderObjectPass;
			if (cameraData.xr.enabled)
			{
				createColorTexture = createColorTexture || createDepthTexture;
			}
			if (RTHandles.rtHandleProperties.rtHandleScale.x != 1f || RTHandles.rtHandleProperties.rtHandleScale.y != 1f)
			{
				createColorTexture = createColorTexture || createDepthTexture;
			}
			if (this.useRenderPassEnabled || base.useDepthPriming)
			{
				createColorTexture = createColorTexture || createDepthTexture;
			}
			if (SystemInfo.graphicsUVStartsAtTop)
			{
				createColorTexture = createColorTexture || createDepthTexture;
			}
			RenderTextureDescriptor colorDescriptor = cameraTargetDescriptor;
			colorDescriptor.useMipMap = false;
			colorDescriptor.autoGenerateMips = false;
			colorDescriptor.depthStencilFormat = GraphicsFormat.None;
			this.m_ColorBufferSystem.SetCameraSettings(colorDescriptor, FilterMode.Bilinear);
			if (cameraData.renderType == CameraRenderType.Base)
			{
				bool sceneViewFilterEnabled = camera.sceneViewFilterMode == Camera.SceneViewFilterMode.ShowFiltered;
				bool intermediateRenderTexture = (createColorTexture || createDepthTexture) && !sceneViewFilterEnabled;
				createDepthTexture = createDepthTexture || createColorTexture;
				RenderTargetIdentifier targetId = BuiltinRenderTextureType.CameraTarget;
				if (cameraData.xr.enabled)
				{
					targetId = cameraData.xr.renderTarget;
				}
				if (this.m_TargetColorHandle == null)
				{
					this.m_TargetColorHandle = RTHandles.Alloc(targetId);
				}
				else if (this.m_TargetColorHandle.nameID != targetId)
				{
					RTHandleStaticHelpers.SetRTHandleUserManagedWrapper(ref this.m_TargetColorHandle, targetId);
				}
				if (this.m_TargetDepthHandle == null)
				{
					this.m_TargetDepthHandle = RTHandles.Alloc(targetId);
				}
				else if (this.m_TargetDepthHandle.nameID != targetId)
				{
					RTHandleStaticHelpers.SetRTHandleUserManagedWrapper(ref this.m_TargetDepthHandle, targetId);
				}
				if (intermediateRenderTexture)
				{
					this.CreateCameraRenderTarget(context, ref cameraTargetDescriptor, cmd, cameraData);
				}
				this.m_RenderOpaqueForwardPass.m_IsActiveTargetBackBuffer = !intermediateRenderTexture;
				this.m_RenderTransparentForwardPass.m_IsActiveTargetBackBuffer = !intermediateRenderTexture;
				this.m_XROcclusionMeshPass.m_IsActiveTargetBackBuffer = !intermediateRenderTexture;
				this.m_ActiveCameraColorAttachment = (createColorTexture ? this.m_ColorBufferSystem.PeekBackBuffer() : this.m_TargetColorHandle);
				this.m_ActiveCameraDepthAttachment = (createDepthTexture ? this.m_CameraDepthAttachment : this.m_TargetDepthHandle);
			}
			else
			{
				UniversalAdditionalCameraData baseCameraData;
				cameraData.baseCamera.TryGetComponent<UniversalAdditionalCameraData>(out baseCameraData);
				UniversalRenderer baseRenderer = (UniversalRenderer)baseCameraData.scriptableRenderer;
				if (this.m_ColorBufferSystem != baseRenderer.m_ColorBufferSystem)
				{
					this.m_ColorBufferSystem.Dispose();
					this.m_ColorBufferSystem = baseRenderer.m_ColorBufferSystem;
				}
				this.m_ActiveCameraColorAttachment = this.m_ColorBufferSystem.PeekBackBuffer();
				this.m_ActiveCameraDepthAttachment = baseRenderer.m_ActiveCameraDepthAttachment;
				this.m_TargetColorHandle = baseRenderer.m_TargetColorHandle;
				this.m_TargetDepthHandle = baseRenderer.m_TargetDepthHandle;
			}
			if (base.rendererFeatures.Count != 0 && !isPreviewCamera)
			{
				base.ConfigureCameraColorTarget(this.m_ColorBufferSystem.PeekBackBuffer());
			}
			bool copyColorPass = cameraData.requiresOpaqueTexture || renderPassInputs.requiresColorTexture;
			copyColorPass &= !isPreviewCamera;
			base.ConfigureCameraTarget(this.m_ActiveCameraColorAttachment, this.m_ActiveCameraDepthAttachment);
			bool hasPassesAfterPostProcessing = base.activeRenderPassQueue.Find((ScriptableRenderPass x) => x.renderPassEvent == RenderPassEvent.AfterRenderingPostProcessing) != null;
			if (mainLightShadows)
			{
				base.EnqueuePass(this.m_MainLightShadowCasterPass);
			}
			if (additionalLightShadows)
			{
				base.EnqueuePass(this.m_AdditionalLightsShadowCasterPass);
			}
			bool requiresDepthCopyPass = !requiresDepthPrepass && (cameraData.requiresDepthTexture || cameraHasPostProcessingWithDepth || renderPassInputs.requiresDepthTexture) && createDepthTexture;
			if (base.DebugHandler != null && base.DebugHandler.IsActiveForCamera(cameraData.isPreviewCamera))
			{
				DebugFullScreenMode fullScreenMode;
				base.DebugHandler.TryGetFullscreenDebugMode(out fullScreenMode);
				if (fullScreenMode == DebugFullScreenMode.Depth)
				{
					requiresDepthPrepass = true;
				}
				if (!base.DebugHandler.IsLightingActive)
				{
					mainLightShadows = false;
					additionalLightShadows = false;
					if (!isSceneViewOrPreviewCamera)
					{
						requiresDepthPrepass = false;
						base.useDepthPriming = false;
						generateColorGradingLUT = false;
						copyColorPass = false;
						requiresDepthCopyPass = false;
					}
				}
				if (this.useRenderPassEnabled)
				{
					this.useRenderPassEnabled = base.DebugHandler.IsRenderPassSupported;
				}
			}
			cameraData.renderer.useDepthPriming = base.useDepthPriming;
			if (this.renderingModeActual == RenderingMode.Deferred && this.m_DeferredLights.UseFramebufferFetch && (RenderPassEvent.AfterRenderingGbuffer == renderPassInputs.requiresDepthNormalAtEvent || !this.useRenderPassEnabled))
			{
				this.m_DeferredLights.DisableFramebufferFetchInput();
			}
			if ((this.renderingModeActual == RenderingMode.Deferred && !this.useRenderPassEnabled) || requiresDepthPrepass || requiresDepthCopyPass)
			{
				RenderTextureDescriptor depthDescriptor = cameraTargetDescriptor;
				if (requiresDepthPrepass && this.renderingModeActual != RenderingMode.Deferred)
				{
					depthDescriptor.graphicsFormat = GraphicsFormat.None;
					depthDescriptor.depthStencilFormat = this.cameraDepthTextureFormat;
				}
				else
				{
					depthDescriptor.graphicsFormat = GraphicsFormat.R32_SFloat;
					depthDescriptor.depthStencilFormat = GraphicsFormat.None;
				}
				depthDescriptor.msaaSamples = 1;
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_DepthTexture, in depthDescriptor, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, "_CameraDepthTexture");
				cmd.SetGlobalTexture(this.m_DepthTexture.name, this.m_DepthTexture.nameID);
				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();
			}
			if (requiresRenderingLayer || (this.renderingModeActual == RenderingMode.Deferred && this.m_DeferredLights.UseRenderingLayers))
			{
				ref RTHandle renderingLayersTexture = ref this.m_DecalLayersTexture;
				string renderingLayersTextureName = "_CameraRenderingLayersTexture";
				if (this.renderingModeActual == RenderingMode.Deferred && this.m_DeferredLights.UseRenderingLayers)
				{
					renderingLayersTexture = ref this.m_DeferredLights.GbufferAttachments[this.m_DeferredLights.GBufferRenderingLayers];
					renderingLayersTextureName = renderingLayersTexture.name;
				}
				RenderTextureDescriptor renderingLayersDescriptor = cameraTargetDescriptor;
				renderingLayersDescriptor.depthStencilFormat = GraphicsFormat.None;
				if (!renderingLayerProvidesRenderObjectPass)
				{
					renderingLayersDescriptor.msaaSamples = 1;
				}
				if (this.renderingModeActual == RenderingMode.Deferred && this.m_DeferredLights.UseRenderingLayers)
				{
					renderingLayersDescriptor.graphicsFormat = this.m_DeferredLights.GetGBufferFormat(this.m_DeferredLights.GBufferRenderingLayers);
				}
				else
				{
					renderingLayersDescriptor.graphicsFormat = RenderingLayerUtils.GetFormat(renderingLayerMaskSize);
				}
				if (this.renderingModeActual == RenderingMode.Deferred && this.m_DeferredLights.UseRenderingLayers)
				{
					this.m_DeferredLights.ReAllocateGBufferIfNeeded(renderingLayersDescriptor, this.m_DeferredLights.GBufferRenderingLayers);
				}
				else
				{
					RenderingUtils.ReAllocateHandleIfNeeded(ref renderingLayersTexture, in renderingLayersDescriptor, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, renderingLayersTextureName);
				}
				cmd.SetGlobalTexture(renderingLayersTexture.name, renderingLayersTexture.nameID);
				RenderingLayerUtils.SetupProperties(CommandBufferHelpers.GetRasterCommandBuffer(cmd), renderingLayerMaskSize);
				if (this.renderingModeActual == RenderingMode.Deferred)
				{
					cmd.SetGlobalTexture("_CameraRenderingLayersTexture", renderingLayersTexture.nameID);
				}
				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();
			}
			if (requiresDepthPrepass && renderPassInputs.requiresNormalsTexture)
			{
				ref RTHandle normalsTexture = ref this.m_NormalsTexture;
				string normalsTextureName = "_CameraNormalsTexture";
				if (this.renderingModeActual == RenderingMode.Deferred)
				{
					normalsTexture = ref this.m_DeferredLights.GbufferAttachments[this.m_DeferredLights.GBufferNormalSmoothnessIndex];
					normalsTextureName = normalsTexture.name;
				}
				RenderTextureDescriptor normalDescriptor = cameraTargetDescriptor;
				normalDescriptor.depthStencilFormat = GraphicsFormat.None;
				normalDescriptor.msaaSamples = (base.useDepthPriming ? cameraTargetDescriptor.msaaSamples : 1);
				if (this.renderingModeActual == RenderingMode.Deferred)
				{
					normalDescriptor.graphicsFormat = this.m_DeferredLights.GetGBufferFormat(this.m_DeferredLights.GBufferNormalSmoothnessIndex);
				}
				else
				{
					normalDescriptor.graphicsFormat = DepthNormalOnlyPass.GetGraphicsFormat();
				}
				if (this.renderingModeActual == RenderingMode.Deferred)
				{
					this.m_DeferredLights.ReAllocateGBufferIfNeeded(normalDescriptor, this.m_DeferredLights.GBufferNormalSmoothnessIndex);
				}
				else
				{
					RenderingUtils.ReAllocateHandleIfNeeded(ref normalsTexture, in normalDescriptor, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, normalsTextureName);
				}
				cmd.SetGlobalTexture(normalsTexture.name, normalsTexture.nameID);
				if (this.renderingModeActual == RenderingMode.Deferred)
				{
					cmd.SetGlobalTexture("_CameraNormalsTexture", normalsTexture.nameID);
				}
				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();
			}
			if (requiresDepthPrepass)
			{
				if (renderPassInputs.requiresNormalsTexture)
				{
					if (this.renderingModeActual == RenderingMode.Deferred)
					{
						int gbufferNormalIndex = this.m_DeferredLights.GBufferNormalSmoothnessIndex;
						if (this.m_DeferredLights.UseRenderingLayers)
						{
							this.m_DepthNormalPrepass.Setup(this.m_ActiveCameraDepthAttachment, this.m_DeferredLights.GbufferAttachments[gbufferNormalIndex], this.m_DeferredLights.GbufferAttachments[this.m_DeferredLights.GBufferRenderingLayers]);
						}
						else if (renderingLayerProvidesByDepthNormalPass)
						{
							this.m_DepthNormalPrepass.Setup(this.m_ActiveCameraDepthAttachment, this.m_DeferredLights.GbufferAttachments[gbufferNormalIndex], this.m_DecalLayersTexture);
						}
						else
						{
							this.m_DepthNormalPrepass.Setup(this.m_ActiveCameraDepthAttachment, this.m_DeferredLights.GbufferAttachments[gbufferNormalIndex]);
						}
						if (RenderPassEvent.AfterRenderingGbuffer <= renderPassInputs.requiresDepthNormalAtEvent && renderPassInputs.requiresDepthNormalAtEvent <= RenderPassEvent.BeforeRenderingOpaques)
						{
							this.m_DepthNormalPrepass.shaderTagIds = UniversalRenderer.k_DepthNormalsOnly;
						}
					}
					else if (renderingLayerProvidesByDepthNormalPass)
					{
						this.m_DepthNormalPrepass.Setup(this.m_DepthTexture, this.m_NormalsTexture, this.m_DecalLayersTexture);
					}
					else
					{
						this.m_DepthNormalPrepass.Setup(this.m_DepthTexture, this.m_NormalsTexture);
					}
					base.EnqueuePass(this.m_DepthNormalPrepass);
				}
				else if (this.renderingModeActual != RenderingMode.Deferred)
				{
					this.m_DepthPrepass.Setup(cameraTargetDescriptor, this.m_DepthTexture);
					base.EnqueuePass(this.m_DepthPrepass);
				}
			}
			if (base.useDepthPriming)
			{
				this.m_PrimedDepthCopyPass.Setup(this.m_ActiveCameraDepthAttachment, this.m_DepthTexture);
				base.EnqueuePass(this.m_PrimedDepthCopyPass);
			}
			if (generateColorGradingLUT)
			{
				RenderTextureDescriptor desc;
				FilterMode filterMode;
				this.colorGradingLutPass.ConfigureDescriptor(in postProcessingData, out desc, out filterMode);
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_PostProcessPasses.m_ColorGradingLut, in desc, filterMode, TextureWrapMode.Clamp, 0, 0f, "_InternalGradingLut");
				ColorGradingLutPass colorGradingLutPass = this.colorGradingLutPass;
				RTHandle rthandle = this.colorGradingLut;
				colorGradingLutPass.Setup(in rthandle);
				base.EnqueuePass(this.colorGradingLutPass);
			}
			if (cameraData.xr.hasValidOcclusionMesh)
			{
				base.EnqueuePass(this.m_XROcclusionMeshPass);
			}
			bool lastCameraInTheStack = cameraData.resolveFinalTarget;
			if (this.renderingModeActual == RenderingMode.Deferred)
			{
				if (this.m_DeferredLights.UseFramebufferFetch && (RenderPassEvent.AfterRenderingGbuffer == renderPassInputs.requiresDepthNormalAtEvent || !this.useRenderPassEnabled))
				{
					this.m_DeferredLights.DisableFramebufferFetchInput();
				}
				this.EnqueueDeferred(cameraData.cameraTargetDescriptor, requiresDepthPrepass, renderPassInputs.requiresNormalsTexture, renderingLayerProvidesByDepthNormalPass, mainLightShadows, additionalLightShadows);
			}
			else
			{
				RenderBufferStoreAction opaquePassColorStoreAction = RenderBufferStoreAction.Store;
				if (cameraTargetDescriptor.msaaSamples > 1)
				{
					opaquePassColorStoreAction = (copyColorPass ? RenderBufferStoreAction.StoreAndResolve : RenderBufferStoreAction.Store);
				}
				RenderBufferStoreAction opaquePassDepthStoreAction = ((copyColorPass || requiresDepthCopyPass || !lastCameraInTheStack) ? RenderBufferStoreAction.Store : RenderBufferStoreAction.DontCare);
				if (cameraData.xr.enabled && cameraData.xr.copyDepth)
				{
					opaquePassDepthStoreAction = RenderBufferStoreAction.Store;
				}
				if (requiresDepthCopyPass && cameraTargetDescriptor.msaaSamples > 1 && RenderingUtils.MultisampleDepthResolveSupported() && this.m_CopyDepthPass.renderPassEvent == RenderPassEvent.AfterRenderingTransparents && !copyColorPass)
				{
					if (opaquePassDepthStoreAction == RenderBufferStoreAction.Store)
					{
						opaquePassDepthStoreAction = RenderBufferStoreAction.StoreAndResolve;
					}
					else if (opaquePassDepthStoreAction == RenderBufferStoreAction.DontCare)
					{
						opaquePassDepthStoreAction = RenderBufferStoreAction.Resolve;
					}
				}
				DrawObjectsPass renderOpaqueForwardPass;
				if (renderingLayerProvidesRenderObjectPass)
				{
					renderOpaqueForwardPass = this.m_RenderOpaqueForwardWithRenderingLayersPass;
					this.m_RenderOpaqueForwardWithRenderingLayersPass.Setup(this.m_ActiveCameraColorAttachment, this.m_DecalLayersTexture, this.m_ActiveCameraDepthAttachment);
				}
				else
				{
					renderOpaqueForwardPass = this.m_RenderOpaqueForwardPass;
				}
				renderOpaqueForwardPass.ConfigureColorStoreAction(opaquePassColorStoreAction, 0U);
				renderOpaqueForwardPass.ConfigureDepthStoreAction(opaquePassDepthStoreAction);
				ClearFlag opaqueForwardPassClearFlag = ((base.activeRenderPassQueue.Find((ScriptableRenderPass x) => x.renderPassEvent <= RenderPassEvent.BeforeRenderingOpaques && !x.overrideCameraTarget) != null || cameraData.renderType != CameraRenderType.Base || camera.clearFlags == CameraClearFlags.Nothing) ? ClearFlag.None : ClearFlag.Color);
				if (SystemInfo.usesLoadStoreActions)
				{
					renderOpaqueForwardPass.ConfigureClear(opaqueForwardPassClearFlag, Color.black);
				}
				base.EnqueuePass(renderOpaqueForwardPass);
			}
			Skybox cameraSkybox;
			if (camera.clearFlags == CameraClearFlags.Skybox && cameraData.renderType != CameraRenderType.Overlay && (RenderSettings.skybox != null || (camera.TryGetComponent<Skybox>(out cameraSkybox) && cameraSkybox.material != null)))
			{
				base.EnqueuePass(this.m_DrawSkyboxPass);
			}
			if (requiresDepthCopyPass && (this.renderingModeActual != RenderingMode.Deferred || !this.useRenderPassEnabled || renderPassInputs.requiresDepthTexture))
			{
				this.m_CopyDepthPass.Setup(this.m_ActiveCameraDepthAttachment, this.m_DepthTexture);
				base.EnqueuePass(this.m_CopyDepthPass);
			}
			if (cameraData.renderType == CameraRenderType.Base && !requiresDepthPrepass && !requiresDepthCopyPass)
			{
				Shader.SetGlobalTexture("_CameraDepthTexture", SystemInfo.usesReversedZBuffer ? Texture2D.blackTexture : Texture2D.whiteTexture);
			}
			if (copyColorPass)
			{
				Downsampling downsamplingMethod = UniversalRenderPipeline.asset.opaqueDownsampling;
				RenderTextureDescriptor descriptor = cameraTargetDescriptor;
				FilterMode filterMode2;
				CopyColorPass.ConfigureDescriptor(downsamplingMethod, ref descriptor, out filterMode2);
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_OpaqueColor, in descriptor, filterMode2, TextureWrapMode.Clamp, 1, 0f, "_CameraOpaqueTexture");
				this.m_CopyColorPass.Setup(this.m_ActiveCameraColorAttachment, this.m_OpaqueColor, downsamplingMethod);
				base.EnqueuePass(this.m_CopyColorPass);
			}
			if (renderPassInputs.requiresMotionVectors)
			{
				RenderTextureDescriptor colorDesc2 = cameraTargetDescriptor;
				colorDesc2.graphicsFormat = GraphicsFormat.R16G16_SFloat;
				colorDesc2.depthStencilFormat = GraphicsFormat.None;
				colorDesc2.msaaSamples = 1;
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_MotionVectorColor, in colorDesc2, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, "_MotionVectorTexture");
				RenderTextureDescriptor depthDescriptor2 = cameraTargetDescriptor;
				depthDescriptor2.graphicsFormat = GraphicsFormat.None;
				depthDescriptor2.depthStencilFormat = cameraTargetDescriptor.depthStencilFormat;
				depthDescriptor2.msaaSamples = 1;
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_MotionVectorDepth, in depthDescriptor2, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, "_MotionVectorDepthTexture");
				MotionVectorRenderPass.SetMotionVectorGlobalMatrices(cmd, cameraData);
				this.m_MotionVectorPass.Setup(this.m_MotionVectorColor, this.m_MotionVectorDepth);
				base.EnqueuePass(this.m_MotionVectorPass);
			}
			if (transparentsNeedSettingsPass)
			{
				base.EnqueuePass(this.m_TransparentSettingsPass);
			}
			RenderBufferStoreAction transparentPassColorStoreAction = ((cameraTargetDescriptor.msaaSamples > 1 && lastCameraInTheStack && !isPreviewCamera) ? RenderBufferStoreAction.Resolve : RenderBufferStoreAction.Store);
			RenderBufferStoreAction transparentPassDepthStoreAction = (lastCameraInTheStack ? RenderBufferStoreAction.DontCare : RenderBufferStoreAction.Store);
			if (requiresDepthCopyPass && this.m_CopyDepthPass.renderPassEvent >= RenderPassEvent.AfterRenderingTransparents)
			{
				transparentPassDepthStoreAction = RenderBufferStoreAction.Store;
				if (cameraTargetDescriptor.msaaSamples > 1 && RenderingUtils.MultisampleDepthResolveSupported())
				{
					transparentPassDepthStoreAction = RenderBufferStoreAction.Resolve;
				}
			}
			this.m_RenderTransparentForwardPass.ConfigureColorStoreAction(transparentPassColorStoreAction, 0U);
			this.m_RenderTransparentForwardPass.ConfigureDepthStoreAction(transparentPassDepthStoreAction);
			base.EnqueuePass(this.m_RenderTransparentForwardPass);
			base.EnqueuePass(this.m_OnRenderObjectCallbackPass);
			this.SetupRawColorDepthHistory(cameraData, ref cameraTargetDescriptor);
			bool shouldRenderUI = cameraData.rendersOverlayUI;
			bool outputToHDR = cameraData.isHDROutputActive;
			if (shouldRenderUI && outputToHDR)
			{
				this.m_DrawOffscreenUIPass.Setup(cameraData, this.cameraDepthTextureFormat);
				base.EnqueuePass(this.m_DrawOffscreenUIPass);
			}
			bool hasCaptureActions = cameraData.captureActions != null && lastCameraInTheStack;
			bool applyFinalPostProcessing = anyPostProcessing && lastCameraInTheStack && (cameraData.antialiasing == AntialiasingMode.FastApproximateAntialiasing || (cameraData.imageScalingMode == ImageScalingMode.Upscaling && cameraData.upscalingFilter != ImageUpscalingFilter.Linear) || (cameraData.IsTemporalAAEnabled() && cameraData.taaSettings.contrastAdaptiveSharpening > 0f)) && (base.DebugHandler == null || (base.DebugHandler != null && base.DebugHandler.IsPostProcessingAllowed));
			bool resolvePostProcessingToCameraTarget = !hasCaptureActions && !hasPassesAfterPostProcessing && !applyFinalPostProcessing;
			bool needsColorEncoding = base.DebugHandler == null || !base.DebugHandler.HDRDebugViewIsActive(cameraData.resolveFinalTarget);
			if (applyPostProcessing)
			{
				RenderTextureDescriptor desc2 = PostProcessPass.GetCompatibleDescriptor(cameraTargetDescriptor, cameraTargetDescriptor.width, cameraTargetDescriptor.height, cameraTargetDescriptor.graphicsFormat, GraphicsFormat.None);
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_PostProcessPasses.m_AfterPostProcessColor, in desc2, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, "_AfterPostProcessTexture");
			}
			if (lastCameraInTheStack)
			{
				this.SetupFinalPassDebug(cameraData);
				if (applyPostProcessing)
				{
					bool doSRGBEncoding = resolvePostProcessingToCameraTarget && needsColorEncoding;
					PostProcessPass postProcessPass = this.postProcessPass;
					bool flag = resolvePostProcessingToCameraTarget;
					RTHandle rthandle = this.colorGradingLut;
					postProcessPass.Setup(in cameraTargetDescriptor, in this.m_ActiveCameraColorAttachment, flag, in this.m_ActiveCameraDepthAttachment, in rthandle, in this.m_MotionVectorColor, applyFinalPostProcessing, doSRGBEncoding);
					base.EnqueuePass(this.postProcessPass);
				}
				RTHandle sourceForFinalPass = this.m_ActiveCameraColorAttachment;
				if (applyFinalPostProcessing)
				{
					this.finalPostProcessPass.SetupFinalPass(in sourceForFinalPass, true, needsColorEncoding);
					base.EnqueuePass(this.finalPostProcessPass);
				}
				if (cameraData.captureActions != null)
				{
					base.EnqueuePass(this.m_CapturePass);
				}
				if (!applyFinalPostProcessing && (!applyPostProcessing || hasPassesAfterPostProcessing || hasCaptureActions) && !(this.m_ActiveCameraColorAttachment.nameID == this.m_TargetColorHandle.nameID))
				{
					this.m_FinalBlitPass.Setup(cameraTargetDescriptor, sourceForFinalPass);
					base.EnqueuePass(this.m_FinalBlitPass);
				}
				if (shouldRenderUI && !outputToHDR)
				{
					base.EnqueuePass(this.m_DrawOverlayUIPass);
				}
				if (cameraData.xr.enabled && !(this.m_ActiveCameraDepthAttachment.nameID == cameraData.xr.renderTarget) && cameraData.xr.copyDepth)
				{
					this.m_XRCopyDepthPass.Setup(this.m_ActiveCameraDepthAttachment, this.m_TargetDepthHandle);
					this.m_XRCopyDepthPass.CopyToDepthXR = true;
					base.EnqueuePass(this.m_XRCopyDepthPass);
					return;
				}
			}
			else if (applyPostProcessing)
			{
				PostProcessPass postProcessPass2 = this.postProcessPass;
				bool flag2 = false;
				RTHandle rthandle = this.colorGradingLut;
				postProcessPass2.Setup(in cameraTargetDescriptor, in this.m_ActiveCameraColorAttachment, flag2, in this.m_ActiveCameraDepthAttachment, in rthandle, in this.m_MotionVectorColor, false, false);
				base.EnqueuePass(this.postProcessPass);
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00035900 File Offset: 0x00033B00
		private void SetupVFXCameraBuffer(UniversalCameraData cameraData)
		{
			if (cameraData != null && cameraData.historyManager != null)
			{
				VFXCameraBufferTypes vfxBufferNeeded = VFXManager.IsCameraBufferNeeded(cameraData.camera);
				if (vfxBufferNeeded.HasFlag(VFXCameraBufferTypes.Color))
				{
					cameraData.historyManager.RequestAccess<RawColorHistory>();
					RawColorHistory historyForRead = cameraData.historyManager.GetHistoryForRead<RawColorHistory>();
					RTHandle handle = ((historyForRead != null) ? historyForRead.GetCurrentTexture(0) : null);
					VFXManager.SetCameraBuffer(cameraData.camera, VFXCameraBufferTypes.Color, handle, 0, 0, (int)((float)cameraData.pixelWidth * cameraData.renderScale), (int)((float)cameraData.pixelHeight * cameraData.renderScale));
				}
				if (vfxBufferNeeded.HasFlag(VFXCameraBufferTypes.Depth))
				{
					cameraData.historyManager.RequestAccess<RawDepthHistory>();
					RawDepthHistory historyForRead2 = cameraData.historyManager.GetHistoryForRead<RawDepthHistory>();
					RTHandle handle2 = ((historyForRead2 != null) ? historyForRead2.GetCurrentTexture(0) : null);
					VFXManager.SetCameraBuffer(cameraData.camera, VFXCameraBufferTypes.Depth, handle2, 0, 0, (int)((float)cameraData.pixelWidth * cameraData.renderScale), (int)((float)cameraData.pixelHeight * cameraData.renderScale));
				}
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x000359FC File Offset: 0x00033BFC
		private void SetupRawColorDepthHistory(UniversalCameraData cameraData, ref RenderTextureDescriptor cameraTargetDescriptor)
		{
			if (cameraData != null && cameraData.historyManager != null)
			{
				UniversalCameraHistory history = cameraData.historyManager;
				bool xrMultipassEnabled = cameraData.xr.enabled && !cameraData.xr.singlePassEnabled;
				int multipassId = cameraData.xr.multipassId;
				if (history.IsAccessRequested<RawColorHistory>())
				{
					RTHandle activeCameraColorAttachment = this.m_ActiveCameraColorAttachment;
					if (((activeCameraColorAttachment != null) ? activeCameraColorAttachment.rt : null) != null)
					{
						RawColorHistory colorHistory = history.GetHistoryForWrite<RawColorHistory>();
						if (colorHistory != null)
						{
							colorHistory.Update(ref cameraTargetDescriptor, xrMultipassEnabled);
							if (colorHistory.GetCurrentTexture(multipassId) != null)
							{
								this.m_HistoryRawColorCopyPass.Setup(this.m_ActiveCameraColorAttachment, colorHistory.GetCurrentTexture(multipassId), Downsampling.None);
								base.EnqueuePass(this.m_HistoryRawColorCopyPass);
							}
						}
					}
				}
				if (history.IsAccessRequested<RawDepthHistory>())
				{
					RTHandle activeCameraDepthAttachment = this.m_ActiveCameraDepthAttachment;
					if (((activeCameraDepthAttachment != null) ? activeCameraDepthAttachment.rt : null) != null)
					{
						RawDepthHistory depthHistory = history.GetHistoryForWrite<RawDepthHistory>();
						if (depthHistory != null)
						{
							if (!this.m_HistoryRawDepthCopyPass.CopyToDepth)
							{
								RenderTextureDescriptor tempColorDepthDesc = cameraTargetDescriptor;
								tempColorDepthDesc.colorFormat = RenderTextureFormat.RFloat;
								tempColorDepthDesc.graphicsFormat = GraphicsFormat.R32_SFloat;
								tempColorDepthDesc.depthStencilFormat = GraphicsFormat.None;
								depthHistory.Update(ref tempColorDepthDesc, xrMultipassEnabled);
							}
							else
							{
								RenderTextureDescriptor tempColorDepthDesc2 = cameraData.cameraTargetDescriptor;
								tempColorDepthDesc2.graphicsFormat = GraphicsFormat.None;
								depthHistory.Update(ref tempColorDepthDesc2, xrMultipassEnabled);
							}
							if (depthHistory.GetCurrentTexture(multipassId) != null)
							{
								this.m_HistoryRawDepthCopyPass.Setup(this.m_ActiveCameraDepthAttachment, depthHistory.GetCurrentTexture(multipassId));
								base.EnqueuePass(this.m_HistoryRawDepthCopyPass);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00035B70 File Offset: 0x00033D70
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void SetupLights(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			UniversalRenderingData universalRenderingData = base.frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = base.frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = base.frameData.Get<UniversalLightData>();
			this.m_ForwardLights.SetupLights(CommandBufferHelpers.GetUnsafeCommandBuffer(*renderingData.commandBuffer), universalRenderingData, cameraData, lightData);
			if (this.renderingModeActual == RenderingMode.Deferred)
			{
				this.m_DeferredLights.SetupLights(*renderingData.commandBuffer, cameraData, new Vector2Int(cameraData.cameraTargetDescriptor.width, cameraData.cameraTargetDescriptor.height), lightData, false);
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00035BF4 File Offset: 0x00033DF4
		public unsafe override void SetupCullingParameters(ref ScriptableCullingParameters cullingParameters, ref CameraData cameraData)
		{
			if (this.renderingModeActual == RenderingMode.ForwardPlus && UniversalRenderPipeline.asset.reflectionProbeBlending)
			{
				cullingParameters.cullingOptions |= CullingOptions.DisablePerObjectCulling;
			}
			bool flag = !UniversalRenderPipeline.asset.supportsMainLightShadows && !UniversalRenderPipeline.asset.supportsAdditionalLightShadows;
			bool isShadowDistanceZero = Mathf.Approximately(*cameraData.maxShadowDistance, 0f);
			if (flag || isShadowDistanceZero)
			{
				cullingParameters.cullingOptions &= ~CullingOptions.ShadowCasters;
			}
			if (this.renderingModeActual == RenderingMode.Deferred)
			{
				cullingParameters.maximumVisibleLights = 65535;
			}
			else if (this.renderingModeActual == RenderingMode.ForwardPlus)
			{
				cullingParameters.maximumVisibleLights = UniversalRenderPipeline.maxVisibleAdditionalLights;
				cullingParameters.reflectionProbeSortingCriteria = ReflectionProbeSortingCriteria.None;
			}
			else
			{
				cullingParameters.maximumVisibleLights = UniversalRenderPipeline.maxVisibleAdditionalLights + 1;
			}
			cullingParameters.shadowDistance = *cameraData.maxShadowDistance;
			cullingParameters.conservativeEnclosingSphere = UniversalRenderPipeline.asset.conservativeEnclosingSphere;
			cullingParameters.numIterationsEnclosingSphere = UniversalRenderPipeline.asset.numIterationsEnclosingSphere;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00035CD3 File Offset: 0x00033ED3
		public override void FinishRendering(CommandBuffer cmd)
		{
			this.m_ColorBufferSystem.Clear();
			this.m_ActiveCameraColorAttachment = null;
			this.m_ActiveCameraDepthAttachment = null;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00035CF0 File Offset: 0x00033EF0
		private void EnqueueDeferred(RenderTextureDescriptor cameraTargetDescriptor, bool hasDepthPrepass, bool hasNormalPrepass, bool hasRenderingLayerPrepass, bool applyMainShadow, bool applyAdditionalShadow)
		{
			this.m_DeferredLights.Setup(applyAdditionalShadow ? this.m_AdditionalLightsShadowCasterPass : null, hasDepthPrepass, hasNormalPrepass, hasRenderingLayerPrepass, this.m_DepthTexture, this.m_ActiveCameraDepthAttachment, this.m_ActiveCameraColorAttachment);
			if (this.useRenderPassEnabled && this.m_DeferredLights.UseFramebufferFetch)
			{
				this.m_GBufferPass.Configure(null, cameraTargetDescriptor);
				this.m_DeferredPass.Configure(null, cameraTargetDescriptor);
			}
			base.EnqueuePass(this.m_GBufferPass);
			if (!this.useRenderPassEnabled || !this.m_DeferredLights.UseFramebufferFetch)
			{
				this.m_GBufferCopyDepthPass.Setup(this.m_CameraDepthAttachment, this.m_DepthTexture);
				base.EnqueuePass(this.m_GBufferCopyDepthPass);
			}
			base.EnqueuePass(this.m_DeferredPass);
			base.EnqueuePass(this.m_RenderOpaqueForwardOnlyPass);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00035DB8 File Offset: 0x00033FB8
		private UniversalRenderer.RenderPassInputSummary GetRenderPassInputs(bool isTemporalAAEnabled, bool postProcessingEnabled)
		{
			UniversalRenderer.RenderPassInputSummary inputSummary = default(UniversalRenderer.RenderPassInputSummary);
			inputSummary.requiresDepthNormalAtEvent = RenderPassEvent.BeforeRenderingOpaques;
			inputSummary.requiresDepthTextureEarliestEvent = RenderPassEvent.BeforeRenderingPostProcessing;
			for (int i = 0; i < base.activeRenderPassQueue.Count; i++)
			{
				ScriptableRenderPass pass = base.activeRenderPassQueue[i];
				bool needsDepth = (pass.input & ScriptableRenderPassInput.Depth) > ScriptableRenderPassInput.None;
				bool needsNormals = (pass.input & ScriptableRenderPassInput.Normal) > ScriptableRenderPassInput.None;
				bool needsColor = (pass.input & ScriptableRenderPassInput.Color) > ScriptableRenderPassInput.None;
				bool needsMotion = (pass.input & ScriptableRenderPassInput.Motion) > ScriptableRenderPassInput.None;
				bool eventBeforeRenderingOpaques = pass.renderPassEvent < RenderPassEvent.AfterRenderingOpaques;
				if (pass is DBufferRenderPass)
				{
					inputSummary.requiresColorTextureCreated = true;
				}
				inputSummary.requiresDepthTexture = inputSummary.requiresDepthTexture || needsDepth;
				inputSummary.requiresDepthPrepass |= needsNormals || (needsDepth && eventBeforeRenderingOpaques);
				inputSummary.requiresNormalsTexture = inputSummary.requiresNormalsTexture || needsNormals;
				inputSummary.requiresColorTexture = inputSummary.requiresColorTexture || needsColor;
				inputSummary.requiresMotionVectors = inputSummary.requiresMotionVectors || needsMotion;
				if (needsDepth)
				{
					inputSummary.requiresDepthTextureEarliestEvent = (RenderPassEvent)Mathf.Min((int)pass.renderPassEvent, (int)inputSummary.requiresDepthTextureEarliestEvent);
				}
				if (needsNormals || needsDepth)
				{
					inputSummary.requiresDepthNormalAtEvent = (RenderPassEvent)Mathf.Min((int)pass.renderPassEvent, (int)inputSummary.requiresDepthNormalAtEvent);
				}
			}
			if (isTemporalAAEnabled)
			{
				inputSummary.requiresMotionVectors = true;
			}
			if (postProcessingEnabled)
			{
				MotionBlur motionBlur = VolumeManager.instance.stack.GetComponent<MotionBlur>();
				if (motionBlur != null && motionBlur.IsActive() && motionBlur.mode.value == MotionBlurMode.CameraAndObjects)
				{
					inputSummary.requiresMotionVectors = true;
				}
			}
			if (inputSummary.requiresMotionVectors)
			{
				inputSummary.requiresDepthTexture = true;
				inputSummary.requiresDepthTextureEarliestEvent = (RenderPassEvent)Mathf.Min((int)this.m_MotionVectorPass.renderPassEvent, (int)inputSummary.requiresDepthTextureEarliestEvent);
			}
			return inputSummary;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00035F5C File Offset: 0x0003415C
		private void CreateCameraRenderTarget(ScriptableRenderContext context, ref RenderTextureDescriptor descriptor, CommandBuffer cmd, UniversalCameraData cameraData)
		{
			using (new ProfilingScope(UniversalRenderer.Profiling.createCameraRenderTarget))
			{
				if (this.m_ColorBufferSystem.PeekBackBuffer() == null || this.m_ColorBufferSystem.PeekBackBuffer().nameID != BuiltinRenderTextureType.CameraTarget)
				{
					this.m_ActiveCameraColorAttachment = this.m_ColorBufferSystem.GetBackBuffer(cmd);
					base.ConfigureCameraColorTarget(this.m_ActiveCameraColorAttachment);
					cmd.SetGlobalTexture("_CameraColorTexture", this.m_ActiveCameraColorAttachment.nameID);
					cmd.SetGlobalTexture("_AfterPostProcessTexture", this.m_ActiveCameraColorAttachment.nameID);
				}
				if (this.m_CameraDepthAttachment == null || this.m_CameraDepthAttachment.nameID != BuiltinRenderTextureType.CameraTarget)
				{
					RenderTextureDescriptor depthDescriptor = descriptor;
					depthDescriptor.useMipMap = false;
					depthDescriptor.autoGenerateMips = false;
					depthDescriptor.bindMS = false;
					if (depthDescriptor.msaaSamples > 1 && SystemInfo.supportsMultisampledTextures != 0)
					{
						if (this.IsDepthPrimingEnabled(cameraData))
						{
							depthDescriptor.bindMS = true;
						}
						else
						{
							depthDescriptor.bindMS = !RenderingUtils.MultisampleDepthResolveSupported() || this.m_CopyDepthMode != CopyDepthMode.AfterTransparents;
						}
					}
					if (this.IsGLESDevice())
					{
						depthDescriptor.bindMS = false;
					}
					depthDescriptor.graphicsFormat = GraphicsFormat.None;
					depthDescriptor.depthStencilFormat = this.cameraDepthAttachmentFormat;
					RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_CameraDepthAttachment, in depthDescriptor, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, "_CameraDepthAttachment");
					cmd.SetGlobalTexture(this.m_CameraDepthAttachment.name, this.m_CameraDepthAttachment.nameID);
					descriptor.depthStencilFormat = depthDescriptor.depthStencilFormat;
				}
			}
			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00036118 File Offset: 0x00034318
		private bool PlatformRequiresExplicitMsaaResolve()
		{
			return (!SystemInfo.supportsMultisampleAutoResolve || !Application.isMobilePlatform) && SystemInfo.graphicsDeviceType != GraphicsDeviceType.Metal;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00036138 File Offset: 0x00034338
		private bool RequiresIntermediateColorTexture(UniversalCameraData cameraData, ref UniversalRenderer.RenderPassInputSummary renderPassInputs)
		{
			if (cameraData.renderType == CameraRenderType.Base && !cameraData.resolveFinalTarget)
			{
				return true;
			}
			if (this.renderingModeActual == RenderingMode.Deferred)
			{
				return true;
			}
			bool isSceneViewCamera = cameraData.isSceneViewCamera;
			RenderTextureDescriptor cameraTargetDescriptor = cameraData.cameraTargetDescriptor;
			int msaaSamples = cameraTargetDescriptor.msaaSamples;
			bool isScaledRender = cameraData.imageScalingMode > ImageScalingMode.None;
			bool isCompatibleBackbufferTextureDimension = cameraTargetDescriptor.dimension == TextureDimension.Tex2D;
			bool requiresExplicitMsaaResolve = msaaSamples > 1 && this.PlatformRequiresExplicitMsaaResolve();
			bool flag = cameraData.targetTexture != null && !isSceneViewCamera;
			bool isCapturing = cameraData.captureActions != null;
			if (cameraData.xr.enabled)
			{
				isScaledRender = false;
				isCompatibleBackbufferTextureDimension = cameraData.xr.renderTargetDesc.dimension == cameraTargetDescriptor.dimension;
			}
			bool requiresBlitForOffscreenCamera = (cameraData.postProcessEnabled && this.m_PostProcessPasses.isCreated) || cameraData.requiresOpaqueTexture || requiresExplicitMsaaResolve || !cameraData.isDefaultViewport;
			if (flag)
			{
				return requiresBlitForOffscreenCamera;
			}
			return requiresBlitForOffscreenCamera || isSceneViewCamera || isScaledRender || cameraData.isHdrEnabled || !isCompatibleBackbufferTextureDimension || isCapturing || cameraData.requireSrgbConversion || renderPassInputs.requiresColorTexture || renderPassInputs.requiresColorTextureCreated;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00036254 File Offset: 0x00034454
		private bool CanCopyDepth(UniversalCameraData cameraData)
		{
			bool flag = cameraData.cameraTargetDescriptor.msaaSamples > 1;
			bool supportsTextureCopy = SystemInfo.copyTextureSupport > CopyTextureSupport.None;
			bool supportsDepthTarget = RenderingUtils.SupportsRenderTextureFormat(RenderTextureFormat.Depth);
			bool supportsDepthCopy = !flag && (supportsDepthTarget || supportsTextureCopy);
			bool msaaDepthResolve = flag && SystemInfo.supportsMultisampledTextures != 0;
			return (!this.IsGLESDevice() || !msaaDepthResolve) && (supportsDepthCopy || msaaDepthResolve);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x000362A8 File Offset: 0x000344A8
		internal override void SwapColorBuffer(CommandBuffer cmd)
		{
			this.m_ColorBufferSystem.Swap();
			if (this.m_ActiveCameraDepthAttachment.nameID != BuiltinRenderTextureType.CameraTarget)
			{
				base.ConfigureCameraTarget(this.m_ColorBufferSystem.GetBackBuffer(cmd), this.m_ActiveCameraDepthAttachment);
			}
			else
			{
				base.ConfigureCameraColorTarget(this.m_ColorBufferSystem.GetBackBuffer(cmd));
			}
			this.m_ActiveCameraColorAttachment = this.m_ColorBufferSystem.GetBackBuffer(cmd);
			cmd.SetGlobalTexture("_CameraColorTexture", this.m_ActiveCameraColorAttachment.nameID);
			cmd.SetGlobalTexture("_AfterPostProcessTexture", this.m_ActiveCameraColorAttachment.nameID);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00036342 File Offset: 0x00034542
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		internal override RTHandle GetCameraColorFrontBuffer(CommandBuffer cmd)
		{
			return this.m_ColorBufferSystem.GetFrontBuffer(cmd);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00036350 File Offset: 0x00034550
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		internal override RTHandle GetCameraColorBackBuffer(CommandBuffer cmd)
		{
			return this.m_ColorBufferSystem.GetBackBuffer(cmd);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0003635E File Offset: 0x0003455E
		internal override void EnableSwapBufferMSAA(bool enable)
		{
			this.m_ColorBufferSystem.EnableMSAA(enable);
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x000039B4 File Offset: 0x00001BB4
		internal override bool supportsNativeRenderPassRendergraphCompiler
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0003636C File Offset: 0x0003456C
		private bool DebugHandlerRequireDepthPass(UniversalCameraData cameraData)
		{
			DebugFullScreenMode debugFullScreenMode;
			return base.DebugHandler != null && base.DebugHandler.IsActiveForCamera(cameraData.isPreviewCamera) && base.DebugHandler.TryGetFullscreenDebugMode(out debugFullScreenMode);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x000363A8 File Offset: 0x000345A8
		private void CreateDebugTexture(RenderTextureDescriptor descriptor)
		{
			RenderTextureDescriptor debugTexDescriptor = descriptor;
			debugTexDescriptor.useMipMap = false;
			debugTexDescriptor.autoGenerateMips = false;
			debugTexDescriptor.bindMS = false;
			debugTexDescriptor.depthStencilFormat = GraphicsFormat.None;
			RenderingUtils.ReAllocateHandleIfNeeded(ref UniversalRenderer.m_RenderGraphDebugTextureHandle, in debugTexDescriptor, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, "_RenderingDebuggerTexture");
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x000363F4 File Offset: 0x000345F4
		private Rect CalculateUVRect(UniversalCameraData cameraData, float width, float height)
		{
			float normalizedSizeX = width / (float)cameraData.pixelWidth;
			float normalizedSizeY = height / (float)cameraData.pixelHeight;
			return new Rect(1f - normalizedSizeX, 1f - normalizedSizeY, normalizedSizeX, normalizedSizeY);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0003642C File Offset: 0x0003462C
		private Rect CalculateUVRect(UniversalCameraData cameraData, int textureHeightPercent)
		{
			float num = Mathf.Clamp01((float)textureHeightPercent / 100f);
			float width = num * (float)cameraData.pixelWidth;
			float height = num * (float)cameraData.pixelHeight;
			return this.CalculateUVRect(cameraData, width, height);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00036464 File Offset: 0x00034664
		private void CorrectForTextureAspectRatio(ref float width, ref float height, float sourceWidth, float sourceHeight)
		{
			if (sourceWidth != 0f && sourceHeight != 0f)
			{
				float targetWidth = height * sourceWidth / sourceHeight;
				if (targetWidth > width)
				{
					height = width * sourceHeight / sourceWidth;
					return;
				}
				width = targetWidth;
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0003649C File Offset: 0x0003469C
		private void SetupRenderGraphFinalPassDebug(RenderGraph renderGraph, ContextContainer frameData)
		{
			UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			if (base.DebugHandler != null && base.DebugHandler.IsActiveForCamera(cameraData.isPreviewCamera))
			{
				DebugFullScreenMode fullScreenDebugMode;
				int textureHeightPercent;
				if (base.DebugHandler.TryGetFullscreenDebugMode(out fullScreenDebugMode, out textureHeightPercent) && (fullScreenDebugMode != DebugFullScreenMode.ReflectionProbeAtlas || this.m_Clustering) && fullScreenDebugMode != DebugFullScreenMode.STP)
				{
					float screenWidth = (float)cameraData.pixelWidth;
					float screenHeight = (float)cameraData.pixelHeight;
					float num = Mathf.Clamp01((float)textureHeightPercent / 100f);
					float height = num * screenHeight;
					float width = num * screenWidth;
					bool supportsStereo = false;
					Vector4 dataRangeRemap = Vector4.zero;
					RenderTextureDescriptor debugDescriptor = cameraData.cameraTargetDescriptor;
					if (SystemInfo.IsFormatSupported(GraphicsFormat.R16G16B16A16_SFloat, GraphicsFormatUsage.Linear | GraphicsFormatUsage.Render))
					{
						debugDescriptor.graphicsFormat = GraphicsFormat.R16G16B16A16_SFloat;
					}
					this.CreateDebugTexture(debugDescriptor);
					ImportResourceParams importParams = default(ImportResourceParams);
					importParams.clearOnFirstUse = false;
					importParams.discardOnLastUse = false;
					TextureHandle debugTexture = renderGraph.ImportTexture(UniversalRenderer.m_RenderGraphDebugTextureHandle, importParams);
					switch (fullScreenDebugMode)
					{
					case DebugFullScreenMode.Depth:
						this.BlitToDebugTexture(renderGraph, resourceData.cameraDepthTexture, debugTexture, false);
						supportsStereo = true;
						break;
					case DebugFullScreenMode.MotionVector:
						this.BlitToDebugTexture(renderGraph, resourceData.motionVectorColor, debugTexture, true);
						supportsStereo = true;
						dataRangeRemap.x = -0.01f;
						dataRangeRemap.y = 0.01f;
						dataRangeRemap.z = 0f;
						dataRangeRemap.w = 1f;
						break;
					case DebugFullScreenMode.AdditionalLightsShadowMap:
						this.BlitToDebugTexture(renderGraph, resourceData.additionalShadowsTexture, debugTexture, false);
						break;
					case DebugFullScreenMode.MainLightShadowMap:
						this.BlitToDebugTexture(renderGraph, resourceData.mainShadowsTexture, debugTexture, false);
						break;
					case DebugFullScreenMode.AdditionalLightsCookieAtlas:
					{
						LightCookieManager lightCookieManager = this.m_LightCookieManager;
						TextureHandle textureHandle = ((lightCookieManager != null && lightCookieManager.AdditionalLightsCookieAtlasTexture != null) ? renderGraph.ImportTexture(this.m_LightCookieManager.AdditionalLightsCookieAtlasTexture) : TextureHandle.nullHandle);
						this.BlitToDebugTexture(renderGraph, textureHandle, debugTexture, false);
						break;
					}
					case DebugFullScreenMode.ReflectionProbeAtlas:
					{
						TextureHandle textureHandle2 = ((this.m_ForwardLights.reflectionProbeManager.atlasRT != null) ? renderGraph.ImportTexture(RTHandles.Alloc(this.m_ForwardLights.reflectionProbeManager.atlasRT, true)) : TextureHandle.nullHandle);
						this.BlitToDebugTexture(renderGraph, textureHandle2, debugTexture, false);
						break;
					}
					}
					RenderTexture source = null;
					switch (fullScreenDebugMode)
					{
					case DebugFullScreenMode.AdditionalLightsShadowMap:
					{
						AdditionalLightsShadowCasterPass additionalLightsShadowCasterPass = this.m_AdditionalLightsShadowCasterPass;
						RenderTexture renderTexture;
						if (additionalLightsShadowCasterPass == null)
						{
							renderTexture = null;
						}
						else
						{
							RTHandle additionalLightsShadowmapHandle = additionalLightsShadowCasterPass.m_AdditionalLightsShadowmapHandle;
							renderTexture = ((additionalLightsShadowmapHandle != null) ? additionalLightsShadowmapHandle.rt : null);
						}
						source = renderTexture;
						break;
					}
					case DebugFullScreenMode.MainLightShadowMap:
					{
						MainLightShadowCasterPass mainLightShadowCasterPass = this.m_MainLightShadowCasterPass;
						RenderTexture renderTexture2;
						if (mainLightShadowCasterPass == null)
						{
							renderTexture2 = null;
						}
						else
						{
							RTHandle mainLightShadowmapTexture = mainLightShadowCasterPass.m_MainLightShadowmapTexture;
							renderTexture2 = ((mainLightShadowmapTexture != null) ? mainLightShadowmapTexture.rt : null);
						}
						source = renderTexture2;
						break;
					}
					case DebugFullScreenMode.AdditionalLightsCookieAtlas:
					{
						LightCookieManager lightCookieManager2 = this.m_LightCookieManager;
						RenderTexture renderTexture3;
						if (lightCookieManager2 == null)
						{
							renderTexture3 = null;
						}
						else
						{
							RTHandle additionalLightsCookieAtlasTexture = lightCookieManager2.AdditionalLightsCookieAtlasTexture;
							renderTexture3 = ((additionalLightsCookieAtlasTexture != null) ? additionalLightsCookieAtlasTexture.rt : null);
						}
						source = renderTexture3;
						break;
					}
					case DebugFullScreenMode.ReflectionProbeAtlas:
					{
						ForwardLights forwardLights = this.m_ForwardLights;
						source = ((forwardLights != null) ? forwardLights.reflectionProbeManager.atlasRT : null);
						break;
					}
					}
					if (source != null)
					{
						this.CorrectForTextureAspectRatio(ref width, ref height, (float)source.width, (float)source.height);
					}
					Rect uvRect = this.CalculateUVRect(cameraData, width, height);
					base.DebugHandler.SetDebugRenderTarget(UniversalRenderer.m_RenderGraphDebugTextureHandle, uvRect, supportsStereo, dataRangeRemap);
				}
				else
				{
					base.DebugHandler.ResetDebugRenderTarget();
				}
			}
			DebugFullScreenMode fullScreenDebugMode2;
			int textureHeightPercent2;
			if (base.DebugHandler != null && !base.DebugHandler.TryGetFullscreenDebugMode(out fullScreenDebugMode2, out textureHeightPercent2))
			{
				DebugDisplayGPUResidentDrawer debugSettings = base.DebugHandler.DebugDisplaySettings.gpuResidentDrawerSettings;
				GPUResidentDrawer.RenderDebugOcclusionTestOverlay(renderGraph, debugSettings, cameraData.camera.GetInstanceID(), resourceData.activeColorTexture);
				float screenWidth2 = (float)((int)((float)cameraData.pixelHeight * cameraData.renderScale));
				float screenHeight2 = (float)((int)((float)cameraData.pixelHeight * cameraData.renderScale));
				float maxHeight = screenHeight2 * (float)textureHeightPercent2 / 100f;
				GPUResidentDrawer.RenderDebugOccluderOverlay(renderGraph, debugSettings, new Vector2(0.25f * screenWidth2, screenHeight2 - 1.5f * maxHeight), maxHeight, resourceData.activeColorTexture);
			}
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0003685C File Offset: 0x00034A5C
		private void SetupAfterPostRenderGraphFinalPassDebug(RenderGraph renderGraph, ContextContainer frameData)
		{
			UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			DebugFullScreenMode debugFullscreenMode;
			int textureHeightPercent;
			if (base.DebugHandler != null && base.DebugHandler.IsActiveForCamera(cameraData.isPreviewCamera) && base.DebugHandler.TryGetFullscreenDebugMode(out debugFullscreenMode, out textureHeightPercent) && debugFullscreenMode == DebugFullScreenMode.STP)
			{
				this.CreateDebugTexture(cameraData.cameraTargetDescriptor);
				ImportResourceParams importParams = default(ImportResourceParams);
				importParams.clearOnFirstUse = false;
				importParams.discardOnLastUse = false;
				TextureHandle debugTexture = renderGraph.ImportTexture(UniversalRenderer.m_RenderGraphDebugTextureHandle, importParams);
				this.BlitToDebugTexture(renderGraph, resourceData.stpDebugView, debugTexture, false);
				Rect uvRect = this.CalculateUVRect(cameraData, textureHeightPercent);
				Vector4 rangeRemap = Vector4.zero;
				base.DebugHandler.SetDebugRenderTarget(UniversalRenderer.m_RenderGraphDebugTextureHandle, uvRect, true, rangeRemap);
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00036914 File Offset: 0x00034B14
		private void BlitToDebugTexture(RenderGraph renderGraph, TextureHandle source, TextureHandle destination, bool isSourceTextureColor = false)
		{
			if (!source.IsValid())
			{
				this.BlitEmptyTexture(renderGraph, destination, "Copy To Debug Texture");
				return;
			}
			if (isSourceTextureColor)
			{
				renderGraph.AddCopyPass(source, destination, 0, 0, 0, 0, "Copy Pass Utility", "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/UniversalRendererDebug.cs", 251);
				return;
			}
			RenderGraphUtils.BlitMaterialParameters blitMaterialParameters = new RenderGraphUtils.BlitMaterialParameters(source, destination, this.m_DebugBlitMaterial, 0);
			renderGraph.AddBlitPass(blitMaterialParameters, "Blit Pass Utility w. Material", "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/UniversalRendererDebug.cs", 260);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00036980 File Offset: 0x00034B80
		private void BlitEmptyTexture(RenderGraph renderGraph, TextureHandle destination, string passName = "Copy To Debug Texture")
		{
			UniversalRenderer.CopyToDebugTexturePassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<UniversalRenderer.CopyToDebugTexturePassData>(passName, out passData, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/UniversalRendererDebug.cs", 271))
			{
				passData.src = renderGraph.defaultResources.blackTexture;
				passData.dest = destination;
				builder.SetRenderAttachment(destination, 0, AccessFlags.Write);
				builder.AllowPassCulling(false);
				builder.SetRenderFunc<UniversalRenderer.CopyToDebugTexturePassData>(delegate(UniversalRenderer.CopyToDebugTexturePassData data, RasterGraphContext context)
				{
					Blitter.BlitTexture(context.cmd, data.src, new Vector4(1f, 1f, 0f, 0f), 0f, false);
				});
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x00036A0C File Offset: 0x00034C0C
		private RTHandle currentRenderGraphCameraColorHandle
		{
			get
			{
				if (!UniversalRenderer.m_UseUpscaledColorHandle)
				{
					return UniversalRenderer.m_RenderGraphCameraColorHandles[UniversalRenderer.m_CurrentColorHandle];
				}
				return UniversalRenderer.m_RenderGraphUpscaledCameraColorHandles[UniversalRenderer.m_CurrentColorHandle];
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x00036A2C File Offset: 0x00034C2C
		private RTHandle nextRenderGraphCameraColorHandle
		{
			get
			{
				UniversalRenderer.m_CurrentColorHandle = (UniversalRenderer.m_CurrentColorHandle + 1) % 2;
				return this.currentRenderGraphCameraColorHandle;
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00036A44 File Offset: 0x00034C44
		private void CleanupRenderGraphResources()
		{
			RTHandle rthandle = UniversalRenderer.m_RenderGraphCameraColorHandles[0];
			if (rthandle != null)
			{
				rthandle.Release();
			}
			RTHandle rthandle2 = UniversalRenderer.m_RenderGraphCameraColorHandles[1];
			if (rthandle2 != null)
			{
				rthandle2.Release();
			}
			RTHandle rthandle3 = UniversalRenderer.m_RenderGraphUpscaledCameraColorHandles[0];
			if (rthandle3 != null)
			{
				rthandle3.Release();
			}
			RTHandle rthandle4 = UniversalRenderer.m_RenderGraphUpscaledCameraColorHandles[1];
			if (rthandle4 != null)
			{
				rthandle4.Release();
			}
			RTHandle renderGraphCameraDepthHandle = UniversalRenderer.m_RenderGraphCameraDepthHandle;
			if (renderGraphCameraDepthHandle != null)
			{
				renderGraphCameraDepthHandle.Release();
			}
			RTHandle renderGraphDebugTextureHandle = UniversalRenderer.m_RenderGraphDebugTextureHandle;
			if (renderGraphDebugTextureHandle == null)
			{
				return;
			}
			renderGraphDebugTextureHandle.Release();
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00036AB8 File Offset: 0x00034CB8
		public static TextureHandle CreateRenderGraphTexture(RenderGraph renderGraph, RenderTextureDescriptor desc, string name, bool clear, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Clamp)
		{
			TextureDesc rgDesc = new TextureDesc(desc.width, desc.height, false, false);
			rgDesc.dimension = desc.dimension;
			rgDesc.clearBuffer = clear;
			rgDesc.bindTextureMS = desc.bindMS;
			rgDesc.format = ((desc.depthStencilFormat != GraphicsFormat.None) ? desc.depthStencilFormat : desc.graphicsFormat);
			rgDesc.slices = desc.volumeDepth;
			rgDesc.msaaSamples = (MSAASamples)desc.msaaSamples;
			rgDesc.name = name;
			rgDesc.enableRandomWrite = desc.enableRandomWrite;
			rgDesc.filterMode = filterMode;
			rgDesc.wrapMode = wrapMode;
			rgDesc.isShadowMap = desc.shadowSamplingMode != ShadowSamplingMode.None && desc.depthStencilFormat > GraphicsFormat.None;
			rgDesc.vrUsage = desc.vrUsage;
			rgDesc.useDynamicScale = desc.useDynamicScale;
			rgDesc.useDynamicScaleExplicit = desc.useDynamicScaleExplicit;
			return renderGraph.CreateTexture(in rgDesc);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00036BB4 File Offset: 0x00034DB4
		internal static TextureHandle CreateRenderGraphTexture(RenderGraph renderGraph, RenderTextureDescriptor desc, string name, bool clear, Color color, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Clamp)
		{
			TextureDesc rgDesc = new TextureDesc(desc.width, desc.height, false, false);
			rgDesc.dimension = desc.dimension;
			rgDesc.clearBuffer = clear;
			rgDesc.clearColor = color;
			rgDesc.bindTextureMS = desc.bindMS;
			rgDesc.format = ((desc.depthStencilFormat != GraphicsFormat.None) ? desc.depthStencilFormat : desc.graphicsFormat);
			rgDesc.slices = desc.volumeDepth;
			rgDesc.msaaSamples = (MSAASamples)desc.msaaSamples;
			rgDesc.name = name;
			rgDesc.enableRandomWrite = desc.enableRandomWrite;
			rgDesc.filterMode = filterMode;
			rgDesc.wrapMode = wrapMode;
			rgDesc.useDynamicScale = desc.useDynamicScale;
			rgDesc.useDynamicScaleExplicit = desc.useDynamicScaleExplicit;
			return renderGraph.CreateTexture(in rgDesc);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00036C8D File Offset: 0x00034E8D
		private bool ShouldApplyPostProcessing(bool postProcessEnabled)
		{
			return postProcessEnabled && this.m_PostProcessPasses.isCreated;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00036C9F File Offset: 0x00034E9F
		private bool CameraHasPostProcessingWithDepth(UniversalCameraData cameraData)
		{
			return this.ShouldApplyPostProcessing(cameraData.postProcessEnabled) && cameraData.postProcessingRequiresDepthTexture;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00036CB8 File Offset: 0x00034EB8
		private void RequiresColorAndDepthAttachments(RenderGraph renderGraph, out bool createColorTexture, out bool createDepthTexture, UniversalCameraData cameraData, ref UniversalRenderer.RenderPassInputSummary renderPassInputs)
		{
			bool isPreviewCamera = cameraData.isPreviewCamera;
			bool requiresDepthPrepass = this.RequireDepthPrepass(cameraData, ref renderPassInputs);
			bool flag = ((this.HasActiveRenderFeatures() && this.m_IntermediateTextureMode == IntermediateTextureMode.Always) | this.HasPassesRequiringIntermediateTexture() | (Application.isEditor && this.m_Clustering) | this.RequiresIntermediateColorTexture(cameraData, ref renderPassInputs)) & !isPreviewCamera;
			bool requireDepthTexture = this.RequireDepthTexture(cameraData, requiresDepthPrepass, ref renderPassInputs);
			base.useDepthPriming = this.IsDepthPrimingEnabled(cameraData);
			bool intermediateRenderTexture = flag || requireDepthTexture;
			createDepthTexture = intermediateRenderTexture;
			createColorTexture = intermediateRenderTexture;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00036D38 File Offset: 0x00034F38
		private void UpdateCameraHistory(UniversalCameraData cameraData)
		{
			if (cameraData != null && cameraData.historyManager != null)
			{
				bool flag = cameraData.xr.enabled && !cameraData.xr.singlePassEnabled;
				int multipassId = cameraData.xr.multipassId;
				if (!flag || multipassId == 0)
				{
					UniversalCameraHistory historyManager = cameraData.historyManager;
					historyManager.GatherHistoryRequests();
					historyManager.ReleaseUnusedHistory();
					historyManager.SwapAndSetReferenceSize(cameraData.cameraTargetDescriptor.width, cameraData.cameraTargetDescriptor.height);
				}
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00036DB4 File Offset: 0x00034FB4
		private void CreateRenderGraphCameraRenderTargets(RenderGraph renderGraph, bool isCameraTargetOffscreenDepth)
		{
			UniversalResourceData resourceData = base.frameData.Get<UniversalResourceData>();
			UniversalCameraData cameraData = base.frameData.Get<UniversalCameraData>();
			UniversalPostProcessingData postProcessingData = base.frameData.Get<UniversalPostProcessingData>();
			bool lastCameraInTheStack = cameraData.resolveFinalTarget;
			bool isBuiltInTexture = cameraData.targetTexture == null;
			RenderTargetIdentifier targetColorId = ((cameraData.targetTexture != null) ? new RenderTargetIdentifier(cameraData.targetTexture) : BuiltinRenderTextureType.CameraTarget);
			RenderTargetIdentifier targetDepthId = ((cameraData.targetTexture != null) ? new RenderTargetIdentifier(cameraData.targetTexture) : BuiltinRenderTextureType.Depth);
			bool clearColor = cameraData.renderType == CameraRenderType.Base;
			bool clearDepth = cameraData.renderType == CameraRenderType.Base || cameraData.clearDepth;
			Color cameraBackgroundColor = ((cameraData.camera.clearFlags == CameraClearFlags.Nothing && cameraData.targetTexture == null) ? Color.yellow : cameraData.backgroundColor);
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
				if (base.DebugHandler != null && base.DebugHandler.IsActiveForCamera(cameraData.isPreviewCamera))
				{
					base.DebugHandler.TryGetScreenClearColor(ref cameraBackgroundColor);
				}
			}
			ImportResourceParams importColorParams = default(ImportResourceParams);
			importColorParams.clearOnFirstUse = clearColor;
			importColorParams.clearColor = cameraBackgroundColor;
			importColorParams.discardOnLastUse = false;
			ImportResourceParams importDepthParams = default(ImportResourceParams);
			importDepthParams.clearOnFirstUse = clearDepth;
			importDepthParams.clearColor = cameraBackgroundColor;
			importDepthParams.discardOnLastUse = false;
			if (cameraData.xr.enabled)
			{
				targetColorId = cameraData.xr.renderTarget;
				targetDepthId = cameraData.xr.renderTarget;
				isBuiltInTexture = false;
			}
			if (this.m_TargetColorHandle == null)
			{
				this.m_TargetColorHandle = RTHandles.Alloc(targetColorId, "Backbuffer color");
			}
			else if (this.m_TargetColorHandle.nameID != targetColorId)
			{
				RTHandleStaticHelpers.SetRTHandleUserManagedWrapper(ref this.m_TargetColorHandle, targetColorId);
			}
			if (this.m_TargetDepthHandle == null)
			{
				this.m_TargetDepthHandle = RTHandles.Alloc(targetDepthId, "Backbuffer depth");
			}
			else if (this.m_TargetDepthHandle.nameID != targetDepthId)
			{
				RTHandleStaticHelpers.SetRTHandleUserManagedWrapper(ref this.m_TargetDepthHandle, targetDepthId);
			}
			this.UpdateCameraHistory(cameraData);
			UniversalRenderer.RenderPassInputSummary renderPassInputs = this.GetRenderPassInputs(cameraData.IsTemporalAAEnabled(), postProcessingData.isEnabled);
			if (this.m_RenderingLayerProvidesByDepthNormalPass)
			{
				renderPassInputs.requiresNormalsTexture = true;
			}
			if (cameraData.renderType == CameraRenderType.Base)
			{
				this.RequiresColorAndDepthAttachments(renderGraph, out UniversalRenderer.m_CreateColorAttachment, out UniversalRenderer.m_CreateDepthAttachment, cameraData, ref renderPassInputs);
			}
			bool clearBackbufferOnFirstUse = cameraData.renderType == CameraRenderType.Base && !UniversalRenderer.m_CreateColorAttachment;
			clearBackbufferOnFirstUse = clearBackbufferOnFirstUse || isCameraTargetOffscreenDepth;
			bool isNativeUIOverlayRenderingAfterURP = !SupportedRenderingFeatures.active.rendersUIOverlay && cameraData.resolveToScreen;
			bool isNativeRenderingAfterURP = Watermark.IsVisible() || isNativeUIOverlayRenderingAfterURP;
			bool noStoreOnlyResolveBBColor = !UniversalRenderer.m_CreateColorAttachment && !isNativeRenderingAfterURP && cameraData.cameraTargetDescriptor.msaaSamples > 1;
			ImportResourceParams importBackbufferColorParams = default(ImportResourceParams);
			importBackbufferColorParams.clearOnFirstUse = clearBackbufferOnFirstUse;
			importBackbufferColorParams.clearColor = cameraBackgroundColor;
			importBackbufferColorParams.discardOnLastUse = noStoreOnlyResolveBBColor;
			ImportResourceParams importBackbufferDepthParams = default(ImportResourceParams);
			importBackbufferDepthParams.clearOnFirstUse = clearBackbufferOnFirstUse;
			importBackbufferDepthParams.clearColor = cameraBackgroundColor;
			importBackbufferDepthParams.discardOnLastUse = !isCameraTargetOffscreenDepth;
			if (cameraData.xr.enabled && cameraData.xr.copyDepth)
			{
				importBackbufferDepthParams.discardOnLastUse = false;
			}
			RenderTargetInfo importInfo = default(RenderTargetInfo);
			RenderTargetInfo importInfoDepth = default(RenderTargetInfo);
			if (isBuiltInTexture)
			{
				int numSamples = base.AdjustAndGetScreenMSAASamples(renderGraph, UniversalRenderer.m_CreateColorAttachment);
				importInfo.width = Screen.width;
				importInfo.height = Screen.height;
				importInfo.volumeDepth = 1;
				importInfo.msaaSamples = numSamples;
				importInfo.format = cameraData.cameraTargetDescriptor.graphicsFormat;
				importInfoDepth = importInfo;
				importInfoDepth.format = cameraData.cameraTargetDescriptor.depthStencilFormat;
			}
			else
			{
				if (cameraData.xr.enabled)
				{
					importInfo.width = cameraData.xr.renderTargetDesc.width;
					importInfo.height = cameraData.xr.renderTargetDesc.height;
					importInfo.volumeDepth = cameraData.xr.renderTargetDesc.volumeDepth;
					importInfo.msaaSamples = cameraData.xr.renderTargetDesc.msaaSamples;
					importInfo.format = cameraData.xr.renderTargetDesc.graphicsFormat;
					importInfoDepth = importInfo;
					importInfoDepth.format = cameraData.xr.renderTargetDesc.depthStencilFormat;
				}
				else
				{
					importInfo.width = cameraData.targetTexture.width;
					importInfo.height = cameraData.targetTexture.height;
					importInfo.volumeDepth = cameraData.targetTexture.volumeDepth;
					importInfo.msaaSamples = cameraData.targetTexture.antiAliasing;
					importInfo.format = cameraData.targetTexture.graphicsFormat;
					importInfoDepth = importInfo;
					importInfoDepth.format = cameraData.targetTexture.depthStencilFormat;
				}
				if (importInfoDepth.format == GraphicsFormat.None)
				{
					importInfoDepth.format = SystemInfo.GetGraphicsFormat(DefaultFormat.DepthStencil);
					Debug.LogWarning("In the render graph API, the output Render Texture must have a depth buffer. When you select a Render Texture in any camera's Output Texture property, the Depth Stencil Format property of the texture must be set to a value other than None.");
				}
			}
			if (!isCameraTargetOffscreenDepth)
			{
				resourceData.backBufferColor = renderGraph.ImportTexture(this.m_TargetColorHandle, importInfo, importBackbufferColorParams);
			}
			resourceData.backBufferDepth = renderGraph.ImportTexture(this.m_TargetDepthHandle, importInfoDepth, importBackbufferDepthParams);
			if (UniversalRenderer.m_CreateColorAttachment && !isCameraTargetOffscreenDepth)
			{
				RenderTextureDescriptor cameraTargetDescriptor = cameraData.cameraTargetDescriptor;
				cameraTargetDescriptor.useMipMap = false;
				cameraTargetDescriptor.autoGenerateMips = false;
				cameraTargetDescriptor.depthStencilFormat = GraphicsFormat.None;
				RenderingUtils.ReAllocateHandleIfNeeded(ref UniversalRenderer.m_RenderGraphCameraColorHandles[0], in cameraTargetDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_CameraTargetAttachmentA");
				RenderingUtils.ReAllocateHandleIfNeeded(ref UniversalRenderer.m_RenderGraphCameraColorHandles[1], in cameraTargetDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_CameraTargetAttachmentB");
				if (cameraData.renderType == CameraRenderType.Base)
				{
					UniversalRenderer.m_CurrentColorHandle = 0;
					UniversalRenderer.m_UseUpscaledColorHandle = false;
				}
				importColorParams.discardOnLastUse = lastCameraInTheStack;
				resourceData.cameraColor = renderGraph.ImportTexture(this.currentRenderGraphCameraColorHandle, importColorParams);
				resourceData.activeColorID = UniversalResourceDataBase.ActiveID.Camera;
				if (cameraData.IsSTPEnabled())
				{
					RenderTextureDescriptor upscaledTargetDesc = cameraTargetDescriptor;
					upscaledTargetDesc.width = cameraData.pixelWidth;
					upscaledTargetDesc.height = cameraData.pixelHeight;
					RenderingUtils.ReAllocateHandleIfNeeded(ref UniversalRenderer.m_RenderGraphUpscaledCameraColorHandles[0], in upscaledTargetDesc, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, "_CameraUpscaledTargetAttachmentA");
					RenderingUtils.ReAllocateHandleIfNeeded(ref UniversalRenderer.m_RenderGraphUpscaledCameraColorHandles[1], in upscaledTargetDesc, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, "_CameraUpscaledTargetAttachmentB");
				}
			}
			else
			{
				resourceData.activeColorID = UniversalResourceDataBase.ActiveID.BackBuffer;
			}
			bool depthTextureIsDepthFormat = this.RequireDepthPrepass(cameraData, ref renderPassInputs) && this.renderingModeActual != RenderingMode.Deferred;
			if (UniversalRenderer.m_CreateDepthAttachment)
			{
				RenderTextureDescriptor depthDescriptor = cameraData.cameraTargetDescriptor;
				depthDescriptor.useMipMap = false;
				depthDescriptor.autoGenerateMips = false;
				bool hasMSAA = depthDescriptor.msaaSamples > 1;
				bool resolveDepth = RenderingUtils.MultisampleDepthResolveSupported() && renderGraph.nativeRenderPassesEnabled;
				depthDescriptor.bindMS = !resolveDepth && hasMSAA;
				if (this.IsGLESDevice())
				{
					depthDescriptor.bindMS = false;
				}
				depthDescriptor.graphicsFormat = GraphicsFormat.None;
				depthDescriptor.depthStencilFormat = this.cameraDepthAttachmentFormat;
				RenderingUtils.ReAllocateHandleIfNeeded(ref UniversalRenderer.m_RenderGraphCameraDepthHandle, in depthDescriptor, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, "_CameraDepthAttachment");
				importDepthParams.discardOnLastUse = lastCameraInTheStack;
				resourceData.cameraDepth = renderGraph.ImportTexture(UniversalRenderer.m_RenderGraphCameraDepthHandle, importDepthParams);
				resourceData.activeDepthID = UniversalResourceDataBase.ActiveID.Camera;
				this.m_CopyDepthPass.MssaSamples = depthDescriptor.msaaSamples;
				this.m_CopyDepthPass.CopyToDepth = depthTextureIsDepthFormat;
				this.m_CopyDepthPass.m_CopyResolvedDepth = !depthDescriptor.bindMS;
			}
			else
			{
				resourceData.activeDepthID = UniversalResourceDataBase.ActiveID.BackBuffer;
			}
			this.CreateCameraDepthCopyTexture(renderGraph, cameraData.cameraTargetDescriptor, depthTextureIsDepthFormat);
			this.CreateCameraNormalsTexture(renderGraph, cameraData.cameraTargetDescriptor);
			this.CreateMotionVectorTextures(renderGraph, cameraData.cameraTargetDescriptor);
			this.CreateRenderingLayersTexture(renderGraph, cameraData.cameraTargetDescriptor);
			if (!isCameraTargetOffscreenDepth)
			{
				this.CreateAfterPostProcessTexture(renderGraph, cameraData.cameraTargetDescriptor);
			}
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00037548 File Offset: 0x00035748
		private void SetupRenderingLayers(int msaaSamples)
		{
			this.m_RequiresRenderingLayer = RenderingLayerUtils.RequireRenderingLayers(this, base.rendererFeatures, msaaSamples, out this.m_RenderingLayersEvent, out this.m_RenderingLayersMaskSize);
			this.m_RenderingLayerProvidesRenderObjectPass = this.m_RequiresRenderingLayer && this.m_RenderingLayersEvent == RenderingLayerUtils.Event.Opaque;
			this.m_RenderingLayerProvidesByDepthNormalPass = this.m_RequiresRenderingLayer && this.m_RenderingLayersEvent == RenderingLayerUtils.Event.DepthNormalPrePass;
			if (this.m_DeferredLights != null)
			{
				this.m_DeferredLights.RenderingLayerMaskSize = this.m_RenderingLayersMaskSize;
				this.m_DeferredLights.UseDecalLayers = this.m_RequiresRenderingLayer;
			}
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000375D2 File Offset: 0x000357D2
		internal void SetupRenderGraphLights(RenderGraph renderGraph, UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData)
		{
			this.m_ForwardLights.SetupRenderGraphLights(renderGraph, renderingData, cameraData, lightData);
			if (this.renderingModeActual == RenderingMode.Deferred)
			{
				this.m_DeferredLights.UseFramebufferFetch = renderGraph.nativeRenderPassesEnabled;
				this.m_DeferredLights.SetupRenderGraphLights(renderGraph, cameraData, lightData);
			}
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00037610 File Offset: 0x00035810
		private void RenderRawColorDepthHistory(RenderGraph renderGraph, UniversalCameraData cameraData, UniversalResourceData resourceData)
		{
			if (cameraData != null && cameraData.historyManager != null && resourceData != null)
			{
				UniversalCameraHistory history = cameraData.historyManager;
				bool xrMultipassEnabled = cameraData.xr.enabled && !cameraData.xr.singlePassEnabled;
				int multipassId = cameraData.xr.multipassId;
				if (history.IsAccessRequested<RawColorHistory>())
				{
					TextureHandle textureHandle = resourceData.cameraColor;
					if (textureHandle.IsValid())
					{
						RawColorHistory colorHistory = history.GetHistoryForWrite<RawColorHistory>();
						if (colorHistory != null)
						{
							colorHistory.Update(ref cameraData.cameraTargetDescriptor, xrMultipassEnabled);
							if (colorHistory.GetCurrentTexture(multipassId) != null)
							{
								TextureHandle colorHistoryTarget = renderGraph.ImportTexture(colorHistory.GetCurrentTexture(multipassId));
								CopyColorPass historyRawColorCopyPass = this.m_HistoryRawColorCopyPass;
								ContextContainer frameData = base.frameData;
								textureHandle = resourceData.cameraColor;
								historyRawColorCopyPass.RenderToExistingTexture(renderGraph, frameData, in colorHistoryTarget, in textureHandle, Downsampling.None);
							}
						}
					}
				}
				if (history.IsAccessRequested<RawDepthHistory>())
				{
					TextureHandle textureHandle = resourceData.cameraDepth;
					if (textureHandle.IsValid())
					{
						RawDepthHistory depthHistory = history.GetHistoryForWrite<RawDepthHistory>();
						if (depthHistory != null)
						{
							if (!this.m_HistoryRawDepthCopyPass.CopyToDepth)
							{
								RenderTextureDescriptor tempColorDepthDesc = cameraData.cameraTargetDescriptor;
								tempColorDepthDesc.graphicsFormat = GraphicsFormat.R32_SFloat;
								tempColorDepthDesc.depthStencilFormat = GraphicsFormat.None;
								depthHistory.Update(ref tempColorDepthDesc, xrMultipassEnabled);
							}
							else
							{
								RenderTextureDescriptor tempColorDepthDesc2 = cameraData.cameraTargetDescriptor;
								tempColorDepthDesc2.graphicsFormat = GraphicsFormat.None;
								depthHistory.Update(ref tempColorDepthDesc2, xrMultipassEnabled);
							}
							if (depthHistory.GetCurrentTexture(multipassId) != null)
							{
								TextureHandle depthHistoryTarget = renderGraph.ImportTexture(depthHistory.GetCurrentTexture(multipassId));
								this.m_HistoryRawDepthCopyPass.Render(renderGraph, base.frameData, depthHistoryTarget, resourceData.cameraDepth, false, "Copy Depth");
							}
						}
					}
				}
			}
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0003778A File Offset: 0x0003598A
		public override void OnBeginRenderGraphFrame()
		{
			base.frameData.Get<UniversalResourceData>().InitFrame();
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0003779C File Offset: 0x0003599C
		internal override void OnRecordRenderGraph(RenderGraph renderGraph, ScriptableRenderContext context)
		{
			UniversalResourceData resourceData = base.frameData.Get<UniversalResourceData>();
			UniversalRenderingData renderingData = base.frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = base.frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = base.frameData.Get<UniversalLightData>();
			this.useRenderPassEnabled = renderGraph.nativeRenderPassesEnabled;
			MotionVectorRenderPass.SetRenderGraphMotionVectorGlobalMatrices(renderGraph, cameraData);
			this.SetupRenderGraphLights(renderGraph, renderingData, cameraData, lightData);
			this.SetupRenderingLayers(cameraData.cameraTargetDescriptor.msaaSamples);
			bool isCameraTargetOffscreenDepth = cameraData.camera.targetTexture != null && cameraData.camera.targetTexture.format == RenderTextureFormat.Depth;
			this.CreateRenderGraphCameraRenderTargets(renderGraph, isCameraTargetOffscreenDepth);
			DebugHandler debugHandler = base.DebugHandler;
			base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.BeforeRendering);
			base.SetupRenderGraphCameraProperties(renderGraph, resourceData.isActiveTargetBackBuffer);
			cameraData.renderer.useDepthPriming = base.useDepthPriming;
			if (isCameraTargetOffscreenDepth)
			{
				this.OnOffscreenDepthTextureRendering(renderGraph, context, resourceData, cameraData);
				return;
			}
			this.OnBeforeRendering(renderGraph);
			base.BeginRenderGraphXRRendering(renderGraph);
			this.OnMainRendering(renderGraph, context);
			this.OnAfterRendering(renderGraph);
			base.EndRenderGraphXRRendering(renderGraph);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0003789B File Offset: 0x00035A9B
		public override void OnEndRenderGraphFrame()
		{
			base.frameData.Get<UniversalResourceData>().EndFrame();
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000378AD File Offset: 0x00035AAD
		internal override void OnFinishRenderGraphRendering(CommandBuffer cmd)
		{
			if (this.renderingModeActual == RenderingMode.Deferred)
			{
				this.m_DeferredPass.OnCameraCleanup(cmd);
			}
			this.m_CopyDepthPass.OnCameraCleanup(cmd);
			this.m_DepthNormalPrepass.OnCameraCleanup(cmd);
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x000378DC File Offset: 0x00035ADC
		public override bool supportsGPUOcclusion
		{
			get
			{
				bool isGpuSupported = SystemInfo.graphicsDeviceVendorID != 20803;
				if (!isGpuSupported && !this.m_IssuedGPUOcclusionUnsupportedMsg)
				{
					Debug.LogWarning("The GPU Occlusion Culling feature is currently unavailable on this device due to suspected driver issues.");
					this.m_IssuedGPUOcclusionUnsupportedMsg = true;
				}
				return this.m_RenderingMode != RenderingMode.Deferred && isGpuSupported;
			}
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00037924 File Offset: 0x00035B24
		private void OnOffscreenDepthTextureRendering(RenderGraph renderGraph, ScriptableRenderContext context, UniversalResourceData resourceData, UniversalCameraData cameraData)
		{
			if (!renderGraph.nativeRenderPassesEnabled)
			{
				ClearTargetsPass.Render(renderGraph, resourceData.activeColorTexture, resourceData.backBufferDepth, RTClearFlags.Depth, cameraData.backgroundColor);
			}
			base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.BeforeRenderingShadows, RenderPassEvent.BeforeRenderingOpaques);
			this.m_RenderOpaqueForwardPass.Render(renderGraph, base.frameData, TextureHandle.nullHandle, resourceData.backBufferDepth, TextureHandle.nullHandle, TextureHandle.nullHandle, uint.MaxValue);
			base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingOpaques, RenderPassEvent.BeforeRenderingTransparents);
			this.m_RenderTransparentForwardPass.Render(renderGraph, base.frameData, TextureHandle.nullHandle, resourceData.backBufferDepth, TextureHandle.nullHandle, TextureHandle.nullHandle, uint.MaxValue);
			base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingTransparents, RenderPassEvent.AfterRendering);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000379D4 File Offset: 0x00035BD4
		private void OnBeforeRendering(RenderGraph renderGraph)
		{
			UniversalResourceData resourceData = base.frameData.Get<UniversalResourceData>();
			UniversalRenderingData renderingData = base.frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = base.frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = base.frameData.Get<UniversalLightData>();
			UniversalShadowData shadowData = base.frameData.Get<UniversalShadowData>();
			this.m_ForwardLights.PreSetup(renderingData, cameraData, lightData);
			base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.BeforeRenderingShadows);
			bool renderShadows = false;
			if (this.m_MainLightShadowCasterPass.Setup(renderingData, cameraData, lightData, shadowData))
			{
				renderShadows = true;
				resourceData.mainShadowsTexture = this.m_MainLightShadowCasterPass.Render(renderGraph, base.frameData);
			}
			if (this.m_AdditionalLightsShadowCasterPass.Setup(renderingData, cameraData, lightData, shadowData))
			{
				renderShadows = true;
				resourceData.additionalShadowsTexture = this.m_AdditionalLightsShadowCasterPass.Render(renderGraph, base.frameData);
			}
			if (renderShadows)
			{
				base.SetupRenderGraphCameraProperties(renderGraph, resourceData.isActiveTargetBackBuffer);
			}
			base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingShadows);
			if (cameraData.postProcessEnabled && this.m_PostProcessPasses.isCreated)
			{
				TextureHandle internalColorLut;
				this.m_PostProcessPasses.colorGradingLutPass.Render(renderGraph, base.frameData, out internalColorLut);
				resourceData.internalColorLut = internalColorLut;
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00037AE8 File Offset: 0x00035CE8
		private unsafe void UpdateInstanceOccluders(RenderGraph renderGraph, UniversalCameraData cameraData, TextureHandle depthTexture)
		{
			int scaledWidth = (int)((float)cameraData.pixelWidth * cameraData.renderScale);
			int scaledHeight = (int)((float)cameraData.pixelHeight * cameraData.renderScale);
			bool isSinglePassXR = cameraData.xr.enabled && cameraData.xr.singlePassEnabled;
			OccluderParameters occluderParams = new OccluderParameters(cameraData.camera.GetInstanceID())
			{
				subviewCount = (isSinglePassXR ? 2 : 1),
				depthTexture = depthTexture,
				depthSize = new Vector2Int(scaledWidth, scaledHeight),
				depthIsArray = isSinglePassXR
			};
			int subviewCount = occluderParams.subviewCount;
			Span<OccluderSubviewUpdate> occluderSubviewUpdates;
			checked
			{
				occluderSubviewUpdates = new Span<OccluderSubviewUpdate>(stackalloc byte[unchecked((UIntPtr)subviewCount) * (UIntPtr)sizeof(OccluderSubviewUpdate)], subviewCount);
			}
			for (int subviewIndex = 0; subviewIndex < occluderParams.subviewCount; subviewIndex++)
			{
				Matrix4x4 viewMatrix = cameraData.GetViewMatrix(subviewIndex);
				Matrix4x4 projMatrix = cameraData.GetProjectionMatrix(subviewIndex);
				*occluderSubviewUpdates[subviewIndex] = new OccluderSubviewUpdate(subviewIndex)
				{
					depthSliceIndex = subviewIndex,
					viewMatrix = viewMatrix,
					invViewMatrix = viewMatrix.inverse,
					gpuProjMatrix = GL.GetGPUProjectionMatrix(projMatrix, true),
					viewOffsetWorldSpace = Vector3.zero
				};
			}
			GPUResidentDrawer.UpdateInstanceOccluders(renderGraph, in occluderParams, occluderSubviewUpdates);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00037C1C File Offset: 0x00035E1C
		private unsafe void InstanceOcclusionTest(RenderGraph renderGraph, UniversalCameraData cameraData, OcclusionTest occlusionTest)
		{
			bool isSinglePassXR = cameraData.xr.enabled && cameraData.xr.singlePassEnabled;
			int subviewCount = (isSinglePassXR ? 2 : 1);
			OcclusionCullingSettings settings = new OcclusionCullingSettings(cameraData.camera.GetInstanceID(), occlusionTest)
			{
				instanceMultiplier = ((isSinglePassXR && !SystemInfo.supportsMultiview) ? 2 : 1)
			};
			int num = subviewCount;
			Span<SubviewOcclusionTest> subviewOcclusionTests;
			checked
			{
				subviewOcclusionTests = new Span<SubviewOcclusionTest>(stackalloc byte[unchecked((UIntPtr)num) * (UIntPtr)sizeof(SubviewOcclusionTest)], num);
			}
			for (int subviewIndex = 0; subviewIndex < subviewCount; subviewIndex++)
			{
				*subviewOcclusionTests[subviewIndex] = new SubviewOcclusionTest
				{
					cullingSplitIndex = 0,
					occluderSubviewIndex = subviewIndex
				};
			}
			GPUResidentDrawer.InstanceOcclusionTest(renderGraph, in settings, subviewOcclusionTests);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00037CD8 File Offset: 0x00035ED8
		private void RecordCustomPassesWithDepthCopyAndMotion(RenderGraph renderGraph, UniversalResourceData resourceData, RenderPassEvent earliestDepthReadEvent, RenderPassEvent currentEvent, bool renderMotionVectors)
		{
			RenderPassEvent startEvent;
			RenderPassEvent splitEvent;
			RenderPassEvent endEvent;
			base.CalculateSplitEventRange(currentEvent, earliestDepthReadEvent, out startEvent, out splitEvent, out endEvent);
			base.RecordCustomRenderGraphPassesInEventRange(renderGraph, startEvent, splitEvent);
			this.ExecuteScheduledDepthCopyWithMotion(renderGraph, resourceData, renderMotionVectors);
			base.RecordCustomRenderGraphPassesInEventRange(renderGraph, splitEvent, endEvent);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00037D10 File Offset: 0x00035F10
		private bool AllowPartialDepthNormalsPrepass(bool isDeferred, RenderPassEvent requiresDepthNormalEvent)
		{
			return isDeferred && RenderPassEvent.AfterRenderingGbuffer <= requiresDepthNormalEvent && requiresDepthNormalEvent <= RenderPassEvent.BeforeRenderingOpaques;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00037D2C File Offset: 0x00035F2C
		private UniversalRenderer.DepthCopySchedule CalculateDepthCopySchedule(RenderPassEvent earliestDepthReadEvent, bool hasFullPrepass)
		{
			UniversalRenderer.DepthCopySchedule schedule;
			if (earliestDepthReadEvent < RenderPassEvent.AfterRenderingOpaques || this.m_CopyDepthMode == CopyDepthMode.ForcePrepass)
			{
				if (hasFullPrepass)
				{
					schedule = UniversalRenderer.DepthCopySchedule.AfterPrepass;
				}
				else
				{
					schedule = UniversalRenderer.DepthCopySchedule.AfterGBuffer;
				}
			}
			else if (earliestDepthReadEvent < RenderPassEvent.AfterRenderingTransparents || this.m_CopyDepthMode == CopyDepthMode.AfterOpaques)
			{
				if (earliestDepthReadEvent < RenderPassEvent.AfterRenderingSkybox)
				{
					schedule = UniversalRenderer.DepthCopySchedule.AfterOpaques;
				}
				else
				{
					schedule = UniversalRenderer.DepthCopySchedule.AfterSkybox;
				}
			}
			else if (earliestDepthReadEvent < RenderPassEvent.BeforeRenderingPostProcessing || this.m_CopyDepthMode == CopyDepthMode.AfterTransparents)
			{
				schedule = UniversalRenderer.DepthCopySchedule.AfterTransparents;
			}
			else
			{
				schedule = UniversalRenderer.DepthCopySchedule.None;
			}
			return schedule;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00037D90 File Offset: 0x00035F90
		private UniversalRenderer.TextureCopySchedules CalculateTextureCopySchedules(UniversalCameraData cameraData, UniversalRenderer.RenderPassInputSummary renderPassInputs, bool isDeferred, bool requiresDepthPrepass, bool hasFullPrepass)
		{
			bool cameraHasPostProcessingWithDepth = this.CameraHasPostProcessingWithDepth(cameraData);
			bool flag = cameraData.requiresDepthTexture || cameraHasPostProcessingWithDepth || renderPassInputs.requiresDepthTexture || this.DebugHandlerRequireDepthPass(base.frameData.Get<UniversalCameraData>());
			UniversalRenderer.DepthCopySchedule depth = UniversalRenderer.DepthCopySchedule.None;
			if (flag)
			{
				depth = ((isDeferred || !requiresDepthPrepass || base.useDepthPriming) ? this.CalculateDepthCopySchedule(renderPassInputs.requiresDepthTextureEarliestEvent, hasFullPrepass) : UniversalRenderer.DepthCopySchedule.DuringPrepass);
			}
			UniversalRenderer.ColorCopySchedule color = (((cameraData.requiresOpaqueTexture || renderPassInputs.requiresColorTexture) & !cameraData.isPreviewCamera) ? UniversalRenderer.ColorCopySchedule.AfterSkybox : UniversalRenderer.ColorCopySchedule.None);
			UniversalRenderer.TextureCopySchedules schedules;
			schedules.depth = depth;
			schedules.color = color;
			return schedules;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00037E29 File Offset: 0x00036029
		private void CopyDepthToDepthTexture(RenderGraph renderGraph, UniversalResourceData resourceData)
		{
			this.m_CopyDepthPass.Render(renderGraph, base.frameData, resourceData.cameraDepthTexture, resourceData.activeDepthTexture, true, "Copy Depth");
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00037E4F File Offset: 0x0003604F
		private void RenderMotionVectors(RenderGraph renderGraph, UniversalResourceData resourceData)
		{
			this.m_MotionVectorPass.Render(renderGraph, base.frameData, resourceData.cameraDepthTexture, resourceData.motionVectorColor, resourceData.motionVectorDepth);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00037E75 File Offset: 0x00036075
		private void ExecuteScheduledDepthCopyWithMotion(RenderGraph renderGraph, UniversalResourceData resourceData, bool renderMotionVectors)
		{
			this.CopyDepthToDepthTexture(renderGraph, resourceData);
			if (renderMotionVectors)
			{
				this.RenderMotionVectors(renderGraph, resourceData);
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00037E8C File Offset: 0x0003608C
		private void OnMainRendering(RenderGraph renderGraph, ScriptableRenderContext context)
		{
			UniversalResourceData resourceData = base.frameData.Get<UniversalResourceData>();
			UniversalCameraData cameraData = base.frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = base.frameData.Get<UniversalLightData>();
			UniversalPostProcessingData postProcessingData = base.frameData.Get<UniversalPostProcessingData>();
			if (!renderGraph.nativeRenderPassesEnabled)
			{
				RTClearFlags clearFlags = (RTClearFlags)ScriptableRenderer.GetCameraClearFlag(cameraData);
				if (clearFlags != RTClearFlags.None)
				{
					ClearTargetsPass.Render(renderGraph, resourceData.activeColorTexture, resourceData.activeDepthTexture, clearFlags, cameraData.backgroundColor);
				}
			}
			base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.BeforeRenderingPrePasses);
			UniversalRenderer.RenderPassInputSummary renderPassInputs = this.GetRenderPassInputs(cameraData.IsTemporalAAEnabled(), postProcessingData.isEnabled);
			if (this.m_RenderingLayerProvidesByDepthNormalPass)
			{
				renderPassInputs.requiresNormalsTexture = true;
			}
			bool isDeferred = this.renderingModeActual == RenderingMode.Deferred;
			bool requiresDepthPrepass = this.RequireDepthPrepass(cameraData, ref renderPassInputs);
			bool flag = requiresDepthPrepass && !renderPassInputs.requiresNormalsTexture;
			bool isDepthNormalPrepass = requiresDepthPrepass && renderPassInputs.requiresNormalsTexture;
			bool hasFullPrepass = flag || (isDepthNormalPrepass && !this.AllowPartialDepthNormalsPrepass(isDeferred, renderPassInputs.requiresDepthNormalAtEvent));
			UniversalRenderer.TextureCopySchedules copySchedules = this.CalculateTextureCopySchedules(cameraData, renderPassInputs, isDeferred, requiresDepthPrepass, hasFullPrepass);
			bool needsOccluderUpdate = cameraData.useGPUOcclusionCulling;
			if (cameraData.xr.enabled && cameraData.xr.hasMotionVectorPass)
			{
				XRDepthMotionPass xrdepthMotionPass = this.m_XRDepthMotionPass;
				if (xrdepthMotionPass != null)
				{
					xrdepthMotionPass.Update(ref cameraData);
				}
				XRDepthMotionPass xrdepthMotionPass2 = this.m_XRDepthMotionPass;
				if (xrdepthMotionPass2 != null)
				{
					xrdepthMotionPass2.Render(renderGraph, base.frameData);
				}
			}
			if (requiresDepthPrepass)
			{
				bool isDepthPrimingTarget = base.useDepthPriming && (cameraData.renderType == CameraRenderType.Base || cameraData.clearDepth);
				bool renderToAttachment = isDeferred || isDepthPrimingTarget;
				TextureHandle depthTarget = (renderToAttachment ? resourceData.activeDepthTexture : resourceData.cameraDepthTexture);
				int passCount = (needsOccluderUpdate ? 2 : 1);
				for (int passIndex = 0; passIndex < passCount; passIndex++)
				{
					uint batchLayerMask = uint.MaxValue;
					if (needsOccluderUpdate)
					{
						OcclusionTest occlusionTest = ((passIndex == 0) ? OcclusionTest.TestAll : OcclusionTest.TestCulled);
						this.InstanceOcclusionTest(renderGraph, cameraData, occlusionTest);
						batchLayerMask = occlusionTest.GetBatchLayerMask();
					}
					bool flag2 = passIndex == passCount - 1;
					bool setGlobalDepth = flag2 && !renderToAttachment;
					bool setGlobalTextures = flag2 && (!isDeferred || hasFullPrepass);
					if (isDepthNormalPrepass)
					{
						this.DepthNormalPrepassRender(renderGraph, renderPassInputs, depthTarget, batchLayerMask, setGlobalDepth, setGlobalTextures);
					}
					else
					{
						this.m_DepthPrepass.Render(renderGraph, base.frameData, ref depthTarget, batchLayerMask, setGlobalDepth);
					}
					if (needsOccluderUpdate)
					{
						this.UpdateInstanceOccluders(renderGraph, cameraData, depthTarget);
						if (passIndex != 0)
						{
							this.InstanceOcclusionTest(renderGraph, cameraData, OcclusionTest.TestAll);
						}
					}
				}
				needsOccluderUpdate = false;
			}
			if (copySchedules.depth == UniversalRenderer.DepthCopySchedule.AfterPrepass)
			{
				this.ExecuteScheduledDepthCopyWithMotion(renderGraph, resourceData, renderPassInputs.requiresMotionVectors);
			}
			else if (copySchedules.depth == UniversalRenderer.DepthCopySchedule.DuringPrepass && renderPassInputs.requiresMotionVectors)
			{
				this.RenderMotionVectors(renderGraph, resourceData);
			}
			base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingPrePasses);
			if (cameraData.xr.hasValidOcclusionMesh)
			{
				XROcclusionMeshPass xrocclusionMeshPass = this.m_XROcclusionMeshPass;
				ContextContainer frameData = base.frameData;
				TextureHandle activeColorTexture = resourceData.activeColorTexture;
				TextureHandle activeDepthTexture = resourceData.activeDepthTexture;
				xrocclusionMeshPass.Render(renderGraph, frameData, in activeColorTexture, in activeDepthTexture);
			}
			if (isDeferred)
			{
				this.m_DeferredLights.Setup(this.m_AdditionalLightsShadowCasterPass);
				this.m_DeferredLights.UseFramebufferFetch = renderGraph.nativeRenderPassesEnabled;
				this.m_DeferredLights.HasNormalPrepass = isDepthNormalPrepass;
				this.m_DeferredLights.HasDepthPrepass = requiresDepthPrepass;
				this.m_DeferredLights.ResolveMixedLightingMode(lightData);
				this.m_DeferredLights.IsOverlay = cameraData.renderType == CameraRenderType.Overlay;
				base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.BeforeRenderingGbuffer);
				bool setGlobalTextures2 = isDepthNormalPrepass && !hasFullPrepass;
				this.m_GBufferPass.Render(renderGraph, base.frameData, resourceData.activeColorTexture, resourceData.activeDepthTexture, setGlobalTextures2);
				if (copySchedules.depth == UniversalRenderer.DepthCopySchedule.AfterGBuffer)
				{
					this.ExecuteScheduledDepthCopyWithMotion(renderGraph, resourceData, renderPassInputs.requiresMotionVectors);
				}
				else if (!renderGraph.nativeRenderPassesEnabled)
				{
					this.CopyDepthToDepthTexture(renderGraph, resourceData);
				}
				base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingGbuffer, RenderPassEvent.BeforeRenderingDeferredLights);
				this.m_DeferredPass.Render(renderGraph, base.frameData, resourceData.activeColorTexture, resourceData.activeDepthTexture, resourceData.gBuffer);
				base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingDeferredLights, RenderPassEvent.BeforeRenderingOpaques);
				TextureHandle mainShadowsTexture = resourceData.mainShadowsTexture;
				TextureHandle additionalShadowsTexture = resourceData.additionalShadowsTexture;
				this.m_RenderOpaqueForwardOnlyPass.Render(renderGraph, base.frameData, resourceData.activeColorTexture, resourceData.activeDepthTexture, mainShadowsTexture, additionalShadowsTexture, uint.MaxValue);
			}
			else
			{
				base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.BeforeRenderingGbuffer, RenderPassEvent.BeforeRenderingOpaques);
				int passCount2 = (needsOccluderUpdate ? 2 : 1);
				for (int passIndex2 = 0; passIndex2 < passCount2; passIndex2++)
				{
					uint batchLayerMask2 = uint.MaxValue;
					if (needsOccluderUpdate)
					{
						OcclusionTest occlusionTest2 = ((passIndex2 == 0) ? OcclusionTest.TestAll : OcclusionTest.TestCulled);
						this.InstanceOcclusionTest(renderGraph, cameraData, occlusionTest2);
						batchLayerMask2 = occlusionTest2.GetBatchLayerMask();
					}
					if (this.m_RenderingLayerProvidesRenderObjectPass)
					{
						this.m_RenderOpaqueForwardWithRenderingLayersPass.Render(renderGraph, base.frameData, resourceData.activeColorTexture, resourceData.renderingLayersTexture, resourceData.activeDepthTexture, resourceData.mainShadowsTexture, resourceData.additionalShadowsTexture, this.m_RenderingLayersMaskSize, batchLayerMask2);
						this.SetRenderingLayersGlobalTextures(renderGraph);
					}
					else
					{
						this.m_RenderOpaqueForwardPass.Render(renderGraph, base.frameData, resourceData.activeColorTexture, resourceData.activeDepthTexture, resourceData.mainShadowsTexture, resourceData.additionalShadowsTexture, batchLayerMask2);
					}
					if (needsOccluderUpdate)
					{
						this.UpdateInstanceOccluders(renderGraph, cameraData, resourceData.activeDepthTexture);
						if (passIndex2 != 0)
						{
							this.InstanceOcclusionTest(renderGraph, cameraData, OcclusionTest.TestAll);
						}
					}
				}
			}
			if (copySchedules.depth == UniversalRenderer.DepthCopySchedule.AfterOpaques)
			{
				this.RecordCustomPassesWithDepthCopyAndMotion(renderGraph, resourceData, renderPassInputs.requiresDepthTextureEarliestEvent, RenderPassEvent.AfterRenderingOpaques, renderPassInputs.requiresMotionVectors);
			}
			else
			{
				base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingOpaques);
			}
			base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.BeforeRenderingSkybox);
			if (cameraData.camera.clearFlags == CameraClearFlags.Skybox && cameraData.renderType != CameraRenderType.Overlay)
			{
				Skybox cameraSkybox;
				cameraData.camera.TryGetComponent<Skybox>(out cameraSkybox);
				Material skyboxMaterial = ((cameraSkybox != null) ? cameraSkybox.material : RenderSettings.skybox);
				if (skyboxMaterial != null)
				{
					this.m_DrawSkyboxPass.Render(renderGraph, base.frameData, context, resourceData.activeColorTexture, resourceData.activeDepthTexture, skyboxMaterial);
				}
			}
			if (copySchedules.depth == UniversalRenderer.DepthCopySchedule.AfterSkybox)
			{
				this.ExecuteScheduledDepthCopyWithMotion(renderGraph, resourceData, renderPassInputs.requiresMotionVectors);
			}
			base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingSkybox);
			if (copySchedules.color == UniversalRenderer.ColorCopySchedule.AfterSkybox)
			{
				TextureHandle activeColor = resourceData.activeColorTexture;
				Downsampling downsamplingMethod = UniversalRenderPipeline.asset.opaqueDownsampling;
				TextureHandle cameraOpaqueTexture;
				this.m_CopyColorPass.Render(renderGraph, base.frameData, out cameraOpaqueTexture, in activeColor, downsamplingMethod);
				resourceData.cameraOpaqueTexture = cameraOpaqueTexture;
			}
			base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.BeforeRenderingTransparents);
			this.m_RenderTransparentForwardPass.m_ShouldTransparentsReceiveShadows = !this.m_TransparentSettingsPass.Setup();
			this.m_RenderTransparentForwardPass.Render(renderGraph, base.frameData, resourceData.activeColorTexture, resourceData.activeDepthTexture, resourceData.mainShadowsTexture, resourceData.additionalShadowsTexture, uint.MaxValue);
			if (copySchedules.depth == UniversalRenderer.DepthCopySchedule.AfterTransparents)
			{
				this.RecordCustomPassesWithDepthCopyAndMotion(renderGraph, resourceData, renderPassInputs.requiresDepthTextureEarliestEvent, RenderPassEvent.AfterRenderingTransparents, renderPassInputs.requiresMotionVectors);
			}
			else
			{
				base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingTransparents);
			}
			if (context.HasInvokeOnRenderObjectCallbacks())
			{
				this.m_OnRenderObjectCallbackPass.Render(renderGraph, resourceData.activeColorTexture, resourceData.activeDepthTexture);
			}
			this.RenderRawColorDepthHistory(renderGraph, cameraData, resourceData);
			bool rendersOverlayUI = cameraData.rendersOverlayUI;
			bool outputToHDR = cameraData.isHDROutputActive;
			if (rendersOverlayUI && outputToHDR)
			{
				TextureHandle overlayUI;
				this.m_DrawOffscreenUIPass.RenderOffscreen(renderGraph, base.frameData, this.cameraDepthAttachmentFormat, out overlayUI);
				resourceData.overlayUITexture = overlayUI;
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00038568 File Offset: 0x00036768
		private void OnAfterRendering(RenderGraph renderGraph)
		{
			UniversalResourceData resourceData = base.frameData.Get<UniversalResourceData>();
			base.frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = base.frameData.Get<UniversalCameraData>();
			UniversalPostProcessingData universalPostProcessingData = base.frameData.Get<UniversalPostProcessingData>();
			if (cameraData.resolveFinalTarget)
			{
				this.SetupRenderGraphFinalPassDebug(renderGraph, base.frameData);
			}
			bool drawGizmos = DebugDisplaySettings<UniversalRenderPipelineDebugDisplaySettings>.Instance.renderingSettings.sceneOverrideMode == DebugSceneOverrideMode.None;
			if (drawGizmos)
			{
				base.DrawRenderGraphGizmos(renderGraph, base.frameData, resourceData.activeColorTexture, resourceData.activeDepthTexture, GizmoSubset.PreImageEffects);
			}
			base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.BeforeRenderingPostProcessing);
			bool applyPostProcessing = this.ShouldApplyPostProcessing(cameraData.postProcessEnabled);
			bool applyFinalPostProcessing = universalPostProcessingData.isEnabled && this.m_PostProcessPasses.isCreated && cameraData.resolveFinalTarget && (cameraData.antialiasing == AntialiasingMode.FastApproximateAntialiasing || (cameraData.imageScalingMode == ImageScalingMode.Upscaling && cameraData.upscalingFilter != ImageUpscalingFilter.Linear) || (cameraData.IsTemporalAAEnabled() && cameraData.taaSettings.contrastAdaptiveSharpening > 0f));
			bool hasCaptureActions = cameraData.captureActions != null && cameraData.resolveFinalTarget;
			bool hasPassesAfterPostProcessing = base.activeRenderPassQueue.Find((ScriptableRenderPass x) => x.renderPassEvent == RenderPassEvent.AfterRenderingPostProcessing) != null;
			bool resolvePostProcessingToCameraTarget = !hasCaptureActions && !hasPassesAfterPostProcessing && !applyFinalPostProcessing;
			bool needsColorEncoding = base.DebugHandler == null || !base.DebugHandler.HDRDebugViewIsActive(cameraData.resolveFinalTarget);
			bool xrDepthTargetResolved = resourceData.activeDepthID == UniversalResourceDataBase.ActiveID.BackBuffer;
			DebugHandler debugHandler = ScriptableRenderPass.GetActiveDebugHandler(cameraData);
			bool resolveToDebugScreen = debugHandler != null && debugHandler.WriteToDebugScreenTexture(cameraData.resolveFinalTarget);
			if (resolveToDebugScreen)
			{
				RenderTextureDescriptor colorDesc = cameraData.cameraTargetDescriptor;
				DebugHandler.ConfigureColorDescriptorForDebugScreen(ref colorDesc, cameraData.pixelWidth, cameraData.pixelHeight);
				resourceData.debugScreenColor = UniversalRenderer.CreateRenderGraphTexture(renderGraph, colorDesc, "_DebugScreenColor", false, FilterMode.Point, TextureWrapMode.Clamp);
				RenderTextureDescriptor depthDesc = cameraData.cameraTargetDescriptor;
				DebugHandler.ConfigureDepthDescriptorForDebugScreen(ref depthDesc, this.cameraDepthAttachmentFormat, cameraData.pixelWidth, cameraData.pixelHeight);
				resourceData.debugScreenDepth = UniversalRenderer.CreateRenderGraphTexture(renderGraph, depthDesc, "_DebugScreenDepth", false, FilterMode.Point, TextureWrapMode.Clamp);
			}
			TextureHandle afterPostProcessColor = resourceData.afterPostProcessColor;
			if (applyPostProcessing)
			{
				TextureHandle activeColor = resourceData.activeColorTexture;
				TextureHandle backbuffer = resourceData.backBufferColor;
				TextureHandle internalColorLut = resourceData.internalColorLut;
				TextureHandle overlayUITexture = resourceData.overlayUITexture;
				bool isTargetBackbuffer = cameraData.resolveFinalTarget && !applyFinalPostProcessing && !hasPassesAfterPostProcessing;
				if (!isTargetBackbuffer)
				{
					ImportResourceParams importColorParams = default(ImportResourceParams);
					importColorParams.clearOnFirstUse = true;
					importColorParams.clearColor = Color.black;
					importColorParams.discardOnLastUse = cameraData.resolveFinalTarget;
					if (cameraData.IsSTPEnabled())
					{
						UniversalRenderer.m_UseUpscaledColorHandle = true;
					}
					resourceData.cameraColor = renderGraph.ImportTexture(this.nextRenderGraphCameraColorHandle, importColorParams);
				}
				TextureHandle target = (isTargetBackbuffer ? backbuffer : resourceData.cameraColor);
				if (resolveToDebugScreen && isTargetBackbuffer)
				{
					target = resourceData.debugScreenColor;
				}
				bool doSRGBEncoding = resolvePostProcessingToCameraTarget && needsColorEncoding;
				this.m_PostProcessPasses.postProcessPass.RenderPostProcessingRenderGraph(renderGraph, base.frameData, in activeColor, in internalColorLut, in overlayUITexture, in target, applyFinalPostProcessing, resolveToDebugScreen, doSRGBEncoding);
				if (cameraData.resolveFinalTarget)
				{
					this.SetupAfterPostRenderGraphFinalPassDebug(renderGraph, base.frameData);
				}
				if (isTargetBackbuffer)
				{
					resourceData.activeColorID = UniversalResourceDataBase.ActiveID.BackBuffer;
					resourceData.activeDepthID = UniversalResourceDataBase.ActiveID.BackBuffer;
				}
			}
			base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingPostProcessing);
			if (applyFinalPostProcessing)
			{
				TextureHandle backBufferColor = resourceData.backBufferColor;
				TextureHandle overlayUITexture2 = resourceData.overlayUITexture;
				TextureHandle target2 = backBufferColor;
				if (resolveToDebugScreen)
				{
					target2 = resourceData.debugScreenColor;
				}
				TextureHandle source = resourceData.cameraColor;
				this.m_PostProcessPasses.finalPostProcessPass.RenderFinalPassRenderGraph(renderGraph, base.frameData, in source, in overlayUITexture2, in target2, needsColorEncoding);
				resourceData.activeColorID = UniversalResourceDataBase.ActiveID.BackBuffer;
				resourceData.activeDepthID = UniversalResourceDataBase.ActiveID.BackBuffer;
			}
			if (cameraData.captureActions != null)
			{
				this.m_CapturePass.RecordRenderGraph(renderGraph, base.frameData);
			}
			bool cameraTargetResolved = applyFinalPostProcessing || (applyPostProcessing && !hasPassesAfterPostProcessing && !hasCaptureActions);
			base.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRendering);
			if (!resourceData.isActiveTargetBackBuffer && cameraData.resolveFinalTarget && !cameraTargetResolved)
			{
				TextureHandle backBufferColor2 = resourceData.backBufferColor;
				TextureHandle overlayUITexture3 = resourceData.overlayUITexture;
				TextureHandle target3 = backBufferColor2;
				if (resolveToDebugScreen)
				{
					target3 = resourceData.debugScreenColor;
				}
				TextureHandle source2 = resourceData.cameraColor;
				this.m_FinalBlitPass.Render(renderGraph, base.frameData, cameraData, in source2, in target3, overlayUITexture3);
				resourceData.activeColorID = UniversalResourceDataBase.ActiveID.BackBuffer;
				resourceData.activeDepthID = UniversalResourceDataBase.ActiveID.BackBuffer;
			}
			bool rendersOverlayUI = cameraData.rendersOverlayUI;
			bool outputToHDR = cameraData.isHDROutputActive;
			if (rendersOverlayUI && !outputToHDR)
			{
				TextureHandle depthBuffer = resourceData.backBufferDepth;
				TextureHandle target4 = resourceData.backBufferColor;
				if (resolveToDebugScreen)
				{
					target4 = resourceData.debugScreenColor;
					depthBuffer = resourceData.debugScreenDepth;
				}
				this.m_DrawOverlayUIPass.RenderOverlay(renderGraph, base.frameData, in target4, in depthBuffer);
			}
			if (cameraData.xr.enabled && !xrDepthTargetResolved && cameraData.xr.copyDepth)
			{
				this.m_XRCopyDepthPass.CopyToDepthXR = true;
				this.m_XRCopyDepthPass.MssaSamples = 1;
				this.m_XRCopyDepthPass.Render(renderGraph, base.frameData, resourceData.backBufferDepth, resourceData.cameraDepth, false, "XR Depth Copy");
			}
			if (debugHandler != null)
			{
				TextureHandle overlayUITexture4 = resourceData.overlayUITexture;
				TextureHandle debugScreenColor = resourceData.debugScreenColor;
			}
			if (cameraData.isSceneViewCamera)
			{
				base.DrawRenderGraphWireOverlay(renderGraph, base.frameData, resourceData.backBufferColor);
			}
			if (drawGizmos)
			{
				base.DrawRenderGraphGizmos(renderGraph, base.frameData, resourceData.backBufferColor, resourceData.activeDepthTexture, GizmoSubset.PostImageEffects);
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00038A64 File Offset: 0x00036C64
		private bool RequireDepthPrepass(UniversalCameraData cameraData, ref UniversalRenderer.RenderPassInputSummary renderPassInputs)
		{
			this.ShouldApplyPostProcessing(cameraData.postProcessEnabled);
			bool cameraHasPostProcessingWithDepth = this.CameraHasPostProcessingWithDepth(cameraData);
			bool forcePrepass = this.m_CopyDepthMode == CopyDepthMode.ForcePrepass;
			bool depthPrimingEnabled = this.IsDepthPrimingEnabled(cameraData);
			bool isGizmosEnabled = false;
			return (((((cameraData.requiresDepthTexture || renderPassInputs.requiresDepthTexture || depthPrimingEnabled || cameraHasPostProcessingWithDepth) && (!this.CanCopyDepth(cameraData) || forcePrepass)) | cameraData.isSceneViewCamera) || isGizmosEnabled) | cameraData.isPreviewCamera | renderPassInputs.requiresDepthPrepass | renderPassInputs.requiresNormalsTexture) || depthPrimingEnabled;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00038AE0 File Offset: 0x00036CE0
		private bool RequireDepthTexture(UniversalCameraData cameraData, bool requiresDepthPrepass, ref UniversalRenderer.RenderPassInputSummary renderPassInputs)
		{
			bool depthPrimingEnabled = this.IsDepthPrimingEnabled(cameraData);
			bool flag = cameraData.requiresDepthTexture || renderPassInputs.requiresDepthTexture || depthPrimingEnabled;
			bool cameraHasPostProcessingWithDepth = this.CameraHasPostProcessingWithDepth(cameraData);
			return ((flag || cameraHasPostProcessingWithDepth) && !requiresDepthPrepass) | !cameraData.resolveFinalTarget | (this.renderingModeActual == RenderingMode.Deferred && !this.useRenderPassEnabled) | (depthPrimingEnabled || cameraData.isPreviewCamera) | this.m_RenderingLayerProvidesRenderObjectPass | this.DebugHandlerRequireDepthPass(cameraData);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00038B58 File Offset: 0x00036D58
		internal void SetRenderingLayersGlobalTextures(RenderGraph renderGraph)
		{
			UniversalResourceData resourceData = base.frameData.Get<UniversalResourceData>();
			if (resourceData.renderingLayersTexture.IsValid() && this.renderingModeActual != RenderingMode.Deferred)
			{
				RenderGraphUtils.SetGlobalTexture(renderGraph, Shader.PropertyToID(this.m_RenderingLayersTextureName), resourceData.renderingLayersTexture, "Set Global Rendering Layers Texture", "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/UniversalRendererRenderGraph.cs", 1697);
			}
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00038BB0 File Offset: 0x00036DB0
		private void CreateCameraDepthCopyTexture(RenderGraph renderGraph, RenderTextureDescriptor descriptor, bool isDepthTexture)
		{
			UniversalResourceData universalResourceData = base.frameData.Get<UniversalResourceData>();
			RenderTextureDescriptor depthDescriptor = descriptor;
			depthDescriptor.msaaSamples = 1;
			if (isDepthTexture)
			{
				depthDescriptor.graphicsFormat = GraphicsFormat.None;
				depthDescriptor.depthStencilFormat = this.cameraDepthTextureFormat;
			}
			else
			{
				depthDescriptor.graphicsFormat = GraphicsFormat.R32_SFloat;
				depthDescriptor.depthStencilFormat = GraphicsFormat.None;
			}
			universalResourceData.cameraDepthTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, depthDescriptor, "_CameraDepthTexture", true, FilterMode.Point, TextureWrapMode.Clamp);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00038C14 File Offset: 0x00036E14
		private void CreateMotionVectorTextures(RenderGraph renderGraph, RenderTextureDescriptor descriptor)
		{
			UniversalResourceData universalResourceData = base.frameData.Get<UniversalResourceData>();
			RenderTextureDescriptor colorDesc = descriptor;
			colorDesc.msaaSamples = 1;
			colorDesc.graphicsFormat = GraphicsFormat.R16G16_SFloat;
			colorDesc.depthStencilFormat = GraphicsFormat.None;
			universalResourceData.motionVectorColor = UniversalRenderer.CreateRenderGraphTexture(renderGraph, colorDesc, "_MotionVectorTexture", true, FilterMode.Point, TextureWrapMode.Clamp);
			RenderTextureDescriptor depthDescriptor = descriptor;
			depthDescriptor.msaaSamples = 1;
			depthDescriptor.graphicsFormat = GraphicsFormat.None;
			depthDescriptor.depthStencilFormat = this.cameraDepthAttachmentFormat;
			universalResourceData.motionVectorDepth = UniversalRenderer.CreateRenderGraphTexture(renderGraph, depthDescriptor, "_MotionVectorDepthTexture", true, FilterMode.Point, TextureWrapMode.Clamp);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00038C90 File Offset: 0x00036E90
		private void CreateCameraNormalsTexture(RenderGraph renderGraph, RenderTextureDescriptor descriptor)
		{
			UniversalResourceData universalResourceData = base.frameData.Get<UniversalResourceData>();
			RenderTextureDescriptor normalDescriptor = descriptor;
			normalDescriptor.depthStencilFormat = GraphicsFormat.None;
			normalDescriptor.msaaSamples = 1;
			string normalsName = ((this.renderingModeActual != RenderingMode.Deferred) ? "_CameraNormalsTexture" : DeferredLights.k_GBufferNames[this.m_DeferredLights.GBufferNormalSmoothnessIndex]);
			normalDescriptor.graphicsFormat = ((this.renderingModeActual != RenderingMode.Deferred) ? DepthNormalOnlyPass.GetGraphicsFormat() : this.m_DeferredLights.GetGBufferFormat(this.m_DeferredLights.GBufferNormalSmoothnessIndex));
			universalResourceData.cameraNormalsTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, normalDescriptor, normalsName, true, FilterMode.Point, TextureWrapMode.Clamp);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00038D1C File Offset: 0x00036F1C
		private void CreateRenderingLayersTexture(RenderGraph renderGraph, RenderTextureDescriptor descriptor)
		{
			if (this.m_RequiresRenderingLayer)
			{
				UniversalResourceData universalResourceData = base.frameData.Get<UniversalResourceData>();
				this.m_RenderingLayersTextureName = "_CameraRenderingLayersTexture";
				if (this.renderingModeActual == RenderingMode.Deferred && this.m_DeferredLights.UseRenderingLayers)
				{
					this.m_RenderingLayersTextureName = DeferredLights.k_GBufferNames[this.m_DeferredLights.GBufferRenderingLayers];
				}
				RenderTextureDescriptor renderingLayersDescriptor = descriptor;
				renderingLayersDescriptor.depthStencilFormat = GraphicsFormat.None;
				if (!this.m_RenderingLayerProvidesRenderObjectPass)
				{
					renderingLayersDescriptor.msaaSamples = 1;
				}
				if (this.renderingModeActual == RenderingMode.Deferred && this.m_RequiresRenderingLayer)
				{
					renderingLayersDescriptor.graphicsFormat = this.m_DeferredLights.GetGBufferFormat(this.m_DeferredLights.GBufferRenderingLayers);
				}
				else
				{
					renderingLayersDescriptor.graphicsFormat = RenderingLayerUtils.GetFormat(this.m_RenderingLayersMaskSize);
				}
				universalResourceData.renderingLayersTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, renderingLayersDescriptor, this.m_RenderingLayersTextureName, true, FilterMode.Point, TextureWrapMode.Clamp);
			}
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00038DE8 File Offset: 0x00036FE8
		private void CreateAfterPostProcessTexture(RenderGraph renderGraph, RenderTextureDescriptor descriptor)
		{
			UniversalResourceData universalResourceData = base.frameData.Get<UniversalResourceData>();
			RenderTextureDescriptor desc = PostProcessPass.GetCompatibleDescriptor(descriptor, descriptor.width, descriptor.height, descriptor.graphicsFormat, GraphicsFormat.None);
			universalResourceData.afterPostProcessColor = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, "_AfterPostProcessTexture", true, FilterMode.Point, TextureWrapMode.Clamp);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00038E34 File Offset: 0x00037034
		private void DepthNormalPrepassRender(RenderGraph renderGraph, UniversalRenderer.RenderPassInputSummary renderPassInputs, TextureHandle depthTarget, uint batchLayerMask, bool setGlobalDepth, bool setGlobalTextures)
		{
			UniversalResourceData universalResourceData = base.frameData.Get<UniversalResourceData>();
			if (this.m_RenderingLayerProvidesByDepthNormalPass)
			{
				this.m_DepthNormalPrepass.enableRenderingLayers = true;
				this.m_DepthNormalPrepass.renderingLayersMaskSize = this.m_RenderingLayersMaskSize;
			}
			else
			{
				this.m_DepthNormalPrepass.enableRenderingLayers = false;
			}
			if (this.renderingModeActual == RenderingMode.Deferred && this.AllowPartialDepthNormalsPrepass(true, renderPassInputs.requiresDepthNormalAtEvent))
			{
				this.m_DepthNormalPrepass.shaderTagIds = UniversalRenderer.k_DepthNormalsOnly;
			}
			TextureHandle normalsTexture = universalResourceData.cameraNormalsTexture;
			TextureHandle renderingLayersTexture = universalResourceData.renderingLayersTexture;
			this.m_DepthNormalPrepass.Render(renderGraph, base.frameData, normalsTexture, depthTarget, renderingLayersTexture, batchLayerMask, setGlobalDepth, setGlobalTextures);
			if (this.m_RequiresRenderingLayer)
			{
				this.SetRenderingLayersGlobalTextures(renderGraph);
			}
		}

		// Token: 0x04000B56 RID: 2902
		private const GraphicsFormat k_DepthStencilFormatDefault = GraphicsFormat.D32_SFloat_S8_UInt;

		// Token: 0x04000B57 RID: 2903
		private const int k_FinalBlitPassQueueOffset = 1;

		// Token: 0x04000B58 RID: 2904
		private const int k_AfterFinalBlitPassQueueOffset = 2;

		// Token: 0x04000B59 RID: 2905
		private static readonly List<ShaderTagId> k_DepthNormalsOnly = new List<ShaderTagId>
		{
			new ShaderTagId("DepthNormalsOnly")
		};

		// Token: 0x04000B5A RID: 2906
		private bool m_Clustering;

		// Token: 0x04000B5B RID: 2907
		private DepthOnlyPass m_DepthPrepass;

		// Token: 0x04000B5C RID: 2908
		private DepthNormalOnlyPass m_DepthNormalPrepass;

		// Token: 0x04000B5D RID: 2909
		private CopyDepthPass m_PrimedDepthCopyPass;

		// Token: 0x04000B5E RID: 2910
		private MotionVectorRenderPass m_MotionVectorPass;

		// Token: 0x04000B5F RID: 2911
		private MainLightShadowCasterPass m_MainLightShadowCasterPass;

		// Token: 0x04000B60 RID: 2912
		private AdditionalLightsShadowCasterPass m_AdditionalLightsShadowCasterPass;

		// Token: 0x04000B61 RID: 2913
		private GBufferPass m_GBufferPass;

		// Token: 0x04000B62 RID: 2914
		private CopyDepthPass m_GBufferCopyDepthPass;

		// Token: 0x04000B63 RID: 2915
		private DeferredPass m_DeferredPass;

		// Token: 0x04000B64 RID: 2916
		private DrawObjectsPass m_RenderOpaqueForwardOnlyPass;

		// Token: 0x04000B65 RID: 2917
		private DrawObjectsPass m_RenderOpaqueForwardPass;

		// Token: 0x04000B66 RID: 2918
		private DrawObjectsWithRenderingLayersPass m_RenderOpaqueForwardWithRenderingLayersPass;

		// Token: 0x04000B67 RID: 2919
		private DrawSkyboxPass m_DrawSkyboxPass;

		// Token: 0x04000B68 RID: 2920
		private CopyDepthPass m_CopyDepthPass;

		// Token: 0x04000B69 RID: 2921
		private CopyColorPass m_CopyColorPass;

		// Token: 0x04000B6A RID: 2922
		private TransparentSettingsPass m_TransparentSettingsPass;

		// Token: 0x04000B6B RID: 2923
		private DrawObjectsPass m_RenderTransparentForwardPass;

		// Token: 0x04000B6C RID: 2924
		private InvokeOnRenderObjectCallbackPass m_OnRenderObjectCallbackPass;

		// Token: 0x04000B6D RID: 2925
		private FinalBlitPass m_FinalBlitPass;

		// Token: 0x04000B6E RID: 2926
		private CapturePass m_CapturePass;

		// Token: 0x04000B6F RID: 2927
		private XROcclusionMeshPass m_XROcclusionMeshPass;

		// Token: 0x04000B70 RID: 2928
		private CopyDepthPass m_XRCopyDepthPass;

		// Token: 0x04000B71 RID: 2929
		private XRDepthMotionPass m_XRDepthMotionPass;

		// Token: 0x04000B72 RID: 2930
		private DrawScreenSpaceUIPass m_DrawOffscreenUIPass;

		// Token: 0x04000B73 RID: 2931
		private DrawScreenSpaceUIPass m_DrawOverlayUIPass;

		// Token: 0x04000B74 RID: 2932
		private CopyColorPass m_HistoryRawColorCopyPass;

		// Token: 0x04000B75 RID: 2933
		private CopyDepthPass m_HistoryRawDepthCopyPass;

		// Token: 0x04000B76 RID: 2934
		internal RenderTargetBufferSystem m_ColorBufferSystem;

		// Token: 0x04000B77 RID: 2935
		internal RTHandle m_ActiveCameraColorAttachment;

		// Token: 0x04000B78 RID: 2936
		private RTHandle m_ColorFrontBuffer;

		// Token: 0x04000B79 RID: 2937
		internal RTHandle m_ActiveCameraDepthAttachment;

		// Token: 0x04000B7A RID: 2938
		internal RTHandle m_CameraDepthAttachment;

		// Token: 0x04000B7B RID: 2939
		private RTHandle m_TargetColorHandle;

		// Token: 0x04000B7C RID: 2940
		private RTHandle m_TargetDepthHandle;

		// Token: 0x04000B7D RID: 2941
		internal RTHandle m_DepthTexture;

		// Token: 0x04000B7E RID: 2942
		private RTHandle m_NormalsTexture;

		// Token: 0x04000B7F RID: 2943
		private RTHandle m_DecalLayersTexture;

		// Token: 0x04000B80 RID: 2944
		private RTHandle m_OpaqueColor;

		// Token: 0x04000B81 RID: 2945
		private RTHandle m_MotionVectorColor;

		// Token: 0x04000B82 RID: 2946
		private RTHandle m_MotionVectorDepth;

		// Token: 0x04000B83 RID: 2947
		private ForwardLights m_ForwardLights;

		// Token: 0x04000B84 RID: 2948
		private DeferredLights m_DeferredLights;

		// Token: 0x04000B85 RID: 2949
		private RenderingMode m_RenderingMode;

		// Token: 0x04000B86 RID: 2950
		private DepthPrimingMode m_DepthPrimingMode;

		// Token: 0x04000B87 RID: 2951
		private CopyDepthMode m_CopyDepthMode;

		// Token: 0x04000B88 RID: 2952
		private DepthFormat m_CameraDepthAttachmentFormat;

		// Token: 0x04000B89 RID: 2953
		private DepthFormat m_CameraDepthTextureFormat;

		// Token: 0x04000B8A RID: 2954
		private bool m_DepthPrimingRecommended;

		// Token: 0x04000B8B RID: 2955
		private StencilState m_DefaultStencilState;

		// Token: 0x04000B8C RID: 2956
		private LightCookieManager m_LightCookieManager;

		// Token: 0x04000B8D RID: 2957
		private IntermediateTextureMode m_IntermediateTextureMode;

		// Token: 0x04000B8E RID: 2958
		private bool m_VulkanEnablePreTransform;

		// Token: 0x04000B8F RID: 2959
		private Material m_BlitMaterial;

		// Token: 0x04000B90 RID: 2960
		private Material m_BlitHDRMaterial;

		// Token: 0x04000B91 RID: 2961
		private Material m_SamplingMaterial;

		// Token: 0x04000B92 RID: 2962
		private Material m_StencilDeferredMaterial;

		// Token: 0x04000B93 RID: 2963
		private Material m_CameraMotionVecMaterial;

		// Token: 0x04000B94 RID: 2964
		private PostProcessPasses m_PostProcessPasses;

		// Token: 0x04000B97 RID: 2967
		private Material m_DebugBlitMaterial = Blitter.GetBlitMaterial(TextureXR.dimension, false);

		// Token: 0x04000B98 RID: 2968
		private static RTHandle[] m_RenderGraphCameraColorHandles = new RTHandle[2];

		// Token: 0x04000B99 RID: 2969
		private static RTHandle[] m_RenderGraphUpscaledCameraColorHandles = new RTHandle[2];

		// Token: 0x04000B9A RID: 2970
		private static RTHandle m_RenderGraphCameraDepthHandle;

		// Token: 0x04000B9B RID: 2971
		private static int m_CurrentColorHandle = 0;

		// Token: 0x04000B9C RID: 2972
		private static bool m_UseUpscaledColorHandle = false;

		// Token: 0x04000B9D RID: 2973
		private static RTHandle m_RenderGraphDebugTextureHandle;

		// Token: 0x04000B9E RID: 2974
		private bool m_RequiresRenderingLayer;

		// Token: 0x04000B9F RID: 2975
		private RenderingLayerUtils.Event m_RenderingLayersEvent;

		// Token: 0x04000BA0 RID: 2976
		private RenderingLayerUtils.MaskSize m_RenderingLayersMaskSize;

		// Token: 0x04000BA1 RID: 2977
		private bool m_RenderingLayerProvidesRenderObjectPass;

		// Token: 0x04000BA2 RID: 2978
		private bool m_RenderingLayerProvidesByDepthNormalPass;

		// Token: 0x04000BA3 RID: 2979
		private string m_RenderingLayersTextureName;

		// Token: 0x04000BA4 RID: 2980
		private const string _CameraTargetAttachmentAName = "_CameraTargetAttachmentA";

		// Token: 0x04000BA5 RID: 2981
		private const string _CameraTargetAttachmentBName = "_CameraTargetAttachmentB";

		// Token: 0x04000BA6 RID: 2982
		private const string _CameraUpscaledTargetAttachmentAName = "_CameraUpscaledTargetAttachmentA";

		// Token: 0x04000BA7 RID: 2983
		private const string _CameraUpscaledTargetAttachmentBName = "_CameraUpscaledTargetAttachmentB";

		// Token: 0x04000BA8 RID: 2984
		private bool m_IssuedGPUOcclusionUnsupportedMsg;

		// Token: 0x04000BA9 RID: 2985
		private static bool m_CreateColorAttachment;

		// Token: 0x04000BAA RID: 2986
		private static bool m_CreateDepthAttachment;

		// Token: 0x020001D8 RID: 472
		private static class Profiling
		{
			// Token: 0x04000BAB RID: 2987
			private const string k_Name = "UniversalRenderer";

			// Token: 0x04000BAC RID: 2988
			public static readonly ProfilingSampler createCameraRenderTarget = new ProfilingSampler("UniversalRenderer.CreateCameraRenderTarget");
		}

		// Token: 0x020001D9 RID: 473
		private struct RenderPassInputSummary
		{
			// Token: 0x04000BAD RID: 2989
			internal bool requiresDepthTexture;

			// Token: 0x04000BAE RID: 2990
			internal bool requiresDepthPrepass;

			// Token: 0x04000BAF RID: 2991
			internal bool requiresNormalsTexture;

			// Token: 0x04000BB0 RID: 2992
			internal bool requiresColorTexture;

			// Token: 0x04000BB1 RID: 2993
			internal bool requiresColorTextureCreated;

			// Token: 0x04000BB2 RID: 2994
			internal bool requiresMotionVectors;

			// Token: 0x04000BB3 RID: 2995
			internal RenderPassEvent requiresDepthNormalAtEvent;

			// Token: 0x04000BB4 RID: 2996
			internal RenderPassEvent requiresDepthTextureEarliestEvent;
		}

		// Token: 0x020001DA RID: 474
		private class CopyToDebugTexturePassData
		{
			// Token: 0x04000BB5 RID: 2997
			internal TextureHandle src;

			// Token: 0x04000BB6 RID: 2998
			internal TextureHandle dest;
		}

		// Token: 0x020001DB RID: 475
		private enum DepthCopySchedule
		{
			// Token: 0x04000BB8 RID: 3000
			DuringPrepass,
			// Token: 0x04000BB9 RID: 3001
			AfterPrepass,
			// Token: 0x04000BBA RID: 3002
			AfterGBuffer,
			// Token: 0x04000BBB RID: 3003
			AfterOpaques,
			// Token: 0x04000BBC RID: 3004
			AfterSkybox,
			// Token: 0x04000BBD RID: 3005
			AfterTransparents,
			// Token: 0x04000BBE RID: 3006
			None
		}

		// Token: 0x020001DC RID: 476
		private enum ColorCopySchedule
		{
			// Token: 0x04000BC0 RID: 3008
			AfterSkybox,
			// Token: 0x04000BC1 RID: 3009
			None
		}

		// Token: 0x020001DD RID: 477
		private struct TextureCopySchedules
		{
			// Token: 0x04000BC2 RID: 3010
			internal UniversalRenderer.DepthCopySchedule depth;

			// Token: 0x04000BC3 RID: 3011
			internal UniversalRenderer.ColorCopySchedule color;
		}
	}
}
