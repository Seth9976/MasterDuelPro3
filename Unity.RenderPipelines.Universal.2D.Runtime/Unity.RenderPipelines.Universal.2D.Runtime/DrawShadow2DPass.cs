using System;
using System.Collections.Generic;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000062 RID: 98
	internal class DrawShadow2DPass : ScriptableRenderPass
	{
		// Token: 0x06000285 RID: 645 RVA: 0x0001324C File Offset: 0x0001144C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0001459C File Offset: 0x0001279C
		private static void ExecuteShadowPass(UnsafeCommandBuffer cmd, DrawLight2DPass.PassData passData, Light2D light)
		{
			using (new ProfilingScope(cmd, DrawShadow2DPass.m_ExecuteProfilingSampler))
			{
				cmd.SetRenderTarget(passData.shadowMap, passData.shadowDepth);
				cmd.ClearRenderTarget(RTClearFlags.All, Color.clear, 1f, 0U);
				passData.rendererData.GetProjectedShadowMaterial();
				passData.rendererData.GetProjectedUnshadowMaterial();
				ShadowRendering.PrerenderShadows(cmd, passData.rendererData, ref passData.layerBatch, light, 0, light.shadowIntensity);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00014638 File Offset: 0x00012838
		public void Render(RenderGraph graph, ContextContainer frameData, Renderer2DData rendererData, ref LayerBatch layerBatch, int batchIndex, bool isVolumetric = false)
		{
			Universal2DResourceData universal2DResourceData = frameData.Get<Universal2DResourceData>();
			UniversalResourceData commonResourceData = frameData.Get<UniversalResourceData>();
			if (!layerBatch.lightStats.useShadows || (isVolumetric && !layerBatch.lightStats.useVolumetricShadowLights))
			{
				return;
			}
			TextureHandle shadowTexture = universal2DResourceData.shadowsTexture;
			TextureHandle depthTexture = universal2DResourceData.shadowsDepth;
			DrawLight2DPass.PassData passData;
			using (IUnsafeRenderGraphBuilder builder = graph.AddUnsafePass<DrawLight2DPass.PassData>((!isVolumetric) ? DrawShadow2DPass.k_ShadowPass : DrawShadow2DPass.k_ShadowVolumetricPass, out passData, (!isVolumetric) ? DrawShadow2DPass.m_ProfilingSampler : DrawShadow2DPass.m_ProfilingSamplerVolume, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/2D/Rendergraph/DrawShadow2DPass.cs", 54))
			{
				passData.layerBatch = layerBatch;
				passData.rendererData = rendererData;
				passData.isVolumetric = isVolumetric;
				passData.shadowMap = shadowTexture;
				passData.shadowDepth = depthTexture;
				passData.normalMap = (layerBatch.lightStats.useNormalMap ? universal2DResourceData.normalsTexture[batchIndex] : TextureHandle.nullHandle);
				if (!isVolumetric)
				{
					passData.lightTextures = universal2DResourceData.lightTextures[batchIndex];
					passData.depthTexture = universal2DResourceData.intermediateDepth;
					builder.UseTexture(in passData.depthTexture, AccessFlags.Write);
				}
				else
				{
					this.intermediateTexture[0] = commonResourceData.activeColorTexture;
					passData.lightTextures = this.intermediateTexture;
				}
				if (passData.lightTexturesRT == null || passData.lightTexturesRT.Length != passData.lightTextures.Length)
				{
					passData.lightTexturesRT = new RenderTargetIdentifier[passData.lightTextures.Length];
				}
				for (int i = 0; i < passData.lightTextures.Length; i++)
				{
					builder.UseTexture(in passData.lightTextures[i], AccessFlags.Write);
				}
				if (layerBatch.lightStats.useNormalMap)
				{
					builder.UseTexture(in universal2DResourceData.normalsTexture[batchIndex], AccessFlags.Read);
				}
				builder.UseTexture(in shadowTexture, AccessFlags.Write);
				builder.UseTexture(in depthTexture, AccessFlags.Write);
				foreach (Light2D light in layerBatch.shadowLights)
				{
					if (!(light == null) && light.m_CookieSpriteTextureHandle.IsValid() && (!isVolumetric || (isVolumetric && light.volumetricEnabled)))
					{
						builder.UseTexture(in light.m_CookieSpriteTextureHandle, AccessFlags.Read);
					}
				}
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				builder.SetRenderFunc<DrawLight2DPass.PassData>(delegate(DrawLight2DPass.PassData data, UnsafeGraphContext context)
				{
					for (int j = 0; j < data.layerBatch.shadowLights.Count; j++)
					{
						DrawShadow2DPass.intermediateLight.Clear();
						DrawShadow2DPass.intermediateLight.Add(data.layerBatch.shadowLights[j]);
						UnsafeCommandBuffer cmd = context.cmd;
						DrawShadow2DPass.ExecuteShadowPass(cmd, data, DrawShadow2DPass.intermediateLight[0]);
						if (Renderer2D.supportsMRT && !data.isVolumetric)
						{
							for (int k = 0; k < data.lightTextures.Length; k++)
							{
								data.lightTexturesRT[k] = data.lightTextures[k];
							}
							cmd.SetRenderTarget(data.lightTexturesRT, data.depthTexture);
						}
						else
						{
							cmd.SetRenderTarget(data.lightTextures[0]);
						}
						using (new ProfilingScope(cmd, DrawLight2DPass.m_ProfilingSamplerLowLevel))
						{
							DrawLight2DPass.ExecuteUnsafe(cmd, data, ref data.layerBatch, DrawShadow2DPass.intermediateLight, true);
						}
					}
				});
			}
		}

		// Token: 0x04000241 RID: 577
		private static readonly string k_ShadowPass = "Shadow2D UnsafePass";

		// Token: 0x04000242 RID: 578
		private static readonly string k_ShadowVolumetricPass = "Shadow2D Volumetric UnsafePass";

		// Token: 0x04000243 RID: 579
		private static readonly ProfilingSampler m_ProfilingSampler = new ProfilingSampler(DrawShadow2DPass.k_ShadowPass);

		// Token: 0x04000244 RID: 580
		private static readonly ProfilingSampler m_ProfilingSamplerVolume = new ProfilingSampler(DrawShadow2DPass.k_ShadowVolumetricPass);

		// Token: 0x04000245 RID: 581
		private static readonly ProfilingSampler m_ExecuteProfilingSampler = new ProfilingSampler("Draw Shadow");

		// Token: 0x04000246 RID: 582
		private static readonly ProfilingSampler m_ExecuteLightProfilingSampler = new ProfilingSampler("Draw Light");

		// Token: 0x04000247 RID: 583
		private TextureHandle[] intermediateTexture = new TextureHandle[1];

		// Token: 0x04000248 RID: 584
		private static List<Light2D> intermediateLight = new List<Light2D>(1);
	}
}
