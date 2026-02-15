using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000070 RID: 112
	[Flags]
	internal enum TransformUpdateFlags : byte
	{
		// Token: 0x04000223 RID: 547
		None = 0,
		// Token: 0x04000224 RID: 548
		HasLightProbeCombined = 1,
		// Token: 0x04000225 RID: 549
		IsPartOfStaticBatch = 2
	}
}
