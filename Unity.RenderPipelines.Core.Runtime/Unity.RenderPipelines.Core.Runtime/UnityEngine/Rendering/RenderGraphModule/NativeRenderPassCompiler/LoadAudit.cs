using System;
using System.Diagnostics;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x02000293 RID: 659
	[DebuggerDisplay("{reason} : {passId}")]
	internal struct LoadAudit
	{
		// Token: 0x060011BB RID: 4539 RVA: 0x00042D18 File Offset: 0x00040F18
		public LoadAudit(LoadReason setReason, int setPassId = -1)
		{
			this.reason = setReason;
			this.passId = setPassId;
		}

		// Token: 0x04000BB1 RID: 2993
		public static readonly string[] LoadReasonMessages = new string[] { "Invalid reason", "The resource is imported in the graph and loaded to retrieve the existing buffer contents.", "The resource is written by {pass} executed previously in the graph. The data is loaded.", "The resource is imported in the graph but was imported with the 'clear on first use' option enabled. The data is cleared.", "The resource is created in this pass and cleared on first use.", "The pass indicated it will rewrite the full resource contents. Existing contents are not loaded or cleared." };

		// Token: 0x04000BB2 RID: 2994
		public LoadReason reason;

		// Token: 0x04000BB3 RID: 2995
		public int passId;
	}
}
