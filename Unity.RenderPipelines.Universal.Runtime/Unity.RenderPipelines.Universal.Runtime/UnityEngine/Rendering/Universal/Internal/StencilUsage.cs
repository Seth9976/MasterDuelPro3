using System;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000228 RID: 552
	internal enum StencilUsage
	{
		// Token: 0x04000DE5 RID: 3557
		UserMask = 15,
		// Token: 0x04000DE6 RID: 3558
		StencilLight,
		// Token: 0x04000DE7 RID: 3559
		MaterialMask = 96,
		// Token: 0x04000DE8 RID: 3560
		MaterialUnlit = 0,
		// Token: 0x04000DE9 RID: 3561
		MaterialLit = 32,
		// Token: 0x04000DEA RID: 3562
		MaterialSimpleLit = 64
	}
}
