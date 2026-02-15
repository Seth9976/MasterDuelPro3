using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x0200020A RID: 522
	internal class DeferredPass : ScriptableRenderPass
	{
		// Token: 0x06000BBC RID: 3004 RVA: 0x00040404 File Offset: 0x0003E604
		public DeferredPass(RenderPassEvent evt, DeferredLights deferredLights)
		{
			base.profilingSampler = new ProfilingSampler("Render Deferred Lighting");
			base.renderPassEvent = evt;
			this.m_DeferredLights = deferredLights;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0004042C File Offset: 0x0003E62C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescripor)
		{
			RTHandle lightingAttachment = this.m_DeferredLights.GbufferAttachments[this.m_DeferredLights.GBufferLightingIndex];
			RTHandle depthAttachment = this.m_DeferredLights.DepthAttachmentHandle;
			if (this.m_DeferredLights.UseFramebufferFetch)
			{
				base.ConfigureInputAttachments(this.m_DeferredLights.DeferredInputAttachments, this.m_DeferredLights.DeferredInputIsTransient);
			}
			base.ConfigureTarget(lightingAttachment, depthAttachment);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00040490 File Offset: 0x0003E690
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			ContextContainer frameData = renderingData.frameData;
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			UniversalShadowData shadowData = frameData.Get<UniversalShadowData>();
			this.m_DeferredLights.ExecuteDeferredPass(CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer), cameraData, lightData, shadowData);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000404D4 File Offset: 0x0003E6D4
		internal void Render(RenderGraph renderGraph, ContextContainer frameData, TextureHandle color, TextureHandle depth, TextureHandle[] gbuffer)
		{
			frameData.Get<UniversalResourceData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			UniversalShadowData shadowData = frameData.Get<UniversalShadowData>();
			DeferredPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<DeferredPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/DeferredPass.cs", 82))
			{
				passData.cameraData = cameraData;
				passData.lightData = lightData;
				passData.shadowData = shadowData;
				passData.color = color;
				builder.SetRenderAttachment(color, 0, AccessFlags.Write);
				passData.depth = depth;
				builder.SetRenderAttachmentDepth(depth, AccessFlags.Write);
				passData.deferredLights = this.m_DeferredLights;
				if (!this.m_DeferredLights.UseFramebufferFetch)
				{
					for (int i = 0; i < gbuffer.Length; i++)
					{
						if (i != this.m_DeferredLights.GBufferLightingIndex)
						{
							builder.UseTexture(in gbuffer[i], AccessFlags.Read);
						}
					}
				}
				else
				{
					int idx = 0;
					for (int j = 0; j < gbuffer.Length; j++)
					{
						if (j != this.m_DeferredLights.GBufferLightingIndex)
						{
							builder.SetInputAttachment(gbuffer[j], idx, AccessFlags.Read);
							idx++;
						}
					}
				}
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				builder.SetRenderFunc<DeferredPass.PassData>(delegate(DeferredPass.PassData data, RasterGraphContext context)
				{
					data.deferredLights.ExecuteDeferredPass(context.cmd, data.cameraData, data.lightData, data.shadowData);
				});
			}
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00040630 File Offset: 0x0003E830
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			this.m_DeferredLights.OnCameraCleanup(cmd);
		}

		// Token: 0x04000D3A RID: 3386
		private DeferredLights m_DeferredLights;

		// Token: 0x0200020B RID: 523
		private class PassData
		{
			// Token: 0x04000D3B RID: 3387
			internal UniversalCameraData cameraData;

			// Token: 0x04000D3C RID: 3388
			internal UniversalLightData lightData;

			// Token: 0x04000D3D RID: 3389
			internal UniversalShadowData shadowData;

			// Token: 0x04000D3E RID: 3390
			internal TextureHandle color;

			// Token: 0x04000D3F RID: 3391
			internal TextureHandle depth;

			// Token: 0x04000D40 RID: 3392
			internal TextureHandle[] gbuffer;

			// Token: 0x04000D41 RID: 3393
			internal DeferredLights deferredLights;
		}
	}
}
