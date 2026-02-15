using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200032F RID: 815
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum CompareFunction
	{
		// Token: 0x040008CD RID: 2253
		Disabled,
		// Token: 0x040008CE RID: 2254
		Never,
		// Token: 0x040008CF RID: 2255
		Less,
		// Token: 0x040008D0 RID: 2256
		Equal,
		// Token: 0x040008D1 RID: 2257
		LessEqual,
		// Token: 0x040008D2 RID: 2258
		Greater,
		// Token: 0x040008D3 RID: 2259
		NotEqual,
		// Token: 0x040008D4 RID: 2260
		GreaterEqual,
		// Token: 0x040008D5 RID: 2261
		Always
	}
}
