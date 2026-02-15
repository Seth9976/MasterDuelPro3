using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003C3 RID: 963
	[Flags]
	public enum RenderStateMask
	{
		// Token: 0x04000C5C RID: 3164
		Nothing = 0,
		// Token: 0x04000C5D RID: 3165
		Blend = 1,
		// Token: 0x04000C5E RID: 3166
		Raster = 2,
		// Token: 0x04000C5F RID: 3167
		Depth = 4,
		// Token: 0x04000C60 RID: 3168
		Stencil = 8,
		// Token: 0x04000C61 RID: 3169
		Everything = 15
	}
}
