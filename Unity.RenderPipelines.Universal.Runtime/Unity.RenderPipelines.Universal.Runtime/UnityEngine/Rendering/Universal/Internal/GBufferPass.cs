using System;
using Unity.Collections;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x0200021F RID: 543
	internal class GBufferPass : ScriptableRenderPass
	{
		// Token: 0x06000C16 RID: 3094 RVA: 0x00042300 File Offset: 0x00040500
		public GBufferPass(RenderPassEvent evt, RenderQueueRange renderQueueRange, LayerMask layerMask, StencilState stencilState, int stencilReference, DeferredLights deferredLights)
		{
			base.profilingSampler = new ProfilingSampler("Draw GBuffer");
			base.renderPassEvent = evt;
			this.m_PassData = new GBufferPass.PassData();
			this.m_DeferredLights = deferredLights;
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(renderQueueRange), layerMask, uint.MaxValue, 0);
			this.m_RenderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
			this.m_RenderStateBlock.stencilState = stencilState;
			this.m_RenderStateBlock.stencilReference = stencilReference;
			this.m_RenderStateBlock.mask = RenderStateMask.Stencil;
			if (GBufferPass.s_ShaderTagValues == null)
			{
				GBufferPass.s_ShaderTagValues = new ShaderTagId[5];
				GBufferPass.s_ShaderTagValues[0] = GBufferPass.s_ShaderTagLit;
				GBufferPass.s_ShaderTagValues[1] = GBufferPass.s_ShaderTagSimpleLit;
				GBufferPass.s_ShaderTagValues[2] = GBufferPass.s_ShaderTagUnlit;
				GBufferPass.s_ShaderTagValues[3] = GBufferPass.s_ShaderTagComplexLit;
				GBufferPass.s_ShaderTagValues[4] = default(ShaderTagId);
			}
			if (GBufferPass.s_RenderStateBlocks == null)
			{
				GBufferPass.s_RenderStateBlocks = new RenderStateBlock[5];
				GBufferPass.s_RenderStateBlocks[0] = DeferredLights.OverwriteStencil(this.m_RenderStateBlock, 96, 32);
				GBufferPass.s_RenderStateBlocks[1] = DeferredLights.OverwriteStencil(this.m_RenderStateBlock, 96, 64);
				GBufferPass.s_RenderStateBlocks[2] = DeferredLights.OverwriteStencil(this.m_RenderStateBlock, 96, 0);
				GBufferPass.s_RenderStateBlocks[3] = DeferredLights.OverwriteStencil(this.m_RenderStateBlock, 96, 0);
				GBufferPass.s_RenderStateBlocks[4] = GBufferPass.s_RenderStateBlocks[0];
			}
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0004247C File Offset: 0x0004067C
		public void Dispose()
		{
			DeferredLights deferredLights = this.m_DeferredLights;
			if (deferredLights == null)
			{
				return;
			}
			deferredLights.ReleaseGbufferResources();
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00042490 File Offset: 0x00040690
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			RTHandle[] gbufferAttachments = this.m_DeferredLights.GbufferAttachments;
			if (cmd != null)
			{
				bool allocateGbufferDepth = true;
				if (this.m_DeferredLights.UseFramebufferFetch && this.m_DeferredLights.DepthCopyTexture != null && this.m_DeferredLights.DepthCopyTexture.rt != null)
				{
					this.m_DeferredLights.GbufferAttachments[this.m_DeferredLights.GbufferDepthIndex] = this.m_DeferredLights.DepthCopyTexture;
					allocateGbufferDepth = false;
				}
				for (int i = 0; i < gbufferAttachments.Length; i++)
				{
					if (i != this.m_DeferredLights.GBufferLightingIndex && (i != this.m_DeferredLights.GBufferNormalSmoothnessIndex || !this.m_DeferredLights.HasNormalPrepass) && (i != this.m_DeferredLights.GbufferDepthIndex || allocateGbufferDepth) && (!this.m_DeferredLights.UseFramebufferFetch || i == this.m_DeferredLights.GbufferDepthIndex || this.m_DeferredLights.HasDepthPrepass))
					{
						this.m_DeferredLights.ReAllocateGBufferIfNeeded(cameraTextureDescriptor, i);
						cmd.SetGlobalTexture(this.m_DeferredLights.GbufferAttachments[i].name, this.m_DeferredLights.GbufferAttachments[i].nameID);
					}
				}
			}
			if (this.m_DeferredLights.UseFramebufferFetch)
			{
				this.m_DeferredLights.UpdateDeferredInputAttachments();
			}
			base.ConfigureTarget(this.m_DeferredLights.GbufferAttachments, this.m_DeferredLights.DepthAttachment, this.m_DeferredLights.GbufferFormats);
			base.ConfigureClear(ClearFlag.None, Color.black);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00042604 File Offset: 0x00040804
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			ContextContainer frameData = renderingData.frameData;
			UniversalRenderingData universalRenderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			this.m_PassData.deferredLights = this.m_DeferredLights;
			this.InitRendererLists(ref this.m_PassData, context, null, universalRenderingData, cameraData, lightData, false);
			CommandBuffer cmd = *renderingData.commandBuffer;
			using (new ProfilingScope(cmd, base.profilingSampler))
			{
				GBufferPass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(cmd), this.m_PassData, this.m_PassData.rendererList, this.m_PassData.objectsWithErrorRendererList);
				if (!this.m_DeferredLights.UseFramebufferFetch)
				{
					renderingData.commandBuffer->SetGlobalTexture(GBufferPass.s_CameraNormalsTextureID, this.m_DeferredLights.GbufferAttachments[this.m_DeferredLights.GBufferNormalSmoothnessIndex]);
				}
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x000426E8 File Offset: 0x000408E8
		private static void ExecutePass(RasterCommandBuffer cmd, GBufferPass.PassData data, RendererList rendererList, RendererList errorRendererList)
		{
			bool flag = data.deferredLights.UseRenderingLayers && !data.deferredLights.HasRenderingLayerPrepass;
			if (flag)
			{
				cmd.SetKeyword(in ShaderGlobalKeywords.WriteRenderingLayers, true);
			}
			if (data.deferredLights.IsOverlay)
			{
				data.deferredLights.ClearStencilPartial(cmd);
			}
			cmd.DrawRendererList(rendererList);
			if (flag)
			{
				cmd.SetKeyword(in ShaderGlobalKeywords.WriteRenderingLayers, false);
			}
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00042750 File Offset: 0x00040950
		private void InitRendererLists(ref GBufferPass.PassData passData, ScriptableRenderContext context, RenderGraph renderGraph, UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData, bool useRenderGraph)
		{
			ShaderTagId lightModeTag = GBufferPass.s_ShaderTagUniversalGBuffer;
			DrawingSettings drawingSettings = base.CreateDrawingSettings(lightModeTag, renderingData, cameraData, lightData, cameraData.defaultOpaqueSortFlags);
			FilteringSettings filterSettings = this.m_FilteringSettings;
			NativeArray<ShaderTagId> tagValues = new NativeArray<ShaderTagId>(GBufferPass.s_ShaderTagValues, Allocator.Temp);
			NativeArray<RenderStateBlock> stateBlocks = new NativeArray<RenderStateBlock>(GBufferPass.s_RenderStateBlocks, Allocator.Temp);
			RendererListParams param = new RendererListParams(renderingData.cullResults, drawingSettings, filterSettings)
			{
				tagValues = new NativeArray<ShaderTagId>?(tagValues),
				stateBlocks = new NativeArray<RenderStateBlock>?(stateBlocks),
				tagName = GBufferPass.s_ShaderTagUniversalMaterialType,
				isPassTagName = false
			};
			if (useRenderGraph)
			{
				passData.rendererListHdl = renderGraph.CreateRendererList(in param);
			}
			else
			{
				passData.rendererList = context.CreateRendererList(ref param);
			}
			tagValues.Dispose();
			stateBlocks.Dispose();
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00042814 File Offset: 0x00040A14
		internal void Render(RenderGraph renderGraph, ContextContainer frameData, TextureHandle cameraColor, TextureHandle cameraDepth, bool setGlobalTextures)
		{
			UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
			UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			bool useCameraRenderingLayersTexture = this.m_DeferredLights.UseRenderingLayers && !this.m_DeferredLights.UseLightLayers;
			GBufferPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<GBufferPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/GBufferPass.cs", 240))
			{
				TextureHandle[] gbuffer = (passData.gbuffer = this.m_DeferredLights.GbufferTextureHandles);
				for (int i = 0; i < this.m_DeferredLights.GBufferSliceCount; i++)
				{
					RenderTextureDescriptor gbufferSlice = cameraData.cameraTargetDescriptor;
					gbufferSlice.depthStencilFormat = GraphicsFormat.None;
					gbufferSlice.stencilFormat = GraphicsFormat.None;
					if (i == this.m_DeferredLights.GBufferNormalSmoothnessIndex && this.m_DeferredLights.HasNormalPrepass)
					{
						gbuffer[i] = resourceData.cameraNormalsTexture;
					}
					else if (i == this.m_DeferredLights.GBufferRenderingLayers && useCameraRenderingLayersTexture)
					{
						gbuffer[i] = resourceData.renderingLayersTexture;
					}
					else if (i != this.m_DeferredLights.GBufferLightingIndex)
					{
						gbufferSlice.graphicsFormat = this.m_DeferredLights.GetGBufferFormat(i);
						gbuffer[i] = UniversalRenderer.CreateRenderGraphTexture(renderGraph, gbufferSlice, DeferredLights.k_GBufferNames[i], true, FilterMode.Point, TextureWrapMode.Clamp);
					}
					else
					{
						gbuffer[i] = cameraColor;
					}
					builder.SetRenderAttachment(gbuffer[i], i, AccessFlags.Write);
				}
				RenderGraphUtils.UseDBufferIfValid(builder, resourceData);
				resourceData.gBuffer = gbuffer;
				passData.depth = cameraDepth;
				builder.SetRenderAttachmentDepth(cameraDepth, AccessFlags.Write);
				passData.deferredLights = this.m_DeferredLights;
				this.InitRendererLists(ref passData, default(ScriptableRenderContext), renderGraph, renderingData, cameraData, lightData, true);
				builder.UseRendererList(in passData.rendererListHdl);
				builder.UseRendererList(in passData.objectsWithErrorRendererListHdl);
				if (setGlobalTextures)
				{
					IBaseRenderGraphBuilder baseRenderGraphBuilder = builder;
					TextureHandle textureHandle = resourceData.cameraNormalsTexture;
					baseRenderGraphBuilder.SetGlobalTextureAfterPass(in textureHandle, GBufferPass.s_CameraNormalsTextureID);
					if (useCameraRenderingLayersTexture)
					{
						IBaseRenderGraphBuilder baseRenderGraphBuilder2 = builder;
						textureHandle = resourceData.renderingLayersTexture;
						baseRenderGraphBuilder2.SetGlobalTextureAfterPass(in textureHandle, GBufferPass.s_CameraRenderingLayersTextureID);
					}
				}
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				builder.SetRenderFunc<GBufferPass.PassData>(delegate(GBufferPass.PassData data, RasterGraphContext context)
				{
					GBufferPass.ExecutePass(context.cmd, data, data.rendererListHdl, data.objectsWithErrorRendererListHdl);
				});
			}
		}

		// Token: 0x04000D96 RID: 3478
		private static readonly int s_CameraNormalsTextureID = Shader.PropertyToID("_CameraNormalsTexture");

		// Token: 0x04000D97 RID: 3479
		private static readonly int s_CameraRenderingLayersTextureID = Shader.PropertyToID("_CameraRenderingLayersTexture");

		// Token: 0x04000D98 RID: 3480
		private static readonly ShaderTagId s_ShaderTagLit = new ShaderTagId("Lit");

		// Token: 0x04000D99 RID: 3481
		private static readonly ShaderTagId s_ShaderTagSimpleLit = new ShaderTagId("SimpleLit");

		// Token: 0x04000D9A RID: 3482
		private static readonly ShaderTagId s_ShaderTagUnlit = new ShaderTagId("Unlit");

		// Token: 0x04000D9B RID: 3483
		private static readonly ShaderTagId s_ShaderTagComplexLit = new ShaderTagId("ComplexLit");

		// Token: 0x04000D9C RID: 3484
		private static readonly ShaderTagId s_ShaderTagUniversalGBuffer = new ShaderTagId("UniversalGBuffer");

		// Token: 0x04000D9D RID: 3485
		private static readonly ShaderTagId s_ShaderTagUniversalMaterialType = new ShaderTagId("UniversalMaterialType");

		// Token: 0x04000D9E RID: 3486
		private DeferredLights m_DeferredLights;

		// Token: 0x04000D9F RID: 3487
		private static ShaderTagId[] s_ShaderTagValues;

		// Token: 0x04000DA0 RID: 3488
		private static RenderStateBlock[] s_RenderStateBlocks;

		// Token: 0x04000DA1 RID: 3489
		private FilteringSettings m_FilteringSettings;

		// Token: 0x04000DA2 RID: 3490
		private RenderStateBlock m_RenderStateBlock;

		// Token: 0x04000DA3 RID: 3491
		private GBufferPass.PassData m_PassData;

		// Token: 0x02000220 RID: 544
		private class PassData
		{
			// Token: 0x04000DA4 RID: 3492
			internal TextureHandle[] gbuffer;

			// Token: 0x04000DA5 RID: 3493
			internal TextureHandle depth;

			// Token: 0x04000DA6 RID: 3494
			internal DeferredLights deferredLights;

			// Token: 0x04000DA7 RID: 3495
			internal RendererListHandle rendererListHdl;

			// Token: 0x04000DA8 RID: 3496
			internal RendererListHandle objectsWithErrorRendererListHdl;

			// Token: 0x04000DA9 RID: 3497
			internal RendererList rendererList;

			// Token: 0x04000DAA RID: 3498
			internal RendererList objectsWithErrorRendererList;
		}
	}
}
