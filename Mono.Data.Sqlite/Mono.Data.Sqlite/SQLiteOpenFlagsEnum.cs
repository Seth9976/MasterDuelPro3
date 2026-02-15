using System;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000006 RID: 6
	[Flags]
	internal enum SQLiteOpenFlagsEnum
	{
		// Token: 0x04000009 RID: 9
		None = 0,
		// Token: 0x0400000A RID: 10
		ReadOnly = 1,
		// Token: 0x0400000B RID: 11
		ReadWrite = 2,
		// Token: 0x0400000C RID: 12
		Create = 4,
		// Token: 0x0400000D RID: 13
		SharedCache = 16777216,
		// Token: 0x0400000E RID: 14
		Default = 6
	}
}
