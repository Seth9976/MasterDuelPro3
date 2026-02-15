using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x02000332 RID: 818
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum StencilOp
	{
		// Token: 0x040008E1 RID: 2273
		Keep,
		// Token: 0x040008E2 RID: 2274
		Zero,
		// Token: 0x040008E3 RID: 2275
		Replace,
		// Token: 0x040008E4 RID: 2276
		IncrementSaturate,
		// Token: 0x040008E5 RID: 2277
		DecrementSaturate,
		// Token: 0x040008E6 RID: 2278
		Invert,
		// Token: 0x040008E7 RID: 2279
		IncrementWrap,
		// Token: 0x040008E8 RID: 2280
		DecrementWrap
	}
}
