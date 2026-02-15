using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x020001F9 RID: 505
	public class ForwardLights
	{
		// Token: 0x06000B66 RID: 2918 RVA: 0x0003D6DA File Offset: 0x0003B8DA
		public ForwardLights()
			: this(ForwardLights.InitParams.Create())
		{
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0003D6E8 File Offset: 0x0003B8E8
		internal ForwardLights(ForwardLights.InitParams initParams)
		{
			this.m_UseStructuredBuffer = RenderingUtils.useStructuredBuffer;
			this.m_UseForwardPlus = initParams.forwardPlus;
			ForwardLights.LightConstantBuffer._MainLightPosition = Shader.PropertyToID("_MainLightPosition");
			ForwardLights.LightConstantBuffer._MainLightColor = Shader.PropertyToID("_MainLightColor");
			ForwardLights.LightConstantBuffer._MainLightOcclusionProbesChannel = Shader.PropertyToID("_MainLightOcclusionProbes");
			ForwardLights.LightConstantBuffer._MainLightLayerMask = Shader.PropertyToID("_MainLightLayerMask");
			ForwardLights.LightConstantBuffer._AdditionalLightsCount = Shader.PropertyToID("_AdditionalLightsCount");
			if (this.m_UseStructuredBuffer)
			{
				this.m_AdditionalLightsBufferId = Shader.PropertyToID("_AdditionalLightsBuffer");
				this.m_AdditionalLightsIndicesId = Shader.PropertyToID("_AdditionalLightsIndices");
			}
			else
			{
				ForwardLights.LightConstantBuffer._AdditionalLightsPosition = Shader.PropertyToID("_AdditionalLightsPosition");
				ForwardLights.LightConstantBuffer._AdditionalLightsColor = Shader.PropertyToID("_AdditionalLightsColor");
				ForwardLights.LightConstantBuffer._AdditionalLightsAttenuation = Shader.PropertyToID("_AdditionalLightsAttenuation");
				ForwardLights.LightConstantBuffer._AdditionalLightsSpotDir = Shader.PropertyToID("_AdditionalLightsSpotDir");
				ForwardLights.LightConstantBuffer._AdditionalLightOcclusionProbeChannel = Shader.PropertyToID("_AdditionalLightsOcclusionProbes");
				ForwardLights.LightConstantBuffer._AdditionalLightsLayerMasks = Shader.PropertyToID("_AdditionalLightsLayerMasks");
				int maxLights = UniversalRenderPipeline.maxVisibleAdditionalLights;
				this.m_AdditionalLightPositions = new Vector4[maxLights];
				this.m_AdditionalLightColors = new Vector4[maxLights];
				this.m_AdditionalLightAttenuations = new Vector4[maxLights];
				this.m_AdditionalLightSpotDirections = new Vector4[maxLights];
				this.m_AdditionalLightOcclusionProbeChannels = new Vector4[maxLights];
				this.m_AdditionalLightsLayerMasks = new float[maxLights];
			}
			if (this.m_UseForwardPlus)
			{
				this.CreateForwardPlusBuffers();
				this.m_ReflectionProbeManager = ReflectionProbeManager.Create();
			}
			this.m_LightCookieManager = initParams.lightCookieManager;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0003D858 File Offset: 0x0003BA58
		private void CreateForwardPlusBuffers()
		{
			this.m_ZBins = new NativeArray<uint>(UniversalRenderPipeline.maxZBinWords, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.m_ZBinsBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Constant, UniversalRenderPipeline.maxZBinWords / 4, UnsafeUtility.SizeOf<float4>());
			this.m_ZBinsBuffer.name = "URP Z-Bin Buffer";
			this.m_TileMasks = new NativeArray<uint>(UniversalRenderPipeline.maxTileWords, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.m_TileMasksBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Constant, UniversalRenderPipeline.maxTileWords / 4, UnsafeUtility.SizeOf<float4>());
			this.m_TileMasksBuffer.name = "URP Tile Buffer";
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0003D8E1 File Offset: 0x0003BAE1
		internal ReflectionProbeManager reflectionProbeManager
		{
			get
			{
				return this.m_ReflectionProbeManager;
			}
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0003D8E9 File Offset: 0x0003BAE9
		private static int AlignByteCount(int count, int align)
		{
			return align * ((count + align - 1) / align);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0003D8F4 File Offset: 0x0003BAF4
		private void GetViewParams(Camera camera, float4x4 viewToClip, out float viewPlaneBot, out float viewPlaneTop, out float4 viewToViewportScaleBias)
		{
			float2 viewPlaneHalfSizeInv = math.float2(viewToClip[0][0], viewToClip[1][1]);
			float2 viewPlaneHalfSize = math.rcp(viewPlaneHalfSizeInv);
			float2 centerClipSpace = (camera.orthographic ? (-math.float2(viewToClip[3][0], viewToClip[3][1])) : math.float2(viewToClip[2][0], viewToClip[2][1]));
			viewPlaneBot = centerClipSpace.y * viewPlaneHalfSize.y - viewPlaneHalfSize.y;
			viewPlaneTop = centerClipSpace.y * viewPlaneHalfSize.y + viewPlaneHalfSize.y;
			viewToViewportScaleBias = math.float4(viewPlaneHalfSizeInv * 0.5f, -centerClipSpace * 0.5f + 0.5f);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0003D9DC File Offset: 0x0003BBDC
		internal void PreSetup(UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData)
		{
			if (this.m_UseForwardPlus)
			{
				using (new ProfilingScope(ForwardLights.m_ProfilingSamplerFPSetup))
				{
					if (!this.m_CullingHandle.IsCompleted)
					{
						throw new InvalidOperationException("Forward+ jobs have not completed yet.");
					}
					if (this.m_TileMasks.Length != UniversalRenderPipeline.maxTileWords)
					{
						this.m_ZBins.Dispose();
						this.m_ZBinsBuffer.Dispose();
						this.m_TileMasks.Dispose();
						this.m_TileMasksBuffer.Dispose();
						this.CreateForwardPlusBuffers();
					}
					else
					{
						UnsafeUtility.MemClear(this.m_ZBins.GetUnsafePtr<uint>(), (long)(this.m_ZBins.Length * 4));
						UnsafeUtility.MemClear(this.m_TileMasks.GetUnsafePtr<uint>(), (long)(this.m_TileMasks.Length * 4));
					}
					Camera camera = cameraData.camera;
					int2 screenResolution = math.int2(cameraData.pixelWidth, cameraData.pixelHeight);
					int viewCount = ((cameraData.xr.enabled && cameraData.xr.singlePassEnabled) ? 2 : 1);
					this.m_LightCount = lightData.visibleLights.Length;
					int lightOffset = 0;
					while (lightOffset < this.m_LightCount && lightData.visibleLights[lightOffset].lightType == LightType.Directional)
					{
						lightOffset++;
					}
					this.m_LightCount -= lightOffset;
					this.m_DirectionalLightCount = lightOffset;
					if (lightData.mainLightIndex != -1 && this.m_DirectionalLightCount != 0)
					{
						this.m_DirectionalLightCount--;
					}
					NativeArray<VisibleLight> visibleLights = lightData.visibleLights.GetSubArray(lightOffset, this.m_LightCount);
					NativeArray<VisibleReflectionProbe> reflectionProbes = renderingData.cullResults.visibleReflectionProbes;
					int reflectionProbeCount = math.min(reflectionProbes.Length, UniversalRenderPipeline.maxVisibleReflectionProbes);
					int itemsPerTile = visibleLights.Length + reflectionProbeCount;
					this.m_WordsPerTile = (itemsPerTile + 31) / 32;
					this.m_ActualTileWidth = 4;
					do
					{
						this.m_ActualTileWidth <<= 1;
						this.m_TileResolution = (screenResolution + this.m_ActualTileWidth - 1) / this.m_ActualTileWidth;
					}
					while (this.m_TileResolution.x * this.m_TileResolution.y * this.m_WordsPerTile * viewCount > UniversalRenderPipeline.maxTileWords);
					if (!camera.orthographic)
					{
						this.m_ZBinScale = (float)(UniversalRenderPipeline.maxZBinWords / viewCount) / ((math.log2(camera.farClipPlane) - math.log2(camera.nearClipPlane)) * (float)(2 + this.m_WordsPerTile));
						this.m_ZBinOffset = -math.log2(camera.nearClipPlane) * this.m_ZBinScale;
						this.m_BinCount = (int)(math.log2(camera.farClipPlane) * this.m_ZBinScale + this.m_ZBinOffset);
					}
					else
					{
						this.m_ZBinScale = (float)(UniversalRenderPipeline.maxZBinWords / viewCount) / ((camera.farClipPlane - camera.nearClipPlane) * (float)(2 + this.m_WordsPerTile));
						this.m_ZBinOffset = -camera.nearClipPlane * this.m_ZBinScale;
						this.m_BinCount = (int)(camera.farClipPlane * this.m_ZBinScale + this.m_ZBinOffset);
					}
					this.m_BinCount = Math.Max(this.m_BinCount, 0);
					Fixed2<float4x4> worldToViews = new Fixed2<float4x4>(cameraData.GetViewMatrix(0), cameraData.GetViewMatrix(math.min(1, viewCount - 1)));
					Fixed2<float4x4> viewToClips = new Fixed2<float4x4>(cameraData.GetProjectionMatrix(0), cameraData.GetProjectionMatrix(math.min(1, viewCount - 1)));
					for (int i = 1; i < reflectionProbeCount; i++)
					{
						VisibleReflectionProbe probe = reflectionProbes[i];
						int j = i - 1;
						while (j >= 0 && ForwardLights.<PreSetup>g__IsProbeGreater|40_0(reflectionProbes[j], probe))
						{
							reflectionProbes[j + 1] = reflectionProbes[j];
							j--;
						}
						reflectionProbes[j + 1] = probe;
					}
					NativeArray<float2> minMaxZs = new NativeArray<float2>(itemsPerTile * viewCount, Allocator.TempJob, NativeArrayOptions.ClearMemory);
					JobHandle lightMinMaxZHandle = new LightMinMaxZJob
					{
						worldToViews = worldToViews,
						lights = visibleLights,
						minMaxZs = minMaxZs.GetSubArray(0, this.m_LightCount * viewCount)
					}.ScheduleParallel(this.m_LightCount * viewCount, 32, default(JobHandle));
					JobHandle reflectionProbeMinMaxZHandle = new ReflectionProbeMinMaxZJob
					{
						worldToViews = worldToViews,
						reflectionProbes = reflectionProbes,
						minMaxZs = minMaxZs.GetSubArray(this.m_LightCount * viewCount, reflectionProbeCount * viewCount)
					}.ScheduleParallel(reflectionProbeCount * viewCount, 32, lightMinMaxZHandle);
					int zBinningBatchCount = (this.m_BinCount + 128 - 1) / 128;
					JobHandle zBinningHandle = new ZBinningJob
					{
						bins = this.m_ZBins,
						minMaxZs = minMaxZs,
						zBinScale = this.m_ZBinScale,
						zBinOffset = this.m_ZBinOffset,
						binCount = this.m_BinCount,
						wordsPerTile = this.m_WordsPerTile,
						lightCount = this.m_LightCount,
						reflectionProbeCount = reflectionProbeCount,
						batchCount = zBinningBatchCount,
						viewCount = viewCount,
						isOrthographic = camera.orthographic
					}.ScheduleParallel(zBinningBatchCount * viewCount, 1, reflectionProbeMinMaxZHandle);
					reflectionProbeMinMaxZHandle.Complete();
					float viewPlaneBottom0;
					float viewPlaneTop0;
					float4 viewToViewportScaleBias0;
					this.GetViewParams(camera, viewToClips[0], out viewPlaneBottom0, out viewPlaneTop0, out viewToViewportScaleBias0);
					float viewPlaneBottom;
					float viewPlaneTop;
					float4 viewToViewportScaleBias;
					this.GetViewParams(camera, viewToClips[1], out viewPlaneBottom, out viewPlaneTop, out viewToViewportScaleBias);
					int rangesPerItem = ForwardLights.AlignByteCount((1 + this.m_TileResolution.y) * UnsafeUtility.SizeOf<InclusiveRange>(), 128) / UnsafeUtility.SizeOf<InclusiveRange>();
					NativeArray<InclusiveRange> tileRanges = new NativeArray<InclusiveRange>(rangesPerItem * itemsPerTile * viewCount, Allocator.TempJob, NativeArrayOptions.ClearMemory);
					JobHandle tileRangeHandle = new TilingJob
					{
						lights = visibleLights,
						reflectionProbes = reflectionProbes,
						tileRanges = tileRanges,
						itemsPerTile = itemsPerTile,
						rangesPerItem = rangesPerItem,
						worldToViews = worldToViews,
						tileScale = screenResolution / (float)this.m_ActualTileWidth,
						tileScaleInv = (float)this.m_ActualTileWidth / screenResolution,
						viewPlaneBottoms = new Fixed2<float>(viewPlaneBottom0, viewPlaneBottom),
						viewPlaneTops = new Fixed2<float>(viewPlaneTop0, viewPlaneTop),
						viewToViewportScaleBiases = new Fixed2<float4>(viewToViewportScaleBias0, viewToViewportScaleBias),
						tileCount = this.m_TileResolution,
						near = camera.nearClipPlane,
						isOrthographic = camera.orthographic
					}.ScheduleParallel(itemsPerTile * viewCount, 1, reflectionProbeMinMaxZHandle);
					JobHandle tilingHandle = new TileRangeExpansionJob
					{
						tileRanges = tileRanges,
						tileMasks = this.m_TileMasks,
						rangesPerItem = rangesPerItem,
						itemsPerTile = itemsPerTile,
						wordsPerTile = this.m_WordsPerTile,
						tileResolution = this.m_TileResolution
					}.ScheduleParallel(this.m_TileResolution.y * viewCount, 1, tileRangeHandle);
					this.m_CullingHandle = JobHandle.CombineDependencies(minMaxZs.Dispose(zBinningHandle), tileRanges.Dispose(tilingHandle));
					JobHandle.ScheduleBatchedJobs();
				}
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0003E0BC File Offset: 0x0003C2BC
		public unsafe void Setup(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			ContextContainer frameData = renderingData.frameData;
			UniversalRenderingData universalRenderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			this.SetupLights(CommandBufferHelpers.GetUnsafeCommandBuffer(*renderingData.commandBuffer), universalRenderingData, cameraData, lightData);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0003E0F8 File Offset: 0x0003C2F8
		internal void SetupRenderGraphLights(RenderGraph renderGraph, UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData)
		{
			ForwardLights.SetupLightPassData passData;
			using (IUnsafeRenderGraphBuilder builder = renderGraph.AddUnsafePass<ForwardLights.SetupLightPassData>(ForwardLights.s_SetupForwardLights.name, out passData, ForwardLights.s_SetupForwardLights, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/ForwardLights.cs", 391))
			{
				passData.renderingData = renderingData;
				passData.cameraData = cameraData;
				passData.lightData = lightData;
				passData.forwardLights = this;
				builder.AllowPassCulling(false);
				builder.SetRenderFunc<ForwardLights.SetupLightPassData>(delegate(ForwardLights.SetupLightPassData data, UnsafeGraphContext rgContext)
				{
					data.forwardLights.SetupLights(rgContext.cmd, data.renderingData, data.cameraData, data.lightData);
				});
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0003E18C File Offset: 0x0003C38C
		internal void SetupLights(UnsafeCommandBuffer cmd, UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData)
		{
			int additionalLightsCount = lightData.additionalLightsCount;
			bool additionalLightsPerVertex = lightData.shadeAdditionalLightsPerVertex;
			using (new ProfilingScope(ForwardLights.m_ProfilingSampler))
			{
				if (this.m_UseForwardPlus)
				{
					this.m_ReflectionProbeManager.UpdateGpuData(CommandBufferHelpers.GetNativeCommandBuffer(cmd), ref renderingData.cullResults);
					using (new ProfilingScope(ForwardLights.m_ProfilingSamplerFPComplete))
					{
						this.m_CullingHandle.Complete();
					}
					using (new ProfilingScope(ForwardLights.m_ProfilingSamplerFPUpload))
					{
						this.m_ZBinsBuffer.SetData<float4>(this.m_ZBins.Reinterpret<float4>(UnsafeUtility.SizeOf<uint>()));
						this.m_TileMasksBuffer.SetData<float4>(this.m_TileMasks.Reinterpret<float4>(UnsafeUtility.SizeOf<uint>()));
						cmd.SetGlobalConstantBuffer(this.m_ZBinsBuffer, "urp_ZBinBuffer", 0, UniversalRenderPipeline.maxZBinWords * 4);
						cmd.SetGlobalConstantBuffer(this.m_TileMasksBuffer, "urp_TileBuffer", 0, UniversalRenderPipeline.maxTileWords * 4);
					}
					cmd.SetGlobalVector("_FPParams0", math.float4(this.m_ZBinScale, this.m_ZBinOffset, (float)this.m_LightCount, (float)this.m_DirectionalLightCount));
					cmd.SetGlobalVector("_FPParams1", math.float4(cameraData.pixelRect.size / (float)this.m_ActualTileWidth, (float)this.m_TileResolution.x, (float)this.m_WordsPerTile));
					cmd.SetGlobalVector("_FPParams2", math.float4((float)this.m_BinCount, (float)(this.m_TileResolution.x * this.m_TileResolution.y), 0f, 0f));
				}
				this.SetupShaderLightConstants(cmd, ref renderingData.cullResults, lightData);
				bool lightCountCheck = (cameraData.renderer.stripAdditionalLightOffVariants && lightData.supportsAdditionalLights) || additionalLightsCount > 0;
				cmd.SetKeyword(in ShaderGlobalKeywords.AdditionalLightsVertex, lightCountCheck && additionalLightsPerVertex && !this.m_UseForwardPlus);
				cmd.SetKeyword(in ShaderGlobalKeywords.AdditionalLightsPixel, lightCountCheck && !additionalLightsPerVertex && !this.m_UseForwardPlus);
				cmd.SetKeyword(in ShaderGlobalKeywords.ForwardPlus, this.m_UseForwardPlus);
				bool isShadowMask = lightData.supportsMixedLighting && this.m_MixedLightingSetup == MixedLightingSetup.ShadowMask;
				bool isShadowMaskAlways = isShadowMask && QualitySettings.shadowmaskMode == ShadowmaskMode.Shadowmask;
				bool isSubtractive = lightData.supportsMixedLighting && this.m_MixedLightingSetup == MixedLightingSetup.Subtractive;
				cmd.SetKeyword(in ShaderGlobalKeywords.LightmapShadowMixing, isSubtractive || isShadowMaskAlways);
				cmd.SetKeyword(in ShaderGlobalKeywords.ShadowsShadowMask, isShadowMask);
				cmd.SetKeyword(in ShaderGlobalKeywords.MixedLightingSubtractive, isSubtractive);
				cmd.SetKeyword(in ShaderGlobalKeywords.ReflectionProbeBlending, lightData.reflectionProbeBlending);
				cmd.SetKeyword(in ShaderGlobalKeywords.ReflectionProbeBoxProjection, lightData.reflectionProbeBoxProjection);
				UniversalRenderPipelineAsset asset = UniversalRenderPipeline.asset;
				bool apvIsEnabled = asset != null && asset.lightProbeSystem == LightProbeSystem.ProbeVolumes;
				ProbeVolumeSHBands probeVolumeSHBands = asset.probeVolumeSHBands;
				cmd.SetKeyword(in ShaderGlobalKeywords.ProbeVolumeL1, apvIsEnabled && probeVolumeSHBands == ProbeVolumeSHBands.SphericalHarmonicsL1);
				cmd.SetKeyword(in ShaderGlobalKeywords.ProbeVolumeL2, apvIsEnabled && probeVolumeSHBands == ProbeVolumeSHBands.SphericalHarmonicsL2);
				ShEvalMode shMode = PlatformAutoDetect.ShAutoDetect(asset.shEvalMode);
				cmd.SetKeyword(in ShaderGlobalKeywords.EVALUATE_SH_MIXED, shMode == ShEvalMode.Mixed);
				cmd.SetKeyword(in ShaderGlobalKeywords.EVALUATE_SH_VERTEX, shMode == ShEvalMode.PerVertex);
				VolumeStack stack = VolumeManager.instance.stack;
				bool enableProbeVolumes = ProbeReferenceVolume.instance.UpdateShaderVariablesProbeVolumes(CommandBufferHelpers.GetNativeCommandBuffer(cmd), stack.GetComponent<ProbeVolumesOptions>(), cameraData.IsTemporalAAEnabled() ? Time.frameCount : 0, lightData.supportsLightLayers);
				cmd.SetGlobalInt("_EnableProbeVolumes", enableProbeVolumes ? 1 : 0);
				cmd.SetKeyword(in ShaderGlobalKeywords.LightLayers, lightData.supportsLightLayers && !CoreUtils.IsSceneLightingDisabled(cameraData.camera));
				if (this.m_LightCookieManager != null)
				{
					this.m_LightCookieManager.Setup(CommandBufferHelpers.GetNativeCommandBuffer(cmd), lightData);
				}
				else
				{
					cmd.SetKeyword(in ShaderGlobalKeywords.LightCookies, false);
				}
			}
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0003E5B8 File Offset: 0x0003C7B8
		internal void Cleanup()
		{
			if (this.m_UseForwardPlus)
			{
				this.m_CullingHandle.Complete();
				this.m_ZBins.Dispose();
				this.m_TileMasks.Dispose();
				this.m_ZBinsBuffer.Dispose();
				this.m_ZBinsBuffer = null;
				this.m_TileMasksBuffer.Dispose();
				this.m_TileMasksBuffer = null;
				this.m_ReflectionProbeManager.Dispose();
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0003E620 File Offset: 0x0003C820
		private void InitializeLightConstants(NativeArray<VisibleLight> lights, int lightIndex, bool supportsLightLayers, out Vector4 lightPos, out Vector4 lightColor, out Vector4 lightAttenuation, out Vector4 lightSpotDir, out Vector4 lightOcclusionProbeChannel, out uint lightLayerMask, out bool isSubtractive)
		{
			UniversalRenderPipeline.InitializeLightConstants_Common(lights, lightIndex, out lightPos, out lightColor, out lightAttenuation, out lightSpotDir, out lightOcclusionProbeChannel);
			lightLayerMask = 0U;
			isSubtractive = false;
			if (lightIndex < 0)
			{
				return;
			}
			ref VisibleLight lightData = ref lights.UnsafeElementAtMutable(lightIndex);
			Light light = lightData.light;
			LightBakingOutput lightBakingOutput = light.bakingOutput;
			isSubtractive = lightBakingOutput.isBaked && lightBakingOutput.lightmapBakeType == LightmapBakeType.Mixed && lightBakingOutput.mixedLightingMode == MixedLightingMode.Subtractive;
			if (light == null)
			{
				return;
			}
			if (lightBakingOutput.lightmapBakeType == LightmapBakeType.Mixed && lightData.light.shadows != LightShadows.None && this.m_MixedLightingSetup == MixedLightingSetup.None)
			{
				MixedLightingMode mixedLightingMode = lightBakingOutput.mixedLightingMode;
				if (mixedLightingMode != MixedLightingMode.Subtractive)
				{
					if (mixedLightingMode == MixedLightingMode.Shadowmask)
					{
						this.m_MixedLightingSetup = MixedLightingSetup.ShadowMask;
					}
				}
				else
				{
					this.m_MixedLightingSetup = MixedLightingSetup.Subtractive;
				}
			}
			if (supportsLightLayers)
			{
				UniversalAdditionalLightData additionalLightData = light.GetUniversalAdditionalLightData();
				lightLayerMask = RenderingLayerUtils.ToValidRenderingLayers(additionalLightData.renderingLayers);
			}
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0003E6E4 File Offset: 0x0003C8E4
		private void SetupShaderLightConstants(UnsafeCommandBuffer cmd, ref CullingResults cullResults, UniversalLightData lightData)
		{
			this.m_MixedLightingSetup = MixedLightingSetup.None;
			this.SetupMainLightConstants(cmd, lightData);
			this.SetupAdditionalLightConstants(cmd, ref cullResults, lightData);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0003E700 File Offset: 0x0003C900
		private void SetupMainLightConstants(UnsafeCommandBuffer cmd, UniversalLightData lightData)
		{
			bool supportsLightLayers = lightData.supportsLightLayers;
			Vector4 lightPos;
			Vector4 lightColor;
			Vector4 lightAttenuation;
			Vector4 lightSpotDir;
			Vector4 lightOcclusionChannel;
			uint lightLayerMask;
			bool isSubtractive;
			this.InitializeLightConstants(lightData.visibleLights, lightData.mainLightIndex, supportsLightLayers, out lightPos, out lightColor, out lightAttenuation, out lightSpotDir, out lightOcclusionChannel, out lightLayerMask, out isSubtractive);
			lightColor.w = (isSubtractive ? 0f : 1f);
			cmd.SetGlobalVector(ForwardLights.LightConstantBuffer._MainLightPosition, lightPos);
			cmd.SetGlobalVector(ForwardLights.LightConstantBuffer._MainLightColor, lightColor);
			cmd.SetGlobalVector(ForwardLights.LightConstantBuffer._MainLightOcclusionProbesChannel, lightOcclusionChannel);
			if (supportsLightLayers)
			{
				cmd.SetGlobalInt(ForwardLights.LightConstantBuffer._MainLightLayerMask, (int)lightLayerMask);
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0003E784 File Offset: 0x0003C984
		private void SetupAdditionalLightConstants(UnsafeCommandBuffer cmd, ref CullingResults cullResults, UniversalLightData lightData)
		{
			bool supportsLightLayers = lightData.supportsLightLayers;
			NativeArray<VisibleLight> lights = lightData.visibleLights;
			int maxAdditionalLightsCount = UniversalRenderPipeline.maxVisibleAdditionalLights;
			int additionalLightsCount = this.SetupPerObjectLightIndices(cullResults, lightData);
			if (additionalLightsCount > 0)
			{
				if (this.m_UseStructuredBuffer)
				{
					NativeArray<ShaderInput.LightData> additionalLightsData = new NativeArray<ShaderInput.LightData>(additionalLightsCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
					int i = 0;
					int lightIter = 0;
					while (i < lights.Length && lightIter < maxAdditionalLightsCount)
					{
						if (lightData.mainLightIndex != i)
						{
							ShaderInput.LightData data;
							bool flag;
							this.InitializeLightConstants(lights, i, supportsLightLayers, out data.position, out data.color, out data.attenuation, out data.spotDirection, out data.occlusionProbeChannels, out data.layerMask, out flag);
							additionalLightsData[lightIter] = data;
							lightIter++;
						}
						i++;
					}
					ComputeBuffer lightDataBuffer = ShaderData.instance.GetLightDataBuffer(additionalLightsCount);
					lightDataBuffer.SetData<ShaderInput.LightData>(additionalLightsData);
					int lightIndices = cullResults.lightAndReflectionProbeIndexCount;
					ComputeBuffer lightIndicesBuffer = ShaderData.instance.GetLightIndicesBuffer(lightIndices);
					cmd.SetGlobalBuffer(this.m_AdditionalLightsBufferId, lightDataBuffer);
					cmd.SetGlobalBuffer(this.m_AdditionalLightsIndicesId, lightIndicesBuffer);
					additionalLightsData.Dispose();
				}
				else
				{
					int j = 0;
					int lightIter2 = 0;
					while (j < lights.Length && lightIter2 < maxAdditionalLightsCount)
					{
						if (lightData.mainLightIndex != j)
						{
							uint lightLayerMask;
							bool isSubtractive;
							this.InitializeLightConstants(lights, j, supportsLightLayers, out this.m_AdditionalLightPositions[lightIter2], out this.m_AdditionalLightColors[lightIter2], out this.m_AdditionalLightAttenuations[lightIter2], out this.m_AdditionalLightSpotDirections[lightIter2], out this.m_AdditionalLightOcclusionProbeChannels[lightIter2], out lightLayerMask, out isSubtractive);
							if (supportsLightLayers)
							{
								this.m_AdditionalLightsLayerMasks[lightIter2] = math.asfloat(lightLayerMask);
							}
							this.m_AdditionalLightColors[lightIter2].w = (isSubtractive ? 1f : 0f);
							lightIter2++;
						}
						j++;
					}
					cmd.SetGlobalVectorArray(ForwardLights.LightConstantBuffer._AdditionalLightsPosition, this.m_AdditionalLightPositions);
					cmd.SetGlobalVectorArray(ForwardLights.LightConstantBuffer._AdditionalLightsColor, this.m_AdditionalLightColors);
					cmd.SetGlobalVectorArray(ForwardLights.LightConstantBuffer._AdditionalLightsAttenuation, this.m_AdditionalLightAttenuations);
					cmd.SetGlobalVectorArray(ForwardLights.LightConstantBuffer._AdditionalLightsSpotDir, this.m_AdditionalLightSpotDirections);
					cmd.SetGlobalVectorArray(ForwardLights.LightConstantBuffer._AdditionalLightOcclusionProbeChannel, this.m_AdditionalLightOcclusionProbeChannels);
					if (supportsLightLayers)
					{
						cmd.SetGlobalFloatArray(ForwardLights.LightConstantBuffer._AdditionalLightsLayerMasks, this.m_AdditionalLightsLayerMasks);
					}
				}
				cmd.SetGlobalVector(ForwardLights.LightConstantBuffer._AdditionalLightsCount, new Vector4((float)lightData.maxPerObjectAdditionalLightsCount, 0f, 0f, 0f));
				return;
			}
			cmd.SetGlobalVector(ForwardLights.LightConstantBuffer._AdditionalLightsCount, Vector4.zero);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0003E9F0 File Offset: 0x0003CBF0
		private int SetupPerObjectLightIndices(CullingResults cullResults, UniversalLightData lightData)
		{
			if (lightData.additionalLightsCount == 0 || this.m_UseForwardPlus)
			{
				return lightData.additionalLightsCount;
			}
			NativeArray<int> perObjectLightIndexMap = cullResults.GetLightIndexMap(Allocator.Temp);
			int globalDirectionalLightsCount = 0;
			int additionalLightsCount = 0;
			int maxVisibleAdditionalLightsCount = UniversalRenderPipeline.maxVisibleAdditionalLights;
			int len = lightData.visibleLights.Length;
			int i = 0;
			while (i < len && additionalLightsCount < maxVisibleAdditionalLightsCount)
			{
				if (i == lightData.mainLightIndex)
				{
					perObjectLightIndexMap[i] = -1;
					globalDirectionalLightsCount++;
				}
				else
				{
					if (lightData.visibleLights[i].lightType == LightType.Directional || lightData.visibleLights[i].lightType == LightType.Spot || lightData.visibleLights[i].lightType == LightType.Point)
					{
						ref NativeArray<int> ptr = ref perObjectLightIndexMap;
						int num = i;
						ptr[num] -= globalDirectionalLightsCount;
					}
					else
					{
						perObjectLightIndexMap[i] = -1;
					}
					additionalLightsCount++;
				}
				i++;
			}
			for (int j = globalDirectionalLightsCount + additionalLightsCount; j < perObjectLightIndexMap.Length; j++)
			{
				perObjectLightIndexMap[j] = -1;
			}
			cullResults.SetLightIndexMap(perObjectLightIndexMap);
			if (this.m_UseStructuredBuffer && additionalLightsCount > 0)
			{
				int lightAndReflectionProbeIndices = cullResults.lightAndReflectionProbeIndexCount;
				cullResults.FillLightAndReflectionProbeIndices(ShaderData.instance.GetLightIndicesBuffer(lightAndReflectionProbeIndices));
			}
			perObjectLightIndexMap.Dispose();
			return additionalLightsCount;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0003EB94 File Offset: 0x0003CD94
		[CompilerGenerated]
		internal static bool <PreSetup>g__IsProbeGreater|40_0(VisibleReflectionProbe probe, VisibleReflectionProbe otherProbe)
		{
			return probe.importance < otherProbe.importance || (probe.importance == otherProbe.importance && probe.bounds.extents.sqrMagnitude > otherProbe.bounds.extents.sqrMagnitude);
		}

		// Token: 0x04000CB8 RID: 3256
		private int m_AdditionalLightsBufferId;

		// Token: 0x04000CB9 RID: 3257
		private int m_AdditionalLightsIndicesId;

		// Token: 0x04000CBA RID: 3258
		private const string k_SetupLightConstants = "Setup Light Constants";

		// Token: 0x04000CBB RID: 3259
		private static readonly ProfilingSampler m_ProfilingSampler = new ProfilingSampler("Setup Light Constants");

		// Token: 0x04000CBC RID: 3260
		private static readonly ProfilingSampler m_ProfilingSamplerFPSetup = new ProfilingSampler("Forward+ Setup");

		// Token: 0x04000CBD RID: 3261
		private static readonly ProfilingSampler m_ProfilingSamplerFPComplete = new ProfilingSampler("Forward+ Complete");

		// Token: 0x04000CBE RID: 3262
		private static readonly ProfilingSampler m_ProfilingSamplerFPUpload = new ProfilingSampler("Forward+ Upload");

		// Token: 0x04000CBF RID: 3263
		private MixedLightingSetup m_MixedLightingSetup;

		// Token: 0x04000CC0 RID: 3264
		private Vector4[] m_AdditionalLightPositions;

		// Token: 0x04000CC1 RID: 3265
		private Vector4[] m_AdditionalLightColors;

		// Token: 0x04000CC2 RID: 3266
		private Vector4[] m_AdditionalLightAttenuations;

		// Token: 0x04000CC3 RID: 3267
		private Vector4[] m_AdditionalLightSpotDirections;

		// Token: 0x04000CC4 RID: 3268
		private Vector4[] m_AdditionalLightOcclusionProbeChannels;

		// Token: 0x04000CC5 RID: 3269
		private float[] m_AdditionalLightsLayerMasks;

		// Token: 0x04000CC6 RID: 3270
		private bool m_UseStructuredBuffer;

		// Token: 0x04000CC7 RID: 3271
		private bool m_UseForwardPlus;

		// Token: 0x04000CC8 RID: 3272
		private int m_DirectionalLightCount;

		// Token: 0x04000CC9 RID: 3273
		private int m_ActualTileWidth;

		// Token: 0x04000CCA RID: 3274
		private int2 m_TileResolution;

		// Token: 0x04000CCB RID: 3275
		private JobHandle m_CullingHandle;

		// Token: 0x04000CCC RID: 3276
		private NativeArray<uint> m_ZBins;

		// Token: 0x04000CCD RID: 3277
		private GraphicsBuffer m_ZBinsBuffer;

		// Token: 0x04000CCE RID: 3278
		private NativeArray<uint> m_TileMasks;

		// Token: 0x04000CCF RID: 3279
		private GraphicsBuffer m_TileMasksBuffer;

		// Token: 0x04000CD0 RID: 3280
		private LightCookieManager m_LightCookieManager;

		// Token: 0x04000CD1 RID: 3281
		private ReflectionProbeManager m_ReflectionProbeManager;

		// Token: 0x04000CD2 RID: 3282
		private int m_WordsPerTile;

		// Token: 0x04000CD3 RID: 3283
		private float m_ZBinScale;

		// Token: 0x04000CD4 RID: 3284
		private float m_ZBinOffset;

		// Token: 0x04000CD5 RID: 3285
		private int m_LightCount;

		// Token: 0x04000CD6 RID: 3286
		private int m_BinCount;

		// Token: 0x04000CD7 RID: 3287
		private static ProfilingSampler s_SetupForwardLights = new ProfilingSampler("Setup Forward Lights");

		// Token: 0x020001FA RID: 506
		private static class LightConstantBuffer
		{
			// Token: 0x04000CD8 RID: 3288
			public static int _MainLightPosition;

			// Token: 0x04000CD9 RID: 3289
			public static int _MainLightColor;

			// Token: 0x04000CDA RID: 3290
			public static int _MainLightOcclusionProbesChannel;

			// Token: 0x04000CDB RID: 3291
			public static int _MainLightLayerMask;

			// Token: 0x04000CDC RID: 3292
			public static int _AdditionalLightsCount;

			// Token: 0x04000CDD RID: 3293
			public static int _AdditionalLightsPosition;

			// Token: 0x04000CDE RID: 3294
			public static int _AdditionalLightsColor;

			// Token: 0x04000CDF RID: 3295
			public static int _AdditionalLightsAttenuation;

			// Token: 0x04000CE0 RID: 3296
			public static int _AdditionalLightsSpotDir;

			// Token: 0x04000CE1 RID: 3297
			public static int _AdditionalLightOcclusionProbeChannel;

			// Token: 0x04000CE2 RID: 3298
			public static int _AdditionalLightsLayerMasks;
		}

		// Token: 0x020001FB RID: 507
		internal struct InitParams
		{
			// Token: 0x06000B78 RID: 2936 RVA: 0x0003EBF8 File Offset: 0x0003CDF8
			internal static ForwardLights.InitParams Create()
			{
				LightCookieManager.Settings settings = LightCookieManager.Settings.Create();
				UniversalRenderPipelineAsset asset = UniversalRenderPipeline.asset;
				if (asset)
				{
					settings.atlas.format = asset.additionalLightsCookieFormat;
					settings.atlas.resolution = asset.additionalLightsCookieResolution;
				}
				ForwardLights.InitParams p;
				p.lightCookieManager = new LightCookieManager(ref settings);
				p.forwardPlus = false;
				return p;
			}

			// Token: 0x04000CE3 RID: 3299
			public LightCookieManager lightCookieManager;

			// Token: 0x04000CE4 RID: 3300
			public bool forwardPlus;
		}

		// Token: 0x020001FC RID: 508
		private class SetupLightPassData
		{
			// Token: 0x04000CE5 RID: 3301
			internal UniversalRenderingData renderingData;

			// Token: 0x04000CE6 RID: 3302
			internal UniversalCameraData cameraData;

			// Token: 0x04000CE7 RID: 3303
			internal UniversalLightData lightData;

			// Token: 0x04000CE8 RID: 3304
			internal ForwardLights forwardLights;
		}
	}
}
