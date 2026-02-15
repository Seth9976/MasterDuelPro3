using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000055 RID: 85
	internal class CopyCameraSortingLayerPass : ScriptableRenderPass
	{
		// Token: 0x0600025A RID: 602 RVA: 0x0001323E File Offset: 0x0001143E
		public CopyCameraSortingLayerPass(Material blitMaterial)
		{
			CopyCameraSortingLayerPass.m_BlitMaterial = blitMaterial;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0001324C File Offset: 0x0001144C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00013254 File Offset: 0x00011454
		public static void ConfigureDescriptor(Downsampling downsamplingMethod, ref RenderTextureDescriptor descriptor, out FilterMode filterMode)
		{
			descriptor.msaaSamples = 1;
			descriptor.depthStencilFormat = GraphicsFormat.None;
			if (downsamplingMethod == Downsampling._2xBilinear)
			{
				descriptor.width /= 2;
				descriptor.height /= 2;
			}
			else if (downsamplingMethod == Downsampling._4xBox || downsamplingMethod == Downsampling._4xBilinear)
			{
				descriptor.width /= 4;
				descriptor.height /= 4;
			}
			filterMode = ((downsamplingMethod == Downsampling.None || downsamplingMethod == Downsampling._4xBox) ? FilterMode.Point : FilterMode.Bilinear);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000132C4 File Offset: 0x000114C4
		private static void Execute(RasterCommandBuffer cmd, RTHandle source)
		{
			using (new ProfilingScope(cmd, CopyCameraSortingLayerPass.m_ExecuteProfilingSampler))
			{
				Vector2 viewportScale = (source.useScaling ? new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : Vector2.one);
				Blitter.BlitTexture(cmd, source, viewportScale, CopyCameraSortingLayerPass.m_BlitMaterial, (source.rt.filterMode == FilterMode.Bilinear) ? 1 : 0);
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00013358 File Offset: 0x00011558
		public void Render(RenderGraph graph, in TextureHandle cameraColorAttachment, in TextureHandle destination)
		{
			CopyCameraSortingLayerPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = graph.AddRasterRenderPass<CopyCameraSortingLayerPass.PassData>(CopyCameraSortingLayerPass.k_CopyCameraSortingLayerPass, out passData, CopyCameraSortingLayerPass.m_ProfilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/2D/Rendergraph/CopyCameraSortingLayerPass.cs", 62))
			{
				passData.source = cameraColorAttachment;
				builder.SetRenderAttachment(destination, 0, AccessFlags.Write);
				builder.UseTexture(in passData.source, AccessFlags.Read);
				builder.AllowPassCulling(false);
				builder.SetGlobalTextureAfterPass(in destination, CopyCameraSortingLayerPass.k_CameraSortingLayerTextureId);
				builder.SetRenderFunc<CopyCameraSortingLayerPass.PassData>(delegate(CopyCameraSortingLayerPass.PassData data, RasterGraphContext context)
				{
					CopyCameraSortingLayerPass.Execute(context.cmd, data.source);
				});
			}
		}

		// Token: 0x04000207 RID: 519
		private static readonly string k_CopyCameraSortingLayerPass = "CopyCameraSortingLayer Pass";

		// Token: 0x04000208 RID: 520
		private static readonly ProfilingSampler m_ProfilingSampler = new ProfilingSampler(CopyCameraSortingLayerPass.k_CopyCameraSortingLayerPass);

		// Token: 0x04000209 RID: 521
		private static readonly ProfilingSampler m_ExecuteProfilingSampler = new ProfilingSampler("Copy");

		// Token: 0x0400020A RID: 522
		internal static readonly string k_CameraSortingLayerTexture = "_CameraSortingLayerTexture";

		// Token: 0x0400020B RID: 523
		private static readonly int k_CameraSortingLayerTextureId = Shader.PropertyToID(CopyCameraSortingLayerPass.k_CameraSortingLayerTexture);

		// Token: 0x0400020C RID: 524
		private static Material m_BlitMaterial;

		// Token: 0x02000056 RID: 86
		private class PassData
		{
			// Token: 0x0400020D RID: 525
			internal TextureHandle source;
		}
	}
}
