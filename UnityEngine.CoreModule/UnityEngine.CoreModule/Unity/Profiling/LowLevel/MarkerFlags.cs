using System;

namespace Unity.Profiling.LowLevel
{
	// Token: 0x0200002F RID: 47
	[Flags]
	public enum MarkerFlags : ushort
	{
		// Token: 0x04000072 RID: 114
		Default = 0,
		// Token: 0x04000073 RID: 115
		Script = 2,
		// Token: 0x04000074 RID: 116
		ScriptInvoke = 32,
		// Token: 0x04000075 RID: 117
		ScriptDeepProfiler = 64,
		// Token: 0x04000076 RID: 118
		AvailabilityEditor = 4,
		// Token: 0x04000077 RID: 119
		AvailabilityNonDevelopment = 8,
		// Token: 0x04000078 RID: 120
		Warning = 16,
		// Token: 0x04000079 RID: 121
		Counter = 128,
		// Token: 0x0400007A RID: 122
		SampleGPU = 256
	}
}
