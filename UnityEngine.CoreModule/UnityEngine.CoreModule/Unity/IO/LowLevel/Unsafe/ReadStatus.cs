using System;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x0200003D RID: 61
	public enum ReadStatus
	{
		// Token: 0x040000B4 RID: 180
		Complete,
		// Token: 0x040000B5 RID: 181
		InProgress,
		// Token: 0x040000B6 RID: 182
		Failed,
		// Token: 0x040000B7 RID: 183
		Truncated = 4,
		// Token: 0x040000B8 RID: 184
		Canceled
	}
}
