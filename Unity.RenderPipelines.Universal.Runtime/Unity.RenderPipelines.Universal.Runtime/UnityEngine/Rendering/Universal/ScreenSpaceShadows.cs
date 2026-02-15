using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000179 RID: 377
	[SupportedOnRenderer(typeof(UniversalRendererData))]
	[DisallowMultipleRendererFeature("Screen Space Shadows")]
	[Tooltip("Screen Space Shadows")]
	internal class ScreenSpaceShadows : ScriptableRendererFeature
	{
		// Token: 0x060007ED RID: 2029 RVA: 0x00025ED8 File Offset: 0x000240D8
		public override void Create()
		{
			if (this.m_SSShadowsPass == null)
			{
				this.m_SSShadowsPass = new ScreenSpaceShadows.ScreenSpaceShadowsPass();
			}
			if (this.m_SSShadowsPostPass == null)
			{
				this.m_SSShadowsPostPass = new ScreenSpaceShadows.ScreenSpaceShadowsPostPass();
			}
			this.LoadMaterial();
			this.m_SSShadowsPass.renderPassEvent = RenderPassEvent.AfterRenderingGbuffer;
			this.m_SSShadowsPostPass.renderPassEvent = RenderPassEvent.BeforeRenderingTransparents;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00025F34 File Offset: 0x00024134
		public unsafe override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			if (UniversalRenderer.IsOffscreenDepthTexture(ref renderingData.cameraData))
			{
				return;
			}
			if (!this.LoadMaterial())
			{
				Debug.LogErrorFormat("{0}.AddRenderPasses(): Missing material. {1} render pass will not be added. Check for missing reference in the renderer resources.", new object[]
				{
					base.GetType().Name,
					base.name
				});
				return;
			}
			if (*renderingData.shadowData.supportsMainLightShadows && *renderingData.lightData.mainLightIndex != -1 && this.m_SSShadowsPass.Setup(this.m_Settings, this.m_Material))
			{
				bool isDeferredRenderingMode = renderer is UniversalRenderer && ((UniversalRenderer)renderer).renderingModeRequested == RenderingMode.Deferred;
				this.m_SSShadowsPass.renderPassEvent = (isDeferredRenderingMode ? RenderPassEvent.AfterRenderingGbuffer : ((RenderPassEvent)201));
				renderer.EnqueuePass(this.m_SSShadowsPass);
				renderer.EnqueuePass(this.m_SSShadowsPostPass);
			}
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0002600C File Offset: 0x0002420C
		protected override void Dispose(bool disposing)
		{
			ScreenSpaceShadows.ScreenSpaceShadowsPass ssshadowsPass = this.m_SSShadowsPass;
			if (ssshadowsPass != null)
			{
				ssshadowsPass.Dispose();
			}
			this.m_SSShadowsPass = null;
			CoreUtils.Destroy(this.m_Material);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00026034 File Offset: 0x00024234
		private bool LoadMaterial()
		{
			if (this.m_Material != null)
			{
				return true;
			}
			if (this.m_Shader == null)
			{
				this.m_Shader = Shader.Find("Hidden/Universal Render Pipeline/ScreenSpaceShadows");
				if (this.m_Shader == null)
				{
					return false;
				}
			}
			this.m_Material = CoreUtils.CreateEngineMaterial(this.m_Shader);
			return this.m_Material != null;
		}

		// Token: 0x04000899 RID: 2201
		[SerializeField]
		[HideInInspector]
		private Shader m_Shader;

		// Token: 0x0400089A RID: 2202
		[SerializeField]
		private ScreenSpaceShadowsSettings m_Settings = new ScreenSpaceShadowsSettings();

		// Token: 0x0400089B RID: 2203
		private Material m_Material;

		// Token: 0x0400089C RID: 2204
		private ScreenSpaceShadows.ScreenSpaceShadowsPass m_SSShadowsPass;

		// Token: 0x0400089D RID: 2205
		private ScreenSpaceShadows.ScreenSpaceShadowsPostPass m_SSShadowsPostPass;

		// Token: 0x0400089E RID: 2206
		private const string k_ShaderName = "Hidden/Universal Render Pipeline/ScreenSpaceShadows";

		// Token: 0x0200017A RID: 378
		private class ScreenSpaceShadowsPass : ScriptableRenderPass
		{
			// Token: 0x060007F2 RID: 2034 RVA: 0x000260AF File Offset: 0x000242AF
			internal ScreenSpaceShadowsPass()
			{
				base.profilingSampler = new ProfilingSampler("Blit Screen Space Shadows");
				this.m_CurrentSettings = new ScreenSpaceShadowsSettings();
				this.m_ScreenSpaceShadowmapTextureID = Shader.PropertyToID("_ScreenSpaceShadowmapTexture");
				this.m_PassData = new ScreenSpaceShadows.ScreenSpaceShadowsPass.PassData();
			}

			// Token: 0x060007F3 RID: 2035 RVA: 0x000260ED File Offset: 0x000242ED
			public void Dispose()
			{
				RTHandle renderTarget = this.m_RenderTarget;
				if (renderTarget == null)
				{
					return;
				}
				renderTarget.Release();
			}

			// Token: 0x060007F4 RID: 2036 RVA: 0x000260FF File Offset: 0x000242FF
			internal bool Setup(ScreenSpaceShadowsSettings featureSettings, Material material)
			{
				this.m_CurrentSettings = featureSettings;
				this.m_Material = material;
				base.ConfigureInput(ScriptableRenderPassInput.Depth);
				return this.m_Material != null;
			}

			// Token: 0x060007F5 RID: 2037 RVA: 0x00026124 File Offset: 0x00024324
			[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
			public unsafe override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
			{
				RenderTextureDescriptor desc = *renderingData.cameraData.cameraTargetDescriptor;
				desc.depthStencilFormat = GraphicsFormat.None;
				desc.msaaSamples = 1;
				desc.graphicsFormat = (SystemInfo.IsFormatSupported(GraphicsFormat.R8_UNorm, GraphicsFormatUsage.Blend) ? GraphicsFormat.R8_UNorm : GraphicsFormat.B8G8R8A8_UNorm);
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_RenderTarget, in desc, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, "_ScreenSpaceShadowmapTexture");
				cmd.SetGlobalTexture(this.m_RenderTarget.name, this.m_RenderTarget.nameID);
				base.ConfigureTarget(this.m_RenderTarget);
				base.ConfigureClear(ClearFlag.None, Color.white);
			}

			// Token: 0x060007F6 RID: 2038 RVA: 0x000261B7 File Offset: 0x000243B7
			private void InitPassData(ref ScreenSpaceShadows.ScreenSpaceShadowsPass.PassData passData)
			{
				passData.material = this.m_Material;
				passData.shadowmapID = this.m_ScreenSpaceShadowmapTextureID;
			}

			// Token: 0x060007F7 RID: 2039 RVA: 0x000261D4 File Offset: 0x000243D4
			public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
			{
				if (this.m_Material == null)
				{
					Debug.LogErrorFormat("{0}.Execute(): Missing material. ScreenSpaceShadows pass will not execute. Check for missing reference in the renderer resources.", new object[] { base.GetType().Name });
					return;
				}
				RenderTextureDescriptor desc = frameData.Get<UniversalCameraData>().cameraTargetDescriptor;
				desc.depthStencilFormat = GraphicsFormat.None;
				desc.msaaSamples = 1;
				desc.graphicsFormat = (SystemInfo.IsFormatSupported(GraphicsFormat.R8_UNorm, GraphicsFormatUsage.Blend) ? GraphicsFormat.R8_UNorm : GraphicsFormat.B8G8R8A8_UNorm);
				TextureHandle color = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, "_ScreenSpaceShadowmapTexture", true, FilterMode.Point, TextureWrapMode.Clamp);
				ScreenSpaceShadows.ScreenSpaceShadowsPass.PassData passData;
				using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<ScreenSpaceShadows.ScreenSpaceShadowsPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/RendererFeatures/ScreenSpaceShadows.cs", 197))
				{
					passData.target = color;
					builder.SetRenderAttachment(color, 0, AccessFlags.Write);
					this.InitPassData(ref passData);
					builder.AllowGlobalStateModification(true);
					if (color.IsValid())
					{
						builder.SetGlobalTextureAfterPass(in color, this.m_ScreenSpaceShadowmapTextureID);
					}
					builder.SetRenderFunc<ScreenSpaceShadows.ScreenSpaceShadowsPass.PassData>(delegate(ScreenSpaceShadows.ScreenSpaceShadowsPass.PassData data, RasterGraphContext rgContext)
					{
						ScreenSpaceShadows.ScreenSpaceShadowsPass.ExecutePass(rgContext.cmd, data, data.target);
					});
				}
			}

			// Token: 0x060007F8 RID: 2040 RVA: 0x000262E8 File Offset: 0x000244E8
			private static void ExecutePass(RasterCommandBuffer cmd, ScreenSpaceShadows.ScreenSpaceShadowsPass.PassData data, RTHandle target)
			{
				Blitter.BlitTexture(cmd, target, Vector2.one, data.material, 0);
				cmd.SetKeyword(in ShaderGlobalKeywords.MainLightShadows, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.MainLightShadowCascades, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.MainLightShadowScreen, true);
			}

			// Token: 0x060007F9 RID: 2041 RVA: 0x00026328 File Offset: 0x00024528
			[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
			public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
			{
				if (this.m_Material == null)
				{
					Debug.LogErrorFormat("{0}.Execute(): Missing material. ScreenSpaceShadows pass will not execute. Check for missing reference in the renderer resources.", new object[] { base.GetType().Name });
					return;
				}
				this.InitPassData(ref this.m_PassData);
				CommandBuffer cmd = *renderingData.commandBuffer;
				using (new ProfilingScope(cmd, base.profilingSampler))
				{
					ScreenSpaceShadows.ScreenSpaceShadowsPass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer), this.m_PassData, this.m_RenderTarget);
				}
			}

			// Token: 0x0400089F RID: 2207
			private Material m_Material;

			// Token: 0x040008A0 RID: 2208
			private ScreenSpaceShadowsSettings m_CurrentSettings;

			// Token: 0x040008A1 RID: 2209
			private RTHandle m_RenderTarget;

			// Token: 0x040008A2 RID: 2210
			private int m_ScreenSpaceShadowmapTextureID;

			// Token: 0x040008A3 RID: 2211
			private ScreenSpaceShadows.ScreenSpaceShadowsPass.PassData m_PassData;

			// Token: 0x0200017B RID: 379
			private class PassData
			{
				// Token: 0x040008A4 RID: 2212
				internal TextureHandle target;

				// Token: 0x040008A5 RID: 2213
				internal Material material;

				// Token: 0x040008A6 RID: 2214
				internal int shadowmapID;
			}
		}

		// Token: 0x0200017D RID: 381
		private class ScreenSpaceShadowsPostPass : ScriptableRenderPass
		{
			// Token: 0x060007FE RID: 2046 RVA: 0x000263E9 File Offset: 0x000245E9
			internal ScreenSpaceShadowsPostPass()
			{
				base.profilingSampler = new ProfilingSampler("Set Screen Space Shadow Keywords");
			}

			// Token: 0x060007FF RID: 2047 RVA: 0x00026401 File Offset: 0x00024601
			[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
			public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
			{
				base.ConfigureTarget(ScreenSpaceShadows.ScreenSpaceShadowsPostPass.k_CurrentActive);
			}

			// Token: 0x06000800 RID: 2048 RVA: 0x00026410 File Offset: 0x00024610
			private static void ExecutePass(RasterCommandBuffer cmd, UniversalShadowData shadowData)
			{
				int cascadesCount = shadowData.mainLightShadowCascadesCount;
				bool supportsMainLightShadows = shadowData.supportsMainLightShadows;
				bool receiveShadowsNoCascade = supportsMainLightShadows && cascadesCount == 1;
				bool receiveShadowsCascades = supportsMainLightShadows && cascadesCount > 1;
				cmd.SetKeyword(in ShaderGlobalKeywords.MainLightShadowScreen, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.MainLightShadows, receiveShadowsNoCascade);
				cmd.SetKeyword(in ShaderGlobalKeywords.MainLightShadowCascades, receiveShadowsCascades);
			}

			// Token: 0x06000801 RID: 2049 RVA: 0x00026464 File Offset: 0x00024664
			[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
			public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
			{
				CommandBuffer cmd = *renderingData.commandBuffer;
				UniversalShadowData shadowData = renderingData.frameData.Get<UniversalShadowData>();
				using (new ProfilingScope(cmd, base.profilingSampler))
				{
					ScreenSpaceShadows.ScreenSpaceShadowsPostPass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer), shadowData);
				}
			}

			// Token: 0x06000802 RID: 2050 RVA: 0x000264C8 File Offset: 0x000246C8
			public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
			{
				ScreenSpaceShadows.ScreenSpaceShadowsPostPass.PassData passData;
				using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<ScreenSpaceShadows.ScreenSpaceShadowsPostPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/RendererFeatures/ScreenSpaceShadows.cs", 294))
				{
					TextureHandle color = frameData.Get<UniversalResourceData>().activeColorTexture;
					builder.SetRenderAttachment(color, 0, AccessFlags.Write);
					passData.shadowData = frameData.Get<UniversalShadowData>();
					passData.pass = this;
					builder.AllowGlobalStateModification(true);
					builder.SetRenderFunc<ScreenSpaceShadows.ScreenSpaceShadowsPostPass.PassData>(delegate(ScreenSpaceShadows.ScreenSpaceShadowsPostPass.PassData data, RasterGraphContext rgContext)
					{
						ScreenSpaceShadows.ScreenSpaceShadowsPostPass.ExecutePass(rgContext.cmd, data.shadowData);
					});
				}
			}

			// Token: 0x040008A9 RID: 2217
			private static readonly RTHandle k_CurrentActive = RTHandles.Alloc(BuiltinRenderTextureType.CurrentActive);

			// Token: 0x0200017E RID: 382
			internal class PassData
			{
				// Token: 0x040008AA RID: 2218
				internal ScreenSpaceShadows.ScreenSpaceShadowsPostPass pass;

				// Token: 0x040008AB RID: 2219
				internal UniversalShadowData shadowData;
			}
		}
	}
}
