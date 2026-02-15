using System;
using System.Collections.Generic;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200007B RID: 123
	internal class DecalPreviewPass : ScriptableRenderPass
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000979C File Offset: 0x0000799C
		public DecalPreviewPass()
		{
			base.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
			base.ConfigureInput(ScriptableRenderPassInput.Depth);
			this.m_ProfilingSampler = new ProfilingSampler("Decal Preview Render");
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(RenderQueueRange.opaque), -1, uint.MaxValue, 0);
			this.m_ShaderTagIdList = new List<ShaderTagId>();
			this.m_ShaderTagIdList.Add(new ShaderTagId("DecalScreenSpaceMesh"));
			this.m_PassData = new DecalPreviewPass.PassData();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00009814 File Offset: 0x00007A14
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			UniversalRenderingData universalRenderingData = renderingData.frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = renderingData.frameData.Get<UniversalLightData>();
			SortingCriteria sortingCriteria = cameraData.defaultOpaqueSortFlags;
			DrawingSettings drawingSettings = RenderingUtils.CreateDrawingSettings(this.m_ShaderTagIdList, universalRenderingData, cameraData, lightData, sortingCriteria);
			RendererListParams param = new RendererListParams(universalRenderingData.cullResults, drawingSettings, this.m_FilteringSettings);
			RendererList rendererList = context.CreateRendererList(ref param);
			using (new ProfilingScope(universalRenderingData.commandBuffer, this.m_ProfilingSampler))
			{
				DecalPreviewPass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(universalRenderingData.commandBuffer), this.m_PassData, rendererList);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000098C8 File Offset: 0x00007AC8
		private static void ExecutePass(RasterCommandBuffer cmd, DecalPreviewPass.PassData passData, RendererList rendererList)
		{
			cmd.DrawRendererList(rendererList);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000098D4 File Offset: 0x00007AD4
		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
			DecalPreviewPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<DecalPreviewPass.PassData>("Decal Preview Pass", out passData, this.m_ProfilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Decal/DecalPreviewPass.cs", 62))
			{
				UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
				UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
				UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
				UniversalLightData lightData = frameData.Get<UniversalLightData>();
				UniversalRenderer universalRenderer = (UniversalRenderer)cameraData.renderer;
				builder.SetRenderAttachment(resourceData.activeColorTexture, 0, AccessFlags.Write);
				builder.SetRenderAttachmentDepth(resourceData.activeDepthTexture, AccessFlags.Read);
				SortingCriteria sortingCriteria = cameraData.defaultOpaqueSortFlags;
				DrawingSettings drawingSettings = RenderingUtils.CreateDrawingSettings(this.m_ShaderTagIdList, renderingData, cameraData, lightData, sortingCriteria);
				RendererListParams param = new RendererListParams(renderingData.cullResults, drawingSettings, this.m_FilteringSettings);
				passData.rendererList = renderGraph.CreateRendererList(in param);
				builder.UseRendererList(in passData.rendererList);
				builder.SetRenderFunc<DecalPreviewPass.PassData>(delegate(DecalPreviewPass.PassData data, RasterGraphContext rgContext)
				{
					DecalPreviewPass.ExecutePass(rgContext.cmd, data, data.rendererList);
				});
			}
		}

		// Token: 0x0400022F RID: 559
		private FilteringSettings m_FilteringSettings;

		// Token: 0x04000230 RID: 560
		private List<ShaderTagId> m_ShaderTagIdList;

		// Token: 0x04000231 RID: 561
		private ProfilingSampler m_ProfilingSampler;

		// Token: 0x04000232 RID: 562
		private DecalPreviewPass.PassData m_PassData;

		// Token: 0x0200007C RID: 124
		private class PassData
		{
			// Token: 0x04000233 RID: 563
			internal RendererListHandle rendererList;
		}
	}
}
