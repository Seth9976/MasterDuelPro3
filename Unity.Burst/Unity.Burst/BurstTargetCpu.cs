using System;

namespace Unity.Burst
{
	// Token: 0x02000013 RID: 19
	internal enum BurstTargetCpu
	{
		// Token: 0x040000A3 RID: 163
		Auto,
		// Token: 0x040000A4 RID: 164
		X86_SSE2,
		// Token: 0x040000A5 RID: 165
		X86_SSE4,
		// Token: 0x040000A6 RID: 166
		X64_SSE2,
		// Token: 0x040000A7 RID: 167
		X64_SSE4,
		// Token: 0x040000A8 RID: 168
		AVX,
		// Token: 0x040000A9 RID: 169
		AVX2,
		// Token: 0x040000AA RID: 170
		WASM32,
		// Token: 0x040000AB RID: 171
		ARMV7A_NEON32,
		// Token: 0x040000AC RID: 172
		ARMV8A_AARCH64,
		// Token: 0x040000AD RID: 173
		THUMB2_NEON32,
		// Token: 0x040000AE RID: 174
		ARMV8A_AARCH64_HALFFP,
		// Token: 0x040000AF RID: 175
		ARMV9A
	}
}
