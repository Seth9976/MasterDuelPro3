using System;

namespace UnityEngine.ResourceManagement.Profiling
{
	// Token: 0x0200006D RID: 109
	[Flags]
	internal enum ContentStatus
	{
		// Token: 0x04000118 RID: 280
		None = 0,
		// Token: 0x04000119 RID: 281
		Queue = 2,
		// Token: 0x0400011A RID: 282
		Downloading = 4,
		// Token: 0x0400011B RID: 283
		Released = 16,
		// Token: 0x0400011C RID: 284
		Loading = 64,
		// Token: 0x0400011D RID: 285
		Active = 256
	}
}
