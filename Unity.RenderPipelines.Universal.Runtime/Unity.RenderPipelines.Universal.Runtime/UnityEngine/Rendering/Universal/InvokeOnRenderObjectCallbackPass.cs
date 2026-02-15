using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200011A RID: 282
	internal class InvokeOnRenderObjectCallbackPass : ScriptableRenderPass
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x0001896A File Offset: 0x00016B6A
		public InvokeOnRenderObjectCallbackPass(RenderPassEvent evt)
		{
			base.profilingSampler = new ProfilingSampler("Invoke OnRenderObject Callback");
			base.renderPassEvent = evt;
			base.useNativeRenderPass = false;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00018990 File Offset: 0x00016B90
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			renderingData.commandBuffer->InvokeOnRenderObjectCallbacks();
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000189A0 File Offset: 0x00016BA0
		internal void Render(RenderGraph renderGraph, TextureHandle colorTarget, TextureHandle depthTarget)
		{
			InvokeOnRenderObjectCallbackPass.PassData passData;
			using (IUnsafeRenderGraphBuilder builder = renderGraph.AddUnsafePass<InvokeOnRenderObjectCallbackPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/InvokeOnRenderObjectCallbackPass.cs", 36))
			{
				passData.colorTarget = colorTarget;
				builder.UseTexture(in colorTarget, AccessFlags.Write);
				passData.depthTarget = depthTarget;
				builder.UseTexture(in depthTarget, AccessFlags.Write);
				builder.AllowPassCulling(false);
				builder.SetRenderFunc<InvokeOnRenderObjectCallbackPass.PassData>(delegate(InvokeOnRenderObjectCallbackPass.PassData data, UnsafeGraphContext context)
				{
					context.cmd.InvokeOnRenderObjectCallbacks();
				});
			}
		}

		// Token: 0x0200011B RID: 283
		private class PassData
		{
			// Token: 0x040005D3 RID: 1491
			internal TextureHandle colorTarget;

			// Token: 0x040005D4 RID: 1492
			internal TextureHandle depthTarget;
		}
	}
}
