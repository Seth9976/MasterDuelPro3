using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x02000199 RID: 409
	public struct RTHandleAllocInfo
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x000284DA File Offset: 0x000266DA
		// (set) Token: 0x06000B2C RID: 2860 RVA: 0x000284E2 File Offset: 0x000266E2
		public int slices { readonly get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x000284EB File Offset: 0x000266EB
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x000284F3 File Offset: 0x000266F3
		public GraphicsFormat format { readonly get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x000284FC File Offset: 0x000266FC
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x00028504 File Offset: 0x00026704
		public FilterMode filterMode { readonly get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0002850D File Offset: 0x0002670D
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x00028515 File Offset: 0x00026715
		public TextureWrapMode wrapModeU { readonly get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0002851E File Offset: 0x0002671E
		// (set) Token: 0x06000B34 RID: 2868 RVA: 0x00028526 File Offset: 0x00026726
		public TextureWrapMode wrapModeV { readonly get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0002852F File Offset: 0x0002672F
		// (set) Token: 0x06000B36 RID: 2870 RVA: 0x00028537 File Offset: 0x00026737
		public TextureWrapMode wrapModeW { readonly get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00028540 File Offset: 0x00026740
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x00028548 File Offset: 0x00026748
		public TextureDimension dimension { readonly get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00028551 File Offset: 0x00026751
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x00028559 File Offset: 0x00026759
		public bool enableRandomWrite { readonly get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00028562 File Offset: 0x00026762
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x0002856A File Offset: 0x0002676A
		public bool useMipMap { readonly get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00028573 File Offset: 0x00026773
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x0002857B File Offset: 0x0002677B
		public bool autoGenerateMips { readonly get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00028584 File Offset: 0x00026784
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x0002858C File Offset: 0x0002678C
		public int anisoLevel { readonly get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00028595 File Offset: 0x00026795
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x0002859D File Offset: 0x0002679D
		public float mipMapBias { readonly get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x000285A6 File Offset: 0x000267A6
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x000285AE File Offset: 0x000267AE
		public MSAASamples msaaSamples { readonly get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x000285B7 File Offset: 0x000267B7
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x000285BF File Offset: 0x000267BF
		public bool bindTextureMS { readonly get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x000285C8 File Offset: 0x000267C8
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x000285D0 File Offset: 0x000267D0
		public bool useDynamicScale { readonly get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x000285D9 File Offset: 0x000267D9
		// (set) Token: 0x06000B4A RID: 2890 RVA: 0x000285E1 File Offset: 0x000267E1
		public RenderTextureMemoryless memoryless { readonly get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x000285EA File Offset: 0x000267EA
		// (set) Token: 0x06000B4C RID: 2892 RVA: 0x000285F2 File Offset: 0x000267F2
		public VRTextureUsage vrUsage { readonly get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x000285FB File Offset: 0x000267FB
		// (set) Token: 0x06000B4E RID: 2894 RVA: 0x00028603 File Offset: 0x00026803
		public string name { readonly get; set; }

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002860C File Offset: 0x0002680C
		public RTHandleAllocInfo(string name = "")
		{
			this.slices = 1;
			this.format = GraphicsFormat.R8G8B8A8_SRGB;
			this.filterMode = FilterMode.Point;
			this.wrapModeU = TextureWrapMode.Repeat;
			this.wrapModeV = TextureWrapMode.Repeat;
			this.wrapModeW = TextureWrapMode.Repeat;
			this.dimension = TextureDimension.Tex2D;
			this.enableRandomWrite = false;
			this.useMipMap = false;
			this.autoGenerateMips = true;
			this.anisoLevel = 1;
			this.mipMapBias = 0f;
			this.msaaSamples = MSAASamples.None;
			this.bindTextureMS = false;
			this.useDynamicScale = false;
			this.memoryless = RenderTextureMemoryless.None;
			this.vrUsage = VRTextureUsage.None;
			this.name = name;
		}
	}
}
