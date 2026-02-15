using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000071 RID: 113
	[Flags]
	internal enum InstanceFlags : byte
	{
		// Token: 0x04000227 RID: 551
		None = 0,
		// Token: 0x04000228 RID: 552
		AffectsLightmaps = 1,
		// Token: 0x04000229 RID: 553
		IsShadowsOff = 2,
		// Token: 0x0400022A RID: 554
		IsShadowsOnly = 4,
		// Token: 0x0400022B RID: 555
		HasProgressiveLod = 8,
		// Token: 0x0400022C RID: 556
		SmallMeshCulling = 16
	}
}
