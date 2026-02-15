using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000194 RID: 404
	public static class ShadowUtils
	{
		// Token: 0x06000881 RID: 2177 RVA: 0x00028874 File Offset: 0x00026A74
		public static bool ExtractDirectionalLightMatrix(ref CullingResults cullResults, ref ShadowData shadowData, int shadowLightIndex, int cascadeIndex, int shadowmapWidth, int shadowmapHeight, int shadowResolution, float shadowNearPlane, out Vector4 cascadeSplitDistance, out ShadowSliceData shadowSliceData, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix)
		{
			bool flag = ShadowUtils.ExtractDirectionalLightMatrix(ref cullResults, ref shadowData, shadowLightIndex, cascadeIndex, shadowmapWidth, shadowmapHeight, shadowResolution, shadowNearPlane, out cascadeSplitDistance, out shadowSliceData);
			viewMatrix = shadowSliceData.viewMatrix;
			projMatrix = shadowSliceData.projectionMatrix;
			return flag;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000288B4 File Offset: 0x00026AB4
		public static bool ExtractDirectionalLightMatrix(ref CullingResults cullResults, ref ShadowData shadowData, int shadowLightIndex, int cascadeIndex, int shadowmapWidth, int shadowmapHeight, int shadowResolution, float shadowNearPlane, out Vector4 cascadeSplitDistance, out ShadowSliceData shadowSliceData)
		{
			return ShadowUtils.ExtractDirectionalLightMatrix(ref cullResults, shadowData.universalShadowData, shadowLightIndex, cascadeIndex, shadowmapWidth, shadowmapHeight, shadowResolution, shadowNearPlane, out cascadeSplitDistance, out shadowSliceData);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000288DC File Offset: 0x00026ADC
		public static bool ExtractDirectionalLightMatrix(ref CullingResults cullResults, UniversalShadowData shadowData, int shadowLightIndex, int cascadeIndex, int shadowmapWidth, int shadowmapHeight, int shadowResolution, float shadowNearPlane, out Vector4 cascadeSplitDistance, out ShadowSliceData shadowSliceData)
		{
			bool flag = cullResults.ComputeDirectionalShadowMatricesAndCullingPrimitives(shadowLightIndex, cascadeIndex, shadowData.mainLightShadowCascadesCount, shadowData.mainLightShadowCascadesSplit, shadowResolution, shadowNearPlane, out shadowSliceData.viewMatrix, out shadowSliceData.projectionMatrix, out shadowSliceData.splitData);
			cascadeSplitDistance = shadowSliceData.splitData.cullingSphere;
			shadowSliceData.offsetX = cascadeIndex % 2 * shadowResolution;
			shadowSliceData.offsetY = cascadeIndex / 2 * shadowResolution;
			shadowSliceData.resolution = shadowResolution;
			shadowSliceData.shadowTransform = ShadowUtils.GetShadowTransform(shadowSliceData.projectionMatrix, shadowSliceData.viewMatrix);
			shadowSliceData.splitData.shadowCascadeBlendCullingFactor = 1f;
			if (shadowData.mainLightShadowCascadesCount > 1)
			{
				ShadowUtils.ApplySliceTransform(ref shadowSliceData, shadowmapWidth, shadowmapHeight);
			}
			return flag;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0002898B File Offset: 0x00026B8B
		public static bool ExtractSpotLightMatrix(ref CullingResults cullResults, ref ShadowData shadowData, int shadowLightIndex, out Matrix4x4 shadowMatrix, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData splitData)
		{
			return ShadowUtils.ExtractSpotLightMatrix(ref cullResults, shadowData.universalShadowData, shadowLightIndex, out shadowMatrix, out viewMatrix, out projMatrix, out splitData);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000289A1 File Offset: 0x00026BA1
		public static bool ExtractSpotLightMatrix(ref CullingResults cullResults, UniversalShadowData shadowData, int shadowLightIndex, out Matrix4x4 shadowMatrix, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData splitData)
		{
			bool flag = cullResults.ComputeSpotShadowMatricesAndCullingPrimitives(shadowLightIndex, out viewMatrix, out projMatrix, out splitData);
			shadowMatrix = ShadowUtils.GetShadowTransform(projMatrix, viewMatrix);
			return flag;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000289CC File Offset: 0x00026BCC
		public static bool ExtractPointLightMatrix(ref CullingResults cullResults, ref ShadowData shadowData, int shadowLightIndex, CubemapFace cubemapFace, float fovBias, out Matrix4x4 shadowMatrix, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData splitData)
		{
			return ShadowUtils.ExtractPointLightMatrix(ref cullResults, shadowData.universalShadowData, shadowLightIndex, cubemapFace, fovBias, out shadowMatrix, out viewMatrix, out projMatrix, out splitData);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x000289F4 File Offset: 0x00026BF4
		public static bool ExtractPointLightMatrix(ref CullingResults cullResults, UniversalShadowData shadowData, int shadowLightIndex, CubemapFace cubemapFace, float fovBias, out Matrix4x4 shadowMatrix, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData splitData)
		{
			bool flag = cullResults.ComputePointShadowMatricesAndCullingPrimitives(shadowLightIndex, cubemapFace, fovBias, out viewMatrix, out projMatrix, out splitData);
			viewMatrix.m10 = -viewMatrix.m10;
			viewMatrix.m11 = -viewMatrix.m11;
			viewMatrix.m12 = -viewMatrix.m12;
			viewMatrix.m13 = -viewMatrix.m13;
			shadowMatrix = ShadowUtils.GetShadowTransform(projMatrix, viewMatrix);
			return flag;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00028A68 File Offset: 0x00026C68
		public static void RenderShadowSlice(CommandBuffer cmd, ref ScriptableRenderContext context, ref ShadowSliceData shadowSliceData, ref ShadowDrawingSettings settings, Matrix4x4 proj, Matrix4x4 view)
		{
			cmd.SetGlobalDepthBias(1f, 2.5f);
			cmd.SetViewport(new Rect((float)shadowSliceData.offsetX, (float)shadowSliceData.offsetY, (float)shadowSliceData.resolution, (float)shadowSliceData.resolution));
			cmd.SetViewProjectionMatrices(view, proj);
			RendererList rl = context.CreateShadowRendererList(ref settings);
			cmd.DrawRendererList(rl);
			cmd.DisableScissorRect();
			context.ExecuteCommandBuffer(cmd);
			cmd.Clear();
			cmd.SetGlobalDepthBias(0f, 0f);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00028AE8 File Offset: 0x00026CE8
		internal static void RenderShadowSlice(RasterCommandBuffer cmd, ref ShadowSliceData shadowSliceData, ref RendererList shadowRendererList, Matrix4x4 proj, Matrix4x4 view)
		{
			cmd.SetGlobalDepthBias(1f, 2.5f);
			cmd.SetViewport(new Rect((float)shadowSliceData.offsetX, (float)shadowSliceData.offsetY, (float)shadowSliceData.resolution, (float)shadowSliceData.resolution));
			cmd.SetViewProjectionMatrices(view, proj);
			if (shadowRendererList.isValid)
			{
				cmd.DrawRendererList(shadowRendererList);
			}
			cmd.DisableScissorRect();
			cmd.SetGlobalDepthBias(0f, 0f);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00028B5F File Offset: 0x00026D5F
		public static void RenderShadowSlice(CommandBuffer cmd, ref ScriptableRenderContext context, ref ShadowSliceData shadowSliceData, ref ShadowDrawingSettings settings)
		{
			ShadowUtils.RenderShadowSlice(cmd, ref context, ref shadowSliceData, ref settings, shadowSliceData.projectionMatrix, shadowSliceData.viewMatrix);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00028B78 File Offset: 0x00026D78
		public static int GetMaxTileResolutionInAtlas(int atlasWidth, int atlasHeight, int tileCount)
		{
			int resolution = Mathf.Min(atlasWidth, atlasHeight);
			for (int currentTileCount = atlasWidth / resolution * atlasHeight / resolution; currentTileCount < tileCount; currentTileCount = atlasWidth / resolution * atlasHeight / resolution)
			{
				resolution >>= 1;
			}
			return resolution;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00028BA8 File Offset: 0x00026DA8
		public static void ApplySliceTransform(ref ShadowSliceData shadowSliceData, int atlasWidth, int atlasHeight)
		{
			Matrix4x4 sliceTransform = Matrix4x4.identity;
			float oneOverAtlasWidth = 1f / (float)atlasWidth;
			float oneOverAtlasHeight = 1f / (float)atlasHeight;
			sliceTransform.m00 = (float)shadowSliceData.resolution * oneOverAtlasWidth;
			sliceTransform.m11 = (float)shadowSliceData.resolution * oneOverAtlasHeight;
			sliceTransform.m03 = (float)shadowSliceData.offsetX * oneOverAtlasWidth;
			sliceTransform.m13 = (float)shadowSliceData.offsetY * oneOverAtlasHeight;
			shadowSliceData.shadowTransform = sliceTransform * shadowSliceData.shadowTransform;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00028C1F File Offset: 0x00026E1F
		public unsafe static Vector4 GetShadowBias(ref VisibleLight shadowLight, int shadowLightIndex, ref ShadowData shadowData, Matrix4x4 lightProjectionMatrix, float shadowResolution)
		{
			return ShadowUtils.GetShadowBias(ref shadowLight, shadowLightIndex, *shadowData.bias, *shadowData.supportsSoftShadows, lightProjectionMatrix, shadowResolution);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00028C39 File Offset: 0x00026E39
		public static Vector4 GetShadowBias(ref VisibleLight shadowLight, int shadowLightIndex, UniversalShadowData shadowData, Matrix4x4 lightProjectionMatrix, float shadowResolution)
		{
			return ShadowUtils.GetShadowBias(ref shadowLight, shadowLightIndex, shadowData.bias, shadowData.supportsSoftShadows, lightProjectionMatrix, shadowResolution);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00028C54 File Offset: 0x00026E54
		private static Vector4 GetShadowBias(ref VisibleLight shadowLight, int shadowLightIndex, List<Vector4> bias, bool supportsSoftShadows, Matrix4x4 lightProjectionMatrix, float shadowResolution)
		{
			if (shadowLightIndex < 0 || shadowLightIndex >= bias.Count)
			{
				Debug.LogWarning(string.Format("{0} is not a valid light index.", shadowLightIndex));
				return Vector4.zero;
			}
			float frustumSize;
			if (shadowLight.lightType == LightType.Directional)
			{
				frustumSize = 2f / lightProjectionMatrix.m00;
			}
			else if (shadowLight.lightType == LightType.Spot)
			{
				frustumSize = Mathf.Tan(shadowLight.spotAngle * 0.5f * 0.017453292f) * shadowLight.range;
			}
			else if (shadowLight.lightType == LightType.Point)
			{
				float fovBias = AdditionalLightsShadowCasterPass.GetPointLightShadowFrustumFovBiasInDegrees((int)shadowResolution, shadowLight.light.shadows == LightShadows.Soft);
				frustumSize = Mathf.Tan((90f + fovBias) * 0.5f * 0.017453292f) * shadowLight.range;
			}
			else
			{
				Debug.LogWarning("Only point, spot and directional shadow casters are supported in universal pipeline");
				frustumSize = 0f;
			}
			float texelSize = frustumSize / shadowResolution;
			float depthBias = -bias[shadowLightIndex].x * texelSize;
			float normalBias = -bias[shadowLightIndex].y * texelSize;
			if (shadowLight.lightType == LightType.Point)
			{
				normalBias = 0f;
			}
			if (supportsSoftShadows && shadowLight.light.shadows == LightShadows.Soft)
			{
				SoftShadowQuality softShadowQuality = SoftShadowQuality.Medium;
				UniversalAdditionalLightData additionalLightData;
				if (shadowLight.light.TryGetComponent<UniversalAdditionalLightData>(out additionalLightData))
				{
					softShadowQuality = additionalLightData.softShadowQuality;
				}
				float kernelRadius = 2.5f;
				switch (softShadowQuality)
				{
				case SoftShadowQuality.Low:
					kernelRadius = 1.5f;
					break;
				case SoftShadowQuality.Medium:
					kernelRadius = 2.5f;
					break;
				case SoftShadowQuality.High:
					kernelRadius = 3.5f;
					break;
				}
				depthBias *= kernelRadius;
				normalBias *= kernelRadius;
			}
			return new Vector4(depthBias, normalBias, (float)shadowLight.lightType, 0f);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00028DD4 File Offset: 0x00026FD4
		internal static void GetScaleAndBiasForLinearDistanceFade(float fadeDistance, float border, out float scale, out float bias)
		{
			if (border < 0.0001f)
			{
				float multiplier = 1000f;
				scale = multiplier;
				bias = -fadeDistance * multiplier;
				return;
			}
			border = 1f - border;
			border *= border;
			float distanceFadeNear = border * fadeDistance;
			scale = 1f / (fadeDistance - distanceFadeNear);
			bias = -distanceFadeNear / (fadeDistance - distanceFadeNear);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00028E1E File Offset: 0x0002701E
		public static void SetupShadowCasterConstantBuffer(CommandBuffer cmd, ref VisibleLight shadowLight, Vector4 shadowBias)
		{
			ShadowUtils.SetupShadowCasterConstantBuffer(CommandBufferHelpers.GetRasterCommandBuffer(cmd), ref shadowLight, shadowBias);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00028E30 File Offset: 0x00027030
		internal static void SetupShadowCasterConstantBuffer(RasterCommandBuffer cmd, ref VisibleLight shadowLight, Vector4 shadowBias)
		{
			ShadowUtils.SetShadowBias(cmd, shadowBias);
			Vector3 lightDirection = -shadowLight.localToWorldMatrix.GetColumn(2);
			ShadowUtils.SetLightDirection(cmd, lightDirection);
			Vector3 lightPosition = shadowLight.localToWorldMatrix.GetColumn(3);
			ShadowUtils.SetLightPosition(cmd, lightPosition);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00028E81 File Offset: 0x00027081
		internal static void SetShadowBias(RasterCommandBuffer cmd, Vector4 shadowBias)
		{
			cmd.SetGlobalVector(ShaderPropertyId.shadowBias, shadowBias);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00028E8F File Offset: 0x0002708F
		internal static void SetLightDirection(RasterCommandBuffer cmd, Vector3 lightDirection)
		{
			cmd.SetGlobalVector(ShaderPropertyId.lightDirection, new Vector4(lightDirection.x, lightDirection.y, lightDirection.z, 0f));
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00028EB8 File Offset: 0x000270B8
		internal static void SetLightPosition(RasterCommandBuffer cmd, Vector3 lightPosition)
		{
			cmd.SetGlobalVector(ShaderPropertyId.lightPosition, new Vector4(lightPosition.x, lightPosition.y, lightPosition.z, 1f));
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00028EE1 File Offset: 0x000270E1
		internal static void SetCameraPosition(RasterCommandBuffer cmd, Vector3 worldSpaceCameraPos)
		{
			cmd.SetGlobalVector(ShaderPropertyId.worldSpaceCameraPos, worldSpaceCameraPos);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00028EF4 File Offset: 0x000270F4
		internal static void SetWorldToCameraMatrix(RasterCommandBuffer cmd, Matrix4x4 viewMatrix)
		{
			Matrix4x4 worldToCameraMatrix = Matrix4x4.Scale(new Vector3(1f, 1f, -1f)) * viewMatrix;
			cmd.SetGlobalMatrix(ShaderPropertyId.worldToCameraMatrix, worldToCameraMatrix);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00028F30 File Offset: 0x00027130
		private static RenderTextureDescriptor GetTemporaryShadowTextureDescriptor(int width, int height, int bits)
		{
			GraphicsFormat format = GraphicsFormatUtility.GetDepthStencilFormat(bits, 0);
			return new RenderTextureDescriptor(width, height, GraphicsFormat.None, format)
			{
				shadowSamplingMode = (RenderingUtils.SupportsRenderTextureFormat(RenderTextureFormat.Shadowmap) ? ShadowSamplingMode.CompareDepths : ShadowSamplingMode.None)
			};
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00028F64 File Offset: 0x00027164
		[Obsolete("Use AllocShadowRT or ShadowRTReAllocateIfNeeded", true)]
		public static RenderTexture GetTemporaryShadowTexture(int width, int height, int bits)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(ShadowUtils.GetTemporaryShadowTextureDescriptor(width, height, bits));
			temporary.filterMode = (ShadowUtils.m_ForceShadowPointSampling ? FilterMode.Point : FilterMode.Bilinear);
			temporary.wrapMode = TextureWrapMode.Clamp;
			return temporary;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00028F8C File Offset: 0x0002718C
		public static bool ShadowRTNeedsReAlloc(RTHandle handle, int width, int height, int bits, int anisoLevel, float mipMapBias, string name)
		{
			if (handle == null || handle.rt == null)
			{
				return true;
			}
			RenderTextureDescriptor descriptor = ShadowUtils.GetTemporaryShadowTextureDescriptor(width, height, bits);
			if (ShadowUtils.m_ForceShadowPointSampling)
			{
				if (handle.rt.filterMode != FilterMode.Point)
				{
					return true;
				}
			}
			else if (handle.rt.filterMode != FilterMode.Bilinear)
			{
				return true;
			}
			TextureDesc shadowDesc = RTHandleResourcePool.CreateTextureDesc(descriptor, TextureSizeMode.Explicit, anisoLevel, mipMapBias, ShadowUtils.m_ForceShadowPointSampling ? FilterMode.Point : FilterMode.Bilinear, TextureWrapMode.Clamp, name);
			return RenderingUtils.RTHandleNeedsReAlloc(handle, in shadowDesc, false);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00029000 File Offset: 0x00027200
		public static RTHandle AllocShadowRT(int width, int height, int bits, int anisoLevel, float mipMapBias, string name)
		{
			RenderTextureDescriptor rtd = ShadowUtils.GetTemporaryShadowTextureDescriptor(width, height, bits);
			return RTHandles.Alloc(in rtd, ShadowUtils.m_ForceShadowPointSampling ? FilterMode.Point : FilterMode.Bilinear, TextureWrapMode.Clamp, true, 1, 0f, name);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00029032 File Offset: 0x00027232
		public static bool ShadowRTReAllocateIfNeeded(ref RTHandle handle, int width, int height, int bits, int anisoLevel = 1, float mipMapBias = 0f, string name = "")
		{
			if (ShadowUtils.ShadowRTNeedsReAlloc(handle, width, height, bits, anisoLevel, mipMapBias, name))
			{
				RTHandle rthandle = handle;
				if (rthandle != null)
				{
					rthandle.Release();
				}
				handle = ShadowUtils.AllocShadowRT(width, height, bits, anisoLevel, mipMapBias, name);
				return true;
			}
			return false;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00029068 File Offset: 0x00027268
		private static Matrix4x4 GetShadowTransform(Matrix4x4 proj, Matrix4x4 view)
		{
			if (SystemInfo.usesReversedZBuffer)
			{
				proj.m20 = -proj.m20;
				proj.m21 = -proj.m21;
				proj.m22 = -proj.m22;
				proj.m23 = -proj.m23;
			}
			Matrix4x4 worldToShadow = proj * view;
			Matrix4x4 textureScaleAndBias = Matrix4x4.identity;
			textureScaleAndBias.m00 = 0.5f;
			textureScaleAndBias.m11 = 0.5f;
			textureScaleAndBias.m22 = 0.5f;
			textureScaleAndBias.m03 = 0.5f;
			textureScaleAndBias.m23 = 0.5f;
			textureScaleAndBias.m13 = 0.5f;
			return textureScaleAndBias * worldToShadow;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00029114 File Offset: 0x00027314
		internal static float SoftShadowQualityToShaderProperty(Light light, bool softShadowsEnabled)
		{
			float softShadows = (softShadowsEnabled ? 1f : 0f);
			UniversalAdditionalLightData additionalLightData;
			if (light.TryGetComponent<UniversalAdditionalLightData>(out additionalLightData))
			{
				SoftShadowQuality? softShadowQuality2;
				if (additionalLightData.softShadowQuality != SoftShadowQuality.UsePipelineSettings)
				{
					softShadowQuality2 = new SoftShadowQuality?(additionalLightData.softShadowQuality);
				}
				else
				{
					UniversalRenderPipelineAsset asset = UniversalRenderPipeline.asset;
					softShadowQuality2 = ((asset != null) ? new SoftShadowQuality?(asset.softShadowQuality) : null);
				}
				SoftShadowQuality? softShadowQuality = softShadowQuality2;
				softShadows *= (float)Math.Max((int)softShadowQuality.Value, 1);
			}
			return softShadows;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x000039B4 File Offset: 0x00001BB4
		internal static bool SupportsPerLightSoftShadowQuality()
		{
			return true;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00029181 File Offset: 0x00027381
		internal static void SetPerLightSoftShadowKeyword(RasterCommandBuffer cmd, bool hasSoftShadows)
		{
			if (ShadowUtils.SupportsPerLightSoftShadowQuality())
			{
				cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadows, hasSoftShadows);
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00029198 File Offset: 0x00027398
		internal static void SetSoftShadowQualityShaderKeywords(RasterCommandBuffer cmd, UniversalShadowData shadowData)
		{
			cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadows, shadowData.isKeywordSoftShadowsEnabled);
			if (ShadowUtils.SupportsPerLightSoftShadowQuality())
			{
				cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadowsLow, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadowsMedium, false);
				cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadowsHigh, false);
				return;
			}
			if (shadowData.isKeywordSoftShadowsEnabled)
			{
				UniversalRenderPipelineAsset asset = UniversalRenderPipeline.asset;
				if (asset != null && asset.softShadowQuality == SoftShadowQuality.Low)
				{
					cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadowsLow, true);
					cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadowsMedium, false);
					cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadowsHigh, false);
					cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadows, false);
					return;
				}
			}
			if (shadowData.isKeywordSoftShadowsEnabled)
			{
				UniversalRenderPipelineAsset asset2 = UniversalRenderPipeline.asset;
				if (asset2 != null && asset2.softShadowQuality == SoftShadowQuality.Medium)
				{
					cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadowsLow, false);
					cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadowsMedium, true);
					cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadowsHigh, false);
					cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadows, false);
					return;
				}
			}
			if (shadowData.isKeywordSoftShadowsEnabled)
			{
				UniversalRenderPipelineAsset asset3 = UniversalRenderPipeline.asset;
				if (asset3 != null && asset3.softShadowQuality == SoftShadowQuality.High)
				{
					cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadowsLow, false);
					cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadowsMedium, false);
					cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadowsHigh, true);
					cmd.SetKeyword(in ShaderGlobalKeywords.SoftShadows, false);
				}
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x000292D0 File Offset: 0x000274D0
		internal static bool IsValidShadowCastingLight(UniversalLightData lightData, int i)
		{
			ref VisibleLight shadowLight = ref lightData.visibleLights.UnsafeElementAt(i);
			Light light = shadowLight.light;
			return !(light == null) && ShadowUtils.IsValidShadowCastingLight(lightData, i, shadowLight.lightType, light.shadows, light.shadowStrength);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00029315 File Offset: 0x00027515
		internal static bool IsValidShadowCastingLight(UniversalLightData lightData, int i, LightType lightType, LightShadows lightShadows, float shadowStrength)
		{
			return i != lightData.mainLightIndex && lightType != LightType.Directional && lightShadows != LightShadows.None && shadowStrength > 0f;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00029338 File Offset: 0x00027538
		internal static int GetPunctualLightShadowSlicesCount(in LightType lightType)
		{
			LightType lightType2 = lightType;
			if (lightType2 == LightType.Spot)
			{
				return 1;
			}
			if (lightType2 != LightType.Point)
			{
				return 0;
			}
			return 6;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00029356 File Offset: 0x00027556
		internal static bool FastApproximately(float a, float b)
		{
			return Mathf.Abs(a - b) < 1E-06f;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00029368 File Offset: 0x00027568
		internal static bool FastApproximately(Vector4 a, Vector4 b)
		{
			return ShadowUtils.FastApproximately(a.x, b.x) && ShadowUtils.FastApproximately(a.y, b.y) && ShadowUtils.FastApproximately(a.z, b.z) && ShadowUtils.FastApproximately(a.w, b.w);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x000293C1 File Offset: 0x000275C1
		internal static int MinimalPunctualLightShadowResolution(bool softShadow)
		{
			if (!softShadow)
			{
				return 8;
			}
			return 16;
		}

		// Token: 0x040008F3 RID: 2291
		internal static readonly bool m_ForceShadowPointSampling = SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal && GraphicsSettings.HasShaderDefine(Graphics.activeTier, BuiltinShaderDefine.UNITY_METAL_SHADOWS_USE_POINT_FILTERING);

		// Token: 0x040008F4 RID: 2292
		internal const int kMinimumPunctualLightHardShadowResolution = 8;

		// Token: 0x040008F5 RID: 2293
		internal const int kMinimumPunctualLightSoftShadowResolution = 16;
	}
}
