using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002B6 RID: 694
	[Flags]
	internal enum InternalTaskOptions
	{
		// Token: 0x04000BF9 RID: 3065
		None = 0,
		// Token: 0x04000BFA RID: 3066
		InternalOptionsMask = 65280,
		// Token: 0x04000BFB RID: 3067
		ContinuationTask = 512,
		// Token: 0x04000BFC RID: 3068
		PromiseTask = 1024,
		// Token: 0x04000BFD RID: 3069
		LazyCancellation = 4096,
		// Token: 0x04000BFE RID: 3070
		QueuedByRuntime = 8192,
		// Token: 0x04000BFF RID: 3071
		DoNotDispose = 16384
	}
}
