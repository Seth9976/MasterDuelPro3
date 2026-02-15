using System;

namespace System.Threading
{
	// Token: 0x02000263 RID: 611
	[Serializable]
	internal enum StackCrawlMark
	{
		// Token: 0x04000AEF RID: 2799
		LookForMe,
		// Token: 0x04000AF0 RID: 2800
		LookForMyCaller,
		// Token: 0x04000AF1 RID: 2801
		LookForMyCallersCaller,
		// Token: 0x04000AF2 RID: 2802
		LookForThread
	}
}
