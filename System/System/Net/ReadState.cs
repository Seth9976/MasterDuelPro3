using System;

namespace System.Net
{
	// Token: 0x02000438 RID: 1080
	internal enum ReadState
	{
		// Token: 0x040011D3 RID: 4563
		None,
		// Token: 0x040011D4 RID: 4564
		Status,
		// Token: 0x040011D5 RID: 4565
		Headers,
		// Token: 0x040011D6 RID: 4566
		Content,
		// Token: 0x040011D7 RID: 4567
		Aborted
	}
}
