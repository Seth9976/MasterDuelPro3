using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001E7 RID: 487
	internal class ClearTargetsPass
	{
		// Token: 0x06000ACF RID: 2767 RVA: 0x00039094 File Offset: 0x00037294
		internal static void Render(RenderGraph graph, TextureHandle colorHandle, TextureHandle depthHandle, UniversalCameraData cameraData)
		{
			RTClearFlags clearFlags = RTClearFlags.None;
			if (cameraData.renderType == CameraRenderType.Base)
			{
				clearFlags = RTClearFlags.All;
			}
			else if (cameraData.clearDepth)
			{
				clearFlags = RTClearFlags.Depth;
			}
			if (clearFlags != RTClearFlags.None)
			{
				ClearTargetsPass.Render(graph, colorHandle, depthHandle, clearFlags, cameraData.backgroundColor);
			}
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000390CC File Offset: 0x000372CC
		internal static void Render(RenderGraph graph, TextureHandle colorHandle, TextureHandle depthHandle, RTClearFlags clearFlags, Color clearColor)
		{
			ClearTargetsPass.PassData passData;
			using (IRasterRenderGraphBuilder builder = graph.AddRasterRenderPass<ClearTargetsPass.PassData>("Clear Targets Pass", out passData, ClearTargetsPass.s_ClearProfilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/UniversalRendererRenderGraph.cs", 1898))
			{
				if (colorHandle.IsValid())
				{
					passData.color = colorHandle;
					builder.SetRenderAttachment(colorHandle, 0, AccessFlags.Write);
				}
				if (depthHandle.IsValid())
				{
					passData.depth = depthHandle;
					builder.SetRenderAttachmentDepth(depthHandle, AccessFlags.Write);
				}
				passData.clearFlags = clearFlags;
				passData.clearColor = clearColor;
				builder.AllowPassCulling(false);
				builder.SetRenderFunc<ClearTargetsPass.PassData>(delegate(ClearTargetsPass.PassData data, RasterGraphContext context)
				{
					context.cmd.ClearRenderTarget(data.clearFlags, data.clearColor, 1f, 0U);
				});
			}
		}

		// Token: 0x04000C01 RID: 3073
		private static ProfilingSampler s_ClearProfilingSampler = new ProfilingSampler("Clear Targets");

		// Token: 0x020001E8 RID: 488
		private class PassData
		{
			// Token: 0x04000C02 RID: 3074
			internal TextureHandle color;

			// Token: 0x04000C03 RID: 3075
			internal TextureHandle depth;

			// Token: 0x04000C04 RID: 3076
			internal RTClearFlags clearFlags;

			// Token: 0x04000C05 RID: 3077
			internal Color clearColor;
		}
	}
}
