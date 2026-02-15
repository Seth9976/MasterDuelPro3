using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200032D RID: 813
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum BlendMode
	{
		// Token: 0x0400089C RID: 2204
		Zero,
		// Token: 0x0400089D RID: 2205
		One,
		// Token: 0x0400089E RID: 2206
		DstColor,
		// Token: 0x0400089F RID: 2207
		SrcColor,
		// Token: 0x040008A0 RID: 2208
		OneMinusDstColor,
		// Token: 0x040008A1 RID: 2209
		SrcAlpha,
		// Token: 0x040008A2 RID: 2210
		OneMinusSrcColor,
		// Token: 0x040008A3 RID: 2211
		DstAlpha,
		// Token: 0x040008A4 RID: 2212
		OneMinusDstAlpha,
		// Token: 0x040008A5 RID: 2213
		SrcAlphaSaturate,
		// Token: 0x040008A6 RID: 2214
		OneMinusSrcAlpha
	}
}
