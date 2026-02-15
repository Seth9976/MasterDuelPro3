using System;

namespace System.IO
{
	// Token: 0x0200035D RID: 861
	[Flags]
	internal enum EventFlags : ushort
	{
		// Token: 0x04000C91 RID: 3217
		Add = 1,
		// Token: 0x04000C92 RID: 3218
		Delete = 2,
		// Token: 0x04000C93 RID: 3219
		Enable = 4,
		// Token: 0x04000C94 RID: 3220
		Disable = 8,
		// Token: 0x04000C95 RID: 3221
		OneShot = 16,
		// Token: 0x04000C96 RID: 3222
		Clear = 32,
		// Token: 0x04000C97 RID: 3223
		Receipt = 64,
		// Token: 0x04000C98 RID: 3224
		Dispatch = 128,
		// Token: 0x04000C99 RID: 3225
		Flag0 = 4096,
		// Token: 0x04000C9A RID: 3226
		Flag1 = 8192,
		// Token: 0x04000C9B RID: 3227
		SystemFlags = 61440,
		// Token: 0x04000C9C RID: 3228
		EOF = 32768,
		// Token: 0x04000C9D RID: 3229
		Error = 16384
	}
}
