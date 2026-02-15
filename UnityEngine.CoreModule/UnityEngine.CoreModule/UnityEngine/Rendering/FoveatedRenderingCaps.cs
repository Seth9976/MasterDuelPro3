using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200034E RID: 846
	[Flags]
	public enum FoveatedRenderingCaps
	{
		// Token: 0x040009E2 RID: 2530
		None = 0,
		// Token: 0x040009E3 RID: 2531
		FoveationImage = 1,
		// Token: 0x040009E4 RID: 2532
		NonUniformRaster = 2,
		// Token: 0x040009E5 RID: 2533
		ModeChangeOnlyBeforeRenderTargetSet = 4
	}
}
