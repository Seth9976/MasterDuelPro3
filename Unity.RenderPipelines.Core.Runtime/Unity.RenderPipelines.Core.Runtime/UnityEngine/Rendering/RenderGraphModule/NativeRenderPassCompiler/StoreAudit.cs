using System;
using System.Diagnostics;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x02000295 RID: 661
	[DebuggerDisplay("{reason} : {passId} / MSAA {msaaReason} : {msaaPassId}")]
	internal struct StoreAudit
	{
		// Token: 0x060011BD RID: 4541 RVA: 0x00042D65 File Offset: 0x00040F65
		public StoreAudit(StoreReason setReason, int setPassId = -1, StoreReason setMsaaReason = StoreReason.NoMSAABuffer, int setMsaaPassId = -1)
		{
			this.reason = setReason;
			this.passId = setPassId;
			this.msaaReason = setMsaaReason;
			this.msaaPassId = setMsaaPassId;
		}

		// Token: 0x04000BBD RID: 3005
		public static readonly string[] StoreReasonMessages = new string[] { "Invalid reason", "The resource is imported in the graph. The data is stored so results are available outside the graph.", "The resource is read by pass {pass} executed later in the graph. The data is stored.", "The resource is imported but the import was with the 'discard on last use' option enabled. The data is discarded.", "The resource is written by this pass but no later passes are using the results. The data is discarded.", "The resource was created as MSAA only resource, the data can never be resolved.", "The resource is a single sample resource, there is no multi-sample data to handle." };

		// Token: 0x04000BBE RID: 3006
		public StoreReason reason;

		// Token: 0x04000BBF RID: 3007
		public int passId;

		// Token: 0x04000BC0 RID: 3008
		public StoreReason msaaReason;

		// Token: 0x04000BC1 RID: 3009
		public int msaaPassId;
	}
}
