using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000071 RID: 113
	internal class DBufferCopyDepthPass : CopyDepthPass
	{
		// Token: 0x06000292 RID: 658 RVA: 0x0000896D File Offset: 0x00006B6D
		public DBufferCopyDepthPass(RenderPassEvent evt, Shader copyDepthShader, bool shouldClear = false, bool copyToDepth = false, bool copyResolvedDepth = false)
			: base(evt, copyDepthShader, shouldClear, copyToDepth, copyResolvedDepth, null)
		{
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00008980 File Offset: 0x00006B80
		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
			UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalRenderer universalRenderer = cameraData.renderer as UniversalRenderer;
			bool isDeferred = universalRenderer.renderingModeActual == RenderingMode.Deferred;
			bool useDepthPriming = universalRenderer.useDepthPriming;
			bool isMsaa = cameraData.cameraTargetDescriptor.msaaSamples > 1;
			if (!isDeferred && (!useDepthPriming || isMsaa))
			{
				RenderTextureDescriptor depthDesc = cameraData.cameraTargetDescriptor;
				depthDesc.graphicsFormat = GraphicsFormat.None;
				depthDesc.depthStencilFormat = cameraData.cameraTargetDescriptor.depthStencilFormat;
				depthDesc.msaaSamples = 1;
				resourceData.dBufferDepth = UniversalRenderer.CreateRenderGraphTexture(renderGraph, depthDesc, DBufferRenderPass.s_DBufferDepthName, true, FilterMode.Point, TextureWrapMode.Clamp);
				base.Render(renderGraph, resourceData.dBufferDepth, resourceData.cameraDepthTexture, resourceData, cameraData, false, "Copy DBuffer Depth");
			}
		}
	}
}
