using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200032E RID: 814
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum BlendOp
	{
		// Token: 0x040008A8 RID: 2216
		Add,
		// Token: 0x040008A9 RID: 2217
		Subtract,
		// Token: 0x040008AA RID: 2218
		ReverseSubtract,
		// Token: 0x040008AB RID: 2219
		Min,
		// Token: 0x040008AC RID: 2220
		Max,
		// Token: 0x040008AD RID: 2221
		LogicalClear,
		// Token: 0x040008AE RID: 2222
		LogicalSet,
		// Token: 0x040008AF RID: 2223
		LogicalCopy,
		// Token: 0x040008B0 RID: 2224
		LogicalCopyInverted,
		// Token: 0x040008B1 RID: 2225
		LogicalNoop,
		// Token: 0x040008B2 RID: 2226
		LogicalInvert,
		// Token: 0x040008B3 RID: 2227
		LogicalAnd,
		// Token: 0x040008B4 RID: 2228
		LogicalNand,
		// Token: 0x040008B5 RID: 2229
		LogicalOr,
		// Token: 0x040008B6 RID: 2230
		LogicalNor,
		// Token: 0x040008B7 RID: 2231
		LogicalXor,
		// Token: 0x040008B8 RID: 2232
		LogicalEquivalence,
		// Token: 0x040008B9 RID: 2233
		LogicalAndReverse,
		// Token: 0x040008BA RID: 2234
		LogicalAndInverted,
		// Token: 0x040008BB RID: 2235
		LogicalOrReverse,
		// Token: 0x040008BC RID: 2236
		LogicalOrInverted,
		// Token: 0x040008BD RID: 2237
		Multiply,
		// Token: 0x040008BE RID: 2238
		Screen,
		// Token: 0x040008BF RID: 2239
		Overlay,
		// Token: 0x040008C0 RID: 2240
		Darken,
		// Token: 0x040008C1 RID: 2241
		Lighten,
		// Token: 0x040008C2 RID: 2242
		ColorDodge,
		// Token: 0x040008C3 RID: 2243
		ColorBurn,
		// Token: 0x040008C4 RID: 2244
		HardLight,
		// Token: 0x040008C5 RID: 2245
		SoftLight,
		// Token: 0x040008C6 RID: 2246
		Difference,
		// Token: 0x040008C7 RID: 2247
		Exclusion,
		// Token: 0x040008C8 RID: 2248
		HSLHue,
		// Token: 0x040008C9 RID: 2249
		HSLSaturation,
		// Token: 0x040008CA RID: 2250
		HSLColor,
		// Token: 0x040008CB RID: 2251
		HSLLuminosity
	}
}
