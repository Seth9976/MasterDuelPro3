using System;

namespace Unity.Profiling
{
	// Token: 0x02000028 RID: 40
	[Flags]
	public enum ProfilerRecorderOptions
	{
		// Token: 0x04000056 RID: 86
		None = 0,
		// Token: 0x04000057 RID: 87
		StartImmediately = 1,
		// Token: 0x04000058 RID: 88
		KeepAliveDuringDomainReload = 2,
		// Token: 0x04000059 RID: 89
		CollectOnlyOnCurrentThread = 4,
		// Token: 0x0400005A RID: 90
		WrapAroundWhenCapacityReached = 8,
		// Token: 0x0400005B RID: 91
		SumAllSamplesInFrame = 16,
		// Token: 0x0400005C RID: 92
		GpuRecorder = 64,
		// Token: 0x0400005D RID: 93
		Default = 24
	}
}
