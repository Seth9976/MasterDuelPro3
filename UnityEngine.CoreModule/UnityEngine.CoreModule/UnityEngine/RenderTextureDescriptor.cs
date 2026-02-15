using System;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000133 RID: 307
	public struct RenderTextureDescriptor
	{
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0001828F File Offset: 0x0001648F
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x00018297 File Offset: 0x00016497
		public int width { readonly get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x000182A0 File Offset: 0x000164A0
		// (set) Token: 0x06000CD9 RID: 3289 RVA: 0x000182A8 File Offset: 0x000164A8
		public int height { readonly get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x000182B1 File Offset: 0x000164B1
		// (set) Token: 0x06000CDB RID: 3291 RVA: 0x000182B9 File Offset: 0x000164B9
		public int msaaSamples { readonly get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x000182C2 File Offset: 0x000164C2
		// (set) Token: 0x06000CDD RID: 3293 RVA: 0x000182CA File Offset: 0x000164CA
		public int volumeDepth { readonly get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x000182D3 File Offset: 0x000164D3
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x000182DB File Offset: 0x000164DB
		public int mipCount { readonly get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x000182E4 File Offset: 0x000164E4
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x000182FC File Offset: 0x000164FC
		public GraphicsFormat graphicsFormat
		{
			get
			{
				return this._graphicsFormat;
			}
			set
			{
				this._graphicsFormat = value;
				this.SetOrClearRenderTextureCreationFlag(GraphicsFormatUtility.IsSRGBFormat(value), RenderTextureCreationFlags.SRGB);
			}
		}

		// Token: 0x17000210 RID: 528
		// (set) Token: 0x06000CE2 RID: 3298 RVA: 0x00018314 File Offset: 0x00016514
		public GraphicsFormat stencilFormat
		{
			[CompilerGenerated]
			set
			{
				this.<stencilFormat>k__BackingField = value;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0001831D File Offset: 0x0001651D
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x00018325 File Offset: 0x00016525
		public GraphicsFormat depthStencilFormat { readonly get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00018330 File Offset: 0x00016530
		// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x0001836C File Offset: 0x0001656C
		public RenderTextureFormat colorFormat
		{
			get
			{
				bool flag = this.graphicsFormat > GraphicsFormat.None;
				RenderTextureFormat renderTextureFormat;
				if (flag)
				{
					renderTextureFormat = GraphicsFormatUtility.GetRenderTextureFormat(this.graphicsFormat);
				}
				else
				{
					renderTextureFormat = ((this.shadowSamplingMode != ShadowSamplingMode.None) ? RenderTextureFormat.Shadowmap : RenderTextureFormat.Depth);
				}
				return renderTextureFormat;
			}
			set
			{
				this.shadowSamplingMode = RenderTexture.GetShadowSamplingModeForFormat(value);
				GraphicsFormat requestedFormat = GraphicsFormatUtility.GetGraphicsFormat(value, this.sRGB);
				this.graphicsFormat = SystemInfo.GetCompatibleFormat(requestedFormat, GraphicsFormatUsage.Render);
				this.depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(this.depthBufferBits, this.shadowSamplingMode);
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x000183BC File Offset: 0x000165BC
		// (set) Token: 0x06000CE8 RID: 3304 RVA: 0x000183D9 File Offset: 0x000165D9
		public bool sRGB
		{
			get
			{
				return GraphicsFormatUtility.IsSRGBFormat(this.graphicsFormat);
			}
			set
			{
				this.graphicsFormat = ((value && QualitySettings.activeColorSpace == ColorSpace.Linear) ? GraphicsFormatUtility.GetSRGBFormat(this.graphicsFormat) : GraphicsFormatUtility.GetLinearFormat(this.graphicsFormat));
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00018408 File Offset: 0x00016608
		public int depthBufferBits
		{
			get
			{
				return GraphicsFormatUtility.GetDepthBits(this.depthStencilFormat);
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00018425 File Offset: 0x00016625
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x0001842D File Offset: 0x0001662D
		public TextureDimension dimension { readonly get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x00018436 File Offset: 0x00016636
		// (set) Token: 0x06000CED RID: 3309 RVA: 0x0001843E File Offset: 0x0001663E
		public ShadowSamplingMode shadowSamplingMode { readonly get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00018447 File Offset: 0x00016647
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x0001844F File Offset: 0x0001664F
		public VRTextureUsage vrUsage { readonly get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00018458 File Offset: 0x00016658
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x00018460 File Offset: 0x00016660
		public RenderTextureMemoryless memoryless { readonly get; set; }

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00018469 File Offset: 0x00016669
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height)
		{
			this = new RenderTextureDescriptor(width, height, RenderTextureFormat.Default);
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00018476 File Offset: 0x00016676
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, 0);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00018484 File Offset: 0x00016684
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat, int depthBufferBits)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, depthBufferBits, Texture.GenerateAllMips);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00018498 File Offset: 0x00016698
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, int depthBufferBits)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, depthBufferBits, Texture.GenerateAllMips);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x000184AC File Offset: 0x000166AC
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat, int depthBufferBits, int mipCount)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, depthBufferBits, mipCount, RenderTextureReadWrite.Linear);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x000184C0 File Offset: 0x000166C0
		public RenderTextureDescriptor(int width, int height, [DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat colorFormat, [DefaultValue("0")] int depthBufferBits, [DefaultValue("Texture.GenerateAllMips")] int mipCount, [DefaultValue("RenderTextureReadWrite.Linear")] RenderTextureReadWrite readWrite)
		{
			GraphicsFormat compatibleFormat = RenderTexture.GetCompatibleFormat(colorFormat, readWrite);
			this = new RenderTextureDescriptor(width, height, compatibleFormat, RenderTexture.GetDepthStencilFormatLegacy(depthBufferBits, colorFormat, false), mipCount);
			this.shadowSamplingMode = RenderTexture.GetShadowSamplingModeForFormat(colorFormat);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00018500 File Offset: 0x00016700
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, int depthBufferBits, int mipCount)
		{
			this = default(RenderTextureDescriptor);
			this._flags = RenderTextureCreationFlags.AutoGenerateMips | RenderTextureCreationFlags.AllowVerticalFlip;
			this.width = width;
			this.height = height;
			this.volumeDepth = 1;
			this.msaaSamples = 1;
			this.graphicsFormat = colorFormat;
			this.depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(depthBufferBits, colorFormat);
			this.mipCount = mipCount;
			this.dimension = TextureDimension.Tex2D;
			this.shadowSamplingMode = ShadowSamplingMode.None;
			this.vrUsage = VRTextureUsage.None;
			this.memoryless = RenderTextureMemoryless.None;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00018580 File Offset: 0x00016780
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, depthStencilFormat, Texture.GenerateAllMips);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00018594 File Offset: 0x00016794
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat, int mipCount)
		{
			this = default(RenderTextureDescriptor);
			this._flags = RenderTextureCreationFlags.AutoGenerateMips | RenderTextureCreationFlags.AllowVerticalFlip;
			this.width = width;
			this.height = height;
			this.volumeDepth = 1;
			this.msaaSamples = 1;
			this.graphicsFormat = colorFormat;
			this.depthStencilFormat = depthStencilFormat;
			this.mipCount = mipCount;
			this.dimension = TextureDimension.Tex2D;
			this.shadowSamplingMode = ShadowSamplingMode.None;
			this.vrUsage = VRTextureUsage.None;
			this.memoryless = RenderTextureMemoryless.None;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00018610 File Offset: 0x00016810
		private void SetOrClearRenderTextureCreationFlag(bool value, RenderTextureCreationFlags flag)
		{
			if (value)
			{
				this._flags |= flag;
			}
			else
			{
				this._flags &= ~flag;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00018648 File Offset: 0x00016848
		// (set) Token: 0x06000CFD RID: 3325 RVA: 0x00018665 File Offset: 0x00016865
		public bool useMipMap
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.MipMap) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.MipMap);
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00018674 File Offset: 0x00016874
		// (set) Token: 0x06000CFF RID: 3327 RVA: 0x00018691 File Offset: 0x00016891
		public bool autoGenerateMips
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.AutoGenerateMips) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.AutoGenerateMips);
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x000186A0 File Offset: 0x000168A0
		// (set) Token: 0x06000D01 RID: 3329 RVA: 0x000186BE File Offset: 0x000168BE
		public bool enableRandomWrite
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.EnableRandomWrite) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.EnableRandomWrite);
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x000186CC File Offset: 0x000168CC
		// (set) Token: 0x06000D03 RID: 3331 RVA: 0x000186ED File Offset: 0x000168ED
		public bool bindMS
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.BindMS) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.BindMS);
			}
		}

		// Token: 0x1700021D RID: 541
		// (set) Token: 0x06000D04 RID: 3332 RVA: 0x000186FD File Offset: 0x000168FD
		internal bool createdFromScript
		{
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.CreatedFromScript);
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x0001870C File Offset: 0x0001690C
		// (set) Token: 0x06000D06 RID: 3334 RVA: 0x0001872D File Offset: 0x0001692D
		public bool useDynamicScale
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.DynamicallyScalable) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.DynamicallyScalable);
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00018740 File Offset: 0x00016940
		// (set) Token: 0x06000D08 RID: 3336 RVA: 0x00018761 File Offset: 0x00016961
		public bool useDynamicScaleExplicit
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.DynamicallyScalableExplicit) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.DynamicallyScalableExplicit);
			}
		}

		// Token: 0x04000406 RID: 1030
		private GraphicsFormat _graphicsFormat;

		// Token: 0x0400040C RID: 1036
		private RenderTextureCreationFlags _flags;
	}
}
