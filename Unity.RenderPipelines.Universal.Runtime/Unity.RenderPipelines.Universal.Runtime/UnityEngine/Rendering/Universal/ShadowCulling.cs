using System;
using Unity.Collections;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000192 RID: 402
	internal static class ShadowCulling
	{
		// Token: 0x0600087B RID: 2171 RVA: 0x00028468 File Offset: 0x00026668
		public static NativeArray<URPLightShadowCullingInfos> CullShadowCasters(ref ScriptableRenderContext context, UniversalShadowData shadowData, ref AdditionalLightsShadowAtlasLayout shadowAtlasLayout, ref CullingResults cullResults)
		{
			ShadowCastersCullingInfos shadowCullingInfos;
			NativeArray<URPLightShadowCullingInfos> urpVisibleLightsShadowCullingInfos;
			ShadowCulling.ComputeShadowCasterCullingInfos(shadowData, ref shadowAtlasLayout, ref cullResults, out shadowCullingInfos, out urpVisibleLightsShadowCullingInfos);
			context.CullShadowCasters(cullResults, shadowCullingInfos);
			return urpVisibleLightsShadowCullingInfos;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00028490 File Offset: 0x00026690
		private static void ComputeShadowCasterCullingInfos(UniversalShadowData shadowData, ref AdditionalLightsShadowAtlasLayout shadowAtlasLayout, ref CullingResults cullingResults, out ShadowCastersCullingInfos shadowCullingInfos, out NativeArray<URPLightShadowCullingInfos> urpVisibleLightsShadowCullingInfos)
		{
			using (new ProfilingScope(ShadowCulling.computeShadowCasterCullingInfosMarker))
			{
				NativeArray<VisibleLight> visibleLights = cullingResults.visibleLights;
				NativeArray<ShadowSplitData> splitBuffer = new NativeArray<ShadowSplitData>(visibleLights.Length * 6, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<LightShadowCasterCullingInfo> perLightInfos = new NativeArray<LightShadowCasterCullingInfo>(visibleLights.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
				urpVisibleLightsShadowCullingInfos = new NativeArray<URPLightShadowCullingInfos>(visibleLights.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
				int totalSplitCount = 0;
				int splitBufferOffset = 0;
				int lightIndex = 0;
				while (lightIndex < visibleLights.Length)
				{
					ref VisibleLight visibleLight = ref cullingResults.visibleLights.UnsafeElementAt(lightIndex);
					LightType lightType = visibleLight.lightType;
					NativeArray<ShadowSliceData> slices = default(NativeArray<ShadowSliceData>);
					uint slicesValidMask = 0U;
					if (lightType == LightType.Directional)
					{
						if (shadowData.supportsMainLightShadows)
						{
							int splitCount = shadowData.mainLightShadowCascadesCount;
							int renderTargetWidth = shadowData.mainLightRenderTargetWidth;
							int renderTargetHeight = shadowData.mainLightRenderTargetHeight;
							int shadowResolution = shadowData.mainLightShadowResolution;
							slices = new NativeArray<ShadowSliceData>(splitCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
							slicesValidMask = 0U;
							for (int i = 0; i < splitCount; i++)
							{
								ShadowSliceData slice = default(ShadowSliceData);
								Vector4 vector;
								if (ShadowUtils.ExtractDirectionalLightMatrix(ref cullingResults, shadowData, lightIndex, i, renderTargetWidth, renderTargetHeight, shadowResolution, visibleLight.light.shadowNearPlane, out vector, out slice))
								{
									slicesValidMask |= 1U << i;
								}
								slices[i] = slice;
								splitBuffer[splitBufferOffset + i] = slice.splitData;
							}
							goto IL_026C;
						}
					}
					else if (lightType == LightType.Point)
					{
						if (shadowData.supportsAdditionalLightShadows && shadowAtlasLayout.HasSpaceForLight(lightIndex))
						{
							int splitCount2 = ShadowUtils.GetPunctualLightShadowSlicesCount(in lightType);
							int allocatedResolution = (int)shadowAtlasLayout.GetSliceShadowResolutionRequest(lightIndex, 0).allocatedResolution;
							bool shadowFiltering = visibleLight.light.shadows == LightShadows.Soft;
							float fovBias = AdditionalLightsShadowCasterPass.GetPointLightShadowFrustumFovBiasInDegrees(allocatedResolution, shadowFiltering);
							slices = new NativeArray<ShadowSliceData>(splitCount2, Allocator.Temp, NativeArrayOptions.ClearMemory);
							slicesValidMask = 0U;
							for (int j = 0; j < splitCount2; j++)
							{
								ShadowSliceData slice2 = default(ShadowSliceData);
								if (ShadowUtils.ExtractPointLightMatrix(ref cullingResults, shadowData, lightIndex, (CubemapFace)j, fovBias, out slice2.shadowTransform, out slice2.viewMatrix, out slice2.projectionMatrix, out slice2.splitData))
								{
									slicesValidMask |= 1U << j;
								}
								slices[j] = slice2;
								splitBuffer[splitBufferOffset + j] = slice2.splitData;
							}
							goto IL_026C;
						}
					}
					else
					{
						if (lightType != LightType.Spot)
						{
							goto IL_026C;
						}
						if (shadowData.supportsAdditionalLightShadows && shadowAtlasLayout.HasSpaceForLight(lightIndex))
						{
							slices = new NativeArray<ShadowSliceData>(1, Allocator.Temp, NativeArrayOptions.ClearMemory);
							slicesValidMask = 0U;
							ShadowSliceData slice3 = default(ShadowSliceData);
							if (ShadowUtils.ExtractSpotLightMatrix(ref cullingResults, shadowData, lightIndex, out slice3.shadowTransform, out slice3.viewMatrix, out slice3.projectionMatrix, out slice3.splitData))
							{
								slicesValidMask |= 1U;
							}
							slices[0] = slice3;
							splitBuffer[splitBufferOffset] = slice3.splitData;
							goto IL_026C;
						}
					}
					IL_02DF:
					lightIndex++;
					continue;
					IL_026C:
					urpVisibleLightsShadowCullingInfos[lightIndex] = new URPLightShadowCullingInfos
					{
						slices = slices,
						slicesValidMask = slicesValidMask
					};
					perLightInfos[lightIndex] = new LightShadowCasterCullingInfo
					{
						splitRange = new RangeInt(splitBufferOffset, slices.Length),
						projectionType = ShadowCulling.GetCullingProjectionType(lightType)
					};
					splitBufferOffset += slices.Length;
					totalSplitCount += slices.Length;
					goto IL_02DF;
				}
				shadowCullingInfos = default(ShadowCastersCullingInfos);
				shadowCullingInfos.splitBuffer = splitBuffer.GetSubArray(0, totalSplitCount);
				shadowCullingInfos.perLightInfos = perLightInfos;
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x000287DC File Offset: 0x000269DC
		private static BatchCullingProjectionType GetCullingProjectionType(LightType type)
		{
			switch (type)
			{
			case LightType.Spot:
				return BatchCullingProjectionType.Perspective;
			case LightType.Directional:
				return BatchCullingProjectionType.Orthographic;
			case LightType.Point:
				return BatchCullingProjectionType.Perspective;
			default:
				return BatchCullingProjectionType.Unknown;
			}
		}

		// Token: 0x040008EB RID: 2283
		private static readonly ProfilingSampler computeShadowCasterCullingInfosMarker = new ProfilingSampler("UniversalRenderPipeline.ComputeShadowCasterCullingInfos");
	}
}
