using System;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x02000294 RID: 660
	internal enum StoreReason
	{
		// Token: 0x04000BB5 RID: 2997
		InvalidReason,
		// Token: 0x04000BB6 RID: 2998
		StoreImported,
		// Token: 0x04000BB7 RID: 2999
		StoreUsedByLaterPass,
		// Token: 0x04000BB8 RID: 3000
		DiscardImported,
		// Token: 0x04000BB9 RID: 3001
		DiscardUnused,
		// Token: 0x04000BBA RID: 3002
		DiscardBindMs,
		// Token: 0x04000BBB RID: 3003
		NoMSAABuffer,
		// Token: 0x04000BBC RID: 3004
		Count
	}
}
