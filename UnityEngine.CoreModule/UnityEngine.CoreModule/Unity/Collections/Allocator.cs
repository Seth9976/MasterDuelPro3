using System;
using UnityEngine.Scripting;

namespace Unity.Collections
{
	// Token: 0x02000052 RID: 82
	[UsedByNativeCode]
	public enum Allocator
	{
		// Token: 0x040000E7 RID: 231
		Invalid,
		// Token: 0x040000E8 RID: 232
		None,
		// Token: 0x040000E9 RID: 233
		Temp,
		// Token: 0x040000EA RID: 234
		TempJob,
		// Token: 0x040000EB RID: 235
		Persistent,
		// Token: 0x040000EC RID: 236
		AudioKernel,
		// Token: 0x040000ED RID: 237
		Domain,
		// Token: 0x040000EE RID: 238
		FirstUserIndex = 64
	}
}
