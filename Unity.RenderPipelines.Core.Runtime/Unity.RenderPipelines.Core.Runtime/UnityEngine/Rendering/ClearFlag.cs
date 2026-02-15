using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000029 RID: 41
	[Flags]
	public enum ClearFlag
	{
		// Token: 0x040000A1 RID: 161
		None = 0,
		// Token: 0x040000A2 RID: 162
		Color = 1,
		// Token: 0x040000A3 RID: 163
		Depth = 2,
		// Token: 0x040000A4 RID: 164
		Stencil = 4,
		// Token: 0x040000A5 RID: 165
		DepthStencil = 6,
		// Token: 0x040000A6 RID: 166
		ColorStencil = 5,
		// Token: 0x040000A7 RID: 167
		All = 7
	}
}
