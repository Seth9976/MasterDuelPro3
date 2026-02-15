using System;
using System.Collections.Generic;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200009B RID: 155
	internal class DecalScreenSpaceRenderPass : ScriptableRenderPass
	{
		// Token: 0x0600036C RID: 876 RVA: 0x0000D098 File Offset: 0x0000B298
		public DecalScreenSpaceRenderPass(DecalScreenSpaceSettings settings, DecalDrawScreenSpaceSystem drawSystem, bool decalLayers)
		{
			base.renderPassEvent = RenderPassEvent.AfterRenderingSkybox;
			ScriptableRenderPassInput scriptableRenderPassInput = ScriptableRenderPassInput.Depth;
			base.ConfigureInput(scriptableRenderPassInput);
			this.m_DrawSystem = drawSystem;
			this.m_Settings = settings;
			base.profilingSampler = new ProfilingSampler("Draw Decal Screen Space");
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(RenderQueueRange.opaque), -1, uint.MaxValue, 0);
			this.m_DecalLayers = decalLayers;
			this.m_ShaderTagIdList = new List<ShaderTagId>();
			if (this.m_DrawSystem == null)
			{
				this.m_ShaderTagIdList.Add(new ShaderTagId("DecalScreenSpaceProjector"));
			}
			else
			{
				this.m_ShaderTagIdList.Add(new ShaderTagId("DecalScreenSpaceMesh"));
			}
			this.m_PassData = new DecalScreenSpaceRenderPass.PassData();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000D148 File Offset: 0x0000B348
		private RendererListParams CreateRenderListParams(UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData)
		{
			SortingCriteria sortingCriteria = SortingCriteria.None;
			DrawingSettings drawingSettings = RenderingUtils.CreateDrawingSettings(this.m_ShaderTagIdList, renderingData, cameraData, lightData, sortingCriteria);
			return new RendererListParams(renderingData.cullResults, drawingSettings, this.m_FilteringSettings);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000D17C File Offset: 0x0000B37C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
			this.InitPassData(cameraData, ref this.m_PassData);
			RenderingUtils.SetScaleBiasRt(CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer), in renderingData);
			UniversalRenderingData universalRenderingData = renderingData.frameData.Get<UniversalRenderingData>();
			UniversalLightData lightData = renderingData.frameData.Get<UniversalLightData>();
			RendererListParams param = this.CreateRenderListParams(universalRenderingData, cameraData, lightData);
			RendererList rendererList = context.CreateRendererList(ref param);
			using (new ProfilingScope(*renderingData.commandBuffer, base.profilingSampler))
			{
				DecalScreenSpaceRenderPass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer), this.m_PassData, rendererList);
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000D230 File Offset: 0x0000B430
		private void InitPassData(UniversalCameraData cameraData, ref DecalScreenSpaceRenderPass.PassData passData)
		{
			passData.drawSystem = this.m_DrawSystem;
			passData.settings = this.m_Settings;
			passData.decalLayers = this.m_DecalLayers;
			passData.isGLDevice = DecalRendererFeature.isGLDevice;
			passData.cameraData = cameraData;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000D270 File Offset: 0x0000B470
		private static void ExecutePass(RasterCommandBuffer cmd, DecalScreenSpaceRenderPass.PassData passData, RendererList rendererList)
		{
			NormalReconstruction.SetupProperties(cmd, in passData.cameraData);
			cmd.SetKeyword(in ShaderGlobalKeywords.DecalNormalBlendLow, passData.settings.normalBlend == DecalNormalBlend.Low);
			cmd.SetKeyword(in ShaderGlobalKeywords.DecalNormalBlendMedium, passData.settings.normalBlend == DecalNormalBlend.Medium);
			cmd.SetKeyword(in ShaderGlobalKeywords.DecalNormalBlendHigh, passData.settings.normalBlend == DecalNormalBlend.High);
			if (!passData.isGLDevice)
			{
				cmd.SetKeyword(in ShaderGlobalKeywords.DecalLayers, passData.decalLayers);
			}
			DecalDrawScreenSpaceSystem drawSystem = passData.drawSystem;
			if (drawSystem != null)
			{
				drawSystem.Execute(cmd);
			}
			cmd.DrawRendererList(rendererList);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000D308 File Offset: 0x0000B508
		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
			UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
			TextureHandle cameraDepthTexture = resourceData.cameraDepthTexture;
			DecalScreenSpaceRenderPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<DecalScreenSpaceRenderPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Decal/ScreenSpace/DecalScreenSpaceRenderPass.cs", 113))
			{
				UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
				UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
				UniversalLightData lightData = frameData.Get<UniversalLightData>();
				this.InitPassData(cameraData, ref passData);
				passData.colorTarget = resourceData.cameraColor;
				builder.SetRenderAttachment(resourceData.activeColorTexture, 0, AccessFlags.Write);
				builder.SetRenderAttachmentDepth(resourceData.activeDepthTexture, AccessFlags.Read);
				RendererListParams param = this.CreateRenderListParams(renderingData, passData.cameraData, lightData);
				passData.rendererList = renderGraph.CreateRendererList(in param);
				builder.UseRendererList(in passData.rendererList);
				if (cameraDepthTexture.IsValid())
				{
					builder.UseTexture(in cameraDepthTexture, AccessFlags.Read);
				}
				builder.AllowGlobalStateModification(true);
				builder.SetRenderFunc<DecalScreenSpaceRenderPass.PassData>(delegate(DecalScreenSpaceRenderPass.PassData data, RasterGraphContext rgContext)
				{
					RenderingUtils.SetScaleBiasRt(rgContext.cmd, in data.cameraData, data.colorTarget);
					DecalScreenSpaceRenderPass.ExecutePass(rgContext.cmd, data, data.rendererList);
				});
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000D01C File Offset: 0x0000B21C
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			cmd.SetKeyword(in ShaderGlobalKeywords.DecalNormalBlendLow, false);
			cmd.SetKeyword(in ShaderGlobalKeywords.DecalNormalBlendMedium, false);
			cmd.SetKeyword(in ShaderGlobalKeywords.DecalNormalBlendHigh, false);
			cmd.SetKeyword(in ShaderGlobalKeywords.DecalLayers, false);
		}

		// Token: 0x040002EA RID: 746
		private FilteringSettings m_FilteringSettings;

		// Token: 0x040002EB RID: 747
		private List<ShaderTagId> m_ShaderTagIdList;

		// Token: 0x040002EC RID: 748
		private DecalDrawScreenSpaceSystem m_DrawSystem;

		// Token: 0x040002ED RID: 749
		private DecalScreenSpaceSettings m_Settings;

		// Token: 0x040002EE RID: 750
		private bool m_DecalLayers;

		// Token: 0x040002EF RID: 751
		private DecalScreenSpaceRenderPass.PassData m_PassData;

		// Token: 0x0200009C RID: 156
		private class PassData
		{
			// Token: 0x040002F0 RID: 752
			internal DecalDrawScreenSpaceSystem drawSystem;

			// Token: 0x040002F1 RID: 753
			internal DecalScreenSpaceSettings settings;

			// Token: 0x040002F2 RID: 754
			internal bool decalLayers;

			// Token: 0x040002F3 RID: 755
			internal bool isGLDevice;

			// Token: 0x040002F4 RID: 756
			internal TextureHandle colorTarget;

			// Token: 0x040002F5 RID: 757
			internal UniversalCameraData cameraData;

			// Token: 0x040002F6 RID: 758
			internal RendererListHandle rendererList;
		}
	}
}
