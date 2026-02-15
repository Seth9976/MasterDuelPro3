using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x0200020D RID: 525
	public class DepthNormalOnlyPass : ScriptableRenderPass
	{
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0004066F File Offset: 0x0003E86F
		// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x00040677 File Offset: 0x0003E877
		internal List<ShaderTagId> shaderTagIds { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x00040680 File Offset: 0x0003E880
		// (set) Token: 0x06000BC8 RID: 3016 RVA: 0x00040688 File Offset: 0x0003E888
		private RTHandle depthHandle { get; set; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00040691 File Offset: 0x0003E891
		// (set) Token: 0x06000BCA RID: 3018 RVA: 0x00040699 File Offset: 0x0003E899
		private RTHandle normalHandle { get; set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x000406A2 File Offset: 0x0003E8A2
		// (set) Token: 0x06000BCC RID: 3020 RVA: 0x000406AA File Offset: 0x0003E8AA
		private RTHandle renderingLayersHandle { get; set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x000406B3 File Offset: 0x0003E8B3
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x000406BB File Offset: 0x0003E8BB
		internal bool enableRenderingLayers { get; set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x000406C4 File Offset: 0x0003E8C4
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x000406CC File Offset: 0x0003E8CC
		internal RenderingLayerUtils.MaskSize renderingLayersMaskSize { get; set; }

		// Token: 0x06000BD1 RID: 3025 RVA: 0x000406D8 File Offset: 0x0003E8D8
		public DepthNormalOnlyPass(RenderPassEvent evt, RenderQueueRange renderQueueRange, LayerMask layerMask)
		{
			base.profilingSampler = ProfilingSampler.Get<URPProfileId>(URPProfileId.DrawDepthNormalPrepass);
			this.m_PassData = new DepthNormalOnlyPass.PassData();
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(renderQueueRange), layerMask, uint.MaxValue, 0);
			base.renderPassEvent = evt;
			base.useNativeRenderPass = false;
			this.shaderTagIds = DepthNormalOnlyPass.k_DepthNormals;
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00040734 File Offset: 0x0003E934
		public static GraphicsFormat GetGraphicsFormat()
		{
			if (SystemInfo.IsFormatSupported(GraphicsFormat.R8G8B8A8_SNorm, GraphicsFormatUsage.Render))
			{
				return GraphicsFormat.R8G8B8A8_SNorm;
			}
			if (SystemInfo.IsFormatSupported(GraphicsFormat.R16G16B16A16_SFloat, GraphicsFormatUsage.Render))
			{
				return GraphicsFormat.R16G16B16A16_SFloat;
			}
			return GraphicsFormat.R32G32B32A32_SFloat;
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00040754 File Offset: 0x0003E954
		public void Setup(RTHandle depthHandle, RTHandle normalHandle)
		{
			this.depthHandle = depthHandle;
			this.normalHandle = normalHandle;
			this.enableRenderingLayers = false;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0004076B File Offset: 0x0003E96B
		public void Setup(RTHandle depthHandle, RTHandle normalHandle, RTHandle decalLayerHandle)
		{
			this.Setup(depthHandle, normalHandle);
			this.renderingLayersHandle = decalLayerHandle;
			this.enableRenderingLayers = true;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00040784 File Offset: 0x0003E984
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			RTHandle[] colorHandles;
			if (this.enableRenderingLayers)
			{
				DepthNormalOnlyPass.k_ColorAttachment2[0] = this.normalHandle;
				DepthNormalOnlyPass.k_ColorAttachment2[1] = this.renderingLayersHandle;
				colorHandles = DepthNormalOnlyPass.k_ColorAttachment2;
			}
			else
			{
				DepthNormalOnlyPass.k_ColorAttachment1[0] = this.normalHandle;
				colorHandles = DepthNormalOnlyPass.k_ColorAttachment1;
			}
			if (renderingData.cameraData.renderer->useDepthPriming && (*renderingData.cameraData.renderType == CameraRenderType.Base || *renderingData.cameraData.clearDepth))
			{
				base.ConfigureTarget(colorHandles, renderingData.cameraData.renderer->cameraDepthTargetHandle);
			}
			else
			{
				base.ConfigureTarget(colorHandles, this.depthHandle);
			}
			base.ConfigureClear(ClearFlag.All, Color.black);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00040830 File Offset: 0x0003EA30
		private static void ExecutePass(RasterCommandBuffer cmd, DepthNormalOnlyPass.PassData passData, RendererList rendererList)
		{
			if (passData.enableRenderingLayers)
			{
				cmd.SetKeyword(in ShaderGlobalKeywords.WriteRenderingLayers, true);
			}
			cmd.DrawRendererList(rendererList);
			if (passData.enableRenderingLayers)
			{
				cmd.SetKeyword(in ShaderGlobalKeywords.WriteRenderingLayers, false);
			}
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00040864 File Offset: 0x0003EA64
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			ContextContainer frameData = renderingData.frameData;
			UniversalRenderingData universalRenderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			this.m_PassData.enableRenderingLayers = this.enableRenderingLayers;
			RendererListParams param = this.InitRendererListParams(universalRenderingData, cameraData, lightData);
			RendererList rendererList = context.CreateRendererList(ref param);
			RasterCommandBuffer cmd = CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer);
			using (new ProfilingScope(cmd, base.profilingSampler))
			{
				DepthNormalOnlyPass.ExecutePass(cmd, this.m_PassData, rendererList);
			}
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00040900 File Offset: 0x0003EB00
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			this.normalHandle = null;
			this.depthHandle = null;
			this.renderingLayersHandle = null;
			this.shaderTagIds = DepthNormalOnlyPass.k_DepthNormals;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00040930 File Offset: 0x0003EB30
		private RendererListParams InitRendererListParams(UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData)
		{
			SortingCriteria sortFlags = cameraData.defaultOpaqueSortFlags;
			DrawingSettings drawSettings = RenderingUtils.CreateDrawingSettings(this.shaderTagIds, renderingData, cameraData, lightData, sortFlags);
			drawSettings.perObjectData = PerObjectData.None;
			return new RendererListParams(renderingData.cullResults, drawSettings, this.m_FilteringSettings);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00040970 File Offset: 0x0003EB70
		internal void Render(RenderGraph renderGraph, ContextContainer frameData, TextureHandle cameraNormalsTexture, TextureHandle cameraDepthTexture, TextureHandle renderingLayersTexture, uint batchLayerMask, bool setGlobalDepth, bool setGlobalTextures)
		{
			UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			DepthNormalOnlyPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<DepthNormalOnlyPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/DepthNormalOnlyPass.cs", 195))
			{
				passData.cameraNormalsTexture = cameraNormalsTexture;
				builder.SetRenderAttachment(cameraNormalsTexture, 0, AccessFlags.Write);
				passData.cameraDepthTexture = cameraDepthTexture;
				builder.SetRenderAttachmentDepth(cameraDepthTexture, AccessFlags.Write);
				passData.enableRenderingLayers = this.enableRenderingLayers;
				if (passData.enableRenderingLayers)
				{
					builder.SetRenderAttachment(renderingLayersTexture, 1, AccessFlags.Write);
					passData.maskSize = this.renderingLayersMaskSize;
				}
				RendererListParams param = this.InitRendererListParams(renderingData, cameraData, lightData);
				param.filteringSettings.batchLayerMask = batchLayerMask;
				passData.rendererList = renderGraph.CreateRendererList(in param);
				builder.UseRendererList(in passData.rendererList);
				if (cameraData.xr.enabled)
				{
					builder.EnableFoveatedRasterization(cameraData.xr.supportsFoveatedRendering && cameraData.xrUniversal.canFoveateIntermediatePasses);
				}
				if (setGlobalTextures)
				{
					builder.SetGlobalTextureAfterPass(in cameraNormalsTexture, DepthNormalOnlyPass.s_CameraNormalsTextureID);
					if (passData.enableRenderingLayers)
					{
						builder.SetGlobalTextureAfterPass(in renderingLayersTexture, DepthNormalOnlyPass.s_CameraRenderingLayersTextureID);
					}
				}
				if (setGlobalDepth)
				{
					builder.SetGlobalTextureAfterPass(in cameraDepthTexture, DepthNormalOnlyPass.s_CameraDepthTextureID);
				}
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				builder.SetRenderFunc<DepthNormalOnlyPass.PassData>(delegate(DepthNormalOnlyPass.PassData data, RasterGraphContext context)
				{
					RenderingLayerUtils.SetupProperties(context.cmd, data.maskSize);
					DepthNormalOnlyPass.ExecutePass(context.cmd, data, data.rendererList);
				});
			}
		}

		// Token: 0x04000D4A RID: 3402
		private FilteringSettings m_FilteringSettings;

		// Token: 0x04000D4B RID: 3403
		private DepthNormalOnlyPass.PassData m_PassData;

		// Token: 0x04000D4C RID: 3404
		private static readonly List<ShaderTagId> k_DepthNormals = new List<ShaderTagId>
		{
			new ShaderTagId("DepthNormals"),
			new ShaderTagId("DepthNormalsOnly")
		};

		// Token: 0x04000D4D RID: 3405
		private static readonly RTHandle[] k_ColorAttachment1 = new RTHandle[1];

		// Token: 0x04000D4E RID: 3406
		private static readonly RTHandle[] k_ColorAttachment2 = new RTHandle[2];

		// Token: 0x04000D4F RID: 3407
		private static readonly int s_CameraDepthTextureID = Shader.PropertyToID("_CameraDepthTexture");

		// Token: 0x04000D50 RID: 3408
		private static readonly int s_CameraNormalsTextureID = Shader.PropertyToID("_CameraNormalsTexture");

		// Token: 0x04000D51 RID: 3409
		private static readonly int s_CameraRenderingLayersTextureID = Shader.PropertyToID("_CameraRenderingLayersTexture");

		// Token: 0x0200020E RID: 526
		private class PassData
		{
			// Token: 0x04000D52 RID: 3410
			internal TextureHandle cameraDepthTexture;

			// Token: 0x04000D53 RID: 3411
			internal TextureHandle cameraNormalsTexture;

			// Token: 0x04000D54 RID: 3412
			internal bool enableRenderingLayers;

			// Token: 0x04000D55 RID: 3413
			internal RenderingLayerUtils.MaskSize maskSize;

			// Token: 0x04000D56 RID: 3414
			internal RendererListHandle rendererList;
		}
	}
}
