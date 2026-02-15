using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x020001B7 RID: 439
	public static class CoreUtils
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0002E614 File Offset: 0x0002C814
		public static Cubemap blackCubeTexture
		{
			get
			{
				if (CoreUtils.m_BlackCubeTexture == null)
				{
					CoreUtils.m_BlackCubeTexture = new Cubemap(1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None);
					for (int i = 0; i < 6; i++)
					{
						CoreUtils.m_BlackCubeTexture.SetPixel((CubemapFace)i, 0, 0, Color.black);
					}
					CoreUtils.m_BlackCubeTexture.Apply();
				}
				return CoreUtils.m_BlackCubeTexture;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0002E668 File Offset: 0x0002C868
		public static Cubemap magentaCubeTexture
		{
			get
			{
				if (CoreUtils.m_MagentaCubeTexture == null)
				{
					CoreUtils.m_MagentaCubeTexture = new Cubemap(1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None);
					for (int i = 0; i < 6; i++)
					{
						CoreUtils.m_MagentaCubeTexture.SetPixel((CubemapFace)i, 0, 0, Color.magenta);
					}
					CoreUtils.m_MagentaCubeTexture.Apply();
				}
				return CoreUtils.m_MagentaCubeTexture;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0002E6BC File Offset: 0x0002C8BC
		public static CubemapArray magentaCubeTextureArray
		{
			get
			{
				if (CoreUtils.m_MagentaCubeTextureArray == null)
				{
					CoreUtils.m_MagentaCubeTextureArray = new CubemapArray(1, 1, GraphicsFormat.R32G32B32A32_SFloat, TextureCreationFlags.None);
					for (int i = 0; i < 6; i++)
					{
						Color[] colors = new Color[] { Color.magenta };
						CoreUtils.m_MagentaCubeTextureArray.SetPixels(colors, (CubemapFace)i, 0);
					}
					CoreUtils.m_MagentaCubeTextureArray.Apply();
				}
				return CoreUtils.m_MagentaCubeTextureArray;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0002E720 File Offset: 0x0002C920
		public static Cubemap whiteCubeTexture
		{
			get
			{
				if (CoreUtils.m_WhiteCubeTexture == null)
				{
					CoreUtils.m_WhiteCubeTexture = new Cubemap(1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None);
					for (int i = 0; i < 6; i++)
					{
						CoreUtils.m_WhiteCubeTexture.SetPixel((CubemapFace)i, 0, 0, Color.white);
					}
					CoreUtils.m_WhiteCubeTexture.Apply();
				}
				return CoreUtils.m_WhiteCubeTexture;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0002E774 File Offset: 0x0002C974
		public static RenderTexture emptyUAV
		{
			get
			{
				if (CoreUtils.m_EmptyUAV == null)
				{
					CoreUtils.m_EmptyUAV = new RenderTexture(1, 1, 0);
					CoreUtils.m_EmptyUAV.enableRandomWrite = true;
					CoreUtils.m_EmptyUAV.Create();
				}
				return CoreUtils.m_EmptyUAV;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0002E7AB File Offset: 0x0002C9AB
		public static GraphicsBuffer emptyBuffer
		{
			get
			{
				if (CoreUtils.m_EmptyBuffer == null || !CoreUtils.m_EmptyBuffer.IsValid())
				{
					CoreUtils.m_EmptyBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Raw, 1, 4);
				}
				return CoreUtils.m_EmptyBuffer;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0002E7D4 File Offset: 0x0002C9D4
		public static Texture3D blackVolumeTexture
		{
			get
			{
				if (CoreUtils.m_BlackVolumeTexture == null)
				{
					Color[] colors = new Color[] { Color.black };
					CoreUtils.m_BlackVolumeTexture = new Texture3D(1, 1, 1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None);
					CoreUtils.m_BlackVolumeTexture.SetPixels(colors, 0);
					CoreUtils.m_BlackVolumeTexture.Apply();
				}
				return CoreUtils.m_BlackVolumeTexture;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0002E82C File Offset: 0x0002CA2C
		internal static Texture3D whiteVolumeTexture
		{
			get
			{
				if (CoreUtils.m_WhiteVolumeTexture == null)
				{
					Color[] colors = new Color[] { Color.white };
					CoreUtils.m_WhiteVolumeTexture = new Texture3D(1, 1, 1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None);
					CoreUtils.m_WhiteVolumeTexture.SetPixels(colors, 0);
					CoreUtils.m_WhiteVolumeTexture.Apply();
				}
				return CoreUtils.m_WhiteVolumeTexture;
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002E883 File Offset: 0x0002CA83
		public static void ClearRenderTarget(CommandBuffer cmd, ClearFlag clearFlag, Color clearColor)
		{
			if (clearFlag != ClearFlag.None)
			{
				cmd.ClearRenderTarget((RTClearFlags)clearFlag, clearColor, 1f, 0U);
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002E896 File Offset: 0x0002CA96
		private static int FixupDepthSlice(int depthSlice, RTHandle buffer)
		{
			if (depthSlice == -1)
			{
				RenderTexture rt = buffer.rt;
				if (rt != null && rt.dimension == TextureDimension.Cube)
				{
					depthSlice = 0;
				}
			}
			return depthSlice;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002E8B7 File Offset: 0x0002CAB7
		private static int FixupDepthSlice(int depthSlice, CubemapFace cubemapFace)
		{
			if (depthSlice == -1 && cubemapFace != CubemapFace.Unknown)
			{
				depthSlice = 0;
			}
			return depthSlice;
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002E8C5 File Offset: 0x0002CAC5
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier buffer, ClearFlag clearFlag, Color clearColor, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = CoreUtils.FixupDepthSlice(depthSlice, cubemapFace);
			cmd.SetRenderTarget(buffer, miplevel, cubemapFace, depthSlice);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0002E8E7 File Offset: 0x0002CAE7
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier buffer, ClearFlag clearFlag = ClearFlag.None, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(cmd, buffer, clearFlag, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0002E8FB File Offset: 0x0002CAFB
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorBuffer, RenderTargetIdentifier depthBuffer, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffer, depthBuffer, ClearFlag.None, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0002E910 File Offset: 0x0002CB10
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorBuffer, RenderTargetIdentifier depthBuffer, ClearFlag clearFlag, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffer, depthBuffer, clearFlag, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0002E926 File Offset: 0x0002CB26
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorBuffer, RenderTargetIdentifier depthBuffer, ClearFlag clearFlag, Color clearColor, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = CoreUtils.FixupDepthSlice(depthSlice, cubemapFace);
			cmd.SetRenderTarget(colorBuffer, depthBuffer, miplevel, cubemapFace, depthSlice);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0002E94A File Offset: 0x0002CB4A
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RenderTargetIdentifier depthBuffer)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffers, depthBuffer, ClearFlag.None, Color.clear);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0002E95A File Offset: 0x0002CB5A
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RenderTargetIdentifier depthBuffer, ClearFlag clearFlag = ClearFlag.None)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffers, depthBuffer, clearFlag, Color.clear);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0002E96A File Offset: 0x0002CB6A
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RenderTargetIdentifier depthBuffer, ClearFlag clearFlag, Color clearColor)
		{
			cmd.SetRenderTarget(colorBuffers, depthBuffer, 0, CubemapFace.Unknown, -1);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0002E980 File Offset: 0x0002CB80
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier buffer, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction, ClearFlag clearFlag, Color clearColor)
		{
			cmd.SetRenderTarget(buffer, loadAction, storeAction);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0002E995 File Offset: 0x0002CB95
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier buffer, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = CoreUtils.FixupDepthSlice(depthSlice, cubemapFace);
			buffer = new RenderTargetIdentifier(buffer, miplevel, cubemapFace, depthSlice);
			cmd.SetRenderTarget(buffer, loadAction, storeAction);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0002E9B9 File Offset: 0x0002CBB9
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier buffer, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction, ClearFlag clearFlag, Color clearColor, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = CoreUtils.FixupDepthSlice(depthSlice, cubemapFace);
			buffer = new RenderTargetIdentifier(buffer, miplevel, cubemapFace, depthSlice);
			CoreUtils.SetRenderTarget(cmd, buffer, loadAction, storeAction, clearFlag, clearColor);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002E9E1 File Offset: 0x0002CBE1
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier buffer, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction, ClearFlag clearFlag)
		{
			CoreUtils.SetRenderTarget(cmd, buffer, loadAction, storeAction, clearFlag, Color.clear);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0002E9F3 File Offset: 0x0002CBF3
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorBuffer, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depthBuffer, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, ClearFlag clearFlag, Color clearColor)
		{
			cmd.SetRenderTarget(colorBuffer, colorLoadAction, colorStoreAction, depthBuffer, depthLoadAction, depthStoreAction);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0002EA0E File Offset: 0x0002CC0E
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorBuffer, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depthBuffer, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = CoreUtils.FixupDepthSlice(depthSlice, cubemapFace);
			colorBuffer = new RenderTargetIdentifier(colorBuffer, miplevel, cubemapFace, depthSlice);
			depthBuffer = new RenderTargetIdentifier(depthBuffer, miplevel, cubemapFace, depthSlice);
			cmd.SetRenderTarget(colorBuffer, colorLoadAction, colorStoreAction, depthBuffer, depthLoadAction, depthStoreAction);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0002EA48 File Offset: 0x0002CC48
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorBuffer, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depthBuffer, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, ClearFlag clearFlag, Color clearColor, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = CoreUtils.FixupDepthSlice(depthSlice, cubemapFace);
			colorBuffer = new RenderTargetIdentifier(colorBuffer, miplevel, cubemapFace, depthSlice);
			depthBuffer = new RenderTargetIdentifier(depthBuffer, miplevel, cubemapFace, depthSlice);
			CoreUtils.SetRenderTarget(cmd, colorBuffer, colorLoadAction, colorStoreAction, depthBuffer, depthLoadAction, depthStoreAction, clearFlag, clearColor);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0002EA90 File Offset: 0x0002CC90
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier buffer, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, ClearFlag clearFlag, Color clearColor)
		{
			cmd.SetRenderTarget(buffer, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002EAAC File Offset: 0x0002CCAC
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorBuffer, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depthBuffer, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, ClearFlag clearFlag)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffer, colorLoadAction, colorStoreAction, depthBuffer, depthLoadAction, depthStoreAction, clearFlag, Color.clear);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0002EACF File Offset: 0x0002CCCF
		private static void SetViewportAndClear(CommandBuffer cmd, RTHandle buffer, ClearFlag clearFlag, Color clearColor)
		{
			CoreUtils.SetViewport(cmd, buffer);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0002EAE0 File Offset: 0x0002CCE0
		public static void SetRenderTarget(CommandBuffer cmd, RTHandle buffer, ClearFlag clearFlag, Color clearColor, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = CoreUtils.FixupDepthSlice(depthSlice, buffer);
			cmd.SetRenderTarget(buffer.nameID, miplevel, cubemapFace, depthSlice);
			CoreUtils.SetViewportAndClear(cmd, buffer, clearFlag, clearColor);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0002EB07 File Offset: 0x0002CD07
		public static void SetRenderTarget(CommandBuffer cmd, RTHandle buffer, ClearFlag clearFlag = ClearFlag.None, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(cmd, buffer, clearFlag, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0002EB1B File Offset: 0x0002CD1B
		public static void SetRenderTarget(CommandBuffer cmd, RTHandle colorBuffer, RTHandle depthBuffer, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffer, depthBuffer, ClearFlag.None, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0002EB30 File Offset: 0x0002CD30
		public static void SetRenderTarget(CommandBuffer cmd, RTHandle colorBuffer, RTHandle depthBuffer, ClearFlag clearFlag, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffer, depthBuffer, clearFlag, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0002EB46 File Offset: 0x0002CD46
		public static void SetRenderTarget(CommandBuffer cmd, RTHandle colorBuffer, RTHandle depthBuffer, ClearFlag clearFlag, Color clearColor, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffer.nameID, depthBuffer.nameID, miplevel, cubemapFace, depthSlice);
			CoreUtils.SetViewportAndClear(cmd, colorBuffer, clearFlag, clearColor);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0002EB6A File Offset: 0x0002CD6A
		public static void SetRenderTarget(CommandBuffer cmd, RTHandle buffer, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction, ClearFlag clearFlag, Color clearColor, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(cmd, buffer.nameID, loadAction, storeAction, miplevel, cubemapFace, depthSlice);
			CoreUtils.SetViewportAndClear(cmd, buffer, clearFlag, clearColor);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0002EB8C File Offset: 0x0002CD8C
		public static void SetRenderTarget(CommandBuffer cmd, RTHandle colorBuffer, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RTHandle depthBuffer, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, ClearFlag clearFlag, Color clearColor, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffer.nameID, colorLoadAction, colorStoreAction, depthBuffer.nameID, depthLoadAction, depthStoreAction, miplevel, cubemapFace, depthSlice);
			CoreUtils.SetViewportAndClear(cmd, colorBuffer, clearFlag, clearColor);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0002EBC3 File Offset: 0x0002CDC3
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RTHandle depthBuffer)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffers, depthBuffer.nameID, ClearFlag.None, Color.clear);
			CoreUtils.SetViewport(cmd, depthBuffer);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0002EBDF File Offset: 0x0002CDDF
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RTHandle depthBuffer, ClearFlag clearFlag = ClearFlag.None)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffers, depthBuffer.nameID);
			CoreUtils.SetViewportAndClear(cmd, depthBuffer, clearFlag, Color.clear);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0002EBFB File Offset: 0x0002CDFB
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RTHandle depthBuffer, ClearFlag clearFlag, Color clearColor)
		{
			cmd.SetRenderTarget(colorBuffers, depthBuffer.nameID, 0, CubemapFace.Unknown, -1);
			CoreUtils.SetViewportAndClear(cmd, depthBuffer, clearFlag, clearColor);
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0002EC18 File Offset: 0x0002CE18
		public static void SetViewport(CommandBuffer cmd, RTHandle target)
		{
			if (target.useScaling)
			{
				Vector2Int scaledViewportSize = target.GetScaledSize(target.rtHandleProperties.currentViewportSize);
				cmd.SetViewport(new Rect(0f, 0f, (float)scaledViewportSize.x, (float)scaledViewportSize.y));
			}
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0002EC64 File Offset: 0x0002CE64
		public static string GetRenderTargetAutoName(int width, int height, int depth, RenderTextureFormat format, string name, bool mips = false, bool enableMSAA = false, MSAASamples msaaSamples = MSAASamples.None)
		{
			return CoreUtils.GetRenderTargetAutoName(width, height, depth, format.ToString(), TextureDimension.None, name, mips, enableMSAA, msaaSamples, false, false);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002EC94 File Offset: 0x0002CE94
		public static string GetRenderTargetAutoName(int width, int height, int depth, GraphicsFormat format, string name, bool mips = false, bool enableMSAA = false, MSAASamples msaaSamples = MSAASamples.None)
		{
			return CoreUtils.GetRenderTargetAutoName(width, height, depth, format.ToString(), TextureDimension.None, name, mips, enableMSAA, msaaSamples, false, false);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0002ECC4 File Offset: 0x0002CEC4
		public static string GetRenderTargetAutoName(int width, int height, int depth, GraphicsFormat format, TextureDimension dim, string name, bool mips = false, bool enableMSAA = false, MSAASamples msaaSamples = MSAASamples.None, bool dynamicRes = false, bool dynamicResExplicit = false)
		{
			return CoreUtils.GetRenderTargetAutoName(width, height, depth, format.ToString(), dim, name, mips, enableMSAA, msaaSamples, dynamicRes, dynamicResExplicit);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0002ECF4 File Offset: 0x0002CEF4
		private static string GetRenderTargetAutoName(int width, int height, int depth, string format, TextureDimension dim, string name, bool mips, bool enableMSAA, MSAASamples msaaSamples, bool dynamicRes, bool dynamicResExplicit)
		{
			string result = string.Format("{0}_{1}x{2}", name, width, height);
			if (depth > 1)
			{
				result = string.Format("{0}x{1}", result, depth);
			}
			if (mips)
			{
				result = string.Format("{0}_{1}", result, "Mips");
			}
			result = string.Format("{0}_{1}", result, format);
			if (dim != TextureDimension.None)
			{
				result = string.Format("{0}_{1}", result, dim);
			}
			if (enableMSAA)
			{
				result = string.Format("{0}_{1}", result, msaaSamples.ToString());
			}
			if (dynamicRes)
			{
				result = string.Format("{0}_{1}", result, "Dynamic");
			}
			if (dynamicResExplicit)
			{
				result = string.Format("{0}_{1}", result, "DynamicExplicit");
			}
			return result;
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002EDB1 File Offset: 0x0002CFB1
		public static string GetTextureAutoName(int width, int height, TextureFormat format, TextureDimension dim = TextureDimension.None, string name = "", bool mips = false, int depth = 0)
		{
			return CoreUtils.GetTextureAutoName(width, height, format.ToString(), dim, name, mips, depth);
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002EDCE File Offset: 0x0002CFCE
		public static string GetTextureAutoName(int width, int height, GraphicsFormat format, TextureDimension dim = TextureDimension.None, string name = "", bool mips = false, int depth = 0)
		{
			return CoreUtils.GetTextureAutoName(width, height, format.ToString(), dim, name, mips, depth);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0002EDEC File Offset: 0x0002CFEC
		private static string GetTextureAutoName(int width, int height, string format, TextureDimension dim = TextureDimension.None, string name = "", bool mips = false, int depth = 0)
		{
			string temp;
			if (depth == 0)
			{
				temp = string.Format("{0}x{1}{2}_{3}", new object[]
				{
					width,
					height,
					mips ? "_Mips" : "",
					format
				});
			}
			else
			{
				temp = string.Format("{0}x{1}x{2}{3}_{4}", new object[]
				{
					width,
					height,
					depth,
					mips ? "_Mips" : "",
					format
				});
			}
			return string.Format("{0}_{1}_{2}", (name == "") ? "Texture" : name, (dim == TextureDimension.None) ? "" : dim.ToString(), temp);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002EEB8 File Offset: 0x0002D0B8
		public static void ClearCubemap(CommandBuffer cmd, RenderTexture renderTexture, Color clearColor, bool clearMips = false)
		{
			int mipCount = 1;
			if (renderTexture.useMipMap && clearMips)
			{
				mipCount = (int)Mathf.Log((float)renderTexture.width, 2f) + 1;
			}
			for (int i = 0; i < 6; i++)
			{
				for (int mip = 0; mip < mipCount; mip++)
				{
					CoreUtils.SetRenderTarget(cmd, new RenderTargetIdentifier(renderTexture), ClearFlag.Color, clearColor, mip, (CubemapFace)i, -1);
				}
			}
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0002EF0F File Offset: 0x0002D10F
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			commandBuffer.DrawProcedural(Matrix4x4.identity, material, shaderPassId, MeshTopology.Triangles, 3, 1, properties);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002EF22 File Offset: 0x0002D122
		public static void DrawFullScreen(RasterCommandBuffer commandBuffer, Material material, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			CoreUtils.DrawFullScreen(commandBuffer.m_WrappedCommandBuffer, material, properties, shaderPassId);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x0002EF32 File Offset: 0x0002D132
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, RenderTargetIdentifier colorBuffer, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			commandBuffer.SetRenderTarget(colorBuffer, 0, CubemapFace.Unknown, -1);
			commandBuffer.DrawProcedural(Matrix4x4.identity, material, shaderPassId, MeshTopology.Triangles, 3, 1, properties);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0002EF50 File Offset: 0x0002D150
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, RenderTargetIdentifier colorBuffer, RenderTargetIdentifier depthStencilBuffer, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			commandBuffer.SetRenderTarget(colorBuffer, depthStencilBuffer, 0, CubemapFace.Unknown, -1);
			commandBuffer.DrawProcedural(Matrix4x4.identity, material, shaderPassId, MeshTopology.Triangles, 3, 1, properties);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0002EF70 File Offset: 0x0002D170
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, RenderTargetIdentifier[] colorBuffers, RenderTargetIdentifier depthStencilBuffer, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			commandBuffer.SetRenderTarget(colorBuffers, depthStencilBuffer, 0, CubemapFace.Unknown, -1);
			commandBuffer.DrawProcedural(Matrix4x4.identity, material, shaderPassId, MeshTopology.Triangles, 3, 1, properties);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0002EF90 File Offset: 0x0002D190
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, RenderTargetIdentifier[] colorBuffers, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			CoreUtils.DrawFullScreen(commandBuffer, material, colorBuffers, colorBuffers[0], properties, shaderPassId);
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0002EFA4 File Offset: 0x0002D1A4
		public static Color ConvertSRGBToActiveColorSpace(Color color)
		{
			if (QualitySettings.activeColorSpace != ColorSpace.Linear)
			{
				return color;
			}
			return color.linear;
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0002EFB7 File Offset: 0x0002D1B7
		public static Color ConvertLinearToActiveColorSpace(Color color)
		{
			if (QualitySettings.activeColorSpace != ColorSpace.Linear)
			{
				return color.gamma;
			}
			return color;
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0002EFCC File Offset: 0x0002D1CC
		public static Material CreateEngineMaterial(string shaderPath)
		{
			if (string.IsNullOrEmpty(shaderPath))
			{
				throw new ArgumentException("shaderPath");
			}
			Shader shader = Shader.Find(shaderPath);
			if (shader == null)
			{
				Debug.LogError("Cannot create required material because shader " + shaderPath + " could not be found");
				return null;
			}
			return CoreUtils.CreateEngineMaterial(shader);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0002F019 File Offset: 0x0002D219
		public static Material CreateEngineMaterial(Shader shader)
		{
			if (shader == null)
			{
				Debug.LogError("Cannot create required material because shader is null");
				return null;
			}
			return new Material(shader)
			{
				hideFlags = HideFlags.HideAndDontSave
			};
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0002F03E File Offset: 0x0002D23E
		public static bool HasFlag<T>(T mask, T flag) where T : IConvertible
		{
			return (mask.ToUInt32(null) & flag.ToUInt32(null)) > 0U;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0002F060 File Offset: 0x0002D260
		public static void Swap<T>(ref T a, ref T b)
		{
			T tmp = a;
			a = b;
			b = tmp;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0002F087 File Offset: 0x0002D287
		public static void SetKeyword(CommandBuffer cmd, string keyword, bool state)
		{
			if (state)
			{
				cmd.EnableShaderKeyword(keyword);
				return;
			}
			cmd.DisableShaderKeyword(keyword);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0002F09C File Offset: 0x0002D29C
		public static void SetKeyword(CommandBuffer cmd, ComputeShader cs, string keyword, bool state)
		{
			LocalKeyword kw = new LocalKeyword(cs, keyword);
			if (state)
			{
				cmd.EnableKeyword(cs, in kw);
				return;
			}
			cmd.DisableKeyword(cs, in kw);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0002F0C8 File Offset: 0x0002D2C8
		public static void SetKeyword(BaseCommandBuffer cmd, string keyword, bool state)
		{
			if (state)
			{
				cmd.m_WrappedCommandBuffer.EnableShaderKeyword(keyword);
				return;
			}
			cmd.m_WrappedCommandBuffer.DisableShaderKeyword(keyword);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0002F0E6 File Offset: 0x0002D2E6
		public static void SetKeyword(Material material, string keyword, bool state)
		{
			if (state)
			{
				material.EnableKeyword(keyword);
				return;
			}
			material.DisableKeyword(keyword);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0002F0FA File Offset: 0x0002D2FA
		public static void SetKeyword(Material material, LocalKeyword keyword, bool state)
		{
			if (state)
			{
				material.EnableKeyword(in keyword);
				return;
			}
			material.DisableKeyword(in keyword);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0002F110 File Offset: 0x0002D310
		public static void SetKeyword(ComputeShader cs, string keyword, bool state)
		{
			if (state)
			{
				cs.EnableKeyword(keyword);
				return;
			}
			cs.DisableKeyword(keyword);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0002F124 File Offset: 0x0002D324
		public static void Destroy(Object obj)
		{
			if (obj != null)
			{
				Object.Destroy(obj);
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0002F138 File Offset: 0x0002D338
		public static IEnumerable<Type> GetAllAssemblyTypes()
		{
			if (CoreUtils.m_AssemblyTypes == null)
			{
				CoreUtils.m_AssemblyTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(delegate(Assembly t)
				{
					Type[] innerTypes = new Type[0];
					try
					{
						innerTypes = t.GetTypes();
					}
					catch
					{
					}
					return innerTypes;
				});
			}
			return CoreUtils.m_AssemblyTypes;
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0002F184 File Offset: 0x0002D384
		public static IEnumerable<Type> GetAllTypesDerivedFrom<T>()
		{
			return from t in CoreUtils.GetAllAssemblyTypes()
				where t.IsSubclassOf(typeof(T))
				select t;
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0002F1AF File Offset: 0x0002D3AF
		public static void SafeRelease(GraphicsBuffer buffer)
		{
			if (buffer != null)
			{
				buffer.Release();
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0002F1BA File Offset: 0x0002D3BA
		public static void SafeRelease(ComputeBuffer buffer)
		{
			if (buffer != null)
			{
				buffer.Release();
			}
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0002F1C8 File Offset: 0x0002D3C8
		public static Mesh CreateCubeMesh(Vector3 min, Vector3 max)
		{
			return new Mesh
			{
				vertices = new Vector3[]
				{
					new Vector3(min.x, min.y, min.z),
					new Vector3(max.x, min.y, min.z),
					new Vector3(max.x, max.y, min.z),
					new Vector3(min.x, max.y, min.z),
					new Vector3(min.x, min.y, max.z),
					new Vector3(max.x, min.y, max.z),
					new Vector3(max.x, max.y, max.z),
					new Vector3(min.x, max.y, max.z)
				},
				triangles = new int[]
				{
					0, 2, 1, 0, 3, 2, 1, 6, 5, 1,
					2, 6, 5, 7, 4, 5, 6, 7, 4, 3,
					0, 4, 7, 3, 3, 6, 2, 3, 7, 6,
					4, 1, 5, 4, 0, 1
				}
			};
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x000104EC File Offset: 0x0000E6EC
		public static bool ArePostProcessesEnabled(Camera camera)
		{
			return true;
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x000104EC File Offset: 0x0000E6EC
		public static bool AreAnimatedMaterialsEnabled(Camera camera)
		{
			return true;
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000090C6 File Offset: 0x000072C6
		public static bool IsSceneLightingDisabled(Camera camera)
		{
			return false;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x000090C6 File Offset: 0x000072C6
		public static bool IsLightOverlapDebugEnabled(Camera camera)
		{
			return false;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x000104EC File Offset: 0x0000E6EC
		public static bool IsSceneViewFogEnabled(Camera camera)
		{
			return true;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000090C6 File Offset: 0x000072C6
		public static bool IsSceneFilteringEnabled()
		{
			return false;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x000090C6 File Offset: 0x000072C6
		public static bool IsSceneViewPrefabStageContextHidden()
		{
			return false;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0002F392 File Offset: 0x0002D592
		public static void DrawRendererList(ScriptableRenderContext renderContext, CommandBuffer cmd, RendererList rendererList)
		{
			cmd.DrawRendererList(rendererList);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0002F39C File Offset: 0x0002D59C
		public static int GetTextureHash(Texture texture)
		{
			int hash = texture.GetHashCode();
			hash = 23 * hash + texture.GetInstanceID().GetHashCode();
			hash = 23 * hash + texture.graphicsFormat.GetHashCode();
			hash = 23 * hash + texture.wrapMode.GetHashCode();
			hash = 23 * hash + texture.width.GetHashCode();
			hash = 23 * hash + texture.height.GetHashCode();
			hash = 23 * hash + texture.filterMode.GetHashCode();
			hash = 23 * hash + texture.anisoLevel.GetHashCode();
			hash = 23 * hash + texture.mipmapCount.GetHashCode();
			return 23 * hash + texture.updateCount.GetHashCode();
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0002F479 File Offset: 0x0002D679
		public static int PreviousPowerOfTwo(int size)
		{
			if (size <= 0)
			{
				return 0;
			}
			size |= size >> 1;
			size |= size >> 2;
			size |= size >> 4;
			size |= size >> 8;
			size |= size >> 16;
			return size - (size >> 1);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002F4AA File Offset: 0x0002D6AA
		public static int GetMipCount(int size)
		{
			return Mathf.FloorToInt(Mathf.Log((float)size, 2f)) + 1;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0002F4BF File Offset: 0x0002D6BF
		public static int GetMipCount(float size)
		{
			return Mathf.FloorToInt(Mathf.Log(size, 2f)) + 1;
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0002F4D3 File Offset: 0x0002D6D3
		public static int DivRoundUp(int value, int divisor)
		{
			return (value + (divisor - 1)) / divisor;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0002F4DC File Offset: 0x0002D6DC
		public static T GetLastEnumValue<T>() where T : Enum
		{
			return typeof(T).GetEnumValues().Cast<T>().Last<T>();
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002F4F7 File Offset: 0x0002D6F7
		internal static string GetCorePath()
		{
			return "Packages/com.unity.render-pipelines.core/";
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0002F500 File Offset: 0x0002D700
		public static Vector3[] CalculateViewSpaceCorners(Matrix4x4 proj, float z)
		{
			Vector3[] outCorners = new Vector3[4];
			Matrix4x4 invProj = Matrix4x4.Inverse(proj);
			outCorners[0] = invProj.MultiplyPoint(new Vector3(-1f, -1f, 0.95f));
			outCorners[1] = invProj.MultiplyPoint(new Vector3(1f, -1f, 0.95f));
			outCorners[2] = invProj.MultiplyPoint(new Vector3(1f, 1f, 0.95f));
			outCorners[3] = invProj.MultiplyPoint(new Vector3(-1f, 1f, 0.95f));
			for (int r = 0; r < 4; r++)
			{
				outCorners[r] *= z / -outCorners[r].z;
			}
			return outCorners;
		}

		// Token: 0x04000870 RID: 2160
		public static readonly Vector3[] lookAtList = new Vector3[]
		{
			new Vector3(1f, 0f, 0f),
			new Vector3(-1f, 0f, 0f),
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, -1f, 0f),
			new Vector3(0f, 0f, 1f),
			new Vector3(0f, 0f, -1f)
		};

		// Token: 0x04000871 RID: 2161
		public static readonly Vector3[] upVectorList = new Vector3[]
		{
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, 0f, -1f),
			new Vector3(0f, 0f, 1f),
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, 1f, 0f)
		};

		// Token: 0x04000872 RID: 2162
		private const string obsoletePriorityMessage = "Use CoreUtils.Priorities instead";

		// Token: 0x04000873 RID: 2163
		[Obsolete("Use CoreUtils.Priorities instead", false)]
		public const int editMenuPriority1 = 320;

		// Token: 0x04000874 RID: 2164
		[Obsolete("Use CoreUtils.Priorities instead", false)]
		public const int editMenuPriority2 = 331;

		// Token: 0x04000875 RID: 2165
		[Obsolete("Use CoreUtils.Priorities instead", false)]
		public const int editMenuPriority3 = 342;

		// Token: 0x04000876 RID: 2166
		[Obsolete("Use CoreUtils.Priorities instead", false)]
		public const int editMenuPriority4 = 353;

		// Token: 0x04000877 RID: 2167
		[Obsolete("Use CoreUtils.Priorities instead", false)]
		public const int assetCreateMenuPriority1 = 230;

		// Token: 0x04000878 RID: 2168
		[Obsolete("Use CoreUtils.Priorities instead", false)]
		public const int assetCreateMenuPriority2 = 241;

		// Token: 0x04000879 RID: 2169
		[Obsolete("Use CoreUtils.Priorities instead", false)]
		public const int assetCreateMenuPriority3 = 300;

		// Token: 0x0400087A RID: 2170
		[Obsolete("Use CoreUtils.Priorities instead", false)]
		public const int gameObjectMenuPriority = 10;

		// Token: 0x0400087B RID: 2171
		private static Cubemap m_BlackCubeTexture;

		// Token: 0x0400087C RID: 2172
		private static Cubemap m_MagentaCubeTexture;

		// Token: 0x0400087D RID: 2173
		private static CubemapArray m_MagentaCubeTextureArray;

		// Token: 0x0400087E RID: 2174
		private static Cubemap m_WhiteCubeTexture;

		// Token: 0x0400087F RID: 2175
		private static RenderTexture m_EmptyUAV;

		// Token: 0x04000880 RID: 2176
		private static GraphicsBuffer m_EmptyBuffer;

		// Token: 0x04000881 RID: 2177
		private static Texture3D m_BlackVolumeTexture;

		// Token: 0x04000882 RID: 2178
		internal static Texture3D m_WhiteVolumeTexture;

		// Token: 0x04000883 RID: 2179
		private static IEnumerable<Type> m_AssemblyTypes;

		// Token: 0x020001B8 RID: 440
		public static class Sections
		{
			// Token: 0x04000884 RID: 2180
			public const int section1 = 10000;

			// Token: 0x04000885 RID: 2181
			public const int section2 = 20000;

			// Token: 0x04000886 RID: 2182
			public const int section3 = 30000;

			// Token: 0x04000887 RID: 2183
			public const int section4 = 40000;

			// Token: 0x04000888 RID: 2184
			public const int section5 = 50000;

			// Token: 0x04000889 RID: 2185
			public const int section6 = 60000;

			// Token: 0x0400088A RID: 2186
			public const int section7 = 70000;

			// Token: 0x0400088B RID: 2187
			public const int section8 = 80000;
		}

		// Token: 0x020001B9 RID: 441
		public static class Priorities
		{
			// Token: 0x0400088C RID: 2188
			public const int assetsCreateShaderMenuPriority = 83;

			// Token: 0x0400088D RID: 2189
			public const int assetsCreateRenderingMenuPriority = 308;

			// Token: 0x0400088E RID: 2190
			public const int editMenuPriority = 320;

			// Token: 0x0400088F RID: 2191
			public const int gameObjectMenuPriority = 10;

			// Token: 0x04000890 RID: 2192
			public const int srpLensFlareMenuPriority = 303;
		}
	}
}
