using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000064 RID: 100
	internal class GlobalPropertiesPass : ScriptableRenderPass
	{
		// Token: 0x0600028D RID: 653 RVA: 0x00014A7C File Offset: 0x00012C7C
		internal static void Setup(RenderGraph graph, UniversalCameraData cameraData)
		{
			GlobalPropertiesPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = graph.AddRasterRenderPass<GlobalPropertiesPass.PassData>(GlobalPropertiesPass.k_SetGlobalProperties, out passData, GlobalPropertiesPass.m_SetGlobalPropertiesProfilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/2D/Rendergraph/GlobalPropertiesPass.cs", 21))
			{
				passData.screenParams = Vector2Int.zero;
				PixelPerfectCamera pixelPerfectCamera;
				cameraData.camera.TryGetComponent<PixelPerfectCamera>(out pixelPerfectCamera);
				if (pixelPerfectCamera != null && pixelPerfectCamera.enabled && pixelPerfectCamera.offscreenRTSize != Vector2Int.zero)
				{
					passData.screenParams = pixelPerfectCamera.offscreenRTSize;
				}
				TextureHandle lightLookupTexture = graph.ImportTexture(Light2DLookupTexture.GetLightLookupTexture_Rendergraph());
				TextureHandle fallOffTexture = graph.ImportTexture(Light2DLookupTexture.GetFallOffLookupTexture_Rendergraph());
				builder.SetGlobalTextureAfterPass(in lightLookupTexture, Light2DLookupTexture.k_LightLookupID);
				builder.SetGlobalTextureAfterPass(in fallOffTexture, Light2DLookupTexture.k_FalloffLookupID);
				builder.AllowPassCulling(false);
				builder.AllowGlobalStateModification(true);
				builder.SetRenderFunc<GlobalPropertiesPass.PassData>(delegate(GlobalPropertiesPass.PassData data, RasterGraphContext context)
				{
					if (data.screenParams != Vector2Int.zero)
					{
						int cameraWidth = data.screenParams.x;
						int cameraHeight = data.screenParams.y;
						context.cmd.SetGlobalVector(ShaderPropertyId.screenParams, new Vector4((float)cameraWidth, (float)cameraHeight, 1f + 1f / (float)cameraWidth, 1f + 1f / (float)cameraHeight));
					}
				});
			}
		}

		// Token: 0x0400024B RID: 587
		private static readonly string k_SetGlobalProperties = "SetGlobalProperties";

		// Token: 0x0400024C RID: 588
		private static readonly ProfilingSampler m_SetGlobalPropertiesProfilingSampler = new ProfilingSampler(GlobalPropertiesPass.k_SetGlobalProperties);

		// Token: 0x02000065 RID: 101
		private class PassData
		{
			// Token: 0x0400024D RID: 589
			internal Vector2Int screenParams;
		}
	}
}
