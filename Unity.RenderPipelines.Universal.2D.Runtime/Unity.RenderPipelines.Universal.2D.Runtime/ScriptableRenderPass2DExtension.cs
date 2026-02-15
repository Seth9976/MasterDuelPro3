using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000054 RID: 84
	internal static class ScriptableRenderPass2DExtension
	{
		// Token: 0x06000259 RID: 601 RVA: 0x000131E8 File Offset: 0x000113E8
		internal static void GetInjectionPoint2D(this ScriptableRenderPass renderPass, out RenderPassEvent2D rpEvent, out int rpLayer)
		{
			rpLayer = int.MinValue;
			if (renderPass.renderPassEvent <= RenderPassEvent.BeforeRenderingTransparents)
			{
				rpEvent = RenderPassEvent2D.BeforeRendering;
				return;
			}
			if (renderPass.renderPassEvent <= RenderPassEvent.AfterRenderingTransparents)
			{
				rpEvent = RenderPassEvent2D.BeforeRenderingPostProcessing;
				return;
			}
			if (renderPass.renderPassEvent <= RenderPassEvent.AfterRenderingPostProcessing)
			{
				rpEvent = RenderPassEvent2D.AfterRenderingPostProcessing;
				return;
			}
			rpEvent = RenderPassEvent2D.AfterRendering;
		}
	}
}
