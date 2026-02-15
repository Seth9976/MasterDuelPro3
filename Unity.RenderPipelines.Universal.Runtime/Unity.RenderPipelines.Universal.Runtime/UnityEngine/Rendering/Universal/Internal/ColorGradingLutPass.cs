using System;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x020001FF RID: 511
	public class ColorGradingLutPass : ScriptableRenderPass
	{
		// Token: 0x06000B82 RID: 2946 RVA: 0x0003ED98 File Offset: 0x0003CF98
		public ColorGradingLutPass(RenderPassEvent evt, PostProcessData data)
		{
			base.profilingSampler = new ProfilingSampler("Blit Color LUT");
			base.renderPassEvent = evt;
			base.overrideCameraTarget = true;
			this.m_LutBuilderLdr = ColorGradingLutPass.<.ctor>g__Load|7_0(data.shaders.lutBuilderLdrPS);
			this.m_LutBuilderHdr = ColorGradingLutPass.<.ctor>g__Load|7_0(data.shaders.lutBuilderHdrPS);
			if (SystemInfo.IsFormatSupported(GraphicsFormat.R16G16B16A16_SFloat, GraphicsFormatUsage.Blend))
			{
				this.m_HdrLutFormat = GraphicsFormat.R16G16B16A16_SFloat;
			}
			else if (SystemInfo.IsFormatSupported(GraphicsFormat.B10G11R11_UFloatPack32, GraphicsFormatUsage.Blend))
			{
				this.m_HdrLutFormat = GraphicsFormat.B10G11R11_UFloatPack32;
			}
			else
			{
				this.m_HdrLutFormat = GraphicsFormat.R8G8B8A8_UNorm;
			}
			this.m_LdrLutFormat = GraphicsFormat.R8G8B8A8_UNorm;
			base.useNativeRenderPass = false;
			if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3 && Graphics.minOpenGLESVersion <= OpenGLESVersion.OpenGLES30 && SystemInfo.graphicsDeviceName.StartsWith("Adreno (TM) 3"))
			{
				this.m_AllowColorGradingACESHDR = false;
			}
			this.m_PassData = new ColorGradingLutPass.PassData();
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0003EE6F File Offset: 0x0003D06F
		public void Setup(in RTHandle internalLut)
		{
			this.m_InternalLut = internalLut;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0003EE7C File Offset: 0x0003D07C
		public void ConfigureDescriptor(in PostProcessingData postProcessingData, out RenderTextureDescriptor descriptor, out FilterMode filterMode)
		{
			PostProcessingData postProcessingData2 = postProcessingData;
			UniversalPostProcessingData universalPostProcessingData = postProcessingData2.universalPostProcessingData;
			this.ConfigureDescriptor(in universalPostProcessingData, out descriptor, out filterMode);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0003EEA4 File Offset: 0x0003D0A4
		public void ConfigureDescriptor(in UniversalPostProcessingData postProcessingData, out RenderTextureDescriptor descriptor, out FilterMode filterMode)
		{
			bool flag = postProcessingData.gradingMode == ColorGradingMode.HighDynamicRange;
			int lutHeight = postProcessingData.lutSize;
			int lutWidth = lutHeight * lutHeight;
			GraphicsFormat format = (flag ? this.m_HdrLutFormat : this.m_LdrLutFormat);
			descriptor = new RenderTextureDescriptor(lutWidth, lutHeight, format, 0);
			descriptor.vrUsage = VRTextureUsage.None;
			filterMode = FilterMode.Bilinear;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0003EEF4 File Offset: 0x0003D0F4
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			ContextContainer frameData = renderingData.frameData;
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalPostProcessingData postProcessingData = frameData.Get<UniversalPostProcessingData>();
			this.m_PassData.cameraData = cameraData;
			this.m_PassData.postProcessingData = postProcessingData;
			this.m_PassData.lutBuilderLdr = this.m_LutBuilderLdr;
			this.m_PassData.lutBuilderHdr = this.m_LutBuilderHdr;
			this.m_PassData.allowColorGradingACESHDR = this.m_AllowColorGradingACESHDR;
			if (renderingData.cameraData.xr.supportsFoveatedRendering)
			{
				renderingData.commandBuffer->SetFoveatedRenderingMode(FoveatedRenderingMode.Disabled);
			}
			CoreUtils.SetRenderTarget(*renderingData.commandBuffer, this.m_InternalLut, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, ClearFlag.None, Color.clear, 0, CubemapFace.Unknown, -1);
			ColorGradingLutPass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer), this.m_PassData, this.m_InternalLut);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0003EFB8 File Offset: 0x0003D1B8
		private static void ExecutePass(RasterCommandBuffer cmd, ColorGradingLutPass.PassData passData, RTHandle internalLutTarget)
		{
			Material lutBuilderLdr = passData.lutBuilderLdr;
			Material lutBuilderHdr = passData.lutBuilderHdr;
			bool allowColorGradingACESHDR = passData.allowColorGradingACESHDR;
			using (new ProfilingScope(cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.ColorGradingLUT)))
			{
				VolumeStack stack = VolumeManager.instance.stack;
				ChannelMixer channelMixer = stack.GetComponent<ChannelMixer>();
				ColorAdjustments colorAdjustments = stack.GetComponent<ColorAdjustments>();
				ColorCurves curves = stack.GetComponent<ColorCurves>();
				LiftGammaGain liftGammaGain = stack.GetComponent<LiftGammaGain>();
				ShadowsMidtonesHighlights shadowsMidtonesHighlights = stack.GetComponent<ShadowsMidtonesHighlights>();
				SplitToning splitToning = stack.GetComponent<SplitToning>();
				Tonemapping tonemapping = stack.GetComponent<Tonemapping>();
				WhiteBalance whiteBalance = stack.GetComponent<WhiteBalance>();
				bool flag = passData.postProcessingData.gradingMode == ColorGradingMode.HighDynamicRange;
				Material material = (flag ? lutBuilderHdr : lutBuilderLdr);
				Vector3 lmsColorBalance = ColorUtils.ColorBalanceToLMSCoeffs(whiteBalance.temperature.value, whiteBalance.tint.value);
				Vector4 hueSatCon = new Vector4(colorAdjustments.hueShift.value / 360f, colorAdjustments.saturation.value / 100f + 1f, colorAdjustments.contrast.value / 100f + 1f, 0f);
				Vector4 channelMixerR = new Vector4(channelMixer.redOutRedIn.value / 100f, channelMixer.redOutGreenIn.value / 100f, channelMixer.redOutBlueIn.value / 100f, 0f);
				Vector4 channelMixerG = new Vector4(channelMixer.greenOutRedIn.value / 100f, channelMixer.greenOutGreenIn.value / 100f, channelMixer.greenOutBlueIn.value / 100f, 0f);
				Vector4 channelMixerB = new Vector4(channelMixer.blueOutRedIn.value / 100f, channelMixer.blueOutGreenIn.value / 100f, channelMixer.blueOutBlueIn.value / 100f, 0f);
				Vector4 shadowsHighlightsLimits = new Vector4(shadowsMidtonesHighlights.shadowsStart.value, shadowsMidtonesHighlights.shadowsEnd.value, shadowsMidtonesHighlights.highlightsStart.value, shadowsMidtonesHighlights.highlightsEnd.value);
				Vector4 vector = shadowsMidtonesHighlights.shadows.value;
				Vector4 vector2 = shadowsMidtonesHighlights.midtones.value;
				Vector4 vector3 = shadowsMidtonesHighlights.highlights.value;
				ValueTuple<Vector4, Vector4, Vector4> valueTuple = ColorUtils.PrepareShadowsMidtonesHighlights(in vector, in vector2, in vector3);
				Vector4 shadows = valueTuple.Item1;
				Vector4 midtones = valueTuple.Item2;
				Vector4 highlights = valueTuple.Item3;
				vector = liftGammaGain.lift.value;
				vector2 = liftGammaGain.gamma.value;
				vector3 = liftGammaGain.gain.value;
				ValueTuple<Vector4, Vector4, Vector4> valueTuple2 = ColorUtils.PrepareLiftGammaGain(in vector, in vector2, in vector3);
				Vector4 lift = valueTuple2.Item1;
				Vector4 gamma = valueTuple2.Item2;
				Vector4 gain = valueTuple2.Item3;
				vector = splitToning.shadows.value;
				vector2 = splitToning.highlights.value;
				ValueTuple<Vector4, Vector4> valueTuple3 = ColorUtils.PrepareSplitToning(in vector, in vector2, splitToning.balance.value);
				Vector4 splitShadows = valueTuple3.Item1;
				Vector4 splitHighlights = valueTuple3.Item2;
				int lutHeight = passData.postProcessingData.lutSize;
				int lutWidth = lutHeight * lutHeight;
				Vector4 lutParameters = new Vector4((float)lutHeight, 0.5f / (float)lutWidth, 0.5f / (float)lutHeight, (float)lutHeight / ((float)lutHeight - 1f));
				material.SetVector(ColorGradingLutPass.ShaderConstants._Lut_Params, lutParameters);
				material.SetVector(ColorGradingLutPass.ShaderConstants._ColorBalance, lmsColorBalance);
				material.SetVector(ColorGradingLutPass.ShaderConstants._ColorFilter, colorAdjustments.colorFilter.value.linear);
				material.SetVector(ColorGradingLutPass.ShaderConstants._ChannelMixerRed, channelMixerR);
				material.SetVector(ColorGradingLutPass.ShaderConstants._ChannelMixerGreen, channelMixerG);
				material.SetVector(ColorGradingLutPass.ShaderConstants._ChannelMixerBlue, channelMixerB);
				material.SetVector(ColorGradingLutPass.ShaderConstants._HueSatCon, hueSatCon);
				material.SetVector(ColorGradingLutPass.ShaderConstants._Lift, lift);
				material.SetVector(ColorGradingLutPass.ShaderConstants._Gamma, gamma);
				material.SetVector(ColorGradingLutPass.ShaderConstants._Gain, gain);
				material.SetVector(ColorGradingLutPass.ShaderConstants._Shadows, shadows);
				material.SetVector(ColorGradingLutPass.ShaderConstants._Midtones, midtones);
				material.SetVector(ColorGradingLutPass.ShaderConstants._Highlights, highlights);
				material.SetVector(ColorGradingLutPass.ShaderConstants._ShaHiLimits, shadowsHighlightsLimits);
				material.SetVector(ColorGradingLutPass.ShaderConstants._SplitShadows, splitShadows);
				material.SetVector(ColorGradingLutPass.ShaderConstants._SplitHighlights, splitHighlights);
				material.SetTexture(ColorGradingLutPass.ShaderConstants._CurveMaster, curves.master.value.GetTexture());
				material.SetTexture(ColorGradingLutPass.ShaderConstants._CurveRed, curves.red.value.GetTexture());
				material.SetTexture(ColorGradingLutPass.ShaderConstants._CurveGreen, curves.green.value.GetTexture());
				material.SetTexture(ColorGradingLutPass.ShaderConstants._CurveBlue, curves.blue.value.GetTexture());
				material.SetTexture(ColorGradingLutPass.ShaderConstants._CurveHueVsHue, curves.hueVsHue.value.GetTexture());
				material.SetTexture(ColorGradingLutPass.ShaderConstants._CurveHueVsSat, curves.hueVsSat.value.GetTexture());
				material.SetTexture(ColorGradingLutPass.ShaderConstants._CurveLumVsSat, curves.lumVsSat.value.GetTexture());
				material.SetTexture(ColorGradingLutPass.ShaderConstants._CurveSatVsSat, curves.satVsSat.value.GetTexture());
				if (flag)
				{
					material.shaderKeywords = null;
					TonemappingMode value = tonemapping.mode.value;
					if (value != TonemappingMode.Neutral)
					{
						if (value == TonemappingMode.ACES)
						{
							material.EnableKeyword(allowColorGradingACESHDR ? "_TONEMAP_ACES" : "_TONEMAP_NEUTRAL");
						}
					}
					else
					{
						material.EnableKeyword("_TONEMAP_NEUTRAL");
					}
					if (passData.cameraData.isHDROutputActive)
					{
						Vector4 hdrOutputLuminanceParams;
						UniversalRenderPipeline.GetHDROutputLuminanceParameters(passData.cameraData.hdrDisplayInformation, passData.cameraData.hdrDisplayColorGamut, tonemapping, out hdrOutputLuminanceParams);
						Vector4 hdrOutputGradingParams;
						UniversalRenderPipeline.GetHDROutputGradingParameters(tonemapping, out hdrOutputGradingParams);
						material.SetVector(ShaderPropertyId.hdrOutputLuminanceParams, hdrOutputLuminanceParams);
						material.SetVector(ShaderPropertyId.hdrOutputGradingParams, hdrOutputGradingParams);
						HDROutputUtils.ConfigureHDROutput(material, passData.cameraData.hdrDisplayColorGamut, HDROutputUtils.Operation.ColorConversion);
					}
				}
				passData.cameraData.xr.StopSinglePass(cmd);
				Blitter.BlitTexture(cmd, internalLutTarget, Vector2.one, material, 0);
				passData.cameraData.xr.StartSinglePass(cmd);
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0003F5C8 File Offset: 0x0003D7C8
		internal void Render(RenderGraph renderGraph, ContextContainer frameData, out TextureHandle internalColorLut)
		{
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalPostProcessingData postProcessingData = frameData.Get<UniversalPostProcessingData>();
			ColorGradingLutPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<ColorGradingLutPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/ColorGradingLutPass.cs", 283))
			{
				RenderTextureDescriptor lutDesc;
				FilterMode filterMode;
				this.ConfigureDescriptor(in postProcessingData, out lutDesc, out filterMode);
				internalColorLut = UniversalRenderer.CreateRenderGraphTexture(renderGraph, lutDesc, "_InternalGradingLut", true, filterMode, TextureWrapMode.Clamp);
				passData.cameraData = cameraData;
				passData.postProcessingData = postProcessingData;
				passData.internalLut = internalColorLut;
				builder.SetRenderAttachment(internalColorLut, 0, AccessFlags.WriteAll);
				passData.lutBuilderLdr = this.m_LutBuilderLdr;
				passData.lutBuilderHdr = this.m_LutBuilderHdr;
				passData.allowColorGradingACESHDR = this.m_AllowColorGradingACESHDR;
				builder.AllowPassCulling(false);
				builder.SetRenderFunc<ColorGradingLutPass.PassData>(delegate(ColorGradingLutPass.PassData data, RasterGraphContext context)
				{
					ColorGradingLutPass.ExecutePass(context.cmd, data, data.internalLut);
				});
			}
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0003F6BC File Offset: 0x0003D8BC
		public void Cleanup()
		{
			CoreUtils.Destroy(this.m_LutBuilderLdr);
			CoreUtils.Destroy(this.m_LutBuilderHdr);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0003F6D4 File Offset: 0x0003D8D4
		[CompilerGenerated]
		internal static Material <.ctor>g__Load|7_0(Shader shader)
		{
			if (shader == null)
			{
				Debug.LogError("Missing shader. ColorGradingLutPass render pass will not execute. Check for missing reference in the renderer resources.");
				return null;
			}
			return CoreUtils.CreateEngineMaterial(shader);
		}

		// Token: 0x04000CED RID: 3309
		private readonly Material m_LutBuilderLdr;

		// Token: 0x04000CEE RID: 3310
		private readonly Material m_LutBuilderHdr;

		// Token: 0x04000CEF RID: 3311
		internal readonly GraphicsFormat m_HdrLutFormat;

		// Token: 0x04000CF0 RID: 3312
		internal readonly GraphicsFormat m_LdrLutFormat;

		// Token: 0x04000CF1 RID: 3313
		private ColorGradingLutPass.PassData m_PassData;

		// Token: 0x04000CF2 RID: 3314
		private RTHandle m_InternalLut;

		// Token: 0x04000CF3 RID: 3315
		private bool m_AllowColorGradingACESHDR = true;

		// Token: 0x02000200 RID: 512
		private class PassData
		{
			// Token: 0x04000CF4 RID: 3316
			internal UniversalCameraData cameraData;

			// Token: 0x04000CF5 RID: 3317
			internal UniversalPostProcessingData postProcessingData;

			// Token: 0x04000CF6 RID: 3318
			internal Material lutBuilderLdr;

			// Token: 0x04000CF7 RID: 3319
			internal Material lutBuilderHdr;

			// Token: 0x04000CF8 RID: 3320
			internal bool allowColorGradingACESHDR;

			// Token: 0x04000CF9 RID: 3321
			internal TextureHandle internalLut;
		}

		// Token: 0x02000201 RID: 513
		private static class ShaderConstants
		{
			// Token: 0x04000CFA RID: 3322
			public static readonly int _Lut_Params = Shader.PropertyToID("_Lut_Params");

			// Token: 0x04000CFB RID: 3323
			public static readonly int _ColorBalance = Shader.PropertyToID("_ColorBalance");

			// Token: 0x04000CFC RID: 3324
			public static readonly int _ColorFilter = Shader.PropertyToID("_ColorFilter");

			// Token: 0x04000CFD RID: 3325
			public static readonly int _ChannelMixerRed = Shader.PropertyToID("_ChannelMixerRed");

			// Token: 0x04000CFE RID: 3326
			public static readonly int _ChannelMixerGreen = Shader.PropertyToID("_ChannelMixerGreen");

			// Token: 0x04000CFF RID: 3327
			public static readonly int _ChannelMixerBlue = Shader.PropertyToID("_ChannelMixerBlue");

			// Token: 0x04000D00 RID: 3328
			public static readonly int _HueSatCon = Shader.PropertyToID("_HueSatCon");

			// Token: 0x04000D01 RID: 3329
			public static readonly int _Lift = Shader.PropertyToID("_Lift");

			// Token: 0x04000D02 RID: 3330
			public static readonly int _Gamma = Shader.PropertyToID("_Gamma");

			// Token: 0x04000D03 RID: 3331
			public static readonly int _Gain = Shader.PropertyToID("_Gain");

			// Token: 0x04000D04 RID: 3332
			public static readonly int _Shadows = Shader.PropertyToID("_Shadows");

			// Token: 0x04000D05 RID: 3333
			public static readonly int _Midtones = Shader.PropertyToID("_Midtones");

			// Token: 0x04000D06 RID: 3334
			public static readonly int _Highlights = Shader.PropertyToID("_Highlights");

			// Token: 0x04000D07 RID: 3335
			public static readonly int _ShaHiLimits = Shader.PropertyToID("_ShaHiLimits");

			// Token: 0x04000D08 RID: 3336
			public static readonly int _SplitShadows = Shader.PropertyToID("_SplitShadows");

			// Token: 0x04000D09 RID: 3337
			public static readonly int _SplitHighlights = Shader.PropertyToID("_SplitHighlights");

			// Token: 0x04000D0A RID: 3338
			public static readonly int _CurveMaster = Shader.PropertyToID("_CurveMaster");

			// Token: 0x04000D0B RID: 3339
			public static readonly int _CurveRed = Shader.PropertyToID("_CurveRed");

			// Token: 0x04000D0C RID: 3340
			public static readonly int _CurveGreen = Shader.PropertyToID("_CurveGreen");

			// Token: 0x04000D0D RID: 3341
			public static readonly int _CurveBlue = Shader.PropertyToID("_CurveBlue");

			// Token: 0x04000D0E RID: 3342
			public static readonly int _CurveHueVsHue = Shader.PropertyToID("_CurveHueVsHue");

			// Token: 0x04000D0F RID: 3343
			public static readonly int _CurveHueVsSat = Shader.PropertyToID("_CurveHueVsSat");

			// Token: 0x04000D10 RID: 3344
			public static readonly int _CurveLumVsSat = Shader.PropertyToID("_CurveLumVsSat");

			// Token: 0x04000D11 RID: 3345
			public static readonly int _CurveSatVsSat = Shader.PropertyToID("_CurveSatVsSat");
		}
	}
}
