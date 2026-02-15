using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Categorization;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x02000165 RID: 357
	public static class STP
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x00024F2B File Offset: 0x0002312B
		public static bool IsSupported()
		{
			return true & SystemInfo.supportsComputeShaders & (SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES3);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00024F44 File Offset: 0x00023144
		public static Vector2 Jit16(int frameIndex)
		{
			Vector2 result;
			result.x = HaltonSequence.Get(frameIndex, 2) - 0.5f;
			result.y = HaltonSequence.Get(frameIndex, 3) - 0.5f;
			return result;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x00024F7A File Offset: 0x0002317A
		public static GUIContent[] debugViewDescriptions
		{
			get
			{
				return STP.s_DebugViewDescriptions;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00024F81 File Offset: 0x00023181
		public static int[] debugViewIndices
		{
			get
			{
				return STP.s_DebugViewIndices;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x00024F88 File Offset: 0x00023188
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x00024F8F File Offset: 0x0002318F
		public static STP.PerViewConfig[] perViewConfigs
		{
			get
			{
				return STP.s_PerViewConfigs;
			}
			set
			{
				STP.s_PerViewConfigs = value;
			}
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00024F98 File Offset: 0x00023198
		private static Hash128 ComputeHistoryHash(ref STP.HistoryUpdateInfo info)
		{
			Hash128 hash = default(Hash128);
			hash.Append<bool>(ref info.useHwDrs);
			hash.Append<bool>(ref info.useTexArray);
			hash.Append<Vector2Int>(ref info.postUpscaleSize);
			if (!info.useHwDrs)
			{
				hash.Append<Vector2Int>(ref info.preUpscaleSize);
			}
			return hash;
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00024FEA File Offset: 0x000231EA
		private static Vector2Int CalculateConvergenceTextureSize(Vector2Int historyTextureSize)
		{
			return new Vector2Int(CoreUtils.DivRoundUp(historyTextureSize.x, 4), CoreUtils.DivRoundUp(historyTextureSize.y, 4));
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002500C File Offset: 0x0002320C
		private static float CalculateMotionScale(float deltaTime, float lastDeltaTime)
		{
			float motionScale = 1f;
			if (!Mathf.Approximately(lastDeltaTime, 0f))
			{
				motionScale = deltaTime / lastDeltaTime;
			}
			return motionScale;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00025038 File Offset: 0x00023238
		private static Matrix4x4 ExtractRotation(Matrix4x4 input)
		{
			Matrix4x4 output = input;
			output[0, 3] = 0f;
			output[1, 3] = 0f;
			output[2, 3] = 0f;
			output[3, 3] = 1f;
			return output;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00025080 File Offset: 0x00023280
		private static int PackVector2ToInt(Vector2 value)
		{
			int num = (int)Mathf.FloatToHalf(value.x);
			uint yAsHalf = (uint)Mathf.FloatToHalf(value.y);
			return num | (int)((int)yAsHalf << 16);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000250AC File Offset: 0x000232AC
		private unsafe static void PopulateConstantData(ref STP.Config config, ref STP.StpConstantBufferData constants)
		{
			int packedBlueNoiseWidthMinusOne = (config.noiseTexture.width - 1) & 255;
			int packedHasValidHistory = (config.hasValidHistory ? 1 : 0) << 8;
			int num = (config.stencilMask & 255) << 16;
			int packedDebugViewIndex = (config.debugViewIndex & 255) << 24;
			int constant0 = num | packedHasValidHistory | packedBlueNoiseWidthMinusOne | packedDebugViewIndex;
			float zBufferParamZ = (config.farPlane - config.nearPlane) / (config.nearPlane * config.farPlane);
			float zBufferParamW = 1f / config.farPlane;
			constants._StpCommonConstant = new Vector4(BitConverter.Int32BitsToSingle(constant0), zBufferParamZ, zBufferParamW, 0f);
			constants._StpSetupConstants0.x = 1f / (float)config.currentImageSize.x;
			constants._StpSetupConstants0.y = 1f / (float)config.currentImageSize.y;
			constants._StpSetupConstants0.z = 0.5f / (float)config.currentImageSize.x;
			constants._StpSetupConstants0.w = 0.5f / (float)config.currentImageSize.y;
			Vector2 jitP = STP.Jit16(config.frameIndex - 1);
			Vector2 jitC = STP.Jit16(config.frameIndex);
			constants._StpSetupConstants1.x = jitC.x / (float)config.currentImageSize.x - jitP.x / (float)config.priorImageSize.x;
			constants._StpSetupConstants1.y = jitC.y / (float)config.currentImageSize.y - jitP.y / (float)config.priorImageSize.y;
			constants._StpSetupConstants1.z = jitC.x / (float)config.currentImageSize.x;
			constants._StpSetupConstants1.w = jitC.y / (float)config.currentImageSize.y;
			constants._StpSetupConstants2.x = (float)config.outputImageSize.x;
			constants._StpSetupConstants2.y = (float)config.outputImageSize.y;
			float k0 = 1f / config.nearPlane;
			float k = 1f / Mathf.Log(k0 * config.farPlane, 2f);
			constants._StpSetupConstants2.z = k0;
			constants._StpSetupConstants2.w = k;
			Vector2 s;
			s.x = 2f;
			s.y = 2f;
			s.x *= (float)config.priorImageSize.x / ((float)config.priorImageSize.x + 4f);
			s.y *= (float)config.priorImageSize.y / ((float)config.priorImageSize.y + 4f);
			constants._StpSetupConstants3.x = s[0];
			constants._StpSetupConstants3.y = s[1];
			constants._StpSetupConstants3.z = -0.5f * s[0];
			constants._StpSetupConstants3.w = -0.5f * s[1];
			constants._StpSetupConstants4.x = Mathf.Log(config.farPlane / config.nearPlane, 2f);
			constants._StpSetupConstants4.y = config.nearPlane;
			constants._StpSetupConstants4.z = (config.enableMotionScaling ? STP.CalculateMotionScale(config.deltaTime, config.lastDeltaTime) : 1f);
			constants._StpSetupConstants4.w = 0f;
			constants._StpSetupConstants5.x = (float)config.currentImageSize.x;
			constants._StpSetupConstants5.y = (float)config.currentImageSize.y;
			constants._StpSetupConstants5.z = (float)config.outputImageSize.x / (Mathf.Ceil((float)config.outputImageSize.x / 4f) * 4f);
			constants._StpSetupConstants5.w = (float)config.outputImageSize.y / (Mathf.Ceil((float)config.outputImageSize.y / 4f) * 4f);
			uint viewIndex = 0U;
			while ((ulong)viewIndex < (ulong)((long)config.numActiveViews))
			{
				uint baseViewDataOffset = viewIndex * 8U * 4U;
				STP.PerViewConfig perViewConfig = config.perViewConfigs[(int)viewIndex];
				Vector4 prjPriABEF;
				prjPriABEF.x = perViewConfig.lastProj[0, 0];
				prjPriABEF.y = Mathf.Abs(perViewConfig.lastProj[1, 1]);
				prjPriABEF.z = -perViewConfig.lastProj[0, 2];
				prjPriABEF.w = -perViewConfig.lastProj[1, 2];
				Vector4 prjPriCDGH;
				prjPriCDGH.x = perViewConfig.lastProj[2, 2];
				prjPriCDGH.y = perViewConfig.lastProj[2, 3];
				prjPriCDGH.z = perViewConfig.lastProj[3, 2];
				prjPriCDGH.w = perViewConfig.lastProj[3, 3];
				Vector4 prjCurABEF;
				prjCurABEF.x = perViewConfig.currentProj[0, 0];
				prjCurABEF.y = Mathf.Abs(perViewConfig.currentProj[1, 1]);
				prjCurABEF.z = perViewConfig.currentProj[0, 2];
				prjCurABEF.w = perViewConfig.currentProj[1, 2];
				Vector4 prjCurCDGH;
				prjCurCDGH.x = perViewConfig.currentProj[2, 2];
				prjCurCDGH.y = perViewConfig.currentProj[2, 3];
				prjCurCDGH.z = perViewConfig.currentProj[3, 2];
				prjCurCDGH.w = perViewConfig.currentProj[3, 3];
				Matrix4x4 forwardTransform = STP.ExtractRotation(perViewConfig.currentView) * Matrix4x4.Translate(-perViewConfig.currentView.GetColumn(3)) * Matrix4x4.Translate(perViewConfig.lastView.GetColumn(3)) * STP.ExtractRotation(perViewConfig.lastView).transpose;
				Vector4 forIJKL = forwardTransform.GetRow(0);
				Vector4 forMNOP = forwardTransform.GetRow(1);
				Vector4 forQRST = forwardTransform.GetRow(2);
				Vector4 prjPrvABEF;
				prjPrvABEF.x = perViewConfig.lastLastProj[0, 0];
				prjPrvABEF.y = Mathf.Abs(perViewConfig.lastLastProj[1, 1]);
				prjPrvABEF.z = perViewConfig.lastLastProj[0, 2];
				prjPrvABEF.w = perViewConfig.lastLastProj[1, 2];
				Vector4 prjPrvCDGH;
				prjPrvCDGH.x = perViewConfig.lastLastProj[2, 2];
				prjPrvCDGH.y = perViewConfig.lastLastProj[2, 3];
				prjPrvCDGH.z = perViewConfig.lastLastProj[3, 2];
				prjPrvCDGH.w = perViewConfig.lastLastProj[3, 3];
				Matrix4x4 backwardTransform = STP.ExtractRotation(perViewConfig.lastLastView) * Matrix4x4.Translate(-perViewConfig.lastLastView.GetColumn(3)) * Matrix4x4.Translate(perViewConfig.lastView.GetColumn(3)) * STP.ExtractRotation(perViewConfig.lastView).transpose;
				Vector4 bckIJKL = backwardTransform.GetRow(0);
				Vector4 bckMNOP = backwardTransform.GetRow(1);
				Vector4 bckQRST = backwardTransform.GetRow(2);
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)baseViewDataOffset * 4UL)) = prjPriCDGH.z / prjPriABEF.x;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 1U) * 4UL)) = prjPriCDGH.w / prjPriABEF.x;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 2U) * 4UL)) = prjPriABEF.z / prjPriABEF.x;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 3U) * 4UL)) = prjPriCDGH.z / prjPriABEF.y;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 4U) * 4UL)) = prjPriCDGH.w / prjPriABEF.y;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 5U) * 4UL)) = prjPriABEF.w / prjPriABEF.y;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 6U) * 4UL)) = forIJKL.x * prjCurABEF.x + forQRST.x * prjCurABEF.z;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 7U) * 4UL)) = forIJKL.y * prjCurABEF.x + forQRST.y * prjCurABEF.z;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 8U) * 4UL)) = forIJKL.z * prjCurABEF.x + forQRST.z * prjCurABEF.z;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 9U) * 4UL)) = forIJKL.w * prjCurABEF.x + forQRST.w * prjCurABEF.z;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 10U) * 4UL)) = forMNOP.x * prjCurABEF.y + forQRST.x * prjCurABEF.w;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 11U) * 4UL)) = forMNOP.y * prjCurABEF.y + forQRST.y * prjCurABEF.w;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 12U) * 4UL)) = forMNOP.z * prjCurABEF.y + forQRST.z * prjCurABEF.w;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 13U) * 4UL)) = forMNOP.w * prjCurABEF.y + forQRST.w * prjCurABEF.w;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 14U) * 4UL)) = forQRST.x * prjCurCDGH.z;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 15U) * 4UL)) = forQRST.y * prjCurCDGH.z;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 16U) * 4UL)) = forQRST.z * prjCurCDGH.z;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 17U) * 4UL)) = forQRST.w * prjCurCDGH.z + prjCurCDGH.w;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 18U) * 4UL)) = bckIJKL.x * prjPrvABEF.x + bckQRST.x * prjPrvABEF.z;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 19U) * 4UL)) = bckIJKL.y * prjPrvABEF.x + bckQRST.y * prjPrvABEF.z;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 20U) * 4UL)) = bckIJKL.z * prjPrvABEF.x + bckQRST.z * prjPrvABEF.z;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 21U) * 4UL)) = bckIJKL.w * prjPrvABEF.x + bckQRST.w * prjPrvABEF.z;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 22U) * 4UL)) = bckMNOP.x * prjPrvABEF.y + bckQRST.x * prjPrvABEF.w;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 23U) * 4UL)) = bckMNOP.y * prjPrvABEF.y + bckQRST.y * prjPrvABEF.w;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 24U) * 4UL)) = bckMNOP.z * prjPrvABEF.y + bckQRST.z * prjPrvABEF.w;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 25U) * 4UL)) = bckMNOP.w * prjPrvABEF.y + bckQRST.w * prjPrvABEF.w;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 26U) * 4UL)) = bckQRST.x * prjPrvCDGH.z;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 27U) * 4UL)) = bckQRST.y * prjPrvCDGH.z;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 28U) * 4UL)) = bckQRST.z * prjPrvCDGH.z;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 29U) * 4UL)) = bckQRST.w * prjPrvCDGH.z + prjPrvCDGH.w;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 30U) * 4UL)) = 0f;
				*((ref constants._StpSetupPerViewConstants.FixedElementField) + (IntPtr)((ulong)(baseViewDataOffset + 31U) * 4UL)) = 0f;
				viewIndex += 1U;
			}
			constants._StpDilConstants0.x = 4f / (float)config.currentImageSize.x;
			constants._StpDilConstants0.y = 4f / (float)config.currentImageSize.y;
			constants._StpDilConstants0.z = BitConverter.Int32BitsToSingle(config.currentImageSize.x >> 2);
			constants._StpDilConstants0.w = BitConverter.Int32BitsToSingle(config.currentImageSize.y >> 2);
			constants._StpTaaConstants0.x = (float)config.currentImageSize.x / (float)config.outputImageSize.x;
			constants._StpTaaConstants0.y = (float)config.currentImageSize.y / (float)config.outputImageSize.y;
			constants._StpTaaConstants0.z = 0.5f * (float)config.currentImageSize.x / (float)config.outputImageSize.x - jitC.x;
			constants._StpTaaConstants0.w = 0.5f * (float)config.currentImageSize.y / (float)config.outputImageSize.y - jitC.y;
			constants._StpTaaConstants1.x = 1f / (float)config.currentImageSize.x;
			constants._StpTaaConstants1.y = 1f / (float)config.currentImageSize.y;
			constants._StpTaaConstants1.z = 1f / (float)config.outputImageSize.x;
			constants._StpTaaConstants1.w = 1f / (float)config.outputImageSize.y;
			constants._StpTaaConstants2.x = 0.5f / (float)config.outputImageSize.x;
			constants._StpTaaConstants2.y = 0.5f / (float)config.outputImageSize.y;
			constants._StpTaaConstants2.z = jitC.x / (float)config.currentImageSize.x - 0.5f / (float)config.currentImageSize.x;
			constants._StpTaaConstants2.w = jitC.y / (float)config.currentImageSize.y + 0.5f / (float)config.currentImageSize.y;
			constants._StpTaaConstants3.x = 0.5f / (float)config.currentImageSize.x;
			constants._StpTaaConstants3.y = 0.5f / (float)config.currentImageSize.y;
			constants._StpTaaConstants3.z = (float)config.outputImageSize.x;
			constants._StpTaaConstants3.w = (float)config.outputImageSize.y;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00026046 File Offset: 0x00024246
		private static TextureHandle UseTexture(IBaseRenderGraphBuilder builder, TextureHandle texture, AccessFlags flags = AccessFlags.Read)
		{
			builder.UseTexture(in texture, flags);
			return texture;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00026054 File Offset: 0x00024254
		public static TextureHandle Execute(RenderGraph renderGraph, ref STP.Config config)
		{
			STP.RuntimeResources runtimeResources = GraphicsSettings.GetRenderPipelineSettings<STP.RuntimeResources>();
			Texture2D noiseTexture = config.noiseTexture;
			RTHandleStaticHelpers.SetRTHandleStaticWrapper(noiseTexture);
			RTHandle noiseTextureRtHandle = RTHandleStaticHelpers.s_RTHandleWrapper;
			RenderTargetInfo noiseTextureInfo;
			noiseTextureInfo.width = noiseTexture.width;
			noiseTextureInfo.height = noiseTexture.height;
			noiseTextureInfo.volumeDepth = 1;
			noiseTextureInfo.msaaSamples = 1;
			noiseTextureInfo.format = noiseTexture.graphicsFormat;
			noiseTextureInfo.bindMS = false;
			TextureHandle noiseTextureHandle = renderGraph.ImportTexture(noiseTextureRtHandle, noiseTextureInfo, default(ImportResourceParams));
			RTHandle priorDepthMotion = config.historyContext.GetPreviousHistoryTexture(STP.HistoryTextureType.DepthMotion, config.frameIndex);
			RTHandle priorLuma = config.historyContext.GetPreviousHistoryTexture(STP.HistoryTextureType.Luma, config.frameIndex);
			RTHandle priorConvergence = config.historyContext.GetPreviousHistoryTexture(STP.HistoryTextureType.Convergence, config.frameIndex);
			RTHandle priorFeedback = config.historyContext.GetPreviousHistoryTexture(STP.HistoryTextureType.Feedback, config.frameIndex);
			RTHandle depthMotion = config.historyContext.GetCurrentHistoryTexture(STP.HistoryTextureType.DepthMotion, config.frameIndex);
			RTHandle luma = config.historyContext.GetCurrentHistoryTexture(STP.HistoryTextureType.Luma, config.frameIndex);
			RTHandle convergence = config.historyContext.GetCurrentHistoryTexture(STP.HistoryTextureType.Convergence, config.frameIndex);
			RTHandle feedback = config.historyContext.GetCurrentHistoryTexture(STP.HistoryTextureType.Feedback, config.frameIndex);
			if (config.enableHwDrs)
			{
				depthMotion.rt.ApplyDynamicScale();
				luma.rt.ApplyDynamicScale();
				convergence.rt.ApplyDynamicScale();
			}
			Vector2Int intermediateSize = (config.enableHwDrs ? config.outputImageSize : config.currentImageSize);
			bool enableLargeKernel = SystemInfo.graphicsDeviceVendorID == STP.kQualcommVendorId;
			Vector2Int kernelSize = new Vector2Int(8, enableLargeKernel ? 16 : 8);
			STP.SetupData passData;
			STP.SetupData setupData;
			using (IComputeRenderGraphBuilder builder = renderGraph.AddComputePass<STP.SetupData>("STP Setup", out passData, ProfilingSampler.Get<STP.ProfileId>(STP.ProfileId.StpSetup), "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/STP/STP.cs", 1108))
			{
				passData.cs = runtimeResources.setupCS;
				passData.cs.shaderKeywords = null;
				if (enableLargeKernel)
				{
					passData.cs.EnableKeyword(STP.ShaderKeywords.EnableLargeKernel);
				}
				if (!config.enableTexArray)
				{
					passData.cs.EnableKeyword(STP.ShaderKeywords.DisableTexture2DXArray);
				}
				STP.PopulateConstantData(ref config, ref passData.constantBufferData);
				passData.noiseTexture = STP.UseTexture(builder, noiseTextureHandle, AccessFlags.Read);
				if (config.debugView.IsValid())
				{
					passData.cs.EnableKeyword(STP.ShaderKeywords.EnableDebugMode);
					passData.debugView = STP.UseTexture(builder, config.debugView, AccessFlags.WriteAll);
				}
				passData.kernelIndex = passData.cs.FindKernel("StpSetup");
				passData.viewCount = config.numActiveViews;
				passData.dispatchSize = new Vector2Int(CoreUtils.DivRoundUp(config.currentImageSize.x, kernelSize.x), CoreUtils.DivRoundUp(config.currentImageSize.y, kernelSize.y));
				passData.inputColor = STP.UseTexture(builder, config.inputColor, AccessFlags.Read);
				passData.inputDepth = STP.UseTexture(builder, config.inputDepth, AccessFlags.Read);
				passData.inputMotion = STP.UseTexture(builder, config.inputMotion, AccessFlags.Read);
				if (config.inputStencil.IsValid())
				{
					passData.cs.EnableKeyword(STP.ShaderKeywords.EnableStencilResponsive);
					passData.inputStencil = STP.UseTexture(builder, config.inputStencil, AccessFlags.Read);
				}
				STP.SetupData setupData2 = passData;
				IBaseRenderGraphBuilder baseRenderGraphBuilder = builder;
				TextureDesc textureDesc = new TextureDesc(intermediateSize.x, intermediateSize.y, config.enableHwDrs, config.enableTexArray);
				textureDesc.name = "STP Intermediate Color";
				textureDesc.format = GraphicsFormat.A2B10G10R10_UNormPack32;
				textureDesc.enableRandomWrite = true;
				setupData2.intermediateColor = STP.UseTexture(baseRenderGraphBuilder, renderGraph.CreateTexture(in textureDesc), AccessFlags.WriteAll);
				Vector2Int convergenceSize = STP.CalculateConvergenceTextureSize(intermediateSize);
				STP.SetupData setupData3 = passData;
				IBaseRenderGraphBuilder baseRenderGraphBuilder2 = builder;
				textureDesc = new TextureDesc(convergenceSize.x, convergenceSize.y, config.enableHwDrs, config.enableTexArray);
				textureDesc.name = "STP Intermediate Convergence";
				textureDesc.format = GraphicsFormat.R8_UNorm;
				textureDesc.enableRandomWrite = true;
				setupData3.intermediateConvergence = STP.UseTexture(baseRenderGraphBuilder2, renderGraph.CreateTexture(in textureDesc), AccessFlags.WriteAll);
				passData.priorDepthMotion = STP.UseTexture(builder, renderGraph.ImportTexture(priorDepthMotion), AccessFlags.Read);
				passData.depthMotion = STP.UseTexture(builder, renderGraph.ImportTexture(depthMotion), AccessFlags.WriteAll);
				passData.priorLuma = STP.UseTexture(builder, renderGraph.ImportTexture(priorLuma), AccessFlags.Read);
				passData.luma = STP.UseTexture(builder, renderGraph.ImportTexture(luma), AccessFlags.WriteAll);
				passData.priorFeedback = STP.UseTexture(builder, renderGraph.ImportTexture(priorFeedback), AccessFlags.Read);
				passData.priorConvergence = STP.UseTexture(builder, renderGraph.ImportTexture(priorConvergence), AccessFlags.Read);
				builder.SetRenderFunc<STP.SetupData>(delegate(STP.SetupData data, ComputeGraphContext ctx)
				{
					ConstantBuffer.UpdateData<STP.StpConstantBufferData>(ctx.cmd.m_WrappedCommandBuffer, in data.constantBufferData);
					ConstantBuffer.Set<STP.StpConstantBufferData>(data.cs, STP.ShaderResources._StpConstantBufferData);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpBlueNoiseIn, data.noiseTexture);
					if (data.debugView.IsValid())
					{
						ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpDebugOut, data.debugView);
					}
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpInputColor, data.inputColor);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpInputDepth, data.inputDepth);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpInputMotion, data.inputMotion);
					if (data.inputStencil.IsValid())
					{
						ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpInputStencil, data.inputStencil, 0, RenderTextureSubElement.Stencil);
					}
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpIntermediateColor, data.intermediateColor);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpIntermediateConvergence, data.intermediateConvergence);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpPriorDepthMotion, data.priorDepthMotion);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpDepthMotion, data.depthMotion);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpPriorLuma, data.priorLuma);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpLuma, data.luma);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpPriorFeedback, data.priorFeedback);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpPriorConvergence, data.priorConvergence);
					ctx.cmd.DispatchCompute(data.cs, data.kernelIndex, data.dispatchSize.x, data.dispatchSize.y, data.viewCount);
				});
				setupData = passData;
			}
			STP.PreTaaData passData2;
			STP.PreTaaData preTaaData;
			using (IComputeRenderGraphBuilder builder2 = renderGraph.AddComputePass<STP.PreTaaData>("STP Pre-TAA", out passData2, ProfilingSampler.Get<STP.ProfileId>(STP.ProfileId.StpPreTaa), "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/STP/STP.cs", 1211))
			{
				passData2.cs = runtimeResources.preTaaCS;
				passData2.cs.shaderKeywords = null;
				if (enableLargeKernel)
				{
					passData2.cs.EnableKeyword(STP.ShaderKeywords.EnableLargeKernel);
				}
				if (!config.enableTexArray)
				{
					passData2.cs.EnableKeyword(STP.ShaderKeywords.DisableTexture2DXArray);
				}
				passData2.noiseTexture = STP.UseTexture(builder2, noiseTextureHandle, AccessFlags.Read);
				if (config.debugView.IsValid())
				{
					passData2.cs.EnableKeyword(STP.ShaderKeywords.EnableDebugMode);
					passData2.debugView = STP.UseTexture(builder2, config.debugView, AccessFlags.ReadWrite);
				}
				passData2.kernelIndex = passData2.cs.FindKernel("StpPreTaa");
				passData2.viewCount = config.numActiveViews;
				passData2.dispatchSize = new Vector2Int(CoreUtils.DivRoundUp(config.currentImageSize.x, kernelSize.x), CoreUtils.DivRoundUp(config.currentImageSize.y, kernelSize.y));
				passData2.intermediateConvergence = STP.UseTexture(builder2, setupData.intermediateConvergence, AccessFlags.Read);
				STP.PreTaaData preTaaData2 = passData2;
				IBaseRenderGraphBuilder baseRenderGraphBuilder3 = builder2;
				TextureDesc textureDesc = new TextureDesc(intermediateSize.x, intermediateSize.y, config.enableHwDrs, config.enableTexArray);
				textureDesc.name = "STP Intermediate Weights";
				textureDesc.format = GraphicsFormat.R8_UNorm;
				textureDesc.enableRandomWrite = true;
				preTaaData2.intermediateWeights = STP.UseTexture(baseRenderGraphBuilder3, renderGraph.CreateTexture(in textureDesc), AccessFlags.WriteAll);
				passData2.luma = STP.UseTexture(builder2, renderGraph.ImportTexture(luma), AccessFlags.Read);
				passData2.convergence = STP.UseTexture(builder2, renderGraph.ImportTexture(convergence), AccessFlags.WriteAll);
				builder2.SetRenderFunc<STP.PreTaaData>(delegate(STP.PreTaaData data, ComputeGraphContext ctx)
				{
					ConstantBuffer.Set<STP.StpConstantBufferData>(data.cs, STP.ShaderResources._StpConstantBufferData);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpBlueNoiseIn, data.noiseTexture);
					if (data.debugView.IsValid())
					{
						ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpDebugOut, data.debugView);
					}
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpIntermediateConvergence, data.intermediateConvergence);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpIntermediateWeights, data.intermediateWeights);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpLuma, data.luma);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpConvergence, data.convergence);
					ctx.cmd.DispatchCompute(data.cs, data.kernelIndex, data.dispatchSize.x, data.dispatchSize.y, data.viewCount);
				});
				preTaaData = passData2;
			}
			STP.TaaData passData3;
			STP.TaaData taaData;
			using (IComputeRenderGraphBuilder builder3 = renderGraph.AddComputePass<STP.TaaData>("STP TAA", out passData3, ProfilingSampler.Get<STP.ProfileId>(STP.ProfileId.StpTaa), "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/STP/STP.cs", 1274))
			{
				passData3.cs = runtimeResources.taaCS;
				passData3.cs.shaderKeywords = null;
				if (enableLargeKernel)
				{
					passData3.cs.EnableKeyword(STP.ShaderKeywords.EnableLargeKernel);
				}
				if (!config.enableTexArray)
				{
					passData3.cs.EnableKeyword(STP.ShaderKeywords.DisableTexture2DXArray);
				}
				passData3.noiseTexture = STP.UseTexture(builder3, noiseTextureHandle, AccessFlags.Read);
				if (config.debugView.IsValid())
				{
					passData3.cs.EnableKeyword(STP.ShaderKeywords.EnableDebugMode);
					passData3.debugView = STP.UseTexture(builder3, config.debugView, AccessFlags.ReadWrite);
				}
				passData3.kernelIndex = passData3.cs.FindKernel("StpTaa");
				passData3.viewCount = config.numActiveViews;
				passData3.dispatchSize = new Vector2Int(CoreUtils.DivRoundUp(config.outputImageSize.x, kernelSize.x), CoreUtils.DivRoundUp(config.outputImageSize.y, kernelSize.y));
				passData3.intermediateColor = STP.UseTexture(builder3, setupData.intermediateColor, AccessFlags.Read);
				passData3.intermediateWeights = STP.UseTexture(builder3, preTaaData.intermediateWeights, AccessFlags.Read);
				passData3.priorFeedback = STP.UseTexture(builder3, renderGraph.ImportTexture(priorFeedback), AccessFlags.Read);
				passData3.depthMotion = STP.UseTexture(builder3, renderGraph.ImportTexture(depthMotion), AccessFlags.Read);
				passData3.convergence = STP.UseTexture(builder3, renderGraph.ImportTexture(convergence), AccessFlags.Read);
				passData3.feedback = STP.UseTexture(builder3, renderGraph.ImportTexture(feedback), AccessFlags.WriteAll);
				passData3.output = STP.UseTexture(builder3, config.destination, AccessFlags.WriteAll);
				builder3.SetRenderFunc<STP.TaaData>(delegate(STP.TaaData data, ComputeGraphContext ctx)
				{
					ConstantBuffer.Set<STP.StpConstantBufferData>(data.cs, STP.ShaderResources._StpConstantBufferData);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpBlueNoiseIn, data.noiseTexture);
					if (data.debugView.IsValid())
					{
						ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpDebugOut, data.debugView);
					}
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpIntermediateColor, data.intermediateColor);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpIntermediateWeights, data.intermediateWeights);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpPriorFeedback, data.priorFeedback);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpDepthMotion, data.depthMotion);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpConvergence, data.convergence);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpFeedback, data.feedback);
					ctx.cmd.SetComputeTextureParam(data.cs, data.kernelIndex, STP.ShaderResources._StpOutput, data.output);
					ctx.cmd.DispatchCompute(data.cs, data.kernelIndex, data.dispatchSize.x, data.dispatchSize.y, data.viewCount);
				});
				taaData = passData3;
			}
			return taaData.output;
		}

		// Token: 0x040006C5 RID: 1733
		private const int kNumDebugViews = 6;

		// Token: 0x040006C6 RID: 1734
		private static readonly GUIContent[] s_DebugViewDescriptions = new GUIContent[]
		{
			new GUIContent("Clipped Input Color", "Shows input color clipped to {0 to 1}"),
			new GUIContent("Log Input Depth", "Shows input depth in log scale"),
			new GUIContent("Reversible Tonemapped Input Color", "Shows input color after conversion to reversible tonemaped space"),
			new GUIContent("Shaped Absolute Input Motion", "Visualizes input motion vectors"),
			new GUIContent("Motion Reprojection {R=Prior G=This Sqrt Luma Feedback Diff, B=Offscreen}", "Visualizes reprojected frame difference"),
			new GUIContent("Sensitivity {G=No motion match, R=Responsive, B=Luma}", "Visualize pixel sensitivities")
		};

		// Token: 0x040006C7 RID: 1735
		private static readonly int[] s_DebugViewIndices = new int[] { 0, 1, 2, 3, 4, 5 };

		// Token: 0x040006C8 RID: 1736
		private const int kMaxPerViewConfigs = 2;

		// Token: 0x040006C9 RID: 1737
		private static STP.PerViewConfig[] s_PerViewConfigs = new STP.PerViewConfig[2];

		// Token: 0x040006CA RID: 1738
		private const int kNumHistoryTextureTypes = 4;

		// Token: 0x040006CB RID: 1739
		private const int kTotalSetupViewConstantsCount = 16;

		// Token: 0x040006CC RID: 1740
		private static readonly int kQualcommVendorId = 20803;

		// Token: 0x02000166 RID: 358
		public struct PerViewConfig
		{
			// Token: 0x040006CD RID: 1741
			public Matrix4x4 currentProj;

			// Token: 0x040006CE RID: 1742
			public Matrix4x4 lastProj;

			// Token: 0x040006CF RID: 1743
			public Matrix4x4 lastLastProj;

			// Token: 0x040006D0 RID: 1744
			public Matrix4x4 currentView;

			// Token: 0x040006D1 RID: 1745
			public Matrix4x4 lastView;

			// Token: 0x040006D2 RID: 1746
			public Matrix4x4 lastLastView;
		}

		// Token: 0x02000167 RID: 359
		public struct Config
		{
			// Token: 0x040006D3 RID: 1747
			public Texture2D noiseTexture;

			// Token: 0x040006D4 RID: 1748
			public TextureHandle inputColor;

			// Token: 0x040006D5 RID: 1749
			public TextureHandle inputDepth;

			// Token: 0x040006D6 RID: 1750
			public TextureHandle inputMotion;

			// Token: 0x040006D7 RID: 1751
			public TextureHandle inputStencil;

			// Token: 0x040006D8 RID: 1752
			public TextureHandle debugView;

			// Token: 0x040006D9 RID: 1753
			public TextureHandle destination;

			// Token: 0x040006DA RID: 1754
			public STP.HistoryContext historyContext;

			// Token: 0x040006DB RID: 1755
			public bool enableHwDrs;

			// Token: 0x040006DC RID: 1756
			public bool enableTexArray;

			// Token: 0x040006DD RID: 1757
			public bool enableMotionScaling;

			// Token: 0x040006DE RID: 1758
			public float nearPlane;

			// Token: 0x040006DF RID: 1759
			public float farPlane;

			// Token: 0x040006E0 RID: 1760
			public int frameIndex;

			// Token: 0x040006E1 RID: 1761
			public bool hasValidHistory;

			// Token: 0x040006E2 RID: 1762
			public int stencilMask;

			// Token: 0x040006E3 RID: 1763
			public int debugViewIndex;

			// Token: 0x040006E4 RID: 1764
			public float deltaTime;

			// Token: 0x040006E5 RID: 1765
			public float lastDeltaTime;

			// Token: 0x040006E6 RID: 1766
			public Vector2Int currentImageSize;

			// Token: 0x040006E7 RID: 1767
			public Vector2Int priorImageSize;

			// Token: 0x040006E8 RID: 1768
			public Vector2Int outputImageSize;

			// Token: 0x040006E9 RID: 1769
			public int numActiveViews;

			// Token: 0x040006EA RID: 1770
			public STP.PerViewConfig[] perViewConfigs;
		}

		// Token: 0x02000168 RID: 360
		internal enum HistoryTextureType
		{
			// Token: 0x040006EC RID: 1772
			DepthMotion,
			// Token: 0x040006ED RID: 1773
			Luma,
			// Token: 0x040006EE RID: 1774
			Convergence,
			// Token: 0x040006EF RID: 1775
			Feedback,
			// Token: 0x040006F0 RID: 1776
			Count
		}

		// Token: 0x02000169 RID: 361
		public struct HistoryUpdateInfo
		{
			// Token: 0x040006F1 RID: 1777
			public Vector2Int preUpscaleSize;

			// Token: 0x040006F2 RID: 1778
			public Vector2Int postUpscaleSize;

			// Token: 0x040006F3 RID: 1779
			public bool useHwDrs;

			// Token: 0x040006F4 RID: 1780
			public bool useTexArray;
		}

		// Token: 0x0200016A RID: 362
		public sealed class HistoryContext : IDisposable
		{
			// Token: 0x06000AA9 RID: 2729 RVA: 0x00026990 File Offset: 0x00024B90
			public bool Update(ref STP.HistoryUpdateInfo info)
			{
				bool hasValidHistory = true;
				Hash128 hash = STP.ComputeHistoryHash(ref info);
				if (hash != this.m_hash)
				{
					hasValidHistory = false;
					this.Dispose();
					this.m_hash = hash;
					Vector2Int historyTextureSize = (info.useHwDrs ? info.postUpscaleSize : info.preUpscaleSize);
					TextureDimension texDimension = (info.useTexArray ? TextureDimension.Tex2DArray : TextureDimension.Tex2D);
					int width = 0;
					int height = 0;
					GraphicsFormat format = GraphicsFormat.None;
					bool useDynamicScaleExplicit = false;
					string name = "";
					for (int historyTypeIndex = 0; historyTypeIndex < 4; historyTypeIndex++)
					{
						switch (historyTypeIndex)
						{
						case 0:
							width = historyTextureSize.x;
							height = historyTextureSize.y;
							format = GraphicsFormat.R32_UInt;
							useDynamicScaleExplicit = info.useHwDrs;
							name = "STP Depth & Motion";
							break;
						case 1:
							width = historyTextureSize.x;
							height = historyTextureSize.y;
							format = GraphicsFormat.R8G8_UNorm;
							useDynamicScaleExplicit = info.useHwDrs;
							name = "STP Luma";
							break;
						case 2:
						{
							Vector2Int convergenceSize = STP.CalculateConvergenceTextureSize(historyTextureSize);
							width = convergenceSize.x;
							height = convergenceSize.y;
							format = GraphicsFormat.R8_UNorm;
							useDynamicScaleExplicit = info.useHwDrs;
							name = "STP Convergence";
							break;
						}
						case 3:
							width = info.postUpscaleSize.x;
							height = info.postUpscaleSize.y;
							format = GraphicsFormat.A2B10G10R10_UNormPack32;
							useDynamicScaleExplicit = false;
							name = "STP Feedback";
							break;
						}
						for (int frameIndex = 0; frameIndex < 2; frameIndex++)
						{
							int offset = frameIndex * 4 + historyTypeIndex;
							RTHandle[] textures = this.m_textures;
							int num = offset;
							int num2 = width;
							int num3 = height;
							GraphicsFormat graphicsFormat = format;
							int slices = TextureXR.slices;
							FilterMode filterMode = FilterMode.Point;
							TextureWrapMode textureWrapMode = TextureWrapMode.Repeat;
							TextureDimension textureDimension = texDimension;
							bool flag = true;
							bool flag2 = false;
							bool flag3 = true;
							bool flag4 = false;
							int num4 = 1;
							float num5 = 0f;
							MSAASamples msaasamples = MSAASamples.None;
							bool flag5 = false;
							bool flag6 = false;
							string text = name;
							textures[num] = RTHandles.Alloc(num2, num3, graphicsFormat, slices, filterMode, textureWrapMode, textureDimension, flag, flag2, flag3, flag4, num4, num5, msaasamples, flag5, flag6, useDynamicScaleExplicit, RenderTextureMemoryless.None, VRTextureUsage.None, text);
						}
					}
				}
				return hasValidHistory;
			}

			// Token: 0x06000AAA RID: 2730 RVA: 0x00026B23 File Offset: 0x00024D23
			internal RTHandle GetCurrentHistoryTexture(STP.HistoryTextureType historyType, int frameIndex)
			{
				return this.m_textures[(int)((frameIndex & 1) * 4 + historyType)];
			}

			// Token: 0x06000AAB RID: 2731 RVA: 0x00026B33 File Offset: 0x00024D33
			internal RTHandle GetPreviousHistoryTexture(STP.HistoryTextureType historyType, int frameIndex)
			{
				return this.m_textures[(int)(((frameIndex & 1) ^ 1) * 4 + historyType)];
			}

			// Token: 0x06000AAC RID: 2732 RVA: 0x00026B48 File Offset: 0x00024D48
			public void Dispose()
			{
				for (int texIndex = 0; texIndex < this.m_textures.Length; texIndex++)
				{
					if (this.m_textures[texIndex] != null)
					{
						this.m_textures[texIndex].Release();
						this.m_textures[texIndex] = null;
					}
				}
				this.m_hash = Hash128.Compute(0);
			}

			// Token: 0x040006F5 RID: 1781
			private RTHandle[] m_textures = new RTHandle[8];

			// Token: 0x040006F6 RID: 1782
			private Hash128 m_hash = Hash128.Compute(0);
		}

		// Token: 0x0200016B RID: 363
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/STP/STP.cs")]
		private enum StpSetupPerViewConstants
		{
			// Token: 0x040006F8 RID: 1784
			Count = 8
		}

		// Token: 0x0200016C RID: 364
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/STP/STP.cs", needAccessors = false, generateCBuffer = true)]
		private struct StpConstantBufferData
		{
			// Token: 0x040006F9 RID: 1785
			public Vector4 _StpCommonConstant;

			// Token: 0x040006FA RID: 1786
			public Vector4 _StpSetupConstants0;

			// Token: 0x040006FB RID: 1787
			public Vector4 _StpSetupConstants1;

			// Token: 0x040006FC RID: 1788
			public Vector4 _StpSetupConstants2;

			// Token: 0x040006FD RID: 1789
			public Vector4 _StpSetupConstants3;

			// Token: 0x040006FE RID: 1790
			public Vector4 _StpSetupConstants4;

			// Token: 0x040006FF RID: 1791
			public Vector4 _StpSetupConstants5;

			// Token: 0x04000700 RID: 1792
			[FixedBuffer(typeof(float), 64)]
			[HLSLArray(16, typeof(Vector4))]
			public STP.StpConstantBufferData.<_StpSetupPerViewConstants>e__FixedBuffer _StpSetupPerViewConstants;

			// Token: 0x04000701 RID: 1793
			public Vector4 _StpDilConstants0;

			// Token: 0x04000702 RID: 1794
			public Vector4 _StpTaaConstants0;

			// Token: 0x04000703 RID: 1795
			public Vector4 _StpTaaConstants1;

			// Token: 0x04000704 RID: 1796
			public Vector4 _StpTaaConstants2;

			// Token: 0x04000705 RID: 1797
			public Vector4 _StpTaaConstants3;

			// Token: 0x0200016D RID: 365
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 256)]
			public struct <_StpSetupPerViewConstants>e__FixedBuffer
			{
				// Token: 0x04000706 RID: 1798
				public float FixedElementField;
			}
		}

		// Token: 0x0200016E RID: 366
		private static class ShaderResources
		{
			// Token: 0x04000707 RID: 1799
			public static readonly int _StpConstantBufferData = Shader.PropertyToID("StpConstantBufferData");

			// Token: 0x04000708 RID: 1800
			public static readonly int _StpBlueNoiseIn = Shader.PropertyToID("_StpBlueNoiseIn");

			// Token: 0x04000709 RID: 1801
			public static readonly int _StpDebugOut = Shader.PropertyToID("_StpDebugOut");

			// Token: 0x0400070A RID: 1802
			public static readonly int _StpInputColor = Shader.PropertyToID("_StpInputColor");

			// Token: 0x0400070B RID: 1803
			public static readonly int _StpInputDepth = Shader.PropertyToID("_StpInputDepth");

			// Token: 0x0400070C RID: 1804
			public static readonly int _StpInputMotion = Shader.PropertyToID("_StpInputMotion");

			// Token: 0x0400070D RID: 1805
			public static readonly int _StpInputStencil = Shader.PropertyToID("_StpInputStencil");

			// Token: 0x0400070E RID: 1806
			public static readonly int _StpIntermediateColor = Shader.PropertyToID("_StpIntermediateColor");

			// Token: 0x0400070F RID: 1807
			public static readonly int _StpIntermediateConvergence = Shader.PropertyToID("_StpIntermediateConvergence");

			// Token: 0x04000710 RID: 1808
			public static readonly int _StpIntermediateWeights = Shader.PropertyToID("_StpIntermediateWeights");

			// Token: 0x04000711 RID: 1809
			public static readonly int _StpPriorLuma = Shader.PropertyToID("_StpPriorLuma");

			// Token: 0x04000712 RID: 1810
			public static readonly int _StpLuma = Shader.PropertyToID("_StpLuma");

			// Token: 0x04000713 RID: 1811
			public static readonly int _StpPriorDepthMotion = Shader.PropertyToID("_StpPriorDepthMotion");

			// Token: 0x04000714 RID: 1812
			public static readonly int _StpDepthMotion = Shader.PropertyToID("_StpDepthMotion");

			// Token: 0x04000715 RID: 1813
			public static readonly int _StpPriorFeedback = Shader.PropertyToID("_StpPriorFeedback");

			// Token: 0x04000716 RID: 1814
			public static readonly int _StpFeedback = Shader.PropertyToID("_StpFeedback");

			// Token: 0x04000717 RID: 1815
			public static readonly int _StpPriorConvergence = Shader.PropertyToID("_StpPriorConvergence");

			// Token: 0x04000718 RID: 1816
			public static readonly int _StpConvergence = Shader.PropertyToID("_StpConvergence");

			// Token: 0x04000719 RID: 1817
			public static readonly int _StpOutput = Shader.PropertyToID("_StpOutput");
		}

		// Token: 0x0200016F RID: 367
		private static class ShaderKeywords
		{
			// Token: 0x0400071A RID: 1818
			public static readonly string EnableDebugMode = "ENABLE_DEBUG_MODE";

			// Token: 0x0400071B RID: 1819
			public static readonly string EnableLargeKernel = "ENABLE_LARGE_KERNEL";

			// Token: 0x0400071C RID: 1820
			public static readonly string EnableStencilResponsive = "ENABLE_STENCIL_RESPONSIVE";

			// Token: 0x0400071D RID: 1821
			public static readonly string DisableTexture2DXArray = "DISABLE_TEXTURE2D_X_ARRAY";
		}

		// Token: 0x02000170 RID: 368
		[SupportedOnRenderPipeline(new Type[] { })]
		[CategoryInfo(Name = "R: STP", Order = 1000)]
		[ElementInfo(Order = 0)]
		[HideInInspector]
		[Serializable]
		internal class RuntimeResources : IRenderPipelineResources, IRenderPipelineGraphicsSettings
		{
			// Token: 0x1700013E RID: 318
			// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x000090C6 File Offset: 0x000072C6
			public int version
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00026D08 File Offset: 0x00024F08
			// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x00026D10 File Offset: 0x00024F10
			public ComputeShader setupCS
			{
				get
				{
					return this.m_setupCS;
				}
				set
				{
					this.SetValueAndNotify(ref this.m_setupCS, value, "setupCS");
				}
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00026D24 File Offset: 0x00024F24
			// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x00026D2C File Offset: 0x00024F2C
			public ComputeShader preTaaCS
			{
				get
				{
					return this.m_preTaaCS;
				}
				set
				{
					this.SetValueAndNotify(ref this.m_preTaaCS, value, "preTaaCS");
				}
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00026D40 File Offset: 0x00024F40
			// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x00026D48 File Offset: 0x00024F48
			public ComputeShader taaCS
			{
				get
				{
					return this.m_taaCS;
				}
				set
				{
					this.SetValueAndNotify(ref this.m_taaCS, value, "taaCS");
				}
			}

			// Token: 0x0400071E RID: 1822
			[SerializeField]
			[ResourcePath("Runtime/STP/StpSetup.compute", SearchType.ProjectPath)]
			private ComputeShader m_setupCS;

			// Token: 0x0400071F RID: 1823
			[SerializeField]
			[ResourcePath("Runtime/STP/StpPreTaa.compute", SearchType.ProjectPath)]
			private ComputeShader m_preTaaCS;

			// Token: 0x04000720 RID: 1824
			[SerializeField]
			[ResourcePath("Runtime/STP/StpTaa.compute", SearchType.ProjectPath)]
			private ComputeShader m_taaCS;
		}

		// Token: 0x02000171 RID: 369
		private enum ProfileId
		{
			// Token: 0x04000722 RID: 1826
			StpSetup,
			// Token: 0x04000723 RID: 1827
			StpPreTaa,
			// Token: 0x04000724 RID: 1828
			StpTaa
		}

		// Token: 0x02000172 RID: 370
		private class SetupData
		{
			// Token: 0x04000725 RID: 1829
			public ComputeShader cs;

			// Token: 0x04000726 RID: 1830
			public int kernelIndex;

			// Token: 0x04000727 RID: 1831
			public int viewCount;

			// Token: 0x04000728 RID: 1832
			public Vector2Int dispatchSize;

			// Token: 0x04000729 RID: 1833
			public STP.StpConstantBufferData constantBufferData;

			// Token: 0x0400072A RID: 1834
			public TextureHandle noiseTexture;

			// Token: 0x0400072B RID: 1835
			public TextureHandle debugView;

			// Token: 0x0400072C RID: 1836
			public TextureHandle inputColor;

			// Token: 0x0400072D RID: 1837
			public TextureHandle inputDepth;

			// Token: 0x0400072E RID: 1838
			public TextureHandle inputMotion;

			// Token: 0x0400072F RID: 1839
			public TextureHandle inputStencil;

			// Token: 0x04000730 RID: 1840
			public TextureHandle intermediateColor;

			// Token: 0x04000731 RID: 1841
			public TextureHandle intermediateConvergence;

			// Token: 0x04000732 RID: 1842
			public TextureHandle priorDepthMotion;

			// Token: 0x04000733 RID: 1843
			public TextureHandle depthMotion;

			// Token: 0x04000734 RID: 1844
			public TextureHandle priorLuma;

			// Token: 0x04000735 RID: 1845
			public TextureHandle luma;

			// Token: 0x04000736 RID: 1846
			public TextureHandle priorFeedback;

			// Token: 0x04000737 RID: 1847
			public TextureHandle priorConvergence;
		}

		// Token: 0x02000173 RID: 371
		private class PreTaaData
		{
			// Token: 0x04000738 RID: 1848
			public ComputeShader cs;

			// Token: 0x04000739 RID: 1849
			public int kernelIndex;

			// Token: 0x0400073A RID: 1850
			public int viewCount;

			// Token: 0x0400073B RID: 1851
			public Vector2Int dispatchSize;

			// Token: 0x0400073C RID: 1852
			public TextureHandle noiseTexture;

			// Token: 0x0400073D RID: 1853
			public TextureHandle debugView;

			// Token: 0x0400073E RID: 1854
			public TextureHandle intermediateConvergence;

			// Token: 0x0400073F RID: 1855
			public TextureHandle intermediateWeights;

			// Token: 0x04000740 RID: 1856
			public TextureHandle luma;

			// Token: 0x04000741 RID: 1857
			public TextureHandle convergence;
		}

		// Token: 0x02000174 RID: 372
		private class TaaData
		{
			// Token: 0x04000742 RID: 1858
			public ComputeShader cs;

			// Token: 0x04000743 RID: 1859
			public int kernelIndex;

			// Token: 0x04000744 RID: 1860
			public int viewCount;

			// Token: 0x04000745 RID: 1861
			public Vector2Int dispatchSize;

			// Token: 0x04000746 RID: 1862
			public TextureHandle noiseTexture;

			// Token: 0x04000747 RID: 1863
			public TextureHandle debugView;

			// Token: 0x04000748 RID: 1864
			public TextureHandle intermediateColor;

			// Token: 0x04000749 RID: 1865
			public TextureHandle intermediateWeights;

			// Token: 0x0400074A RID: 1866
			public TextureHandle priorFeedback;

			// Token: 0x0400074B RID: 1867
			public TextureHandle depthMotion;

			// Token: 0x0400074C RID: 1868
			public TextureHandle convergence;

			// Token: 0x0400074D RID: 1869
			public TextureHandle feedback;

			// Token: 0x0400074E RID: 1870
			public TextureHandle output;
		}
	}
}
