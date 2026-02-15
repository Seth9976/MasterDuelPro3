using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200003C RID: 60
	internal class PixelPerfectBackgroundPass : ScriptableRenderPass
	{
		// Token: 0x06000172 RID: 370 RVA: 0x0000D282 File Offset: 0x0000B482
		public PixelPerfectBackgroundPass(RenderPassEvent evt)
		{
			base.renderPassEvent = evt;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000D294 File Offset: 0x0000B494
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer cmd = *renderingData.commandBuffer;
			using (new ProfilingScope(cmd, PixelPerfectBackgroundPass.m_ProfilingScope))
			{
				CoreUtils.SetRenderTarget(cmd, BuiltinRenderTextureType.CameraTarget, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, ClearFlag.Color, Color.black);
			}
		}

		// Token: 0x04000127 RID: 295
		private static readonly ProfilingSampler m_ProfilingScope = new ProfilingSampler("Pixel Perfect Background Pass");
	}
}
