using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000343 RID: 835
	[Flags]
	public enum RenderTargetFlags
	{
		// Token: 0x04000985 RID: 2437
		None = 0,
		// Token: 0x04000986 RID: 2438
		ReadOnlyDepth = 1,
		// Token: 0x04000987 RID: 2439
		ReadOnlyStencil = 2,
		// Token: 0x04000988 RID: 2440
		ReadOnlyDepthStencil = 3
	}
}
