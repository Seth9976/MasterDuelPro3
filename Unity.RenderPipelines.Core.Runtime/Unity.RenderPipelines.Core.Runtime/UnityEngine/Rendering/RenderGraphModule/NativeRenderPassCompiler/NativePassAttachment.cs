using System;
using System.Diagnostics;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x02000291 RID: 657
	[DebuggerDisplay("Res({handle.index}) : {loadAction} : {storeAction} : {memoryless}")]
	internal struct NativePassAttachment
	{
		// Token: 0x04000BA3 RID: 2979
		public ResourceHandle handle;

		// Token: 0x04000BA4 RID: 2980
		public RenderBufferLoadAction loadAction;

		// Token: 0x04000BA5 RID: 2981
		public RenderBufferStoreAction storeAction;

		// Token: 0x04000BA6 RID: 2982
		public bool memoryless;

		// Token: 0x04000BA7 RID: 2983
		public int mipLevel;

		// Token: 0x04000BA8 RID: 2984
		public int depthSlice;
	}
}
