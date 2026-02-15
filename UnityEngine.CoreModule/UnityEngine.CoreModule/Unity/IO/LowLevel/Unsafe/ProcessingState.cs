using System;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000042 RID: 66
	[NativeHeader("Runtime/File/AsyncReadManagerMetrics.h")]
	public enum ProcessingState
	{
		// Token: 0x040000C1 RID: 193
		Unknown,
		// Token: 0x040000C2 RID: 194
		InQueue,
		// Token: 0x040000C3 RID: 195
		Reading,
		// Token: 0x040000C4 RID: 196
		Completed,
		// Token: 0x040000C5 RID: 197
		Failed,
		// Token: 0x040000C6 RID: 198
		Canceled
	}
}
