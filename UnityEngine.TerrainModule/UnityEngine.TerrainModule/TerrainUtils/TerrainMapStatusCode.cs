using System;

namespace UnityEngine.TerrainUtils
{
	// Token: 0x02000008 RID: 8
	internal enum TerrainMapStatusCode
	{
		// Token: 0x04000016 RID: 22
		OK,
		// Token: 0x04000017 RID: 23
		Overlapping,
		// Token: 0x04000018 RID: 24
		SizeMismatch = 4,
		// Token: 0x04000019 RID: 25
		EdgeAlignmentMismatch = 8
	}
}
