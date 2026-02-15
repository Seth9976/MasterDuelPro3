using System;
using System.Collections.Generic;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000077 RID: 119
	internal class DecalForwardEmissivePass : ScriptableRenderPass
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x00009484 File Offset: 0x00007684
		public DecalForwardEmissivePass(DecalDrawFowardEmissiveSystem drawSystem)
		{
			base.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
			base.ConfigureInput(ScriptableRenderPassInput.Depth);
			this.m_DrawSystem = drawSystem;
			base.profilingSampler = new ProfilingSampler("Draw Decal Forward Emissive");
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(RenderQueueRange.opaque), -1, uint.MaxValue, 0);
			this.m_ShaderTagIdList = new List<ShaderTagId>();
			this.m_ShaderTagIdList.Add(new ShaderTagId("DecalMeshForwardEmissive"));
			this.m_ShaderTagIdList.Add(new ShaderTagId("DecalProjectorForwardEmissive"));
			this.m_PassData = new DecalForwardEmissivePass.PassData();
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00009518 File Offset: 0x00007718
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			this.InitPassData(ref this.m_PassData);
			UniversalRenderingData universalRenderingData = renderingData.frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = renderingData.frameData.Get<UniversalLightData>();
			RendererListParams param = this.InitRendererListParams(universalRenderingData, cameraData, lightData);
			RendererList rendererList = context.CreateRendererList(ref param);
			using (new ProfilingScope(universalRenderingData.commandBuffer, base.profilingSampler))
			{
				DecalForwardEmissivePass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(universalRenderingData.commandBuffer), this.m_PassData, rendererList);
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000095B8 File Offset: 0x000077B8
		private void InitPassData(ref DecalForwardEmissivePass.PassData passData)
		{
			passData.drawSystem = this.m_DrawSystem;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000095C8 File Offset: 0x000077C8
		private RendererListParams InitRendererListParams(UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData)
		{
			SortingCriteria sortingCriteria = cameraData.defaultOpaqueSortFlags;
			DrawingSettings drawingSettings = RenderingUtils.CreateDrawingSettings(this.m_ShaderTagIdList, renderingData, cameraData, lightData, sortingCriteria);
			return new RendererListParams(renderingData.cullResults, drawingSettings, this.m_FilteringSettings);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000095FE File Offset: 0x000077FE
		private static void ExecutePass(RasterCommandBuffer cmd, DecalForwardEmissivePass.PassData passData, RendererList rendererList)
		{
			passData.drawSystem.Execute(cmd);
			cmd.DrawRendererList(rendererList);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00009614 File Offset: 0x00007814
		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
			DecalForwardEmissivePass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<DecalForwardEmissivePass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Decal/DBuffer/DecalForwardEmissivePass.cs", 82))
			{
				UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
				UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
				UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
				UniversalLightData lightData = frameData.Get<UniversalLightData>();
				this.InitPassData(ref passData);
				RendererListParams param = this.InitRendererListParams(renderingData, cameraData, lightData);
				passData.rendererList = renderGraph.CreateRendererList(in param);
				builder.UseRendererList(in passData.rendererList);
				UniversalRenderer universalRenderer = (UniversalRenderer)cameraData.renderer;
				builder.SetRenderAttachment(resourceData.activeColorTexture, 0, AccessFlags.Write);
				builder.SetRenderAttachmentDepth(resourceData.activeDepthTexture, AccessFlags.Read);
				builder.SetRenderFunc<DecalForwardEmissivePass.PassData>(delegate(DecalForwardEmissivePass.PassData data, RasterGraphContext rgContext)
				{
					DecalForwardEmissivePass.ExecutePass(rgContext.cmd, data, data.rendererList);
				});
			}
		}

		// Token: 0x04000226 RID: 550
		private FilteringSettings m_FilteringSettings;

		// Token: 0x04000227 RID: 551
		private List<ShaderTagId> m_ShaderTagIdList;

		// Token: 0x04000228 RID: 552
		private DecalDrawFowardEmissiveSystem m_DrawSystem;

		// Token: 0x04000229 RID: 553
		private DecalForwardEmissivePass.PassData m_PassData;

		// Token: 0x02000078 RID: 120
		private class PassData
		{
			// Token: 0x0400022A RID: 554
			internal DecalDrawFowardEmissiveSystem drawSystem;

			// Token: 0x0400022B RID: 555
			internal RendererListHandle rendererList;
		}
	}
}
