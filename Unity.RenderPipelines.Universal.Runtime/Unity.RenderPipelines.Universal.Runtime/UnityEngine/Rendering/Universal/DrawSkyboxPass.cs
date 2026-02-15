using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000111 RID: 273
	public class DrawSkyboxPass : ScriptableRenderPass
	{
		// Token: 0x06000640 RID: 1600 RVA: 0x00017E7C File Offset: 0x0001607C
		public DrawSkyboxPass(RenderPassEvent evt)
		{
			base.profilingSampler = ProfilingSampler.Get<URPProfileId>(URPProfileId.DrawSkybox);
			base.renderPassEvent = evt;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00017E98 File Offset: 0x00016098
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
			DebugHandler activeDebugHandler = ScriptableRenderPass.GetActiveDebugHandler(cameraData);
			if (activeDebugHandler != null && activeDebugHandler.IsScreenClearNeeded)
			{
				return;
			}
			RendererList skyRendererList = this.CreateSkyboxRendererList(context, cameraData);
			DrawSkyboxPass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer), cameraData.xr, skyRendererList);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00017EE8 File Offset: 0x000160E8
		private RendererList CreateSkyboxRendererList(ScriptableRenderContext context, UniversalCameraData cameraData)
		{
			RendererList skyRendererList = default(RendererList);
			if (cameraData.xr.enabled)
			{
				if (cameraData.xr.singlePassEnabled)
				{
					skyRendererList = context.CreateSkyboxRendererList(cameraData.camera, cameraData.GetProjectionMatrix(0), cameraData.GetViewMatrix(0), cameraData.GetProjectionMatrix(1), cameraData.GetViewMatrix(1));
				}
				else
				{
					skyRendererList = context.CreateSkyboxRendererList(cameraData.camera, cameraData.GetProjectionMatrix(0), cameraData.GetViewMatrix(0));
				}
			}
			else
			{
				skyRendererList = context.CreateSkyboxRendererList(cameraData.camera);
			}
			return skyRendererList;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00017F70 File Offset: 0x00016170
		private RendererListHandle CreateSkyBoxRendererList(RenderGraph renderGraph, UniversalCameraData cameraData)
		{
			RendererListHandle skyRendererListHandle = default(RendererListHandle);
			if (cameraData.xr.enabled)
			{
				if (cameraData.xr.singlePassEnabled)
				{
					skyRendererListHandle = renderGraph.CreateSkyboxRendererList(in cameraData.camera, cameraData.GetProjectionMatrix(0), cameraData.GetViewMatrix(0), cameraData.GetProjectionMatrix(1), cameraData.GetViewMatrix(1));
				}
				else
				{
					skyRendererListHandle = renderGraph.CreateSkyboxRendererList(in cameraData.camera, cameraData.GetProjectionMatrix(0), cameraData.GetViewMatrix(0));
				}
			}
			else
			{
				skyRendererListHandle = renderGraph.CreateSkyboxRendererList(in cameraData.camera);
			}
			return skyRendererListHandle;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00017FF8 File Offset: 0x000161F8
		private static void ExecutePass(RasterCommandBuffer cmd, XRPass xr, RendererList rendererList)
		{
			if (xr.enabled && xr.singlePassEnabled)
			{
				cmd.SetSinglePassStereo(SystemInfo.supportsMultiview ? SinglePassStereoMode.Multiview : SinglePassStereoMode.Instancing);
			}
			cmd.DrawRendererList(rendererList);
			if (xr.enabled && xr.singlePassEnabled)
			{
				cmd.SetSinglePassStereo(SinglePassStereoMode.None);
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00018044 File Offset: 0x00016244
		private void InitPassData(ref DrawSkyboxPass.PassData passData, in XRPass xr, in RendererListHandle handle)
		{
			passData.xr = xr;
			passData.skyRendererListHandle = handle;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001805C File Offset: 0x0001625C
		internal void Render(RenderGraph renderGraph, ContextContainer frameData, ScriptableRenderContext context, TextureHandle colorTarget, TextureHandle depthTarget, Material skyboxMaterial)
		{
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
			DebugHandler activeDebugHandler = ScriptableRenderPass.GetActiveDebugHandler(cameraData);
			if (activeDebugHandler != null && activeDebugHandler.IsScreenClearNeeded)
			{
				return;
			}
			DrawSkyboxPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<DrawSkyboxPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/DrawSkyboxPass.cs", 148))
			{
				RendererListHandle skyRendererListHandle = this.CreateSkyBoxRendererList(renderGraph, cameraData);
				XRPass xr = cameraData.xr;
				this.InitPassData(ref passData, in xr, in skyRendererListHandle);
				passData.material = skyboxMaterial;
				builder.UseRendererList(in skyRendererListHandle);
				builder.SetRenderAttachment(colorTarget, 0, AccessFlags.Write);
				builder.SetRenderAttachmentDepth(depthTarget, AccessFlags.Write);
				builder.AllowPassCulling(false);
				if (cameraData.xr.enabled)
				{
					bool passSupportsFoveation = cameraData.xrUniversal.canFoveateIntermediatePasses || resourceData.isActiveTargetBackBuffer;
					builder.EnableFoveatedRasterization(cameraData.xr.supportsFoveatedRendering && passSupportsFoveation);
				}
				builder.SetRenderFunc<DrawSkyboxPass.PassData>(delegate(DrawSkyboxPass.PassData data, RasterGraphContext context)
				{
					DrawSkyboxPass.ExecutePass(context.cmd, data.xr, data.skyRendererListHandle);
				});
			}
		}

		// Token: 0x02000112 RID: 274
		private class PassData
		{
			// Token: 0x040005B1 RID: 1457
			internal XRPass xr;

			// Token: 0x040005B2 RID: 1458
			internal RendererListHandle skyRendererListHandle;

			// Token: 0x040005B3 RID: 1459
			internal Material material;
		}
	}
}
