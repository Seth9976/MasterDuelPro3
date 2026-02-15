using System;

namespace UnityEngine
{
	// Token: 0x020000FD RID: 253
	public enum RenderingPath
	{
		// Token: 0x040002D5 RID: 725
		UsePlayerSettings = -1,
		// Token: 0x040002D6 RID: 726
		VertexLit,
		// Token: 0x040002D7 RID: 727
		Forward,
		// Token: 0x040002D8 RID: 728
		[Obsolete("DeferredLighting has been removed. Use DeferredShading, Forward or HDRP/URP instead.", false)]
		DeferredLighting,
		// Token: 0x040002D9 RID: 729
		DeferredShading
	}
}
