using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace UnityEngine.Rendering.RenderGraphModule.Util
{
	// Token: 0x02000279 RID: 633
	public static class RenderGraphUtils
	{
		// Token: 0x0600114A RID: 4426 RVA: 0x0003E578 File Offset: 0x0003C778
		public static bool CanAddCopyPassMSAA()
		{
			return Blitter.CanCopyMSAA();
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0003E580 File Offset: 0x0003C780
		public static void AddCopyPass(this RenderGraph graph, TextureHandle source, TextureHandle destination, int sourceSlice = 0, int destinationSlice = 0, int sourceMip = 0, int destinationMip = 0, string passName = "Copy Pass Utility", [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			if (!graph.nativeRenderPassesEnabled)
			{
				throw new ArgumentException("CopyPass only supported for native render pass. Please use the blit functions instead for non native render pass platforms.");
			}
			TextureDesc sourceDesc = graph.GetTextureDesc(source);
			TextureDesc destinationDesc = graph.GetTextureDesc(destination);
			if (sourceSlice < 0 || sourceSlice >= sourceDesc.slices)
			{
				throw new ArgumentException("Invalid sourceSlice.");
			}
			if (destinationSlice < 0 || destinationSlice >= destinationDesc.slices)
			{
				throw new ArgumentException("Invalid destinationSlice.");
			}
			int sourceMaxWidth = math.max(math.max(sourceDesc.width, sourceDesc.height), sourceDesc.slices);
			math.log2((float)sourceMaxWidth);
			if (sourceMip < 0 || sourceMip >= sourceMaxWidth)
			{
				throw new ArgumentException("Invalid sourceMip.");
			}
			int destinationMaxWidth = math.max(math.max(destinationDesc.width, destinationDesc.height), destinationDesc.slices);
			math.log2((float)destinationMaxWidth);
			if (destinationMip < 0 || destinationMip >= destinationMaxWidth)
			{
				throw new ArgumentException("Invalid destinationMip.");
			}
			if (sourceDesc.msaaSamples != destinationDesc.msaaSamples)
			{
				throw new ArgumentException("MSAA samples from source and destination texture doesn't match.");
			}
			bool isMSAA = sourceDesc.msaaSamples > MSAASamples.None;
			if (isMSAA && !Blitter.CanCopyMSAA())
			{
				throw new ArgumentException("Target does not support MSAA for AddCopyPass. Please use the blit alternative or use non MSAA textures.");
			}
			RenderGraphUtils.CopyPassData passData;
			using (IRasterRenderGraphBuilder builder = graph.AddRasterRenderPass<RenderGraphUtils.CopyPassData>(passName, out passData, file, line))
			{
				passData.isMSAA = isMSAA;
				builder.SetInputAttachment(source, 0, AccessFlags.Read, sourceMip, sourceSlice);
				builder.SetRenderAttachment(destination, 0, AccessFlags.Write, destinationMip, destinationSlice);
				builder.SetRenderFunc<RenderGraphUtils.CopyPassData>(delegate(RenderGraphUtils.CopyPassData data, RasterGraphContext context)
				{
					RenderGraphUtils.CopyRenderFunc(data, context);
				});
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0003E708 File Offset: 0x0003C908
		private static void CopyRenderFunc(RenderGraphUtils.CopyPassData data, RasterGraphContext rgContext)
		{
			Blitter.CopyTexture(rgContext.cmd, data.isMSAA);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0003E71C File Offset: 0x0003C91C
		public static void AddBlitPass(this RenderGraph graph, TextureHandle source, TextureHandle destination, Vector2 scale, Vector2 offset, int sourceSlice = 0, int destinationSlice = 0, int numSlices = -1, int sourceMip = 0, int destinationMip = 0, int numMips = 1, RenderGraphUtils.BlitFilterMode filterMode = RenderGraphUtils.BlitFilterMode.ClampBilinear, string passName = "Blit Pass Utility", [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			TextureDesc sourceDesc = graph.GetTextureDesc(source);
			TextureDesc destinationDesc = graph.GetTextureDesc(destination);
			int sourceTotalMipChainLevels = (int)math.log2((float)math.max(math.max(sourceDesc.width, sourceDesc.height), sourceDesc.slices)) + 1;
			int destinationTotalMipChainLevels = (int)math.log2((float)math.max(math.max(destinationDesc.width, destinationDesc.height), destinationDesc.slices)) + 1;
			if (numSlices == -1)
			{
				numSlices = sourceDesc.slices - sourceSlice;
			}
			if (numSlices > sourceDesc.slices - sourceSlice || numSlices > destinationDesc.slices - destinationSlice)
			{
				throw new ArgumentException("BlitPass: " + passName + " attempts to blit too many slices. The pass will be skipped.");
			}
			if (numMips == -1)
			{
				numMips = sourceTotalMipChainLevels - sourceMip;
			}
			if (numMips > sourceTotalMipChainLevels - sourceMip || numMips > destinationTotalMipChainLevels - destinationMip)
			{
				throw new ArgumentException("BlitPass: " + passName + " attempts to blit too many mips. The pass will be skipped.");
			}
			RenderGraphUtils.BlitPassData passData;
			using (IUnsafeRenderGraphBuilder builder = graph.AddUnsafePass<RenderGraphUtils.BlitPassData>(passName, out passData, file, line))
			{
				passData.source = source;
				passData.destination = destination;
				passData.scale = scale;
				passData.offset = offset;
				passData.sourceSlice = sourceSlice;
				passData.destinationSlice = destinationSlice;
				passData.numSlices = numSlices;
				passData.sourceMip = sourceMip;
				passData.destinationMip = destinationMip;
				passData.numMips = numMips;
				passData.filterMode = filterMode;
				builder.UseTexture(in source, AccessFlags.Read);
				builder.UseTexture(in destination, AccessFlags.Write);
				builder.SetRenderFunc<RenderGraphUtils.BlitPassData>(delegate(RenderGraphUtils.BlitPassData data, UnsafeGraphContext context)
				{
					RenderGraphUtils.BlitRenderFunc(data, context);
				});
			}
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0003E8C0 File Offset: 0x0003CAC0
		private static void BlitRenderFunc(RenderGraphUtils.BlitPassData data, UnsafeGraphContext context)
		{
			RenderGraphUtils.s_BlitScaleBias.x = data.scale.x;
			RenderGraphUtils.s_BlitScaleBias.y = data.scale.y;
			RenderGraphUtils.s_BlitScaleBias.z = data.offset.x;
			RenderGraphUtils.s_BlitScaleBias.w = data.offset.y;
			CommandBuffer unsafeCmd = CommandBufferHelpers.GetNativeCommandBuffer(context.cmd);
			for (int currSlice = 0; currSlice < data.numSlices; currSlice++)
			{
				for (int currMip = 0; currMip < data.numMips; currMip++)
				{
					context.cmd.SetRenderTarget(data.destination, data.destinationMip + currMip, CubemapFace.Unknown, data.destinationSlice + currSlice);
					Blitter.BlitTexture(unsafeCmd, data.source, RenderGraphUtils.s_BlitScaleBias, (float)(data.sourceMip + currMip), data.sourceSlice + currSlice, data.filterMode == RenderGraphUtils.BlitFilterMode.ClampBilinear);
				}
			}
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0003E9A8 File Offset: 0x0003CBA8
		public static void AddBlitPass(this RenderGraph graph, RenderGraphUtils.BlitMaterialParameters blitParameters, string passName = "Blit Pass Utility w. Material", [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
		{
			TextureDesc sourceDesc = graph.GetTextureDesc(blitParameters.source);
			TextureDesc destinationDesc = graph.GetTextureDesc(blitParameters.destination);
			int sourceTotalMipChainLevels = (int)math.log2((float)math.max(math.max(sourceDesc.width, sourceDesc.height), sourceDesc.slices)) + 1;
			int destinationTotalMipChainLevels = (int)math.log2((float)math.max(math.max(destinationDesc.width, destinationDesc.height), destinationDesc.slices)) + 1;
			if (blitParameters.numSlices == -1)
			{
				blitParameters.numSlices = destinationDesc.slices - blitParameters.destinationSlice;
			}
			if (blitParameters.numSlices > destinationDesc.slices - blitParameters.destinationSlice || (blitParameters.sourceSlice != -1 && blitParameters.numSlices > sourceDesc.slices - blitParameters.sourceSlice))
			{
				throw new ArgumentException("BlitPass: " + passName + " attempts to blit too many slices. The pass will be skipped.");
			}
			if (blitParameters.numMips == -1)
			{
				blitParameters.numMips = destinationTotalMipChainLevels - blitParameters.destinationMip;
			}
			if (blitParameters.numMips > destinationTotalMipChainLevels - blitParameters.destinationMip || (blitParameters.sourceMip != -1 && blitParameters.numMips > sourceTotalMipChainLevels - blitParameters.sourceMip))
			{
				throw new ArgumentException("BlitPass: " + passName + " attempts to blit too many mips. The pass will be skipped.");
			}
			RenderGraphUtils.BlitMaterialPassData passData;
			using (IUnsafeRenderGraphBuilder builder = graph.AddUnsafePass<RenderGraphUtils.BlitMaterialPassData>(passName, out passData, file, line))
			{
				passData.sourceTexturePropertyID = blitParameters.sourceTexturePropertyID;
				passData.source = blitParameters.source;
				passData.destination = blitParameters.destination;
				passData.scale = blitParameters.scale;
				passData.offset = blitParameters.offset;
				passData.material = blitParameters.material;
				passData.shaderPass = blitParameters.shaderPass;
				passData.propertyBlock = blitParameters.propertyBlock;
				passData.sourceSlice = blitParameters.sourceSlice;
				passData.destinationSlice = blitParameters.destinationSlice;
				passData.numSlices = blitParameters.numSlices;
				passData.sourceMip = blitParameters.sourceMip;
				passData.destinationMip = blitParameters.destinationMip;
				passData.numMips = blitParameters.numMips;
				passData.geometry = blitParameters.geometry;
				passData.sourceSlicePropertyID = blitParameters.sourceSlicePropertyID;
				passData.sourceMipPropertyID = blitParameters.sourceMipPropertyID;
				passData.scaleBiasPropertyID = blitParameters.scaleBiasPropertyID;
				builder.UseTexture(in blitParameters.source, AccessFlags.Read);
				builder.UseTexture(in blitParameters.destination, AccessFlags.Write);
				builder.SetRenderFunc<RenderGraphUtils.BlitMaterialPassData>(delegate(RenderGraphUtils.BlitMaterialPassData data, UnsafeGraphContext context)
				{
					RenderGraphUtils.BlitMaterialRenderFunc(data, context);
				});
			}
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0003EC40 File Offset: 0x0003CE40
		private static void BlitMaterialRenderFunc(RenderGraphUtils.BlitMaterialPassData data, UnsafeGraphContext context)
		{
			RenderGraphUtils.s_BlitScaleBias.x = data.scale.x;
			RenderGraphUtils.s_BlitScaleBias.y = data.scale.y;
			RenderGraphUtils.s_BlitScaleBias.z = data.offset.x;
			RenderGraphUtils.s_BlitScaleBias.w = data.offset.y;
			CommandBuffer unsafeCmd = CommandBufferHelpers.GetNativeCommandBuffer(context.cmd);
			if (data.propertyBlock == null)
			{
				data.propertyBlock = RenderGraphUtils.s_PropertyBlock;
			}
			data.propertyBlock.SetTexture(data.sourceTexturePropertyID, data.source);
			if (data.sourceSlice == -1)
			{
				data.propertyBlock.SetInt(data.sourceSlicePropertyID, 0);
			}
			if (data.sourceMip == -1)
			{
				data.propertyBlock.SetInt(data.sourceMipPropertyID, 0);
			}
			data.propertyBlock.SetVector(data.scaleBiasPropertyID, RenderGraphUtils.s_BlitScaleBias);
			for (int currSlice = 0; currSlice < data.numSlices; currSlice++)
			{
				for (int currMip = 0; currMip < data.numMips; currMip++)
				{
					if (data.sourceSlice != -1)
					{
						data.propertyBlock.SetInt(data.sourceSlicePropertyID, data.sourceSlice + currSlice);
					}
					if (data.sourceMip != -1)
					{
						data.propertyBlock.SetInt(data.sourceMipPropertyID, data.sourceMip + currMip);
					}
					context.cmd.SetRenderTarget(data.destination, data.destinationMip + currMip, CubemapFace.Unknown, data.destinationSlice + currSlice);
					switch (data.geometry)
					{
					case RenderGraphUtils.FullScreenGeometryType.Mesh:
						Blitter.DrawQuadMesh(unsafeCmd, data.material, data.shaderPass, data.propertyBlock);
						break;
					case RenderGraphUtils.FullScreenGeometryType.ProceduralTriangle:
						Blitter.DrawTriangle(unsafeCmd, data.material, data.shaderPass, data.propertyBlock);
						break;
					case RenderGraphUtils.FullScreenGeometryType.ProceduralQuad:
						Blitter.DrawQuad(unsafeCmd, data.material, data.shaderPass, data.propertyBlock);
						break;
					}
				}
			}
		}

		// Token: 0x04000AF6 RID: 2806
		private static MaterialPropertyBlock s_PropertyBlock = new MaterialPropertyBlock();

		// Token: 0x04000AF7 RID: 2807
		private static Vector4 s_BlitScaleBias = default(Vector4);

		// Token: 0x0200027A RID: 634
		private class CopyPassData
		{
			// Token: 0x04000AF8 RID: 2808
			public bool isMSAA;
		}

		// Token: 0x0200027B RID: 635
		public enum BlitFilterMode
		{
			// Token: 0x04000AFA RID: 2810
			ClampNearest,
			// Token: 0x04000AFB RID: 2811
			ClampBilinear
		}

		// Token: 0x0200027C RID: 636
		private class BlitPassData
		{
			// Token: 0x04000AFC RID: 2812
			public TextureHandle source;

			// Token: 0x04000AFD RID: 2813
			public TextureHandle destination;

			// Token: 0x04000AFE RID: 2814
			public Vector2 scale;

			// Token: 0x04000AFF RID: 2815
			public Vector2 offset;

			// Token: 0x04000B00 RID: 2816
			public int sourceSlice;

			// Token: 0x04000B01 RID: 2817
			public int destinationSlice;

			// Token: 0x04000B02 RID: 2818
			public int numSlices;

			// Token: 0x04000B03 RID: 2819
			public int sourceMip;

			// Token: 0x04000B04 RID: 2820
			public int destinationMip;

			// Token: 0x04000B05 RID: 2821
			public int numMips;

			// Token: 0x04000B06 RID: 2822
			public RenderGraphUtils.BlitFilterMode filterMode;
		}

		// Token: 0x0200027D RID: 637
		public enum FullScreenGeometryType
		{
			// Token: 0x04000B08 RID: 2824
			Mesh,
			// Token: 0x04000B09 RID: 2825
			ProceduralTriangle,
			// Token: 0x04000B0A RID: 2826
			ProceduralQuad
		}

		// Token: 0x0200027E RID: 638
		public struct BlitMaterialParameters
		{
			// Token: 0x06001154 RID: 4436 RVA: 0x0003EE3F File Offset: 0x0003D03F
			public BlitMaterialParameters(TextureHandle source, TextureHandle destination, Material material, int shaderPass)
			{
				this = new RenderGraphUtils.BlitMaterialParameters(source, destination, Vector2.one, Vector2.zero, material, shaderPass);
			}

			// Token: 0x06001155 RID: 4437 RVA: 0x0003EE58 File Offset: 0x0003D058
			public BlitMaterialParameters(TextureHandle source, TextureHandle destination, Vector2 scale, Vector2 offset, Material material, int shaderPass)
			{
				this.source = source;
				this.destination = destination;
				this.scale = scale;
				this.offset = offset;
				this.sourceSlice = -1;
				this.destinationSlice = 0;
				this.numSlices = 1;
				this.sourceMip = -1;
				this.destinationMip = 0;
				this.numMips = 1;
				this.material = material;
				this.shaderPass = shaderPass;
				this.propertyBlock = null;
				this.sourceTexturePropertyID = RenderGraphUtils.BlitMaterialParameters.blitTextureProperty;
				this.sourceSlicePropertyID = RenderGraphUtils.BlitMaterialParameters.blitSliceProperty;
				this.sourceMipPropertyID = RenderGraphUtils.BlitMaterialParameters.blitMipProperty;
				this.scaleBiasPropertyID = RenderGraphUtils.BlitMaterialParameters.blitScaleBias;
				this.geometry = RenderGraphUtils.FullScreenGeometryType.ProceduralTriangle;
			}

			// Token: 0x06001156 RID: 4438 RVA: 0x0003EEF8 File Offset: 0x0003D0F8
			public BlitMaterialParameters(TextureHandle source, TextureHandle destination, Material material, int shaderPass, MaterialPropertyBlock mpb, int destinationSlice, int destinationMip, int numSlices = 1, int numMips = 1, int sourceSlice = -1, int sourceMip = -1, RenderGraphUtils.FullScreenGeometryType geometry = RenderGraphUtils.FullScreenGeometryType.Mesh, int sourceTexturePropertyID = -1, int sourceSlicePropertyID = -1, int sourceMipPropertyID = -1)
			{
				this = new RenderGraphUtils.BlitMaterialParameters(source, destination, Vector2.one, Vector2.zero, material, shaderPass, mpb, destinationSlice, destinationMip, numSlices, numMips, sourceSlice, sourceMip, geometry, sourceTexturePropertyID, sourceSlicePropertyID, sourceMipPropertyID, -1);
			}

			// Token: 0x06001157 RID: 4439 RVA: 0x0003EF34 File Offset: 0x0003D134
			public BlitMaterialParameters(TextureHandle source, TextureHandle destination, Vector2 scale, Vector2 offset, Material material, int shaderPass, MaterialPropertyBlock mpb, int destinationSlice, int destinationMip, int numSlices = 1, int numMips = 1, int sourceSlice = -1, int sourceMip = -1, RenderGraphUtils.FullScreenGeometryType geometry = RenderGraphUtils.FullScreenGeometryType.Mesh, int sourceTexturePropertyID = -1, int sourceSlicePropertyID = -1, int sourceMipPropertyID = -1, int scaleBiasPropertyID = -1)
			{
				this = new RenderGraphUtils.BlitMaterialParameters(source, destination, scale, offset, material, shaderPass);
				this.propertyBlock = mpb;
				this.sourceSlice = sourceSlice;
				this.destinationSlice = destinationSlice;
				this.numSlices = numSlices;
				this.sourceMip = sourceMip;
				this.destinationMip = destinationMip;
				this.numMips = numMips;
				if (sourceTexturePropertyID != -1)
				{
					this.sourceTexturePropertyID = sourceTexturePropertyID;
				}
				if (sourceSlicePropertyID != -1)
				{
					this.sourceSlicePropertyID = sourceSlicePropertyID;
				}
				if (sourceMipPropertyID != -1)
				{
					this.sourceMipPropertyID = sourceMipPropertyID;
				}
				if (scaleBiasPropertyID != -1)
				{
					this.scaleBiasPropertyID = scaleBiasPropertyID;
				}
				this.geometry = geometry;
			}

			// Token: 0x06001158 RID: 4440 RVA: 0x0003EFC4 File Offset: 0x0003D1C4
			public BlitMaterialParameters(TextureHandle source, TextureHandle destination, Material material, int shaderPass, MaterialPropertyBlock mpb, RenderGraphUtils.FullScreenGeometryType geometry = RenderGraphUtils.FullScreenGeometryType.Mesh, int sourceTexturePropertyID = -1, int sourceSlicePropertyID = -1, int sourceMipPropertyID = -1)
			{
				this = new RenderGraphUtils.BlitMaterialParameters(source, destination, Vector2.one, Vector2.zero, material, shaderPass, mpb, geometry, sourceTexturePropertyID, sourceSlicePropertyID, sourceMipPropertyID, -1);
			}

			// Token: 0x06001159 RID: 4441 RVA: 0x0003EFF4 File Offset: 0x0003D1F4
			public BlitMaterialParameters(TextureHandle source, TextureHandle destination, Vector2 scale, Vector2 offset, Material material, int shaderPass, MaterialPropertyBlock mpb, RenderGraphUtils.FullScreenGeometryType geometry = RenderGraphUtils.FullScreenGeometryType.Mesh, int sourceTexturePropertyID = -1, int sourceSlicePropertyID = -1, int sourceMipPropertyID = -1, int scaleBiasPropertyID = -1)
			{
				this = new RenderGraphUtils.BlitMaterialParameters(source, destination, scale, offset, material, shaderPass);
				this.propertyBlock = mpb;
				if (sourceTexturePropertyID != -1)
				{
					this.sourceTexturePropertyID = sourceTexturePropertyID;
				}
				if (sourceSlicePropertyID != -1)
				{
					this.sourceSlicePropertyID = sourceSlicePropertyID;
				}
				if (sourceMipPropertyID != -1)
				{
					this.sourceMipPropertyID = sourceMipPropertyID;
				}
				if (scaleBiasPropertyID != -1)
				{
					this.scaleBiasPropertyID = scaleBiasPropertyID;
				}
				this.geometry = geometry;
			}

			// Token: 0x04000B0B RID: 2827
			private static readonly int blitTextureProperty = Shader.PropertyToID("_BlitTexture");

			// Token: 0x04000B0C RID: 2828
			private static readonly int blitSliceProperty = Shader.PropertyToID("_BlitTexArraySlice");

			// Token: 0x04000B0D RID: 2829
			private static readonly int blitMipProperty = Shader.PropertyToID("_BlitMipLevel");

			// Token: 0x04000B0E RID: 2830
			private static readonly int blitScaleBias = Shader.PropertyToID("_BlitScaleBias");

			// Token: 0x04000B0F RID: 2831
			public TextureHandle source;

			// Token: 0x04000B10 RID: 2832
			public TextureHandle destination;

			// Token: 0x04000B11 RID: 2833
			public Vector2 scale;

			// Token: 0x04000B12 RID: 2834
			public Vector2 offset;

			// Token: 0x04000B13 RID: 2835
			public int sourceSlice;

			// Token: 0x04000B14 RID: 2836
			public int destinationSlice;

			// Token: 0x04000B15 RID: 2837
			public int numSlices;

			// Token: 0x04000B16 RID: 2838
			public int sourceMip;

			// Token: 0x04000B17 RID: 2839
			public int destinationMip;

			// Token: 0x04000B18 RID: 2840
			public int numMips;

			// Token: 0x04000B19 RID: 2841
			public Material material;

			// Token: 0x04000B1A RID: 2842
			public int shaderPass;

			// Token: 0x04000B1B RID: 2843
			public MaterialPropertyBlock propertyBlock;

			// Token: 0x04000B1C RID: 2844
			public int sourceTexturePropertyID;

			// Token: 0x04000B1D RID: 2845
			public int sourceSlicePropertyID;

			// Token: 0x04000B1E RID: 2846
			public int sourceMipPropertyID;

			// Token: 0x04000B1F RID: 2847
			public int scaleBiasPropertyID;

			// Token: 0x04000B20 RID: 2848
			public RenderGraphUtils.FullScreenGeometryType geometry;
		}

		// Token: 0x0200027F RID: 639
		private class BlitMaterialPassData
		{
			// Token: 0x04000B21 RID: 2849
			public int sourceTexturePropertyID;

			// Token: 0x04000B22 RID: 2850
			public TextureHandle source;

			// Token: 0x04000B23 RID: 2851
			public TextureHandle destination;

			// Token: 0x04000B24 RID: 2852
			public Vector2 scale;

			// Token: 0x04000B25 RID: 2853
			public Vector2 offset;

			// Token: 0x04000B26 RID: 2854
			public Material material;

			// Token: 0x04000B27 RID: 2855
			public int shaderPass;

			// Token: 0x04000B28 RID: 2856
			public MaterialPropertyBlock propertyBlock;

			// Token: 0x04000B29 RID: 2857
			public int sourceSlice;

			// Token: 0x04000B2A RID: 2858
			public int destinationSlice;

			// Token: 0x04000B2B RID: 2859
			public int numSlices;

			// Token: 0x04000B2C RID: 2860
			public int sourceMip;

			// Token: 0x04000B2D RID: 2861
			public int destinationMip;

			// Token: 0x04000B2E RID: 2862
			public int numMips;

			// Token: 0x04000B2F RID: 2863
			public RenderGraphUtils.FullScreenGeometryType geometry;

			// Token: 0x04000B30 RID: 2864
			public int sourceSlicePropertyID;

			// Token: 0x04000B31 RID: 2865
			public int sourceMipPropertyID;

			// Token: 0x04000B32 RID: 2866
			public int scaleBiasPropertyID;
		}
	}
}
