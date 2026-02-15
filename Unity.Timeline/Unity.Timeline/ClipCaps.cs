using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000032 RID: 50
	[Flags]
	public enum ClipCaps
	{
		// Token: 0x040000E6 RID: 230
		None = 0,
		// Token: 0x040000E7 RID: 231
		Looping = 1,
		// Token: 0x040000E8 RID: 232
		Extrapolation = 2,
		// Token: 0x040000E9 RID: 233
		ClipIn = 4,
		// Token: 0x040000EA RID: 234
		SpeedMultiplier = 8,
		// Token: 0x040000EB RID: 235
		Blending = 16,
		// Token: 0x040000EC RID: 236
		AutoScale = 40,
		// Token: 0x040000ED RID: 237
		All = -1
	}
}
