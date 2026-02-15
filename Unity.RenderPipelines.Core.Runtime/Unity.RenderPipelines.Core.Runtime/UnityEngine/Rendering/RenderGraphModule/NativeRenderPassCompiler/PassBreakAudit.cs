using System;
using System.Diagnostics;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x02000297 RID: 663
	[DebuggerDisplay("{reason} : {breakPass}")]
	internal struct PassBreakAudit
	{
		// Token: 0x060011BF RID: 4543 RVA: 0x00042DD4 File Offset: 0x00040FD4
		public PassBreakAudit(PassBreakReason reason, int breakPass)
		{
			this.reason = reason;
			this.breakPass = breakPass;
		}

		// Token: 0x04000BCE RID: 3022
		public PassBreakReason reason;

		// Token: 0x04000BCF RID: 3023
		public int breakPass;

		// Token: 0x04000BD0 RID: 3024
		public static readonly string[] BreakReasonMessages = new string[]
		{
			"The native render pass optimizer never ran on this pass. Pass is standalone and not merged.",
			"The render target sizes of the next pass do not match.",
			"The next pass reads data output by this pass as a regular texture.",
			"The next pass is not a raster render pass.",
			"The next pass uses a different depth buffer. All passes in the native render pass need to use the same depth buffer.",
			string.Format("The limit of {0} native pass attachments would be exceeded when merging with the next pass.", 8),
			string.Format("The limit of {0} native subpasses would be exceeded when merging with the next pass.", 8),
			"This is the last pass in the graph, there are no other passes to merge.",
			"The the next pass uses a different foveated rendering state",
			"The next pass got merged into this pass."
		};
	}
}
