using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003D5 RID: 981
	[Flags]
	internal enum VisibleLightFlags
	{
		// Token: 0x04000CDD RID: 3293
		IntersectsNearPlane = 1,
		// Token: 0x04000CDE RID: 3294
		IntersectsFarPlane = 2,
		// Token: 0x04000CDF RID: 3295
		ForcedVisible = 4
	}
}
