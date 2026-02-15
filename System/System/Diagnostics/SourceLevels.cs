using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Specifies the levels of trace messages filtered by the source switch and event type filter.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000158 RID: 344
	[Flags]
	public enum SourceLevels
	{
		/// <summary>Does not allow any events through.</summary>
		// Token: 0x04000621 RID: 1569
		Off = 0,
		/// <summary>Allows only <see cref="F:System.Diagnostics.TraceEventType.Critical" /> events through.</summary>
		// Token: 0x04000622 RID: 1570
		Critical = 1,
		/// <summary>Allows <see cref="F:System.Diagnostics.TraceEventType.Critical" /> and <see cref="F:System.Diagnostics.TraceEventType.Error" /> events through.</summary>
		// Token: 0x04000623 RID: 1571
		Error = 3,
		/// <summary>Allows <see cref="F:System.Diagnostics.TraceEventType.Critical" />, <see cref="F:System.Diagnostics.TraceEventType.Error" />, and <see cref="F:System.Diagnostics.TraceEventType.Warning" /> events through.</summary>
		// Token: 0x04000624 RID: 1572
		Warning = 7,
		/// <summary>Allows <see cref="F:System.Diagnostics.TraceEventType.Critical" />, <see cref="F:System.Diagnostics.TraceEventType.Error" />, <see cref="F:System.Diagnostics.TraceEventType.Warning" />, and <see cref="F:System.Diagnostics.TraceEventType.Information" /> events through.</summary>
		// Token: 0x04000625 RID: 1573
		Information = 15,
		/// <summary>Allows <see cref="F:System.Diagnostics.TraceEventType.Critical" />, <see cref="F:System.Diagnostics.TraceEventType.Error" />, <see cref="F:System.Diagnostics.TraceEventType.Warning" />, <see cref="F:System.Diagnostics.TraceEventType.Information" />, and <see cref="F:System.Diagnostics.TraceEventType.Verbose" /> events through.</summary>
		// Token: 0x04000626 RID: 1574
		Verbose = 31,
		/// <summary>Allows the <see cref="F:System.Diagnostics.TraceEventType.Stop" />, <see cref="F:System.Diagnostics.TraceEventType.Start" />, <see cref="F:System.Diagnostics.TraceEventType.Suspend" />, <see cref="F:System.Diagnostics.TraceEventType.Transfer" />, and <see cref="F:System.Diagnostics.TraceEventType.Resume" /> events through.</summary>
		// Token: 0x04000627 RID: 1575
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		ActivityTracing = 65280,
		/// <summary>Allows all events through.</summary>
		// Token: 0x04000628 RID: 1576
		All = -1
	}
}
