using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// Token: 0x02000029 RID: 41
public class GrabTransparentFeature : ScriptableRendererFeature
{
	// Token: 0x060000B2 RID: 178 RVA: 0x0000216D File Offset: 0x0000036D
	public override void Create()
	{
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x0000216D File Offset: 0x0000036D
	public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
	{
	}

	// Token: 0x04000107 RID: 263
	private GrabTransparentFeature.CustomRenderPass m_ScriptablePass;

	// Token: 0x0200002A RID: 42
	private class CustomRenderPass : ScriptableRenderPass
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRenderTarget(RenderTargetIdentifier target)
		{
		}

		// Token: 0x04000108 RID: 264
		private int m_cameraTransparentTextureID;

		// Token: 0x04000109 RID: 265
		private int m_tmpTextureID;

		// Token: 0x0400010A RID: 266
		private string grabTransparentTag;

		// Token: 0x0400010B RID: 267
		private string commandBufferName;

		// Token: 0x0400010C RID: 268
		private RenderTargetIdentifier m_renderTarget;
	}
}
