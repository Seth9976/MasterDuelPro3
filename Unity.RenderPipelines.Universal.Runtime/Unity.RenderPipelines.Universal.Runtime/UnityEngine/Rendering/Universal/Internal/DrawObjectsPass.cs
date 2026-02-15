using System;
using System.Collections.Generic;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000213 RID: 531
	public class DrawObjectsPass : ScriptableRenderPass
	{
		// Token: 0x06000BF0 RID: 3056 RVA: 0x00040F04 File Offset: 0x0003F104
		public DrawObjectsPass(string profilerTag, ShaderTagId[] shaderTagIds, bool opaque, RenderPassEvent evt, RenderQueueRange renderQueueRange, LayerMask layerMask, StencilState stencilState, int stencilReference)
		{
			this.Init(opaque, evt, renderQueueRange, layerMask, stencilState, stencilReference, shaderTagIds);
			base.profilingSampler = new ProfilingSampler(profilerTag);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00040F38 File Offset: 0x0003F138
		public DrawObjectsPass(string profilerTag, bool opaque, RenderPassEvent evt, RenderQueueRange renderQueueRange, LayerMask layerMask, StencilState stencilState, int stencilReference)
			: this(profilerTag, null, opaque, evt, renderQueueRange, layerMask, stencilState, stencilReference)
		{
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00040F57 File Offset: 0x0003F157
		internal DrawObjectsPass(URPProfileId profileId, bool opaque, RenderPassEvent evt, RenderQueueRange renderQueueRange, LayerMask layerMask, StencilState stencilState, int stencilReference)
		{
			this.Init(opaque, evt, renderQueueRange, layerMask, stencilState, stencilReference, null);
			base.profilingSampler = ProfilingSampler.Get<URPProfileId>(profileId);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00040F88 File Offset: 0x0003F188
		internal void Init(bool opaque, RenderPassEvent evt, RenderQueueRange renderQueueRange, LayerMask layerMask, StencilState stencilState, int stencilReference, ShaderTagId[] shaderTagIds = null)
		{
			if (shaderTagIds == null)
			{
				shaderTagIds = new ShaderTagId[]
				{
					new ShaderTagId("SRPDefaultUnlit"),
					new ShaderTagId("UniversalForward"),
					new ShaderTagId("UniversalForwardOnly")
				};
			}
			this.m_PassData = new DrawObjectsPass.PassData();
			foreach (ShaderTagId sid in shaderTagIds)
			{
				this.m_ShaderTagIdList.Add(sid);
			}
			base.renderPassEvent = evt;
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(renderQueueRange), layerMask, uint.MaxValue, 0);
			this.m_RenderStateBlock = new RenderStateBlock(RenderStateMask.Nothing);
			this.m_IsOpaque = opaque;
			this.m_ShouldTransparentsReceiveShadows = false;
			this.m_IsActiveTargetBackBuffer = false;
			if (stencilState.enabled)
			{
				this.m_RenderStateBlock.stencilReference = stencilReference;
				this.m_RenderStateBlock.mask = RenderStateMask.Stencil;
				this.m_RenderStateBlock.stencilState = stencilState;
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00041078 File Offset: 0x0003F278
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			ContextContainer frameData = renderingData.frameData;
			UniversalRenderingData universalRenderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			this.InitPassData(cameraData, ref this.m_PassData, uint.MaxValue, this.m_IsActiveTargetBackBuffer);
			this.InitRendererLists(universalRenderingData, cameraData, lightData, ref this.m_PassData, context, null, false);
			using (new ProfilingScope(*renderingData.commandBuffer, base.profilingSampler))
			{
				DrawObjectsPass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer), this.m_PassData, this.m_PassData.rendererList, this.m_PassData.objectsWithErrorRendererList, this.m_PassData.cameraData.IsCameraProjectionMatrixFlipped());
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00041138 File Offset: 0x0003F338
		internal static void ExecutePass(RasterCommandBuffer cmd, DrawObjectsPass.PassData data, RendererList rendererList, RendererList objectsWithErrorRendererList, bool yFlip)
		{
			Vector4 drawObjectPassData = new Vector4(0f, 0f, 0f, data.isOpaque ? 1f : 0f);
			cmd.SetGlobalVector(DrawObjectsPass.s_DrawObjectPassDataPropID, drawObjectPassData);
			if (data.cameraData.xr.enabled && data.isActiveTargetBackBuffer)
			{
				cmd.SetViewport(data.cameraData.xr.GetViewport(0));
			}
			float flipSign = (yFlip ? (-1f) : 1f);
			Vector4 scaleBias = ((flipSign < 0f) ? new Vector4(flipSign, 1f, -1f, 1f) : new Vector4(flipSign, 0f, 1f, 1f));
			cmd.SetGlobalVector(ShaderPropertyId.scaleBiasRt, scaleBias);
			float alphaToMaskAvailable = ((data.cameraData.cameraTargetDescriptor.msaaSamples > 1 && data.isOpaque) ? 1f : 0f);
			cmd.SetGlobalFloat(ShaderPropertyId.alphaToMaskAvailable, alphaToMaskAvailable);
			if (ScriptableRenderPass.GetActiveDebugHandler(data.cameraData) != null)
			{
				data.debugRendererLists.DrawWithRendererList(cmd);
				return;
			}
			cmd.DrawRendererList(rendererList);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00041253 File Offset: 0x0003F453
		internal void InitPassData(UniversalCameraData cameraData, ref DrawObjectsPass.PassData passData, uint batchLayerMask, bool isActiveTargetBackBuffer = false)
		{
			passData.cameraData = cameraData;
			passData.isOpaque = this.m_IsOpaque;
			passData.shouldTransparentsReceiveShadows = this.m_ShouldTransparentsReceiveShadows;
			passData.batchLayerMask = batchLayerMask;
			passData.isActiveTargetBackBuffer = isActiveTargetBackBuffer;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00041288 File Offset: 0x0003F488
		internal void InitRendererLists(UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData, ref DrawObjectsPass.PassData passData, ScriptableRenderContext context, RenderGraph renderGraph, bool useRenderGraph)
		{
			Camera camera = cameraData.camera;
			SortingCriteria sortFlags = (this.m_IsOpaque ? cameraData.defaultOpaqueSortFlags : SortingCriteria.CommonTransparent);
			if (cameraData.renderer.useDepthPriming && this.m_IsOpaque && (cameraData.renderType == CameraRenderType.Base || cameraData.clearDepth))
			{
				sortFlags = SortingCriteria.SortingLayer | SortingCriteria.RenderQueue | SortingCriteria.OptimizeStateChanges | SortingCriteria.CanvasOrder;
			}
			FilteringSettings filterSettings = this.m_FilteringSettings;
			filterSettings.batchLayerMask = passData.batchLayerMask;
			DrawingSettings drawSettings = RenderingUtils.CreateDrawingSettings(this.m_ShaderTagIdList, renderingData, cameraData, lightData, sortFlags);
			if (cameraData.renderer.useDepthPriming && this.m_IsOpaque && (cameraData.renderType == CameraRenderType.Base || cameraData.clearDepth))
			{
				this.m_RenderStateBlock.depthState = new DepthState(false, CompareFunction.Equal);
				this.m_RenderStateBlock.mask = this.m_RenderStateBlock.mask | RenderStateMask.Depth;
			}
			else if (this.m_RenderStateBlock.depthState.compareFunction == CompareFunction.Equal)
			{
				this.m_RenderStateBlock.depthState = new DepthState(true, CompareFunction.LessEqual);
				this.m_RenderStateBlock.mask = this.m_RenderStateBlock.mask | RenderStateMask.Depth;
			}
			DebugHandler activeDebugHandler = ScriptableRenderPass.GetActiveDebugHandler(cameraData);
			if (useRenderGraph)
			{
				if (activeDebugHandler != null)
				{
					passData.debugRendererLists = activeDebugHandler.CreateRendererListsWithDebugRenderState(renderGraph, ref renderingData.cullResults, ref drawSettings, ref filterSettings, ref this.m_RenderStateBlock);
					return;
				}
				RenderingUtils.CreateRendererListWithRenderStateBlock(renderGraph, ref renderingData.cullResults, drawSettings, filterSettings, this.m_RenderStateBlock, ref passData.rendererListHdl);
				return;
			}
			else
			{
				if (activeDebugHandler != null)
				{
					passData.debugRendererLists = activeDebugHandler.CreateRendererListsWithDebugRenderState(context, ref renderingData.cullResults, ref drawSettings, ref filterSettings, ref this.m_RenderStateBlock);
					return;
				}
				RenderingUtils.CreateRendererListWithRenderStateBlock(context, ref renderingData.cullResults, drawSettings, filterSettings, this.m_RenderStateBlock, ref passData.rendererList);
				return;
			}
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00041414 File Offset: 0x0003F614
		internal void Render(RenderGraph renderGraph, ContextContainer frameData, TextureHandle colorTarget, TextureHandle depthTarget, TextureHandle mainShadowsTexture, TextureHandle additionalShadowsTexture, uint batchLayerMask = 4294967295U)
		{
			UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
			UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			DrawObjectsPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<DrawObjectsPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/DrawObjectsPass.cs", 264))
			{
				builder.UseAllGlobalTextures(true);
				this.InitPassData(cameraData, ref passData, batchLayerMask, resourceData.isActiveTargetBackBuffer);
				if (colorTarget.IsValid())
				{
					passData.albedoHdl = colorTarget;
					builder.SetRenderAttachment(colorTarget, 0, AccessFlags.Write);
				}
				if (depthTarget.IsValid())
				{
					passData.depthHdl = depthTarget;
					builder.SetRenderAttachmentDepth(depthTarget, AccessFlags.Write);
				}
				if (mainShadowsTexture.IsValid())
				{
					builder.UseTexture(in mainShadowsTexture, AccessFlags.Read);
				}
				if (additionalShadowsTexture.IsValid())
				{
					builder.UseTexture(in additionalShadowsTexture, AccessFlags.Read);
				}
				TextureHandle ssaoTexture = resourceData.ssaoTexture;
				if (ssaoTexture.IsValid())
				{
					builder.UseTexture(in ssaoTexture, AccessFlags.Read);
				}
				RenderGraphUtils.UseDBufferIfValid(builder, resourceData);
				this.InitRendererLists(renderingData, cameraData, lightData, ref passData, default(ScriptableRenderContext), renderGraph, true);
				if (ScriptableRenderPass.GetActiveDebugHandler(cameraData) != null)
				{
					passData.debugRendererLists.PrepareRendererListForRasterPass(builder);
				}
				else
				{
					builder.UseRendererList(in passData.rendererListHdl);
					builder.UseRendererList(in passData.objectsWithErrorRendererListHdl);
				}
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				if (cameraData.xr.enabled)
				{
					bool passSupportsFoveation = cameraData.xrUniversal.canFoveateIntermediatePasses || resourceData.isActiveTargetBackBuffer;
					builder.EnableFoveatedRasterization(cameraData.xr.supportsFoveatedRendering && passSupportsFoveation);
				}
				builder.SetRenderFunc<DrawObjectsPass.PassData>(delegate(DrawObjectsPass.PassData data, RasterGraphContext context)
				{
					if (!data.isOpaque && !data.shouldTransparentsReceiveShadows)
					{
						TransparentSettingsPass.ExecutePass(context.cmd, data.shouldTransparentsReceiveShadows);
					}
					bool yFlip = data.cameraData.IsRenderTargetProjectionMatrixFlipped(data.albedoHdl, data.depthHdl);
					DrawObjectsPass.ExecutePass(context.cmd, data, data.rendererListHdl, data.objectsWithErrorRendererListHdl, yFlip);
				});
			}
		}

		// Token: 0x04000D63 RID: 3427
		private FilteringSettings m_FilteringSettings;

		// Token: 0x04000D64 RID: 3428
		private RenderStateBlock m_RenderStateBlock;

		// Token: 0x04000D65 RID: 3429
		private List<ShaderTagId> m_ShaderTagIdList = new List<ShaderTagId>();

		// Token: 0x04000D66 RID: 3430
		private bool m_IsOpaque;

		// Token: 0x04000D67 RID: 3431
		public bool m_IsActiveTargetBackBuffer;

		// Token: 0x04000D68 RID: 3432
		public bool m_ShouldTransparentsReceiveShadows;

		// Token: 0x04000D69 RID: 3433
		private DrawObjectsPass.PassData m_PassData;

		// Token: 0x04000D6A RID: 3434
		private static readonly int s_DrawObjectPassDataPropID = Shader.PropertyToID("_DrawObjectPassData");

		// Token: 0x02000214 RID: 532
		internal class PassData
		{
			// Token: 0x04000D6B RID: 3435
			internal TextureHandle albedoHdl;

			// Token: 0x04000D6C RID: 3436
			internal TextureHandle depthHdl;

			// Token: 0x04000D6D RID: 3437
			internal UniversalCameraData cameraData;

			// Token: 0x04000D6E RID: 3438
			internal bool isOpaque;

			// Token: 0x04000D6F RID: 3439
			internal bool shouldTransparentsReceiveShadows;

			// Token: 0x04000D70 RID: 3440
			internal uint batchLayerMask;

			// Token: 0x04000D71 RID: 3441
			internal bool isActiveTargetBackBuffer;

			// Token: 0x04000D72 RID: 3442
			internal RendererListHandle rendererListHdl;

			// Token: 0x04000D73 RID: 3443
			internal RendererListHandle objectsWithErrorRendererListHdl;

			// Token: 0x04000D74 RID: 3444
			internal DebugRendererLists debugRendererLists;

			// Token: 0x04000D75 RID: 3445
			internal RendererList rendererList;

			// Token: 0x04000D76 RID: 3446
			internal RendererList objectsWithErrorRendererList;
		}
	}
}
