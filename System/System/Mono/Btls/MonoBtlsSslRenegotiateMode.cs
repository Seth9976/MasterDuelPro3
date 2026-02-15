using System;

namespace Mono.Btls
{
	// Token: 0x020000B6 RID: 182
	[Flags]
	internal enum MonoBtlsSslRenegotiateMode
	{
		// Token: 0x040002A4 RID: 676
		NEVER = 0,
		// Token: 0x040002A5 RID: 677
		ONCE = 1,
		// Token: 0x040002A6 RID: 678
		FREELY = 2,
		// Token: 0x040002A7 RID: 679
		IGNORE = 3
	}
}
