using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000206 RID: 518
	public class CopyDepthPass : ScriptableRenderPass
	{
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0003FD9B File Offset: 0x0003DF9B
		// (set) Token: 0x06000BA3 RID: 2979 RVA: 0x0003FDA3 File Offset: 0x0003DFA3
		private RTHandle source { get; set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0003FDAC File Offset: 0x0003DFAC
		// (set) Token: 0x06000BA5 RID: 2981 RVA: 0x0003FDB4 File Offset: 0x0003DFB4
		private RTHandle destination { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0003FDBD File Offset: 0x0003DFBD
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x0003FDC5 File Offset: 0x0003DFC5
		internal int MssaSamples { get; set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0003FDCE File Offset: 0x0003DFCE
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x0003FDD6 File Offset: 0x0003DFD6
		internal bool CopyToDepth { get; set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0003FDDF File Offset: 0x0003DFDF
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x0003FDE7 File Offset: 0x0003DFE7
		internal bool CopyToDepthXR { get; set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0003FDF0 File Offset: 0x0003DFF0
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x0003FDF8 File Offset: 0x0003DFF8
		internal bool CopyToBackbuffer { get; set; }

		// Token: 0x06000BAE RID: 2990 RVA: 0x0003FE04 File Offset: 0x0003E004
		public CopyDepthPass(RenderPassEvent evt, Shader copyDepthShader, bool shouldClear = false, bool copyToDepth = false, bool copyResolvedDepth = false, string customPassName = null)
		{
			base.profilingSampler = ((customPassName != null) ? new ProfilingSampler(customPassName) : ProfilingSampler.Get<URPProfileId>(URPProfileId.CopyDepth));
			this.m_PassData = new CopyDepthPass.PassData();
			this.CopyToDepth = copyToDepth;
			this.m_CopyDepthMaterial = ((copyDepthShader != null) ? CoreUtils.CreateEngineMaterial(copyDepthShader) : null);
			base.renderPassEvent = evt;
			this.m_CopyResolvedDepth = copyResolvedDepth;
			this.m_ShouldClear = shouldClear;
			this.CopyToDepthXR = false;
			this.CopyToBackbuffer = false;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0003FE7F File Offset: 0x0003E07F
		public void Setup(RTHandle source, RTHandle destination)
		{
			this.source = source;
			this.destination = destination;
			this.MssaSamples = -1;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0003FE96 File Offset: 0x0003E096
		public void Dispose()
		{
			CoreUtils.Destroy(this.m_CopyDepthMaterial);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0003FEA3 File Offset: 0x0003E0A3
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			base.ConfigureTarget(this.destination);
			if (this.m_ShouldClear)
			{
				base.ConfigureClear(ClearFlag.All, Color.black);
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0003FEC8 File Offset: 0x0003E0C8
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
			this.m_PassData.copyDepthMaterial = this.m_CopyDepthMaterial;
			this.m_PassData.msaaSamples = this.MssaSamples;
			this.m_PassData.copyResolvedDepth = this.m_CopyResolvedDepth;
			this.m_PassData.copyToDepth = this.CopyToDepth || this.CopyToDepthXR;
			this.m_PassData.isDstBackbuffer = this.CopyToBackbuffer || this.CopyToDepthXR;
			this.m_PassData.cameraData = cameraData;
			CommandBuffer cmd = *renderingData.commandBuffer;
			cmd.SetGlobalTexture(CopyDepthPass.ShaderConstants._CameraDepthAttachment, this.source.nameID);
			if (this.m_PassData.cameraData.xr.enabled && this.m_PassData.cameraData.xr.supportsFoveatedRendering)
			{
				cmd.SetFoveatedRenderingMode(FoveatedRenderingMode.Disabled);
			}
			CopyDepthPass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(cmd), this.m_PassData, this.source);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0003FFC4 File Offset: 0x0003E1C4
		private static void ExecutePass(RasterCommandBuffer cmd, CopyDepthPass.PassData passData, RTHandle source)
		{
			Material copyDepthMaterial = passData.copyDepthMaterial;
			int msaaSamples = passData.msaaSamples;
			bool copyResolvedDepth = passData.copyResolvedDepth;
			bool copyToDepth = passData.copyToDepth;
			if (copyDepthMaterial == null)
			{
				Debug.LogErrorFormat("Missing {0}. Copy Depth render pass will not execute. Check for missing reference in the renderer resources.", new object[] { copyDepthMaterial });
				return;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.CopyDepth)))
			{
				int cameraSamples;
				if (msaaSamples == -1)
				{
					cameraSamples = source.rt.antiAliasing;
				}
				else
				{
					cameraSamples = msaaSamples;
				}
				if (SystemInfo.supportsMultisampledTextures == 0 || copyResolvedDepth)
				{
					cameraSamples = 1;
				}
				if (cameraSamples != 2)
				{
					if (cameraSamples != 4)
					{
						if (cameraSamples == 8)
						{
							cmd.SetKeyword(in ShaderGlobalKeywords.DepthMsaa2, false);
							cmd.SetKeyword(in ShaderGlobalKeywords.DepthMsaa4, false);
							cmd.SetKeyword(in ShaderGlobalKeywords.DepthMsaa8, true);
						}
						else
						{
							cmd.SetKeyword(in ShaderGlobalKeywords.DepthMsaa2, false);
							cmd.SetKeyword(in ShaderGlobalKeywords.DepthMsaa4, false);
							cmd.SetKeyword(in ShaderGlobalKeywords.DepthMsaa8, false);
						}
					}
					else
					{
						cmd.SetKeyword(in ShaderGlobalKeywords.DepthMsaa2, false);
						cmd.SetKeyword(in ShaderGlobalKeywords.DepthMsaa4, true);
						cmd.SetKeyword(in ShaderGlobalKeywords.DepthMsaa8, false);
					}
				}
				else
				{
					cmd.SetKeyword(in ShaderGlobalKeywords.DepthMsaa2, true);
					cmd.SetKeyword(in ShaderGlobalKeywords.DepthMsaa4, false);
					cmd.SetKeyword(in ShaderGlobalKeywords.DepthMsaa8, false);
				}
				cmd.SetKeyword(in ShaderGlobalKeywords._OUTPUT_DEPTH, copyToDepth);
				bool flag = passData.cameraData.IsHandleYFlipped(source) && passData.isDstBackbuffer;
				Vector2 viewportScale = (source.useScaling ? new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : Vector2.one);
				Vector4 scaleBias = (flag ? new Vector4(viewportScale.x, -viewportScale.y, 0f, viewportScale.y) : new Vector4(viewportScale.x, viewportScale.y, 0f, 0f));
				if (passData.isDstBackbuffer)
				{
					cmd.SetViewport(passData.cameraData.pixelRect);
				}
				copyDepthMaterial.SetTexture(CopyDepthPass.ShaderConstants._CameraDepthAttachment, source);
				copyDepthMaterial.SetFloat(CopyDepthPass.ShaderConstants._ZWriteShaderHandle, copyToDepth ? 1f : 0f);
				Blitter.BlitTexture(cmd, source, scaleBias, copyDepthMaterial, 0);
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00040200 File Offset: 0x0003E400
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			this.destination = ScriptableRenderPass.k_CameraTarget;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0004021C File Offset: 0x0003E41C
		public void Render(RenderGraph renderGraph, ContextContainer frameData, TextureHandle destination, TextureHandle source, bool bindAsCameraDepth = false, string passName = "Copy Depth")
		{
			UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			this.Render(renderGraph, destination, source, resourceData, cameraData, bindAsCameraDepth, passName);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00040248 File Offset: 0x0003E448
		public void Render(RenderGraph renderGraph, TextureHandle destination, TextureHandle source, UniversalResourceData resourceData, UniversalCameraData cameraData, bool bindAsCameraDepth = false, string passName = "Copy Depth")
		{
			this.MssaSamples = -1;
			CopyDepthPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<CopyDepthPass.PassData>(passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/CopyDepthPass.cs", 261))
			{
				passData.copyDepthMaterial = this.m_CopyDepthMaterial;
				passData.msaaSamples = this.MssaSamples;
				passData.cameraData = cameraData;
				passData.copyResolvedDepth = this.m_CopyResolvedDepth;
				passData.copyToDepth = this.CopyToDepth || this.CopyToDepthXR;
				passData.isDstBackbuffer = this.CopyToBackbuffer || this.CopyToDepthXR;
				if (this.CopyToDepth)
				{
					builder.SetRenderAttachmentDepth(destination, AccessFlags.WriteAll);
				}
				else if (this.CopyToDepthXR)
				{
					builder.SetRenderAttachmentDepth(destination, AccessFlags.WriteAll);
					if (cameraData.xr.enabled && cameraData.xr.copyDepth)
					{
						builder.SetRenderAttachment(resourceData.backBufferColor, 0, AccessFlags.Write);
					}
				}
				else
				{
					builder.SetRenderAttachment(destination, 0, AccessFlags.WriteAll);
				}
				passData.source = source;
				builder.UseTexture(in source, AccessFlags.Read);
				if (bindAsCameraDepth && destination.IsValid())
				{
					builder.SetGlobalTextureAfterPass(in destination, CopyDepthPass.ShaderConstants._CameraDepthTexture);
				}
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				builder.SetRenderFunc<CopyDepthPass.PassData>(delegate(CopyDepthPass.PassData data, RasterGraphContext context)
				{
					CopyDepthPass.ExecutePass(context.cmd, data, data.source);
				});
			}
		}

		// Token: 0x04000D2A RID: 3370
		private Material m_CopyDepthMaterial;

		// Token: 0x04000D2B RID: 3371
		internal bool m_CopyResolvedDepth;

		// Token: 0x04000D2C RID: 3372
		internal bool m_ShouldClear;

		// Token: 0x04000D2D RID: 3373
		private CopyDepthPass.PassData m_PassData;

		// Token: 0x02000207 RID: 519
		private static class ShaderConstants
		{
			// Token: 0x04000D2E RID: 3374
			public static readonly int _CameraDepthAttachment = Shader.PropertyToID("_CameraDepthAttachment");

			// Token: 0x04000D2F RID: 3375
			public static readonly int _CameraDepthTexture = Shader.PropertyToID("_CameraDepthTexture");

			// Token: 0x04000D30 RID: 3376
			public static readonly int _ZWriteShaderHandle = Shader.PropertyToID("_ZWrite");
		}

		// Token: 0x02000208 RID: 520
		private class PassData
		{
			// Token: 0x04000D31 RID: 3377
			internal TextureHandle source;

			// Token: 0x04000D32 RID: 3378
			internal UniversalCameraData cameraData;

			// Token: 0x04000D33 RID: 3379
			internal Material copyDepthMaterial;

			// Token: 0x04000D34 RID: 3380
			internal int msaaSamples;

			// Token: 0x04000D35 RID: 3381
			internal bool copyResolvedDepth;

			// Token: 0x04000D36 RID: 3382
			internal bool copyToDepth;

			// Token: 0x04000D37 RID: 3383
			internal bool isDstBackbuffer;
		}
	}
}
