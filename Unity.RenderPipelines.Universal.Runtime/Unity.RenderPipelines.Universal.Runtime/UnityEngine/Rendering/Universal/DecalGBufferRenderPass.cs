using System;
using System.Collections.Generic;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000097 RID: 151
	internal class DecalGBufferRenderPass : ScriptableRenderPass
	{
		// Token: 0x0600035E RID: 862 RVA: 0x0000CAD8 File Offset: 0x0000ACD8
		public DecalGBufferRenderPass(DecalScreenSpaceSettings settings, DecalDrawGBufferSystem drawSystem, bool decalLayers)
		{
			base.renderPassEvent = RenderPassEvent.AfterRenderingGbuffer;
			this.m_DrawSystem = drawSystem;
			this.m_Settings = settings;
			base.profilingSampler = new ProfilingSampler("Draw Decal To GBuffer");
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(RenderQueueRange.opaque), -1, uint.MaxValue, 0);
			this.m_DecalLayers = decalLayers;
			this.m_ShaderTagIdList = new List<ShaderTagId>();
			if (drawSystem == null)
			{
				this.m_ShaderTagIdList.Add(new ShaderTagId("DecalGBufferProjector"));
			}
			else
			{
				this.m_ShaderTagIdList.Add(new ShaderTagId("DecalGBufferMesh"));
			}
			this.m_PassData = new DecalGBufferRenderPass.PassData();
			this.m_GbufferAttachments = new RTHandle[4];
			base.breakGBufferAndDeferredRenderPass = false;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000CB8B File Offset: 0x0000AD8B
		internal void Setup(DeferredLights deferredLights)
		{
			this.m_DeferredLights = deferredLights;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000CB94 File Offset: 0x0000AD94
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			if (this.m_DeferredLights.UseFramebufferFetch)
			{
				this.m_GbufferAttachments[0] = this.m_DeferredLights.GbufferAttachments[0];
				this.m_GbufferAttachments[1] = this.m_DeferredLights.GbufferAttachments[1];
				this.m_GbufferAttachments[2] = this.m_DeferredLights.GbufferAttachments[2];
				this.m_GbufferAttachments[3] = this.m_DeferredLights.GbufferAttachments[3];
				if (this.m_DecalLayers)
				{
					RTHandle[] deferredInputAttachments = new RTHandle[]
					{
						this.m_DeferredLights.GbufferAttachments[this.m_DeferredLights.GbufferDepthIndex],
						this.m_DeferredLights.GbufferAttachments[this.m_DeferredLights.GBufferRenderingLayers]
					};
					bool[] array = new bool[2];
					array[0] = true;
					bool[] deferredInputIsTransient = array;
					base.ConfigureInputAttachments(deferredInputAttachments, deferredInputIsTransient);
				}
				else
				{
					RTHandle[] deferredInputAttachments2 = new RTHandle[] { this.m_DeferredLights.GbufferAttachments[this.m_DeferredLights.GbufferDepthIndex] };
					bool[] deferredInputIsTransient2 = new bool[] { true };
					base.ConfigureInputAttachments(deferredInputAttachments2, deferredInputIsTransient2);
				}
			}
			else
			{
				this.m_GbufferAttachments[0] = this.m_DeferredLights.GbufferAttachments[0];
				this.m_GbufferAttachments[1] = this.m_DeferredLights.GbufferAttachments[1];
				this.m_GbufferAttachments[2] = this.m_DeferredLights.GbufferAttachments[2];
				this.m_GbufferAttachments[3] = this.m_DeferredLights.GbufferAttachments[3];
			}
			base.ConfigureTarget(this.m_GbufferAttachments, this.m_DeferredLights.DepthAttachmentHandle);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000CD04 File Offset: 0x0000AF04
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
			this.InitPassData(cameraData, ref this.m_PassData);
			SortingCriteria sortingCriteria = *renderingData.cameraData.defaultOpaqueSortFlags;
			DrawingSettings drawingSettings = RenderingUtils.CreateDrawingSettings(this.m_ShaderTagIdList, ref renderingData, sortingCriteria);
			RendererListParams param = new RendererListParams(*renderingData.cullResults, drawingSettings, this.m_FilteringSettings);
			RendererList rendererList = context.CreateRendererList(ref param);
			using (new ProfilingScope(*renderingData.commandBuffer, base.profilingSampler))
			{
				DecalGBufferRenderPass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer), this.m_PassData, rendererList);
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000CDB8 File Offset: 0x0000AFB8
		private void InitPassData(UniversalCameraData cameraData, ref DecalGBufferRenderPass.PassData passData)
		{
			passData.drawSystem = this.m_DrawSystem;
			passData.settings = this.m_Settings;
			passData.decalLayers = this.m_DecalLayers;
			passData.cameraData = cameraData;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000CDEC File Offset: 0x0000AFEC
		private static void ExecutePass(RasterCommandBuffer cmd, DecalGBufferRenderPass.PassData passData, RendererList rendererList)
		{
			NormalReconstruction.SetupProperties(cmd, in passData.cameraData);
			cmd.SetKeyword(in ShaderGlobalKeywords.DecalNormalBlendLow, passData.settings.normalBlend == DecalNormalBlend.Low);
			cmd.SetKeyword(in ShaderGlobalKeywords.DecalNormalBlendMedium, passData.settings.normalBlend == DecalNormalBlend.Medium);
			cmd.SetKeyword(in ShaderGlobalKeywords.DecalNormalBlendHigh, passData.settings.normalBlend == DecalNormalBlend.High);
			cmd.SetKeyword(in ShaderGlobalKeywords.DecalLayers, passData.decalLayers);
			DecalDrawGBufferSystem drawSystem = passData.drawSystem;
			if (drawSystem != null)
			{
				drawSystem.Execute(cmd);
			}
			cmd.DrawRendererList(rendererList);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000CE7C File Offset: 0x0000B07C
		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
			UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
			TextureHandle cameraDepthTexture = resourceData.cameraDepthTexture;
			DecalGBufferRenderPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<DecalGBufferRenderPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Decal/ScreenSpace/DecalGBufferRenderPass.cs", 167))
			{
				UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
				UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
				UniversalLightData lightData = frameData.Get<UniversalLightData>();
				this.InitPassData(cameraData, ref passData);
				TextureHandle[] gBufferHandles = resourceData.gBuffer;
				builder.SetRenderAttachment(gBufferHandles[0], 0, AccessFlags.Write);
				builder.SetRenderAttachment(gBufferHandles[1], 1, AccessFlags.Write);
				builder.SetRenderAttachment(gBufferHandles[2], 2, AccessFlags.Write);
				builder.SetRenderAttachment(gBufferHandles[3], 3, AccessFlags.Write);
				builder.SetRenderAttachmentDepth(resourceData.activeDepthTexture, AccessFlags.Read);
				if (renderGraph.nativeRenderPassesEnabled)
				{
					builder.SetInputAttachment(gBufferHandles[4], 0, AccessFlags.Read);
					if (this.m_DecalLayers)
					{
						builder.SetInputAttachment(gBufferHandles[5], 1, AccessFlags.Read);
					}
				}
				else if (cameraDepthTexture.IsValid())
				{
					builder.UseTexture(in cameraDepthTexture, AccessFlags.Read);
				}
				SortingCriteria sortingCriteria = passData.cameraData.defaultOpaqueSortFlags;
				DrawingSettings drawingSettings = RenderingUtils.CreateDrawingSettings(this.m_ShaderTagIdList, renderingData, passData.cameraData, lightData, sortingCriteria);
				RendererListParams param = new RendererListParams(renderingData.cullResults, drawingSettings, this.m_FilteringSettings);
				passData.rendererList = renderGraph.CreateRendererList(in param);
				builder.UseRendererList(in passData.rendererList);
				builder.AllowGlobalStateModification(true);
				builder.SetRenderFunc<DecalGBufferRenderPass.PassData>(delegate(DecalGBufferRenderPass.PassData data, RasterGraphContext rgContext)
				{
					DecalGBufferRenderPass.ExecutePass(rgContext.cmd, data, data.rendererList);
				});
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000D01C File Offset: 0x0000B21C
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

		// Token: 0x040002DB RID: 731
		private FilteringSettings m_FilteringSettings;

		// Token: 0x040002DC RID: 732
		private List<ShaderTagId> m_ShaderTagIdList;

		// Token: 0x040002DD RID: 733
		private DecalDrawGBufferSystem m_DrawSystem;

		// Token: 0x040002DE RID: 734
		private DecalScreenSpaceSettings m_Settings;

		// Token: 0x040002DF RID: 735
		private DeferredLights m_DeferredLights;

		// Token: 0x040002E0 RID: 736
		private RTHandle[] m_GbufferAttachments;

		// Token: 0x040002E1 RID: 737
		private bool m_DecalLayers;

		// Token: 0x040002E2 RID: 738
		private DecalGBufferRenderPass.PassData m_PassData;

		// Token: 0x02000098 RID: 152
		private class PassData
		{
			// Token: 0x040002E3 RID: 739
			internal DecalDrawGBufferSystem drawSystem;

			// Token: 0x040002E4 RID: 740
			internal DecalScreenSpaceSettings settings;

			// Token: 0x040002E5 RID: 741
			internal bool decalLayers;

			// Token: 0x040002E6 RID: 742
			internal UniversalCameraData cameraData;

			// Token: 0x040002E7 RID: 743
			internal RendererListHandle rendererList;
		}
	}
}
