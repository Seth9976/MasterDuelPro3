using System;

namespace Unity.Profiling
{
	// Token: 0x02000027 RID: 39
	[Flags]
	public enum ProfilerCounterOptions : ushort
	{
		// Token: 0x04000052 RID: 82
		None = 0,
		// Token: 0x04000053 RID: 83
		FlushOnEndOfFrame = 2,
		// Token: 0x04000054 RID: 84
		ResetToZeroOnFlush = 4
	}
}
