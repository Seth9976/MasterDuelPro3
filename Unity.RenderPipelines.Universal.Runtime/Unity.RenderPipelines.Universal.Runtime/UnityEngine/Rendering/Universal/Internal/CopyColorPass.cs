using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000203 RID: 515
	public class CopyColorPass : ScriptableRenderPass
	{
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0003F88E File Offset: 0x0003DA8E
		// (set) Token: 0x06000B91 RID: 2961 RVA: 0x0003F896 File Offset: 0x0003DA96
		private RTHandle source { get; set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0003F89F File Offset: 0x0003DA9F
		// (set) Token: 0x06000B93 RID: 2963 RVA: 0x0003F8A7 File Offset: 0x0003DAA7
		private RTHandle destination { get; set; }

		// Token: 0x06000B94 RID: 2964 RVA: 0x0003F8B0 File Offset: 0x0003DAB0
		public CopyColorPass(RenderPassEvent evt, Material samplingMaterial, Material copyColorMaterial = null, string customPassName = null)
		{
			base.profilingSampler = ((customPassName != null) ? new ProfilingSampler(customPassName) : ProfilingSampler.Get<URPProfileId>(URPProfileId.CopyColor));
			this.m_PassData = new CopyColorPass.PassData();
			this.m_SamplingMaterial = samplingMaterial;
			this.m_CopyColorMaterial = copyColorMaterial;
			this.m_SampleOffsetShaderHandle = Shader.PropertyToID("_SampleOffset");
			base.renderPassEvent = evt;
			this.m_DownsamplingMethod = Downsampling.None;
			base.useNativeRenderPass = false;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0003F91C File Offset: 0x0003DB1C
		public static void ConfigureDescriptor(Downsampling downsamplingMethod, ref RenderTextureDescriptor descriptor, out FilterMode filterMode)
		{
			descriptor.msaaSamples = 1;
			descriptor.depthStencilFormat = GraphicsFormat.None;
			if (downsamplingMethod == Downsampling._2xBilinear)
			{
				descriptor.width = Mathf.Max(1, descriptor.width / 2);
				descriptor.height = Mathf.Max(1, descriptor.height / 2);
			}
			else if (downsamplingMethod == Downsampling._4xBox || downsamplingMethod == Downsampling._4xBilinear)
			{
				descriptor.width = Mathf.Max(1, descriptor.width / 4);
				descriptor.height = Mathf.Max(1, descriptor.height / 4);
			}
			filterMode = ((downsamplingMethod == Downsampling.None) ? FilterMode.Point : FilterMode.Bilinear);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0003F99E File Offset: 0x0003DB9E
		[Obsolete("Use RTHandles for source and destination.", true)]
		public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination, Downsampling downsampling)
		{
			throw new NotSupportedException("Setup with RenderTargetIdentifier has been deprecated. Use it with RTHandles instead.");
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0003F9AA File Offset: 0x0003DBAA
		public void Setup(RTHandle source, RTHandle destination, Downsampling downsampling)
		{
			this.source = source;
			this.destination = destination;
			this.m_DownsamplingMethod = downsampling;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0003F9C1 File Offset: 0x0003DBC1
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			cmd.SetGlobalTexture(this.destination.name, this.destination.nameID);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0003F9E0 File Offset: 0x0003DBE0
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			this.m_PassData.samplingMaterial = this.m_SamplingMaterial;
			this.m_PassData.copyColorMaterial = this.m_CopyColorMaterial;
			this.m_PassData.downsamplingMethod = this.m_DownsamplingMethod;
			this.m_PassData.sampleOffsetShaderHandle = this.m_SampleOffsetShaderHandle;
			CommandBuffer cmd = *renderingData.commandBuffer;
			if (this.source == renderingData.cameraData.renderer->GetCameraColorFrontBuffer(cmd))
			{
				this.source = renderingData.cameraData.renderer->cameraColorTargetHandle;
			}
			if (renderingData.cameraData.xr.supportsFoveatedRendering)
			{
				cmd.SetFoveatedRenderingMode(FoveatedRenderingMode.Disabled);
			}
			ScriptableRenderer.SetRenderTarget(cmd, this.destination, ScriptableRenderPass.k_CameraTarget, base.clearFlag, base.clearColor);
			CopyColorPass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(cmd), this.m_PassData, this.source, renderingData.cameraData.xr.enabled);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0003FAC8 File Offset: 0x0003DCC8
		private static void ExecutePass(RasterCommandBuffer cmd, CopyColorPass.PassData passData, RTHandle source, bool useDrawProceduralBlit)
		{
			Material samplingMaterial = passData.samplingMaterial;
			Material copyColorMaterial = passData.copyColorMaterial;
			Downsampling downsamplingMethod = passData.downsamplingMethod;
			int sampleOffsetShaderHandle = passData.sampleOffsetShaderHandle;
			if (samplingMaterial == null)
			{
				Debug.LogErrorFormat("Missing {0}. Copy Color render pass will not execute. Check for missing reference in the renderer resources.", new object[] { samplingMaterial });
				return;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.CopyColor)))
			{
				Vector2 viewportScale = (source.useScaling ? new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : Vector2.one);
				switch (downsamplingMethod)
				{
				case Downsampling.None:
					Blitter.BlitTexture(cmd, source, viewportScale, copyColorMaterial, 0);
					break;
				case Downsampling._2xBilinear:
					Blitter.BlitTexture(cmd, source, viewportScale, copyColorMaterial, 1);
					break;
				case Downsampling._4xBox:
					samplingMaterial.SetFloat(sampleOffsetShaderHandle, 2f);
					Blitter.BlitTexture(cmd, source, viewportScale, samplingMaterial, 0);
					break;
				case Downsampling._4xBilinear:
					Blitter.BlitTexture(cmd, source, viewportScale, copyColorMaterial, 1);
					break;
				}
			}
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0003FBE0 File Offset: 0x0003DDE0
		internal TextureHandle Render(RenderGraph renderGraph, ContextContainer frameData, out TextureHandle destination, in TextureHandle source, Downsampling downsampling)
		{
			this.m_DownsamplingMethod = downsampling;
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			RenderTextureDescriptor descriptor = cameraData.cameraTargetDescriptor;
			FilterMode filterMode;
			CopyColorPass.ConfigureDescriptor(downsampling, ref descriptor, out filterMode);
			destination = UniversalRenderer.CreateRenderGraphTexture(renderGraph, descriptor, "_CameraOpaqueTexture", true, filterMode, TextureWrapMode.Clamp);
			this.RenderInternal(renderGraph, in destination, in source, cameraData.xr.enabled);
			return destination;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0003FC40 File Offset: 0x0003DE40
		internal void RenderToExistingTexture(RenderGraph renderGraph, ContextContainer frameData, in TextureHandle destination, in TextureHandle source, Downsampling downsampling = Downsampling.None)
		{
			this.m_DownsamplingMethod = downsampling;
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			this.RenderInternal(renderGraph, in destination, in source, cameraData.xr.enabled);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0003FC74 File Offset: 0x0003DE74
		private void RenderInternal(RenderGraph renderGraph, in TextureHandle destination, in TextureHandle source, bool useProceduralBlit)
		{
			CopyColorPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<CopyColorPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/CopyColorPass.cs", 216))
			{
				passData.destination = destination;
				builder.SetRenderAttachment(destination, 0, AccessFlags.WriteAll);
				passData.source = source;
				builder.UseTexture(in source, AccessFlags.Read);
				passData.useProceduralBlit = useProceduralBlit;
				passData.samplingMaterial = this.m_SamplingMaterial;
				passData.copyColorMaterial = this.m_CopyColorMaterial;
				passData.downsamplingMethod = this.m_DownsamplingMethod;
				passData.sampleOffsetShaderHandle = this.m_SampleOffsetShaderHandle;
				TextureHandle textureHandle = destination;
				if (textureHandle.IsValid())
				{
					builder.SetGlobalTextureAfterPass(in destination, Shader.PropertyToID("_CameraOpaqueTexture"));
				}
				builder.AllowPassCulling(false);
				builder.SetRenderFunc<CopyColorPass.PassData>(delegate(CopyColorPass.PassData data, RasterGraphContext context)
				{
					CopyColorPass.ExecutePass(context.cmd, data, data.source, data.useProceduralBlit);
				});
			}
		}

		// Token: 0x04000D14 RID: 3348
		private int m_SampleOffsetShaderHandle;

		// Token: 0x04000D15 RID: 3349
		private Material m_SamplingMaterial;

		// Token: 0x04000D16 RID: 3350
		private Downsampling m_DownsamplingMethod;

		// Token: 0x04000D17 RID: 3351
		private Material m_CopyColorMaterial;

		// Token: 0x04000D1A RID: 3354
		private CopyColorPass.PassData m_PassData;

		// Token: 0x02000204 RID: 516
		private class PassData
		{
			// Token: 0x04000D1B RID: 3355
			internal TextureHandle source;

			// Token: 0x04000D1C RID: 3356
			internal TextureHandle destination;

			// Token: 0x04000D1D RID: 3357
			internal bool useProceduralBlit;

			// Token: 0x04000D1E RID: 3358
			internal Material samplingMaterial;

			// Token: 0x04000D1F RID: 3359
			internal Material copyColorMaterial;

			// Token: 0x04000D20 RID: 3360
			internal Downsampling downsamplingMethod;

			// Token: 0x04000D21 RID: 3361
			internal int sampleOffsetShaderHandle;
		}
	}
}
