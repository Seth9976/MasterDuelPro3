using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000067 RID: 103
	internal class UpscalePass : ScriptableRenderPass
	{
		// Token: 0x06000294 RID: 660 RVA: 0x00014C04 File Offset: 0x00012E04
		public UpscalePass(RenderPassEvent evt, Material blitMaterial)
		{
			base.renderPassEvent = evt;
			UpscalePass.m_BlitMaterial = blitMaterial;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00014C1C File Offset: 0x00012E1C
		public void Setup(RTHandle colorTargetHandle, int width, int height, FilterMode mode, RenderTextureDescriptor cameraTargetDescriptor, out RTHandle upscaleHandle)
		{
			this.source = colorTargetHandle;
			RenderTextureDescriptor desc = cameraTargetDescriptor;
			desc.width = width;
			desc.height = height;
			desc.depthStencilFormat = GraphicsFormat.None;
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.destination, in desc, mode, TextureWrapMode.Clamp, 1, 0f, "_UpscaleTexture");
			upscaleHandle = this.destination;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00014C70 File Offset: 0x00012E70
		public void Dispose()
		{
			RTHandle rthandle = this.destination;
			if (rthandle == null)
			{
				return;
			}
			rthandle.Release();
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00014C82 File Offset: 0x00012E82
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer commandBuffer = *renderingData.commandBuffer;
			commandBuffer.SetRenderTarget(this.destination);
			UpscalePass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(commandBuffer), this.source);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00014CAC File Offset: 0x00012EAC
		private static void ExecutePass(RasterCommandBuffer cmd, RTHandle source)
		{
			using (new ProfilingScope(cmd, UpscalePass.m_ExecuteProfilingSampler))
			{
				Vector2 viewportScale = (source.useScaling ? new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : Vector2.one);
				Blitter.BlitTexture(cmd, source, viewportScale, UpscalePass.m_BlitMaterial, (source.rt.filterMode == FilterMode.Bilinear) ? 1 : 0);
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00014D40 File Offset: 0x00012F40
		public void Render(RenderGraph graph, Camera camera, in TextureHandle cameraColorAttachment, in TextureHandle upscaleHandle)
		{
			PixelPerfectCamera ppc;
			camera.TryGetComponent<PixelPerfectCamera>(out ppc);
			if (ppc == null || !ppc.enabled || !ppc.requiresUpscalePass)
			{
				return;
			}
			UpscalePass.PassData passData;
			using (IRasterRenderGraphBuilder builder = graph.AddRasterRenderPass<UpscalePass.PassData>(UpscalePass.k_UpscalePass, out passData, UpscalePass.m_ProfilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/2D/Rendergraph/UpscalePass.cs", 71))
			{
				passData.source = cameraColorAttachment;
				builder.SetRenderAttachment(upscaleHandle, 0, AccessFlags.Write);
				builder.UseTexture(in cameraColorAttachment, AccessFlags.Read);
				builder.AllowPassCulling(false);
				builder.SetRenderFunc<UpscalePass.PassData>(delegate(UpscalePass.PassData data, RasterGraphContext context)
				{
					UpscalePass.ExecutePass(context.cmd, data.source);
				});
			}
		}

		// Token: 0x04000250 RID: 592
		private static readonly string k_UpscalePass = "Upscale2D Pass";

		// Token: 0x04000251 RID: 593
		private static readonly ProfilingSampler m_ProfilingSampler = new ProfilingSampler(UpscalePass.k_UpscalePass);

		// Token: 0x04000252 RID: 594
		private static readonly ProfilingSampler m_ExecuteProfilingSampler = new ProfilingSampler("Draw Upscale");

		// Token: 0x04000253 RID: 595
		private static Material m_BlitMaterial;

		// Token: 0x04000254 RID: 596
		private RTHandle source;

		// Token: 0x04000255 RID: 597
		private RTHandle destination;

		// Token: 0x02000068 RID: 104
		private class PassData
		{
			// Token: 0x04000256 RID: 598
			internal TextureHandle source;
		}
	}
}
