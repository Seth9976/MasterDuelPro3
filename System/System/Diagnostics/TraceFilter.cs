using System;

namespace System.Diagnostics
{
	/// <summary>Provides the base class for trace filter implementations.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000163 RID: 355
	public abstract class TraceFilter
	{
		/// <summary>When overridden in a derived class, determines whether the trace listener should trace the event.</summary>
		/// <returns>true to trace the specified event; otherwise, false. </returns>
		/// <param name="cache">The <see cref="T:System.Diagnostics.TraceEventCache" /> that contains information for the trace event.</param>
		/// <param name="source">The name of the source.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A trace identifier number.</param>
		/// <param name="formatOrMessage">Either the format to use for writing an array of arguments specified by the <paramref name="args" /> parameter, or a message to write.</param>
		/// <param name="args">An array of argument objects.</param>
		/// <param name="data1">A trace data object.</param>
		/// <param name="data">An array of trace data objects.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600083F RID: 2111
		public abstract bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1, object[] data);

		// Token: 0x06000840 RID: 2112 RVA: 0x0002CA38 File Offset: 0x0002AC38
		internal bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage)
		{
			return this.ShouldTrace(cache, source, eventType, id, formatOrMessage, null, null, null);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0002CA58 File Offset: 0x0002AC58
		internal bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1)
		{
			return this.ShouldTrace(cache, source, eventType, id, formatOrMessage, args, data1, null);
		}

		// Token: 0x04000654 RID: 1620
		internal string initializeData;
	}
}
