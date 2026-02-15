using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000210 RID: 528
	public class DepthOnlyPass : ScriptableRenderPass
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00040BA8 File Offset: 0x0003EDA8
		// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x00040BB0 File Offset: 0x0003EDB0
		private RTHandle destination { get; set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x00040BB9 File Offset: 0x0003EDB9
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x00040BC1 File Offset: 0x0003EDC1
		internal ShaderTagId shaderTagId { get; set; } = DepthOnlyPass.k_ShaderTagId;

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00040BCC File Offset: 0x0003EDCC
		public DepthOnlyPass(RenderPassEvent evt, RenderQueueRange renderQueueRange, LayerMask layerMask)
		{
			base.profilingSampler = new ProfilingSampler("Draw Depth Only");
			this.m_PassData = new DepthOnlyPass.PassData();
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(renderQueueRange), layerMask, uint.MaxValue, 0);
			base.renderPassEvent = evt;
			base.useNativeRenderPass = false;
			this.shaderTagId = DepthOnlyPass.k_ShaderTagId;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00040C37 File Offset: 0x0003EE37
		public void Setup(RenderTextureDescriptor baseDescriptor, RTHandle depthAttachmentHandle)
		{
			this.destination = depthAttachmentHandle;
			this.depthStencilFormat = baseDescriptor.depthStencilFormat;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00040C50 File Offset: 0x0003EE50
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			ref RenderTextureDescriptor cameraTargetDescriptor = ref renderingData.cameraData.cameraTargetDescriptor;
			if (renderingData.cameraData.renderer->useDepthPriming && (*renderingData.cameraData.renderType == CameraRenderType.Base || *renderingData.cameraData.clearDepth))
			{
				base.ConfigureTarget(renderingData.cameraData.renderer->cameraDepthTargetHandle);
				base.ConfigureClear(ClearFlag.Depth, Color.black);
				return;
			}
			base.useNativeRenderPass = true;
			base.ConfigureTarget(this.destination);
			base.ConfigureClear(ClearFlag.All, Color.black);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00040CDC File Offset: 0x0003EEDC
		private static void ExecutePass(RasterCommandBuffer cmd, RendererList rendererList)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.DepthPrepass)))
			{
				cmd.DrawRendererList(rendererList);
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00040D20 File Offset: 0x0003EF20
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			ContextContainer frameData = renderingData.frameData;
			UniversalRenderingData universalRenderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			RendererListParams param = this.InitRendererListParams(universalRenderingData, cameraData, lightData);
			RendererList rendererList = context.CreateRendererList(ref param);
			DepthOnlyPass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer), rendererList);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00040D70 File Offset: 0x0003EF70
		private RendererListParams InitRendererListParams(UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData)
		{
			SortingCriteria sortFlags = cameraData.defaultOpaqueSortFlags;
			DrawingSettings drawSettings = RenderingUtils.CreateDrawingSettings(this.shaderTagId, renderingData, cameraData, lightData, sortFlags);
			drawSettings.perObjectData = PerObjectData.None;
			return new RendererListParams(renderingData.cullResults, drawSettings, this.m_FilteringSettings);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00040DB0 File Offset: 0x0003EFB0
		internal void Render(RenderGraph renderGraph, ContextContainer frameData, ref TextureHandle cameraDepthTexture, uint batchLayerMask, bool setGlobalDepth)
		{
			UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			DepthOnlyPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<DepthOnlyPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/DepthOnlyPass.cs", 130))
			{
				RendererListParams param = this.InitRendererListParams(renderingData, cameraData, lightData);
				param.filteringSettings.batchLayerMask = batchLayerMask;
				passData.rendererList = renderGraph.CreateRendererList(in param);
				builder.UseRendererList(in passData.rendererList);
				builder.SetRenderAttachmentDepth(cameraDepthTexture, AccessFlags.Write);
				if (setGlobalDepth)
				{
					builder.SetGlobalTextureAfterPass(in cameraDepthTexture, DepthOnlyPass.s_CameraDepthTextureID);
				}
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				if (cameraData.xr.enabled)
				{
					builder.EnableFoveatedRasterization(cameraData.xr.supportsFoveatedRendering && cameraData.xrUniversal.canFoveateIntermediatePasses);
				}
				builder.SetRenderFunc<DepthOnlyPass.PassData>(delegate(DepthOnlyPass.PassData data, RasterGraphContext context)
				{
					DepthOnlyPass.ExecutePass(context.cmd, data.rendererList);
				});
			}
		}

		// Token: 0x04000D5A RID: 3418
		private GraphicsFormat depthStencilFormat;

		// Token: 0x04000D5C RID: 3420
		private DepthOnlyPass.PassData m_PassData;

		// Token: 0x04000D5D RID: 3421
		private FilteringSettings m_FilteringSettings;

		// Token: 0x04000D5E RID: 3422
		private static readonly ShaderTagId k_ShaderTagId = new ShaderTagId("DepthOnly");

		// Token: 0x04000D5F RID: 3423
		private static readonly int s_CameraDepthTextureID = Shader.PropertyToID("_CameraDepthTexture");

		// Token: 0x02000211 RID: 529
		private class PassData
		{
			// Token: 0x04000D60 RID: 3424
			internal RendererListHandle rendererList;
		}
	}
}
