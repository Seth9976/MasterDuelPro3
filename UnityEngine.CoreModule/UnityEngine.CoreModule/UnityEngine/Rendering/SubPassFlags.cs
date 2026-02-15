using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200039B RID: 923
	[Flags]
	public enum SubPassFlags
	{
		// Token: 0x04000B71 RID: 2929
		None = 0,
		// Token: 0x04000B72 RID: 2930
		ReadOnlyDepth = 2,
		// Token: 0x04000B73 RID: 2931
		ReadOnlyStencil = 4,
		// Token: 0x04000B74 RID: 2932
		ReadOnlyDepthStencil = 6
	}
}
