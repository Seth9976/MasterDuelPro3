using System;
using System.Diagnostics;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x02000029 RID: 41
	[UsedByNativeCode]
	[DebuggerDisplay("Value = {Value}; Count = {Count}")]
	public struct ProfilerRecorderSample
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002C8A File Offset: 0x00000E8A
		public long Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x0400005E RID: 94
		private long value;

		// Token: 0x0400005F RID: 95
		private long count;

		// Token: 0x04000060 RID: 96
		private long refValue;
	}
}
