using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000199 RID: 409
	public static class TemporalAA
	{
		// Token: 0x060008B3 RID: 2227 RVA: 0x000298D4 File Offset: 0x00027AD4
		internal static int CalculateTaaFrameIndex(ref TemporalAA.Settings settings)
		{
			int taaFrameCountOffset = settings.jitterFrameCountOffset;
			return Time.frameCount + taaFrameCountOffset;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000298F0 File Offset: 0x00027AF0
		internal static Matrix4x4 CalculateJitterMatrix(UniversalCameraData cameraData, TemporalAA.JitterFunc jitterFunc)
		{
			Matrix4x4 jitterMat = Matrix4x4.identity;
			if (cameraData.IsTemporalAAEnabled())
			{
				int taaFrameIndex = TemporalAA.CalculateTaaFrameIndex(ref cameraData.taaSettings);
				float actualWidth = (float)cameraData.cameraTargetDescriptor.width;
				float actualHeight = (float)cameraData.cameraTargetDescriptor.height;
				float jitterScale = cameraData.taaSettings.jitterScale;
				Vector2 jitter;
				bool allowScaling;
				jitterFunc(taaFrameIndex, out jitter, out allowScaling);
				if (allowScaling)
				{
					jitter *= jitterScale;
				}
				float num = jitter.x * (2f / actualWidth);
				float offsetY = jitter.y * (2f / actualHeight);
				jitterMat = Matrix4x4.Translate(new Vector3(num, offsetY, 0f));
			}
			return jitterMat;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0002998C File Offset: 0x00027B8C
		internal static void CalculateJitter(int frameIndex, out Vector2 jitter, out bool allowScaling)
		{
			float jitterX = HaltonSequence.Get((frameIndex & 1023) + 1, 2) - 0.5f;
			float jitterY = HaltonSequence.Get((frameIndex & 1023) + 1, 3) - 0.5f;
			jitter = new Vector2(jitterX, jitterY);
			allowScaling = true;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000299D8 File Offset: 0x00027BD8
		internal static float[] CalculateFilterWeights(ref TemporalAA.Settings settings)
		{
			int taaFrameIndex = TemporalAA.CalculateTaaFrameIndex(ref settings);
			float totalWeight = 0f;
			for (int i = 0; i < 9; i++)
			{
				Vector2 jitter;
				bool flag;
				TemporalAA.CalculateJitter(taaFrameIndex, out jitter, out flag);
				jitter *= settings.jitterScale;
				float num = TemporalAA.taaFilterOffsets[i].x - jitter.x;
				float y = TemporalAA.taaFilterOffsets[i].y - jitter.y;
				float d2 = num * num + y * y;
				TemporalAA.taaFilterWeights[i] = Mathf.Exp(-2.2727273f * d2);
				totalWeight += TemporalAA.taaFilterWeights[i];
			}
			for (int j = 0; j < 9; j++)
			{
				TemporalAA.taaFilterWeights[j] /= totalWeight;
			}
			return TemporalAA.taaFilterWeights;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00029A98 File Offset: 0x00027C98
		internal static RenderTextureDescriptor TemporalAADescFromCameraDesc(ref RenderTextureDescriptor cameraDesc)
		{
			RenderTextureDescriptor taaDesc = cameraDesc;
			taaDesc.width = cameraDesc.width;
			taaDesc.height = cameraDesc.height;
			taaDesc.msaaSamples = 1;
			taaDesc.volumeDepth = cameraDesc.volumeDepth;
			taaDesc.mipCount = 0;
			taaDesc.graphicsFormat = cameraDesc.graphicsFormat;
			taaDesc.sRGB = false;
			taaDesc.depthStencilFormat = GraphicsFormat.None;
			taaDesc.dimension = cameraDesc.dimension;
			taaDesc.vrUsage = cameraDesc.vrUsage;
			taaDesc.memoryless = RenderTextureMemoryless.None;
			taaDesc.useMipMap = false;
			taaDesc.autoGenerateMips = false;
			taaDesc.enableRandomWrite = false;
			taaDesc.bindMS = false;
			taaDesc.useDynamicScale = false;
			if (!SystemInfo.IsFormatSupported(taaDesc.graphicsFormat, GraphicsFormatUsage.Render))
			{
				taaDesc.graphicsFormat = GraphicsFormat.None;
				for (int i = 0; i < TemporalAA.AccumulationFormatList.Length; i++)
				{
					if (SystemInfo.IsFormatSupported(TemporalAA.AccumulationFormatList[i], GraphicsFormatUsage.Render))
					{
						taaDesc.graphicsFormat = TemporalAA.AccumulationFormatList[i];
						break;
					}
				}
			}
			return taaDesc;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00029B98 File Offset: 0x00027D98
		internal static string ValidateAndWarn(UniversalCameraData cameraData)
		{
			string warning = null;
			if (warning == null && !cameraData.postProcessEnabled)
			{
				warning = "Disabling TAA because camera has post-processing disabled.";
			}
			if (cameraData.taaHistory == null)
			{
				warning = "Disabling TAA due to invalid persistent data.";
			}
			if (warning == null && cameraData.cameraTargetDescriptor.msaaSamples != 1)
			{
				if (cameraData.xr != null && cameraData.xr.enabled)
				{
					warning = "Disabling TAA because MSAA is on. MSAA must be disabled globally for all cameras in XR mode.";
				}
				else
				{
					warning = "Disabling TAA because MSAA is on. Turn MSAA off on the camera or current URP Asset to enable TAA.";
				}
			}
			UniversalAdditionalCameraData additionalCameraData;
			if (warning == null && cameraData.camera.TryGetComponent<UniversalAdditionalCameraData>(out additionalCameraData) && (additionalCameraData.renderType == CameraRenderType.Overlay || additionalCameraData.cameraStack.Count > 0))
			{
				warning = "Disabling TAA because camera is stacked.";
			}
			if (warning == null && cameraData.camera.allowDynamicResolution)
			{
				warning = "Disabling TAA because camera has dynamic resolution enabled. You can use a constant render scale instead.";
			}
			if (warning == null && !cameraData.renderer.SupportsMotionVectors())
			{
				warning = "Disabling TAA because the renderer does not implement motion vectors. Motion vectors are required for TAA.";
			}
			if (TemporalAA.s_warnCounter % 60U == 0U)
			{
				Debug.LogWarning(warning);
			}
			TemporalAA.s_warnCounter += 1U;
			return warning;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00029C74 File Offset: 0x00027E74
		internal unsafe static void ExecutePass(CommandBuffer cmd, Material taaMaterial, ref CameraData cameraData, RTHandle source, RTHandle destination, RenderTexture motionVectors)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.TemporalAA)))
			{
				int multipassId = cameraData.xr.multipassId;
				bool isNewFrame = cameraData.taaHistory->GetAccumulationVersion(multipassId) != Time.frameCount;
				RTHandle taaHistoryAccumulationTex = cameraData.taaHistory->GetAccumulationTexture(multipassId);
				taaMaterial.SetTexture(TemporalAA.ShaderConstants._TaaAccumulationTex, taaHistoryAccumulationTex);
				taaMaterial.SetTexture(TemporalAA.ShaderConstants._TaaMotionVectorTex, isNewFrame ? motionVectors : Texture2D.blackTexture);
				ref TemporalAA.Settings taa = ref cameraData.taaSettings;
				float taaInfluence = ((taa.resetHistoryFrames == 0) ? taa.m_FrameInfluence : 1f);
				taaMaterial.SetFloat(TemporalAA.ShaderConstants._TaaFrameInfluence, taaInfluence);
				taaMaterial.SetFloat(TemporalAA.ShaderConstants._TaaVarianceClampScale, taa.varianceClampScale);
				if (taa.quality == TemporalAAQuality.VeryHigh)
				{
					taaMaterial.SetFloatArray(TemporalAA.ShaderConstants._TaaFilterWeights, TemporalAA.CalculateFilterWeights(ref taa));
				}
				GraphicsFormat graphicsFormat = taaHistoryAccumulationTex.rt.graphicsFormat;
				if (graphicsFormat == GraphicsFormat.R8G8B8A8_UNorm || graphicsFormat == GraphicsFormat.B8G8R8A8_UNorm || graphicsFormat == GraphicsFormat.B10G11R11_UFloatPack32)
				{
					taaMaterial.EnableKeyword(TemporalAA.ShaderKeywords.TAA_LOW_PRECISION_SOURCE);
				}
				else
				{
					taaMaterial.DisableKeyword(TemporalAA.ShaderKeywords.TAA_LOW_PRECISION_SOURCE);
				}
				CoreUtils.SetKeyword(taaMaterial, "_ENABLE_ALPHA_OUTPUT", *cameraData.isAlphaOutputEnabled);
				Blitter.BlitCameraTexture(cmd, source, destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, taaMaterial, (int)taa.quality);
				if (isNewFrame)
				{
					int kHistoryCopyPass = taaMaterial.shader.passCount - 1;
					Blitter.BlitCameraTexture(cmd, destination, taaHistoryAccumulationTex, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, taaMaterial, kHistoryCopyPass);
					cameraData.taaHistory->SetAccumulationVersion(multipassId, Time.frameCount);
				}
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00029E04 File Offset: 0x00028004
		internal static void Render(RenderGraph renderGraph, Material taaMaterial, UniversalCameraData cameraData, ref TextureHandle srcColor, ref TextureHandle srcDepth, ref TextureHandle srcMotionVectors, ref TextureHandle dstColor)
		{
			int multipassId = 0;
			multipassId = cameraData.xr.multipassId;
			ref TemporalAA.Settings taa = ref cameraData.taaSettings;
			bool isNewFrame = cameraData.taaHistory.GetAccumulationVersion(multipassId) != Time.frameCount;
			float taaInfluence = ((taa.resetHistoryFrames == 0) ? taa.m_FrameInfluence : 1f);
			RTHandle accumulationTexture = cameraData.taaHistory.GetAccumulationTexture(multipassId);
			TextureHandle srcAccumulation = renderGraph.ImportTexture(accumulationTexture);
			TextureHandle activeMotionVectors = (isNewFrame ? srcMotionVectors : renderGraph.defaultResources.blackTexture);
			TemporalAA.TaaPassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<TemporalAA.TaaPassData>("Temporal Anti-aliasing", out passData, ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_TAA), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/TemporalAA.cs", 484))
			{
				passData.dstTex = dstColor;
				builder.SetRenderAttachment(dstColor, 0, AccessFlags.Write);
				passData.srcColorTex = srcColor;
				builder.UseTexture(in srcColor, AccessFlags.Read);
				passData.srcDepthTex = srcDepth;
				builder.UseTexture(in srcDepth, AccessFlags.Read);
				passData.srcMotionVectorTex = activeMotionVectors;
				builder.UseTexture(in activeMotionVectors, AccessFlags.Read);
				passData.srcTaaAccumTex = srcAccumulation;
				builder.UseTexture(in srcAccumulation, AccessFlags.Read);
				passData.material = taaMaterial;
				passData.passIndex = (int)taa.quality;
				passData.taaFrameInfluence = taaInfluence;
				passData.taaVarianceClampScale = taa.varianceClampScale;
				if (taa.quality == TemporalAAQuality.VeryHigh)
				{
					passData.taaFilterWeights = TemporalAA.CalculateFilterWeights(ref taa);
				}
				else
				{
					passData.taaFilterWeights = null;
				}
				GraphicsFormat graphicsFormat = accumulationTexture.rt.graphicsFormat;
				if (graphicsFormat == GraphicsFormat.R8G8B8A8_UNorm || graphicsFormat == GraphicsFormat.B8G8R8A8_UNorm || graphicsFormat == GraphicsFormat.B10G11R11_UFloatPack32)
				{
					passData.taaLowPrecisionSource = true;
				}
				else
				{
					passData.taaLowPrecisionSource = false;
				}
				passData.taaAlphaOutput = cameraData.isAlphaOutputEnabled;
				builder.SetRenderFunc<TemporalAA.TaaPassData>(delegate(TemporalAA.TaaPassData data, RasterGraphContext context)
				{
					data.material.SetFloat(TemporalAA.ShaderConstants._TaaFrameInfluence, data.taaFrameInfluence);
					data.material.SetFloat(TemporalAA.ShaderConstants._TaaVarianceClampScale, data.taaVarianceClampScale);
					data.material.SetTexture(TemporalAA.ShaderConstants._TaaAccumulationTex, data.srcTaaAccumTex);
					data.material.SetTexture(TemporalAA.ShaderConstants._TaaMotionVectorTex, data.srcMotionVectorTex);
					data.material.SetTexture(TemporalAA.ShaderConstants._CameraDepthTexture, data.srcDepthTex);
					CoreUtils.SetKeyword(data.material, TemporalAA.ShaderKeywords.TAA_LOW_PRECISION_SOURCE, data.taaLowPrecisionSource);
					CoreUtils.SetKeyword(data.material, "_ENABLE_ALPHA_OUTPUT", data.taaAlphaOutput);
					if (data.taaFilterWeights != null)
					{
						data.material.SetFloatArray(TemporalAA.ShaderConstants._TaaFilterWeights, data.taaFilterWeights);
					}
					Blitter.BlitTexture(context.cmd, data.srcColorTex, Vector2.one, data.material, data.passIndex);
				});
			}
			if (isNewFrame)
			{
				int kHistoryCopyPass = taaMaterial.shader.passCount - 1;
				TemporalAA.TaaPassData passData2;
				using (IRasterRenderGraphBuilder builder2 = renderGraph.AddRasterRenderPass<TemporalAA.TaaPassData>("Temporal Anti-aliasing Copy History", out passData2, ProfilingSampler.Get<URPProfileId>(URPProfileId.RG_TAACopyHistory), "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/TemporalAA.cs", 543))
				{
					passData2.dstTex = srcAccumulation;
					builder2.SetRenderAttachment(srcAccumulation, 0, AccessFlags.Write);
					passData2.srcColorTex = dstColor;
					builder2.UseTexture(in dstColor, AccessFlags.Read);
					passData2.material = taaMaterial;
					passData2.passIndex = kHistoryCopyPass;
					builder2.SetRenderFunc<TemporalAA.TaaPassData>(delegate(TemporalAA.TaaPassData data, RasterGraphContext context)
					{
						Blitter.BlitTexture(context.cmd, data.srcColorTex, Vector2.one, data.material, data.passIndex);
					});
				}
				cameraData.taaHistory.SetAccumulationVersion(multipassId, Time.frameCount);
			}
		}

		// Token: 0x040008FE RID: 2302
		internal static TemporalAA.JitterFunc s_JitterFunc = new TemporalAA.JitterFunc(TemporalAA.CalculateJitter);

		// Token: 0x040008FF RID: 2303
		private static readonly Vector2[] taaFilterOffsets = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 0f),
			new Vector2(-1f, 0f),
			new Vector2(0f, -1f),
			new Vector2(-1f, 1f),
			new Vector2(1f, -1f),
			new Vector2(1f, 1f),
			new Vector2(-1f, -1f)
		};

		// Token: 0x04000900 RID: 2304
		private static readonly float[] taaFilterWeights = new float[TemporalAA.taaFilterOffsets.Length + 1];

		// Token: 0x04000901 RID: 2305
		internal static GraphicsFormat[] AccumulationFormatList = new GraphicsFormat[]
		{
			GraphicsFormat.R16G16B16A16_SFloat,
			GraphicsFormat.B10G11R11_UFloatPack32,
			GraphicsFormat.R8G8B8A8_UNorm,
			GraphicsFormat.B8G8R8A8_UNorm
		};

		// Token: 0x04000902 RID: 2306
		private static uint s_warnCounter = 0U;

		// Token: 0x0200019A RID: 410
		internal static class ShaderConstants
		{
			// Token: 0x04000903 RID: 2307
			public static readonly int _TaaAccumulationTex = Shader.PropertyToID("_TaaAccumulationTex");

			// Token: 0x04000904 RID: 2308
			public static readonly int _TaaMotionVectorTex = Shader.PropertyToID("_TaaMotionVectorTex");

			// Token: 0x04000905 RID: 2309
			public static readonly int _TaaFilterWeights = Shader.PropertyToID("_TaaFilterWeights");

			// Token: 0x04000906 RID: 2310
			public static readonly int _TaaFrameInfluence = Shader.PropertyToID("_TaaFrameInfluence");

			// Token: 0x04000907 RID: 2311
			public static readonly int _TaaVarianceClampScale = Shader.PropertyToID("_TaaVarianceClampScale");

			// Token: 0x04000908 RID: 2312
			public static readonly int _CameraDepthTexture = Shader.PropertyToID("_CameraDepthTexture");
		}

		// Token: 0x0200019B RID: 411
		internal static class ShaderKeywords
		{
			// Token: 0x04000909 RID: 2313
			public static readonly string TAA_LOW_PRECISION_SOURCE = "TAA_LOW_PRECISION_SOURCE";
		}

		// Token: 0x0200019C RID: 412
		[Serializable]
		public struct Settings
		{
			// Token: 0x170001C9 RID: 457
			// (get) Token: 0x060008BE RID: 2238 RVA: 0x0002A253 File Offset: 0x00028453
			// (set) Token: 0x060008BF RID: 2239 RVA: 0x0002A25B File Offset: 0x0002845B
			public TemporalAAQuality quality
			{
				get
				{
					return this.m_Quality;
				}
				set
				{
					this.m_Quality = (TemporalAAQuality)Mathf.Clamp((int)value, 0, 4);
				}
			}

			// Token: 0x170001CA RID: 458
			// (get) Token: 0x060008C0 RID: 2240 RVA: 0x0002A26B File Offset: 0x0002846B
			// (set) Token: 0x060008C1 RID: 2241 RVA: 0x0002A279 File Offset: 0x00028479
			public float baseBlendFactor
			{
				get
				{
					return 1f - this.m_FrameInfluence;
				}
				set
				{
					this.m_FrameInfluence = Mathf.Clamp01(1f - value);
				}
			}

			// Token: 0x170001CB RID: 459
			// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0002A28D File Offset: 0x0002848D
			// (set) Token: 0x060008C3 RID: 2243 RVA: 0x0002A295 File Offset: 0x00028495
			public float jitterScale
			{
				get
				{
					return this.m_JitterScale;
				}
				set
				{
					this.m_JitterScale = Mathf.Clamp01(value);
				}
			}

			// Token: 0x170001CC RID: 460
			// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0002A2A3 File Offset: 0x000284A3
			// (set) Token: 0x060008C5 RID: 2245 RVA: 0x0002A2AB File Offset: 0x000284AB
			public float mipBias
			{
				get
				{
					return this.m_MipBias;
				}
				set
				{
					this.m_MipBias = Mathf.Clamp(value, -1f, 0f);
				}
			}

			// Token: 0x170001CD RID: 461
			// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0002A2C3 File Offset: 0x000284C3
			// (set) Token: 0x060008C7 RID: 2247 RVA: 0x0002A2CB File Offset: 0x000284CB
			public float varianceClampScale
			{
				get
				{
					return this.m_VarianceClampScale;
				}
				set
				{
					this.m_VarianceClampScale = Mathf.Clamp(value, 0.001f, 10f);
				}
			}

			// Token: 0x170001CE RID: 462
			// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0002A2E3 File Offset: 0x000284E3
			// (set) Token: 0x060008C9 RID: 2249 RVA: 0x0002A2EB File Offset: 0x000284EB
			public float contrastAdaptiveSharpening
			{
				get
				{
					return this.m_ContrastAdaptiveSharpening;
				}
				set
				{
					this.m_ContrastAdaptiveSharpening = Mathf.Clamp01(value);
				}
			}

			// Token: 0x060008CA RID: 2250 RVA: 0x0002A2FC File Offset: 0x000284FC
			public static TemporalAA.Settings Create()
			{
				TemporalAA.Settings s;
				s.m_Quality = TemporalAAQuality.High;
				s.m_FrameInfluence = 0.1f;
				s.m_JitterScale = 1f;
				s.m_MipBias = 0f;
				s.m_VarianceClampScale = 0.9f;
				s.m_ContrastAdaptiveSharpening = 0f;
				s.resetHistoryFrames = 0;
				s.jitterFrameCountOffset = 0;
				return s;
			}

			// Token: 0x0400090A RID: 2314
			[SerializeField]
			[FormerlySerializedAs("quality")]
			internal TemporalAAQuality m_Quality;

			// Token: 0x0400090B RID: 2315
			[SerializeField]
			[FormerlySerializedAs("frameInfluence")]
			internal float m_FrameInfluence;

			// Token: 0x0400090C RID: 2316
			[SerializeField]
			[FormerlySerializedAs("jitterScale")]
			internal float m_JitterScale;

			// Token: 0x0400090D RID: 2317
			[SerializeField]
			[FormerlySerializedAs("mipBias")]
			internal float m_MipBias;

			// Token: 0x0400090E RID: 2318
			[SerializeField]
			[FormerlySerializedAs("varianceClampScale")]
			internal float m_VarianceClampScale;

			// Token: 0x0400090F RID: 2319
			[SerializeField]
			[FormerlySerializedAs("contrastAdaptiveSharpening")]
			internal float m_ContrastAdaptiveSharpening;

			// Token: 0x04000910 RID: 2320
			[NonSerialized]
			internal int resetHistoryFrames;

			// Token: 0x04000911 RID: 2321
			[NonSerialized]
			internal int jitterFrameCountOffset;
		}

		// Token: 0x0200019D RID: 413
		// (Invoke) Token: 0x060008CC RID: 2252
		internal delegate void JitterFunc(int frameIndex, out Vector2 jitter, out bool allowScaling);

		// Token: 0x0200019E RID: 414
		private class TaaPassData
		{
			// Token: 0x04000912 RID: 2322
			internal TextureHandle dstTex;

			// Token: 0x04000913 RID: 2323
			internal TextureHandle srcColorTex;

			// Token: 0x04000914 RID: 2324
			internal TextureHandle srcDepthTex;

			// Token: 0x04000915 RID: 2325
			internal TextureHandle srcMotionVectorTex;

			// Token: 0x04000916 RID: 2326
			internal TextureHandle srcTaaAccumTex;

			// Token: 0x04000917 RID: 2327
			internal Material material;

			// Token: 0x04000918 RID: 2328
			internal int passIndex;

			// Token: 0x04000919 RID: 2329
			internal float taaFrameInfluence;

			// Token: 0x0400091A RID: 2330
			internal float taaVarianceClampScale;

			// Token: 0x0400091B RID: 2331
			internal float[] taaFilterWeights;

			// Token: 0x0400091C RID: 2332
			internal bool taaLowPrecisionSource;

			// Token: 0x0400091D RID: 2333
			internal bool taaAlphaOutput;
		}
	}
}
