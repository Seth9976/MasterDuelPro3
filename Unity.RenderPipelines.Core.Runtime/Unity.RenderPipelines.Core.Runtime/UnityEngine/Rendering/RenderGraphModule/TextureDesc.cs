using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000272 RID: 626
	public struct TextureDesc
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x0003D9B7 File Offset: 0x0003BBB7
		// (set) Token: 0x0600110C RID: 4364 RVA: 0x0003D9C4 File Offset: 0x0003BBC4
		public DepthBits depthBufferBits
		{
			get
			{
				return (DepthBits)GraphicsFormatUtility.GetDepthBits(this.format);
			}
			set
			{
				if (value != DepthBits.None)
				{
					this.format = GraphicsFormatUtility.GetDepthStencilFormat((int)value);
					return;
				}
				if (!GraphicsFormatUtility.IsDepthStencilFormat(this.format))
				{
					return;
				}
				this.format = GraphicsFormat.None;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x0003D9EB File Offset: 0x0003BBEB
		// (set) Token: 0x0600110E RID: 4366 RVA: 0x0003DA02 File Offset: 0x0003BC02
		public GraphicsFormat colorFormat
		{
			get
			{
				if (!GraphicsFormatUtility.IsDepthStencilFormat(this.format))
				{
					return this.format;
				}
				return GraphicsFormat.None;
			}
			set
			{
				this.format = value;
			}
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0003DA0B File Offset: 0x0003BC0B
		private void InitDefaultValues(bool dynamicResolution, bool xrReady)
		{
			this.useDynamicScale = dynamicResolution;
			this.vrUsage = VRTextureUsage.None;
			if (xrReady)
			{
				this.slices = TextureXR.slices;
				this.dimension = TextureXR.dimension;
			}
			else
			{
				this.slices = 1;
				this.dimension = TextureDimension.Tex2D;
			}
			this.discardBuffer = false;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0003DA4B File Offset: 0x0003BC4B
		public TextureDesc(int width, int height, bool dynamicResolution = false, bool xrReady = false)
		{
			this = default(TextureDesc);
			this.sizeMode = TextureSizeMode.Explicit;
			this.width = width;
			this.height = height;
			this.msaaSamples = MSAASamples.None;
			this.InitDefaultValues(dynamicResolution, xrReady);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0003DA79 File Offset: 0x0003BC79
		public TextureDesc(Vector2 scale, bool dynamicResolution = false, bool xrReady = false)
		{
			this = default(TextureDesc);
			this.sizeMode = TextureSizeMode.Scale;
			this.scale = scale;
			this.msaaSamples = MSAASamples.None;
			this.dimension = TextureDimension.Tex2D;
			this.InitDefaultValues(dynamicResolution, xrReady);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0003DAA6 File Offset: 0x0003BCA6
		public TextureDesc(ScaleFunc func, bool dynamicResolution = false, bool xrReady = false)
		{
			this = default(TextureDesc);
			this.sizeMode = TextureSizeMode.Functor;
			this.func = func;
			this.msaaSamples = MSAASamples.None;
			this.dimension = TextureDimension.Tex2D;
			this.InitDefaultValues(dynamicResolution, xrReady);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0003DAD3 File Offset: 0x0003BCD3
		public TextureDesc(TextureDesc input)
		{
			this = input;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0003DADC File Offset: 0x0003BCDC
		public TextureDesc(RenderTextureDescriptor input)
		{
			this.sizeMode = TextureSizeMode.Explicit;
			this.width = input.width;
			this.height = input.height;
			this.slices = input.volumeDepth;
			this.scale = Vector2.one;
			this.func = null;
			this.format = ((input.depthStencilFormat != GraphicsFormat.None) ? input.depthStencilFormat : input.graphicsFormat);
			this.filterMode = FilterMode.Bilinear;
			this.wrapMode = TextureWrapMode.Clamp;
			this.dimension = input.dimension;
			this.enableRandomWrite = input.enableRandomWrite;
			this.useMipMap = input.useMipMap;
			this.autoGenerateMips = input.autoGenerateMips;
			this.isShadowMap = input.shadowSamplingMode != ShadowSamplingMode.None;
			this.anisoLevel = 1;
			this.mipMapBias = 0f;
			this.msaaSamples = (MSAASamples)input.msaaSamples;
			this.bindTextureMS = input.bindMS;
			this.useDynamicScale = input.useDynamicScale;
			this.useDynamicScaleExplicit = false;
			this.memoryless = input.memoryless;
			this.vrUsage = input.vrUsage;
			this.name = "UnNamedFromRenderTextureDescriptor";
			this.fastMemoryDesc = default(FastMemoryDesc);
			this.fastMemoryDesc.inFastMemory = false;
			this.fallBackToBlackTexture = false;
			this.disableFallBackToImportedTexture = true;
			this.clearBuffer = true;
			this.clearColor = Color.black;
			this.discardBuffer = false;
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0003DC44 File Offset: 0x0003BE44
		public TextureDesc(RenderTexture input)
		{
			this = new TextureDesc(input.descriptor);
			this.filterMode = input.filterMode;
			this.wrapMode = input.wrapMode;
			this.anisoLevel = input.anisoLevel;
			this.mipMapBias = input.mipMapBias;
			this.name = "UnNamedFromRenderTextureDescriptor";
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0003DC98 File Offset: 0x0003BE98
		public override int GetHashCode()
		{
			HashFNV1A32 hashCode = HashFNV1A32.Create();
			switch (this.sizeMode)
			{
			case TextureSizeMode.Explicit:
				hashCode.Append(in this.width);
				hashCode.Append(in this.height);
				break;
			case TextureSizeMode.Scale:
				hashCode.Append(in this.scale);
				break;
			case TextureSizeMode.Functor:
				if (this.func != null)
				{
					hashCode.Append(this.func);
				}
				break;
			}
			hashCode.Append(in this.mipMapBias);
			hashCode.Append(in this.slices);
			int num = (int)this.format;
			hashCode.Append(in num);
			num = (int)this.filterMode;
			hashCode.Append(in num);
			num = (int)this.wrapMode;
			hashCode.Append(in num);
			num = (int)this.dimension;
			hashCode.Append(in num);
			num = (int)this.memoryless;
			hashCode.Append(in num);
			num = (int)this.vrUsage;
			hashCode.Append(in num);
			hashCode.Append(in this.anisoLevel);
			hashCode.Append(in this.enableRandomWrite);
			hashCode.Append(in this.useMipMap);
			hashCode.Append(in this.autoGenerateMips);
			hashCode.Append(in this.isShadowMap);
			hashCode.Append(in this.bindTextureMS);
			hashCode.Append(in this.useDynamicScale);
			num = (int)this.msaaSamples;
			hashCode.Append(in num);
			hashCode.Append(in this.fastMemoryDesc.inFastMemory);
			return hashCode.value;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0003DE04 File Offset: 0x0003C004
		public Vector2Int CalculateFinalDimensions()
		{
			Vector2Int vector2Int;
			switch (this.sizeMode)
			{
			case TextureSizeMode.Explicit:
				vector2Int = new Vector2Int(this.width, this.height);
				break;
			case TextureSizeMode.Scale:
				vector2Int = RTHandles.CalculateDimensions(this.scale);
				break;
			case TextureSizeMode.Functor:
				vector2Int = RTHandles.CalculateDimensions(this.func);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return vector2Int;
		}

		// Token: 0x04000ABE RID: 2750
		public TextureSizeMode sizeMode;

		// Token: 0x04000ABF RID: 2751
		public int width;

		// Token: 0x04000AC0 RID: 2752
		public int height;

		// Token: 0x04000AC1 RID: 2753
		public int slices;

		// Token: 0x04000AC2 RID: 2754
		public Vector2 scale;

		// Token: 0x04000AC3 RID: 2755
		public ScaleFunc func;

		// Token: 0x04000AC4 RID: 2756
		public GraphicsFormat format;

		// Token: 0x04000AC5 RID: 2757
		public FilterMode filterMode;

		// Token: 0x04000AC6 RID: 2758
		public TextureWrapMode wrapMode;

		// Token: 0x04000AC7 RID: 2759
		public TextureDimension dimension;

		// Token: 0x04000AC8 RID: 2760
		public bool enableRandomWrite;

		// Token: 0x04000AC9 RID: 2761
		public bool useMipMap;

		// Token: 0x04000ACA RID: 2762
		public bool autoGenerateMips;

		// Token: 0x04000ACB RID: 2763
		public bool isShadowMap;

		// Token: 0x04000ACC RID: 2764
		public int anisoLevel;

		// Token: 0x04000ACD RID: 2765
		public float mipMapBias;

		// Token: 0x04000ACE RID: 2766
		public MSAASamples msaaSamples;

		// Token: 0x04000ACF RID: 2767
		public bool bindTextureMS;

		// Token: 0x04000AD0 RID: 2768
		public bool useDynamicScale;

		// Token: 0x04000AD1 RID: 2769
		public bool useDynamicScaleExplicit;

		// Token: 0x04000AD2 RID: 2770
		public RenderTextureMemoryless memoryless;

		// Token: 0x04000AD3 RID: 2771
		public VRTextureUsage vrUsage;

		// Token: 0x04000AD4 RID: 2772
		public string name;

		// Token: 0x04000AD5 RID: 2773
		public FastMemoryDesc fastMemoryDesc;

		// Token: 0x04000AD6 RID: 2774
		public bool fallBackToBlackTexture;

		// Token: 0x04000AD7 RID: 2775
		public bool disableFallBackToImportedTexture;

		// Token: 0x04000AD8 RID: 2776
		public bool clearBuffer;

		// Token: 0x04000AD9 RID: 2777
		public Color clearColor;

		// Token: 0x04000ADA RID: 2778
		public bool discardBuffer;
	}
}
