using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000351 RID: 849
	[Flags]
	public enum RTClearFlags
	{
		// Token: 0x040009ED RID: 2541
		None = 0,
		// Token: 0x040009EE RID: 2542
		Color = 1,
		// Token: 0x040009EF RID: 2543
		Depth = 2,
		// Token: 0x040009F0 RID: 2544
		Stencil = 4,
		// Token: 0x040009F1 RID: 2545
		All = 7,
		// Token: 0x040009F2 RID: 2546
		DepthStencil = 6,
		// Token: 0x040009F3 RID: 2547
		ColorDepth = 3,
		// Token: 0x040009F4 RID: 2548
		ColorStencil = 5
	}
}
