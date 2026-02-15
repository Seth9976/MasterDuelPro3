using System;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RenderGraphModule.Util;

namespace UnityEngine.Rendering
{
	// Token: 0x020001AE RID: 430
	public static class Blitter
	{
		// Token: 0x06000C6C RID: 3180 RVA: 0x0002C548 File Offset: 0x0002A748
		public static void Initialize(Shader blitPS, Shader blitColorAndDepthPS)
		{
			if (Blitter.s_Blit != null)
			{
				throw new Exception("Blitter is already initialized. Please only initialize the blitter once or you will leak engine resources. If you need to re-initialize the blitter with different shaders destroy & recreate it.");
			}
			Blitter.s_Copy = CoreUtils.CreateEngineMaterial(GraphicsSettings.GetRenderPipelineSettings<RenderGraphUtilsResources>().coreCopyPS);
			Blitter.s_Blit = CoreUtils.CreateEngineMaterial(blitPS);
			Blitter.s_BlitColorAndDepth = CoreUtils.CreateEngineMaterial(blitColorAndDepthPS);
			Blitter.s_DecodeHdrKeyword = new LocalKeyword(blitPS, "BLIT_DECODE_HDR");
			if (TextureXR.useTexArray)
			{
				Blitter.s_Blit.EnableKeyword("DISABLE_TEXTURE2D_X_ARRAY");
				Blitter.s_BlitTexArray = CoreUtils.CreateEngineMaterial(blitPS);
				Blitter.s_BlitTexArraySingleSlice = CoreUtils.CreateEngineMaterial(blitPS);
				Blitter.s_BlitTexArraySingleSlice.EnableKeyword("BLIT_SINGLE_SLICE");
			}
			float nearClipZ = -1f;
			if (SystemInfo.usesReversedZBuffer)
			{
				nearClipZ = 1f;
			}
			if (SystemInfo.graphicsShaderLevel < 30 && !Blitter.s_TriangleMesh)
			{
				Blitter.s_TriangleMesh = new Mesh();
				Blitter.s_TriangleMesh.vertices = Blitter.<Initialize>g__GetFullScreenTriangleVertexPosition|14_0(nearClipZ);
				Blitter.s_TriangleMesh.uv = Blitter.<Initialize>g__GetFullScreenTriangleTexCoord|14_1();
				Blitter.s_TriangleMesh.triangles = new int[] { 0, 1, 2 };
			}
			if (!Blitter.s_QuadMesh)
			{
				Blitter.s_QuadMesh = new Mesh();
				Blitter.s_QuadMesh.vertices = Blitter.<Initialize>g__GetQuadVertexPosition|14_2(nearClipZ);
				Blitter.s_QuadMesh.uv = Blitter.<Initialize>g__GetQuadTexCoord|14_3();
				Blitter.s_QuadMesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
			}
			string[] passNames = Enum.GetNames(typeof(Blitter.BlitShaderPassNames));
			Blitter.s_BlitShaderPassIndicesMap = new int[passNames.Length];
			for (int i = 0; i < passNames.Length; i++)
			{
				Blitter.s_BlitShaderPassIndicesMap[i] = Blitter.s_Blit.FindPass(passNames[i]);
			}
			passNames = Enum.GetNames(typeof(Blitter.BlitColorAndDepthPassNames));
			Blitter.s_BlitColorAndDepthShaderPassIndicesMap = new int[passNames.Length];
			for (int j = 0; j < passNames.Length; j++)
			{
				Blitter.s_BlitColorAndDepthShaderPassIndicesMap[j] = Blitter.s_BlitColorAndDepth.FindPass(passNames[j]);
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002C71C File Offset: 0x0002A91C
		public static void Cleanup()
		{
			CoreUtils.Destroy(Blitter.s_Copy);
			Blitter.s_Copy = null;
			CoreUtils.Destroy(Blitter.s_Blit);
			Blitter.s_Blit = null;
			CoreUtils.Destroy(Blitter.s_BlitColorAndDepth);
			Blitter.s_BlitColorAndDepth = null;
			CoreUtils.Destroy(Blitter.s_BlitTexArray);
			Blitter.s_BlitTexArray = null;
			CoreUtils.Destroy(Blitter.s_BlitTexArraySingleSlice);
			Blitter.s_BlitTexArraySingleSlice = null;
			CoreUtils.Destroy(Blitter.s_TriangleMesh);
			Blitter.s_TriangleMesh = null;
			CoreUtils.Destroy(Blitter.s_QuadMesh);
			Blitter.s_QuadMesh = null;
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002C79C File Offset: 0x0002A99C
		public static Material GetBlitMaterial(TextureDimension dimension, bool singleSlice = false)
		{
			Material material = ((dimension == TextureDimension.Tex2DArray) ? (singleSlice ? Blitter.s_BlitTexArraySingleSlice : Blitter.s_BlitTexArray) : null);
			if (!(material == null))
			{
				return material;
			}
			return Blitter.s_Blit;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0002C7D0 File Offset: 0x0002A9D0
		internal static void DrawTriangle(RasterCommandBuffer cmd, Material material, int shaderPass)
		{
			Blitter.DrawTriangle(cmd.m_WrappedCommandBuffer, material, shaderPass);
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0002C7DF File Offset: 0x0002A9DF
		internal static void DrawTriangle(CommandBuffer cmd, Material material, int shaderPass)
		{
			Blitter.DrawTriangle(cmd, material, shaderPass, Blitter.s_PropertyBlock);
			Blitter.s_PropertyBlock.Clear();
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0002C7F8 File Offset: 0x0002A9F8
		internal static void DrawTriangle(CommandBuffer cmd, Material material, int shaderPass, MaterialPropertyBlock propertyBlock)
		{
			if (SystemInfo.graphicsShaderLevel < 30)
			{
				cmd.DrawMesh(Blitter.s_TriangleMesh, Matrix4x4.identity, material, 0, shaderPass, propertyBlock);
				return;
			}
			cmd.DrawProcedural(Matrix4x4.identity, material, shaderPass, MeshTopology.Triangles, 3, 1, propertyBlock);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0002C829 File Offset: 0x0002AA29
		internal static void DrawQuadMesh(CommandBuffer cmd, Material material, int shaderPass, MaterialPropertyBlock propertyBlock)
		{
			cmd.DrawMesh(Blitter.s_QuadMesh, Matrix4x4.identity, material, 0, shaderPass, propertyBlock);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0002C83F File Offset: 0x0002AA3F
		internal static void DrawQuad(RasterCommandBuffer cmd, Material material, int shaderPass, MaterialPropertyBlock propertyBlock)
		{
			Blitter.DrawQuad(cmd.m_WrappedCommandBuffer, material, shaderPass, propertyBlock);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0002C84F File Offset: 0x0002AA4F
		internal static void DrawQuad(CommandBuffer cmd, Material material, int shaderPass)
		{
			Blitter.DrawQuad(cmd, material, shaderPass, Blitter.s_PropertyBlock);
			Blitter.s_PropertyBlock.Clear();
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0002C868 File Offset: 0x0002AA68
		internal static void DrawQuad(CommandBuffer cmd, Material material, int shaderPass, MaterialPropertyBlock propertyBlock)
		{
			if (SystemInfo.graphicsShaderLevel < 30)
			{
				cmd.DrawMesh(Blitter.s_QuadMesh, Matrix4x4.identity, material, 0, shaderPass, propertyBlock);
				return;
			}
			cmd.DrawProcedural(Matrix4x4.identity, material, shaderPass, MeshTopology.Quads, 4, 1, propertyBlock);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0002C899 File Offset: 0x0002AA99
		internal static bool CanCopyMSAA()
		{
			return Blitter.s_Copy.passCount == 2;
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0002C8A8 File Offset: 0x0002AAA8
		internal static void CopyTexture(RasterCommandBuffer cmd, bool isMSAA)
		{
			Blitter.DrawTriangle(cmd, Blitter.s_Copy, isMSAA ? 1 : 0);
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0002C8BC File Offset: 0x0002AABC
		internal static void BlitTexture(CommandBuffer cmd, RTHandle source, Vector4 scaleBias, float sourceMipLevel, int sourceDepthSlice, bool bilinear)
		{
			Blitter.BlitTexture(cmd, source, scaleBias, Blitter.GetBlitMaterial(TextureDimension.Tex2D, false), Blitter.s_BlitShaderPassIndicesMap[bilinear ? 1 : 0], sourceMipLevel, sourceDepthSlice);
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0002C8DE File Offset: 0x0002AADE
		internal static void BlitTexture(CommandBuffer cmd, RTHandle source, Vector4 scaleBias, Material material, int pass, float sourceMipLevel, int sourceDepthSlice)
		{
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, sourceMipLevel);
			Blitter.s_PropertyBlock.SetInt(Blitter.BlitShaderIDs._BlitTexArraySlice, sourceDepthSlice);
			Blitter.BlitTexture(cmd, source, scaleBias, material, pass);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0002C90D File Offset: 0x0002AB0D
		public static void BlitTexture(RasterCommandBuffer cmd, RTHandle source, Vector4 scaleBias, float mipLevel, bool bilinear)
		{
			Blitter.BlitTexture(cmd.m_WrappedCommandBuffer, source, scaleBias, mipLevel, bilinear);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0002C91F File Offset: 0x0002AB1F
		public static void BlitTexture(CommandBuffer cmd, RTHandle source, Vector4 scaleBias, float mipLevel, bool bilinear)
		{
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, mipLevel);
			Blitter.BlitTexture(cmd, source, scaleBias, Blitter.GetBlitMaterial(TextureXR.dimension, false), Blitter.s_BlitShaderPassIndicesMap[bilinear ? 1 : 0]);
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0002C952 File Offset: 0x0002AB52
		public static void BlitTexture2D(RasterCommandBuffer cmd, RTHandle source, Vector4 scaleBias, float mipLevel, bool bilinear)
		{
			Blitter.BlitTexture2D(cmd.m_WrappedCommandBuffer, source, scaleBias, mipLevel, bilinear);
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002C964 File Offset: 0x0002AB64
		public static void BlitTexture2D(CommandBuffer cmd, RTHandle source, Vector4 scaleBias, float mipLevel, bool bilinear)
		{
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, mipLevel);
			Blitter.BlitTexture(cmd, source, scaleBias, Blitter.GetBlitMaterial(TextureDimension.Tex2D, false), Blitter.s_BlitShaderPassIndicesMap[bilinear ? 1 : 0]);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0002C993 File Offset: 0x0002AB93
		public static void BlitColorAndDepth(RasterCommandBuffer cmd, Texture sourceColor, RenderTexture sourceDepth, Vector4 scaleBias, float mipLevel, bool blitDepth)
		{
			Blitter.BlitColorAndDepth(cmd.m_WrappedCommandBuffer, sourceColor, sourceDepth, scaleBias, mipLevel, blitDepth);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0002C9A8 File Offset: 0x0002ABA8
		public static void BlitColorAndDepth(CommandBuffer cmd, Texture sourceColor, RenderTexture sourceDepth, Vector4 scaleBias, float mipLevel, bool blitDepth)
		{
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, mipLevel);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBias);
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, sourceColor);
			if (blitDepth)
			{
				Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._InputDepth, sourceDepth, RenderTextureSubElement.Depth);
			}
			Blitter.DrawTriangle(cmd, Blitter.s_BlitColorAndDepth, Blitter.s_BlitColorAndDepthShaderPassIndicesMap[blitDepth ? 1 : 0]);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0002CA14 File Offset: 0x0002AC14
		public static void BlitTexture(RasterCommandBuffer cmd, RTHandle source, Vector4 scaleBias, Material material, int pass)
		{
			Blitter.BlitTexture(cmd.m_WrappedCommandBuffer, source, scaleBias, material, pass);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0002CA26 File Offset: 0x0002AC26
		public static void BlitTexture(CommandBuffer cmd, RTHandle source, Vector4 scaleBias, Material material, int pass)
		{
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBias);
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.DrawTriangle(cmd, material, pass);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0002CA56 File Offset: 0x0002AC56
		public static void BlitTexture(RasterCommandBuffer cmd, RenderTargetIdentifier source, Vector4 scaleBias, Material material, int pass)
		{
			Blitter.BlitTexture(cmd.m_WrappedCommandBuffer, source, scaleBias, material, pass);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0002CA68 File Offset: 0x0002AC68
		public static void BlitTexture(CommandBuffer cmd, RenderTargetIdentifier source, Vector4 scaleBias, Material material, int pass)
		{
			Blitter.s_PropertyBlock.Clear();
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBias);
			cmd.SetGlobalTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.DrawTriangle(cmd, material, pass);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0002CA9C File Offset: 0x0002AC9C
		public static void BlitTexture(CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material, int pass)
		{
			Blitter.s_PropertyBlock.Clear();
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, Vector2.one);
			cmd.SetGlobalTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			cmd.SetRenderTarget(destination);
			Blitter.DrawTriangle(cmd, material, pass);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0002CAE8 File Offset: 0x0002ACE8
		public static void BlitTexture(CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction, Material material, int pass)
		{
			Blitter.s_PropertyBlock.Clear();
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, Vector2.one);
			cmd.SetGlobalTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			cmd.SetRenderTarget(destination, loadAction, storeAction);
			Blitter.DrawTriangle(cmd, material, pass);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0002CB38 File Offset: 0x0002AD38
		public static void BlitTexture(CommandBuffer cmd, Vector4 scaleBias, Material material, int pass)
		{
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBias);
			Blitter.DrawTriangle(cmd, material, pass);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0002CB52 File Offset: 0x0002AD52
		public static void BlitTexture(RasterCommandBuffer cmd, Vector4 scaleBias, Material material, int pass)
		{
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBias);
			Blitter.DrawTriangle(cmd, material, pass);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0002CB6C File Offset: 0x0002AD6C
		public static void BlitCameraTexture(CommandBuffer cmd, RTHandle source, RTHandle destination, float mipLevel = 0f, bool bilinear = false)
		{
			Vector2 viewportScale = (source.useScaling ? new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : Vector2.one);
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			Blitter.BlitTexture(cmd, source, viewportScale, mipLevel, bilinear);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0002CBCC File Offset: 0x0002ADCC
		public static void BlitCameraTexture2D(CommandBuffer cmd, RTHandle source, RTHandle destination, float mipLevel = 0f, bool bilinear = false)
		{
			Vector2 viewportScale = (source.useScaling ? new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : Vector2.one);
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			Blitter.BlitTexture2D(cmd, source, viewportScale, mipLevel, bilinear);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0002CC2C File Offset: 0x0002AE2C
		public static void BlitCameraTexture(CommandBuffer cmd, RTHandle source, RTHandle destination, Material material, int pass)
		{
			Vector2 viewportScale = (source.useScaling ? new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : Vector2.one);
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			Blitter.BlitTexture(cmd, source, viewportScale, material, pass);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0002CC8C File Offset: 0x0002AE8C
		public static void BlitCameraTexture(CommandBuffer cmd, RTHandle source, RTHandle destination, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction, Material material, int pass)
		{
			Vector2 viewportScale = (source.useScaling ? new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : Vector2.one);
			CoreUtils.SetRenderTarget(cmd, destination, loadAction, storeAction, ClearFlag.None, Color.clear, 0, CubemapFace.Unknown, -1);
			Blitter.BlitTexture(cmd, source, viewportScale, material, pass);
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0002CCF2 File Offset: 0x0002AEF2
		public static void BlitCameraTexture(CommandBuffer cmd, RTHandle source, RTHandle destination, Vector4 scaleBias, float mipLevel = 0f, bool bilinear = false)
		{
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			Blitter.BlitTexture(cmd, source, scaleBias, mipLevel, bilinear);
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0002CD0C File Offset: 0x0002AF0C
		public static void BlitCameraTexture(CommandBuffer cmd, RTHandle source, RTHandle destination, Rect destViewport, float mipLevel = 0f, bool bilinear = false)
		{
			Vector2 viewportScale = (source.useScaling ? new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y) : Vector2.one);
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			cmd.SetViewport(destViewport);
			Blitter.BlitTexture(cmd, source, viewportScale, mipLevel, bilinear);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0002CD74 File Offset: 0x0002AF74
		public static void BlitQuad(CommandBuffer cmd, Texture source, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), Blitter.s_BlitShaderPassIndicesMap[bilinear ? 3 : 2]);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002CDE4 File Offset: 0x0002AFE4
		public static void BlitQuadWithPadding(CommandBuffer cmd, Texture source, Vector2 textureSize, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			Blitter.s_PropertyBlock.SetInt(Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			if (source.wrapMode == TextureWrapMode.Repeat)
			{
				Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), Blitter.s_BlitShaderPassIndicesMap[bilinear ? 7 : 6]);
				return;
			}
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), Blitter.s_BlitShaderPassIndicesMap[bilinear ? 5 : 4]);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0002CEA4 File Offset: 0x0002B0A4
		public static void BlitQuadWithPaddingMultiply(CommandBuffer cmd, Texture source, Vector2 textureSize, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			Blitter.s_PropertyBlock.SetInt(Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			if (source.wrapMode == TextureWrapMode.Repeat)
			{
				Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), Blitter.s_BlitShaderPassIndicesMap[bilinear ? 12 : 11]);
				return;
			}
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), Blitter.s_BlitShaderPassIndicesMap[bilinear ? 10 : 9]);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002CF68 File Offset: 0x0002B168
		public static void BlitOctahedralWithPadding(CommandBuffer cmd, Texture source, Vector2 textureSize, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			Blitter.s_PropertyBlock.SetInt(Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), Blitter.s_BlitShaderPassIndicesMap[8]);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0002CFF8 File Offset: 0x0002B1F8
		public static void BlitOctahedralWithPaddingMultiply(CommandBuffer cmd, Texture source, Vector2 textureSize, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			Blitter.s_PropertyBlock.SetInt(Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), Blitter.s_BlitShaderPassIndicesMap[13]);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0002D088 File Offset: 0x0002B288
		public static void BlitCubeToOctahedral2DQuad(CommandBuffer cmd, Texture source, Vector4 scaleBiasRT, int mipLevelTex)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitCubeTexture, source);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, new Vector4(1f, 1f, 0f, 0f));
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), Blitter.s_BlitShaderPassIndicesMap[14]);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0002D108 File Offset: 0x0002B308
		public static void BlitCubeToOctahedral2DQuadWithPadding(CommandBuffer cmd, Texture source, Vector2 textureSize, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels, Vector4? decodeInstructions = null)
		{
			Material material = Blitter.GetBlitMaterial(source.dimension, false);
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitCubeTexture, source);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, new Vector4(1f, 1f, 0f, 0f));
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			Blitter.s_PropertyBlock.SetInt(Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			cmd.SetKeyword(material, in Blitter.s_DecodeHdrKeyword, decodeInstructions != null);
			if (decodeInstructions != null)
			{
				Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitDecodeInstructions, decodeInstructions.Value);
			}
			Blitter.DrawQuad(cmd, material, Blitter.s_BlitShaderPassIndicesMap[bilinear ? 22 : 21]);
			cmd.SetKeyword(material, in Blitter.s_DecodeHdrKeyword, false);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0002D1F8 File Offset: 0x0002B3F8
		public static void BlitCubeToOctahedral2DQuadSingleChannel(CommandBuffer cmd, Texture source, Vector4 scaleBiasRT, int mipLevelTex)
		{
			int pass = 15;
			if (GraphicsFormatUtility.GetComponentCount(source.graphicsFormat) == 1U)
			{
				if (GraphicsFormatUtility.IsAlphaOnlyFormat(source.graphicsFormat))
				{
					pass = 16;
				}
				if (GraphicsFormatUtility.GetSwizzleR(source.graphicsFormat) == FormatSwizzle.FormatSwizzleR)
				{
					pass = 17;
				}
			}
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitCubeTexture, source);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, new Vector4(1f, 1f, 0f, 0f));
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), Blitter.s_BlitShaderPassIndicesMap[pass]);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0002D2A8 File Offset: 0x0002B4A8
		public static void BlitQuadSingleChannel(CommandBuffer cmd, Texture source, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex)
		{
			int pass = 18;
			if (GraphicsFormatUtility.GetComponentCount(source.graphicsFormat) == 1U)
			{
				if (GraphicsFormatUtility.IsAlphaOnlyFormat(source.graphicsFormat))
				{
					pass = 19;
				}
				if (GraphicsFormatUtility.GetSwizzleR(source.graphicsFormat) == FormatSwizzle.FormatSwizzleR)
				{
					pass = 20;
				}
			}
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), Blitter.s_BlitShaderPassIndicesMap[pass]);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0002D350 File Offset: 0x0002B550
		[CompilerGenerated]
		internal static Vector3[] <Initialize>g__GetFullScreenTriangleVertexPosition|14_0(float z)
		{
			Vector3[] r = new Vector3[3];
			for (int i = 0; i < 3; i++)
			{
				Vector2 uv = new Vector2((float)((i << 1) & 2), (float)(i & 2));
				r[i] = new Vector3(uv.x * 2f - 1f, uv.y * 2f - 1f, z);
			}
			return r;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0002D3B4 File Offset: 0x0002B5B4
		[CompilerGenerated]
		internal static Vector2[] <Initialize>g__GetFullScreenTriangleTexCoord|14_1()
		{
			Vector2[] r = new Vector2[3];
			for (int i = 0; i < 3; i++)
			{
				if (SystemInfo.graphicsUVStartsAtTop)
				{
					r[i] = new Vector2((float)((i << 1) & 2), 1f - (float)(i & 2));
				}
				else
				{
					r[i] = new Vector2((float)((i << 1) & 2), (float)(i & 2));
				}
			}
			return r;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0002D410 File Offset: 0x0002B610
		[CompilerGenerated]
		internal static Vector3[] <Initialize>g__GetQuadVertexPosition|14_2(float z)
		{
			Vector3[] r = new Vector3[4];
			for (uint i = 0U; i < 4U; i += 1U)
			{
				uint topBit = i >> 1;
				uint botBit = i & 1U;
				float x = topBit;
				float y = (1U - (topBit + botBit)) & 1U;
				r[(int)i] = new Vector3(x, y, z);
			}
			return r;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0002D45C File Offset: 0x0002B65C
		[CompilerGenerated]
		internal static Vector2[] <Initialize>g__GetQuadTexCoord|14_3()
		{
			Vector2[] r = new Vector2[4];
			for (uint i = 0U; i < 4U; i += 1U)
			{
				uint num = i >> 1;
				uint botBit = i & 1U;
				float u = num;
				float v = (num + botBit) & 1U;
				if (SystemInfo.graphicsUVStartsAtTop)
				{
					v = 1f - v;
				}
				r[(int)i] = new Vector2(u, v);
			}
			return r;
		}

		// Token: 0x04000835 RID: 2101
		private static Material s_Copy;

		// Token: 0x04000836 RID: 2102
		private static Material s_Blit;

		// Token: 0x04000837 RID: 2103
		private static Material s_BlitTexArray;

		// Token: 0x04000838 RID: 2104
		private static Material s_BlitTexArraySingleSlice;

		// Token: 0x04000839 RID: 2105
		private static Material s_BlitColorAndDepth;

		// Token: 0x0400083A RID: 2106
		private static MaterialPropertyBlock s_PropertyBlock = new MaterialPropertyBlock();

		// Token: 0x0400083B RID: 2107
		private static Mesh s_TriangleMesh;

		// Token: 0x0400083C RID: 2108
		private static Mesh s_QuadMesh;

		// Token: 0x0400083D RID: 2109
		private static LocalKeyword s_DecodeHdrKeyword;

		// Token: 0x0400083E RID: 2110
		private static int[] s_BlitShaderPassIndicesMap;

		// Token: 0x0400083F RID: 2111
		private static int[] s_BlitColorAndDepthShaderPassIndicesMap;

		// Token: 0x020001AF RID: 431
		private static class BlitShaderIDs
		{
			// Token: 0x04000840 RID: 2112
			public static readonly int _BlitTexture = Shader.PropertyToID("_BlitTexture");

			// Token: 0x04000841 RID: 2113
			public static readonly int _BlitCubeTexture = Shader.PropertyToID("_BlitCubeTexture");

			// Token: 0x04000842 RID: 2114
			public static readonly int _BlitScaleBias = Shader.PropertyToID("_BlitScaleBias");

			// Token: 0x04000843 RID: 2115
			public static readonly int _BlitScaleBiasRt = Shader.PropertyToID("_BlitScaleBiasRt");

			// Token: 0x04000844 RID: 2116
			public static readonly int _BlitMipLevel = Shader.PropertyToID("_BlitMipLevel");

			// Token: 0x04000845 RID: 2117
			public static readonly int _BlitTexArraySlice = Shader.PropertyToID("_BlitTexArraySlice");

			// Token: 0x04000846 RID: 2118
			public static readonly int _BlitTextureSize = Shader.PropertyToID("_BlitTextureSize");

			// Token: 0x04000847 RID: 2119
			public static readonly int _BlitPaddingSize = Shader.PropertyToID("_BlitPaddingSize");

			// Token: 0x04000848 RID: 2120
			public static readonly int _BlitDecodeInstructions = Shader.PropertyToID("_BlitDecodeInstructions");

			// Token: 0x04000849 RID: 2121
			public static readonly int _InputDepth = Shader.PropertyToID("_InputDepthTexture");
		}

		// Token: 0x020001B0 RID: 432
		private enum BlitShaderPassNames
		{
			// Token: 0x0400084B RID: 2123
			Nearest,
			// Token: 0x0400084C RID: 2124
			Bilinear,
			// Token: 0x0400084D RID: 2125
			NearestQuad,
			// Token: 0x0400084E RID: 2126
			BilinearQuad,
			// Token: 0x0400084F RID: 2127
			NearestQuadPadding,
			// Token: 0x04000850 RID: 2128
			BilinearQuadPadding,
			// Token: 0x04000851 RID: 2129
			NearestQuadPaddingRepeat,
			// Token: 0x04000852 RID: 2130
			BilinearQuadPaddingRepeat,
			// Token: 0x04000853 RID: 2131
			BilinearQuadPaddingOctahedral,
			// Token: 0x04000854 RID: 2132
			NearestQuadPaddingAlphaBlend,
			// Token: 0x04000855 RID: 2133
			BilinearQuadPaddingAlphaBlend,
			// Token: 0x04000856 RID: 2134
			NearestQuadPaddingAlphaBlendRepeat,
			// Token: 0x04000857 RID: 2135
			BilinearQuadPaddingAlphaBlendRepeat,
			// Token: 0x04000858 RID: 2136
			BilinearQuadPaddingAlphaBlendOctahedral,
			// Token: 0x04000859 RID: 2137
			CubeToOctahedral,
			// Token: 0x0400085A RID: 2138
			CubeToOctahedralLuminance,
			// Token: 0x0400085B RID: 2139
			CubeToOctahedralAlpha,
			// Token: 0x0400085C RID: 2140
			CubeToOctahedralRed,
			// Token: 0x0400085D RID: 2141
			BilinearQuadLuminance,
			// Token: 0x0400085E RID: 2142
			BilinearQuadAlpha,
			// Token: 0x0400085F RID: 2143
			BilinearQuadRed,
			// Token: 0x04000860 RID: 2144
			NearestCubeToOctahedralPadding,
			// Token: 0x04000861 RID: 2145
			BilinearCubeToOctahedralPadding
		}

		// Token: 0x020001B1 RID: 433
		private enum BlitColorAndDepthPassNames
		{
			// Token: 0x04000863 RID: 2147
			ColorOnly,
			// Token: 0x04000864 RID: 2148
			ColorAndDepth
		}
	}
}
