using System;
using Unity.Collections;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x020001EF RID: 495
	internal class DeferredLights
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00002886 File Offset: 0x00000A86
		internal int GBufferAlbedoIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x000039B4 File Offset: 0x00001BB4
		internal int GBufferSpecularMetallicIndex
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x000393AA File Offset: 0x000375AA
		internal int GBufferNormalSmoothnessIndex
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x000393AD File Offset: 0x000375AD
		internal int GBufferLightingIndex
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x000393B0 File Offset: 0x000375B0
		internal int GbufferDepthIndex
		{
			get
			{
				if (!this.UseFramebufferFetch)
				{
					return -1;
				}
				return this.GBufferLightingIndex + 1;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x000393C4 File Offset: 0x000375C4
		internal int GBufferRenderingLayers
		{
			get
			{
				if (!this.UseRenderingLayers)
				{
					return -1;
				}
				return this.GBufferLightingIndex + (this.UseFramebufferFetch ? 1 : 0) + 1;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x000393E5 File Offset: 0x000375E5
		internal int GBufferShadowMask
		{
			get
			{
				if (!this.UseShadowMask)
				{
					return -1;
				}
				return this.GBufferLightingIndex + (this.UseFramebufferFetch ? 1 : 0) + (this.UseRenderingLayers ? 1 : 0) + 1;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00039413 File Offset: 0x00037613
		internal int GBufferSliceCount
		{
			get
			{
				return 4 + (this.UseFramebufferFetch ? 1 : 0) + (this.UseShadowMask ? 1 : 0) + (this.UseRenderingLayers ? 1 : 0);
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0003943D File Offset: 0x0003763D
		internal int GBufferInputAttachmentCount
		{
			get
			{
				return 4 + (this.UseShadowMask ? 1 : 0);
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00039450 File Offset: 0x00037650
		internal GraphicsFormat GetGBufferFormat(int index)
		{
			if (index == this.GBufferAlbedoIndex)
			{
				if (QualitySettings.activeColorSpace != ColorSpace.Linear)
				{
					return GraphicsFormat.R8G8B8A8_UNorm;
				}
				return GraphicsFormat.R8G8B8A8_SRGB;
			}
			else
			{
				if (index == this.GBufferSpecularMetallicIndex)
				{
					return GraphicsFormat.R8G8B8A8_UNorm;
				}
				if (index == this.GBufferNormalSmoothnessIndex)
				{
					if (!this.AccurateGbufferNormals)
					{
						return DepthNormalOnlyPass.GetGraphicsFormat();
					}
					return GraphicsFormat.R8G8B8A8_UNorm;
				}
				else
				{
					if (index == this.GBufferLightingIndex)
					{
						return GraphicsFormat.None;
					}
					if (index == this.GbufferDepthIndex)
					{
						return GraphicsFormat.R32_SFloat;
					}
					if (index == this.GBufferShadowMask)
					{
						return GraphicsFormat.B8G8R8A8_UNorm;
					}
					if (index == this.GBufferRenderingLayers)
					{
						return RenderingLayerUtils.GetFormat(this.RenderingLayerMaskSize);
					}
					return GraphicsFormat.None;
				}
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x000394CF File Offset: 0x000376CF
		internal bool UseShadowMask
		{
			get
			{
				return this.MixedLightingSetup > MixedLightingSetup.None;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x000394DA File Offset: 0x000376DA
		internal bool UseRenderingLayers
		{
			get
			{
				return this.UseLightLayers || this.UseDecalLayers;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x000394EC File Offset: 0x000376EC
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x000394F4 File Offset: 0x000376F4
		internal RenderingLayerUtils.MaskSize RenderingLayerMaskSize { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x000394FD File Offset: 0x000376FD
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x00039505 File Offset: 0x00037705
		internal bool UseDecalLayers { get; set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0003950E File Offset: 0x0003770E
		internal bool UseLightLayers
		{
			get
			{
				return UniversalRenderPipeline.asset.useRenderingLayers;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0003951A File Offset: 0x0003771A
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x00039522 File Offset: 0x00037722
		internal bool UseFramebufferFetch { get; set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0003952B File Offset: 0x0003772B
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x00039533 File Offset: 0x00037733
		internal bool HasDepthPrepass { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0003953C File Offset: 0x0003773C
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x00039544 File Offset: 0x00037744
		internal bool HasNormalPrepass { get; set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0003954D File Offset: 0x0003774D
		// (set) Token: 0x06000B04 RID: 2820 RVA: 0x00039555 File Offset: 0x00037755
		internal bool HasRenderingLayerPrepass { get; set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0003955E File Offset: 0x0003775E
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x00039566 File Offset: 0x00037766
		internal bool IsOverlay { get; set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0003956F File Offset: 0x0003776F
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x00039577 File Offset: 0x00037777
		internal bool AccurateGbufferNormals { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00039580 File Offset: 0x00037780
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x00039588 File Offset: 0x00037788
		internal MixedLightingSetup MixedLightingSetup { get; set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00039591 File Offset: 0x00037791
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x00039599 File Offset: 0x00037799
		internal bool UseJobSystem { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x000395A2 File Offset: 0x000377A2
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x000395AA File Offset: 0x000377AA
		internal int RenderWidth { get; set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x000395B3 File Offset: 0x000377B3
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x000395BB File Offset: 0x000377BB
		internal int RenderHeight { get; set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x000395C4 File Offset: 0x000377C4
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x000395CC File Offset: 0x000377CC
		internal RTHandle[] GbufferAttachments { get; set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x000395D5 File Offset: 0x000377D5
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x000395DD File Offset: 0x000377DD
		internal TextureHandle[] GbufferTextureHandles { get; set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x000395E6 File Offset: 0x000377E6
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x000395EE File Offset: 0x000377EE
		internal RTHandle[] DeferredInputAttachments { get; set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x000395F7 File Offset: 0x000377F7
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x000395FF File Offset: 0x000377FF
		internal bool[] DeferredInputIsTransient { get; set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00039608 File Offset: 0x00037808
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x00039610 File Offset: 0x00037810
		internal RTHandle DepthAttachment { get; set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00039619 File Offset: 0x00037819
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x00039621 File Offset: 0x00037821
		internal RTHandle DepthCopyTexture { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0003962A File Offset: 0x0003782A
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x00039632 File Offset: 0x00037832
		internal GraphicsFormat[] GbufferFormats { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0003963B File Offset: 0x0003783B
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x00039643 File Offset: 0x00037843
		internal RTHandle DepthAttachmentHandle { get; set; }

		// Token: 0x06000B21 RID: 2849 RVA: 0x0003964C File Offset: 0x0003784C
		internal DeferredLights(DeferredLights.InitParams initParams, bool useNativeRenderPass = false)
		{
			DeferredConfig.IsOpenGL = SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLCore || SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3;
			DeferredConfig.IsDX10 = SystemInfo.graphicsDeviceType == GraphicsDeviceType.Direct3D11 && SystemInfo.graphicsShaderLevel <= 40;
			this.m_StencilDeferredMaterial = initParams.stencilDeferredMaterial;
			this.m_StencilDeferredPasses = new int[DeferredLights.k_StencilDeferredPassNames.Length];
			this.InitStencilDeferredMaterial();
			this.AccurateGbufferNormals = true;
			this.UseJobSystem = true;
			this.UseFramebufferFetch = useNativeRenderPass;
			this.m_LightCookieManager = initParams.lightCookieManager;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00039718 File Offset: 0x00037918
		internal void SetupRenderGraphLights(RenderGraph renderGraph, UniversalCameraData cameraData, UniversalLightData lightData)
		{
			DeferredLights.SetupLightPassData passData;
			using (IUnsafeRenderGraphBuilder builder = renderGraph.AddUnsafePass<DeferredLights.SetupLightPassData>(DeferredLights.s_SetupDeferredLights.name, out passData, DeferredLights.s_SetupDeferredLights, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/DeferredLights.cs", 293))
			{
				passData.cameraData = cameraData;
				passData.cameraTargetSizeCopy = new Vector2Int(cameraData.cameraTargetDescriptor.width, cameraData.cameraTargetDescriptor.height);
				passData.lightData = lightData;
				passData.deferredLights = this;
				builder.AllowPassCulling(false);
				builder.SetRenderFunc<DeferredLights.SetupLightPassData>(delegate(DeferredLights.SetupLightPassData data, UnsafeGraphContext rgContext)
				{
					data.deferredLights.SetupLights(CommandBufferHelpers.GetNativeCommandBuffer(rgContext.cmd), data.cameraData, data.cameraTargetSizeCopy, data.lightData, true);
				});
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x000397C8 File Offset: 0x000379C8
		internal void SetupLights(CommandBuffer cmd, UniversalCameraData cameraData, Vector2Int cameraTargetSizeCopy, UniversalLightData lightData, bool isRenderGraph = false)
		{
			Camera camera = cameraData.camera;
			this.RenderWidth = (camera.allowDynamicResolution ? Mathf.CeilToInt(ScalableBufferManager.widthScaleFactor * (float)cameraTargetSizeCopy.x) : cameraTargetSizeCopy.x);
			this.RenderHeight = (camera.allowDynamicResolution ? Mathf.CeilToInt(ScalableBufferManager.heightScaleFactor * (float)cameraTargetSizeCopy.y) : cameraTargetSizeCopy.y);
			this.PrecomputeLights(out this.m_stencilVisLights, out this.m_stencilVisLightOffsets, ref lightData.visibleLights, lightData.additionalLightsCount != 0 || lightData.mainLightIndex >= 0);
			using (new ProfilingScope(cmd, DeferredLights.m_ProfilingSetupLightConstants))
			{
				this.SetupShaderLightConstants(cmd, lightData);
				bool supportsMixedLighting = lightData.supportsMixedLighting;
				cmd.SetKeyword(in ShaderGlobalKeywords._GBUFFER_NORMALS_OCT, this.AccurateGbufferNormals);
				bool isShadowMask = supportsMixedLighting && this.MixedLightingSetup == MixedLightingSetup.ShadowMask;
				bool isShadowMaskAlways = isShadowMask && QualitySettings.shadowmaskMode == ShadowmaskMode.Shadowmask;
				bool isSubtractive = supportsMixedLighting && this.MixedLightingSetup == MixedLightingSetup.Subtractive;
				cmd.SetKeyword(in ShaderGlobalKeywords.LightmapShadowMixing, isSubtractive || isShadowMaskAlways);
				cmd.SetKeyword(in ShaderGlobalKeywords.ShadowsShadowMask, isShadowMask);
				cmd.SetKeyword(in ShaderGlobalKeywords.MixedLightingSubtractive, isSubtractive);
				cmd.SetKeyword(in ShaderGlobalKeywords.RenderPassEnabled, this.UseFramebufferFetch && (cameraData.cameraType == CameraType.Game || camera.cameraType == CameraType.SceneView || isRenderGraph));
				cmd.SetKeyword(in ShaderGlobalKeywords.LightLayers, this.UseLightLayers && !CoreUtils.IsSceneLightingDisabled(camera));
				RenderingLayerUtils.SetupProperties(cmd, this.RenderingLayerMaskSize);
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00039960 File Offset: 0x00037B60
		internal void ResolveMixedLightingMode(UniversalLightData lightData)
		{
			this.MixedLightingSetup = MixedLightingSetup.None;
			if (lightData.supportsMixedLighting)
			{
				NativeArray<VisibleLight> visibleLights = lightData.visibleLights;
				int lightIndex = 0;
				while (lightIndex < lightData.visibleLights.Length && this.MixedLightingSetup == MixedLightingSetup.None)
				{
					Light light = visibleLights.UnsafeElementAtMutable(lightIndex).light;
					if (light != null && light.bakingOutput.lightmapBakeType == LightmapBakeType.Mixed && light.shadows != LightShadows.None)
					{
						MixedLightingMode mixedLightingMode = light.bakingOutput.mixedLightingMode;
						if (mixedLightingMode != MixedLightingMode.Subtractive)
						{
							if (mixedLightingMode == MixedLightingMode.Shadowmask)
							{
								this.MixedLightingSetup = MixedLightingSetup.ShadowMask;
							}
						}
						else
						{
							this.MixedLightingSetup = MixedLightingSetup.Subtractive;
						}
					}
					lightIndex++;
				}
			}
			this.CreateGbufferResources();
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x000399F9 File Offset: 0x00037BF9
		internal void DisableFramebufferFetchInput()
		{
			this.UseFramebufferFetch = false;
			this.CreateGbufferResources();
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00039A08 File Offset: 0x00037C08
		internal void ReleaseGbufferResources()
		{
			if (this.GbufferRTHandles != null)
			{
				for (int i = 0; i < this.GbufferRTHandles.Length; i++)
				{
					if (i != this.GBufferLightingIndex)
					{
						this.GbufferRTHandles[i].Release();
						this.GbufferAttachments[i].Release();
					}
				}
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00039A54 File Offset: 0x00037C54
		internal void ReAllocateGBufferIfNeeded(RenderTextureDescriptor gbufferSlice, int gbufferIndex)
		{
			if (this.GbufferRTHandles != null)
			{
				if (this.GbufferRTHandles[gbufferIndex].GetInstanceID() != this.GbufferAttachments[gbufferIndex].GetInstanceID())
				{
					return;
				}
				gbufferSlice.depthStencilFormat = GraphicsFormat.None;
				gbufferSlice.stencilFormat = GraphicsFormat.None;
				gbufferSlice.graphicsFormat = this.GetGBufferFormat(gbufferIndex);
				RenderingUtils.ReAllocateHandleIfNeeded(ref this.GbufferRTHandles[gbufferIndex], in gbufferSlice, FilterMode.Point, TextureWrapMode.Clamp, 1, 0f, DeferredLights.k_GBufferNames[gbufferIndex]);
				this.GbufferAttachments[gbufferIndex] = this.GbufferRTHandles[gbufferIndex];
			}
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00039AD8 File Offset: 0x00037CD8
		internal void CreateGbufferResources()
		{
			int gbufferSliceCount = this.GBufferSliceCount;
			if (this.GbufferRTHandles == null || this.GbufferRTHandles.Length != gbufferSliceCount)
			{
				this.ReleaseGbufferResources();
				this.GbufferAttachments = new RTHandle[gbufferSliceCount];
				this.GbufferRTHandles = new RTHandle[gbufferSliceCount];
				this.GbufferFormats = new GraphicsFormat[gbufferSliceCount];
				this.GbufferTextureHandles = new TextureHandle[gbufferSliceCount];
				for (int i = 0; i < gbufferSliceCount; i++)
				{
					this.GbufferRTHandles[i] = RTHandles.Alloc(DeferredLights.k_GBufferNames[i], DeferredLights.k_GBufferNames[i]);
					this.GbufferAttachments[i] = this.GbufferRTHandles[i];
					this.GbufferFormats[i] = this.GetGBufferFormat(i);
				}
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00039B84 File Offset: 0x00037D84
		internal void UpdateDeferredInputAttachments()
		{
			this.DeferredInputAttachments[0] = this.GbufferAttachments[0];
			this.DeferredInputAttachments[1] = this.GbufferAttachments[1];
			this.DeferredInputAttachments[2] = this.GbufferAttachments[2];
			this.DeferredInputAttachments[3] = this.GbufferAttachments[4];
			if (this.UseShadowMask && this.UseRenderingLayers)
			{
				this.DeferredInputAttachments[4] = this.GbufferAttachments[this.GBufferShadowMask];
				this.DeferredInputAttachments[5] = this.GbufferAttachments[this.GBufferRenderingLayers];
				return;
			}
			if (this.UseShadowMask)
			{
				this.DeferredInputAttachments[4] = this.GbufferAttachments[this.GBufferShadowMask];
				return;
			}
			if (this.UseRenderingLayers)
			{
				this.DeferredInputAttachments[4] = this.GbufferAttachments[this.GBufferRenderingLayers];
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00039C47 File Offset: 0x00037E47
		internal bool IsRuntimeSupportedThisFrame()
		{
			return this.GBufferSliceCount <= SystemInfo.supportedRenderTargetCount && !DeferredConfig.IsOpenGL && !DeferredConfig.IsDX10;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00039C68 File Offset: 0x00037E68
		public void Setup(AdditionalLightsShadowCasterPass additionalLightsShadowCasterPass, bool hasDepthPrepass, bool hasNormalPrepass, bool hasRenderingLayerPrepass, RTHandle depthCopyTexture, RTHandle depthAttachment, RTHandle colorAttachment)
		{
			this.m_AdditionalLightsShadowCasterPass = additionalLightsShadowCasterPass;
			this.HasDepthPrepass = hasDepthPrepass;
			this.HasNormalPrepass = hasNormalPrepass;
			this.HasRenderingLayerPrepass = hasRenderingLayerPrepass;
			this.DepthCopyTexture = depthCopyTexture;
			this.GbufferAttachments[this.GBufferLightingIndex] = colorAttachment;
			this.DepthAttachment = depthAttachment;
			int inputCount = 4 + (this.UseShadowMask ? 1 : 0) + (this.UseRenderingLayers ? 1 : 0);
			if ((this.DeferredInputAttachments == null && this.UseFramebufferFetch && this.GbufferAttachments.Length >= 3) || (this.DeferredInputAttachments != null && inputCount != this.DeferredInputAttachments.Length))
			{
				this.DeferredInputAttachments = new RTHandle[inputCount];
				this.DeferredInputIsTransient = new bool[inputCount];
				int i = 0;
				int j = 0;
				while (j < inputCount)
				{
					if (i == this.GBufferLightingIndex)
					{
						i++;
					}
					this.DeferredInputAttachments[j] = this.GbufferAttachments[i];
					this.DeferredInputIsTransient[j] = i != this.GbufferDepthIndex;
					j++;
					i++;
				}
			}
			this.DepthAttachmentHandle = this.DepthAttachment;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00039D62 File Offset: 0x00037F62
		internal void Setup(AdditionalLightsShadowCasterPass additionalLightsShadowCasterPass)
		{
			this.m_AdditionalLightsShadowCasterPass = additionalLightsShadowCasterPass;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00039D6B File Offset: 0x00037F6B
		public void OnCameraCleanup(CommandBuffer cmd)
		{
			cmd.SetKeyword(in ShaderGlobalKeywords._GBUFFER_NORMALS_OCT, false);
			if (this.m_stencilVisLights.IsCreated)
			{
				this.m_stencilVisLights.Dispose();
			}
			if (this.m_stencilVisLightOffsets.IsCreated)
			{
				this.m_stencilVisLightOffsets.Dispose();
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00039DAC File Offset: 0x00037FAC
		internal static StencilState OverwriteStencil(StencilState s, int stencilWriteMask)
		{
			if (!s.enabled)
			{
				return new StencilState(true, 0, (byte)stencilWriteMask, CompareFunction.Always, StencilOp.Replace, StencilOp.Keep, StencilOp.Keep, CompareFunction.Always, StencilOp.Replace, StencilOp.Keep, StencilOp.Keep);
			}
			CompareFunction funcFront = ((s.compareFunctionFront != CompareFunction.Disabled) ? s.compareFunctionFront : CompareFunction.Always);
			CompareFunction funcBack = ((s.compareFunctionBack != CompareFunction.Disabled) ? s.compareFunctionBack : CompareFunction.Always);
			StencilOp passFront = s.passOperationFront;
			StencilOp failFront = s.failOperationFront;
			StencilOp zfailFront = s.zFailOperationFront;
			StencilOp passBack = s.passOperationBack;
			StencilOp failBack = s.failOperationBack;
			StencilOp zfailBack = s.zFailOperationBack;
			return new StencilState(true, s.readMask & 15, (byte)((int)s.writeMask | stencilWriteMask), funcFront, passFront, failFront, zfailFront, funcBack, passBack, failBack, zfailBack);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00039E58 File Offset: 0x00038058
		internal static RenderStateBlock OverwriteStencil(RenderStateBlock block, int stencilWriteMask, int stencilRef)
		{
			if (!block.stencilState.enabled)
			{
				block.stencilState = new StencilState(true, 0, (byte)stencilWriteMask, CompareFunction.Always, StencilOp.Replace, StencilOp.Keep, StencilOp.Keep, CompareFunction.Always, StencilOp.Replace, StencilOp.Keep, StencilOp.Keep);
			}
			else
			{
				StencilState s = block.stencilState;
				CompareFunction funcFront = ((s.compareFunctionFront != CompareFunction.Disabled) ? s.compareFunctionFront : CompareFunction.Always);
				CompareFunction funcBack = ((s.compareFunctionBack != CompareFunction.Disabled) ? s.compareFunctionBack : CompareFunction.Always);
				StencilOp passFront = s.passOperationFront;
				StencilOp failFront = s.failOperationFront;
				StencilOp zfailFront = s.zFailOperationFront;
				StencilOp passBack = s.passOperationBack;
				StencilOp failBack = s.failOperationBack;
				StencilOp zfailBack = s.zFailOperationBack;
				block.stencilState = new StencilState(true, s.readMask & 15, (byte)((int)s.writeMask | stencilWriteMask), funcFront, passFront, failFront, zfailFront, funcBack, passBack, failBack, zfailBack);
			}
			block.mask |= RenderStateMask.Stencil;
			block.stencilReference = (block.stencilReference & 15) | stencilRef;
			return block;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00039F4C File Offset: 0x0003814C
		internal void ClearStencilPartial(RasterCommandBuffer cmd)
		{
			if (this.m_FullscreenMesh == null)
			{
				this.m_FullscreenMesh = DeferredLights.CreateFullscreenMesh();
			}
			using (new ProfilingScope(cmd, this.m_ProfilingSamplerClearStencilPartialPass))
			{
				cmd.DrawMesh(this.m_FullscreenMesh, Matrix4x4.identity, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[5]);
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00039FC0 File Offset: 0x000381C0
		internal void ExecuteDeferredPass(RasterCommandBuffer cmd, UniversalCameraData cameraData, UniversalLightData lightData, UniversalShadowData shadowData)
		{
			if (this.m_StencilDeferredPasses[0] < 0)
			{
				this.InitStencilDeferredMaterial();
			}
			if (!this.UseFramebufferFetch)
			{
				for (int i = 0; i < this.GbufferTextureHandles.Length; i++)
				{
					if (i != this.GBufferLightingIndex)
					{
						this.m_StencilDeferredMaterial.SetTexture(DeferredLights.k_GBufferShaderPropertyIDs[i], this.GbufferTextureHandles[i]);
					}
				}
			}
			using (new ProfilingScope(cmd, DeferredLights.m_ProfilingDeferredPass))
			{
				cmd.SetKeyword(in ShaderGlobalKeywords._DEFERRED_MIXED_LIGHTING, this.UseShadowMask);
				this.SetupMatrixConstants(cmd, cameraData);
				if (!this.HasStencilLightsOfType(LightType.Directional))
				{
					this.RenderSSAOBeforeShading(cmd);
				}
				this.RenderStencilLights(cmd, lightData, shadowData, cameraData.renderer.stripShadowsOffVariants);
				cmd.SetKeyword(in ShaderGlobalKeywords._DEFERRED_MIXED_LIGHTING, false);
				this.RenderFog(cmd, cameraData.camera.orthographic);
			}
			cmd.SetKeyword(in ShaderGlobalKeywords.AdditionalLightShadows, shadowData.isKeywordAdditionalLightShadowsEnabled);
			ShadowUtils.SetSoftShadowQualityShaderKeywords(cmd, shadowData);
			cmd.SetKeyword(in ShaderGlobalKeywords.LightCookies, this.m_LightCookieManager != null && this.m_LightCookieManager.IsKeywordLightCookieEnabled);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0003A0EC File Offset: 0x000382EC
		private void SetupShaderLightConstants(CommandBuffer cmd, UniversalLightData lightData)
		{
			this.SetupMainLightConstants(cmd, lightData);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0003A0F8 File Offset: 0x000382F8
		private void SetupMainLightConstants(CommandBuffer cmd, UniversalLightData lightData)
		{
			if (lightData.mainLightIndex < 0)
			{
				return;
			}
			Vector4 lightPos;
			Vector4 lightColor;
			Vector4 lightAttenuation;
			Vector4 lightSpotDir;
			Vector4 lightOcclusionChannel;
			UniversalRenderPipeline.InitializeLightConstants_Common(lightData.visibleLights, lightData.mainLightIndex, out lightPos, out lightColor, out lightAttenuation, out lightSpotDir, out lightOcclusionChannel);
			if (lightData.supportsLightLayers)
			{
				Light light = lightData.visibleLights[lightData.mainLightIndex].light;
				this.SetRenderingLayersMask(CommandBufferHelpers.GetRasterCommandBuffer(cmd), light, DeferredLights.ShaderConstants._MainLightLayerMask);
			}
			cmd.SetGlobalVector(DeferredLights.ShaderConstants._MainLightPosition, lightPos);
			cmd.SetGlobalVector(DeferredLights.ShaderConstants._MainLightColor, lightColor);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0003A17C File Offset: 0x0003837C
		private void SetupMatrixConstants(RasterCommandBuffer cmd, UniversalCameraData cameraData)
		{
			int eyeCount = ((cameraData.xr.enabled && cameraData.xr.singlePassEnabled) ? 2 : 1);
			Matrix4x4[] screenToWorld = this.m_ScreenToWorld;
			for (int eyeIndex = 0; eyeIndex < eyeCount; eyeIndex++)
			{
				Matrix4x4 projectionMatrix = cameraData.GetProjectionMatrix(eyeIndex);
				Matrix4x4 view = cameraData.GetViewMatrix(eyeIndex);
				Matrix4x4 gpuProj = GL.GetGPUProjectionMatrix(projectionMatrix, false);
				Matrix4x4 toScreen = new Matrix4x4(new Vector4(0.5f * (float)this.RenderWidth, 0f, 0f, 0f), new Vector4(0f, 0.5f * (float)this.RenderHeight, 0f, 0f), new Vector4(0f, 0f, 1f, 0f), new Vector4(0.5f * (float)this.RenderWidth, 0.5f * (float)this.RenderHeight, 0f, 1f));
				Matrix4x4 zScaleBias = Matrix4x4.identity;
				if (DeferredConfig.IsOpenGL)
				{
					zScaleBias = new Matrix4x4(new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 0f, 0.5f, 0f), new Vector4(0f, 0f, 0.5f, 1f));
				}
				screenToWorld[eyeIndex] = Matrix4x4.Inverse(toScreen * zScaleBias * gpuProj * view);
			}
			cmd.SetGlobalMatrixArray(DeferredLights.ShaderConstants._ScreenToWorld, screenToWorld);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0003A30C File Offset: 0x0003850C
		private void PrecomputeLights(out NativeArray<ushort> stencilVisLights, out NativeArray<ushort> stencilVisLightOffsets, ref NativeArray<VisibleLight> visibleLights, bool hasAdditionalLights)
		{
			if (!hasAdditionalLights)
			{
				stencilVisLights = new NativeArray<ushort>(0, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
				stencilVisLightOffsets = new NativeArray<ushort>(8, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
				for (int i = 0; i < 8; i++)
				{
					stencilVisLightOffsets[i] = DeferredLights.k_InvalidLightOffset;
				}
				return;
			}
			NativeArray<int> stencilLightCounts = new NativeArray<int>(8, Allocator.Temp, NativeArrayOptions.ClearMemory);
			stencilVisLightOffsets = new NativeArray<ushort>(8, Allocator.Temp, NativeArrayOptions.ClearMemory);
			int visibleLightCount = visibleLights.Length;
			ushort visLightIndex = 0;
			while ((int)visLightIndex < visibleLightCount)
			{
				ref VisibleLight ptr = ref visibleLights.UnsafeElementAtMutable((int)visLightIndex);
				ref NativeArray<ushort> ptr2 = ref stencilVisLightOffsets;
				int num = (int)ptr.lightType;
				ushort num2 = ptr2[num] + 1;
				ptr2[num] = num2;
				visLightIndex += 1;
			}
			int totalStencilLightCount = (int)(stencilVisLightOffsets[0] + stencilVisLightOffsets[1] + stencilVisLightOffsets[2]);
			stencilVisLights = new NativeArray<ushort>(totalStencilLightCount, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			int j = 0;
			int soffset = 0;
			while (j < stencilVisLightOffsets.Length)
			{
				if (stencilVisLightOffsets[j] == 0)
				{
					stencilVisLightOffsets[j] = DeferredLights.k_InvalidLightOffset;
				}
				else
				{
					int c = (int)stencilVisLightOffsets[j];
					stencilVisLightOffsets[j] = (ushort)soffset;
					soffset += c;
				}
				j++;
			}
			ushort visLightIndex2 = 0;
			while ((int)visLightIndex2 < visibleLightCount)
			{
				ref VisibleLight vl = ref visibleLights.UnsafeElementAtMutable((int)visLightIndex2);
				if (vl.lightType == LightType.Spot || vl.lightType == LightType.Directional || vl.lightType == LightType.Point)
				{
					int num = (int)vl.lightType;
					int num3 = stencilLightCounts[num];
					stencilLightCounts[num] = num3 + 1;
					int k = num3;
					stencilVisLights[(int)stencilVisLightOffsets[(int)vl.lightType] + k] = visLightIndex2;
				}
				visLightIndex2 += 1;
			}
			stencilLightCounts.Dispose();
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0003A4A5 File Offset: 0x000386A5
		private bool HasStencilLightsOfType(LightType type)
		{
			return this.m_stencilVisLightOffsets[(int)type] != DeferredLights.k_InvalidLightOffset;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0003A4C0 File Offset: 0x000386C0
		private void RenderStencilLights(RasterCommandBuffer cmd, UniversalLightData lightData, UniversalShadowData shadowData, bool stripShadowsOffVariants)
		{
			if (this.m_stencilVisLights.Length == 0)
			{
				return;
			}
			if (this.m_StencilDeferredMaterial == null)
			{
				Debug.LogErrorFormat("Missing {0}. {1} render pass will not execute. Check for missing reference in the renderer resources.", new object[]
				{
					this.m_StencilDeferredMaterial,
					base.GetType().Name
				});
				return;
			}
			using (new ProfilingScope(cmd, this.m_ProfilingSamplerDeferredStencilPass))
			{
				NativeArray<VisibleLight> visibleLights = lightData.visibleLights;
				bool hasLightCookieManager = this.m_LightCookieManager != null;
				bool hasAdditionalLightPass = this.m_AdditionalLightsShadowCasterPass != null;
				if (this.HasStencilLightsOfType(LightType.Directional))
				{
					this.RenderStencilDirectionalLights(cmd, stripShadowsOffVariants, lightData, shadowData, visibleLights, hasAdditionalLightPass, hasLightCookieManager, lightData.mainLightIndex);
				}
				if (lightData.supportsAdditionalLights)
				{
					if (this.HasStencilLightsOfType(LightType.Point))
					{
						this.RenderStencilPointLights(cmd, stripShadowsOffVariants, lightData, shadowData, visibleLights, hasAdditionalLightPass, hasLightCookieManager);
					}
					if (this.HasStencilLightsOfType(LightType.Spot))
					{
						this.RenderStencilSpotLights(cmd, stripShadowsOffVariants, lightData, shadowData, visibleLights, hasAdditionalLightPass, hasLightCookieManager);
					}
				}
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0003A5B0 File Offset: 0x000387B0
		private void RenderStencilDirectionalLights(RasterCommandBuffer cmd, bool stripShadowsOffVariants, UniversalLightData lightData, UniversalShadowData shadowData, NativeArray<VisibleLight> visibleLights, bool hasAdditionalLightPass, bool hasLightCookieManager, int mainLightIndex)
		{
			if (this.m_FullscreenMesh == null)
			{
				this.m_FullscreenMesh = DeferredLights.CreateFullscreenMesh();
			}
			cmd.SetKeyword(in ShaderGlobalKeywords._DIRECTIONAL, true);
			int lastLightCookieIndex = -1;
			bool isFirstLight = true;
			bool lastLightCookieKeywordState = false;
			bool lastShadowsKeywordState = false;
			bool lastSoftShadowsKeywordState = false;
			for (int soffset = (int)this.m_stencilVisLightOffsets[1]; soffset < this.m_stencilVisLights.Length; soffset++)
			{
				ushort visLightIndex = this.m_stencilVisLights[soffset];
				ref VisibleLight vl = ref visibleLights.UnsafeElementAtMutable((int)visLightIndex);
				if (vl.lightType != LightType.Directional)
				{
					break;
				}
				Light light = vl.light;
				Vector4 lightDir;
				Vector4 lightColor;
				Vector4 lightAttenuation;
				Vector4 lightSpotDir;
				Vector4 lightOcclusionChannel;
				UniversalRenderPipeline.InitializeLightConstants_Common(visibleLights, (int)visLightIndex, out lightDir, out lightColor, out lightAttenuation, out lightSpotDir, out lightOcclusionChannel);
				int lightFlags = 0;
				if (light.bakingOutput.lightmapBakeType == LightmapBakeType.Mixed)
				{
					lightFlags |= 4;
				}
				if (lightData.supportsLightLayers)
				{
					this.SetRenderingLayersMask(cmd, light, DeferredLights.ShaderConstants._LightLayerMask);
				}
				bool hasDeferredShadows = light && light.shadows > LightShadows.None;
				bool isMainLight = (int)visLightIndex == mainLightIndex;
				if (!isMainLight)
				{
					int shadowLightIndex = (hasAdditionalLightPass ? this.m_AdditionalLightsShadowCasterPass.GetShadowLightIndexFromLightIndex((int)visLightIndex) : (-1));
					hasDeferredShadows = light && light.shadows != LightShadows.None && shadowLightIndex >= 0;
					cmd.SetGlobalInt(DeferredLights.ShaderConstants._ShadowLightIndex, shadowLightIndex);
					this.SetLightCookiesKeyword(cmd, (int)visLightIndex, hasLightCookieManager, isFirstLight, ref lastLightCookieKeywordState, ref lastLightCookieIndex);
				}
				this.SetAdditionalLightsShadowsKeyword(ref cmd, stripShadowsOffVariants, shadowData.additionalLightShadowsEnabled, hasDeferredShadows, isFirstLight, ref lastShadowsKeywordState);
				this.SetSoftShadowsKeyword(cmd, shadowData, light, hasDeferredShadows, isFirstLight, ref lastSoftShadowsKeywordState);
				cmd.SetKeyword(in ShaderGlobalKeywords._DEFERRED_FIRST_LIGHT, isFirstLight);
				cmd.SetKeyword(in ShaderGlobalKeywords._DEFERRED_MAIN_LIGHT, isMainLight);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightColor, lightColor);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightDirection, lightDir);
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._LightFlags, lightFlags);
				cmd.DrawMesh(this.m_FullscreenMesh, Matrix4x4.identity, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[3]);
				cmd.DrawMesh(this.m_FullscreenMesh, Matrix4x4.identity, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[4]);
				isFirstLight = false;
			}
			cmd.SetKeyword(in ShaderGlobalKeywords._DIRECTIONAL, false);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0003A7B4 File Offset: 0x000389B4
		private void RenderStencilPointLights(RasterCommandBuffer cmd, bool stripShadowsOffVariants, UniversalLightData lightData, UniversalShadowData shadowData, NativeArray<VisibleLight> visibleLights, bool hasAdditionalLightPass, bool hasLightCookieManager)
		{
			if (this.m_SphereMesh == null)
			{
				this.m_SphereMesh = DeferredLights.CreateSphereMesh();
			}
			cmd.SetKeyword(in ShaderGlobalKeywords._POINT, true);
			int lastLightCookieIndex = -1;
			bool isFirstLight = true;
			bool lastLightCookieKeywordState = false;
			bool lastShadowsKeywordState = false;
			bool lastSoftShadowsKeywordState = false;
			for (int soffset = (int)this.m_stencilVisLightOffsets[2]; soffset < this.m_stencilVisLights.Length; soffset++)
			{
				ushort visLightIndex = this.m_stencilVisLights[soffset];
				ref VisibleLight vl = ref visibleLights.UnsafeElementAtMutable((int)visLightIndex);
				if (vl.lightType != LightType.Point)
				{
					break;
				}
				Light light = vl.light;
				Vector3 posWS = vl.localToWorldMatrix.GetColumn(3);
				Matrix4x4 transformMatrix = new Matrix4x4(new Vector4(vl.range, 0f, 0f, 0f), new Vector4(0f, vl.range, 0f, 0f), new Vector4(0f, 0f, vl.range, 0f), new Vector4(posWS.x, posWS.y, posWS.z, 1f));
				Vector4 lightPos;
				Vector4 lightColor;
				Vector4 lightAttenuation;
				Vector4 vector;
				Vector4 lightOcclusionChannel;
				UniversalRenderPipeline.InitializeLightConstants_Common(visibleLights, (int)visLightIndex, out lightPos, out lightColor, out lightAttenuation, out vector, out lightOcclusionChannel);
				if (lightData.supportsLightLayers)
				{
					this.SetRenderingLayersMask(cmd, light, DeferredLights.ShaderConstants._LightLayerMask);
				}
				int lightFlags = 0;
				if (light.bakingOutput.lightmapBakeType == LightmapBakeType.Mixed)
				{
					lightFlags |= 4;
				}
				int shadowLightIndex = (hasAdditionalLightPass ? this.m_AdditionalLightsShadowCasterPass.GetShadowLightIndexFromLightIndex((int)visLightIndex) : (-1));
				bool hasDeferredShadows = light && light.shadows != LightShadows.None && shadowLightIndex >= 0;
				this.SetAdditionalLightsShadowsKeyword(ref cmd, stripShadowsOffVariants, shadowData.additionalLightShadowsEnabled, hasDeferredShadows, isFirstLight, ref lastShadowsKeywordState);
				this.SetSoftShadowsKeyword(cmd, shadowData, light, hasDeferredShadows, isFirstLight, ref lastSoftShadowsKeywordState);
				this.SetLightCookiesKeyword(cmd, (int)visLightIndex, hasLightCookieManager, isFirstLight, ref lastLightCookieKeywordState, ref lastLightCookieIndex);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightPosWS, lightPos);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightColor, lightColor);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightAttenuation, lightAttenuation);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightOcclusionProbInfo, lightOcclusionChannel);
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._LightFlags, lightFlags);
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._ShadowLightIndex, shadowLightIndex);
				cmd.DrawMesh(this.m_SphereMesh, transformMatrix, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[0]);
				cmd.DrawMesh(this.m_SphereMesh, transformMatrix, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[1]);
				cmd.DrawMesh(this.m_SphereMesh, transformMatrix, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[2]);
				isFirstLight = false;
			}
			cmd.SetKeyword(in ShaderGlobalKeywords._POINT, false);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0003AA3C File Offset: 0x00038C3C
		private void RenderStencilSpotLights(RasterCommandBuffer cmd, bool stripShadowsOffVariants, UniversalLightData lightData, UniversalShadowData shadowData, NativeArray<VisibleLight> visibleLights, bool hasAdditionalLightPass, bool hasLightCookieManager)
		{
			if (this.m_HemisphereMesh == null)
			{
				this.m_HemisphereMesh = DeferredLights.CreateHemisphereMesh();
			}
			cmd.SetKeyword(in ShaderGlobalKeywords._SPOT, true);
			int lastLightCookieIndex = -1;
			bool isFirstLight = true;
			bool lastLightCookieKeywordState = false;
			bool lastShadowsKeywordState = false;
			bool lastSoftShadowsKeywordState = false;
			for (int soffset = (int)this.m_stencilVisLightOffsets[0]; soffset < this.m_stencilVisLights.Length; soffset++)
			{
				ushort visLightIndex = this.m_stencilVisLights[soffset];
				ref VisibleLight vl = ref visibleLights.UnsafeElementAtMutable((int)visLightIndex);
				if (vl.lightType != LightType.Spot)
				{
					break;
				}
				Light light = vl.light;
				float num = 0.017453292f * vl.spotAngle * 0.5f;
				float cosAlpha = Mathf.Cos(num);
				float sinAlpha = Mathf.Sin(num);
				float guard = Mathf.Lerp(1f, DeferredLights.kStencilShapeGuard, sinAlpha);
				Vector4 lightPos;
				Vector4 lightColor;
				Vector4 lightAttenuation;
				Vector4 lightSpotDir;
				Vector4 lightOcclusionChannel;
				UniversalRenderPipeline.InitializeLightConstants_Common(visibleLights, (int)visLightIndex, out lightPos, out lightColor, out lightAttenuation, out lightSpotDir, out lightOcclusionChannel);
				if (lightData.supportsLightLayers)
				{
					this.SetRenderingLayersMask(cmd, light, DeferredLights.ShaderConstants._LightLayerMask);
				}
				int lightFlags = 0;
				if (light.bakingOutput.lightmapBakeType == LightmapBakeType.Mixed)
				{
					lightFlags |= 4;
				}
				int shadowLightIndex = (hasAdditionalLightPass ? this.m_AdditionalLightsShadowCasterPass.GetShadowLightIndexFromLightIndex((int)visLightIndex) : (-1));
				bool hasDeferredShadows = light && light.shadows != LightShadows.None && shadowLightIndex >= 0;
				this.SetAdditionalLightsShadowsKeyword(ref cmd, stripShadowsOffVariants, shadowData.additionalLightShadowsEnabled, hasDeferredShadows, isFirstLight, ref lastShadowsKeywordState);
				this.SetSoftShadowsKeyword(cmd, shadowData, light, hasDeferredShadows, isFirstLight, ref lastSoftShadowsKeywordState);
				this.SetLightCookiesKeyword(cmd, (int)visLightIndex, hasLightCookieManager, isFirstLight, ref lastLightCookieKeywordState, ref lastLightCookieIndex);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._SpotLightScale, new Vector4(sinAlpha, sinAlpha, 1f - cosAlpha, vl.range));
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._SpotLightBias, new Vector4(0f, 0f, cosAlpha, 0f));
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._SpotLightGuard, new Vector4(guard, guard, guard, cosAlpha * vl.range));
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightPosWS, lightPos);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightColor, lightColor);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightAttenuation, lightAttenuation);
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightDirection, new Vector3(lightSpotDir.x, lightSpotDir.y, lightSpotDir.z));
				cmd.SetGlobalVector(DeferredLights.ShaderConstants._LightOcclusionProbInfo, lightOcclusionChannel);
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._LightFlags, lightFlags);
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._ShadowLightIndex, shadowLightIndex);
				cmd.DrawMesh(this.m_HemisphereMesh, vl.localToWorldMatrix, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[0]);
				cmd.DrawMesh(this.m_HemisphereMesh, vl.localToWorldMatrix, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[1]);
				cmd.DrawMesh(this.m_HemisphereMesh, vl.localToWorldMatrix, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[2]);
				isFirstLight = false;
			}
			cmd.SetKeyword(in ShaderGlobalKeywords._SPOT, false);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0003AD03 File Offset: 0x00038F03
		private void RenderSSAOBeforeShading(RasterCommandBuffer cmd)
		{
			if (this.m_FullscreenMesh == null)
			{
				this.m_FullscreenMesh = DeferredLights.CreateFullscreenMesh();
			}
			cmd.DrawMesh(this.m_FullscreenMesh, Matrix4x4.identity, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[7]);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0003AD40 File Offset: 0x00038F40
		private void RenderFog(RasterCommandBuffer cmd, bool isOrthographic)
		{
			if (!RenderSettings.fog || isOrthographic)
			{
				return;
			}
			if (this.m_FullscreenMesh == null)
			{
				this.m_FullscreenMesh = DeferredLights.CreateFullscreenMesh();
			}
			using (new ProfilingScope(cmd, this.m_ProfilingSamplerDeferredFogPass))
			{
				cmd.DrawMesh(this.m_FullscreenMesh, Matrix4x4.identity, this.m_StencilDeferredMaterial, 0, this.m_StencilDeferredPasses[6]);
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0003ADC4 File Offset: 0x00038FC4
		private void InitStencilDeferredMaterial()
		{
			if (this.m_StencilDeferredMaterial == null)
			{
				return;
			}
			for (int pass = 0; pass < DeferredLights.k_StencilDeferredPassNames.Length; pass++)
			{
				this.m_StencilDeferredPasses[pass] = this.m_StencilDeferredMaterial.FindPass(DeferredLights.k_StencilDeferredPassNames[pass]);
			}
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._StencilRef, 0f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._StencilReadMask, 96f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._StencilWriteMask, 16f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._LitPunctualStencilRef, 48f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._LitPunctualStencilReadMask, 112f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._LitPunctualStencilWriteMask, 16f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._SimpleLitPunctualStencilRef, 80f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._SimpleLitPunctualStencilReadMask, 112f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._SimpleLitPunctualStencilWriteMask, 16f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._LitDirStencilRef, 32f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._LitDirStencilReadMask, 96f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._LitDirStencilWriteMask, 0f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._SimpleLitDirStencilRef, 64f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._SimpleLitDirStencilReadMask, 96f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._SimpleLitDirStencilWriteMask, 0f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._ClearStencilRef, 0f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._ClearStencilReadMask, 96f);
			this.m_StencilDeferredMaterial.SetFloat(DeferredLights.ShaderConstants._ClearStencilWriteMask, 96f);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0003AF88 File Offset: 0x00039188
		private static Mesh CreateSphereMesh()
		{
			Vector3[] positions = new Vector3[]
			{
				new Vector3(0f, 0f, -1.07f),
				new Vector3(0.174f, -0.535f, -0.91f),
				new Vector3(-0.455f, -0.331f, -0.91f),
				new Vector3(0.562f, 0f, -0.91f),
				new Vector3(-0.455f, 0.331f, -0.91f),
				new Vector3(0.174f, 0.535f, -0.91f),
				new Vector3(-0.281f, -0.865f, -0.562f),
				new Vector3(0.736f, -0.535f, -0.562f),
				new Vector3(0.296f, -0.91f, -0.468f),
				new Vector3(-0.91f, 0f, -0.562f),
				new Vector3(-0.774f, -0.562f, -0.478f),
				new Vector3(0f, -1.07f, 0f),
				new Vector3(-0.629f, -0.865f, 0f),
				new Vector3(0.629f, -0.865f, 0f),
				new Vector3(-1.017f, -0.331f, 0f),
				new Vector3(0.957f, 0f, -0.478f),
				new Vector3(0.736f, 0.535f, -0.562f),
				new Vector3(1.017f, -0.331f, 0f),
				new Vector3(1.017f, 0.331f, 0f),
				new Vector3(-0.296f, -0.91f, 0.478f),
				new Vector3(0.281f, -0.865f, 0.562f),
				new Vector3(0.774f, -0.562f, 0.478f),
				new Vector3(-0.736f, -0.535f, 0.562f),
				new Vector3(0.91f, 0f, 0.562f),
				new Vector3(0.455f, -0.331f, 0.91f),
				new Vector3(-0.174f, -0.535f, 0.91f),
				new Vector3(0.629f, 0.865f, 0f),
				new Vector3(0.774f, 0.562f, 0.478f),
				new Vector3(0.455f, 0.331f, 0.91f),
				new Vector3(0f, 0f, 1.07f),
				new Vector3(-0.562f, 0f, 0.91f),
				new Vector3(-0.957f, 0f, 0.478f),
				new Vector3(0.281f, 0.865f, 0.562f),
				new Vector3(-0.174f, 0.535f, 0.91f),
				new Vector3(0.296f, 0.91f, -0.478f),
				new Vector3(-1.017f, 0.331f, 0f),
				new Vector3(-0.736f, 0.535f, 0.562f),
				new Vector3(-0.296f, 0.91f, 0.478f),
				new Vector3(0f, 1.07f, 0f),
				new Vector3(-0.281f, 0.865f, -0.562f),
				new Vector3(-0.774f, 0.562f, -0.478f),
				new Vector3(-0.629f, 0.865f, 0f)
			};
			int[] indices = new int[]
			{
				0, 1, 2, 0, 3, 1, 2, 4, 0, 0,
				5, 3, 0, 4, 5, 1, 6, 2, 3, 7,
				1, 1, 8, 6, 1, 7, 8, 9, 4, 2,
				2, 6, 10, 10, 9, 2, 8, 11, 6, 6,
				12, 10, 11, 12, 6, 7, 13, 8, 8, 13,
				11, 10, 14, 9, 10, 12, 14, 3, 15, 7,
				5, 16, 3, 3, 16, 15, 15, 17, 7, 17,
				13, 7, 16, 18, 15, 15, 18, 17, 11, 19,
				12, 13, 20, 11, 11, 20, 19, 17, 21, 13,
				13, 21, 20, 12, 19, 22, 12, 22, 14, 17,
				23, 21, 18, 23, 17, 21, 24, 20, 23, 24,
				21, 20, 25, 19, 19, 25, 22, 24, 25, 20,
				26, 18, 16, 18, 27, 23, 26, 27, 18, 28,
				24, 23, 27, 28, 23, 24, 29, 25, 28, 29,
				24, 25, 30, 22, 25, 29, 30, 14, 22, 31,
				22, 30, 31, 32, 28, 27, 26, 32, 27, 33,
				29, 28, 30, 29, 33, 33, 28, 32, 34, 26,
				16, 5, 34, 16, 14, 31, 35, 14, 35, 9,
				31, 30, 36, 30, 33, 36, 35, 31, 36, 37,
				33, 32, 36, 33, 37, 38, 32, 26, 34, 38,
				26, 38, 37, 32, 5, 39, 34, 39, 38, 34,
				4, 39, 5, 9, 40, 4, 9, 35, 40, 4,
				40, 39, 35, 36, 41, 41, 36, 37, 41, 37,
				38, 40, 35, 41, 40, 41, 39, 41, 38, 39
			};
			return new Mesh
			{
				indexFormat = IndexFormat.UInt16,
				vertices = positions,
				triangles = indices
			};
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0003B45C File Offset: 0x0003965C
		private static Mesh CreateHemisphereMesh()
		{
			Vector3[] positions = new Vector3[]
			{
				new Vector3(0f, 0f, 0f),
				new Vector3(1f, 0f, 0f),
				new Vector3(0.92388f, 0.382683f, 0f),
				new Vector3(0.707107f, 0.707107f, 0f),
				new Vector3(0.382683f, 0.92388f, 0f),
				new Vector3(-0f, 1f, 0f),
				new Vector3(-0.382684f, 0.92388f, 0f),
				new Vector3(-0.707107f, 0.707107f, 0f),
				new Vector3(-0.92388f, 0.382683f, 0f),
				new Vector3(-1f, -0f, 0f),
				new Vector3(-0.92388f, -0.382683f, 0f),
				new Vector3(-0.707107f, -0.707107f, 0f),
				new Vector3(-0.382683f, -0.92388f, 0f),
				new Vector3(0f, -1f, 0f),
				new Vector3(0.382684f, -0.923879f, 0f),
				new Vector3(0.707107f, -0.707107f, 0f),
				new Vector3(0.92388f, -0.382683f, 0f),
				new Vector3(0f, 0f, 1f),
				new Vector3(0.707107f, 0f, 0.707107f),
				new Vector3(0f, -0.707107f, 0.707107f),
				new Vector3(0f, 0.707107f, 0.707107f),
				new Vector3(-0.707107f, 0f, 0.707107f),
				new Vector3(0.816497f, -0.408248f, 0.408248f),
				new Vector3(0.408248f, -0.408248f, 0.816497f),
				new Vector3(0.408248f, -0.816497f, 0.408248f),
				new Vector3(0.408248f, 0.816497f, 0.408248f),
				new Vector3(0.408248f, 0.408248f, 0.816497f),
				new Vector3(0.816497f, 0.408248f, 0.408248f),
				new Vector3(-0.816497f, 0.408248f, 0.408248f),
				new Vector3(-0.408248f, 0.408248f, 0.816497f),
				new Vector3(-0.408248f, 0.816497f, 0.408248f),
				new Vector3(-0.408248f, -0.816497f, 0.408248f),
				new Vector3(-0.408248f, -0.408248f, 0.816497f),
				new Vector3(-0.816497f, -0.408248f, 0.408248f),
				new Vector3(0f, -0.92388f, 0.382683f),
				new Vector3(0.92388f, 0f, 0.382683f),
				new Vector3(0f, -0.382683f, 0.92388f),
				new Vector3(0.382683f, 0f, 0.92388f),
				new Vector3(0f, 0.92388f, 0.382683f),
				new Vector3(0f, 0.382683f, 0.92388f),
				new Vector3(-0.92388f, 0f, 0.382683f),
				new Vector3(-0.382683f, 0f, 0.92388f)
			};
			int[] indices = new int[]
			{
				0, 2, 1, 0, 3, 2, 0, 4, 3, 0,
				5, 4, 0, 6, 5, 0, 7, 6, 0, 8,
				7, 0, 9, 8, 0, 10, 9, 0, 11, 10,
				0, 12, 11, 0, 13, 12, 0, 14, 13, 0,
				15, 14, 0, 16, 15, 0, 1, 16, 22, 23,
				24, 25, 26, 27, 28, 29, 30, 31, 32, 33,
				14, 24, 34, 35, 22, 16, 36, 23, 37, 2,
				27, 35, 38, 25, 4, 37, 26, 39, 6, 30,
				38, 40, 28, 8, 39, 29, 41, 10, 33, 40,
				34, 31, 12, 41, 32, 36, 15, 22, 24, 18,
				23, 22, 19, 24, 23, 3, 25, 27, 20, 26,
				25, 18, 27, 26, 7, 28, 30, 21, 29, 28,
				20, 30, 29, 11, 31, 33, 19, 32, 31, 21,
				33, 32, 13, 14, 34, 15, 24, 14, 19, 34,
				24, 1, 35, 16, 18, 22, 35, 15, 16, 22,
				17, 36, 37, 19, 23, 36, 18, 37, 23, 1,
				2, 35, 3, 27, 2, 18, 35, 27, 5, 38,
				4, 20, 25, 38, 3, 4, 25, 17, 37, 39,
				18, 26, 37, 20, 39, 26, 5, 6, 38, 7,
				30, 6, 20, 38, 30, 9, 40, 8, 21, 28,
				40, 7, 8, 28, 17, 39, 41, 20, 29, 39,
				21, 41, 29, 9, 10, 40, 11, 33, 10, 21,
				40, 33, 13, 34, 12, 19, 31, 34, 11, 12,
				31, 17, 41, 36, 21, 32, 41, 19, 36, 32
			};
			return new Mesh
			{
				indexFormat = IndexFormat.UInt16,
				vertices = positions,
				triangles = indices
			};
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0003B930 File Offset: 0x00039B30
		private static Mesh CreateFullscreenMesh()
		{
			Vector3[] positions = new Vector3[]
			{
				new Vector3(-1f, 1f, 0f),
				new Vector3(-1f, -3f, 0f),
				new Vector3(3f, 1f, 0f)
			};
			int[] indices = new int[] { 0, 1, 2 };
			return new Mesh
			{
				indexFormat = IndexFormat.UInt16,
				vertices = positions,
				triangles = indices
			};
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0003B9C0 File Offset: 0x00039BC0
		private void SetRenderingLayersMask(RasterCommandBuffer cmd, Light light, int shaderPropertyID)
		{
			uint lightLayerMask = RenderingLayerUtils.ToValidRenderingLayers(light.GetUniversalAdditionalLightData().renderingLayers);
			cmd.SetGlobalInt(shaderPropertyID, (int)lightLayerMask);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0003B9E8 File Offset: 0x00039BE8
		private void SetAdditionalLightsShadowsKeyword(ref RasterCommandBuffer cmd, bool stripShadowsOffVariants, bool additionalLightShadowsEnabled, bool hasDeferredShadows, bool shouldOverride, ref bool lastShadowsKeyword)
		{
			bool hasOffVariant = !stripShadowsOffVariants;
			bool shouldEnable = additionalLightShadowsEnabled && (!hasOffVariant || hasDeferredShadows);
			if (!shouldOverride && lastShadowsKeyword == shouldEnable)
			{
				return;
			}
			lastShadowsKeyword = shouldEnable;
			cmd.SetKeyword(in ShaderGlobalKeywords.AdditionalLightShadows, shouldEnable);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0003BA24 File Offset: 0x00039C24
		private void SetSoftShadowsKeyword(RasterCommandBuffer cmd, UniversalShadowData shadowData, Light light, bool hasDeferredShadows, bool shouldOverride, ref bool lastHasSoftShadow)
		{
			bool hasSoftShadow = hasDeferredShadows && shadowData.supportsSoftShadows && light.shadows == LightShadows.Soft;
			if (!shouldOverride && lastHasSoftShadow == hasSoftShadow)
			{
				return;
			}
			lastHasSoftShadow = hasSoftShadow;
			ShadowUtils.SetPerLightSoftShadowKeyword(cmd, hasSoftShadow);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0003BA60 File Offset: 0x00039C60
		private void SetLightCookiesKeyword(RasterCommandBuffer cmd, int visLightIndex, bool hasLightCookieManager, bool shouldOverride, ref bool lastLightCookieState, ref int lastCookieLightIndex)
		{
			if (!hasLightCookieManager)
			{
				return;
			}
			int cookieLightIndex = this.m_LightCookieManager.GetLightCookieShaderDataIndex(visLightIndex);
			bool newState = cookieLightIndex >= 0;
			if (shouldOverride || newState != lastLightCookieState)
			{
				lastLightCookieState = newState;
				cmd.SetKeyword(in ShaderGlobalKeywords.LightCookies, newState);
			}
			if (shouldOverride || cookieLightIndex != lastCookieLightIndex)
			{
				lastCookieLightIndex = cookieLightIndex;
				cmd.SetGlobalInt(DeferredLights.ShaderConstants._CookieLightIndex, cookieLightIndex);
			}
		}

		// Token: 0x04000C12 RID: 3090
		internal static readonly string[] k_GBufferNames = new string[] { "_GBuffer0", "_GBuffer1", "_GBuffer2", "_GBuffer3", "_GBuffer4", "_GBuffer5", "_GBuffer6" };

		// Token: 0x04000C13 RID: 3091
		internal static readonly int[] k_GBufferShaderPropertyIDs = new int[]
		{
			Shader.PropertyToID(DeferredLights.k_GBufferNames[0]),
			Shader.PropertyToID(DeferredLights.k_GBufferNames[1]),
			Shader.PropertyToID(DeferredLights.k_GBufferNames[2]),
			Shader.PropertyToID(DeferredLights.k_GBufferNames[3]),
			Shader.PropertyToID(DeferredLights.k_GBufferNames[4]),
			Shader.PropertyToID(DeferredLights.k_GBufferNames[5]),
			Shader.PropertyToID(DeferredLights.k_GBufferNames[6])
		};

		// Token: 0x04000C14 RID: 3092
		private static readonly string[] k_StencilDeferredPassNames = new string[] { "Stencil Volume", "Deferred Punctual Light (Lit)", "Deferred Punctual Light (SimpleLit)", "Deferred Directional Light (Lit)", "Deferred Directional Light (SimpleLit)", "ClearStencilPartial", "Fog", "SSAOOnly" };

		// Token: 0x04000C15 RID: 3093
		private static readonly ushort k_InvalidLightOffset = ushort.MaxValue;

		// Token: 0x04000C16 RID: 3094
		private static readonly string k_SetupLights = "SetupLights";

		// Token: 0x04000C17 RID: 3095
		private static readonly string k_DeferredPass = "Deferred Pass";

		// Token: 0x04000C18 RID: 3096
		private static readonly string k_DeferredStencilPass = "Deferred Shading (Stencil)";

		// Token: 0x04000C19 RID: 3097
		private static readonly string k_DeferredFogPass = "Deferred Fog";

		// Token: 0x04000C1A RID: 3098
		private static readonly string k_ClearStencilPartial = "Clear Stencil Partial";

		// Token: 0x04000C1B RID: 3099
		private static readonly string k_SetupLightConstants = "Setup Light Constants";

		// Token: 0x04000C1C RID: 3100
		private static readonly float kStencilShapeGuard = 1.06067f;

		// Token: 0x04000C1D RID: 3101
		private static readonly ProfilingSampler m_ProfilingSetupLights = new ProfilingSampler(DeferredLights.k_SetupLights);

		// Token: 0x04000C1E RID: 3102
		private static readonly ProfilingSampler m_ProfilingDeferredPass = new ProfilingSampler(DeferredLights.k_DeferredPass);

		// Token: 0x04000C1F RID: 3103
		private static readonly ProfilingSampler m_ProfilingSetupLightConstants = new ProfilingSampler(DeferredLights.k_SetupLightConstants);

		// Token: 0x04000C2D RID: 3117
		private RTHandle[] GbufferRTHandles;

		// Token: 0x04000C35 RID: 3125
		private NativeArray<ushort> m_stencilVisLights;

		// Token: 0x04000C36 RID: 3126
		private NativeArray<ushort> m_stencilVisLightOffsets;

		// Token: 0x04000C37 RID: 3127
		private AdditionalLightsShadowCasterPass m_AdditionalLightsShadowCasterPass;

		// Token: 0x04000C38 RID: 3128
		private Mesh m_SphereMesh;

		// Token: 0x04000C39 RID: 3129
		private Mesh m_HemisphereMesh;

		// Token: 0x04000C3A RID: 3130
		private Mesh m_FullscreenMesh;

		// Token: 0x04000C3B RID: 3131
		private Material m_StencilDeferredMaterial;

		// Token: 0x04000C3C RID: 3132
		private int[] m_StencilDeferredPasses;

		// Token: 0x04000C3D RID: 3133
		private Matrix4x4[] m_ScreenToWorld = new Matrix4x4[2];

		// Token: 0x04000C3E RID: 3134
		private ProfilingSampler m_ProfilingSamplerDeferredStencilPass = new ProfilingSampler(DeferredLights.k_DeferredStencilPass);

		// Token: 0x04000C3F RID: 3135
		private ProfilingSampler m_ProfilingSamplerDeferredFogPass = new ProfilingSampler(DeferredLights.k_DeferredFogPass);

		// Token: 0x04000C40 RID: 3136
		private ProfilingSampler m_ProfilingSamplerClearStencilPartialPass = new ProfilingSampler(DeferredLights.k_ClearStencilPartial);

		// Token: 0x04000C41 RID: 3137
		private LightCookieManager m_LightCookieManager;

		// Token: 0x04000C42 RID: 3138
		private static ProfilingSampler s_SetupDeferredLights = new ProfilingSampler("Setup Deferred lights");

		// Token: 0x020001F0 RID: 496
		internal static class ShaderConstants
		{
			// Token: 0x04000C43 RID: 3139
			public static readonly int _LitStencilRef = Shader.PropertyToID("_LitStencilRef");

			// Token: 0x04000C44 RID: 3140
			public static readonly int _LitStencilReadMask = Shader.PropertyToID("_LitStencilReadMask");

			// Token: 0x04000C45 RID: 3141
			public static readonly int _LitStencilWriteMask = Shader.PropertyToID("_LitStencilWriteMask");

			// Token: 0x04000C46 RID: 3142
			public static readonly int _SimpleLitStencilRef = Shader.PropertyToID("_SimpleLitStencilRef");

			// Token: 0x04000C47 RID: 3143
			public static readonly int _SimpleLitStencilReadMask = Shader.PropertyToID("_SimpleLitStencilReadMask");

			// Token: 0x04000C48 RID: 3144
			public static readonly int _SimpleLitStencilWriteMask = Shader.PropertyToID("_SimpleLitStencilWriteMask");

			// Token: 0x04000C49 RID: 3145
			public static readonly int _StencilRef = Shader.PropertyToID("_StencilRef");

			// Token: 0x04000C4A RID: 3146
			public static readonly int _StencilReadMask = Shader.PropertyToID("_StencilReadMask");

			// Token: 0x04000C4B RID: 3147
			public static readonly int _StencilWriteMask = Shader.PropertyToID("_StencilWriteMask");

			// Token: 0x04000C4C RID: 3148
			public static readonly int _LitPunctualStencilRef = Shader.PropertyToID("_LitPunctualStencilRef");

			// Token: 0x04000C4D RID: 3149
			public static readonly int _LitPunctualStencilReadMask = Shader.PropertyToID("_LitPunctualStencilReadMask");

			// Token: 0x04000C4E RID: 3150
			public static readonly int _LitPunctualStencilWriteMask = Shader.PropertyToID("_LitPunctualStencilWriteMask");

			// Token: 0x04000C4F RID: 3151
			public static readonly int _SimpleLitPunctualStencilRef = Shader.PropertyToID("_SimpleLitPunctualStencilRef");

			// Token: 0x04000C50 RID: 3152
			public static readonly int _SimpleLitPunctualStencilReadMask = Shader.PropertyToID("_SimpleLitPunctualStencilReadMask");

			// Token: 0x04000C51 RID: 3153
			public static readonly int _SimpleLitPunctualStencilWriteMask = Shader.PropertyToID("_SimpleLitPunctualStencilWriteMask");

			// Token: 0x04000C52 RID: 3154
			public static readonly int _LitDirStencilRef = Shader.PropertyToID("_LitDirStencilRef");

			// Token: 0x04000C53 RID: 3155
			public static readonly int _LitDirStencilReadMask = Shader.PropertyToID("_LitDirStencilReadMask");

			// Token: 0x04000C54 RID: 3156
			public static readonly int _LitDirStencilWriteMask = Shader.PropertyToID("_LitDirStencilWriteMask");

			// Token: 0x04000C55 RID: 3157
			public static readonly int _SimpleLitDirStencilRef = Shader.PropertyToID("_SimpleLitDirStencilRef");

			// Token: 0x04000C56 RID: 3158
			public static readonly int _SimpleLitDirStencilReadMask = Shader.PropertyToID("_SimpleLitDirStencilReadMask");

			// Token: 0x04000C57 RID: 3159
			public static readonly int _SimpleLitDirStencilWriteMask = Shader.PropertyToID("_SimpleLitDirStencilWriteMask");

			// Token: 0x04000C58 RID: 3160
			public static readonly int _ClearStencilRef = Shader.PropertyToID("_ClearStencilRef");

			// Token: 0x04000C59 RID: 3161
			public static readonly int _ClearStencilReadMask = Shader.PropertyToID("_ClearStencilReadMask");

			// Token: 0x04000C5A RID: 3162
			public static readonly int _ClearStencilWriteMask = Shader.PropertyToID("_ClearStencilWriteMask");

			// Token: 0x04000C5B RID: 3163
			public static readonly int _ScreenToWorld = Shader.PropertyToID("_ScreenToWorld");

			// Token: 0x04000C5C RID: 3164
			public static int _MainLightPosition = Shader.PropertyToID("_MainLightPosition");

			// Token: 0x04000C5D RID: 3165
			public static int _MainLightColor = Shader.PropertyToID("_MainLightColor");

			// Token: 0x04000C5E RID: 3166
			public static int _MainLightLayerMask = Shader.PropertyToID("_MainLightLayerMask");

			// Token: 0x04000C5F RID: 3167
			public static int _SpotLightScale = Shader.PropertyToID("_SpotLightScale");

			// Token: 0x04000C60 RID: 3168
			public static int _SpotLightBias = Shader.PropertyToID("_SpotLightBias");

			// Token: 0x04000C61 RID: 3169
			public static int _SpotLightGuard = Shader.PropertyToID("_SpotLightGuard");

			// Token: 0x04000C62 RID: 3170
			public static int _LightPosWS = Shader.PropertyToID("_LightPosWS");

			// Token: 0x04000C63 RID: 3171
			public static int _LightColor = Shader.PropertyToID("_LightColor");

			// Token: 0x04000C64 RID: 3172
			public static int _LightAttenuation = Shader.PropertyToID("_LightAttenuation");

			// Token: 0x04000C65 RID: 3173
			public static int _LightOcclusionProbInfo = Shader.PropertyToID("_LightOcclusionProbInfo");

			// Token: 0x04000C66 RID: 3174
			public static int _LightDirection = Shader.PropertyToID("_LightDirection");

			// Token: 0x04000C67 RID: 3175
			public static int _LightFlags = Shader.PropertyToID("_LightFlags");

			// Token: 0x04000C68 RID: 3176
			public static int _ShadowLightIndex = Shader.PropertyToID("_ShadowLightIndex");

			// Token: 0x04000C69 RID: 3177
			public static int _LightLayerMask = Shader.PropertyToID("_LightLayerMask");

			// Token: 0x04000C6A RID: 3178
			public static int _CookieLightIndex = Shader.PropertyToID("_CookieLightIndex");
		}

		// Token: 0x020001F1 RID: 497
		internal enum StencilDeferredPasses
		{
			// Token: 0x04000C6C RID: 3180
			StencilVolume,
			// Token: 0x04000C6D RID: 3181
			PunctualLit,
			// Token: 0x04000C6E RID: 3182
			PunctualSimpleLit,
			// Token: 0x04000C6F RID: 3183
			DirectionalLit,
			// Token: 0x04000C70 RID: 3184
			DirectionalSimpleLit,
			// Token: 0x04000C71 RID: 3185
			ClearStencilPartial,
			// Token: 0x04000C72 RID: 3186
			Fog,
			// Token: 0x04000C73 RID: 3187
			SSAOOnly
		}

		// Token: 0x020001F2 RID: 498
		internal struct InitParams
		{
			// Token: 0x04000C74 RID: 3188
			public Material stencilDeferredMaterial;

			// Token: 0x04000C75 RID: 3189
			public LightCookieManager lightCookieManager;
		}

		// Token: 0x020001F3 RID: 499
		private class SetupLightPassData
		{
			// Token: 0x04000C76 RID: 3190
			internal UniversalCameraData cameraData;

			// Token: 0x04000C77 RID: 3191
			internal UniversalLightData lightData;

			// Token: 0x04000C78 RID: 3192
			internal DeferredLights deferredLights;

			// Token: 0x04000C79 RID: 3193
			internal Vector2Int cameraTargetSizeCopy;
		}
	}
}
