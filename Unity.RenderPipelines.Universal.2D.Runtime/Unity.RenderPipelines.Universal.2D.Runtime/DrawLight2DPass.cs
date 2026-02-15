using System;
using System.Collections.Generic;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000058 RID: 88
	internal class DrawLight2DPass : ScriptableRenderPass
	{
		// Token: 0x06000264 RID: 612 RVA: 0x00013470 File Offset: 0x00011670
		public void Setup(RenderGraph renderGraph, ref Renderer2DData rendererData)
		{
			foreach (Light2D light in rendererData.lightCullResult.visibleLights)
			{
				if (light.useCookieSprite && light.m_CookieSpriteTexture != null)
				{
					light.m_CookieSpriteTextureHandle = renderGraph.ImportTexture(light.m_CookieSpriteTexture);
				}
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0001324C File Offset: 0x0001144C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000134E4 File Offset: 0x000116E4
		private static void Execute(RasterCommandBuffer cmd, DrawLight2DPass.PassData passData, ref LayerBatch layerBatch)
		{
			cmd.SetGlobalFloat(DrawLight2DPass.k_InverseHDREmulationScaleID, 1f / passData.rendererData.hdrEmulationScale);
			for (int i = 0; i < layerBatch.activeBlendStylesIndices.Length; i++)
			{
				int blendStyleIndex = layerBatch.activeBlendStylesIndices[i];
				string blendOpName = passData.rendererData.lightBlendStyles[blendStyleIndex].name;
				cmd.BeginSample(blendOpName);
				if (!passData.isVolumetric)
				{
					RendererLighting.EnableBlendStyle(cmd, i, true);
				}
				List<Light2D> lights = passData.layerBatch.lights;
				for (int j = 0; j < lights.Count; j++)
				{
					Light2D light = lights[j];
					if (!(light == null) && light.lightType != Light2D.LightType.Global && light.blendStyleIndex == blendStyleIndex && (!passData.isVolumetric || (light.volumeIntensity > 0f && light.volumetricEnabled && layerBatch.endLayerValue == light.GetTopMostLitLayer())))
					{
						Material lightMaterial = passData.rendererData.GetLightMaterial(light, passData.isVolumetric);
						Mesh lightMesh = light.lightMesh;
						int index = light.batchSlotIndex;
						int slotIndex = RendererLighting.lightBatch.SlotIndex(index);
						int lightHash;
						if (!RendererLighting.lightBatch.CanBatch(light, lightMaterial, index, out lightHash) && LightBatch.isBatchingSupported)
						{
							RendererLighting.lightBatch.Flush(cmd);
						}
						if (passData.layerBatch.lightStats.useNormalMap)
						{
							DrawLight2DPass.s_PropertyBlock.SetTexture(DrawLight2DPass.k_NormalMapID, passData.normalMap);
						}
						if (passData.layerBatch.lightStats.useShadows)
						{
							DrawLight2DPass.s_PropertyBlock.SetTexture(DrawLight2DPass.k_ShadowMapID, passData.shadowMap);
						}
						if (!passData.isVolumetric || (passData.isVolumetric && light.volumetricEnabled))
						{
							RendererLighting.SetCookieShaderProperties(light, DrawLight2DPass.s_PropertyBlock);
						}
						RendererLighting.SetPerLightShaderGlobals(cmd, light, slotIndex, passData.isVolumetric, false, LightBatch.isBatchingSupported);
						if (light.normalMapQuality != Light2D.NormalMapQuality.Disabled || light.lightType == Light2D.LightType.Point)
						{
							RendererLighting.SetPerPointLightShaderGlobals(cmd, light, slotIndex, LightBatch.isBatchingSupported);
						}
						if (LightBatch.isBatchingSupported)
						{
							RendererLighting.lightBatch.AddBatch(light, lightMaterial, light.GetMatrix(), lightMesh, 0, lightHash, index);
							RendererLighting.lightBatch.Flush(cmd);
						}
						else
						{
							cmd.DrawMesh(lightMesh, light.GetMatrix(), lightMaterial, 0, 0, DrawLight2DPass.s_PropertyBlock);
						}
					}
				}
				RendererLighting.EnableBlendStyle(cmd, i, false);
				cmd.EndSample(blendOpName);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00013758 File Offset: 0x00011958
		internal static void ExecuteUnsafe(UnsafeCommandBuffer cmd, DrawLight2DPass.PassData passData, ref LayerBatch layerBatch, List<Light2D> lights, bool useShadows = false)
		{
			cmd.SetGlobalFloat(DrawLight2DPass.k_InverseHDREmulationScaleID, 1f / passData.rendererData.hdrEmulationScale);
			for (int i = 0; i < layerBatch.activeBlendStylesIndices.Length; i++)
			{
				int blendStyleIndex = layerBatch.activeBlendStylesIndices[i];
				string blendOpName = passData.rendererData.lightBlendStyles[blendStyleIndex].name;
				cmd.BeginSample(blendOpName);
				if (!Renderer2D.supportsMRT && !passData.isVolumetric)
				{
					cmd.SetRenderTarget(passData.lightTextures[i], passData.depthTexture);
				}
				int indicesIndex = (Renderer2D.supportsMRT ? i : 0);
				if (!passData.isVolumetric)
				{
					RendererLighting.EnableBlendStyle(cmd, indicesIndex, true);
				}
				for (int j = 0; j < lights.Count; j++)
				{
					Light2D light = lights[j];
					if (!(light == null) && light.lightType != Light2D.LightType.Global && light.blendStyleIndex == blendStyleIndex && (!passData.isVolumetric || (light.volumeIntensity > 0f && light.volumetricEnabled && layerBatch.endLayerValue == light.GetTopMostLitLayer())))
					{
						Material lightMaterial = passData.rendererData.GetLightMaterial(light, passData.isVolumetric);
						Mesh lightMesh = light.lightMesh;
						int index = light.batchSlotIndex;
						int slotIndex = RendererLighting.lightBatch.SlotIndex(index);
						int lightHash;
						RendererLighting.lightBatch.CanBatch(light, lightMaterial, index, out lightHash);
						if (passData.layerBatch.lightStats.useNormalMap)
						{
							DrawLight2DPass.s_PropertyBlock.SetTexture(DrawLight2DPass.k_NormalMapID, passData.normalMap);
						}
						if (passData.layerBatch.lightStats.useShadows)
						{
							DrawLight2DPass.s_PropertyBlock.SetTexture(DrawLight2DPass.k_ShadowMapID, passData.shadowMap);
						}
						if (!passData.isVolumetric || (passData.isVolumetric && light.volumetricEnabled))
						{
							RendererLighting.SetCookieShaderProperties(light, DrawLight2DPass.s_PropertyBlock);
						}
						RendererLighting.SetPerLightShaderGlobals(cmd, light, slotIndex, passData.isVolumetric, useShadows, LightBatch.isBatchingSupported);
						if (light.normalMapQuality != Light2D.NormalMapQuality.Disabled || light.lightType == Light2D.LightType.Point)
						{
							RendererLighting.SetPerPointLightShaderGlobals(cmd, light, slotIndex, LightBatch.isBatchingSupported);
						}
						if (!LightBatch.isBatchingSupported)
						{
							cmd.DrawMesh(lightMesh, light.GetMatrix(), lightMaterial, 0, 0, DrawLight2DPass.s_PropertyBlock);
						}
					}
				}
				RendererLighting.EnableBlendStyle(cmd, indicesIndex, false);
				cmd.EndSample(blendOpName);
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000139C0 File Offset: 0x00011BC0
		public void Render(RenderGraph graph, ContextContainer frameData, Renderer2DData rendererData, ref LayerBatch layerBatch, int batchIndex, bool isVolumetric = false)
		{
			Universal2DResourceData universal2DResourceData = frameData.Get<Universal2DResourceData>();
			UniversalResourceData commonResourceData = frameData.Get<UniversalResourceData>();
			if (!layerBatch.lightStats.useLights || (isVolumetric && !layerBatch.lightStats.useVolumetricLights))
			{
				return;
			}
			if (!isVolumetric && Renderer2D.IsGLDevice())
			{
				DrawLight2DPass.PassData passData;
				using (IUnsafeRenderGraphBuilder builder = graph.AddUnsafePass<DrawLight2DPass.PassData>(DrawLight2DPass.k_LightLowLevelPass, out passData, DrawLight2DPass.m_ProfilingSamplerLowLevel, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/2D/Rendergraph/DrawLight2DPass.cs", 221))
				{
					this.intermediateTexture[0] = commonResourceData.activeColorTexture;
					passData.lightTextures = universal2DResourceData.lightTextures[batchIndex];
					passData.depthTexture = universal2DResourceData.intermediateDepth;
					for (int i = 0; i < passData.lightTextures.Length; i++)
					{
						builder.UseTexture(in passData.lightTextures[i], AccessFlags.Write);
					}
					builder.UseTexture(in passData.depthTexture, AccessFlags.Write);
					if (layerBatch.lightStats.useNormalMap)
					{
						builder.UseTexture(in universal2DResourceData.normalsTexture[batchIndex], AccessFlags.Read);
					}
					if (layerBatch.lightStats.useShadows)
					{
						IBaseRenderGraphBuilder baseRenderGraphBuilder = builder;
						TextureHandle textureHandle = universal2DResourceData.shadowsTexture;
						baseRenderGraphBuilder.UseTexture(in textureHandle, AccessFlags.Read);
					}
					foreach (Light2D light in layerBatch.lights)
					{
						if (!(light == null) && light.m_CookieSpriteTextureHandle.IsValid() && (!isVolumetric || (isVolumetric && light.volumetricEnabled)))
						{
							builder.UseTexture(in light.m_CookieSpriteTextureHandle, AccessFlags.Read);
						}
					}
					passData.layerBatch = layerBatch;
					passData.rendererData = rendererData;
					passData.isVolumetric = isVolumetric;
					passData.normalMap = (layerBatch.lightStats.useNormalMap ? universal2DResourceData.normalsTexture[batchIndex] : TextureHandle.nullHandle);
					passData.shadowMap = (layerBatch.lightStats.useShadows ? universal2DResourceData.shadowsTexture : TextureHandle.nullHandle);
					builder.AllowPassCulling(false);
					builder.AllowGlobalStateModification(true);
					builder.SetRenderFunc<DrawLight2DPass.PassData>(delegate(DrawLight2DPass.PassData data, UnsafeGraphContext context)
					{
						DrawLight2DPass.ExecuteUnsafe(context.cmd, data, ref data.layerBatch, data.layerBatch.lights, false);
					});
					return;
				}
			}
			DrawLight2DPass.PassData passData2;
			using (IRasterRenderGraphBuilder builder2 = graph.AddRasterRenderPass<DrawLight2DPass.PassData>((!isVolumetric) ? DrawLight2DPass.k_LightPass : DrawLight2DPass.k_LightVolumetricPass, out passData2, (!isVolumetric) ? DrawLight2DPass.m_ProfilingSampler : DrawLight2DPass.m_ProfilingSamplerVolume, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/2D/Rendergraph/DrawLight2DPass.cs", 265))
			{
				this.intermediateTexture[0] = commonResourceData.activeColorTexture;
				TextureHandle[] lightTextures = ((!isVolumetric) ? universal2DResourceData.lightTextures[batchIndex] : this.intermediateTexture);
				TextureHandle depthTexture = ((!isVolumetric) ? universal2DResourceData.intermediateDepth : commonResourceData.activeDepthTexture);
				for (int j = 0; j < lightTextures.Length; j++)
				{
					builder2.SetRenderAttachment(lightTextures[j], j, AccessFlags.Write);
				}
				builder2.SetRenderAttachmentDepth(depthTexture, AccessFlags.Write);
				if (layerBatch.lightStats.useNormalMap)
				{
					builder2.UseTexture(in universal2DResourceData.normalsTexture[batchIndex], AccessFlags.Read);
				}
				if (layerBatch.lightStats.useShadows)
				{
					IBaseRenderGraphBuilder baseRenderGraphBuilder2 = builder2;
					TextureHandle textureHandle = universal2DResourceData.shadowsTexture;
					baseRenderGraphBuilder2.UseTexture(in textureHandle, AccessFlags.Read);
				}
				foreach (Light2D light2 in layerBatch.lights)
				{
					if (!(light2 == null) && light2.m_CookieSpriteTextureHandle.IsValid() && (!isVolumetric || (isVolumetric && light2.volumetricEnabled)))
					{
						builder2.UseTexture(in light2.m_CookieSpriteTextureHandle, AccessFlags.Read);
					}
				}
				passData2.layerBatch = layerBatch;
				passData2.rendererData = rendererData;
				passData2.isVolumetric = isVolumetric;
				passData2.normalMap = (layerBatch.lightStats.useNormalMap ? universal2DResourceData.normalsTexture[batchIndex] : TextureHandle.nullHandle);
				passData2.shadowMap = (layerBatch.lightStats.useShadows ? universal2DResourceData.shadowsTexture : TextureHandle.nullHandle);
				builder2.AllowPassCulling(false);
				builder2.AllowGlobalStateModification(true);
				builder2.SetRenderFunc<DrawLight2DPass.PassData>(delegate(DrawLight2DPass.PassData data, RasterGraphContext context)
				{
					DrawLight2DPass.Execute(context.cmd, data, ref data.layerBatch);
				});
			}
		}

		// Token: 0x04000210 RID: 528
		private static readonly string k_LightPass = "Light2D Pass";

		// Token: 0x04000211 RID: 529
		private static readonly string k_LightLowLevelPass = "Light2D LowLevelPass";

		// Token: 0x04000212 RID: 530
		private static readonly string k_LightVolumetricPass = "Light2D Volumetric Pass";

		// Token: 0x04000213 RID: 531
		private static readonly ProfilingSampler m_ProfilingSampler = new ProfilingSampler(DrawLight2DPass.k_LightPass);

		// Token: 0x04000214 RID: 532
		internal static readonly ProfilingSampler m_ProfilingSamplerLowLevel = new ProfilingSampler(DrawLight2DPass.k_LightLowLevelPass);

		// Token: 0x04000215 RID: 533
		private static readonly ProfilingSampler m_ProfilingSamplerVolume = new ProfilingSampler(DrawLight2DPass.k_LightVolumetricPass);

		// Token: 0x04000216 RID: 534
		internal static readonly int k_InverseHDREmulationScaleID = Shader.PropertyToID("_InverseHDREmulationScale");

		// Token: 0x04000217 RID: 535
		internal static readonly string k_NormalMapID = "_NormalMap";

		// Token: 0x04000218 RID: 536
		internal static readonly string k_ShadowMapID = "_ShadowTex";

		// Token: 0x04000219 RID: 537
		private TextureHandle[] intermediateTexture = new TextureHandle[1];

		// Token: 0x0400021A RID: 538
		internal static MaterialPropertyBlock s_PropertyBlock = new MaterialPropertyBlock();

		// Token: 0x02000059 RID: 89
		internal class PassData
		{
			// Token: 0x0400021B RID: 539
			internal LayerBatch layerBatch;

			// Token: 0x0400021C RID: 540
			internal Renderer2DData rendererData;

			// Token: 0x0400021D RID: 541
			internal bool isVolumetric;

			// Token: 0x0400021E RID: 542
			internal TextureHandle normalMap;

			// Token: 0x0400021F RID: 543
			internal TextureHandle shadowMap;

			// Token: 0x04000220 RID: 544
			internal RenderTargetIdentifier[] lightTexturesRT;

			// Token: 0x04000221 RID: 545
			internal TextureHandle[] lightTextures;

			// Token: 0x04000222 RID: 546
			internal TextureHandle depthTexture;

			// Token: 0x04000223 RID: 547
			internal TextureHandle shadowDepth;
		}
	}
}
