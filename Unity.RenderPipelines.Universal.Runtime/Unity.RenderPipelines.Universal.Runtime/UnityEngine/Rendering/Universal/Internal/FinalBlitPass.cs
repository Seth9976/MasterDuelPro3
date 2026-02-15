using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000219 RID: 537
	public class FinalBlitPass : ScriptableRenderPass
	{
		// Token: 0x06000C07 RID: 3079 RVA: 0x00041A00 File Offset: 0x0003FC00
		public FinalBlitPass(RenderPassEvent evt, Material blitMaterial, Material blitHDRMaterial)
		{
			base.profilingSampler = ProfilingSampler.Get<URPProfileId>(URPProfileId.BlitFinalToBackBuffer);
			base.useNativeRenderPass = false;
			this.m_PassData = new FinalBlitPass.PassData();
			base.renderPassEvent = evt;
			this.m_BlitMaterialData = new FinalBlitPass.BlitMaterialData[2];
			for (int i = 0; i < 2; i++)
			{
				this.m_BlitMaterialData[i].material = ((i == 0) ? blitMaterial : blitHDRMaterial);
				FinalBlitPass.BlitMaterialData[] blitMaterialData = this.m_BlitMaterialData;
				int num = i;
				Material material = this.m_BlitMaterialData[i].material;
				blitMaterialData[num].nearestSamplerPass = ((material != null) ? material.FindPass("NearestDebugDraw") : (-1));
				FinalBlitPass.BlitMaterialData[] blitMaterialData2 = this.m_BlitMaterialData;
				int num2 = i;
				Material material2 = this.m_BlitMaterialData[i].material;
				blitMaterialData2[num2].bilinearSamplerPass = ((material2 != null) ? material2.FindPass("BilinearDebugDraw") : (-1));
			}
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0000217F File Offset: 0x0000037F
		public void Dispose()
		{
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00041AD5 File Offset: 0x0003FCD5
		[Obsolete("Use RTHandles for colorHandle", true)]
		public void Setup(RenderTextureDescriptor baseDescriptor, RenderTargetHandle colorHandle)
		{
			throw new NotSupportedException("Setup with RenderTargetHandle has been deprecated. Use it with RTHandles instead.");
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00041AE1 File Offset: 0x0003FCE1
		public void Setup(RenderTextureDescriptor baseDescriptor, RTHandle colorHandle)
		{
			this.m_Source = colorHandle;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00041AEA File Offset: 0x0003FCEA
		private static void SetupHDROutput(ColorGamut hdrDisplayColorGamut, Material material, HDROutputUtils.Operation hdrOperation, Vector4 hdrOutputParameters, bool rendersOverlayUI)
		{
			material.SetVector(ShaderPropertyId.hdrOutputLuminanceParams, hdrOutputParameters);
			HDROutputUtils.ConfigureHDROutput(material, hdrDisplayColorGamut, hdrOperation);
			CoreUtils.SetKeyword(material, "_HDR_OVERLAY", rendersOverlayUI);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00041B10 File Offset: 0x0003FD10
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
			DebugHandler debugHandler = ScriptableRenderPass.GetActiveDebugHandler(cameraData);
			if (debugHandler != null && debugHandler.WriteToDebugScreenTexture(cameraData.resolveFinalTarget))
			{
				base.ConfigureTarget(*debugHandler.DebugScreenColorHandle, *debugHandler.DebugScreenDepthHandle);
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00041B58 File Offset: 0x0003FD58
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
			bool outputsToHDR = renderingData.cameraData.isHDROutputActive;
			bool outputsAlpha = false;
			this.InitPassData(cameraData, ref this.m_PassData, outputsToHDR ? FinalBlitPass.BlitType.HDR : FinalBlitPass.BlitType.Core, outputsAlpha);
			if (this.m_PassData.blitMaterialData.material == null)
			{
				Debug.LogErrorFormat("Missing {0}. {1} render pass will not execute. Check for missing reference in the renderer resources.", new object[]
				{
					this.m_PassData.blitMaterialData,
					base.GetType().Name
				});
				return;
			}
			RenderTargetIdentifier cameraTargetIdentifier = RenderingUtils.GetCameraTargetIdentifier(ref renderingData);
			DebugHandler debugHandler = ScriptableRenderPass.GetActiveDebugHandler(cameraData);
			bool resolveToDebugScreen = debugHandler != null && debugHandler.WriteToDebugScreenTexture(cameraData.resolveFinalTarget);
			RTHandleStaticHelpers.SetRTHandleStaticWrapper(cameraTargetIdentifier);
			RTHandle cameraTargetHandle = RTHandleStaticHelpers.s_RTHandleWrapper;
			CommandBuffer cmd = *renderingData.commandBuffer;
			if (this.m_Source == cameraData.renderer.GetCameraColorFrontBuffer(cmd))
			{
				this.m_Source = renderingData.cameraData.renderer->cameraColorTargetHandle;
			}
			using (new ProfilingScope(cmd, base.profilingSampler))
			{
				this.m_PassData.blitMaterialData.material.enabledKeywords = null;
				cmd.SetKeyword(in ShaderGlobalKeywords.LinearToSRGBConversion, cameraData.requireSrgbConversion);
				if (outputsToHDR)
				{
					Tonemapping tonemapping = VolumeManager.instance.stack.GetComponent<Tonemapping>();
					Vector4 hdrOutputLuminanceParams;
					UniversalRenderPipeline.GetHDROutputLuminanceParameters(cameraData.hdrDisplayInformation, cameraData.hdrDisplayColorGamut, tonemapping, out hdrOutputLuminanceParams);
					HDROutputUtils.Operation hdrOperation = HDROutputUtils.Operation.None;
					if (debugHandler == null || !debugHandler.HDRDebugViewIsActive(cameraData.resolveFinalTarget))
					{
						hdrOperation |= HDROutputUtils.Operation.ColorEncoding;
					}
					if (!cameraData.postProcessEnabled)
					{
						hdrOperation |= HDROutputUtils.Operation.ColorConversion;
					}
					FinalBlitPass.SetupHDROutput(cameraData.hdrDisplayColorGamut, this.m_PassData.blitMaterialData.material, hdrOperation, hdrOutputLuminanceParams, cameraData.rendersOverlayUI);
				}
				if (resolveToDebugScreen)
				{
					RenderTexture rt = this.m_Source.rt;
					int shaderPassIndex = ((rt != null && rt.filterMode == FilterMode.Bilinear) ? this.m_PassData.blitMaterialData.bilinearSamplerPass : this.m_PassData.blitMaterialData.nearestSamplerPass);
					Vector2 viewportScale = (this.m_Source.useScaling ? new Vector2(this.m_Source.rtHandleProperties.rtHandleScale.x, this.m_Source.rtHandleProperties.rtHandleScale.y) : Vector2.one);
					Blitter.BlitTexture(cmd, this.m_Source, viewportScale, this.m_PassData.blitMaterialData.material, shaderPassIndex);
					cameraData.renderer.ConfigureCameraTarget(*debugHandler.DebugScreenColorHandle, *debugHandler.DebugScreenDepthHandle);
				}
				else if (GL.wireframe && cameraData.isSceneViewCamera)
				{
					cmd.SetRenderTarget(BuiltinRenderTextureType.CameraTarget, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
					cmd.Blit(this.m_Source.nameID, cameraTargetHandle.nameID);
				}
				else
				{
					RenderBufferLoadAction loadAction = RenderBufferLoadAction.DontCare;
					if (!cameraData.isSceneViewCamera && !cameraData.isDefaultViewport)
					{
						loadAction = RenderBufferLoadAction.Load;
					}
					if (cameraData.xr.enabled)
					{
						loadAction = RenderBufferLoadAction.Load;
					}
					CoreUtils.SetRenderTarget(*renderingData.commandBuffer, cameraTargetHandle, loadAction, RenderBufferStoreAction.Store, ClearFlag.None, Color.clear, 0, CubemapFace.Unknown, -1);
					FinalBlitPass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer), this.m_PassData, this.m_Source, cameraTargetHandle, cameraData);
					cameraData.renderer.ConfigureCameraTarget(cameraTargetHandle, cameraTargetHandle);
				}
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00041E94 File Offset: 0x00040094
		private static void ExecutePass(RasterCommandBuffer cmd, FinalBlitPass.PassData data, RTHandle source, RTHandle destination, UniversalCameraData cameraData)
		{
			bool isRenderToBackBufferTarget = !cameraData.isSceneViewCamera;
			if (cameraData.xr.enabled)
			{
				isRenderToBackBufferTarget = new RenderTargetIdentifier(destination.nameID, 0, CubemapFace.Unknown, -1) == new RenderTargetIdentifier(cameraData.xr.renderTarget, 0, CubemapFace.Unknown, -1);
			}
			Vector4 scaleBias = RenderingUtils.GetFinalBlitScaleBias(source, destination, cameraData);
			if (isRenderToBackBufferTarget)
			{
				cmd.SetViewport(cameraData.pixelRect);
			}
			cmd.SetWireframe(false);
			CoreUtils.SetKeyword(data.blitMaterialData.material, "_ENABLE_ALPHA_OUTPUT", data.enableAlphaOutput);
			RenderTexture rt = source.rt;
			int shaderPassIndex = ((rt != null && rt.filterMode == FilterMode.Bilinear) ? data.blitMaterialData.bilinearSamplerPass : data.blitMaterialData.nearestSamplerPass);
			Blitter.BlitTexture(cmd, source, scaleBias, data.blitMaterialData.material, shaderPassIndex);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00041F62 File Offset: 0x00040162
		private void InitPassData(UniversalCameraData cameraData, ref FinalBlitPass.PassData passData, FinalBlitPass.BlitType blitType, bool enableAlphaOutput)
		{
			passData.cameraData = cameraData;
			passData.requireSrgbConversion = cameraData.requireSrgbConversion;
			passData.enableAlphaOutput = enableAlphaOutput;
			passData.blitMaterialData = this.m_BlitMaterialData[(int)blitType];
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00041F98 File Offset: 0x00040198
		internal void Render(RenderGraph renderGraph, ContextContainer frameData, UniversalCameraData cameraData, in TextureHandle src, in TextureHandle dest, TextureHandle overlayUITexture)
		{
			FinalBlitPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<FinalBlitPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/FinalBlitPass.cs", 270))
			{
				frameData.Get<UniversalResourceData>();
				bool isUniversalRenderer = cameraData.renderer is UniversalRenderer;
				if (cameraData.requiresDepthTexture && isUniversalRenderer)
				{
					builder.UseGlobalTexture(FinalBlitPass.s_CameraDepthTextureID, AccessFlags.Read);
				}
				bool outputsToHDR = cameraData.isHDROutputActive;
				bool outputsAlpha = cameraData.isAlphaOutputEnabled;
				this.InitPassData(cameraData, ref passData, outputsToHDR ? FinalBlitPass.BlitType.HDR : FinalBlitPass.BlitType.Core, outputsAlpha);
				passData.sourceID = ShaderPropertyId.sourceTex;
				passData.source = src;
				builder.UseTexture(in src, AccessFlags.Read);
				passData.destination = dest;
				builder.SetRenderAttachment(dest, 0, AccessFlags.Write);
				bool passSupportsFoveation = !XRSystem.foveatedRenderingCaps.HasFlag(FoveatedRenderingCaps.NonUniformRaster);
				builder.EnableFoveatedRasterization(cameraData.xr.supportsFoveatedRendering && passSupportsFoveation);
				if (outputsToHDR && overlayUITexture.IsValid())
				{
					Tonemapping tonemapping = VolumeManager.instance.stack.GetComponent<Tonemapping>();
					UniversalRenderPipeline.GetHDROutputLuminanceParameters(passData.cameraData.hdrDisplayInformation, passData.cameraData.hdrDisplayColorGamut, tonemapping, out passData.hdrOutputLuminanceParams);
					builder.UseTexture(in overlayUITexture, AccessFlags.Read);
				}
				else
				{
					passData.hdrOutputLuminanceParams = new Vector4(-1f, -1f, -1f, -1f);
				}
				builder.AllowGlobalStateModification(true);
				builder.SetRenderFunc<FinalBlitPass.PassData>(delegate(FinalBlitPass.PassData data, RasterGraphContext context)
				{
					data.blitMaterialData.material.enabledKeywords = null;
					context.cmd.SetKeyword(in ShaderGlobalKeywords.LinearToSRGBConversion, data.requireSrgbConversion);
					data.blitMaterialData.material.SetTexture(data.sourceID, data.source);
					DebugHandler debugHandler = ScriptableRenderPass.GetActiveDebugHandler(data.cameraData);
					bool flag = debugHandler != null && debugHandler.WriteToDebugScreenTexture(data.cameraData.resolveFinalTarget);
					if (data.hdrOutputLuminanceParams.w >= 0f)
					{
						HDROutputUtils.Operation hdrOperation = HDROutputUtils.Operation.None;
						if (debugHandler == null || !debugHandler.HDRDebugViewIsActive(data.cameraData.resolveFinalTarget))
						{
							hdrOperation |= HDROutputUtils.Operation.ColorEncoding;
						}
						if (!data.cameraData.postProcessEnabled)
						{
							hdrOperation |= HDROutputUtils.Operation.ColorConversion;
						}
						FinalBlitPass.SetupHDROutput(data.cameraData.hdrDisplayColorGamut, data.blitMaterialData.material, hdrOperation, data.hdrOutputLuminanceParams, data.cameraData.rendersOverlayUI);
					}
					if (flag)
					{
						RTHandle sourceTex = data.source;
						Vector2 viewportScale = (sourceTex.useScaling ? new Vector2(sourceTex.rtHandleProperties.rtHandleScale.x, sourceTex.rtHandleProperties.rtHandleScale.y) : Vector2.one);
						RenderTexture rt = sourceTex.rt;
						int shaderPassIndex = ((rt != null && rt.filterMode == FilterMode.Bilinear) ? data.blitMaterialData.bilinearSamplerPass : data.blitMaterialData.nearestSamplerPass);
						Blitter.BlitTexture(context.cmd, sourceTex, viewportScale, data.blitMaterialData.material, shaderPassIndex);
						return;
					}
					FinalBlitPass.ExecutePass(context.cmd, data, data.source, data.destination, data.cameraData);
				});
			}
		}

		// Token: 0x04000D7F RID: 3455
		private RTHandle m_Source;

		// Token: 0x04000D80 RID: 3456
		private FinalBlitPass.PassData m_PassData;

		// Token: 0x04000D81 RID: 3457
		private static readonly int s_CameraDepthTextureID = Shader.PropertyToID("_CameraDepthTexture");

		// Token: 0x04000D82 RID: 3458
		private FinalBlitPass.BlitMaterialData[] m_BlitMaterialData;

		// Token: 0x0200021A RID: 538
		private static class BlitPassNames
		{
			// Token: 0x04000D83 RID: 3459
			public const string NearestSampler = "NearestDebugDraw";

			// Token: 0x04000D84 RID: 3460
			public const string BilinearSampler = "BilinearDebugDraw";
		}

		// Token: 0x0200021B RID: 539
		private enum BlitType
		{
			// Token: 0x04000D86 RID: 3462
			Core,
			// Token: 0x04000D87 RID: 3463
			HDR,
			// Token: 0x04000D88 RID: 3464
			Count
		}

		// Token: 0x0200021C RID: 540
		private struct BlitMaterialData
		{
			// Token: 0x04000D89 RID: 3465
			public Material material;

			// Token: 0x04000D8A RID: 3466
			public int nearestSamplerPass;

			// Token: 0x04000D8B RID: 3467
			public int bilinearSamplerPass;
		}

		// Token: 0x0200021D RID: 541
		private class PassData
		{
			// Token: 0x04000D8C RID: 3468
			internal TextureHandle source;

			// Token: 0x04000D8D RID: 3469
			internal TextureHandle destination;

			// Token: 0x04000D8E RID: 3470
			internal int sourceID;

			// Token: 0x04000D8F RID: 3471
			internal Vector4 hdrOutputLuminanceParams;

			// Token: 0x04000D90 RID: 3472
			internal bool requireSrgbConversion;

			// Token: 0x04000D91 RID: 3473
			internal bool enableAlphaOutput;

			// Token: 0x04000D92 RID: 3474
			internal FinalBlitPass.BlitMaterialData blitMaterialData;

			// Token: 0x04000D93 RID: 3475
			internal UniversalCameraData cameraData;
		}
	}
}
