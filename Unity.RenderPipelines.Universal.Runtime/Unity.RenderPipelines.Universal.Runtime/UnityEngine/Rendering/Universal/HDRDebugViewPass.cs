using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000114 RID: 276
	internal class HDRDebugViewPass : ScriptableRenderPass
	{
		// Token: 0x0600064B RID: 1611 RVA: 0x00018198 File Offset: 0x00016398
		public HDRDebugViewPass(Material mat)
		{
			base.profilingSampler = new ProfilingSampler("Blit HDR Debug Data");
			base.renderPassEvent = (RenderPassEvent)1003;
			this.m_PassDataCIExy = new HDRDebugViewPass.PassDataCIExy
			{
				material = mat
			};
			this.m_PassDataDebugView = new HDRDebugViewPass.PassDataDebugView
			{
				material = mat
			};
			this.m_material = mat;
			base.useNativeRenderPass = false;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000181F8 File Offset: 0x000163F8
		public static void ConfigureDescriptorForCIEPrepass(ref RenderTextureDescriptor descriptor)
		{
			descriptor.graphicsFormat = GraphicsFormat.R32_SFloat;
			descriptor.width = (descriptor.height = HDRDebugViewPass.ShaderConstants._SizeOfHDRXYMapping);
			descriptor.useMipMap = false;
			descriptor.autoGenerateMips = false;
			descriptor.useDynamicScale = true;
			descriptor.depthStencilFormat = GraphicsFormat.None;
			descriptor.enableRandomWrite = true;
			descriptor.msaaSamples = 1;
			descriptor.dimension = TextureDimension.Tex2D;
			descriptor.vrUsage = VRTextureUsage.None;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001825C File Offset: 0x0001645C
		internal static Vector4 GetLuminanceParameters(UniversalCameraData cameraData)
		{
			Vector4 luminanceParams = Vector4.zero;
			if (cameraData.isHDROutputActive)
			{
				Tonemapping tonemapping = VolumeManager.instance.stack.GetComponent<Tonemapping>();
				UniversalRenderPipeline.GetHDROutputLuminanceParameters(cameraData.hdrDisplayInformation, cameraData.hdrDisplayColorGamut, tonemapping, out luminanceParams);
			}
			else
			{
				luminanceParams.z = 1f;
			}
			return luminanceParams;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000182AC File Offset: 0x000164AC
		private static void ExecuteCIExyPrepass(CommandBuffer cmd, HDRDebugViewPass.PassDataCIExy data, RTHandle sourceTexture, RTHandle xyTarget, RTHandle destTexture)
		{
			CoreUtils.SetRenderTarget(cmd, destTexture, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare, ClearFlag.None, Color.clear, 0, CubemapFace.Unknown, -1);
			Vector4 debugParameters = new Vector4((float)HDRDebugViewPass.ShaderConstants._SizeOfHDRXYMapping, (float)HDRDebugViewPass.ShaderConstants._SizeOfHDRXYMapping, 0f, 0f);
			cmd.SetRandomWriteTarget(HDRDebugViewPass.ShaderConstants._CIExyUAVIndex, xyTarget);
			data.material.SetVector(HDRDebugViewPass.ShaderConstants._HDRDebugParamsId, debugParameters);
			data.material.SetVector(ShaderPropertyId.hdrOutputLuminanceParams, data.luminanceParameters);
			Vector2 viewportScale = (sourceTexture.useScaling ? new Vector2(sourceTexture.rtHandleProperties.rtHandleScale.x, sourceTexture.rtHandleProperties.rtHandleScale.y) : Vector2.one);
			Blitter.BlitTexture(cmd, sourceTexture, viewportScale, data.material, 0);
			cmd.ClearRandomWriteTargets();
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00018370 File Offset: 0x00016570
		private static void ExecuteHDRDebugViewFinalPass(RasterCommandBuffer cmd, HDRDebugViewPass.PassDataDebugView data, RTHandle sourceTexture, RTHandle destination, RTHandle xyTarget)
		{
			if (data.cameraData.isHDROutputActive)
			{
				HDROutputUtils.ConfigureHDROutput(data.material, data.cameraData.hdrDisplayColorGamut, HDROutputUtils.Operation.ColorEncoding);
				CoreUtils.SetKeyword(data.material, "_HDR_OVERLAY", data.cameraData.rendersOverlayUI);
			}
			data.material.SetTexture(HDRDebugViewPass.ShaderConstants._xyTextureId, xyTarget);
			Vector4 debugParameters = new Vector4((float)HDRDebugViewPass.ShaderConstants._SizeOfHDRXYMapping, (float)HDRDebugViewPass.ShaderConstants._SizeOfHDRXYMapping, 0f, 0f);
			data.material.SetVector(HDRDebugViewPass.ShaderConstants._HDRDebugParamsId, debugParameters);
			data.material.SetVector(ShaderPropertyId.hdrOutputLuminanceParams, data.luminanceParameters);
			data.material.SetInteger(HDRDebugViewPass.ShaderConstants._DebugHDRModeId, (int)data.hdrDebugMode);
			Vector4 scaleBias = RenderingUtils.GetFinalBlitScaleBias(sourceTexture, destination, data.cameraData);
			RenderTargetIdentifier cameraTarget = BuiltinRenderTextureType.CameraTarget;
			if (data.cameraData.xr.enabled)
			{
				cameraTarget = data.cameraData.xr.renderTarget;
			}
			if (destination.nameID == cameraTarget || data.cameraData.targetTexture != null)
			{
				cmd.SetViewport(data.cameraData.pixelRect);
			}
			Blitter.BlitTexture(cmd, sourceTexture, scaleBias, data.material, 1);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000184A6 File Offset: 0x000166A6
		public void Dispose()
		{
			RTHandle ciexyTarget = this.m_CIExyTarget;
			if (ciexyTarget != null)
			{
				ciexyTarget.Release();
			}
			RTHandle passthroughRT = this.m_PassthroughRT;
			if (passthroughRT == null)
			{
				return;
			}
			passthroughRT.Release();
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x000184CC File Offset: 0x000166CC
		public void Setup(UniversalCameraData cameraData, HDRDebugMode hdrdebugMode)
		{
			this.m_PassDataDebugView.hdrDebugMode = hdrdebugMode;
			RenderTextureDescriptor descriptor = cameraData.cameraTargetDescriptor;
			DebugHandler.ConfigureColorDescriptorForDebugScreen(ref descriptor, cameraData.pixelWidth, cameraData.pixelHeight);
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_PassthroughRT, in descriptor, FilterMode.Point, TextureWrapMode.Repeat, 1, 0f, "_HDRDebugDummyRT");
			RenderTextureDescriptor descriptorCIE = cameraData.cameraTargetDescriptor;
			HDRDebugViewPass.ConfigureDescriptorForCIEPrepass(ref descriptorCIE);
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_CIExyTarget, in descriptorCIE, FilterMode.Point, TextureWrapMode.Repeat, 1, 0f, "_xyBuffer");
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00018544 File Offset: 0x00016744
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
			CommandBuffer cmd = *renderingData.commandBuffer;
			this.m_PassDataCIExy.luminanceParameters = (this.m_PassDataDebugView.luminanceParameters = HDRDebugViewPass.GetLuminanceParameters(cameraData));
			this.m_PassDataDebugView.cameraData = cameraData;
			RTHandle sourceTexture = renderingData.cameraData.renderer->cameraColorTargetHandle;
			RTHandleStaticHelpers.SetRTHandleStaticWrapper(RenderingUtils.GetCameraTargetIdentifier(ref renderingData));
			RTHandle cameraTargetHandle = RTHandleStaticHelpers.s_RTHandleWrapper;
			this.m_material.enabledKeywords = null;
			CoreUtils.SetRenderTarget(cmd, this.m_CIExyTarget, ClearFlag.Color, Color.clear, 0, CubemapFace.Unknown, -1);
			this.ExecutePass(cmd, this.m_PassDataCIExy, this.m_PassDataDebugView, sourceTexture, this.m_CIExyTarget, cameraTargetHandle);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000185F4 File Offset: 0x000167F4
		private void ExecutePass(CommandBuffer cmd, HDRDebugViewPass.PassDataCIExy dataCIExy, HDRDebugViewPass.PassDataDebugView dataDebugView, RTHandle sourceTexture, RTHandle xyTarget, RTHandle destTexture)
		{
			RasterCommandBuffer rasterCmd = CommandBufferHelpers.GetRasterCommandBuffer(cmd);
			if (dataDebugView.hdrDebugMode != HDRDebugMode.ValuesAbovePaperWhite)
			{
				using (new ProfilingScope(cmd, base.profilingSampler))
				{
					HDRDebugViewPass.ExecuteCIExyPrepass(cmd, dataCIExy, sourceTexture, xyTarget, this.m_PassthroughRT);
				}
			}
			CoreUtils.SetRenderTarget(cmd, destTexture, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, ClearFlag.None, Color.clear, 0, CubemapFace.Unknown, -1);
			using (new ProfilingScope(cmd, base.profilingSampler))
			{
				HDRDebugViewPass.ExecuteHDRDebugViewFinalPass(rasterCmd, dataDebugView, sourceTexture, destTexture, xyTarget);
			}
			dataDebugView.cameraData.renderer.ConfigureCameraTarget(destTexture, destTexture);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000186B4 File Offset: 0x000168B4
		internal void RenderHDRDebug(RenderGraph renderGraph, UniversalCameraData cameraData, TextureHandle srcColor, TextureHandle overlayUITexture, TextureHandle dstColor, HDRDebugMode hdrDebugMode)
		{
			bool requiresCIExyData = hdrDebugMode != HDRDebugMode.ValuesAbovePaperWhite;
			Vector4 luminanceParameters = HDRDebugViewPass.GetLuminanceParameters(cameraData);
			TextureHandle intermediateRT = srcColor;
			TextureHandle xyBuffer = TextureHandle.nullHandle;
			if (requiresCIExyData)
			{
				RenderTextureDescriptor descriptor = cameraData.cameraTargetDescriptor;
				DebugHandler.ConfigureColorDescriptorForDebugScreen(ref descriptor, cameraData.pixelWidth, cameraData.pixelHeight);
				intermediateRT = UniversalRenderer.CreateRenderGraphTexture(renderGraph, descriptor, "_HDRDebugDummyRT", false, FilterMode.Point, TextureWrapMode.Clamp);
				HDRDebugViewPass.ConfigureDescriptorForCIEPrepass(ref descriptor);
				xyBuffer = UniversalRenderer.CreateRenderGraphTexture(renderGraph, descriptor, "_xyBuffer", true, FilterMode.Point, TextureWrapMode.Clamp);
				HDRDebugViewPass.PassDataCIExy passData;
				using (IUnsafeRenderGraphBuilder builder = renderGraph.AddUnsafePass<HDRDebugViewPass.PassDataCIExy>("Blit HDR DebugView CIExy", out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/HDRDebugViewPass.cs", 234))
				{
					passData.material = this.m_material;
					passData.luminanceParameters = luminanceParameters;
					passData.srcColor = srcColor;
					builder.UseTexture(in srcColor, AccessFlags.Read);
					passData.xyBuffer = xyBuffer;
					builder.UseTexture(in xyBuffer, AccessFlags.Write);
					passData.passThrough = intermediateRT;
					builder.UseTexture(in intermediateRT, AccessFlags.Write);
					builder.SetRenderFunc<HDRDebugViewPass.PassDataCIExy>(delegate(HDRDebugViewPass.PassDataCIExy data, UnsafeGraphContext context)
					{
						HDRDebugViewPass.ExecuteCIExyPrepass(CommandBufferHelpers.GetNativeCommandBuffer(context.cmd), data, data.srcColor, data.xyBuffer, data.passThrough);
					});
				}
			}
			HDRDebugViewPass.PassDataDebugView passData2;
			using (IRasterRenderGraphBuilder builder2 = renderGraph.AddRasterRenderPass<HDRDebugViewPass.PassDataDebugView>("Blit HDR DebugView", out passData2, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/HDRDebugViewPass.cs", 252))
			{
				passData2.material = this.m_material;
				passData2.hdrDebugMode = hdrDebugMode;
				passData2.luminanceParameters = luminanceParameters;
				passData2.cameraData = cameraData;
				if (requiresCIExyData)
				{
					passData2.xyBuffer = xyBuffer;
					builder2.UseTexture(in xyBuffer, AccessFlags.Read);
				}
				passData2.srcColor = srcColor;
				builder2.UseTexture(in srcColor, AccessFlags.Read);
				passData2.dstColor = dstColor;
				builder2.SetRenderAttachment(dstColor, 0, AccessFlags.WriteAll);
				if (overlayUITexture.IsValid())
				{
					passData2.overlayUITexture = overlayUITexture;
					builder2.UseTexture(in overlayUITexture, AccessFlags.Read);
				}
				builder2.SetRenderFunc<HDRDebugViewPass.PassDataDebugView>(delegate(HDRDebugViewPass.PassDataDebugView data, RasterGraphContext context)
				{
					data.material.enabledKeywords = null;
					HDRDebugViewPass.ExecuteHDRDebugViewFinalPass(context.cmd, data, data.srcColor, data.dstColor, data.xyBuffer);
				});
			}
		}

		// Token: 0x040005B6 RID: 1462
		private HDRDebugViewPass.PassDataCIExy m_PassDataCIExy;

		// Token: 0x040005B7 RID: 1463
		private HDRDebugViewPass.PassDataDebugView m_PassDataDebugView;

		// Token: 0x040005B8 RID: 1464
		private RTHandle m_CIExyTarget;

		// Token: 0x040005B9 RID: 1465
		private RTHandle m_PassthroughRT;

		// Token: 0x040005BA RID: 1466
		private Material m_material;

		// Token: 0x02000115 RID: 277
		private enum HDRDebugPassId
		{
			// Token: 0x040005BC RID: 1468
			CIExyPrepass,
			// Token: 0x040005BD RID: 1469
			DebugViewPass
		}

		// Token: 0x02000116 RID: 278
		private class PassDataCIExy
		{
			// Token: 0x040005BE RID: 1470
			internal Material material;

			// Token: 0x040005BF RID: 1471
			internal Vector4 luminanceParameters;

			// Token: 0x040005C0 RID: 1472
			internal TextureHandle srcColor;

			// Token: 0x040005C1 RID: 1473
			internal TextureHandle xyBuffer;

			// Token: 0x040005C2 RID: 1474
			internal TextureHandle passThrough;
		}

		// Token: 0x02000117 RID: 279
		private class PassDataDebugView
		{
			// Token: 0x040005C3 RID: 1475
			internal Material material;

			// Token: 0x040005C4 RID: 1476
			internal HDRDebugMode hdrDebugMode;

			// Token: 0x040005C5 RID: 1477
			internal UniversalCameraData cameraData;

			// Token: 0x040005C6 RID: 1478
			internal Vector4 luminanceParameters;

			// Token: 0x040005C7 RID: 1479
			internal TextureHandle overlayUITexture;

			// Token: 0x040005C8 RID: 1480
			internal TextureHandle xyBuffer;

			// Token: 0x040005C9 RID: 1481
			internal TextureHandle srcColor;

			// Token: 0x040005CA RID: 1482
			internal TextureHandle dstColor;
		}

		// Token: 0x02000118 RID: 280
		internal class ShaderConstants
		{
			// Token: 0x040005CB RID: 1483
			public static readonly int _DebugHDRModeId = Shader.PropertyToID("_DebugHDRMode");

			// Token: 0x040005CC RID: 1484
			public static readonly int _HDRDebugParamsId = Shader.PropertyToID("_HDRDebugParams");

			// Token: 0x040005CD RID: 1485
			public static readonly int _xyTextureId = Shader.PropertyToID("_xyBuffer");

			// Token: 0x040005CE RID: 1486
			public static readonly int _SizeOfHDRXYMapping = 512;

			// Token: 0x040005CF RID: 1487
			public static readonly int _CIExyUAVIndex = 1;
		}
	}
}
