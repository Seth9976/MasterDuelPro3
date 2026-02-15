using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001E0 RID: 480
	[Flags]
	public enum RenderPathCompatibility
	{
		// Token: 0x04000BCE RID: 3022
		Forward = 1,
		// Token: 0x04000BCF RID: 3023
		Deferred = 2,
		// Token: 0x04000BD0 RID: 3024
		ForwardPlus = 4,
		// Token: 0x04000BD1 RID: 3025
		All = 7
	}
}
