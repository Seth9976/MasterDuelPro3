using System;

namespace Unity.Burst
{
	// Token: 0x02000014 RID: 20
	[Flags]
	internal enum NativeDumpFlags
	{
		// Token: 0x040000B1 RID: 177
		None = 0,
		// Token: 0x040000B2 RID: 178
		IL = 1,
		// Token: 0x040000B3 RID: 179
		Unused = 2,
		// Token: 0x040000B4 RID: 180
		IR = 4,
		// Token: 0x040000B5 RID: 181
		IROptimized = 8,
		// Token: 0x040000B6 RID: 182
		Asm = 16,
		// Token: 0x040000B7 RID: 183
		Function = 32,
		// Token: 0x040000B8 RID: 184
		Analysis = 64,
		// Token: 0x040000B9 RID: 185
		IRPassAnalysis = 128,
		// Token: 0x040000BA RID: 186
		ILPre = 256,
		// Token: 0x040000BB RID: 187
		IRPerEntryPoint = 512,
		// Token: 0x040000BC RID: 188
		All = 1021
	}
}
