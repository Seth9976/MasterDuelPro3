using System;

namespace System.Runtime
{
	// Token: 0x02000025 RID: 37
	internal enum TraceEventOpcode
	{
		// Token: 0x0400004C RID: 76
		Info,
		// Token: 0x0400004D RID: 77
		Start,
		// Token: 0x0400004E RID: 78
		Stop,
		// Token: 0x0400004F RID: 79
		Reply = 6,
		// Token: 0x04000050 RID: 80
		Resume,
		// Token: 0x04000051 RID: 81
		Suspend,
		// Token: 0x04000052 RID: 82
		Send,
		// Token: 0x04000053 RID: 83
		Receive = 240
	}
}
