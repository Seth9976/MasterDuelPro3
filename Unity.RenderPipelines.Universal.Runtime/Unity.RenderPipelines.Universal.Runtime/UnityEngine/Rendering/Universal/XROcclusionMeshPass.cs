using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200014D RID: 333
	public class XROcclusionMeshPass : ScriptableRenderPass
	{
		// Token: 0x06000733 RID: 1843 RVA: 0x0002329E File Offset: 0x0002149E
		public XROcclusionMeshPass(RenderPassEvent evt)
		{
			base.profilingSampler = new ProfilingSampler("Draw XR Occlusion Mesh");
			base.renderPassEvent = evt;
			this.m_PassData = new XROcclusionMeshPass.PassData();
			this.m_IsActiveTargetBackBuffer = false;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x000232CF File Offset: 0x000214CF
		private static void ExecutePass(RasterCommandBuffer cmd, XROcclusionMeshPass.PassData data)
		{
			if (data.xr.hasValidOcclusionMesh)
			{
				if (data.isActiveTargetBackBuffer)
				{
					cmd.SetViewport(data.xr.GetViewport(0));
				}
				data.xr.RenderOcclusionMesh(cmd, !data.isActiveTargetBackBuffer);
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0002330D File Offset: 0x0002150D
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			this.m_PassData.xr = renderingData.cameraData.xr;
			this.m_PassData.isActiveTargetBackBuffer = this.m_IsActiveTargetBackBuffer;
			XROcclusionMeshPass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer), this.m_PassData);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00023350 File Offset: 0x00021550
		internal void Render(RenderGraph renderGraph, ContextContainer frameData, in TextureHandle cameraColorAttachment, in TextureHandle cameraDepthAttachment)
		{
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
			XROcclusionMeshPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<XROcclusionMeshPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/XROcclusionMeshPass.cs", 61))
			{
				passData.xr = cameraData.xr;
				passData.cameraColorAttachment = cameraColorAttachment;
				builder.SetRenderAttachment(cameraColorAttachment, 0, AccessFlags.Write);
				passData.cameraDepthAttachment = cameraDepthAttachment;
				builder.SetRenderAttachmentDepth(cameraDepthAttachment, AccessFlags.Write);
				passData.isActiveTargetBackBuffer = resourceData.isActiveTargetBackBuffer;
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				if (cameraData.xr.enabled)
				{
					bool passSupportsFoveation = cameraData.xrUniversal.canFoveateIntermediatePasses || resourceData.isActiveTargetBackBuffer;
					builder.EnableFoveatedRasterization(cameraData.xr.supportsFoveatedRendering && passSupportsFoveation);
				}
				builder.SetRenderFunc<XROcclusionMeshPass.PassData>(delegate(XROcclusionMeshPass.PassData data, RasterGraphContext context)
				{
					XROcclusionMeshPass.ExecutePass(context.cmd, data);
				});
			}
		}

		// Token: 0x040007BD RID: 1981
		private XROcclusionMeshPass.PassData m_PassData;

		// Token: 0x040007BE RID: 1982
		public bool m_IsActiveTargetBackBuffer;

		// Token: 0x0200014E RID: 334
		private class PassData
		{
			// Token: 0x040007BF RID: 1983
			internal XRPass xr;

			// Token: 0x040007C0 RID: 1984
			internal TextureHandle cameraColorAttachment;

			// Token: 0x040007C1 RID: 1985
			internal TextureHandle cameraDepthAttachment;

			// Token: 0x040007C2 RID: 1986
			internal bool isActiveTargetBackBuffer;
		}
	}
}
