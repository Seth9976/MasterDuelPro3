using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000068 RID: 104
	internal class DebugHandler : IDebugDisplaySettingsQuery
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000706B File Offset: 0x0000526B
		private DebugDisplaySettingsLighting LightingSettings
		{
			get
			{
				return this.m_DebugDisplaySettings.lightingSettings;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00007078 File Offset: 0x00005278
		private DebugDisplaySettingsMaterial MaterialSettings
		{
			get
			{
				return this.m_DebugDisplaySettings.materialSettings;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00007085 File Offset: 0x00005285
		private DebugDisplaySettingsRendering RenderingSettings
		{
			get
			{
				return this.m_DebugDisplaySettings.renderingSettings;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00007092 File Offset: 0x00005292
		public bool AreAnySettingsActive
		{
			get
			{
				return this.m_DebugDisplaySettings.AreAnySettingsActive;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000709F File Offset: 0x0000529F
		public bool IsPostProcessingAllowed
		{
			get
			{
				return this.m_DebugDisplaySettings.IsPostProcessingAllowed;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000234 RID: 564 RVA: 0x000070AC File Offset: 0x000052AC
		public bool IsLightingActive
		{
			get
			{
				return this.m_DebugDisplaySettings.IsLightingActive;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000235 RID: 565 RVA: 0x000070BC File Offset: 0x000052BC
		internal bool IsActiveModeUnsupportedForDeferred
		{
			get
			{
				return this.m_DebugDisplaySettings.lightingSettings.lightingDebugMode != DebugLightingMode.None || this.m_DebugDisplaySettings.lightingSettings.lightingFeatureFlags != DebugLightingFeatureFlags.None || this.m_DebugDisplaySettings.renderingSettings.sceneOverrideMode != DebugSceneOverrideMode.None || this.m_DebugDisplaySettings.materialSettings.materialDebugMode != DebugMaterialMode.None || this.m_DebugDisplaySettings.materialSettings.vertexAttributeDebugMode != DebugVertexAttributeMode.None || this.m_DebugDisplaySettings.materialSettings.materialValidationMode != DebugMaterialValidationMode.None || this.m_DebugDisplaySettings.renderingSettings.mipInfoMode > DebugMipInfoMode.None;
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000714A File Offset: 0x0000534A
		public bool TryGetScreenClearColor(ref Color color)
		{
			return this.m_DebugDisplaySettings.TryGetScreenClearColor(ref color);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00007158 File Offset: 0x00005358
		internal Material ReplacementMaterial
		{
			get
			{
				return this.m_ReplacementMaterial;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00007160 File Offset: 0x00005360
		internal UniversalRenderPipelineDebugDisplaySettings DebugDisplaySettings
		{
			get
			{
				return this.m_DebugDisplaySettings;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00007168 File Offset: 0x00005368
		internal ref RTHandle DebugScreenColorHandle
		{
			get
			{
				return ref this.m_DebugScreenColorHandle;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00007170 File Offset: 0x00005370
		internal ref RTHandle DebugScreenDepthHandle
		{
			get
			{
				return ref this.m_DebugScreenDepthHandle;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00007178 File Offset: 0x00005378
		internal HDRDebugViewPass hdrDebugViewPass
		{
			get
			{
				return this.m_HDRDebugViewPass;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00007180 File Offset: 0x00005380
		internal bool HDRDebugViewIsActive(bool resolveFinalTarget)
		{
			return this.DebugDisplaySettings.lightingSettings.hdrDebugMode > HDRDebugMode.None && resolveFinalTarget;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00007197 File Offset: 0x00005397
		internal bool WriteToDebugScreenTexture(bool resolveFinalTarget)
		{
			return this.HDRDebugViewIsActive(resolveFinalTarget);
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600023E RID: 574 RVA: 0x000071A0 File Offset: 0x000053A0
		internal bool IsScreenClearNeeded
		{
			get
			{
				Color color = Color.black;
				return this.TryGetScreenClearColor(ref color);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000071BB File Offset: 0x000053BB
		internal bool IsRenderPassSupported
		{
			get
			{
				return this.RenderingSettings.sceneOverrideMode == DebugSceneOverrideMode.None || this.RenderingSettings.sceneOverrideMode == DebugSceneOverrideMode.Overdraw;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000071DA File Offset: 0x000053DA
		internal int stpDebugViewIndex
		{
			get
			{
				return this.RenderingSettings.stpDebugViewIndex;
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000071E8 File Offset: 0x000053E8
		internal DebugHandler()
		{
			this.m_DebugDisplaySettings = DebugDisplaySettings<UniversalRenderPipelineDebugDisplaySettings>.Instance;
			UniversalRenderPipelineDebugShaders shaders;
			if (GraphicsSettings.TryGetRenderPipelineSettings<UniversalRenderPipelineDebugShaders>(out shaders))
			{
				this.m_ReplacementMaterial = ((shaders.debugReplacementPS != null) ? CoreUtils.CreateEngineMaterial(shaders.debugReplacementPS) : null);
				this.m_HDRDebugViewMaterial = ((shaders.hdrDebugViewPS != null) ? CoreUtils.CreateEngineMaterial(shaders.hdrDebugViewPS) : null);
			}
			this.m_HDRDebugViewPass = new HDRDebugViewPass(this.m_HDRDebugViewMaterial);
			this.m_RuntimeTextures = GraphicsSettings.GetRenderPipelineSettings<UniversalRenderPipelineRuntimeTextures>();
			if (this.m_RuntimeTextures != null)
			{
				this.m_DebugFontTexture = RTHandles.Alloc(this.m_RuntimeTextures.debugFontTexture);
			}
			this.m_debugDisplayConstant = new GraphicsBuffer(GraphicsBuffer.Target.Constant, 32, Marshal.SizeOf(typeof(Vector4)));
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000072C4 File Offset: 0x000054C4
		public void Dispose()
		{
			this.m_HDRDebugViewPass.Dispose();
			RTHandle debugScreenColorHandle = this.m_DebugScreenColorHandle;
			if (debugScreenColorHandle != null)
			{
				debugScreenColorHandle.Release();
			}
			RTHandle debugScreenDepthHandle = this.m_DebugScreenDepthHandle;
			if (debugScreenDepthHandle != null)
			{
				debugScreenDepthHandle.Release();
			}
			RTHandle debugFontTexture = this.m_DebugFontTexture;
			if (debugFontTexture != null)
			{
				debugFontTexture.Release();
			}
			this.m_debugDisplayConstant.Dispose();
			CoreUtils.Destroy(this.m_HDRDebugViewMaterial);
			CoreUtils.Destroy(this.m_ReplacementMaterial);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00007330 File Offset: 0x00005530
		internal bool IsActiveForCamera(bool isPreviewCamera)
		{
			return !isPreviewCamera && this.AreAnySettingsActive;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00007340 File Offset: 0x00005540
		internal bool TryGetFullscreenDebugMode(out DebugFullScreenMode debugFullScreenMode)
		{
			int num;
			return this.TryGetFullscreenDebugMode(out debugFullScreenMode, out num);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00007356 File Offset: 0x00005556
		internal bool TryGetFullscreenDebugMode(out DebugFullScreenMode debugFullScreenMode, out int textureHeightPercent)
		{
			debugFullScreenMode = this.RenderingSettings.fullScreenDebugMode;
			textureHeightPercent = this.RenderingSettings.fullScreenDebugModeOutputSizeScreenPercent;
			return debugFullScreenMode > DebugFullScreenMode.None;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007377 File Offset: 0x00005577
		internal static void ConfigureColorDescriptorForDebugScreen(ref RenderTextureDescriptor descriptor, int cameraWidth, int cameraHeight)
		{
			descriptor.width = cameraWidth;
			descriptor.height = cameraHeight;
			descriptor.useMipMap = false;
			descriptor.autoGenerateMips = false;
			descriptor.useDynamicScale = true;
			descriptor.depthStencilFormat = GraphicsFormat.None;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000073A3 File Offset: 0x000055A3
		internal static void ConfigureDepthDescriptorForDebugScreen(ref RenderTextureDescriptor descriptor, GraphicsFormat depthStencilFormat, int cameraWidth, int cameraHeight)
		{
			descriptor.width = cameraWidth;
			descriptor.height = cameraHeight;
			descriptor.useMipMap = false;
			descriptor.autoGenerateMips = false;
			descriptor.useDynamicScale = true;
			descriptor.depthStencilFormat = depthStencilFormat;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000073D0 File Offset: 0x000055D0
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal void SetupShaderProperties(RasterCommandBuffer cmd, int passIndex = 0)
		{
			if (this.LightingSettings.lightingDebugMode == DebugLightingMode.ShadowCascades)
			{
				cmd.EnableShaderKeyword("_DEBUG_ENVIRONMENTREFLECTIONS_OFF");
			}
			else
			{
				cmd.DisableShaderKeyword("_DEBUG_ENVIRONMENTREFLECTIONS_OFF");
			}
			this.m_debugDisplayConstant.SetData(this.MaterialSettings.debugRenderingLayersColors, 0, 0, 32);
			cmd.SetGlobalConstantBuffer(this.m_debugDisplayConstant, "_DebugDisplayConstant", 0, this.m_debugDisplayConstant.count * this.m_debugDisplayConstant.stride);
			if (this.MaterialSettings.renderingLayersSelectedLight)
			{
				cmd.SetGlobalInt("_DebugRenderingLayerMask", (int)this.MaterialSettings.GetDebugLightLayersMask());
			}
			else
			{
				cmd.SetGlobalInt("_DebugRenderingLayerMask", (int)this.MaterialSettings.renderingLayerMask);
			}
			switch (this.RenderingSettings.sceneOverrideMode)
			{
			case DebugSceneOverrideMode.Overdraw:
			{
				float value = 1f / (float)this.RenderingSettings.maxOverdrawCount;
				cmd.SetGlobalColor(DebugHandler.k_DebugColorPropertyId, new Color(value, value, value, 1f));
				break;
			}
			case DebugSceneOverrideMode.Wireframe:
				cmd.SetGlobalColor(DebugHandler.k_DebugColorPropertyId, Color.black);
				break;
			case DebugSceneOverrideMode.SolidWireframe:
				cmd.SetGlobalColor(DebugHandler.k_DebugColorPropertyId, (passIndex == 0) ? Color.white : Color.black);
				break;
			case DebugSceneOverrideMode.ShadedWireframe:
				if (passIndex == 0)
				{
					cmd.SetKeyword(in ShaderGlobalKeywords.DEBUG_DISPLAY, false);
				}
				else if (passIndex == 1)
				{
					cmd.SetGlobalColor(DebugHandler.k_DebugColorPropertyId, Color.black);
					cmd.SetKeyword(in ShaderGlobalKeywords.DEBUG_DISPLAY, true);
				}
				break;
			}
			DebugMaterialValidationMode materialValidationMode = this.MaterialSettings.materialValidationMode;
			if (materialValidationMode == DebugMaterialValidationMode.Albedo)
			{
				cmd.SetGlobalFloat(DebugHandler.k_DebugValidateAlbedoMinLuminanceId, this.MaterialSettings.albedoMinLuminance);
				cmd.SetGlobalFloat(DebugHandler.k_DebugValidateAlbedoMaxLuminanceId, this.MaterialSettings.albedoMaxLuminance);
				cmd.SetGlobalFloat(DebugHandler.k_DebugValidateAlbedoSaturationToleranceId, this.MaterialSettings.albedoSaturationTolerance);
				cmd.SetGlobalFloat(DebugHandler.k_DebugValidateAlbedoHueToleranceId, this.MaterialSettings.albedoHueTolerance);
				cmd.SetGlobalColor(DebugHandler.k_DebugValidateAlbedoCompareColorId, this.MaterialSettings.albedoCompareColor.linear);
				return;
			}
			if (materialValidationMode != DebugMaterialValidationMode.Metallic)
			{
				return;
			}
			cmd.SetGlobalFloat(DebugHandler.k_DebugValidateMetallicMinValueId, this.MaterialSettings.metallicMinValue);
			cmd.SetGlobalFloat(DebugHandler.k_DebugValidateMetallicMaxValueId, this.MaterialSettings.metallicMaxValue);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000075F0 File Offset: 0x000057F0
		internal void SetDebugRenderTarget(RTHandle renderTarget, Rect displayRect, bool supportsStereo, Vector4 dataRangeRemap)
		{
			this.m_HasDebugRenderTarget = true;
			this.m_DebugRenderTargetSupportsStereo = supportsStereo;
			this.m_DebugRenderTarget = renderTarget;
			this.m_DebugRenderTargetPixelRect = new Vector4(displayRect.x, displayRect.y, displayRect.width, displayRect.height);
			this.m_DebugRenderTargetRangeRemap = dataRangeRemap;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00007641 File Offset: 0x00005841
		internal void ResetDebugRenderTarget()
		{
			this.m_HasDebugRenderTarget = false;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000764C File Offset: 0x0000584C
		private DebugHandler.DebugFinalValidationPassData InitDebugFinalValidationPassData(DebugHandler.DebugFinalValidationPassData passData, UniversalCameraData cameraData, bool isFinalPass)
		{
			passData.isFinalPass = isFinalPass;
			passData.resolveFinalTarget = cameraData.resolveFinalTarget;
			passData.isActiveForCamera = this.IsActiveForCamera(cameraData.isPreviewCamera);
			passData.hasDebugRenderTarget = this.m_HasDebugRenderTarget;
			passData.debugRenderTargetHandle = TextureHandle.nullHandle;
			passData.debugTexturePropertyId = (this.m_DebugRenderTargetSupportsStereo ? DebugHandler.k_DebugTexturePropertyId : DebugHandler.k_DebugTextureNoStereoPropertyId);
			passData.debugRenderTargetPixelRect = this.m_DebugRenderTargetPixelRect;
			passData.debugRenderTargetSupportsStereo = (this.m_DebugRenderTargetSupportsStereo ? 1 : 0);
			passData.debugRenderTargetRangeRemap = this.m_DebugRenderTargetRangeRemap;
			passData.debugFontTextureHandle = TextureHandle.nullHandle;
			passData.renderingSettings = this.RenderingSettings;
			return passData;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000076F4 File Offset: 0x000058F4
		private static void UpdateShaderGlobalPropertiesForFinalValidationPass(RasterCommandBuffer cmd, DebugHandler.DebugFinalValidationPassData data)
		{
			if (!data.isFinalPass || !data.resolveFinalTarget)
			{
				cmd.SetKeyword(in ShaderGlobalKeywords.DEBUG_DISPLAY, false);
				return;
			}
			if (data.isActiveForCamera)
			{
				cmd.SetKeyword(in ShaderGlobalKeywords.DEBUG_DISPLAY, true);
			}
			else
			{
				cmd.SetKeyword(in ShaderGlobalKeywords.DEBUG_DISPLAY, false);
			}
			if (data.hasDebugRenderTarget)
			{
				if (data.debugRenderTargetHandle.IsValid())
				{
					cmd.SetGlobalTexture(data.debugTexturePropertyId, data.debugRenderTargetHandle);
				}
				cmd.SetGlobalVector(DebugHandler.k_DebugTextureDisplayRect, data.debugRenderTargetPixelRect);
				cmd.SetGlobalInteger(DebugHandler.k_DebugRenderTargetSupportsStereo, data.debugRenderTargetSupportsStereo);
				cmd.SetGlobalVector(DebugHandler.k_DebugRenderTargetRangeRemap, data.debugRenderTargetRangeRemap);
			}
			DebugDisplaySettingsRendering renderingSettings = data.renderingSettings;
			if (renderingSettings.validationMode == DebugValidationMode.HighlightOutsideOfRange)
			{
				cmd.SetGlobalInteger(DebugHandler.k_ValidationChannelsId, (int)renderingSettings.validationChannels);
				cmd.SetGlobalFloat(DebugHandler.k_RangeMinimumId, renderingSettings.validationRangeMin);
				cmd.SetGlobalFloat(DebugHandler.k_RangeMaximumId, renderingSettings.validationRangeMax);
			}
			if (renderingSettings.mipInfoMode != DebugMipInfoMode.None)
			{
				cmd.SetGlobalTexture(DebugHandler.k_DebugFontId, data.debugFontTextureHandle);
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000077FC File Offset: 0x000059FC
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal void UpdateShaderGlobalPropertiesForFinalValidationPass(CommandBuffer cmd, UniversalCameraData cameraData, bool isFinalPass)
		{
			DebugHandler.UpdateShaderGlobalPropertiesForFinalValidationPass(CommandBufferHelpers.GetRasterCommandBuffer(cmd), this.InitDebugFinalValidationPassData(this.s_DebugFinalValidationPassData, cameraData, isFinalPass));
			cmd.SetGlobalTexture(this.s_DebugFinalValidationPassData.debugTexturePropertyId, this.m_DebugRenderTarget);
			if (this.RenderingSettings.mipInfoMode != DebugMipInfoMode.None)
			{
				cmd.SetGlobalTexture(DebugHandler.k_DebugFontId, this.m_RuntimeTextures.debugFontTexture);
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00007868 File Offset: 0x00005A68
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal void UpdateShaderGlobalPropertiesForFinalValidationPass(RenderGraph renderGraph, UniversalCameraData cameraData, bool isFinalPass)
		{
			DebugHandler.DebugFinalValidationPassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<DebugHandler.DebugFinalValidationPassData>("UpdateShaderGlobalPropertiesForFinalValidationPass", out passData, DebugHandler.s_DebugFinalValidationSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Debug/DebugHandler.cs", 431))
			{
				this.InitDebugFinalValidationPassData(passData, cameraData, isFinalPass);
				if (this.m_DebugRenderTarget != null)
				{
					passData.debugRenderTargetHandle = renderGraph.ImportTexture(this.m_DebugRenderTarget);
				}
				if (this.m_DebugFontTexture != null)
				{
					passData.debugFontTextureHandle = renderGraph.ImportTexture(this.m_DebugFontTexture);
				}
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				if (passData.debugRenderTargetHandle.IsValid())
				{
					builder.UseTexture(in passData.debugRenderTargetHandle, AccessFlags.Read);
					builder.SetGlobalTextureAfterPass(in passData.debugRenderTargetHandle, passData.debugTexturePropertyId);
				}
				if (passData.debugFontTextureHandle.IsValid())
				{
					builder.UseTexture(in passData.debugFontTextureHandle, AccessFlags.Read);
					builder.SetGlobalTextureAfterPass(in passData.debugFontTextureHandle, DebugHandler.k_DebugFontId);
				}
				builder.SetRenderFunc<DebugHandler.DebugFinalValidationPassData>(delegate(DebugHandler.DebugFinalValidationPassData data, RasterGraphContext context)
				{
					DebugHandler.UpdateShaderGlobalPropertiesForFinalValidationPass(context.cmd, data);
				});
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00007978 File Offset: 0x00005B78
		private DebugHandler.DebugSetupPassData InitDebugSetupPassData(DebugHandler.DebugSetupPassData passData, bool isPreviewCamera)
		{
			passData.isActiveForCamera = this.IsActiveForCamera(isPreviewCamera);
			passData.materialSettings = this.MaterialSettings;
			passData.renderingSettings = this.RenderingSettings;
			passData.lightingSettings = this.LightingSettings;
			return passData;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000079AC File Offset: 0x00005BAC
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		private static void Setup(RasterCommandBuffer cmd, DebugHandler.DebugSetupPassData passData)
		{
			if (passData.isActiveForCamera)
			{
				cmd.SetKeyword(in ShaderGlobalKeywords.DEBUG_DISPLAY, true);
				cmd.SetGlobalFloat(DebugHandler.k_DebugMaterialModeId, (float)passData.materialSettings.materialDebugMode);
				cmd.SetGlobalFloat(DebugHandler.k_DebugVertexAttributeModeId, (float)passData.materialSettings.vertexAttributeDebugMode);
				cmd.SetGlobalInteger(DebugHandler.k_DebugMaterialValidationModeId, (int)passData.materialSettings.materialValidationMode);
				cmd.SetGlobalInteger(DebugHandler.k_DebugMipInfoModeId, (int)passData.renderingSettings.mipInfoMode);
				cmd.SetGlobalInteger(DebugHandler.k_DebugMipMapStatusModeId, (int)passData.renderingSettings.mipDebugStatusMode);
				cmd.SetGlobalInteger(DebugHandler.k_DebugMipMapShowStatusCodeId, passData.renderingSettings.mipDebugStatusShowCode ? 1 : 0);
				cmd.SetGlobalFloat(DebugHandler.k_DebugMipMapOpacityId, passData.renderingSettings.mipDebugOpacity);
				cmd.SetGlobalFloat(DebugHandler.k_DebugMipMapRecentlyUpdatedCooldownId, passData.renderingSettings.mipDebugRecentUpdateCooldown);
				cmd.SetGlobalFloat(DebugHandler.k_DebugMipMapTerrainTextureModeId, (float)passData.renderingSettings.mipDebugTerrainTexture);
				cmd.SetGlobalInteger(DebugHandler.k_DebugSceneOverrideModeId, (int)passData.renderingSettings.sceneOverrideMode);
				cmd.SetGlobalInteger(DebugHandler.k_DebugFullScreenModeId, (int)passData.renderingSettings.fullScreenDebugMode);
				cmd.SetGlobalInteger(DebugHandler.k_DebugMaxPixelCost, passData.renderingSettings.maxOverdrawCount);
				cmd.SetGlobalInteger(DebugHandler.k_DebugValidationModeId, (int)passData.renderingSettings.validationMode);
				cmd.SetGlobalColor(DebugHandler.k_DebugValidateBelowMinThresholdColorPropertyId, Color.red);
				cmd.SetGlobalColor(DebugHandler.k_DebugValidateAboveMaxThresholdColorPropertyId, Color.blue);
				cmd.SetGlobalFloat(DebugHandler.k_DebugLightingModeId, (float)passData.lightingSettings.lightingDebugMode);
				cmd.SetGlobalInteger(DebugHandler.k_DebugLightingFeatureFlagsId, (int)passData.lightingSettings.lightingFeatureFlags);
				cmd.SetGlobalColor(DebugHandler.k_DebugColorInvalidModePropertyId, Color.red);
				cmd.SetGlobalFloat(DebugHandler.k_DebugCurrentRealTimeId, Time.realtimeSinceStartup);
				return;
			}
			cmd.SetKeyword(in ShaderGlobalKeywords.DEBUG_DISPLAY, false);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000217F File Offset: 0x0000037F
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal void Setup(CommandBuffer cmd, bool isPreviewCamera)
		{
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00007B74 File Offset: 0x00005D74
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal void Setup(RenderGraph renderGraph, bool isPreviewCamera)
		{
			DebugHandler.DebugSetupPassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<DebugHandler.DebugSetupPassData>(DebugHandler.s_DebugSetupSampler.name, out passData, DebugHandler.s_DebugSetupSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Debug/DebugHandler.cs", 538))
			{
				this.InitDebugSetupPassData(passData, isPreviewCamera);
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				builder.SetRenderFunc<DebugHandler.DebugSetupPassData>(delegate(DebugHandler.DebugSetupPassData data, RasterGraphContext context)
				{
				});
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00007BFC File Offset: 0x00005DFC
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal void Render(RenderGraph renderGraph, UniversalCameraData cameraData, TextureHandle srcColor, TextureHandle overlayTexture, TextureHandle dstColor)
		{
			if (this.IsActiveForCamera(cameraData.isPreviewCamera) && this.HDRDebugViewIsActive(cameraData.resolveFinalTarget))
			{
				this.m_HDRDebugViewPass.RenderHDRDebug(renderGraph, cameraData, srcColor, overlayTexture, dstColor, this.LightingSettings.hdrDebugMode);
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00007C37 File Offset: 0x00005E37
		internal DebugRendererLists CreateRendererListsWithDebugRenderState(ScriptableRenderContext context, ref CullingResults cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref RenderStateBlock renderStateBlock)
		{
			DebugRendererLists debugRendererLists = new DebugRendererLists(this, filteringSettings);
			debugRendererLists.CreateRendererListsWithDebugRenderState(context, ref cullResults, ref drawingSettings, ref filteringSettings, ref renderStateBlock);
			return debugRendererLists;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00007C53 File Offset: 0x00005E53
		internal DebugRendererLists CreateRendererListsWithDebugRenderState(RenderGraph renderGraph, ref CullingResults cullResults, ref DrawingSettings drawingSettings, ref FilteringSettings filteringSettings, ref RenderStateBlock renderStateBlock)
		{
			DebugRendererLists debugRendererLists = new DebugRendererLists(this, filteringSettings);
			debugRendererLists.CreateRendererListsWithDebugRenderState(renderGraph, ref cullResults, ref drawingSettings, ref filteringSettings, ref renderStateBlock);
			return debugRendererLists;
		}

		// Token: 0x040001B4 RID: 436
		private static readonly int k_DebugColorInvalidModePropertyId = Shader.PropertyToID("_DebugColorInvalidMode");

		// Token: 0x040001B5 RID: 437
		private static readonly int k_DebugCurrentRealTimeId = Shader.PropertyToID("_DebugCurrentRealTime");

		// Token: 0x040001B6 RID: 438
		private static readonly int k_DebugColorPropertyId = Shader.PropertyToID("_DebugColor");

		// Token: 0x040001B7 RID: 439
		private static readonly int k_DebugTexturePropertyId = Shader.PropertyToID("_DebugTexture");

		// Token: 0x040001B8 RID: 440
		private static readonly int k_DebugFontId = Shader.PropertyToID("_DebugFont");

		// Token: 0x040001B9 RID: 441
		private static readonly int k_DebugTextureNoStereoPropertyId = Shader.PropertyToID("_DebugTextureNoStereo");

		// Token: 0x040001BA RID: 442
		private static readonly int k_DebugTextureDisplayRect = Shader.PropertyToID("_DebugTextureDisplayRect");

		// Token: 0x040001BB RID: 443
		private static readonly int k_DebugRenderTargetSupportsStereo = Shader.PropertyToID("_DebugRenderTargetSupportsStereo");

		// Token: 0x040001BC RID: 444
		private static readonly int k_DebugRenderTargetRangeRemap = Shader.PropertyToID("_DebugRenderTargetRangeRemap");

		// Token: 0x040001BD RID: 445
		private static readonly int k_DebugMaterialModeId = Shader.PropertyToID("_DebugMaterialMode");

		// Token: 0x040001BE RID: 446
		private static readonly int k_DebugVertexAttributeModeId = Shader.PropertyToID("_DebugVertexAttributeMode");

		// Token: 0x040001BF RID: 447
		private static readonly int k_DebugMaterialValidationModeId = Shader.PropertyToID("_DebugMaterialValidationMode");

		// Token: 0x040001C0 RID: 448
		private static readonly int k_DebugMipInfoModeId = Shader.PropertyToID("_DebugMipInfoMode");

		// Token: 0x040001C1 RID: 449
		private static readonly int k_DebugMipMapStatusModeId = Shader.PropertyToID("_DebugMipMapStatusMode");

		// Token: 0x040001C2 RID: 450
		private static readonly int k_DebugMipMapShowStatusCodeId = Shader.PropertyToID("_DebugMipMapShowStatusCode");

		// Token: 0x040001C3 RID: 451
		private static readonly int k_DebugMipMapOpacityId = Shader.PropertyToID("_DebugMipMapOpacity");

		// Token: 0x040001C4 RID: 452
		private static readonly int k_DebugMipMapRecentlyUpdatedCooldownId = Shader.PropertyToID("_DebugMipMapRecentlyUpdatedCooldown");

		// Token: 0x040001C5 RID: 453
		private static readonly int k_DebugMipMapTerrainTextureModeId = Shader.PropertyToID("_DebugMipMapTerrainTextureMode");

		// Token: 0x040001C6 RID: 454
		private static readonly int k_DebugSceneOverrideModeId = Shader.PropertyToID("_DebugSceneOverrideMode");

		// Token: 0x040001C7 RID: 455
		private static readonly int k_DebugFullScreenModeId = Shader.PropertyToID("_DebugFullScreenMode");

		// Token: 0x040001C8 RID: 456
		private static readonly int k_DebugValidationModeId = Shader.PropertyToID("_DebugValidationMode");

		// Token: 0x040001C9 RID: 457
		private static readonly int k_DebugValidateBelowMinThresholdColorPropertyId = Shader.PropertyToID("_DebugValidateBelowMinThresholdColor");

		// Token: 0x040001CA RID: 458
		private static readonly int k_DebugValidateAboveMaxThresholdColorPropertyId = Shader.PropertyToID("_DebugValidateAboveMaxThresholdColor");

		// Token: 0x040001CB RID: 459
		private static readonly int k_DebugMaxPixelCost = Shader.PropertyToID("_DebugMaxPixelCost");

		// Token: 0x040001CC RID: 460
		private static readonly int k_DebugLightingModeId = Shader.PropertyToID("_DebugLightingMode");

		// Token: 0x040001CD RID: 461
		private static readonly int k_DebugLightingFeatureFlagsId = Shader.PropertyToID("_DebugLightingFeatureFlags");

		// Token: 0x040001CE RID: 462
		private static readonly int k_DebugValidateAlbedoMinLuminanceId = Shader.PropertyToID("_DebugValidateAlbedoMinLuminance");

		// Token: 0x040001CF RID: 463
		private static readonly int k_DebugValidateAlbedoMaxLuminanceId = Shader.PropertyToID("_DebugValidateAlbedoMaxLuminance");

		// Token: 0x040001D0 RID: 464
		private static readonly int k_DebugValidateAlbedoSaturationToleranceId = Shader.PropertyToID("_DebugValidateAlbedoSaturationTolerance");

		// Token: 0x040001D1 RID: 465
		private static readonly int k_DebugValidateAlbedoHueToleranceId = Shader.PropertyToID("_DebugValidateAlbedoHueTolerance");

		// Token: 0x040001D2 RID: 466
		private static readonly int k_DebugValidateAlbedoCompareColorId = Shader.PropertyToID("_DebugValidateAlbedoCompareColor");

		// Token: 0x040001D3 RID: 467
		private static readonly int k_DebugValidateMetallicMinValueId = Shader.PropertyToID("_DebugValidateMetallicMinValue");

		// Token: 0x040001D4 RID: 468
		private static readonly int k_DebugValidateMetallicMaxValueId = Shader.PropertyToID("_DebugValidateMetallicMaxValue");

		// Token: 0x040001D5 RID: 469
		private static readonly int k_ValidationChannelsId = Shader.PropertyToID("_ValidationChannels");

		// Token: 0x040001D6 RID: 470
		private static readonly int k_RangeMinimumId = Shader.PropertyToID("_RangeMinimum");

		// Token: 0x040001D7 RID: 471
		private static readonly int k_RangeMaximumId = Shader.PropertyToID("_RangeMaximum");

		// Token: 0x040001D8 RID: 472
		private static readonly ProfilingSampler s_DebugSetupSampler = new ProfilingSampler("Setup Debug Properties");

		// Token: 0x040001D9 RID: 473
		private static readonly ProfilingSampler s_DebugFinalValidationSampler = new ProfilingSampler("UpdateShaderGlobalPropertiesForFinalValidationPass");

		// Token: 0x040001DA RID: 474
		private DebugHandler.DebugSetupPassData s_DebugSetupPassData = new DebugHandler.DebugSetupPassData();

		// Token: 0x040001DB RID: 475
		private DebugHandler.DebugFinalValidationPassData s_DebugFinalValidationPassData = new DebugHandler.DebugFinalValidationPassData();

		// Token: 0x040001DC RID: 476
		private readonly Material m_ReplacementMaterial;

		// Token: 0x040001DD RID: 477
		private readonly Material m_HDRDebugViewMaterial;

		// Token: 0x040001DE RID: 478
		private HDRDebugViewPass m_HDRDebugViewPass;

		// Token: 0x040001DF RID: 479
		private RTHandle m_DebugScreenColorHandle;

		// Token: 0x040001E0 RID: 480
		private RTHandle m_DebugScreenDepthHandle;

		// Token: 0x040001E1 RID: 481
		private readonly UniversalRenderPipelineRuntimeTextures m_RuntimeTextures;

		// Token: 0x040001E2 RID: 482
		private bool m_HasDebugRenderTarget;

		// Token: 0x040001E3 RID: 483
		private bool m_DebugRenderTargetSupportsStereo;

		// Token: 0x040001E4 RID: 484
		private Vector4 m_DebugRenderTargetPixelRect;

		// Token: 0x040001E5 RID: 485
		private Vector4 m_DebugRenderTargetRangeRemap;

		// Token: 0x040001E6 RID: 486
		private RTHandle m_DebugRenderTarget;

		// Token: 0x040001E7 RID: 487
		private RTHandle m_DebugFontTexture;

		// Token: 0x040001E8 RID: 488
		private GraphicsBuffer m_debugDisplayConstant;

		// Token: 0x040001E9 RID: 489
		private readonly UniversalRenderPipelineDebugDisplaySettings m_DebugDisplaySettings;

		// Token: 0x02000069 RID: 105
		private class DebugFinalValidationPassData
		{
			// Token: 0x040001EA RID: 490
			public bool isFinalPass;

			// Token: 0x040001EB RID: 491
			public bool resolveFinalTarget;

			// Token: 0x040001EC RID: 492
			public bool isActiveForCamera;

			// Token: 0x040001ED RID: 493
			public bool hasDebugRenderTarget;

			// Token: 0x040001EE RID: 494
			public TextureHandle debugRenderTargetHandle;

			// Token: 0x040001EF RID: 495
			public int debugTexturePropertyId;

			// Token: 0x040001F0 RID: 496
			public Vector4 debugRenderTargetPixelRect;

			// Token: 0x040001F1 RID: 497
			public int debugRenderTargetSupportsStereo;

			// Token: 0x040001F2 RID: 498
			public Vector4 debugRenderTargetRangeRemap;

			// Token: 0x040001F3 RID: 499
			public TextureHandle debugFontTextureHandle;

			// Token: 0x040001F4 RID: 500
			public DebugDisplaySettingsRendering renderingSettings;
		}

		// Token: 0x0200006A RID: 106
		private class DebugSetupPassData
		{
			// Token: 0x040001F5 RID: 501
			public bool isActiveForCamera;

			// Token: 0x040001F6 RID: 502
			public DebugDisplaySettingsMaterial materialSettings;

			// Token: 0x040001F7 RID: 503
			public DebugDisplaySettingsRendering renderingSettings;

			// Token: 0x040001F8 RID: 504
			public DebugDisplaySettingsLighting lightingSettings;
		}
	}
}
