using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000073 RID: 115
	internal class DBufferRenderPass : ScriptableRenderPass
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00008A4A File Offset: 0x00006C4A
		// (set) Token: 0x06000297 RID: 663 RVA: 0x00008A52 File Offset: 0x00006C52
		internal RTHandle[] dBufferColorHandles { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00008A5B File Offset: 0x00006C5B
		// (set) Token: 0x06000299 RID: 665 RVA: 0x00008A63 File Offset: 0x00006C63
		internal RTHandle depthHandle { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00008A6C File Offset: 0x00006C6C
		internal RTHandle dBufferDepth
		{
			get
			{
				return this.m_DBufferDepth;
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00008A74 File Offset: 0x00006C74
		public DBufferRenderPass(Material dBufferClear, DBufferSettings settings, DecalDrawDBufferSystem drawSystem, bool decalLayers)
		{
			base.renderPassEvent = (RenderPassEvent)201;
			ScriptableRenderPassInput scriptableRenderPassInput = ScriptableRenderPassInput.Depth | ScriptableRenderPassInput.Normal;
			base.ConfigureInput(scriptableRenderPassInput);
			this.m_DrawSystem = drawSystem;
			this.m_Settings = settings;
			this.m_DBufferClear = dBufferClear;
			base.profilingSampler = new ProfilingSampler("Draw DBuffer");
			this.m_DBufferClearSampler = new ProfilingSampler("Clear");
			this.m_FilteringSettings = new FilteringSettings(new RenderQueueRange?(RenderQueueRange.opaque), -1, uint.MaxValue, 0);
			this.m_DecalLayers = decalLayers;
			this.m_ShaderTagIdList = new List<ShaderTagId>();
			this.m_ShaderTagIdList.Add(new ShaderTagId("DBufferMesh"));
			this.m_ShaderTagIdList.Add(new ShaderTagId("DBufferProjectorVFX"));
			int dBufferCount = (int)(settings.surfaceData + 1);
			this.dBufferColorHandles = new RTHandle[dBufferCount];
			this.m_PassData = new DBufferRenderPass.PassData();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00008B48 File Offset: 0x00006D48
		public void Dispose()
		{
			RTHandle dbufferDepth = this.m_DBufferDepth;
			if (dbufferDepth != null)
			{
				dbufferDepth.Release();
			}
			foreach (RTHandle rthandle in this.dBufferColorHandles)
			{
				if (rthandle != null)
				{
					rthandle.Release();
				}
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00008B8C File Offset: 0x00006D8C
		public unsafe void Setup(in CameraData cameraData)
		{
			CameraData cameraData2 = cameraData;
			RenderTextureDescriptor depthDesc = *cameraData2.cameraTargetDescriptor;
			depthDesc.graphicsFormat = GraphicsFormat.None;
			cameraData2 = cameraData;
			depthDesc.depthStencilFormat = cameraData2.cameraTargetDescriptor.depthStencilFormat;
			depthDesc.msaaSamples = 1;
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_DBufferDepth, in depthDesc, FilterMode.Point, TextureWrapMode.Repeat, 1, 0f, DBufferRenderPass.s_DBufferDepthName);
			this.Setup(in cameraData, this.m_DBufferDepth);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00008C00 File Offset: 0x00006E00
		public unsafe void Setup(in CameraData cameraData, RTHandle depthTextureHandle)
		{
			CameraData cameraData2 = cameraData;
			RenderTextureDescriptor desc = *cameraData2.cameraTargetDescriptor;
			desc.graphicsFormat = ((QualitySettings.activeColorSpace == ColorSpace.Linear) ? GraphicsFormat.R8G8B8A8_SRGB : GraphicsFormat.R8G8B8A8_UNorm);
			desc.depthStencilFormat = GraphicsFormat.None;
			desc.msaaSamples = 1;
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.dBufferColorHandles[0], in desc, FilterMode.Point, TextureWrapMode.Repeat, 1, 0f, DBufferRenderPass.s_DBufferNames[0]);
			if (this.m_Settings.surfaceData == DecalSurfaceData.AlbedoNormal || this.m_Settings.surfaceData == DecalSurfaceData.AlbedoNormalMAOS)
			{
				cameraData2 = cameraData;
				RenderTextureDescriptor desc2 = *cameraData2.cameraTargetDescriptor;
				desc2.graphicsFormat = GraphicsFormat.R8G8B8A8_UNorm;
				desc2.depthStencilFormat = GraphicsFormat.None;
				desc2.msaaSamples = 1;
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.dBufferColorHandles[1], in desc2, FilterMode.Point, TextureWrapMode.Repeat, 1, 0f, DBufferRenderPass.s_DBufferNames[1]);
			}
			if (this.m_Settings.surfaceData == DecalSurfaceData.AlbedoNormalMAOS)
			{
				cameraData2 = cameraData;
				RenderTextureDescriptor desc3 = *cameraData2.cameraTargetDescriptor;
				desc3.graphicsFormat = GraphicsFormat.R8G8B8A8_UNorm;
				desc3.depthStencilFormat = GraphicsFormat.None;
				desc3.msaaSamples = 1;
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.dBufferColorHandles[2], in desc3, FilterMode.Point, TextureWrapMode.Repeat, 1, 0f, DBufferRenderPass.s_DBufferNames[2]);
			}
			this.depthHandle = depthTextureHandle;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00008D36 File Offset: 0x00006F36
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			base.ConfigureTarget(this.dBufferColorHandles, this.depthHandle);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00008D4C File Offset: 0x00006F4C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			this.InitPassData(ref this.m_PassData);
			CommandBuffer cmd = *renderingData.commandBuffer;
			DBufferRenderPass.PassData passData = this.m_PassData;
			using (new ProfilingScope(cmd, base.profilingSampler))
			{
				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();
				DBufferRenderPass.SetGlobalTextures(*renderingData.commandBuffer, this.m_PassData);
				DBufferRenderPass.SetKeywords(CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer), this.m_PassData);
				DBufferRenderPass.Clear(*renderingData.commandBuffer, this.m_PassData);
				UniversalRenderingData universalRenderingData = renderingData.frameData.Get<UniversalRenderingData>();
				UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
				UniversalLightData lightData = renderingData.frameData.Get<UniversalLightData>();
				RendererListParams param = this.InitRendererListParams(universalRenderingData, cameraData, lightData);
				RendererList rendererList = context.CreateRendererList(ref param);
				DBufferRenderPass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer), this.m_PassData, rendererList, false);
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00008E44 File Offset: 0x00007044
		private static void ExecutePass(RasterCommandBuffer cmd, DBufferRenderPass.PassData passData, RendererList rendererList, bool renderGraph)
		{
			passData.drawSystem.Execute(cmd);
			cmd.DrawRendererList(rendererList);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00008E5C File Offset: 0x0000705C
		private static void SetGlobalTextures(CommandBuffer cmd, DBufferRenderPass.PassData passData)
		{
			RTHandle[] dBufferColorHandles = passData.dBufferColorHandles;
			cmd.SetGlobalTexture(dBufferColorHandles[0].name, dBufferColorHandles[0].nameID);
			if (passData.settings.surfaceData == DecalSurfaceData.AlbedoNormal || passData.settings.surfaceData == DecalSurfaceData.AlbedoNormalMAOS)
			{
				cmd.SetGlobalTexture(dBufferColorHandles[1].name, dBufferColorHandles[1].nameID);
			}
			if (passData.settings.surfaceData == DecalSurfaceData.AlbedoNormalMAOS)
			{
				cmd.SetGlobalTexture(dBufferColorHandles[2].name, dBufferColorHandles[2].nameID);
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00008EDC File Offset: 0x000070DC
		private static void SetKeywords(RasterCommandBuffer cmd, DBufferRenderPass.PassData passData)
		{
			cmd.SetKeyword(in ShaderGlobalKeywords.DBufferMRT1, passData.settings.surfaceData == DecalSurfaceData.Albedo);
			cmd.SetKeyword(in ShaderGlobalKeywords.DBufferMRT2, passData.settings.surfaceData == DecalSurfaceData.AlbedoNormal);
			cmd.SetKeyword(in ShaderGlobalKeywords.DBufferMRT3, passData.settings.surfaceData == DecalSurfaceData.AlbedoNormalMAOS);
			cmd.SetKeyword(in ShaderGlobalKeywords.DecalLayers, passData.decalLayers);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00008F48 File Offset: 0x00007148
		private static void Clear(CommandBuffer cmd, DBufferRenderPass.PassData passData)
		{
			using (new ProfilingScope(cmd, passData.dBufferClearSampler))
			{
				Blitter.BlitTexture(cmd, passData.dBufferColorHandles[0], new Vector4(1f, 1f, 0f, 0f), passData.dBufferClear, 0);
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00008FB4 File Offset: 0x000071B4
		private void InitPassData(ref DBufferRenderPass.PassData passData)
		{
			passData.drawSystem = this.m_DrawSystem;
			passData.settings = this.m_Settings;
			passData.dBufferClear = this.m_DBufferClear;
			passData.dBufferClearSampler = this.m_DBufferClearSampler;
			passData.decalLayers = this.m_DecalLayers;
			passData.dBufferDepth = this.m_DBufferDepth;
			passData.dBufferColorHandles = this.dBufferColorHandles;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000901C File Offset: 0x0000721C
		private RendererListParams InitRendererListParams(UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData)
		{
			SortingCriteria sortingCriteria = cameraData.defaultOpaqueSortFlags;
			DrawingSettings drawingSettings = RenderingUtils.CreateDrawingSettings(this.m_ShaderTagIdList, renderingData, cameraData, lightData, sortingCriteria);
			return new RendererListParams(renderingData.cullResults, drawingSettings, this.m_FilteringSettings);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00009054 File Offset: 0x00007254
		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
			UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
			UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			TextureHandle cameraDepthTexture = resourceData.cameraDepthTexture;
			TextureHandle cameraNormalsTexture = resourceData.cameraNormalsTexture;
			TextureHandle textureHandle = resourceData.dBufferDepth;
			TextureHandle depthTarget = (textureHandle.IsValid() ? resourceData.dBufferDepth : resourceData.activeDepthTexture);
			DBufferRenderPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<DBufferRenderPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Decal/DBuffer/DBufferRenderPass.cs", 238))
			{
				this.InitPassData(ref passData);
				if (this.dbufferHandles == null)
				{
					this.dbufferHandles = new TextureHandle[3];
				}
				RenderTextureDescriptor desc = cameraData.cameraTargetDescriptor;
				desc.graphicsFormat = ((QualitySettings.activeColorSpace == ColorSpace.Linear) ? GraphicsFormat.R8G8B8A8_SRGB : GraphicsFormat.R8G8B8A8_UNorm);
				desc.depthStencilFormat = GraphicsFormat.None;
				desc.msaaSamples = 1;
				this.dbufferHandles[0] = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, DBufferRenderPass.s_DBufferNames[0], true, new Color(0f, 0f, 0f, 1f), FilterMode.Point, TextureWrapMode.Clamp);
				builder.SetRenderAttachment(this.dbufferHandles[0], 0, AccessFlags.Write);
				if (this.m_Settings.surfaceData == DecalSurfaceData.AlbedoNormal || this.m_Settings.surfaceData == DecalSurfaceData.AlbedoNormalMAOS)
				{
					RenderTextureDescriptor desc2 = cameraData.cameraTargetDescriptor;
					desc2.graphicsFormat = GraphicsFormat.R8G8B8A8_UNorm;
					desc2.depthStencilFormat = GraphicsFormat.None;
					desc2.msaaSamples = 1;
					this.dbufferHandles[1] = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc2, DBufferRenderPass.s_DBufferNames[1], true, new Color(0.5f, 0.5f, 0.5f, 1f), FilterMode.Point, TextureWrapMode.Clamp);
					builder.SetRenderAttachment(this.dbufferHandles[1], 1, AccessFlags.Write);
				}
				if (this.m_Settings.surfaceData == DecalSurfaceData.AlbedoNormalMAOS)
				{
					RenderTextureDescriptor desc3 = cameraData.cameraTargetDescriptor;
					desc3.graphicsFormat = GraphicsFormat.R8G8B8A8_UNorm;
					desc3.depthStencilFormat = GraphicsFormat.None;
					desc3.msaaSamples = 1;
					this.dbufferHandles[2] = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc3, DBufferRenderPass.s_DBufferNames[2], true, new Color(0f, 0f, 0f, 1f), FilterMode.Point, TextureWrapMode.Clamp);
					builder.SetRenderAttachment(this.dbufferHandles[2], 2, AccessFlags.Write);
				}
				builder.SetRenderAttachmentDepth(depthTarget, AccessFlags.Read);
				if (cameraDepthTexture.IsValid())
				{
					builder.UseTexture(in cameraDepthTexture, AccessFlags.Read);
				}
				if (cameraNormalsTexture.IsValid())
				{
					builder.UseTexture(in cameraNormalsTexture, AccessFlags.Read);
				}
				if (passData.decalLayers)
				{
					IBaseRenderGraphBuilder baseRenderGraphBuilder = builder;
					textureHandle = resourceData.renderingLayersTexture;
					baseRenderGraphBuilder.UseTexture(in textureHandle, AccessFlags.Read);
				}
				textureHandle = resourceData.ssaoTexture;
				if (textureHandle.IsValid())
				{
					builder.UseGlobalTexture(DBufferRenderPass.s_SSAOTextureID, AccessFlags.Read);
				}
				RendererListParams param = this.InitRendererListParams(renderingData, cameraData, lightData);
				passData.rendererList = renderGraph.CreateRendererList(in param);
				builder.UseRendererList(in passData.rendererList);
				for (int i = 0; i < 3; i++)
				{
					if (this.dbufferHandles[i].IsValid())
					{
						builder.SetGlobalTextureAfterPass(in this.dbufferHandles[i], Shader.PropertyToID(DBufferRenderPass.s_DBufferNames[i]));
					}
				}
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				builder.SetRenderFunc<DBufferRenderPass.PassData>(delegate(DBufferRenderPass.PassData data, RasterGraphContext rgContext)
				{
					DBufferRenderPass.SetKeywords(rgContext.cmd, data);
					DBufferRenderPass.ExecutePass(rgContext.cmd, data, data.rendererList, true);
				});
			}
			resourceData.dBuffer = this.dbufferHandles;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000093A8 File Offset: 0x000075A8
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			cmd.SetKeyword(in ShaderGlobalKeywords.DBufferMRT1, false);
			cmd.SetKeyword(in ShaderGlobalKeywords.DBufferMRT2, false);
			cmd.SetKeyword(in ShaderGlobalKeywords.DBufferMRT3, false);
			cmd.SetKeyword(in ShaderGlobalKeywords.DecalLayers, false);
		}

		// Token: 0x0400020D RID: 525
		internal static string[] s_DBufferNames = new string[] { "_DBufferTexture0", "_DBufferTexture1", "_DBufferTexture2", "_DBufferTexture3" };

		// Token: 0x0400020E RID: 526
		internal static string s_DBufferDepthName = "DBufferDepth";

		// Token: 0x0400020F RID: 527
		private static readonly int s_SSAOTextureID = Shader.PropertyToID("_ScreenSpaceOcclusionTexture");

		// Token: 0x04000210 RID: 528
		private DecalDrawDBufferSystem m_DrawSystem;

		// Token: 0x04000211 RID: 529
		private DBufferSettings m_Settings;

		// Token: 0x04000212 RID: 530
		private Material m_DBufferClear;

		// Token: 0x04000213 RID: 531
		private FilteringSettings m_FilteringSettings;

		// Token: 0x04000214 RID: 532
		private List<ShaderTagId> m_ShaderTagIdList;

		// Token: 0x04000215 RID: 533
		private ProfilingSampler m_DBufferClearSampler;

		// Token: 0x04000216 RID: 534
		private bool m_DecalLayers;

		// Token: 0x04000217 RID: 535
		private RTHandle m_DBufferDepth;

		// Token: 0x04000218 RID: 536
		private DBufferRenderPass.PassData m_PassData;

		// Token: 0x0400021B RID: 539
		private TextureHandle[] dbufferHandles;

		// Token: 0x02000074 RID: 116
		private class PassData
		{
			// Token: 0x0400021C RID: 540
			internal DecalDrawDBufferSystem drawSystem;

			// Token: 0x0400021D RID: 541
			internal DBufferSettings settings;

			// Token: 0x0400021E RID: 542
			internal Material dBufferClear;

			// Token: 0x0400021F RID: 543
			internal ProfilingSampler dBufferClearSampler;

			// Token: 0x04000220 RID: 544
			internal bool decalLayers;

			// Token: 0x04000221 RID: 545
			internal RTHandle dBufferDepth;

			// Token: 0x04000222 RID: 546
			internal RTHandle[] dBufferColorHandles;

			// Token: 0x04000223 RID: 547
			internal RendererListHandle rendererList;
		}
	}
}
