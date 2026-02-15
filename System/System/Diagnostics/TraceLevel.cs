using System;

namespace System.Diagnostics
{
	/// <summary>Specifies what messages to output for the <see cref="T:System.Diagnostics.Debug" />, <see cref="T:System.Diagnostics.Trace" /> and <see cref="T:System.Diagnostics.TraceSwitch" /> classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000165 RID: 357
	public enum TraceLevel
	{
		/// <summary>Output no tracing and debugging messages.</summary>
		// Token: 0x0400065F RID: 1631
		Off,
		/// <summary>Output error-handling messages.</summary>
		// Token: 0x04000660 RID: 1632
		Error,
		/// <summary>Output warnings and error-handling messages.</summary>
		// Token: 0x04000661 RID: 1633
		Warning,
		/// <summary>Output informational messages, warnings, and error-handling messages.</summary>
		// Token: 0x04000662 RID: 1634
		Info,
		/// <summary>Output all debugging and tracing messages.</summary>
		// Token: 0x04000663 RID: 1635
		Verbose
	}
}
