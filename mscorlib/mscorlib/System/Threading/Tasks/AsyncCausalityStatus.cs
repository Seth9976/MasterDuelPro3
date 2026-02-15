using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x020002D2 RID: 722
	[FriendAccessAllowed]
	internal enum AsyncCausalityStatus
	{
		// Token: 0x04000C40 RID: 3136
		Started,
		// Token: 0x04000C41 RID: 3137
		Completed,
		// Token: 0x04000C42 RID: 3138
		Canceled,
		// Token: 0x04000C43 RID: 3139
		Error
	}
}
