using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x020001F5 RID: 501
	public class AdditionalLightsShadowCasterPass : ScriptableRenderPass
	{
		// Token: 0x06000B4B RID: 2891 RVA: 0x0003BEF4 File Offset: 0x0003A0F4
		public AdditionalLightsShadowCasterPass(RenderPassEvent evt)
		{
			base.profilingSampler = new ProfilingSampler("Draw Additional Lights Shadowmap");
			base.renderPassEvent = evt;
			this.m_PassData = new AdditionalLightsShadowCasterPass.PassData();
			AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalLightsWorldToShadow = Shader.PropertyToID("_AdditionalLightsWorldToShadow");
			AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowParams = Shader.PropertyToID("_AdditionalShadowParams");
			AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowOffset0 = Shader.PropertyToID("_AdditionalShadowOffset0");
			AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowOffset1 = Shader.PropertyToID("_AdditionalShadowOffset1");
			AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowFadeParams = Shader.PropertyToID("_AdditionalShadowFadeParams");
			AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowmapSize = Shader.PropertyToID("_AdditionalShadowmapSize");
			this.m_AdditionalLightsShadowmapID = Shader.PropertyToID("_AdditionalLightsShadowmapTexture");
			AdditionalLightsShadowCasterPass.m_AdditionalLightsWorldToShadow_SSBO = Shader.PropertyToID("_AdditionalLightsWorldToShadow_SSBO");
			AdditionalLightsShadowCasterPass.m_AdditionalShadowParams_SSBO = Shader.PropertyToID("_AdditionalShadowParams_SSBO");
			this.m_UseStructuredBuffer = RenderingUtils.useStructuredBuffer;
			int maxVisibleAdditionalLights = UniversalRenderPipeline.maxVisibleAdditionalLights;
			int maxVisibleLights = maxVisibleAdditionalLights + 1;
			int maxAdditionalLightShadowParams = (this.m_UseStructuredBuffer ? maxVisibleLights : Math.Min(maxVisibleLights, maxVisibleAdditionalLights));
			this.m_AdditionalLightIndexToVisibleLightIndex = new short[maxAdditionalLightShadowParams];
			this.m_VisibleLightIndexToAdditionalLightIndex = new short[maxVisibleLights];
			this.m_VisibleLightIndexToIsCastingShadows = new bool[maxVisibleLights];
			this.m_AdditionalLightIndexToShadowParams = new Vector4[maxAdditionalLightShadowParams];
			AdditionalLightsShadowCasterPass.s_EmptyAdditionalLightIndexToShadowParams = new Vector4[maxAdditionalLightShadowParams];
			for (int i = 0; i < AdditionalLightsShadowCasterPass.s_EmptyAdditionalLightIndexToShadowParams.Length; i++)
			{
				AdditionalLightsShadowCasterPass.s_EmptyAdditionalLightIndexToShadowParams[i] = AdditionalLightsShadowCasterPass.c_DefaultShadowParams;
			}
			if (!this.m_UseStructuredBuffer)
			{
				this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix = new Matrix4x4[maxVisibleAdditionalLights];
			}
			this.m_EmptyShadowmapNeedsClear = true;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0003C082 File Offset: 0x0003A282
		public void Dispose()
		{
			RTHandle additionalLightsShadowmapHandle = this.m_AdditionalLightsShadowmapHandle;
			if (additionalLightsShadowmapHandle != null)
			{
				additionalLightsShadowmapHandle.Release();
			}
			RTHandle emptyAdditionalLightShadowmapTexture = this.m_EmptyAdditionalLightShadowmapTexture;
			if (emptyAdditionalLightShadowmapTexture == null)
			{
				return;
			}
			emptyAdditionalLightShadowmapTexture.Release();
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0003C0A8 File Offset: 0x0003A2A8
		internal static float CalcGuardAngle(float frustumAngleInDegrees, float guardBandSizeInTexels, float sliceResolutionInTexels)
		{
			float halfFrustumAngle = frustumAngleInDegrees * 0.017453292f / 2f;
			float num = Mathf.Tan(halfFrustumAngle);
			float halfSliceResolution = sliceResolutionInTexels / 2f;
			float halfGuardBand = guardBandSizeInTexels / 2f;
			float factorBetweenAngleTangents = 1f + halfGuardBand / halfSliceResolution;
			float halfGuardAngleInRadian = Mathf.Atan(num * factorBetweenAngleTangents) - halfFrustumAngle;
			return 2f * halfGuardAngleInRadian * 57.29578f;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0003C0FC File Offset: 0x0003A2FC
		internal static float GetPointLightShadowFrustumFovBiasInDegrees(int shadowSliceResolution, bool shadowFiltering)
		{
			float fovBias = 4f;
			if (shadowSliceResolution <= 8)
			{
				if (!AdditionalLightsShadowCasterPass.m_IssuedMessageAboutPointLightHardShadowResolutionTooSmall)
				{
					Debug.LogWarning("Too many additional punctual lights shadows, increase shadow atlas size or remove some shadowed lights");
					AdditionalLightsShadowCasterPass.m_IssuedMessageAboutPointLightHardShadowResolutionTooSmall = true;
				}
			}
			else if (shadowSliceResolution <= 16)
			{
				fovBias = 43f;
			}
			else if (shadowSliceResolution <= 32)
			{
				fovBias = 18.55f;
			}
			else if (shadowSliceResolution <= 64)
			{
				fovBias = 8.63f;
			}
			else if (shadowSliceResolution <= 128)
			{
				fovBias = 4.13f;
			}
			else if (shadowSliceResolution <= 256)
			{
				fovBias = 2.03f;
			}
			else if (shadowSliceResolution <= 512)
			{
				fovBias = 1f;
			}
			else if (shadowSliceResolution <= 1024)
			{
				fovBias = 0.5f;
			}
			else if (shadowSliceResolution <= 2048)
			{
				fovBias = 0.25f;
			}
			if (shadowFiltering)
			{
				if (shadowSliceResolution <= 16)
				{
					if (!AdditionalLightsShadowCasterPass.m_IssuedMessageAboutPointLightSoftShadowResolutionTooSmall)
					{
						Debug.LogWarning("Too many additional punctual lights shadows to use Soft Shadows. Increase shadow atlas size, remove some shadowed lights or use Hard Shadows.");
						AdditionalLightsShadowCasterPass.m_IssuedMessageAboutPointLightSoftShadowResolutionTooSmall = true;
					}
				}
				else if (shadowSliceResolution <= 32)
				{
					fovBias += 9.35f;
				}
				else if (shadowSliceResolution <= 64)
				{
					fovBias += 4.07f;
				}
				else if (shadowSliceResolution <= 128)
				{
					fovBias += 1.77f;
				}
				else if (shadowSliceResolution <= 256)
				{
					fovBias += 0.85f;
				}
				else if (shadowSliceResolution <= 512)
				{
					fovBias += 0.39f;
				}
				else if (shadowSliceResolution <= 1024)
				{
					fovBias += 0.17f;
				}
				else if (shadowSliceResolution <= 2048)
				{
					fovBias += 0.074f;
				}
			}
			return fovBias;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0003C242 File Offset: 0x0003A442
		private ulong ResolutionLog2ForHash(int resolution)
		{
			if (resolution <= 1024)
			{
				if (resolution == 512)
				{
					return 9UL;
				}
				if (resolution == 1024)
				{
					return 10UL;
				}
			}
			else
			{
				if (resolution == 2048)
				{
					return 11UL;
				}
				if (resolution == 4096)
				{
					return 12UL;
				}
			}
			return 8UL;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0003C280 File Offset: 0x0003A480
		private ulong ComputeShadowRequestHash(UniversalLightData lightData, UniversalShadowData shadowData)
		{
			ulong numberOfShadowedPointLights = 0UL;
			ulong numberOfSoftShadowedLights = 0UL;
			ulong numberOfShadowsWithResolution128 = 0UL;
			ulong numberOfShadowsWithResolution129 = 0UL;
			ulong numberOfShadowsWithResolution130 = 0UL;
			ulong numberOfShadowsWithResolution131 = 0UL;
			ulong numberOfShadowsWithResolution132 = 0UL;
			ulong numberOfShadowsWithResolution133 = 0UL;
			NativeArray<VisibleLight> visibleLights = lightData.visibleLights;
			for (int visibleLightIndex = 0; visibleLightIndex < visibleLights.Length; visibleLightIndex++)
			{
				ref VisibleLight vl = ref visibleLights.UnsafeElementAt(visibleLightIndex);
				Light light = vl.light;
				if (ShadowUtils.IsValidShadowCastingLight(lightData, visibleLightIndex, vl.lightType, light.shadows, light.shadowStrength))
				{
					LightType lightType = vl.lightType;
					if (lightType != LightType.Spot)
					{
						if (lightType == LightType.Point)
						{
							numberOfShadowedPointLights += 1UL;
						}
					}
					else
					{
						numberOfSoftShadowedLights += 1UL;
					}
					int resolution = shadowData.resolution[visibleLightIndex];
					if (resolution <= 512)
					{
						if (resolution != 128)
						{
							if (resolution != 256)
							{
								if (resolution == 512)
								{
									numberOfShadowsWithResolution130 += 1UL;
								}
							}
							else
							{
								numberOfShadowsWithResolution129 += 1UL;
							}
						}
						else
						{
							numberOfShadowsWithResolution128 += 1UL;
						}
					}
					else if (resolution != 1024)
					{
						if (resolution != 2048)
						{
							if (resolution == 4096)
							{
								numberOfShadowsWithResolution133 += 1UL;
							}
						}
						else
						{
							numberOfShadowsWithResolution132 += 1UL;
						}
					}
					else
					{
						numberOfShadowsWithResolution131 += 1UL;
					}
				}
			}
			return (this.ResolutionLog2ForHash(shadowData.additionalLightsShadowmapWidth) - 8UL) | (numberOfShadowedPointLights << 3) | (numberOfSoftShadowedLights << 11) | (numberOfShadowsWithResolution128 << 19) | (numberOfShadowsWithResolution129 << 27) | (numberOfShadowsWithResolution130 << 35) | (numberOfShadowsWithResolution131 << 43) | (numberOfShadowsWithResolution132 << 50) | (numberOfShadowsWithResolution133 << 57);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0003C3E0 File Offset: 0x0003A5E0
		public bool Setup(ref RenderingData renderingData)
		{
			ContextContainer frameData = renderingData.frameData;
			UniversalRenderingData universalRenderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			UniversalShadowData shadowData = frameData.Get<UniversalShadowData>();
			return this.Setup(universalRenderingData, cameraData, lightData, shadowData);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0003C418 File Offset: 0x0003A618
		public bool Setup(UniversalRenderingData renderingData, UniversalCameraData cameraData, UniversalLightData lightData, UniversalShadowData shadowData)
		{
			bool flag;
			using (new ProfilingScope(this.m_ProfilingSetupSampler))
			{
				if (!shadowData.additionalLightShadowsEnabled)
				{
					flag = false;
				}
				else if (!shadowData.supportsAdditionalLightShadows)
				{
					flag = this.SetupForEmptyRendering(cameraData.renderer.stripShadowsOffVariants, shadowData);
				}
				else
				{
					this.Clear();
					this.renderTargetWidth = shadowData.additionalLightsShadowmapWidth;
					this.renderTargetHeight = shadowData.additionalLightsShadowmapHeight;
					NativeArray<VisibleLight> visibleLights = lightData.visibleLights;
					ref AdditionalLightsShadowAtlasLayout atlasLayout = ref shadowData.shadowAtlasLayout;
					if (this.m_VisibleLightIndexToAdditionalLightIndex.Length < visibleLights.Length)
					{
						this.m_VisibleLightIndexToAdditionalLightIndex = new short[visibleLights.Length];
						this.m_VisibleLightIndexToIsCastingShadows = new bool[visibleLights.Length];
					}
					int maxAdditionalLightShadowParams = (this.m_UseStructuredBuffer ? visibleLights.Length : Math.Min(visibleLights.Length, UniversalRenderPipeline.maxVisibleAdditionalLights));
					if (this.m_AdditionalLightIndexToVisibleLightIndex.Length < maxAdditionalLightShadowParams)
					{
						this.m_AdditionalLightIndexToVisibleLightIndex = new short[maxAdditionalLightShadowParams];
						this.m_AdditionalLightIndexToShadowParams = new Vector4[maxAdditionalLightShadowParams];
					}
					int totalShadowSlicesCount = atlasLayout.GetTotalShadowSlicesCount();
					int totalShadowResolutionRequestCount = atlasLayout.GetTotalShadowResolutionRequestCount();
					int shadowSlicesScaleFactor = atlasLayout.GetShadowSlicesScaleFactor();
					bool hasTooManyShadowMaps = atlasLayout.HasTooManyShadowMaps();
					int atlasSize = atlasLayout.GetAtlasSize();
					if (totalShadowSlicesCount < totalShadowResolutionRequestCount && !this.m_IssuedMessageAboutRemovedShadowSlices)
					{
						Debug.LogWarning(string.Format("Too many additional punctual lights shadows to look good, URP removed {0} shadow maps to make the others fit in the shadow atlas. To avoid this, increase shadow atlas size, remove some shadowed lights, replace soft shadows by hard shadows ; or replace point lights by spot lights", totalShadowResolutionRequestCount - totalShadowSlicesCount));
						this.m_IssuedMessageAboutRemovedShadowSlices = true;
					}
					if (!this.m_IssuedMessageAboutShadowMapsTooBig && hasTooManyShadowMaps)
					{
						Debug.LogWarning(string.Format("Too many additional punctual lights shadows. URP tried reducing shadow resolutions by {0} but it was still too much. Increase shadow atlas size, decrease big shadow resolutions, or reduce the number of shadow maps active in the same frame (currently was {1}).", shadowSlicesScaleFactor, totalShadowSlicesCount));
						this.m_IssuedMessageAboutShadowMapsTooBig = true;
					}
					if (!this.m_IssuedMessageAboutShadowMapsRescale && shadowSlicesScaleFactor > 1)
					{
						Debug.Log(string.Format("Reduced additional punctual light shadows resolution by {0} to make {1} shadow maps fit in the {2}x{3} shadow atlas. To avoid this, increase shadow atlas size, decrease big shadow resolutions, or reduce the number of shadow maps active in the same frame", new object[] { shadowSlicesScaleFactor, totalShadowSlicesCount, atlasSize, atlasSize }));
						this.m_IssuedMessageAboutShadowMapsRescale = true;
					}
					if (this.m_AdditionalLightsShadowSlices == null || this.m_AdditionalLightsShadowSlices.Length < totalShadowSlicesCount)
					{
						this.m_AdditionalLightsShadowSlices = new ShadowSliceData[totalShadowSlicesCount];
					}
					if (this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix == null || (this.m_UseStructuredBuffer && this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix.Length < totalShadowSlicesCount))
					{
						this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix = new Matrix4x4[totalShadowSlicesCount];
					}
					for (int i = 0; i < maxAdditionalLightShadowParams; i++)
					{
						this.m_AdditionalLightIndexToShadowParams[i] = AdditionalLightsShadowCasterPass.c_DefaultShadowParams;
					}
					for (int j = 0; j < this.m_VisibleLightIndexToAdditionalLightIndex.Length; j++)
					{
						this.m_VisibleLightIndexToAdditionalLightIndex[j] = -1;
						this.m_VisibleLightIndexToIsCastingShadows[j] = false;
					}
					short additionalLightCount = 0;
					short validShadowCastingLightsCount = 0;
					bool supportsSoftShadows = shadowData.supportsSoftShadows;
					bool isDeferred = ((UniversalRenderer)cameraData.renderer).renderingModeActual == RenderingMode.Deferred;
					for (int visibleLightIndex = 0; visibleLightIndex < visibleLights.Length; visibleLightIndex++)
					{
						if (visibleLightIndex != lightData.mainLightIndex)
						{
							short num;
							if (!isDeferred)
							{
								additionalLightCount = (num = additionalLightCount) + 1;
							}
							else
							{
								num = validShadowCastingLightsCount;
							}
							short lightIndexToUse = num;
							this.m_VisibleLightIndexToAdditionalLightIndex[visibleLightIndex] = lightIndexToUse;
							if ((int)lightIndexToUse < this.m_AdditionalLightIndexToVisibleLightIndex.Length)
							{
								this.m_AdditionalLightIndexToVisibleLightIndex[(int)lightIndexToUse] = (short)visibleLightIndex;
								if (this.m_ShadowSliceToAdditionalLightIndex.Count < totalShadowSlicesCount)
								{
									ref VisibleLight visibleLight = ref visibleLights.UnsafeElementAt(visibleLightIndex);
									Light light = visibleLight.light;
									if (light == null)
									{
										break;
									}
									LightType lightType = visibleLight.lightType;
									int perLightShadowSlicesCount = ShadowUtils.GetPunctualLightShadowSlicesCount(in lightType);
									bool isValidShadowCastingLight = ShadowUtils.IsValidShadowCastingLight(lightData, visibleLightIndex, visibleLight.lightType, light.shadows, light.shadowStrength);
									if (isValidShadowCastingLight && this.m_ShadowSliceToAdditionalLightIndex.Count + perLightShadowSlicesCount > totalShadowSlicesCount)
									{
										if (!this.m_IssuedMessageAboutShadowSlicesTooMany)
										{
											Debug.Log("There are too many shadowed additional punctual lights active at the same time, URP will not render all the shadows. To ensure all shadows are rendered, reduce the number of shadowed additional lights in the scene ; make sure they are not active at the same time ; or replace point lights by spot lights (spot lights use less shadow maps than point lights).");
											this.m_IssuedMessageAboutShadowSlicesTooMany = true;
											break;
										}
										break;
									}
									else
									{
										float softShadows = ShadowUtils.SoftShadowQualityToShaderProperty(light, supportsSoftShadows && light.shadows == LightShadows.Soft);
										int perLightFirstShadowSliceIndex = this.m_ShadowSliceToAdditionalLightIndex.Count;
										bool shouldAddLight = false;
										byte perLightShadowSlice = 0;
										while ((int)perLightShadowSlice < perLightShadowSlicesCount)
										{
											int globalShadowSliceIndex = this.m_ShadowSliceToAdditionalLightIndex.Count;
											Bounds bounds;
											bool lightRangeContainsShadowCasters = renderingData.cullResults.GetShadowCasterBounds(visibleLightIndex, out bounds);
											if (shadowData.supportsAdditionalLightShadows && isValidShadowCastingLight && lightRangeContainsShadowCasters && atlasLayout.HasSpaceForLight(visibleLightIndex))
											{
												if (lightType == LightType.Spot)
												{
													ref URPLightShadowCullingInfos ptr = ref shadowData.visibleLightsShadowCullingInfos.UnsafeElementAt(visibleLightIndex);
													ref ShadowSliceData sliceData = ref ptr.slices.UnsafeElementAt(0);
													this.m_AdditionalLightsShadowSlices[globalShadowSliceIndex].viewMatrix = sliceData.viewMatrix;
													this.m_AdditionalLightsShadowSlices[globalShadowSliceIndex].projectionMatrix = sliceData.projectionMatrix;
													this.m_AdditionalLightsShadowSlices[globalShadowSliceIndex].splitData = sliceData.splitData;
													if (ptr.IsSliceValid(0))
													{
														this.m_ShadowSliceToAdditionalLightIndex.Add(lightIndexToUse);
														this.m_GlobalShadowSliceIndexToPerLightShadowSliceIndex.Add(perLightShadowSlice);
														this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix[globalShadowSliceIndex] = sliceData.shadowTransform;
														this.m_AdditionalLightIndexToShadowParams[(int)lightIndexToUse] = new Vector4(light.shadowStrength, softShadows, 0f, (float)perLightFirstShadowSliceIndex);
														shouldAddLight = true;
													}
												}
												else if (lightType == LightType.Point)
												{
													ref URPLightShadowCullingInfos ptr2 = ref shadowData.visibleLightsShadowCullingInfos.UnsafeElementAt(visibleLightIndex);
													ref ShadowSliceData sliceData2 = ref ptr2.slices.UnsafeElementAt((int)perLightShadowSlice);
													this.m_AdditionalLightsShadowSlices[globalShadowSliceIndex].viewMatrix = sliceData2.viewMatrix;
													this.m_AdditionalLightsShadowSlices[globalShadowSliceIndex].projectionMatrix = sliceData2.projectionMatrix;
													this.m_AdditionalLightsShadowSlices[globalShadowSliceIndex].splitData = sliceData2.splitData;
													if (ptr2.IsSliceValid((int)perLightShadowSlice))
													{
														this.m_ShadowSliceToAdditionalLightIndex.Add(lightIndexToUse);
														this.m_GlobalShadowSliceIndexToPerLightShadowSliceIndex.Add(perLightShadowSlice);
														this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix[globalShadowSliceIndex] = sliceData2.shadowTransform;
														this.m_AdditionalLightIndexToShadowParams[(int)lightIndexToUse] = new Vector4(light.shadowStrength, softShadows, 1f, (float)perLightFirstShadowSliceIndex);
														shouldAddLight = true;
													}
												}
											}
											perLightShadowSlice += 1;
										}
										if (shouldAddLight)
										{
											this.m_VisibleLightIndexToIsCastingShadows[visibleLightIndex] = true;
											this.m_VisibleLightIndexToAdditionalLightIndex[visibleLightIndex] = lightIndexToUse;
											this.m_AdditionalLightIndexToVisibleLightIndex[(int)lightIndexToUse] = (short)visibleLightIndex;
											validShadowCastingLightsCount += 1;
										}
									}
								}
							}
						}
					}
					if (validShadowCastingLightsCount == 0)
					{
						flag = this.SetupForEmptyRendering(cameraData.renderer.stripShadowsOffVariants, shadowData);
					}
					else
					{
						int shadowCastingLightsBufferCount = this.m_ShadowSliceToAdditionalLightIndex.Count;
						int atlasMaxX = 0;
						int atlasMaxY = 0;
						for (int sortedShadowResolutionRequestIndex = 0; sortedShadowResolutionRequestIndex < totalShadowSlicesCount; sortedShadowResolutionRequestIndex++)
						{
							AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest shadowResolutionRequest = atlasLayout.GetSortedShadowResolutionRequest(sortedShadowResolutionRequestIndex);
							atlasMaxX = Mathf.Max(atlasMaxX, (int)(shadowResolutionRequest.offsetX + shadowResolutionRequest.allocatedResolution));
							atlasMaxY = Mathf.Max(atlasMaxY, (int)(shadowResolutionRequest.offsetY + shadowResolutionRequest.allocatedResolution));
						}
						this.renderTargetWidth = Mathf.NextPowerOfTwo(atlasMaxX);
						this.renderTargetHeight = Mathf.NextPowerOfTwo(atlasMaxY);
						float oneOverAtlasWidth = 1f / (float)this.renderTargetWidth;
						float oneOverAtlasHeight = 1f / (float)this.renderTargetHeight;
						for (int globalShadowSliceIndex2 = 0; globalShadowSliceIndex2 < shadowCastingLightsBufferCount; globalShadowSliceIndex2++)
						{
							int additionalLightIndex = (int)this.m_ShadowSliceToAdditionalLightIndex[globalShadowSliceIndex2];
							if (!Mathf.Approximately(this.m_AdditionalLightIndexToShadowParams[additionalLightIndex].x, 0f) && !Mathf.Approximately(this.m_AdditionalLightIndexToShadowParams[additionalLightIndex].w, -1f))
							{
								int visibleLightIndex2 = (int)this.m_AdditionalLightIndexToVisibleLightIndex[additionalLightIndex];
								int perLightSliceIndex = (int)this.m_GlobalShadowSliceIndexToPerLightShadowSliceIndex[globalShadowSliceIndex2];
								AdditionalLightsShadowAtlasLayout.ShadowResolutionRequest shadowResolutionRequest2 = atlasLayout.GetSliceShadowResolutionRequest(visibleLightIndex2, perLightSliceIndex);
								int sliceResolution = (int)shadowResolutionRequest2.allocatedResolution;
								Matrix4x4 sliceTransform = Matrix4x4.identity;
								sliceTransform.m00 = (float)sliceResolution * oneOverAtlasWidth;
								sliceTransform.m11 = (float)sliceResolution * oneOverAtlasHeight;
								this.m_AdditionalLightsShadowSlices[globalShadowSliceIndex2].offsetX = (int)shadowResolutionRequest2.offsetX;
								this.m_AdditionalLightsShadowSlices[globalShadowSliceIndex2].offsetY = (int)shadowResolutionRequest2.offsetY;
								this.m_AdditionalLightsShadowSlices[globalShadowSliceIndex2].resolution = sliceResolution;
								sliceTransform.m03 = (float)this.m_AdditionalLightsShadowSlices[globalShadowSliceIndex2].offsetX * oneOverAtlasWidth;
								sliceTransform.m13 = (float)this.m_AdditionalLightsShadowSlices[globalShadowSliceIndex2].offsetY * oneOverAtlasHeight;
								this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix[globalShadowSliceIndex2] = sliceTransform * this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix[globalShadowSliceIndex2];
							}
						}
						this.UpdateTextureDescriptorIfNeeded();
						this.m_MaxShadowDistanceSq = cameraData.maxShadowDistance * cameraData.maxShadowDistance;
						this.m_CascadeBorder = shadowData.mainLightShadowCascadeBorder;
						this.m_CreateEmptyShadowmap = false;
						base.useNativeRenderPass = true;
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0003CC64 File Offset: 0x0003AE64
		private void UpdateTextureDescriptorIfNeeded()
		{
			if (this.m_AdditionalLightShadowDescriptor.width != this.renderTargetWidth || this.m_AdditionalLightShadowDescriptor.height != this.renderTargetHeight || this.m_AdditionalLightShadowDescriptor.depthBufferBits != 16 || this.m_AdditionalLightShadowDescriptor.colorFormat != RenderTextureFormat.Shadowmap)
			{
				this.m_AdditionalLightShadowDescriptor = new RenderTextureDescriptor(this.renderTargetWidth, this.renderTargetHeight, RenderTextureFormat.Shadowmap, 16);
			}
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0003CCD0 File Offset: 0x0003AED0
		private bool SetupForEmptyRendering(bool stripShadowsOffVariants, UniversalShadowData shadowData)
		{
			if (!stripShadowsOffVariants)
			{
				return false;
			}
			shadowData.isKeywordAdditionalLightShadowsEnabled = true;
			this.m_CreateEmptyShadowmap = true;
			base.useNativeRenderPass = false;
			for (int i = 0; i < this.m_AdditionalLightIndexToShadowParams.Length; i++)
			{
				this.m_AdditionalLightIndexToShadowParams[i] = AdditionalLightsShadowCasterPass.c_DefaultShadowParams;
			}
			return true;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0003CD1C File Offset: 0x0003AF1C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			if (this.m_CreateEmptyShadowmap)
			{
				if (ShadowUtils.ShadowRTReAllocateIfNeeded(ref this.m_EmptyAdditionalLightShadowmapTexture, 1, 1, 16, 1, 0f, "_EmptyAdditionalLightShadowmapTexture"))
				{
					this.m_EmptyShadowmapNeedsClear = true;
				}
				if (!this.m_EmptyShadowmapNeedsClear)
				{
					if (Application.platform == RuntimePlatform.Android && PlatformAutoDetect.isRunningOnPowerVRGPU)
					{
						base.ResetTarget();
					}
					return;
				}
				base.ConfigureTarget(this.m_EmptyAdditionalLightShadowmapTexture);
				this.m_EmptyShadowmapNeedsClear = false;
			}
			else
			{
				ShadowUtils.ShadowRTReAllocateIfNeeded(ref this.m_AdditionalLightsShadowmapHandle, this.renderTargetWidth, this.renderTargetHeight, 16, 1, 0f, "_AdditionalLightsShadowmapTexture");
				base.ConfigureTarget(this.m_AdditionalLightsShadowmapHandle);
			}
			base.ConfigureClear(ClearFlag.All, Color.black);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0003CDC8 File Offset: 0x0003AFC8
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			ContextContainer frameData = renderingData.frameData;
			UniversalRenderingData universalRenderingData = frameData.Get<UniversalRenderingData>();
			if (this.m_CreateEmptyShadowmap)
			{
				this.SetEmptyAdditionalShadowmapAtlas(CommandBufferHelpers.GetRasterCommandBuffer(*renderingData.commandBuffer));
				universalRenderingData.commandBuffer.SetGlobalTexture(this.m_AdditionalLightsShadowmapID, this.m_EmptyAdditionalLightShadowmapTexture);
				return;
			}
			UniversalShadowData shadowData = frameData.Get<UniversalShadowData>();
			if (!shadowData.supportsAdditionalLightShadows)
			{
				return;
			}
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			this.InitPassData(ref this.m_PassData, cameraData, lightData, shadowData);
			this.m_PassData.allocatedShadowAtlasSize = this.m_AdditionalLightsShadowmapHandle.referenceSize;
			this.InitRendererLists(ref universalRenderingData.cullResults, ref this.m_PassData, context, null, false);
			this.RenderAdditionalShadowmapAtlas(CommandBufferHelpers.GetRasterCommandBuffer(universalRenderingData.commandBuffer), ref this.m_PassData, false);
			universalRenderingData.commandBuffer.SetGlobalTexture(this.m_AdditionalLightsShadowmapID, this.m_AdditionalLightsShadowmapHandle.nameID);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0003CEA8 File Offset: 0x0003B0A8
		public int GetShadowLightIndexFromLightIndex(int visibleLightIndex)
		{
			if (visibleLightIndex < 0 || visibleLightIndex >= this.m_VisibleLightIndexToAdditionalLightIndex.Length || !this.m_VisibleLightIndexToIsCastingShadows[visibleLightIndex])
			{
				return -1;
			}
			return (int)this.m_VisibleLightIndexToAdditionalLightIndex[visibleLightIndex];
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0003CECD File Offset: 0x0003B0CD
		private void Clear()
		{
			this.m_ShadowSliceToAdditionalLightIndex.Clear();
			this.m_GlobalShadowSliceIndexToPerLightShadowSliceIndex.Clear();
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0003CEE5 File Offset: 0x0003B0E5
		private void SetEmptyAdditionalShadowmapAtlas(RasterCommandBuffer cmd)
		{
			cmd.EnableKeyword(in ShaderGlobalKeywords.AdditionalLightShadows);
			AdditionalLightsShadowCasterPass.SetEmptyAdditionalLightShadowParams(cmd, this.m_AdditionalLightIndexToShadowParams);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0003CF00 File Offset: 0x0003B100
		internal static void SetEmptyAdditionalLightShadowParams(RasterCommandBuffer cmd, Vector4[] lightIndexToShadowParams)
		{
			if (RenderingUtils.useStructuredBuffer)
			{
				ComputeBuffer shadowParamsBuffer = ShaderData.instance.GetAdditionalLightShadowParamsStructuredBuffer(lightIndexToShadowParams.Length);
				shadowParamsBuffer.SetData(lightIndexToShadowParams);
				cmd.SetGlobalBuffer(AdditionalLightsShadowCasterPass.m_AdditionalShadowParams_SSBO, shadowParamsBuffer);
				return;
			}
			cmd.SetGlobalVectorArray(AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowParams, lightIndexToShadowParams);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0003CF44 File Offset: 0x0003B144
		private void RenderAdditionalShadowmapAtlas(RasterCommandBuffer cmd, ref AdditionalLightsShadowCasterPass.PassData data, bool useRenderGraph)
		{
			NativeArray<VisibleLight> visibleLights = data.lightData.visibleLights;
			bool additionalLightHasSoftShadows = false;
			using (new ProfilingScope(cmd, ProfilingSampler.Get<URPProfileId>(URPProfileId.AdditionalLightsShadow)))
			{
				if (!useRenderGraph)
				{
					ShadowUtils.SetWorldToCameraMatrix(cmd, data.viewMatrix);
				}
				bool anyShadowSliceRenderer = false;
				int shadowSlicesCount = this.m_ShadowSliceToAdditionalLightIndex.Count;
				if (shadowSlicesCount > 0)
				{
					cmd.SetKeyword(in ShaderGlobalKeywords.CastingPunctualLightShadow, true);
				}
				Vector4 lastShadowBias = new Vector4(-10f, -10f, -10f, -10f);
				for (int globalShadowSliceIndex = 0; globalShadowSliceIndex < shadowSlicesCount; globalShadowSliceIndex++)
				{
					int additionalLightIndex = (int)this.m_ShadowSliceToAdditionalLightIndex[globalShadowSliceIndex];
					if (!ShadowUtils.FastApproximately(this.m_AdditionalLightIndexToShadowParams[additionalLightIndex].x, 0f) && !ShadowUtils.FastApproximately(this.m_AdditionalLightIndexToShadowParams[additionalLightIndex].w, -1f))
					{
						int visibleLightIndex = (int)this.m_AdditionalLightIndexToVisibleLightIndex[additionalLightIndex];
						ref VisibleLight shadowLight = ref visibleLights.UnsafeElementAt(visibleLightIndex);
						ShadowSliceData shadowSliceData = this.m_AdditionalLightsShadowSlices[globalShadowSliceIndex];
						Vector4 shadowBias = ShadowUtils.GetShadowBias(ref shadowLight, visibleLightIndex, data.shadowData, shadowSliceData.projectionMatrix, (float)shadowSliceData.resolution);
						if (globalShadowSliceIndex == 0 || !ShadowUtils.FastApproximately(shadowBias, lastShadowBias))
						{
							ShadowUtils.SetShadowBias(cmd, shadowBias);
							lastShadowBias = shadowBias;
						}
						Vector3 lightPosition = shadowLight.localToWorldMatrix.GetColumn(3);
						ShadowUtils.SetLightPosition(cmd, lightPosition);
						RendererList shadowRendererList = (useRenderGraph ? data.shadowRendererListsHdl[globalShadowSliceIndex] : data.shadowRendererLists[globalShadowSliceIndex]);
						ShadowUtils.RenderShadowSlice(cmd, ref shadowSliceData, ref shadowRendererList, shadowSliceData.projectionMatrix, shadowSliceData.viewMatrix);
						additionalLightHasSoftShadows |= shadowLight.light.shadows == LightShadows.Soft;
						anyShadowSliceRenderer = true;
					}
				}
				bool mainLightHasSoftShadows = data.shadowData.supportsMainLightShadows && data.lightData.mainLightIndex != -1 && visibleLights[data.lightData.mainLightIndex].light.shadows == LightShadows.Soft;
				bool hasOffVariant = !data.stripShadowsOffVariants;
				data.shadowData.isKeywordAdditionalLightShadowsEnabled = !hasOffVariant || anyShadowSliceRenderer;
				cmd.SetKeyword(in ShaderGlobalKeywords.AdditionalLightShadows, data.shadowData.isKeywordAdditionalLightShadowsEnabled);
				bool softShadows = data.shadowData.supportsSoftShadows && (mainLightHasSoftShadows || additionalLightHasSoftShadows);
				data.shadowData.isKeywordSoftShadowsEnabled = softShadows;
				ShadowUtils.SetSoftShadowQualityShaderKeywords(cmd, data.shadowData);
				if (anyShadowSliceRenderer)
				{
					this.SetupAdditionalLightsShadowReceiverConstants(cmd, data.allocatedShadowAtlasSize, data.useStructuredBuffer, softShadows);
				}
			}
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0003D1EC File Offset: 0x0003B3EC
		private void SetupAdditionalLightsShadowReceiverConstants(RasterCommandBuffer cmd, Vector2Int allocatedShadowAtlasSize, bool useStructuredBuffer, bool softShadows)
		{
			if (useStructuredBuffer)
			{
				ComputeBuffer shadowParamsBuffer = ShaderData.instance.GetAdditionalLightShadowParamsStructuredBuffer(this.m_AdditionalLightIndexToShadowParams.Length);
				shadowParamsBuffer.SetData(this.m_AdditionalLightIndexToShadowParams);
				cmd.SetGlobalBuffer(AdditionalLightsShadowCasterPass.m_AdditionalShadowParams_SSBO, shadowParamsBuffer);
				ComputeBuffer shadowSliceMatricesBuffer = ShaderData.instance.GetAdditionalLightShadowSliceMatricesStructuredBuffer(this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix.Length);
				shadowSliceMatricesBuffer.SetData(this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix);
				cmd.SetGlobalBuffer(AdditionalLightsShadowCasterPass.m_AdditionalLightsWorldToShadow_SSBO, shadowSliceMatricesBuffer);
			}
			else
			{
				cmd.SetGlobalVectorArray(AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowParams, this.m_AdditionalLightIndexToShadowParams);
				cmd.SetGlobalMatrixArray(AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalLightsWorldToShadow, this.m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix);
			}
			float shadowFadeScale;
			float shadowFadeBias;
			ShadowUtils.GetScaleAndBiasForLinearDistanceFade(this.m_MaxShadowDistanceSq, this.m_CascadeBorder, out shadowFadeScale, out shadowFadeBias);
			cmd.SetGlobalVector(AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowFadeParams, new Vector4(shadowFadeScale, shadowFadeBias, 0f, 0f));
			if (softShadows)
			{
				Vector2 invShadowAtlasSize = Vector2.one / allocatedShadowAtlasSize;
				Vector2 invHalfShadowAtlasSize = invShadowAtlasSize * 0.5f;
				cmd.SetGlobalVector(AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowOffset0, new Vector4(-invHalfShadowAtlasSize.x, -invHalfShadowAtlasSize.y, invHalfShadowAtlasSize.x, -invHalfShadowAtlasSize.y));
				cmd.SetGlobalVector(AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowOffset1, new Vector4(-invHalfShadowAtlasSize.x, invHalfShadowAtlasSize.y, invHalfShadowAtlasSize.x, invHalfShadowAtlasSize.y));
				cmd.SetGlobalVector(AdditionalLightsShadowCasterPass.AdditionalShadowsConstantBuffer._AdditionalShadowmapSize, new Vector4(invShadowAtlasSize.x, invShadowAtlasSize.y, (float)allocatedShadowAtlasSize.x, (float)allocatedShadowAtlasSize.y));
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0003D358 File Offset: 0x0003B558
		private void InitPassData(ref AdditionalLightsShadowCasterPass.PassData passData, UniversalCameraData cameraData, UniversalLightData lightData, UniversalShadowData shadowData)
		{
			passData.pass = this;
			passData.lightData = lightData;
			passData.shadowData = shadowData;
			passData.viewMatrix = cameraData.GetViewMatrix(0);
			passData.stripShadowsOffVariants = cameraData.renderer.stripShadowsOffVariants;
			passData.emptyShadowmap = this.m_CreateEmptyShadowmap;
			passData.shadowmapID = this.m_AdditionalLightsShadowmapID;
			passData.useStructuredBuffer = this.m_UseStructuredBuffer;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0003D3C8 File Offset: 0x0003B5C8
		private void InitEmptyPassData(ref AdditionalLightsShadowCasterPass.PassData passData, UniversalCameraData cameraData, UniversalLightData lightData, UniversalShadowData shadowData)
		{
			passData.pass = this;
			passData.lightData = lightData;
			passData.shadowData = shadowData;
			passData.stripShadowsOffVariants = cameraData.renderer.stripShadowsOffVariants;
			passData.emptyShadowmap = this.m_CreateEmptyShadowmap;
			passData.shadowmapID = this.m_AdditionalLightsShadowmapID;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0003D41C File Offset: 0x0003B61C
		private void InitRendererLists(ref CullingResults cullResults, ref AdditionalLightsShadowCasterPass.PassData passData, ScriptableRenderContext context, RenderGraph renderGraph, bool useRenderGraph)
		{
			if (!this.m_CreateEmptyShadowmap)
			{
				for (int globalShadowSliceIndex = 0; globalShadowSliceIndex < this.m_ShadowSliceToAdditionalLightIndex.Count; globalShadowSliceIndex++)
				{
					int additionalLightIndex = (int)this.m_ShadowSliceToAdditionalLightIndex[globalShadowSliceIndex];
					ShadowSliceData[] additionalLightsShadowSlices = this.m_AdditionalLightsShadowSlices;
					int visibleLightIndex = (int)this.m_AdditionalLightIndexToVisibleLightIndex[additionalLightIndex];
					ShadowDrawingSettings settings = new ShadowDrawingSettings(cullResults, visibleLightIndex);
					settings.useRenderingLayerMaskTest = UniversalRenderPipeline.asset.useRenderingLayers;
					if (useRenderGraph)
					{
						passData.shadowRendererListsHdl[globalShadowSliceIndex] = renderGraph.CreateShadowRendererList(ref settings);
					}
					else
					{
						passData.shadowRendererLists[globalShadowSliceIndex] = context.CreateShadowRendererList(ref settings);
					}
				}
			}
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0003D4C4 File Offset: 0x0003B6C4
		internal TextureHandle Render(RenderGraph graph, ContextContainer frameData)
		{
			UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			UniversalLightData lightData = frameData.Get<UniversalLightData>();
			UniversalShadowData shadowData = frameData.Get<UniversalShadowData>();
			AdditionalLightsShadowCasterPass.PassData passData;
			TextureHandle textureHandle;
			using (IRasterRenderGraphBuilder builder = graph.AddRasterRenderPass<AdditionalLightsShadowCasterPass.PassData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/AdditionalLightsShadowCasterPass.cs", 920))
			{
				this.InitPassData(ref passData, cameraData, lightData, shadowData);
				this.InitRendererLists(ref renderingData.cullResults, ref passData, default(ScriptableRenderContext), graph, true);
				TextureHandle shadowTexture;
				if (!this.m_CreateEmptyShadowmap)
				{
					for (int globalShadowSliceIndex = 0; globalShadowSliceIndex < this.m_ShadowSliceToAdditionalLightIndex.Count; globalShadowSliceIndex++)
					{
						builder.UseRendererList(in passData.shadowRendererListsHdl[globalShadowSliceIndex]);
					}
					shadowTexture = UniversalRenderer.CreateRenderGraphTexture(graph, this.m_AdditionalLightShadowDescriptor, "_AdditionalLightsShadowmapTexture", true, ShadowUtils.m_ForceShadowPointSampling ? FilterMode.Point : FilterMode.Bilinear, TextureWrapMode.Clamp);
					builder.SetRenderAttachmentDepth(shadowTexture, AccessFlags.Write);
				}
				else
				{
					shadowTexture = graph.defaultResources.defaultShadowTexture;
				}
				TextureDesc descriptor = shadowTexture.GetDescriptor(graph);
				passData.allocatedShadowAtlasSize = new Vector2Int(descriptor.width, descriptor.height);
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				if (shadowTexture.IsValid())
				{
					builder.SetGlobalTextureAfterPass(in shadowTexture, passData.shadowmapID);
				}
				builder.SetRenderFunc<AdditionalLightsShadowCasterPass.PassData>(delegate(AdditionalLightsShadowCasterPass.PassData data, RasterGraphContext context)
				{
					if (!data.emptyShadowmap)
					{
						data.pass.RenderAdditionalShadowmapAtlas(context.cmd, ref data, true);
						return;
					}
					data.pass.SetEmptyAdditionalShadowmapAtlas(context.cmd);
				});
				textureHandle = shadowTexture;
			}
			return textureHandle;
		}

		// Token: 0x04000C7C RID: 3196
		[Obsolete("AdditionalLightsShadowCasterPass.m_AdditionalShadowsBufferId was deprecated. Shadow slice matrix is now passed to the GPU using an entry in buffer m_AdditionalLightsWorldToShadow_SSBO", true)]
		public static int m_AdditionalShadowsBufferId;

		// Token: 0x04000C7D RID: 3197
		[Obsolete("AdditionalLightsShadowCasterPass.m_AdditionalShadowsIndicesId was deprecated. Shadow slice index is now passed to the GPU using last member of an entry in buffer m_AdditionalShadowParams_SSBO", true)]
		public static int m_AdditionalShadowsIndicesId;

		// Token: 0x04000C7E RID: 3198
		private static readonly Vector4 c_DefaultShadowParams = new Vector4(0f, 0f, 0f, -1f);

		// Token: 0x04000C7F RID: 3199
		private static int m_AdditionalLightsWorldToShadow_SSBO;

		// Token: 0x04000C80 RID: 3200
		private static int m_AdditionalShadowParams_SSBO;

		// Token: 0x04000C81 RID: 3201
		private bool m_UseStructuredBuffer;

		// Token: 0x04000C82 RID: 3202
		private const int k_ShadowmapBufferBits = 16;

		// Token: 0x04000C83 RID: 3203
		private int m_AdditionalLightsShadowmapID;

		// Token: 0x04000C84 RID: 3204
		internal RTHandle m_AdditionalLightsShadowmapHandle;

		// Token: 0x04000C85 RID: 3205
		private bool m_CreateEmptyShadowmap;

		// Token: 0x04000C86 RID: 3206
		private bool m_EmptyShadowmapNeedsClear;

		// Token: 0x04000C87 RID: 3207
		private RTHandle m_EmptyAdditionalLightShadowmapTexture;

		// Token: 0x04000C88 RID: 3208
		private const int k_EmptyShadowMapDimensions = 1;

		// Token: 0x04000C89 RID: 3209
		private const string k_AdditionalLightShadowMapTextureName = "_AdditionalLightsShadowmapTexture";

		// Token: 0x04000C8A RID: 3210
		private const string k_EmptyAdditionalLightShadowMapTextureName = "_EmptyAdditionalLightShadowmapTexture";

		// Token: 0x04000C8B RID: 3211
		internal static Vector4[] s_EmptyAdditionalLightIndexToShadowParams = null;

		// Token: 0x04000C8C RID: 3212
		private float m_MaxShadowDistanceSq;

		// Token: 0x04000C8D RID: 3213
		private float m_CascadeBorder;

		// Token: 0x04000C8E RID: 3214
		private ShadowSliceData[] m_AdditionalLightsShadowSlices;

		// Token: 0x04000C8F RID: 3215
		private bool[] m_VisibleLightIndexToIsCastingShadows;

		// Token: 0x04000C90 RID: 3216
		private short[] m_VisibleLightIndexToAdditionalLightIndex;

		// Token: 0x04000C91 RID: 3217
		private short[] m_AdditionalLightIndexToVisibleLightIndex;

		// Token: 0x04000C92 RID: 3218
		private Vector4[] m_AdditionalLightIndexToShadowParams;

		// Token: 0x04000C93 RID: 3219
		private Matrix4x4[] m_AdditionalLightShadowSliceIndexTo_WorldShadowMatrix;

		// Token: 0x04000C94 RID: 3220
		private List<short> m_ShadowSliceToAdditionalLightIndex = new List<short>();

		// Token: 0x04000C95 RID: 3221
		private List<byte> m_GlobalShadowSliceIndexToPerLightShadowSliceIndex = new List<byte>();

		// Token: 0x04000C96 RID: 3222
		private int renderTargetWidth;

		// Token: 0x04000C97 RID: 3223
		private int renderTargetHeight;

		// Token: 0x04000C98 RID: 3224
		private RenderTextureDescriptor m_AdditionalLightShadowDescriptor;

		// Token: 0x04000C99 RID: 3225
		private ProfilingSampler m_ProfilingSetupSampler = new ProfilingSampler("Setup Additional Shadows");

		// Token: 0x04000C9A RID: 3226
		private AdditionalLightsShadowCasterPass.PassData m_PassData;

		// Token: 0x04000C9B RID: 3227
		private const float LightTypeIdentifierInShadowParams_Spot = 0f;

		// Token: 0x04000C9C RID: 3228
		private const float LightTypeIdentifierInShadowParams_Point = 1f;

		// Token: 0x04000C9D RID: 3229
		private bool m_IssuedMessageAboutShadowSlicesTooMany;

		// Token: 0x04000C9E RID: 3230
		private bool m_IssuedMessageAboutShadowMapsRescale;

		// Token: 0x04000C9F RID: 3231
		private bool m_IssuedMessageAboutShadowMapsTooBig;

		// Token: 0x04000CA0 RID: 3232
		private bool m_IssuedMessageAboutRemovedShadowSlices;

		// Token: 0x04000CA1 RID: 3233
		private static bool m_IssuedMessageAboutPointLightHardShadowResolutionTooSmall = false;

		// Token: 0x04000CA2 RID: 3234
		private static bool m_IssuedMessageAboutPointLightSoftShadowResolutionTooSmall = false;

		// Token: 0x04000CA3 RID: 3235
		private Dictionary<int, ulong> m_ShadowRequestsHashes = new Dictionary<int, ulong>();

		// Token: 0x020001F6 RID: 502
		private static class AdditionalShadowsConstantBuffer
		{
			// Token: 0x04000CA4 RID: 3236
			public static int _AdditionalLightsWorldToShadow;

			// Token: 0x04000CA5 RID: 3237
			public static int _AdditionalShadowParams;

			// Token: 0x04000CA6 RID: 3238
			public static int _AdditionalShadowOffset0;

			// Token: 0x04000CA7 RID: 3239
			public static int _AdditionalShadowOffset1;

			// Token: 0x04000CA8 RID: 3240
			public static int _AdditionalShadowFadeParams;

			// Token: 0x04000CA9 RID: 3241
			public static int _AdditionalShadowmapSize;
		}

		// Token: 0x020001F7 RID: 503
		private class PassData
		{
			// Token: 0x04000CAA RID: 3242
			internal UniversalLightData lightData;

			// Token: 0x04000CAB RID: 3243
			internal UniversalShadowData shadowData;

			// Token: 0x04000CAC RID: 3244
			internal Matrix4x4 viewMatrix;

			// Token: 0x04000CAD RID: 3245
			internal bool stripShadowsOffVariants;

			// Token: 0x04000CAE RID: 3246
			internal AdditionalLightsShadowCasterPass pass;

			// Token: 0x04000CAF RID: 3247
			internal TextureHandle shadowmapTexture;

			// Token: 0x04000CB0 RID: 3248
			internal int shadowmapID;

			// Token: 0x04000CB1 RID: 3249
			internal bool useStructuredBuffer;

			// Token: 0x04000CB2 RID: 3250
			internal Vector2Int allocatedShadowAtlasSize;

			// Token: 0x04000CB3 RID: 3251
			internal bool emptyShadowmap;

			// Token: 0x04000CB4 RID: 3252
			internal RendererListHandle[] shadowRendererListsHdl = new RendererListHandle[256];

			// Token: 0x04000CB5 RID: 3253
			internal RendererList[] shadowRendererLists = new RendererList[256];
		}
	}
}
