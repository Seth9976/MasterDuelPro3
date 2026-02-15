using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000338 RID: 824
	[Flags]
	public enum ShadowMapPass
	{
		// Token: 0x04000913 RID: 2323
		PointlightPositiveX = 1,
		// Token: 0x04000914 RID: 2324
		PointlightNegativeX = 2,
		// Token: 0x04000915 RID: 2325
		PointlightPositiveY = 4,
		// Token: 0x04000916 RID: 2326
		PointlightNegativeY = 8,
		// Token: 0x04000917 RID: 2327
		PointlightPositiveZ = 16,
		// Token: 0x04000918 RID: 2328
		PointlightNegativeZ = 32,
		// Token: 0x04000919 RID: 2329
		DirectionalCascade0 = 64,
		// Token: 0x0400091A RID: 2330
		DirectionalCascade1 = 128,
		// Token: 0x0400091B RID: 2331
		DirectionalCascade2 = 256,
		// Token: 0x0400091C RID: 2332
		DirectionalCascade3 = 512,
		// Token: 0x0400091D RID: 2333
		Spotlight = 1024,
		// Token: 0x0400091E RID: 2334
		AreaLight = 2048,
		// Token: 0x0400091F RID: 2335
		Pointlight = 63,
		// Token: 0x04000920 RID: 2336
		Directional = 960,
		// Token: 0x04000921 RID: 2337
		All = 2047
	}
}
