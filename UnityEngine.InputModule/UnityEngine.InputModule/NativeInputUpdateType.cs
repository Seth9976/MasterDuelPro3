using System;

namespace UnityEngineInternal.Input
{
	// Token: 0x02000006 RID: 6
	[Flags]
	internal enum NativeInputUpdateType
	{
		// Token: 0x04000012 RID: 18
		Dynamic = 1,
		// Token: 0x04000013 RID: 19
		Fixed = 2,
		// Token: 0x04000014 RID: 20
		BeforeRender = 4,
		// Token: 0x04000015 RID: 21
		Editor = 8,
		// Token: 0x04000016 RID: 22
		IgnoreFocus = -2147483648
	}
}
