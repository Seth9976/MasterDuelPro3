using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200010D RID: 269
	internal class DrawScreenSpaceUIPass : ScriptableRenderPass
	{
		// Token: 0x0600062C RID: 1580 RVA: 0x00017895 File Offset: 0x00015A95
		public DrawScreenSpaceUIPass(RenderPassEvent evt, bool renderOffscreen)
		{
			base.profilingSampler = ProfilingSampler.Get<URPProfileId>(URPProfileId.DrawScreenSpaceUI);
			base.renderPassEvent = evt;
			base.useNativeRenderPass = false;
			this.m_RenderOffscreen = renderOffscreen;
			this.m_PassData = new DrawScreenSpaceUIPass.PassData();
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000178CA File Offset: 0x00015ACA
		public static void ConfigureColorDescriptor(ref RenderTextureDescriptor descriptor, int cameraWidth, int cameraHeight)
		{
			descriptor.graphicsFormat = GraphicsFormat.R8G8B8A8_SRGB;
			descriptor.depthStencilFormat = GraphicsFormat.None;
			descriptor.width = cameraWidth;
			descriptor.height = cameraHeight;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000178E8 File Offset: 0x00015AE8
		public static void ConfigureDepthDescriptor(ref RenderTextureDescriptor descriptor, GraphicsFormat depthStencilFormat, int cameraWidth, int cameraHeight)
		{
			descriptor.graphicsFormat = GraphicsFormat.None;
			descriptor.depthStencilFormat = depthStencilFormat;
			descriptor.width = cameraWidth;
			descriptor.height = cameraHeight;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000098C8 File Offset: 0x00007AC8
		private static void ExecutePass(RasterCommandBuffer commandBuffer, DrawScreenSpaceUIPass.PassData passData, RendererList rendererList)
		{
			commandBuffer.DrawRendererList(rendererList);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00017906 File Offset: 0x00015B06
		private static void ExecutePass(UnsafeCommandBuffer commandBuffer, DrawScreenSpaceUIPass.UnsafePassData passData, RendererList rendererList)
		{
			commandBuffer.DrawRendererList(rendererList);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001790F File Offset: 0x00015B0F
		public void Dispose()
		{
			RTHandle colorTarget = this.m_ColorTarget;
			if (colorTarget != null)
			{
				colorTarget.Release();
			}
			RTHandle depthTarget = this.m_DepthTarget;
			if (depthTarget == null)
			{
				return;
			}
			depthTarget.Release();
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00017934 File Offset: 0x00015B34
		public void Setup(UniversalCameraData cameraData, GraphicsFormat depthStencilFormat)
		{
			if (this.m_RenderOffscreen)
			{
				RenderTextureDescriptor colorDescriptor = cameraData.cameraTargetDescriptor;
				DrawScreenSpaceUIPass.ConfigureColorDescriptor(ref colorDescriptor, cameraData.pixelWidth, cameraData.pixelHeight);
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_ColorTarget, in colorDescriptor, FilterMode.Point, TextureWrapMode.Repeat, 1, 0f, "_OverlayUITexture");
				RenderTextureDescriptor depthDescriptor = cameraData.cameraTargetDescriptor;
				DrawScreenSpaceUIPass.ConfigureDepthDescriptor(ref depthDescriptor, depthStencilFormat, cameraData.pixelWidth, cameraData.pixelHeight);
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_DepthTarget, in depthDescriptor, FilterMode.Point, TextureWrapMode.Repeat, 1, 0f, "_OverlayUITexture_Depth");
			}
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000179B4 File Offset: 0x00015BB4
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			if (this.m_RenderOffscreen)
			{
				base.ConfigureTarget(this.m_ColorTarget, this.m_DepthTarget);
				base.ConfigureClear(ClearFlag.Color, Color.clear);
				if (cmd != null)
				{
					cmd.SetGlobalTexture(ShaderPropertyId.overlayUITexture, this.m_ColorTarget);
					return;
				}
			}
			else
			{
				UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
				DebugHandler debugHandler = ScriptableRenderPass.GetActiveDebugHandler(cameraData);
				if (debugHandler != null && debugHandler.WriteToDebugScreenTexture(cameraData.resolveFinalTarget))
				{
					base.ConfigureTarget(*debugHandler.DebugScreenColorHandle, *debugHandler.DebugScreenDepthHandle);
					return;
				}
				RTHandleStaticHelpers.SetRTHandleStaticWrapper(RenderingUtils.GetCameraTargetIdentifier(ref renderingData));
				RTHandle colorTargetHandle = RTHandleStaticHelpers.s_RTHandleWrapper;
				base.ConfigureTarget(colorTargetHandle);
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00017A58 File Offset: 0x00015C58
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			using (new ProfilingScope(*renderingData.commandBuffer, base.profilingSampler))
			{
				RendererList rendererList = context.CreateUIOverlayRendererList(*renderingData.cameraData.camera);
				DrawScreenSpaceUIPass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer), this.m_PassData, rendererList);
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00017AC8 File Offset: 0x00015CC8
		internal void RenderOffscreen(RenderGraph renderGraph, ContextContainer frameData, GraphicsFormat depthStencilFormat, out TextureHandle output)
		{
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			RenderTextureDescriptor colorDescriptor = cameraData.cameraTargetDescriptor;
			DrawScreenSpaceUIPass.ConfigureColorDescriptor(ref colorDescriptor, cameraData.pixelWidth, cameraData.pixelHeight);
			output = UniversalRenderer.CreateRenderGraphTexture(renderGraph, colorDescriptor, "_OverlayUITexture", true, FilterMode.Point, TextureWrapMode.Clamp);
			RenderTextureDescriptor depthDescriptor = cameraData.cameraTargetDescriptor;
			DrawScreenSpaceUIPass.ConfigureDepthDescriptor(ref depthDescriptor, depthStencilFormat, cameraData.pixelWidth, cameraData.pixelHeight);
			TextureHandle depthBuffer = UniversalRenderer.CreateRenderGraphTexture(renderGraph, depthDescriptor, "_OverlayUITexture_Depth", false, FilterMode.Point, TextureWrapMode.Clamp);
			DrawScreenSpaceUIPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<DrawScreenSpaceUIPass.PassData>("Draw Screen Space UIToolkit/uGUI - Offscreen", out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/DrawScreenSpaceUIPass.cs", 181))
			{
				builder.SetRenderAttachment(output, 0, AccessFlags.Write);
				DrawScreenSpaceUIPass.PassData passData3 = passData;
				UniversalCameraData universalCameraData = cameraData;
				UISubset uisubset = UISubset.UIToolkit_UGUI;
				passData3.rendererList = renderGraph.CreateUIOverlayRendererList(in universalCameraData.camera, in uisubset);
				builder.UseRendererList(in passData.rendererList);
				builder.SetRenderAttachmentDepth(depthBuffer, AccessFlags.ReadWrite);
				if (output.IsValid())
				{
					builder.SetGlobalTextureAfterPass(in output, ShaderPropertyId.overlayUITexture);
				}
				builder.SetRenderFunc<DrawScreenSpaceUIPass.PassData>(delegate(DrawScreenSpaceUIPass.PassData data, RasterGraphContext context)
				{
					DrawScreenSpaceUIPass.ExecutePass(context.cmd, data, data.rendererList);
				});
			}
			DrawScreenSpaceUIPass.UnsafePassData passData2;
			using (IUnsafeRenderGraphBuilder builder2 = renderGraph.AddUnsafePass<DrawScreenSpaceUIPass.UnsafePassData>("Draw Screen Space IMGUI/SoftwareCursor - Offscreen", out passData2, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/DrawScreenSpaceUIPass.cs", 202))
			{
				passData2.colorTarget = output;
				builder2.UseTexture(in output, AccessFlags.Write);
				DrawScreenSpaceUIPass.UnsafePassData unsafePassData = passData2;
				UniversalCameraData universalCameraData2 = cameraData;
				UISubset uisubset = UISubset.LowLevel;
				unsafePassData.rendererList = renderGraph.CreateUIOverlayRendererList(in universalCameraData2.camera, in uisubset);
				builder2.UseRendererList(in passData2.rendererList);
				builder2.SetRenderFunc<DrawScreenSpaceUIPass.UnsafePassData>(delegate(DrawScreenSpaceUIPass.UnsafePassData data, UnsafeGraphContext context)
				{
					context.cmd.SetRenderTarget(data.colorTarget);
					DrawScreenSpaceUIPass.ExecutePass(context.cmd, data, data.rendererList);
				});
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00017C90 File Offset: 0x00015E90
		internal void RenderOverlay(RenderGraph renderGraph, ContextContainer frameData, in TextureHandle colorBuffer, in TextureHandle depthBuffer)
		{
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			frameData.Get<UniversalResourceData>();
			UniversalRenderer renderer = cameraData.renderer as UniversalRenderer;
			DrawScreenSpaceUIPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<DrawScreenSpaceUIPass.PassData>("Draw UIToolkit/uGUI Overlay", out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/DrawScreenSpaceUIPass.cs", 225))
			{
				if (cameraData.requiresOpaqueTexture && renderer != null)
				{
					builder.UseGlobalTexture(DrawScreenSpaceUIPass.s_CameraOpaqueTextureID, AccessFlags.Read);
				}
				builder.SetRenderAttachment(colorBuffer, 0, AccessFlags.Write);
				builder.SetRenderAttachmentDepth(depthBuffer, AccessFlags.ReadWrite);
				DrawScreenSpaceUIPass.PassData passData3 = passData;
				UniversalCameraData universalCameraData = cameraData;
				UISubset uisubset = UISubset.UIToolkit_UGUI;
				passData3.rendererList = renderGraph.CreateUIOverlayRendererList(in universalCameraData.camera, in uisubset);
				builder.UseRendererList(in passData.rendererList);
				builder.SetRenderFunc<DrawScreenSpaceUIPass.PassData>(delegate(DrawScreenSpaceUIPass.PassData data, RasterGraphContext context)
				{
					DrawScreenSpaceUIPass.ExecutePass(context.cmd, data, data.rendererList);
				});
			}
			DrawScreenSpaceUIPass.UnsafePassData passData2;
			using (IUnsafeRenderGraphBuilder builder2 = renderGraph.AddUnsafePass<DrawScreenSpaceUIPass.UnsafePassData>("Draw IMGUI/SoftwareCursor Overlay", out passData2, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/DrawScreenSpaceUIPass.cs", 245))
			{
				passData2.colorTarget = colorBuffer;
				builder2.UseTexture(in colorBuffer, AccessFlags.Write);
				DrawScreenSpaceUIPass.UnsafePassData unsafePassData = passData2;
				UniversalCameraData universalCameraData2 = cameraData;
				UISubset uisubset = UISubset.LowLevel;
				unsafePassData.rendererList = renderGraph.CreateUIOverlayRendererList(in universalCameraData2.camera, in uisubset);
				builder2.UseRendererList(in passData2.rendererList);
				builder2.SetRenderFunc<DrawScreenSpaceUIPass.UnsafePassData>(delegate(DrawScreenSpaceUIPass.UnsafePassData data, UnsafeGraphContext context)
				{
					context.cmd.SetRenderTarget(data.colorTarget);
					DrawScreenSpaceUIPass.ExecutePass(context.cmd, data, data.rendererList);
				});
			}
		}

		// Token: 0x040005A3 RID: 1443
		private DrawScreenSpaceUIPass.PassData m_PassData;

		// Token: 0x040005A4 RID: 1444
		private RTHandle m_ColorTarget;

		// Token: 0x040005A5 RID: 1445
		private RTHandle m_DepthTarget;

		// Token: 0x040005A6 RID: 1446
		private bool m_RenderOffscreen;

		// Token: 0x040005A7 RID: 1447
		private static readonly int s_CameraDepthTextureID = Shader.PropertyToID("_CameraDepthTexture");

		// Token: 0x040005A8 RID: 1448
		private static readonly int s_CameraOpaqueTextureID = Shader.PropertyToID("_CameraOpaqueTexture");

		// Token: 0x0200010E RID: 270
		private class PassData
		{
			// Token: 0x040005A9 RID: 1449
			internal RendererListHandle rendererList;
		}

		// Token: 0x0200010F RID: 271
		private class UnsafePassData
		{
			// Token: 0x040005AA RID: 1450
			internal RendererListHandle rendererList;

			// Token: 0x040005AB RID: 1451
			internal TextureHandle colorTarget;
		}
	}
}
