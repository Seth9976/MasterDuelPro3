using System;

namespace System
{
	// Token: 0x020000FD RID: 253
	[Flags]
	internal enum UnescapeMode
	{
		// Token: 0x04000431 RID: 1073
		CopyOnly = 0,
		// Token: 0x04000432 RID: 1074
		Escape = 1,
		// Token: 0x04000433 RID: 1075
		Unescape = 2,
		// Token: 0x04000434 RID: 1076
		EscapeUnescape = 3,
		// Token: 0x04000435 RID: 1077
		V1ToStringFlag = 4,
		// Token: 0x04000436 RID: 1078
		UnescapeAll = 8,
		// Token: 0x04000437 RID: 1079
		UnescapeAllOrThrow = 24
	}
}
