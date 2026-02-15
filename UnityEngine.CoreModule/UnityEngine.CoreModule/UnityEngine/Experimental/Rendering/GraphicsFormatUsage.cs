using System;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003FA RID: 1018
	[Flags]
	public enum GraphicsFormatUsage
	{
		// Token: 0x04000D9D RID: 3485
		None = 0,
		// Token: 0x04000D9E RID: 3486
		Sample = 1,
		// Token: 0x04000D9F RID: 3487
		Linear = 2,
		// Token: 0x04000DA0 RID: 3488
		Sparse = 4,
		// Token: 0x04000DA1 RID: 3489
		Render = 16,
		// Token: 0x04000DA2 RID: 3490
		Blend = 32,
		// Token: 0x04000DA3 RID: 3491
		GetPixels = 64,
		// Token: 0x04000DA4 RID: 3492
		SetPixels = 128,
		// Token: 0x04000DA5 RID: 3493
		SetPixels32 = 256,
		// Token: 0x04000DA6 RID: 3494
		ReadPixels = 512,
		// Token: 0x04000DA7 RID: 3495
		LoadStore = 1024,
		// Token: 0x04000DA8 RID: 3496
		MSAA2x = 2048,
		// Token: 0x04000DA9 RID: 3497
		MSAA4x = 4096,
		// Token: 0x04000DAA RID: 3498
		MSAA8x = 8192,
		// Token: 0x04000DAB RID: 3499
		StencilSampling = 65536
	}
}
