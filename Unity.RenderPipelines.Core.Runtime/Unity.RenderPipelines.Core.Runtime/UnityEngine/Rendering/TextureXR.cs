using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x020001A2 RID: 418
	public static class TextureXR
	{
		// Token: 0x17000173 RID: 371
		// (set) Token: 0x06000BC4 RID: 3012 RVA: 0x0002AB30 File Offset: 0x00028D30
		public static int maxViews
		{
			set
			{
				TextureXR.m_MaxViews = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0002AB38 File Offset: 0x00028D38
		public static int slices
		{
			get
			{
				return TextureXR.m_MaxViews;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0002AB40 File Offset: 0x00028D40
		public static bool useTexArray
		{
			get
			{
				GraphicsDeviceType graphicsDeviceType = SystemInfo.graphicsDeviceType;
				if (graphicsDeviceType <= GraphicsDeviceType.Metal)
				{
					if (graphicsDeviceType != GraphicsDeviceType.Direct3D11 && graphicsDeviceType != GraphicsDeviceType.PlayStation4 && graphicsDeviceType != GraphicsDeviceType.Metal)
					{
						return false;
					}
				}
				else if (graphicsDeviceType != GraphicsDeviceType.Direct3D12 && graphicsDeviceType != GraphicsDeviceType.Vulkan && graphicsDeviceType - GraphicsDeviceType.PlayStation5 > 1)
				{
					return false;
				}
				return true;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x0002AB7C File Offset: 0x00028D7C
		public static TextureDimension dimension
		{
			get
			{
				if (!TextureXR.useTexArray)
				{
					return TextureDimension.Tex2D;
				}
				return TextureDimension.Tex2DArray;
			}
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002AB88 File Offset: 0x00028D88
		public static RTHandle GetBlackUIntTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_BlackUIntTextureRTH;
			}
			return TextureXR.m_BlackUIntTexture2DArrayRTH;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002AB9C File Offset: 0x00028D9C
		public static RTHandle GetClearTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_ClearTextureRTH;
			}
			return TextureXR.m_ClearTexture2DArrayRTH;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002ABB0 File Offset: 0x00028DB0
		public static RTHandle GetMagentaTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_MagentaTextureRTH;
			}
			return TextureXR.m_MagentaTexture2DArrayRTH;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002ABC4 File Offset: 0x00028DC4
		public static RTHandle GetBlackTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_BlackTextureRTH;
			}
			return TextureXR.m_BlackTexture2DArrayRTH;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0002ABD8 File Offset: 0x00028DD8
		public static RTHandle GetBlackTextureArray()
		{
			return TextureXR.m_BlackTexture2DArrayRTH;
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0002ABDF File Offset: 0x00028DDF
		public static RTHandle GetBlackTexture3D()
		{
			return TextureXR.m_BlackTexture3DRTH;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0002ABE6 File Offset: 0x00028DE6
		public static RTHandle GetWhiteTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_WhiteTextureRTH;
			}
			return TextureXR.m_WhiteTexture2DArrayRTH;
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002ABFC File Offset: 0x00028DFC
		public static void Initialize(CommandBuffer cmd, ComputeShader clearR32_UIntShader)
		{
			if (TextureXR.m_BlackUIntTexture2DArray == null)
			{
				RTHandles.Release(TextureXR.m_BlackUIntTexture2DArrayRTH);
				TextureXR.m_BlackUIntTexture2DArray = TextureXR.CreateBlackUIntTextureArray(cmd, clearR32_UIntShader);
				TextureXR.m_BlackUIntTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_BlackUIntTexture2DArray);
				RTHandles.Release(TextureXR.m_BlackUIntTextureRTH);
				TextureXR.m_BlackUIntTexture = TextureXR.CreateBlackUintTexture(cmd, clearR32_UIntShader);
				TextureXR.m_BlackUIntTextureRTH = RTHandles.Alloc(TextureXR.m_BlackUIntTexture);
				RTHandles.Release(TextureXR.m_ClearTextureRTH);
				TextureXR.m_ClearTexture = new Texture2D(1, 1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None)
				{
					name = "Clear Texture"
				};
				TextureXR.m_ClearTexture.SetPixel(0, 0, Color.clear);
				TextureXR.m_ClearTexture.Apply();
				TextureXR.m_ClearTextureRTH = RTHandles.Alloc(TextureXR.m_ClearTexture);
				RTHandles.Release(TextureXR.m_ClearTexture2DArrayRTH);
				TextureXR.m_ClearTexture2DArray = TextureXR.CreateTexture2DArrayFromTexture2D(TextureXR.m_ClearTexture, "Clear Texture2DArray");
				TextureXR.m_ClearTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_ClearTexture2DArray);
				RTHandles.Release(TextureXR.m_MagentaTextureRTH);
				TextureXR.m_MagentaTexture = new Texture2D(1, 1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None)
				{
					name = "Magenta Texture"
				};
				TextureXR.m_MagentaTexture.SetPixel(0, 0, Color.magenta);
				TextureXR.m_MagentaTexture.Apply();
				TextureXR.m_MagentaTextureRTH = RTHandles.Alloc(TextureXR.m_MagentaTexture);
				RTHandles.Release(TextureXR.m_MagentaTexture2DArrayRTH);
				TextureXR.m_MagentaTexture2DArray = TextureXR.CreateTexture2DArrayFromTexture2D(TextureXR.m_MagentaTexture, "Magenta Texture2DArray");
				TextureXR.m_MagentaTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_MagentaTexture2DArray);
				RTHandles.Release(TextureXR.m_BlackTextureRTH);
				TextureXR.m_BlackTexture = new Texture2D(1, 1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None)
				{
					name = "Black Texture"
				};
				TextureXR.m_BlackTexture.SetPixel(0, 0, Color.black);
				TextureXR.m_BlackTexture.Apply();
				TextureXR.m_BlackTextureRTH = RTHandles.Alloc(TextureXR.m_BlackTexture);
				RTHandles.Release(TextureXR.m_BlackTexture2DArrayRTH);
				TextureXR.m_BlackTexture2DArray = TextureXR.CreateTexture2DArrayFromTexture2D(TextureXR.m_BlackTexture, "Black Texture2DArray");
				TextureXR.m_BlackTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_BlackTexture2DArray);
				RTHandles.Release(TextureXR.m_BlackTexture3DRTH);
				TextureXR.m_BlackTexture3D = TextureXR.CreateBlackTexture3D("Black Texture3D");
				TextureXR.m_BlackTexture3DRTH = RTHandles.Alloc(TextureXR.m_BlackTexture3D);
				RTHandles.Release(TextureXR.m_WhiteTextureRTH);
				TextureXR.m_WhiteTextureRTH = RTHandles.Alloc(Texture2D.whiteTexture);
				RTHandles.Release(TextureXR.m_WhiteTexture2DArrayRTH);
				TextureXR.m_WhiteTexture2DArray = TextureXR.CreateTexture2DArrayFromTexture2D(Texture2D.whiteTexture, "White Texture2DArray");
				TextureXR.m_WhiteTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_WhiteTexture2DArray);
			}
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002AE40 File Offset: 0x00029040
		private static Texture2DArray CreateTexture2DArrayFromTexture2D(Texture2D source, string name)
		{
			Texture2DArray texArray = new Texture2DArray(source.width, source.height, TextureXR.slices, source.format, false)
			{
				name = name
			};
			for (int i = 0; i < TextureXR.slices; i++)
			{
				Graphics.CopyTexture(source, 0, 0, texArray, i, 0);
			}
			return texArray;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002AE90 File Offset: 0x00029090
		private static Texture CreateBlackUIntTextureArray(CommandBuffer cmd, ComputeShader clearR32_UIntShader)
		{
			RenderTexture blackUIntTexture2DArray = new RenderTexture(1, 1, 0, GraphicsFormat.R32_UInt)
			{
				dimension = TextureDimension.Tex2DArray,
				volumeDepth = TextureXR.slices,
				useMipMap = false,
				autoGenerateMips = false,
				enableRandomWrite = true,
				name = "Black UInt Texture Array"
			};
			blackUIntTexture2DArray.Create();
			int kernel = clearR32_UIntShader.FindKernel("ClearUIntTextureArray");
			cmd.SetComputeTextureParam(clearR32_UIntShader, kernel, "_TargetArray", blackUIntTexture2DArray);
			cmd.DispatchCompute(clearR32_UIntShader, kernel, 1, 1, TextureXR.slices);
			return blackUIntTexture2DArray;
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002AF10 File Offset: 0x00029110
		private static Texture CreateBlackUintTexture(CommandBuffer cmd, ComputeShader clearR32_UIntShader)
		{
			RenderTexture blackUIntTexture2D = new RenderTexture(1, 1, 0, GraphicsFormat.R32_UInt)
			{
				dimension = TextureDimension.Tex2D,
				volumeDepth = 1,
				useMipMap = false,
				autoGenerateMips = false,
				enableRandomWrite = true,
				name = "Black UInt Texture"
			};
			blackUIntTexture2D.Create();
			int kernel = clearR32_UIntShader.FindKernel("ClearUIntTexture");
			cmd.SetComputeTextureParam(clearR32_UIntShader, kernel, "_Target", blackUIntTexture2D);
			cmd.DispatchCompute(clearR32_UIntShader, kernel, 1, 1, 1);
			return blackUIntTexture2D;
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002AF88 File Offset: 0x00029188
		private static Texture3D CreateBlackTexture3D(string name)
		{
			Texture3D texture3D = new Texture3D(1, 1, 1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None);
			texture3D.name = name;
			texture3D.SetPixel(0, 0, 0, Color.black, 0);
			texture3D.Apply(false);
			return texture3D;
		}

		// Token: 0x04000807 RID: 2055
		private static int m_MaxViews = 1;

		// Token: 0x04000808 RID: 2056
		private static Texture m_BlackUIntTexture2DArray;

		// Token: 0x04000809 RID: 2057
		private static Texture m_BlackUIntTexture;

		// Token: 0x0400080A RID: 2058
		private static RTHandle m_BlackUIntTexture2DArrayRTH;

		// Token: 0x0400080B RID: 2059
		private static RTHandle m_BlackUIntTextureRTH;

		// Token: 0x0400080C RID: 2060
		private static Texture2DArray m_ClearTexture2DArray;

		// Token: 0x0400080D RID: 2061
		private static Texture2D m_ClearTexture;

		// Token: 0x0400080E RID: 2062
		private static RTHandle m_ClearTexture2DArrayRTH;

		// Token: 0x0400080F RID: 2063
		private static RTHandle m_ClearTextureRTH;

		// Token: 0x04000810 RID: 2064
		private static Texture2DArray m_MagentaTexture2DArray;

		// Token: 0x04000811 RID: 2065
		private static Texture2D m_MagentaTexture;

		// Token: 0x04000812 RID: 2066
		private static RTHandle m_MagentaTexture2DArrayRTH;

		// Token: 0x04000813 RID: 2067
		private static RTHandle m_MagentaTextureRTH;

		// Token: 0x04000814 RID: 2068
		private static Texture2D m_BlackTexture;

		// Token: 0x04000815 RID: 2069
		private static Texture3D m_BlackTexture3D;

		// Token: 0x04000816 RID: 2070
		private static Texture2DArray m_BlackTexture2DArray;

		// Token: 0x04000817 RID: 2071
		private static RTHandle m_BlackTexture2DArrayRTH;

		// Token: 0x04000818 RID: 2072
		private static RTHandle m_BlackTextureRTH;

		// Token: 0x04000819 RID: 2073
		private static RTHandle m_BlackTexture3DRTH;

		// Token: 0x0400081A RID: 2074
		private static Texture2DArray m_WhiteTexture2DArray;

		// Token: 0x0400081B RID: 2075
		private static RTHandle m_WhiteTexture2DArrayRTH;

		// Token: 0x0400081C RID: 2076
		private static RTHandle m_WhiteTextureRTH;
	}
}
