using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200003D RID: 61
	internal class Render2DLightingPass : ScriptableRenderPass, IRenderPass2D
	{
		// Token: 0x06000175 RID: 373 RVA: 0x0000D2FD File Offset: 0x0000B4FD
		public Render2DLightingPass(Renderer2DData rendererData, Material blitMaterial, Material samplingMaterial, Texture2D fallOffLookup)
		{
			this.m_Renderer2DData = rendererData;
			this.m_BlitMaterial = blitMaterial;
			this.m_SamplingMaterial = samplingMaterial;
			this.m_FallOffLookup = fallOffLookup;
			this.m_CameraSortingLayerBoundsIndex = Render2DLightingPass.GetCameraSortingLayerBoundsIndex(this.m_Renderer2DData);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000D333 File Offset: 0x0000B533
		internal void Setup(bool useDepth)
		{
			this.m_NeedsDepth = useDepth;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000D33C File Offset: 0x0000B53C
		private unsafe void CopyCameraSortingLayerRenderTexture(ScriptableRenderContext context, RenderingData renderingData, RenderBufferStoreAction mainTargetStoreAction)
		{
			CommandBuffer cmd = *renderingData.commandBuffer;
			this.CreateCameraSortingLayerRenderTexture(renderingData, cmd, this.m_Renderer2DData.cameraSortingLayerDownsamplingMethod);
			Material copyMaterial = this.m_SamplingMaterial;
			int passIndex = 0;
			if (this.m_Renderer2DData.cameraSortingLayerDownsamplingMethod != Downsampling._4xBox)
			{
				copyMaterial = this.m_BlitMaterial;
				passIndex = ((base.colorAttachmentHandle.rt.filterMode == FilterMode.Bilinear) ? 1 : 0);
			}
			Blitter.BlitCameraTexture(cmd, base.colorAttachmentHandle, this.m_Renderer2DData.cameraSortingLayerRenderTarget, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, copyMaterial, passIndex);
			CoreUtils.SetRenderTarget(cmd, base.colorAttachmentHandle, RenderBufferLoadAction.Load, mainTargetStoreAction, base.depthAttachmentHandle, RenderBufferLoadAction.Load, mainTargetStoreAction, ClearFlag.None, Color.clear, 0, CubemapFace.Unknown, -1);
			cmd.SetGlobalTexture(this.m_Renderer2DData.cameraSortingLayerRenderTarget.name, this.m_Renderer2DData.cameraSortingLayerRenderTarget.nameID);
			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000D40C File Offset: 0x0000B60C
		public static short GetCameraSortingLayerBoundsIndex(Renderer2DData rendererData)
		{
			SortingLayer[] sortingLayers = Light2DManager.GetCachedSortingLayer();
			short i = 0;
			while ((int)i < sortingLayers.Length)
			{
				if (sortingLayers[(int)i].id == rendererData.cameraSortingLayerTextureBound)
				{
					return (short)sortingLayers[(int)i].value;
				}
				i += 1;
			}
			return short.MinValue;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000D458 File Offset: 0x0000B658
		private void DetermineWhenToResolve(int startIndex, int batchesDrawn, int batchCount, LayerBatch[] layerBatches, out int resolveDuringBatch, out bool resolveIsAfterCopy)
		{
			bool anyLightWithVolumetricShadows = false;
			List<Light2D> lights = this.m_Renderer2DData.lightCullResult.visibleLights;
			for (int i = 0; i < lights.Count; i++)
			{
				anyLightWithVolumetricShadows = lights[i].renderVolumetricShadows;
				if (anyLightWithVolumetricShadows)
				{
					break;
				}
			}
			int lastVolumetricLightBatch = -1;
			if (anyLightWithVolumetricShadows)
			{
				for (int j = startIndex + batchesDrawn - 1; j >= startIndex; j--)
				{
					if (layerBatches[j].lightStats.totalVolumetricUsage > 0)
					{
						lastVolumetricLightBatch = j;
						break;
					}
				}
			}
			if (this.m_Renderer2DData.useCameraSortingLayerTexture)
			{
				short cameraSortingLayerBoundsIndex = Render2DLightingPass.GetCameraSortingLayerBoundsIndex(this.m_Renderer2DData);
				int copyBatch = -1;
				for (int k = startIndex; k < startIndex + batchesDrawn; k++)
				{
					LayerBatch layerBatch = layerBatches[k];
					if (cameraSortingLayerBoundsIndex >= layerBatch.layerRange.lowerBound && cameraSortingLayerBoundsIndex <= layerBatch.layerRange.upperBound)
					{
						copyBatch = k;
						break;
					}
				}
				resolveIsAfterCopy = copyBatch > lastVolumetricLightBatch;
				resolveDuringBatch = (resolveIsAfterCopy ? copyBatch : lastVolumetricLightBatch);
				return;
			}
			resolveDuringBatch = lastVolumetricLightBatch;
			resolveIsAfterCopy = false;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000D54C File Offset: 0x0000B74C
		private unsafe void Render(ScriptableRenderContext context, CommandBuffer cmd, ref RenderingData renderingData, ref FilteringSettings filterSettings, DrawingSettings drawSettings)
		{
			DebugHandler activeDebugHandler = ScriptableRenderPass.GetActiveDebugHandler(renderingData.frameData.Get<UniversalCameraData>());
			if (activeDebugHandler != null)
			{
				UniversalRenderingData universalRenderingData = renderingData.universalRenderingData;
				RenderStateBlock renderStateBlock = default(RenderStateBlock);
				activeDebugHandler.CreateRendererListsWithDebugRenderState(context, ref universalRenderingData.cullResults, ref drawSettings, ref filterSettings, ref renderStateBlock).DrawWithRendererList(CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer));
				return;
			}
			RendererListParams param = new RendererListParams(*renderingData.cullResults, drawSettings, filterSettings);
			RendererList rl = context.CreateRendererList(ref param);
			cmd.DrawRendererList(rl);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		private int DrawLayerBatches(LayerBatch[] layerBatches, int batchCount, int startIndex, CommandBuffer cmd, ScriptableRenderContext context, ref RenderingData renderingData, ref FilteringSettings filterSettings, ref DrawingSettings normalsDrawSettings, ref DrawingSettings drawSettings, ref RenderTextureDescriptor desc)
		{
			DebugHandler activeDebugHandler = ScriptableRenderPass.GetActiveDebugHandler(renderingData.frameData.Get<UniversalCameraData>());
			bool drawLights = activeDebugHandler == null || activeDebugHandler.IsLightingActive;
			int batchesDrawn = 0;
			uint rtCount = 0U;
			bool normalsFirstClear = true;
			using (new ProfilingScope(cmd, Render2DLightingPass.m_ProfilingDrawLights))
			{
				for (int i = startIndex; i < batchCount; i++)
				{
					ref LayerBatch layerBatch = ref layerBatches[i];
					uint blendStyleMask = layerBatch.lightStats.blendStylesUsed;
					uint blendStyleCount = 0U;
					while (blendStyleMask > 0U)
					{
						blendStyleCount += blendStyleMask & 1U;
						blendStyleMask >>= 1;
					}
					rtCount += blendStyleCount;
					if (rtCount > LayerUtility.maxTextureCount)
					{
						break;
					}
					batchesDrawn++;
					if (layerBatch.useNormals)
					{
						filterSettings.sortingLayerRange = layerBatch.layerRange;
						RTHandle depthTarget = (this.m_NeedsDepth ? base.depthAttachmentHandle : null);
						this.RenderNormals(context, renderingData, normalsDrawSettings, filterSettings, depthTarget, normalsFirstClear);
						normalsFirstClear = false;
					}
					using (new ProfilingScope(cmd, Render2DLightingPass.m_ProfilingDrawLightTextures))
					{
						this.RenderLights(renderingData, cmd, ref layerBatch, ref desc);
					}
				}
			}
			bool msaaEnabled = renderingData.cameraData.cameraTargetDescriptor.msaaSamples > 1;
			bool isFinalBatchSet = startIndex + batchesDrawn >= batchCount;
			int resolveDuringBatch = -1;
			bool resolveIsAfterCopy = false;
			if (msaaEnabled && isFinalBatchSet)
			{
				this.DetermineWhenToResolve(startIndex, batchesDrawn, batchCount, layerBatches, out resolveDuringBatch, out resolveIsAfterCopy);
			}
			int blendStylesCount = this.m_Renderer2DData.lightBlendStyles.Length;
			using (new ProfilingScope(cmd, Render2DLightingPass.m_ProfilingDrawRenderers))
			{
				RenderBufferStoreAction initialStoreAction;
				if (msaaEnabled)
				{
					initialStoreAction = ((resolveDuringBatch < startIndex) ? RenderBufferStoreAction.Resolve : RenderBufferStoreAction.StoreAndResolve);
				}
				else
				{
					initialStoreAction = RenderBufferStoreAction.Store;
				}
				CoreUtils.SetRenderTarget(cmd, base.colorAttachmentHandle, RenderBufferLoadAction.Load, initialStoreAction, base.depthAttachmentHandle, RenderBufferLoadAction.Load, initialStoreAction, ClearFlag.None, Color.clear, 0, CubemapFace.Unknown, -1);
				for (int j = startIndex; j < startIndex + batchesDrawn; j++)
				{
					using (new ProfilingScope(cmd, Render2DLightingPass.m_ProfilingDrawLayerBatch))
					{
						LayerBatch layerBatch2 = layerBatches[j];
						if (layerBatch2.lightStats.useAnyLights)
						{
							for (int blendStyleIndex = 0; blendStyleIndex < blendStylesCount; blendStyleIndex++)
							{
								uint blendStyleMask2 = 1U << blendStyleIndex;
								bool blendStyleUsed = (layerBatch2.lightStats.blendStylesUsed & blendStyleMask2) > 0U;
								if (blendStyleUsed)
								{
									RenderTargetIdentifier identifier = layerBatch2.GetRTId(cmd, desc, blendStyleIndex);
									cmd.SetGlobalTexture(Render2DLightingPass.k_ShapeLightTextureIDs[blendStyleIndex], identifier);
								}
								RendererLighting.EnableBlendStyle(CommandBufferHelpers.GetRasterCommandBuffer(cmd), blendStyleIndex, blendStyleUsed);
							}
						}
						else
						{
							for (int blendStyleIndex2 = 0; blendStyleIndex2 < Render2DLightingPass.k_ShapeLightTextureIDs.Length; blendStyleIndex2++)
							{
								cmd.SetGlobalTexture(Render2DLightingPass.k_ShapeLightTextureIDs[blendStyleIndex2], Texture2D.blackTexture);
								RendererLighting.EnableBlendStyle(CommandBufferHelpers.GetRasterCommandBuffer(cmd), blendStyleIndex2, blendStyleIndex2 == 0);
							}
						}
						context.ExecuteCommandBuffer(cmd);
						cmd.Clear();
						short cameraSortingLayerBoundsIndex = Render2DLightingPass.GetCameraSortingLayerBoundsIndex(this.m_Renderer2DData);
						RenderBufferStoreAction copyStoreAction;
						if (msaaEnabled)
						{
							copyStoreAction = ((resolveDuringBatch == j && resolveIsAfterCopy) ? RenderBufferStoreAction.Resolve : RenderBufferStoreAction.StoreAndResolve);
						}
						else
						{
							copyStoreAction = RenderBufferStoreAction.Store;
						}
						if (cameraSortingLayerBoundsIndex >= layerBatch2.layerRange.lowerBound && cameraSortingLayerBoundsIndex < layerBatch2.layerRange.upperBound && this.m_Renderer2DData.useCameraSortingLayerTexture)
						{
							filterSettings.sortingLayerRange = new SortingLayerRange(layerBatch2.layerRange.lowerBound, cameraSortingLayerBoundsIndex);
							this.Render(context, cmd, ref renderingData, ref filterSettings, drawSettings);
							this.CopyCameraSortingLayerRenderTexture(context, renderingData, copyStoreAction);
							filterSettings.sortingLayerRange = new SortingLayerRange(cameraSortingLayerBoundsIndex + 1, layerBatch2.layerRange.upperBound);
							this.Render(context, cmd, ref renderingData, ref filterSettings, drawSettings);
						}
						else
						{
							filterSettings.sortingLayerRange = new SortingLayerRange(layerBatch2.layerRange.lowerBound, layerBatch2.layerRange.upperBound);
							this.Render(context, cmd, ref renderingData, ref filterSettings, drawSettings);
							if (cameraSortingLayerBoundsIndex == layerBatch2.layerRange.upperBound && this.m_Renderer2DData.useCameraSortingLayerTexture)
							{
								this.CopyCameraSortingLayerRenderTexture(context, renderingData, copyStoreAction);
							}
						}
						if (drawLights && layerBatch2.lightStats.totalVolumetricUsage > 0)
						{
							string sampleName = "Render 2D Light Volumes";
							cmd.BeginSample(sampleName);
							RenderBufferStoreAction storeAction;
							if (msaaEnabled)
							{
								storeAction = ((resolveDuringBatch == j && !resolveIsAfterCopy) ? RenderBufferStoreAction.Resolve : RenderBufferStoreAction.StoreAndResolve);
							}
							else
							{
								storeAction = RenderBufferStoreAction.Store;
							}
							this.RenderLightVolumes(renderingData, cmd, ref layerBatch2, base.colorAttachmentHandle.nameID, base.depthAttachmentHandle.nameID, RenderBufferStoreAction.Store, storeAction, false, this.m_Renderer2DData.lightCullResult.visibleLights);
							cmd.EndSample(sampleName);
						}
					}
				}
			}
			for (int k = startIndex; k < startIndex + batchesDrawn; k++)
			{
				layerBatches[k].ReleaseRT(cmd);
			}
			return batchesDrawn;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000DADC File Offset: 0x0000BCDC
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			bool isLitView = true;
			Camera camera = *renderingData.cameraData.camera;
			FilteringSettings filterSettings = FilteringSettings.defaultValue;
			filterSettings.renderQueueRange = RenderQueueRange.all;
			filterSettings.layerMask = -1;
			filterSettings.renderingLayerMask = uint.MaxValue;
			filterSettings.sortingLayerRange = SortingLayerRange.all;
			LayerUtility.InitializeBudget(this.m_Renderer2DData.lightRenderTextureMemoryBudget);
			ShadowRendering.InitializeBudget(this.m_Renderer2DData.shadowRenderTextureMemoryBudget);
			RendererLighting.lightBatch.Reset();
			PixelPerfectCamera pixelPerfectCamera;
			camera.TryGetComponent<PixelPerfectCamera>(out pixelPerfectCamera);
			if (pixelPerfectCamera != null && pixelPerfectCamera.enabled && pixelPerfectCamera.offscreenRTSize != Vector2Int.zero)
			{
				int cameraWidth = pixelPerfectCamera.offscreenRTSize.x;
				int cameraHeight = pixelPerfectCamera.offscreenRTSize.y;
				renderingData.commandBuffer->SetGlobalVector(ShaderPropertyId.screenParams, new Vector4((float)cameraWidth, (float)cameraHeight, 1f + 1f / (float)cameraWidth, 1f + 1f / (float)cameraHeight));
			}
			if (this.m_Renderer2DData.lightCullResult.IsSceneLit() && isLitView)
			{
				DrawingSettings combinedDrawSettings = base.CreateDrawingSettings(Render2DLightingPass.k_ShaderTags, ref renderingData, SortingCriteria.CommonTransparent);
				DrawingSettings normalsDrawSettings = base.CreateDrawingSettings(Render2DLightingPass.k_NormalsRenderingPassName, ref renderingData, SortingCriteria.CommonTransparent);
				SortingSettings sortSettings = combinedDrawSettings.sortingSettings;
				RendererLighting.GetTransparencySortingMode(this.m_Renderer2DData, camera, ref sortSettings);
				combinedDrawSettings.sortingSettings = sortSettings;
				normalsDrawSettings.sortingSettings = sortSettings;
				CommandBuffer cmd = *renderingData.commandBuffer;
				cmd.SetGlobalFloat(Render2DLightingPass.k_HDREmulationScaleID, this.m_Renderer2DData.hdrEmulationScale);
				cmd.SetGlobalFloat(Render2DLightingPass.k_InverseHDREmulationScaleID, 1f / this.m_Renderer2DData.hdrEmulationScale);
				cmd.SetGlobalColor(Render2DLightingPass.k_RendererColorID, Color.white);
				cmd.SetGlobalTexture(Render2DLightingPass.k_FalloffLookupID, this.m_FallOffLookup);
				cmd.SetGlobalTexture(Render2DLightingPass.k_LightLookupID, Light2DLookupTexture.GetLightLookupTexture());
				RendererLighting.SetLightShaderGlobals(this.m_Renderer2DData, CommandBufferHelpers.GetRasterCommandBuffer(cmd));
				RenderTextureDescriptor desc = this.GetBlendStyleRenderTextureDesc(renderingData);
				ShadowRendering.CallOnBeforeRender(*renderingData.cameraData.camera, this.m_Renderer2DData.lightCullResult);
				int batchCount;
				LayerBatch[] layerBatches = LayerUtility.CalculateBatches(this.m_Renderer2DData.lightCullResult, out batchCount);
				int batchesDrawn;
				for (int i = 0; i < batchCount; i += batchesDrawn)
				{
					batchesDrawn = this.DrawLayerBatches(layerBatches, batchCount, i, cmd, context, ref renderingData, ref filterSettings, ref normalsDrawSettings, ref combinedDrawSettings, ref desc);
				}
				RendererLighting.DisableAllKeywords(CommandBufferHelpers.GetRasterCommandBuffer(cmd));
				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();
			}
			else
			{
				DrawingSettings unlitDrawSettings = base.CreateDrawingSettings(Render2DLightingPass.k_ShaderTags, ref renderingData, SortingCriteria.CommonTransparent);
				RenderBufferStoreAction storeAction = ((renderingData.cameraData.cameraTargetDescriptor.msaaSamples > 1) ? RenderBufferStoreAction.Resolve : RenderBufferStoreAction.Store);
				SortingSettings sortSettings2 = unlitDrawSettings.sortingSettings;
				RendererLighting.GetTransparencySortingMode(this.m_Renderer2DData, camera, ref sortSettings2);
				unlitDrawSettings.sortingSettings = sortSettings2;
				CommandBuffer cmd2 = *renderingData.commandBuffer;
				using (new ProfilingScope(cmd2, Render2DLightingPass.m_ProfilingSamplerUnlit))
				{
					CoreUtils.SetRenderTarget(cmd2, base.colorAttachmentHandle, RenderBufferLoadAction.Load, storeAction, base.depthAttachmentHandle, RenderBufferLoadAction.Load, storeAction, ClearFlag.None, Color.clear, 0, CubemapFace.Unknown, -1);
					cmd2.SetGlobalColor(Render2DLightingPass.k_RendererColorID, Color.white);
					for (int blendStyleIndex = 0; blendStyleIndex < Render2DLightingPass.k_ShapeLightTextureIDs.Length; blendStyleIndex++)
					{
						if (blendStyleIndex == 0)
						{
							cmd2.SetGlobalTexture(Render2DLightingPass.k_ShapeLightTextureIDs[blendStyleIndex], Texture2D.blackTexture);
						}
						RendererLighting.EnableBlendStyle(CommandBufferHelpers.GetRasterCommandBuffer(cmd2), blendStyleIndex, blendStyleIndex == 0);
					}
				}
				RendererLighting.DisableAllKeywords(CommandBufferHelpers.GetRasterCommandBuffer(cmd2));
				context.ExecuteCommandBuffer(cmd2);
				cmd2.Clear();
				if (this.m_Renderer2DData.useCameraSortingLayerTexture)
				{
					filterSettings.sortingLayerRange = new SortingLayerRange(short.MinValue, this.m_CameraSortingLayerBoundsIndex);
					this.Render(context, cmd2, ref renderingData, ref filterSettings, unlitDrawSettings);
					this.CopyCameraSortingLayerRenderTexture(context, renderingData, storeAction);
					filterSettings.sortingLayerRange = new SortingLayerRange(this.m_CameraSortingLayerBoundsIndex + 1, short.MaxValue);
					this.Render(context, cmd2, ref renderingData, ref filterSettings, unlitDrawSettings);
				}
				else
				{
					this.Render(context, cmd2, ref renderingData, ref filterSettings, unlitDrawSettings);
				}
			}
			filterSettings.sortingLayerRange = SortingLayerRange.all;
			RendererList nullRendererList = RendererList.nullRendererList;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000DEF8 File Offset: 0x0000C0F8
		Renderer2DData IRenderPass2D.rendererData
		{
			get
			{
				return this.m_Renderer2DData;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000DF00 File Offset: 0x0000C100
		public void Dispose()
		{
			RTHandle normalsRenderTarget = this.m_Renderer2DData.normalsRenderTarget;
			if (normalsRenderTarget != null)
			{
				normalsRenderTarget.Release();
			}
			this.m_Renderer2DData.normalsRenderTarget = null;
			RTHandle cameraSortingLayerRenderTarget = this.m_Renderer2DData.cameraSortingLayerRenderTarget;
			if (cameraSortingLayerRenderTarget != null)
			{
				cameraSortingLayerRenderTarget.Release();
			}
			this.m_Renderer2DData.cameraSortingLayerRenderTarget = null;
		}

		// Token: 0x04000128 RID: 296
		private static readonly int k_HDREmulationScaleID = Shader.PropertyToID("_HDREmulationScale");

		// Token: 0x04000129 RID: 297
		private static readonly int k_InverseHDREmulationScaleID = Shader.PropertyToID("_InverseHDREmulationScale");

		// Token: 0x0400012A RID: 298
		private static readonly int k_RendererColorID = Shader.PropertyToID("_RendererColor");

		// Token: 0x0400012B RID: 299
		private static readonly int k_LightLookupID = Shader.PropertyToID("_LightLookup");

		// Token: 0x0400012C RID: 300
		private static readonly int k_FalloffLookupID = Shader.PropertyToID("_FalloffLookup");

		// Token: 0x0400012D RID: 301
		private static readonly int[] k_ShapeLightTextureIDs = new int[]
		{
			Shader.PropertyToID("_ShapeLightTexture0"),
			Shader.PropertyToID("_ShapeLightTexture1"),
			Shader.PropertyToID("_ShapeLightTexture2"),
			Shader.PropertyToID("_ShapeLightTexture3")
		};

		// Token: 0x0400012E RID: 302
		private static readonly ShaderTagId k_CombinedRenderingPassName = new ShaderTagId("Universal2D");

		// Token: 0x0400012F RID: 303
		private static readonly ShaderTagId k_NormalsRenderingPassName = new ShaderTagId("NormalsRendering");

		// Token: 0x04000130 RID: 304
		private static readonly ShaderTagId k_LegacyPassName = new ShaderTagId("SRPDefaultUnlit");

		// Token: 0x04000131 RID: 305
		private static readonly List<ShaderTagId> k_ShaderTags = new List<ShaderTagId>
		{
			Render2DLightingPass.k_LegacyPassName,
			Render2DLightingPass.k_CombinedRenderingPassName
		};

		// Token: 0x04000132 RID: 306
		private static readonly ProfilingSampler m_ProfilingDrawLights = new ProfilingSampler("Draw 2D Lights");

		// Token: 0x04000133 RID: 307
		private static readonly ProfilingSampler m_ProfilingDrawLightTextures = new ProfilingSampler("Draw 2D Lights Textures");

		// Token: 0x04000134 RID: 308
		private static readonly ProfilingSampler m_ProfilingDrawRenderers = new ProfilingSampler("Draw All Renderers");

		// Token: 0x04000135 RID: 309
		private static readonly ProfilingSampler m_ProfilingDrawLayerBatch = new ProfilingSampler("Draw Layer Batch");

		// Token: 0x04000136 RID: 310
		private static readonly ProfilingSampler m_ProfilingSamplerUnlit = new ProfilingSampler("Render Unlit");

		// Token: 0x04000137 RID: 311
		private Material m_BlitMaterial;

		// Token: 0x04000138 RID: 312
		private Material m_SamplingMaterial;

		// Token: 0x04000139 RID: 313
		private readonly Renderer2DData m_Renderer2DData;

		// Token: 0x0400013A RID: 314
		private readonly Texture2D m_FallOffLookup;

		// Token: 0x0400013B RID: 315
		private bool m_NeedsDepth;

		// Token: 0x0400013C RID: 316
		private short m_CameraSortingLayerBoundsIndex;
	}
}
