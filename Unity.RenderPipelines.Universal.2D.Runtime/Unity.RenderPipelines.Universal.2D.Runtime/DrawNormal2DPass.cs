using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200005B RID: 91
	internal class DrawNormal2DPass : ScriptableRenderPass
	{
		// Token: 0x06000270 RID: 624 RVA: 0x0001324C File Offset: 0x0001144C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00013F19 File Offset: 0x00012119
		private static void Execute(RasterCommandBuffer cmd, DrawNormal2DPass.PassData passData)
		{
			cmd.DrawRendererList(passData.rendererList);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00013F2C File Offset: 0x0001212C
		public void Render(RenderGraph graph, ContextContainer frameData, Renderer2DData rendererData, ref LayerBatch layerBatch, int batchIndex)
		{
			Universal2DResourceData universal2DResourceData = frameData.Get<Universal2DResourceData>();
			int num = universal2DResourceData.normalsTexture.Length;
			if (!layerBatch.useNormals)
			{
				return;
			}
			UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			DrawNormal2DPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = graph.AddRasterRenderPass<DrawNormal2DPass.PassData>(DrawNormal2DPass.k_NormalPass, out passData, DrawNormal2DPass.m_ProfilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/2D/Rendergraph/DrawNormal2DPass.cs", 42))
			{
				FilteringSettings filterSettings = FilteringSettings.defaultValue;
				filterSettings.renderQueueRange = RenderQueueRange.all;
				filterSettings.layerMask = -1;
				filterSettings.renderingLayerMask = uint.MaxValue;
				filterSettings.sortingLayerRange = new SortingLayerRange(layerBatch.layerRange.lowerBound, layerBatch.layerRange.upperBound);
				DrawingSettings drawSettings = base.CreateDrawingSettings(DrawNormal2DPass.k_NormalsRenderingPassName, renderingData, cameraData, lightData, SortingCriteria.CommonTransparent);
				SortingSettings sortSettings = drawSettings.sortingSettings;
				RendererLighting.GetTransparencySortingMode(rendererData, cameraData.camera, ref sortSettings);
				drawSettings.sortingSettings = sortSettings;
				builder.AllowPassCulling(false);
				builder.SetRenderAttachment(universal2DResourceData.normalsTexture[batchIndex], 0, AccessFlags.Write);
				builder.SetRenderAttachmentDepth(universal2DResourceData.intermediateDepth, AccessFlags.Write);
				RendererListParams param = new RendererListParams(renderingData.cullResults, drawSettings, filterSettings);
				passData.rendererList = graph.CreateRendererList(in param);
				builder.UseRendererList(in passData.rendererList);
				builder.SetRenderFunc<DrawNormal2DPass.PassData>(delegate(DrawNormal2DPass.PassData data, RasterGraphContext context)
				{
					DrawNormal2DPass.Execute(context.cmd, data);
				});
			}
		}

		// Token: 0x04000227 RID: 551
		private static readonly string k_NormalPass = "Normal2D Pass";

		// Token: 0x04000228 RID: 552
		private static readonly ProfilingSampler m_ProfilingSampler = new ProfilingSampler(DrawNormal2DPass.k_NormalPass);

		// Token: 0x04000229 RID: 553
		private static readonly ShaderTagId k_NormalsRenderingPassName = new ShaderTagId("NormalsRendering");

		// Token: 0x0200005C RID: 92
		private class PassData
		{
			// Token: 0x0400022A RID: 554
			internal RendererListHandle rendererList;
		}
	}
}
