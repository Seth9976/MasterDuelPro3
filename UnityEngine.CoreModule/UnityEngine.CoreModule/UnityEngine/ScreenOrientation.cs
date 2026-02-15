using System;

namespace UnityEngine
{
	// Token: 0x02000113 RID: 275
	public enum ScreenOrientation
	{
		// Token: 0x0400034A RID: 842
		Portrait = 1,
		// Token: 0x0400034B RID: 843
		PortraitUpsideDown,
		// Token: 0x0400034C RID: 844
		LandscapeLeft,
		// Token: 0x0400034D RID: 845
		LandscapeRight,
		// Token: 0x0400034E RID: 846
		AutoRotation,
		// Token: 0x0400034F RID: 847
		[Obsolete("Enum member Unknown has been deprecated.", false)]
		Unknown = 0,
		// Token: 0x04000350 RID: 848
		[Obsolete("Use LandscapeLeft instead (UnityUpgradable) -> LandscapeLeft", true)]
		Landscape = 3
	}
}
