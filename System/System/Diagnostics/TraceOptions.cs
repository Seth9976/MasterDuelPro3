using System;

namespace System.Diagnostics
{
	/// <summary>Specifies trace data options to be written to the trace output.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000168 RID: 360
	[Flags]
	public enum TraceOptions
	{
		/// <summary>Do not write any elements.</summary>
		// Token: 0x0400066E RID: 1646
		None = 0,
		/// <summary>Write the logical operation stack, which is represented by the return value of the <see cref="P:System.Diagnostics.CorrelationManager.LogicalOperationStack" /> property.</summary>
		// Token: 0x0400066F RID: 1647
		LogicalOperationStack = 1,
		/// <summary>Write the date and time. </summary>
		// Token: 0x04000670 RID: 1648
		DateTime = 2,
		/// <summary>Write the timestamp, which is represented by the return value of the <see cref="M:System.Diagnostics.Stopwatch.GetTimestamp" /> method.</summary>
		// Token: 0x04000671 RID: 1649
		Timestamp = 4,
		/// <summary>Write the process identity, which is represented by the return value of the <see cref="P:System.Diagnostics.Process.Id" /> property.</summary>
		// Token: 0x04000672 RID: 1650
		ProcessId = 8,
		/// <summary>Write the thread identity, which is represented by the return value of the <see cref="P:System.Threading.Thread.ManagedThreadId" /> property for the current thread.</summary>
		// Token: 0x04000673 RID: 1651
		ThreadId = 16,
		/// <summary>Write the call stack, which is represented by the return value of the <see cref="P:System.Environment.StackTrace" /> property.</summary>
		// Token: 0x04000674 RID: 1652
		Callstack = 32
	}
}
