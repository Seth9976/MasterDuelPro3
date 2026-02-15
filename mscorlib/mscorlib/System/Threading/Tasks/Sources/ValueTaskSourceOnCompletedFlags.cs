using System;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x020002D6 RID: 726
	[Flags]
	public enum ValueTaskSourceOnCompletedFlags
	{
		// Token: 0x04000C4F RID: 3151
		None = 0,
		// Token: 0x04000C50 RID: 3152
		UseSchedulingContext = 1,
		// Token: 0x04000C51 RID: 3153
		FlowExecutionContext = 2
	}
}
