using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000017 RID: 23
	public struct JobRanges
	{
		// Token: 0x04000010 RID: 16
		internal int BatchSize;

		// Token: 0x04000011 RID: 17
		internal int NumJobs;

		// Token: 0x04000012 RID: 18
		public int TotalIterationCount;

		// Token: 0x04000013 RID: 19
		internal IntPtr StartEndIndex;
	}
}
