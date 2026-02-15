using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000180 RID: 384
	internal static class RenderingLayerUtils
	{
		// Token: 0x06000808 RID: 2056 RVA: 0x00026595 File Offset: 0x00024795
		public static void CombineRendererEvents(bool isDeferred, int msaaSampleCount, RenderingLayerUtils.Event rendererEvent, ref RenderingLayerUtils.Event combinedEvent)
		{
			if (msaaSampleCount > 1 && !isDeferred)
			{
				combinedEvent = RenderingLayerUtils.Event.DepthNormalPrePass;
				return;
			}
			combinedEvent = RenderingLayerUtils.Combine(combinedEvent, rendererEvent);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x000265AC File Offset: 0x000247AC
		public static bool RequireRenderingLayers(UniversalRenderer universalRenderer, List<ScriptableRendererFeature> rendererFeatures, int msaaSampleCount, out RenderingLayerUtils.Event combinedEvent, out RenderingLayerUtils.MaskSize combinedMaskSize)
		{
			RenderingMode renderingMode = universalRenderer.renderingModeActual;
			bool accurateGBufferNormals = universalRenderer.accurateGbufferNormals;
			return RenderingLayerUtils.RequireRenderingLayers(rendererFeatures, renderingMode, accurateGBufferNormals, msaaSampleCount, out combinedEvent, out combinedMaskSize);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000265D4 File Offset: 0x000247D4
		internal static bool RequireRenderingLayers(List<ScriptableRendererFeature> rendererFeatures, RenderingMode renderingMode, bool accurateGbufferNormals, int msaaSampleCount, out RenderingLayerUtils.Event combinedEvent, out RenderingLayerUtils.MaskSize combinedMaskSize)
		{
			combinedEvent = RenderingLayerUtils.Event.Opaque;
			combinedMaskSize = RenderingLayerUtils.MaskSize.Bits8;
			bool isDeferred = renderingMode == RenderingMode.Deferred;
			bool result = false;
			foreach (ScriptableRendererFeature rendererFeature in rendererFeatures)
			{
				if (rendererFeature.isActive)
				{
					RenderingLayerUtils.Event rendererEvent;
					RenderingLayerUtils.MaskSize rendererMaskSize;
					result |= rendererFeature.RequireRenderingLayers(isDeferred, accurateGbufferNormals, out rendererEvent, out rendererMaskSize);
					combinedEvent = RenderingLayerUtils.Combine(combinedEvent, rendererEvent);
					combinedMaskSize = RenderingLayerUtils.Combine(combinedMaskSize, rendererMaskSize);
				}
			}
			if (msaaSampleCount > 1 && combinedEvent == RenderingLayerUtils.Event.Opaque)
			{
				combinedEvent = RenderingLayerUtils.Event.DepthNormalPrePass;
			}
			if (RenderPipelineGlobalSettings<UniversalRenderPipelineGlobalSettings, UniversalRenderPipeline>.instance)
			{
				RenderingLayerUtils.MaskSize maskSize = RenderingLayerUtils.GetMaskSize(RenderingLayerMask.GetRenderingLayerCount());
				combinedMaskSize = RenderingLayerUtils.Combine(combinedMaskSize, maskSize);
			}
			return result;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00026690 File Offset: 0x00024890
		public static void SetupProperties(CommandBuffer cmd, RenderingLayerUtils.MaskSize maskSize)
		{
			RenderingLayerUtils.SetupProperties(CommandBufferHelpers.GetRasterCommandBuffer(cmd), maskSize);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x000266A0 File Offset: 0x000248A0
		internal static void SetupProperties(RasterCommandBuffer cmd, RenderingLayerUtils.MaskSize maskSize)
		{
			int bits = RenderingLayerUtils.GetBits(maskSize);
			uint maxInt = ((bits != 32) ? ((1U << bits) - 1U) : uint.MaxValue);
			float rcpMaxInt = math.rcp(maxInt);
			cmd.SetGlobalInt(ShaderPropertyId.renderingLayerMaxInt, (int)maxInt);
			cmd.SetGlobalFloat(ShaderPropertyId.renderingLayerRcpMaxInt, rcpMaxInt);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000266E6 File Offset: 0x000248E6
		public static GraphicsFormat GetFormat(RenderingLayerUtils.MaskSize maskSize)
		{
			switch (maskSize)
			{
			case RenderingLayerUtils.MaskSize.Bits8:
				return GraphicsFormat.R8_UNorm;
			case RenderingLayerUtils.MaskSize.Bits16:
				if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.WebGPU)
				{
					return GraphicsFormat.R32_SFloat;
				}
				return GraphicsFormat.R16_UNorm;
			case RenderingLayerUtils.MaskSize.Bits24:
			case RenderingLayerUtils.MaskSize.Bits32:
				return GraphicsFormat.R32_SFloat;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00026719 File Offset: 0x00024919
		public static uint ToValidRenderingLayers(uint renderingLayers)
		{
			if (RenderPipelineGlobalSettings<UniversalRenderPipelineGlobalSettings, UniversalRenderPipeline>.instance)
			{
				return RenderingLayerMask.GetDefinedRenderingLayersCombinedMaskValue() & renderingLayers;
			}
			return renderingLayers;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00026730 File Offset: 0x00024930
		private static RenderingLayerUtils.MaskSize GetMaskSize(int bits)
		{
			switch ((bits + 7) / 8)
			{
			case 0:
				return RenderingLayerUtils.MaskSize.Bits8;
			case 1:
				return RenderingLayerUtils.MaskSize.Bits8;
			case 2:
				return RenderingLayerUtils.MaskSize.Bits16;
			case 3:
				return RenderingLayerUtils.MaskSize.Bits24;
			case 4:
				return RenderingLayerUtils.MaskSize.Bits32;
			default:
				return RenderingLayerUtils.MaskSize.Bits32;
			}
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0002676A File Offset: 0x0002496A
		private static int GetBits(RenderingLayerUtils.MaskSize maskSize)
		{
			switch (maskSize)
			{
			case RenderingLayerUtils.MaskSize.Bits8:
				return 8;
			case RenderingLayerUtils.MaskSize.Bits16:
				return 16;
			case RenderingLayerUtils.MaskSize.Bits24:
				return 24;
			case RenderingLayerUtils.MaskSize.Bits32:
				return 32;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00026794 File Offset: 0x00024994
		private static RenderingLayerUtils.Event Combine(RenderingLayerUtils.Event a, RenderingLayerUtils.Event b)
		{
			return (RenderingLayerUtils.Event)Mathf.Min((int)a, (int)b);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0002679D File Offset: 0x0002499D
		private static RenderingLayerUtils.MaskSize Combine(RenderingLayerUtils.MaskSize a, RenderingLayerUtils.MaskSize b)
		{
			return (RenderingLayerUtils.MaskSize)Mathf.Max((int)a, (int)b);
		}

		// Token: 0x02000181 RID: 385
		public enum Event
		{
			// Token: 0x040008AF RID: 2223
			DepthNormalPrePass,
			// Token: 0x040008B0 RID: 2224
			Opaque
		}

		// Token: 0x02000182 RID: 386
		public enum MaskSize
		{
			// Token: 0x040008B2 RID: 2226
			Bits8,
			// Token: 0x040008B3 RID: 2227
			Bits16,
			// Token: 0x040008B4 RID: 2228
			Bits24,
			// Token: 0x040008B5 RID: 2229
			Bits32
		}
	}
}
