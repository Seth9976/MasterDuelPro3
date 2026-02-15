using System;
using System.Collections.Generic;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200010A RID: 266
	internal class CapturePass : ScriptableRenderPass
	{
		// Token: 0x06000625 RID: 1573 RVA: 0x000176D9 File Offset: 0x000158D9
		public CapturePass(RenderPassEvent evt)
		{
			base.profilingSampler = new ProfilingSampler("Capture Camera output");
			base.renderPassEvent = evt;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000176F8 File Offset: 0x000158F8
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer cmdBuf = *renderingData.commandBuffer;
			this.m_CameraColorHandle = renderingData.cameraData.renderer->GetCameraColorBackBuffer(cmdBuf);
			using (new ProfilingScope(cmdBuf, base.profilingSampler))
			{
				RenderTargetIdentifier colorAttachmentIdentifier = this.m_CameraColorHandle.nameID;
				IEnumerator<Action<RenderTargetIdentifier, CommandBuffer>> captureActions = *renderingData.cameraData.captureActions;
				captureActions.Reset();
				while (captureActions.MoveNext())
				{
					Action<RenderTargetIdentifier, CommandBuffer> action = captureActions.Current;
					action(colorAttachmentIdentifier, *renderingData.commandBuffer);
				}
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00017790 File Offset: 0x00015990
		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
			UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			CapturePass.UnsafePassData passData;
			using (IUnsafeRenderGraphBuilder builder = renderGraph.AddUnsafePass<CapturePass.UnsafePassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/CapturePass.cs", 55))
			{
				passData.source = resourceData.cameraColor;
				passData.captureActions = cameraData.captureActions;
				builder.AllowPassCulling(false);
				IBaseRenderGraphBuilder baseRenderGraphBuilder = builder;
				TextureHandle cameraColor = resourceData.cameraColor;
				baseRenderGraphBuilder.UseTexture(in cameraColor, AccessFlags.Read);
				builder.SetRenderFunc<CapturePass.UnsafePassData>(delegate(CapturePass.UnsafePassData data, UnsafeGraphContext unsafeContext)
				{
					CommandBuffer nativeCommandBuffer = CommandBufferHelpers.GetNativeCommandBuffer(unsafeContext.cmd);
					IEnumerator<Action<RenderTargetIdentifier, CommandBuffer>> captureActions = data.captureActions;
					data.captureActions.Reset();
					while (data.captureActions.MoveNext())
					{
						captureActions.Current(data.source, nativeCommandBuffer);
					}
				});
			}
		}

		// Token: 0x0400059E RID: 1438
		private RTHandle m_CameraColorHandle;

		// Token: 0x0200010B RID: 267
		private class UnsafePassData
		{
			// Token: 0x0400059F RID: 1439
			internal TextureHandle source;

			// Token: 0x040005A0 RID: 1440
			public IEnumerator<Action<RenderTargetIdentifier, CommandBuffer>> captureActions;
		}
	}
}
