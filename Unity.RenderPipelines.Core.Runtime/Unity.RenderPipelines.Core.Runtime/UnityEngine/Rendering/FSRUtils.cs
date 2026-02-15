using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001BD RID: 445
	public static class FSRUtils
	{
		// Token: 0x06000D1D RID: 3357 RVA: 0x0002F80C File Offset: 0x0002DA0C
		public static void SetEasuConstants(CommandBuffer cmd, Vector2 inputViewportSizeInPixels, Vector2 inputImageSizeInPixels, Vector2 outputImageSizeInPixels)
		{
			Vector4 constants0;
			constants0.x = inputViewportSizeInPixels.x / outputImageSizeInPixels.x;
			constants0.y = inputViewportSizeInPixels.y / outputImageSizeInPixels.y;
			constants0.z = 0.5f * inputViewportSizeInPixels.x / outputImageSizeInPixels.x - 0.5f;
			constants0.w = 0.5f * inputViewportSizeInPixels.y / outputImageSizeInPixels.y - 0.5f;
			Vector4 constants;
			constants.x = 1f / inputImageSizeInPixels.x;
			constants.y = 1f / inputImageSizeInPixels.y;
			constants.z = 1f / inputImageSizeInPixels.x;
			constants.w = -1f / inputImageSizeInPixels.y;
			Vector4 constants2;
			constants2.x = -1f / inputImageSizeInPixels.x;
			constants2.y = 2f / inputImageSizeInPixels.y;
			constants2.z = 1f / inputImageSizeInPixels.x;
			constants2.w = 2f / inputImageSizeInPixels.y;
			Vector4 constants3;
			constants3.x = 0f / inputImageSizeInPixels.x;
			constants3.y = 4f / inputImageSizeInPixels.y;
			constants3.z = 0f;
			constants3.w = 0f;
			cmd.SetGlobalVector(FSRUtils.ShaderConstants._FsrEasuConstants0, constants0);
			cmd.SetGlobalVector(FSRUtils.ShaderConstants._FsrEasuConstants1, constants);
			cmd.SetGlobalVector(FSRUtils.ShaderConstants._FsrEasuConstants2, constants2);
			cmd.SetGlobalVector(FSRUtils.ShaderConstants._FsrEasuConstants3, constants3);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0002F987 File Offset: 0x0002DB87
		public static void SetEasuConstants(BaseCommandBuffer cmd, Vector2 inputViewportSizeInPixels, Vector2 inputImageSizeInPixels, Vector2 outputImageSizeInPixels)
		{
			FSRUtils.SetEasuConstants(cmd.m_WrappedCommandBuffer, inputViewportSizeInPixels, inputImageSizeInPixels, outputImageSizeInPixels);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0002F998 File Offset: 0x0002DB98
		public static void SetRcasConstants(CommandBuffer cmd, float sharpnessStops = 0.2f)
		{
			float sharpnessLinear = Mathf.Pow(2f, -sharpnessStops);
			ushort num = Mathf.FloatToHalf(sharpnessLinear);
			float packedSharpnessAsFloat = BitConverter.Int32BitsToSingle((int)num | ((int)num << 16));
			Vector4 constants;
			constants.x = sharpnessLinear;
			constants.y = packedSharpnessAsFloat;
			constants.z = 0f;
			constants.w = 0f;
			cmd.SetGlobalVector(FSRUtils.ShaderConstants._FsrRcasConstants, constants);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0002F9F8 File Offset: 0x0002DBF8
		public static void SetRcasConstantsLinear(CommandBuffer cmd, float sharpnessLinear = 0.92f)
		{
			float sharpnessStops = (1f - sharpnessLinear) * 2.5f;
			FSRUtils.SetRcasConstants(cmd, sharpnessStops);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0002FA1A File Offset: 0x0002DC1A
		public static void SetRcasConstantsLinear(RasterCommandBuffer cmd, float sharpnessLinear = 0.92f)
		{
			FSRUtils.SetRcasConstantsLinear(cmd.m_WrappedCommandBuffer, sharpnessLinear);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0002FA28 File Offset: 0x0002DC28
		public static bool IsSupported()
		{
			return SystemInfo.graphicsShaderLevel >= 45;
		}

		// Token: 0x04000895 RID: 2197
		internal const float kMaxSharpnessStops = 2.5f;

		// Token: 0x04000896 RID: 2198
		public const float kDefaultSharpnessStops = 0.2f;

		// Token: 0x04000897 RID: 2199
		public const float kDefaultSharpnessLinear = 0.92f;

		// Token: 0x020001BE RID: 446
		private static class ShaderConstants
		{
			// Token: 0x04000898 RID: 2200
			public static readonly int _FsrEasuConstants0 = Shader.PropertyToID("_FsrEasuConstants0");

			// Token: 0x04000899 RID: 2201
			public static readonly int _FsrEasuConstants1 = Shader.PropertyToID("_FsrEasuConstants1");

			// Token: 0x0400089A RID: 2202
			public static readonly int _FsrEasuConstants2 = Shader.PropertyToID("_FsrEasuConstants2");

			// Token: 0x0400089B RID: 2203
			public static readonly int _FsrEasuConstants3 = Shader.PropertyToID("_FsrEasuConstants3");

			// Token: 0x0400089C RID: 2204
			public static readonly int _FsrRcasConstants = Shader.PropertyToID("_FsrRcasConstants");
		}
	}
}
