using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001A2 RID: 418
	[Flags]
	internal enum TouchFlags : byte
	{
		// Token: 0x040009B5 RID: 2485
		IndirectTouch = 1,
		// Token: 0x040009B6 RID: 2486
		PrimaryTouch = 8,
		// Token: 0x040009B7 RID: 2487
		TapPress = 16,
		// Token: 0x040009B8 RID: 2488
		TapRelease = 32,
		// Token: 0x040009B9 RID: 2489
		OrphanedPrimaryTouch = 64,
		// Token: 0x040009BA RID: 2490
		BeganInSameFrame = 128
	}
}
