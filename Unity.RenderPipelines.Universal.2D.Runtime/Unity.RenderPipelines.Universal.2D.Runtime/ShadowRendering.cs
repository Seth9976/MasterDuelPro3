using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000098 RID: 152
	internal static class ShadowRendering
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00019332 File Offset: 0x00017532
		// (set) Token: 0x0600037E RID: 894 RVA: 0x00019339 File Offset: 0x00017539
		public static uint maxTextureCount { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00019341 File Offset: 0x00017541
		public static RenderTargetIdentifier[] lightInputTextures
		{
			get
			{
				return ShadowRendering.m_LightInputTextures;
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00019348 File Offset: 0x00017548
		internal static void InitializeBudget(uint maxTextureCount)
		{
			if (ShadowRendering.m_RenderTargets == null || (long)ShadowRendering.m_RenderTargets.Length != (long)((ulong)maxTextureCount))
			{
				ShadowRendering.m_RenderTargets = new RTHandle[maxTextureCount];
				ShadowRendering.m_RenderTargetIds = new int[maxTextureCount];
				ShadowRendering.maxTextureCount = maxTextureCount;
				int i = 0;
				while ((long)i < (long)((ulong)maxTextureCount))
				{
					ShadowRendering.m_RenderTargetIds[i] = Shader.PropertyToID(string.Format("ShadowTex_{0}", i));
					ShadowRendering.m_RenderTargets[i] = RTHandles.Alloc(ShadowRendering.m_RenderTargetIds[i], string.Format("ShadowTex_{0}", i));
					i++;
				}
			}
			if (ShadowRendering.m_LightInputTextures == null || (long)ShadowRendering.m_LightInputTextures.Length != (long)((ulong)maxTextureCount))
			{
				ShadowRendering.m_LightInputTextures = new RenderTargetIdentifier[maxTextureCount];
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000193F4 File Offset: 0x000175F4
		private static Material CreateMaterial(Shader shader, int offset, int pass)
		{
			Material material = CoreUtils.CreateEngineMaterial(shader);
			material.SetInt(ShadowRendering.k_ShadowColorMaskID, 1 << offset + 1);
			material.SetPass(pass);
			return material;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00019418 File Offset: 0x00017618
		private static Material GetProjectedShadowMaterial(Material material, Func<Renderer2DResources, Shader> shaderFunc, int offset, int pass)
		{
			if (material != null)
			{
				return material;
			}
			Renderer2DResources renderer2DResources;
			if (!GraphicsSettings.TryGetRenderPipelineSettings<Renderer2DResources>(out renderer2DResources))
			{
				return null;
			}
			Shader shader = shaderFunc(renderer2DResources);
			if (material != null && material.shader != shader)
			{
				material = null;
			}
			if (material == null)
			{
				material = CoreUtils.CreateEngineMaterial(shader);
				material.SetInt(ShadowRendering.k_ShadowColorMaskID, 1 << offset + 1);
				material.SetPass(pass);
			}
			return material;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001948A File Offset: 0x0001768A
		internal static Material GetProjectedShadowMaterial(this Renderer2DData rendererData)
		{
			rendererData.projectedShadowMaterial = ShadowRendering.GetProjectedShadowMaterial(rendererData.projectedShadowMaterial, (Renderer2DResources r) => r.projectedShadowShader, 0, 0);
			return rendererData.projectedShadowMaterial;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000194C4 File Offset: 0x000176C4
		internal static Material GetProjectedUnshadowMaterial(this Renderer2DData rendererData)
		{
			rendererData.projectedUnshadowMaterial = ShadowRendering.GetProjectedShadowMaterial(rendererData.projectedUnshadowMaterial, (Renderer2DResources r) => r.projectedShadowShader, 1, 1);
			return rendererData.projectedUnshadowMaterial;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000194FE File Offset: 0x000176FE
		private static Material GetSpriteShadowMaterial(this Renderer2DData rendererData)
		{
			rendererData.spriteSelfShadowMaterial = ShadowRendering.GetProjectedShadowMaterial(rendererData.spriteSelfShadowMaterial, (Renderer2DResources r) => r.spriteShadowShader, 0, 0);
			return rendererData.spriteSelfShadowMaterial;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00019538 File Offset: 0x00017738
		private static Material GetSpriteUnshadowMaterial(this Renderer2DData rendererData)
		{
			rendererData.spriteUnshadowMaterial = ShadowRendering.GetProjectedShadowMaterial(rendererData.spriteUnshadowMaterial, (Renderer2DResources r) => r.spriteUnshadowShader, 1, 0);
			return rendererData.spriteUnshadowMaterial;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00019572 File Offset: 0x00017772
		private static Material GetGeometryShadowMaterial(this Renderer2DData rendererData)
		{
			rendererData.geometrySelfShadowMaterial = ShadowRendering.GetProjectedShadowMaterial(rendererData.geometrySelfShadowMaterial, (Renderer2DResources r) => r.geometryShadowShader, 0, 0);
			return rendererData.geometrySelfShadowMaterial;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000195AC File Offset: 0x000177AC
		private static Material GetGeometryUnshadowMaterial(this Renderer2DData rendererData)
		{
			rendererData.geometryUnshadowMaterial = ShadowRendering.GetProjectedShadowMaterial(rendererData.geometryUnshadowMaterial, (Renderer2DResources r) => r.geometryUnshadowShader, 1, 0);
			return rendererData.geometryUnshadowMaterial;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000195E8 File Offset: 0x000177E8
		private static void CalculateFrustumCornersPerspective(Camera camera, float distance, NativeArray<Vector3> corners)
		{
			float verticalFieldOfView = camera.fieldOfView;
			float halfHeight = Mathf.Tan(0.5f * verticalFieldOfView * 0.017453292f) * distance;
			float halfWidth = halfHeight * camera.aspect;
			corners[0] = new Vector3(halfWidth, halfHeight, distance);
			corners[1] = new Vector3(halfWidth, -halfHeight, distance);
			corners[2] = new Vector3(-halfWidth, halfHeight, distance);
			corners[3] = new Vector3(-halfWidth, -halfHeight, distance);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00019660 File Offset: 0x00017860
		private static void CalculateFrustumCornersOrthographic(Camera camera, float distance, NativeArray<Vector3> corners)
		{
			float halfHeight = camera.orthographicSize;
			float halfWidth = halfHeight * camera.aspect;
			corners[0] = new Vector3(halfWidth, halfHeight, distance);
			corners[1] = new Vector3(halfWidth, -halfHeight, distance);
			corners[2] = new Vector3(-halfWidth, halfHeight, distance);
			corners[3] = new Vector3(-halfWidth, -halfHeight, distance);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000196C4 File Offset: 0x000178C4
		private static Bounds CalculateWorldSpaceBounds(Camera camera, ILight2DCullResult cullResult)
		{
			NativeArray<Vector3> nearCorners = new NativeArray<Vector3>(4, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			NativeArray<Vector3> farCorners = new NativeArray<Vector3>(4, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			if (camera.orthographic)
			{
				ShadowRendering.CalculateFrustumCornersOrthographic(camera, camera.nearClipPlane, nearCorners);
				ShadowRendering.CalculateFrustumCornersOrthographic(camera, camera.farClipPlane, farCorners);
			}
			else
			{
				ShadowRendering.CalculateFrustumCornersPerspective(camera, camera.nearClipPlane, nearCorners);
				ShadowRendering.CalculateFrustumCornersPerspective(camera, camera.farClipPlane, farCorners);
			}
			Vector3 minCorner = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			Vector3 maxCorner = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			for (int i = 0; i < 4; i++)
			{
				maxCorner = Vector3.Max(maxCorner, nearCorners[i]);
				maxCorner = Vector3.Max(maxCorner, farCorners[i]);
				minCorner = Vector3.Min(minCorner, nearCorners[i]);
				minCorner = Vector3.Min(minCorner, farCorners[i]);
			}
			nearCorners.Dispose();
			farCorners.Dispose();
			maxCorner = camera.transform.TransformPoint(maxCorner);
			minCorner = camera.transform.TransformPoint(minCorner);
			for (int j = 0; j < cullResult.visibleLights.Count; j++)
			{
				Vector3 lightPos = cullResult.visibleLights[j].transform.position;
				maxCorner = Vector3.Max(maxCorner, lightPos);
				minCorner = Vector3.Min(minCorner, lightPos);
			}
			Vector3 vector = 0.5f * (minCorner + maxCorner);
			Vector3 size = maxCorner - minCorner;
			return new Bounds(vector, size);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00019830 File Offset: 0x00017A30
		internal static void CallOnBeforeRender(Camera camera, ILight2DCullResult cullResult)
		{
			if (ShadowCasterGroup2DManager.shadowCasterGroups != null)
			{
				Bounds bounds = ShadowRendering.CalculateWorldSpaceBounds(camera, cullResult);
				List<ShadowCasterGroup2D> groups = ShadowCasterGroup2DManager.shadowCasterGroups;
				for (int groupIndex = 0; groupIndex < groups.Count; groupIndex++)
				{
					List<ShadowCaster2D> shadowCasters = groups[groupIndex].GetShadowCasters();
					if (shadowCasters != null)
					{
						for (int shadowCasterIndex = 0; shadowCasterIndex < shadowCasters.Count; shadowCasterIndex++)
						{
							ShadowCaster2D shadowCaster = shadowCasters[shadowCasterIndex];
							if (shadowCaster != null && shadowCaster.shadowCastingSource == ShadowCaster2D.ShadowCastingSources.ShapeProvider)
							{
								ShapeProviderUtility.CallOnBeforeRender(shadowCaster.shadowShape2DProvider, shadowCaster.shadowShape2DComponent, shadowCaster.m_ShadowMesh, bounds);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000198C1 File Offset: 0x00017AC1
		private static void CreateShadowRenderTexture(IRenderPass2D pass, RenderingData renderingData, CommandBuffer cmdBuffer, int shadowIndex)
		{
			ShadowRendering.CreateShadowRenderTexture(pass, ShadowRendering.m_RenderTargetIds[shadowIndex], renderingData, cmdBuffer);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000198D2 File Offset: 0x00017AD2
		internal static void PrerenderShadows(UnsafeCommandBuffer cmdBuffer, Renderer2DData rendererData, ref LayerBatch layer, Light2D light, int shadowIndex, float shadowIntensity)
		{
			ShadowRendering.RenderShadows(cmdBuffer, rendererData, ref layer, light);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000198E0 File Offset: 0x00017AE0
		internal static bool PrerenderShadows(this IRenderPass2D pass, RenderingData renderingData, CommandBuffer cmdBuffer, ref LayerBatch layer, Light2D light, int shadowIndex, float shadowIntensity)
		{
			ShadowRendering.CreateShadowRenderTexture(pass, renderingData, cmdBuffer, shadowIndex);
			bool flag = layer.shadowCasters.Count != 0;
			if (flag)
			{
				cmdBuffer.SetRenderTarget(ShadowRendering.m_RenderTargets[shadowIndex].nameID, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.DontCare);
				cmdBuffer.ClearRenderTarget(RTClearFlags.All, Color.clear, 1f, 0U);
				ShadowRendering.RenderShadows(CommandBufferHelpers.GetUnsafeCommandBuffer(cmdBuffer), pass.rendererData, ref layer, light);
			}
			ShadowRendering.m_LightInputTextures[shadowIndex] = ShadowRendering.m_RenderTargets[shadowIndex].nameID;
			return flag;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00019960 File Offset: 0x00017B60
		private static void CreateShadowRenderTexture(IRenderPass2D pass, int handleId, RenderingData renderingData, CommandBuffer cmdBuffer)
		{
			float renderTextureScale = Mathf.Clamp(pass.rendererData.lightRenderTextureScale, 0.01f, 1f);
			int width = (int)((float)renderingData.cameraData.cameraTargetDescriptor.width * renderTextureScale);
			int height = (int)((float)renderingData.cameraData.cameraTargetDescriptor.height * renderTextureScale);
			cmdBuffer.GetTemporaryRT(handleId, new RenderTextureDescriptor(width, height)
			{
				useMipMap = false,
				autoGenerateMips = false,
				depthStencilFormat = GraphicsFormatUtility.GetDepthStencilFormat(24),
				graphicsFormat = GraphicsFormat.B10G11R11_UFloatPack32,
				msaaSamples = 1,
				dimension = TextureDimension.Tex2D
			}, FilterMode.Bilinear);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000199FD File Offset: 0x00017BFD
		internal static void ReleaseShadowRenderTexture(CommandBuffer cmdBuffer, int shadowIndex)
		{
			cmdBuffer.ReleaseTemporaryRT(ShadowRendering.m_RenderTargetIds[shadowIndex]);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00019A0C File Offset: 0x00017C0C
		private static void SetShadowProjectionGlobals(UnsafeCommandBuffer cmdBuffer, ShadowCaster2D shadowCaster, Light2D light)
		{
			cmdBuffer.SetGlobalVector(ShadowRendering.k_ShadowModelScaleID, shadowCaster.m_CachedLossyScale);
			cmdBuffer.SetGlobalMatrix(ShadowRendering.k_ShadowModelMatrixID, shadowCaster.m_CachedShadowMatrix);
			cmdBuffer.SetGlobalMatrix(ShadowRendering.k_ShadowModelInvMatrixID, shadowCaster.m_CachedInverseShadowMatrix);
			cmdBuffer.SetGlobalFloat(ShadowRendering.k_ShadowSoftnessFalloffIntensityID, light.shadowSoftnessFalloffIntensity);
			if (shadowCaster.edgeProcessing == ShadowCaster2D.EdgeProcessing.None)
			{
				cmdBuffer.SetGlobalFloat(ShadowRendering.k_ShadowContractionDistanceID, shadowCaster.trimEdge);
				return;
			}
			cmdBuffer.SetGlobalFloat(ShadowRendering.k_ShadowContractionDistanceID, 0f);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00019A8C File Offset: 0x00017C8C
		internal static void SetGlobalShadowTexture(CommandBuffer cmdBuffer, Light2D light, int shadowIndex)
		{
			cmdBuffer.SetGlobalTexture("_ShadowTex", ShadowRendering.m_LightInputTextures[shadowIndex]);
			cmdBuffer.SetGlobalColor(ShadowRendering.k_ShadowShadowColorID, ShadowRendering.k_ShadowColorLookup);
			cmdBuffer.SetGlobalColor(ShadowRendering.k_ShadowUnshadowColorID, ShadowRendering.k_UnshadowColorLookup);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00019AD1 File Offset: 0x00017CD1
		internal static void SetGlobalShadowProp(IRasterCommandBuffer cmdBuffer)
		{
			cmdBuffer.SetGlobalColor(ShadowRendering.k_ShadowShadowColorID, ShadowRendering.k_ShadowColorLookup);
			cmdBuffer.SetGlobalColor(ShadowRendering.k_ShadowUnshadowColorID, ShadowRendering.k_UnshadowColorLookup);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00011A70 File Offset: 0x0000FC70
		private static bool ShadowCasterIsVisible(ShadowCaster2D shadowCaster)
		{
			return true;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00019AF4 File Offset: 0x00017CF4
		private static Renderer GetRendererFromCaster(ShadowCaster2D shadowCaster, Light2D light, int layerToRender)
		{
			Renderer renderer = null;
			if (shadowCaster.IsLit(light) && shadowCaster != null && shadowCaster.IsShadowedLayer(layerToRender))
			{
				shadowCaster.TryGetComponent<Renderer>(out renderer);
			}
			return renderer;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00019B28 File Offset: 0x00017D28
		private static void RenderProjectedShadows(UnsafeCommandBuffer cmdBuffer, int layerToRender, Light2D light, List<ShadowCaster2D> shadowCasters, Material projectedShadowsMaterial, int pass)
		{
			for (int i = 0; i < shadowCasters.Count; i++)
			{
				ShadowCaster2D shadowCaster = shadowCasters[i];
				if (ShadowRendering.ShadowCasterIsVisible(shadowCaster) && shadowCaster.castsShadows && shadowCaster.IsLit(light) && shadowCaster != null && projectedShadowsMaterial != null && shadowCaster.IsShadowedLayer(layerToRender) && shadowCaster.shadowCastingSource != ShadowCaster2D.ShadowCastingSources.None && shadowCaster.mesh != null)
				{
					ShadowRendering.SetShadowProjectionGlobals(cmdBuffer, shadowCaster, light);
					cmdBuffer.DrawMesh(shadowCaster.mesh, shadowCaster.transform.localToWorldMatrix, projectedShadowsMaterial, 0, pass);
				}
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00019BC0 File Offset: 0x00017DC0
		private static int GetRendererSubmeshes(Renderer renderer, ShadowCaster2D shadowCaster2D)
		{
			return shadowCaster2D.spriteMaterialCount;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00019BC8 File Offset: 0x00017DC8
		private static void RenderSelfShadowOption(UnsafeCommandBuffer cmdBuffer, int layerToRender, Light2D light, List<ShadowCaster2D> shadowCasters, Material projectedUnshadowMaterial, Material spriteShadowMaterial, Material spriteUnshadowMaterial, Material geometryShadowMaterial, Material geometryUnshadowMaterial)
		{
			for (int i = 0; i < shadowCasters.Count; i++)
			{
				ShadowCaster2D shadowCaster = shadowCasters[i];
				if (shadowCaster.IsLit(light))
				{
					Renderer renderer = ShadowRendering.GetRendererFromCaster(shadowCaster, light, layerToRender);
					cmdBuffer.SetGlobalFloat(ShadowRendering.k_ShadowAlphaCutoffID, shadowCaster.alphaCutoff);
					if (renderer != null)
					{
						if (ShadowRendering.ShadowCasterIsVisible(shadowCaster) && shadowCaster.selfShadows)
						{
							int numberOfSubmeshes = ShadowRendering.GetRendererSubmeshes(renderer, shadowCaster);
							for (int submeshIndex = 0; submeshIndex < numberOfSubmeshes; submeshIndex++)
							{
								cmdBuffer.DrawRenderer(renderer, spriteShadowMaterial, submeshIndex, 0);
							}
						}
						else
						{
							int numberOfSubmeshes2 = ShadowRendering.GetRendererSubmeshes(renderer, shadowCaster);
							for (int submeshIndex2 = 0; submeshIndex2 < numberOfSubmeshes2; submeshIndex2++)
							{
								cmdBuffer.DrawRenderer(renderer, spriteUnshadowMaterial, submeshIndex2, 0);
							}
						}
					}
					else if (shadowCaster.mesh != null)
					{
						if (ShadowRendering.ShadowCasterIsVisible(shadowCaster) && shadowCaster.selfShadows)
						{
							cmdBuffer.DrawMesh(shadowCaster.mesh, shadowCaster.transform.localToWorldMatrix, geometryShadowMaterial, 0, 0);
						}
						else
						{
							cmdBuffer.DrawMesh(shadowCaster.mesh, shadowCaster.transform.localToWorldMatrix, geometryUnshadowMaterial, 0, 0);
						}
					}
				}
			}
			for (int j = 0; j < shadowCasters.Count; j++)
			{
				ShadowCaster2D shadowCaster2 = shadowCasters[j];
				if (ShadowRendering.ShadowCasterIsVisible(shadowCaster2) && shadowCaster2.IsLit(light) && shadowCaster2.castingOption == ShadowCaster2D.ShadowCastingOptions.CastShadow && shadowCaster2.mesh != null)
				{
					ShadowRendering.SetShadowProjectionGlobals(cmdBuffer, shadowCaster2, light);
					cmdBuffer.DrawMesh(shadowCaster2.mesh, shadowCaster2.transform.localToWorldMatrix, projectedUnshadowMaterial, 0, 1);
				}
			}
			for (int k = 0; k < shadowCasters.Count; k++)
			{
				ShadowCaster2D shadowCaster3 = shadowCasters[k];
				if (ShadowRendering.ShadowCasterIsVisible(shadowCaster3) && !shadowCaster3.selfShadows && shadowCaster3.IsLit(light))
				{
					Renderer renderer2 = ShadowRendering.GetRendererFromCaster(shadowCaster3, light, layerToRender);
					if (renderer2 != null)
					{
						int numberOfSubmeshes3 = ShadowRendering.GetRendererSubmeshes(renderer2, shadowCaster3);
						for (int submeshIndex3 = 0; submeshIndex3 < numberOfSubmeshes3; submeshIndex3++)
						{
							cmdBuffer.DrawRenderer(renderer2, spriteUnshadowMaterial, submeshIndex3, 1);
						}
					}
					else if (shadowCaster3.mesh != null)
					{
						cmdBuffer.DrawMesh(shadowCaster3.mesh, shadowCaster3.transform.localToWorldMatrix, geometryUnshadowMaterial, 0, 1);
					}
				}
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00019DF8 File Offset: 0x00017FF8
		private static void RenderShadows(UnsafeCommandBuffer cmdBuffer, Renderer2DData rendererData, ref LayerBatch layer, Light2D light)
		{
			using (new ProfilingScope(cmdBuffer, ShadowRendering.m_ProfilingSamplerShadows))
			{
				float shadowRadius = light.boundingSphere.radius + (light.transform.position - light.boundingSphere.position).magnitude;
				cmdBuffer.SetGlobalVector(ShadowRendering.k_LightPosID, light.transform.position);
				cmdBuffer.SetGlobalFloat(ShadowRendering.k_ShadowRadiusID, shadowRadius);
				cmdBuffer.SetGlobalFloat(ShadowRendering.k_SoftShadowAngle, 0.017453292f * light.shadowSoftness * ShadowRendering.k_MaxShadowSoftnessAngle);
				Material projectedShadowMaterial = rendererData.GetProjectedShadowMaterial();
				Material projectedUnshadowMaterial = rendererData.GetProjectedUnshadowMaterial();
				Material spriteShadowMaterial = rendererData.GetSpriteShadowMaterial();
				Material spriteUnshadowMaterial = rendererData.GetSpriteUnshadowMaterial();
				Material geometryShadowMaterial = rendererData.GetGeometryShadowMaterial();
				Material geometryUnshadowMaterial = rendererData.GetGeometryUnshadowMaterial();
				for (int group = 0; group < layer.shadowCasters.Count; group++)
				{
					List<ShadowCaster2D> shadowCasters = layer.shadowCasters[group].GetShadowCasters();
					ShadowRendering.RenderProjectedShadows(cmdBuffer, layer.startLayerID, light, shadowCasters, projectedShadowMaterial, 0);
					ShadowRendering.RenderSelfShadowOption(cmdBuffer, layer.startLayerID, light, shadowCasters, projectedUnshadowMaterial, spriteShadowMaterial, spriteUnshadowMaterial, geometryShadowMaterial, geometryUnshadowMaterial);
				}
			}
		}

		// Token: 0x040002D1 RID: 721
		private static readonly int k_LightPosID = Shader.PropertyToID("_LightPos");

		// Token: 0x040002D2 RID: 722
		private static readonly int k_ShadowRadiusID = Shader.PropertyToID("_ShadowRadius");

		// Token: 0x040002D3 RID: 723
		private static readonly int k_ShadowColorMaskID = Shader.PropertyToID("_ShadowColorMask");

		// Token: 0x040002D4 RID: 724
		private static readonly int k_ShadowModelMatrixID = Shader.PropertyToID("_ShadowModelMatrix");

		// Token: 0x040002D5 RID: 725
		private static readonly int k_ShadowModelInvMatrixID = Shader.PropertyToID("_ShadowModelInvMatrix");

		// Token: 0x040002D6 RID: 726
		private static readonly int k_ShadowModelScaleID = Shader.PropertyToID("_ShadowModelScale");

		// Token: 0x040002D7 RID: 727
		private static readonly int k_ShadowContractionDistanceID = Shader.PropertyToID("_ShadowContractionDistance");

		// Token: 0x040002D8 RID: 728
		private static readonly int k_ShadowAlphaCutoffID = Shader.PropertyToID("_ShadowAlphaCutoff");

		// Token: 0x040002D9 RID: 729
		private static readonly int k_SoftShadowAngle = Shader.PropertyToID("_SoftShadowAngle");

		// Token: 0x040002DA RID: 730
		private static readonly int k_ShadowSoftnessFalloffIntensityID = Shader.PropertyToID("_ShadowSoftnessFalloffIntensity");

		// Token: 0x040002DB RID: 731
		private static readonly int k_ShadowShadowColorID = Shader.PropertyToID("_ShadowColor");

		// Token: 0x040002DC RID: 732
		private static readonly int k_ShadowUnshadowColorID = Shader.PropertyToID("_UnshadowColor");

		// Token: 0x040002DD RID: 733
		private static readonly ProfilingSampler m_ProfilingSamplerShadows = new ProfilingSampler("Draw 2D Shadow Texture");

		// Token: 0x040002DE RID: 734
		private static readonly ProfilingSampler m_ProfilingSamplerShadowsA = new ProfilingSampler("Draw 2D Shadows (A)");

		// Token: 0x040002DF RID: 735
		private static readonly ProfilingSampler m_ProfilingSamplerShadowsR = new ProfilingSampler("Draw 2D Shadows (R)");

		// Token: 0x040002E0 RID: 736
		private static readonly ProfilingSampler m_ProfilingSamplerShadowsG = new ProfilingSampler("Draw 2D Shadows (G)");

		// Token: 0x040002E1 RID: 737
		private static readonly ProfilingSampler m_ProfilingSamplerShadowsB = new ProfilingSampler("Draw 2D Shadows (B)");

		// Token: 0x040002E2 RID: 738
		private static readonly float k_MaxShadowSoftnessAngle = 15f;

		// Token: 0x040002E3 RID: 739
		private static readonly Color k_ShadowColorLookup = new Color(0f, 0f, 1f, 0f);

		// Token: 0x040002E4 RID: 740
		private static readonly Color k_UnshadowColorLookup = new Color(0f, 1f, 0f, 0f);

		// Token: 0x040002E5 RID: 741
		private static RTHandle[] m_RenderTargets = null;

		// Token: 0x040002E6 RID: 742
		private static int[] m_RenderTargetIds = null;

		// Token: 0x040002E7 RID: 743
		private static RenderTargetIdentifier[] m_LightInputTextures = null;

		// Token: 0x040002E8 RID: 744
		private static readonly ProfilingSampler[] m_ProfilingSamplerShadowColorsLookup = new ProfilingSampler[]
		{
			ShadowRendering.m_ProfilingSamplerShadowsA,
			ShadowRendering.m_ProfilingSamplerShadowsB,
			ShadowRendering.m_ProfilingSamplerShadowsG,
			ShadowRendering.m_ProfilingSamplerShadowsR
		};
	}
}
