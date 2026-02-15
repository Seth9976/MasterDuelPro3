using System;
using System.Collections.Generic;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200005E RID: 94
	internal class DrawRenderer2DPass : ScriptableRenderPass
	{
		// Token: 0x06000279 RID: 633 RVA: 0x0001324C File Offset: 0x0001144C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000140E8 File Offset: 0x000122E8
		private static void Execute(RasterGraphContext context, DrawRenderer2DPass.PassData passData)
		{
			RasterCommandBuffer cmd = context.cmd;
			int blendStylesCount = passData.blendStyleIndices.Length;
			cmd.SetGlobalFloat(DrawRenderer2DPass.k_HDREmulationScaleID, passData.hdrEmulationScale);
			cmd.SetGlobalColor(DrawRenderer2DPass.k_RendererColorID, Color.white);
			RendererLighting.SetLightShaderGlobals(cmd, passData.lightBlendStyles, passData.blendStyleIndices);
			if (passData.layerUseLights)
			{
				for (int i = 0; i < blendStylesCount; i++)
				{
					int blendStyleIndex = passData.blendStyleIndices[i];
					RendererLighting.EnableBlendStyle(cmd, blendStyleIndex, true);
				}
			}
			else if (passData.isSceneLit)
			{
				RendererLighting.EnableBlendStyle(cmd, 0, true);
			}
			cmd.DrawRendererList(passData.rendererList);
			RendererLighting.DisableAllKeywords(cmd);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00014188 File Offset: 0x00012388
		public void Render(RenderGraph graph, ContextContainer frameData, Renderer2DData rendererData, ref LayerBatch[] layerBatches, int batchIndex, ref FilteringSettings filterSettings)
		{
			UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			Universal2DResourceData universal2DResourceData = frameData.Get<Universal2DResourceData>();
			UniversalResourceData commonResourceData = frameData.Get<UniversalResourceData>();
			LayerBatch layerBatch = layerBatches[batchIndex];
			if (batchIndex == 0)
			{
				DrawRenderer2DPass.SetGlobalPassData passData;
				using (IRasterRenderGraphBuilder builder = graph.AddRasterRenderPass<DrawRenderer2DPass.SetGlobalPassData>(DrawRenderer2DPass.k_SetLightBlendTexture, out passData, DrawRenderer2DPass.m_SetLightBlendTextureProfilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/2D/Rendergraph/DrawRenderer2DPass.cs", 96))
				{
					if (layerBatch.lightStats.useAnyLights)
					{
						passData.lightTextures = universal2DResourceData.lightTextures[batchIndex];
						for (int i = 0; i < passData.lightTextures.Length; i++)
						{
							builder.UseTexture(in passData.lightTextures[i], AccessFlags.Read);
						}
					}
					this.SetGlobalLightTextures(graph, builder, passData.lightTextures, cameraData, ref layerBatch, rendererData);
					builder.AllowPassCulling(false);
					builder.AllowGlobalStateModification(true);
					builder.SetRenderFunc<DrawRenderer2DPass.SetGlobalPassData>(delegate(DrawRenderer2DPass.SetGlobalPassData data, RasterGraphContext context)
					{
					});
				}
			}
			DrawRenderer2DPass.PassData passData2;
			using (IRasterRenderGraphBuilder builder2 = graph.AddRasterRenderPass<DrawRenderer2DPass.PassData>(DrawRenderer2DPass.k_RenderPass, out passData2, DrawRenderer2DPass.m_ProfilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/2D/Rendergraph/DrawRenderer2DPass.cs", 117))
			{
				passData2.lightBlendStyles = rendererData.lightBlendStyles;
				passData2.blendStyleIndices = layerBatch.activeBlendStylesIndices;
				passData2.hdrEmulationScale = rendererData.hdrEmulationScale;
				passData2.isSceneLit = rendererData.lightCullResult.IsSceneLit();
				passData2.layerUseLights = layerBatch.lightStats.useAnyLights;
				DrawingSettings drawSettings = base.CreateDrawingSettings(DrawRenderer2DPass.k_ShaderTags, renderingData, cameraData, lightData, SortingCriteria.CommonTransparent);
				SortingSettings sortSettings = drawSettings.sortingSettings;
				RendererLighting.GetTransparencySortingMode(rendererData, cameraData.camera, ref sortSettings);
				drawSettings.sortingSettings = sortSettings;
				RendererListParams param = new RendererListParams(renderingData.cullResults, drawSettings, filterSettings);
				passData2.rendererList = graph.CreateRendererList(in param);
				builder2.UseRendererList(in passData2.rendererList);
				if (passData2.layerUseLights)
				{
					passData2.lightTextures = universal2DResourceData.lightTextures[batchIndex];
					for (int j = 0; j < passData2.lightTextures.Length; j++)
					{
						builder2.UseTexture(in passData2.lightTextures[j], AccessFlags.Read);
					}
				}
				builder2.SetRenderAttachment(commonResourceData.activeColorTexture, 0, AccessFlags.Write);
				builder2.SetRenderAttachmentDepth(commonResourceData.activeDepthTexture, AccessFlags.Write);
				builder2.AllowPassCulling(false);
				builder2.AllowGlobalStateModification(true);
				builder2.UseAllGlobalTextures(true);
				int nextBatch = batchIndex + 1;
				if (nextBatch < universal2DResourceData.lightTextures.Length)
				{
					this.SetGlobalLightTextures(graph, builder2, universal2DResourceData.lightTextures[nextBatch], cameraData, ref layerBatches[nextBatch], rendererData);
				}
				builder2.SetRenderFunc<DrawRenderer2DPass.PassData>(delegate(DrawRenderer2DPass.PassData data, RasterGraphContext context)
				{
					DrawRenderer2DPass.Execute(context, data);
				});
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00014470 File Offset: 0x00012670
		private void SetGlobalLightTextures(RenderGraph graph, IRasterRenderGraphBuilder builder, TextureHandle[] lightTextures, UniversalCameraData cameraData, ref LayerBatch layerBatch, Renderer2DData rendererData)
		{
			if (layerBatch.lightStats.useAnyLights)
			{
				for (int i = 0; i < lightTextures.Length; i++)
				{
					int blendStyleIndex = layerBatch.activeBlendStylesIndices[i];
					builder.SetGlobalTextureAfterPass(in lightTextures[i], Shader.PropertyToID(RendererLighting.k_ShapeLightTextureIDs[blendStyleIndex]));
				}
				return;
			}
			if (rendererData.lightCullResult.IsSceneLit())
			{
				TextureHandle blackTexture = graph.defaultResources.blackTexture;
				builder.SetGlobalTextureAfterPass(in blackTexture, Shader.PropertyToID(RendererLighting.k_ShapeLightTextureIDs[0]));
			}
		}

		// Token: 0x0400022D RID: 557
		private static readonly string k_RenderPass = "Renderer2D Pass";

		// Token: 0x0400022E RID: 558
		private static readonly string k_SetLightBlendTexture = "SetLightBlendTextures";

		// Token: 0x0400022F RID: 559
		private static readonly ProfilingSampler m_ProfilingSampler = new ProfilingSampler(DrawRenderer2DPass.k_RenderPass);

		// Token: 0x04000230 RID: 560
		private static readonly ProfilingSampler m_SetLightBlendTextureProfilingSampler = new ProfilingSampler(DrawRenderer2DPass.k_SetLightBlendTexture);

		// Token: 0x04000231 RID: 561
		private static readonly ShaderTagId k_CombinedRenderingPassName = new ShaderTagId("Universal2D");

		// Token: 0x04000232 RID: 562
		private static readonly ShaderTagId k_LegacyPassName = new ShaderTagId("SRPDefaultUnlit");

		// Token: 0x04000233 RID: 563
		private static readonly List<ShaderTagId> k_ShaderTags = new List<ShaderTagId>
		{
			DrawRenderer2DPass.k_LegacyPassName,
			DrawRenderer2DPass.k_CombinedRenderingPassName
		};

		// Token: 0x04000234 RID: 564
		private static readonly int k_HDREmulationScaleID = Shader.PropertyToID("_HDREmulationScale");

		// Token: 0x04000235 RID: 565
		private static readonly int k_RendererColorID = Shader.PropertyToID("_RendererColor");

		// Token: 0x0200005F RID: 95
		private class SetGlobalPassData
		{
			// Token: 0x04000236 RID: 566
			internal TextureHandle[] lightTextures;
		}

		// Token: 0x02000060 RID: 96
		private class PassData
		{
			// Token: 0x04000237 RID: 567
			internal Light2DBlendStyle[] lightBlendStyles;

			// Token: 0x04000238 RID: 568
			internal int[] blendStyleIndices;

			// Token: 0x04000239 RID: 569
			internal float hdrEmulationScale;

			// Token: 0x0400023A RID: 570
			internal bool isSceneLit;

			// Token: 0x0400023B RID: 571
			internal bool layerUseLights;

			// Token: 0x0400023C RID: 572
			internal TextureHandle[] lightTextures;

			// Token: 0x0400023D RID: 573
			internal RendererListHandle rendererList;
		}
	}
}
