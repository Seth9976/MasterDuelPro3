using System;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003F9 RID: 1017
	[Obsolete("Use GraphicsFormatUsage instead", false)]
	public enum FormatUsage
	{
		// Token: 0x04000D8E RID: 3470
		Sample,
		// Token: 0x04000D8F RID: 3471
		Linear,
		// Token: 0x04000D90 RID: 3472
		Sparse,
		// Token: 0x04000D91 RID: 3473
		Render = 4,
		// Token: 0x04000D92 RID: 3474
		Blend,
		// Token: 0x04000D93 RID: 3475
		GetPixels,
		// Token: 0x04000D94 RID: 3476
		SetPixels,
		// Token: 0x04000D95 RID: 3477
		SetPixels32,
		// Token: 0x04000D96 RID: 3478
		ReadPixels,
		// Token: 0x04000D97 RID: 3479
		LoadStore,
		// Token: 0x04000D98 RID: 3480
		MSAA2x,
		// Token: 0x04000D99 RID: 3481
		MSAA4x,
		// Token: 0x04000D9A RID: 3482
		MSAA8x,
		// Token: 0x04000D9B RID: 3483
		StencilSampling = 16
	}
}
