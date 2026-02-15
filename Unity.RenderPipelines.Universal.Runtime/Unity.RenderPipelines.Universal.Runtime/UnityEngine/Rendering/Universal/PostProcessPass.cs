using System;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000121 RID: 289
	internal class PostProcessPass : ScriptableRenderPass
	{
		// Token: 0x06000678 RID: 1656 RVA: 0x00019094 File Offset: 0x00017294
		public PostProcessPass(RenderPassEvent evt, PostProcessData data, ref PostProcessParams postProcessParams)
		{
			base.profilingSampler = new ProfilingSampler("PostProcessPass");
			base.renderPassEvent = evt;
			this.m_Data = data;
			this.m_Materials = new PostProcessPass.MaterialLibrary(data);
			PostProcessPass.ShaderConstants._BloomMipUp = new int[16];
			PostProcessPass.ShaderConstants._BloomMipDown = new int[16];
			this.m_BloomMipUp = new RTHandle[16];
			this.m_BloomMipDown = new RTHandle[16];
			this._BloomMipUp = new TextureHandle[16];
			this._BloomMipDown = new TextureHandle[16];
			for (int i = 0; i < 16; i++)
			{
				PostProcessPass.ShaderConstants._BloomMipUp[i] = Shader.PropertyToID("_BloomMipUp" + i.ToString());
				PostProcessPass.ShaderConstants._BloomMipDown[i] = Shader.PropertyToID("_BloomMipDown" + i.ToString());
				this.m_BloomMipUp[i] = RTHandles.Alloc(PostProcessPass.ShaderConstants._BloomMipUp[i], "_BloomMipUp" + i.ToString());
				this.m_BloomMipDown[i] = RTHandles.Alloc(PostProcessPass.ShaderConstants._BloomMipDown[i], "_BloomMipDown" + i.ToString());
			}
			this.m_MRT2 = new RenderTargetIdentifier[2];
			base.useNativeRenderPass = false;
			this.m_BlitMaterial = postProcessParams.blitMaterial;
			bool flag = this.IsHDRFormat(postProcessParams.requestColorFormat);
			bool requestAlpha = this.IsAlphaFormat(postProcessParams.requestColorFormat);
			if (flag)
			{
				this.m_DefaultColorFormatIsAlpha = requestAlpha;
				if (SystemInfo.IsFormatSupported(postProcessParams.requestColorFormat, GraphicsFormatUsage.Blend))
				{
					this.m_DefaultColorFormat = postProcessParams.requestColorFormat;
				}
				else if (SystemInfo.IsFormatSupported(GraphicsFormat.B10G11R11_UFloatPack32, GraphicsFormatUsage.Blend))
				{
					this.m_DefaultColorFormat = GraphicsFormat.B10G11R11_UFloatPack32;
					this.m_DefaultColorFormatIsAlpha = false;
				}
				else
				{
					this.m_DefaultColorFormat = ((QualitySettings.activeColorSpace == ColorSpace.Linear) ? GraphicsFormat.R8G8B8A8_SRGB : GraphicsFormat.R8G8B8A8_UNorm);
				}
			}
			else
			{
				this.m_DefaultColorFormat = ((QualitySettings.activeColorSpace == ColorSpace.Linear) ? GraphicsFormat.R8G8B8A8_SRGB : GraphicsFormat.R8G8B8A8_UNorm);
				this.m_DefaultColorFormatIsAlpha = true;
			}
			if (SystemInfo.IsFormatSupported(GraphicsFormat.R8G8_UNorm, GraphicsFormatUsage.Render) && SystemInfo.graphicsDeviceVendor.ToLowerInvariant().Contains("arm"))
			{
				this.m_SMAAEdgeFormat = GraphicsFormat.R8G8_UNorm;
			}
			else
			{
				this.m_SMAAEdgeFormat = GraphicsFormat.R8G8B8A8_UNorm;
			}
			if (SystemInfo.IsFormatSupported(GraphicsFormat.R16_UNorm, GraphicsFormatUsage.Blend))
			{
				this.m_GaussianCoCFormat = GraphicsFormat.R16_UNorm;
				return;
			}
			if (SystemInfo.IsFormatSupported(GraphicsFormat.R16_SFloat, GraphicsFormatUsage.Blend))
			{
				this.m_GaussianCoCFormat = GraphicsFormat.R16_SFloat;
				return;
			}
			this.m_GaussianCoCFormat = GraphicsFormat.R8_UNorm;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x000192C4 File Offset: 0x000174C4
		public void Cleanup()
		{
			this.m_Materials.Cleanup();
			this.Dispose();
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000192D8 File Offset: 0x000174D8
		public void Dispose()
		{
			foreach (RTHandle rthandle in this.m_BloomMipDown)
			{
				if (rthandle != null)
				{
					rthandle.Release();
				}
			}
			foreach (RTHandle rthandle2 in this.m_BloomMipUp)
			{
				if (rthandle2 != null)
				{
					rthandle2.Release();
				}
			}
			RTHandle scalingSetupTarget = this.m_ScalingSetupTarget;
			if (scalingSetupTarget != null)
			{
				scalingSetupTarget.Release();
			}
			RTHandle upscaledTarget = this.m_UpscaledTarget;
			if (upscaledTarget != null)
			{
				upscaledTarget.Release();
			}
			RTHandle fullCoCTexture = this.m_FullCoCTexture;
			if (fullCoCTexture != null)
			{
				fullCoCTexture.Release();
			}
			RTHandle halfCoCTexture = this.m_HalfCoCTexture;
			if (halfCoCTexture != null)
			{
				halfCoCTexture.Release();
			}
			RTHandle pingTexture = this.m_PingTexture;
			if (pingTexture != null)
			{
				pingTexture.Release();
			}
			RTHandle pongTexture = this.m_PongTexture;
			if (pongTexture != null)
			{
				pongTexture.Release();
			}
			RTHandle blendTexture = this.m_BlendTexture;
			if (blendTexture != null)
			{
				blendTexture.Release();
			}
			RTHandle edgeColorTexture = this.m_EdgeColorTexture;
			if (edgeColorTexture != null)
			{
				edgeColorTexture.Release();
			}
			RTHandle edgeStencilTexture = this.m_EdgeStencilTexture;
			if (edgeStencilTexture != null)
			{
				edgeStencilTexture.Release();
			}
			RTHandle tempTarget = this.m_TempTarget;
			if (tempTarget != null)
			{
				tempTarget.Release();
			}
			RTHandle tempTarget2 = this.m_TempTarget2;
			if (tempTarget2 != null)
			{
				tempTarget2.Release();
			}
			RTHandle streakTmpTexture = this.m_StreakTmpTexture;
			if (streakTmpTexture != null)
			{
				streakTmpTexture.Release();
			}
			RTHandle streakTmpTexture2 = this.m_StreakTmpTexture2;
			if (streakTmpTexture2 != null)
			{
				streakTmpTexture2.Release();
			}
			RTHandle screenSpaceLensFlareResult = this.m_ScreenSpaceLensFlareResult;
			if (screenSpaceLensFlareResult != null)
			{
				screenSpaceLensFlareResult.Release();
			}
			RTHandle userLut = this.m_UserLut;
			if (userLut == null)
			{
				return;
			}
			userLut.Release();
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001942C File Offset: 0x0001762C
		public void Setup(in RenderTextureDescriptor baseDescriptor, in RTHandle source, bool resolveToScreen, in RTHandle depth, in RTHandle internalLut, in RTHandle motionVectors, bool hasFinalPass, bool enableColorEncoding)
		{
			this.m_Descriptor = baseDescriptor;
			this.m_Descriptor.useMipMap = false;
			this.m_Descriptor.autoGenerateMips = false;
			this.m_Source = source;
			this.m_Depth = depth;
			this.m_InternalLut = internalLut;
			this.m_MotionVectors = motionVectors;
			this.m_IsFinalPass = false;
			this.m_HasFinalPass = hasFinalPass;
			this.m_EnableColorEncodingIfNeeded = enableColorEncoding;
			this.m_ResolveToScreen = resolveToScreen;
			this.m_UseSwapBuffer = true;
			this.m_Destination = ScriptableRenderPass.k_CameraTarget;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x000194B0 File Offset: 0x000176B0
		public void Setup(in RenderTextureDescriptor baseDescriptor, in RTHandle source, RTHandle destination, in RTHandle depth, in RTHandle internalLut, bool hasFinalPass, bool enableColorEncoding)
		{
			this.m_Descriptor = baseDescriptor;
			this.m_Descriptor.useMipMap = false;
			this.m_Descriptor.autoGenerateMips = false;
			this.m_Source = source;
			this.m_Destination = destination;
			this.m_Depth = depth;
			this.m_InternalLut = internalLut;
			this.m_IsFinalPass = false;
			this.m_HasFinalPass = hasFinalPass;
			this.m_EnableColorEncodingIfNeeded = enableColorEncoding;
			this.m_UseSwapBuffer = true;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00019520 File Offset: 0x00017720
		public void SetupFinalPass(in RTHandle source, bool useSwapBuffer = false, bool enableColorEncoding = true)
		{
			this.m_Source = source;
			this.m_IsFinalPass = true;
			this.m_HasFinalPass = false;
			this.m_EnableColorEncodingIfNeeded = enableColorEncoding;
			this.m_UseSwapBuffer = useSwapBuffer;
			this.m_Destination = ScriptableRenderPass.k_CameraTarget;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00019551 File Offset: 0x00017751
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			base.overrideCameraTarget = true;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00002886 File Offset: 0x00000A86
		public bool CanRunOnTile()
		{
			return false;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001955C File Offset: 0x0001775C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			VolumeStack stack = VolumeManager.instance.stack;
			this.m_DepthOfField = stack.GetComponent<DepthOfField>();
			this.m_MotionBlur = stack.GetComponent<MotionBlur>();
			this.m_LensFlareScreenSpace = stack.GetComponent<ScreenSpaceLensFlare>();
			this.m_PaniniProjection = stack.GetComponent<PaniniProjection>();
			this.m_Bloom = stack.GetComponent<Bloom>();
			this.m_LensDistortion = stack.GetComponent<LensDistortion>();
			this.m_ChromaticAberration = stack.GetComponent<ChromaticAberration>();
			this.m_Vignette = stack.GetComponent<Vignette>();
			this.m_ColorLookup = stack.GetComponent<ColorLookup>();
			this.m_ColorAdjustments = stack.GetComponent<ColorAdjustments>();
			this.m_Tonemapping = stack.GetComponent<Tonemapping>();
			this.m_FilmGrain = stack.GetComponent<FilmGrain>();
			this.m_UseFastSRGBLinearConversion = *renderingData.postProcessingData.useFastSRGBLinearConversion;
			this.m_SupportScreenSpaceLensFlare = *renderingData.postProcessingData.supportScreenSpaceLensFlare;
			this.m_SupportDataDrivenLensFlare = *renderingData.postProcessingData.supportDataDrivenLensFlare;
			CommandBuffer cmd = *renderingData.commandBuffer;
			if (this.m_IsFinalPass)
			{
				using (new ProfilingScope(cmd, PostProcessPass.m_ProfilingRenderFinalPostProcessing))
				{
					this.RenderFinalPass(cmd, ref renderingData);
					return;
				}
			}
			if (!this.CanRunOnTile())
			{
				using (new ProfilingScope(cmd, PostProcessPass.m_ProfilingRenderPostProcessing))
				{
					this.Render(cmd, ref renderingData);
				}
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000196B8 File Offset: 0x000178B8
		private bool IsHDRFormat(GraphicsFormat format)
		{
			return format == GraphicsFormat.B10G11R11_UFloatPack32 || GraphicsFormatUtility.IsHalfFormat(format) || GraphicsFormatUtility.IsFloatFormat(format);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000196CF File Offset: 0x000178CF
		private bool IsAlphaFormat(GraphicsFormat format)
		{
			return GraphicsFormatUtility.HasAlphaChannel(format);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000196D7 File Offset: 0x000178D7
		private RenderTextureDescriptor GetCompatibleDescriptor()
		{
			return this.GetCompatibleDescriptor(this.m_Descriptor.width, this.m_Descriptor.height, this.m_Descriptor.graphicsFormat, GraphicsFormat.None);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00019701 File Offset: 0x00017901
		private RenderTextureDescriptor GetCompatibleDescriptor(int width, int height, GraphicsFormat format, GraphicsFormat depthStencilFormat = GraphicsFormat.None)
		{
			return PostProcessPass.GetCompatibleDescriptor(this.m_Descriptor, width, height, format, depthStencilFormat);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00019713 File Offset: 0x00017913
		internal static RenderTextureDescriptor GetCompatibleDescriptor(RenderTextureDescriptor desc, int width, int height, GraphicsFormat format, GraphicsFormat depthStencilFormat = GraphicsFormat.None)
		{
			desc.depthStencilFormat = depthStencilFormat;
			desc.msaaSamples = 1;
			desc.width = width;
			desc.height = height;
			desc.graphicsFormat = format;
			return desc;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001973F File Offset: 0x0001793F
		private bool RequireSRGBConversionBlitToBackBuffer(bool requireSrgbConversion)
		{
			return requireSrgbConversion && this.m_EnableColorEncodingIfNeeded;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001974C File Offset: 0x0001794C
		private bool RequireHDROutput(UniversalCameraData cameraData)
		{
			return cameraData.isHDROutputActive && cameraData.captureActions == null;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00019764 File Offset: 0x00017964
		private unsafe void Render(CommandBuffer cmd, ref RenderingData renderingData)
		{
			PostProcessPass.<>c__DisplayClass90_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cmd = cmd;
			UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
			ref ScriptableRenderer renderer = ref cameraData.renderer;
			bool isSceneViewCamera = cameraData.isSceneViewCamera;
			bool useStopNan = cameraData.isStopNaNEnabled && this.m_Materials.stopNaN != null;
			bool useSubPixeMorpAA = cameraData.antialiasing == AntialiasingMode.SubpixelMorphologicalAntiAliasing;
			Material dofMaterial = ((this.m_DepthOfField.mode.value == DepthOfFieldMode.Gaussian) ? this.m_Materials.gaussianDepthOfField : this.m_Materials.bokehDepthOfField);
			bool useDepthOfField = this.m_DepthOfField.IsActive() && !isSceneViewCamera && dofMaterial != null;
			bool useLensFlare = !LensFlareCommonSRP.Instance.IsEmpty() && this.m_SupportDataDrivenLensFlare;
			bool useLensFlareScreenSpace = this.m_LensFlareScreenSpace.IsActive() && this.m_SupportScreenSpaceLensFlare;
			bool useMotionBlur = this.m_MotionBlur.IsActive() && !isSceneViewCamera;
			bool usePaniniProjection = this.m_PaniniProjection.IsActive() && !isSceneViewCamera;
			useMotionBlur = useMotionBlur && Application.isPlaying;
			bool useTemporalAA = cameraData.IsTemporalAAEnabled();
			if (cameraData.antialiasing == AntialiasingMode.TemporalAntiAliasing && !useTemporalAA)
			{
				TemporalAA.ValidateAndWarn(cameraData);
			}
			CS$<>8__locals1.amountOfPassesRemaining = (useStopNan ? 1 : 0) + (useSubPixeMorpAA ? 1 : 0) + (useDepthOfField ? 1 : 0) + (useLensFlare ? 1 : 0) + (useTemporalAA ? 1 : 0) + (useMotionBlur ? 1 : 0) + (usePaniniProjection ? 1 : 0);
			if (this.m_UseSwapBuffer && CS$<>8__locals1.amountOfPassesRemaining > 0)
			{
				renderer.EnableSwapBufferMSAA(false);
			}
			CS$<>8__locals1.source = (this.m_UseSwapBuffer ? renderer.cameraColorTargetHandle : this.m_Source);
			CS$<>8__locals1.destination = (this.m_UseSwapBuffer ? renderer.GetCameraColorFrontBuffer(CS$<>8__locals1.cmd) : null);
			CS$<>8__locals1.cmd.SetGlobalMatrix(PostProcessPass.ShaderConstants._FullscreenProjMat, GL.GetGPUProjectionMatrix(Matrix4x4.identity, true));
			if (useStopNan)
			{
				using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.StopNaNs)))
				{
					Blitter.BlitCameraTexture(CS$<>8__locals1.cmd, this.<Render>g__GetSource|90_0(ref CS$<>8__locals1), this.<Render>g__GetDestination|90_1(ref CS$<>8__locals1), RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, this.m_Materials.stopNaN, 0);
					this.<Render>g__Swap|90_2(ref renderer, ref CS$<>8__locals1);
				}
			}
			if (useSubPixeMorpAA)
			{
				using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.SMAA)))
				{
					this.DoSubpixelMorphologicalAntialiasing(ref renderingData.cameraData, CS$<>8__locals1.cmd, this.<Render>g__GetSource|90_0(ref CS$<>8__locals1), this.<Render>g__GetDestination|90_1(ref CS$<>8__locals1));
					this.<Render>g__Swap|90_2(ref renderer, ref CS$<>8__locals1);
				}
			}
			if (useDepthOfField)
			{
				URPProfileId markerName = ((this.m_DepthOfField.mode.value == DepthOfFieldMode.Gaussian) ? URPProfileId.GaussianDepthOfField : URPProfileId.BokehDepthOfField);
				using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(markerName)))
				{
					this.DoDepthOfField(ref renderingData.cameraData, CS$<>8__locals1.cmd, this.<Render>g__GetSource|90_0(ref CS$<>8__locals1), this.<Render>g__GetDestination|90_1(ref CS$<>8__locals1), cameraData.pixelRect);
					this.<Render>g__Swap|90_2(ref renderer, ref CS$<>8__locals1);
				}
			}
			if (useTemporalAA)
			{
				using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.TemporalAA)))
				{
					CommandBuffer cmd2 = CS$<>8__locals1.cmd;
					Material temporalAntialiasing = this.m_Materials.temporalAntialiasing;
					RTHandle source = CS$<>8__locals1.source;
					RTHandle destination = CS$<>8__locals1.destination;
					RTHandle motionVectors = this.m_MotionVectors;
					TemporalAA.ExecutePass(cmd2, temporalAntialiasing, ref renderingData.cameraData, source, destination, (motionVectors != null) ? motionVectors.rt : null);
					this.<Render>g__Swap|90_2(ref renderer, ref CS$<>8__locals1);
				}
			}
			if (useMotionBlur)
			{
				using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.MotionBlur)))
				{
					this.DoMotionBlur(CS$<>8__locals1.cmd, this.<Render>g__GetSource|90_0(ref CS$<>8__locals1), this.<Render>g__GetDestination|90_1(ref CS$<>8__locals1), this.m_MotionVectors, ref renderingData.cameraData);
					this.<Render>g__Swap|90_2(ref renderer, ref CS$<>8__locals1);
				}
			}
			if (usePaniniProjection)
			{
				using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.PaniniProjection)))
				{
					this.DoPaniniProjection(cameraData.camera, CS$<>8__locals1.cmd, this.<Render>g__GetSource|90_0(ref CS$<>8__locals1), this.<Render>g__GetDestination|90_1(ref CS$<>8__locals1));
					this.<Render>g__Swap|90_2(ref renderer, ref CS$<>8__locals1);
				}
			}
			using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.UberPostProcess)))
			{
				this.m_Materials.uber.shaderKeywords = null;
				bool flag = this.m_Bloom.IsActive();
				bool lensFlareScreenSpaceActive = this.m_LensFlareScreenSpace.IsActive();
				if (flag || lensFlareScreenSpaceActive)
				{
					using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.Bloom)))
					{
						this.SetupBloom(CS$<>8__locals1.cmd, this.<Render>g__GetSource|90_0(ref CS$<>8__locals1), this.m_Materials.uber, cameraData.isAlphaOutputEnabled);
					}
				}
				if (useLensFlareScreenSpace)
				{
					using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.LensFlareScreenSpace)))
					{
						int maxBloomMip = Mathf.Clamp(this.m_LensFlareScreenSpace.bloomMip.value, 0, this.m_Bloom.maxIterations.value / 2);
						this.DoLensFlareScreenSpace(cameraData.camera, CS$<>8__locals1.cmd, this.<Render>g__GetSource|90_0(ref CS$<>8__locals1), this.m_BloomMipUp[0], this.m_BloomMipUp[maxBloomMip]);
					}
				}
				if (useLensFlare)
				{
					bool usePanini;
					float paniniDistance;
					float paniniCropToFit;
					if (this.m_PaniniProjection.IsActive())
					{
						usePanini = true;
						paniniDistance = this.m_PaniniProjection.distance.value;
						paniniCropToFit = this.m_PaniniProjection.cropToFit.value;
					}
					else
					{
						usePanini = false;
						paniniDistance = 1f;
						paniniCropToFit = 1f;
					}
					using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.LensFlareDataDrivenComputeOcclusion)))
					{
						this.LensFlareDataDrivenComputeOcclusion(ref cameraData, CS$<>8__locals1.cmd, this.<Render>g__GetSource|90_0(ref CS$<>8__locals1), usePanini, paniniDistance, paniniCropToFit);
					}
					using (new ProfilingScope(CS$<>8__locals1.cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.LensFlareDataDriven)))
					{
						this.LensFlareDataDriven(ref cameraData, CS$<>8__locals1.cmd, this.<Render>g__GetSource|90_0(ref CS$<>8__locals1), usePanini, paniniDistance, paniniCropToFit);
					}
				}
				this.SetupLensDistortion(this.m_Materials.uber, isSceneViewCamera);
				this.SetupChromaticAberration(this.m_Materials.uber);
				this.SetupVignette(this.m_Materials.uber, cameraData.xr);
				this.SetupColorGrading(CS$<>8__locals1.cmd, ref renderingData, this.m_Materials.uber);
				this.SetupGrain(cameraData, this.m_Materials.uber);
				this.SetupDithering(cameraData, this.m_Materials.uber);
				if (this.RequireSRGBConversionBlitToBackBuffer(cameraData.requireSrgbConversion))
				{
					this.m_Materials.uber.EnableKeyword("_LINEAR_TO_SRGB_CONVERSION");
				}
				if (this.RequireHDROutput(cameraData))
				{
					HDROutputUtils.Operation hdrOperation = ((!this.m_HasFinalPass && this.m_EnableColorEncodingIfNeeded) ? HDROutputUtils.Operation.ColorEncoding : HDROutputUtils.Operation.None);
					this.SetupHDROutput(cameraData.hdrDisplayInformation, cameraData.hdrDisplayColorGamut, this.m_Materials.uber, hdrOperation, cameraData.rendersOverlayUI);
				}
				if (this.m_UseFastSRGBLinearConversion)
				{
					this.m_Materials.uber.EnableKeyword("_USE_FAST_SRGB_LINEAR_CONVERSION");
				}
				CoreUtils.SetKeyword(this.m_Materials.uber, "_ENABLE_ALPHA_OUTPUT", cameraData.isAlphaOutputEnabled);
				DebugHandler debugHandler = ScriptableRenderPass.GetActiveDebugHandler(cameraData);
				bool resolveToDebugScreen = debugHandler != null && debugHandler.WriteToDebugScreenTexture(cameraData.resolveFinalTarget);
				RenderBufferLoadAction colorLoadAction = RenderBufferLoadAction.DontCare;
				if (this.m_Destination == ScriptableRenderPass.k_CameraTarget && !cameraData.isDefaultViewport)
				{
					colorLoadAction = RenderBufferLoadAction.Load;
				}
				RenderTargetIdentifier cameraTargetID = BuiltinRenderTextureType.CameraTarget;
				if (cameraData.xr.enabled)
				{
					cameraTargetID = cameraData.xr.renderTarget;
				}
				if (!this.m_UseSwapBuffer)
				{
					this.m_ResolveToScreen = cameraData.resolveFinalTarget || this.m_Destination.nameID == cameraTargetID || this.m_HasFinalPass;
				}
				if (this.m_UseSwapBuffer && !this.m_ResolveToScreen)
				{
					if (!this.m_HasFinalPass)
					{
						renderer.EnableSwapBufferMSAA(true);
						CS$<>8__locals1.destination = renderer.GetCameraColorFrontBuffer(CS$<>8__locals1.cmd);
					}
					Blitter.BlitCameraTexture(CS$<>8__locals1.cmd, this.<Render>g__GetSource|90_0(ref CS$<>8__locals1), CS$<>8__locals1.destination, colorLoadAction, RenderBufferStoreAction.Store, this.m_Materials.uber, 0);
					renderer.ConfigureCameraColorTarget(CS$<>8__locals1.destination);
					this.<Render>g__Swap|90_2(ref renderer, ref CS$<>8__locals1);
				}
				else if (!this.m_UseSwapBuffer)
				{
					RTHandle firstSource = this.<Render>g__GetSource|90_0(ref CS$<>8__locals1);
					Blitter.BlitCameraTexture(CS$<>8__locals1.cmd, firstSource, this.<Render>g__GetDestination|90_1(ref CS$<>8__locals1), colorLoadAction, RenderBufferStoreAction.Store, this.m_Materials.uber, 0);
					CommandBuffer cmd3 = CS$<>8__locals1.cmd;
					RTHandle rthandle = this.<Render>g__GetDestination|90_1(ref CS$<>8__locals1);
					RTHandle destination2 = this.m_Destination;
					RenderBufferLoadAction renderBufferLoadAction = RenderBufferLoadAction.DontCare;
					RenderBufferStoreAction renderBufferStoreAction = RenderBufferStoreAction.Store;
					Material blitMaterial = this.m_BlitMaterial;
					RenderTexture rt = this.m_Destination.rt;
					Blitter.BlitCameraTexture(cmd3, rthandle, destination2, renderBufferLoadAction, renderBufferStoreAction, blitMaterial, (rt != null && rt.filterMode == FilterMode.Bilinear) ? 1 : 0);
				}
				else if (this.m_ResolveToScreen)
				{
					if (resolveToDebugScreen)
					{
						Blitter.BlitCameraTexture(CS$<>8__locals1.cmd, this.<Render>g__GetSource|90_0(ref CS$<>8__locals1), *debugHandler.DebugScreenColorHandle, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, this.m_Materials.uber, 0);
						renderer.ConfigureCameraTarget(*debugHandler.DebugScreenColorHandle, *debugHandler.DebugScreenDepthHandle);
					}
					else
					{
						RTHandleStaticHelpers.SetRTHandleStaticWrapper((cameraData.targetTexture != null) ? new RenderTargetIdentifier(cameraData.targetTexture) : cameraTargetID);
						RTHandle cameraTargetHandle = RTHandleStaticHelpers.s_RTHandleWrapper;
						RenderingUtils.FinalBlit(CS$<>8__locals1.cmd, cameraData, this.<Render>g__GetSource|90_0(ref CS$<>8__locals1), cameraTargetHandle, colorLoadAction, RenderBufferStoreAction.Store, this.m_Materials.uber, 0);
						renderer.ConfigureCameraColorTarget(cameraTargetHandle);
					}
				}
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001A1C0 File Offset: 0x000183C0
		private unsafe void DoSubpixelMorphologicalAntialiasing(ref CameraData cameraData, CommandBuffer cmd, RTHandle source, RTHandle destination)
		{
			Rect pixelRect = new Rect(Vector2.zero, new Vector2((float)cameraData.cameraTargetDescriptor.width, (float)cameraData.cameraTargetDescriptor.height));
			Material material = this.m_Materials.subpixelMorphologicalAntialiasing;
			RenderTextureDescriptor renderTextureDescriptor = this.GetCompatibleDescriptor(this.m_Descriptor.width, this.m_Descriptor.height, GraphicsFormat.None, GraphicsFormatUtility.GetDepthStencilFormat(24));
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_EdgeStencilTexture, in renderTextureDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_EdgeStencilTexture");
			renderTextureDescriptor = this.GetCompatibleDescriptor(this.m_Descriptor.width, this.m_Descriptor.height, this.m_SMAAEdgeFormat, GraphicsFormat.None);
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_EdgeColorTexture, in renderTextureDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_EdgeColorTexture");
			renderTextureDescriptor = this.GetCompatibleDescriptor(this.m_Descriptor.width, this.m_Descriptor.height, GraphicsFormat.R8G8B8A8_UNorm, GraphicsFormat.None);
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_BlendTexture, in renderTextureDescriptor, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, "_BlendTexture");
			Vector2Int targetSize = (this.m_EdgeColorTexture.useScaling ? this.m_EdgeColorTexture.rtHandleProperties.currentRenderTargetSize : new Vector2Int(this.m_EdgeColorTexture.rt.width, this.m_EdgeColorTexture.rt.height));
			material.SetVector(PostProcessPass.ShaderConstants._Metrics, new Vector4(1f / (float)targetSize.x, 1f / (float)targetSize.y, (float)targetSize.x, (float)targetSize.y));
			material.SetTexture(PostProcessPass.ShaderConstants._AreaTexture, this.m_Data.textures.smaaAreaTex);
			material.SetTexture(PostProcessPass.ShaderConstants._SearchTexture, this.m_Data.textures.smaaSearchTex);
			material.SetFloat(PostProcessPass.ShaderConstants._StencilRef, 64f);
			material.SetFloat(PostProcessPass.ShaderConstants._StencilMask, 64f);
			material.shaderKeywords = null;
			switch (*cameraData.antialiasingQuality)
			{
			case AntialiasingQuality.Low:
				material.EnableKeyword("_SMAA_PRESET_LOW");
				break;
			case AntialiasingQuality.Medium:
				material.EnableKeyword("_SMAA_PRESET_MEDIUM");
				break;
			case AntialiasingQuality.High:
				material.EnableKeyword("_SMAA_PRESET_HIGH");
				break;
			}
			RenderingUtils.Blit(cmd, source, pixelRect, this.m_EdgeColorTexture, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, this.m_EdgeStencilTexture, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, ClearFlag.ColorStencil, Color.clear, material, 0);
			RenderingUtils.Blit(cmd, this.m_EdgeColorTexture, pixelRect, this.m_BlendTexture, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, this.m_EdgeStencilTexture, RenderBufferLoadAction.Load, RenderBufferStoreAction.DontCare, ClearFlag.Color, Color.clear, material, 1);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._BlendTexture, this.m_BlendTexture.nameID);
			Blitter.BlitCameraTexture(cmd, source, destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, material, 2);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001A444 File Offset: 0x00018644
		private unsafe void DoDepthOfField(ref CameraData cameraData, CommandBuffer cmd, RTHandle source, RTHandle destination, Rect pixelRect)
		{
			if (this.m_DepthOfField.mode.value == DepthOfFieldMode.Gaussian)
			{
				this.DoGaussianDepthOfField(cmd, source, destination, pixelRect, *cameraData.isAlphaOutputEnabled);
				return;
			}
			if (this.m_DepthOfField.mode.value == DepthOfFieldMode.Bokeh)
			{
				this.DoBokehDepthOfField(cmd, source, destination, pixelRect, *cameraData.isAlphaOutputEnabled);
			}
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001A4A0 File Offset: 0x000186A0
		private void DoGaussianDepthOfField(CommandBuffer cmd, RTHandle source, RTHandle destination, Rect pixelRect, bool enableAlphaOutput)
		{
			int downSample = 2;
			Material material = this.m_Materials.gaussianDepthOfField;
			int wh = this.m_Descriptor.width / downSample;
			int hh = this.m_Descriptor.height / downSample;
			float farStart = this.m_DepthOfField.gaussianStart.value;
			float farEnd = Mathf.Max(farStart, this.m_DepthOfField.gaussianEnd.value);
			float maxRadius = this.m_DepthOfField.gaussianMaxRadius.value * ((float)wh / 1080f);
			maxRadius = Mathf.Min(maxRadius, 2f);
			CoreUtils.SetKeyword(material, "_ENABLE_ALPHA_OUTPUT", enableAlphaOutput);
			CoreUtils.SetKeyword(material, "_HIGH_QUALITY_SAMPLING", this.m_DepthOfField.highQualitySampling.value);
			material.SetVector(PostProcessPass.ShaderConstants._CoCParams, new Vector3(farStart, farEnd, maxRadius));
			RenderTextureDescriptor renderTextureDescriptor = this.GetCompatibleDescriptor(this.m_Descriptor.width, this.m_Descriptor.height, this.m_GaussianCoCFormat, GraphicsFormat.None);
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_FullCoCTexture, in renderTextureDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_FullCoCTexture");
			renderTextureDescriptor = this.GetCompatibleDescriptor(wh, hh, this.m_GaussianCoCFormat, GraphicsFormat.None);
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_HalfCoCTexture, in renderTextureDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_HalfCoCTexture");
			renderTextureDescriptor = this.GetCompatibleDescriptor(wh, hh, GraphicsFormat.R16G16B16A16_SFloat, GraphicsFormat.None);
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_PingTexture, in renderTextureDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_PingTexture");
			renderTextureDescriptor = this.GetCompatibleDescriptor(wh, hh, GraphicsFormat.R16G16B16A16_SFloat, GraphicsFormat.None);
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_PongTexture, in renderTextureDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_PongTexture");
			PostProcessUtils.SetSourceSize(cmd, this.m_FullCoCTexture);
			cmd.SetGlobalVector(PostProcessPass.ShaderConstants._DownSampleScaleFactor, new Vector4(1f / (float)downSample, 1f / (float)downSample, (float)downSample, (float)downSample));
			Blitter.BlitCameraTexture(cmd, source, this.m_FullCoCTexture, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, material, 0);
			this.m_MRT2[0] = this.m_HalfCoCTexture.nameID;
			this.m_MRT2[1] = this.m_PingTexture.nameID;
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._FullCoCTexture, this.m_FullCoCTexture.nameID);
			CoreUtils.SetRenderTarget(cmd, this.m_MRT2, this.m_HalfCoCTexture);
			Vector2 viewportScale = (source.useScaling ? new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : Vector2.one);
			Blitter.BlitTexture(cmd, source, viewportScale, material, 1);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._HalfCoCTexture, this.m_HalfCoCTexture.nameID);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._ColorTexture, source);
			Blitter.BlitCameraTexture(cmd, this.m_PingTexture, this.m_PongTexture, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, material, 2);
			Blitter.BlitCameraTexture(cmd, this.m_PongTexture, this.m_PingTexture, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, material, 3);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._ColorTexture, this.m_PingTexture.nameID);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._FullCoCTexture, this.m_FullCoCTexture.nameID);
			Blitter.BlitCameraTexture(cmd, source, destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, material, 4);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001A794 File Offset: 0x00018994
		private void PrepareBokehKernel(float maxRadius, float rcpAspect)
		{
			if (this.m_BokehKernel == null)
			{
				this.m_BokehKernel = new Vector4[42];
			}
			int idx = 0;
			float bladeCount = (float)this.m_DepthOfField.bladeCount.value;
			float curvature = 1f - this.m_DepthOfField.bladeCurvature.value;
			float rotation = this.m_DepthOfField.bladeRotation.value * 0.017453292f;
			for (int ring = 1; ring < 4; ring++)
			{
				float bias = 0.14285715f;
				float radius = ((float)ring + bias) / (3f + bias);
				int points = ring * 7;
				for (int point = 0; point < points; point++)
				{
					float phi = 6.2831855f * (float)point / (float)points;
					float nt = Mathf.Cos(3.1415927f / bladeCount);
					float dt = Mathf.Cos(phi - 6.2831855f / bladeCount * Mathf.Floor((bladeCount * phi + 3.1415927f) / 6.2831855f));
					float num = radius * Mathf.Pow(nt / dt, curvature);
					float u = num * Mathf.Cos(phi - rotation);
					float num2 = num * Mathf.Sin(phi - rotation);
					float uRadius = u * maxRadius;
					float vRadius = num2 * maxRadius;
					float num3 = uRadius * uRadius;
					float vRadiusPowTwo = vRadius * vRadius;
					float kernelLength = Mathf.Sqrt(num3 + vRadiusPowTwo);
					float uRCP = uRadius * rcpAspect;
					this.m_BokehKernel[idx] = new Vector4(uRadius, vRadius, kernelLength, uRCP);
					idx++;
				}
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001A8EF File Offset: 0x00018AEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float GetMaxBokehRadiusInPixels(float viewportHeight)
		{
			return Mathf.Min(0.05f, 14f / viewportHeight);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001A904 File Offset: 0x00018B04
		private void DoBokehDepthOfField(CommandBuffer cmd, RTHandle source, RTHandle destination, Rect pixelRect, bool enableAlphaOutput)
		{
			int downSample = 2;
			Material material = this.m_Materials.bokehDepthOfField;
			int wh = this.m_Descriptor.width / downSample;
			int hh = this.m_Descriptor.height / downSample;
			float F = this.m_DepthOfField.focalLength.value / 1000f;
			float num = this.m_DepthOfField.focalLength.value / this.m_DepthOfField.aperture.value;
			float P = this.m_DepthOfField.focusDistance.value;
			float maxCoC = num * F / (P - F);
			float maxRadius = PostProcessPass.GetMaxBokehRadiusInPixels((float)this.m_Descriptor.height);
			float rcpAspect = 1f / ((float)wh / (float)hh);
			CoreUtils.SetKeyword(material, "_ENABLE_ALPHA_OUTPUT", enableAlphaOutput);
			CoreUtils.SetKeyword(material, "_USE_FAST_SRGB_LINEAR_CONVERSION", this.m_UseFastSRGBLinearConversion);
			cmd.SetGlobalVector(PostProcessPass.ShaderConstants._CoCParams, new Vector4(P, maxCoC, maxRadius, rcpAspect));
			int hash = this.m_DepthOfField.GetHashCode();
			if (hash != this.m_BokehHash || maxRadius != this.m_BokehMaxRadius || rcpAspect != this.m_BokehRCPAspect)
			{
				this.m_BokehHash = hash;
				this.m_BokehMaxRadius = maxRadius;
				this.m_BokehRCPAspect = rcpAspect;
				this.PrepareBokehKernel(maxRadius, rcpAspect);
			}
			cmd.SetGlobalVectorArray(PostProcessPass.ShaderConstants._BokehKernel, this.m_BokehKernel);
			RenderTextureDescriptor renderTextureDescriptor = this.GetCompatibleDescriptor(this.m_Descriptor.width, this.m_Descriptor.height, GraphicsFormat.R8_UNorm, GraphicsFormat.None);
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_FullCoCTexture, in renderTextureDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_FullCoCTexture");
			renderTextureDescriptor = this.GetCompatibleDescriptor(wh, hh, GraphicsFormat.R16G16B16A16_SFloat, GraphicsFormat.None);
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_PingTexture, in renderTextureDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_PingTexture");
			renderTextureDescriptor = this.GetCompatibleDescriptor(wh, hh, GraphicsFormat.R16G16B16A16_SFloat, GraphicsFormat.None);
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_PongTexture, in renderTextureDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_PongTexture");
			PostProcessUtils.SetSourceSize(cmd, this.m_FullCoCTexture);
			cmd.SetGlobalVector(PostProcessPass.ShaderConstants._DownSampleScaleFactor, new Vector4(1f / (float)downSample, 1f / (float)downSample, (float)downSample, (float)downSample));
			float uvMargin = 1f / (float)this.m_Descriptor.height * (float)downSample;
			cmd.SetGlobalVector(PostProcessPass.ShaderConstants._BokehConstants, new Vector4(uvMargin, uvMargin * 2f));
			Blitter.BlitCameraTexture(cmd, source, this.m_FullCoCTexture, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, material, 0);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._FullCoCTexture, this.m_FullCoCTexture.nameID);
			Blitter.BlitCameraTexture(cmd, source, this.m_PingTexture, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, material, 1);
			Blitter.BlitCameraTexture(cmd, this.m_PingTexture, this.m_PongTexture, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, material, 2);
			Blitter.BlitCameraTexture(cmd, this.m_PongTexture, this.m_PingTexture, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, material, 3);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._DofTexture, this.m_PingTexture.nameID);
			Blitter.BlitCameraTexture(cmd, source, destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, material, 4);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001ABB8 File Offset: 0x00018DB8
		private static float GetLensFlareLightAttenuation(Light light, Camera cam, Vector3 wo)
		{
			if (!(light != null))
			{
				return 1f;
			}
			switch (light.type)
			{
			case LightType.Spot:
				return LensFlareCommonSRP.ShapeAttenuationSpotConeLight(light.transform.forward, wo, light.spotAngle, light.innerSpotAngle / 180f);
			case LightType.Directional:
				return LensFlareCommonSRP.ShapeAttenuationDirLight(light.transform.forward, cam.transform.forward);
			case LightType.Point:
				return LensFlareCommonSRP.ShapeAttenuationPointLight();
			default:
				return 1f;
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001AC3C File Offset: 0x00018E3C
		private void LensFlareDataDrivenComputeOcclusion(ref UniversalCameraData cameraData, CommandBuffer cmd, RenderTargetIdentifier source, bool usePanini, float paniniDistance, float paniniCropToFit)
		{
			if (!LensFlareCommonSRP.IsOcclusionRTCompatible())
			{
				return;
			}
			Camera camera = cameraData.camera;
			Matrix4x4 nonJitteredViewProjMatrix0;
			if (cameraData.xr.enabled)
			{
				if (cameraData.xr.singlePassEnabled)
				{
					nonJitteredViewProjMatrix0 = GL.GetGPUProjectionMatrix(cameraData.GetProjectionMatrixNoJitter(0), true) * cameraData.GetViewMatrix(0);
				}
				else
				{
					nonJitteredViewProjMatrix0 = GL.GetGPUProjectionMatrix(camera.projectionMatrix, true) * camera.worldToCameraMatrix;
					int multipassId = cameraData.xr.multipassId;
				}
			}
			else
			{
				nonJitteredViewProjMatrix0 = GL.GetGPUProjectionMatrix(cameraData.GetProjectionMatrixNoJitter(0), true) * cameraData.GetViewMatrix(0);
			}
			cmd.SetGlobalTexture(this.m_Depth.name, this.m_Depth.nameID);
			LensFlareCommonSRP.ComputeOcclusion(this.m_Materials.lensFlareDataDriven, camera, cameraData.xr, cameraData.xr.multipassId, (float)this.m_Descriptor.width, (float)this.m_Descriptor.height, usePanini, paniniDistance, paniniCropToFit, true, camera.transform.position, nonJitteredViewProjMatrix0, cmd, false, false, null, null);
			if (cameraData.xr.enabled && cameraData.xr.singlePassEnabled)
			{
				for (int xrIdx = 1; xrIdx < cameraData.xr.viewCount; xrIdx++)
				{
					Matrix4x4 gpuVPXR = GL.GetGPUProjectionMatrix(cameraData.GetProjectionMatrixNoJitter(xrIdx), true) * cameraData.GetViewMatrix(xrIdx);
					cmd.SetGlobalTexture(this.m_Depth.name, this.m_Depth.nameID);
					LensFlareCommonSRP.ComputeOcclusion(this.m_Materials.lensFlareDataDriven, camera, cameraData.xr, xrIdx, (float)this.m_Descriptor.width, (float)this.m_Descriptor.height, usePanini, paniniDistance, paniniCropToFit, true, camera.transform.position, gpuVPXR, cmd, false, false, null, null);
				}
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001AE08 File Offset: 0x00019008
		private void LensFlareDataDriven(ref UniversalCameraData cameraData, CommandBuffer cmd, RenderTargetIdentifier source, bool usePanini, float paniniDistance, float paniniCropToFit)
		{
			Camera camera = cameraData.camera;
			Rect pixelRect = new Rect(Vector2.zero, new Vector2((float)this.m_Descriptor.width, (float)this.m_Descriptor.height));
			if (!cameraData.xr.enabled || (cameraData.xr.enabled && !cameraData.xr.singlePassEnabled))
			{
				Matrix4x4 gpuVP = GL.GetGPUProjectionMatrix(camera.projectionMatrix, true) * camera.worldToCameraMatrix;
				LensFlareCommonSRP.DoLensFlareDataDrivenCommon(this.m_Materials.lensFlareDataDriven, camera, pixelRect, cameraData.xr, cameraData.xr.multipassId, (float)this.m_Descriptor.width, (float)this.m_Descriptor.height, usePanini, paniniDistance, paniniCropToFit, true, camera.transform.position, gpuVP, cmd, false, false, null, null, source, (Light light, Camera cam, Vector3 wo) => PostProcessPass.GetLensFlareLightAttenuation(light, cam, wo), false);
				return;
			}
			for (int xrIdx = 0; xrIdx < cameraData.xr.viewCount; xrIdx++)
			{
				Matrix4x4 gpuVPXR = GL.GetGPUProjectionMatrix(cameraData.GetProjectionMatrixNoJitter(xrIdx), true) * cameraData.GetViewMatrix(xrIdx);
				LensFlareCommonSRP.DoLensFlareDataDrivenCommon(this.m_Materials.lensFlareDataDriven, camera, pixelRect, cameraData.xr, cameraData.xr.multipassId, (float)this.m_Descriptor.width, (float)this.m_Descriptor.height, usePanini, paniniDistance, paniniCropToFit, true, camera.transform.position, gpuVPXR, cmd, false, false, null, null, source, (Light light, Camera cam, Vector3 wo) => PostProcessPass.GetLensFlareLightAttenuation(light, cam, wo), false);
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001AFB8 File Offset: 0x000191B8
		private void DoLensFlareScreenSpace(Camera camera, CommandBuffer cmd, RenderTargetIdentifier source, RTHandle originalBloomTexture, RTHandle screenSpaceLensFlareBloomMipTexture)
		{
			int ratio = (int)this.m_LensFlareScreenSpace.resolution.value;
			int width = Mathf.Max(1, this.m_Descriptor.width / ratio);
			int height = Mathf.Max(1, this.m_Descriptor.height / ratio);
			RenderTextureDescriptor desc = this.GetCompatibleDescriptor(width, height, this.m_DefaultColorFormat, GraphicsFormat.None);
			if (this.m_LensFlareScreenSpace.IsStreaksActive())
			{
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_StreakTmpTexture, in desc, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_StreakTmpTexture");
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_StreakTmpTexture2, in desc, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_StreakTmpTexture2");
			}
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_ScreenSpaceLensFlareResult, in desc, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_ScreenSpaceLensFlareResult");
			LensFlareCommonSRP.DoLensFlareScreenSpaceCommon(this.m_Materials.lensFlareScreenSpace, camera, (float)this.m_Descriptor.width, (float)this.m_Descriptor.height, this.m_LensFlareScreenSpace.tintColor.value, originalBloomTexture, screenSpaceLensFlareBloomMipTexture, null, this.m_StreakTmpTexture, this.m_StreakTmpTexture2, new Vector4(this.m_LensFlareScreenSpace.intensity.value, this.m_LensFlareScreenSpace.firstFlareIntensity.value, this.m_LensFlareScreenSpace.secondaryFlareIntensity.value, this.m_LensFlareScreenSpace.warpedFlareIntensity.value), new Vector4(this.m_LensFlareScreenSpace.vignetteEffect.value, this.m_LensFlareScreenSpace.startingPosition.value, this.m_LensFlareScreenSpace.scale.value, 0f), new Vector4((float)this.m_LensFlareScreenSpace.samples.value, this.m_LensFlareScreenSpace.sampleDimmer.value, this.m_LensFlareScreenSpace.chromaticAbberationIntensity.value, 0f), new Vector4(this.m_LensFlareScreenSpace.streaksIntensity.value, this.m_LensFlareScreenSpace.streaksLength.value, this.m_LensFlareScreenSpace.streaksOrientation.value, this.m_LensFlareScreenSpace.streaksThreshold.value), new Vector4((float)ratio, this.m_LensFlareScreenSpace.warpedFlareScale.value.x, this.m_LensFlareScreenSpace.warpedFlareScale.value.y, 0f), cmd, this.m_ScreenSpaceLensFlareResult, false);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._Bloom_Texture, originalBloomTexture);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001B21C File Offset: 0x0001941C
		internal static void UpdateMotionBlurMatrices(ref Material material, Camera camera, XRPass xr)
		{
			MotionVectorsPersistentData motionData = null;
			UniversalAdditionalCameraData additionalCameraData;
			if (camera.TryGetComponent<UniversalAdditionalCameraData>(out additionalCameraData))
			{
				motionData = additionalCameraData.motionVectorsPersistentData;
			}
			if (motionData == null)
			{
				return;
			}
			if (xr.enabled && xr.singlePassEnabled)
			{
				material.SetMatrixArray(PostProcessPass.k_ShaderPropertyId_PrevViewProjMStereo, motionData.previousViewProjectionStereo);
				material.SetMatrixArray(PostProcessPass.k_ShaderPropertyId_ViewProjMStereo, motionData.viewProjectionStereo);
				return;
			}
			int viewProjMIdx = 0;
			if (xr.enabled)
			{
				viewProjMIdx = xr.multipassId;
			}
			material.SetMatrix(PostProcessPass.k_ShaderPropertyId_PrevViewProjM, motionData.previousViewProjectionStereo[viewProjMIdx]);
			material.SetMatrix(PostProcessPass.k_ShaderPropertyId_ViewProjM, motionData.viewProjectionStereo[viewProjMIdx]);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001B2B8 File Offset: 0x000194B8
		private unsafe void DoMotionBlur(CommandBuffer cmd, RTHandle source, RTHandle destination, RTHandle motionVectors, ref CameraData cameraData)
		{
			Material material = this.m_Materials.cameraMotionBlur;
			PostProcessPass.UpdateMotionBlurMatrices(ref material, *cameraData.camera, cameraData.xr);
			material.SetFloat("_Intensity", this.m_MotionBlur.intensity.value);
			material.SetFloat("_Clamp", this.m_MotionBlur.clamp.value);
			int pass = (int)this.m_MotionBlur.quality.value;
			if (this.m_MotionBlur.mode.value == MotionBlurMode.CameraAndObjects)
			{
				pass += 3;
				material.SetTexture("_MotionVectorTexture", motionVectors);
			}
			PostProcessUtils.SetSourceSize(cmd, source);
			CoreUtils.SetKeyword(material, "_ENABLE_ALPHA_OUTPUT", *cameraData.isAlphaOutputEnabled);
			Blitter.BlitCameraTexture(cmd, source, destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, material, pass);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001B380 File Offset: 0x00019580
		private void DoPaniniProjection(Camera camera, CommandBuffer cmd, RTHandle source, RTHandle destination)
		{
			float distance = this.m_PaniniProjection.distance.value;
			Vector2 viewExtents = this.CalcViewExtents(camera);
			Vector2 vector = this.CalcCropExtents(camera, distance);
			float scaleX = vector.x / viewExtents.x;
			float scaleY = vector.y / viewExtents.y;
			float scaleF = Mathf.Min(scaleX, scaleY);
			float paniniD = distance;
			float paniniS = Mathf.Lerp(1f, Mathf.Clamp01(scaleF), this.m_PaniniProjection.cropToFit.value);
			Material material = this.m_Materials.paniniProjection;
			material.SetVector(PostProcessPass.ShaderConstants._Params, new Vector4(viewExtents.x, viewExtents.y, paniniD, paniniS));
			material.EnableKeyword((1f - Mathf.Abs(paniniD) > float.Epsilon) ? "_GENERIC" : "_UNIT_DISTANCE");
			Blitter.BlitCameraTexture(cmd, source, destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, material, 0);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001B45C File Offset: 0x0001965C
		private Vector2 CalcViewExtents(Camera camera)
		{
			float fovY = camera.fieldOfView * 0.017453292f;
			float num = (float)this.m_Descriptor.width / (float)this.m_Descriptor.height;
			float viewExtY = Mathf.Tan(0.5f * fovY);
			return new Vector2(num * viewExtY, viewExtY);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001B4A4 File Offset: 0x000196A4
		private Vector2 CalcCropExtents(Camera camera, float d)
		{
			float viewDist = 1f + d;
			Vector2 projPos = this.CalcViewExtents(camera);
			float projHyp = Mathf.Sqrt(projPos.x * projPos.x + 1f);
			float cylDistMinusD = 1f / projHyp;
			float cylDist = cylDistMinusD + d;
			return projPos * cylDistMinusD * (viewDist / cylDist);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001B4F8 File Offset: 0x000196F8
		private void SetupBloom(CommandBuffer cmd, RTHandle source, Material uberMaterial, bool enableAlphaOutput)
		{
			BloomDownscaleMode value = this.m_Bloom.downscale.value;
			int downres;
			if (value != BloomDownscaleMode.Half)
			{
				if (value != BloomDownscaleMode.Quarter)
				{
					throw new ArgumentOutOfRangeException();
				}
				downres = 2;
			}
			else
			{
				downres = 1;
			}
			int tw = this.m_Descriptor.width >> downres;
			int th = this.m_Descriptor.height >> downres;
			int mipCount = Mathf.Clamp(Mathf.FloorToInt(Mathf.Log((float)Mathf.Max(tw, th), 2f) - 1f), 1, this.m_Bloom.maxIterations.value);
			float clamp = this.m_Bloom.clamp.value;
			float threshold = Mathf.GammaToLinearSpace(this.m_Bloom.threshold.value);
			float thresholdKnee = threshold * 0.5f;
			float scatter = Mathf.Lerp(0.05f, 0.95f, this.m_Bloom.scatter.value);
			Material bloomMaterial = this.m_Materials.bloom;
			bloomMaterial.SetVector(PostProcessPass.ShaderConstants._Params, new Vector4(scatter, clamp, threshold, thresholdKnee));
			CoreUtils.SetKeyword(bloomMaterial, "_BLOOM_HQ", this.m_Bloom.highQualityFiltering.value);
			CoreUtils.SetKeyword(bloomMaterial, "_ENABLE_ALPHA_OUTPUT", enableAlphaOutput);
			RenderTextureDescriptor desc = this.GetCompatibleDescriptor(tw, th, this.m_DefaultColorFormat, GraphicsFormat.None);
			for (int i = 0; i < mipCount; i++)
			{
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_BloomMipUp[i], in desc, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, this.m_BloomMipUp[i].name);
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_BloomMipDown[i], in desc, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, this.m_BloomMipDown[i].name);
				desc.width = Mathf.Max(1, desc.width >> 1);
				desc.height = Mathf.Max(1, desc.height >> 1);
			}
			Blitter.BlitCameraTexture(cmd, source, this.m_BloomMipDown[0], RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, bloomMaterial, 0);
			RTHandle lastDown = this.m_BloomMipDown[0];
			for (int j = 1; j < mipCount; j++)
			{
				Blitter.BlitCameraTexture(cmd, lastDown, this.m_BloomMipUp[j], RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, bloomMaterial, 1);
				Blitter.BlitCameraTexture(cmd, this.m_BloomMipUp[j], this.m_BloomMipDown[j], RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, bloomMaterial, 2);
				lastDown = this.m_BloomMipDown[j];
			}
			for (int k = mipCount - 2; k >= 0; k--)
			{
				RTHandle lowMip = ((k == mipCount - 2) ? this.m_BloomMipDown[k + 1] : this.m_BloomMipUp[k + 1]);
				RTHandle highMip = this.m_BloomMipDown[k];
				RTHandle dst = this.m_BloomMipUp[k];
				cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._SourceTexLowMip, lowMip);
				Blitter.BlitCameraTexture(cmd, highMip, dst, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, bloomMaterial, 3);
			}
			Color tint = this.m_Bloom.tint.value.linear;
			float luma = ColorUtils.Luminance(in tint);
			tint = ((luma > 0f) ? (tint * (1f / luma)) : Color.white);
			Vector4 bloomParams = new Vector4(this.m_Bloom.intensity.value, tint.r, tint.g, tint.b);
			uberMaterial.SetVector(PostProcessPass.ShaderConstants._Bloom_Params, bloomParams);
			cmd.SetGlobalTexture(PostProcessPass.ShaderConstants._Bloom_Texture, this.m_BloomMipUp[0]);
			Texture dirtTexture = ((this.m_Bloom.dirtTexture.value == null) ? Texture2D.blackTexture : this.m_Bloom.dirtTexture.value);
			float dirtRatio = (float)dirtTexture.width / (float)dirtTexture.height;
			float screenRatio = (float)this.m_Descriptor.width / (float)this.m_Descriptor.height;
			Vector4 dirtScaleOffset = new Vector4(1f, 1f, 0f, 0f);
			float dirtIntensity = this.m_Bloom.dirtIntensity.value;
			if (dirtRatio > screenRatio)
			{
				dirtScaleOffset.x = screenRatio / dirtRatio;
				dirtScaleOffset.z = (1f - dirtScaleOffset.x) * 0.5f;
			}
			else if (screenRatio > dirtRatio)
			{
				dirtScaleOffset.y = dirtRatio / screenRatio;
				dirtScaleOffset.w = (1f - dirtScaleOffset.y) * 0.5f;
			}
			uberMaterial.SetVector(PostProcessPass.ShaderConstants._LensDirt_Params, dirtScaleOffset);
			uberMaterial.SetFloat(PostProcessPass.ShaderConstants._LensDirt_Intensity, dirtIntensity);
			uberMaterial.SetTexture(PostProcessPass.ShaderConstants._LensDirt_Texture, dirtTexture);
			if (this.m_Bloom.highQualityFiltering.value)
			{
				uberMaterial.EnableKeyword((dirtIntensity > 0f) ? "_BLOOM_HQ_DIRT" : "_BLOOM_HQ");
				return;
			}
			uberMaterial.EnableKeyword((dirtIntensity > 0f) ? "_BLOOM_LQ_DIRT" : "_BLOOM_LQ");
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001B998 File Offset: 0x00019B98
		private void SetupLensDistortion(Material material, bool isSceneView)
		{
			float amount = 1.6f * Mathf.Max(Mathf.Abs(this.m_LensDistortion.intensity.value * 100f), 1f);
			float theta = 0.017453292f * Mathf.Min(160f, amount);
			float sigma = 2f * Mathf.Tan(theta * 0.5f);
			Vector2 center = this.m_LensDistortion.center.value * 2f - Vector2.one;
			Vector4 p = new Vector4(center.x, center.y, Mathf.Max(this.m_LensDistortion.xMultiplier.value, 0.0001f), Mathf.Max(this.m_LensDistortion.yMultiplier.value, 0.0001f));
			Vector4 p2 = new Vector4((this.m_LensDistortion.intensity.value >= 0f) ? theta : (1f / theta), sigma, 1f / this.m_LensDistortion.scale.value, this.m_LensDistortion.intensity.value * 100f);
			material.SetVector(PostProcessPass.ShaderConstants._Distortion_Params1, p);
			material.SetVector(PostProcessPass.ShaderConstants._Distortion_Params2, p2);
			if (this.m_LensDistortion.IsActive() && !isSceneView)
			{
				material.EnableKeyword("_DISTORTION");
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001BAEC File Offset: 0x00019CEC
		private void SetupChromaticAberration(Material material)
		{
			material.SetFloat(PostProcessPass.ShaderConstants._Chroma_Params, this.m_ChromaticAberration.intensity.value * 0.05f);
			if (this.m_ChromaticAberration.IsActive())
			{
				material.EnableKeyword("_CHROMATIC_ABERRATION");
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001BB28 File Offset: 0x00019D28
		private void SetupVignette(Material material, XRPass xrPass)
		{
			Color color = this.m_Vignette.color.value;
			Vector2 center = this.m_Vignette.center.value;
			float aspectRatio = (float)this.m_Descriptor.width / (float)this.m_Descriptor.height;
			if (xrPass != null && xrPass.enabled)
			{
				if (xrPass.singlePassEnabled)
				{
					material.SetVector(PostProcessPass.ShaderConstants._Vignette_ParamsXR, xrPass.ApplyXRViewCenterOffset(center));
				}
				else
				{
					center = xrPass.ApplyXRViewCenterOffset(center);
				}
			}
			Vector4 v = new Vector4(color.r, color.g, color.b, this.m_Vignette.rounded.value ? aspectRatio : 1f);
			Vector4 v2 = new Vector4(center.x, center.y, this.m_Vignette.intensity.value * 3f, this.m_Vignette.smoothness.value * 5f);
			material.SetVector(PostProcessPass.ShaderConstants._Vignette_Params1, v);
			material.SetVector(PostProcessPass.ShaderConstants._Vignette_Params2, v2);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001BC30 File Offset: 0x00019E30
		private unsafe void SetupColorGrading(CommandBuffer cmd, ref RenderingData renderingData, Material material)
		{
			bool hdr = *renderingData.postProcessingData.gradingMode == ColorGradingMode.HighDynamicRange;
			int lutHeight = *renderingData.postProcessingData.lutSize;
			int lutWidth = lutHeight * lutHeight;
			float postExposureLinear = Mathf.Pow(2f, this.m_ColorAdjustments.postExposure.value);
			material.SetTexture(PostProcessPass.ShaderConstants._InternalLut, this.m_InternalLut);
			material.SetVector(PostProcessPass.ShaderConstants._Lut_Params, new Vector4(1f / (float)lutWidth, 1f / (float)lutHeight, (float)lutHeight - 1f, postExposureLinear));
			material.SetTexture(PostProcessPass.ShaderConstants._UserLut, this.m_ColorLookup.texture.value);
			material.SetVector(PostProcessPass.ShaderConstants._UserLut_Params, (!this.m_ColorLookup.IsActive()) ? Vector4.zero : new Vector4(1f / (float)this.m_ColorLookup.texture.value.width, 1f / (float)this.m_ColorLookup.texture.value.height, (float)this.m_ColorLookup.texture.value.height - 1f, this.m_ColorLookup.contribution.value));
			if (hdr)
			{
				material.EnableKeyword("_HDR_GRADING");
				return;
			}
			TonemappingMode value = this.m_Tonemapping.mode.value;
			if (value == TonemappingMode.Neutral)
			{
				material.EnableKeyword("_TONEMAP_NEUTRAL");
				return;
			}
			if (value != TonemappingMode.ACES)
			{
				return;
			}
			material.EnableKeyword("_TONEMAP_ACES");
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001BD99 File Offset: 0x00019F99
		private void SetupGrain(UniversalCameraData cameraData, Material material)
		{
			if (!this.m_HasFinalPass && this.m_FilmGrain.IsActive())
			{
				material.EnableKeyword("_FILM_GRAIN");
				PostProcessUtils.ConfigureFilmGrain(this.m_Data, this.m_FilmGrain, cameraData.pixelWidth, cameraData.pixelHeight, material);
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001BDDC File Offset: 0x00019FDC
		private void SetupDithering(UniversalCameraData cameraData, Material material)
		{
			if (!this.m_HasFinalPass && cameraData.isDitheringEnabled)
			{
				material.EnableKeyword("_DITHERING");
				this.m_DitheringTextureIndex = PostProcessUtils.ConfigureDithering(this.m_Data, this.m_DitheringTextureIndex, cameraData.pixelWidth, cameraData.pixelHeight, material);
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001BE28 File Offset: 0x0001A028
		private void SetupHDROutput(HDROutputUtils.HDRDisplayInformation hdrDisplayInformation, ColorGamut hdrDisplayColorGamut, Material material, HDROutputUtils.Operation hdrOperations, bool rendersOverlayUI)
		{
			Vector4 hdrOutputLuminanceParams;
			UniversalRenderPipeline.GetHDROutputLuminanceParameters(hdrDisplayInformation, hdrDisplayColorGamut, this.m_Tonemapping, out hdrOutputLuminanceParams);
			material.SetVector(ShaderPropertyId.hdrOutputLuminanceParams, hdrOutputLuminanceParams);
			HDROutputUtils.ConfigureHDROutput(material, hdrDisplayColorGamut, hdrOperations);
			CoreUtils.SetKeyword(this.m_Materials.uber, "_HDR_OVERLAY", rendersOverlayUI);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001BE70 File Offset: 0x0001A070
		private unsafe void RenderFinalPass(CommandBuffer cmd, ref RenderingData renderingData)
		{
			UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
			Material material = this.m_Materials.finalPass;
			material.shaderKeywords = null;
			PostProcessUtils.SetSourceSize(cmd, cameraData.renderer.cameraColorTargetHandle);
			this.SetupGrain(renderingData.cameraData.universalCameraData, material);
			this.SetupDithering(renderingData.cameraData.universalCameraData, material);
			if (this.RequireSRGBConversionBlitToBackBuffer(renderingData.cameraData.requireSrgbConversion))
			{
				material.EnableKeyword("_LINEAR_TO_SRGB_CONVERSION");
			}
			HDROutputUtils.Operation hdrOperations = HDROutputUtils.Operation.None;
			bool requireHDROutput = this.RequireHDROutput(renderingData.cameraData.universalCameraData);
			if (requireHDROutput)
			{
				hdrOperations = (this.m_EnableColorEncodingIfNeeded ? HDROutputUtils.Operation.ColorEncoding : HDROutputUtils.Operation.None);
				if (!cameraData.postProcessEnabled)
				{
					hdrOperations |= HDROutputUtils.Operation.ColorConversion;
				}
				this.SetupHDROutput(cameraData.hdrDisplayInformation, cameraData.hdrDisplayColorGamut, material, hdrOperations, cameraData.rendersOverlayUI);
			}
			CoreUtils.SetKeyword(material, "_ENABLE_ALPHA_OUTPUT", cameraData.isAlphaOutputEnabled);
			DebugHandler debugHandler = ScriptableRenderPass.GetActiveDebugHandler(cameraData);
			bool resolveToDebugScreen = debugHandler != null && debugHandler.WriteToDebugScreenTexture(cameraData.resolveFinalTarget);
			if (this.m_UseSwapBuffer)
			{
				this.m_Source = cameraData.renderer.GetCameraColorBackBuffer(cmd);
			}
			RTHandle sourceTex = this.m_Source;
			RenderBufferLoadAction colorLoadAction = (cameraData.isDefaultViewport ? RenderBufferLoadAction.DontCare : RenderBufferLoadAction.Load);
			bool isFxaaEnabled = cameraData.antialiasing == AntialiasingMode.FastApproximateAntialiasing;
			bool isFsrEnabled = cameraData.imageScalingMode == ImageScalingMode.Upscaling && cameraData.upscalingFilter == ImageUpscalingFilter.FSR;
			bool isTaaSharpeningEnabled = cameraData.IsTemporalAAEnabled() && cameraData.taaSettings.contrastAdaptiveSharpening > 0f && !isFsrEnabled;
			bool isAlphaOutputEnabled = cameraData.isAlphaOutputEnabled;
			if (cameraData.imageScalingMode != ImageScalingMode.None)
			{
				bool flag = isFxaaEnabled || isFsrEnabled;
				RenderTextureDescriptor tempRtDesc = cameraData.cameraTargetDescriptor;
				tempRtDesc.msaaSamples = 1;
				tempRtDesc.depthStencilFormat = GraphicsFormat.None;
				if (!requireHDROutput)
				{
					tempRtDesc.graphicsFormat = UniversalRenderPipeline.MakeUnormRenderTextureGraphicsFormat();
				}
				this.m_Materials.scalingSetup.shaderKeywords = null;
				if (flag)
				{
					if (requireHDROutput)
					{
						this.SetupHDROutput(cameraData.hdrDisplayInformation, cameraData.hdrDisplayColorGamut, this.m_Materials.scalingSetup, hdrOperations, cameraData.rendersOverlayUI);
					}
					if (isFxaaEnabled)
					{
						this.m_Materials.scalingSetup.EnableKeyword("_FXAA");
					}
					if (isFsrEnabled)
					{
						this.m_Materials.scalingSetup.EnableKeyword(hdrOperations.HasFlag(HDROutputUtils.Operation.ColorEncoding) ? "_GAMMA_20_AND_HDR_INPUT" : "_GAMMA_20");
					}
					if (isAlphaOutputEnabled)
					{
						this.m_Materials.scalingSetup.EnableKeyword("_ENABLE_ALPHA_OUTPUT");
					}
					RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_ScalingSetupTarget, in tempRtDesc, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, "_ScalingSetupTexture");
					Blitter.BlitCameraTexture(cmd, this.m_Source, this.m_ScalingSetupTarget, colorLoadAction, RenderBufferStoreAction.Store, this.m_Materials.scalingSetup, 0);
					sourceTex = this.m_ScalingSetupTarget;
				}
				ImageScalingMode imageScalingMode = cameraData.imageScalingMode;
				if (imageScalingMode != ImageScalingMode.Upscaling)
				{
					if (imageScalingMode == ImageScalingMode.Downscaling)
					{
						isTaaSharpeningEnabled = false;
					}
				}
				else
				{
					switch (cameraData.upscalingFilter)
					{
					case ImageUpscalingFilter.Point:
						if (!isTaaSharpeningEnabled)
						{
							material.EnableKeyword("_POINT_SAMPLING");
						}
						break;
					case ImageUpscalingFilter.FSR:
					{
						this.m_Materials.easu.shaderKeywords = null;
						RenderTextureDescriptor upscaleRtDesc = cameraData.cameraTargetDescriptor;
						upscaleRtDesc.msaaSamples = 1;
						upscaleRtDesc.depthStencilFormat = GraphicsFormat.None;
						upscaleRtDesc.width = cameraData.pixelWidth;
						upscaleRtDesc.height = cameraData.pixelHeight;
						RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_UpscaledTarget, in upscaleRtDesc, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, "_UpscaledTexture");
						Vector2 fsrInputSize = new Vector2((float)cameraData.cameraTargetDescriptor.width, (float)cameraData.cameraTargetDescriptor.height);
						Vector2 fsrOutputSize = new Vector2((float)cameraData.pixelWidth, (float)cameraData.pixelHeight);
						FSRUtils.SetEasuConstants(cmd, fsrInputSize, fsrInputSize, fsrOutputSize);
						Blitter.BlitCameraTexture(cmd, sourceTex, this.m_UpscaledTarget, colorLoadAction, RenderBufferStoreAction.Store, this.m_Materials.easu, 0);
						float sharpness = (cameraData.fsrOverrideSharpness ? cameraData.fsrSharpness : 0.92f);
						if (cameraData.fsrSharpness > 0f)
						{
							material.EnableKeyword(requireHDROutput ? "_EASU_RCAS_AND_HDR_INPUT" : "_RCAS");
							FSRUtils.SetRcasConstantsLinear(cmd, sharpness);
						}
						sourceTex = this.m_UpscaledTarget;
						PostProcessUtils.SetSourceSize(cmd, this.m_UpscaledTarget);
						break;
					}
					}
				}
			}
			else if (isFxaaEnabled)
			{
				material.EnableKeyword("_FXAA");
			}
			if (isTaaSharpeningEnabled)
			{
				material.EnableKeyword("_RCAS");
				FSRUtils.SetRcasConstantsLinear(cmd, cameraData.taaSettings.contrastAdaptiveSharpening);
			}
			RenderTargetIdentifier cameraTarget = RenderingUtils.GetCameraTargetIdentifier(ref renderingData);
			if (resolveToDebugScreen)
			{
				Blitter.BlitCameraTexture(cmd, sourceTex, *debugHandler.DebugScreenColorHandle, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, material, 0);
				cameraData.renderer.ConfigureCameraTarget(*debugHandler.DebugScreenColorHandle, *debugHandler.DebugScreenDepthHandle);
				return;
			}
			RTHandleStaticHelpers.SetRTHandleStaticWrapper(cameraTarget);
			RTHandle cameraTargetHandle = RTHandleStaticHelpers.s_RTHandleWrapper;
			RenderingUtils.FinalBlit(cmd, cameraData, sourceTex, cameraTargetHandle, colorLoadAction, RenderBufferStoreAction.Store, material, 0);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001C2F4 File Offset: 0x0001A4F4
		private void UpdateCameraResolution(RenderGraph renderGraph, UniversalCameraData cameraData, Vector2Int newCameraTargetSize)
		{
			this.m_Descriptor.width = newCameraTargetSize.x;
			this.m_Descriptor.height = newCameraTargetSize.y;
			cameraData.cameraTargetDescriptor.width = newCameraTargetSize.x;
			cameraData.cameraTargetDescriptor.height = newCameraTargetSize.y;
			PostProcessPass.UpdateCameraResolutionPassData passData;
			using (IUnsafeRenderGraphBuilder builder = renderGraph.AddUnsafePass<PostProcessPass.UpdateCameraResolutionPassData>("Update Camera Resolution", out passData, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 28))
			{
				passData.newCameraTargetSize = newCameraTargetSize;
				builder.AllowGlobalStateModification(true);
				builder.AllowPassCulling(false);
				builder.SetRenderFunc<PostProcessPass.UpdateCameraResolutionPassData>(delegate(PostProcessPass.UpdateCameraResolutionPassData data, UnsafeGraphContext ctx)
				{
					ctx.cmd.SetGlobalVector(ShaderPropertyId.screenSize, new Vector4((float)data.newCameraTargetSize.x, (float)data.newCameraTargetSize.y, 1f / (float)data.newCameraTargetSize.x, 1f / (float)data.newCameraTargetSize.y));
				});
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001C3B4 File Offset: 0x0001A5B4
		public void RenderStopNaN(RenderGraph renderGraph, RenderTextureDescriptor cameraTargetDescriptor, in TextureHandle activeCameraColor, out TextureHandle stopNaNTarget)
		{
			RenderTextureDescriptor desc = PostProcessPass.GetCompatibleDescriptor(cameraTargetDescriptor, cameraTargetDescriptor.width, cameraTargetDescriptor.height, cameraTargetDescriptor.graphicsFormat, GraphicsFormat.None);
			stopNaNTarget = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, "_StopNaNsTarget", true, FilterMode.Bilinear, TextureWrapMode.Clamp);
			PostProcessPass.StopNaNsPassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<PostProcessPass.StopNaNsPassData>("Stop NaNs", out passData, ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_StopNaNs), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 69))
			{
				passData.stopNaNTarget = stopNaNTarget;
				builder.SetRenderAttachment(stopNaNTarget, 0, AccessFlags.ReadWrite);
				passData.sourceTexture = activeCameraColor;
				builder.UseTexture(in activeCameraColor, AccessFlags.Read);
				passData.stopNaN = this.m_Materials.stopNaN;
				builder.SetRenderFunc<PostProcessPass.StopNaNsPassData>(delegate(PostProcessPass.StopNaNsPassData data, RasterGraphContext context)
				{
					RasterCommandBuffer cmd = context.cmd;
					RTHandle sourceTextureHdl = data.sourceTexture;
					Vector2 viewportScale = (sourceTextureHdl.useScaling ? new Vector2(sourceTextureHdl.rtHandleProperties.rtHandleScale.x, sourceTextureHdl.rtHandleProperties.rtHandleScale.y) : Vector2.one);
					Blitter.BlitTexture(cmd, sourceTextureHdl, viewportScale, data.stopNaN, 0);
				});
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001C494 File Offset: 0x0001A694
		public void RenderSMAA(RenderGraph renderGraph, UniversalResourceData resourceData, AntialiasingQuality antialiasingQuality, in TextureHandle source, out TextureHandle SMAATarget)
		{
			RenderTextureDescriptor desc = PostProcessPass.GetCompatibleDescriptor(this.m_Descriptor, this.m_Descriptor.width, this.m_Descriptor.height, this.m_Descriptor.graphicsFormat, GraphicsFormat.None);
			SMAATarget = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, "_SMAATarget", true, FilterMode.Bilinear, TextureWrapMode.Clamp);
			RenderTextureDescriptor edgeTextureDesc = PostProcessPass.GetCompatibleDescriptor(this.m_Descriptor, this.m_Descriptor.width, this.m_Descriptor.height, this.m_SMAAEdgeFormat, GraphicsFormat.None);
			TextureHandle edgeTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, edgeTextureDesc, "_EdgeStencilTexture", true, FilterMode.Bilinear, TextureWrapMode.Clamp);
			RenderTextureDescriptor edgeTextureStencilDesc = PostProcessPass.GetCompatibleDescriptor(this.m_Descriptor, this.m_Descriptor.width, this.m_Descriptor.height, GraphicsFormat.None, GraphicsFormatUtility.GetDepthStencilFormat(24));
			TextureHandle edgeTextureStencil = UniversalRenderer.CreateRenderGraphTexture(renderGraph, edgeTextureStencilDesc, "_EdgeTexture", true, FilterMode.Bilinear, TextureWrapMode.Clamp);
			RenderTextureDescriptor blendTextureDesc = PostProcessPass.GetCompatibleDescriptor(this.m_Descriptor, this.m_Descriptor.width, this.m_Descriptor.height, GraphicsFormat.R8G8B8A8_UNorm, GraphicsFormat.None);
			TextureHandle blendTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, blendTextureDesc, "_BlendTexture", true, FilterMode.Point, TextureWrapMode.Clamp);
			Material material = this.m_Materials.subpixelMorphologicalAntialiasing;
			PostProcessPass.SMAASetupPassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<PostProcessPass.SMAASetupPassData>("SMAA Material Setup", out passData, ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_SMAAMaterialSetup), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 143))
			{
				passData.metrics = new Vector4(1f / (float)this.m_Descriptor.width, 1f / (float)this.m_Descriptor.height, (float)this.m_Descriptor.width, (float)this.m_Descriptor.height);
				passData.areaTexture = this.m_Data.textures.smaaAreaTex;
				passData.searchTexture = this.m_Data.textures.smaaSearchTex;
				passData.stencilRef = 64f;
				passData.stencilMask = 64f;
				passData.antialiasingQuality = antialiasingQuality;
				passData.material = material;
				builder.AllowPassCulling(false);
				builder.SetRenderFunc<PostProcessPass.SMAASetupPassData>(delegate(PostProcessPass.SMAASetupPassData data, RasterGraphContext context)
				{
					data.material.SetVector(PostProcessPass.ShaderConstants._Metrics, data.metrics);
					data.material.SetTexture(PostProcessPass.ShaderConstants._AreaTexture, data.areaTexture);
					data.material.SetTexture(PostProcessPass.ShaderConstants._SearchTexture, data.searchTexture);
					data.material.SetFloat(PostProcessPass.ShaderConstants._StencilRef, data.stencilRef);
					data.material.SetFloat(PostProcessPass.ShaderConstants._StencilMask, data.stencilMask);
					data.material.shaderKeywords = null;
					switch (data.antialiasingQuality)
					{
					case AntialiasingQuality.Low:
						data.material.EnableKeyword("_SMAA_PRESET_LOW");
						return;
					case AntialiasingQuality.Medium:
						data.material.EnableKeyword("_SMAA_PRESET_MEDIUM");
						return;
					case AntialiasingQuality.High:
						data.material.EnableKeyword("_SMAA_PRESET_HIGH");
						return;
					default:
						return;
					}
				});
			}
			PostProcessPass.SMAAPassData passData2;
			using (IRasterRenderGraphBuilder builder2 = renderGraph.AddRasterRenderPass<PostProcessPass.SMAAPassData>("SMAA Edge Detection", out passData2, ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_SMAAEdgeDetection), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 184))
			{
				passData2.destinationTexture = edgeTexture;
				builder2.SetRenderAttachment(edgeTexture, 0, AccessFlags.Write);
				passData2.depthStencilTexture = edgeTextureStencil;
				builder2.SetRenderAttachmentDepth(edgeTextureStencil, AccessFlags.Write);
				passData2.sourceTexture = source;
				builder2.UseTexture(in source, AccessFlags.Read);
				IBaseRenderGraphBuilder baseRenderGraphBuilder = builder2;
				TextureHandle cameraDepth = resourceData.cameraDepth;
				baseRenderGraphBuilder.UseTexture(in cameraDepth, AccessFlags.Read);
				passData2.material = material;
				builder2.SetRenderFunc<PostProcessPass.SMAAPassData>(delegate(PostProcessPass.SMAAPassData data, RasterGraphContext context)
				{
					Material SMAAMaterial = data.material;
					RasterCommandBuffer cmd = context.cmd;
					RTHandle sourceTextureHdl = data.sourceTexture;
					Vector2 viewportScale = (sourceTextureHdl.useScaling ? new Vector2(sourceTextureHdl.rtHandleProperties.rtHandleScale.x, sourceTextureHdl.rtHandleProperties.rtHandleScale.y) : Vector2.one);
					Blitter.BlitTexture(cmd, sourceTextureHdl, viewportScale, SMAAMaterial, 0);
				});
			}
			PostProcessPass.SMAAPassData passData3;
			using (IRasterRenderGraphBuilder builder3 = renderGraph.AddRasterRenderPass<PostProcessPass.SMAAPassData>("SMAA Blend weights", out passData3, ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_SMAABlendWeight), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 207))
			{
				passData3.destinationTexture = blendTexture;
				builder3.SetRenderAttachment(blendTexture, 0, AccessFlags.Write);
				passData3.depthStencilTexture = edgeTextureStencil;
				builder3.SetRenderAttachmentDepth(edgeTextureStencil, AccessFlags.Read);
				passData3.sourceTexture = edgeTexture;
				builder3.UseTexture(in edgeTexture, AccessFlags.Read);
				passData3.material = material;
				builder3.SetRenderFunc<PostProcessPass.SMAAPassData>(delegate(PostProcessPass.SMAAPassData data, RasterGraphContext context)
				{
					Material SMAAMaterial2 = data.material;
					RasterCommandBuffer cmd2 = context.cmd;
					RTHandle sourceTextureHdl2 = data.sourceTexture;
					Vector2 viewportScale2 = (sourceTextureHdl2.useScaling ? new Vector2(sourceTextureHdl2.rtHandleProperties.rtHandleScale.x, sourceTextureHdl2.rtHandleProperties.rtHandleScale.y) : Vector2.one);
					Blitter.BlitTexture(cmd2, sourceTextureHdl2, viewportScale2, SMAAMaterial2, 1);
				});
			}
			PostProcessPass.SMAAPassData passData4;
			using (IRasterRenderGraphBuilder builder4 = renderGraph.AddRasterRenderPass<PostProcessPass.SMAAPassData>("SMAA Neighborhood blending", out passData4, ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_SMAANeighborhoodBlend), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 229))
			{
				builder4.AllowGlobalStateModification(true);
				passData4.destinationTexture = SMAATarget;
				builder4.SetRenderAttachment(SMAATarget, 0, AccessFlags.Write);
				passData4.sourceTexture = source;
				builder4.UseTexture(in source, AccessFlags.Read);
				passData4.blendTexture = blendTexture;
				builder4.UseTexture(in blendTexture, AccessFlags.Read);
				passData4.material = material;
				builder4.SetRenderFunc<PostProcessPass.SMAAPassData>(delegate(PostProcessPass.SMAAPassData data, RasterGraphContext context)
				{
					Material SMAAMaterial3 = data.material;
					RasterCommandBuffer cmd3 = context.cmd;
					RTHandle sourceTextureHdl3 = data.sourceTexture;
					SMAAMaterial3.SetTexture(PostProcessPass.ShaderConstants._BlendTexture, data.blendTexture);
					Vector2 viewportScale3 = (sourceTextureHdl3.useScaling ? new Vector2(sourceTextureHdl3.rtHandleProperties.rtHandleScale.x, sourceTextureHdl3.rtHandleProperties.rtHandleScale.y) : Vector2.one);
					Blitter.BlitTexture(cmd3, sourceTextureHdl3, viewportScale3, SMAAMaterial3, 2);
				});
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001C8BC File Offset: 0x0001AABC
		public void UberPostSetupBloomPass(RenderGraph rendergraph, in TextureHandle bloomTexture, Material uberMaterial)
		{
			PostProcessPass.UberSetupBloomPassData passData;
			using (IRasterRenderGraphBuilder builder = rendergraph.AddRasterRenderPass<PostProcessPass.UberSetupBloomPassData>("Setup Bloom Post Processing", out passData, ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_UberPostSetupBloomPass), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 269))
			{
				Color tint = this.m_Bloom.tint.value.linear;
				float luma = ColorUtils.Luminance(in tint);
				tint = ((luma > 0f) ? (tint * (1f / luma)) : Color.white);
				Vector4 bloomParams = new Vector4(this.m_Bloom.intensity.value, tint.r, tint.g, tint.b);
				Texture dirtTexture = ((this.m_Bloom.dirtTexture.value == null) ? Texture2D.blackTexture : this.m_Bloom.dirtTexture.value);
				float dirtRatio = (float)dirtTexture.width / (float)dirtTexture.height;
				float screenRatio = (float)this.m_Descriptor.width / (float)this.m_Descriptor.height;
				Vector4 dirtScaleOffset = new Vector4(1f, 1f, 0f, 0f);
				float dirtIntensity = this.m_Bloom.dirtIntensity.value;
				if (dirtRatio > screenRatio)
				{
					dirtScaleOffset.x = screenRatio / dirtRatio;
					dirtScaleOffset.z = (1f - dirtScaleOffset.x) * 0.5f;
				}
				else if (screenRatio > dirtRatio)
				{
					dirtScaleOffset.y = dirtRatio / screenRatio;
					dirtScaleOffset.w = (1f - dirtScaleOffset.y) * 0.5f;
				}
				passData.bloomParams = bloomParams;
				passData.dirtScaleOffset = dirtScaleOffset;
				passData.dirtIntensity = dirtIntensity;
				passData.dirtTexture = dirtTexture;
				passData.highQualityFilteringValue = this.m_Bloom.highQualityFiltering.value;
				passData.bloomTexture = bloomTexture;
				builder.UseTexture(in bloomTexture, AccessFlags.Read);
				passData.uberMaterial = uberMaterial;
				builder.AllowPassCulling(false);
				builder.SetRenderFunc<PostProcessPass.UberSetupBloomPassData>(delegate(PostProcessPass.UberSetupBloomPassData data, RasterGraphContext context)
				{
					Material uberMaterial2 = data.uberMaterial;
					uberMaterial2.SetVector(PostProcessPass.ShaderConstants._Bloom_Params, data.bloomParams);
					uberMaterial2.SetVector(PostProcessPass.ShaderConstants._LensDirt_Params, data.dirtScaleOffset);
					uberMaterial2.SetFloat(PostProcessPass.ShaderConstants._LensDirt_Intensity, data.dirtIntensity);
					uberMaterial2.SetTexture(PostProcessPass.ShaderConstants._LensDirt_Texture, data.dirtTexture);
					if (data.highQualityFilteringValue)
					{
						uberMaterial2.EnableKeyword((data.dirtIntensity > 0f) ? "_BLOOM_HQ_DIRT" : "_BLOOM_HQ");
					}
					else
					{
						uberMaterial2.EnableKeyword((data.dirtIntensity > 0f) ? "_BLOOM_LQ_DIRT" : "_BLOOM_LQ");
					}
					uberMaterial2.SetTexture(PostProcessPass.ShaderConstants._Bloom_Texture, data.bloomTexture);
				});
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001CADC File Offset: 0x0001ACDC
		public void RenderBloomTexture(RenderGraph renderGraph, in TextureHandle source, out TextureHandle destination, bool enableAlphaOutput)
		{
			BloomDownscaleMode value = this.m_Bloom.downscale.value;
			int downres;
			if (value != BloomDownscaleMode.Half)
			{
				if (value != BloomDownscaleMode.Quarter)
				{
					throw new ArgumentOutOfRangeException();
				}
				downres = 2;
			}
			else
			{
				downres = 1;
			}
			int tw = Mathf.Max(1, this.m_Descriptor.width >> downres);
			int th = Mathf.Max(1, this.m_Descriptor.height >> downres);
			int mipCount = Mathf.Clamp(Mathf.FloorToInt(Mathf.Log((float)Mathf.Max(tw, th), 2f) - 1f), 1, this.m_Bloom.maxIterations.value);
			using (new ProfilingScope(ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_BloomSetup)))
			{
				float clamp = this.m_Bloom.clamp.value;
				float threshold = Mathf.GammaToLinearSpace(this.m_Bloom.threshold.value);
				float thresholdKnee = threshold * 0.5f;
				float scatter = Mathf.Lerp(0.05f, 0.95f, this.m_Bloom.scatter.value);
				PostProcessPass.BloomMaterialParams bloomParams = default(PostProcessPass.BloomMaterialParams);
				bloomParams.parameters = new Vector4(scatter, clamp, threshold, thresholdKnee);
				bloomParams.highQualityFiltering = this.m_Bloom.highQualityFiltering.value;
				bloomParams.enableAlphaOutput = enableAlphaOutput;
				Material material = this.m_Materials.bloom;
				bool flag = !this.m_BloomParamsPrev.Equals(ref bloomParams);
				bool isParamsPropertySet = material.HasProperty(PostProcessPass.ShaderConstants._Params);
				if (flag || !isParamsPropertySet)
				{
					material.SetVector(PostProcessPass.ShaderConstants._Params, bloomParams.parameters);
					CoreUtils.SetKeyword(material, "_BLOOM_HQ", bloomParams.highQualityFiltering);
					CoreUtils.SetKeyword(material, "_ENABLE_ALPHA_OUTPUT", bloomParams.enableAlphaOutput);
					for (uint i = 0U; i < 16U; i += 1U)
					{
						Material material2 = this.m_Materials.bloomUpsample[(int)i];
						material2.SetVector(PostProcessPass.ShaderConstants._Params, bloomParams.parameters);
						CoreUtils.SetKeyword(material2, "_BLOOM_HQ", bloomParams.highQualityFiltering);
						CoreUtils.SetKeyword(material2, "_ENABLE_ALPHA_OUTPUT", bloomParams.enableAlphaOutput);
					}
					this.m_BloomParamsPrev = bloomParams;
				}
				RenderTextureDescriptor desc = this.GetCompatibleDescriptor(tw, th, this.m_DefaultColorFormat, GraphicsFormat.None);
				this._BloomMipDown[0] = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, this.m_BloomMipDown[0].name, false, FilterMode.Bilinear, TextureWrapMode.Clamp);
				this._BloomMipUp[0] = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, this.m_BloomMipUp[0].name, false, FilterMode.Bilinear, TextureWrapMode.Clamp);
				for (int j = 1; j < mipCount; j++)
				{
					tw = Mathf.Max(1, tw >> 1);
					th = Mathf.Max(1, th >> 1);
					TextureHandle[] bloomMipDown = this._BloomMipDown;
					int num = j;
					ref TextureHandle mipUp = ref this._BloomMipUp[j];
					desc.width = tw;
					desc.height = th;
					bloomMipDown[num] = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, this.m_BloomMipDown[j].name, false, FilterMode.Bilinear, TextureWrapMode.Clamp);
					mipUp = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, this.m_BloomMipUp[j].name, false, FilterMode.Bilinear, TextureWrapMode.Clamp);
				}
			}
			PostProcessPass.BloomPassData passData;
			using (IUnsafeRenderGraphBuilder builder = renderGraph.AddUnsafePass<PostProcessPass.BloomPassData>("Blit Bloom Mipmaps", out passData, ProfilingSampler.Get<URPProfileId>(URPProfileId.Bloom), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 444))
			{
				passData.mipCount = mipCount;
				passData.material = this.m_Materials.bloom;
				passData.upsampleMaterials = this.m_Materials.bloomUpsample;
				passData.sourceTexture = source;
				passData.bloomMipDown = this._BloomMipDown;
				passData.bloomMipUp = this._BloomMipUp;
				builder.AllowPassCulling(false);
				builder.UseTexture(in source, AccessFlags.Read);
				for (int k = 0; k < mipCount; k++)
				{
					builder.UseTexture(in this._BloomMipDown[k], AccessFlags.ReadWrite);
					builder.UseTexture(in this._BloomMipUp[k], AccessFlags.ReadWrite);
				}
				builder.SetRenderFunc<PostProcessPass.BloomPassData>(delegate(PostProcessPass.BloomPassData data, UnsafeGraphContext context)
				{
					CommandBuffer cmd = CommandBufferHelpers.GetNativeCommandBuffer(context.cmd);
					Material material3 = data.material;
					int mipCount2 = data.mipCount;
					RenderBufferLoadAction loadAction = RenderBufferLoadAction.DontCare;
					RenderBufferStoreAction storeAction = RenderBufferStoreAction.Store;
					using (new ProfilingScope(cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_BloomPrefilter)))
					{
						Blitter.BlitCameraTexture(cmd, data.sourceTexture, data.bloomMipDown[0], loadAction, storeAction, material3, 0);
					}
					using (new ProfilingScope(cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_BloomDownsample)))
					{
						TextureHandle lastDown = data.bloomMipDown[0];
						for (int l = 1; l < mipCount2; l++)
						{
							TextureHandle mipDown = data.bloomMipDown[l];
							TextureHandle mipUp2 = data.bloomMipUp[l];
							Blitter.BlitCameraTexture(cmd, lastDown, mipUp2, loadAction, storeAction, material3, 1);
							Blitter.BlitCameraTexture(cmd, mipUp2, mipDown, loadAction, storeAction, material3, 2);
							lastDown = mipDown;
						}
					}
					using (new ProfilingScope(cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_BloomUpsample)))
					{
						for (int m = mipCount2 - 2; m >= 0; m--)
						{
							TextureHandle lowMip = ((m == mipCount2 - 2) ? data.bloomMipDown[m + 1] : data.bloomMipUp[m + 1]);
							TextureHandle highMip = data.bloomMipDown[m];
							TextureHandle dst = data.bloomMipUp[m];
							Material upMaterial = data.upsampleMaterials[m];
							upMaterial.SetTexture(PostProcessPass.ShaderConstants._SourceTexLowMip, lowMip);
							Blitter.BlitCameraTexture(cmd, highMip, dst, loadAction, storeAction, upMaterial, 3);
						}
					}
				});
				destination = passData.bloomMipUp[0];
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001CF18 File Offset: 0x0001B118
		public void RenderDoF(RenderGraph renderGraph, UniversalResourceData resourceData, UniversalCameraData cameraData, in TextureHandle source, out TextureHandle destination)
		{
			Material dofMaterial = ((this.m_DepthOfField.mode.value == DepthOfFieldMode.Gaussian) ? this.m_Materials.gaussianDepthOfField : this.m_Materials.bokehDepthOfField);
			RenderTextureDescriptor desc = PostProcessPass.GetCompatibleDescriptor(this.m_Descriptor, this.m_Descriptor.width, this.m_Descriptor.height, this.m_Descriptor.graphicsFormat, GraphicsFormat.None);
			destination = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, "_DoFTarget", true, FilterMode.Bilinear, TextureWrapMode.Clamp);
			CoreUtils.SetKeyword(dofMaterial, "_ENABLE_ALPHA_OUTPUT", cameraData.isAlphaOutputEnabled);
			if (this.m_DepthOfField.mode.value == DepthOfFieldMode.Gaussian)
			{
				this.RenderDoFGaussian(renderGraph, resourceData, cameraData, in source, destination, ref dofMaterial);
				return;
			}
			if (this.m_DepthOfField.mode.value == DepthOfFieldMode.Bokeh)
			{
				this.RenderDoFBokeh(renderGraph, resourceData, cameraData, in source, in destination, ref dofMaterial);
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001CFF0 File Offset: 0x0001B1F0
		public void RenderDoFGaussian(RenderGraph renderGraph, UniversalResourceData resourceData, UniversalCameraData cameraData, in TextureHandle source, TextureHandle destination, ref Material dofMaterial)
		{
			Material material = dofMaterial;
			int downSample = 2;
			int wh = this.m_Descriptor.width / downSample;
			int hh = this.m_Descriptor.height / downSample;
			RenderTextureDescriptor fullCoCTextureDesc = PostProcessPass.GetCompatibleDescriptor(this.m_Descriptor, this.m_Descriptor.width, this.m_Descriptor.height, this.m_GaussianCoCFormat, GraphicsFormat.None);
			TextureHandle fullCoCTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, fullCoCTextureDesc, "_FullCoCTexture", true, FilterMode.Bilinear, TextureWrapMode.Clamp);
			RenderTextureDescriptor halfCoCTextureDesc = PostProcessPass.GetCompatibleDescriptor(this.m_Descriptor, wh, hh, this.m_GaussianCoCFormat, GraphicsFormat.None);
			TextureHandle halfCoCTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, halfCoCTextureDesc, "_HalfCoCTexture", true, FilterMode.Bilinear, TextureWrapMode.Clamp);
			RenderTextureDescriptor pingTextureDesc = PostProcessPass.GetCompatibleDescriptor(this.m_Descriptor, wh, hh, this.m_DefaultColorFormat, GraphicsFormat.None);
			TextureHandle pingTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, pingTextureDesc, "_PingTexture", true, FilterMode.Bilinear, TextureWrapMode.Clamp);
			RenderTextureDescriptor pongTextureDesc = PostProcessPass.GetCompatibleDescriptor(this.m_Descriptor, wh, hh, this.m_DefaultColorFormat, GraphicsFormat.None);
			TextureHandle pongTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, pongTextureDesc, "_PongTexture", true, FilterMode.Bilinear, TextureWrapMode.Clamp);
			PostProcessPass.DoFGaussianPassData passData;
			using (IUnsafeRenderGraphBuilder builder = renderGraph.AddUnsafePass<PostProcessPass.DoFGaussianPassData>("Depth of Field - Gaussian", out passData, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 587))
			{
				float farStart = this.m_DepthOfField.gaussianStart.value;
				float farEnd = Mathf.Max(farStart, this.m_DepthOfField.gaussianEnd.value);
				float maxRadius = this.m_DepthOfField.gaussianMaxRadius.value * ((float)wh / 1080f);
				maxRadius = Mathf.Min(maxRadius, 2f);
				passData.downsample = downSample;
				passData.cocParams = new Vector3(farStart, farEnd, maxRadius);
				passData.highQualitySamplingValue = this.m_DepthOfField.highQualitySampling.value;
				passData.material = material;
				passData.materialCoC = this.m_Materials.gaussianDepthOfFieldCoC;
				passData.sourceTexture = source;
				builder.UseTexture(in source, AccessFlags.Read);
				passData.depthTexture = resourceData.cameraDepthTexture;
				IBaseRenderGraphBuilder baseRenderGraphBuilder = builder;
				TextureHandle cameraDepthTexture = resourceData.cameraDepthTexture;
				baseRenderGraphBuilder.UseTexture(in cameraDepthTexture, AccessFlags.Read);
				passData.fullCoCTexture = fullCoCTexture;
				builder.UseTexture(in fullCoCTexture, AccessFlags.ReadWrite);
				passData.halfCoCTexture = halfCoCTexture;
				builder.UseTexture(in halfCoCTexture, AccessFlags.ReadWrite);
				passData.pingTexture = pingTexture;
				builder.UseTexture(in pingTexture, AccessFlags.ReadWrite);
				passData.pongTexture = pongTexture;
				builder.UseTexture(in pongTexture, AccessFlags.ReadWrite);
				passData.destination = destination;
				builder.UseTexture(in destination, AccessFlags.Write);
				builder.SetRenderFunc<PostProcessPass.DoFGaussianPassData>(delegate(PostProcessPass.DoFGaussianPassData data, UnsafeGraphContext context)
				{
					Material dofMat = data.material;
					Material dofMaterialCoC = data.materialCoC;
					CommandBuffer cmd = CommandBufferHelpers.GetNativeCommandBuffer(context.cmd);
					RTHandle sourceTextureHdl = data.sourceTexture;
					RTHandle dstHdl = data.destination;
					using (new ProfilingScope(ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_SetupDoF)))
					{
						dofMat.SetVector(PostProcessPass.ShaderConstants._CoCParams, data.cocParams);
						CoreUtils.SetKeyword(dofMat, "_HIGH_QUALITY_SAMPLING", data.highQualitySamplingValue);
						dofMaterialCoC.SetVector(PostProcessPass.ShaderConstants._CoCParams, data.cocParams);
						CoreUtils.SetKeyword(dofMaterialCoC, "_HIGH_QUALITY_SAMPLING", data.highQualitySamplingValue);
						PostProcessUtils.SetSourceSize(cmd, data.sourceTexture);
						dofMat.SetVector(PostProcessPass.ShaderConstants._DownSampleScaleFactor, new Vector4(1f / (float)data.downsample, 1f / (float)data.downsample, (float)data.downsample, (float)data.downsample));
					}
					using (new ProfilingScope(ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_DOFComputeCOC)))
					{
						dofMat.SetTexture(PostProcessPass.s_CameraDepthTextureID, data.depthTexture);
						Blitter.BlitCameraTexture(cmd, data.sourceTexture, data.fullCoCTexture, data.materialCoC, 0);
					}
					using (new ProfilingScope(ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_DOFDownscalePrefilter)))
					{
						dofMat.SetTexture(PostProcessPass.ShaderConstants._FullCoCTexture, data.fullCoCTexture);
						data.multipleRenderTargets[0] = data.halfCoCTexture;
						data.multipleRenderTargets[1] = data.pingTexture;
						CoreUtils.SetRenderTarget(cmd, data.multipleRenderTargets, data.halfCoCTexture);
						Vector2 viewportScale = (sourceTextureHdl.useScaling ? new Vector2(sourceTextureHdl.rtHandleProperties.rtHandleScale.x, sourceTextureHdl.rtHandleProperties.rtHandleScale.y) : Vector2.one);
						Blitter.BlitTexture(cmd, data.sourceTexture, viewportScale, dofMat, 1);
					}
					using (new ProfilingScope(ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_DOFBlurH)))
					{
						dofMat.SetTexture(PostProcessPass.ShaderConstants._HalfCoCTexture, data.halfCoCTexture);
						Blitter.BlitCameraTexture(cmd, data.pingTexture, data.pongTexture, dofMat, 2);
					}
					using (new ProfilingScope(ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_DOFBlurV)))
					{
						Blitter.BlitCameraTexture(cmd, data.pongTexture, data.pingTexture, dofMat, 3);
					}
					using (new ProfilingScope(ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_DOFComposite)))
					{
						dofMat.SetTexture(PostProcessPass.ShaderConstants._ColorTexture, data.pingTexture);
						dofMat.SetTexture(PostProcessPass.ShaderConstants._FullCoCTexture, data.fullCoCTexture);
						Blitter.BlitCameraTexture(cmd, sourceTextureHdl, dstHdl, dofMat, 4);
					}
				});
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001D274 File Offset: 0x0001B474
		public void RenderDoFBokeh(RenderGraph renderGraph, UniversalResourceData resourceData, UniversalCameraData cameraData, in TextureHandle source, in TextureHandle destination, ref Material dofMaterial)
		{
			int downSample = 2;
			Material material = dofMaterial;
			int wh = this.m_Descriptor.width / downSample;
			int hh = this.m_Descriptor.height / downSample;
			RenderTextureDescriptor fullCoCTextureDesc = PostProcessPass.GetCompatibleDescriptor(this.m_Descriptor, this.m_Descriptor.width, this.m_Descriptor.height, GraphicsFormat.R8_UNorm, GraphicsFormat.None);
			TextureHandle fullCoCTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, fullCoCTextureDesc, "_FullCoCTexture", true, FilterMode.Bilinear, TextureWrapMode.Clamp);
			RenderTextureDescriptor pingTextureDesc = PostProcessPass.GetCompatibleDescriptor(this.m_Descriptor, wh, hh, GraphicsFormat.R16G16B16A16_SFloat, GraphicsFormat.None);
			TextureHandle pingTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, pingTextureDesc, "_PingTexture", true, FilterMode.Bilinear, TextureWrapMode.Clamp);
			RenderTextureDescriptor pongTextureDesc = PostProcessPass.GetCompatibleDescriptor(this.m_Descriptor, wh, hh, GraphicsFormat.R16G16B16A16_SFloat, GraphicsFormat.None);
			TextureHandle pongTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, pongTextureDesc, "_PongTexture", true, FilterMode.Bilinear, TextureWrapMode.Clamp);
			PostProcessPass.DoFBokehPassData passData;
			using (IUnsafeRenderGraphBuilder builder = renderGraph.AddUnsafePass<PostProcessPass.DoFBokehPassData>("Depth of Field - Bokeh", out passData, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 738))
			{
				float F = this.m_DepthOfField.focalLength.value / 1000f;
				float num = this.m_DepthOfField.focalLength.value / this.m_DepthOfField.aperture.value;
				float P = this.m_DepthOfField.focusDistance.value;
				float maxCoC = num * F / (P - F);
				float maxRadius = PostProcessPass.GetMaxBokehRadiusInPixels((float)this.m_Descriptor.height);
				float rcpAspect = 1f / ((float)wh / (float)hh);
				int hash = this.m_DepthOfField.GetHashCode();
				if (hash != this.m_BokehHash || maxRadius != this.m_BokehMaxRadius || rcpAspect != this.m_BokehRCPAspect)
				{
					this.m_BokehHash = hash;
					this.m_BokehMaxRadius = maxRadius;
					this.m_BokehRCPAspect = rcpAspect;
					this.PrepareBokehKernel(maxRadius, rcpAspect);
				}
				float uvMargin = 1f / (float)this.m_Descriptor.height * (float)downSample;
				passData.bokehKernel = this.m_BokehKernel;
				passData.downSample = downSample;
				passData.uvMargin = uvMargin;
				passData.cocParams = new Vector4(P, maxCoC, maxRadius, rcpAspect);
				passData.useFastSRGBLinearConversion = this.m_UseFastSRGBLinearConversion;
				passData.sourceTexture = source;
				builder.UseTexture(in source, AccessFlags.Read);
				passData.depthTexture = resourceData.cameraDepthTexture;
				IBaseRenderGraphBuilder baseRenderGraphBuilder = builder;
				TextureHandle cameraDepthTexture = resourceData.cameraDepthTexture;
				baseRenderGraphBuilder.UseTexture(in cameraDepthTexture, AccessFlags.Read);
				passData.material = material;
				passData.materialCoC = this.m_Materials.bokehDepthOfFieldCoC;
				passData.fullCoCTexture = fullCoCTexture;
				builder.UseTexture(in fullCoCTexture, AccessFlags.ReadWrite);
				passData.pingTexture = pingTexture;
				builder.UseTexture(in pingTexture, AccessFlags.ReadWrite);
				passData.pongTexture = pongTexture;
				builder.UseTexture(in pongTexture, AccessFlags.ReadWrite);
				passData.destination = destination;
				builder.UseTexture(in destination, AccessFlags.Write);
				builder.SetRenderFunc<PostProcessPass.DoFBokehPassData>(delegate(PostProcessPass.DoFBokehPassData data, UnsafeGraphContext context)
				{
					Material dofMat = data.material;
					Material dofMaterialCoC = data.materialCoC;
					CommandBuffer cmd = CommandBufferHelpers.GetNativeCommandBuffer(context.cmd);
					RTHandle sourceTextureHdl = data.sourceTexture;
					RTHandle dst = data.destination;
					using (new ProfilingScope(ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_SetupDoF)))
					{
						CoreUtils.SetKeyword(dofMat, "_USE_FAST_SRGB_LINEAR_CONVERSION", data.useFastSRGBLinearConversion);
						CoreUtils.SetKeyword(dofMaterialCoC, "_USE_FAST_SRGB_LINEAR_CONVERSION", data.useFastSRGBLinearConversion);
						dofMat.SetVector(PostProcessPass.ShaderConstants._CoCParams, data.cocParams);
						dofMat.SetVectorArray(PostProcessPass.ShaderConstants._BokehKernel, data.bokehKernel);
						dofMat.SetVector(PostProcessPass.ShaderConstants._DownSampleScaleFactor, new Vector4(1f / (float)data.downSample, 1f / (float)data.downSample, (float)data.downSample, (float)data.downSample));
						dofMat.SetVector(PostProcessPass.ShaderConstants._BokehConstants, new Vector4(data.uvMargin, data.uvMargin * 2f));
						PostProcessUtils.SetSourceSize(cmd, data.sourceTexture);
					}
					using (new ProfilingScope(ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_DOFComputeCOC)))
					{
						dofMat.SetTexture(PostProcessPass.s_CameraDepthTextureID, data.depthTexture);
						Blitter.BlitCameraTexture(cmd, sourceTextureHdl, data.fullCoCTexture, dofMat, 0);
					}
					using (new ProfilingScope(ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_DOFDownscalePrefilter)))
					{
						dofMat.SetTexture(PostProcessPass.ShaderConstants._FullCoCTexture, data.fullCoCTexture);
						Blitter.BlitCameraTexture(cmd, sourceTextureHdl, data.pingTexture, dofMat, 1);
					}
					using (new ProfilingScope(ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_DOFBlurBokeh)))
					{
						Blitter.BlitCameraTexture(cmd, data.pingTexture, data.pongTexture, dofMat, 2);
					}
					using (new ProfilingScope(ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_DOFPostFilter)))
					{
						Blitter.BlitCameraTexture(cmd, data.pongTexture, data.pingTexture, dofMat, 3);
					}
					using (new ProfilingScope(ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_DOFComposite)))
					{
						dofMat.SetTexture(PostProcessPass.ShaderConstants._DofTexture, data.pingTexture);
						Blitter.BlitCameraTexture(cmd, sourceTextureHdl, dst, dofMat, 4);
					}
				});
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001D548 File Offset: 0x0001B748
		public void RenderPaniniProjection(RenderGraph renderGraph, Camera camera, in TextureHandle source, out TextureHandle destination)
		{
			RenderTextureDescriptor desc = PostProcessPass.GetCompatibleDescriptor(this.m_Descriptor, this.m_Descriptor.width, this.m_Descriptor.height, this.m_Descriptor.graphicsFormat, GraphicsFormat.None);
			destination = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, "_PaniniProjectionTarget", true, FilterMode.Bilinear, TextureWrapMode.Clamp);
			float distance = this.m_PaniniProjection.distance.value;
			Vector2 viewExtents = this.CalcViewExtents(camera);
			Vector2 vector = this.CalcCropExtents(camera, distance);
			float scaleX = vector.x / viewExtents.x;
			float scaleY = vector.y / viewExtents.y;
			float scaleF = Mathf.Min(scaleX, scaleY);
			float paniniD = distance;
			float paniniS = Mathf.Lerp(1f, Mathf.Clamp01(scaleF), this.m_PaniniProjection.cropToFit.value);
			PostProcessPass.PaniniProjectionPassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<PostProcessPass.PaniniProjectionPassData>("Panini Projection", out passData, ProfilingSampler.Get<URPProfileId>(URPProfileId.PaniniProjection), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 884))
			{
				builder.AllowGlobalStateModification(true);
				passData.destinationTexture = destination;
				builder.SetRenderAttachment(destination, 0, AccessFlags.Write);
				passData.sourceTexture = source;
				builder.UseTexture(in source, AccessFlags.Read);
				passData.material = this.m_Materials.paniniProjection;
				passData.paniniParams = new Vector4(viewExtents.x, viewExtents.y, paniniD, paniniS);
				passData.isPaniniGeneric = 1f - Mathf.Abs(paniniD) > float.Epsilon;
				passData.sourceTextureDesc = this.m_Descriptor;
				builder.SetRenderFunc<PostProcessPass.PaniniProjectionPassData>(delegate(PostProcessPass.PaniniProjectionPassData data, RasterGraphContext context)
				{
					RasterCommandBuffer cmd = context.cmd;
					RTHandle sourceTextureHdl = data.sourceTexture;
					cmd.SetGlobalVector(PostProcessPass.ShaderConstants._Params, data.paniniParams);
					data.material.EnableKeyword(data.isPaniniGeneric ? "_GENERIC" : "_UNIT_DISTANCE");
					Vector2 viewportScale = (sourceTextureHdl.useScaling ? new Vector2(sourceTextureHdl.rtHandleProperties.rtHandleScale.x, sourceTextureHdl.rtHandleProperties.rtHandleScale.y) : Vector2.one);
					Blitter.BlitTexture(cmd, sourceTextureHdl, viewportScale, data.material, 0);
				});
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001D6FC File Offset: 0x0001B8FC
		private void RenderTemporalAA(RenderGraph renderGraph, UniversalResourceData resourceData, UniversalCameraData cameraData, ref TextureHandle source, out TextureHandle destination)
		{
			RenderTextureDescriptor desc = PostProcessPass.GetCompatibleDescriptor(this.m_Descriptor, this.m_Descriptor.width, this.m_Descriptor.height, this.m_Descriptor.graphicsFormat, GraphicsFormat.None);
			destination = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, "_TemporalAATarget", false, FilterMode.Bilinear, TextureWrapMode.Clamp);
			TextureHandle cameraDepth = resourceData.cameraDepth;
			TextureHandle motionVectors = resourceData.motionVectorColor;
			TemporalAA.Render(renderGraph, this.m_Materials.temporalAntialiasing, cameraData, ref source, ref cameraDepth, ref motionVectors, ref destination);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001D778 File Offset: 0x0001B978
		private void RenderSTP(RenderGraph renderGraph, UniversalResourceData resourceData, UniversalCameraData cameraData, ref TextureHandle source, out TextureHandle destination)
		{
			TextureHandle cameraDepth = resourceData.cameraDepthTexture;
			TextureHandle motionVectors = resourceData.motionVectorColor;
			RenderTextureDescriptor desc = PostProcessPass.GetCompatibleDescriptor(cameraData.cameraTargetDescriptor, cameraData.pixelWidth, cameraData.pixelHeight, cameraData.cameraTargetDescriptor.graphicsFormat, GraphicsFormat.None);
			desc.enableRandomWrite = true;
			desc.sRGB = false;
			destination = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, "_UpscaledColorTarget", false, FilterMode.Bilinear, TextureWrapMode.Clamp);
			int frameIndex = Time.frameCount;
			Texture2D noiseTexture = this.m_Data.textures.blueNoise16LTex[frameIndex & (this.m_Data.textures.blueNoise16LTex.Length - 1)];
			StpUtils.Execute(renderGraph, resourceData, cameraData, source, cameraDepth, motionVectors, destination, noiseTexture);
			this.UpdateCameraResolution(renderGraph, cameraData, new Vector2Int(desc.width, desc.height));
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001D844 File Offset: 0x0001BA44
		public void RenderMotionBlur(RenderGraph renderGraph, UniversalResourceData resourceData, UniversalCameraData cameraData, in TextureHandle source, out TextureHandle destination)
		{
			Material material = this.m_Materials.cameraMotionBlur;
			RenderTextureDescriptor desc = PostProcessPass.GetCompatibleDescriptor(this.m_Descriptor, this.m_Descriptor.width, this.m_Descriptor.height, this.m_Descriptor.graphicsFormat, GraphicsFormat.None);
			destination = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, "_MotionBlurTarget", true, FilterMode.Bilinear, TextureWrapMode.Clamp);
			TextureHandle motionVectorColor = resourceData.motionVectorColor;
			TextureHandle cameraDepthTexture = resourceData.cameraDepthTexture;
			MotionBlurMode mode = this.m_MotionBlur.mode.value;
			int passIndex = (int)this.m_MotionBlur.quality.value;
			passIndex += ((mode == MotionBlurMode.CameraAndObjects) ? 3 : 0);
			PostProcessPass.MotionBlurPassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<PostProcessPass.MotionBlurPassData>("Motion Blur", out passData, ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_MotionBlur), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 1001))
			{
				builder.AllowGlobalStateModification(true);
				passData.destinationTexture = destination;
				builder.SetRenderAttachment(destination, 0, AccessFlags.Write);
				passData.sourceTexture = source;
				builder.UseTexture(in source, AccessFlags.Read);
				if (mode == MotionBlurMode.CameraAndObjects)
				{
					passData.motionVectors = motionVectorColor;
					builder.UseTexture(in motionVectorColor, AccessFlags.Read);
				}
				else
				{
					passData.motionVectors = TextureHandle.nullHandle;
				}
				builder.UseTexture(in cameraDepthTexture, AccessFlags.Read);
				passData.material = material;
				passData.passIndex = passIndex;
				passData.camera = cameraData.camera;
				passData.xr = cameraData.xr;
				passData.enableAlphaOutput = cameraData.isAlphaOutputEnabled;
				passData.intensity = this.m_MotionBlur.intensity.value;
				passData.clamp = this.m_MotionBlur.clamp.value;
				builder.SetRenderFunc<PostProcessPass.MotionBlurPassData>(delegate(PostProcessPass.MotionBlurPassData data, RasterGraphContext context)
				{
					RasterCommandBuffer cmd = context.cmd;
					RTHandle sourceTextureHdl = data.sourceTexture;
					PostProcessPass.UpdateMotionBlurMatrices(ref data.material, data.camera, data.xr);
					data.material.SetFloat("_Intensity", data.intensity);
					data.material.SetFloat("_Clamp", data.clamp);
					CoreUtils.SetKeyword(data.material, "_ENABLE_ALPHA_OUTPUT", data.enableAlphaOutput);
					PostProcessUtils.SetSourceSize(cmd, data.sourceTexture);
					Vector2 viewportScale = (sourceTextureHdl.useScaling ? new Vector2(sourceTextureHdl.rtHandleProperties.rtHandleScale.x, sourceTextureHdl.rtHandleProperties.rtHandleScale.y) : Vector2.one);
					Blitter.BlitTexture(cmd, sourceTextureHdl, viewportScale, data.material, data.passIndex);
				});
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001DA18 File Offset: 0x0001BC18
		private void LensFlareDataDrivenComputeOcclusion(RenderGraph renderGraph, UniversalResourceData resourceData, UniversalCameraData cameraData)
		{
			if (!LensFlareCommonSRP.IsOcclusionRTCompatible())
			{
				return;
			}
			PostProcessPass.LensFlarePassData passData;
			using (IUnsafeRenderGraphBuilder builder = renderGraph.AddUnsafePass<PostProcessPass.LensFlarePassData>("Lens Flare Compute Occlusion", out passData, ProfilingSampler.Get<URPProfileId>(URPProfileId.LensFlareDataDrivenComputeOcclusion), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 1072))
			{
				RTHandle occlusionRT = LensFlareCommonSRP.occlusionRT;
				TextureHandle occlusionHandle = renderGraph.ImportTexture(LensFlareCommonSRP.occlusionRT);
				passData.destinationTexture = occlusionHandle;
				builder.UseTexture(in occlusionHandle, AccessFlags.Write);
				passData.cameraData = cameraData;
				passData.viewport = cameraData.pixelRect;
				passData.material = this.m_Materials.lensFlareDataDriven;
				passData.width = (float)this.m_Descriptor.width;
				passData.height = (float)this.m_Descriptor.height;
				if (this.m_PaniniProjection.IsActive())
				{
					passData.usePanini = true;
					passData.paniniDistance = this.m_PaniniProjection.distance.value;
					passData.paniniCropToFit = this.m_PaniniProjection.cropToFit.value;
				}
				else
				{
					passData.usePanini = false;
					passData.paniniDistance = 1f;
					passData.paniniCropToFit = 1f;
				}
				IBaseRenderGraphBuilder baseRenderGraphBuilder = builder;
				TextureHandle cameraDepthTexture = resourceData.cameraDepthTexture;
				baseRenderGraphBuilder.UseTexture(in cameraDepthTexture, AccessFlags.Read);
				builder.SetRenderFunc<PostProcessPass.LensFlarePassData>(delegate(PostProcessPass.LensFlarePassData data, UnsafeGraphContext ctx)
				{
					Camera camera = data.cameraData.camera;
					XRPass xr = data.cameraData.xr;
					Matrix4x4 nonJitteredViewProjMatrix0;
					if (xr.enabled)
					{
						if (xr.singlePassEnabled)
						{
							nonJitteredViewProjMatrix0 = GL.GetGPUProjectionMatrix(data.cameraData.GetProjectionMatrixNoJitter(0), true) * data.cameraData.GetViewMatrix(0);
						}
						else
						{
							nonJitteredViewProjMatrix0 = GL.GetGPUProjectionMatrix(camera.projectionMatrix, true) * camera.worldToCameraMatrix;
							int multipassId = data.cameraData.xr.multipassId;
						}
					}
					else
					{
						nonJitteredViewProjMatrix0 = GL.GetGPUProjectionMatrix(data.cameraData.GetProjectionMatrixNoJitter(0), true) * data.cameraData.GetViewMatrix(0);
					}
					LensFlareCommonSRP.ComputeOcclusion(data.material, camera, xr, xr.multipassId, data.width, data.height, data.usePanini, data.paniniDistance, data.paniniCropToFit, true, camera.transform.position, nonJitteredViewProjMatrix0, ctx.cmd, false, false, null, null);
					if (xr.enabled && xr.singlePassEnabled)
					{
						for (int xrIdx = 1; xrIdx < xr.viewCount; xrIdx++)
						{
							Matrix4x4 gpuVPXR = GL.GetGPUProjectionMatrix(data.cameraData.GetProjectionMatrixNoJitter(xrIdx), true) * data.cameraData.GetViewMatrix(xrIdx);
							LensFlareCommonSRP.ComputeOcclusion(data.material, camera, xr, xrIdx, data.width, data.height, data.usePanini, data.paniniDistance, data.paniniCropToFit, true, camera.transform.position, gpuVPXR, ctx.cmd, false, false, null, null);
						}
					}
				});
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001DB74 File Offset: 0x0001BD74
		public void RenderLensFlareDataDriven(RenderGraph renderGraph, UniversalResourceData resourceData, UniversalCameraData cameraData, in TextureHandle destination)
		{
			PostProcessPass.LensFlarePassData passData;
			using (IUnsafeRenderGraphBuilder builder = renderGraph.AddUnsafePass<PostProcessPass.LensFlarePassData>("Lens Flare Data Driven Pass", out passData, ProfilingSampler.Get<URPProfileId>(URPProfileId.LensFlareDataDriven), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 1170))
			{
				passData.destinationTexture = destination;
				builder.UseTexture(in destination, AccessFlags.Write);
				passData.sourceDescriptor = this.m_Descriptor;
				passData.cameraData = cameraData;
				passData.material = this.m_Materials.lensFlareDataDriven;
				passData.width = (float)this.m_Descriptor.width;
				passData.height = (float)this.m_Descriptor.height;
				passData.viewport.x = 0f;
				passData.viewport.y = 0f;
				passData.viewport.width = (float)this.m_Descriptor.width;
				passData.viewport.height = (float)this.m_Descriptor.height;
				if (this.m_PaniniProjection.IsActive())
				{
					passData.usePanini = true;
					passData.paniniDistance = this.m_PaniniProjection.distance.value;
					passData.paniniCropToFit = this.m_PaniniProjection.cropToFit.value;
				}
				else
				{
					passData.usePanini = false;
					passData.paniniDistance = 1f;
					passData.paniniCropToFit = 1f;
				}
				if (LensFlareCommonSRP.IsOcclusionRTCompatible())
				{
					TextureHandle occlusionHandle = renderGraph.ImportTexture(LensFlareCommonSRP.occlusionRT);
					builder.UseTexture(in occlusionHandle, AccessFlags.Read);
				}
				else
				{
					IBaseRenderGraphBuilder baseRenderGraphBuilder = builder;
					TextureHandle cameraDepthTexture = resourceData.cameraDepthTexture;
					baseRenderGraphBuilder.UseTexture(in cameraDepthTexture, AccessFlags.Read);
				}
				builder.SetRenderFunc<PostProcessPass.LensFlarePassData>(delegate(PostProcessPass.LensFlarePassData data, UnsafeGraphContext ctx)
				{
					Camera camera = data.cameraData.camera;
					XRPass xr = data.cameraData.xr;
					if (!xr.enabled || (xr.enabled && !xr.singlePassEnabled))
					{
						Matrix4x4 nonJitteredViewProjMatrix0 = GL.GetGPUProjectionMatrix(camera.projectionMatrix, true) * camera.worldToCameraMatrix;
						LensFlareCommonSRP.DoLensFlareDataDrivenCommon(data.material, data.cameraData.camera, data.viewport, xr, data.cameraData.xr.multipassId, data.width, data.height, data.usePanini, data.paniniDistance, data.paniniCropToFit, true, camera.transform.position, nonJitteredViewProjMatrix0, ctx.cmd, false, false, null, null, data.destinationTexture, (Light light, Camera cam, Vector3 wo) => PostProcessPass.GetLensFlareLightAttenuation(light, cam, wo), false);
						return;
					}
					for (int xrIdx = 0; xrIdx < xr.viewCount; xrIdx++)
					{
						Matrix4x4 nonJitteredViewProjMatrix_k = GL.GetGPUProjectionMatrix(data.cameraData.GetProjectionMatrixNoJitter(xrIdx), true) * data.cameraData.GetViewMatrix(xrIdx);
						LensFlareCommonSRP.DoLensFlareDataDrivenCommon(data.material, data.cameraData.camera, data.viewport, xr, data.cameraData.xr.multipassId, data.width, data.height, data.usePanini, data.paniniDistance, data.paniniCropToFit, true, camera.transform.position, nonJitteredViewProjMatrix_k, ctx.cmd, false, false, null, null, data.destinationTexture, (Light light, Camera cam, Vector3 wo) => PostProcessPass.GetLensFlareLightAttenuation(light, cam, wo), false);
					}
				});
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001DD28 File Offset: 0x0001BF28
		public TextureHandle RenderLensFlareScreenSpace(RenderGraph renderGraph, Camera camera, in TextureHandle destination, TextureHandle originalBloomTexture, TextureHandle screenSpaceLensFlareBloomMipTexture, bool enableXR)
		{
			int downsample = (int)this.m_LensFlareScreenSpace.resolution.value;
			int width = this.m_Descriptor.width / downsample;
			int height = this.m_Descriptor.height / downsample;
			RenderTextureDescriptor streakTextureDesc = PostProcessPass.GetCompatibleDescriptor(this.m_Descriptor, width, height, this.m_DefaultColorFormat, GraphicsFormat.None);
			TextureHandle streakTmpTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, streakTextureDesc, "_StreakTmpTexture", true, FilterMode.Bilinear, TextureWrapMode.Clamp);
			TextureHandle streakTmpTexture2 = UniversalRenderer.CreateRenderGraphTexture(renderGraph, streakTextureDesc, "_StreakTmpTexture2", true, FilterMode.Bilinear, TextureWrapMode.Clamp);
			TextureHandle resultTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, streakTextureDesc, "Lens Flare Screen Space Result", true, FilterMode.Bilinear, TextureWrapMode.Clamp);
			PostProcessPass.LensFlareScreenSpacePassData passData;
			TextureHandle originalBloomTexture2;
			using (IUnsafeRenderGraphBuilder builder = renderGraph.AddUnsafePass<PostProcessPass.LensFlareScreenSpacePassData>("Lens Flare Screen Space Pass", out passData, ProfilingSampler.Get<URPProfileId>(URPProfileId.LensFlareScreenSpace), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 1290))
			{
				passData.destinationTexture = destination;
				builder.UseTexture(in destination, AccessFlags.Write);
				passData.streakTmpTexture = streakTmpTexture;
				builder.UseTexture(in streakTmpTexture, AccessFlags.ReadWrite);
				passData.streakTmpTexture2 = streakTmpTexture2;
				builder.UseTexture(in streakTmpTexture2, AccessFlags.ReadWrite);
				passData.screenSpaceLensFlareBloomMipTexture = screenSpaceLensFlareBloomMipTexture;
				builder.UseTexture(in screenSpaceLensFlareBloomMipTexture, AccessFlags.ReadWrite);
				passData.originalBloomTexture = originalBloomTexture;
				builder.UseTexture(in originalBloomTexture, AccessFlags.ReadWrite);
				passData.sourceDescriptor = this.m_Descriptor;
				passData.camera = camera;
				passData.material = this.m_Materials.lensFlareScreenSpace;
				passData.lensFlareScreenSpace = this.m_LensFlareScreenSpace;
				passData.downsample = downsample;
				passData.result = resultTexture;
				builder.UseTexture(in resultTexture, AccessFlags.Write);
				builder.SetRenderFunc<PostProcessPass.LensFlareScreenSpacePassData>(delegate(PostProcessPass.LensFlareScreenSpacePassData data, UnsafeGraphContext context)
				{
					UnsafeCommandBuffer cmd = context.cmd;
					Camera camera2 = data.camera;
					ScreenSpaceLensFlare lensFlareScreenSpace = data.lensFlareScreenSpace;
					LensFlareCommonSRP.DoLensFlareScreenSpaceCommon(data.material, camera2, (float)data.sourceDescriptor.width, (float)data.sourceDescriptor.height, data.lensFlareScreenSpace.tintColor.value, data.originalBloomTexture, data.screenSpaceLensFlareBloomMipTexture, null, data.streakTmpTexture, data.streakTmpTexture2, new Vector4(lensFlareScreenSpace.intensity.value, lensFlareScreenSpace.firstFlareIntensity.value, lensFlareScreenSpace.secondaryFlareIntensity.value, lensFlareScreenSpace.warpedFlareIntensity.value), new Vector4(lensFlareScreenSpace.vignetteEffect.value, lensFlareScreenSpace.startingPosition.value, lensFlareScreenSpace.scale.value, 0f), new Vector4((float)lensFlareScreenSpace.samples.value, lensFlareScreenSpace.sampleDimmer.value, lensFlareScreenSpace.chromaticAbberationIntensity.value, 0f), new Vector4(lensFlareScreenSpace.streaksIntensity.value, lensFlareScreenSpace.streaksLength.value, lensFlareScreenSpace.streaksOrientation.value, lensFlareScreenSpace.streaksThreshold.value), new Vector4((float)data.downsample, lensFlareScreenSpace.warpedFlareScale.value.x, lensFlareScreenSpace.warpedFlareScale.value.y, 0f), cmd, data.result, false);
				});
				originalBloomTexture2 = passData.originalBloomTexture;
			}
			return originalBloomTexture2;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001DECC File Offset: 0x0001C0CC
		private static void ScaleViewportAndBlit(RasterCommandBuffer cmd, RTHandle sourceTextureHdl, RTHandle dest, UniversalCameraData cameraData, Material material, bool hasFinalPass)
		{
			Vector4 scaleBias = RenderingUtils.GetFinalBlitScaleBias(sourceTextureHdl, dest, cameraData);
			RenderTargetIdentifier cameraTarget = BuiltinRenderTextureType.CameraTarget;
			if (cameraData.xr.enabled)
			{
				cameraTarget = cameraData.xr.renderTarget;
			}
			if (dest.nameID == cameraTarget || cameraData.targetTexture != null)
			{
				if (hasFinalPass || !cameraData.resolveFinalTarget)
				{
					Rect camViewportNormalized = cameraData.camera.rect;
					int targetWidth = cameraData.cameraTargetDescriptor.width;
					int targetHeight = cameraData.cameraTargetDescriptor.height;
					Rect scaledTargetViewportInPixels = new Rect(camViewportNormalized.x * (float)targetWidth, camViewportNormalized.y * (float)targetHeight, camViewportNormalized.width * (float)targetWidth, camViewportNormalized.height * (float)targetHeight);
					cmd.SetViewport(scaledTargetViewportInPixels);
				}
				else
				{
					cmd.SetViewport(cameraData.pixelRect);
				}
			}
			Blitter.BlitTexture(cmd, sourceTextureHdl, scaleBias, material, 0);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001DFA4 File Offset: 0x0001C1A4
		public void RenderFinalSetup(RenderGraph renderGraph, UniversalCameraData cameraData, in TextureHandle source, in TextureHandle destination, ref PostProcessPass.FinalBlitSettings settings)
		{
			PostProcessPass.PostProcessingFinalSetupPassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<PostProcessPass.PostProcessingFinalSetupPassData>("Postprocessing Final Setup Pass", out passData, ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_FinalSetup), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 1410))
			{
				Material material = this.m_Materials.scalingSetup;
				if (settings.isFxaaEnabled)
				{
					material.EnableKeyword("_FXAA");
				}
				if (settings.isFsrEnabled)
				{
					material.EnableKeyword(settings.hdrOperations.HasFlag(HDROutputUtils.Operation.ColorEncoding) ? "_GAMMA_20_AND_HDR_INPUT" : "_GAMMA_20");
				}
				if (settings.hdrOperations.HasFlag(HDROutputUtils.Operation.ColorEncoding))
				{
					this.SetupHDROutput(cameraData.hdrDisplayInformation, cameraData.hdrDisplayColorGamut, material, settings.hdrOperations, cameraData.rendersOverlayUI);
				}
				if (settings.isAlphaOutputEnabled)
				{
					CoreUtils.SetKeyword(material, "_ENABLE_ALPHA_OUTPUT", settings.isAlphaOutputEnabled);
				}
				builder.AllowGlobalStateModification(true);
				passData.destinationTexture = destination;
				builder.SetRenderAttachment(destination, 0, AccessFlags.Write);
				passData.sourceTexture = source;
				builder.UseTexture(in source, AccessFlags.Read);
				passData.cameraData = cameraData;
				passData.material = material;
				builder.SetRenderFunc<PostProcessPass.PostProcessingFinalSetupPassData>(delegate(PostProcessPass.PostProcessingFinalSetupPassData data, RasterGraphContext context)
				{
					RasterCommandBuffer cmd = context.cmd;
					RTHandle sourceTextureHdl = data.sourceTexture;
					PostProcessUtils.SetSourceSize(cmd, sourceTextureHdl);
					bool hasFinalPass = true;
					PostProcessPass.ScaleViewportAndBlit(context.cmd, sourceTextureHdl, data.destinationTexture, data.cameraData, data.material, hasFinalPass);
				});
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001E10C File Offset: 0x0001C30C
		public void RenderFinalFSRScale(RenderGraph renderGraph, in TextureHandle source, in TextureHandle destination, bool enableAlphaOutput)
		{
			this.m_Materials.easu.shaderKeywords = null;
			PostProcessPass.PostProcessingFinalFSRScalePassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<PostProcessPass.PostProcessingFinalFSRScalePassData>("Postprocessing Final FSR Scale Pass", out passData, ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_FinalFSRScale), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 1461))
			{
				builder.AllowGlobalStateModification(true);
				passData.destinationTexture = destination;
				builder.SetRenderAttachment(destination, 0, AccessFlags.Write);
				passData.sourceTexture = source;
				builder.UseTexture(in source, AccessFlags.Read);
				passData.material = this.m_Materials.easu;
				passData.enableAlphaOutput = enableAlphaOutput;
				builder.SetRenderFunc<PostProcessPass.PostProcessingFinalFSRScalePassData>(delegate(PostProcessPass.PostProcessingFinalFSRScalePassData data, RasterGraphContext context)
				{
					RasterCommandBuffer cmd = context.cmd;
					TextureHandle sourceTexture = data.sourceTexture;
					TextureHandle destTex = data.destinationTexture;
					Material material = data.material;
					bool enableAlphaOutput2 = data.enableAlphaOutput;
					RTHandle sourceHdl = sourceTexture;
					RTHandle destHdl = destTex;
					Vector2 fsrInputSize = new Vector2((float)sourceHdl.referenceSize.x, (float)sourceHdl.referenceSize.y);
					Vector2 fsrOutputSize = new Vector2((float)destHdl.referenceSize.x, (float)destHdl.referenceSize.y);
					FSRUtils.SetEasuConstants(cmd, fsrInputSize, fsrInputSize, fsrOutputSize);
					CoreUtils.SetKeyword(material, "_ENABLE_ALPHA_OUTPUT", enableAlphaOutput2);
					Vector2 viewportScale = (sourceHdl.useScaling ? new Vector2(sourceHdl.rtHandleProperties.rtHandleScale.x, sourceHdl.rtHandleProperties.rtHandleScale.y) : Vector2.one);
					Blitter.BlitTexture(cmd, sourceHdl, viewportScale, material, 0);
				});
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001E1D8 File Offset: 0x0001C3D8
		public void RenderFinalBlit(RenderGraph renderGraph, UniversalCameraData cameraData, in TextureHandle source, in TextureHandle overlayUITexture, in TextureHandle postProcessingTarget, ref PostProcessPass.FinalBlitSettings settings)
		{
			PostProcessPass.PostProcessingFinalBlitPassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<PostProcessPass.PostProcessingFinalBlitPassData>("Postprocessing Final Blit Pass", out passData, ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_FinalBlit), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 1546))
			{
				builder.AllowGlobalStateModification(true);
				passData.destinationTexture = postProcessingTarget;
				builder.SetRenderAttachment(postProcessingTarget, 0, AccessFlags.Write);
				passData.sourceTexture = source;
				builder.UseTexture(in source, AccessFlags.Read);
				passData.cameraData = cameraData;
				passData.material = this.m_Materials.finalPass;
				passData.settings = settings;
				if (settings.requireHDROutput && this.m_EnableColorEncodingIfNeeded)
				{
					builder.UseTexture(in overlayUITexture, AccessFlags.Read);
				}
				if (cameraData.xr.enabled)
				{
					bool passSupportsFoveation = !XRSystem.foveatedRenderingCaps.HasFlag(FoveatedRenderingCaps.NonUniformRaster);
					builder.EnableFoveatedRasterization(cameraData.xr.supportsFoveatedRendering && passSupportsFoveation);
				}
				builder.SetRenderFunc<PostProcessPass.PostProcessingFinalBlitPassData>(delegate(PostProcessPass.PostProcessingFinalBlitPassData data, RasterGraphContext context)
				{
					RasterCommandBuffer cmd = context.cmd;
					Material material = data.material;
					bool isFxaaEnabled = data.settings.isFxaaEnabled;
					bool isFsrEnabled = data.settings.isFsrEnabled;
					bool isRcasEnabled = data.settings.isTaaSharpeningEnabled;
					bool requireHDROutput = data.settings.requireHDROutput;
					bool resolveToDebugScreen = data.settings.resolveToDebugScreen;
					bool isAlphaOutputEnabled = data.settings.isAlphaOutputEnabled;
					RTHandle sourceTextureHdl = data.sourceTexture;
					RTHandle destinationTextureHdl = data.destinationTexture;
					PostProcessUtils.SetSourceSize(cmd, data.sourceTexture);
					if (isFxaaEnabled)
					{
						material.EnableKeyword("_FXAA");
					}
					if (isFsrEnabled)
					{
						float sharpness = (data.cameraData.fsrOverrideSharpness ? data.cameraData.fsrSharpness : 0.92f);
						if (data.cameraData.fsrSharpness > 0f)
						{
							material.EnableKeyword(requireHDROutput ? "_EASU_RCAS_AND_HDR_INPUT" : "_RCAS");
							FSRUtils.SetRcasConstantsLinear(cmd, sharpness);
						}
					}
					else if (isRcasEnabled)
					{
						material.EnableKeyword("_RCAS");
						FSRUtils.SetRcasConstantsLinear(cmd, data.cameraData.taaSettings.contrastAdaptiveSharpening);
					}
					if (isAlphaOutputEnabled)
					{
						CoreUtils.SetKeyword(material, "_ENABLE_ALPHA_OUTPUT", isAlphaOutputEnabled);
					}
					bool isRenderToBackBufferTarget = !data.cameraData.isSceneViewCamera;
					if (data.cameraData.xr.enabled)
					{
						isRenderToBackBufferTarget = destinationTextureHdl == data.cameraData.xr.renderTarget;
					}
					isRenderToBackBufferTarget &= !resolveToDebugScreen;
					Vector2 viewportScale = (sourceTextureHdl.useScaling ? new Vector2(sourceTextureHdl.rtHandleProperties.rtHandleScale.x, sourceTextureHdl.rtHandleProperties.rtHandleScale.y) : Vector2.one);
					Vector4 scaleBias = ((isRenderToBackBufferTarget && data.cameraData.targetTexture == null && SystemInfo.graphicsUVStartsAtTop) ? new Vector4(viewportScale.x, -viewportScale.y, 0f, viewportScale.y) : new Vector4(viewportScale.x, viewportScale.y, 0f, 0f));
					cmd.SetViewport(data.cameraData.pixelRect);
					Blitter.BlitTexture(cmd, sourceTextureHdl, scaleBias, material, 0);
				});
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001E2F4 File Offset: 0x0001C4F4
		public void RenderFinalPassRenderGraph(RenderGraph renderGraph, ContextContainer frameData, in TextureHandle source, in TextureHandle overlayUITexture, in TextureHandle postProcessingTarget, bool enableColorEncodingIfNeeded)
		{
			VolumeStack stack = VolumeManager.instance.stack;
			this.m_Tonemapping = stack.GetComponent<Tonemapping>();
			this.m_FilmGrain = stack.GetComponent<FilmGrain>();
			this.m_Tonemapping = stack.GetComponent<Tonemapping>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			Material material = this.m_Materials.finalPass;
			material.shaderKeywords = null;
			PostProcessPass.FinalBlitSettings settings = PostProcessPass.FinalBlitSettings.Create();
			this.m_HasFinalPass = false;
			this.m_IsFinalPass = true;
			this.m_EnableColorEncodingIfNeeded = enableColorEncodingIfNeeded;
			if (this.m_FilmGrain.IsActive())
			{
				material.EnableKeyword("_FILM_GRAIN");
				PostProcessUtils.ConfigureFilmGrain(this.m_Data, this.m_FilmGrain, cameraData.pixelWidth, cameraData.pixelHeight, material);
			}
			if (cameraData.isDitheringEnabled)
			{
				material.EnableKeyword("_DITHERING");
				this.m_DitheringTextureIndex = PostProcessUtils.ConfigureDithering(this.m_Data, this.m_DitheringTextureIndex, cameraData.pixelWidth, cameraData.pixelHeight, material);
			}
			if (this.RequireSRGBConversionBlitToBackBuffer(cameraData.requireSrgbConversion))
			{
				material.EnableKeyword("_LINEAR_TO_SRGB_CONVERSION");
			}
			settings.hdrOperations = HDROutputUtils.Operation.None;
			settings.requireHDROutput = this.RequireHDROutput(cameraData);
			if (settings.requireHDROutput)
			{
				settings.hdrOperations = (this.m_EnableColorEncodingIfNeeded ? HDROutputUtils.Operation.ColorEncoding : HDROutputUtils.Operation.None);
				if (!cameraData.postProcessEnabled)
				{
					settings.hdrOperations |= HDROutputUtils.Operation.ColorConversion;
				}
				this.SetupHDROutput(cameraData.hdrDisplayInformation, cameraData.hdrDisplayColorGamut, material, settings.hdrOperations, cameraData.rendersOverlayUI);
			}
			DebugHandler debugHandler = ScriptableRenderPass.GetActiveDebugHandler(cameraData);
			if (debugHandler != null)
			{
				debugHandler.WriteToDebugScreenTexture(cameraData.resolveFinalTarget);
			}
			settings.isAlphaOutputEnabled = cameraData.isAlphaOutputEnabled;
			settings.isFxaaEnabled = cameraData.antialiasing == AntialiasingMode.FastApproximateAntialiasing;
			settings.isFsrEnabled = cameraData.imageScalingMode == ImageScalingMode.Upscaling && cameraData.upscalingFilter == ImageUpscalingFilter.FSR;
			settings.isTaaSharpeningEnabled = cameraData.IsTemporalAAEnabled() && cameraData.taaSettings.contrastAdaptiveSharpening > 0f && !settings.isFsrEnabled && !cameraData.IsSTPEnabled();
			RenderTextureDescriptor tempRtDesc = cameraData.cameraTargetDescriptor;
			tempRtDesc.msaaSamples = 1;
			tempRtDesc.depthStencilFormat = GraphicsFormat.None;
			if (!settings.requireHDROutput)
			{
				tempRtDesc.graphicsFormat = UniversalRenderPipeline.MakeUnormRenderTextureGraphicsFormat();
			}
			TextureHandle scalingSetupTarget = UniversalRenderer.CreateRenderGraphTexture(renderGraph, tempRtDesc, "scalingSetupTarget", true, FilterMode.Point, TextureWrapMode.Clamp);
			RenderTextureDescriptor upscaleRtDesc = cameraData.cameraTargetDescriptor;
			upscaleRtDesc.msaaSamples = 1;
			upscaleRtDesc.depthStencilFormat = GraphicsFormat.None;
			upscaleRtDesc.width = cameraData.pixelWidth;
			upscaleRtDesc.height = cameraData.pixelHeight;
			TextureHandle upScaleTarget = UniversalRenderer.CreateRenderGraphTexture(renderGraph, upscaleRtDesc, "_UpscaledTexture", true, FilterMode.Point, TextureWrapMode.Clamp);
			TextureHandle currentSource = source;
			if (cameraData.imageScalingMode != ImageScalingMode.None)
			{
				if (settings.isFxaaEnabled || settings.isFsrEnabled)
				{
					this.RenderFinalSetup(renderGraph, cameraData, in currentSource, in scalingSetupTarget, ref settings);
					currentSource = scalingSetupTarget;
					settings.isFxaaEnabled = false;
				}
				ImageScalingMode imageScalingMode = cameraData.imageScalingMode;
				if (imageScalingMode != ImageScalingMode.Upscaling)
				{
					if (imageScalingMode == ImageScalingMode.Downscaling)
					{
						settings.isTaaSharpeningEnabled = false;
					}
				}
				else
				{
					switch (cameraData.upscalingFilter)
					{
					case ImageUpscalingFilter.Point:
						if (!settings.isTaaSharpeningEnabled)
						{
							material.EnableKeyword("_POINT_SAMPLING");
						}
						break;
					case ImageUpscalingFilter.FSR:
						this.RenderFinalFSRScale(renderGraph, in currentSource, in upScaleTarget, settings.isAlphaOutputEnabled);
						currentSource = upScaleTarget;
						break;
					}
				}
			}
			else if (settings.isFxaaEnabled)
			{
				material.EnableKeyword("_FXAA");
			}
			this.RenderFinalBlit(renderGraph, cameraData, in currentSource, in overlayUITexture, in postProcessingTarget, ref settings);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001E628 File Offset: 0x0001C828
		private TextureHandle TryGetCachedUserLutTextureHandle(RenderGraph renderGraph)
		{
			if (this.m_ColorLookup.texture.value == null)
			{
				if (this.m_UserLut != null)
				{
					this.m_UserLut.Release();
					this.m_UserLut = null;
				}
			}
			else if (this.m_UserLut == null || this.m_UserLut.externalTexture != this.m_ColorLookup.texture.value)
			{
				RTHandle userLut = this.m_UserLut;
				if (userLut != null)
				{
					userLut.Release();
				}
				this.m_UserLut = RTHandles.Alloc(this.m_ColorLookup.texture.value);
			}
			if (this.m_UserLut == null)
			{
				return TextureHandle.nullHandle;
			}
			return renderGraph.ImportTexture(this.m_UserLut);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001E6DC File Offset: 0x0001C8DC
		public void RenderUberPost(RenderGraph renderGraph, ContextContainer frameData, UniversalCameraData cameraData, UniversalPostProcessingData postProcessingData, in TextureHandle sourceTexture, in TextureHandle destTexture, in TextureHandle lutTexture, in TextureHandle overlayUITexture, bool requireHDROutput, bool enableAlphaOutput, bool resolveToDebugScreen, bool hasFinalPass)
		{
			Material material = this.m_Materials.uber;
			bool hdrGrading = postProcessingData.gradingMode == ColorGradingMode.HighDynamicRange;
			int lutHeight = postProcessingData.lutSize;
			int lutWidth = lutHeight * lutHeight;
			float postExposureLinear = Mathf.Pow(2f, this.m_ColorAdjustments.postExposure.value);
			Vector4 lutParams = new Vector4(1f / (float)lutWidth, 1f / (float)lutHeight, (float)lutHeight - 1f, postExposureLinear);
			TextureHandle userLutTexture = this.TryGetCachedUserLutTextureHandle(renderGraph);
			Vector4 userLutParams = ((!this.m_ColorLookup.IsActive()) ? Vector4.zero : new Vector4(1f / (float)this.m_ColorLookup.texture.value.width, 1f / (float)this.m_ColorLookup.texture.value.height, (float)this.m_ColorLookup.texture.value.height - 1f, this.m_ColorLookup.contribution.value));
			PostProcessPass.UberPostPassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<PostProcessPass.UberPostPassData>("Blit Post Processing", out passData, ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_UberPost), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 1855))
			{
				UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
				if (cameraData.xr.enabled)
				{
					bool passSupportsFoveation = cameraData.xrUniversal.canFoveateIntermediatePasses || resourceData.isActiveTargetBackBuffer;
					passSupportsFoveation &= !XRSystem.foveatedRenderingCaps.HasFlag(FoveatedRenderingCaps.NonUniformRaster);
					builder.EnableFoveatedRasterization(cameraData.xr.supportsFoveatedRendering && passSupportsFoveation);
				}
				builder.AllowGlobalStateModification(true);
				passData.destinationTexture = destTexture;
				builder.SetRenderAttachment(destTexture, 0, AccessFlags.Write);
				passData.sourceTexture = sourceTexture;
				builder.UseTexture(in sourceTexture, AccessFlags.Read);
				passData.lutTexture = lutTexture;
				builder.UseTexture(in lutTexture, AccessFlags.Read);
				passData.lutParams = lutParams;
				if (userLutTexture.IsValid())
				{
					passData.userLutTexture = userLutTexture;
					builder.UseTexture(in userLutTexture, AccessFlags.Read);
				}
				if (this.m_Bloom.IsActive())
				{
					builder.UseTexture(in this._BloomMipUp[0], AccessFlags.Read);
				}
				if (requireHDROutput && this.m_EnableColorEncodingIfNeeded)
				{
					TextureHandle textureHandle = overlayUITexture;
					if (textureHandle.IsValid())
					{
						builder.UseTexture(in overlayUITexture, AccessFlags.Read);
					}
				}
				passData.userLutParams = userLutParams;
				passData.cameraData = cameraData;
				passData.material = material;
				passData.toneMappingMode = this.m_Tonemapping.mode.value;
				passData.isHdrGrading = hdrGrading;
				passData.enableAlphaOutput = enableAlphaOutput;
				passData.hasFinalPass = hasFinalPass;
				builder.SetRenderFunc<PostProcessPass.UberPostPassData>(delegate(PostProcessPass.UberPostPassData data, RasterGraphContext context)
				{
					RasterCommandBuffer cmd = context.cmd;
					Camera camera = data.cameraData.camera;
					Material material2 = data.material;
					RTHandle sourceTextureHdl = data.sourceTexture;
					material2.SetTexture(PostProcessPass.ShaderConstants._InternalLut, data.lutTexture);
					material2.SetVector(PostProcessPass.ShaderConstants._Lut_Params, data.lutParams);
					material2.SetTexture(PostProcessPass.ShaderConstants._UserLut, data.userLutTexture);
					material2.SetVector(PostProcessPass.ShaderConstants._UserLut_Params, data.userLutParams);
					if (data.isHdrGrading)
					{
						material2.EnableKeyword("_HDR_GRADING");
					}
					else
					{
						TonemappingMode toneMappingMode = data.toneMappingMode;
						if (toneMappingMode != TonemappingMode.Neutral)
						{
							if (toneMappingMode == TonemappingMode.ACES)
							{
								material2.EnableKeyword("_TONEMAP_ACES");
							}
						}
						else
						{
							material2.EnableKeyword("_TONEMAP_NEUTRAL");
						}
					}
					CoreUtils.SetKeyword(material2, "_ENABLE_ALPHA_OUTPUT", data.enableAlphaOutput);
					PostProcessPass.ScaleViewportAndBlit(cmd, sourceTextureHdl, data.destinationTexture, data.cameraData, material2, data.hasFinalPass);
				});
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001E9B0 File Offset: 0x0001CBB0
		public void RenderPostProcessingRenderGraph(RenderGraph renderGraph, ContextContainer frameData, in TextureHandle activeCameraColorTexture, in TextureHandle lutTexture, in TextureHandle overlayUITexture, in TextureHandle postProcessingTarget, bool hasFinalPass, bool resolveToDebugScreen, bool enableColorEndingIfNeeded)
		{
			UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
			frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalPostProcessingData postProcessingData = frameData.Get<UniversalPostProcessingData>();
			VolumeStack stack = VolumeManager.instance.stack;
			this.m_DepthOfField = stack.GetComponent<DepthOfField>();
			this.m_MotionBlur = stack.GetComponent<MotionBlur>();
			this.m_PaniniProjection = stack.GetComponent<PaniniProjection>();
			this.m_Bloom = stack.GetComponent<Bloom>();
			this.m_LensFlareScreenSpace = stack.GetComponent<ScreenSpaceLensFlare>();
			this.m_LensDistortion = stack.GetComponent<LensDistortion>();
			this.m_ChromaticAberration = stack.GetComponent<ChromaticAberration>();
			this.m_Vignette = stack.GetComponent<Vignette>();
			this.m_ColorLookup = stack.GetComponent<ColorLookup>();
			this.m_ColorAdjustments = stack.GetComponent<ColorAdjustments>();
			this.m_Tonemapping = stack.GetComponent<Tonemapping>();
			this.m_FilmGrain = stack.GetComponent<FilmGrain>();
			this.m_UseFastSRGBLinearConversion = postProcessingData.useFastSRGBLinearConversion;
			this.m_SupportDataDrivenLensFlare = postProcessingData.supportDataDrivenLensFlare;
			this.m_SupportScreenSpaceLensFlare = postProcessingData.supportScreenSpaceLensFlare;
			this.m_Descriptor = cameraData.cameraTargetDescriptor;
			this.m_Descriptor.useMipMap = false;
			this.m_Descriptor.autoGenerateMips = false;
			this.m_HasFinalPass = hasFinalPass;
			this.m_EnableColorEncodingIfNeeded = enableColorEndingIfNeeded;
			ref ScriptableRenderer renderer = ref cameraData.renderer;
			bool isSceneViewCamera = cameraData.isSceneViewCamera;
			bool useStopNan = cameraData.isStopNaNEnabled && this.m_Materials.stopNaN != null;
			bool useSubPixelMorpAA = cameraData.antialiasing == AntialiasingMode.SubpixelMorphologicalAntiAliasing;
			Material dofMaterial = ((this.m_DepthOfField.mode.value == DepthOfFieldMode.Gaussian) ? this.m_Materials.gaussianDepthOfField : this.m_Materials.bokehDepthOfField);
			bool useDepthOfField = this.m_DepthOfField.IsActive() && !isSceneViewCamera && dofMaterial != null;
			bool useLensFlare = !LensFlareCommonSRP.Instance.IsEmpty() && this.m_SupportDataDrivenLensFlare;
			bool useLensFlareScreenSpace = this.m_LensFlareScreenSpace.IsActive() && this.m_SupportScreenSpaceLensFlare;
			bool useMotionBlur = this.m_MotionBlur.IsActive() && !isSceneViewCamera;
			bool usePaniniProjection = this.m_PaniniProjection.IsActive() && !isSceneViewCamera;
			if (cameraData.imageScalingMode == ImageScalingMode.Upscaling)
			{
				bool flag = cameraData.upscalingFilter == ImageUpscalingFilter.FSR;
			}
			useMotionBlur = useMotionBlur && Application.isPlaying;
			if (useMotionBlur && this.m_MotionBlur.mode.value == MotionBlurMode.CameraAndObjects)
			{
				useMotionBlur &= renderer.SupportsMotionVectors();
				if (!useMotionBlur)
				{
					string warning = "Disabling Motion Blur for Camera And Objects because the renderer does not implement motion vectors.";
					if (Time.frameCount % 60 == 0)
					{
						Debug.LogWarning(warning);
					}
				}
			}
			bool useTemporalAA = cameraData.IsTemporalAAEnabled();
			if (cameraData.antialiasing == AntialiasingMode.TemporalAntiAliasing && !useTemporalAA)
			{
				TemporalAA.ValidateAndWarn(cameraData);
			}
			bool useSTP = useTemporalAA && cameraData.IsSTPEnabled();
			PostProcessPass.PostFXSetupPassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<PostProcessPass.PostFXSetupPassData>("Setup PostFX passes", out passData, ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_SetupPostFX), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/PostProcessPassRenderGraph.cs", 2005))
			{
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				builder.SetRenderFunc<PostProcessPass.PostFXSetupPassData>(delegate(PostProcessPass.PostFXSetupPassData data, RasterGraphContext context)
				{
					context.cmd.SetGlobalMatrix(PostProcessPass.ShaderConstants._FullscreenProjMat, GL.GetGPUProjectionMatrix(Matrix4x4.identity, true));
				});
			}
			TextureHandle currentSource = activeCameraColorTexture;
			if (useStopNan)
			{
				TextureHandle stopNaNTarget;
				this.RenderStopNaN(renderGraph, cameraData.cameraTargetDescriptor, in currentSource, out stopNaNTarget);
				currentSource = stopNaNTarget;
			}
			if (useSubPixelMorpAA)
			{
				TextureHandle SMAATarget;
				this.RenderSMAA(renderGraph, resourceData, cameraData.antialiasingQuality, in currentSource, out SMAATarget);
				currentSource = SMAATarget;
			}
			if (useDepthOfField)
			{
				TextureHandle DoFTarget;
				this.RenderDoF(renderGraph, resourceData, cameraData, in currentSource, out DoFTarget);
				currentSource = DoFTarget;
			}
			if (useTemporalAA)
			{
				if (useSTP)
				{
					TextureHandle StpTarget;
					this.RenderSTP(renderGraph, resourceData, cameraData, ref currentSource, out StpTarget);
					currentSource = StpTarget;
				}
				else
				{
					TextureHandle TemporalAATarget;
					this.RenderTemporalAA(renderGraph, resourceData, cameraData, ref currentSource, out TemporalAATarget);
					currentSource = TemporalAATarget;
				}
			}
			if (useMotionBlur)
			{
				TextureHandle MotionBlurTarget;
				this.RenderMotionBlur(renderGraph, resourceData, cameraData, in currentSource, out MotionBlurTarget);
				currentSource = MotionBlurTarget;
			}
			if (usePaniniProjection)
			{
				TextureHandle PaniniTarget;
				this.RenderPaniniProjection(renderGraph, cameraData.camera, in currentSource, out PaniniTarget);
				currentSource = PaniniTarget;
			}
			this.m_Materials.uber.shaderKeywords = null;
			if (this.m_Bloom.IsActive() || useLensFlareScreenSpace)
			{
				TextureHandle BloomTexture;
				this.RenderBloomTexture(renderGraph, in currentSource, out BloomTexture, cameraData.isAlphaOutputEnabled);
				if (useLensFlareScreenSpace)
				{
					int maxBloomMip = Mathf.Clamp(this.m_LensFlareScreenSpace.bloomMip.value, 0, this.m_Bloom.maxIterations.value / 2);
					BloomTexture = this.RenderLensFlareScreenSpace(renderGraph, cameraData.camera, in currentSource, this._BloomMipUp[0], this._BloomMipUp[maxBloomMip], cameraData.xr.enabled);
				}
				this.UberPostSetupBloomPass(renderGraph, in BloomTexture, this.m_Materials.uber);
			}
			if (useLensFlare)
			{
				this.LensFlareDataDrivenComputeOcclusion(renderGraph, resourceData, cameraData);
				this.RenderLensFlareDataDriven(renderGraph, resourceData, cameraData, in currentSource);
			}
			this.SetupLensDistortion(this.m_Materials.uber, isSceneViewCamera);
			this.SetupChromaticAberration(this.m_Materials.uber);
			this.SetupVignette(this.m_Materials.uber, cameraData.xr);
			this.SetupGrain(cameraData, this.m_Materials.uber);
			this.SetupDithering(cameraData, this.m_Materials.uber);
			if (this.RequireSRGBConversionBlitToBackBuffer(cameraData.requireSrgbConversion))
			{
				this.m_Materials.uber.EnableKeyword("_LINEAR_TO_SRGB_CONVERSION");
			}
			if (this.m_UseFastSRGBLinearConversion)
			{
				this.m_Materials.uber.EnableKeyword("_USE_FAST_SRGB_LINEAR_CONVERSION");
			}
			bool requireHDROutput = this.RequireHDROutput(cameraData);
			if (requireHDROutput)
			{
				HDROutputUtils.Operation hdrOperations = ((!this.m_HasFinalPass && this.m_EnableColorEncodingIfNeeded) ? HDROutputUtils.Operation.ColorEncoding : HDROutputUtils.Operation.None);
				this.SetupHDROutput(cameraData.hdrDisplayInformation, cameraData.hdrDisplayColorGamut, this.m_Materials.uber, hdrOperations, cameraData.rendersOverlayUI);
			}
			bool enableAlphaOutput = cameraData.isAlphaOutputEnabled;
			ScriptableRenderPass.GetActiveDebugHandler(cameraData);
			this.RenderUberPost(renderGraph, frameData, cameraData, postProcessingData, in currentSource, in postProcessingTarget, in lutTexture, in overlayUITexture, requireHDROutput, enableAlphaOutput, resolveToDebugScreen, hasFinalPass);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001EFA6 File Offset: 0x0001D1A6
		[CompilerGenerated]
		private RTHandle <Render>g__GetSource|90_0(ref PostProcessPass.<>c__DisplayClass90_0 A_1)
		{
			return A_1.source;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001EFB0 File Offset: 0x0001D1B0
		[CompilerGenerated]
		private RTHandle <Render>g__GetDestination|90_1(ref PostProcessPass.<>c__DisplayClass90_0 A_1)
		{
			if (A_1.destination == null)
			{
				RenderTextureDescriptor renderTextureDescriptor = this.GetCompatibleDescriptor();
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_TempTarget, in renderTextureDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_TempTarget");
				A_1.destination = this.m_TempTarget;
			}
			else if (A_1.destination == this.m_Source && this.m_Descriptor.msaaSamples > 1)
			{
				RenderTextureDescriptor renderTextureDescriptor = this.GetCompatibleDescriptor();
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_TempTarget2, in renderTextureDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_TempTarget2");
				A_1.destination = this.m_TempTarget2;
			}
			return A_1.destination;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001F048 File Offset: 0x0001D248
		[CompilerGenerated]
		private void <Render>g__Swap|90_2(ref ScriptableRenderer r, ref PostProcessPass.<>c__DisplayClass90_0 A_2)
		{
			int num = A_2.amountOfPassesRemaining - 1;
			A_2.amountOfPassesRemaining = num;
			if (this.m_UseSwapBuffer)
			{
				r.SwapColorBuffer(A_2.cmd);
				A_2.source = r.cameraColorTargetHandle;
				if (A_2.amountOfPassesRemaining == 0 && !this.m_HasFinalPass)
				{
					r.EnableSwapBufferMSAA(true);
				}
				A_2.destination = r.GetCameraColorFrontBuffer(A_2.cmd);
				return;
			}
			CoreUtils.Swap<RTHandle>(ref A_2.source, ref A_2.destination);
		}

		// Token: 0x040005F0 RID: 1520
		private RenderTextureDescriptor m_Descriptor;

		// Token: 0x040005F1 RID: 1521
		private RTHandle m_Source;

		// Token: 0x040005F2 RID: 1522
		private RTHandle m_Destination;

		// Token: 0x040005F3 RID: 1523
		private RTHandle m_Depth;

		// Token: 0x040005F4 RID: 1524
		private RTHandle m_InternalLut;

		// Token: 0x040005F5 RID: 1525
		private RTHandle m_MotionVectors;

		// Token: 0x040005F6 RID: 1526
		private RTHandle m_FullCoCTexture;

		// Token: 0x040005F7 RID: 1527
		private RTHandle m_HalfCoCTexture;

		// Token: 0x040005F8 RID: 1528
		private RTHandle m_PingTexture;

		// Token: 0x040005F9 RID: 1529
		private RTHandle m_PongTexture;

		// Token: 0x040005FA RID: 1530
		private RTHandle[] m_BloomMipDown;

		// Token: 0x040005FB RID: 1531
		private RTHandle[] m_BloomMipUp;

		// Token: 0x040005FC RID: 1532
		private TextureHandle[] _BloomMipUp;

		// Token: 0x040005FD RID: 1533
		private TextureHandle[] _BloomMipDown;

		// Token: 0x040005FE RID: 1534
		private RTHandle m_BlendTexture;

		// Token: 0x040005FF RID: 1535
		private RTHandle m_EdgeColorTexture;

		// Token: 0x04000600 RID: 1536
		private RTHandle m_EdgeStencilTexture;

		// Token: 0x04000601 RID: 1537
		private RTHandle m_TempTarget;

		// Token: 0x04000602 RID: 1538
		private RTHandle m_TempTarget2;

		// Token: 0x04000603 RID: 1539
		private RTHandle m_StreakTmpTexture;

		// Token: 0x04000604 RID: 1540
		private RTHandle m_StreakTmpTexture2;

		// Token: 0x04000605 RID: 1541
		private RTHandle m_ScreenSpaceLensFlareResult;

		// Token: 0x04000606 RID: 1542
		private RTHandle m_UserLut;

		// Token: 0x04000607 RID: 1543
		private const string k_RenderPostProcessingTag = "Blit PostProcessing Effects";

		// Token: 0x04000608 RID: 1544
		private const string k_RenderFinalPostProcessingTag = "Blit Final PostProcessing";

		// Token: 0x04000609 RID: 1545
		private static readonly ProfilingSampler m_ProfilingRenderPostProcessing = new ProfilingSampler("Blit PostProcessing Effects");

		// Token: 0x0400060A RID: 1546
		private static readonly ProfilingSampler m_ProfilingRenderFinalPostProcessing = new ProfilingSampler("Blit Final PostProcessing");

		// Token: 0x0400060B RID: 1547
		private PostProcessPass.MaterialLibrary m_Materials;

		// Token: 0x0400060C RID: 1548
		private PostProcessData m_Data;

		// Token: 0x0400060D RID: 1549
		private DepthOfField m_DepthOfField;

		// Token: 0x0400060E RID: 1550
		private MotionBlur m_MotionBlur;

		// Token: 0x0400060F RID: 1551
		private ScreenSpaceLensFlare m_LensFlareScreenSpace;

		// Token: 0x04000610 RID: 1552
		private PaniniProjection m_PaniniProjection;

		// Token: 0x04000611 RID: 1553
		private Bloom m_Bloom;

		// Token: 0x04000612 RID: 1554
		private LensDistortion m_LensDistortion;

		// Token: 0x04000613 RID: 1555
		private ChromaticAberration m_ChromaticAberration;

		// Token: 0x04000614 RID: 1556
		private Vignette m_Vignette;

		// Token: 0x04000615 RID: 1557
		private ColorLookup m_ColorLookup;

		// Token: 0x04000616 RID: 1558
		private ColorAdjustments m_ColorAdjustments;

		// Token: 0x04000617 RID: 1559
		private Tonemapping m_Tonemapping;

		// Token: 0x04000618 RID: 1560
		private FilmGrain m_FilmGrain;

		// Token: 0x04000619 RID: 1561
		private const int k_GaussianDoFPassComputeCoc = 0;

		// Token: 0x0400061A RID: 1562
		private const int k_GaussianDoFPassDownscalePrefilter = 1;

		// Token: 0x0400061B RID: 1563
		private const int k_GaussianDoFPassBlurH = 2;

		// Token: 0x0400061C RID: 1564
		private const int k_GaussianDoFPassBlurV = 3;

		// Token: 0x0400061D RID: 1565
		private const int k_GaussianDoFPassComposite = 4;

		// Token: 0x0400061E RID: 1566
		private const int k_BokehDoFPassComputeCoc = 0;

		// Token: 0x0400061F RID: 1567
		private const int k_BokehDoFPassDownscalePrefilter = 1;

		// Token: 0x04000620 RID: 1568
		private const int k_BokehDoFPassBlur = 2;

		// Token: 0x04000621 RID: 1569
		private const int k_BokehDoFPassPostFilter = 3;

		// Token: 0x04000622 RID: 1570
		private const int k_BokehDoFPassComposite = 4;

		// Token: 0x04000623 RID: 1571
		private const int k_MaxPyramidSize = 16;

		// Token: 0x04000624 RID: 1572
		private readonly GraphicsFormat m_DefaultColorFormat;

		// Token: 0x04000625 RID: 1573
		private bool m_DefaultColorFormatIsAlpha;

		// Token: 0x04000626 RID: 1574
		private readonly GraphicsFormat m_SMAAEdgeFormat;

		// Token: 0x04000627 RID: 1575
		private readonly GraphicsFormat m_GaussianCoCFormat;

		// Token: 0x04000628 RID: 1576
		private int m_DitheringTextureIndex;

		// Token: 0x04000629 RID: 1577
		private RenderTargetIdentifier[] m_MRT2;

		// Token: 0x0400062A RID: 1578
		private Vector4[] m_BokehKernel;

		// Token: 0x0400062B RID: 1579
		private int m_BokehHash;

		// Token: 0x0400062C RID: 1580
		private float m_BokehMaxRadius;

		// Token: 0x0400062D RID: 1581
		private float m_BokehRCPAspect;

		// Token: 0x0400062E RID: 1582
		private bool m_IsFinalPass;

		// Token: 0x0400062F RID: 1583
		private bool m_HasFinalPass;

		// Token: 0x04000630 RID: 1584
		private bool m_EnableColorEncodingIfNeeded;

		// Token: 0x04000631 RID: 1585
		private bool m_UseFastSRGBLinearConversion;

		// Token: 0x04000632 RID: 1586
		private bool m_SupportScreenSpaceLensFlare;

		// Token: 0x04000633 RID: 1587
		private bool m_SupportDataDrivenLensFlare;

		// Token: 0x04000634 RID: 1588
		private bool m_ResolveToScreen;

		// Token: 0x04000635 RID: 1589
		private bool m_UseSwapBuffer;

		// Token: 0x04000636 RID: 1590
		private RTHandle m_ScalingSetupTarget;

		// Token: 0x04000637 RID: 1591
		private RTHandle m_UpscaledTarget;

		// Token: 0x04000638 RID: 1592
		private Material m_BlitMaterial;

		// Token: 0x04000639 RID: 1593
		private PostProcessPass.BloomMaterialParams m_BloomParamsPrev;

		// Token: 0x0400063A RID: 1594
		internal static readonly int k_ShaderPropertyId_ViewProjM = Shader.PropertyToID("_ViewProjM");

		// Token: 0x0400063B RID: 1595
		internal static readonly int k_ShaderPropertyId_PrevViewProjM = Shader.PropertyToID("_PrevViewProjM");

		// Token: 0x0400063C RID: 1596
		internal static readonly int k_ShaderPropertyId_ViewProjMStereo = Shader.PropertyToID("_ViewProjMStereo");

		// Token: 0x0400063D RID: 1597
		internal static readonly int k_ShaderPropertyId_PrevViewProjMStereo = Shader.PropertyToID("_PrevViewProjMStereo");

		// Token: 0x0400063E RID: 1598
		private static readonly int s_CameraDepthTextureID = Shader.PropertyToID("_CameraDepthTexture");

		// Token: 0x0400063F RID: 1599
		private const string _TemporalAATargetName = "_TemporalAATarget";

		// Token: 0x04000640 RID: 1600
		private const string _UpscaledColorTargetName = "_UpscaledColorTarget";

		// Token: 0x02000122 RID: 290
		private class MaterialLibrary
		{
			// Token: 0x060006BC RID: 1724 RVA: 0x0001F0C4 File Offset: 0x0001D2C4
			public MaterialLibrary(PostProcessData data)
			{
				this.stopNaN = this.Load(data.shaders.stopNanPS);
				this.subpixelMorphologicalAntialiasing = this.Load(data.shaders.subpixelMorphologicalAntialiasingPS);
				this.gaussianDepthOfField = this.Load(data.shaders.gaussianDepthOfFieldPS);
				this.gaussianDepthOfFieldCoC = this.Load(data.shaders.gaussianDepthOfFieldPS);
				this.bokehDepthOfField = this.Load(data.shaders.bokehDepthOfFieldPS);
				this.bokehDepthOfFieldCoC = this.Load(data.shaders.bokehDepthOfFieldPS);
				this.cameraMotionBlur = this.Load(data.shaders.cameraMotionBlurPS);
				this.paniniProjection = this.Load(data.shaders.paniniProjectionPS);
				this.bloom = this.Load(data.shaders.bloomPS);
				this.temporalAntialiasing = this.Load(data.shaders.temporalAntialiasingPS);
				this.scalingSetup = this.Load(data.shaders.scalingSetupPS);
				this.easu = this.Load(data.shaders.easuPS);
				this.uber = this.Load(data.shaders.uberPostPS);
				this.finalPass = this.Load(data.shaders.finalPostPassPS);
				this.lensFlareDataDriven = this.Load(data.shaders.LensFlareDataDrivenPS);
				this.lensFlareScreenSpace = this.Load(data.shaders.LensFlareScreenSpacePS);
				this.bloomUpsample = new Material[16];
				for (uint i = 0U; i < 16U; i += 1U)
				{
					this.bloomUpsample[(int)i] = this.Load(data.shaders.bloomPS);
				}
			}

			// Token: 0x060006BD RID: 1725 RVA: 0x0001F27A File Offset: 0x0001D47A
			private Material Load(Shader shader)
			{
				if (shader == null)
				{
					Debug.LogErrorFormat("Missing shader. PostProcessing render passes will not execute. Check for missing reference in the renderer resources.", Array.Empty<object>());
					return null;
				}
				if (!shader.isSupported)
				{
					return null;
				}
				return CoreUtils.CreateEngineMaterial(shader);
			}

			// Token: 0x060006BE RID: 1726 RVA: 0x0001F2A8 File Offset: 0x0001D4A8
			internal void Cleanup()
			{
				CoreUtils.Destroy(this.stopNaN);
				CoreUtils.Destroy(this.subpixelMorphologicalAntialiasing);
				CoreUtils.Destroy(this.gaussianDepthOfField);
				CoreUtils.Destroy(this.gaussianDepthOfFieldCoC);
				CoreUtils.Destroy(this.bokehDepthOfField);
				CoreUtils.Destroy(this.bokehDepthOfFieldCoC);
				CoreUtils.Destroy(this.cameraMotionBlur);
				CoreUtils.Destroy(this.paniniProjection);
				CoreUtils.Destroy(this.bloom);
				CoreUtils.Destroy(this.temporalAntialiasing);
				CoreUtils.Destroy(this.scalingSetup);
				CoreUtils.Destroy(this.easu);
				CoreUtils.Destroy(this.uber);
				CoreUtils.Destroy(this.finalPass);
				CoreUtils.Destroy(this.lensFlareDataDriven);
				CoreUtils.Destroy(this.lensFlareScreenSpace);
				for (uint i = 0U; i < 16U; i += 1U)
				{
					CoreUtils.Destroy(this.bloomUpsample[(int)i]);
				}
			}

			// Token: 0x04000641 RID: 1601
			public readonly Material stopNaN;

			// Token: 0x04000642 RID: 1602
			public readonly Material subpixelMorphologicalAntialiasing;

			// Token: 0x04000643 RID: 1603
			public readonly Material gaussianDepthOfField;

			// Token: 0x04000644 RID: 1604
			public readonly Material gaussianDepthOfFieldCoC;

			// Token: 0x04000645 RID: 1605
			public readonly Material bokehDepthOfField;

			// Token: 0x04000646 RID: 1606
			public readonly Material bokehDepthOfFieldCoC;

			// Token: 0x04000647 RID: 1607
			public readonly Material cameraMotionBlur;

			// Token: 0x04000648 RID: 1608
			public readonly Material paniniProjection;

			// Token: 0x04000649 RID: 1609
			public readonly Material bloom;

			// Token: 0x0400064A RID: 1610
			public readonly Material[] bloomUpsample;

			// Token: 0x0400064B RID: 1611
			public readonly Material temporalAntialiasing;

			// Token: 0x0400064C RID: 1612
			public readonly Material scalingSetup;

			// Token: 0x0400064D RID: 1613
			public readonly Material easu;

			// Token: 0x0400064E RID: 1614
			public readonly Material uber;

			// Token: 0x0400064F RID: 1615
			public readonly Material finalPass;

			// Token: 0x04000650 RID: 1616
			public readonly Material lensFlareDataDriven;

			// Token: 0x04000651 RID: 1617
			public readonly Material lensFlareScreenSpace;
		}

		// Token: 0x02000123 RID: 291
		private static class ShaderConstants
		{
			// Token: 0x04000652 RID: 1618
			public static readonly int _TempTarget = Shader.PropertyToID("_TempTarget");

			// Token: 0x04000653 RID: 1619
			public static readonly int _TempTarget2 = Shader.PropertyToID("_TempTarget2");

			// Token: 0x04000654 RID: 1620
			public static readonly int _StencilRef = Shader.PropertyToID("_StencilRef");

			// Token: 0x04000655 RID: 1621
			public static readonly int _StencilMask = Shader.PropertyToID("_StencilMask");

			// Token: 0x04000656 RID: 1622
			public static readonly int _FullCoCTexture = Shader.PropertyToID("_FullCoCTexture");

			// Token: 0x04000657 RID: 1623
			public static readonly int _HalfCoCTexture = Shader.PropertyToID("_HalfCoCTexture");

			// Token: 0x04000658 RID: 1624
			public static readonly int _DofTexture = Shader.PropertyToID("_DofTexture");

			// Token: 0x04000659 RID: 1625
			public static readonly int _CoCParams = Shader.PropertyToID("_CoCParams");

			// Token: 0x0400065A RID: 1626
			public static readonly int _BokehKernel = Shader.PropertyToID("_BokehKernel");

			// Token: 0x0400065B RID: 1627
			public static readonly int _BokehConstants = Shader.PropertyToID("_BokehConstants");

			// Token: 0x0400065C RID: 1628
			public static readonly int _PongTexture = Shader.PropertyToID("_PongTexture");

			// Token: 0x0400065D RID: 1629
			public static readonly int _PingTexture = Shader.PropertyToID("_PingTexture");

			// Token: 0x0400065E RID: 1630
			public static readonly int _Metrics = Shader.PropertyToID("_Metrics");

			// Token: 0x0400065F RID: 1631
			public static readonly int _AreaTexture = Shader.PropertyToID("_AreaTexture");

			// Token: 0x04000660 RID: 1632
			public static readonly int _SearchTexture = Shader.PropertyToID("_SearchTexture");

			// Token: 0x04000661 RID: 1633
			public static readonly int _EdgeTexture = Shader.PropertyToID("_EdgeTexture");

			// Token: 0x04000662 RID: 1634
			public static readonly int _BlendTexture = Shader.PropertyToID("_BlendTexture");

			// Token: 0x04000663 RID: 1635
			public static readonly int _ColorTexture = Shader.PropertyToID("_ColorTexture");

			// Token: 0x04000664 RID: 1636
			public static readonly int _Params = Shader.PropertyToID("_Params");

			// Token: 0x04000665 RID: 1637
			public static readonly int _SourceTexLowMip = Shader.PropertyToID("_SourceTexLowMip");

			// Token: 0x04000666 RID: 1638
			public static readonly int _Bloom_Params = Shader.PropertyToID("_Bloom_Params");

			// Token: 0x04000667 RID: 1639
			public static readonly int _Bloom_Texture = Shader.PropertyToID("_Bloom_Texture");

			// Token: 0x04000668 RID: 1640
			public static readonly int _LensDirt_Texture = Shader.PropertyToID("_LensDirt_Texture");

			// Token: 0x04000669 RID: 1641
			public static readonly int _LensDirt_Params = Shader.PropertyToID("_LensDirt_Params");

			// Token: 0x0400066A RID: 1642
			public static readonly int _LensDirt_Intensity = Shader.PropertyToID("_LensDirt_Intensity");

			// Token: 0x0400066B RID: 1643
			public static readonly int _Distortion_Params1 = Shader.PropertyToID("_Distortion_Params1");

			// Token: 0x0400066C RID: 1644
			public static readonly int _Distortion_Params2 = Shader.PropertyToID("_Distortion_Params2");

			// Token: 0x0400066D RID: 1645
			public static readonly int _Chroma_Params = Shader.PropertyToID("_Chroma_Params");

			// Token: 0x0400066E RID: 1646
			public static readonly int _Vignette_Params1 = Shader.PropertyToID("_Vignette_Params1");

			// Token: 0x0400066F RID: 1647
			public static readonly int _Vignette_Params2 = Shader.PropertyToID("_Vignette_Params2");

			// Token: 0x04000670 RID: 1648
			public static readonly int _Vignette_ParamsXR = Shader.PropertyToID("_Vignette_ParamsXR");

			// Token: 0x04000671 RID: 1649
			public static readonly int _Lut_Params = Shader.PropertyToID("_Lut_Params");

			// Token: 0x04000672 RID: 1650
			public static readonly int _UserLut_Params = Shader.PropertyToID("_UserLut_Params");

			// Token: 0x04000673 RID: 1651
			public static readonly int _InternalLut = Shader.PropertyToID("_InternalLut");

			// Token: 0x04000674 RID: 1652
			public static readonly int _UserLut = Shader.PropertyToID("_UserLut");

			// Token: 0x04000675 RID: 1653
			public static readonly int _DownSampleScaleFactor = Shader.PropertyToID("_DownSampleScaleFactor");

			// Token: 0x04000676 RID: 1654
			public static readonly int _FlareOcclusionRemapTex = Shader.PropertyToID("_FlareOcclusionRemapTex");

			// Token: 0x04000677 RID: 1655
			public static readonly int _FlareOcclusionTex = Shader.PropertyToID("_FlareOcclusionTex");

			// Token: 0x04000678 RID: 1656
			public static readonly int _FlareOcclusionIndex = Shader.PropertyToID("_FlareOcclusionIndex");

			// Token: 0x04000679 RID: 1657
			public static readonly int _FlareTex = Shader.PropertyToID("_FlareTex");

			// Token: 0x0400067A RID: 1658
			public static readonly int _FlareColorValue = Shader.PropertyToID("_FlareColorValue");

			// Token: 0x0400067B RID: 1659
			public static readonly int _FlareData0 = Shader.PropertyToID("_FlareData0");

			// Token: 0x0400067C RID: 1660
			public static readonly int _FlareData1 = Shader.PropertyToID("_FlareData1");

			// Token: 0x0400067D RID: 1661
			public static readonly int _FlareData2 = Shader.PropertyToID("_FlareData2");

			// Token: 0x0400067E RID: 1662
			public static readonly int _FlareData3 = Shader.PropertyToID("_FlareData3");

			// Token: 0x0400067F RID: 1663
			public static readonly int _FlareData4 = Shader.PropertyToID("_FlareData4");

			// Token: 0x04000680 RID: 1664
			public static readonly int _FlareData5 = Shader.PropertyToID("_FlareData5");

			// Token: 0x04000681 RID: 1665
			public static readonly int _FullscreenProjMat = Shader.PropertyToID("_FullscreenProjMat");

			// Token: 0x04000682 RID: 1666
			public static int[] _BloomMipUp;

			// Token: 0x04000683 RID: 1667
			public static int[] _BloomMipDown;
		}

		// Token: 0x02000124 RID: 292
		private class UpdateCameraResolutionPassData
		{
			// Token: 0x04000684 RID: 1668
			internal Vector2Int newCameraTargetSize;
		}

		// Token: 0x02000125 RID: 293
		private class StopNaNsPassData
		{
			// Token: 0x04000685 RID: 1669
			internal TextureHandle stopNaNTarget;

			// Token: 0x04000686 RID: 1670
			internal TextureHandle sourceTexture;

			// Token: 0x04000687 RID: 1671
			internal Material stopNaN;
		}

		// Token: 0x02000126 RID: 294
		private class SMAASetupPassData
		{
			// Token: 0x04000688 RID: 1672
			internal Vector4 metrics;

			// Token: 0x04000689 RID: 1673
			internal Texture2D areaTexture;

			// Token: 0x0400068A RID: 1674
			internal Texture2D searchTexture;

			// Token: 0x0400068B RID: 1675
			internal float stencilRef;

			// Token: 0x0400068C RID: 1676
			internal float stencilMask;

			// Token: 0x0400068D RID: 1677
			internal AntialiasingQuality antialiasingQuality;

			// Token: 0x0400068E RID: 1678
			internal Material material;
		}

		// Token: 0x02000127 RID: 295
		private class SMAAPassData
		{
			// Token: 0x0400068F RID: 1679
			internal TextureHandle destinationTexture;

			// Token: 0x04000690 RID: 1680
			internal TextureHandle sourceTexture;

			// Token: 0x04000691 RID: 1681
			internal TextureHandle depthStencilTexture;

			// Token: 0x04000692 RID: 1682
			internal TextureHandle blendTexture;

			// Token: 0x04000693 RID: 1683
			internal Material material;
		}

		// Token: 0x02000128 RID: 296
		private class UberSetupBloomPassData
		{
			// Token: 0x04000694 RID: 1684
			internal Vector4 bloomParams;

			// Token: 0x04000695 RID: 1685
			internal Vector4 dirtScaleOffset;

			// Token: 0x04000696 RID: 1686
			internal float dirtIntensity;

			// Token: 0x04000697 RID: 1687
			internal Texture dirtTexture;

			// Token: 0x04000698 RID: 1688
			internal bool highQualityFilteringValue;

			// Token: 0x04000699 RID: 1689
			internal TextureHandle bloomTexture;

			// Token: 0x0400069A RID: 1690
			internal Material uberMaterial;
		}

		// Token: 0x02000129 RID: 297
		private class BloomPassData
		{
			// Token: 0x0400069B RID: 1691
			internal int mipCount;

			// Token: 0x0400069C RID: 1692
			internal Material material;

			// Token: 0x0400069D RID: 1693
			internal Material[] upsampleMaterials;

			// Token: 0x0400069E RID: 1694
			internal TextureHandle sourceTexture;

			// Token: 0x0400069F RID: 1695
			internal TextureHandle[] bloomMipUp;

			// Token: 0x040006A0 RID: 1696
			internal TextureHandle[] bloomMipDown;
		}

		// Token: 0x0200012A RID: 298
		internal struct BloomMaterialParams
		{
			// Token: 0x060006C6 RID: 1734 RVA: 0x0001F65D File Offset: 0x0001D85D
			internal bool Equals(ref PostProcessPass.BloomMaterialParams other)
			{
				return this.parameters == other.parameters && this.highQualityFiltering == other.highQualityFiltering && this.enableAlphaOutput == other.enableAlphaOutput;
			}

			// Token: 0x040006A1 RID: 1697
			internal Vector4 parameters;

			// Token: 0x040006A2 RID: 1698
			internal bool highQualityFiltering;

			// Token: 0x040006A3 RID: 1699
			internal bool enableAlphaOutput;
		}

		// Token: 0x0200012B RID: 299
		private class DoFGaussianPassData
		{
			// Token: 0x040006A4 RID: 1700
			internal int downsample;

			// Token: 0x040006A5 RID: 1701
			internal RenderingData renderingData;

			// Token: 0x040006A6 RID: 1702
			internal Vector3 cocParams;

			// Token: 0x040006A7 RID: 1703
			internal bool highQualitySamplingValue;

			// Token: 0x040006A8 RID: 1704
			internal TextureHandle sourceTexture;

			// Token: 0x040006A9 RID: 1705
			internal TextureHandle depthTexture;

			// Token: 0x040006AA RID: 1706
			internal Material material;

			// Token: 0x040006AB RID: 1707
			internal Material materialCoC;

			// Token: 0x040006AC RID: 1708
			internal TextureHandle halfCoCTexture;

			// Token: 0x040006AD RID: 1709
			internal TextureHandle fullCoCTexture;

			// Token: 0x040006AE RID: 1710
			internal TextureHandle pingTexture;

			// Token: 0x040006AF RID: 1711
			internal TextureHandle pongTexture;

			// Token: 0x040006B0 RID: 1712
			internal RenderTargetIdentifier[] multipleRenderTargets = new RenderTargetIdentifier[2];

			// Token: 0x040006B1 RID: 1713
			internal TextureHandle destination;
		}

		// Token: 0x0200012C RID: 300
		private class DoFBokehPassData
		{
			// Token: 0x040006B2 RID: 1714
			internal Vector4[] bokehKernel;

			// Token: 0x040006B3 RID: 1715
			internal int downSample;

			// Token: 0x040006B4 RID: 1716
			internal float uvMargin;

			// Token: 0x040006B5 RID: 1717
			internal Vector4 cocParams;

			// Token: 0x040006B6 RID: 1718
			internal bool useFastSRGBLinearConversion;

			// Token: 0x040006B7 RID: 1719
			internal TextureHandle sourceTexture;

			// Token: 0x040006B8 RID: 1720
			internal TextureHandle depthTexture;

			// Token: 0x040006B9 RID: 1721
			internal Material material;

			// Token: 0x040006BA RID: 1722
			internal Material materialCoC;

			// Token: 0x040006BB RID: 1723
			internal TextureHandle halfCoCTexture;

			// Token: 0x040006BC RID: 1724
			internal TextureHandle fullCoCTexture;

			// Token: 0x040006BD RID: 1725
			internal TextureHandle pingTexture;

			// Token: 0x040006BE RID: 1726
			internal TextureHandle pongTexture;

			// Token: 0x040006BF RID: 1727
			internal TextureHandle destination;
		}

		// Token: 0x0200012D RID: 301
		private class PaniniProjectionPassData
		{
			// Token: 0x040006C0 RID: 1728
			internal TextureHandle destinationTexture;

			// Token: 0x040006C1 RID: 1729
			internal TextureHandle sourceTexture;

			// Token: 0x040006C2 RID: 1730
			internal RenderTextureDescriptor sourceTextureDesc;

			// Token: 0x040006C3 RID: 1731
			internal Material material;

			// Token: 0x040006C4 RID: 1732
			internal Vector4 paniniParams;

			// Token: 0x040006C5 RID: 1733
			internal bool isPaniniGeneric;
		}

		// Token: 0x0200012E RID: 302
		private class MotionBlurPassData
		{
			// Token: 0x040006C6 RID: 1734
			internal TextureHandle destinationTexture;

			// Token: 0x040006C7 RID: 1735
			internal TextureHandle sourceTexture;

			// Token: 0x040006C8 RID: 1736
			internal TextureHandle motionVectors;

			// Token: 0x040006C9 RID: 1737
			internal Material material;

			// Token: 0x040006CA RID: 1738
			internal int passIndex;

			// Token: 0x040006CB RID: 1739
			internal Camera camera;

			// Token: 0x040006CC RID: 1740
			internal XRPass xr;

			// Token: 0x040006CD RID: 1741
			internal float intensity;

			// Token: 0x040006CE RID: 1742
			internal float clamp;

			// Token: 0x040006CF RID: 1743
			internal bool enableAlphaOutput;
		}

		// Token: 0x0200012F RID: 303
		private class LensFlarePassData
		{
			// Token: 0x040006D0 RID: 1744
			internal TextureHandle destinationTexture;

			// Token: 0x040006D1 RID: 1745
			internal RenderTextureDescriptor sourceDescriptor;

			// Token: 0x040006D2 RID: 1746
			internal UniversalCameraData cameraData;

			// Token: 0x040006D3 RID: 1747
			internal Material material;

			// Token: 0x040006D4 RID: 1748
			internal Rect viewport;

			// Token: 0x040006D5 RID: 1749
			internal float paniniDistance;

			// Token: 0x040006D6 RID: 1750
			internal float paniniCropToFit;

			// Token: 0x040006D7 RID: 1751
			internal float width;

			// Token: 0x040006D8 RID: 1752
			internal float height;

			// Token: 0x040006D9 RID: 1753
			internal bool usePanini;
		}

		// Token: 0x02000130 RID: 304
		private class LensFlareScreenSpacePassData
		{
			// Token: 0x040006DA RID: 1754
			internal TextureHandle destinationTexture;

			// Token: 0x040006DB RID: 1755
			internal TextureHandle streakTmpTexture;

			// Token: 0x040006DC RID: 1756
			internal TextureHandle streakTmpTexture2;

			// Token: 0x040006DD RID: 1757
			internal TextureHandle originalBloomTexture;

			// Token: 0x040006DE RID: 1758
			internal TextureHandle screenSpaceLensFlareBloomMipTexture;

			// Token: 0x040006DF RID: 1759
			internal TextureHandle result;

			// Token: 0x040006E0 RID: 1760
			internal RenderTextureDescriptor sourceDescriptor;

			// Token: 0x040006E1 RID: 1761
			internal Camera camera;

			// Token: 0x040006E2 RID: 1762
			internal Material material;

			// Token: 0x040006E3 RID: 1763
			internal ScreenSpaceLensFlare lensFlareScreenSpace;

			// Token: 0x040006E4 RID: 1764
			internal int downsample;
		}

		// Token: 0x02000131 RID: 305
		private class PostProcessingFinalSetupPassData
		{
			// Token: 0x040006E5 RID: 1765
			internal TextureHandle destinationTexture;

			// Token: 0x040006E6 RID: 1766
			internal TextureHandle sourceTexture;

			// Token: 0x040006E7 RID: 1767
			internal Material material;

			// Token: 0x040006E8 RID: 1768
			internal UniversalCameraData cameraData;
		}

		// Token: 0x02000132 RID: 306
		private class PostProcessingFinalFSRScalePassData
		{
			// Token: 0x040006E9 RID: 1769
			internal TextureHandle destinationTexture;

			// Token: 0x040006EA RID: 1770
			internal TextureHandle sourceTexture;

			// Token: 0x040006EB RID: 1771
			internal Material material;

			// Token: 0x040006EC RID: 1772
			internal bool enableAlphaOutput;
		}

		// Token: 0x02000133 RID: 307
		private class PostProcessingFinalBlitPassData
		{
			// Token: 0x040006ED RID: 1773
			internal TextureHandle destinationTexture;

			// Token: 0x040006EE RID: 1774
			internal TextureHandle sourceTexture;

			// Token: 0x040006EF RID: 1775
			internal Material material;

			// Token: 0x040006F0 RID: 1776
			internal UniversalCameraData cameraData;

			// Token: 0x040006F1 RID: 1777
			internal PostProcessPass.FinalBlitSettings settings;
		}

		// Token: 0x02000134 RID: 308
		public struct FinalBlitSettings
		{
			// Token: 0x060006D0 RID: 1744 RVA: 0x0001F6A4 File Offset: 0x0001D8A4
			public static PostProcessPass.FinalBlitSettings Create()
			{
				return new PostProcessPass.FinalBlitSettings
				{
					isFxaaEnabled = false,
					isFsrEnabled = false,
					isTaaSharpeningEnabled = false,
					requireHDROutput = false,
					resolveToDebugScreen = false,
					isAlphaOutputEnabled = false,
					hdrOperations = HDROutputUtils.Operation.None
				};
			}

			// Token: 0x040006F2 RID: 1778
			public bool isFxaaEnabled;

			// Token: 0x040006F3 RID: 1779
			public bool isFsrEnabled;

			// Token: 0x040006F4 RID: 1780
			public bool isTaaSharpeningEnabled;

			// Token: 0x040006F5 RID: 1781
			public bool requireHDROutput;

			// Token: 0x040006F6 RID: 1782
			public bool resolveToDebugScreen;

			// Token: 0x040006F7 RID: 1783
			public bool isAlphaOutputEnabled;

			// Token: 0x040006F8 RID: 1784
			public HDROutputUtils.Operation hdrOperations;
		}

		// Token: 0x02000135 RID: 309
		private class UberPostPassData
		{
			// Token: 0x040006F9 RID: 1785
			internal TextureHandle destinationTexture;

			// Token: 0x040006FA RID: 1786
			internal TextureHandle sourceTexture;

			// Token: 0x040006FB RID: 1787
			internal TextureHandle lutTexture;

			// Token: 0x040006FC RID: 1788
			internal Vector4 lutParams;

			// Token: 0x040006FD RID: 1789
			internal TextureHandle userLutTexture;

			// Token: 0x040006FE RID: 1790
			internal Vector4 userLutParams;

			// Token: 0x040006FF RID: 1791
			internal Material material;

			// Token: 0x04000700 RID: 1792
			internal UniversalCameraData cameraData;

			// Token: 0x04000701 RID: 1793
			internal TonemappingMode toneMappingMode;

			// Token: 0x04000702 RID: 1794
			internal bool isHdrGrading;

			// Token: 0x04000703 RID: 1795
			internal bool isBackbuffer;

			// Token: 0x04000704 RID: 1796
			internal bool enableAlphaOutput;

			// Token: 0x04000705 RID: 1797
			internal bool hasFinalPass;
		}

		// Token: 0x02000136 RID: 310
		private class PostFXSetupPassData
		{
		}
	}
}
