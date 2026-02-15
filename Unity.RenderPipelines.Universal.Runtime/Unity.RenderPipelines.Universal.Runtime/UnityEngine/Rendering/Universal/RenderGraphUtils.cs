using System;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001E4 RID: 484
	internal static class RenderGraphUtils
	{
		// Token: 0x06000AC8 RID: 2760 RVA: 0x00038FA8 File Offset: 0x000371A8
		internal static void UseDBufferIfValid(IRasterRenderGraphBuilder builder, UniversalResourceData resourceData)
		{
			TextureHandle[] dbufferHandles = resourceData.dBuffer;
			for (int i = 0; i < 3; i++)
			{
				TextureHandle dbuffer = dbufferHandles[i];
				if (dbuffer.IsValid())
				{
					builder.UseTexture(in dbuffer, AccessFlags.Read);
				}
			}
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00038FE4 File Offset: 0x000371E4
		public static void SetGlobalTexture(RenderGraph graph, int nameId, TextureHandle handle, string passName = "Set Global Texture", [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			RenderGraphUtils.PassData passData;
			using (IRasterRenderGraphBuilder builder = graph.AddRasterRenderPass<RenderGraphUtils.PassData>(passName, out passData, RenderGraphUtils.s_SetGlobalTextureProfilingSampler, file, line))
			{
				passData.nameID = nameId;
				passData.texture = handle;
				builder.UseTexture(in handle, AccessFlags.Read);
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				builder.SetGlobalTextureAfterPass(in handle, nameId);
				builder.SetRenderFunc<RenderGraphUtils.PassData>(delegate(RenderGraphUtils.PassData data, RasterGraphContext context)
				{
				});
			}
		}

		// Token: 0x04000BF9 RID: 3065
		private static ProfilingSampler s_SetGlobalTextureProfilingSampler = new ProfilingSampler("Set Global Texture");

		// Token: 0x04000BFA RID: 3066
		internal const int GBufferSize = 7;

		// Token: 0x04000BFB RID: 3067
		internal const int DBufferSize = 3;

		// Token: 0x04000BFC RID: 3068
		internal const int LightTextureSize = 4;

		// Token: 0x020001E5 RID: 485
		private class PassData
		{
			// Token: 0x04000BFD RID: 3069
			internal TextureHandle texture;

			// Token: 0x04000BFE RID: 3070
			internal int nameID;
		}
	}
}
