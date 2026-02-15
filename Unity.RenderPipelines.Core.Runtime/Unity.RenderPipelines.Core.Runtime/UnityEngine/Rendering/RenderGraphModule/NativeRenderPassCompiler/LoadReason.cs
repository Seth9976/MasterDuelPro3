using System;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x02000292 RID: 658
	internal enum LoadReason
	{
		// Token: 0x04000BAA RID: 2986
		InvalidReason,
		// Token: 0x04000BAB RID: 2987
		LoadImported,
		// Token: 0x04000BAC RID: 2988
		LoadPreviouslyWritten,
		// Token: 0x04000BAD RID: 2989
		ClearImported,
		// Token: 0x04000BAE RID: 2990
		ClearCreated,
		// Token: 0x04000BAF RID: 2991
		FullyRewritten,
		// Token: 0x04000BB0 RID: 2992
		Count
	}
}
