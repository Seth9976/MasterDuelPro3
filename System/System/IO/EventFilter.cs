using System;

namespace System.IO
{
	// Token: 0x0200035E RID: 862
	internal enum EventFilter : short
	{
		// Token: 0x04000C9F RID: 3231
		Read = -1,
		// Token: 0x04000CA0 RID: 3232
		Write = -2,
		// Token: 0x04000CA1 RID: 3233
		Aio = -3,
		// Token: 0x04000CA2 RID: 3234
		Vnode = -4,
		// Token: 0x04000CA3 RID: 3235
		Proc = -5,
		// Token: 0x04000CA4 RID: 3236
		Signal = -6,
		// Token: 0x04000CA5 RID: 3237
		Timer = -7,
		// Token: 0x04000CA6 RID: 3238
		MachPort = -8,
		// Token: 0x04000CA7 RID: 3239
		FS = -9,
		// Token: 0x04000CA8 RID: 3240
		User = -10,
		// Token: 0x04000CA9 RID: 3241
		VM = -11
	}
}
