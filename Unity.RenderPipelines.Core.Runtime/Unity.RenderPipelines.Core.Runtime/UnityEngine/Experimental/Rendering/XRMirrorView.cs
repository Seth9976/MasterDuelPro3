using System;
using Unity.Mathematics;
using UnityEngine.Rendering;
using UnityEngine.XR;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200000F RID: 15
	internal static class XRMirrorView
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002F84 File Offset: 0x00001184
		internal static void RenderMirrorView(CommandBuffer cmd, Camera camera, Material mat, XRDisplaySubsystem display)
		{
			if (Application.platform == RuntimePlatform.Android && !XRGraphicsAutomatedTests.running)
			{
				return;
			}
			if (display == null || !display.running || mat == null)
			{
				return;
			}
			int mirrorBlitMode = display.GetPreferredMirrorBlitMode();
			XRDisplaySubsystem.XRMirrorViewBlitDesc blitDesc;
			if (display.GetMirrorViewBlitDesc(null, out blitDesc, mirrorBlitMode))
			{
				using (new ProfilingScope(cmd, XRMirrorView.k_MirrorViewProfilingSampler))
				{
					cmd.SetRenderTarget((camera.targetTexture != null) ? camera.targetTexture : new RenderTargetIdentifier(BuiltinRenderTextureType.CameraTarget));
					if (blitDesc.nativeBlitAvailable)
					{
						display.AddGraphicsThreadMirrorViewBlit(cmd, blitDesc.nativeBlitInvalidStates, mirrorBlitMode);
					}
					else
					{
						for (int i = 0; i < blitDesc.blitParamsCount; i++)
						{
							XRDisplaySubsystem.XRBlitParams blitParam;
							blitDesc.GetBlitParameter(i, out blitParam);
							Vector4 scaleBias = new Vector4(blitParam.srcRect.width, blitParam.srcRect.height, blitParam.srcRect.x, blitParam.srcRect.y);
							Vector4 scaleBiasRt = new Vector4(blitParam.destRect.width, blitParam.destRect.height, blitParam.destRect.x, blitParam.destRect.y);
							if (camera.targetTexture != null || camera.cameraType == CameraType.SceneView || camera.cameraType == CameraType.Preview)
							{
								scaleBias.y = -scaleBias.y;
								scaleBias.w += blitParam.srcRect.height;
							}
							HDROutputSettings mainDisplayHdrSettings = HDROutputSettings.main;
							if (blitParam.srcHdrEncoded || mainDisplayHdrSettings.active)
							{
								ColorGamut mainDisplayColorGamut = (mainDisplayHdrSettings.active ? mainDisplayHdrSettings.displayColorGamut : ColorGamut.sRGB);
								object obj = (blitParam.srcHdrEncoded ? blitParam.srcHdrColorGamut : ColorGamut.sRGB);
								ColorPrimaries mainDisplayColorPrimaries = ColorGamutUtility.GetColorPrimaries(mainDisplayColorGamut);
								object obj2 = obj;
								ColorPrimaries xrDisplayColorPrimaries = ColorGamutUtility.GetColorPrimaries(obj2);
								HDROutputUtils.ConfigureHDROutput(XRMirrorView.s_MirrorViewMaterialProperty, mainDisplayColorGamut);
								HDROutputUtils.ConfigureHDROutput(mat, HDROutputUtils.Operation.ColorConversion | HDROutputUtils.Operation.ColorEncoding);
								int sourceHdrEncoding;
								HDROutputUtils.GetColorEncodingForGamut(obj2, out sourceHdrEncoding);
								XRMirrorView.s_MirrorViewMaterialProperty.SetInteger(XRMirrorView.k_SourceHDREncoding, sourceHdrEncoding);
								float3x3 sourceToRec2020 = float3x3.identity;
								if (xrDisplayColorPrimaries == ColorPrimaries.Rec709)
								{
									sourceToRec2020 = ColorSpaceUtils.Rec709ToRec2020Mat;
								}
								else if (xrDisplayColorPrimaries == ColorPrimaries.P3)
								{
									sourceToRec2020 = ColorSpaceUtils.P3D65ToRec2020Mat;
								}
								float3x3 rec2020ToDest = float3x3.identity;
								if (mainDisplayColorPrimaries == ColorPrimaries.Rec709)
								{
									rec2020ToDest = ColorSpaceUtils.Rec2020ToRec709Mat;
								}
								else if (mainDisplayColorPrimaries == ColorPrimaries.P3)
								{
									rec2020ToDest = ColorSpaceUtils.Rec2020ToP3D65Mat;
								}
								float3x3 colorTransform = math.mul(sourceToRec2020, rec2020ToDest);
								Matrix4x4 j = new Matrix4x4(new float4(colorTransform.c0, 0f), new float4(colorTransform.c1, 0f), new float4(colorTransform.c2, 0f), new Vector4(0f, 0f, 0f, 0f));
								XRMirrorView.s_MirrorViewMaterialProperty.SetMatrix(XRMirrorView.k_ColorTransform, j);
								XRMirrorView.s_MirrorViewMaterialProperty.SetFloat(XRMirrorView.k_MaxNits, mainDisplayHdrSettings.active ? ((float)mainDisplayHdrSettings.maxToneMapLuminance) : 160f);
								XRMirrorView.s_MirrorViewMaterialProperty.SetFloat(XRMirrorView.k_SourceMaxNits, blitParam.srcHdrEncoded ? ((float)blitParam.srcHdrMaxLuminance) : 160f);
							}
							bool manualSRGBRead = !blitParam.srcTex.sRGB && (blitParam.srcTex.graphicsFormat == GraphicsFormat.R8G8B8A8_UNorm || blitParam.srcTex.graphicsFormat == GraphicsFormat.B8G8R8A8_UNorm);
							XRMirrorView.s_MirrorViewMaterialProperty.SetFloat(XRMirrorView.k_SRGBRead, manualSRGBRead ? 1f : 0f);
							XRMirrorView.s_MirrorViewMaterialProperty.SetFloat(XRMirrorView.k_SRGBWrite, (QualitySettings.activeColorSpace == ColorSpace.Linear) ? 0f : 1f);
							XRMirrorView.s_MirrorViewMaterialProperty.SetTexture(XRMirrorView.k_SourceTex, blitParam.srcTex);
							XRMirrorView.s_MirrorViewMaterialProperty.SetVector(XRMirrorView.k_ScaleBias, scaleBias);
							XRMirrorView.s_MirrorViewMaterialProperty.SetVector(XRMirrorView.k_ScaleBiasRt, scaleBiasRt);
							XRMirrorView.s_MirrorViewMaterialProperty.SetFloat(XRMirrorView.k_SourceTexArraySlice, (float)blitParam.srcTexArraySlice);
							if (XRSystem.foveatedRenderingCaps.HasFlag(FoveatedRenderingCaps.NonUniformRaster) && blitParam.foveatedRenderingInfo != IntPtr.Zero)
							{
								cmd.ConfigureFoveatedRendering(blitParam.foveatedRenderingInfo);
								cmd.EnableShaderKeyword("_FOVEATED_RENDERING_NON_UNIFORM_RASTER");
							}
							if (blitParam.srcTex.dimension != TextureDimension.Tex2DArray)
							{
								cmd.EnableShaderKeyword("DISABLE_TEXTURE2D_X_ARRAY");
							}
							cmd.DrawProcedural(Matrix4x4.identity, mat, 0, MeshTopology.Quads, 4, 1, XRMirrorView.s_MirrorViewMaterialProperty);
							if (blitParam.srcTex.dimension != TextureDimension.Tex2DArray && TextureXR.useTexArray)
							{
								cmd.DisableShaderKeyword("DISABLE_TEXTURE2D_X_ARRAY");
							}
						}
					}
				}
			}
			if (XRSystem.foveatedRenderingCaps.HasFlag(FoveatedRenderingCaps.NonUniformRaster))
			{
				cmd.DisableShaderKeyword("_FOVEATED_RENDERING_NON_UNIFORM_RASTER");
				cmd.ConfigureFoveatedRendering(IntPtr.Zero);
			}
		}

		// Token: 0x04000037 RID: 55
		private static readonly MaterialPropertyBlock s_MirrorViewMaterialProperty = new MaterialPropertyBlock();

		// Token: 0x04000038 RID: 56
		private static readonly ProfilingSampler k_MirrorViewProfilingSampler = new ProfilingSampler("XR Mirror View");

		// Token: 0x04000039 RID: 57
		private static readonly int k_SourceTex = Shader.PropertyToID("_SourceTex");

		// Token: 0x0400003A RID: 58
		private static readonly int k_SourceTexArraySlice = Shader.PropertyToID("_SourceTexArraySlice");

		// Token: 0x0400003B RID: 59
		private static readonly int k_ScaleBias = Shader.PropertyToID("_ScaleBias");

		// Token: 0x0400003C RID: 60
		private static readonly int k_ScaleBiasRt = Shader.PropertyToID("_ScaleBiasRt");

		// Token: 0x0400003D RID: 61
		private static readonly int k_SRGBRead = Shader.PropertyToID("_SRGBRead");

		// Token: 0x0400003E RID: 62
		private static readonly int k_SRGBWrite = Shader.PropertyToID("_SRGBWrite");

		// Token: 0x0400003F RID: 63
		private static readonly int k_MaxNits = Shader.PropertyToID("_MaxNits");

		// Token: 0x04000040 RID: 64
		private static readonly int k_SourceMaxNits = Shader.PropertyToID("_SourceMaxNits");

		// Token: 0x04000041 RID: 65
		private static readonly int k_SourceHDREncoding = Shader.PropertyToID("_SourceHDREncoding");

		// Token: 0x04000042 RID: 66
		private static readonly int k_ColorTransform = Shader.PropertyToID("_ColorTransform");
	}
}
