using System;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x02000296 RID: 662
	internal enum PassBreakReason
	{
		// Token: 0x04000BC3 RID: 3011
		NotOptimized,
		// Token: 0x04000BC4 RID: 3012
		TargetSizeMismatch,
		// Token: 0x04000BC5 RID: 3013
		NextPassReadsTexture,
		// Token: 0x04000BC6 RID: 3014
		NonRasterPass,
		// Token: 0x04000BC7 RID: 3015
		DifferentDepthTextures,
		// Token: 0x04000BC8 RID: 3016
		AttachmentLimitReached,
		// Token: 0x04000BC9 RID: 3017
		SubPassLimitReached,
		// Token: 0x04000BCA RID: 3018
		EndOfGraph,
		// Token: 0x04000BCB RID: 3019
		FRStateMismatch,
		// Token: 0x04000BCC RID: 3020
		Merged,
		// Token: 0x04000BCD RID: 3021
		Count
	}
}
