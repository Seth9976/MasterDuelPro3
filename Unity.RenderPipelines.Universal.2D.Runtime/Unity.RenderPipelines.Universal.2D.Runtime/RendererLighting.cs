using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000045 RID: 69
	internal static class RendererLighting
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x0000EE91 File Offset: 0x0000D091
		internal static GraphicsFormat GetRenderTextureFormat()
		{
			if (!RendererLighting.s_HasSetupRenderTextureFormatToUse)
			{
				if (SystemInfo.IsFormatSupported(GraphicsFormat.B10G11R11_UFloatPack32, GraphicsFormatUsage.Blend))
				{
					RendererLighting.s_RenderTextureFormatToUse = GraphicsFormat.B10G11R11_UFloatPack32;
				}
				else if (SystemInfo.IsFormatSupported(GraphicsFormat.R16G16B16A16_SFloat, GraphicsFormatUsage.Blend))
				{
					RendererLighting.s_RenderTextureFormatToUse = GraphicsFormat.R16G16B16A16_SFloat;
				}
				RendererLighting.s_HasSetupRenderTextureFormatToUse = true;
			}
			return RendererLighting.s_RenderTextureFormatToUse;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000EECC File Offset: 0x0000D0CC
		public static void CreateNormalMapRenderTexture(this IRenderPass2D pass, RenderingData renderingData, CommandBuffer cmd, float renderScale)
		{
			RenderTextureDescriptor descriptor = new RenderTextureDescriptor((int)((float)renderingData.cameraData.cameraTargetDescriptor.width * renderScale), (int)((float)renderingData.cameraData.cameraTargetDescriptor.height * renderScale));
			descriptor.graphicsFormat = RendererLighting.GetRenderTextureFormat();
			descriptor.useMipMap = false;
			descriptor.autoGenerateMips = false;
			descriptor.depthStencilFormat = GraphicsFormat.None;
			descriptor.msaaSamples = renderingData.cameraData.cameraTargetDescriptor.msaaSamples;
			descriptor.dimension = TextureDimension.Tex2D;
			RenderingUtils.ReAllocateHandleIfNeeded(ref pass.rendererData.normalsRenderTarget, in descriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_NormalMap");
			cmd.SetGlobalTexture(pass.rendererData.normalsRenderTarget.name, pass.rendererData.normalsRenderTarget.nameID);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000EF94 File Offset: 0x0000D194
		public static RenderTextureDescriptor GetBlendStyleRenderTextureDesc(this IRenderPass2D pass, RenderingData renderingData)
		{
			float renderTextureScale = Mathf.Clamp(pass.rendererData.lightRenderTextureScale, 0.01f, 1f);
			int width = (int)((float)renderingData.cameraData.cameraTargetDescriptor.width * renderTextureScale);
			int height = (int)((float)renderingData.cameraData.cameraTargetDescriptor.height * renderTextureScale);
			return new RenderTextureDescriptor(width, height)
			{
				graphicsFormat = RendererLighting.GetRenderTextureFormat(),
				useMipMap = false,
				autoGenerateMips = false,
				depthStencilFormat = GraphicsFormat.None,
				msaaSamples = 1,
				dimension = TextureDimension.Tex2D
			};
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000F028 File Offset: 0x0000D228
		public static void CreateCameraSortingLayerRenderTexture(this IRenderPass2D pass, RenderingData renderingData, CommandBuffer cmd, Downsampling downsamplingMethod)
		{
			float renderTextureScale = 1f;
			if (downsamplingMethod == Downsampling._2xBilinear)
			{
				renderTextureScale = 0.5f;
			}
			else if (downsamplingMethod == Downsampling._4xBox || downsamplingMethod == Downsampling._4xBilinear)
			{
				renderTextureScale = 0.25f;
			}
			int width = (int)((float)renderingData.cameraData.cameraTargetDescriptor.width * renderTextureScale);
			int height = (int)((float)renderingData.cameraData.cameraTargetDescriptor.height * renderTextureScale);
			RenderTextureDescriptor descriptor = new RenderTextureDescriptor(width, height);
			descriptor.graphicsFormat = renderingData.cameraData.cameraTargetDescriptor.graphicsFormat;
			descriptor.useMipMap = false;
			descriptor.autoGenerateMips = false;
			descriptor.depthStencilFormat = GraphicsFormat.None;
			descriptor.msaaSamples = 1;
			RenderingUtils.ReAllocateHandleIfNeeded(ref pass.rendererData.cameraSortingLayerRenderTarget, in descriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, 1, 0f, "_CameraSortingLayerTexture");
			cmd.SetGlobalTexture(pass.rendererData.cameraSortingLayerRenderTarget.name, pass.rendererData.cameraSortingLayerRenderTarget.nameID);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000F108 File Offset: 0x0000D308
		internal static void EnableBlendStyle(IRasterCommandBuffer cmd, int blendStyleIndex, bool enabled)
		{
			string keyword = RendererLighting.k_UseBlendStyleKeywords[blendStyleIndex];
			if (enabled)
			{
				cmd.EnableShaderKeyword(keyword);
				return;
			}
			cmd.DisableShaderKeyword(keyword);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000F130 File Offset: 0x0000D330
		internal static void DisableAllKeywords(RasterCommandBuffer cmd)
		{
			foreach (string keyword in RendererLighting.k_UseBlendStyleKeywords)
			{
				cmd.DisableShaderKeyword(keyword);
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000F15C File Offset: 0x0000D35C
		internal static void GetTransparencySortingMode(Renderer2DData rendererData, Camera camera, ref SortingSettings sortingSettings)
		{
			TransparencySortMode mode = rendererData.transparencySortMode;
			if (mode == TransparencySortMode.Default)
			{
				mode = (camera.orthographic ? TransparencySortMode.Orthographic : TransparencySortMode.Perspective);
			}
			if (mode == TransparencySortMode.Perspective)
			{
				sortingSettings.distanceMetric = DistanceMetric.Perspective;
				return;
			}
			if (mode != TransparencySortMode.Orthographic)
			{
				sortingSettings.distanceMetric = DistanceMetric.CustomAxis;
				sortingSettings.customAxis = rendererData.transparencySortAxis;
				return;
			}
			sortingSettings.distanceMetric = DistanceMetric.Orthographic;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000F1B0 File Offset: 0x0000D3B0
		private static bool CanRenderLight(IRenderPass2D pass, Light2D light, int blendStyleIndex, int layerToRender, bool isVolume, ref Mesh lightMesh, ref Material lightMaterial)
		{
			if (!(light != null) || light.lightType == Light2D.LightType.Global || light.blendStyleIndex != blendStyleIndex || !light.IsLitLayer(layerToRender))
			{
				return false;
			}
			lightMesh = light.lightMesh;
			if (lightMesh == null)
			{
				return false;
			}
			lightMaterial = pass.rendererData.GetLightMaterial(light, isVolume);
			return !(lightMaterial == null);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000F218 File Offset: 0x0000D418
		internal static bool CanCastShadows(Light2D light, int layerToRender)
		{
			return light.shadowsEnabled && light.shadowIntensity > 0f && light.IsLitLayer(layerToRender);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000F238 File Offset: 0x0000D438
		private static bool CanCastVolumetricShadows(Light2D light, int endLayerValue)
		{
			int topMostLayerValue = light.GetTopMostLitLayer();
			return light.volumetricShadowsEnabled && light.shadowVolumeIntensity > 0f && topMostLayerValue == endLayerValue;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000F268 File Offset: 0x0000D468
		internal static void RenderLight(IRenderPass2D pass, CommandBuffer cmd, Light2D light, bool isVolume, int blendStyleIndex, int layerToRender, bool hasShadows, bool batchingSupported, ref int shadowLightCount)
		{
			Mesh lightMesh = null;
			Material lightMaterial = null;
			if (!RendererLighting.CanRenderLight(pass, light, blendStyleIndex, layerToRender, isVolume, ref lightMesh, ref lightMaterial))
			{
				return;
			}
			int lightHash;
			bool canBatch = RendererLighting.lightBatch.CanBatch(light, lightMaterial, light.batchSlotIndex, out lightHash);
			bool hasCookies = RendererLighting.SetCookieShaderGlobals(cmd, light);
			if ((hasShadows || hasCookies || !canBatch) && batchingSupported)
			{
				RendererLighting.lightBatch.Flush(CommandBufferHelpers.GetRasterCommandBuffer(cmd));
			}
			if (hasShadows)
			{
				int num = shadowLightCount;
				shadowLightCount = num + 1;
				ShadowRendering.SetGlobalShadowTexture(cmd, light, num);
			}
			int slotIndex = RendererLighting.lightBatch.SlotIndex(light.batchSlotIndex);
			RendererLighting.SetPerLightShaderGlobals(CommandBufferHelpers.GetRasterCommandBuffer(cmd), light, slotIndex, isVolume, hasShadows, batchingSupported);
			if (light.lightType == Light2D.LightType.Point)
			{
				RendererLighting.SetPerPointLightShaderGlobals(CommandBufferHelpers.GetRasterCommandBuffer(cmd), light, slotIndex, batchingSupported);
			}
			if (batchingSupported)
			{
				RendererLighting.lightBatch.AddBatch(light, lightMaterial, light.GetMatrix(), lightMesh, 0, lightHash, light.batchSlotIndex);
				return;
			}
			cmd.DrawMesh(lightMesh, light.GetMatrix(), lightMaterial);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000F354 File Offset: 0x0000D554
		private static void RenderLightSet(IRenderPass2D pass, RenderingData renderingData, int blendStyleIndex, CommandBuffer cmd, ref LayerBatch layer, RenderTargetIdentifier renderTexture, List<Light2D> lights)
		{
			uint maxShadowLightCount = ShadowRendering.maxTextureCount;
			bool requiresRTInit = true;
			if (maxShadowLightCount < 1U)
			{
				Debug.LogError("maxShadowTextureCount cannot be less than 1");
				return;
			}
			NativeArray<bool> doesLightAtIndexHaveShadows = new NativeArray<bool>(lights.Count, Allocator.Temp, NativeArrayOptions.ClearMemory);
			int batchedLights;
			for (int lightIndex = 0; lightIndex < lights.Count; lightIndex += batchedLights)
			{
				long remainingLights = (long)((ulong)lights.Count - (ulong)((long)lightIndex));
				batchedLights = 0;
				int shadowLightCount = 0;
				while ((long)batchedLights < remainingLights && (long)shadowLightCount < (long)((ulong)maxShadowLightCount))
				{
					int curLightIndex = lightIndex + batchedLights;
					Light2D light = lights[curLightIndex];
					if (RendererLighting.CanCastShadows(light, layer.startLayerID))
					{
						doesLightAtIndexHaveShadows[curLightIndex] = false;
						if (pass.PrerenderShadows(renderingData, cmd, ref layer, light, shadowLightCount, light.shadowIntensity))
						{
							doesLightAtIndexHaveShadows[curLightIndex] = true;
							shadowLightCount++;
						}
					}
					batchedLights++;
				}
				if (shadowLightCount > 0 || requiresRTInit)
				{
					cmd.SetRenderTarget(renderTexture, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
					requiresRTInit = false;
				}
				shadowLightCount = 0;
				for (int lightIndexOffset = 0; lightIndexOffset < batchedLights; lightIndexOffset++)
				{
					int arrayIndex = lightIndex + lightIndexOffset;
					RendererLighting.RenderLight(pass, cmd, lights[arrayIndex], false, blendStyleIndex, layer.startLayerID, doesLightAtIndexHaveShadows[arrayIndex], LightBatch.isBatchingSupported, ref shadowLightCount);
				}
				RendererLighting.lightBatch.Flush(CommandBufferHelpers.GetRasterCommandBuffer(cmd));
				for (int releaseIndex = shadowLightCount - 1; releaseIndex >= 0; releaseIndex--)
				{
					ShadowRendering.ReleaseShadowRenderTexture(cmd, releaseIndex);
				}
			}
			doesLightAtIndexHaveShadows.Dispose();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000F4A8 File Offset: 0x0000D6A8
		public static void RenderLightVolumes(this IRenderPass2D pass, RenderingData renderingData, CommandBuffer cmd, ref LayerBatch layer, RenderTargetIdentifier renderTexture, RenderTargetIdentifier depthTexture, RenderBufferStoreAction intermediateStoreAction, RenderBufferStoreAction finalStoreAction, bool requiresRTInit, List<Light2D> lights)
		{
			uint maxShadowLightCount = ShadowRendering.maxTextureCount;
			NativeArray<bool> doesLightAtIndexHaveShadows = new NativeArray<bool>(lights.Count, Allocator.Temp, NativeArrayOptions.ClearMemory);
			if (maxShadowLightCount < 1U)
			{
				Debug.LogError("maxShadowLightCount cannot be less than 1");
				return;
			}
			int useFinalStoreActionAfter = lights.Count;
			if (intermediateStoreAction != finalStoreAction)
			{
				for (int i = lights.Count - 1; i >= 0; i--)
				{
					if (lights[i].renderVolumetricShadows)
					{
						useFinalStoreActionAfter = i;
						break;
					}
				}
			}
			int batchedLights;
			for (int lightIndex = 0; lightIndex < lights.Count; lightIndex += batchedLights)
			{
				long remainingLights = (long)((ulong)lights.Count - (ulong)((long)lightIndex));
				batchedLights = 0;
				int shadowLightCount = 0;
				while ((long)batchedLights < remainingLights && (long)shadowLightCount < (long)((ulong)maxShadowLightCount))
				{
					int curLightIndex = lightIndex + batchedLights;
					Light2D light = lights[curLightIndex];
					if (RendererLighting.CanCastVolumetricShadows(light, layer.endLayerValue))
					{
						doesLightAtIndexHaveShadows[curLightIndex] = false;
						if (pass.PrerenderShadows(renderingData, cmd, ref layer, light, shadowLightCount, light.shadowVolumeIntensity))
						{
							doesLightAtIndexHaveShadows[curLightIndex] = true;
							shadowLightCount++;
						}
					}
					batchedLights++;
				}
				if (shadowLightCount > 0 || requiresRTInit)
				{
					RenderBufferStoreAction storeAction = ((lightIndex + batchedLights >= useFinalStoreActionAfter) ? finalStoreAction : intermediateStoreAction);
					cmd.SetRenderTarget(renderTexture, RenderBufferLoadAction.Load, storeAction, depthTexture, RenderBufferLoadAction.Load, storeAction);
					requiresRTInit = false;
				}
				shadowLightCount = 0;
				for (int lightIndexOffset = 0; lightIndexOffset < batchedLights; lightIndexOffset++)
				{
					int arrayIndex = lightIndex + lightIndexOffset;
					Light2D light2 = lights[arrayIndex];
					if (light2.volumeIntensity > 0f && light2.volumetricEnabled && layer.endLayerValue == light2.GetTopMostLitLayer())
					{
						RendererLighting.RenderLight(pass, cmd, light2, true, light2.blendStyleIndex, layer.startLayerID, doesLightAtIndexHaveShadows[arrayIndex], LightBatch.isBatchingSupported, ref shadowLightCount);
					}
				}
				RendererLighting.lightBatch.Flush(CommandBufferHelpers.GetRasterCommandBuffer(cmd));
				for (int releaseIndex = shadowLightCount - 1; releaseIndex >= 0; releaseIndex--)
				{
					ShadowRendering.ReleaseShadowRenderTexture(cmd, releaseIndex);
				}
			}
			doesLightAtIndexHaveShadows.Dispose();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000F678 File Offset: 0x0000D878
		internal static void SetLightShaderGlobals(Renderer2DData rendererData, RasterCommandBuffer cmd)
		{
			for (int i = 0; i < rendererData.lightBlendStyles.Length; i++)
			{
				Light2DBlendStyle blendStyle = rendererData.lightBlendStyles[i];
				if (i >= RendererLighting.k_BlendFactorsPropIDs.Length)
				{
					break;
				}
				cmd.SetGlobalVector(RendererLighting.k_BlendFactorsPropIDs[i], blendStyle.blendFactors);
				cmd.SetGlobalVector(RendererLighting.k_MaskFilterPropIDs[i], blendStyle.maskTextureChannelFilter.mask);
				cmd.SetGlobalVector(RendererLighting.k_InvertedFilterPropIDs[i], blendStyle.maskTextureChannelFilter.inverted);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000F700 File Offset: 0x0000D900
		internal static void SetLightShaderGlobals(RasterCommandBuffer cmd, Light2DBlendStyle[] lightBlendStyles, int[] blendStyleIndices)
		{
			foreach (int blendStyleIndex in blendStyleIndices)
			{
				if (blendStyleIndex >= RendererLighting.k_BlendFactorsPropIDs.Length)
				{
					break;
				}
				Light2DBlendStyle blendStyle = lightBlendStyles[blendStyleIndex];
				cmd.SetGlobalVector(RendererLighting.k_BlendFactorsPropIDs[blendStyleIndex], blendStyle.blendFactors);
				cmd.SetGlobalVector(RendererLighting.k_MaskFilterPropIDs[blendStyleIndex], blendStyle.maskTextureChannelFilter.mask);
				cmd.SetGlobalVector(RendererLighting.k_InvertedFilterPropIDs[blendStyleIndex], blendStyle.maskTextureChannelFilter.inverted);
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000F782 File Offset: 0x0000D982
		private static float GetNormalizedInnerRadius(Light2D light)
		{
			return light.pointLightInnerRadius / light.pointLightOuterRadius;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000F791 File Offset: 0x0000D991
		private static float GetNormalizedAngle(float angle)
		{
			return angle / 360f;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000F79C File Offset: 0x0000D99C
		private static void GetScaledLightInvMatrix(Light2D light, out Matrix4x4 retMatrix)
		{
			float outerRadius = light.pointLightOuterRadius;
			Vector3 lightScale = Vector3.one;
			Vector3 outerRadiusScale = new Vector3(lightScale.x * outerRadius, lightScale.y * outerRadius, lightScale.z * outerRadius);
			Transform transform = light.transform;
			Matrix4x4 scaledLightMat = Matrix4x4.TRS(transform.position, transform.rotation, outerRadiusScale);
			retMatrix = Matrix4x4.Inverse(scaledLightMat);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000F800 File Offset: 0x0000DA00
		internal static void SetPerLightShaderGlobals(IRasterCommandBuffer cmd, Light2D light, int slot, bool isVolumetric, bool hasShadows, bool batchingSupported)
		{
			Color color = light.intensity * light.color.a * light.color;
			color.a = 1f;
			float volumeIntensity = (light.volumetricEnabled ? light.volumeIntensity : 1f);
			if (batchingSupported)
			{
				PerLight2D perLight = RendererLighting.lightBatch.GetLight(slot);
				perLight.Position = new float4(light.transform.position, light.normalMapDistance);
				perLight.FalloffIntensity = light.falloffIntensity;
				perLight.FalloffDistance = light.shapeLightFalloffSize;
				perLight.Color = new float4(color.r, color.g, color.b, color.a);
				perLight.VolumeOpacity = volumeIntensity;
				perLight.LightType = (int)light.lightType;
				perLight.ShadowIntensity = 1f;
				if (hasShadows)
				{
					perLight.ShadowIntensity = (isVolumetric ? (1f - light.shadowVolumeIntensity) : (1f - light.shadowIntensity));
				}
				RendererLighting.lightBatch.SetLight(slot, perLight);
			}
			else
			{
				cmd.SetGlobalVector(RendererLighting.k_L2DPosition, new float4(light.transform.position, light.normalMapDistance));
				cmd.SetGlobalFloat(RendererLighting.k_L2DFalloffIntensity, light.falloffIntensity);
				cmd.SetGlobalFloat(RendererLighting.k_L2DFalloffDistance, light.shapeLightFalloffSize);
				cmd.SetGlobalColor(RendererLighting.k_L2DColor, color);
				cmd.SetGlobalFloat(RendererLighting.k_L2DVolumeOpacity, volumeIntensity);
				cmd.SetGlobalInt(RendererLighting.k_L2DLightType, (int)light.lightType);
				cmd.SetGlobalFloat(RendererLighting.k_L2DShadowIntensity, hasShadows ? (isVolumetric ? (1f - light.shadowVolumeIntensity) : (1f - light.shadowIntensity)) : 1f);
			}
			if (hasShadows)
			{
				ShadowRendering.SetGlobalShadowProp(cmd);
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000F9D0 File Offset: 0x0000DBD0
		internal static void SetPerPointLightShaderGlobals(IRasterCommandBuffer cmd, Light2D light, int slot, bool batchingSupported)
		{
			Matrix4x4 lightInverseMatrix;
			RendererLighting.GetScaledLightInvMatrix(light, out lightInverseMatrix);
			float innerRadius = RendererLighting.GetNormalizedInnerRadius(light);
			float innerAngle = RendererLighting.GetNormalizedAngle(light.pointLightInnerAngle);
			float outerAngle = RendererLighting.GetNormalizedAngle(light.pointLightOuterAngle);
			float innerRadiusMult = 1f / (1f - innerRadius);
			if (batchingSupported)
			{
				PerLight2D perLight = RendererLighting.lightBatch.GetLight(slot);
				perLight.InvMatrix = new float4x4(lightInverseMatrix.GetColumn(0), lightInverseMatrix.GetColumn(1), lightInverseMatrix.GetColumn(2), lightInverseMatrix.GetColumn(3));
				perLight.InnerRadiusMult = innerRadiusMult;
				perLight.InnerAngle = innerAngle;
				perLight.OuterAngle = outerAngle;
				RendererLighting.lightBatch.SetLight(slot, perLight);
				return;
			}
			cmd.SetGlobalMatrix(RendererLighting.k_L2DInvMatrix, lightInverseMatrix);
			cmd.SetGlobalFloat(RendererLighting.k_L2DInnerRadiusMult, innerRadiusMult);
			cmd.SetGlobalFloat(RendererLighting.k_L2DInnerAngle, innerAngle);
			cmd.SetGlobalFloat(RendererLighting.k_L2DOuterAngle, outerAngle);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000FABB File Offset: 0x0000DCBB
		internal static bool SetCookieShaderGlobals(CommandBuffer cmd, Light2D light)
		{
			if (light.useCookieSprite)
			{
				cmd.SetGlobalTexture((light.lightType == Light2D.LightType.Sprite) ? RendererLighting.k_CookieTexID : RendererLighting.k_PointLightCookieTexID, light.lightCookieSprite.texture);
			}
			return light.useCookieSprite;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000FAF6 File Offset: 0x0000DCF6
		internal static void SetCookieShaderProperties(Light2D light, MaterialPropertyBlock properties)
		{
			if (light.useCookieSprite && light.m_CookieSpriteTextureHandle.IsValid())
			{
				properties.SetTexture((light.lightType == Light2D.LightType.Sprite) ? RendererLighting.k_CookieTexID : RendererLighting.k_PointLightCookieTexID, light.m_CookieSpriteTextureHandle);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000FB34 File Offset: 0x0000DD34
		public static void ClearDirtyLighting(this IRenderPass2D pass, CommandBuffer cmd, uint blendStylesUsed)
		{
			for (int i = 0; i < pass.rendererData.lightBlendStyles.Length; i++)
			{
				if ((blendStylesUsed & (1U << i)) != 0U && pass.rendererData.lightBlendStyles[i].isDirty)
				{
					CoreUtils.SetRenderTarget(cmd, pass.rendererData.lightBlendStyles[i].renderTargetHandle, ClearFlag.Color, Color.black, 0, CubemapFace.Unknown, -1);
					pass.rendererData.lightBlendStyles[i].isDirty = false;
				}
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000FBB8 File Offset: 0x0000DDB8
		internal unsafe static void RenderNormals(this IRenderPass2D pass, ScriptableRenderContext context, RenderingData renderingData, DrawingSettings drawSettings, FilteringSettings filterSettings, RTHandle depthTarget, bool bFirstClear)
		{
			CommandBuffer cmd = *renderingData.commandBuffer;
			using (new ProfilingScope(cmd, RendererLighting.m_ProfilingSampler))
			{
				float normalRTScale;
				if (depthTarget != null)
				{
					normalRTScale = 1f;
				}
				else
				{
					normalRTScale = Mathf.Clamp(pass.rendererData.lightRenderTextureScale, 0.01f, 1f);
				}
				pass.CreateNormalMapRenderTexture(renderingData, cmd, normalRTScale);
				RenderBufferStoreAction storeAction = ((renderingData.cameraData.cameraTargetDescriptor.msaaSamples > 1) ? RenderBufferStoreAction.Resolve : RenderBufferStoreAction.Store);
				ClearFlag clearFlag = ((pass.rendererData.useDepthStencilBuffer && bFirstClear) ? ClearFlag.All : ClearFlag.Color);
				if (depthTarget != null)
				{
					CoreUtils.SetRenderTarget(cmd, pass.rendererData.normalsRenderTarget, RenderBufferLoadAction.DontCare, storeAction, depthTarget, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, clearFlag, RendererLighting.k_NormalClearColor, 0, CubemapFace.Unknown, -1);
				}
				else
				{
					CoreUtils.SetRenderTarget(cmd, pass.rendererData.normalsRenderTarget, RenderBufferLoadAction.DontCare, storeAction, clearFlag, RendererLighting.k_NormalClearColor, 0, CubemapFace.Unknown, -1);
				}
				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();
				drawSettings.SetShaderPassName(0, RendererLighting.k_NormalsRenderingPassName);
				RendererListParams param = new RendererListParams(*renderingData.cullResults, drawSettings, filterSettings);
				RendererList rl = context.CreateRendererList(ref param);
				cmd.DrawRendererList(rl);
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000FCEC File Offset: 0x0000DEEC
		public static void RenderLights(this IRenderPass2D pass, RenderingData renderingData, CommandBuffer cmd, ref LayerBatch layerBatch, ref RenderTextureDescriptor rtDesc)
		{
			List<Light2D> culledLights = pass.rendererData.lightCullResult.visibleLights;
			for (int i = 0; i < culledLights.Count; i++)
			{
				culledLights[i].CacheValues();
			}
			ShadowCasterGroup2DManager.CacheValues();
			Light2DBlendStyle[] blendStyles = pass.rendererData.lightBlendStyles;
			for (int j = 0; j < blendStyles.Length; j++)
			{
				if ((layerBatch.lightStats.blendStylesUsed & (1U << j)) != 0U)
				{
					string sampleName = blendStyles[j].name;
					cmd.BeginSample(sampleName);
					Color clearColor;
					if (!Light2DManager.GetGlobalColor(layerBatch.startLayerID, j, out clearColor))
					{
						clearColor = Color.black;
					}
					bool flag = (layerBatch.lightStats.blendStylesWithLights & (1U << j)) > 0U;
					RenderTextureDescriptor desc = rtDesc;
					if (!flag)
					{
						desc.width = (desc.height = 4);
					}
					RenderTargetIdentifier identifier = layerBatch.GetRTId(cmd, desc, j);
					cmd.SetRenderTarget(identifier, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
					cmd.ClearRenderTarget(false, true, clearColor);
					if (flag)
					{
						RendererLighting.RenderLightSet(pass, renderingData, j, cmd, ref layerBatch, identifier, pass.rendererData.lightCullResult.visibleLights);
					}
					cmd.EndSample(sampleName);
				}
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000FE0D File Offset: 0x0000E00D
		private static void SetBlendModes(Material material, BlendMode src, BlendMode dst)
		{
			material.SetFloat(RendererLighting.k_SrcBlendID, (float)src);
			material.SetFloat(RendererLighting.k_DstBlendID, (float)dst);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000FE2C File Offset: 0x0000E02C
		private static uint GetLightMaterialIndex(Light2D light, bool isVolume)
		{
			bool isPoint = light.isPointLight;
			int bitIndex = 0;
			uint volumeBit = (isVolume ? (1U << bitIndex) : 0U);
			bitIndex++;
			uint shapeBit = ((isVolume && !isPoint) ? (1U << bitIndex) : 0U);
			bitIndex++;
			uint additiveBit = ((light.overlapOperation == Light2D.OverlapOperation.AlphaBlend) ? 0U : (1U << bitIndex));
			bitIndex++;
			uint pointCookieBit = ((isPoint && light.lightCookieSprite != null && light.lightCookieSprite.texture != null) ? (1U << bitIndex) : 0U);
			bitIndex++;
			uint num = ((light.normalMapQuality == Light2D.NormalMapQuality.Fast) ? (1U << bitIndex) : 0U);
			bitIndex++;
			uint useNormalMap = ((light.normalMapQuality != Light2D.NormalMapQuality.Disabled) ? (1U << bitIndex) : 0U);
			return num | pointCookieBit | additiveBit | shapeBit | volumeBit | useNormalMap;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000FEE8 File Offset: 0x0000E0E8
		private static Material CreateLightMaterial(Renderer2DData rendererData, Light2D light, bool isVolume)
		{
			Renderer2DResources resources;
			if (!GraphicsSettings.TryGetRenderPipelineSettings<Renderer2DResources>(out resources))
			{
				return null;
			}
			bool isPointLight = light.isPointLight;
			Material material = CoreUtils.CreateEngineMaterial(resources.lightShader);
			if (!isVolume)
			{
				if (light.overlapOperation == Light2D.OverlapOperation.Additive)
				{
					RendererLighting.SetBlendModes(material, BlendMode.One, BlendMode.One);
					material.EnableKeyword(RendererLighting.k_UseAdditiveBlendingKeyword);
				}
				else
				{
					RendererLighting.SetBlendModes(material, BlendMode.SrcAlpha, BlendMode.OneMinusSrcAlpha);
				}
			}
			else
			{
				material.EnableKeyword(RendererLighting.k_UseVolumetric);
				if (light.lightType == Light2D.LightType.Point)
				{
					RendererLighting.SetBlendModes(material, BlendMode.One, BlendMode.One);
				}
				else
				{
					RendererLighting.SetBlendModes(material, BlendMode.SrcAlpha, BlendMode.One);
				}
			}
			if (isPointLight && light.lightCookieSprite != null && light.lightCookieSprite.texture != null)
			{
				material.EnableKeyword(RendererLighting.k_UsePointLightCookiesKeyword);
			}
			if (light.normalMapQuality == Light2D.NormalMapQuality.Fast)
			{
				material.EnableKeyword(RendererLighting.k_LightQualityFastKeyword);
			}
			if (light.normalMapQuality != Light2D.NormalMapQuality.Disabled)
			{
				material.EnableKeyword(RendererLighting.k_UseNormalMap);
			}
			return material;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000FFBC File Offset: 0x0000E1BC
		public static Material GetLightMaterial(this Renderer2DData rendererData, Light2D light, bool isVolume)
		{
			uint materialIndex = RendererLighting.GetLightMaterialIndex(light, isVolume);
			Material material;
			if (!rendererData.lightMaterials.TryGetValue(materialIndex, out material))
			{
				material = RendererLighting.CreateLightMaterial(rendererData, light, isVolume);
				rendererData.lightMaterials[materialIndex] = material;
			}
			return material;
		}

		// Token: 0x04000169 RID: 361
		private static readonly ProfilingSampler m_ProfilingSampler = new ProfilingSampler("Draw Normals");

		// Token: 0x0400016A RID: 362
		private static readonly ShaderTagId k_NormalsRenderingPassName = new ShaderTagId("NormalsRendering");

		// Token: 0x0400016B RID: 363
		public static readonly Color k_NormalClearColor = new Color(0.5f, 0.5f, 0.5f, 1f);

		// Token: 0x0400016C RID: 364
		private static readonly string k_UsePointLightCookiesKeyword = "USE_POINT_LIGHT_COOKIES";

		// Token: 0x0400016D RID: 365
		private static readonly string k_LightQualityFastKeyword = "LIGHT_QUALITY_FAST";

		// Token: 0x0400016E RID: 366
		private static readonly string k_UseNormalMap = "USE_NORMAL_MAP";

		// Token: 0x0400016F RID: 367
		private static readonly string k_UseAdditiveBlendingKeyword = "USE_ADDITIVE_BLENDING";

		// Token: 0x04000170 RID: 368
		private static readonly string k_UseVolumetric = "USE_VOLUMETRIC";

		// Token: 0x04000171 RID: 369
		private static readonly string[] k_UseBlendStyleKeywords = new string[] { "USE_SHAPE_LIGHT_TYPE_0", "USE_SHAPE_LIGHT_TYPE_1", "USE_SHAPE_LIGHT_TYPE_2", "USE_SHAPE_LIGHT_TYPE_3" };

		// Token: 0x04000172 RID: 370
		private static readonly int[] k_BlendFactorsPropIDs = new int[]
		{
			Shader.PropertyToID("_ShapeLightBlendFactors0"),
			Shader.PropertyToID("_ShapeLightBlendFactors1"),
			Shader.PropertyToID("_ShapeLightBlendFactors2"),
			Shader.PropertyToID("_ShapeLightBlendFactors3")
		};

		// Token: 0x04000173 RID: 371
		private static readonly int[] k_MaskFilterPropIDs = new int[]
		{
			Shader.PropertyToID("_ShapeLightMaskFilter0"),
			Shader.PropertyToID("_ShapeLightMaskFilter1"),
			Shader.PropertyToID("_ShapeLightMaskFilter2"),
			Shader.PropertyToID("_ShapeLightMaskFilter3")
		};

		// Token: 0x04000174 RID: 372
		private static readonly int[] k_InvertedFilterPropIDs = new int[]
		{
			Shader.PropertyToID("_ShapeLightInvertedFilter0"),
			Shader.PropertyToID("_ShapeLightInvertedFilter1"),
			Shader.PropertyToID("_ShapeLightInvertedFilter2"),
			Shader.PropertyToID("_ShapeLightInvertedFilter3")
		};

		// Token: 0x04000175 RID: 373
		public static readonly string[] k_ShapeLightTextureIDs = new string[] { "_ShapeLightTexture0", "_ShapeLightTexture1", "_ShapeLightTexture2", "_ShapeLightTexture3" };

		// Token: 0x04000176 RID: 374
		private static GraphicsFormat s_RenderTextureFormatToUse = GraphicsFormat.R8G8B8A8_UNorm;

		// Token: 0x04000177 RID: 375
		private static bool s_HasSetupRenderTextureFormatToUse;

		// Token: 0x04000178 RID: 376
		private static readonly int k_SrcBlendID = Shader.PropertyToID("_SrcBlend");

		// Token: 0x04000179 RID: 377
		private static readonly int k_DstBlendID = Shader.PropertyToID("_DstBlend");

		// Token: 0x0400017A RID: 378
		private static readonly int k_CookieTexID = Shader.PropertyToID("_CookieTex");

		// Token: 0x0400017B RID: 379
		private static readonly int k_PointLightCookieTexID = Shader.PropertyToID("_PointLightCookieTex");

		// Token: 0x0400017C RID: 380
		private static readonly int k_L2DInvMatrix = Shader.PropertyToID("L2DInvMatrix");

		// Token: 0x0400017D RID: 381
		private static readonly int k_L2DColor = Shader.PropertyToID("L2DColor");

		// Token: 0x0400017E RID: 382
		private static readonly int k_L2DPosition = Shader.PropertyToID("L2DPosition");

		// Token: 0x0400017F RID: 383
		private static readonly int k_L2DFalloffIntensity = Shader.PropertyToID("L2DFalloffIntensity");

		// Token: 0x04000180 RID: 384
		private static readonly int k_L2DFalloffDistance = Shader.PropertyToID("L2DFalloffDistance");

		// Token: 0x04000181 RID: 385
		private static readonly int k_L2DOuterAngle = Shader.PropertyToID("L2DOuterAngle");

		// Token: 0x04000182 RID: 386
		private static readonly int k_L2DInnerAngle = Shader.PropertyToID("L2DInnerAngle");

		// Token: 0x04000183 RID: 387
		private static readonly int k_L2DInnerRadiusMult = Shader.PropertyToID("L2DInnerRadiusMult");

		// Token: 0x04000184 RID: 388
		private static readonly int k_L2DVolumeOpacity = Shader.PropertyToID("L2DVolumeOpacity");

		// Token: 0x04000185 RID: 389
		private static readonly int k_L2DShadowIntensity = Shader.PropertyToID("L2DShadowIntensity");

		// Token: 0x04000186 RID: 390
		private static readonly int k_L2DLightType = Shader.PropertyToID("L2DLightType");

		// Token: 0x04000187 RID: 391
		internal static LightBatch lightBatch = new LightBatch();
	}
}
