using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200010F RID: 271
	[NativeHeader("Runtime/Graphics/ColorGamut.h")]
	[UsedByNativeCode]
	public enum ColorGamut
	{
		// Token: 0x04000333 RID: 819
		sRGB,
		// Token: 0x04000334 RID: 820
		Rec709,
		// Token: 0x04000335 RID: 821
		Rec2020,
		// Token: 0x04000336 RID: 822
		DisplayP3,
		// Token: 0x04000337 RID: 823
		HDR10,
		// Token: 0x04000338 RID: 824
		DolbyHDR,
		// Token: 0x04000339 RID: 825
		P3D65G22
	}
}
