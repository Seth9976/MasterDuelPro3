using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200013F RID: 319
	internal class ScreenSpaceAmbientOcclusionPass : ScriptableRenderPass
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00021755 File Offset: 0x0001F955
		private bool isRendererDeferred
		{
			get
			{
				return this.m_Renderer != null && this.m_Renderer is UniversalRenderer && ((UniversalRenderer)this.m_Renderer).renderingModeActual == RenderingMode.Deferred;
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00021784 File Offset: 0x0001F984
		internal ScreenSpaceAmbientOcclusionPass()
		{
			this.m_CurrentSettings = new ScreenSpaceAmbientOcclusionSettings();
			this.m_PassData = new ScreenSpaceAmbientOcclusionPass.SSAOPassData();
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00021810 File Offset: 0x0001FA10
		internal bool Setup(ref ScreenSpaceAmbientOcclusionSettings featureSettings, ref ScriptableRenderer renderer, ref Material material, ref Texture2D[] blueNoiseTextures)
		{
			this.m_BlueNoiseTextures = blueNoiseTextures;
			this.m_Material = material;
			this.m_Renderer = renderer;
			this.m_CurrentSettings = featureSettings;
			if (this.isRendererDeferred)
			{
				base.renderPassEvent = (this.m_CurrentSettings.AfterOpaque ? RenderPassEvent.AfterRenderingOpaques : RenderPassEvent.AfterRenderingGbuffer);
				if (base.renderPassEvent == RenderPassEvent.AfterRenderingGbuffer)
				{
					base.breakGBufferAndDeferredRenderPass = true;
				}
				this.m_CurrentSettings.Source = ScreenSpaceAmbientOcclusionSettings.DepthSource.DepthNormals;
			}
			else
			{
				base.renderPassEvent = (this.m_CurrentSettings.AfterOpaque ? RenderPassEvent.BeforeRenderingTransparents : ((RenderPassEvent)201));
			}
			ScreenSpaceAmbientOcclusionSettings.DepthSource source = this.m_CurrentSettings.Source;
			if (source != ScreenSpaceAmbientOcclusionSettings.DepthSource.Depth)
			{
				if (source != ScreenSpaceAmbientOcclusionSettings.DepthSource.DepthNormals)
				{
					throw new ArgumentOutOfRangeException();
				}
				base.ConfigureInput(ScriptableRenderPassInput.Depth | ScriptableRenderPassInput.Normal);
			}
			else
			{
				base.ConfigureInput(ScriptableRenderPassInput.Depth);
			}
			switch (this.m_CurrentSettings.BlurQuality)
			{
			case ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions.High:
				this.m_BlurType = ScreenSpaceAmbientOcclusionPass.BlurTypes.Bilateral;
				break;
			case ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions.Medium:
				this.m_BlurType = ScreenSpaceAmbientOcclusionPass.BlurTypes.Gaussian;
				break;
			case ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions.Low:
				this.m_BlurType = ScreenSpaceAmbientOcclusionPass.BlurTypes.Kawase;
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return this.m_Material != null && this.m_CurrentSettings.Intensity > 0f && this.m_CurrentSettings.Radius > 0f && this.m_CurrentSettings.Falloff > 0f;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0002195A File Offset: 0x0001FB5A
		private static bool IsAfterOpaquePass(ref ScreenSpaceAmbientOcclusionPass.ShaderPasses pass)
		{
			return pass == ScreenSpaceAmbientOcclusionPass.ShaderPasses.BilateralAfterOpaque || pass == ScreenSpaceAmbientOcclusionPass.ShaderPasses.GaussianAfterOpaque || pass == ScreenSpaceAmbientOcclusionPass.ShaderPasses.KawaseAfterOpaque;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00021970 File Offset: 0x0001FB70
		private void SetupKeywordsAndParameters(ref ScreenSpaceAmbientOcclusionSettings settings, ref UniversalCameraData cameraData)
		{
			int eyeCount = ((cameraData.xr.enabled && cameraData.xr.singlePassEnabled) ? 2 : 1);
			for (int eyeIndex = 0; eyeIndex < eyeCount; eyeIndex++)
			{
				Matrix4x4 view = cameraData.GetViewMatrix(eyeIndex);
				Matrix4x4 proj = cameraData.GetProjectionMatrix(eyeIndex);
				this.m_CameraViewProjections[eyeIndex] = proj * view;
				Matrix4x4 cview = view;
				cview.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
				Matrix4x4 cviewProjInv = (proj * cview).inverse;
				Vector4 topLeftCorner = cviewProjInv.MultiplyPoint(new Vector4(-1f, 1f, -1f, 1f));
				Vector4 topRightCorner = cviewProjInv.MultiplyPoint(new Vector4(1f, 1f, -1f, 1f));
				Vector4 bottomLeftCorner = cviewProjInv.MultiplyPoint(new Vector4(-1f, -1f, -1f, 1f));
				Vector4 farCentre = cviewProjInv.MultiplyPoint(new Vector4(0f, 0f, 1f, 1f));
				this.m_CameraTopLeftCorner[eyeIndex] = topLeftCorner;
				this.m_CameraXExtent[eyeIndex] = topRightCorner - topLeftCorner;
				this.m_CameraYExtent[eyeIndex] = bottomLeftCorner - topLeftCorner;
				this.m_CameraZExtent[eyeIndex] = farCentre;
			}
			this.m_Material.SetVector(ScreenSpaceAmbientOcclusionPass.s_ProjectionParams2ID, new Vector4(1f / cameraData.camera.nearClipPlane, 0f, 0f, 0f));
			this.m_Material.SetMatrixArray(ScreenSpaceAmbientOcclusionPass.s_CameraViewProjectionsID, this.m_CameraViewProjections);
			this.m_Material.SetVectorArray(ScreenSpaceAmbientOcclusionPass.s_CameraViewTopLeftCornerID, this.m_CameraTopLeftCorner);
			this.m_Material.SetVectorArray(ScreenSpaceAmbientOcclusionPass.s_CameraViewXExtentID, this.m_CameraXExtent);
			this.m_Material.SetVectorArray(ScreenSpaceAmbientOcclusionPass.s_CameraViewYExtentID, this.m_CameraYExtent);
			this.m_Material.SetVectorArray(ScreenSpaceAmbientOcclusionPass.s_CameraViewZExtentID, this.m_CameraZExtent);
			if (settings.AOMethod == ScreenSpaceAmbientOcclusionSettings.AOMethodOptions.BlueNoise)
			{
				this.m_BlueNoiseTextureIndex = (this.m_BlueNoiseTextureIndex + 1) % this.m_BlueNoiseTextures.Length;
				Texture2D noiseTexture = this.m_BlueNoiseTextures[this.m_BlueNoiseTextureIndex];
				Vector4 blueNoiseParams = new Vector4((float)cameraData.pixelWidth / (float)this.m_BlueNoiseTextures[this.m_BlueNoiseTextureIndex].width, (float)cameraData.pixelHeight / (float)this.m_BlueNoiseTextures[this.m_BlueNoiseTextureIndex].height, Random.value, Random.value);
				this.m_Material.SetTexture(ScreenSpaceAmbientOcclusionPass.s_BlueNoiseTextureID, noiseTexture);
				this.m_Material.SetVector(ScreenSpaceAmbientOcclusionPass.s_SSAOBlueNoiseParamsID, blueNoiseParams);
			}
			ScreenSpaceAmbientOcclusionPass.SSAOMaterialParams matParams = new ScreenSpaceAmbientOcclusionPass.SSAOMaterialParams(ref settings, cameraData.camera.orthographic);
			int num = ((!this.m_SSAOParamsPrev.Equals(ref matParams)) ? 1 : 0);
			bool isParamsPropertySet = this.m_Material.HasProperty(ScreenSpaceAmbientOcclusionPass.s_SSAOParamsID);
			if (num == 0 && isParamsPropertySet)
			{
				return;
			}
			this.m_SSAOParamsPrev = matParams;
			CoreUtils.SetKeyword(this.m_Material, "_ORTHOGRAPHIC", matParams.orthographicCamera);
			CoreUtils.SetKeyword(this.m_Material, "_BLUE_NOISE", matParams.aoBlueNoise);
			CoreUtils.SetKeyword(this.m_Material, "_INTERLEAVED_GRADIENT", matParams.aoInterleavedGradient);
			CoreUtils.SetKeyword(this.m_Material, "_SAMPLE_COUNT_HIGH", matParams.sampleCountHigh);
			CoreUtils.SetKeyword(this.m_Material, "_SAMPLE_COUNT_MEDIUM", matParams.sampleCountMedium);
			CoreUtils.SetKeyword(this.m_Material, "_SAMPLE_COUNT_LOW", matParams.sampleCountLow);
			CoreUtils.SetKeyword(this.m_Material, "_SOURCE_DEPTH_NORMALS", matParams.sourceDepthNormals);
			CoreUtils.SetKeyword(this.m_Material, "_SOURCE_DEPTH_HIGH", matParams.sourceDepthHigh);
			CoreUtils.SetKeyword(this.m_Material, "_SOURCE_DEPTH_MEDIUM", matParams.sourceDepthMedium);
			CoreUtils.SetKeyword(this.m_Material, "_SOURCE_DEPTH_LOW", matParams.sourceDepthLow);
			this.m_Material.SetVector(ScreenSpaceAmbientOcclusionPass.s_SSAOParamsID, matParams.ssaoParams);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00021D80 File Offset: 0x0001FF80
		private void InitSSAOPassData(ref ScreenSpaceAmbientOcclusionPass.SSAOPassData data)
		{
			data.material = this.m_Material;
			data.BlurQuality = this.m_CurrentSettings.BlurQuality;
			data.afterOpaque = this.m_CurrentSettings.AfterOpaque;
			data.directLightingStrength = this.m_CurrentSettings.DirectLightingStrength;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00021DD0 File Offset: 0x0001FFD0
		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
			TextureHandle aoTexture;
			TextureHandle blurTexture;
			TextureHandle finalTexture;
			this.CreateRenderTextureHandles(renderGraph, resourceData, cameraData, out aoTexture, out blurTexture, out finalTexture);
			TextureHandle cameraDepthTexture = resourceData.cameraDepthTexture;
			TextureHandle cameraNormalsTexture = resourceData.cameraNormalsTexture;
			this.SetupKeywordsAndParameters(ref this.m_CurrentSettings, ref cameraData);
			ScreenSpaceAmbientOcclusionPass.SSAOPassData passData;
			using (IUnsafeRenderGraphBuilder builder = renderGraph.AddUnsafePass<ScreenSpaceAmbientOcclusionPass.SSAOPassData>("Blit SSAO", out passData, this.m_ProfilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/ScreenSpaceAmbientOcclusionPass.cs", 343))
			{
				builder.AllowGlobalStateModification(true);
				builder.AllowPassCulling(false);
				this.InitSSAOPassData(ref passData);
				passData.cameraColor = resourceData.cameraColor;
				passData.AOTexture = aoTexture;
				passData.finalTexture = finalTexture;
				passData.blurTexture = blurTexture;
				builder.UseTexture(in passData.AOTexture, AccessFlags.ReadWrite);
				if (passData.BlurQuality != ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions.Low)
				{
					builder.UseTexture(in passData.blurTexture, AccessFlags.ReadWrite);
				}
				if (cameraDepthTexture.IsValid())
				{
					builder.UseTexture(in cameraDepthTexture, AccessFlags.Read);
				}
				if (this.m_CurrentSettings.Source == ScreenSpaceAmbientOcclusionSettings.DepthSource.DepthNormals && cameraNormalsTexture.IsValid())
				{
					builder.UseTexture(in cameraNormalsTexture, AccessFlags.Read);
					passData.cameraNormalsTexture = cameraNormalsTexture;
				}
				if (!passData.afterOpaque && finalTexture.IsValid())
				{
					builder.UseTexture(in passData.finalTexture, AccessFlags.ReadWrite);
					builder.SetGlobalTextureAfterPass(in finalTexture, ScreenSpaceAmbientOcclusionPass.s_SSAOFinalTextureID);
				}
				builder.SetRenderFunc<ScreenSpaceAmbientOcclusionPass.SSAOPassData>(delegate(ScreenSpaceAmbientOcclusionPass.SSAOPassData data, UnsafeGraphContext rgContext)
				{
					CommandBuffer cmd = CommandBufferHelpers.GetNativeCommandBuffer(rgContext.cmd);
					RenderBufferLoadAction finalLoadAction = (data.afterOpaque ? RenderBufferLoadAction.Load : RenderBufferLoadAction.DontCare);
					if (data.cameraColor.IsValid())
					{
						PostProcessUtils.SetSourceSize(cmd, data.cameraColor);
					}
					if (data.cameraNormalsTexture.IsValid())
					{
						data.material.SetTexture(ScreenSpaceAmbientOcclusionPass.s_CameraNormalsTextureID, data.cameraNormalsTexture);
					}
					Blitter.BlitCameraTexture(cmd, data.AOTexture, data.AOTexture, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, data.material, 0);
					switch (data.BlurQuality)
					{
					case ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions.High:
						Blitter.BlitCameraTexture(cmd, data.AOTexture, data.blurTexture, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, data.material, 1);
						Blitter.BlitCameraTexture(cmd, data.blurTexture, data.AOTexture, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, data.material, 2);
						Blitter.BlitCameraTexture(cmd, data.AOTexture, data.finalTexture, finalLoadAction, RenderBufferStoreAction.Store, data.material, data.afterOpaque ? 4 : 3);
						break;
					case ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions.Medium:
						Blitter.BlitCameraTexture(cmd, data.AOTexture, data.blurTexture, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, data.material, 5);
						Blitter.BlitCameraTexture(cmd, data.blurTexture, data.finalTexture, finalLoadAction, RenderBufferStoreAction.Store, data.material, data.afterOpaque ? 7 : 6);
						break;
					case ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions.Low:
						Blitter.BlitCameraTexture(cmd, data.AOTexture, data.finalTexture, finalLoadAction, RenderBufferStoreAction.Store, data.material, data.afterOpaque ? 9 : 8);
						break;
					default:
						throw new ArgumentOutOfRangeException();
					}
					if (!data.afterOpaque)
					{
						rgContext.cmd.SetKeyword(in ShaderGlobalKeywords.ScreenSpaceOcclusion, true);
						rgContext.cmd.SetGlobalVector(ScreenSpaceAmbientOcclusionPass.s_AmbientOcclusionParamID, new Vector4(1f, 0f, 0f, data.directLightingStrength));
					}
				});
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00021F50 File Offset: 0x00020150
		private void CreateRenderTextureHandles(RenderGraph renderGraph, UniversalResourceData resourceData, UniversalCameraData cameraData, out TextureHandle aoTexture, out TextureHandle blurTexture, out TextureHandle finalTexture)
		{
			RenderTextureDescriptor finalTextureDescriptor = cameraData.cameraTargetDescriptor;
			finalTextureDescriptor.colorFormat = (this.m_SupportsR8RenderTextureFormat ? RenderTextureFormat.R8 : RenderTextureFormat.ARGB32);
			finalTextureDescriptor.depthStencilFormat = GraphicsFormat.None;
			finalTextureDescriptor.msaaSamples = 1;
			int downsampleDivider = (this.m_CurrentSettings.Downsample ? 2 : 1);
			bool useRedComponentOnly = this.m_SupportsR8RenderTextureFormat && this.m_BlurType > ScreenSpaceAmbientOcclusionPass.BlurTypes.Bilateral;
			RenderTextureDescriptor aoBlurDescriptor = finalTextureDescriptor;
			aoBlurDescriptor.colorFormat = (useRedComponentOnly ? RenderTextureFormat.R8 : RenderTextureFormat.ARGB32);
			aoBlurDescriptor.width /= downsampleDivider;
			aoBlurDescriptor.height /= downsampleDivider;
			aoTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, aoBlurDescriptor, "_SSAO_OcclusionTexture0", false, FilterMode.Bilinear, TextureWrapMode.Clamp);
			finalTexture = (this.m_CurrentSettings.AfterOpaque ? resourceData.activeColorTexture : UniversalRenderer.CreateRenderGraphTexture(renderGraph, finalTextureDescriptor, "_ScreenSpaceOcclusionTexture", false, FilterMode.Bilinear, TextureWrapMode.Clamp));
			if (this.m_CurrentSettings.BlurQuality != ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions.Low)
			{
				blurTexture = UniversalRenderer.CreateRenderGraphTexture(renderGraph, aoBlurDescriptor, "_SSAO_OcclusionTexture1", false, FilterMode.Bilinear, TextureWrapMode.Clamp);
			}
			else
			{
				blurTexture = TextureHandle.nullHandle;
			}
			if (!this.m_CurrentSettings.AfterOpaque)
			{
				resourceData.ssaoTexture = finalTexture;
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0002206C File Offset: 0x0002026C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			UniversalCameraData cameraData = renderingData.frameData.Get<UniversalCameraData>();
			this.InitSSAOPassData(ref this.m_PassData);
			this.SetupKeywordsAndParameters(ref this.m_CurrentSettings, ref cameraData);
			int downsampleDivider = (this.m_CurrentSettings.Downsample ? 2 : 1);
			RenderTextureDescriptor descriptor = *renderingData.cameraData.cameraTargetDescriptor;
			descriptor.msaaSamples = 1;
			descriptor.depthStencilFormat = GraphicsFormat.None;
			this.m_AOPassDescriptor = descriptor;
			this.m_AOPassDescriptor.width = this.m_AOPassDescriptor.width / downsampleDivider;
			this.m_AOPassDescriptor.height = this.m_AOPassDescriptor.height / downsampleDivider;
			this.m_AOPassDescriptor.colorFormat = ((this.m_SupportsR8RenderTextureFormat && this.m_BlurType > ScreenSpaceAmbientOcclusionPass.BlurTypes.Bilateral) ? RenderTextureFormat.R8 : RenderTextureFormat.ARGB32);
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_SSAOTextures[0], in this.m_AOPassDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_SSAO_OcclusionTexture0");
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_SSAOTextures[1], in this.m_AOPassDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_SSAO_OcclusionTexture1");
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_SSAOTextures[2], in this.m_AOPassDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_SSAO_OcclusionTexture2");
			this.m_AOPassDescriptor.width = this.m_AOPassDescriptor.width * downsampleDivider;
			this.m_AOPassDescriptor.height = this.m_AOPassDescriptor.height * downsampleDivider;
			this.m_AOPassDescriptor.colorFormat = (this.m_SupportsR8RenderTextureFormat ? RenderTextureFormat.R8 : RenderTextureFormat.ARGB32);
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_SSAOTextures[3], in this.m_AOPassDescriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_SSAO_OcclusionTexture");
			PostProcessUtils.SetSourceSize(cmd, this.m_SSAOTextures[3]);
			base.ConfigureTarget(this.m_CurrentSettings.AfterOpaque ? this.m_Renderer.cameraColorTargetHandle : this.m_SSAOTextures[3]);
			base.ConfigureClear(ClearFlag.None, Color.white);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0002223C File Offset: 0x0002043C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (this.m_Material == null)
			{
				Debug.LogErrorFormat("{0}.Execute(): Missing material. ScreenSpaceAmbientOcclusion pass will not execute. Check for missing reference in the renderer resources.", new object[] { base.GetType().Name });
				return;
			}
			CommandBuffer cmd = *renderingData.commandBuffer;
			using (new ProfilingScope(cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.SSAO)))
			{
				if (!this.m_CurrentSettings.AfterOpaque)
				{
					cmd.SetKeyword(in ShaderGlobalKeywords.ScreenSpaceOcclusion, true);
				}
				cmd.SetGlobalTexture("_ScreenSpaceOcclusionTexture", this.m_SSAOTextures[3]);
				bool isFoveatedEnabled = false;
				if (renderingData.cameraData.xr.supportsFoveatedRendering)
				{
					if (this.m_CurrentSettings.Downsample || SystemInfo.foveatedRenderingCaps.HasFlag(FoveatedRenderingCaps.NonUniformRaster) || (SystemInfo.foveatedRenderingCaps.HasFlag(FoveatedRenderingCaps.FoveationImage) && this.m_CurrentSettings.Source == ScreenSpaceAmbientOcclusionSettings.DepthSource.Depth))
					{
						cmd.SetFoveatedRenderingMode(FoveatedRenderingMode.Disabled);
					}
					else if (SystemInfo.foveatedRenderingCaps.HasFlag(FoveatedRenderingCaps.FoveationImage))
					{
						cmd.SetFoveatedRenderingMode(FoveatedRenderingMode.Enabled);
						isFoveatedEnabled = true;
					}
				}
				int[] textureIndices;
				ScreenSpaceAmbientOcclusionPass.ShaderPasses[] shaderPasses;
				ScreenSpaceAmbientOcclusionPass.GetPassOrder(this.m_BlurType, this.m_CurrentSettings.AfterOpaque, out textureIndices, out shaderPasses);
				RTHandle cameraDepthTargetHandle = renderingData.cameraData.renderer->cameraDepthTargetHandle;
				ScreenSpaceAmbientOcclusionPass.RenderAndSetBaseMap(ref cmd, ref renderingData, renderingData.cameraData.renderer, ref this.m_Material, ref cameraDepthTargetHandle, ref this.m_SSAOTextures[0], ScreenSpaceAmbientOcclusionPass.ShaderPasses.AmbientOcclusion);
				for (int i = 0; i < shaderPasses.Length; i++)
				{
					int baseMapIndex = textureIndices[i];
					int targetIndex = textureIndices[i + 1];
					ScreenSpaceAmbientOcclusionPass.RenderAndSetBaseMap(ref cmd, ref renderingData, renderingData.cameraData.renderer, ref this.m_Material, ref this.m_SSAOTextures[baseMapIndex], ref this.m_SSAOTextures[targetIndex], shaderPasses[i]);
				}
				cmd.SetGlobalVector(ScreenSpaceAmbientOcclusionPass.s_AmbientOcclusionParamID, new Vector4(1f, 0f, 0f, this.m_CurrentSettings.DirectLightingStrength));
				if (isFoveatedEnabled)
				{
					cmd.SetFoveatedRenderingMode(FoveatedRenderingMode.Disabled);
				}
			}
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00022454 File Offset: 0x00020654
		private static void RenderAndSetBaseMap(ref CommandBuffer cmd, ref RenderingData renderingData, ref ScriptableRenderer renderer, ref Material mat, ref RTHandle baseMap, ref RTHandle target, ScreenSpaceAmbientOcclusionPass.ShaderPasses pass)
		{
			if (ScreenSpaceAmbientOcclusionPass.IsAfterOpaquePass(ref pass))
			{
				Blitter.BlitCameraTexture(cmd, baseMap, renderer.cameraColorTargetHandle, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, mat, (int)pass);
				return;
			}
			if (baseMap.rt == null)
			{
				Vector2 viewportScale = (baseMap.useScaling ? new Vector2(baseMap.rtHandleProperties.rtHandleScale.x, baseMap.rtHandleProperties.rtHandleScale.y) : Vector2.one);
				CoreUtils.SetRenderTarget(cmd, target, ClearFlag.None, 0, CubemapFace.Unknown, -1);
				Blitter.BlitTexture(cmd, baseMap.nameID, viewportScale, mat, (int)pass);
				return;
			}
			Blitter.BlitCameraTexture(cmd, baseMap, target, mat, (int)pass);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00022508 File Offset: 0x00020708
		private static void GetPassOrder(ScreenSpaceAmbientOcclusionPass.BlurTypes blurType, bool isAfterOpaque, out int[] textureIndices, out ScreenSpaceAmbientOcclusionPass.ShaderPasses[] shaderPasses)
		{
			switch (blurType)
			{
			case ScreenSpaceAmbientOcclusionPass.BlurTypes.Bilateral:
				textureIndices = ScreenSpaceAmbientOcclusionPass.m_BilateralTexturesIndices;
				shaderPasses = (isAfterOpaque ? ScreenSpaceAmbientOcclusionPass.m_BilateralAfterOpaquePasses : ScreenSpaceAmbientOcclusionPass.m_BilateralPasses);
				return;
			case ScreenSpaceAmbientOcclusionPass.BlurTypes.Gaussian:
				textureIndices = ScreenSpaceAmbientOcclusionPass.m_GaussianTexturesIndices;
				shaderPasses = (isAfterOpaque ? ScreenSpaceAmbientOcclusionPass.m_GaussianAfterOpaquePasses : ScreenSpaceAmbientOcclusionPass.m_GaussianPasses);
				return;
			case ScreenSpaceAmbientOcclusionPass.BlurTypes.Kawase:
				textureIndices = ScreenSpaceAmbientOcclusionPass.m_KawaseTexturesIndices;
				shaderPasses = (isAfterOpaque ? ScreenSpaceAmbientOcclusionPass.m_KawaseAfterOpaquePasses : ScreenSpaceAmbientOcclusionPass.m_KawasePasses);
				return;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00022579 File Offset: 0x00020779
		public override void OnCameraCleanup(CommandBuffer cmd)
		{
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			if (!this.m_CurrentSettings.AfterOpaque)
			{
				cmd.SetKeyword(in ShaderGlobalKeywords.ScreenSpaceOcclusion, false);
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x000225A4 File Offset: 0x000207A4
		public void Dispose()
		{
			RTHandle rthandle = this.m_SSAOTextures[0];
			if (rthandle != null)
			{
				rthandle.Release();
			}
			RTHandle rthandle2 = this.m_SSAOTextures[1];
			if (rthandle2 != null)
			{
				rthandle2.Release();
			}
			RTHandle rthandle3 = this.m_SSAOTextures[2];
			if (rthandle3 != null)
			{
				rthandle3.Release();
			}
			RTHandle rthandle4 = this.m_SSAOTextures[3];
			if (rthandle4 != null)
			{
				rthandle4.Release();
			}
			this.m_SSAOParamsPrev = default(ScreenSpaceAmbientOcclusionPass.SSAOMaterialParams);
		}

		// Token: 0x04000741 RID: 1857
		private readonly bool m_SupportsR8RenderTextureFormat = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.R8);

		// Token: 0x04000742 RID: 1858
		private int m_BlueNoiseTextureIndex;

		// Token: 0x04000743 RID: 1859
		private Material m_Material;

		// Token: 0x04000744 RID: 1860
		private ScreenSpaceAmbientOcclusionPass.SSAOPassData m_PassData;

		// Token: 0x04000745 RID: 1861
		private Texture2D[] m_BlueNoiseTextures;

		// Token: 0x04000746 RID: 1862
		private Vector4[] m_CameraTopLeftCorner = new Vector4[2];

		// Token: 0x04000747 RID: 1863
		private Vector4[] m_CameraXExtent = new Vector4[2];

		// Token: 0x04000748 RID: 1864
		private Vector4[] m_CameraYExtent = new Vector4[2];

		// Token: 0x04000749 RID: 1865
		private Vector4[] m_CameraZExtent = new Vector4[2];

		// Token: 0x0400074A RID: 1866
		private RTHandle[] m_SSAOTextures = new RTHandle[4];

		// Token: 0x0400074B RID: 1867
		private ScreenSpaceAmbientOcclusionPass.BlurTypes m_BlurType;

		// Token: 0x0400074C RID: 1868
		private Matrix4x4[] m_CameraViewProjections = new Matrix4x4[2];

		// Token: 0x0400074D RID: 1869
		private ProfilingSampler m_ProfilingSampler = ProfilingSampler.Get<URPProfileId>(URPProfileId.SSAO);

		// Token: 0x0400074E RID: 1870
		private ScriptableRenderer m_Renderer;

		// Token: 0x0400074F RID: 1871
		private RenderTextureDescriptor m_AOPassDescriptor;

		// Token: 0x04000750 RID: 1872
		private ScreenSpaceAmbientOcclusionSettings m_CurrentSettings;

		// Token: 0x04000751 RID: 1873
		private const string k_SSAOTextureName = "_ScreenSpaceOcclusionTexture";

		// Token: 0x04000752 RID: 1874
		private const string k_AmbientOcclusionParamName = "_AmbientOcclusionParam";

		// Token: 0x04000753 RID: 1875
		internal static readonly int s_AmbientOcclusionParamID = Shader.PropertyToID("_AmbientOcclusionParam");

		// Token: 0x04000754 RID: 1876
		private static readonly int s_SSAOParamsID = Shader.PropertyToID("_SSAOParams");

		// Token: 0x04000755 RID: 1877
		private static readonly int s_SSAOBlueNoiseParamsID = Shader.PropertyToID("_SSAOBlueNoiseParams");

		// Token: 0x04000756 RID: 1878
		private static readonly int s_BlueNoiseTextureID = Shader.PropertyToID("_BlueNoiseTexture");

		// Token: 0x04000757 RID: 1879
		private static readonly int s_SSAOFinalTextureID = Shader.PropertyToID("_ScreenSpaceOcclusionTexture");

		// Token: 0x04000758 RID: 1880
		private static readonly int s_CameraViewXExtentID = Shader.PropertyToID("_CameraViewXExtent");

		// Token: 0x04000759 RID: 1881
		private static readonly int s_CameraViewYExtentID = Shader.PropertyToID("_CameraViewYExtent");

		// Token: 0x0400075A RID: 1882
		private static readonly int s_CameraViewZExtentID = Shader.PropertyToID("_CameraViewZExtent");

		// Token: 0x0400075B RID: 1883
		private static readonly int s_ProjectionParams2ID = Shader.PropertyToID("_ProjectionParams2");

		// Token: 0x0400075C RID: 1884
		private static readonly int s_CameraViewProjectionsID = Shader.PropertyToID("_CameraViewProjections");

		// Token: 0x0400075D RID: 1885
		private static readonly int s_CameraViewTopLeftCornerID = Shader.PropertyToID("_CameraViewTopLeftCorner");

		// Token: 0x0400075E RID: 1886
		private static readonly int s_CameraDepthTextureID = Shader.PropertyToID("_CameraDepthTexture");

		// Token: 0x0400075F RID: 1887
		private static readonly int s_CameraNormalsTextureID = Shader.PropertyToID("_CameraNormalsTexture");

		// Token: 0x04000760 RID: 1888
		private static readonly int[] m_BilateralTexturesIndices = new int[] { 0, 1, 2, 3 };

		// Token: 0x04000761 RID: 1889
		private static readonly ScreenSpaceAmbientOcclusionPass.ShaderPasses[] m_BilateralPasses = new ScreenSpaceAmbientOcclusionPass.ShaderPasses[]
		{
			ScreenSpaceAmbientOcclusionPass.ShaderPasses.BilateralBlurHorizontal,
			ScreenSpaceAmbientOcclusionPass.ShaderPasses.BilateralBlurVertical,
			ScreenSpaceAmbientOcclusionPass.ShaderPasses.BilateralBlurFinal
		};

		// Token: 0x04000762 RID: 1890
		private static readonly ScreenSpaceAmbientOcclusionPass.ShaderPasses[] m_BilateralAfterOpaquePasses = new ScreenSpaceAmbientOcclusionPass.ShaderPasses[]
		{
			ScreenSpaceAmbientOcclusionPass.ShaderPasses.BilateralBlurHorizontal,
			ScreenSpaceAmbientOcclusionPass.ShaderPasses.BilateralBlurVertical,
			ScreenSpaceAmbientOcclusionPass.ShaderPasses.BilateralAfterOpaque
		};

		// Token: 0x04000763 RID: 1891
		private static readonly int[] m_GaussianTexturesIndices = new int[] { 0, 1, 3, 3 };

		// Token: 0x04000764 RID: 1892
		private static readonly ScreenSpaceAmbientOcclusionPass.ShaderPasses[] m_GaussianPasses = new ScreenSpaceAmbientOcclusionPass.ShaderPasses[]
		{
			ScreenSpaceAmbientOcclusionPass.ShaderPasses.GaussianBlurHorizontal,
			ScreenSpaceAmbientOcclusionPass.ShaderPasses.GaussianBlurVertical
		};

		// Token: 0x04000765 RID: 1893
		private static readonly ScreenSpaceAmbientOcclusionPass.ShaderPasses[] m_GaussianAfterOpaquePasses = new ScreenSpaceAmbientOcclusionPass.ShaderPasses[]
		{
			ScreenSpaceAmbientOcclusionPass.ShaderPasses.GaussianBlurHorizontal,
			ScreenSpaceAmbientOcclusionPass.ShaderPasses.GaussianAfterOpaque
		};

		// Token: 0x04000766 RID: 1894
		private static readonly int[] m_KawaseTexturesIndices = new int[] { 0, 3 };

		// Token: 0x04000767 RID: 1895
		private static readonly ScreenSpaceAmbientOcclusionPass.ShaderPasses[] m_KawasePasses = new ScreenSpaceAmbientOcclusionPass.ShaderPasses[] { ScreenSpaceAmbientOcclusionPass.ShaderPasses.KawaseBlur };

		// Token: 0x04000768 RID: 1896
		private static readonly ScreenSpaceAmbientOcclusionPass.ShaderPasses[] m_KawaseAfterOpaquePasses = new ScreenSpaceAmbientOcclusionPass.ShaderPasses[] { ScreenSpaceAmbientOcclusionPass.ShaderPasses.KawaseAfterOpaque };

		// Token: 0x04000769 RID: 1897
		private ScreenSpaceAmbientOcclusionPass.SSAOMaterialParams m_SSAOParamsPrev;

		// Token: 0x02000140 RID: 320
		private enum BlurTypes
		{
			// Token: 0x0400076B RID: 1899
			Bilateral,
			// Token: 0x0400076C RID: 1900
			Gaussian,
			// Token: 0x0400076D RID: 1901
			Kawase
		}

		// Token: 0x02000141 RID: 321
		private enum ShaderPasses
		{
			// Token: 0x0400076F RID: 1903
			AmbientOcclusion,
			// Token: 0x04000770 RID: 1904
			BilateralBlurHorizontal,
			// Token: 0x04000771 RID: 1905
			BilateralBlurVertical,
			// Token: 0x04000772 RID: 1906
			BilateralBlurFinal,
			// Token: 0x04000773 RID: 1907
			BilateralAfterOpaque,
			// Token: 0x04000774 RID: 1908
			GaussianBlurHorizontal,
			// Token: 0x04000775 RID: 1909
			GaussianBlurVertical,
			// Token: 0x04000776 RID: 1910
			GaussianAfterOpaque,
			// Token: 0x04000777 RID: 1911
			KawaseBlur,
			// Token: 0x04000778 RID: 1912
			KawaseAfterOpaque
		}

		// Token: 0x02000142 RID: 322
		private struct SSAOMaterialParams
		{
			// Token: 0x0600071B RID: 1819 RVA: 0x00022788 File Offset: 0x00020988
			internal SSAOMaterialParams(ref ScreenSpaceAmbientOcclusionSettings settings, bool isOrthographic)
			{
				bool isUsingDepthNormals = settings.Source == ScreenSpaceAmbientOcclusionSettings.DepthSource.DepthNormals;
				float radiusMultiplier = ((settings.AOMethod == ScreenSpaceAmbientOcclusionSettings.AOMethodOptions.BlueNoise) ? 1.5f : 1f);
				this.orthographicCamera = isOrthographic;
				this.aoBlueNoise = settings.AOMethod == ScreenSpaceAmbientOcclusionSettings.AOMethodOptions.BlueNoise;
				this.aoInterleavedGradient = settings.AOMethod == ScreenSpaceAmbientOcclusionSettings.AOMethodOptions.InterleavedGradient;
				this.sampleCountHigh = settings.Samples == ScreenSpaceAmbientOcclusionSettings.AOSampleOption.High;
				this.sampleCountMedium = settings.Samples == ScreenSpaceAmbientOcclusionSettings.AOSampleOption.Medium;
				this.sampleCountLow = settings.Samples == ScreenSpaceAmbientOcclusionSettings.AOSampleOption.Low;
				this.sourceDepthNormals = settings.Source == ScreenSpaceAmbientOcclusionSettings.DepthSource.DepthNormals;
				this.sourceDepthHigh = !isUsingDepthNormals && settings.NormalSamples == ScreenSpaceAmbientOcclusionSettings.NormalQuality.High;
				this.sourceDepthMedium = !isUsingDepthNormals && settings.NormalSamples == ScreenSpaceAmbientOcclusionSettings.NormalQuality.Medium;
				this.sourceDepthLow = !isUsingDepthNormals && settings.NormalSamples == ScreenSpaceAmbientOcclusionSettings.NormalQuality.Low;
				this.ssaoParams = new Vector4(settings.Intensity, settings.Radius * radiusMultiplier, 1f / (float)(settings.Downsample ? 2 : 1), settings.Falloff);
			}

			// Token: 0x0600071C RID: 1820 RVA: 0x00022898 File Offset: 0x00020A98
			internal bool Equals(ref ScreenSpaceAmbientOcclusionPass.SSAOMaterialParams other)
			{
				return this.orthographicCamera == other.orthographicCamera && this.aoBlueNoise == other.aoBlueNoise && this.aoInterleavedGradient == other.aoInterleavedGradient && this.sampleCountHigh == other.sampleCountHigh && this.sampleCountMedium == other.sampleCountMedium && this.sampleCountLow == other.sampleCountLow && this.sourceDepthNormals == other.sourceDepthNormals && this.sourceDepthHigh == other.sourceDepthHigh && this.sourceDepthMedium == other.sourceDepthMedium && this.sourceDepthLow == other.sourceDepthLow && this.ssaoParams == other.ssaoParams;
			}

			// Token: 0x04000779 RID: 1913
			internal bool orthographicCamera;

			// Token: 0x0400077A RID: 1914
			internal bool aoBlueNoise;

			// Token: 0x0400077B RID: 1915
			internal bool aoInterleavedGradient;

			// Token: 0x0400077C RID: 1916
			internal bool sampleCountHigh;

			// Token: 0x0400077D RID: 1917
			internal bool sampleCountMedium;

			// Token: 0x0400077E RID: 1918
			internal bool sampleCountLow;

			// Token: 0x0400077F RID: 1919
			internal bool sourceDepthNormals;

			// Token: 0x04000780 RID: 1920
			internal bool sourceDepthHigh;

			// Token: 0x04000781 RID: 1921
			internal bool sourceDepthMedium;

			// Token: 0x04000782 RID: 1922
			internal bool sourceDepthLow;

			// Token: 0x04000783 RID: 1923
			internal Vector4 ssaoParams;
		}

		// Token: 0x02000143 RID: 323
		private class SSAOPassData
		{
			// Token: 0x04000784 RID: 1924
			internal bool afterOpaque;

			// Token: 0x04000785 RID: 1925
			internal ScreenSpaceAmbientOcclusionSettings.BlurQualityOptions BlurQuality;

			// Token: 0x04000786 RID: 1926
			internal Material material;

			// Token: 0x04000787 RID: 1927
			internal float directLightingStrength;

			// Token: 0x04000788 RID: 1928
			internal TextureHandle cameraColor;

			// Token: 0x04000789 RID: 1929
			internal TextureHandle AOTexture;

			// Token: 0x0400078A RID: 1930
			internal TextureHandle finalTexture;

			// Token: 0x0400078B RID: 1931
			internal TextureHandle blurTexture;

			// Token: 0x0400078C RID: 1932
			internal TextureHandle cameraNormalsTexture;
		}
	}
}
