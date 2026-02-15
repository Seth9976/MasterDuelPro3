using System;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	[Flags]
	public enum EventModifiers
	{
		// Token: 0x0400002E RID: 46
		None = 0,
		// Token: 0x0400002F RID: 47
		Shift = 1,
		// Token: 0x04000030 RID: 48
		Control = 2,
		// Token: 0x04000031 RID: 49
		Alt = 4,
		// Token: 0x04000032 RID: 50
		Command = 8,
		// Token: 0x04000033 RID: 51
		Numeric = 16,
		// Token: 0x04000034 RID: 52
		CapsLock = 32,
		// Token: 0x04000035 RID: 53
		FunctionKey = 64
	}
}
