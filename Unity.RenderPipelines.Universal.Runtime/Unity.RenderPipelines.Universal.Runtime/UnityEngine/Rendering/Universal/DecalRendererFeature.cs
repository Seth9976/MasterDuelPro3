using System;
using System.Diagnostics;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000169 RID: 361
	[SupportedOnRenderer(typeof(UniversalRendererData))]
	[DisallowMultipleRendererFeature("Decal")]
	[Tooltip("With this Renderer Feature, Unity can project specific Materials (decals) onto other objects in the Scene.")]
	public class DecalRendererFeature : ScriptableRendererFeature
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0002529D File Offset: 0x0002349D
		private static SharedDecalEntityManager sharedDecalEntityManager { get; } = new SharedDecalEntityManager();

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x000252A4 File Offset: 0x000234A4
		internal ref DecalSettings settings
		{
			get
			{
				return ref this.m_Settings;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x000252AC File Offset: 0x000234AC
		internal bool intermediateRendering
		{
			get
			{
				return this.m_Technique == DecalTechnique.DBuffer;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x000252B7 File Offset: 0x000234B7
		internal bool requiresDecalLayers
		{
			get
			{
				return this.m_Settings.decalLayers;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x000252C4 File Offset: 0x000234C4
		internal static bool isGLDevice
		{
			get
			{
				return SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3 || SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLCore;
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000252DA File Offset: 0x000234DA
		public override void Create()
		{
			this.m_DecalPreviewPass = new DecalPreviewPass();
			this.m_RecreateSystems = true;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x000252F0 File Offset: 0x000234F0
		internal override bool RequireRenderingLayers(bool isDeferred, bool needsGBufferAccurateNormals, out RenderingLayerUtils.Event atEvent, out RenderingLayerUtils.MaskSize maskSize)
		{
			bool checkForInvalidTechniques = Application.isPlaying;
			DecalTechnique technique = this.GetTechnique(isDeferred, needsGBufferAccurateNormals, checkForInvalidTechniques);
			atEvent = ((technique == DecalTechnique.DBuffer) ? RenderingLayerUtils.Event.DepthNormalPrePass : RenderingLayerUtils.Event.Opaque);
			maskSize = RenderingLayerUtils.MaskSize.Bits8;
			return this.requiresDecalLayers;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00025321 File Offset: 0x00023521
		internal DBufferSettings GetDBufferSettings()
		{
			if (this.m_Settings.technique == DecalTechniqueOption.Automatic)
			{
				return new DBufferSettings
				{
					surfaceData = DecalSurfaceData.AlbedoNormalMAOS
				};
			}
			return this.m_Settings.dBufferSettings;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00025348 File Offset: 0x00023548
		internal DecalScreenSpaceSettings GetScreenSpaceSettings()
		{
			if (this.m_Settings.technique == DecalTechniqueOption.Automatic)
			{
				return new DecalScreenSpaceSettings
				{
					normalBlend = DecalNormalBlend.Low
				};
			}
			return this.m_Settings.screenSpaceSettings;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00025370 File Offset: 0x00023570
		internal DecalTechnique GetTechnique(ScriptableRendererData renderer)
		{
			UniversalRendererData universalRenderer = renderer as UniversalRendererData;
			if (universalRenderer == null)
			{
				Debug.LogError("Only universal renderer supports Decal renderer feature.");
				return DecalTechnique.Invalid;
			}
			bool isDeferred = universalRenderer.renderingMode == RenderingMode.Deferred;
			return this.GetTechnique(isDeferred, universalRenderer.accurateGbufferNormals, true);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x000253B4 File Offset: 0x000235B4
		internal DecalTechnique GetTechnique(ScriptableRenderer renderer)
		{
			UniversalRenderer universalRenderer = renderer as UniversalRenderer;
			if (universalRenderer == null)
			{
				Debug.LogError("Only universal renderer supports Decal renderer feature.");
				return DecalTechnique.Invalid;
			}
			bool isDeferred = universalRenderer.renderingModeActual == RenderingMode.Deferred;
			return this.GetTechnique(isDeferred, universalRenderer.accurateGbufferNormals, true);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000253F0 File Offset: 0x000235F0
		internal DecalTechnique GetTechnique(bool isDeferred, bool needsGBufferAccurateNormals, bool checkForInvalidTechniques = true)
		{
			DecalTechnique technique = DecalTechnique.Invalid;
			switch (this.m_Settings.technique)
			{
			case DecalTechniqueOption.Automatic:
				if (this.IsAutomaticDBuffer() || (isDeferred && needsGBufferAccurateNormals))
				{
					technique = DecalTechnique.DBuffer;
				}
				else if (isDeferred)
				{
					technique = DecalTechnique.GBuffer;
				}
				else
				{
					technique = DecalTechnique.ScreenSpace;
				}
				break;
			case DecalTechniqueOption.DBuffer:
				technique = DecalTechnique.DBuffer;
				break;
			case DecalTechniqueOption.ScreenSpace:
				if (isDeferred)
				{
					technique = DecalTechnique.GBuffer;
				}
				else
				{
					technique = DecalTechnique.ScreenSpace;
				}
				break;
			}
			if (!checkForInvalidTechniques)
			{
				return technique;
			}
			if (technique == DecalTechnique.DBuffer && DecalRendererFeature.isGLDevice)
			{
				Debug.LogError("Decal DBuffer technique is not supported with OpenGL.");
				return DecalTechnique.Invalid;
			}
			bool mrt4 = SystemInfo.supportedRenderTargetCount >= 4;
			if (technique == DecalTechnique.DBuffer && !mrt4)
			{
				Debug.LogError("Decal DBuffer technique requires MRT4 support.");
				return DecalTechnique.Invalid;
			}
			if (technique == DecalTechnique.GBuffer && !mrt4)
			{
				Debug.LogError("Decal useGBuffer option requires MRT4 support.");
				return DecalTechnique.Invalid;
			}
			return technique;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00025497 File Offset: 0x00023697
		private bool IsAutomaticDBuffer()
		{
			return Application.platform != RuntimePlatform.WebGLPlayer && !PlatformAutoDetect.isShaderAPIMobileDefined;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000254AC File Offset: 0x000236AC
		private bool RecreateSystemsIfNeeded(ScriptableRenderer renderer, in CameraData cameraData)
		{
			if (!this.m_RecreateSystems)
			{
				return true;
			}
			this.m_Technique = this.GetTechnique(renderer);
			if (this.m_Technique == DecalTechnique.Invalid)
			{
				return false;
			}
			this.m_DBufferSettings = this.GetDBufferSettings();
			this.m_ScreenSpaceSettings = this.GetScreenSpaceSettings();
			UniversalRendererResources rendererShaders = GraphicsSettings.GetRenderPipelineSettings<UniversalRendererResources>();
			if (rendererShaders == null)
			{
				return false;
			}
			this.m_DBufferClearMaterial = CoreUtils.CreateEngineMaterial(rendererShaders.decalDBufferClear);
			if (this.m_DecalEntityManager == null)
			{
				this.m_DecalEntityManager = DecalRendererFeature.sharedDecalEntityManager.Get();
			}
			this.m_DecalUpdateCachedSystem = new DecalUpdateCachedSystem(this.m_DecalEntityManager);
			this.m_DecalUpdateCulledSystem = new DecalUpdateCulledSystem(this.m_DecalEntityManager);
			this.m_DecalCreateDrawCallSystem = new DecalCreateDrawCallSystem(this.m_DecalEntityManager, this.m_Settings.maxDrawDistance);
			if (this.intermediateRendering)
			{
				this.m_DecalUpdateCullingGroupSystem = new DecalUpdateCullingGroupSystem(this.m_DecalEntityManager, this.m_Settings.maxDrawDistance);
			}
			else
			{
				this.m_DecalSkipCulledSystem = new DecalSkipCulledSystem(this.m_DecalEntityManager);
			}
			this.m_DrawErrorSystem = new DecalDrawErrorSystem(this.m_DecalEntityManager, this.m_Technique);
			UniversalRenderer universalRenderer = renderer as UniversalRenderer;
			switch (this.m_Technique)
			{
			case DecalTechnique.DBuffer:
				this.m_CopyDepthPass = new DBufferCopyDepthPass((RenderPassEvent)201, rendererShaders.copyDepthPS, false, universalRenderer.renderingModeActual != RenderingMode.Deferred, false);
				this.m_DecalDrawDBufferSystem = new DecalDrawDBufferSystem(this.m_DecalEntityManager);
				this.m_DBufferRenderPass = new DBufferRenderPass(this.m_DBufferClearMaterial, this.m_DBufferSettings, this.m_DecalDrawDBufferSystem, this.m_Settings.decalLayers);
				this.m_DecalDrawForwardEmissiveSystem = new DecalDrawFowardEmissiveSystem(this.m_DecalEntityManager);
				this.m_ForwardEmissivePass = new DecalForwardEmissivePass(this.m_DecalDrawForwardEmissiveSystem);
				break;
			case DecalTechnique.ScreenSpace:
				this.m_DecalDrawScreenSpaceSystem = new DecalDrawScreenSpaceSystem(this.m_DecalEntityManager);
				this.m_ScreenSpaceDecalRenderPass = new DecalScreenSpaceRenderPass(this.m_ScreenSpaceSettings, this.intermediateRendering ? this.m_DecalDrawScreenSpaceSystem : null, this.m_Settings.decalLayers);
				break;
			case DecalTechnique.GBuffer:
				this.m_DeferredLights = universalRenderer.deferredLights;
				this.m_DrawGBufferSystem = new DecalDrawGBufferSystem(this.m_DecalEntityManager);
				this.m_GBufferRenderPass = new DecalGBufferRenderPass(this.m_ScreenSpaceSettings, this.intermediateRendering ? this.m_DrawGBufferSystem : null, this.m_Settings.decalLayers);
				break;
			}
			this.m_RecreateSystems = false;
			return true;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x000256F0 File Offset: 0x000238F0
		public unsafe override void OnCameraPreCull(ScriptableRenderer renderer, in CameraData cameraData)
		{
			CameraData cameraData2 = cameraData;
			if (*cameraData2.cameraType == CameraType.Preview)
			{
				return;
			}
			if (!this.RecreateSystemsIfNeeded(renderer, in cameraData))
			{
				return;
			}
			this.m_DecalEntityManager.Update();
			this.m_DecalUpdateCachedSystem.Execute();
			if (this.intermediateRendering)
			{
				DecalUpdateCullingGroupSystem decalUpdateCullingGroupSystem = this.m_DecalUpdateCullingGroupSystem;
				cameraData2 = cameraData;
				decalUpdateCullingGroupSystem.Execute(*cameraData2.camera);
			}
			else
			{
				DecalSkipCulledSystem decalSkipCulledSystem = this.m_DecalSkipCulledSystem;
				cameraData2 = cameraData;
				decalSkipCulledSystem.Execute(*cameraData2.camera);
				this.m_DecalCreateDrawCallSystem.Execute();
				if (this.m_Technique == DecalTechnique.ScreenSpace)
				{
					this.m_DecalDrawScreenSpaceSystem.Execute(in cameraData);
				}
				else if (this.m_Technique == DecalTechnique.GBuffer)
				{
					this.m_DrawGBufferSystem.Execute(in cameraData);
				}
			}
			this.m_DrawErrorSystem.Execute(in cameraData);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x000257B4 File Offset: 0x000239B4
		public unsafe override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			if (UniversalRenderer.IsOffscreenDepthTexture(ref renderingData.cameraData))
			{
				return;
			}
			if (*renderingData.cameraData.cameraType == CameraType.Preview)
			{
				renderer.EnqueuePass(this.m_DecalPreviewPass);
				return;
			}
			if (!this.RecreateSystemsIfNeeded(renderer, in renderingData.cameraData))
			{
				return;
			}
			if (this.intermediateRendering)
			{
				this.m_DecalUpdateCulledSystem.Execute();
				this.m_DecalCreateDrawCallSystem.Execute();
			}
			if (this.m_Technique == DecalTechnique.DBuffer)
			{
				if ((renderer as UniversalRenderer).renderingModeActual == RenderingMode.Deferred)
				{
					this.m_CopyDepthPass.CopyToDepth = false;
				}
				else
				{
					this.m_CopyDepthPass.CopyToDepth = true;
					this.m_CopyDepthPass.MssaSamples = 1;
				}
			}
			switch (this.m_Technique)
			{
			case DecalTechnique.DBuffer:
				renderer.EnqueuePass(this.m_CopyDepthPass);
				renderer.EnqueuePass(this.m_DBufferRenderPass);
				renderer.EnqueuePass(this.m_ForwardEmissivePass);
				return;
			case DecalTechnique.ScreenSpace:
				renderer.EnqueuePass(this.m_ScreenSpaceDecalRenderPass);
				return;
			case DecalTechnique.GBuffer:
				this.m_GBufferRenderPass.Setup(this.m_DeferredLights);
				renderer.EnqueuePass(this.m_GBufferRenderPass);
				return;
			default:
				return;
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x000258C1 File Offset: 0x00023AC1
		internal override bool SupportsNativeRenderPass()
		{
			return this.m_Technique == DecalTechnique.GBuffer || this.m_Technique == DecalTechnique.ScreenSpace;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000258D8 File Offset: 0x00023AD8
		public unsafe override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
		{
			if (renderer.cameraColorTargetHandle == null)
			{
				return;
			}
			if (this.m_Technique != DecalTechnique.DBuffer)
			{
				if (this.m_Technique == DecalTechnique.GBuffer && this.m_DeferredLights.UseFramebufferFetch)
				{
					ScriptableRenderPass gbufferRenderPass = this.m_GBufferRenderPass;
					CommandBuffer commandBuffer = null;
					CameraData cameraData = renderingData.cameraData;
					gbufferRenderPass.Configure(commandBuffer, *cameraData.cameraTargetDescriptor);
				}
				return;
			}
			this.m_DBufferRenderPass.Setup(in renderingData.cameraData);
			UniversalRenderer universalRenderer = renderer as UniversalRenderer;
			if (universalRenderer.renderingModeActual == RenderingMode.Deferred)
			{
				this.m_DBufferRenderPass.Setup(in renderingData.cameraData, renderer.cameraDepthTargetHandle);
				this.m_CopyDepthPass.Setup(renderer.cameraDepthTargetHandle, universalRenderer.m_DepthTexture);
				return;
			}
			this.m_DBufferRenderPass.Setup(in renderingData.cameraData);
			this.m_CopyDepthPass.Setup(universalRenderer.m_DepthTexture, this.m_DBufferRenderPass.dBufferDepth);
			this.m_CopyDepthPass.CopyToDepth = true;
			this.m_CopyDepthPass.MssaSamples = 1;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000259C8 File Offset: 0x00023BC8
		protected override void Dispose(bool disposing)
		{
			DBufferRenderPass dbufferRenderPass = this.m_DBufferRenderPass;
			if (dbufferRenderPass != null)
			{
				dbufferRenderPass.Dispose();
			}
			DBufferCopyDepthPass copyDepthPass = this.m_CopyDepthPass;
			if (copyDepthPass != null)
			{
				copyDepthPass.Dispose();
			}
			CoreUtils.Destroy(this.m_DBufferClearMaterial);
			if (this.m_DecalEntityManager != null)
			{
				this.m_DecalEntityManager = null;
				DecalRendererFeature.sharedDecalEntityManager.Release(this.m_DecalEntityManager);
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0000217F File Offset: 0x0000037F
		[Conditional("ADAPTIVE_PERFORMANCE_4_0_0_OR_NEWER")]
		private void ChangeAdaptivePerformanceDrawDistances()
		{
		}

		// Token: 0x04000836 RID: 2102
		[SerializeField]
		private DecalSettings m_Settings = new DecalSettings();

		// Token: 0x04000837 RID: 2103
		private DecalTechnique m_Technique;

		// Token: 0x04000838 RID: 2104
		private DBufferSettings m_DBufferSettings;

		// Token: 0x04000839 RID: 2105
		private DecalScreenSpaceSettings m_ScreenSpaceSettings;

		// Token: 0x0400083A RID: 2106
		private bool m_RecreateSystems;

		// Token: 0x0400083B RID: 2107
		private DecalPreviewPass m_DecalPreviewPass;

		// Token: 0x0400083C RID: 2108
		private DecalEntityManager m_DecalEntityManager;

		// Token: 0x0400083D RID: 2109
		private DecalUpdateCachedSystem m_DecalUpdateCachedSystem;

		// Token: 0x0400083E RID: 2110
		private DecalUpdateCullingGroupSystem m_DecalUpdateCullingGroupSystem;

		// Token: 0x0400083F RID: 2111
		private DecalUpdateCulledSystem m_DecalUpdateCulledSystem;

		// Token: 0x04000840 RID: 2112
		private DecalCreateDrawCallSystem m_DecalCreateDrawCallSystem;

		// Token: 0x04000841 RID: 2113
		private DecalDrawErrorSystem m_DrawErrorSystem;

		// Token: 0x04000842 RID: 2114
		private DBufferCopyDepthPass m_CopyDepthPass;

		// Token: 0x04000843 RID: 2115
		private DBufferRenderPass m_DBufferRenderPass;

		// Token: 0x04000844 RID: 2116
		private DecalForwardEmissivePass m_ForwardEmissivePass;

		// Token: 0x04000845 RID: 2117
		private DecalDrawDBufferSystem m_DecalDrawDBufferSystem;

		// Token: 0x04000846 RID: 2118
		private DecalDrawFowardEmissiveSystem m_DecalDrawForwardEmissiveSystem;

		// Token: 0x04000847 RID: 2119
		private Material m_DBufferClearMaterial;

		// Token: 0x04000848 RID: 2120
		private DecalScreenSpaceRenderPass m_ScreenSpaceDecalRenderPass;

		// Token: 0x04000849 RID: 2121
		private DecalDrawScreenSpaceSystem m_DecalDrawScreenSpaceSystem;

		// Token: 0x0400084A RID: 2122
		private DecalSkipCulledSystem m_DecalSkipCulledSystem;

		// Token: 0x0400084B RID: 2123
		private DecalGBufferRenderPass m_GBufferRenderPass;

		// Token: 0x0400084C RID: 2124
		private DecalDrawGBufferSystem m_DrawGBufferSystem;

		// Token: 0x0400084D RID: 2125
		private DeferredLights m_DeferredLights;
	}
}
