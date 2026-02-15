using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000216 RID: 534
	internal class DrawObjectsWithRenderingLayersPass : DrawObjectsPass
	{
		// Token: 0x06000BFE RID: 3070 RVA: 0x0004166F File Offset: 0x0003F86F
		public DrawObjectsWithRenderingLayersPass(URPProfileId profilerTag, bool opaque, RenderPassEvent evt, RenderQueueRange renderQueueRange, LayerMask layerMask, StencilState stencilState, int stencilReference)
			: base(profilerTag, opaque, evt, renderQueueRange, layerMask, stencilState, stencilReference)
		{
			this.m_ColorTargetIndentifiers = new RTHandle[2];
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00041690 File Offset: 0x0003F890
		public void Setup(RTHandle colorAttachment, RTHandle renderingLayersTexture, RTHandle depthAttachment)
		{
			if (colorAttachment == null)
			{
				throw new ArgumentException("Color attachment can not be null", "colorAttachment");
			}
			if (renderingLayersTexture == null)
			{
				throw new ArgumentException("Rendering layers attachment can not be null", "renderingLayersTexture");
			}
			if (depthAttachment == null)
			{
				throw new ArgumentException("Depth attachment can not be null", "depthAttachment");
			}
			this.m_ColorTargetIndentifiers[0] = colorAttachment;
			this.m_ColorTargetIndentifiers[1] = renderingLayersTexture;
			this.m_DepthTargetIndentifiers = depthAttachment;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x000416EF File Offset: 0x0003F8EF
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			base.ConfigureTarget(this.m_ColorTargetIndentifiers, this.m_DepthTargetIndentifiers);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00041703 File Offset: 0x0003F903
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer commandBuffer = *renderingData.commandBuffer;
			commandBuffer.SetKeyword(in ShaderGlobalKeywords.WriteRenderingLayers, true);
			base.Execute(context, ref renderingData);
			commandBuffer.SetKeyword(in ShaderGlobalKeywords.WriteRenderingLayers, false);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0004172C File Offset: 0x0003F92C
		internal void Render(RenderGraph renderGraph, ContextContainer frameData, TextureHandle colorTarget, TextureHandle renderingLayersTexture, TextureHandle depthTarget, TextureHandle mainShadowsTexture, TextureHandle additionalShadowsTexture, RenderingLayerUtils.MaskSize maskSize, uint batchLayerMask = 4294967295U)
		{
			DrawObjectsWithRenderingLayersPass.RenderingLayersPassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<DrawObjectsWithRenderingLayersPass.RenderingLayersPassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/DrawObjectsPass.cs", 412))
			{
				UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
				UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
				UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
				UniversalLightData lightData = frameData.Get<UniversalLightData>();
				base.InitPassData(cameraData, ref passData.basePassData, batchLayerMask, false);
				passData.maskSize = maskSize;
				passData.basePassData.albedoHdl = colorTarget;
				builder.SetRenderAttachment(colorTarget, 0, AccessFlags.Write);
				builder.SetRenderAttachment(renderingLayersTexture, 1, AccessFlags.Write);
				passData.basePassData.depthHdl = depthTarget;
				builder.SetRenderAttachmentDepth(depthTarget, AccessFlags.Write);
				if (mainShadowsTexture.IsValid())
				{
					builder.UseTexture(in mainShadowsTexture, AccessFlags.Read);
				}
				if (additionalShadowsTexture.IsValid())
				{
					builder.UseTexture(in additionalShadowsTexture, AccessFlags.Read);
				}
				if (cameraData.renderer is UniversalRenderer)
				{
					TextureHandle ssaoTexture = resourceData.ssaoTexture;
					if (ssaoTexture.IsValid())
					{
						builder.UseTexture(in ssaoTexture, AccessFlags.Read);
					}
					RenderGraphUtils.UseDBufferIfValid(builder, resourceData);
				}
				base.InitRendererLists(renderingData, cameraData, lightData, ref passData.basePassData, default(ScriptableRenderContext), renderGraph, true);
				if (ScriptableRenderPass.GetActiveDebugHandler(cameraData) != null)
				{
					passData.basePassData.debugRendererLists.PrepareRendererListForRasterPass(builder);
				}
				else
				{
					builder.UseRendererList(in passData.basePassData.rendererListHdl);
					builder.UseRendererList(in passData.basePassData.objectsWithErrorRendererListHdl);
				}
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				if (cameraData.xr.enabled)
				{
					bool passSupportsFoveation = cameraData.xrUniversal.canFoveateIntermediatePasses || resourceData.isActiveTargetBackBuffer;
					builder.EnableFoveatedRasterization(cameraData.xr.supportsFoveatedRendering && passSupportsFoveation);
				}
				builder.SetRenderFunc<DrawObjectsWithRenderingLayersPass.RenderingLayersPassData>(delegate(DrawObjectsWithRenderingLayersPass.RenderingLayersPassData data, RasterGraphContext context)
				{
					context.cmd.SetKeyword(in ShaderGlobalKeywords.WriteRenderingLayers, true);
					RenderingLayerUtils.SetupProperties(context.cmd, data.maskSize);
					if (!data.basePassData.isOpaque && !data.basePassData.shouldTransparentsReceiveShadows)
					{
						TransparentSettingsPass.ExecutePass(context.cmd, data.basePassData.shouldTransparentsReceiveShadows);
					}
					bool yFlip = data.basePassData.cameraData.IsRenderTargetProjectionMatrixFlipped(data.basePassData.albedoHdl, data.basePassData.depthHdl);
					DrawObjectsPass.ExecutePass(context.cmd, data.basePassData, data.basePassData.rendererListHdl, data.basePassData.objectsWithErrorRendererListHdl, yFlip);
					context.cmd.SetKeyword(in ShaderGlobalKeywords.WriteRenderingLayers, false);
				});
			}
		}

		// Token: 0x04000D79 RID: 3449
		private RTHandle[] m_ColorTargetIndentifiers;

		// Token: 0x04000D7A RID: 3450
		private RTHandle m_DepthTargetIndentifiers;

		// Token: 0x02000217 RID: 535
		private class RenderingLayersPassData
		{
			// Token: 0x06000C03 RID: 3075 RVA: 0x0004190C File Offset: 0x0003FB0C
			public RenderingLayersPassData()
			{
				this.basePassData = new DrawObjectsPass.PassData();
			}

			// Token: 0x04000D7B RID: 3451
			internal DrawObjectsPass.PassData basePassData;

			// Token: 0x04000D7C RID: 3452
			internal RenderingLayerUtils.MaskSize maskSize;
		}
	}
}
