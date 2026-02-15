using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001B3 RID: 435
	[Flags]
	public enum LightLayerEnum
	{
		// Token: 0x04000997 RID: 2455
		Nothing = 0,
		// Token: 0x04000998 RID: 2456
		LightLayerDefault = 1,
		// Token: 0x04000999 RID: 2457
		LightLayer1 = 2,
		// Token: 0x0400099A RID: 2458
		LightLayer2 = 4,
		// Token: 0x0400099B RID: 2459
		LightLayer3 = 8,
		// Token: 0x0400099C RID: 2460
		LightLayer4 = 16,
		// Token: 0x0400099D RID: 2461
		LightLayer5 = 32,
		// Token: 0x0400099E RID: 2462
		LightLayer6 = 64,
		// Token: 0x0400099F RID: 2463
		LightLayer7 = 128,
		// Token: 0x040009A0 RID: 2464
		Everything = 255
	}
}
