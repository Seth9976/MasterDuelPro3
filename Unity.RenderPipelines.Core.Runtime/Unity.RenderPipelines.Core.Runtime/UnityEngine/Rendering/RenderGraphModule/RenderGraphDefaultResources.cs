using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000249 RID: 585
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public class RenderGraphDefaultResources
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x00039F9E File Offset: 0x0003819E
		// (set) Token: 0x06000FD3 RID: 4051 RVA: 0x00039FA6 File Offset: 0x000381A6
		public TextureHandle blackTexture { get; private set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00039FAF File Offset: 0x000381AF
		// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x00039FB7 File Offset: 0x000381B7
		public TextureHandle whiteTexture { get; private set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00039FC0 File Offset: 0x000381C0
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x00039FC8 File Offset: 0x000381C8
		public TextureHandle clearTextureXR { get; private set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00039FD1 File Offset: 0x000381D1
		// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x00039FD9 File Offset: 0x000381D9
		public TextureHandle magentaTextureXR { get; private set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00039FE2 File Offset: 0x000381E2
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x00039FEA File Offset: 0x000381EA
		public TextureHandle blackTextureXR { get; private set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00039FF3 File Offset: 0x000381F3
		// (set) Token: 0x06000FDD RID: 4061 RVA: 0x00039FFB File Offset: 0x000381FB
		public TextureHandle blackTextureArrayXR { get; private set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0003A004 File Offset: 0x00038204
		// (set) Token: 0x06000FDF RID: 4063 RVA: 0x0003A00C File Offset: 0x0003820C
		public TextureHandle blackUIntTextureXR { get; private set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0003A015 File Offset: 0x00038215
		// (set) Token: 0x06000FE1 RID: 4065 RVA: 0x0003A01D File Offset: 0x0003821D
		public TextureHandle blackTexture3DXR { get; private set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x0003A026 File Offset: 0x00038226
		// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x0003A02E File Offset: 0x0003822E
		public TextureHandle whiteTextureXR { get; private set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x0003A037 File Offset: 0x00038237
		// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x0003A03F File Offset: 0x0003823F
		public TextureHandle defaultShadowTexture { get; private set; }

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0003A048 File Offset: 0x00038248
		internal RenderGraphDefaultResources()
		{
			this.m_BlackTexture2D = RTHandles.Alloc(Texture2D.blackTexture);
			this.m_WhiteTexture2D = RTHandles.Alloc(Texture2D.whiteTexture);
			this.m_ShadowTexture2D = RTHandles.Alloc(1, 1, GraphicsFormat.D32_SFloat, 1, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex2D, false, false, true, true, 1, 0f, MSAASamples.None, false, false, false, RenderTextureMemoryless.None, VRTextureUsage.None, "DefaultShadowTexture");
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0003A0A3 File Offset: 0x000382A3
		internal void Cleanup()
		{
			this.m_BlackTexture2D.Release();
			this.m_WhiteTexture2D.Release();
			this.m_ShadowTexture2D.Release();
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0003A0C8 File Offset: 0x000382C8
		internal void InitializeForRendering(RenderGraph renderGraph)
		{
			this.blackTexture = renderGraph.ImportTexture(this.m_BlackTexture2D, true);
			this.whiteTexture = renderGraph.ImportTexture(this.m_WhiteTexture2D, true);
			this.defaultShadowTexture = renderGraph.ImportTexture(this.m_ShadowTexture2D, true);
			this.clearTextureXR = renderGraph.ImportTexture(TextureXR.GetClearTexture(), true);
			this.magentaTextureXR = renderGraph.ImportTexture(TextureXR.GetMagentaTexture(), true);
			this.blackTextureXR = renderGraph.ImportTexture(TextureXR.GetBlackTexture(), true);
			this.blackTextureArrayXR = renderGraph.ImportTexture(TextureXR.GetBlackTextureArray(), true);
			this.blackUIntTextureXR = renderGraph.ImportTexture(TextureXR.GetBlackUIntTexture(), true);
			this.blackTexture3DXR = renderGraph.ImportTexture(TextureXR.GetBlackTexture3D(), true);
			this.whiteTextureXR = renderGraph.ImportTexture(TextureXR.GetWhiteTexture(), true);
		}

		// Token: 0x04000A33 RID: 2611
		private RTHandle m_BlackTexture2D;

		// Token: 0x04000A34 RID: 2612
		private RTHandle m_WhiteTexture2D;

		// Token: 0x04000A35 RID: 2613
		private RTHandle m_ShadowTexture2D;
	}
}
